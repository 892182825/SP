using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class ChangeExceptDAL
    {
        /// <summary>
        /// 检查会员编号
        /// </summary>
        public int GetNumber(string number)
        {
            string SQL = "select number from MemberInfo where Number=@Number";
            SqlParameter[] para = 
            {
               new SqlParameter("@Number",number)
            };
            return (int)DBHelper.ExecuteScalar(SQL, para, CommandType.Text);
        }

        /// <summary>
        /// 检查
        /// </summary>
        public int GetOrderID(string number, string orderId)
        {
            string SQL = "select count(1) from MemberOrder where Number=@number and orderId=@orderId";
            SqlParameter[] para = 
            {
                
               new SqlParameter("@number",number),
               new SqlParameter("@orderId",orderId)
            };
            return (int)DBHelper.ExecuteScalar(SQL, para, CommandType.Text);
        }

        /// <summary>
        /// 获取该会员首次注册期数
        /// </summary>
        /// <param name="number"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int GetRegisExce(string number)
        {
            //注册期数
            int registExcept = 0;

            string SQL = "select ExpectNum from  memberInfo where Number=@number";
            SqlParameter[] para = 
            {
                
               new SqlParameter("@number",number)
            };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            if (reader.Read())
            {
                registExcept = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            return registExcept;
        }

        /// <summary>
        /// 获取输入报单号的期数
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="isAgain">是否是重复消费</param>
        /// <returns>期数</returns>
        public int GetOrderExcept(string orderId, bool isAgain)
        {
            string SQL = null;
            int OrderExcept = 0;
            if (!isAgain)
            {
                SQL = "select OrderExpectNum from  MemberOrder where orderId=@orderId and isAgain=0";
            }
            else
            {
                SQL = "select OrderExpectNum from  MemberOrder where orderId=@orderId and isAgain=1";
            }

            SqlParameter[] para = 
            {
                
               new SqlParameter("@orderId",orderId)
            };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            if (reader.Read())
            {
                OrderExcept = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            return OrderExcept;
        }


        /// <summary>
        /// 获取该订单的支付期数
        /// </summary>
        /// <param name="orderId">d订单id</param>
        /// <returns></returns>
        public int GetPayExcept(string orderId)
        {
            int payExcept = 0;
            string SQL = "select PayExpectNum from MemberOrder where orderId=@orderId ";
            SqlParameter[] para = 
            {
               new SqlParameter("@orderId",orderId)
            };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            if (reader.Read())
            {
                payExcept = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            return payExcept;
        }

        /// <summary>
        /// 获取报单期数
        /// </summary>
        /// <param name="orderId">报单编号</param>
        /// <returns></returns>
        public int GetOrderExcept(string orderId)
        {
            int payExcept = 0;
            string SQL = "select OrderExpectnum from MemberOrder where orderId=@orderId ";
            SqlParameter[] para = 
            {
               new SqlParameter("@orderId",orderId)
            };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            if (reader.Read())
            {
                payExcept = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            return payExcept;
        }


        /// <summary>
        /// 查血该单是否是重复消费
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns>是否重复消费</returns>
        public int GetOrderIsAgain(string orderId)
        {
            int isAgain = 0;
            string SQL = "select isAgain from  memeberOrder where orderID=@orderId";
            SqlParameter[] para = 
            {
               new SqlParameter("@orderId",orderId)
            };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            if (reader.Read())
            {
                isAgain = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            return isAgain;

        }

        /// <summary>
        /// 判断该订单是否是首次报单 memberInfo表中的字段orderId表示首次报单id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public string GetFirstOrder(string orderId)
        {
            string orderId2 = null;
            string SQL = "select orderId from memberInfo where orderId=@orderId";
            SqlParameter[] para = 
            {
               new SqlParameter("@orderId",orderId)
            };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            if (reader.Read())
            {
                orderId2 = Convert.ToString(reader[0]);
            }
            reader.Close();
            return orderId2;
        }

        /// <summary>
        /// 获取报单是否是首次报单
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public static int SelectIsAgain(string OrderId)
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("select IsAgain from memberorder where orderid=@orderid", new SqlParameter[] { new SqlParameter("@orderid", OrderId) }, CommandType.Text));
        }

        /// <summary>
        /// 获取推荐人注册期数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int SelectTJQiShu(string number)
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("select expectnum from memberinfo where number=(select direct from memberinfo where number=@num)", new SqlParameter[] { new SqlParameter("@num", number) }, CommandType.Text));
        }

        /// <summary>
        /// 获取安置人注册期数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int SelectAZQiShu(string number)
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("select expectnum from memberinfo where number=(select placement from memberinfo where number=@num)", new SqlParameter[] { new SqlParameter("@num", number) }, CommandType.Text));
        }

        /// <summary>
        /// 更改期数
        /// </summary>
        /// <param name="except"></param>
        /// <param name="?"></param>
        public int UptOrderExcept(int except, string orderId, string user, string ip, string updateExpectReason)
        {
            SqlParameter[] para = 
            {
              new SqlParameter("@orderID",orderId),
              new SqlParameter("@ExceptNum",except),
              new SqlParameter("@User",user),
              new SqlParameter("@IP",ip),
              new SqlParameter("@updateExpectReason",updateExpectReason)
            };
            return DBHelper.ExecuteNonQuery("ChangeOrderExcept", para, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 得到所有期数的集合
        /// </summary>
        /// <returns></returns>
        public List<int> GetExceptList()
        {
            List<int> exceptList = new List<int>();
            string SQL = "select ExpectNum from Config ";
            SqlDataReader reader = DBHelper.ExecuteReader(SQL);
            while (reader.Read())
            {
                exceptList.Add(Convert.ToInt32(reader[0]));
            }
            reader.Close();
            return exceptList;
        }

        /// <summary>
        /// 获取该订单支付状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int IsPayOreder(string orderId)
        {
            int DefrayState = 0;
            string SQL = "select DefrayState from MemberOrder where orderId=@orderId";
            SqlParameter[] para = 
            {
               new SqlParameter("@orderId",orderId)
            };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            if (reader.Read())
            {
                DefrayState = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            return DefrayState;
        }

        /// <summary>
        /// 获取输入会员编号和单据号是否一致
        /// </summary>
        /// <param name="orderid">报单号</param>
        /// <param name="number">会员编号</param>
        /// <returns></returns>
        public static int SelectMemberAndOrder(string orderid, string number)
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("select count(0) from memberorder where orderid=@orderid and number=@num ",new SqlParameter[]{new SqlParameter("@orderid",orderid),new SqlParameter("@num",number)},CommandType.Text));
        }
    }
}
