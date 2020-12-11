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
using Model;
using BLL.MoneyFlows;
using BLL.CommonClass;
using Model.Other;

public partial class Company_StrikeBalances2 : BLL.TranslationBase 
{
    CommonDataBLL commonData = new CommonDataBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceHuiDuiChongHong);
        if (!this.IsPostBack)
        {
            if (Request.QueryString["qs"] != null)
            {
                ViewState["qs"] = Request.QueryString["qs"].ToString();
                ViewState["number"] = Request.QueryString["number"].ToString();

                this.Number.Text = ViewState["number"].ToString();

                 this.money.Text = ReleaseBLL.getChongHong(ViewState["number"].ToString(),Convert.ToInt32(ViewState["qs"])).ToString("f2");
            }
        }

        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.Button1, new string[][] { new string[] { "000351", "提 交" } });
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.Number.Text.Trim() == "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000787", "请输入编号！") + "')</script>");
            return;
        }

        MemberInfoModel info = ReleaseBLL.GetMemberInfo(this.Number.Text);
         if (info == null)
         {
             Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001536", "该编号不存在于系统中") + "')</script>");
             return;
         }

         try
         {
             double money = Convert.ToDouble(this.money.Text);
             if (money <= 0)
             {
                 Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001539", "操作失败，输入的金额不能小于0！") + "')</script>");
                 return;
             }

             if (this.question.Text.Trim() == "")
             {
                 Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001540", "退回原因不能为空！") + "')</script>");
                 return;
             }
             ChongHongModel chonghong = new ChongHongModel();
             chonghong.MoneyNum = money;
             chonghong.Number = this.Number.Text;
             chonghong.ExpectNum = Convert.ToInt32(ViewState["qs"]);
             chonghong.Remark = this.question.Text;
             chonghong.StartDate = DateTime.Now.ToUniversalTime();
             bool blean = ReleaseBLL.AddChongHong(chonghong);
             if (blean)
             {
                 Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001401", "操作成功！") + "');location.href='StrikeBalancesView.aspx';</script>");
                 cleartext();
             }
         }

         catch
         {
             Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001541", "操作失败！") + "')</script>");
             return;
         }
    }
   
    public void cleartext()
    {
        this.Number.Text = "";
        this.money.Text = "";
        this.question.Text = "";
    }
}
