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
using System.Data.SqlClient;
/// <summary>
/// Add Namespace
/// </summary>
using Model;
using BLL.other.Store;
using BLL.other.Company;
using DAL;

public partial class Member_WriteEmail : BLL.TranslationBase
{

    protected string msg;
    protected int MessagesendId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        if (!this.IsPostBack)
        {
            this.inite_Bianhao();
            this.BindRadioMsgClass();

        }

        TranControls(btnSave, new string[][] 
                        {
                            new string[] { "000497","发 布"}                             
                        }
         );


        TranControls(btnCancle, new string[][] 
                        {
                            new string[] { "000839","取 消"}                             
                        }
           );
    }

    private void inite_Bianhao()
    {
        this.txtNumber.Items.Add(new ListItem(GetTran("000842", "信息管理员"), BLL.CommonClass.CommonDataBLL.getManageID(1)));
    }
    private void BindRadioMsgClass()
    {
        System.Data.SqlClient.SqlDataReader dr = DAL.DBHelper.ExecuteReader("Select ID,ClassName From MsgClass");
        while (dr.Read())
        {
            ListItem list = new ListItem(dr["ClassName"].ToString(), dr["id"].ToString());
            this.RadioListClass.Items.Add(list);
        }
        dr.Close();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        MessageSendBLL msb = new MessageSendBLL();
        if (this.txtNumber.SelectedItem.Text.Trim() == "")
        {
            msg = "<Script language='javascript'>alert('" + GetTran("000848", "对不起，编号不能为空") + "！');</Script>";
            ClientScript.RegisterStartupScript(this.GetType(), "", msg);
            return;
        }
        string sqlStr = "";
        switch (drop_LoginRole.SelectedValue)
        {
            case "0": sqlStr = "Select count(0) From Manage where Number='" + this.txtNumber.SelectedValue.Trim() + "'"; break;
            case "1": sqlStr = "Select count(0) From MemberInfo where Number='" + this.txtNumber.SelectedValue.Trim() + "'"; break;
            case "2": sqlStr = "Select count(0) From StoreInfo where storeid='" + this.txtNumber.SelectedValue.Trim() + "'"; break;
        }
        if (msb.check(sqlStr) != 1)
        {
            msg = "<Script language='javascript'>alert('" + GetTran("000850", "对不起") + "，" + drop_LoginRole.SelectedItem.Text + "" + GetTran("000854", "的编号错误") + "！');</Script>";
            ClientScript.RegisterStartupScript(this.GetType(), "", msg);
            return;
        }
        if (this.RadioListClass.SelectedValue.Equals(""))
        {
            msg = "<script language='javascript'>alert('" + GetTran("007712", "请选择邮件分类") + "！！！');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "", msg);

            return;
        }
        if (this.txtTitle.Text.Trim() == "")
        {
            msg = "<Script language='javascript'>alert('" + GetTran("000859", "对不起，标题信息不能为空") + "！');</Script>";
            ClientScript.RegisterStartupScript(this.GetType(), "", msg);
            return;
        }
        if (this.content1.Value.Trim().Length.Equals(0))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007399", "请输入邮件内容") + "！！！');</script>");

            return;
        }
        if (this.content1.Value.Trim().Length > 4000)
        {
            msg = "<script language='javascript'>alert('" + GetTran("000863", "您输入的信息过长") + "！！！');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "", msg);

            return;
        }
        DateTime date = DateTime.Now;
        MessageSendModel msm = new MessageSendModel();
        ///表示会员
        msm.LoginRole = "0";
        msm.Receive = BLL.CommonClass.CommonDataBLL.getManageID(1);
        msm.InfoTitle = this.txtTitle.Text.ToString().Replace("<", "&lt;").Replace(">", "&gt;");
        msm.Content = this.content1.Value.Trim();
        msm.Sender = Session["Member"].ToString();
        msm.Senddate = date;
        msm.SenderRole = "2";
        msm.MessageClassID = Convert.ToInt16(this.RadioListClass.SelectedValue);
        msm.MessageType = 'm';
        int i = 0;
        i = msb.MemberSendToManage(msm);
        if (i > 0)
        {
            msg = "<Script language='javascript'>alert('" + GetTran("007400", "邮件发送成功") + "！');</script>";
        }
        else
        {
            msg = "<Script language='javascript'>alert('" + GetTran("007401", "邮件发送失败") + "！');</script>";
        }
        ClientScript.RegisterStartupScript(this.GetType(), "", msg);

        btnCancle_Click(null, null);
    }

    protected void btnCancle_Click(object sender, EventArgs e)
    {
        this.txtTitle.Text = "";
        this.content1.Value = "";
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        btnSave_Click(null, null);
    }
}
