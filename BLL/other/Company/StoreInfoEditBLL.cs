using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using DAL;

/**
 * 创建者;刘文
 * 创建时间：2009-8-27
 * 修改者：汪华
 * 修改时间：2009-09-07
 * 文件名：StoreInfoEditBLL
 * 功能：店铺信息编辑
 * **/
namespace BLL.other.Company
{
    public class StoreInfoEditBLL
    {
       // StoreInfoDAL storeInfoDAL = new StoreInfoDAL();

        /// <summary>
        /// 根据条件进行筛选查询店铺信息
        /// </summary>
        public DataTable GetStoreInfo(string Id, string Name, string StoreName, int ExpectNum, int pageIndex, int pagesize, out int pageCount, out int RecordCount)
        {
            return StoreInfoDAL.GetStoreInfo(Id, Name, StoreName, ExpectNum, pageIndex, pagesize, out pageCount, out  RecordCount);
        }
        /// <summary>
        /// 修改会员信息
        /// </summary>
        /// <returns></returns>
        public int updateStore(StoreInfoModel storeInfoMember)
        {
            return StoreInfoDAL.updateStoreInfo(storeInfoMember);
        }
        /// <summary>
        /// 删除店铺
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <returns></returns>
        public int DelStore(int ID)
        {
            return StoreInfoDAL.DelStore(ID);
        }
        /// <summary>
        /// 根据ID查询店铺详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static  StoreInfoModel GetStoreInfoById(int id)
        {
            return StoreInfoDAL.GetStoreInfoById(id);
        }
        public static StoreInfoModel GetStoreInfoByStoreId(string id)
        {
            return StoreInfoDAL.GetStoreInfoByStoreId(id);
        }
        public static int getMemberCount(string storeid)
        {
            return StoreInfoDAL.getMemberCount(storeid);
        }
        /// <summary>
        /// 获取国家 ---DS2012
        /// </summary>
        /// <returns></returns>
        public static DataTable bindCountry()
        {
            return StoreInfoDAL.bindCountry();
        }
        public static SqlDataReader bindCity(string country)
        {
            return StoreInfoDAL.bindCity(country);
        }

        /// <summary>
        /// 获取店铺级别名称
        /// </summary>
        /// <param name="LvNum"></param>
        /// <returns></returns>
        public static string GetStoreLvCH(int LvNum)
        {
            return StoreInfoDAL.GetStoreLvCH(LvNum);
        }
    }
}
