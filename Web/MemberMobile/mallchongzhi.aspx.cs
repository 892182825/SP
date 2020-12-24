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

public partial class MemberMobile_mallchongzhi : System.Web.UI.Page
{
    protected string ddbh = "";
    RegistermemberBLL registermemberBLL = new RegistermemberBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetFunctions();

            GetActMoney();
            if (Session["zfddh"] != null && Session["zfddh"] != "")
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
       

    }

    public string GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    } 
    public void GetFunctions()
    {
        try
        {


            if (Request.QueryString.Count > 0)
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
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('您在理财里暂没有账号，请先登陆理财注册。');setInterval('myInterval()',3000);function myInterval(){window.location.href='http://wap.xphmalls.com/WapShop'};</script>");
                    return;

                   
                }

                string post = "https://openapi.hicoin.vip/api/user/info";
                Dictionary<String, String> myDictionarys = new Dictionary<String, String>();
                myDictionarys.Add("app_id", "98bb076f4d2585c38e5ffaa6921779e0");
                DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select MobileTele,Jackpot-Out as xj from memberinfo where Number='" + Session["Member"].ToString() + "'");
                if (dt_one.Rows.Count > 0)
                {
                    string ipn = dt_one.Rows[0]["MobileTele"].ToString();
                    kymoney.Text = dt_one.Rows[0]["xj"].ToString();
                    number.Text = dt_one.Rows[0]["MobileTele"].ToString();
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
                            rmoney.Text = stJson["data"]["normal_balance"].ToString();

                        }
                        catch
                        {

                            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('请先去钱包完善信息,如果已实名请联系管理员！');", true);
                            return;
                        }

                    }
                    else
                    {
                        rmoney.Text = "0.000";
                        System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('请先去钱包完善信息！');", true);
                        return;
                    }

                }

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>window.location.href = 'https://oauth.factorde.com/api/connect/oauth/authorize?app_id=4f95ab748e204c65d0bdaa61b4e3f1d7&redirect_uri=http%3a%2f%2fsp.factorde.com%2fMemberMobile%2fmallchongzhi.aspx&response_type=code&scope=snsapi_base&wallet_redirect=http%3a%2f%2fsp.factorde.com%2fMemberMobile%2fmallchongzhi.aspx';</script>");
                return;
            }
        }
        catch (Exception)
        {

            ClientScript.RegisterStartupScript(GetType(), "msg", "<script>alert('登陆失败，请确认云端钱包是否登录.');</script>", false); return;
        }

    }

    //提交保存
    protected void Button1_Click(object sender, EventArgs e)
    {
        ///设置特定值防止重复提交
        hid_fangzhi.Value = "0";


        ///判断会员账户是否被冻结
        if (DAL.MemberInfoDAL.CheckState(Session["Member"].ToString())) { Page.ClientScript.RegisterStartupScript(GetType(), null, "<script language='javascript'>alert('您的账户被冻结，不能使用提现申请');window.location.href='First.aspx';</script>"); return; }


        #region 为空验证



        double txMoney = 0; //提现金额
        if (!double.TryParse(this.money.Text.Trim(), out txMoney))
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('金额只能是数字！');", true);
            return;
        }
        if (txMoney <= 0)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('金额不能小于等于0！');", true);
            money.Text = "200";
            return;
        }
        //if (txMoney%10!=0)
        //{
        //    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000000", "金额必须是10的倍数！") + "');", true);
        //    return;
        //}
        //if (txMoney > 3000)
        //{
        //    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000000", "金额不可以大于3000！") + "');", true);
        //    return;
        //}
        string bianhao = Session["Member"].ToString();

        //string word = Encryption.Encryption.GetEncryptionPwd(this.password.Text, bianhao);
        //int blean = ECRemitDetailBLL.ValidatePwd(bianhao, word);
        //if (blean == 1)
        //{
        //    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("001554", "电子账户密码不正确！") + "');", true);
        //    return;
        //}
        //else if (blean == 2)
        //{
        //    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("007162", "对不起，您连续5次输入密码错，请2小时候在登录！") + "');", true);
        //    return;
        //}

        #endregion

        double sxf = Convert.ToDouble(new AjaxClass().Getsxf(txMoney.ToString())) * 0.1;  //手续费
        double xjye = Convert.ToDouble(rmoney.Text); //现金账户余额【单位：美元】

        try
        {
            //string hkxz = "select count(0) from MemberCashXF where XFState not in(2,3) and number='" + Session["Member"].ToString() + "'";
            //int dthkxz = (int)DBHelper.ExecuteScalar(hkxz);
            //if (dthkxz > 0)
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script >alert('每次只能申请一笔！')</script>");
            //    return;
            //}
            //if (txMoney % Convert.ToDouble(value) != 0)
            //{
            //    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("009057", "提现金额只能为") + value + GetTran("009058", "的倍数") + "！');", true);
            //    this.money.Text = "";
            //    return;
            //}


            if (txMoney > xjye)
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('钱包内余额不足，请核对输入FTC数量！');", true);
                return;
            }

            //if ((txMoney + sxf) < Convert.ToDouble(BLL.CommonClass.CommonDataBLL.GetMinTxMoney()) / huilv)
            //{
            //    System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("008149", "金额不得低于最低提现金额！") + "');", true);
            //    return;

        }
        catch (Exception eps)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + eps + "');", true);
            return;
        }

        if (this.remark.Text.Length > 500)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('对不起，备注输入的字符太多,最多500个字符！');", true);
            return;
        };




        GetFunction();


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
        string sl = GenerateCheckCodeNum(32);
        //long mony = Convert.ToInt64(money.Text.Trim());
        if (number.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('充值失败您在商城里暂没有账号，请先登陆注册商城，如果钱已经扣除请联系商城客服。');setInterval('myInterval()',3000);function myInterval(){window.location.href='http://wap.xphmalls.com/WapShop'};</script>");
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

            SqlTransaction tran = null;
            string cinSQL = "Data Source=47.116.14.85;Initial Catalog=ysc;Persist Security Info=True;User ID=ysc;Password=nJjwcdaszxcQ0XOx";       //连接数据库所需要的字符串，即为刚才复制的连接字符串
            SqlConnection conn = new SqlConnection(cinSQL);      //创建连接对象
            conn.Open();
            tran = conn.BeginTransaction();
            try
            {
                int ret = 0;
                string sql = "update aspnet_Members set Balance=Balance+" + skje + " where UserName='" + number.Text + "'";
                int cg = DBHelper.ExecuteNonQuery(tran, sql);

                ret = DBHelper.ExecuteNonQuery(tran, "INSERT INTO Hishop_BalanceDetails(UserId,UserName,TradeDate,TradeType,Income,Balance,Remark)SELECT j.UserId,j.UserName,GETDATE(),2," + skje + " ,j.Balance,'成功从钱包充值" + skje + "' from aspnet_Members j WHERE j.UserName='" + number.Text + "'");

                if (cg > 0 && ret > 0)
                {
                    tran.Commit();
                    conn.Close();
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('充值申请成功，已经到账请查收,如没到账请联系客服。');setInterval('myInterval()',3000);function myInterval(){window.location.href='http://wap.xphmalls.com/WapShop'};</script>");
                    return;

                }
                else
                {
                    tran.Rollback();
                    conn.Close();
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('充值失败1！');</script>");
                }
            }
            catch (Exception ex)
            {

                conn.Close();
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(" + ex.Message + ");</script>");
            }


           


            Session["zfddh"] = "";
            this.money.Text = "";
            this.remark.Text = "";
        }
        else
        {

            WithdrawModel wDraw = new WithdrawModel();
            wDraw.Number = Session["Member"].ToString();
            wDraw.WithdrawMoney = Convert.ToDouble(Session["ddje"].ToString());//兑换金额
            wDraw.WithdrawSXF = Convert.ToDouble(Session["ddje"].ToString());//兑换总额
            wDraw.WithdrawTime = DateTime.Now;//时间
            wDraw.Remark = ddbh;//备注
            wDraw.ApplicationExpecdtNum = 0;//状态


            DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select MobileTele from memberinfo where Number='" + Session["Member"].ToString() + "'");
            string ipn = dt_one.Rows[0]["MobileTele"].ToString();
            wDraw.OperateIP = ipn;





            //wDraw.Khname = HyName;

            MemberInfoModel memberinfo = new MemberInfoModel();
            // memberinfo.Memberships = Convert.ToDecimal(this.HiddenField1.Value) + Convert.ToDecimal(this.money.Text.Trim());
            memberinfo.Number = Session["Member"].ToString();

            bool isSure = false;



            isSure = BLL.Registration_declarations.RegistermemberBLL.XFMoney(wDraw);
            // D_AccountBLL.AddAccountWithdraw(memberinfo.Number, Convert.ToDouble(Session["ddje"].ToString()), D_AccountSftype.MemberType, D_Sftype.BounsAccount, D_AccountKmtype.RechargeByManager, DirectionEnum.AccountsIncreased, "申请充值可用FTC,增加FTC:" + Convert.ToDouble(Session["ddje"].ToString()) + "");
            if (isSure)
            {

                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('充值申请成功，等待审核。');</script>", false);
                Session["zfddh"] = "";
                this.money.Text = "";
                this.remark.Text = "";
            }
            else
            {

                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('充值申请失败，请重新提交。');", true);

            }
        }

    }
    public void GetFunction()
    {
        SqlDataReader sdr = DAL.DBHelper.ExecuteReader("select  Name,MobileTele from MemberInfo where Number='" + Session["Member"] + "'");
        if (sdr.Read())
        {
            string num = GenerateCheckCodeNum(32);
            string ddh = registermemberBLL.GetOrderInfo("add", null);

            Session["ddje"] = "";
            decimal mony = Convert.ToDecimal(money.Text.Trim());
            Session["ddje"] = mony;
            string yum = "https://oauth.factorde.com/api/pay/preOrder";
            Dictionary<String, String> myDictionary = new Dictionary<String, String>();
            Dictionary<String, Object> data = new Dictionary<String, Object>
{
    
    
            {"openid", Session["Member"].ToString()},
            {"body", Convert.ToString(sdr["MobileTele"])+"购买配套"},
            {"subject", "购买配套"},
            {"out_trade_no", ddh},
            {"total_amount", mony*100000000},
            {"settle_currency", "FTC"},

            {"trade_type", "H5"},
            {"trade_timeout_express", "600"},
            {"return_url", "http://sp.factorde.com/MemberMobile/mallchongzhi.aspx"}
            
     
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
        sdr.Close();
        sdr.Dispose();

    }


}