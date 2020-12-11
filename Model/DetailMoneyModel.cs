using System;

/// <summary>
/// 实体类DetailMoneyModel 
///宋俊
///2009-8-27
/// 
/// </summary>

namespace Model
{
	[Serializable]
	public class DetailMoneyModel
	{
        /// <summary>
        /// 奖金明细
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
		/// 会员标号
		/// </summary>
		public string Number
		{
			set{ number=value;}
			get{return number;}
		}
		/// <summary>
		/// 发放期数
		/// </summary>
		public int ExpectNum
		{
			set{ expectNum=value;}
			get{return expectNum;}
		}
		/// <summary>
		/// 银行所属的国家
		/// </summary>
		public string BankCountry
		{
			set{ bankCountry=value;}
			get{return bankCountry;}
		}
		/// <summary>
		/// 银行名称
		/// </summary>
		public string Bank
		{
			set{ bank=value;}
			get{return bank;}
		}
		/// <summary>
		/// 开户名
		/// </summary>
		public string BankBook
		{
			set{ bankBook=value;}
			get{return bankBook;}
		}
		/// <summary>
		/// 银行卡号
		/// </summary>
		public string BankCard
		{
			set{ bankCard=value;}
			get{return bankCard;}
		}
		/// <summary>
		/// 发放金额
		/// </summary>
		public decimal Pay
		{
			set{ pay=value;}
			get{return pay;}
		}
		/// <summary>
        /// 发放原因为：公司发放和会员申请
		/// </summary>
		public string PayDetail
		{
			set{ payDetail=value;}
			get{return payDetail;}
		}
		/// <summary>
		/// 会员申请日期
		/// </summary>
		public DateTime MemberDate
		{
			set{ memberDate=value;}
			get{return memberDate;}
		}
		/// <summary>
		/// 公司发放日期
		/// </summary>
		public DateTime PayDate
		{
			set{ payDate=value;}
			get{return payDate;}
		}
		/// <summary>
		/// 是否发放0为未发放，1为已发放
		/// </summary>
		public int IsPay
		{
			set{ isPay=value;}
			get{return isPay;}
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
		#endregion Model

	}
}

