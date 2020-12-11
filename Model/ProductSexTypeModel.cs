using System;
using System.Collections.Generic;
using System.Text;
/*
 * 
 * 创建人：常艳兵
 * 创建时间：2009年8月27日 AM 10：13
 * 文件名：ProductSexTypeModel
 * 功能：产品适用人群表
 * 
 */
namespace Model
{
    /// <summary>
    /// 产品适用人群表
    /// </summary>
    public class ProductSexTypeModel
    {
        public ProductSexTypeModel() { }
        private int productSexTypeID;
        /// <summary>
        /// 编号
        /// </summary>
        public int ProductSexTypeID
        {
            get { return productSexTypeID; }
            set { productSexTypeID = value; }
        }
        private string productSexTypeName;
        /// <summary>
        /// 名称
        /// </summary>
        public string ProductSexTypeName
        {
            get { return productSexTypeName; }
            set { productSexTypeName = value; }
        }
        private string productSexTypeDescr;
        /// <summary>
        /// 描述
        /// </summary>
        public string ProductSexTypeDescr
        {
            get { return productSexTypeDescr; }
            set { productSexTypeDescr = value; }
        }
    }
}
