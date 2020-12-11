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

public partial class Company_StoreRestoreoff : BLL.TranslationBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.StoreRestoreoff);

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
                    new string []{"001322","恢复注销"},
                    new string []{"000037", "服务机构编号"},
                    new string []{"000000","店长姓名"},
                    new string []{"001289","注销期数"},
                    new string []{"001292","注销日期"},
                    new string []{"001277","注销状态"}
                });

        this.TranControls(this.btnSeach, new string[][] { new string[] { "000048", "查询" } });


    }

    protected string GetbyName(string number)
    {
        string name = StoreOffDAL.getMemberName(number);
        return Encryption.Encryption.GetDecipherName(name);
    }

    protected string GetbyDateTime(string zxdate)
    {

        return Convert.ToDateTime(zxdate).AddHours(Convert.ToDouble(Session["WTH"])).ToString("yyyy-MM-dd HH:mm:ss");
    }


    protected void btnSeach_Click(object sender, EventArgs e)
    {

        StringBuilder sb = new StringBuilder();
        sb.Append("1=1 and iszx=1 and type=0 ");

        string mb = DisposeString.DisString(txtStoreid.Text);
        if (mb.Length > 0)
        {
            sb.Append(" and storeid like '%" + PageBase.InputText(this.txtStoreid.Text.Trim()) + "%'");

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

        ViewState["SQLSTR"] = "select id,iszx,storeid,name,storeoff.Number,zxqishu,zxdate,case when iszx=1 then '" + GetTran("001286", "已注销") + "' when iszx=2 then '" + GetTran("001287", "已恢复") + "' else '' end as zx from storeoff where " + sb.ToString();

        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.PageBind(0, 10, "storeoff", "id,iszx,storeid,name,storeoff.Number,zxqishu,zxdate,case when iszx=1 then '" + GetTran("001286", "已注销") + "' when iszx=2 then '" + GetTran("001287", "已恢复") + "' else '' end as zx", sb.ToString(), "zxdate", "givTWmember");

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


        foreach (DataRow row in dt.Rows)
        {
            row["Name"] = Encryption.Encryption.GetDecipherName(row["Name"].ToString());
            try
            {
                row["zxdate"] = Convert.ToDateTime(row["zxdate"]).AddHours(Convert.ToDouble(Session["WTH"])).ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch
            { }
        }
        Excel.OutToExcel(dt, GetTran("000000", "恢复服务机构注销"), new string[] { "storeid=" + GetTran("000037", "服务机构编号") + "", "Name=" + GetTran("000107", "店长姓名") + "", "zxqishu=" + GetTran("001289", "注销期数") + "", "zxdate=" + GetTran("001292", "注销日期") + "", "zx=" + GetTran("001277", "注销状态") + "" });


    }
    protected void btnWL_Click(object sender, EventArgs e)
    {
        Response.Redirect("StoreOff.aspx");
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
            ScriptHelper.SetAlert(Page, GetTran("000000", "服务机构已经恢复注销！"));
            return;
        }

        int id = Convert.ToInt32(args[0]);
        string storeid = args[1];
        string Operator = Session["Company"].ToString();
        string OperatorName = DataBackupBLL.GetNameByAdminID(Operator);

        if (e.CommandName == "OK")
        {



            int insertCon = StoreOffBLL.getUpdateStoreZX(storeid, CommonDataBLL.getMaxqishu(), id, DateTime.UtcNow, Operator, OperatorName);


            if (insertCon > 0)
            {
                ScriptHelper.SetAlert(Page, GetTran("001338", "确定完成"));
            }
            else
            {
                ScriptHelper.SetAlert(Page, GetTran("007132", "恢复注销失败"));
            }
            btnSeach_Click(null, null);

        }

    }
}
