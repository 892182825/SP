using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Company_SetParams_xiajimingdanxx : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
    }
    protected void lbtnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("../SetParameters.aspx");
    }
  
    protected void jsjj_Click(object sender, EventArgs e)
    {
        if (txtOpen.Text == "" && txtMoney.Text=="")
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("000000", "姓名或手机号至少一个不为空！"));
        }
        string sql = "";
        if (txtOpen.Text != "" && txtMoney.Text != "")
        {
             sql = @"  WITH tb AS (
select a.number,a.direct,a.TotalOneMark,0 AS cw,a.level2,c.MobileTele,a.DTotalNetRecord,c.Number from memberinfobalance1 a ,memberinfo c 
where  c.name='" + txtMoney.Text + "' and c.MobileTele='" + txtOpen.Text + "' and a.number=c.Number ";
            sql += @"
union all
select m.number ,m.direct,m.TotalOneMark,tb.cw+1,m.level2,d.MobileTele,m.DTotalNetRecord,d.Number  from memberinfobalance1 m ,tb ,memberinfo d where tb.number = m.direct and m.number=d.Number
)select  b.MobileTele,a.TotalOneMark,a.DTotalNetRecord,a.cw,b.name,c.OrderDate,case when c.ordertype=22 then '报单' when c.ordertype=23 then '升级'  end as bd,c.TotalPv  from tb a,memberinfo b,MemberOrder c where  a.number=b.Number  and a.number=c.Number and ordertype in(22,23) and PayExpectNum=1  order by a.cw,c.OrderDate desc ";
        }
        if (txtOpen.Text != "" && txtMoney.Text == "")
        {
             sql = @"  WITH tb AS (
select a.number,a.direct,a.TotalOneMark,0 AS cw,a.level2,c.MobileTele,a.DTotalNetRecord,c.Number from memberinfobalance1 a ,memberinfo c 
where  c.MobileTele='" + txtOpen.Text + "' and a.number=c.Number ";
            sql += @"
union all
select m.number ,m.direct,m.TotalOneMark,tb.cw+1,m.level2,d.MobileTele,m.DTotalNetRecord,d.Number  from memberinfobalance1 m ,tb ,memberinfo d where tb.number = m.direct and m.number=d.Number
)select  b.MobileTele,a.TotalOneMark,a.DTotalNetRecord,a.cw,b.name,c.OrderDate,case when c.ordertype=22 then '报单' when c.ordertype=23 then '升级'  end as bd,c.TotalPv  from tb a,memberinfo b,MemberOrder c where  a.number=b.Number  and a.number=c.Number and ordertype in(22,23) and PayExpectNum=1  order by a.cw,c.OrderDate desc ";
        }
        if (txtOpen.Text == "" && txtMoney.Text != "")
        {
             sql = @"  WITH tb AS (
select a.number,a.direct,a.TotalOneMark,0 AS cw,a.level2,c.MobileTele,a.DTotalNetRecord,c.Number from memberinfobalance1 a ,memberinfo c 
where  c.name='" + txtMoney.Text + "' and a.number=c.Number ";
            sql += @"
union all
select m.number ,m.direct,m.TotalOneMark,tb.cw+1,m.level2,d.MobileTele,m.DTotalNetRecord,d.Number  from memberinfobalance1 m ,tb ,memberinfo d where tb.number = m.direct and m.number=d.Number
)select  b.MobileTele,a.TotalOneMark,a.DTotalNetRecord,a.cw,b.name,c.OrderDate,case when c.ordertype=22 then '报单' when c.ordertype=23 then '升级'  end as bd,c.TotalPv  from tb a,memberinfo b,MemberOrder c where  a.number=b.Number  and a.number=c.Number and ordertype in(22,23) and PayExpectNum=1  order by a.cw,c.OrderDate desc ";
        }

        DataTable myda = DBHelper.ExecuteDataTable(sql);
        if (myda.Rows.Count > 0)
        {
            this.GridView1.DataSource = myda;
            this.GridView1.DataBind();
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('无此编号，请检查后再重新输入111！')</script>");
            return;
        }

    }
}