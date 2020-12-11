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

public partial class Company_OrderProductReport : BLL.TranslationBase
{
    protected DataTable dt;
    protected StringBuilder Builder;

    private void Page_Load(object sender, System.EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        // 在此处放置用户代码以初始化页面
        dt = new DataTable();
        Builder = new StringBuilder();
        this.lbl_Begin.Text = Session["Begin"].ToString();
        this.lbl_End.Text = Session["End"].ToString();
        this.lbl_message.Visible = false;
        this.lbl_maketime.Text = DateTime.Now.ToString();
        if (!this.IsPostBack)
        {
            getdata();
            this.Literal1.Text = this.Bind();
        }
    }
    //取数据源
    private void getdata()
    {
        //SqlParameter[] param ={
        //                            new SqlParameter("@BeginDate",SqlDbType.DateTime),
        //                            new SqlParameter("@EndDate",SqlDbType.DateTime),
        //                            new SqlParameter("@qishu",SqlDbType.Int)
        //                       };
        //param[0].Value = Session["Begin"].ToString();
        //param[1].Value = Session["End"].ToString();
        //param[2].Value = 0;
        //dt = Standard.DBHelper.ExecuteDataTable("D_OrderDetail_getData", param, CommandType.StoredProcedure);

        dt=ReportFormsBLL.GetOrderCollect_II(Session["Begin"].ToString(), Session["End"].ToString());

        if (dt.Rows.Count < 1)
        {
            this.lbl_message.Text = GetTran("001946", "没有相关数据!!");
            this.lbl_message.Visible = true;
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
    string[] columns ={"ldinghuo","lcheckout",
							  "lnotcheckout","lsend","lnotsend","ltuihuo","lvalid","lnotvalid","bdinghuo","bcheckout","bnotcheckout","bsend","bnotsend","btuihuo","bvalid",
							   "bnotvalid"};
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
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000587", "现有列名与页面中的列不匹配") + "');</script>");
                    
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

        this.Builder.Append("<table cellSpacing=0 cellPadding=0 width=100% class=colortest  style='color:rgb(0,85,117);'>");

        this.Builder.Append("<TR height=10>");
        this.Builder.Append("<TD align=center width=30 rowSpan=3 >" + GetTran("000012", "序号") + "</TD>");
        this.Builder.Append("<TD align=center width=60 rowSpan=3>" + GetTran("000558", "产品编号") + "</TD>");
        this.Builder.Append("<TD align=center width=70 rowSpan=3 >" + GetTran("000501", "产品名称") + "</TD>");
        this.Builder.Append("<TD align=center colSpan=8>" + GetTran("002233", "累计订货情况") + "</TD>");
        this.Builder.Append("<TD align=center colSpan=8>" + GetTran("002232", "本期订货情况") + "</TD>");
        this.Builder.Append("</TR>");
        this.Builder.Append("<TR height=10>");
        this.Builder.Append("<TD rowspan=2 align=center width=70>" + GetTran("002188", "订货") + "</TD>");
        this.Builder.Append("<TD colspan=2 align=center>" + GetTran("000938", "支付") + "</TD>");
        this.Builder.Append("<TD colspan=2 align=center>" + GetTran("001340", "发货") + "</TD>");
        this.Builder.Append("<TD colspan=3 align=center>" + GetTran("002224", "退货") + "</TD>");
        this.Builder.Append("<TD rowspan=2 align=center width=70>" + GetTran("002188", "订货") + "</TD>");
        this.Builder.Append("<TD colspan=2 align=center>" + GetTran("000938", "支付") + "</TD>");
        this.Builder.Append("<TD colspan=2 align=center>" + GetTran("001340", "发货") + "</TD>");
        this.Builder.Append("<TD colspan=3 align=center>" + GetTran("002224", "退货") + "</TD>");
        this.Builder.Append("</TR>");
        this.Builder.Append("<TR height=10>");
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
        this.Builder.Append("<tr height=20><td width=30 align=center>" + space + "</td>");
        this.Builder.Append("<Td width=60>" + space + "</td>");
        this.Builder.Append("<Td width=70 align=center><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; \">" + "合计" + "</font></td>");

        this.Builder.Append("<td width=70 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; color:rgb(0,85,117);\">" + getstr(htColumntotal[0].ToString()) + "</font></td>");
        this.Builder.Append("<td width=70 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: rgb(0,85,117);\">" + getstr(htColumntotal[1].ToString()) + "</font></td>");
        this.Builder.Append("<td width=50 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: rgb(0,85,117);\">" + getstr(htColumntotal[2].ToString()) + "</font></td>");
        this.Builder.Append("<td width=70 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: rgb(0,85,117);\">" + getstr(htColumntotal[3].ToString()) + "</font></td>");
        this.Builder.Append("<td width=50 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: rgb(0,85,117);\">" + getstr(htColumntotal[4].ToString()) + "</font></td>");
        this.Builder.Append("<td width=40 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: rgb(0,85,117);\">" + getstr(htColumntotal[5].ToString()) + "</font></td>");
        this.Builder.Append("<td width=40 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: rgb(0,85,117);\">" + getstr(htColumntotal[6].ToString()) + "</font></td>");
        this.Builder.Append("<td width=40 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: rgb(0,85,117);\">" + getstr(htColumntotal[7].ToString()) + "</font></td>");

        this.Builder.Append("<td width=70 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: rgb(0,85,117);\">" + getstr(htColumntotal[8].ToString()) + "</font></td>");
        this.Builder.Append("<td width=70 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: rgb(0,85,117);\">" + getstr(htColumntotal[9].ToString()) + "</font></td>");
        this.Builder.Append("<td width=50 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: rgb(0,85,117);\">" + getstr(htColumntotal[10].ToString()) + "</font></td>");
        this.Builder.Append("<td width=70 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: rgb(0,85,117);\">" + getstr(htColumntotal[11].ToString()) + "</font></td>");
        this.Builder.Append("<td width=50 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: rgb(0,85,117);\">" + getstr(htColumntotal[12].ToString()) + "</font></td>");
        this.Builder.Append("<td width=40 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: rgb(0,85,117);\">" + getstr(htColumntotal[13].ToString()) + "</font></td>");
        this.Builder.Append("<td width=40 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: rgb(0,85,117);\">" + getstr(htColumntotal[14].ToString()) + "</font></td>");
        this.Builder.Append("<td width=40 align=right><FONT face=\"宋体\" style=\"FONT-WEIGHT: bold; COLOR: rgb(0,85,117);\">" + getstr(htColumntotal[15].ToString()) + "</font></td>");
        this.Builder.Append("</tr>" + "\n");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            this.Builder.Append("<tr height=20>");
            this.Builder.Append("<td width=30 align=center style='color:rgb(0,85,117);'>" + (i + 1).ToString() + "</td>");
            this.Builder.Append("<td width=60 align=center style='color:rgb(0,85,117);'>" + dt.Rows[i]["productcode"].ToString() + "</td>");
            this.Builder.Append("<td width=70 align=center style='color:rgb(0,85,117);'>" + dt.Rows[i]["productname"].ToString() + "</td>");

            this.Builder.Append("<td width=70 align=right  style='color:rgb(0,85,117);'>" + getstr(dt.Rows[i]["Ldinghuo"].ToString()) + "</td>");
            this.Builder.Append("<td width=70 align=right  style='color:rgb(0,85,117);'>" + getstr(dt.Rows[i]["LCheckOut"].ToString()) + "</td>");
            this.Builder.Append("<td width=50 align=right  style='color:rgb(0,85,117);'>" + getstr(dt.Rows[i]["LNotCheckOut"].ToString()) + "</td>");
            this.Builder.Append("<td width=70 align=right style='color:rgb(0,85,117);' >" + getstr(dt.Rows[i]["Lsend"].ToString()) + "</td>");
            this.Builder.Append("<td width=50 align=right style='color:rgb(0,85,117);' >" + getstr(dt.Rows[i]["LnotSend"].ToString()) + "</td>");
            this.Builder.Append("<td width=40 align=right style='color:rgb(0,85,117);' >" + getstr(dt.Rows[i]["Ltuihuo"].ToString()) + "</td>");
            this.Builder.Append("<td width=40 align=right  style='color:rgb(0,85,117);'>" + getstr(dt.Rows[i]["Lvalid"].ToString()) + "</td>");
            this.Builder.Append("<td width=40 align=right  style='color:rgb(0,85,117);'>" + getstr(dt.Rows[i]["LNotValid"].ToString()) + "</td>");

            this.Builder.Append("<td width=70 align=right  style='color:rgb(0,85,117);'>" + getstr(dt.Rows[i]["Bdinghuo"].ToString()) + "</td>");
            this.Builder.Append("<td width=70 align=right  style='color:rgb(0,85,117);'>" + getstr(dt.Rows[i]["BCheckOut"].ToString()) + "</td>");
            this.Builder.Append("<td width=50 align=right  style='color:rgb(0,85,117);'>" + getstr(dt.Rows[i]["BNotCheckOut"].ToString()) + "</td>");
            this.Builder.Append("<td width=70 align=right  style='color:rgb(0,85,117);'>" + getstr(dt.Rows[i]["Bsend"].ToString()) + "</td>");
            this.Builder.Append("<td width=50 align=right  style='color:rgb(0,85,117);'>" + getstr(dt.Rows[i]["BNotsend"].ToString()) + "</td>");
            this.Builder.Append("<td width=40 align=right  style='color:rgb(0,85,117);'>" + getstr(dt.Rows[i]["Btuihuo"].ToString()) + "</td>");
            this.Builder.Append("<td width=40 align=right  style='color:rgb(0,85,117);'>" + getstr(dt.Rows[i]["BValid"].ToString()) + "</td>");
            this.Builder.Append("<td width=40 align=right  style='color:rgb(0,85,117);'>" + getstr(dt.Rows[i]["BnotValid"].ToString()) + "</td>");

            this.Builder.Append("</tr>" + "\n");

        }



        this.Builder.Append("</table>");
        return this.Builder.ToString();
    }

}



