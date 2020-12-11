using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using System.Web.UI.WebControls;
using System.Data;
using Model;
using DAL;


/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-08
 * 对应菜单：   库存管理—>组合产品
 */

namespace BLL.other.Company
{
    public class CombineProductBLL
    {
        /// <summary>
        /// 向组合产品中插入相关信息
        /// </summary>
        /// <param name="proCombineDetail">组合产品参数</param>
        /// <returns>返回插入组合产品所影响的行数</returns>
        public static int AddCombineProduct(ProductCombineDetailModel proCombineDetail)
        {
            return ProductCombineDetailDAL.AddCombineProduct(proCombineDetail);
        }

        /// <summary>
        /// 绑定货币(货币名称 -- 汇率ID )列表
        /// </summary>
        /// <param name="list">要添加货币的控件</param>
        /// <param name="defaultCurrency">默认的币种名称</param>
        public static void BindCurrencyList(DropDownList list, string defaultCurrency)
        {
            CurrencyDAL.BindCurrencyList(list,defaultCurrency);
        }

        /// <summary>
        /// 获取国家ID,编码和国家名称
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetCountryIDCodeNameOrderByID()
        {
            return CountryDAL.GetCountryIDCodeNameOrderByID();
        }

        /// <summary>
        /// 获取组合产品信息
        /// </summary>
        /// <param name="countryCode">国家编码</param>
        /// <returns></returns>
        public static DataTable GetCombineProductInfoByCountryCode(string countryCode)
        {
            return ProductCombineDetailDAL.GetCombineProductInfoByCountryCode(countryCode);           
        }

        /// <summary>
        /// 通过联合查询获取信息
        /// </summary>
        /// <param name="productCode">产品编码</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMoreProductInfoByProductCode(string productCode)
        {
            return ProductUnitDAL.GetMoreProductInfoByProductCode(productCode);
        }
        

        /// <summary>
        /// 联合查询获取更多的信息
        /// </summary>
        /// <param name="combineProductID">混合产品ID，对应Product表中的ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMoreCombineProductInfoByID(int combineProductID)
        {
            return ProductCombineDetailDAL.GetMoreCombineProductInfoByID(combineProductID);
        }

        /// <summary>
        /// 删除组合产品信息
        /// </summary>
        /// <param name="combineProductID">组合产品ID</param>
        /// <returns>返回删除组合产品信息所影响的行数</returns>
        public static int DelCombineProductInfoByID(int combineProductID)
        {
            return ProductCombineDetailDAL.DelCombineProductInfoByID(combineProductID);
        }

        /// <summary>
        /// 检查组合产品是否可以修改
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>当可以修改组合产品时，则返回真。否则返回假</returns>
        public static bool CheckWheatherChangeCombineProduct(int productID)
        {
            return ProductCombineDetailDAL.CheckWheatherChangeCombineProduct(productID);
        }
    }
}
