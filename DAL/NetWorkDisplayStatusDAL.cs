using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class NetWorkDisplayStatusDAL
    {

        public static int SetNetWorkDisplayStatus(string status)
        {
            //string[] i = status.Split(',');
            //int i = 0;
            //i+=UptNetWorkDisplayStatus(int.Parse(i[0]), "xgfenshu");
            //i+=UptNetWorkDisplayStatus(int.Parse(i[1]), "xwfenshu");
            //i+=UptNetWorkDisplayStatus(int.Parse(i[2]), "unzgfenshu");
            //i+=UptNetWorkDisplayStatus(int.Parse(i[3]), "unzwfenshu");
            //i+=UptNetWorkDisplayStatus(int.Parse(i[4]), "xwrenshu");
            //i+=UptNetWorkDisplayStatus(int.Parse(i[5]), "zwrenshu");
            return 1;
        }

        public static int UptNetWorkDisplayStatus(int status,string columnName)
        {
            string sql = "update NetWorkDisplayStatus set flag=@status where field=@columnName";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@columnName", columnName), new SqlParameter("@status",status) };
            return DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
        }
    }
}
