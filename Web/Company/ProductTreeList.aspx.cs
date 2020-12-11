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
using Model.Other;
using System.Text;
using Model;
using BLL;
using BLL.other.Company;
using BLL.CommonClass;

public partial class Company_ProductTreeList : BLL.TranslationBase
{
    //CountryModel countryModel = new CountryModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        ///检查相应权限      
        Permissions.CheckManagePermission(EnumCompanyPermission.StorageProductTreeManager);     

        if (!IsPostBack)
        {
            DataBindCountry();        
            ddlCountry_SelectedIndexChanged(null, null);            
        }
    }   

    /// <summary>
    /// 绑定国家编码和名称
    /// </summary>
    private void DataBindCountry()
    {
        DataTable dt = AddNewProductBLL.GetCountryIDCodeNameOrderByID();
        if (dt.Rows.Count<1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001959", "对不起，没有可选国家!")));
            return;
        }

        else
        {
            this.ddlCountry.DataSource = dt;
            this.ddlCountry.DataTextField = "Name";
            this.ddlCountry.DataValueField = "CountryCode";
            this.DataBind();
            //默认值为第一个
            this.ddlCountry.SelectedValue = "0";
        }
    }
   
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {        
        ProductTree pTree = new ProductTree();
        lblmenu.Text = pTree.GetProductTree(ddlCountry.SelectedValue);     
    } 
  
}
