using System;
/// <summary>
	///功能： 会员电子转账(汇款)明细表
	///作者：songjun
	///开发时间：2009-8-27
	/// </summary>

namespace Model
{
    [Serializable]
    public class ECRemitDetailModel
    {
        ///会员电子转账(汇款)明细表
        public ECRemitDetailModel()
        { }

        #region Model
        private int id;
        private string operatorid;
        private string remitid;
        private string gatheringid;
        private double remitmoney;
        private string currency;
        private int paymentexpectnum;
        private DateTime paymentdate;
        private int collectionexpectnum;
        private DateTime collectiondays;
        private double collectionmoney;
        private int paymentmethod;
        private int confirmed;
        private string remitnumber;
        private DateTime remitdate;
        private string remitbank;
        private string remitcard;
        private string sbouchementbank;
        private string abouchementcard;
        private string remitter;
        private string identitycard;
        private string remitterremark;
        private string abouchementremark;
        private int isaffirm;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { id = value; }
            get { return id; }
        }
        /// <summary>
        /// 经办会员编号
        /// </summary>
        public string OperatorID
        {
            set { operatorid = value; }
            get { return operatorid; }
        }
        /// <summary>
        ///汇款会员编号
        /// </summary>
        public string RemitID
        {
            set { remitid = value; }
            get { return remitid; }
        }
        /// <summary>
        ///  收款会员编号
        /// </summary>
        public string GatheringID
        {
            set { gatheringid = value; }
            get { return gatheringid; }
        }
        /// <summary>
        /// 汇款金额
        /// </summary>
        public double RemitMoney
        {
            set { remitmoney = value; }
            get { return remitmoney; }
        }
        /// <summary>
        /// 币种
        /// </summary>
        public string Currency
        {
            set { currency = value; }
            get { return currency; }
        }
        /// <summary>
        /// ָ付款期数
        /// </summary>
        public int PayMentExpectNum
        {
            set { paymentexpectnum = value; }
            get { return paymentexpectnum; }
        }
        /// <summary>
        ///付款日期
        /// </summary>
        public DateTime PayMentDate
        {
            set { paymentdate = value; }
            get { return paymentdate; }
        }
        /// <summary>
        ///收款期数
        /// </summary>
        public int CollectionExpectNum
        {
            set { collectionexpectnum = value; }
            get { return collectionexpectnum; }
        }
        /// <summary>
        ///收款日期
        /// </summary>
        public DateTime CollectionDays
        {
            set { collectiondays = value; }
            get { return collectiondays; }
        }
        /// <summary>
        /// 收款金额
        /// </summary>
        public double CollectionMoney
        {
            set { collectionmoney = value; }
            get { return collectionmoney; }
        }
        /// <summary>
        /// 付款方式：0为现金支付；1为银行汇款；2为其它方式
        /// </summary>
        public int PayMentMethod
        {
            set { paymentmethod = value; }
            get { return paymentmethod; }
        }
        /// <summary>
        /// 确认方式：0为电话;2为核实;3为传真
        /// </summary>
        public int Confirmed
        {
            set { confirmed = value; }
            get { return confirmed; }
        }
        /// <summary>
        /// 汇款单号
        /// </summary>
        public string RemitNumber
        {
            set { remitnumber = value; }
            get { return remitnumber; }
        }
        /// <summary>
        /// 汇款日期
        /// </summary>
        public DateTime RemitDate
        {
            set { remitdate = value; }
            get { return remitdate; }
        }
        /// <summary>
        /// 汇出银行
        /// </summary>
        public string RemitBank
        {
            set { remitbank = value; }
            get { return remitbank; }
        }
        /// <summary>
        /// 汇出账号
        /// </summary>
        public string RemitCard
        {
            set { remitcard = value; }
            get { return remitcard; }
        }
        /// <summary>
        /// 汇入银行
        /// </summary>
        public string SbouchementBank
        {
            set { sbouchementbank = value; }
            get { return sbouchementbank; }
        }
        /// <summary>
        /// 汇入账号
        /// </summary>
        public string AbouchementCard
        {
            set { abouchementcard = value; }
            get { return abouchementcard; }
        }
        /// <summary>
        /// 汇款人姓名
        /// </summary>
        public string Remitter
        {
            set { remitter = value; }
            get { return remitter; }
        }
        /// <summary>
        /// 汇款人身份证号
        /// </summary>
        public string IdentityCard
        {
            set { identitycard = value; }
            get { return identitycard; }
        }
        /// <summary>
        /// 付款人备注
        /// </summary>
        public string RemitterRemark
        {
            set { remitterremark = value; }
            get { return remitterremark; }
        }
        /// <summary>
        ///收款人备注
        /// </summary>
        public string AbouchementRemark
        {
            set { abouchementremark = value; }
            get { return abouchementremark; }
        }
        /// <summary>
        /// 确认是否收到:0为未收到;1为收到
        /// </summary>
        public int IsAffirm
        {
            set { isaffirm = value; }
            get { return isaffirm; }
        }
        #endregion
    }
}
	