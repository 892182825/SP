using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Other;
using Model;
using System.Collections;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using DAL;
using System.IO;
using BLL.CommonClass;
using Model.Other;
using System.Configuration;

using BLL.MoneyFlows;
using System.Collections.Generic;
using Model.Other;
using BLL.CommonClass;
using BLL.Logistics;
using System.Data;


/*
 *会员注册报单
 *作者:zhc
 *时间：2009-8-31
 */
namespace BLL.Registration_declarations
{

    public class RegistermemberBLL : BLL.TranslationBase
    {
        //AddOrderDataDAL数据层方法对象
        AddOrderDataDAL addOrderDataDAL = new AddOrderDataDAL();
        OrderDAL orderDAL = new OrderDAL();


        /// <summary>
        /// 循环ProductModel集合类去界面,
        /// 找用户输入的商品型号NProductID
        /// </summary>
        /// <returns></returns>
        public List<string> ToChooseProduct()
        {
            //double zongJine = 0.0;   //用户选的货物的金额总计
            //double zongPv = 0.0;		//用户选的货物的积分总计

            // AddOrderDataDAL addOrderDataDAL=new AddOrderDataDAL();
            List<string> strList = new List<string>();
            List<ProductModel> list = addOrderDataDAL.GetProductList();
            foreach (ProductModel model in list)
            {
                string numStr = "N" + model.ProductID;

                if (numStr != "N")
                {
                    strList.Add(numStr);
                }

            }
            return strList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ProductModel> GetProductModelList()
        {
            return addOrderDataDAL.GetProductList();
        }


        /// <summary>
        /// 将用户从界面选择的商品加入集合类
        /// </summary>
        /// <param name="productID">商品编号</param>
        /// <param name="list">原来所有商品的集合类</param>
        /// <param name="list2">用户选中商品的集合类</param>
        /// <returns>用户选中商品的集合类</returns>
        public OrderProduct GetUserChooseProduct(int ptNum, ProductModel
product)
        {
            //新建结构
            OrderProduct orderProduct = new DAL.Other.OrderProduct(Convert.ToDouble(product.PreferentialPrice), Convert.ToDouble(product.PreferentialPV), ptNum, product.ProductID, 0, null, "", 0, 0, "");
            return orderProduct;
        }

        /// <summary>
        /// 将用户从界面选择的商品加入集合类
        /// </summary>
        /// <param name="productID">商品编号</param>
        /// <param name="list">原来所有商品的集合类</param>
        /// <param name="list2">用户选中商品的集合类</param>
        /// <returns>用户选中商品的集合类</returns>
        public OrderProduct2 GetUserChooseProduct2(int ptNum, ProductModel
product)
        {
            //新建结构
            OrderProduct2 orderProduct2 = new OrderProduct2();
            //去判断是否是组合产品
            if (product.IsCombineProduct == 1)
            {
                orderProduct2 = new OrderProduct2(Convert.ToDouble(product.PreferentialPrice), Convert.ToDouble(product.PreferentialPV), ptNum, product.ProductID, 0, product.ProductName, "true");
            }
            else
            {
                orderProduct2 = new OrderProduct2(Convert.ToDouble(product.PreferentialPrice), Convert.ToDouble(product.PreferentialPV), ptNum, product.ProductID, 0, product.ProductName, null);
            } //返回结构对象
            return orderProduct2;
        }

        public static bool WithdrawMoney(WithdrawModel wDraw)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    //记录提现明细
                    if (!DAL.ECTransferDetailDAL.Withdraw(tran, wDraw))
                    {
                        tran.Rollback();
                        return false;
                    }

                    //更改已提现申请总额
                    if (!DAL.ECTransferDetailDAL.SetMemberShip(tran, wDraw.Number, wDraw.WithdrawMoney, decimal.Parse(wDraw.WithdrawSXF.ToString())))
                    {
                        tran.Rollback();
                        return false;
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        public static bool WithdrawMoney1(WithdrawModel wDraw)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    //记录提现明细
                    if (!DAL.ECTransferDetailDAL.Withdraw(tran, wDraw))
                    {
                        tran.Rollback();
                        return false;
                    }
                    if (wDraw.IsJL == 0)
                    {
                        //冻结提现金额和违约金 和手续费 ，以石斛积分额度冻结
                        //if (!DAL.ECTransferDetailDAL.SetMemberShip1JB(tran, wDraw))
                        //{
                        //    tran.Rollback();
                        //    return false;
                        //}
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        /// <summary>
        /// 审核提现——ds2012——tianfeng
        /// </summary>
        /// <param name="dModel"></param>
        /// <returns></returns>
        public static bool AuditWithdraw(WithdrawModel dModel)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (!ECTransferDetailDAL.SetWithdrawState(tran, dModel))
                    {
                        tran.Rollback();
                        return false;
                    }
                    if (!ECTransferDetailDAL.SetMemberOut(tran, dModel.Number, dModel.WithdrawMoney, dModel.Wyj))
                    {
                        tran.Rollback();
                        return false;
                    }
                    if (dModel.Wyj == 0)
                    {
                        
                        D_AccountBLL.AddAccountTran(dModel.Number, dModel.WithdrawMoney, D_AccountSftype.MemberType, D_Sftype.BounsAccount, D_AccountKmtype.Memberwithdraw, DirectionEnum.AccountReduced, "006633~【" + dModel.Number + "】~008024~,手续费为" + dModel.WithdrawSXF.ToString("#0.00"), tran);

                        tran.Commit();
                        return true;
                    }
                    else
                    {
                        D_AccountBLL.AddAccountTran(dModel.Number, dModel.WithdrawMoney, D_AccountSftype.usdtjj, D_Sftype.usdtjj, D_AccountKmtype.Memberwithdraw, DirectionEnum.AccountReduced, "006633~【" + dModel.Number + "】~008024~,手续费为" + dModel.WithdrawSXF.ToString("#0.00"), tran);

                        tran.Commit();
                        return true;
                    }
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 是否已删除核提现申请——ds2012——tianfeng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isDelWithdraw(int id)
        {
            return ECTransferDetailDAL.isDelWithdraw(id);
        }

        /// <summary>
        /// 是否已审核提现申请——ds2012——tianfeng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetAuditState(int id)
        {
            return ECTransferDetailDAL.GetAuditState(id);
        }

        /// <summary>
        /// 删除提现申请——ds2012——tianfeng
        /// </summary>
        /// <param name="dModel"></param>
        /// <returns></returns>
        public static bool DeleteWithdraw(WithdrawModel dModel)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (!ECTransferDetailDAL.DeleteWithdraw(tran, dModel.Id, dModel.WithdrawMoney, dModel.Number))
                    {
                        tran.Rollback();
                        return false;
                    }
                    tran.Commit();
                    return true;
                }
                catch
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 计算用户选择的总金额
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public double getZongJing(ArrayList list)
        {
            if (list == null)
            {
                return 0;
            }
            double zongjing = 0;
            for (int i = 0; i < list.Count; i++)
            {
                zongjing += ((OrderProduct)list[i]).price * ((OrderProduct)list[i]).count;
            }
            return zongjing;
        }

        /// <summary>
        /// 计算用户选择的总金额
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public double getZongJing(IList<MemberDetailsModel> list)
        {
            if (list == null)
            {
                return 0;
            }
            double zongjing = 0;
            for (int i = 0; i < list.Count; i++)
            {
                zongjing += Convert.ToDouble(list[i].Price) * Convert.ToDouble(list[i].Quantity);
            }
            return zongjing;
        }

        /// <summary>
        /// 计算有货的总金额
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public double getEnoughProductMoney(IList<MemberDetailsModel> list, string storeID)
        {
            //店铺由于货不足必须从可以保单的金额中扣去的金额
            double needPayMoney = 0;
            //得到店库存对象集合
            List<StockModel> list2 = list2 = addOrderDataDAL.GetStock(storeID);

            //循环将用户选择的商品数和库存中的库存做比较
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list2.Count; j++)
                {
                    //找到匹配商品
                    if (list2[j].ProductId == list[i].ProductId)
                    {
                        //查看库存该匹配的商品数是否足够
                        double restNumber = Convert.ToDouble(list2[j].ActualStorage) - list[i].Quantity;
                        //如果不足
                        if (restNumber < 0)
                        {
                            //如果该商品实际库存小于0的时候，用户选择的商品的数目就是不足的数量
                            if ((list2[j].ActualStorage) > 0)
                            {
                                //OrderProduct orderProduct = (OrderProduct)list[i];
                                //list[i] = orderProduct;
                                needPayMoney += ((double)list2[j].ActualStorage) * Convert.ToDouble(list[i].Price);
                            }
                        }
                        else
                        {
                            needPayMoney += ((double)list[i].Quantity) * Convert.ToDouble(list[i].Price);
                        }
                    }
                }
            }
            //}
            return needPayMoney;
        }



        public double getZongJing2(ArrayList list, List<OrderProduct> GroupItemm)
        {
            double zongjing = 0;

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < GroupItemm.Count; j++)
                {

                }
                zongjing += ((OrderProduct)list[i]).price * ((OrderProduct)list[i]).count;
            }
            return zongjing;

        }


        /// 计算用户选择的积分
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public double getZongPv(ArrayList list)
        {
            if (list == null)
            {
                return 0;
            }
            double zongPv = 0;
            for (int i = 0; i < list.Count; i++)
            {
                zongPv += ((OrderProduct)list[i]).pv * ((OrderProduct)list[i]).count;
            }
            return zongPv;
        }

        /// 计算用户选择的积分
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public double getZongPv(IList<MemberDetailsModel> list)
        {
            if (list == null)
            {
                return 0;
            }
            double zongPv = 0;
            for (int i = 0; i < list.Count; i++)
            {
                zongPv += Convert.ToDouble(list[i].Pv) * Convert.ToDouble(list[i].Quantity);
            }
            return zongPv;
        }

        /// <summary>
        /// 与库存比较，如果货不够扣钱的金额
        /// </summary>
        /// <param name="list">用户选择商品的结构集合</param>
        /// <param name="storeID">店编号</param>
        /// <returns></returns>
        /// 
        public double CheckMoneyIsEnough(IList<MemberDetailsModel> list, string storeID)
        {
            //店铺由于货不足必须从可以保单的金额中扣去的金额
            double needPayMoney = 0;
            //得到店库存对象集合
            List<StockModel> list2 = addOrderDataDAL.GetStock(storeID);
            //如果店库存内没有任何商品，需要支付的钱就是用户选择的所有商品的价值总和
            if (list2.Count <= 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].NotEnoughProduct = Convert.ToInt32(list[i].Quantity);
                    needPayMoney += Convert.ToDouble(list[i].Quantity) * Convert.ToDouble(list[i].Price);
                }
                return needPayMoney;
            }
            else
            {
                //循环将用户选择的商品数和库存中的库存做比较
                for (int i = 0; i < list.Count; i++)
                {
                    List<StockModel> list3 = addOrderDataDAL.GetProductStock(storeID, list[i].ProductId.ToString());

                    if (list3.Count > 0)
                    {
                        //查看库存该匹配的商品数是否足够
                        double restNumber = Convert.ToDouble(list3[0].ActualStorage) - list[i].Quantity;
                        //如果不足
                        if (restNumber < 0)
                        {
                            //如果该商品实际库存小于0的时候，用户选择的商品的数目就是不足的数量
                            if (Convert.ToInt32((list3[0].ActualStorage)) < 0)
                            {

                                list[i].NotEnoughProduct = Convert.ToInt32(list[i].Quantity);
                                needPayMoney += Convert.ToDouble(list[i].Quantity) * Convert.ToDouble(list[i].Price);
                            }
                            //否则restNumber就是不足的数量
                            else
                            {

                                list[i].NotEnoughProduct = Convert.ToInt32(restNumber) * (-1);
                                needPayMoney += restNumber * Convert.ToDouble(list[i].Price) * (-1);
                            }
                        }
                        else
                        {
                            list[i].NotEnoughProduct = 0;
                        }
                    }
                    else
                    {

                        list[i].NotEnoughProduct = Convert.ToInt32(list[i].Quantity);
                        needPayMoney += Convert.ToDouble(list[i].Quantity) * Convert.ToDouble(list[i].Price);
                    }
                }
            }
            return needPayMoney;
        }

        /// <summary>
        /// 与库存比较，如果货不够扣钱的金额
        /// </summary>
        /// <param name="list">用户选择商品的结构集合</param>
        /// <param name="storeID">店编号</param>
        /// <returns></returns>
        public double CheckMoneyIsEnough(IList<MemberDetailsModel> list, string storeID, string orderid)
        {
            //店铺由于货不足必须从可以保单的金额中扣去的金额
            double needPayMoney = 0;

            //得到店库存对象集合
            List<StockModel> list2 = new List<StockModel>();
            if (orderid == "")
                list2 = addOrderDataDAL.GetStock(storeID);
            else
                list2 = addOrderDataDAL.GetStock(storeID, orderid);

            //如果店库存内没有任何商品，需要支付的钱就是用户选择的所有商品的价值总和
            if (list2.Count <= 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    //OrderProduct orderProduct = (OrderProduct)list[i];
                    //orderProduct.notEnoughProduct = Convert.ToInt32(((OrderProduct)list[i]).count);
                    //list[i] = orderProduct;
                    list[i].NotEnoughProduct = Convert.ToInt32(list[i].Quantity);
                    needPayMoney += Convert.ToDouble(list[i].Quantity) * Convert.ToDouble(list[i].Price);
                }
                return needPayMoney;
            }
            else
            {
                //循环将用户选择的商品数和库存中的库存做比较
                for (int i = 0; i < list.Count; i++)
                {
                    StockModel stm = new StockModel();
                    int count = 0;
                    foreach (StockModel sm in list2)
                    {
                        if (sm.ProductId == list[i].ProductId)
                        {
                            stm = sm;
                            count++;
                        }
                    }
                    //  List<StockModel> list3 = addOrderDataDAL.GetProductStock(storeID, list[i].ProductId.ToString());

                    if (count > 0)
                    {
                        //查看库存该匹配的商品数是否足够
                        double restNumber = Convert.ToDouble(stm.ActualStorage) - list[i].Quantity;
                        //如果不足
                        if (restNumber < 0)
                        {
                            //如果该商品实际库存小于0的时候，用户选择的商品的数目就是不足的数量
                            if (Convert.ToInt32(stm.ActualStorage) < 0)
                            {
                                //OrderProduct orderProduct = (OrderProduct)list[i];
                                //orderProduct.notEnoughProduct = (int)orderProduct.count;
                                //list[i] = orderProduct;
                                list[i].NotEnoughProduct = Convert.ToInt32(list[i].Quantity);
                                needPayMoney += Convert.ToDouble(list[i].Quantity) * Convert.ToDouble(list[i].Price);
                            }
                            //否则restNumber就是不足的数量
                            else
                            {
                                //OrderProduct orderProduct = (OrderProduct)list[i];
                                //orderProduct.notEnoughProduct = Convert.ToInt32(restNumber) * (-1);
                                //list[i] = orderProduct;
                                list[i].NotEnoughProduct = Convert.ToInt32(restNumber) * (-1);
                                needPayMoney += restNumber * Convert.ToDouble(list[i].Price) * (-1);
                            }
                        }
                    }
                    else
                    {
                        //OrderProduct orderProduct = (OrderProduct)list[i];
                        //orderProduct.notEnoughProduct = (int)orderProduct.count;
                        //list[i] = orderProduct;
                        list[i].NotEnoughProduct = Convert.ToInt32(list[i].Quantity);
                        needPayMoney += Convert.ToDouble(list[i].Quantity) * Convert.ToDouble(list[i].Price);
                    }
                }
            }
            return needPayMoney;
        }


        /// <summary>
        /// 为了方便逻辑处理，奖组合产品集合和非组合产品集合分开处理
        /// </summary>
        /// <param name="list"></param>
        /// <param name="GroupItemm"></param>
        /// <param name="NoGroupItem"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public ArrayList Diff(ArrayList list, List<OrderProduct3> GroupItemm, List<OrderProduct3> NoGroupItem, string storeId, out string errMes, string mode, List<MemberDetailsModel> orignalList, out List<OrderProduct3> smallItem, ArrayList AllsmallItemList)
        {

            GroupItemm = new List<OrderProduct3>();
            NoGroupItem = new List<OrderProduct3>();
            for (int i = 0; i < list.Count; i++)
            {
                OrderProduct3 orderProduct3 = new OrderProduct3(((OrderProduct)list[i]).price, ((OrderProduct)list[i]).pv, ((OrderProduct)list[i]).count, ((OrderProduct)list[i]).id, ((OrderProduct)list[i]).notEnoughProduct, ((OrderProduct)list[i]).isGroupItem, ((OrderProduct)list[i]).hasSubItem, 0, ((OrderProduct)list[i]).isInGroupItemCount, ((OrderProduct)list[i]).only);
                if (orderProduct3.IsGroupItem == "true")
                {
                    GroupItemm.Add(orderProduct3);
                }
                else
                {
                    NoGroupItem.Add(orderProduct3);
                }
            }
            list.Clear();
            //返回
            return HasGroup(list, GroupItemm, NoGroupItem, storeId, out errMes, mode, orignalList, out smallItem, AllsmallItemList);


        }


        #region 组合产品，暂时不用
        /// <summary>
        ///  组合产品中小商品和所有选择的商品中去匹配是否有相同
        ///  的商品，思路是先取得所有组合商品的小产品，然后和其他非组合商品的相比，
        ///  如果小产品和非组合商品是同一类商品，则做标记并且累加count,
        /// （关键是组合产品中的小商品都放入NoGroupItem中， 方便后续代码）
        ///  最后将所有的商品和组合产品的信息全部放入 ArrayList，为了让后面进memberDetails表时
        ///  容易处理
        /// </summary>
        /// <param name="list">大集合（包括组合产品和所有其他商品的集合）</param>
        /// <param name="GroupItem">组合产品集合</param>
        /// <param name="GroupItem">非组合产品集合</param>
        /// <param name="storeID">店编号</param>
        /// <returns></returns>
        public ArrayList HasGroup(ArrayList list, List<OrderProduct3> GroupItem, List<OrderProduct3> NoGroupItem, string storeID, out string errMes, string mode, List<MemberDetailsModel> orignalList, out List<OrderProduct3> smallItem, ArrayList AllsmallItemList)
        {
            string CheckInfo = null;
            string GroupItemId = null;
            smallItem = new List<OrderProduct3>();
            ArrayList allList = new ArrayList();
            ArrayList CheckList = new ArrayList();
            //取得所有的小商品集合
            for (int i = 0; i < GroupItem.Count; i++)
            {
                if (i != GroupItem.Count - 1)
                {
                    GroupItemId += GroupItem[i].Id + ",";
                }
                else
                {
                    GroupItemId += GroupItem[i].Id + "";
                }
                OrderProduct opt = new OrderProduct(GroupItem[i].Price, GroupItem[i].Pv, GroupItem[i].Count, GroupItem[i].Id, GroupItem[i].NotEnoughProduct, GroupItem[i].IsGroupItem, GroupItem[i].HasSubItem, 0, GroupItem[i].IsInGroupItemCount, GroupItem[i].Only);
                smallItem = new AddMemberInfomDAL().GetSamllItemList(GroupItem[i].Id.ToString());
                for (int m = 0; m < smallItem.Count; m++)
                {
                    //取得所有小商品,别忘记为OrderProduct属性赋值思路是组合产品的数量*
                    smallItem[m].Count *= GroupItem[i].Count;
                    smallItem[m].Only = "true";
                    //去数据库库存表的数量做比较
                    CheckInfo = new AddOrderDataDAL().CheckGroupSmallCount(Convert.ToInt32(smallItem[m].Count), smallItem[m].Id) > 0 ? (null) : ("notEnough");

                }
                AllsmallItemList.Add(smallItem);
            }

            //在多有组合商品中循环 
            for (int m = 0; m < AllsmallItemList.Count; m++)
            {
                smallItem = (List<OrderProduct3>)AllsmallItemList[m];
                // 循环遍历匹配小商品和非组合商品，如果相等，做标记并且累计购货数量,不是则将小商品添加到NoGroupItem集合中
                for (int j = 0; j < smallItem.Count; j++)
                {
                    for (int k = 0; k < NoGroupItem.Count; k++)
                    {
                        if (smallItem[j].Id == NoGroupItem[k].Id)
                        {
                            // NoGroupItem[k].IsGroupItem = "true";
                            NoGroupItem[k].HasSubItem = "true";//做标记是组合产品中的小产品
                            NoGroupItem[k].Count += smallItem[j].Count;
                            NoGroupItem[k].IsInGroupItemCount += Convert.ToInt32(smallItem[j].Count);
                            NoGroupItem[k].Only = "";
                            smallItem[j].Only = "";
                        }

                    }
                }
            }

            for (int l = 0; l < NoGroupItem.Count; l++)
            {
                OrderProduct opt = new OrderProduct(NoGroupItem[l].Price, NoGroupItem[l].Pv, NoGroupItem[l].Count, NoGroupItem[l].Id, NoGroupItem[l].NotEnoughProduct, NoGroupItem[l].IsGroupItem, NoGroupItem[l].HasSubItem, 0, NoGroupItem[l].IsInGroupItemCount, NoGroupItem[l].Only);
                list.Add(opt);
            }
            if (mode == "add")
            {
                errMes = CheckStockIfGroup(NoGroupItem, storeID, GroupItemId, CheckInfo);
            }
            else
            {
                errMes = CheckStockIfGroupEdit(NoGroupItem, orignalList, storeID);
            }
            return list;
        }

        /// <summary>
        /// 检查有组合商品的库存
        /// </summary>
        /// <param name="allChoseProduct"></param>
        /// <returns></returns>
        public string CheckStockIfGroup(List<OrderProduct3> noGroupItem, string storeId, string GroupItemId, string checkInfo)
        {

            string errMessgae = null;

            if (checkInfo != null)
            {
                errMessgae = "抱歉!您选择的组合产品中没有库存";
                return errMessgae;
            }
            List<StockModel> list2 = addOrderDataDAL.GetStock(storeId);
            if (list2.Count <= 0)
            {
                errMessgae = "抱歉!您选择的组合产品中没有库存";
                return errMessgae;
            }


            for (int i = 0; i < noGroupItem.Count; i++)
            {
                for (int j = 0; j < list2.Count; j++)
                {
                    if (((OrderProduct3)noGroupItem[i]).Id == list2[j].ProductId)
                    {
                        if (((OrderProduct3)noGroupItem[i]).HasSubItem == "true")
                        {
                            int result = Convert.ToInt32(((OrderProduct3)noGroupItem[i]).Count);
                            //通过数据库去找他在组合商品里的个数
                            int inGroupCount = new AddOrderDataDAL().GetSmallItemInGroup(((OrderProduct3)noGroupItem[i]).Id, GroupItemId) + result;
                            errMessgae = result >= 0 ? (null) : ("抱歉!您选择的组合产品中没有库存");
                            return errMessgae;
                        }
                    }
                }
            }
            return errMessgae;
        }


        /// <summary>
        /// 有组合产品报单修改时比较复杂，先将修改后用户选择的数量-修改前用户选择的数量(用户选择的数量必须大于修改前用户选择的数量时)
        /// 然后在和库存进行比较
        /// </summary>
        /// <param name="NoGroupItem"></param>
        /// <param name="orignalList"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public string CheckStockIfGroupEdit(List<OrderProduct3> NoGroupItem, List<MemberDetailsModel> orignalList, string storeId)
        {
            string errMessgae = null;
            List<StockModel> list2 = addOrderDataDAL.GetStock(storeId);
            if (list2.Count <= 0)
            {
                errMessgae = "抱歉!您选择的组合产品中没有库存";
                return errMessgae;
            }

            //for (int i = 0; i < orignalList.Count; i++)
            //{
            //    for (int j = 0; j < NoGroupItem.Count; j++)
            //    {
            //        if (orignalList[i].ProductId == NoGroupItem[j].Id)
            //        {
            //            if (NoGroupItem[j].HasSubItem == "true")
            //            {
            //                if (orignalList[i].Quantity < NoGroupItem[j].Count)
            //                {
            //                    NoGroupItem[j].Diff = Convert.ToInt32(NoGroupItem[j].Count - orignalList[i].Quantity);
            //                }

            //            }
            //        }

            //    }


            //    //for (int k = 0; k < NoGroupItem.Count; k++)
            //    //{
            //    //    for (int l = 0; l < list2.Count; l++)
            //    //    {
            //    //        if (((OrderProduct3)NoGroupItem[k]).Id == list2[l].ProductId)
            //    //        {
            //    //            if (((OrderProduct3)NoGroupItem[k]).HasSubItem == "true")
            //    //            {
            //    //                int result = Convert.ToInt32(list2[l].ActualStorage) - ((OrderProduct3)NoGroupItem[k]).Diff - Convert.ToInt32(((OrderProduct3)NoGroupItem[k]).Count);
            //    //                errMessgae = result >= 0 ? (null) : ("抱歉!您选择的组合产品中没有库存");
            //    //                return errMessgae;
            //    //            }
            //    //        }

            //    //    }
            //    //}

            //}
            //return null; 
            return errMessgae;

        }



        //将
        public void ChangeValue(ArrayList list1, ArrayList list2)
        {
            for (int i = 0; i < list2.Count; i++)
            {
                for (int j = 0; j < list1.Count; j++)
                {
                    if (((OrderProduct)list1[j]).id == ((OrderProduct)list2[i]).id)
                    {
                        int orignalCount = Convert.ToInt32(((OrderProduct)list2[i]).count);
                        list2.RemoveAt(i);
                        OrderProduct opt = new OrderProduct(((OrderProduct)list1[j]).price, ((OrderProduct)list1[j]).pv, orignalCount, ((OrderProduct)list1[j]).id, ((OrderProduct)list1[j]).notEnoughProduct, ((OrderProduct)list1[j]).isGroupItem, ((OrderProduct)list1[j]).hasSubItem, 0, ((OrderProduct)list1[j]).isInGroupItemCount, ((OrderProduct)list1[j]).only);
                        list2.Add(opt);
                    }
                }
            }
        }

        #endregion



        //-----------------------------------------------以下是注册报单方法-----------------------------------
        /// <summary>
        /// 检查店编号是否存在,返回false 表示已存在
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool CheckExistsStoreinfo(string storeid)
        {
            return addOrderDataDAL.CheckExistsStoreinfo(storeid);
        }
        /// <summary>
        /// 会员报单底线
        /// </summary>
        /// <returns></returns>
        public object GetOrderBaseLine()
        {
            return addOrderDataDAL.GetOrderBaseLine();
        }

        /// <summary>
        /// 获取语言
        /// </summary>
        /// <returns></returns>
        public System.Data.DataTable GetLanguage()
        {
            return addOrderDataDAL.GetLanguage();
        }
        /// <summary>
        ///会员注册编辑共用方法
        /// </summary>
        /// <returns></returns>
        public string AddMemForRegister()
        {
            return null;
        }

        public string AgeIs18(string date)
        {
            try
            {
                //date = date.Substring(0, 4);
                //if (Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(date) < 18)
                //{
                //    return this.GetTran("000336", "你输入的出生年月不符合大于18岁的标准！");// "你输入的出生年月不符合大于18岁的标准！";
                //}
                //if (Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(date) > 80)
                //{
                //    return this.GetTran("006815", "你输入的出生年月不符合小于80岁的标准！");
                //}
                DateTime d1 = Convert.ToDateTime(date);
                DateTime d2 = DateTime.Now;
                TimeSpan s = d2 - d1;
                double ss = s.TotalDays;
                if (ss < 18 * 365)
                {
                    return this.GetTran("000336", "你输入的出生年月不符合大于18岁的标准！");// "你输入的出生年月不符合大于18岁的标准！";
                }
                if (ss > 80 * 365)
                {
                    return this.GetTran("006815", "你输入的出生年月不符合小于80岁的标准！");
                }

            }
            catch (Exception)
            {
                return this.GetTran("000338", "你输入的出生年年月非法");//"你输入的出生年年月非法";
            }
            return null;
        }
        /// <summary>
        /// 判断是否是当前期数
        /// </summary>
        /// <param name="maxQiShu">输入的最大期数</param>
        /// <returns>是否是最大期数</returns>
        public bool IsMaxQiShu(int maxQiShu)
        {
            return CommonDataBLL.getMaxqishu() == maxQiShu;
        }
        /// <summary>
        /// 判断会员名是否小于6位
        /// </summary>
        /// <returns></returns>
        public bool NumberLength(string number)
        {
            //布尔变量
            bool judge = true;
            //会员编号长度
            judge = number.Length >= 6 ? (true) : (false);
            //验证字符串

            return judge;
        }

        /// <summary>
        /// 判断会员名是否合法
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool NumberCheckAgain(string number)
        {
            bool judge = true;
            string validSTR = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-1234567890";
            char[] va = number.ToCharArray();
            //循环验证
            for (int i = 0; i < va.Length; i++)
            {
                if (validSTR.IndexOf(va[i]) == -1)
                {
                    //messageLabel.Text = "编号请输入字母，数字，横线！";
                    judge = false;
                    break;
                }

            }
            return judge;
        }
        /// <summary>
        /// 去空格
        /// </summary>
        public void TrimNull(string[] text)
        {
            //for (int i = 0; i < text.Length; i++) 
            //{
            //   if(text[i].le) text[i] = text[i].Trim;
            //}
        }

        /// <summary>
        /// 是否该会员下是否有安置编号
        /// </summary>
        /// <param name="number">会员编号</param>
        /// <param name="mode">工作类型</param>
        /// <param name="Placement">推荐</param>
        /// <returns>是否有安置编号</returns>
        public string GetHavePlacedOrDriect(string number, string mode, string Placement, string derict)
        {
            string info = null;
            int flag = 0;

            string topMemberId = BLL.CommonClass.CommonDataBLL.GetTopManageID(3);

            //AddOrderDataDAL addOrderDataDAL = new AddOrderDataDAL();
            if (mode == "edit")
            {
                if (Placement != topMemberId)
                {
                    flag = addOrderDataDAL.CheckHavePlaced(number, Placement);
                    if (flag >= 2)
                    {
                        info = this.GetTran("000312", "此安置编号下已经安置了两个人");// "此安置编号下已经安置了两个人！";
                        return info;
                    }
                }

            }
            else if (Placement != topMemberId)
            {
                flag = addOrderDataDAL.CheckHavePlaced(number, Placement);
                if (flag >= 2)
                {
                    info = this.GetTran("000312", "此安置编号下已经安置了两个人");
                    return info;
                }
            }
            return info;
        }

        /// <summary>
        /// 是否是编辑状态
        /// </summary>
        /// <returns></returns>
        public string CheckIsNull(string Placement, string derict, string name)
        {
            string judge = null;

            judge = name != "" ? ("") : (this.GetTran("001284", "会员姓名不能为空"));
            if (judge != "")
            {
                return judge;
            }

            judge = derict != "" ? ("") : (this.GetTran("001283", "推荐编号不能为空"));
            if (judge != "")
            {
                return judge;
            }

            judge = Placement != "" ? ("") : (this.GetTran("001281", "安置编号不能为空"));
            if (judge != "")
            {
                return judge;
            }

            return null;
        }

        /// <summary>
        /// 得到昵称
        /// </summary>
        /// <param name="nickName">输入的昵称</param>
        /// <param name="name">输入的姓名</param>
        /// <returns>返回昵称</returns>
        public string GetNickName(string nickName, string name)
        {
            if (nickName == "")
            {
                nickName = name;
            }
            return nickName;
        }
        /// <summary>
        /// 无安置，无推荐检错信息
        /// </summary>
        /// <param name="placement">安置</param>
        /// <param name="derict">推荐</param>
        /// <returns></returns>
        public string GetError(string placement, string derict)
        {
            //查询结果
            int result = 0;

            string topMemberID = BLL.CommonClass.CommonDataBLL.GetTopManageID(3);

            //错误信息
            string info = null;
            if (placement == null)
            {
                result = addOrderDataDAL.GetError(derict);
                //如果找不到推荐号
                if (result <= 0 && placement != topMemberID)
                {
                    info = this.GetTran("000318", "无推荐！"); //"无推荐！";

                }
            }
            if (derict == null)
            {
                result = addOrderDataDAL.GetError(placement);
                //如果找不到安置号
                if (result <= 0 && placement != topMemberID)
                {
                    info += this.GetTran("000323", "无安置！");//"无安置！";
                }

            }

            return info;

        }


        public string GetError1(string Direct, string placementXuHao)
        {
            string info = null;
            bool isTrue = addOrderDataDAL.GetDirectPlacement(Direct, placementXuHao);
            if (!isTrue)
            {
                info += this.GetTran("005986", "安置编号必须在推荐编号的安置网络下面！");
            }

            return info;
        }
        /// <summary>
        /// 获取该会员的序号
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public int GetXuHao(string number)
        {
            int ret = -1;
            ret = (int)DBHelper.ExecuteScalar("select isnull(xuhao,-1) as xuhao from memberinfo where number='" + number + "'", CommandType.Text);
            return ret;

        }

        public bool GetError3(string tbh, string tuijian)
        {
            bool isTrue = addOrderDataDAL.GetDirectPlacement(tbh, tuijian);

            return isTrue;
        }

        public int GetError2(string number)
        {
            int ExpectNum = addOrderDataDAL.GetError2(number);
            return ExpectNum;
        }

        /// <summary>
        /// 注册会员检错1.无上级  2.无此店  3..死循环
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <param name="Placement">安置编号</param>
        /// <param name="Recommended">推荐编号</param>
        /// <param name="StoreID">店编号</param>
        /// <returns>检测结果</returns>
        public string CheckMemberInProc(string Number, string Placement, string Direct,
            string StoreID)
        {
            string info = null;
            //带入数据层中的存储过程 info为输出函数
            info = addOrderDataDAL.CheckMemberInProc(Number, Placement, Direct, StoreID, info);

            if (info != "检测信息" && info != "")
            {
                return info;
            }
            return null;
        }



        /// <summary>
        /// 注册会员检错1.无上级  2.无此店  3..死循环
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <param name="Placement">安置编号</param>
        /// <param name="Recommended">推荐编号</param>
        /// <param name="StoreID">店编号</param>
        /// <returns>检测结果</returns>
        public string CheckMemberInProc1(string Number, string LoginPass, string Direct, string MobileTele)
        {
            string info = null;
            //带入数据层中的存储过程 info为输出函数
            info = addOrderDataDAL.CheckMemberInProc1(Number, LoginPass, Direct, MobileTele);

           
            return info;
        }

        /// <summary>
        /// 检测并获取出生日期
        /// </summary>
        /// <param name="Birthday">出生日期</param>
        /// <returns>返回出生日期字符串</returns>
        public string CheckBirthDay(string birthday)
        {
            string info = null;
            DateTime birthday2;
            try
            {
                //转换
                birthday2 = Convert.ToDateTime(birthday);
                return birthday2.ToString();
            }
            catch
            {
                //出错显示
                info = "error";

            }

            return info;

        }
        /// <summary>
        /// 检测身份证
        /// </summary>
        /// <returns></returns>
        public string CheckPersonCardNumber(string PaperNumber, string sex, string mode, string district, string placement, string number, DateTime birthday)
        {
            #region
            //if (mode == "edit") //如果是修改
            //{
            //    if (placement != "")
            //    {
            //        int int_qucount = Convert.ToInt32(DBHelper.ExecuteScalar("select count(1) from MemberInfo where Placement='" + placement + "' and number<>'" + number + "'  and District=" + district));
            //        if (int_qucount == 1)
            //        {
            //            if (district== "1")
            //            {
            //                district= "2";
            //            }
            //            else
            //            {
            //                district= "1";
            //            }
            //        }
            //    }
            //    else 
            //    {

            //        if (placement != "")
            //        {
            //            int int_qucount = 0; //Convert.ToInt32(DBHelper.ExecuteScalar("select count(1) from h_info where anzhi='" + this.Txtsb.Text.Trim() + "'  and qu=" + district.SelectedValue));
            //            if (int_qucount == 1)
            //            {
            //                //if (district.SelectedValue == "1")
            //                //{
            //                //    district= "2";
            //                //}
            //                //else
            //                //{
            //                //    district= "1";
            //                //}
            //            }
            //        }
            //    }
            //}
            //return district;
            #endregion
            //判断男女
            bool boolSex = sex == "男" ? true : false;
            //转成数字
            int sex2 = boolSex ? 1 : 0;
            //错误信息
            string errMessage = null;
            if (!CommonClass.ValidData.ValidShengFenZheng(PaperNumber, birthday, sex2, out errMessage))
            {
                errMessage = "<script language='javascript'>alert('对不起，" + errMessage + "')</script>";

            }
            return errMessage;
        }

        /// <summary>
        ///  验证会员编号是否重复
        /// </summary>
        /// <param name="mode">类型</param>
        /// <param name="number">会员编号</param>
        /// <param name="id">会员ID</param>
        /// <returns>检查结果</returns>
        public string CheckNumberTwice(string number) //int id)
        {
            string info = "";
            //AddOrderDataDAL addOrderDataDAL = new AddOrderDataDAL();
            //if (mode == "edit")
            //{
            info = addOrderDataDAL.CheckNumberTwice(number) ? ("") : ("抱歉！该会员编号重复！");
            //}
            //else
            //{
            //info = addOrderDataDAL.CheckNumberTwice(number, id) ? ("") : ("抱歉！该会员编号重复！");
            //}
            if (info != "")
            {
                return info;
            }
            return null;
        }

        /// <summary>
        ///  验证手机号是否重复
        /// </summary>
        /// <param name="mode">类型</param>
        /// <param name="number">会员编号</param>
        /// <param name="id">会员ID</param>
        /// <returns>检查结果</returns>
        public string CheckTeleTwice(string txtTele)
        {
            string info = "";
            info = addOrderDataDAL.CheckTeleTwice(txtTele) ? ("") : ("抱歉！该手机号重复！");
            if (info != "")
            {
                return info;
            }
            return null;
        }

        /// <summary>
        /// 昵称是否重复
        /// </summary>
        /// <param name="mode">类型</param>
        /// <param name="petName">昵称</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string CheckNickNameTwice(string petName)
        {

            string info = "";
            info = addOrderDataDAL.GetPetName(petName) > 0 ? ("抱歉！该会员昵称重复！") : ("");
            if (info != "")
            {
                return info;
            }
            return null;
        }

        /// <summary>
        /// 检查该会员电子账户密码是否正确
        /// </summary>
        public string CheckEctPassWord(string selectValue, string number, string elcPassword)
        {
            //验证信息
            string info = null;
            //如果是电子账户支付
            if (selectValue == "2")
            {
                //访问数据层验证方法
                int int_dzmimacount = addOrderDataDAL.GetElcPassWord(number, elcPassword);
                //验证结果
                info = int_dzmimacount == 0 ? (this.GetTran("000411", "报单账户密码错误")) : ("");//"电子钱包密码错误!"

            }

            //ds
            if (info != "")
            {
                return info;
            }
            return null;
        }

        /// <summary>
        /// 检查收货地址，
        /// 如果不是自提的话收货地址必填
        /// </summary>
        /// <param name="selectValue">用户选择的支付方式</param>
        /// <param name="address">地址</param>
        /// <returns>验证结果</returns>
        public string CheckAddress(string selectValue, string address)
        {
            //验证信息
            string info = null;
            //如果提货方式
            if (selectValue == "1")
            {
                //如果收货地址为空
                if (address.Trim() == "")
                {
                    info = "收货地址不能为空!";
                }

            }
            return info;
        }


        /// <summary>
        /// 返回密码（长度大于6位截取钱6位，否则取会员编号截取前6位）
        /// </summary>
        /// <param name="password">用户输入的密码</param>
        /// <param name="number">用户输入的密码</param>
        /// <returns>修改后的密码</returns>
        public string ReturnUserPassword(string password, string number)
        {
            password = password == null ? (number) : (password);
            if (password.Length >= 6)
            {
                return password.Substring(password.Length - 6, 6);
            }
            else
            {
                return number.Substring(number.Length - 6, 6);
            }

        }
        /// <summary>
        /// 返回电子钱包密码（长度大于6位截取钱6位，否则取会员编号截取前6位）
        /// </summary>
        /// <param name="password">用户输入的密码</param>
        /// <param name="elcPassword">用户输入的电子密码</param>
        /// <returns>修改后的密码</returns>
        public string ReturnUserElcPassword(string elcPassword, string number)
        {
            elcPassword = elcPassword == null ? (number) : (elcPassword);
            if (elcPassword.Length >= 6)
            {
                return elcPassword.Substring(elcPassword.Length - 6, 6);
            }
            else
            {
                return elcPassword.Substring(number.Length - 6, 6);
            }

        }



        /// <summary>
        /// 读取用户输入的订货信息
        /// 保存订单信息和汇总信息(界面层)
        /// </summary>
        #region
        public List<int> GetUserShopInfo()
        {

            // SqlDataReader productReader = ProductData.GetProductList(); 
            //返回list泛型
            /*foreach()
             * {
             *  if(double pdtNum>0)
             *  {
             *     double price = Convert.ToDouble(productReader["PreferentialPrice"]);
				   double pv = Convert.ToDouble(productReader["PreferentialPV"]);
             *     zongji+=price * pdtNum;
             *     zongPv += pv * pdtNum;
             *  }
             * }
             * if(list.count==0)
             * {
             *   没有输入订货信息
             *   return;
             * }
             * double MemBaseLine = Convert.ToDouble( DBHelper.ExecuteScalar("Select OrderBaseLine From bsco_MemOrderLine"));
             * if(zongji<MemBaseLine)
             * {
             *   	msg = "<script language='javascript'>alert('对不起，你订购的金额不能小于"+MemBaseLine.ToString("C")+"！');</script>";
					return;
             * }
             * 
             * 
             * 
             */
            return null;
        }
        #endregion

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
        public string CheckOption(string mode, double notEnProMoney, MemberOrderModel mo)
        {
            //storeDataDAL数据类对象
            StoreDataDAL storeDataDAL = new StoreDataDAL();
            //上次报单金额
            double prevMoney = 0;
            //错误信息
            string msg = null;
            //如果是编辑
            if (mode == "edit")
            {
                //补回上次该报单上传不足货物时补交的钱
                prevMoney += addOrderDataDAL.NeedReturnMoney(mo.OrderId, mo.StoreId);
            }
            double storeLeftMoney = storeDataDAL.GetLeftRegisterMemberMoney(mo.StoreId) + prevMoney;
            //判断是否已经注册
            double bottonLine = new AddOrderBLL().GetBottomLine();
            //如果报单额不足
            if (notEnProMoney > storeLeftMoney)
            {
                //如果是现金支付
                if (mo.DefrayType.ToString() == "1")
                {
                    msg = "<script language='javascript'>alert('" + this.GetTran("000424", "对不起，您的报单额不足！") + "');</script>";
                }
            }
            //判断报单底线
            if (Session["Company"] == null)
            {
                if (mo.TotalMoney < Convert.ToDecimal(bottonLine))
                {
                    msg = "<script language='javascript'>alert('" + this.GetTran("000428", "对不起，你报单的金额必须大于等于保单底线") + bottonLine + GetTran("000564", "元") + "！');</script>";
                }
            }
            return msg;

        }

        /// <summary>
        /// 取得支付状态
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="mo"></param>
        /// <returns></returns>
        public int GetDefreyState(string mode, MemberOrderModel mo)
        {

            int defreyState = 0;
            //如果是店铺注册或已经通过的修改，支付状态则为通过
            if (mo.OrderType == 0 || mo.DefrayState == 1)
            {
                defreyState = 1;
            }
            return defreyState;
        }
        /// <summary>
        /// 电子钱包的钱够不够本次报单（老董已经完成）
        /// </summary>
        /// <returns></returns>
        #region
        public bool EaIsEnough(string selectValue, string elcNumber, double zongjing, string mode)
        {
            if (selectValue == "2") //
            {
                double elcmoney = 0;//Convert.ToDouble(DBHelper.ExecuteScalar("select isnull((ECTJackpot-ECTDeclarations-ReleaseMoney-Out),0) from MemberInfo where Number='" + elcNumber + "'"));
                if (elcmoney + zongjing < Convert.ToDouble(zongjing))
                {
                    //this.Application.UnLock();
                    //Response.Write("<script>alert('用户" + this.txtdzbh.Text + "的电子钱包不够本次报单！！！');</script>");
                    //return;
                }

            }

            return true;

        }
        #endregion
        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <param name="orderID">订单号</param>
        /// <returns>确认后的订单号</returns>
        public string GetOrderInfo(string mode, string orderId)
        {
            string strOrderId = null;
            if (mode != "edit")//编辑状态
            {

                strOrderId = orderDAL.GetNewOrderID();
                if (CheckOrderIdExists(mode, strOrderId) != null)
                {
                    strOrderId = null;
                }
            }
            else
            {
                if (orderId != null)
                {
                    strOrderId = orderId;
                }
            }

            return strOrderId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public string CheckOrderIdExists(string mode, string orderID)
        {
            string info = null;
            if (mode == "edit")
            {
                if (OrderDAL.CheckOrderIdExists(orderID))
                {
                    info = "订单号重复";

                }
            }
            return info;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="mode">页面状态（新增，修改）</param>
        /// <param name="photopath">图片路径</param>
        /// <param name="realDirName">路径名</param>
        /// <param name="oldFilePath">老路径</param>
        /// <param name="file"></param>
        /// <param name="newfileName"></param>
        /// <param name="photoW"></param>
        /// <param name="photoH"></param>
        /// <returns></returns>
        public string UploadPic(string mode, string photopath, string realDirName, string oldFilePath, HtmlInputFile file, out string newFileName, out int photoW, out int photoH)
        {
            photoW = 0;
            photoH = 0;
            string info = "";
            string oldFileName = "";
            newFileName = "";
            string newFilePath = "";
            //如果用户输入路径不为空
            if (oldFilePath != string.Empty)
            {
                if (mode == "edit")
                {
                    if (photopath != string.Empty)
                    {
                        if (System.IO.File.Exists(photopath))
                        {
                            System.IO.File.Delete(photopath);
                        }

                    }
                }
                //检查目录是否存在
                //界面实现 dirName = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();  
                if (!Directory.Exists(realDirName))
                {
                    Directory.CreateDirectory(realDirName);
                }
                //取得原文件夹名
                oldFileName = Path.GetFileName(oldFilePath);

                //取得后缀名
                string fileExtName = Path.GetExtension(oldFilePath);
                //判断后缀名
                if (fileExtName.ToLower() != ".gif" && fileExtName.ToLower() != ".jpg" && fileExtName.ToLower() != ".jpeg")
                {
                    //Response.Write("<script>alert('上传文件格式不正确！');</script>");
                    info = "上传文件格式不正确，只能上传.gif、.jpg或者.jpeg格式的图片！";// this.GetTran("000823", "上传文件格式不正确！");
                    return info;
                }

                if (file.PostedFile.ContentLength > 51200)
                {
                    info = this.GetTran("000824", "上传文件不能大于50K！");
                    return info;
                }

                //随机产生新的文件名
                System.Random rd = new Random(0);
                newFileName = DateTime.Now.Year.ToString() + rd.Next(10).ToString()
                        + DateTime.Now.Month.ToString() + rd.Next(10).ToString()
                        + DateTime.Now.Day.ToString() + rd.Next(10).ToString()
                        + DateTime.Now.Second.ToString()
                        + fileExtName;
                newFilePath = realDirName + "\\" + newFileName;
                //文件上传
                file.PostedFile.SaveAs(newFilePath);
                //取得图片的高度和宽度

                try
                {
                    System.Drawing.Image myIma = System.Drawing.Image.FromFile(newFilePath);
                    photoH = myIma.Height;
                    photoW = myIma.Width;

                }
                catch (Exception)
                {
                    info = this.GetTran("000823", "上传文件格式不正确！");
                    return info;
                }
            }
            return info;
        }

        /// <summary>
        /// 删除旧记录
        /// </summary>
        public bool DelOldInfo()
        {
            return true;
        }


        /// <summary>
        /// 获取当前期数
        /// </summary>
        /// <returns></returns>
        public int GetQiShu()
        {
            return CommonDataBLL.getMaxqishu();
        }

        /// <summary>
        /// 验证证件号码
        /// </summary>
        /// <returns>是否合格</returns>
        public static bool VerifyPaperNumber(string paperNumber, string cardType)
        {
            DateTime dtime = DateTime.Now;
            bool mf = true;
            if (paperNumber != "")
            {
                if (cardType == "2")
                {
                    if (paperNumber.Length != 15 && paperNumber.Length != 18)
                    {
                        return false;
                    }
                    else
                    {
                        if (paperNumber.Length == 15)
                        {
                            //int n=DBHelper.ExecuteNonQuery("select * from H_info where PaperNumber='"+paperNumber.ToString()+"'");
                            //							DataTable dt=DBHelper.ExecuteDataTable("select * from h_info where papernumber='"+paperNumber.ToString()+"'");
                            //							if(dt.Rows.Count>0)
                            //							{
                            //								Response.Write (ReturnAlert ("这个身份证已经注册了，请换一个！"));
                            //								return;
                            //							}
                            //							else
                            //							{
                            int cardMon = Convert.ToInt32(paperNumber.ToString().Substring(8, 2));
                            int cardDay = Convert.ToInt32(paperNumber.ToString().Substring(10, 2));
                            int cardYear = Convert.ToInt32("19" + paperNumber.ToString().Substring(6, 2));//身份证的日期/年

                            int nowtime = dtime.Year;//今天的日期
                            if (nowtime - cardYear > 18 && nowtime - cardYear < 100)
                            {

                                if (cardMon <= 12)
                                {
                                    if (cardDay <= 31)
                                    {
                                        int a = Convert.ToInt32(paperNumber.ToString().Substring(14, 1));
                                        if (a % 2 == 0)
                                        {
                                            mf = false;
                                        }
                                        else
                                        {
                                            mf = true;
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            //							}
                        }
                        if (paperNumber.Length == 18)
                        {
                            //							DataTable dt=DBHelper.ExecuteDataTable("select * from h_info where papernumber='"+paperNumber.ToString()+"'");
                            //							//int n=DBHelper.ExecuteNonQuery("select count(*) from h_info where papernumber='"+paperNumber.ToString()+"'");
                            //							if(dt.Rows.Count>0)
                            //							{
                            //								Response.Write (ReturnAlert ("这个身份证已经注册了，请换一个！"));
                            //								return;
                            //							}
                            //							else
                            //							{
                            ArrayList list = new ArrayList();
                            list.Add("7");
                            list.Add("9");
                            list.Add("10");
                            list.Add("5");
                            list.Add("8");
                            list.Add("4");
                            list.Add("2");
                            list.Add("1");
                            list.Add("6");
                            list.Add("3");
                            list.Add("7");
                            list.Add("9");
                            list.Add("10");
                            list.Add("5");
                            list.Add("8");
                            list.Add("4");
                            list.Add("2");
                            int sum = 0;
                            int ji = 1;
                            int cardMon = Convert.ToInt32(paperNumber.ToString().Substring(10, 2));
                            int cardDay = Convert.ToInt32(paperNumber.ToString().Substring(12, 2));
                            int cardYear = Convert.ToInt32(paperNumber.ToString().Substring(6, 4));//身份证的日期/年

                            int nowtime = dtime.Year;                       //今天的日期/年
                            if (nowtime - cardYear > 18 && nowtime - cardYear < 100)
                            {
                                if (cardMon <= 12)
                                {
                                    if (cardDay <= 31)
                                    {
                                        int b = Convert.ToInt32(paperNumber.ToString().Substring(16, 1));
                                        if (b % 2 == 0)
                                        {
                                            mf = false;
                                        }
                                        else
                                        {
                                            mf = true;
                                        }
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            for (int i = 0; i < 17; i++)
                            {
                                sum += int.Parse(paperNumber.Substring(i, 1)) * int.Parse(list[i].ToString());
                            }
                            ji = sum % 11;
                            string last = paperNumber.ToString().Substring(17, 1);
                            switch (ji)
                            {
                                case 0: if (last != "1")
                                    {
                                        return false;
                                    };
                                    break;
                                case 1: if (last != "0")
                                    {
                                        return false;
                                    };
                                    break;
                                case 2:
                                    if (last != "X" && last != "x")
                                    {
                                        return false;
                                    };
                                    break;
                                case 3: if (last != "9")
                                    {
                                        return false;
                                    };
                                    break;
                                case 4: if (last != "8")
                                    {
                                        return false;
                                    };
                                    break;
                                case 5: if (last != "7")
                                    {
                                        return false;
                                    };
                                    break;
                                case 6: if (last != "6")
                                    {
                                        return false;
                                    };
                                    break;
                                case 7: if (last != "5")
                                    {
                                        return false;
                                    };
                                    break;
                                case 8: if (last != "4")
                                    {
                                        return false;
                                    };
                                    break;
                                case 9: if (last != "3")
                                    {
                                        return false;
                                    };
                                    break;
                                case 10: if (last != "2")
                                    {
                                        return false;
                                    };
                                    break;
                            }
                            //							}
                        }
                    }
                }
                else
                {
                    if (paperNumber.Length >= 5 && paperNumber.Length <= 18)
                    {
                        string validSTR = "abcdefghijklmnopqrstuvwxyz-1234567890";

                        for (int i = 1; i < paperNumber.Trim().Length + 1; i++)
                        {
                            if (validSTR.IndexOf(paperNumber.Substring(i - 1, i).ToString().ToLower()) == -1)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取当前付款期数
        /// </summary>
        /// <returns></returns>
        public int GetPayQiShu()
        {
            return CommonDataBLL.getMaxqishu();
        }
        /// <summary>
        /// 将新注册的会员添加到网络图(使用存储过程js_updatenew)
        /// </summary>
        /// <param name="model">MemberInfoModel对象</param>
        /// <param name="zongPv">对应MemberOrder.TOTALPV</param>
        /// <param name="flag">是否确认(0:未确认;1:确认)</param>
        /// <param name="selectValue"></param>
        /// <param name="zongjing">应付款</param>
        /// <returns></returns>
        public bool AddProcAddNew(MemberInfoModel model, int selectValue, SqlTransaction tran)
        {
            //调用数据层方法
            double payMoney = addOrderDataDAL.HaveMoney(tran, model.Number);
            int flag = 0;
            if (selectValue == 1)
            {
                flag = 1;
            }
            else
            {
                //如果支付的钱大于应收款
                if (Convert.ToDouble(model.TotalMoney) < payMoney)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }
            }
            //使用存储过程js_updatenew
            int result = addOrderDataDAL.Upt_UpdateNew(model, flag, tran);
            return result > 0 ? (true) : (false);
        }


        /// <summary>
        ///  添加订单信息，分别判断添加和更新状态，
        ///  支付和未支付的报单
        /// </summary>
        /// <param name="memberOrderModel"></param>
        /// <param name="SumPv"></param>
        /// <param name="flag"></param>
        /// <param name="storeInfoModel"></param>
        /// <param name="mode"></param>
        /// <param name="memberDetailsModel"></param>
        /// <param name="memberInfoModel"></param>
        /// <returns></returns>
        public bool AddOrderData(MemberOrderModel memberOrderModel, MemberInfoModel memberInfoModel, IList<MemberDetailsModel> list)
        {

            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    //取得当前期数
                    int ExpectNum = memberOrderModel.OrderExpect;// CommonDataBLL.getMaxqishu();
                    //取得付款期数
                    int PayExpectNum = memberOrderModel.PayExpect;
                    //添加订单信息，同时更新店铺的库存和金额
                    new AddOrderBLL().SaveHOrder(tran, list, memberOrderModel);

                    int addInfoResult = InsertMemberInfo(memberInfoModel, tran);
                    if (addInfoResult <= 0)
                    {
                        tran.Rollback();
                        return false;
                    }
                    int result = new GroupRegisterBLL().uptIsActive(memberOrderModel.Number, tran);
                    //如果是批量修改以前期数报单的话，则将config表字段jsflag大于等于最小期数的清0
                    int resultZero = new AddOrderBLL().UpConfigToZero(PayExpectNum, BLL.CommonClass.CommonDataBLL.getMaxqishu(), tran);
                    //添加到网络图

                    //如果不是批量注册的话，执行jsAddNew存储过程
                    if (memberInfoModel.IsBatch != 1)
                    {
                        AddProcAddNew(memberInfoModel, memberOrderModel.DefrayType, tran);

                    }


                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void AddOrderDataForOrderOnly(MemberOrderModel memberOrderModel, IList<MemberDetailsModel> list)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                //修改的话跟新stock新字段
                try
                {
                    addOrderDataDAL.Del_Horder(tran, memberOrderModel.OrderId, memberOrderModel.StoreId, CommonDataBLL.OperateBh, CommonDataBLL.OperateIP);
                    //添加订单信息，同时更新店铺的库存和金额
                    new AddOrderBLL().SaveHOrder(tran, list, memberOrderModel);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    return;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        private void UpdateErr()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 插入MemberInfo表
        /// </summary>
        public int InsertMemberInfo(MemberInfoModel model, SqlTransaction tran)
        {
            AddMemberInfomDAL info = new AddMemberInfomDAL();
            //model.OperateIp = CommonClass.CommonDataBLL.OperateIP;
            //model.OperaterNum = CommonClass.CommonDataBLL.OperateBh;
            return info.InsertMemberInfo(model, tran);


        }
        /// <summary>
        /// 将图片放进数据库
        /// </summary>
        /// <param name="mode">类型</param>
        /// <param name="file">HtmlInputFile控件对象</param>
        /// <param name="dirName">路径</param>
        /// <param name="model">MemberInfoModel 对象</param>
        /// <param name="photopath2">用户输入的路径</param>
        /// <param name="photoW2">图片宽度</param>
        /// <param name="photoH2">图片高度</param>
        /// <returns></returns>
        public bool AddPic(string mode, HtmlInputFile file, string dirName, MemberInfoModel model, string photopath, int photoW, int photoH, string newFileName)
        {
            if (mode == "edit")
            {
                if (file.PostedFile.FileName.Trim() != string.Empty)
                {
                    model.PhotoPath = dirName;// +"\\" + newFileName;
                }
                else
                {
                    model.PhotoPath = "";
                }
            }
            else
            {
                if (file.PostedFile.FileName.Trim() != string.Empty)
                {
                    model.PhotoPath = dirName; //+ "\\" + newFileName;

                }
                else
                {
                    model.PhotoPath = "";

                }
            }
            return true;
        }

        /// <summary>
        /// //更新推荐或安置是他的err字段
        /// </summary>
        /// <param name="number"></param>
        /// <returns>更新成功与否</returns>
        public bool UpdTuijianOrAnzhi(string number)
        {
            return addOrderDataDAL.Check_WhenDelete(number) > 0 ? (true) : (false);
        }




        /// <summary>
        /// 进入级别
        /// </summary>
        /// <returns></returns>
        public bool IntoLevelNum(string number, int Jb, int currentExpetNum)
        {
            return addOrderDataDAL.Add_P_jb(number, Jb, currentExpetNum) > 0 ? (true) : (false);
        }

        /// <summary>
        /// 更新收货地址信息
        /// </summary>
        /// <param name="storeID">店编号</param>
        /// <returns></returns>
        public bool UpdAddress(string storeID, string orderID)
        {
            return addOrderDataDAL.GetAddressInfo(storeID, orderID) > 0 ? (true) : (false);
        }

        /// <summary>
        /// 响应页面验证按钮的推荐姓名验证
        /// </summary>
        /// <param name="DerictNumber">推荐姓名</param>
        /// <returns>返回验证结果</returns>
        public string DrictCheck(string DerictNumber)
        {
            AddOrderDataDAL addOrderDataDAL = new AddOrderDataDAL();
            string info = null;
            if (DerictNumber.Trim() == "")
            {
                return "请输入推荐人编号!";

            }
            info = addOrderDataDAL.DrictCheck(DerictNumber);
            if (info == null)
            {
                return "该用户不存在！";
            }
            info = info == "" ? ("推荐人没有姓名") : ("推荐人的姓名是" + info);

            return info;

        }

        /// <summary>
        /// 判断同名不同网情况
        /// </summary>
        /// <param name="strFirstIDNumber">输入编号</param>
        /// <param name="intFlag">intFlag==1为安置号,其它为推荐号</param>
        /// <returns>判断结果</returns>
        public string GetSameNameInDifNet(string strFirstIDNumber, int intFlag)
        {
            string info = "";
            //对重复身份证得到的最早编号是否在网中能否查到
            //intFlag==1为安置号,其它为推荐号
            if (intFlag == 1)
            {
                info = addOrderDataDAL.GetSameNameInDifNet(strFirstIDNumber, "Placement");
            }
            else
            {
                info = addOrderDataDAL.GetSameNameInDifNet(strFirstIDNumber, "Direct");
            }
            return info;
        }

        /// <summary>
        /// 响应页面验证按钮的安置姓名验证
        /// </summary>
        /// <param name="DerictNumber">安置姓名</param>
        /// <returns>返回验证结果</returns>
        public string DrictCheckPlaceMent(string placeNum)
        {
            AddOrderDataDAL addOrderDataDAL = new AddOrderDataDAL();
            string info = null;
            if (placeNum.Trim() == "")
            {
                info = "请输入安置人编号!";
                return info;
            }
            info = addOrderDataDAL.DrictCheckPlaceMent(placeNum);
            if (info == null)
            {
                return "该用户不存在！";
            }
            //判断用户名是否为空
            info = info == "" ? ("安置人没有姓名") : ("安置人的姓名是" + info);
            //返回用户名
            return info;
        }


        /// <summary>
        /// 如果公司零购注册的时候判断可用期数
        /// </summary>
        /// <param name="except">需要插入的期数</param>
        /// <param name="placement">安置编号</param>
        /// <param name="direct">推荐编号</param>
        public string CheckExcept(int except, string placement, string direct)
        {
            string errInfo = null;
            ChangeExceptDAL changeExceptDAL = new ChangeExceptDAL();
            //获得推荐人的期数
            int plaExcept = changeExceptDAL.GetRegisExce(placement);
            //获得安置人的期数
            int directExcept = changeExceptDAL.GetRegisExce(direct);
            //如果当前会员的期数小于安置人的期数
            if (plaExcept > except)
            {
                errInfo = "该会员的期数不能调至该安置人所置期数之前！";
            }
            //如果当前会员的期数小于推荐人的期数
            if (directExcept > except)
            {

                errInfo = "该会员的期数不能调至该推荐人所置期数之前！";
            }
            return errInfo;
        }


        /// <summary>
        /// 验证是否合法
        /// </summary>
        /// <param name="fatherBh"></param>
        /// <param name="sonBh"></param>
        /// <returns></returns>
        public bool isNet(string isAnZhi_TuiJian, string fatherBh, string sonBh)
        {
            bool temp = false;
            if (fatherBh.ToLower().Equals(sonBh.ToLower()))
                return true;

            string topMemberID = BLL.CommonClass.CommonDataBLL.GetTopManageID(3);

            string nettype = (isAnZhi_TuiJian == "az") ? "Placement" : "Direct";
            if (nettype == "Placement")
            {
                sonBh = MemberInfoDAL.GetPlacement(sonBh);
                while (sonBh.Length > 0)
                {
                    if (sonBh.ToLower() == fatherBh.ToLower())
                    {
                        temp = true; break;
                    }
                    if (sonBh == topMemberID || sonBh == "1111111111")
                    {
                        temp = false; break;
                    }
                    sonBh = MemberInfoDAL.GetPlacement(sonBh);
                }

            }
            else
            {
                sonBh = MemberInfoDAL.GetDirect(sonBh);
                while (sonBh.Length > 0)
                {
                    if (sonBh.ToLower() == fatherBh.ToLower())
                    {
                        temp = true; break;
                    }
                    if (sonBh == topMemberID || sonBh == "1111111111")
                    {
                        temp = false; break;
                    }
                    sonBh = MemberInfoDAL.GetDirect(sonBh);
                }

            }

            return temp;
        }

        /// <summary>
        /// 四个电话必须填一个
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        public bool MustFillOnce(string[] tel)
        {
            bool judge = false;
            for (int i = 0; i < tel.Length; i++)
            {
                if (tel[i] != "")
                {
                    judge = true;
                    break;
                }
            }
            return judge;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<int> GetProductID()
        {
            return new AddFreeOrderDAL().GetProductID();
        }

        public List<int> GetProductID(string storeid)
        {
            return new AddFreeOrderDAL().GetProductID(storeid);
        }

        /// <summary>
        /// 得到该城市邮编
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public string GetAddressCode(string xian)
        {

            return new AddFreeOrderDAL().GetAddressCode(xian);

        }

        /// <summary>
        /// 页面层获得剩下的可用来报单的金额
        /// </summary>
        /// <returns></returns>
        public string GetLeftRegisterMemberMoney(string storeId)
        {
            return new StoreDataDAL().GetLeftRegisterMemberMoney(storeId).ToString("0.00");
        }

        public decimal GetBzMoney(string name, decimal zf_huilv, MemberOrderModel mo)
        {
            return new AddFreeOrderDAL().GetBzMoney(name, zf_huilv, mo);
        }

        public int StoreIsExist(string storeId)
        {
            return new AddFreeOrderDAL().StoreIsExist(storeId);

        }


        /// <summary>
        /// 根据编号查询地址
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<string> ChoseArea(string code)
        {
            return new AddFreeOrderDAL().ChoseArea(code);
        }


        public string ChoseArea2(string country, string province, string city)
        {
            return new AddFreeOrderDAL().ChoseArea2(country, province, city);

        }


        /// <summary>
        /// 更据orderId得到该单使用的货币
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int GetCurrency(string orderId)
        {
            return (new AddMemberInfomDAL().GetCurrency(orderId));
        }
        //public ArrayList Diff(ArrayList choseProList, List<OrderProduct3> GroupItemm, List<OrderProduct3> NoGroupItem)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="notEnoughMoney"></param>
        /// <returns></returns>
        public double ChangeNotEnoughMoney(string storeId, double notEnoughMoney)
        {

            return new AddMemberInfomDAL().ChangeNotEnoughMoney(storeId, notEnoughMoney);

        }


        /// <summary>
        /// 通过ID找汇率
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Decimal GetCurrencyById(string id)
        {

            return new AddMemberInfomDAL().GetCurrencyById(id);
        }

        /// <summary>
        /// 检测是否是公司开的店
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public bool IsDefaultStore(string storeId)
        {
            return new AddMemberInfomDAL().IsDefaultStore(storeId) > 0 ? (true) : (false);
        }

        /// <summary>
        /// 检测是否是公司开的店
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public bool CheckStoreId(string storeId)
        {
            return DAL.CommonDataDAL.CheckStoreId(storeId); ;
        }

        public bool CheckStoreId1(string storeId)
        {
            return DAL.CommonDataDAL.CheckStoreId1(storeId); ;
        }

        public int TXset()
        {
            return DAL.CommonDataDAL.TXset(); ;
        }


        /// <summary>
        /// 零购注册时判断推荐人或安置人的注册期数是否小于选择的期数
        /// </summary>
        /// <param name="placement"></param>
        /// <param name="derict"></param>
        /// <param name="choseExcept"></param>
        /// <returns></returns>
        public bool DOrPExcept(string placement, string derict, int choseExcept)
        {
            return new AddMemberInfomDAL().DOrPExcept(placement, derict, choseExcept) > 0 ? (true) : (false);

        }






        /// <summary>
        /// 获取会员的国家省份城市等信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static DataTable Getcontryofmember(string number)
        {

            return AddOrderDataDAL.Getcontryofmember(number);
        }

        /// <summary>
        /// 获取到订单的一些信息
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static DataTable Getorderinfoofmember(string orderid)
        {

            return AddOrderDataDAL.Getorderinfoofmember(orderid); ;
        }


        /// <summary>
        /// 获取订单的金额信息
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public static DataTable Getmominfoofmember(string orderid)
        {

            return AddOrderDataDAL.Getmominfoofmember(orderid); ; ;

        }

        /// <summary>
        /// 查询提现信息 ds2012——tianfeng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable QueryWithdraw(string id)
        {
            return ECTransferDetailDAL.QueryWithdraw(id);
        }

        /// <summary>
        /// 会员修改提现申请
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public static bool updateWithdraw(WithdrawModel w)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    DataTable dt = ECTransferDetailDAL.QueryWithdraw(tran, w.Id.ToString());
                    double money = Convert.ToDouble(dt.Rows[0]["WithdrawMoney"]);

                    //记录提现明细
                    if (!ECTransferDetailDAL.updateWithdraw(tran, w))
                    {
                        tran.Rollback();
                        return false;
                    }
                    //更改已提现申请总额
                    if (!DAL.ECTransferDetailDAL.SetMemberShip(tran, w.Number, w.WithdrawMoney - money,decimal.Parse(w.WithdrawSXF.ToString())))
                    {
                        tran.Rollback();
                        return false;
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        public static bool updateWithdraw1(MemberInfoModel m)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    //DataTable dt = ECTransferDetailDAL.QueryWithdraw(tran, m.Number.ToString());
                    //double Djmoney = Convert.ToDouble(dt.Rows[0]["membership"]);

                    //修改会员表的冻结字段
                    if (!ECTransferDetailDAL.updatemember(tran, m))
                    {
                        tran.Rollback();
                        return false;
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        /// <summary>
        /// 提现申请账号错误——ds2012——tianfeng
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="id"></param>
        /// <param name="money"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool updateCardEorror(int id, double money, string number)
        {
            return ECTransferDetailDAL.updateCardEorror(id, money, number);
        }

        /// <summary>
        /// 开始处理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool updateKscl(int id)
        {
            return ECTransferDetailDAL.updateKscl(id);
        }

        /// <summary>
        /// 充值申请
        /// </summary>
        /// <param name="wDraw"></param>
        /// <returns></returns>
        public static bool XFMoney(WithdrawModel wDraw)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.connString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    //记录充值明细
                    if (!DAL.ECTransferDetailDAL.XF(tran, wDraw))
                    {
                        tran.Rollback();
                        return false;
                    }
                   


                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}