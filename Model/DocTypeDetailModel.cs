using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 *创建者：张朔
 *创建时间:2009.8.27
 *修改者：汪华
 *修改时间：2009-09-09
 * DocTypeDetailModel
 * 功能：单据类型表
 */

namespace Model
{
    /// <summary>
    /// 单据类型表
    /// </summary>
    public class DocTypeDetailModel
    {
        public DocTypeDetailModel() { }

        private int subID;
        /// <summary>
        /// 标记
        /// </summary>
        public int SubID
        {
            get { return subID; }
            set { subID = value; }
        }
        private int docTypeID;
        /// <summary>
        /// 单据编号
        /// </summary>
        public int DocTypeID
        {
            get { return docTypeID; }
            set { docTypeID = value; }
        }
        private string subDocTypeName;
        /// <summary>
        /// 单据名称
        /// </summary>
        public string SubDocTypeName
        {
            get { return subDocTypeName; }
            set { subDocTypeName = value; }
        }

    }
}
