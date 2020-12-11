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

public partial class Company_ManagerManage : BLL.TranslationBase
{                                                
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SafeRightManage);
        if (Session["Company"] == null)
        {
            return;
        }
        string number = Session["Company"].ToString();
        string mangeId = BLL.CommonClass.CommonDataBLL.getManageID(1);
        if (number != mangeId)
        {
            if (!DeptRoleBLL.CheckAllot(number))
            {
                ScriptHelper.SetAlert((Control)sender, GetTran("001071", "不能对管理员进行操作，没有权限！"));
                return;
            }
        }
        if (!IsPostBack)
        {
            PageSet();
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.GridView1,
                            new string[][]{	
                                new string []{"000015","操作"},
                                new string []{"001066","管理员编号"},
                                new string []{"001067","管理员名称"},
                                new string []{"000331","部门"},
                                new string []{"000333","角色"},
                                new string []{"001057","操作"},
                                new string []{"001090","加入时间"}
                            });

    }

    protected void PageSet()
    {
        string number = Session["Company"].ToString();
        string ids = DeptRoleBLL.GetDeptRoleIDs(number);
        if (ids == "")
        {
            this.GridView1.DataSource = null;
            this.GridView1.DataBind();
            this.Pager1.Visible = false;
            return;
        }
        this.Pager1.PageBind(0, 10, " Manage m inner join deptrole d on m.roleid = d.id inner join companydept c on d.deptid = c.id", " m.id,m.Number,m.Name mName,c.Dept,d.Name dName,BeginDate ", " d.id in (" + ids + ") ", " m.id ", "GridView1");
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Application.Lock();
        if (e.CommandName == "D")
        {
            Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.SafeRightManageDelete);
            int manageId = 0;
            try
            {
                manageId = int.Parse(e.CommandArgument.ToString());
            }
            catch (FormatException)
            {
                ScriptHelper.SetAlert(Page, GetTran("001074", "管理员编号不存在!"));
                return;
            }
            ManageModel manageModel = ManagerBLL.GetManage(manageId);
            if (manageModel == null)
            {
                ScriptHelper.SetAlert(Page, GetTran("001076", "该记录已经被删除"));
                return;
            }
            string manageId1 = BLL.CommonClass.CommonDataBLL.getManageID(1);
            if (manageModel.Number.Trim() == manageId1)
            {
                ScriptHelper.SetAlert(Page, GetTran("001079", "不允许删除该记录!"));
                return;
            }
            string number = Session["Company"].ToString();
            if (number != manageId1)
            {
                if (!DeptRoleBLL.CheckAllot(number, manageModel.RoleID))
                {
                    ScriptHelper.SetAlert((Control)sender, GetTran("001080", "不能对该管理员进行操作，没有权限！"));
                    return;
                }
            }
            BLL.CommonClass.ChangeLogs cl = new BLL.CommonClass.ChangeLogs("manage", "id");
            cl.AddRecord(manageId);
            if (ManagerBLL.DelManage(manageId) > 0)
            {
                cl.DeletedIntoLogs(BLL.CommonClass.ChangeCategory.company26, GetTran("001082", "管理员:") + manageModel.Number.Trim(), BLL.CommonClass.ENUM_USERTYPE.objecttype7);
                ScriptHelper.SetAlert((Control)sender, GetTran("000749", "删除成功！"));
                this.Pager1.PageBind();
            }
        }
        Application.UnLock();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg;");

            int Update = 0;
            Update = (int)Permissions.GetPermissions(Model.Other.EnumCompanyPermission.SafeRightManageUpdate);
            if (Update == 0)
            {
                ((HyperLink)e.Row.FindControl("Hyperlink1")).Visible = false;
            }
            else
            {
                ((HyperLink)e.Row.FindControl("Hyperlink1")).Visible = true;
            }

            int Delete = 0;
            Delete = (int)Permissions.GetPermissions(Model.Other.EnumCompanyPermission.SafeRightManageDelete);
            if (Delete == 0)
            {
                ((LinkButton)e.Row.FindControl("lkbDel")).Visible = false;
            }
            else
            {
                ((LinkButton)e.Row.FindControl("lkbDel")).Visible = true;
            }
            ((LinkButton)e.Row.FindControl("lkbDel")).Attributes.Add("onclick", "return confirm('" + GetTran("000947", "是否删除当前记录") + "?')");
            Translations();
        } 
    }
}
