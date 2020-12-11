using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

///Add Namespace
using System.Data;
using System.Data.SqlClient;
using Model;

/*
 *Creator:      WangHua
 *CreateDate:   2009-10-11
 *FinishDate:   2010-01-28
 *Function：     Member bank operation
 */

namespace DAL
{
    public class MemberBankDAL
    {
        /// <summary>
        /// 向会员使用银行表中插入相关记录
        /// </summary>
        /// <param name="memberBank">会员使用银行模型</param>
        /// <returns>返回向会员使用银行表中插入相关记录所影响的行数</returns>
        public static int AddMemberBank(MemberBankModel memberBank)
        {
            int zc = 0;
            string zcode = "";
            string sql1 = "select countrycode from country where id=@num";
            SqlParameter spa = new SqlParameter("@num", SqlDbType.Int);
            spa.Value = memberBank.CountryCode;
            string bcode = DBHelper.ExecuteScalar(sql1,spa,CommandType.Text).ToString();
            zcode = bcode + "0001";
            string sql2 = "select count(0) from memberbank where substring(bankcode,1,2)=@num";
            SqlParameter spa2 = new SqlParameter("@num", SqlDbType.NVarChar,10);
            spa2.Value = bcode;
            if (Convert.ToInt32(DBHelper.ExecuteScalar(sql2,spa2,CommandType.Text)) > 0)
            {
                string sql3 = "select top 1 bankcode from memberbank where substring(bankcode,1,2)=@num order by bankcode desc";
                SqlParameter spa3 = new SqlParameter("@num", SqlDbType.NVarChar, 10);
                spa3.Value = bcode;
                bcode = DBHelper.ExecuteScalar(sql3,spa3,CommandType.Text).ToString();
                zc = Convert.ToInt32(bcode) + 1;
                if (zc.ToString().Length == 5)
                {
                    zcode = "0" + zc.ToString();
                }
                else if (zc.ToString().Length == 4)
                {
                    zcode = "00" + zc.ToString();
                }
                else if (zc.ToString().Length == 3)
                {
                    zcode = "000" + zc.ToString();
                }
                else if (zc.ToString().Length == 2)
                {
                    zcode = "0000" + zc.ToString();
                }
                else if (zc.ToString().Length == 1)
                {
                    zcode = "00000" + zc.ToString();
                }
                else
                {
                    zcode = zc.ToString();
                }
            }

            SqlParameter[] sparams = new SqlParameter[]
            {               
                new SqlParameter("@bankName",SqlDbType.VarChar,30),
                new SqlParameter("@countryCode",SqlDbType.Int),
                new SqlParameter("@bankcode",SqlDbType.VarChar,20)
            };
            sparams[0].Value = memberBank.BankName;
            sparams[1].Value = memberBank.CountryCode;
            sparams[2].Value = zcode;

            return DBHelper.ExecuteNonQuery("AddMemberBank", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 删除指定的银行信息
        /// </summary>
        /// <param name="bankID">银行编号</param>
        /// <returns>返回删除指定的银行信息所影响的行数</returns>
        public static int DelMemberBankByID(int bankID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@bankID",SqlDbType.Int)
            };
            sparams[0].Value = bankID;
            
            return DBHelper.ExecuteNonQuery("DelMemberBankByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 更新指定的银行信息
        /// </summary>
        /// <param name="memberBank">会员使用银行模型</param>
        /// <returns>返回更新指定的银行信息所影响的行数</returns>
        public static int UpdMemberBankByID(MemberBankModel memberBank)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@bankID",SqlDbType.Int),
                new SqlParameter("@bankName",SqlDbType.VarChar,30),
                new SqlParameter("@countryCode",SqlDbType.Int)
            };
            sparams[0].Value = memberBank.BankID;
            sparams[1].Value = memberBank.BankName;
            sparams[2].Value = memberBank.CountryCode;
                        
            return DBHelper.ExecuteNonQuery("UpdMemberBankByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取指定的会员使用银行行数
        /// </summary>
        /// <param name="memberBank">会员使用银行模型</param>
        /// <returns>返回获取指定的会员使用银行行数</returns>
        public static int GetMemberBankCountByNameCountryCode(MemberBankModel memberBank)
        {
            
           

            SqlParameter[] sparams = new SqlParameter[]
            {     
                new SqlParameter("@bankName",SqlDbType.VarChar,30),
                new SqlParameter("@countryCode",SqlDbType.Int)
            };          
            sparams[0].Value = memberBank.BankName;
            sparams[1].Value = memberBank.CountryCode;

            return Convert.ToInt32(DBHelper.ExecuteScalar("GetMemberBankCountByNameCountryCode", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取指定的会员使用银行行数
        /// </summary>
        /// <param name="memberBank">会员使用银行模型</param>
        /// <returns>返回获取指定的会员使用银行行数</returns>
        public static int GetMemberBankCountByAll(MemberBankModel memberBank)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@bankID",SqlDbType.Int),
                new SqlParameter("@bankName",SqlDbType.VarChar,30),
                new SqlParameter("@countryCode",SqlDbType.Int)
            };
            sparams[0].Value = memberBank.BankID;
            sparams[1].Value = memberBank.BankName;
            sparams[2].Value = memberBank.CountryCode;
            
            return Convert.ToInt32(DBHelper.ExecuteScalar("GetMemberBankCountByAll", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取会员使用银行的银行ID和银行名称
        /// </summary>
        /// <param name="tran">要处理的事务</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMemberBankIDName(SqlTransaction tran)
        {            
            return DBHelper.ExecuteDataTable("GetMemberBankIDName", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取指定国家的会员使用银行信息
        /// </summary>
        /// <param name="countryCode">银行所属国家</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMemberBankInfoByCountryCode(int countryCode)
        {
            SqlParameter[] sparams=new SqlParameter[]
            {
                new SqlParameter("@countryCode",SqlDbType.Int)
            };
            sparams[0].Value = countryCode;
            
            return DBHelper.ExecuteDataTable("GetMemberBankInfoByCountryCode", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取所有的会员使用银行信息
        /// </summary>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMemberBankInfo()
        {            
            return DBHelper.ExecuteDataTable("GetMemberBankInfo", CommandType.StoredProcedure);
        }

        /// <summary>
        /// 根据银行编号获取指定的银行信息
        /// </summary>
        /// <param name="bankID">银行编号</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMemberBankInfoByID(int bankID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@bankID",SqlDbType.Int)
            };

            sparams[0].Value = bankID;
                                    
            return DBHelper.ExecuteDataTable("GetMemberBankInfoByID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 通过联合查询获取银行信息
        /// </summary>
        /// <param name="languageID">语言ID</param>
        /// <returns>返回DataTatble对象</returns>
        public static DataTable GetMoreMemberBankInfoByLanguageID(int languageID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@languageID",SqlDbType.Int)
            };
            sparams[0].Value = languageID;

            return DBHelper.ExecuteDataTable("GetMoreMemberBankInfoByLanguageID", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Juage the MemberBankId whether has operation before delete by Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the MemberBankId by Id</returns>
        public static int MemberBankIdWhetherHasOperation(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("MemberBankIdWhetherHasOperation", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Judge the MemberBankId whether exist by Id before delete or update
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>Return the counts of the MemberBankId by Id</returns>
        public static int MemberBankIdIsExist(int Id)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int)
            };
            sparams[0].Value = Id;

            return Convert.ToInt32(DBHelper.ExecuteScalar("MemberBankIdIsExist", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Out to excel of the all data of MemberBank
        /// </summary>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_MemberBank()
        {
            return DBHelper.ExecuteDataTable("OutToExcel_MemberBank", CommandType.StoredProcedure);
        }
    }
}
