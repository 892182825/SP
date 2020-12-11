using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using DAL;

namespace BLL.other.Company
{
    /// <summary>
    /// 库存报溢
    /// </summary>
    public class AddReportProfit
    {
           #region 报溢
           public static bool ProductReportProfit(double sun, int productid, int warehouseid, int DepotSeatID)
        {
            InventoryDocDAL dao=new InventoryDocDAL ();
            return dao.ProductReportProfit(sun, productid, warehouseid, DepotSeatID);
        }
           #endregion
           /// <summary>
        /// 根据国家和库位获取产品信息
        /// </summary>
        /// <param name="Country"></param>
        /// <param name="DepotSeatID"></param>
        /// <returns></returns>
           public static DataTable GetProductQueryMenu(int Country, int WareHouseID, int DepotSeatID)
        {
            return ProductDAL.GetProductQueryMenu(Country, WareHouseID, DepotSeatID);
        }
        /// <summary>
        /// 得到仓库
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProductWareHouseInfo()
        {
            return WareHouseDAL.GetProductWareHouseInfo();
        }

        /// <summary>
        /// 根据国家和库位获取产品信息(不包含组合产品)
        /// </summary>
        /// <param name="Country"></param>
        /// <param name="DepotSeatID"></param>
        /// <returns></returns>
        public static DataTable GetProductEQueryMenu(int Country, int WareHouseID, int DepotSeatID)
        {
            return ProductDAL.GetProductEQueryMenu(Country, WareHouseID, DepotSeatID);
        }
    }
}
