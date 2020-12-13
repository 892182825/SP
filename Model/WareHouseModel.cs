using System;
using System.Collections.Generic;
using System.Text;
/*
 * 
 * �����ˣ�  WangHua
 * ����ʱ�䣺2010��02��08��
 * �ļ�����  WareHouseModel
 * ���� :   �ֿ���Ϣ��
 * 
 */
namespace Model
{
    /// <summary>
    /// �ֿ���Ϣ��
    /// </summary>
    public class WareHouseModel
    {
        public WareHouseModel() { }
        private int wareHouseID;
        /// <summary>
        /// �ֿ���
        /// </summary>
        public int WareHouseID
        {
            get { return wareHouseID; }
            set { wareHouseID = value; }
        }

        private string countryCode;

        /// <summary>
        /// �ֿ��������ұ���
        /// </summary>
        public string CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; }
        }

        private string wareHouseName;
        /// <summary>
        /// �ֿ�ȫ��
        /// </summary>
        public string WareHouseName
        {
            get { return wareHouseName; }
            set { wareHouseName = value; }
        }
        private string wareHouseForShort;
        /// <summary>
        /// �ֿ���
        /// </summary>
        public string WareHouseForShort
        {
            get { return wareHouseForShort; }
            set { wareHouseForShort = value; }
        }
        private string wareHousePrincipal;
        /// <summary>
        /// �ֿ⸺����
        /// </summary>
        public string WareHousePrincipal
        {
            get { return wareHousePrincipal; }
            set { wareHousePrincipal = value; }
        }
        private string wareHouseAddress;
        /// <summary>
        /// �ֿ����ڵ�
        /// </summary>
        public string WareHouseAddress
        {
            get { return wareHouseAddress; }
            set { wareHouseAddress = value; }
        }
        private string wareHouseDescr;
        /// <summary>
        /// ����
        /// </summary>
        public string WareHouseDescr
        {
            get { return wareHouseDescr; }
            set { wareHouseDescr = value; }
        }
        private int wareControl;
        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        public int WareControl
        {
            get { return wareControl; }
            set { wareControl = value; }
        }


        /// <summary>
        /// ��ַ����
        /// </summary>
        public string CPCCode
        {
            get;
            set;
        }

        public CityModel AddressEntity
        {
            get;
            set;
        }
    }
}
