using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Encryption
{
    public class NumberByBit
    {
        //public static string GetEncryptstring(string oldstr)
        //{

        //    //string   newstr, r,key="XMLLYF";
        //    //int i, j;

        //    //for (i = 0; i < oldstr.Length; i++)
        //    //{
        //    //    oldstr.Insert(i,oldstr[i]+key);
        //    //    //oldstr.SetAt(i, oldstr.get  s.GetAt(i) + k);
        //    //    //依次提取原数据的每一个字符，加密钥后写入新的字符串 
        //    //}
        //    //oldstr  = newstr; //更新原字符串 
        //    //for (i = 0; i < s.GetLength(); i++)
        //    //{
        //    //    j = (BYTE)s.GetAt(i);
        //    //    newstr = "01";
        //    //    newstr.SetAt(0, 65 + j / 26);
        //    //    newstr.SetAt(1, 65 + j % 26); //依次提取更新后的字符串中的每一个字符，用一种算法将一个字符分解成两个字符 
        //    //    r += str; //合成最终字符串 
        //    //}
        //    //return r; //返回这个字符串 
        //}


        //默认密钥向量
        private byte[] Keys = { 0xEF, 0xAB, 0x56, 0x78, 0x90, 0x34, 0xCD, 0x12 };
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        /// <summary>
        /// 将十六进制串转换为指定编码的字符串
        /// </summary>
        /// <param name="hex">十六进制串</param>
        /// <param name="encode">要转换成的字符串的编码</param>
        /// <returns>指定编码的字符串</returns>
        public static string ConvertStringFromHex(string hex, Encoding encode)
        {
            string src = string.Empty;
            int len = hex.Length / 2;
            byte[] buffer = new byte[len];
            for (int i = 0; i < len; i++)
            {
                buffer[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            src = encode.GetString(buffer);
            return src;
        }


    }
}
