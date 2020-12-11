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

public partial class SqlError : System.Web.UI.Page
{

    protected string msg;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ThreeRedirect(Page, "Member/" + Permissions.redirUrl);
        if (!IsPostBack)
        {
            string usertype = "";
            string user = "";
            if (Session["dian"] != null)
            {
                user = Session["dian"].ToString();
                usertype = "Store";
            }
            else if (Session["Company"] != null)
            {
                user = System.Web.HttpContext.Current.Session["Company"].ToString();
                usertype = "Company";
            }
            else if (Session["bh"] != null)
            {
                user = Session["bh"].ToString();
                usertype = "Member";
            }
            string usernum = "";// DBHelper.ExecuteScalar("select usernum from sqlerror where id=(Select max(id) from sqlerror)").ToString().Trim();
            if (usernum == null || usernum == "")
                //DBHelper.ExecuteNonQuery("update SqlError set usernum='" + user + "',usertype='" + usertype + "' where id=(Select max(id) from sqlerror)");

            msg = "<br><br><span color=red><h1>非法操作！</h1><br>";
            msg += "<a href='#' onclick='history.back()'>返回上一页</a></span>";

            if (Session["Refresh"] == null)
            {
                Session["Refresh"] = "0";
                Response.Write("<script>window.location.href=window.location.href</script>");
            }
            else
            {
                Session.Remove("Refresh");
            }
        }
    }
}
