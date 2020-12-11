using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

namespace BLL.QuickPay
{
    public class QuickPaySign
    {
        public QuickPaySign()
        { 
        }


        /// <summary>
        /// 获取人民币网关账户
        /// </summary>
        /// <returns></returns>
        public static  string GetMerchantAcctId()
        {
            string ExSql = "select configValue  from sys_config where configName='quickPay_AccountID'";
            string AccountID = string.Empty;
            object objAccid = DAL.DBHelper.ExecuteScalar(ExSql);
            if (objAccid != DBNull.Value && objAccid != null)
            {
                AccountID = objAccid.ToString();
                AccountID = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(AccountID, new BLL.QuickPay.SecretSecurity().EpayKey);
            }
            return AccountID;
        }

        /// <summary>
        /// 获取人民币网关密钥
        /// </summary>
        /// <returns></returns>
        public static  string GetMerchantAcctKey()
        {
            string ExSql = "select configValue  from sys_config where configName='quickPay_Key'";
            string AccountKey = string.Empty;
            object objAccKey = DAL.DBHelper.ExecuteScalar(ExSql);
            if (objAccKey != DBNull.Value && objAccKey != null)
            {
                AccountKey = objAccKey.ToString();
                AccountKey = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(AccountKey, new BLL.QuickPay.SecretSecurity().EpayKey);
            }
            
            return AccountKey;
        }


        /// <summary>
        /// 返回加密的签名字符串
        /// </summary>
        /// <returns></returns>
        public string GetSignMsg( Model .QuickPay .quickPayParames qpp)
        {
            try
            {
                //生成加密签名串
                ///请务必按照如下顺序和规则组成加密串！
                string signMsgVal = "";
                signMsgVal = appendParam(signMsgVal, "inputCharset", qpp.InputCharset );
                signMsgVal = appendParam(signMsgVal, "pageUrl",  qpp.PageUrl);
                signMsgVal = appendParam(signMsgVal, "bgUrl", qpp.BgUrl);
                signMsgVal = appendParam(signMsgVal, "version", qpp.Version);
                signMsgVal = appendParam(signMsgVal, "language", qpp.Language);
                signMsgVal = appendParam(signMsgVal, "signType", qpp.SignType);
                signMsgVal = appendParam(signMsgVal, "merchantAcctId", qpp.MerchantAcctId);
                signMsgVal = appendParam(signMsgVal, "payerName", qpp.PayerName);
                signMsgVal = appendParam(signMsgVal, "payerContactType", qpp.PayerContactType);
                signMsgVal = appendParam(signMsgVal, "payerContact", qpp.PayerContact);
                signMsgVal = appendParam(signMsgVal, "orderId", qpp.OrderId);
                signMsgVal = appendParam(signMsgVal, "orderAmount", qpp.OrderAmount.ToString());
                signMsgVal = appendParam(signMsgVal, "orderTime", qpp.OrderTime);
                signMsgVal = appendParam(signMsgVal, "productName", qpp.ProductName);
                signMsgVal = appendParam(signMsgVal, "productNum", qpp.ProductNum.ToString());
                signMsgVal = appendParam(signMsgVal, "productId", qpp.ProductId);
                signMsgVal = appendParam(signMsgVal, "productDesc", qpp.ProductDesc);
                signMsgVal = appendParam(signMsgVal, "ext1", qpp.Ext1);
                signMsgVal = appendParam(signMsgVal, "ext2", qpp.Ext2);
                signMsgVal = appendParam(signMsgVal, "payType", qpp.PayType);
                signMsgVal = appendParam(signMsgVal, "bankId", qpp.BankId);
                signMsgVal = appendParam(signMsgVal, "redoFlag", qpp.RedoFlag);
                signMsgVal = appendParam(signMsgVal, "pid", qpp.Pid);
                if (qpp.SignType == "1")
                {
                    signMsgVal = appendParam(signMsgVal, "key", qpp.Key);
                }
                //如果在web.config文件中设置了编码方式，例如<globalization requestEncoding="utf-8" responseEncoding="utf-8"/>（如未设则默认为utf-8），
                //那么，inputCharset的取值应与已设置的编码方式相一致；
                //同时，GetMD5()方法中所传递的编码方式也必须与此保持一致。
                string signMsg = "";
                if (qpp.SignType == "1")
                {//MD5签名
                    signMsg = GetMD5(signMsgVal, "utf-8").ToUpper();
                }
                else if (qpp.SignType == "4")
                {//证书签名                  
                    string priPath = System.Configuration.ConfigurationManager.AppSettings["priKeyPath"].ToString();		//商户私钥证书路径
                    string phyPath = System.AppDomain.CurrentDomain.BaseDirectory;                                          //项目目录路径
                    string prikey_path = string .Empty ;               //商户私钥路径
                    string CertificatePW = string.Empty;        //商户私钥密码 "biiId9e";

                    #region 获取项目根目录父级目录
                    /*
                    phyPath = phyPath.Substring(0, phyPath.Length - 1);    //去最后一个“\”
                    int endIndex = 0;
                    int len = phyPath.Length;
                    if (phyPath.LastIndexOf("\\") > 0)
                    {
                        endIndex = phyPath.LastIndexOf("\\");
                        len = endIndex;
                    }
                    phyPath = phyPath.Substring(0, len);  //上移一个目录 [网站的上级目录路径，防止证书被下载]
                                     
                     */
                    #endregion
                   
                    if(priPath !="")
                        prikey_path = System.IO.Path.Combine(phyPath, priPath);// phyPath + priPath;
                                    
                  
                    //获取商户加密的私钥密码
                    CertificatePW =DAL.DBHelper.ExecuteScalar("select top 1 configValue from sys_config where configName='CertificatePriPub_pwd'").ToString();
                    //解密
                    CertificatePW =BLL.QuickPay .SecretSecurity.Decrypt3Des24Bit(CertificatePW,new BLL.QuickPay .SecretSecurity().EpayKey );

                    signMsg = BLL.QuickPay.PKI .CerRsa.CerRSASignature(signMsgVal, prikey_path, CertificatePW, 2);
                }
                return signMsg;
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString().ToLower();
                if (msg.IndexOf("密码") > 0 || msg.IndexOf("password") > 0)
                {
                    throw new Exception("证书密码错误！");

                }
                return null;

            }
        }

        /// <summary>
        /// 功能函数。将变量值不为空的参数组成字符串
        /// </summary>
        /// <param name="returnStr"返回字符串></param>
        /// <param name="paramId">参数键名</param>
        /// <param name="paramValue">参数键值</param>
        /// <returns></returns>
        public string appendParam(string returnStr, string paramId, string paramValue)
        {

            if (returnStr != "")
            {

                if (paramValue != "")
                {

                    returnStr += "&" + paramId + "=" + paramValue;
                }

            }
            else
            {

                if (paramValue != "")
                {
                    returnStr = paramId + "=" + paramValue;
                }
            }

            return returnStr;
        }
        //功能函数。将变量值不为空的参数组成字符串。结束



        /// <summary>
        /// 功能函数。将字符串进行编码格式转换，并进行MD5加密，然后返回。开始
        /// </summary>
        /// <param name="dataStr">待加密字符串</param>
        /// <param name="codeType">编码类型</param>
        /// <returns></returns>
        public static string GetMD5(string dataStr, string codeType)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(System.Text.Encoding.GetEncoding(codeType).GetBytes(dataStr));
            System.Text.StringBuilder sb = new System.Text.StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取人民币网关账户
        /// </summary>
        /// <returns></returns>
        public static string GetMerchantAcctInfo(string conname)
        {
            string ExSql = "select configValue  from sys_config where configName='" + conname + "'";
            string AccountID = string.Empty;
            object objAccid = DAL.DBHelper.ExecuteScalar(ExSql);
            if (objAccid != DBNull.Value && objAccid != null)
            {
                AccountID = objAccid.ToString();
                if (conname != "huanxunMer_key") 
                AccountID = BLL.QuickPay.SecretSecurity.Decrypt3Des24Bit(AccountID, new BLL.QuickPay.SecretSecurity().EpayKey);
            }
            return AccountID;
        }
    }
}
