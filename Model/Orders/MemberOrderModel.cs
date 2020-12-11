using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Const;

namespace Model.Orders
{
    /// <summary>
    /// 会员订单表
    /// </summary>
    [Serializable]
    public class MemberRefundmentEntity
    {
        public List<MemberOrderEntity> MemberOrderList
        {
            get;
            set;
        }

        /// <summary>
        /// 所有退货产品明细
        /// </summary>
        public List<MemberOrderDetailsEntity> DetailsList
        {
            get
            {
                List<MemberOrderDetailsEntity> collections = new List<MemberOrderDetailsEntity>();
                foreach (MemberOrderEntity o in MemberOrderList)
                {
                    if (o.Details == null || o.Details.Count < 1)
                        continue;
                    foreach (MemberOrderDetailsEntity md in o.Details)
                    {
                        collections.Add(md);
                    }
                }
                return collections;
            }

        }

        /// <summary>
        /// 原始单据号
        /// </summary>
        public string OriginalDocID
        {
            get;
            set;
        }
        /// <summary>
        /// 退货原因
        /// </summary>
        public string Cause
        {
            get;
            set;
        }
        /// <summary>
        /// 退款方式
        /// </summary>
        public RefundsTypeEnum RefundmentType
        {
            get;
            set;
        }

        /// <summary>
        /// 退货地址
        /// </summary>
        public string Address
        {
            get;
            set;
        }
        private string _Number = string.Empty;
        /// <summary>
        /// 订单会员编号 
        /// </summary>
        public string Number
        {
            get {
                string tempNumber = string.Empty;
                if (string.IsNullOrEmpty(_Number))
                {
                    if (MemberOrderList != null && MemberOrderList.Count >0)
                    {
                        tempNumber = MemberOrderList[0].Number; 
                    }
                    return tempNumber;
                }
                else
                    return _Number;
            }
            set
            {
                _Number = value;
            }
        }
        public MemberInfoModel MemberInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 总退货金额
        /// </summary>
        public double ReturnTotalMoney
        {
            //get
            //{
            //    double returnTotalMoney = 0;
            //    foreach (MemberOrderDetailsEntity md in DetailsList)
            //    {
            //        returnTotalMoney += md.UseQuantity * md.UnitPrice;
            //    }
            //    return returnTotalMoney;
            //}
            get;
            set;
        }
        /// <summary>
        /// 总退货PV
        /// </summary>
        public double ReturnTotalPV
        {
            get;
            set;
            //get
            //{
            //    double returnTotalPV = 0;
            //    foreach (MemberOrderDetailsEntity md in DetailsList)
            //    {
            //        returnTotalPV += md.UseQuantity * md.UnitPV;
            //    }
            //    return returnTotalPV;
            //}
        }
       
    }

    /// <summary>
    /// 会员订单表【会员退货用】
    /// </summary>
    [Serializable]
    public class MemberOrderEntity
    {
        public MemberOrderEntity()
        { }
        
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderID
        {
            get;
            set;
        }

        /// <summary>
        /// 订单会员
        /// </summary>
        public string Number
        {
            get;
            set;
        }

        /// <summary>
        /// 报单日期
        /// </summary>
        public DateTime OrderDate
        {
            get;
            set;
        }

        /// <summary>
        /// 总金额
        /// </summary>
        public double TotalMoney
        { get; set; }

        /// <summary>
        /// 总积分
        /// </summary>
        public double TotalPv
        { get; set; }

        /// <summary>
        /// 已退货总金额
        /// </summary>
        public double TotalMoneyReturned
        { get; set; }

        /// <summary>
        /// 已退货总积分
        /// </summary>
        public double TotalPvReturned
        { get; set; }



        /// <summary>
        /// 产品明细
        /// </summary>
        public List<MemberOrderDetailsEntity> Details
        {
            get;
            set;
        }
        

    }
    [Serializable]
    public class MemberOrderDetailsEntity
    {
        public MemberOrderDetailsEntity()
        { }

        /// <summary>
        ///明细ID
        /// </summary>
        public int ID
        {
            get;
            set;
        }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderID
        {
            get;
            set;
        }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductID
        {
            get;
            set;
        }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode
        {
            get;
            set;
        }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName
        {
            get;
            set;
        }
        /// <summary>
        /// 产品数量
        /// </summary>
        public int Quantity
        {
            get;
            set;
        }
        /// <summary>
        /// 退货数量
        /// </summary>
        public int QuantityReturned
        {
            get;
            set;
        }
        /// <summary>
        /// 剩余数量
        /// </summary>
        public int LeftQuantity
        {
            get { return (Quantity - QuantityReturned - QuantityReturning); }
        }
        /// <summary>
        /// 使用的数量，退换货时临时使用
        /// </summary>
        public int UseQuantity
        {
            get;
            set;
        }
        /// <summary>
        /// 退货中的数量
        /// </summary>
        public int QuantityReturning
        {
            get;
            set;
        }
        /// <summary>
        /// 是否全部使用，退换货时临时使用
        /// </summary>
        public bool UseAll
        {
            get;
            set;
        }
        /// <summary>
        /// 单价
        /// </summary>
        public double UnitPrice
        {
            get;
            set;
        }
        /// <summary>
        /// 单位积分
        /// </summary>
        public double UnitPV
        { get; set; }

        /// <summary>
        /// 产品批次
        /// </summary>
        public string pici
        {
            get;
            set;
        }

        
    }
}
