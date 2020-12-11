using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;

namespace BLL.other.Company
{
    /// <summary>
    /// 发件箱（对发件箱进行管理）
    /// </summary>
    public class MessageSendBLL
    {
         /// <summary>
        /// 根据id删除发件箱的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DelMessageSendById(int id,string optr,int OperatorType)
        {
            //MessageSendDAL dao = new MessageSendDAL();

            BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("MessageSend", "ltrim(rtrim(id))");
            cl_h_info.AddRecord(id);
            cl_h_info.DeletedIntoLogs(BLL.CommonClass.ChangeCategory.company21, id.ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype10);

            return MessageSendDAL.DelMessageSendById(id,optr,OperatorType);
        }
          /// <summary>
        /// 删除已经删除的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DelMessageDropbyID(int id)
        {
            BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("V_DroppedMessage", "ltrim(rtrim(id))"); 
            cl_h_info.AddRecord(id);
            cl_h_info.DeletedIntoLogs(BLL.CommonClass.ChangeCategory.company22, id.ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype10);

            return MessageSendDAL.DelMessageDropbyID(id);
        }
           /// <summary>
        /// 发布公告
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool Addsendaffiche(MessageSendModel message)
        {
          
            return MessageSendDAL.Addsendaffiche(message);
        }
         /// <summary>
        /// 保存邮件
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AddMessageSend(MessageSendModel message)
        {
           
            return MessageSendDAL.AddMessageSend(message);
        }
         /// <summary>
        /// 根据编号查询发件箱信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MessageSendModel GetMessageSendById(int id)
        {
            return MessageSendDAL.GetMessageSendById(id);
        }

        /// <summary>
        /// 获取公告的发送角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetSendRole(string id)
        {
            return MessageSendDAL.GetSendRole(id);
        }
        /// <summary>
        /// 检查编号是否存在
        /// </summary>
        /// <param name="type">0管理员，1会员，2店铺</param>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static bool CheckNumber(int type, string number)
        {
          //   MessageSendDAL dao=new MessageSendDAL ();
             return MessageSendDAL.CheckNumber(type, number);
        }
        public string GetRole(string role)
        {
            //MessageSendDAL dao = new MessageSendDAL();
            return MessageSendDAL.GetRole(role);
        }
        public int check(string sql)
        {
           // MessageSendDAL dao = new MessageSendDAL();
            return MessageSendDAL.check(sql);
        }
        public int StoreSendToManage(MessageSendModel msm)
        {
            //MessageSendDAL dao = new MessageSendDAL();
            return MessageSendDAL.StoreSendToManage(msm);
        }
        public int MemberSendToManage(MessageSendModel msm)
        {
            //MessageSendDAL dao = new MessageSendDAL();
            return MessageSendDAL.MemberSendToManage(msm);
        }

        public int getReadType(string id)
        {
            MessageSendDAL dao = new MessageSendDAL();
            return dao.getReadTypr(id);
        }
        public string getReceive(int type)
        {
            MessageSendDAL dao = new MessageSendDAL();
            return dao.getReceive(type);
        }
        public int getMessageReciveInfo(int type)
        {
            MessageSendDAL dao = new MessageSendDAL();
            return dao.getMessageReciveInfo(type);
        }
        public int getmessageID(int type)
        { 
            MessageSendDAL dao = new MessageSendDAL();
            return dao.getMessageID(type);
        }
        public int delSend(int type, int messageid)
        {
            MessageSendDAL dao = new MessageSendDAL();
            return dao.delSend(type, messageid);
        }
        public int delMessageDrop(int id)
        {
            BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("V_DroppedMessage", "ltrim(rtrim(id))");
            cl_h_info.AddRecord(id);
            cl_h_info.DeletedIntoLogs(BLL.CommonClass.ChangeCategory.company22, id.ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype10);

            MessageSendDAL dao = new MessageSendDAL();
            return dao.delMessageDrop(id);
        }
        public static MessageReciveModel getMessageRecive(int id)
        {
            return MessageSendDAL.getMessageRecive(id);
        }
        public static string getContent(int messageid)
        {
            MessageSendDAL dao = new MessageSendDAL();
            return MessageSendDAL.getContent(messageid);
        }
        public static MessageDropModel getMessageDrop(int id)
        {
            return MessageSendDAL.getMessageDrop(id);
        }
        public int updresources(string resid)
        {
            ResourcesDAL rd = new ResourcesDAL();
            return rd.updresources(resid);
        }
    }
}
