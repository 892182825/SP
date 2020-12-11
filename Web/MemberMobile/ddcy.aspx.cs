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

///Add Namespace
using BLL.CommonClass;
using BLL.other.Member;
using System.Globalization;
using System.IO;
using System.Text;
using System.Collections.Generic;



public partial class MemberMobile_ddcy : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        ///设置GridView的样式
        //gvMessageSend.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!this.Page.IsPostBack)
        {
            txtBeginTime.Text = CommonDataBLL.GetDateBegin();
            txtEndTime.Text = CommonDataBLL.GetDateEnd();
            GetShopList();
        }
        Translations_More();
    }

    protected void Translations_More()
    {
        //TranControls(gvMessageSend, new string[][]
        //                {

        //                     new string[] { "000724","公告标题"},
        //                    new string[] { "000726","发送人编号"},
        //                    new string[] { "000720","发布日期"},
        //                    new string[]{"000399","查看详细"}
        //                }
        //            );

        TranControls(btnConfirm, new string[][]
                        {
                            new string[] { "000048","搜 索"}
                        }
            );
    }


    string conditions = "DropFlag=0 and SenderRole=0 and Receive='*' and LoginRole=2 and MessageType='a'";

    private void GetShopList()
    {
        #region
        SqlConnection conn = new SqlConnection(DAL.DBHelper.connString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "MessageCheckCondition";
        conn.Open();
        if (Session["languageCode"].ToString() == "L001")
        {

            conditions += "and CountryCode!=1";
        }
        else
        {

            conditions += "and CountryCode=1";
        }


        string rtn = "";
        string idlist = "";
        //DataTable dt = DAL.DBHelper.ExecuteDataTable("select ID from MessageSend  join MessageReadCondition on MessageSend.ID=MessageReadCondition.MessageID   where " + conditions + "and  MessageReadCondition.ConditionLevel=(select levelint from memberinfo where number='" + Session["Member"].ToString() + "')");
        DataTable dt = DAL.DBHelper.ExecuteDataTable("select ID from MessageSend where " + conditions);
        foreach (DataRow row in dt.Rows)
        {
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@MessageSendID", SqlDbType.Int).Value = Convert.ToInt32(row["ID"]);
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
        string sql1 = "SELECT ma.MessageID FROM dbo.MessageReadCondition AS ma,dbo.MessageSend AS me WHERE ma.MessageID=me.ID and DropFlag=0 and me.SenderRole=0 and me.Receive='*' and me.LoginRole=2 and ma.ConditionLeader='" + Session["Member"].ToString() + "'";
        DataTable dt1 = DAL.DBHelper.ExecuteDataTable(sql1);
        if (dt1.Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                idlist += "," + dt1.Rows[i]["MessageID"].ToString();
            }
        }
        idlist = idlist.TrimStart(",".ToCharArray());
        string where = "";
        if (!idlist.Equals(""))
        {
            where = "ID in(" + idlist + ") and MessageType='a'";
        }
        else
        {
            where = "1=2";
        }
       // Pager1.PageBind(0, 10, "MessageSend", "ID, LoginRole, Receive, InfoTitle, Content, SenderRole, Sender, Senddate, DropFlag, ReadFlag", conditions + " and " + where, "id", "rep_TransferList");

        #endregion



        #region
        //SqlConnection conn = new SqlConnection(DAL.DBHelper.connString);
        //SqlCommand cmd = new SqlCommand();
        //cmd.Connection = conn;
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.CommandText = "MessageCheckCondition";
        //conn.Open();

        //string rtn = "";
        //List <string> idlist =new List<string>() ;
        //List< string> idlist2=new List<string>();
        ////查询所有公告id
        //DataTable dt = DAL.DBHelper.ExecuteDataTable("select ID from MessageSend where " + conditions );
        //foreach (DataRow row in dt.Rows)
        //{
        //    cmd.Parameters.Clear();
        //    cmd.Parameters.Add("@MessageSendID", SqlDbType.Int).Value = Convert.ToInt32(row["ID"]);
        //    cmd.Parameters.Add("@number", SqlDbType.VarChar).Value = Session["Member"].ToString();
        //    cmd.Parameters.Add("@type", SqlDbType.Char).Value = '2';
        //    cmd.Parameters.Add("@rtn", SqlDbType.Char, 1).Direction = ParameterDirection.Output;
        //    cmd.ExecuteNonQuery();
        //    rtn = cmd.Parameters["@rtn"].Value.ToString();
        //    //if (rtn.Equals("1"))
        //    //{
        //    idlist.Add (","+ row["ID"].ToString());
        //    //}
        //}
        //conn.Close();
        ////idlist = idlist.TrimEnd(",".ToCharArray());
        //string sql1 = "";
        ////对自己本身的公告查询
        //sql1 = "SELECT ma.MessageID FROM dbo.MessageReadCondition AS ma,dbo.MessageSend AS me WHERE ma.MessageID=me.ID and DropFlag=0 and me.SenderRole=0 and me.Receive='*' and me.LoginRole=2 and ma.ConditionLeader='" + Session["Member"].ToString() + "' order by me.Senddate desc";
        //DataTable dt1 = DAL.DBHelper.ExecuteDataTable(sql1);
        //if (dt1.Rows.Count > 0)
        //{
        //    for (int i = 0; i < dt1.Rows.Count; i++)
        //    {
        //        idlist.Add( "," + dt1.Rows[i]["MessageID"].ToString());
        //    }
        //}
        //else
        //{
        //    //查询不在此安置团队的公告id
        //    string sql2 = "SELECT ma.MessageID FROM dbo.MessageReadCondition AS ma,dbo.MessageSend AS me WHERE ma.MessageID=me.ID and DropFlag=0 and me.SenderRole=0 and me.Receive='*' and me.LoginRole=2 and ((ma.ConditionLeader!=''  and ma.ConditionLeader !='" + Session["Member"].ToString() + "') or ma.ConditionLevel!=(select LevelInt from memberinfo where Number='" + Session["Member"].ToString() + "')) order by me.Senddate desc ";
        //    DataTable dt2 = DAL.DBHelper.ExecuteDataTable(sql2);
        //    if (dt2.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt2.Rows.Count; i++)
        //        {
        //            idlist2.Add( "," + dt2.Rows[i]["MessageID"].ToString());
        //        }
        //    }
        //}
        ////去除其他安置团队的公告
        //for (int i = 0; i < idlist2.Count; i++)
        //{
        //    for (int j = 0; j < idlist.Count; j++)
        //    {
        //        if (idlist2[i].Equals(idlist[j]))
        //        {
        //            idlist.RemoveAt(j);
        //        }
        //    }
        //}
        //string s = "";
        //for (int i = 0; i < idlist.Count; i++)
        //{
        //    s += idlist[i];
        //}
        //s = s.TrimStart(",".ToCharArray());
        //string where = "";
        //if (!s.Equals(""))
        //{
        //    where = "ID in(" + s + ") and MessageType='a'";
        //}
        //else
        //{
        //    where = "1=2";
        //}
        //Pager1.PageBind(0, 10, "MessageSend", "ID, LoginRole, Receive, InfoTitle, Content, SenderRole, Sender, Senddate, DropFlag, ReadFlag", conditions + " and " + where, "id", "rep_TransferList");//rep_TransferList
        #endregion
    }

    public string GetBiaoZhunTime(string dt)
    {
        return Convert.ToDateTime(dt).AddHours(Convert.ToDouble(Session["WTH"])).ToShortDateString();
    }


    private void GetDataBind()
    {
        //this.rep_TransferList.PageIndex = 0;
        this.GetShopList();
    }
    protected void gvMessageSend_PageIndexChanged(object sender, EventArgs e)
    {
        //this.rep_TransferList.PageIndex = 0;
        this.btnConfirm_Click(null, null);
    }

    /// <summary>
    /// 搜索
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            Convert.ToDateTime(txtBeginTime.Text.Trim());
            Convert.ToDateTime(txtEndTime.Text.Trim());
        }
        catch
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000758", "日期格式不正确，请重新输入") + "！');</script>");
            return;
        }

        if (this.txtBeginTime.Text == "")
        {

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000763", "起始时间不能为空") + "！');</script>");
            return;
        }
        if (this.txtEndTime.Text == "")
        {

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000759", "终止时间不能为空") + "！');</script>");
            return;
        }

        if (this.txtBeginTime.Text.Trim() != "" && this.txtEndTime.Text.Trim() != "")
        {
            string beginDate = txtBeginTime.Text.Trim() + " 00:00:00";
            string endDate = txtEndTime.Text.Trim() + " 23:59:59";
            if (Convert.ToDateTime(beginDate) > Convert.ToDateTime(endDate))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000755", "起始时间不能大于终止之间") + "！');</script>");
                return;
            }
            conditions += ("and  dateadd(hour," + Convert.ToDouble(Session["WTH"]) + ",Senddate)>='" + beginDate + "'" + "and  dateadd(hour," + Convert.ToDouble(Session["WTH"]) + ",Senddate)<='" + endDate + "'");
        }
        //this.gvMessageSend.PageIndex = 0;
        GetShopList();
    }
    protected void gvMessageSend_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations_More();
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
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


    protected void gvMessageSend_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if ("GO" == e.CommandName)
        {
            Response.Redirect("~/Member/MessageContent.aspx?id=" + e.CommandArgument.ToString() + "&T=messagesend&source=QueryInfomation.aspx&type=11");
        }
    }
}
