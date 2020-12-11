using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL.other 
{
    public class IndexBLL
    {
        public static bool CheckLogin(string type, string name, string pwd)
        {
            return IndexDAL.CheckLogin(type, name, pwd);
        }

        public static string UplostLogin(string bianhao, string type)
        {
            return IndexDAL.UplostLogin(bianhao, type);
        }

        public static int insertLoginLog(string number, string pass, string leixing, DateTime logindate, string loginIP, int iscg)
        {
            return IndexDAL.insertLoginLog(number, pass, leixing, logindate, loginIP, iscg);
        }
    }
}
