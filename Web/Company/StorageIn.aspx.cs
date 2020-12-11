using System;
using System.Collections;
using System.Collections.Generic;
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

//Add Namespace
using System.Data.SqlClient;
using BLL.other.Company;
using BLL.CommonClass;
using Model.Other;
using Model;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-07
 * 对应菜单：   库存管理->采购入库
 */

public partial class Company_StorageIn : BLL.TranslationBase
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

        if (!Common.WPermission(Session["Company"].ToString()))
        {
            visdiv.Visible = false;
            Response.Write(Common.ReturnAlert("该管理员没有仓库权限！！"));
            return;
        }
        visdiv.Visible = true;
        //检查店铺
        if (!IsPostBack)
        {
            Session["storageList"] = null;
            BindCountry();
            bindProvider();
            ddlCountry_SelectedIndexChanged(null, null);
            LoadDefaultData();
            Translaton();
        }
    }

    
    /// <summary>
    /// 加载默认数据
    /// xyc:2011-12-7
    /// </summary>
    private void LoadDefaultData()
    { 
        //加载业务员，默认当前登录者,不可修改
        if (Session["Company"] != null)
        {
            string number = Session["Company"].ToString();
            ManageModel loginer = ManagerBLL.GetManage(number);
            this.txtOperationPerson.Text = loginer==null?"":loginer.Name;
            this.txtOperationPerson.ReadOnly = true;
        }
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
            DateTime dt = MYDateTime1.GetCurrentDateTime();
            ///获取新的订单号        
            string availableOrderID = StorageInBLL.GetNewOrderID(EnumOrderFormType.InStorage);

            if (txtpici.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002027", "填写批次")));
                return;
            }

            ///通过批次通过入库批次获取入库批次行数
            int getCount = StorageInBLL.GetCountByBatchCode(txtpici.Text.Trim());

            if (getCount > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001995", "批次已存在!")));
                return;
            }
            var boolvalue=GetOrderInfo();
            if (!boolvalue)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("正确填入产品数量！"));
                return;
            }

            using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    string makeman = StorageInBLL.GetManageNameByNumber(Session["Company"].ToString());
                    int docTypeId = StorageInBLL.GetDocTypeIDByDocTypeName("RK");

                    if (ddlWareHouse.Enabled == false || ddlDepotSeat.Enabled == false)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001999", "对不起，没有可选仓库或库位！")));
                        return;
                    }
                    else
                    {
                        /// <summary>
                        /// 实例化库存单据模型
                        /// </summary>
                        InventoryDocModel inventoryDocModel = new InventoryDocModel
                        (
                            docTypeId, availableOrderID, dt, makeman, Convert.ToInt32(ddlProvider.SelectedValue), "", Convert.ToInt32(ddlWareHouse.SelectedValue),
                            Convert.ToInt32(ddlDepotSeat.SelectedValue),
                            Convert.ToDouble(ViewState["zongPrice"]), Convert.ToDouble(ViewState["totalPV"]), "",
                            CommonDataBLL.getMaxqishu(), "RK", txtMemo.Text.Trim(), 0, txtpici.Text.Trim(), txtOperationPerson.Text.Trim()
                        );
                        inventoryDocModel.OriginalDocID = this.txtOriginalDocID.Text.Trim();
                        inventoryDocModel.Address = this.txtAddress.Text.Trim();
                        //inventoryDocModel.Currency = StorageInBLL.GetMoreStandardMoneyIDByCountryCode(ddlCountry.SelectedValue);
                        //inventoryDocModel.PayCurrency = StorageInBLL.GetMoreCurrencyIDByCountryCode(ddlCountry.SelectedValue);

                        //inventoryDocModel.PayMoney = Convert.ToDecimal(ViewState["zongPrice"]) * StorageInBLL.GetRate_TimesForStandardMoneyByCountryCode(ddlCountry.SelectedValue);
                        inventoryDocModel.OperateIP = CommonDataBLL.OperateIP;
                        inventoryDocModel.OperateNum = CommonDataBLL.OperateBh;

                        ///生成一个单据入库
                        StorageInBLL.CreateInventoryDoc_WH(tran, inventoryDocModel);

                        foreach (InventoryDocDetailsModel InventoryDocDetails in list)//(ArrayList)ViewState["list"]
                        {
                            InventoryDocDetails.DocID = availableOrderID;
                        }
                        ///向单据明细表中插入相关记录
                        StorageInBLL.CreateBillofDocumentDetails(tran, list);

                        tran.Commit();
                    }
                }
                catch
                {
                    tran.Rollback();
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("002010", "入库单申请失败，请联系管理员!")));
                    return;
                }
                finally
                {
                    conn.Close();
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "abs", "alert('" + GetTran("002015", "入库单申请完成，等待管理员审核!") + "');location.href='StorageInBrowse.aspx'", true);
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
        if (ddlProvider.SelectedValue != "")
        {
            provider = ProviderManageBLL.GetProviderInfoById(Convert.ToInt32(ddlProvider.SelectedValue));
            txtAddress.Text = provider.Address;
        }
    }
}
