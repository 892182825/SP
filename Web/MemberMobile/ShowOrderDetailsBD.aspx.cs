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
using BLL.Registration_declarations;
using BLL.CommonClass;

public partial class Member_ShowOrderDetailsBD : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        //检查店铺权限
        Response.Cache.SetExpires(DateTime.Now);
        // Permissions.CheckMemberPermission();//检测是否已登录
        int bzCurrency = CommonDataBLL.GetStandard();
        if (!IsPostBack)
        {
            if (Request.QueryString["byy"] != null)
            {
                double currency = AjaxClass.GetCurrency(int.Parse(Session["Default_Currency"] == null ? bzCurrency.ToString() : Session["Default_Currency"].ToString()));
                ViewState["url"] = Request.UrlReferrer.ToString();
                string orderid = Request.QueryString["orderId"];
                if (orderid.Length > 0)
                    BindData(orderid);

                BLL.Registration_declarations.MemberOrderBLL memberOrderBLL = new BLL.Registration_declarations.MemberOrderBLL();
                DataTable dt = memberOrderBLL.GetMemberByOrderID1(orderid, currency);

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

            //switch (e.Row.Cells[1].Text)
            //{
            //    case "0": e.Row.Cells[1].Text = "<font color=red>" + GetTran("000521", "未支付") + "</font>"; break;
            //    case "1": e.Row.Cells[1].Text = GetTran("000517", "已支付"); break;
            //    default: e.Row.Cells[1].Text = ""; break;
            //}



            try
            {
                e.Row.Cells[11].Text = DateTime.Parse(e.Row.Cells[11].Text).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
            }
            catch
            {
            }

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

    public string GetRegisterWay(string rWay)
    {
        return new GroupRegisterBLL().GetOrderType(rWay);
    }

    public string GetRegisterDate(string rdate)
    {
        return Convert.ToDateTime(rdate).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();
    }

    public string GetPayStatus(string paytype)
    {
        string payStatus = new GroupRegisterBLL().GetDeftrayState(paytype);
        return payStatus;
    }

    public string GetDefrayName(string defrayType)
    {
        string defrayName = new GroupRegisterBLL().GetDefryType(defrayType);
        return defrayName;
    }

    public string GetNumberName(string name)
    {
        //解密姓名
        string namestr = Encryption.Encryption.GetDecipherName(name);
        namestr = namestr.Substring(0, 1) + "**";
        return namestr;
    }

    public string GetStore(string paytype)
    {
        string payStatus = "";
        switch (paytype)
        {
            case "0":
                payStatus = "<font color=red>" + GetTran("005634", "未收款") + "</font>";
                break;
            case "1":
                payStatus = GetTran("005636", "已收款");
                break;
            default:
                payStatus = "<font color=red>" + GetTran("005634", "未收款") + "</font>";
                break;
        }
        return payStatus;
    }

    private void Transllations()
    {
        this.TranControls(this.gvorder,
           new string[][] { 
     
                 new string[] { "000024", "会员编号" },
                 new string[] { "000025", "会员姓名" },
                 new string[] { "000030", "所属店铺" },
                 new string[] { "000774", "报单途径 " },
                 new string[] { "000775", "支付状态" },
                 new string[] { "000186", "支付方式" },
                 new string[] { "000045", "期数" },
                 new string[] { "000780", "审核期数" },
                 new string[] { "000079", "订单号" },
                 new string[] { "000322", "金额" },
                 new string[] { "000414", "积分" },
                 new string[] { "000057", "注册日期" }
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
