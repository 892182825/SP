using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 创建者：张朔
 * 时间：2009-8-30
 * 功能：辅助分页功能
 */

namespace Model.Other
{
    /// <summary>
    /// 分页帮助类
    /// </summary>
    [Serializable]
    public class PaginationModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="rowCount">页面数量</param>
        public PaginationModel(int pageIndex,int rowCount)
        {
            this.pageIndex = pageIndex;
            this.RowCount = rowCount;
            this.PageCount = 0;
        }

        ///// <summary>
        ///// 创建分页类型
        ///// </summary>
        ///// <param name="dataCount">总数据量</param>
        ///// <param name="rowCount">每页显示多少</param>
        //public PaginationModel(int dataCount, int rowCount)
        //{
        //    ComputePageCount(dataCount,rowCount);
        //}

        /// <summary>
        /// 创建分页类构造函数
        /// </summary>
        /// <param name="dataCount">总数据量</param>
        /// <param name="rowCount">每页显示多少</param>
        /// <param name="pageIndex">当前页数(页的索引 第一页为1)</param>
        public PaginationModel(int dataCount, int rowCount, int pageIndex)
        {
            DisPageCount(dataCount,rowCount,pageIndex);
        }

        /// <summary>
        /// 计算总页数
        /// </summary>
        /// <param name="dataCount"></param>
        /// <param name="rowCount"></param>
        public void DisPageCount(int dataCount, int rowCount)
        {
            ComputePageCount(dataCount, rowCount);
            this.PageIndex = 1;
        }

      
       /// <summary>
       /// 计算总页数
       /// </summary>
       /// <param name="dataCount"></param>
       /// <param name="rowCount"></param>
       /// <param name="pageIndex"></param>
        public void DisPageCount(int dataCount, int rowCount, int pageIndex)
        {
            ComputePageCount(dataCount, rowCount);
            this.PageIndex = pageIndex;
        }

        /// <summary>
        /// 分页算法
        /// </summary>
        /// <param name="dataCount"></param>
        /// <param name="rowCount"></param>
        private void ComputePageCount(int dataCount, int rowCount)
        {
            this.RowCount = rowCount;
            this.DataCount = dataCount;
            // PageCount = (rowCount + 10 - 1) / 10;
            this.PageCount = (DataCount + RowCount - 1) / RowCount;
        }

        /// <summary>
        /// 每页的行数
        /// </summary>
        public int RowCount { set; get; }

        private int pageIndex = 0;
        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex
        {
            set
            {
                //如果>总页数那么取总页数为当前页数
                if (value > PageCount)
                {
                    pageIndex = PageCount;
                }
                else
                {
                    pageIndex = value;
                }
            }
            get { return pageIndex; }
        }

        /// <summary>
        /// 总数据量
        /// </summary>
        public int DataCount { set; get; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { set; get; }

        /// <summary>
        /// 下一步按钮状态
        /// </summary>
        /// <returns></returns>
        public Boolean nextButton()
        {
            if (PageIndex == PageCount || PageCount == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 上一步按钮状态
        /// </summary>
        /// <returns></returns>
        public Boolean backButton()
        {
            if (PageIndex == 1)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 返回数据行的头行数
        /// </summary>
        /// <returns></returns>
        public int GetPageDate()
        {
            return (this.PageIndex - 1) * RowCount + 1;
        }
        /// <summary>
        /// 返回数据行的头行数
        /// </summary>
        /// <param name="pageIndex">用户需要的页数</param>
        /// <returns></returns>
        public int GetPageDate(int pageIndex)
        {
            this.pageIndex = pageIndex;
            return GetPageDate();
        }

        /// <summary>
        /// 返回数据行的尾数据
        /// </summary>
        /// <returns></returns>
        public int GetEndDate()
        {
            return PageIndex * RowCount;
        }

        /// <summary>
        /// 返回数据行的尾数据
        /// </summary>
        /// <param name="pageIndex">用户需要的页数</param>
        /// <returns></returns>
        public int GetEndDate(int pageIndex)
        {
            this.pageIndex = pageIndex;
            return GetEndDate();
        }
    }
}
