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
using Model.Other;
using DAL;
using BLL.other.Company;
using BLL.CommonClass;
using System.Data.SqlClient;

public partial class Company_MemberRestoreSPOff : BLL.TranslationBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.MemberRestoreSPoff);

        if (!this.IsPostBack)
        {
            btnSeach_Click(null, null);

        }

        Translations();
    }

    private void Translations()
    {

        this.TranControls(this.givTWmember,
                new string[][]{
                    new string []{"007271","解冻会员账户"},
                    new string []{"000024","会员编号"},
                    new string []{"000025","会员姓名"},
                    new string []{"000000","冻结期数"},
                    new string []{"000000","冻结日期"},
                    new string []{"007272","操作人"},
                    new string []{"000000","冻结状态"},
                    new string []{"000078","备注"}
                });

        this.TranControls(this.btnSeach, new string[][] { new string[] { "000048", "查询" } });


    }

    protected string GetbyName(string number)
    {
        string name = MemberOffDAL.getMemberNameSP(number);
        return Encryption.Encryption.GetDecipherName(name);
    }

    protected string GetbyDateTime(string zxdate)
    {

        return Convert.ToDateTime(zxdate).AddHours(Convert.ToDouble(Session["WTH"])).ToString("yyyy-MM-dd HH:mm:ss");
    }


    protected void btnSeach_Click(object sender, EventArgs e)
    {

        StringBuilder sb = new StringBuilder();
        sb.Append("1=1 and iszx=1 and type=0");

        string mb = DisposeString.DisString(txt_member.Text);
        if (mb.Length > 0)
        {
            sb.Append(" and Number like '%" + PageBase.InputText(this.txt_member.Text.Trim()) + "%'");

        }


        if (this.DatePicker2.Text != "")
        {
            sb.Append(" and year(zxdate)=year('" + PageBase.InputText(this.DatePicker2.Text) + "') and month(zxdate)=month('" + PageBase.InputText(this.DatePicker2.Text) + "') and day(zxdate)=day('" + PageBase.InputText(this.DatePicker2.Text) + "') ");
        }

        UserControl_ExpectNum exp = Page.FindControl("ExpectNum1") as UserControl_ExpectNum;
        int ExpectNum = exp.ExpectNum;
        if (ExpectNum > 0)
        {
            sb.Append(" and zxqishu=" + ExpectNum);
        }
        ViewState["SQLSTR"] = "select id,iszx,offReason,OperatorName,Name,Number,zxqishu,zxdate,case when iszx=1 then '" + GetTran("000000", "已冻结") + "' when iszx=2 then '" + GetTran("001287", "已恢复") + "' else '' end as zx from MemberoffSP ,memberinfobalance" + BLL.CommonClass.CommonDataBLL.GetMaxqishu() + " as b where " + sb.ToString();

        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.PageBind(0, 10, "MemberOffSP ", "id,iszx,MemberOffSP.Name,offReason,Operator,MemberOffSP.Number,zxqishu,zxdate,case when iszx=1 then '" + GetTran("000000", "已冻结") + "' when iszx=2 then '" + GetTran("001287", "已恢复") + "' else '' end as zx", sb.ToString(), "zxdate", "givTWmember");

    }


    protected void givTWmember_RowDataBound(object sender, GridViewRowEventArgs e)
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
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000672", "没有数据，不能导出Excel") + "！')</script>");
            return;
        }
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000672", "没有数据，不能导出Excel") + "！')</script>");
            return;
        }
        DataTable dt1 = dt.Clone();

        foreach (DataRow row in dt.Rows)
        {
            DataRow dr = dt1.NewRow();
            dr["Name"] = Encryption.Encryption.GetDecipherName(row["Name"].ToString());
            try
            {
                dr["zxdate"] = Convert.ToDateTime(row["zxdate"]).AddHours(Convert.ToDouble(Session["WTH"])).ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch
            { }
            dr["Number"] = row["Number"].ToString();
            dr["zxqishu"] = row["zxqishu"].ToString();
            dr["zx"] = row["zx"].ToString();
            dt1.Rows.Add(dr);
        }

        StringBuilder str = Excel.GetExcelTable(dt1, GetTran("000000", "会员冻结"), new string[] { 
            "Number=" + GetTran("000024", "会员编号") + "", "Name=" + GetTran("000025", "会员姓名") + "", 
            "zxqishu=" + GetTran("000000", "冻结期数") + "", "zxdate=" + GetTran("000000", "冻结日期") + "", 
            "zx=" + GetTran("000000", "冻结状态") + "" });
        Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xls");
        Response.ContentEncoding = Encoding.GetEncoding("gb2312");
        Response.Write(str.ToString());
        Response.Flush();
        Response.End();
    }
    protected void btnWL_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberOffSP.aspx");
    }
    protected void givTWmember_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string[] args = e.CommandArgument.ToString().Split(':');
        if (args.Length != 3)
        {
            ScriptHelper.SetAlert(Page, GetTran("001089", "数据异常！"));
            return;
        }

        if (args[2].Trim() == "2")
        {
            ScriptHelper.SetAlert(Page, GetTran("001334", "会员已经恢复冻结！"));
            return;
        }

        int id = Convert.ToInt32(args[0]);
        string number = args[1];

        if (e.CommandName == "OK")
        {
            Response.Redirect("MemberOffSP.aspx?type=1&id=" + id + "&number=" + number);
            //string reason = "取消冻结";
            //string opert = Session["company"].ToString();
            //string opname = "管理员";

            //int insertCon = MemberOffBLL.getUpdateMemberSPZX(number, CommonDataBLL.getMaxqishu(), id, PageBase.GetUtcNowTime(), reason, opert, opname);


            //if (insertCon > 0)
            //{
            //    ScriptHelper.SetAlert(Page, GetTran("001338", "确定完成"));
            //}
            //else
            //{
            //    ScriptHelper.SetAlert(Page, GetTran("007132", "恢复冻结失败"));
            //}
            //btnSeach_Click(null, null);

        }

    }

    protected string ShowOffReason(object obj)
    {
        string rtval = "";
        if (obj == null || obj.ToString() == "")
        {
            rtval = GetTran("000221", "无");
        }
        else
        {
            rtval = "<a href='#' onclick='showControl(event,\"divOffReason\",\"" + obj.ToString() + "\")'>" + GetTran("000440", "查看") + "</a>";
        }
        return rtval;
    }
}
