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


public partial class MemberMobile_BMqiche : BLL.TranslationBase
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
                //Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('设置成功!');window.location.href='AuthoritySet.aspx'</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000847", "对不起，您没有权限！") + "');window.location.href='index.aspx';</script>", false);
            }
            IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
            CoachStartStationsListRequest req = new CoachStartStationsListRequest();
            CoachStartStationsListResponse response = client.Execute(req, BMEshenghuo.accessToken);
            if (!response.IsError)
            {
                qdz.DataSource = response.Stations;
                qdz.DataTextField = "Province";
                qdz.DataValueField = "Province";
                qdz.DataBind();
                qrsf.Visible = true;
            }
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.qrsf, new string[][] { new string[] { "000064", "确认" } });
        this.TranControls(this.cyxlb, new string[][] { new string[] { "000018", "选择" } });
        this.TranControls(this.ydcp, new string[][] { new string[] { "010476", "预订车票" } });
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
                 CoachOrderPayRequest req = new CoachOrderPayRequest();
                 req.TradeNo = seatStatus.Value;
                 //req.OuterTid = "YCZ" + Model.Other.MYDateTime.ToYYMMDDHHmmssString();
                 CoachOrderPayResponse response = client.Execute(req, BMEshenghuo.accessToken);
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


    protected void cyxlb_Click(object sender, EventArgs e)
    {
        IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
        CoachLinesListRequest req = new CoachLinesListRequest();
        req.From = qdz.SelectedValue;
        req.To = zdz.SelectedValue;
        //req.ItemId = mc.SelectedValue;
        req.Date = txtBeginTime.Text;
        CoachLinesListResponse response = client.Execute(req, BMEshenghuo.accessToken);
        if (!response.IsError)
        {
            //hxxz.DataSource = response.CoachLines;
            //hxxz.DataTextField = "DptStation" + "ArrStation" + "DptTime" + "TicketPrice" + "TicketLeft";
            //hxxz.DataValueField = "CoachNO";
            //hxxz.DataBind();
            //lab.Text = parPrice.Value;
            if (response.CoachLines == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("010499", "没有查到车票请重新输入条件") + "');</script>", false);
                return;

            }
            List<Qianmi.Api.Domain.Elife.CoachLine> alist =response.CoachLines;
            
            foreach (Qianmi.Api.Domain.Elife.CoachLine item in alist)
            {
                hxxz.Items.Clear();
                for (int j = 0; j < response.CoachLines.Count; j++)
                {
                    //hxxz.Items.Add(item.Cities[j].City.Name);
                    hxxz.Items.Insert(j, alist[j].DptStation.ToString() + alist[j].ArrStation.ToString() + GetTran("010469", ".时间:") + alist[j].DptTime.ToString() + GetTran("010470", ".票价:") + alist[j].TicketPrice.ToString() + GetTran("010489", ".余票:") + alist[j].TicketLeft.ToString());
                    hxxz.Items[j].Value = alist[j].CoachNO.ToString();
                }


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
        CoachCreateBillRequest req = new CoachCreateBillRequest();
        req.StationCode = seatCode.Value;//商品编号
        req.ContactName = dplxr.Text;
        req.ContactTel = lxrdh.Text;
        req.DptDateTime = DepTime.Value;
        req.DptStation = seatMsg.Value;
        req.ArrStation = seatStatus.Value;
        req.Departure = cfcs.SelectedValue;
        req.Destination = zdz.SelectedValue;
        req.ItemIdCoach = mc.SelectedValue;
        req.CoachNO = flightNo.Value;
        req.SeatPrice = parPrice.Value;
        req.Passagers = ckxm.Text + "," + cksj.Text + "," + ckzj.Text;//乘客信息,以英文逗号分隔,

        CoachCreateBillResponse response = client.Execute(req, BMEshenghuo.accessToken);
        BMOrderPW bo = new BMOrderPW();
        if (!response.IsError)
        {
            seatStatus.Value = response.TicketTrade.TradeNo;
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alertt('" + GetTran("010500", "预订成功请点击支付!") + "');</script>", false);
        }
    }
    protected void hxxz_SelectedIndexChanged(object sender, EventArgs e)
    {
        IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
        CoachLinesListRequest req = new CoachLinesListRequest();
        req.From = qdz.SelectedValue;
        req.To = zdz.SelectedValue;
        //req.ItemId = mc.SelectedValue;
        req.Date = txtBeginTime.Text;
        CoachLinesListResponse response = client.Execute(req, BMEshenghuo.accessToken);
        if (!response.IsError)
        {

            List<Qianmi.Api.Domain.Elife.CoachLine> alist = response.CoachLines;
            int i=0;
            foreach (Qianmi.Api.Domain.Elife.CoachLine item in alist)
            {
                
                    if (hxxz.SelectedValue == alist[i].CoachNO.ToString())
                    {

                        seatCode.Value = item.StationCode.ToString();//站点编号
                        //airlineCode.Value = item.AirlineCode;
                        parPrice.Value = item.TicketPrice.ToString();//座位票面价
                        seatMsg.Value = item.DptStation.ToString();//座位类型名称: 二等座, 一等座, 商务座等..汽车出发站
                        seatStatus.Value = item.ArrStation.ToString(); ;//座位号...汽车到达站
                        flightNo.Value = item.CoachNO.ToString();//车次号
                        //FlightCompanyName.Value = item.TrainTypeName;//车次类型
                        DepTime.Value = item.DptDateTime.ToString();//发车时间
                        //ArriTime.Value = item.EndTime;//到达时间
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#yc11').show();$('#yc1').show();$('#yc22').show();$('#yc3').show();$('#yc4').show();</script>", false);
                    }
                i++;
                
            }
        }
    }
    protected void qdz_SelectedIndexChanged(object sender, EventArgs e)
    {
        IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
        CoachStartStationsListRequest req = new CoachStartStationsListRequest();
        CoachStartStationsListResponse response = client.Execute(req, BMEshenghuo.accessToken);
        if (!response.IsError)
        {
            List<Qianmi.Api.Domain.Elife.CoachStartStation> alist =response.Stations;
            int i = 0;
            foreach (Qianmi.Api.Domain.Elife.CoachStartStation item in alist)
            {
                
                    if (qdz.SelectedValue == alist[i].Province.ToString())
                    {
                        //cfcs.DataSource = item.Cities;
                        cfcs.Items.Clear();
                        for (int j = 0; j < item.Cities.Count; j++)
                        {
                            cfcs.Items.Add(item.Cities[j].City.Name);
                        }
                        //cfcs.DataValueField = item.Cities[0].City.Name;
                        //cfcs.DataBind();
                        if (item.Cities[0].Counties!=null)
                        {
                            xz.DataSource = item.Cities[0].Counties;
                            xz.DataTextField = "Name";
                            xz.DataValueField = "Code";
                            xz.DataBind();
                        }
                            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#yc1').show();</script>", false);
                        
                        
                    }
                    i++;
                
            }
            
        }
    }
    protected void qrsf_Click(object sender, EventArgs e)
    {
        IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
        CoachDestStationsListRequest req = new CoachDestStationsListRequest();
        req.StartStation = cfcs.SelectedValue;
        CoachDestStationsListResponse response = client.Execute(req, BMEshenghuo.accessToken);
        if (!response.IsError)
        {
            zdz.DataSource = response.Stations;
            zdz.DataTextField = "Name";
            zdz.DataValueField = "Name";    
            zdz.DataBind();
            qrsf.Visible = false;
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#yc11').show();$('#yc1').show();</script>", false);
        }

    }
    protected void zdz_SelectedIndexChanged(object sender, EventArgs e)
    {
        IOpenClient client = new DefaultOpenClient(BMEshenghuo.serverUrl, BMEshenghuo.appKey, BMEshenghuo.appSecret);
        CoachItemsListRequest req = new CoachItemsListRequest();
        req.PageSize = 10;
        CoachItemsListResponse response = client.Execute(req, BMEshenghuo.accessToken);

        if (!response.IsError)
        {
            mc.DataSource = response.Items;
            mc.DataTextField = "ItemName";
            mc.DataValueField = "ItemId";
            mc.DataBind();
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>$('#yc11').show();$('#yc1').show();$('#yc22').show();</script>", false);
        }
    }

}