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
using BLL.Logistics;
using System.Data.SqlClient;
using BLL.Registration_declarations;

public partial class MemberMobile_TixianXx : BLL.TranslationBase
{
   public int bzCurrency = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        bzCurrency = CommonDataBLL.GetStandard();
        var id = Request.QueryString["id"];
        string dt_one = "select * from Withdraw  where id='" + id + "'";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
        rep_km.DataSource = dt;
        rep_km.DataBind();

    }



    protected string GetAuditState(string auditState)
    {
        string msg = "";
        switch (auditState)
        {
            case "0":
                msg = "<span style='color:red; background: #fff;float: none'>" + GetTran("007254", "处理中") + "</span>";
                break;
            case "1":
                msg = "<font color='red'>" + GetTran("007254", "处理中") + "</font>";
                break;
            case "2":
                msg = "<font color='red'>" + GetTran("007169", "已汇出") + "</font>";
                break;
            case "3":
                msg = "<font color='red'>" + GetTran("007824", "账号有误") + "</font>";
                break;
            default:
                msg = "<font color='red'>" + GetTran("007168", "待审核") + "</font>";
                break;
        }
        return msg;
    }

    protected string GetWithdrawMoney(string WithdrawMoney)
    {
        string money = (double.Parse(WithdrawMoney) ).ToString("f2");
        return money;
    }

    protected string GetWithdrawTime(string WithdrawTime)
    {
        return DateTime.Parse(WithdrawTime).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }


}