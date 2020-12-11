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


/// <summary>
/// Add Namespace
/// </summary>
using Model.Other;
using BLL.other.Company;
using BLL.CommonClass;
using System.Text.RegularExpressions;

public partial class Company_ProductWareHouseDetails : BLL.TranslationBase
{
    protected string productCode = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        // 权限设置
        Response.Cache.SetExpires(DateTime.Now);
      
        if (Request.Params["Flag"] == "3")
        {
          
           Permissions.CheckManagePermission(EnumCompanyPermission.ReportStockReport3);
        }

        else
        {
           
           Permissions.CheckManagePermission(EnumCompanyPermission.ReportStockReport4);
        }
 
        if (!this.IsPostBack)
        {
            if (Request.Params["Flag"] == "1")
            {
                this.btnStore.Visible = false;
                this.btnProductStoreDetails.Visible = false;
                this.btnProductWareHouseDetails.Visible = false;
                this.txtCondition.Visible = false;
                DataBindWareHouse();
                this.lblCondition.Text = GetTran("000253", "仓库编号");
                this.lblTitle.Text = GetTran("000256", "仓库产品明细表");
            }
            if (Request.Params["Flag"] == "2")
            {
                this.btnStock.Visible = false;
                this.btnProductStoreDetails.Visible = false;
                this.ddlWareHouse.Visible = false;
                this.btnProductWareHouseDetails.Visible = false;
                this.lblCondition.Text = GetTran("000150", "店铺编号");
                this.lblTitle.Text = GetTran("000270", "店铺产品明细表");
            }
            if (Request.Params["Flag"] == "3")
            {
                this.btnProductWareHouseDetails.Visible = true;
                this.btnProductStoreDetails.Visible = false;
                this.ddlWareHouse.Visible = false;
                this.btnStock.Visible = false;
                this.btnStore.Visible = false;
                this.btnImage.Visible = false;
                this.lblCondition.Text = GetTran("000263", "产品编码");
                this.lblTitle.Text = GetTran("000271", "产品仓库明细表"); 
            }
            if (Request.Params["Flag"] == "4")
            {
                this.btnProductWareHouseDetails.Visible = false;
                this.btnProductStoreDetails.Visible = true;
                this.ddlWareHouse.Visible = false;
                this.btnStock.Visible = false;
                this.btnStore.Visible = false;
                this.btnImage.Visible = false;
                this.lblCondition.Text = GetTran("000263", "产品编码");
                this.lblTitle.Text = GetTran("000275", "产品店铺明细表");
            }
        }
        Translations_More();

    }

    /// <summary>
    /// Transmission
    /// </summary>
    protected void Translations_More()
    {
        TranControls(btnStore, new string[][] { new string[] { "000154", "按服务机构汇总" } });
        TranControls(btnStock, new string[][] { new string[] { "000285", "仓库汇总" } });
        TranControls(btnProductWareHouseDetails, new string[][] { new string[] { "000290", "产品仓库明细" } });
        TranControls(btnProductStoreDetails, new string[][] { new string[] { "000295", "产品店铺明细" } });
        TranControls(btnImage, new string[][] { new string[] { "000298", "图形分析" } });
    }

    /// <summary>
    /// 绑定仓库信息
    /// </summary>
    private void DataBindWareHouse()
    {
        ///通过管理员编号获取仓库相应的权限
        DataTable dt = StorageInBLL.GetMoreManagerPermissionByNumber(Session["Company"].ToString());

        ddlWareHouse.DataTextField = "WareHouseName";
        ddlWareHouse.DataValueField = "WareHouseID";
        ddlWareHouse.DataSource = dt;
        ddlWareHouse.DataBind();
    }

    /// <summary>
    /// 按服务机构汇总库存查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnStore_Click(object sender, EventArgs e)
    {
        if (this.txtCondition.Text.Length < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000133", "请输入要查询的店铺编号!")));
        }

        else
        {
            Session["Condition"] = txtCondition.Text.Trim(); ;

            //Response.Write("<script language='javascript'>window.open('ProductStock.aspx?Flag=Store')</script>");
            Response.Redirect("ProductStock.aspx?Flag=Store", true);
        }
    }

    /// <summary>
    /// 仓库汇总库存查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void  btnStock_Click(object sender, EventArgs e)
    {
        Session["WareHouseID"] = this.ddlWareHouse.SelectedValue;

        //Response.Write("<script language='javascript'>window.open('ProductStock.aspx?Flag=WareHouse')</script>");
        Response.Redirect("ProductStock.aspx?Flag=WareHouse", true);
    }

    /// <summary>
    /// Judge ProductCode whether exists
    /// </summary>
    protected bool ProductCodeIsExist(ref string productCode)
    {
        bool flag = true;
        string notexistCode = string.Empty;
        if (this.txtCondition.Text.Length < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000301", "请输入产品编码!")));
            return false;
        }
        else
        {
            Regex rx = new Regex(@"[；|;]", RegexOptions.IgnoreCase);
            this.txtCondition.Text = rx.Replace(this.txtCondition.Text, ";");       //将中文分号转换成英文分号
            productCode = this.txtCondition.Text.Trim();
            Regex rx0 = new Regex(@"\w|;]+", RegexOptions.IgnoreCase);
            if (!rx0.IsMatch(productCode))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("多编号查询请用分号隔开！"));
                return false;

            }
            //Judge ProductCode whether exists
            string[] productCodes = rx.Split(productCode);
            foreach (string code in productCodes)
            {
                int getCount = 0;
                getCount = ProductWareHouseDetailsBLL.GetProductCodeCountByProductCode(code);
                if (getCount < 1)
                {
                    flag = false;
                    notexistCode += code + ",";
                }
            }
        }
        if (!flag)
        {
            if (notexistCode.IndexOf(",") > 0)
                notexistCode = notexistCode.Substring(0, notexistCode.Length - 1);
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000304", "对不起，该产品编码不存在!") + "[" + notexistCode + "]"));
        }
        return flag;     
    }

    /// <summary>
    /// The details of the product of the warehouse
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnProductWareHouseDetails_Click(object sender, EventArgs e)
    {
       // string codes = ProductCodeIsExist();
        //if (codes.Length > 0)
        string codes=string .Empty ;
        if(ProductCodeIsExist(ref codes))
        {
            Session["ProductCode"] = codes;
            //Response.Write("<script language='javascript'>window.open('ProductStockDetail.aspx?Flag=Company')</script>");
            Response.Redirect("ProductStockDetail.aspx?Flag=Company", true);
        }              
    }

    /// <summary>
    /// 店铺产品库存明细查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnProductStoreDetails_Click(object sender, EventArgs e)
    {
        // string codes = ProductCodeIsExist();
        //if (codes.Length > 0)
        string codes = string.Empty;
        if (ProductCodeIsExist(ref codes))
        {
            Session["ProductCode"] = codes;
            //Response.Write("<script language='javascript'>window.open('ProductStockDetail.aspx?Flag=Store')</script>");
            Response.Redirect("ProductStockDetail.aspx?Flag=Store", true);
        }     
    }

    /// <summary>
    /// 图形分析
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnImage_Click(object sender, EventArgs e)
    {
        if (this.btnStock.Visible)
        {
            //Response.Write("<script language='javascript'>window.open('StockGraph.aspx?Flag=WareHouse')</script>");
            Response.Redirect("StockGraph.aspx?Flag=WareHouse", true);
        }
        if (this.btnStore.Visible)
        {
            Session["Condition"] = this.txtCondition.Text.Trim();

            //Response.Write("<script language='javascript'>window.open('StoreStockGraph.aspx?Flag=Store')</script>");
            Response.Redirect("StoreStockGraph.aspx?Flag=Store", true);
        }
    }
}
