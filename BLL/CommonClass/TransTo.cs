using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
namespace BLL.CommonClass
{
    /// <summary>
    /// 翻译
    /// </summary>
    public class TransTo
    {
        public static string getTranToName(string lanuage, string fileName, string control)
        {
            
           return  TranToNDAL.getTransName("TranTo"+lanuage,fileName+"/"+control);
        }
        /// <summary>
        /// 组合文件名字+控件名
        /// </summary>
        /// <param name="fileName">eg:company/a.aspx</param>
        /// <param name="control">lblname</param>
        /// <returns></returns>
        public string GetTableControl(string fileName,string control)
        {
            return fileName + "/" + control;
        }
        /// <summary>
        /// 得到表名
        /// </summary>
        /// <param name="lanuage"></param>
        /// <returns></returns>
        public string GetTableName(string lanuage)
        {
            return "TranTo" + lanuage;
        }
    }
}
