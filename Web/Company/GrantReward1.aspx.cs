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
using System.Text;
using Model.Other;
using BLL.CommonClass;
using Standard.Classes;
using DAL;

public partial class Company_GrantReward1 : BLL.TranslationBase
{
    private static DataTable dtt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //检查管理员权限
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceGrantReward);
        ViewState["byzj"] = 0;
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.GridView2,
                new string[][]{
                    new string []{"000613","日期"},
                    new string []{"000041","总金额"},
                new string []{"001438","总比数"},
                new string []{"001439","支付宝账号"}});

        this.TranControls(this.dgTongji,
        new string[][]{
                    new string []{"000613","日期"},
                    new string []{"000041","总金额"},
                new string []{"001438","总比数"},
                new string []{"001439","支付宝账号"}});


        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"001442","商户流水号"},
                    new string []{"001443","收款银行户名"},
                    new string []{"001444","收款银行账号"},
                    new string []{"001446","收款开户银行"},
                    new string []{"001447","收款银行所在省份"},
                    new string []{"001450","收款银行所在城市"},
                    new string []{"001451","收款支行名称"},
                    new string []{"000322","金额"},
                    new string []{"001453","对公对私标志"},
                    new string []{"000078","备注"},
                    new string []{"001456","信息"}
                });
        this.TranControls(this.dgDetails,
        new string[][]{
                    new string []{"001442","商户流水号"},
                    new string []{"001443","收款银行户名"},
                    new string []{"001444","收款银行账号"},
                    new string []{"001446","收款开户银行"},
                    new string []{"001447","收款银行所在省份"},
                    new string []{"001450","收款银行所在城市"},
                    new string []{"001451","收款支行名称"},
                    new string []{"000322","金额"},
                    new string []{"001453","对公对私标志"},
                    new string []{"000078","备注"},
                    new string []{"001456","信息"}
                });
        this.TranControls(this.btnShow, new string[][] { new string[] { "000566", "显 示" } });
        this.TranControls(this.BtnRelease, new string[][] { new string[] { "001459", "确认汇兑" } });
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        double vjine = 0.00;
        try
        {
            vjine = Convert.ToDouble(jine.Text.Trim());
        }

        catch
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('金额格式输入有误！');</script>");
            return;
        }

        getTotal(vjine);
        string bcard = "&nbsp;";
        string sqlbank = " MemberBank.BankName as bank";
        string sqlcity = " City.City  as bankcity";
        string sqlprovince = " City.Province as bankprovince ";
        string col = " Memberinfo.id,Number,MobileTele,name,BankCard,MemberInfo.bankbook,bankcard as strbankcard," + sqlbank + "," + sqlprovince + "," + sqlcity + ",'' as souzhi,Jackpot-MemberInfo.Out  as zongji," + 2 + " as biaozhi";
        string table = " MemberInfo left join MemberBank on MemberBank.BankCode=Memberinfo.BankCode left join City on Memberinfo.CPCCode=City.CPCCode ";
        string condtion = "  MemberInfo.Jackpot-MemberInfo.Out>=" + vjine.ToString() + " and MemberInfo.Jackpot-MemberInfo.Out>0";
        string ordercol = " MemberInfo.id ";

        this.Pager1.PageBind(0, 10, table, col, condtion, ordercol, "GridView1");

        if (GridView1.Rows.Count > 0)
        {
            GridView1.Visible = true;
            ViewState["pandaun"] = true;
            Pager1.Visible = true;

            PayParames pp = new PayParames();
            string email = pp.Seller_Email.ToString();
            ViewState["dtt"] = "select " + DateTime.Now.ToString("yyyyMMdd") + " as riqi ,sum(MemberInfo.Jackpot-MemberInfo.Out)  as zongMoney ,count(0) as zongBishu,'" + email + "' as  paymentaccount from memberinfo where  memberinfo.number = memberinfo.number AND MemberInfo.Jackpot-MemberInfo.Out>=" + vjine.ToString() + " and (MemberInfo.Jackpot-MemberInfo.Out)>0 ";
            dtt = DBHelper.ExecuteDataTable("select " + DateTime.Now.ToString("yyyyMMdd") + " as riqi ,sum(MemberInfo.Jackpot-MemberInfo.Out)  as zongMoney ,count(0) as zongBishu,'" + email + "' as  paymentaccount from memberinfo where  memberinfo.number = memberinfo.number AND MemberInfo.Jackpot-MemberInfo.Out>=" + vjine.ToString() + " and (MemberInfo.Jackpot-MemberInfo.Out)>0 ");

            if (dtt.Rows.Count > 0)
            {
                this.GridView2.Visible = true;
            }

            this.GridView2.DataSource = dtt;
            this.GridView2.DataBind();
        }
        else
        {
            GridView1.Visible = false;
            ViewState["pandaun"] = false;
            Pager1.Visible = false;
            GridView2.Visible = false;

            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000841", "没有相关记录！") + "')</script>");
        }
        Translations();
    }
    protected void BtnRelease_Click(object sender, EventArgs e)
    {
        if (ViewState["pandaun"] == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001461", "对不起，请在发放奖金之前显示要发放的奖金！") + "')</script>");
            return;
        }
        string grant = "公司发放";
        //发放当期奖金
        int countqishu = CommonDataBLL.getMaxqishu();
        Application.Lock();
        bool blean = PayMentBLL.UpMemberInfo(countqishu, double.Parse(jine.Text), grant, Request.UserHostAddress, Session["Company"].ToString());
        Application.UnLock();
        if (blean == false)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001376", "对不起，写入电子帐户失败，请联系维护人员！") + "')</script>");
            return;
        }

        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001464", "工资发放成功！") + "')</script>");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.dgDetails.AllowPaging = false;

        DataTable dt = PayMentBLL.GetMemberInfoByMoney(double.Parse(this.jine.Text));

        if (dt == null || dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000053", "没有数据，不能导出Excel！") + "')</script>");
            return;
        }
        if (dt.Rows.Count > 0)
        {
            dgDetails.DataSource = dt;
            dgDetails.DataBind();
        }


        dt.Columns.Remove("Number");
        dt.Columns.Remove("MobileTele");
        dt.Columns.Remove("name");
        dt.Columns.Remove("BankCard");
        dt.Columns.Add("remark");
        dt.Columns.Add("biaoshi");
        foreach (DataRow row in dt.Rows)
        {
            row["id"] = dt.Rows.IndexOf(row) + 1;
            row["remark"] = "1";
            row["bankbook"] = Encryption.Encryption.GetDecipherName(row["bankbook"].ToString());//解密收款银行户名
            row["strbankcard"] = Encryption.Encryption.GetDecipherCard(row["strbankcard"].ToString());//解密收款银行账号
        }

        DataTable dtt = DAL.DBHelper.ExecuteDataTable(ViewState["dtt"].ToString());
        string data = WriteDatagridToCsv(dtt, new string[] { GetTran("000613", "日期"), GetTran("000041", "总金额"), GetTran("006004", "总笔数"), GetTran("006005", "支付宝帐号(Email)") });
        data += WriteDatagridToCsv(dt, new string[] { GetTran("001442", "商户流水号"), GetTran("001443", "收款银行户名"), GetTran("001444", "收款银行账号"), GetTran("001446", "收款开户银行"), GetTran("001447", "收款银行所在省份"), GetTran("006006", "收款银行所在市"), GetTran("001451", "收款支行名称"), GetTran("000322", "金额"), GetTran("001453", "对公对私标志"), GetTran("000078", "备注") });

        string temp = string.Format("attachment;filename={0}", "FileName.csv");
        Response.ClearHeaders();
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.AppendHeader("Content-disposition", temp);
        Response.HeaderEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.Write(data);
        Response.End();

    }
    private string WriteDatagridToCsv(DataTable table, string[] str)
    {
        string[] strHead = str;
        StringBuilder sw = new StringBuilder();

        ////画表头
        for (int i = 0; i < strHead.Length; i++)
        {
            sw.Append(strHead[i]);
            sw.Append(",");
        }
        sw.Append("\n"); //画表体 
        for (int i = 0; i < table.Rows.Count; i++)
        {
            for (int j = 0; j < table.Columns.Count; j++)
            {
                if (j == 2)
                {
                    sw.Append("\t" + table.Rows[i][j].ToString().Trim());
                }
                else
                {
                    sw.Append(table.Rows[i][j].ToString().Trim());
                }
                sw.Append(",");
            }
            sw.Append("\n");
        }
        return sw.ToString();
    }
    private string DelQuota(string str)
    {
        string result = str; string[] strQuota = { "~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "`", ";", "'", ",", ".", "/", ":", "/,", "<", ">", "?" };
        for (int i = 0; i < strQuota.Length; i++)
        {
            if (result.IndexOf(strQuota[i]) > -1) result = result.Replace(strQuota[i], "");
        }
        return result;
    }


    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            (e.Row.FindControl("Label1") as Label).Text = Encryption.Encryption.GetDecipherName((e.Row.FindControl("Label1") as Label).Text.Trim());//解密开户名
            (e.Row.FindControl("Label2") as Label).Text = Encryption.Encryption.GetDecipherCard((e.Row.FindControl("Label2") as Label).Text.Trim().Trim());//解密卡号
            Label lbl = e.Row.FindControl("Label3") as Label;
            ViewState["byzj"] = Convert.ToDouble(ViewState["byzj"]) + Convert.ToDouble(lbl.Text.Trim());
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "本也总计";
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[7].Text = Convert.ToDouble(ViewState["byzj"]).ToString("f2");
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
        }
    }
    protected void dgDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("Label9") as Label).Text = Encryption.Encryption.GetDecipherName((e.Row.FindControl("Label9") as Label).Text.Trim());//解密开户名
            (e.Row.FindControl("Label10") as Label).Text = Encryption.Encryption.GetDecipherCard((e.Row.FindControl("Label10") as Label).Text.Trim().Trim());//解密卡号
        }
    }

    public void getTotal(double money)
    {
        Label4.Text = "汇兑金额总计：<font color='red'>" + RemittancesBLL.GetTotalMoney("Jackpot-MemberInfo.Out", "memberinfo", " where (Jackpot-Out)>=" + money) + "</font>";
    }

}