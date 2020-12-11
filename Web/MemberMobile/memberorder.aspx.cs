using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using BLL.Registration_declarations;
using BLL.CommonClass;
using BLL.other;
using Standard.Classes;
using DAL;
using System.Text;

public partial class MemberMobile_memberorder : BLL.TranslationBase
{

    public int maxExpect = CommonDataBLL.GetMaxqishu();
    public int bzCurrency = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);

        //获取标准币种
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        bzCurrency = CommonDataBLL.GetStandard();
        if (!this.IsPostBack)
        {
            if (Session["Default_Currency"] == null) Session["Default_Currency"] = bzCurrency;
            CommonDataBLL.DelOrderByState();

            
            GetShopList();
        }
        
    }

    public string GetSendWay(string sendWay)
    {
        if (sendWay == "0")
        {
            return GetTran("007103", "公司发货到店铺");
        }
        return GetTran("007104", "公司直接发货给会员");

    }

   

    protected object GetOrderType(object obj)
    {
        if (obj.ToString() == "0")
        {
            return GetTran("000000", "无");
        }
        else if (obj.ToString() == "1")
        {
            return GetTran("000000", "店铺支付");
        }
        else if (obj.ToString() == "2")
        {
            return GetTran("000000", "电子货币支付");
        }
        else if (obj.ToString() == "3")
        {
            return GetTran("000000", "离线支付");
        }
        else if (obj.ToString() == "4")
        {
            return GetTran("000000", "在线支付");
        }
        else if (obj.ToString() == "5")
        {
            return GetTran("000000", "周转款订货");
        }
        else if (obj.ToString() == "6")
        {
            return GetTran("000000", "订货款订货");
        }
        else if (obj.ToString() == "7")
        {
            return GetTran("000000", "离线支付");
        }
        else if (obj.ToString() == "8")
        {
            return GetTran("000000", "在线支付");
        }
        else
        {
            return "无";
        }
    }


    public string GetPayStatus(string paytype)
    {
        string payStatus = new GroupRegisterBLL().GetDeftrayState(paytype);
        return payStatus;
    }

    /// <summary>
    /// 转换日期
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    protected object GetOrderDate(object obj)
    {
        try
        {
            obj = Convert.ToDateTime(obj).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours());
        }
        catch { }
        return obj;
    }

    private void GetShopList()
    {

        

    }

    

    public string GetCompany(string paytype)
    {
        string payStatus = "";
        switch (paytype)
        {
            case "0":
                payStatus = "<font color=red>" + GetTran("005634", "未收款") + "</font>";
                break;
            case "1":
                payStatus = GetTran("005636", "已收款");
                break;
            default:
                payStatus = "<font color=red>" + GetTran("005634", "未收款") + "</font>";
                break;
        }
        return payStatus;
    }

    public string GetStore(string paytype)
    {
        string payStatus = "";
        switch (paytype)
        {
            case "0":
                payStatus = "<font color=red>" + GetTran("005634", "未收款") + "</font>";
                break;
            case "1":
                payStatus = GetTran("005636", "已收款");
                break;
            default:
                payStatus = "<font color=red>" + GetTran("005634", "未收款") + "</font>";
                break;
        }
        return payStatus;
    }

    protected string GetOrdertype(string ordertype)
    {
        string strtype = "";
        switch (ordertype)
        {
            case "0": strtype = GetTran("000555", "店铺注册"); break;
            case "1": strtype = GetTran("001445", "店铺复消"); break;
            case "2": strtype = GetTran("001448", "网上购物"); break;
            case "3": strtype = GetTran("001449", "自由注册"); break;
            case "4": strtype = GetTran("001452", "特殊注册"); break;
            case "5": strtype = GetTran("001454", "特殊报单"); break;
            case "6": strtype = GetTran("001455", "手机注册"); break;
            case "7": strtype = GetTran("001457", "手机报单"); break;
            default: strtype = ""; break;
        }
        return strtype;
    }

   
 
   
   
   
}
