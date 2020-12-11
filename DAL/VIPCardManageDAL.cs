using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{
    public class VIPCardManageDAL
    {
        /// <summary>
        /// 获得最大值
        /// </summary>
        /// <returns></returns>
        public int MaxCard()
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("SELECT ISNULL(MAX(EndCard),0) FROM VIPcardrange"));
        }
        /// <summary>
        /// 开始/结束 编号
        /// </summary>
        /// <param name="begincard"></param>
        /// <param name="endcard"></param>
        /// <returns></returns>
        public static int BetweenCard(int begincard, int endcard)
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("SELECT COUNT(*) FROM MemberInfo WHERE VIPCard>=" + begincard + " AND VIPCard<=" + endcard));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetCardRange(string condition, string mark)
        {
            List<VIPCardRangeModel> list = new List<VIPCardRangeModel>();
            string sql = "select id,RangeID,StoreID,BeginCard,EndCard,inuse,CASE inuse WHEN 1 THEN '已分配' ELSE '未分配' END AS _inuse from VIPCardRange where 1=1";
            if (mark == "LIKE")
            {
                sql = sql + "  and  StoreID  like '%" + condition + "%'";
            }
            else
            {
                sql = sql + "  and  StoreID = '" + condition + "'";
            }
            return DBHelper.ExecuteDataTable(sql, CommandType.Text);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static int Addvipcard(VIPCardRangeModel mode)
        {
            SqlParameter[] parameter ={  
                new SqlParameter("@rangeID",SqlDbType.Int),
                new SqlParameter("@storeid",SqlDbType.VarChar,40),
                new SqlParameter("@begincard",SqlDbType.Int),
                new SqlParameter("@endcard",SqlDbType.Int),
                new SqlParameter("@inuse",SqlDbType.Int)
            };
            parameter[0].Value = mode.RangeID;
            parameter[1].Value = mode.StoreID;
            parameter[2].Value = mode.BeginCard;
            parameter[3].Value = mode.EndCard;
            parameter[4].Value = mode.InUse;
            return DBHelper.ExecuteNonQuery("Addvipcard", parameter, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RangeID"></param>
        public static void Uptvipcard(int RangeID)
        {
            SqlParameter[] parameter ={  
                new SqlParameter("@rangeID",SqlDbType.Int)
                                      };
            parameter[0].Value = RangeID;
            DBHelper.ExecuteNonQuery("Uptvipcard", parameter, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reangeID"></param>
        /// <param name="storeID"></param>
        public static void Uptvipcardstore(int reangeID, string storeID)
        {
            SqlParameter[] parameter ={  
                new SqlParameter("@rangeID",SqlDbType.Int),
                 new SqlParameter("@StoreID",SqlDbType.Int)
                                      };
            parameter[0].Value = reangeID;
            parameter[1].Value = storeID;
            DBHelper.ExecuteNonQuery("Uptvipcard", parameter, CommandType.StoredProcedure);
        }
    }
}
