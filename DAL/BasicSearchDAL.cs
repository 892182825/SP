using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
   public  class BasicSearchDAL
    {
       public BasicSearchDAL() { }
       /// <summary>
       /// 获取会员结算信息
       /// ExpectNum:期数  Level：级别 MemberInfoBalanceN：结算表  Number：编号
       /// </summary>
       /// <returns></returns>
       public DataTable GetMemberInfoBalance(string ExpectNum, string Level, string MemberInfoBalanceN, string Number, params double[] Currency)
       {
           string sql=string.Empty;
           if(Currency.Length==0)
             sql = @"select  '" + ExpectNum + "' as qishu,h1.Number,h2.Name,'" + Level + "' as  strJiBie," +
                        "isnull(h1.TotalNetNum,0) TotalNetNum,isnull(h1.CurrentNewNetNum,0) CurrentNewNetNum,isnull(h1.CurrentTotalNetRecord,0) CurrentTotalNetRecord,isnull(h1.TotalNetRecord,0) TotalNetRecord,isnull(h1.CurrentOneMark,0) CurrentOneMark,isnull(h1.TotalOneMark,0) TotalOneMark," +
                        "isnull(h1.Bonus0,0)  as  jiangjin0," +
                        "isnull(h1.Bonus1,0)  as  jiangjin1," +
                        "isnull(h1.Bonus2,0)  as  jiangjin2," +
                        "isnull(h1.Bonus3,0)  as  jiangjin3," +
                        "isnull(h1.Bonus4,0)  as  jiangjin4," +
                        "isnull(h1.Bonus5,0)  as  jiangjin5," +
                        "isnull(h1.CurrentTotalMoney,0)     as  zongji," +
                        "isnull(h1.DeductTax,0)    as  koushui," +
                        "isnull(h1.DeductMoney,0)    as  koukuan," +
                        "isnull(h1.CurrentSolidSend,0)      as  shifa," +
                        "isnull(h1.BonusAccumulation,0)  as  zongji_lj," +
                        "isnull(h1.SolidSendAccumulation,0)   as  shifa_lj  " +
                        "from  " + MemberInfoBalanceN + "  h1,MemberInfo  h2  where  h1.Number=h2.Number  and  h1.Number='" + Number + "'";
           else
               sql = @"select  '" + ExpectNum + "' as qishu,h1.Number,h2.Name,'" + Level + "' as  strJiBie," +
                          "isnull(h1.TotalNetNum,0) TotalNetNum,isnull(h1.CurrentNewNetNum,0) CurrentNewNetNum,isnull(h1.CurrentTotalNetRecord,0) CurrentTotalNetRecord,isnull(h1.TotalNetRecord,0) TotalNetRecord,isnull(h1.CurrentOneMark,0) CurrentOneMark,isnull(h1.TotalOneMark,0) TotalOneMark," +
               "" + Currency[0] + "*isnull(h1.Bonus0,0)  as  jiangjin0," +
               "" + Currency[0] + "*isnull(h1.Bonus1,0)  as  jiangjin1," +
               "" + Currency[0] + "*isnull(h1.Bonus2,0)  as  jiangjin2," +
               "" + Currency[0] + "*isnull(h1.Bonus3,0)  as  jiangjin3," +
               "" + Currency[0] + "*isnull(h1.Bonus4,0)  as  jiangjin4," +
               "" + Currency[0] + "*isnull(h1.Bonus5,0)  as  jiangjin5," +
               "" + Currency[0] + "*isnull(h1.CurrentTotalMoney,0)     as  zongji," +
               "" + Currency[0] + "*isnull(h1.DeductTax,0)    as  koushui," +
               "" + Currency[0] + "*isnull(h1.DeductMoney,0)    as  koukuan," +
               "" + Currency[0] + "*isnull(h1.CurrentSolidSend,0)      as  shifa," +
               "" + Currency[0] + "*isnull(h1.BonusAccumulation,0)  as  zongji_lj," +
               "" + Currency[0] + "*isnull(h1.SolidSendAccumulation,0)   as  shifa_lj  " +
               "from  " + MemberInfoBalanceN + "  h1,MemberInfo  h2  where  h1.Number=h2.Number  and  h1.Number='" + Number + "'";

           return  DBHelper.ExecuteDataTable(sql);
       }

       public Object GetDataByCmdText(string sql)
       {
            return DBHelper.ExecuteScalar(sql);
       }
       /// <summary>
       /// 获取会员编号
       /// </summary>
       /// <returns></returns>
       public Object GetMemberNumber(string dian)
       {
           string sql = "select Number  from  MemberInfo  where  storeid=@StoreID";
           SqlParameter parm = new SqlParameter("@StoreID", SqlDbType.NVarChar, 40);
           parm.Value = dian;
           return DBHelper.ExecuteScalar(sql,parm,CommandType.Text);
       }

       /// <summary>
       /// 获取会员编号
       /// </summary>
       /// <returns></returns>
       public Object GetStoreNumber(string dian)
       {
           string sql = "select Number  from  storeInfo  where  storeid=@StoreID";
           SqlParameter parm = new SqlParameter("@StoreID", SqlDbType.NVarChar, 40);
           parm.Value = dian;
           return DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
       }
       /// <summary>
       /// 获取级别
       /// </summary>
       /// <param name="MemberInfoBalanceN"></param>
       /// <returns></returns>
       public Object GetLevel(string MemberInfoBalanceN)
       {
           string sql = "select isnull(Level,0) from " + MemberInfoBalanceN;
           return this.GetDataByCmdText(sql);
       }

       public int GetExpectNum(string qishu)
       {
           string sql = "Select IsSuance from config where expectNum=@num";
           SqlParameter spa = new SqlParameter("@num",SqlDbType.Int);
           spa.Value = Convert.ToInt32(qishu);
           return (int)DBHelper.ExecuteScalar(sql,spa,CommandType.Text); 
       }
       /// <summary>
       /// 返回系统最大期数
       /// </summary>
       /// <returns></returns>
       public int GetMaxExpectNum()
       {
           string sql = @"select max(expectNum) from config";
           return (int)DBHelper.ExecuteScalar(sql);
       }

       /// <summary>
       /// 查询会员的奖金——ds2012——tianfeng
       /// </summary>
       /// <param name="ExpectNum"></param>
       /// <param name="number"></param>
       /// <returns></returns>
       public DataTable GetMemberBalance(string ExpectNum,string number)
       {
           string sql = "select number," + ExpectNum + " as ExpectNum,CurrentOneMark,CurrentTotalNetRecord,DCurrentNewNetNum,Bonus0,Bonus1,Bonus2,Bonus3,Bonus4,Bonus5,Bonus6,Bonus7,Bonus8,CurrentTotalMoney,DeductTax,CurrentSolidSend,Kougl,Koufl,Koufx from MemberInfoBalance" + ExpectNum + " where number=@num";
           SqlParameter[] spa = new SqlParameter[]{new SqlParameter("@num", SqlDbType.NVarChar,50)};
           spa[0].Value = number;
           return DBHelper.ExecuteDataTable(sql,spa,CommandType.Text);
       }
    }
}
