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

public partial class Company_ShowBillDetailsT : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (!IsPostBack)
        {
            if (null != Request["DocID"])
            {
                DataTable dt = InventoryDocDetailsBLL.GetProductsByIdTwo(Request["DocID"].ToString());
                this.givDoc.DataSource = dt;
                this.givDoc.DataBind();

                //InventoryDocDetailsBLL isvent = new InventoryDocDetailsBLL();
                this.givDocDitals.DataSource = InventoryDocDetailsBLL.GetInventoryDocDetailsByDocID(Request["DocID"].ToString());
                this.givDocDitals.DataBind();

                if (Request["DocID"].ToString().StartsWith("DB") || Request["DocID"].ToString().StartsWith("HT"))//调拨单、退货换货单
                {
                    givDocDitals.Columns[9].Visible = false;
                    givDocDitals.Columns[10].Visible = false;
                    givDocDitals.Columns[11].Visible = false;
                    givDocDitals.Columns[12].Visible = false;
                }
                else if (Request["DocID"].ToString().StartsWith("CP") || Request["DocID"].ToString().StartsWith("CK") || Request["DocID"].ToString().StartsWith("FH"))//报损单、出库、发货单
                {
                    givDocDitals.Columns[11].Visible = false;
                    givDocDitals.Columns[12].Visible = false;
                    givDocDitals.Columns[13].Visible = false;
                    givDocDitals.Columns[14].Visible = false;
                    givDocDitals.Columns[15].Visible = false;
                    givDocDitals.Columns[16].Visible = false;
                }
                else if (Request["DocID"].ToString().StartsWith("RP") || Request["DocID"].ToString().StartsWith("RK") || Request["DocID"].ToString().StartsWith("TH"))//报溢单、入库单、退货单
                {
                    givDocDitals.Columns[9].Visible = false;
                    givDocDitals.Columns[10].Visible = false;
                    givDocDitals.Columns[13].Visible = false;
                    givDocDitals.Columns[14].Visible = false;
                    givDocDitals.Columns[15].Visible = false;
                    givDocDitals.Columns[16].Visible = false;
                }
            }
            else
            {
                ScriptHelper.SetAlert(Page, GetTran("000984", "该编号可能以不存在！！！"), "BrowseBills.aspx");
            }
        }
        Translations();
    }

    protected void givDoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
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
        return dt.AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime().ToString("yyyy-MM-dd");
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

    private void Translations()
    {
        this.TranControls(this.givDoc,
               new string[][]{
                    new string []{"000399","查看详细"},
                    new string []{"000407","单据编号"},
                     new string []{"000632","报溢仓库"},
                    new string []{"000635","报溢库位"},
                    new string []{"000636","报溢时间"},
                    new string []{"000045","期数"},
                    new string []{"000414","积分"},
                    new string []{"000041","总金额"}});

        this.TranControls(this.givDocDitals,
            new string[][]{
                new string[]{"000407","单据编号"},
                new string[]{"000558","产品编号"},
                new string[]{"000501","产品名称"},
                new string[]{"000505","数量"},
                new string[]{"000041","总金额"},
                new string[]{"000562","币种"},
                new string[]{"000113","总积分"},
                new string[]{"000045","期数"},
                new string[]{"000986","单据状态"},
                new string[]{"000386","仓库"},
                new string[]{"000390","库位"},
                new string[]{"000386","仓库"},
                new string[]{"000390","库位"},
                new string[]{"000704","调出仓库"},
                new string[]{"000705","调出库位"},
                new string[]{"000702","调入仓库"},
                new string[]{"000703","调入库位"}
            });
    }

    protected void givDocDitals_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "xabc=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=xabc");
        }
    }
}
