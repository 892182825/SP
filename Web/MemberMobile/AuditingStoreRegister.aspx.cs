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
using System.Data.SqlClient;
using System.Text;
using BLL.other.Company;
using Model.Other;
using DAL;
using BLL.CommonClass;
using Standard.Classes;

public partial class Company_AuditingStoreRegister : BLL.TranslationBase 
{
    string pagecolumn = "ID,number,StoreID,name,StoreName,ExpectNum,Direct,TotalAccountMoney,OperateNum,Direct,OfficeTele,RegisterDate,StoreLevelInt";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.MemRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            if (Session["Default_Currency"] == null)
            {
                CommonDataBLL.BindCurrency_IDList(this.DropCurrency,"");
            }
            else
            {
                CommonDataBLL.BindCurrency_IDList(this.DropCurrency, Session["Default_Currency"].ToString());
            }
            //Pager pager = Page.FindControl("Pager1") as Pager;
            //  pager.PageBind(0,10,"UnauditedStoreInfo",pagecolumn," 1=1","RegisterDate","givStoreInfo");
            this.chShi.Checked = true;
            btnSeasch_Click(null, null);
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.givStoreInfo,
                new string[][]{
                    new string []{"000024","会员编号"},
                    new string []{"000037","店编号"},
                    new string []{"000039","店长姓名"},
                    new string []{"000040","店铺名称"},
                    new string []{"000041","总金额"},
                    new string []{"000042","办店期数"},
                    new string []{"000043","推荐人编号"},
                    new string []{"000044","办公电话"},
                    new string []{"000031","注册时间"},
                    new string []{"000046","级别"}
                });
        this.TranControls(this.btnAddStore, new string[][] { new string[] { "000049", "添加店铺" } });
        this.TranControls(this.chShi, new string[][] { new string[] { "000059", "按照省市查询" } });
    }
    protected void givStoreInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string name = e.CommandName;
        string[] aaa = e.CommandArgument.ToString().Split(',');
        string storeid = aaa[0].ToString();
        string number = aaa[1].ToString();
    }

    protected object GetRDate(object obj)
    {
        if (obj != null)
        {
            try { return Convert.ToDateTime(obj).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()); }
            catch { }
        }
        return "";
    }

    protected void btnSeasch_Click(object sender, EventArgs e)
    {
        //string pagecolumn = "ID,number,StoreID,name,StoreName,ExpectNum,Direct,TotalAccountMoney,OperateNum,Direct,OfficeTele,RegisterDate,StoreLevelStr";
        StringBuilder sb = new StringBuilder();
        sb.Append("1=1 ");
        if (chShi.Checked)
        {
            if (this.CountryCity1.Country != "" && this.CountryCity1.Country != "" && this.CountryCity1.Country != "")
            {


                UserControl_CountryCity countryCity = Page.FindControl("CountryCity1") as UserControl_CountryCity;
                string cp = DBHelper.ExecuteScalar("select cpccode from city where country='" + this.CountryCity1.Country + "' and province='" + this.CountryCity1.Province + "' and city='" + this.CountryCity1.City + "'").ToString();

                sb.Append(" and cpccode='" + cp.ToString() + "'");
            }
        }
        UserControl_ExpectNum expectnum = Page.FindControl("ExpectNum1") as UserControl_ExpectNum;
        if (expectnum.ExpectNum > 0)
        {

            sb.Append(" and ExpectNum=" + expectnum.ExpectNum);
        }
        if (txtStoreId.Text.Trim().Length > 0)
        {
            sb.Append(" and StoreId='"+txtStoreId.Text.Trim()+"'");
        }
        if (ddl_shenhe.SelectedValue == "1")
        {
            sb.Append(" and number='" + Session["Member"].ToString() + "'");
            ViewState["sql"] = sb.ToString();
            string codition = sb.ToString();
            Pager pager = Page.FindControl("Pager1") as Pager;
            ViewState["pagewhere"] = sb.ToString();
            pager.PageBind(0, 10, "UnauditedStoreInfo", pagecolumn, codition, "RegisterDate", "givStoreInfo");
        }
        else
        {
            sb.Remove(0, sb.Length);
            sb.Append(" number='" + Session["Member"].ToString() + "'");
            ViewState["sql"] = sb.ToString();
            string codition = sb.ToString();
            Pager pager = Page.FindControl("Pager1") as Pager;
            ViewState["pagewhere"] = sb.ToString();
            pager.PageBind(0, 10, "storeinfo", pagecolumn, codition, "RegisterDate", "givStoreInfo");
        }
        Translations();
    }
    protected void btndownExcel_Click(object sender, EventArgs e)
    {
        this.givStoreInfo.Columns[0].Visible = false;
        Response.Clear();
        Response.Buffer = true;

        Response.Charset = "GB2312";
        this.givStoreInfo.AllowPaging = false;
        this.givStoreInfo.AllowSorting = false;
        Response.AppendHeader("Content-Disposition", "attachment;filename=registerStore.xls");
        Response.ContentEncoding = System.Text.Encoding.UTF7;
        pagecolumn = "ID,number,StoreID,name,StoreName,ExpectNum,Direct,TotalAccountMoney,OperateNum,Direct,OfficeTele,RegisterDate,StoreLevelInt";
        DataTable dt = DBHelper.ExecuteDataTable("select s.Number,s.StoreID,s.Name,s.StoreName,s.TotalAccountMoney,s.ExpectNum,s.Direct,s.OfficeTele,s.RegisterDate,b.levelstr from UnauditedStoreInfo s,bsco_level b where s.storelevelint=b.levelint and levelflag=1 and " + ViewState["sql"].ToString() + "order by RegisterDate desc");
        foreach (DataRow row in dt.Rows)
        {
            row["Name"] = Encryption.Encryption.GetDecipherName(row["Name"].ToString());
            row["StoreName"] = Encryption.Encryption.GetDecipherName(row["StoreName"].ToString());
            row["OfficeTele"] = Encryption.Encryption.GetDecipherName(row["StoreName"].ToString());

            try
            {
                row["RegisterDate"] = Convert.ToDateTime(row["RegisterDate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours());
            }
            catch { }
        }

        //StringBuilder sb = Excel.GetExcelTable(dt, "待审核店铺信息", new string[] { "Number=会员编号", "Name=店长姓名", "StoreID=店铺编号", "StoreName=店铺名称", "TotalAccountMoney=总金额",  "ExpectNum=期数", "Direct=推荐编号","OfficeTele=办公电话", "RegisterDate=注册时间", "levelstr=店铺级别" });
        StringBuilder sb = Excel.GetExcelTable(dt, GetTran("000397", "待审核店铺信息"), new string[] { "Number=" + GetTran("000024", "会员编号"), "Name=" + GetTran("000039", "店长姓名"), "StoreID=" + GetTran("000150", "店铺编号"), "StoreName=" + GetTran("000040", "店铺名称"), "TotalAccountMoney=" + GetTran("000041", "总金额"), "ExpectNum=" + GetTran("000045", "期数"), "Direct=" + GetTran("000026", "推荐编号"), "RegisterDate=" + GetTran("000031", "注册时间"), "levelstr=" + GetTran("000046", "级别") });
        Response.ContentType = "application/ms-excel";
        this.EnableViewState = false;
        System.Globalization.CultureInfo mycitrad = new System.Globalization.CultureInfo("ZH-CN", true);
        System.IO.StringWriter oStringWriter = new System.IO.StringWriter(mycitrad);
        HtmlTextWriter oHtmlTextWriter = new HtmlTextWriter(oStringWriter);
        this.givStoreInfo.RenderControl(oHtmlTextWriter);
        Response.Write(sb.ToString());
        Response.End();
        this.givStoreInfo.Columns[0].Visible = true; ;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        
    }
    protected void btnAddStore_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegisterStore.aspx");
    }
    protected void givStoreInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int curr = Convert.ToInt32(DBHelper.ExecuteScalar("select rate from Currency where id=standardMoney"));
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            Label lab = (Label)e.Row.FindControl("StoreLevelInt");
            string l = DBHelper.ExecuteScalar("select levelstr from bsco_level where levelflag=1 and levelint = " + Convert.ToInt32(lab.Text.ToString()) + "").ToString();
            lab.Text = l.ToString();
            e.Row.Cells[3].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[3].Text.ToString());
            e.Row.Cells[4].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[4].Text.ToString());
            e.Row.Cells[8].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[8].Text.ToString());
            if (AjaxClass.GetCurrency(curr, Convert.ToInt32(DropCurrency.SelectedValue)) == 0)
            {
                e.Row.Cells[5].Text ="0";
            }
            else
            {
                e.Row.Cells[5].Text = (Convert.ToDouble(e.Row.Cells[5].Text) * AjaxClass.GetCurrency(curr, Convert.ToInt32(DropCurrency.SelectedValue))).ToString("f2");
          
            }
        }
        Translations();
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        btnSeasch_Click(null, null);
    }
    protected void DropCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSeasch_Click(null, null);
    }
}
