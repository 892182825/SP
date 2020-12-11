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
using Model;
using System.Collections.Generic;
using BLL.Logistics;
using Model.Other;
using BLL.Registration_declarations;
using Model.Const;
using BLL.other.Company;

public partial class Company_ProductOrders_RefundmentOrderListForMember : BLL.TranslationBase
{
    ReturnedGoodsBLL returnedGoodsBLL = new ReturnedGoodsBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        GetUrlParams();
        if (string.IsNullOrEmpty(MemberID)&&Session["Company"]!=null)
        {//从公司子系统进入
            LoginIDEntity = 0;
            Permissions.ComRedirect(Page, Permissions.redirUrl);
            //权限
            Response.Cache.SetExpires(DateTime.Now);
            Permissions.CheckManagePermission(EnumCompanyPermission.LogisticsRefundmentOrderBrowse);
        }
        else
        {//从会员子系统进入
            LoginIDEntity = 2;
            Permissions.MemRedirect(Page, Permissions.redirUrl);
        }
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            BindCountryList();
            btn_Submit_Click(null, null);
            // Pager1.PageBind(0, 10, "InventoryDoc As D", "*", GetSQL(), "docid", "gvRefundmentBrowse");
        }
        Translations();
    }

    /// <summary>
    /// 翻译
    /// </summary>
    private void Translations()
    {
        //this.TranControls(this.btn_Submit, new string[][] { new string[] { "000048", "查 询" } });
        this.TranControls(this.btnAdd, new string[][] { new string[] { "001894", "添加退货单" } });
        this.TranControls(this.DropDownList_Items, new string[][] 
        { 
            new string[]{ "000045", "期数" } ,
            new string[]{ "001820", "总价格" } ,
            new string[]{ "001814", "退货日期" }
        });
        this.TranControls(this.DDPStatus, new string[][] 
        { 
            new string[]{ "000633", "全部" },
            new string[] { "001009", "未审核" } ,
            new string[]{ "001011", "已审核" } 
        });

        //this.TranControls(this.gvRefundmentBrowse, new string[][] { 
        //        new string[] { "000015", "操作" }, //"001802", "审核退货单"
        //        //new string[] { "001804", "退货入哪个库" },
        //        new string[] { "001809", "退货单号" }, 
        //        new string[] { "000079", "订单号" },
        //        new string[] { "000045", "期数" }, 
        //        new string[] { "000605", "是否审核" },
        //        new string[] { "001811", "是否锁定" },
        //        new string[] { "001812", "退货总价" },
        //        new string[] { "001813", "退货总积分" },
        //        new string[] { "001814", "退货日期" },
        //});
         
    }
    /// <summary>
    /// 公司为0,店铺为1，会员为2
    /// </summary>
    public int LoginIDEntity
    {
        get { return ViewState["IDEntity"] == null ? 0 : (int)ViewState["IDEntity"]; }
        set { ViewState["IDEntity"] = value; }
    }
    private void GetUrlParams()
    {
        if (Request.QueryString["number"] != null)
        {
            MemberID = Request.QueryString["number"].ToString();
        }
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
    /// 绑定国家集合
    /// </summary>
    public void BindCountryList()
    {
        IList<CountryModel> countryModels = returnedGoodsBLL.GetCountryIdAndName();
        for (int i = 0; i < countryModels.Count; i++)
        {
            ListItem listItem = new ListItem(countryModels[i].Name.ToString(), countryModels[i].ID.ToString());
            this.DropCurrency.Items.Add(listItem);
            BindDorpDownList();
        }
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

    /// <summary>
    /// 选择条件改变时激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DropDownList_Items_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDorpDownList();
    }


    /// <summary>
    /// 绑定级联选择条件
    /// </summary>
    private void BindDorpDownList()
    {
        //if (DropDownList_Items.SelectedIndex <= 1)
        //{
        //    DatePicker1.Visible = false;
        //    txtCondition.Visible = true;
        //    DropDownList_condition.Items.Clear();
        //    DropDownList_condition.Items.Add(new ListItem(GetTran("000378", "包括"), " like "));
        //}
        //else
        if (DropDownList_Items.SelectedIndex <= 1)
        {
            DatePicker1.Visible = false;
            txtCondition.Visible = true;
            DropDownList_condition.Items.Clear();
            DropDownList_condition.Items.Add(new ListItem(GetTran("000361", "大于"), ">"));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000364", "大于等于"), ">="));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000367", "小于"), "<"));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000368", "小于等于"), "<="));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000372", "等于"), "="));
        }
        if (DropDownList_Items.SelectedIndex >= 2)
        {
            DatePicker1.Visible = true;
            txtCondition.Visible = false;
            DropDownList_condition.Items.Clear();
            DropDownList_condition.Items.Add(new ListItem(GetTran("000361", "大于"), ">"));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000364", "大于等于"), ">="));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000367", "小于"), "<"));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000368", "小于等于"), "<="));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000372", "等于"), " = "));
        }
    }

    /// <summary>
    /// 点击“查询”按钮，显示查询结果
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        if (DDPStatus.SelectedIndex != 0)
        {
            gvRefundmentBrowse.Columns[0].Visible = false;
            gvRefundmentBrowse.Columns[1].Visible = false;
        }
        else
        {
            gvRefundmentBrowse.Columns[0].Visible = true;
            gvRefundmentBrowse.Columns[1].Visible = true;
        }
        BidnGridViewList(GetSQL());
        Pager1.PageBind(0, 10, "InventoryDoc As D", "*", GetSQL(), "docid", "gvRefundmentBrowse");

    }
    /// <summary>
    /// 绑定从数据库中查询出的退货单信息
    /// </summary>
    private void BidnGridViewList(string sql)
    {
        int PageCount;
        int RecordCount;
        DataTable dt = returnedGoodsBLL.GetDataTablePage(Convert.ToInt32(ViewState["PageIndex"]), Convert.ToInt32(ViewState["PageSize"]), ViewState["table"].ToString(), ViewState["cloumns"].ToString(), GetSQL(), ViewState["key"].ToString(), out RecordCount, out PageCount);
        ViewState["PageCount"] = PageCount;
        if (dt.Rows.Count == 0)
        {
            //this.gvRefundmentBrowse.Visible = false;
        }
        else
        {
            gvRefundmentBrowse.DataSource = dt;
            gvRefundmentBrowse.DataBind();
            this.gvRefundmentBrowse.Visible = true;
        }
    }

    /// <summary>
    /// 设置SQL语句，存储于ViewState里
    /// </summary>
    private string GetSQL()
    {
        string table = "InventoryDoc As D";
        string condition = " doctypeid = 41 ";

        //根据选择不同的条件来设置不同的查询条件

        if (this.DropDownList_Items.SelectedIndex == 0)
        {//期数
            DatePicker1.Visible = false;
            txtCondition.Visible = true;
            try
            {
                int qishu = -1;
                if (!string.IsNullOrEmpty(this.txtCondition.Text.Trim().ToString()))
                {
                    qishu = int.Parse(this.txtCondition.Text);
                    if (qishu > 0)
                    {
                        condition += "  and D." + this.DropDownList_Items.SelectedValue + "=" + this.txtCondition.Text.Trim().ToString();
                    }
                }
            }
            catch { }
        }
        else if (this.DropDownList_Items.SelectedIndex == 1)
        {//总价格
            DatePicker1.Visible = false;
            this.txtCondition.Visible = true;
            if (!string.IsNullOrEmpty(this.txtCondition.Text.Trim().ToString()))
            {
                try
                {
                    Convert.ToDouble(this.txtCondition.Text.Trim().ToString());
                    if (this.txtCondition.Text.Trim().ToString().Length > 8)
                    {
                        Response.Write(ReturnAlert(GetTran("001837", "输入金额太大！")));
                        Response.End();
                    }
                }
                catch (Exception)
                {
                    Response.Write(ReturnAlert(GetTran("001837", "输入价格格式错误")));
                    Response.End();
                }
                condition += "  and D." + this.DropDownList_Items.SelectedValue + " " + this.DropDownList_condition.SelectedValue + " " + Convert.ToDouble(this.txtCondition.Text.Trim().ToString());
            }
        }
        if (this.DropDownList_Items.SelectedIndex >= 2)
        {//日期
            if (!string.IsNullOrEmpty(this.txtCondition.Text.Trim().ToString()))
            {
                DatePicker1.Visible = true;
                txtCondition.Visible = false;
                try
                {
                    Convert.ToDateTime(this.DatePicker1.Text.Trim().ToString());
                }
                catch (Exception)
                {
                    Response.Write(ReturnAlert(GetTran("001839", "输入日期格式错误")));
                    Response.End();
                }
                condition += "  and  DATEADD(day, DATEDIFF(day,0,D." + DropDownList_Items.SelectedValue + "), 0)" + DropDownList_condition.SelectedValue + " ' " + DatePicker1.Text + "'";
            }
        }
        if (!string.IsNullOrEmpty(DDPStatus.SelectedValue))
        {
            condition += " and " + this.DDPStatus.SelectedValue;
        }
        ViewState["SQL"] = "select * from " + table + " where " + condition + " order by docID";
        string key = "D.docID";
        string cloumns = "*";

        int PageSize = 10;
        ViewState["PageIndex"] = 0;
        ViewState["key"] = key;
        ViewState["PageSize"] = PageSize;
        ViewState["table"] = table;
        ViewState["cloumns"] = cloumns;
        ViewState["condition"] = condition;

        return condition;
    }

    /// <summary>
    /// 返回能够弹出提示框的 javascript 代码
    /// </summary>
    /// <param name="content">要提示的内容</param>
    /// <returns>javascript 代码</returns>
    public string ReturnAlert(string content)
    {
        string retVal;
        retVal = "<script language='javascript'>alert('" + content.Replace("'", " ").Replace("\r", " ").Replace("\n", "").Replace("\t", " ") + "');</script>";
        return retVal;
    }


    /// <summary>
    /// 点击查看退货单详细时跳转到指定的页面
    /// </summary>
    /// <param name="storeOrderId"></param>
    /// <param name="pageUrl"></param>
    /// <returns></returns>
    protected string GetDetailURL(string storeOrderId, string pageUrl)
    {
        return "<a href=\"" + pageUrl + "?ID=" + storeOrderId + "\">" + GetTran("000399", "查看详细") + "</a>";
    }
   

    /// <summary>
    /// 根据仓库ID查询出仓库名称
    /// </summary>
    /// <param name="strwarehouseId">仓库ID</param>
    /// <returns></returns>
    protected string GetWareHouseNameByID(string strwarehouseId)
    {
        return returnedGoodsBLL.GetWareHouseNameByID(strwarehouseId);
    }

    protected void gvRefundmentBrowse_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((Button)e.Row.Cells[0].FindControl("btnAuditing")).Attributes.Add("onclick", "javascript:return confirm('" + GetTran("001843", "你确认要审核吗?") + "')");
                ((Button)e.Row.Cells[0].FindControl("btnnouse")).Attributes.Add("onclick", "javascript:return confirm('" + GetTran("001844", "你确认要取消审核吗?") + "')");
            }
        }
    }

    /// <summary>
    /// 添加退货款
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("RefundmentOrderForMember.aspx");
    }
    protected void gvRefundmentBrowse_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)((LinkButton)(e.CommandSource)).Parent.Parent;
        
        #region 原来的退货审核

        if (e.CommandName == "audit")
        {
            string docID = e.CommandArgument.ToString();
            string key = ActionConst.UrlParmsCMD;
            string keyValue = CMDConst.Key_audit;
            string url = "RefundmentOrderForMemberDetails.aspx?"+ActionConst .Url_ID +"="+docID+"&" + key + "=" + keyValue;
            Response.Redirect(url);
        }
        else if (e.CommandName == "details")
        {
            string docID = e.CommandArgument.ToString();
            string key = ActionConst.UrlParmsCMD;
            string keyValue = CMDConst.Key_details;
            string url = "RefundmentOrderForMemberDetails.aspx?" + ActionConst.Url_ID + "=" + docID + "&" + key + "=" + keyValue;
            Response.Redirect(url);
        }
        else if (e.CommandName == "lock")
        {
            string docID = e.CommandArgument.ToString();

            RefundmentOrderDocBLL refundmentOrderDocService = new RefundmentOrderDocBLL();
            bool flag= refundmentOrderDocService.LuckRefundmentOrderDoc(docID);
            if (flag)
            {
                btn_Submit_Click(null, null);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001541", "操作失败！") + "');</script>", false);
            }

        }
        else if (e.CommandName == "unlock")
        {
            string docID = e.CommandArgument.ToString();
            RefundmentOrderDocBLL refundmentOrderDocService = new RefundmentOrderDocBLL();
            bool flag = refundmentOrderDocService.UnluckRefundmentOrderDoc(docID);
            if (flag)
            {
                btn_Submit_Click(null, null);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001541", "操作失败！") + "');</script>", false);
            }

        }
        else if (e.CommandName == "del")
        {
            string docID = e.CommandArgument.ToString();

            RefundmentOrderDocBLL refundmentOrderDocService = new RefundmentOrderDocBLL();
            bool flag = refundmentOrderDocService.DelRefundmentOrderDoc(docID);
            if (flag)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000749", "删除成功！") + "');</script>", false);
                btn_Submit_Click(null, null);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000417", "删除失败！") + "');</script>", false);
            }
 
        }
        else if (e.CommandName == "print")
        {
            string docID = e.CommandArgument.ToString();

            string url = "../DocPrint.aspx?DocID=" + docID;
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>window.open('" + url + "');</script>", false);
            
        }

        #endregion

    }
    protected void bunExportExcel_Click(object sender, EventArgs e)
    {
        gvRefundmentBrowse.Columns[0].Visible = false;
        gvRefundmentBrowse.Columns[1].Visible = false;
        gvRefundmentBrowse.Columns[2].Visible = false;
        gvRefundmentBrowse.Columns[3].Visible = false;
        gvRefundmentBrowse.AllowPaging = false;
        gvRefundmentBrowse.AllowSorting = false;

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "GB2312";
        Response.AppendHeader("Content-Disposition", "attachment;filename=RefundmentBrowse.xls");
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文
        Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
        this.EnableViewState = false;
        System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

        gvRefundmentBrowse.RenderControl(oHtmlTextWriter);

        Response.Write(oStringWriter.ToString());
        Response.End();

        gvRefundmentBrowse.AllowPaging = true;
        gvRefundmentBrowse.AllowSorting = true;
    }

    //必须得写此个方法，要不然会引发runat=server 的窗体标记内... 异常。
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }

    protected void gvRefundmentBrowse_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");

            Label lbl_StatusFlag_NR = (Label)e.Row.FindControl("lbl_StatusFlag_NR");
            LinkButton lbtn_Audit = (LinkButton)e.Row.FindControl("lbtn_Audit");
            LinkButton lbtn_lock = (LinkButton)e.Row.FindControl("lbtn_lock");
            LinkButton lbtn_unlock = (LinkButton)e.Row.FindControl("lbtn_unlock");
            LinkButton lbl_Del = (LinkButton)e.Row.FindControl("lbl_Del");
            Label lbl_IsLock = (Label)e.Row.FindControl("lbl_IsLock");
            int StatusFlag_NR = int.Parse(lbl_StatusFlag_NR.Text);
            int IsLock = 0; //int.Parse(lbl_IsLock.Text);
            if (LoginIDEntity == 0)
            {
                if (lbl_StatusFlag_NR != null)
                {
                    switch (StatusFlag_NR)
                    {
                        case (int)RefundmentOrderStatusEnum.UnAudit:
                            if (IsLock == 0)
                            {
                                lbtn_Audit.Visible = true;
                                lbl_Del.Visible = true;
                                //lbtn_lock.Visible = true;
                                //lbtn_unlock.Visible = false;
                            }
                            else
                            {
                                lbtn_Audit.Visible = false;
                                lbl_Del.Visible = false;
                                //lbtn_unlock.Visible = true;
                                //lbtn_lock.Visible = false; 
                            }
                            break;
                        case (int)RefundmentOrderStatusEnum.AuditedUnPay:
                            //lbtn_unlock.Visible = false;
                            //lbtn_lock.Visible = false;
                            lbtn_Audit.Visible = false;
                            lbl_Del.Visible = false;
                            break;
                        case (int)RefundmentOrderStatusEnum.Audited:
                            //lbtn_unlock.Visible = false;
                            //lbtn_lock.Visible = false;
                            lbtn_Audit.Visible = false;
                            lbl_Del.Visible = false;
                            break;
                    }
                    if (IsLock == 0)
                    {
                        if (StatusFlag_NR == (int)RefundmentOrderStatusEnum.UnAudit)
                        {
                            //lbtn_lock.Visible = true;
                        }
                        else
                        {
                            lbtn_lock.Visible = false;
                            lbtn_unlock.Visible = false;
                        }
                    }
                    else
                    {
                        lbtn_Audit.Visible = false;
                        lbl_Del.Visible = false;
                        lbtn_unlock.Visible = true;
                        lbtn_lock.Visible = false;
                    }
                }
            }
            else
            {
                lbtn_Audit.Visible = false;
                lbtn_lock.Visible = false;
                lbtn_unlock.Visible = false;
                switch (StatusFlag_NR)
                {
                    case (int)RefundmentOrderStatusEnum.UnAudit:
                        if (IsLock == 0)
                        {
                            lbl_Del.Visible = true;
                        }
                        else
                        {
                            lbl_Del.Visible = false;
                        }
                        break;
                    case (int)RefundmentOrderStatusEnum.AuditedUnPay:
                        lbl_Del.Visible = false;
                        break;
                    case (int)RefundmentOrderStatusEnum.Audited:
                        lbl_Del.Visible = false;
                        break;
                }
            }
           // (e.Row.FindControl("ltAuditing") as Literal).Text = "<a href='#' onclick='ShowTbInfo(\"" + docId + "\",'',\"" + totalMoney + "\",\"" + totalPv + "\",\"" + date + "\")' >" + GetTran("000761", "审核") + "</a>";

        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('../images/tabledp.gif')");
            Translations();
        }
    }

}
