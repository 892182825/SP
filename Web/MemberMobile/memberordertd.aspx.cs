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
using BLL.Registration_declarations;
using System.Text;
using Standard.Classes;
using DAL;

public partial class Member_memberordertd : BLL.TranslationBase
{
    CommonDataBLL commonDataBLL = new CommonDataBLL();
    MemberOrderBLL memberOrderBLL = new MemberOrderBLL();
    protected string msg = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        //Permissions.CheckMemberPermission();

        if (!this.IsPostBack)
        {
            CommonDataBLL.BindQishuList(this.DropDownQiShu, false);
            GetShopList();
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.gvorder,
            new string[][] { 
                new string[] { "000811", "明细" } ,
                new string[] { "000045", "期数" } ,
                new string[] { "000775", "支付状态" } ,
                new string[] { "000079", "订单号" } ,
                new string[] { "000024", "会员编号" } ,
                new string[] { "000025", "会员姓名" } ,
                new string[] { "000322", "金额" } ,
                new string[] { "000414", "积分" } ,
                new string[] { "001429", "报单日期" } ,
                new string[] { "000793", "确认店铺" } ,
                new string[] { "000774", "报单途径" } ,
                new string[] { "006049", "店铺确认" } ,
                new string[] { "006048", "公司确认" } 

            });
    }

    private void GetShopList()
    {
        int startXH = 0;
        int endXH = 0;
        int baseCengwei = 0;
        string myXuhao;
        myXuhao = "Ordinal2";
        int QiShu = Convert.ToInt32(DropDownQiShu.SelectedValue);
        memberOrderBLL.getXHFW(Session["Member"].ToString(), false, QiShu, out startXH, out endXH, out baseCengwei);
        //memberOrderBLL.getXHFW("66666666666", false, QiShu, out startXH, out endXH, out baseCengwei);
        string table = "MemberInfoBalance" + QiShu.ToString() + " as J,memberorder as I,memberinfo c";
        string condition = "J.number=I.number and J.number=c.number and j.number='"+Session["Member"].ToString()+"'";//I.OrderExpectNum=" + QiShu.ToString() + " and J." + myXuhao + ">=" + startXH.ToString() + " and J." + myXuhao + "<=" + endXH.ToString();

        string key = "I.id";
        string cloumns = "I.OrderExpectNum,I.OrderID,I.Number,I.TotalMoney,I.StoreID,I.Totalpv,I.OrderDate,c.Name,I.defraystate as zhifu, I.ordertype as fuxiaoname,case I.defraystate when 1 then 1 else case I.paymentmoney when 0 then 0 else 1 end end as dpqueren,I.defraystate as gsqueren ";
        this.Pager1.ControlName = "gvorder";
        this.Pager1.key = key;
        this.Pager1.PageColumn = cloumns;
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = table;
        this.Pager1.Condition = condition;
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();
        ViewState["colums"] = cloumns;
        ViewState["table"] = table;
        ViewState["condition"] = condition;
        
    }

    protected void DropDownQiShu_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        GetShopList();
        Translations();
    }
    protected void gvorder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
            switch (e.Row.Cells[2].Text)
            {
                case "0": e.Row.Cells[2].Text = "<font color=red>" + GetTran("000521", "未支付") + "</font>"; break;
                case "1": e.Row.Cells[2].Text = GetTran("000517", "已支付"); break;
                default: e.Row.Cells[2].Text = ""; break;
            }
            switch (e.Row.Cells[10].Text)
            {
                case "0": e.Row.Cells[10].Text = GetTran("000555", "店铺注册"); break;
                case "1": e.Row.Cells[10].Text = GetTran("001445", "店铺复消"); break;
                case "2": e.Row.Cells[10].Text = GetTran("001448", "网上购物"); break;
                case "3": e.Row.Cells[10].Text = GetTran("001449", "自由注册"); break;
                case "4": e.Row.Cells[10].Text = GetTran("001452", "特殊注册"); break;
                case "5": e.Row.Cells[10].Text = GetTran("001454", "特殊报单"); break;
                case "6": e.Row.Cells[10].Text = GetTran("001455", "手机注册"); break;
                case "7": e.Row.Cells[10].Text = GetTran("001457", "手机报单"); break;
                default: e.Row.Cells[10].Text = ""; break;
            }
            switch (e.Row.Cells[11].Text)
            {
                case "0": e.Row.Cells[11].Text = "<font color=red>" + GetTran("005634", "未收款") + "</font>"; break;
                case "1": e.Row.Cells[11].Text = GetTran("005636", "已收款"); break;
                default: e.Row.Cells[11].Text = ""; break;
            }
            switch (e.Row.Cells[12].Text)
            {
                case "0": e.Row.Cells[12].Text = "<font color=red>" + GetTran("005634", "未收款") + "</font>"; break;
                case "1": e.Row.Cells[12].Text = GetTran("005636", "已收款"); break;
                default: e.Row.Cells[12].Text = ""; break;
            }
            e.Row.Cells[5].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[5].Text);
            try
            {
                e.Row.Cells[8].Text = DateTime.Parse(e.Row.Cells[8].Text).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
            }
            catch
            {
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xls");
        Response.ContentEncoding = Encoding.UTF7;

        string cmd = "select " + ViewState["colums"].ToString() + " from " + ViewState["table"].ToString() + " where " + ViewState["condition"] + "";
        DataTable dt = DBHelper.ExecuteDataTable(cmd);
        DataTable dt1 = new DataTable();
        dt1 = dt.Clone();
        dt1.Columns[8].DataType = typeof(string);
        dt1.Columns[9].DataType = typeof(string);
        foreach (DataRow r in dt.Rows)
        {
            DataRow newrow = dt1.NewRow();
            newrow["OrderExpectNum"] = r["OrderExpectNum"];
            newrow["OrderID"] = r["OrderID"];
            newrow["Number"] = r["Number"];
            newrow["TotalMoney"] = r["TotalMoney"];
            newrow["StoreID"] = r["StoreID"];
            newrow["Totalpv"] = r["Totalpv"];
            newrow["OrderDate"] = r["OrderDate"];
            newrow["Name"] = r["Name"];
            newrow["zhifu"] = r["zhifu"];
            newrow["fuxiaoname"] = r["fuxiaoname"];
            dt1.Rows.Add(newrow);
        }

        foreach (DataRow row in dt1.Rows)
        {
            switch (row[8].ToString())
            {
                case "0": row[8] = "<font color=red>"+ GetTran("000521", "未支付") +"</font>"; break;
                case "1": row[8] = GetTran("000517", "已支付"); break;
                default: row[8] = ""; break;
            }
            switch (row[9].ToString())
            {
                case "0": row[9] = GetTran("000555", "店铺注册"); break;
                case "1": row[9] = GetTran("001445", "店铺复消"); break;
                case "2": row[9] = GetTran("001448", "网上购物"); break;
                case "3": row[9] = GetTran("001449", "自由注册"); break;
                case "4": row[9] = GetTran("001452", "特殊注册"); break;
                case "5": row[9] = GetTran("001454", "特殊报单"); break;
                case "6": row[9] = GetTran("001455", "手机注册"); break;
                case "7": row[9] = GetTran("001457", "手机报单"); break;
                default: row[9] = ""; break;
            }
            row[7] = Encryption.Encryption.GetDecipherName(row[7].ToString());
        }
        StringBuilder sb = Excel.GetExcelTable(dt1, GetTran("001750", "会员团队业绩"), new string[] { "OrderExpectNum=" + GetTran("000045", "期数"), "zhifu=" + GetTran("000775", "支付状态"), "OrderID=" + GetTran("000079", "订单号"), "Number=" + GetTran("000024", "会员编号"), "Name=" + GetTran("000025", "会员姓名"), "TotalMoney=" + GetTran("000322", "金额"), "Totalpv=" + GetTran("000414", "积分"), "orderdate=" + GetTran("001429", "报单日期"), "StoreID=" + GetTran("000793", "确认店铺"), "fuxiaoname=" + GetTran("000774", "报单途径") });

        Response.Write(sb.ToString());

        Response.Flush();
        Response.End();

    }
}
