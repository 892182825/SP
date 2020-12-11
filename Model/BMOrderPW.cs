using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class BMOrderPW
    {

        public BMOrderPW()
        {}

        private string id;
        /// <summary>
        /// ID
        /// </summary>
        public string ID
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
        private DateTime ctime;
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime Ctime
        {
            get { return ctime; }
            set { ctime = value; }
        }
        private DateTime etime;
        /// <summary>
        /// 订单完成时间
        /// </summary>
        public DateTime Etime
        {
            get { return etime; }
            set { etime = value; }
        }
        private string number;
        /// <summary>
        /// 用户编号
        /// </summary>
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private string itemId;
        /// <summary>
        /// 标准商品编号
        /// </summary>
        public string ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }
        private string passengerName;
        /// <summary>
        /// 乘客姓名
        /// </summary>
        public string PassengerName
        {
            get { return passengerName; }
            set { passengerName = value; }
        }
        private string passengerTel;
        /// <summary>
        /// 乘客联系号码
        /// </summary>
        public string PassengerTel
        {
            get { return passengerTel; }
            set { passengerTel = value; }
        }
        private DateTime startTime;
        /// <summary>
        /// 发车时间
        /// </summary>
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        private string startStation;
        /// <summary>
        /// 出发站
        /// </summary>
        public string StartStation
        {
            get { return startStation; }
            set { startStation = value; }
        }
        private string recevieStation;
        /// <summary>
        /// 抵达站
        /// </summary>
        public string RecevieStation
        {
            get { return recevieStation; }
            set { recevieStation = value; }
        }
        private string flightCompanyName;
        /// <summary>
        /// 航空公司名称
        /// </summary>
        public string FlightCompanyName
        {
            get { return flightCompanyName; }
            set { flightCompanyName = value; }
        }
        private DateTime depTime;
        /// <summary>
        /// 起飞时间
        /// </summary>
        public DateTime DepTime
        {
            get { return depTime; }
            set { depTime = value; }
        }
        private DateTime arriTime;
        /// <summary>
        /// 降落时间
        /// </summary>
        public DateTime ArriTime
        {
            get { return arriTime; }
            set { arriTime = value; }
        }
        private string flightCompanyCode;
        /// <summary>
        /// 航空公司二字码
        /// </summary>
        public string FlightCompanyCode
        {
            get { return flightCompanyCode; }
            set { flightCompanyCode = value; }
        }
        private string flightNo;
        /// <summary>
        /// 航班号
        /// </summary>
        public string FlightNo
        {
            get { return flightNo; }
            set { flightNo = value; }
        }
        private string seatMsg;
        /// <summary>
        /// 仓位描述
        /// </summary>
        public string SeatMsg
        {
            get { return seatMsg; }
            set { seatMsg = value; }
        }
        private string seatStatus;
        /// <summary>
        /// 座位号
        /// </summary>
        public string SeatStatus
        {
            get { return seatStatus; }
            set { seatStatus = value; }
        }
        private string parPrice;
        /// <summary>
        /// 票面价
        /// </summary>
        public string ParPrice
        {
            get { return parPrice; }
            set { parPrice = value; }
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
        private decimal ePmny;
        /// <summary>
        /// 需要支付现金钱包美元的金额
        /// </summary>
        public decimal EPmny
        {
            get { return ePmny; }
            set { ePmny = value; }
        }
        private string totalPayCash;
        /// <summary>
        /// 实际支付金额
        /// </summary>
        public string TotalPayCash
        {
            get { return totalPayCash; }
            set { totalPayCash = value; }
        }
        private int  orderType;
        /// <summary>
        /// 票务类型:1.火车票2.飞机票3.汽车票
        /// </summary>
        public int  OrderType
        {
            get { return orderType; }
            set { orderType = value; }
        }
        private string title;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
    }
}
