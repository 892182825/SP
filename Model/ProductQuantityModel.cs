using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 *  创建人：  郑华超
 *  创建时间：2009.8.27
 *  文件名：ProductQuantityModel.cs
 *  功能：产品库存模型
 */
namespace Model
{
    /// <summary>
    /// 表4.1—60产品库存表
    /// </summary>
    public class ProductQuantityModel
    {
        private int iD;

        public int ID//
        {
            get { return iD; }
           
        }
        private int productID;

        /// <summary>
        /// 产品ID   ProductID
        /// </summary>
        public int ProductID 
        {
            get { return productID; }
            set { productID = value; }
        }
        private double totalIn;

        /// <summary>
        /// 总入：小单位
        /// </summary>
        public double TotalIn
        {
            get { return totalIn; }
            set { totalIn = value; }
        }
        private double totalOut;

        /// <summary>
        /// 总出：小单位  TotalOut
        /// </summary>
        public double TotalOut
        {
            get { return totalOut; }
            set { totalOut = value; }
        }
        private double totalLogicOut;


        /// <summary>
        /// 未用  TotalLogicOut
        /// </summary>
        public double TotalLogicOut
        {
            get { return totalLogicOut; }
            set { totalLogicOut = value; }
        }
        private int wareHouseID;

        /// <summary>
        /// 仓库ID  WareHouseID
        /// </summary>
        public int WareHouseID
        {
            get { return wareHouseID; }
            set { wareHouseID = value; }
        }

        public ProductQuantityModel() { }

        public ProductQuantityModel(int id) 
        {
            this.iD = id;
        }
    }
}
