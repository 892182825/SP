using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;

namespace BLL.other.Company
{
    public class ConfusionBLL
    {
        public static void AddCOnfusion(string tableName1,string name1,string tableName2,string name2)
        {
            ConfusionDAL.AddConfusion(tableName1, name1, tableName2, name2);
        }
        public static DataTable GetAll()
        {
            return ConfusionDAL.GetAll();
        }
        public static void del(int id)
        {
            ConfusionDAL.del(id);
        }

        public static bool IsExistsConfusion(string tableName1, string name1, string tableName2, string name2)
        {
            return ConfusionDAL.IsExistsConfusion(tableName1, name1, tableName2, name2);
        }
    }
}
