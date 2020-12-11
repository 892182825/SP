using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Add Namespace
using System.Data;
using System.Data.SqlClient;
using Model;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-09
 * 文件名：     DocTypeTableDAL
 */

namespace DAL
{
    /// <summary>
    /// 单据类型（冲红）表
    /// </summary>
    public class DocTypeTableDAL
    {
        /// <summary>
        /// 向单据类型（冲红）表中插入记录
        /// </summary>
        /// <param name="docTypeTable">单据类型（冲红）模型</param>
        /// <returns>返回向单据类型（冲红）表中插入记录所影响的行数</returns>
        public static int AddDocTypeTable(DocTypeTableModel docTypeTable)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@docTypeName",SqlDbType.VarChar,20),
                new SqlParameter("@isRubric",SqlDbType.Int),
                new SqlParameter("@docTypeDescr",SqlDbType.VarChar,500)
            };
            sparams[0].Value = docTypeTable.DocTypeName;
            sparams[1].Value = docTypeTable.IsRubric;
            sparams[2].Value = docTypeTable.DocTypeDescr;

            return DBHelper.ExecuteNonQuery("AddDocTypeTable", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 删除指定单据类型（冲红）记录
        /// </summary>
        /// <param name="docTypeID">单据类型ID</param>
        /// <returns>返回删除指定单据类型（冲红）记录</returns>
        public static int DelDocTypeTableByID(int docTypeID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@docTypeID",SqlDbType.Int)
            };
            sparams[0].Value = docTypeID;

            return DBHelper.ExecuteNonQuery("DelDocTypeTableByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 更新指定单据类型（冲红）表中记录
        /// </summary>
        /// <param name="docTypeTable">单据类型（冲红）模型</param>
        /// <returns>返回更新指定单据类型（冲红）表中记录所影响的行数</returns>
        public static int UpdDocTypeTableByID(DocTypeTableModel docTypeTable)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@docTypeID",SqlDbType.Int),
                new SqlParameter("@docTypeName",SqlDbType.VarChar,20),
                new SqlParameter("@isRubric",SqlDbType.Int),
                new SqlParameter("@docTypeDescr",SqlDbType.VarChar,500)
            };
            sparams[0].Value = docTypeTable.DocTypeID;
            sparams[1].Value = docTypeTable.DocTypeName;
            sparams[2].Value = docTypeTable.IsRubric;
            sparams[3].Value = docTypeTable.DocTypeDescr;

            return DBHelper.ExecuteNonQuery("UpdDocTypeTableByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 更新支付方式
        /// </summary>
        /// <param name="payId">支付方式ID</param>
        /// <returns>返回更新支付方式所影响的行数</returns>
        public static int UpdPaymentType(int availability, int id, int isStore, SqlTransaction tran)
        {
            string strSql = "Update payment set availability=@availability where id = @payId and isStore = @isStore";
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@availability",SqlDbType.Int),
                new SqlParameter("@payId",SqlDbType.Int),
                new SqlParameter("@isStore",SqlDbType.Int)
            };
            sparams[0].Value = availability;
            sparams[1].Value = id;
            sparams[2].Value = isStore;

            return DBHelper.ExecuteNonQuery(tran, strSql, sparams, CommandType.Text);
        }

        #region 根据单据名，得到单据类型ID
        /// <summary>
        /// 根据单据名获取单据类型ID
        /// </summary>
        /// <param name="docTypeName">单据名称</param>
        /// <returns>返回单据类型ID</returns>    
        public static int GetDocTypeIDByDocTypeName(string docTypeName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@docTypeCode",SqlDbType.VarChar,40)
            };
            sparams[0].Value = docTypeName;
            try
            {
                return Convert.ToInt32(DBHelper.ExecuteScalar("select DocTypeID from DocTypeTable Where DocTypeCode=@docTypeCode", sparams, CommandType.Text));
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region
        /// <summary>
        /// 根据单据编号获取单据类型ID
        /// </summary>
        /// <param name="docTypeName">单据编码</param>
        /// <returns>返回单据类型ID</returns>    
        public static int GetDocTypeIDEByDocTypeCode(string docTypecode)
        {
            int i = 0;
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@docTypeName",SqlDbType.VarChar,40)
            };
            sparams[0].Value = docTypecode;
            try
            {
                SqlDataReader dr = DBHelper.ExecuteReader("GetDocTypeIDEByDocTypeName", sparams, CommandType.StoredProcedure);
                if (dr.Read())
                {
                    i = Convert.ToInt32(dr["DocTypeID"]);
                }
                dr.Close();

                return i;
            }
            catch
            {
                return -1;
            }
        }
        #endregion
        /// <summary>
        /// Judge the DocTypeID whether has operation by DocTypeID before delete
        /// </summary>
        /// <param name="docTypeID">DocTypeID</param>
        /// <returns>Return counts</returns>
        public static int DocTypeIDWhetherHasOperation(int docTypeID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@docTypeID",SqlDbType.Int)
            };
            sparams[0].Value = docTypeID;

            return Convert.ToInt32(DBHelper.ExecuteScalar("DocTypeIDWhetherHasOperation", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Judge the DocTypeID whether exist by DocTypeID before delete or update
        /// </summary>
        /// <param name="docTypeID">DocTypeID</param>
        /// <returns>Return counts</returns>
        public static int DocTypeIDIsExist(int docTypeID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@docTypeID",SqlDbType.Int)
            };
            sparams[0].Value = docTypeID;

            return Convert.ToInt32(DBHelper.ExecuteScalar("DocTypeIDIsExist", sparams, CommandType.StoredProcedure));
        }


        /// <summary>
        /// 获取指定的单据类型行数
        /// </summary>
        /// <param name="docTypeName">单据类型名称</param>
        /// <returns>返回获取指定的单据类型行数</returns>
        public static int GetDocTypeTableCountByName(string docTypeName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                 new SqlParameter("@docTypeName",SqlDbType.VarChar,20)
            };
            sparams[0].Value = docTypeName;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetDocTypeTableCountByName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取指定的单据类型行数
        /// </summary>
        /// <param name="docTypeID">单据类型ID</param>
        /// <param name="docTypeName">单据类型名称</param>
        /// <returns>返回获取指定的单据类型行数</returns>
        public static int GetDocTypeTableCountByIDName(int docTypeID, string docTypeName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@docTypeID",SqlDbType.Int),
                new SqlParameter("@docTypeName",SqlDbType.VarChar,20)
            };
            sparams[0].Value = docTypeID;
            sparams[1].Value = docTypeName;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetDocTypeTableCountByIDName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取单据类型（冲红）信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetDocTypeTableInfo()
        {
            return DBHelper.ExecuteDataTable("GetDocTypeTableInfo", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 根据编号获取名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetDocTypeNameByID(int id)
        {
            return Convert.ToString(DBHelper.ExecuteScalar("GetDocTypeNameById", new SqlParameter("@id", id), CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取指定单据类型（冲红）信息
        /// </summary>
        /// <param name="docTypeID">单据类型ID</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetDocTypeTableInfoByID(int docTypeID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@docTypeID",SqlDbType.Int)
            };
            sparams[0].Value = docTypeID;

            return DBHelper.ExecuteDataTable("GetDocTypeTableInfoByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Out to excel of the data of DocTypeTable
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_DocTypeTable()
        {
            return DBHelper.ExecuteDataTable("OutToExcel_DocTypeTable", CommandType.StoredProcedure);
        }


        /// <summary>
        ///  获取公司设置系统能否使用的支付方式
        /// </summary>
        /// <param name="payid">
        /// 要根据后台改动，类型如下：
        /// 
        /// 会员：
        ///     现金支付(1,5)  账户支付（2，5） 支付宝（3，5） 快钱（4，5） 环迅（5，5） 离线汇款（6，5）
        /// 
        /// 服务机构：
        ///     账户支付（2，1） 支付宝（3，1） 快钱（4，1） 环迅（5，1） 离线汇款（1，1）
        ///     
        /// 网银支付：
        ///     网银直连接口 支付宝（1，8） 快钱（2，8）
        /// </param>
        /// <param name="isstore"></param>
        /// <returns></returns>
        public static bool Getpaytypeisusebyid(int payid, int isstore)
        {
            string sqlst = " select  isnull(availability,0) from  payment  where payid=@pid and   isStore=@isstore";

            SqlParameter[] sps = new SqlParameter[] {
              new SqlParameter("@pid",payid),
              new SqlParameter("@isstore",isstore)
            };
            int rs = Convert.ToInt32(DBHelper.ExecuteScalar(sqlst, sps, CommandType.Text));
            return rs == 1;
        }


        /// <summary>
        /// 更新网银直连接口 
        /// </summary>
        /// <param name="payId">支付方式ID</param>
        /// <returns>返回更新支付方式所影响的行数</returns>
        public static int UpdPaymentwangyinType(int id, int isStore, int isusewangyin)
        {
            string strSql = " Update payment set availability=0 where   isStore =  " + isStore + " ;  Update payment set availability=1 where id =  " + id +
                ";  update payment set availability=" + isusewangyin + "  where  isstore=9 and payid=1";


            return DBHelper.ExecuteNonQuery(strSql, CommandType.Text);
        }

    }
}