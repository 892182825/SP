using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;

namespace BLL.other.Company
{
    public class DocTypeDetailBLL
    {
         /// <summary>
        /// 根据单据类型编号查询单据名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDocNameByDocTypeID(string id)
        {
            DocTypeDetailDAL dao=new DocTypeDetailDAL ();
            return dao.GetDocNameByDocTypeID(id);
        }
        /// <summary>
        /// 查询所有单据类型
        /// </summary>
        /// <returns></returns>
        public IList<DocTypeDetailModel> GetDocTypeDetailAll()
        {
            DocTypeDetailDAL dao = new DocTypeDetailDAL();
            IList<DocTypeDetailModel> list = dao.GetDocTypeDetailAll();
            return list;
        }


    }
}
