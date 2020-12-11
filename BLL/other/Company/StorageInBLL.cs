using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

//Add Namespace
using DAL;
using Model;
using Model.Other;
using System.Data;
using System.Data.SqlClient;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-08
 * 文件名：     StorageInBLL
 */

namespace BLL.other.Company
{
    /// <summary>
    /// 入库单
    /// </summary>
    public class StorageInBLL
    {
        public static InventoryDocModel getOrderInfo(string billID)
        {
            return InventoryDocDAL.getOrderInfo(billID);
        }
        /// <summary>
        /// 获取产品列表和入库单信息  ---DS2012
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public static DataTable getProduct(string billID)
        {
            return InventoryDocDAL.getProduct(billID);
        }
        public static int CheckBatch(string availableOrderID, string BatchCode)
        {
            return InventoryDocDAL.CheckBatch(availableOrderID,BatchCode);
        }
        public static int updAndSaveOrder(InventoryDocModel idm, ArrayList list)
        {
            return InventoryDocDAL.updAndSaveOrder(idm,list);
        }

        /// <summary>
        ///  生成一个单据入库
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="inventoryDoc">库存单据模型</param>
        /// <returns>返回插入单据入库所影响的行数</returns>
        public static int CreateInventoryDoc(SqlTransaction tran, InventoryDocModel inventoryDoc)
        {
           return InventoryDocDAL.CreateInventoryDoc(tran,inventoryDoc);
        }

        /// <summary>
        /// 生成一个单据，包括各种单据［出库，入库，红单，退货等］，返回受影响的行数 ---DS2012
        /// </summary>
        /// <param name="tran">处理事务</param>
        /// <param name="inventoryDocModel">某种单据类对象</param>
        /// <returns></returns>
        public static int CreateInventoryDoc_WH(SqlTransaction tran, InventoryDocModel inventoryDocModel)
        {
            return InventoryDocDAL.CreateInventoryDoc_WH(tran, inventoryDocModel);
        }

        /// <summary>
        /// 通过管理员编号获取角色ID
        /// </summary>
        /// <param name="number">管理员编号</param>
        /// <returns>返回角色ID</returns>
        public static int GetRoleIDByNumber(string number)
        {
            return ManageDAL.GetRoleIDByNumber(number);
        }
 
        /// <summary>
        /// 通过管理员编号获取管理员姓名
        /// </summary>
        /// <param name="number">管理员编号</param>
        /// <returns>返回管理员姓名</returns>
        public static string GetManageNameByNumber(string number)
        {
            return ManageDAL.GetManageNameByNumber(number);
        }

        /// <summary>
        /// 通过管理员编号获取仓库相应的权限
        /// </summary>
        /// <param name="number">管理员编号</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMoreManagerPermissionByNumber(string number)
        {
            return ManagerPermissionDAL.GetMoreManagerPermissionByNumber(number);
        }

        /// <summary>
        /// Get the information of the WareHouse by Number and CountryCode
        /// </summary>
        /// <param name="number">Number</param>
        /// <param name="countryCode">CountryCode</param>
        /// <returns>Return DataTable Object</returns>
        public static DataTable GetWareHouseInfoByNumberCountryCode(string number, string countryCode)
        {
            return WareHouseDAL.GetWareHouseInfoByNumberCountryCode(number,countryCode);
        }

        /// <summary>
        /// 通过仓库ID获取库位信息
        /// </summary>
        /// <param name="wareHouseID">仓库ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetDepotSeatInfoByWareHouaseID(int wareHouseID)
        {
            return DepotSeatDAL.GetDepotSeatInfoByWareHouaseID(wareHouseID);
        }

        /// <summary>
        /// 通过入库批次获取入库批次行数
        /// </summary>
        /// <param name="batchCode">入库批次</param>
        /// <returns>返回入库批次行数</returns>
        public static int GetCountByBatchCode(string batchCode)
        {
           return  InventoryDocDAL.GetCountByBatchCode(batchCode);  
        }

        /// <summary>
        /// 通过产品ID联合查询获取汇率名称
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <returns>返回汇率名称</returns>
        public static string GetMoreCurrencyNameByProductID(int productID)
        {
            return CurrencyDAL.GetMoreCurrencyNameByProductID(productID);
        }

        /// <summary>
        /// 通过国家编码联合查询获取汇率名称
        /// </summary>
        /// <param name="countryCode">国家编码</param>
        /// <returns>返回汇率名称</returns>
        public static string GetMoreCurrencyNameByCountryCode(string countryCode)
        {
            return CurrencyDAL.GetMoreCurrencyNameByCountryCode(countryCode);
        }

        /// <summary>
        /// 从库存单据表中获取最大的ID
        /// </summary>
        /// <returns>返回SqlDataReader对象</returns>
        public static SqlDataReader GetMaxIDFromInventoryDoc()
        {
           return InventoryDocDAL.GetMaxIDFromInventoryDoc();          
        }

        /// <summary>
        /// 获取新的订单号
        /// </summary>
        /// <param name="enumOrderType">单据类型</param>
        /// <returns>返回新订单号</returns>
        public static string GetNewOrderID(EnumOrderFormType enumOrderType)
        {
            return InventoryDocDAL.GetNewOrderID(enumOrderType);            
        }

        /// <summary>
        /// 生成单据明细
        /// </summary>
        /// <param name="tran">处理事务</param>
        /// <param name="depotManageDoc">某种单据明细类对象数组</param>
        /// <returns>返回插入所影响的行数</returns>
        public static int CreateBillofDocumentDetails(SqlTransaction tran, ArrayList docDetailsList)
        {
            return InventoryDocDetailsDAL.CreateBillofDocumentDetails(tran, docDetailsList);
        }

        /// <summary>
        /// 根据单据名获取单据类型ID
        /// </summary>
        /// <param name="docTypeName">单据名称</param>
        /// <returns>返回单据类型ID</returns>    
        public static int GetDocTypeIDByDocTypeName(string docTypeName)
        {
            return DocTypeTableDAL.GetDocTypeIDByDocTypeName(docTypeName);
        }

        /// <summary>
        /// DropDownList绑定供应商相关信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProviderInfo()
        {
            return ProviderInfoDAL.GetProviderInfo();
        }

        public static int CreateNewBillofDocument(InventoryDocModel tobjopda_depotManageDoc)
        {
           return InventoryDocDetailsDAL.CreateNewBillofDocument(tobjopda_depotManageDoc);
             
        }
        public static int CreateNewBillofDocument(SqlTransaction tran, InventoryDocModel tobjopda_depotManageDoc)
        {
            InventoryDocDetailsDAL.CreateNewBillofDocument(tran, tobjopda_depotManageDoc);
            return -1;
        }

        /// <summary>
        /// 通过联合查询获取币种ID
        /// </summary>
        /// <param name="countryCode">国家编码</param>
        /// <returns>返回币种ID</returns>
        public static int GetMoreCurrencyIDByCountryCode(string countryCode)
        {
            return CurrencyDAL.GetMoreCurrencyIDByCountryCode(countryCode);
        }

        /// <summary>
        /// 通过联合查询获取标准币种ID
        /// </summary>
        /// <param name="countryCode">国家编码</param>
        /// <returns>返回标准币种ID</returns>
        public static int GetMoreStandardMoneyIDByCountryCode(string countryCode)
        {
            return CurrencyDAL.GetMoreStandardMoneyIDByCountryCode(countryCode);
        }

        /// <summary>
        /// 获取该国家相对标准币种的汇率倍数
        /// </summary>
        /// <param name="countryCode">CountryCode</param>
        /// <returns>Return rate times</returns>
        public static decimal GetRate_TimesForStandardMoneyByCountryCode(string countryCode)
        {
            return CurrencyDAL.GetRate_TimesForStandardMoneyByCountryCode(countryCode);
        }

        /// <summary>
        /// 获取产品ID通过名称模糊查询   ---DS2012
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static string GetPID(string name)
        {
            string PID = "";
            DataTable dt = ProductDAL.GetProductID(name);
            foreach (DataRow dr in dt.Rows)
            {
                PID += dr["productID"].ToString() + ",";
                GetPPID(dr["productID"].ToString(),ref PID);
            }
            if (PID.Length > 1)
            {
                PID = PID.Substring(0, PID.Length - 1);
            }
            return PID;
        }

        /// <summary>
        /// 获取产品ID通过名称查询   ---DS2012
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public static void GetPPID(string ID,ref string PID)
        {
            DataTable dt = ProductDAL.GetPProductID(ID);
            foreach (DataRow dr in dt.Rows)
            {
                PID += dr["productID"].ToString() + ",";
                GetPPID(dr["productID"].ToString(),ref PID);
            }
        }
    }
}
