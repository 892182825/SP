using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Model.Other
{
    public class MD5Help
    {
        public static string MD5Encrypt(string str)
        {
            string number = "";
            return  Encryption.Encryption.GetEncryptionPwd(str, number);
        }

        public static string MD5Decrypt(string str)
        {
            string number = "";
            return Encryption.Encryption.GetEncryptionPwd(str, number);            
        }

        public static string MD5Encrypt2(string str)
        {                         
            string number="";
            return Encryption.Encryption.GetEncryptionPwd(str, number);         
        } 

        public static string MD5Decrypt2(string str)
        {
            string number = "";
            return  Encryption.Encryption.GetEncryptionPwd(str, number);           
        }
    }
}
