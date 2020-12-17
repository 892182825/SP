using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Model
{
    public class StoreAccountModel
    {
        private int iD;
        private string number;
        private DateTime happenTime;
        private double happenMoney;
        private double balanceMoney;
        private int direction;
        private int sfType;
        private int kmType;
        private string remark;

        /// <summary>
        /// ID
        /// </summary>
        public int Id
        {
            get { return iD; }
            set { iD = value; }
        }
       
        /// <summary>
        /// 会员或店铺编号
        /// </summary>
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        
        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime HappenTime
        {
            get { return happenTime; }
            set { happenTime = value; }
        }
        
        /// <summary>
        /// 发生金额：正数为账户增加，负为账户减少
        /// </summary>
        public double HappenMoney
        {
            get { return happenMoney; }
            set { happenMoney = value; }
        }
       
        /// <summary>
        /// 帐户余额
        /// </summary>
        public double BalanceMoney
        {
            get { return balanceMoney; }
            set { balanceMoney = value; }
        }
        
        /// <summary>
        /// 方向:账户增加为0，账户减少1
        /// </summary>
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }
       
        /// <summary>
        /// 0为会员交易，1为店铺交易
        /// </summary>
        public int SfType
        {
            get { return sfType; }
            set { sfType = value; }
        }
       
        /// <summary>
        /// 科目
        /// </summary>
        public int KmType
        {
            get { return kmType; }
            set { kmType = value; }
        }
        
        /// <summary>
        /// 摘要
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        
    }
    /// <summary>
    /// 交易类型
    /// </summary>
    public enum D_AccountSftype
    {
        /// <summary>
        /// 会员现金交易
        /// </summary>
        MemberType=0,

        MemberTypeFx=2,

        MemberTypeFxth=3,
        /// <summary>
        /// 会员消费交易
        /// </summary>
        MemberCoshType =4,

        /// <summary>
        /// 会员报单账户
        /// </summary>
        MemberTypeBd=6,
        /// <summary>
        /// 支付订单
        /// </summary>
        
         StoreType = 1,
        /// <summary>
        /// 店铺订货款
        /// </summary>
        StoreDingHuokuan =10,

        /// <summary>
        /// 店铺周转款
        /// </summary>
        StoreZhouZhuankuan=11,

        
        /// <summary>
        /// 分公司
        /// </summary>
        Branch=2,
        /// <summary>
        /// 总公司
        /// </summary>
        Company=3,
        /// <summary>
        /// 保险钱包
        /// </summary>
        zzye=8,

        /// <summary>
        /// 动态钱包
        /// </summary>
        usdtjj = 5


    }
    /// <summary>
    /// 科目枚举
    /// </summary>
    public enum D_AccountKmtype
    {
        /// <summary>
        /// 充值-人工确认
        /// </summary>
        [Description("充值-人工确认")]
        RechargeByManager=1,
        /// <summary>
        /// 充值-在线支付
        /// </summary>
        [Description("充值-在线支付")]
        RechargeByOnline=2,
        /// <summary>
        /// 充值-离线汇款
        /// </summary>
        [Description("充值-离线汇款")]
        RechargeByRemittance=3,
        /// <summary>
        /// 充值-电子转账
        /// </summary>
        [Description("充值-电子转账")]
        RechargeByTransfer=4,
        /// <summary>
        /// 电子转账
        /// </summary>
        [Description("电子转账")]
        AccountTransfer=5,
        /// <summary>
        /// 注册报单
        /// </summary>
        [Description("注册报单")]
        Declarations=6,

        /// <summary>
        /// 复消报单
        /// </summary>
        [Description("复消报单")]
        Declarationsfuxiao = 7, 
      
        /// <summary>
        /// 奖金发放
        /// </summary>
        [Description("奖金发放")]
        Release = 8,

        /// <summary>
        /// 撤销发放
        /// </summary>
        [Description("撤销发放")]
        Revocation = 9,

        /// <summary>
        /// 会员提现
        /// </summary>
        [Description("会员提现")]
        Memberwithdraw=10,

        /// <summary>
        /// 提现扣税
        /// </summary>
        [Description("提现扣税")]
        MemberwithdrawFix = 11,

        /// <summary>
        /// 添加扣款
        /// </summary>
        [Description("添加扣款")]
        AddMoneycut=12,

        /// <summary>
        /// 添加补款
        /// </summary>
        [Description("添加补款")]
        AddMoneyget=13,

       /// <summary>
        /// 奖金汇兑
        /// </summary>
        [Description("奖金汇兑")]
        Cash=14, 
 
        /// <summary>
        /// 奖金退回
        /// </summary>
        [Description("奖金退回")]
        BonusReturn = 15,

        /// <summary>
        /// 删除退回
        /// </summary>
        [Description("删除退回")]
        RemoveBonus = 16,

        /// <summary>
        /// 服务机构订货
        /// </summary>
        [Description("服务机构订货")]
        StoreOrderout = 17,

        /// <summary>
        /// 退换货扣款
        /// </summary>
        [Description("退换货扣款")]
        ReturnCharge = 18,


        /// <summary>
        /// 退换货补款
        /// </summary>
        [Description("退换货补款")]
        ReturnRebate = 19,

        /// <summary>
        /// 删除报单补款
        /// </summary>
        [Description("删除报单补款")]
        OrderDelete = 20,


        /// <summary>
        /// 修改报单补款
        /// </summary>
        [Description("修改报单补款")]
        OrderUpdateIn = 21,

        /// <summary>
        /// 修改报单扣款
        /// </summary>
        [Description("修改报单扣款")]
        OrderUpdateOut = 22,
     
    }
    
    /// <summary>
    /// 账户进出
    /// </summary>
    public enum DirectionEnum
    {
        /// <summary>
        /// 账户减少
        /// </summary>
        AccountReduced=1,
        /// <summary>
        /// 账户增加
        /// </summary>
        AccountsIncreased=0
    }
    /// <summary>
    /// 会员账户类型（0：消费账户。1：现金账户）
    /// </summary>
    public enum D_Sftype
    {
        /// <summary>
        /// 消费账户
        /// </summary>
        EleAccount = 0,
        /// <summary>
        /// 现金账户
        /// </summary>
        BounsAccount = 1,

        //报单账户
        baodanFTC=6,

        //复消账户
        CancellationAccount=2,

        //复消提货账户
         CancellationofgoodsAccount=3,

         usdtjj = 5,
        //保险钱包
        zzye=8
    }
    /// <summary>
    /// 店铺账户类型（0：订货款。1：周转款）
    /// </summary>
    public enum S_Sftype { 
        /// <summary>
        /// 订货款
        /// </summary>
        dianhuo=0,
        /// <summary>
        /// 周转款
        /// </summary>
        zhouzhuan=1,
    }

    /// <summary>
    /// 支付方式
    /// </summary>
    public enum PaymentEnum
    {
        /// <summary>
        /// 在线支付
        /// </summary>
        CompanyRecord=0,
        /// <summary>
        /// 普通汇款
        /// </summary>
        BankTransfer=1,
        /// <summary>
        /// 预收账款(人工汇款确认)
        /// </summary>
        Alipay=2,
    }

}
