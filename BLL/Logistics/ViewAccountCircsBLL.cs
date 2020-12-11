using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Other;
using DAL;
using System.Data;
namespace BLL.Logistics
{
    /// <summary>
    /// 账户情况（店铺子系统）
    /// </summary>
    public class ViewAccountCircsBLL
    {
        InventoryDocDAL inventoryDocDAL = new InventoryDocDAL();
        ConfigDAL configDAL = new ConfigDAL();
        /// <summary>
        /// 返回对应期数的汇款情况
        /// </summary>
        /// <returns></returns>
        /// 

        public IList<RemittancesModel> GetRemittanceListByExpect(int ExpectNum, string storeId, PaginationModel pagin)
        {
            return null;
        }

        /// <summary>
        /// 返回系统最大期数
        /// </summary>
        /// <returns></returns>
        public static int GetMaxExpectNum()
        {
            return ConfigDAL.GetMaxExpectNum();
        }
        /// <summary>
        /// 返回所有的期数和日期
        /// </summary>
        /// <returns></returns>
        public DataTable GetExpectNumAndDates()
        {
            return ConfigDAL.GetExpectNumAndDates();
        }
        /// <summary>
        ///获得总金额用于店铺帐户情况
        /// 
        /// <returns></returns>
        public static double SelRemitMoneyByStoreID(string storeID)
        {
            return InventoryDocDAL.SelRemitMoneyByStoreID(storeID);
        }

        /// <summary>
        /// Get CurrencyName by currencyID
        /// </summary>
        /// <param name="currencyID">currencyID</param>
        /// <returns>return CurrencyName</returns>
        public static string GetCurrencyNameByID(int currencyID)
        {
            return CurrencyDAL.GetCurrencyNameByID(currencyID);
        }

        /// <summary>
        /// Get statistics money information by storeID——ds2012——tianfeng
        /// </summary>
        /// <param name="storeID">storeID</param>
        /// <returns>return Datatable object</returns>
        public static DataTable GetStatisticsMoneyInfoByStoreID(string storeID,int payExpectNum)
        {
            return RemittancesDAL.GetStatisticsMoneyInfoByStoreID(storeID,payExpectNum);
        }        

        /// <summary>
        /// 获得店铺汇款信息
        /// 
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static DataTable GetRemittancesByStoreID(string storeID)
        {
            return InventoryDocDAL.GetRemittancesByStoreID(storeID);
        }
        /// <summary>
        ///  查询到的已审汇款总额——ds2012——tianfeng
        /// 
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="paymentExpectNum"></param>
        /// <returns></returns>
        /// 
        public static double SelRemitMoneyByStoreIDAnExpectNum_Y(string storeID, int PayExpectNum)
        {
            return InventoryDocDAL.dSelRemitMoneyByStoreIDAnExpectNum_Y(storeID, PayExpectNum);
        }
        /// <summary>
        ///  未审汇款总额——ds2012——tianfeng
        /// 
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="paymentExpectNum"></param>
        /// <returns></returns>

        public static double SelRemitMoneyByStoreIDAnExpectNum_N(string storeID, int PayExpectNum)
        {
            return InventoryDocDAL.SelRemitMoneyByStoreIDAnExpectNum_N(storeID, PayExpectNum);
        }
        /// <summary>
        ///  库存情况根据店铺和日期
        /// 
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="paymentExpectNum"></param>
        /// <returns></returns>
        public static DataTable GetStock(string storeID, int expectNum)
        {
            return InventoryDocDAL.GetStock(storeID, expectNum);
        }
        //public DataTable GetStock(string storeID, int pageIndex, string key, int pageSize, string condition, out int recordCount, out int pageCount,int expectNum)
        //{
        //    return inventoryDocDAL.GetStock( storeID,pageIndex,key, pageSize,  condition, out  recordCount, out  pageCount,expectNum);
        //}
        /// <summary>
        ///  库存情况全部
        /// 
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="paymentExpectNum"></param>
        /// <returns></returns>
        public static DataTable GetStockBySoteID(string storeID)
        {
            return InventoryDocDAL.GetStockBySoteID(storeID);
        }

    }
}
