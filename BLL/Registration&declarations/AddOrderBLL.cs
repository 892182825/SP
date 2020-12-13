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
    /// weiter by ֣����
    /// ��ӱ�����ҵ��㷽��
    /// </summary>
    public class AddOrderBLL:BLL.TranslationBase
    {

        AddOrderDataDAL addOrderDataDAL = new AddOrderDataDAL();
        StoreDataDAL storeDataDAL = new StoreDataDAL();
        //�������õ���
        public void SaveHOrder(SqlTransaction tran, IList<MemberDetailsModel> list, MemberOrderModel memberOrderModel )
        {
            RegistermemberBLL RegistermemberBLL = new RegistermemberBLL();

            //��Ӷ��˵�
            if (memberOrderModel.DefrayType == 2)
            {
                BLL.Logistics.D_AccountBLL.AddAccount(memberOrderModel.ElectronicaccountId, Convert.ToDouble(memberOrderModel.TotalMoney), D_AccountSftype.MemberType, D_AccountKmtype.Declarations, DirectionEnum.AccountReduced, "��Ա��" + memberOrderModel.Number + "���û�Ա��" + memberOrderModel.ElectronicaccountId + "�����ӻ��ұ�����������Ϊ��" + memberOrderModel.OrderId + "��", tran);
                BLL.Logistics.D_AccountBLL.AddAccount(memberOrderModel.StoreId, Convert.ToDouble(memberOrderModel.TotalMoney), D_AccountSftype.StoreType, D_AccountKmtype.AccountTransfer, DirectionEnum.AccountsIncreased, "��Ա��" + memberOrderModel.Number + "���û�Ա��" + memberOrderModel.ElectronicaccountId + "�����ӻ��ұ���ת�룬������Ϊ��" + memberOrderModel.OrderId + "��", tran);
               // �����ʻ�֧��          
                IsElecPay(tran,memberOrderModel);
            }
           
            AddOrderDataDAL addOrderDataDAL = new AddOrderDataDAL();
            //����memberOrder�� 
            addOrderDataDAL.INSERT_H_Order(memberOrderModel, tran);
          
            foreach (Model.MemberDetailsModel mDetails in list)
            {
                //���붩����ϸ
                addOrderDataDAL.insert_MemberOrderDetails(memberOrderModel, mDetails, tran);

                //δ֧������������
                if (memberOrderModel.DefrayState == 1 )
                {
                    //���¼�ȥ����
                    int result = 0;
                    result = addOrderDataDAL.updateStore(memberOrderModel.StoreId, mDetails, tran);

                    ////��Ӹ����͵ļ�¼,�ø�����ʾ
                    if (result <= 0)
                    {
                        addOrderDataDAL.updateStore2(memberOrderModel.StoreId, mDetails, tran);
                    }
                }
            }

            if (memberOrderModel.DefrayState == 1)
            {

                //�������ɶ���
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
                    //�Զ�Ϊ��������Ҫ�����뵥
                    OrderGoodsMedel storeItem = GetOrderModel(orderId, memberOrderModel, tran, totalMoney, totalPv);
                    //����Ҫ�����뵥��ϸ
                    OrderSubmit(memberOrderModel.OrderId.ToString(), list, storeItem, tran);  
                }


 
                int sd = addOrderDataDAL.updateStoreL(tran, memberOrderModel.OrderId);

                 


                //��Ӷ��˵�
                if (memberOrderModel.DefrayType == 1||memberOrderModel.DefrayType == 2)
                {
                    if (Convert.ToDouble(memberOrderModel.LackProductMoney) > 0)
                    {                        
                        BLL.Logistics.D_AccountBLL.AddAccount(memberOrderModel.StoreId, Convert.ToDouble(memberOrderModel.LackProductMoney), D_AccountSftype.StoreType, D_AccountKmtype.Declarations, DirectionEnum.AccountReduced,"��Ա��"+memberOrderModel.Number+"�������ֽ�۳��������Ϊ��"+memberOrderModel.OrderId+"��",tran);
                    }
                }
           

                //���Ӹõ��̵��ܱ����ķ��ã��ۼƣ�
                addOrderDataDAL.updateStore3(memberOrderModel.StoreId, tran, Convert.ToDouble(memberOrderModel.LackProductMoney));

            }
        }

        public OrderGoodsMedel GetOrderModel(string orderId, MemberOrderModel mOrder, SqlTransaction tran, decimal totalMoney, decimal totalPv)
        {
            //��ȡ������Ϣ
            OrderGoodsMedel item = new OrderGoodsMedel();
            item.StoreId = mOrder.StoreId;                                                   //����ID
            item.OrderGoodsID = orderId;                                            //������
            item.TotalMoney = Convert.ToDecimal(totalMoney);    //�����ܽ��
            item.TotalPv = Convert.ToDecimal(totalPv);                                       //�����ܻ���
            item.InceptAddress = mOrder.ConAddress;                                //�ջ��˵�ַ
            item.InceptPerson = mOrder.Consignee;                                   //�ջ�������
            item.PostalCode = mOrder.ConZipCode;                                     //�ջ����ʱ�
            item.Telephone = mOrder.ConTelPhone;                                     //�ջ��˵绰
            item.OrderDatetime = DateTime.Now.ToUniversalTime();                                      //����ʱ��
            item.ExpectNum = CommonDataBLL.GetMaxqishu();                           //��ȡ����
            item.TotalCommision = 0;                                                //������
            item.GoodsQuantity = 0;                                     //�������
            item.Carriage = 0;                                                      //�˷�
            item.Weight = 0;                                              //��Ҫ
            item.City.Country = mOrder.ConCity.Country;                                    //����
            item.City.Province = mOrder.ConCity.Province;                                   //ʡ��
            item.City.City = mOrder.ConCity.City;                                          //����
            item.IscheckOut = "Y";                                                  //�Ƿ�֧��
            item.OperateIP = System.Web.HttpContext.Current.Request.UserHostAddress;                               //�û�IP
            item.OrderType = 0;                                                      //��������
            item.SendWay = mOrder.SendWay;
            item.Description = "��Ա��" + mOrder.Number + "������!";

            return item;
        }

        /// <summary>
        /// �������ɶ���
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public void OrderSubmit(string orderid, IList<MemberDetailsModel> mDetailsModel, OrderGoodsMedel storeItem, SqlTransaction tran)
        {
            //���붩��
            int ExpectNum = CommonDataBLL.getMaxqishu();
            int ActiveFlag = 1;             //��ӣ�1���޸ģ�2�����߶�����0
            //���������ɹ�������ϸ��
            if (new AddOrderDataDAL().AddOrderGoods(storeItem, orderid, tran, ActiveFlag))
            {
                //���������ɹ�������ϸ��
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
        /// ���ӻ���֧��
        /// </summary>
        public void IsElecPay(SqlTransaction tran, MemberOrderModel memberOrderModel)
        {
            //֧������
            memberOrderModel.PayExpect = CommonClass.CommonDataBLL.getMaxqishu();
            //���ɻ���
            memberOrderModel.RemittancesId = Model.Other.MYDateTime.ToYYMMDDHHmmssString();
            //֧��״̬��Ϊ1
            memberOrderModel.DefrayState = 1;

            //���ӻ���֧��ʱ����¼�Ѿ�֧���Ľ��
            addOrderDataDAL.Upd_ECTPay(tran, memberOrderModel.ElectronicaccountId, Convert.ToDouble(memberOrderModel.TotalMoney));

            //���ӻ���֧�������ڵ����в����¼,�������������Ҫ���ģ�
            addOrderDataDAL.AddDataTORemittances(tran, memberOrderModel);

            //���µ��̵Ļ��
            addOrderDataDAL.Add_Remittances(tran, Convert.ToDouble(memberOrderModel.TotalMoney), memberOrderModel.StoreId);

        }

        /// <summary>
        ///  �޸��״α���ʱ��ȡ��Ϣ
        /// </summary>
        /// <param name="Number">��Ա���</param>
        /// <param name="OrderExpectNum">�������</param>
        /// <param name="storId">����</param>
        /// <param name="memberInfoModel">MemberInfoModel�����</param>
        /// <param name="memberOrderModel">MemberOrderMode�����</param>
        public void GetDataFormInfoAndOrder(string Number, int OrderExpectNum, string storId, MemberInfoModel memberInfoModel, MemberOrderModel memberOrderModel)
        {
            //�������ݲ����
            addOrderDataDAL.GetDataFormInfoAndOrder(Number, OrderExpectNum, storId, memberInfoModel, memberOrderModel);

        }

        /// <summary>
        ///  �޸��״α���ʱ��ȡ��Ϣ
        /// </summary>
        /// <param name="Number">��Ա���</param>
        /// <param name="OrderExpectNum">�������</param>
        /// <param name="storId">����</param>
        /// <param name="memberInfoModel">MemberInfoModel�����</param>
        /// <param name="memberOrderModel">MemberOrderMode�����</param>
        public void GetDataFormInfoAndOrder(string Number, int OrderExpectNum, MemberInfoModel memberInfoModel, MemberOrderModel memberOrderModel)
        {
            //�������ݲ����
            addOrderDataDAL.GetDataFormInfoAndOrder(Number, OrderExpectNum, memberInfoModel, memberOrderModel);

        }


        /// <summary>
        /// ����orderID�õ��ñ������ܻ���
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public double GetSumPv(string orderId)
        {
            return addOrderDataDAL.GetSumPv(orderId);
        }

        /// <summary>
        /// ����orderID�õ��ñ������ܽ��
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public double GetTotalMoney(string orderId)
        {
            return addOrderDataDAL.GetTotalMoney(orderId);
        }

        /// <summary>
        /// ����orderID�õ��ñ������������ͣ�isAgain��
        /// </summary>
        /// <returns></returns>
        public string GetConsumeState(string orderId)
        {
            return addOrderDataDAL.GetConsumeState(orderId);
        }

        /// <summary>
        /// �õ����̵ı�����ϸ
        /// </summary>		
        /// <param name="orderId">������</param>
        /// <returns></returns>
        public List<MemberDetailsModel> GetDetails(string orderId)
        {
            OrderDAL orderDAl = new OrderDAL();
            return orderDAl.GetDetails2(orderId);
        }
        /// <summary>
        /// ���ݵ��ŵõ��õ�id
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string GetStoreIDByNumber(string number)
        {
            return storeDataDAL.GetStoreIDByNumber(number);
        }





        /// <summary>
        /// �󶨻���(�������� -- ����)�б�
        /// </summary>
        /// <param name="list">Ҫ��������Ŀؼ�</param>		
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
        /// �󶨻���(�������� -- ����)�б�
        /// </summary>
        /// <param name="list">Ҫ��������Ŀؼ�</param>		
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
        /// ����Ƽ�������
        /// </summary>
        /// <returns></returns>
        public string GetParentName(string number)
        {
            return new AddFreeOrderDAL().GetParentName(number);
        }

        /// <summary>
        /// ��ð���������
        /// </summary>
        /// <returns></returns>
        public string GetParentName2(string number)
        {
            return new AddFreeOrderDAL().GetParentName2(number);
        }

        /// <summary>
        ///  ͨ������ID������
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
        /// ������е�ID
        /// </summary>
        /// <param name="bankName"></param>
        /// <returns></returns>
        public int SelectBankId(string bankName)
        {
            return new AddFreeOrderDAL().SelectBankId(bankName);
        }


        /// <summary>
        /// ͨ��������ҳ�õ��Ĺ������ֵõ�����ID
        /// </summary>
        /// <param name="countriName"></param>
        /// <returns></returns>
        public int GetCountryId(string countriName)
        {
            return new AddFreeOrderDAL().GetCountryId(countriName);

        }

        /// <summary>
        /// ���µ��̱������ܼƷ���
        /// </summary>
        /// <param name="model">StoreInfoModel�����</param>
        /// <returns>���ؽ��</returns>
        public int updateStore4(string storeID, SqlTransaction tran, double zongjin)
        {
            return new AddOrderDataDAL().updateStore4(storeID, tran, zongjin);
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        public double GetBottomLine()
        {

            return new AddFreeOrderDAL().GetBottomLine();
        }

         /// <summary>
        /// ��config���ֶ�jsflag����Ա����½���
        /// </summary>
        /// <param name="except">�޸ĺ�����</param>
        /// <param name="except">��ǰ����</param>
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
            return new AddFreeOrderDAL().StoreIsExist(storeId)>0?(null):("��Ǹ����ѡ��ĵ������̲����ڣ�");
        }

        public string GetHavePlace(string number, SqlTransaction tran)
        {
            int result = new AddMemberInfomDAL().GetHavePlace(number, tran);
            return result > 0 ? (this.GetTran("000982", "��Ǹ���û�Ա���а��ã��޷�ɾ��")) : (null);//��Ǹ���û�Ա���а��ã��޷�ɾ��
        }

        public string GetHaveDirect(string number, SqlTransaction tran)
        {
            int result = new AddMemberInfomDAL().GetHavePlace(number, tran);
            return result > 0 ? (this.GetTran("004147", "��Ǹ���û�Ա�����Ƽ����޷�ɾ��")) : (null);//��Ǹ���û�Ա���а��ã��޷�ɾ��
        
        }

        public int GetHaveStore(string number, SqlTransaction tran)
        {
            int result = new AddMemberInfomDAL().GetHaveStore(number, tran);
            return result;
        }

         /// <summary>
        /// ���Ҹð��ñ���Ƿ����
        /// </summary>
        /// <param name="placeNumber"></param>
        /// <returns></returns>
        public string  CheckPlaceMentIsExist(string placeNumber) 
        {
            return new AddFreeOrderDAL().CheckPlaceMentIsExist(placeNumber)>0?(null):("���ñ�Ų�����");
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
        /// ͨ����bankcode�ֶεõ�����id
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string GetCountryByBankId(string number) 
        {
            return new AddMemberInfomDAL().GetCountryByBankId(number);
        }















    

    }



}
