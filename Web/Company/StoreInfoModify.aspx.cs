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
using Model.Other;
using System.Text;
using DAL;
using BLL.other.Company;
using BLL.CommonClass;
using Model;
using Standard.Classes;
using System.Data.SqlClient;

public partial class Company_StoreInfoModify : BLL.TranslationBase
{
    protected string msg = "";
    static int sphours = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerStoreManage);
        Response.Cache.SetExpires(DateTime.Now);
        GridView1.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");
        sphours = Convert.ToInt32(Session["WTH"]);
        if (!IsPostBack)
        {
            DataTable dt = DBHelper.ExecuteDataTable("select levelint,levelstr from bsco_level where levelflag=1 order by levelint");
            this.dplLevel.DataTextField = "levelstr";
            this.dplLevel.DataValueField = "levelint";
            this.dplLevel.DataSource = dt;
            this.dplLevel.DataBind();
            dplLevel.Items.Insert(0, new ListItem(GetTran("000633", "全部"), "-1"));
            CommonDataBLL.BindQishuList(this.drpExpectNum, true);
            BtnSeach_Click(null, null);
        }
        Translations();
    }
    private void Translations()
    {
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"000015","操作"},
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
        string table = "StoreInfo";
        string name = Encryption.Encryption.GetEncryptionName(DisposeString.DisString(this.txtname.Text));
        string storeid = DisposeString.DisString(this.txtstoreid.Text);
        string storename = Encryption.Encryption.GetEncryptionName(DisposeString.DisString(this.txtstorename.Text));
        int ExpectNum = Convert.ToInt32(this.drpExpectNum.SelectedItem.Value);
        string level = dplLevel.SelectedValue;
        StringBuilder sb = new StringBuilder();
        sb.Append(" 1=1 and  StoreState=1");
        if (name.Length > 0)
        {
            sb.Append(" and Name='" + name + "'");
        }
        if (storename.Length > 0)
        {
            sb.Append(" and StoreName='" + storename + "'");
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
        ViewState["SQLSTR"] = "SELECT ID,StoreInfo.Number,StoreID,StoreInfo.Name,StoreName,TotalAccountMoney,(TotalAccountMoney-TotalOrderGoodMoney-(select isnull(sum(ActualStorage*PreferentialPrice*(-1)),0) from Product,stock where Product.ProductId=stock.ProductId and stock.StoreId=StoreInfo.Storeid and ActualStorage<0)) as kebaodane,(TotalAccountMoney-TotalOrderGoodMoney) as kedinghuoe,ExpectNum,Direct,RegisterDate,StoreLevelInt,SCPCCode FROM StoreInfo  WHERE " + sb;
        Pager pager = this.Page.FindControl("Pager1") as Pager;
        pager.PageTable = table;
        pager.PageColumn = "ID,StoreInfo.Number,StoreID,StoreInfo.Name,StoreName,StoreInfo.TotalAccountMoney,(TotalAccountMoney-TotalOrderGoodMoney-(select isnull(sum(ActualStorage*PreferentialPrice*(-1)),0) from Product,stock where Product.ProductId=stock.ProductId and stock.StoreId=StoreInfo.Storeid and ActualStorage<0)) as kebaodane,(TotalAccountMoney-TotalOrderGoodMoney) as kedinghuoe,ExpectNum,Direct,RegisterDate,StoreLevelInt,SCPCCode";
        pager.PageSize = 10;
        pager.Condition = sb.ToString();
        pager.key = "ID";
        pager.ControlName = "GridView1";
        pager.PageCount = 0;
        pager.Pageindex = 0;
        pager.InitBindData = true;
        pager.PageBind();

        Translations();
    }

    protected void givSearchStoreInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument.ToString());
        if (e.CommandName == "del")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string storeid = row.Cells[1].Text;
            int k = StoreInfoEditBLL.getMemberCount(storeid);
            if (k == 0)
            {
                int i = 0;
                StoreInfoEditBLL seb = new StoreInfoEditBLL();
                BLL.CommonClass.ChangeLogs cl_h_info = new BLL.CommonClass.ChangeLogs("storeinfo", "(ltrim(rtrim(id)))");
                cl_h_info.AddRecord(ID);
                i = seb.DelStore(ID);
                cl_h_info.DeletedIntoLogs(BLL.CommonClass.ChangeCategory.company4, Session["Company"].ToString(), BLL.CommonClass.ENUM_USERTYPE.objecttype2);
                if (i > 0)
                {
                    //Response.Write("<script language='javascript'>alert('删除成功！')</script>");
                    this.msg = "<script language='javascript'>alert('" + GetTran("000749", "删除成功！") + "')</script>";
                    BtnSeach_Click(null, null);
                }
                else
                {
                    //Response.Write("<script language='javascript'>alert('删除失败！')</script>");
                    this.msg = "<script language='javascript'>alert('" + GetTran("000417", "删除失败！") + "')</script>";
                    return;
                }
            }
            else
            {
                this.msg = "<script language='javascript'>alert('" + GetTran("000756", "对不起，该店铺已经发生了业务关系，不能删除！") + "')</script>";
                return;
            }
        }
        else
        {
            BtnSeach_Click(null, null);
        }
    }
    protected void download_Click(object sender, EventArgs e)
    {
        DataTable dt = DBHelper.ExecuteDataTable("select s.Number,s.StoreID,s.Name,s.StoreName,s.TotalAccountMoney,(s.TotalAccountMoney-s.TotalOrderGoodMoney-(select isnull(sum(ActualStorage*PreferentialPrice*(-1)),0) from Product,stock where Product.ProductId=stock.ProductId and stock.StoreId=s.Storeid and ActualStorage<0)) as kebaodane,(s.TotalAccountMoney-s.TotalOrderGoodMoney) as kedinghuoe,s.ExpectNum,s.Direct,s.RegisterDate,b.levelstr from storeinfo s,bsco_level b where s.storelevelint=b.levelint and levelflag=1 and" + ViewState["sql"].ToString() + "order by RegisterDate desc");
        foreach (DataRow row in dt.Rows)
        {
            row["Name"] = Encryption.Encryption.GetDecipherName(row["Name"].ToString());
            row["StoreName"] = Encryption.Encryption.GetDecipherName(row["StoreName"].ToString());

            try
            {
                row["RegisterDate"] = Convert.ToDateTime(row["RegisterDate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString();
            }
            catch { }

        }

        Excel.OutToExcel(dt, GetTran("000460", "店铺信息"), new string[] { "Number=" + GetTran("000024", "会员编号"), "Name=" + GetTran("000025", "会员姓名"), "StoreID=" + GetTran("000150", "店铺编号"), "StoreName=" + GetTran("000040", "店铺名称"), "TotalAccountMoney=" + GetTran("000041", "总金额"), "kedinghuoe=" + GetTran("006007", "现金余额"), "ExpectNum=" + GetTran("000042", "办店期数"), "Direct=" + GetTran("000043", "推荐人编号"), "RegisterDate=" + GetTran("000057", "注册日期"), "levelstr=" + GetTran("000046", "级别") });
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

    private void datalist(string sql)
    {
        DataTable dt = new DataTable();
        dt = CommonDataBLL.datalist(sql);
        if (dt == null)
        {
            this.msg = "<script language='javascript'>alert('" + GetTran("000760", "对不起，找不到指定条件的记录！") + "')</script>";
            return;
        }
        if (dt.Rows.Count < 1)
        {
            this.msg = "<script language='javascript'>alert('" + GetTran("000760", "对不起，找不到指定条件的记录！") + "')</script>";
            return;
        }
        if (dt.Rows.Count > 0)
        {
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");

            e.Row.Cells[3].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[3].Text.ToString());
            e.Row.Cells[4].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[4].Text.ToString());
            int curr = Convert.ToInt32(DBHelper.ExecuteScalar("select rate from Currency where id=standardMoney"));

            // e.Row.Cells[5].Text = Double.Parse(e.Row.Cells[5].Text.ToString()).ToString("0.00");

            //Label lab = (Label)e.Row.FindControl("StoreLevelInt");
            ////string l = DBHelper.ExecuteScalar("select levelstr from bsco_level where levelflag=1 and levelint = " + Convert.ToInt32(lab.Text.ToString()) + "").ToString();
            //string l= getStoLevelStr()
            //lab.Text = l.ToString();
            Label labScp = (Label)e.Row.FindControl("country");

            string scp = DBHelper.ExecuteScalar("select top 1 country from city where CPCCode like '" + labScp.Text.ToString() + "%'").ToString();
            labScp.Text = scp.ToString();
            string storeid = e.Row.Cells[1].Text;
            int n = StoreInfoEditBLL.getMemberCount(storeid);
            int EdCount = 1201;
            int DeCount = 1202;
            EdCount = Permissions.GetPermissions(EnumCompanyPermission.CustomerUpdateStore);
            DeCount = Permissions.GetPermissions(EnumCompanyPermission.CustomerStoreManageDelete);
            HyperLink modlink = (HyperLink)e.Row.FindControl("Hyperlink1");
            if (EdCount.ToString() == "1201" || EdCount.ToString() == "1202")
            {
                modlink.Visible = true;
            }
            else
            {
                modlink.Visible = false;
            }
            if (DeCount.ToString() == "1202")
            {
                ((LinkButton)e.Row.FindControl("DelLink")).Attributes["onclick"] = "return confirm('" + GetTran("000765", "此操作为不可恢复操作，您确认要删除吗？") + "')";
            }
            else
            {
                ((LinkButton)e.Row.FindControl("DelLink")).Visible = false;

            }
            string manageId = BLL.CommonClass.CommonDataBLL.getManageID(2);
            if (storeid == "9999999999" || storeid == manageId)
            {
                ((LinkButton)e.Row.FindControl("DelLink")).Visible = false;
            }
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
            Translations();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        BtnSeach_Click(null, null);
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