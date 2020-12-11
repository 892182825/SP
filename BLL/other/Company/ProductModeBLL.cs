using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL.other.Company
{
    public class ProductModeBLL
    {
        public static string GetProductNameByID(int productID)
        {
            return ProductDAL.GetProductNameByID(productID);
        }
        public static double GetProductPriceByID(int productID)
        {
            return ProductDAL.GetProductpriceByID(productID);
        }
    }
}
