using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 *
 * 创建者：张朔
 * 创建时间：2009年8月27日 AM 10：07
 * 文件名：DeductModel
 * 功能：扣款表
 */

namespace Model
{
    /// <summary>
    /// 扣款表
    /// </summary>
    public class DeductModel
    {
        public DeductModel() { }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="id">标示</param>
        public DeductModel(int id)
        {
            this.iD = id;
        }

        private int iD;
        /// <summary>
        /// 标示
        /// </summary>
        public int ID
        {
            set { iD = value; }
            get { return iD; }
        }
        private string number;
        /// <summary>
        /// 会员编号
        /// </summary>
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private double deductMoney;
        /// <summary>
        /// 扣补款额
        /// </summary>
        public double DeductMoney
        {
            get { return deductMoney; }
            set { deductMoney = value; }
        }
        private string deductReason;
        /// <summary>
        /// 扣补款原因
        /// </summary>
        public string DeductReason
        {
            get { return deductReason; }
            set { deductReason = value; }
        }
        private int expectNum;
        /// <summary>
        /// 扣补款期数
        /// </summary>
        public int ExpectNum
        {
            get { return expectNum; }
            set { expectNum = value; }
        }
        private int isDeduct;
        /// <summary>
        /// 0为扣款，1为补款
        /// </summary>
        public int IsDeduct
        {
            get { return isDeduct; }
            set { isDeduct = value; }
        }
        int actype = 0;
        /// <summary>
        ///  0 现金账户  1  E币
        /// </summary>
        public int Actype
        {
            get { return actype; }
            set { actype = value; }
        }
        private DateTime keyInDate;
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime KeyInDate
        {
            get { return keyInDate; }
            set { keyInDate = value; }
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

        private int auditing;
        /// <summary>
        /// 是否审核
        /// </summary>
        public int Auditing
        {
            get { return auditing; }
            set { auditing = value; }
        }

        private DateTime auditingTime;
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime AuditingTime
        {
            get { return auditingTime; }
            set { auditingTime = value; }
        }

        private int auditingexctnum;

        public int Auditingexctnum
        {
            get { return auditingexctnum; }
            set { auditingexctnum = value; }
        }
    }
}
