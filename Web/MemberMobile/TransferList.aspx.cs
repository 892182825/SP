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
public partial class Member_TransferList : BLL.TranslationBase
{
    public int bzCurrency = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack) {
            if (Session["Default_Currency"] == null) Session["Default_Currency"] = bzCurrency;
            btn_SeachList_Click(null, null);
            translation();
        }
    }

    private void translation() {
        TranControls(ddl_InAccount, new string[][] { new string[] { "000633", "全部" }, new string[] { "007259", "会员现金账户" }, new string[] { "007260", "会员消费账户" }, new string[] { "007253", "服务机构订货款" } });
        TranControls(ddl_OutAccount, new string[][] { new string[] { "000633", "全部" }, new string[] { "007259", "会员现金账户" }, new string[] { "007260", "会员消费账户" } });
        TranControls(ddl_TransferMoney, new string[][] { new string[] { "000361", "大于" }, new string[] { "000367", "小于" }, new string[] { "000372", "等于" } });
        TranControls(btn_SeachList, new string[][] { new string[] { "000340", "查询" } });
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


    protected void btn_SeachList_Click(object sender, EventArgs e)
    {

        double OutMoney = 0;
        //StringBuilder sb = new StringBuilder();
        //sb.Append("1=1 and outNumber='" + Session["Member"].ToString() + "'");

        //if (ddl_InAccount.SelectedValue != "-1")
        //{
        //    sb.Append(" and InAccountType=" + ddl_InAccount.SelectedValue);
        //}
        //if (ddl_OutAccount.SelectedValue != "-1")
        //{
        //    sb.Append(" and OutAccountType=" + ddl_OutAccount.SelectedValue);
        //}
       
        //if (txt_OutMoney.Text.Trim() != "")
        //{
        //    if (double.TryParse(txt_OutMoney.Text.Trim(), out OutMoney))
        //    {
        //        sb.Append(" and OutMoney" + ddl_TransferMoney.SelectedValue + OutMoney);
        //    }
        //    else
        //    {
        //        ScriptHelper.SetAlert(Page, GetTran("001094", "金额输入不正确！"));
        //        return;
        //    }
        //}
        //Pager.PageColumn = "*";
        //Pager.Pageindex = 0;
        //Pager.PageTable = "ECTransferDetail";
        //Pager.PageSize = 10;
        //Pager.key = "id";
        //Pager.ControlName = "rep_TransferList";
        //Pager.PageCount = 0;
        //Pager.Condition = sb.ToString();
        //Pager.PageBind();

        string sql;
        sql = "select * from  ECTransferDetail where 1=1 and outNumber='" + Session["Member"].ToString() + "' order by id desc ";
        if (ddl_InAccount.SelectedValue != "-1" && ddl_OutAccount.SelectedValue != "-1" && txt_OutMoney.Text.Trim() == "")
        {
            sql = "select * from  ECTransferDetail where 1=1 and outNumber='" + Session["Member"].ToString() + "' and InAccountType=" + ddl_InAccount.SelectedValue
                + " and OutAccountType=" + ddl_OutAccount.SelectedValue + " order by id desc ";
        }
        if (ddl_InAccount.SelectedValue != "-1" && ddl_OutAccount.SelectedValue == "-1" && txt_OutMoney.Text.Trim() == "")
        {
            sql = "select * from  ECTransferDetail where 1=1 and outNumber='" + Session["Member"].ToString() + "' and InAccountType=" + ddl_InAccount.SelectedValue + " order by id desc ";

        }

        if (ddl_InAccount.SelectedValue == "-1" && ddl_OutAccount.SelectedValue != "-1" && txt_OutMoney.Text.Trim() == "")
        {
            sql = "select * from  ECTransferDetail where 1=1 and outNumber='" + Session["Member"].ToString() + "' and OutAccountType=" + ddl_OutAccount.SelectedValue + " order by id desc ";
        }

        if (ddl_InAccount.SelectedValue == "-1" && ddl_OutAccount.SelectedValue != "-1" && txt_OutMoney.Text.Trim() != "")
        {
            if (double.TryParse(txt_OutMoney.Text.Trim(), out OutMoney))
            {
                sql = "select * from  ECTransferDetail where 1=1 and outNumber='" + Session["Member"].ToString()
                       + "' and InAccountType=" + ddl_InAccount.SelectedValue + " and OutAccountType=" + ddl_OutAccount.SelectedValue + " and OutMoney" + ddl_TransferMoney.SelectedValue + OutMoney + " order by id desc ";
            }
            else
            {
                ScriptHelper.SetAlert(Page, GetTran("001094", "金额输入不正确！"));
                return;
            }
        }

        if (ddl_InAccount.SelectedValue == "-1" && ddl_OutAccount.SelectedValue != "-1" && txt_OutMoney.Text.Trim() != "")
        {
            if (double.TryParse(txt_OutMoney.Text.Trim(), out OutMoney))
            {
                sql = "select * from  ECTransferDetail where 1=1 and outNumber='" + Session["Member"].ToString()
                     + "' and OutAccountType=" + ddl_OutAccount.SelectedValue + " and OutMoney" + ddl_TransferMoney.SelectedValue + OutMoney + " order by id desc ";
            }
            else
            {
                ScriptHelper.SetAlert(Page, GetTran("001094", "金额输入不正确！"));
                return;
            }
        }
        if (ddl_InAccount.SelectedValue != "-1" && ddl_OutAccount.SelectedValue == "-1" && txt_OutMoney.Text.Trim() != "")
        {
            if (double.TryParse(txt_OutMoney.Text.Trim(), out OutMoney))
            {
                sql = "select * from  ECTransferDetail where 1=1 and outNumber='" + Session["Member"].ToString()
                      + "' and InAccountType=" + ddl_InAccount.SelectedValue + " and OutMoney" + ddl_TransferMoney.SelectedValue + OutMoney + " order by id desc ";
            }
            else
            {
                ScriptHelper.SetAlert(Page, GetTran("001094", "金额输入不正确！"));
                return;
            }
        }
        if (ddl_InAccount.SelectedValue == "-1" && ddl_OutAccount.SelectedValue == "-1" && txt_OutMoney.Text.Trim() != "")
        {

            if (double.TryParse(txt_OutMoney.Text.Trim(), out OutMoney))
            {
                sql = "select * from  ECTransferDetail where 1=1 and outNumber='" + Session["Member"].ToString()
                   + "' and OutMoney" + ddl_TransferMoney.SelectedValue + OutMoney + " order by id desc ";
            }
            else
            {
                ScriptHelper.SetAlert(Page, GetTran("001094", "金额输入不正确！"));
                return;
            }
        }
        //this.ucPagerMb1.PageSize = 10;
        //this.ucPagerMb1.PageInit(sql, "rep_TransferList");
    }

    /// <summary>
    /// 获取转入账户类型
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    protected string GetInAccountType(int type) {
        switch (type)
        {
            case (int)InAccountType.MemberCash:
                return GetTran("007259", "会员现金账户");
            case (int)InAccountType.MemberCons:
                return GetTran("007260", "会员消费账户");
            case (int)InAccountType.StoreOrder:
                return GetTran("007253", "服务机构订货款");
        }
        return ""; 
    }

    /// <summary>
    /// 获取转出账户类型
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    protected string GetOutAccountType(int Type) {
        switch (Type)
        {
            case (int)OutAccountType.MemberCash:
                return GetTran("007259", "会员现金账户");
            case (int)OutAccountType.MemberCons:
                return GetTran("007260", "会员消费账户");
        }
        return "";
    }
}
