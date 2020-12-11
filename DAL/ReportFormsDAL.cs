using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class ReportFormsDAL
    {
        public class Item
        {
            public Item(string text, decimal value)
            {
                this._text = text;
                this._value = value;
            }

            private string _text;
            public string Text
            {
                get
                {
                    return _text;
                }
            }

            private decimal _value;
            public decimal Value
            {
                get
                {
                    return _value;
                }
            }
        }

        //根据产品编码得到产品
        /// <summary>
        /// 查询产品
        /// </summary>
        /// <param name="productcode"></param>
        /// <returns></returns>
        public static ProductModel GetProductItem(string productcode)
        {
            string sql = "select productid,productName,Commonprice,productcode,preferentialprice from product  where productcode=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
              new SqlParameter("@num",SqlDbType.VarChar,100)
            };
            sparams[0].Value = productcode;

            SqlDataReader dr = DBHelper.ExecuteReader(sql,sparams,CommandType.Text);

            dr.Read();

            ProductModel pm = new ProductModel();
            pm.ProductID = Convert.ToInt32(dr["productid"]);
            pm.ProductCode = dr["productcode"].ToString();
            pm.ProductName = dr["productName"].ToString();
            pm.CommonPrice = Convert.ToInt32(dr["Commonprice"]);
            pm.PreferentialPrice = Convert.ToInt32(dr["preferentialprice"]);
            dr.Close();
            return pm;
        }

        public static ArrayList ConstructData(int rbtn_typeSelectedIndex, string begin, string end, string ddl_itemSelectedValue)
        {
            int top = 9;
            bool need = true;
            decimal other = 0;
            string column = "";
            string result = "";
            DataTable table = new DataTable();
            ArrayList coll = new ArrayList();
            SqlParameter[] parm ={
									new SqlParameter("@BeginDate",SqlDbType.DateTime),
									new SqlParameter("@EndDate",SqlDbType.DateTime),
									new SqlParameter("@qishu",SqlDbType.Int)
								};
            parm[0].Value = begin;
            parm[1].Value = end;
            parm[2].Value = 0;
            if (rbtn_typeSelectedIndex == 0)
            {
                column = "storeid";

                table = DBHelper.ExecuteDataTable("D_order_getDatabystoreid", parm, CommandType.StoredProcedure);

                switch (ddl_itemSelectedValue)
                {
                    case "-1":
                        result = "Bdinghuo";
                        break;
                    case "0":
                        result = "BCheckOut";
                        break;
                    case "1":
                        result = "BNotCheckOut";
                        break;

                }

            }
            if (rbtn_typeSelectedIndex == 1)
            {

                column = "city";
                table = DBHelper.ExecuteDataTable("D_order_getDatabyArea", parm, CommandType.StoredProcedure);
                switch (ddl_itemSelectedValue)
                {
                    case "-1":
                        result = "Bdinghuo";
                        break;
                    case "0":
                        result = "BCheckOut";
                        break;
                    case "1":
                        result = "BNotCheckOut";
                        break;

                }

            }
            if (rbtn_typeSelectedIndex == 2)
            {

                column = "productname";
                table = DBHelper.ExecuteDataTable("D_OrderDetail_getData", parm, CommandType.StoredProcedure);
                switch (ddl_itemSelectedValue)
                {
                    case "-1":
                        result = "Bdinghuo";
                        break;
                    case "0":
                        result = "BCheckOut";
                        break;
                    case "1":
                        result = "BNotCheckOut";
                        break;

                }

            }
            int rows = table.Rows.Count;
            DataView dv = table.DefaultView;

            dv.Sort = result + "" + " Desc";

            if (top >= dv.Count)
            {
                need = false;
                top = dv.Count;
            }
            for (int i = 0; i < top; i++)
            {

                coll.Add(new Item(dv[i][column].ToString(), Convert.ToDecimal(dv[i][result].ToString())));
            }
            if (need)
            {
                for (int i = top; i < dv.Count; i++)
                {
                    other = other + Convert.ToDecimal(dv[i][result].ToString());
                }
                coll.Add(new Item("其它", other));
            }
            return coll;
        }
        //返回产品仓库明细
        public static DataTable ProductStockDetail_Company_Store(string productID, string flag)
        {
            DataTable dt = null;
            SqlParameter[] param ={
									 new SqlParameter("@ProductId",SqlDbType.VarChar)
								 };
            param[0].Value = productID;
            if (flag == "Company")
                dt = DBHelper.ExecuteDataTable("ProductQuantity_getdatabyproduct", param, CommandType.StoredProcedure);
            else if (flag == "Store")
                dt = DBHelper.ExecuteDataTable("S_Stock_getDatabyproductid", param, CommandType.StoredProcedure);
            return dt;

        }
        /// <summary>
        /// 订单汇总--店铺、省市
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetOrderCollect(string beginTime, string endTime, string mode)
        {
            DataTable dt = null;

            SqlParameter[] param ={
								  new SqlParameter("@BeginDate",SqlDbType.DateTime),
								  new SqlParameter("@EndDate",SqlDbType.DateTime),
								  new SqlParameter("@qishu",SqlDbType.Int)
							      };
            param[0].Value = beginTime;
            param[1].Value = endTime;
            param[2].Value = 0;

            if (mode == "store")
                dt = DBHelper.ExecuteDataTable("D_order_getDatabystoreid", param, CommandType.StoredProcedure);
            else if (mode == "city")
                dt = DBHelper.ExecuteDataTable("D_order_getDatabyArea", param, CommandType.StoredProcedure);
            else if (mode == "Country")
                dt = DBHelper.ExecuteDataTable("D_order_getDatabyCountry", param, CommandType.StoredProcedure);

            return dt;
        }

        /// <summary>
        /// 订单汇总--产品
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetOrderCollect_II(string beginTime, string endTime)
        {
            DataTable dt = null;

            SqlParameter[] param ={
								  new SqlParameter("@BeginDate",SqlDbType.DateTime),
								  new SqlParameter("@EndDate",SqlDbType.DateTime),
								  new SqlParameter("@qishu",SqlDbType.Int)
							      };
            param[0].Value = beginTime;
            param[1].Value = endTime;
            param[2].Value = 0;

            dt = DBHelper.ExecuteDataTable("D_OrderDetail_getData", param, CommandType.StoredProcedure);

            return dt;
        }

        /// <summary>
        /// 订单明细
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static DataTable GetOrderMingXi(string storeid, string beginTime, string endTime)
        {
            SqlParameter[] parm ={
									new SqlParameter("@Storeid",SqlDbType.VarChar),
									new SqlParameter("@BeginDate",SqlDbType.DateTime),
									new SqlParameter("@EndDate",SqlDbType.DateTime)
									
								};
            parm[0].Value = storeid;
            parm[1].Value = beginTime;
            parm[2].Value = endTime;

            DataTable dt = DBHelper.ExecuteDataTable("d_order_SumDetail", parm, CommandType.StoredProcedure);

            return dt;
        }

        /// <summary>
        /// 根据产品id获取产品名字
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static string GetProductName(string productID)
        {
            string rt = "";

            string Sql2 = "select Productname from product where productcode=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
              new SqlParameter("@num",SqlDbType.VarChar,100)
            };
            sparams[0].Value = productID;
            SqlDataReader sdr2 = DBHelper.ExecuteReader(Sql2,sparams, CommandType.Text);

            if (sdr2.Read())
            {
                rt = sdr2["productname"].ToString();
            }
            sdr2.Close();

            return rt;
        }

        /// <summary>
        /// 根据仓库id获取仓库名字
        /// </summary>
        /// <param name="wareHouseNameID"></param>
        /// <returns></returns>
        public static string GetWareHouseName(string wareHouseNameID)
        {
            string rt = "";

            string sql = "select WareHouseName from WareHouse where warehouseid=@num";
            SqlParameter[] sparams = new SqlParameter[]
            {
              new SqlParameter("@num",SqlDbType.Int)
            };
            sparams[0].Value = Convert.ToInt32(wareHouseNameID);
            SqlDataReader sdr = DBHelper.ExecuteReader(sql,sparams,CommandType.Text);

            if (sdr.Read())
            {
                rt = sdr["Warehousename"].ToString();
            }
            sdr.Close();

            return rt;
        }

        /// <summary>
        /// 获取库位名字
        /// </summary>
        /// <param name="depotSeatID"></param>
        /// <returns></returns>
        public static string GetDepotSeatName(string wareHouseID, string depotSeatID)
        {
            string rt = "";

            string sql3 = "select SeatName from DepotSeat where WareHouseID=@num and DepotSeatID=@num1";
            SqlParameter[] sparams = new SqlParameter[]
            {
              new SqlParameter("@num",SqlDbType.Int),
              new SqlParameter("@num1",SqlDbType.Int)
            };
            sparams[0].Value = Convert.ToInt32(wareHouseID);
            sparams[1].Value = Convert.ToInt32(depotSeatID);
            SqlDataReader sdr3 = DBHelper.ExecuteReader(sql3,sparams, CommandType.Text);

            if (sdr3.Read())
            {
                rt = sdr3["SeatName"].ToString();
            }
            sdr3.Close();

            return rt;

        }

        /// <summary>
        /// 产品销售明细
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSellMingXi(string beginTime, string endTime, string productID, string wareHouseID, string depotSeatID)
        {
            SqlParameter[] param ={
									new SqlParameter("@BeginDate",SqlDbType.DateTime),
									new SqlParameter("@EndDate",SqlDbType.DateTime),
									new SqlParameter("@Productid",SqlDbType.VarChar),
									new SqlParameter("@WareHouseid",SqlDbType.Int),
                                    new SqlParameter("@DepotSeatID",SqlDbType.Int)
								 };
            param[0].Value = beginTime;
            param[1].Value = endTime;
            param[2].Value = productID;
            param[3].Value = wareHouseID;
            param[4].Value = depotSeatID;

            DataTable dt = DBHelper.ExecuteDataTable("Opda_docDetail_getDetail", param, CommandType.StoredProcedure);

            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rbtn_typeSelectedIndex"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="ddl_itemSelectedValue"></param>
        /// <returns></returns>
        public static ArrayList ConstructData_II(string condition)
        {
            int top = 9;
            bool need = true;
            decimal other = 0;
            string column = "";
            string result = "";
            DataTable table = new DataTable();
            ArrayList coll = new ArrayList();
            SqlParameter[] param ={
									 new SqlParameter("@Storeid",SqlDbType.VarChar)
								 };

            param[0].Value = condition;

            column = "ProductName";

            table = DBHelper.ExecuteDataTable("D_Kucun_getData", param, CommandType.StoredProcedure);

            result = "TotalEnd";

            int rows = table.Rows.Count;
            DataView dv = table.DefaultView;

            dv.Sort = result + "" + " Desc";

            if (top >= dv.Count)
            {
                need = false;
                top = dv.Count;
            }
            for (int i = 0; i < top; i++)
            {

                coll.Add(new Item(dv[i][column].ToString(), Convert.ToDecimal(dv[i][result].ToString())));
            }
            if (need)
            {
                for (int i = top; i < dv.Count; i++)
                {
                    other = other + Convert.ToDecimal(dv[i][result].ToString());
                }
                coll.Add(new Item("其它", other));
            }
            return coll;
        }

        /// <summary>
        /// 按服务机构汇总
        /// </summary>
        /// <returns></returns>
        public static DataTable GetStoreCollect(string condition)
        {
            SqlParameter[] param ={
									 new SqlParameter("@Storeid",SqlDbType.VarChar)
								 };

            param[0].Value = condition;

            DataTable dt2 = DBHelper.ExecuteDataTable("D_Kucun_getData", param, CommandType.StoredProcedure);

            return dt2;
        }

        /// <summary>
        /// 仓库汇总
        /// </summary>
        /// <returns></returns>
        public static DataTable GetWareHouseCollect(string condition)
        {
            SqlParameter[] param ={
									 new SqlParameter("@WareHouseID",SqlDbType.Int)
								 };
            param[0].Value = condition;
            DataTable dt = DBHelper.ExecuteDataTable("ProductQuantity_get", param, CommandType.StoredProcedure);

            return dt;
        }
    }
}
