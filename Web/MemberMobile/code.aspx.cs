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
using DAL;
using System.Data.SqlClient;
using BLL.other;

using BLL.other.Company;

public partial class MemberMobile_code : BLL.TranslationBase
{
    protected string epurl = "";
    protected string weburl = ConfigurationManager.AppSettings["webUrl"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["Member"] != null)
            {
                epurl = Session["Member"].ToString();
            }
        }
    }
}