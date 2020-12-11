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
using System.Data.SqlClient;

public partial class Company_ManageMessage_Recive : BLL.TranslationBase
{
    int type = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            if (Request.QueryString["type"] != null)
            {
                type = 1;
            }

            Response.Cache.SetExpires(DateTime.Now);
            Permissions.CheckManagePermission(EnumCompanyPermission.ManageMessageRecive);
            givShowMessage.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
            string manageId = BLL.CommonClass.CommonDataBLL.getManageID(1);
            if (Session["Company"].ToString() != manageId)
            {
                //this.tr_Manager.Visible = false;
            }
            this.txtdatastrat.Text = CommonDataBLL.GetDateBegin().ToString();
            this.txtendDate.Text = CommonDataBLL.GetDateEnd().ToString();
            this.Button_Submit.Attributes.Add("onClick", "return confirm('" + GetTran("kwl", "你确定提交此操作吗") + "?')");
            this.BindDropMsgClass();
            PageBind();
            this.HyperClassSetting.Visible = true;
        }
    }

    protected void Translations_More()
    {
        TranControls(givShowMessage, new string[][] 
                        {
                            new string[] { "001689","转发"}, 
                            new string[] { "000015","操作"}, 
                            new string[] { "000015","操作"},
                            new string[] { "007403","展开相关邮件"},
                            new string[] { "000912","接收对象"}, 
                            new string[] { "001721","发送人"}, 
                            new string[] { "000908","发送编号"}, 
                            new string[] { "000825","信息标题"}, 
                            new string[] { "000720","发布日期"}, 
                            new string[] { "000594","处理状态"},
                            new string[] { "007398","邮件分类"}
                        }
                    );
        TranControls(DropHandleStatus, new string[][]{
            new string[]{"000881","不限"},
            new string[]{"007874","未读"},
            new string[]{"000994","已阅读"},
            new string[]{"007254","处理中"},
            new string[]{"007717","已处理"}
         });
        TranControls(DropMsgClass_2, new string[][] { new string[] { "007877", "不转发" } });
        TranControls(DropHandleStatus_2, new string[][] { new string[] { "007878", "不修改" }, new string[] { "007717", "已处理" }, new string[] { "007254", "处理中" } });
        TranControls(DropMsgClass, new string[][] { new string[] { "000881", "不限" } });

        TranControls(btnSeach, new string[][] 
                        {
                            new string[] { "000048","查 询"}                             
                        }
          );

        TranControls(this.Button_Submit, new string[][] 
                        {
                            new string[] { "000321","提交"}                             
                        }
          );

    }
    private void BindDropMsgClass()
    {
        string classidlist = "";
        System.Data.SqlClient.SqlDataReader dr = DBHelper.ExecuteReader(" select a.ID,a.ClassName From MsgClass a,MsgClassAdmin b where a.ID=b.ClassID and b.Admin=@user", new SqlParameter("@user", Session["Company"]), CommandType.Text);
        while (dr.Read())
        {
            ListItem list = new ListItem(dr["ClassName"].ToString(), dr["id"].ToString());

            this.DropMsgClass.Items.Add(list);
            classidlist += classidlist + ",";
        }
        ViewState["ClassIDList"] = classidlist.TrimEnd(",".ToCharArray());
        if (Request.QueryString["ClassID"] != null)
        {
            this.DropMsgClass.SelectedValue = Request.QueryString["ClassID"].ToString();
        }

        dr = DBHelper.ExecuteReader(" select ID,ClassName From MsgClass");
        while (dr.Read())
        {
            ListItem list = new ListItem(dr["ClassName"].ToString(), dr["id"].ToString());

            this.DropMsgClass_2.Items.Add(list);
        }
        dr.Close();
    }



    public string GetBiaoZhunTime(string dt)
    {
        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }

    #region 角色
    protected string jieshuoduixing(string obj)
    {
        switch (obj.ToString().Trim())
        {
            case "0":
                return GetTran("000151", "管理员");
            case "2":
                return GetTran("000599", "会员");
            case "1":
                return GetTran("000388", "店铺");
            case "3":
                return GetTran("006546", "分公司");
            default:
                return GetTran("000599", "会员");

        }

    }
    #endregion
    protected void PageBind()
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
            ScriptHelper.SetAlert(btnSeach, GetTran("001735", "输入的开始时间格式不对") + "！！！");
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
            ScriptHelper.SetAlert(btnSeach, GetTran("001732", "输入的结束时间格式不对") + "！！！");
            return;
        }
        if (this.TxtBianhao.Text.Trim().Length > 0)
        {
            System.Data.SqlClient.SqlDataReader dr = DBHelper.ExecuteReader("Select ID From MemberInfo where Number=@number", new System.Data.SqlClient.SqlParameter("@number", this.TxtBianhao.Text.Trim()), CommandType.Text);
            if (!dr.Read())
            {
                ScriptHelper.SetAlert(btnSeach, GetTran("000794", "对不起,该会员编号不存在") + "！！！");
                return;
            }
        }

        sb.Append("DropFlag=0 and LoginRole=0 ");
         if (!ViewState["ClassIDList"].ToString().Equals(""))
         {
             sb.Append(" and ClassID in(" + ViewState["ClassIDList"].ToString() + ") ");
         }

        sb.Append("and dateadd(hour," + BLL.other.Company.WordlTimeBLL.ConvertAddHours() + ",Senddate) between '");
        sb.Append(txtdatastrat.Text.Trim() + " 00:00:00");
        sb.Append("' and '" + txtendDate.Text.Trim() + " 23:59:59'");
        sb.Append(" and a.ID not in(select MessageID from dbo.F_DroppedReceiveByOperator('" + Session["Company"] + "',0))");
        if (!this.DropMsgClass.SelectedValue.Equals("-1"))
        {
            sb.Append(" and ClassID=" + this.DropMsgClass.SelectedValue);
        }
        if (!this.DropHandleStatus.SelectedValue.Equals("-1"))
        {
            sb.Append(" and ReadFlag=" + this.DropHandleStatus.SelectedValue);
        }
        if (this.TxtBianhao.Text.Trim().Length > 0)
        {
            sb.Append(" and sender='" + this.TxtBianhao.Text.Trim() + "'");
        }
        if (type == 1)
        {
            sb.Append(" and ReadFlag=0");
        }


        ViewState["where"] = sb.ToString();
        Pager page = Page.FindControl("Pager1") as Pager;
        ViewState["pagewhere"] = sb.ToString();
        page.PageBind(0, 10, "MessageReceive a left join MsgClass b on a.ClassID=b.ID", "a.ID, MessagesendID, LoginRole, Receive, InfoTitle, SenderRole, Sender, Senddate, DropFlag, case ReadFlag when 0 then '" + GetTran("007874", "未读") + "' when 1 then '" + GetTran("000994", "已阅读") + "' when 2 then '" + GetTran("007254", "处理中") + "' when 3 then '" + GetTran("007717", "已处理") + "' else 'Unknown' end HandleStatus, ReplyFlag,b.ClassName", sb.ToString(), "a.ID", "givShowMessage");

        Translations_More();
    }
    protected void btnSeach_Click(object sender, EventArgs e)
    {
        PageBind();
    }
    protected void givShowMessage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        MessageReceiveBLL message = new MessageReceiveBLL();
        string name = e.CommandName;
        int id = Convert.ToInt32(e.CommandArgument);
        if ("Del" == name)
        {
            Response.Cache.SetExpires(DateTime.Now);
            Permissions.CheckManagePermission(EnumCompanyPermission.ManageMessageReciveDelete);
            if (message.DelMessageReceive(id, Session["Company"].ToString(),0))
            {
                ScriptHelper.SetAlert(Page, GetTran("000008", "删除成功") + "！！！");

            }
            else
                ScriptHelper.SetAlert(Page, GetTran("000009", "删除失败") + "！！！");
            //Pager page = Page.FindControl("Pager1") as Pager;
            //string wherepage= ViewState["pagewhere"].ToString();
            //page.PageBind(0, 10, "MessageReceive", "ID, MessagesendID, LoginRole, Receive, InfoTitle, SenderRole, Sender, Senddate, DropFlag, ReadFlag, ReplyFlag", wherepage, "ID", "givShowMessage");
            PageBind();
        }
        else if ("huifu" == name)
        {
            if (this.DropMsgClass.SelectedValue.Equals("-1"))
            {
                Response.Redirect("ManageMessage.aspx?type=huifu&hfid=" + id);
            }
            else
            {
                Response.Redirect("ManageMessage.aspx?type=huifu&hfid=" + id + "&ClassID=" + this.DropMsgClass.SelectedValue);
            }
        }
    }


    protected void btnExecl_Click(object sender, EventArgs e)
    {

        Response.AppendHeader("Content-Disposition", "attachment;filename=registerStore.xls");
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-7");

        string cmd = @"select MessagesendID, case LoginRole when 0 then N'" + GetTran("000151", "管理员") + "' when 1 then N'" + GetTran("000388", "店铺") + "' when 2 then N'" + GetTran("000599", "会员") + "' when 3 then N'" + GetTran("006546", "分公司") + "' end as LoginRole, Receive, InfoTitle, SenderRole, Sender, Senddate, DropFlag, ReadFlag, ReplyFlag from MessageReceive where " + ViewState["pagewhere"];

        DataTable dt = DBHelper.ExecuteDataTable(cmd);

        StringBuilder sb = Excel.GetExcelTable(dt, GetTran("000990", "收件箱表"), new string[] { "LoginRole=" + GetTran("000912", "接收对象"), "Receive=" + GetTran("000910", "接收编号"), "Sender=" + GetTran("000908", "发送编号"), "InfoTitle=" + GetTran("000825", "信息标题"), "Senddate=" + GetTran("000752", "发送时间") });

        Response.Write(sb.ToString());
        Response.End();
    }

    /// <summary>
    /// Override
    /// </summary>
    /// <param name="control"></param>
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
            Delete = (int)Permissions.GetPermissions(Model.Other.EnumCompanyPermission.ManageMessageReciveDelete);
            if (Delete == 0)
            {
                ((LinkButton)e.Row.FindControl("Button1")).Visible = false;
            }
            else
            {
                ((LinkButton)e.Row.FindControl("Button1")).Visible = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations_More();
        }
    }
    protected void Button_Submit_Click(object sender, EventArgs e)
    {
        if (this.DropMsgClass_2.SelectedValue.Equals("-1") && this.DropHandleStatus_2.SelectedValue.Equals("-1"))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("kwl", "请选择转发，或修改状态") + ".');</script>");
            return;
        }
        /**/
        int xz = 0;
        xz = (int)Permissions.GetPermissions(Model.Other.EnumCompanyPermission.ManageMessageRecive_ZF);
        if (xz == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("kwl", "无转发权限") + "');</script>");
            return;
        }

        /**/


        if (this.givShowMessage.Rows.Count == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001729", "没有信件") + "！')</script>");
            return;
        }

        bool blean = false;
        string where = "";
        for (int i = 0; i < givShowMessage.Rows.Count; i++)
        {
            CheckBox chkSelect = givShowMessage.Rows[i].FindControl("chk") as CheckBox;
            if (chkSelect.Checked == true)
            {
                string ID = givShowMessage.DataKeys[i].Value.ToString();
                blean = true;
                where += ID + ",";
            }
        }

        if (blean == false)
        {
            this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001717", "请选择信件") + "！')</script>");
            return;
        }

        where = where.TrimEnd(",".ToCharArray());

        string sql_send = "update MessageSend set ";
        string sql_receive = "update MessageReceive set ";
        string sql_transmithistory = "";


        if (!this.DropMsgClass_2.SelectedValue.Equals("-1"))
        {
            sql_send += "ClassID=@classid";
            sql_receive += "ClassID=@classid";
            sql_transmithistory += "insert into MessageTransmitHistory(MessageID,OldClassID,NewClassID,Transmitor) select ID,ClassID,@classid,@user from MessageReceive where id in(" + where + ");";

        }

        if (!this.DropHandleStatus_2.SelectedValue.Equals("-1"))
        {
            sql_send += (sql_send.EndsWith(" ") ? "" : ",") + "ReadFlag=@handle";

            sql_receive += (sql_receive.EndsWith(" ") ? "" : ",") + "ReadFlag=@handle";
        }
        string sql = sql_transmithistory + sql_send + " where id in(" + where + ");" + sql_receive + " where id in(" + where + ");";

        System.Data.SqlClient.SqlParameter[] sp = { new System.Data.SqlClient.SqlParameter("@classid", this.DropMsgClass_2.SelectedValue),
                                                        new SqlParameter("@user",Session["Company"].ToString()),
                                                        new SqlParameter("@handle",this.DropHandleStatus_2.SelectedValue)};
        using (SqlConnection conn = new SqlConnection(DBHelper.connString))
        {
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            int rtn;
            try
            {
                rtn = DAL.DBHelper.ExecuteNonQuery(tran, sql, sp, CommandType.Text);
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
            finally
            {
                conn.Close();
            }
            if (rtn > 0)
            {
                this.PageBind();
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("000001", "提交成功") + "!')</script>");
                this.DropMsgClass_2.SelectedValue = "-1";
                this.DropHandleStatus_2.SelectedValue = "-1";
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001507", "对不起，操作失败") + "!')</script>");

            }
        }
    }


    protected void LinkClassSetting_Click(object sender, EventArgs e)
    {

    }
}

