using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DAL.Other;
using System.Data;
using Model;

/*
 * 创建人：孙延昊
 * **/
namespace DAL
{
    /// <summary>
    /// 黑名单组设置数据访问层
    /// </summary>
    public class BlackGroupDAL
    {

        /// <summary>
        /// 分页获取黑名单组信息
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <param name="condition"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IList<BlackGroupModel> GetBlackGroups(Model.Other.PaginationModel pageInfo, string tableName, string columns, string condition, string key)
        {
            SqlParameter[] ps = new SqlParameter[] 
            {
                new SqlParameter("@tableName",tableName),
                new SqlParameter("@key",key),
                new SqlParameter("@comlums",columns),
                new SqlParameter("@condition",condition),
                new SqlParameter("@start",pageInfo.GetPageDate()),
                new SqlParameter("@end",pageInfo.GetEndDate()),
                new SqlParameter("@DataCount",pageInfo.DataCount)
            };
            ps[6].Direction = ParameterDirection.Output;
            ps[6].Size = 10;
            SqlDataReader reader = DBHelper.ExecuteReader("GetDataPagePagination", ps, CommandType.StoredProcedure);
            IList<BlackGroupModel> blacks = null;
            if (reader.HasRows)
            {
                blacks = new List<BlackGroupModel>();
                while (reader.Read())
                {
                    BlackGroupModel black = new BlackGroupModel(reader.GetInt32(0));
                    black.IntGroupType = reader.GetInt32(1);
                    black.IntGroupValue = reader.GetString(2);
                    blacks.Add(black);
                }
                reader.NextResult();
                pageInfo.DataCount = Convert.ToInt32(ps[6].Value.ToString());
            }
            reader.Close();
            return blacks;
        }


        /// <summary>
        /// 根据条件分页获取黑名单组信息
        /// </summary>
        /// <param name="pageInfo">分页帮助类</param>
        /// <param name="tableName">表名</param>
        /// <param name="columns">列名</param>
        /// <param name="condition">过滤条件</param>
        /// <param name="key">分页条件列</param>
        /// <returns>DataTable 数据</returns>
        public static DataTable GetBlackGroups2(Model.Other.PaginationModel pageInfo, string tableName, string columns, string condition, string key)
        {
            DataTable dt = SqlDataReaderHelp.GetDataTable(pageInfo, tableName, key, columns, condition);
            return dt;
        }


        /// <summary>
        /// 验证黑名单组是否已经存在
        /// </summary>
        /// <param name="GroupValue">黑名单组名</param>
        /// <param name="GroupType">黑名单组类型</param>
        /// <returns>黑名单组对象</returns>
        public static bool GetBlackGroup(string GroupValue, int GroupType)
        {
            string sql = "select 1 from BlackGroup where GroupValue = @GroupValue and GroupType= @GroupType";
            object obj = null;
            try
            {
                obj= DBHelper.ExecuteScalar(sql, new SqlParameter[] { new SqlParameter("@GroupValue", GroupValue), new SqlParameter("@GroupType", GroupType) }, CommandType.Text);
            }
            catch (SqlException)
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }
            else
                return Convert.ToInt32(obj.ToString())==1;
        }

        /// <summary>
        /// 添加黑名单组到数据库
        /// </summary>
        /// <param name="blackGroup">黑名单组</param>
        /// <param name="operateIP">操作人IP</param>
        /// <param name="operateNum">操作人编号</param>
        /// <returns></returns>
        public static int AddBlackGroup(BlackGroupModel blackGroup, string operateIP, string operateNum)
        {
            if (blackGroup.IntGroupType == 3)
            { 
                //保存区域黑名单组
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@GroupValue",blackGroup.IntGroupValue),
                    new SqlParameter("@operateIP",operateIP),
                    new SqlParameter("@operateNum",operateNum)
                };
                object obj = null;
                //保存店辖黑名单组
                try
                {
                    obj = DBHelper.ExecuteScalar("AddAreaBlackGroup", paras, CommandType.StoredProcedure);
                }
                catch (SqlException)
                {
                    return -2;
                }
                return 1;
            }
            else if (blackGroup.IntGroupType == 4)
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@GroupValue",blackGroup.IntGroupValue),
                    new SqlParameter("@operateIP",operateIP),
                    new SqlParameter("@operateNum",operateNum)
                };
                object obj = null;
                //保存店辖黑名单组
                try
                {
                    obj = DBHelper.ExecuteScalar("AddStoreBlackGroup", paras, CommandType.StoredProcedure);
                }
                catch (SqlException)
                {
                    return -2;
                }
                //if (obj != null)
                //{
                //    return Convert.ToInt32(obj.ToString());
                //}
                //else
                //{
                //    return -3;
                //}
                return 1;
            }
            else if (blackGroup.IntGroupType == 5||blackGroup.IntGroupType == 6)
            { 
                //保存安置网络黑名单组
                //保存推荐网络黑名单组
                object obj = null;
                int groupid = 0;
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@GroupValue",blackGroup.IntGroupValue),
                    new SqlParameter("@GroupType",blackGroup.IntGroupType)
                };                                     
                SqlConnection conn = new SqlConnection(DBHelper.connString);
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    string sql = "insert into blackgroup values(@GroupValue,@GroupType)";
                    DBHelper.ExecuteNonQuery( sql, paras, CommandType.Text);
                    paras = new SqlParameter[] { 
                        new SqlParameter("@GroupValue",blackGroup.IntGroupValue),
                        new SqlParameter("@GroupType",blackGroup.IntGroupType)
                    };                     
                    sql = "select id from BlackGroup where GroupValue=@GroupValue and GroupType=@GroupType";
                    obj = DBHelper.ExecuteScalar(sql, paras, CommandType.Text);
                    if (obj == null)
                    {
                        tran.Rollback();
                        return -2;
                    }
                    groupid = int.Parse(obj.ToString());
                    AddBlackList(tran, blackGroup.IntGroupValue, blackGroup.IntGroupType, operateIP, operateNum, groupid);

                    ///////限制该会员登录
                    BlacklistModel blackListModel = new BlacklistModel();
                    blackListModel.UserType = 0;
                    blackListModel.UserID = blackGroup.IntGroupValue;
                    blackListModel.OperateBH = operateNum;
                    blackListModel.OperateIP = operateIP;
                    blackListModel.GroupID = groupid;
                    blackListModel.BlackDate = DateTime.Now.ToUniversalTime();
                    AddBlackList(tran, blackListModel, "AddBlackList");
                    tran.Commit();
                }
                catch (SqlException)
                {
                    tran.Rollback();
                    DelBlackGroup(groupid, blackGroup.IntGroupType);
                    return -2;
                }
                finally
                {
                    conn.Close();
                }
                return 1;
            }
            else
            {
                //数据异常
                return -3;
            }
        }

        public static void AddBlackList(SqlTransaction tran,string number,int type, string operateIP, string operateNum,int groupid)
        {
            string sql = "";
            if (type == 5)
                sql = "select number from memberinfo where placement=@number";
            else
                sql = "select number from memberinfo where direct=@number";
            SqlParameter para = new SqlParameter("@number", number);
            SqlDataReader dr = DBHelper.ExecuteReader(sql, para, CommandType.Text);

            while (dr.Read())
            {
                number = dr.GetString(0);
                AddBlackList(tran, number,type, operateIP, operateNum, groupid);
                BlacklistModel blackListModel = new BlacklistModel();
                blackListModel.UserType = 0;
                blackListModel.UserID = number;
                blackListModel.OperateBH = operateNum;
                blackListModel.OperateIP = operateIP;
                blackListModel.GroupID = groupid;
                blackListModel.BlackDate = DateTime.Now.ToUniversalTime();
                AddBlackList(tran, blackListModel, "AddBlackList");
            }
            dr.Close();
        }

        public static void AddBlackList(SqlTransaction tran, BlacklistModel blackListModel,string sp)
        {
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@userid",blackListModel.UserID),
                new SqlParameter("@state",blackListModel.State),
                new SqlParameter("@userType",blackListModel.UserType),
                new SqlParameter("@userIP",blackListModel.UserIP),
                new SqlParameter("@groupId",blackListModel.GroupID),
                new SqlParameter("@blackDate",blackListModel.BlackDate),
                new SqlParameter("@operateIP",blackListModel.OperateIP),
                new SqlParameter("@operateNum",blackListModel.OperateBH)
            };
            DBHelper.ExecuteNonQuery(tran,sp, paras, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 删除指定黑名单组信息
        /// </summary>
        /// <param name="id">黑名单组编号</param>
        /// <param name="GroupType">黑名单组类型</param>
        /// <returns></returns>
        public static int DelBlackGroup(int id, int GroupType)
        {
            SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@id",id),
                    new SqlParameter("@GroupType",GroupType)
                };
            object obj = null;
            try
            {
                obj = DBHelper.ExecuteScalar("DelBlackGroup", paras, CommandType.StoredProcedure);
            }
            catch (SqlException)
            {
                return -2;
            }
            if (obj == null)
            {
                return -3;
            }
            else 
            {
                return Convert.ToInt32(obj.ToString());
            }
        }
            
//@UserType int,
//@GroupType int,
//@UserID varchar(20),
//@OperateIP varchar(30),
//@OperateNum varchar(30),
//@BlackDate DateTime,
//@GroupValue varchar(50)

        /// <summary>
        /// 删除网络关系黑名单组信息
        /// </summary>
        /// <param name="id">黑名单组编号</param>
        /// <returns></returns>
        public static int DelBlackGroup(int id)
        {
            SqlParameter para = new SqlParameter("@id",id);
            object obj = null;
            try
            {
                obj = DBHelper.ExecuteScalar("DelBlackGroup2", para, CommandType.StoredProcedure);
            }
            catch (SqlException)
            {
                return -2;
            }
            if (obj == null)
            {
                return -3;
            }
            else
            {
                return Convert.ToInt32(obj.ToString());
            }
        }
    }
}

