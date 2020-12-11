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
using BLL.other.Company;
using Model.Other;
using BLL.CommonClass;
using DAL;
using Standard.Classes;

public partial class Company_ManageQueryGongGao : BLL.TranslationBase
{
    int type = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.ManageQueryGongGao);

        if (!IsPostBack)
        {
            if (Request.QueryString["dd"] != null)
            {
                type = 1;
            }
            this.txtdatastrat.Text = CommonDataBLL.GetDateBegin().ToString();
            this.txtendDate.Text = CommonDataBLL.GetDateEnd().ToString();

            getBander();

            Translations_More();
        }
    }

    protected void Translations_More()
    {
        TranControls(givMessageSend, new string[][] 
                        {
                            new string[] { "000015","操作"}, 
                            new string[] { "000015","操作"}, 
                            new string[] { "000724","公告标题"}, 
                             new string[] { "000784","接收者"}, 
                              new string[] { "007227","接收条件"}, 
                            new string[] { "000726","发送人编号"}, 
                            new string[] { "001643","发布时间"}
                        }
                    );

        TranControls(btnseach, new string[][] 
                        {
                            new string[] { "000048","搜 索"}                             
                        }
            );
    }

    protected string jieshuoduixing(string obj)
    {
        switch (obj.ToString().Trim())
        {
            case "0":
                return GetTran("000151", "管理员");
            case "2":
                return GetTran("000599", "会员");
            //case "1":
            //    return GetTran("000388", "店铺");
            default:
                return GetTran("000599", "会员");
        }

    }

    public string GetBiaoZhunTime(string dt)
    {
        return Convert.ToDateTime(dt).AddHours(Convert.ToInt32(Session["WTH"])).ToShortDateString();
    }

    protected void getBander()
    {
        try
        {
            Convert.ToDateTime(txtdatastrat.Text.Trim());
            Convert.ToDateTime(txtendDate.Text.Trim());
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000758", "日期格式不正确，请重新输入") + "！');</script>");
            return;
        }

        StringBuilder sb = new StringBuilder();

        if (type == 1)
        {
            if (Request.QueryString["dd"] != null)
            {
                sb.Append(" Receive='*' and MessageType='a' and  Convert(varchar,sendDate,23) ='" + Request.QueryString["dd"].ToString() + "' ");
            }
        }
        else
        {
            sb.Append(" dateadd(hour," + BLL.other.Company.WordlTimeBLL.ConvertAddHours() + ",Senddate) between '");
            sb.Append(txtdatastrat.Text.Trim() + " 00:00:00");
            sb.Append("' and '" + txtendDate.Text.Trim() + " 23:59:59' and Receive='*' and MessageType='a' ");
        }

        ViewState["where"] = sb.ToString();

        Pager page = Page.FindControl("Pager1") as Pager;
        page.PageBind(0, 10, "MessageSend", "ID,InfoTitle,LoginRole,Sender,Senddate,dbo.[F_GetReadCondition](ID) Condition", sb.ToString(), "ID", "givMessageSend");

    }

    protected void btnseach_Click(object sender, EventArgs e)
    {
        type = 0;
        getBander();
    }
    protected void givMessageSend_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        BLL.other.Company.MessageReceiveBLL bll = new MessageReceiveBLL();

        string name = e.CommandName;
        int id = Convert.ToInt32(e.CommandArgument.ToString());
        if (name == "Del")
        {
            BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("MessageSend", "ltrim(rtrim(id))");
            cl_h_info.AddRecord(id);
            cl_h_info.DeletedIntoLogs(BLL.CommonClass.ChangeCategory.company19, id.ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype10);

            if (bll.delGongGao(id) > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000008", "删除成功") + "！！！');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000009", "删除失败") + "！！！');</script>");
            }

            btnseach_Click(null, null);
        }
        else if ("Distal" == name)
        {
            Response.Redirect("ShowMessage.aspx?id=" + id);
        }
        else if ("GO" == name)
        {
            Response.Redirect("MessageContent.aspx?id=" + e.CommandArgument.ToString() + "&T=messagesend&source=ManageQueryGongGao.aspx");
        }
        else
        {
            Response.Redirect("ManageAffiche.aspx?id=" + id);
        }
    }
    protected void btndownExcel_Click(object sender, EventArgs e)
    {
        BLL.other.Company.MessageReceiveBLL bll = new MessageReceiveBLL();

        string[] tran = new string[] { GetTran("000151", "管理员"), GetTran("000388", "店铺"), GetTran("000599", "会员") };
        DataTable dt = bll.GetExcelTable(tran, ViewState["where"].ToString() + " order by ID desc ");
        foreach (DataRow dr in dt.Rows)
        {
            dr["Senddate"] = (Convert.ToDateTime(dr["Senddate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours())).ToString();
            dr["LoginRole"] = jieshuoduixing(dr["LoginRole"].ToString());
        }

        Excel.OutToExcel(dt, "公告表", new string[] { "InfoTitle=" + GetTran("000724", "公告标题"), "LoginRole=" + GetTran("000784", "接收者"), "Condition=" + GetTran("007227", "接收条件"), "Sender=" + GetTran("000726", "发送人编号"), "Senddate=" + GetTran("000752", "发送时间") });
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void givMessageSend_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");


            int Update = 0;
            Update = (int)Permissions.GetPermissions(Model.Other.EnumCompanyPermission.ManageQueryGongGaoUpd);
            if (Update == 0)
            {
                ((LinkButton)e.Row.FindControl("btnEdit")).Visible = false;
            }
            else
            {
                ((LinkButton)e.Row.FindControl("btnEdit")).Visible = true;
            }

            int Delete = 0;
            Delete = (int)Permissions.GetPermissions(Model.Other.EnumCompanyPermission.ManageQueryGongGaoDel);
            if (Delete == 0)
            {
                ((LinkButton)e.Row.FindControl("Button1")).Visible = false;
            }
            else
            {
                ((LinkButton)e.Row.FindControl("Button1")).Visible = true;
            }
        }
    }

    public string GetStr(string title)
    {
        if (title.Length > 30)
        {
            return title.Substring(0, 30) + "......";
        }
        else
        {
            return title;
        }
    }
}