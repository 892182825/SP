using System;
using System.Collections.Generic;
using System.Collections;
using Model;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


///Add Namespace

/*
 * 创建者：      汪    华
 * 创建时间：    2009-09-23
 */

namespace DAL
{
    public class MemOrderLineDAL
    {
        /// <summary>
        /// 向会员报单底线表中相关插入记录
        /// </summary>
        /// <param name="orderBaseLineMoney">会员订单底线金额</param>
        /// <returns>返回向会员报单底线表中相关插入记录所影响的行数</returns>
        public static int AddMemOrderLine(double orderBaseLineMoney)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@orderBaseLineMoney",SqlDbType.Money)
            };
            sparams[0].Value = orderBaseLineMoney;
            int addCount = 0;
            addCount = DBHelper.ExecuteNonQuery("AddMemOrderLine", sparams, CommandType.StoredProcedure);
            if (addCount == 0)
            {
                return 0;
            }

            else
            {
                return addCount;
            }
        }

        /// <summary>
        /// 更新会员订单底线金额
        /// </summary>
        /// <param name="orderBaseLineMoney">会员订单底线金额</param>
        /// <returns>返回更新会员订单底线金额所影响的行数</returns>
        public static int UpdMemOrderLine(double orderBaseLineMoney)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@orderBaseLineMoney",SqlDbType.Money)
            };
            sparams[0].Value = orderBaseLineMoney;
            int updCount = 0;
            updCount = DBHelper.ExecuteNonQuery("UpdMemOrderLine", sparams, CommandType.StoredProcedure);
            if (updCount == 0)
            {
                return 0;
            }

            else
            {
                return updCount;
            }
        }

        /// <summary>
        /// 获取会员订单底线金额行数
        /// </summary>
        /// <returns>返回获取会员订单底线金额行数</returns>
        public static int GetMemOrderLineCount()
        {
            int getCount = 0;
            getCount = (int)DBHelper.ExecuteScalar("GetMemOrderLineCount", CommandType.StoredProcedure);
            if (getCount == 0)
            {
                return 0;
            }

            else
            {
                return getCount;
            }
        }

        /// <summary>
        /// 获取支付方式——ds2012——tianfeng
        /// </summary>
        /// <returns>返回获取支付方式</returns>
        public static void GetPaymentType(RadioButtonList rbt,int isStore)
        {
            string field = System.Web.HttpContext.Current.Session["LanguageCode"].ToString();
            string strSql = "select p.payId,paymentName from payment p where p.isStore=@isStore and availability=1";
            SqlParameter[] para = {
                                      new SqlParameter ("@isStore",SqlDbType.Int)
                                  };
            para[0].Value = isStore;
            DataTable dt = DBHelper.ExecuteDataTable(strSql,para,CommandType.Text);

            rbt.DataSource = dt;
            rbt.DataTextField = "paymentName";
            rbt.DataValueField = "payId";
            rbt.DataBind();
            if(dt.Rows.Count>0)
                rbt.Items[0].Selected = true;
        }
        /// <summary>
        /// 获取会员订单底线金额
        /// </summary>
        /// <returns>返回会员订单底线金额</returns>
        public static double GetMemOrderLineOrderBaseLine()
        {
            double money = 0;
            money =Convert.ToDouble(DBHelper.ExecuteScalar("GetMemOrderLineOrderBaseLine", CommandType.StoredProcedure));
            if (money == 0)
            {
                return 0;
            }
            else
            {
                return money;
            }
        }

        /// <summary>
        /// 获取支付方式
        /// </summary>
        /// <returns>返回支付方式</returns>
        public static DataTable GetPayment(int isStore)
        {
            string field = System.Web.HttpContext.Current.Session["LanguageCode"].ToString();
           // string strSql = "select t." + field + " as paymentName,p.payID,p.availability,p.id from payment p,T_translation t where t.primarykey = p.id and t.tableName = 'payment' and p.isStore=@isStore";
            string strSql = "select paymentName,p.payID,p.availability,p.id from payment p where p.isStore=@isStore";
            SqlParameter[] para = {
                                      new SqlParameter ("@isStore",SqlDbType.Int)
                                  };
            para[0].Value = isStore;
            DataTable dt = DBHelper.ExecuteDataTable(strSql,para,CommandType.Text);
            return dt;
        }




        /// <summary>
        /// 获取支付方式
        /// </summary>
        /// <returns>返回获取支付方式</returns>
        public static void GetPaymentType2(DropDownList ddl, int isStore)
        {
            string field = System.Web.HttpContext.Current.Session["LanguageCode"].ToString();
             string strSql = "select p.payId,paymentName,p.id from payment p where isStore=@isStore and p.availability=1";
            SqlParameter[] para = {
                                      new SqlParameter ("@isStore",SqlDbType.Int)
                                  };
            para[0].Value = isStore;
            DataTable dt = DBHelper.ExecuteDataTable(strSql, para, CommandType.Text);
            ddl.DataSource = dt;
            ddl.DataTextField = "paymentName";
            ddl.DataValueField = "payId";
            ddl.DataBind();
        }
        /// <summary>
        /// 获取支付方式名称
        /// </summary>
        /// <returns>返回获取支付方式名称</returns>
        public static string GetpaymentName(int payID, int isStore)
        {
            string field = System.Web.HttpContext.Current.Session["LanguageCode"].ToString();
            string strSql = "select t." + field + " as paymentName from payment p,T_translation t where t.primarykey = p.id and t.tableName = 'payment' and isStore=@isStore and p.payID=@payID";
            SqlParameter[] para = {
                                      new SqlParameter ("@isStore",SqlDbType.Int),new SqlParameter ("@payID",SqlDbType.Int)
                                  };
            para[0].Value = isStore;
            para[1].Value = payID;
            object obj = DBHelper.ExecuteScalar(strSql, para, CommandType.Text);
            if (obj != null)
                return obj.ToString();
            else
                return "";
        }
    }
}
