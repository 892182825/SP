using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using System.Data;
using System.Data.SqlClient;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-11
 */

namespace DAL
{
	public class DataDictionaryDAL
	{
        /// <summary>
        /// 通过键获取值
        /// </summary>
        /// <returns>返回值（0表示期数，1表示时间）</returns>
        public static int GetFileValueByCode()
        {
            int value = 0;
            try
            {
                value =Convert.ToInt32(DBHelper.ExecuteScalar("GetFileValueByCode", CommandType.StoredProcedure));
            }

            catch
            {
                value = 0;
            }
            return value;
        }

        /// <summary>
        /// 更改数据字典的值
        /// </summary>
        /// <param name="fileValue">键</param>
        /// <returns>返回更改所影响的行数</returns>
        public static int UpdFileValueByCode(int fileValue)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@fileValue",SqlDbType.Int)
            };
            sparams[0].Value = fileValue;
            
            return DBHelper.ExecuteNonQuery("UpdFileValueByCode", sparams, CommandType.StoredProcedure);
        }
    }
}
