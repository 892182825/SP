using System;
/// <summary>
/// 实体类OrderGoodsMedel 
/// </summary>

namespace Model
{
    /// <summary>
    /// 店铺订货信息
    /// </summary>
    [Serializable]
    public class OrderGoodsMedel
    {
        /// <summary>
        /// 店铺订货信息
        /// </summary>
        public OrderGoodsMedel()
        { }

        public OrderGoodsMedel(int id)
        {
            this.id = id;
        }

        #region Model
        private int id;
        private string storeId;
        private string orderGoodsID;
        private string memberOrderID;
        private DateTime orderDatetime;
        private DateTime paymentDatetime;
        private int expectNum;
        private decimal totalPv;
        private decimal totalMoney;
        private decimal totalcommision;
        private string inceptAddress;
        private string inceptPerson;
        private string postalCode;
        private string telephone;
        private int goodsquantity;
        private decimal carriage;
        private decimal weight;
        private string ischeckOut;
        private string description;
        private string cPCCode;
        private CityModel city = new CityModel();
        private int electronicacCountID;
        private int standardcurrency;
        private decimal standardcurrencyMoney;
        private int orderType;
        private string operateIP;
        private string operateNumber;
        private int payType;
        private decimal payMoney;

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderGoodsID
        {
            get { return orderGoodsID; }
            set { orderGoodsID = value; }
        }

        public CityModel City
        {
            get { return city; }
            set { city = value; }
        }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal PayMoney
        {
            get { return payMoney; }
            set { payMoney = value; }
        }

        public string MemberOrderID
        {
            get { return memberOrderID; }
            set { memberOrderID = value; }
        }

        /// <summary>
        /// 支付币种
        /// </summary>
        private int payCurrency;

        public int PayCurrency
        {
            get { return payCurrency; }
            set { payCurrency = value; }
        }
       /// <summary>
       /// 支付类型
       /// </summary>
        public int PayType
        {
            get { return payType; }
            set { payType = value; }
        }

        public string CPCCode
        {
            get { return cPCCode; }
            set { cPCCode = value; }
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
        /// 店铺ID
        /// </summary>
        public string StoreId
        {
            set { storeId = value; }
            get { return storeId; }
        }
       
        /// <summary>
        /// 订货日期
        /// </summary>
        public DateTime OrderDatetime
        {
            set { orderDatetime = value; }
            get { return orderDatetime; }
        }
        /// <summary>
        /// 付款日期
        /// </summary>
        public DateTime PaymentDatetime
        {
            set { paymentDatetime = value; }
            get { return paymentDatetime; }
        }
        /// <summary>
        /// 订货期数
        /// </summary>
        public int ExpectNum
        {
            set { expectNum = value; }
            get { return expectNum; }
        }
        /// <summary>
        /// 订单总PV
        /// </summary>
        public decimal TotalPv
        {
            set { totalPv = value; }
            get { return totalPv; }
        }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal TotalMoney
        {
            set { totalMoney = value; }
            get { return totalMoney; }
        }
        /// <summary>
        /// 手续费
        /// </summary>
        public decimal TotalCommision
        {
            set { totalcommision = value; }
            get { return totalcommision; }
        }
        /// <summary>
        /// 收货人地址
        /// </summary>
        public string InceptAddress
        {
            set { inceptAddress = value; }
            get { return inceptAddress; }
        }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string InceptPerson
        {
            set { inceptPerson = value; }
            get { return inceptPerson; }
        }
        /// <summary>
        /// 收货人编号
        /// </summary>
        public string PostalCode
        {
            set { postalCode = value; }
            get { return postalCode; }
        }
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string Telephone
        {
            set { telephone = value; }
            get { return telephone; }
        }
        /// <summary>
        /// 货物件数
        /// </summary>
        public int GoodsQuantity
        {
            set { goodsquantity = value; }
            get { return goodsquantity; }
        }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal Carriage
        {
            set { carriage = value; }
            get { return carriage; }
        }
        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight
        {
            set { weight = value; }
            get { return weight; }
        }
        
        /// <summary>
        /// 是否付款
        /// </summary>
        public string IscheckOut
        {
            set { ischeckOut = value; }
            get { return ischeckOut; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set { description = value; }
            get { return description; }
        }
        /// <summary>
        /// 电子转账字符编号
        /// </summary>
        public int ElectronicacCountId
        {
            set { electronicacCountID = value; }
            get { return electronicacCountID; }
        }
        /// <summary>
        /// 标准币种代码
        /// </summary>
        public int StandardCurrency
        {
            set { standardcurrency = value; }
            get { return standardcurrency; }
        }
        /// <summary>
        /// 标准币种金额
        /// </summary>
        public decimal StandardcurrencyMoney
        {
            set { standardcurrencyMoney = value; }
            get { return standardcurrencyMoney; }
        }
        /// <summary>
        /// 订单类型（0为正常订货，1为周转货）
        /// </summary>
        public int OrderType
        {
            set { orderType = value; }
            get { return orderType; }
        }
        /// <summary>
        /// 操作者IP
        /// </summary>
        public string OperateIP
        {
            set { operateIP = value; }
            get { return operateIP; }
        }
        /// <summary>
        /// 操作者编号
        /// </summary>
        public string OperateNumber
        {
            set { operateNumber = value; }
            get { return operateNumber; }
        }
        private int sendWay;
        public int SendWay
        {
            get { return sendWay; }
            set { sendWay = value; }
        }

        #endregion Model

    }
}

