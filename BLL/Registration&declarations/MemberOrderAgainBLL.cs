using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Configuration;
using BLL.CommonClass;
using Model.Other;
using BLL.Logistics;
/**
 * 
 * 
 * 时间：2009.9.3
 * 功能：复消
 */
namespace BLL.Registration_declarations
{
    public class MemberOrderAgainBLL
    {
        RegistermemberBLL registermemberBLL = new RegistermemberBLL();
        AddOrderDataDAL addOrderDataDAL = new AddOrderDataDAL();
        AddOrderBLL addOrderBLL = new AddOrderBLL();
        OrderDAL orderDAL = new OrderDAL();


        //
        /// <summary>
        /// 验证该编号是否存在
        /// </summary>
        /// <param name="number"></param>
        public int CheckNuberIsExist(string number)
        {
            return addOrderDataDAL.NuberIsExist(number);
        }


        /// <summary>
        /// 审核支付报单
        /// </summary>
        /// <param name="orderid"> 报单编号 </param>
        /// <returns>是否审核成功</returns>
        public static string AuditingOrder(string orderid)
        {
            string error = "";//返回错误信息

            MemberOrderModel mo = MemberOrderDAL.GetMemberOrder(orderid);//获取报单信息

            //判断报单是否支付
            if (mo.DefrayState != 0)
            {
                return "报单不可重复确认！";
            }

            //获得店铺不足货时可以报单的金额
            if (mo.DefrayType == 2)
            {
                double emoney = new AddOrderDataDAL().HaveMoney(mo.ElectronicaccountId);
                if (Convert.ToDouble(mo.TotalMoney) > emoney)
                {
                    return "电子账户余额不足，不能确认！";
                }
            }

            //更改--报单信息
            mo.RemittancesId = MYDateTime.ToYYMMDDHHmmssString();
            mo.DefrayState = 1;
            mo.PayExpect = CommonDataBLL.getMaxqishu();

            List<MemberDetailsModel> list = ViewFuXiaoBLL.GetDetails(mo.OrderId);//获取订单明细
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    //更新会员订单信息
                    CommonDataBLL.ConfirmMembersOrder(tran, mo.OrderId, mo.PayExpect);

                    //更新店铺库存
                    foreach (MemberDetailsModel memberDetailsModel in list)
                    {//循环根据订单明细跟新库存
                        int result = ViewFuXiaoBLL.UptStock(tran, memberDetailsModel.StoreId, memberDetailsModel.ProductId, memberDetailsModel.Quantity, memberDetailsModel.NotEnoughProduct);

                        //如果该店铺无盖商品记录，则在店库存表中加记录
                        if (result <= 0)
                        {
                            new BrowseMemberOrdersBLL().updateStore2(memberDetailsModel, tran);
                        }
                    }

                    //更新店铺报单款
                    new AddOrderDataDAL().updateStore3(mo.StoreId, tran, Convert.ToDouble(mo.TotalMoney));

                    if (mo.DefrayType == 2)
                    {
                        //更新电子账户余额
                        AddOrderDataDAL.UpdateECTPay(tran, mo.ElectronicaccountId, mo.TotalMoney);

                        //更新店铺汇款
                        new AddOrderDataDAL().Add_Remittances(tran, Convert.ToDouble(mo.TotalMoney), mo.StoreId);

                        //插入汇款信息
                        new AddOrderDataDAL().AddDataTORemittances(tran, mo);
                    }

                    //更新会员业绩
                    new AddOrderDataDAL().Js_addfuxiao(mo.Number, Convert.ToDouble(mo.TotalPv), mo.PayExpect, mo.DefrayType, tran);//添加网络业绩

                    //插入订货单
                    AddOrderGoods(list, tran, mo);

                    //提交事务
                    tran.Commit();
                }
                catch
                {
                    error = "确认失败！";
                    tran.Rollback();
                }
                finally
                {
                    conn.Close();
                }
            }

            return error;
        }

        /// <summary>
        /// 添加订货单
        /// </summary>
        /// <param name="list"></param>
        public static void AddOrderGoods(List<MemberDetailsModel> list, SqlTransaction tran, MemberOrderModel memberOrderModel)
        {
            //报单生成订单
            int count = 0;
            decimal totalMoney = 0;
            decimal totalPv = 0;
            foreach (MemberDetailsModel mDetails in list)
            {
                if (mDetails.NotEnoughProduct > 0)
                {
                    totalMoney += mDetails.NotEnoughProduct * mDetails.Price;
                    totalPv += mDetails.NotEnoughProduct * mDetails.Pv;
                    count++;
                }
            }
            if (count > 0)
            {
                string orderId = "";
                orderId = BLL.Logistics.OrderGoodsBLL.GetNewOrderID();
                OrderGoodsMedel storeItem = new AddOrderBLL().GetOrderModel(orderId, memberOrderModel, tran, totalMoney, totalPv);
                new AddOrderBLL().OrderSubmit(memberOrderModel.OrderId.ToString(), list, storeItem, tran);
            }
        }

        /// <summary>
        /// 添加订单信息
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="memberOrderModel"></param>
        /// <param name="totalPv"></param>
        /// <param name="except"></param>
        /// <param name="memberDetailsModel"></param>
        /// <param name="storeInfoModel"></param>
        public static bool AddOrderData(bool isEdit, MemberOrderModel memberOrderModel, IList<MemberDetailsModel> list)
        {
            bool state = false;
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    //如果是编辑
                    if (isEdit)
                    {
                        BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("memberorder", "orderid");//实例日志类
                        cl_h_info.AddRecordtran(tran, memberOrderModel.OrderId);//添加日志，修改前记录原来数据

                        MemberOrderModel order = MemberOrderBLL.GetMemberOrder(memberOrderModel.OrderId);

                        if (order.LackProductMoney > 0)
                        {
                            D_AccountBLL.AddAccount(order.StoreId, Convert.ToDouble(order.LackProductMoney), D_AccountSftype.StoreType, D_AccountKmtype.OrderUpdateIn, DirectionEnum.AccountsIncreased, "会员【" + order.Number + "】报单修改现金扣添加，订单号为【" + order.OrderId + "】", tran);
                            if (order.DefrayType == 2)
                            {
                                new Registration_declarations.MemberOrderAgainBLL().IsElecPay(tran, order);
                                D_AccountBLL.AddAccount(order.Number, Convert.ToDouble(order.LackProductMoney), D_AccountSftype.MemberType, D_AccountKmtype.OrderUpdateIn, DirectionEnum.AccountsIncreased, "会员【" + order.Number + "】报单修改现金扣添加，订单号为【" + order.OrderId + "】", tran);
                                D_AccountBLL.AddAccount(order.StoreId, Convert.ToDouble(order.LackProductMoney), D_AccountSftype.StoreType, D_AccountKmtype.OrderUpdateOut, DirectionEnum.AccountReduced, "会员【" + order.Number + "】报单修改现金添加扣，订单号为【" + order.OrderId + "】", tran, true);
                            }
                        }

                        int delResult = new AddOrderDataDAL().Del_Horder(tran, memberOrderModel.OrderId, memberOrderModel.StoreId, CommonDataBLL.OperateBh, CommonDataBLL.OperateIP);

                        if (memberOrderModel.DefrayState == 1)//店铺复消减去业绩
                        {
                            int js_delfuxiao_Result = new AddOrderDataDAL().Js_delfuxiao(memberOrderModel.Number, Convert.ToDouble(memberOrderModel.TotalPv), memberOrderModel.PayExpect, memberOrderModel.DefrayState, tran);
                        }

                        cl_h_info.ModifiedIntoLogstran(tran, ChangeCategory.Order, memberOrderModel.OrderId, ENUM_USERTYPE.objecttype5);//插入日志
                    }

                    //添加订单
                    new AddOrderBLL().SaveHOrder(tran, list, memberOrderModel);

                    //顾客购物业绩上传(注意支付money要改)
                    if (memberOrderModel.DefrayState == 1)
                    {
                        new AddOrderDataDAL().Js_addfuxiao(memberOrderModel.Number, Convert.ToDouble(memberOrderModel.TotalPv), memberOrderModel.PayExpect, memberOrderModel.DefrayState, tran);
                        CommonDataBLL.SetMemberLevel(tran, memberOrderModel.Number, memberOrderModel.OrderId);
                    }
                    state = true;
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            return state;
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
            addOrderDataDAL.Upd_ECTPay(tran, memberOrderModel.ElectronicaccountId, Convert.ToDouble(memberOrderModel.TotalMoney) * -1);

            //电子货币支付，则在店汇款中插入记录,最后两个参数需要更改，
            addOrderDataDAL.AddDataTORemittances1(tran, memberOrderModel);

            //更新店铺的汇款
            addOrderDataDAL.Add_Remittances(tran, Convert.ToDouble(memberOrderModel.TotalMoney) * -1, memberOrderModel.StoreId);

        }

        /// <summary>
        /// 如果是修改则把数据填充到页面上
        /// </summary>
        /// <param name="memberOrderModel">memberOrderModel类对象</param>
        /// <param name="storeID">店id</param>
        /// <param name="orderID">订单Id</param>
        public void WriterDataToPage(MemberOrderModel memberOrderModel, string storeID, string orderID)
        {
            addOrderDataDAL.WriterDataToPage(memberOrderModel, storeID, orderID);
        }

        /// <summary>
        /// 读取会员信息
        /// </summary>
        /// <param name="number">会员</param>
        /// <param name="storeId">店铺编号</param>
        /// <returns>返回会员信息</returns>
        public static MemberInfoModel GetMemberInfoByNumber(string number)
        {
            return AddOrderDataDAL.GetMemberInfoByNumber(number);
        }

        /// <summary>
        /// 获取报单信息
        /// </summary>
        /// <param name="OrderId">报单编号</param>
        /// <returns>单据信息</returns>
        public static MemberOrderModel GetMemberOrderByOrderId(string OrderId)
        {
            return AddOrderDataDAL.SelectMemberOrderByOrderId(OrderId);
        }

        /// <summary>
        /// 检测会员是否存在
        /// </summary>
        /// <param name="number"> 会员编号 </param>
        /// <returns>返回是否存在</returns>
        public static bool CheckMemberExist(string number)
        {
            return MemberInfoDAL.SelectMemberExist(number);
        }

        /// <summary>
        /// 检测会员是否存在
        /// </summary>
        /// <param name="number"> 会员编号 </param>
        /// <returns>返回是否存在</returns>
        public static bool CheckMemberZx(string number)
        {
            return MemberInfoDAL.CheckMemberZx(number);
        }

        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <param name="orderID">订单号</param>
        /// <returns>确认后的订单号</returns>
        public static string GetOrderInfo(bool isedit, string orderId)
        {
            string strOrderId = "";
            if (!isedit)//编辑状态
            {
                strOrderId = new OrderDAL().GetNewOrderID();
                if (OrderDAL.CheckOrderIdExists(strOrderId))
                {
                    GetOrderInfo(isedit, orderId);
                }
            }
            else
            {
                strOrderId = orderId;
            }

            return strOrderId;
        }

        /// <summary>
        /// 分析购物条件
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="prevJine"></param>
        /// <param name="notEnProMoney">用户选择货物不足时，计算出的不足数量*不足数量单价</param>
        /// <param name="orderID"></param>
        /// <param name="storeID"></param>
        /// <param name="list"></param>
        /// <param name="selectValue"></param>
        public static string CheckOption(bool isEdit, double notEnProMoney, string orderID, string storeID, IList<MemberDetailsModel> list, string selectValue, out double storeLeftMoney2, out double prvMoney2, out bool judge)
        {
            judge = true;
            //storeDataDAL数据类对象
            StoreDataDAL storeDataDAL = new StoreDataDAL();
            //上次报单金额
            double prevMoney = 0;
            //支付状态
            int defreyState = 0;
            //错误信息
            string msg = null;
            //如果是编辑
            if (isEdit)
            {
                //补回上次该报单上传不足货物时补交的钱
                prevMoney += new AddOrderDataDAL().NeedReturnMoney(orderID, storeID);

            }
            //需要修改    

            double storeLeftMoney = storeDataDAL.GetLeftRegisterMemberMoney(storeID) + prevMoney;
            //将店剩余金额带出方法
            storeLeftMoney2 = storeLeftMoney;
            //将上次报单的费用带出方法
            prvMoney2 = prevMoney;
            //判断是否已经注册

            //得到
            double currentMoney = new RegistermemberBLL().getZongJing(list);
            double bottonLine = new AddOrderBLL().GetBottomLine();

            //如果报单额不足
            if (notEnProMoney > storeLeftMoney)
            {
                if (selectValue == "1")
                {
                    msg = "alert('" + BLL.Translation.Translate("006018", "对不起，您的报单额不足！") + "');";
                    judge = false;
                }

            }
            //没有库存
            if (currentMoney < bottonLine)
            {
                msg = "alert('" + BLL.Translation.Translate("006019", "对不起，报单金额必须大于等于报单底线") + " " + bottonLine + "');";
                judge = false;
            }

            return msg;
        }

        /// <summary>
        /// 根据报单编号获取店铺编号
        /// </summary>
        /// <param name="orderid">报单编号</param>
        /// <returns>返回店铺编号</returns>
        public static string GetStoreIdByOrderId(string orderid)
        {
            return MemberOrderDAL.GetStoreIdByOrderId(orderid);
        }


        /// <summary>
        /// 验证电子账户密码
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="pass">账户密码</param>
        /// <returns>是否正确</returns>
        public static bool CheckEctPassWord(string number, string pass)
        {
            return MemberInfoDAL.CheckEctPassWord(number, pass) == 1;//如果返回1行记录会员电子账户密码正确
        }


        /// <summary>
        /// 获取店铺汇率--根据店铺汇率转换成标准币种
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns>汇率</returns>
        public static double GetBzMoney(string storeid, double totalmoney)
        {
            return AddFreeOrderDAL.GetBzMoney(storeid, totalmoney);
        }

        /// <summary>
        /// 获取店铺汇率
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns>标准币种金额</returns>
        public static double GetBzType(string storeid)
        {
            return AddFreeOrderDAL.GetBzType(storeid);
        }

        /// <summary>
        /// 根据币种类型获取汇率
        /// </summary>
        /// <param name="id">币种ID</param>
        /// <returns>汇率</returns>
        public static double GetBzHl(int id)
        {
            return AddFreeOrderDAL.GetBzHl(id);
        }

        /// <summary>
        /// 获取店铺汇率代码
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns>标准币种金额</returns>
        public static int GetBzTypeId(string storeid)
        {
            return AddFreeOrderDAL.GetBzTypeId(storeid);
        }

        /// <summary>
        /// 获取店铺汇率代码
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns>标准币种金额</returns>
        public static int GetBzTypeId(string storeid, SqlTransaction tran)
        {
            return AddFreeOrderDAL.GetBzTypeId(storeid, tran);
        }

        /// <summary>
        /// 判断店铺编号是否存在
        /// </summary>
        /// <param name="storeid">店铺编号</param>
        /// <returns></returns>
        public static bool CheckStore(string storeid)
        {
            return StoreInfoDAL.CheckStoreId(storeid);
        }

        /// <summary>
        /// 更具会员编号获取所在店铺
        /// </summary>
        public static string GetStoreIdByNumber(string num)
        {
            return MemberInfoDAL.GetStoreIdByNumber(num);
        }

        /// <summary>
        /// 根据产品编号获取该产品是否销售
        /// </summary>
        /// <param name="proid"></param>
        /// <returns></returns>
        public static string GetIsSellByProId(int proid)
        {
            return ProductDAL.GetIsSellByProId(proid);
        }
        /// <summary>
        /// 查看会员订单明细
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static DataTable GetMemberDetailsByOrderID(string orderid)
        {
            return new MemberOrderDAL().GetMemberDetailsByOrderID(orderid);
        }
        /// <summary>
        /// 查看会员订单明细
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static DataTable GetMemberDetailsByOrderID(string orderid,int currency)
        {
            return new MemberOrderDAL().GetMemberDetailsByOrderID(orderid,currency);
        }

        /// <summary>
        /// 根据店铺获取库存实际数量
        /// </summary>
        /// <param name="proid"></param>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public static int GetCountByProIdAndStoreId(int proid, string storeid)
        {
            return StockDAL.GetCountByProIdAndStoreId(proid, storeid);
        }

    }
}