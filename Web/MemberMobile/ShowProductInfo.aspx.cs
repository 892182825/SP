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

using BLL.other.Company;
using System.Collections.Generic;
using Model;
using BLL.CommonClass;

public partial class ShowProductInfo : BLL.TranslationBase
{
    public int count;
    public string msg;
    public int bzCurrency = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();
       // Permissions.ThreeRedirect(Page, "../Member/" + Permissions.redirUrl);
        if (!IsPostBack)
        {
            if (Session["UserType"].ToString() == "2")
            {
                top.Visible = false;
                //bottom.Visible = false;
                STop1.Visible = true;
                SLeft1.Visible = true;
            }
            else
            {
                top.Visible = true;
                //bottom.Visible = true;
                STop1.Visible = false;
                SLeft1.Visible = false;
            }
            string ProductID = "24";
            if (Request.QueryString["ID"] != null)
            {
                ProductID = Request.QueryString["ID"].ToString();
            }

            IList<ProductModel> lpm = AddNewProductBLL.GetAllProductInfoByID(int.Parse(ProductID));
            if (lpm.Count > 0)
            {
                foreach (ProductModel pm in lpm)
                {
                    lblPCode.Text = pm.ProductCode.ToString();
                    lblPName.Text = pm.ProductName;
                    lblPType.Text = ProductModeBLL.GetProductNameByID(pm.PID);
                    lblPrice.Text = pm.PreferentialPrice.ToString("0.00");// ((pm.PreferentialPrice) * (Convert.ToDecimal(AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))))).ToString("0.00");
                    //lblPPV.Text = pm.PreferentialPV.ToString("0.00");

                    DataTable dt_gg = SetParametersBLL.GetProductSpecInfoByID(pm.ProductSpecID);
                    if (dt_gg != null && dt_gg.Rows.Count > 0)
                        lblPSpec.Text = dt_gg.Rows[0]["ProductSpecName"].ToString();

                    div_PDetails.InnerHtml = pm.Details_TX.Length > 1 ? pm.Details_TX : "无";
                    ProductImage.ImageUrl = "../ReadImage.aspx?ProductID=" + ProductID;
                }
            }
            GetCount();
            Translations();
        }

    }
    private void Translations()
    {
        //this.TranControls(this.LinkButton2,
        //        new string[][]{
        //                new string []{"007377","加入购物车"},
                    
      
        //            });
        //this.TranControls(this.LinkButton1,
        //    new string[][]{
        //                new string []{"009126","返回"},
                       
      
        //            });
      
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
    public int GetCount()
    {
        string sql = "select isnull(sum(PreferentialPrice*proNum),0) as TotalPriceAll,isnull(sum(PreferentialPV*proNum),0) as TotalPvAll,isnull(sum(proNum),0) as totalNum from MemShopCart,Product where MemShopCart.proId=Product.productId  and mType=" + Session["UserType"].ToString() + " and odType=" + Session["LUOrder"].ToString().Split(',')[1] + " and memBh='" + GetMemBh() + "'";
          DataTable dt2 = DAL.DBHelper.ExecuteDataTable(sql);

          //if (dt2.Rows.Count > 0)
          //{
              count = Convert.ToInt32(dt2.Rows[0]["totalNum"]);
              return Convert.ToInt32(dt2.Rows[0]["totalNum"]);
          //}
            msg = "<script>show()</script>";

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //oT 0注册 1复消 lT 1公司 2店铺 3会员 ty 1购物车 2确认购物车
        if (Request["oT"] != null && Request["rT"] != null && Request["ty"] != null)
        {
            string oT = Request["oT"].ToString();
            string lT = Request["rT"].ToString();
            string ty = Request["ty"].ToString();

            if (ty == "1")
            {
                Response.Redirect("ShopingList.aspx?type=" + Request["type"]);
            }
            else
            {
                Response.Redirect("ShoppingCartView.aspx?type=" + Request["type"]);
            }
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        if (Request["oT"] != null && Request["rT"] != null && Request["ty"] != null)
        {
            string oT = Request["oT"].ToString();
            string lT = Request["rT"].ToString();
            string ty = Request["ty"].ToString();

            string proId = Request.QueryString["ID"].ToString();

            string strRes = "";

            if (oT == "0")
            {
                strRes = new AjaxMemShopCart().GetShopCartStr(proId, "1", "0");

                if (strRes != "" && strRes != "添加失败！")
                {
                    if (lT == "1")
                    {
                        //ScriptHelper.SetAlert(Page, "加入购物车成功！", "ShowProductInfo.aspx?oT=" + oT + "&rt=" + lT + "&ty=" + ty + "&ID=" + proId + "&type=" + Request["type"]);
                       // ScriptHelper.SetAlert(Page, GetTran("009080", "加入购物车成功"), "ShoppingCartView.aspx?type=" + Request["type"]);
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#tiaoz').show();$('#gb').hide();document.getElementById('tiaoz').href = 'ShoppingCartView.aspx?type=" + Request["type"] + "'; alertt('加入购物车成功！');</script>", false);

                    }
                    else if (lT == "2")
                    {
                        //ScriptHelper.SetAlert(Page, "加入购物车成功！", "ShowProductInfo.aspx?oT=" + oT + "&rt=" + lT + "&ty=" + ty + "&ID=" + proId + "&type=" + Request["type"]);
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#tiaoz').show();$('#gb').hide();document.getElementById('tiaoz').href = 'ShoppingCartView.aspx?type=" + Request["type"] + "'; alertt('加入购物车成功！');</script>", false);
                    }
                    else
                    {
                        //ScriptHelper.SetAlert(Page, "加入购物车成功！", "ShowProductInfo.aspx?oT=" + oT + "&rt=" + lT + "&ty=" + ty + "&ID=" + proId + "&type=" + Request["type"]);
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#tiaoz').show();$('#gb').hide();document.getElementById('tiaoz').href = 'ShoppingCartView.aspx?type=" + Request["type"] + "'; alertt('加入购物车成功！');</script>", false);
                    }
                }
            }
            else
            {
                strRes = new AjaxMemShopCart().GetShopCartStr(proId, "1", "1");

                if (strRes != "" && strRes != "添加失败！")
                {
                    if (lT == "1")
                    {
                        //ScriptHelper.SetAlert(Page, "加入购物车成功！", "ShowProductInfo.aspx?oT=" + oT + "&rt=" + lT + "&ty=" + ty + "&ID=" + proId + "&type=" + Request["type"]);
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#tiaoz').show();$('#gb').hide();document.getElementById('tiaoz').href = 'ShoppingCartView.aspx?type=" + Request["type"] + "'; alertt('加入购物车成功！');</script>", false);
                        //msg = "<script>gwc();</script>";
                    }
                    else if (lT == "2")
                    {
                        //ScriptHelper.SetAlert(Page, "加入购物车成功！", "ShowProductInfo.aspx?oT=" + oT + "&rt=" + lT + "&ty=" + ty + "&ID=" + proId + "&type=" + Request["type"]);
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#tiaoz').show();$('#gb').hide();document.getElementById('tiaoz').href = 'ShoppingCartView.aspx?type=" + Request["type"] + "'; alertt('加入购物车成功！');</script>", false);
                        //msg = "<script>gwc();</script>";;
                    }
                    else
                    {
                        //ScriptHelper.SetAlert(Page, "加入购物车成功！", "ShowProductInfo.aspx?oT=" + oT + "&rt=" + lT + "&ty=" + ty + "&ID=" + proId + "&type=" + Request["type"]);
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#tiaoz').show();$('#gb').hide();document.getElementById('tiaoz').href = 'ShoppingCartView.aspx?type=" + Request["type"] + "'; alertt('加入购物车成功！');</script>", false);
                        //msg = "<script>gwc();</script>";
                    }
                }
            }
        }
    }
}