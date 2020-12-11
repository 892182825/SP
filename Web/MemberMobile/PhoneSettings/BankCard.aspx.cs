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
public partial class MemberMobile_PhoneSettings_BankCard : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            CommonDataBLL.BindCountry_Bank("中国", ddlcard);
            var Member = Session["Member"];
            if (Member != null)
            {
                MemberInfoModel member = MemInfoEditBLL.getMemberInfo(Member.ToString());
                txtName.Text = member.Name;
                txtCName.Text = member.Name;
                txtcard.Text = member.BankCard;
                ddlcard.SelectedValue = member.BankCode;
                txtBankbranchname.Text = member.Bankbranchname;
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
        var bankCode=ddlcard.SelectedValue.ToString();
        var bankCard=txtcard.Text.Trim();
        var cname = txtCName.Text.Trim();
        var name = txtName.Text.Trim();
        var Bankbranchname = txtBankbranchname.Text.Trim();
        if (Member != null)
        {
            MemberInfoModel member = MemInfoEditBLL.getMemberInfo(Member.ToString());

            if (member != null)
            {
                if(string.IsNullOrEmpty(member.Name)|| string.IsNullOrEmpty(member.PaperNumber)) 
                {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "",
            //"alert('确定？');if(confirm('确定')){}else {}", true);
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请先进行实名认证，再绑定银行卡！');window.location='RealName.aspx?res=success&&type=name'</script>", false);
                    return;
                }
            }
            if(cname!= name)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('开户姓名必须跟真实姓名一致，以免不必要的资金纠纷！！');</script>", false);
                return;
            }


            var value = updateMember(Member.ToString(), bankCode, bankCard, cname, Bankbranchname);
            if (value > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('银行卡绑定成功');</script>", false);
                txtcard.Text = bankCard;
                Response.Redirect("SettingsIndex.aspx?res=success&&type=fanhui");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('银行卡绑定失败');</script>", false);
                txtcard.Text = "";
            }
        }

    }
    private int updateMember(string number, string bankCode, string bankCard,string name,string bankbranchname)
    {
        string BankCard = Encryption.Encryption.GetEncryptionCard(bankCard);//银行卡号
        string BankBook = Encryption.Encryption.GetEncryptionName(name); //开户名
        var sql = @"update memberinfo set BankCode=@BankCode,BankCard=@BankCard,BankBook=@BankBook,Bankbranchname=@Bankbranchname where Number=@Number";
        SqlParameter[] para = {
                                      new SqlParameter("@BankCode",SqlDbType.VarChar),
                                      new SqlParameter("@BankCard",SqlDbType.VarChar),
                                      new SqlParameter("@BankBook",SqlDbType.VarChar),
                                      new SqlParameter("@Bankbranchname",SqlDbType.VarChar),
                                      new SqlParameter("@Number",SqlDbType.VarChar)
                                  };
        para[0].Value = bankCode;
        para[1].Value = BankCard;
        para[2].Value = BankBook;
        para[3].Value = bankbranchname;
        para[4].Value = number;

        var returnvalue =DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);

        return returnvalue;
    }
}
