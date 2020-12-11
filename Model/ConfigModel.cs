using System;
/// <summary>
	/// 实体类ConfigModel
	///songjun
	///2009-8-27
	///结算参数表
	/// </summary>
namespace Model
{
    [Serializable]
    public class ConfigModel
    {
        /// <summary>
        /// 结算参数表
        /// </summary>
        public ConfigModel()
        { }
        #region Model
        private int id;
        private int expectnum;
        private string date;
        private int issuance;
        private double para1;
        private double para2;
        private double para3;
        private double para4;
        private double para5;
        private double para6;
        private double para7;
        private double para8;
        private double para9;
        private double para10;
        private double para11;
        private double para12;
        private double para13;
        private double para14;
        private double para15;
        private double para16;
        private double para17;
        private double para18;
        private double para19;
        private double para20;
        private double para21;
        private double para22;
        private double para23;
        private double para24;
        private double para25;
        private double para26;
        private double para27;
        private double para28;
        private double para29;
        private int iscs;

        private int jsflag;
        private string stardate;
        private string enddate;

        public string Enddate
        {
            get { return enddate; }
            set { enddate = value; }
        }

        public string Stardate
        {
            get { return stardate; }
            set { stardate = value; }
        }

        public int Jsflag
        {
            get { return jsflag; }
            set { jsflag = value; }
        }
        private int jsnum;

        public int Jsnum
        {
            get { return jsnum; }
            set { jsnum = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { id = value; }
            get { return id; }
        }
        /// <summary>
        /// 期数
        /// </summary>
        public int ExpectNum
        {
            set { expectnum = value; }
            get { return expectnum; }
        }
        /// <summary>
        /// 期数的字符串形式
        /// </summary>
        public string Date
        {
            set { date = value; }
            get { return date; }
        }
        /// <summary>
        /// 奖金是否发布
        /// </summary>
        public int IsSuance
        {
            set { issuance = value; }
            get { return issuance; }
        }
        /// <summary>
        /// 参数一
        /// </summary>
        public double Para1
        {
            set { para1 = value; }
            get { return para1; }
        }
        /// <summary>
        /// 参数二
        /// </summary>
        public double Para2
        {
            set { para2 = value; }
            get { return para2; }
        }
        /// <summary>
        /// 参数三
        /// </summary>
        public double Para3
        {
            set { para3 = value; }
            get { return para3; }
        }
        /// <summary>
        /// 参数四
        /// </summary>
        public double Para4
        {
            set { para4 = value; }
            get { return para4; }
        }
        /// <summary>
        /// 参数五
        /// </summary>
        public double Para5
        {
            set { para5 = value; }
            get { return para5; }
        }
        /// <summary>
        /// 参数六
        /// </summary>
        public double Para6
        {
            set { para6 = value; }
            get { return para6; }
        }
        /// <summary>
        /// 参数七
        /// </summary>
        public double Para7
        {
            set { para7 = value; }
            get { return para7; }
        }
        /// <summary>
        /// 参数八
        /// </summary>
        public double Para8
        {
            set { para8 = value; }
            get { return para8; }
        }
        /// <summary>
        /// 参数九
        /// </summary>
        public double Para9
        {
            set { para9 = value; }
            get { return para9; }
        }
        /// <summary>
        /// 参数十
        /// </summary>
        public double Para10
        {
            set { para10 = value; }
            get { return para10; }
        }
        /// <summary>
        /// 参数十一
        /// </summary>
        public double Para11
        {
            set { para11 = value; }
            get { return para11; }
        }
        /// <summary>
        /// 参数十二
        /// </summary>
        public double Para12
        {
            set { para12 = value; }
            get { return para12; }
        }
        /// <summary>
        /// 参数十三
        /// </summary>
        public double Para13
        {
            set { para13 = value; }
            get { return para13; }
        }
        /// <summary>
        /// 参数十四
        /// </summary>
        public double Para14
        {
            set { para14 = value; }
            get { return para14; }
        }
        /// <summary>
        /// 参数十五
        /// </summary>
        public double Para15
        {
            set { para15 = value; }
            get { return para15; }
        }
        /// <summary>
        /// 参数十六
        /// </summary>
        public double Para16
        {
            set { para16 = value; }
            get { return para16; }
        }
        /// <summary>
        /// 参数十七
        /// </summary>
        public double Para17
        {
            set { para17 = value; }
            get { return para17; }
        }
        /// <summary>
        /// 参数十八
        /// </summary>
        public double Para18
        {
            set { para18 = value; }
            get { return para18; }
        }
        /// <summary>
        /// 参数十九
        /// </summary>
        public double Para19
        {
            set { para19 = value; }
            get { return para19; }
        }
        /// <summary>
        /// 参数二十
        /// </summary>
        public double Para20
        {
            set { para20 = value; }
            get { return para20; }
        }
        /// <summary>
        /// 参数二十一
        /// </summary>
        public double Para21
        {
            get { return para21; }
            set { para21 = value; }
        }
        /// <summary>
        /// 参数二十二
        /// </summary>
        public double Para22
        {
            get { return para22; }
            set { para22 = value; }
        }
        /// <summary>
        /// 参数二十三
        /// </summary>
        public double Para23
        {
            get { return para23; }
            set { para23 = value; }
        }
        /// <summary>
        /// 参数二十四
        /// </summary>
        public double Para24
        {
            get { return para24; }
            set { para24 = value; }
        }
        /// <summary>
        /// 参数二十五
        /// </summary>
        public double Para25
        {
            get { return para25; }
            set { para25 = value; }
        }
        /// <summary>
        /// 参数二十六
        /// </summary>
        public double Para26
        {
            get { return para26; }
            set { para26 = value; }
        }
        /// <summary>
        /// 参数二十七
        /// </summary>
        public double Para27
        {
            get { return para27; }
            set { para27 = value; }
        }
        /// <summary>
        /// 参数二十八
        /// </summary>
        public double Para28
        {
            get { return para28; }
            set { para28 = value; }
        }
        /// <summary>
        /// 参数二十九
        /// </summary>
        public double Para29
        {
            get { return para29; }
            set { para29 = value; }
        }
        public int Iscs
        {
            get { return iscs; }
            set { iscs = value; }
        }
        public double Para30
        {
            get;
            set;
        }
        public double Para31
        {
            get;
            set;
        }
        public double Para32
        {
            get;
            set;
        }
        #endregion Model

    }
}