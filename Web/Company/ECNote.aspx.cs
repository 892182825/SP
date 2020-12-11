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

using BLL.MoneyFlows;
public partial class Company_ECNote : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        getnode();
    }
    public void getnode()
    {
        int expectnum = 0;
        string number = string.Empty;
        int id = 0;
        if (Request.QueryString["expectnum"] != null && Request.QueryString["number"] != null && Request.QueryString["id"] != null)
        {
            expectnum = Convert.ToInt32(Request.QueryString["expectnum"].ToString().Trim());
            number = Request.QueryString["number"].ToString().Trim();
            id = Convert.ToInt32(Request.QueryString["id"].ToString());
        }
        divnode.InnerText = DeductBLL.Reason(expectnum, number,id).ToString();

    }
}
