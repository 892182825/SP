using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Other;
using DAL;

namespace BLL.Logistics
{
    /// <summary>
    /// 订单跟踪菜单（店铺子系统）
    /// </summary>
    public class QueryOrdersBLL
    {
        /// <summary>
        /// 搜索对应店铺的订单查看其状态
        /// </summary>
        /// <param name="pagin">分页类</param>
        /// <param name="condition">查询条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="key">键值</param>
        /// <param name="comlums">查询列名</param>
        /// <returns></returns>
        public static IList<StoreOrderModel> GetStoreOrderList(PaginationModel pagin, string condition,string tableName,string key,string comlums)
        {
           // StoreOrderDAL server = new StoreOrderDAL();
            return StoreOrderDAL.GetStoreOrderListEffectQueryOrder(pagin, tableName, key, comlums, condition);
        }

       /// <summary>
        /// 查看对应订单的明细
       /// </summary>
       /// <param name="ID">店铺编号</param>
       /// <returns>返回对应明细</returns>
        public static IList<OrderDetailModel> GetOrderDetailList(string ID)
        {
            //OrderDetailDAL server = new OrderDetailDAL();
            return OrderDetailDAL.GetOrderDetailByStoreId(ID);
        }

        /// <summary>
        /// 获取店铺使用货币
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static string GetStoreCurrency(string storeid)
        {
            return StoreInfoDAL.GetStoreCurrency(storeid).ToString();
        }
        
    }
}
