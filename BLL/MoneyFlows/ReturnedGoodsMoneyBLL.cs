using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using DAL;
using BLL.CommonClass;
namespace BLL.MoneyFlows
{ 
    /*
     * 退货款管理 
     */ 
    public class ReturnedGoodsMoneyBLL
    { 
        /// <summary>
        /// 条件查询
        /// </summary>
        public List<InventoryDocModel>  GetInventoryDocModel(InventoryDocModel InventoryDocModel,string mark,int page,out int count )
        {
            ReturnedGoodsMoneyDAL Goods = new ReturnedGoodsMoneyDAL();
            return Goods.GetInventoryDocModel(InventoryDocModel, mark,page,out count);
        }
        /// <summary>
        /// 退货单详细
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static  List<InventoryDocDetailsModel> GetInventoryDoctails(string ID)
        {
             return ReturnedGoodsMoneyDAL.GetInventoryDoctails(ID);
        }
        /// <summary>
        /// 填写退货退款单 ——ds2012——tianfeng
        /// </summary>
        /// <param name="DocID">订单号</param>
        /// <param name="StoreID">店编号</param>
        /// <returns></returns>
        public static Boolean UPtInventoryDoc(string DocID, string StoreID,int flag,double money,string reason,Boolean bol)
        {

            return ReturnedGoodsMoneyDAL.UPtInventoryDoc(DocID, StoreID, flag, money, reason, bol, CommonDataBLL.OperateBh, CommonDataBLL.OperateIP,CommonDataBLL.getMaxqishu());
        }

        /// <summary>
        /// 获取单据类型的ID
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetDocTypeID(string type) 
        {
            return ReturnedGoodsMoneyDAL.GetDocTypeID(type);
        }
        /// <summary>
        /// 获取币种
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string GetMoneyType(string TypeID)
        {
            return ReturnedGoodsMoneyDAL.GetMoneyType(TypeID);
        }
        /// <summary>
        /// 获取产品名称
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static string GetProductName(string productID)
        {
            return ReturnedGoodsMoneyDAL.GetProductName(productID);
        }
        /// <summary>
        /// 查看备注
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static string GetNote(string docid)
        {
            return ReturnedGoodsMoneyDAL.GetNote(docid);
        }
         /// <summary>
        /// 获取退货款信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="docID"></param>
        /// <returns></returns>
        public static InventoryDocModel GetInventory(int ID, string docID)
        {
           return ReturnedGoodsMoneyDAL.GetInventory(ID,docID);
        }
        // 获取退货款信息是否有效
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="docID"></param>
        /// <returns></returns>
        public static Boolean GetInventoryState(string docid)
        {
            return ReturnedGoodsMoneyDAL.GetInventoryState(docid);
        }
    }
}
