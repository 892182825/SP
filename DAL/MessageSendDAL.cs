using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Data.SqlClient;

/*
 * 创建者：      汪   华
 * 创建时间：    2009-09-14
 */

namespace DAL
{
    /// <summary>
    /// 发件箱（用于发邮件和发布公告）
    /// </summary>
    public class MessageSendDAL
    {
        /// <summary>
        /// 根据id删除发件箱的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DelMessageSendById(int id,string optr,int OperatorType)
        {
            if (DBHelper.ExecuteNonQuery("DelMessageSendById", new SqlParameter[]{new SqlParameter("@id", id),new SqlParameter("@operator",optr),new SqlParameter("@OperatorType",OperatorType)}, CommandType.StoredProcedure) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除已经删除的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DelMessageDropbyID(int id)
        {
            if (DBHelper.ExecuteNonQuery("DelMessageDropById", new SqlParameter("@id", id), CommandType.StoredProcedure) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 发布公告
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool Addsendaffiche(MessageSendModel message)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@LoginRole",message.LoginRole),
                new SqlParameter("@Receive",message.Receive),
                new SqlParameter("@InfoTitle",message.InfoTitle),
                new SqlParameter("@Content",message.Content),
                new SqlParameter("@SenderRole",message.SenderRole),
                new SqlParameter("@Sender",message.Sender),
                new SqlParameter("@Sendedate",DateTime.Now.ToUniversalTime()),
                new SqlParameter("@DropFlag",message.DropFlag),
                new SqlParameter("@ReadFlag",message.ReadFlag),
                new SqlParameter("@CountryCode",message.CountryCode),
                new SqlParameter("@LanguageCode",message.LanguageCode),
                new SqlParameter("@ClassID",message.MessageClassID),
                new SqlParameter("@ReplyFor",message.ReplyFor),
                new SqlParameter("@ConditionLevel",message.ConditionLevel),
                new SqlParameter("@ConditionBonusFrom",message.ConditionBonusFrom),
                new SqlParameter("@ConditionBonusTo",message.ConditionBonusTo),
                new SqlParameter("@ConditionLeader",message.ConditionLeader),
                new SqlParameter("@ConditionRelation",message.ConditionRelation),
                new SqlParameter("@Qishu",message.Qishu),
                new SqlParameter("@MessageType",message.MessageType)

            };
            if (DBHelper.ExecuteNonQuery("sendaffiche", param, CommandType.StoredProcedure) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 保存邮件
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool AddMessageSend(MessageSendModel message)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@LoginRole",message.LoginRole),
                new SqlParameter("@Receive",message.Receive),
                new SqlParameter("@InfoTitle",message.InfoTitle),
                new SqlParameter("@Content",message.Content),
                new SqlParameter("@SenderRole",message.SenderRole),
                new SqlParameter("@Sender",message.Sender),
                new SqlParameter("@DropFlag",message.DropFlag),
                new SqlParameter("@ReadFlag",message.ReadFlag)
            };
            if (DBHelper.ExecuteNonQuery("AddMessageSend", param, CommandType.StoredProcedure) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetRole(string role)
        {
            string s = string.Empty;
            switch (role)
            {
                case "0":
                    s = "管理员";
                    break;
                case "2":
                    s = "店铺";
                    break;
                case "1":
                    s = "会员";
                    break;
                default:
                    s = "会员";
                    break;
            }
            return s;
        }
        /// <summary>
        /// 根据编号查询发件箱信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MessageSendModel GetMessageSendById(int id)
        {
            SqlParameter[] par = new SqlParameter[]
            {
                new SqlParameter("@id",SqlDbType.Int)
            };
            par[0].Value = id;
            SqlDataReader reader = DBHelper.ExecuteReader("getMessageSendById", par, CommandType.StoredProcedure);
            MessageSendModel message = new MessageSendModel();
            if (reader.Read())
            {
                //ConditionLevel, ConditionBonusFrom, ConditionBonusTo, ConditionLeader, ConditionRelation, Qishu
                message.Id = Convert.ToInt32(reader["ID"]);
                message.InfoTitle = reader["InfoTitle"].ToString();
                message.LoginRole = reader["LoginRole"].ToString();
                message.ReadFlag = Convert.ToInt32(reader["ReadFlag"].ToString());
                message.Receive = reader["Receive"].ToString();
                message.Senddate = Convert.ToDateTime(reader["Senddate"].ToString());
                message.Content = reader["Content"].ToString();
                message.SenderRole = reader["SenderRole"].ToString();
                message.Sender = reader["Sender"].ToString();
                message.DropFlag = Convert.ToInt32(reader["DropFlag"].ToString());
                message.ConditionLevel = Convert.ToInt16(reader["ConditionLevel"]);
                message.ConditionBonusFrom = Convert.ToDouble(reader["ConditionBonusFrom"]);
                message.ConditionBonusTo = Convert.ToDouble(reader["ConditionBonusTo"]);
                message.ConditionLeader = reader["ConditionLeader"].ToString();
                message.ConditionRelation = Convert.ToChar(reader["ConditionRelation"]);
                message.Qishu = Convert.ToInt32(reader["Qishu"]);
            }
            reader.Close();
            return message;
        }

        /// <summary>
        /// 获取发送人角色 DS  2012
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetSendRole(string id)
        {
            string sql = "select SenderRole from dbo.MessageReceive where id=" + id;
            object obj = DAL.DBHelper.ExecuteScalar(sql, CommandType.Text);
            if (obj == null)
            {
                return "";
            }
            return Convert.ToString(obj);
        }
        /// <summary>
        /// 检查编号是否存在
        /// </summary>
        /// <param name="type">0管理员，1会员，2店铺</param>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static bool CheckNumber(int type, string number)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@type",type),
                new SqlParameter("@number",number)
            };
            if (Convert.ToInt32(DBHelper.ExecuteScalar("CheckNumberIsExist", param, CommandType.StoredProcedure)) > 0)
            {
                return true;
            }
            return false;
        }

        public static int check(string sql)
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar(sql));
        }

        public static int MemberSendToManage(MessageSendModel msm)
        {
            int i = 0;
            try
            {
                System.Text.StringBuilder insertSql2 = new System.Text.StringBuilder();
                insertSql2.Append("insert into MessageSend values(@role,@receive,@title,@content,2,@sender,@senddate,0,0,null,null,@msgclassid,@replyfor,@messagetype)");

                SqlParameter[] para1 ={
										   new SqlParameter("@role",SqlDbType.Char,10),
										   new SqlParameter("@receive",SqlDbType.VarChar,4000),
										   new SqlParameter("@title",SqlDbType.VarChar,50),
										   new SqlParameter("@content",SqlDbType.Text),
										   new SqlParameter("@sender",SqlDbType.VarChar,20),
										   new SqlParameter("@senddate",SqlDbType.DateTime,8),
									       new SqlParameter("@msgclassid",SqlDbType.Int),
                                           new SqlParameter("@replyfor",SqlDbType.Int),
                                           new SqlParameter("@messagetype",SqlDbType.Char)
									   };
                para1[0].Value = msm.LoginRole.ToString();
                para1[1].Value = msm.Receive.ToString();
                para1[2].Value = msm.InfoTitle.ToString();
                para1[3].Value = msm.Content.ToString();
                para1[4].Value = msm.Sender.ToString();
                para1[5].Value = msm.Senddate.ToUniversalTime();
                para1[6].Value = msm.MessageClassID;
                para1[7].Value = msm.ReplyFor;
                para1[8].Value = msm.MessageType;

                using (SqlConnection conn = new SqlConnection(DBHelper.connString))
                {
                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();
                    try
                    {
                        DBHelper.ExecuteNonQuery(tran, insertSql2.ToString(), para1, CommandType.Text);

                        string sqlStr2 = "select max(id) from messagesend where sender = '" + msm.Sender.ToString() + "'";
                        int MessagesendId = Convert.ToInt32(DBHelper.ExecuteScalar(tran, sqlStr2, CommandType.Text));

                        System.Text.StringBuilder insertSql = new System.Text.StringBuilder();
                        insertSql.Append("insert into MessageReceive values(@messagesendId,@role,@receive,@title,@content,2,@sender,@senddate,0,0,0,null,null,@msgclassid,@replyfor,@messagetype) ");
                        SqlParameter[] para ={
												 new SqlParameter("@MessagesendId",SqlDbType.Int),
												 new SqlParameter("@role",SqlDbType.Char,10),
												 new SqlParameter("@receive",SqlDbType.VarChar,4000),
												 new SqlParameter("@title",SqlDbType.VarChar,50),
                                                 new SqlParameter("@content",SqlDbType.NVarChar,4000),
												 new SqlParameter("@sender",SqlDbType.VarChar,20),
												 new SqlParameter("@senddate",SqlDbType.DateTime,8),
                                                 new SqlParameter("@msgclassid",SqlDbType.Int),
									             new SqlParameter("@replyfor",SqlDbType.Int),
                                                 new SqlParameter("@messagetype",SqlDbType.Char)
											 };
                        para[0].Value = MessagesendId;
                        para[1].Value = msm.LoginRole.ToString();
                        para[2].Value = msm.Receive.ToString();
                        para[3].Value = msm.InfoTitle.ToString();
                        para[4].Value = msm.Content.ToString();
                        para[5].Value = msm.Sender.ToString();
                        para[6].Value = msm.Senddate.ToUniversalTime();
                        para[7].Value = msm.MessageClassID;
                        para[8].Value = msm.ReplyFor;
                        para[9].Value = msm.MessageType;


                        i = DBHelper.ExecuteNonQuery(tran, insertSql.ToString(), para, CommandType.Text);
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            catch (Exception e)
            {

            }
            return i;
        }

        public static int StoreSendToManage(MessageSendModel msm)
        {
            int i = 0;
            try
            {
                System.Text.StringBuilder insertSql2 = new System.Text.StringBuilder();
                insertSql2.Append("insert into MessageSend values(@role,@receive,@title,@content,1,@sender,@senddate,0,0,null,null,@msgclassid,@replyfor,@messagetype)");

                SqlParameter[] para1 ={
										   new SqlParameter("@role",SqlDbType.Char,10),
										   new SqlParameter("@receive",SqlDbType.VarChar,4000),
										   new SqlParameter("@title",SqlDbType.VarChar,50),
										   new SqlParameter("@content",SqlDbType.Text),
										   new SqlParameter("@sender",SqlDbType.VarChar,20),
										   new SqlParameter("@senddate",SqlDbType.DateTime,8),
                                           new SqlParameter("@msgclassid",SqlDbType.Int),
							               new SqlParameter("@replyfor",SqlDbType.Int),
                                           new SqlParameter("@messagetype",SqlDbType.Char)											
									   };
                para1[0].Value = msm.LoginRole.ToString();
                para1[1].Value = msm.Receive.ToString();
                para1[2].Value = msm.InfoTitle.ToString();
                para1[3].Value = msm.Content.ToString();
                para1[4].Value = msm.Sender.ToString();
                para1[5].Value = msm.Senddate.ToUniversalTime();
                para1[6].Value = msm.MessageClassID;
                para1[7].Value = msm.ReplyFor;
                para1[8].Value = msm.MessageType;
                using (SqlConnection conn = new SqlConnection(DBHelper.connString))
                {
                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction();
                    try
                    {
                        DBHelper.ExecuteNonQuery(tran, insertSql2.ToString(), para1, CommandType.Text);

                        string sqlStr2 = "select max(id) from messagesend where sender = '" + msm.Sender.ToString() + "'";
                        int MessagesendId = Convert.ToInt32(DBHelper.ExecuteScalar(tran, sqlStr2, CommandType.Text));

                        System.Text.StringBuilder insertSql = new System.Text.StringBuilder();
                        insertSql.Append("insert into MessageReceive values(@messagesendId,@role,@receive,@title,@content,1,@sender,@senddate,0,0,0,null,null,@msgclassid,@replyfor,@messagetype) ");
                        SqlParameter[] para ={
												 new SqlParameter("@MessagesendId",SqlDbType.Int),
												 new SqlParameter("@role",SqlDbType.Char,10),
												 new SqlParameter("@receive",SqlDbType.VarChar,4000),
												 new SqlParameter("@title",SqlDbType.VarChar,50),
                                                 new SqlParameter("@content",SqlDbType.NVarChar,4000),
												 new SqlParameter("@sender",SqlDbType.VarChar,20),
												 new SqlParameter("@senddate",SqlDbType.DateTime,8),
											     new SqlParameter("@msgclassid",SqlDbType.Int),
							                     new SqlParameter("@replyfor",SqlDbType.Int),
                                                 new SqlParameter("@messagetype",SqlDbType.Char)		
											 };
                        para[0].Value = MessagesendId;
                        para[1].Value = msm.LoginRole.ToString();
                        para[2].Value = msm.Receive.ToString();
                        para[3].Value = msm.InfoTitle.ToString();
                        para[4].Value = msm.Content.ToString();
                        para[5].Value = msm.Sender.ToString();
                        para[6].Value = msm.Senddate.ToUniversalTime();
                        para[7].Value = msm.MessageClassID;
                        para[8].Value = msm.ReplyFor;
                        para[9].Value = msm.MessageType;
                        i = DBHelper.ExecuteNonQuery(tran, insertSql.ToString(), para, CommandType.Text);
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            catch (Exception)
            {

            }
            return i;
        }

        public int getReadTypr(string id)
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("Select ReadFlag from MessageReceive where id=" + id));
        }

        public string getReceive(int type)
        {
            return Convert.ToString(DBHelper.ExecuteScalar("SELECT Receive FROM MessageReceive WHERE id=" + type + ""));
        }

        public int getMessageReciveInfo(int type)
        {
            int n = 0;
            int i = 0;
            string LoginRole2;
            string Recive2;
            string InfoTitle2;
            string SenderRole2;
            string Sender2;
            string Senddate2;
            int DropFlag2;
            int ReadFlag2;
            int MessagesendId;
            string StrSql = "SELECT * FROM MessageReceive WHERE id=" + type + "";
            SqlDataReader reader = DBHelper.ExecuteReader(StrSql);
            MessageReciveModel message = new MessageReciveModel();
            if (reader.Read())
            {
                message.Messagesendid = Convert.ToInt32(reader["Messagesendid"]);
                message.LoginRole = reader["LoginRole"].ToString();
                message.Recive = reader["Receive"].ToString();
                message.SenderRole = reader["SenderRole"].ToString();
                message.Sender = reader["sender"].ToString();
                message.Senddate = Convert.ToDateTime(reader["Senddate"]);
                message.InfoTitle = reader["InfoTitle"].ToString();
            }
            reader.Close();
            MessagesendId = Convert.ToInt32(message.Messagesendid.ToString());
            LoginRole2 = message.LoginRole.ToString();
            Recive2 = message.Recive.ToString();
            InfoTitle2 = message.InfoTitle.ToString(); ;
            SenderRole2 = message.SenderRole.ToString(); ;
            Sender2 = message.Sender.ToString(); ;
            Senddate2 = message.Senddate.ToString(); ;
            DropFlag2 = Convert.ToInt32(message.DropFlag);
            ReadFlag2 = Convert.ToInt32(message.ReadFlag);
            string sqlstr_temple = @"insert into MessageDrop (MessagesendId,LoginRole,Receive,InfoTitle,SenderRole,Sender,Senddate,DropFlag,ReadFlag,tabel) values (" + MessagesendId + ",'" + LoginRole2 + "','" + Recive2 + "','" + InfoTitle2 + "','" + SenderRole2 + "','" + Sender2 + "','" + Senddate2 + "',1,1,2)";
            n = DBHelper.ExecuteNonQuery(sqlstr_temple);
            if (n > 0)
            {
                i = DBHelper.ExecuteNonQuery("delete from MessageReceive WHERE id=" + type + " ");
                {
                    if (i > 0)
                    {
                        return i;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            else
            {
                return 0;
            }
        }

        public int getMessageID(int type)
        {
            int messageid = 0;
            string StrSql = "Select id from MessageSend where id=" + type;
            SqlDataReader dr = DBHelper.ExecuteReader(StrSql);
            if (dr.Read())
            {
                messageid = Convert.ToInt32(dr["ID"]);
            }
            dr.Close();
            return messageid;
        }

        public int delSend(int type, int messageid)
        {
            int a = 0;
            int b = 0;
            //int c = 0;
            string sql1 = "delete from MessageSend WHERE id=" + type;
            a = DBHelper.ExecuteNonQuery(sql1, CommandType.Text);
            if (a > 0)
            {
                string sql2 = "delete from MessageReceive WHERE messagesendId=" + messageid;
                b = DBHelper.ExecuteNonQuery(sql2, CommandType.Text);
                return b;
                //if (b > 0)
                //{
                //    string sql3 = "delete from MessageDrop WHERE messagesendId=" + messageid;
                //    c = DBHelper.ExecuteNonQuery(tran, sql2, CommandType.Text);
                //    return c;
                //}
            }
            else
            {
                return -1;
            }
        }

        public int delMessageDrop(int id)
        {
            if (DBHelper.ExecuteNonQuery("DelMessageDropById", new SqlParameter("@id", id), CommandType.StoredProcedure) > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
            //int i = 0;
            //i = DBHelper.ExecuteNonQuery("Delete MessageDrop WHERE id=" + id);
            //return i;
        }

        public static MessageReciveModel getMessageRecive(int id)
        {
            string sql = "Update  MESSAGERECEIVE set ReadFlag=1 where id=" + id;
            DBHelper.ExecuteNonQuery(sql);
            SqlDataReader reader = DBHelper.ExecuteReader("select messagesendId,LoginRole,Receive,SenderRole,Sender,SendDate,InfoTitle from messagereceive where id=" + id);
            MessageReciveModel message = new MessageReciveModel();
            if (reader.Read())
            {
                message.Messagesendid = Convert.ToInt32(reader["Messagesendid"]);
                message.LoginRole = reader["LoginRole"].ToString();
                message.Recive = reader["Receive"].ToString();
                message.SenderRole = reader["SenderRole"].ToString();
                message.Sender = reader["sender"].ToString();
                message.Senddate = Convert.ToDateTime(reader["Senddate"]);
                message.InfoTitle = reader["InfoTitle"].ToString();
            }
            reader.Close();
            return message;
        }

        public static string getContent(int messageid)
        {
            string sql = "select content from messagesend where id=" + messageid;
            SqlDataReader dr = DBHelper.ExecuteReader(sql);
            string content = "";
            if (dr.Read())
            {
                content = dr["content"].ToString();
            }
            dr.Close();
            return content;
        }

        public static MessageDropModel getMessageDrop(int id)
        {
            SqlDataReader reader = DBHelper.ExecuteReader("select LoginRole,Receive,messagesendId,SenderRole,Sender,SendDate,InfoTitle from messageDrop where id=" + id);
            MessageDropModel message = new MessageDropModel();
            if (reader.Read())
            {
                message.Messagesendid = Convert.ToInt32(reader["Messagesendid"]);
                message.LoginRole = reader["LoginRole"].ToString();
                message.Receive = reader["Receive"].ToString();
                message.SenderRole = reader["SenderRole"].ToString();
                message.Sender = reader["sender"].ToString();
                message.Senddate = Convert.ToDateTime(reader["Senddate"]);
                message.InfoTitle = reader["InfoTitle"].ToString();
            }
            reader.Close();
            return message;
        }

        /// <summary>
        /// 查询指定条件的发件箱表中的记录
        /// </summary>
        /// <param name="columnNames">列名</param>
        /// <param name="conditions">查询条件</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMessageSendInfoByConditions(string columnNames, string conditions)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@columnNames",SqlDbType.NVarChar,1000),
                new SqlParameter("@conditions",SqlDbType.NVarChar,1000)
            };
            sparams[0].Value = columnNames;
            sparams[1].Value = conditions;

            return DBHelper.ExecuteDataTable("GetMessageSendInfoByConditions", sparams, CommandType.StoredProcedure);
        }
    }
}
