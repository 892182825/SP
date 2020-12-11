using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BLL.Logistics
{
    /// <summary>
    /// 收货确认菜单（店铺子系统）
    /// </summary>
    public class AffirmConsignBLL
    {
        /// <summary>
        /// 显示公司发货且未确认收货信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetStoreOrderList(string storeId) 
        {
            return new StoreOrderDAL().GetReceivedOrder(storeId);
        }
        public static DataTable GetBranchOrderList(string storeId)
        {
            return new StoreOrderDAL().GetReceivedOrder1(storeId);
        }

        /// <summary>
        /// 收货确认提交
        /// </summary>
        /// <param name="orderids">订单号</param>
        /// <param name="storeid">店铺编号</param>
        /// <returns>提交是否成功</returns>
        public bool Submit(string orderids, string storeid)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tr = conn.BeginTransaction();//开启事务

                try
                {
                    string[] orders = orderids.Split(new char[1]{','});
                    for (int i = 0; i < orders.Length-1; i++)
                    {
                        string ordertype = new StoreOrderDAL().GetOrderType(orders[i]);
                        DBHelper.ExecuteNonQuery(tr, "update orderdetail set receivedquantity=receivedquantity+i.productquantity from InventoryDocDetails i join InventoryDoc ii on i.docid=ii.docid where i.docid='" + orders[i] + "' and i.productid=orderdetail.productid and orderdetail.storeorderid=ii.storeorderid  ");
                        DBHelper.ExecuteNonQuery(tr, "update storeorder set IsReceived='Y' from  InventoryDoc i where i.docid='" + orders[i] + "' and  i.storeorderid=storeorder.storeorderid  ");
                        if (new StoreOrderDAL().UpdateIsReceived(tr, orders[i]))
                        {
                            if (ordertype == "0" || ordertype == "2")
                            {
                                if (!new StockDAL().UpdateTotalIn(tr, orders[i], storeid))
                                {
                                    tr.Rollback();
                                    return false;
                                }
                               
                            }
                            else
                            {
                                if (!new StockDAL().UpdateTurnStorage(tr, orders[i], storeid))
                                {
                                    tr.Rollback();
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            tr.Rollback();
                            return false;
                        }
                    }
                    tr.Commit();
                    return true;
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
        }

        /// <summary>
        /// 会员收货确认提交
        /// </summary>
        /// <param name="orderids">订单号</param>
        /// <returns>提交是否成功</returns>
        public bool Submit(string orderids)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tr = conn.BeginTransaction();//开启事务

                try
                {
                    string[] orders = orderids.Split(new char[1] { ',' });
                    for (int i = 0; i < orders.Length - 1; i++)
                    {
                        string[] arr = new StoreOrderDAL().GetOrderTypeAndStoreid(orders[i]);
                        DBHelper.ExecuteNonQuery(tr, "update orderdetail set receivedquantity=receivedquantity+i.productquantity from InventoryDocDetails i join InventoryDoc ii on i.docid=ii.docid where i.docid='" + orders[i] + "' and i.productid=orderdetail.productid and orderdetail.storeorderid=ii.storeorderid  ");
                        DBHelper.ExecuteNonQuery(tr, "update storeorder set IsReceived='Y' from  InventoryDoc i where i.docid='" + orders[i] + "' and  i.storeorderid=storeorder.storeorderid  ");

                        if (!new StoreOrderDAL().UpdateIsReceived(tr, orders[i]))
                        {
                            tr.Rollback();
                            return false;
                        }
                    }
                    tr.Commit();
                    return true;
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
        }

        public bool Submit1(string orderids)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tr = conn.BeginTransaction();//开启事务

                try
                {

                    //string[] arr = new StoreOrderDAL().GetOrderTypeAndStoreid(orderids);
                    //DBHelper.ExecuteNonQuery(tr, "update orderdetail set receivedquantity=receivedquantity+i.productquantity from InventoryDocDetails i join InventoryDoc ii on i.docid=ii.docid where i.docid='" + orderids + "' and i.productid=orderdetail.productid and orderdetail.storeorderid=ii.storeorderid  ");
                    //DBHelper.ExecuteNonQuery(tr, "update storeorder set IsReceived='Y' from  InventoryDoc i where i.docid='" + orderids + "' and  i.storeorderid=storeorder.storeorderid  ");

                    //DBHelper.ExecuteNonQuery(tr, "update orderdetail set receivedquantity=receivedquantity+i.productquantity where StoreOrderID='" + orderids + "'   ");
                     DBHelper.ExecuteNonQuery(tr, "update storeorder set IsReceived='Y'  where StoreOrderID='" + orderids + "'  ");

                    if (!new StoreOrderDAL().UpdateIsReceived1(tr, orderids))
                    {
                        tr.Rollback();
                        return false;
                    }

                    tr.Commit();
                    return true;
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
        }

        /// <summary>
        /// 显示公司发货且未确认收货信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMemberOrderList(string number, int sendType)
        {
            return new StoreOrderDAL().GetNumberReceivedOrder(number, sendType);
        }


        /// <summary>
        /// 收货确认提交
        /// </summary>
        /// <param name="orderids">订单号</param>
        /// <param name="storeid">会员编号</param>
        /// <returns>提交是否成功</returns>
        public bool NSubmit(string orderids, string number)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tr = conn.BeginTransaction();//开启事务                
                try
                {
                    string[] orders = orderids.Split(new char[1] { ',' });
                    for (int i = 0; i < orders.Length - 1; i++)
                    {
                        string storeid = new StoreOrderDAL().getStoreIdByOrderId(orders[i]);
                        string ordertype = new StoreOrderDAL().GetOrderType(orders[i]);
                        if (new StoreOrderDAL().UpdateIsReceived(tr, orders[i]))
                        {
                            if (ordertype == "0")
                            {
                                if (!new StockDAL().UpdateTotalIn(tr, orders[i], storeid))
                                {
                                    tr.Rollback();
                                    return false;
                                }
                            }
                            //else
                            //{
                            //    if (!new StockDAL().UpdateTurnStorage(tr, orders[i], storeid))
                            //    {
                            //        tr.Rollback();
                            //        return false;
                            //    }
                            //}
                        }
                        else
                        {
                            tr.Rollback();
                            return false;
                        }
                    }
                    tr.Commit();
                    return true;
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
        }

        /// <summary>
        /// 更新反馈问题
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="question">反馈内容</param>
        /// <returns>是否更新成功</returns>
        public static bool AddQuestion(string orderId, string question)
        {
            return new StoreOrderDAL().UpdateFeedBack(orderId, question);
        }
    }
}
