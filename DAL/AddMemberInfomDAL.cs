using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Data;

/* 
 * 作者：郑华超
 * 时间：2009-9-1
 * 功能：添加会员注册信息
 * 
 */
namespace DAL
{
    public class AddMemberInfomDAL
    {

        /// <summary>
        /// 调用存储过程
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertMemberInfo(MemberInfoModel model, SqlTransaction tran)
        {
            //SqlTransaction tran = null;
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@Number", model.Number),
                new SqlParameter("@Placement", model.Placement),
                new SqlParameter("@Recommended", model.Direct),
                new SqlParameter("@ExpectNum", model.ExpectNum),
                new SqlParameter("@OrderID", model.OrderID),
                new SqlParameter("@StoreID", model.StoreID),
                new SqlParameter("@Name", model.Name),
                new SqlParameter("@PetName",model.PetName),   
                new SqlParameter("@LoginPass", model.LoginPass),
                new SqlParameter("@Advpass", model.AdvPass),
                //new SqlParameter("@LevelStr", model.LevelStr),
                new SqlParameter("@LevelInt", model.LevelInt),
                new SqlParameter("@RegisterDatec", model.RegisterDate),
                new SqlParameter("@Birthday", model.Birthday),
                new SqlParameter("@Sex", model.Sex),
                new SqlParameter("@HomeTele", model.HomeTele),
                new SqlParameter("@OfficeTele", model.OfficeTele),
                new SqlParameter("@MobileTele", model.MobileTele),
                new SqlParameter("@FaxTele", model.FaxTele),
                //new SqlParameter("@Country", model.Country),
                //new SqlParameter("@Province", model.Province),
                //new SqlParameter("@City", model.City),
                new SqlParameter("@CPCCode",model.CPCCode),
                new SqlParameter("@Address", model.Address),
                new SqlParameter("@PostalCode", model.PostalCode),
                new SqlParameter("@PaperTypeCode", model.PaperType.PaperTypeCode),
                new SqlParameter("@PaperNumber", model.PaperNumber),
                new SqlParameter("@BankCode", model.BankCode),
                new SqlParameter("@BankAddress" , model.BankAddress ),
                new SqlParameter("@BankCard", model.BankCard),
                //new SqlParameter("@BankCountry", model.BankCountry),
                //new SqlParameter("@BankProvince", model.BankProvince),
                //new SqlParameter("@BankCity", model.BankCity),
                 new SqlParameter("@BCPCCode",model.BCPCCode),
                new SqlParameter("@BankBook", model.BankBook),
                new SqlParameter("@Remark", model.Remark),
                new SqlParameter("@ChangeInfo", model.ChangeInfo),
                new SqlParameter("@Photopath", model.PhotoPath),
                new SqlParameter("@Email",model.Email),
				new SqlParameter("@IsBatch",model.IsBatch),
				new SqlParameter("@Language",model.Language),
				new SqlParameter("@OperateIP",model.OperateIp),
				new SqlParameter("@OperaterNumber",model.OperaterNum),
				new SqlParameter("@District",model.District),
                new SqlParameter("@Answer",model.Answer),
                new SqlParameter("@Question",model.Question),
                new SqlParameter("@Err",model.Error),
                new SqlParameter("@BankBranchName",model.Bankbranchname),
       
            };

            return DBHelper.ExecuteNonQuery(tran, "UP_MemberInfo_ADD", para, CommandType.StoredProcedure);

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
        public static DataTable QueryDeclaration2(string storeId, string condition, string symbol, string character, int expectNum)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@storeId", storeId),
                new SqlParameter("@symbol", symbol),
                new SqlParameter("@character", character),
                new SqlParameter("@expectNum", expectNum),
                new SqlParameter("@condition",condition),
                //new SqlParameter("@defraystate",SqlDbType.Int),
                //new SqlParameter("@isAgin",SqlDbType.Int),
                //new SqlParameter("@IsReceivables",SqlDbType.Int)
            };

            dt = DBHelper.ExecuteDataTable("P_GetMemberRepeatConsume", param, CommandType.StoredProcedure);

            return dt;
        }

        /// <summary>
        /// 判断批量注册的会员安置人编号、推荐人编号和店铺是否存在
        /// </summary>
        /// <param name="derict"></param>
        public int GetPlacementCount(string placement, SqlTransaction tran)
        {
            string sql = "select count(1) from  memberInfo  where  number=@placement";
            SqlParameter[] para = new SqlParameter[] 
            {
             new SqlParameter("@placement", placement),
              
            };
            return Convert.ToInt32(DBHelper.ExecuteScalar(tran, sql, para, CommandType.Text));

        }


        /// <summary>
        /// 判断批量注册的会员安置人编号、推荐人编号和店铺是否存在
        /// </summary>
        /// <param name="derict"></param>
        public int GetDerictCount(string derict, SqlTransaction tran)
        {
            int isTuijian = 0;
            string sql = "select count(1) from  memberInfo  where  number=@derict";
            SqlParameter[] para = new SqlParameter[] 
           {
             new SqlParameter("@derict", derict),
              
           };
            return Convert.ToInt32(DBHelper.ExecuteScalar(tran, sql, para, CommandType.Text));

        }

        /// <summary>
        /// 判断批量注册的会员安置人编号、推荐人编号和店铺是否存在
        /// </summary>
        /// <param name="derict"></param>
        public int GetStoreIdCount(string storeId, SqlTransaction tran)
        {
            string err = "";
            int isStore = 0;
            string sql = "select count(1) from  storeInfo  where  storeid=@storeId";

            SqlParameter[] para = new SqlParameter[] 
           {
             new SqlParameter("@storeId", storeId),
              
           };
            return Convert.ToInt32(DBHelper.ExecuteScalar(tran, sql, para, CommandType.Text));
        }

        /// <summary>
        /// 批量注册更新错误
        /// </summary>
        /// <returns></returns>
        public int UpdateErr(string err, string orderId, SqlTransaction tran)
        {
            string sql = "update memberOrder  set Error=@err where orderid=@orderId ";
            SqlParameter[] para = new SqlParameter[] 
           {
             new SqlParameter("@err", err),
             new SqlParameter("@orderId", orderId) 
           };

            return DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
        }

        public void UpdateErr(string oldNumber, SqlTransaction tran)
        {
            string sql = "update  memberOrder  set  Error='无安置' where  IsAgain=0  and  number in (select number from memberInfo  where Placement=@oldNumber)";
            SqlParameter[] para = new SqlParameter[] 
           {
             new SqlParameter("@oldNumber", oldNumber) 
           };
            string sql2 = "update  memberOrder  set  Error='无推荐' where  IsAgain=0  and  number in (select number from memberInfo  where Direct=@oldNumber)";
            SqlParameter[] para2 = new SqlParameter[] 
           {
             new SqlParameter("@oldNumber", oldNumber) 
           };

            int test1 = DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
            int test2 = DBHelper.ExecuteNonQuery(tran, sql2, para2, CommandType.Text);
        }

        /// <summary>
        /// 通过银行号查银行信息
        /// </summary>
        /// <param name="bankCode"></param>
        public IList<string> GetBankValue(string bankCode)
        {
            List<string> list = new List<string>();
            string sql = "select * from memberBank where bankCode=@bankCode";
            SqlParameter[] para = new SqlParameter[] 
           {
             new SqlParameter("@bankCode", bankCode) 
           };
            SqlDataReader reader = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            if (reader.Read())
            {
                list.Add(reader["BankName"].ToString());
                list.Add(reader["BankCode"].ToString());
            }
            reader.Close();
            return list;

        }

        /// <summary>
        /// 获得银行code
        /// </summary>
        /// <param name="bankName"></param>
        /// <returns></returns>
        public string GetBankCode(string bankName)
        {
            string sql = "select bankCode from memberBank where bankName=@bankName";
            SqlParameter[] para = new SqlParameter[] 
           {
             new SqlParameter("@bankName", bankName) 
           };
            return DBHelper.ExecuteScalar(sql, para, CommandType.Text).ToString();
        }


        /// <summary>
        /// 通过paperTypeCode查询证件信息
        /// </summary>
        /// <param name="paperTypeCode"></param>
        /// <returns></returns>
        public List<string> GetCardType(string paperTypeCode)
        {
            List<string> list = new List<string>();
            string sql = "select * from bsco_PaperType where paperTypeCode=@paperTypeCode";
            SqlParameter[] para = new SqlParameter[] 
           {
             new SqlParameter("@paperTypeCode", paperTypeCode) 
           };
            SqlDataReader reader = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            if (reader.Read())
            {
                list.Add(reader["PaperType"].ToString());
                list.Add(reader["PaperTypeCode"].ToString());
                list.Add(reader["id"].ToString());
            }
            reader.Close();
            return list;
        }


        /// <summary>
        /// 获得银行code
        /// </summary>
        /// <param name="bankName"></param>
        /// <returns></returns>
        public string GetPaperTypeCode(string paperTypeName)
        {
            string sql = "select paperTypeCode from bsco_PaperType where PaperType=@paperTypeName";
            SqlParameter[] para = new SqlParameter[] 
           {
             new SqlParameter("@paperTypeName", paperTypeName) 
           };
            return DBHelper.ExecuteScalar(sql, para, CommandType.Text).ToString();
        }

        /// <summary>
        /// 激活isActive字段
        /// </summary>
        /// <param name="number"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public int uptIsActive(string number, SqlTransaction tran)
        {
            string sql = "update memberInfo set isActive=1 where number=@number";
            SqlParameter[] para = new SqlParameter[] 
           {
             new SqlParameter("@number", number) 
           };
            return DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
        }


        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        public List<Bsco_PaperType> GetCard()
        {
            List<Bsco_PaperType> list = new List<Bsco_PaperType>();
            Bsco_PaperType bp = null;
            string sql = "select * from dbo.bsco_PaperType order by id";
            SqlDataReader reader = DBHelper.ExecuteReader(sql, CommandType.Text);
            while (reader.Read())
            {
                bp = new Bsco_PaperType(Convert.ToInt32(reader["Id"]));
                //bp.PaperTypeCode = reader["Id"].ToString();
                bp.PaperType = reader["PaperType"].ToString().Trim();
                bp.PaperTypeCode = reader["id"].ToString().Trim();
                list.Add(bp);
            }
            reader.Close();
            return list;
        }


        /// <summary>
        /// 导出excel的dataTable
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <param name="condition"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public DataTable GetInfoAndOrder(string table, string column, string condition, string order)
        {
            string sql = "select " + column + " from " + table + " where " + condition + " order by " + order + " desc ";
            return DBHelper.ExecuteDataTable(sql);

        }
        /// <summary>
        /// 导出excel的dataTable(重载)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <param name="condition"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public DataTable GetInfoAndOrder(string table, string column, string order)
        {
            string sql = "select @column from @table order by @order desc";
            SqlParameter[] para = new SqlParameter[] 
           {
             new SqlParameter("@table", table),
             new SqlParameter("@column", column),
             new SqlParameter("@order", order)
           };
            return DBHelper.ExecuteDataTable(sql, para, CommandType.Text);

        }

        /// <summary>
        ///  从组合产品中获取所有小产品
        /// </summary>
        /// <param name="groupItemId"></param>
        /// <returns></returns>
        //public List<Other.OrderProduct> GetSamllItemList(string groupItemId) 
        //{
        //    List<Other.OrderProduct> list = new List<DAL.Other.OrderProduct>();
        //    SqlParameter[] para =

        //    {
        //      new SqlParameter("@list", groupItemId),
        //    };
        //    SqlDataReader reader = DBHelper.ExecuteReader("GetSmallProduct",para,CommandType.StoredProcedure);
        //    while (reader.Read()) 
        //    {
        //        Other.OrderProduct opt = new DAL.Other.OrderProduct();
        //        opt.id = Convert.ToInt32(reader["productid"]);
        //        list.Add(opt);
        //    }
        //    return list;

        //}


        /// <summary>
        ///  从组合产品中获取所有小产品
        /// </summary>
        /// <param name="groupItemId"></param>
        /// <returns></returns>
        public List<OrderProduct3> GetSamllItemList(string combineProductID)
        {
            List<OrderProduct3> list = new List<OrderProduct3>();
            string sql = @" 
                select a.productid as productid,a.PreferentialPrice as PreferentialPrice,a.PreferentialPv as PreferentialPv,b.Quantity as smallCount  
                from  product a,dbo.ProductCombineDetail b 
                where a.productid=b.SubProductID  and b.CombineProductID =@combineProductID";
            SqlParameter[] para =
            {
              new SqlParameter("@combineProductID", combineProductID),
            };
            SqlDataReader reader = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            while (reader.Read())
            {
                OrderProduct3 opt = new OrderProduct3();
                opt.Id = Convert.ToInt32(reader["productid"]);
                opt.Price = Convert.ToDouble(reader["PreferentialPrice"]);
                opt.Pv = Convert.ToDouble(reader["PreferentialPv"]);
                opt.Count = Convert.ToInt32(reader["smallCount"]);
                list.Add(opt);
            }
            reader.Close();
            return list;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="orderId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int UptStockTemp(string orderId)
        {
            string sql = @"
             select a.ActualStorage,b.productId,b.orderId
				FROM Stock a inner join MemberDetails b 
				ON a.productid=b.productid and a.storeid=b.storeid and orderid='orderId'";
            SqlParameter[] para =
            {
               new SqlParameter("@orderId", orderId)
              
            };
            return Convert.ToInt32(DBHelper.ExecuteNonQuery(sql, para, CommandType.Text));

        }

        public int GetRestCountInGroup(int ProductId, string orderId)
        {
            string sql = @"select isnull(sum(Quantity),0) from MemberDetails 
                where ProductID in( 
                select CombineProductID from ProductCombineDetail 
                where SubProductID=@ProductId) and orderId=@orderId";
            SqlParameter[] para =
            {
               new SqlParameter("@ProductId", ProductId),
               new SqlParameter("@orderId", orderId) 
              
            };

            return Convert.ToInt32(DBHelper.ExecuteScalar(sql, para, CommandType.Text));

        }



         

        public int updateStore11(string storeId, MemberDetailsModel opt, SqlTransaction tran)
        {

            string SQL = string.Format(@"UPDATE Stock SET  
         TotalOut = TotalOut +{0}, ActualStorage = ActualStorage -{1}-{4} WHERE ProductID = '{2}' And  StoreID = '{3}'", opt.Quantity, opt.Quantity, opt.ProductId, storeId, opt.IsInGroupItemCount);

            return DBHelper.ExecuteNonQuery(tran, SQL, null, CommandType.Text);
        }

        /// <summary>
        /// 批量注册检测
        /// </summary>
        /// <param name="allowCount">允许线数</param>
        /// <param name="registerExcept">注册日期</param>
        /// <param name="storeId">店编号</param>
        public void CheckGroup(int allowCount, int registerExcept, string storeId) 
        {
            SqlParameter[] para = 
            {
                new SqlParameter("@xianshu",allowCount),
                new SqlParameter("@mainnumber",DAL.CommonDataDAL.GetManageID(3)),
                new SqlParameter("@qishu",registerExcept),
                new SqlParameter("@storeNumber",storeId)
            };
             DBHelper.ExecuteNonQuery("Batchregistercheck",para,CommandType.StoredProcedure);

        }

        /// <summary>
        /// 更据orderId得到该单使用的货币
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int GetCurrency(string orderId) 
        {
            string sql = "select payCurrency from  memberOrder where orderId=@orderId";
             SqlParameter[] para =
            {
               new SqlParameter("@orderId", orderId)
              
            };
             return Convert.ToInt32(DBHelper.ExecuteScalar(sql,para,CommandType.Text));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="notEnoughMoney"></param>
        /// <returns></returns>
        public double ChangeNotEnoughMoney(string storeId,double notEnoughMoney) 
        {
            SqlParameter[] para =
            {
               new SqlParameter("@storeId", storeId),
               new SqlParameter("@laveAmount", notEnoughMoney)
            };
            return Convert.ToDouble(DBHelper.ExecuteScalar("changeNotEnoughMoney", para, CommandType.StoredProcedure));
           
        }


        /// <summary>
        /// 通过ID找汇率
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public decimal GetCurrencyById(string id) 
        {
            string sql = "select rate from  currency where id=@id";
            SqlParameter[] para =
            {
               new SqlParameter("@id", id),

            };
            return Convert.ToDecimal(DBHelper.ExecuteScalar(sql,para,CommandType.Text)); 
        }

        /// <summary>
        /// 检测是否是公司开的店
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public int IsDefaultStore(string storeId) 
        {
            string sql = "select count(*) from storeINfo where storeid=@storeId and defaultstore=1";
            SqlParameter[] para =
            {
               new SqlParameter("@storeId", storeId),

            };
            return Convert.ToInt32(DBHelper.ExecuteScalar(sql, para, CommandType.Text));
        }


        /// <summary>
        /// 零购注册时判断推荐人或安置人的注册期数是否小于选择的期数
        /// </summary>
        /// <param name="placement"></param>
        /// <param name="derict"></param>
        /// <param name="choseExcept"></param>
        /// <returns></returns>
        public int DOrPExcept(string placement,string derict,int choseExcept) 
        {
            string sql = "select count(expectNum) from memberinfo where number in (@placement,@derict) and expectNum<=@choseExcept";
            SqlParameter[] para =
            {
               new SqlParameter("@placement", placement),
               new SqlParameter("@derict", derict),
               new SqlParameter("@choseExcept", choseExcept)
            };
            return Convert.ToInt32(DBHelper.ExecuteScalar(sql, para, CommandType.Text));
        
        }

        /// <summary>
        /// 将config表字段jsflag清空以便重新结算
        /// </summary>
        /// <param name="except">最小期数</param>
        /// <returns></returns>
        public int UpConfigToZero(int ExpectNum, SqlTransaction tran) 
        {
            string sql = "update config set jsflag=0 where ExpectNum>=@ExpectNum";
            SqlParameter[] para = { new SqlParameter("@ExpectNum", ExpectNum) };
            return DBHelper.ExecuteNonQuery(tran,sql,para,CommandType.Text);
        }

        public int GetHavePlace(string number, SqlTransaction tran) 
        {
            string sql = "select count(*) from memberinfo where placement=@number";
            SqlParameter[] para = { new SqlParameter("@number", number) };
            return Convert.ToInt32(DBHelper.ExecuteScalar(tran, sql, para, CommandType.Text));
        }

        public int GetHaveDirect(string number, SqlTransaction tran)
        {
            string sql = "select count(*) from memberinfo where direct=@number";
            SqlParameter[] para = { new SqlParameter("@number", number) };
            return Convert.ToInt32(DBHelper.ExecuteScalar(tran, sql, para, CommandType.Text));
        }

        public int GetHaveStore(string number, SqlTransaction tran)
        {
            string sql = "select count(*) from storeinfo where number=@number";
            SqlParameter [] para={
                                     new SqlParameter("@number",number)
                                 };
            return Convert.ToInt32(DBHelper.ExecuteScalar(tran, sql, para, CommandType.Text));
        }

        public string DisTran(string paperType,int languageId) 
        {
            string sql = @"declare @lid2 varchar(50)
            select @lid2=languageCode from dbo.Language where id=@languageId
            if(@lid2='L001')
            select a.PaperTypeCode as 'PaperTypeCode' from bsco_PaperType a,T_translation b where a.id=b.primarykey and b.L001=@paperType
            if(@lid2='L002')
            select a.PaperTypeCode as 'PaperTypeCode' from bsco_PaperType a,T_translation b where a.id=b.primarykey and b.L002=@paperType
            if(@lid2='L003')
            select a.PaperTypeCode as 'PaperTypeCode' from bsco_PaperType a,T_translation b where a.id=b.primarykey and b.L003=@paperType";
            SqlParameter[] para = { new SqlParameter("@paperType", paperType),
                                   new SqlParameter("@languageId", languageId) };
            return Convert.ToString(DBHelper.ExecuteScalar(sql, para, CommandType.Text));
        }



        public List<CountryModel> GetCountry() 
        {
            List<CountryModel> list=new List<CountryModel>();
            CountryModel countryModel=null;
            SqlDataReader reader = DBHelper.ExecuteReader("Select id,name From country order by id");
            while(reader.Read()) 
            {
                countryModel = new CountryModel(Convert.ToInt32(reader["id"]));
                countryModel.Name = reader["name"].ToString();
                list.Add(countryModel);
            }
            reader.Close();
            return list;
        
        }


        public string GetCountryByBankId(string number) 
        {
            string sql = @"select name from country where id=
            (select countrycode from memberbank where bankCode=
            (select bankcode from memberinfo where number=@number))";
            SqlParameter[] para = { new SqlParameter("@number", number)};
            return Convert.ToString(DBHelper.ExecuteScalar(sql, para, CommandType.Text));
        }

        /// <summary>
        ///  获取身份证编码
        /// </summary>
        /// /// <returns></returns>
        public List<Bsco_PaperType> GetCardCode()
        {
            List<Bsco_PaperType> list = new List<Bsco_PaperType>();
            Bsco_PaperType bp = null;
            string sql = "select * from dbo.bsco_PaperType order by id";
            SqlDataReader reader = DBHelper.ExecuteReader(sql, CommandType.Text);
            while (reader.Read())
            {
                bp = new Bsco_PaperType(Convert.ToInt32(reader["Id"]));
                bp.PaperType = reader["PaperType"].ToString().Trim();
                bp.PaperTypeCode = reader["papertypecode"].ToString().Trim();
                list.Add(bp);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 拆分产品信息--报单信息
        /// 注：把组合产品拆分成单品
        /// </summary>
        /// <param name="ods">原产品信息</param>
        /// <returns>拆分后产品信息</returns>
        public static IList<MemberDetailsModel> GetNewOrderDetail(IList<MemberDetailsModel> ods)
        {
            IList<MemberDetailsModel> orderdetails = new List<MemberDetailsModel>();
            foreach (MemberDetailsModel od in ods)
            {
                if (ProductDAL.GetIsCombine(od.ProductId))
                {
                    IList<ProductCombineDetailModel> comDetails = ProductCombineDetailDAL.GetCombineDetil(od.ProductId);
                    foreach (ProductCombineDetailModel comDetail in comDetails)
                    {
                        int count = 0;
                        foreach (MemberDetailsModel detail in orderdetails)
                        {
                            if (detail.ProductId == comDetail.SubProductID)
                            {
                                detail.Quantity = (comDetail.Quantity * od.Quantity) + detail.Quantity;
                                //detail.NotEnoughProduct = (comDetail.Quantity * od.NotEnoughProduct) + detail.NotEnoughProduct;
                                count++;
                            }
                        }
                        if (count == 0)
                        {
                            MemberDetailsModel orderdetail = new MemberDetailsModel();
                            orderdetail.Quantity = comDetail.Quantity * od.Quantity;
                            orderdetail.ProductId = comDetail.SubProductID;
                            //orderdetail.NotEnoughProduct = comDetail.Quantity * od.NotEnoughProduct;
                            orderdetails.Add(orderdetail);
                        }
                    }
                }
                else
                {
                    int count = 0;
                    foreach (MemberDetailsModel detail in orderdetails)
                    {
                        if (detail.ProductId == od.ProductId)
                        {
                            detail.Quantity = od.Quantity + detail.Quantity;
                            //detail.NotEnoughProduct = od.NotEnoughProduct + detail.NotEnoughProduct;
                            count++;
                        }
                    }
                    if (count == 0)
                    {
                        MemberDetailsModel orderdetail = new MemberDetailsModel();
                        orderdetail.Quantity = od.Quantity;
                        //orderdetail.NotEnoughProduct = od.NotEnoughProduct;
                        orderdetail.ProductId = od.ProductId;
                        orderdetails.Add(orderdetail);
                    }

                }
            }
            return orderdetails;
        }

        public static int GetBiaoZhunBiZhong()
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("select top 1 standardMoney from currency"));
        }

        public static double GetHuiLv(string bizhong)
        {
            double hl = Convert.ToDouble(DAL.DBHelper.ExecuteScalar("select rate/(select rate from  currency where id=(select top 1 standardMoney from currency)) from currency where id='" + bizhong + "'"));
            return hl;
        }

        /// <summary>
        /// 修改网络使用银行
        /// </summary>
        /// <param name="number"></param>
        /// <param name="bankid"></param>
        /// <returns></returns>
        public static int SetNetCardid(string number, int bankid, string strnumber, string opernum, int type )
        {
            string sqlpro = "ModifyBank";
            SqlParameter[] sps = new SqlParameter[] { 
              new SqlParameter("@Number",number),
              new SqlParameter("@BankId",bankid),
                new SqlParameter("@Str",strnumber),
                     new SqlParameter("@OperateNum",opernum),
                     new SqlParameter("@type",type )
            };
            int res = Convert.ToInt32(DBHelper.ExecuteNonQuery(sqlpro, sps, CommandType.StoredProcedure));

            return res;
        }



        /// <summary>
        /// 设置发展商信誉额度
        /// </summary>
        /// <param name="number"></param>
        /// <param name="xinyu"></param>
        /// <returns></returns>
        public static int Setxinyuedu(string number, double xinyu, string douser, string doip, DateTime limitdate)
        {
            string sqlstrg = "Updatexinyue";

            SqlParameter[] sps = new SqlParameter[] {
             new SqlParameter("@number",number),
             new SqlParameter("@credlimit",xinyu),
            new  SqlParameter("@limitdate",limitdate),
             new SqlParameter("@douser",douser),
             new SqlParameter("@doip",doip)

            };

            int res = Convert.ToInt32(DBHelper.ExecuteNonQuery(sqlstrg, sps, CommandType.StoredProcedure));

            return res;
        }
    }
}
