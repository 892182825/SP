﻿using System;
/// <summary>
/// 实体类OrderGoodsDetail
///宋俊
///2009-8-27
/// </summary>

namespace Model
{
    /// <summary>
    /// 店铺订货产品明细
    /// </summary>
    [Serializable]
    public class OrderGoodsDetailModel
    {
        /// <summary>
        /// 店铺订货产品明细
        /// </summary>
        public OrderGoodsDetailModel() { }



        #region Model

        private int id;
        private string orderGoodsID;
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
            set { id = value; }
            get { return id; }
        }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderGoodsID
        {
            get { return orderGoodsID; }
            set { orderGoodsID = value; }
        }
        /// <summary>
        /// 店铺编号
        /// </summary>
        public string StoreId
        {
            set { storeId = value; }
            get { return storeId; }
        }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductId
        {
            set { productId = value; }
            get { return productId; }
        }
        /// <summary>
        /// 期数
        /// </summary>
        public int ExpectNum
        {
            set { expectNum = value; }
            get { return expectNum; }
        }
        /// <summary>
        /// 原来订的数量
        /// </summary>
        public int Quantity
        {
            set { quantity = value; }
            get { return quantity; }
        }
        
        /// <summary>
        /// 产品单价
        /// </summary>
        public decimal Price
        {
            set { price = value; }
            get { return price; }
        }
        /// <summary>
        /// 产品PV
        /// </summary>
        public decimal Pv
        {
            set { pv = value; }
            get { return pv; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set { description = value; }
            get { return description; }
        }
        
        #endregion Model
    }
}

