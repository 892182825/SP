using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 *  创建人：  郑华超
 *  创建时间：2009.8.27
 *  文件名：BatchModel.cs
 *  功能： 批次
 */
namespace Model
{
    /// <summary>
    /// 批次表
    /// </summary>
    public class BatchModel
    {
        private int id;
        /// <summary>
        /// 标示id
        /// </summary>
        public int Id
        {
            get { return id; }
        }
        private int productId;

        /// <summary>
        /// 商品ID，引ProductQuantity  ProductId
        /// </summary>
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
        private string batchNumber;

        /// <summary>
        /// 批次号BatchNumber
        /// </summary>
        public string BatchNumber
        {
            get { return batchNumber; }
            set { batchNumber = value; }
        }
        private int isNull;
        /// <summary>
        /// 是否清空 IsNull 
        /// </summary>
        public int IsNull
        {
            get { return isNull; }
            set { isNull = value; }
        }
    
        public BatchModel() { }


        public BatchModel(int id ) 
        {
            this.id = id;
        
        }
    }
}
