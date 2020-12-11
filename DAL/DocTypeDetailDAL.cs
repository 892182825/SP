using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    /// <summary>
    /// 单据类型表
    /// </summary>
	public class DocTypeDetailDAL
	{
        /// <summary>
        /// 查询所有单据类型详细
        /// </summary>
        /// <returns></returns>
        public IList<DocTypeDetailModel> GetDocTypeDetailAll()
        {
           DataTable table= DBHelper.ExecuteDataTable("GetDocTypeDetailAll", CommandType.StoredProcedure);
           IList<DocTypeDetailModel> list = new List<DocTypeDetailModel>();
           foreach (DataRow row in table.Rows)
           {
               DocTypeDetailModel doc = new DocTypeDetailModel();
               doc.DocTypeID = Convert.ToInt32(row["DocTypeID"]);
               doc.SubDocTypeName = row["SubDocTypeName"].ToString();
               doc.SubID = Convert.ToInt32(row["SubID"]);
               list.Add(doc);
           }
           return list;
        }
        /// <summary>
        /// 根据单据类型编号查询单据名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDocNameByDocTypeID(string id)
        {
           return DBHelper.ExecuteScalar("GetDocNameByDocTypeID", new SqlParameter("@id", id), CommandType.StoredProcedure).ToString();
        
        }

	}
}
