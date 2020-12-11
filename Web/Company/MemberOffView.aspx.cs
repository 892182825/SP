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

public partial class Company_MemberOffView : BLL.TranslationBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.MemberOffList);

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
                    new string []{"001286","已注销"},
                    new string []{"001287","已恢复"}
                });
        this.TranControls(this.givTWmember,
                new string[][]{
                    new string []{"000035","详细信息"},
                    new string []{"000024","会员编号"},
                    new string []{"000107","姓名"},
                    new string []{"001289","注销期数"},
                    new string []{"001292","注销日期"},
                    new string []{"004134","操作员编号"},
                    new string []{"007191","操作员姓名"},
                    new string []{"001277","注销状态"},
                    new string []{"007164","注销原因"},
                    
                });

        this.TranControls(this.btnWL, new string[][] { new string[] { "001282", "会员注销" } });

        this.TranControls(this.btnSeach, new string[][] { new string[] { "000048", "查询" } });
    }

    protected string GetbyName(string number)
    {
        string name = MemberOffDAL.getMemberName(number);
        return Encryption.Encryption.GetDecipherName(name);
    }

    protected string GetbyDateTime(string zxdate)
    {

        return Convert.ToDateTime(zxdate).AddHours(Convert.ToDouble(Session["WTH"])).ToString("yyyy-MM-dd HH:mm:ss");
    }


    protected void btnSeach_Click(object sender, EventArgs e)
    {

        StringBuilder sb = new StringBuilder();
        sb.Append("1=1");

        string mb = DisposeString.DisString(txt_member.Text);
        if (mb.Length > 0)
        {
            sb.Append(" and Number like '%" + PageBase.InputText(this.txt_member.Text.Trim()) + "%'");

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
            sb.Append(" and iszx="+PageBase.InputText(this.DropDownList1.SelectedValue));
        }

        string col = "MemberOff.Name,MemberOff.Number,zxqishu,zxdate,case when iszx=1 then '" + GetTran("001286", "已注销") + "' when iszx=2 then '" + GetTran("001287", "已恢复") + "' else '' end as zx,offReason,Operator,OperatorName";
        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.PageBind(0, 10, "MemberOff ", col, sb.ToString(), "zxdate", "givTWmember");

        ViewState["SQLSTR"] = "select " + col + " from memberoff where " + sb.ToString()+" order by zxdate desc";

        
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
            
                row["name"] = Encryption.Encryption.GetDecipherName(Convert.ToString(row["name"]));
                row["zxdate"] = Convert.ToDateTime(row["zxdate"]).AddHours(Convert.ToDouble(Session["WTH"]));
            
        }

       Excel.OutToExcel(dt, GetTran("001282", "注销会员"), new string[] { "Number=" + GetTran("000024", "会员编号") + "", "name=" + GetTran("000107", "姓名") + "", "zxqishu=" + GetTran("001289", "期数") + "", "zxdate=" + GetTran("001292", "日期") + "", "zx=" + GetTran("001277", "注销状态") + "" });

        

       
    }
    protected void btnWL_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberOff.aspx");
    }

    protected void lbtn_Select_Click(object sender, CommandEventArgs e)
    {
        string Number = e.CommandName;
        Response.Redirect("DisplayMemberDeatail.aspx?ID=" + Number);
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
            rtval = "<a href='#' onclick='showControl(event,\"divOffReason\",\""+obj.ToString()+"\")'>" + GetTran("000440", "查看") + "</a>";
        }
        return rtval;
    }
   
}
