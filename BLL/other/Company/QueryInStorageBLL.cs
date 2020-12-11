using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data.Sql;
using System.Data;

/*
 * 修改者：汪华
 * 修改时间：2009-09-07
 */

namespace BLL.other.Company
{
    /// <summary>
    /// 入库查询
    /// </summary>
    public class QueryInStorageBLL
    {
        InventoryDocDAL inventoryDocDAL = new InventoryDocDAL();
        DocTypeTableDAL docTypeTableDAL = new DocTypeTableDAL();

        /// <summary>
        /// 入库查询
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
        public DataTable InStoreOrder(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
            return inventoryDocDAL.InStoreOrder(PageIndex, PageSize, table, columns, condition, key, out RecordCount, out PageCount);
        }
        /// <summary>
        /// 根据单据名称获得单据的ID
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static int GetDocTypeIDByDocTypeName(string typeName)
        {
            return DocTypeTableDAL.GetDocTypeIDByDocTypeName(typeName);
        }

        /// <summary>
        /// 根据单据名称获得单据的ID
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static int GetDocTypeIDEByDocTypeCode(string typeCode)
        {
            return DocTypeTableDAL.GetDocTypeIDEByDocTypeCode(typeCode);
        }
    }
}
