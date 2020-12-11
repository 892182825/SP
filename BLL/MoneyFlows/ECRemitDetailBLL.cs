using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using System.Data;
using DAL;
using System.Data.SqlClient;
using System.Collections;


/*
 * 转账管理
 ***/
namespace BLL.MoneyFlows
{
    public class ECRemitDetailBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsRemittances"></param>
        /// <returns></returns>
        public  List<ECTransferDetailModel> GetECRemitDetail(int IsRemittances)
        {
            ECRemitDetailDAL detail = new ECRemitDetailDAL();
            return detail.GetECRemitDetail(IsRemittances);
        }
        /// <summary>
        /// 获取会员信息
        /// </summary>
        public static DataTable GetMember(string number, string name)
        {
            return ECRemitDetailDAL.GetMember(number, name);
        }
      
        /// <summary>
        /// 收款确认
        /// </summary>
        /// <returns></returns>
        public static Boolean UptECTransferDetail(string RemitID, int CollectionExpectNum, string GatheringID, int ID, double RemitMoney)
        {
            return ECRemitDetailDAL.UptECTransferDetail(RemitID, CollectionExpectNum, GatheringID, ID, RemitMoney);
        }

        /// <summary>
        /// 验证密码——ds2012——tianfeng
        /// </summary>
        /// <param name="number"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static int ValidatePwd(string number, string pwd)
        {
            return ECRemitDetailDAL.ValidatePwd(number, pwd);
        }

        /// <summary>
        /// 验证会员是否存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static Boolean validateNumber(string number)
        {
            return ECRemitDetailDAL.validateNumber(number);
        }
        /// <summary>
        /// 汇款申报
        /// </summary>
        /// <returns></returns>
        public static Boolean AddECRemitDetail(ECRemitDetailModel remitdetail)
        {
            return ECRemitDetailDAL.AddECRemitDetail(remitdetail);
        }
        /// <summary>
        /// 电子转账——ds2012——tianfeng
        /// </summary>
        /// <param name="detailmodel"></param>
        /// <returns></returns>
        public static int AddMoneyManage(ECTransferDetailModel detailmodel,int type)
        {
            return ECRemitDetailDAL.AddMoneyManage(detailmodel,type);
        }

        /// <summary>
        /// 电子转账(含多种类型转入)Chengkai(12-05-31)--不带事务处理
        /// </summary>
        /// <param name="detailmodel"></param>
        /// <param name="Intype">转入类型--2.为会员现金账户转入 1.为会员消费账户转入 0.为店铺订货款转入</param>
        /// <param name="Outtype">转出类型 -- 1.为会员现金转出 0.为会员消费账户转出</param>
        /// <returns></returns>
        public static int AddMoneyManage(ECTransferDetailModel detailmodel, int Outtype, int Intype)
        {
            return ECRemitDetailDAL.AddMoneyManage(detailmodel, Outtype, Intype);
        }

        /// <summary>
        /// 电子转账(含多种类型转入)Chengkai(12-05-31)--带事务处理
        /// </summary>
        /// <param name="detailmodel"></param>
        /// <param name="Intype">转入类型--2.为会员现金账户转入 1.为会员消费账户转入 0.为店铺订货款转入</param>
        /// <param name="Outtype">转出类型 -- 1.为会员现金转出 0.为会员消费账户转出</param>
        /// <returns></returns>
        public static int AddMoneyManageTran(ECTransferDetailModel detailmodel, int Outtype, int Intype,SqlTransaction tran)
        {
            return ECRemitDetailDAL.AddMoneyManageTran(detailmodel, Outtype, Intype, tran);
        }

        /// <summary>
        /// 查看会员电子转账(汇款)明细
        /// </summary>
        /// <returns></returns>
        public static List<ECRemitDetailModel> GetECRemitDetail(ECRemitDetailModel detail, DateTime EndTime)
        {
            return ECRemitDetailDAL.GetECRemitDetail(detail, EndTime);
        }

          /// <summary>
        /// 绑定币种
        /// </summary>
        /// <returns></returns>
        public static List<CurrencyModel> GetCurrency()
        {
            return ECRemitDetailDAL.GetCurrency();
        }
        /// <summary>
        /// 查看备注
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetNote(int id)
        {
            return ECRemitDetailDAL.GetNote(id);
        }
        /// <summary>
        /// 查看备注
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetEFTNote(int id)
        {
            return ECRemitDetailDAL.GetEFTNote(id);
        }
        /// <summary>
        /// 转账确认
        /// </summary>
        /// <param name="id">转账ID</param>
        /// <param name="iszhuanchu">是否是转出</param>
        public static void TransferConfirm(int id,int iszhuanchu)
        {
            ECRemitDetailDAL.TransferConfirm(id,iszhuanchu);
        }

        public static bool TransferConfirm(int id, int iszhuanchu, SqlTransaction tran)
        {
            return ECRemitDetailDAL.TransferConfirm(id, iszhuanchu,tran);
        }

        /// <summary>
        /// 判断转账是否已经确认
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isQuren(int id,int iszhuanchu)
        {
            return ECRemitDetailDAL.isQuren(id, iszhuanchu);
        }

        /// <summary>
        /// 根据编号获取转账信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MoneyTransferModel GetMoneyTransfer(int id)
        {
            return ECRemitDetailDAL.GetMoneyTransfer(id);
        }

        public static MoneyTransferModel GetMoneyTransfer(SqlTransaction tran, int id)
        {
            return ECRemitDetailDAL.GetMoneyTransfer(id,tran);
        }

        /// <summary>
        /// 判断转账记录是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isExistsTransfer(int id)
        {
            return ECRemitDetailDAL.isExistsTransfer(id);
        }
        /// <summary>
        /// 删除转账信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelTransfer(int id,MoneyTransferModel info)
        {
            return ECRemitDetailDAL.DelTransfer(id,info);
        }
        /// <summary>
        /// 查看会员的现金账户和报单账户——ds2012——tianfeng
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="Cash">现金账户</param>
        /// <param name="Declarations">报单账户</param>
        public static void GetCashDeclarations(string number,out double Cash, out double Declarations)
        {
            ECRemitDetailDAL.DelTransfer(number,out Cash,out Declarations);
        }
    }
}
