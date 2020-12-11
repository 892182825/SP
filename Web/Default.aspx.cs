using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
        string ismobile = System.Configuration.ConfigurationSettings.AppSettings["ismobile"].ToString();
        if (ismobile == "1")
        {
            Response.Redirect("Membermobile/index.aspx");
        }
        Response.Redirect("Member/Index.aspx");*/
        //Label1.Text = Request.ServerVariables["REMOTE_ADDR"].ToString();


       
            Response.Redirect("MemberMobile/Index.aspx");
         
    }
}