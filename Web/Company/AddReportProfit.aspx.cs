using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Model;
using BLL.other.Company;
using BLL.CommonClass;
using Model.Other;
using System.Data.SqlClient;
using DAL;
using BLL.Logistics;
public partial class Company_AddReportProfit : BLL.TranslationBase
{
    ArrayList list = new ArrayList();
    /// <summary>
    /// 页面加载事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(EnumCompanyPermission.StorageAddReportDamage);
        Response.Cache.SetExpires(DateTime.Now);
        //ajaxPro注册1111111111
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));


        //检查店铺
        if (!IsPostBack)
        {
            Session["storageList"] = null;
            BindCountry();
            ddlCountry_SelectedIndexChanged(null, null);
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.btnSaveOrder, new string[][] { new string[] { "000668", "确认报溢" } });
    }
    /// <summary>
    /// 绑定国家
    /// </summary>
    public void BindCountry()
    {
        this.ddlCountry.DataSource = StoreInfoEditBLL.bindCountry();
        this.ddlCountry.DataValueField = "countrycode";
        this.ddlCountry.DataTextField = "name";
        this.ddlCountry.DataBind();
    }
    /// <summary>
    /// 国家改变事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        menuLabel.Text = "";
        if (ddlCountry.SelectedValue != "" || ddlCountry.SelectedValue != null)
        {
            DataBindWareHouse();
        }
        else
        {
            string alertMessage = GetTran("001959", "对不起，没有可选国家!");
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + alertMessage + "');</script>");
            return;
        }
    }
    /// <summary>
    /// 绑定仓库信息
    /// </summary>
    private void DataBindWareHouse()
    {
        //通过管理员编号获取仓库相应的权限
        DataTable permissionDT = StorageInBLL.GetMoreManagerPermissionByNumber(Convert.ToString(Session["Company"]));
        if (permissionDT.Rows.Count < 1)
        {

            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001963", "对不起，你没有仓库权限!")));
            return;
        }
        else
        {
            ddlWareHouse.Items.Clear();
            DataTable wareHouseDT = StorageInBLL.GetWareHouseInfoByNumberCountryCode(Convert.ToString(Session["Company"]), ddlCountry.SelectedValue);
            if (wareHouseDT.Rows.Count < 1)
            {
                ddlDepotSeat.Items.Clear();
                return;
            }
            else
            {
                ddlWareHouse.DataSource = wareHouseDT;
                ddlWareHouse.DataTextField = "WareHouseName";
                ddlWareHouse.DataValueField = "WareHouseID";
                ddlWareHouse.DataBind();
                DataBindDepotSeat();
            }
        }
    }
    /// <summary>
    /// 绑定库位信息
    /// </summary>
    private void DataBindDepotSeat()
    {
        if (ddlWareHouse.SelectedIndex != -1)
        {
            int wareHouseID = Convert.ToInt32(ddlWareHouse.SelectedValue);
            ddlDepotSeat.DataSource = DAL.DepotSeatDAL.GetDepotSeat(wareHouseID.ToString());
            ddlDepotSeat.DataTextField = "SeatName";
            ddlDepotSeat.DataValueField = "DepotSeatID";
            ddlDepotSeat.DataBind();
            ddlDepotSeat_SelectedIndexChanged(null, null);
        }
        else
        {
            ddlWareHouse.Enabled = true;
            ddlDepotSeat.Enabled = false;
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001933", "对不起，请选择仓库!")));
            return;
        }
    }
    /// <summary>
    /// 仓库(DropDownList)事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlWareHouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWareHouse.SelectedIndex != -1)
        {
            DataBindDepotSeat();
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001932", "仓库错误信息，请联系管理员!")));
            return;
        }
    }

    /// <summary>
    /// 确认
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveOrder_Click(object sender, EventArgs e)
    {
        if (Session["storageList"] == null || Session["storageList"].ToString() == "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002082", "请输入报溢数量!")));
            return;
        }
        else
        {
            DateTime dt = DateTime.Now.ToUniversalTime();
            string availableOrderID = InventoryDocDAL.GetNewOrderID(Model.Other.EnumOrderFormType.CheckOutStorage);

            var boolvalue = GetOrderInfo();
            if (!boolvalue)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("正确填入产品数量！"));
                return;
            }
            string makeman = txtOperationPerson.Text.Trim().Replace("'", "") == "" ? CommonDataBLL.GetNameByAdminID(Session["Company"].ToString()) : txtOperationPerson.Text.Trim().Replace("'", "");
            int docTpyeId = Convert.ToInt32(QueryInStorageBLL.GetDocTypeIDEByDocTypeCode("BY"));//报损单
            //   CountryBLL bl = new CountryBLL();
            //UserControl_Country countrys = Page.FindControl("Country1") as UserControl_Country;
            InventoryDocModel tobjopda_depotManageDoc = new InventoryDocModel
           (CommonDataBLL.GetNameByAdminID(Session["Company"].ToString()), CommonDataBLL.OperateIP, DateTime.UtcNow, CommonDataBLL.OperateBh,
           docTpyeId, availableOrderID, dt, makeman, 0, Session["Company"].ToString(), Convert.ToInt32(ddlWareHouse.SelectedValue.Trim()), Convert.ToInt32(ddlDepotSeat.SelectedValue.Trim()),
           Convert.ToDouble(ViewState["zongPrice"]), Convert.ToDouble(ViewState["totalPV"]), "",
           CommonDataBLL.getMaxqishu(), "BY", txtMemo.Text, 1, DAL.AddFreeOrderDAL.GetCurrenT());

            tobjopda_depotManageDoc.OriginalDocID = txtOriginalDocID.Text.Trim().Replace("'", "");



            string rt = AddReportDamageBLL.ProductReportDamage_II(list, Convert.ToInt32(ddlWareHouse.SelectedValue.Trim()),
           Convert.ToInt32(ddlDepotSeat.SelectedValue.Trim()), "TotalIn", tobjopda_depotManageDoc);

            if (rt == "1")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002091", "报溢成功") + "');location.href='ReportProfit.aspx'</script>");
            }
            else
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002092", "报溢失败") + "');</script>");
            setProductMenu(ddlCountry.SelectedValue, ddlWareHouse.SelectedValue, ddlDepotSeat.SelectedValue);
        }
    }
    private bool GetOrderInfo()
    {
        int count = 0;
        double zongPrice = 0;
        double zongPv = 0;
        list = (ArrayList)Session["storageList"];
        foreach (InventoryDocDetailsModel model in list)
        {
            if (model.MeasureUnit != "0")
            {
                model.ProductQuantity = model.ProductQuantity * Convert.ToInt32(model.MeasureUnit);
            }
            if (model.ProductQuantity <= 0)
            {
                return false;
            }
            model.ProductTotal = model.UnitPrice * model.ProductQuantity;
            model.PV = model.PV * model.ProductQuantity;
            //model.DepotSeatID = Convert.ToInt32(ddlDepotSeat.SelectedValue);
            model.ExpectNum = CommonDataBLL.getMaxqishu();
            zongPrice += model.ProductTotal;
            zongPv += model.PV;
            count++;
        }
        ViewState["zongPrice"] = zongPrice;
        ViewState["totalPV"] = zongPv;

        return count > 0 ? true : false;
    }

    /// <summary>
    /// 绑定库位产品列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDepotSeat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepotSeat.SelectedIndex != -1)
        {
            Session["storageList"] = null;
            setProductMenu(ddlCountry.SelectedValue, ddlWareHouse.SelectedValue, ddlDepotSeat.SelectedValue);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001934", "对不起，请选择库位!")));
            return;
        }
    }

    /// <summary>
    /// 产生树形菜单
    /// </summary>
    /// <param name="dian"></param>
    public void setProductMenu(string countryCode, string WareHouseID, string DepotSeat)
    {
        ProductTree myTree = new ProductTree();
        this.menuLabel.Text = myTree.GetDepotSeatProduct(countryCode, WareHouseID, DepotSeat);
    }
}