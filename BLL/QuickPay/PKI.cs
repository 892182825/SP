using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace BLL.QuickPay.PKI
{

    #region Coding 字符与字节转码
    /// <summary>
    /// 字符与字节转码
    /// </summary>
    public class Coding
    {
        #region 参数类型转换
        /// <summary>
        /// 获取字节值，将字符型转换成字节型【strData：原字符；TypeId编码类型1：Base64编码， 2：UTF8编码， 3：ASCII编码】
        /// </summary>
        public static byte[] CodingToByte(string strData, int TypeId)
        {
            byte[] byteData;
            switch (TypeId)
            {
                case 1:
                    byteData = Convert.FromBase64String(strData);
                    break;
                case 2:
                    byteData = System.Text.Encoding.UTF8.GetBytes(strData);
                    break;
                default:
                    System.Text.ASCIIEncoding ByteConverter = new ASCIIEncoding();
                    byteData = ByteConverter.GetBytes(strData);
                    break;
            }
            return byteData;
        }

        /// <summary>
        /// 获取字符值， 将字节型转换成字符型【byteData：原字节；TypeId编码类型1：Base64编码， 2：UTF8编码， 3：ASCII编码】
        /// </summary>
        public static string CodingToString(byte[] byteData, int TypeId)
        {
            string stringData;
            switch (TypeId)
            {
                case 1:
                    stringData = System.Convert.ToBase64String(byteData);
                    break;
                case 2:
                    stringData = System.Text.Encoding.UTF8.GetString(byteData);
                    break;
                default:
                    System.Text.ASCIIEncoding ByteConverter = new ASCIIEncoding();
                    stringData = ByteConverter.GetString(byteData);
                    break;
            }
            return stringData;
        }

        #endregion
    }

    #endregion

    #region 基于RSA 的证书安全应用类
    /// <summary>
    /// 基于RSA 的证书安全应用类包括引用证书非对称加/解密RSA-私钥签名/和公钥验签,引用证书非对称加密/解密RSA-公钥加密获取密文和私钥解密获取原文
    /// </summary>
    public class CerRsa
    {
        #region MD5签名
        /// <summary>
        /// 将字符串进行编码格式转换，并进行MD5加密，返回签名串，【dataStr：原文；codeType：字符串编码类型，指定gb2312或utf-8】
        /// </summary>
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
        #endregion

        #region RSA证书签名/验签

        /// <summary>
        /// 私钥签名——引用证书非对称加/解密RSA-【OriginalString：原文（有中文用utf-8编码的字节）；prikey_path：私钥证书路径；CertificatePW：私钥证书密码；SignType：签名摘要类型（1：MD5，2：SHA1）】
        /// </summary>
        /// <param name="OriginalString">OriginalString：原文（有中文用utf-8编码的字节）</param>
        /// <param name="prikey_path">prikey_path：私钥证书路径</param>
        /// <param name="CertificatePW">CertificatePW：证书密码</param>
        /// <param name="SignType">SignType：签名摘要类型（1：MD5，2：SHA1）】</param>
        /// <returns></returns>
        public static byte[] CerRSASignature(byte[] OriginalString, string prikey_path, string CertificatePW, int SignType)
        {
            try
            {
                X509Certificate2 x509_Cer1 = new X509Certificate2(prikey_path, CertificatePW);
                RSACryptoServiceProvider rsapri = (RSACryptoServiceProvider)x509_Cer1.PrivateKey;
                RSAPKCS1SignatureFormatter f = new RSAPKCS1SignatureFormatter(rsapri);
                byte[] result;
                switch (SignType)
                {
                    case 1:
                        f.SetHashAlgorithm("MD5");//摘要算法MD5
                        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                        result = md5.ComputeHash(OriginalString);//摘要值
                        break;
                    default:
                        f.SetHashAlgorithm("SHA1");//摘要算法SHA1
                        SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                        result = sha.ComputeHash(OriginalString);//摘要值
                        break;
                }
                byte[] SignData = f.CreateSignature(result);
                return SignData;
            }
            catch (Exception ex)
            {
                // return null;
                throw ex;
            }
        }

        /// <summary>
        /// 引用证书非对称加/解密RSA-私钥签名【OriginalString：原文（有中文用utf-8编码的字节）；prikey_path：证书路径；CertificatePW：证书密码；SignType：签名摘要类型（1：MD5，2：SHA1）】
        /// </summary>
        public static string CerRSASignature(string OriginalString, string prikey_path, string CertificatePW, int SignType)
        {
            byte[] OriginalByte = System.Text.Encoding.UTF8.GetBytes(OriginalString);
            X509Certificate2 x509_Cer1 = new X509Certificate2(prikey_path, CertificatePW);
            RSACryptoServiceProvider rsapri = (RSACryptoServiceProvider)x509_Cer1.PrivateKey;
            RSAPKCS1SignatureFormatter f = new RSAPKCS1SignatureFormatter(rsapri);
            byte[] result;
            switch (SignType)
            {
                case 1:
                    f.SetHashAlgorithm("MD5");//摘要算法MD5
                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    result = md5.ComputeHash(OriginalByte);//摘要值
                    break;
                default:
                    f.SetHashAlgorithm("SHA1");//摘要算法SHA1
                    SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                    result = sha.ComputeHash(OriginalByte);//摘要值
                    break;
            }
            string SignData = System.Convert.ToBase64String(f.CreateSignature(result)).ToString();

            return SignData;
        }


        /// <summary>
        /// 公钥验签——引用证书非对称加/解密RSA-【OriginalString：原文（有中文用utf-8编码的字节）；SignatureString：签名字符；pubkey_path：证书路径；CertificatePW：证书密码；SignType：签名摘要类型（1：MD5，2：SHA1）】
        /// </summary>
        /// <param name="OriginalString">【OriginalString：原文（有中文用utf-8编码的字节）</param>
        /// <param name="SignatureString">SignatureString：签名字符</param>
        /// <param name="pubkey_path">pubkey_path：公钥证书路径</param>
        /// <param name="CertificatePW">CertificatePW：公钥证书密码</param>
        /// <param name="SignType">SignType：签名摘要类型（1：MD5，2：SHA1）】</param>
        /// <returns>bool:签名验证是否正确</returns>              
        public static bool CerRSAVerifySignature(byte[] OriginalString, byte[] SignatureString, string pubkey_path, string CertificatePW, int SignType)
        {
            try
            {
                X509Certificate2 x509_Cer1 = new X509Certificate2(pubkey_path, CertificatePW);
                RSACryptoServiceProvider rsapub = (RSACryptoServiceProvider)x509_Cer1.PublicKey.Key;
                rsapub.ImportCspBlob(rsapub.ExportCspBlob(false));
                RSAPKCS1SignatureDeformatter f = new RSAPKCS1SignatureDeformatter(rsapub);
                byte[] HashData;
                switch (SignType)
                {
                    case 1:
                        f.SetHashAlgorithm("MD5");//摘要算法MD5
                        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                        HashData = md5.ComputeHash(SignatureString);
                        break;
                    default:
                        f.SetHashAlgorithm("SHA1");//摘要算法SHA1
                        SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                        HashData = sha.ComputeHash(SignatureString);
                        break;
                }
                if (f.VerifySignature(HashData, OriginalString))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// 引用证书非对称加/解密RSA-公钥验签【OriginalString：原文；SignatureString：签名字符；pubkey_path：证书路径；CertificatePW：证书密码；SignType：签名摘要类型（1：MD5，2：SHA1）】
        /// </summary>
        public static bool CerRSAVerifySignature(string OriginalString, string SignatureString, string pubkey_path, string CertificatePW, int SignType)
        {
            byte[] OriginalByte = System.Text.Encoding.UTF8.GetBytes(OriginalString);
            byte[] SignatureByte = Convert.FromBase64String(SignatureString);
            X509Certificate2 x509_Cer1 = new X509Certificate2(pubkey_path, CertificatePW);
            RSACryptoServiceProvider rsapub = (RSACryptoServiceProvider)x509_Cer1.PublicKey.Key;
            rsapub.ImportCspBlob(rsapub.ExportCspBlob(false));
            RSAPKCS1SignatureDeformatter f = new RSAPKCS1SignatureDeformatter(rsapub);
            byte[] HashData;
            switch (SignType)
            {
                case 1:
                    f.SetHashAlgorithm("MD5");//摘要算法MD5
                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    HashData = md5.ComputeHash(OriginalByte);
                    break;
                default:
                    f.SetHashAlgorithm("SHA1");//摘要算法SHA1
                    SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                    HashData = sha.ComputeHash(OriginalByte);
                    break;
            }
            if (f.VerifySignature(HashData, SignatureByte))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }
    #endregion
}
