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
using System.Collections.Generic;
using BLL.Registration_declarations;
using Model.Orders;
using BLL.OrderBLL;
using DAL;
using BLL.CommonClass;
using Model.Other;
using Model;
using BLL.Logistics;
using Model.Const;
using BLL.other.Company;
using Model.Logistics;


public partial class Company_ProductOrders_RefundmentOrderForMemberDetails : BLL.TranslationBase
{
    private string _cmd = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        GetUrlParams();

        if (!IsPostBack)
        {
            BindReturnOrderBill();
            if (_cmd == CMDConst.Key_audit)
            {
                pnl_warehouse.Visible = true;
                this.btn_AuditSubmit.Visible = true;
                this.lbl_AuditTag.Visible = true;
                pnl_Back.Visible = false;
                this.btnBack0.Visible = true;
            }
            else
            {
                pnl_warehouse.Visible = false;
                this.btn_AuditSubmit.Visible = false;
                this.lbl_AuditTag.Visible = false;
                pnl_Back.Visible = true;
                this.btnBack0.Visible = false;
            }
        }
        Translations();
        if (string.IsNullOrEmpty(DocID))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007830", "参数传入不正确！") + "');</script>", false);
            Response.End();
        }

    }

    private void Translations()
    {
        this.TranControls(this.gv_OrderDetailsAll, new string[][]{
                    new string[]{"000079", "订单号"},
                    new string[]{"000263", "产品编码"},
                    new string[]{"000501", "产品名称"},
                    new string[]{"000503", "单价 "},
                    new string[]{"000414", "积分 "},
                    new string[]{"007774", "订单数量"},
                   new string[]{"007720", "剩余数量"},
                    new string[]{"001982", "退货数量"}
        });

        this.btn_AuditSubmit.Text = GetTran("007746", "确定");
        this.btnBack0.Text = GetTran("000421", "返回");
    }
    private void GetUrlParams()
    {
        if (Request.QueryString[ActionConst.Url_ID] != null)
        {
            DocID = Request.QueryString[ActionConst.Url_ID].ToString();
        }
        if (Request.QueryString[ActionConst.UrlParmsCMD] != null)
        {
            _cmd = Request.QueryString[ActionConst.UrlParmsCMD].ToString();
        }
    }
    /// <summary>
    /// 退货单号 
    /// </summary>
    public string DocID
    {
        get
        {
            return ViewState["DocID"] == null ? "" : ViewState["DocID"].ToString();
        }
        set { ViewState["DocID"] = value; }
    }
    /// <summary>
    /// 绑定退货单信息
    /// </summary>
    private void BindReturnOrderBill()
    {
        string docid = DocID;
        if (string.IsNullOrEmpty(docid))
            return;
        
        string msg = string.Empty;
        var refundmentOrderDocService = new RefundmentOrderDocBLL();
        RefundmentOrderDocModel refundEntity = refundmentOrderDocService.GetRefundmentOrderDocByDocID(docid, ref msg);
        if (refundEntity == null)
        {
            if (string.IsNullOrEmpty(msg))
                msg = GetTran("007831", "获取数据失败！");
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + msg + "');</script>", false);
            return;
        }
        string number = refundEntity.OwnerNumber_TX;
        var memberInfo = MemberInfoDAL.getMemberInfo(number);

        this.lbl_ApplyName.Text = refundEntity.Applicant_TX;

        decimal memberjj = 0;
        //Jackpot-ECTPay-Releasemoney-Out-membership lbl_MemberTotalMoney
        memberjj = (memberInfo.Jackpot - memberInfo.EctOut - memberInfo.Memberships);
        this.lbl_MemberJJ.Text = memberjj.ToString();
        this.lbl_MemberName.Text = memberInfo.Name;
        this.lbl_MemberNumber.Text = memberInfo.Number;
        DataTable dtMoneyPv = MemberInfoDAL.GetMemberOrderMoneyPVSum(refundEntity.OwnerNumber_TX);
        if (dtMoneyPv != null && dtMoneyPv.Rows.Count > 0)
        {
            this.lbl_MemberTotalMoney.Text = Convert.ToDouble(dtMoneyPv.Rows[0]["totalmoney"]).ToString("f2");
            this.lbl_MemberTotalMoney2.Text = this.lbl_MemberTotalMoney.Text;
            this.lbl_MemberTotalPV.Text = Convert.ToDouble(dtMoneyPv.Rows[0]["totalpv"]).ToString("f2");
            lab_mtpv.Text = Convert.ToDouble(dtMoneyPv.Rows[0]["totalpv"]).ToString("f2");
        }

        this.lbl_OrderIDS.Text = refundEntity.OriginalDocIDS;
        this.lbl_Phone.Text = refundEntity.MobileTele;

        this.lbl_refundmentTypeName.Text = RefundsTypeConst.GetRefundsTypeName((RefundsTypeEnum)refundEntity.RefundmentType_NR);
        if (refundEntity.RefundmentType_NR == (int)RefundsTypeEnum.RefundsUseBank)
        {//银行退款
            this.pnl_bankInfo.Visible = true;
            this.lbl_BankAccountName.Text = refundEntity.BankBookName;
            this.lbl_BankAccountNumber.Text = refundEntity.BankBook;
            this.lbl_BankBranch.Text = refundEntity.BankBranch;
            string bankName = string.Empty;
            string bankCode = refundEntity.BankCode;
            DataTable dtBankInfo = CommonDataBLL.GetCountryBankByBankCode(bankCode);
            if (dtBankInfo != null && dtBankInfo.Rows.Count > 0)
            {
                bankName = dtBankInfo.Rows[0]["BankName"].ToString();
            }
            this.lbl_BankName.Text = bankName;
        }
        else
        {
            this.pnl_bankInfo.Visible = false;
        }
        this.lbl_RegesterDate.Text = memberInfo.RegisterDate.ToString();
        this.lbl_Reson.Text = refundEntity.Cause_TX;
        string fullAddress = string.Empty;
        /*
        string CPCCode = refundEntity.CPCCode; //memberInfo.CPCCode.ToString();
        if (CPCCode != string.Empty)
        {
            CityModel cityModel = CommonDataDAL.GetCPCCode(CPCCode);
            if (cityModel != null)
            {
                fullAddress = cityModel.Country + cityModel.Province + cityModel.City;
            }
        }*/
        fullAddress += refundEntity.Address_TX;
        this.lbl_ReturnAddress.Text = fullAddress;
        this.lbl_ReturnDate.Text = refundEntity.RefundmentDate_DT.ToString();
        this.lbl_ReturnTotalMoney.Text = refundEntity.TotalMoney.ToString();
        this.lbl_ReturnTotalPV.Text = refundEntity.TotalPV.ToString();
        this.lblDealingOrderID.Text = refundEntity.DocID;


        #region 绑定退货明细
        DataTable dtDetails = refundmentOrderDocService.GetRefundmentOrderDetailsByDocID(refundEntity.DocID);
        if (dtDetails != null)
        {
            this.gv_OrderDetailsAll.DataSource = dtDetails;
            this.gv_OrderDetailsAll.DataBind();
        }
        #endregion

    }

    protected void btn_AuditSubmit_Click(object sender, EventArgs e)
    {
        int warehouseid = this.UCWareHouse1.WareHousrID;
        int depotseatID = this.UCWareHouse1.DepotSeatID;
        if (Session["Company"] == null)
            return;
        if (warehouseid < 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007832", "请选择产品退入的仓库！") + "');</script>", false);
            return;
        }
        if (depotseatID < 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007833", "请选择产品退入的库位！") + "');</script>", false);
            return;
        }
        string msg = string.Empty;
        string auditer = Session["Company"].ToString();
        var refundmentOrderDocService = new RefundmentOrderDocBLL();
        RefundmentOrderDocModel refundEntity = refundmentOrderDocService.GetRefundmentOrderDocByDocID(DocID, ref msg);
        refundEntity.WareHouseID = warehouseid;
        refundEntity.DepotSeatID = depotseatID;
        refundEntity.Auditer = auditer;
        refundEntity.AuditTime = DateTime.Now;
        refundEntity.StatusFlag_NR = (int)RefundmentOrderStatusEnum.AuditedUnPay;
        bool flag = refundmentOrderDocService.AuditRefundmentOrderDoc(refundEntity, ref msg);
        if (!flag)
        {
            if (string.IsNullOrEmpty(msg))
                msg = GetTran("001541", "操作失败！");
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + msg + "');</script>", false);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000858", "审核成功！") + "');window.location.href='RefundmentOrderForMemberList.aspx';</script>", false);
        }
    }
    protected void btnBack0_Click(object sender, EventArgs e)
    {
        string url = "RefundmentOrderForMemberList.aspx";
        Response.Redirect(url);
    }
}