using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using System.Data;
using System.Data.SqlClient;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-24
 */

namespace DAL
{
    public class BackupChangeLogsDAL
    {
        /// <summary>
        /// 向日志备份表中插入相关记录
        /// </summary>
        /// <returns>返回向日志备份表中插入相关记录所影响的行数</returns>
        public static int AddBackupChangeLogs()
        {            
            return DBHelper.ExecuteNonQuery("AddBackupChangeLogs", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取日志备份表中信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetBackupChangeLogsInfo()
        {            
            return DBHelper.ExecuteDataTable("GetBackupChangeLogsInfo", CommandType.StoredProcedure);
        }
    }
}
