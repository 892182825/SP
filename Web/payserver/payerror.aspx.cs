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

public partial class payerror : BLL.TranslationBase
{
    protected int bzCurrency = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Store"] != null)
        {
            Literal1.Text = "<a href='../Store/First.aspx'>我的首页</a><a href='../Store/auditingmemberorders.aspx'>注册确认</a><a href='../Store/auditingmemberagain.aspx'>复消确认</a><a href='../Store/CheckOutOrders.aspx'>订单支付</a><a href='../Store/ViewAccountCircs.aspx'>充值浏览</a><a href='../Logout.aspx'>退出系统</a>";
        }
        if (Session["Member"] != null)
        {
            Literal1.Text = "<a href='../Member/First.aspx'>我的首页</a><a href='../Member/AuditingMemberOrder.aspx'>报单支付</a><a href='../Member/ResultBrowse.aspx'>充值浏览</a><a href='../Logout.aspx'>退出系统</a>";
        }

        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        if (!IsPostBack)
        {
            double currency = AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()));
            if (Request.QueryString["ef"] != null)
            {
                string ef = EncryKey.Decrypt(Request.QueryString["ef"].ToUpper()).ToString();
                string typ = "";
                double money = 0;
                string bill = "";
                string[] sf = ef.Split(',');
                if (sf.Length >= 3)
                {
                    typ = sf[0].ToString();
                    bill = sf[1].ToString();
                    money = Convert.ToDouble(sf[2]);
                }
                else typ = ef;
                string showinfo = "";

                switch (typ)
                {
                    case "0":
                        //showinfo = GetTran("007512", "成功支付订单") + bill + " ，" + GetTran("000789", "支付金额") + "：" + double.Parse(money.ToString("0.00")) / currency + GetTran("000564", "元");
                        showinfo = GetTran("007512", "成功支付订单") + bill + " ，" + GetTran("000789", "支付金额") + "：" + double.Parse(money.ToString("0.00")) + GetTran("000564", "元");
                        break;
                    case "1":
                        showinfo = GetTran("007513", "支付失败,订单已支付或订单号不存在");
                        break;
                    case "2":
                        showinfo = GetTran("007514", "支付失败,账户可用余额不足");
                        break;
                    case "3":
                        showinfo = GetTran("007515", "支付失败,账户类型不存在");
                        break;
                    case "4":
                        showinfo = GetTran("007370", "未到账");
                        break;
                    case "5":
                        showinfo = GetTran("007516", "支付失败,服务机构周转款不足");
                        break;
                    case "6":
                        showinfo = GetTran("007517", "支付失败,服务机构订货款不足");
                        break;
                    case "100":
                        //showinfo = GetTran("007518", "请您务必在 24 小时内完成向指定账户汇款") + double.Parse(money.ToString("0.00")) / currency + GetTran("000564", "元") + "，" + GetTran("007519", "否则将视为您自动放弃汇款");
                        showinfo = GetTran("007518", "请您务必在 24 小时内完成向指定账户汇款") + double.Parse(money.ToString("0.00")) + GetTran("000564", "元") + "，" + GetTran("007519", "否则将视为您自动放弃汇款");
                        break;
                    case "101":
                        //showinfo = GetTran("007520", "请将") + bill + GetTran("007521", "订单款额") + double.Parse(money.ToString("0.00")) / currency + GetTran("000564", "元") + GetTran("007522", "交给您的购货店铺，并要求其为您支付订单");
                        showinfo = GetTran("007520", "请将") + bill + GetTran("007521", "订单款额") + double.Parse(money.ToString("0.00")) + GetTran("000564", "元") + GetTran("007522", "交给您的购货店铺，并要求其为您支付订单");
                        break;
                    default:
                        showinfo = GetTran("007523", "未知错误");
                        break;
                }

                lblinfo.Text = showinfo;
            }
        }
    }
}