using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data.SqlClient;
using System.Data;
using BLL.CommonClass;
using BLL.Registration_declarations;

namespace BLL.Logistics
{
    /// <summary>
    /// 在线订货菜单（店铺子系统）
    /// </summary>
    public class OrderGoodsBLL
    {
        /// <summary>
        /// 确认订单按钮
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public Boolean OrderSubmit(string storeId,List<OrderDetailModel> orderDetails,OrderGoodsMedel storeItem,bool IsEdit)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tr=conn.BeginTransaction();//开启事务

                try
                {
                    if (IsEdit)//如果是修改订单，先删除原来的订单并还原原来的信息
                    {
                        BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("OrderGoods", "OrderGoodsID");//实例日志类
                        cl_h_info.AddRecordtran(tr, storeItem.OrderGoodsID);//添加日志，修改前记录原来数据

                        StoreOrderDAL.DelStoreOrderItemProc(tr, storeItem.OrderGoodsID);
                        //StockDAL.DelStoreOrder(tr, OrderDetailDAL.GetOrderGoodsDetail(storeItem.StoreorderId), storeItem.StoreId);//还原店库存
                        //OrderDetailDAL.DelOrderGoodsDetail(tr, storeItem.StoreorderId);//明细表删除失败回滚
                        //StoreOrderDAL.DelOrderGoods(storeItem.StoreorderId, tr); //订单表删除失败回滚

                        cl_h_info.AddRecordtran(tr, storeItem.OrderGoodsID);//添加日志，修改后记录原来数据
                        cl_h_info.ModifiedIntoLogstran(tr, BLL.CommonClass.ChangeCategory.store10, storeItem.OrderGoodsID, BLL.CommonClass.ENUM_USERTYPE.objecttype2);//插入日志
                    }

                    //插入订单
                    if (new StoreOrderDAL().AddOrderGoods(storeItem, tr))
                    {
                        //订单表插入成功插入明细表
                        foreach (OrderDetailModel orderDetailItem in orderDetails)
                        {
                            if (!OrderDetailDAL.AddOrderGoodsDetail(tr, orderDetailItem, storeItem.OrderGoodsID))
                            {
                                tr.Rollback();
                                return false;
                            }
                            
                        }
                        //修改库存信息(预订数量)
                        foreach (OrderDetailModel orderDetailItem in orderDetails)
                        {
                            if (!StockDAL.UpdStockHasOrderCount(tr, storeItem.StoreId, orderDetailItem.ProductId, orderDetailItem.Quantity))
                            {
                                tr.Rollback();
                                return false;
                            }
                        }

                        tr.Commit();//插入订单信息完成
                    }
                    else
                    {//订单插入失败回滚数据
                        tr.Rollback();
                        return false;
                    }
                }
                catch
                {
                    //订单插入失败回滚数据
                    tr.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return true;
        }

        /// <summary>
        /// 获取所有产品数据
        /// </summary>
        /// <param name="storeid">店铺的编号</param>
        /// <returns></returns>
        public static DataTable GetAllProducts(string storeid)
        {
            return ProductDAL.GetAllProducts(storeid);
        }

        public static DataTable GetAllProducts1(string storeid, double huilv)
        {
            return ProductDAL.GetAllProducts1(storeid, huilv);
        }
        /// <summary>
        /// 查詢出公司的實際庫存和在綫數量的總和
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="productid"></param>
        /// <returns></returns>
        public static DataTable GetAllTityByStoreidAndProductid(string storeid, int productid)
        {
            return new StockDAL().GetAllTityByStoreidAndProductid(storeid, productid);
        }

        public DataTable GetTotalProduct(string storeOrderID)
        {
            return ProductDAL.GetTotalProduct(storeOrderID);
        }

        public static StoreInfoModel GetStoreInfoByStoreId(string Storeid)
        {
            return StoreInfoDAL.GetStoreInfoByStoreId(Storeid);
        }

        public static DataTable GetAllProduct()
        {
            return ProductDAL.GetALLProduct();
        }

        /// <summary>
        /// 得到新的报单号 形似：09101309492032
        /// </summary>
        /// <returns>订单编号</returns>
        public static string GetNewOrderID()
        {
            string orderId = DateTime.Now.ToString("yyMMddHHmmss");
            int i = DateTime.Now.Millisecond;
            if (i >= 10)
            {
                orderId += i.ToString().Substring(0, 2);
            }
            else if (i < 10)
            {
                orderId += "0" + i.ToString().Substring(0, 1);
            }
            bool sameflag = false;
            do
            {
                if (new StoreOrderDAL().IsExist(orderId))
                {
                    orderId = DateTime.Now.ToString("yyMMddHHmmss");
                    int i1 = DateTime.Now.Millisecond;
                    if (i1 >= 10)
                    {
                        orderId += i1.ToString().Substring(0, 2);
                    }
                    else if (i1 < 10)
                    {
                        orderId += "0" + i1.ToString().Substring(0, 1);
                    }
                }
                else
                    sameflag = true;
            }while(sameflag);

            return orderId;
        }

        /// <summary>
        /// 获取订货款
        /// </summary>
        /// <param name="storeid">店编号</param>
        /// <returns>返回订货款</returns>
        public static string GetOrderMoney(string storeid)
        {
            return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + BLL.Translation.Translate("000471", "剩余报单款订货额") + "：" + (StoreInfoDAL.GetOrderMoney(storeid));
        }

        /// <summary>
        /// 获取订单明细
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static IList<OrderDetailModel> GetOrderDetail(string orderid)
        {
            return OrderDetailDAL.GetOrderDetail(orderid);
        }

        /// <summary>
        /// 获取订单明细
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static IList<OrderDetailModel> GetOrderGoodsDetail(string orderid)
        {
            return OrderDetailDAL.GetOrderGoodsDetail(orderid);
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static StoreOrderModel GetOrder(string orderId)
        {
            return StoreOrderDAL.GetStoreOrderModel(orderId);
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static OrderGoodsMedel GetOrderGoods(string orderId)
        {
            return StoreOrderDAL.GetOrderGoodsMedel(orderId);
        }

        /// <summary>
        /// 更具发货单号-- 获取订单的支付方式
        /// </summary>
        /// <param name="fahuoorder"></param>
        /// <returns></returns>
        public static int GetOrderGoodsPayType(string fahuoorder)
        {
            return StoreOrderDAL.GetOrderGoodsPayType(fahuoorder);
        }


        /// <summary>
        /// 根据店铺编号获取店铺名称
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <returns>店铺名称</returns>
        public static string GetStoreName(string storeId)
        {
            return StoreInfoDAL.GetStoreName(storeId);
        }


        /// <summary>
        /// 根据物品编号获取类编号
        /// </summary>
        /// <param name="proid">物品编号</param>
        /// <returns>类编号</returns>
        public static string GetPid(string proid)
        {
            return ProductDAL.GetPid(proid);
        }

        /// <summary>
        /// 根据店铺编号获取可发货数量
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns>可发货产品列表</returns>
        public static DataTable GetLackTotalNumber(string storeid)
        {
            return StockDAL.GetLackTotalNumber(storeid);
        }

        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <param name="pid">产品ID</param>
        /// <returns></returns>
        public static ProductModel GetProductById(int pid)
        {
            return ProductDAL.GetProductById(pid);
        }

        /// <summary>
        /// 根据订单ID获取用户编号  ---DS2012
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static string GetNumber(string orderID)
        {
            string sql = "select number from memberorder m join ordergoods o on m.orderid=o.OutStorageOrderID where o.OutStorageOrderID='" + orderID + "'";
            object obj = DBHelper.ExecuteScalar(sql);
            if (obj != null && obj != DBNull.Value)
            {
                return obj.ToString();
            }
            return "";
        }
        /// <summary>
        /// 我要发货，订单生成
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <param name="orderDetails">发货单明细</param>
        /// <param name="storeItem">发货单信息</param>
        /// <returns></returns>
        public Boolean ApplyGoodsSubmit(StoreOrderModel storeorder, string orders)
        {
            string orderId = OrderGoodsBLL.GetNewOrderID();
            int maxqishu = CommonDataBLL.GetMaxqishu();

            //总金额，积分，数量，重量
            decimal totalmoney = 0;
            decimal totalpv = 0;
            int totalQuantity = 0;
            decimal totalWeight = 0;
            string remark = "";

            //保存合并后订单明细
            IList<OrderDetailModel> orderDetails = new List<OrderDetailModel>();

            //获取要合并的订单列表
            IList<OrderGoodsMedel> storeorders = StoreOrderDAL.GetOrderGoodsByOrders(orders);

            //遍历要合并的订单列表
            foreach (OrderGoodsMedel order in storeorders)
            {
                //获取要合并的订单列表明细并且进行遍历
                foreach (OrderDetailModel detail in OrderDetailDAL.GetGoodsDetailModelList(order.OrderGoodsID))
                {
                    //判断是否已经有了该产品
                    int ct = 0;
                    //遍历合并后的订单列表，是否有和要合并的订单重复的产品
                    foreach (OrderDetailModel orderdetail in orderDetails)
                    {
                        if (orderdetail.ProductId == detail.ProductId)
                        {
                            orderdetail.Quantity += detail.Quantity;
                            ct++;//有重复的累加
                        }
                    }
                    //没有重复添加新的明细
                    if (ct == 0)
                    {
                        //生成一条订单明细
                        OrderDetailModel detailNew = new OrderDetailModel();
                        detailNew.StoreorderId = orderId;
                        detailNew.ProductId = detail.ProductId;
                        detailNew.Quantity = detail.Quantity;
                        detailNew.Price = detail.Price;
                        detailNew.Pv = detail.Pv;
                        detailNew.StoreId = detail.StoreId;
                        detailNew.ProductName = detail.ProductName;
                        detailNew.ExpectNum = maxqishu;
                        
                        //添加订单明细，到订单明细列表
                        orderDetails.Add(detailNew);
                    }
                }
                
                //累加总金额，总积分,数量
                totalmoney += order.TotalMoney;
                totalpv += order.TotalPv;
                totalQuantity += order.GoodsQuantity;
                totalWeight += order.Weight;
                remark += order.OrderGoodsID+";";
            }

            //保存合并后订单
            StoreOrderModel storeItem = new StoreOrderModel();
            storeItem.StoreId = storeorder.StoreId;                                                   //店铺ID
            storeItem.StoreorderId = orderId;                                            //订单号
            storeItem.TotalMoney = totalmoney;                                        //订单总金额
            storeItem.TotalPv = totalpv;                                             //订单总积分
            storeItem.InceptAddress = storeorder.InceptAddress;                                //收货人地址
            storeItem.InceptPerson = storeorder.InceptPerson;                                   //收货人姓名
            storeItem.PostalCode = storeorder.PostalCode;                                     //收货人邮编
            storeItem.Telephone = storeorder.Telephone;                                     //收货人电话
            storeItem.OrderDatetime = DateTime.Now.ToUniversalTime();                                      //订单时间
            storeItem.ExpectNum = CommonDataBLL.GetMaxqishu();                           //获取期数
            storeItem.TotalCommision = 0;                                                //手续费
            storeItem.GoodsQuantity = totalQuantity;                                                //货物件数
            storeItem.Carriage = 0;                                                      //运费
            storeItem.Weight = totalWeight;                                                       //重量
            storeItem.City.Country = storeorder.City.Country;                                   //国家
            storeItem.City.Province = storeorder.City.Province;                                   //省份
            storeItem.City.City = storeorder.City.City;                                          //城市
            storeItem.City.Xian = storeorder.City.Xian;    
            storeItem.IscheckOut = "Y";                                                  //是否支付
            storeItem.IsAuditing = "Y";
            storeItem.SendWay = storeorder.SendWay;
            storeItem.OperateIP = System.Web.HttpContext.Current.Request.UserHostAddress;                               //用户IP
            storeItem.Description = remark;                                                 //描述
            storeItem.ConsignmentDatetime = DateTime.Now.ToUniversalTime();                 //申请发货日期
            storeItem.OrderType = storeorder.OrderType;

            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tr = conn.BeginTransaction();//开启事务

                try
                {
                    //插入订单
                    if (new StoreOrderDAL().AddStoreOrder(storeItem, tr))
                    {
                        //DBHelper.ExecuteNonQuery(tr, "insert into ApplygoodsTb select '" + storeItem.Description + "',1");
                        //订单表插入成功插入明细表
                        foreach (OrderDetailModel orderDetailItem in orderDetails)
                        {
                            if (!OrderDetailDAL.AddOrderDetail(tr, orderDetailItem, storeItem.StoreorderId))
                            {
                                tr.Rollback();
                                return false;
                            }
                        }
                        //修改库存信息
                        foreach (OrderDetailModel orderDetailItem in orderDetails)
                        {
                            if (!StockDAL.UpdStockSendTotalNumber(tr, storeItem.StoreId, orderDetailItem.ProductId, orderDetailItem.Quantity))
                            {
                                tr.Rollback();
                                return false;
                            }
                        }
                        //更新订单发货状态
                        StoreOrderDAL.UpdateDeliveryFlag(orders, orderId);

                        //if (storeItem.SendType == 2)
                        //{
                        //    string strSql = @"Update memberorder Set sendType = 2 Where OrderId in (Select OutStorageOrderID from OrderGoods where storeorderid in (" + orders + "))";
                        //    DBHelper.ExecuteNonQuery(tr, strSql, null, CommandType.Text);
                        //}
                       
                        tr.Commit();//插入订单信息完成
                    }
                    else
                    {//订单插入失败回滚数据
                        tr.Rollback();
                        return false;
                    }
                }
                catch
                {
                    //订单插入失败回滚数据
                    tr.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return true;
        }

        /// <summary>
        /// 查询店铺订单列表
        /// </summary>
        /// <param name="storeId">店铺编号</param>
        /// <returns>订单列表</returns>
        public static IList<OrderGoodsMedel> GetOrderGoodsList(string storeId)
        {
            return new StoreOrderDAL().GetOrderGoodsByFlag(storeId);
        }

        public static IList<OrderGoodsMedel> GetOrderGoodsList(string storeId, string condition)
        {
            return new StoreOrderDAL().GetOrderGoodsByFlag(storeId,condition);
        }

        /// <summary>
        /// 获取店铺退货单数量
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns></returns>
        public static int GetStoreTHOrderCount(string storeid)
        {
            return StoreOrderDAL.GetStoreTHOrderCount(storeid);
        }
    }
}
