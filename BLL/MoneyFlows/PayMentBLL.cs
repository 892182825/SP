using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;

using Model;
/*
 * 工资汇兑
 * **/
namespace BLL.MoneyFlows
{
    public class PayMentBLL
    {
        /// <summary>
        /// 获取账户信息
        /// </summary>
        /// <param name="money">电子钱包金额</param>
        /// <param name="bank">所在银行</param>
        /// <returns></returns>
        public static DataTable GetMemberInfo(double money,string bank,string bankCountry)
        {
            return ReleaseDAL.GetMemberInfo(money, bank, bankCountry);
        }
        /// <summary>
        /// 验证是否可以发放了
        /// select COUNT(1) from h_Info where release ="+vqs.ToString()
        /// </summary>
        /// <returns></returns>
        public Boolean ValidateMemeberInfo(int Release)
        {
            return true;
        }

        /// <summary>
        /// 发放奖金——ds2012——tianfeng
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <param name="money">金额</param>
        /// <param name="bank">银行</param>
        /// <returns></returns>
        public static Boolean UpMemberInfo(int ExpectNum, double money, string grant, string ip, string operaternum)
        {
            return ReleaseDAL.UpMemberInfo(ExpectNum, money, grant,ip,operaternum);
        }
        /// <summary>
        /// 根据电子钱包金额查询用户信息——ds2012——tianfeng
        /// </summary>
        /// <param name="money">金额</param>
        /// <returns></returns>
        public static DataTable GetMemberInfoByMoney(double money)
        {
            return ReleaseDAL.GetMemberInfoByMoney(money);
        }
    }
}
