using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;
using Model;
/************************************************************************
 * 开发人：宋俊
 * 开发时间：2009-8-28
 * 
 * *********************************************************************/
namespace BLL.other.Company
{
    ///店铺信息查询
    public class StoreInfoSearchBLL
    {
        
        /// <summary>
        ///根据店铺编号查询店铺
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static StoreInfoModel GetStoreInfoById(int storeid)
        {
            //StoreInfoDAL storeInfoDAL = new StoreInfoDAL();
            return StoreInfoDAL.GetStoreInfoById(storeid);
        }
        /// <summary>
        ///根据店铺编号查询店铺
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static StoreInfoModel GetStoreInfoByStoreid(string storeid)
        {
            //StoreInfoDAL storeInfoDAL = new StoreInfoDAL();
            return StoreInfoDAL.GetStoreInfoByStoreId(storeid);
        }
        /// <summary>
        /// 根据条件进行筛选查询店铺信息
        /// </summary>
        /// <param name="Id">店铺编号</param>
        /// <param name="Name">店长名称</param>
        /// <param name="StoreName">店铺名称</param>
        /// <param name="ExpectNum">查询期数</param>
        /// <param name="pageIndex">当前页编号</param>
        /// <param name="pagesize">页面显示条数</param>
        /// <returns>符合条件的店铺信息</returns>
        public static DataTable GetStoreInfo(string Id, string Name, string StoreName, int ExpectNum,int pageIndex,int pagesize,out int pageCount,out int RecordCount)
        {
            return StoreInfoDAL.GetStoreInfo(Id, Name, StoreName, ExpectNum, pageIndex, pagesize, out pageCount, out RecordCount);
        }
                /// <summary>
        /// 根据storeid查询会员编号——ds2012——tianfeng
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns>会员编号</returns>
        public static string GetNumberByStoreId(string storeid)
        {
            return StoreInfoDAL.GetNumberByStoreId(storeid);
        }
    }
}
