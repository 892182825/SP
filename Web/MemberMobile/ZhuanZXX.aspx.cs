using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Model;
using BLL.CommonClass;

public partial class MemberMobile_ZhuanZXX : BLL.TranslationBase
{
    int bzCurrency = 0;
   public string jine = "";
    public string huikuan="";
    public string hkje = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        bzCurrency = CommonDataBLL.GetStandard();
        if (!IsPostBack)
        {
            if (Session["Default_Currency"] == null) Session["Default_Currency"] = bzCurrency;
        }
        string bh = Session["Member"].ToString();
        var id = Request.QueryString["id"];
        string dt_one = "select * from ECTransferDetail where id=" + id;
        //this.ucPagerMb1.PageInit(dt_one, "rep_km");
        DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
        string mm = dt.Rows[0]["InNumber"].ToString();

        if (mm == bh)
        {
            jine = " + " + dt.Rows[0]["OutMoney"].ToString();
            huikuan = "汇款人编号";
            hkje = dt.Rows[0]["OutNumber"].ToString();
        }
        else
        {
            jine = " - " + dt.Rows[0]["OutMoney"].ToString();
            huikuan="收款人编号";
            hkje = dt.Rows[0]["InNumber"].ToString();
        }
        rep_km.DataSource = dt;
        rep_km.DataBind();
    }



    protected string GetWithdrawMoney(string WithdrawMoney)
    {
        string money = "";
        if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
        {
            money = "0.00";
        }
        else
        {
            money = (double.Parse(WithdrawMoney) * AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
        }
        return money;
    }


    protected string GetInAccountType(int type)
    {
        switch (type)
        {
            case (int)InAccountType.MemberCash:
                return GetTran("000000", "FTC可用账户");
            case (int)InAccountType.MemberCons:
                return GetTran("000000", "消费账户");
            case (int)InAccountType.StoreOrder:
                return GetTran("007253", "服务机构订货款");
        }
        return "";
    }

    protected string GetOutAccountType(int Type)
    {
        switch (Type)
        {
            case (int)OutAccountType.MemberCash:
                return GetTran("000000", "FTC可用账户");
            case (int)OutAccountType.MemberCons:
                return GetTran("007260", "会员消费账户");
            case (int)OutAccountType.MemberTypeFx:
                return GetTran("000000", "注册积分账户");
        }
        return "";
    }
}