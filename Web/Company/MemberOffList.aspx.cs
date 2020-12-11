using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using BLL.other.Company;
using BLL.CommonClass;
using Model.Other;
using Model;
using System.Text;
using BLL;
using DAL;
using Standard.Classes;
using System.Data;
public partial class Company_MemberOffList : BLL.TranslationBase
{
    public string msg = "";
    static int sphours = 0;  //时区差
    MemPassResetBLL mb = new MemPassResetBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(EnumCompanyPermission.MemberOffView);
        Response.Cache.SetExpires(DateTime.Now);
        sphours = Convert.ToInt32(Session["WTH"]);
        if (!IsPostBack)
        {
            CommonDataBLL.BindQishuList(this.DropDownExpectNum, true);
            getMemberInfo();
        }
        Translations();
    }
    private void Translations()
    {
        this.BtnConfirm.Text = GetTran("000048", "查 询");
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"000015","操作"},
                    new string []{"000024","会员编号"},
                    new string []{"000025","会员姓名"},
                    new string []{"000027","安置编号"},
                    new string []{"000026","推荐编号"},
                    new string []{"000029","注册期数"},
                    new string []{"000031","注册时间"}});

    }
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        getMemberInfo();
    }

    private string GetNumber()
    {
        string manageID = Session["Company"].ToString();
        int count = 0;
        string number = BLL.CommonClass.CommonDataBLL.GetWLNumber(manageID, out count);
        if (count == 0)
        {
            return " number in ('')";
        }
        return number;
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
        sb.Append(" 1=1 and MemberState=1 ");
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
        //sb.Append(" and " + this.GetNumber());
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
        Excel.OutToExcel(dt, GetTran("000512", "会员密码重置"), new string[] { "Number=" + GetTran("000024", "会员编号"), "Name=" + GetTran("000025", "会员姓名"),  "Placement=" + GetTran("000027", "安置编号"), "Direct=" + GetTran("000026", "推荐编号"), "ExpectNum=" + GetTran("000029", "注册期数"), "RegisterDate=" + GetTran("000031", "注册日期") });
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
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
            Label lblregisterdate = (Label)e.Row.FindControl("lblregisterdate");

            //检测当前使用国家 时间与格林威治时间的时区差 并显示准确的注册时间
            lblregisterdate.Text = Convert.ToDateTime(drv["registerdate"].ToString()).AddHours(sphours).ToString();

        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
        Translations();
    }
}

