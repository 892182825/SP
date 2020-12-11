using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 功能：      实体类ECTransferDetailModel 
    ///创建者：     刘文
    ///创建日期：   2009-8-27  //CK修改12-05-31
    ///文件名：     ECTransferDetailModel
    /// </summary>
    [Serializable]
    public class ECTransferDetailModel
    {
        /// <summary>
        /// 会员转账明细
        /// </summary>
        public ECTransferDetailModel(){}
        #region Model
        private int id;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 转出编号
        /// </summary>
        private string outNumber;
        /// <summary>
        /// 转出编号
        /// </summary>
        public string OutNumber
        {
            get { return outNumber; }
            set { outNumber = value; }
        }
        /// <summary>
        /// 转出账户（0：会员现金账户。1：会员消费账户。）
        /// </summary>
        public OutAccountType outAccountType
        {
            get;
            set;
        }

        /// <summary>
        /// 转账金额
        /// </summary>
        private double outMoney;

        /// <summary>
        /// 转账金额
        /// </summary>
        public double OutMoney
        {
            get { return outMoney; }
            set { outMoney = value; }
        }


        /// <summary>
        /// 转账期数
        /// </summary>
        private int expectNum;
        /// <summary>
        /// 转账期数
        /// </summary>
        public int ExpectNum
        {
            get { return expectNum; }
            set { expectNum = value; }
        }
        /// <summary>
        /// 收款人编号
        /// </summary>
        private string inNumber;
        /// <summary>
        /// 收款人编号
        /// </summary>
        public string InNumber
        {
            get { return inNumber; }
            set { inNumber = value; }
        }

        /// <summary>
        /// 转入账户（0：会员现金账户。1：会员消费账户。2：店铺订货款）
        /// </summary>
        public InAccountType inAccountType { get; set; }

        /// <summary>
        /// 转账时间
        /// </summary>
        private DateTime date;
        /// <summary>
        /// 转账时间
        /// </summary>
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }


        /// <summary>
        /// 是否汇款
        /// </summary>
        private int isRemittances;
        /// <summary>
        /// 是否汇款
        /// </summary>
        public int IsRemittances
        {
            get { return isRemittances; }
            set { isRemittances = value; }
        }
        /// <summary>
        /// 汇款日期
        /// </summary>
        private DateTime remittancesDate;
        /// <summary>
        /// 汇款日期
        /// </summary>
        public DateTime RemittancesDate
        {
            get { return remittancesDate; }
            set { remittancesDate = value; }
        }


        /// <summary>
        /// 是否收款
        /// </summary>
        private int isReceivablesDate;
        /// <summary>
        /// 是否收款
        /// </summary>
        public int IsReceivablesDate
        {
            get { return isReceivablesDate; }
            set { isReceivablesDate = value; }
        }


        /// <summary>
        /// 收款日期
        /// </summary>
        private DateTime receivablesDate;
        /// <summary>
        /// 收款日期
        /// </summary>
        public DateTime ReceivablesDate
        {
            get { return receivablesDate; }
            set { receivablesDate = value; }
        }


        /// <summary>
        /// 操作者IP
        /// </summary>
        private string operateIP;
        /// <summary>
        /// 操作者IP
        /// </summary>
        public string OperateIP
        {
            get { return operateIP; }
            set { operateIP = value; }
        }


        /// <summary>
        /// 操作者类型及编号
        /// </summary>
        private string operateNumber;
        /// <summary>
        /// 操作者类型及编号
        /// </summary>
        public string OperateNumber
        {
            get { return operateNumber; }
            set { operateNumber = value; }
        }


        /// <summary>
        /// 备注
        /// </summary>
        private string remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        #endregion Model
    }

    /// <summary>
    /// 转入账户（0：会员现金账户。1：会员消费账户。2：店铺订货款）
    /// </summary>
    public enum InAccountType
    {
        /// <summary>
        /// 会员现金账户
        /// </summary>
        MemberCash = 0,
        /// <summary>
        /// 会员报单账户
        /// </summary>
        MemberBD = 0,
        /// <summary>
        /// 会员消费账户
        /// </summary>
        MemberCons = 1,
        ///<summary>
        /// 会员注册积分账户
        /// </summary>
        MemberTypeFx = 2,
        /// <summary>
        /// 店铺订货款
        /// </summary>
        StoreOrder = 3,
    }

    /// <summary>
    /// 转出账户（0：会员现金账户。1：会员消费账户。）
    /// </summary>
    public enum OutAccountType
    {
        /// <summary>
        /// 会员现金账户
        /// </summary>
        MemberCash = 0,
        /// <summary>
        /// 会员报单账户
        /// </summary>
        MemberBD = 6,
        /// <summary>
        /// 会员消费账户
        /// </summary>
        MemberCons = 1,

        ///<summary>
        /// 会员注册积分账户
        /// </summary>
        MemberTypeFx=2,
    }
}
