using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespce
using System.Data;
using System.Data.SqlClient;
using Model;
using DAL;
/************************************************************************
 * 开发人：宋俊
 * 开发时间：2009-8-28
 * 
 * *********************************************************************/
namespace BLL.other.Company
{
    /// <summary>
    /// 店铺密码重置
    /// </summary>
    public class StorePassResetBLL
    {
        StoreInfoDAL storeInfoDAL = new StoreInfoDAL();
        /// <summary>
        /// 根据店铺id重置店铺密码
        /// </summary>
        /// <param name="storeid">店铺id</param>
        /// <returns>执行是否成功</returns>
        public bool StorePassReset(int storeid)
        {
            return false;
        }
        /// <summary>
        /// 根据店铺编号或者店铺名称查询符合条件的店铺
        /// </summary>
        /// <param name="Id">店铺编号</param>
        /// <param name="Name">店长名称</param>
        /// <param name="StoreName">店铺名称</param>
        /// <param name="ExpectNum">查询期数</param>
        /// <param name="pageIndex">当前页编号</param>
        /// <param name="pagesize">页面显示条数</param>
        /// <returns>符合条件的店铺信息</returns>
        //public static DataTable GetStoreInfo(string Id, string Name, string StoreName, int ExpectNum,int pageIndex,int pagesize,out int pageCount,out int RecordCount)
        //{
        //    return GetStoreInfo(Id, Name, StoreName, ExpectNum, pageIndex, pagesize, out pageCount, out RecordCount);
        //}
    }
}
