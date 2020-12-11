using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Model;
using DAL;

/*
 * 创建者：     汪华
 * 创建日期：   2009-08-31
 * 文件名：     AddNewProductBLL
 * 功能：       对产品进行相关操作
 */

namespace BLL.other.Company
{
    public class AddNewProductBLL
    {

        /// <summary>
        /// 添加新品返回ID  ---DS2012
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns>返回添加新品所影响的行数</returns>
        public static int AddProduct(SqlTransaction tran, ProductModel productModel)
        {
            return ProductDAL.AddProductItem(tran, productModel);
        }

        /// <summary>
        /// 添加新类
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns>返回添加新类所影响的行数</returns>
        public static int AddProductKind(ProductModel productModel)
        {
            return ProductDAL.AddProductKind(productModel);
        }

        /// <summary>
        /// 向产品名称等翻译表插入相关记录
        /// </summary>
        /// <param name="languageTrans">产品名称等翻译表模型</param>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddLanguageTrans(LanguageTransModel languageTrans)
        {
            return LanguageTransDAL.AddLanguageTrans(languageTrans);
        }

        /// <summary>
        /// 向产品名称等翻译表插入相关记录
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="languageTrans">产品名称等翻译表模型</param>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddLanguageTrans(SqlTransaction tran, LanguageTransModel languageTrans)
        {
            return LanguageTransDAL.AddLanguageTrans(tran, languageTrans);
        }

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns>返回更新产品所影响的行数</returns>
        public static int UpdProduct(ProductModel productModel)
        {
            return ProductDAL.UpdProductItem(productModel);
        }

        /// <summary>
        /// 更新产品类
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns>返回更新产品类所影响的行数</returns>
        public static int UpdProductKind(ProductModel productModel)
        {
            return ProductDAL.UpdProductKind(productModel);
        }

        /// <summary>
        /// 删除产品  ---DS2012
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>返回删除产品所影响的行数</returns>
        public static int DelProductByID(SqlTransaction tran, int productID)
        {
            return ProductDAL.DelProductByID(tran, productID);
        }

        /// <summary>
        /// 删除产品类   ---DS2012
        /// </summary>
        /// <param name="tran">要处理的事务</param>
        /// <param name="productListID">产品类及类所属的产品ID</param>
        /// <returns>返回删除产品类所影响的行数</returns>
        public static int DelProductKindByID(SqlTransaction tran, string productListID)
        {
            return ProductDAL.DelProductKindByID(tran, productListID);
        }

        /// <summary>
        /// 删除产品库存表中指定的记录
        /// </summary>
        /// <param name="tran">要处理的事务</param>
        /// <param name="productID">产品ID</param>
        /// <returns>返回删除产品库存表中指定的记录所影响的行数</returns>
        public static int DelProductQuantityByProductID(SqlTransaction tran, int productID)
        {
            return ProductQuantityDAL.DelProductQuantityByProductID(tran, productID);
        }

        /// <summary>
        /// 根据原表中的ID删除指定记录  ---DS2012
        /// </summary>
        /// <param name="tran">要处理的事务</param>
        /// <param name="oldID">原表中的ID</param>
        /// <returns>返回删除指定记录所影响的行数</returns>
        public static int DelLanguageTransByOldID(SqlTransaction tran, int oldID)
        {
            return LanguageTransDAL.DelLanguageTransByOldID(tran, oldID);
        }

        /// <summary>
        /// 查询产品
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns></returns>
        public static ProductModel GetProductItem(int productID)
        {
            return ProductDAL.GetProductItem(productID);
        }

        /// <summary>
        /// 根据产品ID获取指定的所有产品信息
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>返回集合</returns>
        public static IList<ProductModel> GetAllProductInfoByID(int productID)
        {
            return ProductDAL.GetAllProductInfoByID(productID);
        }

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProductList()
        {
            return ProductDAL.GetProductList();
        }

        /// <summary>
        /// 获取不包含组合产品的列表
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetNoIncluedCombineProductList()
        {
            return ProductDAL.GetNoIncluedCombineProductList();
        }

        /// <summary>
        /// 通过产品ID获取国家编码
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>返回国家编码</returns>
        public static string GetCountryCodeByProductID(int productID)
        {
            return ProductDAL.GetCountryCodeByProductID(productID);
        }

        /// <summary>
        /// 判断产品名称(产品类名称)是否重复  ---DS2012
        /// </summary>
        /// <param name="productName">产品名称</param>
        /// <returns></returns>
        public static bool ProductNameIsExist(string productName)
        {
            return ProductDAL.GetProductName(productName);
        }

        /// <summary>   
        /// 获取产品描述
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>返回产品说明</returns>
        public static string GetDescriptionByID(int productID)
        {
            return ProductDAL.GetDescriptionByID(productID);
        }

        /// <summary>
        /// 通过产品编码获取产品信息
        /// </summary>
        /// <param name="productCode">产品编码</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductInfoByProductCode(string productCode)
        {
            return ProductDAL.GetProductInfoByProductCode(productCode);
        }

        /// <summary>
        /// 通过产品ID获取价格PV信息
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetPricePVByID(int productID)
        {
            return ProductDAL.GetPricePVByID(productID);
        }

        /// <summary>
        /// 通过产品编码获取行数
        /// </summary>
        /// <param name="productCode">产品编码</param>
        /// <returns>返回所获取的行数</returns>
        public static int GetCountByProductCode(string productCode)
        {
            return ProductDAL.GetCountByProductCode(productCode);
        }

        /// <summary>
        /// 获取新产品信息   ---DS20112
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetNewProductInfo()
        {
            return ProductDAL.GetNewProductInfo();
        }

        /// <summary>
        /// 获取行数
        /// </summary>
        /// <param name="languageID">语言ID</param>
        /// <returns>返回行数</returns>
        public static int GetLanguageTransCountByID(int languageID)
        {
            return LanguageTransDAL.GetLanguageTransCountByID(languageID);
        }

        /// <summary>
        /// 判断产品编码是否重复  ---DS2012
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns>返回产品编码的数目</returns>
        public static int CheckProductCodeIsExist(string productCode)
        {
            return ProductDAL.CheckProductCodeIsExist(productCode);
        }

        /// <summary>
        /// 通过产品ID和产品名称判断产品编码是否重复  ---DS2012
        /// </summary>
        /// <param name="productName">产品名称</param>
        /// <param name="productID">产品ID</param>
        /// <returns>返回产品编码的数目</returns>
        public static int CheckProductCodeIsExistByID(string productName, int productID)
        {
            return ProductDAL.CheckProductCodeIsExistByID(productName, productID);
        }

        /// <summary>
        /// 获取产品型号名称
        /// </summary>
        /// <param name="productTypeID">产品型号ID</param>
        /// <returns>返回产品型号名称</returns>
        public static string GetProductTypeNameByID(int productTypeID)
        {
            return ProductTypeDAL.GetProductTypeNameByID(productTypeID);
        }

        /// <summary>
        /// 获取产品规格名称
        /// </summary>
        /// <param name="productSpecID">产品规格ID</param>
        /// <returns>返回产品规格名称</returns>
        public static string GetProductSpecNameByID(int productSpecID)
        {
            return ProductSpecDAL.GetProductSpecNameByID(productSpecID);
        }

        /// <summary>
        /// 获取产品规格信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductSpecInfo()
        {
            return ProductSpecDAL.GetProductSpecInfo();
        }

        /// <summary>
        /// 获取产品类型信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductTypeInfo()
        {
            return ProductTypeDAL.GetProductTypeInfo();
        }

        /// <summary>
        /// 获取产品单位信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductUnitInfo()
        {
            return ProductUnitDAL.GetProductUnitInfo();
        }

        /// <summary>
        /// 获取产品颜色信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductColorInfo()
        {
            return ProductColorDAL.GetProductColorInfo();
        }

        /// <summary>
        /// 获取产品状态信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductStatusInfo()
        {
            return ProductStatusDAL.GetProductStatusInfo();
        }

        /// <summary>
        /// 获取产品尺寸信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public DataTable GetProductSizeInfo()
        {
            return ProductSizeDAL.GetProductSizeInfo();
        }

        /// <summary>
        /// 获取适用人群信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductSexTypeInfo()
        {
            return ProductSexTypeDAL.GetProductSexTypeInfo();
        }

        /// <summary>
        /// 获取组合产品列表
        /// </summary>
        /// <param name="combineProductID">组合产品ID</param>
        /// <param name="countryID">国家ID</param>
        /// <returns></returns>
        public static DataTable GetCombineProductDetailList(int combineProductID, int countryID)
        {
            return ProductCombineDetailDAL.GetCombineProductDetailList(combineProductID, countryID);
        }

        /// <summary>
        /// 获取语言对应的ID
        /// </summary>
        /// <param name="name">语言名称</param>
        /// <returns>返回语言所对应的ID</returns>
        public static int GetLanguageIDByName(string name)
        {
            return LanguageDAL.GetLanguageIDByName(name);
        }

        /// <summary>
        /// 通过语言ID获取ID
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetLanguageIDByID()
        {
            return LanguageDAL.GetLanguageIDByID();
        }

        /// <summary>
        /// 通过国家名称获取国家ID
        /// </summary>
        /// <param name="name">国家名称</param>
        /// <returns>返回国家ID</returns>
        public static int GetCountryIDByName(string name)
        {
            return CountryDAL.GetCountryIDByName(name);
        }

        /// <summary>
        /// 通过国家编码获取国家名称   ---DS2012
        /// </summary>
        /// <param name="countryID">国家ID</param>
        /// <returns>返回国家名称</returns>
        public static string GetCountryNameByCountryCode(string countryCode)
        {
            return CountryDAL.GetCountryNameByCountryCode(countryCode);
        }

        /// <summary>
        /// 返回国家信息表
        /// </summary>
        /// <returns></returns>
        public static IList<CountryModel> GetCountryIdAndName()
        {
            return CountryDAL.GetCountryIdAndName();
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
        /// 通过国家编码联合查询获取汇率名称  ---DS2012
        /// </summary>
        /// <param name="countryCode">国家编码</param>
        /// <returns>返回汇率名称</returns>
        public static string GetMoreCurrencyNameByCountryCode(string countryCode)
        {
            return CurrencyDAL.GetMoreCurrencyNameByCountryCode(countryCode);
        }


        /// <summary>
        /// Bind the CountryName and CountryCode
        /// </summary>
        /// <returns></returns>
        public static DataTable BindCountryList()
        {
            return CountryDAL.BindCountryList();
        }

        /// <summary>
        /// 通过国家名称联合查询币种名称
        /// </summary>
        /// <param name="countryID">国家名称</param>
        /// <returns>返回币种名称</returns>
        public static string GetCurrencyNameByCountryName(string countryName)
        {
            return CurrencyDAL.GetCurrencyNameByCountryName(countryName);
        }

        /// <summary>
        /// 通过联合查询获取币种ID
        /// </summary>
        /// <param name="countryCode">国家编码</param>
        /// <returns>返回币种ID</returns>
        public static int GetMoreCurrencyIDByCountryCode(string countryCode)
        {
            return CurrencyDAL.GetMoreCurrencyIDByCountryCode(countryCode);
        }

        /// <summary>
        /// Get CurrencyID by ProductID
        /// </summary>
        /// <param name="productID">The ID of the product</param>
        /// <returns>retutrn productID</returns>
        public static int GetCurrencyIDByProductID(int productID)
        {
            return CurrencyDAL.GetCurrencyIDByProductID(productID);
        }

        /// <summary>
        /// 检查某产品是否放生了业务  ---DS2012
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>当发生了业务，返回为真，否则返回假</returns>
        public static bool CheckProductWheatherHasOperation(int productID)
        {
            return ProductDAL.CheckProductWheatherHasOperation(productID);
        }

        /// <summary>
        /// 检查某产品类是否放生了业务  ---DS2012
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="allproductIdList">所有产品ID列表</param>
        /// <returns>如果该产品类发生了业务，则返回真，否则返回假</returns>
        public static bool CheckProductKindWheatherHasOperation(int productID, out string allproductIdList)
        {
            return ProductDAL.CheckProductKindWheatherHasOperation(productID, out allproductIdList);
        }

        /// <summary>
        /// Get the ProductId count of Stock by productId then judge the productId whether exist
        /// </summary>
        /// <param name="productId">ProductId</param>
        /// <returns>Return the ProductId count of Stock by productId</returns>
        public static int ProductIdIsExist_Stock(int productId)
        {
            return StockDAL.ProductIdIsExist_Stock(productId);           
        }

        /// <summary>
        /// Get the minimun ActualStorage by ProductId the judge the Product whether lack
        /// </summary>
        /// <param name="productId">ProductId</param>
        /// <returns>Return the minimun ActualStorage by ProductId</returns>
        public static int ProductIdIsLack_Stock(int productId)
        {
            return StockDAL.ProductIdIsLack_Stock(productId);
        }

        /// <summary>
        /// Get the productId count by productId then judge the productId whether exist
        /// </summary>
        /// <param name="productId">ProductId</param>
        /// <returns>Return the productId count by productId</returns>
        public static int ProductIdIsExist_ProductQuantity(int productId)
        {
            return ProductQuantityDAL.ProductIdIsExist_ProductQuantity(productId);
        }
    }
}
