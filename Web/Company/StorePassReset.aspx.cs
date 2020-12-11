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
using System.Text;
using Model.Other;
using DAL;
using Standard.Classes;
using BLL.CommonClass;

/******
 * 开发人：宋俊
 * 作用：店铺密码重置
 * 
 */
public partial class Company_StorePassReset : BLL.TranslationBase
{
    protected string msg = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerQueryStorePassword);
        if (!IsPostBack)
        {
            btnSeach_Click(null,null);
        }
        Translations();
    }
    private void Translations()
    {
        this.btnSeach.Text = GetTran("000048", "查 询");
        this.TranControls(this.givStoreInfo,
                new string[][]{
                    new string []{"000664","密码重置"},
                    new string []{"000024","会员编号"},
                    new string []{"000037","店编号"},
                    new string []{"000039","店长姓名"},
                    new string []{"000040","店铺名称"}});

    }

    protected void btnSeach_Click(object sender, EventArgs e)
    {
        StringBuilder sb=new StringBuilder ();
        sb.Append("1=1 ");
        string storeid=DisposeString.DisString(txtStoreId.Text);
        if (storeid.Length>0)
        {
            sb.Append(" and Storeid='"+storeid+"'");
        }
        string storename=Encryption.Encryption.GetEncryptionName(DisposeString.DisString(txtStoreName.Text));
        if (storename.Length>0)
        {
            sb.Append(" and StoreName like '"+storename+"%'");
        }
        UserControl_ExpectNum exp = Page.FindControl("ExpectNum1") as UserControl_ExpectNum; 
        int ExpectNum = exp.ExpectNum;
        if (ExpectNum>0)
        {
            sb.Append(" and ExpectNum="+ExpectNum);
        }
        ViewState["sql"] = sb.ToString();
        Pager pager = Page.FindControl("Pager1")as Pager;
        pager.PageBind(0, 10, "StoreInfo", "ID,StoreInfo.Number,StoreId,StoreInfo.Name,StoreName", sb.ToString(), "RegisterDate", "givStoreInfo");
        Translations();
    }

    protected void btnDownExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = CommonDataBLL.GetStoreQueryToExcel(ViewState["sql"].ToString());
        foreach (DataRow row in dt.Rows)
        {
            row["Name"] = Encryption.Encryption.GetDecipherName(row["Name"].ToString());
            row["StoreName"] = Encryption.Encryption.GetDecipherName(row["StoreName"].ToString());


        }
        Excel.OutToExcel(dt, GetTran("000460", "店铺信息"), new string[] { "Number=" + GetTran("000024", "会员编号"), "StoreID=" + GetTran("000037", "店编号"), "Name=" + GetTran("000039", "店长姓名"), "StoreName=" + GetTran("000040", "店铺名称") });
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        
    }
    protected void givStoreInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) 
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            e.Row.Cells[3].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[3].Text.ToString());
            e.Row.Cells[4].Text = Encryption.Encryption.GetDecipherName(e.Row.Cells[4].Text.ToString());
        }
        Translations();
    }
    protected void lkSubmit_Click(object sender, EventArgs e)
    {
        btnSeach_Click(null, null);
    }
}
