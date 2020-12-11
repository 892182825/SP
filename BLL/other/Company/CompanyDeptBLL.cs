using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using BLL.CommonClass;
using Model.Other;


/*
 *  创建人：  孙延昊
 *  创建时间：2009.8.27
 *  文件名：PermissionBLL.cs
 *  功能：公司部门添加，修改，删除
 * **/
namespace BLL.other.Company
{
    /// <summary>
    /// 公司部门
    /// </summary>
    public class CompanyDeptBLL
    {
        CompanyDeptDAL companyDeptDAL = new CompanyDeptDAL();
        /// <summary>
        /// 获取公司所有部门的信息
        /// </summary>
        /// <returns></returns>
        public static IList<CompanyDeptModel> GetCompanyDept()
        {
            return CompanyDeptDAL.GetCompanyDepts();
        }

        /// <summary>
        /// 添加公司部门信息
        /// </summary>
        /// <param name="companyDept">部门信息</param>
        /// <returns></returns>
        public static bool AddCompanyDept(CompanyDeptModel companyDept)
        {
            bool suc = false;
            suc = CompanyDeptDAL.AddCompanyDept(companyDept) == 1;
            return suc;
        }

        /// <summary>
        /// ds2012
        /// 修改公司部门信息
        /// </summary>
        /// <param name="companyDept">部门信息</param>
        /// <returns></returns>
        public static bool UptCompanyDept(CompanyDeptModel companyDept)
        {
            return CompanyDeptDAL.UptCompanyDept(companyDept) == 1;
        }

        /// <summary>
        /// ds2012
        /// 删除公司部门
        /// </summary>
        /// <param name="id">部门编号</param>
        /// <returns></returns>
        public static bool DelCompanyDept(int id)
        {
            //删除公司部门
            return CompanyDeptDAL.DelCompanyDept(id);
        }
        /// <summary>
        /// 部门是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsHaaveCompanyDept(int id)
        {
            return CompanyDeptDAL.IsHaaveCompanyDept(id);
        }

        /// <summary>
        /// 判断公司部门是否已经建立角色
        /// </summary>
        /// <param name="id">部门编号</param>
        /// <returns></returns>
        public static bool GetDeptRoleCount(int id)
        {
            return CompanyDeptDAL.GetDeptRoleCount(id);
        }

        /// <summary>
        /// ds2012
        /// 判断公司部门是否重名
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="companyID">公司部门编号（为-1表明是新添加的部门）</param>
        /// <returns></returns>
        public static bool CheckName(string name,int companyID)
        {
            //返回是否通过重名验证
            return CompanyDeptDAL.CheckName(name, companyID);
        }


        /// <summary>
        /// ds2012
        /// 根据编号获取公司部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CompanyDeptModel GetCompanyDept(int id)
        {
            return CompanyDeptDAL.GetCompanyDeptsk(id);
        }

        public static IList<CompanyDeptModel> GetCompanyDept(string ids)
        {
            if (ids == "")
                return null;
            return CompanyDeptDAL.GetCompanyDepts(ids);
        }
    }
}
