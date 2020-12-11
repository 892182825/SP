using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logoutsj : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //注销登录
        string redirectUrl = "";
        if (Session["Company"] != null)
        {
            redirectUrl = "Company/index.aspx";
            //删除以前遗留的记录

            DAL.DBHelper.ExecuteNonQuery("DELETE FROM Log WHERE number='" + Session["Company"].ToString() + "' and Categories='guanli'");
        }
        //if (Session["Store"] != null)
        //{
        //    redirectUrl = "Store/index.aspx";
        //    //删除以前遗留的记录

        //    DAL.DBHelper.ExecuteNonQuery("DELETE FROM Log WHERE number='" + Session["Store"].ToString() + "' and Categories='dianpu'");

        //}
        if (Session["Member"] != null)
        {
            redirectUrl = "Membermobile/index.aspx";
            //删除以前遗留的记录				
            DAL.DBHelper.ExecuteNonQuery("DELETE FROM Log WHERE number='" + Session["Member"].ToString() + "' and Categories='huiyuan'");
        }
        //if (Session["Branch"] != null)
        //{
        //    redirectUrl = "Branch/index.aspx";
        //    //删除以前遗留的记录				
        //    DAL.DBHelper.ExecuteNonQuery("DELETE FROM Log WHERE number='" + Session["Branch"].ToString() + "' and Categories='Branch'");
        //}
        if (Session["DHNumbers"] != null)
        {
            Session["DHNumbers"] = "";
            Session.Remove("DHNumbers");
        }
        //if (Session["PassWordManage"]!=null)
        //{
        //    redirectUrl = "PassWordManage/FindPass.aspx";
        //}

        if (Request["tp"] != null)
        {
            if (Request["tp"].ToString() == "huiy")
            {
                redirectUrl = "Membermobile/index.aspx";
            }
            else if (Request["tp"].ToString() == "dianp")
            {
                redirectUrl = "Store/index.aspx";
            }
            else if (Request["tp"].ToString() == "dianp")
            {
                redirectUrl = "Company/index.aspx";
            }
        }
        else
        {
            if (redirectUrl == "")
            {
                redirectUrl = "Membermobile/index.aspx";
            }
        }

        Session.Remove("Company");
        Session.Remove("Store");
        Session.Remove("Member");
        Session.Remove("Branch");
        Session.Remove("permission");
        Session.Remove("Success");
        Session.Remove("LUOrder");
        Session["ReFurbish_Timeout"] = DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout);
        Response.Redirect(redirectUrl);
    }
}