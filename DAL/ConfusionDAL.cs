using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ConfusionDAL
    {
        public static void AddConfusion(string tableName1, string name1, string tableName2, string name2)
        {
            string sql = "insert into Confusion values(@num,@num1,@num2,@num3)";
            SqlParameter[] spa = new SqlParameter[] { 
                new SqlParameter("@num",SqlDbType.NVarChar,50),
                new SqlParameter("@num1",SqlDbType.NVarChar,50),
                new SqlParameter("@num2",SqlDbType.NVarChar,50),
                new SqlParameter("@num3",SqlDbType.NVarChar,50)
            };
            spa[0].Value = tableName1;
            spa[1].Value = name1;
            spa[2].Value = tableName2;
            spa[3].Value = name2;
            DBHelper.ExecuteNonQuery(sql,spa,CommandType.Text);
        }

        public static System.Data.DataTable GetAll()
        {
            string sql = "select * from Confusion";
            return DBHelper.ExecuteDataTable(sql);
        }
        public static void del(int id)
        {
            string sql = "delete Confusion where id=@num";
            SqlParameter spa = new SqlParameter("@num",SqlDbType.Int);
            spa.Value = id;
            DBHelper.ExecuteNonQuery(sql,spa,CommandType.Text);
        }

        public static bool IsExistsConfusion(string tableName1, string name1, string tableName2, string name2)
        {
            string sql = "select count(1) from Confusion where tableName1=@num and name1=@num1 and tableName2=@num2 and name2=@num3";
            SqlParameter[] spa = new SqlParameter[] { 
                new SqlParameter("@num",SqlDbType.NVarChar,50),
                new SqlParameter("@num1",SqlDbType.NVarChar,50),
                new SqlParameter("@num2",SqlDbType.NVarChar,50),
                new SqlParameter("@num3",SqlDbType.NVarChar,50)
            };
            spa[0].Value = tableName1;
            spa[1].Value = name1;
            spa[2].Value = tableName2;
            spa[3].Value = name2;
            int count = Convert.ToInt32(DBHelper.ExecuteScalar(sql,spa,CommandType.Text));
            if (count > 0)
                return true;
            else
                return false;
        }
    }
}
