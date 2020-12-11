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

public partial class Company_twQuery : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.BalanceDefault);

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
                    new string []{"000654","调网类型"},
                    new string []{"000024","会员编号"},
                    new string []{"000025","会员姓名"},
                    new string []{"000659","原位置"},
                    new string []{"007138","原位置姓名"},
                    new string []{"000660","新位置"},
                    new string []{"007139","新位置姓名"},
                    new string []{"000661","调网期数"},
                    new string []{"000613","日期"},
                    new string []{"000662","操作员"}
                });
        this.TranControls(this.DropDownList1,
                new string[][]{
                    new string []{"000024","会员编号"},
                    new string []{"000662","操作员"}
                });
        this.TranControls(this.DropDownList2,
                new string[][]{
                    new string []{"000633","全部"},
                    new string []{"000663","安置"},
                    new string []{"000667","推荐"}
                });

        this.TranControls(this.btnWL, new string[][] { new string[] { "000621", "网络调整" } });
        this.TranControls(this.btnSeach, new string[][] { new string[] { "000048", "查询" } });
       
    }

    protected string GetbyDateTime(string AdjustDate)
    {

        return PageBase.GetbyDT(AdjustDate);
        
    }

    private string GetNumber(string tName)
    {
        string manageID = Session["Company"].ToString();
        int count = 0;
        string number = BLL.CommonClass.CommonDataBLL.GetWLNumber(manageID, out count,tName);
        if (count == 0)
        {
            return tName+".number in ('')";
        }
        return number;
    }

    protected void btnSeach_Click(object sender, EventArgs e)
    {
       
        StringBuilder sb = new StringBuilder();
        sb.Append("1=1");


        if (txt_member.Text.Trim().Length > 0)
        {
            if (this.DropDownList1.SelectedValue == "0")
            {
                sb.Append(" and nj.Number like '%" + PageBase.InputText(this.txt_member.Text.Trim()) + "%'");
            }
            else if (this.DropDownList1.SelectedValue == "1")
            {
                sb.Append(" and operateNum like '%" + PageBase.InputText(this.txt_member.Text.Trim()) + "%'");
            }

        }

        
            if (this.DropDownList2.SelectedValue == "1")
            {
                sb.Append(" and type%2=1");//奇数是安置
            }
            else if (this.DropDownList2.SelectedValue == "2")
            {
                sb.Append(" and type%2=0");
            }


        if (this.DatePicker2.Text != "")
        {
            sb.Append(" and year(AdjustDate)=year('" + Convert.ToDateTime(PageBase.InputText(this.DatePicker2.Text)) + "') and month(AdjustDate)=month('" + Convert.ToDateTime(PageBase.InputText(this.DatePicker2.Text)) + "') and day(AdjustDate)=day('" + Convert.ToDateTime(PageBase.InputText(this.DatePicker2.Text)) + "') ");
        }
        UserControl_ExpectNum exp = Page.FindControl("ExpectNum1") as UserControl_ExpectNum;
        int ExpectNum = exp.ExpectNum;
        if (ExpectNum > 0)
        {
            sb.Append(" and nj.ExpectNum=" + ExpectNum);
        }

        //sb.Append(" and " + GetNumber("nj"));

        ViewState["SQLSTR"] = "select nj.Number,Original,NewLocation,nj.ExpectNum,AdjustDate,OperateNum,m1.name as name1,(select [name] from memberinfo where number=nj.Original) as name2,(select [name] from memberinfo where number=nj.NewLocation) as name3,case when type%2=1 then '" + GetTran("000663", "安置") + "' when type%2=0 then '" + GetTran("000667", "推荐") + "' else '' end as lx from NetAdjustmentHistory nj inner join memberinfo m1 on nj.number=m1.number where " + sb.ToString();

        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.PageBind(0, 10, "NetAdjustmentHistory nj inner join memberinfo m1 on nj.number=m1.number", " nj.Number,Original,NewLocation,nj.ExpectNum,AdjustDate,OperateNum,m1.name as name1,(select [name] from memberinfo where number=nj.Original) as name2,(select [name] from memberinfo where number=nj.NewLocation) as name3,case when type%2=1 then '" + GetTran("000663", "安置") + "' when type%2=0 then '" + GetTran("000667", "推荐") + "' else '' end as lx", sb.ToString(), "nj.id", "givTWmember");

        
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
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('"+GetTran("000672", "没有数据，不能导出Excel")+"！')</script>");
            return;
        }
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000672", "没有数据，不能导出Excel") + "！')</script>");
            return;
        }

        DateTime dtime1 = PageBase.GetUtcNowTime();
        foreach (DataRow row in dt.Rows)
        {
            DateTime.TryParse(PageBase.GetDTbyStr(row["AdjustDate"].ToString()).ToString(), out dtime1);
            row["AdjustDate"] = dtime1;

            row["name1"] = Encryption.Encryption.GetDecipherName(row["name1"].ToString());
            row["name2"] = Encryption.Encryption.GetDecipherName(row["name2"].ToString());
            row["name3"] = Encryption.Encryption.GetDecipherName(row["name3"].ToString());

        }

        Excel.OutToExcel(dt, GetTran("000772", "会员调网"), new string[] { "lx=" + GetTran("000654", "调网类型") + "", "Number=" + GetTran("000024", "会员编号") + "", "Name1=" + GetTran("000025", "会员姓名") + "", "Original=" + GetTran("000659", "原位置") + "", "Name2=" + GetTran("007138", "原位置姓名") + "", "NewLocation=" + GetTran("000660", "新位置") + "", "Name3=" + GetTran("007139", "新位置姓名") + "", "ExpectNum=" + GetTran("000661", "调网期数") + "", "AdjustDate=" + GetTran("000613", "日期") + "", "OperateNum=" + GetTran("000662", "操作员") + "" });

         
    }
    protected void btnWL_Click(object sender, EventArgs e)
    {
        Response.Redirect("tw/ChangeTeam2.aspx");
    }
   
}
