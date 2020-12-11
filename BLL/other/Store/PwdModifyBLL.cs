using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data;
using System.Data.SqlClient;

namespace BLL.other.Store
{
    /// <summary>
    /// 店铺密码修改
    /// </summary>
    public class PwdModifyBLL
    {
       // StoreInfoDAL sd = new StoreInfoDAL();
        /// <summary>
        /// 修改店铺密码
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int updStorePass(string StoreID, string NewPass)
        {
            return StoreInfoDAL.updStorePass(StoreID, NewPass);
        }

        /// <summary>
        /// 修改密码（店铺）--带事务处理
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int updStoreLoginPassT(SqlTransaction tran, string StoreID, string NewPass)
        {
            return StoreInfoDAL.updStoreLoginPassT(tran, StoreID, NewPass);
        }

        /// <summary>
        /// 修改二级密码（店铺）--带事务处理
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int updStoreAdvPassT(SqlTransaction tran, string StoreID, string NewPass)
        {
            return StoreInfoDAL.updStoreAdvPassT(tran, StoreID, NewPass);
        }

        /// <summary>
        /// 修改店铺二级密码密码
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int updStoreadvPass(string StoreID, string NewPass)
        {
            return StoreInfoDAL.updStoreadvPass(StoreID, NewPass);
        }

        /// <summary>
        /// 修改密码（会员）--带事务处理
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int updMemberLoginPassT(SqlTransaction tran, string number, string NewPass)
        {
            return StoreInfoDAL.updMemberLoginPassT(tran, number, NewPass);
        }

        /// <summary>
        /// 修改二级密码（会员）--带事务处理
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int updMemberAdvPassT(SqlTransaction tran, string number, string NewPass)
        {
            return StoreInfoDAL.updMemberAdvPassT(tran, number, NewPass);
        }

        public static int updateMemberPass(string number, string NewPass, int passtype)
        {
            return StoreInfoDAL.updMemberPass(number, NewPass, passtype);
        }

        /// <summary>
        /// 分公司密码修改
        /// </summary>
        /// <param name="Branch"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int updBranchPass(string Branch, string NewPass)
        {
            return StoreInfoDAL.updBranchPass(Branch, NewPass);
        }
        /// <summary>
        /// 分公司二级密码
        /// </summary>
        /// <param name="Branch"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int updBranchadvPass(string Branch, string NewPass)
        {
            return StoreInfoDAL.updBranchadvPass(Branch, NewPass);
        }

        public static int check(string Member, string oldPass,int passtype)
        {
            return StoreInfoDAL.check(Member, oldPass,passtype);
        }

        public static int checkBranch(string Branch, string oldPass, int passtype)
        {
            return StoreInfoDAL.checkBranch(Branch, oldPass, passtype);
        }
        public static int checkstore(string storeid, string oldPass ,int ptype)
        {
            return StoreInfoDAL.checkstore(storeid, oldPass, ptype);
        }
        /// <summary>
        /// 验证服务机构二级密码——ds2012——tianfeng
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="advpass"></param>
        /// <returns></returns>
        public static int checkstoreadvpass(string storeid, string advpass)
        {
            return StoreInfoDAL.checkstoreadvpass(storeid, advpass);
        }
        public static int checkPwdQuestion(string Number)
        {
            return StoreInfoDAL.checkPwdQuestion(Number);
        }
        public static string getQuestion(string Number)
        {
            return StoreInfoDAL.getQuestion(Number);
        }
        public static string getAnswer(string Number)
        {
            return StoreInfoDAL.getAnswer(Number);
        }
        public static int updPwdQuestion(string Number, string question, string answer)
        {
            return StoreInfoDAL.updPwdQuestion(Number, question, answer);
        }

        //public int UpdPwdQuestion(string p, string p_2, string p_3)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
