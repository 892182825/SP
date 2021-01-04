using BLL.Registration_declarations;
using DAL;
using Model;
using System;
using System.Data;

public partial class OrderList : BLL.TranslationBase
{
    RegistermemberBLL registermemberBLL = new RegistermemberBLL();
    decimal num = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

       // Session["Member"] = "40321e6e52a6cd5bc6d3adc3d85023c3";
        //  AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        //Session["Member"] = "d2918447acbc262fbcb01efce558752c";
        //Permissions.MemRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        if (!IsPostBack)
        {
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
        //lebuy = Convert.ToInt32(DBHelper.ExecuteScalar("select  countin-countout as lebuy  from Levelbuy  where levelint=1 "));


        string acthtml = "";
        int usd = 20;
        string usdcn = "5%";
        if (lv == 1) { usd = x1p; usdcn = x1cn + "%"; }
        if (lv == 2) { usd = x2p; usdcn = x2cn + "%"; }
        if (lv == 3) { usd = x3p; usdcn = x3cn + "%"; }
        if (lv == 4) { usd = x4p; usdcn = x4cn + "%"; }
        if (lv == 5) { usd = x5p; usdcn = x5cn + "%"; }
        if (lv == 6) { usd = x6p; usdcn = x6cn + "%"; }
        if (lv == 7) { usd = x7p; usdcn = x7cn + "%"; }
        string html = @" <ul>";
        string h = "";
        if (lv > 0)
        {
            html += @"
<li    > <div class='ltimg'><img src = 'img/btb.png'  alt='X1' /></div><div class='dsc1' > <p class='p1' >Super-Planet-X" + lv + "</p> <p class='p2'>" + usd + @" USDT</p> <p class='p3'>矿机价格</p>
</div><div class='dsc2' >" + acthtml + "<p class='p1'>&nbsp;" + usdcn + @"</p><p class='p3'></p><p class='p2'> 激活</p>   </div>  </ li > ";
        }


        double tm = Convert.ToDouble(DBHelper.ExecuteScalar("select    isnull( sum( Totalpv) ,0) as tm   from  memberorder where  Number='" + number + "' and  DefrayState=1   and ordertype  in (22,23,24)   "));

        string orderid = "";
        double ttm = 0;
        string st = " ";
        if (tm > 0 && tm > usd)
        {
            hideye.Value = DBHelper.ExecuteScalar("select  pointEin-pointEout as ye from memberinfo where number ='"+number+"'").ToString();


            DataTable dt = DBHelper.ExecuteDataTable("select      orderid  , TotalMoney    from  memberorder where  Number='" + number + "' and  DefrayState=1   and  isactive=0 and ordertype  in (22,23,24) order by id desc  ");
            if (dt != null && dt.Rows.Count > 0)
            {

                double eprice = Convert.ToDouble(DBHelper.ExecuteScalar("select  coinnewprice  from CoinPlant  where CoinIndex='CoinE'  "));
                DataRow dr = dt.Rows[0];
                orderid = dr["OrderID"].ToString();
                ttm = Convert.ToDouble(dr["TotalMoney"]);
                hidetp.Value = orderid;
                hidpayE.Value = ((ttm * 0.05) / eprice).ToString("0.00");
                acthtml = "";
                st = "未激活";

                html += @"
<li    > <div class='ltimg'><img src = 'img/btb.png'  alt='X1' /></div><div class='dsc1' > <p class='p1' >Super-Planet-X" + lv + "</p> <p class='p2'>" + ttm + @" USDT</p> <p class='p3'>矿机价格</p>
</div><div class='dsc2' ><input type= 'button' onclick= 'showbuy()' class='actv'   value='激活' /><p class='p1'>&nbsp;" + usdcn + @"</p><p class='p3'></p><p class='p2'> 未激活 </p>   </div>  </ li > ";

            }
        }
        else
        {
            st = "已激活";
        }

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
       
        if (Session["Member"] == null)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('未登录！');</script>", false);
            return;  //未登录
        }
        string number = Session["Member"].ToString();

        int re = 0;
        ///获取usdt账户 
        int lv = 0;
        string orderid = hidetp.Value;
        double neede = 0;
        //檢測是否有未支付的單子 如果有則走未支付的訂單
        DataTable dt = DBHelper.ExecuteDataTable("select top 1  OrderID, TotalMoney   from  memberorder where  Number='" + number + "' and  DefrayState=1 and isactive=0 and ordertype  in (22,23,24) and  orderid='" + orderid + "' order by ID  ");

        double ttm = 0;
        string st = " ";
        if (dt != null && dt.Rows.Count > 0)
        {
            double eprice = Convert.ToDouble(DBHelper.ExecuteScalar("select  coinnewprice  from CoinPlant  where CoinIndex='CoinE'  "));
            DataRow dr = dt.Rows[0];
            orderid = dr["OrderID"].ToString();
            ttm = Convert.ToDouble(dr["TotalMoney"]);
            neede = ((ttm * 0.05) / eprice);


            ///
            int ree =  MemberOrderDAL.payOrderEcoin(number,orderid,neede,"会员激活矿机支付E币"+neede.ToString("0.0000"));

            if (ree == -1)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('余额不足！');</script>", false);
                return;
            }
            else if (ree == 1)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('激活成功！');</script>", false);
                Bind();
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('激活失败！');</script>", false);
                return;
            }






        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>showsuc('请选择需要激活矿机！');</script>", false);
            return;
        }



    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GetRegSendPost();

    }
}