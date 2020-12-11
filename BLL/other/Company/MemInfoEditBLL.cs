using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data;
using System.Data.SqlClient;

/**
 * 创建者;刘文
 * 创建时间：2009-8-27
 * 修改者：汪华
 * 修改时间：2009-09-07
 * 文件名：MemInfoEditBLL
 * 功能：会员信息编辑
 * **/
namespace BLL.other.Company
{
    public class MemInfoEditBLL
    {
        MemberInfoDAL memberInfoDAL = new MemberInfoDAL();

        /// <summary>
        /// 根据查询条件获取会员
        /// </summary>
        public DataTable getMemberAll(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
            return memberInfoDAL.getMemberAll(PageIndex, PageSize, table, columns, condition, key, out  RecordCount, out  PageCount);
        }
        /// <summary>
        /// 根据Number获得单个会员的信息
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <returns></returns>
        public static MemberInfoModel getMemberInfo(string Number)
        {
            return MemberInfoDAL.getMemberInfo(Number);
        }
        /// <summary>
        /// 对单个会员的信息进行编辑
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <returns></returns>
        public int updateMember(string Number, string Placement, string Direct, string Name, string PetName, DateTime Birthday, int Sex, string PostalCode, string HomeTele, string OfficeTele, string MobileTele, string FaxTele, string Country, string Province, string City, string Address, string PaperType, string PaperNumber, string BankCountry, string BankProvince, string BankCity, string Bank, string BankAddress, string BankCard, string BankBook, int ExpectNum, string Remark, string OrderId, string StoreId, string ChangeInfo, string OperateIp, string OperaterNum)
        {
            return memberInfoDAL.updateMember(Number, Placement, Direct, Name, PetName, Birthday, Sex, PostalCode, HomeTele, OfficeTele, MobileTele, FaxTele, Country, Province, City, Address, PaperType, PaperNumber, BankCountry, BankProvince, BankCity, Bank, BankAddress, BankCard, BankBook, ExpectNum, Remark, OrderId, StoreId, ChangeInfo, OperateIp, OperaterNum);
        }
        /// <summary>
        /// 对单个会员的信息进行编辑
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <returns></returns>
        public int updateMember(MemberInfoModel info)
        {
            return memberInfoDAL.updateMember(info);
        }

        public static int checkNumber(string NumberName)
        {
            return MemberInfoDAL.checkNumber(NumberName);
        }

         /// <summary>
        /// 修改会员基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Updmemberbasic(MemberInfoModel info)
        {
            BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("memberinfo", "number");//申明日志对象

            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (!MemberInfoDAL.Updmemberbasic(tran,info))
                    {
                        tran.Rollback();
                        return false;
                    }
                    //添加日志
                    cl_h_info.AddRecordtran(tran, info.Number);
                    if (System.Web.HttpContext.Current.Session["Company"] != null)
                    {
                        cl_h_info.ModifiedIntoLogstran(tran, CommonClass.ChangeCategory.Order, info.Number, BLL.CommonClass.ENUM_USERTYPE.Company);
                    }
                    else if (System.Web.HttpContext.Current.Session["Store"] != null)
                    {
                        cl_h_info.ModifiedIntoLogstran(tran, CommonClass.ChangeCategory.Order, info.Number, BLL.CommonClass.ENUM_USERTYPE.Store);
                    }
                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
         /// <summary>
        /// 修改会员联系信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdmemContact(MemberInfoModel info)
        {
            BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("memberinfo", "number");//申明日志对象

            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (!MemberInfoDAL.UpdmemContact(tran,info) )
                    {
                        tran.Rollback();
                        return false;
                    }
                    //添加日志
                    cl_h_info.AddRecordtran(tran, info.Number);
                    if (System.Web.HttpContext.Current.Session["Company"] != null)
                    {
                        cl_h_info.ModifiedIntoLogstran(tran, CommonClass.ChangeCategory.Order, info.Number, BLL.CommonClass.ENUM_USERTYPE.Company);
                    }
                    else if (System.Web.HttpContext.Current.Session["Store"] != null)
                    {
                        cl_h_info.ModifiedIntoLogstran(tran, CommonClass.ChangeCategory.Order, info.Number, BLL.CommonClass.ENUM_USERTYPE.Store);
                    }
                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
            
        }
          /// <summary>
        /// 修改会员银行信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdmemBank(MemberInfoModel info)
        {
            BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("memberinfo", "number");//申明日志对象

            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (!MemberInfoDAL.UpdmemBank(tran, info))
                    {
                        tran.Rollback();
                        return false;
                    }
                    //添加日志
                    cl_h_info.AddRecordtran(tran, info.Number);
                    if (System.Web.HttpContext.Current.Session["Company"] != null)
                    {
                        cl_h_info.ModifiedIntoLogstran(tran, CommonClass.ChangeCategory.Order, info.Number, BLL.CommonClass.ENUM_USERTYPE.Company);
                    }
                    else if (System.Web.HttpContext.Current.Session["Store"] != null)
                    {
                        cl_h_info.ModifiedIntoLogstran(tran, CommonClass.ChangeCategory.Order, info.Number, BLL.CommonClass.ENUM_USERTYPE.Store);
                    }
                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

                /// <summary>
        /// 获取银行国家
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string Getbank(string number)
        {
            return MemberInfoDAL.Getbank(number);
        }

         /// <summary>
        /// 获取银行国家 字符串
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetbankStr(string number)
        {
            return MemberInfoDAL.GetbankStr(number);
        }
          /// <summary>
        /// 获取银行国家 字符串
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Boolean GetStorenumber(string number)
        {
               return MemberInfoDAL.GetStorenumber(number);
        }
    }
}
