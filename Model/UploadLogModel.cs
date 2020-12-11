using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/**
 *  创建人：  郑华超
 *  创建时间：2009.8.27
 *  文件名：UploadLogModel.cs
 *  功能：上传记录模型 
 */
namespace Model
{
    /// <summary>
    /// 表4.1—68上传记录表
    /// </summary>
    public class UploadLogModel
    {
        private int number;
        /// <summary>
        /// //编号bigint
        /// </summary>
        public int Number
        {
            get { return number; }
        }

        private string batch;
        /// <summary>
        /// //批数Batch
        /// </summary>
        public string Batch
        {
            get { return batch; }
            set { batch = value; }
        }

        private int expectNum;

        /// <summary>
        /// //期数expectNum
        /// </summary>
        public int ExpectNum
        {
            get { return expectNum; }
            set { expectNum = value; }
        }

        private string operatorNum;
        /// <summary>
        /// //操作员Operator
        /// </summary>
        public string OperatorNum
        {
            get { return operatorNum; }
            set { operatorNum = value; }
        }

        private string ipAddress;
        /// <summary>
        /// //IP地址IpAddress
        /// </summary>
        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        private string batchBank;
        /// <summary>
        /// //匹配银行 BatchBank
        /// </summary>
        public string BatchBank
        {
            get { return batchBank; }
            set { batchBank = value; }
        }

        private DateTime uploadDate;
        /// <summary>
        /// // 上传日期UploadDate
        /// </summary>
        public DateTime UploadDate
        {
            get { return uploadDate; }
            set { uploadDate = value; }
        }

        private int state;
        /// <summary>
        /// //状态State
        /// </summary>
        public int State
        {
            get { return state; }
            set { state = value; }
        }

        private int matchSuccess;
        /// <summary>
        /// //匹配成功的个数MatchSuccess
        /// </summary>
        public int MatchSuccess
        {
            get { return matchSuccess; }
            set { matchSuccess = value; }
        }

        private int noMatch;
        /// <summary>
        /// //未匹配成功的个数NoMatch
        /// </summary>
        public int NoMatch
        {
            get { return noMatch; }
            set { noMatch = value; }
        }

        public UploadLogModel() { }
        public UploadLogModel(int number) 
        {
            this.number = number;
        }
    }
}