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
using Model.Other;
using BLL.other.Company;
using Model;
using BLL.CommonClass;

using Encryption;
using BLL.Logistics;
using BLL;
/*
 * 修改者：  汪  华
 * 修改时间：2009-11-26
 * 完成时间：2009-11-26
 */

public partial class UserControl_DocPrintOrderOut : System.Web.UI.UserControl
{
    private string docID;

    public string DocID
    {
        get { return docID; }
        set { docID = value; }
    }
    public BLL.TranslationBase tran = new TranslationBase();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pageshow();
            DataTable print = InventoryDocBLL.PrintMemberOrderDocDetails(DocID);
            if (print != null)
            {
                lblbilltypename.Text = "发货单";
                lblAddress.Text = print.Rows[0]["Country"].ToString() + print.Rows[0]["Province"].ToString() + print.Rows[0]["City"].ToString() + Encryption.Encryption.GetDecipherAddress(print.Rows[0]["ConAddress"].ToString());

                lblBillID.Text = DocID;
                
                lblDocType.Text=Common.GetMemberOrderType(print.Rows[0]["OrderType"].ToString());

                lblCurrency.Text = print.Rows[0]["name"].ToString();
                lblDocAuditer.Text = HttpContext.Current.Session["Store"].ToString();

                lblDocAuditTime.Text = Convert.ToDateTime(print.Rows[0]["OrderDate"]).ToShortDateString();
                lblDocMaker.Text = HttpContext.Current.Session["Store"].ToString();

                lblDocMakeTime.Text = DateTime.Now.ToShortDateString();

                lblNote.Text = print.Rows[0]["Remark"].ToString();
                lblOperateNum.Text = HttpContext.Current.Session["Store"].ToString();

                lblprintDate.Text = DateTime.Now.ToShortDateString();

                lblStoreID.Text = HttpContext.Current.Session["Store"].ToString(); 
                lblTotalMoney.Text = Convert.ToDouble(print.Rows[0]["TotalMoney"]).ToString("0.00");
                lbltotalpv.Text = Convert.ToDouble(print.Rows[0]["totalpv"]).ToString("0.00");

                this.givShow.DataSource = InventoryDocBLL.DisplayMemberOrderDocDetails(DocID);
                this.givShow.DataBind();
            }
        }
        Translations();
    }

    private void pageshow()
    {
        string lanaguage = Session["LanguageCode"].ToString();

        litAddress.Text = TransTo.getTranToName(lanaguage, "DocPrintOrderOut.ascx", "litAddress");
        litbillID.Text = TransTo.getTranToName(lanaguage, "DocPrintOrderOut.ascx", "litbillID");
        litCurrency.Text = TransTo.getTranToName(lanaguage, "DocPrintOrderOut.ascx", "litCurrency");

        litDocAuditer.Text = TransTo.getTranToName(lanaguage, "DocPrintOrderOut.ascx", "litDocAuditer");
        litDocAuditTime.Text = TransTo.getTranToName(lanaguage, "DocPrintOrderOut.ascx", "litDocAuditTime");
        litDocMaker.Text = TransTo.getTranToName(lanaguage, "DocPrintOrderOut.ascx", "litDocMaker");
        litDocMakeTime.Text = TransTo.getTranToName(lanaguage, "DocPrintOrderOut.ascx", "litDocMakeTime");
        litDocPrintDate.Text = TransTo.getTranToName(lanaguage, "DocPrintOrderOut.ascx", "litDocPrintDate");

        litDocType.Text = TransTo.getTranToName(lanaguage, "DocPrintOrderOut.ascx", "litDocType");
        litNote.Text = TransTo.getTranToName(lanaguage, "DocPrintOrderOut.ascx", "litNote");
        litOperateNum.Text = TransTo.getTranToName(lanaguage, "DocPrintOrderOut.ascx", "litOperateNum");

        litStoreID.Text = TransTo.getTranToName(lanaguage, "DocPrintOrderOut.ascx", "litStoreID");

        litTotalMoney.Text = TransTo.getTranToName(lanaguage, "DocPrintOrderOut.ascx", "litTotalMoney");
        littotalpv.Text = TransTo.getTranToName(lanaguage, "DocPrintOrderOut.ascx", "littotalpv");
    }

    private void Translations()
    {
        tran.TranControls(this.givShow,
                new string[][]{
                    new string []{"000558","产品编号"},
                    new string []{"000501","产品名称"},
                    new string []{"000045","期数"},
                    new string []{"000627","优惠价格"},
                    new string []{"001883","优惠积分"},
                    new string []{"004161","总数量"},
                    new string []{"000041","总金额"},
                    new string []{"000113","总积分"}});
    }
}
