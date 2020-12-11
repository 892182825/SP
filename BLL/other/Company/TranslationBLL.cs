using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using Model;
using DAL;
using System.Data;

/*
 * 创建者：  汪  华
 * 创建时间：2009-10-19
 */

namespace BLL.other.Company
{
    public class TranslationBLL
    {
        /// <summary>
        /// 更改指定表的文本
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="tranToModel">翻译语言模型</param>
        /// <returns>返回更改指定表的文本所影响的行数</returns>
        public static int UpdTranToN(string tableName, TranToNModel tranToModel)
        {
            return TranToNDAL.UpdTranToN(tableName, tranToModel);
        }

        /// <summary>
        /// 更新指定ID的翻译结果
        /// </summary>
        /// <param name="languageName">翻译结果</param>
        /// <param name="id">标识ID</param>
        /// <returns>返回更新指定ID的翻译结果所影响的行数</returns>
        public static int UplLanguageTransLanguageNameByID(string languageName, int id)
        {
            return LanguageTransDAL.UplLanguageTransLanguageNameByID(languageName, id);
           
        }

        /// <summary>
        /// 获取语言信息
        /// </summary>
        /// <returns>返回Ilist对象</returns>
        public static IList<LanguageModel> GetLanguageInfoOrderByName()
        {
            return LanguageDAL.GetLanguageInfoOrderByName();
        }

        /// <summary>
        /// 获取不包含中文语言信息
        /// </summary>
        /// <returns>返回Ilist对象</returns>
        public static IList<LanguageModel> GetLanguageInfoNoCludingChineseOrderByName()
        {
            return LanguageDAL.GetLanguageInfoNoCludingChineseOrderByName();
        }

        /// <summary>
        /// 获取语言翻译信息
        /// </summary>
        /// <param name="columnNames">列名</param>
        /// <param name="tableName">表名</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="key">排序关键字</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetTranToNInfoByCondition(string columnNames, string tableName, string conditions, string key)
        {
            return TranToNDAL.GetTranToNInfoByCondition(columnNames, tableName, conditions, key);
        }
    }
}
