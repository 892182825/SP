using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SetGivePVModel
    {
        private int id;
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        private double totalpvstart;
        /// <summary>
        /// 总开始pv额度
        /// </summary>
        public double TotalPVStart
        {
            get { return totalpvstart; }
            set { totalpvstart = value; }
        }

        private double totalpvend;
        /// <summary>
        /// 总结束pv额度
        /// </summary>
        public double TotalPVEnd
        {
            get { return totalpvend; }
            set { totalpvend = value; }
        }

        private string operatenum;
        /// <summary>
        /// 操作编号
        /// </summary>
        public string OperateNum
        {
            get { return operatenum; }
            set { operatenum = value; }
        }

        private DateTime operatetime;
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime
        {
            get { return operatetime; }
            set { operatetime = value; }
        }

        private string operateip;
        /// <summary>
        /// 操作IP
        /// </summary>
        public string OperateIP
        {
            get { return operateip; }
            set { operateip = value; }
        }

    }
}
