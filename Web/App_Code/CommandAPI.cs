using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// CommandAPI 的摘要说明
/// </summary>
public class CommandAPI
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
    /// <summary>
    /// 账户余额
    /// </summary>
   public static  double GetActMoney(string  number)
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
}