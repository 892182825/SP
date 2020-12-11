using System;
/// <summary>
/// ʵ����StoreOrderModel 
///�ο�
///2009-8-27
/// </summary>
namespace Model
{
    /// <summary>
    /// ���̶�����Ϣ
    /// </summary>
    [Serializable]
    public class StoreOrderModel  
    {
        /// <summary>
        /// ���̶�����Ϣ
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
        /// ����ID
        /// </summary>
        public string StoreId
        {
            set { storeId = value; }
            get { return storeId; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string StoreorderId
        {
            set { storeorderId = value; }
            get { return storeorderId; }
        }
        /// <summary>
        /// ���ɵĳ��ⵥ�ĵ���
        /// </summary>
        public string OutStorageOrderID
        {
            set { outstorageorderid = value; }
            get { return outstorageorderid; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public DateTime OrderDatetime
        {
            set { orderDatetime = value; }
            get { return orderDatetime; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public DateTime ConsignmentDatetime
        {
            set { consignmentDatetime = value; }
            get { return consignmentDatetime; }
        }
        /// <summary>
        /// Ԥ�Ƶ���ʱ��
        /// </summary>
        public DateTime ForecastarriveDatetime
        {
            set { forecastarriveDatetime = value; }
            get { return forecastarriveDatetime; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public int ExpectNum
        {
            set { expectNum = value; }
            get { return expectNum; }
        }
        /// <summary>
        /// ������PV
        /// </summary>
        public decimal TotalPv
        {
            set { totalPv = value; }
            get { return totalPv; }
        }
        /// <summary>
        /// �����ܽ��
        /// </summary>
        public decimal TotalMoney
        {
            set { totalMoney = value; }
            get { return totalMoney; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public decimal TotalCommision
        {
            set { totalcommision = value; }
            get { return totalcommision; }
        }
        /// <summary>
        /// �ջ��˵�ַ
        /// </summary>
        public string InceptAddress
        {
            set { inceptAddress = value; }
            get { return inceptAddress; }
        }
        /// <summary>
        /// �ջ�������
        /// </summary>
        public string InceptPerson
        {
            set { inceptPerson = value; }
            get { return inceptPerson; }
        }
        /// <summary>
        /// �ջ��˱��
        /// </summary>
        public string PostalCode
        {
            set { postalCode = value; }
            get { return postalCode; }
        }
        /// <summary>
        /// �ջ��˵绰
        /// </summary>
        public string Telephone
        {
            set { telephone = value; }
            get { return telephone; }
        }
        /// <summary>
        /// �������
        /// </summary>
        public int GoodsQuantity
        {
            set { goodsquantity = value; }
            get { return goodsquantity; }
        }
        /// <summary>
        /// �˷�
        /// </summary>
        public decimal Carriage
        {
            set { carriage = value; }
            get { return carriage; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public decimal Weight
        {
            set { weight = value; }
            get { return weight; }
        }
        /// <summary>
        /// ����ģʽ
        /// </summary>
        public string ConveyancemodeId
        {
            set { conveyancemodeId = value; }
            get { return conveyancemodeId; }
        }
        /// <summary>
        /// ����ģʽ
        /// </summary>
        public string ConveyanceMode
        {
            set { conveyancemode = value; }
            get { return conveyancemode; }
        }
        /// <summary>
        /// ������˾���
        /// </summary>
        public string ConveyanceCompanyId
        {
            set { conveyanceCompanyId = value; }
            get { return conveyanceCompanyId; }
        }
        /// <summary>
        /// ������˾
        /// </summary>
        public string ConveyanceCompany
        {
            set { conveyanceCompany = value; }
            get { return conveyanceCompany; }
        }
        /// <summary>
        /// ��˾��˱�־
        /// </summary>
        public string IsAuditing
        {
            set { isauditing = value; }
            get { return isauditing; }
        }
        /// <summary>
        /// �Ƿ񸶿�
        /// </summary>
        public string IscheckOut
        {
            set { ischeckOut = value; }
            get { return ischeckOut; }
        }
        /// <summary>
        /// �Ƿ������ɳ��ⵥ
        /// </summary>
        public string IsGeneOutBill
        {
            set { isgeneoutbill = value; }
            get { return isgeneoutbill; }
        }
        /// <summary>
        /// �Ƿ�֪ͨ����
        /// </summary>
        public string IsInform
        {
            set { isinform = value; }
            get { return isinform; }
        }
        /// <summary>
        /// �Ƿ񷢻�
        /// </summary>
        public string IsSent
        {
            set { issent = value; }
            get { return issent; }
        }
        /// <summary>
        /// �Ƿ����ջ�
        /// </summary>
        public string IsReceived
        {
            set { isreceived = value; }
            get { return isreceived; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Description
        {
            set { description = value; }
            get { return description; }
        }
        /// <summary>
        /// �����
        /// </summary>
        public string AuditingPerson
        {
            set { auditingPerson = value; }
            get { return auditingPerson; }
        }
        /// <summary>
        /// �������
        /// </summary>
        public DateTime AuditingDate
        {
            set { auditingDate = value; }
            get { return auditingDate; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string AuditingExplain
        {
            set { auditingexplain = value; }
            get { return auditingexplain; }
        }
        /// <summary>
        /// ֪ͨ������
        /// </summary>
        public string InformconsignmentPerson
        {
            set { informconsignmentPerson = value; }
            get { return informconsignmentPerson; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string ConsignmentPerson
        {
            set { consignmentPerson = value; }
            get { return consignmentPerson; }
        }
        /// <summary>
        /// ���̷������
        /// </summary>
        public string FeedBack
        {
            set { feedBack = value; }
            get { return feedBack; }
        }

        /// <summary>
        /// �����ֿ���
        /// </summary>
        public int WareHouseID
        {
            set { wareHouseID = value; }
            get { return wareHouseID; }
        }
        /// <summary>
        /// �����˱��
        /// </summary>
        public string GeneoutbillPerson
        {
            set { geneoutbillPerson = value; }
            get { return geneoutbillPerson; }
        }
        /// <summary>
        /// �������ͣ�0Ϊ����������1Ϊ��ת����
        /// </summary>
        public int OrderType
        {
            set { orderType = value; }
            get { return orderType; }
        }
        /// <summary>
        /// ������IP
        /// </summary>
        public string OperateIP
        {
            set { operateIP = value; }
            get { return operateIP; }
        }
        /// <summary>
        /// �����߱��
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
        /// ��ݵ���
        /// </summary>
        public string Kuaididh
        {
            get { return kuaididh; }
            set { kuaididh = value; }
        }

        #endregion Model

    }
}

