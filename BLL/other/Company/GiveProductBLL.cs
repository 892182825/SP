using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DAL;
using Model;

namespace BLL.other.Company
{
    public class GiveProductBLL
    {
        public static bool CreateGiveProducts(SqlTransaction tran, ArrayList giveProductList, int setGivePVID)
        {
            return GiveProductDAL.CreateGiveProducts(tran, giveProductList, setGivePVID) > 0 ? true : false;
        }
        public static int AddSetGivePV(SqlTransaction tran, SetGivePVModel setGiveModel)
        {
            return GiveProductDAL.AddSetGivePV(tran, setGiveModel);
        }
        /// <summary>
        /// 获取最后一条结束PV值
        /// </summary>
        /// <returns></returns>
        public static bool GetLastEndPV(double totalpvStart)
        {
            return GiveProductDAL.GetLastEndPV(totalpvStart);
        }

        public static string GetTableZp(double pv)
        {
            StringBuilder sb = new StringBuilder();
           DataTable dt= GiveProductDAL.GetDtBYPv(pv);

           if (dt!=null)
           {
               if (dt.Rows.Count>0)
               {
                   for (int i = 0; i < dt.Rows.Count; i++)
		           {
		              sb.Append("<tr>");
                      sb.Append("<td class='shoptab-img'><img src='"+ FormatURL(dt.Rows[i]["ProductID"])+"' width='40' height='40' /></td>");
                      sb.Append("<td >" + dt.Rows[i]["ProductCode"] + "</td>");
                      sb.Append("<td >" + dt.Rows[i]["ProductName"] + "</td>");
                      sb.Append("<td >" + dt.Rows[i]["price"] + "</td>");
                      sb.Append("<td >" + dt.Rows[i]["pv"] + "</td>");
                      sb.Append("<td >" + dt.Rows[i]["productQuantity"] + "</td>");
                      sb.Append("<td >" + dt.Rows[i]["totalPrice"] + "</td>");
                      sb.Append("<td >" + dt.Rows[i]["totalPv"] + "</td>");
                      sb.Append("</tr>");
		           }
                   
               }
               else
               {
                   sb.Append("<tr><td colspan='8'>&nbsp;&nbsp;没有赠送产品</td></tr>");
               }
           }
           else
           {
               sb.Append("<tr><td colspan='8'>&nbsp;&nbsp;没有赠送产品</td></tr>");
           }

           return sb.ToString();
        }

        protected static string FormatURL(object strArgument)
        {
            string result = "../ReadImage.aspx?ProductID=" + strArgument.ToString();
            if (result == "" || result == null)
            {
                result = "";
            }
            return result;
        }
        /// <summary>
        /// 删除赠品设置记录
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DelSPVProduct(int id)
        {
            using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (GiveProductDAL.DelGiveProductBySID(tran, id) <= 0)
                    {
                        tran.Rollback();
                        return false;
                    }
                    if (GiveProductDAL.DelSetSetGivePVByID(tran, id) <= 0)
                    {
                        tran.Rollback();
                        return false;
                    }
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
            
            return true;
        }
    }
}
