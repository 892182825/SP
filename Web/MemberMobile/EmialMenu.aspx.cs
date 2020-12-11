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

public partial class Member_EmialMenu : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            GetMenu();
        }
    }

    public void GetMenu()
    {
        //string sql = "select id,classname from msgclass";
        //DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
        //if (dt.Rows.Count > 0)
        //{
        //    ul_span.InnerHtml = "";
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        ul_span.InnerHtml += "<li id='r" + dr["id"].ToString() + "' onclick='onchange(3" + dr["id"].ToString() + ")'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='#'>" + dr["classname"].ToString() + "</a></li> ";
        //    }
        //}
    }
}
