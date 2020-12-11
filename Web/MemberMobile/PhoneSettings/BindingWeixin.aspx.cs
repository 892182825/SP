using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DAL;
using Model;
using BLL.other.Company;
public partial class MemberMobile_PhoneSettings_BindingWeixin : BLL.TranslationBase
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
                var Member = Session["Member"];
                if (Member != null)
                {
                    MemberInfoModel member = MemInfoEditBLL.getMemberInfo(Member.ToString());
                    if (member != null)
                    {
                        txtweixin.Text = member.Weixin;
                        txtName.Text = member.Name;
                    }

                }
                else
                {
                    Response.Redirect("../index.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("../index.aspx");
            }
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        var weixin = txtweixin.Text;
        var Member = Session["Member"];
        if (Member != null)
        {
            var value = updateMember(Member.ToString(), weixin);
            if (value > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('微信修改成功');</script>", false);
                Response.Redirect("SettingsIndex.aspx?res=success&&type=fanhui");
                txtweixin.Text = weixin;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('微信修改失败');</script>", false);
                txtweixin.Text = "";
            }
        }
    }

    private int updateMember(string number, string value)
    {
        var returnvalue = 0;
        var sql = @"update memberinfo set weixin=@weixin where Number=@Number";
        SqlParameter[] para = {
                                      new SqlParameter("@weixin",SqlDbType.VarChar),
                                      new SqlParameter("@Number",SqlDbType.VarChar)
                                  };
        para[0].Value = value;
        para[1].Value = number;
        returnvalue = DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
        return returnvalue;
    }
}