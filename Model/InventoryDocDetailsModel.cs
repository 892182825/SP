using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 创建者：孙延昊
 * 创建时间：2009年8月27日 AM 10：07
 * 文件名：InventoryDocDetailsModel
 * 功能：库存单据明细表
 * 修改者：汪华
 * 修改时间：2009-09-01
 * **/
namespace Model
{

    /// <summary>
    /// 库存单据明细表
    /// </summary>
    [Serializable]
    public class InventoryDocDetailsModel
    {

        public InventoryDocDetailsModel() { }
        public InventoryDocDetailsModel(int id)
        {
            this.docDetailsID = id;
        }

        private int docDetailsID;
        /// <summary>
        /// 单据明细流水号
        /// </summary>
        public int DocDetailsID
        {
            get { return docDetailsID; }
        }
        private string docID;
        /// <summary>
        /// 单据编号
        /// </summary>
        public string DocID
        {
            get { return docID; }
            set { docID = value; }
        }
        private int productID;
        /// <summary>
        /// 产品ID 
        /// </summary>
        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }
        private double productQuantity;
        /// <summary>
        /// 产品数量
        /// </summary>
        public double ProductQuantity
        {
            get { return productQuantity; }
            set { productQuantity = value; }
        }
        private double unitPrice;
        /// <summary>
        /// 商品单价
        /// </summary>
        public double UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }
        private string measureUnit;
        /// <summary>
        /// 商品的计量单位
        /// </summary>
        public string MeasureUnit
        {
            get { return measureUnit; }
            set { measureUnit = value; }
        }
        private double pV;
        /// <summary>
        /// 积分
        /// </summary>
        public double PV
        {
            get { return pV; }
            set { pV = value; }
        }
        private int expectNum;
        /// <summary>
        /// 期数
        /// </summary>
        public int ExpectNum
        {
            get { return expectNum; }
            set { expectNum = value; }
        }
         
        private double productTotal;
        /// <summary>
        /// 单品总值
        /// </summary>
        public double ProductTotal
        {
            get { return productTotal; }
            set { productTotal = value; }
        }

        private string batch;
        /// <summary>
        /// 产品批次
        /// </summary>
        public string Batch
        {
            get { return batch; }
            set { batch = value; }
        }


        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime? MakeDate
        {
            get;
            set;
        }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? EffectiveDate
        {
            get;
            set;
        }



        public InventoryDocDetailsModel(
        string DocID,
        int ProductID,
        double ProductQuantity,
        double UnitPrice,
        string vch_measureUnit,
        double PV,
        int Expect,
        double ProductTotal)
        {
            this.DocID = DocID;
            this.ProductID = ProductID;
            this.ProductQuantity = ProductQuantity;
            this.UnitPrice = UnitPrice;
            this.MeasureUnit = MeasureUnit;
            this.PV = PV;
            this.ExpectNum = ExpectNum;
            this.ProductTotal = ProductTotal;
        }

    }
}
