using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using System.Data;
using System.Data.SqlClient;
using Model;

/*
 * Creator：    WangHua
 * ModifyDate： 09/08/31
 * Modifier:    WangHua
 * FinishDate:  2010-01-27   
 * FileName：   ProductColorDAL
 * Function：   Add,Delete,Update,Select product color information
 */

namespace DAL
{
    /// <summary>
    /// 产品颜色表数据层
    /// </summary>
    public class ProductColorDAL
    {
        /// <summary>
        /// 添加产品颜色
        /// </summary>
        /// <param name="productColor">产品颜色模型</param>
        /// <returns>返回添加产品颜色所影响的行数</returns>
        public static int AddProductColor(SqlTransaction tran,ProductColorModel productColor,out int id)
        {
            int addCount = 0;
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productColorName",SqlDbType.VarChar,50),
                new SqlParameter("@productColorDescr",SqlDbType.VarChar,50),
                new SqlParameter("@identityID",SqlDbType.Int)
            };
            sparams[0].Value = productColor.ProductColorName;
            sparams[1].Value = productColor.ProductColorDescr;
            sparams[2].Direction = ParameterDirection.Output;

            addCount=DBHelper.ExecuteNonQuery(tran,"AddProductColor", sparams, CommandType.StoredProcedure);
            id=Convert.ToInt32(sparams[2].Value);
            return addCount;
        }

        /// <summary>
        /// 添加产品颜色
        /// </summary>
        /// <param name="productColor">产品颜色模型</param>
        /// <returns>返回添加产品颜色所影响的行数</returns>
        public static int AddProductColor(ProductColorModel productColor, out int id)
        {
            int addCount = 0;
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productColorName",SqlDbType.VarChar,50),
                new SqlParameter("@productColorDescr",SqlDbType.VarChar,50),
                new SqlParameter("@identityID",SqlDbType.Int)
            };
            sparams[0].Value = productColor.ProductColorName;
            sparams[1].Value = productColor.ProductColorDescr;
            sparams[2].Direction = ParameterDirection.Output;

            addCount = DBHelper.ExecuteNonQuery("AddProductColor", sparams, CommandType.StoredProcedure);
            id = Convert.ToInt32(sparams[2].Value);
            return addCount;
        }

        /// <summary>
        /// Delete the ProductColor information by productColorId
        /// </summary>
        /// <param name="tran">Transaction</param>
        /// <param name="productColorID">ProductColorId</param>
        /// <returns>Return affected row counts which delete the ProductColor information by productColorId</returns>
        public static int DelProductColorByID(SqlTransaction tran,int productColorID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productColorID",SqlDbType.Int)
            };
            sparams[0].Value = productColorID;            
            return DBHelper.ExecuteNonQuery(tran,"DelProductColorByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 修改指定产品颜色信息
        /// </summary>
        /// <param name="productColor">产品颜色模型</param>
        /// <returns>返回修改指定产品颜色信息所影响的行数</returns>
        public static int UpdProductColorByID(SqlTransaction tran, ProductColorModel productColor)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productColorID",SqlDbType.Int),
                new SqlParameter("@productColorName",SqlDbType.VarChar,50),
                new SqlParameter("@productColorDescr",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productColor.ProductColorID;
            sparams[1].Value = productColor.ProductColorName;
            sparams[2].Value = productColor.ProductColorDescr;
            
            return DBHelper.ExecuteNonQuery(tran,"UpdProductColorByID", sparams, CommandType.StoredProcedure);
        }


        /// <summary>
        /// 获取产品颜色信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductColorInfo()
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@languageCode",SqlDbType.NVarChar,40)
            };
            sparams[0].Value = System.Web.HttpContext.Current.Session["languageCode"];
            return DBHelper.ExecuteDataTable("GetProductColorInfo",sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取指定的产品颜色信息
        /// </summary>
        /// <param name="productColorID">产品颜色ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductColorInfoByID(int productColorID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productColorID",SqlDbType.Int)
            };
            sparams[0].Value = productColorID;            

            return DBHelper.ExecuteDataTable("GetProductColorInfoByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取指定产品名称的行数
        /// </summary>
        /// <param name="productColorName">产品颜色名称</param>
        /// <returns>返回指定产品名称的行数</returns>
        public static int GetProductColorCountByName(string productColorName)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productColorName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productColorName;
            
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetProductColorCountByName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取指定产品名称的行数
        /// </summary>
        /// <param name="productColorID">产品颜色ID</param>
        /// <param name="productColorName">产品颜色名称</param>
        /// <returns>返回指定产品名称的行数</returns>
        public static int GetProductColorCountByIDName(int productColorID, string productColorName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productColorID",SqlDbType.Int),
                new SqlParameter("@productColorName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productColorID;
            sparams[1].Value = productColorName;
            
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetProductColorCountByIDName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Juage the ProductColorID whether has operation before delete
        /// </summary>
        /// <param name="productColorID">ProductColorID</param>
        /// <returns>Return counts</returns>
        public static int ProductColorIDWhetherHasOperation(int productColorID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productColorID",SqlDbType.Int)
            };
            sparams[0].Value = productColorID;

            return Convert.ToInt32(DBHelper.ExecuteScalar("ProductColorIDWhetherHasOperation",sparams,CommandType.StoredProcedure));
        }

        /// <summary>
        /// Judge the ProductColorID whether exist by ProductColorID before delete or update
        /// </summary>
        /// <param name="productColorID">ProductColorID</param>
        /// <returns>Return counts</returns>
        public static int ProductColorIDIsExist(int productColorID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productColorID",SqlDbType.Int)
            };
            sparams[0].Value = productColorID;

            return Convert.ToInt32(DBHelper.ExecuteScalar("ProductColorIDIsExist", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Out to excel the all data of ProductColor
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_ProductColor()
        {
            return DBHelper.ExecuteDataTable("OutToExcel_ProductColor", CommandType.StoredProcedure);
        }
    }
}
