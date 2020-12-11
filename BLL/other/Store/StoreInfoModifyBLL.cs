using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data;
using Model.Other;

namespace BLL.other.Store
{
    /// <summary>
    /// 店铺资料修改
    /// </summary>
    public class StoreInfoModifyBLL
    {
        
        /// <summary>
        /// 店铺资料修改（店铺）
        /// </summary>
        /// <param name="storeInfoMember"></param>
        /// <returns></returns>
        public int updateStoreInfoStore(StoreInfoModel storeInfoMember)
        {
           // StoreInfoDAL sd = new StoreInfoDAL();
            return StoreInfoDAL.updStoreInfo(storeInfoMember);
        }
        /// <summary>
        /// 根据D获得店铺信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static StoreInfoModel GetStoreInfoById(int id)
        {
           // StoreInfoDAL sd = new StoreInfoDAL();
            //StoreInfoDAL dao = new StoreInfoDAL();
            //return dao.GetStoreInfoById(id);
            return StoreInfoDAL.GetStoreInfoById(id);
        }
        /// <summary>
        /// 根据店铺ID获得对应的ID
        /// </summary>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        public static int getStoreInfoID(string StoreID)
        {
            return StoreInfoDAL.getStoreInfoID(StoreID);
        }

        /// <summary>
        /// 根据会员编号读取信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static DataTable GetMemberInfo(string qishu)
        {
            return StoreInfoDAL.GetMemberInfo(qishu);
        }

        public static DataTable GetMemberPlacement(string placeMent, string qishu)
        {
            return StoreInfoDAL.GetMemberPlacement(placeMent,qishu);
        }

        public static DataTable GetMemberPlacement2(string placeMent, string qishu)
        {
            return StoreInfoDAL.GetMemberPlacement2(placeMent, qishu);
        }


        public static MemberInfoModel getStoreInfoMember(string StoreID)
        {
            return StoreInfoDAL.getStoreInfoMember(StoreID);  
        }

          /// <summary>
        /// 获取店铺的会员编号
        /// 
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="mb"></param>
        /// <returns></returns>
        public static string getStoreInfoMemberNumber(string StoreID)
        {
          return   StoreInfoDAL.getStoreInfoMemberNumber(StoreID);  
        }
        /// <summary>
        /// 获取店铺账户余额
        /// </summary>
        /// <param name="state">账户类型</param>
        /// <param name="storeid">店铺编号</param>
        /// <returns></returns>
        public static string GetStoreMoney(string state, string storeid)
        {
            string money = StoreInfoDAL.GetStoreMoney(state, storeid).ToString();
            string rtval = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            if (state == "0")
            {
                rtval += BLL.Translation.Translate("000471", "剩余报单款订货额") + "：" + money;
            }
            else if (state == "1")
            {
                rtval += BLL.Translation.Translate("000475", "剩余周转款订货额") + "：" + money;
            }
            else
            {
                rtval += BLL.Translation.Translate("000478", "剩余订货额") + "：" + money;
            }
            return rtval;
        }
       

       
        /// <summary>
        /// 获取店铺库存信息用于换货
        /// </summary>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        public  DataTable GetStoreInfo(string storeID)
        {
            InventoryDocDAL inventoryDocDAL = new InventoryDocDAL();
            return inventoryDocDAL.GetStoreStorage(storeID);
        }

        public static DataTable GetStoreOrderInfo(string storeOrderID)
        {
            return StoreOrderDAL.GetStoreOrderInfo(storeOrderID);
        }

    }
}
