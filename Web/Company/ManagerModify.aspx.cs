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
using System.Collections.Generic;

public partial class Company_ManagerModify : BLL.TranslationBase
{
    private int deptID=0;
    private int roleID=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SafeRightManageEdit);
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] == null || Request.QueryString["id"].ToString()=="")
            {
                ScriptHelper.SetAlert((Control)sender, GetTran("001102", "异常访问！"), "ManagerManage.aspx");
                return;
            }
            string mid = Request.QueryString["id"];
            int id = 0;
            //验证传入参数合法性

            try
            {
                id = int.Parse(mid);
            }
            catch (FormatException)
            {
                ScriptHelper.SetAlert(Page, GetTran("001102", "异常访问！"), "ManagerManage.aspx");
                return;
            }
            ViewState["id"] = id;
            ManageModel manage = ManagerBLL.GetManage(id);
            if (manage == null)
            {
                ScriptHelper.SetAlert(Page, GetTran("001104", "管理员已经不存在！"), "ManagerManage.aspx");
                return;
            }
            string manageId = BLL.CommonClass.CommonDataBLL.getManageID(1);
            if (manage.Number.ToString().Trim() == manageId)
            {
                ScriptHelper.SetAlert(Page, GetTran("001106", "不允许修改该管理员！"), "ManagerManage.aspx");
                return;
            }
            string number = Session["Company"].ToString();
            if (number != manageId)
            {
                if (!DeptRoleBLL.CheckAllot(number, manage.RoleID))
                {
                    ScriptHelper.SetAlert((Control)sender, GetTran("001080", "不能对该管理员进行操作，没有权限！"), "ManagerManage.aspx");
                    return;
                }
            }
            this.txtName.Text = manage.Name;
            this.txtNumber.Text = manage.Number;
            this.txtNumber.Enabled = false;
            this.RadioButtonList1.SelectedValue = manage.IsViewPermissions.ToString();
            this.RadioButtonList2.SelectedValue = manage.IsRecommended.ToString();
            deptID = DeptRoleBLL.GetDeptRoleByRoleID(manage.RoleID).DeptID;
            roleID = manage.RoleID;
            InitdllDepts();
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.btnUpt, new string[][] { new string[] { "000259", "修改" } });
    }

    /// <summary>
    /// 在部门信息绑定后，绑定角色信息

    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDepts_DataBound(object sender, EventArgs e)
    {
        if (ddlDepts.Items.Count == 0)
        {
            //ScriptHelper.SetAlert(Page, "请先添加部门！", "CompanyDeptAdd.aspx");
            return;
        }       
        if (deptID != 0 )
        {
            this.ddlDepts.SelectedValue = deptID.ToString();
        }
        IList<DeptRoleModel> deptRoleModels = DeptRoleBLL.GetDeptRoles(int.Parse(ddlDepts.SelectedValue), DeptRoleBLL.GetDeptRoleIDs(Session["Company"].ToString()));
        this.ddlRoles.DataSource = deptRoleModels;
        ddlRoles.DataTextField = "name";
        ddlRoles.DataValueField = "id";
        ddlRoles.DataBind();
    }    
    
    /// <summary>
    /// 在选择改变后，重新绑定角色信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDepts_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlRoles.Items.Clear();
        IList<DeptRoleModel> deptRoleModels = DeptRoleBLL.GetDeptRoles(int.Parse(ddlDepts.SelectedValue), DeptRoleBLL.GetDeptRoleIDs(Session["Company"].ToString()));
        this.ddlRoles.DataSource = deptRoleModels;
        ddlRoles.DataTextField = "name";
        ddlRoles.DataValueField = "id";
        ddlRoles.DataBind();
    }
    /// <summary>
    /// 执行修改方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnUpt_Click(object sender, EventArgs e)
    {
        int id = (int)(ViewState["id"] != null ? ViewState["id"] : 0);
        if (id <= 0)
        {
            ScriptHelper.SetAlert((Control)sender, GetTran("001102", "异常访问！"), "ManagerManage.aspx");
            return;
        }
        ManageModel manager = null;
        manager = ManagerBLL.GetManage(id);
        if (manager == null)
        {
            ScriptHelper.SetAlert((Control)sender, GetTran("001102", "异常访问！"), "ManagerManage.aspx");
            return;
        }
        //取出当前登录管理员的编号
        string manageId = BLL.CommonClass.CommonDataBLL.getManageID(1);
        if (manager.Number.ToString().Trim() == manageId)
        {
            ScriptHelper.SetAlert(Page, GetTran("001106", "不允许修改该管理员！"), "ManagerManage.aspx");
            return;
        }  
        //验证必填信息
        if (this.ddlRoles.Text.Trim() == "")
        {
            ScriptHelper.SetAlert((Control)sender, GetTran("001113", "角色信息不能为空"));
            return;
        }

        if (this.ddlDepts.Text.Trim() == "")
        {
            ScriptHelper.SetAlert((Control)sender, GetTran("001114", "部门信息不能为空"));
            return;
        }

        if (this.txtName.Text.Trim() == "")
        {
            ScriptHelper.SetAlert((Control)sender, GetTran("001116", "管理员姓名不能为空"));
            return;
        }

        if (this.txtNumber.Text.Trim() == "")
        {
            ScriptHelper.SetAlert((Control)sender, GetTran("001117", "管理员编号不能为空"));
            return;
        }
        string number = Session["Company"].ToString();
        if (number != manageId)
        {
            if (!DeptRoleBLL.CheckAllot(number, int.Parse(this.ddlRoles.SelectedValue)))
            {
                ScriptHelper.SetAlert((Control)sender, GetTran("000975", "不能对该角色进行操作，没有权限！"));
                return;
            }
        }
        //验证角色信息
        CheckDeptRole();
        //验证部门信息
        CheckCompanyDept();
        //需要验证角色名是否重复
        if (manager.Number.Trim() != txtNumber.Text.Trim())
        {
            if (!ManagerBLL.CheckNumber(txtNumber.Text.Trim()))
            {
                ScriptHelper.SetAlert((Control)sender, GetTran("001118", "当前管理员编号已经存在！"));
                return;
            }
        }
        //根据输入信息构建管理员

        manager.Number = this.txtNumber.Text.Trim();
        manager.Name = txtName.Text.Trim();
        manager.Post = this.ddlRoles.SelectedItem.Text.Trim();
        manager.Branch = this.ddlDepts.SelectedItem.Text.Trim();
        manager.RoleID = int.Parse(this.ddlRoles.SelectedValue);
        manager.Status = 1;
        manager.IsViewPermissions = int.Parse(this.RadioButtonList1.SelectedValue);
        manager.IsRecommended = int.Parse(this.RadioButtonList2.SelectedValue);
        //存储管理员信息到数据库
 
        BLL.CommonClass.ChangeLogs cl = new BLL.CommonClass.ChangeLogs("manage", "id");
        cl.AddRecord(id);
        if (ManagerBLL.UptManage(manager))
        {
            //存储成功，给出提示ChangeCategory
            cl.ModifiedIntoLogs(BLL.CommonClass.ChangeCategory.company26, GetTran("001082", "管理员:") + manager.Name, BLL.CommonClass.ENUM_USERTYPE.objecttype7);
            ScriptHelper.SetAlert((Control)sender, GetTran("001119", "修改管理员成功！"), "ManagerManage.aspx");
        }
        else
        {
            //存储失败，给出提示

            ScriptHelper.SetAlert((Control)sender, GetTran("001121", "修改管理员失败！"));
        }
    }

    /// <summary>
    /// 验证部门信息
    /// </summary>
    private void CheckCompanyDept()
    {
        if (ddlDepts.Items.Count == 0)
        {
            ScriptHelper.SetAlert(Page, GetTran("001122", "部门不存在!"));
            return;
        }
        IList<CompanyDeptModel> depts = CompanyDeptBLL.GetCompanyDept();
        if (depts == null)
        {
            ScriptHelper.SetAlert(Page, GetTran("001122", "部门不存在!"));
            return;
        }
        bool isHave = false;
        foreach (CompanyDeptModel dept in depts)
        {
            if (dept.Id.ToString().Trim().Equals(this.ddlDepts.SelectedValue))
            {
                isHave = true;
                break;
            }
        }
        if (!isHave)
        {
            ScriptHelper.SetAlert(Page, GetTran("001122", "部门不存在!"));
            return;
        }
    }

    /// <summary>
    /// 检查角色信息

    /// </summary>
    private void CheckDeptRole()
    {
        if (ddlRoles.Items.Count == 0)
        {
            ScriptHelper.SetAlert(Page, GetTran("001123", "角色不存在!"));
            return;
        }
        
        IList<DeptRoleModel> deptRoles = DeptRoleBLL.GetDeptRoles(int.Parse(this.ddlDepts.SelectedValue));
        if (deptRoles ==null)
        {
            ScriptHelper.SetAlert(Page, GetTran("001123", "角色不存在!"));
            return;
        }
        bool isHave =false;
        foreach (DeptRoleModel deptRole in deptRoles)
        {
            if (deptRole.Id.ToString().Trim().Equals(this.ddlRoles.SelectedValue))
            {
                isHave = true;
                break;
            }
        }
        if (!isHave)
        {
            ScriptHelper.SetAlert(Page, GetTran("001123", "角色不存在!"));
            return;
        }
    }


    /// <summary>
    /// 验证字符串是否为空提示方法

    /// </summary>
    /// <param name="sender">载体控件</param>
    /// <param name="msg">提示内容</param>
    /// <param name="value">验证字符串</param>
    private void CheckNull(object sender, string msg, string value)
    {
        if (value == "")
        {
            ScriptHelper.SetAlert((Control)sender, msg);
            return;
        }
    }

    /// <summary>
    /// 绑定部门下拉列表框

    /// </summary>
    protected void InitdllDepts()
    {
        IList<CompanyDeptModel> depts = CompanyDeptBLL.GetCompanyDept(DeptRoleBLL.GetDeptRoleIDs(Session["Company"].ToString()));
        this.ddlDepts.DataSource = depts;
        this.ddlDepts.DataTextField = "dept";
        this.ddlDepts.DataValueField = "id";
        this.ddlDepts.DataBind();
    }

    protected void ddlRoles_DataBound(object sender, EventArgs e)
    {
        if (ddlRoles.Items.Count == 0)
        {

        }
        if(roleID!=0)
        this.ddlRoles.SelectedValue = roleID.ToString();
    }
}
