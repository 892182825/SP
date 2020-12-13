using System;
/// <summary>
/// ʵ����StockModel 
///�ο�
///2009-8-27
/// </summary>

namespace Model
{
	[Serializable]
	public class StockModel
	{
        /// <summary>
        /// ���̲�Ʒ����
        /// </summary>
		public StockModel()
		{}
		#region Model
		private int id;
		private string storeId;
		private int productId;
		private int totalIn;
		private int totalOut;
		private decimal actualstorage;
		private int turnstorage;
		private int hasorderCount;
		private int inwayCount;
        private int groupSmallStorage;

        public int GroupSmallStorage
        {
            get { return groupSmallStorage; }
            set { groupSmallStorage = value; }
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
		/// ���̱��
		/// </summary>
		public string StoreId
		{
			set{ storeId=value;}
			get{return storeId;}
		}
		/// <summary>
		/// ��Ʒ���
		/// </summary>
		public int ProductId
		{
			set{ productId=value;}
			get{return productId;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public int TotalIn
		{
			set{ totalIn=value;}
			get{return totalIn;}
		}
		/// <summary>
		/// �ܳ�
		/// </summary>
		public int TotalOut
		{
			set{ totalOut=value;}
			get{return totalOut;}
		}
		/// <summary>
		/// ʵ�ʵĿ��
		/// </summary>
		public decimal ActualStorage
		{
			set{ actualstorage=value;}
			get{return actualstorage;}
		}
		/// <summary>
		/// ��ת���Ŀ��
		/// </summary>
		public int TurnStorage
		{
			set{ turnstorage=value;}
			get{return turnstorage;}
		}
		/// <summary>
		/// Ԥ������
		/// </summary>
		public int HasorderCount
		{
			set{ hasorderCount=value;}
			get{return hasorderCount;}
		}
		/// <summary>
		/// ��;����
		/// </summary>
		public int InwayCount
		{
			set{ inwayCount=value;}
			get{return inwayCount;}
		}

		#endregion Model

	}
}

