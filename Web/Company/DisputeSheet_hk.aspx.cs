using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Company_DisputeSheet_hk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(4501);
        if (!IsPostBack)
        {
            getbind();
        }   
    }
    public void getbind()
    {
        //string table = "remittances r,withdraw w";
        string table = @"remittances r left join withdraw w on r.id=w.hkid
 left join memberinfo m1 on r.remitnumber = m1.Number
 left join memberinfo m2 on w.number = m2.Number";
        //string clounm = "r.remittancesid,r.remitnumber as number,w.HkKhname as hkhname,w.Hkbankcard as hbankcard,w.khname as tkhname,w.bankcard as tbankcard,w.withdrawmoney,r.remittancesdate,w.shenhestate,r.id";
        string clounm = @"r.remittancesid,r.remitnumber as rnumber,r.remittancesdate,r.RemitMoney,r.RemitCardtype,r.AliNo,r.WeiXNo,r.ID,
r.bankcard,r.bankname,r.Khname,m1.Name as rname,
(CASE WHEN RemitCardtype= 0 THEN '' WHEN RemitCardtype = 1 THEN '支付宝号：'+r.AliNo
WHEN RemitCardtype = 2 THEN '微信号：'+r.WeiXNo END) as rinfo,
w.HkKhname as hkhname,w.Hkbankcard as hbankcard,w.khname as tkhname,w.bankcard as tbankcard,w.withdrawmoney,w.number,m2.Name as tname,w.shenhestate,w.InvestJB,
w.DrawCardtype,w.AliNo,w.WeiXNo,(CASE WHEN DrawCardtype= 0 THEN ' 银行卡号:'+w.bankcard
WHEN DrawCardtype = 1 THEN ' 支付宝号：'+w.AliNo
WHEN DrawCardtype = 2 THEN ' 微信号：'+w.WeiXNo END) as winfo";
        StringBuilder sb = new StringBuilder();
        sb.Append(" 1=1 and r.id=w.hkid and w.shenhestate not in(0,99)");
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
            sb.Append(" and r.remittancesdate >= '" + txtbeigandate.Text.Trim() + " 00:00:00'");
        }
        if (txtenddate.Text.Trim() != "")
        {
            sb.Append(" and r.remittancesdate <= '" + txtenddate.Text.Trim() + " 23:59:59'");
        }
        if (DropDownList1.SelectedValue != "-2")
        {
            sb.Append(" and w.shenhestate=" + DropDownList1.SelectedValue);
        }

        Pager1.PageBind(0, 10, table, clounm, sb.ToString(), "r.id", "Repeater1");
    }

    protected string Remittancesinfo(int remitCardtype)
    {
        string str = "";
        if (remitCardtype == 0)
        {
            str = "";
        }
        else if (remitCardtype == 1)
        {
            
        }else if (remitCardtype == 2)
        {
            
        }
        return str;
    }

    protected string Withdrawinfo(int drawCardtype)
    {
        string str = "";
        if (drawCardtype == 0)
        {
            str = "";
        }
        else if (drawCardtype == 1)
        {

        }
        else if (drawCardtype == 2)
        {

        }
        return str;
    }

    public string getstate(string state)
    {
        string str = "";
        if (state == "1")
        {
            str = "匹配成功";
        }
        if (state == "3")
        {
            str = "待汇出";
        }
        if (state == "20")
        {
            str = "已查收";
        }
        if (state == "2")
        {
            str = "超时待查收";
        }
        if (state == "11")
        {
            str = "等待确认";
        }
        if (state == "-1")
        {
            str = "撤销单";
        }
        return str;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        getbind();
    }
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "cs")
        {
            string hkid = e.CommandArgument.ToString();
            SqlParameter[] par = new SqlParameter[]{
                new SqlParameter("@hkid",hkid),
                new SqlParameter("@err",SqlDbType.Int)
            };
            par[1].Direction = ParameterDirection.Output;
            DAL.DBHelper.ExecuteNonQuery("zfpp_cx", par, CommandType.StoredProcedure);
            int res = Convert.ToInt32(par[1].Value);
            if (res == 0)
            {
                getbind();
                ClientScript.RegisterStartupScript(GetType(), "msg", "alert('撤销成功')", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "msg", "alert('撤销失败')", true);
                return;
            }
        }
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item)
        {
           
        }
    }
}