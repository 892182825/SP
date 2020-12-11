using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;


//张振
namespace DAL
{
    public class ReleaseDAL
    {
        #region 工资发放


        /// <summary>
        /// 工资是否已经发放——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static bool IsProvide(int ExpectNum)
        {
            string sql = "select count(*) from PayControl where ExpectNum=@ExpectNum";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ExpectNum", SqlDbType.Int) };
            parm[0].Value = ExpectNum;
            object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (Convert.ToInt32(obj) > 0)
            {
                //表示已发放
                return true;
            }
            else
            {
                //未发放
                return false;
            }
        }
        /// <summary>
        /// 奖金发放——ds2012——tianfeng
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        public static bool Provide(int ExpectNum)
        {
            int num = 0;
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ExpectNum", SqlDbType.Int), new SqlParameter("@error", SqlDbType.Int), new SqlParameter("@gtdate", SqlDbType.DateTime) };
            parm[0].Value = ExpectNum;
            parm[1].Value = num;
            parm[2].Value = DateTime.Now.ToUniversalTime();
            parm[1].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery("Remittances_Provide", parm, CommandType.StoredProcedure);
            num = int.Parse(parm[1].Value.ToString());
            if (num == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据期数查询是否已发布——ds2012——tianfeng
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>
        public static int GetOutBonus(int ExpectNum)
        {
            string sql = "select IsSuance from config where ExpectNum=@ExpectNum";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ExpectNum", SqlDbType.Int) };
            parm[0].Value = ExpectNum;
            object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            return int.Parse(obj.ToString());
        }
        /// <summary>
        /// 奖金是否发布——ds2012——tianfeng
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <param name="num">1.发布0.不发布</param>
        /// <returns></returns>
        public static int Release(int ExpectNum, int num)
        {
            string sql = "update config set IsSuance=@num where ExpectNum=@ExpectNum";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@num", SqlDbType.Int), new SqlParameter("@ExpectNum", SqlDbType.Int) };
            parm[0].Value = num;
            parm[1].Value = ExpectNum;
            return DBHelper.ExecuteNonQuery(sql, parm, CommandType.Text);
        }
        /// <summary>
        /// 撤销发放奖金——ds2012——tianfeng
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        public static int Revert(int ExpectNum)
        {
            int num = 0;
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ExpectNum", SqlDbType.Int), new SqlParameter("@error", SqlDbType.Int), new SqlParameter("@gtdate", SqlDbType.DateTime) };
            parm[0].Value = ExpectNum;
            parm[1].Value = num;
            parm[2].Value = DateTime.Now.ToUniversalTime();
            parm[1].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery("Remittances_Recall", parm, CommandType.StoredProcedure);
            num = int.Parse(parm[1].Value.ToString());
            return num;
        }
        /// <summary>
        /// 获得最大期数
        /// </summary>
        /// <returns></returns>
        public static int GetMaxExpectNum()
        {
            string sql = "select max(ExpectNum) from Config";
            object obj = DBHelper.ExecuteScalar(sql);
            if (obj != null)
            {
                return int.Parse(obj.ToString());
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 更新结算状态
        /// </summary>
        /// <returns></returns>
        public static void UPConfigflag(int ExpectNum)
        {
            string sql = "update Config set jsflag=1 where ExpectNum=@ExpectNum";
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@ExpectNum",SqlDbType.Int)
            };
            parm[0].Value = ExpectNum;
            DBHelper.ExecuteNonQuery(sql, parm, CommandType.Text);

        }

        /// <summary>
        /// 设置自动创建新一期时间
        /// </summary>
        /// <returns></returns>
        public static int UpdJiesuantime(string jt, int isjs, string zqts)
        {
            string sql = "update zidongjiesuan set jiesuantime=@jt, isCNewQi=@isjs,jiesuanZQ=@zqts";
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@jt",SqlDbType.NVarChar,50),
                new SqlParameter("@isjs",SqlDbType.Int),
                new SqlParameter("@zqts",SqlDbType.Int)
            };
            parm[0].Value = jt;
            parm[1].Value = isjs;
            parm[2].Value = zqts;
            return DBHelper.ExecuteNonQuery(sql, parm, CommandType.Text);
        }

        /// <summary>
        /// 设置自动结算是否启用
        /// </summary>
        /// <returns></returns>
        public static int UpdJiesuanQy(string qy)
        {
            string sql = "update zidongjiesuan set qiyong=@qy";
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@qy",SqlDbType.VarChar,50)
            };

            parm[0].Value = qy;

            return DBHelper.ExecuteNonQuery(sql, parm, CommandType.Text);
        }

        /// <summary>
        /// 更新结算次数
        /// </summary>
        /// <returns></returns>
        public static void UPConfigNum(int ExpectNum)
        {
            string sql = "update Config set jsnum=jsnum+1 where ExpectNum=@ExpectNum";
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@ExpectNum",SqlDbType.Int)
            };
            parm[0].Value = ExpectNum;
            DBHelper.ExecuteNonQuery(sql, parm, CommandType.Text);

        }
        /// <summary>
        /// 判断是否是当前最大期——ds2012——tianfeng
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>   
        public static int IsCurrently(int ExpectNum)
        {
            string sql = "select count(*) from PayControl where ExpectNum>=@ExpectNum";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ExpectNum", SqlDbType.Int) };
            parm[0].Value = ExpectNum;
            object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            return int.Parse(obj.ToString());
        }
        /// <summary>
        /// 奖金撤销，不是当期最大期——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static int Cancel(int ExpectNum)
        {
            int num = 0;
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ExpectNum", SqlDbType.Int), new SqlParameter("@error", SqlDbType.Int) };
            parm[0].Value = ExpectNum;
            parm[1].Value = num;
            parm[1].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery("Remittances_Backout", parm, CommandType.StoredProcedure);
            num = int.Parse(parm[1].Value.ToString());
            return num;
        }
        #endregion

        #region 工资退回
        /// <summary>
        /// 删除工资退回
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="money">退回金额</param>
        /// <param name="MemeberNum">会员编号</param>
        /// <returns></returns>
        public static bool DelChongHong(int ID, double money, string MemeberNum)
        {
            int num = 0;
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@ID",SqlDbType.Int),
                new SqlParameter("@money",SqlDbType.Money),
                new SqlParameter("@Number",SqlDbType.VarChar,50),
                new SqlParameter("@error",SqlDbType.Int),
                new SqlParameter("@DateTime",SqlDbType.DateTime)
            };
            parm[0].Value = ID;
            parm[1].Value = money;
            parm[2].Value = MemeberNum;
            parm[3].Value = num;
            parm[3].Direction = ParameterDirection.Output;
            parm[4].Value = DateTime.Now.ToUniversalTime();
            DBHelper.ExecuteNonQuery("Delchonghong", parm, CommandType.StoredProcedure);
            num = Convert.ToInt32(parm[3].Value.ToString());
            if (num == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetzidongjsQy()
        {
            return DBHelper.ExecuteScalar("select qiyong from dbo.zidongjiesuan").ToString();
        }

        public static DataTable Getzidongjiesuan()
        {
            return DBHelper.ExecuteDataTable("select * from zidongjiesuan");
        }

        /// <summary>
        /// 获取会员的某期的实发——ds2012——tianfeng
        /// </summary>
        /// <param name="number"></param>
        /// <param name="qs"></param>
        /// <returns></returns>
        public static double getChongHong(string number, int qs)
        {
            double je = 0;
            string sql = "select CurrentSolidSend from BalanceToPurseDetail where expectnum=@qs and number=@number";
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@number",SqlDbType.VarChar,30),
                new SqlParameter("@qs",SqlDbType.Int)
            };
            parm[0].Value = number;
            parm[1].Value = qs;
            je = Convert.ToDouble(DBHelper.ExecuteScalar(sql, parm, CommandType.Text));
            return je;
        }

        /// <summary>
        /// 添加“工资退回”——ds2012——tianfeng
        /// </summary>
        /// <param name="chonghong">工资退回对象</param>
        /// <returns></returns>
        public static bool AddChongHong(ChongHongModel chonghong)
        {
            int num = 0;
            SqlParameter[] parm = new SqlParameter[]{
                 new SqlParameter("@money",SqlDbType.Money),
                 new SqlParameter("@Number",SqlDbType.VarChar,50),
                 new SqlParameter("@ExpectNum",SqlDbType.Int),
                 new SqlParameter("@Remark",SqlDbType.Text),
                 new SqlParameter("@StartDate",SqlDbType.DateTime),
                 new SqlParameter("@error",SqlDbType.Int)
            };
            parm[0].Value = chonghong.MoneyNum;
            parm[1].Value = chonghong.Number;
            parm[2].Value = chonghong.ExpectNum;
            parm[3].Value = chonghong.Remark;
            parm[4].Value = chonghong.StartDate;
            parm[5].Value = num;
            parm[5].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery("Addchonghong", parm, CommandType.StoredProcedure);
            num = Convert.ToInt32(parm[5].Value.ToString());
            if (num == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string ChongHongBeizhu(int ID)
        {
            string sql = "select Remark from ChongHong where ID=@ID";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int) };
            parm[0].Value = ID;
            Object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return "";
            }
        }

        public static string DeductReason(int ID)
        {
            string sql = "select DeductReason from Deduct where id =@ID";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int) };
            parm[0].Value = ID;
            Object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 根据会员编号查询会员信息
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static MemberInfoModel GetMemberInfo(string Number)
        {
            MemberInfoModel info = null;
            string sql = "SELECT Number,Name,Sex,Address,bankbook,bankcard,Jackpot,Out FROM MemberInfo  WHERE Number=@Number";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.VarChar, 50) };
            parm[0].Value = Number;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            if (reader.Read())
            {
                info = new MemberInfoModel();
                info.Name = reader["Name"].ToString();
                info.Sex = Convert.ToInt32(reader["Sex"].ToString());
                info.Address = reader["Address"].ToString();
                info.Number = reader["Number"].ToString();
                info.BankBook = reader["BankBook"].ToString();
                info.BankCard = reader["bankcard"].ToString();
                info.Jackpot = Convert.ToDecimal(reader["Jackpot"]);
                info.EctOut = Convert.ToDecimal(reader["Out"]);
            }
            reader.Close();
            return info;
        }
        #endregion

        #region 工资汇兑
        /// <summary>
        /// 获取账户信息
        /// </summary>
        /// <param name="money">电子钱包金额</param>
        /// <param name="bank">所在银行</param>
        /// <returns></returns>
        public static DataTable GetMemberInfo(double money, string bank, string bankCountry)
        {
            SqlParameter[] parm = null;
            IList<MemberInfoModel> list = new List<MemberInfoModel>();
            StringBuilder sub = new StringBuilder();
            sub.Append("select ExpectNum,Number,Name,PaperNumber,LevelStr,City,Address,BankCard,(Jackpot-ECTPay-ReleaseMoney-Out) as total,Bank from MemberInfo WHERE Jackpot-ECTPay-ReleaseMoney-Out>=@money AND BankCountry=@BankCountry");
            if (bank != "")
            {
                sub.Append(" AND Bank=@Bank");
                parm = new SqlParameter[]{
                    new SqlParameter("@money",SqlDbType.Money),
                    new SqlParameter("@Bank",SqlDbType.VarChar,50),
                    new SqlParameter("@bankCountry",SqlDbType.VarChar,50)
                };
                parm[0].Value = money;
                parm[1].Value = bank;
                parm[2].Value = bankCountry;
            }
            else
            {
                parm = new SqlParameter[]{
                    new SqlParameter("@money",SqlDbType.Money),
                    new SqlParameter("@bankCountry",SqlDbType.VarChar,50)
                };
                parm[0].Value = money;
                parm[1].Value = bankCountry;
            }
            sub.Append(" ORDER BY Bank");
            return DBHelper.ExecuteDataTable(sub.ToString(), parm, CommandType.Text);
        }
        /// <summary>
        /// 奖金汇兑
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <param name="money">金额</param>
        /// <param name="bank">银行</param>
        /// <returns></returns>
        public static bool UpMemberInfo(int ExpectNum, double money, string grant, string ip, string operaternum)
        {
            int res = 0;
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@res",SqlDbType.Int),
                new SqlParameter("@ExpectNum",SqlDbType.Int),
                new SqlParameter("@money",SqlDbType.Decimal),
                new SqlParameter("@DateTime",SqlDbType.DateTime),
                new SqlParameter("@grant",SqlDbType.NVarChar,50),
                new SqlParameter("@ip",SqlDbType.NVarChar,50),
                new SqlParameter("@operaternum",SqlDbType.NVarChar,50)
            };
            parm[0].Value = res;
            parm[1].Value = ExpectNum;
            parm[2].Value = money;
            parm[3].Value = DateTime.Now.ToUniversalTime();
            parm[4].Value = grant;
            parm[5].Value = ip;
            parm[6].Value = operaternum;
            parm[0].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery("AddDetailMoney", parm, CommandType.StoredProcedure);
            res = Convert.ToInt32(parm[0].Value);
            if (res == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 根据电子钱包金额查询用户信息——ds2012——tianfeng
        /// </summary>
        /// <param name="money">金额</param>
        /// <returns></returns>
        public static DataTable GetMemberInfoByMoney(double money)
        {
            string bcard = "&nbsp;";
            string sqlbank = " mb.BankName as bank";
            string sqlcity = " cty.City as bankcity";
            string sqlprovince = " cty.Province as bankprovince ";
            string col = " mi.id,Number,MobileTele,name,'" + bcard + "'+cast(BankCard as varchar) as bankcard,mi.bankbook,bankcard as strbankcard," + sqlbank + "," + sqlprovince + "," + sqlcity + ",'' as souzhi,mi.Jackpot-mi.Out  as zongji," + 2 + " as biaozhi";
            string table = " MemberInfo mi left join MemberBank mb on mb.BankCode=mi.BankCode left join City cty on mi.CPCCode=cty.CPCCode ";
            string condtion = "  mi.Jackpot-mi.Out>=" + money.ToString() + " and mi.Jackpot-mi.Out>0 and bankcard<>''";
            string ordercol = " mi.id ";
            string strSql = " SELECT " + col + " from " + table + " where " + condtion + " order by " + ordercol;
            return DBHelper.ExecuteDataTable(strSql);
        }
        #endregion

        #region 拨出率显示
        /// <summary>
        /// 根据期数读取总金额、总奖金
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>
        public static IList<BochulvModel> GetTotalBonus(double rate, int ExpectNum)
        {
            IList<BochulvModel> list = new List<BochulvModel>();
            string sql = "select ExpectNum,isnull(totalmoney/@Rate,0) as totalmoney,isnull(TotalBonus/@Rate,0) as TotalBonus,isnull(AllocateLead,0) as  AllocateLead  from bochulv where ExpectNum<@ExpectNum order by ExpectNum";
            SqlParameter[] parm = new SqlParameter[]{
            new SqlParameter("@Rate",SqlDbType.Decimal),
            new SqlParameter("@ExpectNum",SqlDbType.Int)
            };
            parm[0].Value = rate;
            parm[1].Value = ExpectNum;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            while (reader.Read())
            {
                BochulvModel info = new BochulvModel();
                info.ExpectNum = int.Parse(reader["ExpectNum"].ToString());
                info.Totalmoney = decimal.Parse(reader["totalmoney"].ToString());
                info.TotalBonus = decimal.Parse(reader["TotalBonus"].ToString());
                info.AllocateLead = decimal.Parse(reader["AllocateLead"].ToString());
                list.Add(info);
            }
            reader.Close();
            return list;
        }
        /// <summary>
        /// 读取总金额、总奖金
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <returns></returns>
        public static void GetBonus(int MaxExpectNum, double rate, out double CurrentSolidSend, out double CurrentOneMoney)
        {
            CurrentSolidSend = 0.0;
            CurrentOneMoney = 0.0;
            string sql = "Select isnull(Sum(totalmoney),0)/@Rate as totalmoney,isnull(Sum(TotalBonus),0)/@Rate as TotalBonus From bochulv where ExpectNum<>" + MaxExpectNum;
            SqlParameter[] parm = new SqlParameter[]{
            new SqlParameter("@Rate",SqlDbType.Decimal)
            };
            parm[0].Value = rate;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            if (reader.Read())
            {
                CurrentSolidSend = double.Parse(reader["totalmoney"].ToString());
                CurrentOneMoney = double.Parse(reader["TotalBonus"].ToString());
            }
            reader.Close();
            string ssQL = "Select isnull(sum(CurrentOneMoney),0)/" + rate + ",isnull(sum(CurrentSolidSend),0)/" + rate + " From MemberInfoBalance" + MaxExpectNum;
            SqlDataReader reader1 = DBHelper.ExecuteReader(ssQL);
            if (reader1.Read())
            {
                CurrentSolidSend += Convert.ToDouble(reader1[0]);
                CurrentOneMoney += Convert.ToDouble(reader1[1]);
            }
            reader1.Close();
        }
        /// <summary>
        /// 读取总金额、总PV
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <returns></returns>

        public static void GetPV(int MaxExpectNum, out double CurrentSolidSend, out double CurrentOneMoney)
        {
            CurrentSolidSend = 0.0;
            CurrentOneMoney = 0.0;
            string sql = "Select isnull(Sum(Totalpv),0) as totalmoney,isnull(Sum(TotalBonus),0) as TotalBonus From bochulv where ExpectNum<>" + MaxExpectNum;
            SqlDataReader reader = DBHelper.ExecuteReader(sql);
            if (reader.Read())
            {
                CurrentSolidSend = double.Parse(reader["TotalBonus"].ToString());
                CurrentOneMoney = double.Parse(reader["totalmoney"].ToString());
            }
            reader.Close();
            string sql1 = "SELECT  isnull(sum(CurrentOneMark),0)  as moneyZongShouRu,isnull(sum(CurrentSolidSend),0)  as moneyZhiChu FROM MemberInfoBalance" + MaxExpectNum + "";
            SqlDataReader reader1 = DBHelper.ExecuteReader(sql1);
            if (reader1.Read())
            {
                CurrentSolidSend += Convert.ToDouble(reader1[1]);
                CurrentOneMoney += Convert.ToDouble(reader1[0]);
            }
            reader1.Close();
        }
        /// <summary>
        /// 读取本期实发奖金和本个人消费金额
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <param name="ExpectNum">期数</param>
        /// <param name="CurrentSolidSend">本期实发奖金</param>
        /// <param name="CurrentOneMoney">本个人消费金额</param>
        public static void GetTotalMoney(double rate, int ExpectNum, out double CurrentSolidSend, out double CurrentOneMoney)
        {
            CurrentSolidSend = 0;
            CurrentOneMoney = 0;
            string sql = "SELECT  isnull(sum(CurrentOneMoney)/@Rate,0)  as moneyZongShouRu,isnull(sum(CurrentSolidSend)/@rate,0)  as moneyZhiChu FROM MemberInfoBalance" + ExpectNum + "";
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Rate",SqlDbType.Decimal)
            };
            parm[0].Value = rate;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            if (reader.Read())
            {
                CurrentSolidSend = double.Parse(reader["moneyZhiChu"].ToString());
                CurrentOneMoney = double.Parse(reader["moneyZongShouRu"].ToString());
            }
            reader.Close();
        }

        public static double GetRate(int id)
        {
            return Convert.ToDouble(DBHelper.ExecuteScalar("select rate from Currency where id=" + id));
        }


        /// <summary>
        /// 读取本期实发奖金和本个人消费PV
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <param name="ExpectNum">期数</param>
        /// <param name="CurrentSolidSend">本期实发奖金</param>
        /// <param name="CurrentOneMoney">本个人消费PV</param>
        public static void GetCurrentPV(int ExpectNum, out double CurrentSolidSend, out double CurrentOneMoney)
        {
            CurrentSolidSend = 0;
            CurrentOneMoney = 0;
            string sql = "SELECT  isnull(sum(CurrentOneMark),0)  as moneyZongShouRu,isnull(sum(CurrentSolidSend),0)  as moneyZhiChu FROM MemberInfoBalance" + ExpectNum + "";
            SqlDataReader reader = DBHelper.ExecuteReader(sql);
            if (reader.Read())
            {
                CurrentSolidSend = double.Parse(reader["moneyZhiChu"].ToString());
                CurrentOneMoney = double.Parse(reader["moneyZongShouRu"].ToString());
            }
            reader.Close();
        }
        /// <summary>
        /// 获得当前单个奖金滴总额
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <param name="hang"></param>
        /// <param name="zoushi"></param>
        /// <param name="zoushi"></param>
        /// <param name="CurrentOneMark"></param>
        public static void GetOneBonus(double Rate, int ExpectNum, int hang, string[,] zoushi, out string[,] zoushi1, out double CurrentOneMark)
        {
            string sql1;
            CurrentOneMark = 0.0;
            if (ExpectNum.ToString() != GetMaxExpectNum().ToString())
            {
                sql1 = "select isnull(Totalmoney/" + Rate + ",0)  as moneyZongShouRu ";
                for (int q = 1; q < hang; q++)
                {
                    sql1 = sql1.ToString() + ",isnull(" + zoushi[q, 4].ToString() + "/" + Rate + ",0)  as " + zoushi[q, 4].ToString();
                }
                sql1 = sql1 + " from bochulv where ExpectNum=" + ExpectNum.ToString();
            }
            else
            {
                sql1 = "select  isnull(sum(CurrentOneMark)/" + Rate + ",0)  as moneyZongShouRu ";
                for (int q = 1; q < hang; q++)
                {
                    sql1 = sql1.ToString() + ",isnull(sum(" + zoushi[q, 4].ToString() + ")/" + Rate + ",0)  as " + zoushi[q, 4].ToString();
                }
                sql1 = sql1 + " from MemberInfoBalance" + ExpectNum.ToString();
            }

            double moneyZongShouRu = 1;
            SqlDataReader reader = DBHelper.ExecuteReader(sql1, CommandType.Text);//用小唐的类。
            if (reader.Read())
            {
                CurrentOneMark = Convert.ToDouble(reader["moneyZongShouRu"]);


                if (reader["moneyZongShouRu"] == DBNull.Value || Convert.ToDouble(reader["moneyZongShouRu"]) == 0)
                {
                    moneyZongShouRu = 1;
                }
                else
                {
                    moneyZongShouRu = Convert.ToDouble(reader["moneyZongShouRu"]);
                }

                for (int m = 1; m < hang; m++)
                {
                    zoushi[m, 0] = m.ToString();
                    zoushi[m, 2] = Convert.ToString(reader[zoushi[m, 4].ToString()]); // 奖金数额
                    if (zoushi[m, 2].ToString().Trim() == "")
                    {
                        zoushi[m, 2] = "0";
                    }
                }
            }
            reader.Close();
            zoushi1 = zoushi;
            //zoushi1 = zoushi;
            //CurrentOneMark = 0.0;
            //string sql1 = "select  isnull(sum(CurrentOneMark)/@Rate,0)  as moneyZongShouRu ";
            //for (int q = 1; q < hang; q++)
            //{
            //    sql1 = sql1.ToString() + ",isnull(sum(" + zoushi[q, 4].ToString() + ")/" + Rate + ",0)  as " + zoushi[q, 4].ToString();
            //    //sql1 = sql1.ToString() + ",isnull(sum(" + zoushi[q, 4].ToString() + "),0)  as " + zoushi[q, 4].ToString();
            //}
            //sql1 = sql1 + " from MemberInfoBalance"+ExpectNum+"";
            //SqlParameter[] parm = new SqlParameter[] { 
            //new SqlParameter("@Rate",Rate),
            //new SqlParameter("@ExpectNum",ExpectNum)
            //};
            //SqlDataReader reader = DBHelper.ExecuteReader(sql1, parm, CommandType.Text);
            //if (reader.Read())
            //{
            //    if (reader["moneyZongShouRu"] == DBNull.Value || Convert.ToDouble(reader["moneyZongShouRu"].ToString()) == 0)
            //    {
            //        CurrentOneMark = 1;
            //    }
            //    else
            //    {
            //        CurrentOneMark = Convert.ToDouble(reader["moneyZongShouRu"].ToString());
            //    }

            //    for (int m = 1; m < hang; m++)
            //    {
            //        zoushi[m, 0] = m.ToString();
            //        zoushi[m, 2] = Convert.ToString(reader[zoushi[m, 4].ToString()]); // 奖金数额
            //        if (zoushi[m, 2].ToString().Trim() == "")
            //        {
            //            zoushi[m, 2] = "0";
            //        }
            //    }
            //}
            //reader.Close();
            //zoushi1 = zoushi;
        }
        /// <summary>
        /// 根据期数读取总PV、总奖金
        /// </summary>
        /// <param name="rate">汇率</param>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>
        public static IList<BochulvModel> GetTotalPV(int ExpectNum)
        {
            IList<BochulvModel> list = new List<BochulvModel>();
            string sql = "select ExpectNum,isnull(Totalpv,0) as totalmoney,isnull(TotalBonus,0) as TotalBonus,isnull(AllocateLead,0) as  AllocateLead  from bochulv where ExpectNum<@ExpectNum order by ExpectNum";
            SqlParameter[] parm = new SqlParameter[]{
            new SqlParameter("@ExpectNum",SqlDbType.Int)
            };
            parm[0].Value = ExpectNum;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            while (reader.Read())
            {
                BochulvModel info = new BochulvModel();
                info.ExpectNum = int.Parse(reader["ExpectNum"].ToString());
                info.Totalmoney = decimal.Parse(reader["totalmoney"].ToString());
                info.TotalBonus = decimal.Parse(reader["TotalBonus"].ToString());
                info.AllocateLead = decimal.Parse(reader["AllocateLead"].ToString());
                list.Add(info);
            }
            reader.Close();
            return list;
        }

        public static IList<BochulvModel> GetTotalPV(string StartExpect, string EndExpect)
        {
            IList<BochulvModel> list = new List<BochulvModel>();
            string sql = @"select ExpectNum,isnull(Totalpv,0) as totalmoney,isnull(TotalBonus,0) as TotalBonus,isnull(AllocateLead,0) as  AllocateLead  from bochulv 
                        where ExpectNum<=@EndExpect and ExpectNum >= @StartExpect order by ExpectNum";
            SqlParameter[] parm = new SqlParameter[]{
            new SqlParameter("@StartExpect",SqlDbType.Int),
            new SqlParameter("@EndExpect",SqlDbType.Int)
            };
            parm[0].Value = StartExpect;
            parm[1].Value = EndExpect;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            while (reader.Read())
            {
                BochulvModel info = new BochulvModel();
                info.ExpectNum = int.Parse(reader["ExpectNum"].ToString());
                info.Totalmoney = decimal.Parse(reader["totalmoney"].ToString());
                info.TotalBonus = decimal.Parse(reader["TotalBonus"].ToString());
                info.AllocateLead = decimal.Parse(reader["AllocateLead"].ToString());
                list.Add(info);
            }
            reader.Close();
            return list;
        }
        #endregion

        #region 工资结算
        /// <summary>
        /// 获得要结算的最后10期的信息
        /// </summary>
        /// <returns></returns>
        public static IList<ConfigModel> GetConfigInfo()
        {
            IList<ConfigModel> list = new List<ConfigModel>();
            string sql = "select  top  10 *  from  config  order  by  ExpectNum  desc";
            SqlDataReader reader = DBHelper.ExecuteReader(sql);
            while (reader.Read())
            {
                ConfigModel info = new ConfigModel();
                info.Date = reader["Date"].ToString();
                info.ExpectNum = Convert.ToInt32(reader["ExpectNum"].ToString());
                info.ID = Convert.ToInt32(reader["ID"].ToString());
                info.Jsnum = Convert.ToInt32(reader["jsnum"].ToString());
                info.Jsflag = Convert.ToInt32(reader["jsflag"].ToString());
                list.Add(info);
            }
            reader.Close();
            reader.Dispose();
            return list;
        }
        /// <summary>
        /// 插入配置表
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>
        public static bool insertConfig(int ExpectNum)
        {
            //添加配置表中的一条记录
            string insertSql = "insert into config (";
            string valueSql = " values( ";
            string sql = "select * from config where ExpectNum=@ExpectNum";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ExpectNum", SqlDbType.Int) };
            parm[0].Value = ExpectNum;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            if (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    //读取各个字段，排除自增字段(id)
                    if (reader.GetName(i).ToLower() != "ID")
                    {
                        insertSql += reader.GetName(i);

                        //期数字段的值要加1
                        if (reader.GetName(i).ToLower() != "ExpectNum")
                        {
                            //如果是字符型字段，则要在两边加上单引号
                            if (reader.GetFieldType(i).FullName == "System.String")
                            {
                                valueSql += ("'" + reader[i].ToString() + "'");
                            }
                            else
                            {
                                valueSql += reader[i].ToString();
                            }
                        }
                        else
                        {
                            valueSql += (Convert.ToInt32(reader[i]) + 1);
                        }


                        if (i != reader.FieldCount - 1)
                        {
                            insertSql += ",";
                            valueSql += ",";
                        }
                        else
                        {
                            insertSql += ")";
                            valueSql += ")";
                        }
                    }
                }
            }
            reader.Close();

            insertSql += valueSql;

            int num = DBHelper.ExecuteNonQuery(insertSql);
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 创建新一期
        /// </summary>
        /// <param name="ExpectNum">要创建的期数</param>
        /// <returns></returns>
        public static bool addNewQishu(int ExpectNum)
        {
            bool flag = false;
            SqlParameter[] parm ={
                new SqlParameter("@newqishu",SqlDbType.Int),
                new SqlParameter("@res",SqlDbType.Int)
            };
            parm[0].Value = ExpectNum;
            parm[1].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery("CreateNewQi", parm, CommandType.StoredProcedure);
            if (Convert.ToInt32(parm[1].Value) == 0) //创建新一期成功
            {
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// 显示所有期数的结算链接。
        /// </summary>
        /// <param name="startqishu">开始期数</param>
        /// <param name="maxqishu">最大期数</param>
        /// <param name="maxqishu">要显示的期数范围，从1到当前值。</param>
        public static IList<ConfigModel> showTotalQishuLink(int startqishu, int maxqishu)
        {
            IList<ConfigModel> list = new List<ConfigModel>();
            string sql = "select  *  from  config   where  ExpectNum  between  @startqishu  and  @maxqishu  order  by  ExpectNum  desc ";
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@startqishu",SqlDbType.Int),
            new SqlParameter("@maxqishu",SqlDbType.Int)
            };
            parm[0].Value = startqishu;
            parm[1].Value = maxqishu;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            while (reader.Read())
            {
                ConfigModel info = new ConfigModel();
                info.Date = reader["Date"].ToString();
                info.ExpectNum = Convert.ToInt32(reader["ExpectNum"].ToString());
                info.ID = Convert.ToInt32(reader["ID"].ToString());
                info.Jsnum = Convert.ToInt32(reader["Jsnum"]);
                info.Jsflag = Convert.ToInt32(reader["Jsflag"]);
                list.Add(info);
            }
            reader.Close();
            return list;
        }
        /// <summary>
        /// 当期是否有未发放的奖金
        /// </summary>
        /// <param name="ExpectNum">当期</param>
        /// <returns></returns>
        public static bool IsNotProvideBonus(int ExpectNum)
        {
            string sql = "select count(ExpectNum) from PayControl where ExpectNum=@ExpectNum";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ExpectNum", SqlDbType.Int) };
            parm[0].Value = ExpectNum;
            object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (int.Parse(obj.ToString()) == 0)
            {
                //没有
                return false;
            }
            else
            {
                //有
                return true;
            }
        }
        /// <summary>
        /// 更新发放状态 --ds2012--www-b874dce8700
        /// </summary>
        /// <param name="ExpectNum">当期</param>
        /// <returns></returns>
        public static bool upProvideState(int ExpectNum)
        {
            string sql = "update config set IsSuance=1 where ExpectNum=@ExpectNum";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ExpectNum", SqlDbType.Int) };
            parm[0].Value = ExpectNum;
            int num = DBHelper.ExecuteNonQuery(sql, parm, CommandType.Text);
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断发放期数中是否存在所选期
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>
        public static int IsSuperintendent(int ExpectNum)
        {
            string sql = "select isnull(count(*),0) from PayControl where ExpectNum =@ExpectNum";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ExpectNum", SqlDbType.Int) };
            parm[0].Value = ExpectNum;
            return int.Parse(DBHelper.ExecuteScalar(sql, parm, CommandType.Text).ToString());
        }
        /// <summary>
        /// 检测是否存在没有会员编号的店铺
        /// </summary>
        public static int IsNumberExists()
        {
            string sql = "select count(*) from StoreInfo where Number=''";
            return int.Parse(DBHelper.ExecuteScalar(sql).ToString());
        }
        /// <summary>
        /// 撤销电子账户
        /// </summary>
        /// <param name="ExpectNum"></param>
        /// <returns></returns>
        public static void Backout(int ExpectNum)
        {
            SqlParameter[] parm = new System.Data.SqlClient.SqlParameter[]{
                                                                                   new SqlParameter("@qishu",SqlDbType.Int)
                                                                               };
            parm[0].Value = ExpectNum;
            DBHelper.ExecuteNonQuery("Cancle_jj_mingxi", parm, CommandType.StoredProcedure);
        }
        public static int CancleBonus(int ExpectNum)
        {
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ExpectNum", SqlDbType.Int) };
            parm[0].Value = ExpectNum;
            return DBHelper.ExecuteNonQuery("CancleBonus", parm, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 是否有错误的单子
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>
        public static int ISErrorOrdre(int ExpectNum)
        {
            string sql = "SELECT count(*) from MemberOrder WHERE len(ltrim([Error]))>0 and PayExpectNum =@ExpectNum";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ExpectNum", SqlDbType.Int) };
            parm[0].Value = ExpectNum;
            return DBHelper.ExecuteNonQuery(sql, parm, CommandType.Text);
        }
        #endregion

        #region 更多查询
        /// <summary>
        /// 判断当前是否有已结算的工资
        /// </summary>
        /// <returns></returns>
        public static int GetMaxExpectNumByIsSuance()
        {
            string sql = "select isnull(max(ExpectNum),0) from config where IsSuance=1";
            return Convert.ToInt32(DBHelper.ExecuteScalar(sql).ToString());
        }
        /// <summary>
        /// 根据店铺编号获取店长编号
        /// </summary>
        /// <param name="StoreID">店铺编号</param>
        /// <returns></returns>
        public static string GetNumberByStoreID(string StoreID)
        {
            string sql = "select Number from StoreInfo where StoreID=@StoreID";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@StoreID", SqlDbType.VarChar, 50) };
            parm[0].Value = StoreID;
            return DBHelper.ExecuteScalar(sql, parm, CommandType.Text).ToString();
        }
        /// <summary>
        /// 根据会员编号获取会员姓名
        /// </summary>
        /// <param name="Number">编号</param>
        /// <returns></returns>
        public static string GetNameByPlacement(string Number)
        {
            string sql = "select name from MemberInfo where Number=@Number";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.VarChar, 50) };
            parm[0].Value = Number;
            object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }
        /// <summary>
        /// //根据编号、期数读取结算表信息
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <param name="Number">编号</param>
        /// <returns></returns>
        public static DataTable GetMemberInfoBalanceByNumber(int ExpectNum, string Number)
        {
            string sql;
            if (ExpectNum == 0)
            {

                sql = "select *,lv.levelstr from MemberInfoBalance" + " as m,bsco_level as lv where m.Number=@Number and m.level=lv.levelint and lv.levelflag=0";
            }
            else {

                sql = "select *,lv.levelstr from MemberInfoBalance" + ExpectNum + " as m,bsco_level as lv where m.Number=@Number and m.level=lv.levelint and lv.levelflag=0";
            }
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.VarChar, 50) };
            parm[0].Value = Number;
            return DBHelper.ExecuteDataTable(sql, parm, CommandType.Text);
        }
        #endregion


        public static IList<BochulvModel> GetTotalBonus(double rate, string StartExpect, string EndExpect)
        {
            IList<BochulvModel> list = new List<BochulvModel>();
            string sql = @"select ExpectNum,isnull(totalmoney/@Rate,0) as totalmoney,isnull(TotalBonus/@Rate,0) as TotalBonus,isnull(AllocateLead,0) as  AllocateLead  from bochulv
                        where ExpectNum >= @StartExpect and  ExpectNum<=@EndExpect order by ExpectNum";
            SqlParameter[] parm = new SqlParameter[]{
            new SqlParameter("@Rate",SqlDbType.Decimal),
            new SqlParameter("@StartExpect",SqlDbType.Int),
            new SqlParameter("@EndExpect",SqlDbType.Int)
            };
            parm[0].Value = rate;
            parm[1].Value = StartExpect;
            parm[2].Value = EndExpect;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            while (reader.Read())
            {
                BochulvModel info = new BochulvModel();
                info.ExpectNum = int.Parse(reader["ExpectNum"].ToString());
                info.Totalmoney = decimal.Parse(reader["totalmoney"].ToString());
                info.TotalBonus = decimal.Parse(reader["TotalBonus"].ToString());
                info.AllocateLead = decimal.Parse(reader["AllocateLead"].ToString());
                list.Add(info);
            }
            reader.Close();
            reader.Dispose();
            return list;
        }

        public static void GetPV(int StartExpect, int EndExpect, out double CurrentSolidSend, out double CurrentOneMoney)
        {
            CurrentSolidSend = 0.0;
            CurrentOneMoney = 0.0;
            string sql = "Select isnull(Sum(Totalpv),0) as totalmoney,isnull(Sum(TotalBonus),0) as TotalBonus From bochulv where ExpectNum>=" + StartExpect + "and ExpectNum <" + EndExpect;
            SqlDataReader reader = DBHelper.ExecuteReader(sql);
            if (reader.Read())
            {
                CurrentSolidSend = double.Parse(reader["TotalBonus"].ToString());
                CurrentOneMoney = double.Parse(reader["totalmoney"].ToString());
            }
            reader.Close();
            reader.Dispose();
            string sql1 = "SELECT  isnull(sum(CurrentOneMark),0)  as moneyZongShouRu,isnull(sum(CurrentSolidSend),0)  as moneyZhiChu FROM MemberInfoBalance" + EndExpect + "";
            SqlDataReader reader1 = DBHelper.ExecuteReader(sql1);
            if (reader1.Read())
            {
                CurrentSolidSend += Convert.ToDouble(reader1[1]);
                CurrentOneMoney += Convert.ToDouble(reader1[0]);
            }
            reader1.Close();
            reader1.Dispose();
        }

        public static void GetBonus(int StartExpect, int EndExpect, double rate, out double CurrentSolidSend, out double CurrentOneMoney)
        {
            CurrentSolidSend = 0.0;
            CurrentOneMoney = 0.0;
            string sql = "Select isnull(Sum(totalmoney),0)/@Rate as totalmoney,isnull(Sum(TotalBonus),0)/@Rate as TotalBonus From bochulv where ExpectNum>=" + StartExpect + " and ExpectNum<" + EndExpect;
            SqlParameter[] parm = new SqlParameter[]{
            new SqlParameter("@Rate",SqlDbType.Decimal)
            };
            parm[0].Value = rate;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            if (reader.Read())
            {
                CurrentSolidSend = double.Parse(reader["totalmoney"].ToString());
                CurrentOneMoney = double.Parse(reader["TotalBonus"].ToString());
            }
            reader.Close();
            string ssQL = "Select isnull(sum(CurrentOneMoney),0)/" + rate + ",isnull(sum(CurrentSolidSend),0)/" + rate + " From MemberInfoBalance" + EndExpect;
            SqlDataReader reader1 = DBHelper.ExecuteReader(ssQL);
            if (reader1.Read())
            {
                CurrentSolidSend += Convert.ToDouble(reader1[0]);
                CurrentOneMoney += Convert.ToDouble(reader1[1]);
            }
            reader1.Close();
            reader1.Dispose();
        }


        /// <summary>
        /// 获得要结算的最后10期的信息
        /// </summary>
        /// <returns></returns>
        public static IList<ConfigModel> GetConfigInfo1(int MaxQs)
        {
            IList<ConfigModel> list = new List<ConfigModel>();
            string sql = "select  top  10 *  from  config where ExpectNum<@MaxQs order  by  ExpectNum  desc";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@MaxQs", SqlDbType.Int) };
            parm[0].Value = MaxQs;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            while (reader.Read())
            {
                ConfigModel info = new ConfigModel();
                info.Date = reader["Date"].ToString();
                info.ExpectNum = Convert.ToInt32(reader["ExpectNum"].ToString());
                info.ID = Convert.ToInt32(reader["ID"].ToString());
                list.Add(info);
            }
            reader.Close();
            reader.Dispose();
            return list;
        }

        /// <summary>
        /// 扣款原因
        /// </summary>
        /// <param name="Number">编号</param>
        /// <returns></returns>
        public static DataTable Reason(string Number)
        {
            string sql = "select MemberInfo.Number,MemberInfo.Name,Deduct.DeductMoney,Deduct.DeductReason,Deduct.ExpectNum from MemberInfo,Deduct where Deduct.Number=MemberInfo.Number and MemberInfo.Number=@Number";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.VarChar, 50) };
            parm[0].Value = Number;
            return DBHelper.ExecuteDataTable(sql, parm, CommandType.Text);
        }

        public static string GetRemark(int id)
        {
            string sql = "SELECT Remark FROM MemberInfo WHERE id=@ID";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int) };
            parm[0].Value = id;
            object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (obj != null)
                return obj.ToString();
            else
                return "";
        }

        public static int ISSuanceJj(int ExpectNum)
        {
            string strSql = "";
            if (ExpectNum == 0)
            {
                strSql = "select max(ExpectNum) from config where IsSuance=1";
            }
            else
            {
                strSql = "select IsSuance from config where ExpectNum=" + ExpectNum + "";
            }
            Object obj = DBHelper.ExecuteScalar(strSql);
            if (obj != DBNull.Value)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 获得该会员的奖金——ds2012——tianfeng
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <returns></returns>
        public static double GetMemberBonus(string number)
        {
            string sql = "select Jackpot-Out-Membership from MemberInfo where Number=@num";
            SqlParameter[] parm = new SqlParameter[] {
              new SqlParameter("@num",SqlDbType.NVarChar,50)

            };
            parm[0].Value = number;

            return Convert.ToDouble(DBHelper.ExecuteScalar(sql, parm, CommandType.Text));
        }
        /// <summary>
        /// 获得该会员的奖金,提现账户（带事务）——ds2012——tianfeng
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <returns></returns>
        public static double GetMemberBonus(string number, SqlTransaction tran)
        {
            string sql = "select Jackpot-Out-Membership from MemberInfo where Number=@num";
            SqlParameter[] parm = new SqlParameter[] {
              new SqlParameter("@num",SqlDbType.NVarChar,50)

            };
            parm[0].Value = number;

            return Convert.ToDouble(DBHelper.ExecuteScalar(tran, sql, parm, CommandType.Text));
        }
        /// <summary>
        /// 添加转账记录
        /// </summary>
        /// <param name="info"></param>
        public static void AddTransfer(MoneyTransferModel info, out int outid)
        {
            outid = 0;
            //添加记录
            //string sql = "insert into MoneyTransfer(Money,OutNumber,Remark,TransferTime,TransferType,TrunNumber) values(@Money,@OutNumber,@Remark,@TransferTime,@TransferType,@TrunNumber)";
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Money",SqlDbType.Money),
                new SqlParameter("@OutNumber",SqlDbType.NVarChar,50),
                new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                new SqlParameter("@TransferTime",SqlDbType.DateTime),
                new SqlParameter("@TransferType",SqlDbType.Int),
                new SqlParameter("@TrunNumber",SqlDbType.NVarChar,50),
                new SqlParameter("@isMember",SqlDbType.Int),
                new SqlParameter("@outid",SqlDbType.Int)
            };
            parm[0].Value = info.Money;
            parm[1].Value = info.OutNumber;
            parm[2].Value = info.Remark;
            parm[3].Value = info.TransferTime;
            parm[4].Value = info.TransferType;
            parm[5].Value = info.TrunNumber;
            parm[6].Value = info.IsMember;
            parm[7].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery("AddTransfer", parm, CommandType.StoredProcedure);
            outid = Convert.ToInt32(parm[7].Value);
            //更新金额
            if (info.TransferType == 1)
            {

                string sql1 = "update MemberInfo set TotalDefray=TotalDefray+@TotalDefray where Number=@Number";
                SqlParameter[] parm1 = new SqlParameter[] { new SqlParameter("@TotalDefray", SqlDbType.Money), new SqlParameter("@Number", SqlDbType.NVarChar, 50) };
                parm1[0].Value = info.Money;
                parm1[1].Value = info.OutNumber;
                int num = DBHelper.ExecuteNonQuery(sql1, parm1, CommandType.Text);
            }
            if (info.TransferType == 2)
            {
                string sql1 = "update MemberInfo set Out=Out+@Out where Number=@Number";
                SqlParameter[] parm1 = new SqlParameter[] { new SqlParameter("@Out", SqlDbType.Money), new SqlParameter("@Number", SqlDbType.NVarChar, 50) };
                parm1[0].Value = info.Money;
                parm1[1].Value = info.OutNumber;
                int num = DBHelper.ExecuteNonQuery(sql1, parm1, CommandType.Text);
            }
        }

        /// <summary>
        /// 添加转账记录
        /// </summary>
        /// <param name="info"></param>
        public static bool AddTransfer(SqlTransaction tran, MoneyTransferModel info, out int outid)
        {
            outid = 0;
            //添加记录
            //string sql = "insert into MoneyTransfer(Money,OutNumber,Remark,TransferTime,TransferType,TrunNumber) values(@Money,@OutNumber,@Remark,@TransferTime,@TransferType,@TrunNumber)";
            SqlParameter[] parm = new SqlParameter[] {
                new SqlParameter("@Money",SqlDbType.Money),
                new SqlParameter("@OutNumber",SqlDbType.NVarChar,50),
                new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                new SqlParameter("@TransferTime",SqlDbType.DateTime),
                new SqlParameter("@TransferType",SqlDbType.Int),
                new SqlParameter("@TrunNumber",SqlDbType.NVarChar,50),
                new SqlParameter("@isMember",SqlDbType.Int),
                new SqlParameter("@outid",SqlDbType.Int)
            };
            parm[0].Value = info.Money;
            parm[1].Value = info.OutNumber;
            parm[2].Value = info.Remark;
            parm[3].Value = info.TransferTime;
            parm[4].Value = info.TransferType;
            parm[5].Value = info.TrunNumber;
            parm[6].Value = info.IsMember;
            parm[7].Direction = ParameterDirection.Output;
            int count = (int)DBHelper.ExecuteNonQuery(tran, "AddTransfer", parm, CommandType.StoredProcedure);
            if (count == 0)
            {
                return false;
            }
            outid = Convert.ToInt32(parm[7].Value);
            //更新金额
            if (info.TransferType == 1)
            {

                string sql1 = "update MemberInfo set TotalDefray=TotalDefray+@TotalDefray where Number=@Number";
                SqlParameter[] parm1 = new SqlParameter[] { new SqlParameter("@TotalDefray", SqlDbType.Money), new SqlParameter("@Number", SqlDbType.NVarChar, 50) };
                parm1[0].Value = info.Money;
                parm1[1].Value = info.OutNumber;
                int num = DBHelper.ExecuteNonQuery(tran, sql1, parm1, CommandType.Text);
                if (num == 0)
                {
                    return false;
                }
            }
            if (info.TransferType == 2)
            {
                string sql1 = "update MemberInfo set Out=Out+@Out where Number=@Number";
                SqlParameter[] parm1 = new SqlParameter[] { new SqlParameter("@Out", SqlDbType.Money), new SqlParameter("@Number", SqlDbType.NVarChar, 50) };
                parm1[0].Value = info.Money;
                parm1[1].Value = info.OutNumber;
                int num = DBHelper.ExecuteNonQuery(tran, sql1, parm1, CommandType.Text);
                if (num == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 获得该会员的奖金,报单账户
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <returns></returns>
        public static double GetMemberDeclarations(string number)
        {
            string sql = "select TotalRemittances-TotalDefray from MemberInfo where Number=@num";
            SqlParameter[] parm = new SqlParameter[] {
              new SqlParameter("@num",SqlDbType.NVarChar,50)

            };
            parm[0].Value = number;
            return Convert.ToDouble(DBHelper.ExecuteScalar(sql, parm, CommandType.Text));
        }
        public static double GetMemberDeclarations(string number, SqlTransaction tran)
        {
            string sql = "select TotalRemittances-TotalDefray from MemberInfo where Number=@num";
            SqlParameter[] parm = new SqlParameter[] {
              new SqlParameter("@num",SqlDbType.NVarChar,50)

            };
            parm[0].Value = number;
            return Convert.ToDouble(DBHelper.ExecuteScalar(tran, sql, parm, CommandType.Text));
        }
        /// <summary>
        /// 判断是否有未结算的
        /// </summary>
        /// <param name="ExpectNum"></param>
        /// <returns></returns>
        public static bool GetIsExistsConfig(int ExpectNum)
        {
            string sql = "select count(*) from config where jsflag=0 and ExpectNum<@ExpectNum";
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@ExpectNum",SqlDbType.Int)
        };
            parm[0].Value = ExpectNum;
            object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (Convert.ToInt32(obj) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 添加不扣款，更新
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="number">会员编号</param>
        /// <param name="IsDeduct">扣款/补款</param>
        /// <param name="DeductMoney">金额</param>
        /// <returns></returns>
        public static bool UpdateDeductOut(SqlTransaction tran, string number, int IsDeduct, double DeductMoney)
        {
            string mid = "Jackpot=Jackpot+" + DeductMoney;
            if (IsDeduct == 0) // 0 扣款  1 补款
            {
                mid = "[Out]=[Out]+" + DeductMoney;
            }
            return DBHelper.ExecuteNonQuery(tran, "update memberinfo set " + mid + " where number='" + number + "'") > 0 ? true : false;
        }


        /// <summary>
        /// 添加不扣款，更新
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="number">会员编号</param>
        /// <param name="IsDeduct">扣款/补款</param>
        /// <param name="DeductMoney">金额</param>
        /// <returns></returns>
        public static bool UpdateDeductOutBD(SqlTransaction tran, string number, int IsDeduct, double DeductMoney)
        {
            string mid = "";

            if (IsDeduct == 4) // 4 扣款消费  5 补款消费  6 扣重购  7 补重购 
                mid = "[TotalDefray]=[TotalDefray]+" + DeductMoney;
            if (IsDeduct == 5) mid = "TotalRemittances =TotalRemittances +" + DeductMoney;
            if (IsDeduct == 6) mid = "xuhao =xuhao +" + DeductMoney;
            if (IsDeduct == 7) mid = "zzye =zzye +" + DeductMoney;
            if (IsDeduct == 8) mid = "pointBOut =pointBOut +" + DeductMoney;
            if (IsDeduct == 9) mid = "pointBIn =pointBIn +" + DeductMoney;
            if (IsDeduct == 10) mid = "fuxiaothout =fuxiaothout +" + DeductMoney;
            if (IsDeduct == 11) mid = "fuxiaothin =fuxiaothin +" + DeductMoney;
            if (IsDeduct == 12) mid = "pointAOut =pointAOut +" + DeductMoney;
            if (IsDeduct == 13) mid = "pointAIn =pointAIn +" + DeductMoney;
            int cc = 0;
            if (mid != "") cc = DBHelper.ExecuteNonQuery(tran, "update memberinfo set " + mid + " where number='" + number + "'");


            return cc > 0 ? true : false;
        }
        /// <summary>
        /// 获取系统开关的对应列表到临时表##setsys--CK
        /// </summary>
        /// <returns></returns>
        public static bool GetSystemList()
        {
            bool res = false;
            string sql = "CREATE TABLE setsys (id int not null,SystemValue int not null) insert into setsys select id,SystemValue from setsystem";
            int ret = DBHelper.ExecuteNonQuery(sql);
            if (ret > 0)
            {
                res = true;
            }
            return res;
        }
        /// <summary>
        /// 更新系统开关全部为空--CK
        /// </summary>
        /// <returns></returns>
        public static bool UpdateSystem()
        {
            bool res = false;
            try
            {
                string sql = "update setsystem set systemvalue=0";
                if (DBHelper.ExecuteNonQuery(sql) > 0)
                {
                    res = true;
                }
            }
            catch { res = false; }
            return res;
        }
        /// <summary>
        /// 还原原有系统开关--CK
        /// </summary>
        /// <param name="list">原有系统列表</param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static bool UpdateSystemID()
        {
            bool res = false;
            try
            {
                string sql = "update s set s.systemvalue=t.systemvalue from setsystem as s,setsys as t where s.id=t.id";
                int ret = DBHelper.ExecuteNonQuery(sql);
                if (ret > 0)
                {
                    res = true;
                }

            }
            catch
            {
                res = false;
            }
            return res;
        }
        /// <summary>
        /// 验证是否删除系统开关临时表##setsys
        /// </summary>
        /// <returns></returns>
        public static bool CheckSetsys()
        {
            bool res = false;
            try
            {
                string sql = "select * from setsys";
                DataTable dt = DBHelper.ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    res = false;
                }

            }
            catch
            {
                res = true;
            }
            return res;
        }
        /// <summary>
        /// 删除系统开关临时表##setsys--CK
        /// </summary>
        /// <returns></returns>
        public static bool DelSetsys()
        {
            bool res = false;
            string sql1 = "drop table setsys";
            int ret = DBHelper.ExecuteNonQuery(sql1);
            if (ret < 0)
            {
                res = true;
            }
            else
            {
                res = false;
            }
            return res;
        }
    }
}