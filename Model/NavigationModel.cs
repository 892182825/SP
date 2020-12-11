#region 版权信息
/*---------------------------------------------------------
 * copyright (C) 2009 shanghai qianchuang Tech. Co.,Ltd.
 *         上海乾创信息科技有限公司    版权所有
 * 文件名：NavigationModel.cs
 * 文件功能描述：产品定位实体类
 *
 *
 * 创建标识：董晨东 2009/08/26
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
    /// navigationModel 的摘要说明
    /// </summary>
    public class NavigationModel
    {

        #region 公共属性
        private int iD;

        /// <summary>
        /// 自增长，标识列 (ID)
        /// </summary>
        protected int ID
        {
            get { return iD; }
        }

        private int depotSeatId;
        /// <summary>
        /// 库位ID (DepotSeatId)
        /// </summary>
        protected int DepotSeatId
        {
            get { return depotSeatId; }
            set { depotSeatId = value; }
        }
        private int productId;
        /// <summary>
        /// 产品ID(ProductId)
        /// </summary>
        protected int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
        private int batchId;
        /// <summary>
        /// 批次ID (BatchId)
        /// </summary>
        protected int BatchId
        {
            get { return batchId; }
            set { batchId = value; }
        }
        private int count;
        /// <summary>
        /// 数量(Count)
        /// </summary>
        protected int Count
        {
            get { return count; }
            set { count = value; }
        }


        #endregion
        #region 公共方法
        /// <summary>
        /// 
        /// </summary>
        public NavigationModel()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public NavigationModel(int id)
        {
            this.iD = id;
        }
        #endregion

    }
}