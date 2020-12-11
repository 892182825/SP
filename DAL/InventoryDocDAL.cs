using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

//Add Namespace
using System.Data;
using System.Data.SqlClient;
using Model;
using Model.Other;

/*
 * 修改者：     汪  华
 * 修改时间：   2009-09-09
 */

namespace DAL
{
    /// <summary>
    /// 单据
    /// </summary>
    public class InventoryDocDAL
    {
        #region print
        /// <summary>
        /// Get inventory details by docID(Write by WangHua at 2009-11-26)
        /// </summary>
        /// <param name="docID"></param>
        /// <returns>return PrintInventoryDoc model</returns>
        public static PrintInventoryDoc PrintInventoryDocDetails(string docID)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@docID",SqlDbType.NVarChar,50)
            };
            sparams[0].Value = docID;
            SqlDataReader reader = DBHelper.ExecuteReader("PrintInventoryDocEDetails", sparams, CommandType.StoredProcedure);
            PrintInventoryDoc print = null;
            while (reader.Read())
            {
                print = new PrintInventoryDoc();
                print.Address = reader["Address"].ToString();
                print.BatchCode = reader["BatchCode"].ToString();
                if ("" != reader["CloseDate"].ToString())
                    print.CloseDate = Convert.ToDateTime(reader["CloseDate"].ToString());
                else
                    print.CloseDate = DateTime.MinValue;
                if ("" != reader["DocAuditTime"].ToString())
                    print.DocAuditTime = Convert.ToDateTime(reader["DocAuditTime"]);
                else
                    print.DocAuditTime = DateTime.MinValue;
                print.CloseFlag = Convert.ToInt32(reader["CloseFlag"]);
                //print.CurrencyName = reader["Name"].ToString();
                print.DocAuditer = reader["DocAuditer"].ToString();

                print.DocID = reader["DocID"].ToString();
                //print.DocCode = reader["cause"].ToString();
                print.DocMaker = reader["DocMaker"].ToString();
                if ("" != reader["DocMakeTime"].ToString())
                    print.DocMakeTime = Convert.ToDateTime(reader["DocMakeTime"]);
                else
                    print.DocMakeTime = DateTime.MinValue;
                if ("" != reader["DocSecondAuditTime"].ToString())
                    print.DocSecondAuditTime = Convert.ToDateTime(reader["DocSecondAuditTime"]);
                else
                    print.DocSecondAuditTime = DateTime.MinValue;

                print.DocTypeNames = reader["DocTypeName"].ToString();
                print.IsRubric = Convert.ToInt32(reader["isRubric"]);

                print.Note = reader["Note"].ToString();
                print.OperateNum = reader["OperateNum"].ToString();
                print.ProviderName = reader["ProviderName"].ToString();
                print.StateFlag = Convert.ToInt32(reader["StateFlag"].ToString());
                print.StoreID = reader["Client"].ToString();
                if (reader["TotalMoney"].ToString() == "" || reader["TotalMoney"] == null)
                {
                    print.TotalMoney = Convert.ToDecimal(0);
                }
                else
                {
                    print.TotalMoney = Convert.ToDecimal(reader["TotalMoney"]);
                }
                if (reader["Totalpv"].ToString() == "" || reader["Totalpv"] == null)
                {
                    print.Totalpv = Convert.ToDecimal(0);
                }
                else
                {
                    print.Totalpv = Convert.ToDecimal(reader["Totalpv"]);
                }

                print.WareHouseName = reader["WareHouseName"].ToString();
                print.SeatName = reader["SeatName"].ToString();
            }
            reader.Close();
            return print;
        }

        public static DataTable PrintMemberOrderDocDetails(string OrderID)
        {
            SqlParameter[] para = {
                                      new SqlParameter("@OrderID",SqlDbType.NVarChar,30)
                                  };
            para[0].Value = OrderID;
            DataTable dt = DBHelper.ExecuteDataTable("PrintMemberOrderDocEDetails", para, CommandType.StoredProcedure);
            return dt;
        }

        /// <summary>
        /// Display inventory details by docID(Write by WangHua at 2009-11-26)
        /// </summary>
        /// <param name="docID">docID</param>
        /// <returns>return DataTable object</returns>
        public static DataTable DisplayInventoryDocDetails(string docID)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            { 
                new SqlParameter("@docID",SqlDbType.NVarChar,50)
            };
            sparams[0].Value = docID;
            return DBHelper.ExecuteDataTable("DisplayInventoryDocDetails", sparams, CommandType.StoredProcedure);
        }

        public static DataTable DisplayMemberOrderDocDetails(string OrderID)
        {
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@OrderID",SqlDbType.NVarChar,30)
            };
            para[0].Value = OrderID;
            DataTable dt = DBHelper.ExecuteDataTable("MemberOrderDocDetails", para, CommandType.StoredProcedure);
            return dt;

        }

        #endregion

        #region 报溢
        public bool ProductReportProfit(double sun, int productid, int warehouseid, int DepotSeatID)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@sun",sun),
                new SqlParameter("@productid",productid),
                new SqlParameter("@DepotSeatID",DepotSeatID),
                new SqlParameter("@warehouseid",warehouseid)
            };
            if (DBHelper.ExecuteNonQuery("ProductReportProfit", param, CommandType.StoredProcedure) > 0)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 库存报损
        /// <summary>
        /// 库存报损
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public bool addReportDemage(string DocAuditer, string OperateIP, string OperateNum, string DocID)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@DocAuditer",SqlDbType.VarChar,50),
                 new SqlParameter("@OperateIP",SqlDbType.NVarChar,50),
                  new SqlParameter("@OperateNum",SqlDbType.NVarChar,50),
                 new SqlParameter("@DocID",SqlDbType.NVarChar,20)
            };
            param[0].Value = DocAuditer;
            param[1].Value = OperateIP;
            param[2].Value = OperateNum;
            param[3].Value = DocID;
            if (DBHelper.ExecuteNonQuery("update InventoryDoc set DocAuditer=@DocAuditer,DocAuditTime=getdate(),OperateIP=@OperateIP,OperateNum=@OperateNum where DocID=@DocID", param, CommandType.Text) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改金额(报损)
        /// </summary>
        /// <param name="sun"></param>
        /// <param name="productid"></param>
        /// <param name="DepotSeatID"></param>
        /// <returns></returns>
        public string ProductReportDamage(double sun, int productid, int warehouseid, int DepotSeatID, string mode, InventoryDocDetailsModel opda_docDetail)
        {
            string rt = "";
            SqlTransaction tran = null;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(DBHelper.connString);

                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                tran = con.BeginTransaction();
                cmd.Transaction = tran;

                cmd.CommandText = @"update ProductQuantity set " + mode + "=" + mode + "+" + sun + " where ProductID=" + productid + " and DepotSeatID=" + DepotSeatID + " and WareHouseID=" + warehouseid;

                int hs = cmd.ExecuteNonQuery();

                if (hs == 1)
                {
                    cmd.CommandText = @"update LogicProductInventory set " + mode + "=" + mode + "+" + sun + " where ProductID=" + productid;

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        cmd.CommandText = @"insert into InventoryDocDetails( DocID, ProductID,ProductQuantity,UnitPrice,MeasureUnit,PV, ExpectNum, ProductTotal) values
                                        ( @DocID, @ProductID,@ProductQuantity, @UnitPrice,@MeasureUnit, @PV, @ExpectNum, @ProductTotal)";

                        cmd.Parameters.AddWithValue("@DocID", opda_docDetail.DocID);
                        cmd.Parameters.AddWithValue("@ProductID", opda_docDetail.ProductID);
                        cmd.Parameters.AddWithValue("@ProductQuantity", opda_docDetail.ProductQuantity);
                        cmd.Parameters.AddWithValue("@UnitPrice", opda_docDetail.UnitPrice);
                        cmd.Parameters.AddWithValue("@MeasureUnit", opda_docDetail.MeasureUnit);
                        cmd.Parameters.AddWithValue("@PV", opda_docDetail.PV);
                        cmd.Parameters.AddWithValue("@ExpectNum", opda_docDetail.ExpectNum);
                        //cmd.Parameters.AddWithValue("@SelectedIndex", opda_docDetail.SelectedIndex);
                        cmd.Parameters.AddWithValue("@ProductTotal", opda_docDetail.ProductTotal);

                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            rt = "1";//成功
                        }
                        else
                        {
                            rt = "-2";//插入失败
                        }

                    }
                    else
                    {
                        rt = "-1";//更新失败
                    }
                }
                else
                {
                    rt = "0";//更新失败，在此仓库上不存在此条记录
                }

                if (rt != "1")
                    tran.Rollback();
                else if (rt == "1")  //成功
                    tran.Commit();

                cmd.Dispose();
                con.Close();

                return rt;
            }
            catch (Exception)
            {
                if (tran != null)
                    tran.Rollback();
                if (con != null)
                    con.Close();

                return "-3";  // 异常
            }
        }

        /// <summary>
        /// 获取报损列表   ---DS2012
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public static DataTable GetReportList(string sqlWhere)
        {
            return DBHelper.ExecuteDataTable("select dbo.GetTypeName(a.DocTypeID) as DocTypename, a.DocID, a.DocMakeTime,a.DocMaker,a.TotalMoney, a.TotalPV, a.ExpectNum, a.DocAuditer, a.DocAuditTime from InventoryDoc a left join Country b on a.Currency=b.ID where " + sqlWhere + " order by a.id desc", CommandType.Text);
        }

        //库存报溢 
        public static string ProductReportDamage_II(ArrayList inventoryDocDetailsModels, int warehouseid, int DepotSeatID, string mode, InventoryDocModel inventoryDocModel)
        {
            string rt = "-5";
            SqlTransaction tran = null;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(DBHelper.connString);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                tran = con.BeginTransaction();
                cmd.Transaction = tran;

                cmd.CommandText = @"INSERT INTO InventoryDoc(DocAuditer,OperateIP,DocAuditTime,OperateNum,
								DocTypeID,DocID, DocMakeTime, 
								 DocMaker,
								Provider, Client, WareHouseID,DepotSeatID, TotalMoney, TotalPV,
								 ExpectNum,
								Note,StateFlag , OriginalDocID)
						VALUES( @DocAuditer,@OperateIP,@DocAuditTime,@OperateNum,
                                @int_docTypeID, @cha_docID, @dat_docMakeTime, 
								@cha_docMaker, 
								@vch_provider, @cha_client, @warehouseID,@DepotSeatID, @num_totalMoney, @num_totalPV,
								 @int_qishu,
								 @nvch_note ,@StateFlag ,@OriginalDocID)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@DocAuditer", inventoryDocModel.DocAuditer);
                cmd.Parameters.AddWithValue("@OperateIP", inventoryDocModel.OperateIP);
                cmd.Parameters.AddWithValue("@DocAuditTime", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@OperateNum", inventoryDocModel.OperateNum);
                cmd.Parameters.AddWithValue("@int_docTypeID", inventoryDocModel.DocTypeID);
                cmd.Parameters.AddWithValue("@cha_docID", inventoryDocModel.DocID);
                cmd.Parameters.AddWithValue("@dat_docMakeTime", inventoryDocModel.DocMakeTime);
                cmd.Parameters.AddWithValue("@cha_docMaker", inventoryDocModel.DocMaker);
                cmd.Parameters.AddWithValue("@vch_provider", inventoryDocModel.Provider);
                cmd.Parameters.AddWithValue("@cha_client", inventoryDocModel.Client);
                cmd.Parameters.AddWithValue("@warehouseID", inventoryDocModel.WareHouseID);
                cmd.Parameters.AddWithValue("@DepotSeatID", inventoryDocModel.DepotSeatID);
                cmd.Parameters.AddWithValue("@num_totalMoney", inventoryDocModel.TotalMoney);
                cmd.Parameters.AddWithValue("@num_totalPV", inventoryDocModel.TotalPV);
                //cmd.Parameters.AddWithValue("@cha_motherID", inventoryDocModel.MotherID);
                cmd.Parameters.AddWithValue("@int_qishu", inventoryDocModel.ExpectNum);
                //cmd.Parameters.AddWithValue("@vch_Cause", inventoryDocModel.Cause);
                cmd.Parameters.AddWithValue("@nvch_note", inventoryDocModel.Note);
                cmd.Parameters.AddWithValue("@StateFlag", inventoryDocModel.StateFlag);
                cmd.Parameters.AddWithValue("@OriginalDocID", inventoryDocModel.OriginalDocID);
                //cmd.Parameters.AddWithValue("@Currency", inventoryDocModel.Currency);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    for (int i = 0; i < inventoryDocDetailsModels.Count; i++)
                    {
                        InventoryDocDetailsModel opda_docDetail = (InventoryDocDetailsModel)inventoryDocDetailsModels[i];
                        cmd.CommandText = @"update ProductQuantity set " + mode + "=" + mode + "+" + opda_docDetail.ProductQuantity + " where ProductID=" + opda_docDetail.ProductID + " and DepotSeatID=" + DepotSeatID + " and WareHouseID=" + warehouseid;

                        int hs = cmd.ExecuteNonQuery();

                        if (hs == 1)
                        {
                            cmd.CommandText = @"update LogicProductInventory set " + mode + "=" + mode + "+" + opda_docDetail.ProductQuantity + " where ProductID=" + opda_docDetail.ProductID;

                            if (cmd.ExecuteNonQuery() == 1)
                            {
                                cmd.CommandText = @"insert into InventoryDocDetails( DocID, ProductID,ProductQuantity,UnitPrice,MeasureUnit,PV, ExpectNum,ProductTotal) values
                                        ( @DocID, @ProductID,@ProductQuantity, @UnitPrice,@MeasureUnit, @PV, @ExpectNum, @ProductTotal)";

                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@DocID", inventoryDocModel.DocID);
                                cmd.Parameters.AddWithValue("@ProductID", opda_docDetail.ProductID);
                                cmd.Parameters.AddWithValue("@ProductQuantity", opda_docDetail.ProductQuantity);
                                cmd.Parameters.AddWithValue("@UnitPrice", opda_docDetail.UnitPrice);
                                cmd.Parameters.AddWithValue("@MeasureUnit", opda_docDetail.MeasureUnit);
                                cmd.Parameters.AddWithValue("@PV", opda_docDetail.PV);
                                cmd.Parameters.AddWithValue("@ExpectNum", opda_docDetail.ExpectNum);
                                //cmd.Parameters.AddWithValue("@SelectedIndex", opda_docDetail.SelectedIndex);
                                cmd.Parameters.AddWithValue("@ProductTotal", opda_docDetail.ProductTotal);
                                //cmd.Parameters.AddWithValue("@DepotSeatID", opda_docDetail.DepotSeatID);
                                if (cmd.ExecuteNonQuery() == 1)
                                {
                                    rt = "1";//成功
                                }
                                else
                                {
                                    rt = "-2";//插入失败
                                    break;
                                }

                            }
                            else
                            {
                                rt = "-1";//更新失败
                                break;
                            }
                        }
                        else
                        {
                            rt = "-3";//更新失败，在此仓库上不存在此条记录
                            break;
                        }
                    }

                }
                else
                {
                    rt = "0"; //
                }


                if (rt != "1")
                    tran.Rollback();
                else if (rt == "1")  //成功
                    tran.Commit();

                cmd.Dispose();
                con.Close();

                return rt;
            }
            catch (Exception)
            {
                if (tran != null)
                    tran.Rollback();
                if (con != null)
                    con.Close();

                return "-4";  // 异常
            }
        }
        //库存报损 
        public static string ProductReportEDamage_II(ArrayList inventoryDocDetailsModels, int warehouseid, int DepotSeatID, string mode, InventoryDocModel inventoryDocModel)
        {
            string rt = "-5";
            SqlTransaction tran = null;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(DBHelper.connString);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                tran = con.BeginTransaction();
                cmd.Transaction = tran;

                cmd.CommandText = @"INSERT INTO InventoryDoc(DocAuditer,OperateIP,DocAuditTime,OperateNum,
								DocTypeID,DocID, DocMakeTime, 
								 DocMaker,
								Provider, Client, inWareHouseID,inDepotSeatID, TotalMoney, TotalPV,
								 ExpectNum,
								 Note,StateFlag , OriginalDocID)
						VALUES( @DocAuditer,@OperateIP,@DocAuditTime,@OperateNum,
                                @int_docTypeID, @cha_docID, @dat_docMakeTime, 
								@cha_docMaker, 
								@vch_provider, @cha_client, @warehouseID,@DepotSeatID, @num_totalMoney, @num_totalPV,
								 @int_qishu,
								 @nvch_note ,@StateFlag ,@OriginalDocID)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@DocAuditer", inventoryDocModel.DocAuditer);
                cmd.Parameters.AddWithValue("@OperateIP", inventoryDocModel.OperateIP);
                cmd.Parameters.AddWithValue("@DocAuditTime", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@OperateNum", inventoryDocModel.OperateNum);
                cmd.Parameters.AddWithValue("@int_docTypeID", inventoryDocModel.DocTypeID);
                cmd.Parameters.AddWithValue("@cha_docID", inventoryDocModel.DocID);
                cmd.Parameters.AddWithValue("@dat_docMakeTime", inventoryDocModel.DocMakeTime);
                cmd.Parameters.AddWithValue("@cha_docMaker", inventoryDocModel.DocMaker);
                cmd.Parameters.AddWithValue("@vch_provider", inventoryDocModel.Provider);
                cmd.Parameters.AddWithValue("@cha_client", inventoryDocModel.Client);
                cmd.Parameters.AddWithValue("@warehouseID", inventoryDocModel.WareHouseID);
                cmd.Parameters.AddWithValue("@DepotSeatID", inventoryDocModel.DepotSeatID);
                cmd.Parameters.AddWithValue("@num_totalMoney", inventoryDocModel.TotalMoney);
                cmd.Parameters.AddWithValue("@num_totalPV", inventoryDocModel.TotalPV);
                //cmd.Parameters.AddWithValue("@cha_motherID", inventoryDocModel.MotherID);
                cmd.Parameters.AddWithValue("@int_qishu", inventoryDocModel.ExpectNum);
                //cmd.Parameters.AddWithValue("@vch_Cause", inventoryDocModel.Cause);
                cmd.Parameters.AddWithValue("@nvch_note", inventoryDocModel.Note);
                cmd.Parameters.AddWithValue("@StateFlag", inventoryDocModel.StateFlag);
                cmd.Parameters.AddWithValue("@OriginalDocID", inventoryDocModel.OriginalDocID);
                //cmd.Parameters.AddWithValue("@Currency", inventoryDocModel.Currency);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    for (int i = 0; i < inventoryDocDetailsModels.Count; i++)
                    {
                        InventoryDocDetailsModel opda_docDetail = (InventoryDocDetailsModel)inventoryDocDetailsModels[i];
                        cmd.CommandText = @"update ProductQuantity set " + mode + "=" + mode + "+" + opda_docDetail.ProductQuantity + " where ProductID=" + opda_docDetail.ProductID + " and DepotSeatID=" + DepotSeatID + " and WareHouseID=" + warehouseid;

                        int hs = cmd.ExecuteNonQuery();

                        if (hs == 1)
                        {
                            cmd.CommandText = @"update LogicProductInventory set " + mode + "=" + mode + "+" + opda_docDetail.ProductQuantity + " where ProductID=" + opda_docDetail.ProductID;

                            if (cmd.ExecuteNonQuery() == 1)
                            {
                                cmd.CommandText = @"insert into InventoryDocDetails( DocID, ProductID,ProductQuantity,UnitPrice,MeasureUnit,PV, ExpectNum, ProductTotal) values
                                        ( @DocID, @ProductID,@ProductQuantity, @UnitPrice,@MeasureUnit, @PV, @ExpectNum, @ProductTotal)";

                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@DocID", inventoryDocModel.DocID);
                                cmd.Parameters.AddWithValue("@ProductID", opda_docDetail.ProductID);
                                cmd.Parameters.AddWithValue("@ProductQuantity", opda_docDetail.ProductQuantity);
                                cmd.Parameters.AddWithValue("@UnitPrice", opda_docDetail.UnitPrice);
                                cmd.Parameters.AddWithValue("@MeasureUnit", opda_docDetail.MeasureUnit);
                                cmd.Parameters.AddWithValue("@PV", opda_docDetail.PV);
                                cmd.Parameters.AddWithValue("@ExpectNum", opda_docDetail.ExpectNum);
                                //cmd.Parameters.AddWithValue("@SelectedIndex", opda_docDetail.SelectedIndex);
                                cmd.Parameters.AddWithValue("@ProductTotal", opda_docDetail.ProductTotal);
                                //cmd.Parameters.AddWithValue("@DepotSeatID", opda_docDetail.DepotSeatID);
                                if (cmd.ExecuteNonQuery() == 1)
                                {
                                    rt = "1";//成功
                                }
                                else
                                {
                                    rt = "-2";//插入失败
                                    break;
                                }

                            }
                            else
                            {
                                rt = "-1";//更新失败
                                break;
                            }
                        }
                        else
                        {
                            rt = "-3";//更新失败，在此仓库上不存在此条记录
                            break;
                        }
                    }

                }
                else
                {
                    rt = "0"; //
                }


                if (rt != "1")
                    tran.Rollback();
                else if (rt == "1")  //成功
                    tran.Commit();

                cmd.Dispose();
                con.Close();

                return rt;
            }
            catch (Exception)
            {
                if (tran != null)
                    tran.Rollback();
                if (con != null)
                    con.Close();

                return "-4";  // 异常
            }
        }

        //库存调拨   
        public static string SetDiaoBo(ArrayList inventoryDocDetailsModels, int outWareHouse, int outDepotSeatID, int inWareHouse, int inDepotSeatID, InventoryDocModel inventoryDocModel)
        {
            string rt = "";
            SqlTransaction tran = null;
            SqlConnection con = null;

            try
            {
                con = new SqlConnection(DBHelper.connString);

                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                tran = con.BeginTransaction();
                cmd.Transaction = tran;

                cmd.CommandText = @"INSERT INTO InventoryDoc(DocAuditer,OperateIP,DocAuditTime,OperateNum,
								DocTypeID,DocID, DocMakeTime, 
								 DocMaker,
								Provider, Client, WareHouseID,DepotSeatID, TotalMoney, TotalPV,
								 ExpectNum,
								 Note,StateFlag , OriginalDocID,inWareHouseID,indepotseatid
								)
						VALUES( @DocAuditer,@OperateIP,@DocAuditTime,@OperateNum,
                                @int_docTypeID, @cha_docID, @dat_docMakeTime, 
								@cha_docMaker, 
								@vch_provider, @cha_client, @warehouseID,@DepotSeatID, @num_totalMoney, @num_totalPV,
								@int_qishu,
								 @nvch_note ,@StateFlag ,@OriginalDocID,@inWareHouseID,@indepotseatid)";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@DocAuditer", inventoryDocModel.DocAuditer);
                cmd.Parameters.AddWithValue("@OperateIP", inventoryDocModel.OperateIP);
                cmd.Parameters.AddWithValue("@DocAuditTime", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@OperateNum", inventoryDocModel.OperateNum);
                cmd.Parameters.AddWithValue("@int_docTypeID", inventoryDocModel.DocTypeID);
                cmd.Parameters.AddWithValue("@cha_docID", inventoryDocModel.DocID);
                cmd.Parameters.AddWithValue("@dat_docMakeTime", inventoryDocModel.DocMakeTime);
                cmd.Parameters.AddWithValue("@cha_docMaker", inventoryDocModel.DocMaker);
                cmd.Parameters.AddWithValue("@vch_provider", inventoryDocModel.Provider);
                cmd.Parameters.AddWithValue("@cha_client", inventoryDocModel.Client);
                cmd.Parameters.AddWithValue("@warehouseID", inventoryDocModel.WareHouseID);
                cmd.Parameters.AddWithValue("@DepotSeatID", inventoryDocModel.DepotSeatID);
                cmd.Parameters.AddWithValue("@num_totalMoney", inventoryDocModel.TotalMoney);
                cmd.Parameters.AddWithValue("@num_totalPV", inventoryDocModel.TotalPV);
                //cmd.Parameters.AddWithValue("@cha_motherID", inventoryDocModel.MotherID);
                cmd.Parameters.AddWithValue("@int_qishu", inventoryDocModel.ExpectNum);
                //cmd.Parameters.AddWithValue("@vch_Cause", inventoryDocModel.Cause);
                cmd.Parameters.AddWithValue("@nvch_note", inventoryDocModel.Note);
                cmd.Parameters.AddWithValue("@StateFlag", inventoryDocModel.StateFlag);
                cmd.Parameters.AddWithValue("@OriginalDocID", inventoryDocModel.OriginalDocID);
                //cmd.Parameters.AddWithValue("@Currency", inventoryDocModel.Currency);
                cmd.Parameters.AddWithValue("@inWareHouseID", inWareHouse);
                cmd.Parameters.AddWithValue("@indepotseatid", inDepotSeatID);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    for (int i = 0; i < inventoryDocDetailsModels.Count; i++)
                    {
                        InventoryDocDetailsModel opda_docDetail = (InventoryDocDetailsModel)inventoryDocDetailsModels[i];

                        cmd.CommandText = @"update ProductQuantity set TotalOut=TotalOut+" + opda_docDetail.ProductQuantity + "where ProductID=" + opda_docDetail.ProductID + " and DepotSeatID=" + inDepotSeatID + " and WareHouseID=" + inWareHouse;

                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            cmd.CommandText = @"update ProductQuantity set TotalIn=TotalIn+" + opda_docDetail.ProductQuantity + " where ProductID=" + opda_docDetail.ProductID + " and DepotSeatID=" + outDepotSeatID + " and WareHouseID=" + outWareHouse;

                            if (cmd.ExecuteNonQuery() == 1)
                            {

                            }
                            else
                            {
                                cmd.CommandText = @"insert into ProductQuantity(ProductID,TotalIn,TotalOut,TotalLogicOut,DepotSeatID,WareHouseID) values (" + opda_docDetail.ProductID + "," + opda_docDetail.ProductQuantity + ",0,0," + outDepotSeatID + "," + outWareHouse + ")";

                                if (cmd.ExecuteNonQuery() == 1)
                                {

                                }
                                else
                                {
                                    rt = "-1";
                                    break;
                                }
                            }

                            cmd.CommandText = @"insert into InventoryDocDetails( DocID, ProductID,ProductQuantity,UnitPrice,MeasureUnit,PV, ExpectNum, ProductTotal) values
                                        ( @DocID, @ProductID,@ProductQuantity, @UnitPrice,@MeasureUnit, @PV, @ExpectNum, @ProductTotal)";

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@DocID", inventoryDocModel.DocID);
                            cmd.Parameters.AddWithValue("@ProductID", opda_docDetail.ProductID);
                            cmd.Parameters.AddWithValue("@ProductQuantity", opda_docDetail.ProductQuantity);
                            cmd.Parameters.AddWithValue("@UnitPrice", opda_docDetail.UnitPrice);
                            cmd.Parameters.AddWithValue("@MeasureUnit", opda_docDetail.MeasureUnit);
                            cmd.Parameters.AddWithValue("@PV", opda_docDetail.PV);
                            cmd.Parameters.AddWithValue("@ExpectNum", opda_docDetail.ExpectNum);
                            //cmd.Parameters.AddWithValue("@SelectedIndex", opda_docDetail.SelectedIndex);
                            cmd.Parameters.AddWithValue("@ProductTotal", opda_docDetail.ProductTotal);
                            //cmd.Parameters.AddWithValue("@DepotSeatID", opda_docDetail.DepotSeatID);
                            if (cmd.ExecuteNonQuery() == 1)
                            {
                                rt = "1";//成功
                            }
                            else
                            {
                                rt = "-2";//插入失败
                                break;
                            }
                        }
                        else
                        {
                            rt = "0"; //
                            break;
                        }
                    }
                }
                else
                {
                    rt = "-4";
                }
                if (rt != "1")
                    tran.Rollback();
                else if (rt == "1")  //成功
                    tran.Commit();

                cmd.Dispose();
                con.Close();

                return rt;
            }
            catch (Exception)
            {
                if (tran != null)
                    tran.Rollback();
                if (con != null)
                    con.Close();

                return "-3";  // 异常
            }
        }

        #endregion

        #region 库存调拨
        /// <summary>
        /// 库存调拨
        /// </summary>
        /// <param name="DepotSeatID"></param>
        /// <param name="sum"></param>
        /// <param name="productid"></param>
        /// <returns></returns>
        public static bool productReWareHOuse(int outWareHouse, int outDepotSeatID, int inWareHouse, int inDepotSeatID, double sum, int productid)
        {
            SqlParameter[] param = new SqlParameter[] { 
                new SqlParameter("@outWareHouse",outWareHouse),
                new SqlParameter("@outDepotSeatID",outDepotSeatID),
                new SqlParameter("@sum",sum),
                new SqlParameter("@productid",productid),
                new SqlParameter("@inWareHouse",inWareHouse),
                new SqlParameter("@inDepotSeatID",inDepotSeatID)
            };

            if (DBHelper.ExecuteNonQuery("productReWareHOuse", param, CommandType.StoredProcedure) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 入库单查询
        /// <summary>
        /// 入库单查询
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
        public DataTable InStoreOrder(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
            RecordCount = 0;
            PageCount = 0;
            return CommonDataDAL.GetDataTablePage_gr(PageIndex, PageSize, table, columns, condition, key, out RecordCount, out PageCount);
        }
        #endregion

        #region 出库查询
        /// <summary>
        /// 出库单
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <param name="moneyType"></param>
        /// <param name="wareHouseID"></param>
        /// <returns></returns>
        public static DataTable outStoreOrder(DateTime fromTime, DateTime toTime, int moneyType, int wareHouseID, int DepotSeatID)
        {
            DataTable table = new DataTable();
            SqlParameter[] par = new SqlParameter[]
            {
                new SqlParameter("@fromTime",SqlDbType.DateTime),
                new SqlParameter("@toTime",SqlDbType.DateTime),
                new SqlParameter("@wareHouseID",SqlDbType.Int),
                new SqlParameter("@DepotSeatID",SqlDbType.Int)
            };  //new SqlParameter("@moneyType",SqlDbType.Int),
            par[0].Value = fromTime;
            par[1].Value = toTime;
            par[2].Value = wareHouseID;
            par[3].Value = DepotSeatID;
            //par[4].Value = DepotSeatID;
            table = DBHelper.ExecuteDataTable("QueryOutStorageEOrders", par, CommandType.StoredProcedure);
            return table;

        }
        #endregion

        #region 入库审批(查询没有审核的单据)
        /// <summary>
        /// 入库审批（分页：未审核的单据）
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
        public DataTable UnCheckDocType(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
            return CommonDataDAL.GetDataTablePage_Sms(PageIndex, PageSize, table, columns, condition, key, out RecordCount, out PageCount);
        }
        #endregion

        #region 根据单据编号得到入库单的信息
        public static InventoryDocModel getOrderInfo(string billID)
        {
            InventoryDocModel idm = new InventoryDocModel();
            SqlParameter[] par = new SqlParameter[] { new SqlParameter("@billID", SqlDbType.VarChar, 20) };
            par[0].Value = billID;
            SqlDataReader dr = DBHelper.ExecuteReader("GetOrderByBillID", par, CommandType.StoredProcedure);
            if (dr.Read())
            {
                //idm.DocID = Convert.ToInt32(dr["DocID"].ToString());
                idm.Provider = Convert.ToInt32(dr["Provider"]);
                idm.Client = dr["Client"].ToString();
                idm.WareHouseID = Convert.ToInt32(dr["WareHouseID"].ToString());
                idm.TotalMoney = double.Parse(dr["TotalMoney"].ToString());
                idm.TotalPV = double.Parse(dr["TotalPV"].ToString());
                idm.ExpectNum = Convert.ToInt32(dr["ExpectNum"].ToString());
                //idm.Cause = dr["Cause"].ToString();
                idm.Note = dr["Note"].ToString();
                idm.StateFlag = Convert.ToInt32(dr["StateFlag"].ToString());
                idm.BatchCode = dr["BatchCode"].ToString();
                idm.OperationPerson = dr["OperationPerson"].ToString();
                idm.Address = dr["Address"].ToString();
                idm.OriginalDocID = dr["OriginalDocID"].ToString();
                //idm.Currency = Convert.ToInt32(dr["Currency"].ToString());
                idm.DepotSeatID = Convert.ToInt32(dr["DepotSeatID"].ToString());
            }
            dr.Close();
            return idm;
        }
        #endregion

        #region 获得产品列表
        /// <summary>
        ///  获取产品列表和入库单信息  ---DS2012
        /// </summary>
        /// <param name="billID"></param>
        /// <returns></returns>
        public static DataTable getProduct(string billID)
        {
            string sqlstr = " select a.countryCode from WareHouse as a,InventoryDoc as c where  a.WareHouseid=c.WareHouseid and  c.DocID=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@num",SqlDbType.VarChar,20)
                
            };
            sparams[0].Value = billID;
            string countryCode = DBHelper.ExecuteScalar(sqlstr, sparams, CommandType.Text).ToString();


            SqlParameter[] para ={
                                      new SqlParameter ("@billID" ,SqlDbType .VarChar ,20),
                                      new SqlParameter ("@code" ,SqlDbType .NVarChar ,10)
                                  };
            para[0].Value = billID;
            para[1].Value = countryCode;

            DataTable dtTemp = DBHelper.ExecuteDataTable("GetProductByInventoryID", para, CommandType.StoredProcedure);
            return dtTemp;
        }
        #endregion

        #region 检查批次是否已存在
        public static int CheckBatch(string availableOrderID, string BatchCode)
        {
            string checkBatchCodeSQL = "Select Count(BatchCode) From InventoryDoc Where BatchCode=@num and DocID!=@num1";
            SqlParameter[] para ={
                                      new SqlParameter ("@num" ,SqlDbType .VarChar ,50),
                                      new SqlParameter ("@num1" ,SqlDbType .VarChar ,20)
                                  };
            para[0].Value = BatchCode;
            para[1].Value = availableOrderID;
            int i = 0;
            i = (int)DBHelper.ExecuteScalar(checkBatchCodeSQL, para, CommandType.Text);
            return i;
        }
        #endregion

        #region 更新产品入库的信息
        public static int updAndSaveOrder(InventoryDocModel idm, ArrayList list)
        {
            int i = 0;
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    string SQL_UPDATE_opda_depotManageDoc = "UPDATE InventoryDoc SET Provider = @Provider,  WareHouseID = @WareHouseID,DepotSeatID=@DepotSeatID, TotalMoney = @TotalMoney, TotalPV = @TotalPV, ExpectNum = @ExpectNum, Note = @Note,  BatchCode = @BatchCode, OperationPerson = @OperationPerson,Address=@Address,OriginalDocID=@OriginalDocID,OperateIP=@OperateIP,OperateNum=@OperateBh WHERE DocID = @DocID";
                    SqlParameter[] para ={
											new SqlParameter ("@Provider" ,SqlDbType .VarChar ,50),
                                            new SqlParameter ("@WareHouseID" ,SqlDbType.Int ),
											
											new SqlParameter ("@TotalMoney" ,SqlDbType.Decimal ),
											new SqlParameter ("@TotalPV" ,SqlDbType.Decimal ),
											new SqlParameter ("@ExpectNum" ,SqlDbType.Int  ,4),
											new SqlParameter ("@Note" ,SqlDbType .VarChar ,500),
											new SqlParameter ("@BatchCode" ,SqlDbType .VarChar ,50),
											new SqlParameter ("@OperationPerson" ,SqlDbType .VarChar ,20),
											new SqlParameter ("@DocID" ,SqlDbType .VarChar ,50),
										    new SqlParameter ("@Address" ,SqlDbType .VarChar ,50),
										    new SqlParameter ("@OriginalDocID" ,SqlDbType .VarChar ,20),
						                    new SqlParameter("@OperateIP",SqlDbType.VarChar,30),
						                    new SqlParameter("@OperateBh",SqlDbType.VarChar,30),
                                            new SqlParameter ("@DepotSeatID" ,SqlDbType.Int )
										  };
                    para[0].Value = idm.Provider.ToString();
                    para[1].Value = Convert.ToInt32(idm.WareHouseID.ToString());
                    para[2].Value = Convert.ToDecimal(idm.TotalMoney.ToString());
                    para[3].Value = Convert.ToDecimal(idm.TotalPV.ToString());
                    para[4].Value = Convert.ToInt32(idm.ExpectNum.ToString());
                    para[5].Value = idm.Note.ToString();
                    para[6].Value = idm.BatchCode.ToString();
                    para[7].Value = idm.OperationPerson.ToString();
                    para[8].Value = idm.DocID.ToString();
                    para[9].Value = idm.Address.ToString();
                    para[10].Value = idm.OriginalDocID.ToString();
                    para[11].Value = idm.OperateIP.ToString();
                    para[12].Value = idm.OperateNum.ToString();
                    para[13].Value = idm.DepotSeatID.ToString();
                    DBHelper.ExecuteNonQuery(tran, SQL_UPDATE_opda_depotManageDoc, para, CommandType.Text);
                    string sql11 = "Delete From InventoryDocDetails Where DocID=@num1";
                    SqlParameter[] para5 ={
                                      
                                      new SqlParameter ("@num1" ,SqlDbType .VarChar ,20)
                                  };
                    para5[0].Value = idm.DocID;

                    DBHelper.ExecuteNonQuery(tran, sql11, para5, CommandType.Text);
                    i = InventoryDocDetailsDAL.CreateBillofDocumentDetails(tran, list);
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
            return i;
        }
        #endregion

        #region 获取出库产品(Outstock)
        /// <summary>
        /// 获取出库产品(Outstock)
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public static DataTable GetOutProduct(string storeOrderID)
        {
            string cmd = @"select pd.ProductName,id.Batch,ds.SeatName,pn.Count from OrderDetail od inner join Product pd on 
                        od.ProductID=pd.ProductID inner join InventoryDocDetails id on pd.ProductID=id.ProductID 
                        inner join ProductNavigation pn on id.ProductID=pn.ProductID inner join DepotSeat ds on 
                        pn.DepotSeatID=ds.ID where od.StoreOrderID=@storeOrderID";

            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@storeOrderID", storeOrderID) };

            return DBHelper.ExecuteDataTable(cmd, param, CommandType.Text);
        }
        #endregion

        #region 获取产品原来订的数量(Outstock)
        /// <summary>
        /// 获取产品原来订的数量(Outstock)
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public static DataTable GetProductQuantity(string storeOrderID)
        {
            string cmd = @"select pd.ProductName,od.Quantity from OrderDetail od inner join Product pd on 
                            od.ProductID=pd.ProductID where od.StoreOrderID=@storeOrderID";

            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@storeOrderID", storeOrderID) };

            return DBHelper.ExecuteDataTable(cmd, param, CommandType.Text);
        }
        #endregion

        #region 获取店铺库存信息用于换货
        /// <summary>
        /// 获取店铺库存信息用于换货
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public DataTable GetStoreInfo(string storeID)
        {
            string cmd = @"SELECT ID , PostalCode,StoreName ,Name , StoreAddress, HomeTele,OfficeTele,MobileTele,FaxTele,Country,Province,City  FROM StoreInfo WHERE StoreID = @StoreID";

            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@StoreID", storeID) };

            return DBHelper.ExecuteDataTable(cmd, param, CommandType.Text);
        }
        #endregion

        #region 生成一个单据，包括各种单据［出库，入库，红单，退货等］
        /// <summary>
        /// 生成一个单据，包括各种单据［出库，入库，红单，退货等］，返回受影响的行数
        /// </summary>
        /// <param name="tran">处理事务</param>
        /// <param name="inventoryDocModel">某种单据类对象</param>
        /// <returns></returns>
        public static int CreateInventoryDoc(SqlTransaction tran, InventoryDocModel inventoryDocModel)
        {
            string createSql = @"INSERT INTO InventoryDoc
                                (
								docTypeID, docID, docMakeTime, 
								 docMaker,
								provider, client, warehouseID,DepotSeatID,totalMoney, totalPV,
								  ExpectNum,
								note,StateFlag ,BatchCode,OperationPerson,Address,OriginalDocID,OperateIP,OperateNum,InDepotSeatID
								)
						VALUES( @docTypeID, @docID, @docMakeTime, 
								@docMaker, 
								@provider, @client, @warehouseID,@depotSeatID, @totalMoney, @totalPV,
								 @ExpectNum,
								 @note ,@StateFlag ,@BatchCode,@OperationPerson,@Address,@OriginalDocID,@OperateIP,@OperateNum,@InDepotSeatID)";
            SqlParameter[] objParamArray;

            objParamArray = new SqlParameter[]
                {				
				  new SqlParameter("@docTypeID", inventoryDocModel.DocTypeID),
				  new SqlParameter("@docID", inventoryDocModel.DocID),
				  new SqlParameter("@docMakeTime", inventoryDocModel.DocMakeTime),
				  new SqlParameter("@docMaker", inventoryDocModel.DocMaker),
				  new SqlParameter("@provider", inventoryDocModel.Provider),
				  new SqlParameter("@client", inventoryDocModel.Client),
				  new SqlParameter("@warehouseID", inventoryDocModel.WareHouseID),
                  new SqlParameter("@depotSeatID",inventoryDocModel.DepotSeatID),
				  new SqlParameter("@totalMoney", inventoryDocModel.TotalMoney),
				  new SqlParameter("@totalPV", inventoryDocModel.TotalPV),
				  //new SqlParameter("@motherID", inventoryDocModel.MotherID),
				  new SqlParameter("@ExpectNum", inventoryDocModel.ExpectNum),
				  //new SqlParameter("@Cause", inventoryDocModel.Cause),
				  new SqlParameter("@note", inventoryDocModel.Note),
				  new SqlParameter("@StateFlag", inventoryDocModel.StateFlag),
                  new SqlParameter("@BatchCode",inventoryDocModel.BatchCode),
                  new SqlParameter("@OperationPerson",inventoryDocModel.OperationPerson),
                  new SqlParameter("@Address",inventoryDocModel.Address),
				  new SqlParameter("@OriginalDocID" , inventoryDocModel.OriginalDocID),
                  //new SqlParameter("@Currency" , inventoryDocModel.Currency),
                  new SqlParameter("@OperateIP" , inventoryDocModel.OperateIP),
                  new SqlParameter("@OperateNum" , inventoryDocModel.OperateNum),
                  new SqlParameter("@InDepotSeatID",inventoryDocModel.InDepotSeatID)
				};

            return DBHelper.ExecuteNonQuery(tran, createSql, objParamArray, CommandType.Text);
        }
        #endregion

        #region 生成一个单据，包括各种单据［出库，入库，红单，退货等］
        /// <summary>
        /// 生成一个单据，包括各种单据［出库，入库，红单，退货等］，返回受影响的行数    ---DS2012
        /// </summary>
        /// <param name="tran">处理事务</param>
        /// <param name="inventoryDocModel">某种单据类对象</param>
        /// <returns></returns>
        public static int CreateInventoryDoc_WH(SqlTransaction tran, InventoryDocModel inventoryDocModel)
        {
            string createSql = @"INSERT INTO InventoryDoc
                                (
								docTypeID, docID, docMakeTime,docMaker,provider, client, warehouseID,DepotSeatID,totalMoney, totalPV,
								  ExpectNum,
								 note,StateFlag ,BatchCode,OperationPerson,Address,OriginalDocID,OperateIP,OperateNum
								)
						VALUES( @docTypeID, @docID, @docMakeTime,@docMaker,@provider, @client, @warehouseID,@depotSeatID, @totalMoney, @totalPV,
                                 @ExpectNum,
								 @note ,@StateFlag ,@BatchCode,@OperationPerson,@Address,@OriginalDocID,@OperateIP,@OperateNum)";
            SqlParameter[] objParamArray;

            objParamArray = new SqlParameter[]
                {				
				  new SqlParameter("@docTypeID", inventoryDocModel.DocTypeID),
				  new SqlParameter("@docID", inventoryDocModel.DocID),
				  new SqlParameter("@docMakeTime", inventoryDocModel.DocMakeTime),
				  new SqlParameter("@docMaker", inventoryDocModel.DocMaker),
				  new SqlParameter("@provider", inventoryDocModel.Provider),
				  new SqlParameter("@client", inventoryDocModel.Client),
				  new SqlParameter("@warehouseID", inventoryDocModel.WareHouseID),
                  new SqlParameter("@depotSeatID",inventoryDocModel.DepotSeatID),
				  new SqlParameter("@totalMoney", inventoryDocModel.TotalMoney),
				  new SqlParameter("@totalPV", inventoryDocModel.TotalPV),
                  //new SqlParameter("@PayCurrency", inventoryDocModel.PayCurrency),
                  //new SqlParameter("@PayMoney", inventoryDocModel.PayMoney),
                  //new SqlParameter("@motherID", inventoryDocModel.MotherID),
				  new SqlParameter("@ExpectNum", inventoryDocModel.ExpectNum),
                  //new SqlParameter("@Cause", inventoryDocModel.Cause),
				  new SqlParameter("@note", inventoryDocModel.Note),
				  new SqlParameter("@StateFlag", inventoryDocModel.StateFlag),
                  new SqlParameter("@BatchCode",inventoryDocModel.BatchCode),
                  new SqlParameter("@OperationPerson",inventoryDocModel.OperationPerson),
                  new SqlParameter("@Address",inventoryDocModel.Address),
				  new SqlParameter("@OriginalDocID" , inventoryDocModel.OriginalDocID),
                  //new SqlParameter("@Currency" , inventoryDocModel.Currency),
                  new SqlParameter("@OperateIP" , inventoryDocModel.OperateIP),
                  new SqlParameter("@OperateNum" , inventoryDocModel.OperateNum)
				};

            return DBHelper.ExecuteNonQuery(tran, createSql, objParamArray, CommandType.Text);
        }
        #endregion

        #region 通过入库批次获取入库批次行数
        /// <summary>
        /// 通过入库批次获取入库批次行数
        /// </summary>
        /// <param name="batchCode">入库批次</param>
        /// <returns>返回入库批次行数</returns>
        public static int GetCountByBatchCode(string batchCode)
        {
            SqlParameter[] sparams = new SqlParameter[] 
            {
                new SqlParameter("@batchCode",SqlDbType.VarChar,50)
            };
            sparams[0].Value = batchCode;
            int getCount = 0;
            getCount = (int)DBHelper.ExecuteScalar("GetCountByBatchCode", sparams, CommandType.StoredProcedure);
            return getCount;
        }
        #endregion

        #region 从库存单据表中获取最大的ID
        /// <summary>
        /// 从库存单据表中获取最大的ID
        /// </summary>
        /// <returns>返回SqlDataReader对象</returns>
        public static SqlDataReader GetMaxIDFromInventoryDoc()
        {
            SqlDataReader dr;
            dr = DBHelper.ExecuteReader("GetMaxIDFromInventoryDoc", CommandType.StoredProcedure);
            return dr;
        }
        #endregion

        #region 获取新的订单号
        /// <summary>
        /// 获取新的订单号
        /// </summary>
        /// <param name="enumOrderType">单据类型</param>
        /// <returns>返回新订单号</returns>
        public static string GetNewOrderID(EnumOrderFormType enumOrderType)
        {
            ///实例化编码规则
            CodingRule codingRule = new CodingRule();
            string prefix = codingRule.GetOrderFormPrefix(enumOrderType);

            string date = MYDateTime.ToYYMMDDHHmmssString();
            string orderId = string.Empty;
            SqlDataReader reader = GetMaxIDFromInventoryDoc();
            if (reader.Read())
                orderId += (reader.GetInt32(0) + 1).ToString();
            else
                orderId += "1";

            reader.Close();

            if (orderId.Length < 5)
            {
                orderId = geneSomeCharZero(5 - orderId.Length) + orderId;
            }
            return prefix + date + orderId;
        }
        #endregion

        #region  得到可用的换货单编号，前缀为"HH"
        /// <summary>
        /// 得到可用的换货单编号，返回换货单编号
        /// </summary>	
        public static string GetReplacementID()
        {
            string _prefix = "HT";
            string _date = MYDateTime.ToYYMMDDHHmmssString();
            string _orderId = string.Empty;

            string SQL_SELECT_Max_ID = "SELECT Top 1 ID FROM Replacement  Order By ID  Desc ";
            SqlDataReader reader = DBHelper.ExecuteReader(SQL_SELECT_Max_ID);
            if (reader.Read())
                _orderId += (reader.GetInt32(0) + 1).ToString();
            else
                _orderId += "1";

            reader.Close();

            if (_orderId.Length < 5)
            {
                _orderId = geneSomeCharZero(5 - _orderId.Length) + _orderId;
            }

            return _prefix + _date + _orderId;
        }
        #endregion
        #region  产生若干0字符的串
        /// <summary>
        /// 产生若干0字符的串
        /// </summary>
        /// <param name="num">数目</param>
        /// <returns>返回若干0字符串</returns>
        private static string geneSomeCharZero(int num)
        {
            string zero = string.Empty;
            for (int i = 0; i < num; i++)
                zero += "0";
            return zero;
        }
        #endregion

        #region 确认出库 按钮。并生成出库单
        /// <summary>
        /// 确认出库 按钮。并生成出库单
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public static string OutOrder(string storeOrderID, string outStorageOrderID, InventoryDocModel idm, InventoryDocDetailsModel ddm)
        {
            string cmd = "procBillOutOrder";

            SqlParameter[] inparam = new SqlParameter[]
            { 
                new SqlParameter("@storeOrderID", storeOrderID),
                new SqlParameter("@outStorageOrderID", @outStorageOrderID),
                new SqlParameter("@DocTypeID", idm.DocTypeID),
                new SqlParameter("@DocMaker", idm.DocMaker),
                new SqlParameter("@Client", idm.Client),
                new SqlParameter("@DepotSeatID", idm.DepotSeatID),
                new SqlParameter("@WareHouseID", idm.WareHouseID),
                new SqlParameter("@TotalMoney", idm.TotalMoney),
                new SqlParameter("@TotalPV", idm.TotalPV),
                new SqlParameter("@ExpectNum", idm.ExpectNum),
                //new SqlParameter("@Cause", idm.Cause),
                new SqlParameter("@Note", idm.Note),
                new SqlParameter("@StateFlag", idm.StateFlag),
                new SqlParameter("@CloseFlag", idm.CloseFlag),
                new SqlParameter("@OperationPerson", idm.OperationPerson),
                new SqlParameter("@OriginalDocID", idm.OriginalDocID),
                new SqlParameter("@Address", idm.Address),
                new SqlParameter("@ProductID", ddm.ProductID),
                new SqlParameter("@ProductQuantity", ddm.ProductQuantity),
                new SqlParameter("@UnitPrice", ddm.UnitPrice),
                new SqlParameter("@MeasureUnit", ddm.MeasureUnit),
                new SqlParameter("@PV", ddm.PV),
                new SqlParameter("@ProductTotal", ddm.ProductTotal),
            };

            SqlParameter[] outparam = new SqlParameter[] 
            { 
                new SqlParameter("@rt",SqlDbType.NVarChar,50)
            };


            object[] obj = DBHelper.ExecuteNonQuery(cmd, CommandType.StoredProcedure, inparam, outparam);

            return obj[0].ToString();
        }

        /*  */
        /// <summary>
        /// 拆分产品信息--订单信息
        /// 注：把组合产品拆分成单品
        /// </summary>
        /// <param name="ods">原产品信息</param>
        /// <returns>拆分后产品信息</returns>
        public static IList<InventoryDocDetailsModel> GetNewInventoryDocDetails(IList<InventoryDocDetailsModel> ods)
        {
            IList<InventoryDocDetailsModel> orderdetails = new List<InventoryDocDetailsModel>();
            foreach (InventoryDocDetailsModel od in ods)
            {
                if (ProductDAL.GetIsCombine(od.ProductID))
                {
                    IList<ProductCombineDetailModel> comDetails = ProductCombineDetailDAL.GetCombineDetil(od.ProductID);
                    foreach (ProductCombineDetailModel comDetail in comDetails)
                    {
                        int count = 0;
                        foreach (InventoryDocDetailsModel detail in orderdetails)
                        {
                            if (detail.ProductID == comDetail.SubProductID)
                            {
                                detail.ProductQuantity = (comDetail.Quantity * od.ProductQuantity) + detail.ProductQuantity;
                                count++;
                            }
                        }
                        if (count == 0)
                        {
                            InventoryDocDetailsModel orderdetail = new InventoryDocDetailsModel();
                            orderdetail.ProductQuantity = comDetail.Quantity * od.ProductQuantity;
                            orderdetail.ProductID = comDetail.SubProductID;
                            orderdetail.UnitPrice = comDetail.UnitPrice;
                            orderdetail.PV = comDetail.PV;
                            orderdetail.ProductTotal = comDetail.Quantity * orderdetail.UnitPrice;
                            orderdetail.MeasureUnit = "";
                            orderdetails.Add(orderdetail);
                        }
                    }
                }
                else
                {
                    int count = 0;
                    foreach (InventoryDocDetailsModel detail in orderdetails)
                    {
                        if (detail.ProductID == od.ProductID)
                        {
                            detail.ProductQuantity = od.ProductQuantity + detail.ProductQuantity;
                            count++;
                        }
                    }
                    if (count == 0)
                    {
                        InventoryDocDetailsModel orderdetail = new InventoryDocDetailsModel();
                        orderdetail.ProductQuantity = od.ProductQuantity;
                        orderdetail.ProductID = od.ProductID;
                        orderdetail.MeasureUnit = "";
                        orderdetail.PV = od.PV;
                        orderdetail.UnitPrice = od.UnitPrice;
                        orderdetail.ProductTotal = od.ProductTotal;
                        orderdetails.Add(orderdetail);
                    }
                }
            }
            return orderdetails;
        }

        /// <summary>
        /// 订单出库
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <param name="outStorageOrderID"></param>
        /// <param name="idm"></param>
        /// <param name="l_ddm"></param>
        /// <returns></returns>
        public static string OutOrder(string storeOrderID, string outStorageOrderID, InventoryDocModel idm, List<InventoryDocDetailsModel> l_ddm)
        {
            List<InventoryDocDetailsModel> l_ddmZH = (List<InventoryDocDetailsModel>)GetNewInventoryDocDetails(l_ddm);//将出库的组合产品拆分成单品

            string rt = "a";

            SqlTransaction tran = null;
            SqlConnection con = null;
            SqlCommand cmd = null;

            try
            {
                con = new SqlConnection(DBHelper.connString);

                con.Open();

                cmd = new SqlCommand();
                cmd.Connection = con;
                tran = con.BeginTransaction();
                cmd.Transaction = tran;

                //判断是否出库
                cmd.CommandText = "select IsGeneOutBill from Storeorder where StoreOrderID='" + storeOrderID + "'";
                if (cmd.ExecuteScalar().ToString().ToLower() == "a")
                {
                    //已经出库
                    rt = "-88";
                    throw new Exception("已经出库");
                }

                //扣除公司实际库存
                for (int i = 0; i < l_ddmZH.Count; i++)
                {
                    cmd.CommandText = "select TotalIn,TotalOut from ProductQuantity where ProductID='" + l_ddmZH[i].ProductID + "' and DepotSeatID='" + idm.DepotSeatID + "' and WareHouseID='" + idm.WareHouseID + "'";

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        double totalIn = Convert.ToDouble(dr["TotalIn"]);
                        double totalOut = Convert.ToDouble(dr["TotalOut"]);

                        dr.Close();

                        if (l_ddmZH[i].ProductQuantity + totalOut > totalIn)
                        {
                            rt = "0";// 库存不够
                            throw new Exception("库存不够");
                        }
                        else
                        {
                            cmd.CommandText = @"update dbo.ProductQuantity set TotalOut = '" + (l_ddmZH[i].ProductQuantity + totalOut) + "'  where ProductID='" + l_ddmZH[i].ProductID + "' and DepotSeatID='" + idm.DepotSeatID + "' and WareHouseID='" + idm.WareHouseID + "'";
                            if (cmd.ExecuteNonQuery() != 1)
                            {
                                rt = "-1"; //更新库存失败
                                throw new Exception("更新库存失败");
                            }
                        }
                    }
                    else
                    {
                        rt = "-6";  //此库位上没有此产品
                        dr.Close();
                        throw new Exception("此库位上没有此产品");
                    }
                }

                //插入出库的明细
                for (int i = 0; i < l_ddmZH.Count; i++)
                {
                    cmd.Parameters.Clear();

                    cmd.CommandText = @"insert into dbo.InventoryDocDetails(DocID,ProductID,ProductQuantity,UnitPrice,MeasureUnit,PV,
			                                         ExpectNum,ProductTotal)
	                                        values(@outStorageOrderID,@ProductID,@ProductQuantity,@UnitPrice,@MeasureUnit,@PV,@ExpectNum,
			                                        @ProductTotal)";

                    cmd.Parameters.AddWithValue("@outStorageOrderID", outStorageOrderID);
                    cmd.Parameters.AddWithValue("@ProductID", l_ddmZH[i].ProductID);
                    cmd.Parameters.AddWithValue("@ProductQuantity", l_ddmZH[i].ProductQuantity);
                    cmd.Parameters.AddWithValue("@UnitPrice", l_ddmZH[i].UnitPrice);
                    cmd.Parameters.AddWithValue("@MeasureUnit", l_ddmZH[i].MeasureUnit);
                    cmd.Parameters.AddWithValue("@PV", l_ddmZH[i].PV);
                    cmd.Parameters.AddWithValue("@ExpectNum", idm.ExpectNum);
                    cmd.Parameters.AddWithValue("@ProductTotal", l_ddmZH[i].ProductTotal);
                    //cmd.Parameters.AddWithValue("@DepotSeatID", idm.DepotSeatID);

                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        rt = "-2";  //插入 库存单据明细表 失败
                        throw new Exception("插入 库存单据明细表 失败");
                    }
                }

                //修改原要货单明细的出库数量
                for (int i = 0; i < l_ddm.Count; i++)
                {
                    cmd.Parameters.Clear();

                    cmd.CommandText = @"update orderdetail set outbillquantity=outbillquantity+@number where storeorderid=@storeorderid and productid=@productid";

                    cmd.Parameters.AddWithValue("@number", l_ddm[i].ProductQuantity);
                    cmd.Parameters.AddWithValue("@storeorderid", storeOrderID);
                    cmd.Parameters.AddWithValue("@productid", l_ddm[i].ProductID);

                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        rt = "-5";  //插入 库存单据明细表 失败
                        throw new Exception("更新出库数量失败");
                    }
                }

                //更新要货单出库时间
                cmd.CommandText = "update dbo.StoreOrder set AuditingDate='" + DateTime.Now.ToUniversalTime() + "' where StoreOrderID='" + storeOrderID + "'";
                if (cmd.ExecuteNonQuery() != 1)
                {
                    rt = "-3";   //更新 StoreOrder 的出库单号失败
                    throw new Exception("更新 StoreOrder 的出库单号失败");
                }
                else
                {
                    //插入出库订单
                    cmd.CommandText = @"insert into InventoryDoc(DocTypeID, DocID, DocMaker,  
							                                         Client, inDepotSeatID, inWareHouseID, TotalMoney, TotalPV, 
							                                         ExpectNum, Note, StateFlag, 
							                                        CloseFlag, BatchCode, OperationPerson, OriginalDocID, 
							                                        Address,DocMakeTime,DocAuditTime,storeorderid)
	                                         values(@DocTypeID,@outStorageOrderID,@DocMaker,@Client, 
			                                        @DepotSeatID,@WareHouseID,@TotalMoney,@TotalPV,@ExpectNum, 
			                                        @Note,@StateFlag,@CloseFlag,@BatchCode,@OperationPerson,@OriginalDocID, 
			                                        @Address,@DocMakeTime,@DocAuditTime,@storeorderid)";

                    cmd.Parameters.Clear();

                    cmd.Parameters.AddWithValue("@DocTypeID", idm.DocTypeID);
                    cmd.Parameters.AddWithValue("@outStorageOrderID", outStorageOrderID);
                    cmd.Parameters.AddWithValue("@DocMaker", idm.DocMaker);
                    cmd.Parameters.AddWithValue("@Client", idm.Client);
                    cmd.Parameters.AddWithValue("@DepotSeatID", idm.DepotSeatID);
                    cmd.Parameters.AddWithValue("@WareHouseID", idm.WareHouseID);
                    cmd.Parameters.AddWithValue("@TotalMoney", idm.TotalMoney);
                    cmd.Parameters.AddWithValue("@TotalPV", idm.TotalPV);
                    cmd.Parameters.AddWithValue("@ExpectNum", idm.ExpectNum);
                    //cmd.Parameters.AddWithValue("@Cause", idm.Cause);
                    cmd.Parameters.AddWithValue("@Note", idm.Note);
                    cmd.Parameters.AddWithValue("@StateFlag", idm.StateFlag);
                    cmd.Parameters.AddWithValue("@CloseFlag", idm.CloseFlag);
                    cmd.Parameters.AddWithValue("@BatchCode", outStorageOrderID.Replace("CK", "PC"));
                    cmd.Parameters.AddWithValue("@OperationPerson", idm.OperationPerson);
                    cmd.Parameters.AddWithValue("@OriginalDocID", idm.OriginalDocID);
                    cmd.Parameters.AddWithValue("@Address", idm.Address);
                    cmd.Parameters.AddWithValue("@DocMakeTime", DateTime.Now.ToUniversalTime());
                    cmd.Parameters.AddWithValue("@DocAuditTime", DateTime.Now.ToUniversalTime());
                    cmd.Parameters.AddWithValue("@storeorderid", idm.Storeorderid);

                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        rt = "-4";  //插入 库存单据表 失败
                        throw new Exception("插入 库存单据表 失败");
                    }
                }

                tran.Commit();

                return "1";
            }
            catch
            {
                if (tran != null)
                    tran.Rollback();

                return rt;
            }
            finally
            {
                cmd.Dispose();
                if (tran != null)
                {
                    tran.Dispose();
                }
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }



        #endregion

        #region 获取Country
        /// <summary>
        /// 获取Country
        /// 
        public int getCountry(string storeId)
        {
            string cmd = "select a.id from country a,StoreInfo b where a.name=b.StoreCountry and b.storeid=" + storeId + "'";
            int id = int.Parse(DBHelper.ExecuteScalar(cmd).ToString());
            return id;
        }
        #endregion

        #region 取得产品列表
        ///<summary>
        /// 取得产品列表
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public DataTable GetStoreStorage(string storeID)
        {
            string cmd = @"Select P.productID,  
					IsNull((Select (totalin-totalout) From Stock Where ProductID=P.ProductID And StoreID=@StoreID ),0) as Quantity,
p.productcode,
					P.productName ,
					P.bigProductUnitID ,
					(Select ProductUnitName From ProductUnit Where ProductUnitID = P.bigProductUnitID) as BigUnitName ,
					P.smallProductUnitID,
					(Select ProductUnitName From ProductUnit Where ProductUnitID = P.smallProductUnitID) as SmallUnitName ,
					P.BigSmallMultiPle ,
					PreferentialPrice ,PreferentialPV   from product as P where isFold=0 ";

            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@StoreID", storeID) };
            return DBHelper.ExecuteDataTable(cmd, param, CommandType.Text);
        }
        #endregion

        #region 获得单据编号
        /// <summary>
        /// 根据单据类型的不同来获取不同的单据ID
        /// </summary>
        /// <param name="orderType">单据类型</param>
        /// <returns></returns>
        public static string GetDocId(EnumOrderFormType orderType)
        {
            string prefix = "TH";
            string date = MYDateTime.ToYYMMDDHHmmssString();
            string orderId = string.Empty;

            string sql = "SELECT Top 1 id FROM InventoryDoc Order By docID  Desc ";
            SqlDataReader reader = DBHelper.ExecuteReader(sql);
            if (reader.Read())
                orderId += (reader.GetInt32(0) + 1).ToString();
            else
                orderId += "1";

            reader.Close();

            if (orderId.Length < 5)
            {
                orderId = geneSomeCharZero(5 - orderId.Length) + orderId;
            }

            return prefix + date + orderId;
        }
        #endregion

        #region 获得CH单据编号
        /// <summary>
        /// 根据单据类型的不同来获取不同的单据ID
        /// </summary>
        /// <param name="orderType">单据类型</param>
        /// <returns></returns>
        public static string GetPoc(EnumOrderFormType orderType)
        {
            string prefix = "CH";
            string date = MYDateTime.ToYYMMDDString();
            string orderId = string.Empty;

            string sql = "SELECT Top 1 id FROM InventoryDoc Order By docID  Desc ";
            SqlDataReader reader = DBHelper.ExecuteReader(sql);
            if (reader.Read())
                orderId += (reader.GetInt32(0) + 1).ToString();
            else
                orderId += "1";

            reader.Close();

            if (orderId.Length < 5)
            {
                orderId = geneSomeCharZero(5 - orderId.Length) + orderId;
            }

            return prefix + date + orderId;
        }
        #endregion


        #region 获得单据编号
        /// <summary>
        /// 根据单据类型的不同来获取不同的单据ID
        /// </summary>
        /// <param name="orderType">单据类型</param>
        /// <returns></returns>
        public string GetDocIdByTypeCode(string DocTypeCode)
        {
            string prefix = DocTypeCode;
            string date = MYDateTime.ToYYMMDDHHmmssString();
            string orderId = string.Empty;

            string sql = "SELECT Top 1 id FROM InventoryDoc Order By docID  Desc ";
            SqlDataReader reader = DBHelper.ExecuteReader(sql);
            if (reader.Read())
                orderId += (reader.GetInt32(0) + 1).ToString();
            else
                orderId += "1";

            reader.Close();

            if (orderId.Length < 5)
            {
                orderId = geneSomeCharZero(5 - orderId.Length) + orderId;
            }

            return prefix + date + orderId;
        }
        #endregion
        #region 添加退货单和单据明细信息
        /// <summary>
        /// 添加退货单和单据明细信息
        /// </summary>
        /// <param name="nventoryDocModel">退货单</param>
        /// <param name="number">管理员ID</param>
        /// <param name="storeId">店铺ID</param>
        /// <param name="inventoryDocDetailsList">退货单明细集合</param>
        public static void InsertInventoryDoc(InventoryDocModel nventoryDocModel, string number, string storeId, ArrayList inventoryDocDetailsList)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    //获得当前管理员姓名
                    string manageName = ManageDAL.GetNameByAdminID(number);
                    //获得单据类型ID
                    int docTypeId = DocTypeTableDAL.GetDocTypeIDByDocTypeName("TH");
                    nventoryDocModel.DocTypeID = docTypeId;
                    //根据店铺编号获取国家ID
                    int coutryId = CountryDAL.GetIdByStoreId(storeId);

                    //Write by WangHua at 2009-11-19 12:05
                    //nventoryDocModel.DocTypeID = docTypeId;
                    //nventoryDocModel.Currency = coutryId;
                    //nventoryDocModel.DocMaker = manageName;
                    //nventoryDocModel.OriginalDocID = "";
                    //nventoryDocModel.OperationPerson=
                    //nventoryDocModel.BatchCode = "";

                    //往数据库中插入一条订单退货数据
                    CreateInventoryDoc(tran, nventoryDocModel);

                    foreach (InventoryDocDetailsModel inventoryDocDetailsModel in inventoryDocDetailsList)
                    {
                        inventoryDocDetailsModel.DocID = nventoryDocModel.DocID;
                    }

                    //往数据库中出入一条订单明细数据
                    InventoryDocDetailsDAL inventoryDocDetailsDAL = new InventoryDocDetailsDAL();
                    inventoryDocDetailsDAL.CreateInventoryDocDetails(tran, inventoryDocDetailsList);

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

        #endregion

        #region 根据退货单号统计已审核的退货单
        /// <summary>
        /// 根据退货单号统计已审核的退货单
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public static int GetStaInventoryDocByDocId(string docId)
        {
            string sql = "Select Count(*) From InventoryDoc Where  StateFlag = 1 And docID=@num";
            SqlParameter spa = new SqlParameter("@num", SqlDbType.VarChar, 20);
            spa.Value = docId;
            return ((int)DBHelper.ExecuteScalar(sql, spa, CommandType.Text));
        }
        #endregion

        #region 更新退货款状态为已审核
        /// <summary>
        /// 更新退货款状态为已审核
        /// </summary>
        /// <param name="warehouseId">仓库ID</param>
        /// <param name="number">管理员账号</param>
        /// <param name="docId">退货单号</param>
        /// <param name="storeId">退货店铺ID</param>
        public static void UpdateStaInventoryDocOfStateFlag(string depotSeatId, string warehouseId, string number, string docId, string storeId)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    //获得当前管理员姓名
                    string manageName = ManageDAL.GetNameByAdminID(number);

                    //退货单审核后状态修改SQL
                    string sql1 = " UPDATE InventoryDoc SET StateFlag = 1,WareHouseID = " + warehouseId + " ,DepotSeatID= " + depotSeatId + ", docAuditer = @docAuditer ,docAuditTime = @docAuditTime WHERE docID = @docID";
                    SqlParameter[] para ={
										  new SqlParameter ("@docAuditer",SqlDbType.VarChar,20),
										  new SqlParameter ("@docAuditTime" ,SqlDbType .DateTime ),
										  new SqlParameter ("@docID" ,SqlDbType .Char ,20)
									  };
                    para[0].Value = manageName;
                    para[1].Value = MYDateTime.GetCurrentDateTime();
                    para[2].Value = docId;

                    //更新店铺库存SQL
                    string sql2 = @"update Stock set ActualStorage= ActualStorage -
                                ( select productQuantity From InventoryDocDetails Where docID='" + docId + @"' And  productID=Stock.ProductID )
                                ,TotalOut=TotalOut + 
                                ( select productQuantity From InventoryDocDetails Where docID='" + docId + @"' And  productID=Stock.ProductID )
                                Where StoreId='" + storeId + "' And ProductID IN (select productID From InventoryDocDetails Where docID='" + docId + @"')";

                    DBHelper.ExecuteNonQuery(tran, sql1, para, CommandType.Text);
                    DBHelper.ExecuteNonQuery(tran, sql2, null, CommandType.Text);

                    //更新公司库存
                    DataTable dt = DBHelper.ExecuteDataTable("Select productQuantity as Quantity , productID as ProductID From InventoryDocDetails Where docID='" + docId + "'");

                    foreach (DataRow dr in GetNewOrderDetail(dt).Rows)//拆分组合产品
                    {
                        if (((int)DBHelper.ExecuteNonQuery(tran, @"Update ProductQuantity  
																		Set Totalin= Totalin +(" + dr["Quantity"].ToString() + @") 
																		Where ProductID =" + dr["productId"].ToString() + @"
																		And DepotSeatId =" + depotSeatId + @"
                                                                        and warehouseid =" + warehouseId, null, CommandType.Text)) == 0)
                        {
                            DBHelper.ExecuteNonQuery(tran, @"INSERT INTO ProductQuantity
												(ProductID ,TotalIn ,TotalOut , TotalLogicOut , WareHouseID,DepotSeatId ) 
												VALUES( " + dr["ProductID"].ToString() + @" ," + (Convert.ToInt32(dr["Quantity"])) + @", 0 ,0 ," + warehouseId + @"," + depotSeatId + @" )"
                                , null, CommandType.Text);
                        }

                        if (((int)DBHelper.ExecuteNonQuery(tran, @"Update LogicProductInventory  
																		Set Totalin= Totalin +(" + dr["Quantity"].ToString() + @") 
																		Where ProductID =" + dr["productId"].ToString(), null, CommandType.Text)) == 0)
                        {
                            DBHelper.ExecuteNonQuery(tran, @"INSERT INTO LogicProductInventory
												(ProductID ,TotalIn ,TotalOut , TotalLogicOut) 
												VALUES( " + dr["ProductID"].ToString() + @" ," + (Convert.ToInt32(dr["Quantity"])) + @", 0 ,0 )"
                                , null, CommandType.Text);
                        }
                    }
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

        /// <summary>
        /// 拆分产品信息--DataTable
        /// 注：把组合产品拆分成单品
        /// </summary>
        /// <param name="ods">原产品信息</param>
        /// <returns>拆分后产品信息</returns>
        public static DataTable GetNewOrderDetail(DataTable dt)
        {
            DataTable dtnew = new DataTable();
            DataColumn dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "Quantity";
            dtnew.Columns.Add(dc);
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "productId";
            dtnew.Columns.Add(dc);

            foreach (DataRow dr in dt.Rows)
            {
                if (ProductDAL.GetIsCombine(Convert.ToInt32(dr["productId"])))
                {
                    IList<ProductCombineDetailModel> comDetails = ProductCombineDetailDAL.GetCombineDetil(Convert.ToInt32(dr["productId"]));
                    foreach (ProductCombineDetailModel comDetail in comDetails)
                    {
                        int count = 0;
                        foreach (DataRow drnew in dtnew.Rows)
                        {
                            if (Convert.ToInt32(drnew["productId"]) == comDetail.SubProductID)
                            {
                                drnew["Quantity"] = (comDetail.Quantity * Convert.ToInt32(dr["Quantity"])) + Convert.ToInt32(drnew["Quantity"]);
                                count++;
                            }
                        }
                        if (count == 0)
                        {
                            DataRow drx = dtnew.NewRow();
                            drx["Quantity"] = comDetail.Quantity * Convert.ToInt32(dr["Quantity"]);
                            drx["productId"] = comDetail.SubProductID;
                            dtnew.Rows.Add(drx);
                        }
                    }
                }
                else
                {
                    int count = 0;
                    foreach (DataRow drnew in dtnew.Rows)
                    {
                        if (Convert.ToInt32(drnew["productId"]) == Convert.ToInt32(dr["productId"]))
                        {
                            drnew["Quantity"] = Convert.ToInt32(dr["Quantity"]) + Convert.ToInt32(drnew["Quantity"]);
                            count++;
                        }
                    }
                    if (count == 0)
                    {
                        DataRow drx = dtnew.NewRow();
                        drx["Quantity"] = Convert.ToInt32(dr["Quantity"]);
                        drx["productId"] = Convert.ToInt32(dr["productId"]);
                        dtnew.Rows.Add(drx);
                    }

                }
            }

            return dtnew;
        }

        #endregion

        #region 更新订单的状态为无效的
        /// <summary>
        /// 更新订单的状态为无效的
        /// </summary>
        /// <param name="docID"></param>
        public static void UpdateStateFlagAndCloseFlag(string docID)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    string sql = " UPDATE InventoryDoc SET StateFlag = 0,CloseFlag=1,CloseDate=@CloseDate  WHERE docID = @docID ";
                    SqlParameter[] para ={			
										  new SqlParameter ("@CloseDate" ,SqlDbType.DateTime ),
										  new SqlParameter ("@docID" ,SqlDbType .Char ,20)
									  };
                    para[0].Value = MYDateTime.GetCurrentDateTime();
                    para[1].Value = docID;
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

        #region 根据单据号查询单据日志
        /// <summary>
        /// 根据单据号查询单据日志
        /// </summary>
        /// <param name="docId">单据号</param>
        /// <returns></returns>
        public static string GetNoteByDocId(string docId)
        {
            string sql = "SELECT note FROM InventoryDoc WHERE docID = @docID";
            SqlParameter[] para = { new SqlParameter("@docID", SqlDbType.VarChar, 20) };
            para[0].Value = docId;
            return DBHelper.ExecuteScalar(sql, para, CommandType.Text).ToString();
        }
        #endregion

        #region 审核未审核的入库单
        /// <summary>
        /// 审核未审核的入库单  ---DS2012
        /// </summary>
        /// <returns></returns>
        public static int checkDoc(string DocAuditer, DateTime DcAuditTime, string OperateIP, string OperateNum, string DocID, string TempWareHouseID, int changwei)
        {
            string updateSQL = " UPDATE InventoryDoc SET StateFlag = '1',DocAuditer = @num,DocAuditTime = @num1,OperateIP=@num2,OperateNum=@num3 WHERE DocID = @num4";
            SqlParameter[] para = { 
                  new SqlParameter("@num", SqlDbType.VarChar, 25), 
                  new SqlParameter("@num1", SqlDbType.DateTime),
                  new SqlParameter("@num2", SqlDbType.VarChar, 30),
                  new SqlParameter("@num3", SqlDbType.VarChar, 30),
                  new SqlParameter("@num4", SqlDbType.VarChar, 20)
             };
            para[0].Value = DocAuditer;
            para[1].Value = DcAuditTime;
            para[2].Value = OperateIP;
            para[3].Value = OperateNum;
            para[4].Value = DocID;
            //更新公司库存

            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        DBHelper.ExecuteNonQuery(tran, updateSQL, para, CommandType.Text);
                        string sql1 = "Select ProductQuantity as Quantity , ProductID as ProductID From InventoryDocDetails Where DocID=@num";
                        SqlParameter[] para1 = { 
                              new SqlParameter("@num", SqlDbType.VarChar, 20)
                         };
                        para1[0].Value = DocID;
                        DataTable dt = DBHelper.ExecuteDataTable(sql1, para1, CommandType.Text);

                        foreach (DataRow dr in dt.Rows)
                        {
                            if (((int)DBHelper.ExecuteNonQuery(tran, @"Update ProductQuantity Set TotalIn= TotalIn +(" + dr["Quantity"].ToString() + @") Where ProductID =" + dr["ProductID"].ToString() + @"And WareHouseID =" + TempWareHouseID + " and DepotSeatID='" + changwei + @"'", null, CommandType.Text)) != 1)
                            {
                                DBHelper.ExecuteNonQuery(tran, @"INSERT INTO ProductQuantity(ProductID ,TotalIn ,TotalOut , TotalLogicOut , WareHouseID,DepotSeatID ) VALUES( " + dr["productId"].ToString() + @", " + dr["Quantity"].ToString() + @" , 0 , 0 ," + TempWareHouseID + @"," + changwei + @" )", null, CommandType.Text);
                            }
                            //写入公司逻辑汇总仓库库存
                            if ((int)DBHelper.ExecuteNonQuery(tran, @"Update LogicProductInventory Set TotalIn= TotalIn +(" + dr["Quantity"].ToString() + @") Where ProductID =" + dr["productId"].ToString(), null, CommandType.Text) != 1)
                            {
                                DBHelper.ExecuteNonQuery(tran, @"INSERT INTO LogicProductInventory(ProductID ,TotalIn ,TotalOut , TotalLogicOut) VALUES( " + dr["ProductID"].ToString() + @", " + dr["Quantity"].ToString() + @" , 0 , 0 )", null, CommandType.Text);
                            }
                        }
                        tran.Commit();
                        return 1;
                    }
                    catch
                    {
                        tran.Rollback();
                        return 0;
                    }
                }
            }
        }
        #endregion

        #region 将未审核的单据设为无效
        /// <summary>
        /// 将未审核的单据设为无效
        /// </summary>
        /// <returns></returns>
        public static int updDocTypeName(DateTime CloseDate, string DocID, string OperateIP, string OperateNum)
        {
            string updateSQL = " UPDATE InventoryDoc SET StateFlag = '0',CloseFlag='1',CloseDate=@num,OperateIP=@num1,OperateNum=@num2 WHERE DocID = @num3";
            SqlParameter[] para = { 
                  new SqlParameter("@num", SqlDbType.DateTime), 
                  new SqlParameter("@num1", SqlDbType.VarChar,30),
                  new SqlParameter("@num2", SqlDbType.VarChar, 30),
                  new SqlParameter("@num3", SqlDbType.VarChar, 20)
                 
             };
            para[0].Value = CloseDate;
            para[1].Value = OperateIP;
            para[2].Value = OperateNum;
            para[3].Value = DocID;
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        DBHelper.ExecuteNonQuery(tran, updateSQL, para, CommandType.Text);
                        tran.Commit();
                        return 1;
                    }
                    catch
                    {
                        tran.Rollback();
                        return 0;
                    }
                }
            }
        }
        #endregion

        #region 删除未审核的单据
        /// <summary>
        /// 删除未审核的单据   ---DS2012
        /// </summary>
        /// <param name="DocID"></param>
        public static int delDoc(string DocID)
        {
            string SQL_DELETE_opda_depotManageDoc = "DELETE FROM InventoryDoc WHERE DocID = @num";
            string SQL_DELETE_opda_docDetails = "DELETE FROM InventoryDocDetails WHERE DocID = @num";
            SqlParameter[] para = {                   
                  new SqlParameter("@num", SqlDbType.VarChar, 20)
                 
             };
            para[0].Value = DocID;
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {


                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        DBHelper.ExecuteNonQuery(tran, SQL_DELETE_opda_depotManageDoc, para, CommandType.Text);
                        DBHelper.ExecuteNonQuery(tran, SQL_DELETE_opda_docDetails, para, CommandType.Text);
                        tran.Commit();
                        return 1;

                    }
                    catch
                    {
                        tran.Rollback();
                        return 0;
                    }
                }
            }
        }
        #endregion

        #region 得到仓库名称
        /// <summary>
        /// 得到仓库名称
        /// </summary>
        /// <param name="WareHouseId"></param>
        /// <returns></returns>
        public static string GetWarehouseName(string WareHouseId)
        {
            string sSQL = " SELECT WareHouseName FROM WareHouse WHERE WareHouseID = @num";
            SqlParameter[] para = {                   
                  new SqlParameter("@num", SqlDbType.Int)
                 
             };
            para[0].Value = Convert.ToInt32(WareHouseId);
            SqlDataReader reader = DBHelper.ExecuteReader(sSQL, para, CommandType.Text);
            if (reader.Read())
            {
                sSQL = reader[0].ToString();
            }
            else
                sSQL = "";

            reader.Close();

            return sSQL;
        }
        #endregion

        #region 得到库位名称
        /// <summary>
        /// 得到库位名称
        /// </summary>
        /// <param name="DepotSeatID"></param>
        /// <returns></returns>
        public static string GetDepotSeatName(string DepotSeatID)
        {
            //string sSQL = " SELECT WareHouseName FROM WareHouse WHERE DepotSeatID = " + DepotSeatID;
            string Sql = "SELECT SEATNAME FROM dEPOTSEAT WHERE DepotSeatID = @num";
            SqlParameter[] para = {                   
                  new SqlParameter("@num", SqlDbType.Int)
                 
             };
            para[0].Value = Convert.ToInt32(DepotSeatID);
            SqlDataReader reader = DBHelper.ExecuteReader(Sql, para, CommandType.Text);
            if (reader.Read())
            {
                Sql = reader[0].ToString();
            }
            else
                Sql = "";

            reader.Close();

            return Sql;
        }
        #endregion

        #region 得到货币的名称
        /// <summary>
        /// 得到货币的名称
        /// </summary>
        /// <param name="countryid"></param>
        /// <returns></returns>
        public static string GetCurrencyName(string countryid)
        {
            string CurrencyName = "";
            if (countryid != "" && countryid != null)
            {
                string sql = "select a.name from currency a,country b,product c where a.id=b.Rateid and b.countrycode=c.countrycode and c.productid=@num";
                SqlParameter[] para = {                   
                  new SqlParameter("@num", SqlDbType.Int)
                 
               };
                para[0].Value = Convert.ToInt32(countryid);
                CurrencyName = Convert.ToString(DBHelper.ExecuteScalar(sql, para, CommandType.Text));
            }

            return CurrencyName;
        }
        #endregion

        /// <summary>
        /// 获得总金额用于店铺帐户情况
        ///
        /// </summary>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        public static double SelRemitMoneyByStoreID(string storeID)
        {
            string sql = "select isnull(sum(RemitMoney),0)  from Remittances where StoreID=@storeID and isgsqr=1";
            SqlParameter[] para = { new SqlParameter("@storeID", SqlDbType.VarChar, 20) };
            para[0].Value = storeID;
            return Convert.ToDouble(DBHelper.ExecuteScalar(sql, para, CommandType.Text));
            //if (result == DBNull.Value)
            //    return 0.00;
            //else
            //    return result;
        }

        /// <summary>
        /// 获得店铺汇款信息
        /// 
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>        
        public static DataTable GetRemittancesByStoreID(string storeID)
        {
            string sql = "select RemitMoney,StandardCurrency from Remittances where StoreID=@StoreID ";
            SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@StoreID", storeID) };
            return DBHelper.ExecuteDataTable(sql, parm, CommandType.Text);
        }
        /// <summary>
        ///  查询到的已审汇款总额——ds2012——tianfeng
        /// 
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="paymentExpectNum"></param>
        /// <returns></returns>
        /// 
        public static double dSelRemitMoneyByStoreIDAnExpectNum_Y(string storeID, int PayExpectNum)
        {
            string sql = "";
            if (PayExpectNum == 0)
            {
                sql = "SELECT isnull(sum(RemitMoney),0) FROM Remittances WHERE RemitNumber=@storeID and RemitStatus=0  and isgsqr=1 and Remittances.relationorderid='' ";
                SqlParameter[] para = new SqlParameter[] { new SqlParameter("@StoreID", storeID) };
                return Convert.ToDouble(DBHelper.ExecuteScalar(sql, para, CommandType.Text));
            }
            else
            {
                sql = "SELECT isnull(sum(RemitMoney),0) FROM Remittances WHERE RemitNumber=@storeID and RemitStatus=0 and isgsqr=1 and PayExpectNum=@paymentExpectNum and Remittances.relationorderid='' ";
                SqlParameter[] para = new SqlParameter[] { new SqlParameter("@StoreID", storeID), new SqlParameter("@paymentExpectNum", PayExpectNum) };
                return Convert.ToDouble(DBHelper.ExecuteScalar(sql, para, CommandType.Text));
            }

        }
        /// <summary>
        ///  未审汇款总额——ds2012——tianfeng
        ///
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="paymentExpectNum"></param>
        /// <returns></returns>

        public static double SelRemitMoneyByStoreIDAnExpectNum_N(string storeID, int PayExpectNum)
        {
            if (PayExpectNum != 0)
            {
                string sql = "SELECT isnull(sum(RemitMoney),0) FROM Remittances WHERE RemitNumber=@storeID and RemitStatus=0 and isgsqr=0 and PayExpectNum=@paymentExpectNum and Remittances.relationorderid='' ";
                SqlParameter[] para = new SqlParameter[] { new SqlParameter("@StoreID", storeID), new SqlParameter("@paymentExpectNum", PayExpectNum) };
                return Convert.ToDouble(DBHelper.ExecuteScalar(sql, para, CommandType.Text));
            }
            else
            {
                string sql = "SELECT isnull(sum(RemitMoney),0) FROM Remittances WHERE RemitNumber=@storeID and RemitStatus=0 and isgsqr=0 and Remittances.relationorderid=''";
                SqlParameter[] para = new SqlParameter[] { new SqlParameter("@StoreID", storeID) };
                return Convert.ToDouble(DBHelper.ExecuteScalar(sql, para, CommandType.Text));
            }
        }

        /// <summary>
        ///  库存情况,根据店铺和日期
        ///
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static DataTable GetStock(string storeID, int payExpectNum)
        {
            DataTable dt = new DataTable();
            string sql = "select * from Remittances where storeid=@storeid  and  PayExpectNum=@payExpectNum order by PayExpectNum ";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@storeid", storeID), new SqlParameter("@payExpectNum", payExpectNum) };
            dt = DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            return dt;
        }
        /// <summary>
        ///  库存情况,根据店铺得到全部
        /// 
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static DataTable GetStockBySoteID(string storeID)
        {
            DataTable dt = new DataTable();
            string sql = "select * from Remittances where storeid=@storeid order by PayExpectNum ";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@storeid", storeID) };
            dt = DBHelper.ExecuteDataTable(sql, para, CommandType.Text);
            return dt;
        }
        /// <summary>
        ///  查看换货时库存情况
        /// 
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public DataTable GetReplacement(string condition)
        {
            DataTable dt = new DataTable();

            string sql = "select a.DisplaceOrderID,OutStrageOrderID,OutTotalMoney,InTotalMoney,Remark, a.id ,a.StoreID,a.RefundmentOrderID,a.StoreOrderID,a.ExpectNum,a.StateFlag,a.CloseFlag,b.InQuantity*b.Price as ingoodsmoney,b.OutQuantity*b.Price as outgoodmoney ,a.MakeDocDate from Replacement as a inner join ReplacementDetail as b on (a.DisplaceOrderID=b.DisplaceOrderID) where  " + condition;
            return dt = DBHelper.ExecuteDataTable(sql);
        }
        /// <summary>
        /// 得到店铺汇款的付款期数
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>

        public static IList<RemittancesModel> GetPayExpectNum()
        {
            string sql = "select PayExpectNum from Remittances  order by PayExpectNum ";
            IList<RemittancesModel> remittancesList = null;
            SqlDataReader dr = DBHelper.ExecuteReader(sql);
            if (dr.HasRows)
            {
                remittancesList = new List<RemittancesModel>();
                if (dr.Read())
                {
                    RemittancesModel remittancesModel = new RemittancesModel();
                    remittancesModel.PayexpectNum = dr.GetInt32(0);
                    remittancesList.Add(remittancesModel);

                }
            }
            dr.Close();
            return remittancesList;
        }

        public static bool SetQuashBillOutOrder(string outStorageOrderID, List<InventoryDocDetailsModel> l_ddm, string storeorderid)
        {
            List<InventoryDocDetailsModel> l_ddmZH = (List<InventoryDocDetailsModel>)GetNewInventoryDocDetails(l_ddm);

            bool cg = true;

            SqlTransaction tran = null;
            SqlConnection con = null;
            SqlDataReader dr1 = null;

            try
            {
                con = new SqlConnection(DBHelper.connString);

                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                tran = con.BeginTransaction();
                cmd.Transaction = tran;

                cmd.CommandText = "select inWareHouseID,inDepotSeatID from dbo.InventoryDoc where DocID=@outStorageOrderID";
                cmd.Parameters.AddWithValue("@outStorageOrderID", outStorageOrderID);
                dr1 = cmd.ExecuteReader();

                if (dr1.Read())
                {
                    int wareHouseID = Convert.ToInt32(dr1["inWareHouseID"]);
                    int depotSeatID = Convert.ToInt32(dr1["inDepotSeatID"]);

                    dr1.Close();


                    for (int i = 0; i < l_ddmZH.Count; i++)
                    {
                        int productID = Convert.ToInt32(l_ddmZH[i].ProductID);
                        double productQuantity = Convert.ToDouble(l_ddmZH[i].ProductQuantity);

                        cmd.CommandText = "update ProductQuantity set TotalOut=TotalOut-@productQuantity where ProductID=@productID and WareHouseID=@wareHouseID and DepotSeatID=@depotSeatID";
                        cmd.Parameters.AddWithValue("@productQuantity", productQuantity);
                        cmd.Parameters.AddWithValue("@productID", productID);
                        cmd.Parameters.AddWithValue("@wareHouseID", wareHouseID);
                        cmd.Parameters.AddWithValue("@depotSeatID", depotSeatID);
                        if (cmd.ExecuteNonQuery() != 1)
                        {
                            cg = false;
                            break;
                        }
                        else
                            cg = true;
                    }

                    if (cg)
                    {
                        cmd.CommandText = "update dbo.StoreOrder set IsGeneOutBill='N',OutStorageOrderID='' where storeorderid=@storeorderid";
                        cmd.Parameters.AddWithValue("@storeorderid", storeorderid);
                        if (cmd.ExecuteNonQuery() != 1)
                        {
                            cg = false;
                        }
                        else
                        {
                            cmd.CommandText = "delete from InventoryDoc where DocID=@outStorageOrderID";
                            cmd.Parameters.AddWithValue("@outStorageOrderID", outStorageOrderID);
                            if (cmd.ExecuteNonQuery() != 1)
                            {
                                cg = false;
                            }
                            else
                            {
                                cmd.CommandText = "delete from dbo.InventoryDocDetails where DocID=@DocID";
                                cmd.Parameters.AddWithValue("@DocID", outStorageOrderID);

                                if (cmd.ExecuteNonQuery() == 0)
                                {
                                    cg = false;
                                }
                                else
                                {
                                    //cg = true;

                                    cmd.CommandText = @"if exists (select * from UniteDoc where DocIDs like '%" + outStorageOrderID + "%')" +
                                                        @"begin
	                                                        delete from dbo.UniteDoc where DocIDs like '%" + outStorageOrderID + "%'" +
                                                        "end";
                                    cmd.ExecuteNonQuery();

                                    cg = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    cg = false;
                }

                if (cg == false)
                    tran.Rollback();
                else
                    tran.Commit();

                return cg;
            }
            catch (Exception)
            {
                if (dr1 != null)
                    dr1.Close();
                if (tran != null)
                    tran.Rollback();
                if (con != null)
                    con.Close();

                return false;
            }
        }

        public static DataTable GetInventoryDocByCondition(string condition)
        {
            string sql = "select * from InventoryDoc As D where " + condition + "  order by docID";
            return DBHelper.ExecuteDataTable(sql);
        }

        /// <summary>
        /// 得到可用的单编号，
        /// </summary>		
        public static string GetNewOrderID()
        {
            string _date = MYDateTime.ToYYMMDDHHmmssString();

            string _orderId = string.Empty; ;

            string SQL_SELECT_Max_ID = "SELECT Top 1 ID FROM StoreOrder  Order By ID  Desc ";
            SqlDataReader reader = DBHelper.ExecuteReader(SQL_SELECT_Max_ID);
            if (reader.Read())
                _orderId += (reader.GetInt32(0) + 1).ToString();
            else
                _orderId += "1";

            reader.Close();

            if (_orderId.Length < 5)
            {
                _orderId = geneSomeCharZero(5 - _orderId.Length) + _orderId;
            }

            return _date + _orderId;
        }

        /// <summary>
        /// Out to excel of the data about InventoryDoc  ---DE2012
        /// </summary>
        /// <param name="condition">Condiiton</param>
        /// <returns>Return DataTable Object</returns>
        public static DataTable OutToExcel_InventoryDoc_More(string condition)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@condition",SqlDbType.NVarChar,2000)
            };
            sparams[0].Value = condition;
            return DBHelper.ExecuteDataTable("OutToExcel_InventoryDoc_More", sparams, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Judge the docId whether Auditing by docId  ---DS2012
        /// </summary>
        /// <param name="docId">DocId</param>
        /// <param name="i">1 Stand for effective,0 stand for Invalid</param>
        /// <returns>Return the counts of the auditing by docId</returns>
        public static int IsAuditingByDocId(string docId, int i)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@docId",SqlDbType.NVarChar,20),
                new SqlParameter("@stateFlag",SqlDbType.Int)
            };
            sparams[0].Value = docId;
            sparams[1].Value = i;
            return Convert.ToInt32(DBHelper.ExecuteScalar("IsAuditingByDocId", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Judge the docId whether exists before update or delete  ---DS2012
        /// </summary>
        /// <param name="docId">DocId</param>
        /// <returns>Return the counts of the docId by docId</returns>
        public static int DocIdIsExistByDocId(string docId)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
	            new SqlParameter("@docId",SqlDbType.NVarChar,20)
            };
            sparams[0].Value = docId;
            return Convert.ToInt32(DBHelper.ExecuteScalar("DocIdIsExistByDocId", sparams, CommandType.StoredProcedure));
        }


        #region 得到货币的名称
        /// <summary>
        /// 根据币种id获取币种Name
        /// </summary>
        /// <param name="countryid"></param>
        /// <returns></returns>
        public static string GetECurrencyName(string countryid)
        {
            string CurrencyName = string.Empty;
            string sqlstr = "select [name] from currency where id=@num";
            SqlParameter spa = new SqlParameter("@num", SqlDbType.Int);
            spa.Value = countryid;
            CurrencyName = Convert.ToString(DBHelper.ExecuteScalar(sqlstr, spa, CommandType.Text));
            return CurrencyName;
        }
        #endregion

        #region 获取退货款备注
        /// <summary>
        /// 获取退货款备注——ds2012——tianfeng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string QueryRemark(string id)
        {
            string sql = "select isnull(Note,'') from InventoryDoc where docid=@docid";
            SqlParameter[] par = new SqlParameter[]{
                new SqlParameter("@docid",id)
            };
            string str = DBHelper.ExecuteScalar(sql, par, CommandType.Text).ToString();
            return str;
        }
        #endregion
        /// <summary>
        /// 获取币种名称  ---DS2012
        /// </summary>
        /// <param name="Currency"></param>
        /// <returns></returns>
        public static string GetCurrencyNameByID(string Currency)
        {
            return DBHelper.ExecuteScalar("select b.[Name] from Country as a,Currency as b where b.ID=@Currency  and a.RateID=b.ID ", new SqlParameter("@Currency", Currency), CommandType.Text).ToString();
        }
    }
}