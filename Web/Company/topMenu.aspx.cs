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

public partial class Company_topMenu : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //-----------------------------------------------------------------------------
        //获取登入者信息并显示到滚动块去  
        string  number=Session["Company"].ToString();
        if (!IsPostBack)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
            this.ltlId.Text = "<font color=''>" + GetCompanyId() + "</font>";
            this.ltlNme.Text = "<font color=''>" + MenuBLL.GetManageName(GetCompanyId()) + "</font>";
       
            if (Convert.ToString(Session["UserLastLoginDate"]) != "" || Session["UserLastLoginDate"] != null)
            {
                this.ltlTime.Text = "<font color=''>" + Convert.ToDateTime(Session["UserLastLoginDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "</font>";
            }
            else
            {
                this.ltlTime.Text = "<font color=''>" + MenuBLL.GetLoginTime(GetCompanyId(), 1).ToString("yyyy-MM-dd HH:mm:ss") + "</font>";
            }

            #region 公司系统权限
            
            int isShow = 1;
            object o_isshow = DAL.DBHelper.ExecuteScalar("select top 1 isnull(isShow,1) from MenuManage");
            if (o_isshow != null)
            {
                isShow = Convert.ToInt32(o_isshow);
            }
            string field = System.Web.HttpContext.Current.Session["LanguageCode"].ToString();
//            if (isShow == 0)
//            {
//                if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar(@"select count(0) from menuleft m left outer join t_translation t on m.id=t.primarykey join (select menuid from manage g,permission_Name n,ManagerPermission p where p.managerid=g.roleid and p.permissionid=n.permission_id and g.number='" + number + @"') H on m.id=H.menuID 
//where m.MenuName!='预设短信' and m.ParentID in(7,8,9) and m.isfold=1 and t.tableName='menuleft'")) <= 0)
//                {
//                    A2.Visible = false;
//                }
//                if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar(@"select count(0) from menuleft m left outer join t_translation t on m.id=t.primarykey join (select menuid from manage g,permission_Name n,ManagerPermission p where p.managerid=g.roleid and p.permissionid=n.permission_id and g.number='" + number + @"') H on m.id=H.menuID 
//where m.MenuName!='预设短信' and m.ParentID in(45,46,47) and m.isfold=1 and t.tableName='menuleft'")) <= 0)
//                {
//                    A3.Visible = false;
//                }
//                if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar(@"select count(0) from menuleft m left outer join t_translation t on m.id=t.primarykey join (select menuid from manage g,permission_Name n,ManagerPermission p where p.managerid=g.roleid and p.permissionid=n.permission_id and g.number='" + number + @"') H on m.id=H.menuID 
//where m.MenuName!='预设短信' and m.ParentID in(63,65) and m.isfold=1 and t.tableName='menuleft'")) <= 0)
//                {
//                    A4.Visible = false;
//                }
//                if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar(@"select count(0) from menuleft m left outer join t_translation t on m.id=t.primarykey join (select menuid from manage g,permission_Name n,ManagerPermission p where p.managerid=g.roleid and p.permissionid=n.permission_id and g.number='" + number + @"') H on m.id=H.menuID 
//where m.MenuName!='预设短信' and m.ParentID in(74,75,76,77,185) and m.isfold=1 and t.tableName='menuleft'")) <= 0)
//                {
//                    A5.Visible = false;
//                }
//                if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar(@"select count(0) from menuleft m left outer join t_translation t on m.id=t.primarykey join (select menuid from manage g,permission_Name n,ManagerPermission p where p.managerid=g.roleid and p.permissionid=n.permission_id and g.number='" + number + @"') H on m.id=H.menuID 
//where m.MenuName!='预设短信' and m.ParentID in(98,99,100) and m.isfold=1 and t.tableName='menuleft'")) <= 0)
//                {
//                    A6.Visible = false;
//                }
//                if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar(@"select count(0) from menuleft m left outer join t_translation t on m.id=t.primarykey join (select menuid from manage g,permission_Name n,ManagerPermission p where p.managerid=g.roleid and p.permissionid=n.permission_id and g.number='" + number + @"') H on m.id=H.menuID 
//where m.MenuName!='预设短信' and m.ParentID in(112,113,114) and m.isfold=1 and t.tableName='menuleft'")) <= 0)
//                {
//                    A7.Visible = false;
//                }
          //  }
 //加载大菜单
        string html = "";

        DataTable dtt = DAL.DBHelper.ExecuteDataTable(@" select  ml.id, ml.MenuName,MenuFile,ml.ImgFile     from ManagerPermission mp left join Manage  m  on mp.ManagerID=m.RoleID left join permission_Name pn on mp.PermissionID=pn.permission_ID
 left join menuLeft ml on pn.menuID=ml.ID left join T_translation t on ml.ID=t.primarykey
  where m.Number='" + number + "'  and pn.parentid=-100   and  t.tableName='menuleft' and isfold=1 order by ml.id ");
            int i=1;
            int fsid = 1;
            if (dtt != null && dtt.Rows.Count > 0)
            {   fsid=Convert.ToInt32( dtt.Rows[0]["id"]);
                foreach (DataRow item in dtt.Rows)
                {
                    html += @"<li>
                    <a class='selected' href='" + item["MenuFile"] + @"' target='leftmenu' id='A" + i + "' runat='server' onclick='change(" + i + @");'>
                        <img src='" + item["ImgFile"] + @"' /><h2>" + item["MenuName"] + @"</h2>
                    </a>
                </li>";
                    i++;
                }
            }


            menu.InnerHtml = html + "<script>loadmenu("+fsid+");</script>";
            #endregion
        }
        //-----------------------------------------------------------------
        if (StoreID.Value == "")
        {
            StoreID.Value = number;
        }
        else
        {
            if (StoreID.Value != number)
            {
                Session["Company"] = null;
                Response.Redirect("../Logout.aspx", true);
            }
        }


       




    }
    //------------------------------------------------------------
    private string GetCompanyId()
    {
        if (Session["Company"] != null)
        {
            return Session["Company"].ToString();
        }
        else
        {
            return "";
        }
    }
    //------------------------------------------------------------------
}