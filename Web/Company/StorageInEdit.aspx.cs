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
using BLL.other.Company;
using BLL.CommonClass;
using DAL;
using Model;
using Model.Other;

public partial class Company_StorageInEdit : BLL.TranslationBase
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
        ///采购入库
        Permissions.CheckManagePermission(EnumCompanyPermission.StorageStorageIn);
        Response.Cache.SetExpires(DateTime.Now);
        //ajaxPro注册1111111111
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));


        //检查店铺
        if (!IsPostBack)
        {
            Session["storageList"] = null;
            BindCountry();
            bindProvider();
            if (Request.QueryString["billid"] == null)
            {
                Response.End();
            }
            BindInfo(Request.QueryString["billid"].ToString());
            ddlCountry_SelectedIndexChanged(null, null);
        }
        Translaton();
    }

    private void BindInfo(string billid)
    {
        InventoryDocModel idm = StorageInBLL.getOrderInfo(billid);
        this.txtAddress.Text = idm.Address.ToString();
        this.txtpici.Text = idm.BatchCode.ToString();
        this.txtOperationPerson.Text = idm.OperationPerson.ToString();
        this.txtOriginalDocID.Text = idm.OriginalDocID.ToString();
        this.txtMemo.Text = idm.Note.ToString();
        this.ddlWareHouse.SelectedValue = idm.WareHouseID.ToString();
        int wareHouseID = Convert.ToInt32(idm.WareHouseID.ToString());
        this.ddlDepotSeat.SelectedValue = idm.DepotSeatID.ToString();
        this.ddlProvider.SelectedValue = Convert.ToString(idm.Provider);
        //ddlCountry.SelectedValue = CountryBLL.GetCountryCodeByID(idm.Currency.ToString());
        this.ViewState["billId"] = billid;

        DataTable dt = StorageInBLL.getProduct(billid);

        foreach (DataRow dr in dt.Rows)
        {
            InventoryDocDetailsModel model = new InventoryDocDetailsModel();
            model.ProductID = int.Parse(dr["productid"].ToString());
            model.UnitPrice = double.Parse(dr["unitprice"].ToString());
            model.MeasureUnit = "0";
            //model.Pici = dr["pici"].ToString();
            model.ProductQuantity = double.Parse(dr["productquantity"].ToString());
            list.Add(model);
        }
        Session["storageList"] = list;
    }
    public void Translaton()
    {
        this.TranControls(btnSaveOrder, new string[][] { new string[] { "002048", "确认入库单" } });
    }
    private void bindProvider()
    {
        ddlProvider.DataSource = StorageInBLL.GetProviderInfo();
        ddlProvider.DataTextField = "Name";
        ddlProvider.DataValueField = "ID";
        ddlProvider.DataBind();
        ddlProvider_SelectedIndexChanged(null, null);
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
    /// 产生树形菜单
    /// </summary>
    /// <param name="dian"></param>
    public void setProductMenu(string countryCode)
    {
        ProductTree myTree = new ProductTree();
        this.menuLabel.Text = myTree.GetCountryProduct(countryCode);
    }
    /// <summary>
    /// 国家改变事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedValue != "" || ddlCountry.SelectedValue != null)
        {
            setProductMenu(ddlCountry.SelectedValue);
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
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001992", "对不起，您所填写的入库产品的数量不能全部是0!")));
            return;
        }
        else
        {
            if (ddlProvider.SelectedValue == "")
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("006009", "对不起，请选择供应商!")));
                return;
            }
            if (ddlDepotSeat.SelectedValue == "")
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001934", "对不起，请选择库位!")));
                return;
            }

            if (txtpici.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002027", "填写批次")));
                return;
            }

            ///通过批次通过入库批次获取入库批次行数
            var boolvalue = GetOrderInfo();
            if (!boolvalue)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("正确填入产品数量！"));
                return;
            }

            DateTime dt = MYDateTime1.GetCurrentDateTime();
            string BatchCode = this.txtpici.Text.ToString();
            string availableOrderID = ViewState["billId"].ToString();
            int i = StorageInBLL.CheckBatch(availableOrderID, BatchCode);
            if (i > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001995", "批次已存在!")));
                return;
            }
            string makeman = CommonDataBLL.GetNameByAdminID(Session["Company"].ToString());
            int n = 0;
            InventoryDocModel idm = new InventoryDocModel();
            idm.Provider = Convert.ToInt32(ddlProvider.SelectedValue);
            idm.WareHouseID = Convert.ToInt32(this.ddlWareHouse.SelectedValue);
            idm.DepotSeatID = Convert.ToInt32(this.ddlDepotSeat.SelectedValue);
            idm.TotalMoney = Convert.ToDouble(ViewState["zongPrice"]);
            idm.TotalPV = Convert.ToDouble(ViewState["totalPV"]);
            idm.ExpectNum = CommonDataBLL.getMaxqishu();
            idm.Note = this.txtMemo.Text.ToString();
            idm.BatchCode = this.txtpici.Text.ToString();
            idm.OperationPerson = this.txtOperationPerson.Text.ToString();
            idm.DocID = ViewState["billId"].ToString();
            idm.Address = this.txtAddress.Text.ToString();
            idm.OriginalDocID = this.txtOriginalDocID.Text.ToString();
            idm.OperateIP = CommonDataBLL.OperateIP;
            idm.OperateNum = CommonDataBLL.OperateBh;
            ChangeLogs cl = new ChangeLogs("InventoryDoc", "DocID");
            cl.AddRecord(idm.DocID);
            n = StorageInBLL.updAndSaveOrder(idm, list);//(ArrayList)ViewState["list"]
            if (n > 0)
            {
                cl.AddRecord(idm.DocID);
                cl.ModifiedIntoLogs(ChangeCategory.company8, Session["Company"].ToString(), ENUM_USERTYPE.objecttype0);
                ScriptManager.RegisterStartupScript(this, GetType(), "abs", "alert('" + GetTran("004179", "入库单编辑完成，等待管理员审核！") + "');location.href='StorageInBrowse.aspx'", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("004180", "入库单编辑失败，请准确填写！")));
            }
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
            model.Batch = txtpici.Text;
            //model.DepotSeatID = Convert.ToInt32(ddlDepotSeat.SelectedValue);
            model.ExpectNum = CommonDataBLL.getMaxqishu();
            model.DocID = ViewState["billId"].ToString();
            zongPrice += model.ProductTotal;
            zongPv += model.PV;
            count++;
        }
        ViewState["zongPrice"] = zongPrice;
        ViewState["totalPV"] = zongPv;

        return count > 0 ? true : false;
    }
    protected void ddlProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
        ProviderInfoModel provider = new ProviderInfoModel();
        provider = ProviderManageBLL.GetProviderInfoById(Convert.ToInt32(ddlProvider.SelectedValue));
        txtAddress.Text = provider.Address;
    }
}