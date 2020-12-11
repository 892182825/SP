using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Collections;
//Add Namespce
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BLL.CommonClass;

namespace BLL.Registration_declarations
{
    /// <summary>
    /// weiter by 郑华超
    /// 添加报单的业务层方法
    /// </summary>
    public class AddOrderBLL:BLL.TranslationBase
    {

        AddOrderDataDAL addOrderDataDAL = new AddOrderDataDAL();
        StoreDataDAL storeDataDAL = new StoreDataDAL();
        //报单调用的类
        public void SaveHOrder(SqlTransaction tran, IList<MemberDetailsModel> list, MemberOrderModel memberOrderModel )
        {
            RegistermemberBLL RegistermemberBLL = new RegistermemberBLL();

            //添加对账单
            if (memberOrderModel.DefrayType == 2)
            {
                BLL.Logistics.D_AccountBLL.AddAccount(memberOrderModel.ElectronicaccountId, Convert.ToDouble(memberOrderModel.TotalMoney), D_AccountSftype.MemberType, D_AccountKmtype.Declarations, DirectionEnum.AccountReduced, "会员【" + memberOrderModel.Number + "】用会员【" + memberOrderModel.ElectronicaccountId + "】电子货币报单，订单号为【" + memberOrderModel.OrderId + "】", tran);
                BLL.Logistics.D_AccountBLL.AddAccount(memberOrderModel.StoreId, Convert.ToDouble(memberOrderModel.TotalMoney), D_AccountSftype.StoreType, D_AccountKmtype.AccountTransfer, DirectionEnum.AccountsIncreased, "会员【" + memberOrderModel.Number + "】用会员【" + memberOrderModel.ElectronicaccountId + "】电子货币报单转入，订单号为【" + memberOrderModel.OrderId + "】", tran);
               // 电子帐户支付          
                IsElecPay(tran,memberOrderModel);
            }
           
            AddOrderDataDAL addOrderDataDAL = new AddOrderDataDAL();
            //插入memberOrder表 
            addOrderDataDAL.INSERT_H_Order(memberOrderModel, tran);
          
            foreach (Model.MemberDetailsModel mDetails in list)
            {
                //插入订单明细
                addOrderDataDAL.insert_MemberOrderDetails(memberOrderModel, mDetails, tran);

                //未支付报单不算库存
                if (memberOrderModel.DefrayState == 1 )
                {
                    //更新减去店库存
                    int result = 0;
                    result = addOrderDataDAL.updateStore(memberOrderModel.StoreId, mDetails, tran);

                    ////添加该类型的记录,用负数表示
                    if (result <= 0)
                    {
                        addOrderDataDAL.updateStore2(memberOrderModel.StoreId, mDetails, tran);
                    }
                }
            }

            if (memberOrderModel.DefrayState == 1)
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
                    //自动为店铺生成要货申请单
                    OrderGoodsMedel storeItem = GetOrderModel(orderId, memberOrderModel, tran, totalMoney, totalPv);
                    //插入要货申请单明细
                    OrderSubmit(memberOrderModel.OrderId.ToString(), list, storeItem, tran);  
                }


 
                int sd = addOrderDataDAL.updateStoreL(tran, memberOrderModel.OrderId);

                 


                //添加对账单
                if (memberOrderModel.DefrayType == 1||memberOrderModel.DefrayType == 2)
                {
                    if (Convert.ToDouble(memberOrderModel.LackProductMoney) > 0)
                    {                        
                        BLL.Logistics.D_AccountBLL.AddAccount(memberOrderModel.StoreId, Convert.ToDouble(memberOrderModel.LackProductMoney), D_AccountSftype.StoreType, D_AccountKmtype.Declarations, DirectionEnum.AccountReduced,"会员【"+memberOrderModel.Number+"】报单现金扣除额，订单号为【"+memberOrderModel.OrderId+"】",tran);
                    }
                }
           

                //增加该店铺的总报单的费用（累计）
                addOrderDataDAL.updateStore3(memberOrderModel.StoreId, tran, Convert.ToDouble(memberOrderModel.LackProductMoney));

            }
        }

        public OrderGoodsMedel GetOrderModel(string orderId, MemberOrderModel mOrder, SqlTransaction tran, decimal totalMoney, decimal totalPv)
        {
            //获取订单信息
            OrderGoodsMedel item = new OrderGoodsMedel();
            item.StoreId = mOrder.StoreId;                                                   //店铺ID
            item.OrderGoodsID = orderId;                                            //订单号
            item.TotalMoney = Convert.ToDecimal(totalMoney);    //订单总金额
            item.TotalPv = Convert.ToDecimal(totalPv);                                       //订单总积分
            item.InceptAddress = mOrder.ConAddress;                                //收货人地址
            item.InceptPerson = mOrder.Consignee;                                   //收货人姓名
            item.PostalCode = mOrder.ConZipCode;                                     //收货人邮编
            item.Telephone = mOrder.ConTelPhone;                                     //收货人电话
            item.OrderDatetime = DateTime.Now.ToUniversalTime();                                      //订单时间
            item.ExpectNum = CommonDataBLL.GetMaxqishu();                           //获取期数
            item.TotalCommision = 0;                                                //手续费
            item.GoodsQuantity = 0;                                     //货物件数
            item.Carriage = 0;                                                      //运费
            item.Weight = 0;                                              //重要
            item.City.Country = mOrder.ConCity.Country;                                    //国家
            item.City.Province = mOrder.ConCity.Province;                                   //省份
            item.City.City = mOrder.ConCity.City;                                          //城市
            item.IscheckOut = "Y";                                                  //是否支付
            item.OperateIP = System.Web.HttpContext.Current.Request.UserHostAddress;                               //用户IP
            item.OrderType = 0;                                                      //订单类型
            item.SendWay = mOrder.SendWay;
            item.Description = "会员【" + mOrder.Number + "】报单!";

            return item;
        }

        /// <summary>
        /// 报单生成订单
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public void OrderSubmit(string orderid, IList<MemberDetailsModel> mDetailsModel, OrderGoodsMedel storeItem, SqlTransaction tran)
        {
            //插入订单
            int ExpectNum = CommonDataBLL.getMaxqishu();
            int ActiveFlag = 1;             //添加：1，修改：2，在线订货：0
            //订单表插入成功插入明细表
            if (new AddOrderDataDAL().AddOrderGoods(storeItem, orderid, tran, ActiveFlag))
            {
                //订单表插入成功插入明细表
                foreach (MemberDetailsModel mDetails in mDetailsModel)
                {
                    if (mDetails.NotEnoughProduct > 0)
                    {
                        new AddOrderDataDAL().AddOrderGoodsDetail(tran, mDetails, storeItem.OrderGoodsID, storeItem.StoreId, ExpectNum);
                    }
                }
            }
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
            addOrderDataDAL.Upd_ECTPay(tran, memberOrderModel.ElectronicaccountId, Convert.ToDouble(memberOrderModel.TotalMoney));

            //电子货币支付，则在店汇款中插入记录,最后两个参数需要更改，
            addOrderDataDAL.AddDataTORemittances(tran, memberOrderModel);

            //更新店铺的汇款
            addOrderDataDAL.Add_Remittances(tran, Convert.ToDouble(memberOrderModel.TotalMoney), memberOrderModel.StoreId);

        }

        /// <summary>
        ///  修改首次报单时读取信息
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <param name="OrderExpectNum">最大期数</param>
        /// <param name="storId">店编号</param>
        /// <param name="memberInfoModel">MemberInfoModel类对象</param>
        /// <param name="memberOrderModel">MemberOrderMode类对象</param>
        public void GetDataFormInfoAndOrder(string Number, int OrderExpectNum, string storId, MemberInfoModel memberInfoModel, MemberOrderModel memberOrderModel)
        {
            //调用数据层对象
            addOrderDataDAL.GetDataFormInfoAndOrder(Number, OrderExpectNum, storId, memberInfoModel, memberOrderModel);

        }

        /// <summary>
        ///  修改首次报单时读取信息
        /// </summary>
        /// <param name="Number">会员编号</param>
        /// <param name="OrderExpectNum">最大期数</param>
        /// <param name="storId">店编号</param>
        /// <param name="memberInfoModel">MemberInfoModel类对象</param>
        /// <param name="memberOrderModel">MemberOrderMode类对象</param>
        public void GetDataFormInfoAndOrder(string Number, int OrderExpectNum, MemberInfoModel memberInfoModel, MemberOrderModel memberOrderModel)
        {
            //调用数据层对象
            addOrderDataDAL.GetDataFormInfoAndOrder(Number, OrderExpectNum, memberInfoModel, memberOrderModel);

        }


        /// <summary>
        /// 根据orderID得到该报单的总积分
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public double GetSumPv(string orderId)
        {
            return addOrderDataDAL.GetSumPv(orderId);
        }

        /// <summary>
        /// 根据orderID得到该报单的总金额
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public double GetTotalMoney(string orderId)
        {
            return addOrderDataDAL.GetTotalMoney(orderId);
        }

        /// <summary>
        /// 根据orderID得到该报单的消费类型（isAgain）
        /// </summary>
        /// <returns></returns>
        public string GetConsumeState(string orderId)
        {
            return addOrderDataDAL.GetConsumeState(orderId);
        }

        /// <summary>
        /// 得到店铺的报单明细
        /// </summary>		
        /// <param name="orderId">报单号</param>
        /// <returns></returns>
        public List<MemberDetailsModel> GetDetails(string orderId)
        {
            OrderDAL orderDAl = new OrderDAL();
            return orderDAl.GetDetails2(orderId);
        }
        /// <summary>
        /// 根据店编号得到该店id
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string GetStoreIDByNumber(string number)
        {
            return storeDataDAL.GetStoreIDByNumber(number);
        }





        /// <summary>
        /// 绑定货币(货币名称 -- 汇率)列表
        /// </summary>
        /// <param name="list">要添加期数的控件</param>		
        public static void BindCurrency_Rate(DropDownList list, string storeid)
        {
            list.Items.Clear();

            List<CurrencyModel> currencyModelList = AddFreeOrderDAL.GetRateAndName();
            foreach (CurrencyModel model in currencyModelList)
            {
                string str = CommonDataBLL.GetLanguageStr(model.ID, "Currency", "Name");
                ListItem list2 = new ListItem(str, model.ID.ToString());
                list.Items.Add(list2);
            }
            list.SelectedValue = AddFreeOrderDAL.GetBzTypeId(storeid).ToString();
        }

        /// <summary>
        /// 绑定货币(货币名称 -- 汇率)列表
        /// </summary>
        /// <param name="list">要添加期数的控件</param>		
        public static void BindCurrency_Rate(DropDownList list)
        {
            list.Items.Clear();

            List<CurrencyModel> currencyModelList = AddFreeOrderDAL.GetRateAndName();
            foreach (CurrencyModel model in currencyModelList)
            {
                ListItem list2 = new ListItem(model.Name, model.ID.ToString());
                list.Items.Add(list2);
            }
            list.SelectedValue = CurrencyDAL.GetDefaultCurrencyId().ToString();
        }


        public void BindCurrency_RateList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获得推荐人姓名
        /// </summary>
        /// <returns></returns>
        public string GetParentName(string number)
        {
            return new AddFreeOrderDAL().GetParentName(number);
        }

        /// <summary>
        /// 获得安置人姓名
        /// </summary>
        /// <returns></returns>
        public string GetParentName2(string number)
        {
            return new AddFreeOrderDAL().GetParentName2(number);
        }

        /// <summary>
        ///  通过城市ID绑定银行
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public List<MemberBankModel> GetBank(int countryCode, string language)
        {
            List<MemberBankModel> list=new AddFreeOrderDAL().GetBank(countryCode, language);
            //foreach (MemberBankModel model in list)
            //{
            //    model.BankName = CommonDataBLL.GetLanguageStr(model.BankID, "BankName");
            //}
            return list;
        }
        /// <summary>
        /// 获得银行的ID
        /// </summary>
        /// <param name="bankName"></param>
        /// <returns></returns>
        public int SelectBankId(string bankName)
        {
            return new AddFreeOrderDAL().SelectBankId(bankName);
        }


        /// <summary>
        /// 通过店铺首页得到的国家名字得到国家ID
        /// </summary>
        /// <param name="countriName"></param>
        /// <returns></returns>
        public int GetCountryId(string countriName)
        {
            return new AddFreeOrderDAL().GetCountryId(countriName);

        }

        /// <summary>
        /// 更新店铺报单的总计费用
        /// </summary>
        /// <param name="model">StoreInfoModel类对象</param>
        /// <returns>返回结果</returns>
        public int updateStore4(string storeID, SqlTransaction tran, double zongjin)
        {
            return new AddOrderDataDAL().updateStore4(storeID, tran, zongjin);
        }

        /// <summary>
        /// 获取保单底线
        /// </summary>
        /// <returns></returns>
        public double GetBottomLine()
        {

            return new AddFreeOrderDAL().GetBottomLine();
        }

         /// <summary>
        /// 将config表字段jsflag清空以便重新结算
        /// </summary>
        /// <param name="except">修改后期数</param>
        /// <param name="except">当前期数</param>
        /// <returns></returns>
        public int UpConfigToZero(int except,int nowExcept,SqlTransaction tran) 
        {
            if (except != nowExcept && except!=-1)
            {
                return new AddMemberInfomDAL().UpConfigToZero(except, tran);
            }
            else 
            {
                return 0;
            }

        }

        public string StoreIsExist(string storeId) 
        {
            return new AddFreeOrderDAL().StoreIsExist(storeId)>0?(null):("抱歉！您选择的担保店铺不存在！");
        }

        public string GetHavePlace(string number, SqlTransaction tran)
        {
            int result = new AddMemberInfomDAL().GetHavePlace(number, tran);
            return result > 0 ? (this.GetTran("000982", "抱歉！该会员下有安置，无法删除")) : (null);//抱歉！该会员下有安置，无法删除
        }

        public string GetHaveDirect(string number, SqlTransaction tran)
        {
            int result = new AddMemberInfomDAL().GetHavePlace(number, tran);
            return result > 0 ? (this.GetTran("004147", "抱歉！该会员下有推荐，无法删除")) : (null);//抱歉！该会员下有安置，无法删除
        
        }

        public int GetHaveStore(string number, SqlTransaction tran)
        {
            int result = new AddMemberInfomDAL().GetHaveStore(number, tran);
            return result;
        }

         /// <summary>
        /// 查找该安置编号是否存在
        /// </summary>
        /// <param name="placeNumber"></param>
        /// <returns></returns>
        public string  CheckPlaceMentIsExist(string placeNumber) 
        {
            return new AddFreeOrderDAL().CheckPlaceMentIsExist(placeNumber)>0?(null):("安置编号不存在");
        }


        public string DisTran(string paperType, int languageId) 
        {
            return new AddMemberInfomDAL().DisTran(paperType, languageId);
        }


        public void GetCountry(DropDownList list) 
        {
            
            List<CountryModel> list2 = new AddMemberInfomDAL().GetCountry();
            foreach (CountryModel cm in list2) 
            {
               ListItem item = new ListItem(cm.Name,cm.ID.ToString());
               list.Items.Add(item);
            }
          
        
        }

        /// <summary>
        /// 通银行bankcode字段得到国家id
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string GetCountryByBankId(string number) 
        {
            return new AddMemberInfomDAL().GetCountryByBankId(number);
        }















    

    }



}
