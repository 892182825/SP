using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Model;

namespace DAL
{
    /// <summary>
    /// 店铺订单明细数据访问层
    /// </summary>
    public class OrderDetailDAL
    {

        /// <summary>
        /// 插入订单明细表数据
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="orderDetailModel"></param>
        /// <returns></returns>
        public static Boolean AddOrderGoodsDetail(SqlTransaction tr, OrderDetailModel orderDetailModel, string StoreOrderID)
        {
            string sql = "insert into OrderGoodsDetail(OrderGoodsID,StoreID,ProductID,ExpectNum,Quantity,Price,Pv)" +
                "values(@StoreOrderID,@StoreID,@ProductID,@ExpectNum,@Quantity,@Price,@Pv)";
            SqlParameter[] ps = new SqlParameter[]
            {
                new SqlParameter("@StoreOrderID",StoreOrderID),                       //订单号
                new SqlParameter("@StoreID",orderDetailModel.StoreId),                //店铺编号
                new SqlParameter("@ProductID",orderDetailModel.ProductId),            //产品ID
                new SqlParameter("@ExpectNum",orderDetailModel.ExpectNum),            //期数
                new SqlParameter("@Quantity",orderDetailModel.Quantity),              //原来订货数量
                new SqlParameter("@Price",orderDetailModel.Price),                    //产品单价
                new SqlParameter("@Pv",orderDetailModel.Pv)                           //产品积分
            };
            return DBHelper.ExecuteNonQuery(tr, sql, ps, CommandType.Text) > 0;
        }

        /// <summary>
        /// 插入订单明细表数据
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="orderDetailModel"></param>
        /// <returns></returns>
        public static Boolean AddOrderDetail(SqlTransaction tr, OrderDetailModel orderDetailModel, string StoreOrderID)
        {
            string sql = "insert into OrderDetail(StoreOrderID,StoreID,ProductID,ExpectNum,Quantity,Price,Pv)" +
                "values(@StoreOrderID,@StoreID,@ProductID,@ExpectNum,@Quantity,@Price,@Pv)";
            SqlParameter[] ps = new SqlParameter[]
            {
                new SqlParameter("@StoreOrderID",StoreOrderID),                       //订单号
                new SqlParameter("@StoreID",orderDetailModel.StoreId),                //店铺编号
                new SqlParameter("@ProductID",orderDetailModel.ProductId),            //产品ID
                new SqlParameter("@ExpectNum",orderDetailModel.ExpectNum),            //期数
                new SqlParameter("@Quantity",orderDetailModel.Quantity),              //原来订货数量
                new SqlParameter("@Price",orderDetailModel.Price),                    //产品单价
                new SqlParameter("@Pv",orderDetailModel.Pv)                           //产品积分
            };
            return DBHelper.ExecuteNonQuery(tr, sql, ps, CommandType.Text) > 0;
        }

        /// <summary>
        /// 返回对应订单明细(店铺子系统的订单支付)
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public static IList<OrderDetailModel> GetOrderDetailByStoreId(string ID)
        {
            IList<OrderDetailModel> orderDetailList = new List<OrderDetailModel>();
            string storedProcedure = "GetOrderDetailByStoreId";
            SqlDataReader dr = DBHelper.ExecuteReader(storedProcedure, new SqlParameter("@ID", ID), CommandType.StoredProcedure);
            while (dr.Read())
            {
                OrderDetailModel tempItem = new OrderDetailModel();
                tempItem.StoreorderId = dr.GetString(0);      //订单号
                tempItem.StoreId = dr.GetString(1);           //店铺编号
                tempItem.ProductName = dr.GetString(2);       //商品名称
                tempItem.ProductTypeName = dr.GetString(3);   //产品型号名称
                tempItem.Quantity = dr.GetInt32(4);           //原来订货数量
                tempItem.Price = dr.GetDecimal(5);            //产品单价
                tempItem.Pv = dr.GetDecimal(6);               //总积分
                tempItem.ProductId = dr.GetInt32(7);          //产品ID
                orderDetailList.Add(tempItem);
            }
            dr.Close();
            return orderDetailList;
        }



        public Boolean UpdStoreOrderItem(OrderDetailModel updItem)
        {

            return true;
        }



        public static Boolean DelOrderDetailItem(SqlTransaction tr, string storeOrderID)
        {
            string sqlstr = "delete from orderdetail where StoreOrderID=@num";
            SqlParameter[] parm = new SqlParameter[] { 
              new SqlParameter("@num",SqlDbType.VarChar,50)
            
            };
            parm[0].Value = storeOrderID;
            return DBHelper.ExecuteNonQuery(tr, sqlstr, parm, CommandType.Text) > 0;
            //return true;
        }

        public static Boolean DelOrderGoodsDetail(SqlTransaction tr, string storeOrderID)
        {
            string sqlstr = "delete from OrderGoodsDetail where StoreOrderID=@num";
            SqlParameter[] parm = new SqlParameter[] { 
              new SqlParameter("@num",SqlDbType.VarChar,50)
            
            };
            parm[0].Value = storeOrderID;
            return DBHelper.ExecuteNonQuery(tr, sqlstr, parm, CommandType.Text) > 0;

            //return true;
        }

        /// <summary>
        /// 获取订单明细信息
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="storeId">点编号</param>
        /// <returns>返回订单明细</returns>
        public static IList<OrderDetailModel> GetDetail(string orderId, string storeId)
        {
            IList<OrderDetailModel> list = new List<OrderDetailModel>();
            string sql = "select o.Quantity,o.Price,o.Pv,p.ProductName from orderdetail as o,Product as p where o.productid=p.productid and storeorderid=@orderid and storeid=@storeid ";
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@orderid", orderId);
            paras[1] = new SqlParameter("@storeid", storeId);
            SqlDataReader reader = DBHelper.ExecuteReader(sql, paras, CommandType.Text);
            while (reader.Read())
            {
                OrderDetailModel od = new OrderDetailModel();
                od.Quantity = reader.GetInt32(0);
                od.Price = reader.GetDecimal(1);
                od.Pv = reader.GetDecimal(2);
                od.ProductName = reader.GetString(3);
                list.Add(od);
            }

            reader.Close();

            return list;
        }

        /// <summary>
        /// 获取订单明细信息
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <param name="storeId">点编号</param>
        /// <returns>返回订单明细</returns>
        public static IList<OrderDetailModel> GetDetails(string orderId, string storeId)
        {
            IList<OrderDetailModel> list = new List<OrderDetailModel>();
            string sql = "select o.Quantity,o.Price,o.Pv,p.ProductName from ordergoodsdetail as o,Product as p where o.productid=p.productid and ordergoodsId=@orderid and storeid=@storeid ";
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@orderid", orderId);
            paras[1] = new SqlParameter("@storeid", storeId);
            SqlDataReader reader = DBHelper.ExecuteReader(sql, paras, CommandType.Text);
            while (reader.Read())
            {
                OrderDetailModel od = new OrderDetailModel();
                od.Quantity = reader.GetInt32(0);
                od.Price = reader.GetDecimal(1);
                od.Pv = reader.GetDecimal(2);
                od.ProductName = reader.GetString(3);
                list.Add(od);
            }

            reader.Close();

            return list;
        }

        /// <summary>
        /// 获取订单明细   （产品ID，产品数量） 绑定树
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>产品ID，产品数量</returns>
        public static IList<OrderDetailModel> GetOrderDetail(string orderId)
        {
            IList<OrderDetailModel> list = new List<OrderDetailModel>();
            string sql = "select Quantity,ProductId from orderdetail  where storeorderid=@orderid ";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@orderid", orderId);
            SqlDataReader reader = DBHelper.ExecuteReader(sql, paras, CommandType.Text);
            while (reader.Read())
            {
                OrderDetailModel od = new OrderDetailModel();
                od.Quantity = reader.GetInt32(0);
                od.ProductId = reader.GetInt32(1);
                list.Add(od);
            }

            reader.Close();

            return list;
        }

        /// <summary>
        /// 获取订单明细   （产品ID，产品数量） 绑定树
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>产品ID，产品数量</returns>
        public static IList<OrderDetailModel> GetOrderGoodsDetail(string orderId)
        {
            IList<OrderDetailModel> list = new List<OrderDetailModel>();
            string sql = "select Quantity,ProductId from OrderGoodsDetail where OrderGoodsid=@orderid ";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@orderid", orderId);
            SqlDataReader reader = DBHelper.ExecuteReader(sql, paras, CommandType.Text);
            while (reader.Read())
            {
                OrderDetailModel od = new OrderDetailModel();
                od.StoreorderId = orderId;
                od.Quantity = reader.GetInt32(0);
                od.ProductId = reader.GetInt32(1);
                list.Add(od);
            }

            reader.Close();

            return list;
        }


        public static DataTable GetOrderView(string StoreOrderID)
        {
            string sql = @"select StoreOrder.StoreOrderID, StoreOrder.OrderDateTime, StoreOrder.ExpectNum, StoreOrder.TotalMoney, StoreOrder.TotalPv, StoreOrder.IsCheckOut,city.country,city.province,city.city, 
                      StoreOrder.IsSent, StoreOrder.IsReceived, StoreInfo.Name as StoreName, (city.province+city.city+StoreOrder.InceptAddress) as InceptAddress , StoreOrder.Telephone, StoreOrder.Carriage, StoreOrder.Weight, 
                      StoreOrder.Description  from StoreInfo left outer JOIN StoreOrder ON StoreInfo.StoreID = StoreOrder.StoreID  left outer join city on StoreOrder.cpccode=city.cpccode where StoreOrderid=@StoreOrderID";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@StoreOrderID", StoreOrderID);
            return DBHelper.ExecuteDataTable(sql, paras, CommandType.Text);
        }

        public static DataTable GetOrderView1(string StoreOrderID,double Currency)
        {
            string sql = @"select StoreOrder.StoreOrderID, StoreOrder.OrderDateTime, StoreOrder.ExpectNum, (StoreOrder.TotalMoney*" + Currency + ") as TotalMoney, StoreOrder.TotalPv, StoreOrder.IsCheckOut,city.country,city.province,city.city, " +
                     " StoreOrder.IsSent, StoreOrder.IsReceived, StoreInfo.Name as StoreName, (city.province+city.city+StoreOrder.InceptAddress) as InceptAddress , StoreOrder.Telephone, StoreOrder.Carriage, StoreOrder.Weight, " +
                      " StoreOrder.Description  from StoreInfo left outer JOIN StoreOrder ON StoreInfo.StoreID = StoreOrder.StoreID  left outer join city on StoreOrder.cpccode=city.cpccode where StoreOrderid=@StoreOrderID";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@StoreOrderID", StoreOrderID);
            return DBHelper.ExecuteDataTable(sql, paras, CommandType.Text);
        }

        public static DataTable GetOrderViewT(string StoreOrderID)
        {
            string sql = @"select o.ID,StoreID,OrderType,paytype,ordergoodsid,TotalMoney,TotalPV,ExpectNum,(c.Province+c.City+InceptAddress) as InceptAddress, InceptPerson,PostalCode,Telephone,OrderDateTime,c.Country,c.Province,c.City,IsCheckOut,description,ordertype,paytype from OrderGoods o,City c where 1=1 and o.cpccode=c.cpccode and o.ordergoodsID=@StoreOrderID";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@StoreOrderID", StoreOrderID);
            return DBHelper.ExecuteDataTable(sql, paras, CommandType.Text);
        }
        public static DataTable GetOrderViewTNew(string StoreOrderID)
        {
            string sql = @"select o.ID,StoreID,OrderType,paytype,OrderGoodsID,TotalMoney,TotalPV,ExpectNum,(c.Province+c.City+c.xian+InceptAddress) as InceptAddress, InceptPerson,PostalCode,Telephone,OrderDateTime,c.Country,c.Province,c.City,IsCheckOut,description,ordertype,paytype from OrderGoods o,City c where 1=1 and o.cpccode=c.cpccode and o.OrderGoodsID in (" + StoreOrderID + ")";
            return DBHelper.ExecuteDataTable(sql, CommandType.Text);
        }

        public static DataTable GetOrderGoodY(string StoreOrderID)
        {
            string sql = "select OrderGoodsID,StoreID,isnull(TotalMoney,0) as TotalMoney,isnull(TotalPv,0) as TotalPv,isnull(TotalCommision,0) as TotalCommision,OrderType,OrderDateTime,PayType ";
            sql += " from OrderGoods where IsCheckOut='Y' and OrderGoodsID=@StoreOrderID";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@StoreOrderID", StoreOrderID);
            return DBHelper.ExecuteDataTable(sql, paras, CommandType.Text);
        }

        /// <summary>
        /// 查询订单详细--（订单支付）
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>订单明细</returns>
        public static DataTable GetOrderDetailTwo(string orderId)
        {
            string sql = " SELECT OrderDetail.StoreOrderID, OrderDetail.StoreID, Product.ProductName, ProductType.ProductTypeName, OrderDetail.Quantity, OrderDetail.Price,OrderDetail.Pv, isnull(Product.Width,0) as Weight ";
            sql += " FROM Product INNER JOIN   ProductType ON Product.ProductTypeID = ProductType.ProductTypeID CROSS JOIN  OrderDetail ";
            sql += " where OrderDetail.ProductId=Product.ProductId and OrderDetail.StoreOrderID=@orderId ";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@orderid", orderId);
            return DBHelper.ExecuteDataTable(sql, paras, CommandType.Text);
        }

        public static DataTable GetOrderDetailTwo1(string orderId,double Currency)
        {
            string sql = " SELECT OrderDetail.StoreOrderID, OrderDetail.StoreID, Product.ProductName, ProductType.ProductTypeName, OrderDetail.Quantity, OrderDetail.Price*"+ Currency + "as Price,OrderDetail.Pv, isnull(Product.Width,0) as Weight ";
            sql += " FROM Product INNER JOIN   ProductType ON Product.ProductTypeID = ProductType.ProductTypeID CROSS JOIN  OrderDetail ";
            sql += " where OrderDetail.ProductId=Product.ProductId and OrderDetail.StoreOrderID=@orderId ";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@orderid", orderId);
            return DBHelper.ExecuteDataTable(sql, paras, CommandType.Text);
        }

        /// <summary>
        /// 查询订单详细--（订单支付）
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>订单明细</returns>
        public static DataTable GetOrderGoodsDetails(string orderId)
        {
            string sql = " SELECT OrderDetail.OrderGoodsID, OrderDetail.StoreID, Product.ProductName, ProductType.ProductTypeName, OrderDetail.Quantity, OrderDetail.Price,OrderDetail.Pv, isnull(Product.Width,0) as Weight ";
            sql += " FROM Product INNER JOIN   ProductType ON Product.ProductTypeID = ProductType.ProductTypeID CROSS JOIN OrderGoodsDetail as OrderDetail ";
            sql += " where OrderDetail.ProductId=Product.ProductId and OrderDetail.OrderGoodsID=@orderId ";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@orderid", orderId);
            return DBHelper.ExecuteDataTable(sql, paras, CommandType.Text);
        }

        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public OrderDetailModel GetOrderDetailModel(string storeOrderID)
        {
            string cmd = "select ID, StoreOrderID, StoreID, ProductID, ExpectNum, Quantity, Price, Pv, Description from OrderDetail where StoreOrderID=@num";
            SqlParameter[] parm = new SqlParameter[] { 
              new SqlParameter("@num",SqlDbType.VarChar,50)
            
            };
            parm[0].Value = storeOrderID;
            SqlDataReader dr = DBHelper.ExecuteReader(cmd, parm, CommandType.Text);

            dr.Read();

            OrderDetailModel odm = new OrderDetailModel();

            odm.ID = Convert.ToInt32(dr["ID"]);
            odm.StoreorderId = dr["StoreOrderID"].ToString();
            odm.StoreId = dr["StoreID"].ToString();
            odm.ProductId = Convert.ToInt32(dr["ProductID"]);
            odm.ExpectNum = Convert.ToInt32(dr["ExpectNum"]);
            odm.Quantity = Convert.ToInt32(dr["Quantity"]);
            odm.Price = Convert.ToDecimal(dr["Price"]);
            odm.Pv = Convert.ToDecimal(dr["Pv"]);
            odm.Description = dr["Description"].ToString();

            dr.Close();

            return odm;

        }
        public static DataTable Getordergoodstablebyorderid(string orderid)
        {

            string sqlstr = "select top 1 storeid,ordergoodsid,totalmoney,totalpv, IsCheckOut from ordergoods where ordergoodsid=@ogid";
            SqlParameter[] sps = new SqlParameter[] { new SqlParameter("@ogid", orderid) };
            DataTable dt = DBHelper.ExecuteDataTable(sqlstr, sps, CommandType.Text);
            return dt;


        }




        public static DataTable Getordergoodstablebyorderid1(string orderid)
        {
            string sqlstr = "select * from MemberOrder where OrderID=@ogid";
            //string sqlstr = "select top 1 storeid,orderid,totalmoney,totalpv from memberorder where orderid=@ogid";
            SqlParameter[] sps = new SqlParameter[] { new SqlParameter("@ogid", orderid) };
            DataTable dt = DBHelper.ExecuteDataTable(sqlstr, sps, CommandType.Text);
            return dt;


        }

        public static DataTable Getordergoodstablebyorderid2(string orderid)
        {
            string sqlstr = "select * from OrderGoods where OrderGoodsID=@ogid";
            //string sqlstr = "select top 1 storeid,orderid,totalmoney,totalpv from memberorder where orderid=@ogid";
            SqlParameter[] sps = new SqlParameter[] { new SqlParameter("@ogid", orderid) };
            DataTable dt = DBHelper.ExecuteDataTable(sqlstr, sps, CommandType.Text);
            return dt;


        }
        /// <summary>
        /// 查询订单明细
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public static List<OrderDetailModel> GetGoodsDetailModelList(string storeOrderID)
        {
            string cmd = "select ID, ordergoodsID, StoreID, ProductID, ExpectNum, Quantity, Price, Pv, Description from ordergoodsdetail where ordergoodsID=@num";
            SqlParameter[] parm = new SqlParameter[] { 
              new SqlParameter("@num",SqlDbType.VarChar,50)
            
            };
            parm[0].Value = storeOrderID;
            SqlDataReader dr = DBHelper.ExecuteReader(cmd, parm, CommandType.Text);

            List<OrderDetailModel> l_OrderDetailModel = new List<OrderDetailModel>();

            while (dr.Read())
            {
                OrderDetailModel odm = new OrderDetailModel();

                odm.ID = Convert.ToInt32(dr["ID"]);
                odm.StoreorderId = dr["ordergoodsID"].ToString();
                odm.StoreId = dr["StoreID"].ToString();
                odm.ProductId = Convert.ToInt32(dr["ProductID"]);
                odm.ExpectNum = Convert.ToInt32(dr["ExpectNum"]);
                odm.Quantity = Convert.ToInt32(dr["Quantity"]);
                odm.Price = Convert.ToDecimal(dr["Price"]);
                odm.Pv = Convert.ToDecimal(dr["Pv"]);
                odm.Description = dr["Description"].ToString();

                l_OrderDetailModel.Add(odm);
            }

            dr.Close();

            return l_OrderDetailModel;
        }

        public List<OrderDetailModel> GetOrderDetailModelList(string storeOrderID)
        {
            string cmd = "select ID, StoreOrderID, StoreID, ProductID, ExpectNum, Quantity, Price, Pv, Description from OrderDetail where StoreOrderID=@num";
            SqlParameter[] parm = new SqlParameter[] { 
              new SqlParameter("@num",SqlDbType.VarChar,50)
            
            };
            parm[0].Value = storeOrderID;

            SqlDataReader dr = DBHelper.ExecuteReader(cmd, parm, CommandType.Text);

            List<OrderDetailModel> l_OrderDetailModel = new List<OrderDetailModel>();

            while (dr.Read())
            {
                OrderDetailModel odm = new OrderDetailModel();

                odm.ID = Convert.ToInt32(dr["ID"]);
                odm.StoreorderId = dr["StoreOrderID"].ToString();
                odm.StoreId = dr["StoreID"].ToString();
                odm.ProductId = Convert.ToInt32(dr["ProductID"]);
                odm.ExpectNum = Convert.ToInt32(dr["ExpectNum"]);
                odm.Quantity = Convert.ToInt32(dr["Quantity"]);
                odm.Price = Convert.ToDecimal(dr["Price"]);
                odm.Pv = Convert.ToDecimal(dr["Pv"]);
                odm.Description = dr["Description"].ToString();


                l_OrderDetailModel.Add(odm);
            }

            dr.Close();

            return l_OrderDetailModel;
        }

        /// <summary>
        /// 根据新的要货编号获得老订单号
        /// </summary>
        /// <param name="fahuoOrder">要货单号</param>
        /// <returns></returns>
        public static DataTable GetOrderGoodsNoteTable(string fahuoOrder)
        {
            string strSql = @"select StoreID,ordergoodsid,ExpectNum,IsCheckOut,fahuoorder,
                             OrderType,InceptPerson,city.country,city.province,city.city,
                            InceptAddress,PostalCode,TotalMoney,TotalPV,Telephone,Weight,Carriage
                            ,OrderDateTime,Description from ordergoods so  left outer join city on so.cpccode=city.cpccode 
                            where fahuoOrder = @fahuoOrder";
            SqlParameter[] parm = new SqlParameter[]
            {
                new SqlParameter("@fahuoOrder",fahuoOrder)
            };
            return DBHelper.ExecuteDataTable(strSql, parm, CommandType.Text);
        }
    }
}
