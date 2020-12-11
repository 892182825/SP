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

public partial class Company_StrikeBalancesView : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceHuiDuiChongHong);

        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        ViewState["zjj"] = 0;
        ViewState["glj"] = 0;
        ViewState["fxlj"] = 0;
        ViewState["kk"] = 0;
        ViewState["ks"] = 0;
        ViewState["bk"] = 0;
        ViewState["sf"] = 0;
        if (!IsPostBack)
        {
            CommonDataBLL.BindQishuList(this.DropDownExpectNum, true);

            string maxqs = DBHelper.ExecuteScalar("select isnull(max(expectnum),0) from BalanceToPurseDetail").ToString();
            if(maxqs!="0")
            {
                this.DropDownExpectNum.SelectedValue = maxqs;
            }
            BtnConfirm_Click(null, null);
        }
        Translations();

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
                    new string []{"001498", "会员工资退回"},
                    new string []{"000024", "会员编号"},
                     new string []{"000025", "会员姓名"},
                    new string []{"000045", "期数"},
                    new string []{"000939", "新个分数"},
                    new string []{"001352", "网平台综合管理费"},
                    new string []{"001353", "网扣福利奖金"},
                    new string []{"001355", "网扣重复消费"},

                    new string []{"000251", "扣款"},
                    new string []{"000249", "扣税"},

                    new string []{"000247", "总计"},
                    new string []{"000252", "补款"},
                    new string []{"000254", "实发"},
                    new string []{"001546", "时间"}});
        this.TranControls(Button3, new string[][] { new string[] { "000421", "返回" } });

    }
    //查询按钮
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        getBalanceToPurseDetail();
        getTotal();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("StrikeBalances.aspx");
    }


    public void getBalanceToPurseDetail()
    {
        string Number = DisposeString.DisString(this.Number.Text.Trim());
        string Name = Encryption.Encryption.GetEncryptionName(DisposeString.DisString(this.Name.Text.Trim()));

        int ExpectNum = 0;
        if (this.DropDownExpectNum.SelectedValue.ToString() == "")
        {
            ExpectNum = 0;
        }
        else
        {
            ExpectNum = Convert.ToInt32(this.DropDownExpectNum.SelectedItem.Value);
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(" 1=1 and b.number=m.number and istuihui=0");
        if (Number.Length > 0)
        {
            sb.Append(" and b.Number like'%" + Number + "%'");
        }
        if (Name.Length > 0)
        {
            sb.Append(" and m.Name like'%" + Name + "%'");
        }

        if (ExpectNum > 0)
        {
            sb.Append(" and b.ExpectNum=" + ExpectNum);
        }


        ViewState["SQLSTR"] = "SELECT b.*,m.name FROM BalanceToPurseDetail b,memberinfo m WHERE " + sb.ToString() + " order by b.id desc";
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


        Translations();
    }

    //导出Excle
    private void datalist(string sql)
    {
        DataTable dt = CommonDataBLL.datalist(sql);
        if (dt == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000051", "对不起，找不到指定条件的记录！") + "')</script>");
            return;
        }
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000051", "对不起，找不到指定条件的记录！") + "')</script>");
            return;
        }
        if (dt.Rows.Count > 0)
        {
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            ViewState["zzj"] = Convert.ToDouble(ViewState["zzj"]) + Convert.ToDouble(e.Row.Cells[5].Text.Trim());
            ViewState["glj"] = Convert.ToDouble(ViewState["glj"]) + Convert.ToDouble(e.Row.Cells[6].Text.Trim());
            ViewState["fxlj"] = Convert.ToDouble(ViewState["fxlj"]) + Convert.ToDouble(e.Row.Cells[7].Text.Trim());
            ViewState["kk"] = Convert.ToDouble(ViewState["kk"]) + Convert.ToDouble(e.Row.Cells[8].Text.Trim());
            ViewState["ks"] = Convert.ToDouble(ViewState["ks"]) + Convert.ToDouble(e.Row.Cells[9].Text.Trim());
            ViewState["bk"] = Convert.ToDouble(ViewState["bk"]) + Convert.ToDouble(e.Row.Cells[11].Text.Trim());
            ViewState["sf"] = Convert.ToDouble(ViewState["sf"]) + Convert.ToDouble(e.Row.Cells[12].Text.Trim());
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            e.Row.Cells[0].Text = "本页总计";
            e.Row.Cells[5].Text = Convert.ToDouble(ViewState["zzj"]).ToString("f2");
            e.Row.Cells[6].Text = Convert.ToDouble(ViewState["glj"]).ToString("f2");
            e.Row.Cells[7].Text = Convert.ToDouble(ViewState["fxlj"]).ToString("f2");
            e.Row.Cells[8].Text = Convert.ToDouble(ViewState["kk"]).ToString("f2");
            e.Row.Cells[9].Text = Convert.ToDouble(ViewState["ks"]).ToString("f2");
            e.Row.Cells[11].Text = Convert.ToDouble(ViewState["bk"]).ToString("f2");
            e.Row.Cells[12].Text = Convert.ToDouble(ViewState["sf"]).ToString("f2");
        }
        Translations();
    }
    protected void lkSubmit1_Click(object sender, EventArgs e)
    {
        BtnConfirm_Click(null, null);
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
          "Bonus1="+GetTran( "007578","推荐奖"), "Bonus2="+GetTran( "007579","回本奖"), "Bonus3="+GetTran( "007580", "大区奖"), "Bonus4="+GetTran( "007581", "小区奖"), "Bonus5="+GetTran("007582", "永续奖"), 
            "Bonus6="+GetTran("001352", "网平台综合管理费"),
            "Bonus7="+GetTran("001353", "网扣福利奖金"),
            "Bonus8="+GetTran("001355", "网扣重复消费"),
            "DeductMoney=" + GetTran("000251", "扣款"), "DeductTax=" + GetTran("000249", "扣税"), "Total=" + GetTran("000247", "总计"), "bqbukuan=" + GetTran("007076", "补款"), "CurrentSolidSend=" + GetTran("000254", "实发"), "TransferToPurseDate=" + GetTran("001546", "时间") });
        
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        if (e.CommandName == "linkbtnth")
        {
            string[] args = e.CommandArgument.ToString().Split(':');
            string qs = args[0].ToString();
            string number = args[1].ToString();

            Response.Redirect("StrikeBalances2.aspx?qs="+qs+"&number="+number+"");
        }
    }

    public void getTotal()
    {
        string sqlwhere = "";
        if (DropDownExpectNum.SelectedValue != "-1")
        {
            sqlwhere = " where expectnum=" + DropDownExpectNum.SelectedValue;
        }
        Label2.Text = GetTran("007859", "组织奖总计") + "：<font color='red'>" + RemittancesBLL.GetTotalMoney("Bonus0", "BalanceToPurseDetail", sqlwhere) + "</font>";
        Label3.Text = GetTran("007860", "管理奖总计") + "：<font color='red'>" + RemittancesBLL.GetTotalMoney("Bonus1", "BalanceToPurseDetail", sqlwhere) + "</font>";
        Label4.Text = GetTran("007861", "复消累计奖总计") + "：<font color='red'>" + RemittancesBLL.GetTotalMoney("Bonus2", "BalanceToPurseDetail", sqlwhere) + "</font>";
        Label5.Text = GetTran("007756", "扣款总计") + "：<font color='red'>" + RemittancesBLL.GetTotalMoney("DeductMoney", "BalanceToPurseDetail", sqlwhere) + "</font>";
        Label6.Text = GetTran("007862", "扣税总计") + "：<font color='red'>" + RemittancesBLL.GetTotalMoney("DeductTax", "BalanceToPurseDetail", sqlwhere) + "</font>";
        Label7.Text = GetTran("007863", "补款总计") + "：<font color='red'>" + RemittancesBLL.GetTotalMoney("bqbukuan", "BalanceToPurseDetail", sqlwhere) + "</font>";
        Label8.Text = GetTran("007864", "实发总计") + "：<font color='red'>" + RemittancesBLL.GetTotalMoney("CurrentSolidSend", "BalanceToPurseDetail", sqlwhere) + "</font>";
    }
}
