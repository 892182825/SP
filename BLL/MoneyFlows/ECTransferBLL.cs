using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
namespace BLL.MoneyFlows
{
    /*
     * 电子转账
     * **/
    public class ECTransferBLL
    {
        /// <summary>
        /// 无条件查询
        /// 返回值： Number,Name,Bank,BankCard,Jackpot-ECTPay-Membership-Out-ReleaseMoney as jine,ReleaseMoney
        /// </summary>
        /// <returns></returns>
        public List<MemberInfoModel> GetAllMemberInfo()
        {

            return null;
        }

        /// <summary>
        /// 按条件查询
        /// 条件1：MemberNumber
        /// 条件2：MemberName
        /// 条件3：Jackpot-ECTPay-ReleaseMoney-Membership-Out>0
        /// 返回值： Number,Name,Bank,BankCard,Jackpot-ECTPay-Membership-Out-ReleaseMoney as jine,ReleaseMoney
        /// </summary>
        /// <param name="MemberNumber"></param>
        /// <param name="MemberName"></param>
        /// <returns></returns>
        public List<MemberInfoModel> GetMemberInfo(string MemberNumber,string MemberName)
        {
            return null;
        }

        /// <summary>
        /// 验证电子账户密码
        /// 条件1：Number
        /// 条件2：AdvPass
        /// 返回值：Boolean
        /// select count(*) from MemberInfo where bianhao = ' ' and advpass = ' '
        /// </summary>
        /// <returns></returns>
        public Boolean ValidatePwd(string MemberNumber,string MemberPvPwd)
        {
            return true;
        }

        /// <summary>
        /// 验证收款人是否存在
        /// 条件1：Number
        /// 返回值：Boolean
        /// select count(bianhao) from MemberInfo where bianhao =  ' '
        /// </summary>
        /// <returns></returns>
        public Boolean ValidateMember(string MemberNumber)
        {
            return true;
        }
        /// <summary>
        /// 电子转账功能
        /// insert into ECTHTransferDetail(MemberNumber,Money,ExpectNum,ReceivablesNumber,OperateIP,OperateNumber,Remark) values()
        /// update MemberInfo set Out = Out +'  'where bianhao = '转账人编号'
        /// update MemberInfo set ECTzongji = ECTJackpot+' ' where bianhao = '收款人编号'
        /// </summary>
        /// <returns></returns>
        public Boolean TransferMoney(string MemberNumber, double Money, int ExpectNum, string ReceivablesNumber, string OperateIP, string OperateNumber, string Remark)
        {
            return true;
        }

    }
}
