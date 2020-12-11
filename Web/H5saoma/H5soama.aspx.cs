using BLL.CommonClass;
using BLL.Registration_declarations;
using DAL;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class H5saoma_H5soama : System.Web.UI.Page
{
    public string erma = "";
    public string je = "";
    public string wz = "";
    protected string ddbh = "";
    RegistermemberBLL registermemberBLL = new RegistermemberBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            GetFunctions();
            
            if (Session["zfddh"] != null && Session["zfddh"] != "")
            {
                getzf();
                
            }
        }

    }

    public void getzf()
    {
        string sl = GenerateCheckCodeNum(32);
        //long mony = Convert.ToInt64(money.Text.Trim());
        if (MobileTele.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('充值失败您在钱包里暂没有账号，请先登陆注册，如果钱已经扣除请联系客服。');</script>");
            return;
        }


        string postdz = "https://oauth.factorde.com/api/pay/queryOrder";
        Dictionary<String, String> myDi = new Dictionary<String, String>();
        Dictionary<String, Object> da = new Dictionary<String, Object>
{
            {"out_trade_no", Session["zfddh"].ToString()}   
};

        String jso = JsonConvert.SerializeObject(da, Formatting.Indented);

        myDi.Add("nonce_str", sl);
        myDi.Add("biz_content", jso);
        myDi.Add("app_id", PublicClass.app_id);
        myDi.Add("access_token", Session["access_token"].ToString());
        myDi.Add("lang", "zh_CN");
        myDi.Add("version", "1.0");
        myDi.Add("charset", "utf8");
        myDi.Add("openid", Session["Member"].ToString());

        string signj = PublicClass.GetSignContent(myDi);
        string jsonS = PublicClass.HmacSHA256(signj + "&key=bb7c82380fd09174db6cd53369bbf961", "bb7c82380fd09174db6cd53369bbf961");

        myDi.Add("sign", jsonS);

        string rspp = PublicClass.doHttpPost(postdz, myDi);
        JObject stJson = JObject.Parse(rspp);
        // money.Text = rspp;
        string zt = stJson["data"]["trade_status"].ToString();
        int skje = Convert.ToInt32(stJson["data"]["settle_trans_amount"]);
        skje = Convert.ToInt32(skje * Common.GetnowPrice() * 7 / 3);
        if (zt == "SUCCESS")
        {

            Session["zfddh"] = "";
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('充值申请成功，5分钟内到账,如没到账请联系客服。');</script>");
                    return;



            
           // this.money.Value = "";
            
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('充值失败,如有疑问请联系客服。');</script>");
            return;

           
        }

    }

    //public void  Bing()
    //{
    //    erma = Request.QueryString["erma"];
    //    je = Request.QueryString["je"];
    //    string MobileTele = Request.QueryString["MobileTele"];
    //    wz = erma.Substring(0, 2);
    //    if (wz == "wx")
    //    {
    //        wz = "微信";
    //    }
    //    else
    //    {
    //        wz = "支付宝";
    //    }
    //    decimal skje = Convert.ToDecimal(je) / Common.GetnowPrice() / 7 * 4;
    //    SqlTransaction tran = null;
    //    SqlConnection conn = DAL.DBHelper.SqlCon();
    //    conn.Open();
    //    tran = conn.BeginTransaction();
    //    try
    //    {

    //       int rets = DBHelper.ExecuteNonQuery(tran, "INSERT INTO H5saoma(mobile,je,ftc,ewm,zhifu,isno) VALUES ('" + MobileTele + "'," + je + "," + skje + " ,'" + erma + "','" + wz + "',0)");

    //       if (rets > 0)
    //        {
    //            tran.Commit();
    //            conn.Close();
    //            Response.End();
    //        }
    //        else
    //        {
    //            tran.Rollback();
    //            conn.Close();
    //            Response.End();
    //        }
    //    }
    //    catch (Exception err)
    //    {

    //        conn.Close();
    //        Response.Write(err.Message);
    //        Response.End();
    //    }

    //}
    public string GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
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
    public void GetFunctions()
    {
        try
        {
            if (Request.QueryString["erma"] != null && Request.QueryString["erma"].ToString() != "")
            {
                Session["erma"] = Request.QueryString["erma"].ToString();
            }

            if (Request.QueryString.Count > 0 && Request.QueryString["code"] != null && Request.QueryString["code"].ToString() != "")
            {
                string code = Request.QueryString["code"].ToString();
                string grant_type = "authorization_code";

                string yum = "https://oauth.factorde.com/api/sns/oauth/access_token";
                Dictionary<String, String> myDictionary = new Dictionary<String, String>();
                myDictionary.Add("app_id", PublicClass.app_id);
                myDictionary.Add("secret", PublicClass.app_secret);
                myDictionary.Add("code", code);
                myDictionary.Add("grant_type", grant_type);

                //string jsonStr = PublicClass.GetSignContent(myDictionary);
                //jsonStr = HttpUtility.UrlEncode(jsonStr);//字符串进行编码，参数中有中文时一定需要这一步转换，否则接口接收的到参数会乱码
                string rsp = PublicClass.doHttpPost(yum, myDictionary);
                JObject studentsJson = JObject.Parse(rsp);
                Session["Member"] = studentsJson["data"]["openid"].ToString();
                string access_token = studentsJson["data"]["access_token"].ToString();
                Session["access_token"] = access_token;
                if (Session["Member"].ToString() == "")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('您在理财里暂没有账号或未登陆，请先登陆钱包或注册。');</script>");
                    return;


                }

                string post = "https://openapi.hicoin.vip/api/user/info";
                Dictionary<String, String> myDictionarys = new Dictionary<String, String>();
                myDictionarys.Add("app_id", "98bb076f4d2585c38e5ffaa6921779e0");
                DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select MobileTele,Jackpot-Out as xj from memberinfo where Number='" + Session["Member"].ToString() + "'");
                if (dt_one.Rows.Count > 0)
                {
                    string ipn = dt_one.Rows[0]["MobileTele"].ToString();
                    //kymoney.Text = dt_one.Rows[0]["xj"].ToString();
                    MobileTele.Text = dt_one.Rows[0]["MobileTele"].ToString();
                    if (ipn != "")
                    {
                        string quhao = "";
                        if (Encoding.Default.GetByteCount(ipn) == 11)
                        {
                            quhao = "86";
                        }
                        if (Encoding.Default.GetByteCount(ipn) == 8)
                        {
                            quhao = "852";
                        }
                        myDictionarys.Add("country", quhao);
                        myDictionarys.Add("mobile", ipn);
                        myDictionarys.Add("time", GetTimeStamp());
                        string sign = PublicClass.GetSignContents(myDictionarys, "d6462b1fe40f38392d3f6d8d4a52e9e2");

                        string json = MD5(sign);
                        myDictionarys.Add("sign", json);
                        string rsps = PublicClass.GetFunction(post, myDictionarys);
                        JObject sJson = JObject.Parse(rsps);

                        try
                        {


                            string uid = sJson["data"]["uid"].ToString();

                            string postdz = "https://openapi.hicoin.vip/api/account/getByUidAndSymbol";
                            Dictionary<String, String> myDi = new Dictionary<String, String>();


                            myDi.Add("uid", uid);
                            myDi.Add("symbol", "FTC");
                            myDi.Add("app_id", "98bb076f4d2585c38e5ffaa6921779e0");
                            myDi.Add("time", GetTimeStamp());

                            string signj = PublicClass.GetSignContents(myDi, "d6462b1fe40f38392d3f6d8d4a52e9e2");
                            string jsonS = MD5(signj);

                            myDi.Add("sign", jsonS);

                            string rspp = PublicClass.GetFunction(postdz, myDi);
                            JObject stJson = JObject.Parse(rspp);
                            //rmoney.Text = rspp;
                            decimal zhje=Convert.ToDecimal(stJson["data"]["normal_balance"].ToString()) * Common.GetnowPrice() * 7/4;
                            Label1.Text =stJson["data"]["normal_balance"].ToString()+"FTC  换算后可用￥"+ zhje.ToString("0.0000");

                        }
                        catch
                        {

                            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('请先去钱包完善信息,如果已实名请联系管理员！');", true);
                            return;
                        }

                    }
                    else
                    {
                       // rmoney.Text = "0.000";
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('请先去钱包完善信息！');", true);
                        return;
                    }

                }

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>window.location.href = 'https://oauth.factorde.com/api/connect/oauth/authorize?app_id=4f95ab748e204c65d0bdaa61b4e3f1d7&redirect_uri=http%3a%2f%2fzd.factorde.com%2fH5saoma%2fH5soama.aspx&response_type=code&scope=snsapi_base&wallet_redirect=http%3a%2f%2fzd.factorde.com%2fH5saoma%2fH5soama.aspx';</script>");
                return;
            }
        }
        catch (Exception e)
        {

            ClientScript.RegisterStartupScript(GetType(), "msg", "<script>alert('"+e.Message+"');</script>", false); return;
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
    public void GetFunction()
    {
       
            string num = GenerateCheckCodeNum(32);
            string ddh = registermemberBLL.GetOrderInfo("add", null);

            erma = Session["erma"].ToString();
            je = HiddenField1.Value;//只能通过NAME标识控件

            //if (Convert.ToDecimal(Label1.Text) < Convert.ToDecimal(je))
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('钱包余额不足。');setInterval('myInterval()',3000);</script>");
            //    return;
            
            //}

            DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select weixin from memberinfo where Number='" + Session["Member"].ToString() + "'");
            if (dt_one.Rows.Count > 0)
            {
                if (dt_one.Rows[0]["weixin"].ToString() == "" || dt_one.Rows[0]["weixin"].ToString() == null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('您暂时没有权限使用扫码支付功能。');</script>");
                    return;
                }
            }
            else
            {

                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('您暂时没有权限使用扫码支付功能。');</script>");
                return;

            }
            string Mobile = MobileTele.Text;
            wz = erma.Substring(0, 2);
            if (wz == "wx")
            {
                wz = "微信";
            }
            else
            {
                wz = "支付宝";
            }
            decimal skje = Convert.ToDecimal(je) / Common.GetnowPrice() / 7 * 4;
            string str = "";
            SqlTransaction tran = null;
            SqlConnection conn = DAL.DBHelper.SqlCon();
            conn.Open();
            tran = conn.BeginTransaction();
            try
            {
                str = "INSERT INTO H5saoma(mobile,je,ftc,ewm,zhifu,isno) VALUES ('" + Mobile + "'," + je + "," + skje.ToString("0.0000") + " ,'" + erma + "','" + wz + "',0)";
                int rets = DBHelper.ExecuteNonQuery(tran, str);

                if (rets > 0)
                {
                    tran.Commit();
                    conn.Close();
                    
                }
                else
                {
                    tran.Rollback();
                    conn.Close();
                    
                }
            }
            catch (Exception err)
            {

                conn.Close();
                Response.Write(err);
                Response.End();
            }

            Session["ddje"] = "";
            decimal mony = Convert.ToDecimal(skje.ToString("0.0000"));
            Session["ddje"] = mony;
            string yum = "https://oauth.factorde.com/api/pay/preOrder";
            Dictionary<String, String> myDictionary = new Dictionary<String, String>();
            Dictionary<String, Object> data = new Dictionary<String, Object>
{
    
    
            {"openid", Session["Member"].ToString()},
            {"body", Mobile+"扫码支付"},
            {"subject", "扫码支付"},
            {"out_trade_no", ddh},
            {"total_amount", mony*100000000},
            {"settle_currency", "FTC"},

            {"trade_type", "H5"},
            {"trade_timeout_express", "600"},
            {"return_url", "http://zd.factorde.com/H5saoma/H5soama.aspx"}
            
     
};

            String json = JsonConvert.SerializeObject(data, Formatting.Indented);

            myDictionary.Add("nonce_str", num);
            myDictionary.Add("biz_content", json);
            myDictionary.Add("app_id", PublicClass.app_id);
            myDictionary.Add("access_token", Session["access_token"].ToString());
            myDictionary.Add("lang", "zh_CN");
            myDictionary.Add("version", "1.0");
            myDictionary.Add("charset", "utf8");
            myDictionary.Add("openid", Session["Member"].ToString());

            string signjs = PublicClass.GetSignContent(myDictionary);
            string jsonStr = PublicClass.HmacSHA256(signjs + "&key=bb7c82380fd09174db6cd53369bbf961", "bb7c82380fd09174db6cd53369bbf961");

            myDictionary.Add("sign", jsonStr);


            //ClientScript.RegisterStartupScript(GetType(), "msg", "<script>alert('" + myDictionary + "');</script>", false); return;

            string rsp = PublicClass.doHttpPost(yum, myDictionary);


            JObject studentsJson = JObject.Parse(rsp);
            string bh = studentsJson["data"]["out_trade_no"].ToString();
            ddbh = bh;
            Session["zfddh"] = ddbh;
            if (bh == ddh)
            {
                string ddhbm = "prepay_id%3D" + studentsJson["data"]["trade_no"].ToString();
                string sjc = GetTimeStamp();
                string zfqm = PublicClass.HmacSHA256("appId=4f95ab748e204c65d0bdaa61b4e3f1d7&nonceStr=2K426TILTKCH16CQ25145I8ZNMTM67VS&package=" + ddhbm + "&signType=HMAC-SHA256&timeStamp=" + sjc + "&key=bb7c82380fd09174db6cd53369bbf961", "bb7c82380fd09174db6cd53369bbf961");
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>window.location.href = 'https://api.hicoin.vip/hicoinfe/payment?appId=4f95ab748e204c65d0bdaa61b4e3f1d7&nonceStr=2K426TILTKCH16CQ25145I8ZNMTM67VS&package=" + ddhbm + "&signType=HMAC-SHA256&timeStamp=" + sjc + "&paySign=" + zfqm + "';</script>");
                return;

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('支付失败请联系管理员。');</script>");
                return;
            }



        
        

    }

    protected void spaybtn_Click(object sender, EventArgs e)
    {
        GetFunction();
    }
}