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
using Model.Other;

public partial class Company_ManagerAdd : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SafeRightManageEdit);
        if (!IsPostBack)
        {
            string number = Session["Company"].ToString();
            string manageId = BLL.CommonClass.CommonDataBLL.getManageID(1);
            if (number != manageId)
            {
                if (DeptRoleBLL.GetDeptRoleIDs(number).Trim()=="")
                {
                    ScriptHelper.SetAlert(Page, GetTran("001158", "您还没有创建属于您的角色."), "ManagerManage.aspx");
                    return;
                }
            }
            InitdllDepts();
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.RadioButtonList1,
                new string[][]{
                    new string []{"000233","是"},
                    new string []{"000235","否"}
                });
        this.TranControls(this.RadioButtonList2,
                new string[][]{
                    new string []{"000233","是"},
                    new string []{"000235","否"}
                });
        this.TranControls(this.btnAdd, new string[][] { new string[] { "006639", "添加" } });
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        //验证必填信息
        if (this.ddlRoles.Text.Trim() == "")
        {
            ScriptHelper.SetAlert((Control)sender, GetTran("001113", "角色信息不能为空"));
        }
        else
        {
            if (this.ddlDepts.Text.Trim() == "")
            {
                ScriptHelper.SetAlert((Control)sender, GetTran("001114", "部门信息不能为空"));
            }
            else
            {
                if (this.txtName.Text.Trim() == "")
                {
                    ScriptHelper.SetAlert((Control)sender, GetTran("001116", "管理员姓名不能为空"));
                }
                else
                {
                    if (this.txtNumber.Text.Trim() == "")
                    {
                        ScriptHelper.SetAlert((Control)sender, GetTran("001117", "管理员编号不能为空"));
                    }
                    else if (this.txtNumber.Text.Trim().Length < 6)
                    {
                        ScriptHelper.SetAlert((Control)sender, GetTran("001163", "管理员编号必须6个字符以上"));
                    }
                    else
                    {
                        //取出当前登录管理员的编号
                        string number = Session["Company"].ToString();
                        string manageId = BLL.CommonClass.CommonDataBLL.getManageID(1);
                        if (number != manageId)
                        {
                            if (!DeptRoleBLL.CheckAllot(number, int.Parse(this.ddlRoles.SelectedValue)))
                            {
                                ScriptHelper.SetAlert((Control)sender, GetTran("000975", "不能对该角色进行操作，没有权限！"));
                                return;
                            }
                        }
                        //根据输入信息构建管理员
                        ManageModel manager = new ManageModel();
                        if (!ManagerBLL.CheckNumber(txtNumber.Text.Trim()))
                        {
                            ScriptHelper.SetAlert((Control)sender, GetTran("001166", "该编号已经存在"));
                        }
                        else
                        {
                            manager.Number = this.txtNumber.Text.Trim();
                            manager.Name = txtName.Text.Trim();
                            manager.PermissionMan = number.Trim();
                            manager.Post = this.ddlRoles.SelectedItem.Text.Trim();
                            manager.Branch = this.ddlDepts.SelectedItem.Text.Trim();
                            manager.RoleID = int.Parse(this.ddlRoles.SelectedValue);
                            manager.BeginDate = DateTime.UtcNow;
                            manager.Status = 1;
                            manager.LastLoginDate = DateTime.UtcNow;
                            manager.IsViewPermissions = int.Parse(this.RadioButtonList1.SelectedValue);
                            manager.IsRecommended = int.Parse(this.RadioButtonList2.SelectedValue);
                            //获取管理员的默认密码
                            string password = ManagerBLL.GetPassword(this.txtNumber.Text.Trim());
                            //加密存储管理员的密码
                            manager.LoginPass = Encryption.Encryption.GetEncryptionPwd(this.txtNumber.Text.Trim(), this.txtNumber.Text.Trim());
                            //存储管理员信息到数据库
                            if (ManagerBLL.AddManage(manager, number) > 0)
                            {
                                //存储成功，给出提示
                                ScriptHelper.SetAlert((Control)sender, GetTran("001167", "添加管理员成功！"), "ManagerManage.aspx");
                            }
                            else
                            {
                                //存储失败，给出提示
                                ScriptHelper.SetAlert((Control)sender, GetTran("001169", "添加管理员失败！"));
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 验证字符串是否为空提示方法
    /// </summary>
    /// <param name="sender">载体控件</param>
    /// <param name="msg">提示内容</param>
    /// <param name="value">验证字符串</param>
    private bool CheckNull(object sender,string msg,string value)
    {
        if (value == "")
        {
            ScriptHelper.SetAlert((Control)sender, msg);
        }
        return true;
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
    /// <summary>
    /// 在部门信息绑定后，绑定角色信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDepts_DataBound(object sender, EventArgs e)
    {
        if (ddlDepts.SelectedValue =="")
        {
            return;
        }
        IList<DeptRoleModel> deptRoleModels = DeptRoleBLL.GetDeptRoles(int.Parse(ddlDepts.SelectedValue),DeptRoleBLL.GetDeptRoleIDs(Session["Company"].ToString()));
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
        IList<DeptRoleModel> deptRoleModels = DeptRoleBLL.GetDeptRoles(int.Parse(ddlDepts.SelectedValue),DeptRoleBLL.GetDeptRoleIDs(Session["Company"].ToString()));
        this.ddlRoles.DataSource = deptRoleModels;
        ddlRoles.DataTextField = "name";
        ddlRoles.DataValueField = "id";
        ddlRoles.DataBind();
        if (ddlRoles.Items.Count == 0)
        {
            //ScriptHelper.SetAlert(Page, "请先添加角色！", "DeptRoleAdd.aspx");
            //return;
        }
    }
}
