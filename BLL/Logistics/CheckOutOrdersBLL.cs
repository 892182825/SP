using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data;
using System.Data.SqlClient;
using BLL.CommonClass;

namespace BLL.Logistics
{
    /// <summary>
    /// 订单支付菜单（店铺子系统）
    /// </summary>
    public class CheckOutOrdersBLL
    {
        ///// <summary>
        ///// 查询订单信息
        ///// </summary>
        ///// <param name="storeId"></param>
        ///// <returns></returns>
        //public IList<StoreOrderModel> GetStoreOrderList(int storeId)
        //{
        //    StoreOrderDAL server = new StoreOrderDAL();
        //    return server.GetStoreOrderLists(storeId.ToString());
        //}

        /// <summary>
        /// 验证 公司逻辑库存是否足够
        /// </summary>
        /// <param name="orders">订单列表</param>
        /// <returns></returns>
        public static bool CheckLogicProductInventory(IList<StoreOrderModel> orders)
        {
            return LogicProductInventoryDAL.SelectProductNum(orders);
        }

        /// <summary>
        ///  查看订单详细
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public static IList<OrderDetailModel> GetOrderDetailListByStoreId(string storeId)
        {
            return OrderDetailDAL.GetOrderDetailByStoreId(storeId);
        }

        /// <summary>
        /// 确认订单按钮
        /// </summary>
        /// <returns></returns>
        public Boolean CheckOutStoreOrder(IList<StoreOrderModel> orders, string storeid)
        {
            
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();//打开连接
                SqlTransaction tr = conn.BeginTransaction();//开启事务

                try
                {
                    decimal ordermoney = 0;
                    decimal turnmoney = 0;
                    string orderids = "";
                    foreach (StoreOrderModel order in orders)
                    {
                        if (!StoreOrderDAL.GetIsCheckOut(tr, order.StoreorderId))//判断该订单是否已经支付
                        {
                            if (StoreOrderDAL.UpdateOrderGoodsState(tr, order.StoreorderId))//更改订单成已支付状态
                            {
                                orderids += order.StoreorderId + ";";
                                if (order.OrderType == 0)
                                {
                                    ordermoney += order.TotalMoney;
                                }
                                else if (order.OrderType == 1)
                                {
                                    turnmoney += order.TotalMoney;
                                }
                                if (!StockDAL.UpdateInWayCount(tr, OrderDetailDAL.GetOrderGoodsDetail(order.StoreorderId), storeid))//跟新店库存，添加在途数量，去除预定数量
                                {
                                    tr.Rollback();
                                    return false;
                                }
                                if (!LogicProductInventoryDAL.UpdateToatlOut(tr, CommonDataBLL.GetNewOrderDetail(OrderDetailDAL.GetOrderGoodsDetail(order.StoreorderId))))//更新公司 逻辑库存
                                {
                                    tr.Rollback();
                                    return false;
                                }
                            }
                            else
                            {
                                tr.Rollback();
                                return false;
                            }
                        }
                    }

                    //添加对账单
                    D_AccountBLL.AddAccount(storeid, Convert.ToDouble(ordermoney), D_AccountSftype.StoreType, D_AccountKmtype.StoreOrderout, DirectionEnum.AccountReduced, "店铺【"+storeid+"】在线订货，订货款扣除额，订单号为【"+orderids+"】", tr);
                    //跟新店货款
                    if (!StoreInfoDAL.UpdateSomeMoney(tr, ordermoney, turnmoney, storeid))
                    {
                        tr.Rollback();
                        return false;
                    }

                    tr.Commit();
                }
                catch
                {
                    tr.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return true;
        }


        /// <summary>
        /// 查询店铺订单列表
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <returns>订单列表</returns>
        public static IList<StoreOrderModel> GetStoreOrderList(string storeId)
        {
            return new StoreOrderDAL().GetStoreOrderLists(storeId);
        }

        /// <summary>
        /// 查询店铺订单列表
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <returns>订单列表</returns>
        public static IList<OrderGoodsMedel> GetOrderGoodsList(string storeId)
        {
            return new StoreOrderDAL().GetOrderGoodsList(storeId);
        }

         /// <summary>
        /// 获取未支付订单（新）
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <param name="StoreOrderID">订单编号</param>
        /// <param name="payType">支付状态</param>
        /// <returns></returns>
        public static DataTable GetOrderGoodsList(string storeId, string StoreOrderID, string payType)
        {
            return StoreOrderDAL.GetOrderGoodsList(storeId, StoreOrderID, payType);
        }

        public static DataTable GetOrderGoodsListZ(string storeorderId)
        {
            return StoreOrderDAL.GetOrderGoodsListZ(storeorderId);
        }

        /// <summary>
        /// 查询货币，可订货款，周转货款
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <returns>返回货币，可订货款，周转货款</returns>
        public static string[] GetStoreTotalOrderGoodsMoney(string storeId)
        {
            return StoreInfoDAL.GetSomeMoney(storeId);
        }

        /// <summary>
        /// 获取订单明细
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>订单列表</returns>
        public static DataTable GetStoreDetail(string orderId)
        {
            return OrderDetailDAL.GetOrderDetailTwo(orderId);
        }

        public static DataTable GetStoreDetail1(string orderId,double Currency)
        {
            return OrderDetailDAL.GetOrderDetailTwo1(orderId, Currency);
        }

        /// <summary>
        /// 获取订单明细
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>订单列表</returns>
        public static DataTable GetOrderGoodsDetails(string orderId)
        {
            return OrderDetailDAL.GetOrderGoodsDetails(orderId);
        }


        public static DataTable GetOrderView(string StoreOrderID)
        {
            return OrderDetailDAL.GetOrderView(StoreOrderID);
        }
        public static DataTable GetOrderView1(string StoreOrderID, double Currency)
        {
            return OrderDetailDAL.GetOrderView1(StoreOrderID, Currency);
        }


        public static DataTable GetOrderViewT(string StoreOrderID)
        {
            return OrderDetailDAL.GetOrderViewT(StoreOrderID);
        }

        public static DataTable GetOrderViewTNew(string StoreOrderID)
        {
            return OrderDetailDAL.GetOrderViewTNew(StoreOrderID);
        }

        public static DataTable GetOrderGoodY(string StoreOrderID)
        {
            return OrderDetailDAL.GetOrderGoodY(StoreOrderID);
        }

        /// <summary>
        /// 根据新的要货编号获得老订单号
        /// </summary>
        /// <param name="fahuoOrder">要货单号</param>
        /// <returns></returns>
        public static DataTable GetOrderGoodsNoteTable(string fahuoOrder)
        {
            return OrderDetailDAL.GetOrderGoodsNoteTable(fahuoOrder);
        }

    }
}
