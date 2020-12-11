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

//Add Namespace
using BLL.CommonClass;
using BLL.other.Company;
using Model.Other;

public partial class Company_BaodanDetailReport : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        ///产品销售明细
        Permissions.CheckManagePermission(EnumCompanyPermission.ReportBaodanDetailReport);
        
        if (!IsPostBack)
        {
            txtBeginTime.Text = CommonDataBLL.GetDateBegin();
            txtEndTime.Text = CommonDataBLL.GetDateEnd();
            Translations_More();
        }
    }

    /// <summary>
    /// Translation
    /// </summary>
    protected void Translations_More()
    {
        TranControls(btnSearch, new string[][] {new string[]{"000048", "查 询" }});
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (this.txtBeginTime.Text.Length < 1 || this.txtEndTime.Text.Length < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000465", "请输入要查询的日期区间!")));
            return;
        }

        else
        {
            try
            {
                DateTime d1 = Convert.ToDateTime(this.txtBeginTime.Text);
                DateTime d2 = Convert.ToDateTime(this.txtEndTime.Text);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000468", "日期格式不正确!")));
                return;
            }

            string storeId = txtStoreID.Text.Trim();
            //Judage the store whether exists beafore search the information about store by storeId
            int getCount = ReportBLL.StoreIdIsExistByStoreId(storeId);
            //When exist
            if (getCount > 0)
            {
                Session["Begin"] = this.txtBeginTime.Text;
                Session["End"] = this.txtEndTime.Text;
                Session["StoreID"] = storeId;
                //Response.Write("<script lanagage='javascript'>window.open('BaodanDetail.aspx?Flag=1')</script>");
                Response.Redirect("BaodanDetail.aspx?Flag=1",true);
            }

            //When no exist
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000142", "对不起，该店铺编号不存在!")));
                return;
            }          
        }
    }
}
