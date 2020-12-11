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
using DAL;
using BLL.CommonClass;

public partial class Company_ManageAffiche : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);

        int xz = 0;
        xz = (int)Permissions.GetPermissions(Model.Other.EnumCompanyPermission.ManageMessage1);
        if (xz == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + GetTran("000847", "没有权限") + "')", true);
            Response.End();
            return;
        }

        if (!IsPostBack)
        {
            bindcountry();
            bindlange();
            BindDropLevel();
            BindDropRelation();
            this.SetVisible(); 
            td_GongGao.Visible = false;
            td_ReceiveRange.Visible = true;
            txtBianhao.Text = "*";
            if (Request["Id"] != null)
            {
                int id = Convert.ToInt32(Request["Id"].ToString());
                PageBind(id);
            }

          
        }

        Translations_More();
    }

    protected void Translations_More()
    {


        TranControls(drop_LoginRole, new string[][] 
                        {
                            //new string[] { "000151","管理员"}, 
                            new string[] { "000599","会 员"} 
                        }
            );

        TranControls(btn_Save, new string[][] 
                        {
                          
                            new string[] { "000351","提 交"} 
                        }
            );
        TranControls(btn_Cancle, new string[][] 
                        {
                          
                            new string[] { "000839","取 消"} 
                        }
            );
        TranControls(ChkBonus, new string[][] { new string[] { "007869", "奖金区间" } });
        TranControls(ChkNet, new string[][] { new string[] { "007870", "团队" } });

    }

    private void BindDropRelation()
    {
        this.DropRelation.Items.Add(new ListItem(GetTran("kwl", "安置团队"), "1"));
        this.DropRelation.Items.Add(new ListItem(GetTran("kwl", "推荐团队"), "2"));

    }
    private void PageBind(int id)
    {
        MessageSendModel message = MessageSendBLL.GetMessageSendById(id);
        this.txtBianhao.Text = message.Receive;
        this.content1.Value = message.Content;
        this.txtTitle.Text = message.InfoTitle;
        this.drop_LoginRole.SelectedValue = message.LoginRole;
        this.DropDownList1.Enabled = false;
        this.DropDownList2.Enabled = false;
        this.DropDownList1.SelectedValue = message.CountryCode;
        this.DropDownList2.SelectedValue = message.LanguageCode;

        //this.ChkLevel.Checked=(message.ConditionLevel != -1);
        this.ChkBonus.Checked = (message.ConditionBonusFrom != -1 && message.ConditionBonusTo != -1);
        this.ChkNet.Checked = (message.ConditionLeader != "");

        this.BindDropLevel();
        this.SetVisible();
        //this.DropLevel.SelectedValue = message.ConditionLevel.ToString();
        this.TxtBonusFrom.Text = message.ConditionBonusFrom.ToString();
        this.TxtBonusTo.Text = message.ConditionBonusTo.ToString();
        this.TxtLeader.Text = message.ConditionLeader.ToString();
        this.DropRelation.SelectedValue = message.ConditionRelation.ToString();


    }
    private void BindDropLevel()
    {
        string levelflag="-1";
        switch (this.drop_LoginRole.SelectedValue)
        {
            case "0":
                levelflag="-1";
                this.td_ReceiveRange.Visible = false;
                break;
            case "1":
                levelflag="1";
                this.td_ReceiveRange.Visible = true;
                this.SetBonusAndNet(false);
                break;
            case "2":
                levelflag="0";
                this.td_ReceiveRange.Visible = true;
                this.SetBonusAndNet(true);
                break;

        }
        //this.DropLevel.Items.Clear();
        //System.Data.SqlClient.SqlDataReader dr = DBHelper.ExecuteReader("select levelint,levelstr from BSCO_Level where levelflag=" + levelflag );
        //while (dr.Read())
        //{
        //    ListItem list = new ListItem(dr["levelstr"].ToString(), dr["levelint"].ToString());

        //    this.DropLevel.Items.Add(list);
        //}
        //dr.Close();
    }
    private void SetBonusAndNet(bool b)
    {
        if (!b)
        {
            this.TxtBonusFrom.Visible = b;
            this.LblTo.Visible = b;
            this.TxtBonusTo.Visible = b;
            this.TxtLeader.Visible = b;
            this.DropRelation.Visible = b;
            this.ChkBonus.Checked = b;
            this.ChkNet.Checked = b;
        }
        this.ChkBonus.Visible = b;
        this.ChkNet.Visible = b;
   
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        #region 验证用户输入
        if (txtBianhao.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(\"" + GetTran("001607", "请输入收件人编号") + "！！！\");</script>");

            return;
        }
        if (txtTitle.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001609", "请输入标题") + "！！！');</script>");

            Literal1.Text = "";
            return;
        }
        if (this.content1.Value.Trim().Length.Equals(0))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("kwl", "请输入公告内容") + "！！！');</script>");

            return;
        }
        if (this.content1.Value.Trim().Length > 4000)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000863", "您输入的信息过长") + "！！！');</script>");

            return;
        }
        double from = 0;
        double to = 0;
        if (this.ChkBonus.Checked)
        {
            if (string.IsNullOrEmpty(this.TxtBonusFrom.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(' 起始奖金不能为空！');</script>");
                return;
            }
            if (string.IsNullOrEmpty(this.TxtBonusTo.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(' 未尾奖金不能为空！');</script>");
                return;
            }
            try
            {
                from = Convert.ToDouble(this.TxtBonusFrom.Text.Trim());
                to = Convert.ToDouble(this.TxtBonusTo.Text.Trim());
                if (from <= 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(' 起始奖金不能为负数！');</script>");
                    return;
                }
                if (from <= 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(' 未尾奖金不能负数！');</script>");
                    return;
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("kwl", "您输入的奖金区间无效") + "！！！');</script>");
                return;
            }
            if (from >= to)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("kwl", "您输入的奖金区间无效") + "！！！');</script>");
                return;
            }
        }

        if (this.drop_LoginRole.SelectedValue.Equals("2") && this.ChkNet.Checked)
        {
            if (this.TxtLeader.Text.Trim().Length.Equals(0))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("kwl", "请指定团队领导人编号") + "！！！');</script>");
                return;
            }
            else
            {
                if(!MessageSendBLL.CheckNumber(2,this.TxtLeader.Text.Trim()))
                {
                     ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("kwl", "指定的团队领导人不存在") + "！！！');</script>");
                     return;
                }
            }
        }
        #endregion
        StringBuilder sb = new StringBuilder();
  
        if (Request["Id"] != null)
        {
            int id = Convert.ToInt32(Request["Id"]);

            ChangeLogs cl = new ChangeLogs("MessageSend", "ltrim(rtrim(id))");
            cl.AddRecord(id);

            ChangeLogs clsend = new ChangeLogs("MessageReceive", "ltrim(rtrim(messagesendid))");
            clsend.AddRecord(id);


            BLL.other.Company.MessageReceiveBLL bll = new MessageReceiveBLL();
            bll.delGongGao(id);

            cl.ModifiedIntoLogs(ChangeCategory.company19, Session["Company"].ToString(), ENUM_USERTYPE.objecttype10);
            clsend.ModifiedIntoLogs(ChangeCategory.company19, Session["Company"].ToString(), ENUM_USERTYPE.objecttype10);

            this.btn_Save.Text = GetTran("000259", "修改");
            sb.Append(GetTran("001621", "公告修改"));
        }
        else
        {

            sb.Append(GetTran("001620", "公告发布"));
        }
        MessageSendModel messagesend = new MessageSendModel();

        //messagesend.Content = TextBox1.Text.Trim();
        messagesend.Content = this.content1.Value.Trim();
        messagesend.DropFlag = 0;
        messagesend.InfoTitle = txtTitle.Text.Trim().Replace("<", "&lt;").Replace(">", "&gt;");
        messagesend.LoginRole = drop_LoginRole.SelectedItem.Value;
        messagesend.ReadFlag = 0;

        messagesend.Sender = Session["Company"].ToString();
        messagesend.SenderRole = "0";
        messagesend.Receive = "*";

        messagesend.CountryCode = this.DropDownList1.SelectedValue;
        messagesend.LanguageCode = this.DropDownList2.SelectedValue;
        //if (this.ChkLevel.Checked)
        //{
        //    messagesend.ConditionLevel = Convert.ToInt16(this.DropLevel.SelectedValue);
        //}
        //else
        //{
        //    messagesend.ConditionLevel = -1;
        //}
        if (this.ChkBonus.Checked)
        {
            messagesend.ConditionBonusFrom = Convert.ToDouble(this.TxtBonusFrom.Text.Trim());
            messagesend.ConditionBonusTo = Convert.ToDouble(this.TxtBonusTo.Text.Trim());
        }
        else
        {
            messagesend.ConditionBonusFrom = -1;
            messagesend.ConditionBonusTo = -1;
        }
        if (this.ChkNet.Checked)
        {
            messagesend.ConditionRelation = Convert.ToChar(this.DropRelation.SelectedValue);
            messagesend.ConditionLeader = this.TxtLeader.Text.Trim();
        }
        else
        {
            messagesend.ConditionLeader = "";
            messagesend.ConditionRelation = '0';
        }
        messagesend.Qishu = CommonDataDAL.getMaxqishu();
        messagesend.MessageType = 'a';
        if (MessageSendBLL.Addsendaffiche(messagesend))
        {
            BLL.other.Company.MessageReceiveBLL bll = new MessageReceiveBLL();

            if (this.drop_LoginRole.SelectedValue == "1")
            {
                bll.UpdateStore();

            }
            else if (this.drop_LoginRole.SelectedValue == "2")
            {
                bll.UpdateMember();

            }

            sb.Append(GetTran("001600", "成功") + "！！！");
            ScriptHelper.SetAlert(Page, sb.ToString());
        }
        else
        {
            sb.Append(GetTran("001618", "失败"));
            ScriptHelper.SetAlert(Page, sb.ToString());
        }

        txtTitle.Text = "";
        TextBox1.Text = "";
        content1.Value = "";

    }
    protected void btn_Cancle_Click(object sender, EventArgs e)
    {
        this.txtTitle.Text = "";
        this.TextBox1.Text = "";
    }
    /// <summary>
    /// 绑定国家
    /// </summary>
    private void bindcountry()
    {
        DataTable dt = StoreInfoEditBLL.bindCountry();
        this.DropDownList1.DataSource = dt;
        this.DropDownList1.DataTextField = "name";
        this.DropDownList1.DataValueField = "countrycode";
        this.DropDownList1.DataBind();
    }
    /// <summary>
    /// 绑定语言
    /// </summary>
    private void bindlange()
    {
        DataTable dt = BLL.CommonClass.CommonDataBLL.GetLanaguage();
        this.DropDownList2.DataSource = dt;
        this.DropDownList2.DataValueField = "languagecode";
        this.DropDownList2.DataTextField = "name";
        this.DropDownList2.DataBind();
    }
    protected void drop_LoginRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindDropLevel();
    }
    protected void ChkLevel_CheckedChanged(object sender, EventArgs e)
    {
        this.SetVisible();
    }
    protected void ChkBonus_CheckedChanged(object sender, EventArgs e)
    {
        this.SetVisible();
    }
    protected void ChkNet_CheckedChanged(object sender, EventArgs e)
    {
        this.SetVisible();
    }
    private void SetVisible()
    {   
        //this.DropLevel.Visible = this.ChkLevel.Checked;
        this.TxtBonusFrom.Visible = this.ChkBonus.Checked;
        this.TxtBonusTo.Visible = this.ChkBonus.Checked;
        this.TxtLeader.Visible = this.ChkNet.Checked;
        this.DropRelation.Visible = this.ChkNet.Checked;
        this.LblTo.Visible = this.ChkBonus.Checked;
    }
}
