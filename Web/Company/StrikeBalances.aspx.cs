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
using System.Text;
using DAL;
using Standard.Classes;
public partial class Company_StrikeBalances : BLL.TranslationBase
{
    ProcessRequest process = new ProcessRequest();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceHuiDuiChongHong);
        ViewState["byzj"] = 0;
        if (!IsPostBack)
        {
            BtnConfirm_Click(null, null);
            this.BtnConfirm.Attributes.Add("onclick", "return CheckFrom()");
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"001195","编号"},
                    new string []{"000022","删除"},
                    new string []{"000024","会员编号"},
                    new string []{"000025","会员姓名"},
                    new string []{"000045","期数"},
                    new string []{"001488","退回金额"},
                    new string []{"001489","退回日期"},
                    new string []{"001490","原因"}
                });
        this.TranControls(this.Button2, new string[][] { new string[] { "001498", "会员工资退回" } });
        TranControls(BtnConfirm, new string[][] { new string[] { "000340", "查询" } });

    }
    private void GetShopList()
    {
        if (process.ProcessSqlStr(this.txt_member.Text) == false)
        {
            this.txt_member.Text = "";
        };
        StringBuilder condition = new StringBuilder();
        string table = "ChongHong c,memberinfo m";
        condition.Append("IsDelete=0 and c.number=m.number");

        if (this.txt_member.Text != "")
        {
            condition.Append(" and c.Number like '%" + this.txt_member.Text.Trim() + "%'");
        }
        if (this.DropDownQiShu.ExpectNum!=-1)
        {
            condition.Append(" and c.ExpectNum=" + this.DropDownQiShu.ExpectNum);
        }

        if (text_recebh.Text != "")
        {
            condition.Append(" and cast (MoneyNum as varchar (20)) like '%" + this.text_recebh.Text.Trim() + "%'");
        }
        
        string key = "id";
        string cloumns = " c.ID,c.Number,c.ExpectNum,c.MoneyNum,c.StartDate,m.name ";
        this.GridView1.DataSourceID = null;
        this.Pager1.ControlName = "GridView1";
        this.Pager1.key = key;
        this.Pager1.PageColumn = cloumns;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = table;
        this.Pager1.Condition = condition.ToString();
        this.Pager1.PageCount = 0;
        this.Pager1.PageSize = 10;
        this.Pager1.PageBind();
        ViewState["cloumns"] = cloumns;
        ViewState["table"] = table;
        ViewState["condition"] = condition;
        Translations();
    }
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        GetShopList();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            Response.Cache.SetExpires(DateTime.Now);
            Permissions.CheckManagePermission(EnumCompanyPermission.FinanceDelTuihui);
            ChangeLogs cl = new ChangeLogs("ChongHong", "ltrim(rtrim(str(ID)))");  
            cl.AddRecord(int.Parse(e.CommandArgument.ToString()));
            //获取当前选择的行
            GridViewRow row=(GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            //删除
            bool blean = ReleaseBLL.DelChongHong(int.Parse(e.CommandArgument.ToString()), double.Parse(row.Cells[4].Text), row.Cells[2].Text);
            if(blean)
            {
                
                cl.DeletedIntoLogs(ChangeCategory.company17, e.CommandArgument.ToString(), ENUM_USERTYPE.objecttype5);
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000749", "删除成功！") + "')</script>");
            }
            GetShopList();
        
            
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[3].Text);//解密店铺名称
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            ((HyperLink)e.Row.FindControl("HyperLink1")).NavigateUrl = "koukuanyuanyin.aspx?type=chongh&ID=" + (e.Row.FindControl("Label1") as Label).Text.ToString();
            LinkButton LButton = (LinkButton)e.Row.FindControl("Linkbutton2");
            LButton.Attributes.Add("onClick", "return confirm('" + GetTran("001514", "你确定要删除该条信息吗?") + "');");
            try
            {
                e.Row.Cells[6].Text = DateTime.Parse(e.Row.Cells[5].Text).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
            }
            catch
            {
            }
            ViewState["byzj"] = Convert.ToDouble(ViewState["byzj"]) + Convert.ToDouble(e.Row.Cells[5].Text);
        }                    
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            e.Row.Cells[1].Text = "本页总计";
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].Text = Convert.ToDouble(ViewState["byzj"]).ToString("f2");
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
        }
        Translations();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceTianJiaChongHong);
        Response.Redirect("StrikeBalancesView.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string cmd = "select c.Number,c.ExpectNum,c.MoneyNum,c.StartDate,c.Remark,m.name from " + ViewState["table"].ToString() + " where " + ViewState["condition"] + "";
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
        }
        Excel.OutToExcel(dt, GetTran("001498", "工资退回"), 
            new string[] { "Number=" + GetTran("000024", "会员编号"),"name="+GetTran("000025","会员姓名"),
                "ExpectNum=" + GetTran("000045", "期数"), "MoneyNum=" + GetTran("001488", "退回金额"), 
                "StartDate=" + GetTran("001489", "退回日期"), "Remark=" + GetTran("001490", "原因") });

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}
