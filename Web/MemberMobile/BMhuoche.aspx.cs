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

public partial class MemberMobile_BMhuoche : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('暂未开放.');window.location.href='index.aspx';</script>", false);
            var aa = Cxhyqx.GetCxhyqx(Session["member"].ToString(), 1);
            if (!aa)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("000847", "对不起,您没有权限！") + "');window.location.href='index.aspx'; </script>"); return;
            }

            IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
            TrainStationsListRequest req = new TrainStationsListRequest();
            TrainStationsListResponse response = client.Execute(req, BMEshenghuo.accessToken);
            if (!response.IsError)
            {
                qdz.DataSource = response.Stations;
                qdz.DataTextField = "name";
                qdz.DataValueField = "name";
                qdz.DataBind();
                zdz.DataSource = response.Stations;
                zdz.DataTextField = "name";
                zdz.DataValueField = "name";
                zdz.DataBind();
            }
        }
        Translations();
    }

    private void Translations()
    {

        this.TranControls(this.cyx, new string[][] { new string[] { "010475", "查询火车票商品列表" } });
        this.TranControls(this.cyxlb, new string[][] { new string[] { "000018", "选择" } });
        this.TranControls(this.ydcp, new string[][] { new string[] { "010476", "预订车票" } });
        this.TranControls(this.cz, new string[][] { new string[] { "010471", "充值" } });

    }
    public void chayouxi()
    {
        IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
        TrainItemsListRequest req = new TrainItemsListRequest();
        req.PageSize = 10;
        TrainItemsListResponse response = client.Execute(req, BMEshenghuo.accessToken);
        if (!response.IsError)
        {
            mc.DataSource = response.Items;
            mc.DataTextField = "itemName";
            mc.DataValueField = "itemName";
            mc.DataBind();
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


                 IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
                 TrainOrderPayRequest req = new TrainOrderPayRequest();
                 req.TradeNo = seatStatus.Value;
                 //req.OuterTid = "YCZ" + Model.Other.MYDateTime.ToYYMMDDHHmmssString();
                 TrainOrderPayResponse response = client.Execute(req, BMEshenghuo.accessToken);
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
        TrainLinesListRequest req = new TrainLinesListRequest();
        req.From = qdz.SelectedValue;
        req.To = zdz.SelectedValue;
        //req.ItemId = mc.SelectedValue;
        req.Date = txtBeginTime.Text;
        TrainLinesListResponse response = client.Execute(req, BMEshenghuo.accessToken);
        if (!response.IsError)
        {
            //hxxz.DataSource = response;
            //hxxz.DataTextField = "TrainNumber" + "TrainTypeName" + "SeatPrice" + "SeatName" + "RemainderTrainTickets" + "StartTime";
            //hxxz.DataValueField = "TrainNumber" + "StartTime";
            //hxxz.DataBind();
            //lab.Text = parPrice.Value;
            if (response.Trainlines == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("010488", "没有查到火车票请重新输入条件") + "');</script>", false);
                return;
            }
            List<Qianmi.Api.Domain.Elife.Trainline> alist = response.Trainlines;
            foreach (Qianmi.Api.Domain.Elife.Trainline item in alist)
            {
                List<Qianmi.Api.Domain.Elife.TrainSeat> ss = item.TrainSeats;
                hxxz.Items.Clear();
                for (int j = 0; j < response.Trainlines.Count; j++)
                {
                    if (ss != null)
                    {
                        for (int k = 0; k < ss.Count; k++)
                        {
                            hxxz.Items.Insert(j, alist[j].TrainNumber.ToString() + alist[j].TrainTypeName.ToString() + GetTran("010469", ".时间:") + alist[j].StartTime.ToString() + ss[k].SeatName.ToString() + GetTran("010470", ".票价:") + ss[k].SeatPrice.ToString() + GetTran("010489", ".余票:") + ss[k].RemainderTrainTickets.ToString());
                            hxxz.Items[j].Value = alist[j].TrainNumber.ToString() + alist[j].StartTime.ToString() + ss[k].SeatId.ToString(); ;
                        }
                    }
                    
                }
                cyx.Visible = false;
                mc.Enabled = false;
                qdz.Enabled = false;
                zdz.Enabled = false;
                txtBeginTime.ReadOnly = true;
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#yc3').show();</script>", false);

            }
        }
    }
    protected void ydcp_Click(object sender, EventArgs e)
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
        TrainOrderCreateRequest req = new TrainOrderCreateRequest();
        req.ItemIdTrain = mc.SelectedValue;//商品编号
        req.ContactName = dplxr.Text;
        req.ContactTel = lxrdh.Text;
        req.Date = txtBeginTime.Text;
        req.From = qdz.SelectedValue;
        req.To = zdz.SelectedValue;
        req.TrainNumber = flightNo.Value;
        req.StartTime = DepTime.Value;
        req.Passagers = ckxm.Text + "," + cksj.Text + "," + ckzj.Text;//乘客信息,以英文逗号分隔,
      
        TrainOrderCreateResponse response = client.Execute(req, BMEshenghuo.accessToken);
        BMOrderPW bo = new BMOrderPW();
        if (!response.IsError)
        {
            seatStatus.Value = response.TicketTrade.TradeNo;

        }
    }
    protected void hxxz_SelectedIndexChanged(object sender, EventArgs e)
    {
        IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
        TrainLinesListRequest req = new TrainLinesListRequest();
        req.From = qdz.SelectedValue;
        req.To = zdz.SelectedValue;
        //req.ItemId = mc.SelectedValue;
        req.Date = txtBeginTime.Text;
        TrainLinesListResponse response = client.Execute(req, BMEshenghuo.accessToken);
        if (!response.IsError)
        {
            
            List<Qianmi.Api.Domain.Elife.Trainline> alist = response.Trainlines;
            int i = 0;
            
            foreach (Qianmi.Api.Domain.Elife.Trainline item in alist)
            {
               
                
                
                List<Qianmi.Api.Domain.Elife.TrainSeat> ss = item.TrainSeats;
                if (ss != null)
                {

                    int k = 0;
                foreach (Qianmi.Api.Domain.Elife.TrainSeat item1 in ss)
                {
                    if (hxxz.SelectedValue == (alist[i].TrainNumber + alist[i].StartTime + alist[i].TrainSeats[k].SeatId.ToString()))
                    { 

                    //seatCode.Value = item1.SeatCode;
                    //airlineCode.Value = item1.AirlineCode;
                        parPrice.Value = alist[i].TrainSeats[k].SeatPrice;//座位票面价
                        seatMsg.Value = alist[i].TrainSeats[k].SeatName;//座位类型名称: 二等座, 一等座, 商务座等
                    //seatStatus.Value = item1.SeatStatus;
                    flightNo.Value = alist[i].TrainNumber;//车次号
                    FlightCompanyName.Value = alist[i].TrainTypeName;//车次类型
                    DepTime.Value = alist[i].StartTime;//发车时间
                    ArriTime.Value = alist[i].EndTime;//到达时间
                    lab.Text = parPrice.Value;
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#yc3').show();</script>", false);
                break;
                }
                
                    k++;
                }
                }
                i++;
            }
        }
    }
}