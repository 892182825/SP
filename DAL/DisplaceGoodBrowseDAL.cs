using System;
using System.Collections.Generic;
using System.Collections;
using Model;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

/*
 * 创建时间：   2009-09-08
 * 功能：       换货数据访问层
 */
namespace DAL
{
    public class DisplaceGoodBrowseDAL
    {


        /// <summary>
        /// 根据条件查询出换货情况
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableByCondition(string sql)
        {
            return DBHelper.ExecuteDataTable(sql);
        }
        /// <summary>
        /// 获取店铺换货列表
        /// </summary>
        /// <returns></returns>
        public IList<ReplacementModel> GetReplacementlList(int pageIndex, string key,
                int pageSize, string table, string columns, string condition, out int recordCount, out int pageCount)
        {
            IList<ReplacementModel> list = new List<ReplacementModel>();
            recordCount = 0;
            pageCount = 0;
            SqlParameter[] parm0 = {new SqlParameter("@PageIndex",SqlDbType.Int),
                                       new SqlParameter("@PageSize",SqlDbType.Int),
                                       new SqlParameter("@table",SqlDbType.VarChar,1000),
                                       new SqlParameter("@columns",SqlDbType.NVarChar,2000),
                                       new SqlParameter("@condition",SqlDbType.NVarChar,2000),
                                       new SqlParameter("@key",SqlDbType.VarChar,50),
                                       new SqlParameter("@RecordCount",SqlDbType.Int),
                                       new SqlParameter("@PageCount",SqlDbType.Int)
                                   };

            parm0[0].Value = pageIndex;
            parm0[1].Value = pageSize;
            parm0[2].Value = table;
            parm0[3].Value = columns;
            parm0[4].Value = condition;
            parm0[5].Value = key;
            parm0[6].Value = recordCount;
            parm0[7].Value = pageCount;

            parm0[6].Direction = System.Data.ParameterDirection.Output;
            parm0[7].Direction = System.Data.ParameterDirection.Output;


            DataTable dt = DBHelper.ExecuteDataTable("GetCustomersDataPage", parm0, CommandType.StoredProcedure);
            recordCount = Convert.ToInt32(parm0[6].Value);
            pageCount = Convert.ToInt32(parm0[7].Value);

            foreach (DataRow item in dt.Rows)
            {
                ReplacementModel replacementModel = new ReplacementModel((int)item["id"]);
                list.Add(replacementModel);
                replacementModel.StoreID = (string)item["StoreID"];
                replacementModel.RefundmentOrderID = (string)item["RefundmentOrderID"];
                replacementModel.StoreOrderID = (string)item["StoreOrderID"];
                replacementModel.ExpectNum = (int)item["ExpectNum"];
                replacementModel.StateFlag = item["StateFlag"].ToString(); //(int )item["StateFlag"];
                replacementModel.CloseFlag = item["CloseFlag"].ToString();// (int)item["CloseFlag"];
                //replacementModel.InGoodsMoney = 444;// Convert.ToDouble(item["ingoodsmoney"]);
                //replacementModel.OutGoodsMoney = 333;// Convert.ToDouble(item["outgoodmoney"]);
                replacementModel.MakeDocDate = (DateTime)item["MakeDocDate"];
            }

            return list;

        }

        public string GetStoreId(string OrderId)
        {
            string st = "select storeid from Replacement where displaceOrderid=@num";
            SqlParameter spa = new SqlParameter("@num",SqlDbType.VarChar,20);
            spa.Value = OrderId;
            string storeid = DBHelper.ExecuteScalar(st,spa,CommandType.Text).ToString();

            return storeid;
        }

        /// <summary>
        /// 备注查看
        /// </summary>
        /// <param name="StoreOrderID"></param>
        /// <returns></returns>
        public  string GetRemark(string displaceorderid)
        {
            string cmd = "select remark from replacement where  displaceorderid=@num";
            SqlParameter[] sPara = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 20) };
            sPara[0].Value = displaceorderid;

            SqlDataReader dr = DBHelper.ExecuteReader(cmd,sPara,CommandType.Text);

            dr.Read();

            string remark = dr["remark"].ToString();

            dr.Close();

            return remark;
        }
        /// <summary>
        /// 根据换货单号得到换货详情
        /// </summary>
        /// <returns></returns>
        public DataTable GetReplacementDetail(string displaceOrderID)
        {
            string sql = "select r.id,r.displaceorderid,r.productid,r.OutQuantity,r.price,r.pv,r.inquantity,p.productName,p.Currency from ReplacementDetail as r LEFT OUTER JOIN product as  p  on P.ProductID = r.ProductID where DisplaceOrderID=@DisplaceOrderID";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@DisplaceOrderID", displaceOrderID) };
            return DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
        }

        public DataTable GetdisplaceReplace(string displaceOrderID)
        {
            string sql = @"select *  from Replacement As d inner join StoreInfo  on d.storeid=StoreInfo.storeid  where DisplaceOrderID=@DisplaceOrderID";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@DisplaceOrderID", displaceOrderID) };
            return DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
        }


        #region 根据换货单号统计已审核的换货单
        /// <summary>
        /// 根据退货单号统计已审核的退货单
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public int GetStateDisplaceDocByDocId(string displaceOrderID)
        {
            string sql = "Select Count(*) From Replacement Where  StateFlag = 'Y' And DisplaceOrderID=@displaceOrderID";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("displaceOrderID", displaceOrderID) };
            return (int)DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
        }
        #endregion

      

        #region 判断换货店的库存是否大于等于退货的数量

        /// <summary>
        /// 判断换货店的库存是否大于等于退货的数量
        /// </summary>@StoreID  varchar(20)   ,
        /// @DisplaceOrderID   varchar(20)
        /// <returns></returns>
        public int CheckStoreGreaterThanDisplaceQuantity(string storeID, string displaceOrderID)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@StoreID", storeID), new SqlParameter("@DisplaceOrderID", displaceOrderID) };
            return Convert.ToInt32(DBHelper.ExecuteScalar("CheckStoreGreaterThanDisplaceQuantity", para, CommandType.StoredProcedure));
         
        }
        #endregion
        #region
        /// <summary>
        /// 判断进货数量是否小于等于公司库存数量
        /// </summary>@StoreID  varchar(20)   ,
        /// @DisplaceOrderID   varchar(20)
        /// <returns></returns>
        public int CheckCompanyGreaterThanOderQuantity(string displaceOrderID)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@DisplaceOrderID", displaceOrderID) };
            return Convert.ToInt32(DBHelper.ExecuteScalar("CheckCompanyGreaterThanOderQuantity", para, CommandType.StoredProcedure));
        }
        #endregion

        #region 判断退货额加剩余订货额是否小于等于预进货额
        /// <summary>
        /// 判断进货数量是否小于等于公司库存数量
        /// </summary>@StoreID  varchar(20)   ,
        /// @DisplaceOrderID   varchar(20)
        /// <returns></returns>
        public  decimal CheckMoneyWheatherEnough(string storeID, string displaceOrderID)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@StoreID", storeID), new SqlParameter("@DisplaceOrderID", displaceOrderID) };
            return Convert.ToDecimal(DBHelper.ExecuteScalar("CheckMoneyWheatherEnough", para, CommandType.StoredProcedure));
        }
        #endregion
        #region 根据退货单号统计已审核的退货单
        /// <summary>
        /// 根据换货单号统计已审核的换货单
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public int GetStaDisplaceDocByDocId(string displaceOrderID)
        {
            string sql = "Select Count(*) From Replacement Where  CloseFlag = 'Y' And DisplaceOrderID=@num";
            SqlParameter[] sPara = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 20) };
            sPara[0].Value = displaceOrderID;
            return ((int)DBHelper.ExecuteScalar(sql,sPara,CommandType.Text));
        }
        #endregion
        #region 更新订单的状态为无效的
        /// <summary>
        /// 更新订单的状态为无效的
        /// </summary>
        /// <param name="docID"></param>
        public void UpdateStateFlagAndCloseFlag(string displaceOrderID)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    string sql = " UPDATE Replacement SET CloseFlag = 'Y' WHERE displaceOrderID = @displaceOrderID ";
                    SqlParameter[] para = { new SqlParameter("@displaceOrderID", SqlDbType.Char, 20) };
                    para[0].Value = displaceOrderID;
                    DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
                    tran.Commit();
                    //日志处理
                }
                catch (Exception err)
                {
                    tran.Rollback();
                    throw err;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        #endregion
        //根据换货单号删除换货单信息与详细
        public void DeleteDisplaceGoodsOrderAndOrderDetail(string displaceOrderID)
        {


            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = "delete from Replacement where DisplaceOrderID =  @displaceOrderID ";
                        SqlParameter[] para = { new SqlParameter("@displaceOrderID", SqlDbType.Char, 20) };
                        para[0].Value = displaceOrderID;
                        DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
                        sql = "DELETE FROM ReplacementDetail WHERE  DisplaceOrderID = @displaceOrderID ";
                        DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
                        tran.Commit();
                    }
                    catch (System.Exception ee)
                    {
                        tran.Rollback();
                        System.Diagnostics.Trace.WriteLine(ee.Message);
                        throw;
                    }
                }
            }
        }

        //根据换货单号得到换货单详细
        /// <summary>
        /// 根据换货单号得到换货单详细
        /// </summary>
        /// <returns></returns>
        public ReplacementModel GetReplacementlModelByDisplaceOrderID(string displaceOrderID)
        {
            ReplacementModel replacementModel = null;
            string sql = "Select id,displaceorderid,storeid,storeorderid,refundmentorderid,OutStorageOrderID,makedocdate,makedocperson,auditperson,expectnum,outtotalmoney,outtotalpv,intotalmoney,intotalpv,inceptaddress,postalcode,telephone,stateflag,closeflag, remark,operateip,operatenum,inceptperson From Replacement Where DisplaceOrderID = @DisplaceOrderID ";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@DisplaceOrderID", displaceOrderID) };
            SqlDataReader dr = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            if (dr.Read())
            {
                replacementModel = new ReplacementModel();
                replacementModel.DisplaceOrderID = dr.GetString(dr.GetOrdinal("displaceorderid"));
                replacementModel.StoreID = dr.GetString(dr.GetOrdinal("storeid"));
                replacementModel.StoreOrderID = dr.GetString(dr.GetOrdinal("storeorderid"));
                replacementModel.RefundmentOrderID = dr.GetString(dr.GetOrdinal("refundmentorderid"));
                replacementModel.OutStrageOrderID = dr.GetString(dr.GetOrdinal("OutStorageOrderID"));
                replacementModel.MakeDocDate = dr.GetDateTime(dr.GetOrdinal("makedocdate"));
                replacementModel.MakeDocPerson = dr.GetString(dr.GetOrdinal("makedocperson"));
               // replacementModel.AuditingDate = dr.GetDateTime(dr.GetOrdinal("auditingdate"));
                replacementModel.AuditPerson = dr.GetString(dr.GetOrdinal("auditperson"));
                replacementModel.ExpectNum = dr.GetInt32(dr.GetOrdinal("expectnum"));

                replacementModel.OutTotalMoney = double.Parse(dr["outtotalmoney"].ToString());
                replacementModel.OutTotalPV = double.Parse(dr["outtotalpv"].ToString());
                replacementModel.InTotalMoney = double.Parse(dr["intotalmoney"].ToString());
                replacementModel.InTotalPV = double.Parse(dr["intotalpv"].ToString());
                replacementModel.InceptAddress = dr.GetString(dr.GetOrdinal("inceptaddress"));
                replacementModel.PostalCode = dr.GetString(dr.GetOrdinal("postalcode"));
                replacementModel.Telephone = dr.GetString(dr.GetOrdinal("telephone"));
                //replacementModel.StateFlag=int.Parse(dr["stateflag"].ToString());
                //replacementModel.StateFlag = dr.GetInt32(dr.GetOrdinal("stateflag"));
                //replacementModel.CloseFlag=int.Parse(dr["closeflag"].ToString());
                //replacementModel.CloseFlag = dr.GetInt32(dr.GetOrdinal("closeflag"));
                replacementModel.Remark = dr.GetString(dr.GetOrdinal("remark"));
                replacementModel.OperateIP = dr.GetString(dr.GetOrdinal("operateip"));
                replacementModel.OperateNum = dr.GetString(dr.GetOrdinal("operatenum"));
                replacementModel.InceptPerson = dr.GetString(dr.GetOrdinal("inceptperson"));
            }
            dr.Close();
            return replacementModel;
        }

        //得到换货信息用于编辑页面中绑定表单
        public DataTable GetRemplacementTable(string storeId, string displaceOrderId)
        {
            String sqlStr = "select a.id from country a,storeinfo b where a.countrycode=substring(b.SCPCCode,1,2) and b.storeid=@num";
            SqlParameter sPara = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            sPara.Value = storeId;

            string Country = Convert.ToString(DBHelper.ExecuteScalar(sqlStr,sPara,CommandType.Text));

            string sql = "Select P.productID,  IsNull((Select ActualStorage From Stock Where ProductID=P.ProductID And StoreID=@StoreID ),0) as Quantity,IsNull((Select OutQuantity From ReplacementDetail Where ProductID=P.ProductID And DisplaceOrderID=@OrderID ),0) as OutQuantity,IsNull((Select InQuantity From ReplacementDetail Where ProductID=P.ProductID And DisplaceOrderID=@OrderID ),0) as InQuantity,P.productName ,P.bigProductUnitID ,(Select ProductUnitName From ProductUnit Where ProductUnitID = P.bigProductUnitID) as BigUnitName ,P.smallProductUnitID,(Select ProductUnitName From ProductUnit Where ProductUnitID = P.smallProductUnitID) as SmallUnitName ,P.BigSmallMultiPle ,PreferentialPrice ,PreferentialPV   from product as P where isFold=0 ";
            SqlParameter[] para ={
					 new SqlParameter ("@StoreID" , SqlDbType .VarChar ,20),
									 new SqlParameter ("@OrderID" , SqlDbType .VarChar ,20)								 
								 };
            para[0].Value = storeId;
            para[1].Value = displaceOrderId;

            return DBHelper.ExecuteDataTable(sql, para, CommandType.Text);

        }
        //根据换货单号得到换货单详细
        /// <summary>
        /// 根据换货单号得到换货单详细
        /// </summary>
        /// <returns></returns>
        public void SaveReplacementlModelByDisplaceOrderID(string displaceOrderID, string number, ReplacementModel replacementModel, ArrayList displaceList)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {	//得到制单人
                    CommonDataDAL commonDataDAL = new CommonDataDAL();
                    string makeman = commonDataDAL.GetNameByAdminID(number);

                    //删除原来的明细
                    SqlParameter[] sPara = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar,20) };
                    sPara[0].Value = displaceOrderID;

                    DBHelper.ExecuteNonQuery(tran, "delete from ReplacementDetail where displaceOrderID =@num", sPara, CommandType.Text);
                    foreach (ReplacementDetailModel replacementDetailModel in displaceList)
                    {
                        replacementDetailModel.DisplaceOrderID = replacementModel.DisplaceOrderID;
                    }

                    CreateReplacementDetails(tran, displaceList, displaceOrderID);
                                     
                    //更新原来的换货单信息
                    string SQL_UPDATE_DisplaceGoodsOrder = @"UPDATE Replacement SET 
																MakeDocPerson = @MakeDocPerson, ExpectNum = @ExpectNum, 
																OutTotalMoney = @OutTotalMoney, OutTotalPV = @OutTotalPV, 
																InTotalMoney = @InTotalMoney, InTotalPV = @InTotalPV, 
																InceptAddress = @InceptAddress, InceptPerson = @InceptPerson, 
																PostalCode = @PostalCode, Telephone = @Telephone,remark=@remark
																WHERE DisplaceOrderID = @DisplaceOrderID";
                    SqlParameter[] para ={											  										
											  new SqlParameter("@MakeDocPerson",replacementModel.MakeDocPerson),											  
											  new SqlParameter("@ExpectNum", replacementModel.ExpectNum),
											  new SqlParameter("@OutTotalMoney",replacementModel.OutTotalMoney),
											  new SqlParameter("@OutTotalPV", replacementModel.OutTotalPV),
											  new SqlParameter("@InTotalMoney",replacementModel.InTotalMoney),
                                              new SqlParameter("@InTotalPV", replacementModel.InTotalPV),
											  new SqlParameter("@InceptAddress", replacementModel.InceptAddress),
											  new SqlParameter("@InceptPerson", replacementModel.InceptPerson),
											  new SqlParameter("@PostalCode", replacementModel.PostalCode),
											  new SqlParameter("@Telephone", replacementModel.Telephone),
											  new SqlParameter("@DisplaceOrderID", replacementModel.DisplaceOrderID),
												new SqlParameter("@remark",replacementModel.Remark)
										  };
                    int a = DBHelper.ExecuteNonQuery(tran, SQL_UPDATE_DisplaceGoodsOrder, para, CommandType.Text);

                    tran.Commit();
                }
                catch (Exception err)
                {
                    tran.Rollback();
                    throw err;
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        //插入新的明细
        public void CreateReplacementDetails(SqlTransaction tran, ArrayList displaceList,string displayorderid)
        {
           string  sql_save_displaceGoodsOrderDetail = @"insert into ReplacementDetail (displaceorderid,productid,outquantity,price,pv,inquantity)
																values(@displaceorderid,@productid,@outquantity,@price,@pv,@inquantity)";
            SqlParameter[] param ={
                                                 new SqlParameter("@displaceorderid",SqlDbType.VarChar,20),
                                                  new SqlParameter("@productid",SqlDbType.Int),
                                                   new SqlParameter("@outquantity", SqlDbType.Int),
                                                    new SqlParameter("@price",SqlDbType.Decimal),
                                                     new SqlParameter("@pv",SqlDbType.Decimal),
                                                      new SqlParameter("@inquantity",SqlDbType.Int)
                                             };
            foreach (ReplacementDetailModel replacementDetailModel in displaceList)
            {
                param[0].Value = displayorderid;
                     param[1].Value=replacementDetailModel.ProductID;
                      param[2].Value=replacementDetailModel.OutQuantity;
                       param[3].Value=replacementDetailModel.Price;
                        param[4].Value=replacementDetailModel.PV;
                        param[5].Value = replacementDetailModel.InQuantity;
                        DBHelper.ExecuteNonQuery(tran, sql_save_displaceGoodsOrderDetail, param, CommandType.Text);
            }

        }
        //添加换货信息
        public void ADDReplacement(ReplacementModel replacementModel, ArrayList displaceList)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    string SQL_Insert_DisplaceGoodsOrder = @"insert into  Replacement (DisplaceOrderID, StoreID, MakeDocDate, MakeDocPerson,ExpectNum, OutTotalMoney, 
						OutTotalPV, InTotalMoney, InTotalPV, InceptAddress, InceptPerson, PostalCode, Telephone, StateFlag, CloseFlag,remark)
                         values( @DisplaceOrderID, 	@StoreID, @MakeDocDate, @MakeDocPerson,
						@ExpectNum, @OutTotalMoney, @OutTotalPV, @InTotalMoney, @InTotalPV,
						 @InceptAddress, @InceptPerson, @PostalCode, @Telephone, @StateFlag, @CloseFlag,@remark)";

                    SqlParameter[] para ={	
							  				new SqlParameter("@DisplaceOrderID",replacementModel.DisplaceOrderID),
						                    new SqlParameter("@StoreID",replacementModel.StoreID),
                                            new SqlParameter("@MakeDocDate",replacementModel.MakeDocDate),
                                          
											  new SqlParameter("@MakeDocPerson",replacementModel.MakeDocPerson),											  
											  new SqlParameter("@ExpectNum", replacementModel.ExpectNum),
											  new SqlParameter("@OutTotalMoney",replacementModel.OutTotalMoney),
											  new SqlParameter("@OutTotalPV", replacementModel.OutTotalPV),
											  new SqlParameter("@InTotalMoney",replacementModel.InTotalMoney),
                                              new SqlParameter("@InTotalPV", replacementModel.InTotalPV),
											  new SqlParameter("@InceptAddress", replacementModel.InceptAddress),
											  new SqlParameter("@InceptPerson", replacementModel.InceptPerson),
											  new SqlParameter("@PostalCode", replacementModel.PostalCode),
											  new SqlParameter("@Telephone", replacementModel.Telephone),
											 new SqlParameter("@StateFlag",replacementModel.StateFlag),
									         new SqlParameter("@CloseFlag",replacementModel.CloseFlag),
                                             new SqlParameter("@remark",replacementModel.Remark)
										  };
                    DBHelper.ExecuteNonQuery(tran, SQL_Insert_DisplaceGoodsOrder, para, CommandType.Text);
                    
                    CreateReplacementDetails(tran, displaceList,replacementModel.DisplaceOrderID);
                    tran.Commit();
                }
                catch (Exception err)
                {
                    tran.Rollback();
                    throw err;
                }
                finally
                {
                    conn.Close();
                }
            }
        }



        //得到未审的换货单信息
        public DataTable GetNoShenHe(string condition)
        {
            string sql = @"select * from Replacement As d inner join StoreInfo  on d.storeid=StoreInfo.storeid  Order By d.MakeDocDate Desc";
            return DBHelper.ExecuteDataTable(sql, CommandType.Text);
        }
        //换货调用存储过程来生成各种订单并修改相应数据库
        public string UpdateReplacementUseProc(string displaceOrderId, string outStorageOrderId, string storeOrderId, string refundmentOrderID, string storeID, int expectNum)
        {
            string procName = "procCheckDisplaceGoods";
             SqlParameter[] HHpara = {
								new SqlParameter ("@OutStorageOrderId" , SqlDbType .VarChar ,20),
								new SqlParameter ("@StoreOrderId" , SqlDbType .VarChar ,20),
								new SqlParameter ("@DisplaceOrderID" , SqlDbType .VarChar ,20),
								new SqlParameter ("@RefundmentOrderID" , SqlDbType .VarChar ,20),
								new SqlParameter ("@StoreID" , SqlDbType .VarChar ,20),
								new SqlParameter ("@ExpectNum" , SqlDbType.Int ),
													};
                    HHpara[0].Value = outStorageOrderId;
                    HHpara[1].Value = storeOrderId;
                    HHpara[2].Value = displaceOrderId;
                    HHpara[3].Value = refundmentOrderID;
                    HHpara[4].Value = storeID;
                    HHpara[5].Value = CommonDataDAL.GetMaxExpect();

                    SqlParameter[] outparam = new SqlParameter[]  { new SqlParameter("@rval",SqlDbType.NVarChar,50) };
                    object[] obj = DBHelper.ExecuteNonQuery(procName,CommandType.StoredProcedure,HHpara,outparam);
                    return obj[0].ToString();
        }

        //根据换货单号得到换货的数量
          

        /// <summary>
        /// 验证公司库存
        /// </summary>
        /// <param name="DisplaceOrderId">换货单号</param>
        /// <returns></returns>
        public static bool CheckCompanyQuantity(string DisplaceOrderId)
        { 
             //检查公司库存
             SqlParameter[] para = new SqlParameter[] { new SqlParameter("@DisplaceOrderID", DisplaceOrderId) };
             return Convert.ToInt32(DBHelper.ExecuteScalar( "CheckCompanyGreaterThanOderQuantity", para, CommandType.StoredProcedure))>0;
        }

        /// <summary>
        /// 检查店铺库存
        /// </summary>
        /// <param name="DisplaceOrderId">换货单号</param>
        /// <returns></returns>
        public static bool CheckStoreQuantity(string DisplaceOrderId, string storeID)
        {
            //检查店铺库存
            SqlParameter[] para1 = new SqlParameter[] { new SqlParameter("@StoreID", storeID), new SqlParameter("@DisplaceOrderID", DisplaceOrderId) };
            return Convert.ToInt32(DBHelper.ExecuteScalar( "CheckStoreGreaterThanDisplaceQuantity", para1, CommandType.StoredProcedure))>0;
        }

        /// <summary>
        /// 验证店铺编号是否足够
        /// </summary>
        /// <param name="DisplaceOrderId"></param>
        /// <returns></returns>
        public static bool CheckStoreMoney(string DisplaceOrderId, string storeID)
        {
            //检查店铺余额
            SqlParameter[] para2 = new SqlParameter[] { new SqlParameter("@StoreID", storeID), new SqlParameter("@DisplaceOrderID", DisplaceOrderId) };
            return Convert.ToDouble(DBHelper.ExecuteScalar( "CheckMoneyWheatherEnough", para2, CommandType.StoredProcedure)) < 0; 
        }

        //根据换货单号更新店铺换货为已审
        public void UpdateReplacement(SqlTransaction tran,string DisplaceOrderId, string storeOrderId, string refundmentOrderID, string storeID, int expectNum, string warehouseId, string depotSeatId)
        {
            //@OutStorageOrderId varchar(20) ,----生成的出库单号
            //@StoreOrderId  varchar(20),--------生成的订单号
            //@DisplaceOrderID varchar(20), -----换货单号
            //@RefundmentOrderID varchar(20), -------生成的退货单号
            //@StoreID varchar(20),  ------------换货的店号
            //@ExpectNum int ---------------换货的期数

            string updateDisplaceOrderSql = @"Update Replacement Set 	StoreOrderId=@num, RefundmentOrderID=@num1,  AuditingDate=@num2, StateFlag='Y' Where DisplaceOrderId= @num3";
            SqlParameter[] sPara = new SqlParameter[] {
                new SqlParameter("@num", SqlDbType.VarChar,20),
                new SqlParameter("@num1", SqlDbType.VarChar,20),
                new SqlParameter("@num2", SqlDbType.SmallDateTime),
                new SqlParameter("@num3", SqlDbType.VarChar,20)
            };
            sPara[0].Value = storeOrderId;
            sPara[1].Value = refundmentOrderID;
            sPara[2].Value = Model.Other.MYDateTime.GetCurrentDateTime().ToString();
            sPara[3].Value = DisplaceOrderId;
            DBHelper.ExecuteNonQuery(tran, updateDisplaceOrderSql, sPara,CommandType.Text);

            //更新公司逻辑库存--换出
            DataTable dt = DBHelper.ExecuteDataTable("select OutQuantity as Quantity,ProductID as productId From ReplacementDetail where  DisplaceOrderID = @DisplaceOrderID", new SqlParameter[] { new SqlParameter("@DisplaceOrderID", DisplaceOrderId) }, CommandType.Text);
            foreach (DataRow dr in InventoryDocDAL.GetNewOrderDetail(dt).Rows)
            {
                string sqlUpdateLogicProduct = "update LogicProductInventory set Totalin=Totalin+@totalOut where productid=@productid";

                SqlParameter[] paraLogicProduct = new SqlParameter[]{
                             new SqlParameter("@totalOut",dr["Quantity"]),
                             new SqlParameter("@productid",dr["productId"])
                        };
                DBHelper.ExecuteNonQuery(sqlUpdateLogicProduct, paraLogicProduct, CommandType.Text);
                DBHelper.ExecuteNonQuery(tran, @"Update ProductQuantity  
																		Set Totalin= Totalin +(" + dr["Quantity"].ToString() + @") 
																		Where ProductID =" + dr["productId"].ToString() + @"
																		And DepotSeatId =" + depotSeatId + @"
                                                                        and warehouseid =" + warehouseId, null, CommandType.Text);

            }
            //更新公司逻辑库存--换入
            dt = DBHelper.ExecuteDataTable("select InQuantity as Quantity,ProductID as productId From ReplacementDetail where  DisplaceOrderID = @DisplaceOrderID", new SqlParameter[] { new SqlParameter("@DisplaceOrderID", DisplaceOrderId) }, CommandType.Text);
            foreach (DataRow dr in InventoryDocDAL.GetNewOrderDetail(dt).Rows)
            {
                string sqlUpdateLogicProduct = "update LogicProductInventory set totalout=totalout+@totalin where productid=@productid";
                SqlParameter[] paraLogicProduct = new SqlParameter[]{
                             new SqlParameter("@totalin",dr["Quantity"]),
                             new SqlParameter("@productid",dr["productId"])
                        };
                DBHelper.ExecuteNonQuery(sqlUpdateLogicProduct, paraLogicProduct, CommandType.Text);

                //更新公司实际库存
                
            }

            //在此调用GeneBillAndUpdateStorageandMoney存储过程
            SqlParameter[] HHpara = {
								new SqlParameter ("@StoreOrderId" , SqlDbType .VarChar ,20),
								new SqlParameter ("@DisplaceOrderID" , SqlDbType .VarChar ,20),
								new SqlParameter ("@RefundmentOrderID" , SqlDbType .VarChar ,20),
								new SqlParameter ("@StoreID" , SqlDbType .VarChar ,20),
								new SqlParameter ("@ExpectNum" , SqlDbType.Int ),
                                new SqlParameter ("@datenow" , SqlDbType.DateTime),
													};
            HHpara[0].Value = storeOrderId;
            HHpara[1].Value = DisplaceOrderId;
            HHpara[2].Value = refundmentOrderID;
            HHpara[3].Value = storeID;
            HHpara[4].Value = CommonDataDAL.GetMaxExpect();
            HHpara[5].Value = DateTime.Now.ToUniversalTime();


            DBHelper.ExecuteNonQuery(tran, "GeneBillAndUpdateStorageandMoney", HHpara, CommandType.StoredProcedure);

        }

        /// <summary>
        /// 查询换货后店铺金额
        /// </summary>
        /// <param name="returnid"> 换货单号 </param>
        /// <returns></returns>
        public static double GetGoodsReturnMoney(string returnid)
        {
            return Convert.ToDouble(DBHelper.ExecuteScalar("select isnull((sum(outquantity*price)-sum(inquantity*price)),0) from ReplacementDetail where displaceorderid=@orderid", new SqlParameter[] { new SqlParameter("@orderid", returnid) }, CommandType.Text));
        }
    }
}

