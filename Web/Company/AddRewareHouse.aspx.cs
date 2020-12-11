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
using Model.Other;
using BLL.other.Company;
using Model;
using BLL.CommonClass;
using System.Data.SqlClient;
using DAL;
using BLL.Logistics;

public partial class Company_AddRewareHouse : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            Response.Cache.SetExpires(DateTime.Now);
            Permissions.CheckManagePermission(EnumCompanyPermission.StorageAddReWareHouse);

            this.DropCurrery.DataSource = CountryBLL.GetCountryModels();
            this.DropCurrery.DataTextField = "name";
            this.DropCurrery.DataValueField = "countrycode";
            this.DropCurrery.DataBind();

            int cID = Convert.ToInt32(DropCurrery.SelectedItem.Value);

            //绑定入库仓库
            //this.ddlinwareHouse.DataSource = BillOutOrderBLL.GetWareHouseName();

            CountryModel mode = CountryBLL.GetCountryModels()[0];
            DataView dataev = new DataView(BillOutOrderBLL.GetWareHouseName());
            dataev.RowFilter = "[CountryCode]=" + CountryBLL.GetCountryByCode(mode.ID);
            ddlinwareHouse.DataSource = dataev;
            this.ddlinwareHouse.DataTextField = "WareHouseName";
            this.ddlinwareHouse.DataValueField = "WareHouseID";
            this.ddlinwareHouse.DataBind();
            if (string.IsNullOrEmpty(ddlinwareHouse.SelectedValue))
            {
                ddlinwareHouse.DataSource = AddReportProfit.GetProductWareHouseInfo();
                ddlinwareHouse.DataTextField = "name";
                ddlinwareHouse.DataValueField = "id";
                ddlinwareHouse.DataBind();
                ddlinwareHouse.Enabled = false;
                ddlintDepotSeatID.Enabled = false;
                ViewState["Ekuc"] = "false";
            }
            //绑定出库仓库
            //this.ddloutWareHouse.DataSource = BillOutOrderBLL.GetWareHouseName();

            DataView dataev1 = new DataView(BillOutOrderBLL.GetWareHouseName());
            dataev1.RowFilter = "[CountryCode]=" + CountryBLL.GetCountryByCode(mode.ID);
            ddloutWareHouse.DataSource = dataev1;
            this.ddloutWareHouse.DataTextField = "WareHouseName";
            this.ddloutWareHouse.DataValueField = "WareHouseID";
            this.ddloutWareHouse.DataBind();
            if (string.IsNullOrEmpty(ddloutWareHouse.SelectedValue))
            {
                ddloutWareHouse.DataSource = AddReportProfit.GetProductWareHouseInfo();
                ddloutWareHouse.DataTextField = "name";
                ddloutWareHouse.DataValueField = "id";
                ddloutWareHouse.DataBind();
                ddloutWareHouse.Enabled = false;
                ddloutDepotSeatID.Enabled = false;
                ViewState["Ekuc"] = "false";
            }
            else
            {
                ViewState["Ekuc"] = "true";
            }
            ddloutWareHouse_SelectedIndexChanged(null, null);
            ddlinwareHouse_SelectedIndexChanged(null, null);
            //绑定产品列表
            gridBind(cID, Convert.ToInt32(ddloutWareHouse.SelectedValue), Convert.ToInt32(ddloutDepotSeatID.SelectedValue));
        }
        Translations();
    }

    private void Translations()
    {
        //this.TranControls(this.givDoc,
        //        new string[][]{
        //            new string []{"000399","查看详细"},
        //            new string []{"000407","单据编号"},
        //            new string []{"000702","调入仓库"},
        //            new string []{"000703","调入库位"},
        //            new string []{"000704","调出仓库"},
        //            new string []{"000705","调出库位"},
        //            new string []{"000708","调拨时间"},
        //            new string []{"000045","期数"},
        //            new string []{"000414","积分"},
        //            new string []{"000041","总金额"},
        //            new string []{"000078","备注"}});
        this.TranControls(this.btnProfit, new string[][] { new string[] { "000750", "确认调拨" } });
    }

    //public string GetCurrency(object obj)
    //{
    //    return CurrencyDAL.GetCurrencyNameById(Convert.ToInt32(obj));
    //}

    private void gridBind(int country, int cuangku, int kuwei)
    {
        DataTable table = AddReportProfit.GetProductEQueryMenu(country, cuangku, kuwei);
        this.givproduct.DataSource = table;
        this.givproduct.DataBind();
        //判断是否有产品，如果没有报损按钮不显示
        if (table.Rows.Count > 0)
        {
            this.btnProfit.Visible = true;
        }
        else
        {
            this.btnProfit.Visible = false;
        }
        for (int i = 0; i < givproduct.Rows.Count; i++)
        {
            //DataRowView row=table.DefaultView[i];
            RadioButtonList RadioBtnCate = (RadioButtonList)givproduct.Rows[i].FindControl("rdoBtnCate");
            RadioBtnCate.SelectedIndex = 0;

            HtmlInputHidden HidBigSmallMultiPle = (HtmlInputHidden)givproduct.Rows[i].FindControl("HidBigSmallMultiPle");
            HtmlInputHidden HidBigUnitName = (HtmlInputHidden)givproduct.Rows[i].FindControl("HidBigUnitName");
            HtmlInputHidden HidSmallUnitName = (HtmlInputHidden)givproduct.Rows[i].FindControl("HidSmallUnitName");
            RadioBtnCate.Items[0].Value = HidBigSmallMultiPle.Value;
            RadioBtnCate.Items[1].Value = "1";
            RadioBtnCate.Items[0].Text = HidBigUnitName.Value + "(" + GetTran("000505", "数量") + "：" + HidBigSmallMultiPle.Value + ")";
            RadioBtnCate.Items[1].Text = HidSmallUnitName.Value + "(" + GetTran("000505", "数量") + "：1)";
            RequiredFieldValidator rf = (RequiredFieldValidator)givproduct.Rows[i].FindControl("rf");
            TextBox tb = (TextBox)givproduct.Rows[i].FindControl("txtPdtNum");
            int productid = Convert.ToInt32(((Label)givproduct.Rows[i].FindControl("lblproductID")).Text.Trim());
            ((Label)givproduct.Rows[i].FindControl("LblPrice")).Text = ((DAL.AddFreeOrderDAL.GetCurren(Convert.ToInt32(DAL.ProductDAL.GetPCurr(productid)))) * Convert.ToDouble(((Label)givproduct.Rows[i].FindControl("LblPrice")).Text)).ToString();
            Label lbunit = (Label)givproduct.Rows[i].FindControl("LblPrice");
            Label lbpv = (Label)givproduct.Rows[i].FindControl("LblPV");
            HtmlInputHidden hidneedNumber = (HtmlInputHidden)givproduct.Rows[i].FindControl("HidNeedNumber");
            RadioBtnCate.SelectedIndex = 1;
            RadioBtnCate.Enabled = false;
        }
    }
    #region
    protected void givproduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    #endregion
    protected void btnProfit_Click(object sender, EventArgs e)
    {
        if (ddloutDepotSeatID.SelectedValue == ddlinwareHouse.SelectedValue && ddloutDepotSeatID.SelectedValue == ddlintDepotSeatID.SelectedValue)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001993", "对不起，您不可以自己仓库调到自己仓库！") + "');</script>");
            return;
        }
        if (Convert.ToBoolean(ViewState["Ekuc"].ToString()))
        {
            if (Page.IsValid)
            {
                if (GetOrderInfo())
                {
                    saveOrder();
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("001869", "对不起，您没有库存权限！！！") + "')</script>");
        }
    }
    private void saveOrder()
    {
        DateTime dt = DateTime.Now.ToUniversalTime();
        InventoryDocBLL invent = new InventoryDocBLL();
        string availableOrderID = invent.GetNewOrderID(EnumOrderFormType.OneToAnother);

        //  CommonDataBLL common = new CommonDataBLL();
        string makeman = txtOperator.Text.Trim().Replace("'", "") == "" ? CommonDataBLL.GetNameByAdminID(Session["Company"].ToString()) : txtOperator.Text.Trim().Replace("'", "");
        // QueryInStorageBLL qinstore = new QueryInStorageBLL();
        int docTpyeId = Convert.ToInt32(QueryInStorageBLL.GetDocTypeIDEByDocTypeCode("DB"));//调拨单
        //CountryBLL bl = new CountryBLL();
        UserControl_Country countrys = Page.FindControl("Country1") as UserControl_Country;
        InventoryDocModel tobjopda_depotManageDoc = new InventoryDocModel
    (CommonDataBLL.GetNameByAdminID(Session["Company"].ToString()), CommonDataBLL.OperateIP, DateTime.UtcNow, CommonDataBLL.OperateBh,
    docTpyeId, availableOrderID, dt, makeman, 0, Session["Company"].ToString(), Convert.ToInt32(ddlinwareHouse.SelectedValue.Trim()), Convert.ToInt32(ddlintDepotSeatID.SelectedValue.Trim()),
    Convert.ToDouble(ViewState["zongPrice"]), Convert.ToDouble(ViewState["zongPv"]), "",
    (int)Application["maxqishu"], "DB", txtReamrk.Text, 1, DAL.CurrencyDAL.GetMoreCurrencyIDByCountryCode(DropCurrery.SelectedItem.Value));

        tobjopda_depotManageDoc.OriginalDocID = txtoldDocID.Text.Trim().Replace("'", "");

        //StorageInBLL.CreateNewBillofDocument(tobjopda_depotManageDoc);
        //AddReportDamageBLL.addReportDemage(CommonDataBLL.GetNameByAdminID(Session["Company"].ToString()), CommonDataBLL.OperateIP, CommonDataBLL.OperateBh, availableOrderID);
        //int exist;
        //string InsertStr = "";
        //string result = "";
        //foreach (InventoryDocDetailsModel opda_docDetail in (ArrayList)ViewState["list"])
        //{
        //    opda_docDetail.DocID = availableOrderID;
        //    string rt = InventoryDocBLL.SetDiaoBo(Convert.ToInt32(ddloutWareHouse.SelectedValue.Trim()), Convert.ToInt32(ddloutDepotSeatID.SelectedValue), Convert.ToInt32(ddlinwareHouse.SelectedValue.Trim()), Convert.ToInt32(ddlintDepotSeatID.SelectedValue), opda_docDetail.ProductQuantity, opda_docDetail.ProductID, opda_docDetail);
        //    if (rt != "1")
        //        result = result + "产品名称 " + ProductModeBLL.GetProductNameByID(opda_docDetail.ProductID) + " 调拨失败；";
        //    else
        //        result = result + "产品名称 " + ProductModeBLL.GetProductNameByID(opda_docDetail.ProductID) + " 调拨成功；";
        //}
        //int cID = Convert.ToInt32(DropCurrery.SelectedItem.Value);
        //gridBind(cID, Convert.ToInt32(ddloutWareHouse.SelectedValue), Convert.ToInt32(ddloutDepotSeatID.SelectedValue));
        //ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + result + "');</script>");
        //(inventoryDocDetailsModels, outWareHouse, outDepotSeatID, inWareHouse, inDepotSeatID, tobjopda_depotManageDoc);
        string rt = InventoryDocBLL.SetDiaoBo((ArrayList)ViewState["list"],
            Convert.ToInt32(ddlinwareHouse.SelectedValue.Trim()), Convert.ToInt32(ddlintDepotSeatID.SelectedValue), Convert.ToInt32(ddloutWareHouse.SelectedValue.Trim()), Convert.ToInt32(ddloutDepotSeatID.SelectedValue),
            tobjopda_depotManageDoc);

        if (rt == "1")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002001", "调拨成功") + "');</script>");
        }
        else
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002002", "调拨失败") + "');</script>");

        int cID = Convert.ToInt32(DropCurrery.SelectedItem.Value);
        gridBind(cID, Convert.ToInt32(ddloutWareHouse.SelectedValue), Convert.ToInt32(ddloutDepotSeatID.SelectedValue));
    }

    //验证输入信息
    private bool GetOrderInfo()
    {
        ViewState["zongPrice"] = 0;
        decimal zongPrice = 0;
        decimal zongPv = 0;
        int count = 0;
        decimal price = 0;
        decimal pv = 0;
        int id = 0;
        ArrayList list = new ArrayList();
        //检查输入的数据是否合法，
        foreach (GridViewRow row in this.givproduct.Rows)
        {
            TextBox text = row.FindControl("txtPdtNum") as TextBox;
            if (text.Text.Trim() != "")
            {
                try
                {
                    count += Convert.ToInt32(text.Text.Trim());

                    if (count < 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002003", "调拨数量必须为正整数！") + "');</script>");
                        return false;
                    }
                }
                catch
                {
                    ScriptHelper.SetAlert(Page,GetTran("002006", "调拨数量请输入数字！"));
                    return false;
                }
            }
        }
        //判断是否输入要报损的数据
        if (count == 0)
        {
            ScriptHelper.SetAlert(Page, GetTran("002007", "请输入调拨数量！"));
            return false;
        }
        //检查单条数据
        foreach (GridViewRow row in this.givproduct.Rows)
        {
            TextBox text = row.FindControl("txtPdtNum") as TextBox;
            //验证用户输入的是否是数字
            if (text.Text.Trim() != "")
            {
                try
                {
                    count = Convert.ToInt32(text.Text.Trim());

                    if (count < 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('"+ GetTran("002003", "调拨数量必须为正整数！") + "');</script>");
                        return false;
                    }
                }
                catch
                {
                    ScriptHelper.SetAlert(this.givproduct, GetTran("002006", "调拨数量请输入数字！"));
                    return false;
                }
            }
            else
            {
                count = 0;
            }
            if (count > 0)
            {
                price = Convert.ToDecimal(((Label)row.FindControl("LblPrice")).Text.Trim());
                pv = Convert.ToDecimal(((Label)row.FindControl("LblPV")).Text.Trim());
                RadioButtonList radiolist = row.FindControl("rdoBtnCate") as RadioButtonList;
                count = count * Convert.ToInt32(radiolist.SelectedValue);
                id = Convert.ToInt32((row.FindControl("lblhuoID") as Label).Text.Trim());
                int wareDate = int.Parse((row.FindControl("WHouseDate") as Label).Text.Trim());
                if (count > wareDate)
                {
                    ScriptHelper.SetAlert(Page, GetTran("000501", "产品名称") + (row.FindControl("lblName") as Label).Text + GetTran("001859", "在该库位中的库存数量不足!"));
                    return false;
                }
                InventoryDocDetailsModel invert = new InventoryDocDetailsModel();

                //invert.DepotSeatID = Convert.ToInt32(ddloutDepotSeatID.SelectedValue);
                invert.Batch = "";
                invert.DocID = "";
                invert.ExpectNum = Convert.ToInt32(Application["maxqishu"]);
                invert.MeasureUnit = "1";
                invert.ProductID = id;
                invert.ProductQuantity = count;
                invert.ProductTotal = Convert.ToDouble(count * price);
                invert.PV = Convert.ToDouble(pv);
                //invert.SelectedIndex = 0;
                invert.UnitPrice = Convert.ToDouble(price);
                list.Add(invert);
                zongPrice += count * price;
                zongPv += pv * count;
            }
        }
        ViewState["list"] = list;
        ViewState["zongPrice"] = zongPrice;
        ViewState["zongPv"] = zongPv;
        return true;
    }

    //绑定入库库位
    protected void ddlinwareHouse_SelectedIndexChanged(object sender, EventArgs e)
    {
          if (!string.IsNullOrEmpty(ddlinwareHouse.SelectedValue) && ddlinwareHouse.SelectedValue!="-1")
        {
        //绑定入库库位
        int id = Convert.ToInt32(ddlinwareHouse.SelectedValue);
        //DepotSeatDAL depot = new DepotSeatDAL();
        this.ddlintDepotSeatID.DataSource = DepotSeatDAL.GetDepotSeat(id.ToString());
        this.ddlintDepotSeatID.DataTextField = "SeatName";
        this.ddlintDepotSeatID.DataValueField = "DepotSeatID";
        this.ddlintDepotSeatID.DataBind();
        }
          else
          {
              ddlintDepotSeatID.Items.Clear();
              ddlintDepotSeatID.Items.Add(new ListItem(GetTran("000589", "无库位"), "-1"));
          }
    }
    //绑定出库库位
    protected void ddloutWareHouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddloutWareHouse.SelectedValue) && ddloutWareHouse.SelectedValue != "-1")
        {
        //绑定入库库位
        int id = Convert.ToInt32(ddloutWareHouse.SelectedValue);
        // DepotSeatDAL depot = new DepotSeatDAL();
        this.ddloutDepotSeatID.DataSource = DepotSeatDAL.GetDepotSeat(id.ToString());
        this.ddloutDepotSeatID.DataTextField = "SeatName";
        this.ddloutDepotSeatID.DataValueField = "DepotSeatID";
        this.ddloutDepotSeatID.DataBind();
         }
         else
         {
             ddloutDepotSeatID.Items.Clear();
             ddloutDepotSeatID.Items.Add(new ListItem(GetTran("000589", "无库位"), "-1"));
         }
        int cID = Convert.ToInt32(DropCurrery.SelectedItem.Value);
        gridBind(cID, Convert.ToInt32(ddloutWareHouse.SelectedValue), Convert.ToInt32(ddloutDepotSeatID.SelectedValue));
    }
    protected void givproduct_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }
    protected void ddloutDepotSeatID_SelectedIndexChanged(object sender, EventArgs e)
    {
        int cID = Convert.ToInt32(DropCurrery.SelectedItem.Value);
        gridBind(cID, Convert.ToInt32(ddloutWareHouse.SelectedValue), Convert.ToInt32(ddloutDepotSeatID.SelectedValue));
    }
    protected void DropCurrery_SelectedIndexChanged(object sender, EventArgs e)
    {
        Country1_SelectedIndexChanged(sender, e);
        Country2_SelectedIndexChanged(sender, e);
        int cID = Convert.ToInt32(DropCurrery.SelectedItem.Value);
        gridBind(cID, Convert.ToInt32(ddloutWareHouse.SelectedValue), Convert.ToInt32(ddloutDepotSeatID.SelectedValue));
    }

    /// <summary>
    /// 国家与仓库联动（调出）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Country1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataView dataev = new DataView(BillOutOrderBLL.GetWareHouseName());
        //UserControl_Country cou = Page.FindControl("Country1") as UserControl_Country;
        dataev.RowFilter = "[CountryCode]=" + Convert.ToInt32(this.DropCurrery.SelectedValue);
        ddloutWareHouse.DataSource = dataev;
        ddloutWareHouse.DataTextField = "WareHouseName";
        ddloutWareHouse.DataValueField = "WareHouseID";
        ddloutWareHouse.DataBind();
        if (string.IsNullOrEmpty(ddloutWareHouse.SelectedValue))
        {
            //ddlkuwei.DataSource = AddReportProfit.GetProductWareHouseInfo();
            //ddlkuwei.DataTextField = "name";
            //ddlkuwei.DataValueField = "id";
            //ddlkuwei.DataBind();
            //ddlkuwei.Enabled = false;
            //ddlcangku.Enabled = false;
            ddloutWareHouse.Items.Add(new ListItem(GetTran("000592", "无仓库"), "-1"));
            ViewState["Ekuc"] = "false";
        }
        else
        {
            ViewState["Ekuc"] = "true";
        }
        ddloutWareHouse_SelectedIndexChanged(null, null);
    }

    /// <summary>
    /// 国家与仓库联动 (调入)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Country2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataView dataev = new DataView(BillOutOrderBLL.GetWareHouseName());
        //UserControl_Country cou = Page.FindControl("Country1") as UserControl_Country;
        dataev.RowFilter = "[CountryCode]=" + Convert.ToInt32(this.DropCurrery.SelectedValue);
        ddlinwareHouse.DataSource = dataev;
        ddlinwareHouse.DataTextField = "WareHouseName";
        ddlinwareHouse.DataValueField = "WareHouseID";
        ddlinwareHouse.DataBind();
        if (string.IsNullOrEmpty(ddlinwareHouse.SelectedValue))
        {
            //ddlkuwei.DataSource = AddReportProfit.GetProductWareHouseInfo();
            //ddlkuwei.DataTextField = "name";
            //ddlkuwei.DataValueField = "id";
            //ddlkuwei.DataBind();
            //ddlkuwei.Enabled = false;
            //ddlcangku.Enabled = false;
            ddlinwareHouse.Items.Add(new ListItem(GetTran("000592", "无仓库"), "-1"));
            ViewState["Ekuc"] = "false";
        }
        else
        {
            ViewState["Ekuc"] = "true";
        }
        ddlinwareHouse_SelectedIndexChanged(null, null);
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        btnProfit_Click(null,null);
    }
}
