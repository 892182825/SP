using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using BLL.Registration_declarations;

namespace BLL.CommonClass
{
    public class PhoneRechargeBLL
    {
        /// <summary>
        /// 增加话费充值记录
        /// </summary>
        public static string AddRecharge(PhoneRecharge pr)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    int count = 0;
                    //验证账户余额
                    string sql = "select  isnull((jackpot-out-membership),0) from memberinfo where number =@number";
                    decimal balanceMoney = Convert.ToDecimal(DBHelper.ExecuteScalar(tran, sql, new SqlParameter[1] { new SqlParameter("@number", pr.Number) }, CommandType.Text));
                    if (balanceMoney < pr.AddMoney)
                    {
                        tran.Rollback();
                        return "1";//账户余额不足
                    }

                    //扣除会员账户金额
                    sql = "update  memberinfo set  out=out+@ordertotalmoney where  number=@number";
                    count += DBHelper.ExecuteNonQuery(tran, sql, new SqlParameter[2] { new SqlParameter("@ordertotalmoney", pr.AddMoney), new SqlParameter("@number", pr.Number) }, CommandType.Text);

                    //插入对账单
                    sql = @"insert into MemberAccount(number,happentime,happenmoney,BalanceMoney,direction,sftype, kmtype,remark) 
                            values (@number, @happentime, @happenmoney,@BalanceMoney,1,1,23, @number + '~008030~' + @phonenumber + '~008028~' + cast(@happenmoney as varchar)) ";
                    count += DBHelper.ExecuteNonQuery(tran, sql, new SqlParameter[5]{
                        new SqlParameter("@number",pr.Number),
                        new SqlParameter("@happentime",pr.AddTime),
                        new SqlParameter("@happenmoney",pr.AddMoney),
                        new SqlParameter("@BalanceMoney",(balanceMoney-pr.AddMoney)),
                        new SqlParameter("@phonenumber",pr.PhoneNumber),
                    }, CommandType.Text);

                    //插入充值记录
                    sql = "insert into PhoneRecharge(Number,PhoneNumber,AddMoney,AddState,AddTime,OperateIP,OperaterNum,RechargeID) values(@Number,@PhoneNumber,@AddMoney,@AddState,@AddTime,@OperateIP,@OperaterNum,@RechargeID) ";
                    SqlParameter[] paras = new SqlParameter[8];
                    paras[0] = new SqlParameter("@Number", pr.Number);
                    paras[1] = new SqlParameter("@PhoneNumber", pr.PhoneNumber);
                    paras[2] = new SqlParameter("@AddMoney", pr.AddMoney);
                    paras[3] = new SqlParameter("@AddState", pr.AddState);
                    paras[4] = new SqlParameter("@AddTime", pr.AddTime);
                    paras[5] = new SqlParameter("@OperateIP", pr.OperateIP);
                    paras[6] = new SqlParameter("@OperaterNum", pr.OperaterNum);
                    paras[7] = new SqlParameter("@RechargeID", pr.RechargeID);
                    count += DBHelper.ExecuteNonQuery(tran, sql, paras, CommandType.Text);

                    if (count > 2)
                    {
                        tran.Commit();
                        return "ok";
                    }
                    else
                    {
                        tran.Rollback();
                        return "fail";
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    string ee = ex.Message;
                    return "fail";
                }
            }
            return "fail";
        }

        /// <summary>
        /// 验证手机号码
        /// </summary>
        public static bool CheckPhoneNumber(string str_handset)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_handset, @"^[1]+[3,5]+\d{9}");
        }

        /// <summary>
        /// 充值失败，金额返还
        /// </summary>
        public static string FailRecharge(PhoneRecharge pr)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    int count = 0;

                    //扣除会员账户金额
                    string sql = "update  memberinfo set  jackpot=jackpot+@ordertotalmoney where  number=@number";
                    count += DBHelper.ExecuteNonQuery(tran, sql, new SqlParameter[2] { new SqlParameter("@ordertotalmoney", pr.AddMoney), new SqlParameter("@number", pr.Number) }, CommandType.Text);

                    //获取账户余额
                    sql = "select isnull((jackpot-out-membership),0)  from memberinfo where number =@number";
                    decimal balanceMoney = Convert.ToDecimal(DBHelper.ExecuteScalar(tran, sql, new SqlParameter[1] { new SqlParameter("@number", pr.Number) }, CommandType.Text));

                    //插入对账单
                    sql = @"insert into MemberAccount(number,happentime,happenmoney,BalanceMoney,direction,sftype, kmtype,remark) 
                            values (@number, @happentime, @happenmoney,@BalanceMoney,0,1,24, @number + '~008030~' + @phonenumber + '~008035~' + cast(@happenmoney as varchar)) ";
                    count += DBHelper.ExecuteNonQuery(tran, sql, new SqlParameter[5]{
                        new SqlParameter("@number",pr.Number),
                        new SqlParameter("@happentime",pr.AddTime),
                        new SqlParameter("@happenmoney",pr.AddMoney),
                        new SqlParameter("@BalanceMoney",balanceMoney),
                        new SqlParameter("@phonenumber",pr.PhoneNumber),
                    }, CommandType.Text);

                    //修改充值状态
                    sql = "update PhoneRecharge set AddState=@AddState wehre RechargeID=@RechargeID";
                    count += DBHelper.ExecuteNonQuery(tran, sql, new SqlParameter[2] { new SqlParameter("@AddState", pr.AddState), new SqlParameter("@RechargeID", pr.RechargeID) }, CommandType.Text);

                    if (count > 2)
                    {
                        tran.Commit();
                        return "ok";
                    }
                    else
                    {
                        tran.Rollback();
                        return "fail";
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    string ee = ex.Message;
                    return "fail";
                }
            }
            return "fail";
        }


        /// <summary>
        /// 根据充值ID查询充值记录
        /// </summary>
        public static PhoneRecharge FindPhoneRechargeByID(string rechargeID)
        {
            PhoneRecharge pr = new PhoneRecharge();
            DataTable dt = DBHelper.ExecuteDataTable("select * from PhoneRecharge where rechargeID=@rechargeID", new SqlParameter[1] { new SqlParameter("@rechargeID", rechargeID) }, CommandType.Text);
            pr.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
            pr.RechargeID = dt.Rows[0]["RechargeID"].ToString();
            pr.Number = dt.Rows[0]["Number"].ToString();
            pr.PhoneNumber = dt.Rows[0]["PhoneNumber"].ToString();
            pr.AddMoney = Convert.ToDecimal(dt.Rows[0]["AddMoney"]);
            pr.AddState = Convert.ToInt32(dt.Rows[0]["AddState"]);
            pr.AddTime = Convert.ToDateTime(dt.Rows[0]["AddTime"]);
            pr.OperateIP = dt.Rows[0]["OperateIP"].ToString();
            pr.OperaterNum = dt.Rows[0]["OperaterNum"].ToString();

            return pr;
        }

        /// <summary>
        /// 修改充值记录状态
        /// </summary>
        public static bool EditState(PhoneRecharge pr)
        {
            string sql = "update PhoneRecharge set AddState=@AddState where RechargeID=@RechargeID";
            return DBHelper.ExecuteNonQuery(sql, new SqlParameter[2] { new SqlParameter("@AddState", pr.AddState), new SqlParameter("@RechargeID", pr.RechargeID) }, CommandType.Text) > 0;
        }

        /// <summary>
        /// 生成充值单号
        /// </summary>
        public string GetRechargeID()
        {
            string rechargeID = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            Thread.Sleep(100);
            Random rd = new Random(unchecked((int)DateTime.Now.Ticks));
            string rdNum = rd.Next().ToString();
            if (rdNum.Length >= 8)
            {
                rdNum = rdNum.Substring(0, 8);
            }
            else
            {
                for (int i = rdNum.Length; i < 8; i++)
                {
                    rdNum = "0" + rdNum;
                }
            }

            rechargeID = rechargeID + rdNum;

            int count = Convert.ToInt32(DBHelper.ExecuteScalar("select count(0) from PhoneRecharge where RechargeID=@RechargeID", new SqlParameter[1] { new SqlParameter("@RechargeID", rechargeID) }, CommandType.Text));
            if (count > 0)
                GetRechargeID();

            return rechargeID;
        }

        public static PagerParmsInit FindPhoneRecharge(string sqlwhere)
        {
            PagerParmsInit model = new PagerParmsInit();
            model.PageIndex = 0;
            model.PageSize = 10;
            model.Key = "AddTime";
            model.ControlName = "gv_browOrder";
            model.PageTable = "PhoneRecharge";
            model.PageColumn = "id,rechargeid,number,phonenumber,addmoney,case addstate when 0 then '" + new TranslationBase().GetTran("008034", "手机充值失败") + "' when 1 then '" + new TranslationBase().GetTran("008036", "手机充值中") + "' when 2 then '" + new TranslationBase().GetTran("008037", "手机充值成功") + "' end as addstate,addtime,operateip,operaternum";
            model.SqlWhere = " 1=1 " + sqlwhere;

            return model;
        }
    }
}
