using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using Model;
using BLL.other.Company;
using BLL.Logistics;
using System.Collections.Generic;
using BLL.CommonClass;
using Model.Other;
using BLL.Registration_declarations;

public partial class Company_DisplaceGoodsBrowse : BLL.TranslationBase 
{
    DisplaceGoodBrowseBLL bll = new DisplaceGoodBrowseBLL();
    ReturnedGoodsBLL returnedGoodsBLL = new ReturnedGoodsBLL();
    CommonDataBLL commonDataBll = new CommonDataBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //检查相应权限
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.LogisticsDisplaceGoodsBrowse);

        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            BindddlWareHouseList();
            returnedGoodsBLL.GetDepotSeat(ddlDepotSeat, ddlWareHouse.SelectedValue);
            DatePicker1.Visible = false;
            //gvDisplaceGoods.DataSource = bll.GetNoShenHe(getSQL());
            //gvDisplaceGoods.DataBind();
            Pager1.PageBind(0, 10, "Replacement As d inner join StoreInfo  on d.storeid=StoreInfo.storeid   ", " * ", getSQL(), "D.DisplaceOrderID", "gvDisplaceGoods");
        }
    }

    /// <summary>
    /// 绑定仓库信息
    /// </summary>
    protected void BindddlWareHouseList()
    {
        ddlWareHouse.DataTextField = "Name";
        ddlWareHouse.DataValueField = "ID";

        ddlWareHouse.DataSource = returnedGoodsBLL.GetProductWareHouseInfo();
        ddlWareHouse.DataBind();
    }

    protected object GetYesOrNo(object obj)
    {
        if (obj == null)
        {
            return "";
        }
        else
        {
            if (obj.ToString() == "Y")
                return GetTran("000233", "是");
            else
                return GetTran("000235", "否");
        }
    }


    private void Translations()
    {
        this.TranControls(this.gvDisplaceGoods,
                new string[][]{
                    new string []{"001856","审核换货单"},
                    new string []{"001860","编辑换货单"},
                    new string []{"001862","换货单详细"},
                    new string []{"001863","换货店铺"},
                    new string []{"001864","换货单号"},
                    new string []{"001866","对应退单号"},
                    new string []{"001867","对应订单号"},
                    new string []{"000099","对应出库单号"},
                    new string []{"000045","期数"},
                    new string []{"000605","是否审核"},
                    new string []{"001811","是否失效"},
                    new string []{"001875","退货额"},
                    new string []{"001876","进货额"},
                    new string []{"001878","换货单日期"},
                    new string []{"000744","查看备注"}
                });
        this.TranControls(this.DropDownList_Items,
                new string[][]{
                    new string []{"001863","换货店铺"},
                    new string []{"000045","期数"},
                    new string []{"001878","换货单日期"}
                   
                });
        this.TranControls(this.DropDownList_condition,
                new string[][]{
                    new string []{"000378","包括"}
                   
                });
        this.TranControls(this.DDPStatus,
                new string[][]{
                    new string []{"001009","未审核"},
                    new string []{"001011","已审核"},
                    new string []{"001069","无效"}
                   
                });

        this.TranControls(this.btn_Submit, new string[][] { new string[] { "000048", "查 询" } });
        this.TranControls(this.btnAdd, new string[][] { new string[] { "001884", "添加换货单" } });

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



    protected void DropDownList_Items_SelectedIndexChanged(object sender, EventArgs e)
    {
        listCondition();
    }
    private void listCondition()
    {
        if (DropDownList_Items.SelectedIndex <= 1)
        {
            DatePicker1.Visible = false;
            txtCondition.Visible = true;
            DropDownList_condition.Items.Clear();
            DropDownList_condition.Items.Add(new ListItem(GetTran("000378", "包括"), " like "));
        }
        if (DropDownList_Items.SelectedIndex > 1)
        {
            DatePicker1.Visible = true;
            txtCondition.Visible = false;
            DropDownList_condition.Items.Clear();
            DropDownList_condition.Items.Add(new ListItem(GetTran("000361", "大于"), ">"));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000364", "大于等于"), ">="));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000367", "小于"), "<"));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000368", "小于等于"), "<="));
            DropDownList_condition.Items.Add(new ListItem(GetTran("000372", "等于"), "="));
        }
    }


    /// <summary>
    /// 点击查看换货单详细时跳转到指定的页面
    /// </summary>
    /// <param name="storeOrderId"></param>
    /// <param name="pageUrl"></param>
    /// <returns></returns>
    protected string GetDetailURL(string storeOrderId, string pageUrl)
    {
        return "<a href=\"" + pageUrl + "?ID=" + storeOrderId + "\">"+ GetTran("000399", "查看详细") + "</a>";
    }


    #region 导出Excel



    protected void btnExecel_Click(object sender, EventArgs e)
    {
        this.gvDisplaceGoods.Columns[0].Visible = false;
        gvDisplaceGoods.Columns[1].Visible = false;
        gvDisplaceGoods.Columns[2].Visible = false;
        gvDisplaceGoods.AllowPaging = false;
        this.gvDisplaceGoods.AllowSorting = false;

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "GB2312";
        Response.AppendHeader("Content-Disposition", "attachment;filename=ReplacementTable.xls");
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文
        Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
        this.EnableViewState = false;
        System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCItrad);
        System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
        gvDisplaceGoods.RenderControl(oHtmlTextWriter);
        Response.Write(oStringWriter.ToString());
        Response.End();

        gvDisplaceGoods.AllowPaging = true;
        this.gvDisplaceGoods.AllowSorting = true;
    }

    //必须加这个方法，要不然会引发： runat=server 的窗体标记内... 异常。
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    #endregion



    private string getSQL()
    {


        string currency = Country1.CountryName;
        string table = "Replacement As D inner join StoreInfo  on D.storeid=StoreInfo.storeid and StoreInfo.StoreCountry='" + currency + "'  inner join storeorder  on D.storeorderid=storeorder.storeorderid";
        string condition = "1=1";
        if (DropDownList_Items.SelectedIndex <= 1)
        {
            DatePicker1.Visible = false;
            txtCondition.Visible = true;
            condition = condition + " and D." + DropDownList_Items.SelectedValue + " like '%" + this.txtCondition.Text + "'";
        }

        if (DropDownList_Items.SelectedIndex > 1)
        {
            DatePicker1.Visible = true;
            txtCondition.Visible = false;
            try
            {
                Convert.ToDateTime(DatePicker1.Text.Trim());
            }
            catch
            {
                Response.Write("<script>alert('" + GetTran("005823", "条件错误！") + "');</script>");
                Response.End();
            }
            //				SQL=SQL+" DATEADD(day, DATEDIFF(day,0,D."+DropDownList_Items.SelectedValue+"), 0)" + DropDownList_condition.SelectedValue +" ' "+ DatePicker1.Text +"'";
            condition = condition + "  and DATEADD(day, DATEDIFF(day,0,D." + DropDownList_Items.SelectedValue + "), 0)" + DropDownList_condition.SelectedValue + " ' " + DatePicker1.Text + "'";
        }
        //			SQL += " And " + DDPStatus .SelectedValue;
        condition = condition + " And " + DDPStatus.SelectedValue;


        //  默认为"未审核"
        ViewState["SQL"] = "select  D.StoreID,DisplaceOrderID,RefundmentOrderID,D.StoreOrderID,D.ExpectNum,StateFlag,CloseFlag,OutTotalMoney,InTotalMoney,MakeDocDate,D.Remark  from " + table + " where " + condition + " order by D.DisplaceOrderID desc";
        string key = "D.DisplaceOrderID";

        ViewState["table"] = table;
        ViewState["condition"] = condition;
        return condition;

    }


    protected void gvDisplaceGoods_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
        if (e.CommandName.ToString() == string.Empty)
        {
            return;
        }

        if (e.CommandName == "NoEffect")
        {
            int i = bll.GetStaDisplaceDocByDocId(e.CommandArgument.ToString());
            if (i > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("005827", "已被为无效单了,不需要再审核了!") + "');</script>");
                return;
            }
            bll.UpdateStateFlagAndCloseFlag(e.CommandArgument.ToString());
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("005828", "审核无效完成!") + "');location.href='DisplaceGoodsBrowse.aspx';</script>");
            //跳转打印页面
            // Page.RegisterClientScriptBlock("openwin2", "<script>if(confirm('您要打印此单据的退货单吗?'))window.open('docPrint.aspx?docID=" + e.CommandArgument.ToString().Trim() + "&docType=" + CompanyData.GetDocTypeIDByDocTypeName("退货单") + "');</script>");
        }
        else
            if (e.CommandName == "Edit")
            {
                Response.Redirect("DisplaceGoodsEdit.aspx?billID=" + e.CommandArgument.ToString());
            }
            else
                if (e.CommandName == "del")
                {
                    bll.DeleteDisplaceGoodsOrderAndOrderDetail(e.CommandArgument.ToString());
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000749", "删除成功!") + "');location.href='DisplaceGoodsBrowse.aspx';</script>");
                }

    }
    protected void gvDisplaceGoods_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //如果是绑定数据行 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                //((LinkButton)e.Row.Cells[0].FindControl("btnAuditing")).Attributes.Add("onclick", "javascript:return confirm('" + GetTran("005829", "您确认要审核换货单吗?") + "')");
                ((LinkButton)e.Row.Cells[0].FindControl("btnnouse")).Attributes.Add("onclick", "javascript:return confirm('" + GetTran("005830", "您确认此换货单不予审核吗?") + "')");
                ((LinkButton)e.Row.Cells[0].FindControl("btnDelete")).Attributes.Add("onclick", "javascript:return confirm('" + GetTran("005831", "你确认要删除吗?") + "')");
            }

            e.Row.Attributes.Add("onmouseover", "xx=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=xx;");

            string storeId = (e.Row.FindControl("hidStoreId") as HtmlInputHidden).Value;
            string outTotalMoney = (e.Row.FindControl("hidOutTotalMoney") as HiddenField).Value;
            string inTotalMoney = (e.Row.FindControl("hidInTotalMoney") as HiddenField).Value;
            string docId = (e.Row.FindControl("hidDocId") as HiddenField).Value;
            string date = (e.Row.FindControl("hidDate") as HiddenField).Value;
            (e.Row.FindControl("ltAuditing") as Literal).Text = "<a href='#' onclick='ShowTbInfo(\"" + docId + "\",\"" + storeId + "\",\"" + outTotalMoney + "\",\"" + inTotalMoney + "\",\"" + date + "\")' >" + GetTran("000761", "审核") + "</a>";

        }
       
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
            Translations();
        }

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

    protected string SetVisible(string remark)
    {
        if (string.IsNullOrEmpty(remark))
        {
            return "无";
        }
        else
        {
            return "<a href='#' onclick='showControl(event,\"divOffReason\",\"" + remark + "\")'>" + GetTran("000744", "查看备注") + "</a>";
        }
    }
    protected void btn_Submit_Click1(object sender, EventArgs e)
    {
        if (DDPStatus.SelectedIndex != 0)
        {
            gvDisplaceGoods.Columns[0].Visible = false;
            gvDisplaceGoods.Columns[1].Visible = false;
        }
        else
        {
            gvDisplaceGoods.Columns[0].Visible = true;
            gvDisplaceGoods.Columns[1].Visible = true;
        }
        Pager1.PageBind(0, 10, "Replacement As d left outer join StoreInfo  on d.storeid=StoreInfo.storeid   left outer join storeorder  on D.storeorderid=storeorder.storeorderid", " D.StoreID,DisplaceOrderID,RefundmentOrderID,D.StoreOrderID,D.ExpectNum,StateFlag,CloseFlag,OutTotalMoney,InTotalMoney,MakeDocDate,D.Remark ", getSQL(), "D.DisplaceOrderID", "gvDisplaceGoods");


    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("DisplaceGoodsAdd.aspx");
    }
}
