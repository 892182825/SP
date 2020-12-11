using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespce
using System.Data;
using System.Data.SqlClient;
using Model;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-02
 * 文件名：     ProductCombineDetailDAL
 * 功能：       对组合产品的增删改查
 */

namespace DAL
{
    public class ProductCombineDetailDAL
    {
        /// <summary>
        /// 向组合产品中插入相关信息
        /// </summary>
        /// <param name="proCombineDetail">组合产品参数</param>
        /// <returns>返回插入组合产品所影响的行数</returns>
        public static int AddCombineProduct(ProductCombineDetailModel proCombineDetail)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@combineProductID",SqlDbType.Int),
                new SqlParameter("@subProductID",SqlDbType.Int),
                new SqlParameter("@quantity",SqlDbType.Int)
            };

            sparams[0].Value = proCombineDetail.CombineProductID;
            sparams[1].Value = proCombineDetail.SubProductID;
            sparams[2].Value = proCombineDetail.Quantity;

            return DBHelper.ExecuteNonQuery("AddCombineProduct", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 根据产品编号，获取组合方式
        /// </summary>
        /// <param name="proid">组合产品ID</param>
        /// <returns></returns>
        public static IList<ProductCombineDetailModel> GetCombineDetil(int proid)
        {
            SqlParameter[] paras = new SqlParameter[] 
            {
                new SqlParameter("@combineProductID",SqlDbType.Int, 4)
            };
            paras[0].Value = proid;

            DataTable dt = DBHelper.ExecuteDataTable("select a.*,b.PreferentialPrice,b.PreferentialPV from ProductCombineDetail a join Product b on a.SubProductID=b.ProductID where a.CombineProductID=@combineProductID", paras, CommandType.Text);
            IList<ProductCombineDetailModel> comDetails = new List<ProductCombineDetailModel>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductCombineDetailModel comDetail = new ProductCombineDetailModel();
                comDetail.ID = Convert.ToInt32(dr["ID"]);
                comDetail.CombineProductID = Convert.ToInt32(dr["CombineProductID"]);
                comDetail.SubProductID = Convert.ToInt32(dr["SubProductID"]);
                comDetail.Quantity = Convert.ToInt32(dr["Quantity"]);
                comDetail.UnitPrice = Convert.ToInt32(dr["PreferentialPrice"]);
                comDetail.PV = Convert.ToInt32(dr["PreferentialPV"]);
                comDetail.Remark = Convert.ToString(dr["remark"]);
                comDetails.Add(comDetail);
            }
            return comDetails;
        }

        /// <summary>
        /// 删除组合产品信息
        /// </summary>
        /// <param name="combineProductID">组合产品ID</param>
        /// <returns>返回删除组合产品信息所影响的行数</returns>
        public static int DelCombineProductInfoByID(int combineProductID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@combineProductID",SqlDbType.Int)
            };
            sparams[0].Value = combineProductID;

            return DBHelper.ExecuteNonQuery("DelCombineProductInfoByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取组合产品信息
        /// </summary>
        /// <param name="countryCode">国家编码</param>
        /// <returns></returns>
        public static DataTable GetCombineProductInfoByCountryCode(string countryCode)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@countryCode",SqlDbType.NVarChar,10)
            };
            sparams[0].Value = countryCode;

            return DBHelper.ExecuteDataTable("GetCombineProductInfoByCountryCode", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 联合查询获取更多的信息
        /// </summary>
        /// <param name="combineProductID">混合产品ID，对应Product表中的ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMoreCombineProductInfoByID(int combineProductID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@combineProductID",SqlDbType.Int)
            };
            sparams[0].Value = combineProductID;

            return DBHelper.ExecuteDataTable("GetMoreCombineProductInfoByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取组合产品列表
        /// </summary>
        /// <param name="combineProductID">组合产品ID</param>
        /// <param name="countryID">国家ID</param>
        /// <returns></returns>
        public static DataTable GetCombineProductDetailList(int combineProductID, int countryID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@combineProductID",SqlDbType.Int),
                new SqlParameter("@countryID",SqlDbType.Int)
            };

            sparams[0].Value = combineProductID;
            sparams[1].Value = countryID;

            return DBHelper.ExecuteDataTable("GetCombineProductDetail", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 检查组合产品是否可以修改
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>当可以修改组合产品时，则返回真。否则返回假</returns>
        public static bool CheckWheatherChangeCombineProduct(int productID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productID",SqlDbType.Int)
            };

            sparams[0].Value = productID;
            int rowCount = 0;
            rowCount = (int)DBHelper.ExecuteScalar("CheckWheatherChangeCombineProduct", sparams, CommandType.StoredProcedure);
            if (rowCount > 0)
            {
                return false;
            }

            else
            {
                return true;
            }
        }
    }
}