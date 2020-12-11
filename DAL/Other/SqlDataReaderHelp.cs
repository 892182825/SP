using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Other;
using System.Data.SqlClient;
using System.Data;

namespace DAL.Other
{
    /// <summary>
    /// 处理数据结果集封装类
    /// </summary>
    public class SqlDataReaderHelp
    {
        /// <summary>
        /// 处理返回结果集合
        /// </summary>
        /// <param name="pagin">分页类</param>
        /// <param name="tableName">表名</param>
        /// <param name="key">排序键值</param>
        /// <param name="comlums">列名</param>
        /// <param name="condition">条件</param>
        /// <returns>结果集</returns>
        public static SqlDataReader DisSqlReader(PaginationModel pagin, string tableName, string key, string comlums, string condition)
        {
            SqlParameter[] ps = new SqlParameter[] 
            {
                new SqlParameter("@tableName",tableName),
                new SqlParameter("@key",key),
                new SqlParameter("@comlums",comlums),
                new SqlParameter("@condition",condition),
                new SqlParameter("@start",pagin.GetPageDate()),
                new SqlParameter("@end",pagin.GetEndDate()),
                new SqlParameter("@DataCount",pagin.DataCount)
            };
            ps[6].Direction = ParameterDirection.Output;
            ps[6].Size = 10;
            SqlDataReader dr = DBHelper.ExecuteReader("GetDataPagePagination", ps, CommandType.StoredProcedure);
            return dr;
        }

        /// <summary>
        /// 处理返回结果集合
        /// </summary>
        /// <param name="pagin">分页类</param>
        /// <param name="tableName">表名</param>
        /// <param name="key">排序键值</param>
        /// <param name="comlums">列名</param>
        /// <param name="condition">条件</param>
        /// <returns>结果集</returns>
        public static DataTable GetDataTable(PaginationModel pagin, string tableName, string key, string comlums, string condition)
        {
            SqlParameter[] ps = new SqlParameter[] 
            {
                new SqlParameter("@tableName",tableName),
                new SqlParameter("@key",key),
                new SqlParameter("@comlums",comlums),
                new SqlParameter("@condition",condition),
                new SqlParameter("@start",pagin.GetPageDate()),
                new SqlParameter("@end",pagin.GetEndDate()),
                new SqlParameter("@DataCount",pagin.DataCount)
            };
            ps[6].Direction = ParameterDirection.Output;
            ps[6].Size = 10;
            DataTable dt = DBHelper.ExecuteDataTable("GetDataPagePagination", ps, CommandType.StoredProcedure);
            pagin.DataCount = Convert.ToInt32(ps[6].Value.ToString());
            return dt;
        }
        /// <summary>
        /// 得到指定页码数据
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页记录数</param>
        /// <param name="table">表名</param>
        ///<param name="columns">列</param>
        /// <param name="condition">条件</param>
        /// <param name="key">关键字</param>
        /// <param name="RecordCount">总记录数</param>
        ///<param name="PageCount">页数</param>
        /// <returns></returns>
        public DataTable GetDataTablePage(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
            RecordCount = 0;
            PageCount = 0;
            SqlParameter[] parm0 = {new SqlParameter("@PageIndex",SqlDbType.Int),
									   new SqlParameter("@PageSize",SqlDbType.Int),
									   new SqlParameter("@table",SqlDbType.VarChar,1000),
									   new SqlParameter("@columns",SqlDbType.NVarChar,2000),
									   new SqlParameter("@condition",SqlDbType.NVarChar,2000),
									   new SqlParameter("@key",SqlDbType.VarChar,50),
				                       new SqlParameter("@RecordCount",SqlDbType.Int),
				                       new SqlParameter("@PageCount",SqlDbType.Int)
								   };

            parm0[0].Value = PageIndex;
            parm0[1].Value = PageSize;
            parm0[2].Value = table;
            parm0[3].Value = columns;
            parm0[4].Value = condition;
            parm0[5].Value = key;
            parm0[6].Value = RecordCount;
            parm0[7].Value = PageCount;

            parm0[6].Direction = System.Data.ParameterDirection.Output;
            parm0[7].Direction = System.Data.ParameterDirection.Output;


            DataTable dt = DBHelper.ExecuteDataTable("GetCustomersDataPage", parm0, CommandType.StoredProcedure);
            RecordCount = Convert.ToInt32(parm0[6].Value);
            PageCount = Convert.ToInt32(parm0[7].Value);

            return dt;
        }

    }
}
