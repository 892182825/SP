using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using System.Data;
using System.Data.SqlClient;

/*
 *Creator:      WangHua
 *CreateDate:   2009-10-11
 *FinishDate:   2010-01-28
 *Function：     Manager permission operation
 */

namespace DAL
{
    public class ManagerPermissionDAL
    {
        /// <summary>
        /// 通过管理员编号获取仓库相应的权限
        /// </summary>
        /// <param name="number">管理员编号</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMoreManagerPermissionByNumber(string number)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@number",SqlDbType.NVarChar,40)
            };
            sparams[0].Value = number;

            return DBHelper.ExecuteDataTable("GetMoreManagerPermissionByNumber", sparams, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="PermissionID">权限ID</param>
        /// <param name="ManagerID">角色ID</param>
        /// <param name="State">状态</param>
        /// <returns></returns>
        public static int AddManagerPermission(int PermissionID, int ManagerID,int State)
        {
            string sql = string.Format("insert into ManagerPermission values({0},{1},{2})", PermissionID, ManagerID, State);
            return DBHelper.ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="PermissionID">权限ID</param>
        /// <returns></returns>
        public static int DelManagerPermission(int PermissionID)
        {
            string sql = string.Format("delete ManagerPermission where PermissionID={0}", PermissionID);
            return DBHelper.ExecuteNonQuery(sql);
        }
    }
}
