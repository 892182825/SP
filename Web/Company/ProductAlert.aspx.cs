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
using Model;
using Model.Other;
using BLL;
using BLL.CommonClass;
using DAL;
using Standard.Classes;

public partial class Company_DisplayMemberInfo : BLL.TranslationBase
{
    int type = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerQureyMember);
        Response.Cache.SetExpires(DateTime.Now);
        if (!IsPostBack)
        {
            if (Request.QueryString["dd"] != null && Request.QueryString["dd"] != "")
            {
                type = 1;
            }

            loadProductTypeNum(); //加载产品型号
            getProductInfo();//查询
        }
        Translations();
    }
    /// <summary>
    /// 翻译
    /// </summary>
    private void Translations()
    {
        this.TranControls(this.GridView1,
                new string[][]{
                    new string []{"000558","产品编号"},
                    new string []{"000501","产品名称"},
                    new string []{"000882","产品型号"},
                    new string []{"001877","产品产地"},

                    new string []{"001955","成本价"},
                    new string []{"001956","普通价"},
                    new string []{"001957","优惠价"},
                    new string []{"001953","产品重量"},

                    new string []{"001890","是否组合产品"},
                    new string []{"000359","小单位总入"},
                    new string []{"000362","小单位总出"},
                    new string []{"000363","剩余数量"},
                    new string []{"000365","小单位数量预警"}});
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        type = 0;
        getProductInfo();
    }
    //加载产品预警的产品记录
    public void getProductInfo()
    {
        //条件
        //产品名称
        string productname = "";
        if (this.Name.Text.Trim() != "")
        {
            productname += " and ProductName='"+this.Name.Text.Trim()+"'";
        }
        //产品编号
        string productnum = "";
        if (this.Number.Text.Trim() != "")
        {
            productnum += " and ProductCode='" + this.Number.Text.Trim() + "'";
        }
        //产品型号
        string producttypename = "";
        if (this.ddlProductType.SelectedValue != "-1")
        {
            producttypename += " and b.producttypeid=" + this.ddlProductType.SelectedValue;
        }

        StringBuilder sb = new StringBuilder();

        //存储SQL语句,导出Excel表时，直接调用SQL语句
        ViewState["SQLSTR"] = "select * from ProductQuantity as q,Product as p , ProductUnit as U where p.SmallProductUnitID =  U.ProductUnitID and p.ProductID=q.ProductID and p.isFold=0 and p.Countrycode=86 and  (q.TotalIn-q.TotalOut)- p.AlertnessCount <=0";
        //ViewState["SQLSTR"] = "select a.*, pt.producttypename,b.AlertnessCount,(a.TotalIn-a.TotalOut) SurplusCount,b.* from LogicProductInventory a,Product b,producttype pt "
        //                      + " where a.ProductID=b.ProductID and b.producttypeID=pt.producttypeID and (a.TotalIn-a.TotalOut)<=b.AlertnessCount ";

        //sb.Append(" a.ProductID=b.ProductID and b.producttypeID=pt.producttypeID and (a.TotalIn-a.TotalOut)<=b.AlertnessCount ");
        sb.Append("p.SmallProductUnitID =  U.ProductUnitID and p.ProductID=q.ProductID and p.isFold=0 and p.Countrycode=86 and  (q.TotalIn-q.TotalOut)- p.AlertnessCount <=0");
       // sb.Append(productname + productnum + producttypename);

        //分页
        Pager pager = Page.FindControl("Pager1") as Pager; // LogicProductInventory a,Product b,producttype pt
        pager.PageBind(0, 10, " ProductQuantity as q,Product as p , ProductUnit as U   ", " (q.TotalIn-q.TotalOut) as leftKucun,* ", sb.ToString(), "ID", "GridView1");
        ViewState["condition"] = sb.ToString();
        Translations();
    }
    private void datalist(string sql)
    {
        DataTable dt = new DataTable();
        dt = CommonDataBLL.datalist(sql);
        if (dt == null)
        {
            Response.Write("<script language='javascript'>alert('对不起，找不到指定条件的记录')</script>");
            return;
        }
        if (dt.Rows.Count < 1)
        {
            Response.Write("<script language='javascript'>alert('对不起，找不到指定条件的记录')</script>");
            return;
        }
        if (dt.Rows.Count > 0)
        {
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "background-image:url('images/tabledp.gif')");
        }
        Translations();
    }
    /// <summary>
    /// Excel表导出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        string cmd = ViewState["SQLSTR"].ToString();
        DataTable dt = DBHelper.ExecuteDataTable(cmd);
        dt.Columns.Add("IsCombineProductS", Type.GetType("System.String"));
        if (dt == null)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('没有数据，不能导出Excel！')</script>");
            return;
        }
        if (dt.Rows.Count < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('没有数据，不能导出Excel！')</script>");
            return;
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["IsCombineProductS"] = returnCombination(Convert.ToString(dt.Rows[i]["IsCombineProduct"]));
        }
        Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xls");
        Response.ContentEncoding = Encoding.UTF8;

        StringBuilder sb = Excel.GetExcelTable(dt, GetTran("000530", "产品预警"), new string[] { "ProductCode=" + GetTran("000558", "产品编号"), "ProductName=" + GetTran("000501", "产品名称"), "producttypename=" + GetTran("000882", "产品型号"), "ProductArea=" + GetTran("001877", "产品的产地"), "CostPrice=" + GetTran("001955", "成本价"), "CommonPrice=" + GetTran("001956", "普通价"), "PreferentialPrice=" + GetTran("001957", "优惠价"), "Weight=" + GetTran("001953", "产品重量"), "IsCombineProductS=" + GetTran("001890", "是否是组合产品"), "TotalIn=" + GetTran("000359", "小单位总入"), "TotalOut=" + GetTran("000362", "小单位总出"), "SurplusCount=" + GetTran("000363", "剩余数量"), "AlertnessCount=" + GetTran("000365", "小单位数量预警") });

        Response.Write(sb.ToString());

        Response.Flush();
        Response.End();
    }
    /// <summary>
    /// 加载产品型号
    /// </summary>
    public void loadProductTypeNum()
    {
        string sql = "select * from producttype";
        DataTable dt = DAL.DBHelper.ExecuteDataTable(sql, CommandType.Text);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            this.ddlProductType.Items.Add(new ListItem(dt.Rows[i]["ProductTypeName"].ToString(), dt.Rows[i]["ProductTypeID"].ToString()));
        }
        this.ddlProductType.Items.Add(new ListItem("全部","-1"));
        this.ddlProductType.SelectedValue = "-1";
    }
    /// <summary>
    /// 根据产来的值，判断是否是组合产品
    /// 1 是
    /// 2 否
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public string returnCombination(string s)
    {
        if (s == "1")
        {
            return "是";
        }
        return "否";
    }
}
