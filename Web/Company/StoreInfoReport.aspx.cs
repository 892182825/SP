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

/// <summary>
/// Add Namespace
/// </summary>
using Model.Other;
using BLL.CommonClass;
using System.Text;
using DAL;

/*
 * 完成时间：2009-11-03
 * 完成人：  汪  华
 */
public partial class Company_StoreInfoReport : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.ReportStoreDetail2);
        if (!IsPostBack)
        {
            this.txtBeginDate.Text = CommonDataBLL.GetDateBegin().ToString();
            this.txtEndDate.Text = CommonDataBLL.GetDateEnd().ToString();

            Bind();
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.btnSearch, new string[][] { new string[] { "000048", "查 询" } });

        this.TranControls(this.GridView1,
              new string[][]{

                    new string []{"000037","店编号"},
                    new string []{"001423","店名称"},
                    new string []{"001424","店长编号"},
                    new string []{"000039","店长姓名"},
                    new string []{"001425","可订货款"},
                    new string []{"001427","店级别"},
                    new string []{"001428","店邮编"},
                    new string []{"000319","负责人电话"},
                    new string []{"000044","办公电话"},
                    new string []{"000052","手机"},
                    new string []{"000071","传真电话"},
                    new string []{"001430","电子邮件"},
                    new string []{"000057","注册日期"},
                    new string []{"001432","店铺省市"},
                    new string []{"000313","店铺所在地"}
                });

    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DateTime beginDate, endDate;
        if (this.txtBeginDate.Text.Trim() == "" || this.txtEndDate.Text.Trim() == "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("" + GetTran("001409", "对比起，开始时间和截止时间不能为空") + "！"));
            return;
        }

        else
        {
            try
            {
                beginDate = Convert.ToDateTime(this.txtBeginDate.Text.Trim());
                endDate = Convert.ToDateTime(this.txtEndDate.Text.Trim()).AddDays(1);
                if (beginDate > endDate)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("" + GetTran("001410", "对不起，开始时间必须小于或等于截止时间") + "！"));
                }
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert("" + GetTran("001413", "请正确输入时间") + "！"));
                return;
            }
            //Session["beginDate"] = beginDate;
            //Session["endDate"] = endDate;
            //Response.Write("<script lanagage='javascript'>window.open('StoreDetail.aspx?Flag=2')</script>");
            Bind();
        }
    }

    private void Bind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@" s.Number=m.Number and s.cpccode=ct.cpccode and s.storelevelint=lv.levelint and lv.levelflag=1 and s.RegisterDate between '" + Convert.ToDateTime(this.txtBeginDate.Text.Trim()).ToUniversalTime().ToString() + "' and '" + Convert.ToDateTime(this.txtEndDate.Text.Trim()).AddDays(1).ToUniversalTime().ToString() + "'");

        string sql = @"select s.*,leftMoney=s.TotalAccountMoney-s.TotalOrderGoodMoney,shMoney=s.TotalAccountMoney-s.TotalOrdergoodMoney, MemberName=m.name,ct.Country+ct.Province+ct.City+ct.Xian as adds,storeaddress as ad,lv.levelstr from StoreInfo s,MemberInfo m ,city ct,bsco_level lv where  " + sb.ToString() + "  order by s.RegisterDate desc ";
        ViewState["SQLSTR"] = sql;

        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.Pageindex = 0;
        pager.PageSize = 10;
        pager.PageTable = "StoreInfo s,MemberInfo m ,city ct,bsco_level lv ";
        pager.Condition = sb.ToString();
        pager.PageColumn = "s.*,leftMoney=s.TotalAccountMoney-s.TotalOrderGoodMoney,shMoney=s.TotalAccountMoney-s.TotalOrdergoodMoney, MemberName=m.name,ct.Country+ct.Province+ct.City+ct.Xian as adds,storeaddress as ad,lv.levelstr";
        pager.ControlName = "GridView1";
        pager.key = "s.RegisterDate";
        pager.InitBindData = true;
        pager.PageBind();

        string str = GetZJE(" and " + sb.ToString());
        if (str != "")
        {
            try
            {
                lab_ckdhkzj.Text = double.Parse(str.Split(',')[0]).ToString();
            }
            catch
            {
                lab_ckdhkzj.Text = "未知";
            }
        }

        //DataTable dt = DAL.DBHelper.ExecuteDataTable(sql);
        //if (dt.Rows.Count < 1)
        //{
        //    this.lbl_message.Visible = true;
        //    this.lbl_message.Text = "";
        //}
        //else
        //{
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        row["Name"] = Encryption.Encryption.GetDecipherName(row["Name"].ToString());
        //        row["MemberName"] = Encryption.Encryption.GetDecipherName(row["MemberName"].ToString());

        //        row["HomeTele"] = Encryption.Encryption.GetDecipherTele(row["HomeTele"].ToString());
        //        row["officeTele"] = Encryption.Encryption.GetDecipherTele(row["officeTele"].ToString());
        //        row["MobileTele"] = Encryption.Encryption.GetDecipherTele(row["MobileTele"].ToString());
        //        row["FaxTele"] = Encryption.Encryption.GetDecipherTele(row["FaxTele"].ToString());

        //        row["ad"] = Encryption.Encryption.GetDecipherAddress(row["ad"].ToString());
        //        row["RegisterDate"] = Convert.ToDateTime(row["RegisterDate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("yyyy-MM-dd HH:mm:ss");
        //    }

        //    Session["dt"] = dt;

        //    this.lbl_message.Visible = false;
        //    this.GridView1.DataSource = dt;
        //    this.GridView1.DataBind();
        //}
    }

    /// <summary>
    /// 获得总可订货款和总可报单款
    /// </summary>
    /// <param name="sqltj">查询条件</param>
    /// <returns></returns>
    private static string GetZJE(string sqltj)
    {
        string zjef = "";
        DataTable dtTotal = DBHelper.ExecuteDataTable("select sum(s.TotalAccountMoney-s.TotalOrdergoodMoney) as shmoney,sum(s.TotalAccountMoney-s.TotalOrderGoodMoney) as leftMoney from StoreInfo s,MemberInfo m ,city ct,bsco_level lv where 1=1 " + sqltj);
        zjef = dtTotal.Rows[0]["shmoney"].ToString() + "," + dtTotal.Rows[0]["leftMoney"].ToString();
        return zjef;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");

            //((Label)e.Row.FindControl("lbl_num")).Text = Convert.ToString(e.Row.RowIndex + 1);
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations();
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GridView1.PageIndex = e.NewPageIndex;
        //this.GridView1.DataSource = (DataTable)Session["dt"];
        //this.GridView1.DataBind();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnDownExcel_Click(object sender, EventArgs e)
    {
        string cmd = ViewState["SQLSTR"].ToString();
        DataTable dt = DBHelper.ExecuteDataTable(cmd);
        if (dt == null || dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('" + GetTran("000053", "没有数据，不能导出Excel！") + "')</script>");
            return;
        }

        Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xls");
        Response.ContentEncoding = Encoding.UTF8;


        foreach (DataRow row in dt.Rows)
        {
            try
            {
                row["Name"] = Encryption.Encryption.GetDecipherName(row["Name"].ToString());
                row["MemberName"] = Encryption.Encryption.GetDecipherName(row["MemberName"].ToString());

                row["HomeTele"] = Encryption.Encryption.GetDecipherTele(row["HomeTele"].ToString());
                row["officeTele"] = Encryption.Encryption.GetDecipherTele(row["officeTele"].ToString());
                row["MobileTele"] = Encryption.Encryption.GetDecipherTele(row["MobileTele"].ToString());
                row["FaxTele"] = Encryption.Encryption.GetDecipherTele(row["FaxTele"].ToString());

                row["ad"] = Encryption.Encryption.GetDecipherAddress(row["ad"].ToString());
                row["RegisterDate"] = Convert.ToDateTime(row["RegisterDate"]).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("yyyy-MM-dd");

            }
            catch
            {
            }
        }

        StringBuilder sb = Excel.GetExcelTable(dt, GetTran("001421", "店铺名单"), new string[] { "StoreId=" + GetTran("000037", "店编号"), "Name=" + GetTran("001423", "店名称"), "Number=" + GetTran("001424", "店长编号"), "MemberName=" + GetTran("000039", "店长姓名"), "shmoney=" + GetTran("001425", "可订货款"), "leftMoney=" + GetTran("001426", "可报单款"), "LevelStr=" + GetTran("001427", "店级别"), "HomeTele=" + GetTran("000319", "负责人电话"), "officeTele=" + GetTran("000044", "办公电话"), "MobileTele=" + GetTran("000052", "手机"), "FaxTele=" + GetTran("000071", "传真电话"), "RegisterDate=" + GetTran("000057", "注册日期"), "adds=" + GetTran("001432", "店铺省市"), "ad=" + GetTran("000313", "店铺所在地") });

        Response.Write(sb.ToString());

        Response.Flush();
        Response.End();
    }

    protected string GetbyName(string name)
    {
        return Encryption.Encryption.GetDecipherName(name);
    }

    protected string GetbyTele(string Tele)
    {
        return Encryption.Encryption.GetDecipherTele(Tele);
    }

    protected string Getbyad(string ad)
    {
        return Encryption.Encryption.GetDecipherAddress(ad);
    }

    protected string GetbyRegisterDate(string RegisterDate)
    {
        return Convert.ToDateTime(RegisterDate).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToString("yyyy-MM-dd");
    }
}