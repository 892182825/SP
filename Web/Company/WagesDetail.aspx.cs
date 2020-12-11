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


using DAL;
using Model;
using BLL.CommonClass;
using BLL.Registration_declarations;
using BLL.other.Member;
using System.Text;
using Standard.Classes;
using Model.Other;

public partial class Company_WagesDetail : BLL.TranslationBase 
{
    protected CommonDataBLL commonDataBLL = new CommonDataBLL();
    protected BasicSearchBLL basicSearchBLL = new BasicSearchBLL();

    private double money0 = 0;
    private double money1 = 0;
    private double money2 = 0;
    private double money3 = 0;
    private double money4 = 0;
    private double money5 = 0;
    private double money6 = 0;
    private double money7 = 0;
    private double money8 = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceGongzimingxi);
        if (!IsPostBack)
        {
            
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.GridView1,
            new string[][]{
                new string []{"001195","编号"},
                new string []{"000045","期数"},
                new string []{"000240","个人本期消费"},
                new string []{"000241","网络本期业绩"},
               // new string []{"000242","新增网络人数"},
                //new string []{"7577","无"},
                new string []{"010002","业绩奖"},
    //            new string []{ "007579","回本奖"},
    //            new string []{"007580", "大区奖"},
    //            new string []{ "007581", "小区奖"},
    //            new string []{"007582", "永续奖"},
    //new string []{"009128", "进步奖"},
    //               new string []{"001352", "网平台综合管理费"},
    //            new string []{"001353", "网扣福利奖金"},
    //               new string []{"001355", "网扣重复消费"},

                new string []{"000247","总计"},
                //new string []{"000249","扣税"},
                //new string []{"000251","扣款"},
                //new string []{"000252","补款"},
                new string []{"000254","实发"}
            });

        TranControls(BtnQuery, new string[][] { new string[] { "000340", "查询" } });
    }

    protected void BtnQuery_Click(object sender, System.EventArgs e)
    {
        if (this.TextBox1.Text.Trim() == "")
        {
            ScriptHelper.SetAlert(this.Page, GetTran("001018", "请输入会员编号！"));
            return;
        }
        else
        {
            int count = BLL.CommonClass.CommonDataBLL.getCountNumber(this.TextBox1.Text.Trim());
            if (count == 0)
            {
                ScriptHelper.SetAlert(this.Page, GetTran("000794", "该会员编号不存在！"));
                return;
            }
        }

        string bianhao = this.TextBox1.Text.Trim();
        DataTable table = new DataTable();
        int qishu = Convert.ToInt32(DropDownQiShu.ExpectNum);
        int qishu1 = Convert.ToInt32(DropDownQiShu1.ExpectNum);
        if (qishu <= qishu1)
        {
            for (int i = qishu1; i >= qishu; i--)
            {
                DataTable dt = basicSearchBLL.GetMemberBalance(i.ToString(), bianhao);

                if (i == qishu1)
                    table = dt;
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        table.Rows.Add(row.ItemArray);
                    }
                }
            }
            this.GridView1.DataSource = table.DefaultView;
            this.GridView1.DataBind();
        }
        else
        {
            ScriptHelper.SetAlert(this.Page, GetTran("000255","起始期数不能大于终止期数"));
        }
    }
    
    protected void Download_Click(object sender, System.EventArgs e)
    {
        DataTable table = new DataTable();
        string bianhao = this.TextBox1.Text.Trim();
        int qishu = Convert.ToInt32(DropDownQiShu.ExpectNum);
        int qishu1 = Convert.ToInt32(DropDownQiShu1.ExpectNum);
        if (qishu <= qishu1)
        {
            for (int i = qishu1; i >= qishu; i--)
            {
                DataTable dt = basicSearchBLL.GetMemberBalance(i.ToString(), bianhao);

                if (i == qishu1)
                    table = dt;
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        table.Rows.Add(row.ItemArray);
                    }
                }
            }
        }
        if (table == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000053", "没有数据，不能导出Excel！") + "')</script>");
            return;
        }
        if (table.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", 
                "<script language='javascript'>alert('" + GetTran("000053", "没有数据，不能导出Excel！") + "')</script>");
            return;
        }

        //Excel.OutToExcel(table, GetTran("001623", "工资查询"), new string[] { "number="+GetTran("001195", "编号"), 
        //    "ExpectNum="+GetTran("000045", "期数"), "CurrentOneMark="+GetTran("000240", "个人本期消费"), 
        //    "CurrentTotalNetRecord="+GetTran("000241", "网络本期业绩"), 
        //    "CurrentNewNetNum="+GetTran("000242", "新增网络人数"),  
        //    "Bonus1="+GetTran( "007578","推荐奖"), "Bonus2="+GetTran( "007579","回本奖"), 
        //    "Bonus3="+GetTran( "007580", "大区奖"), "Bonus4="+GetTran( "007581", "小区奖"), 
        //    "Bonus5="+GetTran("007582", "永续奖"), 
        //    "Kougl="+GetTran("001352", "网平台综合管理费"),
        //    "Koufl="+GetTran("001353", "网扣福利奖金"),
        //    "Koufx="+GetTran("001355", "网扣重复消费"),
        Excel.OutToExcel(table, GetTran("001623", "工资查询"), new string[] { "number="+GetTran("001195", "编号"),
            "ExpectNum="+GetTran("000045", "期数"), "CurrentOneMark="+GetTran("000240", "个人本期消费"),
            "CurrentTotalNetRecord="+GetTran("000241", "网络本期业绩"),
            //"CurrentNewNetNum="+GetTran("000242", "新增网络人数"),
            "Bonus0=业绩奖", /*"Bonus2="+GetTran( "007579","回本奖"),*/
            //"Bonus3="+GetTran( "007580", "大区奖"), "Bonus4="+GetTran( "007581", "小区奖"),
            //"Bonus5="+GetTran("007582", "永续奖"),
            //"Kougl="+GetTran("001352", "网平台综合管理费"),
            //"Koufl="+GetTran("001353", "网扣福利奖金"),
            //"Koufx="+GetTran("001355", "网扣重复消费"),


            "CurrentTotalMoney="+GetTran("000247", "总计"), "DeductTax="+GetTran("000249", "扣税"),
            /*"DeductMoney=" +GetTran("000251", "扣款"),*/ /*"bqbukuan="+GetTran("000252", "补款"),*/
            "CurrentSolidSend=" +GetTran("000254", "实发")});
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
           
            money0 += Convert.ToDouble((e.Row.Cells[4].Controls[0] as HyperLink).Text);
            //money1 += Convert.ToDouble((e.Row.Cells[5].Controls[0] as HyperLink).Text);
            //money2 += Convert.ToDouble((e.Row.Cells[6].Controls[0] as HyperLink).Text);
            //money3 += Convert.ToDouble((e.Row.Cells[7].Controls[0] as HyperLink).Text);
            //money4 += Convert.ToDouble((e.Row.Cells[8].Controls[0] as HyperLink).Text);
            //money5 += Convert.ToDouble(e.Row.Cells[12].Text);
            //money6 += Convert.ToDouble((e.Row.Cells[9].Controls[0] as HyperLink).Text);
            //money7 += Convert.ToDouble(e.Row.Cells[10].Text);
            //money8 += Convert.ToDouble(e.Row.Cells[11].Text);
            //money6 += Convert.ToDouble(e.Row.Cells[13].Text);
            //money7 += Convert.ToDouble(e.Row.Cells[14].Text);
            //money8 += Convert.ToDouble(e.Row.Cells[15].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = GetTran("000247", "总计");
            e.Row.Cells[4].Text += money0.ToString("0.00");
            //e.Row.Cells[5].Text += money1.ToString("0.00");
            //e.Row.Cells[6].Text += money2.ToString("0.00");
            //e.Row.Cells[7].Text += money3.ToString("0.00");
            //e.Row.Cells[8].Text += money4.ToString("0.00");
           
            //e.Row.Cells[9].Text += money6.ToString("0.00");
            //e.Row.Cells[10].Text += money6.ToString("0.00");
            //e.Row.Cells[11].Text += money8.ToString("0.00");
            //e.Row.Cells[12].Text += money5.ToString("0.00");
        }
        Translations();
    }
}
