using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class WithdrawModel
    {
        int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        string number;
        public string Number
        {
            get { return number; }
            set{ number=value; }
        }

        int applicationExpectNum;
        public int ApplicationExpecdtNum
        {
            get { return applicationExpectNum; }
            set { applicationExpectNum = value; }
        }

        int auditExpectNum;
        public int AuditExpectNum
        {
            get { return auditExpectNum; }
            set { auditExpectNum = value; }
        }

        int isAuditing;
        public int IsAuditing
        {
            get { return isAuditing; }
            set { isAuditing = value; }
        }

        double withdrawMoney;
        public double WithdrawMoney
        {
            get { return withdrawMoney; }
            set { withdrawMoney = value; }
        }

        DateTime withdrwaTime;
        public DateTime WithdrawTime
        {
            get { return withdrwaTime; }
            set { withdrwaTime = value; }
        }

        DateTime auditTime;
        public DateTime AuditTime
        {
            get { return auditTime; }
            set { auditTime = value; }
        }

        string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        string operateIP;
        public string OperateIP
        {
            get { return operateIP; }
            set { operateIP = value; }
        }
        string auditingIP;
        public string AuditingIP
        {
            get { return auditingIP; }
            set { auditingIP = value; }
        }

        string auditingManageId;
        public string AuditingManageId
        {
            get { return auditingManageId; }
            set { auditingManageId = value; }
        }
        string bankcard="";

        public string Bankcard
        {
            get { return bankcard; }
            set { bankcard = value; }
        }

        string bankname="";

        public string Bankname
        {
            get { return bankname; }
            set { bankname = value; }
        }
        double withdrawSXF;
        public double WithdrawSXF
        {
            get { return withdrawSXF; }
            set { withdrawSXF = value; }
        }
        string khname="";
        public string Khname
        {
            get { return khname; }
            set { khname = value; }
        }



        int isJL;
        public int IsJL
        {
            get { return isJL; }
            set { isJL = value; }
        }

        double wyj;
        public double Wyj
        {
            get { return wyj; }
            set { wyj = value; }
        }
        double Blmoney;
        public double blmoney
        {
            get { return Blmoney; }
            set { Blmoney = value; }
        }


        double wyjbl;
        public double Wyjbl
        {
            get { return wyjbl; }
            set { wyjbl = value; }
        }
        decimal investJB;
        /// <summary>
        /// 石斛积分数量
        /// </summary>
        public decimal InvestJB
        {
            get { return investJB; }
            set { investJB = value; }
        }
        decimal investJBSXF;
        /// <summary>
        /// 石斛积分sxf
        /// </summary>
        public decimal InvestJBSXF
        {
            get { return investJBSXF; }
            set { investJBSXF = value; }
        }  decimal investJBWYJ;
        /// <summary>
        /// 石斛积分违约金
        /// </summary>
        public decimal InvestJBWYJ
        {
            get { return investJBWYJ; }
            set { investJBWYJ = value; }
        }
        decimal priceJB;
        public decimal PriceJB
        {
            get { return priceJB; }
            set { priceJB = value; }
        }

        int drawCardtype;
        public int DrawCardtype
        {
            get { return drawCardtype; }
            set { drawCardtype = value; }
        }

        string aliNo="";
        public string  AliNo
        {
            get { return aliNo; }
            set { aliNo = value; }
        }

        string weiXNo="";
        public string WeiXNo
        {
            get { return weiXNo; }
            set { weiXNo = value; }
        }



    }
}
