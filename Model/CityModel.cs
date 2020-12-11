using System;
using System.Collections.Generic;
using System.Text;

/*
 *
 *����ʱ�䣺09/8/27
 *�ļ�����CityModel.cs
 *���ܣ�ϵͳ���б�
 */

namespace Model
{
    /// <summary>
    /// ϵͳ���б�
    /// </summary>
    /// 
    [Serializable]
    public class CityModel
    {
        public CityModel()
        { 
        }

        public CityModel(int id)
        {
            this.id = id;
        }

        private int id;
        private string cPCCode;

        public string CPCCode
        {
            get { return cPCCode; }
            set { cPCCode = value; }
        }
        private string country;
        private string province;
        private string city;
        private string postcode;
        private string fullName;
        private string abridge;
        private string xian;
        /// <summary>
        /// ����
        /// </summary>
        public string Xian
        {
            get
            {
                return xian;
            }
            set
            {
                xian = value;
            }
        }


        /// <summary>
        /// ���
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }

        /// <summary>
        /// ʡ��
        /// </summary>
        public string Province
        {
            get
            {
                return province;
            }
            set
            {
                province = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }

        /// <summary>
        /// �ʱ�
        /// </summary>
        public string Postcode
        {
            get
            {
                return postcode;
            }
            set
            {
                postcode = value;
            }
        }

        /// <summary>
        /// ƴ��ȫ��
        /// </summary>
        public string FullName
        {
            get
            {
                return fullName;
            }
            set
            {
                fullName = value;
            }
        }

        /// <summary>
        /// ƴ����д
        /// </summary>
        public string Abridge
        {
            get
            {
                return abridge;
            }
            set
            {
                abridge = value;
            }
        }
    }
}
