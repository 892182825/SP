using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 创建者：孙延昊
 * 创建时间：2009年8月27日 AM 10：07
 * 文件名：ChineseControlModel
 * 功能：中文对照翻译
 * **/
namespace Model
{
    /// <summary>
    /// 中文对照翻译
    /// </summary>
    public class ChineseControlModel
    {
        public ChineseControlModel()
        { }

        public ChineseControlModel(int id)
        {
            this.iD = id;
        }
        private int iD;

        public int ID
        {
            get { return iD; }
        }
        private string fileName;
        /// <summary>
        /// 文件路径及名称
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        private string location;
        /// <summary>
        /// 所在行数
        /// </summary>
        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        private string text;
        /// <summary>
        /// 内容
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

    }
}
