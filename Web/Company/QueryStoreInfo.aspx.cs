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
using Model.Other;
using BLL.other.Company;
using System.Text;
using BLL;
using BLL.CommonClass;
using DAL;
using Standard.Classes;
using System.Data.SqlClient;

/// <summary>
/// 编写人：宋俊
/// 作用：店铺信息查询
/// </summary>
public partial class Company_QueryStoreInfo : BLL.TranslationBase
{
    static int sphours = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);    //判断是否存在session
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerQueryStore);    //权限判断
        givSearchStoreInfo.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
        sphours = Convert.ToInt32(Session["WTH"]);
        if (!IsPostBack)
        {
            DataTable dt = DBHelper.ExecuteDataTable("select levelint,levelstr from bsco_level where levelflag=1 order by levelint");
            this.dplLevel.DataTextField = "levelstr";
            this.dplLevel.DataValueField = "levelint";
            this.dplLevel.DataSource = dt;
            this.dplLevel.DataBind();
            dplLevel.Items.Insert(0, new ListItem(GetTran("000633", "全部"), "-1"));
            BtnSeach_Click(null, null);
        }
        Translations();
    }
    private void Translations()
    {
        this.BtnSeach.Text = GetTran("000048", "查 询");
        this.TranControls(this.givSearchStoreInfo,
                new string[][]{
                    new string []{"000035","详细信息"},
                    new string []{"000037","店编号"},
                    new string []{"000024","会员编号"},
                    new string []{"000039","店长姓名"},
                    new string []{"000040","店铺名称"},
                    new string []{"000042","办店期数"},
                    new string []{"000043","推荐人编号"},
                    new string []{"000057","注册日期"},
                    new string []{"000454","店铺所属国家"},
                    new string []{"000046","级别"}});

    }

    protected void BtnSeach_Click(object sender, EventArgs e)
    {
        string name = Encryption.Encryption.GetEncryptionName(DisposeString.DisString(this.txtname.Text));
        string storeid = DisposeString.DisString(this.txtstoreid.Text);
        string storename = Encryption.Encryption.GetEncryptionName(DisposeString.DisString(this.txtstorename.Text));
        string level = dplLevel.SelectedValue;
        int ExpectNum = 0;
        UserControl_ExpectNum texpectnum = Page.FindControl("ExpectNum1") as UserControl_ExpectNum; //这边使用as转换没有必要使用try catch来捕获异常，因为使用as时，不能转换不会报异常只会返回一个null
        if (texpectnum == null)
        {
            ExpectNum = -1;
        }
        else
        {
            ExpectNum = texpectnum.ExpectNum;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(" 1=1 ");
        if (name.Length > 0)
        {
            sb.Append(" and Name like '" + name + "%'");
        }
        if (storename.Length > 0)
        {
            sb.Append(" and StoreName like '" + storename + "%'");
        }
        if (storeid.Length > 0)
        {
            sb.Append(" and StoreId='" + storeid + "'");
        }
        if (level != "-1")
        {
            sb.Append(" and storelevelint=" + dplLevel.SelectedValue);
        }
        string totalDataStart = txtBox_OrderDateTimeStart.Text.Trim();
        string totalDataEnd = txtBox_OrderDateTimeEnd.Text.Trim();
        if (totalDataStart != "")
        {
            Convert.ToDateTime(totalDataStart);
            sb.Append(" and dateadd(hour," + sphours + ",RegisterDate)>='" + totalDataStart + " 00:00:00'");
        }
        if (totalDataEnd != "")
        {
            Convert.ToDateTime(totalDataEnd);
            sb.Append(" and dateadd(hour," + sphours + ",RegisterDate)<='" + totalDataEnd + " 23:59:59'");
        }
        if (ExpectNum > 0)
        {
            sb.Append(" and ExpectNum=" + ExpectNum);
        }
        ViewState["sql"] = sb.ToString();
        Pager pager = this.Page.FindControl("Pager1") as Pager;
        pager.PageTable = " StoreInfo";
        pager.PageColumn = "ID,StoreInfo.Number,StoreID,StoreInfo.Name,StoreName,TotalAccountMoney,(TotalAccountMoney-TotalOrderGoodMoney-(select isnull(sum(ActualStorage*PreferentialPrice*(-1)),0) from Product,stock where Product.ProductId=stock.ProductId and stock.StoreId=StoreInfo.Storeid and ActualStorage<0)) as kebaodane,(TotalAccountMoney-TotalOrderGoodMoney) as kedinghuoe,ExpectNum,Direct,RegisterDate,StoreLevelInt,SCPCCode";
        pager.PageSize = 10;
        pager.Condition = sb.ToString();
        pager.key = "ID";
        pager.ControlName = "givSearchStoreInfo";
        pager.Pageindex = 0;
        pager.PageBind();
        Translations();

    }


    protected void givSearchStoreInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DetailsStoreInfo")
        {
            Response.Redirect("StoreInfoDetail.aspx?id=" + e.CommandArgument);
        }
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

    protected void btndownExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = CommonDataBLL.GetStoreQueryToExcel(ViewState["sql"].ToString());
        foreach (DataRow row in dt.Rows)
        {
            row["Name"] = Encryption.Encryption.GetDecipherName(row["Name"].ToString());
            row["StoreName"] = Encryption.Encryption.GetDecipherName(row["StoreName"].ToString());

            try
            {
                DataTable dtCountry = CommonDataBLL.getStoreCountry(row["SCPCCode"].ToString());
                if (dtCountry != null && dtCountry.Rows.Count > 0)
                {
                    row["SCPCCode"] = dtCountry.Rows[0]["Country"].ToString();
                }
                row["RegisterDate"] = Convert.ToDateTime(row["RegisterDate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours());
            }
            catch
            { }
        }
        Excel.OutToExcel(dt, GetTran("000460", "店铺信息"), new string[] { "StoreID=" + GetTran("000150", "店铺编号"), "Number=" + GetTran("000024", "会员编号"), "Name=" + GetTran("000039", "会员姓名"), "StoreName=" + GetTran("000040", "店铺名称"), "TotalAccountMoney=" + GetTran("000041", "总金额"), "kebaodane=" + GetTran("006007", "现金余额"), "ExpectNum=" + GetTran("000042", "办店期数"), "Direct=" + GetTran("000043", "推荐人编号"), "RegisterDate=" + GetTran("000057", "注册日期"), "SCPCCode=" + GetTran("000454", "店铺所属国家"), "levelstr=" + GetTran("000046", "级别") });
    }
    protected void givSearchStoreInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int curr = CommonDataBLL.getStandCurrency();
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            //Label lab = e.Row.FindControl("StoreLevelInt") as Label;    //之前用强制转换如果不能转换会报异常，用as不会，会返回一个null
            Label labScp = e.Row.FindControl("country") as Label;

            string scp = null;
            string l = null;

            if (labScp != null)
            {
                DataTable dt = CommonDataBLL.getStoreCountry(labScp.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    scp = dt.Rows[0]["Country"].ToString();
                }
                labScp.Text = scp;  //之前已经是string类型还toString()有点多余
            }
            //if (lab != null)
            //{
            //    if (e.Row.Cells[1].Text.Trim().Length > 0)
            //    {
            //        l = CommonDataBLL.GetStoreLevelStr(e.Row.Cells[1].Text);
            //        lab.Text = l;
            //    }

            //}
            e.Row.Cells[3].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[3].Text.ToString());
            e.Row.Cells[4].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[4].Text.ToString());
        }
        Translations();
    }
    protected string getStoLevelStr(string stoid)
    {
        //return CommonDataBLL.GetStoreLevelStr(stoid);
        object o_sl = DAL.DBHelper.ExecuteScalar(@"SELECT b.levelstr FROM storeInfo s inner join bsco_level b on s.StoreLevelInt=b.levelint WHERE StoreID=@sid and b.levelflag=1", new SqlParameter[] {
            new SqlParameter("@sid", stoid)
        }, CommandType.Text);
        if (o_sl != null)
            return o_sl.ToString();
        else
            return "";
    }
}