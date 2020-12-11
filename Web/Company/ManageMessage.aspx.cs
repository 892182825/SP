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

public partial class Company_ManageMessage : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);

        int xz = 0;
        xz = (int)Permissions.GetPermissions(Model.Other.EnumCompanyPermission.ManageMessage);
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
            if (Request["Id"] != null)
            {

                int id = Convert.ToInt32(Request["Id"].ToString());
                PageBind(id);
            }

            this.SetTDVisible();



            if (Request.QueryString["type"] == "huifu")
            {
                string id = Request.QueryString["hfid"].ToString();

                string strv = DBHelper.ExecuteScalar("select SenderRole from dbo.MessageReceive where id='" + id + "'").ToString().Trim();

                drop_LoginRole.SelectedValue = strv;

                drop_LoginRole.Enabled = false;
                this.RadioBianhao.Visible = false;
                this.RadioRange.Visible = false;
                this.td_GongGao.Visible = false;
                this.td_ReceiveRange.Visible = false;
            }


        }

        Translations_More();
    }

    protected void Translations_More()
    {


        TranControls(drop_LoginRole, new string[][] 
                        {
                            //new string[] { "000151","管理员"}, 
                            //new string[] { "000388","店 铺"},
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
        TranControls(RadioBianhao, new string[][] { new string[] { "007871", "指定编号" } });
        TranControls(RadioRange, new string[][] { new string[] { "007872", "指定范围" } });
        //TranControls(ChkLevel, new string[][] { new string[] { "000046", "级别" } });
        TranControls(ChkBonus, new string[][] { new string[] { "007869", "奖金区间" } });
        TranControls(ChkNet, new string[][] { new string[] { "007870", "团队" } });

    }

    private void BindDropLevel()
    {
        string levelflag = "";
        switch (this.drop_LoginRole.SelectedValue)
        {
            case "0":
                levelflag = "-1";
                this.RadioBianhao.Checked = true;
                break;
            case "1":
                levelflag = "1";
                break;
            case "2":
                levelflag = "0";
                break;
        }
        this.SetTDVisible();

        //this.DropLevel.Items.Clear();
        //System.Data.SqlClient.SqlDataReader dr = DBHelper.ExecuteReader("select levelint,levelstr from BSCO_Level where levelflag=" + levelflag);
        //while (dr.Read())
        //{
        //    ListItem list = new ListItem(dr["levelstr"].ToString(), dr["levelint"].ToString());

        //    this.DropLevel.Items.Add(list);
        //}
        //dr.Close();
    }
    private void SetRadioButton()
    {
        if (this.drop_LoginRole.SelectedValue.Equals("0"))
        {
            this.RadioBianhao.Checked = true;
            this.RadioRange.Visible = false;
            //this.td_GongGao.Visible = true;
            //this.td_ReceiveRange.Visible = false;
        }
        else
        {
            this.RadioRange.Visible = true;
        }
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
    //private void SetLevel(bool b)
    //{
    //    if (!b)
    //    {
    //        this.DropLevel.Visible = b;
    //        this.ChkLevel.Checked = b;
    //    }
    //    this.ChkLevel.Visible = b;

    //}
    private void BindDropRelation()
    {
        this.DropRelation.Items.Add(new ListItem(GetTran("kwl", "安置团队"), "1"));
        this.DropRelation.Items.Add(new ListItem(GetTran("kwl", "推荐团队"), "2"));

    }
    private void PageBind(int id)
    {
        MessageSendModel message = MessageSendBLL.GetMessageSendById(id);
        this.txtBianhao.Text = message.Receive;
        this.TextBox1.Text = message.Content;
        this.txtTitle.Text = message.InfoTitle;
        this.drop_LoginRole.SelectedItem.Value = message.LoginRole;
        this.DropDownList1.Enabled = false;
        this.DropDownList2.Enabled = false;
        this.DropDownList1.SelectedValue = message.CountryCode;
        this.DropDownList2.SelectedValue = message.LanguageCode;

    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        #region 验证用户输入
        if (Request["type"] != "huifu" && txtBianhao.Text.Trim() == "" && this.RadioBianhao.Checked)
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
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("kwl", "请输入邮件内容") + "！！！');</script>");

            return;
        }
        if (this.content1.Value.Trim().Length > 4000)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000863", "您输入的信息过长") + "！！！');</script>");

            return;
        }
        double from = 0;
        double to = 0;
        if (this.RadioRange.Checked && this.ChkBonus.Checked)
        {

            try
            {
                from = Convert.ToDouble(this.TxtBonusFrom.Text.Trim());
                to = Convert.ToDouble(this.TxtBonusTo.Text.Trim());
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
        if (this.drop_LoginRole.SelectedValue.Equals("2") && this.RadioRange.Checked && this.ChkNet.Checked)
        {
            if (this.TxtLeader.Text.Trim().Length.Equals(0))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("kwl", "请指定团队首领编号") + "！！！');</script>");
                return;
            }
            else
            {
                if (!MessageSendBLL.CheckNumber(2, this.TxtLeader.Text.Trim()))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("kwl", "指定的团队首领不存在") + "！！！');</script>");
                    return;
                }
            }
        }
        #endregion

        StringBuilder sb = new StringBuilder();
        //if (Request["type"] != null)
        //{
        if (Request["type"] == "huifu")
        {
            MessageSendModel messagesend = new MessageSendModel();

            //邮件保存
            messagesend.Content = this.content1.Value.Trim();
            messagesend.DropFlag = 0;
            messagesend.InfoTitle = txtTitle.Text.Trim().Replace("<", "&lt;").Replace(">", "&gt;");
            messagesend.LoginRole = drop_LoginRole.SelectedItem.Value;
            messagesend.ReadFlag = 0;
            messagesend.Sender = Session["Company"].ToString();
            messagesend.SenderRole = "0";
            messagesend.LanguageCode = this.DropDownList2.SelectedValue;
            messagesend.CountryCode = this.DropDownList1.SelectedValue;
            messagesend.MessageType = 'm';
            messagesend.SetNoCondition();
            int hfid;
            try
            {
                hfid = Convert.ToInt32(Request["hfid"]);
                if (hfid < 0)
                {
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }
            messagesend.ReplyFor = hfid;
            System.Data.SqlClient.SqlDataReader dr = DAL.DBHelper.ExecuteReader("select Sender,ClassID from MessageReceive where ID=@id", new System.Data.SqlClient.SqlParameter("@id", hfid), CommandType.Text);
            if (dr.Read())
            {
                messagesend.Receive = dr["Sender"].ToString();
                messagesend.MessageClassID = Convert.ToInt32(dr["ClassID"]);

            }
            else
            {
                ScriptHelper.SetAlert(Page, GetTran("001589", "对不起发件人不存在") + "！！！");
                return;
            }
            if (MessageSendBLL.Addsendaffiche(messagesend))
            {

                string href = "ManageMessage_Recive.aspx" + (Request["ClassID"] == null ? "" : "?ClassID=" + Request["ClassID"]);
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001587", "邮件回复成功") + "！！！');location.href='" + href + "';</script>");


            }
            else
            {
                ScriptHelper.SetAlert(Page, GetTran("001588", "邮件回复失败") + "！！！");
            }
        }

        else//信息
        {
            if (Request["Id"] != null)
            {

                int id = Convert.ToInt32(Request["Id"]);
                //还没改  MessageSendBLL.DelMessageSendById(id);
                sb.Append(GetTran("001590", "邮件修改"));
            }
            else
            {
                Response.Cache.SetExpires(DateTime.Now);
                Permissions.CheckManagePermission(EnumCompanyPermission.ManageMessage);
                sb.Append(GetTran("001592", "邮件发送"));
            }
            MessageSendModel messagesend = new MessageSendModel();
            //邮件保存
            messagesend.Content = this.content1.Value.Trim();
            messagesend.DropFlag = 0;
            messagesend.InfoTitle = txtTitle.Text.Trim().Replace("<", "&lt;").Replace(">", "&gt;");
            messagesend.LoginRole = drop_LoginRole.SelectedItem.Value;
            messagesend.ReadFlag = 0;
            messagesend.Sender = Session["Company"].ToString();
            messagesend.SenderRole = "0";
            messagesend.LanguageCode = this.DropDownList2.SelectedValue;
            messagesend.CountryCode = this.DropDownList1.SelectedValue;

            messagesend.Qishu = CommonDataDAL.getMaxqishu();
            messagesend.MessageType = 'm';
            messagesend.SetNoCondition();
            if (this.RadioBianhao.Checked) //指定了编号
            {
                if (txtBianhao.Text.Trim().Length > 0)
                {
                    if(!txtBianhao.Text.Trim().Equals("*"))
                    {
                        //验证编号是否存在
                        string[] strArray = txtBianhao.Text.Trim().Split(new char[] { ',' });
                        foreach (string strNO in strArray)
                        {
                            if (strNO != "")
                            {
                                int id = Convert.ToInt32(drop_LoginRole.SelectedValue);
                                //验证编号是否存在
                                if (!MessageSendBLL.CheckNumber(id, strNO))
                                {
                                    Response.Write("<script>alert('" + strNO + GetTran("001584", "不存在") + "');</script>");
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    ScriptHelper.SetAlert(Page, GetTran("001594", "请输入收件人") + "！！！");
                    return;
                }
                messagesend.Receive = DisposeString.DisString(txtBianhao.Text.Trim());
            }
            else //指定了Range
            {
                messagesend.Receive = "*";
                //if (this.ChkLevel.Checked)
                //{
                //    messagesend.ConditionLevel = Convert.ToInt16(this.DropLevel.SelectedValue);
                //}
                if (this.ChkBonus.Checked)
                {
                    messagesend.ConditionBonusFrom = Convert.ToDouble(this.TxtBonusFrom.Text.Trim());
                    messagesend.ConditionBonusTo = Convert.ToDouble(this.TxtBonusTo.Text.Trim());
                }
                if (this.ChkNet.Checked)
                {
                    messagesend.ConditionRelation = Convert.ToChar(this.DropRelation.SelectedValue);
                    messagesend.ConditionLeader = this.TxtLeader.Text.Trim();
                }
            }


            if (MessageSendBLL.Addsendaffiche(messagesend))
            {
                sb.Append(GetTran("001600", "成功") + "！！！");
                ScriptHelper.SetAlert(Page, sb.ToString());
            }
            else
            {
                sb.Append(GetTran("001541", "失败") + "！！！");
                ScriptHelper.SetAlert(Page, sb.ToString());
            }
        }
        this.txtTitle.Text = "";
        this.TextBox1.Text = "";
        this.content1.Value = "";
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


    protected void RadioBianhao_CheckedChanged(object sender, EventArgs e)
    {
        this.SetTDVisible();
    }
    protected void RadioRange_CheckedChanged(object sender, EventArgs e)
    {
        this.SetTDVisible();
    }
    private void SetTDVisible()
    {
        this.td_GongGao.Visible = this.RadioBianhao.Checked;
        this.td_ReceiveRange.Visible = this.RadioRange.Checked;
        this.SetBonusAndNet(this.RadioRange.Checked && this.drop_LoginRole.SelectedValue.Equals("2"));
        //this.SetLevel(this.RadioRange.Checked && !this.drop_LoginRole.SelectedValue.Equals("0"));
    }

    protected void ChkLevel_CheckedChanged(object sender, EventArgs e)
    {
        this.SetVisible();
    }
    protected void ChkBonus_CheckedChanged(object sender, EventArgs e)
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
    protected void ChkNet_CheckedChanged(object sender, EventArgs e)
    {
        this.SetVisible();
    }


    protected void drop_LoginRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindDropLevel();
        this.SetRadioButton();
    }
}
