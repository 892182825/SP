using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.other.Store;
public partial class MemberMobile_PhoneSettings_ChangePassword : BLL.TranslationBase
{
    protected string type = "", url = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack) 
        {
            try
            {
                type = Request.QueryString["type"] == "" ? "" : Request.QueryString["type"];
                url = Request.QueryString["url"] == "" ? "" : Request.QueryString["url"];
            }
            catch (Exception ex)
            {
                Response.Redirect("../index.aspx");
            }
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        reset();
    }
    public void reset()
    {
        var passtype = 0;
        var str = "";
        if (inlineRadio1.Checked)
        {
            passtype = 0;
            str = "登录密码";
        }
        else
        {
            passtype = 1;
            str = "支付密码";
        }
        var member = Session["Member"];
        if (member != null)
        {
            var number = member.ToString();
            var NewPass = Encryption.Encryption.GetEncryptionPwd(this.newPassword.Text.ToString(), number);
            var oldPass = Encryption.Encryption.GetEncryptionPwd(this.oldPassword.Text.ToString(), number);
            int n = PwdModifyBLL.check(number, oldPass, passtype);

            if (n > 0)
            {
                BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("Memberinfo", "ltrim(rtrim(number))");
                cl_h_info.AddRecord(number);

                int i = 0;
                i = PwdModifyBLL.updateMemberPass(number, NewPass, passtype);
                if (i > 0)
                {
                    cl_h_info.AddRecord(number);
                    cl_h_info.ModifiedIntoLogs(BLL.CommonClass.ChangeCategory.member3, number, BLL.CommonClass.ENUM_USERTYPE.objecttype6);
                    Response.Redirect("SettingsIndex.aspx?res=success&&type=fanhui");
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + str + "修改成功" + "')</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + str + "修改失败！" + "')</script>");
                }
            }
            else 
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('原始密码不正确，请准确填写！')</script>");
                return;
            }
        }
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        this.oldPassword.Text = "";
        this.newPassword.Text = "";
        this.newPassword2.Text = "";
    }
}