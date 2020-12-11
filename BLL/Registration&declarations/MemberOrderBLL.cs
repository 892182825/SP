using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using BLL.CommonClass;
using System.Data.SqlClient;
using Model;
using System.Data;
using BLL.Logistics;

namespace BLL.Registration_declarations
{
    public class MemberOrderBLL
    {
        public MemberOrderBLL() { }

        MemberOrderDAL memberOrderDAL = new MemberOrderDAL();
        StoreDataDAL storeDataDAL = new StoreDataDAL();

        /// <summary>
        /// 检查店铺银行信息的完整性
        /// </summary>
        /// <returns></returns>
        public StoreInfoModel IsStoreBankInfoFullByStoreId(string storeId)
        {
            StoreInfoModel model = storeDataDAL.GetStoreInfoByStoreId(storeId);
            if (model == null || model.Bank == null || model.BankCard == null || model.BankCard == "" || model.Bank.ToString() == "")
            {
                return null;
            }
            return model;
        }


        /// <summary>
        /// 查看会员订单明细
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public System.Data.DataTable GetMemberDetailsByOrderID(string orderid)
        {
            return memberOrderDAL.GetMemberDetailsByOrderID(orderid);
        }

        /// <summary>
        /// 查看会员订单明细
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public System.Data.DataTable GetMemberDetailsByOrderID(string orderid,int currency)
        {
            return memberOrderDAL.GetMemberDetailsByOrderID(orderid,currency);
        }

        public System.Data.DataTable GetMemberDetailsByOrderID1(string orderid,double currency)
        {
            return memberOrderDAL.GetMemberDetailsByOrderID1(orderid, currency);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public System.Data.DataTable GetMemberByOrderID(string orderid,int currency)
        {
            return memberOrderDAL.GetMemberByOrderID(orderid,currency);
        }

        public static System.Data.DataTable GetBankName(int countryCode)
        {
            return MemberOrderDAL.GetBankName(countryCode);
        }
        public System.Data.DataTable GetMemberByOrderID1(string orderid,double currency)
        {
            return memberOrderDAL.GetMemberByOrderID1(orderid, currency);
        }

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
            memberOrderDAL.getXHFW(bianhao, isAnZhi, qishu, out sXuhao, out eXuhao, out Cengwei);
        }

        /// <summary>
        /// 获取会员报单信息
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public static Model.MemberOrderModel GetMemberOrder(string orderId)
        {
            return MemberOrderDAL.GetMemberOrder(orderId);

        }


        public static DataSet GetAllList(string productid)
        {
            return MemberOrderDAL.GetAllList(productid);
        }

        /// <summary>
        /// 确认订单方法
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public static string PayOrder(string orderId, out bool isPass)
        {
            //BrowseMemberOrdersBLL memberOrderBLL = new BrowseMemberOrdersBLL();
            //CommonDataBLL commonDataBLL = new CommonDataBLL();
            //using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connString"]))
            //{
            //    conn.Open();
            //    SqlTransaction tran = conn.BeginTransaction();
            //    if (e.CommandName == "OK")
            //    {
            //        if (e.CommandArgument != null)
            //        {
            //            try
            //            {                    
            //                //根据订单号获取店铺信息
            //                MemberOrderModel order = GetMemberOrder(orderId);
            //                //根据订单编号获得订单详细
            //                DataTable orderdetails = (new MemberOrderDAL()).GetMemberDetailsByOrderID(order);
            //                //获得会员电子账户剩余金额
            //                object zhifumoney = commonDataBLL.EctIsEnough(order.Number);
            //                //获得会员订单信息
            //                DataTable dt = memberOrderBLL.DeclarationProduct(order.StoreId,order.OrderId);
            //                //获得店铺剩余报单金额
            //                object memberordermoney = commonDataBLL.StoreLaveAmount(order.StoreId);
            //                //是否是电子钱包支付
            //                if (order.DefrayType == 2)
            //                {
            //                    //电子钱包余额是否大于订单金额
            //                    if (Convert.ToDecimal(zhifumoney.ToString()) <order.TotalMoney)
            //                    {
            //                        return "<script language='javascript'>alert('会员" +order.Number+ "的电子帐户不够支付本订单！！！')</script>";
            //                    }
            //                }
            //                else
            //                {
            //                    //店铺剩余可报单额是否大于订单金额
            //                    if (Convert.ToDecimal(memberordermoney) < order.TotalMoney)
            //                    {
            //                        return "<script language='javascript'>alert('店铺" + order.StoreId + "的可报单额不足！！！')</script>";
            //                    }
            //                }
            //                //是否是网上银行支付
            //                if (order.DefrayType == 3)
            //                {
            //                    //Response.Write("<script>location.href='../Send.aspx?V_amount=" + Totalmoney.Value + "&V_oid=" + orderId + "';</script>");
            //                    //return;
            //                }
            //                //获得最大期数
            //                int maxQs = CommonDataBLL.GetMaxqishu();
            //                //更新会员订单信息
            //                if (!BLL.CommonClass.CommonDataBLL.ConfirmMembersOrder(tran,orderId, maxQs))
            //                {                
            //                    tran.Rollback();
            //                    return "<script>alert('支付失败')</script>";
            //                }
            //                //更新结算表
            //                if (!BLL.CommonClass.CommonDataBLL.UPMemberInfoBalance(tran, order.Number,order.totalpv, maxQs))
            //                {                
            //                    tran.Rollback();
            //                    return "<script>alert('结算失败')</script>";
            //                }
            //                //更新店铺库存
            //            //    foreach(DataRow row in orderdetails.Rows)
            //            //    {
            //            //        if (!BLL.CommonClass.CommonDataBLL.UPStoreStock(tran, order.StoreId, Convert.ToInt32(row["productid"].ToString()), 1)) ;
            //            //        {
            //            //            //店铺没有该货物类型的纪录，则添加该类型的记录
            //            //            AuditingMemberOrdersBLL abll = new AuditingMemberOrdersBLL();
            //            //            abll.InsertIntoStock(tran, order.storeid, row["ProductId"].ToString(), row["Quantity"].ToString());
            //            //        }
            //            //    }


            //            //    if (Convert.ToInt32(dt.Rows[selectIndex]["DefrayType"].ToString()) == 2)
            //            //    {
            //            //        //更新会员电子钱包
            //            //        BLL.CommonClass.CommonDataBLL.UPMemberEct(tran, "", Convert.ToDecimal(dt.Rows[selectIndex]["TotalMoney"].ToString()));
            //            //    }
            //            //    //更新报单额

            //                //提交事务
            //                tran.Commit();

            //            }
            //            catch
            //            {
            //                //回滚事务
            //                tran.Rollback();
            //                throw;
            //            }
            //        }
            //    }
            //}
            isPass = false;
            return "";
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
            return MemberOrderDAL.Batch(PayMentMoney, ShouDateTime, OrderID);
        }
        /// <summary>
        /// 是否足够支付订单
        /// </summary>
        /// <param name="OrderID">订单号</param>
        /// <param name="OrderID">店编号</param>
        /// <returns></returns>
        public static bool IsAdequate(string OrderID, string storeId)
        {
            return MemberOrderDAL.IsAdequate(OrderID, storeId);
        }



        /// <summary>
        /// 电子货币支付
        /// </summary>
        public void IsElecPay(SqlTransaction tran, MemberOrderModel memberOrderModel)
        {
            //支付期数
            memberOrderModel.PayExpect = CommonClass.CommonDataBLL.getMaxqishu();
            //生成汇款单号
            memberOrderModel.RemittancesId = Model.Other.MYDateTime.ToYYMMDDHHmmssString();
            //支付状态改为1
            memberOrderModel.DefrayState = 1;

            //电子货币支付时，记录已经支付的金额
            new DAL.AddOrderDataDAL().Upd_ECTPay(tran, memberOrderModel.ElectronicaccountId, Convert.ToDouble(memberOrderModel.TotalMoney) * -1);

            //电子货币支付，则在店汇款中插入记录,最后两个参数需要更改，
            new DAL.AddOrderDataDAL().AddDataTORemittances1(tran, memberOrderModel);

            //更新店铺的汇款
            new DAL.AddOrderDataDAL().Add_Remittances(tran, Convert.ToDouble(memberOrderModel.TotalMoney) * -1, memberOrderModel.StoreId);

        }

        public static string UpdateMemberOrder(string OrderId, IList<MemberDetailsModel> list, MemberOrderModel memberOrderModel, string StoreID)
        {
            SqlConnection conn = new SqlConnection(DBHelper.connString);
            MemberOrderModel order = MemberOrderBLL.GetMemberOrder(memberOrderModel.OrderId);
            SqlTransaction tran = null;
            try
            {
                conn.Open();
                tran = conn.BeginTransaction();

                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = tran;
                cmd.Connection = conn;


                DataTable dhpv = DBHelper.ExecuteDataTable(tran, "select totalpv,LackProductMoney from MemberOrder where OrderID='" + OrderId + "'");

                if (memberOrderModel.PayExpect != -1 && memberOrderModel.DefrayState == 1)
                {
                    if (Convert.ToDecimal(dhpv.Rows[0]["totalpv"]) != memberOrderModel.TotalPv)
                    {
                        //修改后需重新结算
                        cmd.CommandText = "update config set jsflag='0' where ExpectNum>='" + memberOrderModel.PayExpect + "'";
                        cmd.CommandType = CommandType.Text;

                        cmd.ExecuteNonQuery();
                    }
                }

                if (memberOrderModel.DefrayState == 1)
                {
                    decimal oldlackproductmoney = Convert.ToDecimal(dhpv.Rows[0]["LackProductMoney"]);
                    if (memberOrderModel.LackProductMoney != oldlackproductmoney)
                    {
                        D_AccountBLL.AddAccount(StoreID, Convert.ToDouble(oldlackproductmoney), D_AccountSftype.StoreType, D_AccountKmtype.OrderUpdateIn, DirectionEnum.AccountsIncreased, "店铺[" + StoreID + "]修改订单[" + order.OrderId + "]退回钱[" + oldlackproductmoney + "]", tran);
                        //D_AccountBLL.AddAccount(StoreID, Convert.ToDouble(memberOrderModel.LackProductMoney), D_AccountSftype.StoreType, D_AccountKmtype.OrderUpdateOut, DirectionEnum.AccountReduced, "店铺[" + StoreID + "]修改订单[" + memberOrderModel.OrderId + "]扣除钱[" + memberOrderModel.LackProductMoney + "]", tran);

                        if (order.DefrayType == 2)
                        {
                            new MemberOrderBLL().IsElecPay(tran, order);
                            D_AccountBLL.AddAccount(order.Number, Convert.ToDouble(order.LackProductMoney), D_AccountSftype.MemberType, D_AccountKmtype.OrderUpdateIn, DirectionEnum.AccountsIncreased, "会员【" + order.Number + "】报单修改现金扣添加，订单号为【" + order.OrderId + "】", tran);
                            D_AccountBLL.AddAccount(order.StoreId, Convert.ToDouble(order.LackProductMoney), D_AccountSftype.StoreType, D_AccountKmtype.OrderUpdateOut, DirectionEnum.AccountReduced, "会员【" + order.Number + "】报单修改现金添加扣，订单号为【" + order.OrderId + "】", tran, true);
                        }
                    }
                }

                int res = 0;

                SqlParameter[] del_parm = { 
                                                new SqlParameter("@OrderID", OrderId),
                                                new SqlParameter("@StoreID", StoreID),
                                                new SqlParameter("@Type", 1),
                                                new SqlParameter("@res", res),
                                                new SqlParameter("@opnum", memberOrderModel.OperateNumber),
                                                new SqlParameter("@opip", memberOrderModel.OperateIp),
                                          };

                BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("MemberOrder", "ltrim(rtrim(OrderID))");
                cl_h_info.AddRecordtran(tran, OrderId);

                BLL.CommonClass.ChangeLogs cl_h_info1 = new BLL.CommonClass.ChangeLogs("MemberDetails", "ltrim(rtrim(OrderID))");
                cl_h_info1.AddRecordtran(tran, OrderId);

                DBHelper.ExecuteNonQuery(tran, "Delete_H_Order", del_parm, CommandType.StoredProcedure);

                if (Convert.ToInt32(del_parm[3].Value) == 0)
                {
                    //添加订单
                    new AddOrderBLL().SaveHOrder(tran, list, memberOrderModel);

                    cl_h_info.AddRecordtran(tran, OrderId);
                    cl_h_info1.AddRecordtran(tran, OrderId);

                    if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar(tran, "select count(1) from MemberDetails where OrderId='" + OrderId + "'", CommandType.Text)) > 0)
                    {
                        cl_h_info.ModifiedIntoLogstran(tran, ChangeCategory.Order, OrderId, ENUM_USERTYPE.objecttype5);
                        cl_h_info1.ModifiedIntoLogstran(tran, ChangeCategory.Order, OrderId, ENUM_USERTYPE.objecttype5);
                    }

                    tran.Commit();
                    return "1";
                }
                else
                {
                    tran.Rollback();
                    return "-2";
                }
            }
            catch (Exception ee)
            {
                tran.Rollback();
                return "-2";
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}