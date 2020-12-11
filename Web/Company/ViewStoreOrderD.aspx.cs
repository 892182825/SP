
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
using System.Text;
using System.IO;
using DAL;
using BLL;

public partial class Company_ViewStoreOrderD : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            string storeOrderID = Request.QueryString["storeOrderID"].ToString();

            GridView1.DataSource = new OrdersBrowseBLL().StoreOrderDataTable_III(storeOrderID);
            GridView1.DataBind();

            DataTable dt = OrdersBrowseBLL.StoreOrderDT(storeOrderID);

            GridView_BillOutOrder.DataSource = dt;
            GridView_BillOutOrder.DataBind();


            this.TranControls(this.GridView_BillOutOrder,
                             new string[][]{	
                               													 
                                new string []{"000098","订货店铺"},
                                new string []{"000079","订单号"},	
                                new string []{"000045","期数"},
                                new string []{"000106","订单类型"},
                                new string []{"000041","总金额"},
                                new string []{"000113","总积分"},
                                new string []{"000100","付款否"},
                                new string []{"000383","收货人姓名"},
                                new string []{"000108","收货人国家"},
                                new string []{"000109","省份"},												 
                                new string []{"000110","城市"},
                                new string []{"000112","收货地址"},
                                new string []{"000646","电话"},
                                new string []{"000118","重量"},
                                new string []{"000120","运费"},
                                new string []{"000067","订货日期"}

                                });


            this.TranControls(this.GridView1,
                            new string[][]{														 
                                new string []{"000558","产品编号"},
                                new string []{"000501","产品名称"},	
                                new string []{"000505","数量"},
                                new string []{"000518","单位"},
                                new string []{"000045","期数"},
                                new string []{"000503","单价"},
                                new string []{"000414","积分"}
                                
                                        });
        }
    }

    public string StringFormat(string sf)
    {
        if (sf == "Y")
            return new TranslationBase().GetTran("000233");
        else
            return new TranslationBase().GetTran("000235");
    }

    public string SetTimeFormat(string timestr)
    {
        return timestr.Split(' ')[0];
    }

    public string SetFormatString(string str, int len)
    {
        if (str.Length >= len)
            return str.Substring(0, len) + "...";
        return str;
    }


    public string GetBiaoZhunTime(string dt)
    {
        if (Convert.ToDateTime(dt) < Convert.ToDateTime("1910-01-01 00:00:00"))
            return "——";

        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }

    public string GetOrderType(string str)
    {
        if (str == "1")
            return GetTran("000391", "周转款订货");
        else if (str == "2")
            return GetTran("000000", "被动订货");
        else if (str == "0")
            return GetTran("000402", "订货款订货");
        else
            return str;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xls");
        Response.ContentEncoding = Encoding.UTF7;

        StringWriter oStringWriter = new StringWriter();
        HtmlTextWriter oHtmlTextWriter = new HtmlTextWriter(oStringWriter);

        GridView1.RenderControl(oHtmlTextWriter);

        Response.Write(oStringWriter.ToString());
        Response.Flush();
        Response.End();
    }

    //必须加这个方法，要不然会引发：类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内... 异常。
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "xabc=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=xabc");
        }
    }

    protected void GridView_BillOutOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "xabc=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=xabc");




        }


    }
}
