using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.CommonClass;

public partial class MemberMobile_DetailYDZ : BLL.TranslationBase
{
      public int bzCurrency = 0;
      
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        var hkid = Request.QueryString["hkid"];
        var hybh = Session["Member"].ToString();
        string dt_one = "select * from (select w.id,w.hkid,w.bankcard,w.bankname,w.shenHestate,w.WithdrawMoney,m.name,RemittancesDate,w.auditingtime,m.Number from memberinfo m, withdraw w,remittances r where m.Number = w.number and w.hkid = r.ID and  w.shenhestate=20   and m.Number!='" + hybh + "' and r.RemitNumber='" + hybh + "'   union select id,ID as hkid,ImportNumber as bankcard,ImportBank as bankname,shenhestate,RemitMoney as WithdrawMoney,name,RemittancesDate,ReceivablesDate as auditingtime,RemitNumber as Number from Remittances where isjl = 1 and  shenhestate=20 and RemitNumber='" + hybh + "' ) as t order by auditingtime desc";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
        Hkid.Value = hkid;
        rep_km.DataSource = dt;
        rep_km.DataBind();
    }
}