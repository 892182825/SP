using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 创建者：孙延昊
 * 创建时间：2009年8月27日 AM 10：07
 * 文件名：LanguageModel
 * 功能：语言表模型
 * **/
namespace Model
{
    /// <summary>
    /// 语言表
    /// </summary>
    public class LanguageModel
    {
        public LanguageModel()
        { }
        public LanguageModel(int id)
        {
            this.iD = id;
        }
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private string name;
        /// <summary>
        /// 语言名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string tableName;
        /// <summary>
        /// 对应的语言表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

    }
}
