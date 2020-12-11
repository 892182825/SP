using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Web;

namespace BLL.other.Company
{
    public class DataEmptyBLL
    {
        public static int getDataEmpty()
        {
            return DataEmptyDAL.getDataEmpty();
        }
    }
}
