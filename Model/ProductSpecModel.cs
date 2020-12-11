using System;
using System.Collections.Generic;
using System.Text;
/*
 * 
 * 创建人：常艳兵
 * 创建时间：2009年8月27日 AM 10：13
 * 文件名：ProductSpecModel
 * 功能：产品规格表
 * 
 */
namespace Model
{
    /// <summary>
    /// 产品规格表
    /// </summary>
    public class ProductSpecModel
    {
        public ProductSpecModel() { }
        public ProductSpecModel(int id)
        {
            this.productSpecID = id;
        }
        private int productSpecID;
        /// <summary>
        /// 编号
        /// </summary>
        public int ProductSpecID
        {
            get { return productSpecID; }
            set { productSpecID = value; }
        }
        private string productSpecName;
        /// <summary>
        /// 规格名称 
        /// </summary>
        public string ProductSpecName
        {
            get { return productSpecName; }
            set { productSpecName = value; }
        }
        private string productSpecDescr;
        /// <summary>
        /// 描述
        /// </summary>
        public string ProductSpecDescr
        {
            get { return productSpecDescr; }
            set { productSpecDescr = value; }
        }
    }
}
