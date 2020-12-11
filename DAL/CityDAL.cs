using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using System.Data;
using System.Data.SqlClient;
using Model;

/*
 * Creator：    WangHua    
 * FinishDate： 2010-01-30
 * Founction：  Province,City Operation
 */
namespace DAL
{
    public class CityDAL
    {
        /// <summary>
        /// 向城市表中插入相关记录
        /// </summary>
        /// <param name="city">城市模型</param>
        /// <returns>返回向城市表中插入相关记录所影响的行数</returns>
        public static int AddCity(CityModel city)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@country",SqlDbType.NVarChar,40),
                new SqlParameter("@province",SqlDbType.NVarChar,40),
                new SqlParameter("@city",SqlDbType.NVarChar,40),
                new SqlParameter("@xian",SqlDbType.NVarChar,40),
                new SqlParameter("@CPCCode",SqlDbType.NVarChar,20),
                new SqlParameter("@Postcode",SqlDbType.NVarChar,20)
            };
            sparams[0].Value = city.Country;
            sparams[1].Value = city.Province;
            sparams[2].Value = city.City;
            sparams[3].Value = city.Xian;
            sparams[4].Value = city.CPCCode;
            sparams[5].Value = city.Postcode;

            return DBHelper.ExecuteNonQuery("AddCity", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 删除指定城市信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回删除指定城市信息所影响的行数</returns>
        public static int DelCityByID(int id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@ID",SqlDbType.Int)
            };
            sparams[0].Value = id;

            return DBHelper.ExecuteNonQuery("DelCityByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 修改指定的城市信息
        /// </summary>
        /// <param name="city">城市模型</param>
        /// <returns>返回修改指定的城市信息所影响的行数</returns>
        public static int UpdCityByID(CityModel city)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@ID",SqlDbType.Int),
                new SqlParameter("@country",SqlDbType.NVarChar,40),
                new SqlParameter("@province",SqlDbType.NVarChar,40),
                new SqlParameter("@city",SqlDbType.NVarChar,40),
                new SqlParameter("@xian",SqlDbType.NVarChar,40),
                new SqlParameter("@postCode",SqlDbType.NVarChar,20),
                new SqlParameter("@CPCCode",SqlDbType.NVarChar,20)
            };
            sparams[0].Value = city.Id;
            sparams[1].Value = city.Country;
            sparams[2].Value = city.Province;
            sparams[3].Value = city.City;
            sparams[4].Value = city.Xian;
            sparams[5].Value = city.Postcode;
            sparams[6].Value = city.CPCCode;

            return DBHelper.ExecuteNonQuery("UpdCityByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Judge the CityID whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>Return the counts of the city by Id</returns>
        public static int CityIdIsExist(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };

            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("CityIdIsExist", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Judge the CityId whether has operation by Id before delete
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>Return the counts of the city by Id</returns>
        public static int CityIdWhetherHasOperation(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };

            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("CityIdWhetherHasOperation", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 通过省份获取省份行数
        /// </summary>
        /// <param name="country">国家</param>
        /// <param name="province">省份</param>
        /// <returns>返回通过省份获取城市的行数</returns>
        public static int GetCityProvinceCountByCountryProvince(string country, string province)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@country",SqlDbType.NVarChar,40),
                new SqlParameter("@province",SqlDbType.NVarChar,40)
            };
            sparams[0].Value = country;
            sparams[1].Value = province;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetCityProvinceCountByCountryProvince", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 通过国家省份城市获取行数
        /// </summary>
        /// <param name="country">国家</param>
        /// <param name="province">省份</param>
        /// <param name="city">城市</param>
        /// <param name="xian">区县</param>
        /// <returns>返回通过省份城市获取行数</returns>
        public static int GetCityCityCountByCountryProvinceCity(string country, string province, string city, string xian)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@country",SqlDbType.NVarChar,40),
                new SqlParameter("@province",SqlDbType.NVarChar,40),
                new SqlParameter("@city",SqlDbType.NVarChar,40),
                new SqlParameter("@xian",SqlDbType.NVarChar,40)
            };
            sparams[0].Value = country;
            sparams[1].Value = province;
            sparams[2].Value = city;
            sparams[3].Value = xian;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetCityCityCountByCountryProvinceCity", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 通过ID,国家，省份获取行数
        /// </summary>
        /// <param name="city">城市模型</param>
        /// <returns>返回通过ID,国家，省份获取行数</returns>
        public static int GetCityProvinceCountByIDCountryProvince(CityModel city)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@ID",SqlDbType.Int),
                new SqlParameter("@country",SqlDbType.NVarChar,40),
                new SqlParameter("@province",SqlDbType.NVarChar,40)              
            };

            sparams[0].Value = city.Id;
            sparams[1].Value = city.Country;
            sparams[2].Value = city.Province;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetCityProvinceCountByIDCountryProvince", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 通过ID,国家，省份,城市获取行数
        /// </summary>
        /// <param name="city">城市模型</param>
        /// <returns>返回通过ID,国家，省份,城市获取行数</returns>
        public static int GetCityCityCountByIDCountryProvinceCity(CityModel city)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@ID",SqlDbType.Int),
                new SqlParameter("@country",SqlDbType.NVarChar,40),
                new SqlParameter("@province",SqlDbType.NVarChar,40),
                new SqlParameter("@city",SqlDbType.NVarChar,40),
                new SqlParameter("@xian",SqlDbType.NVarChar,40)
            };
            sparams[0].Value = city.Id;
            sparams[1].Value = city.Country;
            sparams[2].Value = city.Province;
            sparams[3].Value = city.City;
            sparams[4].Value = city.Xian;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetCityCityCountByIDCountryProvinceCity", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取城市信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetCityInfo()
        {
            return DBHelper.ExecuteDataTable("GetCityInfo", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取指定城市信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetCityInfoByID(int id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@ID",SqlDbType.Int)
            };
            sparams[0].Value = id;

            return DBHelper.ExecuteDataTable("GetCityInfoByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Out to excel the all data of City
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_City()
        {
            return DBHelper.ExecuteDataTable("OutToExcel_City", CommandType.StoredProcedure);
        }
        /// <summary>
        /// 获取城市信息
        /// </summary>
        /// <param name="cpccode"></param>
        /// <returns></returns>
        public static CityModel GetCityInfoByCPCCode(string cpccode)
        {
            DataTable dt = DBHelper.ExecuteDataTable(" select top 1 ID,CPCCode,Country,Province,City,Xian,PostCode from City where CPCCode='" + cpccode + "'", CommandType.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                CityModel city = new CityModel();
                city.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                city.CPCCode = dt.Rows[0]["CPCCode"].ToString();
                city.Country = dt.Rows[0]["Country"].ToString();
                city.Province = dt.Rows[0]["Province"].ToString();
                city.City = dt.Rows[0]["City"].ToString();
                city.Xian = dt.Rows[0]["Xian"].ToString();
                city.Postcode = dt.Rows[0]["PostCode"].ToString();

                return city;
            }
            return null;
        }
    }
}