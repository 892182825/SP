using System;
namespace Model
{
    /// <summary>
    /// 实体类OrderFinalModel   
    ///</summary>
    [Serializable]
    public class OrderFinalModel
    {
        /// <summary>
        /// 会员基本信息
        /// </summary>
        public OrderFinalModel()
        { }
        #region Model
        private int id;
        private string number;
        private string placement;
        private string direct;
        private int expectNum;
        private int expectNum2;
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
        private string sexStr;
        private string homeTele;
        private string officeTele;
        private string mobileTele;
        private string faxTele;
        private string cPCCode;
        private CityModel city = new CityModel();
        private string address;
        private string postalCode;
        private Bsco_PaperType paperType=new Bsco_PaperType();
        private string papertypecode;
        private string paperNumber;
        private string bankAddress;
        private string bankCard;
        private string bankBook;
        private string remark;
        private int release;
        private decimal ectjackpot;
        private decimal jackpot;
        private decimal ectdeclarations;
        private decimal ectPay;
        private decimal releaseMoney;
        private decimal ectOut;
        private decimal memberships;
        private int healthy;
        private int knowway;
        private string email;
        private string bCPCCode;
        private CityModel bankCity = new CityModel();
        private int isactive;
        private int flag;
        private string answer;
        private string question;
        private string photoPath;
        private int photoh;
        private int photow;
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

        public string Assister
        {
            get { return assister; }
            set { assister = value; }
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

        private int jjtx;
        public int JjTx
        {
            get { return jjtx; }
            set { jjtx = value; }
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
        /// 期数2（备用）
        /// </summary>
        public int ExpectNum2
        {
            set { expectNum2 = value; }
            get { return expectNum2; }
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
        /// 返回性别字符串：男or女
        /// </summary>
        public string SexStr
        {
            get { return sexStr; }
            set { sexStr = value; }
        }
        /// <summary>
        /// 家庭电话
        /// </summary>

        private string homeQuhao;
        public string HomeQuhao
        {
            set { homeQuhao = value; }
            get { return homeQuhao; }
        }

        public string HomeTele
        {
            set { homeTele = value; }
            get { return homeTele; }
        }
        /// <summary>
        /// 办公电话
        /// </summary>

        private string officeQuhao;
        public string OfficeQuhao
        {
            set { officeQuhao = value; }
            get { return officeQuhao; }
        }

        public string OfficeTele
        {
            set { officeTele = value; }
            get { return officeTele; }
        }

        private string officeFjh;
        public string OfficeFjh
        {
            set { officeFjh = value; }
            get { return officeFjh; }
        }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileTele
        {
            set { mobileTele = value; }
            get { return mobileTele; }
        }
        /// <summary>
        /// 传真电话
        /// </summary>
        private string faxQuhao;
        public string FaxQuhao
        {
            set { faxQuhao = value; }
            get { return faxQuhao; }
        }

        public string FaxTele
        {
            set { faxTele = value; }
            get { return faxTele; }
        }

        private string faxFjh;
        public string FaxFjh
        {
            set { faxFjh = value; }
            get { return faxFjh; }
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
        /// 累计奖金何日其他会员转账来的金额（包括未会出的金额）
        /// </summary>
        public decimal ECTJackpot
        {
            set { ectjackpot = value; }
            get { return ectjackpot; }
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
        /// 用奖金或转账报单的已用报单额(包括未支付的报单)
        /// </summary>
        public decimal ECTDeclarations
        {
            set { ectdeclarations = value; }
            get { return ectdeclarations; }
        }
        /// <summary>
        /// 用奖金或转账报单的已用报单额(不包括未支付的报单)
        /// </summary>
        public decimal EctPay
        {
            set { ectPay = value; }
            get { return ectPay; }
        }
        /// <summary>
        /// 发放现金的累计
        /// </summary>
        public decimal ReleaseMoney
        {
            set { releaseMoney = value; }
            get { return releaseMoney; }
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
        /// 健康状况(1:良好;2:一般;3:差)
        /// </summary>
        public int Healthy
        {
            set { healthy = value; }
            get { return healthy; }
        }
        /// <summary>
        /// 知晓方式(4:朋友)
        /// </summary>
        public int KnowWay
        {
            set { knowway = value; }
            get { return knowway; }
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
        /// 长片高度
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

        private decimal investJB;
        /// <summary>
        /// 投资石斛积分数量
        /// </summary>
        public decimal InvestJB
        {
            get { return investJB; }
            set { investJB = value; }
        }

        private decimal priceJB;
        /// <summary>
        /// 石斛积分时价
        /// </summary>
        public decimal PriceJB
        {
            get { return priceJB; }
            set { priceJB = value; }
        }
        #endregion Model

        #region Model2
       
       
        
        private decimal carryMoney;
        private int orderExpect;
        private int payExpect;
        private int isagain;
        private DateTime orderDate;
        private string err;
        
        private int surefare;
        
        private string affluxBank;
        private string affluxacCount;
        private string consignee;
        private string ccpccode;
        private CityModel conCity = new CityModel();
        private string conAddress;
        private string conZipCode;
        private string conTelPhone;
        private string conMobilPhone;
        private int isFavour;
        private int favour;
        private string conPost;
       
        private int isretail;
        private decimal declareMoney;
        private decimal payMoney;
        private int paycurrency;
        private int standardcurrency;
        private decimal standardcurrencyMoney;
       
        private string operateNumber;
        private int type;
        private string remittancesId;
        private string electronicaccountId;
        
        private int isreceiVables;
        private decimal paymentMoney;
        private DateTime receivablesDate;
        private string cCPCCode;

        public string CCPCCode
        {
            get { return cCPCCode; }
            set { cCPCCode = value; }
        }


        public CityModel ConCity
        {
            get { return conCity; }
            set { conCity = value; }
        }

        public string Ccpccode
        {
            get { return ccpccode; }
            set { ccpccode = value; }
        }

        
       
        
        /// <summary>
        /// 运费
        /// </summary>
        public decimal CarryMoney
        {
            set { carryMoney = value; }
            get { return carryMoney; }
        }
        /// <summary>
        /// 报单期数
        /// </summary>
        public int OrderExpect
        {
            set { orderExpect = value; }
            get { return orderExpect; }
        }
        /// <summary>
        /// 付款期数
        /// </summary>
        public int PayExpect
        {
            set { payExpect = value; }
            get { return payExpect; }
        }
        /// <summary>
        /// 是否重复消费 1,0  是否是团购消费5,6
        /// </summary>
        public int IsAgain
        {
            set { isagain = value; }
            get { return isagain; }
        }
        /// <summary>
        /// 订单时间
        /// </summary>
        public DateTime OrderDate
        {
            set { orderDate = value; }
            get { return orderDate; }
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Err
        {
            set { err = value; }
            get { return err; }
        }
        
        /// <summary>
        /// 确认费（元角分匹配专用）
        /// </summary>
        public int SureFare
        {
            set { surefare = value; }
            get { return surefare; }
        }
        
        /// <summary>
        /// 汇入银行
        /// </summary>
        public string AffluxBank
        {
            set { affluxBank = value; }
            get { return affluxBank; }
        }
        /// <summary>
        /// 汇入账号
        /// </summary>
        public string AffluxacCount
        {
            set { affluxacCount = value; }
            get { return affluxacCount; }
        }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee
        {
            set { consignee = value; }
            get { return consignee; }
        }

        /// <summary>
        /// 收货人详细地址
        /// </summary>
        public string ConAddress
        {
            set { conAddress = value; }
            get { return conAddress; }
        }
        /// <summary>
        /// 收货人所在地址邮编
        /// </summary>
        public string ConZipCode
        {
            set { conZipCode = value; }
            get { return conZipCode; }
        }
        /// <summary>
        /// 收货人固定电话
        /// </summary>
        public string ConTelPhone
        {
            set { conTelPhone = value; }
            get { return conTelPhone; }
        }
        /// <summary>
        /// 收货人移动电话
        /// </summary>
        public string ConMobilPhone
        {
            set { conMobilPhone = value; }
            get { return conMobilPhone; }
        }
        /// <summary>
        /// 备用
        /// </summary>
        public int IsFavour
        {
            set { isFavour = value; }
            get { return isFavour; }
        }
        /// <summary>
        /// 单数
        /// </summary>
        public int Favour
        {
            set { favour = value; }
            get { return favour; }
        }
        /// <summary>
        /// 收货人邮箱
        /// </summary>
        public string ConPost
        {
            set { conPost = value; }
            get { return conPost; }
        }
        
        /// <summary>
        /// 备用
        /// </summary>
        public int IsRetail
        {
            set { isretail = value; }
            get { return isretail; }
        }
        /// <summary>
        /// 申报金额
        /// </summary>
        public decimal DeclareMoney
        {
            set { declareMoney = value; }
            get { return declareMoney; }
        }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal PayMoney
        {
            set { payMoney = value; }
            get { return payMoney; }
        }
        /// <summary>
        /// 支付币种代码
        /// </summary>
        public int PayCurrency
        {
            set { paycurrency = value; }
            get { return paycurrency; }
        }
        /// <summary>
        /// 标准币种代码
        /// </summary>
        public int StandardCurrency
        {
            set { standardcurrency = value; }
            get { return standardcurrency; }
        }
        /// <summary>
        /// 标准币种金额
        /// </summary>
        public decimal StandardcurrencyMoney
        {
            set { standardcurrencyMoney = value; }
            get { return standardcurrencyMoney; }
        }
        
        /// <summary>
        /// 操作者编号
        /// </summary>
        public string OperateNumber
        {
            set { operateNumber = value; }
            get { return operateNumber; }
        }
        /// <summary>
        /// 1自提，2邮寄，3空运，4陆运，5水运，6其它
        /// </summary>
        public int Type
        {
            set { type = value; }
            get { return type; }
        }
        /// <summary>
        /// 电子钱包支付时给店铺打款的汇款号
        /// </summary>
        public string RemittancesId
        {
            set { remittancesId = value; }
            get { return remittancesId; }
        }
        /// <summary>
        /// 所用电子账户者编号
        /// </summary>
        public string ElectronicaccountId
        {
            set { electronicaccountId = value; }
            get { return electronicaccountId; }
        }
       
        /// <summary>
        /// 是否收款
        /// </summary>
        public int IsreceiVables
        {
            set { isreceiVables = value; }
            get { return isreceiVables; }
        }
        /// <summary>
        /// 收款金额
        /// </summary>
        public decimal PaymentMoney
        {
            set { paymentMoney = value; }
            get { return paymentMoney; }
        }
        /// <summary>
        /// 收款时间
        /// </summary>
        public DateTime ReceivablesDate
        {
            set { receivablesDate = value; }
            get { return receivablesDate; }
        }

        private decimal enoughProductMoney;

        /// <summary>
        /// 有货金额
        /// </summary>
        public decimal EnoughProductMoney
        {
            get { return enoughProductMoney; }
            set { enoughProductMoney = value; }
        }

        private decimal lackProductMoney;

        /// <summary>
        /// 无货金额
        /// </summary>
        public decimal LackProductMoney
        {
            get { return lackProductMoney; }
            set { lackProductMoney = value; }
        }

        //收货途径
        private int sendWay;

        public int SendWay
        {
            get { return sendWay; }
            set { sendWay = value; }
        }

        #endregion Model2

        private string productIDList;

        public string ProductIDList
        {
            set { productIDList = value; }
            get { return productIDList; }
        }

        private string quantityList;

        public string QuantityList
        {
            set { quantityList = value; }
            get { return quantityList; }
        }

        private string notEnoughProductList;

        public string NotEnoughProductList
        {
            set { notEnoughProductList = value; }
            get { return notEnoughProductList; }
        }

    }
}