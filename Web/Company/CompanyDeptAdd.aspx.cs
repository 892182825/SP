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
using BLL.other.Company;

public partial class Company_AddCompanyDept : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SafeRightDpetManage);
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.btnAdd, new string[][] { new string[] { "006639", "添加" } });
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (this.txtName.Text.Trim() == "")
        {
            ScriptHelper.SetAlert((Control)sender, GetTran("001033", "请填写部门名称!"));
        }
        else if (this.txtName.Text.Trim().Length < 3)
        {
            ScriptHelper.SetAlert((Control)sender, GetTran("001034", "部门名称必须3个字符以上!"));
        }
        else
        {
            if (CompanyDeptBLL.CheckName(this.txtName.Text.Trim(), -1))
            {
                ScriptHelper.SetAlert((Control)sender, GetTran("001036", "部门名称存在!"));
            }
            else
            {
                CompanyDeptModel comDept = new CompanyDeptModel();
                comDept.Dept = this.txtName.Text.Trim();
                comDept.Adddate = DateTime.Now;
                if (CompanyDeptBLL.AddCompanyDept(comDept))
                {
                    ScriptHelper.SetAlert((Control)sender, GetTran("001037", "添加部门成功!"), "CompanyDeptManage.aspx");
                }
                else
                {
                    ScriptHelper.SetAlert((Control)sender, GetTran("001040", "添加部门失败!"), "CompanyDeptManage.aspx");
                }
            }
        }
    }
}
