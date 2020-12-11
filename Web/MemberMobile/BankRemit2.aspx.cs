using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL.MoneyFlows;
using Model;
using BLL.CommonClass;
using System.Data;

public partial class Member_BankRemit2 : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            this.Currency.Visible = false;
            ViewState.Add("Currency", RemittancesBLL.GetCurrencyNameByStoreID());

            this.LabCurrency.Text = ViewState["Currency"].ToString();

            BindBank();
            BindDeclarationType();

            BindCurrency();
            set_get_value();
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.back2, new string[][] { new string[] { "001128", "返回修改" } });
        this.TranControls(this.Button5, new string[][] { new string[] { "001124", "保 存" } });
    }
    //绑定银行
    private void BindBank()
    {
        DataTable dt = RemittancesBLL.GetBank();
        BankName.DataSource = dt;
        BankName.DataTextField = "BankName";
        BankName.DataValueField = "ID";
        BankName.DataBind();
    }
    //绑定汇款用途
    private void BindDeclarationType()
    {
        IList<RemittanceUseType> rus = new List<RemittanceUseType>();
        rus.Add(new RemittanceUseType(1, GetTran("000638", "报单")));
        rus.Add(new RemittanceUseType(2, GetTran("000391", "周转货")));
        rus.Add(new RemittanceUseType(3, GetTran("000641", "店铺投资")));
        rus.Add(new RemittanceUseType(4, GetTran("000642", "其他")));
        foreach (RemittanceUseType info in rus)
        {
            this.DeclarationType.Items.Add(new ListItem(info.TypeName, info.ID.ToString()));
        }
        //删除店铺投资
        foreach (ListItem item in this.DeclarationType.Items)
        {
            if (item.Value == "3")
            {
                this.DeclarationType.Items.Remove(item);
                break;
            }
        }
    }
    //绑定币种
    private void BindCurrency()
    {
        IList<CurrencyModel> list = RemittancesBLL.GetCurrency();
        foreach (CurrencyModel info in list)
        {
            string str = CommonDataBLL.GetLanguageStr(info.ID, "Currency", "Name");
            this.Currency.Items.Add(new ListItem(str, info.ID.ToString()));
        }
    }
    //赋值
    private void set_get_value()
    {
        this.Number.Text = Session["Member"].ToString();
        this.Money.Text = Request["hkMoney"].ToString();
        this.FKBirthday.Text = Request.QueryString["FKDate"];
        this.HCBirthday.Text = Request.QueryString["HCDate"];
        this.LabCurrency2.Text = Request["hkLabCurrency2"].ToString();
        foreach (ListItem item in this.Currency.Items)
        {
            if (item.Value == Request["hkLabCurrency2"].ToString())
            {
                this.LabCurrency22.Text = item.Text;
                break;
            }
        }
        this.DeclarationType.SelectedIndex = -1;
        this.DeclarationType.SelectedValue = Request["hkDeclarationType"].ToString();

        this.PayeeNum.Text = Request["hkPayeeNum"].ToString();
        this.Remitter.Text = Request["hkRemitter"].ToString();
        this.IdentityCard.Text = Request["hkIdentityCard"].ToString();
        this.RemitBank.Text = Request["hkRemitBank"].ToString();
        this.RemitNum.Text = Request["hkRemitNum"].ToString();
        this.BankName.SelectedIndex = -1;
        if (this.BankName.Items.Count > 0)
        {
            this.BankName.SelectedValue = Request["hkBankName"].ToString();
        }
        this.Remark.Text = Request["hkRemark"].ToString();
    }

    bool istrue = true;
    protected void Button5_Click(object sender, EventArgs e)
    {
        //验证店铺是否选择
        if (this.Number.Text.Trim().Length == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("002289", "请输入店铺！") + "')</script>");
            return;
        }

        //验证金额是否输入正确
        if (this.Money.Text.Trim().Length > 0)
        {
            double d = 0;
            bool b = double.TryParse(this.Money.Text.Trim(), out d);
            if (!b)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001094", "金额输入不正确！") + "')</script>");
                return;
            }
            if (d <= 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001313", "申报的金额必须大于0！") + "')</script>");
                return;
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001096", "请输入金额！") + "')</script>");
            return;
        }

        //验证付款日期是否输入
        if (this.FKBirthday.Text.Trim().Length > 0)
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            bool b = DateTime.TryParse(this.FKBirthday.Text.Trim(), out time);
            if (!b)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001097", "付款日期格式输入不正确！") + "')</script>");
                return;
            }
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001098", "请输入日期！") + "')</script>");
            return;
        }
        //验证汇出日期
        if (this.HCBirthday.Text.Trim().Length > 0)
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            bool b = DateTime.TryParse(this.HCBirthday.Text.Trim(), out time);
            if (!b)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001099", "汇出日期格式输入不正确！") + "')</script>");
                return;
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001100", "请输入汇出日期！") + "')</script>");
            return;
        }

        //验证是否有汇入银行及帐号
        if (this.BankName.Items.Count == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001101", "没有汇入银行及帐号！") + "')</script>");
            return;
        }

        if (this.PayeeNum.Text.Trim().Length == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001105", "请输入汇单号！") + "')</script>");
            return;
        }

        if (this.IdentityCard.Text.Trim().Length == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("005900", " 请输入汇款人证件号！") + "')</script>");
            return;
        }
        if (System.Text.RegularExpressions.Regex.IsMatch(this.PayeeNum.Text, @"^[0-9]+$") == false)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001108", "汇单号码必须是数字！") + "')</script>");
            return;
        }
        if (System.Text.RegularExpressions.Regex.IsMatch(this.IdentityCard.Text, @"^[0-9]+$") == false)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001109", "汇款人证件号必须是数字！") + "')</script>");
            return;
        }
        if (System.Text.RegularExpressions.Regex.IsMatch(this.RemitNum.Text, @"^[0-9]+$") == false)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001110", "汇出方帐号必须是数字！") + "')</script>");
            return;
        }
        if (Convert.ToDouble(this.Money.Text.Trim()) > 9999999)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("006912", "输入金额太大！") + "')</script>");
            return;
        }
        if (this.Money.Text.IndexOf(",") != -1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001094", "金额输入不正确！") + "')</script>");
            return;
        }
        if (this.Remark.Text.Length > 500)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("006708", "对不起，备注输入的字符太多,最多500个字符！") + "');", true);
            return;
        };
        RemittancesModel info = new RemittancesModel();
        info.ReceivablesDate = DateTime.Now;//收款时间
        info.RemitNumber = this.Number.Text;
        info.RemitMoney = decimal.Parse(this.Money.Text);
        info.StandardCurrency = int.Parse(this.LabCurrency.Text);//标准币种
        info.Use = int.Parse(this.DeclarationType.SelectedValue);//用途：1.报单，2.
        info.PayexpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();//付款期数
        info.PayWay = (int)PaymentEnum.BankTransfer;//付款方式
        info.Managers = Session["Member"].ToString();//经办人
        info.ConfirmType = 0;//确认方式（已经去掉）
        info.Remark = this.Remark.Text;//备注
        info.RemittancesCurrency = int.Parse(this.LabCurrency2.Text);//汇出币种
        info.RemittancesMoney = decimal.Parse(this.Money.Text);//汇出金额
        info.OperateIp = CommonDataBLL.OperateIP;//操作者IP
        info.OperateNum = Session["Member"].ToString();//操作者编号
        info.Remittancesid = "";//汇款单号
        string zw_hr_kfh = BankName.SelectedItem.Text.Substring(0, BankName.SelectedItem.Text.IndexOf("—", 0));//银行
        string zw_hr_zh = BankName.SelectedItem.Text.Substring(BankName.SelectedItem.Text.IndexOf("—", 0) + 1, BankName.SelectedItem.Text.Length - BankName.SelectedItem.Text.IndexOf("—", 0) - 1);//开户名
        zw_hr_zh = zw_hr_zh.Substring(zw_hr_zh.IndexOf("——") + 2);//账号
        info.ImportBank = zw_hr_kfh;//汇入银行
        info.ImportNumber = Encryption.Encryption.GetEncryptionCard(zw_hr_zh);//加密汇入账号
        info.RemittancesAccount = Encryption.Encryption.GetEncryptionCard(this.RemitNum.Text);//加密汇款账号
        info.RemittancesDate = DateTime.Parse(FKBirthday.Text);//汇出时间
        info.RemittancesBank = this.RemitBank.Text;//汇出银行
        info.SenderID = Encryption.Encryption.GetEncryptionNumber(this.IdentityCard.Text);//加密汇款人身份证
        info.Sender = Encryption.Encryption.GetEncryptionName(this.Remitter.Text);//加密汇款人姓名
        info.Remittancesid = this.PayeeNum.Text;//汇款单号
        info.RemitStatus = 1;//类型（0店铺，1会员）
        info.IsGSQR = false;//是否确认审核
        RemittancesBLL.RemitDeclare(info, this.LabCurrency.Text, this.LabCurrency2.Text);//汇款申报
        Session["MemberRemittancesModel"] = null;
        Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("002290", "录入成功！请等待公司审核！") + "');window.location.href='OnlinePayment.aspx';</script>");
    }

    protected void back2_Click(object sender, EventArgs e)
    {
        Response.Redirect("OnlinePayment.aspx?isclick=1&type=1");
    }
}
