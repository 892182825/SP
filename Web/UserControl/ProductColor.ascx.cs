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
 * 创建时间：   2009-09-03
 * 功能：       绑定产品颜色信息
 */

public partial class UserControl_ProductColor : System.Web.UI.UserControl
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
    /// 绑定数据
    /// </summary>
    public override void DataBind()
    {
        BindDropDownList();
    }


    private void SetSelectedItemValue(string strValue)
    {
        BindDropDownList();        
        ddlProductColor.SelectedIndex = -1;
        foreach (ListItem item in ddlProductColor.Items)
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
        ddlProductColor.SelectedIndex = -1;
        foreach (ListItem item in ddlProductColor.Items)
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
        ddlProductColor.DataTextField = "ProductColorName";
        ddlProductColor.DataValueField = "ProductColorID";               
        ddlProductColor.DataSource = AddNewProductBLL.GetProductColorInfo();
        ddlProductColor.DataBind();
        ViewState["isDataBind"] = "Y";
    }

    private string GetSelectedItemText()
    {
        return ddlProductColor.SelectedItem.Text;
    }

    private string GetSelectedValue()
    {
        return ddlProductColor.SelectedItem.Value;
    }

}
