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
using BLL;
using System.Collections.Generic;
using BLL.Registration_declarations;
using System.Data.SqlClient;
using BLL.CommonClass;
using Model.Other;
using BLL.Logistics;
using BLL.other.Company;
using DAL;

public partial class Company_StorageInTree : BLL.TranslationBase
{
    protected string Flag;
    protected string msg;
    private void Page_Load(object sender, System.EventArgs e)
    {
        ///设置GridView的样式

        Permissions.ComRedirect(Page, Permissions.redirUrl);
        gvProduct.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
        Flag = Request.Params["Flag"].ToString();
        if (!IsPostBack)
        {
            if (Flag == "WareHouse")
            {
                gvProduct.Columns[1].Visible = false;
                msg = GetTran("001943", "仓库产品明细");
                ViewState["type"] = 1;
                BindWareHouseProductDetails();
            }

            else if (Flag == "DepotSeat")
            {
                gvProduct.Columns[1].Visible = true;
                msg = GetTran("001944", "库位产品明细");
                ViewState["type"] = 2;
                BindDepotSeatProductDetails();
            }

            else
            {
                msg = GetTran("001945", "店铺产品明细");
                Bind2();
            }
        }
        Translations_More();
    }

    /// <summary>
    /// Translation
    /// </summary>
    protected void Translations_More()
    {
        TranControls(gvProduct, new string[][]
                        {
                            new string[]{"000355","仓库名称"},
                            new string[]{"000357","库位名称"},
                            new string[]{"000263","产品编码"},
                            new string[]{"000501","产品名称"},
                            new string[]{"001948","产品大单位"},
                            new string[]{"001949","产品小单位"},
                            new string[]{"000359","入库数量"},
                            new string[]{"000362","出库数量"},
                            new string[]{"001958","结库数量"},
                            new string[]{"000365","预警数量"}
                        }
                    );
    }

    DataTable dt;

    /// <summary>
    /// 绑定仓库产品明细
    /// </summary>
    private void BindWareHouseProductDetails()
    {
        dt = WareHouseProductDetailsBLL.GetMoreProductInfoByWareHouseID(Convert.ToInt32(Session["WareHouseID"]));
        this.lbl_storename.Visible = false;
        if (dt.Rows.Count < 1)
        {
            this.lbl_flag.Text = GetTran("001946", "没有相关数据");
        }
        else
        {
            this.lbl_flag.Text = GetTran("000355", "仓库名称") + ":" + dt.Rows[0][0].ToString();
        }
        this.lbl_title.Text = GetTran("000386", "仓库");

        //this.gvProduct.DataSource = dt;
        //this.gvProduct.DataBind();

        string table = " ProductQuantity a,Product b,WareHouse c ";
        string columus = " C.WareHouseName,a.ProductID,b.ProductCode,b.ProductName," +
                " (select d.ProductUnitName from ProductUnit as d where ProductUnitID=b.BigProductUnitID) as ProductBigUnitName," +
                " (select d.ProductUnitName from ProductUnit as d where ProductUnitID=b.SmallProductUnitID) as ProductSmallUnitName ," +
                " b.Weight," +
                " convert(nvarchar(20),b.Length)+'*'+Convert(nvarchar(20),b.Width)+'*'+Convert(nvarchar(20),b.High) as Cubage," +
                " sum(a.TotalIn) as TotalIn," +
                " sum(a.TotalOut) as TotalOut ," +
                " sum(a.TotalIn-a.TotalOut) as TotalEnd," +
                " sum(b.AlertnessCount)as AlertnessCount ";
        string where = " a.ProductID=b.ProductID and a.WareHouseID=c.WareHouseID and a.wareHouseID=" + Session["WareHouseID"];

        string group = " C.WareHouseName,a.ProductID,b.ProductCode,b.ProductName, b.BigProductUnitID,b.SmallProductUnitID, b.Weight,b.Length,b.Width,b.High";
        Pager1.PageBindGroup(0, 10, table, columus, where, " a.ProductID ", group, "gvProduct");
    }

    /// <summary>
    /// 绑定库位产品明细
    /// </summary>
    private void BindDepotSeatProductDetails()
    {
        dt = WareHouseProductDetailsBLL.GetMoreProductInfoByWareHouseIDDepotSeatID(Convert.ToInt32(Session["WareHouseID"]), Convert.ToInt32(Session["DepotSeatID"]));
        this.lbl_storename.Visible = false;
        if (dt.Rows.Count < 1)
        {
            this.lbl_flag.Text = GetTran("001946", "没有相关数据");
        }
        else
        {
            this.lbl_flag.Text = GetTran("000357", "库位名称") + ":" + dt.Rows[0][1].ToString();
        }
        this.lbl_title.Text = GetTran("000390", "库位");

        string table = " ProductQuantity a,Product b,WareHouse c ,DepotSeat j";
        string columus = " C.WareHouseName,j.SeatName,a.ProductID,b.ProductCode,b.ProductName," +
                " (select d.ProductUnitName from ProductUnit as d where ProductUnitID=b.BigProductUnitID) as ProductBigUnitName," +
                " (select d.ProductUnitName from ProductUnit as d where ProductUnitID=b.SmallProductUnitID) as ProductSmallUnitName ," +
                " b.Weight," +
                " convert(nvarchar(20),b.Length)+'*'+Convert(nvarchar(20),b.Width)+'*'+Convert(nvarchar(20),b.High) as Cubage," +
                " sum(a.TotalIn) as TotalIn," +
                " sum(a.TotalOut) as TotalOut ," +
                " sum(a.TotalIn-a.TotalOut) as TotalEnd," +
                " sum(b.AlertnessCount)as AlertnessCount ";
        string where = " a.ProductID=b.ProductID and a.WareHouseID=c.WareHouseID and c.WareHouseID=j.WareHouseID and a.DepotSeatID=j.DepotSeatID and a.DepotSeatID=" + Session["DepotSeatID"] + "  and a.wareHouseID=" + Session["WareHouseID"];

        string group = " C.WareHouseName,j.SeatName,a.ProductID,b.ProductCode,b.ProductName, b.BigProductUnitID,b.SmallProductUnitID, b.Weight,b.Length,b.Width,b.High";
        Pager1.PageBindGroup(0, 10, table, columus, where, " a.ProductID ", group, "gvProduct");
        //this.gvProduct.DataSource = dt;
        //this.gvProduct.DataBind();
    }

    private void Bind2()
    {
        SqlParameter[] param ={
									new SqlParameter("@Storeid",SqlDbType.VarChar)
							    };

        param[0].Value = Session["Condition"].ToString();

        DataTable dt2 = new DataTable();
        dt2 = DBHelper.ExecuteDataTable("D_Kucun_getData", param, CommandType.StoredProcedure);
        if (dt2.Rows.Count < 1)
        {
            this.lbl_flag.Text = GetTran("001946", "没有相关数据");

            return;
        }
        else
        {
            this.lbl_flag.Text = GetTran("000040", "店铺名称") + ":" + dt2.Rows[0][0].ToString();
        }
        this.lbl_title.Text = GetTran("001571", "店铺");
        this.lbl_storename.Text = GetTran("000039", "店长姓名") + ":" + dt2.Rows[0][1].ToString();
        this.lbl_storename.Visible = true;
        this.gvProduct.DataSource = dt2;
        this.gvProduct.DataBind();
    }

    /// <summary>
    /// 行绑定事件

    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ///控制GridView样式
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("WareHouseProductDetails.aspx");
    }

    protected void Butt_Excel_Click(object sender, ImageClickEventArgs e)
    {
        if (ViewState["type"].ToString() == "1")
        {
            dt = WareHouseProductDetailsBLL.GetMoreProductInfoByWareHouseID(Convert.ToInt32(Session["WareHouseID"]));
            Excel.OutToExcel(dt, GetTran("001943", "仓库产品明细"), new string[] { "WareHouseName=" + GetTran("000355", "仓库名称"), "ProductCode=" + GetTran("000263", "产品编码"), "ProductName=" + GetTran("000501", "产品名称"), "ProductBigUnitName=" + GetTran("001948", "产品大单位"), "ProductSmallUnitName=" + GetTran("001949", "产品小单位"), "TotalIn=" + GetTran("000359", "入库数量"), "TotalOut=" + GetTran("000362", "出库数量"), "totalend=" + GetTran("001958", "结库数量"), "AlertnessCount=" + GetTran("000365", "预警数量") });
        }
        else
        {
            dt = WareHouseProductDetailsBLL.GetMoreProductInfoByWareHouseIDDepotSeatID(Convert.ToInt32(Session["WareHouseID"]), Convert.ToInt32(Session["DepotSeatID"]));
            Excel.OutToExcel(dt, GetTran("001944", "库位产品明细"), new string[] { "WareHouseName=" + GetTran("000355", "仓库名称"), "SeatName=" + GetTran("000357", "库位名称"), "ProductCode=" + GetTran("000263", "产品编码"), "ProductName=" + GetTran("000501", "产品名称"), "ProductBigUnitName=" + GetTran("001948", "产品大单位"), "ProductSmallUnitName=" + GetTran("001949", "产品小单位"), "TotalIn=" + GetTran("000359", "入库数量"), "TotalOut=" + GetTran("000362", "出库数量"), "totalend=" + GetTran("001958", "结库数量"), "AlertnessCount=" + GetTran("000365", "预警数量") });
        }
    }
}