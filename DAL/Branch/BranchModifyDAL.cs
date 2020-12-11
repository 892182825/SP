using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model.Branch;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
namespace DAL.Branch
{
    /// <summary>
    /// 分公司基本信息
    /// </summary>
    public class BranchModifyDAL
    {
        /// <summary>
        /// 获取国家
        /// </summary>
        /// <param name="cityCode"></param>
        /// <returns></returns>
        public static string GetCity(string cityCode)
        {
            string sql = "select country from city where cpccode=@cpccode";
            SqlParameter[] par = new SqlParameter[] { 
            new SqlParameter("@cpccode",cityCode)
            };
            return DBHelper.ExecuteScalar(sql, par, CommandType.Text) != null ? DBHelper.ExecuteScalar(sql, par, CommandType.Text).ToString() : null;
        }

        /// <summary>
        /// 判断该分公司是不是可以删除
        /// </summary>
        /// <param name="numnber"></param>
        /// <returns> 0 表示没有数据 1 表示有数据（不能删除）</returns>
        public static Boolean GetBranchDel(string numnber)
        {
            string sql = "select count(@@rowcount) from branchinfo as b left join storeinfo as d on b.number=d.number where b.number=@number";
            SqlParameter[] par = new SqlParameter[] { 
                new SqlParameter("@number",numnber)
            };
            return Convert.ToInt32(DBHelper.ExecuteScalar(sql, par, CommandType.Text).ToString()) <= 0 ? false : true;
        }

        /// <summary>
        /// 绑定银行国家
        /// </summary>
        /// <returns></returns>
        public static List<ListItem> GetBankcity(string bankcode)
        {
            string sql = "Select id,name,countrycode From country order by id";
            SqlDataReader read = DBHelper.ExecuteReader(sql, CommandType.Text);
            List<ListItem> list = new List<ListItem>();
            while (read.Read())
            {
                ListItem item = new ListItem();
                item.Text = read["name"].ToString();
                item.Value = read["countrycode"].ToString();
                if ((!string.IsNullOrEmpty(bankcode)) &&bankcode.StartsWith(read["countrycode"].ToString()))
                {
                    item.Selected = true;
                }
                list.Add(item);
            }
            read.Close();
            return list;
        }

        /// <summary>
        /// 绑定银行国家
        /// </summary>
        /// <returns></returns>
        public static List<ListItem> GetBankcity()
        {
            string sql = "Select id,name,countrycode From country order by id";
            SqlDataReader read = DBHelper.ExecuteReader(sql, CommandType.Text);
            List<ListItem> list = new List<ListItem>();
            while (read.Read())
            {
                ListItem item = new ListItem();
                item.Text = read["name"].ToString();
                item.Value = read["countrycode"].ToString();
                list.Add(item);
            }
            read.Close();
            return list;
        }
        /// <summary>
        /// 获取分公司信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static BranchModel GetBreach(string number)
        {
            string sql = "select * from branchinfo where number=@number";
            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("@number",number)
            };
            SqlDataReader read = DBHelper.ExecuteReader(sql, par, CommandType.Text);
            BranchModel model = null;
            while (read.Read())
            {
                model = new BranchModel();
                model.ID = Convert.ToInt32(read["id"].ToString());
                model.Number = read["Number"].ToString();
                model.Name = read["Name"].ToString();
                model.Forshort = read["ForShort"].ToString();
                model.Linkman = read["LinkMan"].ToString();
                model.Mobile = read["Mobile"].ToString();
                model.Telephone = read["Telephone"].ToString();
                model.Fax = read["Fax"].ToString();
                model.Email = read["Email"].ToString();
                model.Url = read["Url"].ToString();
                model.Cpccode = read["CPCCode"].ToString();
                model.Address = read["Address"].ToString();
                model.Bankcode = read["Bankcode"].ToString();
                model.Bcpccode = read["BCPCCode"].ToString();
                model.Bankaddress = read["BankAddress"].ToString();
                model.Bankuser = read["Bankuser"].ToString();
                model.Banknumber = read["BankNumber"].ToString();
                model.Remark = read["Remark"].ToString();
                //model.Status = read["Status"].ToString();
                //model.Permissionman = read["PermissionMan"].ToString();
                model.Operaterip = read["OperateIP"].ToString();
                model.Operatenum = read["OperateNum"].ToString();
                model.Regdate =Convert.ToDateTime(read["regdate"].ToString());
            }
            read.Close();
            return model;
        }

        /// <summary>
        /// 修改分公司休息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Boolean UpbranchModify(BranchModel model)
        {
            string sql = "update branchinfo set Name=@Name,ForShort=@ForShort,LinkMan=@LinkMan,Mobile=@Mobile,Telephone=@Telephone,Fax=@Fax,Email=@Email,Url=@Url,CPCCode=@CPCCode,Address=@Address,Bankcode=@Bankcode,BCPCCode=@BCPCCode,BankAddress=@BankAddress,Bankuser=@Bankuser,BankNumber=@BankNumber,Remark=@Remark,OperateIP=@OperateIP,OperateNum=@OperateNum where Number=@Number";
            SqlParameter[] para = new SqlParameter[]{
                    new SqlParameter("@Name",SqlDbType.NVarChar,50),
                    new SqlParameter("@ForShort",SqlDbType.NVarChar,250),
                    new SqlParameter("@LinkMan",SqlDbType.NVarChar),
                    new SqlParameter("@Mobile",SqlDbType.NVarChar,50),
                    new SqlParameter("@Telephone",SqlDbType.NVarChar,50),
                    new SqlParameter("@Fax",SqlDbType.NVarChar,50),
                    new SqlParameter("@Email",SqlDbType.NVarChar,50),
                    new SqlParameter("@Url",SqlDbType.NVarChar,50),
                    new SqlParameter("@CPCCode",SqlDbType.NVarChar,10),
                    new SqlParameter("@Address",SqlDbType.NVarChar,500),
                    new SqlParameter("@Bankcode",SqlDbType.NVarChar,10),
                    new SqlParameter("@BCPCCode",SqlDbType.NVarChar,10),
                    new SqlParameter("@BankAddress",SqlDbType.NVarChar,500),
                    new SqlParameter("@Bankuser",SqlDbType.NVarChar,500),
                     new SqlParameter("@BankNumber",SqlDbType.NVarChar,50),
                    new SqlParameter("@Remark",SqlDbType.Text),
                    new SqlParameter("@OperateIP",SqlDbType.NVarChar,30),
                    new SqlParameter("@OperateNum",SqlDbType.NVarChar,30),
                     new SqlParameter("@Number",SqlDbType.NVarChar,50)
            };
            //para[0].Value = model.Name;
            //para[1].Value = model.Forshort;
            //para[2].Value = model.Linkman;
            //para[3].Value = model.Mobile;
            //para[4].Value = model.Telephone;
            //para[5].Value = model.Fax;
            //para[6].Value = model.Email;
            //para[7].Value = model.Cpccode;
            //para[8].Value = model.Address;
            //para[9].Value = model.Bankcode;
            //para[10].Value = model.Bcpccode;
            //para[11].Value = model.Bankaddress;
            //para[12].Value = model.Bankuser;
            //para[13].Value = model.Banknumber;
            //para[14].Value = model.Remark;
            //para[15].Value = model.Operaterip;
            //para[16].Value = model.Operatenum;
            //para[17].Value = model.Number;

                 int i= DBHelper.ExecuteNonQuery(sql,para,CommandType.Text);
                 return i == 0 ? false : true;
        }

        /// <summary>
        /// 判断该分公司编号是否存在
        /// </summary>
        /// <param name="numnber"></param>
        /// <returns> 0 表示不存在 1 表示已存在</returns>
        public static Boolean GetBranchNumber(SqlTransaction tran,string numnber)
        {
            string sql = "select count(@@rowcount) from branchinfo where number= @number";
            SqlParameter[] par = new SqlParameter[] { 
                new SqlParameter("@number",numnber)
            };
            return Convert.ToInt32(DBHelper.ExecuteScalar(tran,sql, par, CommandType.Text).ToString()) <= 0 ? false : true;
        }

        /// <summary>
        /// 添加分公司休息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Boolean AddbranchModify(SqlTransaction tran, BranchModel model)
        {
            SqlParameter[] para = new SqlParameter[]{
                    new SqlParameter("@Number",SqlDbType.NVarChar,50),
                    new SqlParameter("@Name",SqlDbType.NVarChar,50),
                    new SqlParameter("@ForShort",SqlDbType.NVarChar,250),
                    new SqlParameter("@LinkMan",SqlDbType.NVarChar),
                    new SqlParameter("@Mobile",SqlDbType.NVarChar,50),
                    new SqlParameter("@Telephone",SqlDbType.NVarChar,50),
                    new SqlParameter("@Fax",SqlDbType.NVarChar,50),
                    new SqlParameter("@Email",SqlDbType.NVarChar,50),
                    new SqlParameter("@Url",SqlDbType.NVarChar,50),
                    new SqlParameter("@CPCCode",SqlDbType.NVarChar,10),
                    new SqlParameter("@Address",SqlDbType.NVarChar,500),
                    new SqlParameter("@Bankcode",SqlDbType.NVarChar,10),
                    new SqlParameter("@BCPCCode",SqlDbType.NVarChar,10),
                    new SqlParameter("@BankAddress",SqlDbType.NVarChar,500),
                    new SqlParameter("@Bankuser",SqlDbType.NVarChar,500),
                    new SqlParameter("@BankNumber",SqlDbType.NVarChar,50),
                    new SqlParameter("@Remark",SqlDbType.Text),
                    new SqlParameter("@OperateIP",SqlDbType.NVarChar,30),
                    new SqlParameter("@OperateNum",SqlDbType.NVarChar,30),
                    new SqlParameter("@regdate",SqlDbType.DateTime),
                    new SqlParameter("@rowcount",SqlDbType.Int)
            };
            para[0].Value = model.Number;
            para[1].Value = model.Name;
            para[2].Value = model.Forshort;
            para[3].Value = model.Linkman;
            para[4].Value = model.Mobile;
            para[5].Value = model.Telephone;
            para[6].Value = model.Fax;
            para[7].Value = model.Email;
            para[8].Value = model.Url;
            para[9].Value = model.Cpccode;
            para[10].Value = model.Address;
            para[11].Value = model.Bankcode;
            para[12].Value = model.Bcpccode;
            para[13].Value = model.Bankaddress;
            para[14].Value = model.Bankuser;
            para[15].Value = model.Banknumber;
            para[16].Value = model.Remark;
            para[17].Value = model.Operaterip;
            para[18].Value = model.Operatenum;
            para[19].Value = DateTime.Now;
            para[20].Direction = ParameterDirection.Output;
            int i = DBHelper.ExecuteNonQuery(tran,"Add_proc_branch", para, CommandType.StoredProcedure);

            int id =Convert.ToInt32(DBHelper.ExecuteScalar(tran,"select id from branchmanage where branchnumber='"+model.Number+"'",CommandType.Text));

            string[] num = { "101", "102", "103", "104", "105", "106", "107", "108", "109", "110", "201", "202", "203", "204", "205", "206", "301", "302", "401", "402", "403", "404", "501", "502", "503", "504", "505", "506", "601", "602", "603", "604" };

            return int.Parse(para[20].Value.ToString()) == 0 ? true : false;
        }

        /// <summary>
        /// 删除分公司
        /// </summary>
        /// <param name="numnber"></param>
        /// <returns> 0 删除失败 1 删除成功</returns>
        public static Boolean DELBranchNumber(string number)
        {
            string sql = "delete branchinfo where number=@number";
            SqlParameter[] par = new SqlParameter[] { 
                new SqlParameter("@number",number)
            };
            return Convert.ToInt32(DBHelper.ExecuteScalar(sql, par, CommandType.Text).ToString()) <= 0 ? false : true;
        }
    }
}
