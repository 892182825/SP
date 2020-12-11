using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using Model;
using System.Data;
using System.Data.SqlClient;

/*
 *Creator:      WangHua
 *CreateDate:   2010-01-25 
 *FinishDate:   2010-01-25
 *Function：     Convert time
 */

namespace DAL
{
    public class WorldTimeDAL
    {
        /// <summary>
        /// Get the TimeDifference by loingip
        /// </summary>
        /// <param name="loginIP">LoginIP</param>
        /// <returns>Return TimeDifference</returns>
        public static int ConvertAddHoursByIp(string loginIP)
        { 
            long ip=0;             
            string[] strIP =  loginIP.Split('.');
            long[] ipValue = new long[4];
          
            for (int i = 0; i < strIP.Length; i++)
            {
                ipValue[i] = Convert.ToInt64(strIP[i]);
            }

            ip = ipValue[0] * 16777216 + ipValue[1] * 65536 + ipValue[2] * 256 + ipValue[3] - 1;

            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@ip",SqlDbType.Float)                
            };
            sparams[0].Value = ip;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetTimeDifferenceByIP", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Get the display time type        
        /// </summary>
        /// <param name="identityType">IdentityType</param>
        /// <param name="identityNumber">IdentityNumber</param>
        /// <returns>Return display time type</returns>
        public static int GetDiplayTimeType(int identityType,string identityNumber)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@identityType",SqlDbType.Int),
                new SqlParameter("@identityNumber",SqlDbType.NVarChar,100)
            };
            sparams[0].Value = identityType;
            sparams[1].Value = identityNumber;
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetDiplayTimeType",sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Update display time type by what you choice manner
        /// </summary>
        /// <param name="identityType">Identity Type</param>
        /// <param name="identityDisplayTimeType">Identity Display Time type</param>
        /// <param name="identityNumber">IdentityNumber</param>
        /// <returns>Return affected rows</returns>
        public static int UpdDisplayTimeType(int identityType, int identityDisplayTimeType, string identityNumber)
        {
            SqlParameter[] sparams=new SqlParameter[]
            {
                new SqlParameter("@identityType",SqlDbType.Int),
                new SqlParameter("@identityDisplayTimeType",SqlDbType.Int),
                new SqlParameter("@identityNumber",SqlDbType.NVarChar,100)
            };
            sparams[0].Value = identityType;
            sparams[1].Value = identityDisplayTimeType;
            sparams[2].Value = identityNumber;

            return DBHelper.ExecuteNonQuery("UpdDisplayTimeType",sparams,CommandType.StoredProcedure);
        }  

        /// <summary>
        /// Get the TimeDifference by CountryName of the store
        /// </summary>
        /// <param name="storeNumber">Store Number</param>
        /// <returns>Return TimeDifference</returns>
        public static int ConvertAddHoursByCountry_Store(string storeNumber)
        {
            SqlParameter[] sparams=new SqlParameter[]
            {
                new SqlParameter("@storeNumber",SqlDbType.NVarChar,50)
            };
            sparams[0].Value=storeNumber;
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetTimeDifferenceByCountry_Store",sparams,CommandType.StoredProcedure));
        }

        /// <summary>
        /// Get the TimeDifference by CountryName of the member
        /// </summary>
        /// <param name="memberNumber">Member Number</param>
        /// <returns>Return TimeDifference</returns>
        public static int ConvertAddHoursByCountry_Member(string memberNumber)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@memberNumber",SqlDbType.NVarChar,50)
            };
            sparams[0].Value = memberNumber;
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetTimeDifferenceByCountry_Member", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Get the TimeDifference by CountryName of the branch
        /// </summary>
        /// <param name="memberNumber">Branch Number</param>
        /// <returns>Return TimeDifference</returns>
        public static int ConvertAddHoursByCountry_Branch(string branchNumber)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@branchNumber",SqlDbType.NVarChar,50)                
            };
            sparams[0].Value = branchNumber;
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetTimeDifferenceByCountry_Branch", sparams, CommandType.StoredProcedure));
        }        
    }
}
