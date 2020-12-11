using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/**
 *  创建人：  郑华超
 *  创建时间：2009.8.27
 *  文件名：NetWorkDisplayStatusModel.cs
 *  功能：网络显示字段控制模型
 */
namespace Model
{
    /// <summary>
    /// 表4.1—59网络显示字段控制表
    /// </summary>
    public class NetWorkDisplayStatusModel
    {
        private int iD;

        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            get { return iD; }
        }
        private string field;

        /// <summary>
        /// 字段Field
        /// </summary>
        public string Field
        {
            get { return field; }
            set { field = value; }
        }
        
        private string name;
        /// <summary>
        /// 字段名称Name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int flag;

        /// <summary>
        /// 状态-->0：未选中/1：选中      flag
        /// </summary>
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        public NetWorkDisplayStatusModel() { }

        public NetWorkDisplayStatusModel(int id) 
        {
            this.iD = id;
        
        }
    }
}
