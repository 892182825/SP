using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class SMScontentDAL
    {

        public static int insertSMSContent(int pid, int isfold, string productname, string countrycode, string bianhao)
        {
            string strsql = "insert into SMScontent(pid,isfold,productname,countrycode,bianhao) values(@PID,@IsFold,@ProductName,@CountryCode,@Bianhao)";

            SqlParameter[] para = 
            { 
              new SqlParameter("@PID",pid),
              new SqlParameter("@IsFold",isfold),
              new SqlParameter("@ProductName",productname),
              new SqlParameter("@CountryCode",countrycode),
              new SqlParameter("@Bianhao",bianhao)
            };


            return DBHelper.ExecuteNonQuery(strsql, para, CommandType.Text);
        }

        public static string getSMScountrycode(int id)
        {
            string sql = "select countrycode from smscontent where productid=@num";
            SqlParameter spa = new SqlParameter("@num",SqlDbType.Int);
            spa.Value = id;
            return DBHelper.ExecuteScalar(sql,spa,CommandType.Text).ToString();
        }

        public static string getSMSproductName(int id)
        {
            string sql = "select productname from smscontent where productid=@num";
            SqlParameter spa = new SqlParameter("@num", SqlDbType.Int);
            spa.Value = id;
            return DBHelper.ExecuteScalar(sql, spa, CommandType.Text).ToString();
        }

        public static int updateSMSproductName(int id,string proName)
        {
            string sql = "update smscontent set productName=@num where productid=@num1";
            SqlParameter[] spa1 = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.NVarChar, 2000), 
                new SqlParameter("@num1", SqlDbType.Int)
                
              };
            spa1[0].Value = proName;
            spa1[1].Value = id;
            return DBHelper.ExecuteNonQuery(sql,spa1,CommandType.Text);
        }

        public static int deleteSMScontent(SqlTransaction tran, int id)
        {
            SqlParameter[] para = 
            {
                new SqlParameter("productid",id)
            };

            return DBHelper.ExecuteNonQuery(tran,"deleteSMScontent", para, CommandType.StoredProcedure);
        }

    }
}
