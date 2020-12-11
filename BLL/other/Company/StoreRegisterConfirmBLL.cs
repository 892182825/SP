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
    /// <summary>
    /// 店铺注册确认
    /// </summary>
    public class StoreRegisterConfirmBLL
    {
        /// <summary>
        /// 确认注册店铺
        /// </summary>
        /// <param name="Store">店铺注册信息</param>
        /// <returns>返回是否注册成功</returns>
        public static bool StoreRegisterRest(string  Storeid)
        {
            //StoreInfoDAL store=new StoreInfoDAL ();
            return StoreInfoDAL.AddStoreInfo(Storeid);
        }
         /// <summary>
        /// 检查店铺编号是否存在
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static bool CheckStoreId(string storeid)
        {
           // StoreInfoDAL dao = new StoreInfoDAL();
            return StoreInfoDAL.CheckStoreId(storeid);
        }
        /// <summary>
        ///根据店铺编号或者店铺名称在指定的那个的期数查询要审核的店铺
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <param name="storename">店铺名称</param>
        /// <param name="ExpectNum">期数</param>
        /// <returns>未审核的店铺</returns>
        public static DataTable GetAllUnauditedStoreInfo(int pageindex,int pagesize,out int RecordCount,out int pageCount,string storeid, string storename, int ExpectNum)
        {
           // UnauditedStoreInfoDAL un = new UnauditedStoreInfoDAL();
            return UnauditedStoreInfoDAL.GetAll(storeid, storename, ExpectNum, pageindex, pagesize, out RecordCount, out pageCount);
        }
    }
}
