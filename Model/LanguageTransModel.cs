using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 创建者：孙延昊
 * 创建时间：2009年8月27日 AM 10：07
 * 修改者：汪华
 * 修改时间：2009-09-07
 * 文件名：LanguageTransModel
 * 功能：产品名称等翻译模型
 * **/
namespace Model
{
    /// <summary>
    /// 产品名称等翻译
    /// </summary>
    public class LanguageTransModel
    {

        public LanguageTransModel()
        { }
        public LanguageTransModel(int id)
        {
            this.iD = id;
        }

        private int iD;

        public int ID
        {
            get { return iD; }
        }
        private string tableName;
        /// <summary>
        /// 原表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        private int oldID;
        /// <summary>
        /// 原表中的ID
        /// </summary>
        public int OldID
        {
            get { return oldID; }
            set { oldID = value; }
        }
        private string columnsName;
        /// <summary>
        /// 原表中的字段名称
        /// </summary>
        public string ColumnsName
        {
            get { return columnsName; }
            set { columnsName = value; }
        }
        private string languageName;
        /// <summary>
        /// 翻译结果
        /// </summary>
        public string LanguageName
        {
            get { return languageName; }
            set { languageName = value; }
        }
        private int languageID;
        /// <summary>
        /// 翻译的语言ID
        /// </summary>
        public int LanguageID
        {
            get { return languageID; }
            set { languageID = value; }
        }

    }
}
