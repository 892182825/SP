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
using BLL.other.Company;

/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-22
 * 功能：       绑定会员使用银行信息
 */

public partial class UserControl_MemberBank : System.Web.UI.UserControl
{
    protected int countryID =0;

    /// <summary>
    ///获取或设置银行的选中值	
    /// </summary>
    public string SelectedValue
    {
        get { return GetSelectedItemText(); }
        set { SetSelectedItem(value); }
    }
    public string SelectedItemValue
    {
        get { return GetSelectedValue(); }
        set { SetSelectedValue(value); }
    }

    /// <summary>
    /// 根据国家ID编号来确定后的银行个数
    /// </summary>
    public int Select_Get_Bank_Count
    {
        get { return Get_Bank_Count(); }

    }
    /// <summary>
    /// 国家ID编号
    /// </summary>
    public string CountryID
    {
        set
        {
            ///获取指定国家的会员使用银行信息
            DataTable table = SetParametersBLL.GetMemberBankInfoByCountryCode(int.Parse(value));               
            this.ddlBank.Items.Clear();
            this.ddlBank.DataTextField = "BankName";
            this.ddlBank.DataValueField = "BankID";
            this.ddlBank.DataSource = table;
            this.ddlBank.DataBind();
            countryID =Convert.ToInt32(value);
        }
    }

    public int SelectedIndex
    {
        get { return GetSelectedIndex(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable table;
            if (HttpContext.Current.Session["language"].ToString().ToLower() == "chinese" || HttpContext.Current.Session["language"].ToString().ToUpper() == "中文")
            {
                table = SetParametersBLL.GetMemberBankInfoByCountryCode(int.Parse(countryID.ToString()));
            }

            else
            {
                ///获取语言对应的ID
                int languageID = SetParametersBLL.GetLanguageIDByName(HttpContext.Current.Session["language"].ToString());
                ///通过联合查询获取银行信息
                table = SetParametersBLL.GetMoreMemberBankInfoByLanguageID(languageID);
            }           
            this.ddlBank.Items.Clear();
            this.ddlBank.DataTextField = "BankName";
            this.ddlBank.DataValueField = "BankID";
            this.ddlBank.DataSource = table;
            this.ddlBank.DataBind();          
        }
    }    
    
    /// <summary>
    /// 选中国家
    /// </summary>
    /// <param name="bankValue"></param>
    private void SetSelectedValue(string bankValue)
    {        
        ddlBank.SelectedIndex = -1;
        foreach (ListItem item in ddlBank.Items)
        {
            if (item.Value == bankValue)
            {
                item.Selected = true;
                break;
            }
        }
    }

    private int Get_Bank_Count()
    {
        return ddlBank.Items.Count;
    }


    private void SetSelectedItem(string bankText)
    {
        ddlBank.SelectedIndex = -1;
        foreach (ListItem item in ddlBank.Items)
        {
            if (item.Text == bankText)
            {
                item.Selected = true;
                break;
            }
        }
    }

    private string GetSelectedItemText()
    {
        return ddlBank.SelectedItem.Text;
    }
    private string GetSelectedValue()
    {
        return ddlBank.SelectedValue;
    }

    private int GetSelectedIndex()
    {
        return ddlBank.SelectedIndex;
    }
}
