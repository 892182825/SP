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
using Model.Other;
using BLL.other.Company;
using BLL.Logistics;
using Model;

public partial class Company_MoneyDetailReport : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        // 权限设置
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerStoreHuikuanmingxi);
        // 在此处放置用户代码以初始化页面
        
        if (!IsPostBack)
        {
            Session["language"] = LanguageBLL.GetDefaultLanguageTableName();
            Session["LanguegeSelect"] = LanguageBLL.GetDefaultlLanguageName();

            this.DatePicker1.Text = CommonDataBLL.GetDateBegin().ToString();
            this.DatePicker2.Text = CommonDataBLL.GetDateEnd().ToString();
            Bind();
        }
        CommonDataBLL.setlanguage();
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.DataGrid1,
                   new string[][]{
                    new string []{"000012","序号"},
                    new string []{"000150","店铺编号"},
                    new string []{"000039","店长姓名"},
                    
                    new string []{"000045","期数"},
                    new string []{"000322","金额"},
                    new string []{"000562","币种"},

                    new string []{"000588","用途"},
                    new string []{"000591","收款日期"},
                    new string []{"000186","支付方式"},

                    new string []{"000519","经办人"},
                    new string []{"000595","确认方式"},
                    new string []{"000597","汇单号"},

                    new string []{"000570","汇出日期"},
                    new string []{"000600","汇出银行"},
                    new string []{"000601","汇入银行"},

                    new string []{"000602","汇款人"},
                    new string []{"000603","汇出金额"},
                    new string []{"000604","汇出币种"},
                
                    new string []{"000605","是否审核"},
                    new string []{"000078","备注"}
                });
        this.TranControls(this.Btn_Detail, new string[][] { new string[] { "000048", "查 询" } });

    }
    protected void Btn_Detail_Click(object sender, System.EventArgs e)
    {
        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000465", "请输入要查询的日期区间！") + "')</script>");
        }
        else
        {
            try
            {
                DateTime d1 = Convert.ToDateTime(this.DatePicker1.Text);
                DateTime d2 = Convert.ToDateTime(this.DatePicker2.Text);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000468", "日期格式不正确！") + "')</script>");
                return;
            }
            Bind();
            //Session["Begin"] = DateTime.Parse(this.DatePicker1.Text).ToString();
            //Session["End"] = (DateTime.Parse(this.DatePicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
            //Session["bianhao"] = this.Tbx_num.Text;
            //Response.Write("<script lanauage='javascript'>window.open('HuikuanDetail.aspx?Flag=1')</script>");
        }
    }













    private void Bind()
    {
        string Begin = DateTime.Parse(this.DatePicker1.Text).ToString();
        string End = (DateTime.Parse(this.DatePicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
        string cloumns = "A.*,B.ExpectNum,C.name";
        string table = "Remittances A ,StoreInfo B ,MemberInfo C";
        string condition = "";
        if (this.Tbx_num.Text != "")
        {
            condition = "A.remitnumber=B.storeid and B.storeid='" + this.Tbx_num.Text + "' and B.Number=C.Number and A.RemittancesDate>='" + Begin + "' and A.RemittancesDate<='" + End + "'";
        }
        else
        {
            condition = "A.remitnumber=B.storeid and B.Number=C.Number and A.RemittancesDate>='" + Begin + "' and A.RemittancesDate<='" + End + "'";
        }
        string key = "A.RemittancesDate";

        this.DataGrid1.DataSourceID = null;
        this.Pager1.ControlName = "DataGrid1";
        this.Pager1.key = key;
        this.Pager1.PageColumn = cloumns;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = table;
        this.Pager1.Condition = condition.ToString();
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();

        string str = GetZJE(" and " + condition);
        if (str != "") {
            try
            {
                lab_cjezj.Text = double.Parse(str.Split(',')[0]).ToString();
                lab_chcjezj.Text = double.Parse(str.Split(',')[0]).ToString();
            }
            catch {
                lab_chcjezj.Text = "未知";
                lab_cjezj.Text = "未知";
            }
        }

        //dt = CommonDataBLL.RemittancesSumDetail(this.Tbx_num.Text, DateTime.Parse(this.DatePicker1.Text).ToString(), (DateTime.Parse(this.DatePicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString());
        //ViewState["dt"] = dt;
        //if (dt.Rows.Count < 1)
        //{
        //    this.lbl_message.Text = GetTran("000634", "没有相关信息!!");
        //    this.lbl_message.Visible = true;
        //}
        //else
        //{
        //    this.DataGrid1.DataSource = dt;
        //    this.DataGrid1.DataBind();
        //}

    }

    /// <summary>
    /// 获得总金额和总汇出金额
    /// </summary>
    /// <param name="sqltj">查询条件</param>
    /// <returns></returns>
    private static string GetZJE(string sqltj)
    {
        string zjef = "";
        DataTable dtTotal = DAL.DBHelper.ExecuteDataTable("select sum(A.RemitMoney) as RemitMoney,sum(A.RemittancesMoney) as RemittancesMoney from Remittances A ,StoreInfo B ,MemberInfo C where 1=1 " + sqltj);
        zjef = dtTotal.Rows[0]["RemitMoney"].ToString() + "," + dtTotal.Rows[0]["RemittancesMoney"].ToString();
        return zjef;

    }

    protected string getstr(string str)
    {
        if (str == "0")
        {
            str = " &nbsp;";
            return str;
        }
        else
        {
            int index = str.IndexOf(".");
            if (index == -1)
            {
                return str + ".00";
            }
            else
            {
                if (str.Length < index + 3)
                {
                    str = str.Substring(0, index + 2);
                }
                else
                {
                    str = str.Substring(0, index + 3);

                }
                return str;
            }
        }
    }
    //double money = 0.0;
    protected void DataGrid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //money += double.Parse((e.Row.FindControl("Label1") as Label).Text);
            if (e.Row.Cells[18].Text.ToLower() == "true")
            {
                e.Row.Cells[18].Text = GetTran("000233", "是");
            }
            else
            {
                e.Row.Cells[18].Text = GetTran("000235", "否");
            }
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            ((Label)e.Row.FindControl("lbl_num")).Text = Convert.ToString(e.Row.RowIndex + 1);
            e.Row.Cells[5].Text = CommonDataBLL.GetLanguageStr(int.Parse(e.Row.Cells[5].Text), "Currency", "Name");
            e.Row.Cells[17].Text = CommonDataBLL.GetLanguageStr(int.Parse(e.Row.Cells[17].Text), "Currency", "Name");
            //switch (e.Row.Cells[5].Text)
            //{
            //    case "1": e.Row.Cells[5].Text = "美元"; break;
            //    case "2": e.Row.Cells[5].Text = "人民币"; break;
            //    case "3": e.Row.Cells[5].Text = "泰珠"; break;
            //    case "4": e.Row.Cells[5].Text = "英磅"; break;
            //    default: e.Row.Cells[5].Text = ""; break;
            //}
            switch (e.Row.Cells[6].Text)
            {
                case "0": e.Row.Cells[6].Text = GetTran("000000", "会员现金账户"); break;
                case "1": e.Row.Cells[6].Text = GetTran("000000", "会员消费账户"); break;
                case "10": e.Row.Cells[6].Text = GetTran("000000", "店铺订货款"); break;
                case "11": e.Row.Cells[6].Text = GetTran("000000", "店铺周转款"); break;
                default: e.Row.Cells[6].Text = ""; break;
            }
            //switch (e.Row.Cells[10].Text)
            //{
            //    case "1": e.Row.Cells[10].Text = GetTran("000643", "传真"); break;
            //    case "2": e.Row.Cells[10].Text = GetTran("000644", "核实"); break;
            //    case "3": e.Row.Cells[10].Text = GetTran("000646", "电话"); break;
            //    case "4": e.Row.Cells[10].Text = GetTran("000647", "透支"); break;
            //}
            //switch (e.Row.Cells[8].Text)
            //{
            //    case "0": e.Row.Cells[8].Text = GetTran("005962", "公司录入"); break;
            //    case "1": e.Row.Cells[8].Text = GetTran("001582", "银行汇款"); break;
            //    case "2": e.Row.Cells[8].Text = GetTran("005963", "在线支付"); break;
            //    case "3": e.Row.Cells[8].Text = GetTran("005964", "奖金转入"); break;
            //}
            e.Row.Cells[8].Text = D_AccountBLL.GetPaymentstr((PaymentEnum)int.Parse(e.Row.Cells[8].Text));
            //e.Row.Cells[8].Text = CommonDataBLL.GetpaymentName(int.Parse(e.Row.Cells[8].Text), 1);
            e.Row.Cells[2].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[2].Text);//解密店长姓名
            e.Row.Cells[15].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[15].Text);//解密汇款人
            if (e.Row.Cells[7].Text != "")
            {
                try
                {
                    e.Row.Cells[7].Text = DateTime.Parse(e.Row.Cells[7].Text).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
                }
                catch
                {
                }
            }
            if (e.Row.Cells[12].Text != "")
            {
                try
                {
                    e.Row.Cells[12].Text = DateTime.Parse(e.Row.Cells[12].Text).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
                }
                catch
                {
                }
            }
        }
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    //double total = 0;
        //    //foreach (DataRow row in dt.Rows)
        //    //{
        //    //    total += double.Parse(row["RemitMoney"].ToString());
        //    //}
        //    e.Row.Cells[0].Text = GetTran("000630", "合计");
        //    e.Row.Cells[4].Text = getstr(money.ToString());
        //}

    }
    protected void lkSubmit1_Click(object sender, EventArgs e)
    {
        Btn_Detail_Click(null,null);
    }
}
