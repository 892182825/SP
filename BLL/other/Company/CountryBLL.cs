using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data;
using System.Web.UI.WebControls;

namespace BLL.other.Company
{
    public class CountryBLL
    {
          /// <summary>
        /// 根据国家编号获取币种编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetCountryRateIDByID(int id)
        {
            return CountryDAL.GetCountryRateIDByID(id);
        }
        /// <summary>
        /// 获取当前系统中的所有国家
        /// </summary>
        /// <returns></returns>
        public static IList<CountryModel> GetCountryModels()
        {
            return CountryDAL.GetCountryModels();
        }

        /// <summary>
        /// 根据币种ID获取国家编码  ---DS2012
        /// </summary>
        /// <returns></returns>
        public static string GetCountryCodeByID(string currency)
        {
            return CountryDAL.GetCountryCodeByID(currency);
        }

        /// <summary>
        /// 获取指定国家的所有省份
        /// </summary>
        /// <param name="country">国家名</param>
        /// <returns></returns>
        public static DataTable GetProvince(string country)
        {
            return CountryDAL.GetProvinces(country);
        }

        /// <summary>
        /// 获取指定省份的所有城市
        /// </summary>
        /// <param name="country">国家名</param>
        /// <param name="province">省份名</param>
        /// <returns></returns>
        public static DataTable GetCitys(string province,string country)
        {
            return CountryDAL.GetCitys(province, country);
        }

        /// <summary>
        /// 获取指定城市下的所有县--用于四级联动的控件
        /// </summary>
        /// <param name="country">国家名</param>
        /// <param name="province">省份名</param>
        /// <param name="city">城市名</param>
        /// <returns></returns>
        public static DataTable GetXians(string city, string province, string country)
        {
            return CountryDAL.GetXians(city, province, country);
        }

        /// <summary>
        /// 获取所有国家信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCountrys()
        {
            return CountryDAL.GetCountrys();
        }

        /// <summary>
        /// Bind the CountryName and CountryCode
        /// </summary>
        /// <returns></returns>
        public static DataTable BindCountryList()
        {
            return CountryDAL.BindCountryList();
        }
        public static void BindCountryList(DropDownList list, string defaultCountry)
        {
            CountryDAL.BindCountryList(list, defaultCountry);
        }
        public static string BindCountryID(string name)
        {
            return CountryDAL.BindCountryID(name);
        }  

        /// <summary>
        /// 获取国家 id name
        /// </summary>
        /// <returns></returns>
        public static List<CountryModel> GetCountry()
        {
            return CountryDAL.GetCountry();
        }

        /// <summary>
        /// Bind Country
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCountryInfo()
        {
            return CountryDAL.GetCountryInfo();
        }



        /// <summary>
        /// 获取国家code
        /// </summary>
        /// <param name="ID">国家ID</param>
        /// <returns></returns>
        public static string GetCountryByCode(int ID)
        {
            return CountryDAL.GetCountryByCode(ID);
        }

        /// <summary>
        /// 根据简码获取国家 省份 城市
        /// </summary>
        /// <param name="code">简码</param>
        /// <returns></returns>
        public static DataSet GetCityCode(string code)
        {
            return CountryDAL.GetCityCode(code);
        }

        //获取当前系统的币种
        public static string GetCurrency()
        {
            return CountryDAL.GetCurrency();
        }

        public static DataTable GetContry()
        {
            return CountryDAL.GetContry();
        }

        //获取国家名
        public static string GetCountryName(string id)
        {
            return CountryDAL.GetCountryName(id);
        }
        //获取国家简称
        public static string GetCountryShortName(string id)
        {
            return CountryDAL.GetCountryShortName(id);
        }
    }
}
