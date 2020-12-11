using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

using System.Data;
using DAL;

namespace BLL.Logistics
{
    /// <summary>
    /// 退货处理（公司子系统）
    /// </summary>
    public class RefundmentOrderBrowseBLL
    {
        //查询数据
        //获取店铺信息
        //添加退货单

        //换货时用于获取库存等信息
        public DataTable GetStoreStorage(string storeID)
        {
            return new InventoryDocDAL().GetStoreStorage(storeID);
        }
    }
}
