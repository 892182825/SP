using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespce
using System.Data;
using System.Web;
using System.Web.SessionState;
using DAL;
using Model.Other;
using BLL.CommonClass;
using System.Data.SqlClient;
using System.Collections;

/*
 * 修改者：汪华
 * 修改时间：2009-09-01
 */

namespace BLL.CommonClass
{
    public class ChangeLogs : ChangeLogsBase
    {
        protected RemarkInfo _remarkinfo;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tablename">要记录表记录的名称</param>
        /// <param name="identity">要记录表记录的标识列(目前只设置单列)</param>
        public ChangeLogs(string tablename, string identity)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this._remarkinfo = new RemarkInfo(tablename, identity);
        }

        /// <summary>
        /// 添加记录信息行(重载)——ds2012——tianfeng
        /// </summary>
        /// <param name="id">标识列的值(条件列，可扩展)</param>
        public void AddRecord(int id)
        {
            AddRecord(id.ToString());
        }

        /// <summary>
        /// 添加记录信息行(重载)——ds2012——tianfeng
        /// </summary>
        /// <param name="id">标识列的值(条件列，可扩展)</param>
        public void AddRecord(string id)
        {
            this._remarkinfo.AddRecord(id);
        }
        /// <summary>
        /// 添加记录信息行(重载) 有事务——ds2012——tianfeng
        /// </summary>
        /// <param name="id">标识列的值(条件列，可扩展)</param>
        public void AddRecordtran(SqlTransaction tran, string id)
        {
            this._remarkinfo.AddRecordtran(tran, id);
        }
        /// <summary>
        /// 添加记录信息行(重载)
        /// </summary>
        /// <param name="id">标识列的值(条件列，可扩展)</param>
        /// <param name="columns">需要的列数组</param>
        public void AddRecord(string id, string[] columns)
        {
            this._remarkinfo.AddRecord(id, columns);
        }

        /// <summary>
        /// 获取修改记录日志的信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetChangeLogs()
        {
            return DBHelper.ExecuteDataTable("GetChangeLogs", null, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 删除信息，写入记录日志
        /// </summary>
        /// <param name="category">类型</param>
        /// <param name="to">操作对象</param>
        /// <param name="tousertype">操作对象用户类型：会员、店铺、公司、管理员</param>
        /// <param name="remark">操作发生的数据改变记录信息</param>
        public static void DeletedIntoLogs(ChangeCategory category, string to, ENUM_USERTYPE tousertype, string remark)
        {
            // 自动化参数
            string from = GetCurrentUserID();
            string fromusertype = GetCurrentUserType().ToString();
            int qishu = CommonDataBLL.getMaxqishu();

            AddChangeLog((int)ChangeType.Delete, category, from, fromusertype, to, tousertype.ToString(), qishu, remark, 0);
        }

        /// <summary>
        /// 删除信息，写入记录日志
        /// </summary>
        /// <param name="category">类型</param>
        /// <param name="from">操作者</param>
        /// <param name="fromusertype">操作者用户类型：会员、店铺、公司、管理员</param>
        /// <param name="to">操作对象</param>
        /// <param name="tousertype">操作对象用户类型：会员、店铺、公司、管理员</param>
        /// <param name="qishu">期数</param>
        /// <param name="remark">操作发生的数据改变记录信息</param>
        public static void DeletedIntoLogs(ChangeCategory category, string from, string fromusertype, string to, string tousertype, int qishu, string remark)
        {
            AddChangeLog((int)ChangeType.Delete, category, from, fromusertype, to, tousertype, qishu, remark, 0);
        }

        /// <summary>
        /// 删除信息，写入记录日志——ds2012——tianfeng
        /// </summary>
        /// <param name="category">类型</param>
        /// <param name="to">操作对象</param>
        /// <param name="tousertype">操作对象用户类型：会员、店铺、公司、管理员</param>
        public void DeletedIntoLogs(ChangeCategory category, string to, ENUM_USERTYPE tousertype)
        {
            // 自动化参数
            string from = GetCurrentUserID();
            string fromusertype = GetCurrentUserType().ToString();
            int qishu = CommonDataBLL.getMaxqishu();

            AddChangeLog((int)ChangeType.Delete, category, from, fromusertype, to, tousertype.ToString(), qishu, this._remarkinfo.Text, 0);
        }

        /// <summary>
        /// 删除信息，写入记录日志
        /// </summary>
        /// <param name="category">类型</param>
        /// <param name="to">操作对象</param>
        /// <param name="tousertype">操作对象用户类型：会员、店铺、公司、管理员</param>
        public void DeletedIntoLogstran(SqlTransaction tran,ChangeCategory category, string to, ENUM_USERTYPE tousertype)
        {
            // 自动化参数
            string from = GetCurrentUserID();
            string fromusertype = GetCurrentUserType().ToString();
            int qishu = CommonDataBLL.getMaxqishu(tran);

            AddChangeLogtran(tran,(int)ChangeType.Delete, category, from, fromusertype, to, tousertype.ToString(), qishu, this._remarkinfo.Text, 0);
        }
        /// <summary>
        /// 删除信息，写入记录日志
        /// </summary>
        /// <param name="category">类型</param>
        /// <param name="from">操作者</param>
        /// <param name="fromusertype">操作者用户类型：会员、店铺、公司、管理员</param>
        /// <param name="to">操作对象</param>
        /// <param name="tousertype">操作对象用户类型：会员、店铺、公司、管理员</param>
        /// <param name="qishu">期数</param>
        public void DeletedIntoLogs(ChangeCategory category, string from, string fromusertype, string to, string tousertype, int qishu)
        {
            AddChangeLog((int)ChangeType.Delete, category, from, fromusertype, to, tousertype, qishu, this._remarkinfo.Text, 0);
        }

        /// <summary>
        /// 修改信息，写入记录日志
        /// </summary>
        /// <param name="category">类型</param>
        /// <param name="to">操作对象</param>
        /// <param name="tousertype">操作对象用户类型：会员、店铺、公司、管理员</param>
        /// <param name="remark">操作发生的数据改变记录信息</param>
        public static void ModifiedIntoLogs(ChangeCategory category, string to, ENUM_USERTYPE tousertype, string remark)
        {
            // 自动化参数
            string from = GetCurrentUserID();
            string fromusertype = GetCurrentUserType().ToString();
            int qishu = CommonDataBLL.getMaxqishu();

            AddChangeLog((int)ChangeType.Modify, category, from, fromusertype, to, tousertype.ToString(), qishu, remark, 0);
        }

        /// <summary>
        /// 修改信息，写入记录日志
        /// </summary>
        /// <param name="category">类型</param>
        /// <param name="from">操作者</param>
        /// <param name="fromusertype">操作者用户类型：会员、店铺、公司、管理员</param>
        /// <param name="to">操作对象</param>
        /// <param name="tousertype">操作对象用户类型：会员、店铺、公司、管理员</param>
        /// <param name="qishu">期数</param>
        /// <param name="remark">操作发生的数据改变记录信息</param>
        public static void ModifiedIntoLogs(ChangeCategory category, string from, string fromusertype, string to, string tousertype, int qishu, string remark)
        {
            AddChangeLog((int)ChangeType.Modify, category, from, fromusertype, to, tousertype, qishu, remark, 0);
        }

        /// <summary>
        /// 修改信息，写入记录日志
        /// </summary>
        /// <param name="category">类型</param>
        /// <param name="to">操作对象</param>
        /// <param name="tousertype">操作对象用户类型：会员、店铺、公司、管理员</param>
        public void ModifiedIntoLogs(ChangeCategory category, string to, ENUM_USERTYPE tousertype)
        {
            // 自动化参数
            string from = GetCurrentUserID();
            string fromusertype = GetCurrentUserType().ToString();
            int qishu = CommonDataBLL.getMaxqishu();

            AddChangeLog((int)ChangeType.Modify, category, from, fromusertype, to, tousertype.ToString(), qishu, this._remarkinfo.Text, 0);
        }
        public void ModifiedIntoLogstran(SqlTransaction tran, ChangeCategory category, string to, ENUM_USERTYPE tousertype)
        {

            // 自动化参数
            string from = GetCurrentUserID();
            string fromusertype = GetCurrentUserType().ToString();
            int qishu = CommonDataBLL.getMaxqishu(tran);

            AddChangeLogtran(tran, (int)ChangeType.Modify, category, from, fromusertype, to, tousertype.ToString(), qishu, this._remarkinfo.Text, 0);
        }
        /// <summary>
        /// 修改信息，写入记录日志
        /// </summary>
        /// <param name="category">类型</param>
        /// <param name="from">操作者</param>
        /// <param name="fromusertype">操作者用户类型：会员、店铺、公司、管理员</param>
        /// <param name="to">操作对象</param>
        /// <param name="tousertype">操作对象用户类型：会员、店铺、公司、管理员</param>
        /// <param name="qishu">期数</param>
        public void ModifiedIntoLogs(ChangeCategory category, string from, string fromusertype, string to, string tousertype, int qishu)
        {
            AddChangeLog((int)ChangeType.Modify, category, from, fromusertype, to, tousertype, qishu, this._remarkinfo.Text, 0);
        }

        /// <summary>
        /// 获得当前操作员编号(公司、店铺、会员)
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserID()
        {
            HttpSessionState session = HttpContext.Current.Session;
            if (session["Company"] != null)
            {
                return session["Company"].ToString().Trim();
            }
            else if (session["Store"] != null)
            {
                return session["Store"].ToString().Trim();
            }
            else if (session["Member"] != null)
            {
                return session["Member"].ToString().Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获得当前用户的类型
        /// </summary>
        /// <returns></returns>
        public static ENUM_USERTYPE GetCurrentUserType()
        {
            HttpSessionState session = HttpContext.Current.Session;
            if (session["Company"] != null)
            {
                return ENUM_USERTYPE.Company;
            }
            else if (session["Store"] != null)
            {
                return ENUM_USERTYPE.Store;
            }
            else if (session["Member"] != null)
            {
                return ENUM_USERTYPE.Member;
            }
            else
            {
                return ENUM_USERTYPE.None;
            }
        }

    }

    /// <summary>
    /// 网站的用户
    /// </summary>
    public enum ENUM_USERTYPE1
    {
        Member, Store, Company, None
    }

    public enum ENUM_USERTYPE
    {
        /// <summary>
        /// 仓库
        /// </summary>
        objecttype0,
        /// <summary>
        /// 产品
        /// </summary>
        objecttype1,
        /// <summary>
        /// 服务机构
        /// </summary>
        objecttype2,
        /// <summary>
        /// 公司
        /// </summary>
        objecttype3,
        /// <summary>
        /// 供应商
        /// </summary>
        objecttype4 ,
        /// <summary>
        /// 会员
        /// </summary>
        objecttype5,
        /// <summary>
        /// 密码
        /// </summary>
        objecttype6,
        /// <summary>
        /// 权限
        /// </summary>
        objecttype7,
        /// <summary>
        /// 物流公司
        /// </summary>
        objecttype8,
        /// <summary>
        /// 系统参数
        /// </summary>
        objecttype9,
        /// <summary>
        /// 信息
        /// </summary>
        objecttype10,

        Member, Store, Company, None
    }
}
