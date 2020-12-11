using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Text;
using System.IO;
using System.Globalization;
using BLL.CommonClass;
using BLL.other.Company;
using Model;

public partial class MemberMobile_shoujianx : BLL.TranslationBase
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
            ViewState["classid"] = "";
            if (Request.QueryString["type"] != null)
            {
                ViewState["classid"] = Request.QueryString["type"];
            }
            GetShopList(ViewState["classid"].ToString());
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
        //                    new string[] { "000906","是否阅读"},
        //                    new string[] { "007403","展开相关邮件"},
        //                    new string[] { "000022","删除"}
        //                }
        //            );


        TranControls(BtnConfirm, new string[][]
                        {
                            new string[] { "000048","查 询"}
                        }
          );
        //TranControls(rep, new string[][]
        //                {
        //                   new string[] { "000022","删除"}
        //                }
        //   );

    }


    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        if (this.txtBeginTime.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000763", "起始时间不能为空") + "！');</script>");

            return;
        }
        if (this.txtEndTime.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000759", "终止时间不能为空") + "！');</script>");
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
        GetShopList(ViewState["classid"].ToString());
        Translations_More();
    }

    public string GetBiaoZhunTime(string dt)
    {
        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
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
    /// <summary>
    /// 获取发送邮件的对应级别
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    protected string GetService(string id)
    {
        string res = GetTran("000996", "全体成员");
        try
        {
            string sql = "select isnull(levelstr,'')as levelstr FROM BSCO_Level as b, dbo.MessageReadCondition as m  where b.levelint=m.ConditionLevel and b.levelflag=0 and MessageID=" + id;
            res = DAL.DBHelper.ExecuteScalar(sql).ToString();
        }
        catch
        {
            res = GetTran("000996", "全体成员");
        }
        return res;
    }

    private void GetShopList(string classid)
    {
        ///注意会员的登录角色是2
        string table = "MessageReceive";
        StringBuilder sb = new StringBuilder();
        string wheretime = "";
        if (Session["languageCode"].ToString() == "L001")
        {

            sb.Append("DropFlag=0 and LoginRole=2 and Receive like '%" + Session["Member"] + "%' and MessageType='m' and ID not in(select MessageID from dbo.F_DroppedReceiveByOperator('" + Session["Member"].ToString() + "',2))");
        }

        else if (Session["languageCode"].ToString() == "L002")
        {

            sb.Append("DropFlag=0 and LoginRole=2 and (Receive like '%" + Session["Member"] + "%' or  Receive='*') and MessageType='m' and ID not in(select MessageID from dbo.F_DroppedReceiveByOperator('" + Session["Member"].ToString() + "',2)) and CountryCode=1");


        }
        if (classid.Trim() != "")
        {
            sb.Append(" and classid=" + classid);
        }
        if (this.txtBeginTime.Text != "" && this.txtEndTime.Text != "")
        {
            string beginDate = this.txtBeginTime.Text.Trim() + " 00:00:00";
            string endDate = this.txtEndTime.Text.Trim() + " 23:59:59";
            if (Convert.ToDateTime(beginDate) > Convert.ToDateTime(endDate))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000755", "起始时间不能大于终止之间") + "！');</script>");
                return;
            }
            wheretime = "dateadd(hour," + BLL.other.Company.WordlTimeBLL.ConvertAddHours() + ",Senddate)>='" + beginDate + "'" + " and  dateadd(hour," + BLL.other.Company.WordlTimeBLL.ConvertAddHours() + ",Senddate)<='" + endDate + "'";
            sb.Append("and " + wheretime);
        }



        SqlConnection conn = new SqlConnection(DAL.DBHelper.connString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "MessageCheckCondition";
        conn.Open();

        string rtn = "";
        string idlist = "";
        DataTable dt = DAL.DBHelper.ExecuteDataTable("select ID,MessageSendID from MessageReceive where DropFlag=0 and loginRole=2 and Receive='*' and ID not in(select MessageID from dbo.F_DroppedReceiveByOperator('" + Session["Member"].ToString() + "',2)) and MessageType='m' and " + wheretime);
        foreach (DataRow row in dt.Rows)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@MessageSendID", SqlDbType.Int).Value = Convert.ToInt32(row["MessageSendID"]);
            cmd.Parameters.Add("@number", SqlDbType.VarChar).Value = Session["Member"].ToString();
            cmd.Parameters.Add("@type", SqlDbType.Char).Value = '2';
            cmd.Parameters.Add("@rtn", SqlDbType.Char, 1).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            rtn = cmd.Parameters["@rtn"].Value.ToString();
            if (rtn.Equals("1"))
            {
                idlist += row["ID"].ToString() + ",";
            }
        }
        conn.Close();
        idlist = idlist.TrimEnd(",".ToCharArray());

        //对自己本身的公告查询

        string sql1 = "SELECT ma.MessageID FROM dbo.MessageReadCondition AS ma,dbo.MessageSend AS me WHERE ma.MessageID=me.ID and DropFlag=0 and me.SenderRole=0 and me.Receive='*' and me.LoginRole=2  and MessageType='m'and  ma.ConditionLeader='" + Session["Member"].ToString() + "'";
        DataTable dt1 = DAL.DBHelper.ExecuteDataTable(sql1);
        if (dt1.Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                idlist += "," + dt1.Rows[i]["MessageID"].ToString();
            }
        }
        idlist = idlist.TrimStart(",".ToCharArray());
        if (Session["languageCode"].ToString() == "L001")
        {
            if (!idlist.Equals("") && classid == "")
            {
                sb.Append(" or ID in(" + idlist + ") and CountryCode!=1");
            }

            ViewState["SQLSTR"] = "select * from MessageReceive where " + sb.ToString();

        }
        else if (Session["languageCode"].ToString() == "L002")
        {
            ViewState["SQLSTR"] = "select * from MessageReceive where " + sb.ToString();


        }


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

    protected string gethandlestatus(string str)    // 前台调用(接受对象的转换)
    {
        switch (str.Trim())
        {
            case "0":
                return GetTran("007295", "未读邮件");
            case "1":
                return GetTran("000994", "已阅读");
            case "2":
                return GetTran("007254", "处理中");
            case "3":
                return GetTran("007717", "已处理");
            default:
                return GetTran("001416", "未知");
        }
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExcel_Click(object sender, EventArgs e)
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

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        MessageSendBLL msb = new MessageSendBLL();
        MessageReciveModel mrm = new MessageReciveModel();
        GridViewRow row = (GridViewRow)((Image)e.CommandSource).NamingContainer;
        int type = Convert.ToInt32((row.FindControl("Label1") as Label).Text);
        switch (e.CommandName.Trim())
        {
            case "del":
                if (msb.getReceive(type).ToString() == "*")
                {
                    msg = "<Script language='javascript'>alert('" + GetTran("001000", "无删除权限") + "！');</script>";
                }
                else
                {
                    /*int i = msb.getMessageReciveInfo(type);
                    if (i > 0)
                    {
                        msg = "<Script language='javascript'>alert('信息删除成功！');</script>";
                    }*/

                    if (new MessageReceiveBLL().DelMessageReceive(type, Session["Member"].ToString(), 2))
                    {
                        ScriptHelper.SetAlert(Page, GetTran("000008", "删除成功") + "！！！");

                    }
                    else
                        ScriptHelper.SetAlert(Page, GetTran("000008", "删除成功") + "！！！");
                }
                break;
            default:
                break;
        }
        GetShopList(ViewState["classid"].ToString());
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ///控制样式
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");

            TableCell cc = (TableCell)e.Row.Controls[1];
            if (cc.Text == "*")
                cc.Text = "全体成员";
            if (cc.Text.IndexOf(",") > 0)
                cc.Text = Session["Member"].ToString();
            Image LButton = (Image)e.Row.FindControl("Linkbutton2");
            MessageSendBLL msb = new MessageSendBLL();
            Label Hid = (Label)e.Row.FindControl("Label1");
            Label lbyuedu = (Label)e.Row.FindControl("lbyuedu");
            lbyuedu.Text = GetTran("000919", "未阅读");
            if (msb.getReceive(int.Parse(Hid.Text)).ToString() == "*")
            {
                LButton.ImageUrl = "images/view-button3-.png";
                LButton.Enabled = false;
            }
            else
            {
                LButton.Attributes.Add("onClick", "return confirm('" + GetTran("000995", "你确定要删除该条信息吗") + "?');");
                LButton.ImageUrl = "images/view-button3.png";
            }
            int isyuedu = msb.getReadType(Hid.Text.ToString());
            if (isyuedu == 1)
            {
                lbyuedu.Text = GetTran("000994", "已阅读");
                lbyuedu.Enabled = false;
            }
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations_More();
        }
    }
    protected void rep_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        MessageSendBLL msb = new MessageSendBLL();
        MessageReciveModel mrm = new MessageReciveModel();
        //GridViewRow row = (GridViewRow)((Image)e.CommandSource).NamingContainer;
        int type = Convert.ToInt32(e.CommandArgument);
        if (msb.getReceive(type).ToString() == "*")
        {
            msg = "<Script language='javascript'>alert('" + GetTran("001000", "无删除权限") + "！');</script>";
        }
        else
        {
            /*int i = msb.getMessageReciveInfo(type);
            if (i > 0)
            {
                msg = "<Script language='javascript'>alert('信息删除成功！');</script>";
            }*/

            if (new MessageReceiveBLL().DelMessageReceive(type, Session["Member"].ToString(), 2))
            {
                ScriptHelper.SetAlert(Page, GetTran("000008", "删除成功") + "！！！");

            }
            else
                ScriptHelper.SetAlert(Page, GetTran("000008", "删除成功") + "！！！");
        }

    }
}
