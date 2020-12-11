using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/**
 *  创建人：  郑华超
 *  创建时间：2009.8.27
 *  文件名：CompanyBankModel.cs
 *  功能：公司使用的银行模型
 */
namespace Model
{
    /// <summary>
    /// 公司使用银行表
    /// </summary>
    [Serializable]
    public class CompanyBankModel
    {
        
        private int iD; //该属性已修改  2009-09-14
        /// <summary>
        /// jiang
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }


        private int bankNum;
        /// <summary>
        /// 银行编号
        /// </summary>
        public int BankNum
        {
            get { return bankNum; }
            set { bankNum = value; }
        }
        private string bank;
        /// <summary>
        /// 银行名称
        /// </summary>
        public string Bank
        {
            get { return bank; }
            set { bank = value; }
        }

        private string bankname;

        /// <summary>
        /// 开户名
        /// </summary>
        public string Bankname
        {
            get { return bankname; }
            set { bankname = value; }
        }

        
       
        
        
        private string bankBook;
        /// <summary>
        /// 银行账号
        /// </summary>
        public string BankBook
        {
            get { return bankBook; }
            set { bankBook = value; }
        }
        private int countryID;
        /// <summary>
        /// 所属国家
        /// </summary>
        public int CountryID
        {
            get { return countryID; }
            set { countryID = value; }
        }
    }
}
