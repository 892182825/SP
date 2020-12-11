using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAL;
using BLL.Logistics;

public partial class UserControl_UCWareHouse : System.Web.UI.UserControl
{
    protected BLL.TranslationBase tran = new BLL.TranslationBase();
    protected void Page_Load(object sender, EventArgs e)
    { 
        InitData();
    }

    public void InitData()
    {
        DataTable dt = WareHouseDAL.GetWareHouseName();        
        DropDownList1.DataSource = dt;
        DropDownList1.DataBind();

        string wareHouseID = DropDownList1.SelectedItem.Value;

        DropDownList2.DataSource = CompanyConsignBLL.GetDepotSeat(wareHouseID);
        DropDownList2.DataBind();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string wareHouseID = DropDownList1.SelectedItem.Value;
        DropDownList2.DataSource = CompanyConsignBLL.GetDepotSeat(wareHouseID);
        DropDownList2.DataBind();
    }
    /// <summary>
    /// 仓库ID
    /// </summary>
    public int WareHousrID
    {
        get
        {
            string wareHouseID = DropDownList1.SelectedItem.Value;
            return int.Parse(wareHouseID);
        }
        set
        {
            foreach (ListItem li in DropDownList1.Items)
            {
                if (li.Value == value.ToString())
                {
                    li.Selected = true;
                    break;
                }
            }
        }
    }
    /// <summary>
    /// 库位ID
    /// </summary>
    public int DepotSeatID
    {
        get
        {
            string depotSeatID = DropDownList2.SelectedItem.Value;
            return int.Parse(depotSeatID);
        }
        set
        {
            foreach (ListItem li in DropDownList2.Items)
            {
                if (li.Value == value.ToString())
                {
                    li.Selected = true;
                    break;
                }
            }
        }
    }
}
