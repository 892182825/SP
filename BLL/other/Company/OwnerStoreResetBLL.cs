using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;

/*
 * 创建者：汪华
 * 创建时间：2009-09-07
 */

namespace BLL.other.Company
{
    /// <summary>
    /// 所属店铺调整
    /// </summary>
    public class OwnerStoreResetBLL
    {
        //MemberInfoDAL memberInfoDAL = new MemberInfoDAL();
        /// <summary>
        /// 会员所属店铺调整
        /// </summary>
        /// <param name="memberstoreid">会员编号</param>
        /// <param name="NewStoreid">新店铺号</param>
        /// <param name="NewStoreid">老店铺号</param>
        /// <param name="type">调整类型</param>
        /// <returns></returns>
        public static bool MemberStoreReset(string memberstoreid, string NewStoreid, string Oldstoreid, int type, int ExpectNum)
        {
           return StoreInfoDAL.MemberStoreReset(memberstoreid, NewStoreid, Oldstoreid, type, ExpectNum);
        }
        public static bool MemberStoreReset2(string memberstoreid, string NewStoreid, string Oldstoreid, int type, int ExpectNum)
       {
           return StoreInfoDAL.MemberStoreReset2(memberstoreid, NewStoreid, Oldstoreid, type, ExpectNum);
       }
        /// <summary>
        /// 根据会员编号获取会员信息
        /// sj
        /// 
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public static MemberInfoModel GetMemberInfoById(string memberid)
        {
            return MemberInfoDAL.getMemberInfo(memberid);
        }
        /// <summary>
        /// 检查会员编号是否存在
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static bool CheckStoreInfoById(string storeid)
        {
           // StoreInfoDAL dao = new StoreInfoDAL();
            return StoreInfoDAL.CheckStoreId(storeid);
        }
    }
}
