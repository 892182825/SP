using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Data;
/**
 * 作者：
 * 时间：
 * 功能：会员自由注册记录到MemberOrder 
 *  
 */

namespace DAL
{
    public class AddFreeOrderDAL
    {
        /// <summary>
        ///  执行存储过程UP_MemberOrder_ADD
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回成功与否</returns> 
        public int AddOrder(MemberOrderModel model, SqlTransaction tran)
        {
            SqlParameter[] para = 
							{	
							    new SqlParameter("@Number", model.Number),
								new SqlParameter("@OrderID", model.OrderId),
								new SqlParameter("@StoreID", model.StoreId),
								new SqlParameter("@TotalMoney",model.TotalMoney),
								new SqlParameter("@TotalPv", model.TotalPv),
								new SqlParameter("@PayExpect", model.PayExpect),
								new SqlParameter("@OrderExpect", model.OrderExpect),
								new SqlParameter("@IsAgain", model.IsAgain),
								new SqlParameter("@OrderDate",  model.OrderDate ),
								new SqlParameter("@Err", model.Err),
								new SqlParameter("@Remark" , model.Remark),						
								new SqlParameter("@DefrayState" ,model.DefrayState),
								new SqlParameter("@PayCurrency", model.PayCurrency),
								new SqlParameter("@PayMoney", model.PayMoney),
								new SqlParameter("@StandardCurrency" , model.StandardCurrency),		
								new SqlParameter("@StandardCurrencyMoney" ,model.StandardcurrencyMoney),
								new SqlParameter("@OperateIP",model.OperateIp),
								new SqlParameter("@OperateNumber",model.OperateNumber),
								new SqlParameter("@DefrayType" ,model.DefrayType),
								new SqlParameter("@CarryMoney",model.CarryMoney),
                                //new SqlParameter("@RemittancesId",model.RemittancesId),
                                //new SqlParameter("@ElectronicAccountId",model.ElectronicaccountId),
				                new SqlParameter("@ordertype",model.OrderType),
                                new SqlParameter("@CCPCCode",CommonDataDAL.GetCPCCode(model.ConCity)),
                                new SqlParameter("@ConAddress",model.ConAddress),
								new SqlParameter("@ConTelphone",model.ConTelPhone),
								new SqlParameter("@ConMobilPhone",model.ConMobilPhone),
								new SqlParameter("@ConPost",model.ConPost),
								new SqlParameter("@Consignee",model.Consignee), 
				                new SqlParameter("@ConZipCode",model.ConZipCode),
                                new SqlParameter("@ElectronicaccountId",model.ElectronicaccountId)
						    
							};
            return DBHelper.ExecuteNonQuery(tran, "UP_MemberOrder_ADD2", para, CommandType.StoredProcedure);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<int> GetProductID()
        {
            List<int> list = new List<int>();
            string SQL = "select ProductID from product where isfold = 0";
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, CommandType.Text);
            while (reader.Read())
            {
                list.Add(Convert.ToInt32(reader[0]));
            }
            reader.Close();
            return list;
        }

        public List<int> GetProductID(string storeId)
        {
            List<int> list = new List<int>();
            string SQL = @"declare @num varchar(20)
        select @num=scpccode from storeInfo where storeId=@storeid set @num=substring(@num,1, 2)     Select  a.ProductID  From Product a where countryCode=@num";
            SqlParameter spa = new SqlParameter("@storeid", SqlDbType.NVarChar, 50);
            spa.Value = storeId;
            SqlDataReader reader = DBHelper.ExecuteReader(SQL,spa,CommandType.Text);
            while (reader.Read())
            {
                list.Add(Convert.ToInt32(reader[0]));
            }
            reader.Close();
            return list;
        }


        /// <summary>
        /// 得到该城市邮编
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public string GetAddressCode(string xian)
        {
            object obj = null;
            string SQL = "select PostCode from city where xian=@xian";
            SqlParameter[] para = 
            {
              new SqlParameter("@xian",xian)
            
            };
            obj = DBHelper.ExecuteScalar(SQL, para, CommandType.Text);
            if (obj == null)
            {
                return "未找到该城市邮编！";
            }
            return obj.ToString();
        }

        /// <summary>
        /// 得到语言
        /// </summary>
        /// <returns></returns>
        //public List<LanguageModel> GetLanguage() 
        //{
        //    List<LanguageModel> list = new List<LanguageModel>();
        //    LanguageModel languageModel = null;
        //    string SQL = "select id,name from language";
        //    SqlDataReader reader = DBHelper.ExecuteReader(SQL);
        //    while (reader.Read())
        //    {
        //        languageModel = new LanguageModel();
        //        languageModel.ID = Convert.ToInt32(reader["id"]);
        //        languageModel.Name = reader["name"].ToString();
        //        list.Add(languageModel);
        //    }
        //    return list;
        //}

        /// <summary>
        /// 得到汇率名和汇率 
        /// </summary>
        /// <returns></returns>
        public static List<CurrencyModel> GetRateAndName()
        {
            List<CurrencyModel> list = new List<CurrencyModel>();
            CurrencyModel currencyModel = null;
            string SQL = "Select id,name,rate From currency where bzflag=1 order by id ";
            SqlDataReader reader = DBHelper.ExecuteReader(SQL);
            while (reader.Read())
            {
                currencyModel = new CurrencyModel();
                currencyModel.Name = reader["Name"].ToString();
                currencyModel.Rate = Convert.ToDouble(reader["rate"]);
                currencyModel.ID = Convert.ToInt32(reader["id"]);
                list.Add(currencyModel);
            }
            reader.Close();
            return list;
        }
        /// <summary>
        /// 得到汇率名和汇率 
        /// </summary>
        /// <returns></returns>
        public List<CurrencyModel> GetRateAndName(string language)
        {
            String sql = "select id from language where name=@num";
            SqlParameter spa = new SqlParameter("@num",SqlDbType.NVarChar,50);
            spa.Value = language;
            int ID = Convert.ToInt32(DBHelper.ExecuteScalar(sql,spa,CommandType.Text));
            List<CurrencyModel> list = new List<CurrencyModel>();
            CurrencyModel currencyModel = null;
            string SQL = @"select rate,(select languagename from LanguageTrans where 
							LanguageTrans.OldID=currency.id and 
							LanguageTrans.Columnsname='name' and languageid=@ID) as name
							from currency";

            SqlParameter[] para =
            {
              new SqlParameter("@ID",ID)
            };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            while (reader.Read())
            {
                currencyModel = new CurrencyModel();
                currencyModel.Name = reader["Name"].ToString();
                currencyModel.Rate = Convert.ToDouble(reader["Rate"]);
                list.Add(currencyModel);
            }
            reader.Close();
            return list;
        }


        /// <summary>
        /// 得到支付货币转换总金额
        /// </summary>
        /// <param name="name">当前货币名称</param>
        /// <param name="zf_huilv">该货币汇率</param>
        /// <param name="storeId">店ID</param>
        /// <param name="zongjing">总计</param>
        /// <param name="BzMoney">当前金额通过该店汇率得到的金额</param>
        /// <param name="BzHb">标准货币</param>
        /// <param name="zfHbId"></param>
        /// <returns>汇率表对应当前货币的ID</returns>
        public decimal GetBzMoney(string name, decimal zf_huilv,MemberOrderModel mo)
        {
            
            int BzHb = 0;
            decimal BzMoney = 0;
            decimal D_huilv = 0;    
            decimal zfMoney = 0;
            String sql = "select id from currency where name=@num";
            SqlParameter spa = new SqlParameter("@num", SqlDbType.NVarChar, 20);
            spa.Value = name;
            mo.PayCurrency = Convert.ToInt32(DBHelper.ExecuteScalar(sql,spa,CommandType.Text));
            BzHb = Convert.ToInt32(DBHelper.ExecuteScalar("select distinct StandardMoney from currency"));
            sql = "select a.Rate from currency a,storeInfo b where a.id=b.currency and b.storeid=@num";
            spa = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            spa.Value = mo.StoreId;
            D_huilv = Convert.ToDecimal(DBHelper.ExecuteScalar(sql, spa, CommandType.Text));
            BzMoney = Convert.ToDecimal((Convert.ToDecimal(mo.TotalMoney) * D_huilv).ToString("0.0000"));
            zfMoney = Convert.ToDecimal((Convert.ToDecimal(BzMoney / zf_huilv)).ToString("0.0000"));
            mo.TotalMoney = Convert.ToDecimal(Convert.ToDecimal(mo.TotalMoney) * D_huilv);
            return zfMoney;
        }

        /// <summary>
        /// 把店铺报单款转换成标准币种
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns>汇率</returns>
        public static double GetBzMoney(string storeid,double totalmoney )
        {
            String sql = "select a.Rate from currency awhere a.id=1 ";
            SqlParameter spa = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            spa.Value = storeid;
            double D_huilv = Convert.ToDouble(DBHelper.ExecuteScalar(sql,spa,CommandType.Text));
            return totalmoney * D_huilv;
        }

        /// <summary>
        /// 获取店铺汇率
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns>标准币种金额</returns>
        public static double GetBzType(string storeid)
        {
            String sql = "select a.Rate from currency a,storeInfo b where a.id=b.currency and b.storeid=@num";
            SqlParameter spa = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            spa.Value = storeid;
            return Convert.ToDouble(DBHelper.ExecuteScalar(sql, spa, CommandType.Text));
        }

        /// <summary>
        /// 根据币种类型获取汇率
        /// </summary>
        /// <param name="id">币种ID</param>
        /// <returns>汇率</returns>
        public static double GetBzHl(int id)
        {
            return Convert.ToDouble(DBHelper.ExecuteScalar("select rate from currency where id=@id", new SqlParameter[] { new SqlParameter("@id", id) }, CommandType.Text));
        }

        /// <summary>
        /// 通过标准汇率
        /// </summary>
        /// <param name="zongjing"></param>
        /// <returns></returns>
        public decimal GetBzMoney(decimal zongjing)
        {
            decimal C_huilv = Convert.ToDecimal(DBHelper.ExecuteScalar("select Rate from currency where id= StandardMoney"));
            return Convert.ToDecimal((Convert.ToDecimal(zongjing * C_huilv)).ToString("0.0000"));
        }

        /// <summary>
        /// 获取店铺汇率代码
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns>标准币种金额</returns>
        public static int GetBzTypeId(string storeid)
        {
            String sql = "select currency from  storeInfo where storeid=@num";
            SqlParameter spa = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            spa.Value = storeid;
            return Convert.ToInt32(DBHelper.ExecuteScalar(sql, spa, CommandType.Text));
        }

        /// <summary>
        /// 获取店铺汇率代码
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns>标准币种金额</returns>
        public static int GetBzTypeId(string storeid,SqlTransaction tran)
        {
            string strSql = "select currency from  storeInfo where storeid=@storeID";
            SqlParameter[] para = {
                                      new SqlParameter("@storeID",storeid)
                                  };
            return Convert.ToInt32(DBHelper.ExecuteScalar(tran, strSql, para, CommandType.Text));
        }


        /// <summary>
        /// 查找该店是否存在
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public int StoreIsExist(string storeId)
        {

            string SQL = "select count(*) from  storeInfo where storeId=@storeId";
            SqlParameter[] para =
             {
               new SqlParameter("@storeId",storeId)
             };
            return (int)DBHelper.ExecuteScalar(SQL, para, CommandType.Text);
        }


        /// <summary>
        /// 获得推荐人姓名
        /// </summary>
        /// <returns></returns>
        public string GetParentName(string number)
        {
            string name = null;
            string SQL = "select name from memberInfo where number=@number";
            SqlParameter[] para =
            {
              new SqlParameter("@number",number)
            
            };

            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            if (reader.Read())
            {
                name = reader[0].ToString();
            }
            reader.Close();
            return name;
        }


        /// <summary>
        /// 获得安置人姓名
        /// </summary>
        /// <returns></returns>
        public string GetParentName2(string number)
        {
            string name = null;
            string SQL = "select name from memberInfo where number=@number";
            SqlParameter[] para =
            {
              new SqlParameter("@number",number)
            
            };

            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            if (reader.Read())
            {
                name = reader[0].ToString();
            }
            reader.Close();
            return name;
        }

        /// <summary>
        ///  通过城市ID绑定银行
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public List<MemberBankModel> GetBank(int countryCode, string language)
        {
            List<MemberBankModel> model = new List<MemberBankModel>();
            MemberBankModel bankModel = null;
            string SQL = null;
            if (language.ToLower() == "chinese" || language.ToLower() == "中文")
            {

                SQL = "select BankID, BankName,BankCode, CountryCode from MemberBank where CountryCode=@countryCode";
                
            }
            else
            {

                int languageID = LanguageDAL.GetLanguageIDByName(language);
                SQL = @"select BankID,
		        (
			        select LanguageName 
			        from LanguageTrans
			        where	LanguageTrans.OldID=MemberBank.BankID 
					        and LanguageTrans.ColumnsName='BankName' 
					        and LanguageID=" + languageID + ")as BankName,CountryCode,BankCode from MemberBank";


            }
            SqlParameter[] para =
            {
              new SqlParameter("@countryCode",SqlDbType.Int)
            };
            para[0].Value = countryCode;
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            while (reader.Read())
            {
                bankModel = new MemberBankModel();
                bankModel.BankID = Convert.ToInt32(reader["BankID"]);
                bankModel.BankName = reader["BankName"].ToString();
                bankModel.CountryCode = Convert.ToInt32(reader["CountryCode"]);
                bankModel.BankCode = reader["BankCode"].ToString();
                model.Add(bankModel);
            }
            reader.Close();
            return model;

        }
        /// <summary>
        /// 获得银行的ID
        /// </summary>
        /// <param name="bankName"></param>
        /// <returns></returns>
        public int SelectBankId(string bankName)
        {
            string SQL = "select BankID from MemberBank where bankName=@bankName";
            SqlParameter[] para =
            {
              new SqlParameter("@bankName",bankName)
            };
            return (int)DBHelper.ExecuteScalar(SQL, para, CommandType.Text);
        }

        /// <summary>
        /// 通过店铺首页得到的国家名字得到国家ID
        /// </summary>
        /// <param name="countriName"></param>
        /// <returns></returns>
        public int GetCountryId(string countriName)
        {
            string SQL = "select id from country where name=@countriName";
            SqlParameter[] para =
            {
              new SqlParameter("@countriName",countriName)
            };
            return (int)DBHelper.ExecuteScalar(SQL, para, CommandType.Text);
        }

        /// <summary>
        /// 获得该保单的总pv
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public double GetTotalPV(string orderID, out string PayExpectNum)
        {
            PayExpectNum = null;
            string SQL = "select totalPv , PayExpectNum from memberOrder where orderID=@orderID";
            SqlParameter[] para =
            {
              new SqlParameter("@orderID",orderID)
              
            };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            if (reader.Read())
            {
                PayExpectNum = reader[1].ToString();
                return Convert.ToDouble(reader[0]);
            }
            PayExpectNum = null;
            reader.Close();
            return 0;
        }

        public double GetTotalmoney(string orderID)
        {
            double totalmoney = 0;
            string SQL = "select lackproductmoney from memberOrder where orderID=@orderID";
            SqlParameter[] para =
            {
              new SqlParameter("@orderID",orderID)
              
            };
            totalmoney = Convert.ToDouble(DBHelper.ExecuteScalar(SQL,para,CommandType.Text));
            return totalmoney;
        }

        /// <summary>
        /// 获取保单底线
        /// </summary>
        /// <returns></returns>
        public double GetBottomLine()
        {
            string SQL = "select orderBaseLine  from MemOrderLine";
            SqlDataReader reader = DBHelper.ExecuteReader(SQL);
            if (reader.Read())
            {
                return Convert.ToInt32(reader["orderBaseLine"]);
            }
            reader.Close();
            return 0;
        }

        /// <summary>
        /// 根据编号查询地址
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<string> ChoseArea(string code)
        {
            List<string> list = new List<string>();
            string sql = "select country,province,city from city where cpccode=@code";
            SqlParameter[] para =
            {
              new SqlParameter("@code",code)
              
            };
            SqlDataReader reader = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            if (reader.Read())
            {
                list.Add(reader["country"].ToString());
                list.Add(reader["province"].ToString());
                list.Add(reader["city"].ToString());
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 根据地址查编号
        /// </summary>
        /// <param name="countri"></param>
        /// <param name="province"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public string ChoseArea2(string country, string province, string city)
        {
            string sql = "select cpccode from city where country=@country and province=@province and city=@city";
            SqlParameter[] para =
            {
              new SqlParameter("@country",country),
              new SqlParameter("@province",province),
              new SqlParameter("@city",city)

            };
            SqlDataReader reader = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            if (reader.Read())
            {
                return reader["cpccode"].ToString();

            }
            reader.Close();
            return null;

        }

        /// <summary>
        /// 根据币种id获取汇率
        /// </summary>
        /// <param name="countriName"></param>
        /// <returns></returns>
        public static double GetCurren(int  currencyid)
        {
            string SQL = "select (rate/(select rate from currency where id=(select top 1 standardmoney from currency))) as rate from Currency where id=@currencyid";
            SqlParameter[] para =
            {
              new SqlParameter("@currencyid",currencyid)
            };
            object o = DBHelper.ExecuteScalar(SQL, para, CommandType.Text);
            if (o != null)
            {
                return Convert.ToDouble(DBHelper.ExecuteScalar(SQL, para, CommandType.Text).ToString());
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 获取系统标准币种
        /// </summary>
        /// <param name="countriName"></param>
        /// <returns></returns>
        public static int GetCurrenT()
        {
            string SQL = "select distinct(standardmoney) from currency";
            return Convert.ToInt32(DBHelper.ExecuteScalar(SQL,CommandType.Text).ToString());
        }

        /// <summary>
        /// 查找该安置编号是否存在
        /// </summary>
        /// <param name="placeNumber"></param>
        /// <returns></returns>
        public int CheckPlaceMentIsExist(string placeNumber) 
        {
            string sql = "select count(*) from memberinfo where number=@placeNumber";
            SqlParameter[] para =
            {
              new SqlParameter("@placeNumber",placeNumber)
            };
            return Convert.ToInt32(DBHelper.ExecuteScalar(sql, CommandType.Text));
        }

    }
}
