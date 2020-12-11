using BLL.CommonClass;
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

public partial class Member_ShowOrderDetails : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        //检查店铺权限
        Response.Cache.SetExpires(DateTime.Now);
        // Permissions.CheckMemberPermission();//检测是否已登录

        if (!IsPostBack)
        {
            int bzCurrency = CommonDataBLL.GetStandard();
            if (Request.QueryString["byy"] != null)
            {

                ViewState["url"] = Request.UrlReferrer.ToString();
                string orderid = Request.QueryString["byy"];
                if (orderid.Length > 0)
                    BindData(orderid);

                BLL.Registration_declarations.MemberOrderBLL memberOrderBLL = new BLL.Registration_declarations.MemberOrderBLL();

                int currency = Convert.ToInt32(AjaxClass.GetCurrency(int.Parse(Session["Default_Currency"] == null ? bzCurrency.ToString() : Session["Default_Currency"].ToString()))); 
                DataTable dt = memberOrderBLL.GetMemberByOrderID(orderid,currency);

                gvorder.DataSource = dt;
                gvorder.DataBind();
            }
        }
        Transllations();
    }

    protected void gvorder_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");

            switch (e.Row.Cells[1].Text)
            {
                case "0": e.Row.Cells[1].Text = "<font color=red>" + GetTran("000521", "未支付") + "</font>"; break;
                case "1": e.Row.Cells[1].Text = GetTran("000517", "已支付"); break;
                default: e.Row.Cells[1].Text = ""; break;
            }
            e.Row.Cells[9].Text = Common.GetMemberOrderType(e.Row.Cells[9].Text);
           
        }
       

    }

    protected string getQR(string queren)
    {
          string qr = "";
          switch (queren)
          {

              case "0": qr = "<font color=red>" + GetTran("005634", "未收款") + "</font>"; break;
              case "1": qr = GetTran("005636", "已收款"); break;
              default: qr = ""; break;
          }

          return qr;
      }

    protected string getFXTJ(string fuxiaoname)
    {
        string fx = "";
        switch (fuxiaoname)
        {
            case "0": fx = GetTran("000555", "店铺注册"); break;
            case "1": fx = GetTran("001445", "店铺复消"); break;
            case "2": fx = GetTran("001448", "网上购物"); break;
            case "3": fx = GetTran("001449", "自由注册"); break;
            case "4": fx = GetTran("001452", "特殊注册"); break;
            case "5": fx = GetTran("001454", "特殊报单"); break;
            case "6": fx = GetTran("001455", "手机注册"); break;
            case "7": fx = GetTran("001457", "手机报单"); break;
            default: fx = ""; break;
        }

        return fx;
    }

    protected string getZF(string zhifu)
    {
        string zf = "";
        switch (zhifu)
        {
            case "0": zf = "<font color=red>" + GetTran("000521", "未支付") + "</font>"; break;
            case "1": zf = GetTran("000517", "已支付"); break;
            default: zf = ""; break;
        }
        return zf;
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

    private void Transllations()
    {
        this.TranControls(this.gvorder,
           new string[][] { 
     
                new string[] { "000045", "期数" } ,
                new string[] { "000775", "支付状态" } ,
                new string[] { "000079", "订单号" } ,
                new string[] { "000024", "会员编号" } ,
                new string[] { "000025", "会员姓名" } ,
                new string[] { "000322", "金额" } ,
                new string[] { "000414", "积分" } ,
                new string[] { "001429", "报单日期" } ,
                new string[] { "000793", "确认店铺" } ,
                new string[] { "000774", "报单途径" } ,
                new string[] { "006049", "店铺确认" } ,
                new string[] { "006048", "公司确认" } 
            });

        this.TranControls(this.myDatGrid,
            new string[][] { 
                new string[] { "000079", "订单号" } ,
                new string[] { "000150", "店铺编号" } ,
                new string[] { "000501", "产品名称" } ,
                new string[] { "000882", "产品型号" } ,
                new string[] { "000505", "数量" } ,
                new string[] { "000503", "单价" } ,
                new string[] { "000414", "积分" } ,
                new string[] { "000041", "总金额" } 

            });
        this.TranControls(this.btnE, new string[][] { new string[] { "000096", "返 回" } });
    }

    private void BindData(string orderid)
    {
        BLL.Registration_declarations.MemberOrderBLL memberOrderBLL = new BLL.Registration_declarations.MemberOrderBLL();
        myDatGrid.DataSource = memberOrderBLL.GetMemberDetailsByOrderID(orderid);
        myDatGrid.DataBind();

    }

    protected void myDatGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
    }

    protected void btnE_Click(object sender, EventArgs e)
    {
        if (ViewState["url"] == null)
        {

        }
        else
        {
            Response.Redirect(ViewState["url"].ToString(), true);
        }
    }
}
