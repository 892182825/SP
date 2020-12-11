using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using Model.Other;

namespace DAL
{
    /// <summary>
    /// 店铺库存表模型对应的数据访问层
    /// </summary>
    public class StockDAL
    {
        /// <summary>
        /// 根据店铺ID返回对应店铺库存
        /// </summary>
        /// <param name="stockId"></param>
        /// <returns></returns>
        public IList<StockModel> GetStoreStockByStoreId(PaginationModel pagin, int stockId)
        {
            /*
                用到列 商品名称(根据产品编号ProductId链表)，规格(根据商品表字段链表)，型号(根据商品表字段链表)，单位(根据商品表字段链表)，
             * 周转库存(TurnStorage)，入库数量(TotalIn)，出库数量(TotalOut)，实际数量(ActualStorage)，应补数量(TotalIn- TotalOut+ InWayCount)显示绝对值，在途数量(InwayCount)
             */
            return null;
        }

        /// <summary>
        /// 根据店铺编号获取可发货数量
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns></returns>
        public static DataTable GetLackTotalNumber(string storeid)
        {
            string sql = "select isnull((Lacktotalnumber-sendtotalnumber),0) as sumnum,s.productid pid,p.productname pname,p.preferentialprice price,p.preferentialpv pv from  stock s,product p where p.productid=s.productid and isnull((Lacktotalnumber-sendtotalnumber),0)>0 and storeid=@storeid ";
            return DBHelper.ExecuteDataTable(sql, new SqlParameter[] { new SqlParameter("@storeid", storeid) }, CommandType.Text);
        }

        /// <summary>
        /// 根据店铺获取库存实际数量
        /// </summary>
        /// <param name="proid"></param>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static int GetCountByProIdAndStoreId(int proid, string storeid)
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("select actualstorage from stock where storeid=@storeid and productid=@proid", new SqlParameter[2] { new SqlParameter("@storeid", storeid), new SqlParameter("@proid", proid) }, CommandType.Text));
        }

        /// <summary>
        /// 修改店铺库存之修改发货数量
        /// </summary>
        /// <param name="tr">事务对象</param>
        /// <param name="storeId">订单号</param>
        /// <param name="productId">商品ID</param>
        /// <returns>是否修改</returns>
        public static Boolean UpdStockSendTotalNumber(SqlTransaction tr, string storeId, int productId, int num)
        {
            string sql = "";
            //判断店铺是否有该商品没有添加店铺库存记录(未写)
            //if (DBHelper.ExecuteScalar(tr, "select count(id) from Stock where storeid='" + storeId + "' and productid='" + productId + "'", CommandType.Text).ToString() == "0")
            //{
            //    sql = "insert into Stock(StoreID,productID,TotalIn,TotalOut,ActualStorage,TurnStorage,HasOrderCount,InWayCount,LackTotalNumber,SendTotalNumber) values('" + storeId + "','" + productId + "',0,0,0,0,0," + num + ",0," + num + ") ";
            //}
            //else
            //{
            //    sql = "update Stock set  SendTotalNumber=SendTotalNumber+" + num + ",InWayCount=InWayCount+" + num + " where StoreId='" + storeId + "' and  ProductID=" + productId;
            //}
            
            String sqlStr="select count(id) from Stock where storeid=@num and productid=@num1";
            SqlParameter[] spa1 = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.NVarChar, 50), 
                new SqlParameter("@num1", SqlDbType.Int)
                
              };
            spa1[0].Value = storeId;
            spa1[1].Value = productId;
            if (DBHelper.ExecuteScalar(tr, sqlStr,spa1, CommandType.Text).ToString() == "0")
            {
                sql = "insert into Stock(StoreID,productID,TotalIn,TotalOut,ActualStorage,TurnStorage,HasOrderCount,InWayCount,LackTotalNumber,SendTotalNumber) values(@num,@num1,0,0,0,0,0,0,0,@num2) ";
                SqlParameter[] spa = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.NVarChar, 50), 
                new SqlParameter("@num1", SqlDbType.Int),
                new SqlParameter("@num2", SqlDbType.Int)
              };
                spa[0].Value = storeId;
                spa[1].Value = productId;
                spa[2].Value = num;
                return DBHelper.ExecuteNonQuery(tr, sql,spa,CommandType.Text) > 0;
            }
            else
            {
                sql = "update Stock set  SendTotalNumber=SendTotalNumber+@num where StoreId=@num1 and  ProductID=@num2";
                SqlParameter[] spa = new SqlParameter[] { 
                new SqlParameter("@num",SqlDbType.Int), 
                new SqlParameter("@num1", SqlDbType.NVarChar, 50),
                new SqlParameter("@num2", SqlDbType.Int)
              };
                spa[0].Value = num;
                spa[1].Value = storeId;
                spa[2].Value = productId;
                return DBHelper.ExecuteNonQuery(tr, sql, spa, CommandType.Text) > 0;
            }

            
        }

        /// <summary>
        /// 查詢出公司的實際庫存
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="productid"></param>
        /// <returns></returns>
        public DataTable GetAllTityByStoreidAndProductid(string storeid, int productid)
        {
            string sql = "select isNull((-(ActualStorage+HasOrderCount)),0) as Quantity from Stock where storeid=@num and  ProductID=@num1 and (ActualStorage+HasOrderCount)<0 ";
            SqlParameter[] spa = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.NVarChar, 50), 
                new SqlParameter("@num1", SqlDbType.Int)
                
              };
            spa[0].Value = storeid;
            spa[1].Value = productid;
            return DBHelper.ExecuteDataTable(sql,spa,CommandType.Text);
        }

        /// <summary>
        /// 修改店铺库存之修改预定数量
        /// </summary>
        /// <param name="tr">事务对象</param>
        /// <param name="storeId">订单号</param>
        /// <param name="productId">商品ID</param>
        /// <returns>是否修改</returns>
        public static Boolean UpdStockHasOrderCount(SqlTransaction tr, string storeId, int productId, int num)
        {
            string sql = "";
            String sqlStr = "select count(id) from Stock where storeid=@num and productid=@num1";
            SqlParameter[] spa1 = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.NVarChar, 50), 
                new SqlParameter("@num1", SqlDbType.Int)
                
              };
            spa1[0].Value = storeId;
            spa1[1].Value = productId;
            //判断店铺是否有该商品没有添加店铺库存记录(未写)
            if (DBHelper.ExecuteScalar(tr, sqlStr,spa1,CommandType.Text).ToString() == "0")
            {
                sql = "insert into Stock(StoreID,productID,TotalIn,TotalOut,ActualStorage,TurnStorage,HasOrderCount,InWayCount) values(@num,@num1,0,0,0,0,@num2,0) ";
                SqlParameter[] spa = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.NVarChar, 50), 
                new SqlParameter("@num1", SqlDbType.Int),
                new SqlParameter("@num2", SqlDbType.Int)
              };
                spa[0].Value = storeId;
                spa[1].Value = productId;
                spa[2].Value = num;
                return DBHelper.ExecuteNonQuery(tr, sql, spa, CommandType.Text) > 0;
            }
            else
            {
                sql = "update Stock set  HasOrderCount=HasOrderCount+@num  where StoreId=@num1 and  ProductID=@num2";
                SqlParameter[] spa = new SqlParameter[] { 
                new SqlParameter("@num",SqlDbType.Int), 
                new SqlParameter("@num1", SqlDbType.NVarChar, 50),
                new SqlParameter("@num2", SqlDbType.Int)
              };
                spa[0].Value = num;
                spa[1].Value = storeId;
                spa[2].Value = productId;
                return DBHelper.ExecuteNonQuery(tr, sql, spa, CommandType.Text) > 0;
            }

            
        }

        /// <summary>
        /// 修改店铺库存之在途数量
        /// </summary> 
        /// <param name="tr">事务对象</param>
        /// <param name="storeId">订单号</param>
        /// <param name="productId">商品ID</param>
        /// <returns>是否修改</returns>
        public Boolean UpdStockInWayCount(SqlTransaction tr, string storeId, int productId)
        {
            //判断店铺是否有该商品没有添加店铺库存记录(未写)
            string sql = "update Stock set InWayCount=(select sum(Quantity) from view_OrderDetail where IsCheckOut='Y' and IsReceived='N' and StoreID=@num)where StoreId=@num1 and  ProductID=@num2";
            SqlParameter[] spa = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.NVarChar, 50), 
                new SqlParameter("@num1", SqlDbType.NVarChar, 50),
                new SqlParameter("@num2", SqlDbType.Int)
              };
            spa[0].Value = storeId;
            spa[1].Value = storeId;
            spa[2].Value = productId;
            int num = DBHelper.ExecuteNonQuery(tr, sql,spa,CommandType.Text);
            return num > 0;
        }

        /// <summary>
        /// 修改实际库存数量
        /// </summary>
        /// <param name="tr">事务对象</param>
        /// <param name="storeId">订单号</param>
        /// <param name="productId">商品ID</param>
        /// <returns>是否修改</returns>
        public Boolean UpdStockActualStorage(SqlTransaction tr, string storeId, int productId)
        {
            string sql = "update Stock set ActualStorage=TotalIn-TotalOut where StoreID=@num and  ProductID=@num1";
            SqlParameter[] spa1 = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.NVarChar, 50), 
                new SqlParameter("@num1", SqlDbType.Int)
                
              };
            spa1[0].Value = storeId;
            spa1[1].Value = productId;
            return DBHelper.ExecuteNonQuery(tr, sql,spa1,CommandType.Text) > 0;
        }

        /// <summary>
        /// 修改总出与总入
        /// </summary>
        /// <param name="tr">事务对象</param>
        /// <param name="storeId">订单号</param>
        /// <param name="productId">商品ID</param>
        /// <param name="columus">是总出还是总入列名</param>
        /// <param name="totalCount">添加的数量</param>
        /// <returns></returns>
        public Boolean UpdStockTotalInOrOut(SqlTransaction tr, string storeId, int productId, string columus, int totalCount)
        {
            string sql = string.Format("update Stock set {0}=({1}+totalCount) where StoreID='{2}' and ProductID='{3}'", columus, columus, storeId, totalCount);
            if (DBHelper.ExecuteNonQuery(tr, sql) > 0)
            {
                return UpdStockActualStorage(tr, storeId, productId);
            }
            return false;
        }

        /// <summary>
        ///根据id查询店铺详细信息列表
        public List<StoreInfoModel> GetStoreInfoListById(int id)
        {
            //select * from StoreInfo where Id=@id
            SqlParameter[] param = new SqlParameter[] { 
             new SqlParameter("@storeid",id)
            };
            List<StoreInfoModel> storeList = new List<StoreInfoModel>();
            StoreInfoModel store = null;
            SqlDataReader reader = DBHelper.ExecuteReader("select s.*,b.BankName from storeinfo s,memberbank b where s.storeid=@storeid and s.BankCode=b.BankCode ", param, CommandType.StoredProcedure);

            while (reader.Read())
            {
                store = new StoreInfoModel();
                store.StoreID = reader["StoreID"].ToString();
                store.BankCode = reader["Bank"].ToString();
                //store.City = reader["City"].ToString();
                // store.Country = reader["Country"].ToString();
                //store.StoreCountry = reader["StoreCountry"].ToString();
                // store.StoreCity = reader["StoreCity"].ToString();
                store.Email = reader["Email"].ToString();
                store.ExpectNum = Convert.ToInt32(reader["ExpectNum"].ToString());
                store.FareArea = Convert.ToDecimal(reader["FareArea"]);
                store.FaxTele = reader["FaxTele"].ToString();
                store.HomeTele = reader["HomeTele"].ToString();
                //store.InwayCount = reader["InwayCount"].ToString();
                store.Language = Convert.ToInt32(reader["Language"]);
                store.MobileTele = reader["MobileTele"].ToString();
                store.Name = reader["Name"].ToString();
                store.NetAddress = reader["NetAddress"].ToString();
                store.Number = reader["Number"].ToString();
                store.OfficeTele = reader["OfficeTele"].ToString();
                store.PermissionMan = reader["PermissionMan"].ToString();
                store.PostalCode = reader["PostalCode"].ToString();
                //store.Province = reader["Province"].ToString();
                store.Direct = reader["Direct"].ToString();
                store.RegisterDate = Convert.ToDateTime(reader["RegisterDate"]);
                store.Remark = reader["Remark"].ToString();
                store.StoreAddress = reader["StoreAddress"].ToString();
                //store.StoreLevelStr = reader["StoreLevelStr"].ToString();
                store.StoreLevelInt = Convert.ToInt32(reader["StoreLevelInt"].ToString());
                store.StoreName = reader["StoreName"].ToString();
                store.BankCard = reader["BankCard"].ToString();
                store.TotalaccountMoney = Convert.ToDecimal(reader["TotalaccountMoney"]);
                store.TotalordergoodMoney = Convert.ToDecimal(reader["TotalordergoodMoney"]);
                storeList.Add(store);
            }
            reader.Close();
            return storeList;
        }

        /// <summary>
        /// 修改店铺库存的总入和总出
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <param name="storeOrderid">店铺订单编号</param>
        /// <returns></returns>
        public int UpdateStockByStoreidAndStoreOrderid(string storeid, string storeOrderid)
        {
            string sql = @"Update Stock Set TotalIn=TotalIn +(Select Quantity From OrderDetail where StoreOrderID=@StoreOrderID  And ProductId=Stock.ProductID),InWayCount=InWayCount -(Select Quantity From OrderDetail where StoreOrderID=@StoreOrderID  And ProductId=Stock.ProductID) Where StoreID =@StoreID   And  ProductID IN ( Select ProductID  From OrderDetail where StoreOrderID=@StoreOrderID)";
            SqlParameter[] para ={
														 new SqlParameter ("@StoreID" ,SqlDbType.VarChar ,15),
														 new SqlParameter ("@StoreOrderID" , SqlDbType .VarChar ,15)
													 };
            para[0].Value = storeid;
            para[1].Value = storeOrderid;
            return DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
        }

        /// <summary>
        /// 增加退货单时需要显示指定店铺库存情况
        /// cyb
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static DataTable GetStrockSByStoreid(string storeid)
        {
            string sql = @"Select P.productID,p.productcode,  P.productName ,P.bigProductUnitID ,(K.TotalIn-K.TotalOut) as MaxCount,
						(Select ProductUnitName From ProductUnit Where ProductUnitID = P.bigProductUnitID) as BigUnitName ,
						P.smallProductUnitID,
						(Select ProductUnitName From ProductUnit Where ProductUnitID = P.smallProductUnitID) as SmallUnitName ,
						P.BigSmallMultiPle ,
						PreferentialPrice ,PreferentialPV From  Stock as K ,Product as P 
						Where K.ProductID=P.ProductID And  (K.TotalIn-K.TotalOut)>0 And  P.isFold=0 And K.StoreId=@num";
            SqlParameter[] spa = new SqlParameter[]{new SqlParameter("@num",SqlDbType.NVarChar,50)};
            spa[0].Value = storeid;
            return DBHelper.ExecuteDataTable(sql,spa,CommandType.Text);
        }
        /// <summary>
        /// 根据产品编号，得到此产品的剩余可用数量
        /// </summary>
        /// <param name="productID">产品编号</param>
        /// <param name="productID">店铺编号</param>
        /// <returns></returns>
        public static int GetCertainProductLeftStoreCount(string productID, string storeId)
        {
            String sql = "Select (TotalIn-TotalOut)  From Stock Where StoreID = @num And ProductID =@num1";
            int count = 0;
            SqlParameter[] spa1 = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.NVarChar, 50), 
                new SqlParameter("@num1", SqlDbType.Int)
                
              };
            spa1[0].Value = storeId;
            spa1[1].Value = productID;
            SqlDataReader reader = DBHelper.ExecuteReader(sql,spa1,CommandType.Text);
            while (reader.Read())
                count = Convert.ToInt32(reader[0]);
            reader.Close();
            return count;
        }

        /// <summary>
        /// 删除在线订货订单
        /// </summary>
        /// <param name="orderid"> 订单号 </param>
        /// <param name="tr">事务</param>
        /// <returns> 影响记录行数 </returns>
        public static int DelStoreOrder(SqlTransaction tr, string orderid)
        {
            int x = 0;
            string sql = "select ProductID,Quantity,StoreID from OrderDetail where storeorderid=@num";
            SqlParameter[] spa1 = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.VarChar, 50)                             
              };
            spa1[0].Value = orderid;
            SqlDataReader reader = DBHelper.ExecuteReader(sql,spa1,CommandType.Text);
            string sqlstr = "update Stock set HasOrderCount=HasOrderCount-@num where storeid=@num1 and productid=@num2";
            SqlParameter[] spa = new SqlParameter[] { 
                new SqlParameter("@num",SqlDbType.Int), 
                new SqlParameter("@num1", SqlDbType.NVarChar, 50),
                new SqlParameter("@num2", SqlDbType.Int)
              };
            while (reader.Read())
            {
                
                spa[0].Value = reader.GetInt32(1);
                spa[1].Value = reader.GetString(2);
                spa[2].Value = reader.GetInt32(0);
                x = DBHelper.ExecuteNonQuery(tr,sqlstr,spa,CommandType.Text);
            }
            reader.Close();
            return x;
        }

        /// <summary>
        /// 删除在线订货订单
        /// </summary>
        /// <param name="orderid"> 订单号 </param>
        /// <param name="tr">事务</param>
        /// <returns> 影响记录行数 </returns>
        public static int DelStoreOrder(SqlTransaction tr, IList<OrderDetailModel> ods,string storeid)
        {
            int x = 0;
            string sqlstr = "update Stock set HasOrderCount=HasOrderCount-@num where storeid=@num1 and productid=@num2";
            SqlParameter[] spa = new SqlParameter[] { 
                new SqlParameter("@num",SqlDbType.Int), 
                new SqlParameter("@num1", SqlDbType.NVarChar, 50),
                new SqlParameter("@num2", SqlDbType.Int)
              };
            foreach(OrderDetailModel od in ods)
            {

                spa[0].Value = od.Quantity;
                spa[1].Value = storeid;
                spa[2].Value = od.ProductId;
                x = DBHelper.ExecuteNonQuery(tr,sqlstr,spa,CommandType.Text);
            }
            return x;
        }

        /// <summary>
        /// 删除发货申请单
        /// </summary>
        /// <param name="orderid"> 订单号 </param>
        /// <param name="tr">事务</param>
        /// <returns> 影响记录行数 </returns>
        public static int DelGoodsOrder(SqlTransaction tr, IList<OrderDetailModel> ods, string storeid)
        {
            int x = 0;
            string sqlstr = "update Stock set SendTotalNumber=SendTotalNumber-@num,InWayCount=InWayCount-@num1 where storeid=@num2 and productid=@num3";
            SqlParameter[] spa = new SqlParameter[] { 
                new SqlParameter("@num",SqlDbType.Int),
                new SqlParameter("@num1",SqlDbType.Int),
                new SqlParameter("@num2", SqlDbType.NVarChar, 50),
                new SqlParameter("@num3", SqlDbType.Int)
              };
            foreach (OrderDetailModel od in ods)
            {
                spa[0].Value = od.Quantity;
                spa[1].Value = od.Quantity;
                spa[2].Value = storeid;
                spa[3].Value = od.ProductId;
                x = DBHelper.ExecuteNonQuery(tr,sqlstr,spa,CommandType.Text);
            }
            return x;
        }

        /// <summary>
        /// 订单支付，把预定数量改成在途数量
        /// </summary>
        /// <param name="tr">事务</param>
        /// <param name="orderid">订单编号</param>
        /// <param name="storeid">店铺编号</param>
        /// <returns>是否更新成功</returns>
        public static bool UpdateInWayCount(SqlTransaction tr, IList<OrderDetailModel> orderdetails, string storeid)
        {
            bool state = true;

            if (DBHelper.ExecuteScalar(tr, "select ordertype from OrderGoods where storeorderid=@orderid", new SqlParameter[1] { new SqlParameter("@orderid", orderdetails[0].StoreorderId) }, CommandType.Text).ToString() == "0")
            {
                foreach (OrderDetailModel orderdetail in orderdetails)
                {
                    string sql = "update Stock set ActualStorage=ActualStorage+@count , HasOrderCount=HasOrderCount-@count,InWayCount=InWayCount+@count,Lacktotalnumber=Lacktotalnumber+@count where storeid=@storeid and ProductID=@productid";//,InWayCount=InWayCount+@count
                    SqlParameter[] paras = new SqlParameter[3];
                    paras[0] = new SqlParameter("@count", orderdetail.Quantity);
                    paras[1] = new SqlParameter("@storeid", storeid);
                    paras[2] = new SqlParameter("@productid", orderdetail.ProductId);

                    if (DBHelper.ExecuteNonQuery(tr, sql, paras, CommandType.Text) != 1)
                    {
                        state = false;
                    }
                }
            }
            else
            {
                foreach (OrderDetailModel orderdetail in orderdetails)
                {
                    string sql = "update Stock set HasOrderCount=HasOrderCount-@count,turnstorage=turnstorage+@count,Lacktotalnumber=Lacktotalnumber+@count where storeid=@storeid and ProductID=@productid";//,InWayCount=InWayCount+@count
                    SqlParameter[] paras = new SqlParameter[3];
                    paras[0] = new SqlParameter("@count", orderdetail.Quantity);
                    paras[1] = new SqlParameter("@storeid", storeid);
                    paras[2] = new SqlParameter("@productid", orderdetail.ProductId);

                    if (DBHelper.ExecuteNonQuery(tr, sql, paras, CommandType.Text) != 1)
                    {
                        state = false;
                    }
                }
            }
            return state;
        }

        /// <summary>
        /// 拆分产品信息--订单信息
        /// 注：把组合产品拆分成单品
        /// </summary>
        /// <param name="ods">原产品信息</param>
        /// <returns>拆分后产品信息</returns>
        public static IList<OrderDetailModel> GetNewOrderDetail(IList<OrderDetailModel> ods)
        {
            IList<OrderDetailModel> orderdetails = new List<OrderDetailModel>();
            foreach (OrderDetailModel od in ods)
            {
                if (ProductDAL.GetIsCombine(od.ProductId))
                {
                    IList<ProductCombineDetailModel> comDetails = ProductCombineDetailDAL.GetCombineDetil(od.ProductId);
                    foreach (ProductCombineDetailModel comDetail in comDetails)
                    {
                        int count = 0;
                        foreach (OrderDetailModel detail in orderdetails)
                        {
                            if (detail.ProductId == comDetail.SubProductID)
                            {
                                detail.Quantity = (comDetail.Quantity * od.Quantity) + detail.Quantity;
                                count++;
                            }
                        }
                        if (count == 0)
                        {
                            OrderDetailModel orderdetail = new OrderDetailModel();
                            orderdetail.Quantity = comDetail.Quantity * od.Quantity;
                            orderdetail.ProductId = comDetail.SubProductID;
                            orderdetails.Add(orderdetail);
                        }
                    }
                }
                else
                {
                    int count = 0;
                    foreach (OrderDetailModel detail in orderdetails)
                    {
                        if (detail.ProductId == od.ProductId)
                        {
                            detail.Quantity = od.Quantity + detail.Quantity;
                            count++;
                        }
                    }
                    if (count == 0)
                    {
                        OrderDetailModel orderdetail = new OrderDetailModel();
                        orderdetail.Quantity = od.Quantity;
                        orderdetail.ProductId = od.ProductId;
                        orderdetails.Add(orderdetail);
                    }

                }
            }
            return orderdetails;
        }

        /// <summary>
        /// 更新入库数量
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="orderid">订单编号</param>
        /// <param name="storeid">店铺编号</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateTotalIn(SqlTransaction tr, string orderid, string storeid)
        {
            bool state = true;
            //IList<OrderDetailModel> orderdetails = /*GetNewOrderDetail*/(OrderDetailDAL.GetOrderDetail(orderid));
            DataTable dt = InventoryDocDetailsDAL.GetProductIdAndQuantityByDocId(orderid);
            foreach (DataRow dr in dt.Rows)
            {
                string sql = "update Stock set TotalIn=TotalIn+@count,InWayCount=InWayCount-@count where storeid=@storeid and ProductID=@productid";
                SqlParameter[] paras = new SqlParameter[3];
                paras[0] = new SqlParameter("@count", dr["productQuantity"]);
                paras[1] = new SqlParameter("@storeid", storeid);
                paras[2] = new SqlParameter("@productid", dr["productid"]);

                if (DBHelper.ExecuteNonQuery(tr, sql, paras, CommandType.Text) != 1)
                {
                    state = false;
                }
            }

            return state;
        }

        /// <summary>
        /// 更新收转货数量
        /// </summary>
        /// <param name="tr">事务</param>
        /// <param name="orderid">订单编号</param>
        /// <param name="storeid">店铺编号</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateTurnStorage(SqlTransaction tr, string orderid, string storeid)
        {
            bool state = true;

            DataTable dt = InventoryDocDetailsDAL.GetProductIdAndQuantityByDocId(orderid);
            //IList<OrderDetailModel> orderdetails = OrderDetailDAL.GetOrderDetail(orderid);
            foreach (DataRow dr in dt.Rows)
            {
                string sql = "update Stock set TurnStorage=TurnStorage+@count,InWayCount=InWayCount-@count where storeid=@storeid and ProductID=@productid";
                SqlParameter[] paras = new SqlParameter[3];
                paras[0] = new SqlParameter("@count", dr["productQuantity"]);
                paras[1] = new SqlParameter("@storeid", storeid);
                paras[2] = new SqlParameter("@productid", dr["productid"]);

                if (DBHelper.ExecuteNonQuery(tr, sql, paras, CommandType.Text) != 1)
                {
                    state = false;
                }
            }

            return state;
        }

        /// <summary>
        /// Get the ProductId count of Stock by productId then judge the productId whether exist
        /// </summary>
        /// <param name="productId">ProductId</param>
        /// <returns>Return the ProductId count of Stock by productId</returns>
        public static int ProductIdIsExist_Stock(int productId)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productId",SqlDbType.Int)
            };
            sparams[0].Value = productId;
            return Convert.ToInt32(DBHelper.ExecuteScalar("ProductIdIsExist_Stock", sparams, CommandType.StoredProcedure));
        }

        /// <summary>
        /// Get the minimun ActualStorage by ProductId the judge the Product whether lack
        /// </summary>
        /// <param name="productId">ProductId</param>
        /// <returns>Return the minimun ActualStorage by ProductId</returns>
        public static int ProductIdIsLack_Stock(int productId)
        {
            SqlParameter[] sparams = new SqlParameter[]
            {
                new SqlParameter("@productId",SqlDbType.Int)
            };
            sparams[0].Value = productId;
            return Convert.ToInt32(DBHelper.ExecuteScalar("ProductIdIsLack_Stock", sparams, CommandType.StoredProcedure));
        }
    }
}
