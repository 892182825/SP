#region 版权信息
/*---------------------------------------------------------
 * copyright (C) 2009 shanghai qianchuang Tech. Co.,Ltd.
 *         上海乾创信息科技有限公司    版权所有
 * 文件名：AuditingMemberOrdersBLL.cs
 * 文件功能描述：注册确认
 *
 *
 * 创建标识：董晨东 2009/08/26
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
using System.Data.SqlClient;

namespace BLL.Registration_declarations
{
    public class AuditingMemberOrdersBLL
    {

        public AuditingMemberOrdersBLL()
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
        /// 获得会员注册列表
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
            DataSet ds = new DataSet();
            return ds;
        }
        /// <summary>
        /// 修改会员注册报单信息
        /// </summary>
        /// <param name="strId">报单编号</param>
        /// <returns></returns>
        protected int UpdMemberDeclaration(string strId)
        {
            int i = 0;
            return i;
        }
        /// <summary>
        /// 删除会员注册报单信息
        /// </summary>
        /// <param name="strId">报单编号</param>
        /// <returns></returns>
        protected int DelMembersDeclaration(string strId)
        {
            int i = 0;
            return i;
        }
        /// <summary>
        /// 确认 
        /// </summary>
        /// <returns></returns>
        protected string ConfirmMemberinfo(string Number, string OrderID, string StoreID, string DefrayType, string TotalPV, int ProductId, int Quantity)
        {
            //string info = string.Empty;
            //using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
            //{
            //    conn.Open();
            //    SqlTransaction tran = conn.BeginTransaction();
            //    //根据订单编号获得订单详细

            //    try
            //    {
            //        BLL.CommonClass.CommonDataBLL commonDataBLL = new BLL.CommonClass.CommonDataBLL();
            //        BrowseMemberOrdersBLL browseMemberOrdersBLL = new BrowseMemberOrdersBLL();
            //        ViewFuXiaoBLL viewFuXiaoBLL = new ViewFuXiaoBLL();


            //        //获得会员电子账户剩余金额
            //        object zhifumoney = BLL.CommonClass.CommonDataBLL.EctIsEnough(Number);
            //        //获得会员订单信息
            //        DataTable dt = browseMemberOrdersBLL.DeclarationProduct(StoreID, OrderID);
            //        double TotalMoney = 0;//报单金额
            //        if (dt.Rows.Count > 0)
            //        {
            //            foreach (DataRow dr in dt.Rows)
            //            {
            //                TotalMoney += Convert.ToDouble(dr["TotalMoney"]);
            //            }
            //        }

            //        //获得店铺剩余报单金额
            //        object memberordermoney = commonDataBLL.StoreLaveAmount(StoreID);
            //        //是否是电子钱包支付
            //        if (Convert.ToInt32(DefrayType) == 2)
            //        {
            //            //电子钱包余额是否大于订单金额
            //            if (Convert.ToDouble(zhifumoney) < TotalMoney)
            //            {
            //                info = "会员" +Number + "的电子帐户不够支付本订单！！！";
            //                return;
            //            }
            //        }
            //        else
            //        {
            //            //店铺剩余可报单额是否大于订单金额
            //            if (Convert.ToDouble(memberordermoney) < TotalMoney)
            //            {
            //                info = "店铺: " + StoreID + "的可报单额不足！！！";
            //                return;
            //            }
            //        }
            //        //是否是网上银行支付
            //        if (Convert.ToInt32(DefrayType) == 3)
            //        {
            //            //Response.Write("<script>location.href='../Send.aspx?V_amount=" + Totalmoney.Value + "&V_oid=" + orderId + "';</script>");
            //            //return;
            //        }
            //        //获得最大期数
            //        int maxQs = commonDataBLL.getMaxqishu();
            //        //更新会员订单信息
            //        if (!BLL.CommonClass.CommonDataBLL.ConfirmMembersOrder(tran, OrderID, maxQs))
            //        {
            //            info = "支付失败";
            //            return;
            //        }
            //        //更新结算表
            //        if (!BLL.CommonClass.CommonDataBLL.UPMemberInfoBalance(tran, number, decimal.Parse(TotalPV), maxQs))
            //        {
            //            info = "结算失败";
            //            return;
            //        }
            //        //更新店铺库存
            //        if (!BLL.CommonClass.CommonDataBLL.UPStoreStock(tran, number, ProductId, 1))
            //        {
            //            //店铺没有该货物类型的纪录，则添加该类型的记录
            //            BLL.Registration_declarations.AuditingMemberOrdersBLL abll = new BLL.Registration_declarations.AuditingMemberOrdersBLL();
            //            abll.InsertIntoStock(tran, StoreID, ProductId.ToString(), Quantity.ToString());
            //        }
            //        if (DefrayType == 2)
            //        {
            //            //更新会员电子钱包
            //            BLL.CommonClass.CommonDataBLL.UPMemberEct(tran, Number, Convert.ToDecimal(TotalMoney));
            //        }
            //        //更新报单额

            //        //提交事务
            //        tran.Commit();

            //    }
            //    catch
            //    {
            //        //回滚事务
            //        tran.Rollback();
            //        throw;
            //    }

            //}
            return null;
        }

        public PagerParmsInit QueryWhere(string volume, string storeId, string condition, string compare, string content, string iszf,int expectType)
        {
            PagerParmsInit model = new PagerParmsInit();
            model.PageIndex = 0;
            model.PageSize = 10;
            model.Key = "A.ID";
            model.ControlName = "gv_browOrder";

            model.PageTable = "MemberInfo as A,MemberOrder as B,Currency";
            if (volume.Equals("-1"))
                model.SqlWhere = " B.PayCurrency=Currency.id and B.Number=A.Number and B.IsAgain=0  and B.StoreID='" + storeId + "'";
            else
            {
                if (expectType == 0)
                {
                    model.SqlWhere = " B.PayCurrency=Currency.id and B.Number=A.Number  and B.IsAgain=0 and  B.DefrayState>-2 and B.OrderExpectNum=" + volume + " and B.StoreID='" + storeId + "'";
                }
                else
                {
                    model.SqlWhere = " B.PayCurrency=Currency.id and B.Number=A.Number and B.IsAgain=0 and  B.DefrayState>-2 and B.PayExpectNum=" + volume + " and B.StoreID='" + storeId + "'";
                }
            }

            model.PageColumn = "B.SendWay,IsReceivables,Currency.Name as paycrr,A.ID,A.Number,b.OrderID,A.StoreID,A.Name,A.PetName,B.totalMoney,B.totalPv,B.OrderExpectNum,B.PayExpectNum,B.DefrayType"
                                + " ,A.RegisterDate,A.Remark,B.ordertype ,case when B.ordertype=0 then '0' when B.ordertype = 3 then '3' when  B.ordertype = 4 then '4' else '5'  end as RegisterWay, case when B.defraytype=1 then '1'  when "
                                + " B.defraytype = 2 then '2'  when  B.defraytype = 3 then '3'  when  B.defraytype = 4 then '4'  else '5'  end as defrayname, "
                                + " case when B.DefrayState = 0 then '0'  when  B.DefrayState = 1 then '1'  else '2'  end as PayStatus,B.defrayState,B.lackproductmoney,B.operatenum";
            if (condition.Length > 0)
            {
                switch (compare.Trim())
                {
                    case "all": break;
                    case "like":
                        if (content.Length == 0)
                        {
                            model.ErrInfo = "请输入字符!";
                            return model;
                        }
                        else
                            model.SqlWhere += " and  " + condition + " like '%" + content + "%'";
                        break;
                    case "not like":
                        if (content.Length == 0)
                        {
                            model.ErrInfo = "请输入字符!";
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
                            model.ErrInfo = "请输入数值!";
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

        public PagerParmsInit QueryWhere(string volume, string storeId, string condition, string compare, string content,string  iszf)
        {
            PagerParmsInit  model = new PagerParmsInit();
            model.PageIndex = 0;
            model.PageSize = 10;
            model.Key = "A.ID";
            model.ControlName = "gv_browOrder";

            model.PageTable = "MemberInfo as A,MemberOrder as B,Currency";
            if (volume.Equals("-1"))
                model.SqlWhere = " B.PayCurrency=Currency.id and B.Number=A.Number and  B.OrderType=3 and B.IsAgain=0  and B.StoreID='" + storeId + "'";
            else
                model.SqlWhere = " B.PayCurrency=Currency.id and B.Number=A.Number and  B.OrderType=3 and B.IsAgain=0 and B.OrderExpectNum=" + volume + " and B.StoreID='" + storeId + "'";
            model.PageColumn = "IsReceivables,Currency.Name as paycrr,A.ID,A.Number,b.OrderID,A.StoreID,A.Name,A.PetName,B.totalMoney,B.totalPv,B.OrderExpectNum,B.PayExpectNum,B.DefrayType"
                                + " ,A.RegisterDate,A.Remark,B.ordertype ,case when B.ordertype=0 then '0' when B.ordertype = 3 then '3' when  B.ordertype = 4 then '4' else '5'  end as RegisterWay, case when B.defraytype=1 then '1'  when "
                                + " B.defraytype = 2 then '2'  when  B.defraytype = 3 then '3'  when  B.defraytype = 4 then '4'  else '5'  end as defrayname, "
                                + " case when B.DefrayState = 0 then '0'  when  B.DefrayState = 1 then '1'  else '2'  end as PayStatus,B.defrayState,B.lackproductmoney";
            if (condition.Length > 0)
            {
                switch (compare.Trim())
                {
                    case "all": break;
                    case "like":
                        if (content.Length == 0)
                        {
                            model.ErrInfo = "请输入字符!";
                            return model;
                        }
                        else
                            model.SqlWhere += " and  " + condition + " like '%" + content + "%'";
                        break;
                    case "not like":
                        if (content.Length == 0)
                        {
                            model.ErrInfo = "请输入字符!";
                            return model;
                        }
                        else
                            model.SqlWhere += " and  " + condition + " not like '%" + content + "%'";
                        break;
                    case "allErr": model.SqlWhere += " and  " + condition + "<>''"; break;
                    default:
                        try { Convert.ToDouble(content); }
                        catch {
                            model.ErrInfo = "请输入数值!";
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

    public class PagerParmsInit
    {
        public PagerParmsInit() { }

       private string _errinfo;
       private string _key;
       private string _controlname;
       private string _sqlwhere;
       private string _pageColumn;
       private string _pageTable;
       private int _pageIndex;
       private int _pageSize;
        /// <summary>
        /// 错误信息
        /// </summary>
       public string ErrInfo
       {
           set { _errinfo = value; }
           get { return _errinfo; }
       }
        /// <summary>
        /// 排序字段
        /// </summary>
       public string Key
       {
           set { _key = value; }
           get { return _key; }
       }
       /// <summary>
       /// 控件名称
       /// </summary>
       public string ControlName
       {
           set { _controlname = value; }
           get { return _controlname; }
       }

       /// <summary>
       /// 查询条件
       /// </summary>
       public string SqlWhere
       {
           set { _sqlwhere = value; }
           get { return _sqlwhere; }
       }
       /// <summary>
       /// 字段
       /// </summary>
       public string PageColumn
       {
           set { _pageColumn = value; }
           get { return _pageColumn; }
       }
       /// <summary>
       /// 表名
       /// </summary>
       public string PageTable
       {
           set { _pageTable = value; }
           get { return _pageTable; }
       }
       /// <summary>
       /// 索引 从0开始
       /// </summary>
       public int PageIndex
       {
           set { _pageIndex = value; }
           get { return _pageIndex; }
       }
       /// <summary>
       /// 查询 数据数量
       /// </summary>
       public int PageSize
       {
           set { _pageSize = value; }
           get { return _pageSize; }
       }
    }

}
