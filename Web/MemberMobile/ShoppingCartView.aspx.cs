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
using System.Collections.Generic;
using Model;
using DAL;
using BLL.Registration_declarations;
using BLL.CommonClass;

public partial class Member_ShoppingCartView : BLL.TranslationBase
{
    public string number;
    public int bzCurrency = 0;
    public double i;
    protected void Page_Load(object sender, EventArgs e)
    {

          //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
        //i=AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()) );
        //Permissions.ThreeRedirect(Page, "../member/" + Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxMemShopCart));
        
        if (!IsPostBack)
        {
            //bzCurrency =int.Parse( Session["Default_Currency"].ToString());
            if (Request.QueryString["orderid"] != null)
            {
                BindOldOrder(Request.QueryString["orderid"].ToString());
            }

            if (Session["UserType"] != null && Session["UserType"].ToString() != "3")
            {
                if (Session["UserType"].ToString() == "2")
                {
                    
                }
                else
                {
                    
                }


            }
            else
            {
                if (HttpContext.Current.Session["Member"] != null)
                {
                    Session["OrderType"] = 12;
                    Session["LUOrder"] = GetMemBh() + ",12";
                }
                 
            }
            BindData();
            Button2.Text = GetTran("007408", "立即购买");
           
        }
    }

    public void BindOldOrder(string orderid)
    {
        MemberOrderModel model = MemberOrderBLL.GetMemberOrder(orderid);
        Session["LUOrder"] = model.Number + "," + model.OrderType;
        Session["OrderType"] = model.OrderType;

        MemberInfoModel infomodel = MemberInfoDAL.getMemberInfo(model.Number);
        infomodel.StoreID = model.StoreId;
        Session["fxMemberModel"] = infomodel;

        Session["EditOrderID"] = orderid;
        DAL.DBHelper.ExecuteNonQuery("delete from MemShopCart where memBh='" + model.Number + "' and mType=" + Session["UserType"].ToString());
        DBHelper.ExecuteNonQuery("insert into MemShopCart select productid,quantity,number," + Session["UserType"].ToString() + ",getutcdate()," + model.OrderType + " from MemberDetails where orderid='" + orderid + "'");

    }
    public string GetMemBh()
    {
        if (Session["OrderType"] != null)
        {
            if (Session["OrderType"].ToString() == "21" || Session["OrderType"].ToString() == "11" || Session["OrderType"].ToString() == "31")
            {
                if (Session["mbreginfo"] != null)
                    return ((MemberInfoModel)Session["mbreginfo"]).Number;
                else if (Session["fxMemberModel"] != null)
                    return ((MemberInfoModel)Session["fxMemberModel"]).Number;
                else if (Session["LUOrder"] != null)
                {
                    string[] str = Session["LUOrder"].ToString().Split(',');
                    return str[0];
                }
            }
            else
            {
                if (Session["Member"] != null)
                    return Session["Member"].ToString();
                else
                {
                    if (Session["LUOrder"] != null)
                    {
                        string[] str = Session["LUOrder"].ToString().Split(',');
                        return str[0];
                    }
                }
            }
        }
        return Session["Member"].ToString();

    }
  
    private void BindData()
    {
        string sql="";
        if (bzCurrency == 1)
        {
             sql = "select productcode,ProductID,ProductName,PreferentialPrice  ,PreferentialPV ,ProductImage,PreferentialPrice*proNum as totalPrice,PreferentialPV*proNum as totalPv,proNum from MemShopCart,Product where MemShopCart.proId=Product.productId and mType=" + Session["UserType"].ToString() + " and odType=" + Session["LUOrder"].ToString().Split(',')[1] + " and memBh='" + GetMemBh() + "'";
        }
        else
        {
             sql = "select productcode,ProductID,ProductName,PreferentialPrice *(select rate from Currency where ID=2) as PreferentialPrice,PreferentialPV *(select rate from Currency where ID=2) as PreferentialPV,ProductImage,PreferentialPrice*proNum as totalPrice,PreferentialPV*proNum as totalPv,proNum from MemShopCart,Product where MemShopCart.proId=Product.productId and mType=" + Session["UserType"].ToString() + " and odType=" + Session["LUOrder"].ToString().Split(',')[1] + " and memBh='" + GetMemBh() + "'";
        } 
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
        if (dt.Rows.Count == 0)
        {
            if (Session["UserType"].ToString() != "1")
            {
                //ScriptHelper.SetAlert(Page, GetTran("007430", "您至少要选择一种产品")+"!", "ShopingList.aspx?type=" + Request["type"]);
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#tiaoz').show();$('#gb').hide();document.getElementById('tiaoz').href = 'ShopingList.aspx?type=" + Request["type"] + "'; alertt('您至少要选择一种产品！');</script>", false);

            }
        }
        else
        {
            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            sql = "select isnull(sum(PreferentialPrice*proNum),0) as TotalPriceAll,isnull(sum(PreferentialPV*proNum),0) as TotalPvAll,isnull(sum(proNum),0) as totalNum from MemShopCart,Product where MemShopCart.proId=Product.productId  and mType=" + Session["UserType"].ToString() + " and odType=" + Session["LUOrder"].ToString().Split(',')[1] + " and memBh='" + GetMemBh() + "'";

            DataTable dt2 = DAL.DBHelper.ExecuteDataTable(sql);
            double currency=1;
            if (dt2.Rows.Count > 0)
            {
                ltNum.Text = Convert.ToInt32(dt2.Rows[0]["totalNum"]).ToString();
                //number = Convert.ToInt32(dt2.Rows[0]["totalNum"]).ToString();
                ltPrice.Text = (double.Parse(dt2.Rows[0]["TotalPriceAll"].ToString()) * currency).ToString("f2");
                ltPv.Text = dt2.Rows[0]["TotalPvAll"].ToString();

                //ltZp.Text = BLL.other.Company.GiveProductBLL.GetTableZp(Convert.ToDouble(ltPv.Text));
            }
        }
    }
    //获取商品数量
    //private string GetCount()
    //{
    //    string sql = "select isnull(sum(PreferentialPrice*proNum),0) as TotalPriceAll,isnull(sum(PreferentialPV*proNum),0) as TotalPvAll,isnull(sum(proNum),0) as totalNum from MemShopCart,Product where MemShopCart.proId=Product.productId  and mType=" + Session["UserType"].ToString() + " and odType=" + Session["LUOrder"].ToString().Split(',')[1] + " and memBh='" + GetMemBh() + "'";
       
    //    DataTable dt2 = DAL.DBHelper.ExecuteDataTable(sql);

    //    number = Convert.ToInt32(dt2.Rows[0]["totalNum"]).ToString();
    //    return number.ToString();
    //}

    protected string FormatURL(object strArgument)
    {
        string result = "../ReadImage.aspx?ProductID=" + strArgument.ToString();
        if (result == "" || result == null)
        {
            result = "";
        }
        return result;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string sql = "delete from MemShopCart where memBh='" + GetMemBh() + "' and mType=" + Session["UserType"].ToString() + " and odType=" + Session["LUOrder"].ToString().Split(',')[1];

        int i = DAL.DBHelper.ExecuteNonQuery(sql);

        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#tiaoz').show();$('#gb').hide();document.getElementById('tiaoz').href = 'ShopingList.aspx?type=" + Request["type"] + "'; alertt('购物车已清空！');</script>", false);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string count = DAL.DBHelper.ExecuteScalar("select count(*) from MemShopCart where mType=" + Session["UserType"].ToString() + " and odType=" + Session["LUOrder"].ToString().Split(',')[1] + " and memBh='" + GetMemBh() + "'").ToString();
        if (count == "0")
        {
            if (Session["UserType"].ToString() != "1")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#tiaoz').show();$('#gb').hide();document.getElementById('tiaoz').href = 'ShopingList.aspx?type=" + Request["type"] + "'; alertt('您至少要选择一种产品！');</script>", false);
            }
            else
            {
                //Response.Redirect("AddLsOrder.aspx?type=" + Request["type"]);
                Response.Redirect("AddLsOrderNew.aspx?type=" + Request["type"]);
            }
        }
        else
        {
            //Response.Redirect("AddLsOrder.aspx?type=" + Request["type"]);
            Response.Redirect("AddLsOrderNew.aspx?type=" + Request["type"]);
        }
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShopingList.aspx?type=" + Request["type"]);
    }
}