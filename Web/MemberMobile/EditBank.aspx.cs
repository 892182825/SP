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
using BLL.CommonClass;
using DAL;
using System.Data.SqlClient;
using BLL.other;

using BLL.other.Company;
using Model;
using BLL.Registration_declarations;
using BLL.other.Store;
using BLL.other.Member;

public partial class EditBank : BLL.TranslationBase
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCardinfo();
        }
    }

    private void BindCardinfo() {
        string number = Session["Member"].ToString(); 
        this.ddlscbank.DataSource = new AddOrderBLL().GetBank(1, "中文");
        this.ddlscbank.DataTextField = "BankName";
        this.ddlscbank.DataValueField = "BankCode";
        this.ddlscbank.DataBind();
        MemberInfoModel md = MemberInfoDAL.getMemberInfo(number);
        if (md != null) {
            lblname.Text = md.Name;
            txtbankcard.Text = md.BankCard;
            txtbankbanchname.Text = md.Bankbranchname;
           if(md.BankCode!="")  ddlscbank.SelectedValue = md.BankCode;
        }
       

    }
    /// <summary>
    /// 翻译
    /// </summary>
    private void Translations()
    {
        this.TranControls(this.btnSubmit, new string[][] { new string[] { "009129", "登录" } });
        //this.TranControls(this.btnReset, new string[][] { new string[] { "001614", "取消" } });
    }
    
    /// <summary>
    /// 登陆事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string number = Session["Member"].ToString(); 
        //检验
        if (this.txtbankcard.Text.Trim() == "")
        {
            msg = "<script language='javascript'>alert('" + GetTran("007761", "请输入银行卡！") + "');</script>";
            return;
        }
        if (this.txtPwd.Text.Trim() == "")
        {
            msg = "<script language='javascript'>alert('" + GetTran("005598", "请输入密码！") + "');</script>";
            return;
        }

        string oldPass = Encryption.Encryption.GetEncryptionPwd(this.txtPwd.Text.ToString(), Session["Member"].ToString());
        int n = PwdModifyBLL.check(Session["Member"].ToString(), oldPass, 1);
        if (n > 0)
        {
            string bankcode = this.ddlscbank.SelectedValue;
            string BankCard = this.txtbankcard.Text;
            string bankbanchname =txtbankbanchname .Text;
            MemberInfoModifyBll mf = new MemberInfoModifyBll();
            int c = DBHelper.ExecuteNonQuery(" update   memberinfo set Bankbranchname='"+bankbanchname+"' ,  BankCard='" + BankCard + "',bankcode='" + bankcode + "' where  number='" + number + "' ");
          if (c > 0)
          {
              msg = "<script language='javascript'>alert(' " + GetTran("000222", "") + "  '); window.location.href='first.aspx';</script>";
              return;

          }
          else {
              msg = "<script language='javascript'>alert(' " + GetTran("000225", "") + "');</script>";
              return;
          }
        }
        else {
            msg = "<script language='javascript'>alert(' " + GetTran("006058", "") + "');</script>";
            return;
        }
       
    }
   
}