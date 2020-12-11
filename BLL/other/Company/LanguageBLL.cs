using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data;
using System.Data.SqlClient ;

namespace BLL.other.Company
{
    public class LanguageBLL
    {
        /// <summary>
        /// 根据id查询登陆语言的名字
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetLanguageNameById(int id)
        {
            // LanguageDAL lanaguage = new LanguageDAL();
            return LanguageDAL.GetLanguageNameById(id);
        }

        /// <summary>
        /// 获取默认语言表名
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultLanguageTableName()
        {
            return LanguageDAL.GetDefaultLanguageTableName();
        }

        /// <summary>
        /// 获取默认语言名
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultlLanguageName()
        {
            return LanguageDAL.GetDefaultlLanguageName();
        }

        /// <summary>
        /// 根据语言名获取语言表名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetLanguageTableNameByName(string name)
        {
            return LanguageDAL.GetLanguageTableNameByName(name);
        }
        /// <summary>
        /// 获取所有语言
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<LanguageModel> GetAllLanguage()
        {
            return LanguageDAL.GetAllLanguage();
        }
        //添加翻译语言
        public static void AddRendition(string sourcelanguage, string renditionlanguage)
        {
            LanguageDAL.AddRendition(sourcelanguage, renditionlanguage);
        }
        //添加翻译语言
        public static bool IsExistsRendition(string sourcelanguage, string renditionlanguage)
        {
            return LanguageDAL.IsExistsRendition(sourcelanguage, renditionlanguage);
        }
        //查询翻译语言
        public static DataTable SelectRendition()
        {
            return LanguageDAL.SelectRendition();
        }
        //删除翻译语言
        public static void DelRendition(int id)
        {
            LanguageDAL.DelRendition(id);
        }
        //添加三翻译语言
        public static void AddThreeRendition(string sourcelanguage, string renditionlanguage)
        {
            LanguageDAL.AddThreeRendition(sourcelanguage, renditionlanguage);
        }
        public static bool IsExistsTreeRendition(string sourcelanguage, string renditionlanguage)
        {
            return LanguageDAL.IsExistsTreeRendition(sourcelanguage, renditionlanguage);
        }

        #region 新增
        /// <summary>
        /// 添加新语言[新]
        /// </summary>
        /// <param name="LanguageName">语言名称</param>
        /// <param name="remark">语言描述</param>
        /// <returns></returns>
        public static int AddNewLanguage(string LanguageName, string remark)
        {
            int var = 0;
            SqlTransaction tran = null;
            SqlConnection con = DAL.DBHelper.SqlCon();
            try
            {
                con.Open();
                tran = con.BeginTransaction();
                var = LanguageDAL.AddNewLanguage(tran, LanguageName, remark);
                tran.Commit();
            }
            catch
            {
                var = 0;
                tran.Rollback();
            }
            finally
            {
                con.Close();
            }
            return var;
        }

         /// <summary>
        /// 语言修改[新]
        /// </summary>
        /// <param name="id">语言编号</param>
        /// <param name="name">语言名称</param>
        /// <param name="remark">语言描述</param>
        /// <returns></returns>
        public static int ModifyLanguage(int id, string name, string remark)
        {
            return LanguageDAL.ModifyLanguage(id, name, remark);
        }

        /// <summary>
        /// 删除语言
        /// </summary>
        /// <param name="id">语言ID</param>
        /// <returns></returns>
        public static int DelLanguage(int id, string LanguageCode)
        {
            int var = 0;
            SqlTransaction tran = null;
            SqlConnection con = DAL.DBHelper.SqlCon();
            try
            {
                con.Open();
                tran = con.BeginTransaction();
                string ExSql = "delete from language where id=@id ";
                SqlParameter[] spas = new SqlParameter[]{
                    new SqlParameter("@id",SqlDbType.Int )    
                    };
                spas[0].Value = id;

                var = DAL.DBHelper.ExecuteNonQuery(tran, ExSql, spas, CommandType.Text);
                ExSql = "ALTER   TABLE   T_translation   DROP   COLUMN " + LanguageCode + " ";
                var = DAL.DBHelper.ExecuteNonQuery(tran, ExSql, spas, CommandType.Text);
                tran.Commit();
                var = 1;

            }
            catch (Exception ex)
            {
                tran.Rollback();
                var = 0;
            }
            finally
            {
                con.Close();
            }
            return var;
        }
        #endregion
    }
}
