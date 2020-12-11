using System;
using System.Collections.Generic;
using System.Text;

/*
 * 创建者：  汪华
 * 创建时间：2009-10-08
 * 说明：    库位模型层
 */

namespace Model
{
    /// <summary>
    /// DepotSeatModel 的摘要说明
    /// </summary>
    public class DepotSeatModel
    {
        #region 公共属性
        private int iD;

        /// <summary>
        /// 自增长，标识列 (ID)
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private int wareHouseID;

        /// <summary>
        /// 仓库编号
        /// </summary>
        public int WareHouseID
        {
            get { return wareHouseID; }
            set { wareHouseID = value; }
        }

        private int depotSeatID;

        /// <summary>
        /// 库位编号
        /// </summary>
        public int DepotSeatID
        {
            get { return depotSeatID; }
            set { depotSeatID = value; }
        }
        
        private string seatName;

        /// <summary>
        ///库位名称(SeatName)
        /// </summary>
        public string SeatName
        {
            get { return seatName; }
            set { seatName = value; }
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

        #endregion
        #region 公共方法
        /// <summary>
        /// 
        /// </summary>
        public DepotSeatModel()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public DepotSeatModel(int id)
        {
            this.iD = id;
        }
        #endregion

    }
}