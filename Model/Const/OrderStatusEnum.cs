using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Const
{
    public enum OrderStatusEnum
    {
        /// <summary>
        /// 正常单0
        /// </summary>
        Normal=0,
        /// <summary>
        /// 部分退货1
        /// </summary>
        RefundmentedPart=1,
        /// <summary>
        /// 整单退货2
        /// </summary>
        RefundmentedAll=2
    }
}
