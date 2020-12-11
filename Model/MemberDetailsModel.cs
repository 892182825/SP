using System;
namespace Model
{
    /// <summary>
    /// 实体类MemberDetailsModel 
    ///宋俊
    ///2009-8-27
    /// </summary>
    [Serializable]
    public class MemberDetailsModel
    {
        /// <summary>
        /// 会员报单产品明细
        /// </summary>
        public MemberDetailsModel()
        { }
        #region Model
        private int id;
        private string number;
        private string orderId;
        private string storeId;
        private int productId;
        private int quantity;
        private decimal price;
        private decimal pv;
        private int expectNum;
        private int isagain;
        private string remark;
        private DateTime orderDate;
        private int notEnoughProduct;
        private int isInGroupItemCount;
        private int lackTotalNumber;
        private int sendTotalNumber;

        private string piCi;

        public string PiCi
        {
            get { return piCi; }
            set { piCi = value; }
        }
        private int quantityReturned;

        public int QuantityReturned
        {
            get { return quantityReturned; }
            set { quantityReturned = value; }
        }
        private int quantityReturning;

        public int QuantityReturning
        {
            get { return quantityReturning; }
            set { quantityReturning = value; }
        }

        public int SendTotalNumber
        {
            get { return sendTotalNumber; }
            set { sendTotalNumber = value; }
        }

        public int IsInGroupItemCount
        {
            get { return isInGroupItemCount; }
            set { isInGroupItemCount = value; }
        }
        private string isGroupItem;

        public string IsGroupItem
        {
            get { return isGroupItem; }
            set { isGroupItem = value; }
        }
        private string hasGroupItem;

        public string HasGroupItem
        {
            get { return hasGroupItem; }
            set { hasGroupItem = value; }
        }

        public int NotEnoughProduct
        {
            get { return notEnoughProduct; }
            set { notEnoughProduct = value; }
        }

        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        private string productType;

        public string ProductType
        {
            get { return productType; }
            set { productType = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { id = value; }
            get { return id; }
        }
        /// <summary>
        /// 会员编号
        /// </summary>
        public string Number
        {
            set { number = value; }
            get { return number; }
        }
        /// <summary>
        /// 报单号
        /// </summary>
        public string OrderId
        {
            set { orderId = value; }
            get { return orderId; }
        }
        /// <summary>
        /// 店铺编号
        /// </summary>
        public string StoreId
        {
            set { storeId = value; }
            get { return storeId; }
        }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductId
        {
            set { productId = value; }
            get { return productId; }
        }
        /// <summary>
        /// 产品数量
        /// </summary>
        public int Quantity
        {
            set { quantity = value; }
            get { return quantity; }
        }
        /// <summary>
        /// 产品价格
        /// </summary>
        public decimal Price
        {
            set { price = value; }
            get { return price; }
        }
        /// <summary>
        /// 单个产品PV
        /// </summary>
        public decimal Pv
        {
            set { pv = value; }
            get { return pv; }
        }
        /// <summary>
        /// 报单期数
        /// </summary>
        public int ExpectNum
        {
            set { expectNum = value; }
            get { return expectNum; }
        }
        /// <summary>
        /// 是否复销
        /// </summary>
        public int IsAgain
        {
            set { isagain = value; }
            get { return isagain; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { remark = value; }
            get { return remark; }
        }
        /// <summary>
        /// 报单时间
        /// </summary>
        public DateTime OrderDate
        {
            set { orderDate = value; }
            get { return orderDate; }
        }

        public int LackTotalNumber
        {
            set { lackTotalNumber = value; }
            get { return lackTotalNumber; }

        }

        #endregion Model

    }
}

