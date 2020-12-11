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
using DAL;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using BLL.CommonClass;
using System.Collections.Generic;
using System.Text;
using Standard;
using BLL.Registration_declarations;

public partial class Store_auditingmemberagain : BLL.TranslationBase
{
    BLL.Registration_declarations.BrowseMemberOrdersBLL bll = new BLL.Registration_declarations.BrowseMemberOrdersBLL();
    BLL.CommonClass.CommonDataBLL cbll = new BLL.CommonClass.CommonDataBLL();
    BLL.Registration_declarations.BrowseMemberOrdersBLL browseMemberOrders = new BLL.Registration_declarations.BrowseMemberOrdersBLL();
    BLL.Registration_declarations.AuditingMemberagainBLL auditingMemberagainBLL = new BLL.Registration_declarations.AuditingMemberagainBLL();

    public string msg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        AddOrderBLL.BindCurrency_Rate(ddlCurrency);

        if (!IsPostBack)
        {
            BLL.CommonClass.CommonDataBLL.BindQishuList(ddlVolume, true);
            BindCompare();
            string storeId = Session["Store"] == null ? "" : Session["Store"].ToString();//测试用

            QueryWhere(storeId, Convert.ToInt32(ddlVolume.SelectedValue), "", "", "");
        }
        Translations();
    }


    public string GetSendWay(string sendWay)
    {
        if (sendWay == "0")
        {
            return GetTran("007103", "公司发货到店铺");
        }
        return GetTran("007104", "公司直接发货给会员");

    }

    /// <summary>
    /// 翻译
    /// </summary>
    private void Translations()
    {
        this.TranControls(this.ddlContion, new string[][] { 
                new string[] { "000024", "会员编号" }, 
                new string[] { "000025", "会员姓名" }, 
                new string[] { "000026", "推荐编号" }, 
                new string[] { "000083", "证件号码" },
                new string[] { "000087", "开户银行" },
                new string[] { "000088", "银行帐号" },
                new string[] { "000079", "订单号" },
                new string[] { "000322", "金额" },
                new string[] { "000414", "积分" }
        });
        this.TranControls(this.gv_browOrder, new string[][] { 
                new string[] { "000440", "查看" }, 
                new string[] { "006048", "公司确认" }, 
                  new string[] {"006049","店铺确认"},
                new string[] { "000259", "修改" }, 
                new string[] { "000022", "删除" }, 
              
                new string[] { "000024", "会员编号" }, 
                new string[] { "000025", "会员姓名" }, 
                new string[] { "000775", "支付状态" },
                new string[] { "000186", "支付方式" },
                new string[] { "001345", "发货方式"},
                new string[] { "000045", "期数" },
                new string[] { "000322", "金额" },
                new string[] { "000414", "积分" },
                new string[] { "001429", "报单日期" },
                new string[] { "007078", "操作者编号" },
                new string[] { "006050", "公司确认状态" },
                new string[] { "006051", "店铺确认状态" },
                new string[] { "000078", "备注" },
        });


    }

    /// <summary>
    ///  解密姓名
    /// </summary>
    public object GetNumberName(object name)
    {
        //解密姓名
        string namestr = Encryption.Encryption.GetDecipherName(name.ToString());
        return namestr;
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

    public string GetPayStatus(string paytype)
    {
        string payStatus = "";
        switch (paytype)
        {
            case "0":
                payStatus = "<font color=red>" + this.GetTran("000521", "未支付") + "</font>";
                break;
            case "1":
                payStatus = this.GetTran("000517", "已支付");
                break;
            default:
                payStatus = "<font color=red>" + this.GetTran("000521", "未支付") + "</font>";
                break;
        }
        return payStatus;
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
                defrayName = "支付宝支付";
                break;
            case "4":
                defrayName = "快钱支付";
                break;
            default:
                defrayName = this.GetTran("000699", "现金");
                break;
        }
        return defrayName;
    }

    /// <summary>
    ///绑定比较符
    /// </summary>
    protected void BindCompare()
    {
        ddlcompare.Items.Clear();
        txtContent.Text = "";

        if (ddlContion.SelectedValue.IndexOf("error") > 0)
        {
            ddlcompare.Items.Add(new ListItem(GetTran("000881", "不限"), "all"));
            ddlcompare.Items.Add(new ListItem(GetTran("000889", "所有错误"), "allErr"));
        }
        else
            ddlcompare.Items.Add(new ListItem(GetTran("000881", "不限"), "all"));

        if (ddlContion.SelectedValue != "MO.TotalMoney" && ddlContion.SelectedValue != "MO.TotalPv")
        {
            ddlcompare.Items.Add(new ListItem(GetTran("000821", "包含字符"), " like "));
            ddlcompare.Items.Add(new ListItem(GetTran("000822", "不包含字符"), " not like "));
        }
        else
        {
            ddlcompare.Items.Add(new ListItem(GetTran("000802", "数值等于"), " = "));
            ddlcompare.Items.Add(new ListItem(GetTran("000804", "数值大于"), " > "));
            ddlcompare.Items.Add(new ListItem(GetTran("000808", "数值小于"), " < "));
            ddlcompare.Items.Add(new ListItem(GetTran("000810", "数值大于等于"), " >= "));
            ddlcompare.Items.Add(new ListItem(GetTran("000813", "数值小于等于"), " <= "));
            ddlcompare.Items.Add(new ListItem(GetTran("000816", "数值不等于"), " <> "));
        }
    }

    protected void ddlContion_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCompare();
    }

    /// <summary>
    /// 行命令事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gv_browOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string cag = e.CommandArgument.ToString();
        string cname = e.CommandName.ToString();
        if (cname == "qur")  //查看
        {
            string[] parms = cag.Split('|');
            Response.Redirect("OrderDetails.aspx?orderId=" + parms[0] + "&storeId=" + parms[1]);
        }
        else if (cname == "Auditing")  //公司确认
        {
            string[] parms = cag.Split('|');
            int res = 0;// AddOrderDataDAL.OrderPayment(parms[1], parms[0], CommonDataBLL.OperateIP, CommonDataBLL.OperateBh, "", 4, 1, 1, 0);
            //string info = BrowseMemberOrdersBLL.AuditingOrder(cag);
            if (res == 0)
            {
                ScriptHelper.SetAlert(Page, GetTran("000978", "支付成功！"));
                BindData();
            }
            else
            {
                ScriptHelper.SetAlert(Page, GetTran("005715", "支付失败"));
            }
        }
        else if (cname == "MDF")   //修改
        {
            Response.Redirect("MemberOrderAgain.aspx?mode=edit&orderId=" + cag);
        }
        else if (cname == "Del")
        {
            Delete(e.CommandArgument.ToString());
        }
    }

    /// <summary>
    /// 删除会员自由购物信息
    /// </summary>
    /// <param name="CommandArgument"></param>
    private void Delete(string CommandArgument)
    {
        //ispay判断是否确认过
        string isPay = null;

        string[] parms = CommandArgument.Split('|');
        double totalPV = new AddFreeOrderDAL().GetTotalPV(parms[2], out isPay);
        double lackproductmoney = new AddFreeOrderDAL().GetTotalmoney(parms[2]);

        int maxExcept = BLL.CommonClass.CommonDataBLL.getMaxqishu();
        BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("memberorder", "orderid");//实例日志类
        cl_h_info.AddRecord(parms[2]);//添加日志，修改前记录原来数据

        string result = auditingMemberagainBLL.DelMembersDeclaration(parms[2], totalPV, parms[0], maxExcept, parms[3], lackproductmoney);
        if (result == null)
        {
            cl_h_info.DeletedIntoLogs(BLL.CommonClass.ChangeCategory.store3, Session["Store"].ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype5);//插入日志

            ScriptHelper.SetAlert(Page, GetTran("000749", "删除成功!"));
            //重新绑定

            BindData();

        }
        else ScriptHelper.SetAlert(Page, result);
    }
    //搜索
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    public string GetRegisterDate(string regDate)
    {
        try
        {
            regDate = Convert.ToDateTime(regDate).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();
        }
        catch { }
        return regDate;
    }

    protected void BindData()
    {
        //string storeId = Session["Store"].ToString();//测试用
        int volume = Convert.ToInt32(ddlVolume.SelectedValue);
        string condition = ddlContion.SelectedItem.Value;
        string compare = ddlcompare.SelectedValue;
        string content = txtContent.Text.Trim();
        if (ddlContion.SelectedValue == "MO.TotalMoney" || ddlContion.SelectedValue == "MO.TotalPv")
        {
            try
            {
                Convert.ToDouble(txtContent.Text.Trim());
                content = Convert.ToDouble(txtContent.Text.Trim()).ToString();
            }
            catch
            {
                msg = "<script>alert('" + GetTran("006918", "查询条件输入格式错误，请重新输入！") + "');</scipt>";
                return;
            }
        }

        QueryWhere("", volume, condition, compare, content);
    }
    
    protected void QueryWhere(string storeId, int volume, string condition, string compare, string content)
    {
        this.Pager1.Visible = true;

        //this.lbbaodanmoney.Text = new BLL.Registration_declarations.RegistermemberBLL().GetLeftRegisterMemberMoney(storeId);

        try
        {
            if (this.txtBox_OrderDateTimeEnd.Text.Trim() != "")
            {
                Convert.ToDateTime(txtBox_OrderDateTimeEnd.Text.Trim());
            }
            if (this.txtBox_OrderDateTimeStart.Text.Trim() != "")
            {
                Convert.ToDateTime(txtBox_OrderDateTimeStart.Text.Trim());
            }
        }
        catch
        {
            msg = "<script>alert('" + GetTran("000450", "日期格式输入错误！") + "');</scipt>";
            return;
        }
        string endTime = this.txtBox_OrderDateTimeEnd.Text.Trim();
        string startTime = this.txtBox_OrderDateTimeStart.Text.Trim();
        string iszf = this.rtbiszf.SelectedValue;
        BLL.Registration_declarations.PagerParmsInit model = auditingMemberagainBLL.QueryWhere2(volume.ToString(), storeId, condition, compare, content, iszf, endTime, startTime);
        if (model.ErrInfo != null)
            ScriptHelper.SetAlert(Page, model.ErrInfo);
        else
        {
            // this.Pager1.PageBind(model.PageIndex, model.PageSize, model.PageTable, model.PageColumn, model.SqlWhere, model.Key, model.ControlName);
            //this.Pager1.PageSorting(1, model.PageSize, model.PageTable, model.PageColumn, model.SqlWhere, model.Key, model.ControlName);
            Pager1.PageBind(0, model.PageSize, model.PageTable, model.PageColumn, model.SqlWhere, model.Key, model.ControlName);
        }
    }

    protected void gv_browOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations();
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            LinkButton IsAuditing = (LinkButton)e.Row.FindControl("IsAuditing");
            LinkButton Modify = (LinkButton)e.Row.FindControl("HyperModify");
            LinkButton delLink = (LinkButton)e.Row.FindControl("delLink");

            //取得绑定的支付状态
            string AuditingState = drv["defrayState"].ToString();

            IsAuditing.Attributes.Add("onclick", "return confirm('" + GetTran("001722", "确认支付此订单吗？") + "')");
            delLink.Attributes["onclick"] = "return confirm('" + GetTran("001725", "确认要删除当前会员及购物订单吗?") + "')";


            //取得绑定的支付类型
            string defraytype = drv["defraytype"].ToString();

            //获取订单号
            string orderID = drv["orderId"].ToString();
            string HidName = drv["name"].ToString();
            string hidnumber = drv["number"].ToString();
            string ordertype = drv["ordertype"].ToString();
            string paycrr = drv["paycrr"].ToString();
            string hidtotalMoney = drv["totalMoney"].ToString();
            string StoreID = drv["StoreID"].ToString();

            string hidIsReceivables = drv["IsReceivables"].ToString();


            if (AuditingState == "0")//未支付
            {
                if (defraytype != "3" && defraytype != "4")
                {
                    if (hidIsReceivables == "0")
                    {
                        (e.Row.Cells[4].FindControl("labcz") as Literal).Text = "<a onclick='javascript:Dialogsearch(\"" + hidnumber + "\",\"" + GetNumberName(HidName) + "\",\"" + orderID + "\",\"" + ordertype + "\",\"" + paycrr + "\",\"" + hidtotalMoney + "\",\"" + StoreID + "\")' href='#'>" + GetTran("000064", "确认") + "</a>";
                    }

                    // Modify.NavigateUrl = Modify.NavigateUrl + "&ordertype=" + DataBinder.Eval(e.Row.DataItem, "ordertype").ToString() + "&number=" + DataBinder.Eval(e.Row.DataItem, "number").ToString() + "&flag=0";
                }
            }
            else
            {
                IsAuditing.Visible = false;
                if (defraytype != "1")//不是现金支付
                {
                    delLink.Visible = false;
                    Modify.Visible = false;
                }
                //Modify.NavigateUrl = Modify.NavigateUrl + "&ordertype=" + DataBinder.Eval(e.Row.DataItem, "ordertype").ToString() + "&number=" + DataBinder.Eval(e.Row.DataItem, "number").ToString() + "&flag=1";
            }
        }
    }

    protected string SetVisible(string dd, string orderID)
    {
        if (dd.Length > 0)
        {
            string _openWin = "<a href='ShowRemark.aspx?from=auditingmemberorders&orderID=" + orderID + "'>" + this.GetTran("000440") + "</a>";

            return _openWin;
        }
        else
        {
            return "无";
        }
    }
}