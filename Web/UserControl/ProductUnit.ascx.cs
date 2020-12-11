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
 * 功能：       绑定产品单位信息
 */

public partial class UserControl_ProductUnit : System.Web.UI.UserControl
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
        ddlProductUnit.SelectedIndex = -1;
        foreach (ListItem item in ddlProductUnit.Items)
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
        ddlProductUnit.SelectedIndex = -1;
        foreach (ListItem item in ddlProductUnit.Items)
        {
            if (item.Text == strText)
            {
                item.Selected = true;
                break;
            }
        }
    }
    
   /// <summary>
   /// 绑定产品单位信息
   /// </summary>
    private void BindDropDownList()
    {        
        ddlProductUnit.DataTextField = "ProductUnitName";        
        ddlProductUnit.DataValueField = "ProductUnitID";              
        ddlProductUnit.DataSource = AddNewProductBLL.GetProductUnitInfo();
        ddlProductUnit.DataBind();
        ViewState["isDataBind"] = "Y";
    }

    /// <summary>
    /// 获取选择的文本
    /// </summary>
    /// <returns>返回选择的文本</returns>
    private string GetSelectedItemText()
    {
        return ddlProductUnit.SelectedItem.Text;
    }

    /// <summary>
    /// 获取选择的值
    /// </summary>
    /// <returns>返回选择的值</returns>
    private string GetSelectedValue()
    {
        return ddlProductUnit.SelectedItem.Value;
    }
}
