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
using BLL.CommonClass;
using Model.Other;
using BLL.other.Company;

public partial class Company_DeptRolesManage : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SafeRightManage);

        if (!IsPostBack)
        {
            PageSet();
        }
        Translations();
    }
    /// <summary>
    /// GridView列翻译
    /// </summary>
    private void Translations()
    {
        this.TranControls(this.gvDeptRoless,
                            new string[][]{	
                                new string []{"000015","操作"},
                                new string []{"000963","角色名"},
                                new string []{"000964","部门名"},
                                new string []{"000966","直属角色"},
                                new string []{"000967","创建时间"}
                            });

    }
    /// <summary>
    /// 加载公司角色数据
    /// </summary>
    protected void PageSet()
    {
        string number = Session["Company"].ToString();
       
        //获取当前登录管理员可以修改的所有角色编号
        string ids = DeptRoleBLL.GetDeptRoleIDs(number); 
        if (ids == "")
        {
            this.gvDeptRoless.DataSource = null;
            this.gvDeptRoless.DataBind();
            Pager1.Visible = false;
            return;
        }                              
        this.Pager1.PageBind(0, 10, "DeptRole d inner join CompanyDept c on d.deptid=c.id ", "d.Name,c.dept,Allot,d.id,d.addDate,(select Name from deptRole where id=d.ParentId) as RoleName ", "  d.id in (" + ids + ")", " d.id ", "gvDeptRoless");

    }
    protected void gvDeptRoless_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Application.Lock();
        if (e.CommandName == "D")
        {
            Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SafeRightManageDelete);
            string number = Session["Company"].ToString();
            int roleId = int.Parse(e.CommandArgument.ToString());
            string manageId = BLL.CommonClass.CommonDataBLL.getManageID(1);//返回当前登录管理员的编号，主要为了防止不超时
            if (number != manageId)
            {
                if (!DeptRoleBLL.CheckAllot(number, roleId))
                {
                    ScriptHelper.SetAlert((Control)sender, GetTran("000975", "不能对该角色进行操作，没有权限！"));
                    return;
                }
            }
            if (DeptRoleBLL.GetCountByRoleId(roleId) > 0)
            {
                ScriptHelper.SetAlert((Control)sender, GetTran("000977", "该角色下还存在管理员，请先删除该角色下的管理员！"));
                return;
            }
            else
            {
                BLL.CommonClass.ChangeLogs cl = new BLL.CommonClass.ChangeLogs("deptRole", "id");
                cl.AddRecord(roleId);
                string msg = DeptRoleBLL.DelDeptRole(HttpContext.Current, int.Parse(e.CommandArgument.ToString()));
                if (msg == "删除角色成功.")
                {
                    ScriptHelper.SetAlert((Control)sender, msg, "DeptRolesManage.aspx");
                    cl.DeletedIntoLogs(BLL.CommonClass.ChangeCategory.company25, GetTran("000981", "角色:") + int.Parse(e.CommandArgument.ToString()), BLL.CommonClass.ENUM_USERTYPE.objecttype7);
                   
                }
                ScriptHelper.SetAlert((Control)sender, msg);
                PageSet();
            }
        }
        Application.UnLock();
    }

    protected void gvDeptRoless_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CheckBox chk = (CheckBox)e.Row.FindControl("chk");
        if (chk != null)
            chk.Enabled = false;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg;");

            int Update = 0;
            Update = (int)Permissions.GetPermissions(Model.Other.EnumCompanyPermission.SafeUpdateJs);
            if (Update == 0)
            {
                ((HyperLink)e.Row.FindControl("Hyperlink1")).Visible = false;
            }
            else
            {
                ((HyperLink)e.Row.FindControl("Hyperlink1")).Visible = true;
            }

            int Delete = 0;
            Delete = (int)Permissions.GetPermissions(Model.Other.EnumCompanyPermission.SafeDeleteJs);
            if (Delete == 0)
            {
                ((LinkButton)e.Row.FindControl("lbtnDel")).Visible = false;
            }
            else
            {
                ((LinkButton)e.Row.FindControl("lbtnDel")).Visible = true;
            }
            ((LinkButton)e.Row.FindControl("lbtnDel")).Attributes.Add("onclick", "return confirm('" + GetTran("000947", "是否删除当前记录") + "?')");

            string manageId = BLL.CommonClass.CommonDataBLL.getManageID(1);
            if (Session["Company"].ToString() != manageId)
            {
                if (!DeptRoleBLL.CheckAllot(Session["Company"].ToString()))
                {
                    ((HyperLink)e.Row.FindControl("Hyperlink1")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lbtnDel")).Visible = false;
                }
            }

            Translations();
        }
    }
}
