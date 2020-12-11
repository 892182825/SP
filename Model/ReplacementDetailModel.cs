using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 创建者：孙延昊
 * 创建时间：2009年8月27日 AM 10：07
 * 文件名：ReplacementDetailModel
 * 功能：店铺换货明细表模型
 * **/
namespace Model
{
    [Serializable]
    /// <summary>
    /// 店铺换货明细表
    /// </summary>
    public class ReplacementDetailModel
    {
        public ReplacementDetailModel()
        { }



        public ReplacementDetailModel(int id)
        {
            this.iD = id;
        }


        private int iD;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            get { return iD; }
        }
        private string displaceOrderID;
        /// <summary>
        /// 换货单号
        /// </summary>
        public string DisplaceOrderID
        {
            get { return displaceOrderID; }
            set { displaceOrderID = value; }
        }
        private int productID;
        /// <summary>
        /// 产品标识外键
        /// </summary>
        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }
        private int outQuantity;
        /// <summary>
        /// 换出数量
        /// </summary>
        public int OutQuantity
        {
            get { return outQuantity; }
            set { outQuantity = value; }
        }
        private double price;
        /// <summary>
        /// 产品单价
        /// </summary>
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        private double pV;
        /// <summary>
        /// 产品PV
        /// </summary>
        public double PV
        {
            get { return pV; }
            set { pV = value; }
        }
        private int inQuantity;
        /// <summary>
        /// 换入数量
        /// </summary>
        public int InQuantity
        {
            get { return inQuantity; }
            set { inQuantity = value; }
        }
        /// <summary>
        /// 新增换货明细构造函数
        /// </summary>
        /// <param name="DisplaceOrderID">换货单号</param>
        /// <param name="ProductID">产品编号</param>
        /// <param name="OutQuantity">退数量</param>
        /// <param name="Price">价格</param>
        /// <param name="InQuantity">进数量</param>
        /// 
        public ReplacementDetailModel(string displaceOrderID, int productID, int outQuantity, double price, double pV, int inQuantity)
        {
            this.displaceOrderID = displaceOrderID;
            this.productID = productID;
            this.outQuantity = outQuantity;
            this.price = price;
            this.pV = pV;
            this.inQuantity = inQuantity;
        }
    }
}
