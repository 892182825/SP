using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace BLL.QuickPay
{


    /// <summary>
    /// 加解密码属性及方法
    /// </summary>
    public class SecretSecurity
    {
        public SecretSecurity()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

      
        #region Des24Bit 加密/解密算法，密码必须要24位

      






        /// <summary>
        ///加密字串( 3Des24Bit算法)【DataToEncrypt：待加密字符串；s_Key：密码，24个字符】
        /// </summary>
        /// <param name="encryptString">encryptString：待加密字符串</param>
        /// <param name="s_Key">s_Key：密码，24个字符</param>
        /// <returns>返回密文</returns>
        public static string Encrypt3Des24Bit(string encryptString, string s_Key)
        {
            string cipherText = string.Empty;
            try
            {
                if (s_Key.Length != 24)
                    return encryptString;
                byte[] DataToEncrypt = System.Text.UTF8Encoding.UTF8.GetBytes(encryptString);
                TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
                DES.Key = UTF8Encoding.UTF8.GetBytes(s_Key);
                DES.Mode = CipherMode.ECB;
                ICryptoTransform DESEncrypt = DES.CreateEncryptor();
                byte[] BResult = DESEncrypt.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
                cipherText = System.Convert.ToBase64String(BResult);    //密文
                // Regex rx = new Regex(@"=", RegexOptions.IgnoreCase);
                //cipherText = rx.Replace(cipherText, "＝");
            }
            catch(Exception ex)
            {
                cipherText = "私钥有误";
                throw new Exception(cipherText);
            }
            return cipherText;
        }

        /// <summary>
        /// 解密字串(3Des24Bit算法)【DataToDecrypt：密文；s_Key：密码，24个字符】
        /// </summary>
        /// <param name="decryptString">decryptString：密文</param>
        /// <param name="s_Key">密码，24个字符</param>
        /// <returns>返回明文</returns>
        public static string Decrypt3Des24Bit(string decryptString, string s_Key)
        {
            string plainText = string.Empty;
            try
            {
                if (s_Key.Length != 24)
                    return decryptString;
                // Regex rx = new Regex(@"＝", RegexOptions.IgnoreCase);
                // decryptString = rx.Replace(decryptString, "=");

                byte[] DataToDecrypt = System.Convert.FromBase64String(decryptString);
                TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
                DES.Key = UTF8Encoding.UTF8.GetBytes(s_Key);
                DES.Mode = CipherMode.ECB;
                DES.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                ICryptoTransform DESDecrypt = DES.CreateDecryptor();
                byte[] result = DESDecrypt.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
                plainText = UTF8Encoding.UTF8.GetString(result);                                             //明文
            }
            catch (Exception ex)
            {
                plainText = "私钥或解密密文有误";
                throw new Exception(plainText);
            }
            return plainText;
        }
        #endregion

        /// <summary>
        /// 要与管理系统中保存密码时的私钥一致辞，共24位
        /// </summary>
        private string epayKey = "0000000_QuickePay_DS2010";
        
        /// <summary>
        /// 密码管理私钥
        /// </summary>
        public string EpayKey
        {
            get
            {
                return epayKey;
            }
        }

        /// <summary>
        /// 要与管理系统中保存密码时的私钥一致辞，共24位
        /// </summary>
        private string alipayKey = "0000000000_AliPay_DS2010";

        /// <summary>
        /// 密码管理私钥
        /// </summary>
        public string AlipayKey
        {
            get
            {
                return alipayKey;
            }
        }

        ///<summary>
        /// 要与管理系统中保存密码时的私钥一致辞，共24位
        /// </summary>
        private string emailKey = "00000000000_Email_DS2011";

        public string EmailKey
        {
            get {
                return emailKey;
            }
        }

        #region 随机密码，十六个字符，要16位或24位，不支持中文
        /// <summary>
        /// TripleDes算法加密字串【DataToEncrypt：原文；priKkey：随机密码，十六个字符】【原文有中文传入的原文字符用utf-8编码转换成字节】
        /// </summary>
        /// <param name="DataToEncrypt">DataToEncrypt：原文</param>
        /// <param name="priKkey">priKkey：随机密码,要16位或24位，不支持中文</param>
        /// <returns></returns>
        public static byte[] TripleDesEncrypt(byte[] DataToEncrypt, string priKkey)
        {
            SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] eiv = CodingToByte("AAAAAAAAAAA=", 1);
            byte[] ekey = CodingToByte(priKkey, 2);
            ct = mCSP.CreateEncryptor(ekey, eiv);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(DataToEncrypt, 0, DataToEncrypt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return ms.ToArray();
        }


        /// <summary>
        /// TripleDes算法解密字串【DataToDecrypt：密文；priKkey：随机密码，十六个字符，要16位或24位，不支持中文】【原文有中文返回字节用utf-8编码转换成字符】
        /// </summary>
        /// <param name="DataToDecrypt">DataToDecrypt：密文</param>
        /// <param name="priKkey">priKkey：随机密码，十六个字符，要16位或24位，不支持中文</param>
        /// <returns></returns>
        public static byte[] TripleDesDecrypt(byte[] DataToDecrypt, string priKkey)
        {
            SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] eiv = CodingToByte("AAAAAAAAAAA=", 1);
            byte[] ekey = CodingToByte(priKkey, 2);
            // DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //ct = des.CreateDecryptor(ekey, eiv);
            ct = mCSP.CreateDecryptor(ekey, eiv);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(DataToDecrypt, 0, DataToDecrypt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return ms.ToArray();
        }

        #endregion 


        /// <summary>
        /// 获取字节值，将字符型转换成字节型【strData：原字符；TypeId编码类型1：Base64编码， 2：UTF8编码， 3：ASCII编码】
        /// </summary>
        public static byte[] CodingToByte(string strData, int TypeId)
        {
            byte[] byteData;
            switch (TypeId)
            {
                case 1:
                    byteData = Convert.FromBase64String(strData);
                    break;
                case 2:
                    byteData = System.Text.Encoding.UTF8.GetBytes(strData);
                    break;
                default:
                    System.Text.ASCIIEncoding ByteConverter = new ASCIIEncoding();
                    byteData = ByteConverter.GetBytes(strData);
                    break;
            }
            return byteData;
        }

        /// <summary>
        /// 获取字符值， 将字节型转换成字符型【byteData：原字节；TypeId编码类型1：Base64编码， 2：UTF8编码， 3：ASCII编码】
        /// </summary>
        public static string CodingToString(byte[] byteData, int TypeId)
        {
            string stringData;
            switch (TypeId)
            {
                case 1:
                    stringData = System.Convert.ToBase64String(byteData);
                    break;
                case 2:
                    stringData = System.Text.Encoding.UTF8.GetString(byteData);
                    break;
                default:
                    System.Text.ASCIIEncoding ByteConverter = new ASCIIEncoding();
                    stringData = ByteConverter.GetString(byteData);
                    break;
            }
            return stringData;
        }
    }
    
}

