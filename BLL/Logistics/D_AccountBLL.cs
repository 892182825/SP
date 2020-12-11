using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

namespace BLL.Logistics
{
    public class D_AccountBLL
    {
        /// <summary>
        /// 对账单带事务——ds2012——tianfeng
        /// </summary>
        /// <param name="number">店铺或会员编号</param>
        /// <param name="money">交易金额</param>
        /// <param name="sftype">交易类型</param>
        /// <param name="kmtype">科目</param>
        /// <param name="direction">是进还是出</param>
        public static void AddAccount(string number,double money,D_AccountSftype sftype,D_AccountKmtype kmtype,DirectionEnum direction,string remark,SqlTransaction tran)
        {
            
            D_AccountDAL.AddAccount(number,money,sftype,kmtype,direction,remark,tran);
        }

        /// <summary>
        /// 对账单带事务——ds2012——tianfeng
        /// </summary>
        /// <param name="number">店铺或会员编号</param>
        /// <param name="money">交易金额</param>
        /// <param name="sftype">交易类型</param>
        /// <param name="kmtype">科目</param>
        /// <param name="direction">是进还是出</param>
        public static void AddAccount(string number, double money, D_AccountSftype sftype, D_AccountKmtype kmtype, DirectionEnum direction, string remark, SqlTransaction tran, bool state)
        {

            D_AccountDAL.AddAccount(number, money, sftype, kmtype, direction, remark, tran, state);
        }

        /// <summary>
        ///  获取订单是否请求发货
        /// </summary>
        /// <param name="direction">是进还是出</param>
        public static int GetDeliveryflag(string OrderId, SqlTransaction tran)
        {

           return D_AccountDAL.GetDeliveryflag(OrderId, tran);
        }



        /// <summary>
        /// 对账单不带事务——ds2012——tianfeng重载-CK
        /// </summary>
        /// <param name="number">店铺或会员编号</param>
        /// <param name="money">交易金额</param>
        /// <param name="sftype">交易类型</param>
        /// <param name="kmtype">科目</param>
        /// <param name="direction">是进还是出</param>
        public static void AddAccount1(string number, double money, D_AccountSftype sftype, D_Sftype Dsftype, D_AccountKmtype kmtype, DirectionEnum direction, string remark)
        {
            D_AccountDAL.AddAccount1(number, money, sftype,Dsftype, kmtype, direction, remark);
        }

         /// <summary>
        /// 带事务报单款对账单——ds2012——CK
        /// </summary>
        /// <param name="number"></param>
        /// <param name="money"></param>
        /// <param name="sftype"></param>
        /// <param name="kmtype"></param>
        /// <param name="direction"></param>
        /// <param name="str"></param>
        public static int AddAccountTran(string number, double money, D_AccountSftype sftype, D_Sftype Dsftype, D_AccountKmtype kmtype, DirectionEnum direction, string remark, SqlTransaction tran)
        { return D_AccountDAL.AddAccountTran(number, money, sftype, Dsftype, kmtype, direction, remark, tran); }

        /// <summary>
        /// 对账单不带事务——ds2012——tianfeng
        /// </summary>
        /// <param name="number">店铺或会员编号</param>
        /// <param name="money">交易金额</param>
        /// <param name="sftype">交易类型</param>
        /// <param name="kmtype">科目</param>
        /// <param name="direction">是进还是出</param>
        public static void AddAccount(string number, double money, D_AccountSftype sftype, D_AccountKmtype kmtype, DirectionEnum direction, string remark)
        {
            D_AccountDAL.AddAccount(number, money, sftype, kmtype, direction, remark);
        }

         /// <summary>
        /// 服务机构对账单--ck
        /// </summary>
        /// <param name="number">店铺或会员编号</param>
        /// <param name="money">交易金额</param>
        /// <param name="sftype">交易类型</param>
        /// <param name="kmtype">科目</param>
        /// <param name="direction">是进还是出</param>
        public static void AddStoreAccount(string number, double money, D_AccountSftype sftype, S_Sftype Ssftype, D_AccountKmtype kmtype, DirectionEnum direction, string str)
        {
            D_AccountDAL.AddStoreAccount(number, money, sftype, Ssftype, kmtype, direction, str);
        }
        /// <summary>
        /// 服务机构对账单--ck--带事务
        /// </summary>
        /// <param name="number">店铺或会员编号</param>
        /// <param name="money">交易金额</param>
        /// <param name="sftype">交易类型</param>
        /// <param name="kmtype">科目</param>
        /// <param name="direction">是进还是出</param>
        public static int AddStoreAccount(string number, double money, D_AccountSftype sftype, S_Sftype Ssftype, D_AccountKmtype kmtype, DirectionEnum direction, string str,SqlTransaction tran)
        {
           return D_AccountDAL.AddStoreAccount(number, money, sftype, Ssftype, kmtype, direction, str,tran);
        }

        /// <summary>
        /// 获取科目的具体名称 --程凯 2012-6-29
        /// </summary>
        /// <param name="enumItem">具体枚举类型</param>
        /// <returns></returns>
        public static string GetKmtype(string kmtype) {
            string res = "";
            try
            {
                if (int.Parse(kmtype) >= 0 && int.Parse(kmtype) <= 40)
                {
                    res = new BLL.TranslationBase().GetTran(DAL.DBHelper.ExecuteScalar("select isnull(SubjectName,'') as SubjectName  from AccountSubject where SubjectID=" + kmtype).ToString(), "");
                }
                else {
                    res = "";
                }
            }
            catch
            {
                res = "";
            }
            return res;
        }

        public static string GetD_AccountSftypeStr(D_AccountSftype sftype)
        {
            string str = "";
            switch (sftype)
            {
                case D_AccountSftype.Branch: str = BLL.Translation.Translate("006546", "分公司"); break;
                case D_AccountSftype.Company: str = BLL.Translation.Translate("001824", "总公司"); break;
                case D_AccountSftype.MemberType: str = BLL.Translation.Translate("000599", "会员"); break;
                case D_AccountSftype.StoreType: str = BLL.Translation.Translate("000388", "店铺"); break;
            }
            return str;
        }
        /// <summary>
        /// 现金对账单——ds2012——tianfeng
        /// </summary>
        /// <param name="number">店铺或会员编号</param>
        /// <param name="money">交易金额</param>
        /// <param name="sftype">交易类型</param>
        /// <param name="kmtype">科目</param>
        /// <param name="direction">是进还是出</param>
        public static void AddAccountWithdraw(string number, double money, D_AccountSftype sftype, D_Sftype Dsftype, D_AccountKmtype kmtype, DirectionEnum direction, string remark)
        {

            D_AccountDAL.AddAccountWithdraw1(number, money, sftype,Dsftype, kmtype, direction, remark);
        }

        /// <summary>
        /// 现金对账单——ds2012——tianfeng--带事务
        /// </summary>
        /// <param name="number">店铺或会员编号</param>
        /// <param name="money">交易金额</param>
        /// <param name="sftype">交易类型</param>
        /// <param name="kmtype">科目</param>
        /// <param name="direction">是进还是出</param>
        public static int AddAccountWithdrawTran(string number, double money, D_AccountSftype sftype, D_Sftype Dsftype, D_AccountKmtype kmtype, DirectionEnum direction, string remark,SqlTransaction tran)
        {

           return D_AccountDAL.AddAccountWithdraw1(number, money, sftype, Dsftype, kmtype, direction, remark, tran);
        }

        /// <summary>
        /// 获得支付方式——ds2012——tianfeng
        /// </summary>
        /// <param name="kmtype"></param>
        /// <returns></returns>
        public static string GetPaymentstr(PaymentEnum peyment)
        {
            string str = "";
            switch (peyment)
            {
                case PaymentEnum.CompanyRecord: str = BLL.Translation.Translate("005963", "在线支付"); break;
                case PaymentEnum.BankTransfer: str = BLL.Translation.Translate("007594", "普通汇款"); break;
                case PaymentEnum.Alipay: str = BLL.Translation.Translate("007595", "汇款人工确认"); break;
            }
            return str;
        }
        /// <summary>
        /// 获取期末余额总计
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static double GetTotalBalancemoney(int type)
        {
            return D_AccountDAL.GetTotalBalancemoeny(type);
        }
        /// <summary>
        /// 获取所有会员的报单账户余额总计和现金账户余额总计
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllMemberAccountMoney()
        {
            return D_AccountDAL.GetAllMemberAccountMoney();
        }
    }
}
