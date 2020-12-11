using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Other;

public partial class weiyuejin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(EnumCompanyPermission.AccountFlowManagerWYJTj);
        if (!IsPostBack)
        {
            getbind();
        }
    }

    public void getbind()
    {



        string table = "MemberAccount";
        string clounm = "id, Number,HappenTime,HappenMoney,BalanceMoney,Direction,SfType,KmType,Remark	";
        StringBuilder sb = new StringBuilder();
        sb.Append("   KmType =0 and Remark like '%违约'  ");
     
        if (txtnumber.Text.Trim() != "")
        {
            sb.Append(" and r.remitnumber = '" + txtnumber.Text.Trim() + "'");
        }
      
        if (txtbeigandate.Text.Trim() != "")
        {
            sb.Append(" and r.remittancesdate >= '" + txtbeigandate.Text.Trim() + " 00:00:00'");
        }
        if (txtenddate.Text.Trim() != "")
        {
            sb.Append(" and r.remittancesdate <= '" + txtenddate.Text.Trim() + " 23:59:59'");
        }


        object obj = DAL.DBHelper.ExecuteScalar("select  sum(isnull(HappenMoney,0))  from " + table + "  where " + sb);
        lblwyjzj.Text =Convert.ToDouble((obj==null||obj==DBNull.Value)?0:obj ).ToString("0.00");



        Pager1.PageBind(0, 10, table, clounm, sb.ToString(), "id", "Repeater1");
    }

    public string getstate(string state)
    {
        /*
         
            0 初始状态，可以匹配单
            1 匹配成功
            3 待汇出
            2 超时待查收
            11 等待确认

            99 原始单，不能匹配【作废单】
            98 二次撤销单，不能匹配【作废单】

            -1 撤销单，不能匹配
            20 已到账

            30 提现超时，公司打款
            31 提现公司打款，待确认
         
         */
        string str = "";
        if (state == "1" || state == "3" || state == "11")
        {
            str = "待查收";
        }
        else if (state == "20"||state == "2")
        {
            str = "已查收";
        }
        else if (state == "0")
        {
            str = "待撮合";
        }
        else if (state == "-1")
        {
            str = "已撤销";
        }
        else if (state == "98" || state == "99")
        {
            str = "已作废";
        }
        return str;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        getbind();
    }
}