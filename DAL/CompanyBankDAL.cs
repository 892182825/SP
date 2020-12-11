using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data; 
using Model;
namespace DAL
{

    /**
    * 账户管理——ds2012——tianfeng
    ***/
    public class CompanyBankDAL
    {
        /// <summary>
        /// 查询账户——ds2012——tianfeng
        /// </summary>
        /// <param name="countryID">国家ID</param>
        /// <returns></returns>
        public List<CompanyBankModel> GetCompanyBank(int countryID)
        {
            List<CompanyBankModel> list = new List<CompanyBankModel>();
            //按国家查询公司账户信息
            string sql = "select id,Bank,countryID,BankBook from companybank where countryID=" + countryID;
            SqlDataReader read = DBHelper.ExecuteReader(sql, CommandType.Text);
            while (read.Read())
            {
                CompanyBankModel company = new CompanyBankModel();
                company.ID =  int.Parse(read["ID"].ToString());
                company.Bank = read["Bank"].ToString();
                company.CountryID = int.Parse(read["countryID"].ToString());
                company.BankBook = read["BankBook"].ToString();
                list.Add(company);
            }
            read.Close();
            return list;
        }
        /// <summary>
        /// 删除账户——ds2012——tianfeng
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DelCompanyBank(int ID)
        {
            string sql = "delete from companybank where [ID]=" + ID;
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text);
        }
        /// <summary>
        /// 添加账户——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static int AddCompanyBank(CompanyBankModel companyBank)
        {
            SqlParameter [] parameter=new SqlParameter[]{
                new SqlParameter("@Bank",companyBank.Bank),
                new SqlParameter("@BankBook",companyBank.BankBook),
                new SqlParameter("@CountryID",companyBank.CountryID),
                new SqlParameter("@Bankname",companyBank.Bankname)
            };
            return DBHelper.ExecuteNonQuery("AddcompanyBank",parameter,CommandType.StoredProcedure);
        }
        /// <summary>
        /// 修改账户 ——ds2012——tianfeng
        /// </summary> 
        /// <param name="bank"></param>
        /// <returns></returns>
        public static int UpdCompanyBank(CompanyBankModel bank)
        { 
            SqlParameter[] parameter = new SqlParameter[]{
                 new SqlParameter("@id",SqlDbType.Int),
                new SqlParameter("@Bank",SqlDbType.VarChar,50),
                new SqlParameter("@BankBook",SqlDbType.VarChar,50),
                new SqlParameter("@CountryID",SqlDbType.Int),
                new SqlParameter("@Bankname",SqlDbType.VarChar,50)
            };
            parameter[0].Value = bank.ID;
            parameter[1].Value = bank.Bank;
            parameter[2].Value = bank.BankBook;
            parameter[3].Value = bank.CountryID;
            parameter[4].Value = bank.Bankname;
            return DBHelper.ExecuteNonQuery("update CompanyBank set bank=@bank,bankbook=@bankbook,countryid=@countryid,bankname=@Bankname where id=@id", parameter, CommandType.Text);
        }

        /// <summary>
        /// 验证账户是否已存在——ds2012——tianfeng
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static int ValidateCompanyBank(CompanyBankModel company)
        {
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@Bank",SqlDbType.VarChar,50),
                new SqlParameter("@BankBook",SqlDbType.VarChar,50),
                new SqlParameter("@CountryID",SqlDbType.VarChar,50),
                new SqlParameter("@count",SqlDbType.Int)
            };
            parameter[0].Value = company.Bank;
            parameter[1].Value = company.BankBook;
            parameter[2].Value = company.CountryID;
            parameter[3].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery("validateCompanyBank", parameter, CommandType.StoredProcedure);
            return int.Parse(parameter[3].Value.ToString());

        }

        /// <summary>
        /// 获取所有银行信息
        /// </summary>
        /// <returns></returns>
        public static DataTable  getdtallcompanybank()
        {
            string sqlstr = "select * from companybank order by id";
            DataTable dt = DBHelper.ExecuteDataTable(sqlstr);
            return dt;
        }

        /// <summary>
        /// 根据登录者获取指定银行信息
        /// </summary>
        /// <returns></returns>
        public static DataTable getdtcompanybankbynumber(string number, int rotype)
        {
          
            string sqlstr = "";
            if (rotype == 1) sqlstr = "select * from companybank where id  =(select bankid from memberinfo where number =@number) order by id";
            else if (rotype == 2) sqlstr = "select * from companybank where id  =(select bankid from memberinfo where number=(select number from  storeinfo   where storeid =@number)) order by id";
            SqlParameter[] sps = new SqlParameter[] { new SqlParameter("@number", number) };
            DataTable dt = DBHelper.ExecuteDataTable(sqlstr, sps, CommandType.Text);
            if (dt == null || dt.Rows.Count == 0)
            {
                sqlstr = "select top 1 * from companybank order by id";
                dt = DBHelper.ExecuteDataTable(sqlstr);
            }
            return dt;
        }

        public static List<CompanyBankModel> GetCompanyBanks()
        {
            List<CompanyBankModel> list = new List<CompanyBankModel>();
            //按国家查询公司账户信息
            string sql = "select  * from companybank  order by id ";
            DataTable dt = DBHelper.ExecuteDataTable(sql, CommandType.Text);
            CompanyBankModel company = null;
            foreach (DataRow dr in dt.Rows)
            {
                company = new CompanyBankModel();
                company.ID = int.Parse(dr["ID"].ToString());
                company.Bank = dr["Bank"].ToString();
                company.CountryID = int.Parse(dr["countryID"].ToString());
                company.BankBook = dr["BankBook"].ToString();
                company.Bankname = dr["bankname"].ToString();
                list.Add(company);
            }
            return list;
        }

    }
}
