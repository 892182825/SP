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
using Model;
using BLL.MoneyFlows;
using System.Collections.Generic;
using Model.Other;
using BLL.CommonClass;


public partial class Member_BankRemit : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            //绑定银行
            BindBank();
            //绑定汇款用途
            BindDeclarationType();
            //绑定币种
            BindCurrency();
            setStoreInfo(); 
            search_rsj();
            if (this.LabCurrency1.Items.Count != 0)
            {
                this.LabCurrency.Text = RemittancesBLL.GetCurrencyNameByStoreID();
                this.LabCurrency2.Text = this.LabCurrency1.SelectedValue;
            }
            this.FKBirthday.Text = CommonDataBLL.GetDateEnd().ToString();
            this.HCBirthday.Text = CommonDataBLL.GetDateEnd().ToString();
            if (Session["MemberRemittancesModel"] != null && Request.QueryString["isclick"] != null)
            {
                set_value();
            }
        }
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
            this.DeclarationType.Items.Add(new ListItem(info.TypeName,info.ID.ToString()));
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
            this.LabCurrency1.Items.Add(new ListItem(str, info.ID.ToString()));
        }
    }
    //获取当前店铺的编号
    private void setStoreInfo()
    {
        this.Number.Text = Session["Member"].ToString();
    }
    //预填入数据,查询上一次的输入
    private void search_rsj()
    {
        //币种
        this.Currency.SelectedIndex = 0;
        //汇款用途
        this.DeclarationType.SelectedIndex = 0;
        //汇出银行
        this.RemitBank.Text = "";
        //汇出方账号
        this.RemitNum.Text = "";
        //汇款人
        this.Remitter.Text = "";
        //汇款人身份证
        this.IdentityCard.Text = "";
        //汇入银行及帐号
        this.BankName.SelectedIndex = 0;
    }
    //赋值
    private void set_get_value()
    {
        RemittancesModel info = new RemittancesModel();
        info.ReceivablesDate = DateTime.Parse(FKBirthday.Text);
        info.RemittancesDate = DateTime.Now;
        info.ImportBank = "";
        info.ImportNumber = "";
        info.RemittancesAccount = "";
        info.RemittancesBank = "";
        info.SenderID = "";
        info.Sender = "";
        info.RemitNumber = this.Number.Text;
        info.RemitMoney = decimal.Parse(this.Money.Text);
        info.StandardCurrency = int.Parse(this.LabCurrency.Text);
        info.Use = int.Parse(this.DeclarationType.SelectedValue);
        info.ConfirmType = 0;
        info.Remark = this.Remark.Text;
        info.RemittancesCurrency = int.Parse(this.LabCurrency2.Text);
        info.PayWay = 2;
        info.RemittancesMoney = decimal.Parse(this.Money.Text);
        string zw_hr_kfh = BankName.SelectedItem.Text.Substring(0, BankName.SelectedItem.Text.IndexOf("—", 0));
        string zw_hr_zh = BankName.SelectedItem.Text.Substring(BankName.SelectedItem.Text.IndexOf("—", 0) + 1, BankName.SelectedItem.Text.Length - BankName.SelectedItem.Text.IndexOf("—", 0) - 1);
        string zw_hcr = HCBirthday.Text;
        string zw_skr = FKBirthday.Text;
        info.RemittancesDate = DateTime.Parse(zw_hcr);
        info.ImportBank = zw_hr_kfh;
        info.ImportNumber = zw_hr_zh;
        info.BankID = int.Parse(this.BankName.SelectedValue);
        info.RemittancesAccount = this.RemitNum.Text;
        info.ReceivablesDate = DateTime.Parse(zw_skr);
        info.RemittancesBank = this.RemitBank.Text;
        info.SenderID = this.IdentityCard.Text;
        info.Sender = this.Remitter.Text;
        info.Remittancesid = this.PayeeNum.Text;
        Session["MemberRemittancesModel"] = info;
    }
    private void set_value()
    {
        RemittancesModel info = Session["MemberRemittancesModel"] as RemittancesModel;
        FKBirthday.Text = info.ReceivablesDate.ToString();
        this.Number.Text = info.RemitNumber.ToString();
        this.Money.Text = info.RemitMoney.ToString();
        this.LabCurrency.Text = info.StandardCurrency.ToString();
        this.DeclarationType.SelectedValue = info.Use.ToString();
        this.Remark.Text = info.Remark;
        this.LabCurrency2.Text = info.RemittancesCurrency.ToString();
        this.Money.Text = info.RemittancesMoney.ToString();
        this.LabCurrency1.SelectedValue = info.RemittancesCurrency.ToString();

        HCBirthday.Text = info.RemittancesDate.ToString();
        this.BankName.SelectedValue = info.BankID.ToString();
        this.RemitNum.Text = info.RemittancesAccount;
        FKBirthday.Text = info.ReceivablesDate.ToString();
        this.RemitBank.Text = info.RemittancesBank;
        this.IdentityCard.Text = info.SenderID;
        this.Remitter.Text = info.Sender;
        this.PayeeNum.Text = info.Remittancesid;
        Session["MemberRemittancesModel"] = null;
    }
    protected void sub_Click(object sender, EventArgs e)
    {
        //验证店铺是否选择
        if (this.Number.Text.Trim().Length == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("002289", "请输入店铺！") + "')</script>");
            return;
        }
        //验证金额是否输入正确
        if (this.Money.Text.Trim().Length>0)
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

            //验证是否有汇出方帐号
            if (this.RemitNum.Text.Length == 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001103", "请输入汇出方帐号！") + "')</script>");
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
        set_get_value();
        //提交到另一个页面做确认
        if (this.LabCurrency1.Items.Count == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('" + GetTran("001112", "没有币种！") + "')</script>");
            return;
        }

        Server.Transfer("BankRemit2.aspx?FKDate=" + this.FKBirthday.Text + "&HCDate=" + this.HCBirthday.Text);
    }
}
