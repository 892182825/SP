using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

namespace BLL.CommonClass
{
    public class Login
    {
        public static DateTime MaxDateTime = Convert.ToDateTime("2980-09-15 05:20:10");
        #region 禁止登陆
        /// <summary>
        /// 禁止登陆
        /// </summary>
        /// <returns></returns>
        public static bool isDenyLogin()
        {
            bool DenyLogin = false;//禁止登陆
            try
            {
                DateTime MinDateTime = Convert.ToDateTime(MemberInfoDAL.GetMaxRegisterDate());

                if (DateTime.Now < MinDateTime || DateTime.Now > MaxDateTime)
                {
                    DenyLogin = true;
                }
            }
            catch { }
            return DenyLogin;
        }
        #endregion

        #region CheckBlacklistLogin -- 检查用户是否在黑名单列表中的登陆状态
        /// <summary>
        /// 检查用户是否在黑名单列表中的登陆状态
        /// </summary>
        /// <param name="userid">用户编号:包括管理员、店铺、经销商</param>
        /// <param name="usertype">类别：0经销商，1店铺，2管理员</param>
        /// <returns>返回true，禁止登陆；false，允许登陆</returns>
        public static bool CheckBlacklistLogin(string userid, int usertype, string UserAddress)
        {
            ArrayList list = new ArrayList();
            string[] SecPostion = UserAddress.Split('.');
            string strIP = "";
            strIP = SecPostion[0];
            SqlDataReader dr = DBHelper.ExecuteReader("select userid from Blacklist where usertype=3 and userid like '" + strIP + "%'");
            while (dr.Read())
            {
                list.Add(new ListItem(dr[0].ToString()));
            }
            dr.Close();
            foreach (ListItem al in list)
            {
                string[] userIP = al.Value.Split('.');
                string PiPei = "";
                string addressIP = "";
                string PiPei1 = "";
                string addressIP1 = "";
                for (int i = 0; i < 4; i++)
                {
                    if (userIP[i].ToString() != "*")
                    {
                        PiPei += userIP[i].ToString() + ".";
                        addressIP += SecPostion[i].ToString() + ".";
                    }
                    else
                    {

                        for (int j = 0; j < i; j++)
                        {
                            PiPei1 += userIP[j].ToString() + ".";
                            addressIP1 += SecPostion[j].ToString() + ".";
                        }
                        if (PiPei1 == addressIP1)
                        {
                            return true;
                        }
                    }

                }
                if (PiPei == addressIP)
                {
                    return true;
                }
            }

            //HttpContext.Current.Response.Write("<script language='javascript'> alert('对不起你的IP已经被禁止登录,请速与公司联系!');location.href='index.aspx';</script>");

            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = new SqlParameter("@UserID", userid);
            parms[1] = new SqlParameter("@UserType", usertype);

            Object objResult = DBHelper.ExecuteScalar("CheckBlacklistLogin", parms, CommandType.StoredProcedure);
            if (objResult != null)
            {
                try
                {
                    if ((int)objResult > 0)
                        return true;
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write(ex.ToString());
                }
            }

            return false;
        }
        #endregion
    }
}
