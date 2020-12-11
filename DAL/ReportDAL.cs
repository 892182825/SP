using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using System.Data;
using System.Data.SqlClient;

/*
 * CreateDate:  2010-05-19
 * FinishDate:  2010-05-19
 * Function:    Report Data Access Layer
 */

namespace DAL
{
    public class ReportDAL
    {
        protected static DataTable dt = new DataTable();

        /// <summary>
        /// Daily日报(1.各区域汇总表(日报),2.各按服务机构汇总表(日报))
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="countryName">CountryName</param>
        /// <param name="dailyDate">DailyDate</param>
        /// <returns></returns>
        public static DataTable Daily(int type, string countryName, DateTime dailyDate)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@CountryName",SqlDbType.NVarChar,40),
                new SqlParameter("@DailyDate",SqlDbType.DateTime)
            };

            sparams[0].Value = countryName;
            sparams[1].Value = dailyDate;

            //各按服务机构汇总表(日报)
            if (type == 1)
            {
                dt = DBHelper.ExecuteDataTable("Pro_DailyByArea", sparams, CommandType.StoredProcedure);
            }

            //各按服务机构汇总表(日报)
            if (type == 2)
            {
                dt = DBHelper.ExecuteDataTable("Pro_DailyByStore", sparams, CommandType.StoredProcedure);
            }

            return dt;
        }

        /// <summary>
        /// Quarterly季报(1.XX年度XX季度各区域销售业绩汇总表,2.XX年度X季度各区域订货业绩及汇款汇总表
        /// 3.XX年度X季度各店铺报单业绩汇总表,4.XX年度X季度各店铺订货业绩及汇款汇总表)        
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="countryName">CountryName</param>
        /// <param name="year">Year</param>
        /// <param name="quarter">Quarter</param>
        /// <returns></returns>
        public static DataTable Quarterly(int type, string countryName, string year, string quarter)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@countryName",SqlDbType.NVarChar,40),
                new SqlParameter("@year",SqlDbType.NVarChar,40),
                new SqlParameter("@quarter",SqlDbType.NVarChar,40)
            };

            sparams[0].Value = countryName;
            sparams[1].Value = year;
            sparams[2].Value = quarter;

            //XX年度XX季度各区域销售业绩汇总表
            if (type == 1)
            {
                dt = DBHelper.ExecuteDataTable("Pro_AnnualSummaryOfQuarterlyRegionalSales", sparams, CommandType.StoredProcedure);
            }

            //XX年度X季度各区域订货业绩及汇款汇总表
            else if (type == 2)
            {
                dt = DBHelper.ExecuteDataTable("Pro_AnnualQuarterlySummaryOfTheRegionalOrderAndRemittance", sparams, CommandType.StoredProcedure);
            }

            //XX年度X季度各店铺报单业绩汇总表
            else if (type == 3)
            {
                dt = DBHelper.ExecuteDataTable("Pro_AnnualDeclarationsOfEachQuarterAllStores", sparams, CommandType.StoredProcedure);
            }

            //XX年度X季度各店铺订货业绩及汇款汇总表
            else
            {
                dt = DBHelper.ExecuteDataTable("Pro_QuarterlySummaryOfAllShopOrdersAndRemittances", sparams, CommandType.StoredProcedure);
            }

            return dt;
        }

        /// <summary>
        /// XXXX年度各产品销售汇总表
        /// </summary>
        /// <param name="countryName">CountryName</param>
        /// <param name="year">Year</param>
        /// <returns></returns>
        public static DataTable SummaryOfAllSalesByProduct(string countryName, string year)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@countryName",SqlDbType.NVarChar,40),
                new SqlParameter("@year",SqlDbType.VarChar,40)
            };

            sparams[0].Value = countryName;
            sparams[1].Value = year;

            return DBHelper.ExecuteDataTable("Pro_SummaryOfAllSalesByProduct", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// XXXX年度各区域产品销售汇总表
        /// </summary>
        /// <param name="countryName">CountryName</param>
        /// <param name="productName">ProductName</param>
        /// <param name="year">Year</param>
        /// <returns></returns>
        public static DataTable SummaryOfAllSalesByArea(string countryName,string productName,string year)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@countryName",SqlDbType.NVarChar,40),
                new SqlParameter("@productName",SqlDbType.NVarChar,40),
                new SqlParameter("@year",SqlDbType.NVarChar,20)
            };

            sparams[0].Value = countryName;
            sparams[1].Value = productName;
            sparams[2].Value = year;

            return DBHelper.ExecuteDataTable("Pro_SummaryOfAllSalesByArea", sparams, CommandType.StoredProcedure);
        }
    }
}
