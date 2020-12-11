using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Model;
using System.Data;

namespace DAL
{
    /// <summary>
    /// 公司逻辑库存
    /// </summary>
    public class LogicProductInventoryDAL
    {
        public static bool UpdateToatlOut(SqlTransaction tr, IList<OrderDetailModel> orderdetails)
        {
            bool state = true;
            foreach (OrderDetailModel orderdetail in orderdetails)
            {
                string sql = "update LogicProductInventory set TotalOut=TotalOut+@count where ProductID=@productid";
                SqlParameter[] paras = new SqlParameter[2];
                paras[0] = new SqlParameter("@count", orderdetail.Quantity);
                paras[1] = new SqlParameter("@productid", orderdetail.ProductId);

                if (DBHelper.ExecuteNonQuery(tr, sql, paras, CommandType.Text) != 1)
                {
                    state = false;
                }
            }

            return state;
        }

        /// <summary>
        /// 验证公司逻辑库存是否足够
        /// </summary>
        /// <param name="orders">订单列表</param>
        /// <returns></returns>
        public static bool SelectProductNum(IList<StoreOrderModel> orders)
        {
            bool state = true;
            string sql = "select productid,sum(quantity) as Quantity from ordergoodsdetail  where ordergoodsid in (''";
            foreach (StoreOrderModel order in orders)
            {
                sql += ",'" + order.StoreorderId + "'";
            }
            sql += ") group by productid ";
            DataTable dt = DBHelper.ExecuteDataTable(sql);

            foreach (DataRow dr in InventoryDocDAL.GetNewOrderDetail(dt).Rows)
            {
                if (DBHelper.ExecuteScalar("select count(0) from LogicProductInventory where productid=@pid", new SqlParameter[] { new SqlParameter("@pid", dr["productid"].ToString()) }, CommandType.Text).ToString() == "0")
                {
                    state = false;
                }
                else if (Convert.ToInt32(DBHelper.ExecuteScalar("select isnull((totalin-totalout),0) from LogicProductInventory where productid=@pid", new SqlParameter[] { new SqlParameter("@pid", dr["productid"].ToString()) }, CommandType.Text)) < Convert.ToInt32(dr["Quantity"]))
                {
                    state = false;
                }
            }

            return state;
        }

        public static bool DelToatlOut(SqlTransaction tr, string orderid)
        {
            bool state = true;
            IList<OrderDetailModel> orderdetails = OrderDetailDAL.GetOrderDetail(orderid);
            foreach (OrderDetailModel orderdetail in orderdetails)
            {
                string sql = "update LogicProductInventory set TotalOut=TotalOut-@count where ProductID=@productid";
                SqlParameter[] paras = new SqlParameter[2];
                paras[0] = new SqlParameter("@count", orderdetail.Quantity);
                paras[1] = new SqlParameter("@productid", orderdetail.ProductId);

                if (DBHelper.ExecuteNonQuery(tr, sql, paras, CommandType.Text) != 1)
                {
                    state = false;
                }
            }

            return state;
        }

        public static bool UpdateToatlOut(SqlTransaction tr, string orderid, int productid, int quantity)
        {
            bool state = true;

            string sql = "update LogicProductInventory set TotalOut=TotalOut+@count where ProductID=@productid";
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@count", quantity);
            paras[1] = new SqlParameter("@productid", productid);

            if (DBHelper.ExecuteNonQuery(tr, sql, paras, CommandType.Text) != 1)
            {
                state = false;
            }

            return state;
        }

    }
}
