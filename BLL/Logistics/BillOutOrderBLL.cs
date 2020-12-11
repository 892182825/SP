using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Other;
using DAL;
using System.Data;
using System.Web.UI.WebControls;

namespace BLL.Logistics
{
    /// <summary>
    /// 订单出库菜单(公司子系统)
    /// </summary>
    public class BillOutOrderBLL
    {
        StoreOrderDAL storeOrderServer = new StoreOrderDAL();
        /// <summary>
        /// 返回可以处理的出库单(参数未实现)
        /// </summary>
        /// <returns></returns>
        public IList<StoreOrderModel> GetStoreOrderList(PaginationModel pagin, string condition)
        {
            string table = "StoreOrder";
            string key = "ID";
            string comlums = " ID,StoreID,StoreOrderID,ExpectNum,TotalPv,TotalMoney,IsCheckOut,PayMentDateTime,InceptPerson"
                +",InceptAddress,Telephone,Weight,Carriage,OrderDateTime ";
            return storeOrderServer.GetStoreOrderListEffectBillOutOrder(pagin,table,key,comlums,condition);
        }

        /// <summary>
        /// 确认出库按钮。并生成出库单(BillOutOrder.aspx)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Boolean OutOrder(string storeOrderID, string isGeneOutBill)
        {
            return new StoreOrderDAL().OutOrder(storeOrderID, isGeneOutBill);
        }

        /// <summary>
        /// 获取出库名(Outstock.aspx)
        /// </summary>
        /// <param name="WareHouseID"></param>
        /// <returns></returns>
        public static ListItem GetWareHouseName(string storeOrderID)
        {
            return WareHouseDAL.GetWareHouseName(storeOrderID);

        }

        public static DataTable GetWareHouseName()
        {
            return WareHouseDAL.GetWareHouseName();
        }

        public static DataTable GetWareHouseName_Currency(string currency)
        {
            return WareHouseDAL.GetWareHouseName_Currency(currency);
        }


        /// <summary>
        /// 确认出库按钮。并生成出库单(Outstock.aspx)
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public static string OutOrder(string storeOrderID, string outStorageOrderID,InventoryDocModel idm,InventoryDocDetailsModel ddm)
        {
            return InventoryDocDAL.OutOrder(storeOrderID,outStorageOrderID,idm,ddm);
        }

        public static string OutOrder(string storeOrderID, string outStorageOrderID,InventoryDocModel idm, List<InventoryDocDetailsModel> l_ddm)
        {
            return InventoryDocDAL.OutOrder(storeOrderID, outStorageOrderID,idm, l_ddm);
        }

        /// <summary>
        /// 获取出库产品(Outstock)
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public static DataTable GetOutProduct(string storeOrderID)
        {
            return InventoryDocDAL.GetOutProduct(storeOrderID);
        }

        /// <summary>
        /// 获取产品原来订的数量(Outstock)
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public static DataTable GetProductQuantity(string storeOrderID)
        {
            return InventoryDocDAL.GetProductQuantity(storeOrderID);
        }

        /// <summary>
        /// 订单出库 DataTable
        /// </summary>
        /// <returns></returns>
        public static DataTable GetStoreOrderDataTable(string condition)
        {
            return  StoreOrderDAL.StoreOrderDataTable_VI(condition);
        }

        public static StoreOrderModel GetStoreOrderModel(string storeOrderID)
        {
            return StoreOrderDAL.GetStoreOrderModel(storeOrderID);
        }

        public static StoreOrderModel GetStoreOrderModel_II(string storeOrderID)
        {
            return StoreOrderDAL.GetStoreOrderModel_II(storeOrderID);
        }

        /// <summary>
        /// 获取 OrderDetailModel 模型
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public static OrderDetailModel GetOrderDetailModel(string storeOrderID)
        {
            return new OrderDetailDAL().GetOrderDetailModel(storeOrderID);
        }

        public static List<OrderDetailModel> GetOrderDetailModelList(string storeOrderID)
        {
            return new OrderDetailDAL().GetOrderDetailModelList(storeOrderID);
        }

        /// <summary>
        /// 出库单 撤单
        /// </summary>
        /// <param name="outStorageOrderID"></param>
        /// <returns></returns>
        public static bool SetQuashBillOutOrder(string outStorageOrderID, List<InventoryDocDetailsModel> l_ddm, string storeOrderID)
        {
            return InventoryDocDAL.SetQuashBillOutOrder(outStorageOrderID, l_ddm, storeOrderID);
        }

        /// <summary>
        /// 获取仓库xml字符串
        /// </summary>
        /// <returns></returns>
        public static string GetWareHouseXML()
        {
            return WareHouseDAL.GetWareHouseXML();
        }

        /// <summary>
        /// 获取对应库位xml字符串
        /// </summary>
        /// <returns></returns>
        public static string GetDepotSeatXML(string idvalue)
        {
            return DepotSeatDAL.GetDepotSeatXML(idvalue);
        }
    }
}

