using System;
/// <summary>
/// 实体类StoreOrderModel 
///宋俊
///2009-8-27
/// </summary>
namespace Model
{
    /// <summary>
    /// 店铺订货信息
    /// </summary>
    [Serializable]
    public class StoreOrderModel  
    {
        /// <summary>
        /// 店铺订货信息
        /// </summary>
        public StoreOrderModel()
        { }

        public StoreOrderModel(int id)
        {
            this.id = id;
        }

        #region Model

        private int id;
        private string storeId;
        private string storeorderId;
        private string outstorageorderid;
        private DateTime orderDatetime;
        private DateTime consignmentDatetime;
        private DateTime forecastarriveDatetime;
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
        private string conveyancemodeId;
        private string conveyancemode;
        private string conveyanceCompanyId;
        private string conveyanceCompany;
        private string isauditing;
        private string ischeckOut;
        private string isgeneoutbill;
        private string isinform;
        private string issent;
        private string isreceived;
        private string description;
        private string auditingPerson;
        private DateTime auditingDate;
        private string auditingexplain;
        private string informconsignmentPerson;
        private string consignmentPerson;
        private string feedBack;
        private string cPCCode;
        private CityModel city = new CityModel();
        private int wareHouseID;
        private string geneoutbillPerson;
        private int orderType;
        private string operateIP;
        private string operateNumber;
        private int roleID;
        private string inceptNumber;
        private int sendWay;

        public string InceptNumber
        {
            get { return inceptNumber; }
            set { inceptNumber = value; }
        }

        public CityModel City
        {
            get { return city; }
            set { city = value; }
        }

        public string CPCCode
        {
            get { return cPCCode; }
            set { cPCCode = value; }
        }

        public int RoleID
        {
            get { return roleID; }
            set { roleID = value; }
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
        /// 订单号
        /// </summary>
        public string StoreorderId
        {
            set { storeorderId = value; }
            get { return storeorderId; }
        }
        /// <summary>
        /// 生成的出库单的单号
        /// </summary>
        public string OutStorageOrderID
        {
            set { outstorageorderid = value; }
            get { return outstorageorderid; }
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
        /// 发货日期
        /// </summary>
        public DateTime ConsignmentDatetime
        {
            set { consignmentDatetime = value; }
            get { return consignmentDatetime; }
        }
        /// <summary>
        /// 预计到达时间
        /// </summary>
        public DateTime ForecastarriveDatetime
        {
            set { forecastarriveDatetime = value; }
            get { return forecastarriveDatetime; }
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
        /// 运输模式
        /// </summary>
        public string ConveyancemodeId
        {
            set { conveyancemodeId = value; }
            get { return conveyancemodeId; }
        }
        /// <summary>
        /// 运输模式
        /// </summary>
        public string ConveyanceMode
        {
            set { conveyancemode = value; }
            get { return conveyancemode; }
        }
        /// <summary>
        /// 物流公司编号
        /// </summary>
        public string ConveyanceCompanyId
        {
            set { conveyanceCompanyId = value; }
            get { return conveyanceCompanyId; }
        }
        /// <summary>
        /// 物流公司
        /// </summary>
        public string ConveyanceCompany
        {
            set { conveyanceCompany = value; }
            get { return conveyanceCompany; }
        }
        /// <summary>
        /// 公司审核标志
        /// </summary>
        public string IsAuditing
        {
            set { isauditing = value; }
            get { return isauditing; }
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
        /// 是否已生成出库单
        /// </summary>
        public string IsGeneOutBill
        {
            set { isgeneoutbill = value; }
            get { return isgeneoutbill; }
        }
        /// <summary>
        /// 是否通知发货
        /// </summary>
        public string IsInform
        {
            set { isinform = value; }
            get { return isinform; }
        }
        /// <summary>
        /// 是否发货
        /// </summary>
        public string IsSent
        {
            set { issent = value; }
            get { return issent; }
        }
        /// <summary>
        /// 是否已收获
        /// </summary>
        public string IsReceived
        {
            set { isreceived = value; }
            get { return isreceived; }
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
        /// 审核人
        /// </summary>
        public string AuditingPerson
        {
            set { auditingPerson = value; }
            get { return auditingPerson; }
        }
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime AuditingDate
        {
            set { auditingDate = value; }
            get { return auditingDate; }
        }
        /// <summary>
        /// 审核意见
        /// </summary>
        public string AuditingExplain
        {
            set { auditingexplain = value; }
            get { return auditingexplain; }
        }
        /// <summary>
        /// 通知发货人
        /// </summary>
        public string InformconsignmentPerson
        {
            set { informconsignmentPerson = value; }
            get { return informconsignmentPerson; }
        }
        /// <summary>
        /// 发货人
        /// </summary>
        public string ConsignmentPerson
        {
            set { consignmentPerson = value; }
            get { return consignmentPerson; }
        }
        /// <summary>
        /// 店铺反馈意见
        /// </summary>
        public string FeedBack
        {
            set { feedBack = value; }
            get { return feedBack; }
        }

        /// <summary>
        /// 发货仓库编号
        /// </summary>
        public int WareHouseID
        {
            set { wareHouseID = value; }
            get { return wareHouseID; }
        }
        /// <summary>
        /// 出库人编号
        /// </summary>
        public string GeneoutbillPerson
        {
            set { geneoutbillPerson = value; }
            get { return geneoutbillPerson; }
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

        public int SendWay
        {
            get { return sendWay; }
            set { sendWay = value; }
        }
        private string kuaididh;
        /// <summary>
        /// 快递单号
        /// </summary>
        public string Kuaididh
        {
            get { return kuaididh; }
            set { kuaididh = value; }
        }

        #endregion Model

    }
}

