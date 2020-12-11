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

public partial class Company_ProductOrders_RefundmentOrderBrowseForMember : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        GetUrlParams();
        if (!IsPostBack)
        {
            ddlContion_SelectedIndexChanged(null, null);
            btnSearch_Click(null, null);
        }

        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.btnSearch, new string[][] { new string[] { "000340", "查询" } });
        this.TranControls(this.btn_ReturnConfrim, new string[][] { new string[] { "007746", "确定退货" } });
        // this.TranControls(this.btn_ArgeeConfirm, new string[][] { new string[] { "007738", "同意并确认" } });
        this.TranControls(this.ddlType, new string[][] { new string[] { "002055", "报单期数" }, new string[] { "000780", "审核期数" } });
        //this.TranControls(this.ddlContion, new string[][] { new string[] { "000079", "订单号" }, new string[] { "000322", "金额" }, new string[] { "000414", "积分" }, new string[] { "000037", "服务机构编号" }, new string[] { "000024", "会员编号" }, new string[] { "000025", "会员姓名" } });
        this.TranControls(this.gv_browOrder, new string[][] { 
            new string[] { "002224", "退货" },
            new string[] { "000079", "订单号" } ,
            new string[] { "000024", "会员编号" } ,
          new string[]{"000025","会员姓名"},
            new string[]{"007743","报单服务机构编号"},
             new string[]{"000455","报单类型"},
              new string[]{"000045","期数"},
               new string[]{"000322","金额"},
                new string[]{"000414","积分"},
                 new string[]{"005942","报单时间"},
                 new string[]{"000440","查看"}
        });
        this.btn_ConfrimDetails.Text = GetTran("000434", "确定");
        this.btn_Cancel.Text = GetTran("001614", "取消");
        this.btn_backtoOrderList.Text = GetTran("000421", "返回");

        this.rbtn_1.Text = GetTran("007789", "退还至电子账户");
        this.rbtn_0.Text = GetTran("007800", "现金退还");
        this.rbtn_2.Text = GetTran("007801", "退还银行账户");

        this.btn_ConfrimAndSubmit.Text = this.btn_ConfrimDetails.Text = GetTran("000434", "确定");
        this.btn_back.Text = GetTran("000421", "返回");

    }

    private void GetUrlParams()
    {
        if (Request.QueryString["number"] != null)
        {
            MemberID = Request.QueryString["number"].ToString();
            //移除A.StoreId，A.Number，A.Name
            string removeStr = "A.StoreId，A.Number，A.Name".ToLower();
            List<ListItem> removes = new List<ListItem>();
            foreach (ListItem li in this.ddlContion.Items)
            {
                if (removeStr.Contains(li.Value.ToLower().Trim()))
                {
                    removes.Add(li);
                }
            }
            foreach (ListItem li in removes)
            {
                this.ddlContion.Items.Remove(li);
            }
        }

    }
    public string GetRegisterDate(string rdate)
    {
        return Convert.ToDateTime(rdate).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();
    }

    /// <summary>
    /// 会员编号 
    /// </summary>
    public string MemberID
    {
        get
        {
            return ViewState["MemberID"] == null ? "" : ViewState["MemberID"].ToString();
        }
        set { ViewState["MemberID"] = value; }
    }

    /// <summary>
    ///  查询条件
    /// </summary>
    public string Condition
    {
        get { return ViewState["Condition"] == null ? "" : ViewState["Condition"].ToString(); }
        set { ViewState["Condition"] = value; }
    }
    //public DataTable excelTable
    //{
    //    get { return (DataTable)ViewState["excelTable"]; }
    //    set { ViewState["excelTable"] = value; }
    //}
    private void GridViewBind()
    {
        string condtion = Condition;
        string table = "  MemberInfo a inner join MemberOrder b on B.Number=A.Number inner join "  // and b.OrderStatus_NR=" + (int)OrderStatusEnum.Normal + " 
            + " ( select distinct orderid,number from MemberDetails where (isnull(Quantity,0)-isnull(QuantityReturned,0)-isnull(QuantityReturning,0))>0) c on b.OrderID=c.OrderID";
        this.Pager1.PageBind(0, 10, table, "B.SendWay,A.ID,A.Number,A.StoreID,b.OrderID,B.IsReceivables,B.StoreID as OStoreID,A.Name,A.PetName,B.totalMoney,B.totalPv,B.OrderExpectNum,case B.PayExpectNum when -1 then '0' when 0 then '1' else convert(varchar,B.PayExpectNum) end as PayExpectNum,B.OrderDate,A.Remark,A.Error as Error,B.ordertype,case when B.ordertype=0 then '0' when B.ordertype = 3 then '3' when  B.ordertype = 4 then '4' else '5' end as RegisterWay , B.defraytype,B.DefrayState, case when B.DefrayState = 0 then '0'  when  B.DefrayState = 1 then '1' else '2' end as PayStatus ,B.isAgain ,case B.isAgain when 0 then '0' when 1 then '1' when 5 then '5' when 6 then '6' end as againType,case defraystate when 1 then 1 else case paymentmoney when 0 then 0 else 1 end end as dpqueren,defraystate as gsqueren", condtion, " B.Id ", "gv_browOrder");
        // this.excelTable = new BrowseMemberOrdersBLL().GetInfoAndOrder("[MemberInfo]as A,MemberOrder as B", "Case B.SendWay When 0 Then '0' Else '1' End As SendWay,A.ID,A.Number,A.StoreID,b.OrderID,B.IsReceivables,B.StoreID as OStoreID,A.Name,A.PetName,B.totalMoney,B.totalPv,B.OrderExpectNum,case B.PayExpectNum when -1 then '0' when 0 then '1' else convert(varchar,B.PayExpectNum) end as PayExpectNum,B.OrderDate,A.Remark,B.Error as Error,B.ordertype,case when B.ordertype=0 then '0' when B.ordertype = 3 then '3' when  B.ordertype = 4 then '4' else '5' end as RegisterWay , B.defraytype,B.DefrayState, case when B.DefrayState = 0 then '0'  when  B.DefrayState = 1 then '1' else '2' end as PayStatus ,B.isAgain ,case B.isAgain when 0 then '0' when 1 then '1' when 5 then '5' when 6 then '6' end as againType,case defraystate when 1 then 1 else case paymentmoney when 0 then 0 else 1 end end as dpqueren,defraystate as gsqueren", condtion, " B.Id ");
        this.Pager1.Visible = true;
    }

    protected void BindCompare()
    {
        ddlcompare.Items.Clear();
        this.txtContent.Text = "";
        // ddlcompare.Items.Add(new ListItem(this.GetTran("000881", "不限"), "all"));

        if (ddlContion.SelectedValue != "B.TotalMoney" && ddlContion.SelectedValue != "B.TotalPv")
        {
            ddlcompare.Items.Add(new ListItem(this.GetTran("000821", "包含字符"), " like "));
            ddlcompare.Items.Add(new ListItem(this.GetTran("000822", "不包含字符"), " not like "));

        }
        else
        {
            ddlcompare.Items.Add(new ListItem(this.GetTran("000802", "数值等于"), " = "));
            ddlcompare.Items.Add(new ListItem(this.GetTran("000804", "数值大于"), " > "));
            ddlcompare.Items.Add(new ListItem(this.GetTran("000808", "数值小于"), " < "));
            ddlcompare.Items.Add(new ListItem(this.GetTran("000810", "数值大于等于"), " >= "));
            ddlcompare.Items.Add(new ListItem(this.GetTran("000813", "数值小于等于"), " <= "));
            ddlcompare.Items.Add(new ListItem(this.GetTran("000816", "数值不等于"), " <> "));
        }

    }

    protected void ddlContion_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCompare();
    }

    /// <summary>
    /// 搜索按钮点击搜索事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string condition = " and  B.DefrayState!=0 ";
        string expectNum = "";
        if (ExpectNum1.ExpectNum != -1)
        {
            if (this.ddlType.SelectedValue == "0")
            {
                expectNum = " and B.OrderExpectNum=" + ExpectNum1.ExpectNum;
            }
            else
            {
                expectNum = " and B.PayExpectNum=" + ExpectNum1.ExpectNum;
            }
        }
        if (txtContent.Text.Trim() != "")
        {
            if (this.ddlContion.SelectedValue == "B.TotalMoney" || this.ddlContion.SelectedValue == "B.TotalPv")
            {
                try
                {
                    double total = Convert.ToDouble(this.txtContent.Text.Trim());
                }
                catch
                {
                    ScriptHelper.SetAlert(Page, this.GetTran("006918", "查询条件输入格式错误！"));
                    return;
                }
            }
            string content = txtContent.Text;
            if (ddlContion.SelectedValue == "A.Name")
            {
                content = Encryption.Encryption.GetEncryptionName(content);
            }
            string compareString = ddlcompare.SelectedValue.Trim().ToLower();
            if (compareString.IndexOf("like") > 0)
            {
                condition += " and " + this.ddlContion.SelectedValue + " " + compareString + " '%" + content + "%'";
            }
            else if (compareString.IndexOf("all") < 0)
            {
                condition += " and " + this.ddlContion.SelectedValue + " " + compareString + " " + content + " ";

            }
        }
        if (MemberID != "")
        {
            condition += " And a.number='" + MemberID + "'";
        }

        Condition = condition;
        string table = "  MemberInfo a inner join MemberOrder b on B.Number=A.Number inner join "   // and b.OrderStatus_NR=" + (int)OrderStatusEnum.Normal + "
            + " ( select distinct orderid,number from MemberDetails where (isnull(Quantity,0)-isnull(QuantityReturned,0)-isnull(QuantityReturning,0))>0) c on b.OrderID=c.OrderID";
        string columns = @"B.SendWay,A.ID,A.Number,A.StoreID,b.OrderID,B.IsReceivables,B.StoreID as OStoreID,A.Name,A.PetName,B.totalMoney,B.totalPv,B.OrderExpectNum,
                    case B.PayExpectNum when -1 then '0' when 0 then '1' else convert(varchar,B.PayExpectNum) end as PayExpectNum,
                    B.OrderDate,A.Remark,b.Error as Error,B.ordertype,
                    case when B.ordertype=0 then '0' when B.ordertype = 3 then '3' when  B.ordertype = 4 then '4' else '5' end as RegisterWay , 
                    case when B.defraytype=1 then '" + this.GetTran("000699", "现金") + "' when B.defraytype=2 then '" + this.GetTran("001408", "电子转账") + "'  when B.defraytype=3 then '" + this.GetTran("001411", "支付宝支付") + "' when B.defraytype=4 then '" + this.GetTran("001414", "银行支付") + "' else '" + this.GetTran("001416", "未知") + "' end as defrayname,B.defraytype,B.DefrayState, case when B.DefrayState = 0 then '0'  when  B.DefrayState = 1 then '1' else '2' end as PayStatus ,B.isAgain ,case B.isAgain when 0 then '注册报单' when 1 then '复销报单'  end as againType,case defraystate when 1 then 1 else case paymentmoney when 0 then 0 else 1 end end as dpqueren,defraystate as gsqueren";
        // string columnsForExcel = string.Empty;
        //columnsForExcel = "Case B.SendWay When 0 Then '0' Else '1' End As SendWay,A.ID,A.Number,A.StoreID,b.OrderID,B.IsReceivables,B.StoreID as OStoreID,A.Name,A.PetName,B.totalMoney,"
        //    +"B.totalPv,B.OrderExpectNum,case B.PayExpectNum when -1 then '0' when 0 then '1' else convert(varchar,B.PayExpectNum) end as PayExpectNum,B.OrderDate,A.Remark,B.Error as Error,"
        //    +"B.ordertype,case when B.ordertype=0 then '0' when B.ordertype = 3 then '3' when  B.ordertype = 4 then '4' else '5' end as RegisterWay ,case when B.defraytype=1 then '" + this.GetTran("000699", "现金") + "' when B.defraytype=2 then '" + this.GetTran("001408", "电子转账") + "'  when B.defraytype=3 then '" + this.GetTran("001411", "支付宝支付") + "' when B.defraytype=4 then '" + this.GetTran("001414", "银行支付") + "' else '" + this.GetTran("001416", "未知") + "' end as defrayname,"
        //    +"B.defraytype,B.DefrayState, case when B.DefrayState = 0 then '0'  when  B.DefrayState = 1 then '1' else '2' end as PayStatus ,B.isAgain ,case B.isAgain when 0 then '注册报单' when 1 then '复销报单' end as againType,case defraystate when 1 then 1 else case paymentmoney when 0 then 0 else 1 end end as dpqueren,defraystate as gsqueren";

        this.Pager1.PageBind(0, 10, table, columns, " 1=1 " + expectNum + condition, " b.id", "gv_browOrder");
        //this.excelTable = new BrowseMemberOrdersBLL().GetInfoAndOrder(table, columnsForExcel , " 1=1 " + expectNum + condition, "b.id");
        this.Pager1.Visible = true;
        InitCheckBoxInGridView();
        //Translate();
    }
    protected void lbtnSelectAll_Click(object sender, EventArgs e)
    {
        int selectStatus = string.IsNullOrEmpty(this.hidSelectStatus.Value) ? 0 : int.Parse(hidSelectStatus.Value);
        bool selected = false;
        if (selectStatus == 0)
            selected = false;
        else
            selected = true;
        foreach (GridViewRow grv in this.gv_browOrder.Rows)
        {
            CheckBox chk = (CheckBox)grv.FindControl("chk");
            if (chk != null)
            {
                chk.Checked = selected;
            }
        }
    }

    /// <summary>
    /// 目前已选中的订单
    /// </summary>
    public List<MemberOrderEntity> Orders
    {
        get { return ViewState["Orders"] == null ? null : (List<MemberOrderEntity>)ViewState["Orders"]; }
        set { ViewState["Orders"] = value; }

    }
    protected void lbtn_SelectCheck_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        OrderBLL orderService = new OrderBLL();
        List<MemberOrderEntity> orders = Orders;
        if (orders == null)
            orders = new List<MemberOrderEntity>();
        foreach (GridViewRow grv in this.gv_browOrder.Rows)
        {
            CheckBox chk = (CheckBox)grv.FindControl("chk");
            if (chk != null)
            {
                Label lbl_OrderId = (Label)grv.FindControl("lbl_OrderId");
                if (lbl_OrderId == null)
                    continue;
                string orderid = lbl_OrderId.Text;
                MemberOrderEntity obj = orders.FirstOrDefault(p => p.OrderID == orderid);
                if (chk.Checked)
                {//当前行被选中
                    if (obj == null)
                    {//集合中不存在，创建添加，存在的话不做处理
                        var newOrder = orderService.CreateEntityByOrderID(orderid, ref msg);
                        if (newOrder != null)
                        {
                            OrderDAL orderDal = new OrderDAL();
                            var orderDetails = orderDal.GetMemberOrderDetailsEntity(orderid);
                            if (orderDetails != null)
                                newOrder.Details = orderDetails;
                            orders.Add(newOrder);
                        }
                    }
                }
                else
                {//未选中
                    if (obj != null)
                        orders.Remove(obj);
                }
            }
        }
        Orders = orders;
    }
    protected void gv_browOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "details")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string msg = string.Empty;
            List<MemberOrderEntity> orders = Orders;
            if (orders == null)
                orders = new List<MemberOrderEntity>();
            CheckBox chk = (CheckBox)this.gv_browOrder.Rows[index].FindControl("chk");
            if (chk != null)
            {
                Label lbl_OrderId = (Label)this.gv_browOrder.Rows[index].FindControl("lbl_OrderId");
                if (lbl_OrderId == null)
                    return;
                string orderid = lbl_OrderId.Text;
                if (orderid != string.Empty)
                {
                    List<MemberOrderDetailsEntity> orderDetails = new List<MemberOrderDetailsEntity>();
                    MemberOrderEntity obj = orders.FirstOrDefault(p => p.OrderID == orderid);
                    if (obj != null)
                    {//视图中已经存在此记录，从视图中读取 
                        orderDetails = obj.Details;
                    }
                    else
                    {//否则从数量库中读取
                        OrderDAL orderDal = new OrderDAL();
                        orderDetails = orderDal.GetMemberOrderDetailsEntity(orderid);
                        var newOrder = new OrderBLL().CreateEntityByOrderID(orderid, ref msg);
                        if (newOrder == null)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001518", "订单：") + orderid + GetTran("007750", "不存在，或此订单下无产品！") + "');</script>", false);
                            return;
                        }
                        else if (orderDetails == null)
                        {

                        }
                        //newOrder.Details =orderDetails;
                        //orders.Add(newOrder);
                        //Orders = orders;        //保存至视图
                    }
                    lblDealingOrderID.Text = orderid;
                    GV_OrderDetailsBind(orderDetails);
                }
            }
        }
        else if (e.CommandName == "refund")
        {
            string msg = string.Empty;
            OrderBLL orderService = new OrderBLL();
            List<MemberOrderEntity> orders = Orders;
            if (orders == null)
                orders = new List<MemberOrderEntity>();
            string orderid = e.CommandArgument.ToString();

            var newOrder = new MemberOrderEntity();
            newOrder = orders.FirstOrDefault(p => p.OrderID == orderid);
            if (newOrder == null)
                newOrder = orderService.CreateEntityByOrderID(orderid, ref msg);
            if (newOrder != null)
            {
                OrderDAL orderDal = new OrderDAL();
                var orderDetails = orderDal.GetMemberOrderDetailsEntity(orderid);
                if (orderDetails != null)
                    newOrder.Details = orderDetails;
                if (!orders.Contains(newOrder))
                    orders.Add(newOrder);
            }
            Orders = orders;
            btn_ReturnConfrim_Click(null, null);
        }
    }
    private void GV_OrderDetailsBind(List<MemberOrderDetailsEntity> orderDetails)
    {
        if (orderDetails != null)
        {
            this.gv_OrderDetails.DataSource = orderDetails;
            this.gv_OrderDetails.DataBind();
            lbl_Title.Text = GetTran("000884", "订单明细");
            pnl_OrderDetailsList.Visible = true;
            this.pnl_OrderList.Visible = false;
        }
    }
    protected void btn_ConfrimDetails_Click(object sender, EventArgs e)
    {
        List<MemberOrderEntity> orders = Orders;
        if (orders == null)
            orders = new List<MemberOrderEntity>();
        string errInfo = string.Empty;
        MemberOrderEntity confirmOrder = null;
        foreach (GridViewRow gvr in this.gv_OrderDetails.Rows)
        {
            Label lbl_OrderID = (Label)gvr.FindControl("lbl_OrderID");
            TextBox txt_UseNum = (TextBox)gvr.FindControl("txt_UseNum");
            Label lbl_ProductID = (Label)gvr.FindControl("lbl_ProductID");
            //查找源数据
            if (lbl_ProductID == null || lbl_OrderID == null)
                continue;

            string orderid = lbl_OrderID.Text.Trim();
            int productid = int.Parse(lbl_ProductID.Text);
            if (confirmOrder == null)
            {//第一次
                confirmOrder = orders.FirstOrDefault(p => p.OrderID == orderid);
                if (confirmOrder == null)
                { //不在视图数据源中，重新创建一个新实例
                    OrderDAL orderDal = new OrderDAL();
                    confirmOrder = new OrderBLL().CreateEntityByOrderID(orderid, ref errInfo);
                    List<MemberOrderDetailsEntity> orderDetails = new List<MemberOrderDetailsEntity>();
                    orderDetails = orderDal.GetMemberOrderDetailsEntity(orderid);

                    if (confirmOrder == null)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001518", "订单：") + orderid + GetTran("007752", "不存在！") + "');</script>", false);
                        return;
                    }
                    if (orderDetails == null)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(' " + GetTran("001518", "订单：") + orderid + GetTran("007753", " 中无产品明细！") + "');</script>", false);
                        return;
                    }
                    confirmOrder.Details = orderDetails;
                    orders.Add(confirmOrder);
                }
            }
            //当前行产品,遍历
            MemberOrderDetailsEntity detail = confirmOrder.Details.FirstOrDefault(p => p.ProductID == productid);
            if (detail == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001518", "订单：") + orderid + GetTran("007752", "不存在") + gvr.Cells[2].Text + "！');</script>", false);
                return;
            }
            //验证退货数量
            if (txt_UseNum.Text != string.Empty)
            {
                int num = int.Parse(txt_UseNum.Text);
                if (num > detail.LeftQuantity || num < 0)
                {
                    errInfo = GetTran("002186", "产品“") + detail.ProductName + "”" + GetTran("007758", "的退货数量必须在0到剩余数量") + detail.LeftQuantity + GetTran("007759", "之间！");
                    break;
                }
                else
                {
                    detail.UseQuantity = num;
                }
            }
        }
        //如果没有要退的货，则从视图中移除此订单 
        #region
        bool needRemove = true;
        foreach (MemberOrderDetailsEntity mod in confirmOrder.Details)
        {
            if (mod.UseQuantity > 0)
                needRemove = false;
        }
        if (needRemove)
            orders.Remove(confirmOrder);
        #endregion
        //保存至视图
        Orders = orders;
        if (!string.IsNullOrEmpty(errInfo))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + errInfo + "');</script>", false);
        }
        else
        {//所有验证成功

            Orders = orders;        //保存至视图
            this.pnl_OrderDetailsList.Visible = false;
            this.pnl_OrderList.Visible = true;
            lbl_Title.Text = string.Empty;
            lblDealingOrderID.Text = string.Empty;
            InitCheckBoxInGridView();
        }
    }
    /// <summary>
    /// 初始化订单列表的checkbox状态
    /// </summary>
    private void InitCheckBoxInGridView()
    {
        List<MemberOrderEntity> orders = Orders;
        if (orders == null)
            orders = new List<MemberOrderEntity>();
        MemberOrderEntity confirmOrder = null;
        foreach (GridViewRow gvr in this.gv_browOrder.Rows)
        {
            Label lbl_OrderID = (Label)gvr.FindControl("lbl_OrderID");
            CheckBox chk = (CheckBox)gvr.FindControl("chk");
            if (lbl_OrderID == null || chk == null)
                continue;
            string orderid = lbl_OrderID.Text.Trim();
            confirmOrder = orders.FirstOrDefault(p => p.OrderID == orderid);
            if (confirmOrder != null)
            {
                chk.Checked = true;
            }
            else
                chk.Checked = false;
        }
    }
    protected void gv_browOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtn_Details = (LinkButton)e.Row.FindControl("lbtn_Details");
            if (lbtn_Details != null)
            {
                lbtn_Details.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }
    //返回
    protected void btn_backtoOrderList_Click(object sender, EventArgs e)
    {
        this.pnl_OrderDetailsList.Visible = false;
        this.pnl_OrderList.Visible = true;
        lbl_Title.Text = string.Empty;
        lblDealingOrderID.Text = string.Empty;
    }
    /// <summary>
    /// 取消肖前单的退货
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        try
        {
            string dealingOrderid = this.lblDealingOrderID.Text;
            List<MemberOrderEntity> orders = Orders;
            if (orders == null)
                orders = new List<MemberOrderEntity>();
            var curDealingOrder = orders.FirstOrDefault(p => p.OrderID == dealingOrderid);
            if (curDealingOrder != null)
            {
                orders.Remove(curDealingOrder);
                InitCheckBoxInGridView();
            }
            this.lblDealingOrderID.Text = string.Empty;
            this.pnl_OrderDetailsList.Visible = false;
            this.pnl_OrderList.Visible = true;
        }
        catch (Exception ex)
        {
        }
    }
    protected void gv_browOrder_DataBound(object sender, EventArgs e)
    {
        InitCheckBoxInGridView();
    }
    /// <summary>
    /// 退货确认
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_ReturnConfrim_Click(object sender, EventArgs e)
    {
        if (Orders == null || Orders.Count < 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007751", "退货产品数量不能为空，请选择要退货的产品！") + "');</script>", false);
            return;
        }
        this.pnl_OrderList.Visible = false;
        this.pnl_OrderDetailsList.Visible = false;
        this.pnl_ReturnOrderConfirm.Visible = true;
        this.pnl_title.Visible = false;
        BindReturnOrderBill();
    }


    private void BindReturnOrderBill()
    {
        string orderids = string.Empty;
        double ReturnTotalMoney = 0;
        double ReturnTotalPV = 0;
        #region 汇总退货单
        List<MemberOrderEntity> records = Orders;
        List<MemberOrderDetailsEntity> collections = new List<MemberOrderDetailsEntity>();
        RefundmentOrderDocBLL refundmentService = new RefundmentOrderDocBLL();
        foreach (MemberOrderEntity o in records)
        {
            if (o.Details == null || o.Details.Count < 1)
                continue;
            foreach (MemberOrderDetailsEntity md in o.Details)
            {
                if (md.LeftQuantity > 0)
                {
                    ReturnTotalMoney += md.UseQuantity * md.UnitPrice;
                    ReturnTotalPV += md.UseQuantity * md.UnitPV;
                    collections.Add(md);
                }
            }
            orderids += o.OrderID + "；";
        }
        if (orderids.Length > 0)
            orderids = orderids.Substring(0, orderids.Length - 1);
        #endregion

        #region  获取会员信息
        var memberInfo = MemberInfoDAL.getMemberInfo(MemberID);
        if (memberInfo == null)
        {
            return;
        }
        string CPCCode = memberInfo.CPCCode.ToString();
        if (CPCCode != string.Empty)
        {
            CityModel cpccode = CommonDataDAL.GetCPCCode(CPCCode);
            if (cpccode != null)
            {
                DataTable dt = refundmentService.GetCountryCityByCPCCode(CPCCode);
                this.CountryCity1.SelectCountry(cpccode.Country, cpccode.Province, cpccode.City, cpccode.Xian);
                int countryid = -1;
                if (dt != null && dt.Rows.Count > 0)
                {
                    countryid = int.Parse(dt.Rows[0]["CountryID"].ToString());
                    //this.UCBank1.CountryID = countryid;
                    //this.UCBank1.BankCode = memberInfo.BankCode;
                    this.txt_BankCard.Text = memberInfo.BankCard;
                }

            }
        }

        this.txt_ApplyName.Text = memberInfo.Name;
        this.txt_BankBookName.Text = memberInfo.Name;
        decimal memberjj = 0;
        //Jackpot-ECTPay-Releasemoney-Out-membership lbl_MemberTotalMoney
        memberjj = (memberInfo.Jackpot - memberInfo.EctOut - memberInfo.Memberships);
        this.lbl_MemberJJ.Text = memberjj.ToString("f2");
        this.lbl_MemberName.Text = memberInfo.Name;
        this.lbl_MemberNumber.Text = memberInfo.Number;
        DataTable dtMoneyPv = MemberInfoDAL.GetMemberOrderMoneyPVSum(MemberID);
        if (dtMoneyPv != null && dtMoneyPv.Rows.Count > 0)
        {
            this.lbl_MemberTotalMoney.Text = Convert.ToDouble(dtMoneyPv.Rows[0]["totalmoney"]).ToString("f2");
            this.lbl_MemberTotalMoney2.Text = this.lbl_MemberTotalMoney.Text;
            this.lbl_MemberTotalPV.Text = Convert.ToDouble(dtMoneyPv.Rows[0]["totalpv"]).ToString("f2");
            this.lab_TotalPV.Text = Convert.ToDouble(dtMoneyPv.Rows[0]["totalpv"]).ToString("f2");
        }
        this.lbl_OrderIDS.Text = orderids;
        this.txt_Phone.Text = memberInfo.MobileTele;
        lbl_RegesterDate.Text = memberInfo.RegisterDate.ToString();
        this.lbl_ReturnTotalMoney.Text = ReturnTotalMoney.ToString("f2");
        this.lbl_ReturnTotalPV.Text = ReturnTotalPV.ToString("f2");
        this.txt_refundmentDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        #endregion

        string country = this.CountryCity1.Country;
        string province = this.CountryCity1.Province;
        string city = this.CountryCity1.City;
        string address = this.txt_addressDetails.Text;
        string addressDetails = country + province + city + address;
        MemberRefundmentEntity refundmentEntity = new MemberRefundmentEntity();
        refundmentEntity.Number = MemberID;
        refundmentEntity.MemberOrderList = records;
        refundmentEntity.MemberInfo = memberInfo;
        refundmentEntity.Cause = this.txt_Reson.Text.Trim();
        refundmentEntity.Address = addressDetails;
        refundmentEntity.OriginalDocID = orderids;
        refundmentEntity.ReturnTotalMoney = ReturnTotalMoney;
        refundmentEntity.ReturnTotalPV = ReturnTotalPV;
        MemberRefundment = refundmentEntity;

        #region 绑定退货明细
        this.gv_OrderDetailsAll.DataSource = collections;
        this.gv_OrderDetailsAll.DataBind();
        #endregion

    }


    protected void btn_ConfrimAndSubmit_Click(object sender, EventArgs e)
    {
        //若该订单有未审核的订单，则应该先审核，才能继续申请
        if (Convert.ToInt32(DAL.DBHelper.ExecuteScalar("select COUNT(0) from InventoryDoc where OriginalDocID='" + MemberRefundment.OriginalDocID + "' and StateFlag=0")) > 0)
        {
            ScriptHelper.SetAlert(Page, "该订单有未审核的退货单，请先进行审核！");
            return;
        }

        string msg = string.Empty;
        try
        {
            RefundmentOrderDocBLL refundmentOrderDocService = new RefundmentOrderDocBLL();

            var refundmentEntity = MemberRefundment;
            string country = this.CountryCity1.Country;
            string province = this.CountryCity1.Province;
            string city = this.CountryCity1.City;
            string address = this.txt_addressDetails.Text;
            string addressDetails = country + province + city + address;
            refundmentEntity.Cause = this.txt_Reson.Text.Trim();
            refundmentEntity.Address = addressDetails;

            refundmentEntity.MemberInfo.MobileTele = this.txt_Phone.Text;

            RefundmentOrderDocModel rom = new RefundmentOrderDocModel();

            //获得单据编号
            string docId = refundmentOrderDocService.CreateRefundmentOrderDocIdByTypeCode();
            rom.DocID = docId;
            RefundsTypeEnum RefundsType = RefundsTypeEnum.RefundsUseEAccount;
            if (this.rbtn_0.Checked)
            {//通过现金退款
                RefundsType = RefundsTypeEnum.RefundsUseCash;
            }
            else if (this.rbtn_1.Checked)
            {//通过电子账户退款
                RefundsType = RefundsTypeEnum.RefundsUseEAccount;
            }
            else if (this.rbtn_2.Checked)
            {//通过银行退款
                RefundsType = RefundsTypeEnum.RefundsUseBank;

                //判断是否选择了退款银行和相关银行信息
                rom.Country = this.UCBank1.CountryID;
                string BankCard = this.txt_BankCard.Text;
                string BankCode = this.UCBank1.BankCode;
                string BankBookName = this.txt_BankBookName.Text;
                string BankBranch = this.txt_BankBranch.Text.Trim();
                if (string.IsNullOrEmpty(BankCode))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007760", "请选择银行信息！") + "');</script>", false);
                    return;
                }
                if (string.IsNullOrEmpty(BankBranch))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007761", "请填写银行卡账户！") + "');</script>", false);
                    return;
                }
                if (string.IsNullOrEmpty(BankCard))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007761", "请填写银行卡账户！") + "');</script>", false);
                    return;
                }
                if (string.IsNullOrEmpty(BankBookName))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007762", "请填写银行卡账户名！") + "');</script>", false);
                    return;
                }
                rom.BankCode = this.UCBank1.BankCode;
                rom.BankBranch = this.txt_BankBranch.Text;
                rom.BankBook = BankCard;
                rom.BankCard = BankCard;
                rom.BankBookName = BankBookName;
            }

            string ApplyName = this.txt_ApplyName.Text.Trim();
            if (string.IsNullOrEmpty(ApplyName))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007763", "请填写申请人姓名！") + "');</script>", false);
                return;
            }
            DateTime dtimeRefund = new DateTime();
            if (string.IsNullOrEmpty(this.txt_addressDetails.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("007764", "请填写收货地址！") + "');</script>", false);
                return;
            }
            dtimeRefund = DateTime.Parse(this.txt_refundmentDate.Text);
            rom.RefundmentDate_DT = dtimeRefund;
            rom.RefundmentType_NR = (int)RefundsType;
            rom.Auditer = string.Empty;
            rom.Cause_TX = this.txt_Reson.Text;
            rom.Charged_NR = 0;
            rom.Country = 1;
            string cpccode = CommonDataDAL.GetCPCCode(country, province, city);
            rom.CPCCode = cpccode;
            rom.Address_TX = address;
            rom.DepotSeatID = 1;
            rom.ExpectNum = CommonDataBLL.getMaxqishu();
            rom.OwnerNumber_TX = MemberID;
            rom.ApplyTime_DT = DateTime.Now;
            rom.Applicant_TX = this.txt_ApplyName.Text;
            rom.MobileTele = this.txt_Phone.Text;
            rom.Note_TX = string.Empty;
            rom.OperateIP_TX = Request.UserHostAddress;
            rom.OperateNum_TX = CommonDataBLL.OperateBh;
            if (Session["Company"] != null)
            {
                ManageModel loginer = ManagerBLL.GetManage(Session["Company"].ToString());
                string operate = loginer == null ? "" : loginer.Name;
                rom.OperationPerson = operate;
            }
            else
            {
                var memberInfo = MemberInfoDAL.getMemberInfo(MemberID);
                if (memberInfo != null)
                {
                    rom.OperationPerson = memberInfo.Name;
                }
            }
            rom.OriginalDocIDS = MemberRefundment.OriginalDocID;
            rom.OwnerNumber_TX = lbl_MemberNumber.Text;
            rom.PayCurrency = 1;
            //rom.PayMoney = Convert.ToDecimal(MemberRefundment.ReturnTotalMoney);
            rom.Cause_TX = this.txt_Reson.Text;
            rom.RefundmentType_NR = (int)RefundsType;
            rom.RefundTotalMoney = 0;
            rom.StatusFlag_NR = 0;
            //rom.TotalMoney = Convert.ToDecimal(MemberRefundment.ReturnTotalMoney);
            //rom.TotalPV = Convert.ToDecimal(MemberRefundment.ReturnTotalPV);
            rom.WareHouseID = 1;
            rom.OperationPerson = this.txt_ApplyName.Text.Trim();                      //申请人
            List<RefundmentOrderDocDetails> refundDetails = new List<RefundmentOrderDocDetails>();
            #region 单订单退货时使用
            List<MemberOrderEntity> orders = Orders;
            if (orders == null)
                orders = new List<MemberOrderEntity>();
            double ReturnTotalMoney = 0;
            double ReturnTotalPV = 0;
            int unitTotal = 0;
            foreach (MemberOrderEntity mode in orders)
            {
                foreach (MemberOrderDetailsEntity en in mode.Details)
                {
                    if (en.LeftQuantity > 0 && en.UseQuantity > 0)
                    {
                        RefundmentOrderDocDetails detail = new RefundmentOrderDocDetails();
                        if (en.pici == null)
                        {
                            detail.Batch = "0";
                        }
                        else
                        {
                            detail.Batch = en.pici;
                        }
                        detail.DocID = rom.DocID;
                        detail.ExpectNum = rom.ExpectNum;
                        detail.OriginalDocID = en.OrderID;
                        detail.ProductID = en.ProductID;
                        detail.ProductQuantity = en.UseQuantity;
                        detail.UnitPrice = Convert.ToDecimal(en.UnitPrice);
                        detail.UnitPV = Convert.ToDecimal(en.UnitPV);
                        ReturnTotalMoney += en.UseQuantity * en.UnitPrice;
                        ReturnTotalPV += en.UseQuantity * en.UnitPV;
                        unitTotal += en.UseQuantity;
                        refundDetails.Add(detail);
                    }
                }
            }

            if (unitTotal <= 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请选择退货产品！');</script>", false);
                return;
            }
            rom.TotalMoney = Convert.ToDecimal(ReturnTotalMoney);
            rom.TotalPV = Convert.ToDecimal(ReturnTotalPV);
            rom.PayMoney = Convert.ToDecimal(ReturnTotalMoney);
            #endregion
            #region 多订单退货时使用
            /*
            foreach (MemberOrderDetailsEntity en in refundmentEntity.DetailsList)
            {
                RefundmentOrderDocDetails detail = new RefundmentOrderDocDetails();
                detail.Batch = en.pici;
                detail.DocID = rom.DocID;
                detail.ExpectNum = rom.ExpectNum;
                detail.OriginalDocID = en.OrderID;
                detail.ProductID = en.ProductID;
                detail.ProductQuantity = en.UseQuantity;
                detail.UnitPrice = Convert.ToDecimal(en.UnitPrice);
                detail.UnitPV = Convert.ToDecimal(en.UnitPV);
                refundDetails.Add(detail);
            }*/
            #endregion
            rom.RefundmentOrderDetails = refundDetails;
            // var refundmentOrderDocService = new RefundmentOrderDocBLL();
            bool flag = refundmentOrderDocService.AddRefundmentOrderDoc(rom, ref msg);
            if (flag)
                msg = GetTran("001881", "提交成功！");
            else
            {
                if (string.IsNullOrEmpty(msg))
                    msg = GetTran("000302", "提交失败！");

            }
            if (flag)
            {
                string url = "../DocPrint.aspx?DocID=" + rom.DocID;
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>if(confirm('" + GetTran("007765", "提交成功，是否打印退货单？") + "')){window.open('" + url + "');} window.location.href='RefundmentOrderForMemberList.aspx';</script>", false);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + msg + "！');</script>", false);
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000302", "提交失败！") + "');</script>", false);
        }
        lblMsg.Text = msg;
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        this.pnl_OrderDetailsList.Visible = false;
        this.pnl_OrderList.Visible = true;
        this.pnl_ReturnOrderConfirm.Visible = false;
        this.pnl_title.Visible = true;
    }

    public MemberRefundmentEntity MemberRefundment
    {
        get
        {
            return ViewState["MemberRefundmentEntity"] == null ? null : (MemberRefundmentEntity)ViewState["MemberRefundmentEntity"];
        }
        set
        {
            ViewState["MemberRefundmentEntity"] = value;
        }
    }
    /// <summary>
    /// 添加退货单据和产品明细信息
    /// </summary>
    protected void SaveInventoryDocAndDocDetails(RefundmentOrderDocModel refundmentEntity)
    {
        /*
        if (string.IsNullOrEmpty(this.txt_refundmentDate.Text))
            this.txt_refundmentDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        ReturnedGoodsBLL returnedGoodsBLL = new ReturnedGoodsBLL();
        string msg = string.Empty;
        //获得当前操作的时间
        DateTime dt = DateTime.Now.ToUniversalTime();//转换时间
        //获得单据编号
        string docId = returnedGoodsBLL.GetDocIdByTypeCode(DocTypeCodeConst.DocType_THM);
        //往退货单据实体类中添加数据
        InventoryDocModel inventoryDocModel = new InventoryDocModel();
        inventoryDocModel.DocMakeTime = dt;

        inventoryDocModel.Currency = int.Parse(ReturnedGoodsBLL.GetStoreCurrency(refundmentEntity.OwnerNumber_TX.StoreID).ToString());
        inventoryDocModel.DocID = docId;
        inventoryDocModel.DocTypeID = int.Parse (returnedGoodsBLL.GetDocTypeIdByDocTypeName(DocTypeCodeConst.DocType_THM));
        inventoryDocModel.DocMaker = CommonDataBLL.OperateBh;
        inventoryDocModel.DocMakeTime = DateTime.Parse(this.txt_refundmentDate.Text);
        inventoryDocModel.Provider = 0;
        inventoryDocModel.Client = refundmentEntity.MemberInfo .StoreID;
        inventoryDocModel.WareHouseID = 0;
        inventoryDocModel.TotalMoney = refundmentEntity.ReturnTotalMoney;
        inventoryDocModel.TotalPV = refundmentEntity.ReturnTotalPV;
        inventoryDocModel.ExpectNum = BLL.CommonClass.CommonDataBLL.getMaxqishu();
        inventoryDocModel.Cause = refundmentEntity.Cause;//DocTypeCodeConst.DocType_THM;
        inventoryDocModel.Note ="";
        inventoryDocModel.StateFlag = 0;
        inventoryDocModel.BatchCode = "";
        inventoryDocModel.Client = MemberID;
        ManageModel loginer = ManagerBLL.GetManage(Session["Company"].ToString ());
        string operate= loginer == null ? "" : loginer.Name;
        inventoryDocModel.OperationPerson = operate;
        inventoryDocModel.Address =refundmentEntity .Address;
        inventoryDocModel.MotherID = "";
        inventoryDocModel.OperateIP = Request.UserHostAddress;
        inventoryDocModel.OperateNum = CommonDataBLL.OperateBh;
        inventoryDocModel.OriginalDocID = "";
        inventoryDocModel.OriginalDocID2 = refundmentEntity.OriginalDocID;
        inventoryDocModel.InDepotSeatID = returnedGoodsBLL.GetStoreRate(refundmentEntity.MemberInfo.StoreID);
        inventoryDocModel.RefundmentType_NR = Convert.ToInt32(refundmentEntity.RefundmentType);
        returnedGoodsBLL.InsertInventoryDoc(inventoryDocModel, CommonDataBLL.OperateBh, refundmentEntity.DetailsList);
        */
    }


    public void ReBindRefundmentDetails()
    {
        try
        {
            string errMsg = string.Empty;
            MemberRefundmentEntity refundmentEntity = MemberRefundment;
            if (refundmentEntity == null)
                refundmentEntity = new MemberRefundmentEntity();
            List<MemberOrderEntity> orders = Orders;
            if (orders == null)
                orders = new List<MemberOrderEntity>();
            double ReturnTotalMoney = 0;
            double ReturnTotalPV = 0;
            List<MemberOrderDetailsEntity> collections = new List<MemberOrderDetailsEntity>();
            // RefundmentOrderDocBLL refundmentService = new RefundmentOrderDocBLL();
            foreach (MemberOrderEntity o in orders)
            {
                if (o.Details == null || o.Details.Count < 1)
                    continue;
                foreach (MemberOrderDetailsEntity md in o.Details)
                {
                    //ReturnTotalMoney += md.UseQuantity * md.UnitPrice;
                    //ReturnTotalPV += md.UseQuantity * md.UnitPV;
                    collections.Add(md);
                }
            }
            foreach (GridViewRow gr in gv_OrderDetailsAll.Rows)
            {
                string orderid = ((Label)gr.FindControl("lbl_OrderID")).Text.Trim();
                int productid = int.Parse(((Label)gr.FindControl("lbl_ProductID")).Text.Trim());
                int quantity = int.Parse(((Label)gr.FindControl("lbl_Quantity")).Text.Trim());
                int LeftQuantity = int.Parse(((Label)gr.FindControl("lbl_LeftQuantity")).Text.Trim());
                TextBox txt_useQuantity = (TextBox)gr.FindControl("txt_UseQuantity");
                string ProductName = ((Label)gr.FindControl("lbl_ProductName")).Text.Trim();
                int useQuantity = 0;
                try
                {
                    useQuantity = int.Parse(txt_useQuantity.Text);
                }
                catch
                {
                    errMsg = "“" + ProductName + "”" + GetTran("007766", "的退货数量格式错误！");
                    break;
                }

                if (useQuantity > LeftQuantity)
                {
                    errMsg = "“" + ProductName + "”" + GetTran("007767", "的退货数量不能大于剩余数量！");
                    break;
                }
                else if (useQuantity < 0)
                {
                    errMsg = "“" + ProductName + "”" + GetTran("007768", "的退货数量不能为负数！");
                    break;
                }
                var detail = collections.FirstOrDefault(p => p.OrderID == orderid && p.ProductID == productid);
                if (detail != null)
                {
                    detail.UseQuantity = useQuantity;
                }
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + errMsg + "');</script>", false);
                return;
            }
            else
            {
                Orders = orders;
            }
            foreach (MemberOrderDetailsEntity md in collections)
            {
                ReturnTotalMoney += md.UseQuantity * md.UnitPrice;
                ReturnTotalPV += md.UseQuantity * md.UnitPV;
            }
            this.lbl_ReturnTotalMoney.Text = ReturnTotalMoney.ToString();
            this.lbl_ReturnTotalPV.Text = ReturnTotalPV.ToString();
        }
        catch (Exception ex)
        {
            this.lblMsg.Text = ex.Message;
        }
    }

    protected void lbl_ReBindRefundmentDetails_Click(object sender, EventArgs e)
    {
        ReBindRefundmentDetails();
    }
    protected void gv_OrderDetailsAll_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "add")
        {
            string errMsg = string.Empty;
            int i = int.Parse(e.CommandArgument.ToString());

            string curOrderid = ((Label)this.gv_OrderDetailsAll.Rows[i].FindControl("lbl_OrderID")).Text.Trim();
            int curProductid = int.Parse(((Label)this.gv_OrderDetailsAll.Rows[i].FindControl("lbl_ProductID")).Text.Trim());
            int quantity = int.Parse(((Label)this.gv_OrderDetailsAll.Rows[i].FindControl("lbl_Quantity")).Text.Trim());
            int leftQuantity = int.Parse(((Label)this.gv_OrderDetailsAll.Rows[i].FindControl("lbl_LeftQuantity")).Text.Trim());

            string ProductName = ((Label)this.gv_OrderDetailsAll.Rows[i].FindControl("lbl_ProductName")).Text.Trim();
            TextBox txt_useQuantity = (TextBox)this.gv_OrderDetailsAll.Rows[i].FindControl("txt_UseQuantity");
            int useQuantity = 0;
            try
            {
                useQuantity = int.Parse(txt_useQuantity.Text);
            }
            catch
            {
                errMsg = "“" + ProductName + "”" + GetTran("007766", "的退货数量格式错误！");


            }

            if ((useQuantity + 1) > leftQuantity)
            {
                errMsg = "“" + ProductName + "”" + GetTran("007767", "的退货数量不能大于剩余数量！");

            }
            else if (useQuantity < 0)
            {
                errMsg = "“" + ProductName + "”" + GetTran("007768", "的退货数量不能为负数！");
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + errMsg + "');</script>", false);
            }
            else
            {
                txt_useQuantity.Text = (useQuantity + 1).ToString();
            }
            ReBindRefundmentDetails();
        }
        else if (e.CommandName == "sub")
        {
            string errMsg = string.Empty;
            int i = int.Parse(e.CommandArgument.ToString());

            string curOrderid = ((Label)this.gv_OrderDetailsAll.Rows[i].FindControl("lbl_OrderID")).Text.Trim();
            int curProductid = int.Parse(((Label)this.gv_OrderDetailsAll.Rows[i].FindControl("lbl_ProductID")).Text.Trim());
            int quantity = int.Parse(((Label)this.gv_OrderDetailsAll.Rows[i].FindControl("lbl_Quantity")).Text.Trim());
            int leftQuantity = int.Parse(((Label)this.gv_OrderDetailsAll.Rows[i].FindControl("lbl_LeftQuantity")).Text.Trim());

            string ProductName = ((Label)this.gv_OrderDetailsAll.Rows[i].FindControl("lbl_ProductName")).Text.Trim();
            TextBox txt_useQuantity = (TextBox)this.gv_OrderDetailsAll.Rows[i].FindControl("txt_UseQuantity");
            int useQuantity = 0;
            try
            {
                useQuantity = int.Parse(txt_useQuantity.Text);
            }
            catch
            {
                errMsg = "“" + ProductName + "”" + GetTran("007766", "的退货数量格式错误！");


            }

            if ((useQuantity) > leftQuantity)
            {
                errMsg = "“" + ProductName + "”" + GetTran("007767", "的退货数量不能大于剩余数量！");

            }
            else if ((useQuantity - 1) < 0)
            {
                errMsg = "“" + ProductName + "”" + GetTran("007768", "的退货数量不能为负数！");
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + errMsg + "');</script>", false);
            }
            else
            {
                txt_useQuantity.Text = (useQuantity - 1).ToString();
            }
            ReBindRefundmentDetails();
        }
    }
    protected void gv_OrderDetailsAll_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgBtnUp = (ImageButton)e.Row.FindControl("imgBtnUp");
            ImageButton imgBtnSub = (ImageButton)e.Row.FindControl("imgBtnSub");
            if (imgBtnUp != null)
            {
                imgBtnUp.CommandArgument = e.Row.RowIndex.ToString();
            }
            if (imgBtnSub != null)
            {
                imgBtnSub.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }
}