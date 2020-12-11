using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Model;

namespace DAL
{
    public class StoreOffDAL
    {

        public static int getStoreZX(string storeid)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@storeid", storeid);

            int num = (int)DBHelper.ExecuteScalar("select count(0) from storeinfo where storeid=@storeid", para, CommandType.Text);

            return num;
        }

        public static int getStoreISzx(string storeid)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@storeid", storeid);

            int num = (int)DBHelper.ExecuteScalar("select top 1 storestate from storeinfo where storeid=@storeid", para, CommandType.Text);

            return num;
        }

        public static int getInsertStoreZX(StoreOffModel st)
        {
            SqlParameter[] para = new SqlParameter[6];
            para[0] = new SqlParameter("@storeid", st.Storeid);
            para[1] = new SqlParameter("@zxqishu", st.Zxqishu);
            para[2] = new SqlParameter("@zxdate", st.Zxfate);
            para[3] = new SqlParameter("@offReason", st.OffReason);
            para[4] = new SqlParameter("@Operator", st.OperatorNo);
            para[5] = new SqlParameter("@OperatorName", st.OperatorName);

            int num = DBHelper.ExecuteNonQuery("Storeoff_zx", para, CommandType.StoredProcedure);


            return num;
        }

        public static int getUpdateStoreZX(string storeid, int qishu, int id, DateTime zxdate, string Operator, string OperatorName)
        {
            SqlParameter[] para = new SqlParameter[6];
            para[0] = new SqlParameter("@storeid", storeid);
            para[1] = new SqlParameter("@zxqishu", qishu);
            para[2] = new SqlParameter("@id", id);
            para[3] = new SqlParameter("@zxdate", zxdate);
            para[4] = new SqlParameter("@Operator", Operator);
            para[5] = new SqlParameter("@OperatorName", OperatorName);

            int num = DBHelper.ExecuteNonQuery("Storeoff_fhzx", para, CommandType.StoredProcedure);

            return num;
        }

        public static string getMemberName(string number)
        {
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@number", number);

            string num = DBHelper.ExecuteScalar("select top 1 name from memberinfo where number=@number ", para, CommandType.Text).ToString();

            return num;
        }

    }
}
