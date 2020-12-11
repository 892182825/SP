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
using BLL;
using BLL.CommonClass;
using DAL;
using Standard.Classes;
using BLL.Logistics;

public partial class Company_MemberAccountView : BLL.TranslationBase
{
    int type = 0;
    static int sphours = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerQureyMember);
        Response.Cache.SetExpires(DateTime.Now);
        sphours = Convert.ToInt32(Session["WTH"]);
        if (!IsPostBack)
        {
            if (Request.QueryString["dd"] != null && Request.QueryString["dd"] != "")
            {
                type = 1;
            }

            CommonDataBLL.BindQishuList(this.DropDownExpectNum, true);
            getMemberInfo();
            DataTable dt = D_AccountBLL.GetAllMemberAccountMoney();
            if (dt != null && dt.Rows.Count > 0)
            {
                Label1.Text = "现金账户余额总计：<font color='red'>" +dt.Rows[0]["TotalMoney"].ToString() + "</font>";
                Label2.Text = "报单账户余额总计：<font color='red'>" + dt.Rows[0]["TotalOrderMoney"].ToString() + "</font>";
            }
        }
        Translations();
    }
    private void Translations()
    {
        btnsearch.Text = GetTran("000048", "查 询");
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"000024","会员编号"},
                    new string []{"000025","会员姓名"},
                    new string []{"000027","安置编号"},
                    new string []{"000026","推荐编号"},
                    new string []{"000000","安置姓名"},
                    new string []{"000000","推荐姓名"},
                    new string []{"000029","注册期数"},
                    new string []{"000000","现金账户余额"},
                    new string []{"000000","报单账户余额"}});

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

    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        type = 0;
        getMemberInfo();
        DataTable dt = D_AccountBLL.GetAllMemberAccountMoney();
        if (dt != null && dt.Rows.Count > 0)
        {
            Label1.Text = "现金账户余额总计：<font color='red'>" + dt.Rows[0]["TotalMoney"].ToString() + "</font>";
            Label2.Text = "报单账户余额总计：<font color='red'>" + dt.Rows[0]["TotalOrderMoney"].ToString() + "</font>";
        }
    }
    public void getMemberInfo()
    {
        string Number = DisposeString.DisString(this.Number.Text.Trim());
        string Name = DisposeString.DisString(this.Name.Text.Trim());
        string Recommended = DisposeString.DisString(this.Recommended.Text.Trim());
        string Placement = DisposeString.DisString(this.Placement.Text.Trim());
        int ExpectNum = 0;
        if (this.DropDownExpectNum.SelectedValue.ToString() == "")
        {
            ExpectNum = 0;
        }
        else
        {
            ExpectNum = Convert.ToInt32(this.DropDownExpectNum.SelectedItem.Value);
        }
        //string Tele =Encryption.Encryption.GetEncryptionTele(DisposeString.DisString(this.Tele.Text.Trim()));
        StringBuilder sb = new StringBuilder();
        sb.Append(" 1=1 ");
        if (Number.Length > 0)
        {
            sb.Append(" and Number like'%" + Number + "%'");
        }
        if (Name.Length > 0)
        {
            sb.Append(" and Name like'%" + Encryption.Encryption.GetEncryptionName(Name.Trim()) + "%'");
        }
        if (Recommended.Length > 0)
        {
            sb.Append(" and Direct like'%" + Recommended + "%'");
        }
        if (Placement.Length > 0)
        {
            sb.Append(" and Placement like'%" + Placement + "%'");
        }
        //if (Tele.Length > 0)
        //{
        //    sb.Append(" and HomeTele ='" + Tele + "' or OfficeTele='" + Tele + "' or MobileTele='" + Tele + "' or FaxTele='" + Tele + "'");
        //}
        if (type == 0)
        {
            if (ExpectNum > 0)
            {
                sb.Append(" and ExpectNum=" + ExpectNum);
            }
        }
        //首页传过来时候读取当日的会员
        if (type == 1)
        {
            if (Request.QueryString["dd"] != null && Request.QueryString["dd"] != "")
            {
                sb.Append(" and Convert(varchar,RegisterDate,23) ='" + Request.QueryString["dd"].ToString() + "'");

            }
        }

        string totalDataStart = txtBox_OrderDateTimeStart.Text.Trim();
        string totalDataEnd = txtBox_OrderDateTimeEnd.Text.Trim();
        if (totalDataStart != "")
        {
            Convert.ToDateTime(totalDataStart);
            sb.Append(" and dateadd(hour," + sphours + ",RegisterDate)>='" + totalDataStart + " 00:00:00'");
        }
        if (totalDataEnd != "")
        {
            Convert.ToDateTime(totalDataEnd);
            sb.Append(" and dateadd(hour," + sphours + ",RegisterDate)<='" + totalDataEnd + " 23:59:59'");
        }

        sb.Append(" and " + this.GetNumber());
        ViewState["SQLSTR"] = "SELECT Number,[Name],Direct,Placement,(select [Name] from memberinfo d where d.number=m.Direct) as DirectName,(select [Name] from memberinfo p where p.number=m.Placement) as PlacementName,ExpectNum,isnull((Jackpot-Out-membership),0) as TotalMoney,isnull((totalremittances-totaldefray),0) as TotalOrderMoney FROM MemberInfo m WHERE " + sb.ToString();
        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.PageBind(0, 10, "MemberInfo m", "Number,[Name],Direct,Placement,(select [Name] from memberinfo d where d.number=m.Direct) as DirectName,(select [Name] from memberinfo p where p.number=m.Placement) as PlacementName,ExpectNum,isnull((Jackpot-Out-membership),0) as TotalMoney,isnull((totalremittances-totaldefray),0) as TotalOrderMoney", sb.ToString(), "ID", "GridView1");
        ViewState["condition"] = sb.ToString();
        Translations();
    }
    private void datalist(string sql)
    {
        DataTable dt = new DataTable();
        dt = CommonDataBLL.datalist(sql);
        if (dt == null)
        {
            Response.Write("<script language='javascript'>alert('对不起，找不到指定条件的记录')</script>");
            return;
        }
        if (dt.Rows.Count < 1)
        {
            Response.Write("<script language='javascript'>alert('对不起，找不到指定条件的记录')</script>");
            return;
        }
        if (dt.Rows.Count > 0)
        {
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataRowView drv = (DataRowView)e.Row.DataItem;
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            string num = drv["number"].ToString(); //e.Row.Cells[1].Text;

            Label lblname = (Label)e.Row.FindControl("lblname");
            lblname.Text = Encryption.Encryption.GetDecipherName(drv["name"].ToString());  //绑定解密后的用户名 // e.Row.Cells[2].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[2].Text.ToString().Trim());
            Label lblDirectName = (Label)e.Row.FindControl("lblDirectName");
            lblDirectName.Text = Encryption.Encryption.GetDecipherName(drv["DirectName"].ToString());  //绑定解密后的用户名 // e.Row.Cells[2].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[2].Text.ToString().Trim());
            Label lblPlacementName = (Label)e.Row.FindControl("lblPlacementName");
            lblPlacementName.Text = Encryption.Encryption.GetDecipherName(drv["PlacementName"].ToString());  //绑定解密后的用户名 // e.Row.Cells[2].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[2].Text.ToString().Trim());

            Label lblPlacement = (Label)e.Row.FindControl("lblPlacement");
            Label lblDirect = (Label)e.Row.FindControl("lblDirect");
            string comuser = Session["Company"].ToString();
            //判断该管理员是否有权限查看此会员的安置  的权限          
            if (!BLL.CommonClass.CommonDataBLL.GetRole2(comuser, true))
                lblPlacement.Text = "";
            //判断该管理员是否有权限查看此会员的推荐  的权限 
            if (!BLL.CommonClass.CommonDataBLL.GetRole2(comuser, false))
                lblDirect.Text = "";

        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
        Translations();
    }
    /// <summary>
    /// 导出excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dt = DBHelper.ExecuteDataTable(ViewState["SQLSTR"].ToString());
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
        //Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xls");
        //Response.ContentEncoding = Encoding.UTF8;


        foreach (DataRow row in dt.Rows)
        {
            row["Name"] = Encryption.Encryption.GetDecipherName(row["Name"].ToString());//解密姓名
            row["PlacementName"] = Encryption.Encryption.GetDecipherName(row["PlacementName"].ToString());//解密姓名
            row["DirectName"] = Encryption.Encryption.GetDecipherName(row["DirectName"].ToString());//解密姓名
            try
            {
                row["RegisterDate"] = Convert.ToDateTime(row["RegisterDate"]).AddHours(sphours);
            }
            catch
            {
            }
        }
        //StringBuilder sb = Excel.GetExcelTable(dt, GetTran("005006", "会员信息编辑"), new string[] { "Number=" + GetTran("000024", "会员编号"), "Name=" + GetTran("000025", "会员姓名"), "Placement=" + GetTran("000027", "安置编号"), "Direct=" + GetTran("000026", "推荐编号"), "PlacementName=" + GetTran("000030", "安置姓名"), "DirectName=" + GetTran("000000", "推荐姓名"), "ExpectNum=" + GetTran("000029", "注册期数"), "TotalMoney=" + GetTran("000000", "现金账户剩余金额"), "TotalOrderMoney=" + GetTran("000000", "报单账户剩余金额") });

        //Response.Write(sb.ToString());

        //Response.Flush();
        //Response.End();
        Excel.OutToExcel(dt, GetTran("005006", "会员信息编辑"), new string[] { "Number=" + GetTran("000024", "会员编号"), "Name=" + GetTran("000025", "会员姓名"), "Placement=" + GetTran("000027", "安置编号"), "Direct=" + GetTran("000026", "推荐编号"), "PlacementName=" + GetTran("000030", "安置姓名"), "DirectName=" + GetTran("000000", "推荐姓名"), "ExpectNum=" + GetTran("000029", "注册期数"), "TotalMoney=" + GetTran("000000", "现金账户剩余金额"), "TotalOrderMoney=" + GetTran("000000", "报单账户剩余金额") });
    }


    /// <summary>
    /// 行命令事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string number = e.CommandArgument.ToString();
        if (e.CommandName == "detl")
        {
            Response.Redirect("DisplayMemberDeatail.aspx?id=" + number);
        }

    }
}
