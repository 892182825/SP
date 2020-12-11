#region 版权信息
/*---------------------------------------------------------
 * copyright (C) 2009 shanghai qianchuang Tech. Co.,Ltd.
 *         上海乾创信息科技有限公司    版权所有
 * 文件名：UniteDocModel.cs
 * 文件功能描述：合单实体类
 *
 *
 * 创建标识：张朔 2009/09/10 
 * 
 * 修改标识：
 * 
 * 修改描述：
 * 
 * 
 * 
 * 
 * 
 //----------------------------------------- **/
#endregion
using System;
namespace Model
{
    /// <summary>
    /// 合单模型层
    /// </summary>
    public class UniteDocModel
    {
        private int iD = 0;
        /// <summary>
        /// 标示
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string uniteDocID = "";
        /// <summary>
        /// 合单编号
        /// </summary>
        public string UniteDocID
        {
            get { return uniteDocID; }
            set { uniteDocID = value; }
        }

        private string docID = "";
        /// <summary>
        /// 出库单ID集合
        /// </summary>
        public string DocID
        {
            get { return docID; }
            set { docID = value; }
        }

        private DateTime uniteDocTime;
        /// <summary>
        /// 单据开出时间
        /// </summary>
        public DateTime UniteDocTime
        {
            get { return uniteDocTime; }
            set { uniteDocTime = value; }
        }

        private decimal weight = 0;
        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        private double carriage = 0;
        /// <summary>
        /// 运费
        /// </summary>
        public double Carriage
        {
            get { return carriage; }
            set { carriage = value; }
        }

        private string remark = "";
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