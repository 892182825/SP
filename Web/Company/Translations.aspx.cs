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
using Model.Other;

public partial class Company_Translations :BLL .TranslationBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //检查相应权限
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.SystemIntoDict);
        int ddlIndex = 0;
        int ddl1Idnex = 0;
        int ddl2Index = 0;
        if (Request.QueryString["ddl"] != null)
            ddlIndex = Convert.ToInt32(Request.QueryString["ddl"]);

        if (Request.QueryString["ddl1"] != null)
            ddl1Idnex = Convert.ToInt32(Request.QueryString["ddl1"]);

        if (Request.QueryString["ddl2"] != null)
            ddl2Index = Convert.ToInt32(Request.QueryString["ddl2"]);

        if (!IsPostBack)
        {
            LanguageBind(); 
            this.ddlLanguage.SelectedIndex = ddlIndex;
            this.ddlLanguageView1.SelectedIndex = ddl1Idnex;
            this.ddlLanguageView2.SelectedIndex = ddl2Index;

            if (Request.QueryString["index"] != null)
            {
                int index=0;            
               
              
                string _index = Request.QueryString["index"].Trim();
                if (_index != "-1")
                {
                    Pager1.pageIndex = int.Parse(_index);
                    PageInit();
                }
                else
                {
                    this.GridView1.PageIndex = 0;   // Convert.ToInt32(ViewState["pageCount"]);
                    Pager1.pageIndex = 1;
                    PageInit();
                }
                

            }
            else
            {
                PageInit();
            }           
        }
        Translations();
        
    }
    private void Translations()
    {
        this.TranControls(this.btnQuery, new string[][] { new string[] { "000340", "查询" } });

    }
    private void PageInit()
    {
        try
        {
            string keyCode = BLL.CommonClass.ValidData.InputText(this.txtQueryCode.Text.Trim());
            string keyWords = BLL.CommonClass.ValidData.InputText(this.txtKeywords.Text.Trim());
            string viewCode = BLL.CommonClass.ValidData.InputText(this.ddlLanguage.SelectedItem.Value.Trim());

            string table = " t_translation ";
            string columns = " keyCode as " + GetTran("006647", "词典编码") + " ,'" + GetTran("006640", "编辑") + "'  as " + GetTran("006640", "编辑") + " ";
            string condition = " 1=1 ";

            string language1 = this.ddlLanguageView1.SelectedItem.Value.Trim();
            string language2 = this.ddlLanguageView2.SelectedItem.Value.Trim();
            if (language1 != "" && language1 != "-1")
                columns += "," + ddlLanguageView1.SelectedItem.Value.Trim() + " as " + ddlLanguageView1.SelectedItem.Text;
            if (language2 != "" && language2 != "-1" && language2 != language1)
                columns += "," + ddlLanguageView2.SelectedItem.Value.Trim() + " as " + ddlLanguageView2.SelectedItem.Text;

            columns += ",description as " + GetTran("001680", "描述") + " ";

            if (keyWords != "")
            {
                condition += " and " + viewCode + " like '%"+keyWords+"%' ";
            }
            if (keyCode != "")
            {
                condition += " and keyCode='" + keyCode+"'";
            }         
            string key = "keyCode ";
            Pager1.PageSorting(Pager1.pageIndex, 10, table, columns, condition, key + " desc", "GridView1");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    /// <summary>
    /// 当前己有语言绑定
    /// </summary>
    private void LanguageBind()
    {
        string ExSql = "select id,languageCode,name from language order by id ";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(ExSql);
        this.ddlLanguage.Items.Clear();
        string languageCode = string.Empty;
        string languageName = string.Empty;
        this.ddlLanguageView1.Items.Clear();
        this.ddlLanguageView2.Items.Clear();
        ListItem li;
        foreach (DataRow dr in dt.Rows)
        {
            languageCode = dr["languageCode"].ToString().ToLower();
            languageName = dr["name"].ToString().Trim().ToLower();
            this.ddlLanguage.Items.Add(new ListItem(languageName, languageCode));
            this.ddlLanguageView1.Items.Add(new ListItem(languageName, languageCode));
            this.ddlLanguageView2.Items.Add(new ListItem(languageName, languageCode));
        }       
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        PageInit();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string keyCode = string.Empty;           
            if (e.Row.Cells.Count >= 2)
            {
                keyCode = e.Row.Cells[1].Text.Trim();
                e.Row.Cells[1].Attributes.Add("style", "width:100px;white-space:normal;text-align:center;");
            }
            if (e.Row.Cells.Count >= 3)
            {
                keyCode = e.Row.Cells[1].Text.Trim();
                string paras = "index=" + this.Pager1.pageIndex+"&ddl1="+this.ddlLanguageView1 .SelectedIndex +"&ddl2="+this.ddlLanguageView2 .SelectedIndex +"&ddl="+this.ddlLanguage .SelectedIndex;
                e.Row.Cells[2].Attributes.Add("onclick", "javascript:FunEdit('" + keyCode + "','" + paras+ "');");
                e.Row.Cells[2].Text = GetTran("006640", "编辑");
                e.Row.Cells[2].Attributes.Add("style", "cursor:pointer;text-align:center;width:60px;white-space:normal;");
            }
            if (e.Row.Cells.Count >= 4)
                e.Row.Cells[3].Attributes.Add("style", "width:300px;white-space:normal;");
            if (e.Row.Cells.Count >= 5)
                e.Row.Cells[4].Attributes.Add("style", "width:300px;white-space:normal;");
            if (e.Row.Cells.Count >= 6)
                e.Row.Cells[5].Attributes.Add("style", "width:300px;white-space:normal;");

        }
    }
}
   