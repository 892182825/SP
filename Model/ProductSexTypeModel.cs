using System;
using System.Collections.Generic;
using System.Text;
/*
 * 
 * �����ˣ����ޱ�
 * ����ʱ�䣺2009��8��27�� AM 10��13
 * �ļ�����ProductSexTypeModel
 * ���ܣ���Ʒ������Ⱥ��
 * 
 */
namespace Model
{
    /// <summary>
    /// ��Ʒ������Ⱥ��
    /// </summary>
    public class ProductSexTypeModel
    {
        public ProductSexTypeModel() { }
        private int productSexTypeID;
        /// <summary>
        /// ���
        /// </summary>
        public int ProductSexTypeID
        {
            get { return productSexTypeID; }
            set { productSexTypeID = value; }
        }
        private string productSexTypeName;
        /// <summary>
        /// ����
        /// </summary>
        public string ProductSexTypeName
        {
            get { return productSexTypeName; }
            set { productSexTypeName = value; }
        }
        private string productSexTypeDescr;
        /// <summary>
        /// ����
        /// </summary>
        public string ProductSexTypeDescr
        {
            get { return productSexTypeDescr; }
            set { productSexTypeDescr = value; }
        }
    }
}
