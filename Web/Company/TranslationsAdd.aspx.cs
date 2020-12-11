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
using System.Data.SqlClient;

public partial class languageAdd :BLL .TranslationBase 
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
        this.TranControls(this.btnAdd, new string[][] { new string[] { "006639", "添加" } });

    }
    /// <summary>
    /// 当前己有语言绑定
    /// </summary>
    private void LanguageBind()
    {
        string ExSql = "select id,languageCode,name from language order by id ";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(ExSql);
        this.ddlLanguageAdd.Items.Clear();

        //this.ddlLanguage.Items.Add(new ListItem("--语言列表--", ""));
        string languageCode = string.Empty;
        string languageName = string.Empty;
        foreach (DataRow dr in dt.Rows)
        {
            languageCode = dr["languageCode"].ToString().ToLower();
            languageName = dr["name"].ToString().Trim().ToLower();
            this.ddlLanguageAdd.Items.Add(new ListItem(languageName, languageCode));
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        SqlConnection con = DAL.DBHelper.SqlCon();
        SqlTransaction tran = null;
        try
        {
            con.Open();
            tran = con.BeginTransaction();
            string columnName = this.ddlLanguageAdd.SelectedItem.Value.Trim();
            string defaultLanguage = this.txtDefaultLanguage.Text.Trim();
            string desc = this.txtDesc.Text.Trim();
            string keyCode = string.Empty;
            string ExSql = string.Empty;
            SqlParameter[] spas;
            ExSql = "select count(1) from t_translation where " + columnName + "=@defaultLanguage";
            spas = new SqlParameter[] { new SqlParameter("@defaultLanguage", SqlDbType.NVarChar, 1000) 
            };
            spas[0].Value = defaultLanguage;
            int exists = Convert.ToInt32(DAL.DBHelper.ExecuteScalar(tran, ExSql, spas, CommandType.Text));
            if (exists == 0)
            {
                ExSql = "select isnull(max(keyCode),0)+1 as newKeyCode  from t_translation";
                keyCode = DAL.DBHelper.ExecuteScalar(tran, ExSql,new SqlParameter[]{}, CommandType.Text).ToString().Trim();
                keyCode = keyCode.PadLeft(6, '0');
                ExSql = " insert into t_translation(keyCode," + columnName + ",description) "
                         + " values(@keyCode,@defaultLanguage,@description)";
                spas = new SqlParameter[] { 
                new SqlParameter ("@keyCode",SqlDbType .Char ,6),
                new SqlParameter ("@defaultLanguage",SqlDbType.NVarChar ,1000),
                new SqlParameter ("@description",SqlDbType.NVarChar ,100)
            };
                spas[0].Value = keyCode;
                spas[1].Value = defaultLanguage;
                spas[2].Value = desc;
                int var = DAL.DBHelper.ExecuteNonQuery(tran, ExSql, spas, CommandType.Text);
                if (var > 0)
                {
                    tran.Commit();
                    this.txtDefaultLanguage.Text = "";
                    this.txtDesc.Text = "";
                    BLL.CommonClass.Transforms.JSExec("if(!confirm('" + GetTran("006668", "添加成功，是否继续添加？") + "')) { window.returnValue;window.close();}");                  
                }
                else
                {
                    tran.Rollback();
                    BLL.CommonClass.Transforms.JSAlert(GetTran("000007", "添加失败"));
                }
            }
            else
            {
                BLL.CommonClass.Transforms.JSAlert(GetTran("006667", "此默认语言己存在"));
                tran.Rollback();
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            tran.Rollback();
        }
        finally
        {
            con.Close();
        }
    }
}

