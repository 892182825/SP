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
using DAL;
using Model;
using BLL.CommonClass;
using System.Net;
using System.IO;
using System.Text;

public partial class Member_PhoneRecharge : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Translations();
        }
    }

    private void Translations()
    {
        this.TranControls(this.sub, new string[][] { new string[] { "000321", "提交" } });
    }

    protected void sub_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txtPhoneNumber.Text))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("008029", "请输入手机号码！") + "')</script>");
            hid_fangzhi.Value = "0";
            return;
        }
        else if (!PhoneRechargeBLL.CheckPhoneNumber(txtPhoneNumber.Text))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("006545", "手机号码格式错误") + "！')</script>");
            hid_fangzhi.Value = "0";
            return;
        }

        if (MemberInfoDAL.CheckState(Session["Member"].ToString()))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("007456", "会员账户已冻结，不能完成支付!") + "'); </script>");
            hid_fangzhi.Value = "0";
            return;
        }

        PhoneRecharge pr = new PhoneRecharge();
        pr.RechargeID = new PhoneRechargeBLL().GetRechargeID();
        pr.Number = Session["Member"].ToString();
        pr.AddMoney = Convert.ToDecimal(ddlMoney.SelectedValue);
        pr.AddState = 1;
        pr.PhoneNumber = txtPhoneNumber.Text;
        pr.AddTime = DateTime.Now.ToUniversalTime();
        pr.OperateIP = Request.UserHostAddress;
        pr.OperaterNum = Session["Member"].ToString();
        string result=PhoneRechargeBLL.AddRecharge(pr);

        if(String.Equals(result,"1"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("007514", "支付失败,账户可用余额不足") + "！'); </script>");
            hid_fangzhi.Value = "0";
            return;
        }
        else if(String.Equals(result,"fail"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("008031", "手机充值失败，请重新操作！") + "'); </script>");
            hid_fangzhi.Value = "0";
            return;
        }
        else if(String.Equals(result,"ok"))
        {
            string url = Request.Url.ToString().ToLower().Replace("/member/phonerecharge.aspx", "/phonerecharge/chongzhi.aspx");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/ag-plugin, application/xaml+xml, application/vnd.ms-xpsdocument, application/x-ms-xbap, application/x-ms-application, */*";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30)";

            request.Method = "post";
            request.KeepAlive = true;
            request.Headers.Add("Accept-Language", "zh-cn,zh;q=0.5");
            request.Headers.Add("Accept-Charset", "GB2312,utf-8;q=0.7,*;q=0.7");
            request.ContentType = "application/x-www-form-urlencoded";

            Encoding ec = Encoding.GetEncoding("gb2312");
            Byte[] bt = ec.GetBytes("txtPhoneNumber=" + pr.PhoneNumber + "&ddlMoney=" + pr.AddMoney.ToString() + "&RechargeID=" + pr.RechargeID);
            request.ContentLength = bt.Length;

            Stream streamrequest = request.GetRequestStream();
            streamrequest.Write(bt, 0, bt.Length);

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("008032", "手机充值操作完成，请等候5~10分钟话费将会充值到您的手机！") + "'); </script>");
            Response.Redirect(Request.Url.ToString().ToLower().Replace("/phonerecharge.aspx", "/findrecharge.aspx"));
        }
    }
}
