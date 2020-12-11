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

public partial class UserControl_DocPrintbillOut : System.Web.UI.UserControl
{
    private string docID;

    public string DocID
    {
        get { return docID; }
        set { docID = value; }
    }

    private string typname;
    public string TypeName
    {
        get { return typname; }
        set { typname = value; }
    }

    public BLL.TranslationBase tran = new TranslationBase();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pageshow();
            PrintInventoryDoc print = InventoryDocBLL.PrintInventoryDocDetails(DocID);
            if (print != null)
            {
                lblAddress.Text = Encryption.Encryption.GetDecipherAddress(print.Address);
                //lblAddress.Text = print.Address;
                lblBatchCode.Text = print.BatchCode;
                lblBillID.Text = print.DocID;
                //lblbilltypename.Text = print.DocTypeNames;
                lblCloseFlag.Text = print.CloseFlag == 1 ? tran.GetTran("000233", "是") : tran.GetTran("000235", "否");


                lblbilltypename.Text = print.DocTypeNames;

                if (TypeName != "")
                {
                    lblbilltypename.Text = TypeName;
                }
                lblCurrency.Text = print.CurrencyName;
                lblDocAuditer.Text = print.DocAuditer;
                if (print.DocAuditTime == DateTime.MinValue)
                    lblDocAuditTime.Text = "";
                else
                    lblDocAuditTime.Text = print.DocAuditTime.ToShortDateString();
                lblDocMaker.Text = print.DocMaker;
                if (print.DocMakeTime == DateTime.MinValue)
                    lblDocMakeTime.Text = "";
                else
                    lblDocMakeTime.Text = print.DocMakeTime.ToShortDateString();
                if (print.DocSecondAuditTime == DateTime.MinValue)
                    lblDocSecondAuditTime.Text = "";
                else
                    lblDocSecondAuditTime.Text = print.DocSecondAuditTime.ToShortDateString();



                if (TypeName != "")
                {
                    lblDocType.Text = TypeName;
                }
                else {

                    lblDocType.Text = print.DocTypeNames;
                }
                this.lblIsRubric.Text = print.IsRubric == 1 ? tran.GetTran("000233", "是") : tran.GetTran("000235", "否");
                lblNote.Text = print.Note;
                lblOperateNum.Text = CommonDataBLL.OperateBh;

                lblprintDate.Text = DateTime.Now.ToShortDateString();
                lblProvider.Text = print.ProviderName == "0" ? tran.GetTran("000221", "无") : print.ProviderName;
                lblStateFlag.Text = print.StateFlag == 1 ? tran.GetTran("001072", "有效") : tran.GetTran("001069", "无效");
                if (print.CloseDate == DateTime.MinValue)
                {
                    lblStateFlagtime.Text = "";
                }
                else
                {
                    //lblStateFlagtime.Text = print.CloseDate.ToShortDateString();
                }
                lblStoreID.Text = print.StoreID;
                lblTotalMoney.Text = print.TotalMoney.ToString();
                lbltotalpv.Text = print.Totalpv.ToString();
                lblWareHouse.Text = print.WareHouseName;
                lblCloseFlag.Text = print.CloseFlag == 1 ? tran.GetTran("000019", "关闭") : tran.GetTran("004105", "未关闭");
                lblSeatName.Text = print.SeatName;

                this.givShow.DataSource = InventoryDocBLL.DisplayInventoryDocDetails(DocID);
                this.givShow.DataBind();
            }
        }
        Translations();
    }

    private void pageshow()
    {
        string lanaguage = Session["LanguageCode"].ToString();

        litAddress.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litAddress");
        litBatchCode.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litBatchCode");
        litbillID.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litbillID");
        litCloseFlag.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litCloseFlag");
        litCurrency.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litCurrency");

        litDocAuditer.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litDocAuditer");
        litDocAuditTime.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litDocAuditTime");
        litDocMaker.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litDocMaker");
        litDocMakeTime.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litDocMakeTime");
        litDocPrintDate.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litDocPrintDate");

        litDocSecondAuditTime.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litDocSecondAuditTime");
        litDocType.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litDocType");
        this.litIsRubric.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litIsRubric");
        litNote.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litNote");
        litOperateNum.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litOperateNum");

        litProvider.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litProvider");
        litStateFlag.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litStateFlag");
        litStateFlagtime.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litStateFlagtime");
        litStoreID.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litStoreID");

        litTotalMoney.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litTotalMoney");
        littotalpv.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "littotalpv");
        litWareHouse.Text = TransTo.getTranToName(lanaguage, "DocPrintBillOut.ascx", "litWareHouse");
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
                    new string []{"000518","单位"},
                    new string []{"000355","仓库名称"},
                    new string []{"000357","库位名称"},
                    new string []{"000658","批次"},
                    new string []{"004161","总数量"},
                    new string []{"000041","总金额"},
                    new string []{"000113","总积分"}});
    }
}