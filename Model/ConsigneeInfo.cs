using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
     [Serializable]
    public class ConsigneeInfo
    {
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
        private string consignee;

        public string Consignee
        {
            get { return consignee; }
            set { consignee = value; }
        }
        private string moblieTele;

        public string MoblieTele
        {
            get { return moblieTele; }
            set { moblieTele = value; }
        }


        private string cPCCode;

        public string CPCCode
        {
            get { return cPCCode; }
            set { cPCCode = value; }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string conZipCode;
        public string ConZipCode
        {
            get { return conZipCode; }
            set { conZipCode = value; }
        }

        private bool isDefault;
        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }
    }
}
