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

public partial class Company_DisplaceGoodsDetails : BLL.TranslationBase
{
    DisplaceGoodBrowseBLL displaceGoodBrowseBLL = new DisplaceGoodBrowseBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        //检查相应权限
        //Response.Cache.SetExpires(DateTime.Now);
        //Permissions.CheckManagePermission(EnumCompanyPermission.LogisticsDisplaceGoodsBrowse);
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (Request.QueryString["ID"] == null)
        {
            Response.Write(GetTran("002097", "错误参数"));
            Response.End();
        }
        if (!Page.IsPostBack)
        {
            ViewState["url"] = Request.UrlReferrer.ToString();
            this.gvReplacementDetail.DataSource = displaceGoodBrowseBLL.GetReplacementDetail(Request.QueryString["ID"]);
            gvReplacementDetail.DataBind();


            DataTable dt = displaceGoodBrowseBLL.GetdisplaceReplace(Request.QueryString["ID"].ToString());

            gvDisplaceGoods.DataSource = dt;
            gvDisplaceGoods.DataBind();
        }
        Translations();
    }

    protected object GetYesOrNo(object obj)
    {
        if (obj == null)
        {
            return "";
        }
        else
        {
            if (obj.ToString() == "Y")
                return GetTran("000233", "是");
            else
                return GetTran("000235", "否");
        }
    }

    protected void gvDisplaceGoods_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           

            e.Row.Attributes.Add("onmouseover", "xx=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=xx;");

           
        }

       

    }

    private void Translations()
    {
        this.TranControls(this.gvDisplaceGoods,
               new string[][]{
                    
                    new string []{"001863","换货店铺"},
                    new string []{"001864","换货单号"},
                    new string []{"001866","对应退单号"},
                    new string []{"001867","对应订单号"},
                    new string []{"000099","对应出库单号"},
                    new string []{"000045","期数"},
                    new string []{"000605","是否审核"},
                    new string []{"001811","是否失效"},
                    new string []{"001875","退货额"},
                    new string []{"001876","进货额"},
                    new string []{"001878","换货单日期"}
                });

        this.TranControls(this.gvReplacementDetail,
                new string[][]{
                    new string []{"001864","换货单编号"},
                    new string []{"000501","产品名称"},
                    new string []{"001982","退货数量"},
                    new string []{"001985","进货数量"},
                    new string []{"002084","价格"},
                    new string []{"000562","币种"},
                    new string []{"000414","积分"}});
        this.TranControls(this.btnE, new string[][] { new string[] { "000096", "返 回" } });
    }

    protected void bunExportExcel_Click(object sender, EventArgs e)
    {
        gvReplacementDetail.AllowPaging = false;
        gvReplacementDetail.AllowSorting = false;

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "GB2312";
        Response.AppendHeader("Content-Disposition", "attachment;filename=RefundmentDetial.xls");
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");//设置输出流为简体中文
        Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
        this.EnableViewState = false;
        System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

        gvReplacementDetail.RenderControl(oHtmlTextWriter);

        Response.Write(oStringWriter.ToString());
        Response.End();

        gvReplacementDetail.AllowPaging = true;
        gvReplacementDetail.AllowSorting = true;
    }

    //必须得写此个方法，要不然会引发runat=server 的窗体标记内... 异常。
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }

    protected void btnE_Click(object sender, EventArgs e)
    {
        if (ViewState["url"] != null)
        {
            Response.Redirect(ViewState["url"].ToString(),true);
        }
    }
}
