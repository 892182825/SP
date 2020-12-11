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
using DAL;
using System.Text;
using Model.Other;
using BLL.Logistics;
using Model;
public partial class Company_ReWareHouse : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.StorageAddReWareHouse);
        if (!Common.WPermission(Session["Company"].ToString()))
        {
            visdiv.Visible = false;
            Response.Write(Common.ReturnAlert("该管理员没有仓库权限！！"));
            return;
        }
        visdiv.Visible = true;
        if (!IsPostBack)
        {
          

            CountryModel mode = CountryBLL.GetCountryModels()[0];
            DataView dataev = new DataView(BillOutOrderBLL.GetWareHouseName());
            dataev.RowFilter = "[CountryCode]=" + CountryBLL.GetCountryByCode(mode.ID);
            ddlcangku.DataSource = dataev;
            ddlcangku.DataTextField = "WareHouseName";
            ddlcangku.DataValueField = "WareHouseID";
            ddlcangku.DataBind();
            if (string.IsNullOrEmpty(ddlcangku.SelectedValue))
            {
              
                ViewState["Ekuc"] = "false";
            }
            else
            {
                ViewState["Ekuc"] = "true";
            }
            ddlcangku_SelectedIndexChanged(null, null);
            BtnConfirm_Click(null, null);
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.givDoc,
                new string[][]{
                    new string []{"000399","查看详细"},
                    new string []{"000407","单据编号"},
                    new string []{"000702","调入仓库"},
                    new string []{"000703","调入库位"},
                    new string []{"000704","调出仓库"},
                    new string []{"000705","调出库位"},
                    new string []{"000708","调拨时间"},
                    new string []{"000045","期数"},
                    new string []{"000414","积分"},
                    new string []{"000041","总金额"},
                    new string []{"000078","备注"}});
        this.TranControls(this.btnE, new string[][] { new string[] { "005991", "添加调拨" } });
    }

    /// <summary>
    /// 查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        int docTpyeId = Convert.ToInt32(QueryInStorageBLL.GetDocTypeIDEByDocTypeCode("DB"));
        sb.Append(" 1=1 and a.DocTypeID=" + docTpyeId);

        ////判断国家
        //UserControl_Country cou = Page.FindControl("Country1") as UserControl_Country;
        //if (cou.CID > 0)
        //{
        //    sb.Append(" and b.ID=" + cou.CID);
        //}
        //仓库
        if (Convert.ToInt32(ddlcangku.SelectedValue) > 0)
        {
            sb.Append(" and a.WareHouseID=" + ddlcangku.SelectedValue);
        }
        //库位
        if (Convert.ToInt32(ddlkuwei.SelectedValue) > 0)
        {
            sb.Append(" and a.DepotSeatID=" + ddlkuwei.SelectedValue);
        }
        ViewState["sql"] = sb.ToString();
       
        Pager1.PageBind(0, 10, "InventoryDoc a  left outer join warehouse d on d.warehouseid=a.WareHouseID left outer join DepotSeat c on a.DepotSeatID=c.depotseatid  ", "a.inwarehouseid,a.indepotseatid, a.ID, a.DocTypeID, a.DocID, a.DocMakeTime,a.DocMaker,a.Client, d.warehousename,a.TotalMoney,c.seatname,a.TotalPV, a.ExpectNum, a.Note, a.StateFlag, a.CloseFlag, a.CloseDate, a.BatchCode,a.OriginalDocID, a.Address,  a.Flag, a.Charged, a.Reason ", sb.ToString(), "a.ID", "givDoc");
        Translations();
    }



    /// <summary>
    /// 获取格林时间
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public string Getdatetime(object date)
    {
        if (string.IsNullOrEmpty(date.ToString()))
        {
            return "无";
        }
        DateTime dt = Convert.ToDateTime(date.ToString());
        return dt.AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime().ToString("yyyy-MM-dd");
    }

    //绑定库位
    protected void ddlcangku_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlcangku.SelectedValue) && ddlcangku.SelectedValue != "-1")
        {
        int id = Convert.ToInt32(ddlcangku.SelectedValue);
        ddlkuwei.DataSource = DAL.DepotSeatDAL.GetDepotSeat(id.ToString());
        ddlkuwei.DataValueField = "DepotSeatID";
        ddlkuwei.DataTextField = "SeatName";
        ddlkuwei.DataBind();
           }
        else
        {
            ddlkuwei.Items.Clear();
            ddlkuwei.Items.Add(new ListItem(GetTran("000589", "无库位"), "-1"));
        }
    }
    protected void givDoc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        string name = e.CommandName;
        if ("Details" == name)
        {
            Response.Redirect("ShowBillDetailsD.aspx?DocID=" + id);
        }
        else
        {
            //Response.Redirect("DocPrint.aspx?DocID=" + id);
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>window.open('docPrint.aspx?docID=" + id + "');</script>");
        }
    }
    protected void givDoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations();
        }
    }

    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        BtnConfirm_Click(null, null);
    }

    public string GetRemark(string remark, string docID)
    {
        if (String.IsNullOrEmpty(remark))
        {
            return GetTran("000221", "无");
        }
        else
        {
            return "<a href='ShowBillsNote.aspx?docID=" + docID + "'>"+ GetTran("000440", "查看") +"</a>";
        }
    }

    /// <summary>
    /// 获取仓库
    /// </summary>
    /// <param name="warehouseid"></param>
    /// <returns></returns>
    public string getwarehouse(object warehouseid)
    {
        if(string.IsNullOrEmpty(warehouseid.ToString()) )
        {
            return "";
        }
        int id=Convert.ToInt32(warehouseid.ToString());
        return DAL.WareHouseDAL.WareHouseIdEName(id);
    }

    /// <summary>
    /// 获取库位
    /// </summary>
    /// <param name="depotid"></param>
    /// <returns></returns>
    public string getDepot(object depotid)
    {
        if (string.IsNullOrEmpty(depotid.ToString()))
        {
            return "";
        }
        int id = Convert.ToInt32(depotid.ToString());
        return DAL.DepotSeatDAL.DepotSeatEName(id);
    }

    protected void btnDownExcel_Click(object sender, EventArgs e)
    {
        string tj = "";
        if (ViewState["sql"] == null)
            tj = "1=1";
        else
            tj = ViewState["sql"].ToString();

        DataTable dt = DAL.DBHelper.ExecuteDataTable(@"select dbo.GetTypeName(a.DocTypeID) as DocTypename, a.DocID, a.DocMakeTime,a.DocMaker,a.TotalMoney, a.TotalPV, a.ExpectNum, a.DocAuditer, a.DocAuditTime from InventoryDoc a  where " + tj);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["DocMakeTime"] = string.IsNullOrEmpty(dt.Rows[i]["DocMakeTime"].ToString()) ? dt.Rows[i]["DocMakeTime"] : Convert.ToDateTime(dt.Rows[i]["DocMakeTime"].ToString()).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime().ToString();
            dt.Rows[i]["DocAuditTime"] = string.IsNullOrEmpty(dt.Rows[i]["DocAuditTime"].ToString()) ? dt.Rows[i]["DocAuditTime"] : Convert.ToDateTime(dt.Rows[i]["DocAuditTime"].ToString()).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime().ToString();
        }
        Excel.OutToExcel(dt, "库存单据表", new string[] { "DocTypename=单据类型", "DocID=单据编号", "DocMakeTime=开出时间", "DocMaker=开出人", "TotalMoney=总金额", "TotalPV=积分", "ExpectNum=期数", "DocAuditer=审核人", "DocAuditTime=审核时间" });
    }
    protected void btnE_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddRewareHouse.aspx", true);
    }
    /// <summary>
    /// 国家与仓库联动
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Country1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataView dataev = new DataView(BillOutOrderBLL.GetWareHouseName());
        UserControl_Country cou = Page.FindControl("Country1") as UserControl_Country;
        dataev.RowFilter = "[CountryCode]=" + CountryBLL.GetCountryByCode(cou.CID);
        ddlcangku.DataSource = dataev;
        ddlcangku.DataTextField = "WareHouseName";
        ddlcangku.DataValueField = "WareHouseID";
        ddlcangku.DataBind();
        if (string.IsNullOrEmpty(ddlcangku.SelectedValue))
        {
            //ddlkuwei.DataSource = AddReportProfit.GetProductWareHouseInfo();
            //ddlkuwei.DataTextField = "name";
            //ddlkuwei.DataValueField = "id";
            //ddlkuwei.DataBind();
            //ddlkuwei.Enabled = false;
            //ddlcangku.Enabled = false;
            ddlcangku.Items.Add(new ListItem(GetTran("000592", "无仓库"), "-1"));
            ViewState["Ekuc"] = "false";
        }
        else
        {
            ViewState["Ekuc"] = "true";
        }
        ddlcangku_SelectedIndexChanged(null, null);
    }
}

