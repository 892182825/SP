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

public partial class Company_DayPrice : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceReceiveDayPrice);
        if (!IsPostBack)
        {
            this.Datepicker1.Text = CommonDataBLL.GetDateBegin().ToString();
            this.Datepicker2.Text = CommonDataBLL.GetDateEnd().ToString();
           
            GetShopList3();
        }
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
    //绑定汇率历史记录
    private void GetShopList3()
    {
        DateTime time = DateTime.Now.ToUniversalTime();
        bool b = true;
        StringBuilder condition = new StringBuilder();
        string table = " DayPrice ";
        condition.Append("1=1");

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

                condition.Append(" and convert(datetime,NowDate, 20)>= '" + BeginRiQi + "' and convert(datetime,NowDate, 20)<='" + EndRiQi + "'");
            }
            else
            {
                condition.Append(" and convert(datetime,NowDate, 20)>= '" + BeginRiQi + "'");
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
                condition.Append(" and convert(datetime,NowDate, 20)<='" + EndRiQi + "'");
            }
        }

        
        string cloumns = " * ";
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
        
    }

     /// <summary>
    /// 导出excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Download_Click(object sender, System.EventArgs e)
    {
        DataTable dt1 = DAL.DBHelper.ExecuteDataTable("select " + ViewState["PageColumn"].ToString() + " from " + ViewState["table"].ToString() + " where " + ViewState["condition"].ToString());
        if (dt1.Rows.Count == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000053", "没有数据，不能导出Excel！") + "')</script>");
            return;
        }
        DataTable dt = new DataTable();
        dt = dt1.Clone();

        foreach (DataRow r in dt1.Rows)
        {
            DataRow newrow = dt.NewRow();

            newrow["NowDate"] = GetHDate(r["NowDate"].ToString());
            newrow["NowPrice"] = r["NowPrice"];
            newrow["Addrate"] = r["Addrate"];

            dt.Rows.Add(newrow);
        }

        Excel.OutToExcel(dt, "石斛积分价格历史记录", new string[]{"NowDate=日期","NowPrice=价格"," Addrate=增长率"
            });

    }
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        GetShopList3();
    }
}