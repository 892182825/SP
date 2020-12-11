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

public partial class Company_QueryMemberInfo : BLL.TranslationBase
{
    static int sphours = 0;  //时区差
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl); //判断有没有管理员登录
        Response.Cache.SetExpires(DateTime.Now);  //防止缓存导致数据不能及时看到
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerMembermoidy);  //判断该管理员是否有权限访问此页面

        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));  //注册ajax
        sphours = Convert.ToInt32(Session["WTH"]);
        if (!IsPostBack)
        {
            CommonDataBLL.BindQishuList(this.DropDownExpectNum, true);
            getMemberInfo();  //加载会员信息
        }
        Translations();
        //Session["firstpage"] = "QueryMemberInfo.aspx";
       
    }
    /// <summary>
    /// 表头翻译
    /// </summary>
    private void Translations()
    {
        btnsearch.Text = GetTran("000048", "查 询");
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"000024","会员编号"},
                    new string []{"000063","会员昵称"},
                    new string []{"000025","会员姓名"},
                    new string []{"000027","安置编号"},
                    new string []{"000097","安置姓名"},
                    new string []{"000026","推荐编号"},
                    new string []{"000192","推荐姓名"},
                    new string []{"000029","注册期数"},
                    new string []{"000031","注册时间"},
                    new string []{"007301","激活时间"}});

    }

    #region 获得会员
    /// <summary>
    /// 获取会员信息
    /// </summary>
    public void getMemberInfo()
    {
        string Number = DisposeString.DisString(this.Number.Text.Trim()); //去空格后赋值
        string Name = Encryption.Encryption.GetEncryptionName(DisposeString.DisString(this.Name.Text.Trim()));
        string Recommended = DisposeString.DisString(this.Recommended.Text.Trim());
        string Placement = DisposeString.DisString(this.Placement.Text.Trim());
        string DName = DisposeString.DisString(this.DName.Text.Trim());
        string PName = DisposeString.DisString(this.PName.Text.Trim());
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
            sb.Append(" and m.Number like'%" + Number + "%'");
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
        if (Placement.Length > 0)
        {
            sb.Append(" and m.Placement like'%" + Placement + "%'");
        }
        if (PName.Length > 0)
        {
            sb.Append(" and p.name like'%" + PName + "%'");
        }
        string totalDataStart = txtBox_OrderDateTimeStart.Text.Trim();
        string totalDataEnd = txtBox_OrderDateTimeEnd.Text.Trim();
        if (totalDataStart != "")
        {
            Convert.ToDateTime(totalDataStart);
            sb.Append(" and dateadd(hour," + sphours + ",m.advtime)>='" + totalDataStart + " 00:00:00'");
        }
        if (totalDataEnd != "")
        {
            Convert.ToDateTime(totalDataEnd);
            sb.Append(" and dateadd(hour," + sphours + ",m.advtime)<='" + totalDataEnd + " 23:59:59'");
        }
        if (sb.ToString().Contains("advtime"))
        {
            sb.Append(" and m.MemberState=1");
        }

        if (ExpectNum > 0)
        {
            sb.Append(" and m.ExpectNum=" + ExpectNum);
        }
        ViewState["SQLSTR"] = "SELECT m.MemberState,m.Placement,p.name as PlacementName,m.Number,m.Name,m.PetName,m.StoreID,m.Direct,d.name as directName,m.ExpectNum,m.RegisterDate,m.Jackpot-m.Out as jjzh,m.Jackpot-m.Out as jjky,m.TotalRemittances-m.TotalDefray as bdzh,m.advtime FROM MemberInfo m left join Memberinfo d on m.direct=d.number left join Memberinfo p on m.placement=p.number WHERE " + sb.ToString() + " order by m.id desc";
        string asg = ViewState["SQLSTR"].ToString();
        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.Pageindex = 0;
        pager.PageSize = 10;
        pager.PageTable = " MemberInfo m left join Memberinfo d on m.direct=d.number left join Memberinfo p on m.placement=p.number";
        pager.Condition = sb.ToString();
        pager.PageColumn = " m.MemberState,m.Placement,p.name as PlacementName,m.Number,m.Name,m.PetName,m.StoreID,m.Direct,d.name as directName,m.ExpectNum,m.RegisterDate,m.Jackpot-m.Out as jjzh,m.Jackpot-m.Out as jjky,m.TotalRemittances-m.TotalDefray as bdzh,m.advtime ";
        pager.ControlName = "GridView1";
        pager.key = " m.ID ";
        pager.InitBindData = true;
        pager.PageBind();
        Translations();
    }


    #endregion

    /// <summary>
    /// 导出Excle
    /// </summary>
    /// <param name="sql"></param>
    private void datalist(string sql)
    {
        DataTable dt = CommonDataBLL.datalist(sql);
        if (dt == null || dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000051", "对不起，找不到指定条件的记录！") + "')</script>");
            return;
        }
        else
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
            DataRowView drv = (DataRowView)e.Row.DataItem;
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            string num = drv["number"].ToString(); //e.Row.Cells[1].Text;

            Label lblname = (Label)e.Row.FindControl("lblname");
            lblname.Text = Encryption.Encryption.GetDecipherName(drv["name"].ToString());  //绑定解密后的用户名 // e.Row.Cells[2].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[2].Text.ToString().Trim());
            Label lblDirectName = (Label)e.Row.FindControl("lblDirectName");
            lblDirectName.Text = Encryption.Encryption.GetDecipherName(drv["DirectName"].ToString());

            Label lblPlacementName = (Label)e.Row.FindControl("lblPlacementName");
            lblPlacementName.Text = Encryption.Encryption.GetDecipherName(drv["PlacementName"].ToString());
            Label lblPlacement = (Label)e.Row.FindControl("lblPlacement");
            Label lblDirect = (Label)e.Row.FindControl("lblDirect");
            string comuser = Session["Company"].ToString();
            //判断该管理员是否有权限查看此会员的安置  的权限          
            if (!CommonDataBLL.GetRole2(comuser, true))
                lblPlacement.Text = "";
            //判断该管理员是否有权限查看此会员的推荐  的权限 
            if (!CommonDataBLL.GetRole2(comuser, false))
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
                lbladvtime.Text = Convert.ToDateTime(drv["advtime"].ToString()).AddHours(sphours).ToString();
            }


            HiddenField hf = e.Row.FindControl("HiddenField1") as HiddenField;
            if (hf.Value == "0")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }

        }
        else  //如果是表头 加载背景图片
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
            }
        Translations();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        getMemberInfo();
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
        Response.ContentEncoding = Encoding.UTF8;

        dt.Columns.Add("activetimestr");
        foreach (DataRow row in dt.Rows)
        {
            row["Name"] = Encryption.Encryption.GetDecipherName(row["Name"].ToString());
            row["PlacementName"] = Encryption.Encryption.GetDecipherName(row["PlacementName"].ToString());//解密姓名
            row["DirectName"] = Encryption.Encryption.GetDecipherName(row["DirectName"].ToString());//解密姓名
            try
            {
                row["RegisterDate"] = Convert.ToDateTime(row["RegisterDate"]).AddHours(sphours);
                //row["jjzh"] = Convert.ToDouble(row["jjzh"].ToString()).ToString("F2");
                //row["bdzh"] = Convert.ToDouble(row["bdzh"].ToString()).ToString("F2");
                if (row["MemberState"].ToString() == "0")
                {
                    row["activetimestr"] = "";
                }
                else
                {
                    row["activetimestr"] = Convert.ToDateTime(row["advtime"]).AddHours(sphours);
                }
            }
            catch
            {
            }
        }
        StringBuilder sb = Excel.GetExcelTable(dt, GetTran("000056", "会员信息编辑"), new string[] { "Number=" + GetTran("000024", "会员编号"), "Name=" + GetTran("000025", "会员姓名"),"Placement=" + GetTran("000027", "安置编号"), "PlacementName=" + GetTran("000000", "安置姓名"), "Direct=" + GetTran("000026", "推荐编号"), "DirectName=" + GetTran("000000", "推荐姓名"), /*"jjzh=" + GetTran("006966", "奖金账户余额"), "bdzh=" + GetTran("006967", "报单账户余额"),*/ "ExpectNum=" + GetTran("000029", "注册期数"), "RegisterDate=" + GetTran("000031", "注册日期"), "activetimestr=" + GetTran("000000", "激活日期") });
        Response.Write(sb.ToString());

        Response.Flush(); //向客户端发送缓冲输出 即下载excel过程
        Response.End();  //发送缓冲输出停止本页执行
    }

    /// <summary>
    /// 行命令事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        if (e.CommandName == "edit")
        {
            Response.Redirect("MemberInfoModify.aspx?id=" + id);//转到信息编辑页面
        }
    }
}