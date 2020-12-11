using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using Model;


namespace DAL
{
    public class PaymentSetDal
    {

 

        /// <summary>
        /// 删除指定产品颜色信息
        /// </summary>
        /// <param name="productColorID">产品颜色ID</param>
        /// <returns>返回删除指定产品信息所影响的行数</returns>
        public static int DelPaymentID(int pID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@pID",SqlDbType.Int)
            };
            sparams[0].Value = pID;
            int delCount = 0;
            string strSql = "delete from payment where id=@pID";
            delCount = DBHelper.ExecuteNonQuery(strSql, sparams, CommandType.Text);

            return delCount;
        }

        /// <summary>
        /// 修改指定产品颜色信息
        /// </summary>
        /// <param name="productColor">产品颜色模型</param>
        /// <returns>返回修改指定产品颜色信息所影响的行数</returns>
        public static int UpdPaymentByID(string paymentName, int isStore, int pID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@paymentName",SqlDbType.NVarChar,50),
                new SqlParameter("@isStore",SqlDbType.Int),
                new SqlParameter("@pID",SqlDbType.Int)
            };
            sparams[0].Value = paymentName;
            sparams[1].Value = isStore;
            sparams[2].Value = pID;
            int updCount = 0;
            string strSql = @"Update payment set paymentname=@paymentName,isStore=@isStore where id=@pID";
            updCount = DBHelper.ExecuteNonQuery(strSql, sparams, CommandType.Text);
            if (updCount == 0)
            {
                return updCount;
            }

            else
            {
                return updCount;
            }
        }
    }
}