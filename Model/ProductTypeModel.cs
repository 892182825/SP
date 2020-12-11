using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 创建者：张朔
 * 创建时间：2009-8-27
 * 文件名:ProductTypeModel
 * 功能：产品型号表模型
 */

namespace Model
{
    /// <summary>
    /// 产品型号表
    /// </summary>
    public class ProductTypeModel
    {
        public ProductTypeModel() { }

        private int productTypeID;
        /// <summary>
        /// 编号
        /// </summary>
        public int ProductTypeID
        {
            get { return productTypeID; }
            set { productTypeID = value; }
        }
        private string productTypeName;
        /// <summary>
        /// 产品类型名称
        /// </summary>
        public string ProductTypeName
        {
            get { return productTypeName; }
            set { productTypeName = value; }
        }
        private string productTypeDescr;
        /// <summary>
        /// 描述
        /// </summary>
        public string ProductTypeDescr
        {
            get { return productTypeDescr; }
            set { productTypeDescr = value; }
        }

    }
}
