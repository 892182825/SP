using System;
/// <summary>
/// 实体类PayControlModel 
///宋俊
///2009-8-27
/// </summary>

namespace Model
{
	[Serializable]
	public class PayControlModel
	{
        /// <summary>
        /// 奖金发放控制
        /// </summary>
		public PayControlModel()
		{}
		#region Model
		private int id;
		private int expectNum;
		private DateTime payDate;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ id=value;}
			get{return id;}
		}
		/// <summary>
		/// 已发放的期数
		/// </summary>
		public int ExpectNum
		{
			set{ expectNum=value;}
			get{return expectNum;}
		}
		/// <summary>
		/// 发放时间
		/// </summary>
		public DateTime PayDate
		{
			set{ payDate=value;}
			get{return payDate;}
		}
		#endregion Model

	}
}

