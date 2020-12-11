<%@ WebHandler Language="C#" Class="AjaxPost" %>

using System;
using System.Web;
using System.Data;
using BLL.CommonClass;
using System.Text;
public class AjaxPost : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        
        context.Response.ContentType = "text/plain";
        string methon = context.Request["methon"] == "" ? "" : context.Request["methon"];
        
       
        try
        {
            if (methon != "")
            {
                if (methon == "getmegc")
                {
                    string number = context.Request["number"];
                    context.Response.Write( GetNewEmailCount(number).ToString());
                } if (methon == "getgwc")
                {
                    string number = context.Request["number"];
                    context.Response.Write( GetShopCartCount(number).ToString());
                }
            }
            else
            {
                context.Response.Write("0");
            }
        }
        catch { context.Response.Write("0"); }

    }
    public int GetShopCartCount(string number)
    {

        string sqlst = " select count(0) from  memShopCart where memBh='" + number + "'   ";
         
        int nmc = Convert.ToInt32(DAL.DBHelper.ExecuteScalar(sqlst));
        return nmc;
    }
    public int GetNewEmailCount(string number)
    { 
        string sqlst = " select COUNT(0) from  MessageReceive where Receive='" + number + "' and ReadFlag=0  and  loginrole=1 ";
        int nmc = Convert.ToInt32(DAL.DBHelper.ExecuteScalar(sqlst));
        return nmc;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}