using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using DAL;
using Model;

/*
 * Creator：    WangHua
 * CreateDate： 2010-01-01
 * FinishDate:  2010-04-01
 * Menu:        SystemeManagement->SetParameters
 */

namespace BLL.other.Company
{
    public class SetParametersBLL
    {
        /// <summary>
        /// 添加产品颜色
        /// </summary>
        /// <param name="productColor">产品颜色模型</param>
        /// <returns>返回添加产品颜色所影响的行数</returns>
        public static int AddProductColor(SqlTransaction tran, ProductColorModel productColor, out int id)
        {
            return ProductColorDAL.AddProductColor(tran, productColor, out id);
        }

        /// <summary>
        /// 添加产品颜色
        /// </summary>
        /// <param name="productColor">产品颜色模型</param>
        /// <returns>返回添加产品颜色所影响的行数</returns>
        public static int AddProductColor(ProductColorModel productColor, out int id)
        {
            return ProductColorDAL.AddProductColor(productColor, out id);
        }
        /// <summary>
        /// 向产品适用人群表中插入记录
        /// </summary>
        /// <param name="productSexType">适用人群模型</param>
        /// <returns>返回插入记录所影响的行数</returns>
        public static int AddProductSexType(SqlTransaction tran, ProductSexTypeModel productSexType, out int id)
        {
            return ProductSexTypeDAL.AddProductSexType(tran, productSexType, out id);
        }

        /// <summary>
        /// 插入产品尺寸记录
        /// </summary>
        /// <param name="productSize">产品尺寸模型</param>
        /// <returns>返回插入产品尺寸记录所影响的行数</returns>
        public static int AddProductSize(ProductSizeModel productSize)
        {
            return ProductSizeDAL.AddProductSize(productSize);
        }

        /// <summary>
        /// 向产品规格表中插入记录
        /// </summary>
        /// <param name="productSpec">产品规格ID</param>
        /// <returns>返回向产品规格表中插入记录所影响的行数</returns>
        public static int AddProductSpec(ProductSpecModel productSpec)
        {
            return ProductSpecDAL.AddProductSpec(productSpec);
        }

        /// <summary>
        /// 向产品状态表中插入记录
        /// </summary>
        /// <param name="productStatus">产品状态模型</param>
        /// <returns>返回向产品状态表中插入记录所影响的行数</returns>
        public static int AddProductStatus(SqlTransaction tran, ProductStatusModel productStatus, out int id)
        {
            return ProductStatusDAL.AddProductStatus(tran, productStatus, out id);
        }

        /// <summary>
        /// 向产品型号表中插入相关记录
        /// </summary>
        /// <param name="productType">产品型号模型</param>
        /// <returns>返回向产品型号表中插入相关记录所影响的行数</returns>
        public static int AddProductType(ProductTypeModel productType)
        {
            return ProductTypeDAL.AddProductType(productType);
        }

        /// <summary>
        /// 向产品状态单位表中插入记录
        /// </summary>
        /// <param name="tran">Transaction</param>
        /// <param name="productUnit">产品单位模型</param>
        /// <param name="id">Id</param>
        /// <returns>返回向产品状态单位表中插入记录所影响的行数</returns>
        public static int AddProductUnit(SqlTransaction tran, ProductUnitModel productUnit, out int id)
        {
            return ProductUnitDAL.AddProductUnit(tran, productUnit, out id);
        }

        /// <summary>
        /// 向单据类型（冲红）表中插入记录
        /// </summary>
        /// <param name="docTypeTable">单据类型（冲红）模型</param>
        /// <returns>返回向单据类型（冲红）表中插入记录所影响的行数</returns>
        public static int AddDocTypeTable(DocTypeTableModel docTypeTable)
        {
            return DocTypeTableDAL.AddDocTypeTable(docTypeTable);
        }

        /// <summary>
        /// 向城市表中插入相关记录
        /// </summary>
        /// <param name="city">城市模型</param>
        /// <returns>返回向城市表中插入相关记录所影响的行数</returns>
        public static int AddCity(CityModel city)
        {
            return CityDAL.AddCity(city);
        }

        /// <summary>
        /// 向会员使用银行表中插入相关记录
        /// </summary>
        /// <param name="memberBank">会员使用银行模型</param>
        /// <returns>返回向会员使用银行表中插入相关记录所影响的行数</returns>
        public static int AddMemberBank(MemberBankModel memberBank)
        {
            return MemberBankDAL.AddMemberBank(memberBank);
        }

        /// <summary>
        /// 向会员报单底线表中相关插入记录
        /// </summary>
        /// <param name="orderBaseLineMoney">会员订单底线金额</param>
        /// <returns>返回向会员报单底线表中相关插入记录所影响的行数</returns>
        public static int AddMemOrderLine(double orderBaseLineMoney)
        {
            return MemOrderLineDAL.AddMemOrderLine(orderBaseLineMoney);
        }

        /// <summary>
        /// 添加仓库信息
        /// </summary>
        /// <param name="psm">仓库模型</param>
        /// <returns>返回添加仓库模型所影响的行数</returns>
        public static int AddWareHouse(WareHouseModel psm)
        {
            return WareHouseDAL.AddWareHouse(psm);
        }

        /// <summary>
        /// 向库位表中插入记录
        /// </summary>
        /// <param name="depotSeatModel">库位模型</param>
        /// <returns>返回向库位表中插入记录所影响的行数</returns>
        public static int AddDepotSeat(DepotSeatModel depotSeatModel)
        {
            return DepotSeatDAL.AddDepotSeat(depotSeatModel);
        }

        /// <summary>
        /// Delete the ProductColor information by productColorId
        /// </summary>
        /// <param name="tran">Transaction</param>
        /// <param name="productColorID">ProductColorId</param>
        /// <returns>Return affected row counts which delete the ProductColor information by productColorId</returns>
        public static int DelProductColorByID(SqlTransaction tran, int productColorID)
        {
            return ProductColorDAL.DelProductColorByID(tran, productColorID);
        }

        /// <summary>
        /// Delete the ProductSexType information by productSexTypeId
        /// </summary>
        /// <param name="tran">Transaction</param>
        /// <param name="productSexTypeID">ProductSexTypeId</param>
        /// <returns>Return affected row counts delete the ProductSexType information by productSexTypeId</returns>
        public static int DelProductSexTypeByID(SqlTransaction tran, int productSexTypeID)
        {
            return ProductSexTypeDAL.DelProductSexTypeByID(tran, productSexTypeID);
        }

        /// <summary>
        /// 删除指定的产品尺寸记录
        /// </summary>
        /// <param name="productSizeID">产品尺寸ID</param>
        /// <returns>删除指定的产品尺寸记录</returns>
        public static int DelProductSizeByID(int productSizeID)
        {
            return ProductSizeDAL.DelProductSizeByID(productSizeID);
        }

        /// <summary>
        /// 删除指定产品规格记录
        /// </summary>
        /// <param name="productSpecID">产品规格ID</param>
        /// <returns>返回删除指定产品规格记录所影响的行数</returns>
        public static int DelProductSpecByID(int productSpecID)
        {
            return ProductSpecDAL.DelProductSpecByID(productSpecID);
        }

        /// <summary>
        /// Delete the ProductStatus information by productStatusId
        /// </summary>
        /// <param name="tran">Transaction</param>
        /// <param name="productStatusID">ProductStatusId</param>
        /// <returns>Return affeted counts which delete the ProductStatus information by productStatusId</returns>
        public static int DelProductStatusByID(SqlTransaction tran, int productStatusID)
        {
            return ProductStatusDAL.DelProductStatusByID(tran, productStatusID);
        }

        /// <summary>
        /// 删除指定产品型号信息
        /// </summary>
        /// <param name="productTypeID">产品型号ID</param>
        /// <returns>返回删除指定产品型号信息所影响的行数</returns>
        public static int DelProductTypeByID(int productTypeID)
        {
            return ProductTypeDAL.DelProductTypeByID(productTypeID);
        }

        /// <summary>
        /// Delete the productUint information by productUnitId
        /// </summary>
        /// <param name="tran">Transaction</param>
        /// <param name="productUnitID">ProductUnitId</param>
        /// <returns>Return affected couts which delete the productUint information by productUnitId</returns>
        public static int DelProductUnitByID(SqlTransaction tran, int productUnitId)
        {
            return ProductUnitDAL.DelProductUnitByID(tran, productUnitId);
        }

        /// <summary>
        /// 删除指定单据类型（冲红）记录
        /// </summary>
        /// <param name="docTypeID">单据类型ID</param>
        /// <returns>返回删除指定单据类型（冲红）记录</returns>
        public static int DelDocTypeTableByID(int docTypeID)
        {
            return DocTypeTableDAL.DelDocTypeTableByID(docTypeID);
        }

        /// <summary>
        /// 删除指定城市信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回删除指定城市信息所影响的行数</returns>
        public static int DelCityByID(int id)
        {
            return CityDAL.DelCityByID(id);
        }

        /// <summary>
        /// 删除指定的银行信息
        /// </summary>
        /// <param name="bankID">银行编号</param>
        /// <returns>返回删除指定的银行信息所影响的行数</returns>
        public static int DelMemberBankByID(int bankID)
        {
            return MemberBankDAL.DelMemberBankByID(bankID);
        }
        /// <summary>
        /// 查询仓库信息
        /// </summary>
        /// <param name="WareHouseID"></param>
        /// <returns></returns>
        public static WareHouseModel GetWareHouseItem(int WareHouseID)
        {
            return WareHouseDAL.GetWareHouseItem(WareHouseID);
        }

        /// <summary>
        /// 删除指定仓库信息
        /// </summary>
        /// <param name="WareHouseID">仓库ID</param>
        /// <returns>返回删除指定仓库信息所影响的行数</returns>
        public static int DelWareHouseByWareHouseID(int WareHouseID)
        {
            return WareHouseDAL.DelWareHouseByWareHouseID(WareHouseID);
        }

        /// <summary>
        /// 删除指定的库位信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>返回删除指定的库位信息所影响的行数</returns>
        public static int DelDepotSeatByID(int id)
        {
            return DepotSeatDAL.DelDepotSeatByID(id);
        }
        /// <summary>
        /// 获得当前仓库的库位数量
        /// </summary>
        /// <param name="WareHoseID">仓库ID
        /// <returns></returns>
        public static int getDepotSeatCountByWareHoseID(int WareHoseID)
        {
            return DepotSeatDAL.getDepotSeatCountByWareHoseID(WareHoseID);
        }
        /// <summary>
        /// 删除指定的库位信息根据仓库编号
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>返回删除指定的库位信息所影响的行数</returns>
        public static int DelDepotSeatByWareHouse(int WareHouseID)
        {
            return DepotSeatDAL.DelDepotSeatByWareHouse(WareHouseID);
        }

        /// <summary>
        /// 修改指定产品颜色信息
        /// </summary>
        /// <param name="productColor">产品颜色模型</param>
        /// <returns>返回修改指定产品颜色信息所影响的行数</returns>
        public static int UpdProductColorByID(SqlTransaction tran, ProductColorModel productColor)
        {
            return ProductColorDAL.UpdProductColorByID(tran,productColor);
        }

        /// <summary>
        /// 更新指定产品适用人群
        /// </summary>
        /// <param name="productSexType">适用人群模型</param>
        /// <returns>返回更新指定产品适用人群所影响的行数</returns>
        public static int UpdProductSexTypeByID(ProductSexTypeModel productSexType)
        {
            return ProductSexTypeDAL.UpdProductSexTypeByID(productSexType);
        }

        /// <summary>
        /// 更新指定产品尺寸
        /// </summary>
        /// <param name="productSize">产品尺寸模型</param>
        /// <returns>返回更新指定产品尺寸</returns>
        public static int UpdProductSizeByID(ProductSizeModel productSize)
        {
            return ProductSizeDAL.UpdProductSizeByID(productSize);
        }

        /// <summary>
        /// 更新指定产品规格信息
        /// </summary>
        /// <param name="productSpec">产品规格模型</param>
        /// <returns>返回更新指定产品规格信息所影响的行数</returns>
        public static int UpdProductSpecByID(ProductSpecModel productSpec)
        {
            return ProductSpecDAL.UpdProductSpecByID(productSpec);
        }

        /// <summary>
        /// 更新指定产品状态信息
        /// </summary>
        /// <param name="productStatus">产品状态模型</param>
        /// <returns>返回更新指定产品状态信息所影响的行数</returns>
        public static int UpdProductStatusByID(ProductStatusModel productStatus)
        {
            return ProductStatusDAL.UpdProductStatusByID(productStatus);
        }

        /// <summary>
        /// 更新指定产品模型信息
        /// </summary>
        /// <param name="productType">产品型号模型</param>
        /// <returns>返回更新指定产品模型信息所影响的行数</returns>
        public static int UpdProductTypeByID(ProductTypeModel productType)
        {
            return ProductTypeDAL.UpdProductTypeByID(productType);
        }

        /// <summary>
        /// 更新指定产品单位信息
        /// </summary>
        /// <param name="productUnit">产品单位模型</param>
        /// <returns>返回更新指定产品单位信息所影响的行数</returns>
        public static int UpdProductUnitByID(ProductUnitModel productUnit)
        {
            return ProductUnitDAL.UpdProductUnitByID(productUnit);
        }

        /// <summary>
        /// 更新指定单据类型（冲红）表中记录
        /// </summary>
        /// <param name="docTypeTable">单据类型（冲红）模型</param>
        /// <returns>返回更新指定单据类型（冲红）表中记录所影响的行数</returns>
        public static int UpdDocTypeTableByID(DocTypeTableModel docTypeTable)
        {
            return DocTypeTableDAL.UpdDocTypeTableByID(docTypeTable);
        }

        /// <summary>
        /// 修改指定的城市信息
        /// </summary>
        /// <param name="city">城市模型</param>
        /// <returns>返回修改指定的城市信息所影响的行数</returns>
        public static int UpdCityByID(CityModel city)
        {
            return CityDAL.UpdCityByID(city);
        }

        /// <summary>
        /// 更新指定的银行信息
        /// </summary>
        /// <param name="memberBank">会员使用银行模型</param>
        /// <returns>返回更新指定的银行信息所影响的行数</returns>
        public static int UpdMemberBankByID(MemberBankModel memberBank)
        {
            return MemberBankDAL.UpdMemberBankByID(memberBank);
        }

        /// <summary>
        /// 更新会员订单底线金额
        /// </summary>
        /// <param name="orderBaseLineMoney">会员订单底线金额</param>
        /// <returns>返回更新会员订单底线金额所影响的行数</returns>
        public static int UpdMemOrderLine(double orderBaseLineMoney)
        {
            return MemOrderLineDAL.UpdMemOrderLine(orderBaseLineMoney);
        }

        /// <summary>
        /// 更新指定仓库信息
        /// </summary>
        /// <param name="psm">仓库模型</param>
        /// <returns>返回更新指定仓库信息所影响的行数</returns>
        public static int UpdWareHouseByWareHouseID(WareHouseModel psm)
        {
            return WareHouseDAL.UpdWareHouseByWareHouseID(psm);
        }

        /// <summary>
        /// 更新指定的库位信息
        /// </summary>
        /// <param name="depotSeatModel">库位模型</param>
        /// <returns>返回更新指定的库位信息所影响的行数</returns>
        public static int UpdDepotSeatByID(DepotSeatModel depotSeatModel)
        {
            return DepotSeatDAL.UpdDepotSeatByID(depotSeatModel);
        }

        /// <summary>
        /// 获取产品颜色信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductColorInfo()
        {
            return ProductColorDAL.GetProductColorInfo();
        }

        /// <summary>
        /// 获取适用人群信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductSexTypeInfo()
        {
            return ProductSexTypeDAL.GetProductSexTypeInfo();
        }

        /// <summary>
        /// 获取产品尺寸信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductSizeInfo()
        {
            return ProductSizeDAL.GetProductSizeInfo();
        }

        /// <summary>
        /// 获取产品规格信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductSpecInfo()
        {
            return ProductSpecDAL.GetProductSpecInfo();
        }

        /// <summary>
        /// 获取产品状态信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductStatusInfo()
        {
            return ProductStatusDAL.GetProductStatusInfo();
        }

        /// <summary>
        /// 获取产品类型信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductTypeInfo()
        {
            return ProductTypeDAL.GetProductTypeInfo();
        }

        /// <summary>
        /// 获取产品单位信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductUnitInfo()
        {
            return ProductUnitDAL.GetProductUnitInfo();
        }

        /// <summary>
        /// 获取单据类型（冲红）信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetDocTypeTableInfo()
        {
            return DocTypeTableDAL.GetDocTypeTableInfo();
        }

        /// <summary>
        /// 获取城市信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCityInfo()
        {
            return CityDAL.GetCityInfo();
        }

        /// <summary>
        /// 获取所有的会员使用银行信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMemberBankInfo()
        {
            return MemberBankDAL.GetMemberBankInfo();
        }

        /// <summary>
        /// 获取所有的仓库信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetWareHouseInfo()
        {
            return WareHouseDAL.GetWareHouseInfo();
        }

        /// <summary>
        /// 获取库位信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetDepotSeatInfo()
        {
            return DepotSeatDAL.GetDepotSeatInfo();
        }

        /// <summary>
        /// 获取最大的权限编号
        /// </summary>
        /// <returns>返回最大的权限编号</returns>
        public static int GetMaxWareControlFromWareHouse()
        {
            return WareHouseDAL.GetMaxWareControlFromWareHouse();
        }

        /// <summary>
        /// 获取最大的库位ID
        /// </summary>
        /// <returns>返回最大的库位ID</returns>
        public static int GetMaxDepotSeatIDFromDepotSeat()
        {
            return DepotSeatDAL.GetMaxDepotSeatIDFromDepotSeat();
        }

        /// <summary>
        /// 获取指定的产品颜色信息
        /// </summary>
        /// <param name="productColorID">产品颜色ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductColorInfoByID(int productColorID)
        {
            return ProductColorDAL.GetProductColorInfoByID(productColorID);
        }

        /// <summary>
        /// 获取指定适用人群信息
        /// </summary>
        /// <param name="productSexTypeID">产品适用人群ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductSexTypeInfoByID(int productSexTypeID)
        {
            return ProductSexTypeDAL.GetProductSexTypeInfoByID(productSexTypeID);
        }

        /// <summary>
        /// 获取指定产品尺寸信息
        /// </summary>
        /// <param name="productSizeID">产品尺寸ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductSizeInfoByID(int productSizeID)
        {
            return ProductSizeDAL.GetProductSizeInfoByID(productSizeID);
        }

        /// <summary>
        /// 获取指定产品规格信息
        /// </summary>
        /// <param name="productSpecID">产品规格ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductSpecInfoByID(int productSpecID)
        {
            return ProductSpecDAL.GetProductSpecInfoByID(productSpecID);
        }

        /// <summary>
        /// 获取指定产品状态信息
        /// </summary>
        /// <param name="productStatusID">产品你状态ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductStatusInfoByID(int productStatusID)
        {
            return ProductStatusDAL.GetProductStatusInfoByID(productStatusID);
        }

        /// <summary>
        /// 获取指定产品类型信息
        /// </summary>
        /// <param name="productTypeID">产品型号ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductTypeInfoByID(int productTypeID)
        {
            return ProductTypeDAL.GetProductTypeInfoByID(productTypeID);
        }

        /// <summary>
        /// 获取指定产品单位信息
        /// </summary>
        /// <param name="productUnitID">产品单位名称</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetProductUnitInfoByID(int productUnitID)
        {
            return ProductUnitDAL.GetProductUnitInfoByID(productUnitID);
        }

        /// <summary>
        /// 获取指定单据类型（冲红）信息
        /// </summary>
        /// <param name="docTypeID">单据类型ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetDocTypeTableInfoByID(int docTypeID)
        {
            return DocTypeTableDAL.GetDocTypeTableInfoByID(docTypeID);
        }

        /// <summary>
        /// 获取指定城市信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetCityInfoByID(int id)
        {
            return CityDAL.GetCityInfoByID(id);
        }

        /// <summary>
        /// 根据银行编号获取指定的银行信息
        /// </summary>
        /// <param name="bankID">银行编号</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMemberBankInfoByID(int bankID)
        {
            return MemberBankDAL.GetMemberBankInfoByID(bankID);
        }

        /// <summary>
        /// 获取指定的仓库信息
        /// </summary>
        /// <param name="wareHouseID">仓库ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetWareHouseInfoByWareHouseID(int wareHouseID)
        {
            return WareHouseDAL.GetWareHouseInfoByWareHouseID(wareHouseID);
        }

        /// <summary>
        /// 获取指定的库位信息
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetDepotSeatInfoByID(int id)
        {
            return DepotSeatDAL.GetDepotSeatInfoByID(id);
        }

        /// <summary>
        /// 获取指定产品名称的行数
        /// </summary>
        /// <param name="productColorName">产品颜色名称</param>
        /// <returns>返回指定产品名称的行数</returns>
        public static int GetProductColorCountByName(string productColorName)
        {
            return ProductColorDAL.GetProductColorCountByName(productColorName);
        }

        /// <summary>
        /// 获取指定产品适用人群记录行数
        /// </summary>
        /// <param name="productSexTypeName">产品适用人群名称</param>
        /// <returns>返回获取指定产品适用人群记录行数</returns>
        public static int GetProductSexTypeCountByName(string productSexTypeName)
        {
            return ProductSexTypeDAL.GetProductSexTypeCountByName(productSexTypeName);
        }

        /// <summary>
        /// 获取指定产品尺寸的行数
        /// </summary>
        /// <param name="productSizeName">产品尺寸名称</param>
        /// <returns>返回获取指定产品尺寸的行数</returns>
        public static int GetProductSizeCountByName(string productSizeName)
        {
            return ProductSizeDAL.GetProductSizeCountByName(productSizeName);
        }

        /// <summary>
        /// 获取指定产品规格的行数
        /// </summary>
        /// <param name="productSpecName">产品规格名称</param>
        /// <returns>返回获取指定产品规格的行数</returns>
        public static int GetProductSpecCountByName(string productSpecName)
        {
            return ProductSpecDAL.GetProductSpecCountByName(productSpecName);
        }

        /// <summary>
        /// 获取指定产品状态的行数
        /// </summary>
        /// <param name="productStatusName">产品状态名称</param>
        /// <returns>返回获取指定产品状态的行数</returns>
        public static int GetProductStatusCountByName(string productStatusName)
        {
            return ProductStatusDAL.GetProductStatusCountByName(productStatusName);
        }

        /// <summary>
        /// 获取指定产品型号行数
        /// </summary>
        /// <param name="productTypeName">产品型号名称</param>
        /// <returns>返回获取指定产品型号行数</returns>
        public static int GetProductTypeCountByName(string productTypeName)
        {
            return ProductTypeDAL.GetProductTypeCountByName(productTypeName);
        }

        /// <summary>
        /// 获取指定的产品单位行数
        /// </summary>
        /// <param name="productUnitName">产品单位名称</param>
        /// <returns>返回获取指定的产品单位行数</returns>
        public static int GetProductUnitCountByName(string productUnitName)
        {
            return ProductUnitDAL.GetProductUnitCountByName(productUnitName);
        }

        /// <summary>
        /// 获取指定的单据类型行数
        /// </summary>
        /// <param name="docTypeName">单据类型名称</param>
        /// <returns>返回获取指定的单据类型行数</returns>
        public static int GetDocTypeTableCountByName(string docTypeName)
        {
            return DocTypeTableDAL.GetDocTypeTableCountByName(docTypeName);
        }

        /// <summary>
        /// 通过省份获取省份行数
        /// </summary>
        /// <param name="country">国家</param>
        /// <param name="province">省份</param>
        /// <returns>返回通过省份获取城市的行数</returns>
        public static int GetCityProvinceCountByCountryProvince(string country, string province)
        {
            return CityDAL.GetCityProvinceCountByCountryProvince(country, province);
        }

        /// <summary>
        /// 通过国家省份城市获取行数
        /// </summary>
        /// <param name="country">国家</param>
        /// <param name="province">省份</param>
        /// <param name="city">城市</param>
        /// <returns>返回通过省份城市获取行数</returns>
        public static int GetCityCityCountByCountryProvinceCity(string country, string province, string city, string xian)
        {
            return CityDAL.GetCityCityCountByCountryProvinceCity(country, province, city, xian);
        }

        /// <summary>
        /// 获取指定的会员使用银行行数
        /// </summary>
        /// <param name="memberBank">会员使用银行模型</param>
        /// <returns>返回获取指定的会员使用银行行数</returns>
        public static int GetMemberBankCountByNameCountryCode(MemberBankModel memberBank)
        {
            return MemberBankDAL.GetMemberBankCountByNameCountryCode(memberBank);
        }

        /// <summary>
        /// 获取会员订单底线金额行数
        /// </summary>
        /// <returns>返回获取会员订单底线金额行数</returns>
        public static int GetMemOrderLineCount()
        {
            return MemOrderLineDAL.GetMemOrderLineCount();
        }

        /// <summary>
        /// 通过仓库名称获取仓库名称的行数
        /// </summary>
        /// <param name="wareHouseName">仓库名称</param>
        public static int GetWareHouseNameCountByWareHouseName(string wareHouseName)
        {
            return WareHouseDAL.GetWareHouseNameCountByWareHouseName(wareHouseName);
        }

        /// <summary>
        /// 获取指定产品名称的行数
        /// </summary>
        /// <param name="productColorID">产品颜色ID</param>
        /// <param name="productColorName">产品颜色名称</param>
        /// <returns>返回指定产品名称的行数</returns>
        public static int GetProductColorCountByIDName(int productColorID, string productColorName)
        {
            return ProductColorDAL.GetProductColorCountByIDName(productColorID, productColorName);
        }

        /// <summary>
        /// 获取指定产品适用人群记录行数
        /// </summary>
        /// <param name="productSexTypeID">产品适用人群ID</param>
        /// <param name="productSexTypeName">产品适用人群名称</param>
        /// <returns>返回获取指定产品适用人群记录行数</returns>
        public static int GetProductSexTypeCountByIDName(int productSexTypeID, string productSexTypeName)
        {
            return ProductSexTypeDAL.GetProductSexTypeCountByIDName(productSexTypeID, productSexTypeName);
        }

        /// <summary>
        /// 获取指定产品尺寸的行数
        /// </summary>
        /// <param name="productSizeID">产品尺寸ID</param>
        /// <param name="productSizeName">产品尺寸名称</param>
        /// <returns>返回获取指定产品尺寸的行数</returns>
        public static int GetProductSizeCountByIDName(int productSizeID, string productSizeName)
        {
            return ProductSizeDAL.GetProductSizeCountByIDName(productSizeID, productSizeName);
        }

        /// <summary>
        /// 获取指定产品规格的行数
        /// </summary>
        /// <param name="productSpecID">产品规格ID</param>
        /// <param name="productSpecName">产品规格名称</param>
        /// <returns>返回获取指定产品规格的行数</returns>
        public static int GetProductSpecCountByIDName(int productSpecID, string productSpecName)
        {
            return ProductSpecDAL.GetProductSpecCountByIDName(productSpecID, productSpecName);
        }

        /// <summary>
        /// 获取指定产品状态的行数
        /// </summary>
        /// <param name="productStatusID">产品状态ID</param>
        /// <param name="productStatusName">产品状态名称</param>
        /// <returns>返回获取指定产品状态的行数</returns>
        public static int GetProductStatusCountByIDName(int productStatusID, string productStatusName)
        {
            return ProductStatusDAL.GetProductStatusCountByIDName(productStatusID, productStatusName);
        }

        /// <summary>
        /// 获取指定产品型号行数
        /// </summary>
        /// <param name="productTypeID">产品型号ID</param>
        /// <param name="productTypeName">产品型号名称</param>
        /// <returns>返回获取指定产品型号行数</returns>
        public static int GetProductTypeCountByIDName(int productTypeID, string productTypeName)
        {
            return ProductTypeDAL.GetProductTypeCountByIDName(productTypeID, productTypeName);
        }

        /// <summary>
        /// 获取指定的产品单位行数
        /// </summary>
        /// <param name="productUnitID">产品单位ID</param>
        /// <param name="productUnitName">产品单位名称</param>
        /// <returns>返回获取指定的产品单位行数</returns>
        public static int GetProductUnitCountByIDName(int productUnitID, string productUnitName)
        {
            return ProductUnitDAL.GetProductUnitCountByIDName(productUnitID, productUnitName);
        }

        /// <summary>
        /// 获取指定的单据类型行数
        /// </summary>
        /// <param name="docTypeID">单据类型ID</param>
        /// <param name="docTypeName">单据类型名称</param>
        /// <returns>返回获取指定的单据类型行数</returns>
        public static int GetDocTypeTableCountByIDName(int docTypeID, string docTypeName)
        {
            return DocTypeTableDAL.GetDocTypeTableCountByIDName(docTypeID, docTypeName);
        }

        /// <summary>
        /// Judge the DocTypeID whether has operation by DocTypeID
        /// </summary>
        /// <param name="docTypeID">DocTypeID</param>
        /// <returns>Return counts</returns>
        public static int DocTypeIDWhetherHasOperation(int docTypeID)
        {
            return DocTypeTableDAL.DocTypeIDWhetherHasOperation(docTypeID);
        }

        /// <summary>
        /// Judge the DocTypeID whether exist by DocTypeID before delete or update
        /// </summary>
        /// <param name="docTypeID">DocTypeID</param>
        /// <returns>Return counts</returns>
        public static int DocTypeIDIsExist(int docTypeID)
        {
            return DocTypeTableDAL.DocTypeIDIsExist(docTypeID);
        }

        /// <summary>
        /// Judge the CityID whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>Return the counts of the city by Id</returns>
        public static int CityIdIsExist(int Id)
        {
            return CityDAL.CityIdIsExist(Id);
        }

        /// <summary>
        /// Judge the CityId whether has operation by Id before delete
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>Return the counts of the city by Id</returns>
        public static int CityIdWhetherHasOperation(int Id)
        {
            return CityDAL.CityIdWhetherHasOperation(Id);
        }

        /// <summary>
        /// Judge the ProductUnitId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>Return the counts of the ProductUnitId by Id</returns>
        public static int ProductUnitIdIsExist(int Id)
        {
            return ProductUnitDAL.ProductUnitIdIsExist(Id);
        }

        /// <summary>
        /// Judge the ProductUnitId whether has operation by Id before delete
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>Return the counts of the ProductUnitId by Id</returns>
        public static int ProductUnitIdWhetherHasOperation(int Id)
        {
            return ProductUnitDAL.ProductUnitIdWhetherHasOperation(Id);
        }

        /// <summary>
        /// Juage the ProductColorID whether has operation before delete
        /// </summary>
        /// <param name="productColorID">ProductColorID</param>
        /// <returns>Return counts</returns>
        public static int ProductColorIDWhetherHasOperation(int productColorID)
        {
            return ProductColorDAL.ProductColorIDWhetherHasOperation(productColorID);
        }

        /// <summary>
        /// Judge the ProductColorID whether exist by ProductColorID before delete or update
        /// </summary>
        /// <param name="productColorID">ProductColorID</param>
        /// <returns>Return counts</returns>
        public static int ProductColorIDIsExist(int productColorID)
        {
            return ProductColorDAL.ProductColorIDIsExist(productColorID);
        }

        /// <summary>
        /// Judge the ProductSexTypeId whether has operation before delete by Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductSexType by Id</returns>
        public static int ProductSexTypeIdWhetherHasOperation(int Id)
        {
            return ProductSexTypeDAL.ProductSexTypeIdWhetherHasOperation(Id);
        }

        /// <summary>
        /// Judge the ProductSexTypeId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return counts</returns>
        public static int ProductSexTypeIdIsExist(int Id)
        {
            return ProductSexTypeDAL.ProductSexTypeIdIsExist(Id);
        }

        /// <summary>
        /// Juage the ProductSizeId whether has operation before delete by Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductSize by Id</returns>
        public static int ProductSizeIdWhetherHasOperation(int Id)
        {
            return ProductSizeDAL.ProductSizeIdWhetherHasOperation(Id);
        }

        /// <summary>
        /// Judge the ProductSizeId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductSize by Id</returns>
        public static int ProductSizeIdIsExist(int Id)
        {
            return ProductSizeDAL.ProductSizeIdIsExist(Id);
        }

        /// <summary>
        /// Juage the MemberBankId whether has operation before delete by Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the MemberBankId by Id</returns>
        public static int MemberBankIdWhetherHasOperation(int Id)
        {
            return MemberBankDAL.MemberBankIdWhetherHasOperation(Id);
        }

        /// <summary>
        /// Judge the MemberBankId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the MemberBankId by Id</returns>
        public static int MemberBankIdIsExist(int Id)
        {
            return MemberBankDAL.MemberBankIdIsExist(Id);
        }

        /// <summary>
        /// Judge the ProductStatusId whether has operation before delete by Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductStatus by Id</returns>
        public static int ProductStatusIdWhetherHasOperation(int Id)
        {
            return ProductStatusDAL.ProductStatusIdWhetherHasOperation(Id);
        }

        /// <summary>
        /// Judge the ProductStatusId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductStatus by Id</returns>
        public static int ProductStatusIdIsExist(int Id)
        {
            return ProductStatusDAL.ProductStatusIdIsExist(Id);
        }

        /// <summary>
        /// Judge the ProductSpecId whether has operation before delete by Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductSpec by Id</returns>
        public static int ProductSpecIdWhetherHasOperation(int Id)
        {
            return ProductSpecDAL.ProductSpecIdWhetherHasOperation(Id);
        }

        /// <summary>
        /// Judge the ProductSpecId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductSpec by Id</returns>
        public static int ProductSpecIdIsExist(int Id)
        {
            return ProductSpecDAL.ProductSpecIdIsExist(Id);
        }

        /// <summary>
        /// Judge the ProductTypeId whether has operation before delete by Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductType by Id</returns>
        public static int ProductTypeIdWhetherHasOperation(int Id)
        {
            return ProductTypeDAL.ProductTypeIdWhetherHasOperation(Id);
        }

        /// <summary>
        /// Judge the ProductTypeId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the ProductType by Id</returns>
        public static int ProductTypeIdIsExist(int Id)
        {
            return ProductTypeDAL.ProductTypeIdIsExist(Id);
        }

        /// <summary>
        /// Judge the WareHouseId whether has operation by Id before delete
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the WareHouse by Id</returns>
        public static int WareHouseIdWhetherHasOperation(int Id)
        {
            return WareHouseDAL.WareHouseIdWhetherHasOperation(Id);
        }

        /// <summary>
        /// Judge the WareHouseId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the WareHouse by Id</returns>
        public static int WareHouseIdIsExist(int Id)
        {
            return WareHouseDAL.WareHouseIdIsExist(Id);
        }

        /// <summary>
        /// Judge the DepotSeatId whether has operation by Id before delete
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the DepotSeat by Id</returns>
        public static int DepotSeatIdWhetherHasOperation(int Id)
        {
            return DepotSeatDAL.DepotSeatIdWhetherHasOperation(Id);
        }

        /// <summary>
        /// Judge the DepotSeatId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the DepotSeat by Id</returns>
        public static int DepotSeatIdIsExist(int Id)
        {
            return DepotSeatDAL.DepotSeatIdIsExist(Id);
        }

        /// <summary>
        /// 通过ID,国家，省份获取行数
        /// </summary>
        /// <param name="city">城市模型</param>
        /// <returns>返回通过ID,国家，省份获取行数</returns>
        public static int GetCityProvinceCountByIDCountryProvince(CityModel city)
        {
            return CityDAL.GetCityProvinceCountByIDCountryProvince(city);
        }

        /// <summary>
        /// 通过ID,国家，省份,城市获取行数
        /// </summary>
        /// <param name="city">城市模型</param>
        /// <returns>返回通过ID,国家，省份,城市获取行数</returns>
        public static int GetCityCityCountByIDCountryProvinceCity(CityModel city)
        {
            return CityDAL.GetCityCityCountByIDCountryProvinceCity(city);
        }

        /// <summary>
        /// 通过仓库ID和仓库名称获取仓库名称的行数
        /// </summary>
        /// <param name="depotSeatModel">仓库模型</param>
        /// <returns>返回通过仓库ID和仓库名称获取仓库名称的行数</returns>
        public static int GetWareHouseNameCountByWareHouseIDName(WareHouseModel wareHouseModel)
        {
            return WareHouseDAL.GetWareHouseNameCountByWareHouseIDName(wareHouseModel);
        }

        /// <summary>
        /// 获取指定的会员使用银行行数
        /// </summary>
        /// <param name="memberBank">会员使用银行模型</param>
        /// <returns>返回获取指定的会员使用银行行数</returns>
        public static int GetMemberBankCountByAll(MemberBankModel memberBank)
        {
            return MemberBankDAL.GetMemberBankCountByAll(memberBank);
        }

        /// <summary>
        /// 获取指定的库位名称的行数
        /// </summary>
        /// <param name="depotSeatModel">库位模型</param>
        /// <returns>返回指定的库位名称的行数</returns>
        public static int GetSeatNameCountByWareHouseIDSeatName(DepotSeatModel depotSeatModel)
        {
            return DepotSeatDAL.GetSeatNameCountByWareHouseIDSeatName(depotSeatModel);
        }

        /// <summary>
        /// 获取指定的库位名称的行数
        /// </summary>
        /// <param name="depotSeatModel">库位模型</param>
        /// <returns>返回指定的库位名称的行数</returns>
        public static int GetSeatNameCountByIDWareHouseIDSeatName(DepotSeatModel depotSeatModel)
        {
            return DepotSeatDAL.GetSeatNameCountByIDWareHouseIDSeatName(depotSeatModel);
        }

        /// <summary>
        /// 获取指定国家的会员使用银行信息
        /// </summary>
        /// <param name="countryCode">银行所属国家</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMemberBankInfoByCountryCode(int countryCode)
        {
            return MemberBankDAL.GetMemberBankInfoByCountryCode(countryCode);
        }

        /// <summary>
        /// 通过联合查询获取银行信息
        /// </summary>
        /// <param name="languageID">语言ID</param>
        /// <returns>返回DataTatble对象</returns>
        public static DataTable GetMoreMemberBankInfoByLanguageID(int languageID)
        {
            return MemberBankDAL.GetMoreMemberBankInfoByLanguageID(languageID);
        }

        /// <summary>
        /// 获取语言对应的ID
        /// </summary>
        /// <param name="name">语言名称</param>
        /// <returns>返回语言所对应的ID</returns>
        public static int GetLanguageIDByName(string name)
        {
            return LanguageDAL.GetLanguageIDByName(name);
        }

        /// <summary>
        /// 获取会员订单底线金额
        /// </summary>
        /// <returns>返回会员订单底线金额</returns>
        public static double GetMemOrderLineOrderBaseLine()
        {
            return MemOrderLineDAL.GetMemOrderLineOrderBaseLine();
        }

        /// <summary>
        /// 获取支付方式
        /// </summary>
        /// <returns>返回支付方式</returns>
        public static DataTable GetPayment(int isStore)
        {
            return MemOrderLineDAL.GetPayment(isStore);
        }

        /// <summary>
        /// Out to excel of the data of DocTypeTable
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_DocTypeTable()
        {
            return DocTypeTableDAL.OutToExcel_DocTypeTable();
        }

        /// <summary>
        /// Out to excel of the all data of MemberBank
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_MemberBank()
        {
            return MemberBankDAL.OutToExcel_MemberBank();
        }

        /// <summary>
        /// Out to excel the all data of ProductColor
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_ProductColor()
        {
            return ProductColorDAL.OutToExcel_ProductColor();
        }

        /// <summary>
        /// Out to excel of the all data of ProductSexType
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_ProductSexType()
        {
            return ProductSexTypeDAL.OutToExcel_ProductSexType();
        }

        /// <summary>
        /// Out to excel of the all data of ProductSize
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_ProductSize()
        {
            return ProductSizeDAL.OutToExcel_ProductSize();
        }

        /// <summary>
        /// Out to excel to the all data of ProductSpec
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_ProductSpec()
        {
            return ProductSpecDAL.OutToExcel_ProductSpec();
        }

        /// <summary>
        /// Out to excel the all data of ProductStatus 
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_ProductStatus()
        {
            return ProductStatusDAL.OutToExcel_ProductStatus();
        }

        /// <summary>
        /// Out to excel the all data of ProductType
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_ProductType()
        {
            return ProductTypeDAL.OutToExcel_ProductType();
        }

        /// <summary>
        /// Out to excel the all data of ProductUnit
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_ProductUnit()
        {
            return ProductUnitDAL.OutToExcel_ProductUnit();
        }

        /// <summary>
        /// Out to excel the all data of City
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_City()
        {
            return CityDAL.OutToExcel_City();
        }

        /// <summary>
        /// 获取国家ID,编码和国家名称
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetCountryIDCodeNameOrderByID()
        {
            return CountryDAL.GetCountryIDCodeNameOrderByID();
        }
        /// <summary>
        /// 获得当前仓库的索引
        /// </summary>
        /// <returns></returns>
        public static int GetIDENT_CURRENT()
        {
            return CountryDAL.GetIDENT_CURRENT();
        }
    }
}