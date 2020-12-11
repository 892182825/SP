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
using DAL;

public partial class MemberMobile_BaodanZfXx : BLL.TranslationBase
{
      public int bzCurrency = 0;
 
    protected void Page_Load(object sender, EventArgs e)
      {
        //获取标准币种
          bzCurrency = CommonDataBLL.GetStandard();
        if (!IsPostBack)
        {
            
            var OrderID = Request.QueryString["OrderID"];
            string dt_one = "select b.OrderID as OrderID ,* from memberinfo as a join memberorder as b on a.Number=b.Number where b.OrderID='" + OrderID + "'";
            DataTable dt = DAL.DBHelper.ExecuteDataTable(dt_one);
            rep_km.DataSource = dt;
            rep_km.DataBind();



            string sql = "select o.DefrayState , m.productid,m.orderid,m.storeid,m.Quantity,m.Price,m.Pv,m.Quantity*m.Price as totalmoney,p.ProductName,ptype.ProductTypeName" +
                              " from memberorder o, memberdetails m,product  p,producttype ptype " +
                              " where m.ProductID=p.ProductID and p.producttypeid=ptype.producttypeid and m.orderid=o.OrderID and m.orderid='" + OrderID + "'";
            DataTable dt1 = DAL.DBHelper.ExecuteDataTable(sql);
            rep_km1.DataSource = dt1;
            rep_km1.DataBind();
            Translations();
        }
        
    }

    private void Translations()
    {
        this.TranControls(this.rep_km, new string[][] { new string[] { "009092", "去支付" } });
    }

    public string GetPayStatus(string paytype)
    {
        string payStatus = new GroupRegisterBLL().GetDeftrayState(paytype);
        return payStatus;
    }
    protected void linkbtnOK_Click(object sender, CommandEventArgs e)
    {

        if (MemberOrderDAL.Getvalidteiscanpay(e.CommandArgument.ToString(), Session["Member"].ToString()))//限制订单必须有订货所属店铺推荐人协助人支付)
        {

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("007452", "该订单不属于您的协助或推荐报单，不能完成支付！") + "'); window.location.href='../Logout.aspx'; </script>");

            return;
        }
        //ScriptManager.RegisterStartupScript(this, GetType(), "mag", "var formobj=document.createElement('form');"
        //                        + "formobj.action='../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(e.CommandArgument.ToString(), 1, 1) + "';" +
        //                        "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();", true);
        Response.Redirect("../payserver/chosepaysj.aspx?blif=" + EncryKey.GetEncryptstr(e.CommandArgument.ToString(), 1, 1));

        //Response.Redirect("chosepay.aspx?rd=" + e.CommandArgument.ToString() + "&rt=1");
    }


    protected object GetOrderType(object obj)
    {
        if (obj.ToString() == "0")
        {
            return GetTran("000221", "无");
        }
        else if (obj.ToString() == "1")
        {
            return GetTran("000000", "店铺支付");
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
            default: strtype = ""; break;
        }
        return strtype;
    }

    //protected void rep_km_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        ImageButton b = e.Item.FindControl("PayMent") as ImageButton;
            

            

    //    }


    //}
    protected void rep_km_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Button ib = e.Item.FindControl("PayMent") as Button;
            ib.Text = GetTran("009092", "去支付");
        }
    }
}