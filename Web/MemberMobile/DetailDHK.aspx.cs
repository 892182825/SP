using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.CommonClass;

public partial class MemberMobile_DetailDHK : BLL.TranslationBase
{
    public int bzCurrency = 0;
    public double huilv;
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        huilv = (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())));
        string dt_one = "";
        var hkid = Request.QueryString["hkid"];
        var hybh = Session["Member"].ToString();
        var time = DateTime.Now.AddHours(-10);
        Hkid.Value = hkid;
        if (hybh == "8888888888")
        {
            dt_one = "select * from (select w.id,w.hkid,w.bankcard,w.bankname,w.shenHestate,w.WithdrawMoney,m.name,RemittancesDate,w.auditingtime,m.Number from memberinfo m, withdraw w,remittances r where m.Number = w.number and w.hkid = r.ID and w.shenhestate in(1, 3)and  m.Number!='" + hybh + "' and w.auditingtime>='" + time + "' union select id,ID as hkid,ImportNumber as bankcard,ImportBank as bankname,shenhestate,RemitMoney as WithdrawMoney,name,RemittancesDate,ReceivablesDate as auditingtime,RemitNumber as Number from Remittances where isjl = 1 and shenhestate in(1, 3) and ReceivablesDate>='" + time + "' ) as t order by auditingtime desc";

        }// 
        else {

            dt_one = "select * from (select w.id,w.hkid,w.bankcard,w.bankname,w.shenHestate,w.WithdrawMoney,m.name,RemittancesDate,w.auditingtime,m.Number from memberinfo m, withdraw w,remittances r where m.Number = w.number and w.hkid = r.ID and w.shenhestate in(1, 3) and  m.Number!='" + hybh + "'  and   w.auditingtime>='" + time + "'    and  r.RemitNumber='" + hybh + "'  union select id,ID as hkid,ImportNumber as bankcard,ImportBank as bankname,shenhestate,RemitMoney as WithdrawMoney,name,RemittancesDate,ReceivablesDate as auditingtime,RemitNumber as Number from Remittances where isjl = 1 and shenhestate in(1, 3) and ReceivablesDate>='" + time + "'   ) as t order by auditingtime desc";
        }
        //3-16去掉时间限制，超时的也显示
        //   
       
        DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
        var count = dt.Rows.Count;
        rep_km.DataSource = dt;
        rep_km.DataBind();



        //string sql1 = "select * from Withdraw where hkid='" + hkid + "'";
        //DataTable dt1 = DAL.DBHelper.ExecuteDataTable(sql1);


        //if (dt1.Rows.Count == 0)
        //{
        //    rep_km1.Visible = true;
        //    rep_km.Visible = false;
        //    dt_one = " select top(1)* from companybank  order by ID desc";
        //    DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
        //    rep_km1.DataSource = dt;
        //    rep_km1.DataBind();
        //}
        //else
        //{
        //    rep_km.Visible = true;
        //    rep_km1.Visible = false;
        //    dt_one = "select w.id,w.hkid,w.bankcard,w.bankname,w.shenHestate,w.WithdrawMoney,m.name,RemittancesDate,w.auditingtime from memberinfo m, withdraw w,remittances r where m.Number=w.number and w.hkid=r.ID and w.shenhestate=3 and w.auditingtime>='" + time + "' order by w.auditingtime  desc";
        //    DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
        //    rep_km.DataSource = dt;
        //    rep_km.DataBind();
        //}




        //this.ucPagerMb1.PageInit(dt_one, "rep_km");

    }

    protected void rep_km_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        { 
            Button b = e.Item.FindControl("Button1") as Button;
            HiddenField h = e.Item.FindControl("HiddenField1") as HiddenField;
            HiddenField h2 = e.Item.FindControl("HiddenField2") as HiddenField;
            HiddenField h3 = e.Item.FindControl("HiddenField3") as HiddenField;
            HyperLink hl = e.Item.FindControl("HyperLink1") as HyperLink;
            hl.Text = GetTran("009088", "通知查收");

            hl.NavigateUrl = "HCRXX.aspx?hkid=" + h.Value + " &hkmoney=" + h2.Value + " &id=" + h3.Value;

        }
    }
}

    //protected void rep_km1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    var ID = Request.QueryString["hkid"];
    //    var RemitMoney = Request.QueryString["RemitMoney"];
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        Label l = e.Item.FindControl("Label1") as Label;

    //        Button b = e.Item.FindControl("Button1") as Button;
    //        l.Text = RemitMoney;
    //        HyperLink hl = e.Item.FindControl("HyperLink1") as HyperLink;
    //        hl.NavigateUrl = "/MemberMobile/HCRXX.aspx?hkmoney=" + RemitMoney + " &id=" + ID;
    //    }
    //}


