using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

//Add Namespace
using Model;
using System.Web.UI.WebControls;

/*
 * 创建者：     汪  华
 * 创建日期：   2009-09-01(Speed)
 */

namespace DAL
{
    public class CurrencyDAL
    {
        /// <summary>
        ///向汇率表中插入记录
        /// </summary>
        /// <param name="currencyName">币种名称</param>
        /// <returns> 返回向汇率表中插入记录所影响的行数</returns>
        public static int AddCurrency(string currencyName,out int id)
        {
            id = 0;
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@currencyName",SqlDbType.NVarChar,40),
                new SqlParameter("@id",SqlDbType.Int)
            };
            sparams[0].Value = currencyName;
            sparams[1].Direction = ParameterDirection.Output;
            int num=DBHelper.ExecuteNonQuery("AddCurrency", sparams, CommandType.StoredProcedure);
            id = int.Parse(sparams[1].Value.ToString());
            return num;
        }

        /// <summary>
        /// 根据ID获取简称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetJieCheng(int id)
        {
            return DBHelper.ExecuteScalar("select jiecheng from Currency where id=@id", new SqlParameter[] { new SqlParameter("@id", id) }, CommandType.Text).ToString();
        }

        /// <summary>
        /// 删除指定的汇率记录
        /// </summary>
        /// <param name="ID">汇率ID</param>
        /// <returns>返回删除所影响的行数</returns>
        public static int DelCurrencyByID(int ID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@ID",SqlDbType.Int)
            };
            sparams[0].Value = ID;
            
            return DBHelper.ExecuteNonQuery("DelCurrencyByID", sparams, CommandType.StoredProcedure);
        }


        public static int GetCountryByRateID(int ID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@ID",SqlDbType.Int)
            };
            sparams[0].Value = ID;

            return Convert.ToInt32(DBHelper.ExecuteScalar("select count(0) from country where rateid="+ID, sparams, CommandType.Text));
        }

        /// <summary>
        /// 根据汇率ID更改汇率
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <param name="ID">汇率ID</param>
        /// <returns>返回更改汇率所影响的行数</returns>
        public static int UpdCurrencyRateByID(decimal rate, int ID,int flag)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@rate",SqlDbType.Decimal),
                new SqlParameter("@ID",SqlDbType.Int),
                new SqlParameter("@flag",flag)
            };
            sparams[0].Value = rate;
            sparams[1].Value = ID;

            return DBHelper.ExecuteNonQuery("update Currency set Rate=@rate,bzflag=@flag where id=@ID", sparams, CommandType.Text);
        }

        /// <summary>
        /// 根据汇率ID更改汇率
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <param name="ID">汇率ID</param>
        /// <returns>返回更改汇率所影响的行数</returns>
        public static int UpdCurrencyRateByID(decimal rate, int ID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@rate",SqlDbType.Decimal),
                new SqlParameter("@ID",SqlDbType.Int),
            };
            sparams[0].Value = rate;
            sparams[1].Value = ID;

            return DBHelper.ExecuteNonQuery("update Currency set Rate=@rate where id=@ID", sparams, CommandType.Text);
        }

        /// <summary>
        /// 从数据库获取汇率
        /// </summary>
        /// <param name="from">转换汇率</param>
        /// <param name="to">转换成的汇率</param>
        /// <returns>返回汇率</returns>
        public static double GetCurrencyBySql(int from, int to)
        {
            return Convert.ToDouble(DBHelper.ExecuteScalar("select (select rate from Currency where id=@to)/(select rate from Currency where id=@from)", 
                new SqlParameter[] { new SqlParameter("@from", from), new SqlParameter("@to", to) }, CommandType.Text));
        }

        /// <summary>
        /// 根据汇率ID更改汇率
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <param name="ID">汇率ID</param>
        /// <returns>返回更改汇率所影响的行数</returns>
        public static int UpdCurrencyRateByJC(SqlTransaction tran, double rate, string jiecheng)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@rate",SqlDbType.Decimal),
                new SqlParameter("@jc",SqlDbType.VarChar,10 ),
            };
            sparams[0].Value = Convert.ToDecimal(rate);
            sparams[1].Value = jiecheng;

            return DBHelper.ExecuteNonQuery("update Currency set Rate=@rate where jiecheng=@jc", sparams, CommandType.Text);
        }

        /// <summary>
        /// 获取所有币种
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllCurrency()
        {
            return DBHelper.ExecuteDataTable("select jiecheng from Currency");
        }

        /// <summary>
        /// 获取标准币种简称
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultCurrency()
        {
            return DBHelper.ExecuteScalar(" select jiecheng from Currency where id=(select top 1 standardmoney from Currency ) ").ToString();
        }

        /// <summary>
        /// 绑定币种
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectCurrency()
        {
            return DBHelper.ExecuteDataTable("select id,jiecheng from Currency where bzflag=1 ");
        }

        /// <summary>
        /// 获取标准币种id——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static int GetDefaultCurrencyId()
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar(" select top 1 standardmoney from Currency where standardmoney=id"));
        }

        /// <summary>
        /// 根据当前登陆的店铺编号获取币种名称
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public static string SelNameByID(string storeID)
        {
            string name = "";
            string cmd = "Select Currency.name from StoreInfo,Currency where StoreInfo.storeid=@num and StoreInfo.currency=Currency.id";
            SqlParameter[] sPara = new SqlParameter[] { new SqlParameter("@num", SqlDbType.NVarChar,50) };
            sPara[0].Value = storeID;
            SqlDataReader dr = DBHelper.ExecuteReader(cmd,sPara,CommandType.Text);
            dr.Read();
            name = dr["name"].ToString();
            dr.Close();
            return name;
        }

        /// <summary>
        /// 根据店铺编号返回国家ID
        /// </summary>
        /// <param name="stroeID"></param>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static int GetCurrencyIdByStoreId(string storeId)
        {
            string sql = "select ID from Country where Name=(select top 1 Country from StoreInfo where StoreID=@num)";
            SqlParameter[] sPara = new SqlParameter[] { new SqlParameter("@num", SqlDbType.NVarChar, 50) };
            sPara[0].Value = storeId;
            return Convert.ToInt32(DBHelper.ExecuteScalar(sql,sPara,CommandType.Text));
        }

        /// <summary>
        /// Get CurrencyID by ProductID  ---DS2012
        /// </summary>
        /// <param name="productID">The ID of the product</param>
        /// <returns>retutrn productID</returns>
        public static int GetCurrencyIDByProductID(int productID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productID",SqlDbType.Int)
            };
            sparams[0].Value = productID;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetCurrencyIDByProductID", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 通过联合查询获取币种ID
        /// </summary>
        /// <param name="countryCode">国家编码</param>
        /// <returns>返回币种ID</returns>
        public static int GetMoreCurrencyIDByCountryCode(string countryCode)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@countryCode",SqlDbType.NVarChar,10)
            };
            int currencyID = 0;
            sparams[0].Value = countryCode;
            try
            {
                currencyID = Convert.ToInt32(DBHelper.ExecuteScalar("GetMoreCurrencyIDByCountryCode", sparams, CommandType.StoredProcedure));
            }

            catch
            {
                return 0;
            }
            return currencyID;
        }

        /// <summary>
        /// 通过联合查询获取标准币种ID
        /// </summary>
        /// <param name="countryCode">国家编码</param>
        /// <returns>返回标准币种ID</returns>
        public static int GetMoreStandardMoneyIDByCountryCode(string countryCode)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@countryCode",SqlDbType.NVarChar,10)
            };
            int standardMoney = 0;
            sparams[0].Value = countryCode;
            try
            {
                standardMoney = Convert.ToInt32(DBHelper.ExecuteScalar("GetMoreStandardMoneyIDByCountryCode", sparams, CommandType.StoredProcedure));
            }

            catch
            {
                return 0;
            }
            return standardMoney;
        }

        /// <summary>
        /// 通过汇率名称获取行数
        /// </summary>
        /// <param name="currencyName">汇率名称</param>
        /// <returns>返回行数</returns>
        public static int GetCurrencyCountByCurrencyName(string currencyName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@currencyName",SqlDbType.NVarChar,40)
            };
            sparams[0].Value = currencyName;
            
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetCurrencyCountByCurrencyName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Get CurrencyName by currencyID
        /// </summary>
        /// <param name="currencyID">currencyID</param>
        /// <returns>return CurrencyName</returns>
        public static string GetCurrencyNameByID(int currencyID)
        {
            SqlParameter[] sparams=new SqlParameter[]
            {
                new SqlParameter("@currencyID",SqlDbType.Int)
            };
            sparams[0].Value = currencyID;

            return Convert.ToString(DBHelper.ExecuteScalar("GetCurrencyNameByID", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取汇率相关信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetCurrencyInfo()
        {            
            return DBHelper.ExecuteDataTable("GetCurrencyInfo", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取所有的汇率信息
        /// </summary>
        /// <param name="tran">要处理的事务</param>
        /// <returns>返回DataTable</returns>
        public static DataTable GetAllCurrencyInfo(SqlTransaction tran)
        {
            return DBHelper.ExecuteDataTable("GetAllCurrencyInfo", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 通过币种名称联合查询获取行数
        /// </summary>
        /// <param name="currencyName">币种名称</param>
        /// <returns>返回行数</returns>
        public static int GetMoreCurrencyCountByCurrencyName(string currencyName)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@currencyName",SqlDbType.NVarChar,40)
            };
            sparams[0].Value = currencyName;
            
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetMoreCurrencyCountByCurrencyName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取该国家相对标准币种的汇率倍数
        /// </summary>
        /// <param name="countryCode">CountryCode</param>
        /// <returns>Return rate times</returns>
        public static decimal GetRate_TimesForStandardMoneyByCountryCode(string countryCode)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@countryCode",SqlDbType.NVarChar,10)
            };
            sparams[0].Value = countryCode;
            return  (decimal)DBHelper.ExecuteScalar("GetRate_TimesForStandardMoneyByCountryCode", sparams, CommandType.StoredProcedure);
        }
       

        /// <summary>
        /// 获取汇率表中所有的ID和币种名称
        /// </summary>
        /// <returns>返回SqlDataReader对象</returns>
        public static IList<CurrencyModel> GetAllCurrencyIDName()
        {
            IList<CurrencyModel> currencyList = new List<CurrencyModel>();
            SqlDataReader dr = DBHelper.ExecuteReader("GetAllCurrencyIDName", CommandType.StoredProcedure);
            while (dr.Read())
            {
                CurrencyModel currencyModel = new CurrencyModel();
                currencyModel.ID = Convert.ToInt32(dr["ID"]);
                currencyModel.Name = dr["Name"].ToString();
                currencyList.Add(currencyModel);
            }
            dr.Close();
            return currencyList;            
        }

        /// <summary>
        /// 获取汇率表中部分汇率ID和币种名称
        /// </summary>
        /// <returns>返回SqlDataReader对象</returns>
        public static IList<CurrencyModel> GetPartCurrencyIDName()
        {            
            IList<CurrencyModel> currencyList = new List<CurrencyModel>(); 
            SqlDataReader dr = DBHelper.ExecuteReader("GetPartCurrencyIDName", CommandType.StoredProcedure);
            while (dr.Read())
            {
                CurrencyModel currencyModel = new CurrencyModel();
                currencyModel.ID=Convert.ToInt32(dr["ID"]);
                currencyModel.Name=dr["Name"].ToString();
                currencyList.Add(currencyModel);
            }
            dr.Close();
            return currencyList;
        }

        /// <summary>
        /// 通过国家名称联合查询币种名称
        /// </summary>
        /// <param name="countryID">国家名称</param>
        /// <returns>返回币种名称</returns>
        public static string GetCurrencyNameByCountryName(string countryName)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@countryName",SqlDbType.NVarChar,40)
            };
            sparams[0].Value = countryName;
            
            return Convert.ToString(DBHelper.ExecuteScalar("GetCurrencyNameByCountryName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 通过产品ID联合查询获取汇率名称
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>返回汇率名称</returns>
        public static string GetMoreCurrencyNameByProductID(int productID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@productID",SqlDbType.Int)
            };
            sparams[0].Value = productID;
            
            return Convert.ToString(DBHelper.ExecuteScalar("GetMoreCurrencyNameByProductID", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 通过国家编码联合查询获取汇率名称
        /// </summary>
        /// <param name="countryCode">国家编码</param>
        /// <returns>返回汇率名称</returns>
        public static string GetMoreCurrencyNameByCountryCode(string countryCode)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@countryCode",SqlDbType.NVarChar,10)
            };
            sparams[0].Value = countryCode;

            return Convert.ToString(DBHelper.ExecuteScalar("GetMoreCurrencyNameByCountryCode", sparams, CommandType.StoredProcedure));
        }

        public static string GetCurrencyNameById(int id)
        {
            object obj=DBHelper.ExecuteScalar("GetCurrencyNameById", new SqlParameter("@id", id), CommandType.StoredProcedure);
            if (obj!=null)
            {
                return obj.ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 绑定货币(货币名称 -- 汇率ID )列表
        /// </summary>
        /// <param name="list">要添加货币的控件</param>
        /// <param name="defaultCurrency">默认的币种名称</param>
        public static void BindCurrencyList(DropDownList list, string defaultCurrency)
        {
            SqlDataReader dr = DBHelper.ExecuteReader("BindCurrencyList", CommandType.StoredProcedure);
            while (dr.Read())
            {
                ListItem list2 = new ListItem(dr["name"].ToString(), dr["id"].ToString());
                //				if(Default_Currency.Trim()=="" && dr["name"].ToString()=="美元")
                //					list2.Selected=true;
                if (defaultCurrency == dr["name"].ToString())
                    list2.Selected = true;
                list.Items.Add(list2);
            }
            dr.Close();
        }

        /// <summary>
        /// Out to excel of the all data of Currency
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_Currency()
        {
            return DBHelper.ExecuteDataTable("OutToExcel_Currency", CommandType.StoredProcedure);
        }
        /// <summary>
        /// 根据国家编号获取支付简称  ---DS2012
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetJCByCountry(int countrycode)
        {
            return DBHelper.ExecuteScalar("select jiecheng from Currency c,country  cc where c.id=cc.rateid and countrycode=@id", new SqlParameter[] { new SqlParameter("@id", countrycode) }, CommandType.Text).ToString();
        }

        /// <summary>
        /// 根据国家编号获取支付汇率  ---DS2012
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static double GetRate(int countrycode)
        {
            return Convert.ToDouble( DBHelper.ExecuteScalar("select rate from Currency c,country  cc where c.id=cc.rateid and countrycode=@id", new SqlParameter[] { new SqlParameter("@id", countrycode) }, CommandType.Text));
        }
    }
}
