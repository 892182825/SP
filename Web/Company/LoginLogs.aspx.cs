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

public partial class Company_LoginLogs : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);


        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            BtnConfirm_Click(null, null);
        }
    }
    protected object GetRDate(object obj)
    {
        if (obj != null)
        {
            DateTime dt;
            bool b = DateTime.TryParse(obj.ToString(), out dt);
            if (b)
            {
                return dt.AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours());
            }
            else
            {
                return "";
            }
        }
        return "";
    }

    private void Translations()
    {

        TranControls(GridView1, new string[][] 
                        { 
                                new string[] { "000000", "编号" }, 
                                new string[] { "000000", "输入信息" },
                                new string[] { "000000", "类型" },
                                new string[] { "000000", "时间" },
                                new string[] { "000000", "IP地址" },
                                new string[] { "000000", "状态" }
                        }
                    );

        TranControls(BtnConfirm, new string[][] 
                        { 
                                new string[] { "000048", "状态" }
                        }
                    );

    }
    //查询按钮
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        getMemberInfo();
    }

    private string GetNumber()
    {
        string manageID = Session["Company"].ToString();
        int count = 0;
        string number = BLL.CommonClass.CommonDataBLL.GetWLNumber(manageID, out count);
        if (count == 0)
        {
            return " ";
        }
        return number;
    }

    #region
    public void getMemberInfo()
    {

        StringBuilder sb = new StringBuilder();
        sb.Append(" 1=1 ");
        if (this.Number.Text.Length > 0)
        {
            sb.Append(" and Number like'%" + this.Number.Text.Trim() + "%'");
        }
        if (this.pass.Text.Length > 0)
        {
            sb.Append(" and pass like'%" + this.pass.Text.Trim() + "%'");
        }
        if (this.txtip.Text.Length > 0)
        {
            sb.Append(" and loginip like'%" + this.txtip.Text.Trim() + "%'");
        }

        if (this.ddlChangeType.SelectedValue != "-1")
        {
            if (this.ddlChangeType.SelectedValue == "1")
            {
                sb.Append(" and leixing='Company'");
            }
            else if (this.ddlChangeType.SelectedValue == "2")
            {
                sb.Append(" and leixing='Store'");
            }
            else if (this.ddlChangeType.SelectedValue == "3")
            {
                sb.Append(" and leixing='Member'");
            }
        }

        if (this.DropDownList1.SelectedValue != "-1")
        {
            if (this.DropDownList1.SelectedValue == "1")
            {
                sb.Append(" and iscg=1");
            }
            else if (this.DropDownList1.SelectedValue == "2")
            {
                sb.Append(" and iscg=2");
            }

        }
        ///时间条件
        if (txtDate.Text.Trim() != "")
        {
            if (txtDate2.Text.Trim() != "")
            {
                sb.Append(" and DateAdd(day,DateDiff(day,0,[logindate]),0)>='" + txtDate.Text.Trim().Replace("'", "") + "' and DateAdd(day,DateDiff(day,0,[logindate]),0)<='" + txtDate2.Text.Trim().Replace("'", "") + "'");

            }
            else
            {
                ///注意And前有空格
                sb.Append(" and DateAdd(day,DateDiff(day,0,[logindate]),0)='" + txtDate.Text.Trim().Replace("'", "") + "'");
            }
        }


        //if (Tele.Length > 0)
        //{
        //    sb.Append(" and HomeTele ='" + Tele + "' or OfficeTele='" + Tele + "' or MobileTele='" + Tele + "' or FaxTele='" + Tele + "'");
        //}


        //sb.Append(" and " + GetNumber());
        ViewState["SQLSTR"] = "SELECT *,case iscg when 1 then '正确' else '错误' end as zt,case leixing when 'Company' then '公司' when 'Store' then '服务中心' else '会员' end as lx  FROM loginlog WHERE " + sb.ToString() + " order by id desc";
        string asg = ViewState["SQLSTR"].ToString();
        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.Pageindex = 0;
        pager.PageSize = 10;
        pager.PageTable = "loginlog";
        pager.Condition = sb.ToString();
        pager.PageColumn = " *,case iscg when 1 then '正确' else '错误' end as zt,case leixing when 'Company' then '公司' when 'Store' then '服务中心' else '会员' end as lx ";
        pager.ControlName = "GridView1";
        pager.key = " ID ";
        pager.InitBindData = true;
        pager.PageBind();
        Translations();
    }
    #endregion
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }

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
        Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xls");
        Response.ContentEncoding = Encoding.UTF7;


        foreach (DataRow row in dt.Rows)
        {
            DateTime dtime;
            bool b = DateTime.TryParse(row["logindate"].ToString(), out dtime);
            if (b)
            {
                dtime.AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours());
                row["logindate"] = dtime;
            }
        }
        StringBuilder sb = Excel.GetExcelTable(dt, "登录日志", new string[] { "Number=" + "编号", "pass=" + "输入信息", "lx=" + "类型", "logindate=" + "时间", "loginIP=" + "IP地址", "zt=" + "状态" });
        Response.Write(sb.ToString());

        Response.Flush();
        Response.End();

    }
}
