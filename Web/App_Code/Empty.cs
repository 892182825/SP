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
using BLL;


/// <summary>
///Empty 的摘要说明
/// </summary>
public class Empty
{
    public Empty()
    {
        
    }

    public static string GetString(string str)
    {
        if (String.IsNullOrEmpty(str))
            return new TranslationBase().GetTran("000221");
        return str;
    }
}
