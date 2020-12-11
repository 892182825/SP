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

public partial class Company_LevelIconsetting : BLL.TranslationBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            bind();
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"001637","级别名称"},
                    new string []{"001639","级别类型"},
                    new string []{"001641","级别图标"}
                });
    }
    public void bind()
    {
        this.GridView1.DataSource=CommonDataBLL.GetAllLevel();
        this.GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            if (e.Row.Cells[2].Text == "0")
            {
                e.Row.Cells[2].Text = GetTran("000903", "会员级别");
            }
            else
            {
                e.Row.Cells[2].Text = GetTran("001645", "店铺级别");
            }
        }
    }
}
