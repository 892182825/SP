using System;
using System.Collections.Generic;
using System.Text;

/*
 *
 *����ʱ�䣺09/8/27
 *�ļ�����CityModel.cs
 *���ܣ����㲦���ʱ�
 */

namespace Model
{
    /// <summary>
    /// ���㲦���ʱ�
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
        /// ���
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// ��������
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
        /// �����ܽ��
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
        /// ������PV
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
        /// �����ܽ���
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
        /// ������
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
        /// �����ܽ���0
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
        /// �����ܽ���1
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
        /// �����ܽ���2
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
        /// �����ܽ���3
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
        /// �����ܽ���4
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
        /// �����ܽ���5
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
        /// �����ܽ���6
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
        /// �����ܽ���7
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
        /// �����ܽ���8
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
        /// �����ܽ���9
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
        /// �����ܽ���10
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
        /// ����ʱ��
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
