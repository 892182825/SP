using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using DAL;
namespace BLL.MoneyFlows
{
    /// <summary>
    /// 
    /// 账户管理
    /// </summary>
    public class CompanyBankBLL
    {
        /// <summary>
        /// 查询账户——ds2012——tianfeng
        /// </summary>
        /// <param name="countryID">国家ID</param>
        /// <returns></returns>
        public List<CompanyBankModel> GetCompanyBank(int countryID)
        {
            CompanyBankDAL bank = new CompanyBankDAL();
            return bank.GetCompanyBank(countryID);
        }
        /// <summary>
        /// 添加账户——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static Boolean AddCompanyBank(CompanyBankModel companyBank)
        {
            return CompanyBankDAL.AddCompanyBank(companyBank)==0 ? false : true;
        }
          
        /// <summary>
        /// 删除账户  ——ds2012——tianfeng
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Boolean DelCompanyBank(int ID)
        {
            return CompanyBankDAL.DelCompanyBank(ID) == 0 ? false : true;
        }
         
        /// <summary>
        /// 修改账户——ds2012——tianfeng
        /// </summary>
        /// <param name="bank"></param>
        /// <returns></returns>
        public static int UpdCompanyBank(CompanyBankModel model)
        {
            return CompanyBankDAL.UpdCompanyBank(model);
        }
        /// <summary>
        /// 验证账户是否已存在了——ds2012——tianfeng
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Boolean ValidateCompanyBank(CompanyBankModel company)
        {
            return CompanyBankDAL.ValidateCompanyBank(company) == 0 ? false : true;
        }
        /// <summary>
        /// 获取国家id，name——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static List<CountryModel> GetCountry()
        {
            return CountryDAL.GetCountry();
        }

        /// <summary>
        /// 获取国家name——ds2012——tianfeng
        /// </summary>
        /// <param name="ID">国家ID</param>
        /// <returns></returns>
        public static string GetCountryByID(int ID)
        {
            return CountryDAL.GetCountryByID(ID);
        }

    }

}
