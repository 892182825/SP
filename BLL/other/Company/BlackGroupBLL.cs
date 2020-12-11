using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Other;
using Model;
using DAL;
using System.Data;

namespace BLL.other.Company
{
    /// <summary>
    /// 黑名单组设置业务逻辑类
    /// </summary>
    public class BlackGroupBLL
    {
        private BlackListDAL blackListDAL = new BlackListDAL();
        private BlackGroupDAL blackGroupDAL = new BlackGroupDAL();
        /// <summary>
        /// 分页查询黑名单设置信息
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public static IList<BlackGroupModel> GetBlackGroups(PaginationModel pageInfo, string tableName, string columns, string condition, string key)
        {
            return BlackGroupDAL.GetBlackGroups(pageInfo, tableName, columns, condition, key);
        }

        public static DataTable GetBlackGroups2(PaginationModel pageInfo, string tableName, string columns, string condition, string key)
        {
            return BlackGroupDAL.GetBlackGroups2(pageInfo,tableName,columns,condition,key);
        }

        /// <summary>
        /// 根据编号删除黑名单
        /// </summary>
        /// <param name="p">删除指定编号的黑名单</param>
        /// <returns></returns>
        public static int DelBlackGroup(int id)
        {
            return BlackGroupDAL.DelBlackGroup(id);
        }

        /// <summary>
        /// 验证黑名单组是否已经存在
        /// </summary>
        /// <param name="GroupValue">黑名单组名</param>
        /// <param name="GroupType">黑名单组类型</param>
        /// <returns>黑名单组对象</returns>
        public static bool HasBlackGroup(string GroupValue, int GroupType)
        {
            return BlackGroupDAL.GetBlackGroup(GroupValue, GroupType);
        }

        /// <summary>
        /// 根据编号删除黑名单
        /// </summary>
        /// <param name="id">黑名单组编号</param>
        /// <param name="GroupValue">黑名单组类型</param>
        /// <returns>返回是否成功</returns>
        public static int DelBlackGroup(int id, int GroupType)
        {
            return BlackGroupDAL.DelBlackGroup(id, GroupType);
        }
        
        /// <summary>
        /// 存储当前黑名单组
        /// </summary>
        /// <param name="blackGroup">黑名单组类型</param>
        /// <returns>执行结果</returns>
        public static int AddBlackGroup(BlackGroupModel blackGroup,string operateIP,string operateNum)
        {
            return BlackGroupDAL.AddBlackGroup(blackGroup, operateIP, operateNum);
        }
    }
}
