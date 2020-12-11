using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class EncryptionSetting
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string encryptionKey;

        public string EncryptionKey
        {
            get { return encryptionKey; }
            set { encryptionKey = value; }
        }
        private int encryptionValue;

        public int EncryptionValue
        {
            get { return encryptionValue; }
            set { encryptionValue = value; }
        }
        private string remark;

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
    }
}
