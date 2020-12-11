using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DataEmptyDAL
    {
        public static int getDataEmpty()
        {
            int num = DBHelper.ExecuteNonQuery("Initializationdatabase", new SqlParameter[0], CommandType.StoredProcedure);
            return num;
        }
    }
}
