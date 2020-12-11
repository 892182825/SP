
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using DAL;
using System.Data.SqlClient;
using Model;
using System.Web.UI.WebControls;
using BLL.CommonClass;

namespace BLL.Registration_declarations
{

    public class GroupRegisterBLL : BLL.TranslationBase
    {
        MemberInfoDAL memberInfoDAL = new MemberInfoDAL();
        MemberOrderDAL memberOrderDAL = new MemberOrderDAL();
        AddOrderDataDAL addOrderDataDAL = new AddOrderDataDAL();
        BrowsememberordersDAL browsememberordersDAL = new BrowsememberordersDAL();
        AddMemberInfomDAL addMemberInfomDAL = new AddMemberInfomDAL();


        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <returns></returns>
        public PagerParmsInit QueryWhere(string storeId,int maxExcept)
        {
            PagerParmsInit model = new PagerParmsInit();
            model.PageIndex = 0;
            model.PageSize = 10;
            model.Key = "A.RegisterDate";
            model.ControlName = "gv_browOrder";
            model.PageTable = "MemberInfo as A,MemberOrder as B";
            model.PageColumn = @"A.Error as Error2, A.ExpectNum as ExpectNum,A.Placement as Placement,A.Direct as Direct,A.[name] as name2,A.RegisterDate as 'RegisterDate',B.* ,case when B.isAgain=0 then '首次消费' when B.isAgain = 3 then '会员报单' when  B.isAgain = 5 then '首次团购' else ' ' end as fuxiaoName,
                                 case when B.DefrayState = 0 then '<font color=red>未支付</font>' when  B.DefrayState = 1 then '<font color=green>已支付</font>' else ' ' end as zhifu,
                                (select top 1 A2.Name from MemberInfo as A2 Where A.Placement=A2.number) as PlaceName,
                                (select top 1 A3.Name from MemberInfo as A3 Where A.Direct=A3.number) as DerictName";
            model.SqlWhere = " B.Number=A.Number  and A.StoreID='" + storeId + "' and A.isbatch = 1 and B.ordertype = 0 and A.ExpectNum=" + maxExcept;
            return model;
        }



        public IList<string> GetBankValue(string bankCode)
        {

            return new AddMemberInfomDAL().GetBankValue(bankCode);
        }

        public List<string> GetCardType(string paperTypeCode)
        {
            return new AddMemberInfomDAL().GetCardType(paperTypeCode);

        }

        public string GetBankCode(string bankName)
        {
            return new AddMemberInfomDAL().GetBankCode(bankName);
        }

        public string GetPaperTypeCode(string paperTypeName)
        {
            return new AddMemberInfomDAL().GetPaperTypeCode(paperTypeName);
        }


        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="err"></param>
        /// <returns></returns>
        public string GerCheckErrorInfo(string err)
        {
            switch (err)
            {
                case "0":
                    err = this.GetTran("000221", "无"); 
                    break;
                case "1":
                    err = this.GetTran("000345", "无上级(安置)");
                    break;
                case "2":
                    err = this.GetTran("000348", "无上级(推荐)");
                    break;
                case "3":
                    err = this.GetTran("000350", "无此店");
                    break;
                case "4":
                    err = this.GetTran("000353", "死循环(安置)");
                    break;
                case "5":
                    err = this.GetTran("000354", "死循环(推荐)");
                    break;
                case "&nbsp;":
                    err = this.GetTran("000221", "无");
                    break;


            }
            return err;
        }
        /// <summary>
        /// 翻译grid字段中的数字
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetOrderType(string id)
        {
            string orderType = null;
            switch (id)
            {
                case "0":
                    orderType = this.GetTran("001433", "自由注册");
                    break;
                case "31":
                    orderType = this.GetTran("001458", "零购注册");
                    break;
                case "11":
                    orderType = this.GetTran("000555", "店铺注册");
                    break;
                case "21":
                    orderType = this.GetTran("007530", "会员注册");
                    break;
                case "12":
                    orderType = this.GetTran("002141", "服务机构复消");
                    break;
                case "22":
                    orderType = this.GetTran("001448", "会员自由复消");
                    break;
                case "25":
                    orderType = new BLL.TranslationBase().GetTran("008122", "会员复消提货");
                    break;

                case "13":
                    orderType = new BLL.TranslationBase().GetTran("008153", "服务机构升级单");
                    break;

                case "23":
                    orderType = new BLL.TranslationBase().GetTran("008154", "会员升级单");
                    break;

                case "33":
                    orderType = new BLL.TranslationBase().GetTran("008155", "公司升级单");
                    break;
            }
            return orderType;

        }
        /// <summary>
        /// 获取支付类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDefryType(string obj)
        {
            if (obj.ToString() == "0")
            {
                return new BLL.TranslationBase().GetTran("000221", "无");
            }
            else if (obj.ToString() == "1")
            {
                return new BLL.TranslationBase().GetTran("007880", "服务机构支付");
            }
            else if (obj.ToString() == "2")
            {
                return new BLL.TranslationBase().GetTran("007444", "电子货币支付");
            }
            else if (obj.ToString() == "3")
            {
                return new BLL.TranslationBase().GetTran("008123", "转账汇款");
            }
            else if (obj.ToString() == "4")
            {
                return new BLL.TranslationBase().GetTran("005963", "在线支付");
            }
            else if (obj.ToString() == "5")
            {
                return new BLL.TranslationBase().GetTran("000277", "周转款订货");
            }
            else if (obj.ToString() == "6")
            {
                return new BLL.TranslationBase().GetTran("007529", "订货款订货");
            }
            else if (obj.ToString() == "7")
            {
                return new BLL.TranslationBase().GetTran("008123", "转账汇款");
            }
            else if (obj.ToString() == "8")
            {
                return new BLL.TranslationBase().GetTran("005963", "在线支付");
            }
            else
            {
                return GetTran("000521", "未支付");
            }
        }

        public string GetTotalMoney(string totalMoney,double curt)
        {
            string tMoney = "0";
            if (curt != 0)
            {
                tMoney = (Convert.ToDouble(totalMoney) / curt).ToString("f2");
            }
            return tMoney;
        }

        /// <summary>
        /// 翻译grid字段中的数字
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDeftrayState(string id)
        {
            string deftrayState = null;
            switch (id)
            {
                case "0":
                    deftrayState = "<font color=red>" + GetTran("000521", "未支付") + "</font>"; 
                    break;
                case "1":
                    deftrayState = this.GetTran("000517", "已支付");
                    break;
                case "2":
                    deftrayState = "<font color=red>" + GetTran("000521", "未支付") + "</font>"; 
                    break;
                default:
                    deftrayState = this.GetTran("000221", "无");
                    break;

            }
            return deftrayState;

        }


        public string GetDeftrayState1(string id)
        {
            string deftrayState = null;
            switch (id)
            {
                case "0":
                    deftrayState = "<font color=red>" + GetTran("001009", "未审核") + "</font>";
                    break;
                case "1":
                    deftrayState = this.GetTran("001011", "已审核");
                    break;
                case "2":
                    deftrayState = "<font color=red>" + GetTran("001011", "已审核") + "</font>";
                    break;
                    //default:
                    //    deftrayState = this.GetTran("000221", "无");
                    //    break;

            }
            return deftrayState;

        }

        /// <summary>
        /// 翻译grid字段中的数字
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetConsumeType(string id)
        {
            string deftrayType = null;
            switch (id)
            {
                case "0":
                    deftrayType =this.GetTran("001416", "首次报单"); //"首次报单";
                    break;
                case "1":
                    deftrayType =this.GetTran("001174", "复消报单"); //"复消报单";
                    break;
                case "2":
                    deftrayType =this.GetTran("001422", "会员购物"); //"会员购物";
                    break;
                case "3":
                    deftrayType =this.GetTran("001433", "会员自由注册"); //"会员自由注册";
                    break;
                case "4":
                    deftrayType = this.GetTran("001458", "零购注册"); //"零购注册";
                    break;
                //case "5":
                //    deftrayType =this.GetTran("001416", "首次店铺团购"); //"首次店铺团购";
                //    break;
                //case "6":
                //    deftrayType = this.GetTran("001416", "再次店铺团购");//"再次店铺团购";
                //    break;

            }
            return deftrayType;

        }

        /// <summary>
        /// 激活isActive字段
        /// </summary>
        /// <param name="number"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public int uptIsActive(string number, SqlTransaction tran)
        {
            return new AddMemberInfomDAL().uptIsActive(number, tran);
        }


        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        public List<Bsco_PaperType> GetCard()
        {
            List<Bsco_PaperType> list= new AddMemberInfomDAL().GetCard();
            foreach (Bsco_PaperType model in list)
            {
                if (model.PaperType ==  "无")
                {
                    model.PaperType = this.GetTran("005898", "无");
                }
                else
                {
                    model.PaperType = CommonDataBLL.GetLanguageStr(model.Id, "bsco_PaperType", "papertype");
                }
            }
            return list;
        }


        /// <summary>
        /// 绑定支付方式单选按钮
        /// </summary>
        /// <param name="rbt"></param>
        /// <param name="isStore"></param>
        public void GetPaymentType(DropDownList ddl, int isStore)
        {
            MemOrderLineDAL.GetPaymentType2(ddl, isStore);
        }

        public int MinusGroupCount(int ProductId, string orderId)
        {
            return new OrderDAL().MinusGroupCount(ProductId, orderId);
        }

        /// <summary>
        /// 批量注册检测
        /// </summary>
        /// <param name="allowCount">允许线数</param>
        /// <param name="registerExcept">注册日期</param>
        /// <param name="storeId">店编号</param>
        public void CheckGroup(int allowCount, int registerExcept, string storeId) 
        {
            new AddMemberInfomDAL().CheckGroup(allowCount, registerExcept, storeId);
        }

        /// <summary>
        ///  获取身份证编码
        /// </summary>
        /// <returns></returns>
        public List<Bsco_PaperType> GetCardCode()
        {
            List<Bsco_PaperType> list = new AddMemberInfomDAL().GetCardCode();
            foreach (Bsco_PaperType model in list)
            {
                if (model.PaperType == "无")
                {
                    model.PaperType = this.GetTran("000221", "无");
                }
                else
                {
                    model.PaperType = CommonDataBLL.GetLanguageStr(model.Id, "bsco_PaperType", "papertype");
                }
            }
            return list;
        }
    }


}
