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
using System.Collections.Generic;
using BLL.other.Company;
using System.Text;
using Model.Other;
using BLL.CommonClass;

public partial class Company_AddRoleDept : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SafeRightManageEdit);
        if(!IsPostBack)
        {
            string number = Session["Company"].ToString();
            string manageId = BLL.CommonClass.CommonDataBLL.getManageID(1);
            if (number != manageId)
            {
                if (!DeptRoleBLL.CheckAllot(number))
                {
                    Response.Write(Transforms.ReturnAlert(GetTran("000997", "当前登录用户没有给下级分配权限的权限!")));
                    HttpContext.Current.Response.End();
                }
            }
            InitdllDepts();
        }
        Translations();
    }
    /// <summary>
    /// 添加按钮文字翻译
    /// </summary>
    private void Translations()
    {
        this.TranControls(this.btnAdd, new string[][] { new string[] { "006639", "添加" } });
    }
    /// <summary>
    /// 加载公司所有的部门到下拉框里
    /// </summary>
    protected void InitdllDepts()
    {
        IList<CompanyDeptModel> depts = CompanyDeptBLL.GetCompanyDept();
        this.ddlDepts.DataSource = depts;
        this.ddlDepts.DataTextField = "dept";
        this.ddlDepts.DataValueField = "id";
        this.ddlDepts.DataBind();
    }
    /// <summary>
    /// 添加角色的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string number = Session["Company"].ToString();
        string manageId = BLL.CommonClass.CommonDataBLL.getManageID(1);
        if (number != manageId)
        {
            if (!DeptRoleBLL.CheckAllot(number))
            {
                ScriptHelper.SetAlert((Control)sender, GetTran("000997", "当前登录用户没有给下级分配权限的权限!"));
                return;
            }
        }
        if (txtRoleName.Text.Trim() == "")
        {
            ScriptHelper.SetAlert((Control)sender, GetTran("000998", "请输入角色名称!"));
            return;
        }
        else
        {
            if (this.txtRoleName.Text.Trim().Length < 3)
            {
                ScriptHelper.SetAlert((Control)sender, GetTran("000999", "角色名称必须3个字符以上!"));
                return;
            }
        }
        if (DeptRoleBLL.CheckDeptRoleName(this.txtRoleName.Text,0) != null)
        {
            ScriptHelper.SetAlert((Control)sender, GetTran("001001", "角色名称已经存在!"));
            return;
        }
        string ids = Request.Form["qxCheckBox"];
        if (ids == null||ids=="")
        {
            ScriptHelper.SetAlert((Control)sender, GetTran("001002", "请选择要分配权限!"));
            return;
        }
        DeptRoleModel deptRole = new DeptRoleModel();
        string[] id = ids.Split(',');
        Hashtable htb = (Hashtable)Session["permission"];
        htb = DeptRoleBLL.GetAllPermission(Session["Company"].ToString());
        Hashtable htb2 = new Hashtable();
        int i = -1;
        foreach (string n in id)
        {
            if (htb.Contains(int.Parse(n)))
            {
                htb2.Add(n, "0");
            }
            else
            {
                i = 0;
                break;
            }
        }
        manageId = BLL.CommonClass.CommonDataBLL.getManageID(1);
        if (number == manageId)
        {
            i = -1;
        }
        if (i == -1)
        {
            deptRole.htbPerssion = htb2;
            deptRole.Name = this.txtRoleName.Text.Trim();
            ManageModel ma = ManagerBLL.GetManage(Session["Company"].ToString());
            deptRole.PermissionManID = ma.ID;
            deptRole.DeptID = int.Parse(this.ddlDepts.SelectedValue);
            deptRole.Adddate = DateTime.Now;
            deptRole.ParentId = ma.RoleID;
            deptRole.Allot = ((CheckBox)this.UCPermission1.FindControl("chkAllot")).Checked?1:0;
            if (DeptRoleBLL.AddDeptRole(deptRole))  //添加角色
            {
                ScriptHelper.SetAlert((Control)sender, GetTran("000006", "添加成功."), "DeptRolesManage.aspx");
            }
            else
            {
                ScriptHelper.SetAlert((Control)sender, GetTran("000007", "添加失败."), "DeptRolesManage.aspx");
            }
        }
        else
        {
            ScriptHelper.SetAlert((Control)sender, GetTran("001003", "异常数据"), "DeptRolesManage.aspx");
            return;
        }
    }
}
