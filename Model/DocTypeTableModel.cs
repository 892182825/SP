using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 创建者：  汪  华
 * 时间：    2009-8-27
 *          DocTypeTableModel
 * 功能：    单据类型（冲红）表
 */

namespace Model
{
    /// <summary>
    /// 单据类型（冲红）表
    /// </summary>
    public class DocTypeTableModel
    {
        public DocTypeTableModel() { }

        private int docTypeID;
        /// <summary>
        /// 单据编号
        /// </summary>
        public int DocTypeID
        {
            get { return docTypeID; }
            set { docTypeID = value; }
        }
        private string docTypeName;
        /// <summary>
        /// 单据名称
        /// </summary>
        public string DocTypeName
        {
            get { return docTypeName; }
            set { docTypeName = value; }
        }
        private int isRubric;
        /// <summary>
        /// 冲红
        /// </summary>
        public int IsRubric
        {
            get { return isRubric; }
            set { isRubric = value; }
        }
        private string docTypeDescr;
        /// <summary>
        /// 描述
        /// </summary>
        public string DocTypeDescr
        {
            get { return docTypeDescr; }
            set { docTypeDescr = value; }
        }

    }
}
