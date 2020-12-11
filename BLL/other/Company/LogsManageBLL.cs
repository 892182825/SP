using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using System.Data;
using DAL;

/*
 * 创建者：  汪  华
 * 创建时间：2009-09-24
 */

namespace BLL.other.Company
{
    public class LogsManageBLL
    {
        /// <summary>
        /// 向日志备份表中插入相关记录
        /// </summary>
        /// <returns>返回向日志备份表中插入相关记录所影响的行数</returns>
        public static int AddBackupChangeLogs()
        {
            return BackupChangeLogsDAL.AddBackupChangeLogs();
        }

        /// <summary>
        /// 删除指定时间以内的系统日志
        /// </summary>
        /// <param name="endTime">截止时间</param>
        /// <returns>返回删除指定时间以内的系统日志所影响的行数</returns>
        public static int DelChangeLogsByEndTime(DateTime endTime)
        {
            return ChangeLogsDAL.DelChangeLogsByEndTime(endTime);
        }

        /// <summary>
        /// 根据ID获取备注内容
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>返回备注内容</returns>
        public static string GetChangeLogsRemarkByID(int ID)
        {
            return ChangeLogsDAL.GetChangeLogsRemarkByID(ID);
        }

        /// <summary>
        /// 获取日志备份表中信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetBackupChangeLogsInfo()
        {
            return BackupChangeLogsDAL.GetBackupChangeLogsInfo();
        }

        /// <summary>
        /// 根据条件查询日志记录
        /// </summary>
        /// <param name="cloumnNames">列名</param>
        /// <param name="conditions">查询条件</param>        
        /// <returns>返回DataTable对象</returns>
        public static DataTable ChangeLogsQuery(string cloumnNames, string conditions)
        {
            return ChangeLogsDAL.ChangeLogsQuery(cloumnNames,conditions); 
        }
    }
}
