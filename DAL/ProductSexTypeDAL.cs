using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using Model;
using System.Data.SqlClient;
using System.Data;

/*
 * Creator：    WangHua
 * ModifyDate： 09/08/31
 * Modifier:    WangHua
 * FinishDate:  2010-01-27   
 * FileName：   ProductSexTypeDAL
 * Function：   Add,Delete,Update,Select product sex type information
 */

namespace DAL
{
    public class ProductSexTypeDAL
    {
        /// <summary>
        /// 向产品适用人群表中插入记录
        /// </summary>
        /// <param name="productSexType">适用人群模型</param>
        /// <returns>返回插入记录所影响的行数</returns>
        public static int AddProductSexType(SqlTransaction tran,ProductSexTypeModel productSexType,out int id)
        {
            int addCount = 0;
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productSexTypeName",SqlDbType.VarChar,50),
                new SqlParameter("@productSexTypeDescr",SqlDbType.VarChar,50),
                new SqlParameter("@identityId",SqlDbType.Int)
            };
            sparams[0].Value = productSexType.ProductSexTypeName;
            sparams[1].Value = productSexType.ProductSexTypeDescr;
            sparams[2].Value = ParameterDirection.Output;
      
            addCount=DBHelper.ExecuteNonQuery(tran,"AddProductSexType", sparams, CommandType.StoredProcedure);
            id = Convert.ToInt32(sparams[2].Value);
            return addCount;
        }

        /// <summary>
        /// Delete the ProductSexType information by productSexTypeId
        /// </summary>
        /// <param name="tran">Transaction</param>
        /// <param name="productSexTypeID">ProductSexTypeId</param>
        /// <returns>Return affected row counts delete the ProductSexType information by productSexTypeId</returns>
        public static int DelProductSexTypeByID(SqlTransaction tran,int productSexTypeID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productSexTypeID",SqlDbType.Int)
            };
            sparams[0].Value = productSexTypeID;            
            return DBHelper.ExecuteNonQuery(tran,"DelProductSexTypeByID",sparams,CommandType.StoredProcedure);
        }

        /// <summary>
        /// 更新指定产品适用人群
        /// </summary>
        /// <param name="productSexType">适用人群模型</param>
        /// <returns>返回更新指定产品适用人群所影响的行数</returns>
        public static int UpdProductSexTypeByID(ProductSexTypeModel productSexType)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productSexTypeID",SqlDbType.Int),
                new SqlParameter("@productSexTypeName",SqlDbType.VarChar,50),
                new SqlParameter("@productSexTypeDescr",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productSexType.ProductSexTypeID;
            sparams[1].Value = productSexType.ProductSexTypeName;
            sparams[2].Value = productSexType.ProductSexTypeDescr;
            
            return DBHelper.ExecuteNonQuery("UpdProductSexTypeByID", sparams, CommandType.StoredProcedure);
        }        

        /// <summary>
        /// 查询产品适用人群
        /// </summary>
        /// <param name="productSexTypeID"></param>
        /// <returns></returns>
        public static ProductSexTypeModel GetProductSexTypeItem(int productSexTypeID)
        {
            string cmd = "select ProductSexTypeID,ProductSexTypeName,ProductSexTypeDescr from ProductSexType where ProductSexTypeID=@ProductSexTypeID";

            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@ProductSexTypeID",productSexTypeID)
            };

            SqlDataReader dr = DBHelper.ExecuteReader(cmd,param,CommandType.Text);

            dr.Read();

            ProductSexTypeModel psm = new ProductSexTypeModel();
            psm.ProductSexTypeID = Convert.ToInt32(dr["ProductSexTypeID"]);
            psm.ProductSexTypeName = dr["ProductSexTypeName"].ToString();
            psm.ProductSexTypeDescr = dr["ProductSexTypeDescr"].ToString();

            dr.Close();

            return psm;
        }

        /// <summary>
        /// 获取指定产品适用人群记录行数
        /// </summary>
        /// <param name="productSexTypeName">产品适用人群名称</param>
        /// <returns>返回获取指定产品适用人群记录行数</returns>
        public static int GetProductSexTypeCountByName(string productSexTypeName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productSexTypeName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productSexTypeName;
            
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetProductSexTypeCountByName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取指定产品适用人群记录行数
        /// </summary>
        /// <param name="productSexTypeID">产品适用人群ID</param>
        /// <param name="productSexTypeName">产品适用人群名称</param>
        /// <returns>返回获取指定产品适用人群记录行数</returns>
        public static int GetProductSexTypeCountByIDName(int productSexTypeID, string productSexTypeName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productSexTypeID",SqlDbType.Int),
                new SqlParameter("@productSexTypeName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productSexTypeID;
            sparams[1].Value = productSexTypeName;            
            
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetProductSexTypeCountByIDName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取适用人群信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductSexTypeInfo()
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@languageCode",SqlDbType.NVarChar,40)
            };
            sparams[0].Value=System.Web.HttpContext.Current.Session["languageCode"];
            return DBHelper.ExecuteDataTable("GetProductSexTypeInfo",sparams,CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取指定适用人群信息
        /// </summary>
        /// <param name="productSexTypeID">产品适用人群ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductSexTypeInfoByID(int productSexTypeID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productSexTypeID",SqlDbType.Int)
            };
            sparams[0].Value = productSexTypeID;
            
            return DBHelper.ExecuteDataTable("GetProductSexTypeInfoByID",sparams,CommandType.StoredProcedure);
        }

        /// <summary>
        /// Judge the ProductSexTypeId whether has operation before delete by Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductSexType by Id</returns>
        public static int ProductSexTypeIdWhetherHasOperation(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("ProductSexTypeIdWhetherHasOperation", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Judge the ProductSexTypeId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return counts</returns>
        public static int ProductSexTypeIdIsExist(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("ProductSexTypeIdIsExist", sparams, CommandType.StoredProcedure));
        }
        
        /// <summary>
        /// Out to excel of the all data of ProductSexType
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_ProductSexType()
        {
            return DBHelper.ExecuteDataTable("OutToExcel_ProductSexType", CommandType.StoredProcedure);            
        }
    }
}
