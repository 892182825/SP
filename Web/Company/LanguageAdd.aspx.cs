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
using BLL.CommonClass;

public partial class Company_LanguageAdd :BLL.TranslationBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SystemIntoDict);
        if (!IsPostBack)
        {
            Model.EnumOperateModel mode = Model.EnumOperateModel.enum_Add;
            try
            {
                if (Request.QueryString["mode"] != null)
                {
                    mode = (Model.EnumOperateModel)Convert.ToInt32(Request.QueryString["mode"].Trim());
                    if (mode == Model.EnumOperateModel.enum_Add)
                    {
                        this.lblTitle.Text = GetTran("006024", "添加新语言");
                        this.btnSubmit.Text = GetTran("006639", "添加");
                    }
                    else
                    {
                        this.lblTitle.Text = GetTran("006638", "语言修改");
                        this.btnSubmit.Text = GetTran("006640", "编辑");
                        if (Request.QueryString["id"] != null)
                        {
                            int id = int.Parse(Request.QueryString["id"].Trim());
                            DefaultBind(id);
                            ViewState["id"] = id;
                        }
                        else
                        {
                            Response.Write(GetTran("001206", "参数错误"));
                            Response.End();
                        }
                    }
                   
                }
                ViewState["mode"] = mode;
            }
            catch
            {
                Response.Write(GetTran("001206", "参数错误"));
                Response.End();
            }
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.btnSubmit, new string[][] { new string[] { "000036", "修改" } });       

    }
    /// <summary>
    /// 当前己有语言绑定
    /// </summary>
    private void DefaultBind(int id)
    {
        string ExSql = "select id,languageCode,name,languageRemark from language where id=@id ";
        SqlParameter[] spas = new SqlParameter[]{
            new SqlParameter("@id",SqlDbType.Int )               
            };
        spas[0].Value = id;

        SqlDataReader sdr = DAL.DBHelper.ExecuteReader(ExSql, spas, CommandType.Text);
        while (sdr.Read())
        {
            this.txtLanguageDesc.Text = sdr["languageRemark"].ToString();
            this.txtLanguageName.Text = sdr["name"].ToString();
        }
        sdr.Close();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string langugeName = BLL.CommonClass.ValidData.InputText( this.txtLanguageName.Text.Trim());
        string languageRemark = BLL.CommonClass.ValidData.InputText( this.txtLanguageDesc.Text.Trim());
        if (langugeName == "")
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("006641", "语言名不能为空！"));
            return;
        }
        if (languageRemark == "")
        {
            BLL.CommonClass.Transforms.JSAlert(GetTran("006642", "语言描述不能为空！"));
            return;
        } 

        int var=0;       
        Model.EnumOperateModel mode = (Model.EnumOperateModel)ViewState["mode"];
        if (mode == Model.EnumOperateModel.enum_Add)
        {
            var = BLL.other.Company.LanguageBLL.AddNewLanguage(langugeName, languageRemark);
           
            if (var > 0)
            {
                BLL.CommonClass.Transforms.JSExec("alert('" + GetTran("000006", "添加成功") + "');window.location.href='LanguageManage.aspx';");
            }
            else
                BLL.CommonClass.Transforms.JSAlert(GetTran("000007", "添加失败"));
        }
        else if (mode==Model.EnumOperateModel.enum_Edit)
        {
            ChangeLogs cl = new ChangeLogs("language", "ltrim(rtrim(str(ID)))");
            cl.AddRecord(ViewState["id"].ToString());
            int id = (int)ViewState["id"];
            var = BLL.other.Company.LanguageBLL.ModifyLanguage(id,langugeName, languageRemark);

            if (var > 0)
            {
                cl.AddRecord(ViewState["id"].ToString());
                cl.ModifiedIntoLogs(ChangeCategory.company35, Session["Company"].ToString(), ENUM_USERTYPE.objecttype9);
                BLL.CommonClass.Transforms.JSExec("alert('" + GetTran("000001", "修改成功") + "');window.location.href='LanguageManage.aspx';");
            }
            else
                BLL.CommonClass.Transforms.JSAlert(GetTran ("000002","修改失败"));
        }
    }
   
}
