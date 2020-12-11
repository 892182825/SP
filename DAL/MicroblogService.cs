using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Web;
using System.Data;

namespace DAL
{
    public class MicroblogService
    {

        public static int inserMicroblog(string content, string number, string ip)
        {
            string sql = "insert into Microblog(number,Microblog,Createdate,CreateIP) values(@number,@Microblog,@Createdate,@CreateIP)";
            SqlParameter[] para = {
                                    new SqlParameter("@number",SqlDbType.NVarChar,20),
                                    new SqlParameter("@Microblog",SqlDbType.NVarChar,500),
                                    new SqlParameter("@Createdate",SqlDbType.DateTime),
                                    new SqlParameter("@CreateIP",SqlDbType.NVarChar,20)
                                   };
            para[0].Value = number;
            para[1].Value = content;
            para[2].Value = DateTime.Now;
            para[3].Value = ip;

            int num = DBHelper.ExecuteNonQuery(sql,para,CommandType.Text);
            return num;
        }


        public static int inserFriendGroup(string counfz, string Description, string number, string ip)
        {
            int maxxh = Convert.ToInt32(DBHelper.ExecuteScalar("select isnull(max(xuhao),0) from FriendGroup where number='" + number + "'"));

            string sql = "insert into FriendGroup(number,Groupname,xuhao,Description,Createdate,CreateIP) values(@number,@Groupname,@xuhao,@Description,@Createdate,@CreateIP)";

            SqlParameter[] para = {
                                    new SqlParameter("@number",SqlDbType.NVarChar,20),
                                    new SqlParameter("@Groupname",SqlDbType.NVarChar,50),
                                    new SqlParameter("@xuhao",SqlDbType.Int),
                                    new SqlParameter("@Description",SqlDbType.NVarChar,500),
                                    new SqlParameter("@Createdate",SqlDbType.DateTime),
                                    new SqlParameter("@CreateIP",SqlDbType.NVarChar,20)
                                   };
            para[0].Value = number;
            para[1].Value = counfz;
            para[2].Value = maxxh+1;
            para[3].Value = Description;
            para[4].Value = DateTime.Now;
            para[5].Value = ip;

            int num = DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
            return num;

        }


        public static int delMicroblog(int id)
        {
           
            string sql = "delete from Microblog where id=@id";

            SqlParameter[] para = {
                                    new SqlParameter("@id",SqlDbType.Int)
                                   };
            para[0].Value = id;
           

            int num = DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
            return num;
        }

    }
}
