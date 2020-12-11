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

using BLL.Registration_declarations;
using BLL.CommonClass;
using BLL.other;
using Standard.Classes;
using DAL;
using System.Text;

public partial class Member_AuditingMemberOrder : BLL.TranslationBase
{
    public int bzCurrency = 0;
    public int maxExpect = CommonDataBLL.GetMaxqishu();
    BrowseMemberOrdersBLL bll = new BrowseMemberOrdersBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);

        //获取标准币种
        bzCurrency = CommonDataBLL.GetStandard();

        if (!this.IsPostBack)
        {
            if (Session["Default_Currency"] == null) Session["Default_Currency"] = bzCurrency;

            CommonDataBLL.DelOrderByState();

            CommonDataBLL.BindQishuList(ddlQiShu, false);
            GetShopList();
        }
        Transllations();
    }

    public string GetSendWay(string sendWay)
    {
        if (sendWay == "0")
        {
            return GetTran("007103", "公司发货到店铺");
        }
        return GetTran("007104", "公司直接发货给会员");
    }

    private void Transllations()
    {
        this.TranControls(this.btnSearch, new string[][] { new string[] { "000340", "查询" } });
        
        //this.TranControls(this.gvorder,
        //    new string[][] { 
        //        new string[] { "000811", "明细" } ,
        //        new string[] { "000938", "支付" } ,
        //        new string[] { "000259", "修改" } ,
        //        new string[] { "000022", "删除" } ,
        //        new string[] { "000775", "支付状态" } ,
        //        new string[] {"000186","支付方式"},
        //        new string[]{"007416","收货途径"},
        //        new string[] { "001345", "发货方式"},
        //        new string[]{"000045","期数"},
        //        new string[] { "000079", "订单号" } ,
        //        new string[] { "000322", "金额" } ,
        //        new string[] { "000414", "积分" } ,
        //        new string[] { "000793", "确认店铺" } ,
        //        new string[] { "007535", "订购类型" } ,
        //        new string[] { "000510", "订购日期" }  
                
        //    });
    }

    public string GetDefrayName(string defrayType)
    {
        string defrayName = "";
        switch (defrayType)
        {
            case "1":
                defrayName = this.GetTran("000699", "现金");
                break;
            case "2":
                defrayName = this.GetTran("005845", "电子转账");
                break;
            case "3":
                defrayName = GetTran("001411", "支付宝支付");
                break;
            case "4":
                defrayName = GetTran("001414", "快钱支付");
                break;
            default:
                defrayName = this.GetTran("000221", "无");
                break;
        }
        return defrayName;
    }

    public string GetPayStatus(string paytype)
    {
        string payStatus = new GroupRegisterBLL().GetDeftrayState(paytype);
        return payStatus;
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
            obj = Convert.ToDateTime(obj).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours());
        }
        catch { }
        return obj;
    }

    protected void linkbtnOK_Click(object sender, CommandEventArgs e)
    {
        if (MemberOrderDAL.Getvalidteiscanpay(e.CommandArgument.ToString(), Session["Member"].ToString()))//限制订单必须有订货所属店铺推荐人协助人支付)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert('" + GetTran("007452", "该订单不属于您的协助或推荐报单，不能完成支付！") + "'); window.location.href='../Logout.aspx'; </script>");

            return;
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "mag", "var formobj=document.createElement('form');"
                                + "formobj.action='../payserver/chosepay.aspx?blif=" + EncryKey.GetEncryptstr(e.CommandArgument.ToString(), 1, 1) + "';" +
                                "formobj.method='post';formobj.target='_blank';document.body.appendChild(formobj); formobj.submit();", true);

        //Response.Redirect("chosepay.aspx?rd=" + e.CommandArgument.ToString() + "&rt=1");
    }

    private void GetShopList()
    {
        string number = Session["Member"].ToString();
        string sql = "o.number=h.number and ((o.number='" + number + "') or (o.isagain =0 and h.assister='" + number + "')) and o.OrderExpectNum=" + ddlQiShu.SelectedValue + " ";

        if (txtDate.Text != "" || txtDateEnd.Text != "")
        {
            sql += " and  o.orderdate between '" + txtDate.Text + "' and '" + txtDateEnd.Text + "' ";
        }

        sql += " and  o.DefrayState=0 ";
      
        this.Pager1.ControlName = "rep_TransferList";
        this.Pager1.key = "o.id";
        this.Pager1.PageColumn = " o.SendWay,o.OrderExpectNum,o.payExpectNum,o.OrderID,sendtype,o.Number,o.TotalMoney,o.StoreID,o.Totalpv,o.OrderDate,h.Name,o.defraystate as zhifu,o.ordertype as fuxiaoname,o.defraytype,o.IsReceivables, case o.defraystate when 1 then 1 else case o.paymentmoney when 0 then 0 else 1 end end as dpqueren,o.defraystate as gsqueren   ";
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = " memberorder o,memberinfo h ";
        this.Pager1.Condition = sql;
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();
    }

    public string GetCompany(string paytype)
    {
        string payStatus = "";
        switch (paytype)
        {
            case "0":
                payStatus = "<font color=red>" + GetTran("005634", "未收款") + "</font>";
                break;
            case "1":
                payStatus = GetTran("005636", "已收款");
                break;
            default:
                payStatus = "<font color=red>" + GetTran("005634", "未收款") + "</font>";
                break;
        }
        return payStatus;
    }

    public string GetStore(string paytype)
    {
        string payStatus = "";
        switch (paytype)
        {
            case "0":
                payStatus = "<font color=red>" + GetTran("005634", "未收款") + "</font>";
                break;
            case "1":
                payStatus = GetTran("005636", "已收款");
                break;
            default:
                payStatus = "<font color=red>" + GetTran("005634", "未收款") + "</font>";
                break;
        }
        return payStatus;
    }

    protected void gvorder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
            DataRowView drv = (DataRowView)e.Row.DataItem;
            string defrayType = drv["defraytype"].ToString();
            string defrayState = drv["zhifu"].ToString();
            string OrderID = drv["OrderID"].ToString();
            string expectNum = drv["OrderExpectNum"].ToString();
            double totalmoney = Convert.ToDouble(drv["totalmoney"]);


            double totalcomm = 0;
            double zongMoney = totalcomm + totalmoney;

            ImageButton LinkPM = (ImageButton)e.Row.FindControl("HyperLinkPayMent");
            ImageButton linkbtnModify = (ImageButton)e.Row.FindControl("linkbtnModify");
            ImageButton linkbtnDelete = (ImageButton)e.Row.FindControl("linkbtnDelete");

            Label lblTotalMoney = e.Row.FindControl("lblTotalMoney") as Label;

            if (AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString())) == 0)
            {
                lblTotalMoney.Text = "0.00";
            }
            else
            {
                lblTotalMoney.Text = (Convert.ToDouble(lblTotalMoney.Text) * AjaxClass.GetCurrency(Convert.ToInt32(bzCurrency), Convert.ToInt32(Session["Default_Currency"].ToString()))).ToString("f2");
            }
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            Transllations();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetShopList();
    }
    protected void linkbtnModify_Command(object sender, CommandEventArgs e)
    {
        string[] args = e.CommandArgument.ToString().Split(':');
        //Response.Redirect(string.Format("RegistMember.aspx?number={0}&orderID={1}&storeId={2}&mode=edit", args[2], args[0], args[1]));

        Response.Redirect("../RegisterMember/ShoppingCartView.aspx?orderid=" + args[1]);

    }
    /// <summary>
    /// 删除复消报单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linkbtnDelete_Click(object sender, CommandEventArgs e)
    {
        int maxEcept = CommonDataBLL.GetMaxqishu();
        string[] args = e.CommandArgument.ToString().Split(':');

        if (PublicClass.OrderDefrayState(args[0]))
        {
            ScriptHelper.SetAlert(Page, this.GetTran("000000", "该订单已支付，不能删除！"));
            return;
        }

        Model.MemberOrderModel model = MemberOrderBLL.GetMemberOrder(args[0]);
        AuditingMemberagainBLL aml = new AuditingMemberagainBLL();
        string result = "";
        if (model.IsAgain == 1)
        {
            result = aml.DelMembersDeclaration(args[0], Convert.ToDouble(model.TotalPv), model.Number, model.OrderExpect, model.StoreId, Convert.ToDouble(model.TotalMoney));
        }
        else
        {
            result = new BrowseMemberOrdersBLL().DelMembersDeclaration(model.Number, model.OrderExpect, model.OrderId, model.StoreId, Convert.ToDouble(model.LackProductMoney));
        }
        result = result == null ? (this.GetTran("000008", "删除成功")) : (result);//删除成功
        ScriptHelper.SetAlert(Page, result);
        GetShopList();
    }
}