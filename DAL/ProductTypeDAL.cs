using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespce
using Model;
using System.Data;
using System.Data.SqlClient;

/*
 * Creator：    WangHua
 * ModifyDate： 09/08/31
 * Modifier:    WangHua
 * FinishDate:  2010-01-27   
 * FileName：   ProductUnitDAL
 * Function：   Add,Delete,Update,Select product type information
 */

namespace DAL
{
    /// <summary>
    /// 产品型号
    /// </summary>
    public class ProductTypeDAL
    {
        /// <summary>
        /// 向产品型号表中插入相关记录
        /// </summary>
        /// <param name="productType">产品型号模型</param>
        /// <returns>返回向产品型号表中插入相关记录所影响的行数</returns>
        public static int AddProductType(ProductTypeModel productType)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productTypeName",SqlDbType.VarChar,50),
                new SqlParameter("@productTypeDescr",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productType.ProductTypeName;
            sparams[1].Value = productType.ProductTypeDescr;
            
            return DBHelper.ExecuteNonQuery("AddProductType", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 删除指定产品型号信息
        /// </summary>
        /// <param name="productTypeID">产品型号ID</param>
        /// <returns>返回删除指定产品型号信息所影响的行数</returns>
        public static int DelProductTypeByID(int productTypeID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productTypeID",SqlDbType.Int)
            };
            sparams[0].Value = productTypeID;
            
            return DBHelper.ExecuteNonQuery("DelProductTypeByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 更新指定产品模型信息
        /// </summary>
        /// <param name="productType">产品型号模型</param>
        /// <returns>返回更新指定产品模型信息所影响的行数</returns>
        public static int UpdProductTypeByID(ProductTypeModel productType)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productTypeID",SqlDbType.Int),
                new SqlParameter("@productTypeName",SqlDbType.VarChar,50),
                new SqlParameter("@productTypeDescr",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productType.ProductTypeID;
            sparams[1].Value = productType.ProductTypeName;
            sparams[2].Value = productType.ProductTypeDescr;
            
            return DBHelper.ExecuteNonQuery("UpdProductTypeByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取指定产品型号行数
        /// </summary>
        /// <param name="productTypeName">产品型号名称</param>
        /// <returns>返回获取指定产品型号行数</returns>
        public static int GetProductTypeCountByName(string productTypeName)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productTypeName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productTypeName;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetProductTypeCountByName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取指定产品型号行数
        /// </summary>
        /// <param name="productTypeID">产品型号ID</param>
        /// <param name="productTypeName">产品型号名称</param>
        /// <returns>返回获取指定产品型号行数</returns>
        public static int GetProductTypeCountByIDName(int productTypeID, string productTypeName)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productTypeID",SqlDbType.Int),
                new SqlParameter("@productTypeName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productTypeID;
            sparams[1].Value = productTypeName;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetProductTypeCountByIDName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取产品类型信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductTypeInfo()
        {
            return DBHelper.ExecuteDataTable("GetProductTypeInfo", CommandType.StoredProcedure);           
        }

        /// <summary>
        /// 获取指定产品类型信息
        /// </summary>
        /// <param name="productTypeID">产品型号ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductTypeInfoByID(int productTypeID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productTypeID",SqlDbType.Int)
            };
            sparams[0].Value = productTypeID;
            
            return DBHelper.ExecuteDataTable("GetProductTypeInfoByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取产品型号名称
        /// </summary>
        /// <param name="productTypeID">产品型号ID</param>
        /// <returns>返回产品型号名称</returns>
        public static string GetProductTypeNameByID(int productTypeID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productTypeID",SqlDbType.Int)
            };
            
            return Convert.ToString(DBHelper.ExecuteScalar("GetProductTypeNameByID", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Judge the ProductTypeId whether has operation before delete by Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductType by Id</returns>
        public static int ProductTypeIdWhetherHasOperation(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("ProductTypeIdWhetherHasOperation", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Judge the ProductTypeId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductType by Id</returns>
        public static int ProductTypeIdIsExist(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("ProductTypeIdIsExist", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Out to excel the all data of ProductType
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_ProductType()
        {
            return DBHelper.ExecuteDataTable("OutToExcel_ProductType", CommandType.StoredProcedure);
        }
    }
}
