using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.CommonClass;

public partial class MemberMobile_TxDetailHfxx : BLL.TranslationBase
{
     public int bzCurrency = 0;
     public double huilv;  
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        huilv = (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())));
        var id = Request.QueryString["id"];
        string dt_one = "select * from Withdraw where shenhestate=11 and HkDj=1 and IsJL=1 and HkKhname is not  null and id=" + id;
        DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
        rep_km.DataSource = dt;
        rep_km.DataBind();
    }
}