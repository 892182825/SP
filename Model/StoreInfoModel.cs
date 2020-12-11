using System;
namespace Model
{
    /// <summary>
    /// 实体类StoreInfoModel 
    /// 功能：店铺基本信息表
    ///作者：宋俊
    ///开发时间：2009-8-27
    /// </summary>
    [Serializable]
    public class StoreInfoModel
    {
        /// <summary>
        /// 店铺基本信息表
        /// </summary>
        public StoreInfoModel()
        { }
        #region Model
        private int id;
        private string number;
        private string direct;
        private string storeId;
        private string sCPPCode;

        public string SCPPCode
        {
            get { return sCPPCode; }
            set { sCPPCode = value; }
        }

        private string name;
        private int expectNum;
        private string loginPass;
        private string storeName;
        private string cPCCode;
        private string storeAddress;
        private string postalCode;
        private string homeTele;
        private string officeTele;
        private string mobileTele;
        private string faxTele;
        private MemberBankModel bank = new MemberBankModel();
        private string bankCode;
        private string bankCard;
        private string email;
        private string netAddress;
        private string remark;
        private DateTime registerDate;
        private int storeLevelInt;
        private decimal farearea;
        private decimal totalordergoodMoney;
        private decimal totalaccountMoney;
        private decimal totalInvestMoney;
        private decimal turnoverMoney;
        private decimal turnovergoodsMoney;
        private decimal otherMoney;
        private string permissionman;
        private decimal totalmaxMoney;
        private string answer;
        private string question;
        private string photoPath;
        private int language;
        private int currency;
        private DateTime lastloginDate;
        private string sCPCCode;
        private CityModel storeCity = new CityModel();
        private CityModel city = new CityModel();

        private string bankbranchname;

        public string Bankbranchname
        {
            get { return bankbranchname; }
            set { bankbranchname = value; }
        }

        private int bankID;

        public int BankID
        {
            get { return bankID; }
            set { bankID = value; }
        }

        public CityModel City
        {
            get { return city; }
            set { city = value; }
        }
        private string operateIp;
        private string operateNum;

        private int defaultStore;

        public int DefaultStore
        {
            get { return defaultStore; }
            set { defaultStore = value; }
        }
        private string branchNumber;

        public string BranchNumber
        {
            get { return branchNumber; }
            set { branchNumber = value; }
        }
        private DateTime displayTimeType;

        public DateTime DisplayTimeType
        {
            get { return displayTimeType; }
            set { displayTimeType = value; }
        }
        private int gongGaoType;

        public int GongGaoType
        {
            get { return gongGaoType; }
            set { gongGaoType = value; }
        }
        private int advCount;

        public int AdvCount
        {
            get { return advCount; }
            set { advCount = value; }
        }
        private DateTime advTime;

        public DateTime AdvTime
        {
            get { return advTime; }
            set { advTime = value; }
        }
        private string advPass;

        public string AdvPass
        {
            get { return advPass; }
            set { advPass = value; }
        }
        private int storeState;

        public int StoreState
        {
            get { return storeState; }
            set { storeState = value; }
        }

        public CityModel StoreCity
        {
            get { return storeCity; }
            set { storeCity = value; }
        }

        public string SCPCCode
        {
            get { return sCPCCode; }
            set { sCPCCode = value; }
        }

        public string BankCode
        {
            get { return bankCode; }
            set { bankCode = value; }
        }

        public string CPCCode
        {
            get { return cPCCode; }
            set { cPCCode = value; }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            set { id = value; }
            get { return id; }
        }
        /// <summary>
        /// 会员编号
        /// </summary>
        public string Number
        {
            set { number = value; }
            get { return number; }
        }
        /// <summary>
        /// 推荐这家的会员编号
        /// </summary>
        public string Direct
        {
            set { direct = value; }
            get { return direct; }
        }
        /// <summary>
        /// 店编号，不可重复
        /// </summary>
        public string StoreID
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
        /// 建店期数
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
        /// 店名称
        /// </summary>
        public string StoreName
        {
            set { storeName = value; }
            get { return storeName; }
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
        ///移动电话
        /// </summary>
        public string MobileTele
        {
            set { mobileTele = value; }
            get { return mobileTele; }
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
        public MemberBankModel Bank
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
        /// 电子信箱
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
        /// 注册日期
        /// </summary>
        public DateTime RegisterDate
        {
            set { registerDate = value; }
            get { return registerDate; }
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
        /// 总的订货额
        /// </summary>
        public decimal TotalordergoodMoney
        {
            set { totalordergoodMoney = value; }
            get { return totalordergoodMoney; }
        }
        /// <summary>
        ///总汇款额
        /// </summary>
        public decimal TotalaccountMoney
        {
            set { totalaccountMoney = value; }
            get { return totalaccountMoney; }
        }
        /// <summary>
        /// 总投资的钱，不用于报单和订货
        /// </summary>
        public decimal TotalInvestMoney
        {
            set { totalInvestMoney = value; }
            get { return totalInvestMoney; }
        }
        /// <summary>
        /// 周转款
        /// </summary>
        public decimal TurnOverMoney
        {
            set { turnoverMoney = value; }
            get { return turnoverMoney; }
        }
        /// <summary>
        ///周转款所订的货
        /// </summary>
        public decimal TurnOverGoodsMoney
        {
            set { turnovergoodsMoney = value; }
            get { return turnovergoodsMoney; }
        }
        /// <summary>
        /// 其他款项
        /// </summary>
        public decimal OtherMoney
        {
            set { otherMoney = value; }
            get { return otherMoney; }
        }
        /// <summary>
        /// 权限人
        /// </summary>
        public string PermissionMan
        {
            set { permissionman = value; }
            get { return permissionman; }
        }
        /// <summary>
        ///店铺没钱时,最大上限报单额
        /// </summary>
        public decimal TotalMaxMoney
        {
            set { totalmaxMoney = value; }
            get { return totalmaxMoney; }
        }
        /// <summary>
        /// 店铺订货总金额
        /// </summary>
        private decimal totalIndexMoney;

        public decimal TotalIndexMoney
        {
            get { return totalIndexMoney; }
            set { totalIndexMoney = value; }
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
        ///用来记录店铺最后一次的访问时间
        /// </summary>
        public DateTime LastloginDate
        {
            set { lastloginDate = value; }
            get { return lastloginDate; }
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
            get { return operateNum; }
            set { operateNum = value; }
        }

    }
        #endregion
}