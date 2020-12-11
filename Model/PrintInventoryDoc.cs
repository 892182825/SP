using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Editor:      WangHua
 * EditorDate:  2009-11-26
 */

namespace Model
{
    public class PrintInventoryDoc
    {
        /// <summary>
        /// 单据编号
        /// </summary>
       public string DocID{get;set;}

       /// <summary>
       /// 单据类型Code
       /// </summary>
       public string DocCode { get; set; }

        /// <summary>
        /// 单据开出时间
        /// </summary>
        public DateTime DocMakeTime{get;set;}

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime DocAuditTime{get;set;}

        /// <summary>
        /// 复合时间
        /// </summary>
        public DateTime DocSecondAuditTime{get;set;}

        /// <summary>
        /// 开出人
        /// </summary>
        public string DocMaker{get;set;}
        /// <summary>
        /// 审核人
        /// </summary>
        public string DocAuditer{get;set;}
        
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WareHouseName{get;set;}

        /// <summary>
        /// 库位名称
        /// </summary>
        public string SeatName { get; set; }

        /// <summary>
        /// 店铺id
        /// </summary>
        public string StoreID{get;set;}

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchCode{get;set;}

        /// <summary>
        /// 操作人编号
        /// </summary>        
        public string OperateNum{get;set;}

        /// <summary>
        /// 是否关闭
        /// </summary>
        public int CloseFlag{get;set;}

        /// <summary>
        /// 关闭时间
        /// </summary>
        public DateTime CloseDate{get;set;}

        /// <summary>
        /// 单据类型
        /// </summary>
        public string DocTypeNames{get;set;}

        /// <summary>
        /// 供应商
        /// </summary>
        public string ProviderName{get;set;}

        /// <summary>
        /// 是否红单
        /// </summary>
        public int IsRubric { get; set; }

        /// <summary>
        /// 单据状态
        /// </summary>
        public int StateFlag{get;set;}

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalMoney{get;set;}

        /// <summary>
        /// 总pv
        /// </summary>
        public decimal Totalpv{get;set;}

        /// <summary>
        /// 币种名称
        /// </summary>
        public string CurrencyName{get;set;}

        /// <summary>
        /// 备注
        /// </summary>
        public string Note{get;set;}

        /// <summary>
        /// 发货地址
        /// </summary>
        public string Address { get; set; }
    }
}
