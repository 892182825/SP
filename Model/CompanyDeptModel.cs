using System;
using System.Collections.Generic;
using System.Text;

/*
 *
 *����ʱ�䣺09/8/27
 *�ļ�����CityModel.cs
 *���ܣ���˾���ű�
 */

namespace Model
{
    /// <summary>
    /// ��˾���ű�
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
    }
}
