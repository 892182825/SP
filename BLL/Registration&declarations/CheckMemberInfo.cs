using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Registration_declarations
{
    /// <summary>
    /// 验证 报单信息
    /// </summary>
    public class CheckMemberInfo : TranslationBase
    {
        
       public CheckMemberInfo() { }
       /// <summary>
       /// 根据身份证号码得到出生日期和性别 
       /// 18位的身份证，前面六位代表了你户籍所在地，第七位到第十四位代表了你的出生年月，第十五位到第十七为代表了你的性别（偶数为女，奇数为男）,第18位为校验码(一般为0-9， X是罗马数字的10，用X来代替10)
       /// </summary>
       /// <param name="IdentityCard">身份证</param>
       /// <returns>返回格式：“1900-09-12,男”</returns>
       public static string CHK_IdentityCard(string identityCard)
       {
           string info = string.Empty;
           if (string.IsNullOrEmpty(identityCard))
           {
               info = Translation.Translate("000379", "身份证号码不能为空");//身份证号码不能为空，如果为空返回
               return info;
           }
           else 
           {
               if (identityCard.Length != 15 && identityCard.Length != 18)//身份证号码只能为15位或18位其它不合法
               {
                   info = Translation.Translate("000384", "身份证号码为15位或18位！请检查！");// "身份证号码为15位或18位！请检查！";
                   return info;
               }
            
           }

           try
            {
                string birthday = "";
                string sex = "";
                if (identityCard.Length == 18)//处理18位的身份证号码从号码中得到生日和性别代码
                {
                    bool flag=CheckIDCard18(identityCard);
                    if (flag == false)
                    {
                        info = Translation.Translate("000385", "非法身份证号码");//"非法身份证号码！";
                        return info;
                    }
                    birthday = identityCard.Substring(6, 4) + "-" + identityCard.Substring(10, 2) + "-" + identityCard.Substring(12, 2);

                    if (Convert.ToDateTime(birthday).AddYears(18) > DateTime.Now)
                    {
                        info = Translation.Translate("000000", "年龄必须大于18周岁！");//"非法身份证号码！";
                        return info;
                    }
                    sex = identityCard.Substring(14, 3);
                }
                if (identityCard.Length == 15)
                {
                    bool flag = CheckIDCard15(identityCard);
                    if (flag == false)
                    {
                        info = Translation.Translate("000385", "非法身份证号码");// "非法身份证号码！";
                        return info;
                    }
                    birthday = "19" + identityCard.Substring(6, 2) + "-" + identityCard.Substring(8, 2) + "-" + identityCard.Substring(10, 2);
                    if (Convert.ToDateTime(birthday).AddYears(18) > DateTime.Now)
                    {
                        info = Translation.Translate("000000", "年龄必须大于18周岁！");//"非法身份证号码！";
                        return info;
                    }
                    sex = identityCard.Substring(12, 3);
                }

                info = birthday;
                if (int.Parse(sex) % 2 == 0)//性别代码为偶数是女性奇数为男性
                {
                    info += ",女";
                }
                else
                {
                    info += ",男";
                }

                return info;
            }
            catch (Exception )
            {
                info = Translation.Translate("000392", "身份证号码输入有误");
                return info;
            }


       }

       private static bool CheckIDCard18(string Id)
       {
           long n = 0;
           if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
           {
               return false;//数字验证 
           }
           string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
           if (address.IndexOf(Id.Remove(2)) == -1)
           {
               return false;//省份验证 
           }
           string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
           DateTime time = new DateTime();
           if (DateTime.TryParse(birth, out time) == false)
           {
               return false;//生日验证 
           }
           string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
           string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
           char[] Ai = Id.Remove(17).ToCharArray();
           int sum = 0;
           for (int i = 0; i < 17; i++)
           {
               sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
           }
           int y = -1;
           Math.DivRem(sum, 11, out y);
           if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
           {
               return false;//校验码验证 
           }
           return true;//符合GB11643-1999标准 
       }

       private static bool CheckIDCard15(string Id)
       {
           long n = 0;
           if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
           {
               return false;//数字验证 
           }
           string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
           if (address.IndexOf(Id.Remove(2)) == -1)
           {
               return false;//省份验证 
           }
           string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
           DateTime time = new DateTime();
           if (DateTime.TryParse(birth, out time) == false)
           {
               return false;//生日验证 
           }
           return true;//符合15位身份证标准 
       }
    }
}
