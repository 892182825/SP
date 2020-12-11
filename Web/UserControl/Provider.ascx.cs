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
using System.Data.SqlClient;
using BLL.other.Company;
using BLL.other;
/*
 * 创建者：     汪  华
 * 创建时间：   2009-09-08
 * 提供商用户控件
 */

public partial class UserControl_Provider : System.Web.UI.UserControl
{
    /// <summary>
    ///获取或设置供应商的选中值	
    /// </summary>
    public string SelectedValue
    {
        get { return GetSelectedValue(); }
        set { SetSelectedItemValue(value); }
    }

    /// <summary>
    ///获取或设置供应商的选中值	
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
    /// 绑定数据
    /// </summary>
    public override void DataBind()
    {
        BindDropDownList();
    }


    private void SetSelectedItemValue(string strValue)
    {
        BindDropDownList();        
        ddlProvider.SelectedIndex = -1;
        foreach (ListItem item in ddlProvider.Items)
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
        ddlProvider.SelectedIndex = -1;
        foreach (ListItem item in ddlProvider.Items)
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
        /* Name,Number,ID */
        ddlProvider.DataTextField = "Name";
        ddlProvider.DataValueField = "ID";
        ddlProvider.DataSource = StorageInBLL.GetProviderInfo();
        ddlProvider.DataBind();
        ViewState["isDataBind"] = "Y";
    }
    private string GetSelectedItemText()
    {
        return ddlProvider.SelectedItem.Text;
    }

    private string GetSelectedValue()
    {
        return ddlProvider.SelectedItem.Value;
    }

    private void SetEnabled(bool boolValue)
    {
        ddlProvider.Enabled = boolValue;        
    }
}
