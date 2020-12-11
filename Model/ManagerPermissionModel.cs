using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 *
 * 创建者：张朔
 * 创建时间：2009年8月27日 AM 10：07
 * 文件名：ManagerPermissionModel
 * 公司角色权限表
 */

namespace Model
{
    /// <summary>
    /// 公司角色权限表
    /// </summary>
    public class ManagerPermissionModel
    {
        public ManagerPermissionModel() { }
        public ManagerPermissionModel(int id) 
        {
            this.iD = id;
        }

        private int iD;
        /// <summary>
        /// 标示
        /// </summary>
        public int ID
        {
            get { return iD; }
        }
        private int permissionId;
        /// <summary>
        /// 权根编号
        /// </summary>
        public int PermissionId
        {
            get { return permissionId; }
            set { permissionId = value; }
        }
        private int managerId;
        /// <summary>
        /// 角色ID
        /// </summary>
        public int ManagerId
        {
            get { return managerId; }
            set { managerId = value; }
        }
        private int state;
        /// <summary>
        /// 权限等级(权限普通0，高级1)
        /// </summary>
        public int State
        {
            get { return state; }
            set { state = value; }
        }
    }
}
