using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Model;
using Encryption;

/*
 * 创建者：     刘文
 * 创建时间：   2009-08-30
 * 文件名：     MemberInfoDAL
 * 功能：       对会员信息表进行增删改查操作
 */

namespace DAL
{
    public class MemberInfoDAL
    {
        /// <summary>
        /// 根据会员编号获取会员的图片
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public object GetMemberPhoto(string number)
        {
            return DBHelper.ExecuteScalar("select PhotoPath from MemberInfo where Number=@number", new SqlParameter("@number", number), CommandType.Text);
        }

        public static DataTable GetMemberInfoByPlacement(string placement)
        {
            return DBHelper.ExecuteDataTable("select * from MemberInfo where placement=@pm order by District asc ", new SqlParameter[1] { new SqlParameter("@pm", placement) }, CommandType.Text);
        }

        /// <summary>
        /// 获取会员某条报单的剩余金额（totalmoney）和PV（totalpv）
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static DataTable GetOrderMoneyPVSumByOrderID(string orderid)
        {
            string sql = @"select isnull(t.totalmoney,0)-isnull(t.totalmoneyReturned,0) as totalmoney,
                isnull(t.totalpv,0)-isnull(t.totalpvreturned,0) as totalpv from memberorder t
                where t.defraystate=1 and t.orderid=@orderid";
            SqlParameter[] param = { new SqlParameter("@orderid", SqlDbType.NVarChar, 20) };
            param[0].Value = orderid;
            return DBHelper.ExecuteDataTable(sql, param, CommandType.Text);
        }
        /// <summary>
        ///  获取会员某条报单的剩余金额（totalmoney）和PV（totalpv）【带事物】
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static DataTable GetOrderMoneyPVSumByOrderID(SqlTransaction tran, string orderid)
        {
            string sql = @"select isnull(t.totalmoney,0)-isnull(t.totalmoneyReturned,0) as totalmoney,
                isnull(t.totalpv,0)-isnull(t.totalpvreturned,0) as totalpv from memberorder t
                where t.defraystate=1 and t.orderid=@orderid";
            SqlParameter[] param = { new SqlParameter("@orderid", SqlDbType.NVarChar, 20) };
            param[0].Value = orderid;
            return DBHelper.ExecuteDataTable(tran, sql, param, CommandType.Text);
        }
        public bool updMemberInfo(string number, string petname, string homeTele, string faxTele, string officeTele, string mobileTele, string cpccode, string addr, string postalCode, string bankcode, string bankcpccode, string bankbrachname, string bankcard, string remark)
        {
            int res = 0;
            SqlParameter[] para = {
				                   new SqlParameter("@number",SqlDbType.VarChar,50),
				                   new SqlParameter("@petName",SqlDbType.VarChar,50),
				                   new SqlParameter("@homeTele",SqlDbType.VarChar,20),
				                   new SqlParameter("@faxTele",SqlDbType.VarChar,20),
				                   new SqlParameter("@officeTele",SqlDbType.VarChar,20),
				                   new SqlParameter("@mobileTele",SqlDbType.VarChar,20),
                                   new SqlParameter("@cpccode",SqlDbType.VarChar,20),
				                   new SqlParameter("@address",SqlDbType.VarChar,500),
				                   new SqlParameter("@postalCode",SqlDbType.VarChar,10),
                                   
                                   new SqlParameter("@bankcode",SqlDbType.VarChar,20),
                                   new SqlParameter("@bankcpccode",SqlDbType.VarChar,20),
                                   new SqlParameter("@bankbrachname",SqlDbType.VarChar,20),
                                   new SqlParameter("@bankcard",SqlDbType.VarChar,20),

                                   new SqlParameter("@remark",SqlDbType.VarChar,1000),

                                   new SqlParameter("@res",SqlDbType.Int,4)
								  };
            para[0].Value = number;
            para[1].Value = petname;
            para[2].Value = homeTele;
            para[3].Value = faxTele;
            para[4].Value = officeTele;
            para[5].Value = mobileTele;
            para[6].Value = cpccode;
            para[7].Value = addr;
            para[8].Value = postalCode;
            para[9].Value = bankcode;
            para[10].Value = bankcpccode;
            para[11].Value = bankbrachname;
            para[12].Value = bankcard;
            para[13].Value = remark;

            para[14].Value = res;
            para[14].Direction = ParameterDirection.Output;

            DBHelper.ExecuteNonQuery("App_updMemberInfo", para, CommandType.StoredProcedure);
            res = Convert.ToInt32(para[14].Value);

            if (res == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 在会员系统修改会员信息
        /// </summary>
        /// <param name="number"></param>
        /// <param name="name"></param>
        /// <param name="Petname"></param>
        /// <param name="Birthday"></param>
        /// <param name="sex"></param>
        /// <param name="HomeTele"></param>
        /// <param name="OfficeTele"></param>
        /// <param name="MobileTele"></param>
        /// <param name="FaxTele"></param>
        /// <param name="Country"></param>
        /// <param name="Province"></param>
        /// <param name="City"></param>
        /// <param name="Address"></param>
        /// <param name="PostalCode"></param>
        /// <param name="PaperType"></param>
        /// <param name="PaperNumber"></param>
        /// <param name="Remark"></param>
        /// <param name="Healthy"></param>
        /// <param name="photopath"></param>
        /// <param name="photoW"></param>
        /// <param name="photoH"></param>
        /// <returns></returns>
        public bool updateMember(string number, string name, string Petname, DateTime Birthday, int sex, string HomeTele, string OfficeTele, string MobileTele, string FaxTele, string Country, string Province, string City, string Address, string PostalCode, string PaperType, string PaperNumber, string Remark, string photopath, int photoW, int photoH, string cpccode, string bcpccode, string bankaddress, string bankcard, string bankcode, string bankbreachname)
        {
            SqlParameter[] para = {
                                   new SqlParameter("@Name",SqlDbType.VarChar,50),
				                   new SqlParameter("@PetName",SqlDbType.VarChar,50),
				                   new SqlParameter("@Birthday",SqlDbType.DateTime),
				                   new SqlParameter("@Sex",SqlDbType.Bit),
				                   new SqlParameter("@HomeTele",SqlDbType.VarChar,20),
				                   new SqlParameter("@OfficeTele",SqlDbType.VarChar,20),
				                   new SqlParameter("@MobileTele",SqlDbType.VarChar,20),
				                   new SqlParameter("@FaxTele",SqlDbType.VarChar,20),
                                   new SqlParameter("@Country",SqlDbType.VarChar,20),
                                   new SqlParameter("@Province",SqlDbType.VarChar,20),
                                   new SqlParameter("@City",SqlDbType.VarChar,20),
				                   new SqlParameter("@Address",SqlDbType.VarChar,500),
				                   new SqlParameter("@PostalCode",SqlDbType.VarChar,10),
				                   new SqlParameter("@PaperType",SqlDbType.VarChar,20),
				                   new SqlParameter("@PaperNumber",SqlDbType.VarChar,30),
                                   new SqlParameter("@Remark",SqlDbType.VarChar,1000),				                 
				                   new SqlParameter("@photopath",SqlDbType.VarChar,50),
                                   new SqlParameter("@photoW",SqlDbType.Int),
                                   new SqlParameter("@photoH",SqlDbType.Int),
                                   new SqlParameter("@number",SqlDbType.NVarChar),
                                   new SqlParameter("@CPCCode",SqlDbType.NVarChar,40),
                                   new SqlParameter("@bankcode",SqlDbType.NVarChar,20),
                                   new SqlParameter("@bankaddress",SqlDbType.NVarChar,50),
                                   new SqlParameter("@bankbreachname",SqlDbType.NVarChar,50),
                                   new SqlParameter("@bankcard",SqlDbType.NVarChar,50),
                                   new SqlParameter("@bcpccode",SqlDbType.NVarChar,20)
								  };
            para[0].Value = name;
            para[1].Value = Petname;
            para[2].Value = Birthday;
            para[3].Value = sex;
            para[4].Value = HomeTele;
            para[5].Value = OfficeTele;
            para[6].Value = MobileTele;
            para[7].Value = FaxTele;
            para[8].Value = Country;
            para[9].Value = Province;
            para[10].Value = City;
            para[11].Value = Address;
            para[12].Value = PostalCode;
            para[13].Value = PaperType;
            para[14].Value = PaperNumber;
            para[15].Value = Remark;
            para[16].Value = photopath;
            para[17].Value = photoW;
            para[18].Value = photoH;
            para[19].Value = number;
            para[20].Value = cpccode;
            para[21].Value = bankcode;
            para[22].Value = bankaddress;
            para[23].Value = bankbreachname;
            para[24].Value = bankcard;
            para[25].Value = bcpccode;
            if (DBHelper.ExecuteNonQuery("MenberInfoModify", para, CommandType.StoredProcedure) > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 在会员系统修改会员信息
        /// </summary>
        /// <param name="number"></param>
        /// <param name="name"></param>
        /// <param name="Petname"></param>
        /// <param name="Birthday"></param>
        /// <param name="sex"></param>
        /// <param name="HomeTele"></param>
        /// <param name="OfficeTele"></param>
        /// <param name="MobileTele"></param>
        /// <param name="FaxTele"></param>
        /// <param name="Country"></param>
        /// <param name="Province"></param>
        /// <param name="City"></param>
        /// <param name="Address"></param>
        /// <param name="PostalCode"></param>
        /// <param name="PaperType"></param>
        /// <param name="PaperNumber"></param>
        /// <param name="Remark"></param>
        /// <param name="Healthy"></param>
        /// <param name="photopath"></param>
        /// <param name="photoW"></param>
        /// <param name="photoH"></param>
        /// <returns></returns>
        public bool updateMember(string number, string name, string Petname, DateTime Birthday, int sex, string HomeQuhao, string HomeTele, string OfficeQuhao, string OfficeTele, string OfficeFjh, string MobileTele, string FaxQuhao, string FaxTele, string FaxFjh, string Country, string Province, string City, string Address, string PostalCode, string PaperType, string PaperNumber, string Remark, int Healthy, string photopath, int photoW, int photoH, string cpccode, int jjtx)
        {
            SqlParameter[] para = {
                                   new SqlParameter("@Name",SqlDbType.VarChar,50),
				                   new SqlParameter("@PetName",SqlDbType.VarChar,50),
				                   new SqlParameter("@Birthday",SqlDbType.DateTime),
				                   new SqlParameter("@Sex",SqlDbType.Bit),
                                   new SqlParameter("@HomeQuhao",SqlDbType.NVarChar,20),
				                   new SqlParameter("@HomeTele",SqlDbType.VarChar,20),
                                   new SqlParameter("@OfficeQuhao",SqlDbType.VarChar,20),
				                   new SqlParameter("@OfficeTele",SqlDbType.VarChar,20),
                                   new SqlParameter("@OfficeFjh",SqlDbType.VarChar,20),
				                   new SqlParameter("@MobileTele",SqlDbType.VarChar,20),
                                   new SqlParameter("@FaxQuhao",SqlDbType.VarChar,20),
				                   new SqlParameter("@FaxTele",SqlDbType.VarChar,20),
                                   new SqlParameter("@FaxFjh",SqlDbType.VarChar,20),
                                   new SqlParameter("@Country",SqlDbType.VarChar,20),
                                   new SqlParameter("@Province",SqlDbType.VarChar,20),
                                   new SqlParameter("@City",SqlDbType.VarChar,20),
				                   new SqlParameter("@Address",SqlDbType.VarChar,500),
				                   new SqlParameter("@PostalCode",SqlDbType.VarChar,10),
				                   new SqlParameter("@PaperType",SqlDbType.VarChar,20),
				                   new SqlParameter("@PaperNumber",SqlDbType.VarChar,30),
                                   new SqlParameter("@Remark",SqlDbType.VarChar,1000),				                 
				                   new SqlParameter("@Healthy",SqlDbType.Int),
				                   new SqlParameter("@photopath",SqlDbType.VarChar,50),
                                   new SqlParameter("@photoW",SqlDbType.Int),
                                   new SqlParameter("@photoH",SqlDbType.Int),
                                   new SqlParameter("@number",SqlDbType.NVarChar),
                                   new SqlParameter("@CPCCode",SqlDbType.NVarChar,40),
                                   new SqlParameter("@jjtx",SqlDbType.Int)
								  };
            para[0].Value = name;
            para[1].Value = Petname;
            para[2].Value = Birthday;
            para[3].Value = sex;
            para[4].Value = HomeQuhao;
            para[5].Value = HomeTele;
            para[6].Value = OfficeQuhao;
            para[7].Value = OfficeTele;
            para[8].Value = OfficeFjh;
            para[9].Value = MobileTele;
            para[10].Value = FaxQuhao;
            para[11].Value = FaxTele;
            para[12].Value = FaxFjh;
            para[13].Value = Country;
            para[14].Value = Province;
            para[15].Value = City;
            para[16].Value = Address;
            para[17].Value = PostalCode;
            para[18].Value = PaperType;
            para[19].Value = PaperNumber;
            para[20].Value = Remark;
            para[21].Value = Healthy;
            para[22].Value = photopath;
            para[23].Value = photoW;
            para[24].Value = photoH;
            para[25].Value = number;
            para[26].Value = cpccode;
            para[27].Value = jjtx;
            if (DBHelper.ExecuteNonQuery("MenberInfoModify2", para, CommandType.StoredProcedure) > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据会员编号查询会员的安置编号是否存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string GetMemberPlacement(string number)
        {
            return DBHelper.ExecuteScalar("getMemberPlacement", new SqlParameter("@number", number), CommandType.StoredProcedure).ToString();
        }

        /// <summary>
        /// 获取店铺联系方式
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectMemberPhone(string number)
        {
            return DBHelper.ExecuteDataTable("select HomeTele,OfficeTele,MobileTele,FaxTele,PostalCode,Address from storeinfo where number=@number", new SqlParameter[] { new SqlParameter("@number", number) }, CommandType.Text);
        }

        /// <summary>
        /// 获取会员联系方式
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMemberPhone(string number)
        {
            return DBHelper.ExecuteDataTable("select HomeTele,OfficeTele,MobileTele,FaxTele,PostalCode,Address from memberinfo where number=@number", new SqlParameter[] { new SqlParameter("@number", number) }, CommandType.Text);
        }

        /// <summary>
        /// 编号获取会员昵称
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetPetNameByNumber(string number)
        {
            return DBHelper.ExecuteScalar("select name from memberinfo where number=@num", new SqlParameter[1] { new SqlParameter("@num", number) }, CommandType.Text).ToString();
        }

        /// <summary>
        /// 编号获取会员激活状态
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetIsActiveByNumber(string number)
        {
            return DBHelper.ExecuteScalar("select isnull(isActive,0) from memberinfo where number=@num", new SqlParameter[1] { new SqlParameter("@num", number) }, CommandType.Text).ToString();
        }

        /// <summary>
        /// 获取会员总网业绩
        /// </summary>
        /// <param name="number"></param>
        /// <param name="qishu"></param>
        /// <returns></returns>
        public static double GetMemberNetYeJi(string number, int qishu)
        {
            return Convert.ToDouble(DBHelper.ExecuteScalar("select totalnetrecord from MemberInfoBalance" + qishu + " where number=@num", new SqlParameter[1] { new SqlParameter("@num", number) }, CommandType.Text));
        }

        /// <summary>
        /// 获取会员注册店铺
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <returns></returns>
        public static string GetStoreIdByNumber(string number)
        {
            return DBHelper.ExecuteScalar("select storeid from memberinfo where number=@num ", new SqlParameter[] { new SqlParameter("@num", number) }, CommandType.Text).ToString();
        }

        /// <summary>
        /// 会员（分页）
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="condition"></param>
        /// <param name="key"></param>
        /// <param name="RecordCount"></param>
        /// <param name="PageCount"></param>
        /// <returns></returns>
        public DataTable getMemberAll(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
            return CommonDataDAL.GetDataTablePage_Sms(PageIndex, PageSize, table, columns, condition, key, out RecordCount, out PageCount);
        }


        public static int checkmbphone(string number,string phone,string name) {
            string sqlstr = "select  count(0) from memberinfo where number='" + number + "' and MobileTele='"+ phone+"' and name='"+name+"' ";

            return Convert.ToInt32( DBHelper.ExecuteScalar(sqlstr));

        }



        /// <summary>
        /// 获得会员
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static MemberInfoModel getMemberInfo(string Number)
        {
            MemberInfoModel memberInfo = null;
            SqlParameter[] par = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.VarChar) };
            par[0].Value = Number;
            //DataTable dt = DBHelper.ExecuteDataTable("select top 1  c.Province as province , c.Country as country , c.City as city ,bc.Province as bprovince , bc.Country as bcountry , bc.City as bcity , mb.BankName as bankname , m.*  from MemberInfo m,city c,city bc,memberbank mb where bc.cpccode=m.bcpccode and Number=@Number and m.cpccode=c.cpccode and m.bankcode=mb.bankcode ", par, CommandType.Text);
            DataTable dt = DBHelper.ExecuteDataTable("select top 1  m.*  from MemberInfo m where Number=@Number ", par, CommandType.Text);
            foreach (DataRow dr in dt.Rows)
            {
                memberInfo = new MemberInfoModel();
                //memberInfo.Address = dr["Address"].ToString();
                memberInfo.AdvPass = dr["AdvPass"].ToString();
                //memberInfo.Answer = dr["Answer"].ToString();
                //memberInfo.BankCode = dr["BankCode"].ToString();
                //memberInfo.Bank.BankName = CommonDataDAL.GetBankName(dr["BankCode"].ToString());
                //memberInfo.Bankbranchname = dr["bankbranchname"].ToString();
                //memberInfo.BankAddress = dr["BankAddress"].ToString();
                //memberInfo.BankCity = CommonDataDAL.GetCPCCode(dr["BCPCCode"].ToString());
                //memberInfo.BankBook = dr["BankBook"].ToString();
                //memberInfo.BankCard = dr["BankCard"].ToString();
                //memberInfo.BCPCCode = dr["BCPCCode"].ToString();
               // memberInfo.Birthday = DateTime.Parse(dr["Birthday"].ToString());
               // memberInfo.CPCCode = dr["CPCCode"].ToString();
               // memberInfo.City = CommonDataDAL.GetCPCCode(dr["CPCCode"].ToString());
               // memberInfo.ChangeInfo = dr["ChangeInfo"].ToString();
               // memberInfo.District = Convert.ToInt32(dr["District"].ToString());
                memberInfo.EctOut = decimal.Parse(dr["Out"].ToString());
                memberInfo.Email = dr["Email"].ToString();
                memberInfo.Error = dr["Error"].ToString();
                memberInfo.ExpectNum = int.Parse(dr["ExpectNum"].ToString());
              //  memberInfo.FaxTele = dr["FaxTele"].ToString();
              //  memberInfo.Flag = int.Parse(dr["Flag"].ToString());
               // memberInfo.HomeTele = dr["HomeTele"].ToString();
                memberInfo.ID = int.Parse(dr["ID"].ToString());
                memberInfo.IsActive = int.Parse(dr["MemberState"].ToString());
              //  memberInfo.IsBatch = int.Parse(dr["IsBatch"].ToString());
                memberInfo.Jackpot = decimal.Parse(dr["Jackpot"].ToString());
                memberInfo.Language = int.Parse(dr["Language"].ToString());
             //   memberInfo.LastLoginDate = DateTime.Parse(dr["LastLoginDate"].ToString());
                memberInfo.LevelInt = int.Parse(dr["LevelInt"].ToString());
                memberInfo.LoginPass = dr["LoginPass"].ToString();
                memberInfo.Memberships = decimal.Parse(dr["Membership"].ToString());
                memberInfo.MobileTele = dr["MobileTele"].ToString();
                memberInfo.Name = dr["Name"].ToString(); 
                memberInfo.Number = dr["Number"].ToString();
               // memberInfo.OfficeTele = dr["OfficeTele"].ToString();
                memberInfo.OperateIp = dr["OperateIp"].ToString();
                memberInfo.OperaterNum = dr["OperaterNum"].ToString();
                memberInfo.OrderID = dr["OrderID"].ToString();
              //  memberInfo.PaperType = CommonDataDAL.GetPaperType(dr["PaperTypeCode"].ToString());
              //  memberInfo.PaperNumber = dr["PaperNumber"].ToString();
               // memberInfo.PetName = dr["PetName"].ToString();
               // memberInfo.PhotoPath = dr["PhotoPath"].ToString();
               // memberInfo.Placement = dr["Placement"].ToString();
                //memberInfo.PostalCode = dr["PostalCode"].ToString();
                //memberInfo.Question = dr["Question"].ToString();
                memberInfo.Direct = dr["Direct"].ToString();
                memberInfo.RegisterDate = DateTime.Parse(dr["RegisterDate"].ToString());
                memberInfo.Release = int.Parse(dr["Release"].ToString());
                memberInfo.Remark = dr["Remark"].ToString();
                memberInfo.Sex = Convert.ToInt32(dr["Sex"]);
               // memberInfo.StoreID = dr["StoreID"].ToString();
               // memberInfo.VIPCard = int.Parse(dr["VIPCard"].ToString());
                memberInfo.Zhifubao = dr["Zhifubao"].ToString();
                memberInfo.Weixin = dr["Weixin"].ToString();
                memberInfo.Zzye = decimal.Parse(dr["zzye"].ToString());
            }
            return memberInfo;
        }
        /// <summary>
        /// 会员编辑
        /// </summary>
        /// <param name="Number"></param>
        /// <param name="Placement"></param>
        /// <param name="Recommended"></param>
        /// <param name="Name"></param>
        /// <param name="PetName"></param>
        /// <param name="Birthday"></param>
        /// <param name="Sex"></param>
        /// <param name="PostalCode"></param>
        /// <param name="HomeTele"></param>
        /// <param name="OfficeTele"></param>
        /// <param name="MobileTele"></param>
        /// <param name="FaxTele"></param>
        /// <param name="Country"></param>
        /// <param name="Province"></param>
        /// <param name="City"></param>
        /// <param name="Address"></param>
        /// <param name="PaperType"></param>
        /// <param name="PaperNumber"></param>
        /// <param name="BankCountry"></param>
        /// <param name="BankProvince"></param>
        /// <param name="BankCity"></param>
        /// <param name="Bank"></param>
        /// <param name="BankCard"></param>
        /// <param name="BankBook"></param>
        /// <param name="ExpectNum"></param>
        /// <param name="Remark"></param>
        /// <param name="OrderId"></param>
        /// <param name="StoreId"></param>
        /// <param name="ChangeInfo"></param>
        /// <param name="OperateIp"></param>
        /// <param name="OperaterNum"></param>
        /// <returns></returns>
        public int updateMember(string Number, string Placement, string Direct, string Name, string PetName, DateTime Birthday, int Sex, string PostalCode, string HomeTele, string OfficeTele, string MobileTele, string FaxTele, string Country, string Province, string City, string Address, string PaperType, string PaperNumber, string BankCountry, string BankProvince, string BankCity, string Bank, string BankAddress, string BankCard, string BankBook, int ExpectNum, string Remark, string OrderId, string StoreId, string ChangeInfo, string OperateIp, string OperaterNum)
        {
            int i = 0;
            SqlParameter[] par = new SqlParameter[] 
            { 
                                      new SqlParameter("@Number", SqlDbType.VarChar,20),
									  new SqlParameter("@Placement", SqlDbType.VarChar,20),
									  new SqlParameter("@Direct",SqlDbType.VarChar,20),
									  new SqlParameter("@Name",SqlDbType.VarChar,20),
									  new SqlParameter("@PetName",SqlDbType.VarChar,20),

									  new SqlParameter("@Birthday",SqlDbType.DateTime),
									  new SqlParameter("@Sex",SqlDbType.Bit, 1),
									  new SqlParameter("@PostalCode",SqlDbType.VarChar,20),
									  new SqlParameter("@HomeTele",SqlDbType.VarChar,20),
									  new SqlParameter("@OfficeTele",SqlDbType.VarChar,20),

									  new SqlParameter("@MobileTele",SqlDbType.VarChar,20),
									  new SqlParameter("@FaxTele",SqlDbType.VarChar,20),
									  new SqlParameter("@CPccode",SqlDbType.VarChar, 20),

									  new SqlParameter("@Address",SqlDbType.Text),
									  new SqlParameter("@PaperTypeCode",SqlDbType.VarChar, 20),
									  new SqlParameter("@PaperNumber",SqlDbType.VarChar, 30),
									  new SqlParameter("@bcpccode",SqlDbType.VarChar, 20),

									  new SqlParameter("@Bank",SqlDbType.VarChar, 80),
                                      new SqlParameter("@BankAddress",SqlDbType.VarChar, 200),
									  new SqlParameter("@BankCard",SqlDbType.VarChar, 50),
									  new SqlParameter("@BankBook",SqlDbType.VarChar, 50),
                                      new SqlParameter("@ExpectNum",SqlDbType.Int),

									  new SqlParameter("@Remark",SqlDbType.Text ),
									  new SqlParameter("@OrderId",SqlDbType.VarChar,20),
									  new SqlParameter("@StoreId",SqlDbType.VarChar, 10),									 
									  new SqlParameter("@ChangeInfo",SqlDbType.Text ),                                  
				                      new SqlParameter("@OperateIp",SqlDbType.VarChar,20),

									  new SqlParameter("@OperaterNum",SqlDbType.VarChar,20)
            };
            par[0].Value = Number;
            par[1].Value = Placement;
            par[2].Value = Direct;
            par[3].Value = Name;
            par[4].Value = PetName;

            par[5].Value = Birthday;
            par[6].Value = Sex;
            par[7].Value = PostalCode;
            par[8].Value = HomeTele;
            par[9].Value = OfficeTele;

            par[10].Value = MobileTele;
            par[11].Value = FaxTele;
            par[12].Value = CommonDataDAL.GetCPCCode(Country, Province, City);

            par[13].Value = Address;
            par[14].Value = PaperType;
            par[15].Value = PaperNumber;
            par[16].Value = CommonDataDAL.GetCPCCode(BankCountry, BankProvince, BankCity);

            par[17].Value = Bank;
            par[18].Value = BankAddress;
            par[19].Value = BankCard;
            par[20].Value = BankBook;
            par[21].Value = ExpectNum;

            par[22].Value = Remark;
            par[23].Value = OrderId;
            par[24].Value = StoreId;
            par[25].Value = ChangeInfo;
            par[26].Value = OperateIp;

            par[27].Value = OperaterNum;

            i = DBHelper.ExecuteNonQuery("updMemberInfo", par, CommandType.StoredProcedure);
            return i;
        }
        /// <summary>
        /// 会员编辑
        /// </summary>
        /// <param name="Number"></param>
        /// <param name="Placement"></param>
        /// <param name="Recommended"></param>
        /// <param name="Name"></param>
        /// <param name="PetName"></param>
        /// <param name="Birthday"></param>
        /// <param name="Sex"></param>
        /// <param name="PostalCode"></param>
        /// <param name="HomeTele"></param>
        /// <param name="OfficeTele"></param>
        /// <param name="MobileTele"></param>
        /// <param name="FaxTele"></param>
        /// <param name="Country"></param>
        /// <param name="Province"></param>
        /// <param name="City"></param>
        /// <param name="Address"></param>
        /// <param name="PaperType"></param>
        /// <param name="PaperNumber"></param>
        /// <param name="BankCountry"></param>
        /// <param name="BankProvince"></param>
        /// <param name="BankCity"></param>
        /// <param name="Bank"></param>
        /// <param name="BankCard"></param>
        /// <param name="BankBook"></param>
        /// <param name="ExpectNum"></param>
        /// <param name="Remark"></param>
        /// <param name="OrderId"></param>
        /// <param name="StoreId"></param>
        /// <param name="ChangeInfo"></param>
        /// <param name="OperateIp"></param>
        /// <param name="OperaterNum"></param>
        /// <returns></returns>
        public int updateMember(MemberInfoModel info)
        {
            int i = 0;
            SqlParameter[] par = new SqlParameter[] 
            { 
                                      new SqlParameter("@Number", SqlDbType.NVarChar,50),
									  new SqlParameter("@Placement", SqlDbType.NVarChar,50),
									  new SqlParameter("@Direct",SqlDbType.NVarChar,50),
									  new SqlParameter("@Name",SqlDbType.NVarChar,500),
									  new SqlParameter("@PetName",SqlDbType.NVarChar,50),

									  new SqlParameter("@Birthday",SqlDbType.DateTime),
									  new SqlParameter("@Sex",SqlDbType.Bit, 1),
									  new SqlParameter("@PostalCode",SqlDbType.NVarChar,50),
									  new SqlParameter("@HomeTele",SqlDbType.NVarChar,500),

									  new SqlParameter("@OfficeTele",SqlDbType.NVarChar,500),

									  new SqlParameter("@MobileTele",SqlDbType.NVarChar,500),

									  new SqlParameter("@FaxTele",SqlDbType.NVarChar,500),

									  new SqlParameter("@CPccode",SqlDbType.NVarChar, 40),

									  new SqlParameter("@Address",SqlDbType.NVarChar,500),
									  new SqlParameter("@PaperTypeCode",SqlDbType.NVarChar, 30),
									  new SqlParameter("@PaperNumber",SqlDbType.NVarChar, 500),
									  new SqlParameter("@bcpccode",SqlDbType.NVarChar, 20),

                                      new SqlParameter("@BankAddress",SqlDbType.NVarChar, 500),
									  new SqlParameter("@BankCard",SqlDbType.NVarChar, 500),
									  new SqlParameter("@BankBook",SqlDbType.NVarChar, 500),
                                      new SqlParameter("@ExpectNum",SqlDbType.Int),

									  new SqlParameter("@Remark",SqlDbType.Text ),
									  new SqlParameter("@OrderId",SqlDbType.NVarChar,20),
									  new SqlParameter("@StoreId",SqlDbType.NVarChar, 10),									 
									  new SqlParameter("@ChangeInfo",SqlDbType.Text ),                                  
				                      new SqlParameter("@OperateIp",SqlDbType.NVarChar,20),

									  new SqlParameter("@OperaterNum",SqlDbType.NVarChar,20),
                                      new SqlParameter("@BankCode",SqlDbType.NVarChar,50),
                                      new SqlParameter("@Bankbranchname",SqlDbType.NVarChar,50),
                                      new SqlParameter("PhotoPath",SqlDbType.NVarChar,50),
            };
            par[0].Value = info.Number;
            par[1].Value = info.Placement;
            par[2].Value = info.Direct;
            par[3].Value = info.Name;
            par[4].Value = info.PetName;

            par[5].Value = info.Birthday;
            par[6].Value = info.Sex;
            par[7].Value = info.PostalCode;
            par[8].Value = info.HomeTele;

            par[9].Value = info.OfficeTele;

            par[10].Value = info.MobileTele;

            par[11].Value = info.FaxTele;

            par[12].Value = info.CPCCode;

            par[13].Value = info.Address;
            par[14].Value = info.Papertypecode;
            par[15].Value = info.PaperNumber;
            par[16].Value = info.BCPCCode;

            par[17].Value = info.BankAddress;
            par[18].Value = info.BankCard;
            par[19].Value = info.BankBook;
            par[20].Value = info.ExpectNum;

            par[21].Value = info.Remark;
            par[22].Value = info.OrderID;
            par[23].Value = info.StoreID;
            par[24].Value = info.ChangeInfo;
            par[25].Value = info.OperateIp;

            par[26].Value = info.OperaterNum;
            par[27].Value = info.BankCode;
            par[28].Value = info.Bankbranchname;
            par[29].Value = info.PhotoPath;

            i = DBHelper.ExecuteNonQuery("updMemberInfo", par, CommandType.StoredProcedure);
            return i;
        }
        /// <summary>
        /// 更改会员密码
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public int updateMemberPass(string Number, int type)
        {
            int i = 0;
            //SqlParameter[] par = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.VarChar) };
            //par[0].Value = Number;
            //i = DBHelper.ExecuteNonQuery("updMemberPass", par, CommandType.StoredProcedure);
            string sqlStr = "select paperNumber from MemberInfo where Number=@num";
            SqlParameter[] parm = new SqlParameter[] { 
              new SqlParameter("@num",SqlDbType.NVarChar,50)
            
            };
            parm[0].Value = Number;
            object card = DBHelper.ExecuteScalar(sqlStr, parm, CommandType.Text);
            if (card != null)
            {
                card = Encryption.Encryption.GetDecipherNumber(card.ToString());
            }
            else
            {
                card = "";
            }
            if (card.ToString().Length < 6)
            {
                string num = Encryption.Encryption.GetEncryptionPwd(Number, Number);
                //i = (int)DBHelper.ExecuteNonQuery("update MemberInfo set LoginPass='" + num + "', AdvPass='" + Number + "' where Number='" + Number + "'");
                SqlParameter[] parm1 = new SqlParameter[] { 
                    new SqlParameter("@num",SqlDbType.NVarChar,32),
                    new SqlParameter("@num1",SqlDbType.NVarChar,50)
            
                };
                parm1[0].Value = num;
                parm1[1].Value = Number;
                if (type == 0)
                {
                    String sqlStr1 = "update MemberInfo set LoginPass=@num where Number=@num1";
                    i = (int)DBHelper.ExecuteNonQuery(sqlStr1, parm1, CommandType.Text);
                }
                else
                {
                    String sqlStr2 = "update MemberInfo set AdvPass=@num where Number=@num1";
                    i = (int)DBHelper.ExecuteNonQuery(sqlStr2, parm1, CommandType.Text);

                }
            }
            else
            {
                string paperNum = Encryption.Encryption.GetEncryptionPwd(card.ToString().Substring((card.ToString().Length) - 6, 6), Number);
                //i = (int)DBHelper.ExecuteNonQuery("update MemberInfo set LoginPass='" + paperNum + "', AdvPass='" + Number + "' where Number='" + Number + "'");
                SqlParameter[] parm2 = new SqlParameter[] { 
                    new SqlParameter("@num",SqlDbType.NVarChar,32),
                    new SqlParameter("@num1",SqlDbType.NVarChar,50)
            
                };
                parm2[0].Value = paperNum;
                parm2[1].Value = Number;
                if (type == 0)
                {
                    String sqlStr3 = "update MemberInfo set LoginPass=@num where Number=@num1";
                    i = (int)DBHelper.ExecuteNonQuery(sqlStr3, parm2, CommandType.Text);
                }
                else
                {
                    String sqlStr4 = "update MemberInfo set AdvPass=@num where Number=@num1";
                    i = (int)DBHelper.ExecuteNonQuery(sqlStr4, parm2, CommandType.Text);
                }
            }

            return i;
        }


        //会员2级密码
        public static int updateMemberPass1(string storeid, string pass)
        {
            int i = 0;
            string sqlstr = "update memberinfo set LoginPass=@num where number=@num1";
            string num = Encryption.Encryption.GetEncryptionPwd(pass.ToString(), storeid);
            SqlParameter[] parm = new SqlParameter[] { 
                    new SqlParameter("@num",SqlDbType.NVarChar,32),
                    new SqlParameter("@num1",SqlDbType.NVarChar,50)
            
                };
            parm[0].Value = num;
            parm[1].Value = storeid;
            i = (int)DBHelper.ExecuteNonQuery(sqlstr, parm, CommandType.Text);
            return i;
        }
        //会员1级密码
        public static int updateMemberPass2(string storeid, string pass)
        {

            int i = 0;
            string sqlstr = "update memberinfo set advpass=@num where number=@num1";
            string num = Encryption.Encryption.GetEncryptionPwd(pass.ToString(), storeid);
            SqlParameter[] parm = new SqlParameter[] { 
                    new SqlParameter("@num",SqlDbType.NVarChar,32),
                    new SqlParameter("@num1",SqlDbType.NVarChar,50)
            
                };
            parm[0].Value = num;
            parm[1].Value = storeid;
            i = (int)DBHelper.ExecuteNonQuery(sqlstr, parm, CommandType.Text);

            return i;
        }
        /// <summary>
        /// 获取会员注册报单信息
        /// </summary>
        /// <param name="storeId">店编号</param>
        /// <param name="expectNum">期数</param>
        /// <param name="condition">根据字段</param>
        /// <param name="symbol">查询符号</param>
        /// <param name="character">条件</param>
        /// <returns></returns>
        public static DataTable QueryDeclaration(string storeId, string condition, string symbol, string character, int expectNum, int isAgin)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@storeId", SqlDbType.VarChar,50), 
                new SqlParameter("@condition", SqlDbType.VarChar,100),
                new SqlParameter("@symbol", SqlDbType.VarChar,10),
                new SqlParameter("@character", SqlDbType.VarChar,500),
                new SqlParameter("@expectNum", SqlDbType.Int),
               // new SqlParameter("@ordertype",SqlDbType.Int),
                //new SqlParameter("@defraystate",SqlDbType.Int),
                new SqlParameter("@isAgin",SqlDbType.Int),
                //new SqlParameter("@IsReceivables",SqlDbType.Int)
            };
            param[0].Value = storeId;
            param[1].Value = condition;
            param[2].Value = symbol;
            param[3].Value = character;
            param[4].Value = expectNum;
            // param[5].Value = orderType;
            // param[6].Value = defraystate;
            param[5].Value = isAgin;
            //param[8].Value = IsReceivables;
            dt = DBHelper.ExecuteDataTable("P_GetMemberInfo", param, CommandType.StoredProcedure);

            return dt;
        }
        /// <summary>
        /// 获取会员注册报单信息
        /// </summary>
        /// <param name="storeId">店编号</param>
        /// <param name="expectNum">期数</param>
        /// <param name="condition">根据字段</param>
        /// <param name="symbol">查询符号</param>
        /// <param name="character">条件</param>
        /// <returns></returns>
        public static DataTable QueryDeclaration(string condition, string symbol, string character, int expectNum, int orderType, int defraystate, int isAgin, int IsReceivables)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@condition", SqlDbType.VarChar,100),
                new SqlParameter("@symbol", SqlDbType.VarChar,10),
                new SqlParameter("@character", SqlDbType.VarChar,500),
                new SqlParameter("@expectNum", SqlDbType.Int),
                new SqlParameter("@ordertype",SqlDbType.Int),
                //new SqlParameter("@defraystate",SqlDbType.Int),
                //new SqlParameter("@isAgin",SqlDbType.Int),
                //new SqlParameter("@IsReceivables",SqlDbType.Int)
            };
            param[1].Value = condition;
            param[2].Value = symbol;
            param[3].Value = character;
            param[4].Value = expectNum;
            param[5].Value = orderType;
            //param[6].Value = defraystate;
            //param[7].Value = isAgin;
            //param[8].Value = IsReceivables;
            dt = DBHelper.ExecuteDataTable("P_GetMemberInfo", param, CommandType.StoredProcedure);

            return dt;
        }

        public static DataTable DeclarationProduct(string storeId, string orderId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[]{
                        new SqlParameter("@storeId", SqlDbType.VarChar,50), 
                        new SqlParameter("@orderId", SqlDbType.VarChar,50),
            };
            param[0].Value = storeId;
            param[1].Value = orderId;
            dt = DBHelper.ExecuteDataTable("P_GetMemberDetails", param, CommandType.StoredProcedure);
            return dt;
        }


        public static DataTable ProductView(string orderId)
        {
            DataTable dt = new DataTable();
            string sql = @"select case when o.defraytype=1 then '1'  when  o.defraytype = 2 then '2'  when  o.defraytype = 3 then '3'  when  o.defraytype = 4 then '4'  else '5'  end as defrayname,case when o.DefrayState = 0 then '0'  when  o.DefrayState = 1 then '1'  else '2'  end as PayStatus,o.ordertype as RegisterWay,h.number,h.name,o.ordertype,o.DefrayState,o.defraytype,o.orderExpectNum,o.PayExpectNum,o.OrderId,
                       o.totalMoney,o.totalPv,h.RegisterDate from memberinfo h,memberorder o where h.number=o.number and o.orderid=@num";
            SqlParameter[] parm = new SqlParameter[] { 
                    new SqlParameter("@num",SqlDbType.NVarChar,50),
                   
            
                };
            parm[0].Value = orderId;

            dt = DBHelper.ExecuteDataTable(sql, parm, CommandType.Text);

            return dt;
        }

        /// <summary>
        /// 获取指定时间内的会员信息
        /// </summary>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">截止时间</param>
        /// <returns>返回DataTable对象</returns>
        public static DataTable GetMemberInfoByDate(DateTime beginDate, DateTime endDate)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@beginDate",SqlDbType.DateTime),                
                new SqlParameter("@endDate",SqlDbType.DateTime)
            };
            sparams[0].Value = beginDate;
            sparams[1].Value = endDate;
            return DBHelper.ExecuteDataTable("GetMemberInfoByDate", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 获取注册最大时间
        /// </summary>
        /// <returns></returns>
        public static string GetMaxRegisterDate()
        {
            string sql = "select max(registerdate) from MemberInfo";
            object obj = null;
            obj = DBHelper.ExecuteScalar(sql, CommandType.Text);
            if (obj == null)
            {
                throw new Exception("数据缺失.");
            }
            else
            {
                return obj.ToString();
            }
        }

        /// <summary>
        /// 根据关系获取推荐上级
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <returns></returns>
        public static string GetDirect(string Number)
        {
            string sql = "select Direct from MemberInfo where number = @number";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@number", SqlDbType.VarChar, 20) };
            para[0].Value = Number;
            object obj = null;
            obj = DBHelper.ExecuteScalar(sql, para, CommandType.Text);
            return obj == null ? "" : obj.ToString();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Number">编号</param>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>
        public static string GetDirect(string Number, string ExpectNum)
        {
            string sql = "select Direct from MemberInfo m,MemberInfoBalance" + ExpectNum + " b  where m.number=b.number and number = @number";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@number", SqlDbType.VarChar, 20) };
            para[0].Value = Number;
            object obj = null;
            obj = DBHelper.ExecuteScalar(sql, para, CommandType.Text);
            return obj == null ? "" : obj.ToString();

        }

        /// <summary>
        /// 根据关系获取推荐上级
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <returns></returns>
        public static string GetPlacement(string Number)
        {
            string sql = "select placement from MemberInfo where number = @number";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@number", SqlDbType.VarChar, 20) };
            para[0].Value = Number;
            object obj = null;
            obj = DBHelper.ExecuteScalar(sql, para, CommandType.Text);
            return obj == null ? "" : obj.ToString();
        }


        public static int GetMemberQishu(string Number)
        {
            string sql = "select expectnum from memberinfo where number = @number";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@number", SqlDbType.VarChar, 20) };
            para[0].Value = Number;
            object obj = null;
            obj = DBHelper.ExecuteScalar(sql, para, CommandType.Text);
            return int.Parse(obj.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Number">编号</param>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>
        public static string GetPlacement(string Number, string ExpectNum)
        {
            string sql = "select placement from MemberInfo m,MemberInfoBalance" + ExpectNum + " b  where m.number=b.number and number = @number";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@number", SqlDbType.VarChar, 20) };
            para[0].Value = Number;
            object obj = null;
            obj = DBHelper.ExecuteScalar(sql, para, CommandType.Text);
            return obj == null ? "" : obj.ToString();
        }

        public static int GetMemberInfoCount(string leader)
        {
            string sql = "select count(1) from MemberInfo where number = @number";
            SqlParameter para = new SqlParameter("@number", leader);
            object obj = null;
            obj = DBHelper.ExecuteScalar(sql, para, CommandType.Text);
            return int.Parse(obj.ToString());
        }

        /// <summary>
        /// 获取指定编号会员的安置人数
        /// </summary>
        /// <param name="placement"></param>
        /// <returns></returns>
        public static int GetPlaceCount(string placement, string number)
        {
            string sql = "select Count(1) from memberInfo where placement = @placement and number <> @number";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@placement", placement), new SqlParameter("@number", number) };
            return (int)DBHelper.ExecuteScalar(sql, para, CommandType.Text);
        }

        /// <summary>
        /// 获取指定编号会员的推荐人数
        /// </summary>
        /// <param name="Direct"></param>
        /// <returns></returns>
        public static int GetDirectCount(string Direct)
        {
            string sql = "select Count(1) from memberInfo where Direct = @Direct";
            SqlParameter para = new SqlParameter("@Direct", Direct);
            return DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
        }

        /// <summary>
        /// 根据编号获取会员安置推荐关系人
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <returns></returns>
        public static DataTable GetMemberInfoDataTable(string number)
        {
            string sql = "select direct from memberInfo where number= @number";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@number", number) };
            return DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
        }

        /// <summary>
        /// 改变会员安置关系
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="number">编号</param>
        /// <param name="placement">新安置</param>
        public static int ChangePlacement(SqlTransaction tran, string number, string placement)
        {
            string sql = "update memberinfo set placement= @placement where number = @number";
            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@placement",placement),
                new SqlParameter("@number",number)
            };
            return DBHelper.ExecuteNonQuery(tran, sql, paras, CommandType.Text);
        }

        /// <summary>
        /// 改变会员推荐关系
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="number">编号</param>
        /// <param name="placement">新推荐</param>
        public static int ChangeDirect(SqlTransaction tran, string number, string direct)
        {
            string sql = "update memberinfo set direct= @placement where number = @number";
            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@placement",direct),                                                                       
                new SqlParameter("@number",number)
            };
            return DBHelper.ExecuteNonQuery(tran, sql, paras, CommandType.Text);
        }

        public static DataTable GetRecordDataTable(string number, int maxExpect)
        {
            string sql = "select CurrentOneMark,TotalOneMark from MemberInfoBalance" + maxExpect + " where number=@number";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@number", number) };
            return DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
        }
        /// <summary>
        /// 检测会员编号是否重复
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static int checkNumber(string NumberName)
        {
            int i = 0;
            string sql = "select count(*) from MemberInfo where PetName=@num";
            SqlParameter[] parm = new SqlParameter[] { 
                    new SqlParameter("@num",SqlDbType.NVarChar,50),
                   
            
                };
            parm[0].Value = NumberName;
            i = (int)DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            return i;
        }


        #region 删除新注册的会员
        /// <summary>
        /// 注册浏览删除会员信息
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <param name="Number">期数</param>
        public static bool DeleteMemberInfo(string Number, int ExpectNum)
        {
            int error = 0;
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.VarChar, 50), new SqlParameter("@ExpectNum", SqlDbType.Int), new SqlParameter("@error", SqlDbType.Int) };
            parm[0].Value = Number;
            parm[1].Value = ExpectNum;
            parm[2].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery("DelMemberInfo", parm, CommandType.StoredProcedure);
            error = int.Parse(parm[2].Value.ToString());
            if (error == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 验证会员是否可以删除
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <param name="error">错误信息</param>
        public static void IsDeleteMemberInfo(string Number, out string error)
        {
            error = "";
            //是否有复消
            bool blean = IsFuxiao(Number);
            if (blean)
            {
                error = "该会员已经购物不能删除！";
                return;
            }
            //会员是否存在
            if (IsMemberExist(Number) == false)
            {
                error = "该会员不存在！";
                return;
            }
            //是否安置了其他人
            if (IshavingAnzhi(Number))
            {
                error = "该会员下面已经接收其他会员了，不可以删除！";
                return;
            }
            //是否推荐了其他人
            if (IshavingTj(Number))
            {
                error = "该会员推荐其他会员了，不可以删除！";
                return;
            }
        }
        /// <summary>
        /// 是否有复消
        /// </summary>
        /// <param name="Number">编号</param>
        /// <returns></returns>
        public static bool IsFuxiao(string Number)
        {
            string sql = "select count(*) from MemberOrder where Number=@Number";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.VarChar, 50) };
            parm[0].Value = Number;
            object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (int.Parse(obj.ToString()) == 0)
            {
                //不存在
                return false;
            }
            else
            {
                //存在
                return true;
            }
        }
        /// <summary>
        /// 会员是否存在
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <returns></returns>
        public static bool IsMemberExist(string Number)
        {
            string sql = "select count(*) from MemberInfo where Number=@Number";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.VarChar, 50) };
            parm[0].Value = Number;
            object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (int.Parse(obj.ToString()) == 0)
            {
                //不存在
                return false;
            }
            else
            {
                //存在
                return true;
            }
        }
        /// <summary>
        /// 下面是否安置了其他会员
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <returns></returns>
        public static bool IshavingAnzhi(string Number)
        {
            string sql = "select count(*) from MemberInfo where Placement=@Number";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.VarChar, 50) };
            parm[0].Value = Number;
            object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (int.Parse(obj.ToString()) == 0)
            {
                //不存在
                return false;
            }
            else
            {
                //存在
                return true;
            }
        }
        /// <summary>
        /// 是否推荐了其他人
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <returns></returns>
        public static bool IshavingTj(string Number)
        {
            string sql = "select count(*) from MemberInfo where Direct=@Number";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.VarChar, 50) };
            parm[0].Value = Number;
            object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (int.Parse(obj.ToString()) == 0)
            {
                //不存在
                return false;
            }
            else
            {
                //存在
                return true;
            }
        }
        #endregion

        /// <summary>
        /// 获取在指期数之前会员编号是否进入该系统
        /// </summary>
        /// <param name="number"></param>
        /// <param name="expectNum"></param>
        /// <returns></returns>
        public static int GetMemberInfoCount(string number, int expectNum)
        {
            string sql = "select count(1) from MemberInfo where number = @number and expectNum<=@expectNum";
            SqlParameter[] para = new SqlParameter[]{new SqlParameter("@number", number),
                new SqlParameter("@expectNum",expectNum)
            };
            object obj = null;
            obj = DBHelper.ExecuteScalar(sql, para, CommandType.Text);
            return int.Parse(obj.ToString());
        }

        public static DataTable getMemberInfoTable(string Number)
        {
            SqlParameter[] par = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.VarChar) };
            par[0].Value = Number;
            return DBHelper.ExecuteDataTable("getMemberByID", par, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 读取当期会员奖金
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <param name="number">会员编号</param>
        public static MemberInfoBalanceNModel getBonus(int ExpectNum, string number)
        {
            MemberInfoBalanceNModel info = new MemberInfoBalanceNModel();
            string sql = "select * from MemberInfoBalance" + ExpectNum + " where number=@num";

            SqlParameter[] parm = new SqlParameter[] { 
                    new SqlParameter("@num",SqlDbType.NVarChar,20),                             
                };
            parm[0].Value = number;
            SqlDataReader reader = DBHelper.ExecuteReader(sql, parm, CommandType.Text);
            if (reader.Read())
            {
                info.Bonus0 = double.Parse(reader["Bonus0"].ToString());
                info.Bonus1 = double.Parse(reader["Bonus1"].ToString());
                info.Bonus2 = double.Parse(reader["Bonus2"].ToString());
                info.Bonus3 = double.Parse(reader["Bonus3"].ToString());
                info.Bonus4 = double.Parse(reader["Bonus4"].ToString());
                info.Bonus5 = double.Parse(reader["Bonus5"].ToString());
                info.CurrentTotalMark = double.Parse(reader["CurrentTotalMoney"].ToString());
                info.DeductMoney = double.Parse(reader["DeductMoney"].ToString());
                info.DeductTax = double.Parse(reader["DeductTax"].ToString());
                info.SolidSendAccumulation = double.Parse(reader["SolidSendAccumulation"].ToString());
            }
            reader.Close();
            return info;
        }
        /// <summary>
        /// 获取当期补款额
        /// </summary>
        /// <param name="number">编号</param>
        /// <param name="ExpectNum">期数</param>
        /// <returns></returns>
        public static double GetDeductMoney(string number, int ExpectNum)
        {
            string sql = "select sum(DeductMoney) from Deduct where Number=@num and ExpectNum=@num1    and IsDeduct=1";
            SqlParameter[] parm = new SqlParameter[] { 
                    new SqlParameter("@num",SqlDbType.NVarChar,20),
                      new SqlParameter("@num1",SqlDbType.Int),       
                };
            parm[0].Value = number;
            parm[1].Value = ExpectNum;
            object obj = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (obj != null)
                return double.Parse(obj.ToString());
            else
                return 0;
        }
        /// <summary>
        /// 读取当期会员奖金
        /// </summary>
        /// <param name="ExpectNum">期数</param>
        /// <param name="number">会员编号</param>
        public static DataTable getBonusTable(int ExpectNum, string number)
        {
            if (ExpectNum == 0) ExpectNum = 1;
            string sql = "select * from MemberInfoBalance" + ExpectNum + " where  number=@num";
            SqlParameter[] parm = new SqlParameter[] { 
                    new SqlParameter("@num",SqlDbType.NVarChar,20)
                        
                };
            parm[0].Value = number;

            return DBHelper.ExecuteDataTable(sql, parm, CommandType.Text);
        }

        /// <summary>
        /// 过去会员登录的最后时间
        /// </summary>
        /// <param name="memberid">会员编号</param>
        /// <returns></returns>
        public static DateTime GetLastLoginTime(string memberid)
        {
            string sql = "select lastlogindate from memberinfo where number=@num";
            SqlParameter[] parm = new SqlParameter[] { 
                    new SqlParameter("@num",SqlDbType.NVarChar,50)
                        
                };
            parm[0].Value = memberid;
            return DateTime.Parse(DBHelper.ExecuteScalar(sql, parm, CommandType.Text).ToString());
        }

        /// <summary>
        /// 获取会员姓名
        /// </summary>
        /// <param name="memberid">会员编号</param>
        /// <returns></returns>
        public static string GetMemberName(string memberid)
        {
            string sql = "select name from memberinfo where number=@num";
            SqlParameter[] parm = new SqlParameter[] { 
                    new SqlParameter("@num",SqlDbType.NVarChar,50)
                        
                };
            parm[0].Value = memberid;
            object res = DBHelper.ExecuteScalar(sql, parm, CommandType.Text);

            return res == null ? "" : res.ToString();
        }

        /// <summary>
        /// 获取分公司姓名
        /// </summary>
        /// <param name="memberid">分公司编号</param>
        /// <returns></returns>
        //public static string GetBranchName(string memberid)
        //{

        //    return DBHelper.ExecuteScalar("select name from branchmanage where number='" + memberid + "' ").ToString();
        //}

        /// <summary>
        /// 根据会员编号查询会员是否存在
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <returns>是否存在</returns>
        public static bool SelectMemberExist(string number)
        {
            if (DBHelper.ExecuteScalar("select count(0) from memberinfo where number=@num ", new SqlParameter[] { new SqlParameter("@num", number) }, CommandType.Text).ToString() == "0")
            {
                return false;
            }
            return true;
        }

        public static bool CheckMemberZx(string number)
        {
            string sql = "Select isActive From MemberInfo Where number=@num";
            SqlParameter[] para = {
                                      new SqlParameter("@num",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = number;
            string isActive = DBHelper.ExecuteScalar(sql, para, CommandType.Text).ToString();
            if (isActive == "1")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证会员电子账户密码
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="pass">账户密码</param>
        /// <returns>返回查询行数</returns>
        public static int CheckEctPassWord(string number, string pass)
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("select count(0) from memberinfo where number=@num and advpass=@pass", new SqlParameter[] { new SqlParameter("@num", number), new SqlParameter("@pass", pass) }, CommandType.Text));
        }

        /// <summary>
        /// 修改会员基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Updmemberbasic(MemberInfoModel info)
        {
            int i = 0;
            string sql = "update [memberinfo] set [name]=@name,petname=@petname,storeid=@storeid,PaperTypeCode=@PaperTypeCode,sex=@sex,Birthday=@Birthday,papernumber=@papernumber,CPCCode=@CPCCode,address=@address,postalcode=@postalcode,bankbook=@bankbook,photopath=@photopath where number=@number";
            SqlParameter[] par = new SqlParameter[] 
            { 
                                      new SqlParameter("@name", SqlDbType.VarChar,50), new SqlParameter("@petname", SqlDbType.VarChar,50),
  new SqlParameter("@storeid",SqlDbType.VarChar,50),
  new SqlParameter("@PaperTypeCode",SqlDbType.VarChar,50),
  new SqlParameter("@sex",SqlDbType.Int,50),

  new SqlParameter("@Birthday",SqlDbType.DateTime),
  new SqlParameter("@papernumber",SqlDbType.VarChar,50),
  new SqlParameter("@CPCCode",SqlDbType.VarChar,40),
  new SqlParameter("@address",SqlDbType.VarChar,500),
  new SqlParameter("@postalcode",SqlDbType.NVarChar,30),

  new SqlParameter("@bankbook",SqlDbType.VarChar,500),
                                      new SqlParameter("@photopath",SqlDbType.VarChar,50),
  new SqlParameter("@number",SqlDbType.VarChar,50)
            };
            par[0].Value = info.Name;
            par[1].Value = info.PetName;
            par[2].Value = info.StoreID;
            par[3].Value = info.Papertypecode;
            par[4].Value = info.Sex;

            par[5].Value = info.Birthday;
            par[6].Value = info.PaperNumber;
            par[7].Value = info.CPCCode;
            par[8].Value = info.Address;
            par[9].Value = info.PostalCode;

            par[10].Value = info.BankBook;
            par[11].Value = info.PhotoPath;
            par[12].Value = info.Number;
            try
            {
                i = DBHelper.ExecuteNonQuery(sql, par, CommandType.Text);
            }
            catch (Exception ex1)
            {
                i = 0;
            }
            return i;
        }


        /// <summary>
        /// 修改会员基本信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Updmemberbasic(SqlTransaction tran, MemberInfoModel info)
        {
            int i = 0;
            string sql = "update [memberinfo] set [name]=@name,petname=@petname,storeid=@storeid,PaperTypeCode=@PaperTypeCode,sex=@sex,Birthday=@Birthday,papernumber=@papernumber,CPCCode=@CPCCode,address=@address,postalcode=@postalcode,bankbook=@bankbook,photopath=@photopath where number=@number";
            SqlParameter[] par = new SqlParameter[] 
            { 
                                      new SqlParameter("@name", SqlDbType.VarChar,50), new SqlParameter("@petname", SqlDbType.VarChar,50),
  new SqlParameter("@storeid",SqlDbType.VarChar,50),
  new SqlParameter("@PaperTypeCode",SqlDbType.VarChar,50),
  new SqlParameter("@sex",SqlDbType.Int,50),

  new SqlParameter("@Birthday",SqlDbType.DateTime),
  new SqlParameter("@papernumber",SqlDbType.VarChar,50),
  new SqlParameter("@CPCCode",SqlDbType.VarChar,40),
  new SqlParameter("@address",SqlDbType.VarChar,500),
  new SqlParameter("@postalcode",SqlDbType.NVarChar,30),

  new SqlParameter("@bankbook",SqlDbType.VarChar,500),
                                      new SqlParameter("@photopath",SqlDbType.VarChar,50),
  new SqlParameter("@number",SqlDbType.VarChar,50)
            };
            par[0].Value = info.Name;
            par[1].Value = info.PetName;
            par[2].Value = info.StoreID;
            par[3].Value = info.Papertypecode;
            par[4].Value = info.Sex;

            par[5].Value = info.Birthday;
            par[6].Value = info.PaperNumber;
            par[7].Value = info.CPCCode;
            par[8].Value = info.Address;
            par[9].Value = info.PostalCode;

            par[10].Value = info.BankBook;
            par[11].Value = info.PhotoPath;
            par[12].Value = info.Number;
            try
            {
                i = DBHelper.ExecuteNonQuery(tran, sql, par, CommandType.Text);
            }
            catch (Exception ex1)
            {
                i = 0;
            }
            if (i == 0)
                return false;
            return true;
        }


        /// <summary>
        /// 修改会员联系信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdmemContact(MemberInfoModel info)
        {
            int i = 0;
            string sql = @"update memberinfo set MobileTele=@MobileTele,
                                HomeTele=@HomeTele,
                                FaxTele=@FaxTele,
                                OfficeTele=@OfficeTele,
                                Email=@Email where number=@number";
            SqlParameter[] par = new SqlParameter[] 
            { 
                                      new SqlParameter("@MobileTele", SqlDbType.NVarChar,30),
									  new SqlParameter("@HomeTele", SqlDbType.NVarChar,30),
									  new SqlParameter("@FaxTele",SqlDbType.VarChar,30),
									  new SqlParameter("@OfficeTele",SqlDbType.NVarChar,30),
									  new SqlParameter("@Email",SqlDbType.NVarChar,50),
									  new SqlParameter("@number",SqlDbType.NVarChar,50)
            };
            par[0].Value = info.MobileTele;
            par[1].Value = info.HomeTele;
            par[2].Value = info.FaxTele;
            par[3].Value = info.OfficeTele;
            par[4].Value = info.Email;

            par[5].Value = info.Number;
            try
            {
                i = DBHelper.ExecuteNonQuery(sql, par, CommandType.Text);
            }
            catch (Exception ex1)
            {
                i = 0;
            }
            return i;
        }

        /// <summary>
        /// 修改会员联系信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdmemContact(SqlTransaction tran, MemberInfoModel info)
        {
            int i = 0;
            string sql = @"update memberinfo set MobileTele=@MobileTele,
                                HomeTele=@HomeTele,
                                FaxTele=@FaxTele,
                                OfficeTele=@OfficeTele,
                                Email=@Email where number=@number";
            SqlParameter[] par = new SqlParameter[] 
            { 
                                      new SqlParameter("@MobileTele", SqlDbType.NVarChar,30),
									  new SqlParameter("@HomeTele", SqlDbType.NVarChar,30),
									  new SqlParameter("@FaxTele",SqlDbType.VarChar,30),
									  new SqlParameter("@OfficeTele",SqlDbType.NVarChar,30),
									  new SqlParameter("@Email",SqlDbType.NVarChar,50),
									  new SqlParameter("@number",SqlDbType.NVarChar,50)
            };
            par[0].Value = info.MobileTele;
            par[1].Value = info.HomeTele;
            par[2].Value = info.FaxTele;
            par[3].Value = info.OfficeTele;
            par[4].Value = info.Email;

            par[5].Value = info.Number;
            try
            {
                i = DBHelper.ExecuteNonQuery(tran, sql, par, CommandType.Text);
            }
            catch (Exception ex1)
            {
                i = 0;
            }
            if (i == 0)
                return false;
            return true;
        }
        /// <summary>
        /// 修改会员银行信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdmemBank(MemberInfoModel info)
        {
            int i = 0;
            string sql = "update memberinfo set bankcard=@bankcard,bankaddress=@bankaddress,bankbranchname=@bankbranchname,BankCode=@BankCode,bankbook=@bankbook,bcpccode=@bcpccode where number=@number";
            SqlParameter[] par = new SqlParameter[] 
            { 
                                      new SqlParameter("@bankcard", SqlDbType.VarChar,500),
									  new SqlParameter("@bankaddress", SqlDbType.VarChar,500),
									  new SqlParameter("@bankbranchname",SqlDbType.VarChar,500),
									  new SqlParameter("@BankCode",SqlDbType.VarChar,30),
                                      new SqlParameter("@bankbook",SqlDbType.VarChar,30),
                                      new SqlParameter("@bcpccode",SqlDbType.VarChar,40),
									  new SqlParameter("@number",SqlDbType.VarChar,50)
            };
            par[0].Value = info.BankCard;
            par[1].Value = info.BankAddress;
            par[2].Value = info.Bankbranchname;
            par[3].Value = info.Bank.BankName;
            par[4].Value = info.BankBook;
            par[5].Value = info.BCPCCode;
            par[6].Value = info.Number;

            try
            {
                i = DBHelper.ExecuteNonQuery(sql, par, CommandType.Text);
            }
            catch (Exception ex1)
            {
                i = 0;
            }
            return i;
        }

        /// <summary>
        /// 修改会员银行信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdmemBank(SqlTransaction tran, MemberInfoModel info)
        {
            int i = 0;
            string sql = "update memberinfo set bankcard=@bankcard,bankaddress=@bankaddress,bankbranchname=@bankbranchname,BankCode=@BankCode,bankbook=@bankbook,bcpccode=@bcpccode where number=@number";
            SqlParameter[] par = new SqlParameter[] 
            { 
                                      new SqlParameter("@bankcard", SqlDbType.VarChar,500),
									  new SqlParameter("@bankaddress", SqlDbType.VarChar,500),
									  new SqlParameter("@bankbranchname",SqlDbType.VarChar,500),
									  new SqlParameter("@BankCode",SqlDbType.VarChar,30),
                                      new SqlParameter("@bankbook",SqlDbType.VarChar,30),
                                      new SqlParameter("@bcpccode",SqlDbType.VarChar,40),
									  new SqlParameter("@number",SqlDbType.VarChar,50)
            };
            par[0].Value = info.BankCard;
            par[1].Value = info.BankAddress;
            par[2].Value = info.Bankbranchname;
            par[3].Value = info.Bank.BankName;
            par[4].Value = info.BankBook;
            par[5].Value = info.BCPCCode;
            par[6].Value = info.Number;

            try
            {
                i = DBHelper.ExecuteNonQuery(tran, sql, par, CommandType.Text);
            }
            catch (Exception ex1)
            {
                i = 0;
            }
            if (i == 0)
                return false;
            return true;
        }

        /// <summary>
        /// 获取银行国家
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string Getbank(string number)
        {
            string str = string.Empty;
            string sql = "select c.countrycode from memberinfo as m left join memberbank as b on m.bankcode=b.bankcode left join country as c on b.countrycode=c.id where m.number=@number";
            SqlParameter[] par = new SqlParameter[] 
            { 
                  new SqlParameter("@number", SqlDbType.VarChar,500)
            };
            par[0].Value = number;
            try
            {
                str = Convert.ToString(DBHelper.ExecuteScalar(sql, par, CommandType.Text));
            }
            catch (Exception ex1)
            {
                str = "";
            }
            return str;
        }
        /// <summary>
        /// 获取银行国家 字符串
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetbankStr(string number)
        {
            string str = string.Empty;
            string sql = "select c.[name],b.bankname,m.bankbranchname from memberinfo as m left join memberbank as b on m.bankcode=b.bankcode left join country as c on b.countrycode=c.id where m.number=@number";
            SqlParameter[] par = new SqlParameter[] 
            { 
                  new SqlParameter("@number", SqlDbType.VarChar,500)
            };
            par[0].Value = number;
            try
            {
                SqlDataReader reader = DBHelper.ExecuteReader(sql, par, CommandType.Text);
                while (reader.Read())
                {
                    str = reader["name"].ToString();
                    str = str + " " + reader["bankname"].ToString();
                    str = str + " " + reader["bankbranchname"].ToString();
                }
                reader.Read();
            }
            catch (Exception ex1)
            {
                str = "";
            }

            return str;
        }

        /// <summary>
        /// 获取银行国家 字符串
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Boolean GetStorenumber(string number)
        {
            string sql = "select count(@@rowcount) from storeinfo where storeid=@storeid";
            SqlParameter pare = new SqlParameter("@storeid", number);
            return Convert.ToInt32(DBHelper.ExecuteScalar(sql, pare, CommandType.Text)) == 0 ? false : true;
        }
        /// <summary>
        /// 获取会员名单导出(2011-11-07)
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public static DataTable GetMemberQueryToExcel(string condition)
        {
            SqlParameter[] param = { new SqlParameter("@condition", SqlDbType.NVarChar) };
            param[0].Value = condition;
            return DBHelper.ExecuteDataTable("memberQueryToExcel", param, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 获取会员密码重置导出(2011-11-07)
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public static DataTable GetMemberPassResetToExcel(string condition)
        {
            SqlParameter[] param = { new SqlParameter("@condition", SqlDbType.NVarChar) };
            param[0].Value = condition;
            return DBHelper.ExecuteDataTable("MemberPassRestToExcel", param, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 设置会员级别
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="number">会员编号</param>
        /// <param name="orderid">订单号</param>
        /// <returns></returns>
        public static bool SetMemberLevel(SqlTransaction tran, string number, string orderid)
        {
            SqlParameter[] paras = { new SqlParameter("@number", number), new SqlParameter("@orderid", orderid) };
            return DBHelper.ExecuteNonQuery(tran, "SetMemberLevel", paras, CommandType.StoredProcedure) > 0 ? true : false;
        }
        /// <summary>
        /// 根据编号获取会员昵称和级别
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Getmembernameandlevestr(string number)
        {
            string res = "";
            string sqsltr = "select  petname ,levelstr,levelint, from memberinfo m  ,BSCO_level b  where m.levelint = b.levelint and b.levelflag=0 and m.number=@number  ";
            SqlParameter[] sps = new SqlParameter[] { new SqlParameter("@number", number) };
            DataTable dt = DBHelper.ExecuteDataTable(sqsltr, sps, CommandType.Text);
            string jb = dt.Rows[0]["levelint"].ToString();
            if (dt != null && dt.Rows.Count > 0)
            {
                res = dt.Rows[0]["petname"].ToString() + "," + dt.Rows[0]["levelstr"].ToString();
            }
            return res;
        }

        /// <summary>
        /// 获取会员总的报单金额（totalmoney）和PV（totalpv）
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static DataTable GetMemberOrderMoneyPVSum(string number)
        {
            string sql = @"select sum(isnull(t.totalmoney,0))-sum(isnull(t.totalmoneyReturned,0)) as totalmoney,
                sum(isnull(t.totalpv,0))-sum(isnull(t.totalpvreturned,0)) as totalpv from memberorder t
                where t.defraystate=1 and t.number=@number";
            SqlParameter[] param = { new SqlParameter("@number", SqlDbType.NVarChar, 20) };
            param[0].Value = number;
            return DBHelper.ExecuteDataTable(sql, param, CommandType.Text);
        }

        /// <summary>
        /// 获取会员某条报单的剩余金额（totalmoney）和PV（totalpv）
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static DataTable GetOrderMoneyPVSumByNumber(string orderid)
        {
            string sql = @"select isnull(t.totalmoney,0)-isnull(t.totalmoneyReturned,0) as totalmoney,
                isnull(t.totalpv,0)-isnull(t.totalpvreturned,0) as totalpv from memberorder t
                where t.defraystate=1 and t.orderid=@orderid";
            SqlParameter[] param = { new SqlParameter("@orderid", SqlDbType.NVarChar, 20) };
            param[0].Value = orderid;
            return DBHelper.ExecuteDataTable(sql, param, CommandType.Text);
        }

        /// <summary>
        /// 找回密码时，验证填入的编号，姓名，证件号码是否相同
        /// </summary>
        /// <param name="number">编号</param>
        /// <param name="username">姓名</param>
        /// <param name="zjnumber">证件号码</param>
        /// <param name="typeint">类型(1为服务中心，2为会员)</param>
        /// <returns></returns>
        public static bool CheckZJNumber(string number, string username, string zjnumber, int typeint)
        {
            bool info = false;
            if (typeint == 1)
            {
                string sql = @"select isnull(si.storename,'') as storename,isnull(mi.PaperNumber,'')as papernumber from memberinfo mi , storeinfo si where mi.number=si.number and si.storeid = @number";
                SqlParameter[] param = { new SqlParameter("@number", SqlDbType.NVarChar, 20) };
                param[0].Value = number;
                DataTable dt = DBHelper.ExecuteDataTable(sql, param, CommandType.Text);
                if (dt.Rows.Count > 0 && dt.Rows[0]["PaperNumber"].ToString() != "")
                {
                    if (dt.Rows[0]["storename"].ToString() == Encryption.Encryption.GetEncryptionName(username) && dt.Rows[0]["papernumber"].ToString() == Encryption.Encryption.GetEncryptionNumber(zjnumber))
                    {
                        info = true;
                    }
                    else
                    {
                        info = false;
                    }
                }
            }
            else if (typeint == 2)
            {
                string sql = @"select isnull(name,'') as name , isnull(PaperNumber ,'') as PaperNumber from memberinfo where number=@number";
                SqlParameter[] param = { new SqlParameter("@number", SqlDbType.NVarChar, 20) };
                param[0].Value = number;
                DataTable dt = DBHelper.ExecuteDataTable(sql, param, CommandType.Text);
                if (dt.Rows.Count > 0 && dt.Rows[0]["PaperNumber"].ToString() != "")
                {
                    if (dt.Rows[0]["name"].ToString() == Encryption.Encryption.GetEncryptionName(username) && dt.Rows[0]["PaperNumber"].ToString() == Encryption.Encryption.GetEncryptionNumber(zjnumber))
                    {
                        info = true;
                    }
                    else
                    {
                        info = false;
                    }
                }
            }
            return info;
        }
        /// <summary>
        /// 判断会员是否被冻结
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool CheckState(string number)
        {
            bool res = false;
            if ((int)DAL.DBHelper.ExecuteScalar("select memberstate from memberinfo where number='" + number + "'") == 3)
            {
                res = true;
            }
            else
            {
                res = false;
            }
            return res;
        }
        public static object JFparameter(string name)
        {
            string sql = "select value from JLparameter where parameter='" + name + "'";
            object value = DBHelper.ExecuteScalar(sql);
            return value;
        }

        public static object Sxfparameter()
        {
            string sql = "select WithdrawSXF from WithdrawSz";
            object value = DBHelper.ExecuteScalar(sql);
            return value;
        }

        /// <summary>
        /// 指定位置是否有人安置
        /// </summary>
        /// <param name="placement"></param>
        /// <param name="qushu"></param>
        /// <returns></returns>
        public static int CheckMemAtQushu(string placement, string qushu)
        {
            string sql = "select count(0) from MemberInfo where Placement=@placement and District=@qushu";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@placement", placement), new SqlParameter("@qushu", qushu) };
            return (int)DBHelper.ExecuteScalar(sql, para, CommandType.Text);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Number"></param>
        /// <param name="Isdefault"></param>
        /// <returns></returns>
        public static ConsigneeInfo getconsigneeInfo(string Number,bool Isdefault)
        {
            ConsigneeInfo consigneeInfo = null;
            string sqlStr = "";
            DataTable dt = null;
            if (Isdefault)
            {
                sqlStr = @"select ID,Number,Consignee,MoblieTele,CPCCode,[Address],ConZipCode,IsDefault from ConsigneeInfo
where  Number=@Number and IsDefault=@IsDefault";
                SqlParameter[] par = new SqlParameter[] {
                    new SqlParameter("@Number", SqlDbType.VarChar),
                    new SqlParameter("@IsDefault", SqlDbType.Bit),
                };
                par[0].Value = Number;
                par[1].Value = Isdefault;
                dt = DBHelper.ExecuteDataTable(sqlStr, par, CommandType.Text);
            }
            else
            {
                sqlStr = @"select ID,Number,Consignee,MoblieTele,CPCCode,[Address],ConZipCode,IsDefault from ConsigneeInfo
where  Number=@Number";
                SqlParameter[] par = new SqlParameter[] { new SqlParameter("@Number", SqlDbType.VarChar) };
                par[0].Value = Number;

                dt = DBHelper.ExecuteDataTable(sqlStr, par, CommandType.Text);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    consigneeInfo = new ConsigneeInfo();
                    consigneeInfo.Id = Convert.ToInt32(dr["Id"].ToString());
                    consigneeInfo.Number = dr["Number"].ToString();
                    consigneeInfo.Consignee = dr["Consignee"].ToString();
                    consigneeInfo.MoblieTele = dr["MoblieTele"].ToString();
                    consigneeInfo.CPCCode = dr["CPCCode"].ToString();
                    consigneeInfo.Address = dr["Address"].ToString();
                    consigneeInfo.ConZipCode = dr["ConZipCode"].ToString();
                    consigneeInfo.IsDefault = Convert.ToInt32(dr["IsDefault"]) == 1 ? true : false;
                }
            }
            return consigneeInfo;
        }
    }
}