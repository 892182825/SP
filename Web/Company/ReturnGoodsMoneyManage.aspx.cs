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

using BLL.other.Company;
using BLL.MoneyFlows;
using Model;
using Model.Other;
using BLL.CommonClass;
using System.Collections.Generic;
using System.Text;

public partial class Company_ReturnGoodsMoneyManage : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceTuihuokuanManage);
        Response.Cache.SetExpires(DateTime.Now);
        ViewState["thzj"] = 0;
        ViewState["kk"] = 0;
        ViewState["thjf"] = 0;
        if (!IsPostBack)
        {
            butsele_Click(null, null);
            listCondition();
            ViewState["FID"] = null;
            ViewState["Fdocid"] = null;
            getdata(null, null);
            getTotal();
            //InventoryDocModel Inventory = new InventoryDocModel();
            //pagebind(Inventory);
            //object obj=null;
            
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.gwmoney,
               new string[][]{
                    new string []{"001802","审核退货单"},
                    new string []{"000015","操作"},
                    new string []{"001806","退货单详细"},
                    new string []{"001808", "退货店铺"},
                    new string []{"001809","退货单号"},
                    new string []{"000045", "期数"},
                    new string []{"000605","是否审核"},
                    new string []{"001811","是否失效"},
                    new string []{"001988","是否退款"},
                    new string []{"001812", "退货总价 "},
                    new string []{"000251", "扣款"},
                    new string []{"001813", "退货总积分"},
                    new string []{"001814", "退货日期"},
                    new string []{"000744", "查看备注"}});
        this.TranControls(this.ddlItem,
                    new string[][]{
                    new string []{"001808","退货店铺"},
                    new string []{"000045","期数"},
                    new string []{"001820","总价格"},
                    new string []{"001814", "退货日期"}});
        this.TranControls(this.ddlstatu,
                    new string[][]{
                    new string []{"005771","未退款"},
                    new string []{"005772", "已退款"}});
        this.TranControls(this.butInventoryDoc, new string[][] { new string[] { "000434", "确定" } });
        this.TranControls(this.btnret, new string[][] { new string[] { "000421", "返回" } });
        TranControls(butsele, new string[][] { new string[] { "000340", "查询" } });
    }
    /// <summary>
    /// 首次加载 无条件查询
    /// </summary>
    public void getdata(object obj, EventArgs e)
    {
        string table = "InventoryDoc";
        string columns = "ID,Client,DocID,ExpectNum,StateFlag,CloseFlag,Flag,TotalMoney,Charged,TotalPV,DocMakeTime,Note ";
        string sql = " 1=1  and doctypeid=" + QueryInStorageBLL.GetDocTypeIDEByDocTypeCode("TH") + " and Flag=" + this.ddlstatu.SelectedValue;
        string shi = GetTran("000233", "是");
        string fou = GetTran("000235", "否");
        string temp = "select Client,DocID,ExpectNum,StateFlag=case StateFlag when '0' then '" + fou + "' when '1' then '" + shi + "' end ,CloseFlag=case CloseFlag when '0' then '" + fou + "' when '1' then '" + shi + "' end,Flag=case Flag when '0' then '" + fou + "' when '1' then '" + shi + "' end , TotalMoney,Charged,TotalPV,DocMakeTime,Note  from InventoryDoc where ";
        ViewState["sql"] = temp + sql;
        this.Pager1.ControlName = "gwmoney";
        this.Pager1.key = "id";
        this.Pager1.PageColumn = columns;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = table;
        this.Pager1.Condition = sql;
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();
        if (Convert.ToInt32(this.ddlstatu.SelectedValue) == 0)
        {
            gwmoney.Columns[1].Visible = false;
            gwmoney.Columns[0].Visible = true;
        }
        else
        {
            gwmoney.Columns[1].Visible = true;
            gwmoney.Columns[0].Visible = false;
        }
        Translations();
    }



    /// <summary>
    /// 判断是否显示“填写退货单”
    /// </summary>
    /// <param name="State"></param>
    /// <param name="Close"></param> 
    /// <returns></returns>
    public Boolean getValidate(object State, object Close)
    {
        int AudCount = 0;
        AudCount = Permissions.GetPermissions(EnumCompanyPermission.FinanceTuihuokuanManage);
        if (int.Parse(State.ToString()) == 1 && int.Parse(Close.ToString()) == 0)
        {
            if (AudCount.ToString() == "4111")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 是否审核 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public string getstr(object obj)
    {
        return int.Parse(obj.ToString().Trim()) == 1 ? GetTran("000233", "是") : GetTran("000235", "否");
    }

    /// <summary>
    /// 是否有备注及连接到另一个页面查看备注
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public string setstr(object str, object DocID)
    {
        return str.ToString().Trim().Length > 0 ? "<a href =\"javascript:void(window.open('ReturnGoodsMoneyManageNote.aspx?id=" + DocID.ToString().Trim() + "','','left=300,top=150,width=250,height=150'))\">" + GetTran("000440", "查看") + "</a>" : GetTran("000221", "无");
    }

    /// <summary>
    /// 查找
    /// </summary>
    /// <param name="sender"></param> 
    /// <param name="e"></param>
    protected void butsele_Click(object sender, EventArgs e)
    {
        if (this.ddlItem.SelectedItem.Value == "Client" && this.txtvalue.Text.Trim().Length == 0)
        {
            getdata(null,null);
        }
        else if (this.txtvalue.Text.Trim().Length > 0)
        {
            InventoryDocModel inventory = new InventoryDocModel();
            inventory.DocTypeID = QueryInStorageBLL.GetDocTypeIDEByDocTypeCode("TH");
            int expectnum = -1;
            double totalmoney = -0;
            DateTime time = DateTime.Now;
            inventory.TotalMoney = -1;
            inventory.ExpectNum = -1;
            inventory.DocMakeTime = DateTime.Parse("0001-1-1 0:00:00");
            switch (this.ddlItem.SelectedValue.Trim())
            {
                case "Client": if (this.txtvalue.Text.Trim().Length > 0)
                    {
                        inventory.Client = DisposeString.DisString(this.txtvalue.Text.Trim(), "<,>,',-", "&lt;,&gt;,&#39;,&nbsp;", ",");
                    } //退货店铺
                    else { ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001063", "条件错误") + "');</script>"); return; }; break;
                case "ExpectNum": if (this.txtvalue.Text.Trim().Length > 0)
                    {
                        if (int.TryParse(this.txtvalue.Text.Trim(), out expectnum))//期数
                        {
                            inventory.ExpectNum = expectnum;
                        }
                        else { inventory.ExpectNum = CommonDataBLL.GetMaxqishu(); }
                    }
                    else { ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001063", "条件错误") + "');</script>"); return; }; break;
                case "TotalMoney": if (this.txtvalue.Text.Trim().Length > 0)//总价格
                    {
                        if (double.TryParse(this.txtvalue.Text.Trim(), out totalmoney))
                        {
                            inventory.TotalMoney = totalmoney;
                        }
                        else { ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001063", "条件错误") + "');</script>"); return; }
                    }
                    else { ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001063", "条件错误") + "');</script>"); return; }; break;
                case "DocMakeTime": if (this.txttime.Text.Trim().Length > 0)
                    {
                        if (DateTime.TryParse(this.txttime.Text.Trim(), out time)) { inventory.DocMakeTime = time.AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()); }
                    }
                    else { ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001063", "条件错误") + "');</script>"); return; }; break;//退货日期
            }
            inventory.Flag = int.Parse(this.ddlstatu.SelectedValue);
            pagebind(inventory);
        }
        else
        {
            getdata(null, null);
        }
        Translations();
        ViewState["FID"] = null;
        ViewState["Fdocid"] = null;
    }

    //获取格林时间
    public string Getdate(object Vdate)
    {
        if (string.IsNullOrEmpty(Vdate.ToString()))
        {
            return "";
        }
        DateTime Dtime = Convert.ToDateTime(Vdate);
        return Dtime.AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }


    /// <summary>
    /// 确定按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void butInventoryDoc_Click(object sender, EventArgs e)
    {
        if (!ReturnedGoodsMoneyBLL.GetInventoryState(labcard.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001541", "操作失败!") + "')</script>");
            return;
        }

        string valida = string.Empty;
        double money = 0;
        string note = this.txtnote.Text.Trim();
        Boolean bol = false;
        bool b = this.ValidateControl(out valida, out money, out note);
        if (b == false)
        {
            return;
        }
        if(ViewState["FID"]!=null &&ViewState["Fdocid"]!=null)
        {
            //是否是添加
            bol = true;
        }
        if (ReturnedGoodsMoneyBLL.UPtInventoryDoc(labcard.Text, labstore.Text, 1, money, note, bol))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001401", "操作成功！") + "')</script>");
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001541", "操作失败!") + "')</script>");
        }
        this.plflag.Visible = false;
        this.panelsel.Visible = true;
        this.panelselT.Visible = true;
        butsele_Click(null, null);

    }
    /// <summary>
    /// 查询条件类型(级联)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        listCondition();

    }
    /// <summary>
    /// 文本框级联
    /// </summary>
    private void listCondition()
    {
        //退货店铺 
        if (ddlItem.SelectedIndex <= 1)
        {
            txttime.Visible = false;
            txtvalue.Visible = true;
            ddllist.Items.Clear();
            ddllist.Items.Add(new ListItem(GetTran("000378", "包括"), "like"));
        }
        //期数
        if (ddlItem.SelectedIndex >= 1)
        {
            //总价格
            if (ddlItem.SelectedIndex == 2)
            {
                txttime.Visible = false;
                txtvalue.Visible = true;
            }
            else
            {   //退货时间
                if (ddlItem.SelectedIndex == 3)
                {
                    txtvalue.Visible = false;
                    txttime.Visible = true;
                }
            }

            ddllist.Items.Clear();
            ddllist.Items.Add(new ListItem(GetTran("000361", "大于"), ">"));
            ddllist.Items.Add(new ListItem(GetTran("000364", "大于等于"), ">="));
            ddllist.Items.Add(new ListItem(GetTran("000367", "小于"), "<"));
            ddllist.Items.Add(new ListItem(GetTran("000368", "小于等于"), "<="));
            ddllist.Items.Add(new ListItem(GetTran("000372", "等于"), "="));
        }
    }
    /// <summary>
    /// 验证控件
    /// </summary>
    private bool ValidateControl(out string valida, out double money, out string note)
    {
        valida = "";
        money = 0;
        note = "";
        if (string.IsNullOrEmpty(this.txtvali.Text.Trim()))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("005769", "请输入验证人") + "')</script>");
            this.txtvali.Focus();
            return false;
        }
        if (!double.TryParse(this.txtmoney.Text.Trim(), out money))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("005770", "请认真填写扣款金额") + "')</script>");
            this.txtmoney.Focus();
            return false;
        }
        if (money > double.Parse(this.labmoney.Text) || money < 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("005770", "请认真填写扣款金额") + "')</script>");
            this.txtmoney.Focus();
            return false;
        }
        if (this.txtnote.Text.Trim() != null)
        {
            note = DisposeString.DisString(this.txtnote.Text.Trim(), "<,>,',-", "&lt;,&gt;,&#39;,&nbsp;", ",");
        }
        valida = DisposeString.DisString(this.txtvali.Text.Trim(), "<,>,',-", "&lt;,&gt;,&#39;,&nbsp;", ",");

        return true;
    }

    /// <summary>
    /// 填写退货退款单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gwmoney_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {
            //隐藏列表
            this.panelsel.Visible = false;
            //隐藏查询条件
            this.panelselT.Visible = false;
            //退货扣款单
            this.plflag.Visible = true;
            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string gvStoreNumber = gwmoney.Rows[gvrow.RowIndex].Cells[3].Text.Trim();
            string gvDocID = gwmoney.Rows[gvrow.RowIndex].Cells[4].Text.Trim();
            string gvMoney = (gwmoney.Rows[gvrow.RowIndex].FindControl("labmon") as Label).Text.Trim();
            ViewState["FID"] = null;
            ViewState["Fdocid"] = null;
            this.labstore.Text = gvStoreNumber;
            this.labcard.Text = gvDocID;
            this.labmoney.Text = gvMoney;
            this.TranControls(this.butInventoryDoc, new string[][] { new string[] { "000434", "确定" } });
            clear();
        }
        if (e.CommandName == "upmoney")
        {
            this.panelsel.Visible = false;
            this.panelselT.Visible = false;
            this.plflag.Visible = true;
            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int ID = int.Parse(((gwmoney.Rows[gvrow.RowIndex].FindControl("hdfddocid")) as HiddenField).Value.Trim());
            string Fdocid = ((gwmoney.Rows[gvrow.RowIndex].FindControl("hdfEddoc")) as HiddenField).Value.Trim();
            ViewState["FID"] = ID;
            ViewState["Fdocid"] = Fdocid;
            InventoryDocModel mondel = ReturnedGoodsMoneyBLL.GetInventory(ID, Fdocid);
            this.labstore.Text = mondel.Client.ToString();
            this.labcard.Text = mondel.DocID.ToString();
            this.labmoney.Text = mondel.TotalMoney.ToString("f2");
            this.txtmoney.Text = mondel.Charged.ToString("f2");
            this.txtnote.Text = mondel.Reason.ToString();
            this.TranControls(this.butInventoryDoc, new string[][] { new string[] { "000259", "修改" } });
        }

    }
    /// <summary>
    /// 清空控件值
    /// </summary>
    public void clear()
    {
        this.txtvali.Text = string.Empty;
        this.txtmoney.Text = string.Empty;
        this.txtnote.Text = string.Empty;
    }


    /// <summary>
    /// 页面控件数据绑定
    /// </summary>
    public void pagebind(InventoryDocModel Inventory)
    {
        string mark = this.ddllist.SelectedValue;
        string table = "InventoryDoc";
        string columns = " ID,Client,DocID,ExpectNum,StateFlag,CloseFlag,Flag,TotalMoney,Charged,TotalPV,DocMakeTime,Note ";
        string sql = "  1=1 ";
        //单据类型
        if (Inventory.DocTypeID >= 0)
        {
            sql = sql + " and doctypeid=" + Inventory.DocTypeID;
        }
        ////国家
        //if (Inventory.Currency >= 0)
        //{
        //    sql = sql + " and Currency=" + Inventory.Currency;
        //}
        //类型
        if (Inventory.Flag >= 0)
        {
            sql = sql + " and Flag=" + Inventory.Flag;
        }
        //店编号
        if (Inventory.Client != null)
        {
            sql = sql + " and Client like '%" + Inventory.Client + "'";
        }
        //期数
        if (Inventory.ExpectNum >= 0 && mark != null)
        {
            sql = sql + " and ExpectNum" + mark + Inventory.ExpectNum;
        }
        //总钱
        if (Inventory.TotalMoney >= 0 && mark != null)
        {
            sql = sql + " and TotalMoney" + mark + Inventory.TotalMoney;
        }
        //时间 
        if (Inventory.DocMakeTime != DateTime.Parse("0001-1-1 0:00:00") && mark != null)
        {

            if (mark == "=")
            {
                string endtiem = Inventory.DocMakeTime.ToString("yyyy-MM-dd") + " 23:59:59";
                sql = sql + " and DocMakeTime between '" + Inventory.DocMakeTime + "' and '" + endtiem + "'";
            }
            else
            {
                sql = sql + " and DocMakeTime" + mark + "'" + Inventory.DocMakeTime + "'";
            }
        }

        if (Inventory.Flag == 1)
        {
            this.gwmoney.Columns[0].Visible = false;
            this.gwmoney.Columns[1].Visible = true;
        }
        else
        {
            this.gwmoney.Columns[0].Visible = true;
            this.gwmoney.Columns[1].Visible = false;
        }
        string temp = "select Client,DocID,ExpectNum,StateFlag=case StateFlag when '0' then '" +GetTran("000235", "否")+ "' when '1' then '" +GetTran("000233", "是")+ "' end ,CloseFlag=case CloseFlag when '0' then '" +GetTran("000235", "否")+ "' when '1' then '"+ GetTran("000233", "是")+ "' end ,Flag=case Flag when '0' then '" + GetTran("000235", "否") + "' when '1' then '" + GetTran("000233", "是") + "' end , TotalMoney,Charged,TotalPV,DocMakeTime,Note  from InventoryDoc where ";
        ViewState["sql"] = temp + sql;
        this.Pager1.ControlName = "gwmoney";
        this.Pager1.key = "id";
        this.Pager1.PageColumn = columns;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = table;
        this.Pager1.Condition = sql;
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();
        if (Convert.ToInt32(this.ddlstatu.SelectedValue) == 0)
        {
            gwmoney.Columns[1].Visible = false;
            gwmoney.Columns[0].Visible = true;
        }
        else
        {
            gwmoney.Columns[1].Visible = true;
            gwmoney.Columns[0].Visible = false;
        }
    }

    /// <summary>
    /// 导出EXCEL
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEXCEL_Click(object sender, EventArgs e)
    {
        string cmd = ViewState["sql"].ToString();
        DataTable dt = DAL.DBHelper.ExecuteDataTable(cmd);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["DocMakeTime"] = string.IsNullOrEmpty(dt.Rows[i]["DocMakeTime"].ToString()) ? dt.Rows[i]["DocMakeTime"] : Convert.ToDateTime(dt.Rows[i]["DocMakeTime"].ToString()).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToUniversalTime().ToString();
        }

        Excel.OutToExcel(dt, GetTran("001917", "退货款"), new string[] { "Client="+GetTran("001808","退货店铺"), "DocID="+GetTran("001809","退货单号"), 
            "ExpectNum="+GetTran("000045","期数"),"StateFlag="+GetTran("000605","是否审核"), "CloseFlag="+GetTran("001811","是否失效"),
            "Flag="+GetTran("001988","是否退款"), "TotalMoney="+GetTran("001812","退货总价"), "Charged="+GetTran("000251","扣款"),
            "TotalPV="+GetTran("001813","退货总积分"), "DocMakeTime="+GetTran("001814","退货日期"), "Note="+GetTran("000744","查看备注") });
    }

    protected void gwmoney_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            Label labmon = e.Row.FindControl("labmon") as Label;
            ViewState["thzj"] = Convert.ToDouble(ViewState["thzj"].ToString()) + Convert.ToDouble(labmon.Text);
            ViewState["kk"] = Convert.ToDouble(ViewState["kk"]) + Convert.ToDouble(e.Row.Cells[10].Text);
            ViewState["thjf"] = Convert.ToDouble(ViewState["thjf"]) + Convert.ToDouble(e.Row.Cells[11].Text);
        }
        else if(e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            e.Row.Cells[0].Text = GetTran("007160", "本页总计");
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[9].Text = Convert.ToDouble(ViewState["thzj"]).ToString("f2");
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[10].Text = Convert.ToDouble(ViewState["kk"]).ToString("f2");
            e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[11].Text = Convert.ToDouble(ViewState["thjf"]).ToString("f2");
            e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
        }
        Translations();
    }
    /// <summary>
    /// 返回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnret_Click(object sender, EventArgs e)
    {
        this.panelsel.Visible = true;
        this.panelselT.Visible = true;
        this.plflag.Visible = false;
        clear();
        butsele_Click(null, null);
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        butsele_Click(null,null);
        getTotal();
    }

    public void getTotal()
    {
        Label1.Text = GetTran("007755", "退货总价总计") + "：<font color='red'>" + RemittancesBLL.GetTotalMoney("TotalMoney", "InventoryDoc", "") + "</font>";
        Label2.Text = GetTran("007756", "扣款总计") + "：<font color='red'>" + RemittancesBLL.GetTotalMoney("Charged", "InventoryDoc", "") + "</font>";
        Label3.Text = GetTran("007757", "退货积分总计") + "：<font color='red'>" + RemittancesBLL.GetTotalMoney("TotalPV", "InventoryDoc", "") + "</font>";
    }
}
