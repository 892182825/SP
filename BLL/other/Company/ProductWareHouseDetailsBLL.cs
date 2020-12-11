using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using DAL;
using System.Data;

/*
 * 创建者：  汪  华
 * 创建时间：2009-10-27
 * 完成时间：209-10-27
 * 对应菜单：公司->报表中心->产品各仓库明细
 */

namespace BLL.other.Company
{
    public class ProductWareHouseDetailsBLL
    {
        /// <summary>
        /// 通过产品编码获取产品编码的行数
        /// </summary>
        /// <param name="productCode">产品编码</param>
        /// <returns>返回产品编码的行数</returns>
        public static int GetProductCodeCountByProductCode(string productCode)
        {
            return ProductQuantityDAL.GetProductCodeCountByProductCode(productCode);
        }

        /// <summary>
        /// 通过产品编码联合查询获取指定产品所在仓库的产品信息
        /// Get more product information of the WareHouse by ProductCode 
        /// </summary>
        /// <param name="productCode">productCode</param>
        /// <returns>Return DataTable object</returns>
        public static DataTable GetMoreWareHouseProductInfoByProductCode(string productCode)
        {
            return ProductDAL.GetMoreWareHouseProductInfoByProductCode(productCode);            
        }

        /// <summary>
        /// 通过产品编码联合查询获取指定产品所在店铺里的产品信息
        /// Get more product information of the store by ProductCode
        /// </summary>
        /// <param name="productCode">productCode</param>
        /// <returns>Return DataTable object</returns>
        public static DataTable GetMoreStoreProductInfoByProductCode(string productCode)
        {
            return ProductDAL.GetMoreStoreProductInfoByProductCode(productCode);           
        }
    }
}
