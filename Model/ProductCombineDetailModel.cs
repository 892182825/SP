using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



/**
 *  创建人：  郑华超
 *  创建时间：2009.8.27
 *  文件名：ProductCombineDetailModel.cs
 *  功能：组合产品模型
 */
namespace Model
{
    /// <summary>
    /// 表4.1—66组合产品表
    /// </summary>
    public class ProductCombineDetailModel
    {
        private int iD;
        /// <summary>
        /// 编号ID
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private int combineProductID;

        /// <summary>
        /// 组合产品IP与product表中相同CombineProductID
        /// </summary>
        public int CombineProductID//
        {
            get { return combineProductID; }
            set { combineProductID = value; }
        }
        private int subProductID;

        /// <summary>
        /// 子产品的ID,SubProductID
        /// </summary>
        public int SubProductID
        {
            get { return subProductID; }
            set { subProductID = value; }
        }
        private int quantity;

        /// <summary>
        /// 子产品的数量Quantity
        /// </summary>
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        private double unitPrice;
        public double UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }

        private double pv;
        public double PV
        {
            get { return pv; }
            set { pv = value; }
        }

        private string remark;

        /// <summary>
        /// 备注Remark
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public ProductCombineDetailModel() { }

        public ProductCombineDetailModel(int id) 
        {
            this.iD = id;
        }

    }
}
