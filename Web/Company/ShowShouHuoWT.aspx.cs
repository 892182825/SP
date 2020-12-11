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
using DAL;


public partial class Company_ShowShouHuoWT : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            string storeOrderID = Request.QueryString["storeOrderID"].ToString();

            Literal1.Text = DBHelper.ExecuteScalar("select feedback from storeorder where storeorderid='"+storeOrderID+"'")+"";

        }
    }
}
