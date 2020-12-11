using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data;
using System.Data.SqlClient;
using Model.Other;
using BLL.CommonClass;

namespace BLL.Logistics
{
    /// <summary>
    /// 订单编辑菜单（店铺子系统）
    /// </summary>
    public class OrdersBrowseBLL
    {

        /// <summary>
        /// 根据查询条件得出数据结果集
        /// </summary>
        /// <param name="pagin">分页类(界面层需实例化)</param>
        /// <param name="ID">店铺ID标示</param>
        /// <param name="condition">条件</param>
        /// <returns>对应条件的订单数据</returns>
        public IList<StoreOrderModel> GetStoreOrderList(PaginationModel pagin, string key, string condition)
        {
            StoreOrderDAL server = new StoreOrderDAL();
            string tableName = "StoreOrder";
            string comlums = "ID,StoreID,TotalMoney,TotalPV,ExpectNum,InceptAddress,InceptPerson,PostalCode,Telephone,OrderDateTime,Country,Province,City";
            return server.GetStoreOrderListEffectBrowse(pagin, tableName, key, comlums, condition);
        }

        /// <summary>
        /// 根据单号获取点编号
        /// </summary>
        /// <param name="orderid">订单号</param>
        /// <returns>点编号</returns>
        public static string GetStoreIdByOrderId(string orderid)
        {
            return StoreOrderDAL.GetStoreByOrderId(orderid);
        }

        /// <summary>
        /// 查看订单表对应的详细信息  *
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<OrderDetailModel> GetOrderDetailListByStoreOrderId(int id)
        {
            OrderDetailDAL server = new OrderDetailDAL();
            return null;
        }

        /// <summary>
        /// 显示订单信息用于修改功能
        /// </summary>
        /// <param name="storeOrderId">ID</param>
        /// <returns></returns>
        public StoreOrderModel GetStoreOrderModelEffectUpdUpdStoreOrderItem(string id)
        {
            //StoreOrderModel storeOrder = new StoreOrderModel(id);

            //return storeOrder;

            return StoreOrderDAL.GetStoreOrderModel(id); 
        }


        /// <summary>
        /// 修改订货信息表数据
        /// </summary>
        /// <returns></returns>
        public Boolean UpdStoreOrderItem(StoreOrderModel updItem)
        {

            return true;
        }

        /// <summary>
        /// 验证订单是否存在
        /// </summary>
        /// <param name="storeOrderID">订单号</param>
        /// <returns></returns>
        public static bool GetOrderIsExist(string storeOrderID)
        {
            return StoreOrderDAL.GetOrderIsExist(storeOrderID);
        }

        /// <summary>
        /// 验证订单是否已经支付
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public static bool GetOrderCheckState(string storeOrderID)
        {
            return StoreOrderDAL.GetOrderCheckState(storeOrderID);
        }

        /// <summary>
        /// 删除订单方法
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public static Boolean DelStoreOrderItem(string storeOrderID)
        {
            Boolean temp = true;
            string connString = DBHelper.connString;
            SqlTransaction tr = null;
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            tr = conn.BeginTransaction();
            try
            {
                BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("OrderGoods", "ordergoodsID");
                cl_h_info.AddRecordtran(tr,storeOrderID);
                BLL.CommonClass.ChangeLogs cl_h_info2 = new BLL.CommonClass.ChangeLogs("OrderGoodsDetail", "ordergoodsID");
                cl_h_info2.AddRecordtran(tr, storeOrderID);

                StoreOrderDAL.DelStoreOrderItemProc(tr, storeOrderID);
                //StockDAL.DelStoreOrder(tr, OrderDetailDAL.GetOrderGoodsDetail(storeOrderID), StoreOrderDAL.GetStoreIdByGoodsId(storeOrderID));//还原店库存
                //OrderDetailDAL.DelOrderGoodsDetail(tr, storeOrderID);//明细表删除失败回滚
                //StoreOrderDAL.DelOrderGoods(storeOrderID, tr); //订单表删除失败回滚

                cl_h_info.AddRecordtran(tr, storeOrderID);
                cl_h_info.DeletedIntoLogstran(tr, BLL.CommonClass.ChangeCategory.store10, storeOrderID, BLL.CommonClass.ENUM_USERTYPE.objecttype2);
                cl_h_info2.AddRecordtran(tr, storeOrderID);
                cl_h_info2.DeletedIntoLogstran(tr, BLL.CommonClass.ChangeCategory.store10, storeOrderID, BLL.CommonClass.ENUM_USERTYPE.objecttype2);

                tr.Commit();
            }
            catch
            {
                temp = false;
                tr.Rollback();
            }
            finally
            {
                // tr.Connection.Close();
                tr.Dispose();
                conn.Close();
            }
            return temp;

        }

        /// <summary>
        /// StoreOrder DataTable 订单编辑 DataTable
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable StoreOrderDataTable(string condition)
        {
            return new StoreOrderDAL().StoreOrderDataTable(condition);
        }


        public static DataTable StoreOrderDT(string storeOrderID)
        {
            return StoreOrderDAL.StoreOrderDT(storeOrderID);
        }

        public static DataTable StoreOrderGoodsDT(string storeOrderID)
        {
            return StoreOrderDAL.StoreOrderGoodsDT(storeOrderID);
        }

        /// <summary>
        /// StoreOrder DataTable 订单编辑 - 详细信息
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public DataTable StoreOrderDataTable_I(string storeOrderID)
        {
            return new StoreOrderDAL().StoreOrderDataTable_I(storeOrderID);
        }

        /// <summary>
        /// StoreOrder DataTable 用于发货跟踪
        /// </summary>
        /// <returns></returns>
        public static DataTable StoreOrderDataTable_II(string condition)
        {
            return StoreOrderDAL.StoreOrderDataTable_II(condition);
        }

        /// <summary>
        /// StoreOrder DataTable 用于查看发货跟踪
        /// </summary>
        /// <returns></returns>
        public DataTable StoreOrderDataTable_III(string storeOrderID)
        {
            return new StoreOrderDAL().StoreOrderDataTable_III(storeOrderID);
        }

        /// <summary>
        /// StoreOrder DataTable 用于查看发货跟踪
        /// </summary>
        /// <returns></returns>
        public DataTable OrderGoodsDataTable_III(string storeOrderID)
        {
            return new StoreOrderDAL().OrderGoodsDataTable_III(storeOrderID);
        }

        /// <summary>
        /// StoreOrder DataTable 用于订单编辑
        /// </summary>
        /// <returns></returns>
        public static DataTable StoreOrderDataTable_IV(string condition)
        {
            return StoreOrderDAL.StoreOrderDataTable_IV(condition);
        }

        /// <summary>
        /// 订单编辑-备注查看
        /// </summary>
        /// <param name="StoreOrderID"></param>
        /// <returns></returns>
        public static string GetDescription(string storeOrderID)
        {
            return StoreOrderDAL.GetDescription(storeOrderID);
        }

        /// <summary>
        /// 库存单据-备注查看
        /// </summary>
        /// <param name="StoreOrderID"></param>
        /// <returns></returns>
        public static string GetNote(string docID)
        {
            return StoreOrderDAL.GetNote(docID);
        }

        /// <summary>
        /// 订单明细查询
        /// </summary>
        /// <param name="orderId"> 订单号 </param>
        /// <param name="storeId"> 店编号 </param>
        /// <returns></returns>
        public static IList<OrderDetailModel> GetDetails(string orderId, string storeId)
        {
            return OrderDetailDAL.GetDetails(orderId, storeId);
        }

        /// <summary>
        /// 订单明细查询
        /// </summary>
        /// <param name="orderId"> 订单号 </param>
        /// <param name="storeId"> 店编号 </param>
        /// <returns></returns>
        public static IList<OrderDetailModel> GetDetail(string orderId, string storeId)
        {
            return OrderDetailDAL.GetDetail(orderId, storeId);
        }

        /// <summary>
        /// 删除发货单方法
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public static Boolean DelGoodsOrder(string storeOrderID)
        {
            Boolean temp = true;
            string connString = DBHelper.connString;
            SqlTransaction tr = null;
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            tr = conn.BeginTransaction();
            try
            {
                BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("StoreOrder", "storeOrderID");
                cl_h_info.AddRecordtran(tr, storeOrderID);
                cl_h_info.DeletedIntoLogstran(tr, BLL.CommonClass.ChangeCategory.Order, storeOrderID, BLL.CommonClass.ENUM_USERTYPE.Company);


                StockDAL.DelGoodsOrder(tr, OrderDetailDAL.GetOrderDetail(storeOrderID), StoreOrderDAL.GetStoreIdByOrderId(storeOrderID));//还原店库存
                OrderDetailDAL.DelOrderDetailItem(tr, storeOrderID);//明细表删除失败回滚
                StoreOrderDAL.DelStoreOrder(storeOrderID, tr); //订单表删除失败回滚

                tr.Commit();
            }
            catch
            {
                temp = false;
                tr.Rollback();
            }
            finally
            {
                // tr.Connection.Close();
                tr.Dispose();
                conn.Close();
            }
            return temp;

        }
    }
}
