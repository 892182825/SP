using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 配置文件节点属性值
    /// </summary>
    public enum ConfigProperty
    {
        /// <summary>
        /// ID
        /// </summary>
        config_id = 0,

        /// <summary>
        /// 符号
        /// </summary>
        config_sign = 1,

        /// <summary>
        /// 文字
        /// </summary>
        config_text = 2,

        /// <summary>
        /// 值
        /// </summary>
        config_value = 3
    }

    /// <summary>
    /// 短信类型
    /// </summary>
    public enum SMSCategory
    {
        /// <summary>
        /// 人工指定号码发送
        /// </summary>
        sms_ManualSent =0,

        /// <summary>
        /// 会员注册 
        /// </summary>
        sms_Register=1,

        /// <summary>
        /// 订单发货
        /// </summary>
        sms_Delivery = 2,

        /// <summary>
        /// 汇款审核
        /// </summary>
        sms_RemittanceAudit = 3,

        /// <summary>
        /// 应收账款
        /// </summary>
        sms_Receivables = 4,
         /// <summary>
        /// 会员密码重置
        /// </summary>
        sms_menberPassRest = 5,
        /// <summary>
        /// 店铺密码重置
        /// </summary>
        sms_storePassRest = 6,
        
        /// <summary>
        /// 短信群发
        /// </summary>
        sms_GroupSend = 7,

        /// <summary>
        /// 会员找回密码
        /// </summary>
        sms_memberPassFind=8,


        /// <summary>
        /// 店铺找回密码
        /// </summary>
        sms_storePassFind=9,

       
    }


    /// <summary>
    /// 操作枚举类型
    /// </summary>
    public enum EnumOperateModel
    {
        /// <summary>
        /// 添加模式
        /// /// </summary>
        enum_Add=0,

        /// <summary>
        /// 编辑模式
        /// </summary>
        enum_Edit=0
    }


    /// <summary>
    ///身份枚举类型
    /// </summary>
    public enum EnumStatusModel
    {
        /// <summary>
        /// 总公司身份
        /// /// </summary>
        enum_StatusCompany = 0,

        /// <summary>
        /// 分公司身份
        /// /// </summary>
        enum_StatusBranch = 1,

        /// <summary>
        /// 店铺身份
        /// /// </summary>
        enum_StatusStore = 2,

        /// <summary>
        /// 会员身份
        /// </summary>
        enum_StatusMember = 3
    }

    /// <summary>
    /// 汇款类型：1-订单支付[会员->公司]，2-会员汇款[会员->公司]，3-店铺汇款[店->公司]，........
    /// </summary>
    public enum Enum_RemittancesType
    {
        /// <summary>
        /// 订单支付[会员->公司]
        /// </summary>
        enum_PaymentOrder = 1,

        /// <summary>
        /// 会员汇款[会员->公司]
        /// </summary>
        enum_MemberRemittance = 2,

        /// <summary>
        /// 店铺汇款[店->公司]
        /// </summary>
        enum_StoreRemittance = 3
    }

    public class CommonModel
    {

        public static string OperateBh
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["Company"] != null)
                    return System.Web.HttpContext.Current.Session["Company"].ToString();
                if (System.Web.HttpContext.Current.Session["Store"] != null)
                    return System.Web.HttpContext.Current.Session["Store"].ToString();
                if (System.Web.HttpContext.Current.Session["Member"] != null)
                    return System.Web.HttpContext.Current.Session["Member"].ToString();
                return "";
            }

        }
    }
}
