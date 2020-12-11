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

public partial class Company_ManagePassReset :BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(6312);

        if (!IsPostBack)
        {
            PageSet();
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.GridView1,
                            new string[][]{	
                                new string []{"000015","操作"},
                                new string []{"001066","管理员编号"},
                                new string []{"001067","管理员名称"},
                                new string []{"000963","角色名"},
                                new string []{"000964","部门名"},
                                new string []{"000967","创建时间"},
                            });
        TranControls(BtnConfirm, new string[][] { new string[] { "000340", "查询" } });
    }

    protected void PageSet()
    {
        string number = Session["Company"].ToString();
        string tableName=" Manage m inner join deptrole d on m.roleid = d.id inner join companydept c on d.deptid = c.id";
        string Column = " m.id,m.Number,m.Name mName,c.Dept,d.Name dName,BeginDate ";
       
        string ids = BLL.other.Company.DeptRoleBLL.GetDeptRoleIDs(number);
        if (ids == "")
        {
            this.GridView1.DataSource = null;
            this.GridView1.DataBind();
            this.Pager1.Visible = false;
            return;
        }
        StringBuilder builder = new StringBuilder();
        builder.Append(" d.id in (" + ids + ") ");
        string name = this.Name.Text.Trim();
        string num = this.Number.Text.Trim();
        if(num.Length > 0)
        {
            builder.Append(" and m.Number ='"+num+"'");
        }
        if(name.Length > 0)
        {
            builder.Append(" and m.Name like '%");
            builder.Append(name);
            builder.Append("%'");
        }
        ViewState["sql"] = "select " + Column + " from " + tableName + " where " + builder.ToString() + " order by m.id";
        this.Pager1.PageBind(0, 10,tableName ,Column,builder.ToString(), " m.id ", "GridView1");
        Translations();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg;");
           
        }
    }
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        PageSet();
    }

    protected string GetTime(object obj)
    {
        if (obj != null)
        {
            return Convert.ToDateTime(obj).AddHours(Convert.ToDouble(Session["WTH"])).ToShortDateString();
        }
        else
        {
            return "";
        }
    }

    protected void download_Click(object sender,EventArgs e)
    {
        string cmd = ViewState["sql"].ToString();
        DataTable dt = DAL.DBHelper.ExecuteDataTable(cmd);
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

            row["mName"] = Encryption.Encryption.GetDecipherName(Convert.ToString(row["mName"]));
            row["BeginDate"] = Convert.ToDateTime(row["BeginDate"]).AddHours(Convert.ToDouble(Session["WTH"]));

        }

        Excel.OutToExcel(dt, GetTran("007216", "管理员密码重置"), new string[] { "Number=" + GetTran("001066", "管理员编号") + "", "mName=" + GetTran("001067", "管理员名称") + "", "Dept=" + GetTran("000963", "角色名") + "", "dName=" + GetTran("000964", "部门名") + "", "BeginDate=" + GetTran("000967", "创建时间") + "" });

    }
}
