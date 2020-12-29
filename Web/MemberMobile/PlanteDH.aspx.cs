using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Model;
using DAL;
using BLL;
using BLL.Registration_declarations;

public partial class PlanteDH : System.Web.UI.Page
{
    protected string ddbh = "";
    RegistermemberBLL registermemberBLL = new RegistermemberBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
       /// Session["Member"] = "d2918447acbc262fbcb01efce558752c";
        if (!IsPostBack)
        {
          

            GetActMoney();
            if (Session["zfddh"] != null )
            { 
                getzf(); 
            }
            
        }
    }



    public static string MD5(string inputText)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] fromData = System.Text.Encoding.UTF8.GetBytes(inputText);
        byte[] targetData = md5.ComputeHash(fromData);
        string byte2String = null;

        for (int i = 0; i < targetData.Length; i++)
        {
            byte2String += targetData[i].ToString("x2");
        }

        return byte2String;
    }

    /// <summary>
    /// 账户余额
    /// </summary>
    private void GetActMoney()
    {
        string number = Session["member"].ToString();
        double coineblac = Convert.ToDouble(DBHelper.ExecuteScalar("select  pointEin-pointEout as eblc from  memberinfo where number='"+number+"'"));
        double actm =   CommandAPI.GetActMoney();
        double dprice = Convert.ToDouble(DBHelper.ExecuteScalar("select  CoinnewPrice from CoinPlant where CoinIndex='CoinE' "));
        double kg = 80 - coineblac;
        if (kg < 0)
        {

            kg = 0;
                }
        lblusdt.Text =actm.ToString("0.0000");
        lblcsb.Text=coineblac.ToString("0.0000")+"(可购买"+ kg .ToString("0.00")+ "枚)"; 
        lbldj.Text = dprice.ToString("0.0000") ;

        hidactm.Value=actm.ToString("0.0000");
            hiddj.Value= dprice.ToString("0.0000") ;
        hidebc.Value = coineblac.ToString("0.0000");
    }

    public string GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    } 
 

    //提交保存
    protected void Button1_Click(object sender, EventArgs e)
    {
        string number = Session["member"].ToString();
        ///设置特定值防止重复提交
          
        double txMoney = 0; //購買金額
        if (!double.TryParse(this.txtneed.Text.Trim(), out txMoney))
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "showsuc('请正确输入！');", true);
            return;
        }
        if (txMoney <= 0)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "showsuc('请正确输入！');", true);
           return;
        }

        double coineblac = Convert.ToDouble(DBHelper.ExecuteScalar("select  pointEin-pointEout as eblc from  memberinfo where number='" + number + "'"));
        double actm =   CommandAPI.GetActMoney();
        double dprice = Convert.ToDouble(DBHelper.ExecuteScalar("select  CoinnewPrice from CoinPlant where CoinIndex='CoinE' "));
        double kg = 80 - coineblac;
        double cuususdt = txMoney * dprice; //需要使用的usdt數量

        if (kg < txMoney) //購買數量超過可購數量
        { 
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "showsuc('最多可购"+ kg + " 枚火星币！');", true); txtneed.Text = kg.ToString("0.0000");
            return;
        }

        if (actm < cuususdt) //usdt數量超多賬戶
        {
            double zdkd = actm / dprice;
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "showsuc('当前USDT账户最多可购"+ zdkd.ToString("0.0000") + "！');", true); txtneed.Text = zdkd.ToString("0.0000") ;
            return;
        }

    
        //記錄兌換表中兌換情況
     string dhorderid = "DH" + DateTime.Now.ToString("yyMMddHHmmssms");
        Session["zfddh"] = dhorderid;
        Session["getE"] = txMoney;
        Session["USDT"] = cuususdt;
        int  rr= MemberOrderDAL.createDHOrder(number, dhorderid, cuususdt, dprice, txMoney, "会员兑换Mars（火星币）");

        //請求執行支付
        if (rr == 1)
        {
            string postf = CommandAPI.GetFunction(dhorderid, cuususdt.ToString("0.0000"), "plantedh.aspx", RadioButtonList1.SelectedValue);
            ClientScript.RegisterStartupScript(this.GetType(), "", postf, false);
            return;
        }
        else {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "showsuc('兑换操作异常！');", true);
        }
 

    }

    private int rep = 0;
    private string GenerateCheckCodeNum(int codeCount)
    {
        string str = string.Empty;
        long num2 = DateTime.Now.Ticks + this.rep;
        this.rep++;
        Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> this.rep)));
        for (int i = 0; i < codeCount; i++)
        {
            int num = random.Next();
            str = str + ((char)(0x30 + ((ushort)(num % 10)))).ToString();
        }
        return str;
    }

    public void getzf()
    {
        string number = Session["member"].ToString();
        string sl = GenerateCheckCodeNum(32);
        //long mony = Convert.ToInt64(money.Text.Trim());
        if (number == "")
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "showsuc('兑换状态未知，如果钱已扣除，请联系客服处理');", true);
            return;
        }
        string ddh = Session["zfddh"].ToString();
        double getE = Convert.ToDouble(Session["getE"]);
        double usdt = Convert.ToDouble(Session["USDT"]);
        string postdz = "https://oauth.factorde.com/api/pay/queryOrder";
        Dictionary<String, String> myDi = new Dictionary<String, String>();
        Dictionary<String, Object> da = new Dictionary<String, Object>
{
            {"out_trade_no", ddh}
};

        String jso = JsonConvert.SerializeObject(da, Formatting.Indented);

        myDi.Add("nonce_str", sl);
        myDi.Add("biz_content", jso);
        myDi.Add("app_id", PublicClass.app_id);
        myDi.Add("access_token", Session["access_token"].ToString());
        myDi.Add("lang", "zh_CN");
        myDi.Add("version", "1.0");
        myDi.Add("charset", "utf8");
        myDi.Add("openid", number);

        string signj = PublicClass.GetSignContent(myDi);
        string jsonS = PublicClass.HmacSHA256(signj + "&key=cd310d4c38d3b27dd9dfc069e559f73f", "cd310d4c38d3b27dd9dfc069e559f73f");

        myDi.Add("sign", jsonS);

        string rspp = PublicClass.doHttpPost(postdz, myDi);
        JObject stJson = JObject.Parse(rspp);
        // money.Text = rspp;
        string zt = stJson["data"]["trade_status"].ToString();
        int skje = Convert.ToInt32(stJson["data"]["settle_trans_amount"]);
        double dprice = Convert.ToDouble(DBHelper.ExecuteScalar("select  CoinnewPrice from CoinPlant where CoinIndex='CoinE' "));
        skje = Convert.ToInt32(skje * dprice);
        if (zt == "SUCCESS")
        {
            //if (skje == usdt)
            //{
                int rr = MemberOrderDAL.dhOrdersuc(number, ddh, getE);
                if (rr > 0)
                {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "showsuc('兑换失败');", true);
                Session["zfddh"] = "";
                    Session["USDT"] = "";
                    Session["getE"] = "";
                    return;
                }
                else {
                    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "showsuc('兑换失败，如果钱已扣除，请联系客服处理');", true);
                }

                
                return;
            //}

        }
        else {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "showsuc('兑换失败，如果钱已扣除，请联系客服处理');", true);
           // GetActMoney();
            return;
        }


    }
   }
  
 