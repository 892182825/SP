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

/// <summary>
/// Add Namespace
/// </summary>
using Model.Other;
using BLL.other.Company;
using BLL.CommonClass;
using ReportControl.Demo;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

/*
 * 创建者：  汪  华
 * 创建时间：2009-10-26
 * 完成时间：2009-10-26
 * 对应菜单：报表中心->仓库各产品明细 
 */

public partial class Company_WareHouseProductDetails : BLL.TranslationBase
{
    public class Item
    {
        public Item(string text, decimal value)
        {
            this._text = text;
            this._value = value;
        }

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
        }

        private decimal _value;
        public decimal Value
        {
            get
            {
                return _value;
            }
        }
    }
    protected string msg = "";
    protected string msg1 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Translations_More();
        // 权限设置
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.Company_WareHouseProductDetails);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        if (!Common.WPermission(Session["Company"].ToString()))
        {
            visdiv.Visible = false;
            Response.Write(Common.ReturnAlert("该管理员没有仓库权限！！"));
            return;
        }
        visdiv.Visible = true;

        if (!IsPostBack)
        {
            DataBindWareHouse();
            DataBindDepotSeat();          
            btnWareHouseView_Click(null,null);
        }           
    }

    /// <summary>
    /// Translation
    /// </summary>
    protected void Translations_More()
    {
        TranControls(btnWareHouse, new string[][] {new string[]{ "000285", "仓库汇总" }});
        TranControls(btnDepotSeat, new string[][] { new string[] { "001942", "库位汇总" } });        
        TranControls(btn_graph, new string[][] {new string[]{"000298","图形分析"}}); ;
        TranControls(btnWareHouseView, new string[][] {new string[]{"001938", "仓库显示"}});
        TranControls(btnDepotSeatView, new string[][] {new string[]{"001941", "库位显示"}});        
        TranControls(ddlWareHouseImage, new string[][] 
                        {
                            new string[] { "001936", "仓库饼图" },
                            new string[] { "001937", "仓库柱形图" },
                        }
                    );

        TranControls(ddlDepotSeatImage, new string[][] 
                        {
                            new string[] { "001939", "库位饼图" },
                            new string[] { "001940", "库位柱形图" },
                        }
            );
                    
    }

    /// <summary>
    /// 绑定仓库信息
    /// </summary>
    private void DataBindWareHouse()
    {
        ///通过管理员编号获取仓库相应的权限
        DataTable dt = StorageInBLL.GetMoreManagerPermissionByNumber(Session["Company"].ToString());

        ddlWareHouse.DataTextField = "WareHouseName";
        ddlWareHouse.DataValueField = "WareHouseID";
        ddlWareHouse.DataSource = dt;
        ddlWareHouse.DataBind();
    }

    /// <summary>
    /// 绑定库位信息
    /// </summary>
    private void DataBindDepotSeat()
    {
        int wareHouseID = Convert.ToInt32(ddlWareHouse.SelectedItem.Value);
        ddlDepotSeat.DataSource = StorageInBLL.GetDepotSeatInfoByWareHouaseID(wareHouseID);
        ddlDepotSeat.DataTextField = "SeatName";
        ddlDepotSeat.DataValueField = "DepotSeatID";
        ddlDepotSeat.DataBind();
    }

    /// <summary>
    /// 仓库(DropDownList)事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlWareHouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWareHouse.SelectedIndex != -1)
        {
            DataBindDepotSeat();
            btnWareHouseView_Click(null,null);
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001932", "仓库错误信息，请联系管理员!")));
        }
    }

    /// <summary>
    /// 库位(DropDownList)事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDepotSeat_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnDepotSeatView_Click(null,null);        
    }

    private string ConstructData()
    {
        int top = 9;
        bool need = true;
        decimal other = 0;
        string column = "";
        string result = "";
        DataTable table = new DataTable();
        string coll ="[";

        if (ViewState["ID"].ToString()=="WareHouse")
        {
            if (ddlWareHouse.SelectedIndex != -1)
            {
                table = WareHouseProductDetailsBLL.GetMoreProductInfoByWareHouseID(Convert.ToInt32(this.ddlWareHouse.SelectedValue));
            }

            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001933", "对不起，请选择仓库!")));
                return null;
            }            
        }

        if (ViewState["ID"].ToString()=="DepotSeat" )
        {
            if (ddlWareHouse.SelectedIndex != -1)
            {
                if (ddlDepotSeat.SelectedIndex != -1)
                {
                    table = WareHouseProductDetailsBLL.GetMoreProductInfoByWareHouseIDDepotSeatID(Convert.ToInt32(this.ddlWareHouse.SelectedValue), Convert.ToInt32(this.ddlDepotSeat.SelectedValue));
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001934", "对不起，请选择库位!")));
                    return null; 
                }
            }

            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001933", "对不起，请选择仓库!")));
                return null;
            }
        }        

        column = "ProductName";
        result = "TotalEnd";

        int rows = table.Rows.Count;
        DataView dv = table.DefaultView;

        dv.Sort = result + "" + " Desc";

        if (top >= dv.Count)
        {
            need = false;
            top = dv.Count;
        }

        for (int i = 0; i < top; i++)
        {
            coll += "{label: '" + dv[i][column].ToString() + "',data: '" + Convert.ToDecimal(dv[i][result].ToString())+"'},";
        }
        if (need)
        {
            for (int i = top; i < dv.Count; i++)
            {
                other = other + Convert.ToDecimal(dv[i][result].ToString());
            }
            coll += "{label: '" + GetTran("000470", "其它") + "',data: '" + other + "'},";
        }
        coll = coll.Substring(0, coll.Length - 1);
        coll += "];";
        return coll;
    }

    private string ProductData()
    {
        int top = 9;
        bool need = true;
        decimal other = 0;
        string column = "";
        string result = "";
        DataTable table = new DataTable();
        string coll = "var data1=[";
        string CBLL = "  var data2=[";

        if (ViewState["ID"].ToString() == "WareHouse")
        {
            if (ddlWareHouse.SelectedIndex != -1)
            {
                table = WareHouseProductDetailsBLL.GetMoreProductInfoByWareHouseID(Convert.ToInt32(this.ddlWareHouse.SelectedValue));
            }

            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001933", "对不起，请选择仓库!")));
                return null;
            }
        }

        if (ViewState["ID"].ToString() == "DepotSeat")
        {
            if (ddlWareHouse.SelectedIndex != -1)
            {
                if (ddlDepotSeat.SelectedIndex != -1)
                {
                    table = WareHouseProductDetailsBLL.GetMoreProductInfoByWareHouseIDDepotSeatID(Convert.ToInt32(this.ddlWareHouse.SelectedValue), Convert.ToInt32(this.ddlDepotSeat.SelectedValue));
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001934", "对不起，请选择库位!")));
                    return null;
                }
            }

            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001933", "对不起，请选择仓库!")));
                return null;
            }
        }

        column = "ProductName";
        result = "TotalEnd";

        int rows = table.Rows.Count;
        DataView dv = table.DefaultView;

        dv.Sort = result + "" + " Desc";

        if (top >= dv.Count)
        {
            need = false;
            top = dv.Count;
        }

        for (int i = 0; i < top; i++)
        {
            coll += "["+i+",'" + dv[i][column].ToString() + "'],";
            CBLL += "[" + i + "," + Convert.ToDecimal(dv[i][result].ToString()) + "],";
        }
        if (need)
        {
            for (int i = top; i < dv.Count; i++)
            {
                other = other + Convert.ToDecimal(dv[i][column].ToString());
            }
            coll += "{label: '" + GetTran("000470", "其它") + "',data: '" + other + "'},";
        }
        coll = coll.Substring(0, coll.Length - 1);
        CBLL = CBLL.Substring(0, CBLL.Length - 1);
        coll += "];";
        CBLL += "];";
        coll += CBLL;
        return coll;
    }
    
    private void BindData()
    {        
        if (Convert.ToString(ViewState["ID"]) == "WareHouse")
        {
            //displayChart.DataSource = ConstructData();
            if (this.ddlWareHouseImage.SelectedValue == "1")
            {
                msg = "<script language='javascript'>var data = " + ConstructData() + " $('#aa').show();$('#bb').hide();window.onload = yuan();</script>";


            }
            else if (this.ddlWareHouseImage.SelectedValue == "2")
            {
                msg1 = "<script language='javascript'>" + ProductData() + " $('#bb').show();$('#aa').hide();window.onload = zhu();</script>";
            }

            
        }

        
    }

    /// <summary>
    /// 
    /// </summary>
    public void DisplayDepotSeat()
    { 
        if (Convert.ToString(ViewState["ID"]) == "DepotSeat")
        {

            if (this.ddlWareHouseImage.SelectedValue == "1")
            {
                msg = "<script language='javascript'>var data = " + ConstructData() + " $('#aa').show();$('#bb').hide();window.onload = yuan();</script>";


            }
            else if (this.ddlWareHouseImage.SelectedValue == "2")
            {
                msg1 = "<script language='javascript'>" + ProductData() + " $('#bb').show();$('#aa').hide();window.onload = zhu();</script>";
            }

            
        }
    }

    /// <summary>
    /// 仓库显示
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnWareHouseView_Click(object sender, EventArgs e)
    {
        Translations_More();
        ViewState["ID"] = "WareHouse";
        BindData();
      
    }

    /// <summary>
    /// 库位显示
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDepotSeatView_Click(object sender, EventArgs e)
    {       
        ViewState["ID"] = "DepotSeat";
        DisplayDepotSeat();    
    }

    /// <summary>
    /// 仓库汇总
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnWareHouse_Click(object sender, EventArgs e)
    {
        if (this.ddlWareHouse.SelectedIndex != -1)
        {
            Session["WareHouseID"] = this.ddlWareHouse.SelectedValue;

            //Response.Write("<script language='javascript'>window.open('ProductStock.aspx?Flag=WareHouse')</script>");
            Response.Redirect("ProductStock.aspx?Flag=WareHouse", true);
            //btnWareHouseView_Click(null, null);
        }
    }

    /// <summary>
    /// 库位汇总
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDepotSeat_Click(object sender, EventArgs e)
    {
        if (this.ddlWareHouse.SelectedIndex != -1)
        {
            if (this.ddlDepotSeat.SelectedIndex != -1)
            {
                Session["WareHouseID"] = this.ddlWareHouse.SelectedValue;
                Session["DepotSeatID"] = this.ddlDepotSeat.SelectedValue;
                //Response.Write("<script language='javascript'>window.open('ProductStock.aspx?Flag=DepotSeat')</script>");
                Response.Redirect("ProductStock.aspx?Flag=DepotSeat", true);
                //btnDepotSeatView_Click(null, null);
            }

            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001935", "库位错误信息，请联系管理员!")));
                return;
            }
        }

        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("001932", "仓库错误信息，请联系管理员!")));
            return;
        }
    }

    /// <summary>
    /// 仓库图新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlWareHouseImage_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnWareHouseView_Click(null,null);
    }

    /// <summary>
    /// 库位图形
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDepotSeatImage_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnDepotSeatView_Click(null,null);        
    }
   
}
