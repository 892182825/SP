using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class OrderProduct3
    {
          private double price;

        public double Price
        {
            get { return price; }
            set { price = value; }
        }



        private double pv;

        public double Pv
        {
            get { return pv; }
            set { pv = value; }
        }

        private double count;		//货物数量

        public double Count
        {
            get { return count; }
            set { count = value; }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }



        private string pName;

        public string PName
        {
            get { return pName; }
            set { pName = value; }
        }


        private string unitInfo;

        public string UnitInfo
        {
            get { return unitInfo; }
            set { unitInfo = value; }
        }
        private int notEnoughProduct;


        public int NotEnoughProduct
        {
            get { return notEnoughProduct; }
            set { notEnoughProduct = value; }
        }


        private string isGroupItem;

        public string IsGroupItem
        {
            get { return isGroupItem; }
            set { isGroupItem = value; }
        }

        private string hasSubItem;

        public string HasSubItem
        {
            get { return hasSubItem; }
            set { hasSubItem = value; }
        }

        private int diff;

        public int Diff
        {
            get { return diff; }
            set { diff = value; }
        }

        private int isInGroupItemCount;

        public int IsInGroupItemCount
        {
            get { return isInGroupItemCount; }
            set { isInGroupItemCount = value; }
        }

        private string only;

        public string Only
        {
            get { return only; }
            set { only = value; }
        }
        public OrderProduct3() { }



        public OrderProduct3(double price, double pv, double count, int id, int notEnoughProduct, string isGroupItem, string hasSubItem, int diff, int isInGroupItemCount,string only)
        {
            this.Price = price;
            this.Pv = pv;
            this.Count = count;
            this.Id = id;
            this.NotEnoughProduct = notEnoughProduct;
            this.HasSubItem = hasSubItem;
            this.Diff = diff;
            this.isInGroupItemCount = isInGroupItemCount;
            this.Only = only;
            this.UnitInfo = "";
            this.IsGroupItem = isGroupItem;

        }


    }
}
