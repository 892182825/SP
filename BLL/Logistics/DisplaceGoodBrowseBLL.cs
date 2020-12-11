using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace BLL.Logistics
{
    /// <summary>
    /// 换货处理菜单(公司子系统）
    /// </summary>
    /// 
    
  
    public class DisplaceGoodBrowseBLL
    {
        DisplaceGoodBrowseDAL displaceGoodBrowseDAL = new DisplaceGoodBrowseDAL();
        /// <summary>
        /// 根据换货单ID 返回对应换货单明细
        /// </summary>
        /// <returns></returns>
        public List<ReplacementDetailModel> GetReplacementDetailListById()
        {
            return null;
        }
       /// <summary>
        /// 检查店铺编号是否存在
        /// 宋俊
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static bool CheckStoreId(string storeid)
        {

            return StoreInfoDAL.CheckStoreId(storeid);
        }
        /// <summary>
        /// 根据条件查询出换货情况
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableByCondition(string sql)
        {
            return displaceGoodBrowseDAL.GetTableByCondition(sql);
        }

        /// <summary>
        /// 添加换货单
        /// </summary>
        /// <param name="remittancesItem">换货单对象</param>
        /// <param name="replacementDetailList">对应换货单明细集合</param>
        /// <returns></returns>
        public Boolean AddReplacementItem(RemittancesModel remittancesItem, IList<ReplacementDetailModel> replacementDetailList)
        {
            return false;
        }
         /// <summary>
        ///  查看换货时库存情况
        /// 
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public DataTable GetReplacement(string condition)
        {
            InventoryDocDAL inventoryDocDAL=new InventoryDocDAL ();
            //return inventoryDocDAL.GetReplacement(condition);
            return null;
        }
        /// <summary>
        /// 根据换货单号得到换货详情
        /// </summary>
        /// <returns></returns>
        public DataTable GetReplacementDetail(string displaceOrderID)
        {
            return displaceGoodBrowseDAL.GetReplacementDetail(displaceOrderID);
        }

        public DataTable GetdisplaceReplace(string displaceOrderID)
        {
            return displaceGoodBrowseDAL.GetdisplaceReplace(displaceOrderID);
        }
        //////////////////////////////////////////////////////////////
        /// <summary>
        /// 根据退货单号统计已审核的退货单
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public int GetStateDisplaceDocByDocId(string displaceOrderID)
        {
            return displaceGoodBrowseDAL.GetStateDisplaceDocByDocId(displaceOrderID);
        }
        #region 判断换货店的库存是否大于等于退货的数量
        /// </summary>@StoreID  varchar(20)   ,
        /// 判断换货店的库存是否大于等于退货的数量
        /// @DisplaceOrderID   varchar(20)
        /// <returns></returns>
        public int CheckStoreGreaterThanDisplaceQuantity(string storeID, string displaceOrderID)
        {
            return displaceGoodBrowseDAL.CheckStoreGreaterThanDisplaceQuantity(storeID,displaceOrderID);
        }
        #endregion
        #region 判断进货数量是否小于等于公司库存数量
        /// <summary>
        /// 判断进货数量是否小于等于公司库存数量
        /// </summary>@StoreID  varchar(20)   ,
        /// @DisplaceOrderID   varchar(20)
        /// <returns></returns>
        public int CheckCompanyGreaterThanOderQuantity(string displaceOrderID)
        {
            return displaceGoodBrowseDAL.CheckCompanyGreaterThanOderQuantity(displaceOrderID);
        }
        #endregion

        #region 判断退货额加剩余订货额是否小于等于预进货额
        /// <summary>
        /// 判断进货数量是否小于等于公司库存数量
        /// </summary>@StoreID  varchar(20)   ,
        /// @DisplaceOrderID   varchar(20)
        /// <returns></returns>
        public decimal CheckMoneyWheatherEnough(string storeID, string displaceOrderID)
        {
            return displaceGoodBrowseDAL.CheckMoneyWheatherEnough(storeID,displaceOrderID);
        }
        #endregion

        
        //根据换货单号更新店铺换货为已审
        public bool UpdateReplacement(string DisplaceOrderId, string storeOrderId, string refundmentOrderID, string storeID, int expectNum, string warehouseId, string depotSeatId)
        {
            bool state = false;

            double returnmoney = DisplaceGoodBrowseDAL.GetGoodsReturnMoney(DisplaceOrderId);

            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                   

                    new DisplaceGoodBrowseDAL().UpdateReplacement(tran,DisplaceOrderId, storeOrderId, refundmentOrderID, storeID, expectNum, warehouseId, depotSeatId);

                    //添加对账单
                    if (returnmoney > 0)
                    {
                        D_AccountBLL.AddStoreAccount(storeID, Convert.ToDouble(returnmoney), D_AccountSftype.StoreDingHuokuan, S_Sftype.dianhuo, D_AccountKmtype.ReturnRebate, DirectionEnum.AccountsIncreased, refundmentOrderID, tran);
                    }
                    else if (returnmoney < 0)
                    {
                        D_AccountBLL.AddStoreAccount(storeID, Convert.ToDouble(returnmoney), D_AccountSftype.StoreDingHuokuan, S_Sftype.dianhuo, D_AccountKmtype.ReturnCharge, DirectionEnum.AccountReduced, refundmentOrderID, tran);
                    }
                    tran.Commit();
                    state = true;
                }
                catch
                {
                    tran.Rollback();
                    state = false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return state;
        }


               //换货调用存储过程来生成各种订单并修改相应数据库
        public string UpdateReplacementUseProc(string displaceOrderId, string outStorageOrderId, string storeOrderId, string refundmentOrderID, string storeID, int expectNum)
        {
            return displaceGoodBrowseDAL.UpdateReplacementUseProc(displaceOrderId,outStorageOrderId,storeOrderId,refundmentOrderID,storeID,expectNum);

        }
        //////////////////////////////////////////////////////////////
        /// <summary>
        /// 根据换货单号统计已审核的换货单
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public int GetStaDisplaceDocByDocId(string displaceOrderID)
        {
            return displaceGoodBrowseDAL.GetStaDisplaceDocByDocId(displaceOrderID);
        }
        /// <summary>
        /// 更新订单的状态为无效的
        /// </summary>
        /// <param name="docID"></param>
        public void UpdateStateFlagAndCloseFlag(string displaceOrderID)
        {
            displaceGoodBrowseDAL.UpdateStateFlagAndCloseFlag(displaceOrderID);
        }
          //根据换货单号删除换货单信息与详细
        public void DeleteDisplaceGoodsOrderAndOrderDetail(string displaceOrderID)
        {
            displaceGoodBrowseDAL.DeleteDisplaceGoodsOrderAndOrderDetail(displaceOrderID);
        }
        //根据换货单号得到换货单详细
        /// <summary>
        /// 根据换货单号得到换货单详细
        /// </summary>
        /// <returns></returns>
        public ReplacementModel GetReplacementlModelByDisplaceOrderID(string displaceOrderID)
        {
            return displaceGoodBrowseDAL.GetReplacementlModelByDisplaceOrderID(displaceOrderID);
        }
      

        
        //根据换货单号得到换货单详细
        /// 添加换货单和单据明细信息
        /// </summary>
        /// <param name="ReplacementModel">换货单</param>
        /// 根据换货单号得到换货单详细
        /// </summary>
        /// <returns></returns>
        public void SaveReplacementlModelByDisplaceOrderID(string displaceOrderID, string number, ReplacementModel replacementModel, ArrayList displaceList)
        {
             displaceGoodBrowseDAL.SaveReplacementlModelByDisplaceOrderID(displaceOrderID,number,replacementModel,displaceList);
        }

        //添加换货信息
        public void ADDReplacement(ReplacementModel replacementModel, ArrayList displaceList)
        {
            displaceGoodBrowseDAL.ADDReplacement(replacementModel,displaceList);
        }

        //得到未审的换货单信息
        public DataTable GetNoShenHe(string condition)
        {
            return displaceGoodBrowseDAL.GetNoShenHe(condition);
        }
             //得到换货信息用于编辑页面中绑定表单
        public DataTable GetRemplacementTable(string storeId, string displaceOrderId)
        {
            return displaceGoodBrowseDAL.GetRemplacementTable(storeId,displaceOrderId);
        }
        //查看明细
        public string GetRemark(string displaceorderid)
        {
            return displaceGoodBrowseDAL.GetRemark(displaceorderid);
        }


        //根据订单号找店铺编号
        public string GetStoreId(string OrderId)
        {
            return displaceGoodBrowseDAL.GetStoreId(OrderId);
        }
    }
}
