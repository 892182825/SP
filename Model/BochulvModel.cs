using System;
using System.Collections.Generic;
using System.Text;

/*
 *
 *创建时间：09/8/27
 *文件名：CityModel.cs
 *功能：结算拨出率表
 */

namespace Model
{
    /// <summary>
    /// 结算拨出率表
    /// </summary>
    public class BochulvModel
    {
        public BochulvModel()
        { }

        public BochulvModel(int id)
        {
            this.id = id;
        }

        private int id;
        private int expectNum;
        private decimal totalmoney;
        private decimal totalpv;
        private decimal totalBonus;
        private decimal allocateLead;
        private decimal bonus0;
        private decimal bonus1;
        private decimal bonus2;
        private decimal bonus3;
        private decimal bonus4;
        private decimal bonus5;
        private decimal bonus6;
        private decimal bonus7;
        private decimal bonus8;
        private decimal bonus9;
        private decimal bonus10;
        private DateTime settleDate;

        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// 结算期数
        /// </summary>
        public int ExpectNum
        {
            get
            {
                return expectNum;
            }
            set
            {
                expectNum = value;
            }
        }

        /// <summary>
        /// 当期总金额
        /// </summary>
        public decimal Totalmoney
        {
            get
            {
                return totalmoney;
            }
            set
            {
                totalmoney = value;
            }
        }

        /// <summary>
        /// 当期总PV
        /// </summary>
        public decimal Totalpv
        {
            get
            {
                return totalpv;
            }
            set
            {
                totalpv = value;
            }
        }

        /// <summary>
        /// 当期总奖金
        /// </summary>
        public decimal TotalBonus
        {
            get
            {
                return totalBonus;
            }
            set
            {
                totalBonus = value;
            }
        }

        /// <summary>
        /// 拨出率
        /// </summary>
        public decimal AllocateLead
        {
            get
            {
                return allocateLead;
            }
            set
            {
                allocateLead = value;
            }
        }

        /// <summary>
        /// 当期总奖金0
        /// </summary>
        public decimal Bonus0
        {
            get
            {
                return bonus0;
            }
            set
            {
                bonus0 = value;
            }
        }

        /// <summary>
        /// 当期总奖金1
        /// </summary>
        public decimal Bonus1
        {
            get
            {
                return bonus1;
            }
            set
            {
                bonus1 = value;
            }
        }

        /// <summary>
        /// 当期总奖金2
        /// </summary>
        public decimal Bonus2
        {
            get
            {
                return bonus2;
            }
            set
            {
                bonus2 = value;
            }
        }

        /// <summary>
        /// 当期总奖金3
        /// </summary>
        public decimal Bonus3
        {
            get
            {
                return bonus3;
            }
            set
            {
                bonus3 = value;
            }
        }

        /// <summary>
        /// 当期总奖金4
        /// </summary>
        public decimal Bonus4
        {
            get
            {
                return bonus4;
            }
            set
            {
                bonus4 = value;
            }
        }

        /// <summary>
        /// 当期总奖金5
        /// </summary>
        public decimal Bonus5
        {
            get
            {
                return bonus5;
            }
            set
            {
                bonus5 = value;
            }
        }

        /// <summary>
        /// 当期总奖金6
        /// </summary>
        public decimal Bonus6
        {
            get
            {
                return bonus6;
            }
            set
            {
                bonus6 = value;
            }
        }

        /// <summary>
        /// 当期总奖金7
        /// </summary>
        public decimal Bonus7
        {
            get
            {
                return bonus7;
            }
            set
            {
                bonus7 = value;
            }
        }

        /// <summary>
        /// 当期总奖金8
        /// </summary>
        public decimal Bonus8
        {
            get
            {
                return bonus8;
            }
            set
            {
                bonus8 = value;
            }
        }

        /// <summary>
        /// 当期总奖金9
        /// </summary>
        public decimal Bonus9
        {
            get
            {
                return bonus9;
            }
            set
            {
                bonus9 = value;
            }
        }

        /// <summary>
        /// 当期总奖金10
        /// </summary>
        public decimal Bonus10
        {
            get
            {
                return bonus10;
            }
            set
            {
                bonus10 = value;
            }
        }

        /// <summary>
        /// 结算时间
        /// </summary>
        public DateTime SettleDate
        {
            get
            {
                return settleDate;
            }
            set
            {
                settleDate = value;
            }
        }

    }
}
