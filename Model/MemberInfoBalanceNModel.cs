using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 *
 * 创建者：     张朔
 * 创建时间：   2009年8月27日 AM 10：07
 * 修改者：     汪华
 * 修改时间：   2009-08-30
 * 文件名：     MemberInfoBalanceNModel
 * 功能：       会员结算表
 */

namespace Model
{
    /// <summary>
    /// 会员结算表
    /// </summary>
    public class MemberInfoBalanceNModel
    {
        public MemberInfoBalanceNModel() { }

        private string number;
        /// <summary>
        /// 会员编号
        /// </summary>
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private int level;
        /// <summary>
        /// 会员级别 
        /// </summary>
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        private int layerBit1;
        /// <summary>
        /// 安置层位
        /// </summary>
        public int LayerBit1
        {
            get { return layerBit1; }
            set { layerBit1 = value; }
        }
        private int ordinal1;
        /// <summary>
        /// 安置序号
        /// </summary>
        public int Ordinal1
        {
            get { return ordinal1; }
            set { ordinal1 = value; }
        }
        private int layerBit2;
        /// <summary>
        /// 推荐层位
        /// </summary>
        public int LayerBit2
        {
            get { return layerBit2; }
            set { layerBit2 = value; }
        }
        private int ordinal2;
        /// <summary>
        /// 推荐序号
        /// </summary>
        public int Ordinal2
        {
            get { return ordinal2; }
            set { ordinal2 = value; }
        }
        private int limitNum1;
        /// <summary>
        /// 自定义
        /// </summary>
        public int LimitNum1
        {
            get { return limitNum1; }
            set { limitNum1 = value; }
        }
        private int limitNum2;
        /// <summary>
        /// 自定义
        /// </summary>
        public int LimitNum2
        {
            get { return limitNum2; }
            set { limitNum2 = value; }
        }
        private int limitCount1;
        /// <summary>
        /// 自定义
        /// </summary>
        public int LimitCount1
        {
            get { return limitCount1; }
            set { limitCount1 = value; }
        }
        private int limitCount2;
        /// <summary>
        /// 自定义
        /// </summary>
        public int LimitCount2
        {
            get { return limitCount2; }
            set { limitCount2 = value; }
        }
        private double oneMark;
        /// <summary>
        /// 总个人首次消费
        /// </summary>
        public double OneMark
        {
            get { return oneMark; }
            set { oneMark = value; }
        }
        private int currentNewNetNum;
        /// <summary>
        /// 本期网络新进人数
        /// </summary>
        public int CurrentNewNetNum
        {
            get { return currentNewNetNum; }
            set { currentNewNetNum = value; }
        }
        private int totalNetNum;
        /// <summary>
        /// 网络总人数
        /// </summary>
        public int TotalNetNum
        {
            get { return totalNetNum; }
            set { totalNetNum = value; }
        }
        private double currentTotalNetRecord;
        /// <summary>
        /// 本期网络业绩
        /// </summary>
        public double CurrentTotalNetRecord
        {
            get { return currentTotalNetRecord; }
            set { currentTotalNetRecord = value; }
        }
        private double totalNetRecord;
        /// <summary>
        /// 网络总业绩
        /// </summary>
        public double TotalNetRecord
        {
            get { return totalNetRecord; }
            set { totalNetRecord = value; }
        }
        private double currentOneRecord;
        /// <summary>
        /// 本期个人消费
        /// </summary>
        public double CurrentOneRecord
        {
            get { return currentOneRecord; }
            set { currentOneRecord = value; }
        }
        private double totalOneMark;
        /// <summary>
        /// 个人总消费
        /// </summary>
        public double TotalOneMark
        {
            get { return totalOneMark; }
            set { totalOneMark = value; }
        }
        private double notTotalMark;
        /// <summary>
        /// 未支付的总消费
        /// </summary>
        public double NotTotalMark
        {
            get { return notTotalMark; }
            set { notTotalMark = value; }
        }
        private double totalNotNetRecord;
        /// <summary>
        /// 未支付的网络总消费
        /// </summary>
        public double TotalNotNetRecord
        {
            get { return totalNotNetRecord; }
            set { totalNotNetRecord = value; }
        }
        private double bonus0;
        /// <summary>
        /// 自定义奖金
        /// </summary>
        public double Bonus0
        {
            get { return bonus0; }
            set { bonus0 = value; }
        }
        private double bonus1;
        /// <summary>
        /// 自定义奖金
        /// </summary>
        public double Bonus1
        {
            get { return bonus1; }
            set { bonus1 = value; }
        }
        private double bonus2;
        /// <summary>
        /// 自定义奖金
        /// </summary>
        public double Bonus2
        {
            get { return bonus2; }
            set { bonus2 = value; }
        }
        private double bonus3;
        /// <summary>
        /// 自定义奖金
        /// </summary>
        public double Bonus3
        {
            get { return bonus3; }
            set { bonus3 = value; }
        }
        private double bonus4;
        /// <summary>
        /// 自定义奖金
        /// </summary>
        public double Bonus4
        {
            get { return bonus4; }
            set { bonus4 = value; }
        }
        private double bonus5;
        /// <summary>
        /// 自定义奖金
        /// </summary>
        public double Bonus5
        {
            get { return bonus5; }
            set { bonus5 = value; }
        }
        private double bonus6;
        /// <summary>
        /// 自定义奖金
        /// </summary>
        public double Bonus6
        {
            get { return bonus6; }
            set { bonus6 = value; }
        }
        private double bonus7;
        /// <summary>
        /// 自定义奖金
        /// </summary>
        public double Bonus7
        {
            get { return bonus7; }
            set { bonus7 = value; }
        }
        private double bonus8;
        /// <summary>
        /// 自定义奖金
        /// </summary>
        public double Bonus8
        {
            get { return bonus8; }
            set { bonus8 = value; }
        }
        private double bonus9;
        /// <summary>
        /// 自定义奖金
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
        private double currentTotalMark;
        /// <summary>
        /// 本期总奖金
        /// </summary>
        public double CurrentTotalMark
        {
            get { return currentTotalMark; }
            set { currentTotalMark = value; }
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
        private double currentOneMark;
        /// <summary>
        /// 本个人消费金额
        /// </summary>
        public double CurrentOneMark
        {
            get { return currentOneMark; }
            set { currentOneMark = value; }
        }
        private double totalOneMoney;
        /// <summary>
        /// 个人消费总金额
        /// </summary>
        public double TotalOneMoney
        {
            get { return totalOneMoney; }
            set { totalOneMoney = value; }
        }
        private double bonusAccumulation;
        /// <summary>
        /// 奖金累计
        /// </summary>
        public double BonusAccumulation
        {
            get { return bonusAccumulation; }
            set { bonusAccumulation = value; }
        }
        private double solidSendAccumulation;
        /// <summary>
        /// 实发奖金累计
        /// </summary>
        public double SolidSendAccumulation
        {
            get { return solidSendAccumulation; }
            set { solidSendAccumulation = value; }
        }
        private double customUseNum;
        /// <summary>
        /// 自定义结算用
        /// </summary>
        public double CustomUseNum
        {
            get { return customUseNum; }
            set { customUseNum = value; }
        }
        private string customUse;
        /// <summary>
        /// 自定义结算用
        /// </summary>
        public string CustomUse
        {
            get { return customUse; }
            set { customUse = value; }
        }
        private decimal bonus10;
        /// <summary>
        /// 自定义奖金
        /// </summary>
        public decimal Bonus10
        {
            get { return bonus10; }
            set { bonus10 = value; }
        }


    }
}
