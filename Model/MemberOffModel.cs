using System;


namespace Model
{
    public class MemberOffModel
    {
        public MemberOffModel()
        { }

        #region Model

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string number;

        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int zxqishu;

        public int Zxqishu
        {
            get { return zxqishu; }
            set { zxqishu = value; }
        }
        private string storeid;

        public string Storeid
        {
            get { return storeid; }
            set { storeid = value; }
        }
        private string papernumber;

        public string Papernumber
        {
            get { return papernumber; }
            set { papernumber = value; }
        }
        private string mobiletele;
        private DateTime zxfate;

        public DateTime Zxfate
        {
            get { return zxfate; }
            set { zxfate = value; }
        }
        private int iszx;

        public int Iszx
        {
            get { return iszx; }
            set { iszx = value; }
        }

        private string offReason;

        public string OffReason
        {
            get { return offReason; }
            set { offReason = value; }
        }

        private string operatorNo;

        public string OperatorNo
        {
            get { return operatorNo; }
            set { operatorNo = value; }
        }

        private string operatorName;

        public string OperatorName
        {
            get { return operatorName; }
            set { operatorName = value; }
        }
        #endregion 
    }

}
