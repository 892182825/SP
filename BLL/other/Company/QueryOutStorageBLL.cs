using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using Model;

namespace BLL.other.Company
{
    /// <summary>
    /// 出库查询
    /// </summary>
    public class QueryOutStorageBLL
    {


        public static DataTable outStoreOrder(DateTime fromTime, DateTime toTime, int moneyType, int wareHouseID, int DepotSeatID)
        {
            return InventoryDocDAL.outStoreOrder(fromTime, toTime, moneyType, wareHouseID, DepotSeatID);
        }
    }
}
