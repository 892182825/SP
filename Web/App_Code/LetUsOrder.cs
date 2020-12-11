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
///LetUsOrder 的摘要说明
/// </summary>
public class LetUsOrder
{
    public LetUsOrder()       
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //

        
    }

    public void SetVlaue()
    {
        if (HttpContext.Current.Session["LUOrder"] != null)
        {
            string[] str = HttpContext.Current.Session["LUOrder"].ToString().Split(',');
            MemBh = str[0];
            OrderType = Convert.ToInt32(str[1]);
        }
        else
        {
            OrderType = 22;
            MemBh = HttpContext.Current.Session["Member"].ToString();

        }
    }

    public void DisposeValue()
    {
        if (HttpContext.Current.Session["LUOrder"] != null)
        {
            HttpContext.Current.Session.Remove("LUOrder");
        }
    }

    private int orderType;

    public int OrderType
    {
        get { return orderType; }
        set { orderType = value; }
    }

    private string memBh;

    public string MemBh
    {
        get { return memBh; }
        set { memBh = value; }
    }
}
