using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Other
{
    public class MYDateTime
    {
        public MYDateTime() { }

        public static string ToYYYYMMDDString(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 返回形如:050108
        /// </summary>
        public static string ToYYMMDDString(DateTime dt)
        {
            return dt.ToString("yyMMdd");
        }

        /// <summary>
        /// 返回当前日期的字符：形如：050113121212
        /// </summary>
        public static string ToYYMMDDString()
        {
            return DateTime.Now.ToString("yyMMdd");
        }

        /// <summary>
        /// 返回当前日期的字符：形如：050113
        /// </summary>
        public static string ToYYMMDDHHmmssString()
        {
            return DateTime.Now.ToString("yyMMddHHmmss");
        }


        /// <summary>
        /// 返回当前日期的字符：形如：20050113
        /// </summary>
        public static string ToYYYYMMDDString()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }



        public static DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
