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

public partial class Company_SetParams_SetPassResetMail : BLL.TranslationBase
{
    public string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, "../index.aspx");
        if (!IsPostBack)
        {
            GetBind();
        }
        Translation();
    }
    private void Translation()
    {
        this.TranControls(this.RegularExpressionValidator1, new string[][] { new string[] { "000467", "抱歉！请输入正确的Email地址" }, });
        this.TranControls(this.btnSubmit, new string[][] { new string[] { "000321", "提交" } });
    }

    private void GetBind()
    {
        DataTable dt = BLL.CommonClass.CommonDataBLL.GetEmail();
        if (dt.Rows.Count > 0)
        {
            this.txtEmail.Text = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(dt.Rows[0]["email"].ToString(), new BLL.QuickPay.SecretSecurity().EmailKey);
            this.txtUsername.Text = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(dt.Rows[0]["username"].ToString(), new BLL.QuickPay.SecretSecurity().EmailKey);
            this.txtPassword.Text = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(dt.Rows[0]["password"].ToString(), new BLL.QuickPay.SecretSecurity().EmailKey);
            this.txtEmailAddress.Text = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(dt.Rows[0]["emailAddress"].ToString(), new BLL.QuickPay.SecretSecurity().EmailKey);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!IsValid)
        {
            return;
        }
        if (this.txtEmailAddress.Text.Trim() == "" || txtEmail.Text.Trim() == "" || txtPassword.Text.Trim() == "" || txtUsername.Text.Trim() == "")
        {
            msg = "<script>alert('"+GetTran("007089","所有项都不能为空")+"！');</script>";
            return;
        }

        try
        {
            string email = BLL.QuickPay.SecretSecurity.Encrypt3Des24Bit(this.txtEmail.Text.Trim(), new BLL.QuickPay.SecretSecurity().EmailKey);
            string userName = BLL.QuickPay.SecretSecurity.Encrypt3Des24Bit(this.txtUsername.Text.Trim(), new BLL.QuickPay.SecretSecurity().EmailKey);
            string pwd = BLL.QuickPay.SecretSecurity.Encrypt3Des24Bit(this.txtPassword.Text.Trim(), new BLL.QuickPay.SecretSecurity().EmailKey);
            string emailAddress = BLL.QuickPay.SecretSecurity.Encrypt3Des24Bit(this.txtEmailAddress.Text.Trim(), new BLL.QuickPay.SecretSecurity().EmailKey);

            bool isSure = BLL.CommonClass.CommonDataBLL.SetResetEmail(email, userName, pwd, emailAddress);
            if (!isSure)
            {
                msg = "<script>alert('"+GetTran("006922","设置失败")+"');</script>";
                return;
            }
            msg = "<script>alert('" + GetTran("006921", "设置成功") + "');</script>";
        }
        catch
        {
            msg = "<script>alert('" + GetTran("006922", "设置失败") + "');</script>";
            return;
        }
    }
}
