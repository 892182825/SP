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
using System.Collections.Generic;
using Model;
using Model.Other;
using BLL.MoneyFlows;
public partial class Company_CompanyAccount : BLL.TranslationBase 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.FinanceSetCompanyAccount);

        Response.Cache.SetExpires(DateTime.Now);

        DropDownList ddlCountry = (DropDownList)CountryUC.FindControl("ddlCountry");
        ddlCountry.DataBound += new EventHandler(bindcontrol);
        ddlCountry.SelectedIndexChanged += new EventHandler(ddlCountry_SelectedIndexChanged);
        ddlCountry.AutoPostBack = true;
        if (!IsPostBack)
        {
            this.TranControls(this.but1, new string[][] { new string[] { "002047", "添 加" } });
        }
        Translations();
    }


    private void Translations()
    {
        this.TranControls(this.gwbankCard,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"000047","国家"},
                    new string []{"001243","开户行"},
                    new string []{"000086","开户名"},
                    new string []{"002073","账号"}});
    }
    /// <summary>
    /// 当国家改变时改变显示的内容
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindcontrol(null, null);
        Translations();
    }

    //数据绑定
    private void bindcontrol(object obj, EventArgs e)
    {
        DropDownList ddlCountry = (DropDownList)CountryUC.FindControl("ddlCountry");
        //获取国家下拉列表的值
        int ID = -1;
        if (!int.TryParse(ddlCountry.SelectedItem.Value, out ID))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("004066", "请选择国家！！！") + "')</script>");
        }
        
        string sql = "  countryID=" + ID;
        ViewState["sqlE"] = "select id,Bank,countryID,BankBook,bankname from companybank where " + sql+" order by id desc";
        this.Pager1.ControlName = "gwbankCard";
        this.Pager1.key = "id";
        this.Pager1.PageColumn = "  id,Bank,countryID,BankBook,bankname  ";
        this.Pager1.Pageindex = 0;
        this.Pager1.PageTable = "companybank";
        this.Pager1.Condition = sql;
        this.Pager1.PageSize = 10;
        this.Pager1.PageCount = 0;
        this.Pager1.PageBind();
        Translations();
    }
    /// <summary>
    /// 添加和修改功能
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void but1_Click(object sender, EventArgs e)
    {
        DropDownList ddlCountry = (DropDownList)CountryUC.FindControl("ddlCountry");
        if (this.txtbank.Text.Trim().Length <= 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("004092", "银行不能为空！") + "')</script>");
            return;
        }
        if (this.txtname.Text.Trim().Length <= 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("004090", "开户名不能为空！") + "')</script>");
            return;
        }
        if (this.txtcard.Text.Trim().Length <= 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("004089", "账号不能为空！") + "')</script>");
            return;
        }
        CompanyBankModel mode = new CompanyBankModel();

        mode.Bank = this.txtbank.Text.Trim();
        mode.Bankname = this.txtname.Text.Trim();
        mode.BankBook = this.txtcard.Text.Trim();
        mode.CountryID = int.Parse(ddlCountry.SelectedItem.Value);
        //当为空时，此操作为添加；否则，修改操作
        if (ViewState["ID"] != null)
        {
            mode.ID = int.Parse(ViewState["ID"].ToString());

            string card = ViewState["bnakbook"].ToString();
            //修改账号
            ChangeLogs cl = new ChangeLogs("companybank", "ltrim(rtrim(str(id)))");
            cl.AddRecord(mode.ID);
            if (CompanyBankBLL.UpdCompanyBank(mode) == 1)
            {
                cl.AddRecord(mode.ID);
                cl.ModifiedIntoLogs(ChangeCategory.company13, Session["Company"].ToString(), ENUM_USERTYPE.objecttype3);
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("004086", "修改账户成功！") + "')</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("004084", "修改账户失败！") + "')</script>");
            }
        }
        else  //添加
        {
            //验证账号
            if (CompanyBankBLL.ValidateCompanyBank(mode))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("004083", "此账号已存在！！！") + "')</script>");
                return;
            }
            else
            {
                //添加账户
                if (CompanyBankBLL.AddCompanyBank(mode))
                {
                    ViewState["ID"] = null;
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("004081", "帐户添加成功！") + "')</script>");
                }
            }
        }
        this.TranControls(this.but1, new string[][] { new string[] { "002047", "添 加" } });
        this.txtcard.Text = string.Empty;
        ViewState["ID"] = null;
        bindcontrol(null, null);
    }
    /// <summary>
    /// 判断是删除还是修改功能
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gwbankCard_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "updCard")
        {   //获取id
            int id = int.Parse(e.CommandArgument.ToString());
            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent);
            this.txtbank.Text = gwbankCard.Rows[gvrow.RowIndex].Cells[2].Text.Trim();
            this.txtname.Text = gwbankCard.Rows[gvrow.RowIndex].Cells[3].Text.Trim();
            this.txtcard.Text = gwbankCard.Rows[gvrow.RowIndex].Cells[4].Text.Trim();
            ViewState["ID"] = id;
            ViewState["bnakbook"] = gwbankCard.Rows[gvrow.RowIndex].Cells[3].Text.Trim();
            this.TranControls(this.but1, new string[][] { new string[] { "001124", "保 存" } });
        }
        else if (e.CommandName == "delCard")
        {
            //获取id
            int id = int.Parse(e.CommandArgument.ToString());
            ChangeLogs cl = new ChangeLogs("companybank", "ltrim(rtrim(str(id)))");
            cl.AddRecord(id);
            if (CompanyBankBLL.DelCompanyBank(id))
            {
                cl.AddRecord(id);
                cl.DeletedIntoLogs(ChangeCategory.company13, Session["Company"].ToString(), ENUM_USERTYPE.objecttype3);
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000749", "删除成功！") + "')</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000417", "删除失败！") + "')</script>");
            }
            ViewState["ID"] = null;
        }

        bindcontrol(null, null);
    }

    /// <summary>
    /// 绑定国家Name
    /// </summary>
    public string bindcontry(object OID)
    {
        return CompanyBankBLL.GetCountryByID(int.Parse(OID.ToString()));
    }

    /// <summary>
    /// 导出EXCEL
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEXCL_Click(object sender, ImageClickEventArgs e)
    {
        if (ViewState["sqlE"] == null)
        {
            return;
        }
        DataTable dt = DAL.DBHelper.ExecuteDataTable(ViewState["sqlE"].ToString());

        Excel.OutToExcel1(dt, GetTran("003083", "账户管理"), new string[] { "countryID=" + GetTran("000047", "国家"), "Bank=" + GetTran("001243", "开户行"), "bankname=" + GetTran("000086", "开户名"), "BankBook=" + GetTran("002073", "账号") });
    }
    protected void gwbankCard_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
        Translations();
    }
}
