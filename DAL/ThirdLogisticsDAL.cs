using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using Model.Other;
using DAL.Other;

namespace DAL
{
    public class ThirdLogisticsDAL
    {
        /// <summary>
        /// 添加物流公司
        /// </summary>
        /// <param name="item"></param>
        /// 
        /// <returns></returns>   
        public int AddLogistics(LogisticsModel logisticsModel)
        {

            string sql = "insert into Logistics(Number,LogisticsCompany,Principal,Telephone1,Telephone2,Telephone3,Telephone4,cpccode,StoreAddress,PostalCode,LicenceCode,BankCode,BankCard,tax,RigisterDate,Remark,OperateIP)" +
                 " values (@number,@logisticsCompany,@principal,@telephone1,@telephone3,@telephone3,@telephone4,@cpccode,@storeAddress,@postalCode,@licenceCode,@bankCode,@bankCard,@tax,@rigisterDate,@remark,@operateIP)";

            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter ("@number",logisticsModel.Number),
                new SqlParameter("@logisticsCompany" ,logisticsModel.LogisticsCompany),
                new SqlParameter("@principal",logisticsModel.Principal),
                new SqlParameter ("@telephone1",logisticsModel.Telephone1 ),
                new SqlParameter ("@telephone2",logisticsModel.Telephone2 ),
                new SqlParameter ("@telephone3",logisticsModel.Telephone3),
                new SqlParameter ("@telephone4",logisticsModel.Telephone4),
                new SqlParameter ("@cpccode",logisticsModel.Cpccode ),
                new SqlParameter ("@storeAddress", logisticsModel.StoreAddress),
                new SqlParameter ("@postalCode", logisticsModel.PostalCode),
                new SqlParameter ("@licenceCode",logisticsModel.LicenceCode ),
                new SqlParameter ("@bankCode", logisticsModel.BankCode),
                new SqlParameter ("@bankCard",logisticsModel.BankCard ),
                new SqlParameter ("@tax", logisticsModel.Tax),
                new SqlParameter ("@rigisterDate",Convert.ToDateTime(logisticsModel.RigisterDate.ToShortDateString()+" "+DateTime.Now.ToLongTimeString()).ToUniversalTime() ),
                new SqlParameter ("@remark",logisticsModel.Remark ),
                 new SqlParameter ("@operateIP",logisticsModel.OperateIP ),
            };
            return DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);

        }

        /// <summary>
        /// 添加新的物流公司
        /// </summary>
        /// <param name="logisticsModel">物流公司对象</param>
        /// <param name="wareStr">物流仓库权限字符</param>
        /// <param name="mgrPassword">管理密码计算</param>
        /// <param name="number">操作人编号</param>
        /// <returns> 执行返回值 0 失败 1 成功</returns>
        public int AddLogistics2(LogisticsModel logisticsModel,string wareStr,string mgrPassword)
        {
            string sp = "AddLogisticsManage";
            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter ("@number",logisticsModel.Number),
                new SqlParameter("@logisticsCompany" ,logisticsModel.LogisticsCompany),
                new SqlParameter("@principal",logisticsModel.Principal),
                new SqlParameter ("@telephone1",logisticsModel.Telephone1 ),
                new SqlParameter ("@telephone2",logisticsModel.Telephone2 ),
                new SqlParameter ("@telephone3",logisticsModel.Telephone3),
                new SqlParameter ("@telephone4",logisticsModel.Telephone4),
                new SqlParameter ("@country",logisticsModel.Country ),
                new SqlParameter ("@province", logisticsModel.Province),
                new SqlParameter ("@city",logisticsModel.City ),
                new SqlParameter ("@storeAddress", logisticsModel.StoreAddress),
                new SqlParameter ("@postalCode", logisticsModel.PostalCode),
                new SqlParameter ("@licenceCode",logisticsModel.LicenceCode ),
                new SqlParameter ("@bank", logisticsModel.Bank),
                new SqlParameter ("@bankCard",logisticsModel.BankCard ),
                new SqlParameter ("@tax", logisticsModel.Tax),
                new SqlParameter ("@rigisterDate",logisticsModel.RigisterDate ),
                new SqlParameter ("@remark",logisticsModel.Remark ),
                new SqlParameter ("@operateIP",logisticsModel.OperateIP ),
                new SqlParameter ("@number",logisticsModel.OperateNum ), 
                //new SqlParameter ("@pass",logisticsModel.mgrPassword ),
                //new SqlParameter ("@WareHouse",logisticsModel.wareStr )               
            };
            return (int)DBHelper.ExecuteScalar(sp, paras, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 添加物流公司及管理员
        /// </summary>
        /// <param name="logisticsModel">物流公司实例</param>
        /// <param name="pass">物流管理的密码</param>
        /// <param name="wareHouse">物流管理的仓库权限</param>
        /// <returns></returns>
        public static int AddLogistics(LogisticsModel logisticsModel,string pass,string wareHouse)
        {

            string sp = "AddThirdLogister";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter ("@number",logisticsModel.Number),
                new SqlParameter("@logisticsCompany" ,logisticsModel.LogisticsCompany),
                new SqlParameter("@principal",logisticsModel.Principal),
                new SqlParameter ("@telephone1",logisticsModel.Telephone1 ),
                new SqlParameter ("@telephone2",logisticsModel.Telephone2 ),
                new SqlParameter ("@telephone3",logisticsModel.Telephone3),
                new SqlParameter ("@telephone4",logisticsModel.Telephone4),
                new SqlParameter ("@country",logisticsModel.Country ),
                new SqlParameter ("@province", logisticsModel.Province),
                new SqlParameter ("@city",logisticsModel.City ),
                new SqlParameter ("@storeAddress", logisticsModel.StoreAddress),
                new SqlParameter ("@postalCode", logisticsModel.PostalCode),
                new SqlParameter ("@licenceCode",logisticsModel.LicenceCode ),
                new SqlParameter ("@bank", logisticsModel.Bank),
                new SqlParameter ("@bankCard",logisticsModel.BankCard ),
                new SqlParameter ("@tax", logisticsModel.Tax),
                new SqlParameter ("@rigisterDate",logisticsModel.RigisterDate ),
                new SqlParameter ("@remark",logisticsModel.Remark ),
                 new SqlParameter ("@operateIP",logisticsModel.OperateIP ),
                 new SqlParameter ("@OperateNum",logisticsModel.OperateNum ),
                 new SqlParameter ("@pass",pass ),
                 new SqlParameter ("@WareHouse",wareHouse )
            };
            return int.Parse(DBHelper.ExecuteScalar(sp, para, CommandType.Text).ToString());
        }


        /// <summary>
        /// 修改物流公司及管理员
        /// </summary>
        /// <param name="logisticsModel">物流公司实例</param>
        /// <param name="pass">物流管理的密码</param>
        /// <param name="wareHouse">物流管理的仓库权限</param>
        /// <returns></returns>
        public static int UptLogistics(LogisticsModel logisticsModel, string pass, string wareHouse,int id)
        {

            string sp = "UptThirdLogister";
            SqlParameter[] para = new SqlParameter[]{    
                new SqlParameter ("@id",id),
                new SqlParameter ("@number",logisticsModel.Number),
                new SqlParameter("@logisticsCompany" ,logisticsModel.LogisticsCompany),
                new SqlParameter("@principal",logisticsModel.Principal),
                new SqlParameter ("@telephone1",logisticsModel.Telephone1 ),
                new SqlParameter ("@telephone2",logisticsModel.Telephone2 ),
                new SqlParameter ("@telephone3",logisticsModel.Telephone3),
                new SqlParameter ("@telephone4",logisticsModel.Telephone4),
                new SqlParameter ("@country",logisticsModel.Country ),
                new SqlParameter ("@province", logisticsModel.Province),
                new SqlParameter ("@city",logisticsModel.City ),
                new SqlParameter ("@storeAddress", logisticsModel.StoreAddress),
                new SqlParameter ("@postalCode", logisticsModel.PostalCode),
                new SqlParameter ("@licenceCode",logisticsModel.LicenceCode ),
                new SqlParameter ("@bank", logisticsModel.Bank),
                new SqlParameter ("@bankCard",logisticsModel.BankCard ),
                new SqlParameter ("@tax", logisticsModel.Tax),
                new SqlParameter ("@rigisterDate",logisticsModel.RigisterDate ),
                new SqlParameter ("@remark",logisticsModel.Remark ),
                 new SqlParameter ("@operateIP",logisticsModel.OperateIP ),
                 new SqlParameter ("@OperateNum",logisticsModel.OperateNum ),
                 new SqlParameter ("@pass",pass ),
                 new SqlParameter ("@WareHouse",wareHouse )
            };
            return (int)DBHelper.ExecuteScalar(sp, para, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 杳看第三方物流公司
        /// </summary>
        /// <param name="item"></param>
        /// 
        /// <returns></returns>   
        public DataTable GetThirdLogistics(string country)
        {
            string sql = "select ID,Number,LogisticsCompany,Principal,Telephone1,Telephone2,Telephone3,Telephone4	,Country,Province," +
  "City,StoreAddress,PostalCode,LicenceCode,Bank,BankCard,RigisterDate,Remark,Tax,Administer,LogisticsPerson," +
  "OperateIP,OperateNum from Logistics where country=@country order by RigisterDate ";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@country", country) };

            //return DBHelper.ExecuteDataTable("GetLogisticsByPage", para, CommandType.StoredProcedure);
            return DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
        }
        /// <summary>
        /// 根据ID删除物流公司
        /// </summary>
        /// <param name="item"></param>
        /// 
        /// <returns></returns>   
        public int DelThirdLogistics(int id)
        {
            string sql = "delete from Logistics where id=@id";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@id", id) };
            return DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
        }

        /// <summary>
        /// 根据ID删除指定物流公司及其附属的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelThirdLogistics_(int id)
        {
            string sp = "DelThirdLogister";
            SqlParameter param = new SqlParameter("@Tid", id);
            return (int)DBHelper.ExecuteScalar(sp, param, CommandType.StoredProcedure);            
        }
        /// <summary>
        /// 根据编号查询物流公司备注
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public  string GetRemarkById(string id)
        {
            string sql = "select remark from Logistics where id = @id";
            SqlParameter[] para = { new SqlParameter("@id", SqlDbType.VarChar, 20) };
            para[0].Value = id;
            return DBHelper.ExecuteScalar(sql, para, CommandType.Text).ToString();
        }
        /// <summary>
        /// 根据ID删除物流公司
        /// </summary>
        /// <param name="id"></param>
        /// 
        /// <returns></returns>   
        public static int DelThirdLogistics_sp(int id)
        {
            string sp = "DelThirdLogister";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@id", id) };
            return DBHelper.ExecuteNonQuery(sp, para, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 修改物流公司
        /// </summary>
        /// <param name="item"></param>
        /// 
        /// <returns></returns>   
        public int UpdateThirdLogistics(LogisticsModel logisticsModel, int id)
        {
            string sql = "update Logistics set LogisticsCompany=@logisticsCompany,Principal=@principal,Telephone1=@telephone1,Telephone2=@telephone2,Telephone3=@telephone3,Telephone4=@telephone4," +
                "Cpccode=@cpccode,StoreAddress=@storeAddress,PostalCode=@postalCode,LicenceCode=@licenceCode,BankCode=@bankCode,BankCard=@bankCard,tax=@tax," +
                "Remark=@remark where id=@id";
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@logisticsCompany" ,logisticsModel.LogisticsCompany),
                new SqlParameter("@principal",logisticsModel.Principal),
                new SqlParameter ("@telephone1",logisticsModel.Telephone1 ),
                new SqlParameter ("@telephone2",logisticsModel.Telephone2 ),
                new SqlParameter ("@telephone3",logisticsModel.Telephone3),
                new SqlParameter ("@telephone4",logisticsModel.Telephone4),
                new SqlParameter ("@cpccode",logisticsModel.Cpccode ),
                new SqlParameter ("@storeAddress", logisticsModel.StoreAddress),
                new SqlParameter ("@postalCode", logisticsModel.PostalCode),
                new SqlParameter ("@licenceCode",logisticsModel.LicenceCode ),
                new SqlParameter ("@bankCode", logisticsModel.BankCode),
                new SqlParameter ("@bankCard",logisticsModel.BankCard ),
                new SqlParameter ("@tax", logisticsModel.Tax),
                new SqlParameter ("@remark",logisticsModel.Remark ),
                  new SqlParameter ("@id",id)
                 
            };
            return DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
        }

        /// <summary>
        /// 得到物流公司信息用于初始化显示
        /// </summary>
        /// <param name="item"></param>
        /// 
        /// <returns></returns>
        public LogisticsModel GetThirdLogisticsInit(int id)
        {
            string sql = "select a.ID,a.Number,a.LogisticsCompany,a.Principal,a.Telephone1,a.Telephone2,a.Telephone3,a.Telephone4," +
                "a.cpccode,b.Country,b.Province,b.City,b.xian,a.StoreAddress,a.PostalCode,a.LicenceCode,a.bankcode,isnull((select bankname from memberbank where bankcode = a.bankcode),'') as bank,a.BankCard,a.RigisterDate,a.Remark,a.Tax,a.Administer,a.LogisticsPerson,a.OperateIP,a.OperateNum from Logistics a,city b where a.id=@id and a.cpccode=b.cpccode";
            
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@id", id) };
            SqlDataReader dr = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            LogisticsModel logisticsModel = null;
            while (dr.Read())
            {
                logisticsModel = new LogisticsModel();
                logisticsModel.ID = id;
                logisticsModel.Number = dr.GetString(dr.GetOrdinal("Number"));
                logisticsModel.LogisticsCompany = dr.GetString(dr.GetOrdinal("LogisticsCompany"));
                logisticsModel.Principal = dr.GetString(dr.GetOrdinal("Principal"));
                logisticsModel.Telephone1 = dr.GetString(dr.GetOrdinal("Telephone1"));
                logisticsModel.Telephone2 = dr.GetString(dr.GetOrdinal("Telephone2"));
                logisticsModel.Telephone3 = dr.GetString(dr.GetOrdinal("Telephone3"));
                logisticsModel.Telephone4 = dr.GetString(dr.GetOrdinal("Telephone4"));
                logisticsModel.StoreAddress = dr.GetString(dr.GetOrdinal("StoreAddress"));
                logisticsModel.PostalCode = dr.GetString(dr.GetOrdinal("PostalCode"));
                logisticsModel.LicenceCode = dr.GetString(dr.GetOrdinal("LicenceCode"));
                logisticsModel.Bank = dr.GetString(dr.GetOrdinal("Bank"));
                logisticsModel.BankCard = dr.GetString(dr.GetOrdinal("BankCard"));
                logisticsModel.Tax = dr.GetString(dr.GetOrdinal("Tax"));
                logisticsModel.Remark = dr.GetString(dr.GetOrdinal("Remark"));
                logisticsModel.RigisterDate = Convert.ToDateTime(dr["RigisterDate"]);
                logisticsModel.Country = dr["Country"].ToString();
                logisticsModel.City = dr["City"].ToString();
                logisticsModel.Xian = dr["xian"].ToString();
                logisticsModel.Province = dr["Province"].ToString();
            }
            dr.Close();
            return logisticsModel;
        }
        // <summary>
        /// 得到店铺库存情况
        /// </summary>
        /// <param name="item"></param>
        /// 
        /// <returns></returns>
        public DataTable GetShowStorage(string storeID)
        {
//            string sql = "select P.ProductName,p.productTypename,P.PreferentialPrice,P.ProductspecID,S.ID,S.StoreID,S.ProductID,S.TotalIn,S.TotalOut,S.turnstorage,case  when  S.ActualStorage>=0  then  cast(ActualStorage as  varchar)  when  S.ActualStorage<0  then  '0'  end   as  ActualStorage ,case  when  S.ActualStorage<0  then  cast(-S.ActualStorage as  varchar)  when  S.ActualStorage>=0  then  '0'  end   as  ActualStorage1 ,S.HasOrderCount,S.inwaycount,U.ProductspecName,T.ProductUnitName as UnitName" +
//"from Stock as S,product as P LEFT OUTER JOIN ProductSpec U on P.ProductSpecID = U.ProductSpecID  left outer join ProductUnit T on P.smallproductunitid = T.ProductUnitID" +
//"where P.ProductID = S.ProductID and S.StoreID=@storeID";
            //string sql = "select * from Stock where storeID=@storeID";
            string sql = " select P.ProductName,p.productTypename,P.PreferentialPrice,P.ProductspecID,S.ID,S.StoreID,S.ProductID,S.TotalIn,S.TotalOut,S.turnstorage,case  when  S.ActualStorage>=0  then  cast(ActualStorage as  varchar)  when  S.ActualStorage<0  then  '0'  end   as  ActualStorage ,case  when  S.ActualStorage<0  then  cast(-S.ActualStorage as  varchar)  when  S.ActualStorage>=0  then  '0'  end   as  ActualStorage1 ,S.HasOrderCount,S.inwaycount,U.ProductspecName,T.ProductUnitName as UnitName  from Stock as S,product as P LEFT OUTER JOIN ProductSpec U on P.ProductSpecID = U.ProductSpecID  left outer join ProductUnit T on P.smallproductunitid = T.ProductUnitID where P.ProductID = S.ProductID and S.StoreID=@storeID";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@storeID", storeID) };        
            return DBHelper.ExecuteDataTable(sql,para,CommandType.Text);
        }

        //查询是否编号已存在
        public int CheckLogisticsNumIsUse(string number)
        {
            string sql = "SELECT COUNT(*) FROM Logistics WHERE number=@number";
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@number",number)
            };
            return Convert.ToInt32(DBHelper.ExecuteScalar(sql,param,CommandType.Text));
        }

        //绑定银行
        public  IList<MemberBankModel> BindBank_List()
        {
            IList<MemberBankModel> list = new List<MemberBankModel>();
            string sql = "SELECT BankID,BankName  FROM MemberBank  ";
            SqlDataReader reader = DBHelper.ExecuteReader(sql);
            while (reader.Read())
            {
                MemberBankModel info = new MemberBankModel(int.Parse(reader["BankID"].ToString()));
                info.BankName = reader["BankName"].ToString();
                list.Add(info);
            }
            reader.Close();
            return list;
        }
    }
}
