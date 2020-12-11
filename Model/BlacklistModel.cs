using System;
using System.Collections.Generic;
using System.Text;

/*
 *
 *����ʱ�䣺09/8/27
 *�ļ�����CityModel.cs
 *���ܣ�ϵͳ��������
 */

namespace Model
{
    /// <summary>
    /// ϵͳ��������
    /// </summary>
    public class BlacklistModel
    {
        public BlacklistModel()
        { }

        public BlacklistModel(int id)
        {
            this.id = id;
        }

        private int id;
        private string userID;
        private byte state;
        private byte userType;
        private string userIP;
        private int groupID;
        private DateTime blackDate;
        private string operateIP;
        private string operateBH;
        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        private string mobile;

        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        /// <summary>
        /// ���
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
        }
        /// <summary>
        /// �û����
        /// </summary>
        public string UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID=value;
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public byte State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
        /// <summary>
        /// �û�����
        /// </summary>
        public byte UserType
        {
            get
            {
                return userType;
            }
            set
            {
                userType = value;
            }
        }
        /// <summary>
        /// IP��ַ
        /// </summary>
        public string UserIP
        {
            get
            {
                return userIP==null?"":userIP;
            }
            set
            {
                userIP = value;
            }
        }
        /// <summary>
        /// ��������ID,0��ʾû�к�������
        /// </summary>
        public int GroupID
        {
            get
            {
                return groupID;
            }
            set
            {
                groupID = value;
            }
        }
        /// <summary>
        /// ¼������
        /// </summary>
        public DateTime BlackDate
        {
            get
            {
                return blackDate;
            }
            set
            {
                blackDate = value;
            }
        }
        /// <summary>
        /// ������IP
        /// </summary>
        public string OperateIP
        {
            get
            {
                return operateIP;
            }
            set
            {
                operateIP = value;
            }
        }
        /// <summary>
        /// �����߱��
        /// </summary>
        public string OperateBH
        {
            get
            {
                return operateBH;
            }
            set
            {
                operateBH = value;
            }
        }
    }
}
