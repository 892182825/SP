using System;

/*
 * ���ܣ�       ʵ����UnauditedStoreInfoModel 
 * �����ߣ�     ��  ��
 * ����ʱ�䣺   2009-8-27
 * �޸��ߣ�     ��  ��
 * �޸�ʱ�䣺   2009-08-31
 */


namespace Model
{
    [Serializable]
    public class UnauditedStoreInfoModel
    {
        /// <summary>
        /// δ��˵�����Ϣ
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
        /// �곤��Ա���
        /// </summary>
        public string Number
        {
            set { number = value; }
            get { return number; }
        }
        /// <summary>
        /// �Ƽ���Ļ�Ա���
        /// </summary>
        public string Direct
        {
            set { direct = value; }
            get { return direct; }
        }
        /// <summary>
        /// ���̱��
        /// </summary>
        public string StoreId
        {
            set { storeId = value; }
            get { return storeId; }
        }

        /// <summary>
        /// �곤����
        /// </summary>
        public string Name
        {
            set { name = value; }
            get { return name; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int ExpectNum
        {
            set { expectNum = value; }
            get { return expectNum; }
        }
        /// <summary>
        /// ��¼����
        /// </summary>
        public string LoginPass
        {
            set { loginPass = value; }
            get { return loginPass; }
        }
        /// <summary>
        /// �����˻����루δ�ã�
        /// </summary>
        public string AdvPass
        {
            set { advPass = value; }
            get { return advPass; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string StoreName
        {
            set { storeName = value; }
            get { return storeName; }
        }
        /// <summary>
        /// �곤��������
        /// </summary>
        public string Country
        {
            set { country = value; }
            get { return country; }
        }
        /// <summary>
        /// �곤����ʡ��
        /// </summary>
        public string Province
        {
            set { province = value; }
            get { return province; }
        }
        /// <summary>
        /// �곤��������
        /// </summary>
        public string City
        {
            set { city = value; }
            get { return city; }
        }
        /// <summary>
        /// �곤������/��
        /// </summary>
        public string Xian
        {
            set { xian = value; }
            get { return xian; }
        }
        /// <summary>
        /// �곤��ϸ��ַ
        /// </summary>
        public string StoreAddress
        {
            set { storeAddress = value; }
            get { return storeAddress; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string PostalCode
        {
            set { postalCode = value; }
            get { return postalCode; }
        }
        /// <summary>
        /// ��ͥ�绰
        /// </summary>
        public string HomeTele
        {
            set { homeTele = value; }
            get { return homeTele; }
        }
        /// <summary>
        /// �칫�绰
        /// </summary>
        public string OfficeTele
        {
            set { officeTele = value; }
            get { return officeTele; }
        }
        /// <summary>
        /// ����绰
        /// </summary>
        public string FaxTele
        {
            set { faxTele = value; }
            get { return faxTele; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string Bank
        {
            set { bank = value; }
            get { return bank; }
        }
        /// <summary>
        /// ���п���
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
        /// ��ַ
        /// </summary>
        public string NetAddress
        {
            set { netAddress = value; }
            get { return netAddress; }
        }
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            set { remark = value; }
            get { return remark; }
        }
        /// <summary>
        /// ע��ʱ��
        /// </summary>
        public DateTime RegisterDate
        {
            set { registerDate = value; }
            get { return registerDate; }
        }
        /// <summary>
        /// �����ַ���
        /// </summary>
        public string StoreLevelStr
        {
            set { storeLevelStr = value; }
            get { return storeLevelStr; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public int StoreLevelInt
        {
            set { storeLevelInt = value; }
            get { return storeLevelInt; }
        }
        /// <summary>
        /// ��Ӫ���
        /// </summary>
        public decimal FareArea
        {
            set { farearea = value; }
            get { return farearea; }
        }
        /// <summary>
        /// ��ӪƷ��
        /// </summary>
        public string FareBreed
        {
            set { fareBreed = value; }
            get { return fareBreed; }
        }
        /// <summary>
        /// Υ�����
        /// </summary>
        public int OffendTimes
        {
            set { offendTimes = value; }
            get { return offendTimes; }
        }
        /// <summary>
        /// ��Ȩ����
        /// </summary>
        public int AccreditExpectNum
        {
            set { accreditExpectNum = value; }
            get { return accreditExpectNum; }
        }
        /// <summary>
        /// �ܵĶ�����
        /// </summary>
        public decimal TotalordergoodsMoney
        {
            set { totalordergoodsMoney = value; }
            get { return totalordergoodsMoney; }
        }
        /// <summary>
        /// �ܵı�����
        /// </summary>
        public decimal TotalmemberorderMoney
        {
            set { totalmemberorderMoney = value; }
            get { return totalmemberorderMoney; }
        }
        /// <summary>
        /// �ܻؿ��
        /// </summary>
        public decimal TotalaccountMoney
        {
            set { totalaccountMoney = value; }
            get { return totalaccountMoney; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public decimal TotalinvestMoney
        {
            set { totalinvestMoney = value; }
            get { return totalinvestMoney; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int StorageScalar
        {
            set { storagescalar = value; }
            get { return storagescalar; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string PermissionMan
        {
            set { permissionman = value; }
            get { return permissionman; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public decimal TotalmaxMoney
        {
            set { totalmaxMoney = value; }
            get { return totalmaxMoney; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public decimal TotalcomityMoney
        {
            set { totalcomityMoney = value; }
            get { return totalcomityMoney; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public decimal TotalcomityPv
        {
            set { totalcomityPv = value; }
            get { return totalcomityPv; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public decimal TotalindentMoney
        {
            set { totalindentMoney = value; }
            get { return totalindentMoney; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public decimal TotalindentPv
        {
            set { totalindentPv = value; }
            get { return totalindentPv; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public decimal TotalchangeMoney
        {
            set { totalchangeMoney = value; }
            get { return totalchangeMoney; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public decimal Totalchangepv
        {
            set { totalchangepv = value; }
            get { return totalchangepv; }
        }
        /// <summary>
        /// �һ������
        /// </summary>
        public string Answer
        {
            set { answer = value; }
            get { return answer; }
        }
        /// <summary>
        /// �һ���������
        /// </summary>
        public string Question
        {
            set { question = value; }
            get { return question; }
        }
        /// <summary>
        /// ��Ƭ·��
        /// </summary>
        public string PhotoPath
        {
            set { photoPath = value; }
            get { return photoPath; }
        }
        /// <summary>
        /// ��Ƭ�߶�
        /// </summary>
        public int PhotoH
        {
            set { photoh = value; }
            get { return photoh; }
        }
        /// <summary>
        /// ��Ƭ���
        /// </summary>
        public int PhotoW
        {
            set { photow = value; }
            get { return photow; }
        }

        /// <summary>
        /// ��¼����
        /// </summary>
        public int Language
        {
            set { language = value; }
            get { return language; }
        }
        /// <summary>
        /// ���ñ���
        /// </summary>
        public int Currency
        {
            set { currency = value; }
            get { return currency; }
        }
        /// <summary>
        /// ������������
        /// </summary>
        public string DianCity
        {
            set { dianCity = value; }
            get { return dianCity; }
        }
        /// <summary>
        /// ������������
        /// </summary>
        public string DCountry
        {
            set { dcountry = value; }
            get { return dcountry; }
        }
        /// <summary>
        /// ������IP
        /// </summary>
        public string OperateIp
        {
            set { operateIp = value; }
            get { return operateIp; }
        }
        /// <summary>
        /// �����߱��
        /// </summary>
        public string OperateNum
        {
            set { operateNum = value; }
            get { return operateNum; }
        }
        #endregion Model

    }
}