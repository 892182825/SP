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
using BLL.Logistics;
using Model.Other;
using BLL.other.Company;
using DAL;
using Standard.Classes;
public partial class Company_ThirdLogistics : BLL.TranslationBase
{
    ThirdLogisticsDLL thirdLogisticsDLL = new ThirdLogisticsDLL();
    protected void Page_Load(object sender, EventArgs e)       
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerThirdLogistics);

        

        if (!IsPostBack)
        {
            this.DropCurrency.DataSource = CountryBLL.GetCountryModels();
            this.DropCurrency.DataTextField = "name";
            this.DropCurrency.DataValueField = "id";
            this.DropCurrency.DataBind();
            bind();

            SetFanY();
            this.DataBind() ;
        }

       
    }

    public void SetFanY()
    {
        this.TranControls(this.Button2, new string[][] { new string[] { "000048", "查 询" } });

        this.TranControls(this.gvThirdLogistics,
                            new string[][]{	
                                new string []{"000015","操作"},													 
                                new string []{"000015","操作"},													 
                                new string []{"001897","公司编号"},
                                new string []{"001900","公司名称"},	
                                new string []{"001910","负责人姓名"},
                                new string []{"",""},
                                new string []{"000044","办公电话"},
                                new string []{"000071","传真电话"},
                                new string []{"",""},
                                new string []{"",""},
                                new string []{"001918","所在国家"},
                                new string []{"001921","所在省份"},
                                new string []{"001923","所在城市"},
                                new string []{"000087", "开户银行"},
                                new string []{"000088","银行帐号"},
                                new string []{"000962", "税号"},
                                new string []{"",""},
                                new string []{"001981", "批准日期"},
                                new string []{"001983", "营业执照号码"},
                                new string []{"000078", "备注"}

                                });
    }

    public void bind()
    {
        string country = DropCurrency.SelectedItem.Text ;

        ViewState["tj"]="country='"+country+"'";

        Pager1.PageBind(0, 10, @" (select a.ID,a.Number,a.LogisticsCompany,a.Principal,a.Telephone1,
                                    a.Telephone2,a.Telephone3,a.Telephone4,a.cpccode,b.Country,b.Province,
                                    b.City,a.StoreAddress,a.PostalCode,a.LicenceCode,a.bankcode,
                                    (select bankname from memberbank where bankcode = a.bankcode) as bank,a.BankCard,a.RigisterDate,
                                    a.Remark,a.Tax,a.Administer,a.LogisticsPerson,a.OperateIP,a.OperateNum 
                                    from Logistics a left outer join city b on a.cpccode=b.cpccode
                                    ) t", " * ", " country='"+country+"' ", "id", "gvThirdLogistics");
    }

   
    protected void gvThirdLogistics_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerThirdLogisticsDelete);
        if (e.CommandName == "del")
        {   //获取id
            int id = int.Parse(e.CommandArgument.ToString());
            if (thirdLogisticsDLL.DelThirdLogistics(id))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000749", "删除成功!") + "');location.href='ThirdLogistics.aspx';</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("000417", "删除失败!") + "');location.href='ThirdLogistics.aspx';</script>");
            }
        }
    }
    /// <summary>
    /// 点击查看备注详情时跳转到指定的页面
    /// </summary>
    /// <param name="dd"></param>
    /// <param name="id"></param>
    /// <param name="_pageUrl"></param>
    /// <returns></returns>
    protected string SetVisible(string remark, string id)
    {
        if (string.IsNullOrEmpty(remark))
        {
            return GetTran("000221", "无");
        }
        else
        {
            return "<a href='ThirdLogisticsRemark.aspx?id=" + id + "'>" + GetTran("000440", "查看") + "</a>";
        }
    }

    public string GetBiaoZhunTime(string dt)
    {
        return Convert.ToDateTime(dt).AddHours(BLL.other.Company.WordlTimeBLL.ConvertAddHours()).ToShortDateString();
    }

    private void datalist(string country)
    {
        DataTable dt = new DataTable();
        dt = thirdLogisticsDLL.GetThirdLogistics(country);
        if (dt == null)
        {
            Response.Write("<script language='javascript'>alert('" + GetTran("000760", "对不起，找不到指定条件的记录") + "')</script>");
            return;
        }
        if (dt.Rows.Count < 1)
        {
            Response.Write("<script language='javascript'>alert('"+ GetTran("000760", "对不起，找不到指定条件的记录") +"')</script>");
            return;
        }
        if (dt.Rows.Count > 0)
        {
            gvThirdLogistics.DataSource = dt;
            gvThirdLogistics.DataBind();
        }
    }
    //必须加这个方法，要不然会引发：必须放在具有 runat=server 的窗体标记内... 异常。
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void btnDownLoad_Click(object sender, EventArgs e)
    {

        Response.AppendHeader("Content-Disposition", "attachment;filename=ThirdLogistics.xls");
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");//设置输出流为简体中文

        string sqlcmd = @"select * from (select a.ID,a.Number,a.LogisticsCompany,a.Principal,a.Telephone1,
                                    a.Telephone2,a.Telephone3,a.Telephone4,a.cpccode,b.Country,b.Province,
                                    b.City,a.StoreAddress,a.PostalCode,a.LicenceCode,a.bankcode,
                                    (select bankname from memberbank where bankcode = a.bankcode) as bank,a.BankCard,a.RigisterDate,
                                    a.Remark,a.Tax,a.Administer,a.LogisticsPerson,a.OperateIP,a.OperateNum 
                                    from Logistics a left outer join city b on a.cpccode=b.cpccode
                                    ) t where "+ViewState["tj"]+" order by t.id desc";

        DataTable dt = DBHelper.ExecuteDataTable(sqlcmd);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //dt.Rows[i]["InceptPerson"] = Encryption.Encryption.GetDecipherName(dt.Rows[i]["InceptPerson"].ToString());
            //dt.Rows[i]["InceptAddress"] = Encryption.Encryption.GetDecipherAddress(dt.Rows[i]["InceptAddress"].ToString());

            dt.Rows[i]["RigisterDate"] = GetBiaoZhunTime(dt.Rows[i]["RigisterDate"].ToString());
        }

        System.Text.StringBuilder sbexcel = Excel.GetExcelTable(dt, "第三方物流公司表", new string[] { "Number="+GetTran("001897","公司编号"), "LogisticsCompany="+GetTran("001900","公司名称"), "Principal="+GetTran("001910","负责人姓名"), "Telephone2="+GetTran("000044","办公电话"),
            "Telephone4="+GetTran("000071","传真电话") ,  "country="+GetTran("001918","所在国家") , "province="+GetTran("001921","所在省份"), "city="+GetTran("001923","所在城市") , "bank="+GetTran("000087", "开户银行"), "BankCard="+GetTran("000088","银行帐号"), "Tax="+GetTran("000962", "税号"), "RigisterDate="+GetTran("001981", "批准日期"), "LicenceCode="+GetTran("001983", "营业执照号码")});


        Response.Write(sbexcel.ToString());
        Response.End();

    }
    protected void gvThirdLogistics_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int tedit = Permissions.GetPermissions(EnumCompanyPermission.CustomerThirdLogisticsEdit);
            int tdel = Permissions.GetPermissions(EnumCompanyPermission.CustomerThirdLogisticsDelete);
            if (tedit != 0)
                ((HyperLink)e.Row.FindControl("hlnkModify")).Visible = true;
            else
                ((HyperLink)e.Row.FindControl("hlnkModify")).Visible = false;
            if (tdel != 0)
                ((LinkButton)e.Row.FindControl("delLink")).Visible = true;
            else
                ((LinkButton)e.Row.FindControl("delLink")).Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "xabc=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=xabc");


        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            SetFanY();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        bind();

        SetFanY();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        btnDownLoad_Click(null, null);

    }
}
