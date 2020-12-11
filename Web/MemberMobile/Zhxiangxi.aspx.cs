using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.CommonClass;

public partial class MemberMobile_Zhxiangxi : BLL.TranslationBase
{
     public int bzCurrency = 0;
     public string jine = "";
     public string nic = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        string bh = Session["Member"].ToString();
        var id= Request.QueryString["id"];
        string dt_one = "select * from memberaccount where number='" + bh + "' and id=" + id ;
        //this.ucPagerMb1.PageInit(dt_one, "rep_km");
        DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);

        string mm = dt.Rows[0]["Direction"].ToString();
        nic = CommonDataBLL.GetPetNameByNumber(bh);
        if (mm == "0")
        {
            jine = " + " + dt.Rows[0]["HappenMoney"].ToString();
            
        }
        else
        {
            jine = " - " + dt.Rows[0]["HappenMoney"].ToString();
            
        }
       // dt.Rows[0]["HappenTime"] = Convert.ToDateTime(dt.Rows[0]["HappenTime"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours());
        rep_km.DataSource = dt;
        rep_km.DataBind();
    }


    protected string getMark(string remark)
    {
        string res = "";
        if (remark.IndexOf('~') > 0)
        {
            for (int i = 0; i < remark.Split('~').Length; i++)
            {
                res += GetTran(remark.Split('~')[i], "") == "" ? remark.Split('~')[i] : GetTran(remark.Split('~')[i], "");
            }
        }
        else
        {
            res = remark;
        }
        return res;
    }
}