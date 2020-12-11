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

public partial class RegisterUpdate1 : BLL.TranslationBase
{
    public string topurlstr, mainurlstr;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ThreeRedirect(Page, "Member/" + Permissions.redirUrl);
        if (!IsPostBack)
        {
            //topurlstr = "UpdateTop.aspx?CssType=" + Request.QueryString["CssType"] + "&OrderID=" + Request.QueryString["OrderID"].ToString() + "&Number=" + Request.QueryString["Number"].ToString();
            mainurlstr = "UpBasic.aspx?Number=" + Request.QueryString["Number"].ToString() + "&CssType=" + Request.QueryString["CssType"].ToString();

            ViewState["OrderID"] = Request.QueryString["OrderID"].ToString();
            ViewState["CssType"] = Request.QueryString["CssType"].ToString();
            ViewState["Number"] = Request.QueryString["Number"].ToString();

            //从会员,公司系统连接过来
            if (Request.QueryString["storeId"] != null)
                Session["storeId"] = Request.QueryString["storeId"].ToString();

            ViewState["tp"] = "-1";
            if (Request.QueryString["tp"] != null)
                ViewState["tp"] = Request.QueryString["tp"].ToString();

            int reg = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select COUNT(0) from MemberOrder where OrderID='" + ViewState["OrderID"].ToString() + "' and IsAgain=0"));
            if (reg == 0)
            {
                td1.Visible = false;
                td3.Visible = false;

                mainurlstr = "UpdZhuChe.aspx?OrderID=" + ViewState["OrderID"].ToString() + "&Number=" + ViewState["Number"].ToString() + "&CssType=" + ViewState["CssType"].ToString() + "&tp=" + ViewState["tp"].ToString();
            }
        }
    }

    public string GetFormatString(string str)
    {
        if (str.Length > 10)
            return str.Substring(0, 10) + "...";
        return str;
    }
}