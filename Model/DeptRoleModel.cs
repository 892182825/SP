using System;
using System.Collections.Generic;
using System.Text;

/*
 *
 *����ʱ�䣺09/8/27
 *�ļ�����CityModel.cs
 *���ܣ���˾���Ž�ɫ��
 */

namespace Model
{
    /// <summary>
    /// ��˾���Ž�ɫ��
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
        /// ��ɫȨ��
        /// </summary>
        public System.Collections.Hashtable htbPerssion { get; set; }
        private int id;
        private string name;
        private int deptID;
        private DateTime adddate;
        private int permissionManID;
        private int allot;

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
        /// ��ɫ����
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
        /// ����ID
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
        /// ���ʱ��
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
        /// Ȩ����(����ԱID)
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
        /// ��ʶ��ɫ�Ƿ���Խ��Լ���Ȩ���·ŵ��Լ������Ľ�ɫ��ȥ 0������ 1 ����
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
