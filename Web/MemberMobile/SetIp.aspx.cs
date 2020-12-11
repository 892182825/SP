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

public partial class Member_SetIp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        if (!this.IsPostBack)
        {
           
        }
    }
    /// <summary>
    /// 确定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        int iporgj = Convert.ToInt32(this.ip.SelectedValue);

        BLL.other.Company.WordlTimeBLL.UpdDisplayTimeType(2, iporgj, Session["Member"] + "");//由你选择的方式，更新显示时间类型

        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>a();</script>");
    }
}
