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
 * 创建者：     汪 华
 * 创建时间：   2009-09-03
 * 功能：       绑定产品类型信息
 */

public partial class UserControl_ProductType : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (ViewState["isDataBind"] == null || ViewState["isDataBind"].ToString() == "N")
            {
                BindDropDownList();
            }                
        }
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

    /// <summary>
    ///设置为是否可用
    /// </summary>
    public bool Enabled
    {
        set { SetEnabled(value); }
    }

    /// <summary>
    /// 绑定数据
    /// </summary>
    public override void DataBind()
    {
        BindDropDownList();
    }

    private void SetSelectedItemValue(string strValue)
    {
        BindDropDownList();        
        ddlProductType.SelectedIndex = -1;
        foreach (ListItem item in ddlProductType.Items)
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
        ddlProductType.SelectedIndex = -1;
        foreach (ListItem item in ddlProductType.Items)
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
        ddlProductType.DataTextField = "ProductTypeName";        
        ddlProductType.DataValueField = "ProductTypeID";        
        ddlProductType.DataSource = AddNewProductBLL.GetProductTypeInfo();
        ddlProductType.DataBind();
        ViewState["isDataBind"] = "Y";
    }


    private string GetSelectedItemText()
    {        
        return ddlProductType.SelectedItem.Text;
    }

    /// <summary>
    /// 获取选择的值
    /// </summary>
    /// <returns>返回选择的值</returns>
    private string GetSelectedValue()
    {        
        return ddlProductType.SelectedItem.Value;
    }


    private void SetEnabled(bool strValue)
    {        
        ddlProductType.Enabled = strValue;
    }
}
