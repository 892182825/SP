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
using System.Text;
using Model;
using BLL.other.Company;
using DAL;
using Model.Other;
using Standard.Classes;

using BLL.Logistics;
using Model;

public partial class Company_BrowseBills : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.StorageBrowseBills);
        if (!Common.WPermission(Session["Company"].ToString()))
        {
            vistable.Visible = false;
            vistable1.Visible = false;
            Response.Write(Common.ReturnAlert("该管理员没有仓库权限！！"));
            return;
        }
        vistable.Visible = true;
        vistable1.Visible = true;
        if (!IsPostBack)
        {
            //绑定单据类型
            ddltype.DataSource = DocTypeTableDAL.GetDocTypeTableInfo();
            ddltype.DataTextField = "DocTypeName";
            ddltype.DataValueField = "DocTypeID";
            ddltype.DataBind();

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

            btnSeach_Click(null, null);

            this.txtshenheren.Text = GetTran("000881", "不限");
            this.txtkaichuren.Text = GetTran("000881", "不限");

            Translations();
        }
    }

    private void Translations()
    {
        this.TranControls(this.ddlkuaichuDatetype,
            new string[][]{
                new string[]{"000881","不限"},
                new string[]{"000361","大于"},
                new string[]{"000367","小于"},
                new string[]{"000372","等于"}
            });
        this.TranControls(this.ddlState,
            new string[][]{
                new string[]{"000881","不限"},
                new string[]{"001009","未审核"},
                new string[]{"001011","已审核"},
                new string[]{"001072","有效"},
                new string[]{"001069","无效"}
            });
        this.TranControls(this.ddlshenheshijitype,
            new string[][]{
                new string[]{"000881","不限"},
                new string[]{"000361","大于"},
                new string[]{"000367","小于"},
                new string[]{"000372","等于"}
            });
        this.TranControls(this.givDoc,
            new string[][]{
                new string[]{"000399", "查看详细"},
                new string[]{"001149","打印"},
                new string[]{"001151","单据类型"},
                new string[]{"000407","单据编号"},
                new string[]{"001153","开出时间"},
                new string[]{"000519","开出人"},
                new string[]{"000041","总金额"},
                new string[]{"000414","积分"},
                new string[]{"000045","期数"},
                new string[]{"000655","审核人"},
                new string[]{"001155","审核时间"},
                new string[]{"000744","查看备注"}
            });
        this.TranControls(this.Button3, new string[][] { new string[] { "000633", "全部" } });
    }

    /// <summary>
    /// 根据id获取单据名称
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    protected string Type(object obj)
    {
        int id = Convert.ToInt32(obj);
        string codee = DocTypeTableDAL.GetDocTypeNameByID(id);
        string typename = string.Empty;
        switch (codee)
        {
            case "入库单": typename = GetTran("005381", "入库单"); break;
            case "出库单": typename = GetTran("005382", "出库单"); break;
            case "报溢单": typename = GetTran("000584", "报溢单"); break;
            case "报损单": typename = GetTran("000768", "报损单"); break;
            case "调拨单": typename = GetTran("000581", "调拨单"); break;
            case "退货单": typename = GetTran("000583", "退货单"); break;
            case "换货退货单": typename = GetTran("005409", "换货退货单"); break;
            case "发货单": typename = GetTran("005410", "发货单"); break;
        }
        return typename;
    }

    public string GetRemark(string remark, string docID)
    {
        if (String.IsNullOrEmpty(remark))
        {
            return GetTran("000221", "无");
        }
        else
        {
            return "<a href='ShowBillsNote.aspx?docID=" + docID + "'>" + GetTran("000440", " 查看") + "</a>";
        }
    }

    //得到币种
    //CurrencyDAL currency = new CurrencyDAL();
    //protected string GetCurrency(object obj)
    //{
    //    int id = Convert.ToInt32(obj);
    //    return CurrencyDAL.GetCurrencyNameById(id);
    //}
    protected void btnSeach_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" 1=1 ");
        //判断单据类型
        if (Convert.ToInt32(ddltype.SelectedValue) > 0)
        {
            sb.Append(" and DocTypeID=" + ddltype.SelectedValue);
        }
        //判断国家
        //UserControl_Country cou = Page.FindControl("Country1") as UserControl_Country;
        //if (cou.CID > 0)
        //{
        //    sb.Append(" and b.ID=" + cou.CID);
        //}
        //仓库
        if (Convert.ToInt32(ddlcangku.SelectedValue) > 0)
        {
            if (ddltype.SelectedItem.Text == "出库单" || ddltype.SelectedItem.Text == "报损单")
            {
                sb.Append(" and inWareHouseID=" + ddlcangku.SelectedValue);
            }
            else
            {
                sb.Append(" and WareHouseID=" + ddlcangku.SelectedValue);
            }
        }
        //开出时间
        if (txtkaichurenTime.Text.Trim().Length > 0)
        {
            if (ddlkuaichuDatetype.SelectedValue != "0")
            {
                switch (ddlkuaichuDatetype.SelectedValue)
                {
                    case ">":
                        sb.Append(" and DocMakeTime" + ddlkuaichuDatetype.SelectedValue + "'" + Convert.ToDateTime(txtkaichurenTime.Text.Trim() + " 23:59:59").AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime() + "'");
                        break;
                    case "<":
                        sb.Append(" and DocMakeTime" + ddlkuaichuDatetype.SelectedValue + "'" + Convert.ToDateTime(txtkaichurenTime.Text.Trim() + " 00:00:00").AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime() + "'");
                        break;
                    case "=":
                        sb.Append(" and DocMakeTime >= '" + Convert.ToDateTime(txtkaichurenTime.Text.Trim() + " 00:00:00").AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime() + "'");
                        sb.Append(" and DocMakeTime <='" + Convert.ToDateTime(txtkaichurenTime.Text.Trim() + " 23:59:59").AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime() + "'");
                        break;
                }

            }

        }
        //审核时间
        if (txtshenherentime.Text.Trim().Length > 0)
        {
            if (ddlshenheshijitype.SelectedValue != "0")
            {
                switch (ddlshenheshijitype.SelectedValue)
                {
                    case ">":
                        sb.Append(" and DocAuditTime " + ddlshenheshijitype.SelectedValue + "'" + Convert.ToDateTime(txtshenherentime.Text.Trim() + " 23:59:59").AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime() + "'");
                        break;
                    case "<":
                        sb.Append(" and DocAuditTime " + ddlshenheshijitype.SelectedValue + "'" + Convert.ToDateTime(txtshenherentime.Text.Trim() + " 00:00:00").AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime() + "'");
                        break;
                    case "=":
                        sb.Append(" and DocAuditTime>= '" + Convert.ToDateTime(txtshenherentime.Text.Trim() + " 00:00:00").AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime() + "'");
                        sb.Append(" and DocAuditTime<= '" + Convert.ToDateTime(txtshenherentime.Text.Trim() + " 23:59:59").AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime() + "'");
                        break;
                }
            }
        }
        //开出人
        if (txtkaichuren.Text.Trim().Replace("'", "") != GetTran("000881", "不限"))
        {
            sb.Append(" and DocMaker ='" + txtkaichuren.Text.Trim().Replace("'", "") + "'");
        }
        //审核人
        if (txtshenheren.Text.Trim().Replace("'", "") != GetTran("000881", "不限"))
        {
            sb.Append(" and DocAuditer='" + txtshenheren.Text.Trim().Replace("'", "") + "'");
        }
        //库位
        if (Convert.ToInt32(ddlkuwei.SelectedValue) > 0)
        {
            if (ddltype.SelectedItem.Text == "出库单" || ddltype.SelectedItem.Text == "报损单")
            {
                sb.Append(" and inDepotSeatID=" + ddlkuwei.SelectedValue);
            }
            else
            {
                sb.Append(" and DepotSeatID=" + ddlkuwei.SelectedValue);
            }
        }
        //单据状态
        if (Convert.ToInt32(ddlState.SelectedValue) >= 0)
        {
            switch (ddlState.SelectedValue)
            {
                case "0":
                    sb.Append(" and StateFlag=" + ddlState.SelectedValue);
                    break;
                case "1":
                    sb.Append(" and StateFlag=" + ddlState.SelectedValue);
                    break;
                case "3":
                    sb.Append(" and closeflag= 0 ");
                    break;
                case "4":
                    sb.Append(" and closeflag=1 ");
                    break;
            }

        }

        string columns = "a.ID,a.DocTypeID,a.DocID,a.DocMakeTime,a.DocAuditTime,a.DocMaker,a.DocAuditer,a.Provider,a.Client,a.DepotSeatID,a.WareHouseID,a.TotalMoney,a.TotalPV,a.DocSecondAuditTime,a.ExpectNum,a.Note,a.StateFlag,a.CloseFlag,a.CloseDate,a.BatchCode,a.OperationPerson,a.OriginalDocID,a.Address,a.Flag,a.Charged,a.Reason,a.OperateIP,a.OperateNum";
        string table = "InventoryDoc a";
        string key = "a.ID";

        ViewState["SQLSTR"] = "select " + columns + " from " + table + " where " + sb.ToString() + " order by " + key + " desc";

        Pager page = Page.FindControl("Pager1") as Pager;
        page.PageBind(0, 10, table, columns, sb.ToString(), key, "givDoc");
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
            return GetTran("000221", "无");
        }
        DateTime dt = Convert.ToDateTime(date.ToString());
        return dt.AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();
    }

    //绑定库位
    protected void ddlcangku_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlcangku.SelectedValue) && ddlcangku.SelectedValue != "-2")
        {
            int id = Convert.ToInt32(ddlcangku.SelectedValue);
            // DepotSeatDAL depot = new DepotSeatDAL();
            ddlkuwei.DataSource = DepotSeatDAL.GetDepotSeat(id.ToString());
            ddlkuwei.DataValueField = "DepotSeatID";
            ddlkuwei.DataTextField = "SeatName";
            ddlkuwei.DataBind();
        }
        else
        {
            ddlkuwei.Items.Clear();
            ddlkuwei.Items.Add(new ListItem(GetTran("000589", "无库位"), "-2"));
        }
    }
    protected void givDoc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        string name = e.CommandName;
        if ("Details" == name)
        {
            Response.Redirect("ShowBillDetailsB.aspx?DocID=" + id);
        }
        else
        {
            //Response.Redirect("DocPrint.aspx?DocID=" + id);
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>window.open('docPrint.aspx?docID=" + id + "');</script>");
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void givDoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations();
        }
    }
    protected void Butt_Excel_Click(object sender, ImageClickEventArgs e)
    {
        //DataTable dt = DBHelper.ExecuteDataTable(@"select dbo.GetTypeName(a.DocTypeID) as DocTypename,a.DocID,a.DocMakeTime,a.DocMaker,a.TotalMoney,a.TotalPV,a.ExpectNum,a.DocAuditer,a.DocAuditTime from InventoryDoc a left join Country b on a.Currency=b.ID where " + tj + " order by a.id desc");
        DataTable dt = DAL.DBHelper.ExecuteDataTable(ViewState["SQLSTR"].ToString());
        if (dt != null && dt.Rows.Count < 1)
        {
            Response.Write("<script language='javascript'>alert('" + GetTran("000051", "对不起，找不到指定条件的记录！") + "')</script>");
            return;
        }

        dt.Columns.Add("DocTypename", typeof(System.String));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["DocTypename"] = Type(dt.Rows[i]["DocTypeID"].ToString());
            dt.Rows[i]["DocMakeTime"] = string.IsNullOrEmpty(dt.Rows[i]["DocMakeTime"].ToString()) ? dt.Rows[i]["DocMakeTime"] : Convert.ToDateTime(dt.Rows[i]["DocMakeTime"].ToString()).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime().ToString();
            dt.Rows[i]["DocAuditTime"] = string.IsNullOrEmpty(dt.Rows[i]["DocAuditTime"].ToString()) ? dt.Rows[i]["DocAuditTime"] : Convert.ToDateTime(dt.Rows[i]["DocAuditTime"].ToString()).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime().ToString();
        }
        Excel.OutToExcel(dt, GetTran("001200", "库存单据表"), new string[] { "DocTypename=" + GetTran("001151", "单据类型"), "DocID=" + GetTran("000407", "单据编号"), "DocMakeTime=" + GetTran("001153", "开出时间"), "DocMaker=" + GetTran("000519", "开出人"), "TotalMoney=" + GetTran("000041", "总金额"), "TotalPV=" + GetTran("000414", "积分"), "ExpectNum=" + GetTran("000045", "期数"), "DocAuditer=" + GetTran("000655", "审核人"), "DocAuditTime=" + GetTran("001155", "审核时间") });
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("BrowseBills.aspx");
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        btnSeach_Click(null, null);
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
            ddlcangku.Items.Add(new ListItem(GetTran("000592", "无仓库"), "-2"));
            ViewState["Ekuc"] = "false";
        }
        else
        {
            ViewState["Ekuc"] = "true";
        }
        ddlcangku_SelectedIndexChanged(null, null);
    }
}