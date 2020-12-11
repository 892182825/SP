using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DAL;

public partial class Member_SetLanguage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        if (!this.IsPostBack)
        {
            GetBind();
        }
    }
    /// <summary>
    /// 绑定语言种类
    /// </summary>
    private void GetBind()
    {
        SqlDataReader dr = DBHelper.ExecuteReader("select Name,languageCode from dbo.Language");
        while (dr.Read())
        {
            ListItem item = new ListItem() ;
            item.Text = dr["Name"].ToString();
            item.Value = dr["languageCode"].ToString();
            this.Language.Items.Add(item);
        }
        dr.Close();

        this.Language.SelectedValue = Session["languageCode"].ToString();
    }
    /// <summary>
    /// 确认设置
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        //语言
        string sess = this.Language.SelectedValue;
    
        Session["languageCode"] = sess;
        Session["LanguageID"] = DBHelper.ExecuteScalar("select id from dbo.Language where languagecode='" + sess + "'");

        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>a();</script>");
    }
}
