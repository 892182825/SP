﻿using BLL.Registration_declarations;
using DAL;
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

/// <summary>
/// CommandAPI 的摘要说明
/// </summary>
public class CommandAPI : BLL.TranslationBase
{
    public CommandAPI()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    static string app_id = "98bb076f4d2585c38e5ffaa6921779e0";
    static string signkey = "d6462b1fe40f38392d3f6d8d4a52e9e2";
    static string posturl = "https://openapi.hicoin.vip/api/";
    RegistermemberBLL registermemberBLL = new RegistermemberBLL();
    /// <summary>
    /// 账户余额
    /// </summary>
    private double GetActMoney(string  number)
    {
        double blance = 0;
        string post = posturl+"/user/info";
        Dictionary<String, String> myDictionary = new Dictionary<String, String>();
        myDictionary.Add("app_id", app_id);
        DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select MobileTele,Jackpot-Out as xj from memberinfo where Number='" + number + "'");
        if (dt_one.Rows.Count > 0)
        {
            string ipn = dt_one.Rows[0]["MobileTele"].ToString();
          
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
                myDictionary.Add("country", quhao);
                myDictionary.Add("mobile", ipn);
                myDictionary.Add("time", GetTimeStamp());
                string sign = PublicClass.GetSignContents(myDictionary, signkey);

                string json = MD5(sign);
                myDictionary.Add("sign", json);
                string rsp = PublicClass.GetFunction(post, myDictionary);
                JObject sJson = JObject.Parse(rsp);

                try
                {


                    string uid = sJson["data"]["uid"].ToString();

                    string postdz = "https://openapi.hicoin.vip/api/account/getByUidAndSymbol";
                    Dictionary<String, String> myDi = new Dictionary<String, String>();


                    myDi.Add("uid", uid);
                    myDi.Add("symbol", "USDT");
                    myDi.Add("app_id", app_id);
                    myDi.Add("time", GetTimeStamp());

                    string signj = PublicClass.GetSignContents(myDi, signkey);
                    string jsonS = MD5(signj);

                    myDi.Add("sign", jsonS);

                    string rspp = PublicClass.GetFunction(postdz, myDi);
                    JObject stJson = JObject.Parse(rspp);
                    //rmoney.Text = rspp;
                    blance =Convert.ToDouble( stJson["data"]["normal_balance"] );

                }
                catch
                {
                    return 0;
                }

            }
            else
            {
                return blance;
            }

        }
        return blance;

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
    /// <summary>
    /// 支付后查询订单状态，zfddh订单编号
    /// 返回值订单金额及订单状态用逗号隔开的
    /// 订单状态NOTPAY (待支付)，SUCCESS（已支付）， CLOSED（订单过期或关闭） UNKNOW（未知状态）
    /// </summary>

    public string getzf()
    {
        string sl = GenerateCheckCodeNum(32);
        //long mony = Convert.ToInt64(money.Text.Trim());



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
        return zt+","+skje;
        //if (zt == "SUCCESS")
        //{
          
        //}

    }
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
    /// <summary>
    /// 支付接口跳转到支付界面，ddje订单金额
    /// </summary>
    public string GetFunction(string ddje)
    {
        SqlDataReader sdr = DAL.DBHelper.ExecuteReader("select  Name,MobileTele from MemberInfo where Number='" + Session["Member"] + "'");
        string str = "";
        if (sdr.Read())
        {
            string num = GenerateCheckCodeNum(32);
            string ddh = registermemberBLL.GetOrderInfo("add", null);

            Session["ddje"] = "";
            decimal mony = Convert.ToDecimal(ddje);
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
            {"settle_currency", "USDT"},

            {"trade_type", "H5"},
            {"trade_timeout_express", "600"},
            {"return_url", "http://zd.factorde.com/MemberMobile/MemberCashXF.aspx"}


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
            string ddbh = bh;
            Session["zfddh"] = ddbh;
            if (bh == ddh)
            {
                string ddhbm = "prepay_id%3D" + studentsJson["data"]["trade_no"].ToString();
                string sjc = GetTimeStamp();
                string zfqm = PublicClass.HmacSHA256("appId=4f95ab748e204c65d0bdaa61b4e3f1d7&nonceStr=2K426TILTKCH16CQ25145I8ZNMTM67VS&package=" + ddhbm + "&signType=HMAC-SHA256&timeStamp=" + sjc + "&key=bb7c82380fd09174db6cd53369bbf961", "bb7c82380fd09174db6cd53369bbf961");
                str="<script language='javascript'>window.location.href = 'https://api.hicoin.vip/hicoinfe/payment?appId=4f95ab748e204c65d0bdaa61b4e3f1d7&nonceStr=2K426TILTKCH16CQ25145I8ZNMTM67VS&package=" + ddhbm + "&signType=HMAC-SHA256&timeStamp=" + sjc + "&paySign=" + zfqm + "';</script>";
                

            }
            else
            {
                str="<script language='javascript'>alert('支付失败请联系管理员。');</script>";
                
            }



        }
        sdr.Close();
        sdr.Dispose();
        return str;
    }

}