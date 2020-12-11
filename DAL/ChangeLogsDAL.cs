using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using System.Data;
using System.Data.SqlClient;
using Model;

/*
 * 创建者：  汪  华
 * 创建时间：2009-09-23
 * 功能：    对日志进行增删改查
 */

namespace DAL
{
    public class ChangeLogsDAL
    {
        /// <summary>
        /// 删除指定时间以内的系统日志
        /// </summary>
        /// <param name="endTime">截止时间</param>
        /// <returns>返回删除指定时间以内的系统日志所影响的行数</returns>
        public static int DelChangeLogsByEndTime(DateTime endTime)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@endTime",SqlDbType.DateTime)
            };
            sparams[0].Value = endTime;
            
            return DBHelper.ExecuteNonQuery("DelChangeLogsByEndTime", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 根据条件查询日志记录
        /// </summary>
        /// <param name="cloumnNames">列名</param>
        /// <param name="conditions">查询条件</param>        
        /// <returns>返回DataTable对象</returns>
        public static DataTable ChangeLogsQuery(string cloumnNames, string conditions)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@cloumnNames",SqlDbType.NVarChar,1000),
                new SqlParameter("@conditions",SqlDbType.NVarChar,1000)
            };
            sparams[0].Value = cloumnNames;
            sparams[1].Value = conditions;
            
            return DBHelper.ExecuteDataTable("ChangeLogsQuery", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 根据ID获取备注内容
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>返回备注内容</returns>
        public static string GetChangeLogsRemarkByID(int ID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@ID",SqlDbType.Int)
            };
            sparams[0].Value = ID;            
            
            return Convert.ToString(DBHelper.ExecuteScalar("GetChangeLogsRemarkByID", sparams, CommandType.StoredProcedure));   
        }
    }
}
