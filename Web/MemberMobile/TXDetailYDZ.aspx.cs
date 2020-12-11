using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.CommonClass;

public partial class MemberMobile_TXDetailYDZ : BLL.TranslationBase
{
    protected int bzCurrency = 0;
    protected double huilv;
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        huilv = (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())));
        var hybh = Session["Member"].ToString();
        string dt_one = "select id,hkid,Khname,bankcard,bankname,shenHestate,Ppje,WithdrawTime from withdraw where shenhestate=20 and IsJL=1 and number='" + hybh + "' order by WithdrawTime desc";//and HkDj=1 and TxDj=1
        DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
        rep_km.DataSource = dt;
        rep_km.DataBind();
    }
}