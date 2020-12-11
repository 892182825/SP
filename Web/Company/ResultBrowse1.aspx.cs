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
using BLL.MoneyFlows;
using Model.Other;
using System.Text;
using Model;
using BLL.Logistics;
using BLL.other.Company;
using System.Collections.Generic;
using DAL;

public partial class Company_ResultBrowse1 : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.ResultBrowse1);

        if (!IsPostBack)
        {
            this.Datepicker1.Text = CommonDataBLL.GetDateBegin().ToString();
            this.Datepicker2.Text = CommonDataBLL.GetDateEnd().ToString();
            //Label1.Text = "期末余额总计：<font color='red'>" + D_AccountBLL.GetTotalBalancemoney(1) + "</font>";
            GetShopList3();
        }

        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.RadioButtonList1,
                new string[][]{
                    new string []{"010005","消费账户明细"},
                    new string []{"000000","可用FTC账户明细"},
                      new string []{ "000000", "报单账户明细"},
                      new string []{ "000000", "保险账户明细"},
                      new string []{ "000000", "锁仓FTC账户明细"},
                       new string []{ "000000", "USDT账户明细"},
                });
        this.TranControls(this.GridView2,
                new string[][]{
                    new string []{"001195","编号"},
                    new string []{"006615","科目"},
                    new string []{"006581","发生时间"},
                    new string []{"000000","转入FTC"},
                    new string []{"000000","转出FTC"},
                    new string []{"000000","结余FTC"},
                    new string []{"006585","类型"},
                    new string []{"006616","摘要"}
                });
        TranControls(BtnConfirm, new string[][]{
            new string[]{"000340","查询"}
        });
    }

    protected object GetHDate(object obj)
    {
        if (obj != null)
        {//.AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours())
            try { return Convert.ToDateTime(obj).AddHours(8); }
            catch { }
        }
        return "";
    }

    //绑定会员现金账户明细
    private void GetShopList3()
    {
        zsf.Text = "";
        DateTime time = DateTime.Now.ToUniversalTime();
        bool b = true;
        StringBuilder condition = new StringBuilder();
        string table = " MemberAccount ";
        condition.Append("1=1");
        string number = "";
        if (TextBox1.Text.Trim() != "")
        {
            string sql = "select number from MemberInfo where MobileTele='" + TextBox1.Text + "'";
            DataTable shj = DBHelper.ExecuteDataTable(sql);
            if (shj.Rows.Count > 0)
            {
                number = shj.Rows[0][0].ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002096", "无此编号，请检查后再重新输入") + "！')</script>");
                return;
            }
            condition.Append(" and Number='" + number + "'");
        }
        condition.Append(" and sftype=0");
        string BeginRiQi = "";
        string EndRiQi = "";
        if (this.Datepicker1.Text != "")
        {
            b = DateTime.TryParse(this.Datepicker1.Text, out time);
            if (!b)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                return;
            }
            BeginRiQi = this.Datepicker1.Text.Trim().ToString();
            DisposeString.DisString(BeginRiQi, "'", "");
            if (this.Datepicker2.Text != "")
            {
                b = DateTime.TryParse(this.Datepicker2.Text, out time);
                if (!b)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = (DateTime.Parse(this.Datepicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                DisposeString.DisString(EndRiQi, "'", "");

                condition.Append(" and happentime>= '" + BeginRiQi + "' and happentime<='" + EndRiQi + "'");
            }
            else
            {
                condition.Append(" and happentime>= '" + BeginRiQi + "'");
            }
        }
        else
        {
            if (this.Datepicker2.Text != "")
            {
                b = DateTime.TryParse(this.Datepicker2.Text, out time);
                if (!b)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = (DateTime.Parse(this.Datepicker2.Text.Trim()).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                condition.Append(" and happentime<='" + EndRiQi + "'");
            }
        }
        
        
        string happenmoney = "happenmoney";
        string Balancemoney = "Balancemoney";
        string cloumns = "id,number,happentime,Direction,sftype,kmtype,remark" + "," + happenmoney + "," + Balancemoney;
        string key = "id";
        ViewState["key"] = key;
        ViewState["PageColumn"] = cloumns;
        ViewState["table"] = table;
        ViewState["condition"] = condition.ToString();
        this.GridView2.DataSourceID = null;
        this.Pager1.ControlName = "GridView2";
        this.Pager1.key = key;
        this.Pager1.PageColumn = cloumns;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = table;
        this.Pager1.Condition = condition.ToString();
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();
        Translations();



    }
    //绑定可用石斛积分账户明细
    private void GetShopList4()
    {
        zsf.Text = "";
        DateTime time = DateTime.Now.ToUniversalTime();
        bool b = true;
        StringBuilder condition = new StringBuilder();
        string table = " MemberAccount ";
        condition.Append("1=1");
        string number = "";
        if (TextBox1.Text.Trim() != "")
        {
            string sql = "select number from MemberInfo where MobileTele='" + TextBox1.Text + "'";
            DataTable shj = DBHelper.ExecuteDataTable(sql);
            if (shj.Rows.Count > 0)
            {
                number = shj.Rows[0][0].ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002096", "无此编号，请检查后再重新输入") + "！')</script>");
                return;
            }
            condition.Append(" and Number='" + number + "'");
        }
        condition.Append(" and sftype=1");
        string BeginRiQi = "";
        string EndRiQi = "";
        if (this.Datepicker1.Text != "")
        {
            b = DateTime.TryParse(this.Datepicker1.Text, out time);
            if (!b)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                return;
            }
            BeginRiQi = this.Datepicker1.Text.Trim().ToString();
            DisposeString.DisString(BeginRiQi, "'", "");
            if (this.Datepicker2.Text != "")
            {
                b = DateTime.TryParse(this.Datepicker2.Text, out time);
                if (!b)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = (DateTime.Parse(this.Datepicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                DisposeString.DisString(EndRiQi, "'", "");

                condition.Append(" and happentime>= '" + BeginRiQi + "' and happentime<='" + EndRiQi + "'");
            }
            else
            {
                condition.Append(" and happentime>= '" + BeginRiQi + "'");
            }
        }
        else
        {
            if (this.Datepicker2.Text != "")
            {
                b = DateTime.TryParse(this.Datepicker2.Text, out time);
                if (!b)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = (DateTime.Parse(this.Datepicker2.Text.Trim()).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                condition.Append(" and happentime<='" + EndRiQi + "'");
            }
        }
        condition.Append(" and Number!='8888888888'");
        string happenmoney = "happenmoney";
        string Balancemoney = "Balancemoney";
        string cloumns = "id,number,happentime,Direction,sftype,kmtype,remark" + "," + happenmoney + "," + Balancemoney;
        string key = "id";
        ViewState["key"] = key;
        ViewState["PageColumn"] = cloumns;
        ViewState["table"] = table;
        ViewState["condition"] = condition.ToString();
        this.GridView2.DataSourceID = null;
        this.Pager1.ControlName = "GridView2";
        this.Pager1.key = key;
        this.Pager1.PageColumn = cloumns;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = table;
        this.Pager1.Condition = condition.ToString();
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();
        Translations();
    }

    //绑定投资石斛积分账户明细
    private void GetShopList5()
    {
        DateTime time = DateTime.Now.ToUniversalTime();
        bool b = true;
        StringBuilder condition = new StringBuilder();
        string table = " MemberAccount ";
        condition.Append("1=1");
        string number = "";
        if (TextBox1.Text.Trim() != "")
        {
            string sql = "select number from MemberInfo where MobileTele='" + TextBox1.Text + "'";
            DataTable shj = DBHelper.ExecuteDataTable(sql);
            if (shj.Rows.Count > 0)
            {
                number = shj.Rows[0][0].ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002096", "无此编号，请检查后再重新输入") + "！')</script>");
                return;
            }
            condition.Append(" and Number='" + number + "'");
        }
        condition.Append(" and sftype=6");
        string BeginRiQi = "";
        string EndRiQi = "";
        if (this.Datepicker1.Text != "")
        {
            b = DateTime.TryParse(this.Datepicker1.Text, out time);
            if (!b)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                return;
            }
            BeginRiQi = this.Datepicker1.Text.Trim().ToString();
            DisposeString.DisString(BeginRiQi, "'", "");
            if (this.Datepicker2.Text != "")
            {
                b = DateTime.TryParse(this.Datepicker2.Text, out time);
                if (!b)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = (DateTime.Parse(this.Datepicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                DisposeString.DisString(EndRiQi, "'", "");

                condition.Append(" and happentime>= '" + BeginRiQi + "' and happentime<='" + EndRiQi + "'");
            }
            else
            {
                condition.Append(" and happentime>= '" + BeginRiQi + "'");
            }
        }
        else
        {
            if (this.Datepicker2.Text != "")
            {
                b = DateTime.TryParse(this.Datepicker2.Text, out time);
                if (!b)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = (DateTime.Parse(this.Datepicker2.Text.Trim()).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                condition.Append(" and happentime<='" + EndRiQi + "'");
            }
        }
       
        string happenmoney = "happenmoney";
        string Balancemoney = "Balancemoney";
        string cloumns = "id,number,happentime,Direction,sftype,kmtype,remark" + "," + happenmoney + "," + Balancemoney;
        string key = "id";
        ViewState["key"] = key;
        ViewState["PageColumn"] = cloumns;
        ViewState["table"] = table;
        ViewState["condition"] = condition.ToString();
        this.GridView2.DataSourceID = null;
        this.Pager1.ControlName = "GridView2";
        this.Pager1.key = key;
        this.Pager1.PageColumn = cloumns;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = table;
        this.Pager1.Condition = condition.ToString();
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();
        Translations();
    }

    //绑定奖励石斛积分账户明细
    private void GetShopList6()
    {
        DateTime time = DateTime.Now.ToUniversalTime();
        bool b = true;
        StringBuilder condition = new StringBuilder();
        string table = " MemberAccount ";
        condition.Append("1=1");
        string number = "";
        if (TextBox1.Text.Trim() != "")
        {
            string sql = "select number from MemberInfo where MobileTele='" + TextBox1.Text + "'";
            DataTable shj = DBHelper.ExecuteDataTable(sql);
            if (shj.Rows.Count > 0)
            {
                number = shj.Rows[0][0].ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002096", "无此编号，请检查后再重新输入") + "！')</script>");
                return;
            }
            condition.Append(" and Number='" + number + "'");
        }
        condition.Append(" and sftype=5");
        string BeginRiQi = "";
        string EndRiQi = "";
        if (this.Datepicker1.Text != "")
        {
            b = DateTime.TryParse(this.Datepicker1.Text, out time);
            if (!b)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                return;
            }
            BeginRiQi = this.Datepicker1.Text.Trim().ToString();
            DisposeString.DisString(BeginRiQi, "'", "");
            if (this.Datepicker2.Text != "")
            {
                b = DateTime.TryParse(this.Datepicker2.Text, out time);
                if (!b)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = (DateTime.Parse(this.Datepicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                DisposeString.DisString(EndRiQi, "'", "");

                condition.Append(" and happentime>= '" + BeginRiQi + "' and happentime<='" + EndRiQi + "'");
            }
            else
            {
                condition.Append(" and happentime>= '" + BeginRiQi + "'");
            }
        }
        else
        {
            if (this.Datepicker2.Text != "")
            {
                b = DateTime.TryParse(this.Datepicker2.Text, out time);
                if (!b)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = (DateTime.Parse(this.Datepicker2.Text.Trim()).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                condition.Append(" and happentime<='" + EndRiQi + "'");
            }
        }
       
        string happenmoney = "happenmoney";
        string Balancemoney = "Balancemoney";
        string cloumns = "id,number,happentime,Direction,sftype,kmtype,remark" + "," + happenmoney + "," + Balancemoney;
        string key = "id";
        ViewState["key"] = key;
        ViewState["PageColumn"] = cloumns;
        ViewState["table"] = table;
        ViewState["condition"] = condition.ToString();
        this.GridView2.DataSourceID = null;
        this.Pager1.ControlName = "GridView2";
        this.Pager1.key = key;
        this.Pager1.PageColumn = cloumns;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = table;
        this.Pager1.Condition = condition.ToString();
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();
        Translations();
    }
    private void GetShopList7()
    {
        DateTime time = DateTime.Now.ToUniversalTime();
        bool b = true;
        StringBuilder condition = new StringBuilder();
        string table = " MemberAccount ";
        condition.Append("1=1");
        string number = "";
        if (TextBox1.Text.Trim() != "")
        {
            string sql = "select number from MemberInfo where MobileTele='" + TextBox1.Text + "'";
            DataTable shj = DBHelper.ExecuteDataTable(sql);
            if (shj.Rows.Count > 0)
            {
                number = shj.Rows[0][0].ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002096", "无此编号，请检查后再重新输入") + "！')</script>");
                return;
            }
            condition.Append(" and Number='" + number + "'");
        }
        condition.Append(" and sftype=2");
        string BeginRiQi = "";
        string EndRiQi = "";
        if (this.Datepicker1.Text != "")
        {
            b = DateTime.TryParse(this.Datepicker1.Text, out time);
            if (!b)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                return;
            }
            BeginRiQi = this.Datepicker1.Text.Trim().ToString();
            DisposeString.DisString(BeginRiQi, "'", "");
            if (this.Datepicker2.Text != "")
            {
                b = DateTime.TryParse(this.Datepicker2.Text, out time);
                if (!b)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = (DateTime.Parse(this.Datepicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                DisposeString.DisString(EndRiQi, "'", "");

                condition.Append(" and happentime>= '" + BeginRiQi + "' and happentime<='" + EndRiQi + "'");
            }
            else
            {
                condition.Append(" and happentime>= '" + BeginRiQi + "'");
            }
        }
        else
        {
            if (this.Datepicker2.Text != "")
            {
                b = DateTime.TryParse(this.Datepicker2.Text, out time);
                if (!b)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = (DateTime.Parse(this.Datepicker2.Text.Trim()).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                condition.Append(" and happentime<='" + EndRiQi + "'");
            }
        }
       
        string happenmoney = "happenmoney";
        string Balancemoney = "Balancemoney";
        string cloumns = "id,number,happentime,Direction,sftype,kmtype,remark" + "," + happenmoney + "," + Balancemoney;
        string key = "id";
        ViewState["key"] = key;
        ViewState["PageColumn"] = cloumns;
        ViewState["table"] = table;
        ViewState["condition"] = condition.ToString();
        this.GridView2.DataSourceID = null;
        this.Pager1.ControlName = "GridView2";
        this.Pager1.key = key;
        this.Pager1.PageColumn = cloumns;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = table;
        this.Pager1.Condition = condition.ToString();
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();
        Translations();
    }
    private void GetShopList8()
    {
        DateTime time = DateTime.Now.ToUniversalTime();
        bool b = true;
        StringBuilder condition = new StringBuilder();
        string table = " MemberAccount ";
        condition.Append("1=1");
        string number = "";
        if (TextBox1.Text.Trim() != "")
        {
            string sql = "select number from MemberInfo where MobileTele='" + TextBox1.Text + "'";
            DataTable shj = DBHelper.ExecuteDataTable(sql);
            if (shj.Rows.Count > 0)
            {
                number = shj.Rows[0][0].ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002096", "无此编号，请检查后再重新输入") + "！')</script>");
                return;
            }
            condition.Append(" and Number='" + number + "'");
        }
        condition.Append(" and sftype=8");
        string BeginRiQi = "";
        string EndRiQi = "";
        if (this.Datepicker1.Text != "")
        {
            b = DateTime.TryParse(this.Datepicker1.Text, out time);
            if (!b)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                return;
            }
            BeginRiQi = this.Datepicker1.Text.Trim().ToString();
            DisposeString.DisString(BeginRiQi, "'", "");
            if (this.Datepicker2.Text != "")
            {
                b = DateTime.TryParse(this.Datepicker2.Text, out time);
                if (!b)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = (DateTime.Parse(this.Datepicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                DisposeString.DisString(EndRiQi, "'", "");

                condition.Append(" and happentime>= '" + BeginRiQi + "' and happentime<='" + EndRiQi + "'");
            }
            else
            {
                condition.Append(" and happentime>= '" + BeginRiQi + "'");
            }
        }
        else
        {
            if (this.Datepicker2.Text != "")
            {
                b = DateTime.TryParse(this.Datepicker2.Text, out time);
                if (!b)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = (DateTime.Parse(this.Datepicker2.Text.Trim()).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                condition.Append(" and happentime<='" + EndRiQi + "'");
            }
        }

        string happenmoney = "happenmoney";
        string Balancemoney = "Balancemoney";
        string cloumns = "id,number,happentime,Direction,sftype,kmtype,remark" + "," + happenmoney + "," + Balancemoney;
        string key = "id";
        ViewState["key"] = key;
        ViewState["PageColumn"] = cloumns;
        ViewState["table"] = table;
        ViewState["condition"] = condition.ToString();
        this.GridView2.DataSourceID = null;
        this.Pager1.ControlName = "GridView2";
        this.Pager1.key = key;
        this.Pager1.PageColumn = cloumns;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = table;
        this.Pager1.Condition = condition.ToString();
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();
        Translations();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            try
            {
                e.Row.Cells[2].Text = DateTime.Parse(e.Row.Cells[2].Text).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();
            }
            catch
            {
            }
        }
        Translations();
    }

    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        if (this.RadioButtonList1.SelectedValue == "0")
        {
            //消费账户明细
            GetShopList3();
        }
        if (this.RadioButtonList1.SelectedValue == "1")
        {//可用石斛积分账户明细
            GetShopList4();
        }

        if (this.RadioButtonList1.SelectedValue == "6")
        {//投资石斛积分账户明细
            GetShopList5();
        }
        if (this.RadioButtonList1.SelectedValue == "5")
        {
            //奖励石斛积分账户明细
            GetShopList6();
        }
        if (this.RadioButtonList1.SelectedValue == "2")
        {
            //奖励石斛积分账户明细
            GetShopList7();
        }
        if (this.RadioButtonList1.SelectedValue == "8")
        {
            //奖励石斛积分账户明细
            GetShopList8();
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.RadioButtonList1.SelectedValue == "0")
        {
            //消费账户明细
            GetShopList3();
        }
        if (this.RadioButtonList1.SelectedValue == "1")
        {//可用石斛积分账户明细
            GetShopList4();
        }

        if (this.RadioButtonList1.SelectedValue == "6")
        {//投资石斛积分账户明细
            GetShopList5();
        }
        if (this.RadioButtonList1.SelectedValue == "5")
        {
            //奖励石斛积分账户明细
            GetShopList6();
        }
        if (this.RadioButtonList1.SelectedValue == "2")
        {
            //奖励石斛积分账户明细
            GetShopList7();
        }
        if (this.RadioButtonList1.SelectedValue == "8")
        {
            //奖励石斛积分账户明细
            GetShopList8();
        }

    }
    /// <summary>
    /// 导出excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Download_Click(object sender, System.EventArgs e)
    {
        DataTable dt1 = DAL.DBHelper.ExecuteDataTable("select  Balancemoney-happenmoney as qcje," + ViewState["PageColumn"].ToString() + " from " + ViewState["table"].ToString() + " where " + ViewState["condition"].ToString());
        if (dt1.Rows.Count == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000053", "没有数据，不能导出Excel！") + "')</script>");
            return;
        }
        DataTable dt = new DataTable();
        dt = dt1.Clone();
        dt.Columns["kmtype"].DataType = typeof(String);
        foreach (DataRow r in dt1.Rows)
        {
            DataRow newrow = dt.NewRow();
            newrow["Number"] = r["Number"];
            newrow["kmtype"] = D_AccountBLL.GetKmtype(r["kmtype"].ToString());
            newrow["happentime"] = GetHDate(r["happentime"].ToString());
            newrow["qcje"] = r["qcje"];
            newrow["happenmoney"] = r["happenmoney"];
            newrow["Balancemoney"] = r["Balancemoney"];
            newrow["remark"] = getMark(r["remark"].ToString());
            dt.Rows.Add(newrow);
        }
        if (this.RadioButtonList1.SelectedValue == "0")
        {
            Excel.OutToExcel(dt, GetTran("010005","消费账户明细"), new string[]{
                "number="+GetTran("001195", "编号"),"kmtype="+GetTran("006615", "科目"),"happentime="+GetTran("006581", "发生时间"),
            "qcje="+GetTran("006605","期初金额"),"happenmoney="+GetTran("006582", "发生金额"),"Balancemoney="+GetTran("006583", "账户余额"),"remark="+GetTran("006616", "摘要")
            });
        }
        else if (this.RadioButtonList1.SelectedValue == "1")
        {
            Excel.OutToExcel(dt, GetTran("010006", "可用石斛积分账户明细"), new string[]{
                "number="+GetTran("001195", "编号"),"kmtype="+GetTran("006615", "科目"),"happentime="+GetTran("006581", "发生时间"),
            "qcje="+GetTran("006605","期初金额"),"happenmoney="+GetTran("006582", "发生金额"),
            "Balancemoney="+GetTran("006583", "账户余额"),"remark="+GetTran("006616", "摘要")
            });
        }

        else if (this.RadioButtonList1.SelectedValue == "4")
        {
            Excel.OutToExcel(dt, GetTran("010007", "投资石斛积分账户明细"), new string[]{
                "number="+GetTran("001195", "编号"),"kmtype="+GetTran("006615", "科目"),"happentime="+GetTran("006581", "发生时间"),
            "qcje="+GetTran("006605","期初金额"),"happenmoney="+GetTran("006582", "发生金额"),
            "Balancemoney="+GetTran("006583", "账户余额"),"remark="+GetTran("006616", "摘要")
            });
        }

        else if (this.RadioButtonList1.SelectedValue == "5")
        {
            Excel.OutToExcel(dt, GetTran("010008", "奖励石斛积分账户明细"), new string[]{
                "number="+GetTran("001195", "编号"),"kmtype="+GetTran("006615", "科目"),"happentime="+GetTran("006581", "发生时间"),
            "qcje="+GetTran("006605","期初金额"),"happenmoney="+GetTran("006582", "发生金额"),
            "Balancemoney="+GetTran("006583", "账户余额"),"remark="+GetTran("006616", "摘要")
            });
        }
    }

    protected string getMark(string remark)
    {
        string res = "";
        if (remark.IndexOf('~') > 0)
        {
            for (int i = 0; i < remark.Split('~').Length; i++)
            {
                res += GetTran(remark.Split('~')[i], "") == "" ? remark.Split('~')[i] : GetTran(remark.Split('~')[i], "");
            }
        }
        else
        {
            res = remark;
        }
        return res;
    }
}
