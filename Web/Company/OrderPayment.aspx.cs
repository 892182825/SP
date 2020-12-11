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
using BLL.MoneyFlows;
using Model;
using System.Collections.Generic;
using BLL.Logistics;
using DAL;
using BLL.CommonClass;

public partial class Company_OrderPayment : BLL.TranslationBase 
{
    protected string msg;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceChAgainOrder);

        if (!IsPostBack)
        {
            tbStore.Style.Add("display", "none");
            OKButtonID.Attributes.Add("onclick", "javascript:return confirm('" + GetTran("005795", "确认要支付这些订单吗?") + "')");
        }
        Translations();
    }

    /// <summary>
    /// 翻译
    /// </summary>
    private void Translations()
    {
        this.TranControls(this.btnSelect, new string[][] { new string[] { "000340", "查询" } });
        this.TranControls(this.OKButtonID, new string[][] { new string[] { "000938", "订货付款" } });
        this.TranControls(this.gvStoreOrder, new string[][] { 
                new string[] { "000938", "支付" }, 
                new string[]{"000015","操作"},
                new string[] { "000079", "订单号" }, 
                new string[] { "000150", "店铺编号" }, 
                new string[] { "000322", "金额" }, 
                new string[] { "000414", "积分" }, 
                new string[] { "000943", "手续费" }, 
                new string[] { "000186", "支付方式" }, 
                new string[] { "000067", "订货日期" },
                new string[] { "000945", "订单详细信息" }
        });

    }




    //绑定页面控件
    private void Bind()
    {
        //显示店铺金额信息
        string[] storeInfo = CheckOutOrdersBLL.GetStoreTotalOrderGoodsMoney(GetStoreID());
        this.lblOrderMoney.Text = storeInfo[0];
        this.lblTurnMoney.Text = storeInfo[1];
        this.lblCurrency.Text = storeInfo[2];
        //绑定支付方式

        //绑定GridView
        this.gvStoreOrder.DataSource = CheckOutOrdersBLL.GetOrderGoodsList(GetStoreID());
        this.gvStoreOrder.DataBind();
        Translations();
        tbStore.Style.Add("display", "");
    }

    /// <summary>
    /// 转换日期
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    protected object GetOrderDate(object obj)
    {
        try
        {
            obj = Convert.ToDateTime(obj).AddHours(Convert.ToDouble(Session["WTH"]));
        }
        catch { }
        return obj;
    }

    private string GetStoreID()
    {
        return txtStoreID.Text;
    }

    /// <summary>
    /// 付款确定按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void OKButtonID_Click(object sender, EventArgs e)
    {
        //判断选中正确否(判断金额)
        int count = 0;
        int paycount = 0;
        IList<StoreOrderModel> orders = new List<StoreOrderModel>();
        string[] storeMoney = CheckOutOrdersBLL.GetStoreTotalOrderGoodsMoney(GetStoreID());
        double ordermoney = double.Parse(storeMoney[0]);//订货款
        double turnmonty = double.Parse(storeMoney[1]); //周转款
        double ordernum = 0;
        double trunnum = 0;
        for (int i = 0; i < this.gvStoreOrder.Rows.Count; i++)
        {
            CheckBox chb = this.gvStoreOrder.Rows[i].Controls[0].FindControl("ckSell") as CheckBox;

            if (chb.Checked)
            {
                StoreOrderModel order = new StoreOrderModel();
                order.StoreorderId = ((HtmlInputHidden)gvStoreOrder.Rows[i].FindControl("StoreOrderID")).Value;
                double totalMoney = double.Parse(((HtmlInputHidden)gvStoreOrder.Rows[i].FindControl("HidTotalMoney")).Value);
                if ((gvStoreOrder.Rows[i].FindControl("lblType") as Label).Text == GetTran("000274", "报单款订货"))
                {
                    order.OrderType = 0;
                    ordernum += totalMoney;
                    if (ordernum > ordermoney)
                    {
                        msg = "<script>alert('" + GetTran("000957", "报单款不足！") + "');</script>";
                        return;
                    }
                }
                else if ((gvStoreOrder.Rows[i].FindControl("lblType") as Label).Text == GetTran("000277", "周转款订货"))
                {
                    order.OrderType = 1;
                    trunnum += ordernum += totalMoney;
                    if (trunnum > turnmonty)
                    {
                        msg = "<script>alert('" + GetTran("000965", "周转款不足！") + "');</script>";
                        return;
                    }
                }
                else if ((gvStoreOrder.Rows[i].FindControl("lblType") as Label).Text == GetTran("000968", "支付宝"))
                {
                    paycount++;
                }
                order.TotalMoney = Convert.ToDecimal(totalMoney);
                orders.Add(order);
                count++;
            }
        }
        if (count == 0)
        {
            msg = "<script>alert('" + GetTran("000970", "请选择要支付的订单进行支付！") + "');</script>";
            return;
        }
        if (paycount > 0 && count != 1)//支付宝支付
        {
            msg = "<script>alert('" + GetTran("000972", "支付宝支付订单一次只能支付一张订单！") + "');</script>";
            return;
        }

        if (!CheckOutOrdersBLL.CheckLogicProductInventory(orders))
        {
            msg = "<script>alert('" + GetTran("000974", "公司库存不足，请通知公司及时进货！") + "')</script>";
            return;
        }

        if (paycount == 1)//支付宝支付
        {
            foreach (StoreOrderModel so in orders)
            {
                msg = "<script>window.open('../Store/payment/default.aspx?zongMoney=" + so.TotalMoney + "&TotalMoney=" + so.TotalMoney + "&TotalComm=0 &OrderID =" + so.StoreorderId + "&OnlineOrder=1');</script>";
            }
            return;
        }
        else
        {
            if (new CheckOutOrdersBLL().CheckOutStoreOrder(orders, GetStoreID()))
            {
                msg = "<script>alert('" + GetTran("000978", "支付成功！") + "')</script>";
                Bind();
            }
            else
            {
                msg = "<script>alert('" + GetTran("000979", "支付失败请重新支付！") + "')</script>";
            }
        }
    }

    //获取订货款方式
    protected object GetOrderType(object obj)
    {
        if (obj.ToString() == "0")
        {
            return GetTran("000277", "周转款订货");
        }
        else if (obj.ToString() == "1")
        {
            return GetTran("000274", "报单款订货");
        }
        else if (obj.ToString() == "2")
        {
            return GetTran("000968", "支付宝");
        }
        else if (obj.ToString() == "3")
        {
            return GetTran("000983", "快钱");
        }
        else
        {
            return "";
        }
    }

    private double OrderNum = 0.00;
    private double TrunNum = 0.00;
    //绑定是否可支付订单
    protected void gvStoreOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg;");

            string type = ((HtmlInputHidden)e.Row.FindControl("HidPaytype")).Value;
            string StoreOrderID = ((HtmlInputHidden)e.Row.FindControl("StoreOrderID")).Value;
            double ordermoney = double.Parse(this.lblOrderMoney.Text);
            double turnmonty = double.Parse(this.lblTurnMoney.Text);
            double totalMoney = double.Parse(((HtmlInputHidden)e.Row.FindControl("HidTotalMoney")).Value);

            if (type == "1")
            {
                if ((OrderNum + totalMoney) <= ordermoney)
                {
                    (e.Row.FindControl("ckSell") as CheckBox).Checked = true;
                    OrderNum += totalMoney;
                }
            }
            else if (type == "0")
            {

                if ((TrunNum + totalMoney) <= turnmonty)
                {
                    (e.Row.FindControl("ckSell") as CheckBox).Checked = true;
                    TrunNum += totalMoney;
                }
            }
            else if (type == "2")
            {
                (e.Row.FindControl("ckSell") as CheckBox).Visible = false;
                e.Row.FindControl("lbtnOk").Visible = false;
                Literal ltSell = e.Row.FindControl("ltSell") as Literal;
                string url = "../Store/payment/default.aspx?zongMoney=" + totalMoney + "&TotalMoney=" + totalMoney + "&TotalComm=0 &OrderID =" + StoreOrderID + "&OnlineOrder=1";
                ltSell.Text = "<a href='#' onclick='javascript:window.open(\"" + url + "\"); '> " + GetTran("000938", "支付") + " </a>";
            }
            else if (type == "3")
            {
                (e.Row.FindControl("ckSell") as CheckBox).Visible = false;
                e.Row.FindControl("lbtnOk").Visible = false;
                Literal ltSell = e.Row.FindControl("ltSell") as Literal;

                string url = "quickPay/quickPay.aspx?RemittanceType=" + (int)Model.Enum_RemittancesType.enum_StoreRemittance + "&hkid=" + StoreOrderID;
                ltSell.Text = "<a href='#' onclick='javascript:window.open(\"" + url + "\"); '> " + GetTran("000938", "支付") + " </a>";
            }

        }

    }

    //释放Session
    protected void gvStoreOrder_DataBound(object sender, EventArgs e)
    {
        if (Session["OrderNum"] != null)
        {
            Session.Remove("OrderNum");
        }
        if (Session["TrunNum"] != null)
        {
            Session.Remove("TrunNum");
        }
    }



    //查看详情
    protected void lkbtnShow_Click(object sender, EventArgs e)
    {
        LinkButton lk = sender as LinkButton;
        Response.Redirect("ShowOrderDetails.aspx?orderId=" + lk.CommandArgument+"&StoreID="+txtStoreID.Text.Trim());
    }

    protected void gvStoreOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string[] args = e.CommandArgument.ToString().Split(':');
        string orderId = args[0];
        string storeId = args[1];
        string isCheckOut = args[2];
        double totalMoney = Convert.ToDouble(args[3]);
        string payWay = args[4];
        if (e.CommandName == "OK")
        {
            //判断选中正确否(判断金额)
            int count = 0;
            int paycount = 0;
            IList<StoreOrderModel> orders = new List<StoreOrderModel>();
            string[] storeMoney = CheckOutOrdersBLL.GetStoreTotalOrderGoodsMoney(storeId);
            double ordermoney = double.Parse(storeMoney[0]);//订货款
            double turnmonty = double.Parse(storeMoney[1]); //周转款

            StoreOrderModel order = new StoreOrderModel();
            order.StoreorderId = orderId;
            if (payWay == "0")
            {
                order.OrderType = 0;
                if (totalMoney > ordermoney)
                {
                    msg = "<script>alert('" + GetTran("000957", "订货款不足！") + "');</script>";
                    return;
                }
            }
            else if (payWay == "1")
            {
                order.OrderType = 1;
                if (totalMoney > turnmonty)
                {
                    msg = "<script>alert('" + GetTran("000965", "周转款不足！") + "');</script>";
                    return;
                }
            }
            order.TotalMoney = Convert.ToDecimal(totalMoney);
            orders.Add(order);

            //if (!CheckOutOrdersBLL.CheckLogicProductInventory(orders))
            //{
            //    msg = "<script>alert('" + GetTran("000974", "公司库存不足，请通知公司及时进货！") + "')</script>";
            //    return;
            //}
            if (AddOrderDataDAL.OrderPayment(storeId, orderId, CommonDataBLL.OperateIP, 2, 1, 10, "管理员", "", 2, -1, 1, 1, "", 0, "") == 0)
            {
                msg = "<script>alert('" + GetTran("000978", "支付成功！") + "')</script>";
                Bind();
            }
            else
            {
                msg = "<script>alert('" + GetTran("000979", "支付失败请重新支付！") + "')</script>";
            }
            //if (payWay == "3")//支付宝支付
            //{
            //    msg = "<script>window.open('../Store/payment/default.aspx?zongMoney=" + totalMoney + "&TotalMoney=" + totalMoney + "&TotalComm=0 &OrderID =" + orderId + "&OnlineOrder=1');</script>";

            //    return;
            //}
            //else
            //{
               
            //}
        }
        else if (e.CommandName == "D")
        {
            //验证订单
            if (OrdersBrowseBLL.GetOrderIsExist(orderId))
            {
                msg = "<script>alert('" + GetTran("005993", "对不起，订单已被删除！") + "');</script>";
                Bind();
                return;
            }
            else if (OrdersBrowseBLL.GetOrderCheckState(orderId))
            {
                msg = "<script>alert('" + GetTran("005994", "对不起，该订单已支付，不能删除！") + "');</script>";
                Bind();
                return;
            }

            if (OrdersBrowseBLL.DelStoreOrderItem(orderId))
            {
                msg = "<script>alert('" + GetTran("000749", "删除成功！") + "');</script>";
            }
            else
            {
                msg = "<script>alert('" + GetTran("000417", "删除失败！") + "');</script>";
            }
            Bind();
        }

    }

    //根据店铺编号查询店铺所有订单
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        //选定当前页可以支付的所有订单
        //设置店铺编号必填               
        string storeIDStr = this.txtStoreID.Text.Trim();
        if (storeIDStr == "")
        {
            ScriptHelper.SetAlert(Page, GetTran("005796", "店铺编号必填！"));
            return;
        }
        //验证店铺是否存在
        int storeId = new OrderPaymentBLL().CheckStoreID(storeIDStr);
        if (storeId == 0)
        {
            ScriptHelper.SetAlert(Page, GetTran("005797", "指定店铺不存在！"));
            return;
        }
        //查询店铺剩余订货款
        DataRow row = new OrderPaymentBLL().GetLeftMoney(storeIDStr);
        if (row == null)
        {
            ScriptHelper.SetAlert(Page, GetTran("005797", "指定店铺不存在！！"));
            return;
        }
        Bind();
    }
}
