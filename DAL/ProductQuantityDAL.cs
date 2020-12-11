using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using Model;
using DAL;
using System.Data;
using System.Data.SqlClient;

/*
 * Creator：    SongJun
 * ModifyDate： 09/08/31
 * Modifier:    WangHua
 * FinishDate:  2010-01-27   
 * FileName：   ProductQuantityDAL
 * Function：   
 */

namespace DAL
{
    /// <summary>
    /// 产品库存
    /// </summary>
    public class ProductQuantityDAL
    {
        CommonDataDAL commonDataDAL = new CommonDataDAL();
        #region 产品库存查询
        /// <summary>
        /// 产品库存查询（分页）
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="condition"></param>
        /// <param name="key"></param>
        /// <param name="RecordCount"></param>
        /// <param name="PageCount"></param>
        /// <returns></returns>
        public DataTable GetDataTablePage(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
            return commonDataDAL.GetDataTablePage(PageIndex, PageSize, table, columns, condition, key, out RecordCount, out PageCount);
        }
        #endregion
        public static DataTable getStorage(string sql)
        {
            DataTable dt = DBHelper.ExecuteDataTable(sql);
            return dt;
        }
        public static string GetWarehouseName(string warehouseId)
        {
            string sSQL = " SELECT WareHouseName FROM WareHouse WHERE WareHouseID = " + warehouseId;
            SqlDataReader reader = DBHelper.ExecuteReader(sSQL);
            if (reader.Read())
            {
                sSQL = reader[0].ToString();
            }
            else
            {
                sSQL = "";
            }

            reader.Close();

            return sSQL;
        }

        /// <summary>
        /// 删除产品库存表中指定的记录
        /// </summary>
        /// <param name="tran">要处理的事务</param>
        /// <param name="productID">产品ID</param>
        /// <returns>返回删除产品库存表中指定的记录所影响的行数</returns>
        public static int DelProductQuantityByProductID(SqlTransaction tran,int productID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productID",SqlDbType.Int)
            };
            sparams[0].Value = productID;

            return DBHelper.ExecuteNonQuery("DelProductQuantityByProductID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 通过产品编码获取产品编码的行数
        /// </summary>
        /// <param name="productCode">产品编码</param>
        /// <returns>返回产品编码的行数</returns>
        public static int GetProductCodeCountByProductCode(string productCode)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productCode",SqlDbType.VarChar,100)
            };
            sparams[0].Value = productCode;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetProductCodeCountByProductCode", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Get the productId count by productId then judge the productId whether exist
        /// </summary>
        /// <param name="productId">ProductId</param>
        /// <returns>Return the productId count by productId</returns>
        public static int ProductIdIsExist_ProductQuantity(int productId)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productId", SqlDbType.Int)
            };
            sparams[0].Value = productId;
            return Convert.ToInt32(DBHelper.ExecuteScalar("ProductIdIsExist_ProductQuantity", sparams, CommandType.StoredProcedure));
        }
    }
}
