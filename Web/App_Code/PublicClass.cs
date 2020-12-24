using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Model.Other;
using System.Data;
using System.Data.SqlClient;
using BLL;
using BLL.CommonClass;
using System.Net;

/// <summary>
///PublicClass 的摘要说明
/// </summary>
public class PublicClass
{
    static string sKey = "_SE_MVS_";//"StandardEdition_MobileVersionService";

    public PublicClass()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    #region DESEnCode DES加密
    public static string DESEnCode(string pToEncrypt)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //byte[] inputByteArray = Encoding.GetEncoding("UTF-8").GetBytes(pToEncrypt);
        byte[] inputByteArray = Encoding.GetEncoding("GB2312").GetBytes(pToEncrypt);

        //建立加密对象的密钥和偏移量      
        //原文使用ASCIIEncoding.ASCII方法的GetBytes方法      
        //使得输入密码必须输入英文文本      
        des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();

        StringBuilder ret = new StringBuilder();
        foreach (byte b in ms.ToArray())
        {
            ret.AppendFormat("{0:X2}", b);
        }
        ret.ToString();
        return ret.ToString();
    }
    #endregion

    #region DESDeCode DES解密
    public static string DESDeCode(string pToDecrypt)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();

        byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
        for (int x = 0; x < pToDecrypt.Length / 2; x++)
        {
            int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
            inputByteArray[x] = (byte)i;
        }

        des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();

        StringBuilder ret = new StringBuilder();

        return System.Text.Encoding.Default.GetString(ms.ToArray());
    }
    #endregion

    #region 系统内部加密方法
    public static string GetEncryptionStr(string str)
    {
        try
        {
            char[] c = str.ToCharArray();
            str = "";
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                str += System.Web.HttpServerUtility.UrlTokenEncode(b);
            }
            c = str.ToCharArray();
            str = "";
            for (int i = (c.Length - 1); i >= 0; i--)
            {
                str += c[i].ToString();
            }
        }
        catch
        {
            str = "";
        }

        return str;
    }
    #endregion

    #region 系统内部解密方法
    public static string GetDecipherStr(string str)
    {
        try
        {
            char[] c = str.ToCharArray();
            str = "";
            for (int x = (c.Length - 1); x >= 0; x--)
            {
                str += c[x].ToString();
            }
            c = str.ToCharArray();
            str = "";
            string temp = "";
            int i = 0, j = 1;
            while (i < c.Length)
            {
                temp += Convert.ToString(c[i]);
                if (j == 4)
                {
                    byte[] b = System.Web.HttpServerUtility.UrlTokenDecode(temp);
                    str += System.Text.Encoding.Unicode.GetChars(b)[0].ToString();
                    j = 0;
                    temp = "";
                }
                i++;
                j++;
            }
            c = str.ToCharArray();
        }
        catch
        {
            str = "";
        }

        return str;
    }
    #endregion

    #region 账户明细备注翻译
    public static string getMark(string remark)
    {
        string res = "";
        if (remark.IndexOf('~') > 0)
        {
            for (int i = 0; i < remark.Split('~').Length; i++)
            {
                res += BLL.Translation.Translate(remark.Split('~')[i], "") == "" ? remark.Split('~')[i] : BLL.Translation.Translate(remark.Split('~')[i], "");
            }
        }
        else
        {
            res = remark;
        }
        return res;
    }
    #endregion

    #region 获取标准时间
    public static string GetBiaoZhunTime(string utcTime)
    {
        return DateTime.Parse(utcTime).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }
    #endregion

    #region 订单相关状态

    /// <summary>
    /// 获取会员订单的支付状态
    /// </summary>
    /// <param name="orderid"></param>
    /// <returns>true 已支付  false 未支付</returns>
    public static bool OrderDefrayState(string orderid)
    {
        SqlParameter[] parm = { 
                                new SqlParameter("@orderid", orderid)
                              };
        if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select COUNT(0) from MemberOrder where OrderID=@orderid and DefrayState=1 and PayExpectNum>-1", parm, CommandType.Text)) > 0)
            return true;
        else
            return false;
    }

    #endregion

    #region 店铺/会员级别

    /// <summary>
    /// 店铺/会员级别
    /// </summary>
    /// <param name="type"></param>
    /// <param name="level">1 店铺  2 会员</param>
    /// <returns></returns>
    public static string GetAllLevelStr(int type, int level)
    {
        object o_levelstr = null;
        
        if (type == 2)
            //o_levelstr = DAL.DBHelper.ExecuteScalar("select b." + HttpContext.Current.Session["languageCode"] + " as levelstr from BSCO_Level a,T_translation b where a.id=b.primarykey and b.tableName='BSCO_Level' and a.levelflag=0 and a.levelint=" + level);
            o_levelstr = DAL.DBHelper.ExecuteScalar("select levelstr from BSCO_Level where levelflag=0 and levelint=" + level);
        if (o_levelstr != null)
            return o_levelstr.ToString();
        else
            return new TranslationBase().GetTran("000221", "无");
    }

    #endregion


    #region 短信发送

    /// <summary>
    /// 短信发送
    /// </summary>
    /// <param name="type">1 注册短信  2 物流信息  3 改会员密码  4 改店铺密码</param>
    /// <param name="billid">订单编号/会员编号/店铺编号</param>
    public static void SendMsg(int type, string billid)
    {
        string recipientNo = "";
        string mobile = "";
        string msg = "";
        string info = "";

        if (type == 1)
        {
            DataTable dt = DAL.DBHelper.ExecuteDataTable("select mi.Number,mi.Name,mi.MobileTele from MemberInfo mi join MemberOrder mo on mi.Number=mo.Number where mo.OrderID='" + billid + "' and mo.IsAgain=0");
            if (dt != null && dt.Rows.Count > 0)
            {
                recipientNo = dt.Rows[0]["Number"].ToString();
                mobile = dt.Rows[0]["MobileTele"].ToString();
                //msg = dt.Rows[0]["Name"].ToString() + "欢迎您加入上海莱芙蔻大家庭，您的编号为:" + recipientNo + "，一级密码为身份证前六位，二级为身份证后六位。客服电话：4006983877";
                DataTable dt_pwd_info = DAL.DBHelper.ExecuteDataTable("select loginpass,paypass from reg_Initi_pwd where number='" + recipientNo + "'");
                if (dt_pwd_info != null && dt_pwd_info.Rows.Count > 0)
                {
                    msg = dt.Rows[0]["Name"].ToString() + "老师您好！欢迎您加入上海莱芙蔻大家庭，您的编号为:" + recipientNo + "，一级密码为" + dt_pwd_info.Rows[0]["loginpass"].ToString() + "，二级密码为" + dt_pwd_info.Rows[0]["paypass"].ToString() + "。客服电话：4006983877 ";
                }
                info = "";
            }
        }
        else if (type == 2)
        {
            DataTable dt = DAL.DBHelper.ExecuteDataTable("select si.StoreID,si.StoreName,so.Telephone,so.ConveyanceCompany,so.kuaididh from StoreInfo si join StoreOrder so on si.StoreID=so.StoreID where so.StoreOrderID='" + billid + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                recipientNo = dt.Rows[0]["StoreID"].ToString();
                mobile = dt.Rows[0]["Telephone"].ToString();
                msg = dt.Rows[0]["StoreName"].ToString() + "您好。我公司于" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "通过" + dt.Rows[0]["ConveyanceCompany"].ToString() + "给您发了一单货。单号为：" + dt.Rows[0]["kuaididh"].ToString() + "请注意查收，货物如有异常或破损，请在7天之内与我公司客服联系。电话：400-6983-877。莱芙蔻祝您生活愉快！";
                info = "";
            }
        }
        else if (type == 3)
        {
            DataTable dt = DAL.DBHelper.ExecuteDataTable("select Number,Name,MobileTele from MemberInfo where Number='" + billid + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                recipientNo = dt.Rows[0]["Number"].ToString();
                mobile = dt.Rows[0]["MobileTele"].ToString();
                msg = dt.Rows[0]["Name"].ToString() + "您好！你的" + recipientNo + "密码已经重置为原始密码，请您及时修改密码，祝您生活愉快！";
                info = "";
            }
        }
        else if (type == 4)
        {
            DataTable dt = DAL.DBHelper.ExecuteDataTable("select StoreID,StoreName,MobileTele from StoreInfo where StoreID='" + billid + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                recipientNo = dt.Rows[0]["StoreID"].ToString();
                mobile = dt.Rows[0]["MobileTele"].ToString();
                msg = dt.Rows[0]["StoreName"].ToString() + "您好！你的" + recipientNo + "密码已经重置为原始密码，请您及时修改密码，祝您生活愉快！";
                info = "";
            }
        }

        #region 短信发送

        bool flag = false;

        using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
        {
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();

            try
            {
                if (msg.Trim().Length > 0)
                {
                    if (type == 1)
                        flag = BLL.MobileSMS.SendMsgTo(tran, recipientNo, "", mobile, msg, out info, Model.SMSCategory.sms_Register);
                    else if (type == 2)
                        flag = BLL.MobileSMS.SendMsgTo(tran, recipientNo, "", mobile, msg, out info, Model.SMSCategory.sms_Delivery);
                    else if (type == 3)
                        flag = BLL.MobileSMS.SendMsgTo(tran, recipientNo, "", mobile, msg, out info, Model.SMSCategory.sms_menberPassRest);
                    else if (type == 4)
                        flag = BLL.MobileSMS.SendMsgTo(tran, recipientNo, "", mobile, msg, out info, Model.SMSCategory.sms_storePassRest);
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        #endregion

    }

    /// <summary>
    /// 短信发送
    /// </summary>
    /// <param name="type">1 注册短信  2 物流信息  3 改会员密码  4 改店铺密码</param>
    /// <param name="billid">订单编号/会员编号/店铺编号</param>
    /// <param name="msg">短信内容</param>
    public static void SendMsg(int type, string billid, string msg)
    {
        string recipientNo = "";
        string recipientName = "";
        string mobile = "";
        string info = "";

        if (type == 1)
        {
            DataTable dt = DAL.DBHelper.ExecuteDataTable("select mi.Number,mi.Name,mi.MobileTele from MemberInfo mi join MemberOrder mo on mi.Number=mo.Number where mo.OrderID='" + billid + "' and mo.IsAgain=0");
            if (dt != null && dt.Rows.Count > 0)
            {
                recipientNo = dt.Rows[0]["Number"].ToString();
                recipientName = dt.Rows[0]["Name"].ToString();
                mobile = dt.Rows[0]["MobileTele"].ToString();
                msg = "您的编号为：" + dt.Rows[0]["Number"].ToString();
            }
        }
        else if (type == 2)
        {
            DataTable dt = DAL.DBHelper.ExecuteDataTable("select si.StoreID,si.StoreName,so.Telephone,so.ConveyanceCompany,so.kuaididh from StoreInfo si join StoreOrder so on si.StoreID=so.StoreID where so.StoreOrderID='" + billid + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                recipientNo = dt.Rows[0]["StoreID"].ToString();
                mobile = dt.Rows[0]["Telephone"].ToString();
            }
        }
        else if (type == 3)
        {
            DataTable dt = DAL.DBHelper.ExecuteDataTable("select Number,Name,MobileTele from MemberInfo where Number='" + billid + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                recipientNo = dt.Rows[0]["Number"].ToString();
                mobile = dt.Rows[0]["MobileTele"].ToString();
            }
        }
        else if (type == 4)
        {
            DataTable dt = DAL.DBHelper.ExecuteDataTable("select StoreID,StoreName,MobileTele from StoreInfo where StoreID='" + billid + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                recipientNo = dt.Rows[0]["StoreID"].ToString();
                mobile = dt.Rows[0]["MobileTele"].ToString();
            }
        }

        #region 短信发送

        bool flag = false;

        using (SqlConnection conn = new SqlConnection(DAL.DBHelper.connString))
        {
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();

            try
            {
                if (msg.Trim().Length > 0)
                {
                    if (type == 1)
                    {
                        //flag = BLL.MobileSMS.SendMsgTo(tran, recipientNo, "", mobile, msg, out info, Model.SMSCategory.sms_Register);
                        flag = MobileSMS.SendMsgMode(tran, recipientName, msg, recipientNo, mobile, "", Model.SMSCategory.sms_Register);
                    }
                    else if (type == 2)
                    {
                        flag = BLL.MobileSMS.SendMsgTo(tran, recipientNo, "", mobile, msg, out info, Model.SMSCategory.sms_Delivery);
                    }
                    else if (type == 3)
                    {
                        //flag = BLL.MobileSMS.SendMsgTo(tran, recipientNo, "", mobile, msg, out info, Model.SMSCategory.sms_menberPassRest);
                        flag = BLL.MobileSMS.SendMsgTo(tran, recipientNo, "", mobile, msg, out info, Model.SMSCategory.sms_menberPassRest);
                    }
                    else if (type == 4)
                    {
                        //flag = BLL.MobileSMS.SendMsgTo(tran, recipientNo, "", mobile, msg, out info, Model.SMSCategory.sms_storePassRest);
                        flag = BLL.MobileSMS.SendMsgTo(tran, recipientNo, "", mobile, msg, out info, Model.SMSCategory.sms_storePassRest);
                    }
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        #endregion

    }

    /// <summary>
    /// 名商通短信接口-发送短信
    /// </summary>
    /// <param name="name"></param>
    /// <param name="pwd"></param>
    /// <param name="mobile"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static string SendMsg(string name, string pwd, string mobile, string message)
    {
        string mRtv = "";

        name = System.Web.HttpUtility.UrlEncode(name, System.Text.Encoding.GetEncoding("gb2312"));
        pwd = System.Web.HttpUtility.UrlEncode(pwd, System.Text.Encoding.GetEncoding("gb2312"));
        message = System.Web.HttpUtility.UrlEncode(message, System.Text.Encoding.GetEncoding("gb2312"));
        string mToUrl = "http://www.139000.com/send/gsend.asp?name=" + name + "&pwd=" + pwd + "&dst=" + mobile + "&msg=" + message + "&txt=ccdx";
        try
        {
            System.Net.HttpWebResponse rs = (System.Net.HttpWebResponse)System.Net.HttpWebRequest.Create(mToUrl).GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(rs.GetResponseStream(), System.Text.Encoding.Default);
            mRtv = sr.ReadToEnd();
        }
        catch (Exception ex)
        {
            mRtv = ex.Message.ToString().Trim();
        }

        return mRtv;
    }

    #endregion

    #region  钱包支付
    static CookieContainer cookie = new CookieContainer();
    public const string app_id = "58b7824b8b4f5c339bddf6079d153145";//	String	是	应用ID
    //public const string access_token = "4dedb14fae76dae682de02e671eac408";//	string	是	础授权接口
    public const string app_secret = "7182d95496d812c62c81707126eac754";
    public const string lang="zh_CN";//	string	是	i18n 语言,固定zh_CN
    public const string version="1.0";//	string	是	接口版本固定1.0
    public const string charset = "utf8";//	string	是	固定utf8
    public const string serverUrl = "HTTPS://oauth.factorde.com/api";

    public static string GetFunction(string Url, Dictionary<string, string> dic)
    {
        

        StringBuilder builder = new StringBuilder();

        builder.Append(Url);
        if (dic.Count > 0)  
        {        
            builder.Append("?");        
            int i = 0;        
            foreach (var item in dic)       
            {            
                if (i > 0)                
                    builder.Append("&");            
                builder.AppendFormat("{0}={1}", item.Key, item.Value);           
                i++;        
            }    
        }

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(builder.ToString());
        request.Method = "GET";
        request.ContentType = "application/json;charset=UTF-8";
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream myResponseStream = response.GetResponseStream();
        StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
        string retString = myStreamReader.ReadToEnd();
        myStreamReader.Close();
        myResponseStream.Close();
        return retString;
    }
    public static string doHttpPost(string url, Dictionary<string, string> dic)
    {
        string result = "";
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";
        
        StringBuilder builder = new StringBuilder();
        int i = 0;
        foreach (var item in dic)
        {
            if (i > 0)
                builder.Append("&");
            builder.AppendFormat("{0}={1}", item.Key, item.Value);
            i++;
        }
        byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
        req.ContentLength = data.Length;
        using (Stream reqStream = req.GetRequestStream())
        {
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();
        }
        
        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        Stream stream = resp.GetResponseStream();
        //获取响应内容
        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        {
            result = reader.ReadToEnd();
        }
        return result;

        
    }
    public static string GetSignContent(IDictionary<string, string> parameters)
    {
        // 第一步：把字典按Key的字母顺序排序
        IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
        IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

        // 第二步：把所有参数名和参数值串在一起
        StringBuilder query = new StringBuilder("");
        while (dem.MoveNext())
        {
            string key = dem.Current.Key;
            string value = dem.Current.Value;
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                query.Append(key).Append("=").Append(value).Append("&");
            }
        }
        
        string content = query.ToString().Substring(0, query.Length - 1);

        return content;
    }


    public static string GetSignContents(IDictionary<string, string> parameters, string appSecret)
    {
        // 第一步：把字典按Key的字母顺序排序
        IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
        IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

        // 第二步：把所有参数名和参数值串在一起
        StringBuilder query = new StringBuilder("");
        while (dem.MoveNext())
        {
            string key = dem.Current.Key;
            string value = dem.Current.Value;
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                query.Append(key).Append("=").Append(value).Append("&");
            }
        }
        StringBuilder qmq = new StringBuilder("");
       string content = query.ToString().Substring(0, query.Length - 1);
       qmq.Append(appSecret);

        content=content+qmq.ToString();

        return content;
    }
    public static string HmacSHA256(string secret, string signKey)      
    {            
        string signRet = string.Empty;   
        using (HMACSHA256 mac = new HMACSHA256(Encoding.UTF8.GetBytes(signKey)))        
        {              
            byte[] hash = mac.ComputeHash(Encoding.UTF8.GetBytes(secret));    
            signRet = Convert.ToBase64String(hash);               
            signRet = ToHexString(hash).ToUpper() ;       
        }            return signRet;      
    }
    
    //byte[]转16进制格式string       
    public static string ToHexString(byte[] bytes)    
    {            string hexString = string.Empty;     
        if (bytes != null)        
        {             
            StringBuilder strB = new StringBuilder();    
            foreach (byte b in bytes)            
            {                 
                strB.AppendFormat("{0:x2}", b);    
            }               
            hexString = strB.ToString();     
        }          
        return hexString;      
    }

    #endregion
}