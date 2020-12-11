using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Const
{
   
    public enum RefundmentOrderStatusEnum
    {
        /// <summary>
        ///禁用记录
        /// </summary>
        Lock = -1,
        /// <summary>
        /// 未审核
        /// </summary>
        UnAudit = 0,
        /// <summary>
        /// 己审核未退款
        /// </summary>
        AuditedUnPay = 1,
        /// <summary>
        /// 己审核己退款
        /// </summary>
        Audited = 2
    }
}
