using System;
using System.Collections.Generic;
using System.Text;
/*
 * 
 * �����ˣ����ޱ�
 * ����ʱ�䣺2009��8��27�� AM 10��13
 * �ļ�����ProductSizeModel
 * ���ܣ���Ʒ�ߴ��ģ��
 * 
 */
namespace Model
{
    /// <summary>
    /// ��Ʒ�ߴ��
    /// </summary>
    public class ProductSizeModel
    {
        public ProductSizeModel() { }
        private int productSizeID;
        /// <summary>
        /// ���
        /// </summary>
        public int ProductSizeID
        {
            get { return productSizeID; }
            set { productSizeID = value; }
        }
        private string productSizeName;
        /// <summary>
        /// �ߴ�����
        /// </summary>
        public string ProductSizeName
        {
            get { return productSizeName; }
            set { productSizeName = value; }
        }
        private string productSizeDescr;
        /// <summary>
        /// ����
        /// </summary>
        public string ProductSizeDescr
        {
            get { return productSizeDescr; }
            set { productSizeDescr = value; }
        }
    }
}
