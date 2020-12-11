using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.CommonClass;
using System.Data.SqlClient;
using System.Data;
using DAL;
using Model;
using BLL.other.Company;
public partial class MemberMobile_PhoneSettings_RealName : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack) 
        {
            var Member = Session["Member"];
            if (Member != null)
            {
                MemberInfoModel member = MemInfoEditBLL.getMemberInfo(Member.ToString());
                if (member != null)
                {
                    if (!string.IsNullOrEmpty(member.Name))
                    {
                        if (member.Name.Length == 2)//两个字的名字
                        {
                            txtName.Text = Common.ReplaceWithSpecialChar(member.Name, 1, 0, '*');
                        }
                        else if (member.Name.Length < 2)
                        {
                            txtName.Text = member.Name;
                        }
                        else
                        {
                            txtName.Text = Common.ReplaceWithSpecialChar(member.Name, 1, 1, '*');
                        }
                        txtName.ReadOnly = true;
                    }
                    if (!string.IsNullOrEmpty(member.PaperNumber))
                    {
                        Hfcard.Value = member.PaperNumber;
                        txtcard.Text = Common.ReplaceWithSpecialChar(member.PaperNumber, 3, 4, '*');
                        txtcard.ReadOnly = true;
                    }

                    if(!string.IsNullOrEmpty(member.Name)&& !string.IsNullOrEmpty(member.PaperNumber))
                    {
                        btn_submit.Enabled = false;
                    }
                }
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
        var card = txtcard.Text.Trim();
        var name = txtName.Text.Trim();
        if (Member != null)
        {
            var value = updateMember(Member.ToString(), card, name);
            if (value > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('实名认证成功');</script>", false);
                Response.Redirect("SettingsIndex.aspx?res=success&&type=fanhui");
                txtcard.Text = card;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('实名认证失败');</script>", false);
                txtcard.Text = "";
            }
        };
    }
    private int updateMember(string number, string cdCard,string name)
    {
        var sql = "";
        MemberInfoModel member = MemInfoEditBLL.getMemberInfo(number);
        if (member != null)
        {
            if (string.IsNullOrEmpty(member.Name) && string.IsNullOrEmpty(member.PaperNumber))
            {
                sql = @"update memberinfo set PaperNumber=@PaperNumber,Name=@Name,papertypecode=@papertypecode where Number=@Number";
            }
            else if (string.IsNullOrEmpty(member.Name))
            {
                sql = @"update memberinfo set Name=@Name,papertypecode=@papertypecode where Number=@Number";
            }
            else if (string.IsNullOrEmpty(member.PaperNumber))
            {
                sql = @"update memberinfo set PaperNumber=@PaperNumber,papertypecode=@papertypecode where Number=@Number";
            }
        }
       
        SqlParameter[] para = {
                                      new SqlParameter("@PaperNumber",SqlDbType.VarChar),
                                      new SqlParameter("@Name",SqlDbType.NVarChar),
                                      new SqlParameter("@Number",SqlDbType.VarChar),
                                      new SqlParameter("@papertypecode",SqlDbType.VarChar)
                                  };
        para[0].Value = cdCard;
        para[1].Value = name;
        para[2].Value = number;
        para[3].Value = "P001";
        var returnvalue =DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);

        return returnvalue;
    }
}