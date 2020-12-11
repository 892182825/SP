using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Const
{
    /// <summary>
    /// 退款方式
    /// </summary>
    public class RefundsTypeConst
    {
        /// <summary>
        /// 通过现金退款
        /// </summary>
        public const string RefundsUseCash = "0";
        /// <summary>
        /// 通过电子账户退款
        /// </summary>
        public const string RefundsUseEAccount = "1";
        /// <summary>
        /// 通过银行账户退款
        /// </summary>
        public const string RefundsUseBank = "2";
        public RefundsTypeConst()
        { 
        }
        public static  string GetRefundsTypeName(RefundsTypeEnum refundstype)
        {
            string name = string.Empty;
            switch ((int)refundstype)
            {
                case 0:
                    name = "通过现金退款";
                    break;
                case 1:
                    name = "通过电子账户退款";
                    break;
                case 2:
                    name = "通过银行账户退款";
                    break;
            }
            return name;
        }
    }
    /// <summary>
    /// 退款方式
    /// </summary>
    public enum RefundsTypeEnum
    {
        /// <summary>
        /// 通过现金退款-0
        /// </summary>
        RefundsUseCash=0,
        /// <summary>
        /// 通过电子账户退款-1
        /// </summary>
        RefundsUseEAccount=1,
        /// <summary>
        /// 通过银行账户退款-2
        /// </summary>
        RefundsUseBank = 2        
    }

     
}
