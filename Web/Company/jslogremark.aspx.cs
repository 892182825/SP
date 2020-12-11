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
using Model;
using BLL.CommonClass;

public partial class Company_jslogremark : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);

        if (Request.QueryString["id"] != null)
        {
            this.lb.Text = "<table width='100%' border='0' cellpadding='0' cellspacing='1' bgcolor='#F7FBFC' class='talbebk'  id='tbColor'><tr class='zitbmenu'><td><b>" + GetTran("005837", "备注信息查看") + "</b></td></tr><tr><td>&nbsp;</td></tr>";
            this.lb.Text = this.lb.Text + "<tr><td>" + CommonDataBLL.Getjsremark(Request.QueryString["id"].Trim()) + "</td></tr><tr><td align=center><br><a href=# onclick='javascript:window.opener=null;window.close()'>" + GetTran("001225", "关闭窗口") + "</a></td></tr></table>";
        }
    }
}
