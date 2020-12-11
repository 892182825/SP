using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL.MoneyFlows
{
    public class VIPCardManageBll
    {
        /// <summary>
        /// 获得最大值
        /// </summary>
        /// <returns></returns>
        //public int MaxCard()
        //{
        //    VIPCardManageDAL card = new VIPCardManageDAL();
        //    return card.MaxCard();
        //}
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="mark"></param>
        /// <returns></returns>
        public System.Data.DataTable GetCardRange(string condition, string mark)
        {
            VIPCardManageDAL card = new VIPCardManageDAL();
            return card.GetCardRange(condition, mark);
        }

        /// <summary>
        /// 验证店编号是否存在
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public static int valiStore(string storeID)
        {
            return StoreInfoDAL.valiStore(storeID);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static int Addvipcard(Model.VIPCardRangeModel mode)
        {
             return VIPCardManageDAL.Addvipcard(mode);
        }
        /// <summary>
        /// 开始/结束 编号
        /// </summary>
        /// <param name="begincard"></param>
        /// <param name="endcard"></param>
        /// <returns></returns>
        public static int BetweenCard(int begincard, int endcard)
        {
            return VIPCardManageDAL.BetweenCard(begincard, endcard);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RangeID"></param>
        public static void Uptvipcard(int RangeID)
        {
            VIPCardManageDAL.Uptvipcard(RangeID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reangeID"></param>
        /// <param name="storeID"></param>
        public static void Uptvipcardstore(int reangeID, string storeID)
        {
             VIPCardManageDAL.Uptvipcardstore(reangeID, storeID);
        }

    }
}
