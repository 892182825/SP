using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class AjaxPost : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string methon = Request["methon"] == "" ? "" :  Request["methon"];


        try
        {
            if (methon != "")
            {
                if (methon == "getmegc")
                {
                    string number = Request["number"];
                    Response.Write(GetNewEmailCount(number).ToString());
                } if (methon == "getgwc")
                {
                    string number = Request["number"];
                    Response.Write(GetShopCartCount(number).ToString());
                }
                if (methon == "getissm")
                {
                    string number = Request["number"];
                    Response.Write(GetISSm(number).ToString());
                }
                if (methon == "getjycc")
                {
                    string number = Request["number"];
                    Response.Write(GetEchargecount(number).ToString());
                } 
                if (methon == "getjytm")
                { 
                    Response.Write(GetISCanCharge() .ToString());
                }
            }
            else
            {
                Response.Write("0");
            }
        }
        catch { Response.Write("0"); }

    }
    public int GetShopCartCount(string number)
    {

        string sqlst = " select count(0) from  memShopCart where memBh='" + number + "'   ";

        int nmc = Convert.ToInt32(DAL.DBHelper.ExecuteScalar(sqlst));
        return nmc;
    }

    public int GetISSm(string number)
    { int  issm=0; 
        object obj = DAL.DBHelper.ExecuteScalar("select papernumber  from memberinfo where number='" + number + "'");
        if (!(obj == DBNull.Value || obj == null || obj.ToString() == "")) issm = 1;
        return issm;
    }
    public int GetNewEmailCount(string number)
    {
        string sqlst = " select COUNT(0) from  MessageReceive where (Receive='"+number+"' and ReadFlag=0  and  loginrole=2 ) or (Receive='*'  and ReadFlag=0 and MessageType='m') ";
        int nmc = Convert.ToInt32(DAL.DBHelper.ExecuteScalar(sqlst));
        return nmc;
    }

 public int GetEchargecount(string number)
    {
        string sqlst = "  select  count(0) from  Withdraw  w   join Remittances r on w.hkid=r.ID  where (w.number='"+number+"' or r.RemitNumber='"+number+"' ) and (w.shenHestate<>20 or r.shenHestate<>20) ";
        int nmc = Convert.ToInt32(DAL.DBHelper.ExecuteScalar(sqlst));
        return nmc;
    }




 /// <summary>
 /// 判断是否可以开始交易
 /// </summary>
 /// <returns></returns>
 public static string  GetISCanCharge()
 {
     string  rec = "0,9,21,";
     DateTime dn = DateTime.Now;

     string sql = "select top 1  Opentime,Closetime from  ExchangeTime order by id desc ";
     DataTable dt = DBHelper.ExecuteDataTable(sql);
     if (dt != null && dt.Rows.Count > 0)
     {
         DateTime open = Convert.ToDateTime(dt.Rows[0]["Opentime"]);
         DateTime close = Convert.ToDateTime(dt.Rows[0]["Closetime"]);

         if (dn.Hour >= open.Hour && dn.Hour <= close.Hour) rec = "1," + open.Hour+","+close.Hour+",";
     }
     else
     {
         if (dn.Hour >= 9 && dn.Hour < 21) rec = "1,9,21,";
     }
     return rec;
 }



}