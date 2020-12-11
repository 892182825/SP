using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DAL;
using System.Data.SqlClient;
using Model;
using BLL.other.Company;

/// <summary>
///SendEmail 的摘要说明
/// </summary>
public class SendEmail
{
    public SendEmail()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    public static bool SendMails(string toMail, string emailtitle, string emailcontext)
    {
        try
        {
            jmail.Message Jmail = new jmail.Message();

            DateTime t = DateTime.Now;
            string et = emailtitle;
            string body = emailcontext;
            
            string ToEmail = toMail;
            //Silent属性：如果设置为true,JMail不会抛出例外错误. JMail. Send( () 会根据操作结果返回true或false
            Jmail.Silent = true;
            //Jmail创建的日志，前提loging属性设置为true
            Jmail.Logging = true;
            //字符集，缺省为"US-ASCII"
            Jmail.Charset = "GB2312";
            //信件的contentype. 缺省是"text/plain"） : 字符串如果你以HTML格式发送邮件, 改为"text/html"即可。
            //Jmail.ContentType = "Multipart/Mixed";
            //添加收件人（若几个收件人就添加几行下面的代码）
            Jmail.AddRecipient(ToEmail, "", "");
            //Jmail.AddRecipientCC，Jmail.AddRecipientBCC （抄送，密送，用法同Jmail.AddRecipient）
           

            DataTable dt = BLL.CommonClass.CommonDataBLL.GetEmail();
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            string fromemail = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(dt.Rows[0]["Email"].ToString(), new BLL.QuickPay.SecretSecurity().EmailKey);// "qc";;
            Jmail.From = fromemail; 
            //发件人邮件用户名
            Jmail.MailServerUserName = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(dt.Rows[0]["UserName"].ToString(), new BLL.QuickPay.SecretSecurity().EmailKey);// "qc";
            //发件人邮件密码
            Jmail.MailServerPassWord = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(dt.Rows[0]["Password"].ToString(), new BLL.QuickPay.SecretSecurity().EmailKey);//"zx001";
            //设置邮件标题
            Jmail.Subject = et;
            //邮件添加附件,(多附件的话，可以再加一条Jmail.AddAttachment( "c:\\test.jpg",true,null);)就可以搞定了。

            ////［注］：加了附件，讲把上面的Jmail.ContentType="text/html";删掉。否则会在邮件里出现乱码。
            //Jmail.AddAttachment("c:\\img200610311000250.jpg", true, null);
            ////邮件内容,(若为纯文本就改为Jmail.Body )
            Jmail.Body = body;
            //Jmail发送的方法
            Jmail.Send(BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(dt.Rows[0]["EmailAddress"].ToString(), new BLL.QuickPay.SecretSecurity().EmailKey), false);
            Jmail.Close();
            return true;
        }
        catch
        {
            return false;
        }


    }

    //发送系统邮件
    public static bool SendSystemEmail(string sendnumber,string sendrole,string content,string recivenumber) {
        MessageSendModel messagesend = new MessageSendModel();

        //邮件保存
        messagesend.Content = content;
        messagesend.DropFlag = 0;
        messagesend.InfoTitle = "系统邮件";
        messagesend.LoginRole = "2";
        messagesend.ReadFlag = 0;
        messagesend.Sender = sendnumber;
        messagesend.SenderRole = "2";
        messagesend.LanguageCode = "L001";
        messagesend.CountryCode = "1";
        messagesend.MessageType = 'm';
        messagesend.Receive = recivenumber;
        messagesend.SetNoCondition();
 bool bb= MessageSendBLL.Addsendaffiche(messagesend);
 HttpContext.Current.Session["EmailCount"] = AjaxClass.GetNewEmailCount( );
         return bb;
    }



    /// <summary>
    /// 发送短信
    /// </summary>
    /// <param name="number"></param>
    /// <param name="mesage"></param>
    /// <returns></returns>
    public static bool SendSMS(string number,string mesage)
    {   
      bool b=false;
        string sjhm = ""; 
        string sjname = "";
        string hybianhao =number;
        DataTable dtmb = DBHelper.ExecuteDataTable("select top 1  number ,name ,mobiletele from memberinfo where number='"+number+"'");
        if (dtmb != null && dtmb.Rows.Count > 0)
        {
            sjhm = dtmb.Rows[0]["mobiletele"].ToString();
            sjname = dtmb.Rows[0]["name"].ToString();
            hybianhao = dtmb.Rows[0]["number"].ToString();  
        }
        if (sjhm != "" && mesage != "")
        {
            //    // 短信
            SqlConnection con = null;
            SqlTransaction tran = null;
            try
            {

                con = DAL.DBHelper.SqlCon();
                con.Open();
                tran = con.BeginTransaction();

                 b = BLL.MobileSMS.SendMsgMode(tran, sjname, mesage, number, sjhm, "", Model.SMSCategory.sms_Register);
                if (b)
                    tran.Commit();
                else
                    tran.Rollback();
            }
            catch (Exception ee)
            {
                if (tran != null)
                    tran.Rollback();
            }
            finally
            {
                con.Close();
            }
        }

        return b;
    }
}
