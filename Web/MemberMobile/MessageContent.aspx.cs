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

/// <summary>
/// Add Namespace
/// </summary>
using Model;
using BLL.other.Company;
using DAL;
using System.Data.SqlClient;

public partial class Member_MessageContent : BLL.TranslationBase
{
    private readonly BLL.other.Company.MessageReceiveBLL bll = new MessageReceiveBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);

        // 在此处放置用户代码以初始化页面
        string tableName = Request.QueryString["T"].ToString().ToUpper();
        int id = Convert.ToInt32(Request.QueryString["id"].ToString());
        if (Request.QueryString["source"].ToString().ToLower() == "queryinfomation.aspx" || Request.QueryString["type"].ToString().ToLower() == "first.aspx")
        {
            Top.Visible = true;
            Bottom.Visible = true;
        }
        else
        {
            Top.Visible = false;
            Bottom.Visible = false;
            div_content.Attributes.Add("class", "");
        }


        string sqlid = "";

        SqlDataReader dr = null;
        if (tableName.ToLower() == "messagereceive")
            dr = bll.GetEmailDetail(tableName, id, Session["Member"].ToString());
        else
            dr = bll.GetGongGao(tableName, id, Session["Member"].ToString());

        if (dr.Read())
        {
            Text_Date.Text = DateTime.Parse(dr["Senddate"].ToString()).AddHours(Convert.ToDouble(Session["WTH"])).ToString();

            if (dr["Receive"].ToString().Trim() == "*")
                Text_Recive.Text = Session["member"] + "";
            else
                Text_Recive.Text = dr["Receive"].ToString();

            Text_Title.Text = dr["Infotitle"].ToString();
            Text_Send.Text = dr["sender"].ToString();
            DetailSpan.InnerHtml = dr["Content"].ToString();
            sqlid = dr["id"].ToString();

            dr.Close();
            dr.Dispose();
        }
        else
        {
            if (tableName.ToLower() == "messagereceive")
                ScriptHelper.SetAlert(Page, GetTran("008045", "该条邮件不正确！"), "ReceiveEmail.aspx");
            else
                ScriptHelper.SetAlert(Page, GetTran("008046", "该条公告不正确！"), "QueryInfomation.aspx");
        }

        bll.UpdateIsRead(tableName, sqlid);


        TranControls(Button2, new string[][] 
                        {
                            new string[] { "000096","返 回"}                             
                        }
         );
    }
    public string roleName(int id)
    {
        string role = "";
        switch (id)
        {
            case 0: role = GetTran("000151", "管理员"); break;
            case 1: role = GetTran("000388", "店铺"); break;
            case 2: role = GetTran("000599", "会员"); break;
        }
        return role;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["type"].ToString() == "first.aspx")
        {
            Response.Redirect(Request.QueryString["type"].ToString());
        }
        else
        {
            Response.Redirect(Request.QueryString["source"].ToString());
        }
    }
}