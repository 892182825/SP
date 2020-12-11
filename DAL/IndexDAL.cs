using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class IndexDAL
    {
        public static bool CheckLogin(string type, string name, string pwd)
        {
            bool ck = false;

            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@user", name);
            paras[1] = new SqlParameter("@pwd", pwd);

            StringBuilder sql = new StringBuilder();
            if (type == "Company")
            {
                sql.Append("select count(id) from manage where number=@user and loginpass=@pwd");
                if (DBHelper.ExecuteScalar(sql.ToString(), paras, CommandType.Text).ToString() != "0")
                {
                    ck = true;
                }
            }
            else if (type == "Store")
            {
                sql.Append("select count(id) from storeinfo where storeid=@user and loginpass=@pwd");
                if (DBHelper.ExecuteScalar(sql.ToString(), paras, CommandType.Text).ToString() != "0")
                {
                    ck = true;
                }
            }
            else if (type == "Member")
            {
                sql.Append("select count(id) from memberinfo where (number=@user or MobileTele=@user) and loginpass=@pwd");

                if (DBHelper.ExecuteScalar(sql.ToString(), paras, CommandType.Text).ToString() != "0")
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            else if (type == "Branch")
            {
                sql.Append("select count(id) from branchmanage where number=@user and loginpass=@pwd");

                if (DBHelper.ExecuteScalar(sql.ToString(), paras, CommandType.Text).ToString() != "0")
                {
                    ck = true;
                }


            }

            return ck;

        }

        public static string UplostLogin(string bianhao, string type)
        {
            int lx = 0;
            string endDate = "";
            string strDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();


            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@times",SqlDbType.DateTime),
                new SqlParameter("@bianhao",SqlDbType.VarChar,20)
            };
            parm[0].Value = DateTime.Now.ToUniversalTime();
            parm[1].Value = bianhao;

            SqlParameter spa = new SqlParameter("@bianhao", SqlDbType.VarChar, 20);
            spa.Value = bianhao;

            if (type == "1")
            {
                string sql = "select lastlogindate from manage where  number=@bianhao";
                endDate = DBHelper.ExecuteScalar(sql, spa, CommandType.Text).ToString();
                lx = DBHelper.ExecuteNonQuery("update manage set lastlogindate=@times where number=@bianhao", parm, CommandType.Text);/*strDate*/
            }
            else if (type == "2")
            {
                string sql = "select lastlogindate from storeinfo where  storeid=@bianhao";
                endDate = DBHelper.ExecuteScalar(sql, spa, CommandType.Text).ToString();

                lx = DBHelper.ExecuteNonQuery("update storeinfo set lastlogindate=@times where storeid=@bianhao", parm, CommandType.Text);
            }
            else if (type == "3")
            {
                string sql = "select lastlogindate from memberinfo where  number=@bianhao";
                endDate = DBHelper.ExecuteScalar(sql, spa, CommandType.Text).ToString();

                lx = DBHelper.ExecuteNonQuery("update memberinfo set lastlogindate=@times where number=@bianhao", parm, CommandType.Text);
            }
            else if (type == "4")
            {
                string sql = "select lastlogindate from branchmanage where  number=@bianhao";
                endDate = DBHelper.ExecuteScalar(sql, spa, CommandType.Text).ToString();

                lx = DBHelper.ExecuteNonQuery("update branchmanage set lastlogindate=@times where number=@bianhao", parm, CommandType.Text);
            }
            return endDate;
        }

        public static int insertLoginLog(string number, string pass, string leixing, DateTime logindate, string loginIP, int iscg)
        {
            string sql = "Insert Into LoginLog (number,pass,leixing,logindate,loginIP,iscg) values(@number,@pass,@leixing,@logindate,@loginIP,@iscg)";
            SqlParameter[] para = {
                                          new SqlParameter("@number",SqlDbType.NVarChar,50),
                                          new SqlParameter("@pass",SqlDbType.NVarChar,50),
                                          new SqlParameter("@leixing",SqlDbType.NVarChar,50),
                                          new SqlParameter("@logindate",SqlDbType.DateTime),
                                          new SqlParameter("@loginIP",SqlDbType.NVarChar,50),
                                          new SqlParameter("@iscg",SqlDbType.Int)
                                      };
            para[0].Value = number;
            para[1].Value = pass;
            para[2].Value = leixing;
            para[3].Value = logindate;
            para[4].Value = loginIP;
            para[5].Value = iscg;

            int count = (int)DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
            return count;
        }
    }
}
