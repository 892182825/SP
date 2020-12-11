using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Model;


/**
 *  
 *  创建人：郑华超
 *  创建时间：2009.8.30
 *  功能：增加订单的一系列存储过程
 */
namespace DAL
{
    public class AddOrderDataDAL
    {
        public Boolean AddFinalOrder(OrderFinalModel ofm)
        {
            string sql = "AddMemberOrderFinal";
            int res = 0;
            SqlParameter[] para = 
            {
                new SqlParameter("@res",res),
                new  SqlParameter("@Number",ofm.Number),
                new  SqlParameter("@Placement", ofm.Placement),
                new  SqlParameter("@Recommended", ofm.Direct),
                new  SqlParameter("@ExpectNum", ofm.ExpectNum),
                new  SqlParameter("@OrderID", ofm.OrderID),
                new  SqlParameter("@StoreID", ofm.StoreID),
                new  SqlParameter("@Name", ofm.Name),
                new  SqlParameter("@PetName", ofm.PetName),
                new  SqlParameter("@LoginPass", ofm.LoginPass),
                new  SqlParameter("@Advpass", ofm.AdvPass),
                new  SqlParameter("@LevelInt", ofm.LevelInt),
                new  SqlParameter("@RegisterDatec", ofm.RegisterDate),
                new SqlParameter("@Birthday", ofm.Birthday),
                new SqlParameter("@Sex", ofm.Sex),
                new SqlParameter("@HomeTele", ofm.HomeTele),
                new SqlParameter("@OfficeTele", ofm.OfficeTele),
                new SqlParameter("@MobileTele", ofm.MobileTele),
                new SqlParameter("@FaxTele", ofm.FaxTele),
                
                new SqlParameter("@CPCCode",ofm.CPCCode),
                new SqlParameter("@Address", ofm.Address),
                new SqlParameter("@PostalCode", ofm.PostalCode),
                new SqlParameter("@PaperTypeCode", ofm.PaperType.PaperTypeCode),
                new SqlParameter("@PaperNumber", ofm.PaperNumber),
                new SqlParameter("@BankCode", ofm.BankCode),
                new SqlParameter("@BankAddress" , ofm.BankAddress ),
                new SqlParameter("@BankCard", ofm.BankCard),
                
                new SqlParameter("@BCPCCode",ofm.BCPCCode),
                new SqlParameter("@BankBook", ofm.BankBook),
                new SqlParameter("@Remark", ofm.Remark),
                new SqlParameter("@ChangeInfo", ofm.ChangeInfo),
                new SqlParameter("@Photopath", ofm.PhotoPath),
                new SqlParameter("@Email",ofm.Email),
				new SqlParameter("@IsBatch",ofm.IsBatch),
				new SqlParameter("@Language",ofm.Language),
				new SqlParameter("@OperateIP",ofm.OperateIp),
				new SqlParameter("@OperaterNumber",ofm.OperaterNum),
				new SqlParameter("@District",ofm.District),
                new SqlParameter("@Answer",ofm.Answer),
                new SqlParameter("@Question",ofm.Question),
                new SqlParameter("@Err",ofm.Error),
                new SqlParameter("@BankBranchName",ofm.Bankbranchname),

               
								new SqlParameter("@TotalMoney",ofm.TotalMoney),
								new SqlParameter("@TotalPv", ofm.TotalPv),
								new SqlParameter("@PayExpect", ofm.PayExpect),
								new SqlParameter("@OrderExpect", ofm.OrderExpect),
								new SqlParameter("@IsAgain", ofm.IsAgain),
								new SqlParameter("@OrderDate",  ofm.OrderDate ),
													
								new SqlParameter("@DefrayState" ,ofm.DefrayState),
								new SqlParameter("@PayCurrency", ofm.PayCurrency),
								new SqlParameter("@PayMoney", ofm.PayMoney),
								new SqlParameter("@StandardCurrency" , ofm.StandardCurrency),		
								new SqlParameter("@StandardCurrencyMoney" ,ofm.StandardcurrencyMoney),
								
								new SqlParameter("@DefrayType" ,ofm.DefrayType),
								new SqlParameter("@CarryMoney",ofm.CarryMoney),
								new SqlParameter("@RemittancesId",ofm.RemittancesId),
								new SqlParameter("@ElectronicAccountId",ofm.ElectronicaccountId),
				                new SqlParameter("@ordertype",ofm.OrderType),
                                new SqlParameter("@CCPCCode" ,ofm.CCPCCode),
								new SqlParameter("@ConAddress",ofm.ConAddress),
								new SqlParameter("@ConTelphone",ofm.ConTelPhone),
								new SqlParameter("@ConMobilPhone",ofm.ConMobilPhone),
								new SqlParameter("@ConPost",ofm.ConPost),
								new SqlParameter("@Consignee",ofm.Consignee),
				                new SqlParameter("@ConZipCode",ofm.ConZipCode),
                                new SqlParameter("@IsReceivables",ofm.IsreceiVables),
                                new SqlParameter("@PayMentMoney",ofm.PaymentMoney),
                                new SqlParameter("@ReceivablesDate",DateTime.Now.ToUniversalTime()),
                                new SqlParameter("@EnoughProductMoney",ofm.EnoughProductMoney),
                                new SqlParameter("@LackProductMoney",ofm.LackProductMoney),
                                new SqlParameter("@SendWay",ofm.SendWay),
                                new SqlParameter("@Sendtype",ofm.Type),

                                new SqlParameter("@ProductIDList",ofm.ProductIDList),
                                new SqlParameter("@QuantityList",ofm.QuantityList),
                                new SqlParameter("@notEnoughProductList",ofm.NotEnoughProductList),
                                new SqlParameter("@assister",ofm.Assister),
                                new SqlParameter("@InvestJB",ofm.InvestJB),
                                new SqlParameter("@PriceJB",ofm.PriceJB)
                
            };

            try
            {
                para[0].Direction = ParameterDirection.Output;
                DBHelper.ExecuteDataTable(sql, para, CommandType.StoredProcedure);
                res = Convert.ToInt32(para[0].Value);
                if (res < 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        /// <summary>
        /// 添加会员报单，用于修改报单，注册报单不添加会员信息
        /// </summary>
        /// <param name="ofm"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public Boolean AddFinalOrderNoInfo(OrderFinalModel ofm, SqlTransaction tran)
        {
            string sql = "AddMemberOrderNoInfo";

            SqlParameter[] para = 
            {
                new  SqlParameter("@Number",ofm.Number),
                new  SqlParameter("@Placement", ofm.Placement),
                new  SqlParameter("@Recommended", ofm.Direct),
                new  SqlParameter("@ExpectNum", ofm.ExpectNum),
                new  SqlParameter("@OrderID", ofm.OrderID),
                new  SqlParameter("@StoreID", ofm.StoreID),
                new  SqlParameter("@Name", ofm.Name),
                new  SqlParameter("@PetName", ofm.PetName),
                new  SqlParameter("@LoginPass", ofm.LoginPass),
                new  SqlParameter("@Advpass", ofm.AdvPass),
                new  SqlParameter("@LevelInt", ofm.LevelInt),
                new  SqlParameter("@RegisterDatec", ofm.RegisterDate),
                new SqlParameter("@Birthday", ofm.Birthday),
                new SqlParameter("@Sex", ofm.Sex),
                new SqlParameter("@HomeTele", ofm.HomeTele),
                new SqlParameter("@OfficeTele", ofm.OfficeTele),
                new SqlParameter("@MobileTele", ofm.MobileTele),
                new SqlParameter("@FaxTele", ofm.FaxTele),
                
                new SqlParameter("@CPCCode",ofm.CPCCode),
                new SqlParameter("@Address", ofm.Address),
                new SqlParameter("@PostalCode", ofm.PostalCode),
                new SqlParameter("@PaperTypeCode", ofm.PaperType.PaperTypeCode),
                new SqlParameter("@PaperNumber", ofm.PaperNumber),
                new SqlParameter("@BankCode", ofm.BankCode),
                new SqlParameter("@BankAddress" , ofm.BankAddress ),
                new SqlParameter("@BankCard", ofm.BankCard),
                
                new SqlParameter("@BCPCCode",ofm.BCPCCode),
                new SqlParameter("@BankBook", ofm.BankBook),
                new SqlParameter("@Remark", ofm.Remark),
                new SqlParameter("@ChangeInfo", ofm.ChangeInfo),
                new SqlParameter("@Photopath", ofm.PhotoPath),
                new SqlParameter("@Email",ofm.Email),
				new SqlParameter("@IsBatch",ofm.IsBatch),
				new SqlParameter("@Language",ofm.Language),
				new SqlParameter("@OperateIP",ofm.OperateIp),
				new SqlParameter("@OperaterNumber",ofm.OperaterNum),
				new SqlParameter("@District",ofm.District),
                new SqlParameter("@Answer",ofm.Answer),
                new SqlParameter("@Question",ofm.Question),
                new SqlParameter("@Err",ofm.Error),
                new SqlParameter("@BankBranchName",ofm.Bankbranchname),

               
								new SqlParameter("@TotalMoney",ofm.TotalMoney),
								new SqlParameter("@TotalPv", ofm.TotalPv),
								new SqlParameter("@PayExpect", ofm.PayExpect),
								new SqlParameter("@OrderExpect", ofm.OrderExpect),
								new SqlParameter("@IsAgain", ofm.IsAgain),
								new SqlParameter("@OrderDate",  ofm.OrderDate ),
													
								new SqlParameter("@DefrayState" ,ofm.DefrayState),
								new SqlParameter("@PayCurrency", ofm.PayCurrency),
								new SqlParameter("@PayMoney", ofm.PayMoney),
								new SqlParameter("@StandardCurrency" , ofm.StandardCurrency),		
								new SqlParameter("@StandardCurrencyMoney" ,ofm.StandardcurrencyMoney),
								
								new SqlParameter("@DefrayType" ,ofm.DefrayType),
								new SqlParameter("@CarryMoney",ofm.CarryMoney),
								new SqlParameter("@RemittancesId",ofm.RemittancesId),
								new SqlParameter("@ElectronicAccountId",ofm.ElectronicaccountId),
				                new SqlParameter("@ordertype",ofm.OrderType),
                                new SqlParameter("@CCPCCode" ,ofm.CCPCCode),
								new SqlParameter("@ConAddress",ofm.ConAddress),
								new SqlParameter("@ConTelphone",ofm.ConTelPhone),
								new SqlParameter("@ConMobilPhone",ofm.ConMobilPhone),
								new SqlParameter("@ConPost",ofm.ConPost),
								new SqlParameter("@Consignee",ofm.Consignee),
				                new SqlParameter("@ConZipCode",ofm.ConZipCode),
                                new SqlParameter("@IsReceivables",ofm.IsreceiVables),
                                new SqlParameter("@PayMentMoney",ofm.PaymentMoney),
                                new SqlParameter("@ReceivablesDate",DateTime.Now.ToUniversalTime()),
                                new SqlParameter("@EnoughProductMoney",ofm.EnoughProductMoney),
                                new SqlParameter("@LackProductMoney",ofm.LackProductMoney),
                                new SqlParameter("@SendWay",ofm.SendWay),
                                new SqlParameter("@Sendtype",ofm.Type),

                                new SqlParameter("@ProductIDList",ofm.ProductIDList),
                                new SqlParameter("@QuantityList",ofm.QuantityList),
                                new SqlParameter("@notEnoughProductList",ofm.NotEnoughProductList),
                                new SqlParameter("@assister",ofm.Assister), 
                                new SqlParameter("@InvestJB",ofm.InvestJB),
                                new SqlParameter("@PriceJB",ofm.PriceJB)
                
            };

            try
            {
                DBHelper.ExecuteDataTable(tran, sql, para, CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                return false;
            }

            return true;



        }

        /// <summary>
        /// 是否该会员下是否有安置编号
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="placement">安置编号</param>
        /// <returns>检查结果</returns>
        public int CheckHavePlaced(string number, string placement)
        {
            string SQL = "select count(*) from MemberInfo where Placement=@Placement and Number<>@number  and memberstate=1 ";
            //参数
            SqlParameter[] para = 
              {
                new  SqlParameter("@Placement", placement),
                new  SqlParameter("@number",number)
              };
            //取得结果
            return (int)DBHelper.ExecuteScalar(SQL, para, CommandType.Text);
        }


        /// <summary>
        /// 是否该会员下是否有推荐编号
        /// </summary>
        /// <param name="number">推荐编号</param>
        /// <param name="placement">推荐编号</param>
        /// <returns>检查结果</returns>
        public int CheckHaveDirected(string number, string Direct)
        {
            string SQL = "select count(*) from MemberInfo where Direct=@Direct and Number<>@number";
            //参数
            SqlParameter[] para = 
              {
                new  SqlParameter("@Direct",Direct),
                new  SqlParameter("@number",number)
              };
            //取得结果
            return (int)DBHelper.ExecuteScalar(SQL, para, CommandType.Text);
        }



        /// <summary>
        /// 检查编号重复
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool CheckNumberTwice(string number)
        {
            string upt_SQL = "Select Number,PaperNumber From MemberInfo Where Number=@number";
            //参数
            SqlParameter[] para = 
            {
              new SqlParameter("@number",number)
            };

            SqlDataReader reader = DBHelper.ExecuteReader(upt_SQL, para, CommandType.Text);
            if (reader.Read())
            {
                return false;
            }
            reader.Close();
            return true;
        }


        /// <summary>
        /// 检查手机号重复
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool CheckTeleTwice(string txtTele)
        {
            string upt_SQL = "Select MobileTele,PaperNumber From MemberInfo Where MobileTele=@MobileTele";
            //参数
            SqlParameter[] para = 
            {
              new SqlParameter("@MobileTele",txtTele)
            };

            SqlDataReader reader = DBHelper.ExecuteReader(upt_SQL, para, CommandType.Text);
            if (reader.Read())
            {
                return false;
            }
            reader.Close();
            return true;
        }

        /// <summary>
        /// 检查编号重复
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool CheckNumberTwice(string number, int ID)
        {

            string add_SQL = "Select Number,PaperNumber From MemberInfo Where ID<> @ID and Number=@number";
            //参数
            SqlParameter[] para = 
            {
              new SqlParameter("@ID",number),
              new SqlParameter("@number",number)
            };
            SqlDataReader reader = DBHelper.ExecuteReader(add_SQL, para, CommandType.Text);
            if (reader.Read())
            {
                return false;
            }
            reader.Close();
            return true;
        }



        /// <summary>
        /// 查询昵称
        /// </summary>
        /// <param name="petName"></param>
        /// <returns></returns>
        public int GetPetName(string petName)
        {
            string SQL = "Select count(*) From MemberInfo Where PetName=@petName";
            SqlParameter[] para = 
              {
                new SqlParameter("@petName",petName)
              };
            return Convert.ToInt32(DBHelper.ExecuteScalar(SQL, para, CommandType.Text));

        }

        /// <summary>
        /// 查询昵称(重载)
        /// </summary>
        /// <param name="petName"></param>
        /// <returns></returns>
        public int GetPetName(string petName, int ID)
        {
            string SQL = "Select count(*) From MemberInfo Where ID<> @ID and PetName= @petName";
            SqlParameter[] para = 
              {
                new SqlParameter("@ID",ID),
                new SqlParameter("@petName",petName)
              };
            return Convert.ToInt32(DBHelper.ExecuteScalar(SQL, para, CommandType.Text));

        }

        /// <summary>
        /// 检查电子账户密码
        /// </summary>
        /// <param name="elcNumber">会员编号</param>
        /// <param name="elcPassWord">会员电子账户</param>
        /// <returns>检查结果</returns>
        public int GetElcPassWord(string elcNumber, string elcPassWord)
        {
            string SQL = "select count(1) from memberInfo where number= @elcNumber and advpass=@elcPassWord";
            SqlParameter[] para = 
              {
                new SqlParameter("@elcNumber",elcNumber),
                new SqlParameter("@elcPassWord",elcPassWord)
              };
            return Convert.ToInt32(DBHelper.ExecuteScalar(SQL, para, CommandType.Text));

        }



        /// <summary>
        /// 检查店编号是否存在,返回false 表示已存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool CheckExistsStoreinfo(string storeid)
        {
            string sql = "select count(0) from storeinfo where storeid=@StoreID ";
            SqlParameter[] parm = { new SqlParameter("@StoreID", SqlDbType.VarChar, 40) };
            parm[0].Value = storeid;
            int result = (int)DBHelper.ExecuteScalar(sql, parm, CommandType.Text);
            if (result > 0)
                return false;
            else return true;
        }
        /// <summary>
        /// 会员报单底线
        /// </summary>
        /// <returns></returns>
        public object GetOrderBaseLine()
        {
            string sql = "select OrderBaseLine from MemOrderLine";
            return DBHelper.ExecuteScalar(sql);
        }
        /// <summary>
        /// 获取语言
        /// </summary>
        /// <returns></returns>
        public DataTable GetLanguage()
        {
            string sql = "select id,name from language";
            return DBHelper.ExecuteDataTable(sql);
        }



        ///// <summary>
        ///// 检查编号重复
        ///// </summary>
        ///// <param name="number"></param>
        ///// <returns></returns>
        //public int CheckNumberTwice( string placement)
        //{
        //   string SQL = "Select COUNT(ID) From MemberInfo Where number=@placement";
        //}

        ///// <summary>
        ///// 检查编号重复
        ///// </summary>
        ///// <param name="number"></param>
        ///// <returns></returns>

        //public int CheckNumberTwice(string Recommended) 
        //{

        //    string SQL = "Select  COUNT(ID) From MemberInfo Where number=@Recommended";
        //}










        /// <summary>
        /// 调用存储过程js_updateNew
        /// </summary>
        /// <param name="number"></param>
        /// <param name="Placement"></param>
        /// <param name="Recommended"></param>
        /// <param name="zongPv"></param>
        /// <param name="totalPv"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int UpdateNew(MemberInfoModel model, int flag, SqlTransaction tran)
        {
            SqlParameter[] para =  
            {
               new SqlParameter("@Number",model.Number),
               new SqlParameter("@Placement",model.Placement),
			   new SqlParameter("@Direct",model.Direct),
			   new SqlParameter("@CurrentOneMark",model.TotalPv),
			   new SqlParameter("@oldfenshu",model.TotalPv),
			   new SqlParameter("@flag",flag)
             
            };

            return (int)DBHelper.ExecuteScalar(tran, "js_updatenew", para, CommandType.StoredProcedure);

        }

        /// <summary>
        /// 调用存储过程Del_Horder
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="orderID"></param>
        /// <param name="storeID"></param>
        /// <param name="opnum">操作编号</param>
        /// <param name="opip">操作IP</param>
        /// <returns></returns>
        public int Del_Horder(SqlTransaction tran, string orderID, string storeID, string opnum, string opip)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@OrderID",orderID),
                new SqlParameter("@StoreID",storeID),
                new SqlParameter("@res",0),
                new SqlParameter("@opnum",opnum),
                new SqlParameter("@opip",opip)
            };
            para[2].Direction = ParameterDirection.Output;
            int result = DBHelper.ExecuteNonQuery(tran, "Delete_H_Order", para, CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// 删除会员报单，用于报单修改，不删除会员信息
        /// </summary>
        /// <returns></returns>
        public int Del_Horder(string orderID, SqlTransaction tran)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@OrderID",orderID)
            };
            int result = DBHelper.ExecuteNonQuery(tran, "DeleteMemberOrder", para, CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// 错误检查
        /// </summary>
        /// <param name="number"></param>
        /// <returns>返回错误字符串</returns>
        public int Check_WhenDelete(SqlTransaction tran, string number)
        {
            //SqlTransaction tran = null;
            SqlParameter[] para = { new SqlParameter("@bh", SqlDbType.VarChar, 12) };
            para[0].Value = number;
            return DBHelper.ExecuteNonQuery(tran, "Check_WhenDelete", para, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 错误检查(重载)
        /// </summary>
        /// <param name="number"></param>
        /// <returns>返回错误字符串</returns>
        public int Check_WhenDelete(string number)
        {
            //SqlTransaction tran = null;
            SqlParameter[] para = { new SqlParameter("@bh", SqlDbType.VarChar, 12) };
            para[0].Value = number;
            return DBHelper.ExecuteNonQuery("Check_WhenDelete", para, CommandType.StoredProcedure);
        }




        /// <summary>
        ///  执行存储过程UP_MemberOrder_ADD
        /// </summary>
        /// <param name="model"></param>
        /// <returns>返回成功与否</returns>
        public int INSERT_H_Order(MemberOrderModel model, SqlTransaction tran)
        {
            string Insert_Sql_Order = @"INSERT INTO [MemberOrder]([Number],[OrderID],[StoreID],[TotalMoney],[TotalPv],[CarryMoney],[OrderExpectNum],[PayExpectNum],[IsAgain],[OrderDate],[Error],[Remark],[DefrayState],[Consignee],[CCPCCode],[ConAddress],[ConZipCode],[ConTelphone],[ConMobilPhone],[ConPost],[DefrayType],[PayMoney],[PayCurrency],[StandardCurrency],[StandardCurrencyMoney],[OperateIP],[OperateNum],[RemittancesId],[ElectronicAccountId],[ordertype],[IsReceivables],[PayMentMoney],[ReceivablesDate],[EnoughProductMoney],[LackProductMoney],[SendWay],sendtype)
                                            VALUES(	@Number,@OrderID,@StoreID,@TotalMoney,@TotalPv,@CarryMoney,@OrderExpect,@PayExpect,@IsAgain,@OrderDate,@Err,@Remark,@DefrayState,@Consignee,@CCPCCode,@ConAddress,@ConZipCode,@ConTelphone,@ConMobilPhone,@ConPost,@DefrayType,@PayMoney,@PayCurrency,@StandardCurrency,@StandardCurrencyMoney,@OperateIP,@OperateNumber,@RemittancesId,@ElectronicAccountId,@ordertype,@IsReceivables,@PayMentMoney,@ReceivablesDate,@EnoughProductMoney,@LackProductMoney,@SendWay,@sendtype)";
            SqlParameter[] para = 
							{	
							    new SqlParameter("@Number", model.Number),
								new SqlParameter("@OrderID", model.OrderId),
								new SqlParameter("@StoreID", model.StoreId),
								new SqlParameter("@TotalMoney",model.TotalMoney),
								new SqlParameter("@TotalPv", model.TotalPv),
								new SqlParameter("@PayExpect", model.PayExpect),
								new SqlParameter("@OrderExpect", model.OrderExpect),
								new SqlParameter("@IsAgain", model.IsAgain),
								new SqlParameter("@OrderDate",  model.OrderDate ),
								new SqlParameter("@Err", model.Err),
								new SqlParameter("@Remark" , model.Remark),						
								new SqlParameter("@DefrayState" ,model.DefrayState),
								new SqlParameter("@PayCurrency", model.PayCurrency),
								new SqlParameter("@PayMoney", model.PayMoney),
								new SqlParameter("@StandardCurrency" , model.StandardCurrency),		
								new SqlParameter("@StandardCurrencyMoney" ,model.StandardcurrencyMoney),
								new SqlParameter("@OperateIP",model.OperateIp),
								new SqlParameter("@OperateNumber",model.OperateNumber),
								new SqlParameter("@DefrayType" ,model.DefrayType),
								new SqlParameter("@CarryMoney",model.CarryMoney),
								new SqlParameter("@RemittancesId",model.RemittancesId),
								new SqlParameter("@ElectronicAccountId",model.ElectronicaccountId),
				                new SqlParameter("@ordertype",model.OrderType),
                                new SqlParameter("@CCPCCode" ,CommonDataDAL.GetCPCCode(model.ConCity)),
								new SqlParameter("@ConAddress",model.ConAddress),
								new SqlParameter("@ConTelphone",model.ConTelPhone),
								new SqlParameter("@ConMobilPhone",model.ConMobilPhone),
								new SqlParameter("@ConPost",model.ConPost),
								new SqlParameter("@Consignee",model.Consignee),
				                new SqlParameter("@ConZipCode",model.ConZipCode),
                                new SqlParameter("@IsReceivables",model.IsreceiVables),
                                new SqlParameter("@PayMentMoney",model.PaymentMoney),
                                new SqlParameter("@ReceivablesDate",DateTime.Now),
                                new SqlParameter("@EnoughProductMoney",model.EnoughProductMoney),
                                new SqlParameter("@LackProductMoney",model.LackProductMoney),
                                new SqlParameter("@SendWay",model.SendWay),
                                    new SqlParameter("@sendtype",model.SendType)
						
							};
            return DBHelper.ExecuteNonQuery(tran, Insert_Sql_Order, para, CommandType.Text);

        }


        /// <summary>
        /// 执行存储过程insert_MemberOrderDetails
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int insert_MemberOrderDetails(MemberOrderModel memberOrderModel, MemberDetailsModel opt, SqlTransaction tran)
        {
            SqlParameter[] para = 
			{
               new SqlParameter("@Number",memberOrderModel.Number),
               new SqlParameter("@OrderID",memberOrderModel.OrderId),
               new SqlParameter("@StoreID",memberOrderModel.StoreId),
               new SqlParameter("@ProductID",opt.ProductId),
               new SqlParameter("@Quantity",opt.Quantity),
               new SqlParameter("@Price",opt.Price),
               new SqlParameter("@Pv",opt.Pv),
               new SqlParameter("@ExpectNum",memberOrderModel.OrderExpect),
               new SqlParameter("@IsAgain",memberOrderModel.IsAgain),
               new SqlParameter("@Remark",""),  
               new SqlParameter("@OrderDate",memberOrderModel.OrderDate),
               new SqlParameter("@notEnoughProduct",opt.NotEnoughProduct)//,
             
            };
            return DBHelper.ExecuteNonQuery(tran, "insert_MemberOrderDetails", para, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 插入订单明细表数据
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="orderDetailModel"></param>
        /// <returns></returns>
        public bool AddOrderGoodsDetail(SqlTransaction tr, MemberDetailsModel orderDetailModel, string StoreOrderID, string storeId, int ExpectNum)
        {
            string sql = "insert into OrderGoodsDetail(StoreOrderID,StoreID,ProductID,ExpectNum,Quantity,Price,Pv)" +
                "values(@StoreOrderID,@StoreID,@ProductID,@ExpectNum,@Quantity,@Price,@Pv)";
            SqlParameter[] ps = new SqlParameter[]
            {
                new SqlParameter("@StoreOrderID",StoreOrderID),                       //订单号
                new SqlParameter("@StoreID",storeId),                //店铺编号
                new SqlParameter("@ProductID",orderDetailModel.ProductId),            //产品ID
                new SqlParameter("@ExpectNum",ExpectNum),            //期数
                new SqlParameter("@Quantity",orderDetailModel.NotEnoughProduct),              //原来订货数量
                new SqlParameter("@Price",orderDetailModel.Price),                    //产品单价
                new SqlParameter("@Pv",orderDetailModel.Pv)                           //产品积分
            };
            return ((int)DBHelper.ExecuteNonQuery(tr, sql, ps, CommandType.Text)) > 0;
        }

        /// <summary>
        /// 添加订货单信息
        /// </summary>
        /// <param name="item">订货单对象</param>
        /// <param name="tr">事务参数</param>
        /// <returns>是否插入成功</returns>
        public Boolean AddOrderGoods(OrderGoodsMedel item, string OrderID, SqlTransaction tr, int ActiveFlag)
        {
            string sql = "insert into OrderGoods (StoreID,OrderGoodsID,OutStorageOrderID,TotalMoney,TotalPV,InceptAddress,InceptPerson,PostalCode,Telephone,OrderType,Description,OrderDateTime,PayMentDateTime,ExpectNum,TotalCommision,GoodsQuantity,Weight,CPCCode,IscheckOut,OperateIP,PayType,PayCurrency,PayMoney,ActiveFlag,SendWay)"
                        + "values(@StoreID,@OrderGoodsID,@OutStorageOrderID,@TotalMoney,@TotalPV,@InceptAddress,@InceptPerson,@PostalCode,@Telephone,@OrderType,@Description,@OrderDateTime,@PayMentDateTime,@ExpectNum,@TotalCommision,@GoodsQuantity,@Weight,@CPCCode,@IscheckOut,@OperateIP,@PayType,@PayCurrency,@PayMoney,@ActiveFlag,@SendWay)";
            SqlParameter[] ps = new SqlParameter[] 
            {
                new SqlParameter("@StoreID",item.StoreId),                                //店铺ID
                new SqlParameter("@OrderGoodsID",item.OrderGoodsID),                      //订单号
                new SqlParameter("@OutStorageOrderID",OrderID),                           //会员报单号
                new SqlParameter("@TotalMoney",item.TotalMoney),                          //订单总金额
                new SqlParameter("@TotalPV",item.TotalPv),                                //订单总积分
                new SqlParameter("@InceptAddress",item.InceptAddress),                    //收货人地址
                new SqlParameter("@InceptPerson",item.InceptPerson),                      //收货人姓名
                new SqlParameter("@PostalCode",item.PostalCode),                          //收货人邮编
                new SqlParameter("@Telephone",item.Telephone),                            //收货人电话
                new SqlParameter("@OrderType",item.OrderType),                            //订单类型
                new SqlParameter("@Description",item.Description),                        //描述
                new SqlParameter("@OrderDateTime",item.OrderDatetime),                    //订单日期
                new SqlParameter("@ExpectNum",item.ExpectNum),                            //期数
                new SqlParameter("@TotalCommision",item.TotalCommision),                  //手续费
                new SqlParameter("@GoodsQuantity",item.GoodsQuantity),                    //货物数量
                new SqlParameter("@Carriage",item.Carriage),                              //运费
                new SqlParameter("@Weight",item.Weight),                                  //重量
                new SqlParameter("@CPCCode",CommonDataDAL.GetCPCCode(item.City)),         //国家省份城市
                new SqlParameter("@IscheckOut",item.IscheckOut),                          //是否支付
                new SqlParameter("@OperateIP",item.OperateIP),                            //操作者IP
                new SqlParameter("@PayMentDateTime",DateTime.Now.ToLongTimeString()),
                new SqlParameter("@PayType",item.PayType),                                 //支付类型
                new SqlParameter("@PayCurrency",item.PayCurrency),
                new SqlParameter("@PayMoney",item.PayMoney),
                new SqlParameter("@ActiveFlag",ActiveFlag),
                new SqlParameter("@SendWay" ,item.SendWay)

            };
            return DBHelper.ExecuteNonQuery(tr, sql, ps, CommandType.Text) > 0;
        }


        ///<summary>
        ///更新公司逻辑库存
        /// </summary>
        public int updateLogicProductInventory(MemberDetailsModel opt, SqlTransaction tran)
        {
            //处理公司逻辑库存
            string sqlStr = string.Format(@"Update LogicProductInventory Set TotalOut=TotalOut+{0} Where ProductID = '{1}'", opt.NotEnoughProduct, opt.ProductId);
            int count1 = (int)DBHelper.ExecuteNonQuery(tran, sqlStr, null, CommandType.Text);
            return count1;
        }

        ///<summary>
        ///更新公司逻辑库存
        /// </summary>
        public int updateLogicProductInventory(string orderid, SqlTransaction tran)
        {
            //处理公司逻辑库存
            string sqlpro = "Upcomanylogiststock";
            SqlParameter[] sps = new SqlParameter[] { 
                new SqlParameter("@orderid", SqlDbType.VarChar, 100),
                new SqlParameter("@uptype", SqlDbType.Int, 4)
            };
            sps[0].Value = orderid;
            sps[1].Value = 0;
            int count1 = (int)DBHelper.ExecuteNonQuery(tran, sqlpro, sps, CommandType.StoredProcedure);
            return count1;
        }

        /// <summary>
        /// 更新店库存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int updateStore(string storeId, MemberDetailsModel opt, SqlTransaction tran)
        {
            string SQL = null;
            int count2 = 0;
            if (opt.NotEnoughProduct < 0)
            {
                SQL = string.Format(@"UPDATE Stock SET  
         TotalOut = TotalOut +{0}, ActualStorage = ActualStorage -{1} WHERE ProductID = '{2}    ' And  StoreID = '{3}'", opt.Quantity, opt.Quantity, opt.ProductId, storeId);
            }
            else
            {

                SQL = string.Format(@"UPDATE Stock SET  
                 TotalOut = TotalOut +{0}, ActualStorage = ActualStorage -{1}+{4},inwaycount=inwaycount+{4}, LackTotalNumber=LackTotalNumber+{4} WHERE ProductID = '{2}' And  StoreID = '{3}'", opt.Quantity, opt.Quantity, opt.ProductId, storeId, opt.NotEnoughProduct);

            }

            count2 = (int)DBHelper.ExecuteNonQuery(tran, SQL, null, CommandType.Text);
            return count2;
        }

        /// <summary>
        /// 更新店库存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int updateStoreL(SqlTransaction tran, IList<MemberDetailsModel> list)
        {
            int count2 = 0;
            foreach (Model.MemberDetailsModel md in list)
            {
                //处理公司逻辑库存
                if (md.NotEnoughProduct >= 0)
                {
                    count2 = updateLogicProductInventory(md, tran);
                }
            }

            return count2;
        }
        /// <summary>
        ///  //处理公司逻辑库存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int updateStoreL(SqlTransaction tran, string orderid)
        {
            return updateLogicProductInventory(orderid, tran);
        }



        /// <summary>
        /// 更新店库存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int updateStore4(string storeId, Other.OrderProduct opt, SqlTransaction tran)
        {

            string SQL = string.Format(@"UPDATE Stock SET  
         TotalOut = TotalOut +{0}, ActualStorage = ActualStorage -{1}-{4} WHERE ProductID = '{2}' And  StoreID = '{3}'", opt.count, opt.count, opt.id, storeId, opt.isInGroupItemCount);

            return DBHelper.ExecuteNonQuery(tran, SQL, null, CommandType.Text);
        }

        public int updateStore5(string storeId, Other.OrderProduct opt, SqlTransaction tran)
        {
            string SQL = string.Format(@"UPDATE Stock SET ActualStorage=ActualStorage-{1} WHERE ProductID='{2}' And StoreID='{3}'", opt.count, opt.count, opt.id, storeId);
            return DBHelper.ExecuteNonQuery(tran, SQL, null, CommandType.Text);
        }

        public int updateStore9(string storeId, OrderProduct3 opt, SqlTransaction tran)
        {
            string SQL = string.Format(@"UPDATE Stock SET ActualStorage=ActualStorage-{0} WHERE ProductID='{1}' And StoreID='{2}'", opt.Count, opt.Id, storeId);
            return DBHelper.ExecuteNonQuery(tran, SQL, null, CommandType.Text);
        }

        public int updateStore10(string storeId, OrderProduct3 opt, SqlTransaction tran)
        {
            string SQL = string.Format(@"UPDATE Stock SET ActualStorage=ActualStorage +{0} WHERE ProductID= 1} And StoreID='{2}'", opt.Count, opt.Id, storeId);

            return DBHelper.ExecuteNonQuery(tran, SQL, null, CommandType.Text);
        }
        /// <summary>
        /// 将不够的货物记录负数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int updateStore2(string storeId, MemberDetailsModel opt, SqlTransaction tran)
        {
            string SQL_INSERT_Stock = string.Format("INSERT INTO Stock( StoreID, ProductID, TotalIn, TotalOut,ActualStorage, HasOrderCount,LackTotalNumber,inwaycount) VALUES('{0}',{1},0,{2},{3}+{4},0,{4},{4}) ", storeId, opt.ProductId, opt.Quantity, opt.Quantity * (-1), opt.Quantity);
            return DBHelper.ExecuteNonQuery(tran, SQL_INSERT_Stock);
        }

        /// <summary>
        /// 将不够的货物记录负数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int updateStore2(MemberDetailsModel model, SqlTransaction tran)
        {
            string SQL_INSERT_Stock = string.Format("INSERT INTO Stock( StoreID, ProductID, TotalIn, TotalOut,ActualStorage, HasOrderCount,LackTotalNumber,inwaycount) VALUES('{0}',{1},0,{2},{3}+{4},0,{4},{4}) ", model.StoreId, model.ProductId, model.Quantity, model.Quantity * (-1), model.Quantity);
            return DBHelper.ExecuteNonQuery(tran, SQL_INSERT_Stock);
        }


        /// <summary>
        /// 更新店铺报单的总计费用
        /// </summary>
        /// <param name="model">StoreInfoModel类对象</param>
        /// <returns>返回结果</returns>
        public int updateStore3(string storeid, SqlTransaction tran, double zongjin)
        {
            string SQL_UPDATE_TotalMemberOrderMoney = @"UPDATE storeInfo SET TotalOrderGoodMoney = TotalOrderGoodMoney + @zongjing  WHERE StoreID = @storid";
            SqlParameter[] para = 
            {
                new SqlParameter("@zongjing",zongjin),
                new SqlParameter("@storid",storeid)
            };
            return DBHelper.ExecuteNonQuery(tran, SQL_UPDATE_TotalMemberOrderMoney, para, CommandType.Text);
        }


        /// <summary>
        /// 更新店铺报单的总计费用
        /// </summary>
        /// <param name="model">StoreInfoModel类对象</param>
        /// <returns>返回结果</returns>
        public int updateStore4(string storeID, SqlTransaction tran, double zongjin)
        {
            string SQL_UPDATE_TotalMemberOrderMoney = @"UPDATE storeInfo SET TotalMemberOrderMoney = TotalMemberOrderMoney + @zongjing  WHERE StoreID = @storid";
            SqlParameter[] para = 
            {
                new SqlParameter("@zongjing",zongjin),
                new SqlParameter("@storid",storeID)
            };
            return DBHelper.ExecuteNonQuery(tran, SQL_UPDATE_TotalMemberOrderMoney, para, CommandType.Text);
        }

        /// <summary>
        /// 响应页面验证按钮的推荐姓名验证
        /// </summary>
        /// <param name="DirectNumber">推荐姓名</param>
        /// <returns>返回验证结果</returns>
        public string DrictCheck(string DirectNumber)
        {
            //验证信息
            string info = null;
            string sql = "select [Name] from h_info where bianhao=@DirectNumber";
            SqlParameter[] para = 
			{
                new SqlParameter("@DirectNumber",DirectNumber)
            };
            SqlDataReader reader = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            if (reader.Read())
            {
                info = reader[0].ToString().Trim();
            }
            else
            {
                info = null;
            }
            reader.Close();
            //返回验证信息
            return info;
        }
        /// <summary>
        /// 响应页面验证按钮的安置姓名验证
        /// </summary>
        /// <param name="DirectNumber">安置姓名</param>
        /// <returns>返回验证结果</returns>
        public string DrictCheckPlaceMent(string placeNum)
        {
            string info = null;
            string sql = "select [Name] from h_info where bianhao=@placeNum";
            SqlParameter[] para = 
			{
              new SqlParameter("@placeNum",placeNum)
            };
            SqlDataReader reader = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            if (reader.Read())
            {
                info = reader[0].ToString().Trim();
            }
            else
            {
                info = null;
            }
            reader.Close();
            return info;
        }

        /// <summary>
        ///  获取不是组合商品的商品对象集合
        /// </summary>
        /// <returns>商品对象集合</returns>
        public List<ProductModel> GetProductList()
        {
            //声明集合类
            List<ProductModel> list = new List<ProductModel>();

            string sql = "Select IsCombineProduct,BigProductUnitID,SmallProductUnitID, BigSmallMultiple,PreferentialPrice,PreferentialPV , ProductID, ProductName ,Weight From Product Where IsFold = 0";
            SqlDataReader reader = DBHelper.ExecuteReader(sql);
            //循环赋值加入集合类
            while (reader.Read())
            {
                ProductModel model = new ProductModel();
                model.BigProductUnitID = Convert.ToInt32(reader["BigProductUnitID"]);
                model.SmallProductUnitID = Convert.ToInt32(reader["SmallProductUnitID"]);
                model.BigSmallMultiple = Convert.ToInt32(reader["BigSmallMultiple"]);
                model.PreferentialPrice = Convert.ToDecimal(reader["PreferentialPrice"]);
                model.PreferentialPV = Convert.ToDecimal(reader["PreferentialPV"]);
                model.ProductID = Convert.ToInt32(reader["ProductID"]);
                model.ProductName = Convert.ToString(reader["ProductName"]);
                model.Weight = Convert.ToDecimal(reader["Weight"]);
                model.IsCombineProduct = Convert.ToInt32(reader["IsCombineProduct"]);
                list.Add(model);
            }
            reader.Close();
            //返回集合类
            return list;
        }


        /// <summary>
        /// 调用dbo.js_addnew
        /// </summary>
        /// <param name="model">MemberInfoModel类对象</param>
        /// <param name="zongPv">新个分数，对应H_ORDER.TOTALPV</param>
        /// <returns>返回执行结果</returns>
        public int Upt_UpdateNew(MemberInfoModel model, int flag, SqlTransaction tran)
        {

            SqlParameter[] para =
            {
                new  SqlParameter("@Number",model.Number),
                new  SqlParameter("@Placement",model.Placement),
                new  SqlParameter("@Direct",model.Direct),
                new  SqlParameter("@CurrentOneMark",model.TotalPv),
                new  SqlParameter("@ExpectNum",model.ExpectNum),
                new  SqlParameter("@flag",flag)
            };
            return (int)DBHelper.ExecuteNonQuery(tran, "js_addnew", para, CommandType.StoredProcedure);

        }

        /// <summary>
        /// 调用dbo.js_addnew
        /// </summary>
        /// <param name="model">MemberInfoModel类对象</param>
        /// <param name="zongPv">新个分数，对应H_ORDER.TOTALPV</param>
        /// <returns>返回执行结果</returns>
        public int Upt_UpdateNew1(MemberInfoModel model, SqlTransaction tran)
        {

            SqlParameter[] para =
            {
                new  SqlParameter("@Number",model.Number),
                new  SqlParameter("@CurrentOneMark",model.TotalPv),
                new  SqlParameter("@ExpectNum",model.ExpectNum)
            };
            return (int)DBHelper.ExecuteNonQuery("js_sure", para, CommandType.StoredProcedure);

        }

        ///// <summary>
        ///// 得到支付金额
        ///// </summary>
        ///// <param name="number">会员编号</param>
        ///// <returns>支付金额</returns>
        //public double  GetPayMoney(string number,SqlTransaction tran) 
        //{
        //    string SQL = "select isnull((Jackpot-ECTPay-ReleaseMoney-Out),0) from MemberInfo where Number=@number";
        //    //参数
        //    SqlParameter[] para = 
        //    {
        //        new SqlParameter("@number",number)

        //    };
        //    return Convert.ToDouble(DBHelper.ExecuteScalar(tran,SQL,para,CommandType.Text));
        //}


        /// <summary>
        /// 进入级别表
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="Jb">级别序号</param>
        /// <param name="currentExpetNum">当期期数</param>
        /// <returns>返回结果</returns>
        public int Add_P_jb(string number, int Jb, int currentExpetNum)
        {
            SqlParameter[] para = 
            {
            
                new SqlParameter("@Number",number),
                new SqlParameter("@LevelNum",Jb),
                new SqlParameter("@ExpectNum",currentExpetNum)
            };
            return DBHelper.ExecuteNonQuery("p_jb", para, CommandType.StoredProcedure);
        }





        /// <summary>
        /// 店铺报单时更新h_order中的收货信息
        /// </summary>
        /// <param name="ConCountry">国家</param>
        /// <param name="ConProvince">省市</param>
        /// <param name="ConCity">城市</param>
        /// <param name="ConAddress">地址</param>
        /// <param name="OrderID">报单编号</param>
        public int UpdateMemberOrderReceiveInfo(string ConCountry, string ConProvince, string ConCity, string ConAddress, string OrderID)
        {
            string SQL = "update MemberOrder set ConCountry=@ConCountry,ConProvince=@ConProvince,ConCity=@ConCity,ConAddress=@ConAddress where OrderID=@OrderID";
            System.Data.SqlClient.SqlParameter[] para = new SqlParameter[]
					{
						new System .Data .SqlClient .SqlParameter ("@ConCountry",System.Data .SqlDbType .VarChar ),
						new System .Data .SqlClient .SqlParameter ("@ConProvince",System.Data .SqlDbType .VarChar ),
						new System .Data .SqlClient .SqlParameter ("@ConCity",System.Data .SqlDbType .VarChar ),
						new System .Data .SqlClient .SqlParameter ("@ConAddress",System.Data .SqlDbType .VarChar ),
				        new System .Data .SqlClient .SqlParameter ("@OrderID",System.Data .SqlDbType .VarChar )
					};
            para[0].Value = ConCountry;
            para[1].Value = ConProvince;
            para[2].Value = ConCity;
            para[3].Value = ConAddress;
            para[4].Value = OrderID;

            int result = DBHelper.ExecuteNonQuery(SQL, para, System.Data.CommandType.Text);

            return result;
        }


        /// <summary>
        /// 跟新收货地址
        /// </summary>
        /// <param name="storeID">店ID</param>
        public int GetAddressInfo(string storeID, string orderId)
        {
            string ConCountry = "", ConProvince = "", ConCity = "", ConAddress = "";
            //查找地址
            string sql2 = "select ";


            string SQL = "select Country,Province,City,StoreAddress from StoreInfo where StoreID=@storeID";
            SqlParameter[] para = 
            { 
              new SqlParameter("@storeID",storeID)
            
            };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            if (reader.Read())
            {
                ConCountry = reader["Country"].ToString();
                ConProvince = reader["Province"].ToString();
                ConCity = reader["City"].ToString();
                ConAddress = reader["StoreAddress"].ToString();
            }
            reader.Close();
            //更新地址
            return UpdateMemberOrderReceiveInfo(ConCountry, ConProvince, ConCity, ConAddress, orderId);
        }

        /// <summary>
        /// 判断同名不同网情况
        /// </summary>
        /// <param name="strFirstIDNumber">最早编号</param>
        /// <param name="typeName">安置或推荐</param>
        /// <returns></returns>
        public string GetSameNameInDifNet(string strFirstIDNumber, string typeName)
        {
            string SQL = "SELECT top 1 " + typeName + " FROM MemberInfo WHERE Number = @Number  ";

            SqlParameter[] para = 
            { 
              new SqlParameter("@Number",strFirstIDNumber)
            
            };
            return Convert.ToString(DBHelper.ExecuteScalar(SQL));
        }



        /// <summary>
        /// 使用存储过程registercheck
        /// </summary>
        /// <param name="Number"></param>
        /// <param name="Placement"></param>
        /// <param name="Recommended"></param>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        public string CheckMemberInProc(string Number, string Placement, string Direct,
            string StoreID, string info)
        {

            SqlParameter[] para =
            {  new SqlParameter("@Number",SqlDbType.VarChar,20),		
               new SqlParameter("@Placement",SqlDbType.VarChar,20),
			   new SqlParameter("@Direct",SqlDbType.VarChar,20),
			   new SqlParameter("@storeid",SqlDbType.VarChar,20),
			   new SqlParameter("@checkinfo",SqlDbType.VarChar,40)
								 };

            para[0].Value = Number;
            para[1].Value = Placement;
            para[2].Value = Direct;
            para[3].Value = StoreID;
            para[4].Value = info;
            para[4].Direction = System.Data.ParameterDirection.Output;
            DBHelper.ExecuteNonQuery("registercheck", para, CommandType.StoredProcedure);
            info = para[4].Value.ToString();
            return info;
        }


        public string CheckMemberInProc1(string Number, string LoginPass, string Direct, string MobileTele)
        {

            SqlParameter[] para =
            {  new SqlParameter("@Number",Number),		
               new SqlParameter("@LoginPass",LoginPass),
			   new SqlParameter("@Direct",Direct),
			   new SqlParameter("@MobileTele",MobileTele)
			  
								 };


            string info = DBHelper.ExecuteNonQuery("RegistraFree", para, CommandType.StoredProcedure).ToString();
            
            return info;
        }

        /// <summary>
        /// 无安置，无推荐检错信息
        /// </summary>
        /// <param name="placement">安置</param>
        /// <param name="Direct">推荐</param>
        /// <returns></returns>
        public int GetError(string option)
        {
            string SQL = "Select COUNT(ID) From MemberInfo Where Number=@option";
            SqlParameter[] para = 
            { 
              new SqlParameter("@option",option)
            
            };
            return (int)DBHelper.ExecuteScalar(SQL, para, CommandType.Text);
        }

        /// <summary>
        /// 判断推荐和安置人的注册期数是否合格
        /// </summary>
        /// <param name="number">编号</param>
        /// <returns name="qishu">期数</returns>
        public int GetError2(string number)
        {
            string SQL = "Select top 1 ExpectNum From MemberInfo Where Number=@option";
            SqlParameter[] para = 
            { 
              new SqlParameter("@option",number)
            
            };
            return (int)DBHelper.ExecuteScalar(SQL, para, CommandType.Text);
        }


        /// <summary>
        /// 检测安置编号是否在推荐编号的安置网络下面
        /// </summary>
        /// <param name="placement">安置</param>
        /// <param name="Direct">推荐</param>
        /// <returns></returns>
        public bool GetDirectPlacement(string Direct, string placementXuHao)
        {
            bool res = false;
            int QiShu = (int)DAL.CommonDataDAL.GetMaxExpect();
            object obj = DBHelper.ExecuteScalar("select count(0) as count  from memberinfobalance" + QiShu.ToString() + " where PlacementList like('%," + placementXuHao + ",%') and number='" + Direct + "'", CommandType.Text);
            if (obj.ToString() != "0")
            {
                res = true;
            }
            return res;

        }

        /// <summary>
        /// 求某编号的序号范围 ： 
        /// </summary>
        /// <param name="number">编号</param>
        /// <param name="anzhi">null为推荐，否则为安置</param>
        /// <param name="qishu">期数</param>
        /// <param name="sXuhao">起始序号</param>
        /// <param name="eXuhao">终止序号</param>
        public static void getXHFW(string bianhao, int qishu, out int sXuhao, out int eXuhao, out int Cengwei)
        {
            string myXuhao, myCengwei;
            Cengwei = 9999;

            myXuhao = "Ordinal1";
            myCengwei = "LayerBit1";

            //获取最大序号
            object maxNum = DAL.DBHelper.ExecuteScalar("SELECT MAX(" + myXuhao + ") as mShu FROM MemberInfoBalance" + qishu.ToString(), CommandType.Text);
            if (maxNum == System.DBNull.Value)
            {
                eXuhao = 0;
            }
            else
            {
                eXuhao = Convert.ToInt32(maxNum);
            }

            sXuhao = eXuhao + 1;
            //获取输入会员的层位和序号
            SqlDataReader dr;
            string sql = "SELECT   isnull(" + myCengwei + ",0)  as  " + myCengwei + " , isnull(" + myXuhao + ",0)  as  " + myXuhao + " FROM MemberInfoBalance" + qishu.ToString() + " WHERE number = '" + bianhao + "'";
            dr = DAL.DBHelper.ExecuteReader(sql, CommandType.Text);
            if (dr.Read())
            {
                Cengwei = Convert.ToInt32(dr[myCengwei]);
                sXuhao = Convert.ToInt32(dr[myXuhao]);
            }
            dr.Close();

            //确定终止序号
            int lsXuhao = Convert.ToInt32(DAL.DBHelper.ExecuteScalar("SELECT " + myXuhao + " FROM MemberInfoBalance" + qishu.ToString() + " WHERE " + myCengwei + "<=" + Cengwei.ToString() + " AND " + myXuhao + ">" + sXuhao.ToString() + " ORDER BY " + myXuhao + " ASC", CommandType.Text));
            if (lsXuhao > 0)
            {
                eXuhao = lsXuhao - 1;
            }

        }





        /// <summary>
        /// 调用存储过程js_delfuxiao
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <param name="currentOneMark">新个积分</param>
        /// <param name="ExpectNum">期数</param>
        /// <param name="flag">是否确认(0:未确认;1:确认)</param>
        /// <returns>返回结果</returns>
        public int Js_delfuxiao(string Number, double currentOneMark, int ExpectNum, int flag, SqlTransaction tran)
        {
            SqlParameter[] para = 
            {
            
              new SqlParameter("@Number",Number),
              new SqlParameter("@CurrentOneMark",currentOneMark),
              new SqlParameter("@ExpectNum",ExpectNum),
              new SqlParameter("@flag",flag)
            
            };
            return DBHelper.ExecuteNonQuery(tran, "js_delfuxiao", para, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 调用存储过程js_addfuxiao
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <param name="currentOneMark">新个积分</param>
        /// <param name="ExpectNum">期数</param>
        /// <param name="flag">是否确认(0:未确认;1:确认)</param>
        /// <param name="tran"></param>
        /// <returns>返回结果</returns>
        public int Js_addfuxiao(string Number, double currentOneMark, int ExpectNum, int flag, SqlTransaction tran)
        {
            SqlParameter[] para = 
            {
            
              new SqlParameter("@Number",Number),
              new SqlParameter("@CurrentOneMark",currentOneMark),
              new SqlParameter("@ExpectNum",ExpectNum),
              new SqlParameter("@flag",flag)
            
            };
            return DBHelper.ExecuteNonQuery(tran, "js_addfuxiao", para, CommandType.StoredProcedure);
        }


        /// <summary>
        /// 验证该编号是否存在
        /// </summary>
        /// <param name="number"></param>
        public int NuberIsExist(string number)
        {
            string SQL = "Select COUNT(ID) From MemberInfo Where Number=@Number";
            SqlParameter[] para = 
            {
            
              new SqlParameter("@Number",number)
            };
            return (int)DBHelper.ExecuteScalar(SQL, para, CommandType.Text);
        }


        /// <summary>
        ///  修改首次报单时读取信息
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <param name="OrderExpectNum">最大期数</param>
        /// <param name="storId">店编号</param>
        /// <param name="memberInfoModel">MemberInfoModel类对象</param>
        /// <param name="memberOrderModel">MemberOrderMode类对象</param>
        public void GetDataFormInfoAndOrder(string Number, int OrderExpectNum, string storId, MemberInfoModel memberInfoModel, MemberOrderModel memberOrderModel)
        {
            SqlParameter[] para =
            {
              new SqlParameter("@Number",Number),
            };
            string sql = @"Select a.*,b.* ,
            (select isnull(country ,'') from city where cpccode=a.cpccode) as country,
            (select isnull(province,'') from city where cpccode=a.cpccode) as  province,
            (select isnull(city,'') from city where cpccode=a.cpccode) as city
             From MemberInfo a,MemberOrder b Where a.Number=b.Number  and isagain=0  and a.Number=@Number";
            SqlDataReader reader = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            while (reader.Read())
            {
                memberInfoModel.OrderID = reader["OrderId"].ToString();
                memberInfoModel.Placement = reader["Placement"].ToString();
                memberInfoModel.ExpectNum = Convert.ToInt32(reader["ExpectNum"]);
                memberOrderModel.PayExpect = Convert.ToInt32((reader["PayExpectNum"] == DBNull.Value ? 0 : reader["PayExpectNum"]));
                memberInfoModel.ID = Convert.ToInt32(reader["ID"]);
                memberInfoModel.RegisterDate = Convert.ToDateTime(reader["RegisterDate"]);
                memberInfoModel.Direct = reader["Direct"].ToString();
                memberInfoModel.Number = reader["Number"].ToString();
                memberInfoModel.Name = reader["Name"].ToString();
                memberInfoModel.PetName = reader["PetName"].ToString();
                memberInfoModel.PostalCode = reader["PostalCode"].ToString();
                memberInfoModel.Address = reader["Address"].ToString();
                memberInfoModel.HomeTele = reader["HomeTele"].ToString();
                memberInfoModel.OfficeTele = reader["OfficeTele"].ToString();
                memberInfoModel.FaxTele = reader["FaxTele"].ToString();
                memberInfoModel.Email = reader["Email"].ToString();
                memberInfoModel.LoginPass = reader["LoginPass"].ToString();
                memberInfoModel.AdvPass = reader["AdvPass"].ToString();
                memberInfoModel.IsBatch = Convert.ToInt32(reader["IsBatch"]);
                memberInfoModel.BankCode = reader["BankCode"].ToString();
                memberInfoModel.PaperType.PaperTypeCode = reader["PaperTypeCode"].ToString();
                memberInfoModel.City.City = reader["city"].ToString();
                memberInfoModel.City.Province = reader["province"].ToString();
                memberInfoModel.City.Country = reader["country"].ToString();
                memberInfoModel.CPCCode = reader["CPCCode"].ToString();
                memberInfoModel.Birthday = Convert.ToDateTime(reader["Birthday"]);
                memberInfoModel.Sex = Convert.ToInt32(reader["Sex"]);
                memberInfoModel.BankCard = reader["BankCard"].ToString();
                memberInfoModel.PaperNumber = reader["PaperNumber"].ToString();
                memberInfoModel.PaperType.PaperTypeCode = reader["PaperTypeCode"].ToString();
                memberInfoModel.MobileTele = reader["MobileTele"].ToString();
                memberInfoModel.Remark = reader["Remark"].ToString();
                memberInfoModel.PhotoPath = reader["PhotoPath"].ToString();
                memberInfoModel.BCPCCode = reader["BCPCCode"].ToString();
                memberInfoModel.BankBook = reader["BankBook"].ToString();
                memberInfoModel.BankAddress = reader["BankAddress"].ToString();
                memberOrderModel.CCPCCode = reader["CCPCCode"].ToString();
                memberOrderModel.DefrayType = Convert.ToInt32(reader["DefrayType"].ToString());
                memberOrderModel.ElectronicaccountId = reader["ElectronicAccountID"].ToString();
                memberInfoModel.PaperNumber = reader["PaperNumber"].ToString();
                memberInfoModel.Remark = reader["Remark"].ToString();
                memberOrderModel.DefrayState = (int)reader["defraystate"];
                memberOrderModel.IsAgain = (byte)reader["IsAgain"];
                memberOrderModel.Number = reader["number"].ToString();
                memberOrderModel.StoreId = reader["storeId"].ToString();
                memberOrderModel.OrderType = Convert.ToInt32(reader["OrderType"]);
                memberOrderModel.ConZipCode = Convert.ToString(reader["ConZipCode"]);
            }
            reader.Close();
        }

        /// <summary>
        ///  修改首次报单时读取信息
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <param name="OrderExpectNum">最大期数</param>
        /// <param name="storId">店编号</param>
        /// <param name="memberInfoModel">MemberInfoModel类对象</param>
        /// <param name="memberOrderModel">MemberOrderMode类对象</param>
        public void GetDataFormInfoAndOrder(string Number, int OrderExpectNum, MemberInfoModel memberInfoModel, MemberOrderModel memberOrderModel)
        {
            SqlParameter[] para =
            {
              new SqlParameter("@Number",Number),
              new SqlParameter("@MaxOrderExpectNum",OrderExpectNum),
            };
            SqlDataReader reader = DBHelper.ExecuteReader("DS_GetFirstOrderDetails_1", para, CommandType.StoredProcedure);
            while (reader.Read())
            {
                memberInfoModel.OrderID = reader["OrderId"].ToString();
                memberInfoModel.Placement = reader["Placement"].ToString();
                memberInfoModel.ExpectNum = Convert.ToInt32(reader["ExpectNum"]);
                memberOrderModel.PayExpect = Convert.ToInt32(reader["PayExpectNum"]);
                memberInfoModel.ID = Convert.ToInt32(reader["ID"]);
                memberInfoModel.RegisterDate = Convert.ToDateTime(reader["RegisterDate"]);
                memberInfoModel.Direct = reader["Direct"].ToString();
                memberInfoModel.Number = reader["Number"].ToString();
                memberInfoModel.Name = reader["Name"].ToString();
                memberInfoModel.PetName = reader["PetName"].ToString();
                memberInfoModel.PostalCode = reader["PostalCode"].ToString();
                memberInfoModel.Address = reader["Address"].ToString();
                memberInfoModel.HomeTele = reader["HomeTele"].ToString();
                memberInfoModel.OfficeTele = reader["OfficeTele"].ToString();
                memberInfoModel.FaxTele = reader["FaxTele"].ToString();
                memberInfoModel.Email = reader["Email"].ToString();
                memberInfoModel.LoginPass = reader["LoginPass"].ToString();
                memberInfoModel.AdvPass = reader["AdvPass"].ToString();
                memberInfoModel.IsBatch = Convert.ToInt32(reader["IsBatch"]);
                memberInfoModel.BankCode = reader["Bank"].ToString();
                memberInfoModel.PaperType.PaperTypeCode = reader["PaperTypeCode"].ToString();
                //memberInfoModel.Country = reader["Country"].ToString();
                //memberInfoModel.Province = reader["Province"].ToString();
                //memberInfoModel.City = reader["City"].ToString();
                memberInfoModel.CPCCode = reader["CPCCode"].ToString();
                memberInfoModel.Birthday = Convert.ToDateTime(reader["Birthday"]);
                memberInfoModel.Sex = Convert.ToInt32(reader["Sex"]);
                memberInfoModel.BankCard = reader["BankCard"].ToString();
                //图片
                memberInfoModel.PhotoPath = reader["PhotoPath"].ToString();
                //
                //memberInfoModel.BankCountry = reader["BankCountry"].ToString();
                //memberInfoModel.BankProvince = reader["BankProvince"].ToString();
                //memberInfoModel.BankCity = reader["BankCity"].ToString();

                memberInfoModel.BankBook = reader["BankBook"].ToString();
                memberInfoModel.BankAddress = reader["BankAddress"].ToString();
                //
                //memberOrderModel.ConCountry = reader["ConCountry"].ToString();
                //memberOrderModel.ConProvince = reader["ConProvince"].ToString();
                //memberOrderModel.ConCity = reader["ConCity"].ToString();
                memberOrderModel.Ccpccode = reader["CCPCCode"].ToString();
                memberOrderModel.DefrayType = Convert.ToInt32(reader["DefrayType"].ToString());
                memberOrderModel.ElectronicaccountId = reader["ElectronicAccountID"].ToString();
                memberInfoModel.PaperNumber = reader["PaperNumber"].ToString();
                memberInfoModel.Remark = reader["Remark"].ToString();
                memberInfoModel.DefrayState = (int)reader["defraystate"];
                memberOrderModel.IsAgain = (byte)reader["IsAgain"];
                memberOrderModel.Number = reader["number"].ToString();
                memberOrderModel.StoreId = reader["StoreId"].ToString();
                memberOrderModel.OrderType = Convert.ToInt32(reader["OrderType"]);
            }
            reader.Close();

        }

        /// <summary>
        /// 根据orderID得到该报单的总积分
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public double GetSumPv(string orderId)
        {
            string SQL = "Select TotalPv From MemberOrder Where OrderID=@orderId";
            SqlParameter[] para =
            {
                new SqlParameter("@orderId",orderId)
            };
            return Convert.ToDouble(DBHelper.ExecuteScalar(SQL, para, CommandType.Text));
        }

        /// <summary>
        /// 根据orderID得到该报单的总金额
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public double GetTotalMoney(string orderId)
        {
            string SQL = "Select TotalMoney From MemberOrder Where OrderID=@orderId";
            SqlParameter[] para =
            {
                new SqlParameter("@orderId",orderId)
            };
            return Convert.ToDouble(DBHelper.ExecuteScalar(SQL, para, CommandType.Text));
        }

        /// <summary>
        /// 根据orderID得到该报单的消费类型（isAgain）
        /// </summary>
        /// <returns></returns>
        public string GetConsumeState(string orderId)
        {
            string SQL = "Select isAgain From MemberOrder Where OrderID=@orderId";
            SqlParameter[] para =
            {
                new SqlParameter("@orderId",orderId)
            };
            return Convert.ToString(DBHelper.ExecuteScalar(SQL, para, CommandType.Text));

        }


        /// <summary>
        /// 如果是修改则把数据填充到页面上（复消）
        /// </summary>
        /// <param name="memberOrderModel">memberOrderModel类对象</param>
        public void WriterDataToPage(MemberOrderModel memberOrderModel, string storeID, string OrderId)
        {
            string SQL = "Select *  From MemberOrder  Where OrderID=@OrderId  And  StoreID = @StoreId";
            SqlParameter[] para =
            {
                new SqlParameter("@orderId",OrderId),
                new SqlParameter("@StoreId",storeID)
            };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            while (reader.Read())
            {
                memberOrderModel.TotalPv = Convert.ToDecimal(reader["TotalPv"]);
                memberOrderModel.Number = reader["Number"].ToString();
                memberOrderModel.Remark = reader["Remark"].ToString();
                memberOrderModel.Consignee = reader["Consignee"].ToString();
                memberOrderModel.ConZipCode = reader["ConZipCode"].ToString();
                memberOrderModel.ConPost = reader["ConPost"].ToString();
                memberOrderModel.ConTelPhone = reader["ConTelPhone"].ToString();
                memberOrderModel.ConMobilPhone = Convert.ToString(reader["ConMobilPhone"]);
                memberOrderModel.ConAddress = reader["ConAddress"].ToString();
                //memberOrderModel.ConCountry = reader["ConCountry"].ToString();
                //memberOrderModel.ConProvince = reader["ConProvince"].ToString();
                //memberOrderModel.ConCity = reader["ConCity"].ToString();
                memberOrderModel.Ccpccode = reader["CCPCCode"].ToString();
                memberOrderModel.OrderExpect = Convert.ToInt32(reader["OrderExpectNum"]);
                memberOrderModel.PayExpect = Convert.ToInt32(reader["PayExpectNum"]);
                memberOrderModel.DefrayType = Convert.ToInt32(reader["DefrayType"]);
                memberOrderModel.ElectronicaccountId = reader["ElectronicaccountId"].ToString();
                memberOrderModel.DefrayState = (int)reader["defraystate"];
                memberOrderModel.IsAgain = (byte)reader["IsAgain"];
                memberOrderModel.StoreId = reader["storeId"].ToString();
                memberOrderModel.OrderType = Convert.ToInt32(reader["OrderType"]);

            }
            reader.Close();
        }


        /// <summary>
        /// 根据编号显示会员信息
        /// </summary>
        /// <param name="memberOrderModel"></param>
        /// <param name="number"></param>
        /// <param name="storeID"></param>
        public static MemberInfoModel GetMemberInfoByNumber(string number)
        {
            MemberInfoModel mi = new MemberInfoModel();

            string SQL = "Select top 1 m.*,c.city,c.province,c.country,c.xian From MemberInfo as m,City as c  Where m.cpccode=c.cpccode and m.number=@number";

            SqlParameter[] para =
            {
                new SqlParameter("@number",number),
            };

            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);

            while (reader.Read())
            {
                mi.Name = reader["name"].ToString();
                mi.PostalCode = reader["postalcode"].ToString();
                mi.Email = reader["Email"].ToString();
                mi.HomeTele = reader["HomeTele"].ToString();
                mi.MobileTele = reader["MobileTele"].ToString();
                mi.Address = reader["Address"].ToString();
                mi.City.Country = reader["country"].ToString();
                mi.City.Province = reader["province"].ToString();
                mi.City.City = reader["city"].ToString();
                mi.City.Xian = reader["xian"].ToString();
            }
            reader.Close();

            return mi;
        }

        /// <summary>
        /// 根据报单编号获取报单信息
        /// </summary>
        /// <param name="orderid">报单编号</param>
        /// <returns>返回报单信息</returns>
        public static MemberOrderModel SelectMemberOrderByOrderId(string orderid)
        {
            MemberOrderModel mo = new MemberOrderModel();

            string SQL = "select top 1 o.*,c.city,c.province,c.country,c.xian from memberorder o left join city c on c.cpccode=o.ccpccode where o.orderid=@orderid  ";

            SqlParameter[] paras =
            {
                new SqlParameter("@orderid",orderid)
            };

            DataTable dt = DBHelper.ExecuteDataTable(SQL, paras, CommandType.Text);
            foreach (DataRow dr in dt.Rows)
            {
                mo.Number = dr["Number"].ToString();
                mo.OrderId = dr["OrderId"].ToString();
                mo.Consignee = dr["Consignee"].ToString();
                mo.ConZipCode = dr["ConZipCode"].ToString();
                mo.ConPost = dr["ConPost"].ToString();
                mo.ConTelPhone = dr["ConTelPhone"].ToString();
                mo.ConMobilPhone = dr["ConMobilPhone"].ToString();
                mo.ConAddress = dr["ConAddress"].ToString();
                mo.DefrayType = Convert.ToInt32(dr["DefrayType"]);
                mo.Remark = dr["Remark"].ToString();
                mo.SendType = int.Parse(dr["SendType"].ToString());
                mo.DefrayState = Convert.ToInt32(dr["DefrayState"]);
                mo.ElectronicaccountId = dr["ElectronicaccountId"].ToString();
                mo.DefrayState = Convert.ToInt32(dr["DefrayState"]);
                mo.ConCity.Country = dr["country"].ToString();
                mo.ConCity.Province = dr["province"].ToString();
                mo.ConCity.City = dr["city"].ToString();
                mo.ConCity.Xian = dr["xian"].ToString();
                mo.OrderExpect = Convert.ToInt32(dr["OrderExpectNum"]);
                mo.PayExpect = Convert.ToInt32(dr["PayExpectNum"]);
                mo.OrderDate = Convert.ToDateTime(dr["OrderDate"]);
                mo.IsAgain = Convert.ToInt32(dr["IsAgain"]);
                mo.OrderType = Convert.ToInt32(dr["OrderType"]);
                mo.PayCurrency = Convert.ToInt32(dr["PayCurrency"]);
                mo.SendWay = Convert.ToInt32(dr["SendWay"]);
            }

            return mo;
        }

        /// <summary>
        /// 电子货币支付时记录未支付的金额
        /// </summary>
        /// <param name="dzNumber">电子账户号</param>
        /// <param name="sumMoney">本次报单的总金额</param>
        /// <returns></returns>
        public int Upd_ECTDeclarations(SqlTransaction tran, string dzNumber, double sumMoney)
        {
            string SQL = "update memberInfo set ECTDeclarations=ECTDeclarations+@sumMoney where number=@dzNumber";
            SqlParameter[] para =
            {
                new SqlParameter("@sumMoney",sumMoney),
                new SqlParameter("@dzNumber",dzNumber)
            };
            return DBHelper.ExecuteNonQuery(tran, SQL, para, CommandType.Text);
        }

        /// <summary>
        /// 电子货币支付时，记录已经支付的金额
        /// </summary>
        /// <param name="dzNumber">电子账户编号</param>
        /// <param name="sumMoney">消费总价</param>
        /// <returns></returns>
        public int Upd_ECTPay(SqlTransaction tran, string dzNumber, double sumMoney)
        {
            string SQL = "update memberInfo set totaldefray=totaldefray+@sumMoney where number=@dzNumber";
            SqlParameter[] para =
            {
                new SqlParameter("@sumMoney",sumMoney),
                new SqlParameter("@dzNumber",dzNumber)
            };

            return DBHelper.ExecuteNonQuery(tran, SQL, para, CommandType.Text);
        }

        /// <summary>
        /// 电子货币支付
        /// </summary>
        /// <param name="dzNumber">电子账户编号</param>
        /// <param name="sumMoney">消费总价</param>
        /// <returns></returns>
        public static int UpdateECTPay(SqlTransaction tran, string dzNumber, decimal sumMoney)
        {
            string SQL = "update memberInfo set totaldefray=totaldefray+@sumMoney where number=@dzNumber";
            SqlParameter[] para =
            {
                new SqlParameter("@sumMoney",sumMoney),
                new SqlParameter("@dzNumber",dzNumber)
            };

            return DBHelper.ExecuteNonQuery(tran, SQL, para, CommandType.Text);
        }

        /// <summary>
        /// 如果电子货币支付，则在店汇款中插入记录
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="mo">报单信息</param>
        /// <returns></returns>
        public int AddDataTORemittances(SqlTransaction tran, MemberOrderModel mo)
        {
            string SQL = @"insert into Remittances (RemittancesId,StoreID,RemitMoney,PayExpectNum,ReceivablesDate,RemittancesDate,remark,IsGSQR,RemittancesCurrency,RemittancesMoney,[use],standardcurrency,operateIp,operatenum,payway)
                                              values (@RemittanceId,@storeID,@sumMoney,@payExcept,@acceptDate, @sendOutDate,@remark,1,@payCurren,@payMoney,@use,@standardcurrency,@operateIp,@operatenum,@payway)";
            SqlParameter[] para =
            {
                new SqlParameter("@RemittanceId",mo.RemittancesId),
                new SqlParameter("@storeID",mo.StoreId),
                new SqlParameter("@sumMoney",mo.TotalMoney),
                new SqlParameter("@payExcept",mo.PayExpect),
                new SqlParameter("@acceptDate",DateTime.Now.ToUniversalTime()),
                new SqlParameter("@sendOutDate",DateTime.Now.ToUniversalTime()),
                new SqlParameter("@remark",mo.Number+"会员用"+mo.ElectronicaccountId+"会员电子钱包报单！"),
                new SqlParameter("@payCurren",1),
                new SqlParameter("@payMoney",mo.TotalMoney),
                new SqlParameter("@use",1),                                 //用于报单
                new SqlParameter("@standardcurrency",1),                     //标准币种
                new SqlParameter("@operateIp",mo.OperateIp),
                new SqlParameter("@operatenum",mo.OperateNumber),
                new SqlParameter("@payway",1)
            };
            return DBHelper.ExecuteNonQuery(tran, SQL, para, CommandType.Text);
        }
        public int AddDataTORemittances1(SqlTransaction tran, MemberOrderModel mo)
        {
            string SQL = @"insert into Remittances (RemittancesId,StoreID,RemitMoney,PayExpectNum,ReceivablesDate,RemittancesDate,remark,IsGSQR,RemittancesCurrency,RemittancesMoney,[use],standardcurrency,operateIp,operatenum,payway)
                                              values (@RemittanceId,@storeID,@sumMoney,@payExcept,@acceptDate, @sendOutDate,@remark,1,@payCurren,@payMoney,@use,@standardcurrency,@operateIp,@operatenum,@payway)";
            SqlParameter[] para =
            {
                new SqlParameter("@RemittanceId",mo.RemittancesId),
                new SqlParameter("@storeID",mo.StoreId),
                new SqlParameter("@sumMoney",mo.TotalMoney*-1),
                new SqlParameter("@payExcept",mo.PayExpect),
                new SqlParameter("@acceptDate",DateTime.Now.ToUniversalTime()),
                new SqlParameter("@sendOutDate",DateTime.Now.ToUniversalTime()),
                new SqlParameter("@remark","删除"+mo.Number+"会员用"+mo.ElectronicaccountId+"会员电子钱包的报单！"),
                new SqlParameter("@payCurren",1),
                new SqlParameter("@payMoney",mo.TotalMoney*-1),
                new SqlParameter("@use",1),                                 //用于报单
                new SqlParameter("@standardcurrency",1),                     //标准币种
                new SqlParameter("@operateIp",mo.OperateIp),
                new SqlParameter("@operatenum",mo.OperateNumber),
                new SqlParameter("@payway",1)
            };
            return DBHelper.ExecuteNonQuery(tran, SQL, para, CommandType.Text);
        }

        /// <summary>
        /// 如果电子货币支付，则在店汇款中插入记录
        /// </summary>
        /// <param name="tran">事务</param>
        /// <param name="mo">报单信息</param>
        /// <returns></returns>
        public static int AddDataTORemittances(SqlTransaction tran, string remid, string storeid, double totalmoeny, string docid, string ip, string opnum, int qishu)
        {
            string SQL = @"insert into Remittances (RemittancesId,remitnumber,RemitMoney,PayExpectNum,ReceivablesDate,RemittancesDate,remark,IsGSQR,RemittancesCurrency,RemittancesMoney,[use],standardcurrency,operateIp,operatenum,payway,RemitStatus)
                                              values (@RemittanceId,@storeID,@sumMoney,@payExcept,@acceptDate, @sendOutDate,@remark,1,@payCurren,@payMoney,@use,@standardcurrency,@operateIp,@operatenum,@payway,0)";
            SqlParameter[] para =
            {
                new SqlParameter("@RemittanceId",remid),
                new SqlParameter("@storeID",storeid),
                new SqlParameter("@sumMoney",totalmoeny),
                new SqlParameter("@payExcept",qishu),
                new SqlParameter("@acceptDate",DateTime.Now.ToUniversalTime()),
                new SqlParameter("@sendOutDate",DateTime.Now.ToUniversalTime()),
                new SqlParameter("@remark",storeid+"店铺退货,退货单号："+docid),
                new SqlParameter("@payCurren",1),
                new SqlParameter("@payMoney",totalmoeny),
                new SqlParameter("@use",1),                                 //用于报单
                new SqlParameter("@standardcurrency",1),                     //标准币种
                new SqlParameter("@operateIp",ip),
                new SqlParameter("@operatenum",opnum),
                new SqlParameter("@payway",1)
            };
            return DBHelper.ExecuteNonQuery(tran, SQL, para, CommandType.Text);
        }


        /// <summary>
        /// 更新店铺的汇款
        /// </summary>
        /// <param name="sumMoney">报单消费总价</param>
        /// <param name="storeId">店ID</param>
        /// <returns>更改结果</returns>
        public int Add_Remittances(SqlTransaction tran, double sumMoney, string storeId)
        {
            string SQL = "update  storeInfo set totalaccountmoney=totalaccountmoney+@sumMoney where storeId=@storeId";
            SqlParameter[] para =
            {
                new SqlParameter("@sumMoney",sumMoney),
                new SqlParameter("@storeId",storeId)
            };
            return DBHelper.ExecuteNonQuery(tran, SQL, para, CommandType.Text);
        }

        public int SetTotalDefray(string number, double totalMoney, SqlTransaction tran)
        {
            string sql = @"Update MemberInfo Set TotalDefray=TotalDefray-@TotalMoney Where Number=@Number";
            SqlParameter[] para = {
                                      new SqlParameter("@TotalMoney",totalMoney),
                                      new SqlParameter("@Number",number)
                                  };
            return DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
        }

        /// <summary>
        /// 更新店铺报单款，电子账款
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="money"></param>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static int UpdateStoreMoney(SqlTransaction tran, decimal money, string storeid)
        {
            string SQL = "update  storeInfo set totalaccountmoney=totalaccountmoney+@sumMoney,TotalMemberOrderMoney=TotalMemberOrderMoney+@sumMoney where storeId=@storeId";
            SqlParameter[] para =
            {
                new SqlParameter("@sumMoney",money),
                new SqlParameter("@storeId",storeid)
            };
            return DBHelper.ExecuteNonQuery(tran, SQL, para, CommandType.Text);
        }

        /// <summary>
        /// 获得最新汇款ID
        /// </summary>
        /// <returns></returns>
        public int GetRemittanceID()
        {
            int result = 0;
            string SQL = "select top 1 ID from Remittances order by id desc";
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, CommandType.Text);
            if (reader.Read())
            {
                result = Convert.ToInt32(reader[0]);
            }
            reader.Close();
            return result;
        }

        /// <summary>
        /// 个人电子账户剩余金额
        /// </summary>
        /// <returns></returns>
        public double HaveMoney(SqlTransaction tran, string dznumber)
        {
            string SQL = "select isnull((jackpot-ectpay-releasemoney-out-membership),0) from memberInfo where number=@dznumber";
            SqlParameter[] para =
            {
                new SqlParameter("@dznumber",dznumber)
            };
            return Convert.ToDouble(DBHelper.ExecuteScalar(tran, SQL, para, CommandType.Text));

        }

        /// <summary>
        /// 个人电子账户剩余金额
        /// </summary>
        /// <returns></returns>
        public double HaveMoney(string dznumber)
        {
            string SQL = "select isnull((totalremittances-totaldefray),0) from memberInfo where number=@dznumber";
            SqlParameter[] para =
            {
                new SqlParameter("@dznumber",dznumber)
            };
            return Convert.ToDouble(DBHelper.ExecuteScalar(SQL, para, CommandType.Text));

        }

        /// <summary>
        /// 得到所有期数集合
        /// </summary>
        /// <returns></returns>
        public List<ConfigModel> GetExceptList()
        {
            ConfigModel configModel = null;
            List<ConfigModel> exceptList = new List<ConfigModel>();
            string SQL = "select ExpectNum,convert(char(10),Date,120) as Date FROM  config order by ExpectNum";
            SqlDataReader reader = DBHelper.ExecuteReader(SQL);
            while (reader.Read())
            {
                configModel = new ConfigModel();
                configModel.ExpectNum = Convert.ToInt32(reader[0]);
                exceptList.Add(configModel);
            }
            reader.Close();
            return exceptList;
        }

        /// <summary>
        /// 获取店铺库存
        /// </summary>
        /// <returns></returns>
        public List<StockModel> GetStock(string storeId)
        {
            List<StockModel> list = new List<StockModel>();
            string SQL = "select ActualStorage,productId from Stock where storeId=@storeId";


            SqlParameter[] para =
            {
                new SqlParameter("@storeId",storeId)
           };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            while (reader.Read())
            {
                StockModel stockModel = new StockModel();
                stockModel.ActualStorage = Convert.ToDecimal(reader["ActualStorage"]);
                stockModel.ProductId = Convert.ToInt32(reader["ProductId"]);
                list.Add(stockModel);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 获取店铺库存
        /// </summary>
        /// <returns></returns>
        public List<StockModel> GetStock(string storeId, string orderid)
        {
            List<StockModel> list = new List<StockModel>();
            SqlParameter[] para =
            {
                new SqlParameter("@orderid",orderid),
                new SqlParameter("@storeId",storeId)
            };

            System.Web.HttpContext.Current.Application.UnLock();
            System.Web.HttpContext.Current.Application.Lock();
            SqlDataReader reader = DBHelper.ExecuteReader("GetBackGoods", para, CommandType.StoredProcedure);
            System.Web.HttpContext.Current.Application.UnLock();

            while (reader.Read())
            {
                StockModel stockModel = new StockModel();
                stockModel.ActualStorage = Convert.ToDecimal(reader["num"]);
                stockModel.ProductId = Convert.ToInt32(reader["pid"]);
                list.Add(stockModel);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 获取店铺某个产品库存
        /// </summary>
        /// <returns></returns>
        public List<StockModel> GetProductStock(string storeId, string productID)
        {
            List<StockModel> list = new List<StockModel>();
            string SQL = "select ActualStorage,productId from Stock where storeId=@storeId and productid=@productId";


            SqlParameter[] para =
            {
                new SqlParameter("@storeId",storeId),
                new SqlParameter("@productId",productID)
           };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            while (reader.Read())
            {
                StockModel stockModel = new StockModel();
                stockModel.ActualStorage = Convert.ToDecimal(reader["ActualStorage"]);
                stockModel.ProductId = Convert.ToInt32(reader["ProductId"]);
                list.Add(stockModel);
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 获取该订单不足货物时支付的费用（报单修改时使用）
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public double NeedReturnMoney(string orderID, string storeId)
        {
            string SQL = @"
                declare @laveAmount int
                declare @d_huilv money
                set @laveAmount=(select Isnull(sum(notEnoughProduct*PreferentialPrice),0) from MemberDetails,Product  where Product.ProductId=MemberDetails.ProductId and  MemberDetails.orderID=@orderID)
                set @d_huilv=(select a.Rate from currency a,storeInfo b where a.id=b.currency and b.storeid=@storeId)
                set @laveAmount=@laveAmount*@d_huilv
                select @laveAmount";
            SqlParameter[] para =
            {
                new SqlParameter("@orderID",orderID),
                new SqlParameter("@storeId",storeId)
            };
            return Convert.ToDouble(DBHelper.ExecuteScalar(SQL, para, CommandType.Text));

        }


        /// <summary>
        /// 扣除店铺可以报单的费用
        /// </summary>
        /// <param name="orderId">报单编号</param>
        /// <param name="storeId">店编号</param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public int Js_updateAllowMoney(string orderId, string storeId, SqlTransaction tran)
        {
            SqlParameter[] para = 
			{
               new SqlParameter("@orderID",orderId),
               new SqlParameter("@StoreID",storeId)
            };
            return DBHelper.ExecuteNonQuery(tran, "js_updateAllowMoney", para, CommandType.StoredProcedure);
        }
        ///// <summary>
        ///// 获取该订单订购的商品补回给库存
        ///// </summary>
        ///// <param name="orderID"></param>
        ///// <param name="storeID"></param>
        ///// <returns></returns>
        //public double NeedReturnItem(string orderID,string storeID) 
        //{
        //    string SQL = "update ";

        //}

        /// <summary>
        /// 得到店铺的报单明细
        /// </summary>		
        /// <param name="orderId">报单号</param>
        /// <returns></returns>
        //public static SqlDataReader GetH_mingxi(string orderId)
        //{
        //    string _ProcName = "Select ProductID , Quantity ,  Price , Pv FROM H_mingxi  WHERE OrderID = '" + orderId + "'";

        //    return DBHelper.ExecuteReader(_ProcName, null, CommandType.Text);

        //}

        //得到支付该保单的电子帐户
        public string GerExcNuber(string orderId)
        {
            string SQL = "select ElectronicAccountID from memberOrder where orderId=@orderId";
            SqlParameter[] para = 
			{
               new SqlParameter("@orderId",orderId),
              
            };
            return DBHelper.ExecuteScalar(SQL, para, CommandType.Text).ToString();

        }

        /// <summary>
        /// 查看memberDetails表中旧组合产品外有没有组合产品中的小产品
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int SmallItemIsOnlyInGroup(int productId, string orderId)
        {
            string sql = "select count(*) from memberDetails where orderId=@orderId and productId=@productId";
            SqlParameter[] para = 
			{
               new SqlParameter("@orderId",orderId),
               new SqlParameter("@productId",productId),
              
            };
            return (int)DBHelper.ExecuteScalar(sql, para, CommandType.Text);
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public int GetSmallItemInGroup(int productId, string GroupItemId)
        {
            string sql = "select sum(Quantity) from dbo.ProductCombineDetail where CombineProductID in(" + GroupItemId + ")";
            SqlParameter[] para = 
			{
               new SqlParameter("@productId",productId)
            };
            return (int)DBHelper.ExecuteScalar(sql, para, CommandType.Text);

        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public int CheckGroupSmallCount(int count, int productId)
        {
            string sql = "select (ActualStorage-@count) as needCount from Stock where productId=@productId";
            SqlParameter[] para = 
			{
               new SqlParameter("@productId",productId),
               new SqlParameter("@count",count)
            };
            SqlDataReader reader = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            if (reader.Read())
            {
                return Convert.ToInt32(reader[0]);
            }
            reader.Close();
            return 0;

        }

        /// <summary>
        /// 根据报单号获取总金额
        /// </summary>
        /// <param name="orderid">订单号</param>
        /// <returns></returns>
        public static double GetTotalMoneyByOrderId(string orderid)
        {
            return Convert.ToDouble(DBHelper.ExecuteScalar("select lackproductmoney from MemberOrder where orderid=@orderid", new SqlParameter[] { new SqlParameter("@orderid", orderid) }, CommandType.Text));
        }

        /// <summary>
        /// 获取会员的国家省份城市等信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static DataTable Getcontryofmember(string number)
        {
            string sqlstr = @"select ci.Country,ci.Province,ci.City,ci.postcode,mi.address,mi.cpccode,mi.postalCode,
              mi.homeTele,mi.MobileTele,mi.Email  from MemberInfo mi left outer join city ci on mi.cpccode=ci.cpccode where Number=@number";
            SqlParameter[] sps = new SqlParameter[] { 
             new SqlParameter("@number",number)
            };

            DataTable dt = DBHelper.ExecuteDataTable(sqlstr, sps, CommandType.Text);
            return dt;
        }

        /// <summary>
        /// 获取到订单的一些信息
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static DataTable Getorderinfoofmember(string orderid)
        {
            string sqlstr = @"select DefrayType,sendType ,SendWay,m.ccpccode,Country,Province,City,Xian,ConAddress,m.electronicAccountId from MemberOrder m left outer join City c on m.CcPCCode=c.CPCCode where OrderId=@orderid";
            SqlParameter[] sps = new SqlParameter[] { 
             new SqlParameter("@orderid",orderid)
            };

            DataTable dt = DBHelper.ExecuteDataTable(sqlstr, sps, CommandType.Text);
            return dt;
        }

        /// <summary>
        /// 获取订单的金额信息
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static DataTable Getmominfoofmember(string orderid)
        {
            string sqlstr = " select  RemittancesId,ElectronicaccountId,OrderType,DefrayState,ReceivablesDate,CarryMoney,OrderExpectNum,PayExpectNum,IsAgain,OrderDate,DefrayState,Consignee    from  memberorder where orderid=@orderid";
            SqlParameter[] sps = new SqlParameter[] { 
             new SqlParameter("@orderid",orderid)
            };
            DataTable dt = DBHelper.ExecuteDataTable(sqlstr, sps, CommandType.Text);
            return dt;

        }




        /// <summary>
        /// 订单支付功能方法
        /// </summary>
        /// <param name="numorstid">登录者编号（会员或者店铺）</param>
        /// <param name="orid">订单编号 （会员订单或者店铺订货单号）</param>
        /// <param name="ip">操作人ip</param>
        /// <param name="roletype">角色类型     1 是会员  2 是店铺</param>
        /// <param name="dotype">操作类型 1订单支付  2 充值  3店铺支付会员订单</param>
        /// <param name="accouttype">电子货币支付的时候 用到的账户类型  1会员现金账户 0会员消费账户 10店铺订货账户 11店铺周转款账户  </param>
        /// <param name="opert">操作人</param>
        /// <param name="remark">备注</param>
        /// <param name="pt">支付类型   1公司注册支付 ，2,电子账户支付   3尾数普通汇款支付   4 会员在线支付  5 去店铺支付</param>
        /// <param name="comflag">公司审核尾数匹配汇款   1（未点击确认汇款按钮公司确认）到账  2（会员确认汇款后公司点击）到账  3未到账 4迟到账</param>
        /// <param name="paycurry">支付币种</param>
        /// <param name="stcurry">标准币种</param>
        /// <param name="retmpid">汇款单编号id</param>
        /// <param name="realmoney">真实支付金额</param>
        /// <param name="Tserialnumber">在线支付流水号</param>
        /// <returns></returns>
        public static int OrderPayment(string numorstid, string orid, string ip, int roletype, int dotype, int accouttype, string opert, string remark, int pt, int comflag, int paycurry, int stcurry, string retmpid, double realmoney, string Tserialnumber)
        {
            string sqlpro = "OrderPayment";
            int res = 0;
            SqlParameter[] sps = new SqlParameter[] {
                new SqlParameter("@res",res),
                new SqlParameter("@number",numorstid),
                new SqlParameter("@orderid",orid),
                new SqlParameter("@Remittanceid",retmpid), //汇款单号
                new SqlParameter("@ip",ip),
                new SqlParameter("@roletype",roletype), // 角色
                new SqlParameter("@dotype",dotype),  //操作类型 
                new SqlParameter("@ptype",pt),
                new SqlParameter("@comflag",comflag),
                new SqlParameter("@accouttype",accouttype),
                new SqlParameter("@opert",opert),
                new SqlParameter("@remark",remark),
                new SqlParameter("@paycurry",paycurry),
                new SqlParameter("@standcurry",stcurry),
                new SqlParameter("@RealMoney",realmoney),
                new SqlParameter("@Tserialnumber",Tserialnumber)
            };

            sps[0].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery(sqlpro, sps, CommandType.StoredProcedure);
            res = Convert.ToInt32(sps[0].Value);

            return res;
        }
        public static int OrderPayment(SqlTransaction tran, string numorstid, string orid, string ip, int roletype, int dotype, int accouttype, string opert, string remark, int pt, int comflag, int paycurry, int stcurry, string retmpid, double realmoney, string Tserialnumber)
        {
            string sqlpro = "OrderPayment";
            int res = 0;
            SqlParameter[] sps = new SqlParameter[] {
                new SqlParameter("@res",res),
                new SqlParameter("@number",numorstid),
                new SqlParameter("@orderid",orid),
                new SqlParameter("@Remittanceid",retmpid), //汇款单号
                new SqlParameter("@ip",ip),
                new SqlParameter("@roletype",roletype), // 角色
                new SqlParameter("@dotype",dotype),  //操作类型 
                new SqlParameter("@ptype",pt),
                new SqlParameter("@comflag",comflag),
                new SqlParameter("@accouttype",accouttype),
                new SqlParameter("@opert",opert),
                new SqlParameter("@remark",remark),
                new SqlParameter("@paycurry",paycurry),
                new SqlParameter("@standcurry",stcurry),
                new SqlParameter("@RealMoney",realmoney),
                new SqlParameter("@Tserialnumber",Tserialnumber)
            };

            sps[0].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery(tran,sqlpro, sps, CommandType.StoredProcedure);
            res = Convert.ToInt32(sps[0].Value);

            return res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public static int OrderPayment1(string rd)
        {
            string sqlstr = "update  remittances set  shenhestate=20 where RemittancesID=@ord";
            SqlParameter[] sps = new SqlParameter[] { new SqlParameter("@ord", rd) };
            int count = DBHelper.ExecuteNonQuery(sqlstr, sps, CommandType.Text);
            return count;
        }

        public static int applystorepay(string rd)
        {
            string sqlstr = "update  memberorder set  DefrayType=1 where orderid=@ord";
            SqlParameter[] sps = new SqlParameter[] { new SqlParameter("@ord", rd) };
            int count = DBHelper.ExecuteNonQuery(sqlstr, sps, CommandType.Text);
            return count;
        }
        /// <summary>
        /// 根据编号获取区位
        /// </summary>
        /// <param name="number"></param>
        /// <param name="quwei"></param>
        /// <returns></returns>
        public static int Getquwei(string plnumber, int quwei)
        {
            object oc = DBHelper.ExecuteScalar("select count(0) from memberinfo where  Placement='" + plnumber + "'");

            if (oc != null)
            {
                int count = Convert.ToInt32(oc);
                if (count == 0)
                {
                    return quwei;
                }
                else if (count == 1)
                {
                    object qu = DBHelper.ExecuteScalar("select top 1 qushu from memberinfo where  Placement='" + plnumber + "'");
                    return Convert.ToInt32(qu) == 1 ? 2 : 1;
                }
                else
                {
                    return ++count;
                }
            }
            else
            {
                return quwei;
            }

        }

        /// <summary>
        /// 完成充值过程
        /// </summary>
        /// <param name="rimetid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int PaymentRemitmoney(string rimetid, string opnumber, string opip, int type)
        {
            string sqlproc = "PaymentRemitmoney";
            int er = 0;
            SqlParameter[] sps = new SqlParameter[] {
                new SqlParameter("@Remittancesid",rimetid),
                new SqlParameter("@opernumber",opnumber),
                new SqlParameter("@opertip",opip),
                new SqlParameter("@operttype",type),
                new SqlParameter("@err",er)
            };
            sps[4].Direction = ParameterDirection.Output;
            DBHelper.ExecuteNonQuery(sqlproc, sps, CommandType.StoredProcedure);
            er = Convert.ToInt32(sps[4].Value);

            return er;
        }
        /// <summary>
        /// 获取区数
        /// </summary>
        /// <param name="placement">安置人编号</param>
        /// <param name="district">区位：1 左区  2 右区</param>
        /// <returns></returns>
        public static int GetDistrict(string placement, int district)
        {
            object oc = DBHelper.ExecuteScalar("select count(0) from MemberInfo where Placement='" + placement + "'");

            if (oc != null)
            {
                int count = Convert.ToInt32(oc);
                if (count == 0)
                {
                    return 1;
                }
                else if (count == 1)
                {
                    object qu = DBHelper.ExecuteScalar("select top 1 District  from MemberInfo  where Placement='" + placement + "'");
                    return Convert.ToInt32(qu) == 1 ? 2 : 1;
                }
                else
                {
                    return ++count;
                }
            }
            else
            {
                return district;
            }

        }
    }
}