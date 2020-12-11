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
using DAL;

public partial class WareHouses : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(WareHouses));
        if (!IsPostBack)
        {
            Pagebind();
        }
    }

    #region 公开的Ajax方法
    [AjaxPro.AjaxMethod]
    public DataTable getDepotseat(string houseid)
    {
        DataTable talbe = DepotSeatDAL.GetDepotSeats(houseid);
        return talbe;
    }
    #endregion

    #region 页面初始化
    private void Pagebind()
    {
        drpwarehouse.DataSource = WareHouseDAL.GetProductWareHouseInfo();
        drpwarehouse.DataTextField = "Name";
        drpwarehouse.DataValueField = "ID";
        drpwarehouse.DataBind();
        if (drpwarehouse.DataSource!=null)
        {
            drpwarehouse.Attributes.Add("onchange","showNext(this.options[selectedIndex].value)");
            drpdepotseat.Attributes.Add("onchange", "getdepotseat()");
        }
        if (drpwarehouse.Items.Count>0)
        {
            drpdepotseat.DataSource = DepotSeatDAL.GetDepotSeats(drpwarehouse.SelectedItem.Value.ToString());
            drpdepotseat.DataTextField = "Name";
            drpdepotseat.DataValueField = "ID";
            drpdepotseat.DataBind();
        }

    }
    #endregion

    #region 设置仓库属性
    private int GetWareHoueseID()
    {
        if (txtwarehouse.Text.Trim() != "")
        {
           return Convert.ToInt32(txtwarehouse.Text.Trim());
        }
        else
        {
            ScriptHelper.SetAlert(drpwarehouse,"对不起，不存在仓库！！！");
        }
        return 0;
    }
    #endregion

    #region 设置库位属性
    private int GetDepotseatID()
    {
        if (txtdepotseat.Text.Trim()!="")
        {
            return Convert.ToInt32(txtdepotseat.Text.Trim());
        }
        else
        {
            ScriptHelper.SetAlert(drpwarehouse, "对不起，不存在仓库！！！");
        }
        return 0;
    }
    #endregion

    public int DepotseatID
    {
        get { return GetDepotseatID(); }
    }

    public int WarehouseID
    {
        get { return GetWareHoueseID(); }
    }
}
