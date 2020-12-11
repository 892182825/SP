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
using BLL.other;
using Model;
using BLL.other.Company;

public partial class UserControl_Permission : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //调用初始化权限树方法
            InitPermissionTree();
        }
    }

    private void InitPermissionTree()
    {
        string mid = HttpContext.Current.Request.QueryString["id"];
        //调用角色树图生成方法生成树图
        string number = Session["Company"].ToString();
        Hashtable htb = DeptRoleBLL.GetAllPermission(Session["Company"].ToString()); //获取指定管理员的所有权限
        this.DivPermission.InnerHtml = (new DeptRoleBLL()).ResetAllPermission(ManagerBLL.GetManage(number).RoleID, number, htb); //获取权限菜单，并生成权限树
        
        //如果mid不为空则判断为mid的角色是否可以被当前用户登录用户编辑
        if (mid!=null&&mid != "")
        {
            int id = 0;
            //检查传入参数的合法性
            try
            {
                id = int.Parse(mid);
            }
            catch (FormatException)
            {
                Response.End();
            }
            DeptRoleModel deptRoleModel = DeptRoleBLL.GetDeptRoleByRoleID(id);
            if (deptRoleModel == null)
            {
                Response.Write("<script>alert('" + BLL.Translation.Translate("004200", "当前角色已经不存在,不允许操作") + ".');window.location='DeptRolesManage.aspx'</script>");
                Response.End();
            }
            ManagerBLL manageBLL = new ManagerBLL();
            this.chkAllot.Checked = (deptRoleModel.Allot==1);
            
            //查询指定角色权限信息
            htb = DeptRoleBLL.GetAllPermission(deptRoleModel.Id);
            IDictionaryEnumerator idiction = htb.GetEnumerator();
            string str_html = "";
            while (idiction.MoveNext())
            {
                str_html += "<script>getpermission('" + idiction.Key + "');</script>";
            }
            this.DivSetPer.InnerHtml = str_html;
        }
    }
}

