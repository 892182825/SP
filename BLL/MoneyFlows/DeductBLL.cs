using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Model;
using DAL;

using System.Data;
/*
 * 扣款、补款
 * 
 * **/

//张振
namespace BLL.MoneyFlows
{
    public class DeductBLL
    {
        /// <summary>
        /// 扣款原因
        /// </summary>
        /// <param name="expectnum">期数</param>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static object Reason(int expectnum, string number, int id)
        {
            return DeductDAL.Reason(expectnum, number, id);
        }
        /// <summary>
        /// 编号是否存在
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static bool IsExist(string number)
        {
            return DeductDAL.IsExist(number);
        }
        /// <summary>
        /// 添加扣款或补款信息
        /// </summary>
        /// <param name="info">扣补款对象</param>
        public static void AddInfo(DeductModel info)
        {
            DeductDAL.AddInfo(info);
        }
        /// <summary>
        /// 审核补扣款
        /// </summary>
        /// <param name="info">扣补款对象</param>
        public static bool UpdateInfoTran(DeductModel info)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (DeductDAL.UpdateDeduct(info, tran) <= 0)
                    {
                        tran.Rollback();
                        return false;
                    }
                    if (!ReleaseDAL.UpdateDeductOut(tran, info.Number, info.IsDeduct, info.DeductMoney))
                    {
                        tran.Rollback();
                        return false;
                    }
                    DirectionEnum de = DirectionEnum.AccountsIncreased;
                    if (info.IsDeduct == 0)
                    {
                        de = DirectionEnum.AccountReduced;
                    }
                    string remark = "";
                    if (info.IsDeduct == 0)
                    {
                        remark = "008021~【" + info.Number + "】~000153~" + info.ExpectNum + "~008022";
                        D_AccountDAL.AddAccountWithdraw1(info.Number, info.DeductMoney, D_AccountSftype.MemberType, D_Sftype.BounsAccount, D_AccountKmtype.AddMoneycut, de, remark, tran);
                    }
                    else
                    {
                        remark = "008021~【" + info.Number + "】~000153~" + info.ExpectNum + "~008023";
                        D_AccountDAL.AddAccountWithdraw1(info.Number, info.DeductMoney, D_AccountSftype.MemberType, D_Sftype.BounsAccount, D_AccountKmtype.AddMoneyget, de, remark, tran);
                    }

                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }


        /// <summary>
        /// 审核补扣款
        /// </summary>
        /// <param name="info">扣补款对象</param>
        public static bool UpdateInfoTranBD(DeductModel info)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (DeductDAL.UpdateDeduct(info, tran) <= 0)
                    {
                        tran.Rollback();
                        return false;
                    }
                    if (!ReleaseDAL.UpdateDeductOutBD(tran, info.Number, info.IsDeduct, info.DeductMoney))
                    {
                        tran.Rollback();
                        return false;
                    }
                    DirectionEnum de = DirectionEnum.AccountsIncreased;
                    if (info.IsDeduct == 4 || info.IsDeduct == 6 || info.IsDeduct == 8 || info.IsDeduct == 10 || info.IsDeduct == 12)
                    {
                        de = DirectionEnum.AccountReduced;
                    }
                    D_Sftype sf = D_Sftype.EleAccount;
                    D_AccountSftype dasf = D_AccountSftype.MemberCoshType;
                    if (info.IsDeduct == 4 || info.IsDeduct == 5) { dasf = D_AccountSftype.MemberCoshType; sf = D_Sftype.EleAccount; }
                    if (info.IsDeduct == 6 || info.IsDeduct == 7) { dasf = D_AccountSftype.zzye; sf = D_Sftype.zzye; }
                    if (info.IsDeduct == 8 || info.IsDeduct == 9) { dasf = D_AccountSftype.MemberTypeBd; sf = D_Sftype.baodanFTC; }
                    if (info.IsDeduct == 10 || info.IsDeduct == 11) { dasf = D_AccountSftype.MemberTypeFxth; sf = D_Sftype.CancellationofgoodsAccount; }
                    if (info.IsDeduct == 12 || info.IsDeduct == 13) { dasf = D_AccountSftype.usdtjj; sf = D_Sftype.usdtjj; }
                    string remark = "";
                    if (info.IsDeduct == 4 || info.IsDeduct == 6 || info.IsDeduct == 8 || info.IsDeduct == 10 || info.IsDeduct == 12)
                    {
                        remark = "008021~【" + info.Number + "】~000153~" + info.ExpectNum + "~008022";
                        D_AccountDAL.AddAccountWithdraw1(info.Number, info.DeductMoney, dasf, sf, D_AccountKmtype.AddMoneycut, de, remark, tran);
                    }
                    else if (info.IsDeduct == 5 || info.IsDeduct == 7 || info.IsDeduct == 9 || info.IsDeduct == 11 || info.IsDeduct == 13)
                    {
                        remark = "008021~【" + info.Number + "】~000153~" + info.ExpectNum + "~008023";
                        D_AccountDAL.AddAccountWithdraw1(info.Number, info.DeductMoney, dasf, sf, D_AccountKmtype.AddMoneyget, de, remark, tran);
                    }

                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
        public static bool AddDeduct(DeductModel info)
        {
            return DeductDAL.AddInfo(info);
        }
        /// <summary>
        /// 根据编号获取会员信息
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static MemberInfoModel GetMemberInfo(string number)
        {
            return DeductDAL.GetMemberInfo(number);
        }
        /// <summary>
        /// 根据期数和编号查询扣补款信息
        /// </summary>
        /// <param name="expectnum">期数</param>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static DeductModel GetDeductInfo(int expectnum, string number)
        {
            return DeductDAL.GetDeductInfo(expectnum, number);
        }
        /// <summary>
        /// 获取扣补款信息
        /// </summary>
        /// <param name="expct"></param>
        /// <param name="deduct"></param>
        /// <param name="mark"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static DataTable GetMember(int expct, int deduct, string mark, string search)
        {
            DeductDAL deductdal = new DeductDAL();
            return deductdal.GetMember(expct, deduct, mark, search);
        }
        /// <summary>
        /// 判断是否已经添加计算差异
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isExistsBonusDifference(int id)
        {
            return DeductDAL.isExistsBonusDifference(id);
        }
        /// <summary>
        /// 判断是否已经删除计算差异
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isDelBonusDifference(int id)
        {
            return DeductDAL.isDelBonusDifference(id);
        }
        /// <summary>
        /// 更新记录差异状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int upBonusDifference(int id)
        {
            return DeductDAL.upBonusDifference(id);
        }
        /// <summary>
        /// 获得记录差异信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BonusDifferenceModel GetBonusDifference(int id)
        {
            return DeductDAL.GetBonusDifference(id);
        }
        /// <summary>
        /// 删除记录差异
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelBonusDifference(int id)
        {
            return DeductDAL.DelBonusDifference(id);
        }
    }
}