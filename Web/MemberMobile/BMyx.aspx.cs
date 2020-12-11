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

public partial class MemberMobile_BMyx : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        var aa = Cxhyqx.GetCxhyqx(Session["member"].ToString(), 1);
        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('暂未开放.');window.location.href='index.aspx';</script>", false);
        if (!aa)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000847", "对不起，您没有权限！") + "');window.location.href='index.aspx';</script>", false);
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.cyx, new string[][] { new string[] { "010514", "查询游戏列表" } });
        this.TranControls(this.cyxlb, new string[][] { new string[] { "000018", "选择" } });
        this.TranControls(this.yxkczxz, new string[][] { new string[] { "000018", "选择" } });
        this.TranControls(this.cz, new string[][] { new string[] { "010471", "充值" } });
    }
    public void chayouxi()
    { 
    IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
    BmGamesListRequest req = new BmGamesListRequest();
    BmGamesListResponse response=client.Execute(req, BMEshenghuo.accessToken);
    if (!response.IsError)
    {
        mc.DataSource = response.Games;
        mc.DataTextField = "GameName";
        mc.DataValueField = "GameId";
        mc.DataBind();
        cyx.Visible = false;
        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#yc1').show();$('#yc2').hide();</script>", false);


    }
    }
   
    public void chongzhi()
    {
         DataTable dt_one = DAL.DBHelper.ExecuteDataTable("select MobileTele,TotalRemittances-TotalDefray as xf from memberinfo where Number='" + Session["Member"].ToString() + "'");
         if (dt_one.Rows.Count > 0)
         {
             string ipn = dt_one.Rows[0]["MobileTele"].ToString();
             decimal xf = Convert.ToDecimal(dt_one.Rows[0]["xf"].ToString());
             if (xf < Convert.ToDecimal(sjh.Text))
             {

                 ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#tiaoz').show();document.getElementById('tiaoz').href = '../MemberMobile/MemberCZXF.aspx'; alertt('" + GetTran("000000", "账户金额不足，请先充值！") + "');</script>", false);

             }
             else
             {

                 if (jykxm.Text == "")
                 {
                     ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("010515", "游戏账号不能为空!") + "')</script>", false);
                 }
                 if (sjh.Text == "")
                 {
                     ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('数量不能为空!')</script>", false);
                 }
                 IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
                 BmDirectRechargeGamePayBillRequest req = new BmDirectRechargeGamePayBillRequest();
                 req.ItemId = yxkcz.SelectedValue;//商品号
                 req.ItemNum = sjh.Text;//手机号
                 req.RechargeAccount = jykxm.Text;//持卡人完整姓名
                 req.OuterTid = "YCZ" + Model.Other.MYDateTime.ToYYMMDDHHmmssString();
                 //req.Province = "全国";
                 //req.RechargeAmount = hth.Text;
                 BmDirectRechargeGamePayBillResponse response = client.Execute(req, BMEshenghuo.accessToken);
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

    protected void cz_Click(object sender, EventArgs e)
    {
        chongzhi();
    }

    protected void cyx_Click(object sender, EventArgs e)
    {
        chayouxi();
    }
    protected void cyxlb_Click(object sender, EventArgs e)
    {
        IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
        BmGameClassesListRequest req = new BmGameClassesListRequest();
        req.GameId = mc.SelectedValue;
         BmGameClassesListResponse response = client.Execute(req, BMEshenghuo.accessToken);
         if (!response.IsError)
         {
             yxkcz.DataSource = response.Classes;
             yxkcz.DataTextField = "ClassName";
             yxkcz.DataValueField = "ClassId";
             yxkcz.DataBind();
             ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#yc3').show();</script>", false);


         }
    }

    protected void yxkczxz_Click(object sender, EventArgs e)
    {
        IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
        BmGameItemsListRequest req = new BmGameItemsListRequest();
        req.ClassId = yxkcz.SelectedValue;
        BmGameItemsListResponse response = client.Execute(req, BMEshenghuo.accessToken);
        if (!response.IsError)
        {
            xzsp.DataSource = response.Items;
            xzsp.DataTextField = "ItemName";
            xzsp.DataValueField = "ItemId";
            xzsp.DataBind();
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#yc4').show();</script>", false);
        }
    }
}