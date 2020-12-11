using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.other;
using System.Collections;
using BLL.CommonClass;
using Model;
//Add Namespace
using System.Web;
using Model.Other;

/*
 * 创建时间：   2009-08-31
 * 修改者：     汪  华
 * 文件名：     PermissionsBLL
 * 功能：       判断权限 
 * 备注：       该类是从源代码中复制来的，做了部分改动，可能有点小问题
 */

public class Permissions
{
    public Permissions()
    {

    }
    public static string redirUrl = "index.aspx";
    public static string baseUrl = HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.RawUrl, "") + HttpContext.Current.Request.ApplicationPath;
    /// <summary>
    /// 检查当前店是否有资格登陆
    /// </summary>
    public static void CheckStorePermission()
    {
        if (HttpContext.Current.Application["jinzhi"].ToString().IndexOf("D") != -1)
        {
            HttpContext.Current.Response.Write("<div align=center style='font-size=12px;' >" + System.Configuration.ConfigurationSettings.AppSettings["jsError"].ToString());
            HttpContext.Current.Response.End();
        }
        if (HttpContext.Current.Session["Store"] == null)
        {
            //2009.09.07.10：56注释
            HttpContext.Current.Response.Redirect(baseUrl + "/Store/index.aspx?login=" + System.Configuration.ConfigurationSettings.AppSettings["lgError"].ToString(), true);
        }
    }

    /// <summary>
    /// 检查当前店铺的登陆状态
    /// </summary>
    /// <returns>返回登陆状态类型的枚举值</returns>
    public static int GetStorePermission()
    {
        if (HttpContext.Current.Application["jinzhi"].ToString().IndexOf("D") != -1)
        {
            return (int)EnumPermissions.RefuseD;
        }
        if (HttpContext.Current.Session["Store"] == null)
        {
            return (int)EnumPermissions.EmptySessionofD;
        }
        return (int)EnumPermissions.ALLOW;
    }

    /// <summary>
    /// 检查当前店铺的登陆状态
    /// </summary>
    /// <returns>返回登陆状态类型的枚举</returns>
    public static EnumPermissions GetStorePermissionEnum()
    {
        if (HttpContext.Current.Application["jinzhi"].ToString().IndexOf("D") != -1)
        {
            return EnumPermissions.RefuseD;
        }
        if (HttpContext.Current.Session["Store"] == null)
        {
            return EnumPermissions.EmptySessionofD;
        }
        return EnumPermissions.ALLOW;
    }


    /// <summary>
    /// 检查当前会员是否有资格登陆
    /// </summary>		
    public static void CheckMemberPermission()
    {
        if (HttpContext.Current.Application["jinzhi"].ToString().IndexOf("H") != -1)
        {
            HttpContext.Current.Response.Write("<div align=center style='font-size=12px;' >" + System.Configuration.ConfigurationSettings.AppSettings["jsError"].ToString());
            HttpContext.Current.Response.End();
        }
        if (HttpContext.Current.Session["Member"] == null)
        {
            HttpContext.Current.Response.Redirect(baseUrl + "/Member/index.aspx?login=" + System.Configuration.ConfigurationSettings.AppSettings["lgError"].ToString(), true);
        }
    }


    /// <summary>
    /// 检查当前会员的登陆状态
    /// </summary>
    /// <returns>返回登陆状态类型的枚举值</returns>
    public static int GetMemberPermission()
    {
        if (HttpContext.Current.Application["jinzhi"].ToString().IndexOf("H") != -1)
        {
            return (int)EnumPermissions.RefuseH;
        }
        if (HttpContext.Current.Session["bh"] == null)
        {
            return (int)EnumPermissions.EmptySessionofH;
        }
        return (int)EnumPermissions.ALLOW;
    }

    /// <summary>
    /// 检查当前会员的登陆状态
    /// </summary>
    /// <returns>返回登陆状态类型的枚举</returns>
    public static EnumPermissions GetMemberPermissionEnum()
    {
        if (HttpContext.Current.Application["jinzhi"].ToString().IndexOf("H") != -1)
        {

            return EnumPermissions.RefuseH;
        }
        if (HttpContext.Current.Session["bh"] == null)
        {
            return EnumPermissions.EmptySessionofH;
        }
        return EnumPermissions.ALLOW;
    }


    /// <summary>
    /// 检查管理员是否有指定的权限，默认当禁止访问时限制访问
    /// </summary>
    /// <param name="permissionId">权限 id</param>
    public static void CheckManagePermission(int permissionId)
    {
        CheckManagePermission(permissionId, true);
    }

    /// <summary>
    /// ds2012
    /// 检查管理员是否有指定的权限，默认当禁止访问时限制访问
    /// </summary>
    /// <param name="enum_per">权限的枚举值</param>
    public static void CheckManagePermission(EnumCompanyPermission enum_per)
    {
        CheckManagePermission(enum_per, true);
    }

    /// <summary>
    /// 检查管理员是否有指定的权限, 并得到此权限的类型:普通/高级
    /// </summary>
    /// <param name="permissionId">权限 id</param>
    /// <param name="check">true:禁止访问时不可访问 false:没有限制</param>
    public static void CheckManagePermission(int permissionId, bool check)
    {
        if (check == true)
        {
            if (HttpContext.Current.Application["jinzhi"].ToString().IndexOf("G") != -1)
            {
                HttpContext.Current.Response.Write("<div align=center style='font-size=12px;' >" + System.Configuration.ConfigurationSettings.AppSettings["jsError"].ToString());
                HttpContext.Current.Response.End();
            }
        }
        if (HttpContext.Current.Session["permission"] != null)
        {
            Hashtable table = (Hashtable)HttpContext.Current.Session["permission"];
            if (!table.Contains(permissionId))
            {
                HttpContext.Current.Response.Write(Transforms.ReturnAlert("对不起，您没有权限！"));
                HttpContext.Current.Response.End();
            }
            else
            {
                HttpContext.Current.Session["state"] = table[permissionId];
            }
        }
        else
        {
            HttpContext.Current.Response.Redirect(baseUrl + "/Company/index.aspx?login=" + System.Configuration.ConfigurationSettings.AppSettings["lgError"].ToString());
            HttpContext.Current.Response.End();
        }
    }

    /// <summary>
    /// ds2012
    /// 检查管理员是否有指定的权限, 并得到此权限的类型:普通/高级
    /// </summary>
    /// <param name="enum_per">权限的枚举值</param>
    /// <param name="check">true:禁止访问时不可访问 false:没有限制</param>
    public static void CheckManagePermission(EnumCompanyPermission enum_per, bool check)
    {
        string manageId = BLL.CommonClass.CommonDataBLL.getManageID(1);
        if (HttpContext.Current.Session["Company"] != null && HttpContext.Current.Session["Company"].ToString() == manageId)
        {
            return;
        }
        if (check == true)
        {
            if (HttpContext.Current.Application["jinzhi"].ToString().IndexOf("G") != -1)
            {
                HttpContext.Current.Response.Write("<div align=center style='font-size=12px;' >" + System.Configuration.ConfigurationSettings.AppSettings["jsError"].ToString());
                HttpContext.Current.Response.End();
            }
        }
        if (HttpContext.Current.Session["permission"] != null)
        {
            Hashtable table = (Hashtable)HttpContext.Current.Session["permission"];
            if (!table.Contains((int)enum_per))
            {
                HttpContext.Current.Response.Write(Transforms.ReturnAlert("对不起，您没有权限！"));
                HttpContext.Current.Response.End();
            }
            else
            {
                HttpContext.Current.Session["state"] = table[(int)enum_per];
            }

        }
        else
        {
            HttpContext.Current.Response.Write("<script>parent.window.location='" + baseUrl + "/Company/index.aspx?login=" + System.Configuration.ConfigurationSettings.AppSettings["lgError"].ToString() + "'</script>");
            HttpContext.Current.Response.End();
        }
    }
    public static int GetPermissions(EnumCompanyPermission enum_per)
    {
        int MSAccount = 0;
        if (HttpContext.Current.Session["permission"] != null)
        {
            Hashtable table = (Hashtable)HttpContext.Current.Session["permission"];

            if (table.Contains((int)enum_per))
            {

                MSAccount = (int)enum_per;
            }

        }
        return MSAccount;

    }

    /// <summary>
    /// 得到管理员是否有指定的权限
    /// </summary>
    /// <param name="permissionId">权限的枚举值</param>
    /// <param name="check">true:禁止访问时不可访问 false:没有限制</param>
    /// <returns>返回登陆状态类型的枚举值</returns>
    public static int GetManagePermission(int permissionId, bool check)
    {
        if (check == true)
        {
            if (HttpContext.Current.Application["jinzhi"].ToString().IndexOf("G") != -1)
            {
                return (int)EnumPermissions.RefuseG;
            }
        }
        if (HttpContext.Current.Session["permission"] != null)
        {
            Hashtable table = (Hashtable)HttpContext.Current.Session["permission"];
            if (!table.Contains(permissionId))
            {
                return (int)EnumPermissions.NoPermission;
            }
        }
        else
        {
            return (int)EnumPermissions.NoPermission;
        }

        return (int)EnumPermissions.ALLOW;
    }

    /// <summary>
    /// 得到管理员是否有指定的权限
    /// </summary>
    /// <param name="enum_per">权限的枚举值</param>
    /// <param name="check">true:禁止访问时不可访问 false:没有限制</param>
    /// <returns>返回登陆状态类型的枚举</returns>
    public static EnumPermissions GetManagePermission(EnumCompanyPermission enum_per, bool check)
    {
        if (check == true)
        {
            if (HttpContext.Current.Application["jinzhi"].ToString().IndexOf("G") != -1)
            {
                return EnumPermissions.RefuseG;
            }
        }
        if (HttpContext.Current.Session["permission"] != null)
        {
            Hashtable table = (Hashtable)HttpContext.Current.Session["permission"];
            if (!table.Contains((int)enum_per))
            {
                return EnumPermissions.NoPermission;
            }
        }
        else
        {
            return EnumPermissions.NoPermission;
        }

        return EnumPermissions.ALLOW;
    }


    /// <summary>
    /// 得到当前用户的类型，错误返回-1
    /// </summary>		
    /// <returns>返回用户类型的枚举值</returns>
    public static EnumUserType GetCurrentUserTypeEnum()
    {
        if (HttpContext.Current.Session["bh"] != null)
        {
            return EnumUserType.Member;
        }
        if (HttpContext.Current.Session["Store"] != null)
        {
            return EnumUserType.Store;
        }
        if (HttpContext.Current.Session["Company"] != null)
        {
            return EnumUserType.Company;
        }

        return EnumUserType.None;
    }

    /// <summary>
    /// 得到管理员权限的类型:普通 / 高级
    /// </summary>
    /// <returns></returns>
    public static EnumPermissionState GetManagerPermissionState()
    {
        if (HttpContext.Current.Session["state"] != null)
        {
            return (EnumPermissionState)(Convert.ToInt32(HttpContext.Current.Session["state"]));
        }
        else
        {
            return EnumPermissionState.Error;
        }
    }
    /// <summary>
    /// 验证是否有指定的操作权限
    /// </summary>
    /// <param name="enum_per"></param>
    /// <returns></returns>
    public static bool GetPermissionsValidate(EnumCompanyPermission enum_per)
    {
        bool flag = false;
        if (HttpContext.Current.Session["permission"] != null)
        {
            Hashtable table = (Hashtable)HttpContext.Current.Session["permission"];

            if (table.Contains((int)enum_per))
            {

                flag = true;
            }

        }
        return flag;
    }
    /// <summary>
    /// DS2012
    /// 判断会员登录是否超时
    /// </summary>
    /// <param name="pg"></param>
    /// <param name="url"></param>
    public static void MemRedirect(System.Web.UI.Page pg, String url)
    {

        if (pg.Session["Member"] == null)
        {
            pg.Response.Redirect(url, true);
        }
    }
    /// <summary>
    /// DS2012 
    /// 判断当前Session["Company"]是否为空，空则跳转到登陆页面
    /// </summary>
    /// <param name="pg"></param>
    /// <param name="url"></param>
    public static void ComRedirect(System.Web.UI.Page pg, String url)
    {

        if (pg.Session["Company"] == null)
        {
            pg.Response.Redirect(url ,true);
        }
    }

    public static void StoreRedirect(System.Web.UI.Page pg,String url)
    {

        if (pg.Session["Store"] == null)
        {
            pg.Response.Redirect(url, true);
        }
    }

    public static void ThreeRedirect(System.Web.UI.Page pg, String url)
    {

        if (pg.Session["Company"] == null && pg.Session["Store"] == null && pg.Session["Member"] == null)
        {
            if (pg.ClientQueryString != "pass=Login&type=member")
            {
               pg.Response.Redirect(url, true);//"Member/index.aspx"
            }
        }
    }
}

/// <summary>
/// 管理员权限 高低 枚举  0  1 不要改
/// </summary>
public enum EnumPermissionState
{
    /// <summary>
    /// 普通  0 
    /// </summary>
    common = 0,
    /// <summary>
    /// 高级  1 
    /// </summary>
    Advanced = 1,

    Error
}

