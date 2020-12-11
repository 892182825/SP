using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using System.Data;
using System.Data.SqlClient;
using DAL;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-10
 */

namespace BLL.other.Company
{
    public class SetDataOrExpectNumBLL
    {
        /// <summary>
        /// 根据期数更改结算表中的日期
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="expectNum">期数</param>
        /// <returns>返回更改日期所影响的行数</returns>
        public static int UpdDateByExpectNum(string date, int expectNum, string stardate, string enddate)
        {
            return ConfigDAL.UpdDateByExpectNum(date,expectNum,stardate,enddate);
        }        

        /// <summary>
        /// 更改数据字典的值
        /// </summary>
        /// <param name="fileValue">键</param>
        /// <returns>返回更改所影响的行数</returns>
        public static int UpdFileValueByCode(int fileValue)
        {
            return DataDictionaryDAL.UpdFileValueByCode(fileValue);
        }

        /// <summary>
        /// 从结算表中获取所有的日期和期数
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetAllExpectNumDateFromConfig()
        {
            return ConfigDAL.GetAllExpectNumDateFromConfig();
        }


        /// <summary>
        /// 通过期数获取日期
        /// </summary>
        /// <param name="expectNum">期数</param>
        /// <returns>返回日期</returns>
        public static string GetDateByExpectNumFromConfig(int expectNum)
        {
            return ConfigDAL.GetDateByExpectNumFromConfig(expectNum);
        }
    }
}
