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
using System.Data.SqlClient;
using BLL.other.Company;

public partial class Company_OrderSumReport : BLL.TranslationBase
{
    protected DataTable dt;
    protected StringBuilder Builder;
    protected string Flag;
    protected string lbl_flag;
    protected string msg;

    private void Page_Load(object sender, System.EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        // 在此处放置用户代码以初始化页面
        dt = new DataTable();
        Builder = new StringBuilder();
        this.lbl_Begin.Text = Session["Begin"].ToString();
        this.lbl_End.Text = Session["End"].ToString();
        this.lbl_maketime.Text = DateTime.Now.ToString();
        if (!this.IsPostBack)
        {
            this.lbl_message.Visible = false;

            getdata();
            this.Literal1.Text = Bind();
        }
    }
    private void getdata()
    {
        if (Request.Params["Flag"] == "store")
        {
            msg = GetTran("002241", "店铺订单汇总");
            this.lbl_title.Text = GetTran("000388", "店铺");
            this.lbl_flag = GetTran("000571","店铺号");
            this.Flag = "storeid";

            dt = ReportFormsBLL.GetOrderCollect(Session["Begin"].ToString(), Session["End"].ToString(), "store");

            if (dt.Rows.Count < 1)
            {
                this.lbl_message.Text = GetTran("001946", "没有相关数据!!");
                this.lbl_message.Visible = true;
            }
        }
        if (Request.Params["Flag"] == "city")
        {
            msg = GetTran("002238", "区域订单汇总");
            this.lbl_title.Text = GetTran("000110", "000109");
            this.lbl_flag = GetTran("000110", "000109");
            this.Flag = "City";

            dt = ReportFormsBLL.GetOrderCollect(Session["Begin"].ToString(), Session["End"].ToString(), "city");

            if (dt.Rows.Count < 1)
            {
                this.lbl_message.Text = GetTran("001946","没有相关数据")+"!!!";
                this.lbl_message.Visible = true;
            }
        }
        if (Request.Params["Flag"] == "Country")
        {
            msg = GetTran("000000", "国家订单汇总");
            this.lbl_title.Text = GetTran("000110", "000109");
            this.lbl_flag = GetTran("000110", "000109");
            this.Flag = "Country";

            dt = ReportFormsBLL.GetOrderCollect(Session["Begin"].ToString(), Session["End"].ToString(), "Country");

            if (dt.Rows.Count < 1)
            {
                this.lbl_message.Text = GetTran("001946", "没有相关数据") + "!!!";
                this.lbl_message.Visible = true;
            }
        }

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

    string[] columns ={"ldinghuo","lcheckout",
							  "lnotcheckout","lsend","lnotsend","ltuihuo","lvalid","lnovalid","bdinghuo","bcheckout","bnotcheckout","bsend","bnotsend","btuihuo","bvalid",
							  "bnovalid"};
    private int GetIndexOfArray(string content)
    {
        for (int i = 0; i < columns.Length; i++)
        {
            if (columns[i].Trim().ToLower() == content.Trim())
                return i;
        }
        return -1;
    }
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
                int index = GetIndexOfArray(columnname);
                if (index == -1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000587", "现有列名与页面中的列不匹配") + "');</script>");
                    
                    return;
                }
                htColumntotal.Remove(columnname);
                htColumntotal.Add(index, total);
            }

        }
    }
    private string Bind()
    {
        string space = "&nbsp;";

        this.Builder.Append("<table cellSpacing=0 cellPadding=0 width=100% class=colortest>");

        this.Builder.Append("<TR height=15>");
        this.Builder.Append("<TD rowspan=3 align=center width=30>" + GetTran("000012", "序号") + "</TD>");
        this.Builder.Append("<TD rowspan=3 align=center width=60>" + lbl_flag + "</td>");

        this.Builder.Append("<TD colspan=8 align=center>" + GetTran("002233", "累计订货情况") + "</TD>");
        this.Builder.Append("<TD colspan=8 align=center>" + GetTran("002232", "本期订货情况") + "</TD>");
        this.Builder.Append("</TR>");
        this.Builder.Append("<TR height=15>");
        this.Builder.Append("<TD rowspan=2 align=center width=70>" + GetTran("002188", "订货") + "</TD>");
        this.Builder.Append("<TD colspan=2 align=center>" + GetTran("000938", "支付") + "</TD>");
        this.Builder.Append("<TD colspan=2 align=center>" + GetTran("001340", "发货") + "</TD>");
        this.Builder.Append("<TD colspan=3 align=center>" + GetTran("002224", "退货") + "</TD>");
        this.Builder.Append("<TD rowspan=2 align=center width=70>" + GetTran("002188", "订货") + "</TD>");
        this.Builder.Append("<TD colspan=2 align=center>" + GetTran("000938", "支付") + "</TD>");
        this.Builder.Append("<TD colspan=2 align=center>" + GetTran("001340", "发货") + "</TD>");
        this.Builder.Append("<TD colspan=3 align=center>" + GetTran("002224", "退货") + "</TD>");
        this.Builder.Append("</TR>");
        this.Builder.Append("<TR height=15>");
        this.Builder.Append("<TD width=70 align=center>" + GetTran("000517", "已支付") + "</TD>");
        this.Builder.Append("<TD width=50 align=center>" + GetTran("000521", "未支付") + "</TD>");
        this.Builder.Append("<TD width=70 align=center>" + GetTran("001340", "发货") + "</TD>");
        this.Builder.Append("<TD width=50 align=center>" + GetTran("001370", "未发货") + "</TD>");
        this.Builder.Append("<TD width=40 align=center>" + GetTran("002224", "退货") + "</TD>");
        this.Builder.Append("<TD width=40 align=center>" + GetTran("001011", "已审核") + "</TD>");
        this.Builder.Append("<TD width=40 align=center>" + GetTran("001009", "未审核") + "</TD>");
        this.Builder.Append("<TD width=70 align=center>" + GetTran("000517", "已支付") + "</TD>");
        this.Builder.Append("<TD width=50 align=center>" + GetTran("000521", "未支付") + "</TD>");
        this.Builder.Append("<TD width=70 align=center>" + GetTran("001340", "发货") + "</TD>");
        this.Builder.Append("<TD width=50 align=center>" + GetTran("001370", "未发货") + "</TD>");
        this.Builder.Append("<TD width=40 align=center>" + GetTran("002224", "退货") + "</TD>");
        this.Builder.Append("<TD width=40 align=center>" + GetTran("001011", "已审核") + "</TD>");
        this.Builder.Append("<TD width=40 align=center>" + GetTran("001009", "未审核") + "</TD>");
        this.Builder.Append("</TR>");

        getdtColumntotal(dt);
        this.Builder.Append("<tr height=25><td width=30 align=center>" + space + "</td>");
        this.Builder.Append("<Td width=70 align=center><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: \">" + "合计" + "</font></td>");
        this.Builder.Append("<td width=70 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: \">" + getstr(htColumntotal[0].ToString()) + "</font></td>");
        this.Builder.Append("<td width=70 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: \">" + getstr(htColumntotal[1].ToString()) + "</font></td>");
        this.Builder.Append("<td width=50 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: \">" + getstr(htColumntotal[2].ToString()) + "</font></td>");
        this.Builder.Append("<td width=70 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: \">" + getstr(htColumntotal[3].ToString()) + "</font></td>");
        this.Builder.Append("<td width=50 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: \">" + getstr(htColumntotal[4].ToString()) + "</font></td>");
        this.Builder.Append("<td width=40 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: \">" + getstr(htColumntotal[5].ToString()) + "</font></td>");
        this.Builder.Append("<td width=40 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: \">" + getstr(htColumntotal[6].ToString()) + "</font></td>");
        this.Builder.Append("<td width=40 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: \">" + getstr(htColumntotal[7].ToString()) + "</font></td>");
        this.Builder.Append("<td width=70 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: \">" + getstr(htColumntotal[8].ToString()) + "</font></td>");
        this.Builder.Append("<td width=70 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: \">" + getstr(htColumntotal[9].ToString()) + "</font></td>");
        this.Builder.Append("<td width=50 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: \">" + getstr(htColumntotal[10].ToString()) + "</font></td>");
        this.Builder.Append("<td width=70 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: \">" + getstr(htColumntotal[11].ToString()) + "</font></td>");
        this.Builder.Append("<td width=50 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: \">" + getstr(htColumntotal[12].ToString()) + "</font></td>");
        this.Builder.Append("<td width=40 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: \">" + getstr(htColumntotal[13].ToString()) + "</font></td>");
        this.Builder.Append("<td width=40 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: \">" + getstr(htColumntotal[14].ToString()) + "</font></td>");
        this.Builder.Append("<td width=40 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: \">" + getstr(htColumntotal[15].ToString()) + "</font></td>");
        this.Builder.Append("</tr>" + "\n");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            this.Builder.Append("<tr height=25>");
            this.Builder.Append("<td width=30 align=center>" + (i + 1).ToString() + "</td>");
            this.Builder.Append("<td width=70 align=center>" + dt.Rows[i][Flag] + "</td>");
            this.Builder.Append("<td width=70 align=right >" + getstr(dt.Rows[i]["Ldinghuo"].ToString()) + "</td>");
            this.Builder.Append("<td width=70 align=right >" + getstr(dt.Rows[i]["LCheckOut"].ToString()) + "</td>");
            this.Builder.Append("<td width=50 align=right >" + getstr(dt.Rows[i]["LNotCheckOut"].ToString()) + "</td>");
            this.Builder.Append("<td width=70 align=right >" + getstr(dt.Rows[i]["Lsend"].ToString()) + "</td>");
            this.Builder.Append("<td width=50 align=right >" + getstr(dt.Rows[i]["LnotSend"].ToString()) + "</td>");
            this.Builder.Append("<td width=40 align=right >" + getstr(dt.Rows[i]["Ltuihuo"].ToString()) + "</td>");
            this.Builder.Append("<td width=40 align=right >" + getstr(dt.Rows[i]["Lvalid"].ToString()) + "</td>");
            this.Builder.Append("<td width=40 align=right >" + getstr(dt.Rows[i]["LNoValid"].ToString()) + "</td>");
            this.Builder.Append("<td width=70 align=right >" + getstr(dt.Rows[i]["Bdinghuo"].ToString()) + "</td>");
            this.Builder.Append("<td width=70 align=right >" + getstr(dt.Rows[i]["BCheckOut"].ToString()) + "</td>");
            this.Builder.Append("<td width=50 align=right >" + getstr(dt.Rows[i]["BNotCheckOut"].ToString()) + "</td>");
            this.Builder.Append("<td width=70 align=right >" + getstr(dt.Rows[i]["Bsend"].ToString()) + "</td>");
            this.Builder.Append("<td width=50 align=right >" + getstr(dt.Rows[i]["BNotsend"].ToString()) + "</td>");
            this.Builder.Append("<td width=40 align=right >" + getstr(dt.Rows[i]["Btuihuo"].ToString()) + "</td>");
            this.Builder.Append("<td width=40 align=right >" + getstr(dt.Rows[i]["BValid"].ToString()) + "</td>");
            this.Builder.Append("<td width=40 align=right >" + getstr(dt.Rows[i]["BnoValid"].ToString()) + "</td>");

            this.Builder.Append("</tr>" + "\n");

        }



        this.Builder.Append("</table>");
        return this.Builder.ToString();
    }
}

