using System;
using System.Collections.Generic;
using System.Text;

/*
 *
 *创建时间：09/8/27
 *文件名：CityModel.cs
 *功能：系统黑名单表
 */

namespace Model
{
    /// <summary>
    /// 系统黑名单表
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
        /// 用户编号
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
        /// 状态
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
        /// 用户类型
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
        /// IP地址
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
        /// 黑名单组ID,0表示没有黑名单组
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
        /// 录入日期
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
        /// 操作者IP
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
        /// 操作者编号
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
