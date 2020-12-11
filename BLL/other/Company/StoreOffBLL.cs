using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using DAL;
using Model;

namespace BLL.other.Company
{
    public class StoreOffBLL
    {
        /// <summary>
        /// --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static int getStoreZX(string storeid)
        {
            return StoreOffDAL.getStoreZX(storeid);
        }

        public static int getStoreISzx(string storeid)
        {
            return StoreOffDAL.getStoreISzx(storeid);
        }
        /// <summary>
        /// --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="number"></param>
        /// <param name="qishu"></param>
        /// <param name="zxname"></param>
        /// <param name="zxdate"></param>
        /// <returns></returns>
        public static int getInsertStoreZX(StoreOffModel st)
        {
            return StoreOffDAL.getInsertStoreZX(st);
        }

        /// <summary>
        /// --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="storeid"></param>
        /// <param name="qishu"></param>
        /// <param name="id"></param>
        /// <param name="zxdate"></param>
        /// <returns></returns>
        public static int getUpdateStoreZX(string storeid, int qishu, int id, DateTime zxdate, string Operator, string OperatorName)
        {
            return StoreOffDAL.getUpdateStoreZX(storeid, qishu, id, zxdate, Operator, OperatorName);
        }
    }
}
