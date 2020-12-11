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

/// <summary>
///SfType 的摘要说明
/// </summary>
public class SfType
{
    public SfType()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    public static string getType()
    {
        string sfType = "H";
        if (HttpContext.Current.Session["Store"] != null)
        {
            sfType = "D";
        }
        else if (HttpContext.Current.Session["Company"] != null)
        {
            sfType = "G";
        }
        return sfType;
    }
    /// <summary>
    /// 获取会员编号
    /// </summary>
    /// <returns></returns>
    public static string getBH()
    {
        string bh = "";
        switch (SfType.getType())
        {
            case "D":
                string storeid = HttpContext.Current.Session["Store"].ToString();
                bh = BLL.Registration_declarations.DetialQueryBLL.GetNumberByStoreID(storeid);
                break;
            case "G":
                bh = BLL.CommonClass.CommonDataBLL.getManageID(3);
                break;
            case "H":
                if (HttpContext.Current.Session["bh"] == null)
                    bh = HttpContext.Current.Session["Member"].ToString();
                else
                    bh = HttpContext.Current.Session["bh"].ToString();
                break;
        }

        return bh;
    }
}