using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 *
 * 创建者：张朔--程凯修改2012-06-14
 * 创建时间：2009年8月27日 AM 10：07
 * 文件名：LevelModel
 * 功能：手工定级表
 */

namespace Model
{
    /// <summary>
    /// 手工定级表
    /// </summary>
    public class GradingModel
    {
        public GradingModel() { }
        public GradingModel(int id)
        {
            this.iD = id;
        }
        private int iD;
        /// <summary>
        /// 标示
        /// </summary>
        public int ID
        {
            get { return iD; }
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

        private int levelNum;
        /// <summary>
        /// 新级别（关联BSCO_Level表）
        /// </summary>
        public int LevelNum
        {
            get { return levelNum; }
            set { levelNum = value; }
        }
        private int ExpectNum;
        /// <summary>
        /// 期数
        /// </summary>
        public int ExpectNum1
        {
            get { return ExpectNum; }
            set { ExpectNum = value; }
        }
        private DateTime InputDate;
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime InputDate1
        {
            get { return InputDate; }
            set { InputDate = value; }
        }

        private int OldLN;
        /// <summary>
        /// 原级别
        /// </summary>
        public int OldLN1 {
            get { return OldLN; }
            set { OldLN = value; }
        }

        private string OperaterNum;
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperaterNum1 {
            get { return OperaterNum;}
            set { OperaterNum = value; }
        }

        private string OperateIP;
        /// <summary>
        /// 操作人IP
        /// </summary>
        public string OperateIP1 {
            get { return OperateIP; }
            set { OperateIP = value; }
        }

        private string Mark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Mark1 {
            get { return Mark; }
            set { Mark = value; }
        }
        /// <summary>
        /// 手动定级类别，0店铺，1会员
        /// </summary>
        public Grading GradingStatus
        {
            get;
            set;
        }

    }
    /// <summary>
    /// 手动定级类别，0店铺，1会员
    /// </summary>
    public enum Grading { 
        /// <summary>
        /// 店铺手工定级
        /// </summary>
        StoreLevel=0,
        /// <summary>
        /// 会员手工定级
        /// </summary>
        MemberLevel=1,
    }
}
