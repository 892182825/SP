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

//Add Namespace
using DAL;
using Model.Other;

public partial class Company_BaodanDetail : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        //设置GridView的样式        
        gvInfo.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");

        if (!this.IsPostBack)
        {
            Bind();
        }
        Translations_More();
    }

    /// <summary>
    /// Translation
    /// </summary>
    protected void Translations_More()
    {
        TranControls(gvInfo, new string[][] 
                        { 
                            //new string[] {"000012","序号"}, 
                            new string[] {"000024","会员编号"}, 
                            new string[] {"000025","会员姓名"}, 
                            new string[] {"000785","销售期数"}, 
                            new string[] {"000739","付款期数"}, 
                            new string[] {"000079","订单号"}, 
                            new string[] {"000322","金额"}, 
                            new string[] {"000414","积分"}, 
                            new string[] {"000790","销售日期"}, 
                        }
                    );
    }

    private void Bind()
    {
        string columns = @" H1.Number,H1.[name],H1.ExpectNum,O.PayExpectNum,O.Orderid,O.TotalMoney,O.TotalPV,O.orderdate,O.storeid ";
        string condition = " O.Number=H1.Number and (O.isAgain = '0' or O.isAgain = '1' or O.isAgain = '5') ";
        this.lbl_number.Text = GetTran("000633", "全部");
        condition = condition + " and orderdate between '" + Session["Begin"] + "' and '" + Session["End"] + "'";
        ViewState["SQLSTR"] = "select " + columns + " from MemberOrder as O,MemberInfo as H1 where " + condition;
        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.Pageindex = 0;
        pager.PageSize = 10;
        pager.PageTable = " MemberOrder as O,MemberInfo as H1 ";
        pager.Condition = condition;
        pager.PageColumn = columns;
        pager.ControlName = "gvInfo";
        pager.key = " H1.ID ";
        pager.InitBindData = true;
        pager.PageBind();
    }

    protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            if (Encryption.Encryption.GetEncryptionSetting("--Name--"))
            {
                e.Row.Cells[1].Text= Encryption.Encryption.GetDecipherName(Convert.ToString(e.Row.Cells[1].Text));
            }
        }

        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
    }
    protected void btnExcel_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = DBHelper.ExecuteDataTable(ViewState["SQLSTR"].ToString());
        if (dt.Rows.Count < 1)
        {
            this.lbl_message.Visible = true;
            this.lbl_message.Text = GetTran("000053", "没有数据，不能导出Excel！");
        }
        else
        {
            this.lbl_message.Visible = false;
            if (Encryption.Encryption.GetEncryptionSetting("--Name--"))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Name"] = Encryption.Encryption.GetDecipherName(Convert.ToString(dt.Rows[i]["Name"]));
                }
            }
        }
        string[] coloums = { "number=" + this.GetTran("000024", "会员编号"), "name=" + this.GetTran("000025", "会员姓名"), "ExpectNum=" + this.GetTran("000000", "销售期数"), "PayExpectNum=" + this.GetTran("000000", "付款期数"), "OrderId=" + this.GetTran("000079", "订单号"), "totalMoney=" + this.GetTran("000322", "金额"), "totalPv=" + this.GetTran("000414", "积分"), "OrderDate=" + this.GetTran("005942", "报单时间")};
        Excel.OutToExcel(dt, this.GetTran("003044", "会员销售明细"), coloums);
    }
}
