using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL.CommonClass;
using System.Text;
using Model.Other;

public partial class AccountDetail_AccountDetail : BLL.TranslationBase
{
    protected int bzCurrency = 0;
    protected double currency = 0;
    protected string happenmoney = "";
    protected string Balancemoney = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        ViewState["Kmtype"] = (Request.QueryString["type"] == "" ? "" : Request.QueryString["type"]);
        String str = ViewState["Kmtype"].ToString();
        if (ViewState["Kmtype"].ToString() == "AccountXJ" || ViewState["Kmtype"].ToString() == "AccountXF" || ViewState["Kmtype"].ToString() == "AccountFX" || ViewState["Kmtype"].ToString() == "AccountFXth"
            || ViewState["Kmtype"].ToString() == "AccountZSJF" || ViewState["Kmtype"].ToString() == "AccountSFJF")
        {
            Top.Visible = true;
            bottom.Visible = true;
            SLeft1.Visible = false;
            STop1.Visible = false;
            Permissions.ThreeRedirect(Page, "../member/" + Permissions.redirUrl);
        }
        else { Top.Visible = false; bottom.Visible = false; Permissions.ThreeRedirect(Page, "../store/" + Permissions.redirUrl); }
        if (!IsPostBack)
        {

            GetKmtype();
            DataBind();
        }
        translation();
    }

    private void translation()
    {
        TranControls(ddl_OutIn, new string[][] { new string[] { "000633", "全部" }, new string[] { "006600", "转入" }, new string[] { "007637", "转出" } });
        TranControls(btn_zuotian, new string[][] { new string[] { "007639", "前天" } });
        TranControls(btn_jintian, new string[][] { new string[] { "007640", "昨天" } });
        TranControls(btn_mingtian, new string[][] { new string[] { "004251", "今天" } });
        btn_kmtype.Value = GetTran("007650", "选择/修改");
        TranControls(btn_serach, new string[][] { new string[] { "000011", "搜索" } });
    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    private void DataBind()
    {
        StringBuilder sb = new StringBuilder();
        string table = "";

        //会员
        if (ViewState["Kmtype"].ToString() == "AccountXJ")//现金账户
        { table = "memberaccount"; sb.Append("and number='" + Session["Member"].ToString() + "' and SfType=1 and happenmoney!=0"); }
        else if (ViewState["Kmtype"].ToString() == "AccountXF")//消费账户
        { table = "memberaccount"; sb.Append("and number='" + Session["Member"].ToString() + "' and SfType=0 and happenmoney!=0"); }
        else if (ViewState["Kmtype"].ToString() == "AccountFX")//复消账户
        { table = "memberaccount"; sb.Append("and number='" + Session["Member"].ToString() + "' and SfType=2 and happenmoney!=0"); }
        else if (ViewState["Kmtype"].ToString() == "AccountFXth")//复消提货账户
        { table = "memberaccount"; sb.Append("and number='" + Session["Member"].ToString() + "' and SfType=3 and happenmoney!=0"); }
        else if (ViewState["Kmtype"].ToString() == "AccountZSJF")//赠送积分
        { table = "memberaccount"; sb.Append("and number='" + Session["Member"].ToString() + "' and SfType=4 and happenmoney!=0"); }
        else if (ViewState["Kmtype"].ToString() == "AccountSFJF")//释放积分
        { table = "memberaccount"; sb.Append("and number='" + Session["Member"].ToString() + "' and SfType=5 and happenmoney!=0"); }

        //店铺
        else if (ViewState["Kmtype"].ToString() == "AccountDH")//订货款账户
        { table = "storeaccount"; sb.Append("and number='" + Session["Store"].ToString() + "' and SfType=0 and happenmoney!=0"); }
        else if (ViewState["Kmtype"].ToString() == "AccountZZ")//周转款账户
        { table = "storeaccount"; sb.Append("and number='" + Session["Store"].ToString() + "' and SfType=1 and happenmoney!=0"); }

        string BeginRiQi = "";
        string EndRiQi = "";

        string kmtype = Request.Form["chb_name"];
        if (kmtype != null)
        {
            sb.Append(" and kmtype in(" + kmtype + ")");
        }
        if (ddl_OutIn.SelectedValue != "-1")
        {
            sb.Append(" and Direction=" + ddl_OutIn.SelectedValue);
        }

        if (this.Datepicker1.Text != "")
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            bool b = DateTime.TryParse(Datepicker1.Text.Trim(), out time);
            if (!b)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                return;
            }
            BeginRiQi = this.Datepicker1.Text.Trim().ToString();
            DisposeString.DisString(BeginRiQi, "'", "");
            if (this.Datepicker2.Text != "")
            {
                b = DateTime.TryParse(Datepicker2.Text.Trim(), out time);
                if (!b)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = (DateTime.Parse(this.Datepicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                DisposeString.DisString(EndRiQi, "'", "");

                sb.Append(" and happentime>= '" + BeginRiQi + "' and happentime<='" + EndRiQi + "'");
            }
            else
            {
                sb.Append(" and happentime>= '" + BeginRiQi + "'");
            }
        }
        else
        {
            if (this.Datepicker2.Text != "")
            {
                DateTime time = DateTime.Now.ToUniversalTime();
                bool b = DateTime.TryParse(Datepicker2.Text.Trim(), out time);
                if (!b)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                    return;
                }
                EndRiQi = (DateTime.Parse(this.Datepicker2.Text.Trim()).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
                sb.Append(" and happentime<='" + EndRiQi + "'");
            }
        }
        ViewState["Sql"] = "select isnull(sum(ABS(isnull(happenmoney,0))),0) as happenmoney from " + table + " where 1=1  " + sb.ToString();
        //获取汇率
        currency = AjaxClass.GetCurrency(int.Parse(Session["Default_Currency"] == null ? bzCurrency.ToString() : Session["Default_Currency"].ToString()));
        //转入0  转出1  
        happenmoney = "happenmoney*" + currency + " as happenmoney";
        //结余金额
        string Balancemoney = "Balancemoney*" + currency + " as Balancemoney";
        //
        string cloumns = "id,number,happentime,Direction,sftype,kmtype,remark" + "," + happenmoney + "," + Balancemoney;
        string key = "id";
        if (table != "")
        {
            lit_heji.Text = GetHappenMoney(ViewState["Sql"].ToString());
            string sql = "select " + cloumns + " from " + table + " where 1=1  " + sb.ToString() + " order by " + key + " desc ";
            this.ucPagerMb1.PageSize = 10;
            this.ucPagerMb1.PageInit(sql, rep_km.UniqueID);
        }
        else { Response.Redirect("../member/index.aspx"); }
    }
    /// <summary>
    /// 获取总入，总出
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    private string GetHappenMoney(string sql)
    {
        string money = "0.00";

        if (ddl_OutIn.SelectedValue != "-1")
        {
            money = DAL.DBHelper.ExecuteScalar(sql, CommandType.Text).ToString();
            if (currency != 1)
            {
                if (ddl_OutIn.SelectedValue == "0")
                {
                    money = GetTran("006600", "转入") + "：<span style=\"color:red\">" + (double.Parse(money) * currency).ToString("0.00") + "</span>";
                }
                else if (ddl_OutIn.SelectedValue == "1")
                {
                    money = GetTran("007637", "转出") + "：<span style=\"color:red\">" + Math.Abs(double.Parse(money) * currency).ToString("0.00") + "</span>";
                }
            }
            else
            {
                if (ddl_OutIn.SelectedValue == "0")
                {
                    money = GetTran("006600", "转入") + "：<span style=\"color:red\">" + (double.Parse(money)).ToString("0.00") + "</span>";
                }
                else if (ddl_OutIn.SelectedValue == "1")
                {
                    money = GetTran("007637", "转出") + "：<span style=\"color:red\">" + Math.Abs(double.Parse(money)).ToString("0.00") + "</span>";
                }
            }

        }
        else
        {
            if (currency != 1)
            {
                double money1 = double.Parse(DAL.DBHelper.ExecuteScalar(sql + " and Direction=0", CommandType.Text).ToString());
                double money2 = Math.Abs(double.Parse(DAL.DBHelper.ExecuteScalar(sql + " and direction=1", CommandType.Text).ToString()));
                money = "<b>" + GetTran("006600", "转入") + "</b>" + "：<span style=\"color:red\">" +
                    (money1 * currency).ToString("0.00") + "</span>" + "&nbsp;&nbsp;" + "<b>" + GetTran("007637", "转出") +
                    "</b>" + "：<span style=\"color:red\">" + (money2 * currency).ToString("0.00") + "</span>" + "&nbsp;&nbsp;" + "<b>" +
                    GetTran("006583", "账户余额") + "</b>" + "：<span style=\"color:red\">" + ((money1 - money2) * currency).ToString("0.00") + "</span>";
            }
            else
            {
                double money1 = double.Parse(DAL.DBHelper.ExecuteScalar(sql + " and Direction=0", CommandType.Text).ToString());
                double money2 = Math.Abs(double.Parse(DAL.DBHelper.ExecuteScalar(sql + " and direction=1", CommandType.Text).ToString()));
                money = "<b>" + GetTran("006600", "转入") + "</b>" + "：<span style=\"color:red\">" +
                    (money1).ToString("0.00") + "</span>" + "&nbsp;&nbsp;" + "<b>" + GetTran("007637", "转出") +
                    "</b>" + "：<span style=\"color:red\">" + (money2).ToString("0.00") + "</span>" + "&nbsp;&nbsp;" + "<b>" +
                    GetTran("006583", "账户余额") + "</b>" + "：<span style=\"color:red\">" + ((money1 - money2)).ToString("0.00") + "</span>";
            }

        }
        return money;
    }

    /// <summary>
    /// 绑定科目列表
    /// </summary>
    private void GetKmtype()
    {
        DataTable dt = CommonDataBLL.GetKmtype(ViewState["Kmtype"].ToString());
        rep_kmtype.DataSource = dt;
        rep_kmtype.DataBind();
    }
    //前天
    protected void btn_zuotian_Click(object sender, EventArgs e)
    {
        this.Datepicker1.Text = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
        this.Datepicker2.Text = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
        DataBind();
    }
    //昨天
    protected void btn_jintian_Click(object sender, EventArgs e)
    {
        this.Datepicker1.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
        this.Datepicker2.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
        DataBind();
    }
    //今天
    protected void btn_mingtian_Click(object sender, EventArgs e)
    {
        this.Datepicker1.Text = DateTime.Now.ToString("yyyy-MM-dd");
        this.Datepicker2.Text = DateTime.Now.ToString("yyyy-MM-dd");
        DataBind();
    }
    protected void btn_serach_Click(object sender, EventArgs e)
    {
        DataBind();
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
            //res = GetTran( remark,"");
            res = GetTran(remark, remark);
        }
        return res;
    }
}