using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 创建者：  汪  华
 * 创建时间：2009-10-19
 * 功能：    翻译语言模型
 */

namespace Model
{
    public class TranToNModel
    {
        private int id;

        /// <summary>
        /// 标识
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string fileName;

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private string location;

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}
