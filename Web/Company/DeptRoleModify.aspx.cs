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
using BLL.CommonClass;

public partial class Company_DeptRoleModify : BLL.TranslationBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SafeRightManageEdit);
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("DeptRolesManage.aspx");
            }

            int roleId = 0;
            bool b = int.TryParse(Request.QueryString["id"], out roleId);
            //验证角色编号的合法性
            if (!b)
            {
                //编号不合法则转到角色管理页面
                Response.Redirect("DeptRolesManage.aspx");
            }

            string number = Session["Company"].ToString();
            string manageId = BLL.CommonClass.CommonDataBLL.getManageID(1);
            if (number != manageId)
            {
                if (!DeptRoleBLL.CheckAllot(number, roleId))
                {
                    ScriptHelper.SetAlert((Control)sender, GetTran("000975", "不能对该角色进行操作，没有权限！"), "DeptRolesManage.aspx");
                    return;
                }
            }
            else
            {
                ManageModel model =  ManagerBLL.GetManage(number);
                if (model.RoleID == roleId)
                {
                    ScriptHelper.SetAlert((Control)sender, GetTran("001180", "不能对该系统管理角色进行任何操作."), "DeptRolesManage.aspx");
                    return;
                }
            }
            DeptRoleModel deptRole = DeptRoleBLL.GetDeptRoleByRoleID(roleId);
            if (deptRole == null)
            {
                Response.Redirect("DeptRolesManage.aspx");
            }
            ViewState["deptId"] = deptRole.DeptID;
            ViewState["roleid"] = roleId;
            this.txtRoleName.Text = deptRole.Name;  //将角色的名称加载到文本框里
            ViewState["Name"] = deptRole.Name;
            InitdllDepts();
        }
        Translations();
    }
    /// <summary>
    /// 修改按钮文字翻译
    /// </summary>
    private void Translations()
    {
        this.TranControls(this.btnUpt, new string[][] { new string[] { "000259", "修改" } });
    }
    /// <summary>
    /// 将所有部门加载到下拉框中
    /// </summary>
    protected void InitdllDepts()
    {
        IList<CompanyDeptModel> depts = CompanyDeptBLL.GetCompanyDept();
        this.ddlDepts.DataSource = depts;
        this.ddlDepts.DataTextField = "dept";
        this.ddlDepts.DataValueField = "id";
        this.ddlDepts.DataBind();
    }
    protected void ddlDepts_DataBound(object sender, EventArgs e)
    {
        this.ddlDepts.SelectedValue=ViewState["deptId"].ToString();
    }
    /// <summary>
    /// 修改角色的操作事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpt_Click(object sender, EventArgs e)
    {
        Application.Lock();
        if (ViewState["roleid"] == null)
        {
            Response.Redirect("DeptRolesManage.aspx");
        }
        if (txtRoleName.Text.Trim() == "")
        {
            ScriptHelper.SetAlert((Control)sender, GetTran("001183", "请输入角色名称!"));
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
        if (txtRoleName.Text != ViewState["Name"].ToString())
        {
            if (DeptRoleBLL.CheckDeptRoleName(this.txtRoleName.Text.Trim(), (int)ViewState["roleid"]) != null)
            {
                ScriptHelper.SetAlert((Control)sender, GetTran("001001", "角色名称已经存在!"));
                return;
            }
        }
        int roleId = (int)ViewState["roleid"];
        string number = Session["Company"].ToString();
        string manageId = BLL.CommonClass.CommonDataBLL.getManageID(1);
        if (number != manageId)
        {
            if (!DeptRoleBLL.CheckAllot(number, roleId))
            {
                ScriptHelper.SetAlert((Control)sender, GetTran("000975", "不能对该角色进行操作，没有权限！"));
                return;
            }
        }
        DeptRoleModel deptRole = new DeptRoleModel(roleId);
        deptRole.DeptID = int.Parse(this.ddlDepts.SelectedValue);
        string ids = Request.Form["qxCheckBox"]; //获取所有选中的菜单的值(pmID)，在生成的页面可以查看(后台拼接而成)，qxCheckBox是菜单的name(checkbox的name)
        string[] id = ids.Split(',');
        Hashtable htb = (Hashtable)Session["permission"];
        htb = DeptRoleBLL.GetAllPermission(Session["Company"].ToString());
        Hashtable htb2 = new Hashtable();
        int i = -1;
        if (number != manageId)
        {
            foreach (string n in id)
            {
                if (htb.Contains(int.Parse(n)))
                {
                    htb2.Add(n, "0");
                }
                else
                {
                    i = 0;
                }
            }
        }
        else
        {
            foreach (string n in id)
            {
                if (htb.Contains(int.Parse(n)))
                {
                    htb2.Add(n, "0");
                }
            }
        }
        if (i == -1)
        {
            BLL.CommonClass.ChangeLogs cl = new BLL.CommonClass.ChangeLogs("deptRole", "id");
            cl.AddRecord(roleId);

            deptRole.htbPerssion = htb2;
            deptRole.Name = this.txtRoleName.Text.Trim();
            deptRole.Allot = ((CheckBox)this.UCPermission1.FindControl("chkAllot")).Checked ? 1 : 0;
            if (DeptRoleBLL.UptDeptRole(deptRole)) //修改角色
            {
                cl.AddRecord(roleId);
                cl.ModifiedIntoLogs(BLL.CommonClass.ChangeCategory.company25, "角色:" + deptRole.Name, BLL.CommonClass.ENUM_USERTYPE.objecttype7);
                ScriptHelper.SetAlert((Control)sender, GetTran("000001", "修改成功."), "DeptRolesManage.aspx");
            }
            else
            {
                ScriptHelper.SetAlert((Control)sender, GetTran("000002","修改失败."), "DeptRolesManage.aspx");
            }
        }
        else
        {
            ScriptHelper.SetAlert((Control)sender, GetTran("001003", "异常数据"), "DeptRolesManage.aspx");
            return;
        }
        Application.UnLock();
    }
}
