using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Other;
using Model;
using DAL;
using System.Data;

using System.Web.UI.WebControls;

namespace BLL.other.Company
{
    /// <summary>
    /// 黑名单设置业务逻辑类
    /// </summary>
    public class BlackListBLL
    {

        /// <summary>
        /// 分页获取黑名单设置
        /// </summary>
        /// <param name="pageInfo">分页帮助类</param>
        /// <param name="condition">条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="columns">列名</param>
        /// <param name="key">分页键</param>
        /// <returns></returns>
        public static IList<BlacklistModel> GetBlackLists(PaginationModel pageInfo,string condition,string tableName,string columns ,string key)
        {
            return BlackListDAL.GetBlackList(pageInfo, condition, tableName, columns, key);
        }
        /// <summary>
        /// 删除黑名单设置
        /// </summary>
        /// <param name="blackID">黑名单标识</param>
        /// <param name="userType">黑名单类型3 ip,0 会员</param>
        public static int DelBlackList(int blackID, int userType)
        {
            return BlackListDAL.DelBlackList(blackID, userType);
        }

        /// <summary>
        /// 验证黑名单已经是否存在
        /// </summary>
        /// <param name="userid">黑名单编号</param>
        /// <param name="userType">黑名单类型</param>
        /// <returns></returns>
        public static bool HasBlackList(string userid,int userType,int groupId)
        {
            return BlackListDAL.GetBlackList(userid, userType,groupId) != null;
        }

        /// <summary>
        /// 添加系统黑名单(-)
        /// </summary>
        /// <param name="userid">黑名单编号</param>
        /// <param name="userType">黑名单类型</param>
        /// <param name="groupID">黑名单组别</param>
        /// <returns></returns>
        public static int AddBlackList(string userid, int userType, int groupID)
        {
            return BlackListDAL.AddBlackList(userid, userType, groupID);
        }

        /// <summary>
        /// 添加系统黑名单
        /// </summary>
        /// <param name="userid">黑名单编号</param>
        /// <param name="userType">黑名单类型</param>
        /// <param name="groupID">黑名单组别</param>
        /// <returns></returns>
        public static int AddMobileBlackList(BlacklistModel blackListModel)
        {
            return BlackListDAL.AddMobileBlackList(blackListModel);
        }
        /// <summary>
        /// 添加系统黑名单
        /// </summary>
        /// <param name="userid">黑名单编号</param>
        /// <param name="userType">黑名单类型</param>
        /// <param name="groupID">黑名单组别</param>
        /// <returns></returns>
        public static int AddBlackList(BlacklistModel blackListModel)
        {
            return BlackListDAL.AddBlackList(blackListModel);
        }

        /// <summary>
        /// 检查用户是否在黑名单列表中的登陆状态
        /// </summary>
        /// <param name="userid">用户编号:包括管理员、店铺、会员</param>
        /// <param name="usertype">类别：0会员，1店铺，2管理员</param>
        /// <returns>返回true，禁止登陆；false，允许登陆</returns>
        public static bool CheckBlacklistLogin(string userid, int usertype, string UserAddress)
        {
            return BlackListDAL.CheckBlacklistLogin(userid, usertype, UserAddress);
        }

        /// <summary>
        /// 查询黑名单设置内容
        /// </summary>
        /// <param name="pageInfo">分页帮助类</param>
        /// <param name="p">条件</param>
        /// <param name="p_3">表名</param>
        /// <param name="p_4">列名</param>
        /// <param name="p_5">分页字段</param>
        /// <returns>DataTable</returns>
        public static DataTable GetBlackListsDataTable(PaginationModel pageInfo, string condition, string tableName, string columns, string key)
        {
            return BlackListDAL.GetBlackListDataTable(pageInfo, condition, tableName, columns, key);
        }

        /// <summary>
        /// 验证是否存在店铺编号
        /// </summary>
        /// <param name="p">店铺编号</param>
        /// <returns></returns>
        public static bool CheckStoreID(string p)
        {
            return !StoreInfoDAL.CheckStoreId(p);
        }

        /// <summary>
        /// 模糊查询ip地址黑名单数量
        /// </summary>
        /// <param name="ipAddress"></param>
        public static int GetLikeIPCount(string ipAddress)
        {
            return BlackListDAL.GetLikeIPCount(ipAddress);
        }

        /// <summary>
        /// 查询黑名单数量
        /// </summary>
        /// <param name="userid">黑名单名</param>
        /// <param name="userType">黑名单类型</param>
        public static int GetLikeIPCount(int userType, string userid)
        {
            return BlackListDAL.GetLikeIPCount(userType, userid);
        }
        /// <summary>
        /// 查询限制区域登陆
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static Boolean GetLikeAddress(string number)
        {
            return BlackListDAL.GetLikeAddress(number);
        }
          /// <summary>
        /// 限制系统登入
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<ListItem> GetLoginSystem()
        { 
            return BlackListDAL.GetLoginSystem();
        }
           ///// <summary>
        ///// 限制系统 登入
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <returns></returns>
        public static Boolean SetLoginSystem(List<ListItem> list,string Operatenum,string Operateip)
        {
            return BlackListDAL.SetLoginSystem( list,Operatenum,Operateip);
        }

         /// <summary>
        /// 系统开关
        /// </summary>
        /// <param name="sysname">系统类型</param>
        /// <returns>0:关（false） 1:开（true）</returns>
        public static Boolean GetSystem(string sysname)
        {
            return BlackListDAL.GetSystem(sysname);
        }

        /// <summary>
        /// 系统开关结算时调用
        /// </summary>
        public static int GetSystemClose(string operateIP, string operateNum)
        {
            return BlackListDAL.GetSystemClose(operateIP, operateNum);
        }
        /// <summary>
        /// 系统开关结算时调用
        /// </summary>
        public static int GetSystemOpen(string operateIP, string operateNum)
        {
            return BlackListDAL.GetSystemOpen(operateIP, operateNum);
        }
    } 
}
