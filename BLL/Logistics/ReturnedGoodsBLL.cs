using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using Model.Other;
using DAL;
using System.Collections;
using DAL.Other;

namespace BLL.Logistics
{
    public class ReturnedGoodsBLL
    {
        CountryDAL countryDAL = new CountryDAL();
        DocTypeTableDAL docTypeTableDAL = new DocTypeTableDAL();
        WareHouseDAL wareHouseDAL = new WareHouseDAL();
        StoreInfoDAL storeInfoDAL = new StoreInfoDAL();
        StockDAL stockDAL = new StockDAL();
        InventoryDocDAL inventoryDocDAL = new InventoryDocDAL();
        InventoryDocDetailsDAL inventoryDocDetailsDAL = new InventoryDocDetailsDAL();
        SqlDataReaderHelp help = new SqlDataReaderHelp();
        /// <summary>
        /// 得到指定页码数据
        /// </summary>
        /// <param name="PageIndex">页码</param>
        /// <param name="PageSize">页记录数</param>
        /// <param name="table">表名</param>
        ///<param name="columns">列</param>
        /// <param name="condition">条件</param>
        /// <param name="key">关键字</param>
        /// <param name="RecordCount">总记录数</param>
        ///<param name="PageCount">页数</param>
        /// <returns></returns>
        public DataTable GetDataTablePage(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
            return help.GetDataTablePage(PageIndex, PageSize, table, columns, condition, key, out RecordCount, out PageCount);
        }
        /// <summary>
        /// 返回国家信息表
        /// </summary>
        /// <returns></returns>
        public IList<CountryModel> GetCountryIdAndName()
        {
            return CountryDAL.GetCountryIdAndName();
        }
        /// <summary>
        /// 根据订单类型名称查询出订单ID
        /// </summary>
        /// <param name="docTypeName"></param>
        /// <returns></returns>
        public string GetDocTypeIdByDocTypeName(string docTypeName)
        {
            return DocTypeTableDAL.GetDocTypeIDByDocTypeName(docTypeName).ToString();
        }
        /// <summary>
        /// 读取仓库名称
        /// </summary>		
        /// <returns></returns>
        public string GetWareHouseNameByID(string warehouseId)
        {
            return WareHouseDAL.GetWareHouseNameByID(warehouseId);
        }
        /// <summary>
        /// 读取产品基础数据的仓库信息
        /// CYB
        /// </summary>		
        /// <returns></returns>
        public DataTable GetProductWareHouseInfo()
        {
            return WareHouseDAL.GetProductWareHouseInfo();
        }

        /// <summary>
        /// 根据仓库ID绑定库位
        /// </summary>		
        /// <returns></returns>
        public void GetDepotSeat(System.Web.UI.WebControls.DropDownList ddlDepotSeat,string wID)
        {
            ddlDepotSeat.Items.Clear();
            foreach (DataRow dr in DepotSeatDAL.GetDepotSeat(wID).Rows)
            { 
                ddlDepotSeat.Items.Add(new System.Web.UI.WebControls.ListItem(dr["SeatName"].ToString(),dr["DepotSeatID"].ToString()));
            }
        }

        /// <summary>
        /// 根据仓库ID绑定库位
        /// </summary>		
        /// <returns></returns>
        public static DataTable GetDepotSeat(string wID)
        {
            return DepotSeatDAL.GetDepotSeat(wID);
        }

        /// <summary>
        /// 得到单铺的邮政编码，店名，店长名，店地址，电话
        /// cyb
        /// </summary>
        public StoreInfoModel GetStorInfoByStoreid(string storeid)
        {
            return StoreInfoDAL.GetStorInfoByStoreid(storeid);
        }
        /// <summary>
        /// 增加退货单时需要显示指定店铺库存情况
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public DataTable GetStrockSByStoreid(string storeid)
        {
            return StockDAL.GetStrockSByStoreid(storeid);
        }
        /// <summary>
        /// 根据单据类型的不同来获取不同的单据ID
        /// </summary>
        /// <param name="orderType">单据类型</param>
        /// <returns></returns>
        public string GetDocId(EnumOrderFormType orderType)
        {
            return InventoryDocDAL.GetDocId(orderType);
        }
        /// <summary>
        /// 添加退货单和单据明细信息
        /// </summary>
        /// <param name="nventoryDocModel">退货单</param>
        /// <param name="number">管理员ID</param>
        /// <param name="storeId">店铺ID</param>
        /// <param name="inventoryDocDetailsList">退货单明细集合</param>
        public void InsertInventoryDoc(InventoryDocModel nventoryDocModel, string number, string storeId, ArrayList inventoryDocDetailsList)
        {
            InventoryDocDAL.InsertInventoryDoc(nventoryDocModel, number, storeId, inventoryDocDetailsList);
        }

        /// <summary>
        ///  获取店铺币种
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public int GetStoreRate(string storeid)
        {
            return StoreInfoDAL.GetStoreRate(storeid);
        }

        //////////////////////////////////////////////////////////////
        /// <summary>
        /// 根据退货单号统计已审核的退货单
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public int GetStaInventoryDocByDocId(string docId)
        {
            return InventoryDocDAL.GetStaInventoryDocByDocId(docId);
        }
        /// <summary>
        /// 根据订单号查询出订单明细表的产品
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public DataTable GetProductIdAndQuantityByDocId(string docId)
        {
            return InventoryDocDetailsDAL.GetProductIdAndQuantityByDocId(docId);
        }
        /// <summary>
        /// 根据产品编号，得到此产品的剩余可用数量
        /// </summary>
        /// <param name="productID">产品编号</param>
        /// <param name="productID">店铺编号</param>
        /// <returns></returns>
        public int GetCertainProductLeftStoreCount(string productID, string storeId)
        {
            return StockDAL.GetCertainProductLeftStoreCount(productID, storeId);
        }
        /// <summary>
        /// 更新退货款状态为已审核
        /// </summary>
        /// <param name="warehouseId">仓库ID</param>
        /// <param name="number">管理员账号</param>
        /// <param name="docId">退货单号</param>
        /// <param name="storeId">退货店铺ID</param>
        public void UpdateStaInventoryDocOfStateFlag(string depotSeatId,string warehouseId, string number, string docId, string storeId)
        {
            InventoryDocDAL.UpdateStaInventoryDocOfStateFlag(depotSeatId,warehouseId, number, docId, storeId);
        }
        /// <summary>
        /// 根据订单号查询出订单明细表
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public DataTable GetProductsByDocId(string docId)
        {
            return InventoryDocDetailsDAL.GetProductsByDocId(docId);
        }

        public static DataTable GetProById(string docId)
        {
            return InventoryDocDetailsDAL.GetProById(docId);
        }
        /// <summary>
        /// 根据单据号查询单据日志
        /// </summary>
        /// <param name="docId">单据号</param>
        /// <returns></returns>
        public string GetNoteByDocId(string docId)
        {
            return InventoryDocDAL.GetNoteByDocId(docId);
        }
        /// <summary>
        /// 更新订单的状态为无效的
        /// </summary>
        /// <param name="docID"></param>
        public void UpdateStateFlagAndCloseFlag(string docID)
        {
            InventoryDocDAL.UpdateStateFlagAndCloseFlag(docID);
        }
        //根据条件得到单据信息
        public DataTable GetInventoryDocByCondition(string condition)
        {
            return InventoryDocDAL.GetInventoryDocByCondition(condition);
        }

        /// <summary>
        /// 根据店铺编号获取店铺汇率
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns>店铺汇率</returns>
        public static double GetStoreCurrency(string storeid)
        {
            return StoreInfoDAL.GetStoreCurrencyByStoreId(storeid);
        }

        #region 获取退货款备注
        /// <summary>
        /// 获取退货款备注——ds2012——tianfeng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string QueryRemark(string id)
        {
            return InventoryDocDAL.QueryRemark(id);
        }
        #endregion

        public string GetDocIdByTypeCode(string DocTypeCode)
        {
            return new InventoryDocDAL().GetDocIdByTypeCode(DocTypeCode);
        }

    }
}
