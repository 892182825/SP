using System;
namespace Model
{
    /// <summary>
    /// 实体类MemberInfoModel 
    ///宋俊
    ///2009-8-27
    ///修改人：汪华
    ///修改时间：2009-09-01
    ///</summary>
    [Serializable]
    public class MemberInfoModel
    {
        /// <summary>
        /// 会员基本信息
        /// </summary>
        public MemberInfoModel()
        { }
        #region Model
        private int id;
        private string number;
        private string placement;
        private string direct;
        private int expectNum;
        private string orderID;
        private string storeID;
        private string name;
        private string petName;
        private string loginPass;
        private string advPass;
        private int levelInt;
        private DateTime registerDate;
        private DateTime birthday;
        private int sex;
        private string homeTele;
        private string officeTele;
        private string mobileTele;
        private string faxTele;
        private string cPCCode;
        private CityModel city = new CityModel();
        private string address;
        private string postalCode;
        private Bsco_PaperType paperType = new Bsco_PaperType();
        private string papertypecode;
        private string paperNumber;
        private string bankAddress;
        private string bankCard;
        private string bankBook;
        private string remark;
        private int release;
        private decimal jackpot;
        private decimal ectOut;
        private decimal memberships;
        private string email;
        private string bCPCCode;
        private CityModel bankCity = new CityModel();
        private int isactive;
        private int flag;
        private string answer;
        private string question;
        private string photoPath;
        private int vipcard;
        private int isBatch;
        private int language;
        private string error;
        private int district;
        private DateTime lastLoginDate;
        private string operateIp;
        private string operaterNum;
        private string changeInfo;
        private string bankCode;
        private string bankbranchname;
        private string assister;
        private DateTime activeDate;
        private int bankID;
        private decimal zzye;
        
        //-----
        private string zhifubao;
        private string weixin;

        public decimal Zzye
        {
            get { return zzye; }
            set { zzye = value; }
        }
        public string Zhifubao
        {
            get { return zhifubao; }
            set { zhifubao = value; }
        }
        public string Weixin
        {
            get { return weixin; }
            set { weixin = value; }
        }

        public int BankID
        {
            get { return bankID; }
            set { bankID = value; }
        }

        public DateTime ActiveDate
        {
            get { return activeDate; }
            set { activeDate = value; }
        }

        /// <summary>
        /// 协助人
        /// </summary>
        public string Assister
        {
            get { return assister; }
            set { assister = value; }
        }
        private decimal totalRemittances;

        public decimal TotalRemittances
        {
            get { return totalRemittances; }
            set { totalRemittances = value; }
        }
        private decimal totalDefray;

        public decimal TotalDefray
        {
            get { return totalDefray; }
            set { totalDefray = value; }
        }
        private int displayTimeType;

        public int DisplayTimeType
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
        private int defaultNumber;

        public int DefaultNumber
        {
            get { return defaultNumber; }
            set { defaultNumber = value; }
        }
        private int advCount;

        public int AdvCount
        {
            get { return advCount; }
            set { advCount = value; }
        }
        private int xuHao;

        public int XuHao
        {
            get { return xuHao; }
            set { xuHao = value; }
        }
        private DateTime advTime;

        public DateTime AdvTime
        {
            get { return advTime; }
            set { advTime = value; }
        }

        /// <summary>
        /// 分行名称
        /// </summary>
        public string Bankbranchname
        {
            get { return bankbranchname; }
            set { bankbranchname = value; }
        }
        private MemberBankModel bank = new MemberBankModel();
        private BscoLevelModel level = new BscoLevelModel();

        public string Papertypecode
        {
            get { return papertypecode; }
            set { papertypecode = value; }
        }

        public BscoLevelModel Level
        {
            get { return level; }
            set { level = value; }
        }

        public MemberBankModel Bank
        {
            get { return bank; }
            set { bank = value; }
        }

        public string BankCode
        {
            get { return bankCode; }
            set { bankCode = value; }
        }

        public CityModel BankCity
        {
            get { return bankCity; }
            set { bankCity = value; }
        }

        public string BCPCCode
        {
            get { return bCPCCode; }
            set { bCPCCode = value; }
        }

        public CityModel City
        {
            get { return city; }
            set { city = value; }
        }

        private decimal totalMoney;

        public decimal TotalMoney
        {
            get { return totalMoney; }
            set { totalMoney = value; }
        }
        private decimal totalPv;

        public decimal TotalPv
        {
            get { return totalPv; }
            set { totalPv = value; }
        }
        private int defrayType;

        public int DefrayType
        {
            get { return defrayType; }
            set { defrayType = value; }
        }
        private int orderType;

        public int OrderType
        {
            get { return orderType; }
            set { orderType = value; }
        }
        private int defrayState;

        public int DefrayState
        {
            get { return defrayState; }
            set { defrayState = value; }
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        public string ChangeInfo
        {
            get { return changeInfo; }
            set { changeInfo = value; }
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
        /// 会员编号
        /// </summary>
        public string Number
        {
            set { number = value; }
            get { return number; }
        }
        /// <summary>
        /// 安置人编号
        /// </summary>
        public string Placement
        {
            set { placement = value; }
            get { return placement; }
        }
        /// <summary>
        /// 推荐人编号
        /// </summary>
        public string Direct
        {
            set { direct = value; }
            get { return direct; }
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
        /// 首次报单
        /// </summary>
        public string OrderID
        {
            set { orderID = value; }
            get { return orderID; }
        }
        /// <summary>
        /// 所属店铺编号
        /// </summary>
        public string StoreID
        {
            set { storeID = value; }
            get { return storeID; }
        }
        /// <summary>
        /// 本人真实姓名
        /// </summary>
        public string Name
        {
            set { name = value; }
            get { return name; }
        }
        /// <summary>
        /// 昵称
        /// </summary>
        public string PetName
        {
            set { petName = value; }
            get { return petName; }
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
        /// 电子账户密码
        /// </summary>
        public string AdvPass
        {
            set { advPass = value; }
            get { return advPass; }
        }

        /// <summary>
        /// 级别数字
        /// </summary>
        public int LevelInt
        {
            set { levelInt = value; }
            get { return levelInt; }
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
        /// 出生日期
        /// </summary>
        public DateTime Birthday
        {
            set { birthday = value; }
            get { return birthday; }
        }
        /// <summary>
        /// 性别：0：女，1：男
        /// </summary>
        public int Sex
        {
            set { sex = value; }
            get { return sex; }
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
        /// 手机号码
        /// </summary>
        public string MobileTele
        {
            set { mobileTele = value; }
            get { return mobileTele; }
        }

        public string FaxTele
        {
            set { faxTele = value; }
            get { return faxTele; }
        }

        /// <summary>
        /// 所属国家或地区
        /// </summary>
        public string CPCCode
        {
            set { cPCCode = value; }
            get { return cPCCode; }
        }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address
        {
            set { address = value; }
            get { return address; }
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
        /// 证件类型
        /// </summary>
        public Bsco_PaperType PaperType
        {
            set { paperType = value; }
            get { return paperType; }
        }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string PaperNumber
        {
            set { paperNumber = value; }
            get { return paperNumber; }
        }

        /// <summary>
        /// 银行详细地址
        /// </summary>
        public string BankAddress
        {
            set { bankAddress = value; }
            get { return bankAddress; }
        }
        /// <summary>
        /// 卡号
        /// </summary>
        public string BankCard
        {
            set { bankCard = value; }
            get { return bankCard; }
        }
        /// <summary>
        /// 开户名
        /// </summary>
        public string BankBook
        {
            set { bankBook = value; }
            get { return bankBook; }
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
        /// 发放奖金的标识
        /// </summary>
        public int Release
        {
            set { release = value; }
            get { return release; }
        }
        /// <summary>
        /// 累积的奖金和其它会员转账来的金额(不包括未汇出款的金额)
        /// </summary>
        public decimal Jackpot
        {
            set { jackpot = value; }
            get { return jackpot; }
        }
        /// <summary>
        /// 奖金转出累计
        /// </summary>
        public decimal EctOut
        {
            set { ectOut = value; }
            get { return ectOut; }
        }
        /// <summary>
        /// 会员申请要发放的奖金数在公司审核时应该进减去操作
        /// </summary>
        public decimal Memberships
        {
            set { memberships = value; }
            get { return memberships; }
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
        /// 是否激活
        /// </summary>
        public int IsActive
        {
            set { isactive = value; }
            get { return isactive; }
        }
        /// <summary>
        /// 标识
        /// </summary>
        public int Flag
        {
            set { flag = value; }
            get { return flag; }
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
        /// VIP会员卡号
        /// </summary>
        public int VIPCard
        {
            set { vipcard = value; }
            get { return vipcard; }
        }
        /// <summary>
        /// 是否批量注册
        /// </summary>
        public int IsBatch
        {
            set { isBatch = value; }
            get { return isBatch; }
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
        /// 错误信息
        /// </summary>
        public string Error
        {
            set { error = value; }
            get { return error; }
        }
        /// <summary>
        /// 1是左区，2是右区
        /// </summary>
        public int District
        {
            set { district = value; }
            get { return district; }
        }
        /// <summary>
        /// 记录会员最后一次的访问时间
        /// </summary>
        public DateTime LastLoginDate
        {
            set { lastLoginDate = value; }
            get { return lastLoginDate; }
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
        public string OperaterNum
        {
            set { operaterNum = value; }
            get { return operaterNum; }
        }
        #endregion Model

    }
}

