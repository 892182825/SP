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

public partial class Company_LanguageManage : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SystemIntoDict);
        if (!IsPostBack)
        {
            LanguageBind();
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.btnSearch, new string[][] { new string[] { "000340", "查询" } });
        this.TranControls(this.GridView1, new string[][] {
            new string[] { "000015", "操作" } ,
            new string[] { "006022", "语言代码" } ,
            new string[] { "006023", "语言名称" } ,
            new string[] { "006637", "语言简介" } 
        });

    }
    private void LanguageBind()
    {
        string table = " language ";
        string columns = "id,name,languageCode,languageRemark";
        string sort = " id desc";

        string condition = GetCondition();
        // string ExSql = "select "+columns +" from "+table +" where "+condition +" order by "+sort ;


        Pager1.PageSorting(1, 10, table, columns, condition, sort, "GridView1");
        //this.Pager1.Visible = false;

    }

    private string GetCondition()
    {
        string condition = " 1=1 ";

        string languageName = BLL.CommonClass.ValidData.InputText(this.txtLanguageName.Text);
        if (languageName != "")
        {
            condition += " and Name='" + languageName + "'";
        }

        string languageCode = BLL.CommonClass.ValidData.InputText(this.txtLanguageCode.Text);
        if (languageCode != "" && languageCode != "-1")
        {
            condition += " and languageCode='" + languageCode + "'";
        }
        return condition;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnDelLanguage = (LinkButton)e.Row.FindControl("lbtnDelLanguage");
            lbtnDelLanguage.Attributes.Add("onclick", "return confirm('" + GetTran("001718", "确实要删除吗") + "');");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LanguageBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Trim() == "del")
        {
            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent);
            string LanguageCode = ((Label)gvrow.FindControl("lblLanguageCode")).Text.Trim();
            string id = e.CommandArgument.ToString();
            ChangeLogs cl = new ChangeLogs("language", "ltrim(rtrim(str(ID)))");
            cl.AddRecord(id);

            int var = BLL.other.Company.LanguageBLL.DelLanguage(Convert.ToInt32(id), LanguageCode);
            if (var > 0)
            {
                cl.AddRecord(id);
                cl.DeletedIntoLogs(ChangeCategory.company35, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);

                BLL.CommonClass.Transforms.JSAlert(GetTran("000008", "删除成功！"));
                LanguageBind();
            }
            else
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("000009", "删除失败！"));
            }
        }

    }
}
