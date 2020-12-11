using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class OrderProduct2
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

        public OrderProduct2() { }
        public OrderProduct2(double price, double pv, double count, int id, int notEnoughProduct, string pname)
        {
            this.Price = price;
            this.Pv = pv;
            this.Count = count;
            this.Id = id;
            this.NotEnoughProduct = notEnoughProduct;
            this.PName = pname;
            this.UnitInfo = "";

        }


        public OrderProduct2(double price, double pv, double count, int id, int notEnoughProduct, string pname,string isGroupItem)
        {
            this.Price = price;
            this.Pv = pv;
            this.Count = count;
            this.Id = id;
            this.NotEnoughProduct = notEnoughProduct;
            this.PName = pname;
            this.UnitInfo = "";
            this.IsGroupItem = isGroupItem;

        }



    }
}
