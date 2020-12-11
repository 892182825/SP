using System;
using System.Collections.Generic;
using System.Text;

/*
 *
 *����ʱ�䣺09/8/27
 *�ļ�����CityModel.cs
 *���ܣ�ϵͳ������־��
 */

namespace Model
{
    /// <summary>
    /// ϵͳ������־��
    /// </summary>
    public class ChangeLogsModel
    {
        public ChangeLogsModel()
        { 
        }

        public ChangeLogsModel(int id)
        {
            this.id = id;
        }

        private int id;
        private short type;
        private DateTime time;
        private string sIP;
        private string category;
        private string from;
        private string fromUserType;
        private string to;
        private string toUserType;
        private int expectNum;
        private string remark;

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
        /// ��������
        /// </summary>
        public short Type
        {
            get 
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }

        /// <summary>
        /// IP��ַ
        /// </summary>
        public string SIP
        {
            get
            {
                return sIP;
            }
            set
            {
                sIP = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }

        /// <summary>
        /// ��Դ
        /// </summary>
        public string From
        {
            get
            {
                return from;
            }
            set
            {
                from = value;
            }
        }

        /// <summary>
        /// �û�������Դ
        /// </summary>
        public string FromUserType
        {
            get
            {
                return fromUserType;
            }
            set
            {
                fromUserType = value;
            }
        }

        /// <summary>
        /// ȥ��
        /// </summary>
        public string To
        {
            get
            {
                return to;
            }
            set
            {
                to = value;
            }
        }

        /// <summary>
        /// �û�����ȥ��
        /// </summary>
        public string ToUserType
        {
            get
            {
                return toUserType;
            }
            set
            {
                toUserType = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public int ExpectNum
        {
            get
            {
                return expectNum;
            }
            set
            {
                expectNum = value;
            }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
            }
        }
    }
}
