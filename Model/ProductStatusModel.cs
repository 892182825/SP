using System;
using System.Collections.Generic;
using System.Text;
/*
 * 
 * 创建人：常艳兵
 * 创建时间：2009年8月27日 AM 10：13
 * 文件名：ProductStatus
 * 功能:产品状态表
 * 
 */
namespace Model
{
    /// <summary>
    /// 产品状态表
    /// </summary>
    public class ProductStatusModel
    {
        public ProductStatusModel() { }
        private int productStatusID;
        /// <summary>
        /// 编号
        /// </summary>
        public int ProductStatusID
        {
            get { return productStatusID; }
            set { productStatusID = value; }
        }
        private string productStatusName;
        /// <summary>
        /// 状态名称
        /// </summary>
        public string ProductStatusName
        {
            get { return productStatusName; }
            set { productStatusName = value; }
        }
        private string productStatusDescr;
        /// <summary>
        /// 描述
        /// </summary>
        public string ProductStatusDescr
        {
            get { return productStatusDescr; }
            set { productStatusDescr = value; }
        }
    }
}
