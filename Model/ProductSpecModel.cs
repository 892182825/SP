using System;
using System.Collections.Generic;
using System.Text;
/*
 * 
 * �����ˣ����ޱ�
 * ����ʱ�䣺2009��8��27�� AM 10��13
 * �ļ�����ProductSpecModel
 * ���ܣ���Ʒ����
 * 
 */
namespace Model
{
    /// <summary>
    /// ��Ʒ����
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
        /// ���
        /// </summary>
        public int ProductSpecID
        {
            get { return productSpecID; }
            set { productSpecID = value; }
        }
        private string productSpecName;
        /// <summary>
        /// ������� 
        /// </summary>
        public string ProductSpecName
        {
            get { return productSpecName; }
            set { productSpecName = value; }
        }
        private string productSpecDescr;
        /// <summary>
        /// ����
        /// </summary>
        public string ProductSpecDescr
        {
            get { return productSpecDescr; }
            set { productSpecDescr = value; }
        }
    }
}
