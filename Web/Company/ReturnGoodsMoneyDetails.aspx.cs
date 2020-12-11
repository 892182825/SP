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

using BLL.MoneyFlows;
using Model.Other;
public partial class Company_ReturnGoodsMoneyDetails : BLL.TranslationBase
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceTuihuokuanManage);

        Response.Cache.SetExpires(DateTime.Now);

        if (!IsPostBack)
        {
            ViewState["Eurl"] = Request.UrlReferrer.ToString();
        }

        if (Request.QueryString["ID"] == null)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("002097", "错误参数") + "')</script>");
            return;
        }
        string id = Request.QueryString["ID"].ToString().Trim();
        bind(id);
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.gwdetails,
               new string[][]{
                    new string []{"002088","退货编号"},
                    new string []{"000501","产品名称"},
                    new string []{"001982", "退货数量"},
                    new string []{"000045", "期数"},
                    new string []{"002084","价格"},
                    //new string []{"000562","币种"},
                    new string []{"000414", "积分"}});
        this.TranControls(this.btnEHistory, new string[][] { new string[] { "000096", "返 回" } });
    }
    /// <summary>
    /// 获取产品名称
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public string getName(object obj)
    {
        return ReturnedGoodsMoneyBLL.GetProductName(obj.ToString().Trim());
    }
    /// <summary>
    /// 获得币种
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public string getMoney(object obj)
    {
        return ReturnedGoodsMoneyBLL.GetMoneyType(obj.ToString()); 
    }
    /// <summary>
    /// 绑定数据
    /// </summary>
    /// <param name="id">ID</param>
    private void bind(string id)
    {
        this.gwdetails.DataSource = ReturnedGoodsMoneyBLL.GetInventoryDoctails(id);
        this.gwdetails.DataBind();
    }

    protected void gwdetails_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void gwdetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }

    }

    protected void btnEHistory_Click(object sender, EventArgs e)
    {
        if (ViewState["Eurl"]!=null)
        {
            Response.Redirect(ViewState["Eurl"].ToString());
        }
    }
}
