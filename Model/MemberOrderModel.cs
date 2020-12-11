using System;

/// <summary>
///Author:      WangHua
///FileName:    MemberOrderModel
///FinishDate:  2010-03-04
/// </summary>

namespace Model
{
    [Serializable]
    public class MemberOrderModel 
    {
        /// <summary>
        /// 会员报单
        /// </summary>
        public MemberOrderModel()
        { }
        #region Model
        private int id;
        private string number;
        private string orderId;
        private string storeId;
        private decimal totalMoney;
        private decimal totalPv;
        private decimal carryMoney;
        private int orderExpect;
        private int payExpect;
        private int isagain;
        private DateTime orderDate;
        private string err;
        private string remark;
        private int defrayState;
        private string consignee;
        private string ccpccode;
        private CityModel conCity = new CityModel();
        private string conAddress;
        private string conZipCode;
        private string conTelPhone;
        private string conMobilPhone;
        private string conPost;
        private int defrayType;
        private decimal payMoney;
        private int paycurrency;
        private int standardcurrency;
        private decimal standardcurrencyMoney;
        private string operateIp;
        private string operateNumber;
        private string remittancesId;
        private string electronicaccountId;
        private int orderType;
        private int isreceiVables;
        private decimal paymentMoney;
        private DateTime receivablesDate;
        private string cCPCCode;
        private int sendType;

        private decimal totalMoneyReturned;

        public decimal TotalMoneyReturned1
        {
            get { return totalMoneyReturned; }
            set { totalMoneyReturned = value; }
        }

        private decimal totalPvReturned;

        public decimal TotalPvReturned1
        {
            get { return totalPvReturned; }
            set { totalPvReturned = value; }
        }
        private int orderStatus;

        public int OrderStatus
        {
            get { return orderStatus; }
            set { orderStatus = value; }
        }
        private decimal totalMoneyReturning;

        public decimal TotalMoneyReturning
        {
            get { return totalMoneyReturning; }
            set { totalMoneyReturning = value; }
        }
        private decimal totalPvReturning;

        public decimal TotalPvReturning
        {
            get { return totalPvReturning; }
            set { totalPvReturning = value; }
        }

        private decimal investJB;
        /// <summary>
        /// 投资石斛积分数量
        /// </summary>
        public decimal InvestJB 
        {
            get { return investJB; }
            set { investJB = value; }
        }

        private decimal priceJB;
        /// <summary>
        /// 石斛积分时价
        /// </summary>
        public decimal PriceJB 
        {
            get { return priceJB; }
            set { priceJB = value; }
        }


        public int SendType
        {
            get { return sendType; }
            set { sendType = value; }
        }

        /// <summary>
        /// 已退货总金额
        /// </summary>
        public double TotalMoneyReturned
        { get; set; }

        /// <summary>
        /// 已退货总积分
        /// </summary>
        public double TotalPvReturned
        { get; set; }

        public string CCPCCode
        {
            get { return cCPCCode; }
            set { cCPCCode = value; }
        }


        public CityModel ConCity
        {
            get { return conCity; }
            set { conCity = value; }
        }

        public string Ccpccode
        {
            get { return ccpccode; }
            set { ccpccode = value; }
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
        /// 订单号
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
        /// 报单金额
        /// </summary>
        public decimal TotalMoney
        {
            set { totalMoney = value; }
            get { return totalMoney; }
        }
        /// <summary>
        /// 报单PV
        /// </summary>
        public decimal TotalPv
        {
            set { totalPv = value; }
            get { return totalPv; }
        }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal CarryMoney
        {
            set { carryMoney = value; }
            get { return carryMoney; }
        }
        /// <summary>
        /// 报单期数
        /// </summary>
        public int OrderExpect
        {
            set { orderExpect = value; }
            get { return orderExpect; }
        }
        /// <summary>
        /// 付款期数
        /// </summary>
        public int PayExpect
        {
            set { payExpect = value; }
            get { return payExpect; }
        }
        /// <summary>
        /// 是否重复消费 1,0  是否是团购消费5,6
        /// </summary>
        public int IsAgain
        {
            set { isagain = value; }
            get { return isagain; }
        }
        /// <summary>
        /// 订单时间
        /// </summary>
        public DateTime OrderDate
        {
            set { orderDate = value; }
            get { return orderDate; }
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Err
        {
            set { err = value; }
            get { return err; }
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
        /// 支付状态
        /// </summary>
        public int DefrayState
        {
            set { defrayState = value; }
            get { return defrayState; }
        }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee
        {
            set { consignee = value; }
            get { return consignee; }
        }

        /// <summary>
        /// 收货人详细地址
        /// </summary>
        public string ConAddress
        {
            set { conAddress = value; }
            get { return conAddress; }
        }
        /// <summary>
        /// 收货人所在地址邮编
        /// </summary>
        public string ConZipCode
        {
            set { conZipCode = value; }
            get { return conZipCode; }
        }
        /// <summary>
        /// 收货人固定电话
        /// </summary>
        public string ConTelPhone
        {
            set { conTelPhone = value; }
            get { return conTelPhone; }
        }
        /// <summary>
        /// 收货人移动电话
        /// </summary>
        public string ConMobilPhone
        {
            set { conMobilPhone = value; }
            get { return conMobilPhone; }
        }
        /// <summary>
        /// 收货人邮箱
        /// </summary>
        public string ConPost
        {
            set { conPost = value; }
            get { return conPost; }
        }
        /// <summary>
        /// 支付类型，1是现金，2电子转帐，3网上支付，4是圆角分
        /// </summary>
        public int DefrayType
        {
            set { defrayType = value; }
            get { return defrayType; }
        }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal PayMoney
        {
            set { payMoney = value; }
            get { return payMoney; }
        }
        /// <summary>
        /// 支付币种代码
        /// </summary>
        public int PayCurrency
        {
            set { paycurrency = value; }
            get { return paycurrency; }
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
        /// 操作者IP
        /// </summary>
        public string OperateIp
        {
            set { operateIp = value; }
            get { return operateIp; }
        }
        /// <summary>
        /// 操作者编号
        /// </summary>
        public string OperateNumber
        {
            set { operateNumber = value; }
            get { return operateNumber; }
        }
        /// <summary>
        /// 电子钱包支付时给店铺打款的汇款号
        /// </summary>
        public string RemittancesId
        {
            set { remittancesId = value; }
            get { return remittancesId; }
        }
        /// <summary>
        /// 所用电子账户者编号
        /// </summary>
        public string ElectronicaccountId
        {
            set { electronicaccountId = value; }
            get { return electronicaccountId; }
        }
        /// <summary>
        /// 0店铺注册报单，1店铺复销，2会员网上购物，3自由注册，4特殊会员注册，5特殊会员报单,6手机注册，7手机报单
        /// </summary>
        public int OrderType
        {
            set { orderType = value; }
            get { return orderType; }
        }
        /// <summary>
        /// 是否收款
        /// </summary>
        public int IsreceiVables
        {
            set { isreceiVables = value; }
            get { return isreceiVables; }
        }
        /// <summary>
        /// 收款金额
        /// </summary>
        public decimal PaymentMoney
        {
            set { paymentMoney = value; }
            get { return paymentMoney; }
        }
        /// <summary>
        /// 收款时间
        /// </summary>
        public DateTime ReceivablesDate
        {
            set { receivablesDate = value; }
            get { return receivablesDate; }
        }

        private decimal enoughProductMoney;

        /// <summary>
        /// 有货金额
        /// </summary>
        public decimal EnoughProductMoney
        {
            get { return enoughProductMoney; }
            set { enoughProductMoney = value; }
        }

        private decimal lackProductMoney;

        /// <summary>
        /// 无货金额
        /// </summary>
        public decimal LackProductMoney
        {
            get { return lackProductMoney; }
            set { lackProductMoney = value; }
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