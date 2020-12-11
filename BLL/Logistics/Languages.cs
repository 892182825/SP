using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace BLL.Logistics
{
    public class Languages
    {
        public Languages()
        {
        }

        /// <summary>
        /// 向翻译列添加翻译记录
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="tableName">数据库表名</param>
        /// <param name="columnName">表列名</param>
        /// <param name="primaryKey">自增标识ID</param>
        /// <param name="defaultText">默认值</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public bool AddNewTranslationRecord(SqlTransaction tran, string tableName, string columnName, int primaryKey, string defaultText, string description)
        {
            string ExSql = "select LanguageCode from language";
            string columns = " description,tableName,columnName,primaryKey";
            string values = "'" + description + "','" + tableName + "','" + columnName + "'," + primaryKey + "";
            string keyCode = string.Empty;
            int var = 0;
            SqlParameter[] spas;

            DataTable dt = DAL.DBHelper.ExecuteDataTable(tran, ExSql);
            if (dt == null || dt.Rows.Count < 1)
            {
                return false;
            }

            foreach (DataRow dr in dt.Rows)
            {
                columns += "," + dr["LanguageCode"].ToString().Trim();
                values += ",'" + defaultText + "'";
            }
            ExSql = "select count(1) from t_translation where tableName=@tableName and columnName=@columnName and primaryKey=@primaryKey";
            spas = new SqlParameter[] {
                new SqlParameter("@tableName", SqlDbType.VarChar , 50) ,
                new SqlParameter("@columnName", SqlDbType.NVarChar, 100) ,
                new SqlParameter("@primaryKey", SqlDbType.Int)
            };
            spas[0].Value = tableName;
            spas[1].Value = columnName;
            spas[2].Value = primaryKey;
            int exists = Convert.ToInt32(DAL.DBHelper.ExecuteScalar(tran, ExSql, spas, CommandType.Text));
            if (exists == 0)
            {//不存在
                ExSql = "select isnull(max(keyCode),0)+1 as newKeyCode  from t_translation";
                keyCode = DAL.DBHelper.ExecuteScalar(tran, ExSql, new SqlParameter[0] { }, CommandType.Text).ToString().Trim();
                keyCode = keyCode.PadLeft(6, '0');

                columns += ",keyCode ";
                values += ",'" + keyCode + "'";
            }
            else//
                return false;

            ExSql = "insert into t_translation (" + columns + ") "
                + " values(" + values + ")";
            var = DAL.DBHelper.ExecuteNonQuery(tran, ExSql);
            if (var > 0)
                return true;
            else
                return false;
        }

        public bool UpdateNewTranslationRecord(SqlTransaction tran, string tableName, string columnName, int primaryKey, string description)
        {
            string keyCode = "";
            var existsSql = "select count(1) from t_translation where tableName=@tableName and columnName=@columnName and primaryKey=@primaryKey";
            SqlParameter[] spa = new SqlParameter[] {
                new SqlParameter("@tableName", SqlDbType.VarChar , 50) ,
                new SqlParameter("@columnName", SqlDbType.NVarChar, 100) ,
                new SqlParameter("@primaryKey", SqlDbType.Int)
            };
            spa[0].Value = tableName;
            spa[1].Value = columnName;
            spa[2].Value = primaryKey;
            int exists = Convert.ToInt32(DAL.DBHelper.ExecuteScalar(tran, existsSql, spa, CommandType.Text));
            if (exists == 0)//不存在
            {
                var keyCodeSql = "select isnull(max(keyCode),0)+1 as newKeyCode  from t_translation";
                keyCode = DAL.DBHelper.ExecuteScalar(tran, keyCodeSql, CommandType.Text).ToString().Trim();
                keyCode = keyCode.PadLeft(6, '0');


                string sql = @"insert into T_translation
                                      (keyCode,[description],primarykey,columnName,L001,L002,tableName,L004,L005)
                                values(@keyCode,@description,@primarykey,@columnName,@value,@value,@tableName,@value,@value)";

                SqlParameter[] ss = new SqlParameter[] {
                new SqlParameter("@primarykey", SqlDbType.Int),
                new SqlParameter("@tableName", SqlDbType.NVarChar, 50),
                new SqlParameter("@columnName", SqlDbType.NVarChar, 100),
                new SqlParameter("@value", SqlDbType.NVarChar, 1000),
                new SqlParameter("@keyCode", SqlDbType.NVarChar,6),
                };
                ss[0].Value = primaryKey;
                ss[1].Value = tableName;
                ss[2].Value = columnName;
                ss[3].Value = description;
                ss[4].Value = keyCode;
                try
                {
                    var valueint = DAL.DBHelper.ExecuteNonQuery(tran, sql, ss, CommandType.Text);
                    if (valueint > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
             
            }
            else
            {
                string sql = @"update T_Translation set [description]=@value,L001=@value,L002=@value,L004=@value,L005=@value
where primarykey=@primarykey and columnName=@columnName and tableName=@tableName";
                SqlParameter[] spas = new SqlParameter[]
                {
                new SqlParameter("@primarykey", SqlDbType.Int),
                new SqlParameter("@tableName", SqlDbType.NVarChar, 50),
                new SqlParameter("@columnName", SqlDbType.NVarChar, 100),
                new SqlParameter("@value",SqlDbType.NVarChar,1000)
                };
                spas[0].Value = primaryKey;
                spas[1].Value = tableName;
                spas[2].Value = columnName;
                spas[3].Value = description;
                try
                {
                    var resint = DAL.DBHelper.ExecuteNonQuery(tran, sql, spas, CommandType.Text);
                    if (resint > 0)
                        return true;
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 向翻译列添加翻译记录
        /// </summary>      
        /// <param name="tableName">数据库表名</param>
        /// <param name="columnName">表列名</param>
        /// <param name="primaryKey">自增标识ID</param>
        /// <param name="defaultText">默认值</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public bool AddNewTranslationRecord(string tableName, string columnName, int primaryKey, string defaultText, string description)
        {
            string ExSql = "select LanguageCode from language";
            string columns = " description,tableName,columnName,primaryKey";
            string values = "'" + description + "','" + tableName + "','" + columnName + "'," + primaryKey + "";
            string keyCode = string.Empty;
            int var = 0;
            SqlParameter[] spas;

            DataTable dt = DAL.DBHelper.ExecuteDataTable(ExSql);
            if (dt == null || dt.Rows.Count < 1)
            {
                return false;
            }

            foreach (DataRow dr in dt.Rows)
            {
                columns += "," + dr["LanguageCode"].ToString().Trim();
                values += ",'" + defaultText + "'";
            }
            ExSql = "select count(1) from t_translation where tableName=@tableName and columnName=@columnName and primaryKey=@primaryKey";
            spas = new SqlParameter[] {
                new SqlParameter("@tableName", SqlDbType.VarChar , 50) ,
                new SqlParameter("@columnName", SqlDbType.NVarChar, 100) ,
                new SqlParameter("@primaryKey", SqlDbType.Int)
            };
            spas[0].Value = tableName;
            spas[1].Value = columnName;
            spas[2].Value = primaryKey;
            int exists = Convert.ToInt32(DAL.DBHelper.ExecuteScalar(ExSql, spas, CommandType.Text));
            if (exists == 0)
            {//不存在
                ExSql = "select isnull(max(keyCode),0)+1 as newKeyCode  from t_translation";
                keyCode = DAL.DBHelper.ExecuteScalar(ExSql, new SqlParameter[0] { }, CommandType.Text).ToString().Trim();
                keyCode = keyCode.PadLeft(6, '0');

                columns += ",keyCode ";
                values += ",'" + keyCode + "'";
            }
            else//
                return false;

            ExSql = "insert into t_translation (" + columns + ") "
                + " values(" + values + ")";
            var = DAL.DBHelper.ExecuteNonQuery(ExSql);
            if (var > 0)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 从语言表中移去翻译记录
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="tableName">数据库列名</param>
        /// <param name="primaryKey">表的自增标识ID</param>
        /// <returns></returns>
        public bool RemoveTranslationRecord(SqlTransaction tran, string tableName, int primaryKey)
        {
            int var = 0;
            string ExSql = "delete from t_translation where tableName=@tableName and primaryKey=@primaryKey";
            SqlParameter[] spas = new SqlParameter[] {
            new SqlParameter ("@tableName",SqlDbType .VarChar ,50),
            new SqlParameter ("@primaryKey",SqlDbType.Int),
            };
            spas[0].Value = tableName;
            spas[1].Value = primaryKey;

            var = DAL.DBHelper.ExecuteNonQuery(tran, ExSql, spas, CommandType.Text);
            if (var > 0)
                return true;
            else
                return false;

        }
        /// <summary>
        /// 从语言表中移去翻译记录
        /// </summary>      
        /// <param name="tableName">数据库列名</param>
        /// <param name="primaryKey">表的自增标识ID</param>
        /// <returns></returns>
        public bool RemoveTranslationRecord(string tableName, int primaryKey)
        {
            int var = 0;
            string ExSql = "delete from t_translation where tableName=@tableName and primaryKey=@primaryKey";
            SqlParameter[] spas = new SqlParameter[] {
            new SqlParameter ("@tableName",SqlDbType .VarChar ,50),
            new SqlParameter ("@primaryKey",SqlDbType.Int),
            };
            spas[0].Value = tableName;
            spas[1].Value = primaryKey;

            var = DAL.DBHelper.ExecuteNonQuery(ExSql, spas, CommandType.Text);
            if (var > 0)
                return true;
            else
                return false;

        }

    }
}
