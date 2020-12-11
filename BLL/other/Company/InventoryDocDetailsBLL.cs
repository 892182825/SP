using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using Model;
using System.Data.SqlClient;

namespace BLL.other.Company
{
    public class InventoryDocDetailsBLL
    {
        
        #region 入库单明细
        /// <summary>
        /// 分页(查看入库单明细)
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="condition"></param>
        /// <param name="key"></param>
        /// <param name="RecordCount"></param>
        /// <param name="PageCount"></param>
        /// <returns></returns>
        public DataTable InStoreOrderDetails(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
           
             InventoryDocDetailsDAL dao = new InventoryDocDetailsDAL();
             return dao.InStoreOrderDetails(PageIndex, PageSize, table, columns, condition, key, out RecordCount, out PageCount);
        }
        #endregion

         /// <summary>
        /// 产生单据明细
        /// </summary>
        /// <param name="DocID"></param>
        /// <returns></returns>
        public static DataTable GetInventoryDocDetailsByDocID(string DocID)
        {
            InventoryDocDetailsDAL insert=new InventoryDocDetailsDAL ();
            return insert.GetInventoryDocDetailsByDocID(DocID);
        }

        public static DataTable GetProductsById(string docId)
        {
            return InventoryDocDetailsDAL.GetProductsById(docId);
        }

        public static DataTable GetProductsByIdTwo(string docId)
        {
            return InventoryDocDetailsDAL.GetProductsByIdTwo(docId);
        }

        public static DataTable GetProductsBillById(string docId)
        {
            return InventoryDocDetailsDAL.GetProductsBillById(docId);
        }

        public static DataTable GetProductsByIdDB(string docId)
        {
            return InventoryDocDetailsDAL.GetProductsByIdDB(docId);
        }
        
        #region 生成单据明细
        /// <summary>
        /// 生成单据明细
        /// </summary>
        /// <param name="tran">处理事务</param>
        /// <param name="tobjopda_depotManageDoc">某种单据明细类对象数组</param>
        /// <returns>返回插入所影响的行数</returns>
        public static int CreateBillofDocumentDetails( ArrayList tobjopda_docDetailsList)
        {
            InventoryDocDetailsDAL dao = new InventoryDocDetailsDAL();
            return dao.CreateBillofDocumentDetails(tobjopda_docDetailsList);
        }
        #endregion
        #region 入库单详细信息
        public static DataTable getStoageInDetails(string ID)
        {
            return InventoryDocDetailsDAL.getStoageInDetails(ID);
           
        }
        #endregion

        #region 生成单据明细
        /// <summary>
        /// 生成单据明细
        /// </summary>
        /// <param name="tran">处理事务</param>
        /// <param name="tobjopda_depotManageDoc">某种单据明细类对象数组</param>
        /// <returns></returns>
        public static void CreateInventoryDocDetails(SqlTransaction tran, ArrayList inventoryDocDetailsList)
        {
            InventoryDocDetailsDAL dao = new InventoryDocDetailsDAL();
            dao.CreateInventoryDocDetails(tran, inventoryDocDetailsList);

        }
        #endregion

        /// <summary>
        /// 根据订单号查询出订单明细表的产品ID和数量
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public static DataTable GetProductIdAndQuantityByDocId(string docId)
        {
            return InventoryDocDetailsDAL.GetProductIdAndQuantityByDocId(docId);
        }
        /// <summary>
        /// 根据订单号查询出订单明细表
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public static DataTable GetProductsByDocId(string docId)
        {
            return InventoryDocDetailsDAL.GetProductsByDocId(docId);
        }
        //根据店名得到店的库存信息在换货中用此方法
        public StoreInfoModel GetStoreInfoByStoreID(string storeID)
        {
            InventoryDocDetailsDAL dao = new InventoryDocDetailsDAL();
            return dao.GetStoreInfoByStoreID(storeID);
        }

        #region 生成一个单据，包括各种单据［出库，入库，红单，退货等］
        /// <summary>
        /// 生成一个单据，包括各种单据［出库，入库，红单，退货等］，返回受影响的行数
        /// </summary>
        /// <param name="tran">处理事务</param>
        /// <param name="tobjopda_depotManageDoc">某种单据类对象</param>
        /// <returns></returns>
        public static int CreateNewBillofDocument(SqlTransaction tran, InventoryDocModel tobjopda_depotManageDoc)
        {
            return InventoryDocDetailsDAL.CreateNewBillofDocument(tran, tobjopda_depotManageDoc);
        }
        #endregion
    }
}
