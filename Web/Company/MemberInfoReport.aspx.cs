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
using System.Data.SqlClient;
using System.Text;
using BLL.other.Company;
using BLL.CommonClass;
using BLL;
using DAL;
using Standard.Classes;

public partial class Company_H_infoReport : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.ReportMemberDetail2);
        if (!IsPostBack)
        {
            this.DatePicker1.Text = CommonDataBLL.GetDateBegin().ToString();
            this.DatePicker2.Text = CommonDataBLL.GetDateEnd().ToString();

            Bind();
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.Btn_Dsearch, new string[][] { new string[] { "000048", "查 询" } });

        this.TranControls(this.gvMemberDetail,
              new string[][]{
                   
                    new string []{"000024","会员编号"},
                    new string []{"000107","姓名"},
         
                    new string []{"000027","安置编号"},
                    new string []{"000097","安置姓名"},
                    new string []{"000026","推荐编号"},
                    new string []{"000192","推荐姓名"},
                    new string []{"000029","注册期数"},
                    new string []{"000057","注册日期"},
                    new string []{"000069","移动电话"},
                    new string []{"000916","国家省份城市"},
                    new string []{"000072","地址"},
                   
                    new string []{"000083","证件号码"},
                     new string []{"001404","证件名称"},
                    new string []{"001406","银行名称"},
                    new string []{"001407","银行卡号"}
                });
    }

    protected void Btn_Dsearch_Click(object sender, EventArgs e)
    {
        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001377", "请输入要查询的日期区间") + "!');</script>");
            return;
        }
        else
        {
            try
            {
                DateTime d1 = Convert.ToDateTime(this.DatePicker1.Text);
                DateTime d2 = Convert.ToDateTime(this.DatePicker2.Text);
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001379", "日期格式不正确") + "!');</script>");
                return;
            }
            Bind();
        }
    }

    private void Bind()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"A.RegisterDate 
                        between '" + Convert.ToDateTime(this.DatePicker1.Text).ToUniversalTime() + "' and '" + Convert.ToDateTime(this.DatePicker2.Text).AddDays(1).ToUniversalTime() + "'");

        ViewState["SQLSTR"] = sb.ToString();
        Pager pager = Page.FindControl("Pager1") as Pager;
        pager.Pageindex = 0;
        pager.PageSize = 10;
        pager.PageTable = "MemberInfo A left join city ct on a.CPCCode=ct.CPCCode";
        pager.Condition = sb.ToString();
        pager.PageColumn = "A.Number,A.[Name],A.PetName,A.Sex,A.Placement,(select [name] from memberinfo b where b.number=a.placement) AnzhiName,A.Direct,(select [name] from memberinfo c where c.number=a.Direct) TuijianName,A.ExpectNum,A.RegisterDate,A.MobileTele as tele,A.PostalCode,ct.Country+ct.Province+ct.City+ct.Xian as address,A.Address as ad,(select papertype from bsco_PaperType pt where pt.papertypecode=a.papertypecode) PaperType,A.PaperNumber,(select isnull(bankname,'无') from memberbank mb where mb.bankcode=a.bankcode) Bankname,A.BankCard";
        pager.ControlName = "gvMemberDetail";
        pager.key = "A.RegisterDate";
        pager.InitBindData = true;
        pager.PageBind();
        Translations();
    }

    protected void gvMemberDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ///控制样式
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            Translations();
        }
    }

    protected void ToExcel(System.Web.UI.Control ctl)
    {
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=Excel.xls");
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
        HttpContext.Current.Response.ContentType = "application/ms-excel";//image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword
        ctl.Page.EnableViewState = false;
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        ctl.RenderControl(hw);
        HttpContext.Current.Response.Write(tw.ToString());
        HttpContext.Current.Response.End();
    }

    protected void btnDownExcel_Click(object sender, EventArgs e)
    {
        ToExcel(gvMemberDetail);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected string GetbyName(string name)
    {
        return Encryption.Encryption.GetDecipherName(name);
    }

    protected string GetbyPaperNumber(string number)
    {
        return Encryption.Encryption.GetDecipherNumber(number);
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