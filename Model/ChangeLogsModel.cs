using System;
using System.Collections.Generic;
using System.Text;

/*
 *
 *创建时间：09/8/27
 *文件名：CityModel.cs
 *功能：系统操作日志表
 */

namespace Model
{
    /// <summary>
    /// 系统操作日志表
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
        /// 编号
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// 操作类型
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
        /// 操作时间
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
        /// IP地址
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
        /// 种类
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
        /// 来源
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
        /// 用户类型来源
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
        /// 去向
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
        /// 用户类型去向
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
        /// 期数
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
        /// 备注
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
