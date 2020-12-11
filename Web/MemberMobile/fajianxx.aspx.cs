using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MemberMobile_fajianxx : BLL.TranslationBase
{
    public string hybh;
    protected void Page_Load(object sender, EventArgs e)
    {
        hybh = Session["Member"].ToString();
        if (!IsPostBack)
        {
            string bh = Session["Member"].ToString();
            var id = Request.QueryString["id"];

            string dt_one1 = " update MessageSend set ReadFlag=1 where ID=" + id;
            DataTable dt1 = DAL.DBHelper.ExecuteDataTable(dt_one1);

            string dt_one = " select * from MessageSend where ID=" + id;
            //this.ucPagerMb1.PageInit(dt_one, "rep_km");
            DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
            rep_km.DataSource = dt;
            rep_km.DataBind();
        }
       
    }


    protected string getloginRole(string str)    // 前台调用(接受对象的转换)
    {
        switch (str.Trim())
        {
            case "2":
                return GetTran("000599", "会员");
            case "1":
                return GetTran("000388", "店铺");
            default:
                return GetTran("000151", "管理员");
        }
    }
}