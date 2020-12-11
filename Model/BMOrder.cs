using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class BMOrder
    {


        public BMOrder()
        {}
        private int id;

        /// <summary>
        /// Id
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }



        private string ourterTid;
        /// <summary>
        /// 外部订单编号
        /// </summary>
        public string OurterTid
        {
            get { return ourterTid; }
            set { ourterTid = value; }
        }



        private DateTime orderTime;
        /// <summary>
        /// 订单生成时间
        /// </summary>
        public DateTime OrderTime
        {
            get { return orderTime; }
            set { orderTime = value; }
        }



        private DateTime operateTime;
        /// <summary>
        /// 订单处理时间
        /// </summary>
        public DateTime OperateTime
        {
            get { return operateTime; }
            set { operateTime = value; }
        }



        private string number;
        /// <summary>
        /// 会员编号
        /// </summary>
        public string Number
        {
            get { return number; }
            set { number = value; }
        }



        private string rechargeAccount;
        /// <summary>
        /// 充值账号
        /// </summary>
        public string RechargeAccount
        {
            get { return rechargeAccount; }
            set { rechargeAccount = value; }
        }



        private string itemName;
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }



        private string itemNum;
        /// <summary>
        /// 商品数量
        /// </summary>
        public string ItemNum
        {
            get { return itemNum; }
            set { itemNum = value; }
        }



        private decimal saleAmount;
        /// <summary>
        /// 人民币金额
        /// </summary>
        public decimal SaleAmount
        {
            get { return saleAmount; }
            set { saleAmount = value; }
        }



        private decimal epmny;
        /// <summary>
        /// 美元的金额
        /// </summary>
        public decimal EPmny
        {
            get { return epmny; }
            set { epmny = value; }
        }



        private decimal hl;
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Hl
        {
            get { return hl; }
            set { hl = value; }
        }



        private string billId;
        /// <summary>
        /// 订单编号
        /// </summary>
        public string BillId
        {
            get { return billId; }
            set { billId = value; }
        }



        private string revokeMessage;
        /// <summary>
        /// 撤销原因
        /// </summary>
        public string RevokeMessage
        {
            get { return revokeMessage; }
            set { revokeMessage = value; }
        }



        private int rechargeState;
        /// <summary>
        /// 订单充值状态 0充值中 1成功 9撤销
        /// </summary>
        public int RechargeState
        {
            get { return rechargeState; }
            set { rechargeState = value; }
        }



        private int outerType;
        /// <summary>
        /// OuterType:1.手机话费  2.水电煤 3.加油卡充值 4.游戏直充
        /// </summary>
        public int OuterType
        {
            get { return outerType; }
            set { outerType = value; }
        }


       


		




    }
}
