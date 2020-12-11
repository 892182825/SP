using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using System.Data;
using System.Data.SqlClient;
using Model;

/*
 * 修改者：     汪  华
 * 修改时间：   2009-09-10
 */

namespace DAL
{
    public class ConfigDAL
    {
        /// <summary>
        /// 根据期数更改结算表中的日期
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="expectNum">期数</param>
        /// <returns>返回更改日期所影响的行数</returns>
        public static int UpdDateByExpectNum(string date, int expectNum, string stardate, string enddate)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@date",SqlDbType.VarChar,20),
                new SqlParameter("@expectNum",SqlDbType.Int),
                new SqlParameter("@stardate",SqlDbType.VarChar,30),
                new SqlParameter("@enddate",SqlDbType.VarChar,30)
            };
            sparams[0].Value = date;
            sparams[1].Value = expectNum;
            sparams[2].Value = stardate;
            sparams[3].Value = enddate;

            return DBHelper.ExecuteNonQuery("UpdDateByExpectNum", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 返回系统最大期数
        /// </summary>
        /// <returns></returns>
        public static int GetMaxExpectNum()
        {
            string sql = @"select max(expectNum) from config";
            return Convert.ToInt32(DBHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// 返回所有的期数和日期
        /// </summary>
        /// <returns></returns>
        public static DataTable GetExpectNumAndDates()
        {
            string sql = @"SELECT ExpectNum,Convert(char(10),Date,120) as Date FROM CONFIG ORDER BY ExpectNum";
            return DBHelper.ExecuteDataTable(sql);
        }

        /// <summary>
        /// 从结算表中获取所有的日期和期数
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetAllExpectNumDateFromConfig()
        {
            return DBHelper.ExecuteDataTable("GetAllExpectNumDateFromConfig", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 通过期数获取日期
        /// </summary>
        /// <param name="expectNum">期数</param>
        /// <returns>返回日期</returns>
        public static string GetDateByExpectNumFromConfig(int expectNum)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@expectNum",SqlDbType.Int)
            };
            sparams[0].Value = expectNum;

            return Convert.ToString(DBHelper.ExecuteScalar("GetDateByExpectNumFromConfig", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 从计算表中获取期数
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetExpectNumFromConfig()
        {
            return DBHelper.ExecuteDataTable("GetExpectNumFromConfig", CommandType.StoredProcedure);
        }

        public static ConfigModel GetConfig(int expectNum)
        {
            string sql = "select * from config where ExpectNum = @ExpectNum";
            SqlParameter para = new SqlParameter("@ExpectNum", expectNum);
            SqlDataReader dr = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            ConfigModel model = null;
            if (dr.Read())
            {
                model = new ConfigModel();
                model.ID = Convert.ToInt32(dr["ID"]);
                model.ExpectNum = expectNum;
                model.Date = dr["Date"].ToString();
                model.IsSuance = Convert.ToInt32(dr["IsSuance"]);
                model.Para1 = double.Parse(dr["Para1"].ToString());
                model.Para2 = double.Parse(dr["Para2"].ToString());
                model.Para3 = double.Parse(dr["Para3"].ToString());
                model.Para4 = double.Parse(dr["Para4"].ToString());
                model.Para5 = double.Parse(dr["Para5"].ToString());
                model.Para6 = double.Parse(dr["Para6"].ToString());
                model.Para7 = double.Parse(dr["Para7"].ToString());
                model.Para8 = double.Parse(dr["Para8"].ToString());
                model.Para9 = double.Parse(dr["Para9"].ToString());
                model.Para10 = double.Parse(dr["Para10"].ToString());
                model.Para11 = double.Parse(dr["Para11"].ToString());
                model.Para12 = double.Parse(dr["Para12"].ToString());
                model.Para13 = double.Parse(dr["Para13"].ToString());
                model.Para14 = double.Parse(dr["Para14"].ToString());
                model.Para15 = double.Parse(dr["Para15"].ToString());
                model.Para16 = double.Parse(dr["Para16"].ToString());

                model.Para17 = double.Parse(dr["Para17"].ToString());
                model.Para18 = double.Parse(dr["Para18"].ToString());
                model.Para19 = double.Parse(dr["Para19"].ToString());
            }
            dr.Close();
            dr.Dispose();
            return model;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectNum"></param>
        /// <returns></returns>
        public static Model.ConfigModel GetConfig2(int expectNum)
        {
            string sql = "select * from tkconfig where ExpectNum = @ExpectNum";
            SqlParameter para = new SqlParameter("@ExpectNum", expectNum);
            SqlDataReader dr = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            ConfigModel model = null;
            if (dr.Read())
            {
                model = new ConfigModel();
                model.ID = dr.GetInt32(0);
                model.ExpectNum = expectNum;
                model.Date = dr.GetString(2);
                model.IsSuance = dr.GetInt32(3);
                model.Para1 = float.Parse(dr[4].ToString());
                model.Para2 = float.Parse(dr[5].ToString());
                model.Para3 = float.Parse(dr[6].ToString());
                model.Para4 = float.Parse(dr[7].ToString());
                model.Para5 = float.Parse(dr[8].ToString());
                model.Para6 = float.Parse(dr[9].ToString());
                model.Para7 = float.Parse(dr[10].ToString());
                model.Para8 = float.Parse(dr[11].ToString());
                model.Para9 = float.Parse(dr[12].ToString());
                model.Para10 = float.Parse(dr[13].ToString());
                model.Para11 = float.Parse(dr[14].ToString());
                model.Para12 = float.Parse(dr[15].ToString());
                model.Para13 = float.Parse(dr[16].ToString());
                model.Para14 = float.Parse(dr[17].ToString());
                model.Para15 = float.Parse(dr[18].ToString());
                model.Para16 = float.Parse(dr[19].ToString());
                model.Para17 = float.Parse(dr[20].ToString());
                model.Para18 = float.Parse(dr[21].ToString());
                model.Para19 = float.Parse(dr[22].ToString());
                model.Para20 = float.Parse(dr[23].ToString());
            }
            dr.Close();
            return model;

        }

        public static int UpdateConfig(ConfigModel model)
        {
            SqlParameter[] paras = new SqlParameter[]{
              new SqlParameter("@Para1",model.Para1),
              new SqlParameter("@Para2",model.Para2),
              new SqlParameter("@Para3",model.Para3),
              new SqlParameter("@Para4",model.Para4),
              new SqlParameter("@Para5",model.Para5),
              new SqlParameter("@Para6",model.Para6),
              new SqlParameter("@Para7",model.Para7),
              new SqlParameter("@Para8",model.Para8),
              new SqlParameter("@Para9",model.Para9),
              new SqlParameter("@Para10",model.Para10),
              new SqlParameter("@Para11",model.Para11),
              new SqlParameter("@Para12",model.Para12),
              new SqlParameter("@Para13",model.Para13),
              new SqlParameter("@Para14",model.Para14),
              new SqlParameter("@Para15",model.Para15),
              new SqlParameter("@Para16",model.Para16),
              new SqlParameter("@Para17",model.Para17),
              new SqlParameter("@Para18",model.Para18),
              new SqlParameter("@Para19",model.Para19),
              new SqlParameter("@expectNum",model.ExpectNum)
            };
            string sql = @"update config set Para1=@Para1,Para2=@Para2,Para3=@Para3,Para4=@Para4,Para5=@Para5,
Para6=@Para6,Para7=@Para7,Para8=@Para8,Para9=@Para9,Para10=@Para10,Para11=@Para11,Para12=@Para12,Para13=@Para13,Para14=@Para14,Para15=@Para15,
Para16=@Para16,Para17=@Para17,Para18=@Para18,Para19=@Para19 where expectNum=@expectNum";
            return DBHelper.ExecuteNonQuery(sql, paras, CommandType.Text);
        }

        public static int UpdateConfig2(ConfigModel model)
        {
            SqlParameter[] paras = new SqlParameter[]{
              new SqlParameter("@Para1",model.Para1),
              new SqlParameter("@Para2",model.Para2),
              new SqlParameter("@Para3",model.Para3),
              new SqlParameter("@Para4",model.Para4),
              new SqlParameter("@Para5",model.Para5),
              new SqlParameter("@Para6",model.Para6),
              new SqlParameter("@Para7",model.Para7),
              new SqlParameter("@Para8",model.Para8),
              new SqlParameter("@Para9",model.Para9),
              new SqlParameter("@Para10",model.Para10),
              new SqlParameter("@Para11",model.Para11),
              new SqlParameter("@Para12",model.Para12),
              new SqlParameter("@Para13",model.Para13),
              new SqlParameter("@Para14",model.Para14),
              new SqlParameter("@Para15",model.Para15),
              new SqlParameter("@Para16",model.Para16),
              new SqlParameter("@Para17",model.Para17),
              new SqlParameter("@Para18",model.Para18),
              new SqlParameter("@Para19",model.Para19),
              new SqlParameter("@Para20",model.Para20),
              new SqlParameter("@Para21",model.Para21),
              new SqlParameter("@Para22",model.Para22),
              new SqlParameter("@Para23",model.Para23),
              new SqlParameter("@Para24",model.Para24),
              new SqlParameter("@Para25",model.Para25),
              new SqlParameter("@Para26",model.Para26),
              new SqlParameter("@Para27",model.Para27),
              new SqlParameter("@Para28",model.Para28),
              new SqlParameter("@Para29",model.Para29),
              new SqlParameter("@Iscs",model.Iscs),
              new SqlParameter("@expectNum",model.ExpectNum)
            };
            string sql = "update config set Para1= @Para1,Para2 = @Para2,Para3 = @Para3,Para4=@Para4,Para5=@Para5,Para6=@Para6,Para7=@Para7,Para8=@Para8,Para9=@Para9,Para10=@Para10,Para11=@Para11,Para12=@Para12,Para13=@Para13,Para14=@Para14,Para15=@Para15,Para16=@Para16,Para17=@Para17,Para18=@Para18,Para19=@Para19,Para20=@Para20,Para21=@Para21,Para22=@Para22,Para23=@Para23,Para24=@Para24,Para25=@Para25,Para26=@Para26,Para27=@Para27,Para28=@Para28,Para29=@Para29,Iscs=@Iscs where expectNum = @expectNum";
            return DBHelper.ExecuteNonQuery(sql, paras, CommandType.Text);
        }

        public static int AddtkType(int type, int expectNum, string p, DateTime nowdate)
        {
            string sql = "insert into tktype(type,expectNum,number,AddDate) values(@type,@expectNum,@number,@nowdate)";
            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@type",type),
                new SqlParameter("@expectNum",expectNum),
                new SqlParameter("@number",p),
                new SqlParameter("@nowdate",nowdate)
            };
            return DBHelper.ExecuteNonQuery(sql, paras, CommandType.Text);
        }

        /// <summary>
        /// 获取是否存在
        /// </summary>
        /// <param name="type"></param>
        /// <param name="expectNum"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int GettkType(int type, int expectNum, string number)
        {
            string sql = "select count(1) from tktype where type=@type and expectNum= @expectNum and number= @number";
            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@type",type),
                new SqlParameter("@expectNum",expectNum),
                new SqlParameter("@number",number)
            };
            return int.Parse(DBHelper.ExecuteScalar(sql, paras, CommandType.Text).ToString());

        }

        public static int DelTkType(int p)
        {
            string sql = "delete from tktype where id = @id";
            SqlParameter para = new SqlParameter("@id", p);
            return DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
        }
    }
}