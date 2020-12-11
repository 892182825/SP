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
using System.Data.SqlClient;
using BLL.CommonClass;
using BLL.Registration_declarations;
using System.Collections.Generic;
using Model;
using DAL;
using Encryption;

public partial class Member_BrowseMemberOrders : BLL.TranslationBase
{

    public string id;
    public int bzCurrency = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
         bzCurrency = CommonDataBLL.GetStandard();
        id = Request.QueryString["id"];
        string sql = " select * from MemberInfo as A,MemberOrder as B where  B.Number=A.Number and A.ID='" + id + "'";                                                                
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
        rep.DataSource = dt;
        rep.DataBind();
    }

    public string GetRegisterDate(string rdate)
    {
        return Convert.ToDateTime(rdate).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();
    }

    public string GetPayStatus(string paytype)
    {
        string payStatus = new GroupRegisterBLL().GetDeftrayState(paytype);
        return payStatus;
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
}
