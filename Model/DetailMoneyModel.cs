using System;

/// <summary>
/// ʵ����DetailMoneyModel 
///�ο�
///2009-8-27
/// 
/// </summary>

namespace Model
{
	[Serializable]
	public class DetailMoneyModel
	{
        /// <summary>
        /// ������ϸ
        /// </summary>
		public DetailMoneyModel()
		{}
		#region Model
		private int id;
		private string number;
		private int expectNum;
		private string bankCountry;
		private string bank;
		private string bankBook;
		private string bankCard;
		private decimal pay;
		private string payDetail;
		private DateTime memberDate;
		private DateTime payDate;
		private int isPay;
		private string remark;
		private string operateIp;
		private string operateNum;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ id=value;}
			get{return id;}
		}
		/// <summary>
		/// ��Ա���
		/// </summary>
		public string Number
		{
			set{ number=value;}
			get{return number;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public int ExpectNum
		{
			set{ expectNum=value;}
			get{return expectNum;}
		}
		/// <summary>
		/// ���������Ĺ���
		/// </summary>
		public string BankCountry
		{
			set{ bankCountry=value;}
			get{return bankCountry;}
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
		/// ������
		/// </summary>
		public string BankBook
		{
			set{ bankBook=value;}
			get{return bankBook;}
		}
		/// <summary>
		/// ���п���
		/// </summary>
		public string BankCard
		{
			set{ bankCard=value;}
			get{return bankCard;}
		}
		/// <summary>
		/// ���Ž��
		/// </summary>
		public decimal Pay
		{
			set{ pay=value;}
			get{return pay;}
		}
		/// <summary>
        /// ����ԭ��Ϊ����˾���źͻ�Ա����
		/// </summary>
		public string PayDetail
		{
			set{ payDetail=value;}
			get{return payDetail;}
		}
		/// <summary>
		/// ��Ա��������
		/// </summary>
		public DateTime MemberDate
		{
			set{ memberDate=value;}
			get{return memberDate;}
		}
		/// <summary>
		/// ��˾��������
		/// </summary>
		public DateTime PayDate
		{
			set{ payDate=value;}
			get{return payDate;}
		}
		/// <summary>
		/// �Ƿ񷢷�0Ϊδ���ţ�1Ϊ�ѷ���
		/// </summary>
		public int IsPay
		{
			set{ isPay=value;}
			get{return isPay;}
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
		#endregion Model

	}
}

