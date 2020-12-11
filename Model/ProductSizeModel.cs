using System;
using System.Collections.Generic;
using System.Text;
/*
 * 
 * 创建人：常艳兵
 * 创建时间：2009年8月27日 AM 10：13
 * 文件名：ProductSizeModel
 * 功能：产品尺寸表模型
 * 
 */
namespace Model
{
    /// <summary>
    /// 产品尺寸表
    /// </summary>
    public class ProductSizeModel
    {
        public ProductSizeModel() { }
        private int productSizeID;
        /// <summary>
        /// 编号
        /// </summary>
        public int ProductSizeID
        {
            get { return productSizeID; }
            set { productSizeID = value; }
        }
        private string productSizeName;
        /// <summary>
        /// 尺寸名称
        /// </summary>
        public string ProductSizeName
        {
            get { return productSizeName; }
            set { productSizeName = value; }
        }
        private string productSizeDescr;
        /// <summary>
        /// 描述
        /// </summary>
        public string ProductSizeDescr
        {
            get { return productSizeDescr; }
            set { productSizeDescr = value; }
        }
    }
}
