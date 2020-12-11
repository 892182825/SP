using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 *创建者：  汪华
 *创建日期：2009-08-28
 *文件名：  ChongHongModel
 *功能：    冲红表模型
 * 
 */

namespace Model
{
    public class ChongHongModel
    {
        public ChongHongModel()
        { }

        public ChongHongModel(int id)
        {
            this.iD = id;           
        }

        private int iD;

        /// <summary>
        /// 标识
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string number;

        /// <summary>
        /// 编号
        /// </summary>
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        private int expectNum;

        /// <summary>
        /// 期数
        /// </summary>
        public int ExpectNum
        {
            get { return expectNum; }
            set { expectNum = value; }
        }

        private double moneyNum;

        /// <summary>
        /// 金额
        /// </summary>
        public double MoneyNum
        {
            get { return moneyNum; }
            set { moneyNum = value; }
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

        private int isDelete;

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDelete
        {
            get { return isDelete; }
            set { isDelete = value; }
        }

        private DateTime startDate;

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private DateTime deleteDate;

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime DeleteDate
        {
            get { return deleteDate; }
            set { deleteDate = value; }
        }


    }
}
