using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
 * 创建者：     汪华
 * 创建时间：   2009-08-30 
 * 文件名：     BalanceToPurseDetailModel
 * 功能：       结算奖金发放至钱包的明细记录模型
 */

namespace Model
{
    public class BalanceToPurseDetailModel
    {
        public BalanceToPurseDetailModel()
        { }

        public BalanceToPurseDetailModel(int id)
        {
            this.iD = id;
        }

        #region 模型

        private int iD;

        /// <summary>
        /// 标识
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
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

        private int expectNum;

        /// <summary>
        /// 发放期数
        /// </summary>
        public int ExpectNum
        {
            get { return expectNum; }
            set { expectNum = value; }
        }

        private double currentOneMark;

        /// <summary>
        /// 本期数消费PV
        /// </summary>
        public double CurrentOneMark
        {
            get { return currentOneMark; }
            set { currentOneMark = value; }
        }

        private double bonus0;

        /// <summary>
        /// 本期奖金0
        /// </summary>
        public double Bonus0
        {
            get { return bonus0; }
            set { bonus0 = value; }
        }

        private double bonus1;

        /// <summary>
        /// 本期奖金1
        /// </summary>
        public double Bonus1
        {
            get { return bonus1; }
            set { bonus1 = value; }
        }

        private double bonus2;

        /// <summary>
        /// 本期奖金2
        /// </summary>
        public double Bonus2
        {
            get { return bonus2; }
            set { bonus2 = value; }
        }

        private double bonus3;

        /// <summary>
        /// 本期奖金3
        /// </summary>
        public double Bonus3
        {
            get { return bonus3; }
            set { bonus3 = value; }
        }

        private double bonus4;

        /// <summary>
        /// 本期奖金4
        /// </summary>
        public double Bonus4
        {
            get { return bonus4; }
            set { bonus4 = value; }
        }

        private double bonus5;

        /// <summary>
        /// 本期奖金5
        /// </summary>
        public double Bonus5
        {
            get { return bonus5; }
            set { bonus5 = value; }
        }

        private double bonus6;

        /// <summary>
        /// 本期奖金6
        /// </summary>
        public double Bonus6
        {
            get { return bonus6; }
            set { bonus6 = value; }
        }

        private double bonus7;

        /// <summary>
        /// 本期奖金7
        /// </summary>
        public double Bonus7
        {
            get { return bonus7; }
            set { bonus7 = value; }
        }

        private double bonus8;

        /// <summary>
        /// 本期奖金8
        /// </summary>
        public double Bonus8
        {
            get { return bonus8; }
            set { bonus8 = value; }
        }

        private double bonus9;

        /// <summary>
        /// 本期奖金9
        /// </summary>
        public double Bonus9
        {
            get { return bonus9; }
            set { bonus9 = value; }
        }

        private double deductTax;

        /// <summary>
        /// 本期扣税
        /// </summary>
        public double DeductTax
        {
            get { return deductTax; }
            set { deductTax = value; }
        }

        private double deductMoney;

        /// <summary>
        /// 本期扣款
        /// </summary>
        public double DeductMoney
        {
            get { return deductMoney; }
            set { deductMoney = value; }
        }

        private double total;

        /// <summary>
        /// 本期奖金总计
        /// </summary>
        public double Total
        {
            get { return total; }
            set { total = value; }
        }

        private double currentSolidSend;

        /// <summary>
        /// 本期实发奖金
        /// </summary>
        public double CurrentSolidSend
        {
            get { return currentSolidSend; }
            set { currentSolidSend = value; }
        }

        private DateTime transferToPurseDate;

        /// <summary>
        /// 奖金转入电子钱包日期
        /// </summary>
        public DateTime TransferToPurseDate
        {
            get { return transferToPurseDate; }
            set { transferToPurseDate = value; }
        }
        #endregion
    }
}
