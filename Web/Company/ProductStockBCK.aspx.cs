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
using System.Data.SqlClient;
using Standard;

///Add Namespace
using BLL.other.Company;

/*
 * 创建者：  汪  华

 * 创建时间：2009-10-26
 * 完成时间：2009-10-26
 * 对应菜单：报表中心->仓库各产品明细 
 */

public partial class Company_ProductStock : BLL.TranslationBase
{
    protected string Flag;
    protected string msg;

    /// <summary>
    /// 为GridView排序声明变量和赋值

    /// </summary>
    private static bool blWareHouseName = true;
    private static bool blSeatName = true;
    private static bool blProductID = true;
    private static bool blProductCode = true;
    private static bool blProductName = true;
    private static bool blProductBigUnitName = true;
    private static bool blProductSmallUnitName = true;
    private static bool blProductColorName = true;
    private static bool blProductSpecName = true;
    private static bool blProductSexTypeName = true;
    private static bool blProductStatusName = true;
    private static bool blWeight = true;
    private static bool blCubage = true;
    private static bool blCostPrice = true;
    private static bool blCommonPrice = true;
    private static bool blCommonPV = true;
    private static bool blPreferentialPrice = true;
    private static bool blPreferentialPV = true;
    private static bool blTotalIn = true;
    private static bool blTotalOut = true;
    private static bool blTotalEnd = true;
    private static bool blAlertnessCount = true;

    private void Page_Load(object sender, System.EventArgs e)
    {
        ///设置GridView的样式
        //Response.Redirect("storageintree.aspx?Flag=WareHouse");
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
        TranControls(gvProduct,new string[][]
                        {
                            new string[]{"000355","仓库名称"},
                            new string[]{"000357","库位名称"},
                            new string[]{"000263","产品编码"},
                            new string[]{"000501","产品名称"},
                            new string[]{"001948","产品大单位"},
                            new string[]{"001949","产品小单位"},
                            //new string[]{"001950","产品颜色"},
                            //new string[]{"000880","产品规格"},
                            //new string[]{"001951","产品适用人群"},
                            //new string[]{"001952","产品状态"},
                            //new string[]{"001953","产品重量"},
                            //new string[]{"001954","产品体积"},
                            //new string[]{"001955","成本价"},
                            //new string[]{"001956","普通价"},
                            //new string[]{"001888","普通积分"},
                            //new string[]{"001957","优惠价"},
                            //new string[]{"001883","优惠积分"},
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
        string columus = " C.WareHouseName,a.ProductID,b.ProductCode,b.ProductName,"+
				" (select d.ProductUnitName from ProductUnit as d where ProductUnitID=b.BigProductUnitID) as ProductBigUnitName,"+
				" (select d.ProductUnitName from ProductUnit as d where ProductUnitID=b.SmallProductUnitID) as ProductSmallUnitName ,"+					
				" b.Weight,"+
				" convert(nvarchar(20),b.Length)+'*'+Convert(nvarchar(20),b.Width)+'*'+Convert(nvarchar(20),b.High) as Cubage,"+
				" sum(a.TotalIn) as TotalIn,"+
				" sum(a.TotalOut) as TotalOut ,"+
				" sum(a.TotalIn-a.TotalOut) as TotalEnd,"+
				" sum(b.AlertnessCount)as AlertnessCount ";
        string where = " a.ProductID=b.ProductID and a.WareHouseID=c.WareHouseID and a.wareHouseID=" + Session["WareHouseID"];
           
        string group =  " C.WareHouseName,a.ProductID,b.ProductCode,b.ProductName, b.BigProductUnitID,b.SmallProductUnitID, b.Weight,b.Length,b.Width,b.High";
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
            this.lbl_flag.Text = GetTran("001946","没有相关数据");
        }
        else
        {
            this.lbl_flag.Text = GetTran("000357","库位名称") + ":" + dt.Rows[0][1].ToString();
        }
        this.lbl_title.Text = GetTran("000390","库位");

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
            this.lbl_flag.Text = GetTran("001946","没有相关数据");

            return;
        }
        else
        {
            this.lbl_flag.Text = GetTran("000040","店铺名称") + ":" + dt2.Rows[0][0].ToString();
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

    ///// <summary>
    ///// GridView排序事件
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void gvProduct_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    DataView dv = new DataView((DataTable)ViewState["sortTable"]);
    //    string sortString = e.SortExpression;
    //    switch (sortString.ToLower().Trim())
    //    {
    //        case "warehousename":
    //            if (blWareHouseName)
    //            {
    //                dv.Sort = "WareHouseName desc";
    //                blWareHouseName = false;
    //            }
    //            else
    //            {
    //                dv.Sort = "WareHouseName asc";
    //                blWareHouseName = true;
    //            }
    //            break;

    //        case "seatname":
    //            if (blSeatName)
    //            {
    //                dv.Sort = "SeatName desc";
    //                blSeatName = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "SeatName asc";
    //                blSeatName = true;
    //            }
    //            break;

    //        case "productid":
    //            if (blProductID)
    //            {
    //                dv.Sort = "ProductID desc";
    //                blProductID = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "ProductID asc";
    //                blProductID = true;
    //            }
    //            break;

    //        case "productcode":
    //            if (blProductCode)
    //            {
    //                dv.Sort = "ProductCode desc";
    //                blProductCode = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "ProductCode asc";
    //                blProductCode = true;
    //            }
    //            break;

    //        case "productname":
    //            if (blProductName)
    //            {
    //                dv.Sort = "ProductName desc";
    //                blProductName = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "ProductName asc";
    //                blProductName = true;
    //            }
    //            break;

    //        case "productbigunitname":
    //            if (blProductBigUnitName)
    //            {
    //                dv.Sort = "ProductBigUnitName desc";
    //                blProductBigUnitName = false;
    //            }


    //            else
    //            {
    //                dv.Sort = "ProductBigUnitName asc";
    //                blProductBigUnitName = true;
    //            }
    //            break;

    //        case "productsmallunitname":
    //            if (blProductSmallUnitName)
    //            {
    //                dv.Sort = "ProductSmallUnitName desc";
    //                blProductSmallUnitName = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "ProductSmallUnitName asc";
    //                blProductSmallUnitName = true;
    //            }
    //            break;

    //        case "productcolorname":
    //            if (blProductColorName)
    //            {
    //                dv.Sort = "ProductColorName desc";
    //                blProductColorName = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "ProductColorName asc";
    //                blProductColorName = true;
    //            }
    //            break;

    //        case "productspecname":
    //            if (blProductSpecName)
    //            {
    //                dv.Sort = "ProductSpecName desc";
    //                blProductSpecName = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "ProductSpecName asc";
    //                blProductSpecName = true;
    //            }
    //            break;

    //        case "productsextypename":
    //            if (blProductSexTypeName)
    //            {
    //                dv.Sort = "ProductSexTypeName desc";
    //                blProductSexTypeName = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "ProductSexTypeName asc";
    //                blProductSexTypeName = true;
    //            }
    //            break;

    //        case "productstatusname":
    //            if (blProductStatusName)
    //            {
    //                dv.Sort = "ProductStatusName desc";
    //                blProductStatusName = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "ProductStatusName asc";
    //                blProductStatusName = true;
    //            }
    //            break;

    //        case "weight":
    //            if (blWeight)
    //            {
    //                dv.Sort = "Weight desc";
    //                blWeight = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "Weight asc";
    //                blWeight = true;
    //            }
    //            break;

    //        case "cubage":
    //            if (blCubage)
    //            {
    //                dv.Sort = "Cubage desc";
    //                blCubage = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "Cubage asc";
    //                blCubage = true;
    //            }
    //            break;

    //        case "costprice":
    //            if (blCostPrice)
    //            {
    //                dv.Sort = "CostPrice desc";
    //                blCostPrice = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "CostPrice asc";
    //                blCostPrice = true;
    //            }
    //            break;

    //        case "commonprice":
    //            if (blCommonPrice)
    //            {
    //                dv.Sort = "CommonPrice desc";
    //                blCommonPrice = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "CommonPrice asc";
    //                blCommonPrice = true;
    //            }
    //            break;

    //        case "commonpv":
    //            if (blCommonPV)
    //            {
    //                dv.Sort = "CommonPV desc";
    //                blCommonPV = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "CommonPV asc";
    //                blCommonPV = true;
    //            }
    //            break;

    //        case "preferentialprice":
    //            if (blPreferentialPrice)
    //            {
    //                dv.Sort = "PreferentialPrice desc";
    //                blPreferentialPrice = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "PreferentialPrice asc";
    //                blPreferentialPrice = true;
    //            }
    //            break;

    //        case "preferentialpv":
    //            if (blPreferentialPV)
    //            {
    //                dv.Sort = "PreferentialPV desc";
    //                blPreferentialPV = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "PreferentialPV asc";
    //                blPreferentialPV = true;
    //            }
    //            break;

    //        case "totalin":
    //            if (blTotalIn)
    //            {
    //                dv.Sort = "TotalIn desc";
    //                blTotalIn = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "TotalIn asc";
    //                blTotalIn = true;
    //            }
    //            break;

    //        case "totalout":
    //            if (blTotalOut)
    //            {
    //                dv.Sort = "TotalOut desc";
    //                blTotalOut = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "TotalOut asc";
    //                blTotalOut = true;
    //            }
    //            break;

    //        case "totalend":
    //            if (blTotalEnd)
    //            {
    //                dv.Sort = "TotalEnd desc";
    //                blTotalEnd = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "TotalEnd asc";
    //                blTotalEnd = true;
    //            }
    //            break;

    //        case "alertnesscount":
    //            if (blAlertnessCount)
    //            {
    //                dv.Sort = "AlertnessCount desc";
    //                blAlertnessCount = false;
    //            }

    //            else
    //            {
    //                dv.Sort = "AlertnessCount asc";
    //                blAlertnessCount = true;
    //            }
    //            break;
    //    }
    //    this.gvProduct.DataSource = dv;
    //    this.gvProduct.DataBind();
    //}
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