using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using System.Web;
using Model;
using DAL;

//I am reading the the newspaper

/*
 *Creator:      WangHua
 *CreateDate:   2010-01-25 
 *Function：     Convert time
 */

namespace BLL.other.Company
{
    public class WordlTimeBLL
    {
        /// <summary>
        /// Convert time --ds2012--www-b874dce8700——tianfeng
        /// </summary>
        /// <returns>Return add hours</returns>
        public static int ConvertAddHours()
        {
            return 8;
            int hours = 0;
            if (HttpContext.Current.Session["UserType"] == null)
            {
                HttpContext.Current.Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["IP_Country_UserType"].ToString());
                return hours;
            }

            else
            {
                //Get the display time type
                int displayTimeType = 0;

                //Company
                if (HttpContext.Current.Session["UserType"].ToString() == "1")
                {
                    hours = WorldTimeDAL.ConvertAddHoursByIp(CommonClass.CommonDataBLL.OperateIP);
                }

                //Store
                if (HttpContext.Current.Session["UserType"].ToString() == "2")
                {
                    displayTimeType = GetDiplayTimeType(2, Convert.ToString(HttpContext.Current.Session["Store"]));
                    //0 stand for display time by IP,
                    if (displayTimeType == 0)
                    {
                        hours = WorldTimeDAL.ConvertAddHoursByIp(CommonClass.CommonDataBLL.OperateIP);
                    }

                    //1 Stand for display time by country
                    if (displayTimeType == 1)
                    {
                        hours = WorldTimeDAL.ConvertAddHoursByCountry_Store(HttpContext.Current.Session["Store"].ToString());
                    }
                }

                //Member
                if (HttpContext.Current.Session["UserType"].ToString() == "3")
                {
                    displayTimeType = GetDiplayTimeType(3, Convert.ToString(HttpContext.Current.Session["Member"]));
                    if (displayTimeType == 0)
                    {
                        hours = WorldTimeDAL.ConvertAddHoursByIp(CommonClass.CommonDataBLL.OperateIP);
                    }

                    if (displayTimeType == 1)
                    {
                        hours = WorldTimeDAL.ConvertAddHoursByCountry_Store(HttpContext.Current.Session["Member"].ToString());
                    }
                }

                //Branch Company
                if ((HttpContext.Current.Session["UserType"].ToString() == "4"))
                {
                    displayTimeType = GetDiplayTimeType(4, Convert.ToString(HttpContext.Current.Session["Branch"]));
                    if (displayTimeType == 0)
                    {
                        hours = WorldTimeDAL.ConvertAddHoursByIp(CommonClass.CommonDataBLL.OperateIP);
                    }

                    if (displayTimeType == 1)
                    {
                        hours = WorldTimeDAL.ConvertAddHoursByCountry_Branch(HttpContext.Current.Session["Branch"].ToString());
                    }
                }

                return hours;
            }
        }

        /// <summary>
        /// ds2012
        /// Update display time type by what you choice manner
        /// </summary>
        /// <param name="identityType">Identity Type</param>
        /// <param name="identityDisplayTimeType">Identity Display Time type</param>
        /// <param name="identityNumber">IdentityNumber</param>
        /// <returns>Return affected rows</returns>
        public static int UpdDisplayTimeType(int identityType, int identityDisplayTimeType, string identityNumber)
        {
            return WorldTimeDAL.UpdDisplayTimeType(identityType, identityDisplayTimeType, identityNumber);
        }

        /// <summary>
        /// Get the display time type        
        /// </summary>
        /// <param name="identityType">IdentityType</param>
        /// <param name="identityNumber">IdentityNumber</param>
        /// <returns>Return display time type</returns>
        public static int GetDiplayTimeType(int identityType, string identityNumber)
        {
            return WorldTimeDAL.GetDiplayTimeType(identityType, identityNumber);
        }
    }
}
