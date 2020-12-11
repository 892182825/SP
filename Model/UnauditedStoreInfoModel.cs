using System;

/*
 * 功能：       实体类UnauditedStoreInfoModel 
 * 创建者：     宋  俊
 * 创建时间：   2009-8-27
 * 修改者：     汪  华
 * 修改时间：   2009-08-31
 */


namespace Model
{
    [Serializable]
    public class UnauditedStoreInfoModel
    {
        /// <summary>
        /// 未审核店铺信息
        /// </summary>
        public UnauditedStoreInfoModel()
        { }
        #region Model
        private int id;
        private string number;
        private string direct;
        private string storeId;
        private string name;
        private int expectNum;
        private string loginPass;
        private string advPass;
        private string storeName;
        private string country;
        private string province;
        private string city;
        private string xian;
        private string storeAddress;
        private string postalCode;
        private string homeTele;
        private string officeTele;
        private string faxTele;
        private string bank;
        private string bankCard;
        private string email;
        private string netAddress;
        private string remark;
        private DateTime registerDate;
        private string storeLevelStr;
        private int storeLevelInt;
        private decimal farearea;
        private string fareBreed;
        private int offendTimes;
        private int accreditExpectNum;
        private decimal totalordergoodsMoney;
        private decimal totalmemberorderMoney;
        private decimal totalaccountMoney;
        private decimal totalinvestMoney;
        private int storagescalar;
        private string permissionman;
        private decimal totalmaxMoney;
        private decimal totalcomityMoney;
        private decimal totalcomityPv;
        private decimal totalindentMoney;
        private decimal totalindentPv;
        private decimal totalchangeMoney;
        private decimal totalchangepv;
        private string answer;
        private string question;
        private string photoPath;
        private int photoh;
        private int photow;
        private int language;
        private int currency;
        private string dianCity;
        private string dcountry;
        private string operateIp;
        private string operateNum;
        private string storeCity;
        private string sCPCCode;
        private string bankCode;

        public string BankCode
        {
            get { return bankCode; }
            set { bankCode = value; }
        }
        public string SCPCCode
        {
            get { return sCPCCode; }
            set { sCPCCode = value; }
        }

        public string StoreCity
        {
            get { return storeCity; }
            set { storeCity = value; }
        }
        private string storeCountry;

        public string StoreCountry
        {
            get { return storeCountry; }
            set { storeCountry = value; }
        }
        private string mobileTele;

        public string MobileTele
        {
            get { return mobileTele; }
            set { mobileTele = value; }
        }
        private string cPCCode;

        public string CPCCode
        {
            get { return cPCCode; }
            set { cPCCode = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { id = value; }
            get { return id; }
        }
        /// <summary>
        /// 店长会员编号
        /// </summary>
        public string Number
        {
            set { number = value; }
            get { return number; }
        }
        /// <summary>
        /// 推荐店的会员编号
        /// </summary>
        public string Direct
        {
            set { direct = value; }
            get { return direct; }
        }
        /// <summary>
        /// 店铺编号
        /// </summary>
        public string StoreId
        {
            set { storeId = value; }
            get { return storeId; }
        }

        /// <summary>
        /// 店长姓名
        /// </summary>
        public string Name
        {
            set { name = value; }
            get { return name; }
        }
        /// <summary>
        /// 期数
        /// </summary>
        public int ExpectNum
        {
            set { expectNum = value; }
            get { return expectNum; }
        }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPass
        {
            set { loginPass = value; }
            get { return loginPass; }
        }
        /// <summary>
        /// 电子账户密码（未用）
        /// </summary>
        public string AdvPass
        {
            set { advPass = value; }
            get { return advPass; }
        }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string StoreName
        {
            set { storeName = value; }
            get { return storeName; }
        }
        /// <summary>
        /// 店长所属国家
        /// </summary>
        public string Country
        {
            set { country = value; }
            get { return country; }
        }
        /// <summary>
        /// 店长所属省份
        /// </summary>
        public string Province
        {
            set { province = value; }
            get { return province; }
        }
        /// <summary>
        /// 店长所属城市
        /// </summary>
        public string City
        {
            set { city = value; }
            get { return city; }
        }
        /// <summary>
        /// 店长所属区/县
        /// </summary>
        public string Xian
        {
            set { xian = value; }
            get { return xian; }
        }
        /// <summary>
        /// 店长详细地址
        /// </summary>
        public string StoreAddress
        {
            set { storeAddress = value; }
            get { return storeAddress; }
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string PostalCode
        {
            set { postalCode = value; }
            get { return postalCode; }
        }
        /// <summary>
        /// 家庭电话
        /// </summary>
        public string HomeTele
        {
            set { homeTele = value; }
            get { return homeTele; }
        }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string OfficeTele
        {
            set { officeTele = value; }
            get { return officeTele; }
        }
        /// <summary>
        /// 传真电话
        /// </summary>
        public string FaxTele
        {
            set { faxTele = value; }
            get { return faxTele; }
        }
        /// <summary>
        /// 银行名称
        /// </summary>
        public string Bank
        {
            set { bank = value; }
            get { return bank; }
        }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankCard
        {
            set { bankCard = value; }
            get { return bankCard; }
        }
        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            set { email = value; }
            get { return email; }
        }
        /// <summary>
        /// 网址
        /// </summary>
        public string NetAddress
        {
            set { netAddress = value; }
            get { return netAddress; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { remark = value; }
            get { return remark; }
        }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterDate
        {
            set { registerDate = value; }
            get { return registerDate; }
        }
        /// <summary>
        /// 级别字符串
        /// </summary>
        public string StoreLevelStr
        {
            set { storeLevelStr = value; }
            get { return storeLevelStr; }
        }
        /// <summary>
        /// 级别数字
        /// </summary>
        public int StoreLevelInt
        {
            set { storeLevelInt = value; }
            get { return storeLevelInt; }
        }
        /// <summary>
        /// 经营面积
        /// </summary>
        public decimal FareArea
        {
            set { farearea = value; }
            get { return farearea; }
        }
        /// <summary>
        /// 经营品种
        /// </summary>
        public string FareBreed
        {
            set { fareBreed = value; }
            get { return fareBreed; }
        }
        /// <summary>
        /// 违规次数
        /// </summary>
        public int OffendTimes
        {
            set { offendTimes = value; }
            get { return offendTimes; }
        }
        /// <summary>
        /// 授权期数
        /// </summary>
        public int AccreditExpectNum
        {
            set { accreditExpectNum = value; }
            get { return accreditExpectNum; }
        }
        /// <summary>
        /// 总的订货额
        /// </summary>
        public decimal TotalordergoodsMoney
        {
            set { totalordergoodsMoney = value; }
            get { return totalordergoodsMoney; }
        }
        /// <summary>
        /// 总的报单额
        /// </summary>
        public decimal TotalmemberorderMoney
        {
            set { totalmemberorderMoney = value; }
            get { return totalmemberorderMoney; }
        }
        /// <summary>
        /// 总回款额
        /// </summary>
        public decimal TotalaccountMoney
        {
            set { totalaccountMoney = value; }
            get { return totalaccountMoney; }
        }
        /// <summary>
        /// 备用
        /// </summary>
        public decimal TotalinvestMoney
        {
            set { totalinvestMoney = value; }
            get { return totalinvestMoney; }
        }
        /// <summary>
        /// 备用
        /// </summary>
        public int StorageScalar
        {
            set { storagescalar = value; }
            get { return storagescalar; }
        }
        /// <summary>
        /// 备用
        /// </summary>
        public string PermissionMan
        {
            set { permissionman = value; }
            get { return permissionman; }
        }
        /// <summary>
        /// 备用
        /// </summary>
        public decimal TotalmaxMoney
        {
            set { totalmaxMoney = value; }
            get { return totalmaxMoney; }
        }
        /// <summary>
        /// 备用
        /// </summary>
        public decimal TotalcomityMoney
        {
            set { totalcomityMoney = value; }
            get { return totalcomityMoney; }
        }
        /// <summary>
        /// 备用
        /// </summary>
        public decimal TotalcomityPv
        {
            set { totalcomityPv = value; }
            get { return totalcomityPv; }
        }
        /// <summary>
        /// 备用
        /// </summary>
        public decimal TotalindentMoney
        {
            set { totalindentMoney = value; }
            get { return totalindentMoney; }
        }
        /// <summary>
        /// 备用
        /// </summary>
        public decimal TotalindentPv
        {
            set { totalindentPv = value; }
            get { return totalindentPv; }
        }
        /// <summary>
        /// 备用
        /// </summary>
        public decimal TotalchangeMoney
        {
            set { totalchangeMoney = value; }
            get { return totalchangeMoney; }
        }
        /// <summary>
        /// 备用
        /// </summary>
        public decimal Totalchangepv
        {
            set { totalchangepv = value; }
            get { return totalchangepv; }
        }
        /// <summary>
        /// 找回密码答案
        /// </summary>
        public string Answer
        {
            set { answer = value; }
            get { return answer; }
        }
        /// <summary>
        /// 找回密码问题
        /// </summary>
        public string Question
        {
            set { question = value; }
            get { return question; }
        }
        /// <summary>
        /// 照片路径
        /// </summary>
        public string PhotoPath
        {
            set { photoPath = value; }
            get { return photoPath; }
        }
        /// <summary>
        /// 照片高度
        /// </summary>
        public int PhotoH
        {
            set { photoh = value; }
            get { return photoh; }
        }
        /// <summary>
        /// 照片宽度
        /// </summary>
        public int PhotoW
        {
            set { photow = value; }
            get { return photow; }
        }

        /// <summary>
        /// 登录语言
        /// </summary>
        public int Language
        {
            set { language = value; }
            get { return language; }
        }
        /// <summary>
        /// 所用币种
        /// </summary>
        public int Currency
        {
            set { currency = value; }
            get { return currency; }
        }
        /// <summary>
        /// 店铺所属城市
        /// </summary>
        public string DianCity
        {
            set { dianCity = value; }
            get { return dianCity; }
        }
        /// <summary>
        /// 店铺所属国家
        /// </summary>
        public string DCountry
        {
            set { dcountry = value; }
            get { return dcountry; }
        }
        /// <summary>
        /// 操作者IP
        /// </summary>
        public string OperateIp
        {
            set { operateIp = value; }
            get { return operateIp; }
        }
        /// <summary>
        /// 操作者编号
        /// </summary>
        public string OperateNum
        {
            set { operateNum = value; }
            get { return operateNum; }
        }
        #endregion Model

    }
}