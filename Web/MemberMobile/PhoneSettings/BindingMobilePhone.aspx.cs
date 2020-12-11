using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.other.Store;

using BLL.CommonClass;
using System.Data.SqlClient;
using System.Data;
using DAL;
using Model;
using BLL.other.Company;
using BLL.Registration_declarations;

public partial class MemberMobile_PhoneSettings_BindingMobilePhone : BLL.TranslationBase
{
    RegistermemberBLL registermemberBLL = new RegistermemberBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        //Response.Cache.SetExpires(DateTime.Now);
        if (!IsPostBack) 
        {
            var Member = Session["Member"];
            if (Member != null)
            {

                MemberInfoModel member = MemInfoEditBLL.getMemberInfo(Member.ToString());
                libindtel.Visible = true;
                txtBindtel.Text = member.MobileTele;
            }
            else
            {
                Response.Redirect("../index.aspx");
            }
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        var Member = Session["Member"];
        var tel = txtTele.Text.Trim();
        if (Member != null)
        {
            //验证手机号码是否重复
            if (registermemberBLL.CheckTeleTwice(txtTele.Text.Trim()) != null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('抱歉！该手机号码已被注册！');</script>", false);
                txtTele.Text = "";
                return;
            }
            var value = updateMember(Member.ToString(), tel);
            if (value > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('手机修改成功');</script>", false);
                Response.Redirect("SettingsIndex.aspx?res=success&&type=fanhui");
                txtTele.Text = tel;
                txtBindtel.Text = tel;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('手机修改失败');</script>", false);
                txtTele.Text = "";
            }
        };
       
    }

    private int updateMember(string number, string tel)
    {
        var sql = @"update memberinfo set MobileTele=@MobileTele where Number=@Number";
        SqlParameter[] para = {
                                      new SqlParameter("@MobileTele",SqlDbType.NVarChar),
                                      new SqlParameter("@Number",SqlDbType.NVarChar),
                                  };
        para[0].Value = tel;
        para[1].Value = number;

        var returnvalue =DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);

        return returnvalue;
    }
}