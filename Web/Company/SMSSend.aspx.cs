using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient ;
using System.Text.RegularExpressions;
using Model.Other;
using System.Text.RegularExpressions;

public partial class Company_SMSSend : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.GroupSend);
        if (!IsPostBack)
        {
            QishuBind();
            JibieBind();
            ddlRecever_SelectedIndexChanged(null, null);
        }
        Translations();
    }
 
    private void Translations()
    {
        this.TranControls(this.btnAddMobile, new string[][] { new string[] { "000492", "添加号码" } });
        this.TranControls(this.btnRemoveMobile, new string[][] { new string[] { "000496", "移除号码" } });
        this.TranControls(this.btnSend, new string[][] { new string[] { "000497", "发送" } });
        this.TranControls(this.btnClear, new string[][] { new string[] { "006812", "重填" } });

        this.TranControls(this.ddlRecever, new string[][] { 
            new string[] { "006634", "指定手机" }, 
            new string[] { "006633", "会员" }, 
            //new string[] { "006632", "店长" } 
        });

        this.TranControls(this.ddlMemberType, new string[][] { 
            new string[] { "000633", "所有" }, 
            new string[] { "006635", "今天生日的会员" }, 
            new string[] { "006636", "奖金己发放" } 
        });
     
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        string mobile = string.Empty;
        int IsTrem = 0;
        string number = "";
        string msg = BLL.CommonClass.ValidData.InputText(this.txtMsg.Text.Trim());
        if (msg == "")
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("005813", "请填写发送内容"));
            return;
        }
        else if (msg.Length > 256)
        {
            BLL.CommonClass.Transforms.JSAlert(BLL.Translation.Translate("006807", "短信内容不能超过256个字符！"));
            return;
        }
      
        string bianhao = string.Empty;
        string info = string.Empty;
        string selectRValue = this.ddlRecever.SelectedItem.Value.Trim();
        string keyCodes=BLL.CommonClass .ValidData .InputText (this .txtkeyWords .Text );
        SqlTransaction tran = null;
        SqlConnection con = DAL.DBHelper.SqlCon();
        try
        {
            con.Open();
            tran = con.BeginTransaction();
            string MobileErr = string.Empty;
            string MobileSuc = string.Empty;
            if (selectRValue == "-1")
            {//向指定号码发送
                if (ListBoxMobiles.Items.Count < 1)
                {
                    BLL.CommonClass.Transforms.JSAlert(GetTran("005812", "请添加手机号码"));
                    return;
                }
                foreach (ListItem li in ListBoxMobiles.Items)
                {                    
                    mobile = li.Value.Trim();
                    bool flag = BLL.MobileSMS.SendMsgTo(tran, bianhao, bianhao, mobile, msg, out info, Model.SMSCategory.sms_ManualSent);
                    if (!flag)
                    {
                        if (MobileErr == "")
                            MobileErr += mobile;
                        else
                            MobileErr += "，" + mobile;
                    }
                    else
                    {
                        if (MobileSuc == "")
                            MobileSuc += mobile;
                        else
                            MobileSuc += "，" + mobile;
                    }
                }
                if (MobileErr == "")
                {
                    BLL.CommonClass.Transforms.JSAlert(GetTran("005615", "发送成功！"));
                    this.txtMsg.Text = string.Empty;
                }
                else
                {
                    BLL.CommonClass.Transforms.JSAlert("(" + MobileErr + ")" + GetTran("005616", "发送失败") + "：" + info);
                }
            }
            else
            {//向特定群体发送
                string condition = " 1=1 ";
                int jibie = int.Parse (this.ddlJibie.SelectedValue.Trim());
                int typeValue=int.Parse (this.ddlMemberType.SelectedValue.Trim ());
                int qishu = int.Parse(this.ddlQishu.SelectedItem.Value.Trim());
                Model.EnumStatusModel enumStatus = Model.EnumStatusModel.enum_StatusStore;
                if (selectRValue == "2")
                {
                    #region //向店长发送
                    enumStatus =Model .EnumStatusModel.enum_StatusStore ;
                    if (jibie != -1)
                    {
                        condition += " and  StoreLevelInt="+jibie+" " ;
                    }
                    #endregion 
                }
                else if (selectRValue == "3")
                {
                    #region //向会员发送
                    enumStatus =Model .EnumStatusModel.enum_StatusMember ;
                    condition += " and mi.MemberState=1 and mi.Number not in(select userid from BlackList)";
                    if (typeValue == 0)
                    {//今天生日的会员
                        DateTime dateNow = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                        //condition += " and Birthday between '" + dateNow + "' and '" + dateNow.AddDays(1) + "' ";
                        condition += " and datepart(month,birthday)=" + DateTime.UtcNow.Month + " and datepart(day,birthday)=" + DateTime.UtcNow.Day + "";
                    }
                    else if (typeValue == 1)
                    {
                        
                        if (jibie != -1)
                        {
                            condition += " and  LevelInt=" + jibie + " ";
                        }
                        double bonus=0;
                        
                        if (qishu != -1)
                        {                           
                            if (keyCodes == "")
                                keyCodes = "0";
                            try
                            {
                                if (keyCodes.Length > 11)
                                {
                                    BLL.CommonClass.Transforms.JSAlert(GetTran("006579", "奖金格式错误"));
                                    return;
                                }
                                bonus = double.Parse(keyCodes);
                            }
                            catch
                            {
                                BLL.CommonClass.Transforms.JSAlert(GetTran("006579", "奖金格式错误"));
                                return;
                            }
                            if (bonus > 0)
                            {
                                string match = this.ddlMatch.SelectedItem.Value.Trim();
                                Regex rx0 = new Regex(@"(&gt;)", RegexOptions.IgnoreCase);
                                Regex rx1 = new Regex(@"(&lt;)", RegexOptions.IgnoreCase);
                                match = rx0.Replace(match, ">");
                                match = rx1.Replace(match, "<");
                                condition += " and CurrentTotalMoney>0 and CurrentTotalMoney " + match + " " + keyCodes;
                            }
                        }
                    }
                    else if (typeValue == 2)
                    {
                        IsTrem = 2;
                        number = this.MangeTerm.Text.Trim();
                        if (string.IsNullOrEmpty(number))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + GetTran("000000", "请填写团队网络起点") + "')",true);
                            return;
                        }
                    }

                    else if (typeValue == 3)
                    {
                        IsTrem = 1;
                        number = this.MangeTerm.Text.Trim();
                        if (string.IsNullOrEmpty(number))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + GetTran("000000", "请填写团队网络起点") + "')", true);
                            return;
                        }
                    }
                    #endregion 
                }
                bool flag = BLL.other.Company.SMSBLL.SendMsg(tran, msg, qishu, condition, enumStatus, out info,IsTrem,number);
            }
            tran.Commit();
            BLL.CommonClass.Transforms.JSAlert(info);
            this.txtMsg.Text = string.Empty;

        }
        catch (Exception ex)
        {
            tran.Rollback();
            BLL.CommonClass.Transforms.JSAlert(GetTran("000004", "异常") + "：" + ex.Message);
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
    protected void btnRemoveMobile_Click(object sender, EventArgs e)
    {
        if (this.ListBoxMobiles.SelectedIndex != -1)
        {
            this.ListBoxMobiles.Items.RemoveAt(this.ListBoxMobiles.SelectedIndex);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.txtMobile.Text = string.Empty;
        this.txtMsg.Text = string.Empty;
        this.ListBoxMobiles.Items.Clear();
       // this.txtSubject 
    }
    protected void btnAddMobile_Click(object sender, EventArgs e)
    {
        string mobile = BLL.CommonClass.ValidData.InputText(this.txtMobile.Text.Trim());
        if (mobile == "")
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("005812", "请输入手机号码！"));
            return;
        }
        Regex rx = new Regex(@"(1)\d{10}", RegexOptions.IgnoreCase);
        if (!rx.IsMatch(mobile))
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("006545", "手机号码格式错误！"));
            return;
        }
        this.ListBoxMobiles.Items.Remove(new ListItem(mobile, mobile));
        this.ListBoxMobiles.Items.Add(new ListItem(mobile, mobile));
        this.txtMobile.Text = string.Empty;
    }

    protected void ddlRecever_SelectedIndexChanged(object sender, EventArgs e)
    {
        JibieBind();
        string selectValue = this.ddlRecever.SelectedItem.Value.Trim();
        this.divM0.Visible = false;
        this.divM1.Visible = false;
        this.divM2.Visible = false;
        this.divM3.Visible = false;
        this.divM4.Visible = false;
        this.divM5.Visible = false;
        this.divM6.Visible = false;
        this.divM0.Visible = false;
        this.divM1.Visible = false;
        this.divSmsSet.Visible = false;
        this.MangeTerm.Visible = false;
        if (selectValue == "-1")
        {//指定号码发送 
            this.divSmsSet.Visible = false;
            this.divMS0.Visible = false;
            this.divMS1.Visible = false;
            trAdd.Visible = true;
        }
        else
        {//指定接收群体
            this.divSmsSet.Visible = true;
            trAdd.Visible = false;
            this.divMS0.Visible = true;
            this.divMS1.Visible = true;
            if (selectValue == "3")
            {//会员
                this.divM0.Visible = true;
                this.divM1.Visible = true;
            }
            else if (selectValue == "2")
            {//店长
                
            }
        }

    }

    /// <summary>
    /// 期数绑定
    /// </summary>
    private void QishuBind()
    {
        bool IsSuance = true;
        BLL.CommonClass.CommonDataBLL.BindQishuList(this.ddlQishu, false, IsSuance);
        this.ddlQishu.SelectedIndex = this.ddlQishu.Items.Count - 1;
    }
    /// <summary>
    /// 期数绑定
    /// </summary>
    private void JibieBind()
    {
        string selectValue = this.ddlRecever.SelectedItem.Value.Trim();
        if (selectValue == "-1")
        {//指定号码发送 
        }
        else if (selectValue == "2")
        {//向店长发送
            BLL.CommonClass.CommonDataBLL.BindStoreJibie(this.ddlJibie,true );
            this.ddlJibie.SelectedIndex = this.ddlJibie.Items.Count - 1;
        }
        else if (selectValue == "3")
        {//向会员发送
            BLL.CommonClass.CommonDataBLL.BindMemberJibie(this.ddlJibie, true);
            this.ddlJibie.SelectedIndex = this.ddlJibie.Items.Count - 1;
        }        
       
    }

    protected void ddlMemberType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int typeValue =int.Parse ( this.ddlMemberType.SelectedValue.Trim());
        if (typeValue == 1)
        {//奖金发放
            this.divM2.Visible = true;
            this.divM3.Visible = true;
            this.divM4.Visible = true;
            this.divM5.Visible = true;
            this.divM6.Visible = true;
            this.MangeTerm.Visible = false;
        }
        else if (typeValue == 0)
        {//今天生日的会员
            this.divM2.Visible = false ;
            this.divM3.Visible = false;
            this.divM4.Visible = false;
            this.divM5.Visible = false;
            this.divM6.Visible = false;
            this.MangeTerm.Visible = false;
        }
        else if (typeValue == -1)
        {
 
        }
        else if (typeValue == 2)
        {
            this.divM2.Visible = false;
            this.divM3.Visible = false;
            this.divM4.Visible = false;
            this.divM5.Visible = false;
            this.divM6.Visible = false;
            this.divMS0.Visible = false;
            this.divMS1.Visible = false;
            this.MangeTerm.Visible = true;
        }

        else if (typeValue == 3)
        {
            this.divM2.Visible = false;
            this.divM3.Visible = false;
            this.divM4.Visible = false;
            this.divM5.Visible = false;
            this.divM6.Visible = false;
            this.divMS0.Visible = false;
            this.divMS1.Visible = false;
            this.MangeTerm.Visible = true;
        }
    }
    protected void lbtnSaveNew_Click(object sender, EventArgs e)
    {
        string msg = BLL.CommonClass.ValidData.InputText(this.txtMsg.Text.Trim());
        try
        {
            int categoryID = 0;           
            if (msg == "")
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("000005", "请输入预设短信内容"));
                return;
            }
            string info = string.Empty;
            bool flag = new BLL.other.Company.SMSBLL().AddNewPreSetSMS(msg, categoryID,out info);
            if (flag)
            {
                BLL.CommonClass.Transforms.JSAlert(info);
            
            }
            BLL.CommonClass.Transforms.JSAlert(info);          
        }
        catch (Exception ex)
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("000004", "异常") + "：" + ex.Message);
        }
    }
}
