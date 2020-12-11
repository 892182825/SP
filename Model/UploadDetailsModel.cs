using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/**
 *  创建人：  郑华超
 *  创建时间：2009.8.27
 *  文件名：UploadDetailsModel.cs
 *  功能：上传记录明细模型
 */
namespace Model
{
    /// <summary>
    /// 表4.1—69上传记录明细表
    /// </summary>
    public class UploadDetailsModel
    {
        private int iD;
        /// <summary>
        /// //编号id
        /// </summary>
        public int ID
        {
            get { return iD; }
            
        }
        private string batch;

        /// <summary>
        /// //批次Batch
        /// </summary>
        public string Batch
        {
            get { return batch; }
            set { batch = value; }
        }
        private decimal uploadData;

        /// <summary>
        /// //上传的数据UploadData
        /// </summary>
        public decimal UploadData   
        {
            get { return uploadData; }
            set { uploadData = value; }
        }

        public UploadDetailsModel() { }


        public UploadDetailsModel(int id) 
        {
            this.iD = id;
        }
    }
}
