using System;
using System.Collections.Generic;
using System.Text;
/*
 * 
 * �����ˣ����ޱ�
 * ����ʱ�䣺2009��8��27�� AM 10��13
 * �ļ�����ProductStatus
 * ����:��Ʒ״̬��
 * 
 */
namespace Model
{
    /// <summary>
    /// ��Ʒ״̬��
    /// </summary>
    public class ProductStatusModel
    {
        public ProductStatusModel() { }
        private int productStatusID;
        /// <summary>
        /// ���
        /// </summary>
        public int ProductStatusID
        {
            get { return productStatusID; }
            set { productStatusID = value; }
        }
        private string productStatusName;
        /// <summary>
        /// ״̬����
        /// </summary>
        public string ProductStatusName
        {
            get { return productStatusName; }
            set { productStatusName = value; }
        }
        private string productStatusDescr;
        /// <summary>
        /// ����
        /// </summary>
        public string ProductStatusDescr
        {
            get { return productStatusDescr; }
            set { productStatusDescr = value; }
        }
    }
}
