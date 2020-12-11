using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Model;
using Model.Other;

namespace DAL
{
    public class CompanyDeptDAL
    {
        ///// <summary>
        ///// 获取公司部门
        ///// </summary>
        ///// <param name="pageInfo">分页帮助类</param>
        ///// <returns></returns>
        //public static IList<Model.CompanyDeptModel> GetCompanyDepts(PaginationModel pageInfo)
        //{
        //    IList<CompanyDeptModel> list = null;
        //    SqlParameter[] paras = new SqlParameter[]{
        //        GetSqlParameter("",SqlDbType.Int,pageInfo.RowCount)
        //    };
        //    SqlDataReader reader = DBHelper.ExecuteReader("", CommandType.StoredProcedure);
        //    if (reader.HasRows)
        //    {
        //        list = new List<CompanyDeptModel>();
        //        while (reader.Read())
        //        {
        //            CompanyDeptModel companyDeptModel = new CompanyDeptModel(reader.GetInt32(0));
        //            companyDeptModel.Dept = reader.GetString(1);
        //            companyDeptModel.Adddate = reader.GetDateTime(2);
        //            list.Add(companyDeptModel);
        //        }
        //    }
        //    reader.Close();
        //    return list;
        //}


        /// <summary>
        /// 获取公司部门
        /// </summary>
        /// <returns></returns>
        public static IList<Model.CompanyDeptModel> GetCompanyDepts()
        {
            IList<CompanyDeptModel> list = null;
            SqlDataReader reader = DBHelper.ExecuteReader("select id,Dept from CompanyDept order By id", CommandType.Text);
            if (reader.HasRows)
            {
                list = new List<CompanyDeptModel>();
                while (reader.Read())
                {
                    CompanyDeptModel companyDeptModel = new CompanyDeptModel(reader.GetInt32(0));
                    companyDeptModel.Dept = reader.GetString(1);
                    list.Add(companyDeptModel);
                }
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 添加公司部门
        /// </summary>
        /// <param name="companyDept"></param>
        /// <returns></returns>
        public static int AddCompanyDept(Model.CompanyDeptModel companyDept)
        {
            int i = -1;
            try
            {
                SqlParameter[] paras = new SqlParameter[]{
                GetSqlParameter("@Dept",SqlDbType.NVarChar,companyDept.Dept),
                GetSqlParameter("@AddDate",SqlDbType.DateTime, companyDept.Adddate)
            };
                i = DBHelper.ExecuteNonQuery("AddCompanyDept", paras, CommandType.StoredProcedure);
            }
            catch (SqlException)
            {
                throw new Exception("添加公司部门信息产生异常!");
            }
            return i;
        }


        public static SqlParameter GetSqlParameter(string paramName, SqlDbType sqlDbType, object obj)
        {
            SqlParameter para = new SqlParameter(paramName, sqlDbType);
            para.Value = obj;
            return para;
        }

        /// <summary>
        /// ds2012
        /// 修改部门信息
        /// </summary>
        /// <param name="companyDept">部门信息</param>
        /// <returns></returns>
        public static int UptCompanyDept(Model.CompanyDeptModel companyDept)
        {
            int i = -1;
            try
            {
                SqlParameter[] paras = new SqlParameter[]{
                GetSqlParameter("@Dept",SqlDbType.NVarChar,companyDept.Dept),
                GetSqlParameter("@ID",SqlDbType.Int,companyDept.Id)
            };
                i = DBHelper.ExecuteNonQuery("UptCompanyDept", paras, CommandType.StoredProcedure);
            }
            catch (SqlException)
            {
                throw new Exception("修改公司部门信息产生异常!");
            }
            return i;
        }
        /// <summary>
        /// 判断部门是否可以删除
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns></returns>
        public static bool GetDeptRoleCount(int id)
        {
            string sql = "select count(1) from DeptRole where DeptId = @id";
            SqlParameter para = new SqlParameter("@id",SqlDbType.Int);
            para.Value = id;
            object obj=DBHelper.ExecuteScalar(sql,para, CommandType.Text);
            return obj == null ? true : int.Parse(obj.ToString())==0;
        }

        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DelCompanyDept(int id)
        {
            string sql = "delete from CompanyDept where id = @id";
            SqlParameter para = new SqlParameter("@id", id);
            int n = DBHelper.ExecuteNonQuery(sql,para,CommandType.Text);
            return n==1;
        }
        /// <summary>
        /// 部门是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsHaaveCompanyDept(int id)
        {
            string sql = "select count(*) from CompanyDept where id = " + id;
            int n = int.Parse(DBHelper.ExecuteDataTable(sql, CommandType.Text).Rows[0][0].ToString());
            if (n == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ds2012
        /// 判断公司部门是否重名
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="companyID">公司部门编号（为-1表明是新添加的部门）</param>
        /// <returns>true 重名 false 不重名</returns>
        public static bool CheckName(string name, int companyID)
        {
            SqlParameter[] paras = new SqlParameter[]{
                GetSqlParameter("@Dept",SqlDbType.VarChar,name),
                GetSqlParameter("@companyId",SqlDbType.Int,companyID)
            };
            //调用存储过程
            object obj = DBHelper.ExecuteScalar("CheckCompanyDept", paras, CommandType.StoredProcedure);
            return obj != null;
        }

        /// <summary>
        /// ds2012
        /// 根据编号获取公司部门信息
        /// </summary>
        /// <param name="id">公司部门编号</param>
        /// <returns></returns>
        public static CompanyDeptModel GetCompanyDeptsk(int id)
        {
            CompanyDeptModel companyDept = null;
            SqlParameter para = GetSqlParameter("@ID", SqlDbType.Int, id);
            SqlDataReader reader = DBHelper.ExecuteReader("GetCompanyDeptByID", para, CommandType.StoredProcedure);
            if (reader.HasRows)
            {
                reader.Read();
                companyDept = new CompanyDeptModel();
                companyDept.Dept = reader.GetString(0);
            }
            reader.Close();
            return companyDept;
        }

        public static IList<CompanyDeptModel> GetCompanyDepts(string p)
        {
            IList<CompanyDeptModel> list = null;
            if (p == "")
            {
                return null;
            }
            SqlDataReader reader = DBHelper.ExecuteReader("select distinct CompanyDept.id,Dept from CompanyDept right outer join DeptRole on deptRole.deptid=CompanyDept.id where deptRole.id in (" + p + ") order By id", CommandType.Text);
            if (reader.HasRows)
            {
                list = new List<CompanyDeptModel>();
                while (reader.Read())
                {
                    if (reader[0] == DBNull.Value)
                    {
                        continue;
                    }
                    CompanyDeptModel companyDeptModel = new CompanyDeptModel(reader.GetInt32(0));
                    companyDeptModel.Dept = reader.GetString(1);
                    list.Add(companyDeptModel);
                }
            }
            reader.Close();
            return list;
        }
    }
}

