using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using Model;
using System.Data;
using System.Data.SqlClient;


/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-14
 */

namespace DAL
{
    public class TranToNDAL
    {
        public static string getTransName(string tablename, string coutrol)
        {
            //try
            //{
            //    object obj = DBHelper.ExecuteScalar("select Text from " + tablename + " where Location='" + coutrol + "'");
            //    return obj == null ? "" : obj.ToString();
            //}
            //catch (Exception)
            //{
                return "";
            //}
        }

        /// <summary>
        ///  将中文对照翻译插入该表（TranToN）
        /// </summary>
        /// <param name="countryName">国家名称</param>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddTranToN(string countryName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@countryName",SqlDbType.NVarChar,100)
            };
            sparams[0].Value = countryName;
            int addCount = 0;
            addCount = DBHelper.ExecuteNonQuery("AddTranToN", sparams, CommandType.StoredProcedure);
            if (addCount == 0)
            {
                return addCount;
            }

            else
            {
                return addCount;
            }
        }

        /// <summary>
        /// 删除表TranToN
        /// </summary>
        /// <param name="countryName">国家名称</param>
        /// <returns>返回删除表TranToN所影响的行数</returns>
        public static int DelTranToN(string countryName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@countryName",SqlDbType.NVarChar,100)
            };
            sparams[0].Value = countryName;
            int delCount = 0;
            delCount = DBHelper.ExecuteNonQuery("DelTranToN", sparams, CommandType.StoredProcedure);
            if (delCount == 0)
            {
                return 0;
            }

            else
            {
                return delCount;
            }            
        }

        /// <summary>
        /// 更改指定表的文本
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="tranToModel">翻译语言模型</param>
        /// <returns>返回更改指定表的文本所影响的行数</returns>
        public static int UpdTranToN(string tableName, TranToNModel tranToModel)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@tableName",SqlDbType.NVarChar,200),
                new SqlParameter("@location",SqlDbType.VarChar,4000),
                new SqlParameter("@text",SqlDbType.NVarChar,2000)
            };
            sparams[0].Value = tableName;
            sparams[1].Value = tranToModel.Location;
            sparams[2].Value = tranToModel.Text;
            int updCount = 0;
            updCount = DBHelper.ExecuteNonQuery("UpdTranToN", sparams, CommandType.StoredProcedure);
            return updCount;
        }

        /// <summary>
        /// 获取语言翻译信息
        /// </summary>
        /// <param name="columnNames">列名</param>
        /// <param name="tableName">表名</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="key">排序关键字</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetTranToNInfoByCondition(string columnNames,string tableName,string conditions,string key)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@columnNames",SqlDbType.NVarChar,200),
                new SqlParameter("@tableName",SqlDbType.NVarChar,100),
                new SqlParameter("@conditions",SqlDbType.NVarChar,200),
                new SqlParameter("@key",SqlDbType.NVarChar,20)
            };
            sparams[0].Value = columnNames;
            sparams[1].Value = tableName;
            sparams[2].Value = conditions;
            sparams[3].Value = key;
            return DBHelper.ExecuteDataTable("GetTranToNInfoByCondition", sparams, CommandType.StoredProcedure);
        }
    }
}
