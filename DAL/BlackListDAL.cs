using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Model;
using DAL.Other;
using System.Web;
using System.Collections;
using System.Web.UI.WebControls;

namespace DAL
{
    /// <summary>
    /// 黑名单数据访问层
    /// </summary>
    public class BlackListDAL
    {

        /// <summary>
        /// 删除黑名单设置
        /// </summary>
        /// <param name="blackID">黑名单标识</param>
        /// <param name="userType">黑名单类型</param>
        /// <returns></returns>
        public static int DelBlackList(int blackID, int userType)
        {
            String sql = "delete from blackList where id = @id and userType = @userType";
            SqlParameter[] para = new SqlParameter[]{
                //删除指定黑名单标识
                GetSqlParameter("@id", SqlDbType.Int, blackID, 4),
                //黑名单类型 3是IP黑名单 0 是普通会员黑名单
                GetSqlParameter("@userType",SqlDbType.Int,userType,4)
            };
            try
            {
                //返回影响行数
                return DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
            }
            catch (SqlException)
            {
                //返回-1表明产生sql异常
                return -1;
            }
        }

        /// <summary>
        /// 获取指定编号的和类型的黑名单
        /// </summary>
        /// <param name="userid">黑名单编号</param>
        /// <param name="userType">黑名单类型</param>
        /// <param name="groupId">黑名单组别</param>
        /// <returns></returns>
        public static BlacklistModel GetBlackList(string userid, int userType, int groupId)
        {
            BlacklistModel obj = null;
            string sql = "select * from blackList where userid=@userid and userType=@userType and groupId = @groupId";
            SqlParameter[] paras = new SqlParameter[] { 
                GetSqlParameter("@userid",SqlDbType.VarChar,userid),
                GetSqlParameter("@userType",SqlDbType.Int,userType),
                GetSqlParameter("@groupId",SqlDbType.Int,groupId)
            };
            SqlDataReader dr = DBHelper.ExecuteReader(sql, paras, CommandType.Text);
            if (dr.Read())
            {
                obj = new BlacklistModel(dr.GetInt32(0));
                obj.UserID = dr.GetString(1);
                obj.State = dr.GetByte(2);
                obj.UserType = dr.GetByte(3);
                obj.UserIP = dr.GetString(4);
                obj.GroupID = dr.GetInt32(5);
                obj.BlackDate = dr.GetDateTime(6);
                obj.OperateIP = dr.GetString(7);
                obj.OperateBH = dr.GetString(8);
            }
            dr.Close();
            return obj;
        }

        /// <summary>
        /// 添加新的黑名单(-)
        /// </summary>
        /// <param name="userid">黑名单编号</param>
        /// <param name="userType">黑名单类型</param>
        /// <param name="groupID">黑名单组别</param>
        /// <returns>执行结果 >0 执行成功, -1执行遇到异常</returns>
        public static int AddBlackList(string userid, int userType, int groupID)
        {
            return -100;
        }

        /// <summary>
        /// 检查用户是否在黑名单列表中的登陆状态
        /// </summary>
        /// <param name="userid">用户编号:包括管理员、店铺、会员</param>
        /// <param name="usertype">类别：0会员，1店铺，2管理员</param>
        /// <returns>返回true，禁止登陆；false，允许登陆</returns>
        public static bool CheckBlacklistLogin(string userid, int usertype, string UserAddress)
        {
            ArrayList list = new ArrayList();
            string[] SecPostion = UserAddress.Split('.');
            string strIP = "";
            strIP = SecPostion[0];
            SqlDataReader dr = DBHelper.ExecuteReader("select userid from Blacklist where usertype=3 and userid like '" + strIP + ".%'");
            while (dr.Read())
            {
                list.Add(new ListItem(dr[0].ToString()));
            }
            dr.Close();
            foreach (ListItem al in list)
            {
                string[] userIP = al.Value.Split('.');
                string PiPei = "";
                string addressIP = "";
                string PiPei1 = "";
                string addressIP1 = "";
                for (int i = 0; i < 4; i++)
                {
                    if (userIP[i].ToString() != "*")
                    {
                        PiPei += userIP[i].ToString() + ".";
                        addressIP += SecPostion[i].ToString() + ".";
                    }
                    else
                    {

                        for (int j = 0; j < i; j++)
                        {
                            PiPei1 += userIP[j].ToString() + ".";
                            addressIP1 += SecPostion[j].ToString() + ".";
                        }
                        if (PiPei1 == addressIP1)
                        {
                            return true;
                        }
                    }

                }
                if (PiPei == addressIP)
                {
                    return true;
                }
            }

            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = new SqlParameter("@UserID", userid);
            parms[1] = new SqlParameter("@UserType", usertype);

            Object objResult = DBHelper.ExecuteScalar("CheckBlacklistLogin", parms, CommandType.StoredProcedure);
            if (objResult != null)
            {
                try
                {
                    if ((int)objResult > 0)
                        return true;
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write(ex.ToString());
                }
            }
            return false;
        }

        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="blackListModel">黑名单信息</param>
        /// <returns>执行结果 >0 执行成功, -1执行遇到异常</returns>
        public static int AddBlackList(BlacklistModel blackListModel)
        {
            string sp = "AddBlackList";
            SqlParameter[] paras = new SqlParameter[] { 
                GetSqlParameter("@userid",SqlDbType.VarChar,blackListModel.UserID,20),
                GetSqlParameter("@state",SqlDbType.TinyInt,blackListModel.State,4),
                GetSqlParameter("@userType",SqlDbType.TinyInt,blackListModel.UserType,4),
                GetSqlParameter("@userIP",SqlDbType.NVarChar,blackListModel.UserIP,100),
                GetSqlParameter("@groupId",SqlDbType.Int,blackListModel.GroupID,4),
                GetSqlParameter("@blackDate",SqlDbType.DateTime,blackListModel.BlackDate),
                GetSqlParameter("@operateIP",SqlDbType.VarChar,blackListModel.OperateIP,30),
                GetSqlParameter("@operateNum",SqlDbType.VarChar,blackListModel.OperateBH,30)
            };
            try
            {
                return DBHelper.ExecuteNonQuery(sp, paras, CommandType.StoredProcedure);
            }
            catch (SqlException)
            {
                return -1;
            }
        }
        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="blackListModel">黑名单信息</param>
        /// <returns>执行结果 >0 执行成功, -1执行遇到异常</returns>
        public static int AddMobileBlackList(BlacklistModel blackListModel)
        {
            string sp = "AddMobileBlackList";
            SqlParameter[] paras = new SqlParameter[] { 
                GetSqlParameter("@userid",SqlDbType.VarChar,blackListModel.UserID,20),
                GetSqlParameter("@state",SqlDbType.TinyInt,blackListModel.State,4),
                GetSqlParameter("@userType",SqlDbType.TinyInt,blackListModel.UserType,4),
                GetSqlParameter("@userIP",SqlDbType.NVarChar,blackListModel.UserIP,100),
                GetSqlParameter("@groupId",SqlDbType.Int,blackListModel.GroupID,4),
                GetSqlParameter("@blackDate",SqlDbType.DateTime,blackListModel.BlackDate),
                GetSqlParameter("@operateIP",SqlDbType.VarChar,blackListModel.OperateIP,30),
                GetSqlParameter("@operateNum",SqlDbType.VarChar,blackListModel.OperateBH,30),
                GetSqlParameter("@Mobile",SqlDbType.NVarChar,blackListModel.Mobile,100),
                GetSqlParameter("@Remark",SqlDbType.Text,blackListModel.Remark,1000)
            };
            try
            {
                return DBHelper.ExecuteNonQuery(sp, paras, CommandType.StoredProcedure);
            }
            catch (SqlException)
            {
                return -1;
            }
        }
        private static SqlParameter GetSqlParameter(string paramName, SqlDbType sqlDbType, object obj)
        {
            SqlParameter para = new SqlParameter(paramName, sqlDbType);
            para.Value = obj;
            return para;
        }
        private static SqlParameter GetSqlParameter(string paramName, SqlDbType sqlDbType, object obj, int size)
        {
            SqlParameter para = new SqlParameter(paramName, sqlDbType, size);
            para.Value = obj;
            return para;
        }


        /// <summary>
        /// 分页获取黑名单设置
        /// </summary>
        /// <param name="pageInfo">分页帮助类</param>
        /// <param name="condition">分页条件</param>
        /// <param name="tableName">表名</param>
        /// <param name="columns">列名</param>
        /// <param name="key">分页键</param>
        /// <returns></returns>
        public static IList<BlacklistModel> GetBlackList(Model.Other.PaginationModel pageInfo, string condition, string tableName, string columns, string key)
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
            SqlDataReader dr = DBHelper.ExecuteReader("GetDataPagePagination", ps, CommandType.StoredProcedure);
            IList<BlacklistModel> blacks = null;
            //DataTable blacks = SqlDataReaderHelp.GetDataTable(pageInfo, tableName, key, columns, condition);
            if (dr.HasRows)
            {
                blacks = new List<BlacklistModel>();
                while (dr.Read())
                {
                    BlacklistModel black = new BlacklistModel(dr.GetInt32(0));
                    black.UserID = dr.GetString(1);
                    black.State = dr.GetByte(2);
                    black.UserType = dr.GetByte(3);
                    black.UserIP = dr.GetString(4);
                    black.GroupID = dr.GetInt32(5);
                    black.BlackDate = dr.GetDateTime(6);
                    black.OperateIP = dr.GetString(7);
                    black.OperateBH = dr.GetString(8);
                    blacks.Add(black);
                }
                dr.NextResult();
                pageInfo.DataCount = Convert.ToInt32(ps[6].Value.ToString());
            }
            dr.Close();
            return blacks;
        }


        public static DataTable GetBlackListDataTable(Model.Other.PaginationModel pageInfo, string condition, string tableName, string columns, string key)
        {
            return SqlDataReaderHelp.GetDataTable(pageInfo, tableName, key, columns, condition);
        }

        /// <summary>
        /// 根据ip段模糊获取IP地址黑名单数量
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static int GetLikeIPCount(string ipAddress)
        {
            string sql = "select count(1) from blacklist where userType=3 and userid = @ipAddress";
            //SqlParameter para = new SqlParameter("ipAddress", ipAddress + "%");
            SqlParameter para = new SqlParameter("ipAddress", ipAddress);
            object obj = null;
            obj = DBHelper.ExecuteScalar(sql, para, CommandType.Text);
            return Convert.ToInt32(obj.ToString());
        }

        /// <summary>
        /// 查询黑名单数量
        /// </summary>
        /// <param name="userid">黑名单名</param>
        /// <param name="userType">黑名单类型</param>
        public static int GetLikeIPCount(int userType, string userid)
        {
            string sql = "select count(1) from blacklist where userType=@userType and userid = @userid";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("userid", userid),
                new SqlParameter("@userType",userType)
            };

            object obj = null;
            obj = DBHelper.ExecuteScalar(sql, para, CommandType.Text);
            return Convert.ToInt32(obj.ToString());
        }


        /// <summary>
        /// 查询限制区域登陆
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static Boolean GetLikeAddress(string number)
        {
            string sql = "select count(@@rowcount) from memberinfo left join blackgroup on memberinfo.CPCCode=blackgroup.groupvalue where grouptype=3 and number=@number";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@number", number)
            };
            object obj = null;
            obj = DBHelper.ExecuteScalar(sql, para, CommandType.Text);
            return Convert.ToInt32(obj.ToString()) > 0 ? true : false;
        }

        /// <summary>
        /// 限制系统 查看
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<ListItem> GetLoginSystem()
        {
            string sql = "select systemname,systemvalue from setsystem ";
            SqlDataReader read = DBHelper.ExecuteReader(sql, CommandType.Text);
            List<ListItem> list = new List<ListItem>();
            while (read.Read())
            {
                ListItem item = new ListItem();
                item.Value = read["systemvalue"].ToString();
                item.Text = read["systemname"].ToString();
                item.Selected = Convert.ToInt32(read["systemvalue"].ToString()) == 0 ? true : false;
                list.Add(item);
            }
            read.Close();
            return list;
        }

        ///// <summary>
        ///// 限制系统 登入
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <returns></returns>
        public static Boolean SetLoginSystem(List<ListItem> list, string OperateNum, string OperateIP)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (ListItem item in list)
                        {

                            string sql = "update setsystem set systemvalue=@systemvalue ,[time]=getdate(),OperateIP=@OperateIP,OperateNum=@OperateNum where  systemname=@systemname ";
                            SqlParameter[] para = new SqlParameter[]{
                        new SqlParameter("@systemvalue", item.Selected==true?0:1),
                        new SqlParameter("@OperateIP", OperateIP ),
                        new SqlParameter("@OperateNum", OperateNum),
                        new SqlParameter("@systemname", item.Value)
                        };
                            DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
                        }
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        conn.Close();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 系统开关
        /// </summary>
        /// <param name="sysname">系统类型</param>
        /// <returns>0:关（false） 1:开（true）</returns>
        public static Boolean GetSystem(string sysname)
        {
            string sql = "select systemvalue from setsystem where systemname=@systemname";
            SqlParameter par = new SqlParameter("@systemname", sysname);
            int bol = Convert.ToInt32(DBHelper.ExecuteScalar(sql,par,CommandType.Text));
            return bol == 0 ? false : true;
        }
        /// <summary>
        /// 系统开关结算时调用
        /// </summary>
        /// <param name="sysname">系统类型</param>
        /// <returns>0:关 1:开</returns>
        public static int GetSystemOpen(string operateIP, string operateNum)
        {
            string sql = "update SetSystem set systemvalue=1,time=getdate(),operateIP=@operateIP,operateNum=@operateNum";
            SqlParameter[] par = new SqlParameter[]{ new SqlParameter("@operateIP", operateIP),
                                                      new SqlParameter("@operateNum",operateNum)
                                                    };
            int bol = DBHelper.ExecuteNonQuery(sql, par, CommandType.Text);
            return bol;
        }
        /// <summary>
        /// 系统开关结算时调用
        /// </summary>
        /// <param name="sysname">系统类型</param>
        /// <returns>0:关 1:开</returns>
        public static int GetSystemClose(string operateIP, string operateNum)
        {
            string sql = "update SetSystem set systemvalue=0,time=getdate(),operateIP=@operateIP,operateNum=@operateNum";
            SqlParameter[] par = new SqlParameter[]{ new SqlParameter("@operateIP", operateIP),
                                                      new SqlParameter("@operateNum",operateNum)
                                                    };
            int bol = DBHelper.ExecuteNonQuery(sql, par, CommandType.Text);
            return bol;
        }
    }
}
