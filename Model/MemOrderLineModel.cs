using System;
using System.Collections.Generic;
using System.Text;

/*
 *
 *创建时间：09/8/27
 *文件名：CityModel.cs
 *功能：会员报单底线表
 */

namespace Model
{
    /// <summary>
    /// 会员报单底线表
    /// </summary>
    public class MemOrderLineModel
    {
        public MemOrderLineModel()
        {

        }

        public MemOrderLineModel(int id)
        {
            this.id = id;
        }

        private int id;
        private string number;
        private decimal orderBaseLine;

        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// 会员编号
        /// </summary>
        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }

        /// <summary>
        /// 会员报单底线
        /// </summary>
        public decimal OrderBaseLine
        {
            get
            {
                return orderBaseLine;
            }
            set
            {
                orderBaseLine = value;
            }
        }
    }
}
