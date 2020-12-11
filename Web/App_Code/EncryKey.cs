using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
/// <summary>
///EncryKey 的摘要说明
/// </summary>
public static class EncryKey
{
 
   static  string sKey = "20120530";

   


    public static string Encrypt(string pToEncrypt)
    {
        StringBuilder ret = new StringBuilder();
        try
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //把字符串放到byte数组中     
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

            //建立加密对象的密钥和偏移量     
            //原文使用ASCIIEncoding.ASCII方法的GetBytes方法     
            //使得输入密码必须输入英文文本     
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            //Write the byte array into the crypto stream     
            //(It will end up in the memory stream)     
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //Get the data back from the memory stream, and into a string     

            foreach (byte b in ms.ToArray())
            {
                //Format as hex     
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
        }
        catch (Exception)
        {
            return pToEncrypt;

        }
        return ret.ToString();
    }


    public  static string Decrypt(string pToDecrypt)
    {
        MemoryStream ms = new MemoryStream();
        try
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //Put the input string into the byte array     
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            //建立加密对象的密钥和偏移量，此值重要，不能修改     
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            //Flush the data through the crypto stream into the memory stream     
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            //Get the decrypted data back from the memory stream     
            //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象     
            StringBuilder ret = new StringBuilder();
        }
        catch (Exception)
        {
            return pToDecrypt;

        }

        return System.Text.Encoding.Default.GetString(ms.ToArray());
    }

    /// <summary>
    /// 获取一个加密字串
    /// </summary>
    /// <param name="payid">支付id（订单号或汇款单号）</param>
    /// <param name="paytype">支付类型（1订单支付或2汇款充值）</param>
    /// <param name="payrole">支付角色（1会员或2店铺）</param>
    /// <returns></returns>
    public static string GetEncryptstr(string payid,int paytype ,int  payrole) 
    {
        string  newstr=payid+"_"+paytype.ToString()+"_"+payrole.ToString();
        return   Encrypt(newstr).ToLower();
    }
    /// <summary>
    /// 解密字符串得到数组
    /// </summary>
    /// <param name="Encstr"></param>
    /// <returns></returns>
    public static string[] GetDecrypt(string Encstr) 
    {
        string dstr = Decrypt(Encstr.ToUpper());

        if (dstr.IndexOf('_') !=-1)
        {
            return dstr.Split('_');
        }
        else return new string[] { Encstr };
    }
}
