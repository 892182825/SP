using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using System.Data;
using System.Data.SqlClient;
using DAL;
using Model;


/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-11
 * 对应菜单：   系统管理->各汇率设置
 */

namespace BLL.other.Company
{
    public class SetRateBLL
    {
        /// <summary>
        /// 向产品名称等翻译表插入相关记录
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="languageTrans">产品名称等翻译表模型</param>
        /// <returns>返回插入所影响的行数</returns>
        public static  int AddLanguageTrans(SqlTransaction tran, LanguageTransModel languageTrans)
        {
            return LanguageTransDAL.AddLanguageTrans(tran,languageTrans);
        }

        /// <summary>
        /// 向语言表中插入记录
        /// </summary>
        /// <param name="languageModel">语言模型</param>
        /// <returns>返回向语言表中插入记录所影响的行数</returns>
        public static int AddLanguage(LanguageModel languageModel)
        {
            return LanguageDAL.AddLanguage(languageModel);
        }

        /// <summary>
        ///向汇率表中插入记录
        /// </summary>
        /// <param name="currencyName">币种名称</param>
        /// <returns> 返回向汇率表中插入记录所影响的行数</returns>
        public static int AddCurrency(string currencyName,out int id)
        {
            return CurrencyDAL.AddCurrency(currencyName,out id);
        }

        /// <summary>
        /// 向国家表中插入相关记录
        /// </summary>
        /// <param name="country">实体参数</param>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddCountry(CountryModel country)
        {
            return CountryDAL.AddCountry(country);
        }

        /// <summary>
        ///  将中文对照翻译插入该表（TranToN）
        /// </summary>
        /// <param name="countryName">国家名称</param>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddTranToN(string countryName)
        {
            return TranToNDAL.AddTranToN(countryName);
        }

        /// <summary>
        /// 删除指定的汇率记录
        /// </summary>
        /// <param name="ID">汇率ID</param>
        /// <returns>返回删除所影响的行数</returns>
        public static int DelCurrencyByID(int ID)
        {
            return CurrencyDAL.DelCurrencyByID(ID);
        }

        /// <summary>
        /// Judge the CountryCode whether has operation before delete by Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the CountryCode by Id</returns>
        public static int CountryCodeWhetherHasOperation(int Id)
        {
            return CountryDAL.CountryCodeWhetherHasOperation(Id);
        }

        public static int GetCountryByRateID(int ID)
        {
            return CurrencyDAL.GetCountryByRateID(ID);
        }

        /// <summary>
        /// 删除指定国家记录
        /// </summary>
        /// <param name="ID">国家ID</param>
        /// <returns>返回删除指定国家记录所影响的行数</returns>
        public static int DelCountryByID(int ID)
        {
            return CountryDAL.DelCountryByID(ID);
        }

        /// <summary>
        /// 根据ID删除指定的语言记录
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>返回删除指定记录所影响的行数</returns>
        public static int DelLanguageByID(int ID)
        {
            return LanguageDAL.DelLanguageByID(ID);
        }

        /// <summary>
        /// 删除表TranToN
        /// </summary>
        /// <param name="countryName">国家名称</param>
        /// <returns>返回删除表TranToN所影响的行数</returns>
        public static int DelTranToN(string countryName)
        {
            return TranToNDAL.DelTranToN(countryName);
        }

        /// <summary>
        /// 根据语言ID删除指定的产品名称等翻译记录
        /// </summary>
        /// <param name="languageID">语言ID</param>
        /// <returns>返回删除指定记录所影响的行数</returns>
        public static int DelLanguageTransByLanguageID(int languageID)
        {
            return LanguageTransDAL.DelLanguageTransByLanguageID(languageID);
        }
        
        /// <summary>
        /// 根据汇率ID更改汇率
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <param name="ID">汇率ID</param>
        /// <returns>返回更改汇率所影响的行数</returns>
        public static int UpdCurrencyRateByID(decimal rate, int ID,int flag)
        {
            return CurrencyDAL.UpdCurrencyRateByID(rate,ID,flag);
        }

        /// <summary>
        /// 根据汇率ID更改汇率
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <param name="ID">汇率ID</param>
        /// <returns>返回更改汇率所影响的行数</returns>
        public static int UpdCurrencyRateByID(decimal rate, int ID )
        {
            return CurrencyDAL.UpdCurrencyRateByID(rate, ID );
        }

        /// <summary>
        /// 根据WebService同步汇率
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <param name="jiancheng">币种简称</param>
        /// <returns>返回影响行数</returns>
        public static bool UpdCurrencyRateAll(IList<CurrencyModel> currs)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tr = conn.BeginTransaction();//开启事务

                try
                {
                    foreach (CurrencyModel curr in currs)
                    {
                        if (CurrencyDAL.UpdCurrencyRateByJC(tr, curr.Rate, curr.JianCheng) != 1)
                        {
                            tr.Rollback();
                            return false;
                        }
                    }

                    tr.Commit();
                    return true;
                }
                catch
                {
                    tr.Rollback();
                }
                finally
                {
                    conn.Close();
                }
            }

            return false;
        }

        /// <summary>
        /// 获取标准币种简称
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultCurrency()
        {
            return CurrencyDAL.GetDefaultCurrency();
        }

        /// <summary>
        /// 获取标准币种ID——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static int GetDefaultCurrencyId()
        {
            return CurrencyDAL.GetDefaultCurrencyId();
        }

        /// <summary>
        /// 获取所有币种
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllCurrency()
        {
            return CurrencyDAL.GetAllCurrency();
        }

        /// <summary>
        /// 根据国家ID更改币种ID
        /// </summary>
        /// <param name="rateID">币种ID</param>
        /// <param name="ID">国家ID</param>
        /// <returns>返回更改币种ID所影响的行数</returns>
        public static int UpdCountryRateIDByID(int rateID, int ID)
        {
            return CountryDAL.UpdCountryRateIDByID(rateID,ID);
        }

        /// <summary>
        /// 从数据库获取汇率
        /// </summary>
        /// <param name="from">转换汇率</param>
        /// <param name="to">转换成的汇率</param>
        /// <returns>返回汇率</returns>
        public static double GetCurrencyBySql(int from, int to)
        {
            return CurrencyDAL.GetCurrencyBySql(from, to);
        }
        
        
        /// <summary>
        /// 获取汇率相关信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetCurrencyInfo()
        {
            return  CurrencyDAL.GetCurrencyInfo();
        }
        
        /// <summary>
        /// 获取所有的汇率信息
        /// </summary>
        /// <param name="tran">要处理的事务</param>
        /// <returns>返回DataTable</returns>
        public static DataTable GetAllCurrencyInfo(SqlTransaction tran)
        {
            return CurrencyDAL.GetAllCurrencyInfo(tran);
        }

        /// <summary>
        /// 通过币种名称联合查询获取行数
        /// </summary>
        /// <param name="currencyName">币种名称</param>
        /// <returns>返回行数</returns>
        public static int GetMoreCurrencyCountByCurrencyName(string currencyName)
        {
            return CurrencyDAL.GetMoreCurrencyCountByCurrencyName(currencyName);            
        }

        /// <summary>
        /// 通过汇率名称获取行数
        /// </summary>
        /// <param name="currencyName">汇率名称</param>
        /// <returns>返回行数</returns>
        public static int GetCurrencyCountByCurrencyName(string currencyName)
        {
            return CurrencyDAL.GetCurrencyCountByCurrencyName(currencyName);
        }

        /// <summary>
        /// 通过国家名称获取行数
        /// </summary>
        /// <param name="countryName">国家名称</param>
        /// <returns>返回指定国家的行数</returns>
        public static int GetCountryCountByCountryName(string countryName)
        {
            return CountryDAL.GetCountryCountByCountryName(countryName);
        }

        /// <summary>
        /// 通过币种ID获取行数
        /// </summary>
        /// <param name="rateID">币种ID</param>
        /// <returns>返回指定币种的行数</returns>
        public static int GetCountryCountByRateID(int rateID)
        {
            return CountryDAL.GetCountryCountByRateID(rateID);
        }

        /// <summary>
        /// 通过语言名称获取行数
        /// </summary>
        /// <param name="name">语言名称</param>
        /// <returns>返回行数</returns>
        public static int GetLanguageCountByName(string name)
        {
            return LanguageDAL.GetLanguageCountByName(name);            
        }

        /// <summary>
        /// 获取语言表中最大的ID
        /// </summary>
        /// <returns>返回语言表中最大的ID</returns>
        public static int GetLanguageMaxID()
        {
            return LanguageDAL.GetLanguageMaxID();
        }

      

        /// <summary>
        /// 获取汇率表中所有的ID和币种名称
        /// </summary>
        /// <returns>返回SqlDataReader对象</returns>
        public static IList<Model.CurrencyModel> GetAllCurrencyIDName()
        {
            return CurrencyDAL.GetAllCurrencyIDName();
        }

        /// <summary>
        /// 获取汇率表中部分汇率ID和币种名称
        /// </summary>
        /// <returns>返回SqlDataReader对象</returns>
        public static IList<Model.CurrencyModel> GetPartCurrencyIDName()
        {
            return CurrencyDAL.GetPartCurrencyIDName();
        }

        /// <summary>
        /// 获取语言ID和语言名称按语言名称排序
        /// </summary>
        /// <returns>返回SqlDataReader对象</returns>
        public static SqlDataReader GetAllLanguageIDNameOrderByName()
        {
            return LanguageDAL.GetAllLanguageIDNameOrderByName();
        }
        
        
        /// <summary>
        /// 获取所有语言ID和语言名称
        /// </summary>
        /// <returns>返回DataTable</returns>
        public static DataTable GetAllLanguageIDName()
        {
            return LanguageDAL.GetAllLanguageIDName();            
        }


        /// <summary>
        /// 通过联合查询获取更多的信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMoreCountryInfo()
        {
            return CountryDAL.GetMoreCountryInfo();
        }

        /// <summary>
        /// 获取产品ID和产品名称
        /// </summary>
        /// <param name="tran">处理的事务</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductIDNameOrderByProductID(SqlTransaction tran)
        {
            return ProductDAL.GetProductIDNameOrderByProductID(tran);
        }

        /// <summary>
        /// 获取产品ID和产品说明
        /// </summary>
        /// <param name="tran">要处理的事务</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductIDDescriptionByDescription(SqlTransaction tran)
        {
            return ProductDAL.GetProductIDDescriptionByDescription(tran);
        }

        /// <summary>
        /// 获取产品单位ID和单位名称
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductUnitIDNameOrderByUnitID()
        {
            return ProductUnitDAL.GetProductUnitIDNameOrderByUnitID();
        }

        /// <summary>
        /// 获取会员使用银行的银行ID和银行名称
        /// </summary>
        /// <param name="tran">要处理的事务</param>
        /// <returns>返回DataTable对象</returns>
        public static  DataTable GetMemberBankIDName(SqlTransaction tran)
        {
            return MemberBankDAL.GetMemberBankIDName(tran);
        }

        /// <summary>
        /// Get the count of the CoutryCode by CountryCode
        /// </summary>
        /// <param name="countryCode">Country Code</param>
        /// <returns>Return the count of the CoutryCode by CountryCode</returns>
        public static int CountryCodeIsExist(string countryCode)
        {
            return CountryDAL.CountryCodeIsExist(countryCode);
        }

        /// <summary>
        /// Out to excel of the all data of Currency
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_Currency()
        {
            return CurrencyDAL.OutToExcel_Currency();
        }
        
        /// <summary>
        /// Out to excel of the all data of Country
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_Country()
        {
            return CountryDAL.OutToExcel_Country();
        }
    }
}
