using System;
/// <summary>
/// ʵ����RemittancesModel 
///�ο�
///2009-8-27
/// </summary>

namespace Model
{
	[Serializable]
	public class RemittancesModel
	{
        /// <summary>
        /// ���̻��
        /// </summary>
		public RemittancesModel() {}

		#region Model

		private int id;
        private string remittancesid;
        private string remitNumber;
        private int remitStatus;
		private decimal remitMoney;
		private int standardCurrency;
		private int use;
        private int expectNum;
        private int payexpectNum;
        private DateTime remittancesDate;
		private DateTime receivablesDate;
		private int payway;
        private string numericalOrder;
		private string managers;
		private int confirmType;
		private string remittancesBank;
		private string remittancesAccount;
		private string importBank;
		private string importNumber;
		private string sender;
		private string senderID;
		private string remark;
        private bool isGSQR;
		private int remittancescurrency;
		private decimal remittancesMoney;
		private string photoPath;
		private string operateIp;
		private string operateNum;
        private int bankID;
        private int isJL;
        private string Name;
        private decimal investJB;
        /// <summary>
        /// ����ʯ����������
        /// </summary>
        public decimal InvestJB
        {
            get { return investJB; }
            set { investJB = value; }
        }
        private decimal priceJB;
        /// <summary>
        /// �����м�
        /// </summary>
        public decimal PriceJB
        {
            get { return priceJB; }
            set { priceJB = value; }
        }

        public string NumericalOrder
        {
            get { return numericalOrder; }
            set { numericalOrder = value; }
        }

        public int ExpectNum
        {
            get { return expectNum; }
            set { expectNum = value; }
        }
        /// <summary>
        /// ��������ͣ�0�����̡�1����Ա����
        /// </summary>
        public int RemitStatus
        {
            get { return remitStatus; }
            set { remitStatus = value; }
        }
        /// <summary>
        /// ����˱��
        /// </summary>
        public string RemitNumber
        {
            get { return remitNumber; }
            set { remitNumber = value; }
        }

        public int BankID
        {
            get { return bankID; }
            set { bankID = value; }
        }

        public string Remittancesid
        {
            get { return remittancesid; }
            set { remittancesid = value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ id=value;}
			get{return id;}
		}
		/// <summary>
		/// �����ۺϱ�׼���ֽ��
		/// </summary>
		public decimal RemitMoney
		{
			set{ remitMoney=value;}
			get{return remitMoney;}
		}
		/// <summary>
		/// ��׼���ִ���
		/// </summary>
		public int StandardCurrency
		{
			set{ standardCurrency=value;}
			get{return standardCurrency;}
		}
		/// <summary>
		/// ��;
		/// </summary>
		public int Use
		{
			set{
                switch (value)
                {
                    case 0:
                        this.use = 0;
                        break;
                    case 1:
                        use = 1;
                        break;
                    case 2:
                        use = 2;
                        break;
                    case 10:
                        use = 10;
                        break;
                    case 11:
                        use = 11;
                        break;
                    default:
                        use = -1;
                        break;
                }
            }
			get{
                return use;
            }
		}

        /// <summary>
        /// �����;�ַ�����Ӧ
        /// </summary>
        public string toUserStr
        {
            get
            {
                string usert = "";
                switch (this.use)
                {
                    case 0:
                        usert = "��Ա�ֽ��˻�";
                        break;
                    case 1:
                        usert = "��Ա�����˻�";
                        break;
                    case 2:
                        usert = "��Ա�����˻�";
                        break;
                    case 10:
                        usert = "���̶�����";
                        break;
                    case 11:
                        usert = "������ת��";
                        break;
                    default:
                        usert = "������;";
                        break;
                }
                return usert; 
            }
        }

        /// <summary>
        /// ���л������
        /// </summary>
        public System.Collections.Hashtable toUserStrs
        {
            get {
                System.Collections.Hashtable htb = new System.Collections.Hashtable();
                htb.Add(REMITTANCES_TYPE.MEMBER_ORDER_MONEY, "��Ա�ֽ��˻�");
                htb.Add(REMITTANCES_TYPE.TURNOVER_MONEY, "��Ա�����˻�");
                htb.Add(REMITTANCES_TYPE.INVEST_MONEY, "���̶�����");
                htb.Add(REMITTANCES_TYPE.YAJIN_MONEY, "������ת��");
                htb.Add(REMITTANCES_TYPE.OTHER_MONEY, "����");
                return htb;
            }
        }

        public enum REMITTANCES_TYPE
        { 
            MEMBER_ORDER_MONEY=1,
            TURNOVER_MONEY=2,
            INVEST_MONEY=3,
            YAJIN_MONEY=4,
            OTHER_MONEY=5           
        }

		/// <summary>
		/// ��������
		/// </summary>
        public int PayexpectNum
		{
			set{ payexpectNum=value;}
            get { return payexpectNum; }
		}
		/// <summary>
		/// �տ�ʱ��
		/// </summary>
		public DateTime ReceivablesDate
		{
			set{ receivablesDate=value;}
			get{return receivablesDate;}
		}
		/// <summary>
        /// ���ʽ��0������֧����1����ͨ��2��Ԥ���˿��
		/// </summary>
		public int PayWay
		{
			set{ payway=value;}
			get{return payway;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string Managers
		{
			set{ managers=value;}
			get{return managers;}
		}
		/// <summary>
        /// �����ȷ�Ϸ�ʽ������:"����","��ʵ","�绰"
		/// </summary>
		public int ConfirmType
		{
			set{ confirmType=value;}
			get{return confirmType;}
		}
		/// <summary>
		/// ���ʱ��
		/// </summary>
		public DateTime RemittancesDate
		{
			set{ remittancesDate=value;}
			get{return remittancesDate;}
		}
		/// <summary>
		/// �������
		/// </summary>
		public string RemittancesBank
		{
			set{ remittancesBank=value;}
			get{return remittancesBank;}
		}
		/// <summary>
		/// ����˺�
		/// </summary>
		public string RemittancesAccount
		{
			set{ remittancesAccount=value;}
			get{return remittancesAccount;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string ImportBank
		{
			set{ importBank=value;}
			get{return importBank;}
		}
		/// <summary>
		/// �����˺�
		/// </summary>
		public string ImportNumber
		{
			set{ importNumber=value;}
			get{return importNumber;}
		}
		/// <summary>
		/// �����
		/// </summary>
		public string Sender
		{
			set{ sender=value;}
			get{return sender;}
		}
		/// <summary>
		/// ��������֤
		/// </summary>
		public string SenderID
		{
			set{ senderID=value;}
			get{return senderID;}
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public string Remark
		{
			set{ remark=value;}
			get{return remark;}
		}
		/// <summary>
		/// 0 ��δ��ˣ�1�������
		/// </summary>
		public bool IsGSQR
		{
            set { isGSQR = value; }
            get { return isGSQR; }
		}
		/// <summary>
		/// �������
		/// </summary>
		public int RemittancesCurrency
		{
			set{ remittancescurrency=value;}
			get{return remittancescurrency;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public decimal RemittancesMoney
		{
			set{ remittancesMoney=value;}
			get{return remittancesMoney;}
		}
		/// <summary>
		/// �����·��
		/// </summary>
		public string PhotoPath
		{
			set{ photoPath=value;}
			get{return photoPath;}
		}
		/// <summary>
		/// ������IP
		/// </summary>
		public string OperateIp
		{
			set{ operateIp=value;}
			get{return operateIp;}
		}
		/// <summary>
		/// �����߱��
		/// </summary>
		public string OperateNum
		{
			set{ operateNum=value;}
			get{return operateNum;}
		}

        /// <summary>
        /// �Ƿ���Ҫ����
        /// </summary>
        public int IsJL
        {
            set { isJL = value; }
            get { return isJL; }
        }


        public string name
        {
            set { Name = value; }
            get { return Name; }
        }


        #endregion Model
    }
    /// <summary>
    /// ���� - ���ʽ��0������֧����1����ͨ��2��Ԥ���˿��
    /// </summary>
    public enum PayWayType { 
        /// <summary>
        /// ����֧��
        /// </summary>
        OnlinePay=0,
        /// <summary>
        /// ��ͨ���
        /// </summary>
        MoneyTrans=1,
        /// <summary>
        /// Ԥ���˿�
        /// </summary>
        RecAdvance=2,
    }
}

