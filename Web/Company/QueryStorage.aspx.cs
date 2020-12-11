using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using Model;
using BLL.other.Company;
using BLL.CommonClass;
using DAL;
using System.Collections.Generic;
using BLL.MoneyFlows;
using Model.Other;
using BLL.Logistics;
public partial class Company_QueryStorage : BLL.TranslationBase 
{

    const string strkeyWord = "ILIKETHISGAME";
    string[] columns = { strkeyWord, strkeyWord, strkeyWord, strkeyWord, strkeyWord, strkeyWord, strkeyWord, strkeyWord, strkeyWord, strkeyWord, strkeyWord, "totalin", "totalout", "leftkucun", strkeyWord };
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.StorageKcglKcqk);

        GridView1.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
        if (!Common.WPermission(Session["Company"].ToString()))
        {
            vistable.Visible = false;
            lkbtnesellog.Visible = false;
            Response.Write(ReturnAlert("该管理员没有仓库权限！！"));
            return;
        }
        vistable.Visible = true;
        lkbtnesellog.Visible = true;
        if (!Page.IsPostBack)
        {
            //if (Session["Default_Country"] == null)
            //{
            //    Session["Default_Country"] = "";
            //}
            BindCountry_List();
            //CommonDataBLL.BindCountry_List(this.DropDownList1, Session["Default_Country"].ToString());//绑定货币
           
            DataBindWareHouse();
            DataBindDepotSeat();
            BindData();
            getgvElogin();
        }
        Translationsdd();
        Translations();
    }

    private void Translationsdd()
    {
        this.TranControls(this.DropDownListBM,
                new string[][]{
                    new string []{"000633","全部"},
                    new string []{"000263","产品编码"}});
    }

    private void Translations()
    {
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"000253","仓库编号"},
                    new string []{"000355","仓库名称"},
                    new string []{"000877","库位编号"},
                    new string []{"000357","库位名称"},
                    new string []{"000263","产品编码"},
                    new string []{"000501","产品名称"},
                    new string []{"000880","产品规格"},
                    new string []{"000882","产品型号"},
                    new string []{"000518","单位"},
                    new string []{"000885","会员价格"},
                    new string []{"000562","币种"},
                    new string []{"000359","入库数量"},
                    new string []{"000362","出库数量"},
                    new string []{"000888","余结库存"},
                    new string []{"000365","预警数量"}});
        this.TranControls(this.btn_search, new string[][] { new string[] { "000048", "查 询" } });
        
        this.TranControls(this.gvElogin,
                new string[][]{
                    new string []{"000501","产品名称"},
                    new string []{"001479","总入库"},
                    new string []{"001482","总出库"},
                    new string []{"001484","可用库存"}});
    }

    //绑定国家
    private void BindCountry_List()
    {
        IList<CountryModel> list = RemittancesBLL.BindCountry_List();
        this.DropDownList1.DataSource = list;
        this.DropDownList1.DataTextField = "Name";
        this.DropDownList1.DataValueField = "CountryCode";
        this.DropDownList1.DataBind();
        Translations();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        BindData();
    }
    private void BindData()
    {
        string Sql = "";
        string condition = "";

        if (this.ddlWareHouse.SelectedItem.Text.Equals("全部"))
        {
            if (this.DropDownListBM.SelectedValue == "全部")
            {
                Sql = "select q.DepotSeatID,q.WareHouseID,(select ProductSpecName from ProductSpec where ProductSpecID=p.ProductSpecID) as ProductSpecName,p.ProductCode,p.ProductTypeName ,p.ProductName as ProductName,p.PreferentialPrice as preferentialprice,q.TotalIn as TotalIn,q.TotalOut as TotalOut,(q.TotalIn-q.TotalOut) as leftKucun,p.AlertnessCount as AlertnessCount,U.ProductUnitName as UnitName,p.productid from ProductQuantity as q,Product as p , ProductUnit as U where p.SmallProductUnitID =  U.ProductUnitID and p.ProductID=q.ProductID and p.isFold=0 and p.Countrycode=" + System.Convert.ToInt32(this.DropDownList1.SelectedValue);
                condition = " p.SmallProductUnitID =  U.ProductUnitID and p.ProductID=q.ProductID and p.isFold=0 and p.Countrycode=" + System.Convert.ToInt32(this.DropDownList1.SelectedValue);
            }
            else
            {
                Sql = "select q.DepotSeatID,q.WareHouseID,(select ProductSpecName from ProductSpec where ProductSpecID=p.ProductSpecID) as ProductSpecName,p.ProductCode,p.ProductTypeName ,p.ProductName as ProductName,p.PreferentialPrice as preferentialprice,q.TotalIn as TotalIn,q.TotalOut as TotalOut,(q.TotalIn-q.TotalOut) as leftKucun,p.AlertnessCount as AlertnessCount,U.ProductUnitName as UnitName,p.Productid from ProductQuantity as q,Product as p , ProductUnit as U where p.SmallProductUnitID =  U.ProductUnitID and p.ProductID=q.ProductID and p.isFold=0 and p.Countrycode=" + System.Convert.ToInt32(this.DropDownList1.SelectedValue) + " and p.ProductCode='" + txtProductCode.Text.Trim() + "'";
                condition = " p.SmallProductUnitID =  U.ProductUnitID and p.ProductID=q.ProductID and p.isFold=0 and p.Countrycode=" + System.Convert.ToInt32(this.DropDownList1.SelectedValue) + " and p.ProductCode='" + txtProductCode.Text.Trim() + "'";
            }
        }
        else
        {
            if (this.DropDownListBM.SelectedValue == "全部")
            {
                Sql = "select q.DepotSeatID,q.WareHouseID,(select ProductSpecName from ProductSpec where ProductSpecID=p.ProductSpecID) as ProductSpecName,p.ProductCode,p.ProductTypeName ,p.ProductName as ProductName,p.PreferentialPrice as preferentialprice,q.TotalIn as TotalIn,q.TotalOut as TotalOut,(q.TotalIn-q.TotalOut) as leftKucun,p.AlertnessCount as AlertnessCount,U.ProductUnitName as UnitName,p.Productid from ProductQuantity as q,Product as p , ProductUnit as U where p.SmallProductUnitID =  U.ProductUnitID and p.ProductID=q.ProductID and p.isFold=0 and p.Countrycode=" + System.Convert.ToInt32(this.DropDownList1.SelectedValue) + " and q.WareHouseID=" + this.ddlWareHouse.SelectedValue + " and q.DepotSeatID=" + this.ddlDepotSeat.SelectedValue;
                condition = "  p.SmallProductUnitID =  U.ProductUnitID and p.productID=q.productID and p.isFold=0 and p.countrycode=" + System.Convert.ToInt32(this.DropDownList1.SelectedValue) + " and q.WareHouseID=" + this.ddlWareHouse.SelectedValue + " and q.DepotSeatID=" + this.ddlDepotSeat.SelectedValue;
            }
            else
            {
                Sql = "select q.DepotSeatID,q.WareHouseID,(select ProductSpecName from ProductSpec where ProductSpecID=p.ProductSpecID) as ProductCode,p.ProductTypeName ,p.ProductName as ProductName,p.preferentialprice as preferentialprice,q.TotalIn as TotalIn,q.TotalOut as TotalOut,(q.TotalIn-q.TotalOut) as leftKucun,p.AlertnessCount as AlertnessCount,U.ProductUnitName as UnitName,p.ProductID from ProductQuantity as q,Product as p , ProductUnit as U where p.SmallProductUnitID =  U.ProductUnitID and p.ProductID=q.ProductID and p.isFold=0 and p.Countrycode=" + System.Convert.ToInt32(this.DropDownList1.SelectedValue) + " and q.WareHouseID=" + this.ddlWareHouse.SelectedValue + " and q.DepotSeatID=" + this.ddlDepotSeat.SelectedValue + " and p.ProductCode='" + txtProductCode.Text.Trim() + "'";

                condition = "  p.SmallProductUnitID =  U.ProductUnitID and p.ProductID=q.ProductID and p.isFold=0 and p.Countrycode=" + System.Convert.ToInt32(this.DropDownList1.SelectedValue) + " and q.WareHouseID=" + this.ddlWareHouse.SelectedValue + " and q.DepotSeatID=" + this.ddlDepotSeat.SelectedValue + " and p.ProductCode='" + txtProductCode.Text.Trim() + "'";
            }
        }
        Sql += " order by q.id desc";
        string table = " ProductQuantity as q,Product as p,ProductUnit as U ";

        string key = "q.ID";
        string cloumns = "q.ID,q.DepotSeatID,q.WareHouseID,(select ProductSpecName from ProductSpec where ProductSpecID=p.ProductSpecID) as ProductSpecName,p.ProductCode,p.ProductTypeName ,p.ProductName as ProductName,p.preferentialprice as preferentialprice,q.TotalIn as TotalIn,q.TotalOut as TotalOut,(q.TotalIn-q.TotalOut) as leftKucun,p.AlertnessCount as AlertnessCount,U.ProductUnitName as UnitName,p.productid ";
        ViewState["sql"] = "select " + cloumns + "from " + table + "where 1=1 ";
        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.Pageindex = 0;
        pager.PageSize = 10;
        pager.PageTable = table;
        pager.Condition = condition;
        pager.PageColumn = cloumns;
        pager.ControlName = "GridView1";
        pager.key = key;
        pager.PageCount = 0;
         DataTable dtHa = QueryStorageBLL.getStorage(Sql);
        ViewState["dt"] = dtHa;
        GetTotalColumnHashtable(dtHa);
        pager.InitBindData = true;
        pager.PageBind();
        if (pager.PageCount > 0)
        {
            this.btnDownExcel.Visible = true;
        }
        else
        {
            this.btnDownExcel.Visible = false;
        }
        Translations();
    }
    private void GetTotalColumnHashtable(DataTable dt)
    {
        Hashtable htColumnTotal = new Hashtable();
        if (htColumnTotal == null)
            htColumnTotal = new Hashtable();

        htColumnTotal.Clear();

        for (int i = 0; i < columns.Length; i++)
        {
            if (columns[i].ToString().Trim() != strkeyWord)
                htColumnTotal.Add(columns[i], 0);
        }
        int columnIndex = 0;
        double totalValue = 0;
        string _columnName = string.Empty;
        foreach (DataColumn dc in dt.Columns)
        {
            _columnName = dc.ColumnName.ToLower();
            if (htColumnTotal.Contains(_columnName))
            {
                totalValue = 0;
                object oo = dt.Compute("Sum(" + _columnName + ")", "");
                totalValue = Convert.ToDouble(oo == DBNull.Value ? 0 : oo);
                htColumnTotal.Remove(_columnName);
                columnIndex = GetIndexOfArray(_columnName);
                if (columnIndex == -1)
                {
                    Response.Write(ReturnAlert(GetTran("000587", "现有列名与页面中的列不匹配")));
                    return;
                }
                htColumnTotal[columnIndex] = totalValue;
            }
        }
        ViewState["htColumnTotal"] = htColumnTotal;
    }
    public string ReturnAlert(string content)
    {
        string retVal;
        retVal = "<script language='javascript'>alert('" + content.Replace("'", " ").Replace("\r", " ").Replace("\n", "").Replace("\t", " ") + "');</script>";
        return retVal;
    }
    private int GetIndexOfArray(string content)
    {
        for (int i = 0; i < columns.Length; i++)
        {
            if (columns[i].Trim().ToLower() == content.Trim())
                return i;
        }
        return -1;
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Country1_SelectedIndexChanged(sender, e);
        btn_search_Click(null, null);
        Session["Default_Country"] = DropDownList1.SelectedItem.Text;
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["dt"];
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
           // dt.Rows[i]["ProductID"] = GetProductNameCode(dt.Rows[i]["ProductID"].ToString());
        }
        Excel.OutToExcel(dt, GetTran("000527", "发货跟踪表"), new string[] { "ProductCode="+ GetTran("000263", "产品编码"), "ProductName="+ GetTran("000501", "产品名称"), "ProductSpecName="+ GetTran("000880", "产品规格"), "ProductTypeName="+ GetTran("000882", "产品型号"), 
            "UnitName="+ GetTran("000518", "单位"), "preferentialprice="+ GetTran("000885", "会员价格"),"TotalIn="+ GetTran("000359", "入库数量"), "TotalOut="+ GetTran("000362", "出库数量"), "leftKucun="+ GetTran("000888", "余结库存"), "AlertnessCount="+ GetTran("000365", "预警数量") });
        //this.GridView1.AllowSorting = false;

        //DataTable dt = (DataTable)ViewState["dt"];
        //GetTotalColumnHashtable(dt);
        //this.GridView1.DataSource = dt.DefaultView;
        //this.GridView1.DataBind();

        //Response.Clear();
        //Response.Buffer = true;
        //Response.Charset = "GB2312";
        //Response.AppendHeader("Content-Disposition", "attachment;filename=HelloAdmin.xls");
        //Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-7");
        ////设置输出流为简体中文ss
        //Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
        //this.EnableViewState = false;
        //System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
        //System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
        //System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        //this.GridView1.RenderControl(oHtmlTextWriter);
        //Response.Write(oStringWriter.ToString());
        //Response.End();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    IDictionaryEnumerator myEnumerator = (ViewState["htColumnTotal"] as Hashtable).GetEnumerator();

        //    while (myEnumerator.MoveNext())
        //    {
        //        int sss = (int)myEnumerator.Key;
        //        e.Row.Cells[(int)myEnumerator.Key].Text = myEnumerator.Value.ToString();
        //    }

        //    e.Row.Cells[1].Text = "合计";
        //    e.Row.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;

        //    if (this.GridView1.PageIndex == this.GridView1.PageCount - 1)
        //    {
        //        this.GridView1.ShowFooter = true;
        //    }
        //    else
        //    {
        //        this.GridView1.ShowFooter = false;
        //    }
        //}
        //else
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations();
        }
    }
    /// <summary>
    /// 得到产品名称
    /// </summary>
    /// <returns>得到产品名称</returns>
    public string GetProductNameCode(string productId)
    {
        string flag = CommonDataBLL.GetProductNameCode(productId);
        return flag;
    }
    public string GetWarehouseName(string warehouseId)
    {
        return QueryStorageBLL.GetWarehouseName(warehouseId);
    }
    public static string GetDepotSeatName(string DepotSeatID)
    {
        string DepotSeatName = StorageInBrowseBLL.GetDepotSeatName(DepotSeatID);
        return DepotSeatName;
    }
    public string getCurrencyName(string productid)
    {
        string CurrencyName = "";
        if (productid != "" && productid != null)
        {
            CurrencyName = Convert.ToString(StorageInBrowseBLL.GetCurrencyName(productid));
        }
        return CurrencyName;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
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
        BindData();
    }
    protected void ddlDepotSeat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    /// <summary>
    /// 加号
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (this.p2.Visible == true)
        {
            this.p2.Visible = false;
            this.i1.Src = "images/dis2.GIF";
        }
        else
        {
            this.i1.Src = "images/dis3.GIF";
            p2.Visible = true;
        }
    }

    public void getgvElogin()
    {
        string sql = " LogicProductInventory.productid=product.productid ";
        this.Pager11.ControlName = "gvElogin";
        this.Pager11.key = "product.pid";
        this.Pager11.PageColumn = " LogicProductInventory.*,product.productname ,totalin-totalout as total";
        this.Pager11.Pageindex = 0;
        this.Pager11.PageTable = " LogicProductInventory,product ";
        this.Pager11.Condition = sql;
        this.Pager11.PageSize = 10;
        this.Pager11.PageCount = 0;
        this.Pager11.PageBind();
    }
    protected void gvElogin_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
        Translations();
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        btn_search_Click(null, null);
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
