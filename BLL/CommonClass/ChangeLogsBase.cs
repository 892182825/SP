using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/*
 * 创建者：汪华
 * 创建时间：2009-09-04
 */

namespace BLL.CommonClass
{
    public class ChangeLogsBase
    {

        #region 空构造函数
        public ChangeLogsBase()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //

        }
        #endregion

        #region GetCurrentUserIPAddress 获得用户IP地址
        /// <summary>
        /// 获得用户IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserIPAddress()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }
        #endregion

        #region AddChangeLog 添加修改日志记录函数
        /// <summary>
        /// AddChangeLog 添加修改日志记录函数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="category"></param>
        /// <param name="from"></param>
        /// <param name="fromusertype"></param>
        /// <param name="to"></param>
        /// <param name="tousertype"></param>
        /// <param name="qishu"></param>
        /// <param name="remark"></param>
        /// <param name="recordid"></param>
        /// <returns></returns>
        protected static int AddChangeLog(int type, ChangeCategory category, string from, string fromusertype, string to, string tousertype, int qishu, string remark, int recordid)
        {
            return AddChangeLog(type, DateTime.UtcNow, GetCurrentUserIPAddress(), category, from, fromusertype, to, tousertype, qishu, remark, recordid);
        }

        protected static int AddChangeLogtran(SqlTransaction tran, int type, ChangeCategory category, string from, string fromusertype, string to, string tousertype, int qishu, string remark, int recordid)
        {
            return AddChangeLogtran(tran, type, DateTime.Now, GetCurrentUserIPAddress(), category, from, fromusertype, to, tousertype, qishu, remark, recordid);
        }
        #endregion

        #region AddChangeLog 添加修改日志记录函数(基础)
        /// <summary>
        /// DS2012
        /// 添加修改日志记录过程
        /// </summary>
        /// <param name="type"></param>
        /// <param name="time"></param>
        /// <param name="sip"></param>
        /// <param name="category"></param>
        /// <param name="from"></param>
        /// <param name="fromusertype"></param>
        /// <param name="to"></param>
        /// <param name="tousertype"></param>
        /// <param name="qishu"></param>
        /// <param name="remark"></param>
        /// <param name="recordid"></param>
        /// <returns>返回插入成功的标识列ID,未插入记录返回"-1"</returns>
        private static int AddChangeLog(int type, DateTime time, string sip, ChangeCategory category, string from, string fromusertype, string to, string tousertype, int qishu, string remark, int recordid)
        {
            #region remark
            /*
			@Type smallint,
			@Time datetime,
			@SIP varchar(50),
			@Category varchar(10),
			@From varchar(20),
			@FromUserType varchar(10),
			@To varchar(20),
			@ToUserType varchar(10),
			@Qishu int,
			@Remark varchar(4000),
			@RecordID int output --返回插入成功的标识列ID
			*/
            #endregion

            SqlParameter[] parms = new SqlParameter[11];

            parms[0] = new SqlParameter("@Type", type);
            parms[1] = new SqlParameter("@Time", time);
            parms[2] = new SqlParameter("@SIP", sip);
            parms[3] = new SqlParameter("@Category", (int)category);
            parms[4] = new SqlParameter("@From", from);
            parms[5] = new SqlParameter("@FromUserType", fromusertype);
            parms[6] = new SqlParameter("@To", to);
            parms[7] = new SqlParameter("@ToUserType", tousertype);
            parms[8] = new SqlParameter("@Qishu", qishu);
            parms[9] = new SqlParameter("@Remark", remark);
            parms[10] = new SqlParameter("@RecordID", recordid);
            // 输出参数，返回插入成功的标识列ID
            parms[10].Direction = ParameterDirection.Output;

            // 执行添加修改日志记录过程存储过程
            int rid = DBHelper.ExecuteNonQuery("AddChangeLog", parms, CommandType.StoredProcedure);

            if (rid > 0)
                return (int)parms[10].Value;
            else
                return -1;
        }
        #endregion
        private static int AddChangeLogtran(SqlTransaction tran, int type, DateTime time, string sip, ChangeCategory category, string from, string fromusertype, string to, string tousertype, int qishu, string remark, int recordid)
        {
            #region remark
            /*
			@Type smallint,
			@Time datetime,
			@SIP varchar(50),
			@Category varchar(10),
			@From varchar(20),
			@FromUserType varchar(10),
			@To varchar(20),
			@ToUserType varchar(10),
			@Qishu int,
			@Remark varchar(4000),
			@RecordID int output --返回插入成功的标识列ID
			*/
            #endregion

            SqlParameter[] parms = new SqlParameter[11];

            parms[0] = new SqlParameter("@Type", type);
            parms[1] = new SqlParameter("@Time", DateTime.Now.ToUniversalTime());
            parms[2] = new SqlParameter("@SIP", sip);
            parms[3] = new SqlParameter("@Category", (int)category);
            parms[4] = new SqlParameter("@From", from);
            parms[5] = new SqlParameter("@FromUserType", fromusertype);
            parms[6] = new SqlParameter("@To", to);
            parms[7] = new SqlParameter("@ToUserType", tousertype);
            parms[8] = new SqlParameter("@Qishu", qishu);
            parms[9] = new SqlParameter("@Remark", remark);
            parms[10] = new SqlParameter("@RecordID", recordid);
            // 输出参数，返回插入成功的标识列ID
            parms[10].Direction = ParameterDirection.Output;

            // 执行添加修改日志记录过程存储过程
            int rid = DBHelper.ExecuteNonQuery(tran, "AddChangeLog", parms, CommandType.StoredProcedure);

            if (rid > 0)
                return (int)parms[10].Value;
            else
                return -1;
        }
    }
 /// <summary>
 /// 操作部位
 /// </summary>
    public enum ChangeCategory
    {
        /// <summary>
        /// 会员信息编辑
        /// </summary>
        company0 = 0,
        /// <summary>
        /// 会员密码重置
        /// </summary>
        company1 = 1,
        /// <summary>
        /// 批量修改
        /// </summary>
        company2 = 2,
        /// <summary>
        /// 报单浏览
        /// </summary>
        company3 = 3,
        /// <summary>
        /// 机构信息编辑
        /// </summary>
        company4 = 4,
        /// <summary>
        /// 机构密码重置
        /// </summary>
        company5 = 5,
        /// <summary>
        /// 供应商管理
        /// </summary>
        company6 = 6,
        /// <summary>
        /// 产品修改
        /// </summary>
        company7 = 7,
        /// <summary>
        /// 入库审核
        /// </summary>
        company8 = 8,
        /// <summary>
        /// 第三方物流管理
        /// </summary>
        company9 = 9,
        /// <summary>
        /// 订单支付
        /// </summary>
        company10 = 10,
        /// <summary>
        /// 预收帐款
        /// </summary>
        company11 = 11,
        /// <summary>
        /// 退货款管理
        /// </summary>
        company12 = 12,
        /// <summary>
        /// 帐户管理
        /// </summary>
        company13 = 13,
        /// <summary>
        /// 提现审核
        /// </summary>
        company14 = 14,
        /// <summary>
        /// 结算参数
        /// </summary>
        company15 = 15,
        /// <summary>
        /// 调控参数
        /// </summary>
        company16 = 16,
        /// <summary>
        /// 奖金退回
        /// </summary>
        company17 = 17,
        /// <summary>
        /// 资料管理
        /// </summary>
        company18 = 18,
        /// <summary>
        /// 公告查询
        /// </summary>
        company19 = 19,
        /// <summary>
        /// 收件箱
        /// </summary>
        company20 = 20,
        /// <summary>
        /// 发件箱
        /// </summary>
        company21 = 21,
        /// <summary>
        /// 废件箱
        /// </summary>
        company22 = 22,
        /// <summary>
        /// 预设短信
        /// </summary>
        company23 = 23,
        /// <summary>
        /// 部门管理
        /// </summary>
        company24 = 24,
        /// <summary>
        /// 角色管理
        /// </summary>
        company25 =25,
        /// <summary>
        /// 管理员管理
        /// </summary>
        company26 = 26,
        /// <summary>
        /// 密码修改
        /// </summary>
        company27 = 27,
        /// <summary>
        /// 参数设置
        /// </summary>
        company28 = 28,
        /// <summary>
        /// 系统开关
        /// </summary>
        company29 = 29,
        /// <summary>
        /// 查询控制
        /// </summary>
        company30 = 30,
        /// <summary>
        /// 支付设置
        /// </summary>
        company31 = 31,
        /// <summary>
        /// 数据加密设置
        /// </summary>
        company32 = 32,
        /// <summary>
        /// 短信网关设置
        /// </summary>
        company33 = 33,
        /// <summary>
        /// 短信内容预设
        /// </summary>
        company34 = 34,
        /// <summary>
        /// 各语种翻译
        /// </summary>
        company35 = 35,
        /// <summary>
        /// 各汇率设置
        /// </summary>
        company36 = 36,
        /// <summary>
        /// 日期时间设置
        /// </summary>
        company37 = 37,
        /// <summary>
        /// 国家和地区设置
        /// </summary>
        company38 = 38,

       

        /// <summary>
        /// 注册浏览
        /// </summary>
        store0 = 39,
        /// <summary>
        /// 复消浏览
        /// </summary>
        store1 = 40,
        /// <summary>
        /// 注册确认
        /// </summary>
        store2 = 41,
        /// <summary>
        /// 复消确认
        /// </summary>
        store3 = 42,
        /// <summary>
        /// 批量注册检测
        /// </summary>
        store4 = 43,
        /// <summary>
        /// 收件箱
        /// </summary>
        store5 = 44,
        /// <summary>
        /// 发件箱
        /// </summary>
        store6 = 45,
        /// <summary>
        /// 废件箱
        /// </summary>
        store7 = 46,
        /// <summary>
        /// 密码修改
        /// </summary>
        store8 = 47,
        /// <summary>
        /// 服务机构资料修改
        /// </summary>
        store9 = 48,
        /// <summary>
        /// 订单编辑
        /// </summary>
        store10 = 49,

        /// <summary>
        /// 注册报单浏览
        /// </summary>
        member0 = 50,
        /// <summary>
        /// 充值浏览
        /// </summary>
        member1 = 51,
        /// <summary>
        /// 密码修改
        /// </summary>
        member2 = 52,
        /// <summary>
        /// 会员资料修改
        /// </summary>
        member3 = 53,
        /// <summary>
        /// 收件箱
        /// </summary>
        member4 = 54,
        /// <summary>
        /// 发件箱
        /// </summary>
        member5 = 55,
        /// <summary>
        /// 废件箱
        /// </summary>
        member6 = 56,

        /// <summary>
        /// 会员
        /// </summary>
        Member = 57,
        /// <summary>
        /// 店铺
        /// </summary>
        Store = 58,
        /// <summary>
        /// 公司
        /// </summary>
        Company = 59,
        /// <summary>
        /// 批量修改或报单浏览
        /// </summary>
        Order = 60,
        /// <summary>
        /// 权限(角色)
        /// </summary>
        Role = 61,
        /// <summary>
        /// 密码
        /// </summary>
        Password = 62,
        /// <summary>
        /// 产品
        /// </summary>
        Product = 63,
             
         /// <summary>
        /// 奖金公布
        /// </summary>
        company39 = 64,

        /// <summary>
        /// 短信删除
        /// </summary> 
        company65 = 65 
    }

    public enum ChangeCategory1
    {
        /// <summary>
        /// 会员
        /// </summary>
        Member = 0,
        /// <summary>
        /// 店铺
        /// </summary>
        Store = 1,
        /// <summary>
        /// 公司
        /// </summary>
        Company = 2,
        /// <summary>
        /// 报单
        /// </summary>
        Order = 3,
        /// <summary>
        /// 权限(角色)
        /// </summary>
        Role = 4,
        /// <summary>
        /// 密码
        /// </summary>
        Password = 5,
        /// <summary>
        /// 产品
        /// </summary>
        Product = 6
    }
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum ChangeType
    {
        /// <summary>
        /// 删除操作
        /// </summary>
        Delete = 0,
        /// <summary>
        /// 修改操作
        /// </summary>
        Modify = 1
    }


    
}

