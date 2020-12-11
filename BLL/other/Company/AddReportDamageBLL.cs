using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Collections;
using System.Data;

namespace BLL.other.Company
{
    public class AddReportDamageBLL
    { 
        #region 库存报损
        /// <summary>
        /// 库存报损
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool addReportDemage(string DocAuditer, string OperateIP, string OperateNum, string DocID)
        {
            InventoryDocDAL invent = new InventoryDocDAL();
            return invent.addReportDemage(DocAuditer, OperateIP, OperateNum, DocID);
        }
        #endregion

        /// <summary>
       /// 修改金额
       /// </summary>
       /// <param name="sun"></param>
       /// <param name="productid"></param>
       /// <param name="DepotSeatID"></param>
       /// <returns></returns>
        public static string ProductReportDamage(double sun, int productid, int warehouseid, int DepotSeatID, string mode, InventoryDocDetailsModel opda_docDetail)
        {
             InventoryDocDAL invent = new InventoryDocDAL();
             return invent.ProductReportDamage(sun, productid, warehouseid, DepotSeatID, mode, opda_docDetail);
        }

        /// <summary>
        /// 库存报溢 
        /// </summary>
        /// <param name="sun"></param>
        /// <param name="productid"></param>
        /// <param name="warehouseid"></param>
        /// <param name="DepotSeatID"></param>
        /// <param name="mode"></param>
        /// <param name="opda_docDetail"></param>
        /// <returns></returns>
        public static string ProductReportDamage_II(ArrayList inventoryDocDetailsModels, int warehouseid, int DepotSeatID, string mode, InventoryDocModel inventoryDocModel)
        {
            return InventoryDocDAL.ProductReportDamage_II(inventoryDocDetailsModels, warehouseid, DepotSeatID, mode, inventoryDocModel);
        }
        /// <summary>
        /// 库存报损
        /// </summary>
        /// <param name="sun"></param>
        /// <param name="productid"></param>
        /// <param name="warehouseid"></param>
        /// <param name="DepotSeatID"></param>
        /// <param name="mode"></param>
        /// <param name="opda_docDetail"></param>
        /// <returns></returns>
        public static string ProductReportEDamage_II(ArrayList inventoryDocDetailsModels, int warehouseid, int DepotSeatID, string mode, InventoryDocModel inventoryDocModel)
        {
            return InventoryDocDAL.ProductReportEDamage_II(inventoryDocDetailsModels, warehouseid, DepotSeatID, mode, inventoryDocModel);
        }

        /// <summary>
        /// 根据条件获取报损列表    ---DS2012
        /// </summary>
        /// <param name="SQLWhere"></param>
        /// <returns></returns>
        public static DataTable GetReportList(string SQLWhere)
        {
            return InventoryDocDAL.GetReportList(SQLWhere);
        }
    }
}
