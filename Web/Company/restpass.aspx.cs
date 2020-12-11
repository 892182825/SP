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
using BLL.other.Company;
using BLL.CommonClass;
using Model.Other;
using Model;
using System.Text;
using System.Collections.Generic;
using BLL;
using DAL;
using Standard.Classes;
using System.Data.SqlClient;
using System.Web.Mail;
using jmail;
using Encryption;

public partial class Company_restpass : BLL.TranslationBase
{
    BLL.CommonClass.ChangeLogs cl_h_info;
    MailMessage objMailMessage;
    MailAttachment objMailAttachment;
    public string msg = "";
    MemPassResetBLL mb = new MemPassResetBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            string type = Request.QueryString["type"].ToString();//类型
            if (type == "3")
            {
                chkPass.Visible = false;
                RadioButtonList1.Items.Remove(new ListItem("产生一个随机密码通过手机发送给用户", "3"));
                RadioButtonList1.Items.Remove(new ListItem("产生一个随机密码通过邮件发送给用户", "2"));
                RadioButtonList1.Items[0].Selected = true;
                RadioButtonList1_SelectedIndexChanged(null, null);
            }
            else
                RadioButtonList1_SelectedIndexChanged(null, null);

        }
        Translations();

    }
    private void Translations()
    {
        this.chkPass.Items[0].Text = GetTran("006057", "一级密码");
        this.chkPass.Items[1].Text = GetTran("001538", "二级密码");
        if (Request.QueryString["type"] == "1")
        {
            this.TranControls(this.RadioButtonList1,
                    new string[][]{
                        new string []{"006689","重置为店铺编号"},
                    new string []{"006688","产生一个随机密码通过邮件发送给用户"},
                    new string []{"006687","产生一个随机密码通过手机发送给用户"},
                    new string []{"007080","手动填写密码"}});
        }
        else if (Request.QueryString["type"] == "2")
        {
            this.TranControls(this.RadioButtonList1,
                    new string[][]{
                    new string []{"006687","产生一个随机密码通过手机发送给用户"},
                    new string []{"006688","产生一个随机密码通过邮件发送给用户"},
                    new string []{"006968","重置为店铺编号"},
                    new string []{"007080","手动填写密码"}});
        }
        else
        {
            this.TranControls(this.RadioButtonList1,
                    new string[][]{
                    new string []{"007219","重置为管理员编号"},
                    new string []{"007080","手动填写密码"}});
        }

        this.TranControls(this.btn_ok, new string[][] { new string[] { "002190", "确 定" } });
    }

    protected string GetType()
    {
        string type = Request.QueryString["type"];
        return type;
    }
    protected void btn_ok_Click(object sender, EventArgs e)
    {
        string newpass = getpass();
        if (this.RadioButtonList1.SelectedValue == "4")
        {
            if (newpass.Length < 6 || newpass.Length > 10)
            {
                msg = "<script>alert('密码必须是6到10位！');</script>";
                return;
            }
        }
        //判断是否选择修改密码
        bool flag = false;
        foreach (ListItem lst in chkPass.Items)
        {
            if (lst.Selected)
            {
                flag = true;
                break;
            }
        }
        if (!flag)
        {
            msg = "<script>alert('请选择要修改的密码类型！');</script>";
            return;
        }
        int count = 0;
        string type = Request.QueryString["type"].ToString();//类型
        string id = "";
        if (GetType() != "3")
        {
            id = Request.QueryString["ID"].ToString();//店铺编号
        }


        string number = Request.QueryString["number"].ToString();//会员编号
        
        if (type.Length > 0 && number.Length > 0)
        {
            if (type == "1")
            {
                foreach (ListItem lst in chkPass.Items)
                {
                    if (lst.Selected)
                    {
                        if (lst.Value == "one")
                        {
                            switch (this.RadioButtonList1.SelectedValue.ToString())
                            {
                                case "1": count = mb.updateMemberPass(number, 0); break;//手机
                                case "2": count = MemberInfoDAL.updateMemberPass1(number, newpass); break;//邮件
                                case "3": count = MemberInfoDAL.updateMemberPass1(number, newpass); break;//商店
                                case "4": count = MemberInfoDAL.updateMemberPass1(number, newpass); break;//手动
                            }
                        }
                        else
                        {
                            switch (this.RadioButtonList1.SelectedValue.ToString())
                            {
                                case "1": count = mb.updateMemberPass(number, 1); break;
                                case "2": count = MemberInfoDAL.updateMemberPass2(number, newpass); break;
                                case "3": count = MemberInfoDAL.updateMemberPass2(number, newpass); break;
                                case "4": count = MemberInfoDAL.updateMemberPass2(number, newpass); break;
                            }

                        }
                    }
                }
            }
            else if (type == "2")
            {
                foreach (ListItem lst in chkPass.Items)
                {
                    if (lst.Selected)
                    {
                        if (lst.Value == "one")
                        {
                            switch (this.RadioButtonList1.SelectedValue.ToString())
                            {
                                case "1": count = StoreInfoDAL.StorePassReset(id, newpass); break;//店铺编号
                                case "2": count = StoreInfoDAL.StorePassReset(id, newpass); break;//邮件
                                case "3": count = StoreInfoDAL.StorePassReset(id, id); break;//服务机构
                                case "4": count = StoreInfoDAL.StorePassReset(id, newpass); break;
                            }
                        }
                        else
                        {
                            switch (this.RadioButtonList1.SelectedValue.ToString())
                            {
                                case "1": count = StoreInfoDAL.StorePassReset1(id, newpass); break;
                                case "2": count = StoreInfoDAL.StorePassReset1(id, newpass); break;
                                case "3": count = StoreInfoDAL.StorePassReset1(id, id); break;
                                case "4": count = StoreInfoDAL.StorePassReset1(id, newpass); break;
                            }
                        }
                    }
                }
            }
            else if (type == "3")
            {
                if (RadioButtonList1.SelectedValue == "1")
                {
                    count = ManageDAL.UpdateManagePass(number, number);
                }
                else
                {
                    count = ManageDAL.UpdateManagePass(number, newpass);
                }
            }
            if (count > 0)
            {
                if (type == "1")
                {
                    cl_h_info = new BLL.CommonClass.ChangeLogs("memberinfo", "number");
                    cl_h_info.AddRecord(number);
                }
                else if (type == "2")
                {
                    cl_h_info = new BLL.CommonClass.ChangeLogs("storeinfo", "storeid");
                    cl_h_info.AddRecord(id);
                }
                else if (type == "3")
                {
                    cl_h_info = new BLL.CommonClass.ChangeLogs("Manage", "Number");
                    cl_h_info.AddRecord(number);
                }
                string mess = "";

                if (type == "1")
                {
                    foreach (ListItem lst in chkPass.Items)
                    {
                        if (lst.Selected)
                        {
                            if (lst.Value == "one")
                            {
                                mess = GetTran("006589", "您好，您的会员一级密码重置成功，密码为：") + newpass;
                            }
                            else
                            {
                                if (mess.Contains("您好"))
                                {
                                    mess += GetTran("000000", "，您的会员二级密码重置成功，密码为：") + newpass;
                                }
                                else
                                {
                                    mess = GetTran("006590", "您好，您的会员二级密码重置成功，密码为：") + newpass;
                                }
                            }
                        }
                    }

                }
                else if (type == "2")
                {
                    foreach (ListItem lst in chkPass.Items)
                    {
                        if (lst.Selected)
                        {
                            if (lst.Value == "one")
                            {
                                mess = GetTran("006680", "您好，您的店铺一级密码重置成功，密码为：") + newpass;
                            }
                            else
                            {
                                if (mess.Contains("您好"))
                                {
                                    mess += GetTran("000000", "，您的店铺二级密码重置成功，密码为：") + newpass;
                                }
                                else
                                {
                                    mess = GetTran("006588", "您好，您的店铺二级密码重置成功，密码为：") + newpass;
                                }
                            }
                        }
                    }
                }

                if (this.RadioButtonList1.SelectedValue.ToString() == "3")
                {
                    SqlTransaction tran = null;
                    SqlConnection con = DAL.DBHelper.SqlCon();
                    con.Open();
                    tran = con.BeginTransaction();
                    try
                    {
                        string outInfo = string.Empty;
                        if (type == "1" || type == "2")
                        {
                            BLL.MobileSMS.SendMsgTo(tran, number, "", this.txt_1.Text.ToString(), mess, out outInfo, Model.SMSCategory.sms_menberPassRest);
                        }
                        if (type == "3" || type == "4")
                        {
                            BLL.MobileSMS.SendMsgTo(tran, number, "", this.txt_1.Text.ToString(), mess, out outInfo, Model.SMSCategory.sms_storePassRest);
                        }
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                    }
                    finally
                    {
                        con.Close();
                        con.Dispose();
                    }
                }
                else if (this.RadioButtonList1.SelectedValue.ToString() == "2")
                {
                    string title = "";
                    title = type == "1" ? "会员密码重置" : "服务机构密码重置";
                    if (send(mess, title))
                    {
                        msg = "<script language='javascript'>alert('" + GetTran("006681", "重置成功,请到邮箱查阅密码！") + "');this.close();</script>";
                        return;
                    }
                }
                else
                {
                    if (type == "1")
                    {
                        PublicClass.SendMsg(3, number, mess);
                    }
                    else if (type == "2")
                    {
                        PublicClass.SendMsg(4, number, mess);
                    }
                }

                if (type == "1" || type == "2")
                {
                    cl_h_info.AddRecord(id);
                    cl_h_info.ModifiedIntoLogs(ChangeCategory.company5, id, ENUM_USERTYPE.objecttype6);
                }
                else if (type == "3" || type == "4")
                {
                    cl_h_info.AddRecord(number);
                    cl_h_info.ModifiedIntoLogs(ChangeCategory.company1, number, ENUM_USERTYPE.objecttype6);
                }
                msg = "<script language='javascript'>alert('" + GetTran("000506", "重置成功！") + "');this.close();</script>";
            }
            else
            {
                msg = "<script language='javascript'>alert('" + GetTran("000507", "重置失败！") + "');this.close();</script>";
            }
        }
    }

    //获得随即密码
    public string getpass()
    {
        string sv = RadioButtonList1.SelectedValue;
        string pass = "";
        if (sv != "4")
        {
            Random r = new Random();
            int x = r.Next(999999);
            if (x >= 100000)
            {
                pass = x.ToString();
            }
        }
        else
        {
            pass = this.txt_1.Text.Trim();
        }

        return pass;
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string number = string.Empty;
        number = Request.QueryString["number"].ToString();
        if (Request.QueryString["type"] == "3")
        {
            ManageModel model = ManagerBLL.GetManage(number);
            lit_name.Text = new TranslationBase().GetTran("001066", "管理员编号") + "：" + number;
            lit_number.Text = new TranslationBase().GetTran("001067", "管理员名称") + "：" +  model.Name;
            if (this.RadioButtonList1.SelectedValue.ToString() == "1")
            {
                this.lab1.Visible = false;
                this.txt_1.Style.Add("display", "none");
                this.lab1.Text = "";
            }
            if (this.RadioButtonList1.SelectedValue.ToString() == "4")
            {
                this.lab1.Visible = true;
                this.txt_1.Style.Add("display", "");
                this.txt_1.Text = "";
                this.lab1.Text = "请输入密码：";

            }
        }
        else if (Request.QueryString["type"] == "2")
        {
            StoreInfoModel model = StoreInfoEditBLL.GetStoreInfoByStoreId(number);
            lit_number.Text = new TranslationBase().GetTran("000037", "服务机构编号") + "：" + number;
            lit_name.Text = new TranslationBase().GetTran("000040", "服务机构名称") + "：" + Encryption.Encryption.GetDecipherName(model.StoreName);

            if (this.RadioButtonList1.SelectedValue.ToString() == "3")
            {
                this.txt_1.Text = Encryption.Encryption.GetDecipherTele(model.MobileTele);
                this.lab1.Visible = true;
                this.txt_1.Style.Add("display", "");
                this.lab1.Text = GetTran("006684", "手机号码：");
            }
            if (this.RadioButtonList1.SelectedValue.ToString() == "2")
            {
                this.txt_1.Text = model.Email;
                this.lab1.Visible = true;
                this.txt_1.Style.Add("display", "");
                this.lab1.Text = "E-Mail:";
            }
            if (this.RadioButtonList1.SelectedValue.ToString() == "1")
            {
                this.lab1.Visible = false;
                this.txt_1.Style.Add("display", "none");
                this.lab1.Text = "";
            }
            if (this.RadioButtonList1.SelectedValue.ToString() == "4")
            {
                this.lab1.Visible = true;
                this.txt_1.Style.Add("display", "");
                this.txt_1.Text = "";
                this.lab1.Text = "请输入密码：";
            }
        }
        else
        {
            MemberInfoModel model = MemInfoEditBLL.getMemberInfo(number);
            lit_number.Text = new TranslationBase().GetTran("000024", "会员编号") + "：" + number;
            lit_name.Text = new TranslationBase().GetTran("000025", "会员姓名") + "：" + Encryption.Encryption.GetDecipherName(model.Name);

            if (this.RadioButtonList1.SelectedValue.ToString() == "3")
            {
                this.txt_1.Text = Encryption.Encryption.GetDecipherTele(model.MobileTele);
                this.lab1.Visible = true;
                this.txt_1.Style.Add("display", "");
                this.lab1.Text = GetTran("006684", "手机号码：");
            }
            if (this.RadioButtonList1.SelectedValue.ToString() == "2")
            {
                this.txt_1.Text = model.Email;
                this.lab1.Visible = true;
                this.txt_1.Style.Add("display", "");
                this.lab1.Text = "E-Mail:";
            }
            if (this.RadioButtonList1.SelectedValue.ToString() == "1")
            {
                this.lab1.Visible = false;
                this.txt_1.Style.Add("display", "none");
                this.lab1.Text = "";
            }
            if (this.RadioButtonList1.SelectedValue.ToString() == "4")
            {
                this.lab1.Visible = true;
                this.txt_1.Style.Add("display", "");
                this.txt_1.Text = "";
                this.lab1.Text = "请输入密码：";
            }
        }
    }

    private bool SendMails(string fromMail, string toMail, string ccMail, string bccMail, string subject, string subbody)
    {
        try
        {
            jmail.Message Jmail = new jmail.Message();

            DateTime t = DateTime.Now;
            string Subject = subject;
            string body = subbody;
            string FromEmail = fromMail;
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
            Jmail.From = FromEmail;

            DataTable dt = BLL.CommonClass.CommonDataBLL.GetEmail();
            if (dt.Rows.Count == 0)
            {
                return false;
            }

            //发件人邮件用户名
            Jmail.MailServerUserName = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(dt.Rows[0]["UserName"].ToString(), new BLL.QuickPay.SecretSecurity().EmailKey);// "qc";
            //发件人邮件密码
            Jmail.MailServerPassWord = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(dt.Rows[0]["Password"].ToString(), new BLL.QuickPay.SecretSecurity().EmailKey);//"zx001";
            //设置邮件标题
            Jmail.Subject = Subject;
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
    protected bool send(string newpass, string title)
    {
        DataTable dt = BLL.CommonClass.CommonDataBLL.GetEmail();
        if (dt.Rows.Count == 0)
        {
            return false;
        }
        bool flag = SendMails(BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(dt.Rows[0]["Email"].ToString(), new BLL.QuickPay.SecretSecurity().EmailKey), this.txt_1.Text, "", "", title, newpass);
        return flag;
    }
}