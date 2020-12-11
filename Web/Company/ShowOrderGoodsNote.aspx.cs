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
using BLL;

public partial class Company_ShowOrderGoodsNote : BLL.TranslationBase
{
    private string isButton = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            string storeOrderID = Request.QueryString["storeOrderID"].ToString();

            getbtCon(storeOrderID);

        }
        Translations();
    }
    protected void Translations()
    {
        this.TranControls(this.GridView_Order, new string[][]{
                                 new string[]{"000339","详细"},
                                 new string []{"000098","订货店铺"},
                                new string []{"000079","订单号"},	
                                new string []{"007356","要货单号"},

                                new string []{"000045","期数"},

                                new string []{"000100","付款否"},
                                new string []{"000106","订单类型"},

                                new string []{"000107","姓名"},
                                new string []{"000108","收货人国家"},
												 
                                new string []{"000109","省份"},												 
                                new string []{"000110","城市"},												 
                                new string []{"000112","收货地址"},

                                new string []{"000073","邮编"},
                                new string []{"000041","总金额"},
                                new string []{"000113","总积分"},
                                new string []{"000115","联系电话"},

                                new string []{"000118","重量"},
                                new string []{"000120","运费"},
                                new string []{"000067","订货日期"},
                                new string []{"000078","备注"},
        
        });
    


    
    }

    public string StringFormat(string sf)
    {
        if (sf == "Y")
            return new TranslationBase().GetTran("000233");
        else
            return new TranslationBase().GetTran("000235");
    }
    public string SetFormatString(string str, int len)
    {
        if (str.Length >= len)
            return str.Substring(0, len) + "...";
        return str;
    }
    public string GetName(string name)
    {
        return Encryption.Encryption.GetDecipherName(name);
    }

    public string GetBiaoZhunTime(string dt)
    {
        if (Convert.ToDateTime(dt) < Convert.ToDateTime("1910-01-01 00:00:00"))
            return "——";

        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }
   
    protected void getbtCon(string storeOrderID)
    {
        GridView_Order.DataSource =  BLL.Logistics.CheckOutOrdersBLL.GetOrderGoodsNoteTable(storeOrderID);
        GridView_Order.DataBind();

    }

    protected void GridView_Order_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "xabc=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=xabc");



        }


    }

    public string GetOrderType(string str)
    {
        if (str == "1")
            return GetTran("000391", "周转货");
        else if (str == "2")
            return GetTran("007900", "被动订货");
        else if (str == "0")
            return GetTran("007529", "订货款订货");
        else
            return str;
    }
    
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

        string storeOrderID = ((Label)GridView_Order.SelectedRow.FindControl("Lab_StoreOrderID")).Text;

        if (isButton == "sel")
        {
            Response.Redirect("OrderGoodsDetail.aspx?storeOrderID=" + storeOrderID);
        }

    }

    protected void lkb_Click(object sender, EventArgs e)
    {
        isButton = "sel";
    }
    protected void butt_Query_Click(object sender, EventArgs e)
    {
        Response.Redirect("BrowseStoreOrders.aspx");
    }
}
