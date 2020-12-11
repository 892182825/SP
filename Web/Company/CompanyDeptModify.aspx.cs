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
using BLL.other.Company;
using Model;

public partial class Company_CompanyDeptModify : BLL.TranslationBase
{
    CompanyDeptBLL companydeptBLL = new CompanyDeptBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SafeRightDpetManage);
        if (!IsPostBack)
        { 
            //获取参数ID
            string mid = Request.QueryString["id"];
            int id = 0;
            //验证id类型，并将id转换成int类型
            try
            {
                id = int.Parse(mid);
            }
            catch (Exception)
            {
                ScriptHelper.SetAlert(this.Page, GetTran("001041", "指定ID无效."), "CompanyDeptManage.aspx");
                return;
            }
            if (id <= 0)
            {
                ScriptHelper.SetAlert(this.Page, GetTran("001041", "指定ID无效."), "CompanyDeptManage.aspx");
                return;
            }
            CompanyDeptModel dept = CompanyDeptBLL.GetCompanyDept(id);
            if (dept == null)
            {
                ScriptHelper.SetAlert(this.Page, GetTran("001042", "部门不存在"), "CompanyDeptManage.aspx");
                return;
            }
            ViewState["id"] = id;
            this.txtDept.Text = dept.Dept;
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.btnModify, new string[][] { new string[] { "000259", "修改" } });
    }

    protected void BtnModify_Click(object sender ,EventArgs e)
    {
        int id = (int)ViewState["id"];
        if (this.txtDept.Text.Trim() == "")
        {
            ScriptHelper.SetAlert(this.Page, GetTran("001043", "请填写部门名称"));
        }
        else if (this.txtDept.Text.Trim().Length < 3)
        {
            ScriptHelper.SetAlert((Control)sender, GetTran("001034", "部门名称必须3个字符以上!"));
        }
        else
        {
            if (CompanyDeptBLL.CheckName(this.txtDept.Text.Trim(), id)) //是否重名，重名不能添加
            {
                ScriptHelper.SetAlert(this.Page, GetTran("001036", "部门名称存在!"));
            }
            else
            {
                CompanyDeptModel comDept = new CompanyDeptModel(id);
                comDept.Dept = this.txtDept.Text.Trim();
                BLL.CommonClass.ChangeLogs cl = new BLL.CommonClass.ChangeLogs("companyDept", "id");
                cl.AddRecord(id);
                if (CompanyDeptBLL.UptCompanyDept(comDept))
                {
                    cl.ModifiedIntoLogs(BLL.CommonClass.ChangeCategory.company24, GetTran("001047", "部门:") + id, BLL.CommonClass.ENUM_USERTYPE.objecttype7);
                    ScriptHelper.SetAlert(this.Page, GetTran("001050", "修改部门成功"), "CompanyDeptManage.aspx");
                }
                else
                {
                    ScriptHelper.SetAlert(this.Page, GetTran("001052", "修改部门失败!"), "CompanyDeptManage.aspx");
                }
            }
        }
    }

}
