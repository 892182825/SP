using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using Model;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

/*
 * 修改者：     汪  华
 * 修改时间：   2009-09-05
 * 文件名：     CountryDAL(Speed)
 */

namespace DAL
{
    public class CountryDAL
    {
        /// <summary>
        /// 向国家表中插入相关记录
        /// </summary>
        /// <param name="country">Country Model</param>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddCountry(CountryModel country)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@countryCode",SqlDbType.NVarChar,40),
                new SqlParameter("@countryForShort",SqlDbType.NVarChar,40),
                new SqlParameter("@name",SqlDbType.NVarChar,40),
                new SqlParameter("@rateID",SqlDbType.Int)
            };

            sparams[0].Value = country.CountryCode;
            sparams[1].Value = country.CountryForShort;
            sparams[2].Value = country.Name;
            sparams[3].Value = country.RateID;

            return DBHelper.ExecuteNonQuery("AddCountry", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 删除指定国家记录
        /// </summary>
        /// <param name="ID">国家ID</param>
        /// <returns>返回删除指定国家记录所影响的行数</returns>
        public static int DelCountryByID(int ID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@ID",SqlDbType.Int)
            };
            sparams[0].Value = ID;

            return DBHelper.ExecuteNonQuery("DelCountryByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 根据币种ID获取国家编码  ---DS2012
        /// </summary>
        /// <param name="curreny"></param>
        /// <returns></returns>
        public static string GetCountryCodeByID(string curreny)
        {
            string sqlstr = " select countrycode from country,currency where country.RateID=currency.id and currency.id=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@num",SqlDbType.VarChar,20)
                
            };
            sparams[0].Value = curreny;
            return DBHelper.ExecuteScalar(sqlstr, sparams, CommandType.Text).ToString();
        }

        /// <summary>
        /// 根据国家ID更改币种ID
        /// </summary>
        /// <param name="rateID">币种ID</param>
        /// <param name="ID">国家ID</param>
        /// <returns>返回更改币种ID所影响的行数</returns>
        public static int UpdCountryRateIDByID(int rateID, int ID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@rateID",SqlDbType.Int),
                new SqlParameter("@ID",SqlDbType.Int)
            };
            sparams[0].Value = rateID;
            sparams[1].Value = ID;

            return DBHelper.ExecuteNonQuery("UpdCountryRateIDByID", sparams, CommandType.StoredProcedure);
        }

        public static IList<CountryModel> GetCountryModels()
        {
            //IList<CountryModel> countryModels = null;
            //string sql = "select id,name,rateid from country";
            //SqlDataReader reader = DBHelper.ExecuteReader(sql, CommandType.Text);
            //if (reader.HasRows)
            //{
            //    //构建国家泛型集合
            //    countryModels = new List<CountryModel>();
            //    //读取国家数据
            //    while (reader.Read())
            //    {
            //        //获取国家数据
            //        CountryModel countryModel = new CountryModel(reader.GetInt32(0));
            //        countryModel.Name = reader.GetString(1);
            //        countryModel.RateID = reader.GetInt32(2);
            //        countryModels.Add(countryModel);
            //    }
            //}
            //reader.Close();
            //return countryModels;

            IList<CountryModel> countryModels = null;
            string sql = "select id,CountryCode,name,rateid from country order by id ";
            SqlDataReader reader = DBHelper.ExecuteReader(sql, CommandType.Text);
            if (reader.HasRows)
            {
                //构建国家泛型集合
                countryModels = new List<CountryModel>();
                //读取国家数据
                while (reader.Read())
                {
                    //获取国家数据
                    CountryModel countryModel = new CountryModel(reader.GetInt32(0));
                    countryModel.CountryCode = reader.GetString(1);
                    countryModel.Name = reader.GetString(2);
                    countryModel.RateID = reader.GetInt32(3);
                    countryModels.Add(countryModel);
                }
            }
            reader.Close();
            return countryModels;
        }

        /// <summary>
        /// 通过国家名称获取行数
        /// </summary>
        /// <param name="countryName">国家名称</param>
        /// <returns>返回指定国家的行数</returns>
        public static int GetCountryCountByCountryName(string countryName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@countryName",SqlDbType.NVarChar,40)
            };
            sparams[0].Value = countryName;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetCountryCountByCountryName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 通过币种ID获取行数
        /// </summary>
        /// <param name="rateID">币种ID</param>
        /// <returns>返回指定币种的行数</returns>
        public static int GetCountryCountByRateID(int rateID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@rateID",SqlDbType.Int)
            };
            sparams[0].Value = rateID;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetCountryCountByRateID", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 通过国家编码获取国家名称
        /// </summary>
        /// <param name="countryID">国家ID</param>
        /// <returns>返回国家名称</returns>
        public static string GetCountryNameByCountryCode(string countryCode)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@countryCode",SqlDbType.NVarChar,10)
            };
            sparams[0].Value = countryCode;

            return Convert.ToString(DBHelper.ExecuteScalar("GetCountryNameByCountryCode", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 通过国家名称获取国家ID
        /// </summary>
        /// <param name="name">国家名称</param>
        /// <returns>返回国家ID</returns>
        public static int GetCountryIDByName(string name)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@name",SqlDbType.VarChar,50)
            };
            sparams[0].Value = name;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetCountryIDByName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 通过联合查询获取更多的信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMoreCountryInfo()
        {
            return DBHelper.ExecuteDataTable("GetMoreCountryInfo", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取国家ID,编码和国家名称
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetCountryIDCodeNameOrderByID()
        {
            return DBHelper.ExecuteDataTable("GetCountryIDCodeNameOrderByID", CommandType.StoredProcedure);
        }

        /// <summary>
        /// Bind the CountryName and CountryCode
        /// </summary>
        /// <returns></returns>
        public static DataTable BindCountryList()
        {
            return DBHelper.ExecuteDataTable("BindCountryList", CommandType.StoredProcedure);
        }


        public static string BindCountryID(string name)
        {
            SqlParameter[] sPara = new SqlParameter[] { new SqlParameter("@num", SqlDbType.NVarChar, 20) };
            sPara[0].Value = name;

            SqlDataReader dr = DBHelper.ExecuteReader("select ID from country where Name=@num", sPara, CommandType.Text);
            dr.Read();
            string id = dr["ID"].ToString();

            dr.Close();

            return id;
        }

        /// <summary>
        /// 根据国家获取省份
        /// </summary>
        /// <param name="country">国家名称</param>
        /// <returns>省份名</returns>
        public static DataTable GetProvinces(string country)
        {
            //string sql = "select DISTINCT province,(SELECT TOP 1 id FROM City C2 WHERE C2.Province=c1.Province) AS id from city c1 where Country=@country order by id asc";
            string sql = "select distinct Province from City where Country=@country order by Province";
            return DBHelper.ExecuteDataTable(sql, new SqlParameter[] { new SqlParameter("@country", country) }, CommandType.Text);
        }

        /// <summary>
        /// 根据省份获取城市
        /// </summary>
        /// <param name="province">省份名称</param>
        /// <returns></returns>
        public static DataTable GetCitys(string province, string country)
        {
            //string sql = "select city from city where province =@province and country=@country order by id asc";
            string sql = "select City from City where Province =@province and country=@country  group by City ";
            return DBHelper.ExecuteDataTable(sql, new SqlParameter[] { new SqlParameter("@province", province), new SqlParameter("@country", country) }, CommandType.Text);
        }

        /// <summary>
        /// 根据省份获取城市--用于四级联动的控件
        /// </summary>
        /// <param name="country">国家名</param>
        /// <param name="province">省份名</param>
        /// <param name="city">城市名</param>
        /// <returns></returns>
        public static DataTable GetXians(string city, string province, string country)
        {
            string sql = "select Xian from City where City=@city and Province =@province and country=@country order by id asc";
            return DBHelper.ExecuteDataTable(sql, new SqlParameter[] { new SqlParameter("@city", city), new SqlParameter("@province", province), new SqlParameter("@country", country) }, CommandType.Text);
        }

        public static DataTable GetCountrys()
        {
            string sql = "select country from city";
            return DBHelper.ExecuteDataTable(sql);
        }

        /// <summary>
        /// 获取国家id，name
        /// </summary>
        /// <returns></returns>
        public static List<CountryModel> GetCountry()
        {
            List<CountryModel> list = new List<CountryModel>();
            SqlDataReader read = DBHelper.ExecuteReader("select id,name from Country order by id", CommandType.Text);
            while (read.Read())
            {
                CountryModel model = new CountryModel(int.Parse(read["ID"].ToString().Trim()));
                model.Name = read["Name"].ToString().Trim();
                list.Add(model);
            }
            read.Close();
            return list;
        }

        /// <summary>
        /// 获取国家name
        /// </summary>
        /// <param name="ID">国家ID</param>
        /// <returns></returns>
        public static string GetCountryByID(int ID)
        {
            string name = string.Empty;
            SqlDataReader read = DBHelper.ExecuteReader("select id,name from Country where id=" + ID + "", CommandType.Text);
            if (read.Read())
            {
                name = read["Name"].ToString().Trim();
            }
            read.Close();
            return name;
        }

        /// <summary>
        /// 返回国家信息表
        /// </summary>
        /// <returns></returns>
        public static IList<CountryModel> GetCountryIdAndName()
        {
            IList<CountryModel> countryModels = null;
            string sql = "select id,name from Country order by id";
            SqlDataReader reader = DBHelper.ExecuteReader(sql);
            if (reader.HasRows)
            {
                //构建国家集合
                countryModels = new List<CountryModel>();
                //读取国家ID和Name信息
                while (reader.Read())
                {
                    //获取国家信息
                    CountryModel cm = new CountryModel();
                    cm.ID = Convert.ToInt32(reader["id"]);
                    cm.Name = Convert.ToString(reader["Name"]);
                    countryModels.Add(cm);
                }
            }
            reader.Close();
            return countryModels;
        }
        /// <summary>
        /// 根据店铺编号查出国家ID
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public static int GetIdByStoreId(string storeId)
        {
            SqlParameter[] sPara = new SqlParameter[] { new SqlParameter("@num", SqlDbType.NVarChar, 20) };
            sPara[0].Value = storeId;

            return Convert.ToInt32(DBHelper.ExecuteScalar("select b.id from Country b,StoreInfo c where  b.countrycode=substring(c.SCPCCode,1,2) and c.storeid=@num", sPara, CommandType.Text));
        }

        /// <summary>
        /// 根据国家编号获取币种编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetCountryRateIDByID(int id)
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetCountryRateID", new SqlParameter("@id", id), CommandType.StoredProcedure));
        }

        /// <summary>
        /// 绑定国家(国家名称 -- 国家ID )列表
        /// </summary>
        /// <param name="list">要添加期数的控件</param>
        /// <param name="defaultCountry">默认国家</param>
        public static void BindCountryList(DropDownList list, string defaultCountry)
        {
            SqlDataReader dr = DBHelper.ExecuteReader("BindCountryList", CommandType.StoredProcedure);
            while (dr.Read())
            {
                ListItem listItem = new ListItem(dr["Name"].ToString(), dr["CountryCode"].ToString());
                if (defaultCountry == dr["Name"].ToString())
                    listItem.Selected = true;
                list.Items.Add(listItem);
            }
            dr.Close();
        }

        /// <summary>
        /// 获得当前仓库的索引
        /// </summary>
        /// <returns></returns>
        public static int GetIDENT_CURRENT()
        {
            string sql = "select IDENT_CURRENT('WareHouse')";
            return int.Parse(DBHelper.ExecuteScalar(sql).ToString());
        }

        /// <summary>
        /// Judge the CountryCode whether has operation before delete by Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the CountryCode by Id</returns>
        public static int CountryCodeWhetherHasOperation(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;
            return Convert.ToInt32(DBHelper.ExecuteScalar("CountryCodeWhetherHasOperation", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Get the count of the CoutryCode by CountryCode
        /// </summary>
        /// <param name="countryCode">Country Code</param>
        /// <returns>Return the count of the CoutryCode by CountryCode</returns>
        public static int CountryCodeIsExist(string countryCode)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@countryCode",SqlDbType.NVarChar,40)
            };
            sparams[0].Value = countryCode;
            return Convert.ToInt32(DBHelper.ExecuteScalar("CountryCodeIsExist", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Out to excel of the all data of Country
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_Country()
        {
            return DBHelper.ExecuteDataTable("OutToExcel_Country", CommandType.StoredProcedure);
        }
        /// <summary>
        /// 获取国家code
        /// </summary>
        /// <param name="ID">国家ID</param>
        /// <returns></returns>
        public static string GetCountryByCode(int ID)
        {
            string name = string.Empty;
            SqlDataReader read = DBHelper.ExecuteReader("select CountryCode from Country where id=" + ID + "", CommandType.Text);
            if (read.Read())
            {
                name = read["CountryCode"].ToString().Trim();
            }
            read.Close();
            return name;
        }

        /// <summary>
        /// 根据简码获取国家 省份 城市
        /// </summary>
        /// <param name="code">简码</param>
        /// <returns></returns>
        public static DataSet GetCityCode(string code)
        {
            string sql = "select country,province,city,xian from city where cpccode=@num";
            SqlParameter[] sPara = new SqlParameter[] { new SqlParameter("@num", SqlDbType.NVarChar, 40) };
            sPara[0].Value = code;
            SqlDataAdapter adapter = ExecuteDataAdapter(sql, sPara, CommandType.Text);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        public static SqlDataAdapter ExecuteDataAdapter(string cmdText, SqlParameter[] sPara, CommandType cmdtype)
        {
            SqlConnection conn = new SqlConnection(DBHelper.connString);
            SqlDataAdapter da = new SqlDataAdapter(cmdText, conn);
            da.SelectCommand.CommandType = cmdtype;
            if (sPara != null)
            {
                //添加参数
                foreach (SqlParameter parm in sPara)
                {
                    da.SelectCommand.Parameters.Add(parm);
                }
            }
            return da;
        }

        //
        public static string GetCurrency()
        {
            return DBHelper.ExecuteScalar("select top 1 standardMoney from dbo.Currency").ToString();
        }

        //绑定国家
        public static DataTable GetContry()
        {
            return DBHelper.ExecuteDataTable("select id,cncountry+'('+Encountry+')' as country from dbo.BSCO_country");
        }

        /// <summary>
        /// Bind Country
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCountryInfo()
        {
            return DBHelper.ExecuteDataTable("select id,cncountry as country from dbo.BSCO_country");
        }

        //获取国家名
        public static string GetCountryName(string id)
        {
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.Int);
            sPara.Value = id;

            return DBHelper.ExecuteScalar("select Cncountry from dbo.BSCO_country where id=@num", sPara, CommandType.Text).ToString();
        }
        //获取国家简称
        public static string GetCountryShortName(string id)
        {
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.Int);
            sPara.Value = id;
            return DBHelper.ExecuteScalar("select code from dbo.BSCO_country where id=@num", sPara, CommandType.Text).ToString();
        }
    }
}