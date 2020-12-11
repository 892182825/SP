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

public partial class UserControl_paytop : System.Web.UI.UserControl
{
    public BLL.TranslationBase tran = new BLL.TranslationBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["member"] != null)
            {
                lblname.Text = Session["member"].ToString();
            }else
            if (Session["store"] != null)
            {
                lblname.Text = Session["store"].ToString();
            }
        }
    }
}
