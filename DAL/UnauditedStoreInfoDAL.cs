using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using Model.Other;
/***************************************************************************
 * 功能：未审核的店铺
 * 修改人:汪华
 * 修改时间：2009-09-02
 * 
 *************************************************************************/
namespace DAL
{
    public class UnauditedStoreInfoDAL
    {

        CommonDataDAL commonDataDAL = new CommonDataDAL();
        #region 添加未注册的店铺
        /// <summary>
        /// 添加未注册的店铺
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public bool AddUnauditedStoreInfo(UnauditedStoreInfoModel store)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@AccreditExpectNum",store.AccreditExpectNum),
                new SqlParameter("@AdvPass",store.AdvPass),
                new SqlParameter("@Answer",store.Answer),
                new SqlParameter("@Bank",store.Bank),
                new SqlParameter("@BankCard",store.BankCard),
                new SqlParameter("@City",store.City),
                new SqlParameter("@Country",store.Country),
                new SqlParameter("@Currency",store.Currency),
                new SqlParameter("@DCountry",store.DCountry),
                new SqlParameter("@DianCity",store.DianCity),
                new SqlParameter("@Email",store.Email),
                new SqlParameter("@ExpectNum",store.ExpectNum),
                new SqlParameter("@FareArea",store.FareArea),
                new SqlParameter("@FareBreed",store.FareBreed),
                new SqlParameter("@FaxTele",store.FaxTele),
                new SqlParameter("@HomeTele",store.HomeTele),
                //new SqlParameter("@Level",store.Level),
                //new SqlParameter("@Level1",store.Level1),
                new SqlParameter("@Language",store.Language),
                new SqlParameter("@LoginPass",store.LoginPass),
                new SqlParameter("@Name",store.Name),
                new SqlParameter("@NetAddress",store.NetAddress),
                new SqlParameter("@Number",store.Number),
                //new SqlParameter("@OffendTimes",store.OffendTimes),
                new SqlParameter("@OfficeTele",store.OfficeTele),
                new SqlParameter("@OperateIp",store.OperateIp),
                new SqlParameter("@OperateNum",store.OperateNum),
               // new SqlParameter("@PermissionMan",store.PermissionMan),
                new SqlParameter("@PhotoH",store.PhotoH),
                new SqlParameter("@PhotoPath",store.PhotoPath),
                new SqlParameter("@PhotoW",store.PhotoW),
                new SqlParameter("@PostalCode",store.PostalCode),
                new SqlParameter("@Province",store.Province),
                new SqlParameter("@Question",store.Question),
                new SqlParameter("@Recommended",store.Direct),
                new SqlParameter("@RegisterDate",store.RegisterDate),
                //new SqlParameter("@Remark",store.Remark),
                //new SqlParameter("@StorageScalar",store.StorageScalar),
                new SqlParameter("@StoreAddress",store.StoreAddress),
                new SqlParameter("@StoreId",store.StoreId),
                new SqlParameter("@StoreLevelInt",store.StoreLevelInt),
                new SqlParameter("@StoreLevelStr",store.StoreLevelStr),
                new SqlParameter("@StoreName",store.StoreName),
                new SqlParameter("@TotalaccountMoney",store.TotalaccountMoney),
               // new SqlParameter("@TotalchangeMoney",store.TotalchangeMoney),
              //  new SqlParameter("@Totalchangepv",store.Totalchangepv),
               // new SqlParameter("@TotalcomityMoney",store.TotalcomityMoney),
               // new SqlParameter("@TotalcomityPv",store.TotalcomityPv),
                //new SqlParameter("@TotalindentMoney",store.TotalindentMoney),
               // new SqlParameter("@TotalindentPv",store.TotalindentPv),
               // new SqlParameter("@TotalinvestMoney",store.TotalinvestMoney),
               // new SqlParameter("@TotalmaxMoney",store.TotalmaxMoney),
                new SqlParameter("@TotalmemberorderMoney",store.TotalmemberorderMoney),
                new SqlParameter("@TotalordergoodsMoney",store.TotalordergoodsMoney)
            };
            int flag = DBHelper.ExecuteNonQuery("AddUnauditedStoreInfo", param, CommandType.StoredProcedure);
            if (flag > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 查询全部未审核的店铺
        /// <summary>
        /// 查询全部未审核的店铺
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="storename"></param>
        /// <param name="ExpectNum"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="RecordCount"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        public static DataTable GetAll(string storeid, string storename, int ExpectNum,int pageindex, int pagesize, out int RecordCount, out int pagecount)
        {
            string columns="";
            StringBuilder sb = new StringBuilder();
            sb.Append(" 1=1 ");
            if (storeid.Length>0)
            {
                sb.Append("and StoreId='"+storeid+"'");
            }
            if (storename.Length>0)
            {
                sb.Append(" and StoreName='"+storename+"'");
            }
            if (ExpectNum>0)
            {
                sb.Append(" and ExpectNum="+ExpectNum);
            }
            string wheres = sb.ToString();
            DataTable table = CommonDataDAL.GetDataTablePage_Sms(pageindex, pagesize, "UnauditedStoreInfo", columns, wheres, "ID", out RecordCount, out pagecount);
           return table;
        }
        #endregion
    }
}
