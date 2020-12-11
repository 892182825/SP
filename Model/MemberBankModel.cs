using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/**
 *  创建人：  郑华超
 *  创建时间：2009.8.27
 *  文件名：MemberBankModel.cs
 *  功能：会员使用的银行模型
 */
namespace Model
{
    /// <summary>
    /// 表4.1—63会员使用银行表
    /// </summary>
    [Serializable]
   public class MemberBankModel
    {
        private int bankID;

       /// <summary>
       /// 银行编号BankID
       /// </summary>
        public int BankID//
        {
            get { return bankID; }
            set { bankID = value; }
        }
        private string bankName;
       /// <summary>
       /// 银行名称BankName
       /// </summary>
        public string BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }
        private string bankCode;

        public string BankCode
        {
            get { return bankCode; }
            set { bankCode = value; }
        }

        private int countryCode;

       /// <summary>
        /// 银行所属国家CountryCode
       /// </summary>
        public int CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; }
        }

        
        public MemberBankModel() 
        { }

        public MemberBankModel(int bankId)
        {
            this.bankID = bankId;
        } 

    }
}
