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

public partial class MemberMobile_BMjyk : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        var aa = Cxhyqx.GetCxhyqx(Session["member"].ToString(), 1);
        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('更新调整,暂未开放.');window.location.href='index.aspx';</script>", false);
        if (!aa)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000847", "对不起,您没有权限！") + "');window.location.href='index.aspx';</script>", false);
        }
        Translations();
    }
    private void Translations()
    {

        this.TranControls(this.mc, new string[][] { new string[] { "010481", "中石化" }, new string[] { "010482", "中石油" } });
        this.TranControls(this.cz, new string[][] { new string[] { "000340", "充值" } });
        this.TranControls(this.cx, new string[][] { new string[] { "000340", "查询" } });
       

    }
    public void chaxun()
    {

        if (kh.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("010490", "卡号不能为空!") + "')</script>", false);
        }
        
        IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
        BmGasCardItemListRequest req = new BmGasCardItemListRequest();
        req.ItemName = mc.SelectedValue;
        req.PageSize = 10;
        BmGasCardItemListResponse response = client.Execute(req, BMEshenghuo.accessToken);
        if (!response.IsError)
        {
            czje.DataSource = response.Items;
            czje.DataTextField = "ItemName";
            czje.DataValueField = "ItemId";
            czje.DataBind();
            mc.Enabled = false;
            kh.ReadOnly = true;
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#yc').show();</script>", false);
           

        }
        else {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("010491", "卡号不正确") + "')</script>", false);
        }

    }
    public void chongzhi()
    {

         DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select MobileTele,TotalRemittances-TotalDefray as xf from memberinfo where Number='" + Session["Member"].ToString() + "'");
         if (dt_one.Rows.Count > 0)
         {
             string ipn = dt_one.Rows[0]["MobileTele"].ToString();
             decimal xf = Convert.ToDecimal(dt_one.Rows[0]["xf"].ToString());
             if (xf < Convert.ToDecimal(czje.SelectedValue))
             {

                 ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#tiaoz').show();document.getElementById('tiaoz').href = '../MemberMobile/MemberCZXF.aspx'; alertt('" + GetTran("000000", "账户金额不足，请先充值！") + "');</script>", false);

             }
             else
             {


                 if (jykxm.Text == "")
                 {
                     ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("010492", "加油卡完整姓名不能为空!") + "')</script>", false);
                 }
                 if (sjh.Text == "")
                 {
                     ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("010493", "手机号不能为空!") + "')</script>", false);
                 }
                 IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
                 BmGasCardPayBillRequest req = new BmGasCardPayBillRequest();
                 req.ItemId = czje.SelectedValue;//商品号
                 req.GasCardTel = sjh.Text;//手机号
                 req.GasCardName = jykxm.Text;//持卡人完整姓名
                 req.OuterTid = "YCZ" + Model.Other.MYDateTime.ToYYMMDDHHmmssString();
                 req.Province = GetTran("010494", "全国");
                 //req.RechargeAmount = hth.Text;
                 BmGasCardPayBillResponse response = client.Execute(req, BMEshenghuo.accessToken);
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

    protected void czje_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal ysj = Convert.ToDecimal(czje.SelectedValue);
        lab.Text = Convert.ToDecimal(ysj / 3).ToString(); ;
    }
}