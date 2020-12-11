using System;
using System.Collections.Generic;
using System.Text;

/*
 *
 *创建时间：09/8/27
 *文件名：CityModel.cs
 *功能：公司部门表
 */

namespace Model
{
    /// <summary>
    /// 公司部门表
    /// </summary>
    public class CompanyDeptModel
    {
        public CompanyDeptModel()
        {
 
        }

        public CompanyDeptModel(int id)
        {
            this.id = id;
        }

        private int id;
        private string dept;
        private DateTime adddate;

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
        /// 部门名称
        /// </summary>
        public string Dept
        {
            get
            {
                return dept;
            }
            set
            {
                dept = value;
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
    }
}
