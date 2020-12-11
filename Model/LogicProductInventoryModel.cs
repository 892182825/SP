using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/**
 *  创建人：  郑华超
 *  创建时间：2009.8.27
 *  文件名：LogicProductInventoryModel.cs
 *  功能：产品逻辑库存模型
 */
namespace Model
{
    /// <summary>
    /// 表4.1—61产品逻辑库存表
    /// </summary>
  public  class LogicProductInventoryModel
    {
        private int iD;

         /// <summary>
         /// 编号ID
         /// </summary>
        public int ID  
        {
            get { return iD; }
           
        }
        private int productID;

        /// <summary>
        /// 产品ProductID
        /// </summary>
        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }
        private double totalIn;

        /// <summary>
        /// 总入：小单位TotalIn
        /// </summary>
        public double TotalIn//总入：小单位TotalIn
        {
            get { return totalIn; }
            set { totalIn = value; }
        }
        private double totalOut;

       /// <summary>
        /// 总出：小单位TotalOut
       /// </summary>
        public double TotalOut
        {
            get { return totalOut; }
            set { totalOut = value; }
        }
        private double totalLogicOut;

       /// <summary>
       /// 总出：小单位TotalOut
       /// </summary>
        public double TotalLogicOutz
        {
            get { return totalLogicOut; }
            set { totalLogicOut = value; }
        }

        public LogicProductInventoryModel() 
        {}
        public LogicProductInventoryModel(int id)
        {
            this.iD = id;
        }

    }
}
