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

public partial class Member_EmailFPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
    }
    public string returnUrl()
    {
        if (Request.QueryString["Type"] != null)
        {
            if (Request.QueryString["Type"].ToString() == "UnReadEmail")
            {
                return "ReceiveEmail.aspx";
            }
        }
        return "1";
    }
}
