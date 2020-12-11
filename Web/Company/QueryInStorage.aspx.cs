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
using Model.Other;
using System.Text;
using BLL.other.Company;
using BLL.CommonClass;

using System.Data.SqlClient;
using DAL;
using BLL.Logistics;

public partial class Company_QueryInStorage : BLL.TranslationBase 
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(EnumCompanyPermission.StorageInLibSearch);
        Response.Cache.SetExpires(DateTime.Now);

        if (!Common.WPermission(Session["Company"].ToString()))
        {
            vistable.Visible = false;
            Response.Write(Common.ReturnAlert("该管理员没有仓库权限！！"));
            return;
        }
        vistable.Visible = true;
        if (!Page.IsPostBack)
        {
           
            Permissions.CheckManagePermission(EnumCompanyPermission.StorageInLibSearch);
            Response.Cache.SetExpires(DateTime.Now);
            //Session["Company"] = "";
            //if (Session["Default_Country"] == null) Session["Default_Country"] = "";
            //cd.BindCountry_List(this.DropDownList1, Session["Default_Country"].ToString());
            BindCountryList();
            DataBindWareHouse();
            DataBindDepotSeat();
            DateTime dt = DateTime.Now;
            string t = dt.Year.ToString() + "-" + (dt.Month<10?("0"+dt.Month.ToString()):dt.Month.ToString())+ "-" + (dt.Day<10?("0"+dt.Day.ToString()):dt.Day.ToString());
            DateTime t1 = dt.AddDays(-30);
            string t2 = t1.Year.ToString() + "-" +( t1.Month<10?("0"+t1.Month.ToString()):t1.Month.ToString()) + "-" + (t1.Day<10?("0"+t1.Day.ToString()):t1.Day.ToString());
            this.TextBox2.Text = t2;
            this.TextBox3.Text = t;
            ViewState["SQL1"] = "";
            this.Panel1.Visible = false;
            this.Panel2.Visible = false;
            this.btn.Visible = false;
            this.Button2.Visible = false;
            this.img1.Style.Add("display", "block");
            this.img2.Style.Add("display", "none");
            Button1_Click(null, null);
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.GridView1NmaeNiHao,
                new string[][]{
                    new string []{"000399", "查看详细"},
                    new string []{"000263","产品编码"},
                    new string []{"000501","产品名称"},
                    new string []{"000515","产品数量"},
                    new string []{"000518","单位"},
                    new string []{"000041","总金额"},
                    new string []{"000562","币种"}});
        this.TranControls(this.Button1, new string[][] { new string[] { "000048", "查 询" } });
            }

    private void Translations2()
    {
        this.TranControls(this.GridView2,
                new string[][]{
                    new string []{"000263","产品编码"},
                    new string []{"000501","产品名称"},
                    new string []{"000505","数量"},
                    new string []{"000518","单位"},
                    new string []{"000503","单价"},
                    new string []{"000041","总金额"},
                    new string []{"000562","币种"},
                    new string []{"000613","日期"},
                    new string []{"000655","审核人"},
                    new string []{"000658","批次"},
                    new string []{"000453","类型"},
                    new string []{"000078","备注"}});
        this.TranControls(this.Back, new string[][] { new string[] { "000096", "返 回" } });

    }

    private void BindCountryList()
    {
        this.DropDownList1.DataSource = CountryBLL.GetCountryModels();
        this.DropDownList1.DataTextField = "name";
        this.DropDownList1.DataValueField = "countrycode";
        this.DropDownList1.DataBind();
        Translations();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int docTypeId = Convert.ToInt32(QueryInStorageBLL.GetDocTypeIDEByDocTypeCode("RK")); //入库单
        int docTypeId1 = Convert.ToInt32(QueryInStorageBLL.GetDocTypeIDEByDocTypeCode("BY"));//报溢单
        int docTypeId2 = Convert.ToInt32(QueryInStorageBLL.GetDocTypeIDEByDocTypeCode("TH"));//退货单
        int docTypeId3 = Convert.ToInt32(QueryInStorageBLL.GetDocTypeIDEByDocTypeCode("DB"));//调拨单
        try
        {
            DateTime dtTemp1 = Convert.ToDateTime(this.TextBox2.Text).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime();
            DateTime dtTemp2 = Convert.ToDateTime(this.TextBox3.Text).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime();
            if (dtTemp1 > dtTemp2)
            {
                msg = GetTran("000479", "开始时间不能大于结束时间")+"！";
            }
        }
        catch
        {
            msg = GetTran("000485", "日期非法,请重新输入") + "!";
            return;
        }
        string sqlSQL_WareHouseID = "";
        if (this.ddlWareHouse.SelectedValue != "-1")
        {
            sqlSQL_WareHouseID = " and M.WareHouseID=" + this.ddlWareHouse.SelectedValue + " and M.DepotSeatID=" + this.ddlDepotSeat.SelectedValue;
        }

        StringBuilder sb1 = new StringBuilder();
        sb1.Append("SELECT D.ProductID, P.ProductName, SUM(D.ProductQuantity)");
        sb1.Append("AS ProductQuantity, SUM(D.ProductTotal) AS ProductTotal,U.ProductUnitName as UnitName ");
        sb1.Append("FROM InventoryDocDetails D INNER JOIN ");
        sb1.Append("Product P ON D.ProductID = P.ProductID INNER JOIN ");
        sb1.Append("InventoryDoc M ON D.DocID = M.DocID " + sqlSQL_WareHouseID);
        sb1.Append(" left outer join ProductUnit U on P.SmallProductUnitID =  U.ProductUnitID ");
        sb1.Append("WHERE (M.DocTypeID =" + docTypeId + " or M.DocTypeID=" + docTypeId1 + " or M.DocTypeID=" + docTypeId1 + " or M.DocTypeID=" + docTypeId3 + ")");
        sb1.Append("And  StateFlag='1' And CloseFlag='0'  And DATEADD(day, DATEDIFF(day,0,M.DocAuditTime ), 0) between '" + this.TextBox2.Text + "' and  '" + this.TextBox3.Text + "'");
        sb1.Append(" GROUP BY D.ProductID, P.ProductName,U.ProductUnitName");

        ViewState["SQL1"] = sb1.ToString();
        StringBuilder sb2 = new StringBuilder();
        string key = "D.ProductID";
        this.Panel1.Visible = true;
        this.Panel2.Visible = false;
        this.btn.Visible = true;
        this.img1.Style.Add("display", "block");
        this.Button2.Visible = false;
        this.img2.Style.Add("display", "none");
        sb2.Append("D.ProductID = P.ProductID and P.SmallProductUnitID =  U.ProductUnitID and D.DocID = M.DocID and M.WareHouseID=" + this.ddlWareHouse.SelectedValue + "and M.DepotSeatID=" + this.ddlDepotSeat.SelectedValue);
        sb2.Append("and (M.DocTypeID =" + docTypeId + " or M.DocTypeID=" + docTypeId1 + " or M.DocTypeID=" + docTypeId2 + " or M.DocTypeID=" + docTypeId3 + ") And  StateFlag='1' And CloseFlag='0'  And DATEADD(day, DATEDIFF(day,0,M.DocAuditTime ), 0) between '" + this.TextBox2.Text + "' and  '" + this.TextBox3.Text + "'");
        sb2.Append(" GROUP BY D.ProductID, P.ProductName,U.ProductUnitName");
        string table = "InventoryDocDetails as D, Product as P,InventoryDoc as M,ProductUnit as U";

        string cloumns = "D.ProductID, P.ProductName, SUM(D.ProductQuantity) AS ProductQuantity, SUM(D.ProductTotal) AS ProductTotal,U.ProductUnitName as UnitName";

        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.Pageindex = 0;
        pager.PageSize = 10;
        pager.PageTable = table;
        pager.Condition = sb2.ToString();
        pager.PageColumn = cloumns;
        pager.ControlName = "GridView1NmaeNiHao";
        pager.PageCount = 0;
        pager.key = key;
        pager.InitBindData = true;
        //pager.PageBind(0, 10, pager.PageTable, pager.PageColumn, pager.Condition, pager.key, pager.ControlName);
        pager.PageBind1();
        //if (pager.PageCount > 0)
        //{
        this.Panel1.Visible = true;
        //this.GridView1NmaeNiHao.Visible = true;
        this.btn.Visible = true;
        this.img1.Style.Add("display", "block");
        this.Button2.Visible = false;
        this.img2.Style.Add("display", "none");
        this.label2.Text = "<p align=center>" + GetTran("000488", "统计信息");
        //}
        //else
        //{
        //    this.Panel1.Visible = false;
        //    //this.GridView1NmaeNiHao.Visible = false;
        //    this.btn.Visible = false;
        //    this.img1.Style.Add("display", "none");
        //    this.Button2.Visible = false;
        //    this.img2.Style.Add("display", "none");
        //}
        ViewState["SQL1"].ToString();
        Translations();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Country1_SelectedIndexChanged( sender, e);
        Button1_Click(null, null);
        Session["Default_Country"] = this.DropDownList1.SelectedItem.Text;
    }
    public string getCurrencyName(string productid)
    {
        string CurrencyName = StorageInBrowseBLL.GetCurrencyName(productid);
        return CurrencyName;
    }
    #region 得到产品编码
    /// <summary>
    /// 得到产品编码
    /// </summary>
    /// <returns>得到产品编码</returns>
    public string GetProductNameCode(string productId)
    {
        return CommonDataBLL.GetProductNameCode(productId);
    }
    #endregion
    protected void btn_Click(object sender, EventArgs e)
    {
       
        DataTable dt = DBHelper.ExecuteDataTable(ViewState["SQL1"].ToString());
        DataTable dt2 = dt.Clone();
        DataColumn dc2 = dt2.Columns["ProductID"];
        dc2.DataType = Type.GetType("System.String");
        //DataColumn dcc = dt2.Columns.Add("currency","System.String");
        dt2.Rows.Add(dt2.NewRow());
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            dt2.Rows.Add(dt2.NewRow());
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                
                dt2.Rows[j][i] = dt.Rows[j][i];
            }
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt2.Rows[i]["ProductID"] = GetProductNameCode(dt.Rows[i]["ProductID"].ToString());
            dt2.Rows[i]["ProductID"] = "&nbsp;" + dt2.Rows[i]["ProductID"];
        }
        Excel.OutToExcel(dt2, GetTran("000511", "库存表"), new string[] { "ProductID=" + GetTran("000263", "产品编码"), "ProductName=" + GetTran("000501", "产品名称"), "ProductQuantity=" + GetTran("000515", "产品数量"), "UnitName=" + GetTran("000518", "单位"), "ProductTotal=" + GetTran("000041", "总金额") });
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataTable dt = DBHelper.ExecuteDataTable(ViewState["dt1"].ToString());
    
        DataTable dt2 = dt.Clone();
        DataColumn dc2 = dt2.Columns["ProductID"];
        dc2.DataType = Type.GetType("System.String");
        dt2.Rows.Add(dt2.NewRow());
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            dt2.Rows.Add(dt2.NewRow());
            for (int i = 0; i < dt.Columns.Count; i++)
            {

                dt2.Rows[j][i] = dt.Rows[j][i];
            }
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt2.Rows[i]["ProductID"] = GetProductNameCode(dt.Rows[i]["ProductID"].ToString());
            dt2.Rows[i]["ProductID"] = "&nbsp;" + dt2.Rows[i]["ProductID"];
        }

        Excel.OutToExcel(dt2, GetTran("007098", "入库明细表"), new string[] { "ProductId="+ GetTran("000263", "产品编码"), "ProductName="+ GetTran("000501", "产品名称"), "ProductQuantity="+ GetTran("000505", "数量"), "UnitName="+ GetTran("000518", "单位"), "UnitPrice="+ GetTran("000503", "单价"), "ProductTotal="+ GetTran("000041", "总金额"), 
            "DocAuditTime="+ GetTran("000613", "日期"), "DocAuditer="+ GetTran("000655", "审核人"), "BatchCode="+ GetTran("000658", "批次"), "DocType="+ GetTran("000453", "类型") });

    }
    protected void GridView1NmaeNiHao_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "LinkbtnXinxi")
        {
            int docTpyeId = Convert.ToInt32(QueryInStorageBLL.GetDocTypeIDEByDocTypeCode("RK")); //入库单
            int docTpyeId1 = Convert.ToInt32(QueryInStorageBLL.GetDocTypeIDEByDocTypeCode("BY"));//报溢单
            int docTpyeId2 = Convert.ToInt32(QueryInStorageBLL.GetDocTypeIDEByDocTypeCode("TH"));//退货单
            int docTypeId3 = Convert.ToInt32(QueryInStorageBLL.GetDocTypeIDEByDocTypeCode("DB"));//调拨单
            string sqlSQL_WareHouseID = "";
            if (this.ddlWareHouse.SelectedValue != "-1")
            {
                sqlSQL_WareHouseID = " and M.warehouseID=" + this.ddlWareHouse.SelectedValue + " and M.DepotSeatID=" + this.ddlDepotSeat.SelectedValue;
            }

            this.Panel1.Visible = false;
            this.Panel2.Visible = true;
            this.btn.Visible = false;
            //  this.Button2.Visible = true;
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT D.ProductID, P.ProductName, D.ProductQuantity, D.UnitPrice,D.ProductTotal, M.DocAuditTime, M.Provider, M.DocAuditer,M.Note, M.BatchCode,U.ProductUnitName as UnitName, case M.DocTypeID when 1 then '" + GetTran("000579", "入库单") + "' when 4 then '" + GetTran("000584", "报溢单") + "' when 15 then '" + GetTran("000583", "退货单") + "' when 9 then '" + GetTran("000581", "调拨单") + "' else '' end as DocType ");
            sb.Append("D.ProductTotal, M.DocAuditTime, M.Provider, M.DocAuditer,");
            sb.Append("M.Note, M.BatchCode,U.ProductUnitName as UnitName ");
            sb.Append("FROM InventoryDocDetails D INNER JOIN ");
            sb.Append("Product P ON D.ProductID = P.ProductID INNER JOIN ");
            sb.Append("InventoryDoc M ON D.DocID = M.DocID " + sqlSQL_WareHouseID);
            sb.Append(" left outer join	ProductUnit U on P.SmallProductunitID =  U.ProductUnitID ");
            sb.Append(" WHERE P.Countrycode=" + System.Convert.ToInt32(this.DropDownList1.SelectedValue) + " and  (M.DocTypeID =" + docTpyeId + " or M.DocTypeID=" + docTpyeId1 + " or M.DocTypeID=" + docTpyeId2 + " or M.DocTypeID=" + docTypeId3 + ") And  StateFlag='1' And CloseFlag='0' And DATEADD(day, DATEDIFF(day,0,M.DocAuditTime ), 0) between '" + this.TextBox2.Text + "' and  '" + this.TextBox3.Text + "'");
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string PrID = ((HtmlInputHidden)row.FindControl("hidPrID")).Value;
            ViewState["PrID"] = PrID;
            sb.Append(" and D.ProductID=" + PrID);

            string table = "InventoryDocDetails as D, Product as P,InventoryDoc as M,ProductUnit as U";
            StringBuilder sb3 = new StringBuilder();
            sb3.Append("D.ProductID = P.ProductID and P.SmallProductunitID =  U.ProductUnitID and D.DocID = M.DocID and M.WareHouseID=" + this.ddlWareHouse.SelectedValue + " and M.DepotSeatID=" + this.ddlDepotSeat.SelectedValue);
            sb3.Append(" and P.Countrycode=" + System.Convert.ToInt32(this.DropDownList1.SelectedValue) + " and  (M.DocTypeID =" + docTpyeId + " or M.DocTypeID=" + docTpyeId1 + "or M.DocTypeID=" + docTpyeId2 + " or M.DocTypeID=" + docTypeId3 + ") And  StateFlag='1' And CloseFlag='0' And DATEADD(day, DATEDIFF(day,0,M.DocAuditTime ), 0) between '" + this.TextBox2.Text + "' and  '" + this.TextBox3.Text + "'" + " and D.ProductID=" + PrID);
            string key = "D.DocDetailsID";
            string cloumns = " D.ProductID, P.ProductName, D.ProductQuantity, D.UnitPrice,D.ProductTotal, M.DocAuditTime, M.Provider, M.DocAuditer,M.Note, M.BatchCode,U.ProductUnitName as UnitName, case M.DocTypeID when 1 then '" + GetTran("000579", "入库单") + "' when 4 then '" + GetTran("000584", "报溢单") + "' when 15 then '" + GetTran("000583", "退货单") + "' when 9 then '" + GetTran("000581", "调拨单") + "' else '' end as DocType";
          //  Pager pagerT = Page.FindControl("Pager11") as Pager;
           
            Pager11.Pageindex = 0;
            Pager11.PageSize = 10;
            Pager11.PageTable = table;
            Pager11.Condition = sb3.ToString();
            Pager11.PageColumn = cloumns;
            Pager11.ControlName = "GridView2";
            Pager11.key = key;
            Pager11.PageCount = 0;
            Pager11.PageBind1();

          
            //  if (Pager11.PageCount > 0)
            // {
            this.Button2.Visible = true;
            this.img2.Style.Add("display", "block");
            this.btn.Visible = false;
            this.img1.Style.Add("display", "none");
            // }
            ViewState["dt1"] = "Select " + cloumns + " From " + table + " Where " + sb3.ToString() + " Order By D.DocDetailsID";
        }
        Translations2();
    }
    protected void GridView1NmaeNiHao_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            LinkButton link = (LinkButton)e.Row.FindControl("LinkBtnSearch");
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void Back_Click(object sender, EventArgs e)
    {
        this.Panel1.Visible = true;
        this.Panel2.Visible = false;
        this.Button2.Visible = false;
        this.img2.Style.Add("display", "none");
        this.btn.Visible = true;
        this.img1.Style.Add("display", "block");
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
    }

    private void DataBindWareHouse()
    {
        ///通过管理员编号获取仓库相应的权限
        DataTable dt = StorageInBLL.GetMoreManagerPermissionByNumber(Session["Company"].ToString());
        DataView dataev = new DataView(dt);
        dataev.RowFilter = "[CountryCode]=" + DropDownList1.SelectedValue;
        ddlWareHouse.DataSource = dataev;

        //ddlWareHouse.DataSource = dt;
        ddlWareHouse.DataTextField = "WareHouseName";
        ddlWareHouse.DataValueField = "WareHouseID";
      
        ddlWareHouse.DataBind();
    }

    /// <summary>
    /// 绑定库位信息
    /// </summary>
    private void DataBindDepotSeat()
    {
        if (!string.IsNullOrEmpty(ddlWareHouse.SelectedValue) && ddlWareHouse.SelectedValue != "-2")
        {
        int wareHouseID = Convert.ToInt32(ddlWareHouse.SelectedItem.Value);
        ddlDepotSeat.DataSource = StorageInBLL.GetDepotSeatInfoByWareHouaseID(wareHouseID);
        ddlDepotSeat.DataTextField = "SeatName";
        ddlDepotSeat.DataValueField = "DepotSeatID";
        ddlDepotSeat.DataBind();
        }
         else
         {
             ddlDepotSeat.Items.Clear();
             ddlDepotSeat.Items.Add(new ListItem(GetTran("000589", "无库位"), "-2"));
         }
    }
    protected void ddlWareHouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBindDepotSeat();
        Button1_Click(null, null);
    }
    protected void ddlDepotSeat_SelectedIndexChanged(object sender, EventArgs e)
    {
        Button1_Click(null, null);
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        Button1_Click(null, null);
    }

    public string getSubRemark(object remark)
    { 
        return remark.ToString().Length>12?("<a title='"+remark.ToString()+"' style='cursor:hand'>"+remark.ToString().Substring(0,12)+"..."+"</a>"):remark.ToString();
    }
    /// <summary>
    /// 国家与仓库联动
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Country1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataView dataev = new DataView(BillOutOrderBLL.GetWareHouseName());
        //UserControl_Country cou = Page.FindControl("Country1") as UserControl_Country;
        dataev.RowFilter = "[CountryCode]=" + Convert.ToInt32(this.DropDownList1.SelectedValue);
        ddlWareHouse.DataSource = dataev;
        ddlWareHouse.DataTextField = "WareHouseName";
        ddlWareHouse.DataValueField = "WareHouseID";
        ddlWareHouse.DataBind();
        if (string.IsNullOrEmpty(ddlWareHouse.SelectedValue))
        {
            //ddlkuwei.DataSource = AddReportProfit.GetProductWareHouseInfo();
            //ddlkuwei.DataTextField = "name";
            //ddlkuwei.DataValueField = "id";
            //ddlkuwei.DataBind();
            //ddlkuwei.Enabled = false;
            //ddlcangku.Enabled = false;
            ddlWareHouse.Items.Add(new ListItem(GetTran("000592", "无仓库"), "-2"));
            ViewState["Ekuc"] = "false";
        }
        else
        {
            ViewState["Ekuc"] = "true";
        }
        ddlWareHouse_SelectedIndexChanged(null, null);
    }
}
