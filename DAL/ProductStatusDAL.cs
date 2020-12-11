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
 * FileName：   ProductStatusDAL
 * Function：   Add,Delete,Update,Select product status information
 */

namespace DAL
{
    public class ProductStatusDAL
    {
        /// <summary>
        /// 向产品状态表中插入记录
        /// </summary>
        /// <param name="productStatus">产品状态模型</param>
        /// <returns>返回向产品状态表中插入记录所影响的行数</returns>
        public static int AddProductStatus(SqlTransaction tran,ProductStatusModel productStatus,out int id)
        {
            int addCount = 0;
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productStatusName",SqlDbType.VarChar,50),
                new SqlParameter("@productStatusDescr",SqlDbType.VarChar,50),
                new SqlParameter("@identityId",SqlDbType.Int)
            };
            sparams[0].Value = productStatus.ProductStatusName;
            sparams[1].Value = productStatus.ProductStatusDescr;
            sparams[2].Value = ParameterDirection.Output;

            addCount=DBHelper.ExecuteNonQuery("AddProductStatus", sparams, CommandType.StoredProcedure);
            id = Convert.ToInt32(sparams[2].Value);
            return addCount;
        }

        /// <summary>
        /// Delete the ProductStatus information by productStatusId
        /// </summary>
        /// <param name="tran">Transaction</param>
        /// <param name="productStatusID">ProductStatusId</param>
        /// <returns>Return affeted counts which delete the ProductStatus information by productStatusId</returns>
        public static int DelProductStatusByID(SqlTransaction tran,int productStatusID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productStatusID",SqlDbType.Int)
            };
            sparams[0].Value = productStatusID;            
            return DBHelper.ExecuteNonQuery(tran,"DelProductStatusByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 更新指定产品状态信息
        /// </summary>
        /// <param name="productStatus">产品状态模型</param>
        /// <returns>返回更新指定产品状态信息所影响的行数</returns>
        public static int UpdProductStatusByID(ProductStatusModel productStatus)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productStatusID",SqlDbType.Int),
                new SqlParameter("@productStatusName",SqlDbType.VarChar,50),
                new SqlParameter("@productStatusDescr",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productStatus.ProductStatusID;
            sparams[1].Value = productStatus.ProductStatusName;
            sparams[2].Value = productStatus.ProductStatusDescr;
            
            return DBHelper.ExecuteNonQuery("UpdProductStatusByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 查询产品状态
        /// </summary>
        /// <param name="productStatusID"></param>
        /// <returns></returns>
        public static ProductStatusModel GetProductStatusItem(int productStatusID)
        {
            string cmd = "select ProductStatusID,ProductStatusName,ProductStatusDescr from ProductStatus where ProductStatusID=@ProductStatusID";

            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@ProductStatusID",productStatusID)
            };

            SqlDataReader dr = DBHelper.ExecuteReader(cmd,param,CommandType.Text);

            dr.Read();

            ProductStatusModel psm = new ProductStatusModel();
            psm.ProductStatusID = Convert.ToInt32(dr["ProductStatusID"]);
            psm.ProductStatusName = dr["ProductStatusName"].ToString();
            psm.ProductStatusDescr = dr["ProductStatusDescr"].ToString();

            dr.Close();

            return psm;
        }

        /// <summary>
        /// 获取指定产品状态的行数
        /// </summary>
        /// <param name="productStatusName">产品状态名称</param>
        /// <returns>返回获取指定产品状态的行数</returns>
        public static int GetProductStatusCountByName(string productStatusName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productStatusName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productStatusName;
            
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetProductStatusCountByName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取指定产品状态的行数
        /// </summary>
        /// <param name="productStatusID">产品状态ID</param>
        /// <param name="productStatusName">产品状态名称</param>
        /// <returns>返回获取指定产品状态的行数</returns>
        public static int GetProductStatusCountByIDName(int productStatusID, string productStatusName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productStatusID",SqlDbType.Int),
                new SqlParameter("@productStatusName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = productStatusID;
            sparams[1].Value = productStatusName;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetProductStatusCountByIDName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取产品状态信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductStatusInfo()
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@languageCode",SqlDbType.NVarChar,40)
            };
            sparams[0].Value = System.Web.HttpContext.Current.Session["languageCode"];

            return DBHelper.ExecuteDataTable("GetProductStatusInfo",sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取指定产品状态信息
        /// </summary>
        /// <param name="productStatusID">产品你状态ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductStatusInfoByID(int productStatusID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productStatusID",SqlDbType.Int)
            };
            sparams[0].Value = productStatusID;
            
            return DBHelper.ExecuteDataTable("GetProductStatusInfoByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Judge the ProductStatusId whether has operation before delete by Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductStatus by Id</returns>
        public static int ProductStatusIdWhetherHasOperation(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("ProductStatusIdWhetherHasOperation", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Judge the ProductStatusId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductStatus by Id</returns>
        public static int ProductStatusIdIsExist(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("ProductStatusIdIsExist", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Out to excel the all data of ProductStatus 
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_ProductStatus()
        {
            return DBHelper.ExecuteDataTable("OutToExcel_ProductStatus", CommandType.StoredProcedure);
        }
    }
}
