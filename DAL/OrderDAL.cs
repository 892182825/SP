/*-------------------2009-08-30------mark by 郑华超----------------------
 *  功能：报单
 * 流程：
 * 修改者：汪华
 * 修改时间：2009-08-31
 * */
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Model.Other;
using Model;
using System.Collections.Generic;
using Model.Orders;
namespace DAL
{
    public class OrderDAL
    {

        /// <summary>
        /// 得到新的报单号 形如：050108101
        /// </summary>
        /// <returns></returns>
        public string GetNewOrderID()
        {
            string orderId = MYDateTime.ToYYMMDDHHmmssString();



            bool sameflag = true;
            while (sameflag)
            {
                string sSQL = "Select Count(*) From MemberOrder Where OrderId=@orderId";
                SqlParameter[] para = {
									  new SqlParameter("@orderId",orderId )
								  };
                int result = (int)DBHelper.ExecuteScalar(sSQL, para, CommandType.Text);
                if (result > 0)
                {
                    orderId = MYDateTime.ToYYMMDDHHmmssString();
                }
                else
                    sameflag = false;
            }

            return orderId;
        }


        /// <summary>
        /// 检查订单号是否存在 三张表都要判断
        /// </summary>
        public static bool CheckOrderIdExists(string ddh)
        {
            string SQL = "Select Count(OrderID) From MemberOrder Where OrderID=@ddh";
            SqlParameter[] parm = {
									  new SqlParameter("@ddh", SqlDbType.VarChar,15)
								  };
            parm[0].Value = ddh;

            int count = (int)DBHelper.ExecuteScalar(SQL, parm, CommandType.Text);

            SQL = "Select Count(ordergoodsid) From ordergoods Where ordergoodsid=@ddh";
            count += (int)DBHelper.ExecuteScalar(SQL, parm, CommandType.Text);

            SQL = "Select Count(storeorderid) From storeorder Where storeorderid=@ddh";
            count += (int)DBHelper.ExecuteScalar(SQL, parm, CommandType.Text);

            return count > 0 ? true : false;
        }


        /// <summary>
        /// 得到店铺的报单明细
        /// </summary>		
        /// <param name="orderId">报单号</param>
        /// <returns></returns>
        public List<MemberDetailsModel> GetDetails2(string orderId)
        {
            List<MemberDetailsModel> MemberDetailsList = new List<MemberDetailsModel>();
            string SQL = "Select ProductID,Quantity,Price,Pv FROM MemberDetails WHERE OrderID=@OrderID";
            SqlParameter[] para = 
            {
                new SqlParameter("@OrderID",orderId )				  
            };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);

            while (reader.Read())
            {
                MemberDetailsModel memberDetailsModel = new MemberDetailsModel();
                memberDetailsModel.ProductId = Convert.ToInt32(reader["ProductId"]);
                memberDetailsModel.Quantity = Convert.ToInt32(reader["Quantity"]);

                MemberDetailsList.Add(memberDetailsModel);
            }
            reader.Close();
            reader.Dispose();
            return MemberDetailsList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public int MinusGroupCount(int ProductId, string orderId)
        {
            string sql = @"
            select isnull(sum(Quantity),0) from MemberDetails where ProductID in (
            select CombineProductID from ProductCombineDetail where SubProductID=@ProductId) and 
            orderId=@orderId";
            SqlParameter[] para = 
            {
              new SqlParameter("@ProductId",ProductId ),  
              new SqlParameter("@orderId",orderId )
            };
            return Convert.ToInt32(DBHelper.ExecuteScalar(sql, para, CommandType.Text));
        }

        public void AddNewSmallItemInStock(string storeId, OrderProduct3 opt, SqlTransaction tran)
        {
            string sql = @"declare @count22 int
                select @count22=count(*) from stock where storeId=@storeId and productId=@productId
                if(@count22=0)
                insert into stock( StoreID, ProductID, TotalIn, TotalOut,groupSmallStorage,ActualStorage, HasOrderCount)  values('xiaoxiao',@productId,0,0,@smallItemCount,-@act,0)";
            SqlParameter[] para = 
            {
              new SqlParameter("@productId",opt.Id ),  
              new SqlParameter("@smallItemCount",opt.Count ),
              new SqlParameter("@storeId",storeId ),
              new SqlParameter("@act",opt.Count ),
            };
            DBHelper.ExecuteNonQuery(tran, sql, para, CommandType.Text);
        }

        /// <summary>
        /// 对店铺订单的锁定与解锁
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public bool LuckOrUnlockStoreOrderByOrderID(string orderid)
        {
            bool flag = false;
            string sql = " update StoreOrder  set IsLock_YN=(ISNULL(IsLock_YN,0)+1)%2 where StoreOrderID=@StoreOrderID ";
            SqlParameter[] para = 
            {
              new SqlParameter("@StoreOrderID",SqlDbType.VarChar,20)
            };
            para[0].Value = orderid;
            var iVal = DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
            if (iVal > 0)
                flag = true;
            return flag;
        }

        /// <summary>
        /// 对会员订单的锁定与解锁
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public bool LuckOrUnlockMemberOrderByOrderID(string orderid)
        {
            bool flag = false;
            string sql = " update MemberOrder  set IsLock_YN=(ISNULL(IsLock_YN,0)+1)%2 where OrderID=@OrderID ";
            SqlParameter[] para = 
            {
              new SqlParameter("@OrderID",SqlDbType.VarChar,20)
            };
            para[0].Value = orderid;
            var iVal = DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
            if (iVal > 0)
                flag = true;
            return flag;
        }

        /// <summary>
        /// 得到会员的报单明细
        /// </summary>		
        /// <param name="orderId">报单号</param>
        /// <returns></returns>
        public List<MemberOrderDetailsEntity> GetMemberOrderDetailsEntity(string orderId)
        {
            List<MemberOrderDetailsEntity> MemberDetailsList = new List<MemberOrderDetailsEntity>();
            string SQL = "select * from V_MemberOrderDetails t where OrderID = @OrderID";
            SqlParameter[] para = 
            {
              new SqlParameter("@OrderID",orderId )				  
            };
            SqlDataReader reader = DBHelper.ExecuteReader(SQL, para, CommandType.Text);

            while (reader.Read())
            {
                MemberOrderDetailsEntity memberDetailsModel = new MemberOrderDetailsEntity();
                memberDetailsModel.ID = Convert.ToInt32(reader["ID"]);
                memberDetailsModel.OrderID = reader["OrderID"].ToString();
                memberDetailsModel.ProductCode = Convert.ToString(reader["ProductCode"]);
                memberDetailsModel.ProductID = Convert.ToInt32(reader["ProductID"]);
                memberDetailsModel.ProductName = reader["ProductName"].ToString();
                memberDetailsModel.Quantity = Convert.ToInt32(reader["Quantity"]);
                memberDetailsModel.QuantityReturned = Convert.ToInt32(reader["QuantityReturned"]);
                memberDetailsModel.UnitPrice = Convert.ToDouble(reader["UnitPrice"]);
                memberDetailsModel.UnitPV = Convert.ToDouble(reader["UnitPV"]);
                memberDetailsModel.UseAll = true;
                //默认赋剩余可用数量
                memberDetailsModel.UseQuantity = (Convert.ToInt32(reader["Quantity"]) - Convert.ToInt32(reader["QuantityReturned"]));
                MemberDetailsList.Add(memberDetailsModel);
            }
            reader.Close();
            reader.Dispose();
            return MemberDetailsList;
        }

    }
}