using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class DefaultMessage
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int expectNum;
        public int ExpectNum
        {
            get { return expectNum; }
            set { expectNum = value; }
        }

        private string oldId;
        public string OldId
        {
            get { return oldId; }
            set { oldId = value; }
        }

        private string newId;
        public string NewId
        {
            get { return newId; }
            set { newId = value; }
        }

        private string operateIp;
        public string OperateIp
        {
            get { return operateIp; }
            set { operateIp = value; }
        }

        private string operateNum;
        public string OperateNum
        {
            get { return operateNum; }
            set { operateNum = value; }
        }

        private DateTime updateTime;
        public DateTime UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }

        private int type;
        public int Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
