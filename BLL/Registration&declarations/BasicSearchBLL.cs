using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;

namespace BLL.Registration_declarations
{
    public class BasicSearchBLL
    {
        public BasicSearchBLL() { }

        BasicSearchDAL basicSearchDAL = new BasicSearchDAL();

        /// <summary>
       /// 获取会员结算信息
        /// ExpectNum:期数  Level：级别 MemberInfoBalanceN：结算表  Number：编号 Currency:货币利率
       /// </summary>
       /// <returns></returns>
        public DataTable GetMemberInfoBalance(string ExpectNum, string Level, string MemberInfoBalanceN, string Number, params double [] Currency)
        {
            return basicSearchDAL.GetMemberInfoBalance(ExpectNum, Level, MemberInfoBalanceN, Number, Currency);
        }

        public Object GetDataByCmdText(string sql)
        {
            return basicSearchDAL.GetDataByCmdText(sql);
        }
        /// <summary>
       /// 获取会员编号
       /// </summary>
       /// <returns></returns>
        public Object GetMemberNumber(string dian)
        {
            return basicSearchDAL.GetMemberNumber(dian);
        }

        public Object GetStoreNumber(string dian)
        {

            return basicSearchDAL.GetStoreNumber(dian);
        }
         /// <summary>
       /// 获取级别
       /// </summary>
       /// <param name="MemberInfoBalanceN"></param>
       /// <returns></returns>
        public Object GetLevel(string MemberInfoBalanceN)
        {
            return basicSearchDAL.GetLevel(MemberInfoBalanceN);
        }

        public int GetExpectNum(string qishu)
        {
            return basicSearchDAL.GetExpectNum(qishu);
        }

        /// <summary>
       /// 返回系统最大期数
       /// </summary>
       /// <returns></returns>
        public int GetMaxExpectNum()
        {
            return basicSearchDAL.GetMaxExpectNum();
        }

        /// <summary>
        /// 查询会员的奖金——ds2012——tianfeng
        /// </summary>
        /// <param name="ExpectNum"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public DataTable GetMemberBalance(string ExpectNum,string number)
        {
            return basicSearchDAL.GetMemberBalance(ExpectNum,number);
        }
    }
}
