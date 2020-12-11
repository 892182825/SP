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

public partial class TranslationEdit : BLL.TranslationBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SystemIntoDict);
        if (!IsPostBack)
        {
            LanguageBind();
            if (Request.QueryString["keyCode"] != null)
            {
                string keyCode = Request.QueryString["keyCode"].Trim();
                DefaultBind(keyCode);
            }
            else
            {
                Response.Write(GetTran("001206", "参数错误"));
                Response.End();
            }
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.btnEdit, new string[][] { new string[] { "000036", "修改" } });
        this.TranControls(this.btmBack, new string[][] { new string[] { "000421", "返回" } });   

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
        foreach (DataRow dr in dt.Rows)
        {
            languageCode = dr["languageCode"].ToString().ToLower();
            languageName = dr["name"].ToString().Trim().ToLower();
            this.ddlLanguage.Items.Add(new ListItem(languageName, languageCode));
        }
    }

    private void DefaultBind(string keyCode)
    {
        string ExSql = "select * from t_translation  where keycode=@keycode";
        string languageCode = this.ddlLanguage.SelectedItem.Value.Trim();
        SqlParameter[] spas;

        spas = new SqlParameter[] { new SqlParameter("@keycode", SqlDbType.Char, 6) 
            };
        spas[0].Value = keyCode;
        SqlDataReader sdr = DAL.DBHelper.ExecuteReader(ExSql, spas, CommandType.Text);
        while (sdr.Read())
        {
            this.txtCode.Text = keyCode;
            this.txtCode.ReadOnly = true;
            this.txtDesc.Text = sdr["description"].ToString();
            this.txtLanguage.Text = sdr[languageCode].ToString();
        }
        sdr.Close();

    }
    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        string keyCode = Request.QueryString["keyCode"].Trim();
        DefaultBind(keyCode);
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            string columnName = this.ddlLanguage.SelectedItem.Value.Trim();
            string language = this.txtLanguage.Text.Trim();
            string desc = this.txtDesc.Text.Trim();
            string keyCode = this.txtCode.Text.Trim();
            string ExSql = string.Empty;
            SqlParameter[] spas;
            ExSql = "select count(1) from t_translation where  keyCode!=@keyCode and " + columnName + "=@language";
            spas = new SqlParameter[] { 
                new SqlParameter ("@keyCode",SqlDbType .Char ,6),
                new SqlParameter ("@language",SqlDbType.NVarChar ,1000)               
            };
            spas[0].Value = keyCode;
            spas[1].Value = language;
            int var = Convert.ToInt32(DAL.DBHelper.ExecuteScalar(ExSql, spas, CommandType.Text));
            if (var > 0)
            {
                BLL.CommonClass.Transforms.JSAlert ("此语言己存在！");
                return;
            }
            else
            {
                ExSql = "update t_translation set " + columnName + "=@language,description=@description  where keyCode=@keyCode";
                spas = new SqlParameter[] { 
                new SqlParameter ("@keyCode",SqlDbType .Char ,6),
                new SqlParameter ("@language",SqlDbType.NVarChar ,1000),
                new SqlParameter ("@description",SqlDbType.NVarChar ,100)
            };
                spas[0].Value = keyCode;
                spas[1].Value = language;
                spas[2].Value = desc;
                var = DAL.DBHelper.ExecuteNonQuery(ExSql, spas, CommandType.Text);
                if (var > 0)
                {
                    BLL.CommonClass.Transforms.JSExec("if(!confirm('修改成功，是否继续编辑？')){window.returnValue=1;window.close();}");
                    // DefaultBind(keyCode);
                }
                else
                {
                    BLL.CommonClass.Transforms.JSAlert("修改失败！");
                }
            }
        }
        catch (Exception ex)
        {
            BLL.CommonClass.Transforms.JSAlert("修改失败：" + BLL.CommonClass.ValidData.InputText(ex.Message));
            //  Response.Write(ex.Message);
        }

    }
    protected void btmBack_Click(object sender, EventArgs e)
    {
        string index = Request.QueryString["index"];
        string url = "index.aspx?index=" + index;
        // Response.Redirect(url);
        //BLL.CommonClass.ValidData.JSExec("window.location.href='index.aspx?index=" + index + "';");
        BLL.CommonClass.Transforms.JSExec("window.returnValue=1;window.close();");
    }
}
