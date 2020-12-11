using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class PhoneRecharge
    {
        public PhoneRecharge()
        { }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string rechargeID;

        public string RechargeID
        {
            get { return rechargeID; }
            set { rechargeID = value; }
        }
        private string number;

        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        private decimal addMoney;

        public decimal AddMoney
        {
            get { return addMoney; }
            set { addMoney = value; }
        }
        private int addState;

        public int AddState
        {
            get { return addState; }
            set { addState = value; }
        }
        private DateTime addTime;

        public DateTime AddTime
        {
            get { return addTime; }
            set { addTime = value; }
        }
        private string operateIP;

        public string OperateIP
        {
            get { return operateIP; }
            set { operateIP = value; }
        }
        private string operaterNum;

        public string OperaterNum
        {
            get { return operaterNum; }
            set { operaterNum = value; }
        }

    }
}
