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

public partial class MemberMobile_BMfeiji : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
         
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
            AirStationsListRequest req = new AirStationsListRequest();
            AirStationsListResponse response = client.Execute(req, BMEshenghuo.accessToken);
            var aa = Cxhyqx.GetCxhyqx(Session["member"].ToString(), 1);
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('暂未开放.');window.location.href='index.aspx';</script>", false);
            if (!aa)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000847", "对不起，您没有权限！") + "');window.location.href='index.aspx';</script>", false);
            }
            if (!response.IsError)
            {
                qdz.DataSource = response.Stations;
                qdz.DataTextField = "name";
                qdz.DataValueField = "Code";
                qdz.DataBind();
                zdz.DataSource = response.Stations;
                zdz.DataTextField = "name";
                zdz.DataValueField = "Code";
                zdz.DataBind();
            }
        }
        Translations();
    }
    private void Translations()
    {

        this.TranControls(this.cyx, new string[][] { new string[] { "010472", "查询飞机票商品列表" } });
        this.TranControls(this.cyxlb, new string[][] { new string[] { "000018", "选择" } });

    }
    public void chayouxi()
    {
        IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
        AirItemsListRequest req = new AirItemsListRequest();
        req.PageSize = 10;
        AirItemsListResponse response = client.Execute(req, BMEshenghuo.accessToken);
        if (!response.IsError)
        {
            mc.DataSource = response.Items;
            mc.DataTextField = "itemName";
            mc.DataValueField = "itemId";
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
             if (xf < Convert.ToDecimal(lab.Text))
             {

                 ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#tiaoz').show();document.getElementById('tiaoz').href = '../MemberMobile/MemberCZXF.aspx'; alertt('" + GetTran("000000", "账户金额不足，请先充值！") + "');</script>", false);

             }
             else
             {


                 if (ckxm.Text == "")
                 {
                     ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("010464", "乘客姓名不能为空!") + "')</script>", false);
                 }
                 if (cksj.Text == "")
                 {
                     ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("010465", "乘客手机号不能为空!") + "')</script>", false);
                 }
                 if (ckzj.Text == "")
                 {
                     ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("010466", "乘客证件号不能为空!") + "')</script>", false);
                 }
                 if (dplxr.Text == "")
                 {
                     dplxr.Text = ckxm.Text;
                 }
                 if (lxrdh.Text == "")
                 {
                     lxrdh.Text = cksj.Text;
                 }

                 IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
                 AirOrderPayBillRequest req = new AirOrderPayBillRequest();
                 req.SeatCode = cwxz.SelectedValue;//舱位编号
                 req.Passagers = ckxm.Text + "," + cksj.Text + "," + ckzj.Text;//乘客信息,以英文逗号分隔,
                 req.ItemId = mc.SelectedValue;//商品编号
                 req.ContactName = dplxr.Text;
                 req.ContactTel = lxrdh.Text;
                 req.Date = txtBeginTime.Text;
                 req.From = qdz.SelectedValue;
                 req.To = zdz.SelectedValue;
                 req.CompanyCode = airlineCode.Value;
                 req.FlightNo = flightNo.Value;
                 //req.OuterTid = "YCZ" + Model.Other.MYDateTime.ToYYMMDDHHmmssString();
                 AirOrderPayBillResponse response = client.Execute(req, BMEshenghuo.accessToken);
                 BMOrderPW bo = new BMOrderPW();
                 if (!response.IsError)
                 {
                     bo.OurterTid = "FJP" + Model.Other.MYDateTime.ToYYMMDDHHmmssString(); ;
                     bo.Ctime = DateTime.Now;
                     bo.Etime = DateTime.Now;
                     bo.Number = Session["Member"].ToString();
                     List<Qianmi.Api.Domain.Elife.TicketOrder> alist = response.TicketTrade.TicketOrders;
                     foreach (Qianmi.Api.Domain.Elife.TicketOrder item in alist)
                     {
                         bo.ItemId = item.ItemId;
                         bo.PassengerName = item.PassengerName + "," + item.PassengerTel + "," + item.IdcardNo;
                         bo.PassengerTel = "";
                     }

                     bo.StartTime = Convert.ToDateTime(response.TicketTrade.StartTime);
                     bo.EPmny = Convert.ToDecimal(response.TicketTrade.TotalPayCash) / Common.GetnowPrice() / 7 * 4;
                     bo.Hl = 4;
                     bo.StartStation = response.TicketTrade.StartStation;
                     bo.RecevieStation = response.TicketTrade.RecevieStation;
                     bo.FlightCompanyName = FlightCompanyName.Value;
                     bo.DepTime = Convert.ToDateTime(DepTime.Value);
                     bo.ArriTime = Convert.ToDateTime(ArriTime.Value);
                     bo.FlightCompanyCode = airlineCode.Value;
                     bo.FlightNo = flightNo.Value;
                     bo.SeatMsg = seatMsg.Value;
                     bo.SeatStatus = seatStatus.Value;
                     bo.ParPrice = response.TicketTrade.TotalFacePrice;
                     bo.TotalPayCash = response.TicketTrade.TotalPayCash;
                     bo.OrderType = 2;
                     bo.Title = response.TicketTrade.Title;
                     BMOrderBLL bob = new BMOrderBLL();
                     int abo = bob.AddBMOrderPW(bo);
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
        AirLinesListRequest req = new AirLinesListRequest();
        req.From = qdz.SelectedValue;
        req.To = zdz.SelectedValue;
        req.ItemId = mc.SelectedValue;
        req.Date = txtBeginTime.Text;
        AirLinesListResponse response = client.Execute(req, BMEshenghuo.accessToken);
        if (!response.IsError)
        {
            //hxxz.DataSource = response.Airlines;
            //hxxz.DataTextField = "FlightNo" + "SeatMsg" + "SettlePrice" + "DepTime";
            //hxxz.DataValueField = "FlightNo";
            //hxxz.DataBind();
            if (response.Airlines == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("010468", "没有查到机票请重新输入条件") + "');</script>", false);
                return;
            }
            List<Qianmi.Api.Domain.Elife.Airline> alist = response.Airlines;
            
            
           foreach (Qianmi.Api.Domain.Elife.Airline item in alist)
	   {

        List<Qianmi.Api.Domain.Elife.AirSeat> ss = item.AirSeats;
         foreach (Qianmi.Api.Domain.Elife.AirSeat item1 in ss)
	   {
           hxxz.Items.Clear();
           for (int j = 0; j < response.Airlines.Count; j++)
           {
               
                   //hxxz.Items.Add(item.Cities[j].City.Name);
               hxxz.Items.Insert(j, alist[j].FlightNo.ToString() + GetTran("010469", ".时间:") + alist[j].DepTime.ToString());
                   hxxz.Items[j].Value = alist[j].FlightNo.ToString()+ alist[j].DepTime.ToString();
              
           }
           
         }
         
	   }

           //lab.Text = parPrice.Value;

           mc.Enabled = false;
           qdz.Enabled = false;
           zdz.Enabled = false;
           txtBeginTime.Enabled = true;
           cyxlb.Visible = false;
           ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#yc3').show();$('#yc1').show();</script>", false);


        }
    }
    protected void hxxz_SelectedIndexChanged(object sender, EventArgs e)
    {
        IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
        AirLinesListRequest req = new AirLinesListRequest();
        req.From = qdz.SelectedValue;
        req.To = zdz.SelectedValue;
        req.ItemId = mc.SelectedValue;
        req.Date = txtBeginTime.Text;
        AirLinesListResponse response = client.Execute(req, BMEshenghuo.accessToken);
        if (!response.IsError)
        {
            //hxxz.DataSource = response.Airlines;
            //hxxz.DataTextField = "FlightNo" + "SeatMsg" + "SettlePrice" + "DepTime";
            //hxxz.DataValueField = "FlightNo";
            //hxxz.DataBind();
            List<Qianmi.Api.Domain.Elife.Airline> alist = response.Airlines;

            int i = 0;
            
            foreach (Qianmi.Api.Domain.Elife.Airline item in alist)
            {
                
                    List<Qianmi.Api.Domain.Elife.AirSeat> ss = item.AirSeats;
                    
                    foreach (Qianmi.Api.Domain.Elife.AirSeat item1 in ss)
                    {
                        
                        if (hxxz.SelectedValue == alist[i].FlightNo.ToString() + alist[i].DepTime.ToString())
                      {
                          cwxz.Items.Clear();
                          if (ss.Count == null || ss.Count == 0)
                          {
                              cwxz.Items.Insert(0, "暂无余票");

                          }
                          for (int j = 0; j < ss.Count; j++)
                          {
                              cwxz.Items.Insert(j, alist[i].AirSeats[j].SeatMsg + GetTran("010470", ".票价:") + alist[i].AirSeats[j].ParPrice.ToString());
                              cwxz.Items[j].Value = alist[i].AirSeats[j].SeatCode.ToString();
                          }
                         
                    }
                        
                        
                    
                }
                i++;
            }

            lab.Text = parPrice.Value;
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#yc3').show();$('#yc1').show();</script>", false);


        }
    }
    protected void cwxz_SelectedIndexChanged(object sender, EventArgs e)
    {
        IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
        AirLinesListRequest req = new AirLinesListRequest();
        req.From = qdz.SelectedValue;
        req.To = zdz.SelectedValue;
        req.ItemId = mc.SelectedValue;
        req.Date = txtBeginTime.Text;
        AirLinesListResponse response = client.Execute(req, BMEshenghuo.accessToken);
        if (!response.IsError)
        {
            //hxxz.DataSource = response.Airlines;
            //hxxz.DataTextField = "FlightNo" + "SeatMsg" + "SettlePrice" + "DepTime";
            //hxxz.DataValueField = "FlightNo";
            //hxxz.DataBind();
            List<Qianmi.Api.Domain.Elife.Airline> alist = response.Airlines;

            int i = 0;

            foreach (Qianmi.Api.Domain.Elife.Airline item in alist)
            {

                List<Qianmi.Api.Domain.Elife.AirSeat> ss = item.AirSeats;

                foreach (Qianmi.Api.Domain.Elife.AirSeat item1 in ss)
                {
                   
                    if (hxxz.SelectedValue == alist[i].FlightNo.ToString() + alist[i].DepTime.ToString())
                    {
                        for (int j = 0; j < ss.Count; j++)
                        {
                            if (cwxz.SelectedValue == alist[i].AirSeats[j].SeatCode.ToString())
                            {
                                seatCode.Value = alist[i].AirSeats[j].SeatCode;
                                airlineCode.Value = alist[i].AirSeats[j].AirlineCode;
                                parPrice.Value = alist[i].AirSeats[j].ParPrice.ToString();
                                seatMsg.Value = alist[i].AirSeats[j].SeatMsg;
                                seatStatus.Value = alist[i].AirSeats[j].SeatStatus;
                                flightNo.Value = alist[i].FlightNo;
                                FlightCompanyName.Value = alist[i].FlightCompanyName;
                                DepTime.Value = alist[i].DepTime;
                                ArriTime.Value = alist[i].ArriTime;
                            }
                        }

                    }


                }
                i++;
            }

            lab.Text = parPrice.Value;
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#yc3').show();$('#yc1').show();</script>", false);


        }
    }
}