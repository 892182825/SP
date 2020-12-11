using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using Model.Other;

/*
 * Author:      WangHua
 * FinishDate:  2010-01-29
 * Function:    ProviderInfo DAL
 */

namespace DAL
{
    /// <summary>
    /// ProviderInfo
    /// </summary>
    public class ProviderInfoDAL
    {
        CommonDataDAL commonDataDAL = new CommonDataDAL();
        /// <summary>
        /// 根据供应商编号查询供应商是否存在
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static bool GetProviderinfoNumber(string Number)
        {
            object obj=DBHelper.ExecuteScalar("GetProviderinfoNumber", new SqlParameter("@number", Number), CommandType.StoredProcedure);
            if (obj!=null)
            {
                return true;
            }
            return false;
        }

        #region Add Provider Information
        /// <summary>
        /// Add provider information
        /// </summary>
        /// <param name="provider">ProviderInfo object</param>
        /// <returns>Return affected rows which add provider information</returns>
        public static int AddProverderInfo(ProviderInfoModel provider)
        {
            SqlParameter[] param = new SqlParameter[] 
            { 

                new SqlParameter("@Address",provider.Address),
                new SqlParameter("@BankAddress",provider.BankAddress),
                new SqlParameter("@BankName",provider.BankName),
                new SqlParameter("@BankNumber",provider.BankNumber),

                new SqlParameter("@DutyNumber",provider.DutyNumber),
                new SqlParameter("@Fax",provider.Fax),
                new SqlParameter("@LinkMan",provider.LinkMan),
                new SqlParameter("@Mobile",provider.Mobile),
                new SqlParameter("@Name",provider.Name),
                new SqlParameter("@OperateIP",provider.OperateIP),
                new SqlParameter("@OperateNum",provider.OperateNum),
                new SqlParameter("@PermissionMan",provider.PermissionMan),
                new SqlParameter("@Remark",provider.Remark),

                new SqlParameter("@Status",provider.Status),
                new SqlParameter("@Telephone",provider.Telephone),
                new SqlParameter("@Url",provider.Url),
                new SqlParameter("@ForShort",provider.ForShort),
                new SqlParameter("@Email",provider.Email),
                new SqlParameter("@Number",provider.Number)
            };
            
            return DBHelper.ExecuteNonQuery("AddPrividerInfo", param, CommandType.StoredProcedure);

        }
        #endregion

        #region Update the provider information
        /// <summary>
        /// Update the provider information by ID  ---DS2012
        /// </summary>
        /// <param name="provider">ProviderInfo model</param>
        /// <returns>Return affected rows which update the provider information by ID</returns>
        public static int UpdateProviderInfo(ProviderInfoModel provider)
        {
            SqlParameter[] param = new SqlParameter[] 
            { 
                new SqlParameter("@ID",provider.ID),
                new SqlParameter("@Number",provider.Number),
                new SqlParameter("@Name",provider.Name),
                new SqlParameter("@ForShort",provider.ForShort),
                new SqlParameter("@LinkMan",provider.LinkMan),
                new SqlParameter("@Mobile",provider.Mobile),
                new SqlParameter("@Telephone",provider.Telephone),
                new SqlParameter("@Fax",provider.Fax),
                new SqlParameter("@Url",provider.Url),
                new SqlParameter("@Email",provider.Email),
                new SqlParameter("@Address",provider.Address),
                new SqlParameter("@BankAddress",provider.BankAddress),
                new SqlParameter("@BankName",provider.BankName),
                new SqlParameter("@BankNumber",provider.BankNumber),
                new SqlParameter("@DutyNumber",provider.DutyNumber),
                new SqlParameter("@Remark",provider.Remark),
                new SqlParameter("@Status",provider.Status),
                new SqlParameter("@PermissionMan",provider.PermissionMan),
                new SqlParameter("@OperateIP",provider.OperateIP),
                new SqlParameter("@OperateNum",provider.OperateNum)
            };
            
            return DBHelper.ExecuteNonQuery("UpdatePrividerInfo", param, CommandType.StoredProcedure);
        }
        #endregion

        #region Delete ProviderInfo by ID
        /// <summary>
        /// 删除供应商信息
        /// </summary>
        /// <param name="number">供应商编号</param>
        /// <returns>是否成功</returns>
        public static int DelProvider(string number)
        {
            SqlParameter[] param = new SqlParameter[]             
            { 
                new SqlParameter("@number",number)
            };
            
            return DBHelper.ExecuteNonQuery("DelProviderInfo", param, CommandType.StoredProcedure);
        }
        #endregion

        #region Delete provider information
        /// <summary>
        /// Delete provider information by id   ---DS2012
        /// </summary>
        /// <param name="ID">ID(Identity Column)</param>
        /// <returns>Retrun affected rows which delete provider information by id</returns>
        public static int DelProviderInfobyID(int id)
        {
            SqlParameter[] param = new SqlParameter[] 
            { 
                new SqlParameter("@id",id)
            };
            
            return DBHelper.ExecuteNonQuery("DelProviderInfobyID", param, CommandType.StoredProcedure);
        }
        #endregion

        /// <summary>
        /// 根据id查询供应商 ---DS2012
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ProviderInfoModel GetProviderInfoById(int id)
        {
            SqlDataReader row = DBHelper.ExecuteReader("GetProviderInfoByID", new SqlParameter("@id", id), CommandType.StoredProcedure);

            ProviderInfoModel pro = new ProviderInfoModel();
            while (row.Read())
            {
                pro.Address = row["Address"].ToString();
                pro.BankAddress = row["BankAddress"].ToString();
                pro.BankName = row["BankName"].ToString();
                pro.BankNumber = row["BankNumber"].ToString();
                pro.DutyNumber = row["DutyNumber"].ToString();
                pro.Email = row["Email"].ToString();
                pro.Fax = row["Fax"].ToString();
                pro.ForShort = row["ForShort"].ToString();
                pro.ID = Convert.ToInt32(row["ID"]);
                pro.LinkMan = row["LinkMan"].ToString();
                pro.Mobile = row["Mobile"].ToString();
                pro.Name = row["Name"].ToString();
                pro.Number = row["Number"].ToString();
                pro.OperateIP = row["OperateIP"].ToString();
                pro.OperateNum = row["OperateNum"].ToString();
                pro.PermissionMan = row["PermissionMan"].ToString();
                pro.Remark = row["Remark"].ToString();
                pro.Status = Convert.ToInt32(row["Status"]);
                pro.Telephone = row["Telephone"].ToString();
                pro.Url = row["Url"].ToString();
            }
            row.Close();
            return pro;
        }
        #region 查询全部的供应商信息
        /// <summary>
        /// 查询全部的供应商信息
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="RecordCount"></param>
        /// <param name="PageCount"></param>
        /// <returns></returns>
        public static IList<ProviderInfoModel> GetProviderInfoAll(int pageindex, int pagesize,out int RecordCount,out int PageCount)
        {           
            string conuls = "ID, Number, Name, ForShort, LinkMan, Mobile, Telephone, Fax, Email, Url, Address, BankName, BankAddress, BankNumber, DutyNumber, Remark, Status, PermissionMan, OperateIP, OperateNum";
            DataTable table = CommonDataDAL.GetDataTablePage_Sms(pageindex, pagesize, "ProviderInfo", conuls, "", "ID", out RecordCount, out PageCount);
            IList<ProviderInfoModel> list = new List<ProviderInfoModel>();
            foreach (DataRow row in table.Rows)
            {
                ProviderInfoModel pro = new ProviderInfoModel();
                pro.Address = row["Address"].ToString();
                pro.BankAddress = row["BankAddress"].ToString();
                pro.BankName = row["BankName"].ToString();
                pro.BankNumber = row["BankNumber"].ToString();
                pro.DutyNumber = row["DutyNumber"].ToString();
                pro.Email = row["Email"].ToString();
                pro.Fax = row["Fax"].ToString();
                pro.ForShort=row["ForShort"].ToString();
                //pro.ID = Convert.ToInt32(row["ID"]);
                pro.LinkMan = row["LinkMan"].ToString();
                pro.Mobile = row["Mobile"].ToString();
                pro.Name= row["Name"].ToString();
                pro.Number = row["Number"].ToString();
                pro.OperateIP = row["OperateIP"].ToString();
                pro.OperateNum = row["OperateNum"].ToString();
                pro.PermissionMan = row["PermissionMan"].ToString();
                pro.Remark = row["Remark"].ToString();
                pro.Status = Convert.ToInt32(row["Status"]);
                pro.Telephone = row["Telephone"].ToString();
                pro.Url = row["Url"].ToString();
                list.Add(pro);
            }
            return list;
        }
        #endregion

        #region Get the provider information
        /// <summary>
        /// Get the all provider information into DropDownList
        /// </summary>
        /// <returns>Return datatable object</returns>
        public static DataTable GetProviderInfo()
        {
            return DBHelper.ExecuteDataTable("GetProviderInfo", CommandType.StoredProcedure);
        }        
        #endregion

        /// <summary>
        /// Get the all provider information to the excel
        /// </summary>
        /// <returns>Return datatable object</returns>
        public static DataTable GetProviderInfoToExcel()
        {
            return DBHelper.ExecuteDataTable("GetProviderInfoToExcel", CommandType.StoredProcedure);
        }

        /// <summary>
        /// Judge the ProviderId whether has operation by Id before delete  DS2012
        /// 删除和修改供货商判断是否有业务
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the Provider by Id</returns>
        public static int ProviderIdWhetherHasOperation(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("ProviderIdWhetherHasOperation", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Judge the ProviderId whether exist by Id before delete or update ---DS2012
        /// 修改和删除加盟商时判断加盟商是否存在
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the Provider by Id返回加盟商数量</returns>
        public static int ProviderIdIsExist(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("ProviderIdIsExist", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Judge the ProviderName whether exist by id and providerName before update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <param name="providerName">ProviderName</param>
        /// <returns>Return the counts of the providerName by Id and providerName</returns>
        public static int ProviderNameIsExist(int Id, string providerName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
	            new SqlParameter("@Id",SqlDbType.Int),
	            new SqlParameter("@providerName",SqlDbType.NVarChar,40)
            };

            sparams[0].Value = Id;
            sparams[1].Value = providerName;

            return Convert.ToInt32(DBHelper.ExecuteScalar("ProviderNameIsExist", sparams, CommandType.StoredProcedure));
        }
        /// <summary>
        /// Judge the ProviderName whether exist by id and providerName before update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <param name="providerName">ProviderName</param>
        /// <returns>Return the counts of the providerName by Id and providerName</returns>
        public static int ProviderNameIsExist(string providerName)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
	            new SqlParameter("@providerName",SqlDbType.NVarChar,40)
            };

            sparams[0].Value = providerName;

            return Convert.ToInt32(DBHelper.ExecuteScalar("select count(1) from ProviderInfo where name=@providerName", sparams, CommandType.Text));
        }
        /// <summary>
        /// 根据银行获取国家 ---DS2012
        /// </summary>
        /// <param name="BankName"></param>
        /// <returns></returns>
        public static string GetCountryByBank(string BankName)
        {
            return DBHelper.ExecuteScalar("select b.name from memberbank a,country b where a.countrycode=b.id and a.BankCode=@name", new SqlParameter("@name", BankName), CommandType.Text).ToString();
        }
    }
}
