using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Web;
using System.Data.Common;

  
namespace DAL
{
    /// <summary>
    /// DBHelper 的摘要说明
    /// </summary>
    public class DBHelper
    {
        public DBHelper()
        {

        }
        public static readonly string connString =  System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString ;

        /// <summary>
        /// 返回SqlServer连接接口
        /// </summary>
        /// <returns></returns>
        public static SqlConnection SqlCon()
        {
            SqlConnection con = new SqlConnection(connString);
            return con;
        }

        public static int ExecuteNonQuery(SqlTransaction tran, string cmdText, SqlParameter[] parms, CommandType cmdtype)
        {
            int retVal = 0;

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Connection = tran.Connection;
            cmd.Transaction = tran;
            cmd.CommandType = cmdtype;
            cmd.CommandTimeout = 600;

            if (parms != null)
            {
                //添加参数
                foreach (SqlParameter parm in parms)
                {
                    cmd.Parameters.Add(parm);
                }
            }

            retVal = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            return retVal;
        }

        /// <summary>
        /// 2005.11.21 
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlTransaction tran, string cmdText)
        {
            int retVal = 0;

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Connection = tran.Connection;
            cmd.Transaction = tran;
            cmd.CommandTimeout = 600;

            retVal = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return retVal;
        }


        public static int ExecuteNonQuery(string cmdText, SqlParameter[] parms, CommandType cmdtype)
        {
            int retVal;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = cmdtype;
                cmd.Parameters.Clear();
                if (parms != null)
                {
                    //添加参数
                    foreach (SqlParameter spa in parms)
                    {
                        cmd.Parameters.Add(spa);
                    }
                    // cmd.Parameters.AddRange(parms);
                }

                conn.Open();
                retVal = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
            }

            return retVal;
        }

        public static int ExecuteNonQuery(string cmdText, SqlParameter parms, CommandType cmdtype)
        {
            int retVal;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = cmdtype;
                cmd.Parameters.Clear();
                if (parms != null)
                {
                    //添加参数
                    cmd.Parameters.Add(parms);
                }

                conn.Open();
                retVal = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
            }

            return retVal;
        }


        public static int ExecuteNonQuery(string cmdText, CommandType cmdtype)
        {
            int retVal;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = cmdtype;
                cmd.Parameters.Clear();
                conn.Open();
                retVal = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
            }

            return retVal;
        }


        public static int ExecuteNonQuery(string cmdText)
        {
            int retVal;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.Parameters.Clear();
                retVal = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
            }

            return retVal;
        }


        public static object ExecuteScalar(SqlTransaction tran, string cmdText, SqlParameter[] parms, CommandType cmdtype)
        {
            object retVal;

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Connection = tran.Connection;
            cmd.Transaction = tran;
            cmd.CommandType = cmdtype;
            cmd.CommandTimeout = 600;
            cmd.Parameters.Clear();
            if (parms != null)
            {
                //添加参数
                cmd.Parameters.AddRange(parms);
            }

            retVal = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return retVal;
        }

        /// <summary>
        /// 空参数方法
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdtype"></param>
        /// <returns></returns>
        public static object ExecuteScalar(SqlTransaction tran, string cmdText, CommandType cmdtype)
        {
            object retVal;

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Connection = tran.Connection;
            cmd.Transaction = tran;
            cmd.CommandType = cmdtype;
            cmd.CommandTimeout = 600;
            retVal = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return retVal;
        }

        public static object ExecuteScalar(SqlTransaction tran, string cmdText, SqlParameter parms, CommandType cmdtype)
        {
            object retVal;

            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Connection = tran.Connection;
            cmd.Transaction = tran;
            cmd.CommandType = cmdtype;
            cmd.CommandTimeout = 600;
            cmd.Parameters.Clear();

            if (parms != null)
            {
                //添加参数
                cmd.Parameters.Add(parms);
            }

            retVal = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return retVal;
        }


        public static object ExecuteScalar(string cmdText, SqlParameter[] parms, CommandType cmdtype)
        {
            object retVal;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = cmdtype;
                cmd.Parameters.Clear();
                if (parms != null)
                {
                    //添加参数                                                                                      
                    foreach (SqlParameter parm in parms)
                    {
                        cmd.Parameters.Add(parm);
                    }
                }
                conn.Open();
                retVal = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                conn.Close();
            }

            return retVal;
        }

        public static object ExecuteScalar(string cmdText, SqlParameter parm, CommandType cmdtype)
        {
            object retVal;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = cmdtype;
                cmd.Parameters.Clear();
                if (parm != null)
                {
                    //添加参数
                    cmd.Parameters.Add(parm);
                }

                conn.Open();
                retVal = cmd.ExecuteScalar();
                conn.Close();
            }

            return retVal;
        }


        public static object ExecuteScalar(string cmdText, CommandType cmdtype)
        {
            object retVal;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = cmdtype;

                conn.Open();
                retVal = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                conn.Close();
            }

            return retVal;
        }


        public static object ExecuteScalar(string cmdText)
        {
            object retVal;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = CommandType.Text;

                conn.Open();
                retVal = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                conn.Close();
            }

            return retVal;
        }


        //返回一个数据读取器
        public static SqlDataReader ExecuteReader(string cmdText, SqlParameter[] parms, CommandType cmdtype)
        {
            SqlDataReader reader;

            SqlConnection conn = new SqlConnection(connString);

            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = cmdtype;

            if (parms != null)
            {
                //添加参数
                foreach (SqlParameter parm in parms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
            conn.Open();
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return reader;
        }

        public static SqlDataReader ExecuteReader(string cmdText, SqlParameter parms, CommandType cmdtype)
        {
            SqlDataReader reader;

            SqlConnection conn = new SqlConnection(connString);

            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = cmdtype;

            if (parms != null)
            {
                //添加参数
                cmd.Parameters.Add(parms);
            }

            conn.Open();
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return reader;
        }


        public static SqlDataReader ExecuteReader(string cmdText, CommandType cmdtype)
        {
            SqlDataReader reader;

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = cmdtype;

            conn.Open();
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return reader;
        }


        public static SqlDataReader ExecuteReader(string cmdText)
        {
            SqlDataReader reader;

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.Text;

            conn.Open();
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return reader;
        }


        public static SqlDataReader ExecuteReader(SqlTransaction tran, string cmdText, SqlParameter[] parms, CommandType cmdtype)
        {

            SqlDataReader reader;
            SqlCommand cmd = new SqlCommand(cmdText);
            cmd.Connection = tran.Connection;
            cmd.Transaction = tran;
            cmd.CommandType = cmdtype;
            cmd.CommandTimeout = 600;

            if (parms != null)
            {
                //添加参数
                foreach (SqlParameter parm in parms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return reader;
        }
        

        public static DataTable ExecuteDataTable(string cmdText, SqlParameter[] parms, CommandType cmdtype)
        {
            DataTable dt = new DataTable();
           
            SqlConnection conn = new SqlConnection(connString);
            SqlDataAdapter apt = new SqlDataAdapter(cmdText, conn);
            apt.SelectCommand.CommandType = cmdtype;

            if (parms != null)
            {
                //添加参数
                foreach (SqlParameter parm in parms)
                {
                    apt.SelectCommand.Parameters.Add(parm);
                }
            }

            apt.Fill(dt);

            return dt;
        }

        public static DataTable ExecuteDataTable(SqlTransaction tran, string cmdText, SqlParameter[] parms, CommandType cmdtype)
        {
            DataTable dt = new DataTable();


            SqlConnection conn = tran.Connection;//new SqlConnection(connString);
            SqlDataAdapter apt = new SqlDataAdapter(cmdText, conn);
            apt.SelectCommand.CommandType = cmdtype;
            apt.SelectCommand.Transaction = tran;
            if (parms != null)
            {
                //添加参数
                foreach (SqlParameter parm in parms)
                {
                    apt.SelectCommand.Parameters.Add(parm);
                }
            }

            apt.Fill(dt);

            return dt;
        }

        public static SqlDataAdapter ExecuteDataAdapter(string cmdText, CommandType cmdtype)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlDataAdapter da = new SqlDataAdapter(cmdText, conn);
            da.SelectCommand.CommandType = cmdtype;
            return da;
        }

        public static DataTable ExecuteDataTable(string cmdText, CommandType cmdtype)
        {
            DataTable dt = new DataTable();


            SqlConnection conn = new SqlConnection(connString);
            SqlDataAdapter apt = new SqlDataAdapter(cmdText, conn);
            apt.SelectCommand.CommandType = cmdtype;
            apt.Fill(dt);

            return dt;
        }


        public static DataTable ExecuteDataTable(string cmdText)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(connString);
            SqlDataAdapter apt = new SqlDataAdapter(cmdText, conn);
            apt.SelectCommand.CommandType = CommandType.Text;
            apt.Fill(dt);

            return dt;
        }

        public static DataTable ExecuteDataTable(SqlTransaction tran, string cmdText)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = tran.Connection;
            SqlDataAdapter apt = new SqlDataAdapter(cmdText, conn);
            apt.SelectCommand.CommandType = CommandType.Text;
            apt.SelectCommand.Transaction = tran;
            apt.Fill(dt);

            return dt;
        }
        public static SqlParameter GetSqlParameter(string paraName, SqlDbType sqlDbType, SqlParameter para, object obj)
        {
            para = new SqlParameter(paraName, sqlDbType);
            para.Value = obj;
            return para;
        }

        public static SqlParameter GetSqlParameter(string paramName, SqlDbType sqlDbType, object obj)
        {
            SqlParameter para = new SqlParameter(paramName, sqlDbType);
            para.Value = obj;
            return para;
        }


        public static object[] ExecuteNonQuery(string sql, CommandType type, SqlParameter[] inparam, SqlParameter[] outparam)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.CommandType = type;

                if (outparam != null)
                {
                    for (int i = 0; i < outparam.Length; i++)
                    {
                        cmd.Parameters.Add(outparam[i]);

                        cmd.Parameters[i].Direction = ParameterDirection.Output;
                    }
                }

                if (inparam != null)
                {
                    for (int i = 0; i < inparam.Length; i++)
                    {
                        cmd.Parameters.Add(inparam[i]);
                    }
                }

                cmd.ExecuteNonQuery();

                object[] obj = new object[outparam.Length];

                for (int i = 0; i < outparam.Length; i++)
                {
                    obj[i] = cmd.Parameters[i].Value;
                }

                cmd.Dispose();
                con.Close();

                return obj;
            }
        }

        //internal static void ExecuteNonQuery(string sql, SqlParameter[] para, CommandType commandType)
        //{
        //    throw new NotImplementedException();
        //}
        public static DataSet ExecuteDataSet(string cmdText, SqlParameter[] parms, CommandType cmdtype)
        {
            DataSet ds = new DataSet();

            SqlConnection conn = new SqlConnection(connString);
            SqlDataAdapter apt = new SqlDataAdapter(cmdText, conn);

            apt.SelectCommand.CommandType = cmdtype;
            apt.SelectCommand.CommandTimeout = 600;

            if (parms != null)
            {
                //添加参数
                foreach (SqlParameter parm in parms)
                {
                    apt.SelectCommand.Parameters.Add(parm);
                }
            }

            apt.Fill(ds);
            conn.Close();
            conn.Dispose();
            return ds;
        }

    }



}
