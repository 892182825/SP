using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using Model;
using System.Data;
using System.Data.SqlClient;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-07
 * 文件名：     LanguageTransDAL
 * 功能：       对产品名称等翻译表进行增删改查
 */

namespace DAL
{
    public class LanguageTransDAL
    {
        /// <summary>
        /// 向产品名称等翻译表插入相关记录
        /// </summary>
        /// <param name="languageTrans">产品名称等翻译表模型</param>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddLanguageTrans(LanguageTransModel languageTrans)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@tableName",SqlDbType.VarChar,50),
                new SqlParameter("@oldID",SqlDbType.Int),                
                new SqlParameter("@columnName",SqlDbType.VarChar,50),                
                new SqlParameter("@languageName",SqlDbType.VarChar,200),                
                new SqlParameter("@languageID",SqlDbType.Int)                
            };

            sparams[0].Value = languageTrans.TableName;
            sparams[1].Value = languageTrans.OldID;
            sparams[2].Value = languageTrans.ColumnsName;
            sparams[3].Value = languageTrans.LanguageName;
            sparams[4].Value = languageTrans.LanguageID;
                        
            return DBHelper.ExecuteNonQuery("AddLanguageTrans", sparams, CommandType.StoredProcedure);
        }
        
        /// <summary>
        /// 向产品名称等翻译表插入相关记录
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="languageTrans">产品名称等翻译表模型</param>
        /// <returns>返回插入所影响的行数</returns>
        public static int AddLanguageTrans(SqlTransaction tran, LanguageTransModel languageTrans)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@tableName",SqlDbType.VarChar,50),
                new SqlParameter("@oldID",SqlDbType.Int),                
                new SqlParameter("@columnName",SqlDbType.VarChar,50),                
                new SqlParameter("@languageName",SqlDbType.VarChar,200),                
                new SqlParameter("@languageID",SqlDbType.Int)              
            };

            sparams[0].Value = languageTrans.TableName;
            sparams[1].Value = languageTrans.OldID;
            sparams[2].Value = languageTrans.ColumnsName;
            sparams[3].Value = languageTrans.LanguageName;
            sparams[4].Value = languageTrans.LanguageID;
                        
            return DBHelper.ExecuteNonQuery("AddLanguageTrans", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 根据语言ID删除指定的产品名称等翻译记录
        /// </summary>
        /// <param name="languageID">语言ID</param>
        /// <returns>返回删除指定记录所影响的行数</returns>
        public static int DelLanguageTransByLanguageID(int languageID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@languageID",SqlDbType.Int)
            };
            sparams[0].Value = languageID;
            
            return DBHelper.ExecuteNonQuery("DelLanguageTransByLanguageID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 根据原表中的ID删除指定记录
        /// </summary>
        /// <param name="tran">要处理的事务</param>
        /// <param name="oldID">原表中的ID</param>
        /// <returns>返回删除指定记录所影响的行数</returns>
        public static int DelLanguageTransByOldID(SqlTransaction tran, int oldID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@oldID",SqlDbType.Int)
            };
            sparams[0].Value = oldID;
            
            return DBHelper.ExecuteNonQuery("DelLanguageTransByOldID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 更新指定ID的翻译结果
        /// </summary>
        /// <param name="languageName">翻译结果</param>
        /// <param name="id">标识ID</param>
        /// <returns>返回更新指定ID的翻译结果所影响的行数</returns>
        public static int UplLanguageTransLanguageNameByID(string languageName,int id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@languageName",SqlDbType.VarChar,200),
                new SqlParameter("@id",SqlDbType.Int)
            };
            sparams[0].Value = languageName;
            sparams[1].Value = id;
            return DBHelper.ExecuteNonQuery("UplLanguageTransLanguageNameByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取行数
        /// </summary>
        /// <param name="languageID">语言ID</param>
        /// <returns>返回行数</returns>
        public static int GetLanguageTransCountByID(int languageID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@languageID",SqlDbType.Int)
            };
            sparams[0].Value = languageID;
            
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetLanguageTransCountByID", sparams, CommandType.StoredProcedure));
        }
    }
}
