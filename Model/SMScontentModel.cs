using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SMScontentModel
    {
        public SMScontentModel() { }

        public SMScontentModel(int productid)
        {
            this.productid = productid;
        }


        private int productid;

        public int Productid
        {
            get { return productid; }
            set { productid = value; }
        }
        private int pid;

        public int Pid
        {
            get { return pid; }
            set { pid = value; }
        }
        private int isfold;

        public int Isfold
        {
            get { return isfold; }
            set { isfold = value; }
        }
        private string productname;

        public string Productname
        {
            get { return productname; }
            set { productname = value; }
        }
        private string countrycode;

        public string Countrycode
        {
            get { return countrycode; }
            set { countrycode = value; }
        }
        private string bianhao;

        public string Bianhao
        {
            get { return bianhao; }
            set { bianhao = value; }
        }

    }
}
