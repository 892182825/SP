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
 * 创建时间：   2009-09-08
 * 仓库控件
 */

public partial class UserControl_WareHouse : System.Web.UI.UserControl
{
    /// <summary>
    /// 实例化采购入库业务逻辑层
    /// </summary>

    /// <summary>
    ///获取或设置物流公司的选中值	
    /// </summary>
    public string SelectedValue
    {
        get { return GetSelectedValue(); }
        set { SetSelectedItemValue(value); }
    }

    /// <summary>
    ///获取或设置物流公司的选中值	
    /// </summary>
    public string SelectedItemText
    {
        get { return GetSelectedItemText(); }
        set { SetSelectedItemText(value); }
    }

    /// <summary>
    ///查看仓库的个数	
    /// </summary>
    public string Selected_Count
    {
        get { return Get_count().ToString(); }

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

    private int Get_count()
    {
        return ddlWareHouse.Items.Count;
    }

    private void SetSelectedItemValue(string strValue)
    {
        BindDropDownList();        
        ddlWareHouse.SelectedIndex = -1;
        foreach (ListItem item in ddlWareHouse.Items)
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
        ddlWareHouse.SelectedIndex = -1;
        foreach (ListItem item in ddlWareHouse.Items)
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
        ///通过管理员编号获取仓库相应的权限
        DataTable dt = StorageInBLL.GetMoreManagerPermissionByNumber(Session["Company"].ToString());

        ddlWareHouse.DataTextField = "WareHouseName";
        ddlWareHouse.DataValueField = "WareHouseID";
        ddlWareHouse.DataSource = dt;
        ddlWareHouse.DataBind();
        ViewState["isDataBind"] = "Y";
    }

    private string GetSelectedItemText()
    {        
        return ddlWareHouse.SelectedItem.Text;
    }

    private string GetSelectedValue()
    {
        return ddlWareHouse.SelectedItem.Value;
    }

    private void SetEnabled(bool strValue)
    {        
        ddlWareHouse.Enabled = strValue;
    }  
}
