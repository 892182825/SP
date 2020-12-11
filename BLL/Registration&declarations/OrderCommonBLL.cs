using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

//Add Namespace
using Model;
using Model.Other;
using DAL;
using BLL.CommonClass;
using DAL.Other;

/*
 * 修改者：汪华
 * 修改时间：2009-09-01
 */

namespace BLL.Registration_declarations
{
    public class OrderCommonBLL
    {

        CommonDataBLL commonDataBLL = new CommonDataBLL();
        private const string PARAM_Bianhao = "@Number";//
        private const string PARAM_OrderID = "@OrderID";
        private const string PARAM_StoreID = "@StoreID";
        private const string PARAM_TotalMoney = "@TotalMoney";
        private const string PARAM_TotalPv = "@TotalPv";
        private const string PARAM_Qishu = "@OrderExpect";//期数
        private const string PARAM_GaiQishu = "@PayExpect";//
        private const string PARAM_isFuxiao = "@IsAgain ";//
        private const string PARAM_OrderDate = "@OrderDate";
        private const string PARAM_Err = "@Err";
        private const string PARAM_isConfirm = "@isConfirm";


        private const string PARAM_ProductID = "@ProductID";
        private const string PARAM_Quantity = "@Quantity";
        private const string PARAM_Price = "@Price";
        private const string PARAM_Pv = "@Pv";
        private const string PARAM_Remark = "@Remark";

        private const string PARAM_SureFare = "@SureFare";
        private const string PARAM_DefrayState = "@DefrayState";
        private const string PARAM_AuditingState = "@AuditingState";
        private const string PARAM_AffluxBank = "@AffluxBank";
        private const string PARAM_AffluxAccount = "@AffluxAccount";
        private const string PARAM_Consignee = "@Consignee";
        private const string PARAM_ConCountry = "@ConCountry";
        private const string PARAM_ConProvince = "@ConProvince";
        private const string PARAM_ConCity = "@ConCity";
        private const string PARAM_ConAddress = "@ConAddress";
        private const string PARAM_ConZipCode = "@ConZipCode";
        private const string PARAM_ConTelphone = "@ConTelphone";
        private const string PARAM_ConMobilPhone = "@ConMobilPhone";
        private const string PARAM_ConPost = "@ConPost";
        private const string PARAM_DefrayType = "@DefrayType";
        private const string PARAM_zfHb = "@PayCurrency";//
        private const string PARAM_zfMoney = "@PayMoney";//
        private const string PARAM_BzHb = "@StandardCurrency";//
        private const string PARAM_BzMoney = "@StandardCurrencyMoney";//
        private const string PARAM_carrymoney = "@carrymoney";//
        private const string PARAM_thtype = "@Type";//
        private const string PARAM_OperateIP = "@OperateIP";
        private const string PARAM_OperateBh = "@OperateNumber";//
        private const string PARAM_huikuanid = "@RemittancesId";//
        private const string PARAM_dzbianhao = "@ElectronicAccountId";//
        private const string PARAM_ordertype = "@ordertype";
        private const string PARAM_defraytype = "@defraytype";

        private const string PARAM_concountry = "@concountry";
        private const string PARAM_conprovince = "@conprovince";
        private const string PARAM_concity = "@concity";
        private const string PARAM_conaddress = "@conaddress";
        private const string PARAM_contelphone = "@contelphone";
        private const string PARAM_conmobilphone = "@conmobilphone";
        private const string PARAM_conpost = "@conpost";
        private const string PARAM_consignee = "@consignee";
        private const string PARAM_conzipcode = "@conzipcode";




        public OrderCommonBLL()
        {
        }


        //********************************************************************************

        /// <summary>
        /// 保存团购的订单信息，同时更新店铺和公司的相应信息  , 店铺报单 是付过款的
        /// </summary>
        /// <param name="list">包含用户的订货信息列表</param>
        /// <param name="bianhao">包含用户的订货信息列表</param>
        /// <param name="dian">店铺编号</param>
        /// <param name="gaiQiShu">改期数</param>
        /// <param name="isfuxiao">是否复消</param>
        /// <param name="beizhu">备注信息</param>
        /// <param name="QiShu">用户指定的期数</param>
        /// <param name="errMessage">首次输入时的错误信息</param>
        /// <param name="zongJine">当前订单的总金额</param>
        /// <param name="zongPv">当前订单的总积分</param>
        /// <param name="orderId">订单号</param>
        public void SaveComityOrder(SqlTransaction tran, string bianhao, string dian,
            int gaiQiShu, byte isfuxiao, string beizhu, int QiShu, string errMessage, double zongJine, double zongPv, string orderId)
        {

            string SQL_INSERT_H_Order = @"INSERT INTO H_Order(Bianhao, OrderID, StoreID, TotalMoney, TotalPv, Qishu, 
										GaiQishu, isFuxiao, OrderDate, Err, isConfirm,Remark ,DefrayState,OperateIP,OperateBh) 
										VALUES( @Bianhao, @OrderID, @StoreID, @TotalMoney, @TotalPv, @Qishu, 
										@GaiQishu, @isFuxiao, @OrderDate, @Err, @isConfirm,@Remark ,1,@OperateIP,@OperateBh)";

            string SQL_UPDATE_TotalComityMoney;
            //string SQL_UPDATE_TotalComityPv;

            DateTime currentDT = MYDateTime.GetCurrentDateTime();

            SqlParameter[] parmOrder = 
							{	
								new SqlParameter(PARAM_Bianhao, bianhao),
								new SqlParameter(PARAM_OrderID, orderId),
								new SqlParameter(PARAM_StoreID, dian),
								new SqlParameter(PARAM_TotalMoney, zongJine),
								new SqlParameter(PARAM_TotalPv, zongPv),
								new SqlParameter(PARAM_Qishu, QiShu),
								new SqlParameter(PARAM_GaiQishu, gaiQiShu),
								new SqlParameter(PARAM_isFuxiao, isfuxiao),
								new SqlParameter(PARAM_OrderDate,  currentDT ),
								new SqlParameter(PARAM_Err, errMessage),
								new SqlParameter(PARAM_isConfirm, ""),
								new SqlParameter(PARAM_Remark , beizhu)	,
				                new SqlParameter("@OperateIP",""),//CommonDataBLL.OperateIP),
				                new SqlParameter("@OperateBh",""),//CommonDataBLL.OperateBh)
								
							};


            DBHelper.ExecuteNonQuery(tran, SQL_INSERT_H_Order, parmOrder, CommandType.Text);

            //更新店铺团购金额		//,TotalMemberOrderMoney = TotalMemberOrderMoney + "+ zongPv.ToString() +" 
            SQL_UPDATE_TotalComityMoney = "UPDATE D_Info SET TotalComityMoney = TotalComityMoney -" + zongJine.ToString() + ", TotalComityPv = TotalComityPv -" + zongPv.ToString() + ",TotalChangeMoney = TotalChangeMoney -" + zongJine.ToString() + ", TotalChangePv = TotalChangePv -" + zongPv.ToString() + "WHERE StoreID = '" + dian + "'";
            DBHelper.ExecuteNonQuery(tran, SQL_UPDATE_TotalComityMoney, null, CommandType.Text);

            //更新店铺报单额
            //	SQL_UPDATE_TotalMemberOrderMoney = "UPDATE D_Info SET TotalMemberOrderMoney = TotalMemberOrderMoney + ("+ zongJine +") WHERE StoreID = '"+ dian +"'";
            //	DBHelper .ExecuteNonQuery(tran ,SQL_UPDATE_TotalMemberOrderMoney  , null ,CommandType .Text );

        }


        //********************************************************************************

        /// <summary>
        /// 保存再次输入的订单信息，同时更新店铺和公司的相应信息
        /// </summary>
        /// <param name="list">包含用户的订货信息列表</param>
        /// <param name="bianhao">包含用户的订货信息列表</param>
        /// <param name="dian">店铺编号</param>
        /// <param name="gaiQiShu">改期数</param>
        /// <param name="isfuxiao">是否复消</param>
        /// <param name="beizhu">备注信息</param>
        /// <param name="QiShu">用户指定的期数</param>
        /// <param name="zongJine">当前订单的总金额</param>
        /// <param name="zongPv">当前订单的总积分</param>
        /// <param name="orderId">订单号</param>
        /// <param name="DefrayState">支付状态</param>
        public void SaveHOrder(SqlTransaction tran, ArrayList list, string bianhao, string dian, int gaiQiShu,
            int isfuxiao, string beizhu, int QiShu, double zongJine, double zongPv, string orderId, int DefrayState)
        {
            SaveHOrder(tran, list, bianhao, dian, gaiQiShu, isfuxiao, beizhu, QiShu, "", zongJine, zongPv, orderId, DefrayState);
        }
        public void SaveHOrder(SqlTransaction tran, ArrayList list, string bianhao, string dian, int gaiQiShu,
            int isfuxiao, string beizhu, int QiShu, double zongJine, double zongPv, string orderId, int DefrayState, int zfHb, decimal zfMoney, int BzHb, decimal BzMoney)
        {
            SaveHOrder(tran, list, bianhao, dian, gaiQiShu, isfuxiao, beizhu, QiShu, "", zongJine, zongPv, orderId, DefrayState, zfHb, zfMoney, BzHb, BzMoney);
        }

        //暂时使用
        public void SaveHOrder(SqlTransaction tran, ArrayList list, string bianhao, string dian, int gaiQiShu,
            int isfuxiao, string beizhu, int QiShu, double zongJine, double zongPv, string orderId, int DefrayState, int zfHb, decimal zfMoney, int BzHb, decimal BzMoney, int DefrayType, int thtype, decimal carrymoney, string DzBianhao, int ordertype, string concountry, string conprovince, string concity, string conaddress, string contelphone, string conmobilphone, string conpost, string consignee, string conzipcode)
        {
           // SaveHOrder(tran, list, bianhao, dian, gaiQiShu, isfuxiao, beizhu, QiShu, "", zongJine, zongPv, orderId, DefrayState, zfHb, zfMoney, BzHb, BzMoney, DefrayType, thtype, carrymoney, DzBianhao, ordertype, concountry, conprovince, concity, conaddress, contelphone, conmobilphone, conpost, consignee, conzipcode);
        }

        /// <summary>
        /// 保存首次输入的订单信息，同时更新店铺和公司的相应信息  , 店铺报单 是付过款的
        /// </summary>
        /// <param name="list">包含用户的订货信息列表</param>
        /// <param name="bianhao">包含用户的订货信息列表</param>
        /// <param name="dian">店铺编号</param>
        /// <param name="gaiQiShu">改期数</param>
        /// <param name="isfuxiao">是否复消</param>
        /// <param name="beizhu">备注信息</param>
        /// <param name="QiShu">用户指定的期数</param>
        /// <param name="errMessage">首次输入时的错误信息</param>
        /// <param name="zongJine">当前订单的总金额</param>
        /// <param name="zongPv">当前订单的总积分</param>
        /// <param name="orderId">订单号</param>
        /// <param name="DefrayState">支付状态</param>
        public void SaveHOrder(SqlTransaction tran, ArrayList list, string bianhao, string dian,
            int gaiQiShu, int isfuxiao, string beizhu, int QiShu, string errMessage, double zongJine, double zongPv, string orderId, int DefrayState)
        {

            string SQL_INSERT_H_Order = @"INSERT INTO H_Order(Bianhao, OrderID, StoreID, TotalMoney, TotalPv, Qishu, 
										GaiQishu, isFuxiao, OrderDate, Err, isConfirm,Remark ,DefrayState,OperateIP,OperateBh) 
										VALUES( @Bianhao, @OrderID, @StoreID, @TotalMoney, @TotalPv, @Qishu, 
										@GaiQishu, @isFuxiao, @OrderDate, @Err, @isConfirm,@Remark ,@DefrayState,@OperateIP,@OperateBh)";

            string SQL_INSERT_D_Kucun;
            string SQL_UPDATE_D_Kucun;
            string SQL_UPDATE_TotalMemberOrderMoney;

            DateTime currentDT = MYDateTime.GetCurrentDateTime();

            SqlParameter[] parmOrder = 
							{	
								new SqlParameter(PARAM_Bianhao, bianhao),
								new SqlParameter(PARAM_OrderID, orderId),
								new SqlParameter(PARAM_StoreID, dian),
								new SqlParameter(PARAM_TotalMoney, zongJine),
								new SqlParameter(PARAM_TotalPv, zongPv),
								new SqlParameter(PARAM_Qishu, QiShu),
								new SqlParameter(PARAM_GaiQishu, gaiQiShu),
								new SqlParameter(PARAM_isFuxiao, isfuxiao),
								new SqlParameter(PARAM_OrderDate,  currentDT ),
								new SqlParameter(PARAM_Err, errMessage),
								new SqlParameter(PARAM_isConfirm, ""),
								new SqlParameter(PARAM_Remark , beizhu),						
								new SqlParameter(PARAM_DefrayState ,DefrayState),
								new SqlParameter("@OperateIP",""),//CommonDataBLL.OperateIP),
				                new SqlParameter("@OperateBh",""),//CommonDataBLL.OperateBh)
								

							};

            DBHelper.ExecuteNonQuery(tran, SQL_INSERT_H_Order, parmOrder, CommandType.Text);

            //录入货物明细纪录			
            string SQL_INSERT_H_Mingxi = @"INSERT INTO H_Mingxi( Bianhao, OrderID, StoreID, ProductID, Quantity, Price, Pv, Qishu, isFuxiao, Remark, OrderDate)
														VALUES(  @Bianhao, @OrderID, @StoreID, @ProductID, @Quantity, @Price, @Pv, @Qishu, @isFuxiao, @Remark, @OrderDate)";

            SqlCommand cmd = new SqlCommand(SQL_INSERT_H_Mingxi);
            cmd.Connection = tran.Connection;
            cmd.Transaction = tran;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(PARAM_Bianhao, SqlDbType.VarChar, 10);
            cmd.Parameters.Add(PARAM_OrderID, SqlDbType.VarChar, 15);
            cmd.Parameters.Add(PARAM_StoreID, SqlDbType.VarChar, 10);
            cmd.Parameters.Add(PARAM_ProductID, SqlDbType.VarChar, 15);
            cmd.Parameters.Add(PARAM_Quantity, SqlDbType.Int);
            cmd.Parameters.Add(PARAM_Price, SqlDbType.Money);
            cmd.Parameters.Add(PARAM_Pv, SqlDbType.Money);
            cmd.Parameters.Add(PARAM_Qishu, SqlDbType.Int);
            cmd.Parameters.Add(PARAM_isFuxiao, SqlDbType.TinyInt);
            cmd.Parameters.Add(PARAM_Remark, SqlDbType.VarChar, 100);
            cmd.Parameters.Add(PARAM_OrderDate, SqlDbType.DateTime);


            foreach (OrderProduct _product in list)
            {
                cmd.Parameters[0].Value = bianhao;
                cmd.Parameters[1].Value = orderId;
                cmd.Parameters[2].Value = dian;
                cmd.Parameters[3].Value = _product.id;
                cmd.Parameters[4].Value = _product.count;
                cmd.Parameters[5].Value = _product.price;
                cmd.Parameters[6].Value = _product.pv;
                cmd.Parameters[7].Value = QiShu;
                cmd.Parameters[8].Value = isfuxiao;
                cmd.Parameters[9].Value = "";
                cmd.Parameters[10].Value = currentDT;

                cmd.ExecuteNonQuery();

                // 如果是会员自由注册时--------------这时不减去店铺库存，直到店铺自己审核时才减去自己的库存量
                if (isfuxiao.ToString() != "3")
                {
                    SQL_UPDATE_D_Kucun = @"UPDATE D_Kucun SET  
										TotalOut = TotalOut +( " + _product.count + @"), 
										ActualStorage = ActualStorage -(" + _product.count + @") WHERE ProductID = '" + _product.id + @"' And StoreID = '" + dian + "'";

                    //减去店铺的库存
                    int _productExists = DBHelper.ExecuteNonQuery(tran, SQL_UPDATE_D_Kucun, null, CommandType.Text);

                    if (_productExists == 0)
                    {
                        //店铺没有该货物类型的纪录，则添加该类型的记录
                        SQL_INSERT_D_Kucun = "INSERT INTO D_Kucun( StoreID, ProductID, TotalIn, TotalOut, ActualStorage, HasOrderCount) VALUES ";
                        SQL_INSERT_D_Kucun += "('" + dian + "'," + _product.id + "," + 0 + "," + _product.count + "," + (_product.count * (-1)) + "," + 0 + ")";

                        DBHelper.ExecuteNonQuery(tran, SQL_INSERT_D_Kucun, null, CommandType.Text);
                    }
                }
            }
            if (isfuxiao.ToString() != "3")
            {
                SQL_UPDATE_TotalMemberOrderMoney = "UPDATE D_Info SET TotalMemberOrderMoney = TotalMemberOrderMoney + (" + zongJine + "),TotalComityMoney = TotalComityMoney - '" + zongJine + "',TotalComityPv = TotalComityPv - '" + zongPv + "'  WHERE StoreID = '" + dian + "'";
                DBHelper.ExecuteNonQuery(tran, SQL_UPDATE_TotalMemberOrderMoney, null, CommandType.Text);
            }
        }



        //批量注册调用的类


        public void SaveHOrder(SqlTransaction tran, ArrayList list, string bianhao, string dian,
            int gaiQiShu, int isfuxiao, string beizhu, int QiShu, string errMessage, double zongJine, double zongPv, string orderId, int DefrayState, int zfHb, decimal zfMoney, int BzHb, decimal BzMoney)
        {


            string SQL_INSERT_H_Order = @"INSERT INTO H_Order(Bianhao, OrderID, StoreID, TotalMoney, TotalPv, Qishu, 
										GaiQishu, isFuxiao, OrderDate, Err, isConfirm,Remark ,DefrayState,zfhb,zfMoney,BzHb,BzMoney,OperateIP,OperateBh) 
										VALUES( @Bianhao, @OrderID, @StoreID, @TotalMoney, @TotalPv, @Qishu, 
										@GaiQishu, @isFuxiao, @OrderDate, @Err, @isConfirm,@Remark ,@DefrayState,@zfHb,@zfMoney,@BzHb,@BzMoney,@OperateIP,@OperateBh";

            string SQL_INSERT_D_Kucun;
            string SQL_UPDATE_D_Kucun;
            string SQL_UPDATE_TotalMemberOrderMoney;

            DateTime currentDT = MYDateTime.GetCurrentDateTime();

            SqlParameter[] parmOrder = 
							{	
								new SqlParameter(PARAM_Bianhao, bianhao),
								new SqlParameter(PARAM_OrderID, orderId),
								new SqlParameter(PARAM_StoreID, dian),
								new SqlParameter(PARAM_TotalMoney, zongJine),
								new SqlParameter(PARAM_TotalPv, zongPv),
								new SqlParameter(PARAM_Qishu, QiShu),
								new SqlParameter(PARAM_GaiQishu, gaiQiShu),
								new SqlParameter(PARAM_isFuxiao, isfuxiao),
								new SqlParameter(PARAM_OrderDate,  currentDT ),
								new SqlParameter(PARAM_Err, errMessage),
								new SqlParameter(PARAM_isConfirm, ""),
								new SqlParameter(PARAM_Remark , beizhu),						
								new SqlParameter(PARAM_DefrayState ,DefrayState),
								new SqlParameter(PARAM_zfHb, zfHb),
								new SqlParameter(PARAM_zfMoney, zfMoney),
								new SqlParameter(PARAM_BzHb , BzHb),						
								new SqlParameter(PARAM_BzMoney ,BzMoney),
								new SqlParameter(PARAM_OperateIP,""),//CommonDataBLL.OperateIP),
								new SqlParameter(PARAM_OperateBh,""),//CommonDataBLL.OperateBh)
								

							};

            DBHelper.ExecuteNonQuery(tran, SQL_INSERT_H_Order, parmOrder, CommandType.Text);

            //录入货物明细纪录			
            string SQL_INSERT_H_Mingxi = @"INSERT INTO H_Mingxi( Bianhao, OrderID, StoreID, ProductID, Quantity, Price, Pv, Qishu, isFuxiao, Remark, OrderDate)
														VALUES(  @Bianhao, @OrderID, @StoreID, @ProductID, @Quantity, @Price, @Pv, @Qishu, @isFuxiao, @Remark, @OrderDate)";

            SqlCommand cmd = new SqlCommand(SQL_INSERT_H_Mingxi);
            cmd.Connection = tran.Connection;
            cmd.Transaction = tran;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(PARAM_Bianhao, SqlDbType.VarChar, 10);
            cmd.Parameters.Add(PARAM_OrderID, SqlDbType.VarChar, 15);
            cmd.Parameters.Add(PARAM_StoreID, SqlDbType.VarChar, 10);
            cmd.Parameters.Add(PARAM_ProductID, SqlDbType.VarChar, 15);
            cmd.Parameters.Add(PARAM_Quantity, SqlDbType.Int);
            cmd.Parameters.Add(PARAM_Price, SqlDbType.Money);
            cmd.Parameters.Add(PARAM_Pv, SqlDbType.Money);
            cmd.Parameters.Add(PARAM_Qishu, SqlDbType.Int);
            cmd.Parameters.Add(PARAM_isFuxiao, SqlDbType.TinyInt);
            cmd.Parameters.Add(PARAM_Remark, SqlDbType.VarChar, 100);
            cmd.Parameters.Add(PARAM_OrderDate, SqlDbType.DateTime);


            foreach (OrderProduct _product in list)
            {
                cmd.Parameters[0].Value = bianhao;
                cmd.Parameters[1].Value = orderId;
                cmd.Parameters[2].Value = dian;
                cmd.Parameters[3].Value = _product.id;
                cmd.Parameters[4].Value = _product.count;
                cmd.Parameters[5].Value = _product.price;
                cmd.Parameters[6].Value = _product.pv;
                cmd.Parameters[7].Value = QiShu;
                cmd.Parameters[8].Value = isfuxiao;
                cmd.Parameters[9].Value = "";
                cmd.Parameters[10].Value = currentDT;

                cmd.ExecuteNonQuery();

                // 如果是会员自由注册时--------------这时不减去店铺库存，直到店铺自己审核时才减去自己的库存量
                if (DefrayState == 1)
                {
                    SQL_UPDATE_D_Kucun = @"UPDATE D_Kucun SET  
										TotalOut = TotalOut +( " + _product.count + @"), 
										ActualStorage = ActualStorage -(" + _product.count + @") WHERE ProductID = '" + _product.id + @"' And StoreID = '" + dian + "'";

                    //减去店铺的库存
                    int _productExists = DBHelper.ExecuteNonQuery(tran, SQL_UPDATE_D_Kucun, null, CommandType.Text);

                    if (_productExists == 0)
                    {
                        //店铺没有该货物类型的纪录，则添加该类型的记录
                        SQL_INSERT_D_Kucun = "INSERT INTO D_Kucun( StoreID, ProductID, TotalIn, TotalOut, ActualStorage, HasOrderCount) VALUES ";
                        SQL_INSERT_D_Kucun += "('" + dian + "'," + _product.id + "," + 0 + "," + _product.count + "," + (_product.count * (-1)) + "," + 0 + ")";

                        DBHelper.ExecuteNonQuery(tran, SQL_INSERT_D_Kucun, null, CommandType.Text);
                    }
                }
            }

            if (DefrayState == 1)
            {
                SQL_UPDATE_TotalMemberOrderMoney = "UPDATE D_Info SET TotalMemberOrderMoney = TotalMemberOrderMoney + (" + zongJine + "),TotalComityMoney = TotalComityMoney - '" + zongJine + "',TotalComityPv = TotalComityPv - '" + zongPv + "'  WHERE StoreID = '" + dian + "'";
                DBHelper.ExecuteNonQuery(tran, SQL_UPDATE_TotalMemberOrderMoney, null, CommandType.Text);
            }



        }





        //报单调用的类
        /*
         public static void SaveHOrder(SqlTransaction tran, ArrayList list, string bianhao, string dian,
             int gaiQiShu, int isfuxiao, string beizhu, int QiShu, string errMessage, double zongJine, double zongPv, string orderId, int DefrayState, int zfHb, decimal zfMoney, int BzHb, decimal BzMoney, int DefrayType, int thtype, decimal carrymoney, string DzBianhao, int ordertype, string concountry, string conprovince, string concity, string conaddress, string contelphone, string conmobilphone, string conpost, string consignee, string conzipcode)
         {
             int huikuanid = 0;
             if (DefrayType == 1)//现金支付
             {

                 if (ordertype.ToString() != "3" && ordertype.ToString() != "2")//如果不是自由注册或网上购物
                 {
                     DefrayState = 1;
                     gaiQiShu = QiShu;

                 }
                 else
                 {
                     if (gaiQiShu != -1)
                     {
                         DefrayState = 1;
                     }
                 }

             }
             if (DefrayType == 2)//电子帐户支付
             {
                 DBHelper.ExecuteNonQuery(tran, "update h_info set ectbaodan=ectbaodan+" + zongJine + " where bianhao='" + DzBianhao + "'", null, CommandType.Text);
                 double zhifumoney = Convert.ToDouble(DBHelper.ExecuteScalar(tran, "select isnull((zongji-ectzhifu-fafanglj-ectzhuan),0) from h_info where bianhao='" + DzBianhao + "'", CommandType.Text));
                 if (zhifumoney > zongJine)
                 {
                     DefrayState = 1;
                     gaiQiShu = QiShu;
                     DBHelper.ExecuteNonQuery(tran, "update h_info set ectzhifu=ectzhifu+" + zongJine + " where bianhao='" + DzBianhao + "'", null, CommandType.Text);
                     string zfhuobi = Convert.ToString(DBHelper.ExecuteScalar(tran, "select isnull(name,'') from m_currency where id=" + zfHb, CommandType.Text));
                     string InsertD_huikuan = "insert into d_huikuan (storeid,remitmoney,fukuanqi,shoukuanri,huichuri,beizhu,isgsqr,zfhuobi,zfmoney) values ('" + dian + "'," + zongJine + "," + QiShu + ",'" + DateTime.Now + "','" + DateTime.Now + "','会员" + DzBianhao + "用电子钱包报单',1,'" + zfhuobi + "'," + zfMoney + ")";
                     DBHelper.ExecuteNonQuery(tran, InsertD_huikuan, null, CommandType.Text);
                     DBHelper.ExecuteNonQuery(tran, "update d_info set totalaccountmoney=totalaccountmoney+" + zongJine + " where storeid='" + dian + "'", null, CommandType.Text);
                     huikuanid = Convert.ToInt32(DBHelper.ExecuteScalar(tran, "select top 1 id from d_huikuan order by id desc", CommandType.Text));
                 }
             }


             string SQL_INSERT_H_Order = @"INSERT INTO H_Order(Bianhao, OrderID, StoreID, TotalMoney, TotalPv, Qishu, 
                                         GaiQishu, isFuxiao, OrderDate, Err, isConfirm,Remark ,DefrayState,zfhb,zfMoney,BzHb,BzMoney,OperateIP,OperateBh,DefrayType,thtype,carrymoney,huikuanid,dzbianhao,ordertype,concountry,conprovince,concity,conaddress,contelphone,conmobilphone,conpost,consignee,conzipcode) 
                                         VALUES( @Bianhao, @OrderID, @StoreID, @TotalMoney, @TotalPv, @Qishu, 
                                         @GaiQishu, @isFuxiao, @OrderDate, @Err, @isConfirm,@Remark ,@DefrayState,@zfHb,@zfMoney,@BzHb,@BzMoney,@OperateIP,@OperateBh,@DefrayType,@thtype,@carrymoney,@huikuanid,@dzbianhao,@ordertype,@concountry,@conprovince,@concity,@conaddress,@contelphone,@conmobilphone,@conpost,@consignee,@conzipcode)";

             string SQL_INSERT_D_Kucun;
             string SQL_UPDATE_D_Kucun;
             string SQL_UPDATE_TotalMemberOrderMoney;

             DateTime currentDT = MYDateTime.GetCurrentDateTime();

             SqlParameter[] parmOrder = 
                             {	
                                 new SqlParameter(PARAM_Bianhao, bianhao),
                                 new SqlParameter(PARAM_OrderID, orderId),
                                 new SqlParameter(PARAM_StoreID, dian),
                                 new SqlParameter(PARAM_TotalMoney, zongJine),
                                 new SqlParameter(PARAM_TotalPv, zongPv),
                                 new SqlParameter(PARAM_Qishu, QiShu),
                                 new SqlParameter(PARAM_GaiQishu, gaiQiShu),
                                 new SqlParameter(PARAM_isFuxiao, isfuxiao),
                                 new SqlParameter(PARAM_OrderDate,  currentDT ),
                                 new SqlParameter(PARAM_Err, errMessage),
                                 new SqlParameter(PARAM_isConfirm, ""),
                                 new SqlParameter(PARAM_Remark , beizhu),						
                                 new SqlParameter(PARAM_DefrayState ,DefrayState),
                                 new SqlParameter(PARAM_zfHb, zfHb),
                                 new SqlParameter(PARAM_zfMoney, zfMoney),
                                 new SqlParameter(PARAM_BzHb , BzHb),						
                                 new SqlParameter(PARAM_BzMoney ,BzMoney),
                                 new SqlParameter(PARAM_OperateIP,CommonDataBLL.OperateIP),
                                 new SqlParameter(PARAM_OperateBh,CommonDataBLL.OperateBh),
                                 new SqlParameter(PARAM_DefrayType ,DefrayType),
                                 new SqlParameter(PARAM_thtype,thtype),
                                 new SqlParameter(PARAM_carrymoney,carrymoney),
                                 new SqlParameter(PARAM_huikuanid,huikuanid),
                                 new SqlParameter(PARAM_dzbianhao,DzBianhao),
                                 new SqlParameter(PARAM_ordertype,ordertype),
                                 new SqlParameter(PARAM_concountry,concountry),
                                 new SqlParameter(PARAM_conprovince,conprovince),
                                 new SqlParameter(PARAM_concity ,concity),
                                 new SqlParameter(PARAM_conaddress,conaddress),
                                 new SqlParameter(PARAM_contelphone,contelphone),
                                 new SqlParameter(PARAM_conmobilphone,conmobilphone),
                                 new SqlParameter(PARAM_conpost,conpost),
                                 new SqlParameter(PARAM_consignee,consignee),
                                 new SqlParameter(PARAM_conzipcode,conzipcode)
						
                             };

             DBHelper.ExecuteNonQuery(tran, SQL_INSERT_H_Order, parmOrder, CommandType.Text);

             //录入货物明细纪录			
             string SQL_INSERT_H_Mingxi = @"INSERT INTO H_Mingxi( Bianhao, OrderID, StoreID, ProductID, Quantity, Price, Pv, Qishu, isFuxiao, Remark, OrderDate)
                                                         VALUES(  @Bianhao, @OrderID, @StoreID, @ProductID, @Quantity, @Price, @Pv, @Qishu, @isFuxiao, @Remark, @OrderDate)";

             SqlCommand cmd = new SqlCommand(SQL_INSERT_H_Mingxi);
             cmd.Connection = tran.Connection;
             cmd.Transaction = tran;
             cmd.CommandType = CommandType.Text;

             cmd.Parameters.Add(PARAM_Bianhao, SqlDbType.VarChar, 10);
             cmd.Parameters.Add(PARAM_OrderID, SqlDbType.VarChar, 15);
             cmd.Parameters.Add(PARAM_StoreID, SqlDbType.VarChar, 10);
             cmd.Parameters.Add(PARAM_ProductID, SqlDbType.VarChar, 15);
             cmd.Parameters.Add(PARAM_Quantity, SqlDbType.Int);
             cmd.Parameters.Add(PARAM_Price, SqlDbType.Money);
             cmd.Parameters.Add(PARAM_Pv, SqlDbType.Money);
             cmd.Parameters.Add(PARAM_Qishu, SqlDbType.Int);
             cmd.Parameters.Add(PARAM_isFuxiao, SqlDbType.TinyInt);
             cmd.Parameters.Add(PARAM_Remark, SqlDbType.VarChar, 100);
             cmd.Parameters.Add(PARAM_OrderDate, SqlDbType.DateTime);


             foreach (OrderProduct _product in list)
             {
                 cmd.Parameters[0].Value = bianhao;
                 cmd.Parameters[1].Value = orderId;
                 cmd.Parameters[2].Value = dian;
                 cmd.Parameters[3].Value = _product.id;
                 cmd.Parameters[4].Value = _product.count;
                 cmd.Parameters[5].Value = _product.price;
                 cmd.Parameters[6].Value = _product.pv;
                 cmd.Parameters[7].Value = QiShu;
                 cmd.Parameters[8].Value = isfuxiao;
                 cmd.Parameters[9].Value = "";
                 cmd.Parameters[10].Value = currentDT;

                 cmd.ExecuteNonQuery();

                 // 如果是会员自由注册时--------------这时不减去店铺库存，直到店铺自己审核时才减去自己的库存量
                 if (DefrayState == 1)
                 {
                     SQL_UPDATE_D_Kucun = @"UPDATE D_Kucun SET  
                                         TotalOut = TotalOut +( " + _product.count + @"), 
                                         ActualStorage = ActualStorage -(" + _product.count + @") WHERE ProductID = '" + _product.id + @"' And StoreID = '" + dian + "'";

                     //减去店铺的库存
                     int _productExists = DBHelper.ExecuteNonQuery(tran, SQL_UPDATE_D_Kucun, null, CommandType.Text);

                     if (_productExists == 0)
                     {
                         //店铺没有该货物类型的纪录，则添加该类型的记录
                         SQL_INSERT_D_Kucun = "INSERT INTO D_Kucun( StoreID, ProductID, TotalIn, TotalOut, ActualStorage, HasOrderCount) VALUES ";
                         SQL_INSERT_D_Kucun += "('" + dian + "'," + _product.id + "," + 0 + "," + _product.count + "," + (_product.count * (-1)) + "," + 0 + ")";

                         DBHelper.ExecuteNonQuery(tran, SQL_INSERT_D_Kucun, null, CommandType.Text);
                     }
                 }
             }

             if (DefrayState == 1)
             {
                 SQL_UPDATE_TotalMemberOrderMoney = "UPDATE D_Info SET TotalMemberOrderMoney = TotalMemberOrderMoney + (" + zongJine + "),TotalComityMoney = TotalComityMoney - '" + zongJine + "',TotalComityPv = TotalComityPv - '" + zongPv + "'  WHERE StoreID = '" + dian + "'";
                 DBHelper.ExecuteNonQuery(tran, SQL_UPDATE_TotalMemberOrderMoney, null, CommandType.Text);
             }



         }

         */


//        #region 有问题

//        //元角分调用类
//        /// <summary>
//        /// 保存会员购货输入的订单信息，同时更新店铺和公司的相应信息--------会员报单部分会员购货
//        /// </summary>
//        /// <param name="tran">事务</param>
//        /// <param name="list">包含用户的订货信息列表</param>
//        /// <param name="tobjH_Order">H_Order对象</param>
//        /// <param name="ConfirmType">0为需要修改店铺相关的信息，1为不需要</param>
//        public static void SaveHOrder(SqlTransaction tran, ArrayList list, H_Order tobjH_Order, int ConfirmType, int zfHb, decimal zfMoney, int BzHb, decimal BzMoney)
//        {
//            string SQL_INSERT_H_Order = @"INSERT INTO H_Order
//										(
//										Bianhao, OrderID, StoreID, TotalMoney, TotalPv, Qishu,
//										GaiQishu, isFuxiao, OrderDate, Err,  isConfirm, Remark, SureFare,
//										DefrayState, AffluxBank, AffluxAccount, Consignee, ConCountry, ConProvince, 
//										ConCity, ConAddress, ConZipCode, ConTelphone, ConMobilPhone, ConPost,DefrayType,zfhb,zfmoney,bzhb,bzMoney,OperateIP,OperateBh,thtype,carrymoney,ordertype
//										)
//										VALUES
//										( @Bianhao, @OrderID, @StoreID, @TotalMoney, @TotalPv, @Qishu,
//										@GaiQishu, @isFuxiao, @OrderDate, @Err, @isConfirm, @Remark, @SureFare,
//										@DefrayState, @AffluxBank, @AffluxAccount, @Consignee, @ConCountry, @ConProvince, 
//										@ConCity, @ConAddress, @ConZipCode, @ConTelphone, @ConMobilPhone, @ConPost,@DefrayType,@zfHb,@zfMoney,@BzHb,@BzMoney,@OperateIP,@OperateBh,@thtype,@carrymoney,@ordertype)";

//            //			string SQL_INSERT_D_Kucun ;
//            //			string SQL_UPDATE_D_Kucun ;
//            //			string SQL_UPDATE_TotalMemberOrderMoney;

//            DateTime currentDT = MYDateTime.GetCurrentDateTime();

//            SqlParameter[] H_OrderParms = 
//                            {
//                                new SqlParameter(PARAM_Bianhao,SqlDbType.VarChar ,12),
//                                new SqlParameter(PARAM_OrderID,SqlDbType.VarChar ,15),
//                                new SqlParameter(PARAM_StoreID,SqlDbType.VarChar ,15),
//                                new SqlParameter(PARAM_TotalMoney,SqlDbType.Money ),
//                                new SqlParameter(PARAM_TotalPv, SqlDbType.Money ),
//                                new SqlParameter(PARAM_Qishu, SqlDbType.Int ),
//                                new SqlParameter(PARAM_GaiQishu,SqlDbType.Int ),
//                                new SqlParameter(PARAM_isFuxiao, SqlDbType.TinyInt ),
//                                new SqlParameter(PARAM_OrderDate,SqlDbType.DateTime ),
//                                new SqlParameter(PARAM_Err, SqlDbType.VarChar ,500),
//                                new SqlParameter(PARAM_isConfirm, SqlDbType.Char ,1),
//                                new SqlParameter(PARAM_Remark, SqlDbType.VarChar ,500),
//                                new SqlParameter(PARAM_SureFare, SqlDbType.Int ),
//                                new SqlParameter(PARAM_DefrayState,SqlDbType.Int ),
//                                new SqlParameter(PARAM_AffluxBank, SqlDbType.VarChar ,50),
//                                new SqlParameter(PARAM_AffluxAccount,SqlDbType.VarChar ,50),
//                                new SqlParameter(PARAM_Consignee, SqlDbType.VarChar ,50),
//                                new SqlParameter(PARAM_ConCountry,SqlDbType.VarChar ,20),
//                                new SqlParameter(PARAM_ConProvince, SqlDbType.VarChar ,20),
//                                new SqlParameter(PARAM_ConCity, SqlDbType.VarChar ,20),
//                                new SqlParameter(PARAM_ConAddress, SqlDbType.VarChar ,255),
//                                new SqlParameter(PARAM_ConZipCode, SqlDbType.VarChar ,10),
//                                new SqlParameter(PARAM_ConTelphone,SqlDbType.VarChar ,10),
//                                new SqlParameter(PARAM_ConMobilPhone, SqlDbType.VarChar ,20),
//                                new SqlParameter(PARAM_ConPost, SqlDbType.VarChar ,50),
//                                new SqlParameter(PARAM_DefrayType,SqlDbType.Int ) ,
//                                new SqlParameter(PARAM_zfHb,SqlDbType.Int ) ,
//                                new SqlParameter(PARAM_zfMoney,SqlDbType.Decimal ) ,
//                                new SqlParameter(PARAM_BzHb,SqlDbType.Int ) ,
//                                new SqlParameter(PARAM_BzMoney,SqlDbType.Decimal ) ,
//                                new SqlParameter("@OperateIP",SqlDbType.VarChar,30),
//                                new SqlParameter("@OperateBh",SqlDbType.VarChar,30),
//                                new SqlParameter(PARAM_thtype,SqlDbType.Int ) ,
//                                new SqlParameter(PARAM_carrymoney,SqlDbType.Decimal),
//                                new SqlParameter(PARAM_ordertype,SqlDbType.Int)
//                            };

//            H_OrderParms[0].Value = tobjH_Order.Bianhao;
//            H_OrderParms[1].Value = tobjH_Order.OrderID;
//            H_OrderParms[2].Value = tobjH_Order.StoreID;
//            H_OrderParms[3].Value = tobjH_Order.TotalMoney;
//            H_OrderParms[4].Value = tobjH_Order.TotalPv;
//            H_OrderParms[5].Value = tobjH_Order.Qishu;
//            H_OrderParms[6].Value = tobjH_Order.GaiQishu;
//            H_OrderParms[7].Value = tobjH_Order.isFuxiao;
//            H_OrderParms[8].Value = currentDT;
//            H_OrderParms[9].Value = tobjH_Order.Err;
//            H_OrderParms[10].Value = tobjH_Order.isConfirm;
//            H_OrderParms[11].Value = tobjH_Order.Remark;
//            H_OrderParms[12].Value = tobjH_Order.SureFare;
//            H_OrderParms[13].Value = tobjH_Order.DefrayState;
//            H_OrderParms[14].Value = tobjH_Order.AffluxBank;
//            H_OrderParms[15].Value = tobjH_Order.AffluxAccount;
//            H_OrderParms[16].Value = tobjH_Order.Consignee;
//            H_OrderParms[17].Value = tobjH_Order.ConCountry;
//            H_OrderParms[18].Value = tobjH_Order.ConProvince;
//            H_OrderParms[19].Value = tobjH_Order.ConCity;
//            H_OrderParms[20].Value = tobjH_Order.ConAddress;
//            H_OrderParms[21].Value = tobjH_Order.ConZipCode;
//            H_OrderParms[22].Value = tobjH_Order.ConTelphone;
//            H_OrderParms[23].Value = tobjH_Order.ConMobilPhone;
//            H_OrderParms[24].Value = tobjH_Order.ConPost;
//            H_OrderParms[25].Value = tobjH_Order.DefrayType;
//            H_OrderParms[26].Value = zfHb;
//            H_OrderParms[27].Value = zfMoney;
//            H_OrderParms[28].Value = BzHb;
//            H_OrderParms[29].Value = BzMoney;
//            H_OrderParms[30].Value = CommonDataBLL.OperateIP;
//            H_OrderParms[31].Value = CommonDataBLL.OperateBh;
//            H_OrderParms[32].Value = tobjH_Order.thtype;
//            H_OrderParms[33].Value = tobjH_Order.carrymoney;
//            H_OrderParms[34].Value = tobjH_Order.ordertype;

//            DBHelper.ExecuteNonQuery(tran, SQL_INSERT_H_Order, H_OrderParms, CommandType.Text);

//            //录入货物明细纪录			
//            string SQL_INSERT_H_Mingxi = @"INSERT INTO H_Mingxi( Bianhao, OrderID, StoreID, ProductID, Quantity, Price, Pv, Qishu, isFuxiao, Remark, OrderDate)
//														VALUES(  @Bianhao, @OrderID, @StoreID, @ProductID, @Quantity, @Price, @Pv, @Qishu, @isFuxiao, @Remark, @OrderDate)";

//            SqlCommand cmd = new SqlCommand(SQL_INSERT_H_Mingxi);
//            cmd.Connection = tran.Connection;
//            cmd.Transaction = tran;
//            cmd.CommandType = CommandType.Text;

//            cmd.Parameters.Add(PARAM_Bianhao, SqlDbType.VarChar, 10);
//            cmd.Parameters.Add(PARAM_OrderID, SqlDbType.VarChar, 15);
//            cmd.Parameters.Add(PARAM_StoreID, SqlDbType.VarChar, 10);
//            cmd.Parameters.Add(PARAM_ProductID, SqlDbType.VarChar, 15);
//            cmd.Parameters.Add(PARAM_Quantity, SqlDbType.Int);
//            cmd.Parameters.Add(PARAM_Price, SqlDbType.Money);
//            cmd.Parameters.Add(PARAM_Pv, SqlDbType.Money);
//            cmd.Parameters.Add(PARAM_Qishu, SqlDbType.Int);
//            cmd.Parameters.Add(PARAM_isFuxiao, SqlDbType.TinyInt);
//            cmd.Parameters.Add(PARAM_Remark, SqlDbType.VarChar, 100);
//            cmd.Parameters.Add(PARAM_OrderDate, SqlDbType.DateTime);


//            foreach (OrderProduct _product in list)
//            {
//                cmd.Parameters[0].Value = tobjH_Order.Bianhao;
//                cmd.Parameters[1].Value = tobjH_Order.OrderID;
//                cmd.Parameters[2].Value = tobjH_Order.StoreID;
//                cmd.Parameters[3].Value = _product.id;
//                cmd.Parameters[4].Value = _product.count;
//                cmd.Parameters[5].Value = _product.price;
//                cmd.Parameters[6].Value = _product.pv;
//                cmd.Parameters[7].Value = tobjH_Order.Qishu;
//                cmd.Parameters[8].Value = tobjH_Order.isFuxiao;
//                cmd.Parameters[9].Value = "";
//                cmd.Parameters[10].Value = currentDT;

//                cmd.ExecuteNonQuery();
//                //				if(ConfirmType==0)
//                //				{
//                //					SQL_UPDATE_D_Kucun = @"UPDATE D_Kucun SET  
//                //										TotalOut = TotalOut +( "+ _product.count +@"), 
//                //										ActualStorage = ActualStorage -("+ _product.count +@") WHERE ProductID = '"+ _product.id + @"' And StoreID = '"+ tobjH_Order.StoreID +"'";
//                //
//                //					//减去店铺的库存
//                //					int _productExists = DBHelper.ExecuteNonQuery(tran,SQL_UPDATE_D_Kucun,null,CommandType.Text);
//                //		
//                //					if ( _productExists == 0 )
//                //					{
//                //						//店铺没有该货物类型的纪录，则添加该类型的记录
//                //						SQL_INSERT_D_Kucun = "INSERT INTO D_Kucun( StoreID, ProductID, TotalIn, TotalOut, ActualStorage, HasOrderCount) VALUES ";
//                //						SQL_INSERT_D_Kucun+="('"+ tobjH_Order.StoreID +"',"+ _product .id +","+ 0 +"," + _product.count +","+ (_product.count* (-1)) +","+  0 +")";
//                //						
//                //						DBHelper.ExecuteNonQuery(tran, SQL_INSERT_D_Kucun ,null,CommandType.Text);
//                //					}
//                //				}

//                //2006-04-07会员元角分注册暂不加店总报单额
//                //				if(ConfirmType == 0)
//                //				{
//                //					SQL_UPDATE_TotalMemberOrderMoney = "UPDATE D_Info SET TotalMemberOrderMoney = TotalMemberOrderMoney + ("+ tobjH_Order.TotalMoney +") WHERE StoreID = '"+ tobjH_Order.StoreID +"'";
//                //					DBHelper .ExecuteNonQuery(tran ,SQL_UPDATE_TotalMemberOrderMoney  , null ,CommandType .Text );
//                //				}
//            }

//        }

//        #endregion有问题


        /// <summary>
        /// 把订单号中对应的库存加入到店铺库存中
        /// </summary>
        /// <param name="dingdanhao">订单号</param>
        /// <param name="msg">错误信息</param>
        public static void AddDianProduct(string dingdanhao, out string msg)
        {
            msg = "";
            SqlParameter[] parm = { new SqlParameter("@dingdanhao", SqlDbType.VarChar, 50), };
            parm[0].Value = dingdanhao;
            DBHelper.ExecuteNonQuery("addDianKucun", parm, CommandType.StoredProcedure);

        }


        /// <summary>
        /// 检查订单号是否存在
        /// </summary>
        public static bool CheckOrderIdExists(string ddh)
        {
            string sSQL = "Select Count(OrderID) From H_order Where OrderID=@ddh";
            SqlParameter[] parm = {
									  new SqlParameter("@ddh", SqlDbType.VarChar,15)
								  };
            parm[0].Value = ddh;

            int count = (int)DBHelper.ExecuteScalar(sSQL, parm, CommandType.Text);

            return count > 0 ? true : false;
        }

        /// <summary>
        /// 得到新的报单号 形如：050108101
        /// </summary>
        /// <returns>店的余额</returns>
        public static string GetNewOrderID_old()
        {
            string _orderId = MYDateTime.ToYYMMDDString();

            string sSQL = "Select Top 1 ID From H_Order Order By ID Desc";
            SqlDataReader reader = DBHelper.ExecuteReader(sSQL);
            if (reader.Read())
                _orderId += (reader.GetInt32(0) + 1).ToString();
            else
                _orderId += "1";

            reader.Close();

            return _orderId;
        }

        /// <summary>
        /// 得到新的报单号 形如：050108101
        /// </summary>
        /// <returns>店的余额</returns>
        public static string GetNewOrderID()
        {
            string _orderId = MYDateTime.ToYYMMDDHHmmssString();

            string sSQL = "Select Count(*) From H_Order Where OrderId='";

            bool sameflag = true;
            while (sameflag)
            {
                if (((int)DBHelper.ExecuteScalar(sSQL + _orderId + "'")) > 0)
                {
                    _orderId = MYDateTime.ToYYMMDDHHmmssString();
                }
                else
                    sameflag = false;
            }

            return _orderId;
        }

        /// <summary>
        /// 根据当前的报单状态来检查报单
        /// </summary>
        /// <param name="storeId">店编号</param>
        /// <param name="updateOrderMoney">被编辑报单的金额，不是编辑的话就给"0"</param>
        /// <param name="currentOrderMoney">当前报单的金额</param>
        /// <param name="OrderGoodsList">报单的货物ArrayList</param>
        /// <returns>EnumCheckWheatherRegister</returns>
        public static EnumCheckWheatherRegister CheckWheatherCanRegister(string storeId, double updateOrderMoney, double currentOrderMoney,
             ArrayList OrderGoodsList)
        {
            if (Model.Other.WebSiteBaseDataConfig.EnumRegisterMemberType == EnumRegisterMemberType.UseMoneyRegister)
            {                
                double storeLeftMoney = 0;// StoreData.GetLeftRegisterMemberMoney(storeId) + updateOrderMoney;
                if (currentOrderMoney > storeLeftMoney)
                    return EnumCheckWheatherRegister.StoreMoneyNotEnough;
                else
                    return EnumCheckWheatherRegister.StoreMoneyEnough;
            }
            else
                if (Model.Other.WebSiteBaseDataConfig.EnumRegisterMemberType == EnumRegisterMemberType.UseStorageRegister)
                {                    
                    DataTable DTstoreStorage = null;// StoreData.GetStoreActualStorageList(storeId);
                    if (DTstoreStorage.Rows.Count == 0)
                        return EnumCheckWheatherRegister.StoreStorageNoStorage;
                    DataRow[] dr;
                    double ProductCount = 0;
                    foreach (OrderProduct _product in OrderGoodsList)
                    {
                        dr = DTstoreStorage.Select("ProductID =" + _product.id);
                        ProductCount += _product.count;

                        if (dr.Length == 0)
                            return EnumCheckWheatherRegister.StoreStorageNoStorage;

                        if (_product.count > Convert.ToInt32(dr[0]["ActualStorage"].ToString()))
                            return EnumCheckWheatherRegister.StoreStorageNotEnough;
                    }
                    //Add Message--make by wangfei 2005-3-14( Target for Set Balance Parameter in order to control the Storage's Regesiter
                    string BalanSql = "select StorageScalar from d_info where StoreID='" + storeId + "'";
                    string StorageMoney = @"select P.PreferentialPrice as Money,D.ActualStorage as Amount
									from d_kucun as D,product as P 
									where P.ProductID = D.ProductID and D.StoreID = '" + storeId + "'"; //获取店铺的库存货的金钱数
                    double StorMoney = 0;
                    SqlDataReader rs = DBHelper.ExecuteReader(StorageMoney);
                    while (rs.Read())
                    {
                        StorMoney += Convert.ToDouble(rs["Money"]) * Convert.ToInt64(rs["Amount"]);
                    }
                    rs.Close();
                    double BalanceScalar = Convert.ToDouble(DBHelper.ExecuteScalar(BalanSql)); //店铺底线报单金额

                    double StorageScalar = StorMoney - currentOrderMoney;
                    if (StorageScalar < BalanceScalar)
                    {
                        return EnumCheckWheatherRegister.StoreStorageNotEnough;
                    }
                    return EnumCheckWheatherRegister.StoreStorageNotEnough;

                }
                else
                    if (Model.Other.WebSiteBaseDataConfig.EnumRegisterMemberType == EnumRegisterMemberType.None)
                    {
                        return EnumCheckWheatherRegister.StoreForBIDRegister;
                    }
                    else if (Model.Other.WebSiteBaseDataConfig.EnumRegisterMemberType == EnumRegisterMemberType.UseMoneyRegister)
                    {
                        string BalanceSql = "select TotalMaxMoney from d_info where StoreID='" + storeId + "'";
                        double BalanceScalar = Convert.ToDouble(DBHelper.ExecuteScalar(BalanceSql)); //店铺没钱时,最大允许录入的报单金额
                        double storeLeftMoney = 0;// StoreData.GetLeftRegisterMemberMoney(storeId) + updateOrderMoney;
                        if (currentOrderMoney > (storeLeftMoney + BalanceScalar))
                            return EnumCheckWheatherRegister.StoreNoMoneyNotEnough;
                        else
                            return EnumCheckWheatherRegister.StoreNoMoneyNotEnough;
                    }
                    else
                        return EnumCheckWheatherRegister.Error;


        }
    }

    
}
