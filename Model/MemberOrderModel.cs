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
        /// ��Ա����
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
        /// Ͷ��ʯ����������
        /// </summary>
        public decimal InvestJB 
        {
            get { return investJB; }
            set { investJB = value; }
        }

        private decimal priceJB;
        /// <summary>
        /// ʯ������ʱ��
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
        /// ���˻��ܽ��
        /// </summary>
        public double TotalMoneyReturned
        { get; set; }

        /// <summary>
        /// ���˻��ܻ���
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
        /// ��Ա���
        /// </summary>
        public string Number
        {
            set { number = value; }
            get { return number; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string OrderId
        {
            set { orderId = value; }
            get { return orderId; }
        }
        /// <summary>
        /// ���̱��
        /// </summary>
        public string StoreId
        {
            set { storeId = value; }
            get { return storeId; }
        }
        /// <summary>
        /// �������
        /// </summary>
        public decimal TotalMoney
        {
            set { totalMoney = value; }
            get { return totalMoney; }
        }
        /// <summary>
        /// ����PV
        /// </summary>
        public decimal TotalPv
        {
            set { totalPv = value; }
            get { return totalPv; }
        }
        /// <summary>
        /// �˷�
        /// </summary>
        public decimal CarryMoney
        {
            set { carryMoney = value; }
            get { return carryMoney; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public int OrderExpect
        {
            set { orderExpect = value; }
            get { return orderExpect; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public int PayExpect
        {
            set { payExpect = value; }
            get { return payExpect; }
        }
        /// <summary>
        /// �Ƿ��ظ����� 1,0  �Ƿ����Ź�����5,6
        /// </summary>
        public int IsAgain
        {
            set { isagain = value; }
            get { return isagain; }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime OrderDate
        {
            set { orderDate = value; }
            get { return orderDate; }
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string Err
        {
            set { err = value; }
            get { return err; }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            set { remark = value; }
            get { return remark; }
        }
        /// <summary>
        /// ֧��״̬
        /// </summary>
        public int DefrayState
        {
            set { defrayState = value; }
            get { return defrayState; }
        }
        /// <summary>
        /// �ջ���
        /// </summary>
        public string Consignee
        {
            set { consignee = value; }
            get { return consignee; }
        }

        /// <summary>
        /// �ջ�����ϸ��ַ
        /// </summary>
        public string ConAddress
        {
            set { conAddress = value; }
            get { return conAddress; }
        }
        /// <summary>
        /// �ջ������ڵ�ַ�ʱ�
        /// </summary>
        public string ConZipCode
        {
            set { conZipCode = value; }
            get { return conZipCode; }
        }
        /// <summary>
        /// �ջ��˹̶��绰
        /// </summary>
        public string ConTelPhone
        {
            set { conTelPhone = value; }
            get { return conTelPhone; }
        }
        /// <summary>
        /// �ջ����ƶ��绰
        /// </summary>
        public string ConMobilPhone
        {
            set { conMobilPhone = value; }
            get { return conMobilPhone; }
        }
        /// <summary>
        /// �ջ�������
        /// </summary>
        public string ConPost
        {
            set { conPost = value; }
            get { return conPost; }
        }
        /// <summary>
        /// ֧�����ͣ�1���ֽ�2����ת�ʣ�3����֧����4��Բ�Ƿ�
        /// </summary>
        public int DefrayType
        {
            set { defrayType = value; }
            get { return defrayType; }
        }
        /// <summary>
        /// ֧�����
        /// </summary>
        public decimal PayMoney
        {
            set { payMoney = value; }
            get { return payMoney; }
        }
        /// <summary>
        /// ֧�����ִ���
        /// </summary>
        public int PayCurrency
        {
            set { paycurrency = value; }
            get { return paycurrency; }
        }
        /// <summary>
        /// ��׼���ִ���
        /// </summary>
        public int StandardCurrency
        {
            set { standardcurrency = value; }
            get { return standardcurrency; }
        }
        /// <summary>
        /// ��׼���ֽ��
        /// </summary>
        public decimal StandardcurrencyMoney
        {
            set { standardcurrencyMoney = value; }
            get { return standardcurrencyMoney; }
        }
        /// <summary>
        /// ������IP
        /// </summary>
        public string OperateIp
        {
            set { operateIp = value; }
            get { return operateIp; }
        }
        /// <summary>
        /// �����߱��
        /// </summary>
        public string OperateNumber
        {
            set { operateNumber = value; }
            get { return operateNumber; }
        }
        /// <summary>
        /// ����Ǯ��֧��ʱ�����̴��Ļ���
        /// </summary>
        public string RemittancesId
        {
            set { remittancesId = value; }
            get { return remittancesId; }
        }
        /// <summary>
        /// ���õ����˻��߱��
        /// </summary>
        public string ElectronicaccountId
        {
            set { electronicaccountId = value; }
            get { return electronicaccountId; }
        }
        /// <summary>
        /// 0����ע�ᱨ����1���̸�����2��Ա���Ϲ��3����ע�ᣬ4�����Աע�ᣬ5�����Ա����,6�ֻ�ע�ᣬ7�ֻ�����
        /// </summary>
        public int OrderType
        {
            set { orderType = value; }
            get { return orderType; }
        }
        /// <summary>
        /// �Ƿ��տ�
        /// </summary>
        public int IsreceiVables
        {
            set { isreceiVables = value; }
            get { return isreceiVables; }
        }
        /// <summary>
        /// �տ���
        /// </summary>
        public decimal PaymentMoney
        {
            set { paymentMoney = value; }
            get { return paymentMoney; }
        }
        /// <summary>
        /// �տ�ʱ��
        /// </summary>
        public DateTime ReceivablesDate
        {
            set { receivablesDate = value; }
            get { return receivablesDate; }
        }

        private decimal enoughProductMoney;

        /// <summary>
        /// �л����
        /// </summary>
        public decimal EnoughProductMoney
        {
            get { return enoughProductMoney; }
            set { enoughProductMoney = value; }
        }

        private decimal lackProductMoney;

        /// <summary>
        /// �޻����
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