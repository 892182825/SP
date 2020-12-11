using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using Model;
using System.Data;
using System.Data.SqlClient;

/*
 * 创建者：  汪  华
 * 创建时间：2009-10-19
 */

namespace DAL
{
    public class BackupDatabaseDAL
    {
        /// <summary>
        /// 向数据库备份路径表中插入记录
        /// </summary>
        /// <param name="backupDatabaseModel">数据库备份路径表模型</param>
        /// <returns>返回向数据库备份路径表中插入记录所影响的行数</returns>
        public static int AddBackupDatabase(BackupDatabaseModel backupDatabaseModel)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@dataBackupTime",SqlDbType.DateTime),
                new SqlParameter("@operatorNum",SqlDbType.VarChar,50),
                new SqlParameter("@pathFileName",SqlDbType.VarChar,500)
            };
            sparams[0].Value = backupDatabaseModel.DataBackupTime;
            sparams[1].Value = backupDatabaseModel.OperatorNum;
            sparams[2].Value = backupDatabaseModel.PathFileName;
            return DBHelper.ExecuteNonQuery("AddBackupDatabase", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 删除指定文件路径名数据记录
        /// </summary>
        /// <param name="filePathName">文件路径名</param>
        /// <returns>返回删除指定文件路径名数据记录</returns>
        public static int DelBackupDatabaseByFilePathName(string filePathName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@filePathName",SqlDbType.VarChar,300)
            };
            sparams[0].Value = filePathName;
            return DBHelper.ExecuteNonQuery("DelBackupDatabaseByFilePathName", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="databaseName">数据库名称</param>
        /// <param name="pathFileName">路径及文件名</param>
        /// <returns>返回</returns>
        public static int BackupDatabaseInfo(string databaseName, string pathFileName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@databaseName",SqlDbType.VarChar,100),
                new SqlParameter("@pathFileName",SqlDbType.VarChar,300)
            };
            sparams[0].Value = databaseName;
            sparams[1].Value = pathFileName;
            return DBHelper.ExecuteNonQuery("BackupDatabaseInfo", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取备份数据库系信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetBackupDatabaseInfo()
        {
            return DBHelper.ExecuteDataTable("GetBackupDatabaseInfo", CommandType.StoredProcedure);
        }


        /// <summary>
        /// 设定要备份的数据库名
        /// </summary>
        /// <returns>返回要备份的数据库名</returns>
        public static string GetDataBaseName()
        {
            string dataBaseName = "";
            string strSql = "select db_name()";
            SqlDataReader dr = DBHelper.ExecuteReader(strSql,CommandType.Text);
            while (dr.Read())
            {
                dataBaseName = dr[0].ToString();
            }
            dr.Close();
            return dataBaseName;
        }
    }
}
