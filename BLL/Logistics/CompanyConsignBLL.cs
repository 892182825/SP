using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Other;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BLL.Logistics
{
    /// <summary>
    /// 订单发货菜单(公司子系统)
    /// </summary>
    public class CompanyConsignBLL
    {
        StoreOrderDAL storeServer = new StoreOrderDAL();
        /// <summary>
        /// 根据订单编号返回订单对象
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public StoreOrderModel GetStoreOrderByNumber(string number)
        {
            return null;
        }

        /// <summary>
        /// 返回普通单据
        /// </summary>
        /// <param name="pagin"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IList<StoreOrderModel> GetStoreOrderList(PaginationModel pagin,string condition)
        {
            string tableName = "StoreOrder as S inner join InventoryDoc as I on(I.ChaDocID=S.OutStorageOrderID)";
            string key = "S.ID";
            string comlums = "S.ID,S.StoreID,S.StoreOrderID,S.OrderDateTime,S.ExpectNum,S.PayMentDateTime,S.OrderType,S.TotalMoney,S.TotalPV,S.Weight"
                    + ",S.Carriage,S.ForeCastArriveDateTime,S.InceptPerson,S.InceptAddress,S.PostalCode,S.Telephone,S.ConveyanceMode"
                    + ",S.ConveyanceCompany,S.OutStorageOrderID";
            return storeServer.GetStoreOrderListEffectCompanyConsign(pagin, tableName, key, comlums, condition);
        }

        /// <summary>
        /// 订单发货
        /// </summary>
        /// <param name="storeOrderIDs"></param>
        /// <returns></returns>
        public static string Consign(List<StoreOrderModel> lsom)
        {
            bool isSucceed = true;
            string storeOrderID = "";

            for (int i = 0; i < lsom.Count; i++)
            {
                StoreOrderModel som = StoreOrderDAL.GetStoreOrderModel_II(lsom[i].StoreorderId);

                if (som.IsGeneOutBill == "Y")
                {
                    StoreOrderDAL.UpdStoreOrderIsSent(lsom[i]);
                }
                else
                {
                    isSucceed = false;

                    storeOrderID = lsom[i].StoreorderId;

                    break;
                }
            }

            if (isSucceed)
                return "Y";
            else
                return storeOrderID;
        }

        /// <summary>
        /// 订单发货 DataTable
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static DataTable GetStoreOrderDataTable(string condition)
        {
            return StoreOrderDAL.StoreOrderDataTable_V(condition);
        }

        /// <summary>
        /// 根据仓库id返会库位名称，返回的是一个xml格式的字符串
        /// </summary>
        /// <param name="WareHouseID"></param>
        /// <returns></returns>
        public static DataTable GetDepotSeat(string WareHouseID)
        {
            return DepotSeatDAL.GetDepotSeat(WareHouseID);
        }

        /// <summary>
        /// 合并单据
        /// </summary>
        /// <param name="lsom"></param>
        /// <returns></returns>
        public static string SetUniteForm(List<StoreOrderModel> lsom)
        {
            bool isSucceed = true;
            string err = "";
            string storeorderId="";
            string storeID = "";

            for (int i = 0; i < lsom.Count; i++)
            {
                StoreOrderModel som = StoreOrderDAL.GetStoreOrderModel(lsom[i].StoreorderId);

                if (som.IsGeneOutBill == "Y")
                {
                    if (storeID == "")
                        storeID = som.StoreId;
                    else
                    {
                        if (storeID != som.StoreId)
                        {
                            isSucceed = false;

                            err = "1";

                            break;
                        }
                    }
                }
                else
                {
                    isSucceed = false;

                    err = "2";
                    storeorderId=lsom[i].StoreorderId;

                    break;
                }
            }

            if (isSucceed)
            {
                return "Y";
            }
            else
            {
                if (err == "1")
                    return "不在同一个店铺下不能合单";
                //else if (err == "2")
                    return "订单号 ： " + storeorderId + ", 没有出库";
            }
        }

        /// <summary>
        /// 返回合单表的DataTable
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUniteDoc(string condition)
        {
            return UniteDocDAL.GetUniteDoc(condition);
        }

        /// <summary>
        /// 添加合单信息
        /// </summary>
        /// <returns></returns>
        public static bool AddUniteDoc(UniteDocModel unitedocm)
        {
            return UniteDocDAL.AddUniteDoc_I(unitedocm);
        }

        /// <summary>
        /// 根据 出库单号 获取一条信息
        /// 用于合单出库
        /// </summary>
        /// <param name="outStorageOrderID"></param>
        /// <returns></returns>
        public static StoreOrderModel GetStoreOrder(string outStorageOrderID)
        {
            return StoreOrderDAL.GetStoreOrder(outStorageOrderID);
        }

        /// <summary>
        /// 删除一条合单信息
        /// </summary>
        /// <param name="UniteDocID"></param>
        /// <returns></returns>
        public static bool DelUniteDoc(string UniteDocID)
        {
            return UniteDocDAL.DelUniteDoc(UniteDocID);
        }
    }
}
