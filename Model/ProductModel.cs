using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * 
 * 创建人：  常艳兵
 * 创建时间：2009年8月27日 AM 10：13
 * 修改人：  汪  华
 * 修改时间：2009-09-04
 * 修改人：  汪  华
 * 修改时间：2009-10-22(删除三个字段及属性)(WareHouseID,ShowOrderID,ProductNumber)
 * 文件名：  ProductModel
 * 功能：产品信息表
 * 
 */
namespace Model
{

    /// <summary>
    /// 产品信息表
    /// </summary>
    public class ProductModel
    {
        public ProductModel() { }
        private int productID;
        /// <summary>
        /// 产品（产品类）编号
        /// </summary>
        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }
        private int pID;
        /// <summary>
        /// 父产品类编号
        /// </summary>
        public int PID
        {
            get { return pID; }
            set { pID = value; }
        }
        private int isFold;
        /// <summary>
        /// 是否是产品类，包括0:产品;1:产品类
        /// </summary>
        public int IsFold
        {
            get { return isFold; }
            set { isFold = value; }
        }
        private string productCode;
        /// <summary>
        /// 产品代码
        /// </summary>
        public string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }
        private string productName;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        private int productTypeID;
        /// <summary>
        /// 产品型号编号
        /// </summary>
        public int ProductTypeID
        {
            get { return productTypeID; }
            set { productTypeID = value; }
        }
        private int productSpecID;
        /// <summary>
        /// 产品规格编号
        /// </summary>
        public int ProductSpecID
        {
            get { return productSpecID; }
            set { productSpecID = value; }
        }
        private int productColorID;
        /// <summary>
        /// 产品颜色编号
        /// </summary>
        public int ProductColorID
        {
            get { return productColorID; }
            set { productColorID = value; }
        }
        private int productSizeID;
        /// <summary>
        /// 产品尺寸编号
        /// </summary>
        public int ProductSizeID
        {
            get { return productSizeID; }
            set { productSizeID = value; }
        }
        private int productSexTypeID;
        /// <summary>
        /// 产品适用人群编号
        /// </summary>
        public int ProductSexTypeID
        {
            get { return productSexTypeID; }
            set { productSexTypeID = value; }
        }
        private int productStatusID;
        /// <summary>
        /// 产品形态编号
        /// </summary>
        public int ProductStatusID
        {
            get { return productStatusID; }
            set { productStatusID = value; }
        }
        
        private int bigProductUnitID;
        /// <summary>
        /// 产品大单位编号
        /// </summary>
        public int BigProductUnitID
        {
            get { return bigProductUnitID; }
            set { bigProductUnitID = value; }
        }
        private int smallProductUnitID;
        /// <summary>
        /// 产品小单位编号
        /// </summary>
        public int SmallProductUnitID
        {
            get { return smallProductUnitID; }
            set { smallProductUnitID = value; }
        }
        private int bigSmallMultiple;
        /// <summary>
        /// 大小单位的折合倍数
        /// </summary>
        public int BigSmallMultiple
        {
            get { return bigSmallMultiple; }
            set { bigSmallMultiple = value; }
        }
        private string productArea;
        /// <summary>
        /// 产品的产地
        /// </summary>
        public string ProductArea
        {
            get { return productArea; }
            set { productArea = value; }
        }
        private decimal costPrice;
        /// <summary>
        /// 成本价
        /// </summary>
        public decimal CostPrice
        {
            get { return costPrice; }
            set { costPrice = value; }
        }
        private decimal commonPrice;
        /// <summary>
        /// 普通价
        /// </summary>
        public decimal CommonPrice
        {
            get { return commonPrice; }
            set { commonPrice = value; }
        }
        private decimal commonPV;
        /// <summary>
        /// 普通价积分
        /// </summary>
        public decimal CommonPV
        {
            get { return commonPV; }
            set { commonPV = value; }
        }
        private decimal preferentialPrice;
        /// <summary>
        /// 优惠价
        /// </summary>
        public decimal PreferentialPrice
        {
            get { return preferentialPrice; }
            set { preferentialPrice = value; }
        }
        private decimal preferentialPV;
        /// <summary>
        /// 优惠价积分
        /// </summary>
        public decimal PreferentialPV
        {
            get { return preferentialPV; }
            set { preferentialPV = value; }
        }
        private int alertnessCount;
        /// <summary>
        /// 小单位数量预警
        /// </summary>
        public int AlertnessCount
        {
            get { return alertnessCount; }
            set { alertnessCount = value; }
        }
        private string description;
        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private decimal weight;
        /// <summary>
        /// 产品重量
        /// </summary>
        public decimal Weight
        {
            get { return weight; }
            set { weight = value; }
        }
       
        private byte[] productImage;//image
        /// <summary>
        /// 产品图片
        /// </summary>
        public byte[] ProductImage
        {
            get { return productImage; }
            set { productImage = value; }
        }
        private string imageType;
        /// <summary>
        /// 产品图片类型
        /// </summary>
        public string ImageType
        {
            get { return imageType; }
            set { imageType = value; }
        }
        private int isCombineProduct;
        /// <summary>
        /// 是否组合产品
        /// </summary>
        public int IsCombineProduct
        {
            get { return isCombineProduct; }
            set { isCombineProduct = value; }
        }
        private int productType;
        /// <summary>
        /// 店铺订货时是使用大单位还是小单位（0为小单位，1为大单位）
        /// </summary>
        public int ProductType
        {
            get { return productType; }
            set { productType = value; }
        }
        private string productTypeName;
        /// <summary>
        /// 产品型号名称
        /// </summary>
        public string ProductTypeName
        {
            get { return productTypeName; }
            set { productTypeName = value; }
        }
        private int currency;
        /// <summary>
        /// 产品单价的币种
        /// </summary>
        public int Currency
        {
            get { return currency; }
            set { currency = value; }
        }
       
        private string countryCode;
        /// <summary>
        /// 国家编码
        /// </summary>
        public string CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; }
        }

        private string operateIP;
        /// <summary>
        /// 录入者IP
        /// </summary>
        public string OperateIP
        {
            get { return operateIP; }
            set { operateIP = value; }
        }
        private string operateNum;
        /// <summary>
        /// 录入者编号
        /// </summary>
        public string OperateNum
        {
            get { return operateNum; }
            set { operateNum = value; }
        }
        private int isSell;
        /// <summary>
        /// 是否可以销售(1表示不销售，0表示销售)
        /// </summary>
        public int IsSell
        {
            get { return isSell; }
            set { isSell = value; }
        }
        private decimal length;
        /// <summary>
        /// 产品长度
        /// </summary>
        public decimal Length
        {
            get { return length; }
            set { length = value; }
        }
        private decimal width;
        /// <summary>
        /// 产品宽度
        /// </summary>
        public decimal Width
        {
            get { return width; }
            set { width = value; }
        }
        private decimal high;
        /// <summary>
        /// 产品高度
        /// </summary>
        public decimal High
        {
            get { return high; }
            set { high = value; }
        }

        /// <summary>
        /// 产品用途
        /// </summary>
        private int yongtu;
        public int Yongtu
        {
            get { return yongtu; }
            set { yongtu = value; }
        }


        private string _Details_TX = string.Empty;
        /// <summary>
        /// 产品的详细描述数据库类型为Text
        /// </summary>
        public string Details_TX
        {
            get { return _Details_TX; }
            set { _Details_TX = value; }
        }
        /// <summary>
        /// 是否只针对团队销售
        /// </summary>
        public int OnlyForGroup_NR
        { get; set; }
        /// <summary>
        /// 安置团队编号,分号符分隔
        /// </summary>
        public string GroupIDS_AZ_TX
        { get; set; }
        /// <summary>
        /// 推荐团队编号,分号符分隔
        /// </summary>
        public string GroupIDS_TJ_TX
        { get; set; }

    }
}
