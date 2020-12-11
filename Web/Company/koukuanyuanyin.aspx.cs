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
using System.Xml.Linq;
using Model;
using BLL.MoneyFlows;

public partial class Company_koukuanyuanyin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (Request.QueryString["type"] != null)
        {
            if (Request.QueryString["ID"] != "")
            {
                int ID = int.Parse(Request.QueryString["ID"]);
                this.DetailSpan.InnerHtml = ReleaseBLL.ChongHongBeizhu(ID);
                
            }
        }
        else
        {
            if (Request.QueryString["ID"] != "")
            {
                int ID = int.Parse(Request.QueryString["ID"]);
                this.DetailSpan.InnerHtml = ReleaseBLL.DeductReason(ID);
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("StrikeBalances.aspx");
    }
}