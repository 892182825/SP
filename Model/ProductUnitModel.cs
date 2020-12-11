using System;
using System.Collections.Generic;
using System.Text;
/*
 * 
 * 创建人：     常艳兵
 * 创建时间：   2009年8月27日 AM 10：13
 * 修改人：     汪  华
 * 修改时间：   2009-09-16
 * 文件名：     ProductUnitModel
 * 产品单位表
 * 
 */
namespace Model
{
    /// <summary>
    /// 产品单位表
    /// </summary>
    public class ProductUnitModel
    {
        public ProductUnitModel() { }
        private int productUnitID;
        /// <summary>
        /// 编号
        /// </summary>
        public int ProductUnitID
        {
            get { return productUnitID; }
            set { productUnitID = value; }
        }
        private string productUnitName;
        /// <summary>
        /// 单位名称
        /// </summary>
        public string ProductUnitName
        {
            get { return productUnitName; }
            set { productUnitName = value; }
        }
        private string productUnitDescr;
        /// <summary>
        /// 单位描述
        /// </summary>
        public string ProductUnitDescr
        {
            get { return productUnitDescr; }
            set { productUnitDescr = value; }
        }
    }
}
