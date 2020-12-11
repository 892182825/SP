using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;
using System.Xml.Linq;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //注销登录
        string redirectUrl = "";
        if (Request.QueryString["tp"] != null)
        {
            string tp = Request.QueryString["tp"];
            if (tp == "gongs")
            {
                redirectUrl = "Company/index.aspx";
            }
            else { redirectUrl = "Member/index.aspx"; }
        }
        else  if (Session["Company"] != null)
        {
            redirectUrl = "Company/index.aspx";

            //删除以前遗留的记录
            DAL.DBHelper.ExecuteNonQuery("DELETE FROM Log WHERE number='" + Session["Company"].ToString() + "' and Categories='guanli'");
        }
        else   if (Session["Store"] != null)
        {
            redirectUrl = "Store/index.aspx";

            //删除以前遗留的记录
            DAL.DBHelper.ExecuteNonQuery("DELETE FROM Log WHERE number='" + Session["Store"].ToString() + "' and Categories='dianpu'");
        }

        //System.Web.HttpBrowserCapabilities myBrowserCaps = Request.Browser;
        //int isMobile = ((System.Web.Configuration.HttpCapabilitiesBase)myBrowserCaps).IsMobileDevice ? 1 : 0;

        int isMobile = 0;
        if (!IsPC())
        {
            isMobile = 1;
        }
        //int x = Screen.PrimaryScreen.Bounds.Width;
        //if (x < 700)
        //{
        //    isMobile = 1;      
        //}
        if (Session["Member"] != null)
        {
            redirectUrl = "Member/index.aspx";

            if (isMobile == 1)
            {
                redirectUrl = "Membermobile/index.aspx";
            }

            //删除以前遗留的记录				
            DAL.DBHelper.ExecuteNonQuery("DELETE FROM Log WHERE number='" + Session["Member"].ToString() + "' and Categories='huiyuan'");
        }

        if (Session["Branch"] != null)
        {
            redirectUrl = "Branch/index.aspx";

            //删除以前遗留的记录				
            DAL.DBHelper.ExecuteNonQuery("DELETE FROM Log WHERE number='" + Session["Branch"].ToString() + "' and Categories='Branch'");
        }
        if (Session["DHNumbers"] != null)
        {
            Session["DHNumbers"] = "";
            Session.Remove("DHNumbers");
        }

        if (Request["tp"] != null)
        {
            if (Request["tp"].ToString() == "huiy")
            {
                redirectUrl = "Member/index.aspx";
                if (isMobile == 1)
                {
                    redirectUrl = "Membermobile/index.aspx";
                }
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
                if (Session["Membermobile"] != null)
                {
                    redirectUrl = "Membermobile/index.aspx";
                }
                else
                {
                    redirectUrl = "Membermobile/index.aspx";
                }
                if (isMobile == 1)
                {
                    redirectUrl = "Membermobile/index.aspx";
                }
            }
        }

        Session.Remove("Company");
        Session.Remove("Store");
        Session.Remove("Member");
        Session.Remove("Branch");
        Session.Remove("permission");
        Session.Remove("Success");
        Session["ReFurbish_Timeout"] = DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout);
        if (redirectUrl == "") redirectUrl = "Membermobile/index.aspx";
        Response.Redirect(redirectUrl);
    }
    public bool IsPC()
    {
        string userAgentInfo = Request.UserAgent;
        string[] Agents = new string[]{"Android", "iPhone",
                    "SymbianOS", "Windows Phone",
                    "iPad", "iPod"};
        var flag = true;
        for (var v = 0; v < Agents.Length; v++)
        {
            if (userAgentInfo.IndexOf(Agents[v]) > 0)
            {
                flag = false;
                break;
            }
        }
        return flag;
    }
}