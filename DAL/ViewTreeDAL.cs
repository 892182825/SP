using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Data;

/**
 * 作者：zhc
 * 日期：2009.9.15
 * 功能：与树形图有关的SQL语句和存储过程
 */
namespace DAL
{
    public class ViewTreeDAL
    {
        /// <summary>
        /// 调用伸缩网络图的存储过程
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="tree"></param>
        /// <param name="state">是否安置状态</param>
        /// <param name="leyerNumber">层数</param>
        /// <param name="excempt">期数</param>
        /// <param name="storeid">店id</param>
        public DataTable GetExtendTreeView(string number, string tree, int state, int leyerNumber, int excempt, string storeid)
        {
            SqlParameter[] para =
            {
               new SqlParameter("@ID",number),
               new SqlParameter("@TREE",tree),
               new SqlParameter("@ISAZ",state),
               new SqlParameter("@CS",leyerNumber),
               new SqlParameter("@ExpectNum",excempt),
               new SqlParameter("@storeID",storeid)
            
            };
            return DBHelper.ExecuteDataTable("JS_TreeNet", para, CommandType.StoredProcedure);

        }

        public DataTable GetExtendTreeView_newAz(string number, string tree, int leyerNumber, int excempt, int type)
        {
            SqlParameter[] para =
            {
               new SqlParameter("@BH",number),               
               new SqlParameter("@qishu",excempt),
               new SqlParameter("@TREE",""),
               new SqlParameter("@TREE12",tree),
               new SqlParameter("@CS",leyerNumber),
               new SqlParameter("@type",type)               
            
            };
            return DBHelper.ExecuteDataTable("JS_TreeNet1", para, CommandType.StoredProcedure);

        }

        public DataTable GetExtendTreeView_newAz(string number, string tree, int leyerNumber, int excempt,int type,int cw)
        {
            SqlParameter[] para =
            {
               new SqlParameter("@BH",number),               
               new SqlParameter("@qishu",excempt),
               new SqlParameter("@TREE",""),
               new SqlParameter("@TREE12",tree),
               new SqlParameter("@CS",leyerNumber),
               new SqlParameter("@type",type),
               new SqlParameter("@jcCw",cw)
            
            };
            return DBHelper.ExecuteDataTable("JS_TreeNet1_New", para, CommandType.StoredProcedure);

        }

        public DataTable GetExtendTreeView_newAz1(string number, string tree, int leyerNumber, int excempt, int type, int cw)
        {
            SqlParameter[] para =
            {
               new SqlParameter("@ID",number),
               new SqlParameter("@TREE",tree),
               new SqlParameter("@ISAZ",1),
               new SqlParameter("@CS",leyerNumber),
               new SqlParameter("@ExpectNum",excempt),
               new SqlParameter("@storeID","")
            };
            return DBHelper.ExecuteDataTable("JS_TreeNet", para, CommandType.StoredProcedure);

        }

        public DataTable GetExtendTreeView_NewTj(string number, string tree, int leyerNumber, int excempt, int type,int cw)
        {
            SqlParameter[] para =
            {
               new SqlParameter("@BH",number),               
               new SqlParameter("@qishu",excempt),
               new SqlParameter("@TREE",""),
               new SqlParameter("@TREE12",tree),
               new SqlParameter("@CS",leyerNumber),
               new SqlParameter("@type",type),
               new SqlParameter("@jcCw",cw)
            
            };
            DataTable dt = DBHelper.ExecuteDataTable("JS_TreeNet2_New", para, CommandType.StoredProcedure);
            int count = dt.Rows.Count;
            return dt;

        }

        public DataTable GetExtendTreeView_NewTj(string number, string tree, int leyerNumber, int excempt,int type)
        {
            SqlParameter[] para =
            {
               new SqlParameter("@BH",number),               
               new SqlParameter("@qishu",excempt),
               new SqlParameter("@TREE",""),
               new SqlParameter("@TREE12",tree),
               new SqlParameter("@CS",leyerNumber),
               new SqlParameter("@type",type)
            
            };
            DataTable dt = DBHelper.ExecuteDataTable("JS_TreeNet2_New", para, CommandType.StoredProcedure);
            int count = dt.Rows.Count;
            return dt;

        }
    }
}
