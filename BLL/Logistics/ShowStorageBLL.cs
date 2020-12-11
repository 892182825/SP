using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Other;
using DAL;
using System.Data;

namespace BLL.Logistics
{
    /// <summary>
    /// 库存情况(店铺子系统）
    /// </summary>
    public class ShowStorageBLL
    {
        /// <summary>
        /// 返回对应店铺库存
        /// </summary>
        /// <returns></returns>
        public DataTable GetShowStorage(string storeID)
        {
            ThirdLogisticsDAL thirdLogisticsDAL = new ThirdLogisticsDAL();
            return thirdLogisticsDAL.GetShowStorage(storeID);
        }
        /// <summary>
        /// 返回对应店铺的币种——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static string GetStoreCurrency(string storeid)
        {
            return StoreInfoDAL.GetStoreCurrency(storeid).ToString();
        }

        /// <summary>
        /// 返回对应店铺的币种——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static string GetStoreECurrency()
        {
            return StoreInfoDAL.GetStoreECurrency();
        }
    }
}
