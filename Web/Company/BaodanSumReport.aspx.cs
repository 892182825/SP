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

//Add Namespace
using DAL;
using System.Data.SqlClient;
using System.Text;
using BLL.CommonClass;

public partial class Company_BaodanSumReport : BLL.TranslationBase
{
    protected DataTable dt;
    protected string Flag;
    protected string lbl_flag;
    protected StringBuilder Builder;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        dt = new DataTable();
        Builder = new StringBuilder();
        // 在此处放置用户代码以初始化页面
        if (!this.IsPostBack)
        {
            this.lbl_Begin.Text = Session["Begin"].ToString();
            this.lbl_End.Text = Session["End"].ToString();
            this.lbl_message.Visible = false;
            this.lbl_maketime.Text = DateTime.Now.ToString();
            getdata();
            this.Literal1.Text = Bind();
        }
    }

    private void getdata()
    {
        SqlParameter[] param ={
								   new SqlParameter("@BeginDate",SqlDbType.DateTime),
								   new SqlParameter("@endDate",SqlDbType.DateTime)
							   };
        param[0].Value = Session["Begin"].ToString();
        param[1].Value = Session["End"].ToString();
        if (Request.Params["Flag"] == "store")
        {
            this.lbl_title.Text = GetTran("000388", "店铺");
            this.lbl_flag = GetTran("000571", "店铺号");
            this.Flag = "storeid";
            dt = DBHelper.ExecuteDataTable("H_order_getDataBystore", param, CommandType.StoredProcedure);
        }
        if (Request.Params["Flag"] == "city")
        {
            this.lbl_title.Text = GetTran("000110", "城市");
            this.lbl_flag = GetTran("000110", "城市");
            this.Flag = "City";
            dt = DBHelper.ExecuteDataTable("MemberOrder_GetDataByArea", param, CommandType.StoredProcedure);
        }
        if (dt.Rows.Count < 1)
        {
            this.lbl_message.Text = GetTran("000580", "没有相关信息");
            this.lbl_message.Visible = true;
        }
        if (Request.Params["Flag"] == "Country")
        {
            this.lbl_title.Text = GetTran("000000", "国家");
            this.lbl_flag = GetTran("000000", "国家");
            this.Flag = "Country";
            dt = DBHelper.ExecuteDataTable("MemberOrder_GetDataByCountry", param, CommandType.StoredProcedure);
        }
    }
    //如果字符串为0或不带小数点,返回空或者带两位小数的数
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
    //定义要合计的列
    string[] columns ={"lfbaodan","lfcheckout",
							  "lfncheckout","labaodan","lacheckout","lancheckout","bfbaodan","bfcheckout","bfncheckout","babaodan","bacheckout","bancheckout"};

    //取得要合计的列在columns中的索引					 
    private int GetIndexOfArray(string content)
    {
        for (int i = 0; i < columns.Length; i++)
        {
            if (columns[i].Trim().ToLower() == content.Trim())
                return i;
        }
        return -1;
    }
    //计算合计存入htColumntotal中
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
                    Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000587", "现有列名与页面中的列不匹配")));
                    return;
                }
                htColumntotal.Remove(columnname);
                htColumntotal.Add(index, total);
            }
        }
    }
    //动态html形式输出报表
    private string Bind()
    {
        string space = "&nbsp;";

        this.Builder.Append("<table cellSpacing=0 cellPadding=0 width=1020 class=colortest>");

        this.Builder.Append("<TR height=10>");
        this.Builder.Append("<TD align=center width=30 rowSpan=3>" + GetTran("000012", "序号") + "</TD>");
        this.Builder.Append("<TD align=center width=70 rowSpan=3>" + lbl_flag + "</TD>");
        this.Builder.Append("<TD align=center colSpan=6>" + GetTran("000607", "累计报单情况") + "</TD>");
        this.Builder.Append("<TD align=center colSpan=6>" + GetTran("000610", "本期报单情况") + "</TD>");
        this.Builder.Append("</TR>");
        this.Builder.Append("<TR height=10>");
        this.Builder.Append("<TD align=center colSpan=3>" + GetTran("000508", "首次报单") + "</TD>");
        this.Builder.Append("<TD align=center colSpan=3>" + GetTran("000614", "再次报单") + "</TD>");
        this.Builder.Append("<TD align=center colSpan=3>" + GetTran("000508", "首次报单") + "</TD>");
        this.Builder.Append("<TD align=center colSpan=3>" + GetTran("000614", "再次报单") + "</TD>");
        this.Builder.Append("</TR>");
        this.Builder.Append("<TR height=10>");
        this.Builder.Append("<TD align=center width=80>" + GetTran("000534", "小计") + "</TD>");
        this.Builder.Append("<TD align=center width=80>" + GetTran("000517", "已支付") + "</TD>");
        this.Builder.Append("<TD align=center width=70>" + GetTran("000521", "未支付") + "</TD>");
        this.Builder.Append("<TD align=center width=80>" + GetTran("000534", "小计") + "</TD>");
        this.Builder.Append("<TD align=center width=80>" + GetTran("000517", "已支付") + "</TD>");
        this.Builder.Append("<TD align=center width=70>" + GetTran("000521", "未支付") + "</TD>");
        this.Builder.Append("<TD align=center width=80>" + GetTran("000534", "小计") + "</TD>");
        this.Builder.Append("<TD align=center width=80>" + GetTran("000517", "已支付") + "</TD>");
        this.Builder.Append("<TD align=center width=70>" + GetTran("000521", "未支付") + "</TD>");
        this.Builder.Append("<TD align=center width=80>" + GetTran("000534", "小计") + "</TD>");
        this.Builder.Append("<TD align=center width=80>" + GetTran("000517", "已支付") + "</TD>");
        this.Builder.Append("<TD align=center width=70>" + GetTran("000521", "未支付") + "</TD>");
        this.Builder.Append("</TR>");

        getdtColumntotal(dt);
        this.Builder.Append("<tr height=20><Td width=30>" + space + "</td>");
        this.Builder.Append("<td width=70 align=center><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + GetTran("000630", "合计") + "</font></td>");

        this.Builder.Append("<td width=80 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal[0].ToString()) + "</font></td>");
        this.Builder.Append("<td width=80 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal[1].ToString()) + "</font></td>");
        this.Builder.Append("<td width=70 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal[2].ToString()) + "</font></td>");
        this.Builder.Append("<td width=80 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal[3].ToString()) + "</font></td>");
        this.Builder.Append("<td width=80 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal[4].ToString()) + "</font></td>");
        this.Builder.Append("<td width=70 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal[5].ToString()) + "</font></td>");
        this.Builder.Append("<td width=80 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal[6].ToString()) + "</font></td>");
        this.Builder.Append("<td width=80 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal[7].ToString()) + "</font></td>");
        this.Builder.Append("<td width=70 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal[8].ToString()) + "</font></td>");
        this.Builder.Append("<td width=80 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal[9].ToString()) + "</font></td>");
        this.Builder.Append("<td width=80 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal[10].ToString()) + "</font></td>");
        this.Builder.Append("<td width=70 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: black\">" + getstr(htColumntotal[11].ToString()) + "</font></td>");

        this.Builder.Append("</tr>" + "\n");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            this.Builder.Append("<tr height=20>");
            this.Builder.Append("<td width=30 align=center>" + (i + 1).ToString() + "</td>");
            this.Builder.Append("<td width=70 align=center>" + dt.Rows[i][Flag].ToString() + "</td>");

            this.Builder.Append("<td width=80 align=right >" + getstr(dt.Rows[i]["LFbaodan"].ToString()) + "</td>");
            this.Builder.Append("<td width=80 align=right >" + getstr(dt.Rows[i]["LFCheckOut"].ToString()) + "</td>");
            this.Builder.Append("<td width=70 align=right >" + getstr(dt.Rows[i]["LFNCheckOut"].ToString()) + "</td>");
            this.Builder.Append("<td width=80 align=right >" + getstr(dt.Rows[i]["LAbaodan"].ToString()) + "</td>");
            this.Builder.Append("<td width=80 align=right >" + getstr(dt.Rows[i]["LACheckOut"].ToString()) + "</td>");
            this.Builder.Append("<td width=70 align=right >" + getstr(dt.Rows[i]["LANCheckOut"].ToString()) + "</td>");
            this.Builder.Append("<td width=80 align=right >" + getstr(dt.Rows[i]["BFbaodan"].ToString()) + "</td>");
            this.Builder.Append("<td width=80 align=right >" + getstr(dt.Rows[i]["BFCheckOut"].ToString()) + "</td>");
            this.Builder.Append("<td width=70 align=right >" + getstr(dt.Rows[i]["BFNCheckOut"].ToString()) + "</td>");
            this.Builder.Append("<td width=80 align=right >" + getstr(dt.Rows[i]["BAbaodan"].ToString()) + "</td>");
            this.Builder.Append("<td width=80 align=right >" + getstr(dt.Rows[i]["BACheckOut"].ToString()) + "</td>");
            this.Builder.Append("<td width=70 align=right >" + getstr(dt.Rows[i]["BANCheckOut"].ToString()) + "</td>");

            this.Builder.Append("</tr>" + "\n");
        }

        this.Builder.Append("</table>");
        return this.Builder.ToString();
    }
}
