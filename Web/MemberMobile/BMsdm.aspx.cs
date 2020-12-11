using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Qianmi.Api;
using Qianmi.Api.Request;
using Qianmi.Api.Response;
using BLL;
using BLL.MoneyFlows;
using BLL.CommonClass;
using System.Data;

public partial class MemberMobile_BMsdm : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        var aa = Cxhyqx.GetCxhyqx(Session["member"].ToString(), 1);
        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('更新调整,暂未开放.');window.location.href='index.aspx';</script>", false); return;
        if (!aa)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000847", "对不起，您没有权限！") + "');window.location.href='index.aspx';</script>", false);
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.cx, new string[][] { new string[] { "000340", "查询" } });
        this.TranControls(this.cz, new string[][] { new string[] { "010471", "充值" } });
        this.sheng.Attributes.Add("placeholder", GetTran("010508", "后面不用带省"));
        this.shi.Attributes.Add("placeholder", GetTran("010509", "后面不用带市"));
    }
    public void chaxun()
    {

        if (sheng.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("010516", "省份不能为空!") + "')</script>", false); 
        }
        if (shi.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("010517", "城市不能为空!") + "')</script>", false);
        }
        IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
        BmDirectRechargeWaterCoalItemListRequest req = new BmDirectRechargeWaterCoalItemListRequest();
        req.ItemName = sheng.Text;
        req.City = shi.Text;
        req.PageSize = 10;
        BmDirectRechargeWaterCoalItemListResponse response = client.Execute(req, BMEshenghuo.accessToken);
        if (!response.IsError)
        {
            lb.DataSource = response.Items;
            lb.DataTextField = "ItemName";
            lb.DataValueField = "ItemId";
            lb.DataBind();
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#yc').show()</script>", false);
            sheng.ReadOnly = true;
            shi.ReadOnly = true;
            cx.Visible = false;

            if (sheng.Text == GetTran("010518", "江苏"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#hth').hide()</script>", false);
            }

        }

    }
    public void chongzhi()
    {
         DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select MobileTele,TotalRemittances-TotalDefray as xf from memberinfo where Number='" + Session["Member"].ToString() + "'");
         if (dt_one.Rows.Count > 0)
         {
             string ipn = dt_one.Rows[0]["MobileTele"].ToString();
             decimal xf = Convert.ToDecimal(dt_one.Rows[0]["xf"].ToString());
             if (xf < Convert.ToDecimal(ysje.Text) / Common.GetnowPrice() / 7 * 4)
             {

                 ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#tiaoz').show();document.getElementById('tiaoz').href = '../MemberMobile/MemberCZXF.aspx'; alertt('" + GetTran("000000", "账户金额不足，请先充值！") + "');</script>", false);
                 return;
             }
             else
             {
                 DataTable dt = DAL.DBHelper.ExecuteDataTable("select isnull(sum(saleAmount),0) as saleAmount from BMOuter where OuterType=2 and Number='" + Session["Member"].ToString() + "' and convert(varchar(6),ordertime,112)=convert(varchar(6),getdate(),112)");
                 if (dt.Rows.Count > 0)
                 {
                     double saleAmount = Convert.ToDouble(dt.Rows[0]["saleAmount"].ToString());
                     if (saleAmount > 100)
                     {
                         ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000000", "因目前民生系统刚开放，水电煤每个账户每月只能充值100元") + "');window.location.href='First.aspx';</script>", false);
                         return;
                     }
                     if (Convert.ToInt32(ysje.Text) + saleAmount>100)
                     {
                         ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000000", "因目前民生系统刚开放，水电煤每个账户每月只能充值100元") + "');window.location.href='First.aspx';</script>", false);
                         return; 
                     }
                     if (Convert.ToInt32(ysje.Text) > 100)
                     {
                         ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000000", "因目前民生系统刚开放，水电煤每个账户每月只能充值100元") + "');window.location.href='First.aspx';</script>", false);
                         return;
                     }

                 }

                 if (jfzh.Text == "")
                 {
                     ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("010519", "缴费账户不能为空!") + "')</script>", false);
                 }
                 if (ysje.Text == "")
                 {
                     ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("010520", "原始金额不能为空!") + "')</script>", false);
                 }
                 IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
                 BmDirectRechargeLifeRechargePayBillRequest req = new BmDirectRechargeLifeRechargePayBillRequest();
                 req.ItemId = lb.SelectedValue;//商品号
                 req.ItemNum = ysje.Text;//充值金额
                 req.OuterTid = "SCZ" + Model.Other.MYDateTime.ToYYMMDDHHmmssString();
                 req.RechargeAccount = jfzh.Text;
                 req.ContractNo = hth.Text;
                 BmDirectRechargeLifeRechargePayBillResponse response = client.Execute(req, BMEshenghuo.accessToken);
                 BMOrder bo = new BMOrder();
                 if (!response.IsError)
                 {
                     bo.OurterTid = response.OrderDetailInfo.OuterTid;
                     bo.OrderTime = DateTime.Now;
                     bo.OperateTime = DateTime.Now;
                     bo.Number = Session["Member"].ToString();
                     bo.RechargeAccount = response.OrderDetailInfo.RechargeAccount;
                     bo.ItemName = response.OrderDetailInfo.ItemName;
                     bo.ItemNum = response.OrderDetailInfo.ItemNum;
                     bo.SaleAmount = Convert.ToDecimal(response.OrderDetailInfo.SaleAmount);
                     bo.EPmny = Convert.ToDecimal(response.OrderDetailInfo.SaleAmount) / Common.GetnowPrice() / 7 * 4;
                     bo.Hl = 4;
                     bo.BillId = response.OrderDetailInfo.BillId;
                     bo.RevokeMessage = "";
                     bo.RechargeState = Convert.ToInt32(response.OrderDetailInfo.RechargeState);
                     bo.OuterType = 2;
                     BMOrderBLL bob = new BMOrderBLL();
                     int abo = bob.AddBMOrder(bo);
                     if (abo == 1)
                     {
                         ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000000", "账户金额不足!") + "')</script>", false);
                         return;
                     }
                     else
                     {
                         //DAL.CommonDataDAL.EncryptionAccount(1, Session["Member"].ToString(), "E生活操作", "", CommonDataBLL.OperateIP);
                         ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#tiaoz').show();document.getElementById('tiaoz').href = '../MemberMobile/First.aspx'; alertt('" + GetTran("010487", "订单已支付，请稍后查询！") + "');</script>", false);
                     }

                 }
             }
         }

    }
    protected void cx_Click(object sender, EventArgs e)
    {
        chaxun();
    }
    protected void cz_Click(object sender, EventArgs e)
    {
        chongzhi();
    }
    protected void ysje_TextChanged(object sender, EventArgs e)
    {
        decimal ysj = Convert.ToDecimal(ysje.Text);
        lab.Text = Convert.ToDecimal(ysj /3).ToString(); ;
    }
}