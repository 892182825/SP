using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using Model.Other;
using DAL.Other;

namespace DAL
{
    /// <summary>
    /// 店铺订单表对应的数据访问层
    /// </summary>
    public class StoreOrderDAL
    {
        /// <summary>
        /// 添加订货单信息
        /// </summary>
        /// <param name="item">订货单对象</param>
        /// <param name="tr">事务参数</param>
        /// <returns>是否插入成功</returns>
        public Boolean AddOrderGoods(OrderGoodsMedel item, SqlTransaction tr)
        {
            string sql = "insert into OrderGoods (StoreID,OrderGoodsID,TotalMoney,TotalPV,InceptAddress,InceptPerson,PostalCode,Telephone,OrderType,Description,OrderDateTime,PayMentDateTime,ExpectNum,TotalCommision,GoodsQuantity,Weight,CPCCode,IscheckOut,OperateIP,PayType,PayCurrency,PayMoney,SendWay)"
                        + "values(@StoreID,@OrderGoodsID,@TotalMoney,@TotalPV,@InceptAddress,@InceptPerson,@PostalCode,@Telephone,@OrderType,@Description,@OrderDateTime,@PayMentDateTime,@ExpectNum,@TotalCommision,@GoodsQuantity,@Weight,@CPCCode,@IscheckOut,@OperateIP,@PayType,@PayCurrency,@PayMoney,@SendWay)";
            SqlParameter[] ps = new SqlParameter[] 
            {
                new SqlParameter("@StoreID",item.StoreId),                                //店铺ID
                new SqlParameter("@OrderGoodsID",item.OrderGoodsID),                      //订单号
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
                new SqlParameter("@PayMentDateTime","1900-01-01"),
                new SqlParameter("@PayType",item.PayType),                                 //支付类型
                new SqlParameter("@PayCurrency",item.PayCurrency),
                new SqlParameter("@PayMoney",item.PayMoney),
                new SqlParameter("@SendWay",item.SendWay)

            };
            return DBHelper.ExecuteNonQuery(tr, sql, ps, CommandType.Text) > 0;
        }

        /// <summary>
        /// 获取会员到货订单（收货确认）
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <returns>订单列表</returns>
        public DataTable GetNumberReceivedOrder(string number, int sendType)
        {
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@Number", number);
            paras[1] = new SqlParameter("@sendType", sendType);
            return DBHelper.ExecuteDataTable("SELECT StoreOrderID, TotalMoney, TotalPv, InceptPerson, InceptAddress, PostalCode, Telephone, IsCheckOut, Carriage, Weight,membernumber,kuaididh FROM StoreOrder where membernumber=@Number and sendType=@sendType and IsSent='Y' and IsReceived='N' ", paras, CommandType.Text);
        }
        /// <summary>
        ///  获取店铺退货单数量
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns></returns>
        public static int GetStoreTHOrderCount(string storeid)
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("select count(0) from InventoryDoc where client=@storeid", new SqlParameter[1] { new SqlParameter("@storeid", storeid) }, CommandType.Text));
        }

        public string getStoreIdByOrderId(string orderId)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@orderid", orderId) };
            return DBHelper.ExecuteScalar("select storeid from storeorder  where storeorderid=@orderid", para, CommandType.Text).ToString();
        }
        /// <summary>
        /// 获取订单支付状态
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static Boolean GetIsCheckOut(string orderid)
        {
            string sql = "select ischeckout from OrderGoods where storeorderid=@orderid ";
            if (DBHelper.ExecuteScalar(sql, new SqlParameter[] { new SqlParameter("@orderid", orderid) }, CommandType.Text).ToString() == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取订单支付状态
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static Boolean GetIsCheckOut(SqlTransaction tran, string orderid)
        {
            string sql = "select ischeckout from OrderGoods where storeorderid=@orderid ";
            if (DBHelper.ExecuteScalar(tran, sql, new SqlParameter[] { new SqlParameter("@orderid", orderid) }, CommandType.Text).ToString() == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 根据订单号获取支付状态
        /// </summary>
        /// <param name="orderid">订单号</param>
        /// <returns></returns>
        public static bool GetOrderCheckState(string orderid)
        {
            return DBHelper.ExecuteScalar("select ischeckout from ordergoods where ordergoodsid=@orderid ", new SqlParameter[] { new SqlParameter("@orderid", orderid) }, CommandType.Text).ToString() == "Y";
        }

        /// <summary>
        /// 根据订单号，获取订单是否存在
        /// </summary>
        /// <param name="orderid">订单号</param>
        /// <returns></returns>
        public static bool GetOrderIsExist(string orderid)
        {
            return DBHelper.ExecuteScalar("select count(0) from ordergoods where ordergoodsid=@orderid ", new SqlParameter[] { new SqlParameter("@orderid", orderid) }, CommandType.Text).ToString() == "0";
        }

        /// <summary>
        /// 添加订货单信息
        /// </summary>
        /// <param name="item">订货单对象</param>
        /// <param name="tr">事务参数</param>
        /// <returns>是否插入成功</returns>
        public Boolean AddStoreOrder(StoreOrderModel item, SqlTransaction tr)
        {
            string sql = "insert into StoreOrder (StoreID,StoreOrderID,TotalMoney,TotalPV,InceptAddress,InceptPerson,PostalCode,Telephone,OrderType,Description,OrderDateTime,ConsignmentDateTime,ForeCastArriveDateTime,ExpectNum,TotalCommision,GoodsQuantity,Weight,CPCCode,IscheckOut,IsAuditing,OperateIP,sendway)"
                        + "values(@StoreID,@StoreOrderID,@TotalMoney,@TotalPV,@InceptAddress,@InceptPerson,@PostalCode,@Telephone,@OrderType,@Description,@OrderDateTime,@ConsignmentDateTime,@ForeCastArriveDateTime,@ExpectNum,@TotalCommision,@GoodsQuantity,@Weight,@CPCCode,@IscheckOut,@IsAuditing,@OperateIP,@sendway)";
            SqlParameter[] ps = new SqlParameter[] 
            {
                new SqlParameter("@StoreID",item.StoreId),                                //店铺ID
                new SqlParameter("@StoreOrderID",item.StoreorderId),                      //订单号
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
                new SqlParameter("@IsAuditing",item.IsAuditing),
                new SqlParameter("@OperateIP",item.OperateIP),                            //操作者IP
                new SqlParameter("@ConsignmentDateTime","1900-01-01"),
                new SqlParameter("@ForeCastArriveDateTime","1900-01-01"),                              //支付类型
                 new SqlParameter("@sendway",item.SendWay),                              //发货类型

            };
            return DBHelper.ExecuteNonQuery(tr, sql, ps, CommandType.Text) > 0;
        }

        /// <summary>
        ///  根据订单号获取店铺ID
        /// </summary>
        /// <param name="orderid">订单号</param>
        /// <returns>店铺编号</returns>
        public static string GetStoreIdByOrderId(string orderid)
        {
            return DBHelper.ExecuteScalar("select storeid from storeorder where storeorderid=@orderid ", new SqlParameter[] { new SqlParameter("@orderid", orderid) }, CommandType.Text).ToString();
        }

        /// <summary>
        ///  根据订单号获取店铺ID
        /// </summary>
        /// <param name="orderid">订单号</param>
        /// <returns>店铺编号</returns>
        public static string GetStoreByOrderId(string orderid)
        {
            return DBHelper.ExecuteScalar("select storeid from ordergoods where storeorderid=@orderid ", new SqlParameter[] { new SqlParameter("@orderid", orderid) }, CommandType.Text).ToString();
        }

        /// <summary>
        ///  根据订单号获取店铺ID
        /// </summary>
        /// <param name="orderid">订单号</param>
        /// <returns>店铺编号</returns>
        public static string GetStoreIdByGoodsId(string orderid)
        {
            return DBHelper.ExecuteScalar("select storeid from OrderGoods where storeorderid=@orderid ", new SqlParameter[] { new SqlParameter("@orderid", orderid) }, CommandType.Text).ToString();
        }

        ///// <summary>
        ///// 处理返回结果集合
        ///// </summary>
        ///// <param name="pagin">分页类</param>
        ///// <param name="tableName">表名</param>
        ///// <param name="key">排序键值</param>
        ///// <param name="comlums">列名</param>
        ///// <param name="condition">条件</param>
        ///// <returns>结果集</returns>
        //private static SqlDataReader DisSqlReader(PaginationModel pagin, string tableName, string key, string comlums, string condition)
        //{
        //    SqlParameter[] ps = new SqlParameter[] 
        //    {
        //        new SqlParameter("@tableName",tableName),
        //        new SqlParameter("@key",key),
        //        new SqlParameter("@comlums",comlums),
        //        new SqlParameter("@condition",condition),
        //        new SqlParameter("@start",pagin.GetPageDate()),
        //        new SqlParameter("@end",pagin.GetEndDate()),
        //        new SqlParameter("@DataCount",pagin.DataCount)
        //    };
        //    ps[6].Direction = ParameterDirection.Output;
        //    ps[6].Size = 10;
        //    SqlDataReader dr = DBHelper.ExecuteReader("GetDataPagePagination", ps, CommandType.StoredProcedure);
        //    return dr;
        //}


        /// <summary>
        /// 返回订单信息数据用户订单编辑店铺子系统（带分页功能）
        /// </summary>
        /// <param name="pagin">分页类</param>
        /// <param name="tableName">表名</param>
        /// <param name="key">键值</param>
        /// <param name="comlums">列名</param>
        /// <param name="condition">条件</param>
        /// <returns>返回对应数据</returns>
        public IList<StoreOrderModel> GetStoreOrderListEffectBrowse(PaginationModel pagin, string tableName, string key, string comlums, string condition)
        {//GetDataPagePagination
            IList<StoreOrderModel> storeOrderList = new List<StoreOrderModel>();
            SqlParameter[] ps = new SqlParameter[] 
            {
                new SqlParameter("@tableName",tableName),
                new SqlParameter("@key",key),
                new SqlParameter("@comlums",comlums),
                new SqlParameter("@condition",condition),
                new SqlParameter("@start",pagin.GetPageDate()),
                new SqlParameter("@end",pagin.GetEndDate()),
                new SqlParameter("@DataCount",pagin.DataCount)
            };
            ps[6].Direction = ParameterDirection.Output;
            ps[6].Size = 10;
            SqlDataReader dr = DBHelper.ExecuteReader("GetDataPagePagination", ps, CommandType.StoredProcedure);
            StoreOrderModel item;
            while (dr.Read())
            {
                item = new StoreOrderModel();
                item.ID = dr.GetInt32(0);
                item.StoreorderId = dr.GetString(1);
                item.TotalMoney = dr.GetDecimal(2);
                item.TotalPv = dr.GetDecimal(3);
                item.ExpectNum = dr.GetInt32(4);
                item.InceptAddress = dr.GetString(5);
                item.InceptPerson = dr.GetString(6);
                item.PostalCode = dr.GetString(7);
                item.Telephone = dr.GetString(8);
                item.OrderDatetime = dr.GetDateTime(9);
                // item.City.Country = dr.GetString(10);
                //item.City.Province = dr.GetString(11);
                // item.City.City = dr.GetString(12);
                storeOrderList.Add(item);
            }
            dr.NextResult();
            pagin.DataCount = Convert.ToInt32(ps[6].Value.ToString());
            dr.Close();
            return storeOrderList;
        }

        /// <summary>
        /// 返回订单信息数据用户订单跟踪店铺子系统（带分页功能）
        /// </summary>
        /// <param name="pagin">分页类</param>
        /// <param name="tableName">表名</param>
        /// <param name="key">键值</param>
        /// <param name="comlums">列名</param>
        /// <param name="condition">条件</param>
        /// <returns>返回对应数据</returns>
        public static IList<StoreOrderModel> GetStoreOrderListEffectQueryOrder(PaginationModel pagin, string tableName, string key, string comlums, string condition)
        {//GetDataPagePagination
            IList<StoreOrderModel> storeOrderList = new List<StoreOrderModel>();
            SqlParameter[] ps = new SqlParameter[] 
            {
                new SqlParameter("@tableName",tableName),
                new SqlParameter("@key",key),
                new SqlParameter("@comlums",comlums),
                new SqlParameter("@condition",condition),
                new SqlParameter("@start",pagin.GetPageDate()),
                new SqlParameter("@end",pagin.GetEndDate()),
                new SqlParameter("@DataCount",pagin.DataCount)
            };
            ps[6].Direction = ParameterDirection.Output;
            ps[6].Size = 10;
            SqlDataReader dr = DBHelper.ExecuteReader("GetDataPagePagination", ps, CommandType.StoredProcedure);
            StoreOrderModel item;
            while (dr.Read())
            {
                item = new StoreOrderModel();
                item.StoreorderId = dr.GetString(0);                        //订单号
                item.ID = dr.GetInt32(1);                                   //标示
                item.OrderDatetime = dr.GetDateTime(2);                     //订货日期
                item.ExpectNum = dr.GetInt32(3);                            //订货期数
                item.TotalPv = dr.GetDecimal(4);                            //总PV
                item.TotalMoney = dr.GetDecimal(5);                         //总金额
                item.IscheckOut = dr.GetString(6);                          //是否付款
                item.IsSent = dr.GetString(7);                              //是否发货
                item.IsReceived = dr.GetString(8);                          //是否已收货
                item.InceptPerson = dr.GetString(9);                        //收货人姓名
                item.InceptAddress = dr.GetString(10);                      //收货人地址
                item.ConveyancemodeId = dr.GetString(11);                   //运输模式
                item.Carriage = dr.GetDecimal(12);                          //运费
                item.Weight = dr.GetDecimal(13);                            //重量
                item.Description = dr.GetString(14);                        //描述
                storeOrderList.Add(item);
            }
            dr.NextResult();
            pagin.DataCount = Convert.ToInt32(ps[6].Value.ToString());
            dr.Close();
            return storeOrderList;
        }

        /// <summary>
        /// 公司子系统订单出库界面（带分页功能）
        /// </summary>
        /// <param name="pagin">分页类</param>
        /// <param name="tableName">表名</param>
        /// <param name="key">键值</param>
        /// <param name="comlums">列名</param>
        /// <param name="condition">条件</param>
        /// <returns>返回对应数据</returns>
        public IList<StoreOrderModel> GetStoreOrderListEffectBillOutOrder(PaginationModel pagin, string tableName, string key, string comlums, string condition)
        {
            IList<StoreOrderModel> storeOrderList = new List<StoreOrderModel>();
            SqlParameter[] ps = new SqlParameter[] 
            {
                new SqlParameter("@tableName",tableName),
                new SqlParameter("@key",key),
                new SqlParameter("@comlums",comlums),
                new SqlParameter("@condition",condition),
                new SqlParameter("@start",pagin.GetPageDate()),
                new SqlParameter("@end",pagin.GetEndDate()),
                new SqlParameter("@DataCount",pagin.DataCount)
            };
            ps[6].Direction = ParameterDirection.Output;
            ps[6].Size = 10;
            SqlDataReader dr = DBHelper.ExecuteReader("GetDataPagePagination", ps, CommandType.StoredProcedure);
            StoreOrderModel item;
            while (dr.Read())//InceptAddress,Telephone,Weight,Carriage,OrderDateTime
            {
                item = new StoreOrderModel();
                item.ID = dr.GetInt32(0);
                item.StoreId = dr.GetString(1);
                item.StoreorderId = dr.GetString(2);
                item.ExpectNum = dr.GetInt32(3);
                item.TotalPv = dr.GetDecimal(4);
                item.TotalMoney = dr.GetDecimal(5);
                item.IscheckOut = dr.GetString(6);
                item.InceptPerson = dr.GetString(7);
                item.InceptAddress = dr.GetString(8);
                item.Telephone = dr.GetString(9);
                item.Weight = dr.GetDecimal(10);
                item.Carriage = dr.GetDecimal(11);
                item.OrderDatetime = dr.GetDateTime(12);
                storeOrderList.Add(item);
            }
            dr.NextResult();
            pagin.DataCount = Convert.ToInt32(ps[6].Value.ToString());
            dr.Close();
            return storeOrderList;
        }

        /// <summary>
        /// 公司子系统订单发货显示数据
        /// </summary>
        /// <returns></returns>
        public IList<StoreOrderModel> GetStoreOrderListEffectCompanyConsign(PaginationModel pagin, string tableName, string key, string comlums, string condition)
        {
            IList<StoreOrderModel> storeOrderList = new List<StoreOrderModel>();
            SqlParameter[] ps = new SqlParameter[] 
            {
                new SqlParameter("@tableName",tableName),
                new SqlParameter("@key",key),
                new SqlParameter("@comlums",comlums),
                new SqlParameter("@condition",condition),
                new SqlParameter("@start",pagin.GetPageDate()),
                new SqlParameter("@end",pagin.GetEndDate()),
                new SqlParameter("@DataCount",pagin.DataCount)
            };
            ps[6].Direction = ParameterDirection.Output;
            ps[6].Size = 10;
            SqlDataReader dr = DBHelper.ExecuteReader("GetDataPagePagination", ps, CommandType.StoredProcedure);
            //  dr.NextResult();
            //  pagin.DataCount = Convert.ToInt32(ps[6].Value.ToString());
            StoreOrderModel item;

            while (dr.Read())
            {
                item = new StoreOrderModel();
                string dd = dr.GetString(0);
                item.ID = dr.GetInt32(0);
                item.StoreId = dr.GetString(1);
                item.StoreorderId = dr.GetString(2);
                item.OrderDatetime = dr.GetDateTime(3);
                item.ExpectNum = dr.GetInt32(4);
                item.OrderType = dr.GetInt32(5);
                item.TotalMoney = dr.GetDecimal(6);
                item.TotalPv = dr.GetDecimal(7);
                item.Weight = dr.GetDecimal(8);
                item.Carriage = dr.GetDecimal(9);
                item.ForecastarriveDatetime = dr.GetDateTime(10);
                item.InceptPerson = dr.GetString(11);
                item.InceptAddress = dr.GetString(12);
                item.PostalCode = dr.GetString(13);
                item.Telephone = dr.GetString(14);
                item.ConveyanceMode = dr.GetString(15);
                item.ConveyanceCompany = dr.GetString(16);
                item.OutStorageOrderID = dr.GetString(17);
                storeOrderList.Add(item);
            }
            dr.NextResult();
            pagin.DataCount = Convert.ToInt32(ps[6].Value.ToString());
            dr.Close();
            return storeOrderList;
        }

        /// <summary>
        /// 返回订单信息用户收货确认(无分页)
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public IList<StoreOrderModel> GetStoreOrderList(string storeId)
        {
            IList<StoreOrderModel> storeOrderList = new List<StoreOrderModel>();
            string sql = "select StoreOrderID,TotalMoney,TotalPV,InceptPerson,InceptAddress,PostalCode,Telephone,ConveyanceModeID,IsCheckOut,Carriage,Weight,OrderDateTime from StoreOrder where StoreID=@num order by orderdatetime ";
            SqlParameter spa = new SqlParameter("@num", SqlDbType.NVarChar, 50);
            spa.Value = storeId;
            SqlDataReader dr = DBHelper.ExecuteReader(sql, spa, CommandType.Text);
            while (dr.Read())
            {
                StoreOrderModel tempItem = new StoreOrderModel();
                tempItem.StoreorderId = dr.GetString(0);
                tempItem.TotalMoney = dr.GetDecimal(1);
                tempItem.TotalPv = dr.GetDecimal(2);
                tempItem.InceptPerson = dr.GetString(3);
                tempItem.InceptAddress = dr.GetString(4);
                tempItem.PostalCode = dr.GetString(5);
                tempItem.Telephone = dr.GetString(6);
                tempItem.ConveyancemodeId = dr.GetString(7);
                tempItem.IscheckOut = dr.GetString(8);
                tempItem.Carriage = dr.GetDecimal(9);
                tempItem.Weight = dr.GetDecimal(10);
                tempItem.OrderDatetime = dr.GetDateTime(11);
                storeOrderList.Add(tempItem);
            }
            dr.Close();
            return storeOrderList;
        }


        /// <summary>
        /// 查询订单信息用于订单支付页面(无分页)
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <returns>该店铺对应的未支付订单</returns>
        public IList<StoreOrderModel> GetStoreOrderLists(string storeId)
        {
            IList<StoreOrderModel> storeOrderList = new List<StoreOrderModel>();
            string sql = "select StoreOrderID,StoreID,isnull(TotalMoney,0),isnull(TotalPv,0),isnull(TotalCommision,0),OrderType,OrderDateTime ";
            sql += " from StoreOrder where storeid=@storeId and IsCheckOut='N'";
            SqlDataReader dr = DBHelper.ExecuteReader(sql, new SqlParameter("@storeId", storeId), CommandType.Text);
            while (dr.Read())
            {
                StoreOrderModel tempItem = new StoreOrderModel();
                tempItem.StoreorderId = dr.GetString(0);
                tempItem.StoreId = dr.GetString(1);
                tempItem.TotalMoney = dr.GetDecimal(2);
                tempItem.TotalPv = dr.GetDecimal(3);
                tempItem.TotalCommision = dr.GetDecimal(4);
                tempItem.OrderType = dr.GetInt32(5);
                tempItem.OrderDatetime = dr.GetDateTime(6);
                storeOrderList.Add(tempItem);
            }
            dr.Close();
            return storeOrderList;
        }

        /// <summary>
        /// 查询订单信息用于订单支付页面(无分页)
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <returns>该店铺对应的未支付订单</returns>
        public IList<OrderGoodsMedel> GetOrderGoodsList(string storeId)
        {
            IList<OrderGoodsMedel> storeOrderList = new List<OrderGoodsMedel>();
            string sql = "select OrderGoodsID,StoreID,isnull(TotalMoney,0) TotalMoney,isnull(TotalPv,0) TotalPv,isnull(TotalCommision,0) TotalCommision,OrderType,OrderDateTime,PayType ";
            sql += " from OrderGoods where storeid=@storeId and IsCheckOut='N' order by OrderDateTime desc ";
            SqlDataReader dr = DBHelper.ExecuteReader(sql, new SqlParameter("@storeId", storeId), CommandType.Text);
            while (dr.Read())
            {
                OrderGoodsMedel tempItem = new OrderGoodsMedel();
                tempItem.OrderGoodsID = dr.GetString(0);
                tempItem.StoreId = dr.GetString(1);
                tempItem.TotalMoney = dr.GetDecimal(2);
                tempItem.TotalPv = dr.GetDecimal(3);
                tempItem.TotalCommision = dr.GetDecimal(4);
                tempItem.OrderType = dr.GetInt32(5);
                tempItem.OrderDatetime = dr.GetDateTime(6);
                tempItem.PayType = dr.GetInt32(7);
                storeOrderList.Add(tempItem);
            }
            dr.Close();
            return storeOrderList;
        }

        /// <summary>
        /// 获取未支付订单（新）
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <param name="StoreOrderID">订单编号</param>
        /// <param name="payType">支付状态</param>
        /// <returns></returns>
        public static DataTable GetOrderGoodsList(string storeId, string StoreOrderID, string payType)
        {
            StringBuilder sql = new StringBuilder();
            int num = 1;
            sql.Append("select ID,OrderGoodsID,StoreID,isnull(TotalMoney,0) TotalMoney,isnull(TotalPv,0) TotalPv,isnull(TotalCommision,0) TotalCommision,OrderType,OrderDateTime,PayType ");
            sql.Append(" from OrderGoods where storeid='");
            sql.Append(storeId);
            sql.Append("' and IsCheckOut='N'");
            if (StoreOrderID != "")
            {
                sql.Append(" and OrderGoodsID like '%");
                sql.Append(StoreOrderID);
                sql.Append("%'");
            }
            if (payType != "-1")
            {
                sql.Append(" and PayType='");
                sql.Append(payType);
                sql.Append("'");
            }

            return DBHelper.ExecuteDataTable(sql.ToString(), CommandType.Text);
        }

        public static DataTable GetOrderGoodsListZ(string storeorderId)
        {
            string sql = "select OrderGoodsID,StoreID,isnull(TotalMoney,0) as TotalMoney,isnull(TotalPv,0) as TotalPv,isnull(TotalCommision,0) as TotalCommision,OrderType,OrderDateTime,PayType ";
            sql += " from OrderGoods where  IsCheckOut='N' and OrderGoodsID=@storeorderId order by OrderDateTime desc";

            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@storeorderId", storeorderId);
            return DBHelper.ExecuteDataTable(sql, paras, CommandType.Text);
        }

        /// <summary>
        /// 查询为申请发货的订单
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <returns>该店铺为申请发货的订单</returns>
        public IList<OrderGoodsMedel> GetOrderGoodsByFlag(string storeId)
        {
            IList<OrderGoodsMedel> storeOrderList = new List<OrderGoodsMedel>();
            string sql = "select OrderGoodsID,StoreID,isnull(TotalMoney,0),isnull(TotalPv,0),isnull(TotalCommision,0),OrderType,OrderDateTime,PayType ";
            sql += " from OrderGoods where storeid=@storeId and IsCheckOut='Y' and deliveryflag=0 order by OrderDateTime desc ";
            SqlDataReader dr = DBHelper.ExecuteReader(sql, new SqlParameter("@storeId", storeId), CommandType.Text);
            while (dr.Read())
            {
                OrderGoodsMedel tempItem = new OrderGoodsMedel();
                tempItem.OrderGoodsID = dr.GetString(0);
                tempItem.StoreId = dr.GetString(1);
                tempItem.TotalMoney = dr.GetDecimal(2);
                tempItem.TotalPv = dr.GetDecimal(3);
                tempItem.TotalCommision = dr.GetDecimal(4);
                tempItem.OrderType = dr.GetInt32(5);
                tempItem.OrderDatetime = dr.GetDateTime(6);
                tempItem.PayType = dr.GetInt32(7);
                storeOrderList.Add(tempItem);
            }
            dr.Close();
            return storeOrderList;
        }
        public IList<OrderGoodsMedel> GetOrderGoodsByFlag(string storeId, string condition)
        {
            IList<OrderGoodsMedel> storeOrderList = new List<OrderGoodsMedel>();
            string sql = "select OrderGoodsID,StoreID,isnull(TotalMoney,0),isnull(TotalPv,0),isnull(TotalCommision,0),OrderType,OrderDateTime,PayType ,sendway";
            sql += " from OrderGoods where storeid=@storeId and fahuoorder='' and IsCheckOut='Y' order by OrderDateTime desc";
            sql += condition;
            SqlDataReader dr = DBHelper.ExecuteReader(sql, new SqlParameter("@storeId", storeId), CommandType.Text);
            while (dr.Read())
            {
                OrderGoodsMedel tempItem = new OrderGoodsMedel();
                tempItem.OrderGoodsID = dr.GetString(0);
                tempItem.StoreId = dr.GetString(1);
                tempItem.TotalMoney = dr.GetDecimal(2);
                tempItem.TotalPv = dr.GetDecimal(3);
                tempItem.TotalCommision = dr.GetDecimal(4);
                tempItem.OrderType = dr.GetInt32(5);
                tempItem.OrderDatetime = dr.GetDateTime(6);
                tempItem.PayType = dr.GetInt32(7);
                tempItem.SendWay = dr.GetInt32(8);
                storeOrderList.Add(tempItem);
            }
            dr.Close();
            return storeOrderList;
        }

        /// <summary>
        /// 查询选择的订单
        /// </summary>
        /// <param name="storeId">订单号</param>
        /// <returns>该店铺选择的订单</returns>
        public static IList<OrderGoodsMedel> GetOrderGoodsByOrders(string orderids)
        {
            IList<OrderGoodsMedel> storeOrderList = new List<OrderGoodsMedel>();
            string sql = "select OrderGoodsID,StoreID,isnull(TotalMoney,0),isnull(TotalPv,0),isnull(TotalCommision,0),OrderType,OrderDateTime,PayType ";
            sql += " from OrderGoods where OrderGoodsid in (" + orderids + ") and IsCheckOut='Y' order by OrderDateTime desc";
            SqlDataReader dr = DBHelper.ExecuteReader(sql);
            while (dr.Read())
            {
                OrderGoodsMedel tempItem = new OrderGoodsMedel();
                tempItem.OrderGoodsID = dr.GetString(0);
                tempItem.StoreId = dr.GetString(1);
                tempItem.TotalMoney = dr.GetDecimal(2);
                tempItem.TotalPv = dr.GetDecimal(3);
                tempItem.TotalCommision = dr.GetDecimal(4);
                tempItem.OrderType = dr.GetInt32(5);
                tempItem.OrderDatetime = dr.GetDateTime(6);
                tempItem.PayType = dr.GetInt32(7);
                storeOrderList.Add(tempItem);
            }
            dr.Close();
            return storeOrderList;
        }

        public static void UpdateDeliveryFlag(string orders, string orderId)
        {
            string sql = " update OrderGoods set fahuoOrder='" + orderId + "'  where OrderGoodsid in (" + orders + ") and IsCheckOut='Y' ";
            DBHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 修改订单表状态（店铺子系统--订单支付）
        /// </summary>
        /// <param name="storeOrderItem">店铺订货信息</param>
        /// <param name="tr">事务</param>
        /// <returns>是否修改成功</returns>
        public Boolean UpdStoreOrderCheckOut(StoreOrderModel storeOrderItem, SqlTransaction tr)
        {
            string sql = "Update StoreOrder set IsCheckOut='Y' where StoreorderId=@num";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 50) };
            spa[0].Value = storeOrderItem.StoreorderId;
            int retVal = DBHelper.ExecuteNonQuery(tr, sql, spa, CommandType.Text);
            return retVal > 0;
        }

        /// <summary>
        /// 删除订单主表信息
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public static Boolean DelStoreOrder(string orderId, SqlTransaction tr)
        {
            string sql = "delete from  storeorder where StoreOrderID=@num";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 50) };
            spa[0].Value = orderId;
            return DBHelper.ExecuteNonQuery(tr, sql, spa, CommandType.Text) > 0;
        }

        /// <summary>
        /// 删除订单主表信息
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public static Boolean DelOrderGoods(string orderId, SqlTransaction tr)
        {
            string sql = "delete from OrderGoods where StoreOrderID=@num";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 50) };
            spa[0].Value = orderId;
            return DBHelper.ExecuteNonQuery(tr, sql, spa, CommandType.Text) > 0;
        }

        /// <summary>
        /// 确认订单按钮(订单支付按钮)
        /// </summary>
        /// <param name="storeOrderList"></param>
        /// <returns></returns>
        public Boolean CheckOutStoreOrderDAL(IList<StoreOrderModel> storeOrderList)
        {
            Boolean temp = true;
            string connString = @"Data Source=PC-200909081635\SQLEXPRESS;Initial Catalog=DS2009;Integrated Security=True";
            OrderDetailDAL orderDetailServer = new OrderDetailDAL();//订单明细数据层
            IList<OrderDetailModel> orderDetails = new List<OrderDetailModel>(); //临时订单明细集合
            StockDAL stockServer = new StockDAL(); //店铺库存数据层
            //int retVal = 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlTransaction tr = conn.BeginTransaction();

                try
                {
                    foreach (StoreOrderModel item in storeOrderList)
                    {//当修改不成功数据回滚
                        if (UpdStoreOrderCheckOut(item, tr))
                        {
                            //修改成功读出对应订单明细集合随之修改库存在途数量以及预定数量
                            orderDetails = OrderDetailDAL.GetOrderDetailByStoreId(item.StoreorderId);
                            foreach (OrderDetailModel ordertailItem in orderDetails)
                            {
                                ///修改预定数量或在途数量未成功时回滚数据
                                if (!(StockDAL.UpdStockHasOrderCount(tr, ordertailItem.StoreId, ordertailItem.ProductId, ordertailItem.Quantity)) || !(stockServer.UpdStockInWayCount(tr, ordertailItem.StoreId, ordertailItem.ProductId)))
                                {
                                    tr.Rollback();
                                    conn.Close();
                                    temp = false;
                                }
                            }
                            continue;
                        }
                        else
                        {
                            tr.Rollback();
                            conn.Close();
                            temp = false;
                        }
                    }
                    if (temp)
                    {
                        tr.Commit();
                        conn.Close();
                        temp = true;
                    }
                }
                catch (SqlException)
                {
                    tr.Rollback();
                    conn.Close();
                    temp = false;
                }
                return temp;
            }

        }



        /// <summary>
        /// //订单编辑 DataTable
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataTable StoreOrderDataTable(string condition)
        {
            string sqlcmd = @"select StoreOrderID,OrderDateTime,ExpectNum,TotalPV,TotalMoney,InceptAddress,InceptPerson,PostalCode,Telephone,Country
                            ,Province,City from dbo.StoreOrder where " + condition;

            return DBHelper.ExecuteDataTable(sqlcmd);
        }


        /// <summary>
        /// //订单编辑 - 详细信息 DataTable
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public DataTable StoreOrderDataTable_I(string storeOrderID)
        {
            string sqlcmd = @"select pd.ProductName,od.Quantity,od.Price,od.Pv from dbo.OrderDetail od inner join dbo.Product pd on od.ProductID=pd.ProductID
                            where StoreOrderID=@num";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 50) };
            spa[0].Value = storeOrderID;
            return DBHelper.ExecuteDataTable(sqlcmd, spa, CommandType.Text);
        }

        /// <summary>
        /// StoreOrderModel
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public static StoreOrderModel GetStoreOrderModel(string storeOrderID)
        {
            string sqlcmd = @"select c.province,c.city,c.country,o.CPCCode,AuditingDate,StoreID,StoreOrderID,OrderDateTime,OutStorageOrderID,ExpectNum,PaymentDatetime,TotalPV,TotalMoney,Weight,Carriage,InceptAddress,InceptPerson,PostalCode,Telephone,ConveyanceMode
                            ,ForeCastArriveDateTime,ConveyanceMode,OrderType,Description,IsCheckOut,IsGeneOutBill,IsSent,IsReceived from storeorder o,city c where o.CPCCode=c.CPCCode and StoreOrderID=@num";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 50) };
            spa[0].Value = storeOrderID;
            SqlDataReader dr = DBHelper.ExecuteReader(sqlcmd, spa, CommandType.Text);

            dr.Read();

            StoreOrderModel som = new StoreOrderModel();

            som.AuditingDate = Convert.ToDateTime(dr["AuditingDate"]);
            som.StoreId = dr["StoreID"].ToString();
            som.StoreorderId = dr["StoreOrderID"].ToString();
            som.OrderDatetime = Convert.ToDateTime(dr["OrderDateTime"]);
            som.ExpectNum = Convert.ToInt32(dr["ExpectNum"]);
            som.OutStorageOrderID = dr["OutStorageOrderID"].ToString();
            som.TotalPv = Convert.ToDecimal(dr["TotalPV"]);
            som.TotalMoney = Convert.ToDecimal(dr["TotalMoney"]);
            som.Weight = Convert.ToDecimal(dr["Weight"]);
            som.Carriage = Convert.ToDecimal(dr["Carriage"]);
            som.InceptAddress = dr["InceptAddress"].ToString();
            som.InceptPerson = dr["InceptPerson"].ToString();
            som.PostalCode = dr["PostalCode"].ToString();
            som.Telephone = dr["Telephone"].ToString();
            som.ForecastarriveDatetime = Convert.ToDateTime(dr["ForeCastArriveDateTime"]);
            som.ConveyanceCompany = dr["ConveyanceMode"].ToString();
            som.ConveyanceMode = dr["ConveyanceMode"].ToString();
            som.CPCCode = dr["CPCCode"].ToString();
            som.Description = dr["Description"].ToString();
            som.OrderType = Convert.ToInt32(dr["OrderType"]);
            som.IscheckOut = dr["IsCheckOut"].ToString();
            som.IsGeneOutBill = dr["IsGeneOutBill"].ToString();
            som.IsSent = dr["IsSent"].ToString();
            som.IsReceived = dr["IsReceived"].ToString();
            som.City.Country = dr["country"].ToString();
            som.City.Province = dr["province"].ToString();
            som.City.City = dr["city"].ToString();
            dr.Close();

            return som;
        }


        /// <summary>
        /// StoreOrderModel
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public static OrderGoodsMedel GetOrderGoodsMedel(string storeOrderID)
        {
            string sqlcmd = @"select c.xian,c.province,c.city,c.country,o.CPCCode,StoreID,OrderGoodsID,OrderDateTime,ExpectNum,PaymentDatetime,TotalPV,TotalMoney,Weight,Carriage,InceptAddress,InceptPerson,PostalCode,Telephone
                            ,OrderType,Description,IsCheckOut from ordergoods o,city c where o.CPCCode=c.CPCCode and ordergoodsID=@num";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 50) };
            spa[0].Value = storeOrderID;
            SqlDataReader dr = DBHelper.ExecuteReader(sqlcmd, spa, CommandType.Text);

            dr.Read();

            OrderGoodsMedel som = new OrderGoodsMedel();

            //som.AuditingDate = Convert.ToDateTime(dr["AuditingDate"]);
            som.StoreId = dr["StoreID"].ToString();
            som.OrderGoodsID = dr["OrderGoodsID"].ToString();
            som.OrderDatetime = Convert.ToDateTime(dr["OrderDateTime"]);
            som.ExpectNum = Convert.ToInt32(dr["ExpectNum"]);
            som.TotalPv = Convert.ToDecimal(dr["TotalPV"]);
            som.TotalMoney = Convert.ToDecimal(dr["TotalMoney"]);
            som.Weight = Convert.ToDecimal(dr["Weight"]);
            som.Carriage = Convert.ToDecimal(dr["Carriage"]);
            som.InceptAddress = dr["InceptAddress"].ToString();
            som.InceptPerson = dr["InceptPerson"].ToString();
            som.PostalCode = dr["PostalCode"].ToString();
            som.Telephone = dr["Telephone"].ToString();



            som.CPCCode = dr["CPCCode"].ToString();
            som.Description = dr["Description"].ToString();
            som.OrderType = Convert.ToInt32(dr["OrderType"]);
            som.IscheckOut = dr["IsCheckOut"].ToString();



            som.City.Country = dr["country"].ToString();
            som.City.Province = dr["province"].ToString();
            som.City.City = dr["city"].ToString();
            som.City.Xian = dr["xian"].ToString();
            dr.Close();

            return som;
        }
        /// <summary>
        /// 更具发货单号-- 获取订单的支付方式
        /// </summary>
        /// <param name="fahuoorder"></param>
        /// <returns></returns>
        public static int GetOrderGoodsPayType(string fahuoorder)
        {
            int paytype = -1;
            string sqlcmd = @"select Paytype from ordergoods where fahuoorder=@fahuoorder";
            SqlParameter[] sp = { new SqlParameter("@fahuoorder", fahuoorder) };
            SqlDataReader dr = DBHelper.ExecuteReader(sqlcmd, CommandType.Text);
            if (dr.Read())
            {
                paytype = int.Parse(dr["paytype"].ToString());
            }
            return paytype;
        }

        public static StoreOrderModel GetStoreOrderModel_II(string storeOrderID)
        {
            string sqlcmd = @"select c.province,c.city,c.country,c.xian,o.CPCCode,AuditingDate,StoreID,StoreOrderID,OrderDateTime,ExpectNum,TotalPV,TotalMoney,Weight,Carriage,InceptAddress,InceptPerson,PostalCode,Telephone,ConveyanceMode
                            ,ForeCastArriveDateTime,ConveyanceMode,OrderType,Description,IsCheckOut,IsGeneOutBill,IsSent,IsReceived from StoreOrder o,city c where o.CPCCode=c.CPCCode and StoreOrderID=@num";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 50) };
            spa[0].Value = storeOrderID;
            SqlDataReader dr = DBHelper.ExecuteReader(sqlcmd, spa, CommandType.Text);

            dr.Read();

            StoreOrderModel som = new StoreOrderModel();

            som.AuditingDate = Convert.ToDateTime(dr["AuditingDate"]);
            som.StoreId = dr["StoreID"].ToString();
            som.StoreorderId = dr["StoreOrderID"].ToString();
            som.OrderDatetime = Convert.ToDateTime(dr["OrderDateTime"]);
            som.ExpectNum = Convert.ToInt32(dr["ExpectNum"]);
            som.TotalPv = Convert.ToDecimal(dr["TotalPV"]);
            som.TotalMoney = Convert.ToDecimal(dr["TotalMoney"]);
            som.Weight = Convert.ToDecimal(dr["Weight"]);
            som.Carriage = Convert.ToDecimal(dr["Carriage"]);
            som.InceptAddress = dr["InceptAddress"].ToString();
            som.InceptPerson = dr["InceptPerson"].ToString();
            som.PostalCode = dr["PostalCode"].ToString();
            som.Telephone = dr["Telephone"].ToString();
            som.ForecastarriveDatetime = Convert.ToDateTime(dr["ForeCastArriveDateTime"]);
            som.ConveyanceCompany = dr["ConveyanceMode"].ToString();
            som.ConveyanceMode = dr["ConveyanceMode"].ToString();
            som.CPCCode = dr["CPCCode"].ToString();
            som.Description = dr["Description"].ToString();
            som.OrderType = Convert.ToInt32(dr["OrderType"]);
            som.IscheckOut = dr["IsCheckOut"].ToString();
            som.IsGeneOutBill = dr["IsGeneOutBill"].ToString();
            som.IsSent = dr["IsSent"].ToString();
            som.IsReceived = dr["IsReceived"].ToString();
            som.City.Country = dr["country"].ToString();
            som.City.Province = dr["province"].ToString();
            som.City.City = dr["city"].ToString();
            som.City.Xian = dr["Xian"].ToString();
            dr.Close();

            return som;
        }

        public static DataTable GetStoreOrderInfo(string storeOrderID)
        {
            string strSql = "select o.ID,StoreID,StoreOrderID,TotalMoney,TotalPV,ExpectNum,(c.Province+c.City+InceptAddress) as InceptAddress,InceptPerson,PostalCode,Telephone,OrderDateTime,c.Country,c.Province,c.City from StoreOrder o,City c Where o.cpccode=c.cpccode and o.StoreOrderId=@StoreOrderId";

            SqlParameter[] parm = new SqlParameter[]{
                new SqlParameter("@StoreOrderId", storeOrderID)
            };

            return DBHelper.ExecuteDataTable(strSql, parm, CommandType.Text);
        }

        /// <summary>
        /// 根据店铺编号获取剩余订单款
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <returns>返回订单款</returns>
        public DataTable GetLeftMoney(string storeId)
        {
            DataTable dt = DBHelper.ExecuteDataTable("select isnull(TotalAccountMoney-TotalOrderGoodMoney,0),isnull(TurnOverMoney-TurnOverGoodsMoney,0) from StoreInfo where StoreID=@StoreID", new SqlParameter[] { new SqlParameter("@StoreID", storeId) }, CommandType.Text);
            return dt;
        }


        /// <summary>
        /// 根据店店铺编号查询店铺所有未支付订单信息
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <returns>店铺订货单泛型集合</returns>
        public IList<StoreOrderModel> GetStoreOrdersPagin(string storeId)
        {
            IList<StoreOrderModel> storeOrders = null;
            return storeOrders;
        }

        /// <summary>
        /// StoreOrder DataTable
        /// </summary>
        /// <returns></returns>
        public static DataTable StoreOrderDT(string storeOrderID)
        {
            string cmd = @"select feedback,StoreID,StoreOrderID,ExpectNum,IsCheckOut,IsSent,IsReceived,
                             OrderType,InceptPerson,city.country,city.province,city.city,city.xian,ConveyanceMode,
                            InceptAddress,PostalCode,TotalMoney,TotalPV,Telephone,Weight,Carriage,ConveyanceCompany
                            ,OrderDateTime,ConsignmentDateTime,Description from 
                            StoreOrder so  left outer join city on so.cpccode=city.cpccode
                            where storeorderid=@num";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 50) };
            spa[0].Value = storeOrderID;
            return DBHelper.ExecuteDataTable(cmd, spa, CommandType.Text);
        }

        /// <summary>
        /// StoreOrder DataTable
        /// </summary>
        /// <returns></returns>
        public static DataTable StoreOrderGoodsDT(string storeOrderID)
        {
            string cmd = @"select StoreID,ordergoodsid,ExpectNum,IsCheckOut,
                             OrderType,InceptPerson,city.country,city.province,city.city,
                            (city.province+city.city+InceptAddress) as InceptAddress,PostalCode,TotalMoney,TotalPV,Telephone,Weight,Carriage
                            ,OrderDateTime,Description from 
                            ordergoods so  left outer join city on so.cpccode=city.cpccode 
                            where ordergoodsid=@num";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 50) };
            spa[0].Value = storeOrderID;
            return DBHelper.ExecuteDataTable(cmd, spa, CommandType.Text);
        }

        /// <summary>
        /// StoreOrder DataTable
        /// </summary>
        /// <returns></returns>
        public static DataTable StoreOrderDataTable_II(string condition)
        {
            string cmd = @"select StoreID, StoreOrderID, OutStorageOrderID, ExpectNum,IsCheckOut,IsSent,IsReceived
                            ,InceptPerson,Province,City,InceptAddress,PostalCode,Telephone,TotalMoney,cr.Name
                            ,TotalPV,Telephone,Weight,OrderDateTime,ConsignmentDateTime from dbo.StoreOrder so left outer join 
                            dbo.Currency cr on so.StandardCurrency=cr.ID where " + condition + " order by OrderDateTime desc";

            return DBHelper.ExecuteDataTable(cmd);
        }

        /// <summary>
        /// StoreOrder DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable StoreOrderDataTable_III(string storeOrderID)
        {
            string cmd = @"select p.ProductCode,p.ProductName,od.Quantity,pu.ProductUnitName,so.ExpectNum,od.Price,
                        od.Pv
                        from dbo.StoreOrder so left outer join OrderDetail od on so.StoreOrderID=od.StoreOrderID
                         left outer join dbo.Product p  on p.ProductID=od.ProductID left outer join dbo.ProductUnit pu
                        on P.SmallProductUnitID=pu.ProductUnitID  where so.StoreOrderID=@num";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 50) };
            spa[0].Value = storeOrderID;
            return DBHelper.ExecuteDataTable(cmd, spa, CommandType.Text);
        }

        /// <summary>
        /// StoreOrder DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable OrderGoodsDataTable_III(string storeOrderID)
        {
            string cmd = @"select p.ProductCode,p.ProductName,od.Quantity,pu.ProductUnitName,so.ExpectNum,od.Price,
                        od.Pv,cr.Name
                        from dbo.OrderGoods so left outer join OrderGoodsDetail od on so.OrderGoodsID=od.OrderGoodsID
                         left outer join dbo.Product p  on p.ProductID=od.ProductID left outer join dbo.ProductUnit pu
                        on P.SmallProductUnitID=pu.ProductUnitID left outer join 
                        dbo.Currency cr on so.StandardCurrency=cr.ID where so.OrderGoodsID=@num";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 50) };
            spa[0].Value = storeOrderID;
            return DBHelper.ExecuteDataTable(cmd, spa, CommandType.Text);
        }

        /// <summary>
        /// 检查所有指定标识的订单是否已经支付
        /// </summary>
        /// <param name="orderids">判断所选订单是否已经被支付过</param>
        /// <returns></returns>
        public bool CheckOrderPay(string orderids)
        {
            orderids = DisposeString.DisString(orderids);
            string sql = "select count(1) from StoreOrder where isCheckOut = 'Y' and id in (" + orderids + ")";
            object obj = DBHelper.ExecuteScalar(sql);
            if (obj == null)
            {
                return false;
            }
            else
            {
                if (int.Parse(obj.ToString()) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// StoreOrder DataTable 用于订单编辑
        /// </summary>
        /// <returns></returns>
        public static DataTable StoreOrderDataTable_IV(string condition)
        {
            string cmd = @"select StoreID,StoreOrderID,OutStorageOrderID,ExpectNum,IsCheckOut,IsSent,IsReceived,
                            case OrderType when 1 then '周转货' when 0 then '正常订货' end as OrderType,InceptPerson,
                            InceptAddress,PostalCode,TotalMoney,TotalPV,Telephone,Weight,Carriage,ConveyanceCompany
                            ,OrderDateTime,ConsignmentDateTime,Description
                              from dbo.StoreOrder where " + condition;

            return DBHelper.ExecuteDataTable(cmd);
        }
        /// <summary>
        /// 根据店铺编号查询已付款已发送未收货确认的订单集合
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns></returns>
        public DataTable GetStoreOrdersByStoreid(string storeid)
        {
            string sql = @"SELECT * FROM StoreOrder where StoreID=@num And isCheckOut='Y' And isSent='Y' And isReceived<>'Y'";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.NVarChar, 50) };
            spa[0].Value = storeid;
            return DBHelper.ExecuteDataTable(sql, spa, CommandType.Text);
        }
        /// <summary>
        /// 修改对应的订单为已收货
        /// </summary>
        /// <param name="storeOrderid">订单编号</param>
        /// <returns></returns>
        public int UpdateStoreOrderByStoreOrderid(string storeOrderid)
        {
            string sql = "update StoreOrder set isReceived='Y' where StoreOrderID=@num";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 50) };
            spa[0].Value = storeOrderid;
            return DBHelper.ExecuteNonQuery(sql, spa, CommandType.Text);
        }

        /// <summary>
        /// 订单编辑-备注查看
        /// </summary>
        /// <param name="StoreOrderID"></param>
        /// <returns></returns>
        public static string GetDescription(string storeOrderID)
        {
            string cmd = "select Description from dbo.StoreOrder where StoreOrderID=@num";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 50) };
            spa[0].Value = storeOrderID;
            SqlDataReader dr = DBHelper.ExecuteReader(cmd, spa, CommandType.Text);

            dr.Read();

            string description = dr["Description"].ToString();

            dr.Close();

            return description;
        }

        /// <summary>
        /// 库存单据-备注查看
        /// </summary>
        /// <param name="StoreOrderID"></param>
        /// <returns></returns>
        public static string GetNote(string docID)
        {
            string cmd = "select Note from InventoryDoc where DocID=@num";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 20) };
            spa[0].Value = docID;
            SqlDataReader dr = DBHelper.ExecuteReader(cmd, spa, CommandType.Text);

            dr.Read();

            string note = dr["Note"].ToString();

            dr.Close();

            return note;
        }

        /// <summary>
        /// 订单发货
        /// </summary>
        /// <param name="storeOrderIDs"></param>
        /// <returns></returns>
        public static int UpdStoreOrderIsSent(StoreOrderModel som)
        {
            string cmd = "update StoreOrder set IsSent=@issent,Weight=@weight,ForeCastArriveDateTime=@foreCastArriveDateTime,ConsignmentDateTime=@ConsignmentDateTime where StoreOrderID=@storeorderId";

            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter("@issent","Y"),
                new SqlParameter("@weight",som.Weight),
                new SqlParameter("@foreCastArriveDateTime",som.ForecastarriveDatetime),
                new SqlParameter("@storeorderId",som.StoreorderId),
                new SqlParameter("@ConsignmentDateTime",DateTime.Now)

            };

            return DBHelper.ExecuteNonQuery(cmd, param, CommandType.Text);
        }
        /// <summary>
        /// 店铺收货时添加店铺反馈意见
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <param name="id">店铺订货单编号</param>
        /// <param name="questions">店铺录入的问题</param>
        public void InserFeedBack(string storeId, int id, string questions)
        {
            string sql = "update StoreOrder set FeedBack=@num where id=@num1 and StoreID=@num2";
            SqlParameter[] spa = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.VarChar, 500),
                new SqlParameter("@num1", SqlDbType.Int),
                new SqlParameter("@num2", SqlDbType.NVarChar,50)
            };
            spa[0].Value = questions;
            spa[1].Value = id;
            spa[2].Value = storeId;
            DBHelper.ExecuteNonQuery(sql, spa, CommandType.Text);
        }


        /// <summary>
        /// 修改为发货状态
        /// </summary>
        /// <param name="storeOrderID"></param>
        /// <returns></returns>
        public Boolean UpdStoreOrderIsSent1(string[] storeOrderIDs)
        {
            Boolean temp = false;
            string sql = "";
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tr = conn.BeginTransaction();
                try
                {
                    foreach (string item in storeOrderIDs)
                    {
                        sql = "Update StoreOrder set IsSent='Y' where StoreOrderID=@num";
                        SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 50) };
                        spa[0].Value = item;
                        temp = DBHelper.ExecuteNonQuery(tr, sql, spa, CommandType.Text) > 0;
                        if (!temp)
                        {
                            tr.Rollback();
                            break;
                        }
                    }
                    tr.Commit();
                }
                catch
                {
                    tr.Rollback();
                }
                finally
                {
                    tr.Dispose();
                    conn.Close();
                }
            }
            return temp;
        }

        public string PayOrderment(string orderids, string storeID, string operateIP, string operateNum)
        {

            string sp = "PayOrders";
            int vRel = 0;
            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@orderNums",orderids),
                new SqlParameter("@storeID",storeID),
                new SqlParameter("@operateIP",operateIP),
                new SqlParameter("@operateNum",operateNum)
            };
            try
            {
                vRel = DBHelper.ExecuteNonQuery(sp, paras, CommandType.StoredProcedure);
            }
            catch (SqlException e)
            {
                System.Diagnostics.Trace.WriteLine(e.Message);
                throw;
            }
            return "成功支付订单.";
        }

        /// <summary>
        /// 订单发货 DataTable
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static DataTable StoreOrderDataTable_V(string condition)
        {
            string cmd = @"select so.StoreID,so.StoreOrderID,so.OutStorageOrderID,so.IsAuditing,so.OrderDateTime,so.ExpectNum,so.PayMentDateTime,
                            so.IsCheckOut,so.IsSent,case so.OrderType when 1 then '周转货' when 0 then '正常订货' end as OrderType,
                            so.TotalMoney,so.TotalPV,so.Weight,so.Carriage,so.ForeCastArriveDateTime,so.InceptPerson,
                            so.InceptAddress,so.PostalCode,so.Telephone,so.ConveyanceMode,so.ConveyanceCompany,
                            wh.WareHouseName,ds.SeatName
                            from dbo.StoreOrder so 
                            left outer join InventoryDoc ity on so.OutStorageOrderID=ity.DocID
                            left outer join WareHouse wh on wh.WareHouseID=ity.WareHouseID left outer join DepotSeat ds
                            on ds.DepotSeatID=ity.DepotSeatID and ity.WareHouseID=ds.WareHouseID where " + condition + " order by so.OrderDateTime desc";

            return DBHelper.ExecuteDataTable(cmd);
        }

        /// <summary>
        /// 判断订单是否存在
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns>是或否</returns>
        public bool IsExist(string orderId)
        {
            bool isexist = false;
            string sSQL = "Select Count(*) From StoreOrder Where StoreOrderID=@num";
            SqlParameter[] spa = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.VarChar, 50) 
            };
            spa[0].Value = orderId;
            if (DBHelper.ExecuteScalar(sSQL, spa, CommandType.Text).ToString() == "0")
            {
                isexist = true;
            }
            return isexist;
        }

        /// <summary>
        /// 订单支付，修改是否支付状态为‘Y’
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>是否成功</returns>
        public static bool UpdateState(SqlTransaction tr, string orderId)
        {
            string sql = "update storeorder set IsCheckOut='Y',IsAuditing='Y',paymentdatetime=getdate() where storeorderid=@num";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 50) };
            spa[0].Value = orderId;
            return DBHelper.ExecuteNonQuery(tr, sql, spa, CommandType.Text) > 0;
        }

        /// <summary>
        /// 订单支付，修改是否支付状态为‘Y’
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>是否成功</returns>
        public static bool UpdateOrderGoodsState(SqlTransaction tr, string orderId)
        {
            string sql = "update OrderGoods set IsCheckOut='Y',IsAuditing='Y',paymentdatetime=getdate() where storeorderid=@num";
            SqlParameter[] spa = new SqlParameter[] { new SqlParameter("@num", SqlDbType.VarChar, 50) };
            spa[0].Value = orderId;
            return DBHelper.ExecuteNonQuery(tr, sql, spa, CommandType.Text) > 0;
        }

        /// <summary>
        /// 确认出库按钮。并生成出库单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Boolean OutOrder(string storeOrderID, string isGeneOutBill)
        {
            string cmd = "update StoreOrder set IsGeneOutBill=@num where StoreOrderID=@num1";
            SqlParameter[] spa = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.Char, 1),
                new SqlParameter("@num1", SqlDbType.VarChar, 50)
            };
            spa[0].Value = isGeneOutBill;
            spa[1].Value = storeOrderID;
            if (DBHelper.ExecuteNonQuery(cmd, spa, CommandType.Text) == 1)
                return true;
            return false;
        }

        /// <summary>
        /// 根据 出库单号 获取一条信息
        /// </summary>
        /// <param name="outStorageOrderID"></param>
        /// <returns></returns>
        public static StoreOrderModel GetStoreOrder(string outStorageOrderID)
        {
            string cmd = "select StoreID,StoreOrderID,ForeCastArriveDateTime,Carriage,Weight from dbo.StoreOrder where OutStorageOrderID=@num";
            SqlParameter[] spa = new SqlParameter[] { 
                new SqlParameter("@num", SqlDbType.NVarChar, 50) 
            };
            spa[0].Value = outStorageOrderID;
            SqlDataReader dr = DBHelper.ExecuteReader(cmd, spa, CommandType.Text);

            StoreOrderModel som = new StoreOrderModel();

            if (dr.Read())
            {
                som.StoreId = dr["StoreID"].ToString();
                som.StoreorderId = dr["StoreOrderID"].ToString();
                som.ForecastarriveDatetime = Convert.ToDateTime(dr["ForeCastArriveDateTime"]);
                som.Carriage = Convert.ToDecimal(dr["Carriage"]);
                som.Weight = Convert.ToDecimal(dr["Weight"]);
            }
            dr.Close();
            return som;
        }

        /// <summary>
        /// 订单出库 DataTable
        /// </summary>
        /// <returns></returns>
        public static DataTable StoreOrderDataTable_VI(string condition)
        {
            string cmd = @"select StoreID,StoreOrderID,OutStorageOrderID,ExpectNum,case OrderType when 1 then '周转货' when 0 then '正常订货' end as OrderType,
                            TotalMoney,TotalPv,IsCheckOut,PayMentDateTime,InceptPerson,InceptAddress,Telephone,Weight,Carriage,OrderDateTime,
                            Description from dbo.StoreOrder where " + condition + " order by OrderDateTime desc";

            return DBHelper.ExecuteDataTable(cmd);
        }

        /// <summary>
        /// 获取店铺到货订单（收货确认）
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <returns>订单列表</returns>
        public DataTable GetReceivedOrder(string storeId)
        {
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@storeid", storeId);
            string sqltxt = @"SELECT i.trackingnum,s.StoreOrderID,i.docid,i.TotalMoney,i.client,i.TotalPv,s.InceptPerson,s.InceptAddress,s.PostalCode,s.Telephone,s.IsCheckOut,s.kuaididh FROM StoreOrder s join InventoryDoc i on s.storeorderid=i.storeorderid where s.sendway=0 and s.storeId=@storeid and i.IsSend=1 and i.IsReceived=0 ";
            return DBHelper.ExecuteDataTable(sqltxt, paras, CommandType.Text);
        }

        /// <summary>
        /// 获取会员到货订单（收货确认）
        /// </summary>
        /// <param name="storeId">会员编号</param>
        /// <returns>订单列表</returns>
        public DataTable GetMemberReceivedOrder(string number)
        {
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@number", number);
            string sqltxt = @"SELECT s.StoreOrderID, i.docid,i.client, i.TotalMoney, i.TotalPv, s.InceptPerson, s.InceptAddress, s.PostalCode, s.Telephone, s.IsCheckOut, s.kuaididh FROM StoreOrder s join InventoryDoc i on s.storeorderid=i.storeorderid where s.sendway=1 and i.client=@number and i.IsSend=1 and i.IsReceived=0 ";
            return DBHelper.ExecuteDataTable(sqltxt, paras, CommandType.Text);
        }

        public DataTable GetReceivedOrder1(string storeId)
        {
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@storeid", storeId);
            return DBHelper.ExecuteDataTable("SELECT StoreOrderID, TotalMoney, TotalPv, InceptPerson, InceptAddress, PostalCode, Telephone, IsCheckOut, Carriage, Weight FROM StoreOrder where storeId=@storeid and IsSent='Y' and IsReceived='N' ", paras, CommandType.Text);
        }
        /// <summary>
        /// 更新订单是否收货
        /// </summary>
        /// <param name="tr">事务</param>
        /// <param name="orderid">订单编号</param>
        /// <returns>返回是否更新成功</returns>
        public bool UpdateIsReceived(SqlTransaction tr, string orderid)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@orderid", orderid) };
            return DBHelper.ExecuteNonQuery(tr, "update InventoryDoc set IsReceived=1 where docid=@orderid", para, CommandType.Text) == 1;
        }

        /// <summary>
        /// 更新订单是否收货
        /// </summary>
        /// <param name="tr">事务</param>
        /// <param name="orderid">订单编号</param>
        /// <returns>返回是否更新成功</returns>
        public bool UpdateIsReceived1(SqlTransaction tr, string orderid)
        {
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@orderid", orderid) };
            return DBHelper.ExecuteNonQuery(tr, "update InventoryDoc set IsReceived=1 where StoreOrderID=@orderid", para, CommandType.Text) == 1;
        }
        /// <summary>
        /// 查询订单类型
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public string GetOrderType(string orderid)
        {
            SqlParameter para = new SqlParameter("@orderid", orderid);
            return DBHelper.ExecuteScalar("select OrderType from storeorder s join InventoryDoc i on s.storeorderid=i.storeorderid where docid=@orderid", para, CommandType.Text).ToString();
        }

        /// <summary>
        /// 查询订单类型和店铺ID
        /// </summary>
        /// <param name="orderid">单据ID inv</param>
        /// <returns>订单类型和店铺ID</returns>
        public string[] GetOrderTypeAndStoreid(string orderid)
        {
            SqlParameter[] para = { new SqlParameter("@orderid", orderid) };
            DataTable dt = DBHelper.ExecuteDataTable("select OrderType,storeid from storeorder s join InventoryDoc i on s.storeorderid=i.storeorderid where docid=@orderid", para, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                return new string[] { dt.Rows[0]["OrderType"].ToString(), dt.Rows[0]["storeid"].ToString() };
            }
            return new string[] { "", "" };
        }

        public bool UpdateFeedBack(string orderid, string question)
        {
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@orderid", orderid);
            para[1] = new SqlParameter("@question", question);
            return DBHelper.ExecuteNonQuery("update InventoryDoc set FeedBack=@question where docid=@orderid", para, CommandType.Text) == 1;
        }

        /// <summary>
        /// 删除未支付订单
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="storeOrderID">订单编号</param>
        /// <returns></returns>
        public static int DelStoreOrderItemProc(SqlTransaction tran, string storeOrderID)
        {
            SqlParameter[] parm = new SqlParameter[]
            {
                new SqlParameter("@sotreOrdreID",storeOrderID)
            };

            return DBHelper.ExecuteNonQuery(tran, "DelStoreOrderItem", parm, CommandType.StoredProcedure);
        }
    }
}