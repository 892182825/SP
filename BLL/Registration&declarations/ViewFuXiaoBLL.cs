using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DAL;
using Model;
using BLL.Logistics;
using BLL.CommonClass;

namespace BLL.Registration_declarations
{

    /**
     * 当前期数还要修改
     * 
     * 
     */
    public class ViewFuXiaoBLL
    {
        AddOrderDataDAL addOrderDataDAL = new AddOrderDataDAL();
        BrowsememberordersDAL browsememberordersDAL = new BrowsememberordersDAL();

        /// <summary>
        /// 删除复消单
        /// </summary>
        public string DelOredrAgain(string orderId, double totalPv, string number, int except, string storeId)
        {
            double totalMoney = AddOrderDataDAL.GetTotalMoneyByOrderId(orderId);
            MemberOrderModel order = MemberOrderDAL.GetMemberOrder(orderId);
            string info = null;
            using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("memberorder", "orderid");//实例日志类
                    cl_h_info.AddRecordtran(tran, orderId);//添加日志，修改前记录原来数据

                    //如果该订单不存在
                    if (!BrowsememberordersDAL.CheckOrderIdExists(orderId))
                    {
                        info = BLL.Translation.Translate("001661", "抱歉，该订单不存在！");
                        tran.Rollback();
                        conn.Close();
                        return info;
                    }

                    if (order.DefrayState == 1)
                    {
                        if (totalMoney > 0)
                        {
                            D_AccountBLL.AddAccount(storeId, totalMoney, D_AccountSftype.StoreType, D_AccountKmtype.OrderDelete, DirectionEnum.AccountsIncreased, "会员【" + number + "】报单删除现金扣添加，订单号为【" + orderId + "】", tran);
                            if (order.DefrayType == 2)
                            {
                                IsElecPay(tran, order);
                                D_AccountBLL.AddAccount(order.Number, Convert.ToDouble(order.TotalMoney), D_AccountSftype.MemberType, D_AccountKmtype.OrderUpdateIn, DirectionEnum.AccountsIncreased, "会员【" + order.Number + "】报单删除现金扣添加，订单号为【" + order.OrderId + "】", tran);
                                D_AccountBLL.AddAccount(order.StoreId, Convert.ToDouble(order.TotalMoney), D_AccountSftype.StoreType, D_AccountKmtype.OrderUpdateOut, DirectionEnum.AccountReduced, "会员【" + order.Number + "】报单删除现金添加扣，订单号为【" + order.OrderId + "】", tran, true);
                            }
                        }

                        int result = addOrderDataDAL.Js_delfuxiao(number, totalPv, except, 1, tran);
                    }

                    //删除复消单
                    addOrderDataDAL.Del_Horder(tran, orderId, storeId, CommonDataBLL.OperateBh, CommonDataBLL.OperateIP);

                    cl_h_info.DeletedIntoLogstran(tran, BLL.CommonClass.ChangeCategory.store1, orderId, BLL.CommonClass.ENUM_USERTYPE.objecttype5);//插入日志
                    tran.Commit();
                }
                catch
                {
                    info = BLL.Translation.Translate("000417", "删除失败！");
                    tran.Rollback();
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return info;
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
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="storeId"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public int UpdateStockAndMoney(string orderId, string storeId, SqlTransaction tran)
        {
            return BrowsememberordersDAL.UpdateStockAndMoney(orderId, storeId, tran);
        }

        public static double GetNotEnoughMoney(string orderId, SqlTransaction tran)
        {
            return BrowsememberordersDAL.GetNotEnoughMoney(orderId, tran);
        }

        /// <summary>
        /// 查找报单明细
        /// </summary>
        /// <returns></returns>
        public static List<MemberDetailsModel> GetDetails(string orderId)
        {
            return BrowsememberordersDAL.GetDetails(orderId);
        }

        /// <summary>
        /// 循环将更新库存
        /// </summary>
        /// <param name="stordId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <param name="notEnoughProduct"></param>
        /// <returns></returns>
        public static int UptStock(SqlTransaction tran, string stordId, int productId, int quantity, int notEnoughProduct)
        {
            return BrowsememberordersDAL.UptStock(tran, stordId, productId, quantity, notEnoughProduct);
        }

        public PagerParmsInit QueryWhere(string volume, string storeId, string condition, string compare, string content, string iszf, int expectType)
        {
            PagerParmsInit model = new PagerParmsInit();
            model.PageIndex = 0;
            model.PageSize = 10;
            model.Key = "B.OrderDate desc";
            model.ControlName = "gv_browOrder";
            model.PageTable = "MemberInfo as A,MemberOrder as B";
            model.PageColumn = "B.SendWay,B.DefrayType,B.DefrayState,B.OrderType,B.remark,B.totalPv,B.totalMoney,B.PayExpectNum,B.orderExpectNum,B.number,B.OrderID,A.name,A.PetName, B.OrderDate,"
                //+ " case when B.DefrayState = 0 then '" + BLL.Translation.Translate("000521", "未支付") + "'"
                //+ "     when  B.DefrayState = 1 then '" + BLL.Translation.Translate("000517", "已支付") + "'"
                //+ " else ''"
                //+ " end as zhifu,"
                              + " case when B.Error = '' then '" + BLL.Translation.Translate("000221", "无") + "' end as Error,"
                              + " (select Name from MemberInfo as anzhiName where anzhiName.number=A.Placement) as anzhiName, "
                              + " (select Name from MemberInfo as tuijianName where tuijianName.number=A.Direct) as tuijianName";
            if (expectType == 0)
            {
                model.SqlWhere = " B.Number=A.Number and B.IsAgain = 1 and OrderType!=22 and OrderType!=25 and  B.DefrayState>-2 and B.OrderExpectNum=" + volume + " and ordertype<>11 and B.StoreID='" + storeId + "'";
            }
            else
            {
                model.SqlWhere = " B.Number=A.Number and B.IsAgain = 1 and OrderType!=22 and OrderType!=25 and  B.DefrayState>-2 and B.PayExpectNum=" + volume + " and ordertype<>11 and B.StoreID='" + storeId + "'";
            }
            if (condition.Length > 0)
            {
                switch (compare.Trim())
                {
                    case "all": break;
                    case "like":
                        if (content.Length == 0)
                        {
                            model.ErrInfo = BLL.Translation.Translate("001662", "请输入字符!");
                            return model;
                        }
                        else
                            model.SqlWhere += " and  " + condition + " like '%" + content + "%'";
                        break;
                    case "not like":
                        if (content.Length == 0)
                        {
                            model.ErrInfo = BLL.Translation.Translate("001662", "请输入字符!");
                            return model;
                        }
                        else
                            model.SqlWhere += " and  " + condition + " not like '%" + content + "%'";
                        break;
                    case "allErr": model.SqlWhere += " and  " + condition + "<>''"; break;
                    default:
                        try
                        {
                            Convert.ToDouble(content);
                        }
                        catch
                        {
                            model.ErrInfo = BLL.Translation.Translate("001665", "请输入数值!");
                            return model;

                        }
                        model.SqlWhere += " and  " + condition + compare + content;
                        break;
                }
            }
            if (iszf != "-1")
            {
                model.SqlWhere += " and  " + iszf;
            }

            return model;
        }
        public PagerParmsInit QueryWhereSJ(string volume, string storeId, string condition, string compare, string content, string iszf, int expectType)
        {
            PagerParmsInit model = new PagerParmsInit();
            model.PageIndex = 0;
            model.PageSize = 10;
            model.Key = "B.OrderDate desc";
            model.ControlName = "gv_browOrder";
            model.PageTable = "MemberInfo as A,MemberOrder as B";
            model.PageColumn = "B.SendWay,B.DefrayType,B.DefrayState,B.OrderType,B.remark,B.totalPv,B.totalMoney,B.PayExpectNum,B.orderExpectNum,B.number,B.OrderID,A.name,A.PetName, B.OrderDate,"
                              + " case when B.ordertype=1 then '" + BLL.Translation.Translate("001445", "店铺复消") + "'"
                              + "      when  B.ordertype = 2 then '" + BLL.Translation.Translate("001448", "会员复消") + "'"
                              + "      when  B.ordertype = 11 then '" + BLL.Translation.Translate("000000", "店铺升级") + "'"
                              + "      when  B.ordertype = 6 then '" + BLL.Translation.Translate("000000", "会员升级") + "'"
                              + " else  '' "
                              + " end as fuxiaoName,   "
                              + " case when B.defraytype=1 then '" + BLL.Translation.Translate("000699", "现金") + "'"
                              + "      when B.defraytype = 2 then '" + BLL.Translation.Translate("001672", "电子转帐") + "'"
                              + "      when  B.defraytype = 3 then '" + BLL.Translation.Translate("000968", "支付宝") + "'"
                              + "      when  B.defraytype = 4 then '" + BLL.Translation.Translate("000983", "快钱支付") + "'"
                              + " else  ''"
                              + " end as defrayname,  "
                //+ " case when B.DefrayState = 0 then '" + BLL.Translation.Translate("000521", "未支付") + "'"
                //+ "     when  B.DefrayState = 1 then '" + BLL.Translation.Translate("000517", "已支付") + "'"
                //+ " else ''"
                //+ " end as zhifu,"
                              + " case when B.Error = '' then '" + BLL.Translation.Translate("000221", "无") + "' end as Error,"
                              + " (select Name from MemberInfo as anzhiName where anzhiName.number=A.Placement) as anzhiName, "
                              + " (select Name from MemberInfo as tuijianName where tuijianName.number=A.Direct) as tuijianName";
            if (expectType == 0)
            {
                model.SqlWhere = " B.Number=A.Number and B.IsAgain = 1 and OrderType=11  and B.OrderExpectNum=" + volume + " and B.StoreID='" + storeId + "'";
            }
            else
            {
                model.SqlWhere = " B.Number=A.Number and B.IsAgain = 1 and OrderType=11   and B.PayExpectNum=" + volume + " and B.StoreID='" + storeId + "'";
            }
            if (condition.Length > 0)
            {
                switch (compare.Trim())
                {
                    case "all": break;
                    case "like":
                        if (content.Length == 0)
                        {
                            model.ErrInfo = BLL.Translation.Translate("001662", "请输入字符!");
                            return model;
                        }
                        else
                            model.SqlWhere += " and  " + condition + " like '%" + content + "%'";
                        break;
                    case "not like":
                        if (content.Length == 0)
                        {
                            model.ErrInfo = BLL.Translation.Translate("001662", "请输入字符!");
                            return model;
                        }
                        else
                            model.SqlWhere += " and  " + condition + " not like '%" + content + "%'";
                        break;
                    case "allErr": model.SqlWhere += " and  " + condition + "<>''"; break;
                    default:
                        try
                        {
                            Convert.ToDouble(content);
                        }
                        catch
                        {
                            model.ErrInfo = BLL.Translation.Translate("001665", "请输入数值!");
                            return model;

                        }
                        model.SqlWhere += " and  " + condition + compare + content;
                        break;
                }
            }
            if (iszf != "-1")
            {
                model.SqlWhere += " and  " + iszf;
            }

            return model;
        }

        public PagerParmsInit QueryWhere(string volume, string storeId, string condition, string compare, string content, string iszf)
        {
            PagerParmsInit model = new PagerParmsInit();
            model.PageIndex = 0;
            model.PageSize = 10;
            model.Key = "B.OrderDate desc";
            model.ControlName = "gv_browOrder";
            model.PageTable = "MemberInfo as A,MemberOrder as B";
            model.PageColumn = "B.DefrayType,B.OrderType,B.remark,B.totalPv,B.totalMoney,B.PayExpectNum,B.orderExpectNum,B.number,B.OrderID,A.name,A.PetName, B.OrderDate,"
                              + " case when B.ordertype=1 then '" + BLL.Translation.Translate("001445", "店铺复消") + "'"
                              + "      when  B.ordertype = 2 then '" + BLL.Translation.Translate("001448", "网上购物") + "'"
                              + "      when  B.ordertype = 5 then '" + BLL.Translation.Translate("001454", "特殊报单") + "'"
                              + " else  '' "
                              + " end as fuxiaoName,   "
                              + " case when B.defraytype=1 then '" + BLL.Translation.Translate("000699", "现金") + "'"
                              + "      when B.defraytype = 2 then '" + BLL.Translation.Translate("001672", "电子转帐") + "'"
                              + "      when  B.defraytype = 3 then '" + BLL.Translation.Translate("000968", "支付宝") + "'"
                              + "      when  B.defraytype = 4 then '" + BLL.Translation.Translate("000983", "快钱支付") + "'"
                              + " else  ''"
                              + " end as defrayname,  "
                              + " case when B.DefrayState = 0 then '" + BLL.Translation.Translate("000521", "未支付") + "'"
                              + "     when  B.DefrayState = 1 then '" + BLL.Translation.Translate("000517", "已支付") + "'"
                              + " else ''"
                              + " end as zhifu,"
                              + " case when B.Error = '' then '" + BLL.Translation.Translate("000221", "无") + "' end as Error,"
                              + " (select Name from MemberInfo as anzhiName where anzhiName.number=A.Placement) as anzhiName, "
                              + " (select Name from MemberInfo as tuijianName where tuijianName.number=A.Direct) as tuijianName";
            model.SqlWhere = " B.Number=A.Number and B.IsAgain = 1 and B.OrderExpectNum=" + volume + " and B.StoreID='" + storeId + "'";
            if (condition.Length > 0)
            {
                switch (compare.Trim())
                {
                    case "all": break;
                    case "like":
                        if (content.Length == 0)
                        {
                            model.ErrInfo = BLL.Translation.Translate("001662", "请输入字符!");
                            return model;
                        }
                        else
                            model.SqlWhere += " and  " + condition + " like '%" + content + "%'";
                        break;
                    case "not like":
                        if (content.Length == 0)
                        {
                            model.ErrInfo = BLL.Translation.Translate("001662", "请输入字符!");
                            return model;
                        }
                        else
                            model.SqlWhere += " and  " + condition + " not like '%" + content + "%'";
                        break;
                    case "allErr": model.SqlWhere += " and  " + condition + "<>''"; break;
                    default:
                        try
                        {
                            Convert.ToDouble(content);
                        }
                        catch
                        {
                            model.ErrInfo = BLL.Translation.Translate("001665", "请输入数值!");
                            return model;

                        }
                        model.SqlWhere += " and  " + condition + compare + content;
                        break;
                }
            }
            if (iszf != "-1")
            {
                model.SqlWhere += " and  " + iszf;
            }

            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="storeId"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static int UpdateStockAndMoneyN(string orderId, string storeId, SqlTransaction tran)
        {
            return BrowsememberordersDAL.UpdateStockAndMoney(orderId, storeId, tran);
        }

        //查询报单是否存在
        public static bool GetIsExistOrder(string orderid)
        {
            if (MemberOrderDAL.GetOrderCount(orderid) == 0)
            {
                return false;
            }
            return true;
        }

    }
}