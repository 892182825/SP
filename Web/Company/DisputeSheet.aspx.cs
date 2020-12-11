using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Company_DisputeSheet : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(4502);
        if (!IsPostBack)
        {
            getbind();
        }
    }

    public void getbind()
    {
        StringBuilder sb = new StringBuilder();
        if (ddlhistory.SelectedValue == "0")
        {
            sb.Append(" 1=1 and (hchaoshi>0 or tchaoshi>0)");

            if (txtnumber.Text.Trim() != "")
            {
                if (DropDownList1.SelectedValue == "0")
                    sb.Append(" and hnumber = '" + txtnumber.Text.Trim() + "'");
                else if (DropDownList1.SelectedValue == "1")
                    sb.Append(" and tnumber = '" + txtnumber.Text.Trim() + "'");
                else
                    sb.Append(" and (tnumber = '" + txtnumber.Text.Trim() + "' or hnumber = '" + txtnumber.Text.Trim() + "')");
            }
            if (DropDownList2.SelectedValue != "-1")
            {
                sb.Append(" and " + DropDownList2.SelectedValue);
            }
            sb.Append(" and shenhestate!=20");
            string clounmname = "case when hstate=0 and hchaoshi>0 then 1 else 0 end as hzr,case when tstate=0 and tchaoshi>0 then 1 else 0 end as tzr ,*";
            string tablename = @"(select w.HkDjdate,w.TxDjdate,w.auditingtime, w.iscl,w.id as wid,remitnumber as hnumber,w.HkDj as hstate,w.TxDj as tstate,r.shenHestate as shenst,case 
when w.HkDj=1 then datediff(mi,w.auditingtime,dateadd(hh,-10,w.HkDjdate)) else datediff(mi,w.auditingtime,dateadd(hh,-2,getutcdate())) end as hchaoshi,
r.remitmoney as hmoney,w.HkJs as hshuoming,w.TxJs as hshuoming1, auditingtime as ctime,w.TxDjdate as hctime,r.remark as hremark,w.number as tnumber,
case when w.TxDj=1 and w.HkDj=0 then datediff(mi,dateadd(hh,2,w.auditingtime),w.TxDjdate)when w.TxDj=0 and w.HkDj=0 then datediff(mi,dateadd(hh,2,w.auditingtime),getutcdate()) 
when w.TxDj=1 and w.HkDj=1 then datediff(mi,w.HkDjdate,dateadd(hh,-24,w.TxDjdate)) else datediff(mi,w.HkDjdate,dateadd(hh,-24,getdate())) end as tchaoshi,w.withdrawmoney as tmoney,isnull(w.khname,'')+w.bankname+w.bankcard as bankinfo,w.shenhestate ,r.hkpzImglj
from withdraw w,remittances r where w.hkid=r.id and w.shenhestate not in(0,99,-1) and Jfcl=0 and w.isjl=1) as t";

        Pager1.PageBind(0, 10, tablename, clounmname, sb.ToString(), "wid", "Repeater1");

        }
        else if (ddlhistory.SelectedValue == "1")
        {

            sb.Append(" 1=1 and (hchaoshi>0 or tchaoshi>0)");

            if (txtnumber.Text.Trim() != "")
            {
                if (DropDownList1.SelectedValue == "0")
                    sb.Append(" and hnumber = '" + txtnumber.Text.Trim() + "'");
                else if (DropDownList1.SelectedValue == "1")
                    sb.Append(" and tnumber = '" + txtnumber.Text.Trim() + "'");
                else
                    sb.Append(" and (tnumber = '" + txtnumber.Text.Trim() + "' or hnumber = '" + txtnumber.Text.Trim() + "')");
            }
            if (DropDownList2.SelectedValue != "-1")
            {
                sb.Append(" and " + DropDownList2.SelectedValue);
            }
           // sb.Append(" and shenhestate!=20");
            string clounmname = "case when hstate=0 and hchaoshi>0 then 1 else 0 end as hzr,case when tstate=0 and tchaoshi>0 then 1 else 0 end as tzr ,*";
            string tablename = @"(select w.HkDjdate,w.TxDjdate,w.auditingtime, w.iscl,w.id as wid,remitnumber as hnumber,w.HkDj as hstate,w.TxDj as tstate,r.shenHestate as shenst,case 
when w.HkDj=1 then datediff(mi,w.auditingtime,dateadd(hh,-10,w.HkDjdate)) else datediff(mi,w.auditingtime,dateadd(hh,-2,getutcdate())) end as hchaoshi,
r.remitmoney as hmoney,w.HkJs as hshuoming,w.TxJs as hshuoming1, auditingtime as ctime,w.TxDjdate as hctime,r.remark as hremark,w.number as tnumber,
case when w.TxDj=1 and w.HkDj=0 then datediff(mi,dateadd(hh,2,w.auditingtime),w.TxDjdate)when w.TxDj=0 and w.HkDj=0 then datediff(mi,dateadd(hh,2,w.auditingtime),getutcdate()) 
when w.TxDj=1 and w.HkDj=1 then datediff(mi,w.HkDjdate,dateadd(hh,-24,w.TxDjdate)) else datediff(mi,w.HkDjdate,dateadd(hh,-24,getdate())) end as tchaoshi,w.withdrawmoney as tmoney,isnull(w.khname,'')+w.bankname+w.bankcard as bankinfo,w.shenhestate ,r.hkpzImglj
from withdraw w,remittances r where w.hkid=r.id and w.shenhestate not  in(0,99,-1) and iscl=1 and w.isjl=1) as t";

            Pager1.PageBind(0, 10, tablename, clounmname, sb.ToString(), "wid", "Repeater1");
          
        }


    }

    public string GetName(string number)
    {
        string sql = "select name from memberinfo where number='" + number + "'";
        object obj = DAL.DBHelper.ExecuteScalar(sql);
        if (obj != null)
            return obj.ToString();

        return "";
    }
    public string GetPhone(string number)
    {
        string sql = "select mobiletele from memberinfo where number='" + number + "'";
        object obj = DAL.DBHelper.ExecuteScalar(sql);
        if (obj != null)
            return obj.ToString();

        return "";
    }

    public string getchaoshi(string chaoshi)
    {
        double cs = Convert.ToDouble(chaoshi);
        if (cs < 0)
        {
            return "0";
        }
        else
        {
            double a = Math.Ceiling(cs/60)-1;
            double b = cs - a * 60;
            return a + ":" + b;
        }

    }
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "cl")
        {
            string[] args = e.CommandArgument.ToString().Split(',');
            Response.Redirect("DisputeSheet_cl.aspx?id=" + args[0] + "&money=" + args[1] + "&status=" + args[2]);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        getbind();
    }
}