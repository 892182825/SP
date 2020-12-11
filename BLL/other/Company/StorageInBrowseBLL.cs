using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;

namespace BLL.other.Company
{
    public class StorageInBrowseBLL
    {
        #region 入库单详细信息
        public static DataTable getStoageInDetails(string ID)
        {
            return InventoryDocDetailsDAL.getStoageInDetails(ID);
        }
        #endregion

        public static DataTable getStoageIn(string ID)
        {
            return InventoryDocDetailsDAL.getStoageIn(ID);
        }

        /// <summary>
        /// 审核入库单并更改库存  ---DS2012
        /// </summary>
        /// <param name="DocAuditer"></param>
        /// <param name="DcAuditTime"></param>
        /// <param name="OperateIP"></param>
        /// <param name="OperateNum"></param>
        /// <param name="DocID"></param>
        /// <param name="TempWareHouseID"></param>
        /// <param name="changwei"></param>
        /// <returns></returns>
        public static int checkDoc(string DocAuditer, DateTime DcAuditTime, string OperateIP, string OperateNum, string DocID, string TempWareHouseID,int changwei)
        {
            return InventoryDocDAL.checkDoc(DocAuditer, DcAuditTime, OperateIP, OperateNum, DocID, TempWareHouseID,changwei);

        }
        public static int updDocTypeName(DateTime CloseDate, string DocID, string OperateIP, string OperateNum)
        {
            return InventoryDocDAL.updDocTypeName(CloseDate, DocID, OperateIP, OperateNum);
        }

        /// <summary>
        /// 删除入库单  ---DS2012
        /// </summary>
        /// <param name="DocID"></param>
        /// <returns></returns>
        public static int delDoc(string DocID)
        {
            return InventoryDocDAL.delDoc(DocID);
        }

        public static string GetWarehouseName(string WareHouseId)
        {
            return InventoryDocDAL.GetWarehouseName(WareHouseId);
        }

        public static string GetDepotSeatName(string DepotSeatID)
        {
            return InventoryDocDAL.GetDepotSeatName(DepotSeatID);
        }
        public static string GetCurrencyName(string countryid)
        { 
             return InventoryDocDAL.GetCurrencyName(countryid);
        }
        public string GetCurrencyNameByPID(string productid)
        {
            InventoryDocDAL idd = new InventoryDocDAL();
            return "";//idd.GetCurrencyNameByPID(productid);
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
        /// Bind the CountryName and CountryCode
        /// </summary>
        /// <returns></returns>
        public static DataTable BindCountryList()
        {
            return CountryDAL.BindCountryList();
        }

        /// <summary>
        /// Out to excel of the data about InventoryDoc   ---DS2012
        /// </summary>
        /// <param name="condition">Condiiton</param>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_InventoryDoc_More(string condition)
        {
            return InventoryDocDAL.OutToExcel_InventoryDoc_More(condition);
        }

        /// <summary>
        /// Judge the docId whether Auditing by docId  ---DS2012
        /// </summary>
        /// <param name="docId">DocId</param>
        /// <param name="i">1 Stand for effective,0 stand for Invalid</param>
        /// <returns>Return the counts of the auditing by docId</returns>
        public static int IsAuditingByDocId(string docId, int i)
        {
            return InventoryDocDAL.IsAuditingByDocId(docId,i);
        }

        /// <summary>
        /// Judge the docId whether exists before update or delete ---DS2012
        /// </summary>
        /// <param name="docId">DocId</param>
        /// <returns>Return the counts of the docId by docId</returns>
        public static int DocIdIsExistByDocId(string docId)
        {
            return InventoryDocDAL.DocIdIsExistByDocId(docId);
        }

        /// <summary>
        /// 获取币种Name
        /// </summary>
        /// <param name="countryid"></param>
        /// <returns></returns>
        public static string GetECurrencyName(string countryid)
        {
            return InventoryDocDAL.GetECurrencyName(countryid);
        }

        /// <summary>
        /// 获取币种名称  ---DS2012
        /// </summary>
        /// <param name="Currency"></param>
        /// <returns></returns>
        public static string GetCurrencyNameByID(string Currency)
        {
            return InventoryDocDAL.GetCurrencyNameByID(Currency);
        }
    }
}
