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
 * 文件名：     BackupMemberDetailsDAL
 */

namespace DAL
{
    public class BackupMemberDetailsDAL
    {
        /// <summary>
        /// 获取所有的会员备份报单产品明细数据
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetALLBackupMemberDetails()
        {            
            return DBHelper.ExecuteDataTable("GetALLBackupMemberDetails", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 将指定期数的数据从会员报单产品明细备份到会员备份报单产品明细
        /// </summary>
        /// <param name="expectNum">期数</param>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddBackupMemberDetailsByExpectNum(int expectNum)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@expectNum",SqlDbType.Int)                    
            };
            sparams[0].Value = expectNum;
            
            return DBHelper.ExecuteNonQuery("AddBackupMemberDetailsByExpectNum", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 备份指定时间的会员报单产品明细
        /// </summary>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">截止时间</param>
        /// <returns>返回插入记录所影响的行数</returns>
        public static int AddBackupMemberDetailsByDate(DateTime beginDate, DateTime endDate)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@beginDate",SqlDbType.DateTime),
                new SqlParameter("@endDate",SqlDbType.DateTime)
            };
            sparams[0].Value = beginDate;
            sparams[1].Value = endDate;
            
            return DBHelper.ExecuteNonQuery("AddBackupMemberDetailsByDate", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 根据期数删除会员备份报单产品明细数据
        /// </summary>
        /// <param name="expectNum">期数</param>
        /// <returns>返回删除会员备份报单产品明细数据所影响的行数</returns>
        public static int DelBackupMemberDetailsByExpectNum(int expectNum)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@expectNum",SqlDbType.Int)
            };
            sparams[0].Value = expectNum;
            
            return DBHelper.ExecuteNonQuery("DelBackupMemberDetailsByExpectNum", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 删除指定日期备份的会员报单产品明细记录
        /// </summary>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">截止时间</param>
        /// <returns>返回删除所影响的行数</returns>
        public static int DelBackupMemberDetailsByDate(DateTime beginDate, DateTime endDate)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@beginDate",SqlDbType.DateTime),
                new SqlParameter("@endDate",SqlDbType.DateTime)
            };
            sparams[0].Value = beginDate;
            sparams[1].Value = endDate;
            
            return DBHelper.ExecuteNonQuery("DelBackupMemberDetailsByDate", sparams, CommandType.StoredProcedure);
        }
    }
}
