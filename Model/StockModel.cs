using System;
/// <summary>
/// 实体类StockModel 
///宋俊
///2009-8-27
/// </summary>

namespace Model
{
	[Serializable]
	public class StockModel
	{
        /// <summary>
        /// 店铺产品库存表
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
		/// 店铺编号
		/// </summary>
		public string StoreId
		{
			set{ storeId=value;}
			get{return storeId;}
		}
		/// <summary>
		/// 产品编号
		/// </summary>
		public int ProductId
		{
			set{ productId=value;}
			get{return productId;}
		}
		/// <summary>
		/// 总入
		/// </summary>
		public int TotalIn
		{
			set{ totalIn=value;}
			get{return totalIn;}
		}
		/// <summary>
		/// 总出
		/// </summary>
		public int TotalOut
		{
			set{ totalOut=value;}
			get{return totalOut;}
		}
		/// <summary>
		/// 实际的库存
		/// </summary>
		public decimal ActualStorage
		{
			set{ actualstorage=value;}
			get{return actualstorage;}
		}
		/// <summary>
		/// 周转货的库存
		/// </summary>
		public int TurnStorage
		{
			set{ turnstorage=value;}
			get{return turnstorage;}
		}
		/// <summary>
		/// 预订数量
		/// </summary>
		public int HasorderCount
		{
			set{ hasorderCount=value;}
			get{return hasorderCount;}
		}
		/// <summary>
		/// 在途数量
		/// </summary>
		public int InwayCount
		{
			set{ inwayCount=value;}
			get{return inwayCount;}
		}

		#endregion Model

	}
}

