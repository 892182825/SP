using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Other;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BLL.other.Company
{
    /// <summary>
    /// 单据
    /// </summary>
    public class InventoryDocBLL
    {
         #region print
        /// <summary>
        /// Get inventory details by docID(Write by WangHua at 2009-11-26)
        /// </summary>
        /// <param name="docID"></param>
        /// <returns>return PrintInventoryDoc model</returns>
        public static PrintInventoryDoc PrintInventoryDocDetails(string docID)
        {
            return InventoryDocDAL.PrintInventoryDocDetails(docID);
        }

        public static DataTable PrintMemberOrderDocDetails(string OrderID)
        {
            return InventoryDocDAL.PrintMemberOrderDocDetails(OrderID);
        }

        /// <summary>
        /// Display inventory details by docID(Write by WangHua at 2009-11-26)
        /// </summary>
        /// <param name="docID">docID</param>
        /// <returns>return DataTable object</returns>
        public static DataTable DisplayInventoryDocDetails(string docID)
        {
            return InventoryDocDAL.DisplayInventoryDocDetails(docID);
        }

        public static DataTable DisplayMemberOrderDocDetails(string OrderID)
        {
            return InventoryDocDAL.DisplayMemberOrderDocDetails(OrderID);
        }
        #endregion
        #region 库存调拨
        /// <summary>
        /// 库存调拨
        /// </summary>
        /// <param name="DepotSeatID"></param>
        /// <param name="sum"></param>
        /// <param name="productid"></param>
        /// <returns></returns>
        public static bool productReWareHOuse(int outWarehouse,int outDepotSeatID,int inwarehouse, int inDepotSeatID, double sum, int productid)
        {
              return InventoryDocDAL.productReWareHOuse(outWarehouse,outDepotSeatID, inwarehouse,inDepotSeatID, sum, productid);
        }
        #endregion

        //wujinjian
        public static string SetDiaoBo(ArrayList inventoryDocDetailsModels, int outWareHouse, int outDepotSeatID, int inWareHouse, int inDepotSeatID, InventoryDocModel tobjopda_depotManageDoc)
        {
            return InventoryDocDAL.SetDiaoBo(inventoryDocDetailsModels, outWareHouse, outDepotSeatID, inWareHouse, inDepotSeatID, tobjopda_depotManageDoc);
        }

        #region 库存报损
        /// <summary>
        /// 库存报损
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public bool addReportDemage(string DocAuditer, string OperateIP, string OperateNum, string DocID)
        {
            InventoryDocDAL dao = new InventoryDocDAL();
            return dao.addReportDemage(DocAuditer,OperateIP,OperateNum,DocID);

        }

        public static string GetPoc(EnumOrderFormType orderType)
        {
            return InventoryDocDAL.GetPoc(orderType);
        }
        /// <summary>
        /// 修改金额
        /// </summary>
        /// <param name="sun"></param>
        /// <param name="productid"></param>
        /// <param name="DepotSeatID"></param>
        /// <returns></returns>
        public bool ProductReportDamage(int sun, int productid, int warehouseid,int DepotSeatID)
        {
           //InventoryDocDAL dao = new InventoryDocDAL();
           //return dao.ProductReportDamage(sun, productid, warehouseid, DepotSeatID);

            return false;
        }
        #endregion


        #region 入库单查询
        /// <summary>
        /// 入库单查询
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
        public DataTable InStoreOrder(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
            InventoryDocDAL dao = new InventoryDocDAL();
            return dao.InStoreOrder(PageIndex,PageSize,table,columns,condition,key,out RecordCount,out PageCount);
        }
        #endregion

        #region 出库查询
        /// <summary>
        /// 出库单
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <param name="moneyType"></param>
        /// <param name="wareHouseID"></param>
        /// <returns></returns>
        public DataTable outStoreOrder(DateTime fromTime, DateTime toTime, int moneyType, int wareHouseID, int DepotSeatID)
        {
           //return InventoryDocDAL.outStoreOrder(fromTime, toTime, moneyType, wareHouseID,DepotSeatID);
            return null;
        }
        #endregion

        #region 入库审批(查询没有审核的单据)
        /// <summary>
        /// 入库审批（分页：未审核的单据）
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
        public DataTable UnCheckDocType(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
             InventoryDocDAL dao = new InventoryDocDAL();
             return dao.UnCheckDocType(PageIndex,PageSize,table,columns,condition,key,out RecordCount,out PageCount);
        }
        #endregion

        #region 根据单据编号得到入库单的信息
        public InventoryDocModel getOrderInfo(string billID)
        {
           return InventoryDocDAL.getOrderInfo(billID);
        }
        #endregion

        #region 获得产品列表
        public DataTable getProduct(string billID)
        {
             return InventoryDocDAL.getProduct(billID);
        }
        #endregion

        #region 检查批次是否已存在
        public int CheckBatch(string availableOrderID, string BatchCode)
        {
             return InventoryDocDAL.CheckBatch(availableOrderID, BatchCode);
        }
        #endregion

        #region 更新产品入库的信息
        public int updAndSaveOrder(InventoryDocModel idm, ArrayList list)
        {
             return InventoryDocDAL.updAndSaveOrder(idm, list);
        }
        #endregion

        #region 获取出库产品(Outstock)
        /// <summary>
        /// 获取出库产品(Outstock)
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public static DataTable GetOutProduct(string storeOrderID)
        {
             return InventoryDocDAL.GetOutProduct(storeOrderID);
        }
        #endregion

        #region 获取产品原来订的数量(Outstock)
        /// <summary>
        /// 获取产品原来订的数量(Outstock)
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public static DataTable GetProductQuantity(string storeOrderID)
        {
             return InventoryDocDAL.GetProductQuantity(storeOrderID);
        }
        #endregion

        #region 获取店铺库存信息用于换货
        /// <summary>
        /// 获取店铺库存信息用于换货
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public DataTable GetStoreInfo(string storeID)
        {
              InventoryDocDAL dao = new InventoryDocDAL();
              return dao.GetStoreInfo(storeID);
        }
        #endregion

        #region 生成一个单据，包括各种单据［出库，入库，红单，退货等］
        /// <summary>
        /// 生成一个单据，包括各种单据［出库，入库，红单，退货等］，返回受影响的行数
        /// </summary>
        /// <param name="tran">处理事务</param>
        /// <param name="inventoryDocModel">某种单据类对象</param>
        /// <returns></returns>
        public int CreateInventoryDoc(SqlTransaction tran, InventoryDocModel inventoryDocModel)
        {
            return InventoryDocDAL.CreateInventoryDoc(tran, inventoryDocModel);
        }
        #endregion

        #region 通过入库批次获取入库批次行数
        /// <summary>
        /// 通过入库批次获取入库批次行数
        /// </summary>
        /// <param name="batchCode">入库批次</param>
        /// <returns>返回入库批次行数</returns>
        public int GetCountByBatchCode(string batchCode)
        {
           return InventoryDocDAL.GetCountByBatchCode(batchCode);
        }
        #endregion

        #region 从库存单据表中获取最大的ID
        /// <summary>
        /// 从库存单据表中获取最大的ID
        /// </summary>
        /// <returns>返回SqlDataReader对象</returns>
        public SqlDataReader GetMaxIDFromInventoryDoc()
        {
            return InventoryDocDAL.GetMaxIDFromInventoryDoc();
        }
        #endregion

        #region 获取新的订单号
        /// <summary>
        /// 获取新的订单号
        /// </summary>
        /// <param name="enumOrderType">单据类型</param>
        /// <returns>返回新订单号</returns>
        public string GetNewOrderID(EnumOrderFormType enumOrderType)
        {
            return InventoryDocDAL.GetNewOrderID(enumOrderType);
        }
        #endregion
        #region 确认出库 按钮。并生成出库单
        /// <summary>
        /// 确认出库 按钮。并生成出库单
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        //public Boolean OutOrder(string storeOrderID)
        //{
        //    InventoryDocDAL dao = new InventoryDocDAL();
        //    return dao.OutOrder(storeOrderID);
        //}
        #endregion

        #region 获取Country
        /// <summary>
        /// 获取Country
        /// 
        public int getCountry(string storeId)
        {
            InventoryDocDAL dao = new InventoryDocDAL();
            return dao.getCountry(storeId);
        }
        #endregion

        #region 取得产品列表
        ///<summary>
        /// 取得产品列表
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public DataTable GetStoreStorage(string storeID)
        {

           InventoryDocDAL dao = new InventoryDocDAL();
           return dao.GetStoreStorage(storeID);
        }
        #endregion

        #region 获得单据编号
        /// <summary>
        /// 根据单据类型的不同来获取不同的单据ID
        /// </summary>
        /// <param name="orderType">单据类型</param>
        /// <returns></returns>
        public static string GetDocId(EnumOrderFormType orderType)
        {
           return InventoryDocDAL.GetDocId(orderType);
        }
        #endregion

        #region 添加退货单和单据明细信息
        /// <summary>
        /// 添加退货单和单据明细信息
        /// </summary>
        /// <param name="nventoryDocModel">退货单</param>
        /// <param name="number">管理员ID</param>
        /// <param name="storeId">店铺ID</param>
        /// <param name="inventoryDocDetailsList">退货单明细集合</param>
        public static void InsertInventoryDoc(InventoryDocModel nventoryDocModel, string number, string storeId, ArrayList inventoryDocDetailsList)
        {
            InventoryDocDAL.InsertInventoryDoc(nventoryDocModel, number, storeId, inventoryDocDetailsList);
        }

        #endregion

        #region 根据退货单号统计已审核的退货单
        /// <summary>
        /// 根据退货单号统计已审核的退货单
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public static int GetStaInventoryDocByDocId(string docId)
        {
            return InventoryDocDAL.GetStaInventoryDocByDocId(docId);
        }
        #endregion

        #region 更新退货款状态为已审核
        /// <summary>
        /// 更新退货款状态为已审核
        /// </summary>
        /// <param name="warehouseId">仓库ID</param>
        /// <param name="number">管理员账号</param>
        /// <param name="docId">退货单号</param>
        /// <param name="storeId">退货店铺ID</param>
        public static void UpdateStaInventoryDocOfStateFlag(string depotSeatId,string warehouseId, string number, string docId, string storeId)
        {		
              //InventoryDocDAL.UpdateStaInventoryDocOfStateFlag(depotSeatId,warehouseId, number, docId, storeId);
        }
        #endregion

        

        #region 更新订单的状态为无效的
        /// <summary>
        /// 更新订单的状态为无效的
        /// </summary>
        /// <param name="docID"></param>
        public static void UpdateStateFlagAndCloseFlag(string docID)
        {
           InventoryDocDAL.UpdateStateFlagAndCloseFlag(docID);
        }
        #endregion

        #region 根据单据号查询单据日志
        /// <summary>
        /// 根据单据号查询单据日志
        /// </summary>
        /// <param name="docId">单据号</param>
        /// <returns></returns>
        public static string GetNoteByDocId(string docId)
        {
           return InventoryDocDAL.GetNoteByDocId(docId);
        }
        #endregion

        #region 审核未审核的入库单
        /// <summary>
        /// 审核未审核的入库单
        /// </summary>
        /// <returns></returns>
        public static void checkDoc(string DocAuditer, DateTime DcAuditTime, string OperateIP, string OperateNum, string DocID, string TempWareHouseID,int changwei)
        {
             InventoryDocDAL.checkDoc(DocAuditer, DcAuditTime, OperateIP, OperateNum, DocID, TempWareHouseID,changwei);
        }
        #endregion

        #region 将未审核的单据设为无效
        /// <summary>
        /// 将未审核的单据设为无效
        /// </summary>
        /// <returns></returns>
        public static void updDocTypeName(DateTime CloseDate, string DocID, string OperateIP, string OperateNum)
        { 
            InventoryDocDAL.updDocTypeName(CloseDate, DocID, OperateIP, OperateNum);

         }
        #endregion

        #region 删除未审核的单据
        /// <summary>
        /// 删除未审核的单据
        /// </summary>
        /// <param name="DocID"></param>
        public static void delDoc(string DocID)
        {
           InventoryDocDAL.delDoc(DocID);
        }
        #endregion

        #region 得到仓库名称
        /// <summary>
        /// 得到仓库名称
        /// </summary>
        /// <param name="WareHouseId"></param>
        /// <returns></returns>
        public string GetWarehouseName(string WareHouseId)
        {
             return InventoryDocDAL.GetWarehouseName(WareHouseId);
        }
        #endregion
        #region 得到货币的名称
        /// <summary>
        /// 得到货币的名称
        /// </summary>
        /// <param name="countryid"></param>
        /// <returns></returns>
        public string GetCurrencyName(string countryid)
        {
           return InventoryDocDAL.GetCurrencyName(countryid);
        }
        #endregion
        #region 得到币种
        /// <summary>
        /// 得到货币的名称
        /// </summary>
        /// <param name="countryid"></param>
        /// <returns></returns>
        public string GetCurrencyNameByPID(string productid)
        {
            InventoryDocDAL dao = new InventoryDocDAL();
            return "";//dao.GetCurrencyNameByPID(productid);
        }
        #endregion 
          #region  
        /// <summary>
        /// 得到可用的单编号，
        /// </summary>			
        public static string GetNewOrderID()
        {
            return InventoryDocDAL.GetNewOrderID();
        }
#endregion


         #region  得到可用的换货单编号，前缀为"HH"
        /// <summary>
        /// 得到可用的换货单编号，返回换货单编号
        /// </summary>	
        public static string GetReplacementID()
        {
            return InventoryDocDAL.GetReplacementID();
        }
#endregion
    }
}
