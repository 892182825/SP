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
using BLL.CommonClass;

public partial class Agreement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // Permissions.StoreRedirect(Page, Permissions.redirUrl);
        divShow.InnerHtml = CommonDataBLL.GetAgreement();
        if (Session["UserType"] != null)
        {
            int ut = Convert.ToInt32(Session["UserType"]);
            
        }
    }
    protected void btagree_Click(object sender, EventArgs e)
    {
        if (Session["UserType"] != null)
        { int  ut=Convert.ToInt32( Session["UserType"]);
        switch (ut)
        {
            case 1:
                Response.Redirect("RegisterMember/registermember.aspx");
                break;

            case 2:
                Response.Redirect("RegisterMember/registermember.aspx");
                break;

            case 3:
                Response.Redirect("RegisterMember/registermember.aspx");
                break;
            default:
                break;
        }
            Response.Redirect("default.aspx");
        }
    }
}
