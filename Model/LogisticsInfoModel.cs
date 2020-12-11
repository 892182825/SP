using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * 创建者: 张朔
 * 创建时间: 2009-8-27
 * 文件名：LogisticsInfoModel
 * 功能：物流公司信息表
 */

namespace Model
{
    /// <summary>
    /// 物流公司信息表
    /// </summary>
    public class LogisticsInfoModel
    {
        public LogisticsInfoModel(){}
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">标示</param>
        public LogisticsInfoModel(int id)
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
        /// 与Logistics（物流表）中供应商编号
        /// </summary>
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private int logisticsID;
        /// <summary>
        /// 与Logistics（物流表）中ID关联
        /// </summary>
        public int LogisticsID
        {
            get { return logisticsID; }
            set { logisticsID = value; }
        }
        private int maxWeight;
        /// <summary>
        /// 最大重量区间
        /// </summary>
        public int MaxWeight
        {
            get { return maxWeight; }
            set { maxWeight = value; }
        }
        private int minWeight;
        /// <summary>
        /// 最小重量区间
        /// </summary>
        public int MinWeight
        {
            get { return minWeight; }
            set { minWeight = value; }
        }
        private string beginPlace;
        /// <summary>
        /// 始发地
        /// </summary>
        public string BeginPlace
        {
            get { return beginPlace; }
            set { beginPlace = value; }
        }
        private string endPlace;
        /// <summary>
        /// 到达地
        /// </summary>
        public string EndPlace
        {
            get { return endPlace; }
            set { endPlace = value; }
        }
        private int hour;
        /// <summary>
        /// 所需时间（小时）
        /// </summary>
        public int Hour
        {
            get { return hour; }
            set { hour = value; }
        }
        private string form;
        /// <summary>
        /// 运输方式
        /// </summary>
        public string Form
        {
            get { return form; }
            set { form = value; }
        }
        private decimal freight;
        /// <summary>
        /// 运费
        /// </summary>
        public decimal Freight
        {
            get { return freight; }
            set { freight = value; }
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

    }
}
