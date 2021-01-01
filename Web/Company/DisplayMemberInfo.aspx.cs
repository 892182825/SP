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
public partial class Company_DisplayMemberInfo : BLL.TranslationBase
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
        }
        Translations();
    }
    private void Translations()
    {
        //ddl_zxState
        this.TranControls(this.ddl_zxState, new string[][]{
                    new string []{"000633","全部"},
                       //new string []{"007525","未激活"},
                       // new string []{"007524","已激活"},
                        new string []{"001286","已注销"},
                        new string []{"007542","已冻结"}
                    });

        btnsearch.Text = GetTran("000048", "查 询");
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"000035","详细信息"},
                    new string []{"000000","会员账号"},
                 
                    new string []{"000025","会员姓名"},
                   
                    new string []{"0000","推荐账号"},
                    new string []{"000192","推荐姓名"},
                    new string []{"000029","注册期数"},
                    new string []{"000031","注册时间"},
                    new string []{"000000","注册金额"},
                    new string []{"007301","激活时间"},
                    new string []{"008025","会员状态"}});

    }

    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        type = 0;
        getMemberInfo();
    }
    public void getMemberInfo()
    {
        string Number = DisposeString.DisString(this.Number.Text.Trim()); //去空格后赋值
        string Name = Encryption.Encryption.GetEncryptionName(DisposeString.DisString(this.Name.Text.Trim()));
        string Recommended = DisposeString.DisString(this.Recommended.Text.Trim());
        
        string DName = DisposeString.DisString(this.DName.Text.Trim());
        string PName = DisposeString.DisString(this.Name.Text.Trim());
        int ExpectNum = 0;
        if (this.DropDownExpectNum.SelectedValue.ToString() == "")
        {
            ExpectNum = 0;
        }
        else
        {
            ExpectNum = Convert.ToInt32(this.DropDownExpectNum.SelectedItem.Value);
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(" 1=1 ");
        if (Number.Length > 0)
        {
            sb.Append(" and m.mobiletele like'%" + Number + "%'");
        }
        if (Name.Length > 0)
        {
            sb.Append(" and m.Name like'%" + Name + "%'");
        }
        if (Recommended.Length > 0)
        {
            sb.Append(" and m.Direct like'%" + Recommended + "%'");
        }
        if (DName.Length > 0)
        {
            sb.Append(" and d.name like'%" + DName + "%'");
        }
        //if (Placement.Length > 0)
        //{
        //    sb.Append(" and m.Placement like'%" + Placement + "%'");
        //}
        if (PName.Length > 0)
        {
            sb.Append(" and p.name like'%" + PName + "%'");
        }
        if (ddl_zxState.SelectedValue != "-1")
        {
            sb.Append(" and m.MemberState=" + ddl_zxState.SelectedValue);
        }
        if (type == 0)
        {
            if (ExpectNum > 0)
            {
                sb.Append(" and m.ExpectNum=" + ExpectNum);
            }
        }
        //首页传过来时候读取当日的会员
        if (type == 1)
        {
            if (Request.QueryString["dd"] != null && Request.QueryString["dd"] != "")
            {
                sb.Append(" and Convert(varchar,m.RegisterDate,23) ='" + Request.QueryString["dd"].ToString() + "'");

            }
        }

        string totalDataStart = txtBox_OrderDateTimeStart.Text.Trim();
        string totalDataEnd = txtBox_OrderDateTimeEnd.Text.Trim();
        if (totalDataStart != "")
        {
            Convert.ToDateTime(totalDataStart);
            sb.Append(" and dateadd(hour," + sphours + ",m.RegisterDate)>='" + totalDataStart + " 00:00:00'");
        }
        if (totalDataEnd != "")
        {
            Convert.ToDateTime(totalDataEnd);
            sb.Append(" and dateadd(hour," + sphours + ",m.RegisterDate)<='" + totalDataEnd + " 23:59:59'");
        }
        //string advtimeStart = txtBox_AdvTimeStart.Text.Trim();
        //string advtimeEnd = txtBox_AdvTimeEnd.Text.Trim();
        //if (advtimeStart != "")
        //{
        //    Convert.ToDateTime(advtimeStart);
        //    sb.Append(" and dateadd(hour," + sphours + ",m.ActiveDate)>='" + advtimeStart + " 00:00:00'");
        //}
        //if (advtimeEnd != "")
        //{
        //    Convert.ToDateTime(advtimeEnd);
        //    sb.Append(" and dateadd(hour," + sphours + ",m.ActiveDate)<='" + advtimeEnd + " 23:59:59'");
        //}
        if (sb.ToString().Contains("ActiveDate"))
        {
            sb.Append(" and m.MemberState=1");
        }
        ViewState["SQLSTR"] = "SELECT m.MemberState,  m.Number,m.Name  , d.mobiletele dtele, m.Direct,d.name as directName,m.ExpectNum,m.RegisterDate,m.ActiveDate,(select top 1 PayMoney from MemberOrder where Number=m.Number) as zcPrice FROM MemberInfo m left join Memberinfo d on m.direct=d.number    WHERE " + sb.ToString() + " order by m.id desc";
        Pager pager = Page.FindControl("Pager1") as Pager;
        //pager.Pageindex = 0;
        //pager.PageSize = 10;
        //pager.PageTable = "MemberInfo";
        //pager.Condition = sb.ToString();
        //pager.PageColumn = "*";
        //pager.ControlName = "GridView1";
        //pager.key = "ID";
        pager.PageBind(0, 10, " MemberInfo m left join Memberinfo d on m.direct=d.number  ", "m.MemberState, m.number,m.mobiletele,m.Name  ,  d.mobiletele dtele,m.Direct,d.name as directName,m.ExpectNum,m.RegisterDate,m.ActiveDate,(select top 1 PayMoney from MemberOrder where Number=m.Number) as zcPrice", sb.ToString(), "m.ID", "GridView1");
        ViewState["condition"] = sb.ToString();
        Translations();
    }
    private void datalist(string sql)
    {
        DataTable dt = new DataTable();
        dt = CommonDataBLL.datalist(sql);
        if (dt == null)
        {
            Response.Write("<script language='javascript'>alert('" + GetTran("000051", "对不起，找不到指定条件的记录！") + "')</script>");
            return;
        }
        if (dt.Rows.Count < 1)
        {
            Response.Write("<script language='javascript'>alert('" + GetTran("000051", "对不起，找不到指定条件的记录！") + "')</script>");
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
            lblDirectName.Text = Encryption.Encryption.GetDecipherName(drv["DirectName"].ToString());

            Label lblPlacementName = (Label)e.Row.FindControl("lblPlacementName");
            
            Label lblPlacement = (Label)e.Row.FindControl("lblPlacement");
            Label lblDirect = (Label)e.Row.FindControl("lblDirect");
            string comuser = Session["Company"].ToString();
            //判断该管理员是否有权限查看此会员的安置  的权限          
            if (!BLL.CommonClass.CommonDataBLL.GetRole2(comuser, true))
                lblPlacement.Text = "";
            //判断该管理员是否有权限查看此会员的推荐  的权限 
            if (!BLL.CommonClass.CommonDataBLL.GetRole2(comuser, false))
                lblDirect.Text = "";

            Label lblregisterdate = (Label)e.Row.FindControl("lblregisterdate");

            //检测当前使用国家 时间与格林威治时间的时区差 并显示准确的注册时间
            lblregisterdate.Text = Convert.ToDateTime(drv["registerdate"].ToString()).AddHours(sphours).ToString();
            Label lbladvtime = (Label)e.Row.FindControl("lbladvtime");

            if (drv["MemberState"].ToString() == "0")
            {
                //检测当前使用国家 时间与格林威治时间的时区差 并显示准确的注册时间
                lbladvtime.Text = "";
            }
            else
            {
                //检测当前使用国家 时间与格林威治时间的时区差 并显示准确的注册时间
                lbladvtime.Text = Convert.ToDateTime(drv["ActiveDate"].ToString()).AddHours(sphours).ToString();
            }
            Label MemberState = (Label)e.Row.FindControl("MemberState");
            switch (drv["MemberState"].ToString())
            {
                case "0":
                    MemberState.Text = GetTran("007525", "未激活");
                    break;
                case "1":
                    MemberState.Text = GetTran("007524", "已激活");
                    break;
                case "3":
                    MemberState.Text = GetTran("007542", "已冻结");
                    break;
                case "2":
                    MemberState.Text = GetTran("001286", "已注销");
                    break;
                default:
                    MemberState.Text = GetTran("007525", "未激活");
                    break;
            }
            HiddenField hf = e.Row.FindControl("HiddenField1") as HiddenField;
            if (hf.Value == "0")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }

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
        Response.ContentEncoding = Encoding.UTF8;

        dt.Columns.Add("activetimestr");
        foreach (DataRow row in dt.Rows)
        {
            row["Name"] = Encryption.Encryption.GetDecipherName(row["Name"].ToString());//解密姓名
            
            row["DirectName"] = Encryption.Encryption.GetDecipherName(row["DirectName"].ToString());//解密姓名
            try
            {
                row["RegisterDate"] = Convert.ToDateTime(row["RegisterDate"]).AddHours(sphours);
                if (row["MemberState"].ToString() == "0")
                {
                    row["activetimestr"] = "";
                }
                else
                {
                    row["activetimestr"] = Convert.ToDateTime(row["ActiveDate"]).AddHours(sphours);
                }
            }
            catch
            {
            }
        }
        StringBuilder sb = Excel.GetExcelTable(dt, GetTran("005006", "会员信息编辑"), new string[] { "Number=" + GetTran("000024", "会员编号"), "Name=" + GetTran("000025", "会员姓名"),    "Direct=" + GetTran("000026", "推荐编号"), "DirectName=" + GetTran("000192", "推荐姓名"), "ExpectNum=" + GetTran("000029", "注册期数"), "RegisterDate=" + GetTran("000031", "注册日期"),"zcPrice=注册金额", "activetimestr=" + GetTran("007301", "激活日期") });

        Response.Write(sb.ToString());

        Response.Flush();
        Response.End();
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
