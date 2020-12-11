using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/**
 *  创建人：  郑华超
 *  创建时间：2009.8.27
 *  文件名：BackupDatabaseModel.cs
 *  功能：数据库备份模型 
 */
namespace Model
{
    /// <summary>
    /// 表4.1—65数据库备份路径表
    /// </summary>
    public class BackupDatabaseModel
    {
        private int dataBackupID;
        /// <summary>
        /// 标识
        /// </summary>
        public int DataBackupID
        {
            get { return dataBackupID; }
            set { dataBackupID = value;}
        }
        private DateTime dataBackupTime;

        /// <summary>
        /// 备份时间
        /// </summary>
        public DateTime DataBackupTime
        {
            get { return dataBackupTime; }
            set { dataBackupTime = value; }
        }

        private string operatorNum;

        /// <summary>
        /// 操格者编号
        /// </summary>
        public string OperatorNum
        {
            get { return operatorNum; }
            set { operatorNum = value; }
        }
        private string pathFileName;

        /// <summary>
        /// 路径及文件名
        /// </summary>
        public string PathFileName
        {
            get { return pathFileName; }
            set { pathFileName = value; }
        }

        private string remark;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public BackupDatabaseModel() { }

        public BackupDatabaseModel(int dataBinkId) 
        {
            this.dataBackupID = dataBinkId;
        }
    }
}
