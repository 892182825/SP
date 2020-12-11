using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using DAL;
using System.Data;
using Model;

namespace BLL.other.Company
{
    /// <summary>
    /// 报表中心
    /// </summary>
    public class ReportFormsBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rbtn_typeSelectedIndex"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="ddl_itemSelectedValue"></param>
        /// <returns></returns>
        public static ArrayList ConstructData(int rbtn_typeSelectedIndex, string begin, string end, string ddl_itemSelectedValue)
        {
            return ReportFormsDAL.ConstructData(rbtn_typeSelectedIndex, begin, end, ddl_itemSelectedValue);
        }
        public static DataTable ProductStockDetail_Company_Store(string productID, string flag)
        {
            return ReportFormsDAL.ProductStockDetail_Company_Store(productID,flag);
        }
        /// <summary>
        /// 订单汇总--店铺、省市
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetOrderCollect(string beginTime, string endTime,string mode)
        {
            return ReportFormsDAL.GetOrderCollect(beginTime,  endTime, mode);
        }

        /// <summary>
        /// 订单汇总--产品
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetOrderCollect_II(string beginTime, string endTime)
        {
            return ReportFormsDAL.GetOrderCollect_II(beginTime, endTime);
        }
        //根据产品编码得到产品
        /// <summary>
        /// 查询产品
        /// </summary>
        /// <param name="productcode"></param>
        /// <returns></returns>
        public static ProductModel GetProductItem(string productcode)
        {
            return ReportFormsDAL.GetProductItem(productcode);
        }
        /// <summary>
        /// 订单明细
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetOrderMingXi(string storeid,string beginTime,string endTime)
        {
            return ReportFormsDAL.GetOrderMingXi(storeid, beginTime, endTime);
        }

        /// <summary>
        /// 根据产品id获取产品名字
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static string GetProductName(string productID)
        {
            return ReportFormsDAL.GetProductName(productID);
        }

        /// <summary>
        /// 根据仓库id获取仓库名字
        /// </summary>
        /// <param name="wareHouseNameID"></param>
        /// <returns></returns>
        public static string GetWareHouseName(string wareHouseNameID)
        {
            return ReportFormsDAL.GetWareHouseName(wareHouseNameID);
        }

        /// <summary>
        /// 获取库位名字
        /// </summary>
        /// <param name="depotSeatID"></param>
        /// <returns></returns>
        public static string GetDepotSeatName(string wareHouseID,string depotSeatID)
        {
            return ReportFormsDAL.GetDepotSeatName(wareHouseID, depotSeatID);
        }

        /// <summary>
        /// 产品销售明细
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSellMingXi(string beginTime, string endTime, string productID, string wareHouseID, string depotSeatID)
        {
            return ReportFormsDAL.GetSellMingXi( beginTime,  endTime,  productID,  wareHouseID,  depotSeatID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rbtn_typeSelectedIndex"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="ddl_itemSelectedValue"></param>
        /// <returns></returns>
        public static ArrayList ConstructData_II(string condition)
        {
            return ReportFormsDAL.ConstructData_II(condition);
        }

        /// <summary>
        /// 按服务机构汇总
        /// </summary>
        /// <returns></returns>
        public static DataTable GetStoreCollect(string condition)
        {
            return ReportFormsDAL.GetStoreCollect(condition);
        }

        /// <summary>
        /// 仓库汇总
        /// </summary>
        /// <returns></returns>
        public static DataTable GetWareHouseCollect(string condition)
        {
            return ReportFormsDAL.GetWareHouseCollect(condition);
        }
    }
}
