using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Company_xiaoqujiang : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var qishu=Request.QueryString["qishu"];
        var number=Request.QueryString["id"];
        string dt_one = "select id,Number,DownNumber,TotalPV,bili,Bonus,ExpectNum from Bonus4Detail where ExpectNum=" + qishu + " and Number=" + number; ;
        //this.ucPagerMb1.PageInit(dt_one, "rep_km");
        DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("WagesDetail.aspx");
    }
}