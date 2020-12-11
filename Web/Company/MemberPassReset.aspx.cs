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
using BLL.CommonClass;
using Model.Other;
using Model;
using System.Text;
using BLL;
using DAL;
using Standard.Classes;

public partial class Company_MemberPassReset : BLL.TranslationBase
{
    protected string msg = "";
    MemPassResetBLL mb = new MemPassResetBLL();
    //CommonDataBLL CommonData = new CommonDataBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerQueryMemberPassword);
        Response.Cache.SetExpires(DateTime.Now);
        if (!IsPostBack)
        {
            CommonDataBLL.BindQishuList(this.DropDownExpectNum, true);
            getMemberInfo();
        }
        Translations();
    }
    private void Translations()
    {

        this.BtnConfirm.Text = GetTran("000340", "查询");
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"000024","会员编号"},
                    new string []{"000063","会员昵称"},
                    new string []{"000025","会员姓名"},
                    //new string []{"000030","所属店铺"},
                    new string []{"000027","安置编号"},
                    new string []{"000026","推荐编号"},
                    new string []{"000029","注册期数"},
                    new string []{"000031","注册时间"}});

    }
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        getMemberInfo();
    }

    public void getMemberInfo()
    {
        string Number = DisposeString.DisString(this.Number.Text.Trim());
        string Name = DisposeString.DisString(this.Name.Text.Trim());
        string Recommended = DisposeString.DisString(this.Recommended.Text.Trim());
        string Placement = DisposeString.DisString(this.Placement.Text.Trim());
        int ExpectNum = 0;
        if (this.DropDownExpectNum.SelectedValue.ToString() == "")
        {
            ExpectNum = 0;
        }
        else
        {
            ExpectNum = Convert.ToInt32(this.DropDownExpectNum.SelectedItem.Value);
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(" 1=1 ");
        if (Number.Length > 0)
        {
            sb.Append(" and Number like'%" + Number + "%'");
        }
        if (Name.Length > 0)
        {
            sb.Append(" and Name like'%" + Encryption.Encryption.GetEncryptionName(Name) + "%'");
        }
        if (Recommended.Length > 0)
        {
            sb.Append(" and Direct like'%" + Recommended + "%'");
        }
        if (Placement.Length > 0)
        {
            sb.Append(" and Placement like'%" + Placement + "%'");
        }
        if (ExpectNum > 0)
        {
            sb.Append(" and ExpectNum=" + ExpectNum);
        }
        //if (TextBox1.Text.Trim().Length > 0)
        //{
        //    sb.Append(" and StoreID like'%" + TextBox1.Text.Trim() + "%'");
        //}
        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.Pageindex = 0;
        pager.PageSize = 10;
        pager.PageTable = "MemberInfo";
        pager.Condition = sb.ToString();
        pager.PageColumn = "*";
        pager.ControlName = "GridView1";
        pager.key = "ID";
        pager.InitBindData = true;
        pager.PageBind();
        ViewState["condition"] = sb.ToString() + " order by registerdate desc";
        Translations();
    }
    protected void download_Click(object sender, EventArgs e)
    {
        DataTable dt = CommonDataBLL.GetMemberPassResetToExcel(ViewState["condition"].ToString());
        if (dt == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000053", "没有数据，不能导出Excel！") + "')</script>");
            return;
        }
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000053", "没有数据，不能导出Excel！") + "')</script>");
            return;
        }
        foreach (DataRow row in dt.Rows)
        {
            row["Name"] = Encryption.Encryption.GetDecipherName(row["Name"].ToString());//解密姓名
            try
            {
                row["RegisterDate"] = Convert.ToDateTime(row["RegisterDate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours());
            }
            catch
            {
            }
        }
        Excel.OutToExcel(dt, GetTran("000512", "会员密码重置"), new string[] { "Number=" + GetTran("000024", "会员编号"), "Name=" + GetTran("000025", "会员姓名"), "Placement=" + GetTran("000027", "安置编号"), "Direct=" + GetTran("000026", "推荐编号"), "ExpectNum=" + GetTran("000029", "注册期数"), "RegisterDate=" + GetTran("000031", "注册日期") });
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            e.Row.Cells[2].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[2].Text);//解密姓名
            try
            {
                e.Row.Cells[7].Text = DateTime.Parse(e.Row.Cells[7].Text).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();
            }
            catch
            {
            }
            if (!BLL.CommonClass.CommonDataBLL.GetRole2(Session["Company"].ToString(), true))
            {
                e.Row.Cells[4].Text = "";
            }
            if (!BLL.CommonClass.CommonDataBLL.GetRole2(Session["Company"].ToString(), false))
            {
                e.Row.Cells[5].Text = "";
            }
            HiddenField hf = e.Row.FindControl("HiddenField1") as HiddenField;
            if (hf.Value == "0")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
            Translations();
        }
    }
}