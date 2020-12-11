using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DataSourceDAL
    {
        public DataSourceDAL()
        { 
        }

        /// <summary>
        /// 分页数据读取函数 
        /// </summary>
        /// <param name="_tblName">表名，可以是多个表，但不能用别名</param>
        /// <param name="_strKey">主键，可以为空，但@Order为空时该值不能为空</param>
        /// <param name="_fldName">要取出的字段，可以是多个表的字段，可以为空，为空表示select *</param>
        /// <param name="_pageSize">每页记录数</param>
        /// <param name="_page">当前页，0表示第1页</param>
        /// <param name="_strWhere">条件，可以为空，不用填 where</param>
        /// <param name="_group">分组依据，可以为空，不用填 group by</param>
        /// <param name="_fldSort">排序，可以为空，为空默认按主键升序排列，不用填 order by</param>
        /// <param name="_PageCount">返回多少页</param>
        /// <param name="_RecordCount">返回多少条记录</param>
        /// <returns></returns>
        public static DataTable GetDataPage_DataTable(string _tblName, string _strKey, string _fldName, int _pageSize, int _page, string _strWhere, string _group, string _fldSort, out int _RecordCount, out int _PageCount)
        {

            _RecordCount = 0;
            _PageCount = 0;
            SqlParameter[] parm0 = {   new SqlParameter("@TableNames",SqlDbType.VarChar,2000),
									   new SqlParameter("@PrimaryKey",SqlDbType.VarChar,100),
									   new SqlParameter("@Fields",SqlDbType.VarChar,2000),
									   new SqlParameter("@PageSize",SqlDbType.Int),
									   new SqlParameter("@CurrentPage",SqlDbType.Int),
									   new SqlParameter("@Filter",SqlDbType.VarChar,2000),
									   new SqlParameter("@Group",SqlDbType.VarChar,200),
									   new SqlParameter("@Order",SqlDbType.VarChar,200),
									   new SqlParameter("@RecordCount",SqlDbType.Int),
									   new SqlParameter("@PageCount",SqlDbType.Int)
								   };

            parm0[0].Value = _tblName;
            parm0[1].Value = _strKey;
            parm0[2].Value = _fldName;
            parm0[3].Value = _pageSize;
            parm0[4].Value = _page;
            parm0[5].Value = _strWhere;
            parm0[6].Value = _group;
            parm0[7].Value = _fldSort;
            parm0[8].Value = _RecordCount;
            parm0[9].Value = _PageCount;

            parm0[8].Direction = System.Data.ParameterDirection.Output;
            parm0[9].Direction = System.Data.ParameterDirection.Output;


            DataTable dt = DBHelper.ExecuteDataTable("P_DataPage", parm0, CommandType.StoredProcedure);
            _RecordCount = Convert.ToInt32(parm0[8].Value);
            _PageCount = Convert.ToInt32(parm0[9].Value);
            return dt;

        }

    }
}
