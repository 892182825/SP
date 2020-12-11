using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Model;
using DAL;
using System.Collections;
using Model.Other;
using System.Data;

/*
 *  创建人：  孙延昊
 *  创建时间：2009.8.27
 *  文件名：PermissionBLL.cs
 *  功能：角色添加，修改，删除，查询
 * **/                                                                                     
namespace BLL.other.Company
{
    #region 角色业务逻辑处理类
    /// <summary>
    /// 角色
    /// </summary>
    public class DeptRoleBLL
    {
        public DeptRoleBLL() { }

        DeptRoleDAL deptRoleDAL = new DeptRoleDAL();

        #region 获取所有角色信息
        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns></returns>
        public static IList<DeptRoleModel> GetDeptRole(PaginationModel pageInfo)
        {
            return DeptRoleDAL.GetDeptRoles(pageInfo);
        }
        #endregion

        #region 根据管理员编号获取所有角色信息

        /// <summary>
        /// 根据管理员编号获取所有角色信息
        /// </summary>
        /// <param name="manager">管理员编号</param>
        /// <param name="pageSize">页行数</param>
        /// <param name="currentPage">当前页数</param>
        /// <param name="recordCount">总数据行数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns></returns>
        public static IList<DeptRoleModel> GetDeptRole(int manager, int pageSize, int currentPage, out int recordCount, out int pageCount)
        {
            recordCount = 0;
            pageCount = 0;
            return null;
        }
        #endregion

        #region 添加角色及角色权限
        /// <summary>
        /// ds2012
        /// 添加角色及角色权限
        /// </summary>
        /// <param name="context">用来获取当前登录用户权限</param>
        /// <param name="deptRole">角色信息</param>
        /// <returns></returns>
        public static bool AddDeptRole(DeptRoleModel deptRole)
        {
            string ids = "";
            IDictionaryEnumerator ideor = deptRole.htbPerssion.GetEnumerator();
            while (ideor.MoveNext())
            {
                ids=ids+ideor.Key.ToString()+",";
            }
            try
            {
                DeptRoleDAL.AddDeptRole(ids, deptRole);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 删除角色信息
        /// <summary>
        /// ds2012
        /// 删除角色信息
        /// </summary>
        /// <returns></returns>
        public static string DelDeptRole(HttpContext context, int roleid)
        {
            DeptRoleModel deptRole = DeptRoleDAL.GetRoleDeptByID(roleid);
            if(deptRole==null)
            {
                return "该角色已经删除!";
            }
            return DeptRoleDAL.DelDeptRole(roleid);
        }
        #endregion

        #region 更新角色信息及角色对应权限信息
        /// <summary>
        /// ds2012
        /// 更新角色信息及角色对应权限信息
        /// </summary>
        /// <param name="deptRole"></param>
        /// <param name="permissionIds"></param>
        /// <returns></returns>
        public static bool UptDeptRole(DeptRoleModel deptRole)
        {
            string ids = "";
            IDictionaryEnumerator ideor = deptRole.htbPerssion.GetEnumerator();
            while (ideor.MoveNext())
            {
                ids += ideor.Key.ToString() + ",";
            }
            try
            {
                DeptRoleDAL.UptRoleDept(ids, deptRole);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion


        #region 判断当前给予角色的权限，是否在登录用户的权限树中
        /// <summary>
        /// 判断当前给予角色的权限，是否在登录用户的权限树中
        /// </summary>
        /// <param name="perId">权限编号</param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool CheckDeptRolePermission(int perId,HttpContext context)
        {
            System.Collections.Hashtable htb = (System.Collections.Hashtable)context.Session["permission"];
            return htb.Contains(perId);
        }
        #endregion

        #region 判断用户角色是否有将自己的权限分配给下级的能力
        /// <summary>
        /// 判断用户角色是否有将自己的权限分配给下级的能力
        /// </summary>
        /// <returns></returns>
        private bool CheckDeptRole(int deptRoleID)
        {
            return false;
        }
        #endregion

        #region 判断角色名是否重复
        /// <summary>
        /// 判断角色名是否重复
        /// </summary>
        /// <param name="deptRoleID">角色ID</param>
        /// <param name="deptRoleName"></param>
        /// <returns></returns>
        public static DeptRoleModel CheckDeptRoleName(string deptRoleName,int deptRoleID)
        {
            return DeptRoleDAL.GetDeptRole(deptRoleName,deptRoleID);
        }
        #endregion

        #region 获取角色所有权限编号
        public static IList<ManagerPermissionModel> GetMPermission()
        {
           
            return null;
        }
        #endregion

        #region 获取角色权限树
        /// <summary>
        /// 获取角色权限树
        /// </summary>
        /// <param name="hashtable">角色权限</param>
        /// <param name="isEdit">是否为修改 true 为修改</param>
        /// <returns></returns>
        public static string GetRPTree(Hashtable hashtable,bool isAdd,string number)
        {
            if (isAdd)
            {
                return DeptRoleDAL.GetRPTree(hashtable, number);
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 获取角色权限
        /// <summary>
        /// ds2012
        /// 获取指定管理员的所有权限
        /// </summary>
        /// <param name="manageId">指定管理员的编号</param>
        /// <returns></returns>
        public static Hashtable GetAllPermission(string manageId)
        {
            return DeptRoleDAL.GetAllPermission(manageId);
        }
        #endregion

        #region  获取指定角色信息
        /// <summary>
        /// ds2012
        /// 获取指定角色信息
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <returns></returns>
        public static DeptRoleModel GetDeptRoleByRoleID(int roleID)
        {
            return DeptRoleDAL.GetRoleDeptByID(roleID);
        }
        #endregion

        #region 根据角色编号获取角色权限信息
        /// <summary>
        /// 根据角色编号获取角色权限信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Hashtable GetAllPermission(int p)
        {
            return DeptRoleDAL.GetAllPermission(p);
        }
        #endregion

        #region  根据角色编号获取该角色下的管理员人数
        /// <summary>
        /// 根据角色编号获取该角色下的管理员人数
        /// </summary>
        /// <param name="p">角色编号</param>
        public static int GetCountByRoleId(int p)
        {
            return DeptRoleDAL.GetCountByRoleId(p);
        }
        #endregion

        #region 根据部门编号获取角色信息
        /// <summary>
        /// 根据部门编号获取角色信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static IList<DeptRoleModel> GetDeptRoles(int p)
        {
            return DeptRoleDAL.GetRoleDeptByComDept(p);
        }
        #endregion


        #region 角色分页获取
        /// <summary>
        /// 管理员
        /// </summary>
        /// <param name="number">管理员编号</param>
        /// <returns></returns>
        public static IList<DeptRoleModel> GetDeptRoles(string number,PaginationModel pageInfo)
        {
            string topManageId = BLL.CommonClass.CommonDataBLL.GetTopManageID(1);
            if (number.ToString() == topManageId)
            {
                return null;
            }
            else
            { 
                return DeptRoleDAL.GetDeptRoles(number, pageInfo);
            }
        }
        #endregion

        #region 分页获取角色数据
        /// <summary>
        /// 分页获取角色数据
        /// </summary>
        /// <param name="pageInfo">分页帮助类</param>
        /// <param name="tableName">表名</param>
        /// <param name="columns">列名</param>
        /// <param name="key">分页列名</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static System.Data.DataTable GetDeptRoleDataTable(PaginationModel pageInfo, string tableName, string columns, string key, string condition)
        {
            return DeptRoleDAL.GetDeptRolesDataTable(pageInfo, tableName, columns, key, condition);
        }
        #endregion

        #region 获取该当前登录管理员所有可以修改的角色编号    格式如:(3,5,6)
        /// <summary>
        /// DS2012
        /// 获取该当前登录管理员所有可以修改的角色编号
        /// </summary>
        /// <param name="number">当前管理员编号</param>
        /// <returns></returns>
        public static string GetDeptRoleIDs(string number)
        {
            return DeptRoleDAL.GetDeptRoleIds(number);
        }
        #endregion

        /// <summary>
        /// 获取该当前登录管理员可以查看的网络团队
        /// </summary>
        /// <param name="number">当前管理员编号</param>
        /// <returns></returns>
        public static int GetViewManage(string manageID)
        {
            return DeptRoleDAL.GetViewManage(manageID);
        }


        #region 验证当前登录管理员是否有分配权限的权利
        /// <summary>
        /// ds2012
        /// 验证当前登录管理员是否有分配权限的权利
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool CheckAllot(string number)
        {
            return DeptRoleDAL.GetAllot(number);
        }
        #endregion


        #region 获取当前要修改的角色在不在登录管理员角色的更改范围内
        /// <summary>
        /// ds2012
        /// 获取当前要修改的角色在不在登录管理员角色的更改范围内
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool CheckAllot(string number,int RoleId)
        {
            if (CheckAllot(number))
            {
                string ids = GetDeptRoleIDs(number);
                if (ids == "")
                    return false;
                else
                {
                    string[] id = ids.Split(',');
                    return id.Contains(RoleId.ToString());
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 根据角色及当前登录管理可修改角色获取所有制定部门下的角色
        /// <summary>
        /// 根据角色及当前登录管理可修改角色获取所有制定部门下的角色
        /// </summary>
        /// <param name="p">部门id</param>
        /// <param name="ids">当前登录管理可管理角色的集合</param>
        /// <returns></returns>
        public static IList<DeptRoleModel> GetDeptRoles(int p, string ids)
        {
            if (ids == "")
                return null;
            return DeptRoleDAL.GetRoleDeptByComDept(p,ids);
        }
        #endregion

        #region 分析当前用户的权限生成
        /// <summary>
        /// DS2012
        /// 获取权限菜单，并生成权限树
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="number"></param>
        /// <param name="htb"></param>
        /// <returns></returns>
        public string ResetAllPermission(int roleid,string number,Hashtable htb)
        {
            DataTable dt = DeptRoleDAL.GetPermission(roleid);
            StringBuilder strb = new StringBuilder();
            GetPermissionTree(dt, -100,strb,"");
            strb.Append(DeptRoleDAL.GetWareHousePer(number,htb,roleid));
            return strb.ToString();
        }
        #endregion

        #region 分析当前用户权限生成过程
        /// <summary>
        /// ds2012
        /// 拼接权限树 将菜单树状化
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="pid"></param>
        /// <param name="strb"></param>
        /// <param name="pids"></param>
        private void GetPermissionTree(DataTable dt,int pid,StringBuilder strb,string pids)
        {
            DataRow[] rows = dt.Select("parentid="+pid,"haschild desc");
            string pids_ = "";
            foreach (DataRow row in rows)
            {
                pids_ = pids == "" ? row[0].ToString() : pids + "," + row[0].ToString();
                if ((int)row[4] == 1)
                {
                    //是一个带下级的菜单项 
                    strb.Append("<div id=\"menu_" );strb.Append( row[0].ToString() );strb.Append( "\">"); 
                    if ((int)row[5] == 2)
                        strb.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\">");
                    if((int)row[5] == 3)
                        strb.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\">");
                    strb.Append("<IMG class=\"menutop\" id=\"plus" );strb.Append( row[0].ToString() );strb.Append( "\" onclick=\"menu('menu");
                    strb.Append( row[0].ToString() );strb.Append( "','img" );strb.Append( row[0].ToString() );
                    strb.Append( "',this)\" src=\"images/plus3.gif\" align=\"absMiddle\"><IMG id=\"img" );
                    strb.Append( row[0].ToString() );strb.Append( "\" src=\"images/foldclose.gif\" align=\"absMiddle\"><input id=\"checkbox" );
                    strb.Append( row[0].ToString() );strb.Append( "\" type=\"checkbox\" onclick=\"CheckChildren('menu" );
                    strb.Append( row[0].ToString() );strb.Append( "','checkbox" );strb.Append( row[0].ToString() );strb.Append( "','");
                    strb.Append(pids_);strb.Append("')\" value=\"" );strb.Append( row[1].ToString() );strb.Append( "\" name=\"qxCheckBox\">" );
                    strb.Append( row[2].ToString() );strb.Append( "</div>");
                    strb.Append("<div id=\"menu" );strb.Append( row[0].ToString() );strb.Append( "\" style=\"MARGIN-TOP: -3px; DISPLAY: none\">"); 
                    if ((int)row[5] == 4)
                        strb.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\">");
                    int i = (int)row[0];
                    GetPermissionTree(dt, i, strb, pids_);
                    strb.Append("</div>");
                }
                else
                {
                    strb.Append("<div>");
                    if ((int)row[5] == 2)
                        strb.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line3.gif\" align=\"absMiddle\">");
                    if ((int)row[5] == 3)
                        strb.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\">");
                    if ((int)row[5] == 4)
                        strb.Append("<IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line1.gif\" align=\"absMiddle\"><IMG src=\"images/line2.gif\" align=\"absMiddle\">");
                    strb.Append("<input id=\"checkbox"); strb.Append(row[0].ToString()); strb.Append("\" onclick=\"checkpid('checkbox"); strb.Append(pid); strb.Append("',this,'"); strb.Append(pids_); strb.Append("');\" type=\"checkbox\"  value=\""); strb.Append(row[1].ToString()); strb.Append("\"name=\"qxCheckBox\">"); strb.Append(row[2].ToString()); strb.Append("<br /></div>");
                }
            }
            
        }
        #endregion
    }
    #endregion


}
