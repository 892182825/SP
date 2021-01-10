using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.CommonClass;

public partial class Selldetails : BLL.TranslationBase
{
    public int bzCurrency = 0;
    public string jine = "";
    public string nic = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            if (Request.QueryString["wdid"] != null)
            {
                int wdid = Convert.ToInt32(Request.QueryString["wdid"]);
                LoadinfoList(wdid);
            }
        }
    }
    public void LoadinfoList(int wdid)
    {
        string sqlss = "select  AuditingTime, actype,WithdrawTime as  trantime, w.hkjs, w.hkid, w.id,m.Number,m.MobileTele,m.Name,isnull( w.bankcard,'') as bankcard,isnull( bankname,'') as bankname ,isnull(khName,'') as name,drawcardtype, isnull(alino,'') as alino , isnull(weixno,'') as weixno ,shenhestate,WithdrawMoney,InvestJB,priceJB  from Withdraw w left join memberinfo m on w.number=m.Number where w.id=@wdid  order by id desc ";
        string wdlist = "";
        SqlParameter[] sps1 = new SqlParameter[] { 
         new SqlParameter("@wdid",wdid)
        };
        string hkjs = "";
        DataTable dtw = DAL.DBHelper.ExecuteDataTable(sqlss, sps1, CommandType.Text);
        if (dtw != null && dtw.Rows.Count > 0)
        {
            DataRow drw = dtw.Rows[0];
            int dtype = Convert.ToInt32(drw["drawcardtype"]);
            int shenhestate = Convert.ToInt32(drw["shenhestate"]);
            string name = drw["name"].ToString();
              hkjs = drw["hkjs"].ToString();
            string bankname = drw["bankname"].ToString();
            string bankcard = drw["bankcard"].ToString();
            string alino = drw["alino"].ToString();
            string weixno = drw["weixno"].ToString();
            int hkid = Convert.ToInt32(drw["hkid"]);
            double sellbb = Convert.ToDouble(drw["InvestJB"]);
            decimal price = Convert.ToDecimal(drw["priceJB"]);
            double ttprice = Convert.ToDouble(drw["WithdrawMoney"]);
            litjbbb.Text =   sellbb.ToString("0.00");
            litstate.Text = GetSHState(shenhestate);
            litjindu.Text = GetSHStatejdt(shenhestate);
            int actype = Convert.ToInt32(drw["actype"]);
            string xb = "";
            if (actype == 1) xb = "水星";
            if (actype == 2) xb = "金星";
            if (actype == 3) xb = "土星";
            if (actype == 4) xb = "木星";
            if (actype == 5) xb = "火星";

            wdlist += " <li><div class='fdiv'>挂单时间</div><div class='sdiv'>" + Convert.ToDateTime(drw["trantime"]).AddHours(8).ToString("yyyy-MM-dd HH:mm:ss") + " </div></li> <li><div class='fdiv'>卖出"+xb+"币</div><div class='sdiv'>" + sellbb.ToString("0.0000") + "</div></li>   <li><div class='fdiv'>卖出价格</div><div class='sdiv'>" + price.ToString("0.0000") + "</div></li>     <li><div class='fdiv'>卖出市值</div><div class='sdiv'> " + ttprice.ToString("0.0000") + " USDT</div></li>";

            if (shenhestate == 1)
            {
                wdlist += "<li> </li>";
                string audittime = Convert.ToDateTime(drw["AuditingTime"]) .ToString("yyyy-MM-dd HH:mm:ss");
                wdlist += "<li><div class='fdiv'>状态</div><div class='sdiv'>交易完成</div></li><li><div class='fdiv'>交易时间</div><div class='sdiv'>"+ audittime + "</div></li>";
            }


            //if (dtype == 0) wdlist += "  <li style='height:70px;' ><div class='fdiv'>收款账号</div><div class='sdiv'>" + name + "  " + bankname + " " + bankcard + "    </div> </li>";
            //if (dtype == 1) wdlist += "  <li><div class='fdiv'>收款账号</div><div class='sdiv'>" + name + "  支付宝 " + alino + "    </div> </li>";
            //if (dtype == 2) wdlist += "  <li><div class='fdiv'>收款账号</div><div class='sdiv'>" + name + "  微信 " + weixno + "    </div> </li>";
            litseller.Text = wdlist;

             


            //匹配到买方的信息
            string buystr = "";
        //    if (hkid > 0)
        //    {     SqlParameter[] sps2 = new SqlParameter[] { 
        // new SqlParameter("@hkid",hkid)
        //};
        //    string sqls = " select r.hkpzImglj,r.khname,r.RemitCardtype,r.bankname,r.bankcard,r.alino,r.weixno ,m.Number,m.Name,r.RemitMoney,r.InvestJB ,r.priceJB ,RemitCardtype from Remittances r left join memberinfo m on r.RemitNumber=m.Number where r.ID=@hkid ";
        //        DataTable dtm = DAL.DBHelper.ExecuteDataTable(sqls,sps2,CommandType.Text);
        //        if (dtm != null && dtm.Rows.Count > 0) {
        //            DataRow dr = dtm.Rows[0] ;
        //            string number = dr["number"].ToString();
        //            string buyname = dr["khname"].ToString();

        //            double jbsl = Convert.ToDouble(dr["InvestJB"]);
        //            decimal buypr = Convert.ToDecimal(dr["priceJB"]);
        //            int bdtype = Convert.ToInt32(dr["RemitCardtype"]);
        //            double RemitMoney = Convert.ToDouble(dr["RemitMoney"]);
        //            string bbankname = dr["bankname"].ToString();
        //            string bbankcard = dr["bankcard"].ToString();
        //            string balino = dr["alino"].ToString();
        //            string bweixno = dr["weixno"].ToString();
        //            string imgsrc = "../hkpzimg/" + dr["hkpzImglj"].ToString();
        //            string imgs=dr["hkpzImglj"].ToString();

        //            buystr += " <li><div class='fdiv'>买方信息</div><div class='sdiv'>" + number + "(" + buyname + ")</div></li>     ";

        //            buystr += " <li    ><div > 买方汇款信息</div></li>";

        //            if (bdtype == 0) buystr += "  <li style='height:70px;'><div class='fdiv'>汇款账户</div><div class='sdiv'>" + buyname + "  " + bbankname + " " + bbankcard + "    </div> </li>";
        //            if (bdtype == 1) buystr += "  <li><div class='fdiv'>汇款账户</div><div class='sdiv'>" + buyname + "  支付宝 " + balino + "    </div> </li>";
        //            if (bdtype == 2) buystr += "  <li><div class='fdiv'>汇款账户</div><div class='sdiv'>" + buyname + "  微信 " + bweixno + "    </div> </li>";

        //            // <li><div class='fdiv'>买入石斛积分/div><div class='sdiv'>" + jbsl.ToString("0.00") + "</div></li>  <li><div class='fdiv'>买入价格</div><div class='sdiv'>" + buypr.ToString("0.00") + "</div></li>  <li><div class='fdiv'>买入市值</div><div class='sdiv'>&yen;" + RemitMoney.ToString("0.00") + "</div></li>


        //            if (hkjs != "" || imgs!="") buystr += "<li><div class='fdiv'>买方解释</div><div class='sdiv'> " + hkjs + "</div></li><li><div class='fdiv'>汇款凭证</div><div class='sdiv'><a href='#' onclick=\"showimg('" + imgsrc + "')\">查看凭证</a></div></li>";


        //            if (shenhestate!=20)
        //            buystr += "<li><div >  <input type='button' style='width:100%;' class='btn btn-primary '  onclick='shoukuan(this," + wdid + ");' value='确认收款' /></div></li>";
                    

        //        }

        //    }
        //    else {
        //        buystr += " <li><div class='title' >未匹配 请耐心等待交易</div></li>";
        //    }
        //    litbuyerinfo.Text = buystr;

        }

    }


    public string GetSHState(int st)
    {
        string res = "";
        if (st == 0) res = "待交易";
       
        if (st ==1) res = "卖出已成";
        return res;
    }

    public string GetSHStatejdt(int st)
    {
        string res = "<div style='width:50%;'> </div>";
        if (st == 0) res = "<div style='width:50%;'> </div>";
         if (st == 1) res = "<div style='width:100%; '> </div>"; 
        return res;
    }

}