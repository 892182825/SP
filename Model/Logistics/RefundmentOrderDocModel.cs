using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Logistics
{
    [Serializable]
    public class RefundmentOrderDocModel
    {

        public RefundmentOrderDocModel()
        { 
        }
        public int ID{get;set;}
        public string DocID{get;set;} 
        /// <summary>
        /// 归属人
        /// </summary>
        public string OwnerNumber_TX{get;set;} 
        /// <summary>
        /// 申请人
        /// </summary>
        public string Applicant_TX{get;set;} 
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime  ApplyTime_DT{get;set;}

        private string _Auditer = string.Empty;
        /// <summary>
        /// 审核人
        /// </summary>
        public string Auditer
        {

            get { return _Auditer; }
            set { _Auditer = value; }
        }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime?  AuditTime{get;set;}

        /// <summary>
        /// 退货日期
        /// </summary>
        public DateTime RefundmentDate_DT { get; set; } 
        /// <summary>
        /// 当前的流程状态(0已提交，1已审核未退款，2己审核已退款)	
        /// </summary>
        public int StatusFlag_NR{get;set;}
        /// <summary>
        /// 仓库，产品退入仓库
        /// </summary>
        public int WareHouseID{get;set;}
        /// <summary>
        /// 库位，产品退入库位
        /// </summary>
        public int DepotSeatID{get;set;}
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalMoney{get;set;}
        /// <summary>
        /// 总PV
        /// </summary>
        public decimal TotalPV{get;set;}
        
        /// <summary>
        /// 退款方式
        /// </summary>
        public int RefundmentType_NR { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string MobileTele { get; set; }
        /// <summary>
        /// 支付币种
        /// </summary>
        public int PayCurrency{get;set;}
        
        /// <summary>
        /// 对应支付金额
        /// </summary>
        public decimal PayMoney{get;set;}
        /// <summary>
        /// 提交期数
        /// </summary>
        public int ExpectNum{get;set;}
        /// <summary>
        /// 退货原因
        /// </summary>
        public string Cause_TX{get;set;} 
        /// <summary>
        /// 备注
        /// </summary>
        public string Note_TX{get;set;} 
        /// <summary>
        /// 业务员
        /// </summary>
        public string OperationPerson{get;set;} 
        /// <summary>
        /// 原始单据
        /// </summary>
        public string OriginalDocIDS{get;set;} 
	    /// <summary>
	    /// 国家ID
	    /// </summary>
        public int Country{get;set;}
        /// <summary>
        /// 地址编码
        /// </summary>
        public string CPCCode{get;set;} 
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address_TX{get;set;}

        private string _BankCode = string.Empty;
        /// <summary>
        /// 银行名
        /// </summary>
        public string BankCode 
        {
            get { return _BankCode; }
            set { _BankCode = value; } 
        }

        private string _BankBranch = string.Empty;
        /// <summary>
        /// 支行名
        /// </summary>
        public string BankBranch
        {
            get { return _BankBranch; }
            set { _BankBranch = value; }
        }

        private string _BankAddres = string.Empty;
        /// <summary>
        /// 银行地址
        /// </summary>
        public string BankAddres
        {
            get { return _BankAddres; }
            set { _BankAddres = value; }
        }

        private string _BankBookName = string.Empty;
        /// <summary>
        /// 开户账户名
        /// </summary>
        public string BankBookName
        {
            get { return _BankBookName; }
            set { _BankBookName = value; }
        }

        private string _BankBook = string.Empty;
        /// <summary>
        /// 开户账户
        /// </summary>
        public string BankBook
        {
            get { return _BankBook; }
            set { _BankBook = value; }
        }

        private string _BankCard = string.Empty;
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankCard
        {
            get { return _BankCard; }
            set { _BankCard = value; }
        } 
        /// <summary>
        /// 实退金额
        /// </summary>
        public decimal RefundTotalMoney{get;set;}

        private decimal _Charged_NR = 0;
	    /// <summary>
	    /// 退货扣款额
	    /// </summary>
        public decimal Charged_NR
        {
            get { return _Charged_NR; }
            set { _Charged_NR = value; }
        }

        private string _ChargedReason_TX = string.Empty;
        /// <summary>
        /// 退货扣款原因
        /// </summary>
        public string ChargedReason_TX
        {
            get { return _ChargedReason_TX; }
            set { _ChargedReason_TX = value; }
        } 
        /// <summary>
        /// 操作者IP
        /// </summary>
        public string OperateIP_TX{get;set;} 
        /// <summary>
        /// 操作者编号
        /// </summary>
        public string OperateNum_TX{get;set;}

        public List<RefundmentOrderDocDetails> RefundmentOrderDetails
        {
            get;
            set;
        }

        private string _RefundNumber_TX = string.Empty;
        /// <summary>
        /// 退款验收人
        /// </summary>
        public string RefundNumber_TX
        {

            get { return _RefundNumber_TX; }
            set { _RefundNumber_TX = value; }
        }
        /// <summary>
        /// 退款验收时间
        /// </summary>
        public DateTime? RefundTime_DT { get; set; }

        private int _IsLock = 0;
        /// <summary>
        /// 当前的流程状态(0未锁定，1已锁定)	
        /// </summary>
        public int IsLock{get;set;}
    }

    [Serializable]
    public class RefundmentOrderDocDetails
    {
        public RefundmentOrderDocDetails()
        { 
        }

        public int DocDetailsID{get;set;}
        
        public string DocID{get;set;} 
        public string OriginalDocID{get;set;} 
        public int ProductID{get;set;} 
        public int ProductQuantity{get;set;} 
	
        public decimal UnitPrice{get;set;} 
        public decimal UnitPV{get;set;} 
        /// <summary>
        /// 期数
        /// </summary>
        public int ExpectNum{get;set;} 
        /// <summary>
        /// 批次
        /// </summary>
        public string Batch{get;set;}         
    }
}
