using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using Model;
using System.Data;
using System.Data.SqlClient;

/*
 *Creator:      WangHua
 *CreateDate:   2010-01-25 
 *FinishDate:   2010-01-25
 *Function：     Language
 */

namespace DAL
{
    public class LanguageDAL
    {
        /// <summary>
        /// 根据id查询登陆语言的名字
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetLanguageNameById(int id)
        {
            object obj = DBHelper.ExecuteScalar("GetLanguageNameById", new SqlParameter("@id", id), CommandType.StoredProcedure);
            if (obj != null)
            {
                return obj.ToString();
            }
            return "";
        }
        /// <summary>
        /// 向语言表中插入记录
        /// </summary>
        /// <param name="languageModel">语言模型</param>
        /// <returns>返回向语言表中插入记录所影响的行数</returns>
        public static int AddLanguage(LanguageModel languageModel)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@name",SqlDbType.VarChar,50),
                new SqlParameter("@tableName",SqlDbType.VarChar,50)
            };
            sparams[0].Value = languageModel.Name;
            sparams[1].Value = languageModel.TableName;

            return DBHelper.ExecuteNonQuery("AddLanguage", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 删除指定的语言记录
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>返回删除指定记录所影响的行数</returns>
        public static int DelLanguageByID(int ID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@ID",SqlDbType.Int)
            };
            sparams[0].Value = ID;

            return DBHelper.ExecuteNonQuery("DelLanguageByID", sparams, CommandType.StoredProcedure);
        }



        /// <summary>
        /// 通过语言名称获取行数
        /// </summary>
        /// <param name="name">语言名称</param>
        /// <returns>返回行数</returns>
        public static int GetLanguageCountByName(string name)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@name",SqlDbType.VarChar,40)
            };
            sparams[0].Value = name;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetLanguageCountByName", sparams, CommandType.StoredProcedure));
        }


        /// <summary>
        /// 获取语言对应的ID
        /// </summary>
        /// <param name="name">语言名称</param>
        /// <returns>返回语言所对应的ID</returns>
        public static int GetLanguageIDByName(string name)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@name",SqlDbType.VarChar,50)
            };
            sparams[0].Value = name;
 
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetLanguageIDByName", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取语言表中最大的ID
        /// </summary>
        /// <returns>返回语言表中最大的ID</returns>
        public static int GetLanguageMaxID()
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetLanguageMaxID", CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取语言ID和语言名称
        /// </summary>
        /// <returns>返回SqlDataReader对象</returns>
        public static SqlDataReader GetAllLanguageIDNameOrderByName()
        {
            return DBHelper.ExecuteReader("GetAllLanguageIDNameOrderByName", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取语言ID和语言名称
        /// </summary>
        /// <returns>返回DataTable</returns>
        public static DataTable GetAllLanguageIDName()
        {
            return DBHelper.ExecuteDataTable("GetLanguageIDName", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取语言信息
        /// </summary>
        /// <returns>返回Ilist对象</returns>
        public static IList<LanguageModel> GetLanguageInfoOrderByName()
        {
            SqlDataReader dr = DBHelper.ExecuteReader("GetLanguageInfoOrderByName", CommandType.StoredProcedure);
            IList<LanguageModel> listLanguage = new List<LanguageModel>();
            while (dr.Read())
            {
                LanguageModel languageModel = new LanguageModel();
                languageModel.Name = dr["Name"].ToString();
                languageModel.TableName = dr["TableName"].ToString();
                listLanguage.Add(languageModel);
            }
            dr.Close();
            return listLanguage;
        }

        /// <summary>
        /// 获取不包含中文语言信息
        /// </summary>
        /// <returns>返回Ilist对象</returns>
        public static IList<LanguageModel> GetLanguageInfoNoCludingChineseOrderByName()
        {
            SqlDataReader dr = DBHelper.ExecuteReader("GetLanguageInfoNoCludingChineseOrderByName", CommandType.StoredProcedure);
            IList<LanguageModel> listLanguage = new List<LanguageModel>();
            while (dr.Read())
            {
                LanguageModel languageModel = new LanguageModel();
                languageModel.ID = Convert.ToInt32(dr["ID"]);
                languageModel.Name = dr["Name"].ToString();
                listLanguage.Add(languageModel);
            }
            dr.Close();
            return listLanguage;
        }

        /// <summary>
        /// 通过语言ID获取ID
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetLanguageIDByID()
        {
            return DBHelper.ExecuteDataTable("GetLanguageIDByID", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取默认语言表名
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultLanguageTableName()
        {
            return DBHelper.ExecuteScalar("Select Top 1 TableName From LANGUAGE ORDER BY ID").ToString().Replace("TranTo", "");
        }

        /// <summary>
        /// 获取默认语言名
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultlLanguageName()
        {
            return DBHelper.ExecuteScalar("Select Top 1 Name From LANGUAGE ORDER BY ID").ToString();
        }

        /// <summary>
        /// 根据语言名获取语言表名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetLanguageTableNameByName(string name)
        {
            string sql = "Select TableName From LANGUAGE where Name=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@num",SqlDbType.NVarChar,50)
            };
            sparams[0].Value = name;
            return DBHelper.ExecuteScalar(sql,sparams,CommandType.Text).ToString().Replace("TranTo", "");
        }

        public static List<LanguageModel> GetAllLanguage()
        {
            List<LanguageModel> list = new List<LanguageModel>();
            string sql = "select * from LANGUAGE";
            SqlDataReader reader = DBHelper.ExecuteReader(sql);
            while (reader.Read())
            {
                LanguageModel mode = new LanguageModel(Convert.ToInt32(reader["ID"]));
                mode.Name = reader["Name"].ToString();
                mode.TableName = reader["TableName"].ToString();
                list.Add(mode);
            }
            reader.Close();
            return list;
        }
        //添加翻译语言
        public static void AddRendition(string sourcelanguage, string renditionlanguage)
        {
            string sql = "insert into Rendition values(@num,@num1,0)";
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@num",SqlDbType.NVarChar,50),
                new SqlParameter("@num1",SqlDbType.NVarChar,50)
            };
            sparams[0].Value = sourcelanguage;
            sparams[1].Value = renditionlanguage;
            DBHelper.ExecuteNonQuery(sql,sparams,CommandType.Text);
        }
        //添加三翻译语言
        public static void AddThreeRendition(string sourcelanguage, string renditionlanguage)
        {
            string sql = "insert into Rendition values(@num,@num1,1)";
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@num",SqlDbType.NVarChar,50),
                new SqlParameter("@num1",SqlDbType.NVarChar,50)
            };
            sparams[0].Value = sourcelanguage;
            sparams[1].Value = renditionlanguage;
            DBHelper.ExecuteNonQuery(sql, sparams, CommandType.Text);
        }

        public static bool IsExistsRendition(string sourcelanguage, string renditionlanguage)
        {
            string sql = "select count(1) from Rendition where sourcelanguage=@num and renditionlanguage=@num1 and flag=0";
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@num",SqlDbType.NVarChar,50),
                new SqlParameter("@num1",SqlDbType.NVarChar,50)
            };
            sparams[0].Value = sourcelanguage;
            sparams[1].Value = renditionlanguage;
            int count = Convert.ToInt32(DBHelper.ExecuteScalar(sql,sparams,CommandType.Text));
            if (count > 0)
                return true;
            else
                return false;
        }
        public static bool IsExistsTreeRendition(string sourcelanguage, string renditionlanguage)
        {
            string sql = "select count(1) from Rendition where sourcelanguage=@num and renditionlanguage=@num1 and flag=1";
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@num",SqlDbType.NVarChar,50),
                new SqlParameter("@num1",SqlDbType.NVarChar,50)
            };
            sparams[0].Value = sourcelanguage;
            sparams[1].Value = renditionlanguage;
            int count = Convert.ToInt32(DBHelper.ExecuteScalar(sql,sparams,CommandType.Text));
            if (count > 0)
                return true;
            else
                return false;
        }

        public static DataTable SelectRendition()
        {
            string sql = "select * from Rendition where flag=0";
            return DBHelper.ExecuteDataTable(sql);
        }

        public static void DelRendition(int id)
        {
            string sql = "delete Rendition where id=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@num",SqlDbType.Int)
                
            };
            sparams[0].Value = id;
            DBHelper.ExecuteNonQuery(sql,sparams,CommandType.Text);
        }

        #region 新增
        /// <summary>
        /// 向语言表中插入记录
        /// </summary>
        /// <param name="LanguageName">语言名称</param>
        /// <param name="remark">语言描述</param>
        /// <returns></returns>
        public static int AddNewLanguage(SqlTransaction tran, string LanguageName, string remark)
        {
            string tableName = string.Empty;
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@name",SqlDbType.NVarChar,50),
                new SqlParameter("@tableName",SqlDbType.VarChar,50),
                new SqlParameter("@languageRemark",SqlDbType.NVarChar,50)
            };
            sparams[0].Value = LanguageName;
            sparams[1].Value = tableName;
            sparams[2].Value = remark;

            return DAL.DBHelper.ExecuteNonQuery(tran, "AddLanguage", sparams, CommandType.StoredProcedure);
             
        }

        /// <summary>
        /// 语言修改
        /// </summary>
        /// <param name="id">语言编号</param>
        /// <param name="name">语言名称</param>
        /// <param name="remark">语言描述</param>
        /// <returns></returns>
        public static int ModifyLanguage(int id, string name, string remark)
        {
            string ExSql = "update language set languageRemark=@languageRemark,name=@name where id=@id ";
            SqlParameter[] spas = new SqlParameter[] 
            {
                new SqlParameter("@id",SqlDbType.Int ),
                new SqlParameter("@name",SqlDbType.NVarChar,50),              
                new SqlParameter("@languageRemark",SqlDbType.NVarChar,50)
            };
            spas[0].Value = id;
            spas[1].Value = name;
            spas[2].Value = remark;

            int var = DAL.DBHelper.ExecuteNonQuery(ExSql, spas, CommandType.Text);
            return var;
        }
        #endregion 
    }
}
