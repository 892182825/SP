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
using System.Text;
using Model;
using Model.Other;
using BLL.other.Company;
using BLL.CommonClass;
using BLL;
using DAL;
using Standard.Classes;
using BLL.MoneyFlows;

public partial class Company_SalaryGrantView : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceJiangjinshezhi);

        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            if (Request.QueryString["qs"] != null)
            {
                ViewState["qs"] = Request.QueryString["qs"].ToString();
                 bool blean = ReleaseBLL.IsProvide(Convert.ToInt32(ViewState["qs"].ToString()));
                 if (blean)
                 {
                     this.labmx.Text = "第 " + ViewState["qs"].ToString() + " 期已发放奖金明细";

                 }
                 else
                 {
                     this.labmx.Text = "第 " + ViewState["qs"].ToString() + " 期未发放奖金明细";
                 }
            }
           
            BtnConfirm_Click(null, null);
        }
        Translations();

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalaryGrant.aspx");
    }
    protected object GetRDate(object obj)
    {
        if (obj != null)
        {
            try { return Convert.ToDateTime(obj).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()); }
            catch { }
        }
        return "";
    }

    private void Translations()
    {
        this.TranControls(this.GridView1,
                new string[][]{
                new string []{"000024", "会员编号"},
                new string []{"000025", "会员姓名"},
                new string []{"000045", "期数"},
                new string []{"007578","推荐奖"},
                     new string []{"007579","回本奖"},
                          new string []{"007580", "大区奖"},
                               new string []{"007581", "小区奖"},
                                    new string []{"007582", "永续奖"},
                                         new string []{"009128", "进步奖"},
                                              new string []{"001352", "网平台综合管理费"},
                                                   new string []{"001353", "网扣福利奖金"},
                                                        new string []{"001355", "网扣重复消费"},
                                                             new string []{"000251", "扣款"},
                new string []{"000249", "扣税"},
                 new string []{"000247", "总计"},
                   new string []{"007076", "补款"},
                   new string []{"000254", "实发"},
                   new string []{"001546", "时间"},
                });

    }
    //查询按钮
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        getBalanceToPurseDetail();
    }

    

    #region 获得会员
    public void getBalanceToPurseDetail()
    {
        string Number = DisposeString.DisString(this.Number.Text.Trim());
        string Name = Encryption.Encryption.GetEncryptionName(DisposeString.DisString(this.Name.Text.Trim()));

        bool blean = ReleaseBLL.IsProvide(Convert.ToInt32(ViewState["qs"].ToString()));
        if (blean)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(" 1=1 and b.number=m.number and b.isyxj=1 and b.ExpectNum=" + ViewState["qs"].ToString());
            if (Number.Length > 0)
            {
                sb.Append(" and b.Number like'%" + Number + "%'");
            }
            if (Name.Length > 0)
            {
                sb.Append(" and m.Name like'%" + Name + "%'");
            }

            ViewState["SQLSTR"] = "SELECT b.*,m.name FROM BalanceToPurseDetail b,memberinfo m WHERE " + sb.ToString() + " isyxj=1 order by b.id desc";
            string asg = ViewState["SQLSTR"].ToString();
            Pager pager = Page.FindControl("Pager1") as Pager;
            pager.Pageindex = 0;
            pager.PageSize = 10;
            pager.PageTable = "BalanceToPurseDetail b,memberinfo m";
            pager.Condition = sb.ToString();
            pager.PageColumn = " b.*,m.name ";
            pager.ControlName = "GridView1";
            pager.key = " b.id ";
            pager.InitBindData = true;
            pager.PageBind();
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" 1=1 and b.number=m.number and b.isyxj=1 and b.ExpectNum=" + ViewState["qs"].ToString());
            if (Number.Length > 0)
            {
                sb.Append(" and b.Number like'%" + Number + "%'");
            }
            if (Name.Length > 0)
            {
                sb.Append(" and m.Name like'%" + Name + "%'");
            }
            ViewState["SQLSTR"] = "SELECT b.Number,b.CurrentOneMark,b.Bonus1,b.Bonus2,b.Bonus3,b.Bonus4,b.Bonus5,b.Bonus6,b.Bonus7,b.Bonus8,b.DeductTax as Total,b.CurrentSolidSend,m.name, b.ExpectNum as ExpectNum,'" + DateTime.Now.ToUniversalTime() + "' as TransferToPurseDate,0 as bqbukuan FROM BalanceToPurseDetail b,memberinfo m WHERE " + sb.ToString() + " order by m.id desc";
            string asg = ViewState["SQLSTR"].ToString();
            Pager pager = Page.FindControl("Pager1") as Pager;
            pager.Pageindex = 0;
            pager.PageSize = 10;
            pager.PageTable = " BalanceToPurseDetail b,memberinfo m";
            pager.Condition = sb.ToString();
            pager.PageColumn = " b.Number,b.CurrentOneMark,b.Bonus1,b.Bonus2,b.Bonus3,b.Bonus4,b.Bonus5,b.Bonus6,b.Bonus7,b.Bonus8,b.DeductTax as Total,b.CurrentSolidSend,m.name,b.ExpectNum as ExpectNum,'" + DateTime.Now.ToUniversalTime() + "' as TransferToPurseDate,0 as bqbukuan  ";
            pager.ControlName = "GridView1";
            pager.key = " b.id ";
            pager.InitBindData = true;
            pager.PageBind();
        }

        Translations();
    }
    #endregion
   
    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[1].Text);//解密店铺名称
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
          

        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
        Translations();
    }
    
    protected void btnDownExcel_Click(object sender, EventArgs e)
    {
        string cmd = ViewState["SQLSTR"].ToString();
        DataTable dt = DBHelper.ExecuteDataTable(cmd);
        if (dt == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000053", "没有数据，不能导出Excel！") + "')</script>");
            return;
        }
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000053", "没有数据，不能导出Excel！") + "')</script>");
            return;
        }
        
        foreach (DataRow row in dt.Rows)
        {
            row["Name"] = Encryption.Encryption.GetDecipherName(row["Name"].ToString());
            try
            {
                row["TransferToPurseDate"] = Convert.ToDateTime(row["TransferToPurseDate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours());
            }
            catch
            {
            }
        }
        Excel.OutToExcel(dt, GetTran("007077", "发放奖金明细"), new string[] { "Number=" + GetTran("000024", "会员编号"), "Name=" + GetTran("000025", "会员姓名"), "ExpectNum=" + GetTran("000045", "期数"), "CurrentOneMark=" + GetTran("000939", "新个分数"),
         "Bonus1="+GetTran( "007578","推荐奖"), "Bonus2="+GetTran( "007579","回本奖"), "Bonus3="+GetTran( "007580", "大区奖"), "Bonus4="+GetTran( "007581", "小区奖"), "Bonus5="+GetTran("007582", "永续奖"), "Bonus6="+GetTran("009128", "进步奖"), 
            "Kougl="+GetTran("001352", "网平台综合管理费"),
            "Koufl="+GetTran("001353", "网扣福利奖金"),
            "Koufx="+GetTran("001355", "网扣重复消费"),

            "DeductMoney=" + GetTran("000251", "扣款"), "DeductTax=" + GetTran("000249", "扣税"), "Total=" + GetTran("000247", "总计"), "bqbukuan=" + GetTran("007076", "补款"), "CurrentSolidSend=" + GetTran("000254", "实发"), "TransferToPurseDate=" + GetTran("001546", "时间") });
    }
}
