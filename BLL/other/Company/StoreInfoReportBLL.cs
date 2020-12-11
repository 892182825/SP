using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using DAL;
using System.Data;

/*
 * 创建者：  汪  华
 * 创建时间：2009-11-03
 * 完成时间：2009-11-03
 * 对应菜单：公司子系统->报表中心->店铺名单
*/

namespace BLL.other.Company
{
    public class StoreInfoReportBLL
    {
        /// <summary>
        /// 获取指定时间内的店铺信息
        /// </summary>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">截止时间</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetStoreInfoByDate(DateTime beginDate, DateTime endDate)
        {
 //           return StoreInfoDAL.GetStoreInfoByDate(beginDate, endDate);
            return null;
        }
    }
}
