#region 版权信息
/*---------------------------------------------------------
 * copyright (C) 2009 shanghai qianchuang Tech. Co.,Ltd.
 *         上海乾创信息科技有限公司    版权所有
 * 文件名：VIPCardRangeModel.cs
 * 文件功能描述：会员卡号实体类
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
    /// VIPCardRangeModel 的摘要说明
    /// </summary>
    public class VIPCardRangeModel
    {

        #region 公共属性

        private int iD;

        /// <summary>
        /// 自增长，标识列 (ID)
        /// </summary>
        public int ID
        {
            get { return iD; }
        }
        private int rangeID;

        /// <summary>
        /// 卡号范围编号(id)
        /// </summary>
        public int RangeID
        {
            get { return rangeID; }
            set { rangeID = value; }
        }
        private string storeID;

        /// <summary>
        /// 点编号(StoreID)
        /// </summary>
        public string StoreID
        {
            get { return storeID; }
            set { storeID = value; }
        }
        private int beginCard;
        /// <summary>
        /// 范围起始卡号	(BeginCard)
        /// </summary>
        public int BeginCard
        {
            get { return beginCard; }
            set { beginCard = value; }
        }
        private int endCard;
        /// <summary>
        /// 范围结束卡号(EndCard)
        /// </summary>
        public int EndCard
        {
            get { return endCard; }
            set { endCard = value; }
        }
        private string beginNum;
        /// <summary>
        /// 编号开始标号(beginbianhao)
        /// </summary>
        public string BeginNum
        {
            get { return beginNum; }
            set { beginNum = value; }
        }
        private string endNum;
        /// <summary>
        /// 编号结束标号(endbianhao)
        /// </summary>
        public string EndNum
        {
            get { return endNum; }
            set { endNum = value; }
        }
        private int inUse;
        /// <summary>
        /// 是否使用(inuse)
        /// </summary>
        public int InUse
        {
            get { return inUse; }
            set { inUse = value; }
        }

        #endregion
        #region 公共方法
        /// <summary>
        /// 
        /// </summary>
        public VIPCardRangeModel()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public VIPCardRangeModel(int id)
        {
            this.iD = id;
        }
        #endregion
    }
}