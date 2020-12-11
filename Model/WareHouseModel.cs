using System;
using System.Collections.Generic;
using System.Text;
/*
 * 
 * 创建人：  WangHua
 * 创建时间：2010年02月08日
 * 文件名：  WareHouseModel
 * 功能 :   仓库信息表
 * 
 */
namespace Model
{
    /// <summary>
    /// 仓库信息表
    /// </summary>
    public class WareHouseModel
    {
        public WareHouseModel() { }
        private int wareHouseID;
        /// <summary>
        /// 仓库编号
        /// </summary>
        public int WareHouseID
        {
            get { return wareHouseID; }
            set { wareHouseID = value; }
        }

        private string countryCode;

        /// <summary>
        /// 仓库所属国家编码
        /// </summary>
        public string CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; }
        }

        private string wareHouseName;
        /// <summary>
        /// 仓库全称
        /// </summary>
        public string WareHouseName
        {
            get { return wareHouseName; }
            set { wareHouseName = value; }
        }
        private string wareHouseForShort;
        /// <summary>
        /// 仓库简称
        /// </summary>
        public string WareHouseForShort
        {
            get { return wareHouseForShort; }
            set { wareHouseForShort = value; }
        }
        private string wareHousePrincipal;
        /// <summary>
        /// 仓库负责人
        /// </summary>
        public string WareHousePrincipal
        {
            get { return wareHousePrincipal; }
            set { wareHousePrincipal = value; }
        }
        private string wareHouseAddress;
        /// <summary>
        /// 仓库所在地
        /// </summary>
        public string WareHouseAddress
        {
            get { return wareHouseAddress; }
            set { wareHouseAddress = value; }
        }
        private string wareHouseDescr;
        /// <summary>
        /// 描述
        /// </summary>
        public string WareHouseDescr
        {
            get { return wareHouseDescr; }
            set { wareHouseDescr = value; }
        }
        private int wareControl;
        /// <summary>
        /// 权限控制
        /// </summary>
        public int WareControl
        {
            get { return wareControl; }
            set { wareControl = value; }
        }


        /// <summary>
        /// 地址代码
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
