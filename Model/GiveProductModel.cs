using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class GiveProductModel
    {
        private int id;
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private int productid;
        /// <summary>
        /// 产品编号
        /// </summary>
        public int productId
        {
            get { return productid; }
            set { productid = value; }
        }

        private double price;
        /// <summary>
        /// 单价price
        /// </summary>
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        private int productquantity;

        public int ProductQuantity
        {
            get { return productquantity; }
            set { productquantity = value; }
        }

        private double pv;
        /// <summary>
        /// 单价pv
        /// </summary>
        public double PV
        {
            get { return pv; }
            set { pv = value; }
        }

        private double totalprice;
        /// <summary>
        /// 总价
        /// </summary>
        public double TotalPrice
        {
            get { return totalprice; }
            set { totalprice = value; }
        }

        private double totalpv;
        /// <summary>
        /// 总pv
        /// </summary>
        public double TotalPV
        {
            get { return totalpv; }
            set { totalpv = value; }
        }

        private int setgivepvid;
        /// <summary>
        /// 对应的PV额度编号
        /// </summary>
        public int SetGivePVID
        {
            get { return setgivepvid; }
            set { setgivepvid = value; }
        }
    }
}
