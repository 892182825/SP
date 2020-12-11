using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MemberMobile_POS : BLL.TranslationBase
{
    public static string PostUrl = ConfigurationManager.AppSettings["WebReference.Service.PostUrl"];
    protected void Page_Load(object sender, EventArgs e)
    {
        string account = "C48907006";//用户名是登录用户中心->验证码、通知短信->帐户及签名设置->APIID
        string password = "e944a4b289c517bc8166e88656c74255"; //密码是请登录用户中心->验证码、通知短信->帐户及签名设置->APIKEY
        string mobile = Request.QueryString["mobile"];
        Random rad = new Random();

        int mobile_code = rad.Next(100000, 999999);
        Session["smscode"] = mobile_code;
        //int mobile_code = Convert.ToInt32(Request.QueryString["yzm"]);
        string content = "您的验证码是：" + mobile_code + "。请不要把验证码泄露给其他人。";

        //Session["mobile"] = mobile;
        //Session["mobile_code"] = mobile_code;

        string postStrTpl = "account={0}&password={1}&mobile={2}&content={3}";

        UTF8Encoding encoding = new UTF8Encoding();
        byte[] postData = encoding.GetBytes(string.Format(postStrTpl, account, password, mobile, content));

        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(PostUrl);
        myRequest.Method = "POST";
        myRequest.ContentType = "application/x-www-form-urlencoded";
        myRequest.ContentLength = postData.Length;

        Stream newStream = myRequest.GetRequestStream();
        // Send the data.
        newStream.Write(postData, 0, postData.Length);
        newStream.Flush();
        newStream.Close();

        HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
        if (myResponse.StatusCode == HttpStatusCode.OK)
        {
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

            //Response.Write(reader.ReadToEnd());

            string res = reader.ReadToEnd();
            int len1 = res.IndexOf("</code>");
            int len2 = res.IndexOf("<code>");
            string code = res.Substring((len2 + 6), (len1 - len2 - 6));
            //Response.Write(code);

            int len3 = res.IndexOf("</msg>");
            int len4 = res.IndexOf("<msg>");
            string msg = res.Substring((len4 + 5), (len3 - len4 - 5));
            Response.Write(msg);

            Response.End();

        }
        else
        {
            //访问失败
        }
    }
}