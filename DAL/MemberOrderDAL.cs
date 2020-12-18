using System;
using System.Collections.Generic;
using System.Collections;
using Model;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

/*
 * 创建者：     郭恒
 * 创建时间：   2009-09-12
 * 功能：       会员报单数据访问层
 */

namespace DAL
{
    public class MemberOrderDAL
    {
        /// <summary>
        /// 根据店铺ID获取该店铺的用户订单表
        /// </summary>
        /// <param name="storeID">店铺ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="key"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public DataTable GetMemberOrderListByStoreID(string storeID, int pageIndex, string key,
                int pageSize, string condition, out int recordCount, out int pageCount)
        {
            DataTable dt = new DataTable();
            recordCount = 0;
            pageCount = 0;
            SqlParameter[] parm0 = {new SqlParameter("@PageIndex",SqlDbType.Int),
                                       new SqlParameter("@PageSize",SqlDbType.Int),
                                       new SqlParameter("@table",SqlDbType.VarChar,1000),
                                       new SqlParameter("@columns",SqlDbType.NVarChar,2000),
                                       new SqlParameter("@condition",SqlDbType.NVarChar,2000),
                                       new SqlParameter("@key",SqlDbType.VarChar,50),
                                       new SqlParameter("@RecordCount",SqlDbType.Int),
                                       new SqlParameter("@PageCount",SqlDbType.Int)
                                   };

            parm0[0].Value = pageIndex;
            parm0[1].Value = pageSize;
            parm0[2].Value = "MemberOrder as MO inner join MemberInfo as MI on(MO.Number=MI.Number)";
            parm0[3].Value = @"MO.id,MO.OrderExpectNum,case when MO.DefrayState = 0 then '未支付'   when  MO.DefrayState = 1
                                then '已支付'  else '未知'  end as PayStatus,case when MO.defraytype=1 then '现金'  when 
                                MO.defraytype = 2 then '电子转帐'  when  MO.defraytype = 3 then '快钱支付'when  MO.defraytype = 4 
                                then '银行汇款'  else '未知'  end as defrayname,MO.defraytype,MO.StoreID,MO.OrderID,MI.Number,MI.Name,Mo.TotalMoney,MO.TotalPv";
            parm0[4].Value = condition + " and MO.storeID='" + storeID + "'";
            parm0[5].Value = key;
            parm0[6].Value = recordCount;
            parm0[7].Value = pageCount;

            parm0[6].Direction = System.Data.ParameterDirection.Output;
            parm0[7].Direction = System.Data.ParameterDirection.Output;


            dt = DBHelper.ExecuteDataTable("GetCustomersDataPage", parm0, CommandType.StoredProcedure);
            recordCount = Convert.ToInt32(parm0[6].Value);
            pageCount = Convert.ToInt32(parm0[7].Value);


            return dt;
        }

        public DataTable GetMemberByOrderID(string orderid,int currency)
        {
            string sql = "select h.RegisterDate,o.OrderExpectNum,o.PayExpectNum,o.OrderID,o.defraytype,o.Number,o.TotalMoney*"+currency+" as TotalMoney ,o.StoreID,o.Totalpv,o.OrderDate,h.Name,o.defraystate ,o.ordertype " +
                         " from memberorder o,memberinfo h  " +
                         " where o.number=h.number and o.orderid=@OrderID";
            SqlParameter[] parms = { new SqlParameter("@OrderID", SqlDbType.VarChar, 40) };
            parms[0].Value = orderid;
            return DBHelper.ExecuteDataTable(sql, parms, CommandType.Text);
        }

        public DataTable GetMemberByOrderID1(string orderid,double currency)
        {
            string sql = "select h.RegisterDate,o.OrderExpectNum,o.PayExpectNum,o.OrderID,o.defraytype,o.Number,o.TotalMoney*"+ currency + " as TotalMoney ,o.StoreID,o.Totalpv,o.OrderDate,h.Name,o.defraystate ,o.ordertype " +
                         " from memberorder o,memberinfo h  " +
                         " where o.number=h.number and o.orderid=@OrderID";
            SqlParameter[] parms = { new SqlParameter("@OrderID", SqlDbType.VarChar, 40) };
            parms[0].Value = orderid;
            return DBHelper.ExecuteDataTable(sql, parms, CommandType.Text);
        }
        /// <summary>
        /// 通过国家countryCode获取银行名称
        /// </summary>
        /// <param name="CountryCode"></param>
        /// <returns></returns>
        public static DataTable GetBankName(int CountryCode)
        {
            string sql = "select BankCode,BankName from MemberBank m,Country c where m.CountryCode=c.ID and c.CountryCode= @CountryCode";
            SqlParameter[] parms = { new SqlParameter("@CountryCode",CountryCode) };
            parms[0].Value = CountryCode;
            return DBHelper.ExecuteDataTable(sql,parms,CommandType.Text);
        }




        /// <summary>
        /// 查看会员订单明细
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public DataTable GetMemberDetailsByOrderID(string orderid)
        {
            string sql = "select m.productid,m.orderid,m.storeid,m.Quantity,m.Price ,m.Pv,m.Quantity*m.Price as TotalMoney ,p.ProductName,ptype.ProductTypeName" +
                         " from memberdetails m,product  p,producttype ptype " +
                         " where m.ProductID=p.ProductID and p.producttypeid=ptype.producttypeid and orderid=@OrderID";
            SqlParameter[] parms = { new SqlParameter("@OrderID", SqlDbType.VarChar, 40) };
            parms[0].Value = orderid;
            return DBHelper.ExecuteDataTable(sql, parms, CommandType.Text);
        }
        /// <summary>
        /// 查看会员订单明细
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public DataTable GetMemberDetailsByOrderID(string orderid,double currency)
        {
            string sql = "select m.productid,m.orderid,m.storeid,m.Quantity,m.Price*" + currency + "as Price ,m.Pv,m.Quantity*m.Price*" + currency + " as TotalMoney ,p.ProductName,ptype.ProductTypeName" +
                         " from memberdetails m,product  p,producttype ptype " +
                         " where m.ProductID=p.ProductID and p.producttypeid=ptype.producttypeid and orderid=@OrderID";
            SqlParameter[] parms = { new SqlParameter("@OrderID", SqlDbType.VarChar, 40) };
            parms[0].Value = orderid;
            return DBHelper.ExecuteDataTable(sql, parms, CommandType.Text);
        }


        public DataTable GetMemberDetailsByOrderID1(string orderid,double currency)
        {
            string sql = "select m.productid,m.orderid,m.storeid,m.Quantity,m.Price*"+ currency + " as Price,m.Pv,m.Quantity*m.Price*" + currency + " as TotalMoney ,p.ProductName,ptype.ProductTypeName" +
                         " from memberdetails m,product  p,producttype ptype " +
                         " where m.ProductID=p.ProductID and p.producttypeid=ptype.producttypeid and orderid=@OrderID";
            SqlParameter[] parms = { new SqlParameter("@OrderID", SqlDbType.VarChar, 40) };
            parms[0].Value = orderid;
            return DBHelper.ExecuteDataTable(sql, parms, CommandType.Text);
        }
        #region 获取序号范围
        /// <summary>
        /// 求某编号的序号范围 ： 
        /// </summary>
        /// <param name="bianhao">编号</param>
        /// <param name="anzhi">null为推荐，否则为安置</param>
        /// <param name="qishu">期数</param>
        /// <param name="sXuhao">起始序号</param>
        /// <param name="eXuhao">终止序号</param>
        public void getXHFW(string bianhao, bool isAnZhi, int qishu, out int sXuhao, out int eXuhao, out int Cengwei)
        {
            string myXuhao, myCengwei;
            Cengwei = 9999;
            if (isAnZhi)
            {
                myXuhao = "Ordinal1";
                myCengwei = "LayerBit1";
            }
            else
            {
                myXuhao = "Ordinal2";
                myCengwei = "LayerBit2";
            }
            //获取最大序号
            object maxNum = DBHelper.ExecuteScalar("SELECT MAX(" + myXuhao + ") as mShu FROM MemberInfoBalance" + qishu.ToString(), CommandType.Text);
            if (maxNum == System.DBNull.Value)
            {
                eXuhao = 0;
            }
            else
            {
                eXuhao = Convert.ToInt32(maxNum);
            }

            sXuhao = eXuhao + 1;
            //获取输入会员的层位和序号
            SqlDataReader dr;
            dr = DBHelper.ExecuteReader("SELECT   isnull(" + myCengwei + ",0)  as  " + myCengwei + " , isnull(" + myXuhao + ",0)  as  " + myXuhao + " FROM MemberInfoBalance" + qishu.ToString() + " WHERE number = '" + bianhao + "'", CommandType.Text);
            if (dr.Read())
            {
                Cengwei = Convert.ToInt32(dr[myCengwei]);
                sXuhao = Convert.ToInt32(dr[myXuhao]);
            }
            dr.Close();

            //确定终止序号
            int lsXuhao = Convert.ToInt32(DBHelper.ExecuteScalar("SELECT " + myXuhao + " FROM MemberInfoBalance" + qishu.ToString() + " WHERE " + myCengwei + "<=" + Cengwei.ToString() + " AND " + myXuhao + ">" + sXuhao.ToString() + " ORDER BY " + myXuhao + " ASC", CommandType.Text));
            if (lsXuhao > 0)
            {
                eXuhao = lsXuhao - 1;
            }

        }
        #endregion

        /// <summary>
        /// 查询报单个数
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static int GetOrderCount(string orderid)
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar("select count(0) from memberorder where orderid=@orderid ",new SqlParameter[]{new SqlParameter("@orderid",orderid)},CommandType.Text));
        }

        public static MemberOrderModel GetMemberOrder(string orderId)
        {
            MemberOrderModel model = null;
            string sql = "select InvestJB,PriceJB ,Number,OrderId,StoreId,TotalMoney,TotalPv,CarryMoney,OrderExpectNum,PayExpectNum,IsAgain,Error,Remark,DefrayState," +
                "Consignee,CCPCCode ,ConZipCode,ConTelPhone,ConMobilPhone," +
                "ConPost,DefrayType,PayMoney,PayCurrency,StandardCurrency,StandardcurrencyMoney," +
                "OperateIp,OperateNum,RemittancesId,ElectronicaccountId,OrderType,IsreceiVables,PaymentMoney,ReceivablesDate, conaddress,lackproductmoney,sendway,isnull(city.country,'') country,isnull(city.province,'') province,isnull(city.city,'') city,isnull(city.xian,'') xian  from MemberOrder left join city on memberorder.CCPCCode=city.CPCCode where orderId= @orderId";
            SqlParameter para = new SqlParameter("@orderId", orderId);
            SqlDataReader dr = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            if (dr.Read())
            {
                model = new MemberOrderModel();
                model.Number = dr["Number"].ToString();
                model.OrderId = dr["OrderId"].ToString();
                model.StoreId = dr["StoreId"].ToString();
                model.TotalMoney = Convert.ToDecimal(dr["TotalMoney"]);
                model.TotalPv = Convert.ToDecimal(dr["TotalPv"]);
                model.CarryMoney = Convert.ToDecimal(dr["CarryMoney"]);
                model.OrderExpect = Convert.ToInt32(dr["OrderExpectNum"]);
                model.PayExpect = Convert.ToInt32(dr["PayExpectNum"]);
                model.IsAgain = Convert.ToInt32(dr["IsAgain"]);
                model.Err = dr["Error"].ToString();
                model.Remark = dr["Remark"].ToString();
                model.DefrayState = Convert.ToInt32(dr["DefrayState"]);
                model.Consignee = dr["Consignee"].ToString();
                model.CCPCCode = dr["CCPCCode"].ToString();
                model.ConZipCode = dr["ConZipCode"].ToString();
                model.ConTelPhone = dr["ConTelPhone"].ToString();
                model.ConMobilPhone = dr["ConMobilPhone"].ToString();
                model.ConPost = dr["ConPost"].ToString();
                model.DefrayType = Convert.ToInt32(dr["DefrayType"]);
                //model.IsRetail = dr.GetInt32(19);
                //model.DeclareMoney = dr.GetDecimal(20);
                model.PayMoney = Convert.ToDecimal(dr["PayMoney"]);
                model.PayCurrency = Convert.ToInt32(dr["PayCurrency"]);
                model.StandardCurrency = Convert.ToInt32(dr["StandardCurrency"]);
                model.StandardcurrencyMoney = Convert.ToDecimal(dr["StandardcurrencyMoney"]);
                model.OperateIp = dr["OperateIp"].ToString();
                model.OperateNumber = dr["OperateNum"].ToString();
                //model.Type = dr.GetInt32(27);
                model.RemittancesId = dr["RemittancesId"].ToString();
                model.ElectronicaccountId = dr["ElectronicaccountId"].ToString();
                model.OrderType = Convert.ToInt32(dr["OrderType"]);
                model.IsreceiVables = Convert.ToInt32(dr["IsreceiVables"]);
                model.PaymentMoney = Convert.ToDecimal(dr["PaymentMoney"]);
                model.ReceivablesDate =Convert.ToDateTime( dr["ReceivablesDate"] );
                model.ConAddress = dr["ConAddress"].ToString();
                model.LackProductMoney = Convert.ToDecimal(dr["LackProductMoney"]);
                model.SendWay = Convert.ToInt32(dr["SendWay"]);
                model.ConCity.Country = dr["country"].ToString();
                model.ConCity.Province = dr["province"].ToString();
                model.ConCity.City = dr["City"].ToString();
                model.ConCity.Xian = dr["Xian"].ToString();
                model.InvestJB = Convert.ToDecimal((dr["InvestJB"] == DBNull.Value ? 0 : dr["InvestJB"]));
                model.PriceJB = Convert.ToDecimal((dr["PriceJB"] == DBNull.Value ? 0 : dr["PriceJB"]));
                    

            }
            dr.Close();
            return model;
        }

        //使用本地账户支付
        public static int  PayOrder(string number, string orderid, double aneed, double bneed, double cneed ,double eneed,int lv)
        {
            int res = 0;
             
            SqlTransaction tran = null;
            SqlConnection conn = null;
            using (conn = DBHelper.SqlCon()) {
                conn.Open();
                tran = conn.BeginTransaction();

                if (aneed > 0 || bneed > 0 || cneed > 0)
                {
                    //修改会员账户
                    int r = DBHelper.ExecuteNonQuery(tran, "update memberinfo set  pointAout=pointAOut+" + aneed + " ,pointBout=pointBout+" + bneed + " ,pointCout=pointCout+" + cneed + " ,levelint=" + lv + "  where number='" + number + "'");
                   //更新销毁字段 
                    DBHelper.ExecuteNonQuery(tran, "update CoinPlant set  CoinDestroy=CoinDestroy+" + aneed + "   where CoinIndex='CoinA' ;update CoinPlant set  CoinDestroy=CoinDestroy+" + bneed + "   where CoinIndex='CoinB' ;update CoinPlant set  CoinDestroy=CoinDestroy+" + cneed + "   where CoinIndex='CoinC' ;update CoinPlant set   CoinDestroy=CoinDestroy+" + eneed + "   where CoinIndex='CoinE' ;");

                    if (r == 0) tran.Rollback();
                }
                else {
                    //修改会员账户
                    int r = DBHelper.ExecuteNonQuery(tran, "update memberinfo set  levelint=" + lv + "  where number='" + number + "'");
                    if (r == 0) tran.Rollback();
                }

                string ddremark = "支付订单:";
                //插入对账单
                if (aneed > 0) {
                    ddremark += "A币支付  "+ aneed ;
                int c=    D_AccountDAL.AddAccount("A",number,aneed,D_AccountSftype.MemberType, D_AccountKmtype.Declarations,DirectionEnum.AccountReduced,"购买矿机支付",tran);
                    if (c == 0) tran.Rollback();
                }
                if (bneed > 0)
                {
                    ddremark += "  B币支付  " + bneed;
                    int c = D_AccountDAL.AddAccount("B", number, bneed, D_AccountSftype.MemberType, D_AccountKmtype.Declarations, DirectionEnum.AccountReduced, "购买矿机支付,订单号"+orderid, tran);
                    if (c == 0) tran.Rollback();
                }
                if (cneed > 0)
                {
                    ddremark += "  C币支付  " + cneed;
                    int c = D_AccountDAL.AddAccount("C", number, cneed, D_AccountSftype.MemberType, D_AccountKmtype.Declarations, DirectionEnum.AccountReduced, "购买矿机支付,订单号" + orderid, tran);
                    if (c == 0) tran.Rollback();
                }
                //修改订单状态
               int rr=  DBHelper.ExecuteNonQuery(tran, "update  memberorder set  DefrayState=1   where orderid='" + orderid+"' ");
                if (rr == 1)
                {
                    tran.Commit();
                    res =1;
                }
                else tran.Rollback();

            }
            return res;
            
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static MemberInfoModel GetMemberInfo(string orderId)
        {
            MemberInfoModel model = null;
            string sql = @"select a.Number,a.Placement,a.Direct,b.TotalPv,a.ExpectNum 
                        from memberinfo a,memberorder b 
                        where a.orderid=b.orderId and  a.orderid=@orderId";
            SqlParameter para = new SqlParameter("@orderId", orderId);
            SqlDataReader dr = DBHelper.ExecuteReader(sql, para, CommandType.Text);
            if (dr.Read()) 
            {
                model = new MemberInfoModel();
                model.Number = dr.GetString(0);
                model.Placement = dr.GetString(1);
                model.Direct = dr.GetString(2);
                model.TotalPv = dr.GetDecimal(3);
                model.ExpectNum = dr.GetInt32(4);
            }
            dr.Close();
            return model;
        }


        public static DataSet GetAllList(string productid)
        {
            string sql = "";
                      
            if (productid != "")
            {
                string pid = productid+",";
                string pidstr = productid + ",";
                while (1 == 1)
                {
                    DataTable dt = DBHelper.ExecuteDataTable("select productid from product where pid in (" + pidstr.Substring(0, pidstr.Length - 1).ToString() + ") and issell=0");
                    pidstr = "";
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            pidstr = pidstr + dt.Rows[i][0].ToString() + ",";
                            pid = pid + dt.Rows[i][0].ToString() + ",";
                        }
                    }
                    else
                    {
                        break;
                    }

                }


                sql = @"select productcode,ProductID,ProductName,CommonPrice,CommonPV,PreferentialPrice,PreferentialPV,Description,ProductImage,
currency.name as currency from Product,currency where Product.currency=currency.id and Product.isfold=0 and Product.pid in (" + pid.Substring(0, pid.Length - 1).ToString() + ")  and Product.issell=0 and (Product.Yongtu=0 or Product.Yongtu=2)";
            }
            else
            {
                sql = @"select productcode,ProductID,ProductName,CommonPrice,CommonPV,PreferentialPrice,PreferentialPV,Description,ProductImage,
currency.name as currency from Product,currency where Product.currency=currency.id and Product.isfold=0  and Product.issell=0 and (Product.Yongtu=0 or Product.Yongtu=2)";
            }

            SqlDataAdapter da = DBHelper.ExecuteDataAdapter(sql,CommandType.Text);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 获取会员报单
        /// </summary>
        /// <param name="member">会员ID</param>
        /// <param name="flag">0：注册，1：复消</param>
        /// <returns></returns>
        public static DataTable GetMemberOrder(string member, string flag)
        {
            return DBHelper.ExecuteDataTable("select orderid from memberorder where number=@num and isagain=@flag", new SqlParameter[] { new SqlParameter("@num", member), new SqlParameter("@flag", flag) }, CommandType.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static MemberInfoModel GetMemberInfo(string orderId, SqlTransaction tran)
        {
            MemberInfoModel model = null;
//            string sql = @"select top 1 a.Number,a.Placement,a.Direct,b.TotalPv,a.ExpectNum 
//                        from memberinfo a,memberorder b 
//                        where a.orderid=b.orderId and  a.orderid=@orderId";
            string sql = "select top 1 a.Number,a.Placement,a.Direct,b.TotalPv,a.ExpectNum from memberinfo a,memberorder b  where a.orderid=b.orderId and  a.orderid=@orderId";
            SqlParameter[] para = {
                                      new SqlParameter("@orderId",orderId)
                                  };
            DataTable dt = DBHelper.ExecuteDataTable(tran, sql, para, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                model = new MemberInfoModel();
                model.Number = dt.Rows[0]["number"].ToString();
                model.Placement = dt.Rows[0]["Placement"].ToString();
                model.Direct = dt.Rows[0]["Direct"].ToString();
                model.TotalPv = Convert.ToDecimal(dt.Rows[0]["TotalPv"].ToString());
                model.ExpectNum = Convert.ToInt32(dt.Rows[0]["ExpectNum"].ToString());
            }
            //SqlDataReader dr = DBHelper.ExecuteReader(tran,sql,para,CommandType.Text);
            //if (dr.Read())
            //{
            //    model = new MemberInfoModel();
            //    model.Number = dr["number"].ToString();
            //    model.Placement = dr["Placement"].ToString();
            //    model.Direct = dr["Direct"].ToString();
            //    model.TotalPv = Convert.ToDecimal(dr["TotalPv"].ToString());
            //    model.ExpectNum = Convert.ToInt32(dr["ExpectNum"].ToString());
            //}
            //dr.Close();
            return model;
        }

        /// <summary>
        /// 确认支付
        /// </summary>
        /// <param name="PayMentMoney">金额</param>
        /// <param name="ShouDateTime">收款日期</param>
        /// <param name="OrderID">店编号</param>
        /// <returns></returns>
        public static bool Batch(double PayMentMoney, DateTime ShouDateTime, string OrderID)
        {
            string sql = "update MemberOrder set IsReceivables=1,PayMentMoney=@Money, ReceivablesDate=@Date where OrderID=@OrderID";
            SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@Money",SqlDbType.Decimal),
                new SqlParameter("@Date",SqlDbType.DateTime),
                new SqlParameter("OrderID",SqlDbType.VarChar,50)
            };
            parm[0].Value = PayMentMoney;
            parm[1].Value = ShouDateTime;
            parm[2].Value = OrderID;
            int num = DBHelper.ExecuteNonQuery(sql, parm, CommandType.Text);
            if (num > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 是否足够支付订单
        /// </summary>
        /// <param name="OrderID">订单号</param>
        /// <param name="OrderID">店编号</param>
        /// <returns></returns>
        public static bool IsAdequate(string OrderID, string storeId)
        {
            //string sql = "select MemberDetails.quantity*MemberDetails.Price as totalmoney from MemberDetails,product where MemberDetails.ProductID = product.ProductID and MemberDetails.OrderID = @orderId and MemberDetails.StoreID=@storeId";
            //SqlParameter[] parm = new SqlParameter[] { new SqlParameter("@orderId",OrderID),new SqlParameter("@storeId",storeId)};
            //parm[0].Value = OrderID;
            //parm[1].Value = storeId;
            //object obj = DBHelper.ExecuteScalar(sql,parm,CommandType.Text);
            string sql1 = "select totalMoney,PayMentMoney from MemberOrder where OrderID=@OrderID";
            SqlParameter[] parm1 = new SqlParameter[] { new SqlParameter("@orderId", OrderID) };
            parm1[0].Value = OrderID;
            SqlDataReader reader = DBHelper.ExecuteReader(sql1, parm1, CommandType.Text);
            double totalMoney = 0;
            double PayMentMoney = 0;
            if (reader.Read())
            {
                totalMoney = double.Parse(reader[0].ToString());
                PayMentMoney = double.Parse(reader[1].ToString());
            }
            reader.Close();
            if (totalMoney == PayMentMoney)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 根据报单编号，获取店铺编号
        /// </summary>
        /// <param name="orderid">报单编号</param>
        /// <returns>店铺编号</returns>
        public static string GetStoreIdByOrderId(string orderid)
        {
            return DBHelper.ExecuteScalar("select storeid from memberorder where orderid=@orderid ", new SqlParameter[] { new SqlParameter("@orderid", orderid) }, CommandType.Text).ToString();
        }


        /// <summary>
        /// 判断是否是协助人支付或店铺支付
        /// </summary>
        /// <param name="loginnumber"></param>
        /// <returns></returns>
        public static bool Getvalidteiscanpay(string odid, string loginnumber)
        {
            string odnumber = "";
            string sqlst = "select number from memberorder where orderid='" + odid + "' ";
            object oc = DBHelper.ExecuteScalar(sqlst);
            if (oc == null) return true;
            else odnumber = oc.ToString();
            string str = "select  count(0) from memberinfo where  number ='" + odnumber + "' and   (direct='" + loginnumber + "'  or '" + odnumber + "'='" + loginnumber + "' )  ";

            int qc = Convert.ToInt32(DBHelper.ExecuteScalar(str));

            return qc == 0;

        }




        /// <summary>
        /// 超时未支付删除会员信息
        /// </summary>
        public static void clearouttimenopay()
        {

            string time = System.Configuration.ConfigurationManager.AppSettings["outtimenorit"].ToString();

            string sproc = "Clearuttimenopay";
            SqlParameter[] sps = new SqlParameter[] {
              new SqlParameter("@outtime",time),
             
            };
            DBHelper.ExecuteNonQuery(sproc, sps, CommandType.StoredProcedure);
        }

    }
}
