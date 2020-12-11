using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Web.UI.WebControls;


/**
 * 创建人：     郑华超
 * 创建时间：   2009-08-30
 * 修改人：     汪  华
 * 修改时间：   2009-08-31
 * 文件名：     WebSiteBaseDataConfig
*/

namespace Model.Other
{
    public class WebSiteBaseDataConfig
    {

        #region 常量定义
        public static EnumRegisterMemberType EnumRegisterMemberType = EnumRegisterMemberType.UseMoneyRegister;
        public static EnumRegisterStoreType EnumRegisterStoreType = EnumRegisterStoreType.None;
        /// <summary>
        /// 专门用于给会员自由购货的店铺
        /// </summary> 
        public static string SystemStoreForMember = "9999999999";
        #endregion
    }
    #region 各种登陆状态
    /// <summary>
    /// 各种登陆状态
    /// </summary>
    public enum EnumPermissions
    {
        ALLOW,
        RefuseG,
        RefuseD,
        RefuseH,
        EmptySessionofG,
        EmptySessionofD,
        EmptySessionofH,
        NoPermission
    }
    #endregion

    #region 网站的用户类型
    /// <summary>
    /// 网站的用户
    /// </summary>
    public enum EnumUserType
    {
        Member, Store, Company, None
    }
    #endregion

    #region 公司部分的权限，权限部分的要和此一一对应
    /// <summary>
    /// 公司部分的权限，权限部分的要和此一一对应
    /// </summary>
    /// 

    public enum EnumCompanyPermission
    {
        cw_huiduiConfirm = 0,
        cw_huiduiDownLoad = 0,
        BalanceCompanyBalance = 0,
        Balancetk_Config = 0,
        BalanceConfig = 0,
        #region 除去六大模块外的权限配置--任何人不能防问
        Other_simuBack = 161,
        #endregion

        #region ***********************************客户管理********************************************

        //以下是修改的部分

        /// <summary>
        /// 修改会员信息(会员信息编辑)
        /// </summary>
        CustomerMembermoidy = 1101,

        /// <summary>
        /// 会员基本信息修改
        /// </summary>
        CustomerModifyMemberInfoKaHao = 1102,

        /// <summary>
        /// 会员高级信息修改
        /// </summary>
        CustomerModifyMemberInfoZheHao = 1103,

        /// <summary>
        ///  查看会员信息(会员信息查询)
        /// </summary>
        CustomerQureyMember = 1104,

        /// <summary>
        /// 会员密码重置
        /// </summary>
        CustomerQueryMemberPassword = 1105,
        /// <summary>
        /// 零购注册
        /// </summary>
        Companyregister = 1106,

        /// <summary>0
        /// 会员密码重置
        /// </summary>
        CustomerChangeExcept = 1107,

        /// <summary>
        /// 批量修改
        /// </summary>
        ModifyOldConsume=1108,

        /// <summary>
        /// 修改店铺信息
        /// </summary>
        CustomerStoreManage = 1201,

        /// <summary>
        /// 修改店铺信息(店铺信息编辑)
        /// </summary>
        CustomerUpdateStore = 1201,

        /// <summary>
        /// 店铺账号编辑(店铺信息编辑)
        /// </summary>
        CustomerStoreAccount = 1202,


        /// <summary>
        /// 删除店铺信息(店铺信息删除)
        /// </summary>
        CustomerStoreManageDelete = 1202,

        /// <summary>
        /// 店铺信息查询
        /// </summary>
        CustomerQueryStore = 1203,

        /// <summary>
        /// 店铺密码重置
        /// </summary>
        CustomerQueryStorePassword = 1204,

        /// <summary>
        /// 会员所属店铺重置
        /// </summary>
        CustomerQueryMemberStoreReset = 1206,

        /// <summary>
        /// 店铺注册确认
        /// </summary>
        CustomerAuditingStoreRegister = 1205,

        /// <summary>
        ///注册店铺审核(店铺注册确认)
        /// </summary>
        CustomerAuditingStoreRegisterAuditing = 1205,

        /// <summary>
        /// 注册店铺删除(店铺注册确认)
        /// </summary>
        CustomerAuditingStoreRegisterDelete = 1205,

        /// <summary>
        /// 添加新店铺(店铺注册确认)
        /// </summary>
        CustomerCreateStore = 1205,
        /// <summary>
        /// 店铺定级
        /// </summary>
        CustomerStoreLevel = 1208,
        /// <summary>
        /// 店铺定级浏览
        /// </summary>
        StoreLevelQuery = 6317,

        /// <summary>
        /// 供应商管理
        /// </summary>
        CustomerProviderViewEdit = 2101,

        /// <summary>
        /// 供应商修改(供应商管理)
        /// </summary>
        CustomerProviderViewEditEdit = 2102,

        /// <summary>
        /// 供应商删除(供应商管理)
        /// </summary>
        CustomerProviderViewEditDelete = 2103,

        /// <summary>
        /// 供应商管理（添加供应商管理）
        /// </summary>
        CustomerProvider_Add = 2104,


        /// <summary>
        /// 第三方物流
        /// </summary>
        CustomerThirdLogistics = 3302,

        /// <summary>
        /// 第三方物流修改(第三方物流)
        /// </summary>
        CustomerThirdLogisticsEdit = 3303,

        /// <summary>
        /// 第三方物流删除(第三方物流)
        /// </summary>
        CustomerThirdLogisticsDelete = 3304,

        /// <summary>
        /// 添加第三方物流
        /// </summary>
        CustomerThirdLogisticsAdd = 3305,


        /// <summary>
        /// 店铺汇款汇总
        /// </summary>
        CustomerStoreHuikuanTotal = 1305,

        /// <summary>
        /// 店铺汇款明细
        /// </summary>
        CustomerStoreHuikuanmingxi = 1306,

        /// <summary>
        /// 注销会员
        /// </summary>
        MemberOffView = 1120,

        /// <summary>
        /// 恢复注销会员
        /// </summary>
        MemberRestoreoff = 1121,

        /// <summary>
        /// 冻结会员查询
        /// </summary>
        MemberSPOffView = 1122,

        /// <summary>
        /// 设置冻结会员
        /// </summary>
        MemberSPOff = 1123,

        /// <summary>
        /// 解冻会员查询
        /// </summary>
        MemberRestoreSPoff = 1124,

        /// <summary>
        /// 注销服务机构
        /// </summary>
        StoreOffView=1210,

        /// <summary>
        /// 恢复注销服务机构
        /// </summary>
        StoreRestoreoff = 1211,

        /// <summary>
        /// 注销会员查询
        /// </summary>
        MemberOffList=1212,

        #endregion

        #region ***********************************库存管理********************************************

        /// <summary>
        /// 增加新品(产品树)
        /// </summary>
        StorageProductTreeManager = 2105,

        /// <summary>
        /// 增加新品(添加新品页面)
        /// </summary>
        StorageProductTreeAdd = 2106,

        /// <summary>
        /// 添加新类(增加新品)
        /// </summary>
        StorageProductTreeManageAddStyle = 2107,

        /// <summary>
        /// 添加新品(增加新品)
        /// </summary>
        StorageProductTreeManagerAddProduct = 2108,

        /// <summary>
        /// 修改类(增加新品)
        /// </summary>
        StorageProductTreeManagerEditStyle = 2109,

        /// <summary>
        /// 删除类(增加新品)
        /// </summary>
        StorageProductTreeManagerDeleteStyle = 2110,

        /// <summary>
        ///修改产品(增加新品) 
        /// </summary>
        StorageProductTreeManagerEditProduct = 2111,

        /// <summary>
        /// 删除产品(增加新品)
        /// </summary>
        StorageProductTreeManagerDeleteProduct = 2112,

        /// <summary>
        /// 组合产品
        /// </summary>
        StorageAdminCombineProduct = 2113,

        /// <summary>
        /// 采购入库
        /// </summary>
        StorageStorageIn = 2201,

        /// <summary>
        /// 入库审批
        /// </summary>
        StorageStorageInBrowse = 2202,

        /// <summary>
        /// 审核入库单(入库审批)
        /// </summary>
        StorageStorageInBrowseAuditing = 2203,

        /// <summary>
        ///无效入库单(入库审批) 
        /// </summary>
        StorageStorageInBrowseNouse = 2204,

        /// <summary>
        /// 删除入库单(入库审批)
        /// </summary>
        StorageStorageInBrowseDelete = 2205,

        /// <summary>
        /// 编辑入库单(入库审批)
        /// </summary>
        StorageStorageInBrowseEdit = 2206,

        /// <summary>
        /// 入库查询
        /// </summary>
        StorageInLibSearch = 2301,

        /// <summary>
        /// 出库查询
        /// </summary>
        StorageQueryOutStorageOrders = 2302,

        /// <summary>
        /// 库存查询
        /// </summary>
        StorageKcglKcqk = 2303,

        /// <summary>
        /// 库存单据
        /// </summary>
        StorageBrowseBills = 2304,

        /// <summary>
        /// 打印单据
        /// </summary>
        StorageBrowseBillsPrint = 2305,

        /// <summary>
        /// 库存报损
        /// </summary>
        StorageAddReportDamage = 2207,

        /// <summary>
        /// 库存报溢
        /// </summary>
        StorageAddReportProfit = 2208,

        /// <summary>
        /// 产品调拨
        /// </summary>
        StorageAddReWareHouse = 2209,




        #endregion

        #region ***********************************物流管理********************************************
        /// <summary>
        /// 订单编辑
        /// </summary>
        LogisticsBrowseStoreOrders = 3101,

        /// <summary>
        /// 删除订单(订单编辑)
        /// </summary>
        LogisticsBrowseStoreOrdersDelete = 3102,

        /// <summary>
        /// 订单出库
        /// </summary>
        LogisticsBillOutOrder = 3104,

        /// <summary>
        /// 生成出库单(订单出库)
        /// </summary>
        LogisticsBillOutOrderOut = 3105,

        /// <summary>
        /// 撤消出库单(订单出库)
        /// </summary>
        LogisticsBillOutOrderDelete = 305,

        /// <summary>
        /// 订单发货
        /// </summary>
        LogisticsCompanyConsign = 3106,

        /// <summary>
        /// 生成发货单
        /// </summary>
        LogisticsCompanyConsignOut = 3107,

        /// <summary>
        /// 发货跟踪
        /// </summary>
        LogisticswuliuStateShow = 3301,

        /// <summary>
        /// 退货处理
        /// </summary>
        LogisticsRefundmentOrderBrowse = 3201,

        /// <summary>
        /// 添加退货单
        /// </summary>
        AddRefundmentOrder = 3202,


        /// <summary>
        /// 审核退货单(退货处理)
        /// </summary>
        //LogisticsRefundmentOrderBrowseAuditing = 309,

        /// <summary>
        /// 无效退货单(退货处理)
        /// </summary>
        //LogisticsRefundmentOrderBrowseNullity = 310,


        /// <summary>
        /// 换货处理
        /// </summary>
        LogisticsDisplaceGoodsBrowse = 3203,

        /// <summary>
        /// 添加换货单
        /// </summary>
        DisplaceGoodsAdd = 3204,

        /// <summary>
        /// 审核换货单(换货处理)
        /// </summary>
        //LogisticsDisplaceGoodsBrowseAuditing = 312,

        /// <summary>
        /// 无效换货单(换货处理)
        /// </summary>
        //LogisticsDisplaceGoodsBrowseNullity = 313,

        /// <summary>
        /// 编辑换货单(换货处理)
        /// </summary>
        //LogisticsDisplaceGoodsBrowseEdit = 314,


        /// <summary>
        ///删除换货单(换货处理) 
        /// </summary>
        //LogisticsDisplaceGoodsBrowseDelete = 315,


        #endregion

        #region ***********************************财务管理********************************************
        //以下是修改的部分

        /// <summary>
        /// 预收帐款
        /// </summary>
        FinanceAuditingStoreAccount = 4101,

        /// <summary>
        /// 审核预收帐款(预收帐款)
        /// </summary>
        FinanceAuditingStoreAccountAuditing = 4102,

        /// <summary>
        /// 订单支付
        /// </summary>
        FinanceChAgainOrder = 3103,

        /// <summary>
        /// 应收帐款
        /// </summary>
        FinanceStoreTotalAccount = 4103,

        /// <summary>
        /// 添加应收账款
        /// </summary>
        FinanceAddStoreTotalAccount = 4104,

        /// <summary>
        /// 电子转帐
        /// </summary>
        FinanceMoneymanage = 4105,

        /// <summary>
        /// 转帐(电子转帐)
        /// </summary>
        FinanceMoneymanageSubmit = 4106,

        /// <summary>
        /// 汇率历史记录
        /// </summary>
        FinanceReceiveDayPrice = 4403,
        ///// <summary>
        ///// 转帐管理
        ///// </summary>
        //FinanceReceiveConfirm = 4401,
        ///// <summary>
        ///// 审核转账
        ///// </summary>
        //FinanceSHReceiveConfirm = 4402,
        /// <summary>
        /// 转账记录查询
        /// </summary>
        FinanceSelReceiveConfirm = 4403,

        /// <summary>
        /// 拨出率显示
        /// </summary>
        FinanceFinanceStat = 4207,

        /// <summary>
        /// 拨出率调控
        /// </summary>
        FinanceBochulvTiaokong = 4208,

        /// <summary>
        /// 工资发放
        /// </summary>
        FinanceJiangjinshezhi = 4301,

        /// <summary>
        /// 工资发布
        /// </summary>
        FinanceFafang = 4302,

        /// <summary>
        ///工资汇兑 
        /// </summary>
        FinanceGrantReward = 4303,

        /// <summary>
        /// 工资退回
        /// </summary>
        FinanceHuiDuiChongHong = 4304,

        /// <summary>
        /// 会员退回工资(工资退回)
        /// </summary>
        FinanceTianJiaChongHong = 4305,

        /// <summary>
        /// 删除工资退回单
        /// </summary>
        FinanceDelTuihui = 4306,

        /// <summary>
        /// 汇款查询
        /// </summary>
        FinanceHuikuanMingxi = 4404,

        /// <summary>
        /// 工资查询
        /// </summary>
        FinanceGongzimingxi = 4405,

        /// <summary>
        /// 店铺账户明细
        /// </summary>
        ResultBrowse2 = 4401,

        /// <summary>
        /// 会员账户明细
        /// </summary>
        ResultBrowse1 = 4402,

        /// <summary>
        /// 扣补款管理
        /// </summary>
        FinanceKoukuanMingxi = 4107,
        ///// <summary>
        ///// 添加扣款(扣补款管理)
        ///// </summary>
        //FinanceKoukuanAddkou = 4108,
        /// <summary>
        /// 添加补扣款管理
        /// </summary>
        FinanceKoukuanAddbu = 4109,

        /// <summary>
        /// 账户管理
        /// </summary>
        FinanceSetCompanyAccount = 4113,

        /// <summary>
        /// 设置汇入帐号 
        /// </summary>
        ManNetBankcard = 4120,


        /// <summary>
        /// 添加账户
        /// </summary>
        FinanceSetAddCompanyAccount = 4114,
        /// <summary>
        /// 修改账户
        /// </summary>
        FinanceSetUPCompanyAccount = 4115,
        /// <summary>
        /// 删除管理
        /// </summary>
        FinanceSetDelCompanyAccount = 4116,
    /// <summary>
        /// 提现申请审核
        /// </summary>
        WithdrawManager = 4117,
 /// <summary>
        /// 汇款手工确认
        /// </summary>
       RemittanceHandManager = 6309,


        /// <summary>
        /// 退货款管理
        /// </summary>
        FinanceTuihuokuanManage = 4111,

        /// <summary>
        /// 添加退货款
        /// </summary>
        FinanceAddTuihuokuan = 4110,
     /// <summary>
        ///金流管理
        /// </summary>
        AccountFlowManager = 4500,
        /// <summary>
        ///汇款管理
        /// </summary>
        AccountFlowManagerHK = 4501,
        /// <summary>
        ///纠纷单管理
        /// </summary>
        AccountFlowManagerWYJF = 4502,
        /// <summary>
        ///提现管理
        /// </summary>
        AccountFlowManagerTX = 4503,
        /// <summary>
        ///违约金统计
        /// </summary>
        AccountFlowManagerWYJTj = 4504,
   



        #endregion

        #region ***********************************报表中心********************************************


        //以下是新增加的，报表中心：116-136，预留5个

        /// <summary>
        /// 汇款汇总表
        /// </summary>	
        ReportMoneyReport = 501,
        /// <summary>
        /// 汇款明细表
        /// </summary>	
        ReportMoneyDetailReport = 502,
        /// <summary>
        /// 订单汇总表
        /// </summary>	
        ReportOrderReport = 1307,
        /// <summary>
        /// 订单明细表
        /// </summary>	
        ReportOrderDetail = 1308,

        /// <summary>
        /// 会员销售汇总
        /// </summary>	
        ReportBaodanReport = 1312,

        /// <summary>
        /// 销售明细表
        /// </summary>	
        ReportBaodanDetailReport = 2306,
        /// <summary>
        /// 产品明细表
        /// </summary>	
        ReportProductDetail = 2306,

        /// <summary>
        /// 仓库产品明细表
        /// </summary>	
        ReportStockReport1 = 508,

        /// <summary>
        /// 产品仓库明细表
        /// </summary>	
        ReportStockReport3 = 2308,

        /// <summary>
        /// 产品销售明细表
        /// </summary>
        ReportStockDetailReport = 2309,

        /// <summary>
        /// 店铺各产品明细表
        /// </summary>	
        ReportStockReport2 = 1309,

        /// <summary>
        /// 产品店铺明细表
        /// </summary>	
        ReportStockReport4 = 1310,
        /// <summary>
        /// 会员明细表
        /// </summary>	
        ReportMemberDetail2 = 1301,
        /// <summary>
        /// 会员区域明细表
        /// </summary>	
        ReportMemberReport = 1302,
        /// <summary>
        /// 店铺明细表
        /// </summary>	
        ReportStoreDetail2 = 1303,
        /// <summary>
        /// 店铺区域明细表
        /// </summary>	
        ReportStorereport = 1304,


        /// <summary>
        /// 产品店铺明细表
        /// </summary>	
        Company_ProductWareHouseDetails = 2308,

        /// <summary>
        /// 仓库各产品明细
        /// </summary>	
        Company_WareHouseProductDetails = 2307,

        #endregion

        #region ***********************************结算中心********************************************
        /// <summary>
        /// 调控团队
        /// </summary>
        Balancetk_Team = 602,
        /// <summary>
        /// 工资结算
        /// </summary>
        FinanceJiesuan = 4201,

        /// <summary>
        /// 创建新一期
        /// </summary>
        FinanceNewQi = 4202,

        /// <summary>
        /// 系统结算
        /// </summary>
        FinanceXitongjiesuan = 4203,

        /// <summary>
        /// 结算参数
        /// </summary>
        FinanceJiesuanchanshu = 4204,

        /// <summary>
        /// 调控结算
        /// </summary>
        FinanceTiaokong = 4205,

        /// <summary>
        /// 调控参数
        /// </summary>
        FinanceTiaokongchanshu = 4206,

        /// 报单浏览
        /// </summary>
        BalanceBrowseMemberOrders = 1110,
        /// <summary>
        /// <summary>
        /// 审核报单(报单浏览)
        /// </summary>
        BalanceBrowseMemberOrdersConfirm = 1111,
        /// 修改会员(报单浏览)
        /// </summary>
        BalanceBrowseMemberOrdersEdit = 1112,
        /// <summary>
        /// 删除会员(报单浏览)
        /// </summary>
        BalanceBrowseMemberOrdersDelete = 1113,
        /// <summary>
        /// 店铺网络
        /// </summary>
        BalanceQueryNetworkViewDP = 1207,
        /// <summary>
        /// 推荐网络
        /// </summary>
        BalanceQueryTuiJianNetworkView = 1114,
        /// <summary>
        /// 安置网络
        /// </summary>
        BalanceQueryAnZhiNetworkView = 1115,

        /// <summary>
        ///链路网络
        /// </summary>
        BalanceQueryLinkView = 1116,

        /// <summary>
        /// 高级查询
        /// </summary>
        BalanceDetialQuery = 1119,

        /// <summary>
        /// 网络调整
        /// </summary>
        BalanceDefault = 1117,
        /// <summary>
        /// 编号分配
        /// </summary>
        BalanceVIPCardManage = 1209,
        /// <summary>
        /// 新增卡号(编号分配)
        /// </summary>
        BalanceVIPCardManageAdd = 616,

        /// <summary>
        /// 重新分配(编号分配)
        /// </summary>
        BalanceVIPCardManageReset = 617,
        /// <summary>
        /// 会员定级浏览
        /// </summary>
        BalanceMemberLevelModify = 1118,
        /// <summary>
        /// 会员定级
        /// </summary>
        MemberGrading = 6316,
        /// <summary>
        /// 店铺定级
        /// </summary>
        BalanceStoreLevelModify = 1208,
        /// <summary>
        /// 特殊会员注册
        /// </summary>
        BalanceRegisterMember1 = 1106,
        /// <summary>
        /// 特殊会员报单
        /// </summary>
        BalanceMemberOrderAgain1 = 621,
        
        #endregion

        #region ***********************************信息管理********************************************
        //以下是修改的部分，信息管理：159-187，预留5个

        /// <summary>
        /// 文件上传
        /// </summary>
        ManageResource = 5101,

        /// <summary>
        /// 上传资料
        /// </summary>
        ManageResource_S = 5102,

        /// <summary>
        /// 下载资料
        /// </summary>
        XZManageResource = 5103,

        /// <summary>
        /// 内部公告信息发布
        /// </summary>
        ManageMessage1 = 5104,
        /// <summary>
        /// 公告查询
        /// </summary>
        ManageQueryGongGao = 5105,

        /// <summary>
        /// 公告修改
        /// </summary>
        ManageQueryGongGaoUpd = 5106,

        /// <summary>
        /// 公告删除
        /// </summary>
        ManageQueryGongGaoDel = 5107,


        /// <summary>
        /// 写邮件
        /// </summary>
        ManageMessage = 5201,
        /// <summary>
        /// 收件箱
        /// </summary>
        ManageMessageRecive = 5202,

        /// <summary>
        /// 收件箱转发
        /// </summary>
        ManageMessageRecive_ZF = 5203,

        /// <summary>
        /// 收件箱删除邮件(收件箱)
        /// </summary>
        ManageMessageReciveDelete = 5204,
        /// <summary>
        /// 发件箱
        /// </summary>
        ManageMessageSend = 5205,
        /// <summary>
        /// 发件箱删除邮件(发件箱)
        /// </summary>
        ManageMessageSendDelete = 5206,
        /// <summary>
        /// 废件箱
        /// </summary>
        ManageMessageDrop = 5207,
        /// <summary>
        /// 废件箱删除邮件(废件箱)
        /// </summary>
        ManageMessageDropDelete = 710,
        /// <summary>
        /// 短信群发
        /// </summary>
        GroupSend = 5301,

        /// <summary>
        /// 接收查询
        /// </summary>
        ReceiveBox = 712,

        /// <summary>
        /// 发送查询
        /// </summary>
        QuerySMS = 5303,      //SendBox

        /// <summary>
        /// 待发短信
        /// </summary>
        WaitSms = 714,

        /// <summary>
        /// 权限设置
        /// </summary>
        RightSet = 715,

        /// <summary>
        /// 指令集合
        /// </summary>
        InstructionsListx = 716,

        /// <summary>
        /// 短信互动
        /// </summary>
        /// 

        /// <summary>
        /// 短信费用
        /// </summary>


        #endregion

        #region***********************************系统管理********************************************

        //以下是修改部分

        /// <summary>
        /// 密码修改
        /// </summary>
        SystemPwdModify = 6111,
        /// <summary>
        /// 数据备份
        /// </summary>
        SystemBackup = 6112,

        //// <summary>
        /// 参数设置
        /// </summary>
        SystemBscoManage = 6201,

        /// <summary>
        /// 日志管理
        /// </summary>
        SystemChangeLogsQuery = 6113,

        /// <summary>
        /// 修改日志
        /// </summary>
        SystemChangeLogssEdit = 6114,

        /// <summary>
        /// 备份日志
        /// </summary>
        SystemChangeLogsBackup = 6115,

        /// <summary>
        /// 加密设置
        /// </summary>
        EncryptionSetting=6206,

        /// <summary>
        /// 删除日志
        /// </summary>
        SystemChangeLogsDelete = 6116,

        /// <summary>
        /// 添加语言
        /// </summary>
        SystemIntoDict = 6215,

        /// <summary>
        /// 翻译多语言
        /// </summary>
        SystemIntoDictEdit = 806,

        /// <summary>
        /// 汇率设置
        /// </summary>
        SystemCurrencyRate = 6218,

        /// <summary>
        /// 设置汇率操作(汇率设置)
        /// </summary>
        SystemCurrencyRateCountryEdit = 6219,

        /// <summary>
        ///编辑国家或地区
        /// </summary>
        SystemCurrencyRateCountryDelete = 809,


        /// <summary>
        /// 期数显示
        /// </summary>
        SystemQishuVSdate = 6221,


        /// <summary>
        /// 修改期数时间（期数显示）
        /// </summary>
        SystemQishuVSdateEdit = 6222,

        /// <summary>
        /// 支付设置
        /// </summary>
        SystemPayment = 6205,

        /// <summary>
        /// 世界时区设置
        /// </summary>
        SetWorldTimeRight = 6204,

        #endregion

        #region***********************************安全控制********************************************
        ///安全控制权限140-159
        ///
        ///
        /// <summary>
        /// 安全控制----管理员权限分配
        /// </summary>
        /// 
        SafeRightManage = 6101,

        /// <summary>
        /// 安全控制----修改角色
        /// </summary>
        /// 
        SafeUpdateJs = 6103,

        /// <summary>
        /// 安全控制----删除角色
        /// </summary>
        /// 
        SafeDeleteJs = 6104,

        /// <summary>
        /// 安全控制----编辑管理员权限分配 (管理员权限分配)
        /// </summary>
        SafeRightManageEdit = 6102,
        /// <summary>
        /// 安全控制----删除管理员权限分配 (管理员权限分配)
        /// </summary>
        SafeRightManageDelete = 6110,

        /// <summary>
        /// 安全控制----修改管理员权限分配 (管理员权限分配)
        /// </summary>
        SafeRightManageUpdate = 6109,

        /// <summary>
        /// 安全控制----管理公司部门权限 (管理员权限分配)
        /// </summary>
        SafeRightDpetManage = 6105,

        /// <summary>
        /// 安全控制----修改部门权限 (管理员权限分配)
        /// </summary>
        SafeUpdateDpet = 6106,

        /// <summary>
        /// 安全控制----删除部门权限 (管理员权限分配)
        /// </summary>
        SafeDeleteDpet = 6106,

        /// <summary>
        /// 安全控制----系统总开关
        /// </summary>
        /// 
        SafeLoginSetting = 6202,

        /// <summary>
        /// 安全控制----查询权限控制
        /// </summary>
        ///
        SafeDetailQuerySetting = 6203,

        /// <summary>
        /// 安全控制----应急措施
        /// </summary>
        ///



        /// <summary>
        /// 安全控制----财务安全处理
        /// </summary>
        ///


        /// <summary>
        /// 安全控制----服务器安全处理
        /// </summary>
        ///



        /// <summary>
        /// 安全控制----地区登录限制
        /// </summary>
        ///
        SafeLoginSettingArea = 6301,
        /// <summary>
        /// 安全控制----IP登录限制
        /// </summary>
        ///
        SafeLoginSettingIP = 6302,


        /// <summary>
        /// 安全控制----会员登录限制
        /// </summary>
        ///
        SafeLoginSettingMember = 6303,


        /// <summary>
        /// 安全控制----店铺登录限制
        /// </summary>
        ///
        SafeLoginSettingStore = 6304,


        /// <summary>
        /// 安全控制----店辖会员登录限制
        /// </summary>
        ///
        SafeLoginSettingStoreArea = 6305,

        /// <summary>
        /// 遥控设置
        /// </summary>
        SetRemote = 6223,

        /// <summary>
        /// 安全控制----网络团队登录限制
        /// </summary>
        ///
        SafeLoginSettingNetWork = 6306,


        /// <summary>
        /// 安全控制----报单设置
        /// </summary>
        ///
        SafeGlobalOrderSettings = 913,

        #endregion

        #region***********************************仓库控制********************************************
        //仓库控制权限从7001起


        #endregion


    }
    #endregion


    #region 单据类型定义
    /// <summary>
    /// 单据的类型
    /// </summary>
    public enum EnumOrderFormType
    {
        /// <summary>
        /// 入库单
        /// </summary>
        InStorage,
        /// <summary>
        /// 入库红单
        /// </summary>
        RedInStorage,
        /// <summary>
        /// 退货入库单
        /// </summary>
        ReturnInStorage,
        /// <summary>
        /// 盘点入库单--报益
        /// </summary>
        CheckInStorage,
        /// <summary>
        /// 出库单
        /// </summary>
        OutStorage,
        /// <summary>
        /// 出库红单
        /// </summary>
        RedOutStorage,
        /// <summary>
        /// 盘点出库单 -- 报损 
        /// </summary>
        CheckOutStorage,
        /// <summary>
        /// 退货出库单
        /// </summary>
        ReturnOutStorage,
        /// <summary>
        /// 调拨单
        /// </summary>
        OneToAnother
    }
    #endregion

    #region 店铺报单的类型：用钱报单还是用货报单
    /// <summary>
    /// 店铺报单的类型：用钱报单还是用货报单还是用没钱报单
    /// </summary>
    public enum EnumRegisterMemberType
    {
        /// <summary>
        /// 用钱报单
        /// </summary>
        UseMoneyRegister = 1,
        /// <summary>
        /// 用库存报单
        /// </summary>
        UseStorageRegister = 2,
        /// <summary>
        /// 禁止报单
        /// </summary>
        None = 3,
        /// <summary>
        /// 没钱报单
        /// </summary>
        UseNoMoneyRegister = 4
    }
    #endregion


    #region 店铺报单的类型：用钱报单还是用货报单
    /// <summary>
    /// 店铺报单的类型：用钱报单还是用货报单还是用没钱报单
    /// </summary>
    public enum EnumRegisterStoreType
    {

        None = 5,
        /// <summary>
        /// 店铺团购报单设置
        /// </summary>
        UseStoreIndent = 6
    }
    #endregion



    #region 检查店铺报单的错误信息类型
    /// <summary>
    /// 检查店铺报单的错误信息类型
    /// </summary>
    public enum EnumCheckWheatherRegister
    {
        StoreMoneyNotEnough,
        StoreMoneyEnough,
        StoreStorageNotEnough,
        StoreStorageEnough,
        StoreStorageNoStorage,
        StoreForBIDRegister,

        /// <summary>
        /// 没钱报单允许,没有超过设置的最大店铺报单金额
        /// </summary>
        StoreNoMoneyEnough,
        /// <summary>
        /// 没钱报单不允许,已经超过设置的最大店铺报单金额
        /// </summary>
        StoreNoMoneyNotEnough,
        Error
    }
    #endregion

    /// <summary>
    /// 报单类型
    /// </summary>
    public enum EnumOrderType
    {
        /// <summary>
        /// 首次报单
        /// </summary>
        FirstOrder = 0,

        /// <summary>
        /// 复消
        /// </summary>
        Fuxiao = 1,

        /// <summary>
        /// 会员自由注册
        /// </summary>
        FreeEnrol = 3,

        /// <summary>
        /// 首次团购
        /// </summary>
        ComityOrderFirst = 5,

        /// <summary>
        /// 再次团购
        /// </summary>
        ComityOrderSecond = 6
    }
}
