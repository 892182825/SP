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
public partial class Company_MemberSPOffView : BLL.TranslationBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.MemberSPOffView);

        if (!this.IsPostBack)
        {
            btnSeach_Click(null, null);

        }

        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.DropDownList1,
                new string[][]{
                    new string []{"000633","全部"},
                    new string []{"007542","已冻结"},
                    new string []{"001287","已恢复"}
                });
        this.TranControls(this.givTWmember,
                new string[][]{
                    new string []{"000024","会员编号"},
                    new string []{"000025","会员姓名"},
                    new string []{"000000","冻结期数"},
                    new string []{"000000","冻结日期"},
                    new string []{"004134","操作员编号"},
                    new string []{"007272","操作员姓名"},
                    new string []{"007560","冻结状态"},
                    new string []{"007561","冻结原因"},
                });

        this.TranControls(this.btnWL, new string[][] { new string[] { "007562", "会员冻结" } });

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
        sb.Append("1=1 and  mp.number=mi.number ");

        string mb = DisposeString.DisString(txt_member.Text);
        if (mb.Length > 0)
        {
            sb.Append(" and mobiletele like '%" + PageBase.InputText(this.txt_member.Text.Trim()) + "%'");

        }
        if (txtName.Text.Length>0)
        {
            sb.Append(" and name like '%" + PageBase.InputText(this.txtName.Text.Trim()) + "%'");
        }


        if (this.DatePicker2.Text != "")
        {
            sb.Append(" and year(zxdate)=year('" + Convert.ToDateTime(PageBase.InputText(this.DatePicker2.Text)).ToUniversalTime() + "') and month(zxdate)=month('" + Convert.ToDateTime(PageBase.InputText(this.DatePicker2.Text)).ToUniversalTime() + "') and day(zxdate)=day('" + Convert.ToDateTime(PageBase.InputText(this.DatePicker2.Text)).AddDays(1).ToUniversalTime() + "') ");
        }


        UserControl_ExpectNum exp = Page.FindControl("ExpectNum1") as UserControl_ExpectNum;
        int ExpectNum = exp.ExpectNum;
        if (ExpectNum > 0)
        {
            sb.Append(" and zxqishu=" + ExpectNum);
        }

        if (this.DropDownList1.SelectedValue != "0")
        {
            sb.Append(" and iszx=" + PageBase.InputText(this.DropDownList1.SelectedValue));
        }

        string col = "mi.mobiletele,mp.Name,mp.Number,zxqishu,zxdate,case when iszx=1 then '" + GetTran("007542", "已冻结") + "' when iszx=2 then '" + GetTran("001287", "已恢复") + "' else '' end as zx,offReason,Operator,OperatorName";
        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.PageBind(0, 10, "MemberOffSP  mp ,memberinfo  mi", col, sb.ToString(), "mp.id", "givTWmember");

        ViewState["SQLSTR"] = "select " + col + " from MemberOffSP where " + sb.ToString();


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

        dt.Columns.Add(new DataColumn("petName", typeof(string)));
        foreach (DataRow row in dt.Rows)
        {
            row["petName"] = GetbyName(row["Number"].ToString());
            row["name"] = Encryption.Encryption.GetDecipherName(Convert.ToString(row["name"]));
            row["zxdate"] = Convert.ToDateTime(row["zxdate"]).AddHours(Convert.ToDouble(Session["WTH"]));

        }

        Excel.OutToExcel(dt, GetTran("007562", "冻结会员"), new string[] { "Number=" + GetTran("000024", "会员编号") + "", "petName=" + GetTran("000025", "会员姓名") + "", "zxqishu=" + GetTran("001289", "冻结期数") + "", "zxdate=" + GetTran("001292", "冻结日期") + "", "Operator=" + GetTran("004134", "操作人编号") + "", "OperatorName=" + GetTran("007272", "操作人姓名") + "", "zx=" + GetTran("001277", "冻结状态") + "", "offReason=" + GetTran("007561", "冻结原因") + "" });




    }
    protected void btnWL_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberOffSP.aspx");
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
