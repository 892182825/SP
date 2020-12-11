using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using System.Data;
using DAL;

/*
 * 创建者：  汪  华
 * 创建时间：2009-10-26
 * 完成时间：2009-10-26
 * 对应菜单：报表中心->仓库各产品明细 
 */

namespace BLL.other.Company
{
    public class WareHouseProductDetailsBLL
    {
        /// <summary>
        /// 通过仓库ID获取指定仓库产品信息
        /// </summary>
        /// <param name="wareHouseID">仓库ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMoreProductInfoByWareHouseID(int wareHouseID)
        {
            return ProductDAL.GetMoreProductInfoByWareHouseID(wareHouseID);


        }

        /// <summary>
        /// 通过仓库ID，库位ID获取指定的仓库库位产品信息
        /// </summary>
        /// <param name="wareHouseID">仓库ID</param>
        /// <param name="depotSeatID">库位ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMoreProductInfoByWareHouseIDDepotSeatID(int wareHouseID, int depotSeatID)
        {
            return ProductDAL.GetMoreProductInfoByWareHouseIDDepotSeatID(wareHouseID, depotSeatID);

    
        }
    }
}
