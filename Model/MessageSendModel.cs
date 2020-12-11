using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 创建者：孙延昊
 * 创建时间：2009年8月27日 AM 10：07
 * 文件名：MessageSendModel
 * 功能：发件箱表模型
 * **/
namespace Model
{
    /// <summary>
    /// 发件箱表
    /// </summary>
    public class MessageSendModel
    {
        public MessageSendModel() { }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        //public int ID
        //{
        //    get { return iD; }

        //}
        public void SetNoCondition()
        {
            this.ConditionLevel = -1;
            this.ConditionBonusFrom = -1;
            this.ConditionBonusTo = -1;
            this.ConditionLeader = "";
            this.ConditionRelation = '0';
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
        private string content;
        /// <summary>
        /// 信息内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
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

        private string countryCode;
        public string CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; }
        }

        private string languageCode;
        public string LanguageCode
        {
            get { return languageCode; }
            set { languageCode = value; }
        }
        private int msgClassID;
        /// <summary>
        /// Message Class ID
        /// </summary>
        public int MessageClassID
        {
            get { return msgClassID; }
            set { msgClassID = value; }
        }

        private int replyFor;
        /// <summary>
        /// For which Message ID
        /// </summary>
        public int ReplyFor
        {
            get { return replyFor; }
            set { replyFor = value; }
        }

        private int conditionLevel;
        /// <summary>
        /// Condition of Member or Store Level
        /// </summary>
        public int ConditionLevel
        {
            get { return conditionLevel; }
            set { conditionLevel = value; }
        }

        private double conditionBonusFrom;
        /// <summary>
        /// Condition of Bonus From
        /// </summary>
        public double ConditionBonusFrom
        {
            get { return conditionBonusFrom; }
            set { conditionBonusFrom = value; }
        }

        private double conditionBonusTo;
        /// <summary>
        /// Condition of Bonus To
        /// </summary>
        public double ConditionBonusTo
        {
            get { return conditionBonusTo; }
            set { conditionBonusTo = value; }
        }


        private string conditionLeader;
        /// <summary>
        /// Condition of Leader number
        /// </summary>
        public string ConditionLeader
        {
            get { return conditionLeader; }
            set { conditionLeader = value; }
        }


        private char conditionRelation;
        /// <summary>
        /// Condition of Relation
        /// </summary>
        public char ConditionRelation
        {
            get { return conditionRelation; }
            set { conditionRelation = value; }
        }

        private int qishu;
        /// <summary>
        /// Qishu when send 
        /// </summary>
        public int Qishu
        {
            get { return qishu; }
            set { qishu = value; }
        }

        private char messageType;
        /// <summary>
        /// Message Type (Mail or Affiche)
        /// </summary>
        public char MessageType
        {
            get { return messageType; }
            set { messageType = value; }
        }

    }
}
