using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using DAL;
using System.Data;


/*
 * Author:      WangHua
 * FinishDate:  2010-02-02
 * Function:    Report
 */

namespace BLL.other.Company
{
    public class ReportBLL
    {
        /// <summary>
        /// Judage the store whether exists beafore search the information about store by storeId
        /// </summary>
        /// <param name="storeId">StoreId</param>
        /// <returns>Return the counts about store by storeId</returns>
        public static int StoreIdIsExistByStoreId(string storeId)
        {
            return StoreInfoDAL.StoreIdIsExistByStoreId(storeId);
        }

        /// <summary>
        /// Daily日报(1.各区域汇总表(日报),2.各按服务机构汇总表(日报))
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="countryName">CountryName</param>
        /// <param name="dailyDate">DailyDate</param>
        /// <returns></returns>
        public static DataTable DailyByArea(int type, string countryName, DateTime dailyDate)
        {
            return ReportDAL.Daily(type,countryName,dailyDate);
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
            return ReportDAL.Quarterly(type,countryName,year,quarter);
        }

        /// <summary>
        /// XXXX年度各产品销售汇总表
        /// </summary>
        /// <param name="countryName">CountryName</param>
        /// <param name="year">Year</param>
        /// <returns></returns>
        public static DataTable SummaryOfAllSalesByProduct(string countryName, string year)
        {
            return ReportDAL.SummaryOfAllSalesByProduct(countryName,year);
        }

        /// <summary>
        /// XXXX年度各区域产品销售汇总表
        /// </summary>
        /// <param name="countryName">CountryName</param>
        /// <param name="productName">ProductName</param>
        /// <param name="year">Year</param>
        /// <returns></returns>
        public static DataTable SummaryOfAllSalesByArea(string countryName, string productName, string year)
        {
            return ReportDAL.SummaryOfAllSalesByArea(countryName,productName,year);            
        }
    }
}
