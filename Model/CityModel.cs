using System;
using System.Collections.Generic;
using System.Text;

/*
 *
 *创建时间：09/8/27
 *文件名：CityModel.cs
 *功能：系统城市表
 */

namespace Model
{
    /// <summary>
    /// 系统城市表
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
        /// 区县
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
        /// 编号
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
        /// 国家
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
        /// 省份
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
        /// 城市
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
        /// 邮编
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
        /// 拼音全称
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
        /// 拼音缩写
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
