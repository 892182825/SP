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

public partial class Company_OrderDetails : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            Response.Cache.SetExpires(DateTime.Now);
            Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.BalanceBrowseMemberOrders);
            BindData();

        }
        TransLation();
    }

    protected void BindData()
    {
        string strSotreId = "";
        string strOrderId = "";
        try
        {
            strSotreId = Request.QueryString["storeId"].ToString();
            strOrderId = Request.QueryString["orderId"].ToString();
        }
        catch (Exception)
        {
            Response.Redirect("BrowseMemberOrders.aspx");
        }
        gv_orderDetails.DataSource = BrowseMemberOrdersBLL.DeclarationProduct(strSotreId, strOrderId);
        gv_orderDetails.DataBind();

    }
    protected void gv_orderDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg;");
        }
    }



    public void TransLation() 
    {
        this.TranControls(this.gv_orderDetails, new string[][] 
        { 
            new string[]{"000079","订单号"},
            new string[]{"000150","店铺编号"},
            new string[]{"000501","产品名称"},
            new string[]{"000882","产品型号"},
            new string[]{"000505","数量"},
            new string[]{"000503","单价"},
            new string[]{"000414","积分"},
            new string[]{"000041","总金额"}
        });
    }
}
