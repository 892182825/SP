using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using DAL;

/*
 * 修改者：汪华
 * 修改时间：2009-09-07
 */

namespace BLL.other.Company
{
    public class QueryStorageBLL
    {
        ProductQuantityDAL productQuantityDAL = new ProductQuantityDAL();

        /// <summary>
        /// 分页(产品库存)
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="condition"></param>
        /// <param name="key"></param>
        /// <param name="RecordCount"></param>
        /// <param name="PageCount"></param>
        /// <returns></returns>
        public DataTable GetDataPage(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
            return productQuantityDAL.GetDataTablePage(PageIndex, PageSize, table, columns, condition, key, out RecordCount, out PageCount);
        }
        public static DataTable getStorage(string sql)
        {
            return ProductQuantityDAL.getStorage(sql);
        }
        public static string GetWarehouseName(string warehouseId)
        {
            return ProductQuantityDAL.GetWarehouseName(warehouseId);
        }
    }
}
