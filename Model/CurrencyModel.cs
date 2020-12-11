using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 创建者：孙延昊
 * 创建时间：2009年8月27日 AM 10：07
 * 文件名：CurrencyModel
 * 功能：汇率表模型
 * **/
namespace Model
{
    /// <summary>
    /// 汇率表
    /// </summary>
    public class CurrencyModel
    {
        public CurrencyModel()
        { }

        public CurrencyModel(int id)
        {
            this.iD = id;
        }
        private int iD;

        public int ID
        {
            get { return iD; }
            set {  iD=value;}
        }
        private double rate;
        /// <summary>
        /// 汇率
        /// </summary>
        public double Rate
        {
            get { return rate; }
            set { rate = value; }
        }
        private string name;
        /// <summary>
        /// 币种名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int standardMoney;
        /// <summary>
        /// 标准币ID
        /// </summary>
        public int StandardMoney
        {
            get { return standardMoney; }
            set { standardMoney = value; }
        }

        private string jianCheng;

        /// <summary>
        /// 简称
        /// </summary>
        public string JianCheng
        {
            get { return jianCheng; }
            set { jianCheng = value; }
        }


    }
}
