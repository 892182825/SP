using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    /// <summary>
    /// 收件箱
    /// </summary>
    public class MessageReceiveDAL
    {
        /// <summary>
        /// 根据id查询发件人的编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object GetMessageReceiveSender(int id)
        {
            return DBHelper.ExecuteScalar("GetMessageReceiveSender", new SqlParameter("@ID", id), CommandType.StoredProcedure);
        }

        /// <summary>
        /// 插入收件箱
        /// </summary>
        /// <param name="messagesendId"></param>
        /// <param name="role"></param>
        /// <param name="Number"></param>
        /// <param name="title"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static bool InsertMessageReceive(int messagesendId, string role, string Number, string title, string sender, string content)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@messagesendId",messagesendId),
                new SqlParameter("@role",role),
                new SqlParameter("@Number",Number),
                new SqlParameter("@title",title),
                new SqlParameter("@sender",sender),
               new SqlParameter("Senddate",DateTime.Now.ToUniversalTime()),
                new SqlParameter("@content",content)
            };
            if (DBHelper.ExecuteNonQuery("InsertMessageReceive", param, CommandType.StoredProcedure) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 插入转发信息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="number"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static bool insertReceiveSend(string role, string number, string title, string content, string sender)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@role",role),
                new SqlParameter("@bianhao",number),
                 new SqlParameter("@title",title),
                  new SqlParameter("@content",content),
                  new SqlParameter("@sender",sender),
                  new SqlParameter("Senddate",DateTime.Now.ToUniversalTime()),
            };
            if (DBHelper.ExecuteNonQuery("insertReceiveSend", param, CommandType.StoredProcedure) > 0)
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// 根据收件箱的信息id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetReceivemenegerCount(int id)
        {
            return DBHelper.ExecuteScalar("GetReceivemenegerCount", new SqlParameter("@id", id), CommandType.StoredProcedure) + "";
        }

        /// <summary>
        /// 查询收的信息用于转发
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetReceivemeneger(int id)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@id",id)
            };
            return DBHelper.ExecuteDataTable("GetReceivemeneger", param, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 根据id删除收件箱信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DelMessageReceive(int id, string optr, int OperatorType)
        {
            if (DBHelper.ExecuteNonQuery("DelMessageReceive", new SqlParameter[] { new SqlParameter("@id", id), new SqlParameter("@operator", optr), new SqlParameter("@OperatorType", OperatorType) }, CommandType.StoredProcedure) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 返回要导出excel的表格
        /// </summary>
        /// <param name="par"></param>
        /// <param name="conntion"></param>
        /// <returns></returns>
        public static DataTable GetExcelTable(string[] par, string conntion)
        {
            string sql = "select InfoTitle,case LoginRole when 0 then '" + par[0] + "' when 1 then '" + par[1] + "' when 2 then '" + par[2] + "' end as LoginRole,Sender,Senddate from MessageSend  where" + conntion;
            return DAL.DBHelper.ExecuteDataTable(sql);

        }
        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int delGongGao(int id)
        {

            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@id",id)
            };
            return DAL.DBHelper.ExecuteNonQuery("DeleteGG", param, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 返回公告信息
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SqlDataReader GetGongGao(string tablename, int id)
        {
            string sql = "select * from " + tablename + " where id=" + id + "";
            return DAL.DBHelper.ExecuteReader(sql);
        }
        /// <summary>
        /// 返回邮件信息
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SqlDataReader GetEmailDetail(string tablename, int id, string receiver)
        {
            string sql = "select * from " + tablename + " where id=" + id + " and ([Receive]='*' or [Receive]='" + receiver + "')";
            return DAL.DBHelper.ExecuteReader(sql);
        }
        /// <summary>
        /// 返回公告信息
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="id"></param>
        /// <param name="receiver"></param>
        /// <returns></returns>
        public static SqlDataReader GetGongGao(string tablename, int id, string receiver)
        {
            string sql = "select * from " + tablename + " where id=" + id + " and ([Receive]='*' or [Receive]='" + receiver + "' or [Receive]='8888888888')";
            return DAL.DBHelper.ExecuteReader(sql);
        }
        /// <summary>
        /// 更新是否阅读
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="sqlid"></param>
        public static void UpdateIsRead(string tableName, string sqlid)
        {
            string sql;
            if (tableName.ToUpper().Equals("V_DroppedMessage".ToUpper()))
            {
                sql = " exec UpdateReadStatusForDropped " + sqlid;
            }
            else
            {
                sql = "update " + tableName + " set ReadFlag='1' where id='" + sqlid + "' and ReadFlag=0";
            }
            DAL.DBHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新机构信息
        /// </summary>
        public static void UpdateStore()
        {
            string sql = "update storeinfo set gonggaotype=0";
            DAL.DBHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新会员信息
        /// </summary>
        public static void UpdateMember()
        {
            string sql = "update memberinfo set gonggaotype=0";
            DAL.DBHelper.ExecuteNonQuery(sql);
        }
    }
}