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
using System.Text.RegularExpressions;

///AddNamespace
using System.Collections;

/// <summary>
///PageBase 的摘要说明
/// </summary>
public class PageBase : System.Web.UI.Page
{
    private string _AspxFileName;	

    public PageBase():base()
    {
             
    }

    public string GetAspxTran(string Location)
    {
        
           return "";	//返回空字符串
        
    }

    public static string InputText(string text)
    {
        text = text.Trim();
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        text = Regex.Replace(text, "[\\s]{2,}", " ");    //two or more spaces
        text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", "\n");    //<br>
        text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", " ");    //&nbsp;
        text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);    //any other tags
        text = text.Replace("'", "");
        text = text.Replace("xp_cmdshell", "");
        text = text.Replace("exec master.dbo.xp_cmdshell", "");
        text = text.Replace("net localgroup administrators", "");
        text = text.Replace("delete from", "");
        text = text.Replace("net user", "");
        text = text.Replace("/add", "");
        text = text.Replace("drop table", "");
        text = text.Replace("update", "");
        return text;
    }

    public static DateTime GetUtcNowTime()
    {
        DateTime nowTime = DateTime.Now.AddHours(Convert.ToDouble(HttpContext.Current.Session["WTH"]));
        return nowTime;
    }

    public static string GetbyDT(string AdjustDate)
    {
        return Convert.ToDateTime(AdjustDate).AddHours(Convert.ToDouble(HttpContext.Current.Session["WTH"])).ToString("yyyy-MM-dd HH:mm:ss");
    }

    public static DateTime GetDTbyStr(string AdjustDate)
    {
        return Convert.ToDateTime(AdjustDate).AddHours(Convert.ToDouble(HttpContext.Current.Session["WTH"]));
    }

}
