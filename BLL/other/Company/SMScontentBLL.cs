using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data.SqlClient;

namespace BLL.other.Company
{
    public class SMScontentBLL
    {

        public static int insertSMSContent(int pid,int isfold,string productname,string countrycode,string bianhao)
        {
           return  SMScontentDAL.insertSMSContent(pid, isfold, productname, countrycode, bianhao);
        }


        public static string getSMScountrycode(int id)
        {
            return SMScontentDAL.getSMScountrycode(id);
        }

        public static string getSMSproductName(int id)
        {
            return SMScontentDAL.getSMSproductName(id);
        }

        public static int updateSMSproductName(int id,string proName)
        {
            return SMScontentDAL.updateSMSproductName(id, proName);
        }

        public static int deleteSMScontent(SqlTransaction tran, int id)
        {
            return SMScontentDAL.deleteSMScontent(tran, id);
        }

    }


}
