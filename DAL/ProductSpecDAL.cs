using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Data;

/*
 * Creator：    WangHua
 * ModifyDate： 09/08/31
 * Modifier:    WangHua
 * FinishDate:  2010-01-27   
 * FileName：   ProductSpecDAL
 * Function：   Add,Delete,Update,Select product specifications information
 */

namespace DAL
{
    public class ProductSpecDAL
    {
        /// <summary>
        /// 向产品规格表中插入记录
        /// </summary>
        /// <param name="productSpec">产品规格ID</param>
        /// <returns>返回向产品规格表中插入记录所影响的行数</returns>
        public static int AddProductSpec(ProductSpecModel productSpec)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productSpecName",SqlDbType.VarChar,50),
                new SqlParameter("@productSpecDescr",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productSpec.ProductSpecName;
            sparams[1].Value = productSpec.ProductSpecDescr;
           
            return DBHelper.ExecuteNonQuery("AddProductSpec", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 删除指定产品规格记录
        /// </summary>
        /// <param name="productSpecID">产品规格ID</param>
        /// <returns>返回删除指定产品规格记录所影响的行数</returns>
        public static int DelProductSpecByID(int productSpecID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productSpecID",SqlDbType.Int)
            };
            sparams[0].Value = productSpecID;
          
            return DBHelper.ExecuteNonQuery("DelProductSpecByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 更新指定产品规格信息
        /// </summary>
        /// <param name="productSpec">产品规格模型</param>
        /// <returns>返回更新指定产品规格信息所影响的行数</returns>
        public static int UpdProductSpecByID(ProductSpecModel productSpec)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productSpecID",SqlDbType.Int),
                new SqlParameter("@productSpecName",SqlDbType.VarChar,50),
                new SqlParameter("@productSpecDescr",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productSpec.ProductSpecID;
            sparams[1].Value = productSpec.ProductSpecName;
            sparams[2].Value = productSpec.ProductSpecDescr;
                       
            return DBHelper.ExecuteNonQuery("UpdProductSpecByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 查询产品规格
        /// </summary>
        /// <param name="productSpecID"></param>
        /// <returns></returns>
        public static ProductSpecModel GetProductSpecItem(int productSpec)
        {
            string cmd = "select ProductSpecName,ProductSpecDEscr from ProductSpec where ProductSpec=@ProductSpec";

            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@ProductSpec",productSpec)
            };

            SqlDataReader dr = DBHelper.ExecuteReader(cmd,param,CommandType.Text);

            dr.Read();

            ProductSpecModel psm = new ProductSpecModel(productSpec);
     
            psm.ProductSpecName = dr["ProductSpecName"].ToString();
            psm.ProductSpecDescr = dr["ProductSpecDEscr"].ToString();

            dr.Close();

            return psm;
        }

        /// <summary>
        /// 获取产品规格名称
        /// </summary>
        /// <param name="productSpecID">产品规格ID</param>
        /// <returns>返回产品规格名称</returns>
        public static string GetProductSpecNameByID(int productSpecID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productSpecID",SqlDbType.Int)
            };
            sparams[0].Value = productSpecID;
            
            return Convert.ToString(DBHelper.ExecuteScalar("GetProductSpecNameByID", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取指定产品规格的行数
        /// </summary>
        /// <param name="productSpecName">产品规格名称</param>
        /// <returns>返回获取指定产品规格的行数</returns>
        public static int GetProductSpecCountByName(string productSpecName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productSpecName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productSpecName;            

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetProductSpecCountByName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取指定产品规格的行数
        /// </summary>
        /// <param name="productSpecID">产品规格ID</param>
        /// <param name="productSpecName">产品规格名称</param>
        /// <returns>返回获取指定产品规格的行数</returns>
        public static int GetProductSpecCountByIDName(int productSpecID, string productSpecName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productSpecID",SqlDbType.Int),
                new SqlParameter("@productSpecName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productSpecID;
            sparams[1].Value = productSpecName;
            
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetProductSpecCountByIDName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取产品规格信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductSpecInfo()
        {         
            return DBHelper.ExecuteDataTable("GetProductSpecInfo", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取指定产品规格信息
        /// </summary>
        /// <param name="productSpecID">产品规格ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductSpecInfoByID(int productSpecID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productSpecID",SqlDbType.Int)
            };
            sparams[0].Value = productSpecID;
            
            return DBHelper.ExecuteDataTable("GetProductSpecInfoByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Judge the ProductSpecId whether has operation before delete by Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductSpec by Id</returns>
        public static int ProductSpecIdWhetherHasOperation(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("ProductSpecIdWhetherHasOperation", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Judge the ProductSpecId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductSpec by Id</returns>
        public static int ProductSpecIdIsExist(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("ProductSpecIdIsExist", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Out to excel to the all data of ProductSpec
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_ProductSpec()
        {
            return DBHelper.ExecuteDataTable("OutToExcel_ProductSpec", CommandType.StoredProcedure); 
        }
    }
}
