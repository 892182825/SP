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



public partial class Company_ModifyOldConsume : BLL.TranslationBase
{
    BLL.Registration_declarations.BrowseMemberOrdersBLL bll = new BLL.Registration_declarations.BrowseMemberOrdersBLL();
    BLL.CommonClass.CommonDataBLL cbll = new BLL.CommonClass.CommonDataBLL();
    ViewFuXiaoBLL viewFuXiaoBLL = new ViewFuXiaoBLL();
    public int maxExpect = CommonDataBLL.GetMaxqishu();

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.ModifyOldConsume);

        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        if (!IsPostBack)
        {
            BLL.CommonClass.CommonDataBLL.BindQishuListNew(ExpectNum1);
            Initddl();

            btnSearch_Click(null, null);
            //this.Pager1.PageBind(0, 10, "[MemberInfo]as A,MemberOrder as B", "B.SendWay,A.ID,A.Number,A.StoreID,b.OrderID,B.IsReceivables,B.StoreID as OStoreID,A.Name,A.PetName,B.totalMoney,B.totalPv,B.OrderExpectNum,case B.PayExpectNum when -1 then '0' when 0 then '1' else convert(varchar,B.PayExpectNum) end as PayExpectNum,A.RegisterDate as RegisterDatec,A.Remark,B.Error as Error,B.ordertype,case when B.ordertype=0 then '0' when B.ordertype = 3 then '3' when  B.ordertype = 4 then '4' else '5' end as RegisterWay , case when B.defraytype=1 then '1' when B.defraytype=2 then '2'  when B.defraytype=3 then '3' when B.defraytype=4 then '4' else '5' end as defrayname,B.defraytype,B.DefrayState, case when B.DefrayState = 0 then '0'  when  B.DefrayState = 1 then '1' else '2' end as PayStatus ,B.isAgain ,case B.isAgain when 0 then '1' when 1 then '2' when 5 then '5' when 6 then '6' end as againType", " B.Number=A.Number and " + this.GetNumber() + " and B.defraystate=1 and B.OrderExpectNum=" + (CommonDataBLL.getMaxqishu()-1), " B.Id ", "gv_browOrder");

            //ViewState["excelTable"] = "Select Case B.SendWay When 0 Then '0' Else '1' End As SendWay,A.ID,A.Number,A.StoreID,b.OrderID,B.IsReceivables,B.StoreID as OStoreID,A.Name,A.PetName,B.totalMoney,B.totalPv,B.OrderExpectNum,case B.PayExpectNum when -1 then '0' when 0 then '1' else convert(varchar,B.PayExpectNum) end as PayExpectNum,A.RegisterDate as RegisterDatec,A.Remark,B.Error as Error,B.ordertype,case when B.ordertype=0 then '0' when B.ordertype = 3 then '3' when  B.ordertype = 4 then '4' else '5' end as RegisterWay , case when B.defraytype=1 then '1' when B.defraytype=2 then '2'  when B.defraytype=3 then '3' when B.defraytype=4 then '4' else '5' end as defrayname,B.defraytype,B.DefrayState, case when B.DefrayState = 0 then '0'  when  B.DefrayState = 1 then '1' else '2' end as PayStatus ,B.isAgain ,case B.isAgain when 0 then '注册报单' when 1 then '复销报单' when 5 then '首次店铺团购' when 6 then '再次店铺团购' end as againType From [MemberInfo]as A,MemberOrder as B Where B.Number=A.Number And " + this.GetNumber() + " and B.defraystate=1 and B.OrderExpectNum=" + (CommonDataBLL.getMaxqishu()-1) +" Order By B.Id Desc";
            //this.Pager1.Visible = true;
            //string str=GetZJE(" and  B.Number=A.Number and " + this.GetNumber() + " and B.defraystate=1 and B.OrderExpectNum=" + (CommonDataBLL.getMaxqishu()-1));
            //if (str != "")
            //{
            //    try
            //    {
            //        lab_cjezj.Text =double.Parse(str.Split(',')[0]).ToString();
            //        lab_cjfzj.Text = double.Parse(str.Split(',')[1]).ToString();
            //    }
            //    catch
            //    {
            //        lab_cjezj.Text = "未知";
            //        lab_cjfzj.Text = "未知";
            //    }
            //}
        }
        
        Translate();
    }
    protected void Initddl()
    {
        BindCompare();
    }

    public string GetSendWay(string sendWay)
    {
        if (sendWay == "0")
        {
            return GetTran("007103", "公司发货到店铺");
        }
        return GetTran("007104", "公司直接发货给会员");

    }

    protected void BindCompare()
    {

        ddlcompare.Items.Clear();
        this.txtContent.Text = "";

        if (ddlContion.SelectedValue.IndexOf("error") > 0)
        {
            ddlcompare.Items.Add(new ListItem(this.GetTran("000881", "不限"), "all"));
            ddlcompare.Items.Add(new ListItem(this.GetTran("000889", "所有错误"), "allErr"));
        }
        else
            ddlcompare.Items.Add(new ListItem(this.GetTran("000881", "不限"), "all"));

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

    /// <summary>
    /// 解密姓名 
    /// </summary>
    /// <param name="obj">姓名</param>
    protected static string GetName(object obj)
    {
        if (obj != null)
        {
            return Encryption.Encryption.GetDecipherName(obj.ToString());
        }
        return "";
    }

    protected void ddlContion_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCompare();
    }

    private string GetNumber()
    {
        string manageID = Session["Company"].ToString();
        int count = 0;
        string number = BLL.CommonClass.CommonDataBLL.GetWLNumber1(manageID, out count);
        if (count == 0)
        {
            return " B.number in ('')";
        }
        return number;
    }

    /// <summary>
    /// 搜索按钮点击搜索事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string condition = " and  OrderExpectNum<" + maxExpect;
        string expectNum = "";
        if (ExpectNum1.SelectedValue != "-1")
            expectNum = " and B.OrderExpectNum=" + ExpectNum1.SelectedValue;
        if (this.ddlContion.SelectedValue.IndexOf("error") > 0)
        {
            if (this.ddlcompare.SelectedValue != "all")
            {
                condition += " and B.error <> ''";
            }
            if (this.ddlcompare.SelectedValue == " like " || this.ddlcompare.SelectedValue == " not like ")
            {
                if (txtContent.Text.Trim() != "")
                    condition += " and " + DisposeString.DisString(this.ddlContion.SelectedValue) + " " + ddlcompare.SelectedValue + " '%" + DisposeString.DisString(txtContent.Text) + "%'";
            }
        }
        else
        {

            if (this.ddlcompare.SelectedValue == " like " || this.ddlcompare.SelectedValue == " not like ")
            {
                if (txtContent.Text.Trim() != "")
                {
                    if (ddlContion.SelectedValue == "A.Name")
                    {
                        condition += " and " + DisposeString.DisString(this.ddlContion.SelectedValue) + " " + ddlcompare.SelectedValue + " '%" + Encryption.Encryption.GetEncryptionName(DisposeString.DisString(txtContent.Text)) + "%'";
                    }
                    else
                    {
                        condition += " and " + DisposeString.DisString(this.ddlContion.SelectedValue) + " " + ddlcompare.SelectedValue + " '%" + DisposeString.DisString(txtContent.Text) + "%'";
                    }
                }
            }
            else if (this.ddlcompare.SelectedValue != "all")
            {
                if (txtContent.Text.Trim() != "")
                {
                    if (ddlContion.SelectedValue == "B.TotalMoney")
                    {
                        try
                        {
                            double ads = Convert.ToDouble(this.txtContent.Text.Trim());
                            if (ads > 9999999)
                            {
                                ScriptHelper.SetAlert(Page, "金额输入过大.");
                                return;
                            }
                        }
                        catch
                        {
                            ScriptHelper.SetAlert(Page, "金额格式输入错误.");
                            return;
                        }
                        condition += " and " + DisposeString.DisString(this.ddlContion.SelectedValue) + " " + ddlcompare.SelectedValue + " " + DisposeString.DisString(txtContent.Text.Trim());
                    }
                    else if (ddlContion.SelectedValue == "B.TotalPv")
                    {
                        try
                        {
                            double ads = Convert.ToDouble(this.txtContent.Text.Trim());
                            if (ads > 9999999)
                            {
                                ScriptHelper.SetAlert(Page, "积分输入过大.");
                                return;
                            }
                        }
                        catch
                        {
                            ScriptHelper.SetAlert(Page, "积分格式输入错误.");
                            return;
                        }
                        condition += " and " + DisposeString.DisString(this.ddlContion.SelectedValue) + " " + ddlcompare.SelectedValue + " " + DisposeString.DisString(txtContent.Text.Trim());
                    }
                    else
                    {
                        condition += " and " + DisposeString.DisString(this.ddlContion.SelectedValue) + " " + DisposeString.DisString(ddlcompare.SelectedValue) + " " + DisposeString.DisString(txtContent.Text);
                    }
                }
            }
        }
        condition += " and B.defraystate=1";
        //condition += " and " + GetNumber();
        this.Pager1.PageBind(0, 10, @"[MemberInfo]as A,MemberOrder as B", "B.SendWay,A.ID,A.Number,A.StoreID,b.OrderID,B.IsReceivables,B.StoreID as OStoreID,A.Name,A.PetName,B.totalMoney,B.totalPv,B.OrderExpectNum,case B.PayExpectNum when -1 then '0' when 0 then '1' else convert(varchar,B.PayExpectNum) end as PayExpectNum,A.RegisterDate as RegisterDatec,A.Remark,B.Error as Error,B.ordertype,case when B.ordertype=0 then '0' when B.ordertype = 3 then '3' when  B.ordertype = 4 then '4' else '5' end as RegisterWay , case when B.defraytype=1 then '1' when B.defraytype=2 then '2'  when B.defraytype=3 then '3' when B.defraytype=4 then '4' else '5' end as defrayname,B.defraytype,B.DefrayState, case when B.DefrayState = 0 then '0'  when  B.DefrayState = 1 then '1' else '2' end as PayStatus ,B.isAgain ,case B.isAgain when 0 then '注册报单' when 1 then '复销报单' when 5 then '首次店铺团购' when 6 then '再次店铺团购' end as againType", " B.Number=A.Number " + expectNum + condition, " B.Id ", "gv_browOrder");
        //this.excelTable = new BrowseMemberOrdersBLL().GetInfoAndOrder("[MemberInfo]as A,MemberOrder as B", "A.ID,A.Number,A.StoreID,b.OrderID,B.IsReceivables,B.StoreID as OStoreID,A.Name,A.PetName,B.totalMoney,B.totalPv,B.OrderExpectNum,case B.PayExpectNum when -1 then '0' when 0 then '1' else convert(varchar,B.PayExpectNum) end as PayExpectNum,A.RegisterDate as RegisterDatec,A.Remark,B.Error as Error,B.ordertype,case when B.ordertype=0 then '0' when B.ordertype = 3 then '3' when  B.ordertype = 4 then '4' else '5' end as RegisterWay , case when B.defraytype=1 then '1' when B.defraytype=2 then '2'  when B.defraytype=3 then '3' when B.defraytype=4 then '4' else '5' end as defrayname,B.defraytype,B.DefrayState, case when B.DefrayState = 0 then '0'  when  B.DefrayState = 1 then '1' else '2' end as PayStatus ,B.isAgain ,case B.isAgain when 0 then '注册报单' when 1 then '复销报单' when 5 then '首次店铺团购' when 6 then '再次店铺团购' end as againType", " B.Number=A.Number " + expectNum + condition, " B.Id ");
        ViewState["excelTable"] = "Select Case B.SendWay When 0 Then '0' Else '1' End As SendWay,A.ID,A.Number,A.StoreID,b.OrderID,B.IsReceivables,B.StoreID as OStoreID,A.Name,A.PetName,B.totalMoney,B.totalPv,B.OrderExpectNum,case B.PayExpectNum when -1 then '0' when 0 then '1' else convert(varchar,B.PayExpectNum) end as PayExpectNum,A.RegisterDate as RegisterDatec,A.Remark,B.Error as Error,B.ordertype,case when B.ordertype=0 then '0' when B.ordertype = 3 then '3' when  B.ordertype = 4 then '4' else '5' end as RegisterWay , case when B.defraytype=1 then '1' when B.defraytype=2 then '2'  when B.defraytype=3 then '3' when B.defraytype=4 then '4' else '5' end as defrayname,B.defraytype,B.DefrayState, case when B.DefrayState = 0 then '0'  when  B.DefrayState = 1 then '1' else '2' end as PayStatus ,B.isAgain ,case B.isAgain when 0 then '注册报单' when 1 then '复销报单' when 5 then '首次店铺团购' when 6 then '再次店铺团购' end as againType From [MemberInfo]as A,MemberOrder as B Where B.Number=A.Number" + expectNum + condition + " Order By B.Id Desc";
        this.Pager1.Visible = true;

        string str = GetZJE(" " + expectNum + condition);
        if (str != ",") {
            try
            {
                lab_cjezj.Text = str.Split(',')[0];
                lab_cjfzj.Text = str.Split(',')[1];
            }
            catch {
                lab_cjezj.Text = "未知";
                lab_cjfzj.Text = "未知";
            }
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
        DataTable dtTotal = DBHelper.ExecuteDataTable("select sum(totalmoney) as zje,sum(totalpv) as zjf from [MemberInfo]as A,MemberOrder as B where B.Number=A.Number and 1=1 " + sqltj);
        zjef = dtTotal.Rows[0]["zje"].ToString() + "," + dtTotal.Rows[0]["zjf"].ToString();
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

            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");

            //为了方便翻译，将数据库的中某些数字字段转为中文
            //1:错误信息
            e.Row.Cells[0].Text = e.Row.Cells[0].Text == "&nbsp;" ? (new GroupRegisterBLL().GerCheckErrorInfo(e.Row.Cells[0].Text)) : (e.Row.Cells[0].Text);
            //2:orderType
            e.Row.Cells[7].Text = new GroupRegisterBLL().GetOrderType(e.Row.Cells[7].Text);
            //3:defrayType
            e.Row.Cells[9].Text = new GroupRegisterBLL().GetDefryType(e.Row.Cells[9].Text);
            //4:defrayState
            e.Row.Cells[8].Text = new GroupRegisterBLL().GetDeftrayState(e.Row.Cells[8].Text);

            LinkButton linkOK = (LinkButton)e.Row.FindControl("linkbtnOk");
            LinkButton linkbtnDelete = (LinkButton)e.Row.FindControl("linkbtnDelete");
            linkbtnDelete.Attributes["onclick"] = "return confirm('" + this.GetTran("001064", "确认要删除当前订单所有信息?") + "')";
        }
        if (e.Row.RowType == DataControlRowType.Header)
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

    /// <summary>
    /// 确认会员报单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linkbtnOK_Click(object sender, CommandEventArgs e)
    {

    }


    /// <summary>
    /// 修改会员报单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void linkbtnModify_Click(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            if (e.CommandArgument != null)
            {
                Response.Redirect("RegisterMember.aspx?orderId=" + e.CommandArgument.ToString() + "&");
            }

        }
    }

    protected void gv_browOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string[] args = e.CommandArgument.ToString().Split(':');
        if (args.Length != 7)
        {
            ScriptHelper.SetAlert(Page, "数据异常");
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
            ScriptHelper.SetAlert(Page, "当前报单已经不存在.");
            return;
        }
        string number = args[1];
        string defrayname = args[2];
        string payStatus = args[3];
        string orderExpectNum = args[4];
        string isagain = args[5].ToString();
        string storeId = args[6].ToString();
        string SqlCon = System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        //如果是修改
        if (e.CommandName == "M")
        {
            //          Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.BalanceBrowseMemberOrdersEdit);
            if (payStatus == "0")
            {
                //对未支付报单
                if (isagain == "0") //未确认注册修改
                {
                    //Response.Redirect("RegisterMember.aspx?mode=edit&orderId=" + orderId + "&flag=0&number=" + number + "&StoreID=" + storeId);

                    Response.Redirect("../RegisterUpdate1.aspx?OrderID=" + orderId + "&Number=" + number + "&CssType=3&storeId=" + storeId);

                }
                else if (isagain == "1")          //未确认复销修改
                {
                    Response.Redirect("MemberOrderAgain.aspx?mode=edit&orderId=" + orderId + "&ordertype=" + mOrderModel.OrderType + "&flag=0&number=" + number + "&StoreID=" + storeId);

                   // Response.Redirect("../RegisterUpdate1.aspx?OrderID=" + orderId + "&Number=" + number + "&CssType=3&storeId=" + storeId);
                }
                else
                {
                    return;
                }
            }
            else if (payStatus == "1" && defrayname == "1")
            {
                //对已支付报单
                if (isagain == "0")    //已确认注册修改
                {
                    if (mOrderModel.OrderType == 4)
                    {
                        //Response.Redirect("RegisterMember1.aspx?mode=edit&orderId=" + orderId + "&flag=1&number=" + number + "&StoreID=" + storeId + "&except=" + orderExpectNum);
                        Response.Redirect("../RegisterUpdate1.aspx?OrderID=" + orderId + "&Number=" + number + "&CssType=3&storeId=" + storeId + "&tp=1");
                    }
                    else
                    {
                        //Server.Transfer("RegisterMember.aspx?mode=edit&orderId=" + orderId + "&flag=1&number=" + number + "&StoreID=" + storeId+"&ExceptOld=true");

                        Response.Redirect("../RegisterUpdate1.aspx?OrderID=" + orderId + "&Number=" + number + "&CssType=3&storeId=" + storeId);
                    }
                }
                else if (isagain == "1")         //已确认复销修改
                {
                    Response.Redirect("MemberOrderAgain.aspx?mode=edit&orderId=" + orderId + "&ordertype=" + mOrderModel.OrderType + "&flag=1&number=" + number + "&StoreID=" + storeId);
                    //Response.Redirect("../RegisterUpdate1.aspx?OrderID=" + orderId + "&Number=" + number + "&CssType=3&storeId=" + storeId);
                }
                else
                {
                    return;
                }
            }
        }
    }

    //protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    //{

    //    for (int i = 0; i < excelTable.Rows.Count; i++)
    //    {
    //        excelTable.Rows[i]["Name"] = Encryption.Encryption.GetDecipherName(Convert.ToString(excelTable.Rows[i]["Name"]));

    //    }
    //    string[] coloums = { "number=" + this.GetTran("000024", "会员编号"), "name=" + this.GetTran("000025", "会员姓名"), "OStoreId=" + this.GetTran("000030", "会员店铺编号"), "againType=" + this.GetTran("000455", "报单类型"), "PayStatus=" + this.GetTran("000109", "支付状态"), "defrayname=" + this.GetTran("000186", "支付方式"), "orderExpectNum=" + this.GetTran("000045", "期数"), "PayExpectNum=" + this.GetTran("000780", "审核期数"), "OrderId=" + this.GetTran("000079", "订单号"), "totalMoney=" + this.GetTran("000322", "金额"), "totalPv=" + this.GetTran("000414", "积分"), "RegisterDatec=" + this.GetTran("000031", "注册日期") };
    //    Excel.OutToExcel(this.excelTable, this.GetTran("005010", "批量修改"), coloums);

    //}

    protected void btnDownExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = DBHelper.ExecuteDataTable(ViewState["excelTable"].ToString());
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["Name"] = Encryption.Encryption.GetDecipherName(Convert.ToString(dt.Rows[i]["Name"]));

        }
        string[] coloums = { "number=" + this.GetTran("000024", "会员编号"), "name=" + this.GetTran("000025", "会员姓名"), "OStoreId=" + this.GetTran("000030", "会员店铺编号"), "againType=" + this.GetTran("000455", "报单类型"), "PayStatus=" + this.GetTran("000109", "支付状态"), "defrayname=" + this.GetTran("000186", "支付方式"), "SendWay=" + this.GetTran("001345", "发货方式"), "orderExpectNum=" + this.GetTran("000045", "期数"), "PayExpectNum=" + this.GetTran("000780", "审核期数"), "OrderId=" + this.GetTran("000079", "订单号"), "totalMoney=" + this.GetTran("000322", "金额"), "totalPv=" + this.GetTran("000414", "积分"), "RegisterDatec=" + this.GetTran("000031", "注册日期") };
        Excel.OutToExcel(dt, this.GetTran("005010", "批量修改"), coloums);
    }


    /// <summary>
    /// 翻译方法
    /// </summary>
    public void Translate()
    {

        this.TranControls(this.ddlContion, new string[][] 
        { 
         new string[] { "000742", "错误信息" }, 
         new string[] { "000150", "店铺编号" }, 
         new string[] { "000024", "会员编号" },
         new string[] { "000025", "会员姓名" },
         new string[] { "000079", "订单号" },
         new string[] { "000322", "金额" },
         new string[] { "000414", "积分" },
        });

        this.TranControls(this.gv_browOrder, new string[][] 
        { 
         new string[] { "000742", "错误信息" }, 
         new string[] { "000811", "确认" }, 
         new string[] { "000259", "修改" },
         new string[] { "000022", "删除 " },  
         new string[] { "000024", "会员编号" },
         new string[] { "000025", "会员姓名" },
         new string[] { "000030", "报单店铺编号" },
         new string[] { "000455", "报单类型 " },
         new string[] { "000775", "支付状态" },
         new string[] { "000186", "支付方式" },
         new string[] { "001345", "发货方式"},
         new string[] { "000045", "期数" },
         new string[] { "000780", "审核期数" },
         new string[] { "000079", "订单号" },
         new string[] { "000322", "金额" },
         new string[] { "000414", "积分" },
         new string[] { "000057", "注册日期" },
         new string[] { "000078", "备注" },
         new string[] { "000078", "备注" },
         new string[] { "000440", "查看" },
        });

        //this.TranControls(this.btnSearch, new string[][] { new string[] { "000695", "搜索" } });

    }
    protected void LinkButton1_Command(object sender, CommandEventArgs e)
    {
        string orderid = e.CommandArgument.ToString();
        string storeid = e.CommandName;
        Response.Redirect(string.Format("OrderDetails.aspx?orderId={0}&storeId={1}", orderid, storeid));
    }
}
