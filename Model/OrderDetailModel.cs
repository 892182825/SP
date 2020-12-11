using System;
/// <summary>
/// ʵ����OrderDetailModel
///�ο�
///2009-8-27
/// </summary>

namespace Model
{
    /// <summary>
    /// ���̶�����Ʒ��ϸ
    /// </summary>
    [Serializable]
	public class OrderDetailModel
	{
        /// <summary>
        /// ���̶�����Ʒ��ϸ
        /// </summary>
		public OrderDetailModel()
		{}
		#region Model
		private int id;
		private string storeorderId;
		private string storeId;
		private int productId;
		private int expectNum;
		private int quantity;
		private decimal price;
		private decimal pv;
		private string description;
       

		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ id=value;}
			get{return id;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string StoreorderId
		{
			set{ storeorderId=value;}
			get{return storeorderId;}
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
		/// ��ƷID
		/// </summary>
		public int ProductId
		{
			set{ productId=value;}
			get{return productId;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public int ExpectNum
		{
			set{ expectNum=value;}
			get{return expectNum;}
		}
		/// <summary>
        /// ԭ����������
		/// </summary>
		public int Quantity
		{
			set{ quantity=value;}
			get{return quantity;}
		}
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public decimal Price
		{
			set{ price=value;}
			get{return price;}
		}
		/// <summary>
		/// ��ƷPV
		/// </summary>
		public decimal Pv
		{
			set{ pv=value;}
			get{return pv;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Description
		{
			set{ description=value;}
			get{return description;}
		}
		#endregion Model


        /**************************************************������************************************************/
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public string ProductName { set; get; }
        /// <summary>
        /// ��Ʒ�ͺ�����
        /// </summary>
        public string ProductTypeName { set; get; }


	}
}

