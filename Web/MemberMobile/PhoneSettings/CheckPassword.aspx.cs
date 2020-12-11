using System;
using BLL.other.Store;
using DAL;
using Model;
public partial class MemberMobile_PhoneSettings_CheckPassword : BLL.TranslationBase
{
    protected string  type="", url = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        //Response.Cache.SetExpires(DateTime.Now);
        type = Request.QueryString["type"] == "" ? "" : Request.QueryString["type"];
        url = Request.QueryString["url"] == "" ? "" : Request.QueryString["url"];
        if (!IsPostBack)
        {
            Session["smscode"] = null;
            try
            {

                if (url != null && url != "")
                {
                    if (Session["member"] == null)
                    {
                        Response.Redirect("../index.aspx");
                    }
                }
                else
                {
                    if (Session["member"] == null)
                    {
                        Response.Redirect("../index.aspx");
                    }
                }
                string number = Session["member"].ToString();
                MemberInfoModel mb = MemberInfoDAL.getMemberInfo(number);
                MobileTele.Value = mb.MobileTele;
            }
            catch (Exception ex)
            {
                if (Session["member"] == null)
                {
                    Response.Redirect("../index.aspx");
                }
            }
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (Session["Member"] == null) 
        {
            Response.Redirect("../index.aspx");
        }
        else
        {
            if (Session["smscode"] == null || Session["smscode"]=="")
            {
                ScriptHelper.SetAlert(Page, "验证码不正确！");
                return;
            }
            string sms = Session["smscode"].ToString();
            if (yzm.Text != sms)
            {
                ScriptHelper.SetAlert(Page, "验证码不正确！");
                return;
            }
            var number = Session["Member"].ToString();
            string oldPass = Encryption.Encryption.GetEncryptionPwd(this.txtPassword.Text.ToString(), number);
            int n = PwdModifyBLL.check(number, oldPass, 1);
            if (n > 0)
            {
                if (type == "setpwd")
                {
                    Response.Redirect("../PhoneSettings/ChangePassword.aspx?type=" + type);
                    return;
                }
                else
                {
                    Response.Redirect("../PhoneSettings/" + url + ".aspx?res=success&&type=" + type);
                    return;
                }
            }
            else
            {
                ScriptHelper.SetAlert(Page, "二级密码不正确！");
                return;
            }
        }
    }
}