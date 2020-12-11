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
using BLL;
using Model.Other;

public partial class Company_RefundmentOrderDetails : BLL.TranslationBase
{
    ReturnedGoodsBLL returnedGoodsBLL = new ReturnedGoodsBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        //检查相应权限

        //Response.Cache.SetExpires(DateTime.Now);
       // Permissions.CheckManagePermission(EnumCompanyPermission.LogisticsRefundmentOrderBrowse);
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (Request.QueryString["ID"] == null)
        {
            Response.Write(GetTran("002097","错误参数"));
            Response.End();
        }
        if (!IsPostBack)
        {
            ViewState["url"] = Request.UrlReferrer.ToString();
            BindGridViewList();

            DataTable dt = ReturnedGoodsBLL.GetProById(Request.QueryString["ID"].ToString());

            gvRefundmentBrowse.DataSource = dt;
            gvRefundmentBrowse.DataBind();

        }
        Translations();
    }

    protected object GetOrderDate(object obj)
    {
        try
        {
            obj = Convert.ToDateTime(obj).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours());
        }
        catch { }
        return obj;
    }


    protected string GetWareHouseNameByID(string strwarehouseId)
    {
        return returnedGoodsBLL.GetWareHouseNameByID(strwarehouseId);
    }

    private void Translations()
    {
        this.TranControls(this.btnhistory, new string[][] { new string[] { "000096", "返 回" } });
        this.TranControls(this.gvRefundmentDetail, new string[][] 
        { 
            new string[] { "002088", "退货编号" } ,
            new string[]{ "000501", "产品名称" } ,
            new string[]{ "001982", "退货数量" } ,
            new string[]{ "000045", "期数" },
            new string[]{ "002084", "价格" } ,
            //new string[]{ "000562", "币种" } ,
            new string[]{ "000414", "积分" } 
        });

        this.TranControls(this.gvRefundmentBrowse, new string[][] { 
                
                new string[] { "001808", "退货店铺" }, 
                new string[] { "001809", "退货单号" }, 
                new string[] { "000045", "期数" }, 
                new string[] { "000605", "是否审核" },
                new string[] { "001811", "是否失效" },
                new string[] { "001812", "退货总价" },
                new string[] { "001813", "退货总积分" },
                new string[] { "001814", "退货日期" }
        });
    }

    private void BindGridViewList()
    {
        this.gvRefundmentDetail.DataSource = returnedGoodsBLL.GetProductsByDocId(Request.QueryString["ID"].ToString());
        this.gvRefundmentDetail.DataBind();
      
    }

    protected void bunExportExcel_Click(object sender, EventArgs e)
    {
        gvRefundmentDetail.AllowPaging = false;
        gvRefundmentDetail.AllowSorting = false;

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "GB2312";
        Response.AppendHeader("Content-Disposition", "attachment;filename=RefundmentDetial.xls");
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文

        Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
        this.EnableViewState = false;
        System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

        gvRefundmentDetail.RenderControl(oHtmlTextWriter);

        Response.Write(oStringWriter.ToString());
        Response.End();

        gvRefundmentDetail.AllowPaging = true;
        gvRefundmentDetail.AllowSorting = true;
    }

//必须得写此个方法，要不然会引发runat=server 的窗体标记内... 异常。

    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }

    protected void gvRefundmentBrowse_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");

       

        }
       
    }


    protected void btnhistory_Click(object sender, EventArgs e)
    {
        if (ViewState["url"] != null)
        {
            Response.Redirect(ViewState["url"].ToString(),true);
        }
    }
}
