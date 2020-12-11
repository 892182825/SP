using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 创建者：孙延昊
 * 创建时间：2009年8月27日 AM 10：07
 * 文件名：NetAdjustmentHistoryModel
 * 功能：会员调网临时表模型
 * **/
namespace Model
{
    /// <summary>
    /// 会员调网临时表
    /// </summary>
    public class NetAdjustmentHistoryModel
    {

        public NetAdjustmentHistoryModel()
        { }
        public NetAdjustmentHistoryModel(int id)
        {
            this.iD = id;
        }

        private int iD;
        private int type;
        /// <summary>
        /// 0和10为推荐调网，1和11为安置调网
        /// </summary>
        public int Type
        {
            get { return type; }
            set { type = value; }
        }
        private string number;
        /// <summary>
        /// 会员编号
        /// </summary>
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string original;
        /// <summary>
        /// 原上级编号
        /// </summary>
        public string Original
        {
            get { return original; }
            set { original = value; }
        }
        private string newLocation;
        /// <summary>
        /// 新上级编号
        /// </summary>
        public string NewLocation
        {
            get { return newLocation; }
            set { newLocation = value; }
        }
        private int expectNum;
        /// <summary>
        /// 调网期数
        /// </summary>
        public int ExpectNum
        {
            get { return expectNum; }
            set { expectNum = value; }
        }
        private string error;
        /// <summary>
        /// 调网结果
        /// </summary>
        public string Error
        {
            get { return error; }
            set { error = value; }
        }

    }
}
