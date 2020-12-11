using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Model.Other
{
    public class Common
    {
        #region 截取一个指定长度的字符串
        /// <summary>
        /// 截取一个指定长度的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetCutString(string str, int length)
        {
            if (str.Length<length)
            {
                return str;
            }
            else
            {
                return str.Substring(0, length);
            }
        }
        #endregion
        #region 截取一个指定长度的字符串，返回的字符串后面的部分用"……"代替
        /// <summary>
        /// 截取一个指定长度的字符串，返回的字符串后面的部分用"……"代替
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetCutStr(string str, int length)
        {
            if (str.Length < length)
            {
                return str;
            }
            else
            {
                return str.Substring(0, length)+"……";
            }
        }
        #endregion
        #region  产生指点长度字符串的方法
        /// <summary>
        /// 产生指点长度字符串的方法
        /// </summary>
        /// <param name="lengths"></param>
        /// <returns></returns>
        public static string GetReadomStr(int lengths)
        {
            char[] ch = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            Random read = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lengths; i++)
            {
                sb.Append(ch[read.Next(62)].ToString());
            }
            return sb.ToString();
        }
        #endregion
    }
}
