using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL.other.Store;
using DAL;
using System.Data.SqlClient;
using Model;

public partial class Member_updatePass : BLL.TranslationBase
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        if (!IsPostBack)
        {
            try
            {
                string res = Request.QueryString["res"] == "" ? "" : Request.QueryString["res"];
                if (Request.UrlReferrer.ToString().IndexOf("/PassWordManage/CheckAdv1.aspx?type=member&url=updatePass")<0 || res != "success")
                {
                    Response.Redirect("../PassWordManage/CheckAdv1.aspx?type=member&url=updatePass");
                }
                else
                {
                    table2.Visible = true;
                }
            }
            catch {
                Response.Redirect("../PassWordManage/CheckAdv1.aspx?type=member&url=updatePass");
            }
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.passtype,
                new string[][]{
                    new string []{"006057","一级密码"},
                    new string []{"006056","二级密码"}
                });
        this.TranControls(this.btn_submit, new string[][] { new string[] { "000434", "确定" } });
        this.TranControls(this.Button1, new string[][] { new string[] { "001614", "取消" } });
        
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        reset();
    }
    public void reset()
    {
        if (newPassword.Text != "" && newPassword2.Text != "" && oldPassword.Text != "")
        {
            if (newPassword.Text.Trim().Length < 4 || newPassword.Text.Trim().Length > 10)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001361", "密码长度必须在4到10之间！") + "')</script>");
                return;
            }
            if (newPassword.Text != newPassword2.Text)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001617", "两次密码不一样！") + "')</script>");
                return;
            }
            string Member = Session["Member"].ToString();
            string NewPass = Encryption.Encryption.GetEncryptionPwd(this.newPassword.Text.ToString(), Session["Member"].ToString());
            string oldPass = Encryption.Encryption.GetEncryptionPwd(this.oldPassword.Text.ToString(), Session["Member"].ToString());
            int n = PwdModifyBLL.check(Member, oldPass, int.Parse(this.passtype.SelectedValue));
            string str = "";
            if (int.Parse(this.passtype.SelectedValue) == 0)
                str = GetTran("006057", "一级密码");
            else
                str = GetTran("006056", "二级密码");
            if (n > 0)
            {
                BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("Memberinfo", "ltrim(rtrim(number))");
                cl_h_info.AddRecord(Member);

                int i = 0;
                i = PwdModifyBLL.updateMemberPass(Member, NewPass, int.Parse(this.passtype.SelectedValue));
                if (i > 0)
                {
                    cl_h_info.AddRecord(Member);
                    cl_h_info.ModifiedIntoLogs(BLL.CommonClass.ChangeCategory.member3, Session["Member"].ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype6);
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + str + GetTran("000222", "修改成功") + "')</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + str + GetTran("000225", "修改失败！") + "')</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001624", "原始密码不正确，请准确填写！") + "')</script>");
                return;
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001626", "密码不能为空！") + "')</script>");
            
            return;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.oldPassword.Text = "";
        this.newPassword.Text = "";
        this.newPassword2.Text = "";
    }
}
