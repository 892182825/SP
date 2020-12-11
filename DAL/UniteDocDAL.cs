using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model.Other;
using Model;

namespace DAL
{
    /// <summary>
    /// 合单表数据层
    /// </summary>
    public class UniteDocDAL
    {
        /// <summary>
        /// 生成合单号
        /// </summary>
        /// <returns></returns>
        public string GetUniteId()
        {
            string prefix = "UD";
            string timeString = MYDateTime.ToYYMMDDHHmmssString();
            string dataId = DBHelper.ExecuteScalar("select top 1 ID from UniteDoc order by ID desc").ToString();
            dataId = dataId.Length == 0 ? "1" : dataId;
            return prefix + timeString + dataId; 
        }

        /// <summary>
        /// 添加合单信息
        /// </summary>
        /// <param name="uniteItem"></param>
        /// <returns></returns>
        public Boolean AddUniteDoc(UniteDocModel uniteItem)
        {
            Boolean temp = false;
            //string sql = "insert into UniteDoc (UniteDocID,Remark,DocID,StoreID,ExpectNum,TotalPV,TotalMoney,IsCheckOut,InceptPerson,InceptAddress,Telephone,Weight,Carriage) "
            //    + "values(@UniteDocID,@Remark,@DocID,@StoreID,@ExpectNum,@TotalPV,@TotalMoney,@IsCheckOut,@InceptPerson,@InceptAddress,@Telephone,@Weight,@Carriage)";
            //SqlParameter[] ps = new SqlParameter[] { 
            //    new SqlParameter("@UniteDocID",uniteItem.UniteDocID),
            //    new SqlParameter("@Remark",uniteItem.Remark),
            //    new SqlParameter("@DocID",uniteItem.DocID),
            //    new SqlParameter("@StoreID",uniteItem.StoreID),
            //    new SqlParameter("@ExpectNum",uniteItem.ExpectNum),
            //    new SqlParameter("@TotalPV",uniteItem.TotalPV),
            //    new SqlParameter("@TotalMoney",uniteItem.TotalMoney),
            //    new SqlParameter("@IsCheckOut",uniteItem.IsCheckOut),
            //    new SqlParameter("@InceptPerson",uniteItem.InceptPerson)
            //};
            return temp;
        }

        /// <summary>
        ///  添加合单信息(调用存储过程)
        /// </summary>
        /// <param name="uniteItem"></param>
        /// <returns></returns>
        public static Boolean AddUniteDoc_I(UniteDocModel uniteItem)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@DocIDs",uniteItem.DocID),
                new SqlParameter("@Weight",uniteItem.Weight),
                new SqlParameter("@Carriage",uniteItem.Carriage),
                new SqlParameter("@Remark",uniteItem.Remark)
            };

            int hs = DBHelper.ExecuteNonQuery("procInsertUniteDoc", param, CommandType.StoredProcedure);
            if (hs == 2)
                return true;
            return false;
        }

        /// <summary>
        /// 返回合单表DataTable
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUniteDoc(string condition)
        {
//            string cmd = @"select so.StoreID,ud.UniteDocID,ud.DocIDs,ud.UniteDocTime,ud.Weight,ud.Carriage,ud.Remark,
//                            so.InceptPerson,so.InceptAddress,so.PostalCode,so.Telephone,so.ConveyanceMode,so.ConveyanceCompany,
//                            wh.WareHouseName,case so.OrderType when 1 then '周转货' when 0 then '正常订货' end as OrderType,
//                            so.ForeCastArriveDateTime from dbo.UniteDoc ud
//                            left outer join StoreOrder so on substring(ud.DocIDs,1,charindex(',',ud.DocIDs)-1)=so.OutStorageOrderID 
//                            left outer join InventoryDoc ity on so.OutStorageOrderID=ity.DocID
//                            left outer join WareHouse wh on wh.WareHouseID=ity.WareHouseID
//                            where " + condition;
            string cmd = @"select so.StoreID,ud.UniteDocID,ud.DocIDs,ud.UniteDocTime,ud.Weight,ud.Carriage,ud.Remark,
                            so.InceptPerson,so.InceptAddress,so.PostalCode,so.Telephone,so.ConveyanceMode,so.ConveyanceCompany,
                            case so.OrderType when 1 then '周转货' when 0 then '正常订货' end as OrderType,
                            so.ForeCastArriveDateTime from dbo.UniteDoc ud
                            left outer join StoreOrder so on substring(ud.DocIDs,1,charindex(',',ud.DocIDs)-1)=so.OutStorageOrderID where " + condition + " order by ud.UniteDocTime desc";

            return DBHelper.ExecuteDataTable(cmd);
        }

        /// <summary>
        /// 删除一条合单信息
        /// </summary>
        /// <param name="UniteDocID"></param>
        /// <returns></returns>
        public static bool DelUniteDoc(string UniteDocID)
        {
            string cmd = "delete from UniteDoc where UniteDocID=@num";
            SqlParameter spa = new SqlParameter("@num",SqlDbType.VarChar,150);
            spa.Value = UniteDocID;
            if (DBHelper.ExecuteNonQuery(cmd,spa,CommandType.Text) == 1)
                return true;
            return false;
        }

    }
}
