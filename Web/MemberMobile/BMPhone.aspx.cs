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

public partial class MemberMobile_BMPhone : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

       // ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('更新调整,暂未开放.');window.location.href='index.aspx';</script>", false);
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        var aa = Cxhyqx.GetCxhyqx(Session["member"].ToString(),1);
        if (!aa)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000847", "对不起,您没有权限！") + "');window.location.href='First.aspx';</script>", false);
        }
        Translations();

    }
    private void Translations()
    {

        this.TranControls(this.cz, new string[][] { new string[] { "010486", "充值话费" } });
    }
    public void chaxun()
    {


        IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
        BmRechargeMobileGetPhoneInfoRequest req = new BmRechargeMobileGetPhoneInfoRequest();
        req.PhoneNo = phone.Text;
        BmRechargeMobileGetPhoneInfoResponse response = client.Execute(req, BMEshenghuo.accessToken);
        if (!response.IsError)
        {
            lab.Text = response.PhoneInfo.PhoneNo;
        }
        else
        {
            lab.Text = response.ErrMsg;
        }
    }
    public void chongzhi()
    {
         DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select MobileTele,TotalRemittances-TotalDefray as xf from memberinfo where Number='" + Session["Member"].ToString() + "'");
         if (dt_one.Rows.Count > 0)
         {
             string ipn = dt_one.Rows[0]["MobileTele"].ToString();
             decimal xf =Convert.ToDecimal( dt_one.Rows[0]["xf"].ToString());
             if (je.SelectedValue == "" || je.SelectedValue == null)
             {
                 je.SelectedValue = "0";
             }
             if (xf < Convert.ToDecimal(je.SelectedValue) / Common.GetnowPrice() / 7 * 4)
             {

                 ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#tiaoz').show();document.getElementById('tiaoz').href = '../MemberMobile/MemberCZXF.aspx'; alertt('" + GetTran("000000", "账户金额不足，请先充值！") + "');</script>", false);

             }
             else
             {
                 DataTable dt = DAL.DBHelper.ExecuteDataTable("select isnull(sum(saleAmount),0) as saleAmount from BMOuter where OuterType=1 and Number='" + Session["Member"].ToString() + "' and convert(varchar(6),ordertime,112)=convert(varchar(6),getdate(),112)");
                          if (dt.Rows.Count > 0)
                          {
                              double saleAmount = Convert.ToDouble(dt.Rows[0]["saleAmount"].ToString());
                              if (saleAmount == 80 || Convert.ToInt32(je.SelectedValue) > 10)
                              {
                                  ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000000", "因目前民生系统刚开放，话费每个账户每月只能充值100元") + "');setInterval('myInterval()',3000);function myInterval(){window.location.href='First.aspx'};</script>", false);
                                  return;
                              }
                              if (saleAmount == 50 && Convert.ToInt32(je.SelectedValue) > saleAmount)
                              {
                                  ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000000", "因目前民生系统刚开放，话费每个账户每月只能充值100元") + "');setInterval('myInterval()',3000);function myInterval(){window.location.href='First.aspx'};</script>", false);
                                  return;
                              }
                              if (saleAmount == 60 && Convert.ToInt32(je.SelectedValue) > 50)
                              {
                                  ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000000", "因目前民生系统刚开放，话费每个账户每月只能充值100元") + "');setInterval('myInterval()',3000);function myInterval(){window.location.href='First.aspx'};</script>", false);
                                  return;
                              }
                              if (saleAmount > 100 || saleAmount == 100)
                              {
                                  ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000000", "因目前民生系统刚开放，话费每个账户每月只能充值100元") + "');setInterval('myInterval()',3000);function myInterval(){window.location.href='First.aspx'};</script>", false);
                                  return;
                              }

                          }

        IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
        BmRechargeMobilePayBillRequest req = new BmRechargeMobilePayBillRequest();
        req.MobileNo = phone.Text;//手机号
        req.RechargeAmount =je.SelectedValue;//充值金额
        req.OuterTid = "PCZ" + Model.Other.MYDateTime.ToYYMMDDHHmmssString();
       // req.Callback = je.SelectedValue;
        BmRechargeMobilePayBillResponse response = client.Execute(req, BMEshenghuo.accessToken);
        BMOrder bo=new BMOrder();
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
            bo.EPmny = Convert.ToDecimal(response.OrderDetailInfo.SaleAmount) /Common.GetnowPrice()/7*4;
            bo.Hl = 4;
            bo.BillId = response.OrderDetailInfo.BillId;
            bo.RevokeMessage = "";
            bo.RechargeState = Convert.ToInt32(response.OrderDetailInfo.RechargeState);
            bo.OuterType = 1;
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
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + response.SubErrMsg + "')</script>", false);

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
}