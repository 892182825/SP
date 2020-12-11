using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL.CommonClass;

public partial class payerror : BLL.TranslationBase
{
   public int bzCurrency = 0;
   public double huilv;
    protected void Page_Load(object sender, EventArgs e)
    {
        bzCurrency = CommonDataBLL.GetStandard();
        huilv = Convert.ToDouble(Common.GetnowPrice());
        if (!IsPostBack)
        {
            if (Request.QueryString["ef"] != null)
            {
                string ef = EncryKey.Decrypt(Request.QueryString["ef"].ToUpper()).ToString();//"0,171101092822,1400.00"
                string typ = "";
                double money = 0;
                string bill = "";
                string[] sf = ef.Split(',');
                if (sf.Length >= 3)
                {
                    typ = sf[0].ToString();//0
                    bill = sf[1].ToString();//"171101092822"
                    money = Convert.ToDouble(sf[2]);//1400.00
                    DataTable dtt=DAL.DBHelper.ExecuteDataTable("select ordertype, totalmoney,totalpv from memberorder where orderid ='"+bill+"' ");
                    if (dtt != null && dtt.Rows.Count > 0) { 
                       int ordertype=Convert.ToInt32( dtt.Rows[0]["ordertype"]);
                       double m = Convert.ToDouble(dtt.Rows[0]["totalmoney"]);
                       double pv = Convert.ToDouble(dtt.Rows[0]["totalpv"]);
                       if (ordertype == 22 || ordertype == 12) money = m;
                       else money = pv;
                    }

                }
                else typ = ef;
                string showinfo = "";
                
                switch (typ)
                {
                    case "0":
                        showinfo = "支付成功："   +money;
         
                        break;
                    case "1":
                        showinfo = GetTran("007513", "支付失败,订单已支付或订单号不存在");
                        break;
                    case "2":
                        showinfo = GetTran("007514", "支付失败,账户可用余额不足");
                        break;
                    case "3":
                        showinfo = GetTran("007515", "支付失败,账户类型不存在");
                        break;
                    case "4":
                        showinfo = GetTran("007370", "未到账");
                        break;
                    case "5":
                        showinfo = GetTran("007516", "支付失败,服务机构周转款不足");
                        break;
                    case "6":
                        showinfo = GetTran("007517", "支付失败,服务机构订货款不足");
                        break;
                    case "100":
                       showinfo = GetTran("007518", "请您务必在 24 小时内完成向指定账户汇款")+"："+(huilv == 1 ? "$" : "￥") + (money ).ToString("f2")+","+GetTran("007519", "否则将视为您自动放弃汇款");
                             
                        break;
                    case "101":
                        showinfo = GetTran("007520", "请将") + bill + GetTran("007521", "订单款额") + (huilv == 1 ? "$" : "￥")+(money).ToString("f2")  +
                            GetTran("007522", "交给您的购货店铺，并要求其为您支付订单");
                        break;

                    default:
                        showinfo = GetTran("007523", "未知错误");
                        break;
                }
                if (typ != "0" && typ!="100" && typ != "101")
                {
                    cg.Visible = false;
                    sb.Visible = true;

                }
                if (typ == "0")
                {
                    cg.Visible = true;
                    sb.Visible = false;
                }
                if (typ == "100")
                {
                    cg.Visible = true;
                    sb.Visible = false;
                }


                lblinfo.Text = showinfo;
            }
        }
    }
}
