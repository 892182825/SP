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
using BLL.CommonClass;
using BLL.other.Company;

public partial class Company_SetAgreement2 : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {

            BindCountry();
            BindLanguage();
            int CountryCode = int.Parse(ddlCountry.SelectedValue);
            string LanguageCode = ddlLanguage.SelectedValue;
            BindTextBox(CountryCode, LanguageCode);
        }
        Translations_More();
    }

    private void BindTextBox(int CountryCode, string LanguageCode)
    {
        TextBox1.Text = DAL.CommonDataDAL.GetAgreement(CountryCode, LanguageCode, 1);// CommonDataBLL.GetAgreement(CountryCode, LanguageCode); 
    }
    private void BindCountry()
    {
        ddlCountry.DataSource = StoreInfoEditBLL.bindCountry();
        ddlCountry.DataTextField = "Name";
        ddlCountry.DataValueField = "countrycode";
        ddlCountry.DataBind();

    }

    private void BindLanguage()
    {
        ddlLanguage.DataSource = SetRateBLL.GetAllLanguageIDName();
        ddlLanguage.DataTextField = "Name";
        ddlLanguage.DataValueField = "LanguageCode";
        ddlLanguage.DataBind();
        if (Session["languageCode"] != null)
        {
            foreach (ListItem li in ddlLanguage.Items)
            {
                if (li.Value == Session["languageCode"].ToString())
                {
                    li.Selected = true;
                    break;
                }
            }
        }
    }

    protected void Translations_More()
    {
        TranControls(btn_Cancle, new string[][] 
                        {
                          
                            new string[] { "000839","取 消"} 
                        }
            );
    }


    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string Agreement = TextBox1.Text;
        string LanguageCode = ddlLanguage.SelectedValue;
        int CountryCode = int.Parse(ddlCountry.SelectedValue);
        if (Agreement.Length <= 0)
        {
            Response.Write("<script>alert('" + GetTran("007163", "注册协议内容不能为空") + "!');</script>");
            return;
        }
        if (CommonDataBLL.RegisterAgreement(Agreement, CountryCode, LanguageCode, 1) == 1)
        {
            Response.Write("<script>alert('" + this.GetTran("001615", "更新成功") + "！');</script>");
        }
        else
        {
            Response.Write("<script>alert('" + this.GetTran("001616", "更新失败") + "！');</script>");
        }
    }

    protected void btn_Cancle_Click(object sender, EventArgs e)
    {
        BindTextBox(int.Parse(ddlCountry.SelectedValue), ddlLanguage.SelectedValue);
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTextBox(int.Parse(ddlCountry.SelectedValue), ddlLanguage.SelectedValue);
    }

    protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindTextBox(int.Parse(ddlCountry.SelectedValue), ddlLanguage.SelectedValue);
    }
}
