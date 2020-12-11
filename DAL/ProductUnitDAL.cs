using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using Model;
using System.Data.SqlClient;
using System.Data;

/*
 * Creator:     WangHua
 * ModifyDate： 09/08/31
 * Modifier:    WangHua
 * FinishDate:  2010-01-27   
 * FileName：   ProductUnitDAL
 * Function：   Add,Delete,Update,Select product unit information
 */

namespace DAL
{
    public class ProductUnitDAL
    {
        /// <summary>
        /// 向产品状态单位表中插入记录
        /// </summary>
        /// <param name="tran">Transaction</param>
        /// <param name="productUnit">产品单位模型</param>
        /// <param name="id">Id</param>
        /// <returns>返回向产品状态单位表中插入记录所影响的行数</returns>
        public static int AddProductUnit(SqlTransaction tran,ProductUnitModel productUnit,out int id)
        {
            int addCount = 0;
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productUnitName",SqlDbType.VarChar,50),
                new SqlParameter("@productUnitDescr",SqlDbType.VarChar,50),
                new SqlParameter("@identityId",SqlDbType.Int)
            };
            sparams[0].Value = productUnit.ProductUnitName;
            sparams[1].Value = productUnit.ProductUnitDescr;
            sparams[2].Value = ParameterDirection.Output;
         
            addCount=DBHelper.ExecuteNonQuery(tran,"AddProductUnit", sparams, CommandType.StoredProcedure);
            id = Convert.ToInt32(sparams[2].Value);
            return addCount;
        }

        /// <summary>
        /// Delete the productUint information by productUnitId
        /// </summary>
        /// <param name="tran">Transaction</param>
        /// <param name="productUnitID">ProductUnitId</param>
        /// <returns>Return affected couts which delete the productUint information by productUnitId</returns>
        public static int DelProductUnitByID(SqlTransaction tran,int productUnitID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productUnitID",SqlDbType.Int)
            };
            sparams[0].Value = productUnitID;            
            return DBHelper.ExecuteNonQuery(tran,"DelProductUnitByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 更新指定产品单位信息
        /// </summary>
        /// <param name="productUnit">产品单位模型</param>
        /// <returns>返回更新指定产品单位信息所影响的行数</returns>
        public static int UpdProductUnitByID(ProductUnitModel productUnit)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productUnitID",SqlDbType.Int),
                new SqlParameter("@productUnitName",SqlDbType.VarChar,50),
                new SqlParameter("@productUnitDescr",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productUnit.ProductUnitID;
            sparams[1].Value = productUnit.ProductUnitName;
            sparams[2].Value = productUnit.ProductUnitDescr;

            return DBHelper.ExecuteNonQuery("UpdProductUnitByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 查询产品单位
        /// </summary>
        /// <param name="productUnitID"></param>
        /// <returns></returns>
        public static ProductUnitModel GetProductUnitItem(int productUnitID)
        {
            string cmd = "select ProductUnitID,ProductUnitName,ProductUnitDescr from ProductUnit where ProductUnitID=@ProductUnitID";

            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@ProductUnitID",productUnitID)
            };

            SqlDataReader dr = DBHelper.ExecuteReader(cmd,param,CommandType.Text);

            dr.Read();

            ProductUnitModel psm = new ProductUnitModel();
            psm.ProductUnitID = Convert.ToInt32(dr["ProductUnitID"]);
            psm.ProductUnitName = dr["ProductUnitName"].ToString();
            psm.ProductUnitDescr = dr["ProductUnitDescr"].ToString();

            dr.Close();

            return psm;
        }

        /// <summary>
        /// Judge the ProductUnitId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>Return the counts of the ProductUnitId by Id</returns>
        public static int ProductUnitIdIsExist(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };

            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("ProductUnitIdIsExist", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Judge the ProductUnitId whether has operation by Id before delete
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>Return the counts of the ProductUnitId by Id</returns>
        public static int ProductUnitIdWhetherHasOperation(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };

            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("ProductUnitIdWhetherHasOperation", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 通过联合查询获取信息
        /// </summary>
        /// <param name="productCode">产品编码</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMoreProductInfoByProductCode(string productCode)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productCode",SqlDbType.VarChar,100)
            };
            sparams[0].Value = productCode;
            
            return DBHelper.ExecuteDataTable("GetMoreProductInfoByProductCode", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取产品单位名称
        /// </summary>
        /// <param name="productUnitID">产品编号</param>
        /// <returns>返回产品单位名称</returns>
        public static string GetProductUnitNameByID(int productUnitID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productUnitID",SqlDbType.Int)
            };
            sparams[0].Value = productUnitID; 

            return Convert.ToString(DBHelper.ExecuteScalar("GetProductUnitNameByID", sparams, CommandType.StoredProcedure).ToString());
        }

        /// <summary>
        /// 获取产品单位ID和单位名称
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductUnitIDNameOrderByUnitID()
        {
            return DBHelper.ExecuteDataTable("GetProductUnitIDNameOrderByUnitID", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取指定的产品单位行数
        /// </summary>
        /// <param name="productUnitName">产品单位名称</param>
        /// <returns>返回获取指定的产品单位行数</returns>
        public static int GetProductUnitCountByName(string productUnitName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productUnitName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productUnitName;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetProductUnitCountByName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取指定的产品单位行数
        /// </summary>
        /// <param name="productUnitID">产品单位ID</param>
        /// <param name="productUnitName">产品单位名称</param>
        /// <returns>返回获取指定的产品单位行数</returns>
        public static int GetProductUnitCountByIDName(int productUnitID, string productUnitName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productUnitID",SqlDbType.Int),
                new SqlParameter("@productUnitName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productUnitID;
            sparams[1].Value = productUnitName;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetProductUnitCountByIDName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取产品单位信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductUnitInfo()
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@languageCode",SqlDbType.NVarChar,40)
            };
            sparams[0].Value = System.Web.HttpContext.Current.Session["languageCode"];
            return DBHelper.ExecuteDataTable("GetProductUnitInfo",sparams,CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取指定产品单位信息
        /// </summary>
        /// <param name="productUnitID">产品单位名称</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductUnitInfoByID(int productUnitID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productUnitID",SqlDbType.Int)
            };
            sparams[0].Value = productUnitID;

            return DBHelper.ExecuteDataTable("GetProductUnitInfoByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Out to excel the all data of ProductUnit
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_ProductUnit()
        {
            return DBHelper.ExecuteDataTable("OutToExcel_ProductUnit", CommandType.StoredProcedure);
        }
    }
}
