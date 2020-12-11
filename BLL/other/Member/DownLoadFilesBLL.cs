using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using DAL;
using Model;
using Model.Other;
using System.Data;
using System.Data.SqlClient;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-24
 * 文件名：     DownLoadFilesBLL
 */

namespace BLL.other.Member
{
    public class DownLoadFilesBLL
    {
        /// <summary>
        /// 查询指定条件的Resources信息
        /// </summary>
        /// <param name="columnNames">列名</param>
        /// <param name="conditions">条件</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetResourcesInfoByConditions(string columnNames, string conditions)
        {
            return ResourcesDAL.GetResourcesInfoByConditions(columnNames, conditions);
        }

        /// <summary>
        /// 更新指定的下载次数
        /// </summary>
        /// <param name="resID">资源ID</param>
        /// <returns>返回更新指定的下载次数所影响的行数</returns>
        public static int UpdResourcesResTimesByResID(int resID)
        {
            return ResourcesDAL.UpdResourcesResTimesByResID(resID);
    
        }
    }
}
