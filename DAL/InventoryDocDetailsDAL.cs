using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{

    /// <summary>
    /// 单据明细
    /// </summary>
    public class InventoryDocDetailsDAL
    {
        CommonDataDAL commonDataDAL = new CommonDataDAL();
        /// <summary>
        /// 产生单据明细
        /// </summary>
        /// <param name="DocID"></param>
        /// <returns></returns>
        public DataTable GetInventoryDocDetailsByDocID(string DocID)
        {
            SqlParameter[] param = new SqlParameter[] { 
            new SqlParameter("@DocID", DocID)
            };
            return DBHelper.ExecuteDataTable("GetInventoryDocDetailsByDocID", param, CommandType.StoredProcedure);
        }
        #region 入库单明细
        /// <summary>
        /// 分页(查看入库单明细)
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
        public DataTable InStoreOrderDetails(int PageIndex, int PageSize, string table, string columns, string condition, string key, out int RecordCount, out int PageCount)
        {
            return commonDataDAL.GetDataTablePage(PageIndex, PageSize, table, columns, condition, key, out RecordCount, out PageCount);
        }
        #endregion
        #region 生成单据明细
        public int CreateBillofDocumentDetails(ArrayList tobjopda_docDetailsList)
        {
            string SQL_INSERT_opda_docDetails = @"INSERT INTO InventoryDocDetails( DocID, ProductID,ProductQuantity,UnitPrice,MeasureUnit,PV, ExpectNum, ProductTotal)VALUES  ( @DocID, @ProductID,@ProductQuantity, @UnitPrice,@MeasureUnit, @PV, @ExpectNum, @ProductTotal)";
            int count = 0;

            foreach (InventoryDocDetailsModel tobjopda_docDetails in tobjopda_docDetailsList)
            {
                SqlParameter[] objPara ={ 
                                       new SqlParameter("@DocID",SqlDbType.VarChar ,20 ),
                                       new SqlParameter("@ProductID",SqlDbType.Int  ),
                                       new SqlParameter("@ProductQuantity",SqlDbType.Money  ),
                                       new SqlParameter("@UnitPrice", SqlDbType.Money ),
                                       new SqlParameter("@MeasureUnit" , SqlDbType.VarChar ,50),
                                       new SqlParameter("@PV", SqlDbType.Money ),
                                       new SqlParameter("@ExpectNum", SqlDbType.Int  ),
                                       //new SqlParameter("@SelectedIndex", SqlDbType.Int ),
                                       new SqlParameter("@ProductTotal",SqlDbType.Money  )
                                   };
                objPara[0].Value = tobjopda_docDetails.DocID;
                objPara[1].Value = tobjopda_docDetails.ProductID;
                objPara[2].Value = tobjopda_docDetails.ProductQuantity;
                objPara[3].Value = tobjopda_docDetails.UnitPrice;
                //objPara[4].Value = tobjopda_docDetails.MeasureUnit.ToString();
                objPara[4].Value = "";
                objPara[5].Value = tobjopda_docDetails.PV;
                objPara[6].Value = tobjopda_docDetails.ExpectNum;
                //objPara[7].Value = tobjopda_docDetails.SelectedIndex;
                objPara[8].Value = tobjopda_docDetails.ProductTotal;
                int a = DBHelper.ExecuteNonQuery(SQL_INSERT_opda_docDetails, objPara, CommandType.Text);
                count++;
            }
            return count;
        }
        /// <summary>
        /// 生成单据明细(汪华)（2009-10-15所改）
        /// </summary>
        /// <param name="tran">处理事务</param>
        /// <param name="tobjopda_depotManageDoc">某种单据明细类对象数组</param>
        /// <returns>返回插入所影响的行数</returns>
        public static int CreateBillofDocumentDetails(SqlTransaction tran, ArrayList tobjopda_docDetailsList)
        {
            string SQL_INSERT_opda_docDetails = @"INSERT INTO InventoryDocDetails
										( DocID, ProductID, 
										ProductQuantity,UnitPrice,
										MeasureUnit,PV, ExpectNum, 
										 ProductTotal,Batch) 
										 VALUES  ( @DocID, @ProductID, 
										@ProductQuantity, @UnitPrice,
										@MeasureUnit, @PV, @ExpectNum, 
										 @ProductTotal,@Batch)";
            SqlParameter[] objPara ={ 
									   new SqlParameter("@DocID",SqlDbType.VarChar ,20 ),
									   new SqlParameter("@ProductID",SqlDbType.Int  ),
									   new SqlParameter("@ProductQuantity",SqlDbType.Money  ),
									   new SqlParameter("@UnitPrice", SqlDbType.Money ),
									   new SqlParameter("@MeasureUnit" , SqlDbType.VarChar ,50),
									   new SqlParameter("@PV", SqlDbType.Money ),
									   new SqlParameter("@ExpectNum", SqlDbType.Int  ),
                                       //new SqlParameter("@SelectedIndex", SqlDbType.Int ),
									   new SqlParameter("@ProductTotal",SqlDbType.Money  ),
                                       new SqlParameter("@Batch",SqlDbType.VarChar,50),
                                       //new SqlParameter("@DepotSeatID",SqlDbType.Int),
                                       //new SqlParameter("@pici",SqlDbType.VarChar,20),
                                       //new SqlParameter("@MFD_DT",SqlDbType.DateTime),
                                       //new SqlParameter("@Exp_DT",SqlDbType.DateTime)
								   };

            int count = 0;

            foreach (InventoryDocDetailsModel tobjopda_docDetails in tobjopda_docDetailsList)
            {
                objPara[0].Value = tobjopda_docDetails.DocID;
                objPara[1].Value = tobjopda_docDetails.ProductID;
                objPara[2].Value = tobjopda_docDetails.ProductQuantity;
                objPara[3].Value = tobjopda_docDetails.UnitPrice;
                //objPara[4].Value = tobjopda_docDetails.MeasureUnit;
                objPara[4].Value = "";
                objPara[5].Value = tobjopda_docDetails.PV;
                objPara[6].Value = tobjopda_docDetails.ExpectNum;
                //objPara[7].Value = tobjopda_docDetails.SelectedIndex;
                objPara[7].Value = tobjopda_docDetails.ProductTotal;
                objPara[8].Value = tobjopda_docDetails.Batch;
                //objPara[10].Value = tobjopda_docDetails.DepotSeatID;
                //objPara[11].Value = tobjopda_docDetails.Pici;
                //if (tobjopda_docDetails.MFD_DT == null)
                //    objPara[12].Value = DBNull.Value;
                //else
                //    objPara[12].Value = tobjopda_docDetails.MFD_DT;
                //if (tobjopda_docDetails.EffectiveDate == null)
                //    objPara[13].Value = DBNull.Value;
                //else
                //    objPara[13].Value = tobjopda_docDetails.EffectiveDate;
                DBHelper.ExecuteNonQuery(tran, SQL_INSERT_opda_docDetails, objPara, CommandType.Text);
                count++;
            }
            return count;
        }
        #endregion
        #region 入库单详细信息
        public static DataTable getStoageInDetails(string ID)
        {
            string sSQL = @"Select *  From InventoryDocDetails D inner join 
								Product P on D.ProductID = P.ProductID inner join 
								ProductUnit U on P.SmallProductUnitID =  U.ProductUnitID 
								Where DocID=@ID";
            SqlParameter[] para = { new SqlParameter("@ID", SqlDbType.VarChar, 20) };
            para[0].Value = ID;
            return DBHelper.ExecuteDataTable(sSQL, para, CommandType.Text);
        }
        #endregion

        #region 生成单据明细
        /// <summary>
        /// 生成单据明细
        /// </summary>
        /// <param name="tran">处理事务</param>
        /// <param name="tobjopda_depotManageDoc">某种单据明细类对象数组</param>
        /// <returns></returns>
        public void CreateInventoryDocDetails(SqlTransaction tran, ArrayList inventoryDocDetailsList)
        {
            string creatSql = @"INSERT INTO InventoryDocDetails
										( docID, productID, 
										productQuantity, unitPrice,
										measureUnit, pv, ExpectNum, 
										 productTotal) 
										 VALUES  ( @docID, @productID, 
										@productQuantity, @unitPrice,
										@measureUnit, @pv, @ExpectNum, 
										 @productTotal)";

            SqlParameter[] objPara ={ 
									   new SqlParameter("@docID",SqlDbType.VarChar ,20 ),
									   new SqlParameter("@productID",SqlDbType.Int  ),
									   new SqlParameter("@productQuantity",SqlDbType.Money  ),
									   new SqlParameter("@unitPrice", SqlDbType.Money ),
									   new SqlParameter("@measureUnit" , SqlDbType.VarChar ,50),
									   new SqlParameter("@pv", SqlDbType.Money ),
									   new SqlParameter("@ExpectNum", SqlDbType.Int  ),
                                       //new SqlParameter("@selectedIndex", SqlDbType.Int ),
									   new SqlParameter("@productTotal",SqlDbType.Money  )
								   };

            foreach (InventoryDocDetailsModel inventoryDocDetailsModel in inventoryDocDetailsList)
            {
                objPara[0].Value = inventoryDocDetailsModel.DocID;
                objPara[1].Value = inventoryDocDetailsModel.ProductID;
                objPara[2].Value = inventoryDocDetailsModel.ProductQuantity;
                objPara[3].Value = inventoryDocDetailsModel.UnitPrice;
                objPara[4].Value = inventoryDocDetailsModel.MeasureUnit;
                objPara[5].Value = inventoryDocDetailsModel.PV;
                objPara[6].Value = inventoryDocDetailsModel.ExpectNum;
                //objPara[7].Value = inventoryDocDetailsModel.SelectedIndex;
                objPara[7].Value = inventoryDocDetailsModel.ProductTotal;
                DBHelper.ExecuteNonQuery(tran, creatSql, objPara, CommandType.Text);
            }


        }
        #endregion

        #region 单信息
        public static DataTable getStoageIn(string ID)
        {
            string sSQL = @"select d.*,p.[Name] from
                            InventoryDoc As D,ProviderInfo as p 
                                where d.Provider=p.ID 
								and DocID=@ID";
            SqlParameter[] para = { new SqlParameter("@ID", SqlDbType.VarChar, 20) };
            para[0].Value = ID;
            return DBHelper.ExecuteDataTable(sSQL, para, CommandType.Text);
        }
        #endregion


        /// <summary>
        /// 根据订单号查询出订单表
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public static DataTable GetProductsById(string docId)
        {
            string sql = "select a.ID, a.DocTypeID, a.DocID, a.DocMakeTime,a.DocMaker,a.Client, d.warehousename,a.TotalMoney,c.seatname,a.TotalPV, a.ExpectNum,  a.Note, a.StateFlag, a.CloseFlag, a.CloseDate, a.BatchCode,a.OriginalDocID, a.Address, a.Flag, a.Charged, a.Reason from InventoryDoc a  left outer join warehouse d on d.warehouseid=a.inWareHouseID left outer join DepotSeat c on a.inDepotSeatID=c.depotseatid  where a.DocID=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@num",SqlDbType.VarChar,20)
                
            };
            sparams[0].Value = docId;
            //string sql = "select docdetailsID,docid,productID,productQuantity,unitprice,measureunit,pv,expectnum,selectedindex,producttotal,batch,depotseatID From InventoryDocDetails Where docID='" + docId + "'";
            return DBHelper.ExecuteDataTable(sql, sparams, CommandType.Text);
        }

        public static DataTable GetProductsByIdTwo(string docId)
        {
            string sql = "select a.ID, a.DocTypeID, a.DocID, a.DocMakeTime,a.DocMaker,a.Client, d.warehousename,a.TotalMoney,c.seatname,a.TotalPV, a.ExpectNum,  a.Note, a.StateFlag, a.CloseFlag, a.CloseDate, a.BatchCode,a.OriginalDocID, a.Address,  a.Flag, a.Charged, a.Reason from InventoryDoc a  left outer join warehouse d on d.warehouseid=a.WareHouseID left outer join DepotSeat c on a.DepotSeatID=c.depotseatid  where a.DocID=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@num",SqlDbType.VarChar,20)
                
            };
            sparams[0].Value = docId;
            //string sql = "select docdetailsID,docid,productID,productQuantity,unitprice,measureunit,pv,expectnum,selectedindex,producttotal,batch,depotseatID From InventoryDocDetails Where docID='" + docId + "'";
            return DBHelper.ExecuteDataTable(sql, sparams, CommandType.Text);
        }

        public static DataTable GetProductsBillById(string docId)
        {
            string sql = "select dbo.GetTypeName(a.DocTypeID) as DocTypename, a.DocID,a.DocTypeID, a.DocMakeTime,a.DocMaker,a.TotalMoney, a.TotalPV, a.ExpectNum, a.DocAuditer, a.DocAuditTime from InventoryDoc a   where a.DocID=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@num",SqlDbType.VarChar,20)
                
            };
            sparams[0].Value = docId;
            //string sql = "select docdetailsID,docid,productID,productQuantity,unitprice,measureunit,pv,expectnum,selectedindex,producttotal,batch,depotseatID From InventoryDocDetails Where docID='" + docId + "'";
            return DBHelper.ExecuteDataTable(sql, sparams, CommandType.Text);
        }

        public static DataTable GetProductsByIdDB(string docId)
        {
            string sql = "select a.inwarehouseid,a.indepotseatid, a.ID, a.DocTypeID, a.DocID, a.DocMakeTime,a.DocMaker,a.Client, d.warehousename,a.TotalMoney,c.seatname,a.TotalPV, a.ExpectNum,  a.Note, a.StateFlag, a.CloseFlag, a.CloseDate, a.BatchCode,a.OriginalDocID, a.Address, a.Flag, a.Charged, a.Reason from InventoryDoc a  left outer join warehouse d on d.warehouseid=a.WareHouseID left outer join DepotSeat c on a.DepotSeatID=c.depotseatid    where a.DocID=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@num",SqlDbType.VarChar,20)
                
            };
            sparams[0].Value = docId;
            //string sql = "select docdetailsID,docid,productID,productQuantity,unitprice,measureunit,pv,expectnum,selectedindex,producttotal,batch,depotseatID From InventoryDocDetails Where docID='" + docId + "'";
            return DBHelper.ExecuteDataTable(sql, sparams, CommandType.Text);
        }


        /// <summary>
        /// 根据订单号查询出订单明细表的产品ID和数量
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public static DataTable GetProductIdAndQuantityByDocId(string docId)
        {
            string sql = "select productid,productQuantity From InventoryDocDetails Where docID=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@num",SqlDbType.VarChar,20)
                
            };
            sparams[0].Value = docId;
            return DBHelper.ExecuteDataTable(sql, sparams, CommandType.Text);
        }
        /// <summary>
        /// 根据订单号查询出订单明细表
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public static DataTable GetProductsByDocId(string docId)
        {
            string sql = "select i.docdetailsID,i.docid,i.productID,i.productQuantity,i.unitprice,i.measureunit,i.pv,i.expectnum,i.producttotal,i.batch,p.productName from InventoryDocDetails as i left outer join product as p on p.productID=i.productID  where DocID=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@num",SqlDbType.VarChar,20)
                
            };
            sparams[0].Value = docId;
            //string sql = "select docdetailsID,docid,productID,productQuantity,unitprice,measureunit,pv,expectnum,selectedindex,producttotal,batch,depotseatID From InventoryDocDetails Where docID='" + docId + "'";
            return DBHelper.ExecuteDataTable(sql, sparams, CommandType.Text);
        }

        public static DataTable GetProById(string docId)
        {
            string sql = "select * from InventoryDoc As D  where D.docTypeId=(select DocTypeID from DocTypeTable Where DocTypeCode='TH') and DocID='" + docId + "'";
            //string sql = "select docdetailsID,docid,productID,productQuantity,unitprice,measureunit,pv,expectnum,selectedindex,producttotal,batch,depotseatID From InventoryDocDetails Where docID='" + docId + "'";
            return DBHelper.ExecuteDataTable(sql);
        }

        //根据店名得到店的库存信息在换货中用此方法
        public StoreInfoModel GetStoreInfoByStoreID(string storeID)
        {
            //select * from StoreInfo where Id=@id

            string sql = "select StoreName,StoreAddress, PostalCode,HomeTele,MobileTele,Name from  StoreInfo where storeid=@storeid";
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@storeid", storeID) };
            SqlDataReader dr = DBHelper.ExecuteReader(sql, param, CommandType.Text);

            StoreInfoModel store = null;
            while (dr.Read())
            {
                store = new StoreInfoModel();
                store.StoreName = dr.GetString(dr.GetOrdinal("StoreName"));
                store.StoreAddress = dr.GetString(dr.GetOrdinal("StoreAddress"));
                store.PostalCode = dr.GetString(dr.GetOrdinal("PostalCode"));
                store.HomeTele = dr.GetString(dr.GetOrdinal("HomeTele"));
                store.MobileTele = dr.GetString(dr.GetOrdinal("MobileTele"));
                store.Name = dr.GetString(dr.GetOrdinal("Name"));
            }
            dr.Close();
            return store;
        }

        #region 生成一个单据，包括各种单据［出库，入库，红单，退货等］
        public static int CreateNewBillofDocument(InventoryDocModel tobjopda_depotManageDoc)
        {
            string SQL_INSERT_opda_depotManageDoc = @"INSERT INTO InventoryDoc(
								DocTypeID,DocID, DocMakeTime, 
								 DocMaker,
								Provider, Client, WareHouseID,DepotSeatID, TotalMoney, TotalPV,
								 ExpectNum,
								Note,StateFlag , OriginalDocID
								)
						VALUES( @int_docTypeID, @cha_docID, @dat_docMakeTime, 
								@cha_docMaker, 
								@vch_provider, @cha_client, @warehouseID,@DepotSeatID, @num_totalMoney, @num_totalPV,
								 @int_qishu,
								 @nvch_note ,@StateFlag ,@OriginalDocID)";
            SqlParameter[] objParamArray;

            objParamArray = new SqlParameter[]{				
												  new SqlParameter("@int_docTypeID", tobjopda_depotManageDoc.DocTypeID),
												  new SqlParameter("@cha_docID", tobjopda_depotManageDoc.DocID),
												  new SqlParameter("@dat_docMakeTime", tobjopda_depotManageDoc.DocMakeTime),
												  new SqlParameter("@cha_docMaker", tobjopda_depotManageDoc.DocMaker),
												  new SqlParameter("@vch_provider", tobjopda_depotManageDoc.Provider),
												  new SqlParameter("@cha_client", tobjopda_depotManageDoc.Client),
												  new SqlParameter("@warehouseID", tobjopda_depotManageDoc.WareHouseID),
                                                  new SqlParameter("@DepotSeatID",tobjopda_depotManageDoc.DepotSeatID),
												  new SqlParameter("@num_totalMoney", tobjopda_depotManageDoc.TotalMoney),
												  new SqlParameter("@num_totalPV", tobjopda_depotManageDoc.TotalPV),
                                                  //new SqlParameter("@cha_motherID", tobjopda_depotManageDoc.MotherID),
												  new SqlParameter("@int_qishu", tobjopda_depotManageDoc.ExpectNum),
                                                  //new SqlParameter("@vch_Cause", tobjopda_depotManageDoc.Cause),
												  new SqlParameter("@nvch_note", tobjopda_depotManageDoc.Note),
												  new SqlParameter("@StateFlag", tobjopda_depotManageDoc.StateFlag ),
												  new SqlParameter("@OriginalDocID" , tobjopda_depotManageDoc .OriginalDocID  ),
                                                  //new SqlParameter("@Currency" , tobjopda_depotManageDoc.Currency  )
											  };

            return DBHelper.ExecuteNonQuery(SQL_INSERT_opda_depotManageDoc, objParamArray, CommandType.Text);

        }
        /// <summary>
        /// 生成一个单据，包括各种单据［出库，入库，红单，退货等］，返回受影响的行数
        /// </summary>
        /// <param name="tran">处理事务</param>
        /// <param name="tobjopda_depotManageDoc">某种单据类对象</param>
        /// <returns></returns>
        public static int CreateNewBillofDocument(SqlTransaction tran, InventoryDocModel tobjopda_depotManageDoc)
        {
            string SQL_INSERT_opda_depotManageDoc = @"INSERT INTO InventoryDoc(
								DocTypeID,DocID, DocMakeTime, 
								 DocMaker,
								Provider, Client, WareHouseID, DepotSeatID,TotalMoney, TotalPV,
								 ExpectNum,
								Note,StateFlag , OriginalDocID
								)
						VALUES( @int_docTypeID, @cha_docID, @dat_docMakeTime, 
								@cha_docMaker, 
								@vch_provider, @cha_client, @warehouseID, @DepotSeatID,@num_totalMoney, @num_totalPV,
								 @int_qishu,
								 @nvch_note ,@StateFlag ,@OriginalDocID)";
            SqlParameter[] objParamArray;

            objParamArray = new SqlParameter[]{				
												  new SqlParameter("@int_docTypeID", tobjopda_depotManageDoc.ID),
												  new SqlParameter("@cha_docID", tobjopda_depotManageDoc.DocID),
												  new SqlParameter("@dat_docMakeTime", tobjopda_depotManageDoc.DocMakeTime),
												  new SqlParameter("@cha_docMaker", tobjopda_depotManageDoc.DocMaker),
												  new SqlParameter("@vch_provider", tobjopda_depotManageDoc.Provider),
												  new SqlParameter("@cha_client", tobjopda_depotManageDoc.Client),
												  new SqlParameter("@warehouseID", tobjopda_depotManageDoc.WareHouseID),
                                                  new SqlParameter("@DepotSeatID",tobjopda_depotManageDoc.DepotSeatID),
												  new SqlParameter("@num_totalMoney", tobjopda_depotManageDoc.TotalMoney),
												  new SqlParameter("@num_totalPV", tobjopda_depotManageDoc.TotalPV),
                                                  //new SqlParameter("@cha_motherID", tobjopda_depotManageDoc.MotherID),
												  new SqlParameter("@int_qishu", tobjopda_depotManageDoc.ExpectNum),
                                                  //new SqlParameter("@vch_Cause", tobjopda_depotManageDoc.Cause),
												  new SqlParameter("@nvch_note", tobjopda_depotManageDoc.Note),
												  new SqlParameter("@StateFlag", tobjopda_depotManageDoc.StateFlag ),
												  new SqlParameter("@OriginalDocID" , tobjopda_depotManageDoc .OriginalDocID  ),
                                                  //new SqlParameter("@Currency" , tobjopda_depotManageDoc.Currency  )
											  };

            return DBHelper.ExecuteNonQuery(tran, SQL_INSERT_opda_depotManageDoc, objParamArray, CommandType.Text);

        }
        #endregion
    }
}
