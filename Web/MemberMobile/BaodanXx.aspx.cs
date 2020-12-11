using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Model;
using BLL.CommonClass;
using BLL.Registration_declarations;

public partial class MemberMobile_BaodanXx : BLL.TranslationBase
{
     public int bzCurrency = 0;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        var OrderID = Request.QueryString["OrderID"];
        string dt_one = "select b.OrderID as OrderID ,* from memberinfo as a join memberorder as b on a.Number=b.Number where b.OrderID='" + OrderID+"'";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
        rep_km.DataSource = dt;
        rep_km.DataBind();

        string sql = "select m.productid,m.orderid,m.storeid,m.Quantity,m.Price,m.Pv,m.Quantity*m.Price as totalmoney,p.ProductName,ptype.ProductTypeName" +
                          " from memberdetails m,product  p,producttype ptype " +
                          " where m.ProductID=p.ProductID and p.producttypeid=ptype.producttypeid and m.orderid='" + OrderID + "'";
        DataTable dt1 = DAL.DBHelper.ExecuteDataTable(sql);
        rep_km1.DataSource = dt1;
        rep_km1.DataBind();
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
            return GetTran("000221", "无");
        }
        else if (obj.ToString() == "1")
        {
            return GetTran("008180", "店铺支付");
        }
        else if (obj.ToString() == "2")
        {
            return GetTran("007444", "电子货币支付");
        }
        else if (obj.ToString() == "3")
        {
            return GetTran("007447", "离线支付");
        }
        else if (obj.ToString() == "4")
        {
            return GetTran("005963", "在线支付");
        }
        else if (obj.ToString() == "5")
        {
            return GetTran("000277", "周转款订货");
        }
        else if (obj.ToString() == "6")
        {
            return GetTran("007529", "订货款订货");
        }
        else if (obj.ToString() == "7")
        {
            return GetTran("007447", "离线支付");
        }
        else if (obj.ToString() == "8")
        {
            return GetTran("005963", "在线支付");
        }
        else
        {
            return GetTran("000221", "无");
        }
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
            case "24": strtype = GetTran("009123", "自动复消报单"); break;
            default: strtype = ""; break;
        }
        return strtype;
    }
}