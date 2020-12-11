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
using Model;
using BLL;
using BLL.MoneyFlows;
using BLL.CommonClass;
using Model.Other;
using System.Collections.Generic;
using System.Text;
using DAL;
using Standard.Classes;
using System.Data.SqlClient;
using BLL.Logistics;

public partial class DoSeecan : BLL.TranslationBase
{
    protected ProcessRequest process = new ProcessRequest();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Expires = 0;
        Response.Cache.SetExpires(DateTime.Now.ToUniversalTime());
        Permissions.CheckManagePermission(6309); //检查相应权限

        if (!IsPostBack)
        {
            if (Session["Company"] == null)
            {
                Response.Write("Index.aspx");
            }
            //this.DropDownList1.Items.Add(new ListItem(GetTran("000633", "全部"), "-1"));
            //this.DropDownList1.Items.Add(new ListItem(GetTran("007168", "待审核"), "0"));
            //this.DropDownList1.Items.Add(new ListItem(GetTran("007170", "开始处理"), "1"));
            //this.DropDownList1.Items.Add(new ListItem(GetTran("007169", "已汇出"), "2"));
            //this.DropDownList1.Items.Add(new ListItem(GetTran("007171", "账号错误"), "3"));

            //绑定国家

            loadlist();
            translation();
        }
    }

    private void translation()
    {
        TranControls(BtnConfirm, new string[][] { new string[] { "000340", "查询" } });
        TranControls(GridView1, new string[][]{
                new string[]{"007818","公司审核"},
                new string[]{"001970","汇款金额"},
                new string[]{"007798","汇款时间"},
                new string[]{"007819","汇入账户"},
                new string[]{"000107","姓名"},
                new string[]{"001195","编号"},
                new string[]{"000000","金流匹配"} ,
                new string[]{"000000","汇款凭证"}
        });
    }

    private void loadlist()
    {
        string table = " remtemp rp,Remittances mp ";
        StringBuilder condition = new StringBuilder();
        condition.Append(" mp.Remittancesid=rp.Remittancesid and mp.shenhestate<>-1 and memberflag=1 and isusepay=1 and ((Ispipei=1 and isjl=1) or isjl=0) ");
        string number = txtNumber.Text.Trim();
        //加入条件
        if (this.Datepicker1.Text.Trim() != "")
        {
            condition.Append(" and mp.remittancesdate>='" + this.Datepicker1.Text.Trim() + "'");
        }
        if (this.Datepicker2.Text.Trim() != "")
        {
            condition.Append(" and mp.remittancesdate<='" + this.Datepicker2.Text.Trim() + "'");
        }
        if (number.Length > 0)
        {
            condition.Append(" and rp.Number like '%" + number + "%'");
        }


        string cloumns = @" case mp.isjl when 1 then '是' else '否' end as isjlstr,rp.id, rp.number,case mp.RemitStatus when 1 then (select name from memberinfo mb where mb.number=mp.remitnumber) when 0 then (select name from storeinfo st where st.storeid=mp.remitnumber) end as name,rp.totalrmbmoney,rp.totalmoney,rp.flag,mp.remittancesdate,mp.Remittancesid ,rp.memberflag ,rp.memberSureTime,
rp.CompanySureTime,rp.filepicname,rp.RemBankBook,rp.RemBankname,rp.RemBankaddress  ,mp.hkpzImglj ";
        string key = " rp.id ";
        ViewState["key"] = key;
        ViewState["PageColumn"] = cloumns;
        ViewState["table"] = table;
        ViewState["condition"] = condition.ToString();
        this.GridView1.DataSourceID = null;
        this.Pager1.ControlName = "GridView1";
        this.Pager1.key = key;
        this.Pager1.PageColumn = cloumns;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = table;
        this.Pager1.Condition = condition.ToString();
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();
        ViewState["sql"] = "select (case when rp.flag=1 then '已收款' else '确认收款' end) flag,rp.totalrmbmoney,mp.remittancesdate,rp.RemBankaddress+'-'+rp.RemBankBook zh,mb.[name],rp.[number] from " + table + " where " + condition.ToString() + " order by rp.id desc";
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");

            DataRowView drv = (DataRowView)e.Row.DataItem;
            string rmid = drv["Remittancesid"].ToString();
            string bankaddress = drv["RemBankaddress"].ToString();
            string bankbook = drv["RemBankBook"].ToString();

            string imgname = drv["hkpzImglj"].ToString();
            Image img=(Image)e.Row.FindControl("Image1");
            if (imgname != "") img.Visible = true;
            DateTime dtrem = Convert.ToDateTime(drv["remittancesdate"]);
            Label lblbankinfo = (Label)e.Row.FindControl("lblbankinfo");
            lblbankinfo.Text = bankaddress + "-" + bankbook;
            int fg = Convert.ToInt32(drv["flag"]);
            LinkButton lkbtsuregetmoney = (LinkButton)e.Row.FindControl("lkbtsuregetmoney");
            Label lblsure = (Label)e.Row.FindControl("lblsure");
            Label lblremdate = (Label)e.Row.FindControl("lblremdate");
            HiddenField h = (HiddenField)e.Row.FindControl("hiddTime");
            lblremdate.Text = dtrem.AddHours(Convert.ToDouble(Session["WTH"])).ToString();

            if (fg == 1 || fg == 4 || fg == 2)
            {
                lblsure.Text = "已收款";
                lkbtsuregetmoney.Text = "已收款";
                lkbtsuregetmoney.Enabled = false;
            }
            else if (fg == 0)
            {
                lblsure.Text = "<a href='#' onclick=\"opshow('" + rmid + "');\"> 收款确认</a>";

                //lblsure.Text = "<a href='#' OnClientClick=\"return confirm('确定收到该笔汇款了吗?')\"> 收款确认</a>";
                lkbtsuregetmoney.OnClientClick = "return confirm('确定收到该笔汇款了吗？');";
            }
        }
    }
    //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    // int  repid=Convert.ToInt32( e.CommandArgument );
    // //确认收款
    // if (e.CommandName == "OM")
    // {
    //     string ip =Request.UserHostAddress.ToString();
    //     string remark = "";
    //     DataTable dt = DBHelper.ExecuteDataTable("select number,orderid from  remtemp where id='" + repid + "'");
    //     if (dt != null && dt.Rows.Count > 0)
    //     {
    //         string number = dt.Rows[0]["number"].ToString();
    //         string orderid = dt.Rows[0]["orderid"].ToString();

    //         int res = 0;// AddOrderDataDAL.OrderPayment(number, orderid, ip, Session["company"].ToString(), remark, 3, 1, 1, repid);
    //             if (res == 0)
    //             {
    //                 string esb = ""; //邮件是否发送成功
    //                 string msb = "";  //短信是否发送成功
    //                 DataTable dtmb = DBHelper.ExecuteDataTable("select  number ,name,email from memberinfo where number = '" + number + "'");
    //                 string regnumber = "";
    //                 string name = "";
    //                 string email = "";
    //                 if (dtmb != null && dtmb.Rows.Count > 0)
    //                 {
    //                     regnumber = dtmb.Rows[0]["number"].ToString();
    //                     name = dtmb.Rows[0]["name"].ToString();
    //                     email = dtmb.Rows[0]["email"].ToString();
    //                 }
    //                 //发送短信  邮件
    //                 string message = "会员[" + name + "]注册成功，编号为[" + regnumber + "]，登录密码、二级密码初始状态均为[" + regnumber + "]，请立即登录系统进行密码修改，防止个人信息泄露！";
    //                 msb = SendEmail.SendSMS(regnumber, message) ? "短信已成功发送，请注意查收！" : "短信发送失败！";
    //                 esb = SendEmail.SendMails(email, "注册成功", message) ? "邮件已成功发送到您的注册邮箱,请注意查收！" : "邮件发送失败！";

    //                 ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('确认收款成功！');</script>");
    //                 loadlist();
    //             }
    //             else
    //             {
    //                 ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('确认收款失败！');</script>");
    //             }

    //     }
    // }
    //}

    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        loadlist();
    }
    protected void Button1_DaoChu(object sender, EventArgs e)
    {
        DataTable dt = DAL.DBHelper.ExecuteDataTable(ViewState["sql"].ToString(), CommandType.Text);
        if (dt == null || dt.Rows.Count == 0)
        {
            ScriptHelper.SetAlert(this, "当前页面没有要导出的数据。");
            return;
        }
        StringBuilder strb = Excel.GetExcelTable(dt, "汇款确认", new string[] { "flag=公司审核", "totalrmbmoney=汇款金额", "remittancesdate=汇款时间", "zh=汇入账户", "name=姓名", "number=编号" });
        System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xls");
        System.Web.HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
        //System.Web.HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
        System.Web.HttpContext.Current.Response.Write(strb.ToString());
        System.Web.HttpContext.Current.Response.Flush();
        System.Web.HttpContext.Current.Response.End();
    }
}