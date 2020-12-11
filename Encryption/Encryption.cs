using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;


namespace Encryption
{
    public class Encryption
    {
        private static string valueState;//修改设置时

        public static string ValueState
        {
            get { return valueState; }
            set { valueState = value; }
        }


        /// <summary>
        /// 获取是否需要加密——ds2012——tianfeng
        /// </summary>
        /// <param name="flag">加密类型</param>
        /// <returns></returns>
        public static bool GetEncryptionSetting(string flag)
        {
            
            bool state = false;
            using (SqlConnection conn = new SqlConnection(new NumberByBit().DecryptDES(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString, "qc2010jmDataLine")))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select top 1 isnull(encryptionvalue,0) from encryptionsetting where encryptionkey=@key", conn);
                    SqlParameter para = new SqlParameter("@key", flag);
                    cmd.Parameters.Add(para);
                    if (cmd.ExecuteScalar().ToString() == "1")
                    {
                        state = true;
                    }
                }
                catch
                {
                }
                finally
                {
                    conn.Close();
                }
            }

            return state;
        }

        /// <summary>
        /// 密码加密——ds2012——tianfeng
        /// </summary>
        /// <param name="pwd">密码</param>
        /// <param name="number">编号</param>
        /// <returns>加密后密码</returns>
        public static string GetEncryptionPwd(string pwd, string number)
        {
            pwd = pwd.ToLower();
            string pass = "17YS" + pwd;
            for (int i = 0; i < number.Length; i += 2)
            {
                pass += number.Substring(i, 1);
            }
            pass += "20SL";
            pass = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pass, "MD5");
            ArrayList arr = new ArrayList();
            for (int i = 0; i < pass.Length; i++)
            {
                arr.Add(pass.Substring(i, 1));
            }
            arr.Reverse();
            pass = "";
            for (int i = 0; i < arr.Count; i++)
            {
                pass += arr[i].ToString();
            }
            return pass;
        }


        /// <summary>
        /// 加密姓名——ds2012——tianfeng
        /// </summary>
        /// <param name="name">用户姓名</param>
        /// <returns>加密后的姓名</returns>
        public static string GetEncryptionName(string name)
        {
            //判断是否设置加密
            if ((ValueState == "1" || GetEncryptionSetting("--Name--")) && name != "" && name != null)
            {
                try
                {
                    char[] c = name.ToCharArray();
                    name = "";
                    //混淆
                    for (int i = 0; i < c.Length; i++)
                    {
                        byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                        name += b[0] + "," + b[1] + ",";
                    }
                    //倒叙
                    c = name.ToCharArray();
                    name = "";
                    for (int i = (c.Length - 1); i >= 0; i--)
                    {
                        name += c[i].ToString();
                    }
                }
                catch
                {
                    name = "";
                }
            }

            return name;
        }

        /// <summary>
        /// 解密姓名——ds2012——tianfeng
        /// </summary>
        /// <param name="name">加密后的姓名</param>
        /// <returns>解密后的姓名</returns>
        public static string GetDecipherName(string name)
        {
            //判断是否设置加密
            if ((ValueState == "1" || GetEncryptionSetting("--Name--")) && name != "" && name != null)
            {
                try
                {
                    char[] c = name.ToCharArray();
                    //倒叙
                    name = "";
                    for (int i = (c.Length - 1); i >= 0; i--)
                    {
                        name += c[i].ToString();
                    }
                    string[] s = name.Split(new char[] { ',' });

                    name = "";
                    for (int i = 0; i < (s.Length - 1); i++)
                    {
                        if (i % 2 == 0)
                        {
                            byte[] b = new byte[2];
                            b[0] = (byte)Convert.ToInt32(s[i]);
                            b[1] = (byte)Convert.ToInt32(s[i + 1]);
                            name += System.Text.Encoding.Unicode.GetChars(b)[0].ToString();
                        }
                    }
                }
                catch
                {
                    name = "";
                }
                //name="";
                //for(int i=0;i<c.Length;i++)
                //{
                //    name+="●";
                //}
            }
            return name;
        }

        /// <summary>
        /// 电话号码加密
        /// </summary>
        /// <param name="tele">号码</param>
        /// <returns>解密结果</returns>
        public static string GetEncryptionTele(string tele)
        {
            //判断是否设置加密
            if ((ValueState == "1" || GetEncryptionSetting("--Tele--")) && tele != "" && tele != null)
            {
                try
                {
                    char[] c = tele.ToCharArray();
                    char temp;
                    //临近的奇数位和偶数位颠倒
                    for (int i = 0; i < c.Length; i++)
                    {
                        if (i % 2 == 1)
                        {
                            temp = c[i];
                            c[i] = c[i - 1];
                            c[i - 1] = temp;
                        }
                    }
                    //倒叙
                    tele = "";
                    for (int i = (c.Length - 1); i >= 0; i--)
                    {
                        tele += c[i].ToString();
                    }
                }
                catch
                {
                    tele = "";
                }
            }
            return tele;
        }

        /// <summary>
        /// 解密电话号码——ds2012——tianfeng
        /// </summary>
        /// <param name="tele">加密后的电话号码</param>
        /// <returns>原电话号码</returns>
        public static string GetDecipherTele(string tele)
        {
            //判断是否设置加密
            if ((ValueState == "1" || GetEncryptionSetting("--Tele--")) && tele != "" && tele != null)
            {
                try
                {
                    char[] c = tele.ToCharArray();
                    //倒叙
                    tele = "";
                    for (int i = (c.Length - 1); i >= 0; i--)
                    {
                        tele += c[i].ToString();
                    }
                    //临近的奇数位和偶数位颠倒
                    c = tele.ToCharArray();
                    char temp;
                    for (int i = 0; i < c.Length; i++)
                    {
                        if (i % 2 == 1)
                        {
                            temp = c[i];
                            c[i] = c[i - 1];
                            c[i - 1] = temp;
                        }
                    }

                    tele = new string(c);
                }
                catch
                {
                    tele = "";
                }
            }
            return tele;
        }

        /// <summary>
        /// 地址加密
        /// </summary>
        /// <param name="address">地址信息</param>
        /// <returns>加密后地址信息</returns>
        public static string GetEncryptionAddress(string address)
        {
            //判断是否设置加密
            if ((ValueState == "1" || GetEncryptionSetting("--Address--")) && address != "" && address != null)
            {
                try
                {
                    char[] c = address.ToCharArray();
                    address = "";
                    for (int i = 0; i < c.Length; i++)
                    {
                        byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                        address += System.Web.HttpServerUtility.UrlTokenEncode(b);
                    }
                    c = address.ToCharArray();
                    address = "";
                    for (int i = (c.Length - 1); i >= 0; i--)
                    {
                        address += c[i].ToString();
                    }
                }
                catch
                {
                    address = "";
                }
            }

            return address;
        }

        /// <summary>
        /// 解密地址信息——ds2012——tianfeng
        /// </summary>
        /// <param name="address">加密后的地址信息</param>
        /// <returns>原地址信息</returns>
        public static string GetDecipherAddress(string address)
        {
            //判断是否设置加密
            if ((ValueState == "1" || GetEncryptionSetting("--Address--")) && address != "" && address != null)
            {
                try
                {
                    char[] c = address.ToCharArray();
                    address = "";
                    for (int x = (c.Length - 1); x >= 0; x--)
                    {
                        address += c[x].ToString();
                    }
                    c = address.ToCharArray();
                    address = "";
                    string temp = "";
                    int i = 0, j = 1;
                    while (i < c.Length)
                    {
                        temp += Convert.ToString(c[i]);
                        if (j == 4)
                        {
                            byte[] b = System.Web.HttpServerUtility.UrlTokenDecode(temp);
                            address += System.Text.Encoding.Unicode.GetChars(b)[0].ToString();
                            j = 0;
                            temp = "";
                        }
                        i++;
                        j++;
                    }
                    c = address.ToCharArray();
                }
                catch
                {
                    address = "";
                }
            }
            return address;
        }

        /// <summary>
        /// 卡号加密——ds2012——tianfeng
        /// </summary>
        /// <param name="card">卡号</param>
        /// <returns>加密后卡号</returns>
        public static string GetEncryptionCard(string card)
        {
            //判断是否设置加密
            if ((ValueState == "1" || GetEncryptionSetting("--Card--")) && card != "" && card != null)
            {
                try
                {
                    char[] c = card.ToCharArray();
                    //混淆
                    for (int i = 0; i < c.Length; i++)
                    {
                        byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                        if (b.Length != 0)
                        {
                            b[0] = (byte)(b[0] + 1);
                            c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                        }
                    }

                    //临近的奇数位和偶数位颠倒
                    char temp;
                    for (int i = 0; i < c.Length; i++)
                    {
                        if (i % 2 == 1)
                        {
                            temp = c[i];
                            c[i] = c[i - 1];
                            c[i - 1] = temp;
                        }
                    }
                    card = new string(c);
                }
                catch
                {
                    card = "";
                }
            }

            return card;
        }

        /// <summary>
        /// 解密卡号
        /// </summary>
        /// <param name="card">加密后卡号</param>
        /// <returns>原卡号</returns>
        public static string GetDecipherCard(string card)
        {
            //判断是否设置加密
            if ((ValueState == "1" || GetEncryptionSetting("--Card--")) && card != "" && card != null)
            {
                try
                {
                    char[] c = card.ToCharArray();

                    //临近的奇数位和偶数位颠倒
                    char temp;
                    for (int i = 0; i < c.Length; i++)
                    {
                        if (i % 2 == 1)
                        {
                            temp = c[i];
                            c[i] = c[i - 1];
                            c[i - 1] = temp;
                        }
                    }

                    //去除混淆
                    for (int i = 0; i < c.Length; i++)
                    {
                        byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                        if (b.Length != 0)
                        {
                            b[0] = (byte)(b[0] - 1);
                            c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                        }
                    }

                    card = new string(c);
                }
                catch
                {
                    card = "";
                }
                //card = "";
                //for (int i = 0; i < c.Length; i++)
                //{
                //    card += "●";
                //}
            }

            return card;
        }


        /// <summary>
        /// 证件号码加密——ds2012——tianfeng
        /// </summary>
        /// <param name="num">证件号码</param>
        /// <returns>加密后证件号码</returns>
        public static string GetEncryptionNumber(string num)
        {
            //判断是否设置加密
            if ((ValueState == "1" || GetEncryptionSetting("--Number--")) && num != "" && num != null)
            {
                try
                {
                    char[] c = num.ToCharArray();
                    //临近的奇数位和偶数位颠倒
                    char temp;
                    for (int i = 0; i < c.Length; i++)
                    {
                        if (i % 2 == 1)
                        {
                            temp = c[i];
                            c[i] = c[i - 1];
                            c[i - 1] = temp;
                        }
                    }

                    //混淆
                    for (int i = 0; i < c.Length; i++)
                    {
                        byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                        if (b.Length != 0)
                        {
                            b[0] = (byte)(b[0] - 1);
                            c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                        }
                    }
                    num = new string(c);
                }
                catch
                {
                    num = "";
                }
            }
            return num;
        }

        /// <summary>
        /// 解密证件号码——ds2012——tianfeng
        /// </summary>
        /// <param name="num">加密后证件号码</param>
        /// <returns>证件号码</returns>
        public static string GetDecipherNumber(string num)
        {
            //判断是否设置加密
            if ((ValueState == "1" || GetEncryptionSetting("--Number--")) && num != "" && num != null)
            {
                try
                {
                    char[] c = num.ToCharArray();
                    //去除混淆
                    for (int i = 0; i < c.Length; i++)
                    {
                        byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                        if (b.Length != 0)
                        {
                            b[0] = (byte)(b[0] + 1);
                            c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                        }
                    }

                    //临近的奇数位和偶数位颠倒
                    char temp;
                    for (int i = 0; i < c.Length; i++)
                    {
                        if (i % 2 == 1)
                        {
                            temp = c[i];
                            c[i] = c[i - 1];
                            c[i - 1] = temp;
                        }
                    }

                    num = new string(c);
                }
                catch
                {
                    num = "";
                }
                //num = "";
                //for (int i = 0; i < c.Length; i++)
                //{
                //    num += "●";
                //}
            }

            return num;
        }

        /// <summary>
        /// 手机版本MD5加密
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetEncryptionByMD5(string number)
        {
            number = number.ToLower();
            string str = "MOBILE" + number;
            for (int i = 0; i < number.Length; i += 2)
            {
                str += number.Substring(i, 1);
            }
            str += "VERSIONSERVICE";
            str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            ArrayList arr = new ArrayList();
            for (int i = 0; i < str.Length; i++)
            {
                arr.Add(str.Substring(i, 1));
            }
            arr.Reverse();
            str = "";
            for (int i = 0; i < arr.Count; i++)
            {
                str += arr[i].ToString();
            }
            return str;
        }

    }
}
