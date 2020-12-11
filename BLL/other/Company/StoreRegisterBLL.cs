using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;

namespace BLL.other.Company
{
    public class StoreRegisterBLL
    {
        /// <summary>
        /// 根据会员编号获取会员名称
        /// </summary>
        /// <param name="id"></param>   
        /// <returns></returns>
        public static string GetMemberName(string id)
        {
            // StoreInfoDAL dao=new StoreInfoDAL ();
            return StoreInfoDAL.GetMemberName(id);
        }

                /// <summary>
        /// 根据会员编号获取会员昵称
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetMemberPetName(string number)
        {
            return StoreInfoDAL.GetMemberPetName(number);
        }
        public static string getMemberMTele(string member)
        {
            return StoreInfoDAL.getMemberMTele(member);
        }
        public static string getMemberFTele(string member)
        {
            return StoreInfoDAL.getMemberFTele(member);
        }
        public static string getMemberHTele(string member)
        {
            return StoreInfoDAL.getMemberHTele(member);
        }
        public static string getMemberOTele(string member)
        {
            return StoreInfoDAL.getMemberOTele(member);
        }
        /// <summary>
        /// 店铺注册
        /// </summary>
        /// <param name="store"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool RegisterStoreInfo(StoreInfoModel store, string type)
        {
            //  StoreInfoDAL dao=new StoreInfoDAL ();
            return StoreInfoDAL.RegisterStoreInfo(store, type);
        }
        public static bool CheckMemberInfoByNumber(string number)
        {
            // MemberInfoDAL dao = new MemberInfoDAL();
            MemberInfoModel member = MemberInfoDAL.getMemberInfo(number);
            if (member != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 添加自由注册的店铺
        /// </summary>
        /// <param name="store">店铺信息</param>
        /// <returns></returns>
        public static bool AllerRegisterStoreInfo(UnauditedStoreInfoModel ustore)
        {
            return StoreInfoDAL.AllerRegisterStoreInfo(ustore);
        }
        /// <summary>
        /// 会员编号是否存在
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static int IsMemberNum(string number)
        {
            return StoreInfoDAL.IsMemberNum(number);
        }
        public static string GetPassword(string Number, string storeid)
        {
            return StoreInfoDAL.GetPassword(Number, storeid);
        }

        /// <summary>
        /// 检测会员是否已经申请或注册为服务机构
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <returns></returns>
        public static int CheckStoreNumber(string number)
        {
            return StoreInfoDAL.CheckStoreNumber(number);
        }
    }
}
