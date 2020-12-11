using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.CommonClass;

public partial class MemberMobile_TxDetailCkhkpz : BLL.TranslationBase
{
    public int bzCurrency = 0;
    public double huilv;
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        huilv = (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())));
        var id = Request.QueryString["id"];
        string dt_one = "select * from Withdraw where  id=" + id;
        DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
        Image1.ImageUrl = "../hkpzimg/"+ dt.Rows[0]["hkpzImglj"].ToString();
        //rep_km.DataSource = dt;
        //rep_km.DataBind();
    }
}