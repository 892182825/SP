using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Data;
using Model.Logistics;
using Model.Const;
using Model.Other;

namespace DAL.Logistics
{
    public class RefundmentOrderDocDAL
    {
        public RefundmentOrderDocDAL()
        {
        }
        /// <summary>
        /// 添加会员退货单记录
        /// </summary>
        /// <param name="refundmentOrder"></param>
        /// <returns></returns>
        public bool AddRefundmentOrderDoc(RefundmentOrderDocModel refundmentOrder, ref string msg)
        {
            bool flag = true;

            SqlTransaction tran = null;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(DBHelper.connString);
                con.Open();
                tran = con.BeginTransaction();

                int id = Convert.ToInt32(DBHelper.ExecuteScalar("select isnull(doctypeid,0) from DocTypeTable where doctypecode='THM'"));
                string sql = @"INSERT INTO InventoryDoc
                                (
                                    docTypeID, docID, docMakeTime, 
                                    docMaker,client, warehouseID,DepotSeatID,totalMoney, totalPV,
                                    StoreOrderID, ExpectNum,
                                    Reason, note,StateFlag ,BatchCode,OperationPerson,Address,OriginalDocID,OperateIP,OperateNum,InDepotSeatID,flag,cpccode
								)
						 VALUES(@docTypeID, @docID, @docMakeTime, 
								@docMaker, 
								@client, @warehouseID,@depotSeatID, @totalMoney, @totalPV,
								@StoreOrderID, @ExpectNum,
								@Reason, @note ,@StateFlag ,@BatchCode,@OperationPerson,@Address,@OriginalDocID,@OperateIP,@OperateNum,@InDepotSeatID,@flag,@cpccode)";
                SqlParameter[] para = new SqlParameter[]
                {				
				  new SqlParameter("@docTypeID", id),
				  new SqlParameter("@docID", refundmentOrder.DocID),
				  new SqlParameter("@docMakeTime", refundmentOrder.ApplyTime_DT),
				  new SqlParameter("@docMaker", refundmentOrder.Applicant_TX),
				  new SqlParameter("@client", refundmentOrder.OwnerNumber_TX),
				  new SqlParameter("@warehouseID", refundmentOrder.WareHouseID),
                  new SqlParameter("@depotSeatID",refundmentOrder.DepotSeatID),
				  new SqlParameter("@totalMoney", refundmentOrder.TotalMoney),
				  new SqlParameter("@totalPV", refundmentOrder.TotalPV),
				  new SqlParameter("@StoreOrderID", refundmentOrder.OriginalDocIDS),
				  new SqlParameter("@ExpectNum", refundmentOrder.ExpectNum),
				  new SqlParameter("@Reason", refundmentOrder.Cause_TX),
				  new SqlParameter("@note", refundmentOrder.Note_TX),
				  new SqlParameter("@StateFlag", refundmentOrder.StatusFlag_NR),
                  new SqlParameter("@BatchCode",refundmentOrder.DocID),
                  new SqlParameter("@OperationPerson",refundmentOrder.OperationPerson),
                  new SqlParameter("@Address",refundmentOrder.Address_TX),
				  new SqlParameter("@OriginalDocID" , refundmentOrder.OriginalDocIDS),
                  new SqlParameter("@OperateIP" , refundmentOrder.OperateIP_TX),
                  new SqlParameter("@OperateNum" , refundmentOrder.OperateNum_TX),
                  new SqlParameter("@InDepotSeatID",refundmentOrder.DepotSeatID),
                   new SqlParameter("@flag",refundmentOrder.RefundmentType_NR),
                   new SqlParameter("@cpccode",refundmentOrder.CPCCode)
				};


                int iVal = 0;
                object objVal = DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
                if (objVal == DBNull.Value || objVal == null)
                {
                    flag = false;
                    return flag;
                }
                else
                {
                    iVal = Convert.ToInt32(objVal);
                    if (iVal < 1)
                    {
                        flag = false;
                        return flag;
                    }
                    else if (iVal > 0)
                    { //退货单表插入成功

                        if (refundmentOrder != null && refundmentOrder.RefundmentOrderDetails.Count > 0)
                        {
                            #region 插入退单详细
                            string sqlDetails = string.Empty;
                            SqlParameter[] para2 ={
										  new SqlParameter ("@DocID",SqlDbType.VarChar,30),
										  new SqlParameter ("@ProductID",SqlDbType.Int),
										  new SqlParameter ("@ProductQuantity",SqlDbType.Int),
										  new SqlParameter ("@UnitPrice",SqlDbType.Decimal),
                                          
										  new SqlParameter ("@UnitPV",SqlDbType.Decimal),
										  new SqlParameter ("@ExpectNum",SqlDbType.Int),
										  new SqlParameter ("@Batch",SqlDbType.NVarChar,50),
                                          new SqlParameter ("@producttotal",SqlDbType.Decimal)

                                              };
                            foreach (RefundmentOrderDocDetails rod in refundmentOrder.RefundmentOrderDetails)
                            {
                                sqlDetails = @"insert into InventoryDocDetails(DocID,ProductID,ProductQuantity,UnitPrice,
                                    PV,ExpectNum,Batch,producttotal)
                                    values(@DocID,@ProductID,@ProductQuantity,@UnitPrice,
                                    @UnitPV,@ExpectNum,@Batch,@producttotal);select @@identity;";

                                para2[0].Value = rod.DocID;
                                para2[1].Value = rod.ProductID;
                                para2[2].Value = rod.ProductQuantity;
                                para2[3].Value = rod.UnitPrice;

                                para2[4].Value = rod.UnitPV;
                                para2[5].Value = rod.ExpectNum;
                                para2[6].Value = rod.Batch;
                                para2[7].Value = rod.UnitPrice * rod.ProductQuantity;
                                objVal = DBHelper.ExecuteScalar(tran, sqlDetails, para2, CommandType.Text);
                                if (objVal != DBNull.Value)
                                {
                                    iVal = Convert.ToInt32(objVal);
                                    if (iVal < 1)
                                    {
                                        flag = false;
                                        break;
                                    }
                                }

                                #region 更新会员订单[退货中的订单金额和PV]  TotalMoneyReturned,TotalPvReturned,QuantityReturned

                                int productid = rod.ProductID;
                                int Quantity = rod.ProductQuantity;
                                decimal TotalMoneyReturning = rod.UnitPrice * Quantity;
                                decimal TotalPvReturning = rod.UnitPV * Quantity;
                                string updateOrderSql = @" update MemberOrder set 
                                            TotalMoneyReturning=isnull(TotalMoneyReturning,0)+@TotalMoneyReturning,TotalPvReturning=isnull(TotalPvReturning,0)+@TotalPvReturning
                                         where OrderID=@OrderID; ";
                                SqlParameter[] para3 ={
										  new SqlParameter ("@TotalMoneyReturning",SqlDbType.Decimal),
										  new SqlParameter ("@TotalPvReturning" ,SqlDbType .Decimal ),
										  new SqlParameter ("@OrderID" ,SqlDbType.VarChar ,20)
									  };
                                para3[0].Value = TotalMoneyReturning;
                                para3[1].Value = TotalPvReturning;
                                para3[2].Value = rod.OriginalDocID;
                                iVal = DBHelper.ExecuteNonQuery(tran, updateOrderSql, para3, CommandType.Text);
                                if (iVal <= 0)
                                {
                                    flag = false;
                                    break;
                                }
                                string updateDetailsSql = " update MemberDetails set QuantityReturning=isnull(QuantityReturning,0)+@QuantityReturning where OrderID=@OrderID and ProductID=@ProductID; ";
                                SqlParameter[] para4 ={
										  new SqlParameter ("@QuantityReturned",SqlDbType.Int),
										  new SqlParameter ("@QuantityReturning",SqlDbType.Int),
										  new SqlParameter ("@OrderID" ,SqlDbType.VarChar ,30),
										  new SqlParameter ("@ProductID" ,SqlDbType.Int)
									  };
                                para4[0].Value = Quantity;
                                para4[1].Value = Quantity;
                                para4[2].Value = rod.OriginalDocID;
                                para4[3].Value = rod.ProductID;
                                iVal = DBHelper.ExecuteNonQuery(tran, updateDetailsSql, para4, CommandType.Text);
                                if (iVal <= 0)
                                {
                                    flag = false;
                                    break;
                                }
                                #endregion
                            }
                            #endregion

                        }
                        else
                        {
                            flag = false;
                            return flag;
                        }

                    }

                }
                if (flag)
                {
                    tran.Commit();
                    msg = "添加成功！";
                }
                else
                {
                    tran.Rollback();
                    msg = "添加失败！";
                }
            }
            catch (Exception ex)
            {
                flag = false;
                msg = ex.Message.Trim();
                tran.Rollback();
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return flag;
        }

        /// <summary>
        /// 审核会员退货审核单
        /// </summary>
        /// <param name="refundmentOrder"></param>
        /// <returns></returns>
        public bool AuditRefundmentOrderDoc(RefundmentOrderDocModel refundmentOrder, ref string msg)
        {
            bool flag = true;
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                SqlTransaction tran = null;
                try
                {

                    conn.Open();
                    tran = conn.BeginTransaction();


                    string sqlAuditValidate = "select StatusFlag_NR from RefundmentOrderDoc where DocID=@DocID";
                    SqlParameter[] para0 ={
										  new SqlParameter ("@DocID",SqlDbType.VarChar,30)
                                           };
                    para0[0].Value = refundmentOrder.DocID;
                    int statusFlag = -1;
                    object objStatusFlag = DBHelper.ExecuteScalar(tran, sqlAuditValidate, para0, CommandType.Text);
                    if (objStatusFlag != DBNull.Value)
                    {
                        statusFlag = Convert.ToInt32(objStatusFlag);
                    }
                    if (statusFlag == -1)
                    {
                        msg = "当前退货单己锁定，不能审核！";
                        flag = false;
                    }
                    else if (statusFlag == 2)
                    {
                        msg = "当前退货单己审核，不能再次审核！";
                        flag = false;
                    }
                    if (flag)
                    {
                        string sqlAudit = @" update InventoryDoc set docAuditer=@Auditer,docAuditTime=@AuditTime,StateFlag=@StatusFlag_NR,WareHouseID=@WareHouseID,DepotSeatID=@DepotSeatID,
                        TotalMoney=@RefundTotalMoney where DocID=@DocID and StateFlag=0";
                        SqlParameter[] para ={
										  new SqlParameter ("@Auditer", SqlDbType.VarChar,30),
										  new SqlParameter ("@AuditTime" , SqlDbType.DateTime),
										  new SqlParameter ("@StatusFlag_NR", SqlDbType.Int),
										  new SqlParameter ("@WareHouseID", SqlDbType.Int),
										  new SqlParameter ("@DepotSeatID", SqlDbType.Int),
										  new SqlParameter ("@RefundTotalMoney" ,SqlDbType.Decimal),
										  new SqlParameter ("@DocID" ,SqlDbType.VarChar,30)
									  };
                        para[0].Value = refundmentOrder.Auditer;
                        para[1].Value = DateTime.Now;
                        para[2].Value = refundmentOrder.StatusFlag_NR;
                        para[3].Value = refundmentOrder.WareHouseID;
                        para[4].Value = refundmentOrder.DepotSeatID;
                        para[5].Value = refundmentOrder.RefundTotalMoney;
                        para[6].Value = refundmentOrder.DocID;
                        int iVal = DBHelper.ExecuteNonQuery(tran, sqlAudit, para, CommandType.Text);
                        if (iVal < 1)
                        {
                            flag = false;
                        }
                        else
                        {//主表更新成功 if (iVal > 0)

                            #region
                            if (refundmentOrder.RefundmentOrderDetails != null && refundmentOrder.RefundmentOrderDetails.Count > 0)
                            {
                                List<string> orderids = new List<string>();
                                foreach (RefundmentOrderDocDetails rod in refundmentOrder.RefundmentOrderDetails)
                                {
                                    #region 更新公司库存/逻辑库存
                                    //组合产品
                                    if (DAL.DBHelper.ExecuteScalar("select id from ProductCombineDetail where CombineProductID=" + rod.ProductID) != null)
                                    {
                                        DataTable dt_x1 = DAL.DBHelper.ExecuteDataTable("select * from ProductCombineDetail where CombineProductID=" + rod.ProductID);
                                        for (int i = 0; i < dt_x1.Rows.Count; i++)
                                        {
                                            //实际库存
                                            if (((int)DBHelper.ExecuteNonQuery(tran, @"Update ProductQuantity  
                																		Set TotalIn= TotalIn +(" + rod.ProductQuantity * int.Parse(dt_x1.Rows[i]["Quantity"].ToString()) + @") 
                																		Where ProductID =" + dt_x1.Rows[i]["SubProductID"] + @"
                																		And DepotSeatId =" + refundmentOrder.DepotSeatID + @"
                                                                                        and warehouseid =" + refundmentOrder.WareHouseID, null, CommandType.Text)) == 0)
                                            {
                                                DBHelper.ExecuteNonQuery(tran, @"INSERT INTO ProductQuantity
                                            												(ProductID,TotalIn,TotalOut,TotalLogicOut,WareHouseID,DepotSeatId) 
                                            												VALUES( " + dt_x1.Rows[i]["SubProductID"] + @" ," + (rod.ProductQuantity * int.Parse(dt_x1.Rows[i]["Quantity"].ToString())) + @",0,0," + refundmentOrder.WareHouseID + @"," + refundmentOrder.DepotSeatID + @" )"
                                                    , null, CommandType.Text);
                                            }

                                            //逻辑库存
                                            if (((int)DBHelper.ExecuteNonQuery(tran, @"Update LogicProductInventory  
																		Set TotalIn= TotalIn +(" + rod.ProductQuantity * int.Parse(dt_x1.Rows[i]["Quantity"].ToString()) + @") 
																		Where ProductID =" + dt_x1.Rows[i]["SubProductID"], null, CommandType.Text)) == 0)
                                            {
                                                DBHelper.ExecuteNonQuery(tran, @"INSERT INTO LogicProductInventory
												(ProductID ,TotalIn ,TotalOut , TotalLogicOut) 
												VALUES( " + dt_x1.Rows[i]["SubProductID"] + @" ," + (rod.ProductQuantity * int.Parse(dt_x1.Rows[i]["Quantity"].ToString())) + @", 0 ,0 )"
                                                    , null, CommandType.Text);
                                            }
                                        }
                                    }
                                    else //单品
                                    {
                                        //实际库存
                                        if (((int)DBHelper.ExecuteNonQuery(tran, @"Update ProductQuantity  
																		Set TotalIn= TotalIn +(" + rod.ProductQuantity + @") 
																		Where ProductID =" + rod.ProductID + @"
																		And DepotSeatId =" + refundmentOrder.DepotSeatID + @"
                                                                        and warehouseid =" + refundmentOrder.WareHouseID, null, CommandType.Text)) == 0)
                                        {
                                            DBHelper.ExecuteNonQuery(tran, @"INSERT INTO ProductQuantity
												(ProductID ,TotalIn ,TotalOut , TotalLogicOut , WareHouseID,DepotSeatId ) 
												VALUES( " + rod.ProductID + @" ," + (rod.ProductQuantity) + @", 0 ,0 ," + refundmentOrder.WareHouseID + @"," + refundmentOrder.DepotSeatID + @" )"
                                                , null, CommandType.Text);
                                        }

                                        //逻辑库存
                                        if (((int)DBHelper.ExecuteNonQuery(tran, @"Update LogicProductInventory  
																		Set TotalIn= TotalIn +(" + rod.ProductQuantity + @") 
																		Where ProductID =" + rod.ProductID, null, CommandType.Text)) == 0)
                                        {
                                            DBHelper.ExecuteNonQuery(tran, @"INSERT INTO LogicProductInventory
												(ProductID ,TotalIn ,TotalOut , TotalLogicOut) 
												VALUES( " + rod.ProductID + @" ," + (rod.ProductQuantity) + @", 0 ,0 )"
                                                , null, CommandType.Text);
                                        }
                                    }
                                    #endregion

                                    #region 更新会员订单[订单金额和PV]  TotalMoneyReturned,TotalPvReturned,QuantityReturned

                                    if (!orderids.Contains(rod.OriginalDocID) && string.IsNullOrEmpty(rod.OriginalDocID))
                                        orderids.Add(rod.OriginalDocID);
                                    int productid = rod.ProductID;
                                    int Quantity = rod.ProductQuantity;
                                    decimal TotalMoneyReturned = rod.UnitPrice * Quantity;
                                    decimal TotalPvReturned = rod.UnitPV * Quantity;
                                    string updateOrderSql = @" update MemberOrder  set TotalMoney=TotalMoney-@TotalMoneyReturned,TotalPv=TotalPv-@TotalPvReturned,
                                                                    TotalMoneyReturned=isnull(TotalMoneyReturned,0)+@TotalMoneyReturned,TotalPvReturned=isnull(TotalPvReturned,0)+@TotalPvReturned,
                                                                    TotalMoneyReturning=isnull(TotalMoneyReturning,0)-@TotalMoneyReturning,TotalPvReturning=isnull(TotalPvReturning,0)-@TotalPvReturning  
                                                                 where OrderID=@OrderID; ";
                                    SqlParameter[] para1 ={
										  new SqlParameter ("@TotalMoneyReturned",SqlDbType.Decimal),
										  new SqlParameter ("@TotalPvReturned" ,SqlDbType .Decimal ),
										  new SqlParameter ("@TotalMoneyReturning",SqlDbType.Decimal),
										  new SqlParameter ("@TotalPvReturning" ,SqlDbType .Decimal ),
										  new SqlParameter ("@OrderID" ,SqlDbType.VarChar ,20)
									  };
                                    para1[0].Value = TotalMoneyReturned;
                                    para1[1].Value = TotalPvReturned;
                                    para1[2].Value = TotalMoneyReturned;
                                    para1[3].Value = TotalPvReturned;
                                    para1[4].Value = rod.OriginalDocID;
                                    iVal = DBHelper.ExecuteNonQuery(tran, updateOrderSql, para1, CommandType.Text);
                                    if (iVal <= 0)
                                    {
                                        flag = false;
                                        break;
                                    }
                                    string updateDetailsSql = " update MemberDetails set QuantityReturned=isnull(QuantityReturned,0)+@QuantityReturned, "
                                        + " QuantityReturning=isnull(QuantityReturning,0)-@QuantityReturning where OrderID=@OrderID and ProductID=@ProductID; ";
                                    SqlParameter[] para2 ={
										  new SqlParameter ("@QuantityReturned",SqlDbType.Int),
										  new SqlParameter ("@QuantityReturning",SqlDbType.Int),
										  new SqlParameter ("@OrderID" ,SqlDbType.VarChar ,30),
										  new SqlParameter ("@ProductID" ,SqlDbType.Int)
									  };
                                    para2[0].Value = Quantity;
                                    para2[1].Value = Quantity;
                                    para2[2].Value = rod.OriginalDocID;
                                    para2[3].Value = rod.ProductID;
                                    iVal = DBHelper.ExecuteNonQuery(tran, updateDetailsSql, para2, CommandType.Text);
                                    if (iVal <= 0)
                                    {
                                        flag = false;
                                        break;
                                    }
                                    #endregion

                                    #region 更新订单退货状态

                                    string sqlRemoveToDeletedTB = string.Empty;
                                    DataTable dtTemp = new DataTable();
                                    dtTemp = MemberInfoDAL.GetOrderMoneyPVSumByOrderID(tran, rod.OriginalDocID);
                                    if (dtTemp != null && dtTemp.Rows.Count > 0)
                                    {
                                        SqlParameter[] para3 ={
										  new SqlParameter ("@OrderID" ,SqlDbType.VarChar ,30)
                                                                  };
                                        para3[0].Value = rod.OriginalDocID;
                                        string updateOrderStatusSql = string.Empty;
                                        decimal leftTotalMoney = Convert.ToDecimal(dtTemp.Rows[0]["totalMoney"]);

                                        if (leftTotalMoney <= 0)
                                        {//可用余额为0
                                            //如果金额为
                                            //sqlRemoveToDeletedTB = "insert into  Deleted_MemberOrder select * from MemberOrder  where  OrderID=@OrderID";
                                            //iVal = DBHelper.ExecuteNonQuery(tran, updateOrderStatusSql, para3, CommandType.Text);
                                            //sqlRemoveToDeletedTB = "insert into  Deleted_MemberDetails select * from MemberDetails  where  OrderID=@OrderID";
                                            //iVal = DBHelper.ExecuteNonQuery(tran, updateOrderStatusSql, para3, CommandType.Text);  
                                            updateOrderStatusSql = "update MemberOrder set OrderStatus_NR=" + (int)OrderStatusEnum.RefundmentedAll + " where  OrderID=@OrderID and TotalMoneyReturned!=TotalMoney ";

                                        }
                                        else
                                        {
                                            updateOrderStatusSql = "update MemberOrder set OrderStatus_NR=" + (int)OrderStatusEnum.RefundmentedPart + " where  OrderID=@OrderID and TotalMoneyReturned!=TotalMoney ";
                                        }
                                        //更新订单状态
                                        iVal = DBHelper.ExecuteNonQuery(tran, updateOrderStatusSql, para3, CommandType.Text);
                                        if (iVal <= 0)
                                        {
                                            flag = false;
                                            break;
                                        }
                                    }
                                    #endregion

                                    #region 根据退款方式
                                    /*
                                    int RefundmentType_NR = refundmentOrder.RefundmentType_NR;
                                    string updateAccountSql = string.Empty;
                                    switch (RefundmentType_NR)
                                    {
                                        通过电子账户退还
                                        case (int)RefundsTypeEnum.RefundsUseEAccount:
                                            updateAccountSql = "update memberInfo set Jackpot=isnull(Jackpot,0)+" + refundmentOrder.RefundTotalMoney + " where number='" + refundmentOrder.OwnerNumber_TX.Trim() + "'";

                                            break;
                                        通过现金返回
                                        case (int)RefundsTypeEnum.RefundsUseCash:
                                            break;
                                        通过银行卡返还
                                        case (int)RefundsTypeEnum.RefundsUseBank:
                                            break;

                                    }
                                    if (!string.IsNullOrEmpty(updateAccountSql))
                                    {
                                        iVal = DBHelper.ExecuteNonQuery(tran, updateAccountSql, null, CommandType.Text);
                                        if (iVal <= 0)
                                        {
                                            flag = false;
                                            break;
                                        }
                                    }
                                     * */
                                    #endregion

                                    #region 退货强制重结

                                    string orderId = rod.OriginalDocID;
                                    DataTable dt_orderInfo = DAL.DBHelper.ExecuteDataTable(tran, "select Number,PayExpectNum from MemberOrder where OrderID='" + orderId + "'", null, CommandType.Text);
                                    if (dt_orderInfo != null && dt_orderInfo.Rows.Count > 0)
                                        DAL.DBHelper.ExecuteNonQuery(tran, "update Config set jsflag=0 where ExpectNum>=" + dt_orderInfo.Rows[0]["PayExpectNum"], null, CommandType.Text);

                                    #endregion

                                    #region 删除业绩

                                    //if (dt_orderInfo != null && dt_orderInfo.Rows.Count > 0)
                                    //    DAL.DBHelper.ExecuteNonQuery(tran, "update  MemberInfoBalance" + dt_orderInfo.Rows[0]["PayExpectNum"] + "  set CurrentOneMark=CurrentOneMark-" + TotalPvReturned + ",TotalOneMark=TotalOneMark-" + TotalPvReturned + "  where number='" + dt_orderInfo.Rows[0]["Number"] + "'", null, CommandType.Text);

                                    #endregion
                                }
                            }


                            #endregion
                        }
                    }
                    else
                    {//己审核或已锁定 
                    }

                    if (flag)
                    {
                        tran.Commit();
                        msg = "添加成功！";
                    }
                    else
                    {
                        tran.Rollback();
                        if (string.IsNullOrEmpty(msg))
                            msg = "添加失败！";
                    }
                }
                catch (Exception ex)
                {
                    flag = false;
                    tran.Rollback();
                    msg = ex.Message.Trim();
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            return flag;
        }

        public RefundmentOrderDocModel GetRefundmentOrderDocByDocID(string DocID, ref string msg)
        {
            try
            {
                RefundmentOrderDocModel refundmentOrderDoc = new RefundmentOrderDocModel();
                string sql = "select * from InventoryDoc t where t.DocID=@DocID";
                SqlParameter[] para0 ={
										  new SqlParameter ("@docID" ,SqlDbType .Char ,20)
									  };
                para0[0].Value = DocID;
                DataTable dt = DBHelper.ExecuteDataTable(sql, para0, CommandType.Text);
                if (dt == null && dt.Rows.Count < 1)
                    return null;
                foreach (DataRow dr in dt.Rows)
                {
                    refundmentOrderDoc.DocID = dr["DocID"].ToString();
                    refundmentOrderDoc.OwnerNumber_TX = dr["client"].ToString();
                    refundmentOrderDoc.Applicant_TX = dr["docmaker"].ToString();
                    refundmentOrderDoc.ApplyTime_DT = dr["docmaketime"] == DBNull.Value ? DateTime.Parse("1900-1-1") : DateTime.Parse(dr["docmaketime"].ToString());
                    refundmentOrderDoc.Auditer = dr["docAuditer"].ToString();

                    refundmentOrderDoc.AuditTime = dr["docAuditTime"] == DBNull.Value ? DateTime.Parse("1900-1-1") : DateTime.Parse(dr["docAuditTime"].ToString());
                    refundmentOrderDoc.StatusFlag_NR = dr["StateFlag"] == DBNull.Value ? -1 : int.Parse(dr["StateFlag"].ToString());
                    refundmentOrderDoc.WareHouseID = dr["WareHouseID"] == DBNull.Value ? -1 : int.Parse(dr["WareHouseID"].ToString());
                    refundmentOrderDoc.DepotSeatID = dr["DepotSeatID"] == DBNull.Value ? -1 : int.Parse(dr["DepotSeatID"].ToString());
                    refundmentOrderDoc.TotalMoney = dr["TotalMoney"] == DBNull.Value ? 0 : decimal.Parse(dr["TotalMoney"].ToString());

                    refundmentOrderDoc.TotalPV = dr["TotalPV"] == DBNull.Value ? 0 : decimal.Parse(dr["TotalPV"].ToString());
                    //refundmentOrderDoc.MobileTele = dr["MobileTele"].ToString();
                    refundmentOrderDoc.RefundmentType_NR = dr["flag"] == DBNull.Value ? -1 : int.Parse(dr["flag"].ToString());
                    //refundmentOrderDoc.PayCurrency = dr["PayCurrency"] == DBNull.Value ? -1 : int.Parse(dr["PayCurrency"].ToString());
                    refundmentOrderDoc.PayCurrency = 1;
                    //refundmentOrderDoc.PayMoney = dr["PayMoney"] == DBNull.Value ? -1 : decimal.Parse(dr["PayMoney"].ToString());
                    refundmentOrderDoc.PayMoney = 0;

                    refundmentOrderDoc.ExpectNum = dr["ExpectNum"] == DBNull.Value ? -1 : int.Parse(dr["ExpectNum"].ToString());
                    refundmentOrderDoc.Cause_TX = dr["Reason"].ToString();//dr["Cause"].ToString();
                    refundmentOrderDoc.Note_TX = dr["Note"].ToString();
                    refundmentOrderDoc.OperationPerson = dr["OperationPerson"].ToString();
                    refundmentOrderDoc.OriginalDocIDS = dr["OriginalDocID"].ToString();

                    //refundmentOrderDoc.Country = dr["Country"] == DBNull.Value ? -1 : int.Parse(dr["Country"].ToString());
                    //refundmentOrderDoc.CPCCode = dr["CPCCode"].ToString();
                    string cpccode_str = "";
                    if (dr["cpccode"] != null && dr["cpccode"].ToString().Trim() != "")
                    {
                        CityModel cm = CityDAL.GetCityInfoByCPCCode(dr["cpccode"].ToString());
                        cpccode_str = cm.Country + cm.Province + cm.City + cm.Xian + " ";
                    }
                    refundmentOrderDoc.Address_TX = cpccode_str + dr["Address"].ToString();
                    //refundmentOrderDoc.BankCode = dr["BankCode"].ToString();
                    //refundmentOrderDoc.BankBranch = dr["BankBranch"].ToString();

                    //refundmentOrderDoc.BankAddres = dr["BankAddres"].ToString();
                    //refundmentOrderDoc.BankBookName = dr["BankBookName"].ToString();
                    //refundmentOrderDoc.BankBook = dr["BankBook"].ToString();
                    //refundmentOrderDoc.BankCard = dr["BankCard"].ToString();
                    refundmentOrderDoc.RefundmentDate_DT = Convert.ToDateTime(dr["DocMakeTime"]);
                    refundmentOrderDoc.RefundTotalMoney = dr["totalmoney"] == DBNull.Value ? -1 : decimal.Parse(dr["totalmoney"].ToString());

                    //refundmentOrderDoc.Charged_NR = dr["Charged_NR"] == DBNull.Value ? -1 : decimal.Parse(dr["Charged_NR"].ToString());
                    //refundmentOrderDoc.ChargedReason_TX = dr["ChargedReason_TX"].ToString();
                    refundmentOrderDoc.OperateIP_TX = dr["OperateIP"].ToString();
                    refundmentOrderDoc.OperateNum_TX = dr["OperateNum"].ToString();
                    refundmentOrderDoc.MobileTele = ""; //dr["MobileTele"].ToString();
                    //refundmentOrderDoc.RefundmentDate_DT = Convert.ToDateTime(dr["RefundmentDate_DT"]);
                    //refundmentOrderDoc.RefundNumber_TX = dr["RefundNumber_TX"].ToString();
                    //refundmentOrderDoc.RefundTime_DT = dr["RefundTime_DT"] == DBNull.Value ? DateTime.Parse("1900-1-1") : DateTime.Parse(dr["RefundTime_DT"].ToString());// Convert.ToDateTime(dr["RefundTime_DT"]);
                    //refundmentOrderDoc.IsLock = dr["IsLock"] == DBNull.Value ? -1 : int.Parse(dr["IsLock"].ToString());


                }
                #region 获取明细值
                List<RefundmentOrderDocDetails> refundmentOrderDocDetails = new List<RefundmentOrderDocDetails>();
                string sqlDetails = "select * from InventoryDocDetails t where t.DocID=@DocID";
                SqlParameter[] para1 ={
										  new SqlParameter ("@docID" ,SqlDbType .Char ,20)
									  };
                para1[0].Value = DocID;
                DataTable dtDetails = DBHelper.ExecuteDataTable(sqlDetails, para1, CommandType.Text);
                foreach (DataRow dr in dtDetails.Rows)
                {
                    var refundmentOrderDocDetail = new RefundmentOrderDocDetails();
                    refundmentOrderDocDetail.DocDetailsID = dr["DocDetailsID"] == DBNull.Value ? -1 : int.Parse(dr["DocDetailsID"].ToString());
                    refundmentOrderDocDetail.DocID = dr["DocID"].ToString();
                    refundmentOrderDocDetail.OriginalDocID = refundmentOrderDoc.OriginalDocIDS;
                    refundmentOrderDocDetail.ProductID = dr["ProductID"] == DBNull.Value ? -1 : int.Parse(dr["ProductID"].ToString());
                    refundmentOrderDocDetail.ProductQuantity = dr["ProductQuantity"] == DBNull.Value ? -1 : int.Parse(dr["ProductQuantity"].ToString());

                    refundmentOrderDocDetail.UnitPrice = dr["UnitPrice"] == DBNull.Value ? -1 : decimal.Parse(dr["UnitPrice"].ToString());
                    refundmentOrderDocDetail.UnitPV = dr["PV"] == DBNull.Value ? -1 : decimal.Parse(dr["PV"].ToString());
                    refundmentOrderDocDetail.ExpectNum = dr["ExpectNum"] == DBNull.Value ? -1 : int.Parse(dr["ExpectNum"].ToString());
                    refundmentOrderDocDetails.Add(refundmentOrderDocDetail);
                }
                refundmentOrderDoc.RefundmentOrderDetails = refundmentOrderDocDetails;
                #endregion
                return refundmentOrderDoc;
            }
            catch (Exception ex)
            {
                msg = ex.Message.Trim();
                return null;
            }

        }

        /// <summary>
        /// 锁定退货单，只有未审核退货的单才能锁定
        /// </summary>
        /// <param name="docid"></param>
        /// <returns></returns>
        public bool LuckRefundmentOrderDoc(string docid)
        {
            bool flag = false;
            try
            {
                string sql = @" update RefundmentOrderDoc set IsLock=1 where  docid=@DocID and IsLock!=1 ";
                SqlParameter[] spas = new SqlParameter[] {
            new SqlParameter ("@DocID",SqlDbType .VarChar ,30)
            };
                spas[0].Value = docid;

                int iVal = DBHelper.ExecuteNonQuery(sql, spas, CommandType.Text);
                if (iVal > 0)
                    flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;

        }
        /// <summary>
        /// 解锁定退货单，只有己锁定，未审核退货的单才能锁定
        /// </summary>
        /// <param name="docid"></param>
        /// <returns></returns>
        public bool UnluckRefundmentOrderDoc(string docid)
        {
            bool flag = false;
            try
            {
                string sql = @" update RefundmentOrderDoc set IsLock=0 where  docid=@DocID and IsLock=1 ";
                SqlParameter[] spas = new SqlParameter[] {
            new SqlParameter ("@DocID",SqlDbType .VarChar ,30)
            };
                spas[0].Value = docid;

                int iVal = DBHelper.ExecuteNonQuery(sql, spas, CommandType.Text);
                if (iVal > 0)
                    flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;

        }

        /// <summary>
        /// 删除未审核的退货单
        /// </summary>
        /// <param name="docid"></param>
        /// <returns></returns>
        public bool DelRefundmentOrderDoc(string docid)
        {
            bool flag = true;
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                SqlTransaction tran = null;
                try
                {
                    int iVal = 0;
                    conn.Open();
                    tran = conn.BeginTransaction(); string sqlDT = @" select * from  InventoryDoc  where DocID=@DocID ";
                    SqlParameter[] para2 ={
                        new SqlParameter ("@DocID" ,SqlDbType.VarChar,30)
                    };
                    para2[0].Value = docid;
                    DataTable dt = DBHelper.ExecuteDataTable(tran, sqlDT, para2, CommandType.Text);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string OrderID = dt.Rows[0]["OriginalDocID"].ToString();
                        decimal TotalMoneyReturning = decimal.Parse(dt.Rows[0]["TotalMoney"].ToString());
                        decimal TotalPvReturning = decimal.Parse(dt.Rows[0]["TotalPV"].ToString());
                        string updateOrderSql = @" update MemberOrder set TotalMoneyReturning=isnull(TotalMoneyReturning,0)-@TotalMoneyReturning,TotalPvReturning=isnull(TotalPvReturning,0)-@TotalPvReturning
                                         where OrderID=@OrderID; ";
                        SqlParameter[] para3 ={
                            new SqlParameter ("@TotalMoneyReturning",SqlDbType.Decimal),
                            new SqlParameter ("@TotalPvReturning" ,SqlDbType .Decimal ),
                            new SqlParameter ("@OrderID" ,SqlDbType.VarChar ,20)
                        };
                        para3[0].Value = TotalMoneyReturning;
                        para3[1].Value = TotalPvReturning;
                        para3[2].Value = OrderID;
                        iVal = DBHelper.ExecuteNonQuery(tran, updateOrderSql, para3, CommandType.Text);
                        if (iVal <= 0)
                        {
                            flag = false;
                        }
                        DataTable dtDetails = GetRefundmentOrderDetailsByDocID(docid);
                        if (dtDetails != null && dtDetails.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtDetails.Rows)
                            {
                                OrderID = dr["OriginalDocID"].ToString();
                                int productid = int.Parse(dr["ProductID"].ToString());
                                int QuantityReturning = int.Parse(dr["QuantityReturning"].ToString());
                                string updateDetailsSql = " update MemberDetails set QuantityReturning=isnull(QuantityReturning,0)-@QuantityReturning where OrderID=@OrderID and ProductID=@ProductID; ";
                                SqlParameter[] para4 ={
                                    new SqlParameter ("@QuantityReturning",SqlDbType.Int),
                                    new SqlParameter ("@OrderID" ,SqlDbType.VarChar ,30),
                                    new SqlParameter ("@ProductID" ,SqlDbType.Int)
                                };
                                para4[0].Value = QuantityReturning;
                                para4[1].Value = OrderID;
                                para4[2].Value = productid;
                                iVal = DBHelper.ExecuteNonQuery(tran, updateDetailsSql, para4, CommandType.Text);
                                if (iVal <= 0)
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        string sql = @" delete from  InventoryDoc  where  docid=@DocID ";
                        SqlParameter[] spas = new SqlParameter[] {
                        new SqlParameter ("@DocID",SqlDbType .VarChar ,30)
                         };
                        spas[0].Value = docid; iVal = DBHelper.ExecuteNonQuery(tran, sql, spas, CommandType.Text);
                        if (iVal > 0)
                            flag = true;
                        if (flag)
                        {
                            sql = @" delete from  InventoryDocDetails  where  docid=@DocID  ";
                            SqlParameter[] spas1 = new SqlParameter[] {
                                new SqlParameter ("@DocID",SqlDbType .VarChar ,30)
                             };
                            spas1[0].Value = docid; iVal = DBHelper.ExecuteNonQuery(tran, sql, spas1, CommandType.Text);
                            if (iVal > 0)
                                flag = true;
                        }
                    }
                    if (flag)
                        tran.Commit();
                    else
                        tran.Rollback();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    flag = false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            return flag;
        }

        public DataTable GetCountryCityByCPCCode(string cpccode)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = @" select 
                    countryid, CountryCode,Name,CountryForShort,RateID,CountrySort_NR,CPCCode ,Country,Province ,City ,PostCode ,FullName ,Abridge
                    from v_CountryCity 
                    where 1=1 and  cpccode=@cpccode 
                    order by countrysort_nr asc ;";
                SqlParameter[] spas = new SqlParameter[] {
            new SqlParameter ("@cpccode",SqlDbType .NVarChar ,40)
            };
                spas[0].Value = cpccode;

                dt = DBHelper.ExecuteDataTable(sql, spas, CommandType.Text);
            }
            catch (Exception ex)
            {
                return null;
            }
            return dt;
        }
        /// <summary>
        /// 获取退货单的退单详细 DocID,OriginalDocID,ProductID ,ProductCode ,ProductName ,UnitPrice ,UnitPV,QuantityReturning,LeftQuantity,OrderQuantity
        /// </summary>
        /// <returns></returns>
        public DataTable GetRefundmentOrderDetailsByDocID(string docid)
        {
            /*
            DataTable dt = new DataTable();
            try
            {
                string sql = @" select 
                        DocID,OriginalDocID,ProductID ,ProductCode ,ProductName ,UnitPrice ,UnitPV,QuantityReturning,LeftQuantity,OrderQuantity	
                    from V_RefundmentDetails  
                    where 1=1 and  docid=@DocID 
                    order by OriginalDocID asc ;";
                SqlParameter[] spas = new SqlParameter[] {
            new SqlParameter ("@DocID",SqlDbType .VarChar ,30)
            };
                spas[0].Value = docid;

                dt = DBHelper.ExecuteDataTable(sql, spas, CommandType.Text);
            }
            catch (Exception ex)
            {
                return null;
            }
            return dt;
            */

            DataTable dt = new DataTable();
            try
            {
                string sql = @" select 
                        i.DocID, id.OriginalDocID ,i.ProductID ,ProductCode ,ProductName ,UnitPrice ,PV,productQuantity,productQuantity as QuantityReturning 	
                    from InventoryDocDetails i join InventoryDoc id on i.DocID=id.DocID join product p on p.productid=i.productid
                    where i.docid=@DocID ";
                SqlParameter[] spas = new SqlParameter[] {
            new SqlParameter ("@DocID",SqlDbType .VarChar ,30)
            };
                spas[0].Value = docid;

                dt = DBHelper.ExecuteDataTable(sql, spas, CommandType.Text);
            }
            catch (Exception ex)
            {
                return null;
            }
            return dt;
        }

        /// <summary>
        /// 会员退货审核时，添加一条汇款记录
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public int AddDataTOMemberRemittancesFromRefundOrder(SqlTransaction tran, RemittancesModel info)
        {
            string SQL = @"insert into Remittances(
                            ReceivablesDate,RemittancesDate,ImportBank,ImportNumber,RemittancesAccount,
                            RemittancesBank,SenderID,Sender,RemitNumber,
                            RemitMoney,StandardCurrency,[Use],PayExpectNum,PayWay,
                            Managers,ConfirmType,Remark,RemittancesCurrency,RemittancesMoney,
                            Photopath,OperateIP,OperateNum,Remittancesid,RemitStatus,
                            IsGSQR) 
                            values(
                            @ReceivablesDate,@RemittancesDate,@ImportBank,@ImportNumber,@RemittancesAccount,
                            @RemittancesBank,@SenderID,@Sender,@Number,
                            @RemitMoney,@StandardCurrency,@Use,@PayExpectNum,@PayWay,
                            @Managers,@ConfirmType,@Remark,@RemittancesCurrency,@RemittancesMoney,
                            @Photopath,@OperateIP,@OperateNum,@Remittancesid,@RemitStatus,
                            @IsGSQR)";


            SqlParameter[] para =
            {
                new SqlParameter("@ReceivablesDate",SqlDbType.DateTime),                //收款时间
                new SqlParameter("@RemittancesDate",SqlDbType.DateTime),                //汇出时间
                new SqlParameter("@ImportBank",SqlDbType.VarChar ,50),
                new SqlParameter("@ImportNumber",SqlDbType.VarChar ,20),                //汇入银行账户
                new SqlParameter("@RemittancesAccount",SqlDbType .VarChar ,20),         //汇出银行账户

                new SqlParameter("@RemittancesBank",SqlDbType.VarChar ,50),             //汇出银行
                new SqlParameter("@SenderID",SqlDbType .VarChar ,20),                   //汇出人身份证
                new SqlParameter("@Sender",SqlDbType .VarChar ,20),                     //汇款人          //汇款单号
                new SqlParameter("@Number",SqlDbType .VarChar ,30),                    //店铺编号/会员编号

                new SqlParameter("@RemitMoney",SqlDbType .Decimal),                     //标准币种(付款折合标准币种金额)
                new SqlParameter("@StandardCurrency",SqlDbType .Int),                   //汇出币种
                new SqlParameter("@Use",SqlDbType .Int),                                //用途
                new SqlParameter("@PayExpectNum",SqlDbType .Int),                       //支付期数   
                new SqlParameter("@PayWay",SqlDbType .Int),                             //支付方式  

                new SqlParameter("@Managers",SqlDbType.VarChar ,50),                    //经办人
                new SqlParameter("@ConfirmType",SqlDbType .Int),                        //付款的确认方式。包括:"传真","核实","电话"
                new SqlParameter("@Remark",SqlDbType .VarChar ,5000),                   //备注
                new SqlParameter("@RemittancesCurrency",SqlDbType .Int),        //汇出币种
                new SqlParameter("@RemittancesMoney",SqlDbType .Decimal),         //汇出金额    
                
                new SqlParameter("@Photopath",SqlDbType.VarChar ,5000),                    //传真件路径
                new SqlParameter("@OperateIP",SqlDbType .VarChar ,32),                    //汇出币种
                new SqlParameter("@OperateNum",SqlDbType .VarChar ,30),                                 //操作者编号
                new SqlParameter("@Remittancesid",SqlDbType.VarChar ,30),                       //汇款单编号   
                new SqlParameter("@RemitStatus",SqlDbType .Int),                             //支付方式  

                new SqlParameter("@IsGSQR",SqlDbType .Int)                             //支付方式  
            };
            if (info.ReceivablesDate != null && info.ReceivablesDate != DateTime.MinValue)
                para[0].Value = info.ReceivablesDate;
            else
                para[0].Value = DBNull.Value;
            if (info.RemittancesDate != null && info.RemittancesDate != DateTime.MinValue)
                para[1].Value = info.RemittancesDate;
            else
                para[1].Value = DBNull.Value;
            para[2].Value = info.ImportBank;
            para[3].Value = info.ImportNumber;
            para[4].Value = info.RemittancesAccount;

            //               RemittancesBank,SenderID,Sender,RemittancesNumber,StoreID,
            //               RemitMoney,StandardCurrency,[Use],PayExpectNum,PayWay,
            para[5].Value = info.RemittancesBank;
            para[6].Value = info.SenderID;
            para[7].Value = info.Sender;
            para[8].Value = info.RemitNumber;

            para[9].Value = info.RemitMoney;
            para[10].Value = info.StandardCurrency;
            para[11].Value = info.Use;
            para[12].Value = info.PayexpectNum;
            para[13].Value = info.PayWay;
            //ReceivablesDate,RemittancesDate,ImportBank,ImportNumber,RemittancesAccount,
            para[14].Value = info.Managers;
            para[15].Value = info.ConfirmType;
            para[16].Value = info.Remark;
            para[17].Value = info.RemittancesCurrency;
            para[18].Value = info.RemittancesMoney;
            //               Managers,ConfirmType,Remark,RemittancesCurrency,RemittancesMoney,

            para[19].Value = info.PhotoPath;
            para[20].Value = info.OperateIp;
            para[21].Value = info.OperateNum;
            para[22].Value = info.Remittancesid;
            para[23].Value = 1;//汇款人类型（0：店铺。1：会员。）

            para[24].Value = info.IsGSQR == true ? 1 : 0;
            //               Photopath,OperateIP,OperateNum,Remittancesid,Iszhifu) 
            return DBHelper.ExecuteNonQuery(tran, SQL, para, CommandType.Text);
        }


        #region 获得退款单据编号
        /// <summary>
        /// 获得退款单据编号
        /// </summary>
        /// <returns></returns>
        public string CreateRefundmentOrderDocIdByTypeCode()
        {
            string prefix = DocTypeCodeConst.DocType_THM;
            string date = MYDateTime.ToYYMMDDHHmmssString();
            string orderId = string.Empty;

            string sql = "SELECT Top 1 id FROM RefundmentOrderDoc Order By docID  Desc ";
            SqlDataReader reader = DBHelper.ExecuteReader(sql);
            if (reader.Read())
                orderId += (reader.GetInt32(0) + 1).ToString();
            else
                orderId += "1";

            reader.Close();
            if (orderId.Length < 5)
            {
                orderId = orderId.PadLeft(5, '0');
            }

            return prefix + date + orderId;
        }
        /// <summary>
        /// 创建会员汇款单号M+YYMMDD{ID}
        /// </summary>
        /// <returns></returns>
        public string CreateMemberRemittancesID(SqlTransaction tran)
        {
            string prefix = "M";
            string date = MYDateTime.ToYYMMDDHHmmssString();
            string orderId = string.Empty;

            string sql = "SELECT Top 1 id FROM MemberRemittances Order By id  Desc ";
            DataTable dt = DBHelper.ExecuteDataTable(tran, sql, null, CommandType.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                orderId += (Convert.ToInt32(dt.Rows[0]["id"]) + 1).ToString();
            }
            else
                orderId += "1";
            if (orderId.Length < 6)
            {
                orderId = orderId.PadLeft(6, '0');
            }

            return prefix + date + orderId;
        }
        /// <summary>
        /// 创建会员汇款单号M+YYMMDD{ID}
        /// </summary>
        /// <returns></returns>
        public string CreateMemberRemittancesID()
        {
            string prefix = "M";
            string date = MYDateTime.ToYYMMDDHHmmssString();
            string orderId = string.Empty;

            string sql = "SELECT Top 1 id FROM MemberRemittances Order By id  Desc ";
            SqlDataReader reader = DBHelper.ExecuteReader(sql);
            if (reader.Read())
                orderId += (reader.GetInt32(0) + 1).ToString();
            else
                orderId += "1";

            reader.Close();
            if (orderId.Length < 6)
            {
                orderId = orderId.PadLeft(6, '0');
            }

            return prefix + date + orderId;
        }
        #endregion

        /// <summary>
        /// 填写会员退货退款单
        /// </summary>
        /// <param name="refundmentOrder"></param>
        /// <param name="isEdit"></param>
        /// <param name="OperateBh"></param>
        /// <param name="OperateIP"></param>
        /// <param name="qishu"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool UpdateRefundmentOrderDocForBill(RefundmentOrderDocModel refundmentOrder, bool isEdit, string OperateBh, string OperateIP, int qishu, ref string msg)
        {
            string sqlRefund = @" update RefundmentOrderDoc set RefundNumber_TX=@RefundNumber_TX,RefundTime_DT=@RefundTime_DT,StatusFlag_NR=@StatusFlag_NR,
                        RefundTotalMoney=@RefundTotalMoney,Charged_NR=@Charged_NR,ChargedReason_TX=@ChargedReason_TX where DocID=@DocID ";
            SqlParameter[] para ={
										  new SqlParameter ("@RefundNumber_TX",SqlDbType.VarChar,30),
										  new SqlParameter ("@RefundTime_DT" ,SqlDbType .DateTime ),
										  new SqlParameter ("@StatusFlag_NR" ,SqlDbType.Int ),
										  new SqlParameter ("@RefundTotalMoney" ,SqlDbType.Decimal),
										  new SqlParameter ("@Charged_NR",SqlDbType.Decimal),
										  new SqlParameter ("@ChargedReason_TX" ,SqlDbType.NVarChar ,1000 ),
										  new SqlParameter ("@DocID" ,SqlDbType.VarChar,30)
									  };
            para[0].Value = refundmentOrder.RefundNumber_TX;
            para[1].Value = DateTime.Now;
            para[2].Value = refundmentOrder.StatusFlag_NR;
            para[3].Value = refundmentOrder.RefundTotalMoney;
            para[4].Value = refundmentOrder.Charged_NR;
            para[5].Value = refundmentOrder.ChargedReason_TX;
            para[6].Value = refundmentOrder.DocID;

            bool flag = true;
            double totalFmoney = Convert.ToDouble(refundmentOrder.TotalMoney);
            string memberid = refundmentOrder.OwnerNumber_TX;
            string DocID = refundmentOrder.DocID;
            int iVal = 0;
            int RefundmentType_NR = refundmentOrder.RefundmentType_NR;

            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        RemittancesModel remittances = new RemittancesModel();
                        string updateAccount = string.Empty;
                        if (isEdit)
                        {//修改                            
                            double Fmoney = GetRefundedMoney(tran, DocID);
                            //添加还原退货款对账单
                            D_AccountDAL.AddAccount(memberid, totalFmoney, D_AccountSftype.MemberType, D_AccountKmtype.ReturnRebate, DirectionEnum.AccountReduced, "返回上次会员退货单款" + DocID, tran);
                            // AddOrderDataDAL.AddDataTORemittances(tran, "", memberid, (Fmoney * (-1)), DocID, OperateIP, OperateBh, qishu);
                            remittances = CreateNewMemberRemittances(refundmentOrder, OperateBh, OperateIP, qishu, tran);
                            remittances.Remark = "返回上次会员退货单款" + DocID;
                            remittances.RemittancesMoney = Convert.ToDecimal(Fmoney) * (-1);
                            remittances.RemitMoney = remittances.RemittancesMoney;
                            iVal = AddDataTOMemberRemittancesFromRefundOrder(tran, remittances);
                            if (iVal < 1)
                                flag = false;
                            if (flag)
                            {
                                //还原上次的退货款，以便重新修改退款金额
                                updateAccount = "update memberInfo set Jackpot=isnull(Jackpot,0)-" + Fmoney + " where number='" + refundmentOrder.OwnerNumber_TX.Trim() + "'";
                                iVal = DBHelper.ExecuteNonQuery(tran, updateAccount, null, CommandType.Text);
                                if (iVal < 1)
                                    flag = false;
                            }
                            #region 公共部分
                            /*
                            //添加退款对账单
                            D_AccountDAL.AddAccount(memberid, totalFmoney, D_AccountSftype.MemberType, D_AccountKmtype.ReturnRebate, DirectionEnum.AccountsIncreased, "会员退货单退款" + DocID, tran);
                            DBHelper.ExecuteNonQuery(tran, sqlRefund, para, CommandType.Text);
                            remittances = CreateNewMemberRemittances(refundmentOrder, OperateBh, OperateIP, qishu);
                            remittances.Remark = "会员退货单退款" + DocID;
                            remittances.RemittancesMoney = Convert.ToDecimal(totalFmoney);
                            iVal = AddDataTOMemberRemittancesFromRefundOrder(tran, remittances);
                            //更新金额
                            string updateAccountSql = "update memberInfo set Jackpot=isnull(Jackpot,0)+" + refundmentOrder.TotalMoney + " where number='" + refundmentOrder.OwnerNumber_TX.Trim() + "'";
                            DBHelper.ExecuteNonQuery(tran, updateAccountSql, null, CommandType.Text);

                            //添加扣款对账单
                            D_AccountDAL.AddAccount(memberid, totalFmoney, D_AccountSftype.StoreType, D_AccountKmtype.ReturnRebate, DirectionEnum.AccountReduced, "会员退货单扣款" + DocID, tran);
                            remittances = CreateNewMemberRemittances(refundmentOrder, OperateBh, OperateIP, qishu);
                            remittances.Remark = "会员退货单扣款" + DocID;
                            remittances.RemittancesMoney = Convert.ToDecimal(refundmentOrder.Charged_NR) * (-1);
                            iVal = AddDataTOMemberRemittancesFromRefundOrder(tran, remittances);

                            //AddOrderDataDAL.AddDataTORemittances(tran, "", memberid, (money * (-1)), DocID, OperateIP, OperateBh, qishu);
                            updateAccount = "update memberInfo set Jackpot=isnull(Jackpot,0)-" + Convert.ToDecimal(refundmentOrder.Charged_NR) + " where number='" + refundmentOrder.OwnerNumber_TX.Trim() + "'";
                            DBHelper.ExecuteNonQuery(tran, updateAccount, null, CommandType.Text);
                           

                           // DBHelper.ExecuteNonQuery(tran, "update storeinfo set totalaccountmoney=totalaccountmoney - @money where storeid=@storeid", new SqlParameter[2] { new SqlParameter("@money", money), new SqlParameter("@storeid", StoreID) }, CommandType.Text);
                             */
                            #endregion
                        }
                        else
                        {//添加
                            #region 公共部分
                            /*
                            //添加退款对账单
                            D_AccountDAL.AddAccount(memberid, totalFmoney, D_AccountSftype.MemberType, D_AccountKmtype.ReturnRebate, DirectionEnum.AccountsIncreased, "会员退货单退款" + DocID, tran);
                            DBHelper.ExecuteNonQuery(tran, sqlRefund, para, CommandType.Text);
                            remittances = CreateNewMemberRemittances(refundmentOrder, OperateBh, OperateIP, qishu);
                            remittances.Remark = "会员退货单退款" + DocID;
                            remittances.RemittancesMoney = Convert.ToDecimal(totalFmoney);
                            iVal = AddDataTOMemberRemittancesFromRefundOrder(tran, remittances);
                            //更新金额
                            string updateAccountSql = "update memberInfo set Jackpot=isnull(Jackpot,0)+" + refundmentOrder.TotalMoney + " where number='" + refundmentOrder.OwnerNumber_TX.Trim() + "'";
                            DBHelper.ExecuteNonQuery(tran, updateAccountSql, null, CommandType.Text);

                            //添加扣款对账单
                            D_AccountDAL.AddAccount(memberid, totalFmoney, D_AccountSftype.StoreType, D_AccountKmtype.ReturnRebate, DirectionEnum.AccountReduced, "会员退货单扣款" + DocID, tran);
                            remittances = CreateNewMemberRemittances(refundmentOrder, OperateBh, OperateIP, qishu);
                            remittances.Remark = "会员退货单扣款" + DocID;
                            remittances.RemittancesMoney = Convert.ToDecimal(refundmentOrder.Charged_NR) * (-1);
                            iVal = AddDataTOMemberRemittancesFromRefundOrder(tran, remittances);

                            //AddOrderDataDAL.AddDataTORemittances(tran, "", memberid, (money * (-1)), DocID, OperateIP, OperateBh, qishu);
                            updateAccount = "update memberInfo set Jackpot=isnull(Jackpot,0)-" + Convert.ToDecimal(refundmentOrder.Charged_NR) + " where number='" + refundmentOrder.OwnerNumber_TX.Trim() + "'";
                            DBHelper.ExecuteNonQuery(tran, updateAccount, null, CommandType.Text);
                           

                           // DBHelper.ExecuteNonQuery(tran, "update storeinfo set totalaccountmoney=totalaccountmoney - @money where storeid=@storeid", new SqlParameter[2] { new SqlParameter("@money", money), new SqlParameter("@storeid", StoreID) }, CommandType.Text);
                             */
                            #endregion

                        }

                        //添加退款对账单--1
                        if (flag)
                        {
                            D_AccountDAL.AddAccount(memberid, totalFmoney, D_AccountSftype.MemberType, D_AccountKmtype.ReturnRebate, DirectionEnum.AccountsIncreased, "会员退货单退款" + DocID, tran);
                            remittances = CreateNewMemberRemittances(refundmentOrder, OperateBh, OperateIP, qishu, tran);
                            remittances.Remark = "会员退货单退款" + DocID;
                            remittances.RemittancesMoney = Convert.ToDecimal(totalFmoney);
                            iVal = AddDataTOMemberRemittancesFromRefundOrder(tran, remittances);
                            if (iVal < 1)
                                flag = false;
                        }
                        //更新金额--2
                        if (flag)
                        {
                            updateAccount = "update memberInfo set Jackpot=isnull(Jackpot,0)+" + refundmentOrder.TotalMoney + " where number='" + refundmentOrder.OwnerNumber_TX.Trim() + "'";
                            iVal = DBHelper.ExecuteNonQuery(tran, updateAccount, null, CommandType.Text);
                            if (iVal < 1)
                                flag = false;
                        }

                        //添加扣款对账单--3
                        if (flag)
                        {
                            D_AccountDAL.AddAccount(memberid, Convert.ToDouble(refundmentOrder.Charged_NR), D_AccountSftype.StoreType, D_AccountKmtype.ReturnRebate, DirectionEnum.AccountReduced, "会员退货单扣款" + DocID, tran);
                            remittances = CreateNewMemberRemittances(refundmentOrder, OperateBh, OperateIP, qishu, tran);
                            remittances.Remark = "会员退货单扣款" + DocID;
                            remittances.RemittancesMoney = Convert.ToDecimal(refundmentOrder.Charged_NR) * (-1);
                            iVal = AddDataTOMemberRemittancesFromRefundOrder(tran, remittances);
                            if (iVal < 1)
                                flag = false;
                        }
                        //更新金额--4
                        if (flag)
                        {
                            updateAccount = "update memberInfo set Jackpot=isnull(Jackpot,0)-" + Convert.ToDecimal(refundmentOrder.Charged_NR) + " where number='" + refundmentOrder.OwnerNumber_TX.Trim() + "'";
                            iVal = DBHelper.ExecuteNonQuery(tran, updateAccount, null, CommandType.Text);
                            if (iVal < 1)
                                flag = false;
                        }
                        //更新退货单信息 --5
                        #region
                        iVal = DBHelper.ExecuteNonQuery(tran, sqlRefund, para, CommandType.Text);
                        if (iVal < 1)
                            flag = false;
                        #endregion

                        if (flag)
                        {
                            tran.Commit();
                        }
                        else
                        {
                            tran.Rollback();
                        }

                    }
                    catch (Exception ex1)
                    {
                        msg = "操作失败：" + ex1.Message;
                        tran.Rollback();
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return flag;

        }

        public RemittancesModel CreateNewMemberRemittances(RefundmentOrderDocModel refundmentOrder, string OperateBh, string OperateIP, int qishu, SqlTransaction tran)
        {
            #region 生成汇款单
            RemittancesModel remittances = new RemittancesModel();
            string bankCode = refundmentOrder.BankCode;
            DataTable dtBank = CommonDataDAL.GetCountryBankByBankCode(tran, bankCode);
            if (dtBank != null && dtBank.Rows.Count > 0)
            {
                remittances.BankID = Convert.ToInt32(dtBank.Rows[0]["BankID"]);
                remittances.ImportBank = dtBank.Rows[0]["BankName"].ToString() + refundmentOrder.BankBranch;
            }
            else
                remittances.BankID = -1;

            remittances.ConfirmType = -1;
            remittances.ImportNumber = refundmentOrder.BankCard;
            remittances.IsGSQR = true;
            remittances.Managers = refundmentOrder.RefundNumber_TX;
            remittances.RemitNumber = refundmentOrder.OwnerNumber_TX;
            remittances.OperateIp = OperateIP;
            remittances.OperateNum = OperateBh;
            remittances.PayexpectNum = qishu;
            remittances.PayWay = refundmentOrder.RefundmentType_NR;
            remittances.PhotoPath = string.Empty;
            remittances.ReceivablesDate = DateTime.Now;
            remittances.Remark = "退货退款";
            remittances.RemitMoney = refundmentOrder.PayMoney;
            remittances.RemittancesAccount = string.Empty;
            remittances.RemittancesBank = string.Empty;
            remittances.RemittancesCurrency = 1;
            remittances.RemittancesDate = DateTime.Now;
            remittances.Remittancesid = CreateMemberRemittancesID(tran);
            remittances.RemittancesMoney = 0;
            remittances.Sender = string.Empty;
            remittances.SenderID = string.Empty;
            remittances.StandardCurrency = 1;
            remittances.Use = 4;
            return remittances;

            #endregion
        }

        /// <summary>
        /// 获取是否已退款 退款额——ds2012——tianfeng
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="docID"></param>
        /// <returns></returns>
        public static double GetRefundedMoney(SqlTransaction tran, string docID)
        {
            double money = 0.00;
            string sql = "select TotalMoney-Charged as [money] from V_RefundmentOrderDocForBill where docid=@docid";
            SqlParameter[] parameter = new SqlParameter[] { 
                new SqlParameter("@docid",SqlDbType.VarChar,20)
            };
            parameter[0].Value = docID;
            DataTable dt = DBHelper.ExecuteDataTable(tran, sql, parameter, CommandType.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                money = double.Parse(dt.Rows[0]["money"].ToString());
            }
            return money;
        }

    }

}