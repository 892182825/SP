using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 创建者：孙延昊
 * 创建时间：2009年8月27日 AM 10：07
 * 文件名：ReplacementModel
 * 功能：店铺换货表模型
 * **/
namespace Model
{
    /// <summary>
    /// 店铺换货表
    /// </summary>
    public class ReplacementModel
    {

        public ReplacementModel()
        { }

        public ReplacementModel(int id)
        {
            this.iD = id;
        }

        private int iD;
        /// <summary>
        /// 换货单标识
        /// </summary>
        public int ID
        {
            get { return iD; }
        }
       
        /// <summary>
        /// 换货单号
        /// </summary>
         private string displaceOrderID;

        public string DisplaceOrderID
        {
            get { return displaceOrderID; }
            set { displaceOrderID = value; }
        }
        private string storeID;
        /// <summary>
        /// 店铺编号
        /// </summary>
        public string StoreID
        {
            get { return storeID; }
            set { storeID = value; }
        }
        private string storeOrderID;
        /// <summary>
        /// 退货编号
        /// </summary>
        public string StoreOrderID
        {
            get { return storeOrderID; }
            set { storeOrderID = value; }
        }
        private string refundmentOrderID;
        /// <summary>
        /// 报损编号
        /// </summary>
        public string RefundmentOrderID
        {
            get { return refundmentOrderID; }
            set { refundmentOrderID = value; }
        }
        private string outStrageOrderID;

        /// <summary>
        /// 换货单时间
        /// </summary>
        public string OutStrageOrderID
        {
            get { return outStrageOrderID; }
            set { outStrageOrderID = value; }
        }
        private DateTime makeDocDate;

        /// <summary>
        /// 提交日期
        /// </summary>
        public DateTime MakeDocDate
        {
            get { return makeDocDate; }
            set { makeDocDate = value; }
        }
        private string makeDocPerson;

        /// <summary>
        /// 提交人
        /// </summary>
        public string MakeDocPerson
        {
            get { return makeDocPerson; }
            set { makeDocPerson = value; }
        }
        private DateTime auditingDate;

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime AuditingDate
        {
            get { return auditingDate; }
            set { auditingDate = value; }
        }
        private string auditPerson;

        /// <summary>
        /// 审核人
        /// </summary>
        public string AuditPerson
        {
            get { return auditPerson; }
            set { auditPerson = value; }
        }
        private int expectNum;

        /// <summary>
        /// 期数
        /// </summary>
        public int ExpectNum
        {
            get { return expectNum; }
            set { expectNum = value; }
        }
        private double outTotalMoney;

        /// <summary>
        /// 报损总金额
        /// </summary>
        public double OutTotalMoney
        {
            get { return outTotalMoney; }
            set { outTotalMoney = value; }
        }
        private double outTotalPV;

        /// <summary>
        /// 报损总PV
        /// </summary>
        public double OutTotalPV
        {
            get { return outTotalPV; }
            set { outTotalPV = value; }
        }
        private double inTotalMoney;

        /// <summary>
        /// 报溢总金额
        /// </summary>
        public double InTotalMoney
        {
            get { return inTotalMoney; }
            set { inTotalMoney = value; }
        }
        private double inTotalPV;

        /// <summary>
        /// 报溢总PV
        /// </summary>
        public double InTotalPV
        {
            get { return inTotalPV; }
            set { inTotalPV = value; }
        }
        private string inceptAddress;

        /// <summary>
        /// 接收地址
        /// </summary>
        public string InceptAddress
        {
            get { return inceptAddress; }
            set { inceptAddress = value; }
        }

        private string inceptPerson;

        /// <summary>
        /// 接收人
        /// </summary>
        public string InceptPerson
        {
            get { return inceptPerson; }
            set { inceptPerson = value; }
        }
        private string postalCode;

        /// <summary>
        /// 邮编
        /// </summary>
        public string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; }
        }
        private string telephone;

        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }
        private string stateFlag;

        public string StateFlag
        {
            get { return stateFlag; }
            set { stateFlag = value; }
        }

        /// <summary>
        /// 状态标记 0 不可用 1 可用
        /// </summary>
        
        private string closeFlag;

        public string CloseFlag
        {
            get { return closeFlag; }
            set { closeFlag = value; }
        }

        /// <summary>
        /// 关闭标记 0 开启 1 关闭
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
        private string operateIP;


        /// <summary>
        /// 操作者IP
        /// </summary>
        public string OperateIP
        {
            get { return operateIP; }
            set { operateIP = value; }
        }
        private string operateNum;

        /// <summary>
        /// 操作者编号
        /// </summary>
        public string OperateNum
        {
            get { return operateNum; }
            set { operateNum = value; }
        }

        
        /// <summary>
        /// 退货额
        /// </summary>
        private double outGoodsMoney;

        public double OutGoodsMoney
        {
            get { return outGoodsMoney; }
            set { outGoodsMoney = value; }
        }

        /// <summary>
        /// 进货额
        /// </summary>
        private double inGoodsMoney;

        public double InGoodsMoney
        {
            get { return inGoodsMoney; }
            set { inGoodsMoney = value; }
        }

    }
}
