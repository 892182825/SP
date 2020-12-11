using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BLL.Logistics;
using Model;
using DAL;
using Model.Orders;

namespace BLL.OrderBLL
{
    public class OrderBLL
    {
        public OrderBLL()
        { 
        }

        public static bool QPOrders(Model.QuickPay.quickPayParames qpp)
        {
            string ExSql = string .Empty ;
            bool flag = false;
            string notify_id = qpp.DealId;
            string TotalMoney = Convert.ToString(qpp.PayAmount / 100);  //定单额
            string hkid = qpp.OrderId;                                  //如果是汇款
            string BankId = qpp.BankId;                                 //银行代码
            string body = qpp.ProductDesc;                              //商品描述       
            string orderID = qpp.OrderId;                               //如果是订单支付
            Model.Enum_RemittancesType RemitType = (Model.Enum_RemittancesType)int.Parse(qpp.Ext1);
            SqlParameter[] spas = new SqlParameter[] { 
            new SqlParameter ("@PayWay",SqlDbType .Int ),
            new SqlParameter ("@Remittancesid",SqlDbType .NVarChar ,20),
            new SqlParameter ("@PayExpectNum",SqlDbType .Int),
            new SqlParameter ("@ReceivablesDate",SqlDbType.DateTime)
            
            };
            spas[0].Value = 4;
            spas[1].Value = qpp.OrderId;
            spas[2].Value =BLL .CommonClass .CommonDataBLL.getMaxqishu() ;
            spas[3].Value = DateTime.Now;

            int var = 0;
            SqlTransaction tran = null;
            SqlConnection con = DAL.DBHelper.SqlCon();
            try
            {
                con.Open();
                tran = con.BeginTransaction();

                if (RemitType == Model.Enum_RemittancesType.enum_StoreRemittance)
                {
                    Model.RemittancesModel model=BLL.MoneyFlows.RemittancesBLL.GetRemitByHuidan(qpp.OrderId);
                    
                    string type = "";
                    ExSql = "update Remittances set IsGSQR=1,PayWay=@PayWay,PayExpectNum=@PayExpectNum,ReceivablesDate=@ReceivablesDate where Remittancesid=@Remittancesid";
                    
                    switch (model.Use)
                    {
                        case 1: type = string.Format("TotalAccountMoney = TotalAccountMoney+{0}",TotalMoney); break;
                        case 2: type = string.Format("TurnOverMoney=TurnOverMoney+{0}",TotalMoney); break;
                        case 3: type = string.Format("TotalInvestMoney=TotalInvestMoney+{0}",TotalMoney); break;
                        case 4: type = string.Format("OtherMoney=OtherMoney+{0}",TotalMoney); break;
                    }
                    string upstore = "";
                    if (type != "")
                    {
                        upstore = string.Format(" update StoreInfo set {0} where StoreID='{1}'",type,model.RemitNumber);
                    }
                    else
                    {
                        flag = false;
                        tran.Rollback();
                        return flag;
                    }
                    ExSql += upstore;
                    //加入对账单
                    D_AccountBLL.AddAccount(model.RemitNumber, double.Parse(TotalMoney), D_AccountSftype.StoreType, D_AccountKmtype.RechargeByOnline, DirectionEnum.AccountsIncreased, BLL.Translation.Translate("006712", "店铺快钱支付"), tran);
                }
                else if (RemitType == Model.Enum_RemittancesType.enum_MemberRemittance)
                {
                    ExSql = "update MemberRemittances set IsGSQR=1,PayWay=@PayWay,PayExpectNum=@PayExpectNum,ReceivablesDate=@ReceivablesDate where Remittancesid=@Remittancesid";
                    Model.RemittancesModel model = BLL.MoneyFlows.RemittancesBLL.GetMemberRemittances(qpp.OrderId);
                    string upmember = string.Format(" update MemberInfo set TotalRemittances = TotalRemittances+{0} where Number='{1}'",TotalMoney,model.RemitNumber);
                    ExSql += upmember;
                    D_AccountBLL.AddAccount(model.RemitNumber, double.Parse(model.RemitMoney.ToString()), D_AccountSftype.MemberType, D_AccountKmtype.RechargeByOnline, DirectionEnum.AccountsIncreased, BLL.Translation.Translate("006713", "会员快钱支付"), tran);
                }
                var = DAL.DBHelper.ExecuteNonQuery(tran, ExSql, spas, CommandType.Text);
                if (var > 0)
                {
                    flag = true;
                    tran.Commit();
                }
                else
                {
                    flag = false;
                    tran.Rollback();
                }
            }
            catch (Exception ex)
            {
                flag = false;
                tran.Rollback();
            }
            return flag;
        }
        /// <summary>
        /// 不管验证成功，都记录访问记录
        /// </summary>
        /// <param name="responseTxt">访问成功与否</param>
        /// <returns></returns>
        public static bool QPAccessRecord(Model .QuickPay .quickPayParames  qpp)
        {
            bool flag = false;
            try
            {
                string notify_id = qpp.DealId;
                string TotalMoney = Convert.ToString(qpp.PayAmount / 100);//定单额
                string hkid = qpp.OrderId;   //汇款的订单号
                string BankId = qpp.BankId;    //流水号
                string body = qpp.ProductDesc;         //商品描述             

                string Msg = "dealid=" + notify_id + "&hkid=" + hkid + "&BankID=" + BankId + "&body=" + body + "Ext1=" + qpp.Ext1 + "&Ext2=" + qpp.Ext2 + "&OrderAmount=" + qpp.OrderAmount+"&PayAccount="+qpp.PayAmount;
 
                string ExSql = "insert into T_quickPayRecord(msg,happenDate) "
                              + "values('" + Msg + "',getdate())";
                int var = DAL.DBHelper.ExecuteNonQuery(ExSql);
                if (var > 0)
                    flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }


        /// <summary>
        /// 对店铺订单的锁定与解锁
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public bool LuckOrUnlockStoreOrderByOrderID(string orderid, ref string msg)
        {
            bool flag = false;
            try
            {
                OrderDAL orderDal = new OrderDAL();
                flag = orderDal.LuckOrUnlockStoreOrderByOrderID(orderid);
                if (!flag)
                {
                    msg = "操作失败！";
                }
            }
            catch (Exception ex)
            {
                msg = "操作失败：" + ex.Message;
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 对会员订单的锁定与解锁
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public bool LuckOrUnlockMemberOrderByOrderID(string orderid, ref string msg)
        {
            bool flag = false;
            try
            {
                OrderDAL orderDal = new OrderDAL();
                flag = orderDal.LuckOrUnlockMemberOrderByOrderID(orderid);
                if (!flag)
                {
                    msg = "操作失败！";
                }
            }
            catch (Exception ex)
            {
                msg = "操作失败：" + ex.Message;
                flag = false;
            }
            return flag;
        }


        public MemberOrderEntity CreateEntityByOrderID(string orderid, ref string msg)
        {
            try
            {
                MemberOrderEntity orderEntity = new MemberOrderEntity();
                OrderDAL orderDal = new OrderDAL();
                var order = MemberOrderDAL.GetMemberOrder(orderid);
                if (order == null)
                    return null;
                orderEntity.Number = order.Number;
                orderEntity.OrderDate = order.OrderDate;
                orderEntity.OrderID = order.OrderId;
                orderEntity.TotalMoney =Convert .ToDouble (order.TotalMoney);
                orderEntity.TotalPv = Convert.ToDouble(order.TotalPv);
                orderEntity.TotalMoneyReturned = Convert.ToDouble(order.TotalMoneyReturned);
                orderEntity.TotalPvReturned = Convert.ToDouble(order.TotalPvReturned);     
                List<MemberOrderDetailsEntity> details = orderDal.GetMemberOrderDetailsEntity(orderid);
                orderEntity.Details = details;
                return orderEntity;
            }
            catch (Exception ex)
            {
                msg = "生成错误："+ex.Message;
                return null;
            }
        }
    }
}
