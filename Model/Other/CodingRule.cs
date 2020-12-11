using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Other
{
    public class CodingRule
    {
        public CodingRule() { }
        /// <summary>
        /// 得到单据的前缀字符
        /// 入库：KR  红入库：RH  退入库：RT 盘入：RP
        /// 出库：CK  红出库：CH  盘出库：CP 退出库：CT
        /// </summary>
        public string GetOrderFormPrefix(EnumOrderFormType enumOrderFormType)
        {
            string prefix;
            switch (enumOrderFormType)
            {
                case EnumOrderFormType.InStorage:
                    prefix = "RK";
                    break;
                case EnumOrderFormType.RedInStorage:
                    prefix = "RH";
                    break;
                case EnumOrderFormType.ReturnInStorage:
                    prefix = "HT";
                    break;
                case EnumOrderFormType.CheckInStorage:
                    prefix = "RP";
                    break;
                case EnumOrderFormType.OutStorage:
                    prefix = "CK";
                    break;
                case EnumOrderFormType.RedOutStorage:
                    prefix = "CH";
                    break;
                case EnumOrderFormType.CheckOutStorage:
                    prefix = "CP";
                    break;
                case EnumOrderFormType.ReturnOutStorage:
                    prefix = "TH";
                    break;
                case EnumOrderFormType.OneToAnother:
                    prefix = "DB";
                    break;
                default:
                    prefix = string.Empty;
                    break;
            }
            return prefix;
        }
    }
}
