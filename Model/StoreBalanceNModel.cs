using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 *
 * 创建者：张朔
 * 创建时间：2009年8月27日 AM 10：07
 * 文件名：StoreBalanceNModel
 * 功能：店铺结算表
 */

namespace Model
{
    /// <summary>
    /// 店铺结算表
    /// </summary>
    public class StoreBalanceNModel
    {
        public StoreBalanceNModel() { }

        private string number;
        /// <summary>
        /// 会员编号
        /// </summary>
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string storeId;
        /// <summary>
        /// 店铺编号
        /// </summary>
        public string StoreId
        {
            get { return storeId; }
            set { storeId = value; }
        }
        private double currentSale;
        /// <summary>
        /// 本期销售额
        /// </summary>
        public double CurrentSale
        {
            get { return currentSale; }
            set { currentSale = value; }
        }
        private double accumulationSale;
        /// <summary>
        /// 累计销售额
        /// </summary>
        public double AccumulationSale
        {
            get { return accumulationSale; }
            set { accumulationSale = value; }
        }
        private double bonus1;
        /// <summary>
        /// 奖金1
        /// </summary>
        public double Bonus1
        {
            get { return bonus1; }
            set { bonus1 = value; }
        }
        private double bonus2;
        /// <summary>
        /// 奖金2
        /// </summary>
        public double Bonus2
        {
            get { return bonus2; }
            set { bonus2 = value; }
        }
        private double bonus3;
        /// <summary>
        /// 奖金3
        /// </summary>
        public double Bonus3
        {
            get { return bonus3; }
            set { bonus3 = value; }
        }
        private double bonus4;
        /// <summary>
        /// 奖金4
        /// </summary>
        public double Bonus4
        {
            get { return bonus4; }
            set { bonus4 = value; }
        }
    }
}
