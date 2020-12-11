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

public partial class Company_DefaultDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetBind();
        }
    }


    protected object GetRDate(object obj)
    {
        if (obj != null)
        {
            try { return Convert.ToDateTime(obj).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()); }
            catch { }
        }
        return "";
    }

    private void GetBind()
    {
        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.Pageindex = 0;
        pager.PageSize = 10;
        pager.PageTable = "updDefaultDedails";
        pager.Condition = " Type = " + Request.QueryString["type"].ToString();
        pager.PageColumn = " * ";
        pager.ControlName = "GridView1";
        pager.key = " ID  ";
        pager.InitBindData = true;
        pager.PageBind();
    }
}
