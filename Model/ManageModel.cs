using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 *
 * 创建者：张朔
 * 创建时间：2009年8月27日 AM 10：07
 * 文件名：ManageModel
 * 功能:公司管理员信息表
 */

namespace Model
{
    /// <summary>
    /// 公司管理员信息表
    /// </summary>
    public class ManageModel
    {
        public ManageModel() { }
        public ManageModel(int id)
        {
            this.iD = id;        
        }
        private int iD = 0;
        /// <summary>
        /// 标示
        /// </summary>
        public int ID
        {
            get { return iD; }
        }

        private string number;
        /// <summary>
        /// 管理员编号
        /// </summary>
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string name;
        /// <summary>
        /// 管理员姓名
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string post;
        /// <summary>
        /// 岗位
        /// </summary>
        public string Post
        {
            get { return post; }
            set { post = value; }
        }
        private string branch;
        /// <summary>
        /// 部门
        /// </summary>
        public string Branch
        {
            get { return branch; }
            set { branch = value; }
        }
        private string loginPass;
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string LoginPass
        {
            get { return loginPass; }
            set { loginPass = value; }
        }
        private DateTime beginDate;
        /// <summary>
        /// 管理员日期
        /// </summary>
        public DateTime BeginDate
        {
            get { return beginDate; }
            set { beginDate = value; }
        }
        private DateTime lastLoginDate;
        /// <summary>
        /// 最后登录日期
        /// </summary>
        public DateTime LastLoginDate
        {
            get { return lastLoginDate; }
            set { lastLoginDate = value; }
        }
        private int status;
        /// <summary>
        /// 是否有效
        /// </summary>
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private string permissionMan;
        /// <summary>
        /// 权限人
        /// </summary>
        public string PermissionMan
        {
            get { return permissionMan; }
            set { permissionMan = value; }
        }
        private int roleID;
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID
        {
            get { return roleID; }
            set { roleID = value; }
        }

        private int isViewPermissions;
        /// <summary>
        /// 是否有查看会员安置的权限
        /// </summary>
        public int IsViewPermissions
        {
            get { return isViewPermissions; }
            set { isViewPermissions = value; }
        }
        private int isRecommended;
        /// <summary>
        /// 是否有查看会员推荐的权限
        /// </summary>
        public int IsRecommended
        {
            get { return isRecommended; }
            set { isRecommended = value; }
        }
    }
}
