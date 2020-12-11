using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Model;
/*
 *创建者：郑华超
 *创建时间：2009-08-28
 *文件名：StoreDataDAL
 *功能：店数据操作类
 */
namespace DAL
{
    public class StoreDataDAL
    {
        /// <summary>
        /// 得到报单的金额
        /// </summary>		
        /// <param name="orderId">报单号</param>
        /// <returns></returns>
        public static double GetOrderTotalMoney(string orderId)
        {
            string SQL_SELECT_TotalMoney = "SELECT TotalMoney FROM MemberOrder WHERE OrderID = @orderId";

            SqlParameter[] para = 
            {
               new SqlParameter("@orderId",orderId)
            
            };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL_SELECT_TotalMoney, para, CommandType.Text);
            double TotalMoney = 0;
            if (reader.Read())
            {
                TotalMoney = Convert.ToDouble(reader[0]);
            }
            reader.Close();
            return TotalMoney;

        }


        #region 得到店铺的剩余报单额

        /// <summary>
        /// 得到店铺的剩余金额(获取老董的存储过程，暂时用自己的方法)
        /// </summary>
        /// <param name="tstrObjectId">店编号</param>
        /// <returns></returns>
        public double GetLeftRegisterMemberMoney(string tstrStoreID)
        {
            SqlParameter[] para = new SqlParameter[] 
            { 
                new SqlParameter("@storeId", tstrStoreID) 
            };

            SqlDataReader reader = DBHelper.ExecuteReader("P_StoreLaveAmount", para, CommandType.StoredProcedure);
            double leftMoney = 0;
            if (reader.Read())
            {
                leftMoney += Convert.ToDouble(reader[0]);
            }
            reader.Close();

            return leftMoney;
            //}

        }

        #endregion


        /// <summary>
        /// 得到店铺的当前库存信息
        /// </summary>		
        /// <param name="storeId">报单号</param>
        /// <returns>库存信息数据表</returns>
        public DataTable GetStoreActualStorageList(string storeId)
        {
            string SQL = "SELECT ProductID , ActualStorage FROM Stock WHERE StoreId =@storeId";
            SqlParameter[] para = new SqlParameter[] 
            { 
                new SqlParameter("@storeId", storeId) 
            };
            DataTable dtactualStorage = new DataTable();
            return DBHelper.ExecuteDataTable(SQL, para, CommandType.Text);

        }

        /// <summary>
        /// 得到店铺库存底线
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public double GetStoreStockDeadLine(string storeId)
        {
            string SQL = "select StorageScalar from d_info where StoreID=@storeId";
            SqlParameter[] para = new SqlParameter[] 
            { 
                new SqlParameter("@storeId", storeId) 
            };

            return Convert.ToDouble(DBHelper.ExecuteScalar(SQL, para, CommandType.Text));
        }



        /// <summary>
        /// 得到该店库存价格
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns>返回价格</returns>
        public double GetStockMoney(string storeId)
        {
            //价格
            double money = 0;
            string SQL = @"select P.PreferentialPrice as Money,D.ActualStorage as Amount
from d_kucun as D,product as P where P.ProductID = D.ProductID and D.StoreID =@storeId";
            SqlParameter[] para = new SqlParameter[] 
            { 
                new SqlParameter("@storeId", storeId) 
            };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            //循环累加
            while (reader.Read())
            {
                money += Convert.ToDouble(reader[0]) * Convert.ToInt64(reader[1]);
            }
            reader.Close();
            return money;
        }

        /// <summary>
        /// 店铺没钱时,最大允许录入的报单金额
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public double GetBalanceScalar(string storeId)
        {
            string SQL = "select TotalMaxMoney from d_info where StoreID=@storeId";
            SqlParameter[] para = new SqlParameter[] 
            { 
                new SqlParameter("@storeId", storeId) 
            };
            return Convert.ToDouble(DBHelper.ExecuteScalar(SQL, para, CommandType.Text));
        }


        #region 根据店铺编号获取店铺信息
        /// <summary>
        /// 根据店铺编号获取店铺信息
        /// </summary>
        /// <param name="storeId">店编号</param>
        /// <returns></returns>
        public StoreInfoModel GetStoreInfoByStoreId(string storeId)
        {
            StoreInfoModel storeInfo = null;
            string sql = "select bank,bankcard from storeinfo where storeid=@num";
            SqlParameter[] spa = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.NVarChar, 50) 
            };
            spa[0].Value = storeId;
            SqlDataReader reader = DBHelper.ExecuteReader(sql,spa,CommandType.Text);
            if (reader.Read())
            {
                storeInfo = new StoreInfoModel();
                storeInfo.BankCode = reader[0].ToString();
                storeInfo.BankCard = reader[1].ToString();
            }

            reader.Close();
            return storeInfo;

        }

        #endregion


        /// <summary>
        /// 根据店编号得到该店id
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string GetStoreIDByNumber(string number)
        {
            string SQL = "select storeid from memberInfo where number=@number";
            SqlParameter[] para = 
            {
              new SqlParameter("@number",number)
            };
            return DBHelper.ExecuteScalar(SQL, para, CommandType.Text).ToString();
        }
    }
}
