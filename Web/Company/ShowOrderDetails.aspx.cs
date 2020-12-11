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

using BLL.Logistics;

public partial class Company_ShowOrderDetails : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            Bind();
        }
        Translations();
    }

    protected string GetCheckState(string obj)
    {
        if (obj != null)
        {
            if (obj.ToString() == "Y")
            {
                return "<font color='green'>" + GetTran("000517", "已支付") + "</font>  ";
            }
            else if (obj.ToString() == "N")
            {
                return "<font color='red'>" + GetTran("000521", "未支付") + "</font>  ";
            }
        }
        return "";
    }


    protected object GetType(object obj)
    {
        string str = obj.ToString();
        if (str == "1")
            return GetTran("000277", "周转款订货");
        else if (str == "2")
            return GetTran("007900", "被动订货");
        else if (str == "0")
            return GetTran("007529", "订货款订货");
        else
            return str;
    }


    //获取订货款方式
    protected object GetOrderType(object obj)
    {
        if (obj.ToString() == "0")
        {
            return GetTran("000391", "周转货款");
        }
        else if (obj.ToString() == "1")
        {
            return GetTran("000274", "现金余额");
        }
        else if (obj.ToString() == "2")
        {
            return GetTran("000968", "支付宝");
        }
        else if (obj.ToString() == "3")
        {
            return GetTran("000983", "快钱");
        }
        else
        {
            return "";
        }
    }


    protected string GetActiveFlag(string obj)
    {
        if (obj != null)
        {
            if (obj.ToString() == "0")
            {
                return GetTran("006000", "主动订货");
            }
            else if (obj.ToString() == "1")
            {
                return GetTran("006001", "自动订货");
            }
            else if (obj.ToString() == "2")
            {
                return GetTran("006002", "自动转主动");
            }
        }
        return "";
    }

    /// <summary>
    /// 翻译
    /// </summary>
    private void Translations()
    {
        this.TranControls(this.gvOrderDetail, new string[][] { 
                new string[] { "000895", "商品名称" }, 
                new string[] { "000898", "商品数量" }, 
                new string[] { "000900", "商品单价" }, 
                new string[] { "000902", "商品积分" }
        });

        this.TranControls(this.gvStoreOrder, new string[][] { 
              
                new string[] { "000079", "订单号" }, 
                new string[] { "000041", "总金额" }, 
                new string[] { "000113", "总积分" }, 
                new string[] { "000817", "购货期数" }, 
                new string[] {"000775","支付状态"},
                new string[] { "006975", "订货类型" }, 
                new string[] { "000186", "支付方式" }, 
                new string[]{"000106","订单类型"},
                new string[] { "000047", "国家" },
                new string[] { "000393", "收货人地址" },
                new string[] { "000383", "收货人" },
                new string[] { "000403", "收货人电话" }
                
        });
    }

    private string GetOrderId()
    {
        return Request.QueryString["orderId"].ToString();
    }

    private string GetStoreId()
    {
        return Request.QueryString["StoreID"].ToString();
    }

    private void Bind()
    {
        this.gvOrderDetail.DataSource = OrdersBrowseBLL.GetDetails(GetOrderId(), GetStoreId());
        this.gvOrderDetail.DataBind();

        DataTable dt = CheckOutOrdersBLL.GetOrderViewT(GetOrderId());
        gvStoreOrder.DataSource = dt;
        gvStoreOrder.DataBind();
    }
    protected void gvOrderDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg;");
        }
    }

    protected object GetActiveFlag(object obj)
    {
        if (obj != null)
        {
            if (obj.ToString() == "0")
            {
                return GetTran("006000", "主动订货");
            }
            else if (obj.ToString() == "1")
            {
                return GetTran("006001", "被动订货");
            }
            else if (obj.ToString() == "2")
            {
                return GetTran("006002", "被动转主动");
            }
        }
        return "";
    }

    protected object GetCheckState(object obj)
    {
        if (obj != null)
        {
            if (obj.ToString() == "Y")
            {
                return "<font color='green'>" + GetTran("000517", "已支付") + "</font>  ";
            }
            else if (obj.ToString() == "N")
            {
                return "<font color='red'>" + GetTran("000521", "未支付") + "</font>  ";
            }
        }
        return "";
    }

    protected void gvStoreOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg;");


        }

    }
}
