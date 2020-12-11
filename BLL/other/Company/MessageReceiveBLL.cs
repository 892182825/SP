using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using Model;
using System.Data.SqlClient;

namespace BLL.other.Company
{
    public class MessageReceiveBLL
    {
        /// <summary>
        /// 根据id查询发件人的编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object GetMessageReceiveSender(int id)
        {
            return MessageReceiveDAL.GetMessageReceiveSender(id);
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
        public bool InsertMessageReceive(int messagesendId, string role, string Number, string title, string sender, string content)
        {
            return MessageReceiveDAL.InsertMessageReceive(messagesendId, role, Number, title, sender, content);
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
        public bool insertReceiveSend(string role, string number, string title, string content, string sender)
        {
            return MessageReceiveDAL.insertReceiveSend(role, number, title, content, sender);
        }
        /// <summary>
        /// 根据收件箱的信息id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetReceivemenegerCount(int id)
        {
            return MessageReceiveDAL.GetReceivemenegerCount(id);
        }
        /// <summary>
        /// 查询收的信息用于转发
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetReceivemeneger(int id)
        {
            return MessageReceiveDAL.GetReceivemeneger(id);
        }
        /// <summary>
        /// 根据id删除收件箱信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelMessageReceive(int id, string optr, int OperatorType)
        {
            BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("MessageReceive", "ltrim(rtrim(id))");
            cl_h_info.AddRecord(id);
            cl_h_info.DeletedIntoLogs(BLL.CommonClass.ChangeCategory.company20, id.ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype10);

            return MessageReceiveDAL.DelMessageReceive(id, optr, OperatorType);
        }

        /// <summary>
        /// 返回要导出excel的表格
        /// </summary>
        /// <param name="par"></param>
        /// <param name="conntion"></param>
        /// <returns></returns>
        public DataTable GetExcelTable(string[] par, string conntion)
        {
            return MessageReceiveDAL.GetExcelTable(par, conntion);
        }
        public int delGongGao(int id)
        {
            return MessageReceiveDAL.delGongGao(id);
        }

        /// <summary>
        /// 返回公告信息
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public SqlDataReader GetGongGao(string tablename, int id)
        {
            return MessageReceiveDAL.GetGongGao(tablename, id);
        }
        /// <summary>
        /// 返回公告信息
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public SqlDataReader GetGongGao(string tablename, int id, string receiver)
        {
            return MessageReceiveDAL.GetGongGao(tablename, id, receiver);
        }
        /// <summary>
        /// 返回邮件信息
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="id"></param>
        /// <param name="receiver"></param>
        /// <returns></returns>
        public SqlDataReader GetEmailDetail(string tablename, int id, string receiver)
        {
            return MessageReceiveDAL.GetEmailDetail(tablename, id, receiver);
        }

        /// <summary>
        /// 更新是否阅读
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="sqlid"></param>
        public void UpdateIsRead(string tableName, string sqlid)
        {
            DAL.MessageReceiveDAL.UpdateIsRead(tableName, sqlid);
        }

        /// <summary>
        /// 更新会员信息
        /// </summary>
        public void UpdateMember()
        {
            DAL.MessageReceiveDAL.UpdateMember();
        }

        /// <summary>
        /// 更新机构信息
        /// </summary>
        public void UpdateStore()
        {
            DAL.MessageReceiveDAL.UpdateStore();
        }
    }
}