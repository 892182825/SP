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
 */

namespace DAL
{
    public class BackupOrderDetailDAL
    {
        /// <summary>
        /// 备份指定时间段店铺订货产品明细
        /// </summary>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddBackupOrderDetailByDate(DateTime beginDate,DateTime endDate)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@beginDate",SqlDbType.DateTime),
                new SqlParameter("@endDate",SqlDbType.DateTime)
            };
            sparams[0].Value = beginDate;
            sparams[1].Value = endDate;
            
            return DBHelper.ExecuteNonQuery("AddBackupOrderDetailByDate",sparams,CommandType.StoredProcedure);
        }

        /// <summary>
        /// 备份指定期数店铺订货产品明细记录
        /// </summary>
        /// <param name="expectNum">期数</param>
        /// <returns>返回插入记录所影响的行数</returns>
        public static int AddBackupOrderDetailByExpectNum(int expectNum)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@expectNum",SqlDbType.Int)
            };
            sparams[0].Value = expectNum;
            
            return DBHelper.ExecuteNonQuery("AddBackupOrderDetailByExpectNum", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取所有的店铺订货产品明细备份数据
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetALLBackupOrderDetail()
        {            
            return DBHelper.ExecuteDataTable("GetALLBackupOrderDetail", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 删除指定期数店铺订货产品明细记录
        /// </summary>
        /// <param name="expectNum">期数</param>
        /// <returns>返回删除记录所影响的行数</returns>
        public static int DelBackupOrderDetailByExpectNum(int expectNum)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@expectNum",SqlDbType.Int)
            };
            sparams[0].Value = expectNum;
            
            return DBHelper.ExecuteNonQuery("DelBackupOrderDetailByExpectNum", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 清空所有的店铺订货产品明细备份数据
        /// </summary>
        /// <returns>返回受影响的行数</returns>
        public static int ClearAllBackupOrderDetail()
        {            
            return DBHelper.ExecuteNonQuery("ClearAllBackupOrderDetail", CommandType.StoredProcedure);
        }
    }
}
