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

using BLL.other.Company;

using Model.Other;
using BLL.Registration_declarations;
using System.Text;

public partial class Company_OrderDetail : BLL.TranslationBase
{
    private void Page_Load(object sender, System.EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        this.Btn_Detail.Click += new System.EventHandler(this.Btn_Detail_Click);

        if (!IsPostBack)
        {
            Response.Cache.SetExpires(DateTime.Now);
            Permissions.CheckManagePermission(EnumCompanyPermission.ReportOrderDetail);

            DateTime dt = DateTime.Now;
            string t = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();

            this.DatePicker1.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            this.DatePicker2.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Btn_Detail_Click(null, null);
            Bind();

        }

        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        AddOrderBLL.BindCurrency_Rate(Dropdownlist1);
        Dropdownlist1.SelectedValue = CountryBLL.GetCurrency();

        // 在此处放置用户代码以初始化页面
        Session["language"] = LanguageBLL.GetDefaultLanguageTableName();
        Session["LanguegeSelect"] = LanguageBLL.GetDefaultlLanguageName();


        Translations();

    }

    public void Btn_Detail_Click(object sender, System.EventArgs e)
    {
        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000465", "请输入要查询的日期区间！") + "!')</script>");
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
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000468", "日期格式不正确！")+"')</script>");
                return;
            }
            Session["Begin"] = this.DatePicker1.Text+" 00:00:00";
            Session["End"] = this.DatePicker2.Text+" 23:59:59";
            Session["bianhao"] = this.Tbx_num.Text;

            Bind();
        }
    }

    private void Bind()
    {

        string condition = "";
        if (Tbx_num.Text.Trim()!="")
            condition = "A.storeid=B.storeid and A.storeid='" + Session["bianhao"] + "' and B.Number=C.Number and A.orderdatetime between '" + Session["Begin"] + "' and '" + Session["End"] + "'";
        else
            condition = "A.storeid=B.storeid  and B.Number=C.Number and A.orderdatetime between '" + Session["Begin"] + "' and '" + Session["End"] + "'";

        Pager1.PageBind(0, 10, "StoreOrder A ,StoreInfo B ,MemberInfo C ", @"a.storeid,c.name,a.ExpectNum,a.storeorderid,
                    a.GeneOutBillPerson,a.orderdatetime,a.totalmoney,a.totalpv,a.ischeckout,a.isgeneoutbill,a.issent,a.isreceived
                    ", condition, "a.StoreOrderID", "DataGrid1", "a.OrderDateTime", 0);

        string str = GetZJE(" and " + condition);
        if (str != "") {
            try
            {
                lab_cjezj.Text = double.Parse(str.Split(',')[0]).ToString();
                lab_cjfzj.Text = double.Parse(str.Split(',')[1]).ToString();
            }
            catch {
                lab_cjezj.Text = "未知";
                lab_cjfzj.Text = "未知";
            }
        }

        Translations();

        ViewState["SQLSTR"] = "select a.storeid,c.name,a.ExpectNum,a.storeorderid, a.GeneOutBillPerson,a.orderdatetime,a.totalmoney,a.totalpv,a.ischeckout,a.isgeneoutbill,a.issent,a.isreceived from StoreOrder A ,StoreInfo B ,MemberInfo C where " + condition;

    }

    /// <summary>
    /// 获得总金额和总积分
    /// </summary>
    /// <param name="sqltj">查询条件</param>
    /// <returns></returns>
    private static string GetZJE(string sqltj)
    {
        string zjef = "";
        DataTable dtTotal = DAL.DBHelper.ExecuteDataTable("select sum(a.totalmoney) as totalmoney,sum(a.totalpv) as totalpv from StoreOrder A ,StoreInfo B ,MemberInfo C where 1=1 " + sqltj);
        zjef = dtTotal.Rows[0]["totalmoney"].ToString() + "," + dtTotal.Rows[0]["totalpv"].ToString();
        return zjef;

    }


    public string GetBiaoZhunTime(string dt)
    {
        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }

    private void Translations()
    {
        this.TranControls(this.Btn_Detail, new string[][] { new string[] { "000048", "查 询" } });
        this.TranControls(this.DataGrid1, new string[][] 
                            { 
                                new string[] { "000012", "序号" },
                            new string[] { "000150", "服务机构编号" },
                            new string[] { "000039", "机构负责人姓名" },
                            new string[] { "000045", "期数" },
                            new string[] { "000079", "订单号" },
                            new string[] { "002158", "出库单号" },
                            new string[] { "000735", "订单日期" },
                            new string[] { "000322", "金额" },
                            new string[] { "000414", "积分" },
                            new string[] { "002152", "是否支付" },
                            new string[] { "002147", "是否出库" },
                            new string[] { "000328", "是否发货" },
                            new string[] { "001848", "是否收货" }
                     
                            });
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
   
    protected void DataGrid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
          
        }
    }

    public void OutToExcel_Click(object sender, EventArgs e)
    {
        string cmd = ViewState["SQLSTR"].ToString();
        DataTable dt = DAL.DBHelper.ExecuteDataTable(cmd);
        dt.Columns.Add("IsPay", Type.GetType("System.String"));
        dt.Columns.Add("IsOut", Type.GetType("System.String"));
        dt.Columns.Add("IsSend", Type.GetType("System.String"));
        dt.Columns.Add("IsReceive", Type.GetType("System.String"));
        if (dt == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('没有数据，不能导出Excel！')</script>");
            return;
        }
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('没有数据，不能导出Excel！')</script>");
            return;
        }
        Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xls");
        Response.ContentEncoding = Encoding.UTF8;


        for (int i = 0; i < dt.Rows.Count; i++) //foreach (DataRow row in dt.Rows)
        {
            dt.Rows[i]["Name"] = Encryption.Encryption.GetDecipherName(Convert.ToString(dt.Rows[i]["Name"]));
            dt.Rows[i]["orderdatetime"] = GetBiaoZhunTime(dt.Rows[i]["orderdatetime"].ToString());
            dt.Rows[i]["IsPay"] = dt.Rows[i]["ischeckout"].ToString() == "Y" ? GetTran("000233", "是") : GetTran("000235", "否");
            dt.Rows[i]["IsOut"] = dt.Rows[i]["isgeneoutbill"].ToString() == "Y" ? GetTran("000233", "是") : GetTran("000235", "否");
            dt.Rows[i]["IsSend"] = dt.Rows[i]["issent"].ToString() == "Y" ? GetTran("000233", "是") : GetTran("000235", "否");
            dt.Rows[i]["IsReceive"] = dt.Rows[i]["isreceived"].ToString() == "Y" ? GetTran("000233", "是") : GetTran("000235", "否");
        }
        StringBuilder sb = Excel.GetExcelTable(dt, GetTran("003040", "服务机构订单明细"), new string[] { "storeid=" + GetTran("000150", "店铺编号"), "Name=" + GetTran("000039", "店长姓名"), "ExpectNum=" + GetTran("000045", "期数"), "storeorderid=" + GetTran("000079", "订单号"), "GeneOutBillPerson=" + GetTran("002158", "出库单号"), "ExpectNum=" + GetTran("000735", "订单日期"), "totalmoney=" + GetTran("000322", "金额"), "totalpv=" + GetTran("000414", "积分"), "IsPay=" + GetTran("002152", "是否支付"), "IsOut=" + GetTran("002147", "是否出库"), "IsSend=" + GetTran("000328", "是否发货"), "IsReceive=" + GetTran("001848", "是否收货") });

        Response.Write(sb.ToString());

        Response.Flush();
        Response.End();
    }
}



