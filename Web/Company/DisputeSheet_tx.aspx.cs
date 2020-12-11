using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Company_DisputeSheet_tx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(4503);
        if (!IsPostBack)
        {
            getbind();
        }
    }

    public void getbind()
    {
        
        string table = @" withdraw w left join remittances r on r.id=w.hkid
 left join memberinfo m1 on r.remitnumber = m1.Number
 left join memberinfo m2 on w.number = m2.Number";
        //string table = "withdraw w left join remittances r on r.id=w.hkid ";
        //string clounm = "r.remittancesid,w.number as number,w.HkKhname as hkhname,w.Hkbankcard as hbankcard,w.khname as tkhname,w.bankcard as tbankcard,w.withdrawmoney,r.remittancesdate,w.shenhestate,r.id";
        string clounm = @"r.remittancesid,r.remitnumber as rnumber,r.remittancesdate,r.RemitMoney,r.RemitCardtype,r.AliNo,r.WeiXNo,r.ID,
r.bankcard,r.bankname,r.Khname,m1.Name as rname,
(CASE WHEN RemitCardtype= 0 THEN '' WHEN RemitCardtype = 1 THEN '支付宝号：'+r.AliNo
WHEN RemitCardtype = 2 THEN '微信号：'+r.WeiXNo END) as rinfo,
w.HkKhname as hkhname,w.Hkbankcard as hbankcard,w.khname as tkhname,w.bankcard as tbankcard,w.WithdrawTime,w.withdrawmoney,w.number,m2.Name as tname,w.shenhestate,w.InvestJB,
w.DrawCardtype,w.AliNo,w.WeiXNo,(CASE WHEN DrawCardtype= 0 THEN ' 银行卡号:'+w.bankcard
WHEN DrawCardtype = 1 THEN ' 支付宝号：'+w.AliNo
WHEN DrawCardtype = 2 THEN ' 微信号：'+w.WeiXNo END) as winfo";
        StringBuilder sb = new StringBuilder();
        sb.Append(" 1=1 and w.IsJL=1 ");
        if (txtdh.Text.Trim() != "")
        {
            sb.Append(" and r.remittancesid = '" + txtdh.Text.Trim() + "'");
        }
        if (txtnumber.Text.Trim() != "")
        {
            sb.Append(" and r.remitnumber = '" + txtnumber.Text.Trim() + "'");
        }
        if (txtsnumber.Text.Trim() != "")
        {
            sb.Append(" and w.number = '" + txtsnumber.Text.Trim() + "'");
        }
        if (txtbeigandate.Text.Trim() != "")
        {
            sb.Append(" and w.WithdrawTime >= '" + txtbeigandate.Text.Trim() + " 00:00:00'");
        }
        if (txtenddate.Text.Trim() != "")
        {
            sb.Append(" and w.WithdrawTime <= '" + txtenddate.Text.Trim() + " 23:59:59'");
        }
        if (DropDownList1.SelectedValue != "-2")
        {
            sb.Append(" and w.shenhestate in(" + DropDownList1.SelectedValue + ")");
        }
        else
        {
            sb.Append(" and w.shenhestate not in(-1,99,98,30,31)");
        }

        Pager1.PageBind(0, 10, table, clounm, sb.ToString(), "w.id", "Repeater1");
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