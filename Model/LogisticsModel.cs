using System;
namespace Model
{
	/// <summary>
	/// ʵ����LogisticsModel
	///songjun
	///2009-08-27
	/// </summary>
	[Serializable]
	public class LogisticsModel
	{
        /// <summary>
        /// Ϊ�˹�˾��Ϣ��
        /// </summary>
		public LogisticsModel()
		{}
		#region Model
		private int id;
		private string number;
		private string logisticscompany ;
		private string principal;
		private string telephone1;
		private string telephone2;
		private string telephone3;
		private string telephone4;
		private string country;
		private string province;
		private string city;
        private string xian;
		private string storeaddress;
		private string postalcode;
		private string licencecode;
		private string bank;
		private string bankcard;
		private DateTime rigisterdate;
		private string remark;
		private string tax;
		private string administer;
		private string logisticsperson;
		private string operateip;
		private string operatenum;
        private string cpccode;
        private string bankcode;

		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ id=value;}
			get{return id;}
		}
		/// <summary>
        /// ������˾���
		/// </summary>
		public string Number
		{
			set{ number=value;}
			get{return number;}
		}

        public string BankCode
        {
            set { bankcode = value; }
            get { return bankcode; }
        }

        public string Cpccode
        {
            set { cpccode = value; }
            get { return cpccode; }
        }

		/// <summary>
        /// ������˾����
		/// </summary>
		public string LogisticsCompany 
		{
			set{ logisticscompany =value;}
			get{return logisticscompany ;}
		}
		/// <summary>
        /// ������
		/// </summary>
		public string Principal
		{
			set{ principal=value;}
			get{return principal;}
		}
		/// <summary>
        /// �绰1
		/// </summary>
		public string Telephone1
		{
			set{ telephone1=value;}
			get{return telephone1;}
		}
		/// <summary>
        /// �绰2
		/// </summary>
		public string Telephone2
		{
			set{ telephone2=value;}
			get{return telephone2;}
		}
		/// <summary>
        /// �绰3
		/// </summary>
		public string Telephone3
		{
			set{ telephone3=value;}
			get{return telephone3;}
		}
		/// <summary>
        /// �绰4
		/// </summary>
		public string Telephone4
		{
			set{ telephone4=value;}
			get{return telephone4;}
		}
		/// <summary>
        /// ����
		/// </summary>
		public string Country
		{
			set{ country=value;}
			get{return country;}
		}
		/// <summary>
        /// ʡ��
		/// </summary>
		public string Province
		{
			set{ province=value;}
			get{return province;}
		}
		/// <summary>
        /// ����
		/// </summary>
		public string City
		{
			set{ city=value;}
			get{return city;}
		}

        public string Xian {
            set { xian = value; }
            get { return xian; }
        }
		/// <summary>
        /// ��ϸ��ַ
		/// </summary>
		public string StoreAddress
		{
			set{ storeaddress=value;}
			get{return storeaddress;}
		}
		/// <summary>
        /// �ʱ�
		/// </summary>
		public string PostalCode
		{
			set{ postalcode=value;}
			get{return postalcode;}
		}
		/// <summary>
        /// ִ�մ���
		/// </summary>
		public string LicenceCode
		{
			set{ licencecode=value;}
			get{return licencecode;}
		}
		/// <summary>
        /// ��������
		/// </summary>
		public string Bank
		{
			set{ bank=value;}
			get{return bank;}
		}
		/// <summary>
        /// �����˻�
		/// </summary>
		public string BankCard
		{
			set{ bankcard=value;}
			get{return bankcard;}
		}
		/// <summary>
        /// ע������
		/// </summary>
		public DateTime RigisterDate
		{
			set{ rigisterdate=value;}
			get{return rigisterdate;}
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
        /// ˰��
		/// </summary>
		public string Tax
		{
			set{ tax=value;}
			get{return tax;}
		}
		/// <summary>
        /// ��������Ա
		/// </summary>
		public string Administer
		{
			set{ administer=value;}
			get{return administer;}
		}
		/// <summary>
        /// ��������Ա
		/// </summary>
		public string LogisticsPerson
		{
			set{ logisticsperson=value;}
			get{return logisticsperson;}
		}
		/// <summary>
        /// ¼����IP
		/// </summary>
		public string OperateIP
		{
			set{ operateip=value;}
			get{return operateip;}
		}
		/// <summary>
        /// ¼���߱��
		/// </summary>
		public string OperateNum
		{
			set{ operatenum=value;}
			get{return operatenum;}
		}
		#endregion Model

	}
}

