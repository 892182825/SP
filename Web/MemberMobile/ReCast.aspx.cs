using BLL.Registration_declarations;
using DAL;
using Model;
using System;
using System.Data;

public partial class ReCast : BLL.TranslationBase
{
    RegistermemberBLL registermemberBLL = new RegistermemberBLL();
    decimal num = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //  AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        //Session["Member"] = "d2918447acbc262fbcb01efce558752c";
        //Permissions.MemRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        if (!IsPostBack)
        {
            if (Request.QueryString["orderid"] != null)
            {
                string orderid = Request.QueryString["orderid"];
                string getresult = CommandAPI.getzf(orderid);
                string[] rlist = getresult.Split(',');
                //修改订单状态
                if (rlist[0] == "SUCCESS")
                {
                    string number = Session["Member"].ToString();
                    int choselv = Convert.ToInt32(Session["choselv"]);
                    double eneed=Convert.ToDouble(  Session["Eneed"] );
                    int rr = MemberOrderDAL.PayOrder(number, orderid, 0, 0, 0, eneed, choselv, "USDT账户支付成功");


                    if (rr == 1)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('购买矿机成功！');</script>", false); Bind();
                        return;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('付款成功但插入订单失败，请联系客服查询处理！');</script>", false);

                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('购买矿机失败！');</script>", false);
                }
            }
            else
                Bind();
        }
    }

    public void Bind()
    {

        string number = Session["Member"].ToString();
        decimal sum = 0;
        int lv = 0;

        DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select LevelInt from memberinfo where Number='" + number + "'");
        if (dt_one.Rows != null && dt_one.Rows.Count > 0)
        {
            lv = Convert.ToInt32(dt_one.Rows[0]["LevelInt"]);//获取账户等级
        }
        int jd = Common.GetcurJieDuan();//获取阶段状态

        DataTable dt_config = DAL.DBHelper.ExecuteDataTable("select top 1  * from config order  by id desc");
        //  ConfigModel cm = ConfigDAL.GetConfig();
        int x1p = 0; int x2p = 0; int x3p = 0; int x4p = 0; int x5p = 0; int x6p = 0; int x7p = 0;
        double x1cn = 0; double x2cn = 0; double x3cn = 0; double x4cn = 0; double x5cn = 0; double x6cn = 0; double x7cn = 0;
        if (dt_config != null && dt_config.Rows.Count > 0)
        {
            DataRow dr = dt_config.Rows[0];
            x1p = Convert.ToInt32(dr["para1"]); x2p = Convert.ToInt32(dr["para2"]);
            x3p = Convert.ToInt32(dr["para3"]); x4p = Convert.ToInt32(dr["para4"]);
            x5p = Convert.ToInt32(dr["para5"]); x6p = Convert.ToInt32(dr["para6"]);
            x7p = Convert.ToInt32(dr["para7"]);
            x1cn = Convert.ToDouble(dr["para8"]) * 100; x2cn = Convert.ToDouble(dr["para9"]) * 100;
            x3cn = Convert.ToDouble(dr["para10"]) * 100; x4cn = Convert.ToDouble(dr["para11"]) * 100;
            x5cn = Convert.ToDouble(dr["para12"]) * 100; x6cn = Convert.ToDouble(dr["para13"]) * 100;
            x7cn = Convert.ToDouble(dr["para14"]) * 100;
        }
        int lebuy = 0;
        lebuy = Convert.ToInt32(DBHelper.ExecuteScalar("select  countin-countout as lebuy  from Levelbuy  where levelint=1 "));

        string buyorup = "升级";
        if (lv < 2) buyorup = "购买";
        string html = @" <ul>";
        string h = "";
        if (lebuy > 0 && lv == 0)
        {
            html += @"  <li  onclick='showbuy(1)'  > <div class='ltimg'><img src = 'img/btb.png'  alt='X1' /></div><div class='dsc1' > <p class='p1' >Super-Planet-X1</p> <p class='p2'> " + x1p + @" USDT</p> <p class='p3'>矿机价格</p>   
</div><div class='dsc2' ><p class='p1'>&nbsp;</p><p class='p2'>" + x1cn + @"%</p><p class='p3'>日收益率</p>   </div>
                </li>";
        }
        if (lv < 7)
            h = @"<li><div class='ltimg'><img src = 'img/btb.png'  alt='X7' /></div><div class='dsc1' > <p class='p1'>Super-Planet-X7(未开放)</p> <p class='p2'> " + x7p + @" USDT</p><p class='p3'>矿机价格</p>  </div>
<div class='dsc2' ><p class='p1'>&nbsp;</p><p class='p2'>" + x7cn + @"%</p><p class='p3'>日收益率</p>   </div>
                   </li>";
        if (lv < 6)
            h = @"<li><div class='ltimg'><img src = 'img/btb.png'  alt='X6' /></div><div class='dsc1' > <p class='p1'>Super-Planet-X6(未开放)</p> <p class='p2'> " + x6p + @" USDT</p><p class='p3'>矿机价格</p> </div><div class='dsc2' ><p class='p1'>&nbsp;</p><p class='p2'>" + x6cn + @"%</p><p class='p3'>日收益率</p>   </div>
                   </li>" + h;
        if (lv < 5)
            h = @" <li onclick='showbuy(5)' ><div class='ltimg'><img src = 'img/btb.png'  alt='X5' /></div><div class='dsc1' > <p class='p1'>Super-Planet-X5</p> <p class='p2'> " + x5p + @" USDT</p><p class='p3'>矿机价格</p> </div><div class='dsc2' ><p class='p1'>&nbsp;</p><p class='p2'>" + x5cn + @"%</p><p class='p3'>日收益率</p>   </div>
                    </li>" + h;
        if (lv < 4)
            h = @" <li onclick='showbuy(4)' ><div class='ltimg'><img src = 'img/btb.png'  alt='X4' /></div><div class='dsc1' > <p class='p1'>Super-Planet-X4</p> <p  class='p2'> " + x4p + @" USDT</p><p class='p3'>矿机价格</p> </div><div class='dsc2' ><p class='p1'>&nbsp;</p><p class='p2'>" + x4cn + @"%</p><p class='p3'>日收益率</p>   </div>
                  </li>" + h;
        if (lv < 3)
            h = @"<li onclick='showbuy(3)' ><div class='ltimg'><img src = 'img/btb.png'  alt='X3' /></div><div class='dsc1' > <p class='p1'>Super-Planet-X3</p> <p class='p2'> " + x3p + @" USDT</p><p class='p3'>矿机价格</p> </div><div class='dsc2' ><p class='p1'>&nbsp;</p><p class='p2'>" + x3cn + @"%</p><p class='p3'>日收益率</p>   </div>
                     </li>" + h;
        if (lv < 2)
            h = @"<li onclick='showbuy(2)'  ><div class='ltimg'><img src = 'img/btb.png'  alt='X2' /></div><div class='dsc1' > <p class='p1'>Super-Planet-X2</p> <p class='p2'> " + x2p + @" USDT</p><p class='p3'>矿机价格</p> </div><div class='dsc2' ><p class='p1'>&nbsp;</p><p class='p2'>" + x2cn + @"%</p><p class='p3'>日收益率</p>   </div>
                 </li>" + h;
        html += h;
        html += " </ul>";
        getshow.InnerHtml = html;

    }



    /// <summary>
    /// 购买矿机
    /// </summary>
    /// <param name="chosenum"></param>
    /// <returns></returns> 
    public void GetRegSendPost()
    {
        int chosenum = Convert.ToInt32(hidetp.Value);
        if (Session["Member"] == null)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('未登录！');</script>", false);
            return;  //未登录
        }
        string number = Session["Member"].ToString();

        int re = 0;
        ///获取usdt账户 
        int lv = 0;

        //檢測是否有未支付的單子 如果有則走未支付的訂單

        double ddmm = 0;
        string orderid = "";
        //DataTable ddt = DBHelper.ExecuteDataTable("select  top 1 OrderID,TotalMoney from memberorder where  DefrayState=0 order by id  ");
        //if (ddt != null && ddt.Rows.Count > 0)
        //{
        //    DataRow dr = ddt.Rows[0];
        //    ddmm = Convert.ToDouble(dr["TotalMoney"]);
        //    orderid =  dr["orderid"].ToString();



        //    return;
        //} 

        //清除未支付订单 
        DBHelper.ExecuteNonQuery(@" insert  into   memberorderdel(id,[Number],[OrderID],[StoreID],[TotalMoney]
           ,[TotalPv],[CarryMoney],[OrderExpectNum],[PayExpectNum]
           ,[IsAgain],[OrderDate],[Error],[Remark],[DefrayState],[Consignee]
           ,[CCPCCode],[ConCity],[ConAddress]
           ,[ConZipCode],[ConTelphone]
           ,[ConMobilPhone],[ConPost]
           ,[DefrayType],[PayMoney]
           ,[PayCurrency],[StandardCurrency]
           ,[StandardCurrencyMoney],[OperateIP]
           ,[OperateNum],[RemittancesID],[ElectronicAccountID],[ordertype]
           ,[IsReceivables],[PayMentMoney],[ReceivablesDate],[EnoughProductMoney]
           ,[LackProductMoney],[IsReturn],[SendType],[SendWay],[TotalMoneyReturned]
           ,[TotalPvReturned],[OrderStatus],[TotalMoneyReturning],[TotalPvReturning]
           ,[OrderStatus_NR],[Isjjff],[trackingnum],[InvestJB]
           ,[PriceJB],[isSend],[xjpay],[xfpay],[bdpay],[ISSettle])
 select id,[Number],[OrderID],[StoreID],[TotalMoney]
           ,[TotalPv],[CarryMoney],[OrderExpectNum],[PayExpectNum]
           ,[IsAgain],[OrderDate],[Error],[Remark],[DefrayState],[Consignee]
           ,[CCPCCode],[ConCity],[ConAddress]
           ,[ConZipCode],[ConTelphone]
           ,[ConMobilPhone],[ConPost]
           ,[DefrayType],[PayMoney]
           ,[PayCurrency],[StandardCurrency]
           ,[StandardCurrencyMoney],[OperateIP]
           ,[OperateNum],[RemittancesID],[ElectronicAccountID],[ordertype]
           ,[IsReceivables],[PayMentMoney],[ReceivablesDate],[EnoughProductMoney]
           ,[LackProductMoney],[IsReturn],[SendType],[SendWay],[TotalMoneyReturned]
           ,[TotalPvReturned],[OrderStatus],[TotalMoneyReturning],[TotalPvReturning]
           ,[OrderStatus_NR],[Isjjff],[trackingnum],[InvestJB]
           ,[PriceJB],[isSend],[xjpay],[xfpay],[bdpay],[ISSettle]
 from MemberOrder where DefrayState = 0
 and number ='" + number+"'   ");
        //删除未支付的订单
          DBHelper.ExecuteNonQuery("delete  from   memberorder where  DefrayState=0 and number='"+number+"'   ");


        DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select LevelInt from memberinfo where Number='" + number + "'");
        if (dt_one.Rows != null && dt_one.Rows.Count > 0)
        {
            lv = Convert.ToInt32(dt_one.Rows[0]["LevelInt"]);//获取账户等级
        }
        double zhye = 0;
        int jd = Common.GetcurJieDuan();//获取阶段状态
        if ((lv == 1 || (lv == 0 && chosenum > 1)) && jd == 1)
            zhye = CommandAPI.GetActMoney();
        if (chosenum < 0 || chosenum > 7 || lv > chosenum)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('请选择矿机！');</script>", false);
            return;
        } //选择购买的矿机

        ConfigModel cm = ConfigDAL.GetConfig();
        double ttmoney = 0;
        double ttpv = 0;
        int ordertype = 22; // 0 第一次购买 1 补差升级 
        int isagain = 0;
        double yymoney = 0;
        //   int ispay = 0; ///支付状态
        if (lv == 0)
        {
            ordertype = 22; //抢购20u

        }
        if (lv == 1|| (lv==0 &&chosenum>1))
        {
            ordertype = 23; //购买 
        }
        if (lv == 2)
        {
            yymoney = cm.Para2;
        }
        if (lv == 3) yymoney = cm.Para3;
        if (lv == 4) yymoney = cm.Para4;
        if (lv == 5) yymoney = cm.Para5;
        if (lv == 6) yymoney = cm.Para6;
        if (lv == 7) ordertype = 25;  //复投
        if (chosenum == 1) { ttmoney = cm.Para1; ttpv = 0; }//20u 不计算业绩
        if (chosenum == 2) { ttmoney = cm.Para2 - yymoney; ttpv = cm.Para2 - yymoney; }
        if (chosenum == 3) { ttmoney = cm.Para3 - yymoney; ttpv = cm.Para3 - yymoney; }
        if (chosenum == 4) { ttmoney = cm.Para4 - yymoney; ttpv = cm.Para4 - yymoney; }
        if (chosenum == 5) { ttmoney = cm.Para5 - yymoney; ttpv = cm.Para5 - yymoney; }
        if (chosenum == 6) { ttmoney = cm.Para6 - yymoney; ttpv = cm.Para6 - yymoney; }
        if (chosenum == 7) { ttmoney = cm.Para7 - yymoney; ttpv = cm.Para7 - yymoney; }

        if (yymoney > 0) { isagain = 1; ordertype = 24; }//升级

        DataTable dtmb = DBHelper.ExecuteDataTable("select pointAin-pointAout  as  ablc,pointbin-pointbout  as  bblc,pointcin-pointcout  as  cblc,pointdin-pointdout  as  dblc,pointein-pointeout  as  eblc  from memberinfo where number='" + number + "'");
        DataTable conp = DBHelper.ExecuteDataTable("select CoinIndex ,coinnewprice  from CoinPlant  order by id ");
        double ablc = 0; double bblc = 0; double cblc = 0; double dblc = 0; double eblc = 0;
        double cap = 0; double cbp = 0; double ccp = 0; double cdp = 0; double cep = 0;
        if (dtmb != null && dtmb.Rows.Count > 0)
        {
            DataRow dr = dtmb.Rows[0];
            ablc = Convert.ToDouble(dr["ablc"]);
            bblc = Convert.ToDouble(dr["bblc"]);
            cblc = Convert.ToDouble(dr["cblc"]);
            dblc = Convert.ToDouble(dr["dblc"]);
            eblc = Convert.ToDouble(dr["eblc"]);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('账户余额不足！');</script>", false);
            return;
        }
        if (conp != null && conp.Rows.Count > 0)
        {
            foreach (DataRow item in conp.Rows)
            {
                string s = item["CoinIndex"].ToString();
                if (s == "CoinA") cap = Convert.ToDouble(item["coinnewprice"]);
                if (s == "CoinB") cbp = Convert.ToDouble(item["coinnewprice"]);
                if (s == "CoinC") ccp = Convert.ToDouble(item["coinnewprice"]);
                if (s == "CoinD") cdp = Convert.ToDouble(item["coinnewprice"]);
                if (s == "CoinE") cep = Convert.ToDouble(item["coinnewprice"]);
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('账户余额不足！');</script>", false);
            return;
        }


        double aneed = 0;
        double bneed = 0;
        double cneed = 0;
        double eneed = 0;
        if (lv > 0)
        {
            if (jd == 1) if (zhye < ttmoney)
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('账户余额不足！');</script>", false);
                    //return;
                }//余额不足
                else if (jd == 2 || jd == 3)
                {
                    aneed = ttmoney / cap;
                    if (aneed > ablc)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('账户余额不足！');</script>", false);
                        return;
                    };//余额不足
                }
                else if (jd == 4 || jd == 5)
                {
                    aneed = (ttmoney * 0.5) / cap;
                    bneed = (ttmoney * 0.5) / cbp;
                    if (aneed > ablc || bneed > bblc)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('账户余额不足！');</script>", false);
                        return;
                    }//余额不足
                }
                else if (jd == 6 || jd == 7)
                {
                    aneed = (ttmoney * 0.2) / cap;
                    bneed = (ttmoney * 0.3) / cbp;
                    cneed = (ttmoney * 0.5) / ccp;
                    if (aneed > ablc || bneed > bblc || cneed > cblc)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>  showsuc('账户余额不足！');</script>", false);
                        return;
                    }//余额不足
                }

            if (jd > 0&& chosenum>1) ///  如果是20u以上 则需要额外支付 5% 的E
            {
                eneed = (ttmoney * 0.05) / cep;
                if (eneed > eblc)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('火星币余额不足，请先去抢购兑换！');</script>", false);
                    return;
                }//余额不足

                Session["Eneed"] = eneed;
            }

        }

        RegistermemberBLL registermemberBLL = new RegistermemberBLL();
          orderid = registermemberBLL.GetOrderInfo("add", null);
        int maxexpt = ConfigDAL.GetMaxExpectNum();

        Boolean flag = new AddOrderDataDAL().AddOrderInfo(number, orderid, maxexpt, isagain, ttmoney, ttpv, ordertype);
        Session["choselv"] = chosenum;//保存当前选中级别
        if (flag)  //插入订单成功 开始支付
        {
            if (jd==1 && chosenum > 1)//说明是第一阶段的所有购买都 必须使用USDT买 
            {
                string postf = CommandAPI.GetFunction(orderid, ttmoney.ToString(),"recast.aspx", RadioButtonList1.SelectedValue);
                ClientScript.RegisterStartupScript(this.GetType(), "", postf, false);
                return;
            }

            else
            {
                //本地支付开始
                int r = MemberOrderDAL.PayOrder(number, orderid, aneed, bneed, cneed, eneed, chosenum, "使用本地币种账户支付");
                if (r == 1)
                {
                    //销毁
                    if (aneed > 0) CommandAPI.Destruction("A", aneed);
                    if (bneed > 0) CommandAPI.Destruction("B", bneed);
                    if (cneed > 0) CommandAPI.Destruction("C", cneed);
                    if (eneed > 0) CommandAPI.Destruction("E", eneed);

                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('购买成功！');</script>", false); Bind();
                    return;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('购买失败！');</script>", false);
                    return;
                }
            }


        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GetRegSendPost();

    }
}