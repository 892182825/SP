using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using DAL;
using Model;
using System.Data;

/*
 * Author:      WangHua
 * FinishDate:  2010-01-29
 * Function:    Provider Operation
 */

namespace BLL.other.Company
{
    /// <summary>
    /// 供应商管理
    /// </summary>
    public class ProviderManageBLL
    {
        /// <summary>
        /// 根据供应商编号查询供应商是否存在 ---DS2012
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static bool GetProviderinfoNumber(string Number)
        {
             return ProviderInfoDAL.GetProviderinfoNumber(Number);
        }

        /// <summary>
        /// Add provider information  ---DS2012
        /// 添加供应商
        /// </summary>
        /// <param name="provider">ProviderInfo object</param>
        /// <returns>Return affected rows which add provider information</returns>
        public static int AddPrivider(ProviderInfoModel provider)
        {          
           return ProviderInfoDAL.AddProverderInfo(provider);
        }

        /// <summary>
        /// Delete provider information by id  ---DS2012
        /// 删除供应商
        /// </summary>
        /// <param name="ID">ID(Identity Column)</param>
        /// <returns>Retrun affected rows which delete provider information by id</returns>
        public static int Delprivider(int id)
        {
            return ProviderInfoDAL.DelProviderInfobyID(id);
        }

        /// <summary>
        /// 根据id查询供应商 ---DS2012
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ProviderInfoModel GetProviderInfoById(int id)
        { 
            return ProviderInfoDAL.GetProviderInfoById(id);
        }

        /// <summary>
        /// Update the provider information by ID  ---DS2012
        /// 修改供应商信息
        /// </summary>
        /// <param name="provider">ProviderInfo model</param>
        /// <returns>Return affected rows which update the provider information by ID</returns>
        public static int UpdatePrivider(ProviderInfoModel provider)
        {            
            return ProviderInfoDAL.UpdateProviderInfo(provider);
        }

        /// <summary>
        /// Get the all provider information
        /// </summary>
        /// <returns>Return datatable object</returns>
        public static DataTable GetProviderInfo()
        {
            return ProviderInfoDAL.GetProviderInfo();
        }

        /// <summary>
        /// Get the all provider information to the excel ---DS2012
        /// </summary>
        /// <returns>Return datatable object</returns>
        public static DataTable GetProviderInfoToExcel()
        {
            return ProviderInfoDAL.GetProviderInfoToExcel();                            
        }

        /// <summary>
        /// 查询全部供应商
        /// </summary>
        /// <returns></returns>
        public static IList<ProviderInfoModel> GetAllProvider()
        {
            return null;
        }

        /// <summary>
        /// 根据编号和名称查询供应商
        /// </summary>
        /// <param name="providerId">编号</param>
        /// <param name="providerName">名称</param>
        /// <returns></returns>
        public static IList<ProviderInfoModel> GetProvider(string providerId, string providerName)
        {
            return null;
        }

        /// <summary>
        /// 获取所有仓库权限控制编号
        /// </summary>
        /// <returns></returns>
        public static IList<WareHouseModel> GetAllWareHouse()
        {
            return WareHouseDAL.GetWareHouses();
        }

        /// <summary>
        /// Judge the ProviderId whether has operation by Id before delete ---DS2012
        /// 删除和修改供货商判断是否有业务
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the Provider by Id业务数量</returns>
        public static int ProviderIdWhetherHasOperation(int Id)
        {            
            return ProviderInfoDAL.ProviderIdWhetherHasOperation(Id);
        }

        /// <summary>
        ///Judge the ProviderId whether exist by Id before delete or update  ---ds2012
        /// 修改和删除加盟商时判断加盟商是否存在
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the Provider by Id，返回加盟商数量</returns>
        public static int ProviderIdIsExist(int Id)
        {            
            return ProviderInfoDAL.ProviderIdIsExist(Id);
        }
 
        /// <summary>
        /// Judge the ProviderName whether exist by id and providerName before update ---DS2012
        /// </summary>
        /// <param name="Id">Id</param>
        /// <param name="providerName">ProviderName</param>
        /// <returns>Return the counts of the providerName by Id and providerName</returns>
        public static int ProviderNameIsExist(int Id, string providerName)
        {
            return ProviderInfoDAL.ProviderNameIsExist(Id,providerName);
        }
        /// <summary>
        /// Judge the ProviderName whether exist by id and providerName before update ---DS2012
        /// </summary>
        /// <param name="Id">Id</param>
        /// <param name="providerName">ProviderName</param>
        /// <returns>Return the counts of the providerName by Id and providerName</returns>
        public static int ProviderNameIsExist(string providerName)
        {
            return ProviderInfoDAL.ProviderNameIsExist(providerName);
        }

        public static string GetCountryByBank(string BankName)
        {
            return ProviderInfoDAL.GetCountryByBank(BankName);
        }
    }
}
