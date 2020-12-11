using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Other
{
    /// <summary>
    /// 字符串处理类
    /// </summary>
    public class DisposeString
    {
        /// <summary>
        /// 处理字符串  --ds2012--www-b874dce8700——tianfeng
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="former">要替换的字符串</param>
        /// <param name="rep">指定替换字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string DisString(string str, string former, string rep)
        {
            return str.Replace(former,rep);
        }
        /// <summary>
        /// 去空格 --ds2012--www-b874dce8700——tianfeng
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string DisString(string str)
        {
            return DisString(str," ","");
        }

       /// <summary>
       /// 处理字符串
       /// </summary>
       /// <param name="str">要处理的字符串</param>
       /// <param name="former">要替换的字符串组</param>
       /// <param name="rep">制定替换的字符串组</param>
       /// <param name="partition">制定分割字符串组的字符</param>
       /// <returns>处理后的字符串</returns>
        public static string DisString(string str, string former, string rep, string partition)
        {
            string[] formers = former.Split(partition.ToCharArray()); //要处理的字符组
            string[] reps = rep.Split(partition.ToCharArray());       //替换为的字符串组
            for (int i = 0; i < formers.Length; i++)
            {
                str = DisString(str, formers[i], reps[i]);
            }
            return str;
        }

    }
}
