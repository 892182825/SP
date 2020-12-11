using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace BLL.Logistics
{
    /// <summary>
    /// 订单编辑菜单（公司子系统）
    /// </summary>
    public class BrowseStoreOrdersBLL
    {
        /// <summary>
        /// 返回订单信息 (参数未写)
        /// </summary>
        /// <returns></returns>
        public IList<StoreOrderModel> GetStoreOrderList()
        {
            return null;
        }

        /// <summary>
        /// 根据标示返回订单详细记录
        /// </summary>
        /// <param name="id">标示</param>
        /// <returns></returns>
        public IList<OrderDetailModel> GetOrderDetailList(int id)
        {
            return null;
        }

        /// <summary>
        /// 根据订单表ID删除对应订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Boolean DelStoreOrderItem(int id)
        {
            return true;
        }

    }
}
