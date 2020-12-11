using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 创建者：      WangHua
 * 完成时间：    2010年02月21日
 * 文件名：      CountryModel
 * 功能：        国家表模型
 * **/
namespace Model
{
    /// <summary>
    /// 国家表
    /// </summary>
    public class CountryModel
    {
        public CountryModel()
        { }

        public CountryModel(int id)
        {
            this.iD = id;
        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string name;
        /// <summary>
        /// 国家名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int rateID;

        /// <summary>
        /// 对应币种ID
        /// </summary>
        public int RateID
        {
            get { return rateID; }
            set { rateID = value; }
        }

        private string countryCode;

        /// <summary>
        /// 国家代码
        /// </summary>
        public string CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; }
        }

        private string countryForShort;

        /// <summary>
        /// 国家简称
        /// </summary>
        public string CountryForShort
        {
            get { return countryForShort; }
            set { countryForShort = value; }
        }
    }
}
