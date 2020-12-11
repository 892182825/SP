using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Model;
using Model.Other;


/*
 * 修改人：汪华
 * 修改时间：2009-09-02
 */

namespace DAL
{
    /// <summary>
    /// 店铺基本信息
    /// </summary>
    public class StoreInfoDAL
    {
        //CommonDataDAL commonDataDAL = new CommonDataDAL();
        #region 会员所属店铺调整
        /// <summary>
        /// 会员所属店铺调整
        /// </summary>
        /// <param name="memberstoreid">会员编号</param>
        /// <param name="NewStoreid">新店铺号</param>
        /// <param name="NewStoreid">老店铺号</param>
        /// <param name="type">调整类型</param>
        /// <returns></returns>
        public static bool MemberStoreReset(string memberstoreid, string NewStoreid, string Oldstoreid, int type, int ExpectNum)
        {
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@MemberId",memberstoreid),
                new SqlParameter("@oldStoreid",Oldstoreid),
                new SqlParameter("@newStoreid",NewStoreid),
                 new SqlParameter("@ExpectNum",ExpectNum),
                  new SqlParameter("@flag",type),
            };
            if (DBHelper.ExecuteNonQuery("UpdateMemberStore", param, CommandType.StoredProcedure) > 0)
            {
                return true;
            }
            return false;
        }
        public static bool MemberStoreReset2(string memberstoreid, string NewStoreid, string Oldstoreid, int type, int ExpectNum)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@MemberId",memberstoreid),
                new SqlParameter("@oldStoreid",Oldstoreid),
                new SqlParameter("@newStoreid",NewStoreid),
                 new SqlParameter("@ExpectNum",ExpectNum),
                  new SqlParameter("@flag",type),
            };
                DBHelper.ExecuteNonQuery("UpdateMemberStore", param, CommandType.StoredProcedure);
                SqlParameter[] pa = new SqlParameter[]{
            new SqlParameter("@bianhao",memberstoreid),
             new SqlParameter("@storeid",NewStoreid)
            };
                DBHelper.ExecuteNonQuery("UpdateMemberStoreid", pa, CommandType.StoredProcedure);
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion
        /// <summary>
        /// 根据会员编号获取会员名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetMemberName(string id)
        {
            string name = DBHelper.ExecuteScalar("GetMemberName", new SqlParameter("@id", id), CommandType.StoredProcedure).ToString();
            return Encryption.Encryption.GetDecipherName(name);
        }

        /// <summary>
        /// 根据会员编号获取会员或店铺昵称
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetMemberPetName(string number)
        {
            string sql = "select Name from memberinfo where number=@number";
            SqlParameter[] par = new SqlParameter[] { 
            new SqlParameter("@number",number)
            };

            string sql1 = "select StoreName from storeinfo where storeid=@number";
            SqlParameter[] par1 = new SqlParameter[] { 
            new SqlParameter("@number",number)
            };

            object objPet = DAL.DBHelper.ExecuteScalar(sql, par, CommandType.Text);
            if (objPet != null)
            {
                return objPet.ToString();
            }
            else
            {
                object objStore = DAL.DBHelper.ExecuteScalar(sql1, par, CommandType.Text);
                if (objStore != null)
                {
                    return objStore.ToString();
                }
                return "";
            }
        }

        /// <summary>
        /// 获取店铺汇率——ds2012——tianfeng
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns></returns>
        public static int GetStoreCurrency(string storeid)
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("select Currency from storeinfo where storeid=@id", new SqlParameter[] { new SqlParameter("@id", storeid) }, CommandType.Text));
        }

        /// <summary>
        /// 获取店铺联系方式
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectStorePhone(string storeid)
        {
            return DBHelper.ExecuteDataTable("select HomeTele,OfficeTele,MobileTele,FaxTele,PostalCode,StoreAddress as Address from storeinfo where storeid=@id", new SqlParameter[] { new SqlParameter("@id", storeid) }, CommandType.Text);
        }

        /// <summary>
        /// 获取当前币种——ds2012——tianfeng
        /// </summary>
        /// <returns></returns>
        public static string GetStoreECurrency()
        {
            return (DBHelper.ExecuteScalar("select name from dbo.Currency where standardmoney=id", CommandType.Text)).ToString();
        }

        /// <summary>
        /// 获取店铺币种
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static int GetStoreRate(string storeid)
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("select rateid from storeinfo s,country c where substring(s.scpccode,1,2)=c.countrycode and storeid=@id", new SqlParameter[] { new SqlParameter("@id", storeid) }, CommandType.Text));
        }

        /// <summary>
        /// 检查店铺编号是否存在
        /// 宋俊
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static bool CheckStoreId(string storeid)
        {
            if (storeid.Length > 0)
            {
                SqlParameter[] par = new SqlParameter[]
                {
                    new SqlParameter("@StoreId", storeid),
                };
                int flag = int.Parse(DBHelper.ExecuteScalar("CheckRegisterStoreID", par, CommandType.StoredProcedure).ToString());
                if (flag > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #region 店铺注册确认
        /// <summary>
        /// 店铺注册确认--2012-06-05 CK修改
        /// </summary>
        /// <param name="StoreID">店铺编号</param>
        /// <returns></returns>
        public static bool AddStoreInfo(string StoreID)
        {
            string sql = "update storeinfo set storestate=1 where storeid=@storeid";
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@storeid",StoreID),
            };
            int flag = DBHelper.ExecuteNonQuery(sql, param, CommandType.Text);
            if (flag > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region 根据id查询店铺详细信息
        /// <summary>
        ///根据id查询店铺详细信息
        /// </summary>
        /// <param name="id">店铺编号（自动增长列）</param>
        /// <returns>店铺对象</returns>
        /// sj
        public static StoreInfoModel GetStoreInfoById(int id)
        {
            //select * from StoreInfo where Id=@id
            SqlParameter[] param = new SqlParameter[] { 
             new SqlParameter("@storeid",id)
            };
            SqlDataReader reader = DBHelper.ExecuteReader("select s.*,b.BankName from storeinfo s,memberbank b where s.id=@storeid and s.BankCode=b.BankCode ", param, CommandType.Text);
            StoreInfoModel store = new StoreInfoModel();
            while (reader.Read())
            {
                store.Number = reader["Number"].ToString();
                store.StoreID = reader["StoreID"].ToString();
                store.Direct = reader["Direct"].ToString();
                store.BankCode = reader["BankCode"].ToString();
                store.Bank = new MemberBankModel();
                store.Bank.BankName = reader["BankName"].ToString();
                store.Email = reader["Email"].ToString();
                store.ExpectNum = Convert.ToInt32(reader["ExpectNum"].ToString());
                if (reader["FareArea"].ToString() == "" || reader["FareArea"] == null)
                {
                    store.FareArea = 0;
                }
                else
                {
                    store.FareArea = Convert.ToDecimal(reader["FareArea"]);
                }
                store.FaxTele = reader["FaxTele"].ToString();
                store.HomeTele = reader["HomeTele"].ToString();
                //store.InwayCount = reader["InwayCount"].ToString();
                store.Language = Convert.ToInt32(reader["Language"]);
                store.MobileTele = reader["MobileTele"].ToString();
                store.Name = reader["Name"].ToString();
                store.NetAddress = reader["NetAddress"].ToString();
                store.CPCCode = reader["CPCCode"].ToString();
                store.OfficeTele = reader["OfficeTele"].ToString();
                store.PermissionMan = reader["PermissionMan"].ToString();
                store.PostalCode = reader["PostalCode"].ToString();
                // store.Province = reader["Province"].ToString();

                // store.RegisterDate = Convert.ToDateTime(reader["RegisterDate"]);

                if (reader["RegisterDate"].ToString() == "" || reader["RegisterDate"].ToString() == null)
                {
                    store.RegisterDate = DateTime.Now;
                }
                else
                {
                    store.RegisterDate = Convert.ToDateTime(reader["RegisterDate"]);
                }
                store.Remark = reader["Remark"].ToString();
                store.StoreAddress = reader["StoreAddress"].ToString();
                store.SCPCCode = reader["SCPCCode"].ToString();
                //store.StoreLevelStr = reader["StoreLevelStr"].ToString();
                store.StoreLevelInt = Convert.ToInt32(reader["StoreLevelInt"].ToString());
                if (reader["TotalInvestMoney"].ToString() == "" || reader["TotalInvestMoney"] == null)
                {
                    store.TotalInvestMoney = 0;
                }
                else
                {
                    store.TotalInvestMoney = decimal.Parse(reader["TotalInvestMoney"].ToString());
                }
                store.StoreName = reader["StoreName"].ToString();
                store.BankCard = reader["BankCard"].ToString();
                if (reader["TotalaccountMoney"].ToString() == "" || reader["TotalaccountMoney"] == null)
                {
                    store.TotalaccountMoney = 0;
                }
                else
                {
                    store.TotalaccountMoney = Convert.ToDecimal(reader["TotalaccountMoney"]);
                }
                if (reader["TotalordergoodMoney"].ToString() == "" || reader["TotalordergoodMoney"] == null)
                {
                    store.TotalordergoodMoney = 0;
                }
                else
                {
                    store.TotalordergoodMoney = Convert.ToDecimal(reader["TotalordergoodMoney"]);
                }

                //if (reader["PhotoH"].ToString() == "" || reader["PhotoH"] == null)
                //{
                //    store.PhotoH = 0;
                //}
                //else
                //{
                //    store.PhotoH = Convert.ToInt32(reader["PhotoH"]);
                //}
                store.PhotoPath = reader["PhotoPath"].ToString();

                //if (reader["PhotoW"].ToString() == "" || reader["PhotoW"] == null)
                //{
                //    store.PhotoH = 0;
                //}
                //else
                //{
                //    store.PhotoH = Convert.ToInt32(reader["PhotoW"]);
                //}


            }
            reader.Close();
            return store;
        }
        #endregion
        #region 根据storeid查询店铺详细信息——ds2012——tianfeng

        /// <summary>
        /// 根据storeid查询店铺详细信息——ds2012——tianfeng
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns>店铺信息</returns>
        public static StoreInfoModel GetStoreInfoByStoreId(string storeid)
        {
            SqlParameter[] param = new SqlParameter[] { 
             new SqlParameter("@storeid",storeid)
            };
            SqlDataReader reader = DBHelper.ExecuteReader("select top 1 s.*,b.BankName,c.Province as Province,c.Country as Country,c.City as City,c.Xian as Xian from storeinfo s,city c,memberbank b where s.cpccode=c.cpccode and storeid=@storeid  and s.BankCode=b.BankCode", param, CommandType.Text);
            StoreInfoModel store = new StoreInfoModel();
            while (reader.Read())
            {
                store.StoreID = reader["StoreID"].ToString();
                store.BankCode = reader["BankCode"].ToString();
                store.Bank.BankName = reader["BankName"].ToString();
                store.CPCCode = reader["CPCCode"].ToString();
                store.StoreCity.Province = reader["Province"].ToString();
                store.StoreCity.Country = reader["Country"].ToString();
                store.StoreCity.City = reader["City"].ToString();
                store.StoreCity.Xian = reader["Xian"].ToString();
                store.Email = reader["Email"].ToString();
                store.ExpectNum = Convert.ToInt32(reader["ExpectNum"].ToString());
                store.FareArea = Convert.ToDecimal(reader["FareArea"]);
                store.FaxTele = reader["FaxTele"].ToString();
                store.HomeTele = reader["HomeTele"].ToString();
                store.Language = Convert.ToInt32(reader["Language"]);
                store.MobileTele = reader["MobileTele"].ToString();
                store.Name = reader["Name"].ToString();
                store.NetAddress = reader["NetAddress"].ToString();
                store.Number = reader["Number"].ToString();
                store.OfficeTele = reader["OfficeTele"].ToString();
                store.PermissionMan = reader["PermissionMan"].ToString();
                store.PostalCode = reader["PostalCode"].ToString();
                store.Direct = reader["Direct"].ToString();
                store.RegisterDate = Convert.ToDateTime(reader["RegisterDate"]);
                store.Remark = reader["Remark"].ToString();
                store.StoreAddress = reader["StoreAddress"].ToString();
                // store.StoreLevelStr = reader["StoreLevelStr"].ToString();
                store.StoreLevelInt = Convert.ToInt32(reader["StoreLevelInt"].ToString());
                store.StoreName = reader["StoreName"].ToString();
                store.BankCard = reader["BankCard"].ToString();
                store.TotalaccountMoney = Convert.ToDecimal(reader["TotalaccountMoney"]);
                store.TotalordergoodMoney = Convert.ToDecimal(reader["TotalordergoodMoney"]);
                store.PhotoPath = reader["PhotoPath"].ToString();
                store.CPCCode = reader["CPCCode"].ToString();
                store.SCPCCode = reader["SCPCCode"].ToString();

            }
            reader.Close();
            return store;
        }
        /// <summary>
        /// 根据storeid查询会员编号——ds2012——tianfeng
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns>会员编号</returns>
        public static string GetNumberByStoreId(string storeid)
        {

            SqlParameter[] param = new SqlParameter[] { 
             new SqlParameter("@storeid",storeid)
            };
            Object obj = DBHelper.ExecuteScalar("select top 1 Number from storeinfo s where storeid=@storeid", param, CommandType.Text);
            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion
        #region  查询全部店铺信息
        /// <summary>
        /// 查询全部店铺信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <param name="RecordCount"></param>
        /// <returns></returns>
        /// sj
        public static DataTable GetStoreInfo(string Id, string Name, string StoreName, int ExpectNum, int pageIndex, int pagesize, out int pageCount, out int RecordCount)
        {
            string columns = "ID,Number,StoreID,Name,StoreName,TotalAccountMoney,(TotalAccountMoney-TotalMemberOrderMoney) as kebaodane,(TotalAccountMoney-TotalOrderGoodMoney) as kedinghuoe,ExpectNum,Recommended,Province,RegisterDate,StoreLevelStr";
            StringBuilder sb = new StringBuilder();
            sb.Append("1=1 ");
            if (Id != "")
            {
                sb.Append("and StoreId='" + Id + "'");
            }
            if (Name != "")
            {
                sb.Append(" and like Name='%" + Name + "%'");
            }
            if (StoreName != "")
            {
                sb.Append(" and like StorteName='%" + StoreName + "'%");
            }
            if (ExpectNum != 0)
            {
                sb.Append(" and ExpectNum=" + ExpectNum);
            }
            sb.Append(" order by ID desc");
            string where = sb.ToString();
            DataTable table = CommonDataDAL.GetDataTablePage_Sms(pageIndex, pagesize, "StoreInfo", columns, where, "ID", out RecordCount, out pageCount);
            return table;
        }
        #endregion
        #region 根据店铺id重置店铺密码
        /// <summary>
        /// 根据店铺id重置店铺密码
        /// </summary>
        /// <param name="storeid">店铺id</param>
        /// <returns>执行是否成功</returns>
        public static int StorePassReset(string storeid)
        {
            //string md5string = "";
            //string password = SetStorePass(storeid, out md5string);
            //SqlParameter[] param = new SqlParameter[] { 
            //    new SqlParameter("@StoreID",storeid),
            //    new SqlParameter("@NewPass",md5string)
            //};
            //if (DBHelper.ExecuteNonQuery("updStorePass", param, CommandType.StoredProcedure) > 0)
            //{ return true; }
            //else
            //{
            //    return false;
            //}
            int i = 0;
            String sqlStr = "select Number from storeinfo where storeid=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = storeid;
            object Number = DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text);
            sqlStr = "select paperNumber from storeinfo a,memberinfo b where a.number=b.number and a.storeid=@num";
            object card1 = DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text);
            string card = Encryption.Encryption.GetDecipherNumber(card1.ToString());
            if (card.Length < 6 || card == null)
            {
                string num = Encryption.Encryption.GetEncryptionPwd(Number.ToString(), storeid);
                sqlStr = "update storeinfo set LoginPass=@num where storeid=@num1";
                SqlParameter[] sParas = new SqlParameter[]{
                    new SqlParameter("@num", SqlDbType.VarChar, 100),
                    new SqlParameter("@num1", SqlDbType.NVarChar, 50)
                };
                sParas[0].Value = num;
                sParas[1].Value = storeid;
                i = (int)DBHelper.ExecuteNonQuery(sqlStr, sParas, CommandType.Text);
            }
            else
            {
                string paperNum = Encryption.Encryption.GetEncryptionPwd(card.ToString().Substring((card.ToString().Length) - 6, 6), storeid);
                sqlStr = "update storeinfo set LoginPass=@num where storeid=@num1";
                SqlParameter[] sParas = new SqlParameter[]{
                    new SqlParameter("@num", SqlDbType.VarChar, 100),
                    new SqlParameter("@num1", SqlDbType.NVarChar, 50)
                };
                sParas[0].Value = paperNum;
                sParas[1].Value = storeid;
                i = (int)DBHelper.ExecuteNonQuery(sqlStr, sParas, CommandType.Text);
            }
            return i;
        }
        public static int StorePassReset1(string storeid)
        {
            //string md5string = "";
            //string password = SetStorePass(storeid, out md5string);
            //SqlParameter[] param = new SqlParameter[] { 
            //    new SqlParameter("@StoreID",storeid),
            //    new SqlParameter("@NewPass",md5string)
            //};
            //if (DBHelper.ExecuteNonQuery("updStorePass", param, CommandType.StoredProcedure) > 0)
            //{ return true; }
            //else
            //{
            //    return false;
            //}
            int i = 0;
            String sqlStr = "select Number from storeinfo where storeid=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = storeid;
            object Number = DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text);
            sqlStr = "select paperNumber from storeinfo a,memberinfo b where a.number=b.number and a.storeid=@num";
            object card1 = DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text);
            string card = Encryption.Encryption.GetDecipherNumber(card1.ToString());
            if (card.Length < 6 || card == null)
            {
                string num = Encryption.Encryption.GetEncryptionPwd(Number.ToString(), storeid);
                sqlStr = "update storeinfo set LoginPass=@num where storeid=@num1";
                SqlParameter[] sParas = new SqlParameter[]{
                    new SqlParameter("@num", SqlDbType.VarChar, 100),
                    new SqlParameter("@num1", SqlDbType.NVarChar, 50)
                };
                sParas[0].Value = num;
                sParas[1].Value = storeid;
                i = (int)DBHelper.ExecuteNonQuery(sqlStr, sParas, CommandType.Text);
            }
            else
            {
                string paperNum = Encryption.Encryption.GetEncryptionPwd(card.ToString().Substring((card.ToString().Length) - 6, 6), storeid);
                sqlStr = "update storeinfo set LoginPass=@num where storeid=@num1";
                SqlParameter[] sParas = new SqlParameter[]{
                    new SqlParameter("@num", SqlDbType.VarChar, 100),
                    new SqlParameter("@num1", SqlDbType.NVarChar, 50)
                };
                sParas[0].Value = paperNum;
                sParas[1].Value = storeid;
                i = (int)DBHelper.ExecuteNonQuery(sqlStr, sParas, CommandType.Text);
            }
            return i;
        }

        public static int StorePassReset1(string storeid, string pass)
        {
            int i = 0;
            string num = Encryption.Encryption.GetEncryptionPwd(pass.ToString(), storeid);
            String sqlStr = "update storeinfo set advpass=@num where storeid=@num1";
            SqlParameter[] sParas = new SqlParameter[]{
                    new SqlParameter("@num", SqlDbType.NVarChar, 100),
                    new SqlParameter("@num1", SqlDbType.NVarChar, 50)
                };
            sParas[0].Value = num;
            sParas[1].Value = storeid;
            i = (int)DBHelper.ExecuteNonQuery(sqlStr, sParas, CommandType.Text);
            return i;
        }
        public static int StorePassReset(string storeid, string pass)
        {

            int i = 0;
            string num = Encryption.Encryption.GetEncryptionPwd(pass.ToString(), storeid);
            String sqlStr = "update storeinfo set LoginPass=@num where storeid=@num1";
            SqlParameter[] sParas = new SqlParameter[]{
                    new SqlParameter("@num", SqlDbType.VarChar, 100),
                    new SqlParameter("@num1", SqlDbType.NVarChar, 50)
                };
            sParas[0].Value = num;
            sParas[1].Value = storeid;
            i = (int)DBHelper.ExecuteNonQuery(sqlStr, sParas, CommandType.Text);

            return i;
        }

        #endregion
        #region 获取为店铺设置的密码
        /// <summary>
        /// 获取为店铺设置的密码
        /// </summary>
        /// <returns></returns>
        public static string SetStorePass(string storeid, out string MD5Str)
        {
            int setid = Convert.ToInt32(ConfigurationManager.AppSettings["SetStorePass"].ToString());
            string password = "";
            switch (setid)
            {
                case 1:
                    password = Common.GetReadomStr(6);
                    break;
                case 2:
                    password = SetStorePassWrod(storeid);
                    break;
                default:
                    break;
            }
            MD5Str = MD5Help.MD5Decrypt(password);
            return password;
        }
        #endregion
        #region 根据注册店铺的移动电话或根据注册店铺的会员的证件编号设置密码获取密码
        /// <summary>
        /// 根据注册店铺的移动电话或根据注册店铺的会员的证件编号设置密码获取密码
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static string SetStorePassWrod(string storeid)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@storeId",storeid)
            };
            string str = DBHelper.ExecuteScalar("GetStorePassword", param, CommandType.StoredProcedure).ToString();
            if (str.Length > 0)
            {
                return str.Substring(str.Length - 6);
            }
            else
            {

                str = Common.GetReadomStr(6);
                return str;
            }
        }
        #endregion
        #region 修改店铺信息 （公司）
        /// <summary>
        /// 修改店铺信息（公司）
        /// </summary>
        /// <param name="storeInfoMember">店铺信息</param>
        /// <returns></returns>
        public static int updateStoreInfo(StoreInfoModel storeInfoMember)
        {
            int i = 0;
            SqlParameter[] par = new SqlParameter[]
            {
                                        new SqlParameter("@Number", SqlDbType.VarChar, 50), 
										new SqlParameter("@StoreID", SqlDbType.VarChar, 50),
										new SqlParameter("@Name", SqlDbType.VarChar, 500),
										new SqlParameter("@StoreName", SqlDbType.VarChar, 500),
										new SqlParameter("@StoreAddress", SqlDbType.Text ),

										new SqlParameter("@HomeTele", SqlDbType.VarChar, 50),
										new SqlParameter("@OfficeTele", SqlDbType.VarChar, 50),
										new SqlParameter("@MobileTele", SqlDbType.VarChar, 50),
										new SqlParameter("@FaxTele", SqlDbType.VarChar, 50),
										new SqlParameter("@Bank", SqlDbType.VarChar, 80),

										new SqlParameter("@BankCard", SqlDbType.VarChar, 50),
										new SqlParameter("@Email", SqlDbType.VarChar, 50),
										new SqlParameter("@NetAddress", SqlDbType.VarChar, 50),
										new SqlParameter("@Remark", SqlDbType.Text ),
										new SqlParameter("@Recommended", SqlDbType.VarChar, 20),

										new SqlParameter("@ExpectNum", SqlDbType.Int),
										new SqlParameter("@RegisterDate", SqlDbType.DateTime),

										new SqlParameter("@FareArea", SqlDbType.Money ),
										new SqlParameter("@TotalInvestMoney", SqlDbType.Money),

										new SqlParameter("@PostalCode", SqlDbType.VarChar,20),
										new SqlParameter("@StoreLevelInt",SqlDbType.Int),
										new SqlParameter("@Language",SqlDbType.Int),
										new SqlParameter("@OperateIP",SqlDbType.VarChar),

										new SqlParameter("@OperaterNum",SqlDbType.VarChar),
									    new SqlParameter("@Photopath",SqlDbType.VarChar,50),
                                        new SqlParameter("@ID",SqlDbType.Int),
                                        new SqlParameter("@CP",SqlDbType.VarChar,20),
                                        new SqlParameter("@bankbranchname",SqlDbType.NVarChar,50)
                   

            };
            par[0].Value = storeInfoMember.Number;
            par[1].Value = storeInfoMember.StoreID;
            par[2].Value = storeInfoMember.Name;
            par[3].Value = storeInfoMember.StoreName;

            par[4].Value = storeInfoMember.StoreAddress;
            par[5].Value = storeInfoMember.HomeTele;
            par[6].Value = storeInfoMember.OfficeTele;
            par[7].Value = storeInfoMember.MobileTele;
            par[8].Value = storeInfoMember.FaxTele;
            par[9].Value = storeInfoMember.BankCode;
            par[10].Value = storeInfoMember.BankCard;
            par[11].Value = storeInfoMember.Email;
            par[12].Value = storeInfoMember.NetAddress;
            par[13].Value = storeInfoMember.Remark;
            par[14].Value = storeInfoMember.Direct;
            par[15].Value = storeInfoMember.ExpectNum;
            par[16].Value = storeInfoMember.RegisterDate;

            par[17].Value = storeInfoMember.FareArea;
            par[18].Value = storeInfoMember.TotalInvestMoney;
            par[19].Value = storeInfoMember.PostalCode;


            par[20].Value = storeInfoMember.StoreLevelInt;
            par[21].Value = storeInfoMember.Language;
            par[22].Value = storeInfoMember.OperateIp;
            par[23].Value = storeInfoMember.OperateNum;
            par[24].Value = storeInfoMember.PhotoPath;

            par[25].Value = storeInfoMember.ID;
            par[26].Value = storeInfoMember.CPCCode;
            par[27].Value = storeInfoMember.Bankbranchname;

            i = DBHelper.ExecuteNonQuery("updStore", par, CommandType.StoredProcedure);
            return i;
        }
        #endregion

        #region 删除店铺
        /// <summary>
        /// 删除店铺
        /// </summary>
        /// <param name="Number">编号</param>
        /// <returns></returns>
        public static int DelStore(int ID)
        {
            int i = 0;
            SqlParameter[] par = new SqlParameter[]
            {
                new SqlParameter("@ID",SqlDbType.Int)
            };
            par[0].Value = ID;
            i = DBHelper.ExecuteNonQuery("DelStore", par, CommandType.StoredProcedure);
            return i;
        }
        #endregion
        #region 修改店铺信息 （店铺）
        /// <summary>
        /// 修改店铺信息（店铺）
        /// </summary>
        /// <param name="storeInfoMember">店铺信息</param>
        /// <returns></returns>
        public static int updStoreInfo(StoreInfoModel storeInfoMember)
        {
            int i = 0;
            SqlParameter[] par = new SqlParameter[]
            {
                                        new SqlParameter("@Number", SqlDbType.VarChar, 10), 
										new SqlParameter("@StoreID", SqlDbType.VarChar, 10),
										new SqlParameter("@Name", SqlDbType.NVarChar, 500),
										new SqlParameter("@StoreName", SqlDbType.NVarChar, 500),
                                        new SqlParameter("@CPCCode",SqlDbType.VarChar,20),
										new SqlParameter("@StoreAddress", SqlDbType.Text ),
										new SqlParameter("@HomeTele", SqlDbType.NVarChar, 500),
										new SqlParameter("@OfficeTele", SqlDbType.NVarChar, 500),

										new SqlParameter("@MobileTele", SqlDbType.NVarChar, 500),
										new SqlParameter("@FaxTele", SqlDbType.NVarChar, 500),
										new SqlParameter("@BankCode", SqlDbType.VarChar, 80),
										new SqlParameter("@BankCard", SqlDbType.NVarChar, 500),
										new SqlParameter("@Email", SqlDbType.VarChar, 50),

										new SqlParameter("@NetAddress", SqlDbType.VarChar, 50),
										new SqlParameter("@Remark", SqlDbType.Text ),
										new SqlParameter("@ExpectNum", SqlDbType.Int),
										new SqlParameter("@RegisterDate", SqlDbType.DateTime),
										new SqlParameter("@StoreLevelInt", SqlDbType.VarChar ,10),

										new SqlParameter("@PostalCode", SqlDbType.VarChar ,20 ),

										//new SqlParameter("@StorageScalar",SqlDbType.Int),
                                        new SqlParameter("@ID",SqlDbType.Int),
                                        new SqlParameter("@PhotoPath",SqlDbType.VarChar,100)

            };
            par[0].Value = storeInfoMember.Number;
            par[1].Value = storeInfoMember.StoreID;
            par[2].Value = storeInfoMember.Name;
            par[3].Value = storeInfoMember.StoreName;

            par[4].Value = storeInfoMember.CPCCode;
            par[5].Value = storeInfoMember.StoreAddress;
            par[6].Value = storeInfoMember.HomeTele;
            par[7].Value = storeInfoMember.OfficeTele;

            par[8].Value = storeInfoMember.MobileTele;
            par[9].Value = storeInfoMember.FaxTele;
            par[10].Value = storeInfoMember.BankCode;
            par[11].Value = storeInfoMember.BankCard;
            par[12].Value = storeInfoMember.Email;

            par[13].Value = storeInfoMember.NetAddress;
            par[14].Value = storeInfoMember.Remark;
            par[15].Value = storeInfoMember.ExpectNum;
            par[16].Value = storeInfoMember.RegisterDate;
            par[17].Value = storeInfoMember.StoreLevelInt;

            par[18].Value = storeInfoMember.PostalCode;
            //par[20].Value = storeInfoMember.StorageScalar;
            par[19].Value = storeInfoMember.ID;
            par[20].Value = storeInfoMember.PhotoPath;

            i = DBHelper.ExecuteNonQuery("updStoreStore", par, CommandType.StoredProcedure);
            return i;
        }
        #endregion
        #region 修改密码（店铺）
        /// <summary>
        /// 修改密码（店铺）
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int updStorePass(string StoreID, string NewPass)
        {
            int i = 0;
            SqlParameter[] par = new SqlParameter[]
            {
                new SqlParameter("@StoreID",SqlDbType.VarChar,20),
                new SqlParameter("NewPass",SqlDbType.VarChar,80)
            };
            par[0].Value = StoreID;
            par[1].Value = NewPass;

            i = DBHelper.ExecuteNonQuery("updStorePass", par, CommandType.StoredProcedure);
            return i;
        }

        /// <summary>
        /// 修改密码（店铺）--带事务处理
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int updStoreLoginPassT(SqlTransaction tran, string StoreID, string NewPass)
        {
            int i = 0;
            string sql = "update StoreInfo set loginpass=@NewPass where StoreID=@StoreID";
            SqlParameter[] par = new SqlParameter[]
            {
                new SqlParameter("@StoreID",SqlDbType.VarChar,20),
                new SqlParameter("@NewPass",SqlDbType.VarChar,80)
            };
            par[0].Value = StoreID;
            par[1].Value = NewPass;

            i = DBHelper.ExecuteNonQuery(tran, sql, par, CommandType.Text);
            return i;
        }


        /// <summary>
        /// 修改二级密码（店铺）--带事务处理
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int updStoreAdvPassT(SqlTransaction tran, string StoreID, string NewPass)
        {
            int i = 0;
            string sql = "update StoreInfo set advpass=@NewPass where StoreID=@StoreID";
            SqlParameter[] par = new SqlParameter[]
            {
                new SqlParameter("@StoreID",SqlDbType.VarChar,20),
                new SqlParameter("@NewPass",SqlDbType.VarChar,80)
            };
            par[0].Value = StoreID;
            par[1].Value = NewPass;

            i = DBHelper.ExecuteNonQuery(tran, sql, par, CommandType.Text);
            return i;
        }

        /// <summary>
        /// 修改二级密码（店铺）
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int updStoreadvPass(string StoreID, string NewPass)
        {
            int i = 0;
            string sql = "update StoreInfo set advpass=@NewPass where StoreID=@StoreID";
            SqlParameter[] par = new SqlParameter[]
            {
                new SqlParameter("@StoreID",SqlDbType.VarChar,20),
                new SqlParameter("@NewPass",SqlDbType.VarChar,80)
            };
            par[0].Value = StoreID;
            par[1].Value = NewPass;

            i = DBHelper.ExecuteNonQuery(sql, par, CommandType.Text);
            return i;
        }
        #endregion

        #region 修改密码（分公司）
        /// <summary>
        /// 修改密码（分公司）
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int updBranchPass(string Branch, string NewPass)
        {
            int i = 0;
            string sql = "update branchmanage set loginpass=@NewPass where number=@Branch";
            SqlParameter[] par = new SqlParameter[]
            {
                new SqlParameter("@Branch",SqlDbType.VarChar,20),
                new SqlParameter("@NewPass",SqlDbType.VarChar,80)
            };
            par[0].Value = Branch;
            par[1].Value = NewPass;

            i = DBHelper.ExecuteNonQuery(sql, par, CommandType.Text);
            return i;
        }
        /// <summary>
        /// 修改二级密码（分公司）
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int updBranchadvPass(string Branch, string NewPass)
        {
            int i = 0;
            string sql = "update branchmanage set advpass=@NewPass where number=@Branch";
            SqlParameter[] par = new SqlParameter[]
            {
                new SqlParameter("@Branch",SqlDbType.VarChar,20),
                new SqlParameter("@NewPass",SqlDbType.VarChar,80)
            };
            par[0].Value = Branch;
            par[1].Value = NewPass;

            i = DBHelper.ExecuteNonQuery(sql, par, CommandType.Text);
            return i;
        }


        #endregion

        #region 修改密码（会员）


        /// <summary>
        /// 修改密码（会员）--带事务处理
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int updMemberLoginPassT(SqlTransaction tran, string number, string NewPass)
        {
            int i = 0;
            string sql = "update memberinfo set loginpass=@NewPass where number=@number";
            SqlParameter[] par = new SqlParameter[]
            {
                new SqlParameter("@number",SqlDbType.VarChar,20),
                new SqlParameter("@NewPass",SqlDbType.VarChar,80)
            };
            par[0].Value = number;
            par[1].Value = NewPass;

            i = DBHelper.ExecuteNonQuery(tran, sql, par, CommandType.Text);
            return i;
        }

        /// <summary>
        /// 修改二级密码（会员）--带事务处理
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int updMemberAdvPassT(SqlTransaction tran, string number, string NewPass)
        {
            int i = 0;
            string sql = "update memberinfo set Advpass=@NewPass where number=@number";
            SqlParameter[] par = new SqlParameter[]
            {
                new SqlParameter("@number",SqlDbType.VarChar,20),
                new SqlParameter("@NewPass",SqlDbType.VarChar,80)
            };
            par[0].Value = number;
            par[1].Value = NewPass;

            i = DBHelper.ExecuteNonQuery(tran, sql, par, CommandType.Text);
            return i;
        }

        /// <summary>
        /// 修改密码（店铺）
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="NewPass"></param>
        /// <returns></returns>
        public static int updMemberPass(string number, string NewPass, int passtype)
        {
            int i = 0;
            SqlParameter[] par = new SqlParameter[]
            {
                new SqlParameter("@Number",SqlDbType.NVarChar,50),
                new SqlParameter("@NewPass",SqlDbType.NVarChar,500),
                new SqlParameter("@passtype",SqlDbType.Int)
            };
            par[0].Value = number;
            par[1].Value = NewPass;
            par[2].Value = passtype;
            i = DBHelper.ExecuteNonQuery("updateMemberPass", par, CommandType.StoredProcedure);
            return i;
        }
        #endregion
        #region 获得店铺的ID
        /// <summary>
        /// 根据店铺ID获得对应的ID
        /// </summary>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        public static int getStoreInfoID(string StoreID)
        {
            int id = 0;
            string pwd = "";
            String sqlStr = "select id from StoreInfo where storeID=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = StoreID;
            object obj = DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text);
            if (obj != null)
            {
                id = Convert.ToInt32(obj);
            }
            return id;
        }
        #endregion

        public static DataTable GetMemberInfo(string qishu)
        {
            string str = "select b.number,a.level,b.expectnum,b.Direct  from MemberInfoBalance" + qishu + " a,MemberInfo b where a.number=b.number";
            DataTable dt = DBHelper.ExecuteDataTable(str);
            return dt;
        }

        public static DataTable GetMemberPlacement(string placeMent, string qishu)
        {
            string sql = "select * from Memberinfo where placement=@placeMent and expectNum<=@qishu";
            SqlParameter[] para = {
                                      new SqlParameter("@placeMent",SqlDbType.NVarChar,20),
                                      new SqlParameter("@qishu",SqlDbType.Int)
                                  };
            para[0].Value = placeMent;
            para[1].Value = qishu;
            DataTable dt = DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            return dt;
        }

        public static DataTable GetMemberPlacement2(string placeMent, string qishu)
        {
            string topMemberId = DAL.CommonDataDAL.GetManageID(3);
            string sql = "select *  from  memberInfo  where placeMent=@placeMent and  expectnum<=@qishu and  placement<>'" + topMemberId + "'";
            SqlParameter[] para = {
                                      new SqlParameter("@placeMent",SqlDbType.NVarChar,20),
                                      new SqlParameter("@qishu",SqlDbType.Int)
                                  };
            para[0].Value = placeMent;
            para[1].Value = qishu;
            DataTable dt = DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            return dt;
        }
        /// <summary>
        /// 获取店铺的会员编号
        /// 
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="mb"></param>
        /// <returns></returns>
        public static string getStoreInfoMemberNumber(string StoreID)
        {
            string sqlstr = "select top 1 number from memberinfo where number =(select number from storeinfo where storeid=@storeid) ";

            SqlParameter[] sps = new SqlParameter[] { new SqlParameter("@storeid", SqlDbType.VarChar, 20) };
            sps[0].Value = StoreID;

            object number = DBHelper.ExecuteScalar(sqlstr, sps, CommandType.Text);
            return number == null ? "" : number.ToString();
        }

        #region 根据店铺编号获取他的会员信息

        public static MemberInfoModel getStoreInfoMember(string StoreID)
        {
            MemberInfoModel memberInfo = null;
            SqlParameter[] par = new SqlParameter[] { new SqlParameter("@StoreID", SqlDbType.VarChar) };
            par[0].Value = StoreID;
            //DataTable dt = DBHelper.ExecuteDataTable("select top 1  c.Province as province , c.Country as country , c.City as city ,bc.Province as bprovince , bc.Country as bcountry , bc.City as bcity , mb.BankName as bankname , m.*  from MemberInfo m,city c,city bc,memberbank mb where bc.cpccode=m.bcpccode and Number=@Number and m.cpccode=c.cpccode and m.bankcode=mb.bankcode ", par, CommandType.Text);
            DataTable dt = DBHelper.ExecuteDataTable("select top 1  m.*  from MemberInfo m where Number=(select number from storeinfo where storeid=@StoreID) ", par, CommandType.Text);
            foreach (DataRow dr in dt.Rows)
            {
                memberInfo = new MemberInfoModel();
                memberInfo.Address = dr["Address"].ToString();
                memberInfo.AdvPass = dr["AdvPass"].ToString();
                memberInfo.Answer = dr["Answer"].ToString();
                memberInfo.BankCode = dr["BankCode"].ToString();
                memberInfo.Bank.BankName = CommonDataDAL.GetBankName(dr["BankCode"].ToString());
                memberInfo.BankAddress = dr["BankAddress"].ToString();
                memberInfo.BankCity = CommonDataDAL.GetCPCCode(dr["BCPCCode"].ToString());
                memberInfo.BankBook = dr["BankBook"].ToString();
                memberInfo.BankCard = dr["BankCard"].ToString();
                memberInfo.BCPCCode = dr["BCPCCode"].ToString();
                memberInfo.Birthday = DateTime.Parse(dr["Birthday"].ToString());
                memberInfo.CPCCode = dr["CPCCode"].ToString();
                memberInfo.City = CommonDataDAL.GetCPCCode(dr["CPCCode"].ToString());
                memberInfo.ChangeInfo = dr["ChangeInfo"].ToString();
                memberInfo.District = Convert.ToInt32(dr["District"].ToString());
                memberInfo.EctOut = decimal.Parse(dr["Out"].ToString());
                memberInfo.Email = dr["Email"].ToString();
                memberInfo.Error = dr["Error"].ToString();
                memberInfo.ExpectNum = int.Parse(dr["ExpectNum"].ToString());
                memberInfo.FaxTele = dr["FaxTele"].ToString();
                memberInfo.Flag = int.Parse(dr["Flag"].ToString());
                memberInfo.HomeTele = dr["HomeTele"].ToString();
                memberInfo.ID = int.Parse(dr["ID"].ToString());
                memberInfo.IsActive = int.Parse(dr["IsActive"].ToString());
                memberInfo.IsBatch = int.Parse(dr["IsBatch"].ToString());
                memberInfo.Jackpot = decimal.Parse(dr["Jackpot"].ToString());
                memberInfo.Language = int.Parse(dr["Language"].ToString());
                memberInfo.LastLoginDate = DateTime.Parse(dr["LastLoginDate"].ToString());
                memberInfo.LevelInt = int.Parse(dr["LevelInt"].ToString());
                memberInfo.LoginPass = dr["LoginPass"].ToString();
                memberInfo.Memberships = decimal.Parse(dr["Membership"].ToString());
                memberInfo.MobileTele = dr["MobileTele"].ToString();
                memberInfo.Name = dr["Name"].ToString();
                memberInfo.Number = dr["Number"].ToString();
                memberInfo.OfficeTele = dr["OfficeTele"].ToString();
                memberInfo.OperateIp = dr["OperateIp"].ToString();
                memberInfo.OperaterNum = dr["OperaterNum"].ToString();
                memberInfo.OrderID = dr["OrderID"].ToString();
                memberInfo.PaperType = CommonDataDAL.GetPaperType(dr["PaperTypeCode"].ToString());
                memberInfo.PaperNumber = dr["PaperNumber"].ToString();
                memberInfo.PetName = dr["PetName"].ToString();
                memberInfo.PhotoPath = dr["PhotoPath"].ToString();
                memberInfo.Placement = dr["Placement"].ToString();
                memberInfo.PostalCode = dr["PostalCode"].ToString();
                memberInfo.Question = dr["Question"].ToString();
                memberInfo.Direct = dr["Direct"].ToString();
                memberInfo.RegisterDate = DateTime.Parse(dr["RegisterDate"].ToString());
                memberInfo.Release = int.Parse(dr["Release"].ToString());
                memberInfo.Remark = dr["Remark"].ToString();
                memberInfo.Sex = Convert.ToInt32(dr["Sex"]);
                memberInfo.StoreID = dr["StoreID"].ToString();
                memberInfo.VIPCard = int.Parse(dr["VIPCard"].ToString());
            }
            return memberInfo;
        }

        #endregion
        /// <summary>
        /// 店铺注册
        /// </summary>
        /// <param name="store"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool RegisterStoreInfo(StoreInfoModel store, string type)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@type",type),
                new SqlParameter("@name",store.Name),
                new SqlParameter("@photo",store.PhotoPath),
                new SqlParameter("@storeid",store.StoreID),
                new SqlParameter("@mumber",store.Number),
                new SqlParameter("@storename",store.StoreName),
               // new SqlParameter("@storecountry",store.StoreCountry),
               // new SqlParameter("@storepriovince",store.StoreCity),
                //new SqlParameter("@country",store.Country),
               // new SqlParameter("@province",store.Province),
               // new SqlParameter("@city",store.City),
                new SqlParameter("@storeaddress",store.StoreAddress),
                new SqlParameter("@postalcode",store.PostalCode),
                new SqlParameter("@hometel",store.HomeTele),
                new SqlParameter("@officetel",store.OfficeTele),
                new SqlParameter("@mobiletel",store.MobileTele),
                new SqlParameter("@faxtel",store.FaxTele),
                new SqlParameter("@bank",store.BankCode),
                new SqlParameter("@bankcard",store.BankCard),
                new SqlParameter("@email",store.Email),
                 new SqlParameter("@netAddress",store.NetAddress),
                 new SqlParameter("@remark",store.Remark),
                 //new SqlParameter("@storelevelstr",store.StoreLevelStr),
                 new SqlParameter("@storelevelint",store.StoreLevelInt),
                 new SqlParameter("@fareArea",store.FareArea),
                 new SqlParameter("@Direct",store.Direct),
                 new SqlParameter("@ExpectNum",store.ExpectNum),
                 new SqlParameter("@LoginPass",store.LoginPass),
                 new SqlParameter("@OperateIP",store.OperateIp),
                 new SqlParameter("@TotalAccountMoney",store.TotalInvestMoney),
                 new SqlParameter("@Language",store.Language),
                 new SqlParameter("@OperateNum",store.OperateNum),
                 new SqlParameter("@Currency",store.Currency),
                 new SqlParameter("@Cp",store.CPCCode),
                 new SqlParameter("@Scp",store.SCPPCode),
                 //new SqlParameter("@AccreditExpectNum",store.AccreditExpectNum)
            };
            if (DBHelper.ExecuteNonQuery("RegisterStoreInfo", param, CommandType.StoredProcedure) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string GetPassword(string Number, string storeid)
        {
            string pwd = "";
            String sqlStr = "select paperNumber from MemberInfo where Number=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = Number;
            object card1 = DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text);
            string card = Encryption.Encryption.GetDecipherNumber(card1.ToString());
            if (card.ToString().Length < 6)
            {
                pwd = Encryption.Encryption.GetEncryptionPwd(Number.ToString(), storeid);
            }
            else
            {
                pwd = Encryption.Encryption.GetEncryptionPwd(card.ToString().Substring((card.ToString().Length) - 6, 6), storeid);
            }
            return pwd;
        }
        /// <summary>
        /// 为店铺子系统的订单支付功能页面显示当前货币，当前报单订货余额，当前周转款订货余额
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public static string[] GetStoreTotalOrderGoodsMoney(string storeId)
        {
            string[] storeInfos = new string[3];
            string sql = "select S.TotalOrderGoodMoney,S.TurnOverMoney,S.Currency,C.[Name] from StoreInfo as S"
                + "inner join Currency as C on (C.ID=S.Currency)"
                + "where StoreID=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = storeId;
            SqlDataReader dr = DBHelper.ExecuteReader(sql, sPara, CommandType.Text);
            if (dr.Read())
            {
                storeInfos[0] = dr.GetDecimal(0).ToString();
                storeInfos[1] = dr.GetDecimal(1).ToString();
                storeInfos[2] = dr.GetString(2);
            }
            dr.Close();
            return storeInfos;
        }

        /// <summary>
        /// 得到单铺的邮政编码，店名，店长名，店地址，电话
        /// </summary>
        public static StoreInfoModel GetStorInfoByStoreid(string storeid)
        {
            StoreInfoModel storeInfo = new StoreInfoModel();
            string sql = "select StoreInfo.ID , PostalCode,StoreName ,Name , StoreAddress, HomeTele,OfficeTele,MobileTele,FaxTele,city.Country,city.Province,city.City,StoreInfo.CPCCode  FROM StoreInfo,city  WHERE city.cpccode=storeinfo.cpccode and StoreID = @StoreID";
            SqlParameter[] para ={
									 new SqlParameter ("@StoreID" ,SqlDbType.VarChar ,15 )
								 };
            para[0].Value = storeid;

            SqlDataReader dataReader = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            if (dataReader.Read())
            {
                storeInfo.ID = Convert.ToInt32(dataReader["id"]);
                storeInfo.PostalCode = Convert.ToString(dataReader["PostalCode"]);
                storeInfo.StoreName = Convert.ToString(dataReader["StoreName"]);
                storeInfo.Name = Convert.ToString(dataReader["Name"]);
                storeInfo.StoreAddress = Convert.ToString(dataReader["StoreAddress"]);
                storeInfo.HomeTele = Convert.ToString(dataReader["HomeTele"]);
                storeInfo.OfficeTele = Convert.ToString(dataReader["OfficeTele"]);
                storeInfo.MobileTele = Convert.ToString(dataReader["MobileTele"]);
                storeInfo.FaxTele = Convert.ToString(dataReader["FaxTele"]);
                storeInfo.City.Country = Convert.ToString(dataReader["Country"]);
                storeInfo.City.Province = Convert.ToString(dataReader["Province"]);
                storeInfo.City.City = Convert.ToString(dataReader["City"]);
                storeInfo.CPCCode = dataReader["CPCCode"].ToString();
            }
            dataReader.Close();
            return storeInfo;
        }

        /// <summary>
        /// 根据店铺编号查询店铺汇率
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns>店铺汇率</returns>
        public static double GetStoreCurrencyByStoreId(string storeid)
        {
            string sql = "select cc.rate from storeinfo s,Country c,Currency cc where substring(s.scpccode,1,2)=c.countrycode and c.rateid=cc.id and storeid=@storeid ";
            return Convert.ToDouble(DBHelper.ExecuteScalar(sql, new SqlParameter[] { new SqlParameter("@storeid", storeid) }, CommandType.Text));
        }

        /// <summary>
        /// 根据店铺编号获取店铺名称
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <returns>店铺名称</returns>
        public static string GetStoreName(string storeId)
        {
            String sqlStr = "select StoreName from storeInfo where storeId=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = storeId;
            return DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text).ToString();
        }

        /// <summary>
        /// 获取可订单额
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns>可报单额</returns>
        public static string GetOrderMoney(string storeid)
        {
            String sqlStr = "select sum(TotalAccountMoney-TotalOrderGoodMoney) from storeinfo where storeid=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = storeid;
            return DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text).ToString();
        }
        public static int getMemberCount(string storeid)
        {
            int i = 0;

            String sqlStr = "select count(*) from storeinfo where StoreID=@num";

            SqlParameter[] para = {
                                      new SqlParameter("@num",SqlDbType.NVarChar,50)
                                  };
            para[0].Value = storeid;

            //i = (int)DBHelper.ExecuteScalar(sqlStr, para, CommandType.Text);

            sqlStr = "select count(*) from dbo.Remittances where RemitNumber='"+ storeid+"'";
            i += (int)DBHelper.ExecuteScalar(sqlStr, para, CommandType.Text);

            sqlStr = "select count(*) from dbo.StoreOrder where storeid='" + storeid + "'";
            i += (int)DBHelper.ExecuteScalar(sqlStr, para, CommandType.Text);
            return i;
        }
        public static int check(string Member, string oldPass, int passtype)
        {
            int i = 0;
            if (passtype == 1)
            {
                String sqlStr1 = "select count(*) from MemberInfo where Number=@num and Advpass=@num1";
                SqlParameter[] sPara = new SqlParameter[] { new SqlParameter("@num", SqlDbType.NVarChar,50) ,
                new SqlParameter("@num1", SqlDbType.NVarChar,32) ,
            };
                sPara[0].Value = Member;
                sPara[1].Value = oldPass;
                i = (int)DBHelper.ExecuteScalar(sqlStr1, sPara, CommandType.Text);
            }
            else
            {
                String sqlStr1 = "select count(*) from MemberInfo where Number=@num and LoginPass=@num1";
                SqlParameter[] sPara = new SqlParameter[] { new SqlParameter("@num", SqlDbType.NVarChar,50) ,
                new SqlParameter("@num1", SqlDbType.NVarChar,32) ,
            };
                sPara[0].Value = Member;
                sPara[1].Value = oldPass;
                i = (int)DBHelper.ExecuteScalar(sqlStr1, sPara, CommandType.Text);
            }
            return i;
        }

        public static int checkBranch(string Branch, string oldPass, int passtype)
        {
            int i = 0;
            if (passtype == 1)
            {
                String sqlStr1 = "select count(*) from branchmanage where Number=@num and Advpass=@num1";
                SqlParameter[] sPara = new SqlParameter[] { new SqlParameter("@num", SqlDbType.NVarChar,50) ,
                new SqlParameter("@num1", SqlDbType.NVarChar,100) ,
            };
                sPara[0].Value = Branch;
                sPara[1].Value = oldPass;
                i = (int)DBHelper.ExecuteScalar(sqlStr1, sPara, CommandType.Text);
            }
            else
            {
                String sqlStr1 = "select count(*) from branchmanage where Number=@num and LoginPass=@num1";
                SqlParameter[] sPara = new SqlParameter[] { new SqlParameter("@num", SqlDbType.NVarChar,50) ,
                new SqlParameter("@num1", SqlDbType.VarChar,100) ,
            };
                sPara[0].Value = Branch;
                sPara[1].Value = oldPass;
                i = (int)DBHelper.ExecuteScalar(sqlStr1, sPara, CommandType.Text);
            }
            return i;
        }

        public static int checkstore(string storeid, string oldPass, int pstype)
        {
            int i = 0;
            String sqlStr1 = "";
            if (pstype == 0)
                sqlStr1 = "select count(*) from StoreInfo where StoreID=@num and LoginPass=@num1";
            else if (pstype == 1)
                sqlStr1 = "select count(*) from StoreInfo where StoreID=@num and advpass=@num1";
            SqlParameter[] sPara = new SqlParameter[] { new SqlParameter("@num", SqlDbType.NVarChar,50) ,
                new SqlParameter("@num1", SqlDbType.VarChar,100) ,
            };
            sPara[0].Value = storeid;
            sPara[1].Value = oldPass;
            i = (int)DBHelper.ExecuteScalar(sqlStr1, sPara, CommandType.Text);
            return i;
        }

        /// <summary>
        /// 验证服务机构二级密码——ds2012——tianfeng
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="advpass"></param>
        /// <returns></returns>
        public static int checkstoreadvpass(string storeid, string advpass)
        {
            int i = 0;
            int logincount = 0;
            SqlParameter[] par = new SqlParameter[]{
                new SqlParameter("@storeid",storeid)
            };
            string logincount_sql = "select isnull(advcount,0),isnull(advtime,getutcdate()) from storeinfo where storeid=@storeid";
            DataTable dt = DAL.DBHelper.ExecuteDataTable(logincount_sql, par, CommandType.Text);
            logincount = Convert.ToInt32(dt.Rows[0][0]);
            DateTime nowtime = Convert.ToDateTime(DAL.DBHelper.ExecuteScalar("select getutcdate()"));
            DateTime dtime = Convert.ToDateTime(dt.Rows[0][1]);
            TimeSpan ts = dtime.AddHours(2) - nowtime;
            if (ts.Seconds <= 0)
            {
                string update_member = "update storeinfo set advcount=0,advtime=getutcdate() where storeid='" + storeid + "'";
                DAL.DBHelper.ExecuteNonQuery(update_member);
            }
            if (logincount >= 5 && ts.Seconds > 0)
            {
                //msg = "<script language='javascript'>alert('" + GetTran("000000", "对不起，您连续5次输入密码错，请2小时候在登录！") + "');</script>";
                return 2;
            }

            String sqlStr1 = "select count(*) from StoreInfo where StoreID=@num and advpass=@num1";
            SqlParameter[] sPara = new SqlParameter[] { new SqlParameter("@num", SqlDbType.NVarChar,50) ,
                new SqlParameter("@num1", SqlDbType.NVarChar,100) ,
            };
            sPara[0].Value = storeid;
            sPara[1].Value = advpass;
            int num = (int)DBHelper.ExecuteScalar(sqlStr1, sPara, CommandType.Text);
            if (num > 0)
            {
                SqlParameter[] par1 = new SqlParameter[]{
                    new SqlParameter("@number",storeid)
                };
                string update = "update storeinfo set advcount=0,advtime=getutcdate() where storeid=@number";
                DBHelper.ExecuteNonQuery(update, par1, CommandType.Text);
                return 0;
            }
            else
            {
                string up_lost = "update storeinfo set advcount=advcount+1,advtime=getutcdate() where storeid='" + storeid + "'";
                DBHelper.ExecuteNonQuery(up_lost);
                return 1;
            }
        }
        public static int checkPwdQuestion(string Number)
        {
            int i = 0;
            String sqlStr1 = "select count(*) from MemberInfo where Number=@num and Question <> '' and answer <> ''";
            SqlParameter sPara1 = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara1.Value = Number;
            i = (int)DBHelper.ExecuteScalar(sqlStr1, sPara1, CommandType.Text);
            return i;
        }

        /// <summary>
        /// 获取订货款，周转款，货币类型
        /// </summary>
        /// <param name="storeId">店编号</param>
        /// <returns>订货款，周转款，货币类型</returns>
        public static string[] GetSomeMoney(string storeId)
        {
            String sqlStr = "select sum(isnull(TotalAccountMoney,0)-isnull(TotalOrderGoodMoney,0)),sum(isnull(TurnOverMoney,0)-isnull(TurnOverGoodsMoney,0)),sum(isnull(Currency,0)) from StoreInfo where storeid=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = storeId;

            string[] money = new string[3];
            SqlDataReader reader = DBHelper.ExecuteReader(sqlStr, sPara, CommandType.Text);
            if (reader.Read())
            {
                money[0] = reader.GetDecimal(0).ToString("0.00");
                money[1] = reader.GetDecimal(1).ToString("0.00");
                String sqlStr1 = "select Name from Currency where id=@id";
                SqlParameter[] para = {
                                          new SqlParameter("@id",SqlDbType.Int)
                                      };
                para[0].Value = Convert.ToInt32(reader.GetValue(2));
                money[2] = DBHelper.ExecuteScalar(sqlStr1, para, CommandType.Text).ToString();
            }
            reader.Close();
            return money;

        }

        /// <summary>
        /// 订单支付，修改订单钱数
        /// </summary>
        /// <param name="tr">事务</param>
        /// <param name="ordermoney">订货款</param>
        /// <param name="turnmoney">周转款</param>
        /// <param name="storeid">店铺编号</param>
        /// <returns>是否修改成功</returns>
        public static bool UpdateSomeMoney(SqlTransaction tr, decimal ordermoney, decimal turnmoney, string storeid)
        {
            string sql = "update StoreInfo set TotalOrderGoodMoney=TotalOrderGoodMoney+@ordermoney,TurnOverGoodsMoney=TurnOverGoodsMoney+@turnmoney where storeId=@storeid";
            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@ordermoney", ordermoney);
            paras[1] = new SqlParameter("@turnmoney", turnmoney);
            paras[2] = new SqlParameter("@storeid", storeid);
            return DBHelper.ExecuteNonQuery(tr, sql, paras, CommandType.Text) > 0;

        }

        public static string getQuestion(string Number)
        {
            string question = "";
            String sqlStr = "Select question from MemberInfo where Number=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = Number;
            question = DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text).ToString().ToString();
            return question;
        }
        public static string getAnswer(string Number)
        {
            string Answer = "";
            String sqlStr = "Select Answer from MemberInfo where Number=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = Number;

            Answer = DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text).ToString().ToString();
            return Answer;
        }
        public static int updPwdQuestion(string Number, string question, string answer)
        {
            int i = 0;
            string sql = "update MemberInfo set question = @question , answer = @answer where Number=@num";
            SqlParameter[] parm = {new SqlParameter("@question",SqlDbType.NVarChar,50),
				                   new SqlParameter("@answer",SqlDbType.NVarChar,50) ,
                                   new SqlParameter("@num",SqlDbType.NVarChar,50)
								  };

            parm[0].Value = question;
            parm[1].Value = answer;
            parm[2].Value = Number;
            i = DBHelper.ExecuteNonQuery(sql, parm, CommandType.Text);
            return i;
        }

        public static int valiStore(string storeid)
        {
            String sqlStr = "SELECT COUNT(*) FROM StoreInfo WHERE StoreID=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = storeid;

            return Convert.ToInt32(DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text));
        }
        /// <summary>
        /// ---DS2012
        /// </summary>
        /// <returns></returns>
        public static DataTable bindCountry()
        {
            DataTable dt = DBHelper.ExecuteDataTable("Select id,name,countrycode From country order by id");
            return dt;
        }
        public static SqlDataReader bindCity(string country)
        {
            String sqlStr = "SELECT DISTINCT Province,substring(cpccode,1,4) FROM City where country=@num order by substring(cpccode,1,4)";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 40);
            sPara.Value = country;
            SqlDataReader dr = DBHelper.ExecuteReader(sqlStr, sPara, CommandType.Text);

            return dr;
        }
        /// <summary>
        /// 添加自由注册的店铺
        /// </summary>
        /// <param name="store">店铺信息</param>
        /// <returns></returns>
        public static bool AllerRegisterStoreInfo(UnauditedStoreInfoModel ustore)
        {
            SqlParameter[] parm = { new SqlParameter("@Number", SqlDbType.VarChar, 10), 
											  new SqlParameter("@StoreID", SqlDbType.VarChar, 10),
											  new SqlParameter("@Name", SqlDbType.VarChar, 50),
											  new SqlParameter("@StoreName", SqlDbType.NVarChar, 500),
                                              //new SqlParameter("@Country", SqlDbType.VarChar, 20),
                                              //new SqlParameter("@Province", SqlDbType.VarChar, 20),
                                              //new SqlParameter("@City", SqlDbType.VarChar, 20),
                                              new SqlParameter("@CPCCode", SqlDbType.VarChar, 50),
											  new SqlParameter("@StoreAddress", SqlDbType.Text ),
											  new SqlParameter("@HomeTele", SqlDbType.NVarChar, 500),
											  new SqlParameter("@OfficeTele", SqlDbType.NVarChar, 500),
											  new SqlParameter("@MobileTele", SqlDbType.NVarChar, 500),
											  new SqlParameter("@FaxTele", SqlDbType.NVarChar, 500),
											  new SqlParameter("@BankCode", SqlDbType.VarChar, 80),
											  new SqlParameter("@BankCard", SqlDbType.NVarChar, 500),
											  new SqlParameter("@Email", SqlDbType.VarChar, 50),
											  new SqlParameter("@NetAddress", SqlDbType.VarChar, 50),
											  new SqlParameter("@Remark", SqlDbType.Text ),
											  new SqlParameter("@Direct", SqlDbType.VarChar, 10),
											  new SqlParameter("@ExpectNum", SqlDbType.Int),
											  new SqlParameter("@RegisterDate", SqlDbType.DateTime),
											  new SqlParameter("@LoginPass", SqlDbType.VarChar ,50),
											  new SqlParameter("@AdvPass", SqlDbType.VarChar ,50),
											  new SqlParameter("@StoreLevelStr", SqlDbType.VarChar ,10),///////
											  new SqlParameter("@FareArea", SqlDbType.Money ),
											  new SqlParameter("@FareBreed", SqlDbType.Text ),
											  new SqlParameter("@TotalAccountMoney", SqlDbType.Money ),
											  new SqlParameter("@PostalCode", SqlDbType.VarChar ,10 ),
											  new SqlParameter("@AccreditExpectNum",SqlDbType.Int),
											  new SqlParameter("@PermissionMan",SqlDbType.VarChar),
											  new SqlParameter("@Language",SqlDbType.Int),
											  new SqlParameter("@Currency",SqlDbType.Int),
											  new SqlParameter("@StoreLevelInt",SqlDbType.Int),
											  new SqlParameter("@StoreCity",SqlDbType.VarChar,100),
											  new SqlParameter("@StoreCountry",SqlDbType.VarChar,100),
											  new SqlParameter("@OperateIP",SqlDbType.VarChar,30),
											  new SqlParameter("@OperateNum",SqlDbType.VarChar,30),
											  new SqlParameter("@photopath",SqlDbType.VarChar,50),
                                              new SqlParameter("@SPCCode",SqlDbType.VarChar,20)
										  };
            parm[0].Value = ustore.Number;
            parm[1].Value = ustore.StoreId;
            parm[2].Value = ustore.Name;
            parm[3].Value = ustore.StoreName;
            //parm[4].Value = ustore.Country;
            //parm[5].Value = ustore.Province;
            //parm[6].Value = ustore.City;
            parm[4].Value = ustore.SCPCCode;
            parm[5].Value = ustore.StoreAddress;
            parm[6].Value = ustore.HomeTele;
            parm[7].Value = ustore.OfficeTele;
            parm[8].Value = ustore.MobileTele;
            parm[9].Value = ustore.FaxTele;
            parm[10].Value = ustore.BankCode;
            parm[11].Value = ustore.BankCard;
            parm[12].Value = ustore.Email;
            parm[13].Value = ustore.NetAddress;
            parm[14].Value = ustore.Remark;
            parm[15].Value = ustore.Direct;
            parm[16].Value = ustore.ExpectNum;
            parm[17].Value = ustore.RegisterDate;
            parm[18].Value = ustore.LoginPass;
            parm[19].Value = ustore.AdvPass;
            parm[20].Value = ustore.StoreLevelStr;
            parm[21].Value = ustore.FareArea;
            parm[22].Value = ustore.FareBreed;
            parm[23].Value = ustore.TotalinvestMoney;
            parm[24].Value = ustore.PostalCode;
            parm[25].Value = ustore.AccreditExpectNum;
            parm[26].Value = ustore.PermissionMan;
            parm[27].Value = ustore.Language;
            parm[28].Value = ustore.Currency;
            parm[29].Value = ustore.StoreLevelInt;
            parm[30].Value = ustore.StoreCity;
            parm[31].Value = ustore.StoreCountry;
            parm[32].Value = ustore.OperateIp;
            parm[33].Value = ustore.OperateNum;
            parm[34].Value = ustore.PhotoPath;
            parm[35].Value = ustore.CPCCode;
            if (DBHelper.ExecuteNonQuery("AddUnauditedStoreInfo", parm, CommandType.StoredProcedure) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 会员编号是否存在
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns></returns>
        public static int IsMemberNum(string number)
        {
            String sqlStr = "SELECT COUNT(*) FROM MemberInfo WHERE Number=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = number;

            return (int)DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text);
        }

        /// <summary>
        /// 获取店铺最后登录时间
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns></returns>
        public static DateTime GetLastLoginTime(string storeid)
        {
            String sqlStr = "select lastlogindate from storeinfo where storeid=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = storeid;
            string tele = DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text).ToString();
            return DateTime.Parse(tele);
        }

        public static string getMemberMTele(string member)
        {
            String sqlStr = "select mobiletele from MemberInfo where Number=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = member;
            string tele = DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text).ToString();

            return Encryption.Encryption.GetDecipherTele(tele);
        }

        public static string getMemberFTele(string member)
        {
            String sqlStr = "select faxtele from MemberInfo where Number=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = member;
            string tele = DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text).ToString();

            return Encryption.Encryption.GetDecipherTele(tele);
        }

        public static string getMemberHTele(string member)
        {
            String sqlStr = "select hometele from MemberInfo where Number=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = member;
            string tele = DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text).ToString();

            return Encryption.Encryption.GetDecipherTele(tele);
        }

        public static string getMemberOTele(string member)
        {
            String sqlStr = "select officetele from MemberInfo where Number=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = member;
            string tele = DBHelper.ExecuteScalar(sqlStr, sPara, CommandType.Text).ToString();

            return Encryption.Encryption.GetDecipherTele(tele);
        }

        /// <summary>
        /// Judage the store whether exists beafore search the information about store by storeId
        /// </summary>
        /// <param name="storeId">StoreId</param>
        /// <returns>Return the counts about store by storeId</returns>
        public static int StoreIdIsExistByStoreId(string storeId)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@storeId",SqlDbType.NVarChar,50)
            };
            sparams[0].Value = storeId;
            return Convert.ToInt32(DBHelper.ExecuteScalar("StoreIdIsExistByStoreId", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// 获取店铺账户余额
        /// </summary>
        /// <param name="state">账户类型</param>
        /// <param name="storeid">店铺编号</param>
        /// <returns></returns>
        public static decimal GetStoreMoney(string state, string storeid)
        {
            string moneyTypeS = "";     //充值
            string moenyTypeX = "";     //消费
            if (state == "0")
            {
                moneyTypeS = "TotalAccountMoney";
                moenyTypeX = "TotalOrderGoodMoney";

            }
            else if (state == "1")
            {
                moneyTypeS = "TurnOverMoney";
                moenyTypeX = "TurnOverGoodsMoney";
            }
            else
            {
                return Convert.ToDecimal(0.00);
            }
            string strSql = "select isnull(sum(isnull(" + moneyTypeS + ",0)-isnull(" + moenyTypeX + ",0)),0) from storeinfo where storeid=@storeid";
            SqlParameter parm = new SqlParameter("@storeid", storeid);

            return Convert.ToDecimal(DBHelper.ExecuteScalar(strSql, parm, CommandType.Text));

        }

        /// <summary>
        /// 查询店铺导出信息
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataTable GetStoreQueryToExcel(string condition)
        {
            SqlParameter[] param = { new SqlParameter("@condition", SqlDbType.NVarChar) };
            param[0].Value = condition;
            return DBHelper.ExecuteDataTable("StoreQueryToExcel", param, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 获取店铺所属国家
        /// </summary>
        /// <param name="CPCCode">国家代号</param>
        /// <returns></returns>
        public static DataTable GetStoreCountry(string CPCCode)
        {
            SqlParameter[] param = { new SqlParameter("@cpccode", SqlDbType.NVarChar) };
            param[0].Value = CPCCode;
            return DBHelper.ExecuteDataTable("getStoreCountry", param, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取店铺级别名称
        /// </summary>
        /// <param name="LvNum"></param>
        /// <returns></returns>
        public static string GetStoreLvCH(int LvNum)
        {
            string sql = "select levelstr from bsco_level where levelint=@levelint and levelflag=1";

            SqlParameter parm = new SqlParameter("@levelint", LvNum);

            object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            return obj == null ? "" : obj.ToString();
        }


        /// <summary>
        /// 检测会员是否已经申请或注册为服务机构
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <returns></returns>
        public static int CheckStoreNumber(string number)
        {
            int res = 0;
            string sql = "select count(0) as count1 from UnauditedStoreInfo where number=@number";
            SqlParameter[] sp = { new SqlParameter("@number", number) };
            DataTable dt = DBHelper.ExecuteDataTable(sql, sp, CommandType.Text);
            if (dt.Rows.Count > 0 && int.Parse(dt.Rows[0]["count1"].ToString()) > 0)
            {
                res = -1;// "该会员已经申请服务机构,不可重复申请";
            }
            else
            {
                string sql1 = "  select count(0) as count2 from storeinfo where number=@number";
                SqlParameter[] sp1 = { new SqlParameter("@number", number) };
                DataTable dt1 = DBHelper.ExecuteDataTable(sql1, sp1, CommandType.Text);
                if (dt1.Rows.Count > 0 && int.Parse(dt1.Rows[0]["count2"].ToString()) > 0)
                {
                    res = -2;// "该会员已经是服务机构，不可重复申请";
                }
            }
            return res;

        }
    }
}