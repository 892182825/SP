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
using BLL.CommonClass;
using Model.Other;

public partial class Company_CombineProduct_I : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            Response.Cache.SetExpires(DateTime.Now);
            Permissions.CheckManagePermission(EnumCompanyPermission.StorageAdminCombineProduct);
            DataTable dt = ProductCombine.GetCountry();
            foreach (DataRow dr in dt.Rows)
            {
                ddlCountry.Items.Add(new ListItem(dr["name"].ToString(), dr["countrycode"].ToString()));
            }
            ddlCountry_SelectedIndexChanged(null, null);
        }
    }
    /// <summary>
    /// 绑定国家
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCombineProduct.Items.Clear();
        DataTable dt = ProductCombine.GetCombineProduct(ddlCountry.SelectedValue);
        foreach (DataRow dr in dt.Rows)
        {
            ddlCombineProduct.Items.Add(new ListItem(dr["productname"].ToString(), dr["productid"].ToString()));
        }
        ddlCombineProduct.Items.Insert(0, new ListItem(GetTran("000303", "请选择"), "0"));
        treeid.InnerHtml = new ProductTree().getMenuZhuHe(ddlCountry.SelectedValue);
    }
}
