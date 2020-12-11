using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * 结构类
 * 作者：zhc
 */
namespace DAL.Other
{
    [Serializable]
    public struct OrderProduct
    {
        public double price, pv;
        public double count;		//货物数量
        public int id;

        public bool isNew;
        public string unitInfo;
        public int notEnoughProduct;
        public string isGroupItem;
        public string hasSubItem;
        public int diff;
        public int isInGroupItemCount;
        public string only;

        //public OrderProduct(int id ) 
        //{
        //    this.id = id;

        //}
        //public OrderProduct(double price, double pv, double count, int id, int notEnoughProduct)
        //{
        //    this.price = price;
        //    this.pv = pv;
        //    this.count = count;
        //    this.id = id;
        //    this.notEnoughProduct = notEnoughProduct;
        //    isNew = true;
        //    unitInfo = "";

        //}

        public OrderProduct(double price, double pv, double count, int id, int notEnoughProduct, string isGroupItem, string hasSubItem, int diff, int isInGroupItemCount, string only)
        {
            this.price = price;
            this.pv = pv;
            this.count = count;
            this.id = id;
            this.notEnoughProduct = notEnoughProduct;
            this.isGroupItem = isGroupItem;
            this.hasSubItem = hasSubItem;
            this.diff = diff;
            this.isInGroupItemCount = isInGroupItemCount;
            this.only = only;
            isNew = true;
            unitInfo = "";

        }
        //public OrderProduct(double price, double pv, double count, int id, bool isNew)
        //{
        //    this.price = price;
        //    this.pv = pv;
        //    this.count = count;
        //    this.id = id;
        //    this.isNew = isNew;
        //    unitInfo = "";

        //}

        //public OrderProduct(double price, double pv, double count, int id, string unitInfo)
        //{
        //    this.price = price;
        //    this.pv = pv;
        //    this.count = count;
        //    this.id = id;
        //    this.unitInfo = unitInfo;
        //    isNew = true;
        //}


    }
}
