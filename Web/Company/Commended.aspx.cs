using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Company_Commended : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var qishu = Request.QueryString["qishu"];
        var number = Request.QueryString["id"];
        if (string.IsNullOrEmpty(number) && string.IsNullOrEmpty(qishu))
        {
            return;
        }
        else
        {
            string dt_one = "select id,hybh,xjbh,orderid,bdbh,bdpv,bonus,qs from mx0 where hybh=@Number and qs=@qs";
            SqlParameter[] para =
                {
                    new SqlParameter("@Number",SqlDbType.NChar),
                    new SqlParameter("@qs",SqlDbType.Int),
                };
            para[0].Value = number;
            para[1].Value = qishu;
            //string dt_one = "select id,Number,DownNumber,orderId,yj,bili,Bonus,ExpectNum from Bonus1Detail where ExpectNum =" + qishu+" and number ="+number;
            //this.ucPagerMb1.PageInit(dt_one, "rep_km");
            DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one, para, CommandType.Text);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

    }

    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("WagesDetail.aspx");
    }
}