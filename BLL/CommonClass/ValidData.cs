using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Add Namespace
using System.Data;
using System.Data.SqlClient;
using DAL;
using System.Text.RegularExpressions;

/**
 *  作者：郑华超
 *  时间：2009.9.1
 *  修改者：汪华
 *  修改时间：2009-09-04
 *  功能：用于验证各种数据的合法性
 * 
 */
namespace BLL.CommonClass
{
    public class ValidData
    {
        public ValidData()
        {

        }

        /// <summary>
        /// 验证身份证的合法性
        /// </summary>
        /// <param name="content">要验证的身份证号码</param>
        /// <param name="birthday">检验身份证号码中的出生年月是否符合输入的日期</param>
        /// <param name="sex">性别,1表示男，0表示女</param>
        /// <param name="errMessage">如果产生错误，则包含错误信息</param>
        /// <returns>表示验证是否成功</returns>
        public static bool ValidShengFenZheng(string content, DateTime birthday, int sex, out string errMessage)
        {
            //把字母替换成大写字母
            content = content.ToUpper();

            errMessage = "";

            int length = content.Length;
            //验证长度
            if (length != 15 && length != 18)
            {
                errMessage = "身份证长度错误！";
                return false;
            }

            //检验前两位
            int fstTwo = Convert.ToInt32(content.Substring(0, 2));
            if (fstTwo < 11 || (fstTwo > 15 && fstTwo < 21) || (fstTwo > 23 && fstTwo < 31) || (fstTwo > 37 && fstTwo < 41) || (fstTwo > 46 && fstTwo < 50) || (fstTwo > 54 && fstTwo < 61) || (fstTwo > 65 && fstTwo != 71 && fstTwo != 81 && fstTwo != 91))
            {
                errMessage = "身份证前两位错误！";
                return false;
            }

            //			//检验三四位
            //			int secTwo = Convert.ToInt32(content.Substring(2,2));
            //			if ( secTwo > 70 || secTwo < 1 )
            //			{
            //				errMessage = "身份证三四位错误！";
            //				return false;
            //			}

            //			//检验五六位
            //			int thrTwo = Convert.ToInt32(content.Substring(4,2));
            //			if ( thrTwo == 0 )
            //			{
            //				errMessage = "身份证五六位错误！";
            //				return false;
            //			}

            //检验后续位
            if (length == 15)
            {
                //检验日期信息
                string bthStr = content.Substring(6, 6);

                //组成完整的日期信息，年变成4位，由于两位保存年的身份证号只限于1900-1999，所以加上 “19”。
                bthStr = "19" + bthStr;

                string monthStr;
                if (birthday.Month < 10)
                    monthStr = "0" + birthday.Month.ToString();
                else
                    monthStr = birthday.Month.ToString();
                string dayStr;
                if (birthday.Day < 10)
                    dayStr = "0" + birthday.Day.ToString();
                else
                    dayStr = birthday.Day.ToString();

                if (bthStr != (birthday.Year.ToString() + monthStr + dayStr))
                {
                    errMessage = "对不起，身份证号码中的出生年月跟输入的出生年月不符！";
                    return false;
                }

                //                int fthStr = Convert.ToInt32(content.Substring(12,2));
                //				if ( fthStr == 0 )
                //				{
                //					errMessage = "身份证十三，十四位错误！";
                //					return false;
                //				}

                //				//验证性别
                int checkSex = Convert.ToInt32(content.Substring(14, 1));
                if (checkSex % 2 != sex)
                {
                    errMessage = "身份证中的性别信息跟用户输入的性别信息不符！";
                    return false;
                }
            }
            else
            {
                //检验日期信息
                string bthStr = content.Substring(6, 8);

                string monthStr;
                if (birthday.Month < 10)
                    monthStr = "0" + birthday.Month.ToString();
                else
                    monthStr = birthday.Month.ToString();
                string dayStr;
                if (birthday.Day < 10)
                    dayStr = "0" + birthday.Day.ToString();
                else
                    dayStr = birthday.Day.ToString();

                if (bthStr != (birthday.Year.ToString() + monthStr + dayStr))
                {
                    errMessage = "对不起，身份证号码中的出生年月跟输入的出生年月不符！";
                    return false;
                }

                //				int fthStr = Convert.ToInt32(content.Substring(14,2));
                //				if ( fthStr == 0 )
                //				{
                //					errMessage = "身份证十五，十六位错误！";
                //					return false;
                //				}

                //验证性别
                int checkSex = Convert.ToInt32(content.Substring(16, 1));
                if (checkSex % 2 != sex)
                {
                    errMessage = "身份证中的性别信息跟用户输入的性别信息不符！";
                    return false;
                }

                string last = content.Substring(16, 1);
                bool valid = false;
                for (int i = 0; i <= 9; i++)
                {
                    if (last == i.ToString())
                        valid = true;
                }
                if (valid == false && last != "X")
                {
                    errMessage = "身份证最后一位错误！";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 把店的货物转换成金额
        /// </summary>
        /// <param name="dian"></param>
        public static void TransformProduct(string dian)
        {
            double price = 0;
            //	double pv = 0;
            string sqlStr;

            SqlDataReader reader = DBHelper.ExecuteReader("SELECT D_kucun.huoid,D_kucun.kucunshu,product.price2,product.pv2 from D_kucun,product where D_kucun.dian='" + dian + "' and D_kucun.kucunshu > 0 and D_kucun.huoid = product.id");

            while (reader.Read())
            {
                price = Convert.ToDouble(reader["kucunshu"]) * Convert.ToDouble(reader["price2"]);

                //减去总消费
                sqlStr = "UPDATE d_info SET zongxf = zongxf-" + price + " where dian='" + dian + "'";
                DBHelper.ExecuteNonQuery(sqlStr);
                //公司加上库存
                DBHelper.ExecuteNonQuery("UPDATE product set chuku=chuku-" + Convert.ToDouble(reader["kucunshu"]) + " where id=" + reader["huoid"]);

            }
            reader.Close();

            //更新店铺的库存
            DBHelper.ExecuteNonQuery("UPDATE D_kucun SET kucunshu=0 where dian='" + dian + "' and kucunshu>0");

        }



        /// <summary>
        /// Web页面输入框非法字符过滤,输入安全校验
        /// </summary>
        public static string InputText(string _str)
        {
            StringBuilder retVal = new StringBuilder();
            Regex rxb = new Regex(@"~|!|=|>|<|>=|<=|!=|--|/\*|\*/|'|;", RegexOptions.IgnoreCase);   //去数学运算符
            if (_str != null)
            {
                //				_str = _str.Trim();
                for (int i = 0; i < _str.Length; i++)
                {
                    switch (_str[i])
                    {
                        case '\"':
                            retVal.Append("&quot;");
                            break;
                        case '<':
                            retVal.Append("&lt;");
                            break;
                        case '>':
                            retVal.Append("&gt;");
                            break;
                        default:
                            retVal.Append(_str[i]);
                            break;
                    }
                }
                //retVal.Replace("'", "");                     
            }
            return rxb.Replace(retVal.ToString(), "");
        }




        /// <summary>
        /// 过滤Javascript,iframe,fremeset,input href=javasript on <stye></stye>,<link></link> 
        /// 添加人：xyc 添加时间：2011-12-6
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveScript(string html)
        {
            Regex regex1 = new Regex(@"<script[\s\S]+</script *>", RegexOptions.IgnoreCase);
            Regex regex2 = new Regex(@" href\s*=.*javascript[^>\s]*", RegexOptions.IgnoreCase);
            Regex regex3 = new Regex(@" on\w+=", RegexOptions.IgnoreCase);
            Regex regex4 = new Regex(@"<iframe[\s\S]+</iframe *>", RegexOptions.IgnoreCase);
            Regex regex5 = new Regex(@"<frameset[\s\S]+</frameset *>", RegexOptions.IgnoreCase);
            Regex regex6 = new Regex(@"<input[\s\S]+</input *>", RegexOptions.IgnoreCase);
            // Regex regex7 = new Regex(@"<style[\s\S]+</style *>", RegexOptions.IgnoreCase);
            Regex regex8 = new Regex(@"<link[\s\S]+</link *>", RegexOptions.IgnoreCase);
            Regex regex9 = new Regex(@"<(input|link|iframe|frameset|frame)[^>]*/>", RegexOptions.IgnoreCase);
            html = regex1.Replace(html, string.Empty); //过滤<script></script>标记 
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件 
            html = regex4.Replace(html, string.Empty); //过滤iframe 
            html = regex5.Replace(html, string.Empty); //过滤frameset 
            html = regex6.Replace(html, string.Empty); //过滤input
            //html = regex7.Replace(html, string.Empty); //过滤 style
            html = regex8.Replace(html, string.Empty); //过滤link 
            html = regex2.Replace(html, " href=\"#\""); //过滤href=javascript: (<A>) 属性
            html = regex9.Replace(html, string.Empty); //过滤href=javascript: (<A>) 属性
            return html;
        }
    }
}
