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
using Model.Other;
using BLL.CommonClass;
using System.Collections.Generic;
using System.Text;
using BLL.Registration_declarations;
using DAL;
using Standard.Classes;
using BLL.Logistics;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class Company_AddTotalAccount : BLL.TranslationBase
{
    ProcessRequest process = new ProcessRequest();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //检查相应权限
        Response.Cache.SetExpires(DateTime.Now);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            ViewState["Type"] = Request.QueryString["Type"] == "" ? "" : Request.QueryString["Type"];
            Number.Text = Request.QueryString["Number"] == "" ? "" : Request.QueryString["Number"];
            lit_number.Text = ViewState["Type"].ToString() == "Member" ? GetTran("000024", "会员编号") : GetTran("000037", "服务机构编号");
            if (ViewState["Type"].ToString() == "Member")
            {
                Permissions.CheckManagePermission(EnumCompanyPermission.FinanceAddStoreTotalAccount);
                member_type.Visible = true;
                //member_type2.Visible = true;
                store_type.Visible = false;
            }
            else if (ViewState["Type"].ToString() == "Store")
            {
                Permissions.CheckManagePermission(EnumCompanyPermission.FinanceStoreTotalAccount);
                member_type.Visible = false;
                //member_type2.Visible = false;
                store_type.Visible = true;
            }
            else {
                //member_type2.Visible = false;
                member_type.Visible = false;
                store_type.Visible = false;
            }
            BindTime();
            //绑定银行
            BindBank();
            search_rsj();
            
            this.FKBirthday.Text = CommonDataBLL.GetDate().ToString();
            this.HCBirthday.Text = CommonDataBLL.GetDate().ToString();
            if (Session["RemittancesModel"] != null && Request.QueryString["isclick"]!=null)
            {
                set_value();
            }
        }
        
        Translations();
    }

    //绑定时间
    private void BindTime()
    {
        for (int i = 0; i < 24; i++)
        {
            this.ddlHour.Items.Add(i.ToString());
        }
        this.ddlHour.SelectedValue = DateTime.Now.Hour.ToString();
        for (int j = 0; j < 60; j++)
        {
            this.ddlMinute.Items.Add(j.ToString());
        }
        this.ddlMinute.SelectedValue = DateTime.Now.Minute.ToString();

        for (int i = 0; i < 24; i++)
        {
            this.DropDownList1.Items.Add(i.ToString());
        }
        this.DropDownList1.SelectedValue = DateTime.Now.Hour.ToString();
        for (int j = 0; j < 60; j++)
        {
            this.DropDownList2.Items.Add(j.ToString());
        }
        this.DropDownList2.SelectedValue = DateTime.Now.Minute.ToString();
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
    private void Translations()
    {
        this.TranControls(Btn_Detail, new string[][] { new string[] { "000440", "查看" } });
        this.TranControls(member_type, new string[][] { new string[] { "007252", "消费账户" } });
        //this.TranControls(member_type2, new string[][] { new string[] { "008117", "复消账户" } });
        this.TranControls(store_type, new string[][] { new string[] { "007253", "服务机构订货款" }, new string[] { "007383", "服务机构周转款 " } });
        this.TranControls(this.RadPayFashion,
        new string[][]{
                    new string []{"001558","现金支付"},
                    new string []{"001582","银行汇款"}
                });
        this.TranControls(this.Way,
new string[][]{
                    new string []{"000643","传真"},
                    new string []{"000644","核实"},
                    new string []{"000647","透支"}
                });
        
    }

    //赋值
    private void set_get_value()
    {
        string ReceivablesDate = Convert.ToDateTime(FKBirthday.Text).ToString("yyyy-MM-dd") + " " + this.ddlHour.SelectedValue + ":" + this.ddlMinute.SelectedValue + ":00";
        string zw_hcr = Convert.ToDateTime(HCBirthday.Text).ToString("yyyy-MM-dd") + " " + this.DropDownList1.SelectedValue + ":" + this.DropDownList2.SelectedValue + ":00";

        RemittancesModel info = new RemittancesModel();
        info.ReceivablesDate = DateTime.Parse(ReceivablesDate).ToUniversalTime();
        info.RemittancesDate = DateTime.Parse(zw_hcr).ToUniversalTime();
        info.ImportBank = "";
        info.ImportNumber = "";
        info.RemittancesAccount = "";
        info.RemittancesBank = "";
        info.SenderID = "";
        info.Sender = "";
        
        info.RemitNumber = this.Number.Text;
        info.RemitMoney = decimal.Parse(this.Money.Text);
        info.StandardCurrency = CommonDataBLL.GetStandard();
        info.ConfirmType = int.Parse(Way.SelectedValue);
        info.Remark = this.Remark.Text;
        info.RemittancesCurrency = CommonDataBLL.GetStandard();
        info.PayWay = (int)PayWayType.RecAdvance;
        info.RemittancesMoney = decimal.Parse(this.Money.Text);
        info.Managers = Session["Company"].ToString();
        info.PhotoPath = "";
        info.OperateIp = Request.UserHostAddress;
        info.OperateNum = Session["Company"].ToString();
        info.IsGSQR = true;
        info.PayexpectNum = BLL.CommonClass.CommonDataBLL.GetMaxqishu();
        //获取汇单号
        string huidan = "HK" + Model.Other.MYDateTime.ToYYMMDDHHmmssString();
        //判断汇单号是否存在:true存在,false不存在
        bool isExist = RemittancesBLL.isMemberExistsHuiDan(huidan);
        while (isExist)
        {
            huidan = "HK" + Model.Other.MYDateTime.ToYYMMDDHHmmssString();
            isExist = RemittancesBLL.isMemberExistsHuiDan(huidan);
        }
        info.Remittancesid = huidan;
        if (this.RadPayFashion.SelectedIndex == 1)
        {

            string zw_hr_kfh = BankName.SelectedItem.Text.Substring(0, BankName.SelectedItem.Text.IndexOf("—", 0));
            string zw_hr_zh = BankName.SelectedItem.Text.Substring(BankName.SelectedItem.Text.IndexOf("—", 0) + 1, BankName.SelectedItem.Text.Length - BankName.SelectedItem.Text.IndexOf("—", 0) - 1);
            string zw_skr = ReceivablesDate;
            info.RemittancesDate = DateTime.Parse(zw_hcr).ToUniversalTime();
            info.ImportBank = zw_hr_kfh;
            info.ImportNumber = zw_hr_zh;
            info.RemittancesAccount = this.RemitNum.Text;
            info.ReceivablesDate = DateTime.Parse(zw_skr).ToUniversalTime();
            info.RemittancesBank = this.RemitBank.Text;
            info.SenderID = this.IdentityCard.Text;
            info.Sender = this.Remitter.Text;
        }
        if (ViewState["Type"].ToString() == "Member") {
            info.RemitStatus = 1;
            info.Use = int.Parse(member_type.SelectedValue);
        }
        else if (ViewState["Type"].ToString() == "Store") {
            info.RemitStatus = 0;
            info.Use = int.Parse(store_type.SelectedValue);
        }
        int ret = 0;
        SqlTransaction tran = null;
        SqlConnection conn = DAL.DBHelper.SqlCon();
        conn.Open();
        tran = conn.BeginTransaction();
        //添加汇款记录，并更新对应的金额
        int id = 0;
        RemittancesBLL.AddMemberRemittancesTran(info, CommonDataBLL.GetStandard().ToString(), CommonDataBLL.GetStandard().ToString(), out id, tran);
        if (id > 0)
        {
            if (info.Use == 0)//现金账户对账单
            {
                ret = D_AccountBLL.AddAccountTran(info.RemitNumber, Double.Parse(info.RemitMoney.ToString()) * RemittancesBLL.GetCurrency(info.RemittancesCurrency), D_AccountSftype.MemberType, D_Sftype.BounsAccount, D_AccountKmtype.RechargeByManager, DirectionEnum.AccountsIncreased, " " + "~007214", tran);
            }
            else if (info.Use == 1)//消费账户对账单
            {
                ret = D_AccountBLL.AddAccountTran(info.RemitNumber, Double.Parse(info.RemitMoney.ToString()) * RemittancesBLL.GetCurrency(info.RemittancesCurrency), D_AccountSftype.MemberCoshType, D_Sftype.EleAccount, D_AccountKmtype.RechargeByManager, DirectionEnum.AccountsIncreased, " " + "~007214", tran);
            }
            else if (info.Use == 2)//复消账户对账单
            {
                ret = D_AccountBLL.AddAccountTran(info.RemitNumber, Double.Parse(info.RemitMoney.ToString()) * RemittancesBLL.GetCurrency(info.RemittancesCurrency), D_AccountSftype.MemberTypeFx, D_Sftype.CancellationAccount, D_AccountKmtype.RechargeByManager, DirectionEnum.AccountsIncreased, " " + "~007214", tran);
            }


            //店铺订货款
            else if (info.Use == 10)
            {
                ret = D_AccountBLL.AddStoreAccount(info.RemitNumber, Double.Parse(info.RemitMoney.ToString()) * RemittancesBLL.GetCurrency(info.RemittancesCurrency), D_AccountSftype.StoreDingHuokuan, S_Sftype.dianhuo, D_AccountKmtype.RechargeByManager, DirectionEnum.AccountsIncreased, " " + "~003079", tran);
            }//店铺周转款
            else if (info.Use == 11)
            {
                ret = D_AccountBLL.AddStoreAccount(info.RemitNumber, Double.Parse(info.RemitMoney.ToString()) * RemittancesBLL.GetCurrency(info.RemittancesCurrency), D_AccountSftype.StoreZhouZhuankuan, S_Sftype.zhouzhuan, D_AccountKmtype.RechargeByManager, DirectionEnum.AccountsIncreased, " " + "~003079", tran);
            }
            if (ret > 0)
            {

                //发送短信

                //string receiverName = "", storeidnumber = "", mobile = "";
                //if (ViewState["Type"].ToString() == "Store")
                //{
                //    StoreInfoModel storeinfo = StoreInfoDAL.GetStoreInfoByStoreId(this.Number.Text);
                //    receiverName = Encryption.Encryption.GetDecipherName(storeinfo.Name);
                //    storeidnumber = storeinfo.Number;
                //    mobile = Encryption.Encryption.GetDecipherTele(storeinfo.MobileTele);
                //}
                //else if (ViewState["Type"].ToString() == "Member")
                //{
                //    MemberInfoModel storeinfo = MemberInfoDAL.getMemberInfo(Number.Text.Trim());
                //    receiverName = Encryption.Encryption.GetDecipherName(storeinfo.Name);
                //    storeidnumber = storeinfo.Number;
                //    mobile = Encryption.Encryption.GetDecipherTele(storeinfo.MobileTele);
                //}
                //Session["RemittancesModel"] = null;

                //string hkid = id.ToString();
                //try
                //{
                //    BLL.MobileSMS.SendMsgMode(tran, receiverName, "收到" + info.RemittancesMoney + RemittancesBLL.GetCurrencyByID(info.RemittancesCurrency), storeidnumber, mobile, hkid, Model.SMSCategory.sms_Receivables);
                //    tran.Commit();
                //}
                //catch
                //{
                //    tran.Rollback();
                //}
                if (ViewState["Type"].ToString() == "Member")
                {
                    tran.Commit();
                    conn.Close();
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001126", "汇款成功！") + "');location.href='AuditingStoreAccount.aspx?type=Member';</script>");
                }
                else
                {
                    tran.Commit();
                    conn.Close();
                    Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001126", "汇款成功！") + "');location.href='AuditingStoreAccount.aspx';</script>");
                }
            }
            else { tran.Rollback(); conn.Close(); Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("008020", "汇款失败") + "！');</script>"); }
        }
        else { tran.Rollback(); conn.Close(); Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("008020", "汇款失败") + "！');</script>"); }
    }
    private void set_value()
    {
        RemittancesModel info = Session["RemittancesModel"] as RemittancesModel;
        FKBirthday.Text=info.ReceivablesDate.ToString("yyyy-MM-dd");
        this.Number.Text = info.RemitNumber.ToString();
        this.Money.Text =info.RemitMoney.ToString();
        
        this.Remark.Text =info.Remark ;
         this.Money.Text=info.RemittancesMoney.ToString();
        this.RadPayFashion.SelectedValue=info.PayWay.ToString();
         if (info.PayWay == 2)
         {
             RadPayFashion_SelectedIndexChanged(null, null);
             HCBirthday.Text = info.RemittancesDate.ToString("yyyy-MM-dd");
             this.BankName.SelectedItem.Text = info.ImportBank + "-" + info.ImportNumber;
             this.RemitNum.Text = info.RemittancesAccount;
             FKBirthday.Text = info.ReceivablesDate.ToString("yyyy-MM-dd");
             this.RemitBank.Text = info.RemittancesBank;
             this.IdentityCard.Text = info.SenderID;
             this.Remitter.Text = info.Sender;
             this.PayeeNum.Text = info.Remittancesid;
         }

         this.ddlHour.SelectedValue = Convert.ToDateTime(info.ReceivablesDate).Hour.ToString();
         this.ddlMinute.SelectedValue = Convert.ToDateTime(info.ReceivablesDate).Minute.ToString();

         this.DropDownList1.SelectedValue = Convert.ToDateTime(info.RemittancesDate).Hour.ToString();
         this.DropDownList2.SelectedValue = Convert.ToDateTime(info.RemittancesDate).Minute.ToString();
        Session["RemittancesModel"] = null;
    }
    //预填入数据,查询上一次的输入
    private void search_rsj()
    {
        //币种
            //this.Currency.SelectedIndex = 0;
            //汇款用途
            store_type.SelectedIndex = 0;
            member_type.SelectedIndex = 0;
            //汇款方式
            this.RadPayFashion.SelectedIndex = 0;
            //汇款方式
            this.RadPayFashion.SelectedIndex = 0;
            //汇出银行
            this.RemitBank.Text = "";
            //汇出方账号
            this.RemitNum.Text = "";
            //汇款人
            this.Remitter.Text = "";
            //汇款人身份证
            this.IdentityCard.Text = "";
            //汇入银行及帐号
            if (this.BankName.Items.Count != 0)
                this.BankName.SelectedIndex = 0;
            //确认途径
            this.Way.SelectedIndex = 0;
    }
    protected void sub_Click(object sender, EventArgs e)
    {
        //if (this.ViewState["storename"] != null)
        //{
        //    this.Number.Text = this.ViewState["storename"].ToString();
        //}
        //设置特定值防止重复提交
        hid_fangzhi.Value = "0";

        //验证店铺是否选择
        if (this.Number.Text.Trim().Length == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001547", "编号不能为空！") + "')</script>");
            return;
        }

        if (ViewState["Type"].ToString() == "Member" && CommonDataBLL.getCountNumber(Number.Text.Trim()) <= 0) {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000725", "会员编号不存在！") + "!')</script>");
            return;
        }

        if (ViewState["Type"].ToString() == "Store" && (int)DAL.DBHelper.ExecuteScalar("select count(1) from storeinfo where storeid='" + Number.Text.Trim() + "'") <= 0) {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001562", "服务机构编号不存在！") + "')</script>");
            return;
        }
        //验证金额是否输入正确
        if (this.Money.Text.Trim().Length > 0)
        {
            try
            {
                double m=Convert.ToDouble(this.Money.Text.Trim());
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
        if (this.Money.Text.IndexOf(",")!=-1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("001094", "金额输入不正确！") + "')</script>");
            return;
        }
        if (double.Parse(this.Money.Text) > 9999999)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("006912", "金额输入太大！") + "')</script>");
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
        //如果选择银行汇款,验证汇款信息
        if (this.RadPayFashion.SelectedIndex == 1)
        {
            //验证汇出日期
            if (this.HCBirthday.Text.Trim().Length > 0)
            {
                try
                {
                    DateTime time = Convert.ToDateTime(this.HCBirthday.Text.Trim());
                }
                catch
                {
                    this.Page.RegisterStartupScript("", "<script language='javascript'>alert('" + GetTran("001099", "汇出日期格式输入不正确！") + "')</script>");
                    return;
                }
            }
            else
            {
                this.Page.RegisterStartupScript("", "<script language='javascript'>alert('" + GetTran("001100", "请输入汇出日期！") + "')</script>");
                return;
            }
            //验证是否有汇入银行及帐号
            if (this.BankName.Items.Count == 0)
            {
                this.Page.RegisterStartupScript("", "<script language='javascript'>alert('" + GetTran("001101", "没有汇入银行及帐号！") + "')</script>");
                return;
            }
            //验证是否有汇出方帐号
            if (this.RemitNum.Text.Length == 0)
            {
                this.Page.RegisterStartupScript("", "<script language='javascript'>alert('" + GetTran("001103", "请输入汇出方帐号！") + "')</script>");
                return;
            }
            if (this.Remitter.Text.Trim() == "") {
                this.Page.RegisterStartupScript("", "<script language='javascript'>alert('" + GetTran("008019", "请输入汇款人姓名") + "！')</script>");
                return;
            }
            if (this.PayeeNum.Text.Trim().Length == 0)
            {
                this.Page.RegisterStartupScript("", "<script language='javascript'>alert('" + GetTran("001105", "请输入汇单号！") + "')</script>");
                return;
            }
            if (this.IdentityCard.Text.Trim().Length == 0)
            {
                this.Page.RegisterStartupScript("", "<script language='javascript'>alert('" + GetTran("001107", "请输入身份证！") + "')</script>");
                return;
            }
            Regex reg = new Regex("[^\u4e00-\u9fa5]");
            Match match = reg.Match(BankName.Text.Trim());
            Match match1 = reg.Match(RemitNum.Text.Trim());
            Match match2 = reg.Match(PayeeNum.Text.Trim());
            Match match3 = reg.Match(IdentityCard.Text.Trim());
            if (!match.Success || !match1.Success || !match2.Success || !match3.Success)
            {
                this.Page.RegisterStartupScript("", "<script language='javascript'>alert('" + GetTran("008018", "请保证输入正确") + "！')</script>");
                RadPayFashion.SelectedValue = "2";
                return;
            }
        }
        set_get_value();
    }
    protected void RadPayFashion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["storename"] != null)
        {
            this.Number.Text = this.ViewState["storename"].ToString();
        }
        if (this.RadPayFashion.SelectedIndex == 0)
        {
            this.place1.Style.Add("display", "none");
        }
        else
        {
            this.place1.Style.Add("display", "block");
        }
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dt = RemittancesBLL.Quertystore(ViewState["table"].ToString(), ViewState["condition"].ToString());
        if (dt == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000053", "没有数据，不能导出Excel！") + "')</script>");
            return;
        }
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000053", "没有数据，不能导出Excel！") + "')</script>");
            return;
        }
        


        foreach (DataRow row in dt.Rows)
        {
            row[2] = Encryption.Encryption.GetDecipherName(row[2].ToString());//解密姓名
            row[3] = Encryption.Encryption.GetDecipherName(row[3].ToString());//解密店铺姓名

            CityModel info = CommonDataDAL.GetCPCCode(row[5].ToString());
            row[4] = info.Country + info.Province + info.City + Encryption.Encryption.GetDecipherAddress(row[4].ToString());//解密店铺地址
        }
        Excel.OutToExcel(dt, GetTran("000388", "店铺"), new string[] { "id=ID", "StoreID=" + GetTran("000150", "店铺编号"), "Name=" + GetTran("000107", "姓名"), "StoreName=" + GetTran("000040", "店铺名称"), "StoreAddress=" + GetTran("001038", "店铺地址") });

    }
    protected void Btn_Detail_Click(object sender, EventArgs e)
    {
        
        if (Number.Text.Trim().Length <= 0)
        {
            ScriptHelper.SetAlert(Page, GetTran("000129", "对不起，会员编号不能为空！"));
            return;
        }
        if (ViewState["Type"].ToString() == "Member")
        {
            if (CommonDataBLL.getCountNumber(Number.Text.Trim()) <= 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000000", "会员编号不存在！") + "')</script>");
                return;
            }
            else
            {
                MemberInfoModel model = BLL.other.Company.MemInfoEditBLL.getMemberInfo(Number.Text.Trim());
                lit_petname.Text = model.PetName;
                lit_add.Text = model.City.Country + model.City.Province + model.City.City + Encryption.Encryption.GetDecipherAddress(model.Address);
                //Response.Redirect("DisplayMemberDeatail.aspx?ID=" + Number.Text.Trim() + "&type=AddTotalAccount.aspx&type1=Member");
                //Page.RegisterStartupScript(null, "<script language='javascript'>window.close();</script>");
            }
        }
        else if (ViewState["Type"].ToString() == "Store") {
            if ((int)DAL.DBHelper.ExecuteScalar("select count(0) from storeinfo where storeid='" + Number.Text.Trim() + "'") <= 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000000", "服务机构不存在！") + "')</script>");
                return;
            }
            else
            {
                StoreInfoModel model = BLL.other.Company.StoreInfoEditBLL.GetStoreInfoByStoreId(Number.Text.Trim());
                lit_petname.Text = model.StoreName;
                lit_add.Text = model.StoreCity.Country + model.StoreCity.Province + model.StoreCity.City + Encryption.Encryption.GetDecipherAddress(model.StoreAddress);
            }
        }
    }
}
