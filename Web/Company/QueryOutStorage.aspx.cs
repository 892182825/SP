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
using BLL.CommonClass;
using BLL.other.Company;
using Model;
using Model.Other;
using DAL;
using System.Text;

using BLL.Logistics;
public partial class Company_QueryOutStorage : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.StorageQueryOutStorageOrders);

        if (!Common.WPermission(Session["Company"].ToString()))
        {
            vistable.Visible = false;
            Response.Write(Common.ReturnAlert("该管理员没有仓库权限！！"));
            return;
        }
        vistable.Visible = true;

        GridView1.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        if (Page.IsPostBack == false)
        {
            if (Session["Default_Country"] == null) Session["Default_Country"] = "";

            BindCountryList();//绑定国家 
            DataBindWareHouse();
            DataBindDepotSeat();
            DateTime dt = DateTime.Now;
            string t = dt.Year.ToString() + "-" + (dt.Month < 10 ? ("0" + dt.Month.ToString()) : dt.Month.ToString()) + "-" + (dt.Day < 10 ? ("0" + dt.Day.ToString()) : dt.Day.ToString());
            DateTime t1 = dt.AddDays(-30);
            string t2 = t1.Year.ToString() + "-" + (t1.Month < 10 ? ("0" + t1.Month.ToString()) : t1.Month.ToString()) + "-" + (t1.Day < 10 ? ("0" + t1.Day.ToString()) : t1.Day.ToString());
            this.TextBox2.Text = t2;
            this.TextBox3.Text = t;
            getOutStorage();

            this.Panel1.Visible = true;
            this.Panel2.Visible = false;

            Translations();
        }
    }

    private void Translations()
    {
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"000399", "查看详细"},
                    new string []{"000263","产品编码"},
                    new string []{"000501","产品名称"},
                    new string []{"000748","出库产品总数"},
                    new string []{"000751","出库产品总金额"},
                    new string []{"000753","退货产品总数"},
                    new string []{"000757","退货产品总金额"},
                    new string []{"000630","合计"},
                    new string []{"000762","合计总金额"},
                    new string []{"000562","币种"}});
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

    protected object GetProName(object obj)
    {
        return ProductModeBLL.GetProductNameByID(Convert.ToInt32(obj));
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        getOutStorage();
    }
    public void getOutStorage()
    {
        if (this.TextBox2.Text != "" && this.TextBox3.Text != "")
        {
            //起始
            DateTime fromTime = DateTime.Parse(this.TextBox2.Text);
            //终止
            DateTime toTime = DateTime.Parse(this.TextBox3.Text);
            //国家
            int moneyType = System.Convert.ToInt32(this.DropDownList1.SelectedValue);
            //仓库
            int warehouseID = System.Convert.ToInt32(this.ddlWareHouse.SelectedValue);
            //库位
            int DepotSeatID = System.Convert.ToInt32(this.ddlDepotSeat.SelectedValue);
            DataTable dt = QueryOutStorageBLL.outStoreOrder(fromTime, toTime, moneyType, warehouseID, DepotSeatID);
            ViewState["warehouseID"] = warehouseID;
            ViewState["DepotSeatID"] = DepotSeatID;
            ViewState["dt"] = dt;

            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
            this.btn.Visible = true;
            Translations();
        }
        else
        {
            Response.Write(Transforms.ReturnAlert(GetTran("000741", "请选择日期")));
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Country1_SelectedIndexChanged(sender, e);
        Session["Default_Country"] = DropDownList1.SelectedItem.Text;
        Button1_Click(null, null);
    }
    public string GetcurrencyName(string productid)
    {
        string CurrencyName = StorageInBrowseBLL.GetCurrencyName(productid);
        return CurrencyName;
    }
    #region 得到产品编码
    /// <summary>
    /// 得到产品名称
    /// </summary>
    /// <returns>得到产品名称</returns>
    public string GetProductNameCode(string productId)
    {
        string ProductNameCode = CommonDataBLL.GetProductNameCode(productId);
        return ProductNameCode;
    }
    #endregion
    public string GetProductNameByID(string productId)
    {
        string ProductName = CommonDataBLL.GetProductNameByID(productId);
        return ProductName;
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["dt"];
        if (dt == null || dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000672", "没有数据，不能导出Excel") + "！')</script>");
            return;
        }

        dt.Columns.Add("ProductName", typeof(System.String));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["ProductName"] = GetProName(dt.Rows[i]["ProductID"].ToString());
            dt.Rows[i]["ProductID"] = GetProductNameCode(dt.Rows[i]["ProductID"].ToString());
        }

        Excel.OutToExcel(dt, GetTran("000745", "出库查询"), new string[] { "ProductID=" + GetTran("000263", "产品编码"), "ProductName=" + GetTran("000501", "产品名称"),"Oquantity=" + GetTran("000748", "出库产品总数"), "TotalOMoney=" + GetTran("000751", "出库产品总金额"), "Tquantity="+GetTran("000753", "退货产品总数"), "TotalTuiMoney=" + GetTran("000757", "退货产品总金额"), 
            "TotalQuantity=" + GetTran("000630", "合计"), "TotalMoney=" + GetTran("000762", "合计总金额") });
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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
        Translations();
    }

    private void DataBindWareHouse()
    {
        ///通过管理员编号获取仓库相应的权限
        DataTable dt = StorageInBLL.GetMoreManagerPermissionByNumber(Session["Company"].ToString());
        DataView dataev = new DataView(dt);
        dataev.RowFilter = "[CountryCode]=" + DropDownList1.SelectedValue;
        ddlWareHouse.DataSource = dataev;
        ddlWareHouse.DataTextField = "WareHouseName";
        ddlWareHouse.DataValueField = "WareHouseID";
        //ddlWareHouse.DataSource = dt;
        ddlWareHouse.DataBind();
        Translations();
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
        Translations();
    }
    protected void ddlWareHouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBindDepotSeat();
        getOutStorage();
    }
    protected void ddlDepotSeat_SelectedIndexChanged(object sender, EventArgs e)
    {
        getOutStorage();
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        Button1_Click(null, null);
    }
    protected void Back_Click(object sender, EventArgs e)
    {
        this.Panel1.Visible = true;
        this.Panel2.Visible = false;
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
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations2();
        }
    }
    public string getSubRemark(object remark)
    {
        return remark.ToString().Length > 12 ? ("<a title='" + remark.ToString() + "' style='cursor:hand'>" + remark.ToString().Substring(0, 12) + "..." + "</a>") : remark.ToString();
    }
    public string getCurrencyName(string productid)
    {
        string CurrencyName = StorageInBrowseBLL.GetCurrencyName(productid);
        return CurrencyName;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "LinkbtnXinxi")
        {
            int docTpyeId = Convert.ToInt32(QueryInStorageBLL.GetDocTypeIDEByDocTypeCode("DB"));//调拨单
            int docTpyeId1 = Convert.ToInt32(QueryInStorageBLL.GetDocTypeIDEByDocTypeCode("CK"));//出库单
            int docTpyeId2 = Convert.ToInt32(QueryInStorageBLL.GetDocTypeIDEByDocTypeCode("BS"));//报损单
            string sqlSQL_WareHouseID = "";
            if (ViewState["warehouseID"] != null && ViewState["DepotSeatID"] != null)
            {
                sqlSQL_WareHouseID = " and M.inwarehouseID=" + ViewState["warehouseID"].ToString() + " and M.inDepotSeatID=" + ViewState["DepotSeatID"].ToString();
            }

            this.Panel1.Visible = false;
            this.Panel2.Visible = true;
            this.btn.Visible = false;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT D.ProductID, P.ProductName, D.ProductQuantity, D.UnitPrice,P.PreferentialPrice,sum(P.PreferentialPrice*D.ProductQuantity) as ProductTotal1,");
            sb.Append("D.ProductTotal, M.DocAuditTime, M.Provider, M.DocAuditer,");
            sb.Append("M.Note, M.BatchCode,U.ProductUnitName as UnitName ");
            sb.Append("FROM InventoryDocDetails D INNER JOIN ");
            sb.Append("Product P ON D.ProductID = P.ProductID INNER JOIN ");
            sb.Append("InventoryDoc M ON D.DocID = M.DocID " + sqlSQL_WareHouseID);
            sb.Append(" left outer join	ProductUnit U on P.SmallProductunitID =  U.ProductUnitID ");
            sb.Append(" WHERE P.Countrycode=" + System.Convert.ToInt32(this.DropDownList1.SelectedValue) + " and  (M.DocTypeID =" + docTpyeId + " or M.DocTypeID=" + docTpyeId1 + " or M.DocTypeID=" + docTpyeId2 + ") And  StateFlag='1' And CloseFlag='0' And DATEADD(day, DATEDIFF(day,0,M.DocAuditTime ), 0) between '" + this.TextBox2.Text + "' and  '" + this.TextBox3.Text + "'");
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string PrID = ((HtmlInputHidden)row.FindControl("hidPrID")).Value;
            ViewState["PrID"] = PrID;
            sb.Append(" and D.ProductID=" + PrID+ " group by D.ProductID, P.ProductName,D.DocDetailsID, D.ProductQuantity,M.DocTypeID, D.UnitPrice,P.PreferentialPrice,D.ProductTotal, M.DocAuditTime, M.Provider,M.DocAuditer, M.Note, M.BatchCode, U.ProductUnitName  ");

            string table = "InventoryDocDetails as D, Product as P,InventoryDoc as M,ProductUnit as U";
            StringBuilder sb3 = new StringBuilder();
            sb3.Append("D.ProductID = P.ProductID and P.SmallProductunitID =  U.ProductUnitID and D.DocID = M.DocID and M.inWareHouseID=" + ViewState["warehouseID"].ToString() + " and M.inDepotSeatID=" + ViewState["DepotSeatID"].ToString());
            sb3.Append(" and P.Countrycode=" + System.Convert.ToInt32(this.DropDownList1.SelectedValue) + " and  (M.DocTypeID =" + docTpyeId + " or M.DocTypeID=" + docTpyeId1 + "or M.DocTypeID=" + docTpyeId2 + ") And  StateFlag='1' And CloseFlag='0' And DATEADD(day, DATEDIFF(day,0,M.DocAuditTime ), 0) between '" + this.TextBox2.Text + "' and  '" + this.TextBox3.Text + "'" + " and D.ProductID=" + PrID+ " group by D.ProductID, P.ProductName, D.ProductQuantity,M.DocTypeID, D.UnitPrice,P.PreferentialPrice,D.ProductTotal, M.DocAuditTime, M.Provider,M.DocAuditer,D.DocDetailsID, M.Note, M.BatchCode, U.ProductUnitName");
            string key = "D.DocDetailsID";
            string cloumns = " D.ProductID, P.ProductName, D.ProductQuantity, D.UnitPrice,D.ProductTotal,P.PreferentialPrice,sum(P.PreferentialPrice*D.ProductQuantity) as ProductTotal1, M.DocAuditTime, M.Provider, M.DocAuditer,M.Note, M.BatchCode,U.ProductUnitName as UnitName, case M.DocTypeID when 2 then '" + GetTran("000769", "出库单") + "' when 6 then '" + GetTran("000768", "报损单") + "' when 9 then '" + GetTran("000581", "调拨单") + "' else '' end as DocType ";
            Pager pager2 = Page.FindControl("Pager1") as Pager;
            pager2.Pageindex = 0;
            pager2.PageSize = 10;
            pager2.PageTable = table;
            pager2.Condition = sb3.ToString();
            pager2.PageColumn = cloumns;
            pager2.ControlName = "GridView2";
            pager2.key = key;
            pager2.PageCount = 0;
            pager2.PageBind();

            ViewState["dt1"] = sb.ToString();
            Translations();
        }
        Translations2();
    }

    /// <summary>
    /// 国家与仓库联动
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Country1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataView dataev = new DataView(BillOutOrderBLL.GetWareHouseName());

        dataev.RowFilter = "[CountryCode]=" + Convert.ToInt32(this.DropDownList1.SelectedValue);
        ddlWareHouse.DataSource = dataev;
        ddlWareHouse.DataTextField = "WareHouseName";
        ddlWareHouse.DataValueField = "WareHouseID";
        ddlWareHouse.DataBind();
        Translations();
        if (string.IsNullOrEmpty(ddlWareHouse.SelectedValue))
        {
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