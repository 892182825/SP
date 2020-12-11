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
using BLL.other.Company;
using Model.Other;
using BLL.CommonClass;
using DAL;
using Standard.Classes;

public partial class Company_ManageMessage_Drop : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.ManageMessageDrop);

        givShowMessage.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
        if (!IsPostBack)
        {
            this.txtdatastrat.Text = CommonDataBLL.GetDateBegin().ToString();
            this.txtendDate.Text = CommonDataBLL.GetDateEnd().ToString();
            Pager page = Page.FindControl("Pager1") as Pager;
            // page.PageBind(0, 10, "MessageDrop", " ID,InfoTitle,LoginRole,Receive,Sender,Senddate", " 1=1 ", "ID", "givShowMessage");
            btnSeach_Click(null, null);
        }
        Translations_More();
    }


    protected void Translations_More()
    {
        TranControls(givShowMessage, new string[][] 
                        {
                            new string[] { "000015", "操作"},
                            new string[] { "000912","接收对象"}, 
                            new string[] { "000910","接收编号"}, 
                            new string[] { "000908","发送编号"}, 
                            new string[] { "000825","信息标题"}, 
                             
                            new string[] { "000720","发布日期"},
                            new string[] { "000022","删除"}
                        }
                    );


        TranControls(btnSeach, new string[][] 
                        {
                            new string[] { "000048","查 询"}                             
                        }
          );
    }


    protected string jieshuoduixing(object obj)
    {
        switch (obj.ToString().Trim())
        {
            case "0":
                return GetTran("000151", "管理员");
            case "2":
                return GetTran("000599", "会员");
            case "1":
                return GetTran("000388", "店铺");
            default:
                return GetTran("000599", "会员");
        }

    }

    public string GetBiaoZhunTime(string dt)
    {
        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }

    protected void btnSeach_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            if (txtdatastrat.Text.Trim() != "")
            {
                DateTime strat = Convert.ToDateTime(txtdatastrat.Text.Trim());
                if (strat.CompareTo(Convert.ToDateTime("1991-1-1")) < 0)
                {
                    ScriptHelper.SetAlert(btnSeach, GetTran("001733", "输入的请从1991年开始") + "！！！");
                    return;
                }
            }
        }
        catch (Exception)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000758", "日期格式不正确，请重新输入") + "！');</script>");
            return;
        }
        try
        {
            if (txtendDate.Text.Trim() != "")
            {
                DateTime end = Convert.ToDateTime(txtendDate.Text.Trim().Trim());
                if (end.CompareTo(Convert.ToDateTime("1991-1-1")) < 0)
                {
                    ScriptHelper.SetAlert(btnSeach, GetTran("001733", "输入的请从1991年开始") + "！！！");
                    return;
                }
            }
        }
        catch (Exception)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000758", "日期格式不正确，请重新输入") + "！');</script>");
            return;
        }
        sb.Append(" dateadd(hour," + BLL.other.Company.WordlTimeBLL.ConvertAddHours() + ",Senddate)  between '");
        sb.Append(txtdatastrat.Text.Trim());
        sb.Append(" 00:00:00' and '" + txtendDate.Text.Trim() + " 23:59:59' ");
        // sb.Append(" and Receive='" + Session["Company"].ToString() + "' or Sender='" + Session["Company"].ToString() + "' and DropFlag=1");
        sb.Append(" and Operator='" + Session["Company"].ToString() + "' and OperatorType=0 ");
        ViewState["where"] = sb.ToString();
        Pager page = Page.FindControl("Pager1") as Pager;
        page.PageBind(0, 10, "V_DroppedMessage", " ID,InfoTitle,LoginRole,Receive,Sender,Senddate", sb.ToString(), "ID", "givShowMessage");
        Translations_More();
    }
    protected void givShowMessage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string name = e.CommandName;
        int id = Convert.ToInt32(e.CommandArgument);
        if ("Del" == name)
        {
            if (MessageSendBLL.DelMessageDropbyID(id))
            {
                ScriptHelper.SetAlert(Page,  GetTran("000008", "删除成功") + "！！！");
                //Pager page = Page.FindControl("Pager1") as Pager;

                //page.PageBind(0, 10, "MessageDrop", "ID,InfoTitle,LoginRole,Receive,Sender,Senddate", ViewState["where"].ToString(), "ID", "givShowMessage");
                btnSeach_Click(null, null);
            }
        }

    }
    protected void btndownExcel_Click(object sender, EventArgs e)
    {
        Response.AppendHeader("Content-Disposition", "attachment;filename=registerStore.xls");
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-7");

        string cmd = @"select InfoTitle,case LoginRole when 0 then N'" + GetTran("000151", "管理员") + "' when 1 then N'" + GetTran("000388", "店铺") + "' when 2 then N'" + GetTran("000599", "会员") + "' end as LoginRole,Receive,Sender,Senddate from V_DroppedMessage where " + ViewState["where"];

        DataTable dt = DBHelper.ExecuteDataTable(cmd);

        StringBuilder sb = Excel.GetExcelTable(dt, GetTran("005156", "废件箱表"), new string[] { "LoginRole=" + GetTran("000912", "接收对象"), "Receive=" + GetTran("000910", "接收编号"), "Sender=" + GetTran("000908", "发送编号"), "InfoTitle=" + GetTran("000825", "信息标题"), "Senddate=" + GetTran("000752", "发送时间") });


        Response.Write(sb.ToString());
        Response.End();



    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void givShowMessage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");


            int Delete = 0;
            Delete = (int)Permissions.GetPermissions(Model.Other.EnumCompanyPermission.ManageMessageSendDelete);
            if (Delete == 0)
            {
                ((LinkButton)e.Row.FindControl("Button2")).Visible = false;
            }
            else
            {
                ((LinkButton)e.Row.FindControl("Button2")).Visible = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations_More();
        }
    }

    protected void lbtnRecover_Command(object sender, CommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        int recoverCount = MessageDropBLL.RecoverMessage(id);
        if (recoverCount > 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("006965", "还原成功！") + "');</script>");
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("007059", "还原失败，请联系管理员！") + "');</script>");

        }
        btnSeach_Click(null, null);
    }
}
