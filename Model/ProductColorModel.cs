using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 创建者：张朔
 * 创建时间：2009年8月27日 AM 10：07
 * 文件名：ProductColorModel
 * 功能：产品颜色表
 */

namespace Model
{
    /// <summary>
    /// 产品颜色表
    /// </summary>
    public class ProductColorModel
    {
        public ProductColorModel() { }

        private int productColorID;
        /// <summary>
        /// 编号
        /// </summary>
        public int ProductColorID
        {
            get { return productColorID; }
            set { productColorID = value; }
        }
        private string productColorName;
        /// <summary>
        /// 名称
        /// </summary>
        public string ProductColorName
        {
            get { return productColorName; }
            set { productColorName = value; }
        }
        private string productColorDescr;
        /// <summary>
        /// 描述
        /// </summary>
        public string ProductColorDescr
        {
            get { return productColorDescr; }
            set { productColorDescr = value; }
        }

    }
}
