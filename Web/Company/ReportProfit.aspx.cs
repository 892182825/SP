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
public partial class Company_ReportProfit : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.StorageAddReportProfit);


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
            dataev.RowFilter = "[CountryCode]='" + CountryBLL.GetCountryByCode(mode.ID) + "'";
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
            Pager page = Page.FindControl("Pager1") as Pager;

            Translations();
        }
    }
    private void Translations()
    {
        this.TranControls(this.givDoc,
                new string[][]{
                    new string []{"000399", "查看详细"},
                    new string []{"000407","单据编号"},
                    new string []{"000632","报溢仓库"},
                    new string []{"000635","报溢库位"},
                    new string []{"000636","报溢时间"},
                    new string []{"000045","期数"},
                    new string []{"000414","积分"},
                    new string []{"000041","总金额"},
                    new string []{"000078","备注"}});
        this.TranControls(this.btnE, new string[][] { new string[] { "005990", "添加报溢" } });
    }

    /// <summary>
    /// 查询按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        int docTpyeId = Convert.ToInt32(QueryInStorageBLL.GetDocTypeIDEByDocTypeCode("BY"));
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


        string colunmns = " a.ID,a.DocTypeID,a.DocID,a.DocMakeTime,a.DocMaker,a.Client,d.warehousename,a.TotalMoney,c.seatname,a.TotalPV,a.ExpectNum,a.Note,a.StateFlag,a.CloseFlag,a.CloseDate,a.BatchCode,a.OriginalDocID,a.Address,a.Flag,a.Charged,a.Reason";
        string table = "InventoryDoc a left outer join warehouse d on d.warehouseid=a.WareHouseID left outer join DepotSeat c on a.DepotSeatID=c.depotseatid";

        ViewState["SQLSTR"] = "select " + colunmns + " from " + table + " where " + sb.ToString() + " order by a.ID desc";

        Pager1.PageBind(0, 10, table, colunmns, sb.ToString(), "a.ID", "givDoc");
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
            Response.Redirect("ShowBillDetailsT.aspx?DocID=" + id);
        }
        else
        {
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
            return "<a href='ShowBillsNote.aspx?docID=" + docID + "'>" + GetTran("000440", "查看") + "</a>";
        }
    }

    protected void btnDownExcel_Click(object sender, EventArgs e)
    {
        //DataTable dt = AddReportDamageBLL.GetReportList(ViewState["SQLSTR"].ToString());
        DataTable dt = DAL.DBHelper.ExecuteDataTable(ViewState["SQLSTR"].ToString());
        if (dt != null && dt.Rows.Count < 1)
        {
            Response.Write("<script language='javascript'>alert('" + GetTran("000051", "对不起，找不到指定条件的记录！") + "')</script>");
            return;
        }

        dt.Columns.Add("Remark", typeof(System.String));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["DocMakeTime"] = string.IsNullOrEmpty(dt.Rows[i]["DocMakeTime"].ToString()) ? dt.Rows[i]["DocMakeTime"] : Convert.ToDateTime(dt.Rows[i]["DocMakeTime"].ToString()).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime().ToString();
            dt.Rows[i]["Remark"] = GetRemark(dt.Rows[i]["Note"].ToString(), dt.Rows[i]["DocID"].ToString());
        }
        Excel.OutToExcel1(dt, "库存报溢", new string[] { 
            "DocID=" + GetTran("000407", "单据编号"), 
            "warehousename=报溢仓库", "seatname=报溢库位", 
            "DocMakeTime=报溢时间", "ExpectNum=期数", 
            "TotalPV=积分","TotalMoney=总金额","Remark=备注" });
    }
    protected void btnE_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddReportProfit.aspx", true);
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
        dataev.RowFilter = "[CountryCode]='" + CountryBLL.GetCountryByCode(cou.CID) + "'";
        ddlcangku.DataSource = dataev;
        ddlcangku.DataTextField = "WareHouseName";
        ddlcangku.DataValueField = "WareHouseID";
        ddlcangku.DataBind();
        if (string.IsNullOrEmpty(ddlcangku.SelectedValue))
        {
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