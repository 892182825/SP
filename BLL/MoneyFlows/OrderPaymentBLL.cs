using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model.Other;
using DAL;
using Model;

/**
 * 创建者;孙延昊
 * 创建时间：2009-8-27
 * 文件名：OrderPaymentBLL
 * 功能：支付店铺订单类
 * **/
namespace BLL.MoneyFlows
{
    /// <summary>
    /// 支付店铺订单类
    /// </summary>
    public class OrderPaymentBLL
    {
        /// <summary>
        /// 店铺数据访问
        /// </summary>
        StoreInfoDAL storeInfoServer = new StoreInfoDAL();
        StoreOrderDAL storeOrderServer = new StoreOrderDAL();
        /// <summary>
        /// 验证店铺是否存在
        /// </summary>
        /// <param name="storeID">店铺编号</param>
        /// <returns>true 存在指定编号的店铺，false 不存在指定编号的店铺</returns>
        public int CheckStoreID(string storeID)
        {
            return StoreInfoDAL.getStoreInfoID(storeID);
        }

        /// <summary>
        /// 根据店铺标识获取店铺信息
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <returns>店铺剩余订货款,店铺剩余周转订货款</return>
        public DataRow GetLeftMoney(string storeId)
        {
            //获取店铺订货款，周转货款
            DataTable dt = storeOrderServer.GetLeftMoney(storeId);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return dt.Rows[0];
            }
        }

        /// <summary>
        /// 根据店店铺编号查询店铺所有未支付订单信息
        /// </summary>
        /// <param name="p">店铺编号</param>
        /// <returns>店铺订货单泛型集合</returns>
        public IList<StoreOrderModel> GetStoreOrdersPagin(string p)
        {
            return storeOrderServer.GetStoreOrdersPagin(p);
        }

        /// <summary>
        /// 订单支付方法
        /// </summary>
        /// <param name="orderids">订单标识字符串</param>
        /// <returns>-1 标识发生sql异常 -2 标识当前订单有已经支付的订单 </returns>
        public string PayOrders(string orderids,string storeID,string operateIP,string operateNum )
        {
            return storeOrderServer.PayOrderment(orderids, storeID, operateIP, operateNum);
        }

        /// <summary>
        /// 判断当前选中的所有订单是否已经支付
        /// </summary>
        /// <param name="orderids"></param>
        /// <returns></returns>
        public bool CheckOrderPay(string orderids)
        {
            return storeOrderServer.CheckOrderPay(orderids);
        }

        /// <summary>
        /// 验证库存是否足够
        /// </summary>
        /// <param name="orderids">订单号</param>
        /// <returns>返回结果字符串 足够则返回null</returns>
        public string CheckQuentity(string orderids)
        {
            return null;
        }
    }

}
