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

public partial class Member_JiangjinDetial : BLL.TranslationBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        Translations();
    }

    private void Translations()
    {


        this.TranControls(this.BtnConfirm, new string[][] { new string[] { "000096", "返 回" } });


    }

    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("DetialQuery.aspx");
    }
}
