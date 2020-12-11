using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using System.Data;
using DAL;

/*
 * 创建者：  汪  华
 * 创建时间：2009-10-23
 * 完成时间：2009-10-23
 * 对应菜单：公司->报表中心->产品明细
 */

namespace BLL.other.Company
{
    public class ProductDetailsBLL
    {
        /// <summary>
        /// 通过联合查询获取产品详细信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMoreProductDetailsInfo()
        {
            return ProductDAL.GetMoreProductDetailsInfo();
        }

        /// <summary>
        /// Out to excle the data of ProductDetailsInfo 
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_ProductDetailsInfo()
        {
            return ProductDAL.OutToExcel_ProductDetailsInfo();
        }
    }
}
