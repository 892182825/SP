using System;
using System.Collections.Generic;
using System.Text;
/*
 * 
 * �����ˣ�     ���ޱ�
 * ����ʱ�䣺   2009��8��27�� AM 10��13
 * �޸��ˣ�     ��  ��
 * �޸�ʱ�䣺   2009-09-16
 * �ļ�����     ProductUnitModel
 * ��Ʒ��λ��
 * 
 */
namespace Model
{
    /// <summary>
    /// ��Ʒ��λ��
    /// </summary>
    public class ProductUnitModel
    {
        public ProductUnitModel() { }
        private int productUnitID;
        /// <summary>
        /// ���
        /// </summary>
        public int ProductUnitID
        {
            get { return productUnitID; }
            set { productUnitID = value; }
        }
        private string productUnitName;
        /// <summary>
        /// ��λ����
        /// </summary>
        public string ProductUnitName
        {
            get { return productUnitName; }
            set { productUnitName = value; }
        }
        private string productUnitDescr;
        /// <summary>
        /// ��λ����
        /// </summary>
        public string ProductUnitDescr
        {
            get { return productUnitDescr; }
            set { productUnitDescr = value; }
        }
    }
}
