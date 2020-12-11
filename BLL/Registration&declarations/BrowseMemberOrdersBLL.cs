#region 版权信息
/*---------------------------------------------------------
 * copyright (C) 2009 shanghai qianchuang Tech. Co.,Ltd.
 *         上海乾创信息科技有限公司    版权所有
 * 文件名：BrowseMemberOrdersBLL.cs
 * 文件功能描述：注册浏览
 *
 *
 * 创建标识：董晨东 2009/08/26
 * 修改者：汪华
 * 修改时间：2009-09-07
 * 
 * 修改标识：
 * 
 * 修改描述：
 * 
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
using System.Data;
using System.Collections;
using DAL;
using System.Data.SqlClient;
using Model;
using System.Web;
using BLL.CommonClass;
using Model.Other;
namespace BLL.Registration_declarations
{
    /// <summary>
    /// 查询
    /// </summary>
    public class BrowseMemberOrdersBLL : BLL.TranslationBase
    {
        MemberInfoDAL memberInfoDAL = new MemberInfoDAL();
        MemberOrderDAL memberOrderDAL = new MemberOrderDAL();
        AddOrderDataDAL addOrderDataDAL = new AddOrderDataDAL();
        BrowsememberordersDAL browsememberordersDAL = new BrowsememberordersDAL();
        AddMemberInfomDAL addMemberInfomDAL = new AddMemberInfomDAL();

        public BrowseMemberOrdersBLL()
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


        protected List<Model.ConfigModel> GetVolumeList1()
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
        /// 获得会员报单列表
        /// </summary>
        /// <param name="strVolume">级别</param>
        /// <param name="strCondition">查询条件</param>
        /// <returns></returns>
        protected DataSet GetMemberDeclarationList(string strVolume, string strCondition)
        {

            return null;
        }
        /// <summary>
        /// 修改会员报单信息
        /// </summary>
        /// <param name="strId">报单编号</param>
        /// <returns></returns>
        protected int UpdMemberDeclaration(string strId)
        {
            int i = 0;
            return i;
        }
        /// <summary>
        /// 删除报单信息
        /// </summary>
        /// <param name="strId">报单编号</param>
        /// <param name="maxExcept">当前期数</param>
        /// <returns></returns>
        public string DelMembersDeclaration(string number, int maxExcept, string orderId, string storeId, double lackproductmoney)
        {
            OrderDAL orderDAL = new OrderDAL();
            string info = null;

            MemberOrderModel order = MemberOrderDAL.GetMemberOrder(orderId);
            BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("memberinfo", "number");//申明日志对象
            BLL.CommonClass.ChangeLogs cl_h_order = new BLL.CommonClass.ChangeLogs("MemberOrder", "orderId");//申明日志对象
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    //判断会员下是否有安置
                    //全球统一可以删除有下级的会员，如果有两个下级则注销，有一个下级则紧缩
                    info = new AddOrderBLL().GetHavePlace(number, tran);
                    if (info != null)
                    {
                        tran.Rollback();
                        conn.Close();
                        return info;
                    }
                    ////判断会员下是否有推荐
                    info = new AddOrderBLL().GetHaveDirect(number, tran);
                    if (info != null)
                    {
                        tran.Rollback();
                        conn.Close();
                        return info;
                    }

                    int cnt = new AddOrderBLL().GetHaveStore(number, tran);
                    if (cnt > 0)
                    {
                        info = this.GetTran("007097", "抱歉！该会员已经注册店铺，不可以删除！");// "抱歉！该会员有重复消费请先删除重复消费!";
                        tran.Rollback();
                        conn.Close();
                        return info;
                    }
                    //判断该会员是否有复效单
                    int result = browsememberordersDAL.GetHasOrderAgain(number);
                    if (result > 0)
                    {
                        info = this.GetTran("000976", "抱歉！该会员有重复消费请先删除重复消费!");// "抱歉！该会员有重复消费请先删除重复消费!";
                        tran.Rollback();
                        conn.Close();
                        return info;
                    }
                    else
                    {
                        string electronicaccountid = order.ElectronicaccountId; ;
                        int defrayState = order.DefrayState;
                        int defrayType = order.DefrayType;
                        //if (defrayState == 1)
                        //{
                        //    if (lackproductmoney > 0)
                        //    {
                        //        int deliveryflag = BLL.Logistics.D_AccountBLL.GetDeliveryflag(orderId, tran);
                        //        if (deliveryflag == 0)
                        //        {
                        //            if (defrayType == 2)
                        //            {
                        //                IsElecPay(tran, order);
                        //                BLL.Logistics.D_AccountBLL.AddAccount(storeId, lackproductmoney, D_AccountSftype.StoreType, D_AccountKmtype.OrderDelete, DirectionEnum.AccountsIncreased, "会员【" + number + "】报单删除现金返还，订单号为【" + orderId + "】", tran);
                        //                BLL.Logistics.D_AccountBLL.AddAccount(electronicaccountid, lackproductmoney, D_AccountSftype.MemberType, D_AccountKmtype.OrderDelete, DirectionEnum.AccountsIncreased, "会员【" + number + "】报单删除现金返还，订单号为【" + orderId + "】", tran);
                        //            }
                        //            else if (defrayType == 1)
                        //            {
                        //                BLL.Logistics.D_AccountBLL.AddAccount(storeId, lackproductmoney, D_AccountSftype.StoreType, D_AccountKmtype.OrderDelete, DirectionEnum.AccountsIncreased, "会员【" + number + "】报单删除现金扣添加，订单号为【" + orderId + "】", tran);
                        //            }
                        //        }
                        //    }
                        //}



   // cl_h_info.DeletedIntoLogstran(tran, CommonClass.ChangeCategory.Order, storeId, BLL.CommonClass.ENUM_USERTYPE.objecttype5);
                        browsememberordersDAL.DelNew(number, maxExcept, tran);

                        //添加日志
                        cl_h_info.AddRecordtran(tran, number);
                        cl_h_order.AddRecordtran(tran, orderId);
                        cl_h_order.DeletedIntoLogstran(tran, CommonClass.ChangeCategory.Order, storeId, BLL.CommonClass.ENUM_USERTYPE.objecttype5);
                     
                        addOrderDataDAL.Del_Horder(tran, orderId, storeId, CommonDataBLL.OperateBh, CommonDataBLL.OperateIP);

                        ///删除后，把推荐或安置是他的err赋上值
                        int checkResult = addOrderDataDAL.Check_WhenDelete(tran, number);
                    }

                    tran.Commit();
                }
                catch (Exception ext)
                {
                    info = this.GetTran("000985", "抱歉！系统异常");// "抱歉！系统异常";
                    tran.Rollback();
                    conn.Close();
                    return info;
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
            addOrderDataDAL.Upd_ECTPay(tran, order.ElectronicaccountId, Convert.ToDouble(order.TotalMoney) * -1);

            //电子货币支付，则在店汇款中插入记录,最后两个参数需要更改，
            addOrderDataDAL.AddDataTORemittances1(tran, order);

            //更新店铺的汇款
            addOrderDataDAL.Add_Remittances(tran, Convert.ToDouble(order.TotalMoney) * -1, order.StoreId);

        }

        public DataTable QueryDeclaration2(string storeId, string condition, string symbol, string character, int expectNum)
        {
            return AddMemberInfomDAL.QueryDeclaration2(storeId, condition, symbol, character, expectNum);


        }
        public DataTable QueryDeclaration(string storeId, string condition, string symbol, string character, int expectNum, int isAgin)
        {
            return DAL.MemberInfoDAL.QueryDeclaration(storeId, condition, symbol, character, expectNum, isAgin);
        }

        public static DataTable DeclarationProduct(string storeId, string orderId)
        {
            return DAL.MemberInfoDAL.DeclarationProduct(storeId, orderId);
        }

        public static DataTable ProductView(string orderId)
        {
            return DAL.MemberInfoDAL.ProductView(orderId);
        }

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
            return memberOrderDAL.GetMemberOrderListByStoreID(storeID, pageIndex, key, pageSize, condition, out recordCount, out pageCount);
        }


        public int updateStore2(MemberDetailsModel model, SqlTransaction tran)
        {
            return addOrderDataDAL.updateStore2(model, tran);
        }

        public PagerParmsInit QueryWhere(string volume, string storeId, string condition, string compare, string content)
        {
            PagerParmsInit model = new PagerParmsInit();
            model.PageIndex = 0;
            model.PageSize = 10;
            model.Key = "A.registerDate desc";
            model.ControlName = "gv_browOrder";
            model.PageTable = "MemberInfo as A,MemberOrder as B";
            model.PageColumn = "B.SendWay,A.ID,A.Number,b.OrderID,A.StoreID,A.Name,A.PetName,case when B.Error='' then '0' end as Error,B.totalMoney,B.totalPv,B.OrderExpectNum,B.PayExpectNum,B.defraytype"
                                + " ,A.RegisterDate,A.Remark,B.ordertype ,B.ordertype as RegisterWay, case when B.defraytype=1 then '1'  when "
                                + " B.defraytype = 2 then '2'  when  B.defraytype = 3 then '3'  when  B.defraytype = 4 then '4'  else '5'  end as defrayname, "
                                + " case when B.DefrayState = 0 then '0'  when  B.DefrayState = 1 then '1'  else '2'  end as PayStatus,B.lackproductmoney";
            model.SqlWhere = " B.Number=A.Number and B.IsAgain=0  and  B.DefrayState>-2   and B.StoreID='" + storeId + "'";



            if (condition.Length > 0)
            {
                switch (compare.Trim())
                {
                    case "all": break;
                    case "like":
                        if (content.Length == 0)
                        {
                            model.ErrInfo = this.GetTran("000959", "请输入字符!");// "请输入字符!";
                            return model;
                        }
                        else
                            model.SqlWhere += " and  " + condition + " like '%" + content + "%'";
                        break;
                    case "not like":
                        if (content.Length == 0)
                        {
                            model.ErrInfo = this.GetTran("000959", "请输入字符");// "请输入字符!";
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
                            model.ErrInfo = this.GetTran("000969", "请输入数值"); //"请输入数值!";
                            return model;
                        }

                        model.SqlWhere += " and  " + condition + compare + content;
                        break;
                }
            }
            return model;
        }

        /// <summary>
        /// 获取会员报单列表
        /// </summary>
        /// <param name="condition">列名</param>
        /// <param name="compare">运算符</param>
        /// <param name="content">值</param>
        /// <param name="sqlwhere">附加条件</param>
        /// <returns></returns>
        public PagerParmsInit GetMemberOrderList(string condition, string compare, string content, string sqlwhere)
        {
            PagerParmsInit model = new PagerParmsInit();
            model.PageIndex = 0;
            model.PageSize = 10;
            model.Key = "A.registerDate";
            model.ControlName = "gv_browOrder";
            model.PageTable = "MemberInfo as A,MemberOrder as B,city c";
            model.PageColumn = "B.SendWay,A.ID,A.Number,b.OrderID,A.StoreID,A.Name,A.PetName,case when B.Error='' then '0' end as Error,B.totalMoney,B.totalPv,B.OrderExpectNum,B.PayExpectNum,B.defraytype"
                                + " ,A.RegisterDate,A.Remark,B.ordertype ,B.ordertype ,c.country,c.province,city,xian,  B.defraystate,a.mobiletele, a.papertypecode,papernumber,sendway,sendtype,direct,placement,"
                                + " B.DefrayState,B.lackproductmoney";
            model.SqlWhere = " B.Number=A.Number and c.cpccode=a.cpccode ";

            model.SqlWhere += sqlwhere;

            if (condition.Length > 0)
            {
                switch (compare.Trim())
                {
                    case "all": break;
                    case "like":
                        if (content.Length == 0)
                        {
                            model.ErrInfo = this.GetTran("000959", "请输入字符!");// "请输入字符!";
                            return model;
                        }
                        else
                            model.SqlWhere += " and  " + condition + " like '%" + content + "%'";
                        break;
                    case "not like":
                        if (content.Length == 0)
                        {
                            model.ErrInfo = this.GetTran("000959", "请输入字符");// "请输入字符!";
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
                            model.ErrInfo = this.GetTran("000969", "请输入数值"); //"请输入数值!";
                            return model;
                        }

                        model.SqlWhere += " and  " + condition + compare + content;
                        break;
                }
            }
            return model;
        }


        public int DelNotPayStatus(string orderId)
        {
            return this.browsememberordersDAL.DelPayStatus(orderId);
        }

        public string GetRemark(string orderid)
        {
            return browsememberordersDAL.GetRemark(orderid);
        }

        public string GetMiaoShu(string orderid)
        {
            return browsememberordersDAL.GetMiaoShu(orderid);
        }

        public string GetMiaoShu1(string orderid)
        {
            return browsememberordersDAL.GetMiaoShu1(orderid);
        }

        /// <summary>
        /// 导出excel的dataTable
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <param name="condition"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public DataTable GetInfoAndOrder(string table, string column, string condition, string order)
        {
            return new AddMemberInfomDAL().GetInfoAndOrder(table, column, condition, order);
        }

        /// <summary>
        /// 通过网络图删除会员
        /// </summary>
        /// <param name="member">会员编号</param>
        /// <returns></returns>
        public string DelMembersDeclarationTreeNet(string member)
        {
            string info = "";

            //判断该会员是否当前期注册
            int regQishu = browsememberordersDAL.GetMemberRegisterQS(member);
            int maxExcept = Convert.ToInt32(CommonDataDAL.GetMaxExpect());
            if (regQishu < maxExcept)
            {
                info = "对不起，该会员不是当前期注册的，不可以删除！";
                return info;
            }

            //判断该会员是否有复效单
            int result = browsememberordersDAL.GetHasOrderAgain(member);

            if (result > 0)
            {
                info = "对不起！该会员有重复消费请先删除重复消费!";
                return info;
            }

            //int tjCount = browsememberordersDAL.GetTuiJianCount(member,false);
            //if (tjCount > 0)
            //{
            //    info = "对不起！该会员已经推荐了其他会员，不能删除!";
            //    return info;
            //}
            //int azCount = browsememberordersDAL.GetTuiJianCount(member, true);
            //if (azCount > 0)
            //{
            //    info = "对不起！该会员已经安置了其他会员，不能删除!";
            //    return info;
            //}

            int xh = browsememberordersDAL.GetXHanzhi(member);

            if (xh > 0)
            {
                info = "对不起！删除该会员后安置人数以超过2人，请将上级位置空出后再操作。!";
                return info;
            }



            string orderId = browsememberordersDAL.GetOrderID(member);
            string storeId = browsememberordersDAL.GetStoreID(member);
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    //删除后 下面的人紧缩上来  （用循环调网其下面的人）
                    browsememberordersDAL.XunHuanTW(member, maxExcept, tran);



                    browsememberordersDAL.DelNew(member, maxExcept, tran);

                    addOrderDataDAL.Del_Horder(tran, orderId, storeId, CommonDataBLL.OperateBh, CommonDataBLL.OperateIP);

                    tran.Commit();
                    info = "删除成功";
                }
                catch (Exception)
                {
                    info = "抱歉！系统异常，删除失败！";
                    tran.Rollback();
                    conn.Close();
                    return info;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return info;
        }

        public static string AuditingOrder(string orderid)
        {
            string error = "";//返回错误信息

            MemberOrderModel mo = MemberOrderDAL.GetMemberOrder(orderid);//获取报单信息
            List<MemberDetailsModel> list = ViewFuXiaoBLL.GetDetails(mo.OrderId);//获取订单明细

            //判断报单是否支付
            if (mo.DefrayState != 0)
            {
                return BLL.Translation.Translate("000987", "报单不可重复确认");// "报单不可重复确认！";
            }


            //验证店铺钱是否够支付保单
            double notEnoughmoney = new RegistermemberBLL().CheckMoneyIsEnough(list, mo.StoreId);
            double storeLeftMoney = new StoreDataDAL().GetLeftRegisterMemberMoney(mo.StoreId);
            if (storeLeftMoney < notEnoughmoney)
            {
                return BLL.Translation.Translate("006018", "对不起，您的报单额不足！");// "报单不可重复确认！";
            }


            //更改--报单信息
            mo.RemittancesId = MYDateTime.ToYYMMDDHHmmssString();
            mo.DefrayState = 1;
            mo.PayExpect = CommonDataBLL.getMaxqishu();

            //double notEnoughmoney = new RegistermemberBLL().CheckMoneyIsEnough(list, mo.StoreId);

            IList<MemberDetailsModel> listnew = CommonDataBLL.GetNewOrderDetail1(list);
            for (int i = 0; i < list.Count; i++)
            {
                int left = BLL.CommonClass.CommonDataBLL.GetLeftLogicProductInventory(Convert.ToInt32(listnew[i].ProductId));
                if (left < listnew[i].Quantity)
                {
                    return BLL.Translation.Translate("005967", "对不起，公司库存不够") + "！" + listnew[i].ProductName + BLL.Translation.Translate("005970", "库存数只有") + "：" + left;
                }
            }
            //转化汇率
            notEnoughmoney = new RegistermemberBLL().ChangeNotEnoughMoney(mo.StoreId, notEnoughmoney);

            double EnoughProductMoney = Convert.ToDouble(new RegistermemberBLL().getEnoughProductMoney(list, mo.StoreId));

            mo.EnoughProductMoney = Convert.ToInt32(EnoughProductMoney);
            mo.LackProductMoney = Convert.ToInt32(notEnoughmoney);

            //拆分组合产品
            IList<MemberDetailsModel> md = BLL.CommonClass.CommonDataBLL.GetNewOrderDetail1(list);

            System.Web.HttpContext.Current.Application.UnLock();
            System.Web.HttpContext.Current.Application.Lock();
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    //更新会员订单信息
                    if (!CommonDataBLL.ConfirmMembersOrder(tran, mo.OrderId, mo.PayExpect, mo.EnoughProductMoney, mo.LackProductMoney))
                    {
                        tran.Rollback();
                        System.Web.HttpContext.Current.Application.UnLock();
                        return BLL.Translation.Translate("000993", "确认失败");
                    }

                    //更新店铺库存
                    foreach (MemberDetailsModel memberDetailsModel in list)
                    {
                        //循环根据订单明细跟新库存
                        int result = ViewFuXiaoBLL.UptStock(tran, memberDetailsModel.StoreId, memberDetailsModel.ProductId, memberDetailsModel.Quantity, memberDetailsModel.NotEnoughProduct);

                        //如果该店铺无盖商品记录，则在店库存表中加记录
                        if (result <= 0)
                        {
                            new BrowseMemberOrdersBLL().updateStore2(memberDetailsModel, tran);
                        }

                        if (memberDetailsModel.NotEnoughProduct > 0)
                        {
                            if (!CommonDataBLL.ConfirmMembersDetails(tran, memberDetailsModel.ProductId, mo.OrderId, memberDetailsModel.NotEnoughProduct))
                            {
                                tran.Rollback();
                                System.Web.HttpContext.Current.Application.UnLock();
                                return BLL.Translation.Translate("000993", "确认失败");
                            }
                        }
                    }

                    //处理公司逻辑库存
                    int sd = new DAL.AddOrderDataDAL().updateStoreL(tran, md);



                    //报单生成订单
                    Insert_OrderGoods(list, mo, tran);

                    if (mo.DefrayType == 1)
                    {
                        if (Convert.ToDouble(mo.LackProductMoney) > 0)
                        {
                            //记录对账单明细
                            BLL.Logistics.D_AccountBLL.AddAccount(mo.StoreId, Convert.ToDouble(mo.LackProductMoney), D_AccountSftype.StoreType, D_AccountKmtype.Declarations, DirectionEnum.AccountReduced, "会员【" + mo.Number + "】报单现金扣除额，订单号为【" + mo.OrderId + "】", tran);
                        }
                    }
                    if (mo.DefrayType == 2)
                    {
                        //记录对账单明细
                        BLL.Logistics.D_AccountBLL.AddAccount(mo.ElectronicaccountId, Convert.ToDouble(mo.TotalMoney), D_AccountSftype.MemberType, D_AccountKmtype.Declarations, DirectionEnum.AccountReduced, "会员【" + mo.Number + "】用会员【" + mo.ElectronicaccountId + "】电子货币报单，订单号为【" + mo.OrderId + "】", tran);
                        BLL.Logistics.D_AccountBLL.AddAccount(mo.StoreId, Convert.ToDouble(mo.TotalMoney), D_AccountSftype.StoreType, D_AccountKmtype.Declarations, DirectionEnum.AccountsIncreased, "会员【" + mo.Number + "】用会员【" + mo.ElectronicaccountId + "】电子货币报单转入，订单号为【" + mo.OrderId + "】", tran);
                        if (Convert.ToDouble(mo.LackProductMoney) > 0)
                        {
                            BLL.Logistics.D_AccountBLL.AddAccount(mo.StoreId, Convert.ToDouble(mo.LackProductMoney), D_AccountSftype.StoreType, D_AccountKmtype.Declarations, DirectionEnum.AccountReduced, "会员【" + mo.Number + "】报单现金扣除额，订单号为【" + mo.OrderId + "】", tran);
                        }

                        //更新电子账户余额
                        AddOrderDataDAL.UpdateECTPay(tran, mo.ElectronicaccountId, mo.LackProductMoney);

                        //更新店铺汇款
                        new AddOrderDataDAL().Add_Remittances(tran, Convert.ToDouble(mo.LackProductMoney), mo.StoreId);

                        //插入汇款信息
                        new AddOrderDataDAL().AddDataTORemittances(tran, mo);
                    }

                    //更新店铺报单款
                    new AddOrderDataDAL().updateStore3(mo.StoreId, tran, Convert.ToDouble(mo.LackProductMoney));

                    //判断是否是注册的会员
                    if (mo.IsAgain == 0)
                    {
                        //获取会员信息
                        MemberInfoModel mi = MemberOrderDAL.GetMemberInfo(orderid, tran);
                        //更新会员业绩,会员进入网络图
                        new AddOrderDataDAL().Upt_UpdateNew1(mi, tran);
                        //激活会员
                        int resultActive = new GroupRegisterBLL().uptIsActive(mi.Number, tran);
                    }
                    //实时更新会员级别
                    CommonDataBLL.SetMemberLevel(tran, mo.Number, mo.OrderId);
                    //提交事务
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    string sga = ex.Message;
                    error = BLL.Translation.Translate("000993", "确认失败");// "确认失败！";
                    tran.Rollback();
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                    System.Web.HttpContext.Current.Application.UnLock();
                }
            }

            return error;
        }

        public static void Insert_OrderGoods(IList<MemberDetailsModel> list, MemberOrderModel memberOrderModel, SqlTransaction tran)
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
                string StoreorderId = "";
                StoreorderId = BLL.Logistics.OrderGoodsBLL.GetNewOrderID();
                OrderGoodsMedel storeItem = new BLL.Registration_declarations.AddOrderBLL().GetOrderModel(StoreorderId, memberOrderModel, tran, totalMoney, totalPv);
                new BLL.Registration_declarations.AddOrderBLL().OrderSubmit(memberOrderModel.OrderId.ToString(), list, storeItem, tran);
            }
        }

        public PagerParmsInit QueryWhereMember(string volume, string number, string condition, string compare, string content)
        {
            PagerParmsInit model = new PagerParmsInit();
            model.PageIndex = 0;
            model.PageSize = 10;
            model.Key = "A.registerDate";
            model.ControlName = "gv_browOrder";
            model.PageTable = "MemberInfo as A,MemberOrder as B";
            model.PageColumn = "A.storeId as storeId,A.ID,A.Number,b.OrderID,A.StoreID,A.Name,A.PetName,case when B.Error='' then '0' end as Error,B.totalMoney,B.totalPv,B.OrderExpectNum,B.PayExpectNum,B.defraytype"
                                + " ,A.RegisterDate,A.Remark,B.ordertype ,B.ordertype as RegisterWay, case when B.defraytype=1 then '1'  when "
                                + " B.defraytype = 2 then '2'  when  B.defraytype = 3 then '3'  when  B.defraytype = 4 then '4'  else '5'  end as defrayname, "
                                + " case when B.DefrayState = 0 then '0'  when  B.DefrayState = 1 then '1'  else '2'  end as PayStatus,B.lackproductmoney,case defraystate when 1 then 1 else case paymentmoney when 0 then 0 else 1 end end as dpqueren,defraystate as gsqueren";
            model.SqlWhere = " B.Number=A.Number and B.IsAgain=0 and A.Direct='" + number + "' and B.orderType=3";

            if (condition.Length > 0)
            {
                switch (compare.Trim())
                {
                    case "all": break;
                    case "like":
                        if (content.Length == 0)
                        {
                            model.ErrInfo = this.GetTran("000959", "请输入字符!");// "请输入字符!";
                            return model;
                        }
                        else
                            model.SqlWhere += " and  " + condition + " like '%" + content + "%'";
                        break;
                    case "not like":
                        if (content.Length == 0)
                        {
                            model.ErrInfo = this.GetTran("000959", "请输入字符");// "请输入字符!";
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
                            model.ErrInfo = this.GetTran("000969", "请输入数值"); //"请输入数值!";
                            return model;
                        }

                        model.SqlWhere += " and  " + condition + compare + content;
                        break;
                }
            }
            return model;
        }
    }
}