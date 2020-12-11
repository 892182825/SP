#region 版权信息
/*---------------------------------------------------------
 * copyright (C) 2009 shanghai qianchuang Tech. Co.,Ltd.
 *         上海乾创信息科技有限公司    版权所有
 * 文件名：AuditingMemberagainBLL.cs
 * 文件功能描述：注册确认
 *
 *
 * 创建标识：董晨东 2009/08/26
 * 
 * 修改标识：
 * 
 * 修改描述：
 * 修改者：汪华
 * 修改时间：2009-09-01
 * 
 * 
 * 
 * 
 //----------------------------------------- **/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using BLL.CommonClass;
using DAL;
using Model;
using DAL.Other;
using BLL.Logistics;

namespace BLL.Registration_declarations
{
    public class AuditingMemberagainBLL
    {
        DAL.AuditingMemberagainDAL auditingMemberagainDAL = new DAL.AuditingMemberagainDAL();

        public AuditingMemberagainBLL()
        {
        }
        /// <summary>
        /// 获得期数列表
        /// </summary>
        /// <returns></returns>
        protected DataSet GetVolumeList()
        {
            return null;
        }
        /// <summary>
        /// 获得查询条件列表
        /// </summary>
        /// <returns></returns>
        protected ArrayList GetAdvancedQueryList()
        {
            return null;
        }
        /// <summary>
        /// 获得会员复销列表
        /// </summary>
        /// <param name="strVolume">级别</param>
        /// <param name="strCondition">查询条件</param>
        /// <returns></returns>
        protected DataSet GetMemberDeclarationList(string strVolume, string strCondition)
        {
            return null;
        }
        /// <summary>
        /// 获得会员报单其他信息
        /// </summary>
        /// <param name="strId">报单编号</param>
        /// <returns></returns>
        protected DataSet GetMemberOtherInfo(string strId)
        {
            return null;
        }
        /// <summary>
        /// 修改会员复销信息
        /// </summary>
        /// <param name="strId">报单编号</param>
        /// <returns></returns>
        protected int UpdMemberDeclaration(string strId)
        {
            int i = 0;
            return i;
        }
        /// <summary>
        /// 删除会员复销信息
        /// </summary>
        /// <param name="strId">报单编号</param>
        /// <returns></returns>
        public string DelMembersDeclaration(string orderId, double totalPv, string number, int except, string storeId, double lackproductmoney)
        {
            string info = null;
            MemberOrderModel order = MemberOrderDAL.GetMemberOrder(orderId);
            using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                #region 处理组合商品库存
                /*
                List<MemberDetailsModel> groupItemList2 = new AddOrderBLL().GetDetails(orderId);
                List<OrderProduct3> oldSmallItem = new List<OrderProduct3>();
                for (int i = 0; i < groupItemList2.Count; i++)
                {
                    if (groupItemList2[i].IsGroupItem != "")
                    {
                        oldSmallItem = new AddMemberInfomDAL().GetSamllItemList(groupItemList2[i].ProductId.ToString());
                        for (int j = 0; j < oldSmallItem.Count; j++)
                        {
                            int hasOnly = new AddOrderDataDAL().SmallItemIsOnlyInGroup(oldSmallItem[j].Id, orderId);
                            if (hasOnly <= 0)
                            {
                                oldSmallItem[j].Count *= groupItemList2[i].Quantity;
                                int never = new AddOrderDataDAL().updateStore10(storeId, oldSmallItem[j], tran);
                                if (never <= 0)
                                {
                                    tran.Rollback();
                                    conn.Close();
                                    conn.Dispose();
                                    info = BLL.Translation.Translate("001730", "抱歉！系统异常！");
                                    return info;
                                }
                            }

                        }
                    }
                }*/

                #endregion

                //如果该订单不存在
                if (!BrowsememberordersDAL.CheckOrderIdExists(tran, orderId))
                {
                    info = BLL.Translation.Translate("001661", "抱歉，该订单不存在！");
                    tran.Rollback();
                    conn.Close();
                    conn.Dispose();
                    return info;
                }

                //删除复消单
                if (order.DefrayState == 1)
                {
                    if (lackproductmoney > 0)
                    {
                        D_AccountBLL.AddAccount(storeId, lackproductmoney, D_AccountSftype.StoreType, D_AccountKmtype.OrderDelete, DirectionEnum.AccountsIncreased, "会员【" + number + "】报单删除现金返还，订单号为【" + orderId + "】", tran);
                    }
                }
                else if (order.DefrayState == 2)
                {
                    string electronicaccountid = order.ElectronicaccountId; ;
                    if (lackproductmoney > 0)
                    {
                        IsElecPay(tran, order);
                        D_AccountBLL.AddAccount(storeId, lackproductmoney, D_AccountSftype.StoreType, D_AccountKmtype.OrderDelete, DirectionEnum.AccountsIncreased, "会员【" + number + "】报单删除现金返还，订单号为【" + orderId + "】", tran);
                        D_AccountBLL.AddAccount(electronicaccountid, lackproductmoney, D_AccountSftype.MemberType, D_AccountKmtype.OrderDelete, DirectionEnum.AccountsIncreased, "会员【" + number + "】报单删除现金返还，订单号为【" + orderId + "】", tran);
                    }
                }
                if (order.DefrayState == 1)
                {
                    int result = new AddOrderDataDAL().Js_delfuxiao(number, totalPv, except, order.DefrayState, tran);
                }

                try
                {
                    new AddOrderDataDAL().Del_Horder(tran, orderId, storeId, CommonDataBLL.OperateBh, CommonDataBLL.OperateIP);
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    info = BLL.Translation.Translate("001730", "抱歉！系统异常！");
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
        public void IsElecPay(SqlTransaction tran, MemberOrderModel order)
        {
            //支付期数
            order.PayExpect = CommonClass.CommonDataBLL.getMaxqishu();
            //生成汇款单号
            order.RemittancesId = Model.Other.MYDateTime.ToYYMMDDHHmmssString();
            //支付状态改为1
            order.DefrayState = 1;

            //电子货币支付时，记录已经支付的金额
            new DAL.AddOrderDataDAL().Upd_ECTPay(tran, order.ElectronicaccountId, Convert.ToDouble(order.TotalMoney) * -1);

            //电子货币支付，则在店汇款中插入记录,最后两个参数需要更改，
            new DAL.AddOrderDataDAL().AddDataTORemittances1(tran, order);

            //更新店铺的汇款
            new DAL.AddOrderDataDAL().Add_Remittances(tran, Convert.ToDouble(order.TotalMoney) * -1, order.StoreId);

        }

        /// <summary>
        /// 删除当期,未支付的自由注册会员SqlTransaction tran,
        /// </summary>
        /// <returns>返回 0 失败</returns>
        public string DeleteCurretMemberinfo(string Number, int ExceptNum, string OrderID, string StoreID)
        {
            return auditingMemberagainDAL.DeleteCurretMemberinfo(Number, ExceptNum, OrderID, StoreID);
        }

        /// <summary>
        /// 确认 当期,未支付的自由注册 信息
        /// </summary>
        /// <param name="Number"></param>
        /// <param name="OrderID"></param>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        public string ConfirmCurretMemberinfo(string Number, string OrderID, string StoreID, string TotalPV, string TotalMoney, int DefrayType, string ElectronicAccountID)
        {
            //根据订单编号获得订单详细
            string info = "";

            //获取电子账号
            string elcAccountId = new AddOrderDataDAL().GerExcNuber(OrderID);
            //try
            //{
            BrowseMemberOrdersBLL browseMemberOrdersBLL = new BrowseMemberOrdersBLL();
            ViewFuXiaoBLL viewFuXiaoBLL = new ViewFuXiaoBLL();
            using (SqlConnection con = new SqlConnection(DAL.DBHelper.connString))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                //获得会员电子账户剩余金额
                object zhifumoney = CommonDataBLL.EctIsEnough(Number);
                //获得会员订单信息
                DataTable dt = BrowseMemberOrdersBLL.DeclarationProduct(StoreID, OrderID);


                if (DefrayType == 2)//支付类型，1是现金，2电子转帐，3网上支付，
                {
                    //电子钱包余额是否大于订单金额
                    if (Convert.ToDouble(zhifumoney) < Convert.ToDouble(TotalMoney))
                    {
                        info = BLL.Translation.Translate("000599", "会员") + ":" + Number + BLL.Translation.Translate("001736", "的电子帐户不够支付本报单！");
                        return info;
                    }

                }

                //获得店铺不足货时可以报单的金额
                object memberordermoney = CommonDataBLL.StoreLaveAmount(StoreID);

                //获得该订单所以不足货所的报单的金额
                double sumNotenoughMoney = ViewFuXiaoBLL.GetNotEnoughMoney(OrderID, tran);

                // 店铺剩余可报单额是否大于订单金额
                if (Convert.ToDouble(memberordermoney) < sumNotenoughMoney)
                {
                    info = BLL.Translation.Translate("000388", "店铺") + ": " + StoreID + BLL.Translation.Translate("001739", "的可以用来报单的费用不足！");
                    return info;
                }

                //是否是网上银行支付
                if (DefrayType == 3)
                {
                    //info = "网上银行支付";
                    ////Response.Write("<script>location.href='../Send.aspx?V_amount=" + Totalmoney.Value + "&V_oid=" + orderId + "';</script>");
                    //return info;
                }


                //获得最大期数(测试)
                int maxQs = CommonDataBLL.getMaxqishu();
                //更新会员订单信息
                if (!CommonDataBLL.ConfirmMembersOrder(tran, OrderID, maxQs))
                {
                    info = BLL.Translation.Translate("000979", "支付失败");
                    return info;
                }
                //更新结算（需要修改存储过程）
                if (!BLL.CommonClass.CommonDataBLL.UPMemberInfoBalance(tran, Number, decimal.Parse(TotalPV), maxQs))
                {
                    info = BLL.Translation.Translate("001741", "结算失败");
                    return info;
                }


                //更新店铺库存

                List<Model.MemberDetailsModel> list = ViewFuXiaoBLL.GetDetails(OrderID);


                int result = 0;
                bool real = true;
                foreach (Model.MemberDetailsModel memberDetailsModel in list)
                {
                    //循环根据订单明细跟新库存
                    if (memberDetailsModel.IsGroupItem == "" || memberDetailsModel.IsGroupItem == null)
                    {

                        if (memberDetailsModel.HasGroupItem == "true")
                        {
                            result = new AddMemberInfomDAL().updateStore11(StoreID, memberDetailsModel, tran);
                            real = false;
                        }
                        else
                        {
                            result = ViewFuXiaoBLL.UptStock(tran, StoreID, memberDetailsModel.ProductId, memberDetailsModel.Quantity, memberDetailsModel.NotEnoughProduct);
                            real = false;
                        }
                    }
                    if (result <= 0 && real == false)
                    {
                        if (browseMemberOrdersBLL.updateStore2(memberDetailsModel, tran) <= 0)
                        {
                            tran.Rollback();
                            con.Close();
                            return info;
                        }
                    }
                }


                if (DefrayType == 2)
                {

                    //添加到店汇款表
                    int result3 = new AddOrderDataDAL().AddDataTORemittances(tran, MemberOrderBLL.GetMemberOrder(OrderID));
                    if (result3 > 0)
                    {
                        //跟新被转账会员的电子帐户
                        if (!BLL.CommonClass.CommonDataBLL.UPMemberEct(elcAccountId, Convert.ToDecimal(TotalMoney)))
                        {
                            tran.Rollback();
                            con.Close();
                            return info;
                        };
                        //更新店铺的汇款
                        int result2 = new AddOrderDataDAL().Add_Remittances(tran, Convert.ToDouble(TotalMoney), StoreID);
                    }
                    else
                    {
                        tran.Rollback();
                        con.Close();
                        return info;
                    }
                }

                //更新店铺总保单额(累计)
                if (new AddOrderBLL().updateStore4(StoreID, tran, Convert.ToDouble(TotalMoney)) <= 0)
                {
                    tran.Rollback();
                    con.Close();
                    return info;
                }
                //提交事务
                tran.Commit();
            }


            //}
            //catch
            //{

            //}
            info = BLL.Translation.Translate("001743", "确定完成!");

            return info;
        }


        public PagerParmsInit QueryWhere1(string volume, string storeId, string condition, string compare, string content, string iszf, string startTime, string endTime)
        {
            PagerParmsInit model = new PagerParmsInit();
            model.PageIndex = 0;
            model.PageSize = 10;
            model.Key = "MO.ID";
            model.ControlName = "gv_browOrder";
            model.PageColumn = "MO.SendWay,MO.id,MO.ElectronicAccountID,MO.OrderExpectNum,MO.OrderType,case when MO.DefrayState = 0 then '" + BLL.Translation.Translate("000521", "未支付") + "'   when  MO.DefrayState = 1 then '" + BLL.Translation.Translate("000517", "已支付") + "'  else '" + BLL.Translation.Translate("001416", "未知") + "'  end as PayStatus,case when MO.defraytype=1 then '" + BLL.Translation.Translate("001558", "现金") + "'  when "
                              + " MO.defraytype = 2 then '" + BLL.Translation.Translate("001672", "电子转帐") + "'  when  MO.defraytype = 3 then '" + BLL.Translation.Translate("000983", "快钱") + "' when  MO.defraytype = 4 then '" + BLL.Translation.Translate("001582", "银行汇款") + "'  else '" + BLL.Translation.Translate("001416", "未知") + "'  end as defrayname,MO.defraytype,MO.StoreID,MO.OrderID,MI.Number,MI.RegisterDate,MI.Remark,MI.Name,Mo.TotalMoney,MO.TotalPv,MO.IsReceivables,Currency.Name as paycrr,MO.defrayState,MO.OrderDate,MO.operatenum ";

            model.PageTable = "MemberOrder as MO ,MemberInfo as MI,Currency";
            if (volume.Equals("-1"))
                model.SqlWhere = "MO.PayCurrency=Currency.id and MO.Number=MI.Number and MO.IsAgain=1 and (ordertype in(13,23,33,12,32) or (ordertype = 12 and defraytype=1)) and  MO.DefrayState>-2 and MO.storeID='" + storeId + "' ";
            else
            {
                model.SqlWhere = "MO.PayCurrency=Currency.id and MO.Number=MI.Number and MO.IsAgain=1 and (ordertype in(13,23,33,12,32) or (ordertype = 12 and defraytype=1)) and  MO.DefrayState>-2 and MO.storeID='" + storeId + "' and  MO.OrderExpectNum=" + volume;
            }
            //model.SqlWhere += "and Mo.ordertype not in (21,22,23)";
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
                        try { Convert.ToDouble(content); }
                        catch
                        {
                            model.ErrInfo = BLL.Translation.Translate("001665", "请输入数值!");
                            return model;

                        }
                        model.SqlWhere += " and  " + condition + compare + content ;
                        break;
                }
            }

            if (iszf != "-1")
            {
                model.SqlWhere += " and  " + iszf;
            }

            if (startTime != "")
            {
                model.SqlWhere += " and OrderDate>='" + Convert.ToDateTime(startTime).ToString("yyyy-MM-dd") + " 00:00:00'";
            }
            if (endTime != "")
            {
                model.SqlWhere += " and OrderDate<='" + Convert.ToDateTime(endTime).ToString("yyyy-MM-dd") + " 23:59:59'";
            }

            return model;
        }
        public PagerParmsInit QueryWhere2(string volume, string storeId, string condition, string compare, string content, string iszf, string startTime, string endTime)
        {
            PagerParmsInit model = new PagerParmsInit();
            model.PageIndex = 0;
            model.PageSize = 10;
            model.Key = "MO.ID";
            model.ControlName = "gv_browOrder";
            model.PageColumn = "MO.SendWay,MO.id,MO.ElectronicAccountID,MO.OrderExpectNum,MO.OrderType,case when MO.DefrayState = 0 then '" + BLL.Translation.Translate("000521", "未支付") + "'   when  MO.DefrayState = 1 then '" + BLL.Translation.Translate("000517", "已支付") + "'  else '" + BLL.Translation.Translate("001416", "未知") + "'  end as PayStatus,case when MO.defraytype=1 then '" + BLL.Translation.Translate("001558", "现金") + "'  when "
                              + " MO.defraytype = 2 then '" + BLL.Translation.Translate("001672", "电子转帐") + "'  when  MO.defraytype = 3 then '" + BLL.Translation.Translate("000983", "快钱") + "' when  MO.defraytype = 4 then '" + BLL.Translation.Translate("001582", "银行汇款") + "'  else '" + BLL.Translation.Translate("001416", "未知") + "'  end as defrayname,MO.defraytype,MO.StoreID,MO.OrderID,MI.Number,MI.RegisterDate,MI.Remark,MI.Name,Mo.TotalMoney,MO.TotalPv,MO.IsReceivables,Currency.Name as paycrr,MO.defrayState,MO.OrderDate,MO.operatenum ";

            model.PageTable = "MemberOrder as MO ,MemberInfo as MI,Currency";
            if (volume.Equals("-1"))
                model.SqlWhere = "MO.PayCurrency=Currency.id and MO.Number=MI.Number and MO.defraytype=3 ";
            else
            {
                model.SqlWhere = "MO.PayCurrency=Currency.id and MO.Number=MI.Number and MO.defraytype=3 and  MO.OrderExpectNum=" + volume;
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
                        try { Convert.ToDouble(content); }
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

            if (startTime != "")
            {
                model.SqlWhere += " and OrderDate>='" + Convert.ToDateTime(startTime).ToString("yyyy-MM-dd") + " 00:00:00'";
            }
            if (endTime != "")
            {
                model.SqlWhere += " and OrderDate<='" + Convert.ToDateTime(endTime).ToString("yyyy-MM-dd") + " 23:59:59'";
            }

            return model;
        }
        public PagerParmsInit QueryWhere(string volume, string storeId, string condition, string compare, string content, string iszf)
        {
            PagerParmsInit model = new PagerParmsInit();
            model.PageIndex = 0;
            model.PageSize = 10;
            model.Key = "MO.ID";
            model.ControlName = "gv_browOrder";
            model.PageColumn = "MO.id,MO.ElectronicAccountID,MO.OrderExpectNum,MO.OrderType,case when MO.DefrayState = 0 then '" + BLL.Translation.Translate("000521", "未支付") + "'   when  MO.DefrayState = 1 then '" + BLL.Translation.Translate("000517", "已支付") + "'  else '" + BLL.Translation.Translate("001416", "未知") + "'  end as PayStatus,case when MO.defraytype=1 then '" + BLL.Translation.Translate("001558", "现金") + "'  when "
                              + " MO.defraytype = 2 then '" + BLL.Translation.Translate("001672", "电子转帐") + "'  when  MO.defraytype = 3 then '" + BLL.Translation.Translate("000983", "快钱") + "' when  MO.defraytype = 4 then '" + BLL.Translation.Translate("001582", "银行汇款") + "'  else '" + BLL.Translation.Translate("001416", "未知") + "'  end as defrayname,MO.defraytype,MO.StoreID,MO.OrderID,MI.Number,MI.RegisterDate,MI.Remark,MI.Name,Mo.TotalMoney,MO.TotalPv,MO.IsReceivables,Currency.Name as paycrr,MO.defrayState ";

            model.PageTable = "MemberOrder as MO ,MemberInfo as MI,Currency";
            if (volume.Equals("-1"))
                model.SqlWhere = "MO.PayCurrency=Currency.id and MO.Number=MI.Number and MO.OrderType=2 and MO.IsAgain=1 and MO.storeID='" + storeId + "' ";
            else
            {
                model.SqlWhere = "MO.PayCurrency=Currency.id and MO.Number=MI.Number and MO.OrderType=2 and MO.IsAgain=1 and MO.storeID='" + storeId + "' and  MO.OrderExpectNum=" + volume;
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
                        try { Convert.ToDouble(content); }
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

    }
}