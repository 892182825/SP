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
 * FileName：   ProductSizeDAL
 * Function：   Add,Delete,Update,Select product size information
 */

namespace DAL
{
    /// <summary>
    /// 产品尺寸表
    /// </summary>
    public class ProductSizeDAL
    {
        /// <summary>
        /// 插入产品尺寸记录
        /// </summary>
        /// <param name="productSize">产品尺寸模型</param>
        /// <returns>返回插入产品尺寸记录所影响的行数</returns>
        public static int AddProductSize(ProductSizeModel productSize)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productSizeName",SqlDbType.VarChar,50),
                new SqlParameter("@productSizeDescr",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productSize.ProductSizeName;
            sparams[1].Value = productSize.ProductSizeDescr;
         
            return DBHelper.ExecuteNonQuery("AddProductSize", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 删除指定的产品尺寸记录
        /// </summary>
        /// <param name="productSizeID">产品尺寸ID</param>
        /// <returns>删除指定的产品尺寸记录</returns>
        public static int DelProductSizeByID(int productSizeID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productSizeID",SqlDbType.Int)
            };
            sparams[0].Value = productSizeID;
            
            return DBHelper.ExecuteNonQuery("DelProductSizeByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 更新指定产品尺寸
        /// </summary>
        /// <param name="productSize">产品尺寸模型</param>
        /// <returns>返回更新指定产品尺寸</returns>
        public static int UpdProductSizeByID(ProductSizeModel productSize)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productSizeID",SqlDbType.Int),
                new SqlParameter("@productSizeName",SqlDbType.VarChar,50),
                new SqlParameter("@productSizeDescr",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productSize.ProductSizeID;
            sparams[1].Value = productSize.ProductSizeName;
            sparams[2].Value = productSize.ProductSizeDescr;
            
            return DBHelper.ExecuteNonQuery("UpdProductSizeByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取指定产品尺寸的行数
        /// </summary>
        /// <param name="productSizeName">产品尺寸名称</param>
        /// <returns>返回获取指定产品尺寸的行数</returns>
        public static int GetProductSizeCountByName(string productSizeName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productSizeName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productSizeName;
            
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetProductSizeCountByName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取指定产品尺寸的行数
        /// </summary>
        /// <param name="productSizeID">产品尺寸ID</param>
        /// <param name="productSizeName">产品尺寸名称</param>
        /// <returns>返回获取指定产品尺寸的行数</returns>
        public static int GetProductSizeCountByIDName(int productSizeID, string productSizeName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productSizeID",SqlDbType.Int),
                new SqlParameter("@productSizeName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productSizeID;
            sparams[1].Value = productSizeName;
            
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetProductSizeCountByIDName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 查询产品尺寸
        /// </summary>
        /// <param name="productSizeID"></param>
        /// <returns></returns>
        public static ProductSizeModel GetProductSizeItem(int productSizeID)
        {
            string cmd = "select ProductSizeID,ProductSizeName,ProductSizeDescr from ProductSize where ProductSizeID=@ProductSizeID";

            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@ProductSizeID",productSizeID)
            };

            SqlDataReader dr = DBHelper.ExecuteReader(cmd,param,CommandType.Text);

            dr.Read();

            ProductSizeModel psm = new ProductSizeModel();
            psm.ProductSizeID = Convert.ToInt32(dr["ProductSizeID"]);
            psm.ProductSizeName = dr["ProductSizeName"].ToString();
            psm.ProductSizeDescr = dr["ProductSizeDescr"].ToString();

            dr.Close();

            return psm;
        }

        /// <summary>
        /// 获取产品尺寸信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductSizeInfo()
        {
            return DBHelper.ExecuteDataTable("GetProductSizeInfo", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取指定产品尺寸信息
        /// </summary>
        /// <param name="productSizeID">产品尺寸ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductSizeInfoByID(int productSizeID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productSizeID",SqlDbType.Int)
            };
            sparams[0].Value = productSizeID;
            
            return DBHelper.ExecuteDataTable("GetProductSizeInfoByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Juage the ProductSizeId whether has operation before delete by Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductSize by Id</returns>
        public static int ProductSizeIdWhetherHasOperation(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("ProductSizeIdWhetherHasOperation", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Judge the ProductSizeId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductSize by Id</returns>
        public static int ProductSizeIdIsExist(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("ProductSizeIdIsExist", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Out to excel of the all data of ProductSize
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_ProductSize()
        {
            return DBHelper.ExecuteDataTable("OutToExcel_ProductSize", CommandType.StoredProcedure);
        }
    }
}
