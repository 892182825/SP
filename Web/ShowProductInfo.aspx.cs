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

public partial class ShowProductInfo : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
                    lblPrice.Text = pm.PreferentialPrice.ToString("0.00");
                    lblPPV.Text = pm.PreferentialPV.ToString("0.00");
                    lblPSpec.Text = SetParametersBLL.GetProductSpecInfoByID(pm.ProductSpecID).Rows[0]["ProductSpecName"].ToString();
                    div_PDetails.InnerHtml = pm.Description.Length>1?pm.Description:"无";
                    ProductImage.ImageUrl = "ReadImage.aspx?ProductID=" + ProductID;

                }
            }
        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //oT 0注册 1复消 lT 1公司 2店铺 3会员 ty 1购物车 2确认购物车
        if (Request["oT"]!=null && Request["rT"]!=null && Request["ty"]!=null)
        {
            string oT = Request["oT"].ToString();
            string lT = Request["rT"].ToString();
            string ty = Request["ty"].ToString();

            if (oT == "0" && lT == "1" && ty=="1")
            {
                Response.Redirect("~/Company/ShopingList.aspx");
            }
            else if (oT == "0" && lT == "1" && ty == "2")
            {
                Response.Redirect("~/Company/ShoppingCartView.aspx");
            }
            else if (oT == "0" && lT == "2" && ty == "1")
            {
                Response.Redirect("~/Store/ShopingList.aspx");
            }
            else if (oT == "0" && lT == "2" && ty == "2")
            {
                Response.Redirect("~/Store/ShoppingCartView.aspx");
            }
            else if (oT == "0" && lT == "3" && ty == "1")
            {
                Response.Redirect("~/Member/ShopingList.aspx");
            }
            else if (oT == "0" && lT == "3" && ty == "2")
            {
                Response.Redirect("~/Member/ShoppingCartView.aspx");
            }
            else if (oT == "1" && lT == "2" && ty == "1")
            {
                Response.Redirect("~/Store/ShopingListAgain.aspx");
            }
            else if (oT == "1" && lT == "2" && ty == "2")
            {
                Response.Redirect("~/Store/ShoppingCartViewAgain.aspx");
            }
            else if (oT == "1" && lT == "3" && ty == "1")
            {
                Response.Redirect("~/Member/ShopingListAgain.aspx");
            }
            else if (oT == "1" && lT == "3" && ty == "2")
            {
                Response.Redirect("~/Member/ShoppingCartViewAgain.aspx");
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

            string proId=Request.QueryString["ID"].ToString();

            string strRes = "";

            if (oT=="0")
            {
                strRes = new AjaxMemShopCart().GetShopCartStr(proId, "1", "0");

                if (strRes != "" && strRes != "添加失败！")
                {
                    if (lT=="1")
                    {
                        ScriptHelper.SetAlert(Page, "加入购物车成功！", "Company/ShoppingCartView.aspx");
                    }
                    else if (lT=="2")
                    {
                        ScriptHelper.SetAlert(Page, "加入购物车成功！", "Store/ShoppingCartView.aspx");
                    }
                    else
                    {
                        ScriptHelper.SetAlert(Page, "加入购物车成功！", "Member/ShoppingCartView.aspx");
                    }
                    
                }

                //if (lT=="1")
                //{
                    
                //}
                //else if (lT=="2")
                //{
                    
                //}
                //else
                //{

                //}
            }
            else
            {
                strRes = new AjaxMemShopCart().GetShopCartStrFx(proId, "1", "1");

                if (strRes != "" && strRes != "添加失败！")
                {
                    if (lT == "1")
                    {
                        ScriptHelper.SetAlert(Page, "加入购物车成功！", "Company/ShoppingCartViewAgain.aspx");
                    }
                    else if (lT == "2")
                    {
                        ScriptHelper.SetAlert(Page, "加入购物车成功！", "Store/ShoppingCartViewAgain.aspx");
                    }
                    else
                    {
                        ScriptHelper.SetAlert(Page, "加入购物车成功！", "Member/ShoppingCartViewAgain.aspx");
                    }

                }
            }

            
        }
    }
}
