using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public  class EnumClass
    {
        
    }
    /// <summary>
        /// 身份枚举
        /// </summary>
        public enum EnumStatus
        {
            /// <summary>
            /// 会员身份
            /// </summary>
            enum_Member = 0,

            /// <summary>
            /// 商家身份
            /// </summary>
            enum_Store = 1,

            /// <summary>
            /// 供应商身份

            /// </summary>
            enum_Supply = 2,

            /// <summary>
            /// 系统管理员身份[总公司]
            /// </summary>
            enum_Company = 3,

            /// <summary>
            /// 分公司

            /// </summary>
            enum_Ware = 4,

            /// <summary>
            /// 配送站
            /// </summary>
            enum_D_info = 5
        }
    public enum CommtypePage
    {
        /// <summary>
        /// sql语句分页
        /// </summary>
        sql = 1,
        /// <summary>
        /// 存储过程分页
        /// </summary>
        proced = 2,
        /// <summary>
        /// 可以排序的存储过程分页

        /// </summary>
        Sort=3
    }

    public enum AscAndDesc
    {
        /// <summary>
        /// 升序
        /// </summary>
        asc = 1,

        /// <summary>
        /// 降序
        /// </summary>
        desc = 0
    }

  

    /// <summary>
    /// 支付接口方式
    /// </summary>
    public enum EnumPayAPI
    {
        /// <summary>
        /// 支付宝1
        /// </summary>
        alipayrecharge = 1,
        /// <summary>
        /// 快钱2
        /// </summary>
        quickrecharge = 2
    }

    public enum  EnumAccountBillType
    {
        /// <summary>
        /// 购物消费
        /// </summary>
        enum_Order=0,

        /// <summary>
        /// 提取现金
        /// </summary>
        enum_Cash=1,

        /// <summary>
        /// 收款
        /// </summary>
        enum_Collection = 2,

        /// <summary>
        /// 充值

        /// </summary>
        enum_Rechange = 3,

        /// <summary>
        /// 发送短信

        /// </summary>
        enum_SendSMS= 4
    }

    /// <summary>
    ///  账户支付:0, 积分支付:1,  在线支付宝支付:2,  在线快钱支付:3
    /// </summary>
    public enum Enum_OrderPayType
    {
        /// <summary>
        /// 账户支付
        /// </summary>
        enum_CashPay=0,

        /// <summary>
        /// 积分支付
        /// </summary>
        enum_PointPay=1,

        /// <summary>
        /// 在线支付宝支付

        /// </summary>
        enum_OnlineAliPay=2,

        /// <summary>
        /// 在线快钱支付
        /// </summary>
        enum_OnlineQuickPay=3
    }

    /// <summary>
    /// 商家可修改的图片类型
    /// </summary>
    public enum Enum_StoreImgType
    {
        /// <summary>
        /// 商家形象照

        /// </summary>
        enum_ImagePhoto = 0,

        /// <summary>
        /// 商家的商城LOGO
        /// </summary>
        enum_StoreLogo = 1,

        /// <summary>
        /// 商家商城头部背景图片
        /// </summary>
        enum_StoreBackground = 2
    }
}
