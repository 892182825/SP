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
public partial class Company_ResultBrowse2 : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.ResultBrowse2);
        if (!IsPostBack)
        {
            this.Datepicker1.Text = CommonDataBLL.GetDateBegin().ToString();
            this.Datepicker2.Text = CommonDataBLL.GetDateEnd().ToString();
            GetShopList2();
        }

        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.RadioButtonList2,
               new string[][]{
                    new string []{"009004","服务机构订货款明细"},
                    new string []{"009003","服务机构周转款明细"},
                });
        this.TranControls(this.GridView2,
                new string[][]{
                    new string []{"001195","编号"},
                    new string []{"006615","科目"},
                    new string []{"006581","发生时间"},
                    new string []{"007275","转入金额"},
                    new string []{"001630","转出金额"},
                    new string []{"007276","结余金额"},
                    new string []{"006585","类型"},  
                    new string []{"006616","摘要"}
                });
        TranControls(BtnConfirm, new string[][]{
            new string[]{"000340","查询"}
        });
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
    //绑定会员汇款
    private void GetShopList2()
    {
        StringBuilder condition = new StringBuilder();
        string table = " StoreAccount ";
        condition.Append(" 1=1 ");
        if (this.TxtBh.Text.Trim() != "")
        {
            condition.Append( " and Number='" + this.TxtBh.Text.Trim() + "'");
        }
        //condition.Append(" and sftype=" + (int)D_AccountSftype.StoreType + "");
        string BeginRiQi = "";
        string EndRiQi = "";
        if (this.Datepicker1.Text != "")
        {
            DateTime time1 = DateTime.Now.ToUniversalTime();
            bool bb = DateTime.TryParse(this.Datepicker2.Text, out time1);
            if (!bb)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000827", "时间格式不正确！") + "')</script>");
                return;
            }
            BeginRiQi = this.Datepicker1.Text.Trim().ToString();
            DisposeString.DisString(BeginRiQi, "'", "");
            if (this.Datepicker2.Text != "")
            {
                DateTime time = DateTime.Now.ToUniversalTime();
                bool b = DateTime.TryParse(this.Datepicker2.Text, out time);
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
                DateTime time = DateTime.Now.ToUniversalTime();
                bool b = DateTime.TryParse(this.Datepicker2.Text, out time);
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
        GetShopList2();
    }

    //绑定会员现金账户明细
    private void GetShopList3()
    {
        DateTime time = DateTime.Now.ToUniversalTime();
        bool b = true;
        StringBuilder condition = new StringBuilder();
        string table = " StoreAccount ";
        condition.Append("1=1");
        if (TxtBh.Text.Trim() != "")
        {
            condition.Append(" and Number='" + TxtBh.Text.Trim() + "'");
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

    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.RadioButtonList2.SelectedValue == "1")
        {
            GetShopList2();
        }
        if (this.RadioButtonList2.SelectedValue == "2")
        {
            GetShopList3();
        }

    }

    /// <summary>
    /// 导出excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Download_Click(object sender, System.EventArgs e)
    {
        DataTable dt1 = DAL.DBHelper.ExecuteDataTable("select Balancemoney-happenmoney as qcje," + ViewState["PageColumn"].ToString() + " from " + ViewState["table"].ToString() + " where " + ViewState["condition"].ToString());
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
            newrow["happentime"] = DateTime.Parse(r["happentime"].ToString()).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();
            newrow["qcje"] = r["qcje"];
            newrow["happenmoney"] = r["happenmoney"];
            newrow["Balancemoney"] = r["Balancemoney"];
            newrow["remark"] = r["remark"];
            dt.Rows.Add(newrow);
        }
        Excel.OutToExcel(dt, GetTran("007130", "服务机构报单账户明细"), new string[]{
            "number="+GetTran("001195", "编号"),"kmtype="+GetTran("006615", "科目"),"happentime="+GetTran("006581", "发生时间"),
            "qcje="+GetTran("006605","期初金额"),"happenmoney="+GetTran("006582", "发生金额"),"Balancemoney="+GetTran("006583", "账户余额"),"remark="+GetTran("006616", "摘要")
        });
    }
}
