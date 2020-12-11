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
using BLL.other.Company;

public partial class Member_OnlinePayment2 : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            //绑定汇款方式
            BindRadPay();
            this.Currency.Visible = false;
            ViewState.Add("Currency", RemittancesBLL.GetCurrencyNameByStoreID());

            this.LabCurrency.Text = ViewState["Currency"].ToString();

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
    //绑定付款方式
    private void BindRadPay()
    {
        CommonDataBLL.GetPaymentType(this.RadPayFashion, 7);

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
        this.Money.Text = Request["Money"].ToString();
        this.FKBirthday.Text = Request.QueryString["FKDate"];
        this.LabCurrency2.Text = Request["LabCurrency2"].ToString();
        foreach (ListItem item in this.Currency.Items)
        {
            if (item.Value == Request["LabCurrency2"].ToString())
            {
                this.LabCurrency22.Text = item.Text;
                break;
            }
        }

        this.DeclarationType.SelectedIndex = -1;
        this.DeclarationType.SelectedValue = Request["DeclarationType"].ToString();

        RadPayFashion.SelectedValue = Request["RadPayFashion"].ToString();
        this.Remark.Text = Request["Remark"].ToString();
        this.RadPayFashion.SelectedValue = Request["RadPayFashion"].ToString();
    }

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
            try
            {
                if (Convert.ToDouble(this.Money.Text.Trim()) <= 0)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001313", "申报的金额必须大于0！") + "')</script>");
                    return;
                }
            }

            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001094", "金额输入不正确！") + "')</script>");
                return;
            }
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001096", "请输入金额！") + "')</script>");
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
        //验证付款日期是否输入
        if (this.FKBirthday.Text.Trim().Length > 0)
        {
            try
            {
                DateTime time = Convert.ToDateTime(this.FKBirthday.Text.Trim());
            }
            catch
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
        if (this.Remark.Text.Length > 500)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("006708", "对不起，备注输入的字符太多,最多500个字符！") + "');", true);
            return;
        };
        this.Button5.Enabled = false;
        //读取当前期数和当前登录的管理员编号
        string zw_dian;
        zw_dian = Session["Member"].ToString();
        string use = this.DeclarationType.SelectedValue;

        RemittancesModel info = new RemittancesModel();
        info.ReceivablesDate = DateTime.Now;
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
        info.PayexpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
        info.PayWay = int.Parse(this.RadPayFashion.SelectedValue);
        info.Managers = zw_dian;
        info.ConfirmType = 0;
        info.Remark = this.Remark.Text;
        info.IsGSQR = false;
        info.RemittancesCurrency = int.Parse(this.LabCurrency2.Text);
        info.RemittancesMoney = decimal.Parse(this.Money.Text);
        info.OperateIp = CommonDataBLL.OperateIP;
        info.OperateNum = Session["Member"].ToString();
        info.Remittancesid = "";
        //支付宝支付
        string huidan = "";
        string payType = this.RadPayFashion.SelectedValue.Trim();
        if (payType == "3")
        {
            string isStore = "M";
            //获取汇单号
            huidan = Model.Other.MYDateTime.ToYYMMDDHHmmssString();
            //判断汇单号是否存在:true存在,false不存在
            bool isExist = RemittancesBLL.isMemberExistsHuiDan(huidan);
            while (isExist)
            {
                huidan = Model.Other.MYDateTime.ToYYMMDDHHmmssString();
                isExist = RemittancesBLL.isMemberExistsHuiDan(huidan);
            }
            huidan = isStore + huidan;
            info.RemitStatus = 1;
            info.IsGSQR = false;
            info.Remittancesid = huidan;
            RemittancesBLL.RemitDeclare(info, this.LabCurrency.Text, this.LabCurrency2.Text);
            decimal huilv = info.RemitMoney / (decimal.Parse(RemittancesBLL.GetCurrency(info.StandardCurrency).ToString())) * decimal.Parse(RemittancesBLL.GetCurrency(info.RemittancesCurrency).ToString());
            string url = "../Store/payment/default.aspx?zongMoney=(" + info.RemitMoney + huilv + ")&TotalMoney=" + info.RemitMoney + "&TotalComm=" + huilv + "&HuiDanID=" + huidan;
            Response.Write("<script language='javascript'>alert('" + GetTran("001127", "录入成功！请到网上银行付款页面。") + "');window.open('" + url + "')</script>");
        }
        else if (payType == "4")
        {
            //获取汇单号
            huidan = Model.Other.MYDateTime.ToYYMMDDHHmmssString();
            //判断汇单号是否存在:true存在,false不存在
            bool isExist = RemittancesBLL.isMemberExistsHuiDan(huidan);
            while (isExist)
            {
                huidan = Model.Other.MYDateTime.ToYYMMDDHHmmssString();
                isExist = RemittancesBLL.isMemberExistsHuiDan(huidan);
            }
            info.RemitStatus = 1;
            info.IsGSQR = false;
            info.Remittancesid = huidan;
            RemittancesBLL.RemitDeclare(info, this.LabCurrency.Text, this.LabCurrency2.Text);
            decimal huilv = info.RemitMoney / (decimal.Parse(RemittancesBLL.GetCurrency(info.StandardCurrency).ToString())) * decimal.Parse(RemittancesBLL.GetCurrency(info.RemittancesCurrency).ToString());
            string url = "../Store/quickPay/quickPay.aspx?RemittanceType=" + (int)Model.Enum_RemittancesType.enum_MemberRemittance + "&hkid=" + huidan;
            Response.Write("<script language='javascript'>alert('" + GetTran("001127", "录入成功！请到网上银行付款页面。") + "');window.open('" + url + "');</script>");
        }

    }

    protected void back2_Click(object sender, EventArgs e)
    {
        Response.Redirect("OnlinePayment.aspx?isclick=1&type=0");
    }
}
