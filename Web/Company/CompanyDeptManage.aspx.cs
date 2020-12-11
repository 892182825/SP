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

public partial class Company_CompanyDeptManage : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);//设置缓存到期时间
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SafeRightDpetManage);
        if (!IsPostBack)
        {
            PageSet();
        }
        Translations();
    }
    /// <summary>
    /// 翻译
    /// </summary>
    private void Translations()
    {
        this.TranControls(this.gvCompanyDepts,
                            new string[][]{	
                                new string []{"000015","操作"},
                                new string []{"000964","部门名"}
                            });

    }
    /// <summary>
    /// 加载部门
    /// </summary>
    protected void PageSet()
    {
        this.PagerCompanyDept.PageBind(0, 10, "CompanyDept", "id, dept", "1=1", "id", "gvCompanyDepts");
    }

    protected void gvCompanyDepts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Application.Lock();
        if (e.CommandName == "D")
        {
            Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SafeRightDpetManage);
            if (CompanyDeptBLL.GetDeptRoleCount(int.Parse(e.CommandArgument.ToString())))
            {

                BLL.CommonClass.ChangeLogs cl = new BLL.CommonClass.ChangeLogs("companydept", "id");
                cl.AddRecord(int.Parse(e.CommandArgument.ToString()));

                //部门是否存在
                if (!CompanyDeptBLL.IsHaaveCompanyDept(int.Parse(e.CommandArgument.ToString())))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "提示", "alert('部门已经被删除！');", true);
                    return;
                }

                if (CompanyDeptBLL.DelCompanyDept(int.Parse(e.CommandArgument.ToString())))
                {
                    cl.DeletedIntoLogs(BLL.CommonClass.ChangeCategory.company24, "部门:" + int.Parse(e.CommandArgument.ToString()), BLL.CommonClass.ENUM_USERTYPE.objecttype7);
                    ScriptHelper.SetAlert(this.Page, "删除部门成功！");
                    PageSet();
                }
                else
                {
                    ScriptHelper.SetAlert(this.Page, "该部门已经删除，无法重复执行！");
                    PageSet();
                }
            }
            else
            {
                ScriptHelper.SetAlert(this.Page, "该部门下已经安排有角色，请先删除角色再删除部门！");
            }
        }
        Application.UnLock();
    }
    protected void gvCompanyDepts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg;");

            int Update = 0;
            Update = (int)Permissions.GetPermissions(Model.Other.EnumCompanyPermission.SafeUpdateDpet);
            if (Update == 0)
            {
                ((HyperLink)e.Row.FindControl("Hyperlink1")).Visible = false;
            }
            else
            {
                ((HyperLink)e.Row.FindControl("Hyperlink1")).Visible = true;
            }

            int Delete = 0;
            Delete = (int)Permissions.GetPermissions(Model.Other.EnumCompanyPermission.SafeDeleteDpet);
            if (Delete == 0)
            {
                ((LinkButton)e.Row.FindControl("lbtnDel")).Visible = false;
            }
            else
            {
                ((LinkButton)e.Row.FindControl("lbtnDel")).Visible = true;
            }
            ((LinkButton)e.Row.FindControl("lbtnDel")).Attributes.Add("onclick", "return confirm('" + GetTran("000947", "是否删除当前记录") + "?')");
            Translations(); 
        }
    }
}
