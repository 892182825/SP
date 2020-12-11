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
using System.Text;

public partial class Company_StoreReportDetail : BLL.TranslationBase
{
    DataTable dt = new DataTable();
    private string Flag;
    protected StringBuilder Builder;
    protected string lbl_flag;
    protected string msg;
    public string str;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        this.Builder = new StringBuilder();
        CommonDataBLL.setlanguage();
        this.lbl_BeginDate.Text = Session["Begin"].ToString();
        this.lbl_EndDate.Text = Session["End"].ToString();
        if (!this.IsPostBack)
        {
            if (Request.Params["Flag"] == "store")
            {
                msg = GetTran("000675", "店铺汇款汇总");
                getdata1();
                this.lbl_flag = GetTran("000571", "店铺号");
                this.lbl_title.Text = GetTran("000388", "店铺");
                str = this.lbl_title.Text;
                this.Flag = "remitnumber";
            }
            else if (Request.Params["Flag"] == "Country")
            {
                msg = GetTran("000000", "国家汇款汇总");
                getdata3();
                this.lbl_flag = GetTran("000000", "国家");
                this.lbl_title.Text = GetTran("000000", "国家");
                str = this.lbl_title.Text;
                this.Flag = "Country";
            }
            else
            {
                msg = GetTran("000678", "区域汇款汇总");
                getdata2();
                this.lbl_flag = GetTran("000109", "省份");
                this.lbl_title.Text = GetTran("000109", "省份");
                str = this.lbl_title.Text;
                this.Flag = "Province";
            }
            this.Literal1.Text = this.Bind();
            this.lbl_time.Text = DateTime.Now.ToString();
        }
    }
    private void getdata1()
    {
        dt = CommonDataBLL.getDataByStoreid(Session["Begin"].ToString(), Session["End"].ToString());
        ViewState["dt"] = CommonDataBLL.getDataByStoreid(Session["Begin"].ToString(), Session["End"].ToString());
    }
    private void getdata2()
    {
        dt = CommonDataBLL.getDataByCity(Session["Begin"].ToString(), Session["End"].ToString());
        ViewState["dt"] = CommonDataBLL.getDataByCity(Session["Begin"].ToString(), Session["End"].ToString());
    }
    /// <summary>
    /// 按按国家汇总
    /// </summary>
    private void getdata3()
    {
        dt = CommonDataBLL.getDataByCountry(Session["Begin"].ToString(), Session["End"].ToString());
        ViewState["dt"] = CommonDataBLL.getDataByCountry(Session["Begin"].ToString(), Session["End"].ToString());
    }
    private string getstr(string str)
    {
        if (str == "0")
        {
            str = " &nbsp;";
            return str;
        }
        else
        {
            int index = str.IndexOf(".");
            if (index == -1)
            {
                return str + ".00";
            }
            else
            {
                if (str.Length < index + 3)
                {
                    str = str.Substring(0, index + 2);
                }
                else
                {
                    str = str.Substring(0, index + 3);

                }
                return str;
            }
        }
    }
    private string Bind()
    {
        string space = "&nbsp;";

        this.Builder.Append("<table cellSpacing=0 cellPadding=1 width=1570 class=colortest >");

        this.Builder.Append("<TR height=10>");
        this.Builder.Append("<TD align=center width=30 rowSpan=3 class=td1>" + GetTran("000012", "序号") + "</TD>");
        this.Builder.Append("<TD align=center width=80 rowSpan=3 class=td1>" + lbl_flag + "</TD>");
        this.Builder.Append("<TD align=center colSpan=5 class=td1>" + GetTran("000680", "期初数") + "</TD>");
        this.Builder.Append("<TD align=center colSpan=10 class=td1>" + GetTran("000681", "新增数") + "</TD>");
        this.Builder.Append("</TR>");
        this.Builder.Append("<TR height=10>");
        this.Builder.Append("<TD align=center width=80 rowSpan=2 class=td1>" + GetTran("000689", "周转货款") + "</TD>");
        this.Builder.Append("<TD align=center width=80 rowSpan=2 class=td1>" + GetTran("000538", "报单款") + "</TD>");

        this.Builder.Append("<TD align=center width=80 rowSpan=2 class=td1>" + GetTran("000689", "周转货款") + "</TD>");
        this.Builder.Append("<TD align=center width=80 rowSpan=2 class=td1>" + GetTran("000538", "报单款") + "</TD>");
        this.Builder.Append("<TD align=center colSpan=2 class=td1>" + GetTran("000698", "付款方式") + "</TD>");
        this.Builder.Append("<TD align=center colSpan=3 class=td1>" + GetTran("000595", "确认方式") + "</TD>");
        this.Builder.Append("<TD align=center width=60 rowSpan=2 class=td1>" + GetTran("000689", "周转货款") + "</TD>");
        this.Builder.Append("<TD align=center width=60 rowSpan=2 class=td1>" + GetTran("000538", "报单款") + "</TD>");
        this.Builder.Append("<TD align=center width=80 rowSpan=2 class=td1>" + GetTran("000689", "周转货款") + "</TD>");
        this.Builder.Append("<TD align=center width=80 rowSpan=2 class=td1>" + GetTran("000538", "报单款") + "</TD>");
        this.Builder.Append("</TR>");
        this.Builder.Append("<TR height=10>");
        this.Builder.Append("<TD align=center width=80 class=td1>" + GetTran("000699", "现金") + "</TD>");
        this.Builder.Append("<TD align=center width=80 class=td1>" + GetTran("000535", "汇款") + "</TD>");
        this.Builder.Append("<TD align=center width=80 class=td1>" + GetTran("000643", "传真") + "</TD>");
        this.Builder.Append("<TD align=center width=80 class=td1>" + GetTran("000644", "核实") + "</TD>");
        this.Builder.Append("<TD align=center width=80 class=td1>" + GetTran("000646", "电话") + "</TD>");
        this.Builder.Append("</TR>");

        getdtColumntotal(dt);
        this.Builder.Append("<tr height=20><td width=30 align=center class=td1>" + space + "</td>");
        this.Builder.Append("<Td width=60 align=center class=td1>" + GetTran("000630", "合计") + "</td>");

        this.Builder.Append("<td width=80 align=right class=td1><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal["byajin"].ToString()) + "</font></td>");
        this.Builder.Append("<td width=80 align=right class=td1><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal["bjiameng"].ToString()) + "</font></td>");

        this.Builder.Append("<td width=80 align=right class=td1><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal["nyajin"].ToString()) + "</font></td>");
        this.Builder.Append("<td width=80 align=right class=td1><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal["njiameng"].ToString()) + "</font></td>");

        this.Builder.Append("<td width=80 align=right class=td1><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal["nxianjin"].ToString()) + "</font></td>");
        this.Builder.Append("<td width=80 align=right class=td1><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal["nhuikuan"].ToString()) + "</font></td>");

        this.Builder.Append("<td width=80 align=right class=td1><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal["nchuanzhen"].ToString()) + "</font></td>");
        this.Builder.Append("<td width=80 align=right class=td1><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal["nheshi"].ToString()) + "</font></td>");
        this.Builder.Append("<td width=80 align=right class=td1><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal["ndianhua"].ToString()) + "</font></td>");

        this.Builder.Append("<td width=60 align=right class=td1><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal["tyajin"].ToString()) + "</font></td>");
        this.Builder.Append("<td width=60 align=right class=td1><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal["tjiameng"].ToString()) + "</font></td>");

        this.Builder.Append("<td width=80 align=right class=td1><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal["eyajin"].ToString()) + "</font></td>");
        this.Builder.Append("<td width=80 align=right class=td1><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal["ejiameng"].ToString()) + "</font></td>");
        this.Builder.Append("</tr>" + "\n");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            this.Builder.Append("<tr height=20>");
            this.Builder.Append("<td width=30 align=center class=td1>" + (i + 1).ToString() + "</td>");
            this.Builder.Append("<td width=60 align=center class=td1>" + dt.Rows[i][Flag] + "</td>");

            this.Builder.Append("<td width=80 align=right class=td1>" + getstr(dt.Rows[i]["Byajin"].ToString()) + "</td>");
            this.Builder.Append("<td width=80 align=right class=td1>" + getstr(dt.Rows[i]["Bjiameng"].ToString()) + "</td>");

            this.Builder.Append("<td width=80 align=right class=td1>" + getstr(dt.Rows[i]["Nyajin"].ToString()) + "</td>");
            this.Builder.Append("<td width=80 align=right class=td1>" + getstr(dt.Rows[i]["Njiameng"].ToString()) + "</td>");

            this.Builder.Append("<td width=80 align=right class=td1>" + getstr(dt.Rows[i]["Nxianjin"].ToString()) + "</td>");
            this.Builder.Append("<td width=80 align=right class=td1>" + getstr(dt.Rows[i]["Nhuikuan"].ToString()) + "</td>");

            this.Builder.Append("<td width=80 align=right class=td1>" + getstr(dt.Rows[i]["Nchuanzhen"].ToString()) + "</td>");
            this.Builder.Append("<td width=80 align=right class=td1>" + getstr(dt.Rows[i]["Nheshi"].ToString()) + "</td>");
            this.Builder.Append("<td width=80 align=right class=td1>" + getstr(dt.Rows[i]["Ndianhua"].ToString()) + "</td>");

            this.Builder.Append("<td width=60 align=right class=td1>" + getstr(dt.Rows[i]["Tyajin"].ToString()) + "</td>");
            this.Builder.Append("<td width=60 align=right class=td1>" + getstr(dt.Rows[i]["Tjiameng"].ToString()) + "</td>");

            this.Builder.Append("<td width=80 align=right class=td1>" + getstr(dt.Rows[i]["Eyajin"].ToString()) + "</td>");
            this.Builder.Append("<td width=80 align=right class=td1>" + getstr(dt.Rows[i]["Ejiameng"].ToString()) + "</td>");
            this.Builder.Append("</tr>");
        }

        this.Builder.Append("</table>");
        return this.Builder.ToString();
    }
    string[] columns ={"strkey",
							  "bjiameng","byajin","njiameng","nyajin","nxianjin","nhuikuan","nchuanzhen","nheshi","ndianhua",
							  "tjiameng","tyajin","ejiameng","eyajin"};
    Hashtable htColumntotal;
    private void getdtColumntotal(DataTable dt)
    {
        if (htColumntotal == null)
        {
            htColumntotal = new Hashtable();
        }
        for (int i = 0; i < columns.Length; i++)
        {
            if (columns[i].ToString().Trim() != "strkey")
                htColumntotal.Add(columns[i], 0);
        }
        double total;
        string columnname;
        foreach (DataColumn dc in dt.Columns)
        {
            total = 0;
            columnname = string.Empty;
            columnname = dc.ColumnName.ToLower();
            if (htColumntotal.Contains(columnname))
            {
                foreach (DataRow dr in dt.Rows)
                {
                    total += double.Parse(dr[columnname].ToString());
                }
            }
            htColumntotal[columnname] = total;
        }
    }
}