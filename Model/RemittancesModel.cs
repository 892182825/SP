using System;
/// <summary>
/// 实体类RemittancesModel 
///宋俊
///2009-8-27
/// </summary>

namespace Model
{
	[Serializable]
	public class RemittancesModel
	{
        /// <summary>
        /// 店铺汇款
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
        /// 交易石斛积分数量
        /// </summary>
        public decimal InvestJB
        {
            get { return investJB; }
            set { investJB = value; }
        }
        private decimal priceJB;
        /// <summary>
        /// 交易市价
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
        /// 汇款人类型（0：店铺。1：会员。）
        /// </summary>
        public int RemitStatus
        {
            get { return remitStatus; }
            set { remitStatus = value; }
        }
        /// <summary>
        /// 汇款人编号
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
		/// 付款折合标准币种金额
		/// </summary>
		public decimal RemitMoney
		{
			set{ remitMoney=value;}
			get{return remitMoney;}
		}
		/// <summary>
		/// 标准币种代码
		/// </summary>
		public int StandardCurrency
		{
			set{ standardCurrency=value;}
			get{return standardCurrency;}
		}
		/// <summary>
		/// 用途
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
        /// 汇款用途字符串对应
        /// </summary>
        public string toUserStr
        {
            get
            {
                string usert = "";
                switch (this.use)
                {
                    case 0:
                        usert = "会员现金账户";
                        break;
                    case 1:
                        usert = "会员消费账户";
                        break;
                    case 2:
                        usert = "会员复消账户";
                        break;
                    case 10:
                        usert = "店铺订货款";
                        break;
                    case 11:
                        usert = "店铺周转款";
                        break;
                    default:
                        usert = "错误用途";
                        break;
                }
                return usert; 
            }
        }

        /// <summary>
        /// 所有汇款类型
        /// </summary>
        public System.Collections.Hashtable toUserStrs
        {
            get {
                System.Collections.Hashtable htb = new System.Collections.Hashtable();
                htb.Add(REMITTANCES_TYPE.MEMBER_ORDER_MONEY, "会员现金账户");
                htb.Add(REMITTANCES_TYPE.TURNOVER_MONEY, "会员消费账户");
                htb.Add(REMITTANCES_TYPE.INVEST_MONEY, "店铺订货款");
                htb.Add(REMITTANCES_TYPE.YAJIN_MONEY, "店铺周转款");
                htb.Add(REMITTANCES_TYPE.OTHER_MONEY, "其他");
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
		/// 付款期数
		/// </summary>
        public int PayexpectNum
		{
			set{ payexpectNum=value;}
            get { return payexpectNum; }
		}
		/// <summary>
		/// 收款时间
		/// </summary>
		public DateTime ReceivablesDate
		{
			set{ receivablesDate=value;}
			get{return receivablesDate;}
		}
		/// <summary>
        /// 付款方式（0：在线支付。1：普通汇款。2：预收账款。）
		/// </summary>
		public int PayWay
		{
			set{ payway=value;}
			get{return payway;}
		}
		/// <summary>
		/// 经办人
		/// </summary>
		public string Managers
		{
			set{ managers=value;}
			get{return managers;}
		}
		/// <summary>
        /// 付款的确认方式。包括:"传真","核实","电话"
		/// </summary>
		public int ConfirmType
		{
			set{ confirmType=value;}
			get{return confirmType;}
		}
		/// <summary>
		/// 汇出时间
		/// </summary>
		public DateTime RemittancesDate
		{
			set{ remittancesDate=value;}
			get{return remittancesDate;}
		}
		/// <summary>
		/// 汇出银行
		/// </summary>
		public string RemittancesBank
		{
			set{ remittancesBank=value;}
			get{return remittancesBank;}
		}
		/// <summary>
		/// 汇款账号
		/// </summary>
		public string RemittancesAccount
		{
			set{ remittancesAccount=value;}
			get{return remittancesAccount;}
		}
		/// <summary>
		/// 汇入银行
		/// </summary>
		public string ImportBank
		{
			set{ importBank=value;}
			get{return importBank;}
		}
		/// <summary>
		/// 汇入账号
		/// </summary>
		public string ImportNumber
		{
			set{ importNumber=value;}
			get{return importNumber;}
		}
		/// <summary>
		/// 汇款人
		/// </summary>
		public string Sender
		{
			set{ sender=value;}
			get{return sender;}
		}
		/// <summary>
		/// 汇款人身份证
		/// </summary>
		public string SenderID
		{
			set{ senderID=value;}
			get{return senderID;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ remark=value;}
			get{return remark;}
		}
		/// <summary>
		/// 0 是未审核，1是已审核
		/// </summary>
		public bool IsGSQR
		{
            set { isGSQR = value; }
            get { return isGSQR; }
		}
		/// <summary>
		/// 汇出币种
		/// </summary>
		public int RemittancesCurrency
		{
			set{ remittancescurrency=value;}
			get{return remittancescurrency;}
		}
		/// <summary>
		/// 汇出金额
		/// </summary>
		public decimal RemittancesMoney
		{
			set{ remittancesMoney=value;}
			get{return remittancesMoney;}
		}
		/// <summary>
		/// 传真件路径
		/// </summary>
		public string PhotoPath
		{
			set{ photoPath=value;}
			get{return photoPath;}
		}
		/// <summary>
		/// 操作者IP
		/// </summary>
		public string OperateIp
		{
			set{ operateIp=value;}
			get{return operateIp;}
		}
		/// <summary>
		/// 操作者编号
		/// </summary>
		public string OperateNum
		{
			set{ operateNum=value;}
			get{return operateNum;}
		}

        /// <summary>
        /// 是否需要金流
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
    /// 汇款表 - 付款方式（0：在线支付。1：普通汇款。2：预收账款。）
    /// </summary>
    public enum PayWayType { 
        /// <summary>
        /// 在线支付
        /// </summary>
        OnlinePay=0,
        /// <summary>
        /// 普通汇款
        /// </summary>
        MoneyTrans=1,
        /// <summary>
        /// 预收账款
        /// </summary>
        RecAdvance=2,
    }
}

