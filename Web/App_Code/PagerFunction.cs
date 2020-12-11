using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using DAL;

    public class PagerFunction
    {



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
        public static DataTable GetDataTablePage(int PageIndex, int PageSize, string table, string columns, string condition, string key, int ascOrDesc, out int RecordCount, out int PageCount)
        {

            RecordCount = 0;
            PageCount = 0;
            SqlParameter[] parm0 = {new SqlParameter("@PageIndex",SqlDbType.Int),
									   new SqlParameter("@PageSize",SqlDbType.Int),
									   new SqlParameter("@table",SqlDbType.NVarChar,4000),
									   new SqlParameter("@columns",SqlDbType.NVarChar,4000),
									   new SqlParameter("@condition",SqlDbType.NVarChar,4000),
									   new SqlParameter("@key",SqlDbType.VarChar,50),
                                       new SqlParameter("@ascOrDesc",SqlDbType.Int),
									   new SqlParameter("@RecordCount",SqlDbType.Int),
									   new SqlParameter("@PageCount",SqlDbType.Int)
								   };

            parm0[0].Value = PageIndex;
            parm0[1].Value = PageSize;
            parm0[2].Value = table;
            parm0[3].Value = columns;
            parm0[4].Value = condition;
            parm0[5].Value = key;
            parm0[6].Value = ascOrDesc;
            parm0[7].Value = RecordCount;
            parm0[8].Value = PageCount;

            parm0[7].Direction = System.Data.ParameterDirection.Output;
            parm0[8].Direction = System.Data.ParameterDirection.Output;


            DataTable dt = DBHelper.ExecuteDataTable("GetCustomersDataPage_shop", parm0, CommandType.StoredProcedure);
            RecordCount = Convert.ToInt32(parm0[7].Value);
            PageCount = Convert.ToInt32(parm0[8].Value);


            return dt;
        }
        /// <summary>
        /// 得到指定页的数据
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页记录数</param>
        /// <param name="sqlstr">sql语句</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="PageCount">页数</param>
        /// <returns></returns>
        public static DataTable GetPagerDataBySql(int PageIndex, int PageSize, string sqlstr, out int RecordCount, out int PageCount)
        {

            RecordCount = 0;
            PageCount = 0;
            SqlParameter[] parm0 = {   new SqlParameter("@currentpage",SqlDbType.Int),
									   new SqlParameter("@pagesize",SqlDbType.Int),
									   new SqlParameter("@sqlstr",SqlDbType.NVarChar,4000),
									   new SqlParameter("@rowcount",SqlDbType.Int),
									   new SqlParameter("@PageCount",SqlDbType.Int)
								   };

            parm0[0].Value = PageIndex;
            parm0[1].Value = PageSize;
            parm0[2].Value = sqlstr;
            parm0[3].Value = RecordCount;
            parm0[4].Value = PageCount;

            parm0[3].Direction = System.Data.ParameterDirection.Output;
            parm0[4].Direction = System.Data.ParameterDirection.Output;


            DataSet ds = DBHelper.ExecuteDataSet("GetPagerDataBySql", parm0, CommandType.StoredProcedure);

            RecordCount = Convert.ToInt32(parm0[3].Value);
            PageCount = Convert.ToInt32(parm0[4].Value);
            ds.Tables[1].Columns.Remove("ROWSTAT");
            return ds.Tables[1];
        }
    }
