using System;
/// <summary>
/// ʵ����PayControlModel 
///�ο�
///2009-8-27
/// </summary>

namespace Model
{
	[Serializable]
	public class PayControlModel
	{
        /// <summary>
        /// ���𷢷ſ���
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
		/// �ѷ��ŵ�����
		/// </summary>
		public int ExpectNum
		{
			set{ expectNum=value;}
			get{return expectNum;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime PayDate
		{
			set{ payDate=value;}
			get{return payDate;}
		}
		#endregion Model

	}
}

