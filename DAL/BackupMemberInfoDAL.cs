using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using System.Data;
using System.Data.SqlClient;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-10
 * 文件名：     BackupMemberInfoDAL 
 */

namespace DAL
{
    public class BackupMemberInfoDAL
    {
        /// <summary>
        /// 从会员备份基本信息表获取所有的信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetAllInfoFromBackupMemberInfo()
        {            
            return DBHelper.ExecuteDataTable("GetAllInfoFromBackupMemberInfo", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 把数据从会员信息表中复制到会员备份基本信息表中
        /// </summary>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddAllDataFromMemberInfo()
        {            
            return DBHelper.ExecuteNonQuery("AddAllDataFromMemberInfo", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 把数据从会员信息表中复制到会员备份基本信息表中
        /// </summary>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddAllDataFromMemberInfo(DateTime beginDate, DateTime endDate)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@beginDate",SqlDbType.DateTime),
                new SqlParameter("@endDate",SqlDbType.DateTime)
            };
            sparams[0].Value = beginDate.ToUniversalTime();
            sparams[1].Value = endDate.ToUniversalTime();

            return DBHelper.ExecuteNonQuery("AddAllDataFromMemberInfoBeginEndTime", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 把数据从会员信息表中复制到会员备份基本信息表中
        /// </summary>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddAllDataFromMemberInfo(int qishu)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@qishu",SqlDbType.Int)
            };
            sparams[0].Value = qishu;

            return DBHelper.ExecuteNonQuery("AddAllDataFromMemberInfoQishu", sparams, CommandType.StoredProcedure);
        }
    }
}
