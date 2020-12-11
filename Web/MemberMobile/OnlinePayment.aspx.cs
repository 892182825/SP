using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Model;
using BLL.MoneyFlows;
using Model.Other;
using BLL.CommonClass;
using System.Data;
using DAL;

public partial class Member_OnlinePayment : BLL.TranslationBase
{
    int bzCurrency = 0;
    public double huilv;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        huilv = (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())));
        if (!IsPostBack)
        {
            if (Session["Default_Currency"] == null) Session["Default_Currency"] = bzCurrency;

            setStoreInfo();
            search_rsj();
            Translations();
        }
    }

    private void Translations()
    {
        //this.TranControls(this.DeclarationType, new string[][] { new string[] { "006068", "现金账户" }, new string[] { "007252", "消费账户" } });
        this.TranControls(this.sub, new string[][] { new string[] { "000321", "提交" } });
        //this.TranControls(this.Button1, new string[][] { new string[] { "000434", "确定" } });
        ////this.TranControls(this.Button2, new string[][] { new string[] { "001614", "取消" } });
        //this.TranControls(this.Button3, new string[][] { new string[] { "000628", "说明" } });
    }

    //获取当前店铺的编号
    private void setStoreInfo()
    {
        this.Number.Text = Session["Member"].ToString();
    }
    //预填入数据,查询上一次的输入
    private void search_rsj()
    {
        //汇款用途
        //this.DeclarationType.SelectedIndex = 0;
    }
    protected void sub_Click(object sender, EventArgs e)
    {
        //设置特定值防止重复提交
        hid_fangzhi.Value = "0";

        string hkxz = " select value from JLparameter  where jlcid=6";
        DataTable dthkxz = DAL.DBHelper.ExecuteDataTable(hkxz);
        string value = dthkxz.Rows[0]["value"].ToString();


        //验证店铺是否选择
        if (this.Number.Text.Trim().Length == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("002289", "请输入店铺！") + "')</script>");
            return;
        }
        //验证金额是否输入正确
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
        if (d > 9999999)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("006912", "输入金额太大！") + "')</script>");
            return;
        }
        if (Convert.ToDecimal(d) % Convert.ToDecimal(value) != 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("009052", "汇款金额只能为") + value + GetTran("009053", "的倍数") + "！')</script>");
            Money.Text = "";
            return;

        }


        string zw_dian;
        zw_dian = Session["Member"].ToString();

        string Bank = "";
        string BankName = "";
        string BankBook = "";
        string aa = " select top(1)* from companybank order by ID desc";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(aa);
        if (dt != null && dt.Rows.Count > 0)
        {
            Bank = dt.Rows[0]["Bank"].ToString();
            BankName = dt.Rows[0]["BankName"].ToString();
            BankBook = dt.Rows[0]["BankBook"].ToString();
        }



        RemittancesModel info = new RemittancesModel();
        info.ReceivablesDate = DateTime.UtcNow;
        info.RemittancesDate = DateTime.UtcNow;
        info.IsJL = 1;
        info.ImportBank = Bank;
        info.ImportNumber = BankBook;
        info.name = BankName;
        info.RemittancesAccount = "";
        info.RemittancesBank = "";
        info.SenderID = "";
        info.Sender = "";
        info.RemitNumber = this.Number.Text;

        info.RemitMoney = Convert.ToDecimal(Convert.ToDouble(this.Money.Text) / huilv);
        info.StandardCurrency = bzCurrency;
        info.Use = 0; /*int.Parse(this.DeclarationType.SelectedValue)*/
        info.PayexpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
        info.Managers = zw_dian;
        info.ConfirmType = 0;
        info.Remark = "";
        info.RemittancesCurrency = int.Parse(Session["Default_Currency"].ToString());
        info.RemittancesMoney = Convert.ToDecimal(Convert.ToDouble(this.Money.Text) / huilv);
        info.OperateIp = CommonDataBLL.OperateIP;
        info.OperateNum = Session["Member"].ToString();

        //获取汇单号
        string huidan = "HK" + Model.Other.MYDateTime.ToYYMMDDHHmmssString();
        //判断汇单号是否存在:true存在,false不存在
        bool isExist = RemittancesBLL.isMemberExistsHuiDan(huidan);
        while (isExist)
        {
            huidan = "HK" + Model.Other.MYDateTime.ToYYMMDDHHmmssString();
            isExist = RemittancesBLL.isMemberExistsHuiDan(huidan);
        }
        info.RemitStatus = 1;
        info.IsGSQR = false;
        info.Remittancesid = huidan;

        RemittancesBLL.RemitDeclare(info, bzCurrency.ToString(), Session["Default_Currency"].ToString());

        DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select ID from remittances where RemittancesID='" + huidan + "'");
        string HkID = dt_one.Rows[0]["ID"].ToString();//汇款ID
        int bishu = 4;

        string billid = EncryKey.GetEncryptstr(huidan, 2, 1);
        string url = "OnlinePayQD.aspx?HkID=" + HkID + "&bishu=" + bishu + "&RemitMoney=" + info.RemitMoney;
        Response.Redirect(url);
        //Page.ClientScript.RegisterStartupScript(GetType(), null, @"<script type='text/javascript'>var formobj=document.createElement('form');formobj.action='"+url+"';formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj);formobj.submit(); </script>");
        this.Money.Text = "";
    }
}