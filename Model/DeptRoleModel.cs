using System;
using System.Collections.Generic;
using System.Text;

/*
 *
 *创建时间：09/8/27
 *文件名：CityModel.cs
 *功能：公司部门角色表
 */

namespace Model
{
    /// <summary>
    /// 公司部门角色表
    /// </summary>
    public class DeptRoleModel
    {
        public DeptRoleModel()
        { }

        public DeptRoleModel(int id)
        {
            this.id = id;
        }


        public int ParentId { get; set; }
        /// <summary>
        /// 角色权限
        /// </summary>
        public System.Collections.Hashtable htbPerssion { get; set; }
        private int id;
        private string name;
        private int deptID;
        private DateTime adddate;
        private int permissionManID;
        private int allot;

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
        /// 角色名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// 部门ID
        /// </summary>
        public int DeptID
        {
            get
            {
                return deptID;
            }
            set
            {
                deptID = value;
            }
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime Adddate
        {
            get
            {
                return adddate;
            }
            set
            {
                adddate = value;
            }
        }

        /// <summary>
        /// 权限人(管理员ID)
        /// </summary>
        public int PermissionManID
        {
            get
            {
                return permissionManID;
            }
            set
            {
                permissionManID = value;
            }
        }

        /// <summary>
        /// 标识角色是否可以将自己的权限下放到自己创建的角色中去 0不可以 1 可以
        /// </summary>
        public int Allot
        {
            get
            {
                return allot;
            }
            set
            {
                allot = value;
            }
        }

    }
}
