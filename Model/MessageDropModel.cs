using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 创建者：孙延昊
 * 创建时间：2009年8月27日 AM 10：07
 * 文件名：MessageDropModel
 * 功能：废件箱表模型
 * **/
namespace Model
{
    /// <summary>
    /// 废件箱表
    /// </summary>
    public class MessageDropModel
    {

        public MessageDropModel()
        { }
        public MessageDropModel(int id)
        {
            this.iD = id;
        }

        private int iD;

        public int ID
        {
            get { return iD; }

        }
        private int messagesendid;
        /// <summary>
        /// 发布信息ID
        /// </summary>
        public int Messagesendid
        {
            get { return messagesendid; }
            set { messagesendid = value; }
        }
        private string loginRole;
        /// <summary>
        /// 类型：0为管理员；1为店铺，2为会员
        /// </summary>
        public string LoginRole
        {
            get { return loginRole; }
            set { loginRole = value; }
        }
        private string receive;
        /// <summary>
        /// “*”表示公告，接收编号
        /// </summary>
        public string Receive
        {
            get { return receive; }
            set { receive = value; }
        }
        private string infoTitle;
        /// <summary>
        /// 信息标题
        /// </summary>
        public string InfoTitle
        {
            get { return infoTitle; }
            set { infoTitle = value; }
        }
        private string senderRole;
        /// <summary>
        /// 信息发布人类型：0为管理员；1为店铺，2为会员
        /// </summary>
        public string SenderRole
        {
            get { return senderRole; }
            set { senderRole = value; }
        }
        private string sender;
        /// <summary>
        /// 信息发布人
        /// </summary>
        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }
        private DateTime senddate;
        /// <summary>
        /// 信息发布日期
        /// </summary>
        public DateTime Senddate
        {
            get { return senddate; }
            set { senddate = value; }
        }
        private int dropFlag;
        /// <summary>
        /// 是否删除
        /// </summary>
        public int DropFlag
        {
            get { return dropFlag; }
            set { dropFlag = value; }
        }
        private int readFlag;
        /// <summary>
        /// 是否阅读
        /// </summary>
        public int ReadFlag
        {
            get { return readFlag; }
            set { readFlag = value; }
        }
        private int tabel;
        /// <summary>
        /// 那个表过来的 1是收件箱，2是发件箱，3是公告
        /// </summary>
        public int Tabel
        {
            get { return tabel; }
            set { tabel = value; }
        }

    }
}
