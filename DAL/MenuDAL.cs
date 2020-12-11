using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Model;

namespace DAL
{
    public class MenuDAL
    {
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <param name="pid">父菜单编号</param>
        /// <returns>返回菜单列表</returns>
        public static IList<MenuModel> GetThisMenu(int type,int pid)
        {
            string field = System.Web.HttpContext.Current.Session["LanguageCode"].ToString();
            IList<MenuModel> menus = new List<MenuModel>();

            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@type", type);
            paras[1] = new SqlParameter("@pid", pid);
            StringBuilder sql = new StringBuilder();
            sql.Append("select m.ID,t." + field + " as MenuName,m.MenuFile,isnull(m.ImgFile,'') as ImgFile,m.MenuType,m.ParentID,m.isfold from menu m,t_translation t where m.id = t.primarykey and t.tablename='menu' and m.MenuType=@type and m.ParentID=@pid order by m.sortid");
            SqlDataReader reader = DBHelper.ExecuteReader(sql.ToString(), paras, CommandType.Text);
            while (reader.Read())
            {
                MenuModel menu = new MenuModel();
                menu.ID = Convert.ToInt32(reader["ID"].ToString());
                menu.MenuName = reader["MenuName"].ToString();
                menu.MenuFile = reader["MenuFile"].ToString();
                menu.ImgFile = reader["ImgFile"].ToString();
                menu.MenuType = Convert.ToInt32(reader["MenuType"].ToString());
                menu.ParentID = Convert.ToInt32(reader["ParentID"].ToString());
                menu.Isfold = Convert.ToInt32(reader["IsFold"].ToString());
                menus.Add(menu);
            }
            reader.Close();
            return menus;
        }

        /// <summary>
        ///  获取菜单个数
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <param name="pid">父菜单编号</param>
        /// <returns>返回菜单个数</returns>
        public static int GetMenuCount(int type, int pid)
        {
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@type", type);
            paras[1] = new SqlParameter("@pid", pid);
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(id) from menu where MenuType=@type and ParentID=@pid");
            string count= DBHelper.ExecuteScalar(sql.ToString(), paras, CommandType.Text).ToString();
            return int.Parse(count);
        }

        /// <summary>
        ///  获取菜单个数
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <param name="pid">父菜单编号</param>
        /// <returns>返回菜单个数</returns>
        public static string GetLeftMenuName(int pid)
        {
            string field = System.Web.HttpContext.Current.Session["LanguageCode"].ToString() ;
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@pid", pid);
            StringBuilder sql = new StringBuilder();
            sql.Append("select top 1 t." + field + " as menuName,m.isfold from menuleft m,t_translation t where m.id = t.primarykey and t.tablename = 'menuleft' and id=@pid");
            string mName = DBHelper.ExecuteScalar(sql.ToString(), paras, CommandType.Text).ToString();
            return mName;
        }

        /// <summary>
        ///  获取菜单个数
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <param name="pid">父菜单编号</param>
        /// <returns>返回菜单个数</returns>
        public static DataTable GetLeftMenuNameDT(int pid)
        {
            string field = System.Web.HttpContext.Current.Session["LanguageCode"].ToString();
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@pid", pid);
            StringBuilder sql = new StringBuilder();
            sql.Append("select t." + field + " as menuName,m.menuFile,m.id,m.isfold from menuleft m,t_translation t where m.id = t.primarykey and t.tablename = 'menuleft' and m.parentID=@pid and m.isfold=1 order by m.sortid");
            DataTable dt = DBHelper.ExecuteDataTable(sql.ToString(), paras, CommandType.Text);
            return dt;
        }


        /// <summary>
        /// 获取菜单头名称
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <param name="tid">菜单ID</param>
        /// <returns>菜单名称</returns>
        public static string GetTitleName(int type, int tid)
        {
            string field = System.Web.HttpContext.Current.Session["LanguageCode"].ToString();
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@type", type);
            paras[1] = new SqlParameter("@tid", tid);
            StringBuilder sql = new StringBuilder();
            sql.Append("select t." + field + " menuname,m.isfold from menu m,t_translation t where m.id = t.primarykey and t.tablename='menu' and m.MenuType=@type and m.id=@tid order by m.sortid");
            return DBHelper.ExecuteScalar(sql.ToString(), paras, CommandType.Text).ToString();
        }
        /// <summary>
        /// 获取菜单头名称
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <param name="tid">菜单ID</param>
        /// <returns>菜单名称</returns>
        public static string GetTitleNameLeft(int tid)
        {
            string field = System.Web.HttpContext.Current.Session["LanguageCode"].ToString();
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@tid", tid);
            StringBuilder sql = new StringBuilder();
            sql.Append("select t." + field + " as menuName,m.isfold from menuleft m,t_translation t where m.id = t.primarykey and t.tablename = 'menuleft' and  m.id=@tid");
            return DBHelper.ExecuteScalar(sql.ToString(), paras, CommandType.Text).ToString();
        }
    }
}
