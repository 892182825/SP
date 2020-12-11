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
using BLL.CommonClass;
using Model.Other;
using BLL.Registration_declarations;
using Model;
using System.Data.SqlClient;
using System.Collections.Generic;
using DAL;
using System.Text;
using System.IO;
using Standard.Classes;
using DAL.Other;

public partial class BrowseMemberOrders : BLL.TranslationBase
{
    BLL.Registration_declarations.BrowseMemberOrdersBLL bll = new BLL.Registration_declarations.BrowseMemberOrdersBLL();
    BLL.CommonClass.CommonDataBLL cbll = new BLL.CommonClass.CommonDataBLL();
    ViewFuXiaoBLL viewFuXiaoBLL = new ViewFuXiaoBLL();
    public int maxExpect = CommonDataBLL.GetMaxqishu();

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.BalanceBrowseMemberOrders);

        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        if (!IsPostBack)
        {
            Initddl();

            btnSearch_Click(null, null);

            Translate();
        }
    }


    public string GetRegisterDate(string rdate)
    {
        return Convert.ToDateTime(rdate).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();
    }


    protected void Initddl()
    {
        this.ExpectNum1.ExpectNum = CommonDataBLL.GetMaxqishu();
        BindCompare();
    }

    protected string Getgsqueren(string gsqueren)
    {
        string strGsqueren = "";
        switch (gsqueren)
        {
            case "0":
                strGsqueren = "<font color=red>" + GetTran("005634", "未收款") + "</font>";
                break;
            case "1":
                strGsqueren = GetTran("005636", "已收款");
                break;
            default:
                strGsqueren = "<font color=red>" + GetTran("005634", "未收款") + "</font>";
                break;
        }
        return strGsqueren;
    }

    protected void BindCompare()
    {
        ddlcompare.Items.Clear();
        this.txtContent.Text = "";

        if (ddlContion.SelectedValue.IndexOf("error") > 0)
        {
            //ddlcompare.Items.Add(new ListItem(this.GetTran("000881", "不限"), "all"));
            //ddlcompare.Items.Add(new ListItem(this.GetTran("000889", "所有错误"), "allErr"));
        }
        else
            ddlcompare.Items.Add(new ListItem(this.GetTran("000881", "不限"), "all"));

        if (ddlContion.SelectedValue != "B.TotalMoney" && ddlContion.SelectedValue != "B.TotalPv" && ddlContion.SelectedValue != "B.InvestJB")
        {
            if (ddlContion.SelectedValue != "B.DefrayState")
            {
                ddlcompare.Items.Add(new ListItem(this.GetTran("000821", "包含字符"), " like "));
                ddlcompare.Items.Add(new ListItem(this.GetTran("000822", "不包含字符"), " not like "));
            }
            else
            {
                ddlcompare.Items.Add(new ListItem(this.GetTran("000517", "已支付"), " B.DefrayState!=0 "));
                ddlcompare.Items.Add(new ListItem(this.GetTran("000521", "未支付"), " B.DefrayState=0 "));
            }
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
        string condition = "";
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
        if (this.ddlContion.SelectedValue.IndexOf("error") > 0)
        {
            if (this.ddlcompare.SelectedValue != "all")
            {
                condition += " and B.error <> ''";
            }
            if (this.ddlcompare.SelectedValue == " like " || this.ddlcompare.SelectedValue == " not like ")
            {
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
                    else if (ddlContion.SelectedValue == "A.PaperNumber")
                    {
                        content = Encryption.Encryption.GetEncryptionNumber(content);
                    }
                    else if (ddlContion.SelectedValue == "A.BankCard")
                    {
                        content = Encryption.Encryption.GetEncryptionCard(content);
                    }
                    condition += " and " + this.ddlContion.SelectedValue + " " + ddlcompare.SelectedValue + " '%" + content + "%'";
                }
            }
        }
        else
        {
            if (this.ddlcompare.SelectedValue == " like " || this.ddlcompare.SelectedValue == " not like ")
            {
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
                    else if (ddlContion.SelectedValue == "A.PaperNumber")
                    {
                        content = Encryption.Encryption.GetEncryptionNumber(content);
                    }
                    else if (ddlContion.SelectedValue == "A.BankCard")
                    {
                        content = Encryption.Encryption.GetEncryptionCard(content);
                    }
                    condition += " and " + this.ddlContion.SelectedValue + " " + ddlcompare.SelectedValue + " '%" + content + "%'";
                }

            }
            else if (this.ddlcompare.SelectedValue.IndexOf("B.DefrayState") > 0)
            {
                condition += " and " + ddlcompare.SelectedValue;
            }
            else if (this.ddlcompare.SelectedValue != "all")
            {
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
                    else if (ddlContion.SelectedValue == "A.PaperNumber")
                    {
                        content = Encryption.Encryption.GetEncryptionNumber(content);
                    }
                    else if (ddlContion.SelectedValue == "A.BankCard")
                    {
                        content = Encryption.Encryption.GetEncryptionCard(content);
                    }
                    condition += " and " + this.ddlContion.SelectedValue + " " + ddlcompare.SelectedValue + " " + content;
                }
            }
        }

        if (this.ddliszf.SelectedValue != "-1")
        {
            condition += " and " + ddliszf.SelectedValue;
        }

        #region 报单时间

        if (TextBox1.Text.Trim().Length > 0)
        {
            
            DateTime dtime;
            if (!DateTime.TryParse(TextBox1.Text, out dtime))
            {
                ScriptHelper.SetAlert(Page, GetTran("000827", "时间格式不正确！"));
                return;
            }
            condition += " and B.OrderDate>='" + dtime.AddHours(0 - Convert.ToInt32(Session["WTH"])).ToString() + "'";
        }
        if (TextBox2.Text.Trim().Length > 0)
        {
            DateTime dtime;
            if (!DateTime.TryParse(TextBox2.Text, out dtime))
            {
                ScriptHelper.SetAlert(Page, GetTran("000827", "时间格式不正确！"));
                return;
            }
            
            condition += " and B.OrderDate<'" + dtime.AddHours(24 - Convert.ToInt32(Session["WTH"])).ToString() + "'";
        }

        #endregion

        condition += "  and ordertype in(22,23) " ;

        string columns = "B.isSend,(C.Country+C.Province+C.City+C.Xian+B.ConAddress) as ConAddress,a.MobileTele as ConMobilPhone,B.SendWay,A.ID,A.Number,b.OrderID,B.IsReceivables,B.StoreID as OStoreID,A.Name,B.totalMoney,B.totalPv,B.InvestJB,B.OrderExpectNum,case B.PayExpectNum when -1 then '0' when 0 then '1' else convert(varchar,B.PayExpectNum) end as PayExpectNum,B.OrderDate,A.Remark,b.Error as Error,B.ordertype,case when B.ordertype=0 then '0' when B.ordertype = 3 then '3' when  B.ordertype = 4 then '4' else '5' end as RegisterWay ,  defraytype,B.defraytype,B.DefrayState, case when B.DefrayState = 0 then '0'  when  B.DefrayState = 1 then '1' else '2' end as PayStatus ,B.isAgain ,case B.isAgain when 0 then '注册报单' when 1 then '复销报单'  end as againType,case defraystate when 1 then 1 else case paymentmoney when 0 then 0 else 1 end end as dpqueren,defraystate as gsqueren";
        string table = "[MemberInfo]as A,MemberOrder as B left join City C  on C.CPCCode=B.CCPCCode";
        string condistions = " B.Number=A.Number " + expectNum + condition;

        ViewState["SQLSTR"] = "select " + columns + " from " + table + " where " + condistions + " order by b.id desc";

        this.Pager1.PageBind(0, 10, table, columns, condistions, "b.id", "gv_browOrder");
        this.Pager1.Visible = true;

        string zjef = GetZJE(" " + expectNum + condition);
        if (zjef != ",")
        {
            string[] zje = zjef.Split(',');
            for (int i = 0; i < zje.Length; i++)
            {
                lab_cjezj.Text = double.Parse(zje[0].ToString()).ToString("f4");
                lab_cjfzj.Text = double.Parse(zje[1].ToString()).ToString("f4");
            }
        }
        else
        {
            lab_cjezj.Text = "0";
            lab_cjfzj.Text = "0";
        }

        Translate();
    }

    /// <summary>
    /// 获得总积分和总金额
    /// </summary>
    /// <param name="sqltj">查询条件</param>
    /// <returns></returns>
    private static string GetZJE(string sqltj)
    {
        string zjef = "";
        DataTable dtTotal = DBHelper.ExecuteDataTable("select sum(totalmoney) as zje,sum(totalpv) as zjf from [MemberInfo]as A with(nolock) inner join MemberOrder as B with(nolock) on B.Number=A.Number where 1=1 " + sqltj);
        if (dtTotal.Rows.Count > 0)
        {
            zjef = dtTotal.Rows[0]["zje"].ToString() + "," + dtTotal.Rows[0]["zjf"].ToString();
        }
        return zjef;
    }

    /// <summary>
    /// GridView行处理事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gv_browOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            //解密姓名  
            e.Row.Cells[4].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[4].Text);
            //为了方便翻译，将数据库的中某些数字字段转为各国语言
            //1:错误信息,建议不要删除列而是因隐藏列，如果列数修改责以下代码要改动
            //e.Row.Cells[0].Text = e.Row.Cells[0].Text == "&nbsp;" ? (e.Row.Cells[0].Text):(new GroupRegisterBLL().GerCheckErrorInfo(e.Row.Cells[0].Text));
            //2:orderType
            //e.Row.Cells[6].Text = new GroupRegisterBLL().GetOrderType(e.Row.Cells[6].Text);
            //3:defrayType
            //e.Row.Cells[8].Text = new GroupRegisterBLL().GetDefryType(e.Row.Cells[8].Text);
            //4:defrayState
            //e.Row.Cells[7].Text = new GroupRegisterBLL().GetDeftrayState(e.Row.Cells[7].Text);

            LinkButton linkOK = (LinkButton)e.Row.FindControl("linkbtnOk");
            LinkButton linkbtnModify = (LinkButton)e.Row.FindControl("linkbtnModify");
            LinkButton linkbtnDelete = (LinkButton)e.Row.FindControl("linkbtnDelete");
            if (drv["orderExpectNum"].ToString() == maxExpect.ToString())
            {
                if (drv["defraystate"].ToString() == "0")
                {
                    linkbtnModify.Visible = true;
                   // linkbtnDelete.Visible = true;
                }
                else
                {
                    //linkbtnDelete.Visible = false;
                    linkbtnModify.Visible = false;
                    //if (drv["defraytype"].ToString() == "1" || drv["defraytype"].ToString() == "2")
                    //{
                    //    linkbtnModify.Visible = true;
                    //}
                }
            }
            //else
            //{
            //    linkbtnModify.Visible = false;
            //    linkbtnDelete.Visible = false;
            //}

            linkbtnDelete.Attributes["onclick"] = "return confirm('" + this.GetTran("001064", "确认要删除当前订单所有信息?") + "')";//确认要删除当前订单所有信息(包括基本信息)吗?

        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            this.Translate();
        }
    }

    /// <summary>
    /// 查看订单详细信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linkbtnquery_Click(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Query")
        {
            if (e.CommandArgument != null)
            {
                string orderId = e.CommandArgument.ToString();
                Response.Write("<SCRIPT language='javascript'>window.open('OrderDetails.aspx?orderId=" + orderId + "',   '报单详情',   'height='+400+',   width='+500+',   toolbar=no,   menubar=no')</SCRIPT>");
            }
        }
    }

    protected void gv_browOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string[] args = e.CommandArgument.ToString().Split(':');
        if (args.Length != 7)
        {
            ScriptHelper.SetAlert(Page, this.GetTran("001089", "数据异常"));
            return;
        }
        string orderId = args[0];//报单ID    
        MemberOrderModel mOrderModel = new MemberOrderModel();
        MemberInfoModel mInfoModel = null;
        if (args[5].Trim() == "1")
        {
            MemberOrderAgainBLL memberOrderAgainBLL = new MemberOrderAgainBLL();
            memberOrderAgainBLL.WriterDataToPage(mOrderModel, args[6], args[0]);
        }
        else
        {
            mInfoModel = new MemberInfoModel();
            AddOrderBLL AddOrderBLL = new AddOrderBLL();
            AddOrderBLL.GetDataFormInfoAndOrder(args[1], int.Parse(args[4]), args[6], mInfoModel, mOrderModel);
        }
        if (mOrderModel == null)
        {
            ScriptHelper.SetAlert(Page, this.GetTran("001784", "当前报单已经不存在"));
            return;
        }
        string number = args[1];
        string defrayname = args[2];
        string payStatus = args[3];
        string orderExpectNum = args[4];
        string isagain = args[5].ToString();
        string storeId = args[6].ToString();
        string SqlCon = DAL.DBHelper.connString;
        if (e.CommandName == "OK")
        {
            int selectIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
            Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.BalanceBrowseMemberOrdersEdit);
            if (payStatus == "0")
            {
                if (mOrderModel.IsAgain == 0)
                {
                    string info = MemberOrderAgainBLL.AuditingOrder((sender as LinkButton).CommandArgument.ToString());
                    if (info == "")
                    {
                        ScriptHelper.SetAlert(Page, "支付成功！");
                        btnSearch_Click(null, null);
                    }
                    else
                    {
                        ScriptHelper.SetAlert(Page, info);
                    }
                }
                else if (mOrderModel.IsAgain == 1)
                {
                    string info = MemberOrderAgainBLL.AuditingOrder((sender as LinkButton).CommandArgument.ToString());
                    if (info == "")
                    {
                        ScriptHelper.SetAlert(Page, "支付成功！");
                        btnSearch_Click(null, null);
                    }
                    else
                    {
                        ScriptHelper.SetAlert(Page, info);
                    }
                }
                else
                {
                    ScriptHelper.SetAlert(Page, this.GetTran("001786", "当前报单是否复销类型不正确."));
                    return;
                }
            }
            else
            {
                ScriptHelper.SetAlert(Page, this.GetTran("001789", "已经确认过的报单单不可再确认."));
                return;
            }
            btnSearch_Click(null, null);
        }
        else if (e.CommandName == "M")
        {
            if (payStatus == "0")
            {
                Response.Redirect("../RegisterUpdate1.aspx?OrderID=" + orderId + "&Number=" + number + "&CssType=3&storeId=" + storeId);
                //对未支付报单
                if (isagain == "0") //未确认注册修改
                {
                    Response.Redirect("../RegisterUpdate1.aspx?OrderID=" + orderId + "&Number=" + number + "&CssType=3&storeId=" + storeId);
                }
                else if (isagain == "1")          //未确认复销修改
                {
                    Response.Redirect("MemberOrderAgain.aspx?mode=edit&orderId=" + orderId + "&ordertype=" + mOrderModel.OrderType + "&flag=0&number=" + number + "&StoreID=" + storeId);
                }
                else
                {
                    return;
                }
            }
            else
            {
                ScriptHelper.SetAlert(Page, this.GetTran("000000", "无法对已支付的报单进行修改！"));
            }
        }
        else if (e.CommandName == "D")
        {
            //if (payStatus == "0")
            //{

                if (isagain == "0")
                {
                    Application.Lock();
                    //注册报单删除
                    string result = bll.DelMembersDeclaration(number, mInfoModel.ExpectNum, orderId, mOrderModel.StoreId, Convert.ToDouble(mOrderModel.LackProductMoney));
                    //返回null标识没有产生错误
                    Application.UnLock();
                    if (result == null)
                    {
                        result = this.GetTran("000008", "删除成功");
                    }
                    ScriptHelper.SetAlert(Page, result);
                }
                else if (isagain == "1")
                {
                    Application.Lock();
                    string result = viewFuXiaoBLL.DelOredrAgain(orderId, double.Parse(mOrderModel.TotalPv.ToString()), number, mOrderModel.OrderExpect, mOrderModel.StoreId);
                    Application.UnLock();
                    if (result == null)
                    {
                        result = this.GetTran("000008", "删除成功");

                    }
                    ScriptHelper.SetAlert(Page, result);
                }
                btnSearch_Click(null, null);
            //}
            //else
            //{



            //   // ScriptHelper.SetAlert(Page, this.GetTran("000000", "无法删除已支付的报单！"));
            //}
            //btnSearch_Click(null, null);
        }
        else if (e.CommandName == "E")
        {
            if (payStatus == "1")
            {
                string sql = @"UPDATE memberorder SET isSend = 1  WHERE OrderID = @orderId";
                SqlParameter[] para = 
            {
                new SqlParameter("@orderId",orderId)
            };
                int idd= DBHelper.ExecuteNonQuery(sql, para, CommandType.Text);
                if (idd == 1)
                {
                    ScriptHelper.SetAlert(Page, "发货成功！");
                }
                else {
                    ScriptHelper.SetAlert(Page, "发货失败！");
                }

            }
            else
            {
                ScriptHelper.SetAlert(Page, this.GetTran("000000", "未付款的报单没法发货！"));
            }
            btnSearch_Click(null, null);
        }
    }

    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        DataTable excelTable = DAL.DBHelper.ExecuteDataTable(ViewState["SQLSTR"].ToString());
        if (excelTable == null || excelTable.Rows.Count < 1)
        {
            Response.Write("<script language='javascript'>alert('" + GetTran("000051", "对不起，找不到指定条件的记录！") + "')</script>");
            return;
        }

        excelTable.Columns.Add("defraytypestr", typeof(System.String));
        excelTable.Columns.Add("queren", typeof(System.String));
        for (int i = 0; i < excelTable.Rows.Count; i++)
        {
            excelTable.Rows[i]["Name"] = Encryption.Encryption.GetDecipherName(Convert.ToString(excelTable.Rows[i]["Name"]));
            excelTable.Rows[i]["againType"] = Common.GetOrderType(Convert.ToInt32(excelTable.Rows[i]["orderType"].ToString()));
            excelTable.Rows[i]["PayStatus"] = new GroupRegisterBLL().GetDeftrayState(excelTable.Rows[i]["PayStatus"].ToString());
            excelTable.Rows[i]["OrderId"] = "&nbsp;" + excelTable.Rows[i]["OrderId"];
            excelTable.Rows[i]["queren"] = Getgsqueren(excelTable.Rows[i]["gsqueren"].ToString());
            excelTable.Rows[i]["OrderDate"] = Convert.ToDateTime(excelTable.Rows[i]["OrderDate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();
            excelTable.Rows[i]["defraytypestr"] = Common.GetOrderPayType(excelTable.Rows[i]["defraytype"]);
        }
        string[] coloums = { /*"error=" + this.GetTran("000742", "错误信息"),*/ "number=" + this.GetTran("000024", "会员编号"), "name=" + this.GetTran("000025", "会员姓名"), "orderExpectNum=" + this.GetTran("000045", "期数"), "PayExpectNum=" + this.GetTran("000780", "审核期数"), "OrderId=" + this.GetTran("000079", "订单号"), "againType=" + this.GetTran("000000", "报单类型"), "totalMoney=" + this.GetTran("000322", "金额"), "totalPv=" + this.GetTran("010001", "石斛积分"), "queren=" + this.GetTran("000000", "确认"), "OrderDate=" + this.GetTran("005942", "报单时间"), "ConMobilPhone=" + this.GetTran("000000", "收货人手机号"), "ConAddress=" + this.GetTran("000000", "收货人地址") };
        Excel.OutToExcel(excelTable, this.GetTran("001567", "报单浏览"), coloums);
    }

    /// <summary>
    /// 翻译方法
    /// </summary>
    public void Translate()
    {
        this.TranControls(this.ddliszf, new string[][] 
        {
            new string[] { "000440", "查看" }, 
         new string[] { "000517", "已支付"}, 
         new string[] { "000521", "未支付" }});

        btnSearch.Text = GetTran("000048", "查 询");
        this.TranControls(this.ddlContion, new string[][] 
        {  
         new string[] { "000024", "会员编号" },
         new string[] { "000025", "会员姓名" },
         //new string[] { "000027", "安置编号" },
         //new string[] { "000083", "证件号码" },
         //new string[] { "000087", "开户银行" },
         //new string[] { "000088", "银行帐号" },
         new string[] { "000079", "订单号" },
         new string[] { "000322", "金额" },
         new string[] { "010001", "石斛积分" },
        });

        this.TranControls(this.gv_browOrder, new string[][] 
        { 
         //new string[] { "000742", "错误信息" }, 
         new string[] { "000811", "确认" }, 
         new string[] { "000259", "修改" },
      
         new string[] { "000024", "会员编号" },
         new string[] { "000025", "会员姓名" },
        
         //new string[] { "000775", "支付状态" },
         //new string[] { "000186", "支付方式" },
         new string[] { "000045", "期数" },
         new string[] { "000780", "审核期数" },
         new string[] { "000079", "订单号" },
          new string[] { "000455", "报单类型 " },
         new string[] { "000322", "金额" },
        new string[] { "000000", "USDT" },
         new string[] { "000064", "确认" } ,
         
         new string[] { "005942", "注册日期" },
         new string[] { "000000", "收货人手机号" },
         new string[] { "000000", "收货人地址" },
         new string[] { "000078", "备注" },
         new string[] { "000078", "备注" },
         new string[] { "000440", "查看" },
            new string[] { "000022", "删除 " },  
        });

        this.TranControls(this.ddlType, new string[][] { 
            new string[]{"002055","报单期数"},
            new string[]{"000780","审核期数"},
        });
    }
    protected void LinkButton1_Command(object sender, CommandEventArgs e)
    {
        string orderid = e.CommandArgument.ToString();
        string storeid = e.CommandName;
        Response.Redirect(string.Format("OrderDetails.aspx?orderId={0}&storeId={1}", orderid, storeid));
    }
    protected void ddliszf_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddliszf.SelectedValue == "B.DefrayState=0")
        {
            this.ddlType.SelectedValue = "0";
            this.ddlType.Enabled = false;
        }
        else
        {
            this.ddlType.Enabled = true;
        }
    }
}