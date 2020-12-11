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

///Add Namespace
using System.Text;
using System.IO;
using System.Globalization;
using BLL.CommonClass;
using BLL.other.Company;
using Model;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-11
 * 对应菜单：   信息中心->废邮件
 */

public partial class MemberMobile_feijianx : BLL.TranslationBase
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        ///设置GridView的样式
        //GridView1.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            txtBeginTime.Text = CommonDataBLL.GetDateBegin();
            txtEndTime.Text = CommonDataBLL.GetDateEnd();
            GetShopList();
        }

        Translations_More();
    }


    protected void Translations_More()
    {
        //TranControls(GridView1, new string[][]
        //                {
        //                    new string[] { "000912","接收对象"},
        //                    new string[] { "000910","接收编号"},
        //                    new string[] { "000908","发送编号"},
        //                    new string[] { "000825","信息标题"},
        //                    new string[] { "000720","发布日期"},
        //                    new string[] { "000022","删除"},
        //                    new string[]{"000000","还原"}
        //                }
        //            );


        TranControls(BtnConfirm, new string[][]
                        {
                            new string[] { "000048","查 询"}
                        }
          );
    }

    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        if (this.txtBeginTime.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000763", "起始时间不能为空") + "！"));
            return;
        }

        if (this.txtEndTime.Text == "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000759", "终止时间不能为空") + "！"));
            return;
        }

        try
        {
            this.txtBeginTime.Text = Convert.ToDateTime(this.txtBeginTime.Text).ToShortDateString();
            this.txtEndTime.Text = Convert.ToDateTime(this.txtEndTime.Text).ToShortDateString();
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000758", "日期格式不正确，请重新输入") + "！');</script>");
            return;
        }
        GetShopList();
        Translations_More();
    }
    protected string getloginRole(string str)    // 前台调用(接受对象的转换)
    {
        switch (str.Trim())
        {
            case "2":
                return GetTran("000599", "会员");
            case "1":
                return GetTran("000388", "店铺");
            default:
                return GetTran("000151", "管理员");
        }
    }

    public string GetBiaoZhunTime(string dt)
    {
        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }

    private void GetShopList()
    {
        //StringBuilder sb = new StringBuilder();
        //string table = "V_DroppedMessage";
        //sb.Append(" Operator='" + Session["Member"].ToString() + "' and OperatorType=2 ");

        //if (this.txtBeginTime.Text != "" && this.txtEndTime.Text != "")
        //{
        //    string beginDate = this.txtBeginTime.Text.Trim() + " 00:00:00";
        //    string endDate = this.txtEndTime.Text.Trim() + " 23:59:59";
        //    if (Convert.ToDateTime(beginDate) > Convert.ToDateTime(endDate))
        //    {
        //        Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("" + GetTran("000755", "起始时间不能大于终止之间") + "！"));
        //        return;
        //    }
        //    sb.Append(" and dateadd(hour," + BLL.other.Company.WordlTimeBLL.ConvertAddHours() + ",Senddate)>='" + beginDate + "'" + "and dateadd(hour," + BLL.other.Company.WordlTimeBLL.ConvertAddHours() + ",Senddate)<='" + endDate + "'");
        //}
        //ViewState["SQLSTR"] = "select * from V_DroppedMessage where " + sb.ToString();

        //Pager1.Pageindex = 0;
        //Pager1.PageSize = 10;
        //Pager1.PageTable = table;
        //Pager1.Condition = sb.ToString();
        //Pager1.PageColumn = "* ";
        //Pager1.ControlName = "rep_TransferList";
        //Pager1.PageCount = 0;
        //Pager1.key = "id";
        //Pager1.PageBind();
    }
    private void datalist(string sql)
    {
        DataTable dt = CommonDataBLL.datalist(sql);
        if (dt == null)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000051", "对不起，找不到指定条件的记录") + "')</script>");
            return;
        }
        if (dt.Rows.Count < 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000051", "对不起，找不到指定条件的记录") + "')</script>");
            return;
        }
        if (dt.Rows.Count > 0)
        {
            //this.rep_TransferList.DataSource = dt;
            //this.rep_TransferList.DataBind();
        }
    }

    /// <summary>
    /// 针对Excel导出重载
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //this.GridView1.Columns[0].Visible = false;
        //this.GridView1.AllowPaging = false;
        //this.GridView1.AllowSorting = false;

        //string sql = ViewState["SQLSTR"].ToString();
        //datalist(sql);

        //Response.Clear();
        //Response.Buffer = true;
        //Response.Charset = "GB2312";
        //Response.AddHeader("Content-Disposition", "attachment;filename=MemberMoidy.xls");
        //Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");//设置输出流为简体中文
        //Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
        //this.EnableViewState = false;
        //CultureInfo myCItrad = new CultureInfo("ZH-CN", true);
        //StringWriter oStringWriter = new StringWriter(myCItrad);
        //HtmlTextWriter oHtmlTextWriter = new HtmlTextWriter(oStringWriter);
        //this.GridView1.RenderControl(oHtmlTextWriter);
        //Response.Write(oStringWriter.ToString());
        //Response.End();

        //GridView1.Columns[0].Visible = true;
        //GridView1.AllowPaging = true;
        //this.GridView1.AllowSorting = true;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        MessageSendBLL msb = new MessageSendBLL();
        MessageReciveModel mrm = new MessageReciveModel();
        GridViewRow row = (GridViewRow)((Image)e.CommandSource).NamingContainer;
        int ID = Convert.ToInt32((row.FindControl("HidID") as Label).Text);
        int i = 0;
        switch (e.CommandName.Trim())
        {
            case "del":
                i = msb.delMessageDrop(ID);
                if (i > 0)
                {
                    msg = "<script language='javascript'>alert('" + GetTran("000008", "删除成功") + "！');</script>";
                }
                break;
            default:
                break;

        }
        GetShopList();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ///控制样式
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            }

            TableCell cc = (TableCell)e.Row.Controls[1];
            if (cc.Text == "*")
                cc.Text = GetTran("000996", "全体成员");
            if (cc.Text.IndexOf(",") > 0)
                cc.Text = Session["Store"].ToString();
            cc = (TableCell)e.Row.Controls[2];
            if (cc.Text == "*")
                cc.Text = GetTran("000996", "全体成员");
            if (cc.Text.Length > 10)
                cc.Text = cc.Text.Substring(0, 10) + "...";

            Image LButton = (Image)e.Row.FindControl("Linkbutton2");
            LButton.Attributes.Add("onClick", "return confirm('" + GetTran("000995", "你确定要删除该条信息吗") + "?');");
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
        BtnConfirm_Click(null, null);
    }
}
