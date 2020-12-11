using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Model;



namespace DAL
{
    /**
     * 注册浏览数据层
     * 作者zhc
     * 时间：2009-9-19
     */
    public class BrowsememberordersDAL
    {
        /// <summary>
        /// 判断该会员有没有复消
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public int GetHasOrderAgain(string number)
        {
            string SQL = "select count(*) from MemberOrder where number=@number and isagain=1";
            SqlParameter[] para =
            {
               new SqlParameter("@number",number)
             
            };
            return (int)DBHelper.ExecuteScalar(SQL, para, CommandType.Text);
        }

        public int GetXHanzhi(string number)
        {
            int aznum = 0;
            if (DBHelper.ExecuteScalar("select placement from memberinfo where number='" + number + "'").ToString() != DAL.CommonDataDAL.GetManageID(3))
            {
                string sql = "select count(0) from memberinfo where placement=(select placement from memberinfo where number='" + number + "')";
                string sql2 = "select count(0) from memberinfo where placement='" + number + "')";

                int az1 = Convert.ToInt32(DBHelper.ExecuteScalar(sql));
                int az2 = Convert.ToInt32(DBHelper.ExecuteScalar(sql2));
                if (az1 + az2 > 3)
                {
                    aznum = 1;
                }
            }
            return aznum;
        }

        /// <summary>
        /// 获取会员注册的订单号
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string GetOrderID(string number)
        {
            string SQL = "select orderid from MemberOrder where number=@number and isagain=0";
            SqlParameter[] para =
            {
               new SqlParameter("@number",number)
             
            };
            return DBHelper.ExecuteScalar(SQL, para, CommandType.Text).ToString();
        }

        /// <summary>
        /// 获取会员注册的订单号
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public int GetDefrayState(string OrderId)
        {
            string SQL = "select top 1 defraystate from MemberOrder where OrderId=@OrderId";
            SqlParameter[] para =
            {
               new SqlParameter("@OrderId",OrderId)
             
            };
            return (int)DBHelper.ExecuteScalar(SQL, para, CommandType.Text);
        }

        /// <summary>
        /// 获取会员报单的支付方式
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public int GetDefrayType(string OrderId)
        {
            string SQL = "select top 1 defrayType from MemberOrder where OrderId=@OrderId";
            SqlParameter[] para =
            {
               new SqlParameter("@OrderId",OrderId)
             
            };
            return (int)DBHelper.ExecuteScalar(SQL, para, CommandType.Text);
        }

        /// <summary>
        /// 获取会员注册的店铺编号
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string GetStoreID(string number)
        {
            string SQL = "select storeid from MemberOrder where number=@number and isagain=0";
            SqlParameter[] para =
            {
               new SqlParameter("@number",number)
             
            };
            return DBHelper.ExecuteScalar(SQL, para, CommandType.Text).ToString();
        }

        /// <summary>
        /// 获取会员的注册期数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public int GetMemberRegisterQS(string number)
        {
            string SQL = "select ExpectNum from MemberInfo where number=@number";
            SqlParameter[] para =
            {
               new SqlParameter("@number",number)
             
            };
            return (int)DBHelper.ExecuteScalar(SQL, para, CommandType.Text);
        }

        /// <summary>
        /// 判断该会员是否有下级
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public int GetTuiJianCount(string number, bool isAnzhi)
        {
            string field = "Direct";
            if (isAnzhi)
            {
                field = "Placement";
            }
            string SQL = "select count(1) from MemberInfo where " + field + "=@number";
            SqlParameter[] para =
            {
               new SqlParameter("@number",number)
             
            };
            return (int)DBHelper.ExecuteScalar(SQL, para, CommandType.Text);
        }

        /// <summary>
        /// 删除新会员并且调整业绩
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="except">会员期数</param>
        public void DelNew(string number, int except, SqlTransaction tran)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Number",number),
                new SqlParameter("@ExpectNum",except)
            };
            DBHelper.ExecuteNonQuery(tran, "js_delnew", para, CommandType.StoredProcedure);
        }

        public void XunHuanTW(string number, int except, SqlTransaction tran)
        {

            //推荐
            string Direct = DBHelper.ExecuteScalar(tran, "select Direct from memberinfo where number='" + number + "'", CommandType.Text).ToString();
            //安置
            string Placement = DBHelper.ExecuteScalar(tran, "select Placement from memberinfo where number='" + number + "'", CommandType.Text).ToString();

            DataTable dttj = DBHelper.ExecuteDataTable(tran, "select number from memberinfo where Direct='" + number + "'");
            for (int i = 0; i < dttj.Rows.Count; i++)
            {
                //MemberInfoDAL.ChangeDirect(tran, dttj.Rows[i]["number"].ToString(), Direct);
                TempHistoryDAL.ExecuteUpdateNet(dttj.Rows[i]["number"].ToString(), number, Direct, 0, except, CommonModel.OperateBh, DateTime.Now.AddHours(Convert.ToDouble(System.Web.HttpContext.Current.Session["WTH"])));
            }

            DataTable dtaz = DBHelper.ExecuteDataTable(tran, "select number from memberinfo where Placement='" + number + "'");
            for (int i = 0; i < dtaz.Rows.Count; i++)
            {
                //MemberInfoDAL.ChangePlacement(tran, dtaz.Rows[i]["number"].ToString(), Placement);
                TempHistoryDAL.ExecuteUpdateNet(dtaz.Rows[i]["number"].ToString(), number, Placement, 1, except, CommonModel.OperateBh, DateTime.Now.AddHours(Convert.ToDouble(System.Web.HttpContext.Current.Session["WTH"])));
            }
        }

        /// <summary>
        /// 检查订单号是否存在
        /// </summary>
        public static bool CheckOrderIdExists(string orderId)
        {
            string SQL = "Select Count(OrderID) From memberOrder Where OrderID=@orderId";
            SqlParameter[] parm = 
            {
			  new SqlParameter("@orderId", orderId)
								  
            };

            int count = (int)DBHelper.ExecuteScalar(SQL, parm, CommandType.Text);

            return count > 0 ? true : false;
        }
        /// <summary>
        /// 检查订单号是否存在
        /// </summary>
        public static bool CheckOrderIdExists(SqlTransaction tran, string orderId)
        {
            string SQL = "Select count(0) From memberOrder Where OrderID=@orderId";
            SqlParameter[] parm = 
            {
                new SqlParameter("@orderId", orderId)
            };

            int count = (int)DBHelper.ExecuteScalar(tran, SQL, parm, CommandType.Text);

            return count > 0 ? true : false;
        }

        /// <summary>
        /// 更新店铺库存和钱
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="storeId"></param>
        public static int UpdateStockAndMoney(string orderId, string storeId, SqlTransaction tran)
        {
            SqlParameter[] para = 
            {
			  new SqlParameter("@orderID", orderId),
              new SqlParameter("@StoreID", storeId)			  
            };

            return DBHelper.ExecuteNonQuery("P_UPSotreStock", para, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static double GetNotEnoughMoney(string orderId, SqlTransaction tran)
        {
            double notEnough = 0;
            SqlParameter[] para = 
            {
			  new SqlParameter("@orderID", orderId)
            };
            SqlDataReader reader = DBHelper.ExecuteReader("[P_GetNotEnoughMoney]", para, CommandType.StoredProcedure);
            if (reader.Read())
            {
                notEnough = Convert.ToDouble(reader[0]);
            }
            reader.Close();
            return notEnough;
        }


        /// <summary>
        /// 查找报单明细
        /// </summary>
        /// <returns></returns>
        public static List<MemberDetailsModel> GetDetails(string orderId)
        {
            List<MemberDetailsModel> list = new List<MemberDetailsModel>();
            MemberDetailsModel memberDetailsModel = null;
            string SQL = "select * from MemberDetails where orderId=@orderId";
            SqlParameter[] para = 
            {
			  new SqlParameter("@orderId", orderId),
              		  
            };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);
            while (reader.Read())
            {
                memberDetailsModel = new MemberDetailsModel();
                memberDetailsModel.StoreId = reader["StoreId"].ToString();
                memberDetailsModel.Quantity = Convert.ToInt32(reader["Quantity"]);
                memberDetailsModel.ProductId = Convert.ToInt32(reader["ProductId"]);
                memberDetailsModel.NotEnoughProduct = Convert.ToInt32(reader["NotEnoughProduct"]);
                //memberDetailsModel.IsInGroupItemCount = Convert.ToInt32(reader["IsInGroupItemCount"]);
                //memberDetailsModel.IsGroupItem = reader["IsGroupItem"].ToString();
                //memberDetailsModel.HasGroupItem = reader["IsGroupItem"].ToString();
                memberDetailsModel.Price = Convert.ToDecimal(reader["Price"]);
                memberDetailsModel.Pv = Convert.ToDecimal(reader["pv"]);
                list.Add(memberDetailsModel);
            }
            reader.Close();
            reader.Dispose();
            return list;
        }

        /// <summary>
        /// 循环将更新库存
        /// </summary>
        /// <param name="stordId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <param name="notEnoughProduct"></param>
        /// <returns></returns>
        public static int UptStock(string stordId, int productId, int quantity, int notEnoughProduct)
        {
            string SQL = @"UPDATE stock SET TotalOut = TotalOut + @ProductCount, ActualStorage = ActualStorage - @ProductCount
		     WHERE ProductID = @ProductID And StoreID = @StoreID";
            SqlParameter[] para = 
            {
                new SqlParameter("@ProductCount", quantity),
                new SqlParameter("@notEnoughProduct", notEnoughProduct),
                new SqlParameter("@ProductID", productId),
                new SqlParameter("@StoreID", stordId)
            };
            return DBHelper.ExecuteNonQuery(SQL, para, CommandType.Text);
        }

        /// <summary>
        /// 循环将更新库存
        /// </summary>
        /// <param name="stordId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <param name="notEnoughProduct"></param>
        /// <returns></returns>
        public static int UptStock(SqlTransaction tran, string storeId, int productId, int quantity, int notEnoughProduct)
        {
            string SQL = null;
            int count2 = 0;
            if (notEnoughProduct < 0)
            {
                SQL = string.Format(@"UPDATE Stock SET  
                                      TotalOut = TotalOut +{0}, ActualStorage = ActualStorage -{1} WHERE ProductID = '{2}' And  StoreID = '{3}'", quantity, quantity, productId, storeId);
            }
            else
            {
                SQL = string.Format(@"UPDATE Stock SET  
                                         TotalOut = TotalOut +{0}, ActualStorage = ActualStorage -{1}+{4},inwaycount=inwaycount+{4}, LackTotalNumber=LackTotalNumber+{4}
                                         WHERE ProductID = '{2}' And  StoreID = '{3}'", quantity, quantity, productId, storeId, notEnoughProduct);
            }

            //            string SQL = @"UPDATE stock SET TotalOut = TotalOut + @ProductCount, ActualStorage = ActualStorage - @ProductCount
            //		     WHERE ProductID = @ProductID And StoreID = @StoreID";
            //            SqlParameter[] para = 
            //            {
            //              new SqlParameter("@ProductCount", quantity),
            //              new SqlParameter("@notEnoughProduct", notEnoughProduct),
            //              new SqlParameter("@ProductID", productId),
            //              new SqlParameter("@StoreID", stordId)

            //            };
            count2 = (int)DBHelper.ExecuteNonQuery(tran, SQL, null, CommandType.Text);
            return count2;
        }

        public int DelPayStatus(string orderId)
        {
            string sql = "delete from memberorder where orderId = @orderid";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@orderId",orderId)
            };
            return DBHelper.ExecuteNonQuery(sql, paras, CommandType.Text);
        }

        public string GetRemark(string orderid)
        {
            string sql = "select remark from memberorder where orderId = @orderid";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@orderId",orderid)
            };
            object obj = DBHelper.ExecuteScalar(sql, paras, CommandType.Text);
            if (obj == null)
                return "";
            else return obj.ToString();
        }

        public string GetMiaoShu(string orderid)
        {
            string sql = "select Description from storeOrder where StoreOrderID = @orderid";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@orderId",orderid)
            };
            object obj = DBHelper.ExecuteScalar(sql, paras, CommandType.Text);
            if (obj == null)
                return "";
            else return obj.ToString();
        }

        public string GetMiaoShu1(string orderid)
        {
            string sql = "select Description from OrderGoods where OrderGoodsID = @orderid";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@orderId",orderid)
            };
            object obj = DBHelper.ExecuteScalar(sql, paras, CommandType.Text);
            if (obj == null)
                return "";
            else return obj.ToString();
        }
    }
}