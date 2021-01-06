using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using DAL;
using Model.Other;

//张振
namespace BLL.MoneyFlows
{
    /// <summary>
    /// 预收帐款
    /// </summary>
    public class RemittancesBLL
    {
        /// <summary>
        /// 应收账款分页
        /// </summary>
        /// <param name="pagin">分页帮助类</param>
        /// <param name="key">键</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static IList<StoreInfoModel> GetStoreOrderList(PaginationModel pagin,string tableName,string key, string condition)
        {
            string cloumns = " id,StoreID ,Name ,StoreName ,StoreAddress ";
            return RemittancesDAL.GetStoreOrderList(pagin, tableName, key, cloumns, condition);
        }
        /// <summary>
        /// 预收账款分页
        /// </summary>
        /// <param name="pagin">分页帮助类</param>
        /// <param name="key">键</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static IList<RemittancesModel> ReceiveOrderList(PaginationModel pagin, string tableName, string key, string condition)
        {
            string cloumns = " Remittances.StoreID, Remittances.Sender,Remittances.Managers,Remittances.ImportBank, Remittances.PayWay,Remittances.RemittancesNumber,Remittances.[Use],Remittances.StandardCurrency,Remittances.ConfirmType, Remittances.SenderID, Remittances.RemitMoney,Remittances.PayExpectNum,Remittances.Id,Remittances.ReceivablesDate,Remittances.PayExpectNum,Remittances.isgsqr,Remittances.Remark ";
            return RemittancesDAL.ReceiveOrderList(pagin, tableName, key, cloumns, condition);
        }

        /// <summary>
        /// 汇款合计总额
        /// </summary>
        /// <param name="summoney">总金额</param>
        /// <param name="currencyname">货币类型名称</param>
        public static void TotalMoney(out double summoney, out string currencyname)
        {
            RemittancesDAL.TotalMoney(out summoney, out currencyname);
        }
        /// <summary>
        /// 更新店铺汇款额，预收账款——ds2012——tianfeng
        /// </summary>
        ///<param name="type">汇款ID</param>
        /// <param name="money">汇款金额</param>
        /// <param name="storeID">汇款店铺</param>
        /// <param name="storeID">操作者IP</param>
        /// <param name="storeID">操作者编号</param>
        public static void Auditing(int id, double money, string storeID, string OperateIP, string OperateNum)
        {
            RemittancesDAL.Auditing(id, money, storeID, OperateIP, OperateNum);
        }
        /// <summary>
        /// 更新会员汇款额，预收账款——ds2012——tianfeng
        /// </summary>
        ///<param name="type">汇款ID</param>
        /// <param name="money">汇款金额</param>
        /// <param name="storeID">汇款会员</param>
        /// <param name="storeID">操作者IP</param>
        /// <param name="storeID">操作者编号</param>
        public static void MemberAuditing(int id, double money, string number, string OperateIP, string OperateNum)
        {
            RemittancesDAL.MemberAuditing(id, money, number, OperateIP, OperateNum);
        }
        /// <summary>
        /// 店铺汇款单是否存在——ds2012——tianfeng
        /// </summary>
        /// <param name="id">汇款单编号</param>
        /// <returns></returns>
        public static bool IsExist(int id)
        {
            return RemittancesDAL.IsExist(id);
        }
        /// <summary>
        /// 会员汇款单是否存在——ds2012——tianfeng
        /// </summary>
        /// <param name="id">汇款单编号</param>
        /// <returns></returns>
        public static bool MemberIsExist(int id)
        {
            return RemittancesDAL.MemberIsExist(id);
        }
        /// <summary>
        /// 判断店铺汇款是否以审核——ds2012——tianfeng
        /// </summary>
        /// <param name="id">汇款id</param>
        /// <returns></returns>
        public static object IsGSQR(int id)
        {
            return RemittancesDAL.IsGSQR(id);
        }
        /// <summary>
        /// 判断会员汇款是否以审核——ds2012——tianfeng
        /// </summary>
        /// <param name="id">汇款id</param>
        /// <returns></returns>
        public static object IsMemberGSQR(int id)
        {
            return RemittancesDAL.IsMemberGSQR(id);
        }
        /// <summary>
        /// 根据汇单号判断是否以审核
        /// </summary>
        /// <param name="huidan">汇单号</param>
        /// <returns></returns>
        public static object IsGSQRMemberByHuidan(string huidan)
        {
            return RemittancesDAL.IsGSQRMemberByHuidan(huidan);
        }
        /// <summary>
        /// 根据汇单号判断是否以审核
        /// </summary>
        /// <param name="huidan">汇单号</param>
        /// <returns></returns>
        public static object IsGSQRByHuidan(string huidan)
        {
            return RemittancesDAL.IsGSQRByHuidan(huidan);
        }
        /// <summary>
        /// 根据汇单号获取店铺信息
        /// </summary>
        /// <param name="storeID">汇单号</param>
        /// <returns></returns>
        public static RemittancesModel GetRemitByHuidan(string huidan)
        {
            return RemittancesDAL.GetRemitByHuidan(huidan);
        }
        /// <summary>
        /// 根据汇单号获取会员信息——ds2012——tianfeng
        /// </summary>
        /// <param name="storeID">汇单号</param>
        /// <returns></returns>
        public static RemittancesModel GetMemberRemitByID(int id)
        {
            return RemittancesDAL.GetMemberRemitByID(id);
        }
        /// <summary>
        /// 根据汇单号获取店铺信息——ds2012——tianfeng
        /// </summary>
        /// <param name="storeID">汇单号</param>
        /// <returns></returns>
        public static RemittancesModel GetRemitByID(int id)
        {
            return RemittancesDAL.GetRemitByID(id);
        }

        /// <summary>
        /// 删除店铺未审核汇款——ds2012——tianfeng
        /// </summary>
        /// <param name="id">汇款编号</param>
        public static void DeleteMoney(int id)
        {
            RemittancesDAL.DeleteMoney(id);
        }
        /// <summary>
        /// 删除会员未审核汇款——ds2012——tianfeng
        /// </summary>
        /// <param name="id">汇款编号</param>
        public static void DeleteMemberMoney(int id)
        {
            RemittancesDAL.DeleteMemberMoney(id);
        }
        
        /// <summary>
        /// 添加汇款，更新店铺金额——ds2012——tianfeng
        /// </summary>
        /// <param name="info">汇款信息对象</param>
        /// <param name="RateName1">店铺货币汇率名称</param>
        /// <param name="RateName2">实际付款汇率名称</param>
        public static void AddRemittances(RemittancesModel info, string RateName1, string RateName2,out int id)
        {
            RemittancesDAL.AddRemittances(info, RateName1, RateName2,out id);
        }

        /// <summary>
        /// 添加会员汇款，更新会员金额——ds2012——tianfeng
        /// </summary>
        /// <param name="info">汇款信息对象</param>
        /// <param name="RateName1">店铺货币汇率名称</param>
        /// <param name="RateName2">实际付款汇率名称</param>
        public static void AddMemberRemittances(RemittancesModel info, string RateName1, string RateName2, out int id)
        {
            RemittancesDAL.AddMemberRemittances(info, RateName1, RateName2, out id);
        }


        /// <summary>
        /// 添加会员汇款，更新会员金额——ds2012——CK--带事务
        /// </summary>
        /// <param name="info">汇款信息对象</param>
        /// <param name="RateName1">店铺货币汇率名称</param>
        /// <param name="RateName2">实际付款汇率名称</param>
        public static void AddMemberRemittancesTran(RemittancesModel info, string RateName1, string RateName2, out int id,SqlTransaction tran)
        {
            RemittancesDAL.AddMemberRemittancesTran(info, RateName1, RateName2, out id, tran);
        }

        /// <summary>
        /// 根据店铺编号获取店铺信息
        /// </summary>
        /// <param name="storeID">店铺编号</param>
        /// <returns></returns>
        public static RemittancesModel GetRemitByStoreID(string storeID)
        {
            return RemittancesDAL.GetRemitByStoreID(storeID);
        }

        /// <summary>
        /// 获得银行——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static DataTable GetBank()
        {
            return RemittancesDAL.GetBank();
        }
        /// <summary>
        /// 获取币种——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static IList<CurrencyModel> GetCurrency()
        {
            return RemittancesDAL.GetCurrency();
        }
        /// <summary>
        /// 获取汇率——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static double GetCurrency(int id)
        {
            return RemittancesDAL.GetCurrency(id);
        }
        /// <summary>
        /// 获取系统的标准币种——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static string GetCurrencyNameByStoreID()
        {
            return RemittancesDAL.GetCurrencyNameByStoreID();
        }
        /// <summary>
        /// 汇款申报，更新店铺金额——ds2012——tianfeng
        /// </summary>
        /// <param name="info">汇款信息对象</param>
        /// <param name="RateName1">店铺货币汇率名称</param>
        /// <param name="RateName2">实际付款汇率名称</param>
        public static int RemitDeclare(RemittancesModel info, string RateName1, string RateName2)
        {
           return  RemittancesDAL.RemitDeclare(info, RateName1, RateName2);
        }

        /// <summary>
        /// 金流查询能匹配的数据
        /// </summary>
        /// <param name="HkID"></param>
        /// <param name="bishu"></param>
        public static int jinliucx(string HkID )
        {
            int res = 0;
            try
            { 
        
            SqlParameter[] parm = new SqlParameter[] {
            new SqlParameter("@hkid",SqlDbType.NVarChar,50) 
            };
            parm[0].Value = HkID;  
             DAL.DBHelper.ExecuteNonQuery("zfpp", parm, CommandType.StoredProcedure);
                res = 1;
            }
            catch (Exception)
            {
               
                res =-1;
                
            }
            return res;
        }
        /// <summary>
        /// 0 成功 ，1 不成功
        /// </summary>
        /// <param name="hkid"></param>
        /// <returns></returns>
        public static int jiliuZZ(string hkid)
        {
            //string sql = "update Remittances set shenhestate=-1 where ID=@id";

            //return DBHelper.ExecuteNonQuery(sql, new SqlParameter[]{
            //    new SqlParameter("@id", hkid)
            //}, CommandType.Text);
            SqlParameter[] par = new SqlParameter[]{
                new SqlParameter("@hkid",hkid),
                new SqlParameter("@err",SqlDbType.Int)
            };
            par[1].Direction = ParameterDirection.Output;
            DAL.DBHelper.ExecuteNonQuery("zfpp_cx", par, CommandType.StoredProcedure);
            int num = Convert.ToInt32(par[1].Value);
            return num;
        }

        ///// <summary>
        ///// 汇款申报，更新会员金额——ds2012——tianfeng
        ///// </summary>
        ///// <param name="info">汇款信息对象</param>
        ///// <param name="RateName1">货币汇率名称</param>
        ///// <param name="RateName2">实际付款汇率名称</param>
        //public static void RemitDeclare(RemittancesModel info, string RateName1, string RateName2)
        //{
        //    RemittancesDAL.RemitDeclare(info, RateName1, RateName2);
        //}

        /// <summary>
        /// 汇款申报，更新会员金额
        /// </summary>
        /// <param name="info">汇款信息对象</param>
        /// <param name="RateName1">货币汇率名称</param>
        /// <param name="RateName2">实际付款汇率名称</param>
        public static bool RemitDeclare(SqlTransaction tran, RemittancesModel info, string RateName1, string RateName2)
        {
            return RemittancesDAL.RemitDeclare(tran, info, RateName1, RateName2);
        }

        /// <summary>
        /// 绑定国家——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static IList<CountryModel> BindCountry_List()
        {
            return RemittancesDAL.BindCountry_List();
        }

        public static string GetCurrencyByID(int type)
        {
            return RemittancesDAL.GetCurrencyByID(type);
        }

        /// <summary>
        /// 判断汇单号是否存在——ds2012——tianfeng
        /// </summary>
        /// <param name="huidan">汇单号</param>
        /// <returns></returns>
        public static bool isExistsHuiDan(string huidan)
        {
            return RemittancesDAL.isExistsHuiDan(huidan);
        }
        /// <summary>
        /// 判断汇单号是否存在——ds2012——tianfeng
        /// </summary>
        /// <param name="huidan">汇单号</param>
        /// <returns></returns>
        public static bool isMemberExistsHuiDan(string huidan)
        {
            return RemittancesDAL.isMemberExistsHuiDan(huidan);
        }
        /// <summary>
        /// 判断会员汇单号是否存在
        /// </summary>
        /// <param name="huidan">汇单号</param>
        /// <returns></returns>
        public static bool MemberisExistsHuiDan(string huidan)
        {
            return RemittancesDAL.MemberisExistsHuiDan(huidan);
        }
        /// <summary>
        /// 获得会员汇款相应币种的总金额
        /// </summary>
        /// <param name="isqueren"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static DataTable GetTotalMoneyorCurrency(string isqueren, DateTime begin, DateTime end, string number)
        {
            return RemittancesDAL.GetTotalMoneyorCurrency(isqueren,begin,end,number);
        }
        /// <summary>
        /// 电子转账
        /// </summary>
        /// <param name="info">汇款信息对象</param>
        /// <param name="RateName1">货币汇率名称</param>
        /// <param name="RateName2">实际付款汇率名称</param>
        public static void RemitEFT(RemittancesModel info, string RateName1, string RateName2)
        {
            RemittancesDAL.RemitEFT(info, RateName1, RateName2);
        }
        /// <summary>
        /// 奖金转店铺
        /// </summary>
        /// <param name="info"></param>
        /// <param name="RateName1"></param>
        /// <param name="RateName2"></param>
        public static void EFT(RemittancesModel info,string RateName1, string RateName2)
        {
            RemittancesDAL.EFT(info,RateName1,RateName2);
        }
        ///// <summary>
        ///// 会员奖金转店铺
        ///// </summary>
        ///// <param name="info"></param>
        ///// <param name="RateName1"></param>
        ///// <param name="RateName2"></param>
        //public static void EFT(RemittancesModel info, string RateName1, string RateName2)
        //{
        //    RemittancesDAL.EFT(info, RateName1, RateName2);
        //}
        public static bool EFT(RemittancesModel info, string RateName1, string RateName2,string storeID, SqlTransaction tran)
        {
            return RemittancesDAL.EFT(info, RateName1, RateName2,storeID,tran);
        }
        /// <summary>
        /// 店铺是否存在
        /// </summary>
        /// <param name="id">店铺编号</param>
        /// <returns></returns>
        public static bool IsStoreExist(string number)
        {
            return RemittancesDAL.IsStoreExist(number);
        }

        /// <summary>
        /// 店铺是否存在
        /// </summary>
        /// <param name="id">店铺编号</param>
        /// <returns></returns>
        public static bool IsMemberExist(string number)
        {
            return RemittancesDAL.IsMemberExist(number);
        }

        /// <summary>
        /// 店铺是否存在
        /// </summary>
        /// <param name="id">店铺编号</param>
        /// <returns></returns>
        public static bool IsMemberExist(string number, string Name)
        {
            return RemittancesDAL.IsMemberExist(number,Name);
        }

        /// <summary>
        /// 店铺是否存在
        /// </summary>
        /// <param name="id">店铺编号</param>
        /// <param name="Name">店长姓名</param>
        /// <returns></returns>
        public static bool IsStoreExist(string number,string Name)
        {
            return RemittancesDAL.IsStoreExist(number, Name);
        }
        /// <summary>
        /// 根据会员编号获取店铺编号
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetStoreIDByNumber(string number)
        {
            return RemittancesDAL.GetStoreIDByNumber(number);
        }
        /// <summary>
        /// 根据汇单id获取信息
        /// </summary>
        /// <param name="huidan"></param>
        /// <returns></returns>
        public static RemittancesModel GetMemberRemittances(string huidan)
        {
            return RemittancesDAL.GetMemberRemittances(huidan);
        }

        /// <summary>
        /// 查询店铺信息——ds2012——tianfeng
        /// </summary>
        /// <param name="table"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static DataTable Quertystore(string table, string condition)
        {
            return RemittancesDAL.QueryStore(table, condition);
        }

        /// <summary>
        /// 根据银行编码查询银行名称——ds2012——tianfeng
        /// </summary>
        /// <param name="bankcode"></param>
        /// <returns></returns>
        public static string GetBankName(string bankcode)
        {
            return RemittancesDAL.QueryBankName(bankcode);
        }

        /// <summary>
        /// 根据付款用途统计金额（店铺汇款）——ds2012——tianfeng
        /// </summary>
        /// <param name="use"></param>
        /// <returns></returns>
        public static string GetUseToatal(int use)
        {
            return RemittancesDAL.GetUseTotal(use).ToString("f2");
        }

        /// <summary>
        /// 查询会员汇款总计——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static string GetTotalMemberRemittances()
        {
            return RemittancesDAL.GetTotalMemberRemittances().ToString("f2");
        }
        /// <summary>
        /// 通用的统计金额——ds2012——tianfeng
        /// </summary>
        /// <param name="clounms"></param>
        /// <param name="table"></param>
        /// <param name="sqlwhere"></param>
        /// <returns></returns>
        public static string GetTotalMoney(string clounms, string table, string sqlwhere)
        {
            return RemittancesDAL.GetTotalMoney(clounms, table, sqlwhere).ToString("f2");
        }
    }
}