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

/*
 * Author:      WangHua
 * FinishDate:  2010-01-30
 * Function:    User control of country
 */

public partial class UserControl_Country : System.Web.UI.UserControl
{
    public delegate void userEvent(object sender, EventArgs e);

    public event userEvent CSelectedIndexChanged;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDownList();            
        }
    }

    public string CountryName
    {
        get { return this.ddlCountry.Items.Count == 0 ? "" : this.ddlCountry.SelectedItem.Text; }
        set { this.ddlCountry.SelectedItem.Text = value; }
    }

    /// <summary>
    /// 获取国家id
    /// </summary>
    public int CID
    {
        get { return this.ddlCountry.Items.Count == 0 ? -1 :Convert.ToInt32( this.ddlCountry.SelectedValue); }
        set { this.ddlCountry.SelectedValue = value.ToString(); }
    }

    /// <summary>
    /// 获得国家ID
    /// </summary>
    public string Country
    {
        get{ return this.ddlCountry.Items.Count==0 ? "" : this.ddlCountry.SelectedValue;}
        set{ this.ddlCountry.SelectedValue = value;}
    }

    /// <summary>
    ///获取或设置证件的选中值	
    /// </summary>
    public string SelectedValue
    {
        get { return GetSelectedValue(); }
        set { SetSelectedItemValue(value); }
    }

    /// <summary>
    ///获取或设置证件的选中值	
    /// </summary>
    public string SelectedItemText
    {
        get { return GetSelectedItemText(); }
        set { SetSelectedItemText(value); }
    }

    private void SetSelectedItemValue(string strValue)
    {
        BindDropDownList();
        ddlCountry.SelectedIndex = -1;        
        foreach (ListItem item in ddlCountry.Items)
        {
            if (item.Value == strValue)
            {
                item.Selected = true;
                break;
            }
        }
    }

    private void SetSelectedItemText(string strText)
    {
        BindDropDownList();        
        ddlCountry.SelectedIndex = -1;
        foreach (ListItem item in ddlCountry.Items)
        {
            if (item.Text == strText)
            {
                item.Selected = true;
                break;
            }
        }
    }

    private void BindDropDownList()
    {
        this.ddlCountry.DataSource = CountryBLL.GetCountryModels();
        this.ddlCountry.DataTextField = "name";
        this.ddlCountry.DataValueField = "id";
        this.ddlCountry.DataBind();
    }

    /// <summary>
    /// 获取所选择的文本
    /// </summary>
    /// <returns>返回所选择的文本</returns>
    private string GetSelectedItemText()
    {
        return ddlCountry.SelectedItem.Text;
    }

    /// <summary>
    /// 获取所选择的值
    /// </summary>
    /// <returns>返回所选择的值</returns>
    private string GetSelectedValue()
    {
        return ddlCountry.SelectedItem.Value;
    }

    //
    public ListItem SelectedItem
    {
        get { return ddlCountry.SelectedItem; }   
    }

    /// <summary>
    /// 下拉列表改变时 触发该事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.CSelectedIndexChanged != null)
            this.CSelectedIndexChanged(this, e);

    }
}
