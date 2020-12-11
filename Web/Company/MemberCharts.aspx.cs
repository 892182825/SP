using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.CommonClass;

public partial class Company_MemberCharts : BLL.TranslationBase
{
    protected string msg = "";
    protected string msg1 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //stackchart.Visible = true;
            //piechart.Visible = false; 
            this.DatePicker1.Text = CommonDataBLL.GetDateBegin();
            this.DatePicker2.Text = CommonDataBLL.GetDateEnd();
            btn_view_Click(null, null);
        }
    }
 
    /// <summary>
    /// 显示
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_view_Click(object sender, EventArgs e)
    {
        showChat();
    }

    /// <summary>
    /// 显示图形
    /// </summary>
    public void showChat()
    {
        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            Response.Write("<script language='javascript'>alert('" + GetTran("001377", "请输入要查询的日期区间") + "!')</script>");
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
                Response.Write("<script language='javascript'>alert('" + GetTran("001379", "日期格式不正确") + "!')</script>");
                return;
            }
            Session["Begin"] = this.DatePicker1.Text;
            Session["End"] = this.DatePicker2.Text;
        }
        if (this.ddl_image.SelectedValue == "1")
        {
            //stackchart.Visible = false;
            //piechart.Visible = true;

            msg = "<script language='javascript'>var data = " + ConstructData1() + " $('#divpiechart').show();$('#divstackchart').hide();window.onload = yuan();</script>";

           
        }
        if (this.ddl_image.SelectedValue == "2")
        {
            //stackchart.Visible = true;
            //piechart.Visible = false;
            msg1 = "<script language='javascript'>" + ConstructData() + " $('#divstackchart').show();$('#divpiechart').hide();window.onload = zhu();</script>";
        }
    }

    /// <summary>
    /// 按国家汇总
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_Countrysearch_Click(object sender, EventArgs e)
    {
        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001377", "请输入要查询的日期区间") + "!');</script>");

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
            Session["Begin"] = this.DatePicker1.Text;
            Session["End"] = this.DatePicker2.Text;

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script lanagage='javascript'>window.open('MemberArea.aspx?Flag=Country')</script>");
        }
    }

    /// <summary>
    /// 按省份汇总
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_Citysearch_Click(object sender, EventArgs e)
    {
        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001377", "请输入要查询的日期区间") + "!');</script>");

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
            Session["Begin"] = this.DatePicker1.Text;
            Session["End"] = this.DatePicker2.Text;

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script lanagage='javascript'>window.open('MemberArea.aspx?Flag=Province')</script>");
        }
    }

    protected void btn_graph_Click(object sender, EventArgs e)
    {
    }

    private string ConstructData()
    {
        string coll = "var data1=[";
        string CBLL = "var data2=[";

        //var d1 = [];
        int top = 9;
        bool need = true;
        decimal other = 0;
        string column = "";
        string result = "";
        DataTable table = new DataTable();
        //string coll = "[";
        SqlParameter[] param ={
                                     new SqlParameter("@BeginDate",SqlDbType.DateTime),
                                     new SqlParameter("@EndDate",SqlDbType.DateTime)
                                 };
        param[0].Value = 
            Convert.ToDateTime(Session["Begin"].ToString()).ToUniversalTime();
        param[1].Value = 
        Convert.ToDateTime(Session["End"].ToString()).AddDays(1).ToUniversalTime();

        column = "province";
            table = DAL.DBHelper.ExecuteDataTable("M_info_getDatabyArea", param, CommandType.StoredProcedure);

            result = "Bnum";


        int rows = table.Rows.Count;
        DataView dv = table.DefaultView;

        dv.Sort = result + "" + " Desc";

        if (top >= dv.Count)
        {
            need = false;
            top = dv.Count;
        }
        for (int i = 0; i < top; i++)
        {
            
            coll += "[" + i + ",'" + dv[i][column].ToString() + "'],";
            CBLL += "[" + i + "," + Convert.ToDecimal(dv[i][result].ToString()) + "],";
        }
        if (need)
        {
            for (int i = top; i < dv.Count; i++)
            {
                other = other + Convert.ToDecimal(dv[i][result].ToString());
            }
            coll += "{label: '" + GetTran("000470", "其它") + "',data: '" + other + "'},";
           
        }
        coll = coll.Substring(0, coll.Length - 1);
        CBLL = CBLL.Substring(0, CBLL.Length - 1);
        coll += "];";
        CBLL += "];";
        coll += CBLL;
        return coll;
    }

    private string ConstructData1()
    {
        int top = 9;
        bool need = true;
        decimal other = 0;
        string column = "";
        string result = "";
        DataTable table = new DataTable();
        //ArrayList coll = new ArrayList();
        string coll = "[";
        SqlParameter[] param ={
                                     new SqlParameter("@BeginDate",SqlDbType.DateTime),
                                     new SqlParameter("@EndDate",SqlDbType.DateTime)
                                 };
        param[0].Value = //Convert.ToDateTime("2018-04-15 00:00:00");
        Convert.ToDateTime(Session["Begin"].ToString()).ToUniversalTime();
        param[1].Value = //Convert.ToDateTime("2018-05-16 00:00:00");
        Convert.ToDateTime(Session["End"].ToString()).AddDays(1).ToUniversalTime();

        column = "province";
        table = DAL.DBHelper.ExecuteDataTable("M_info_getDatabyArea", param, CommandType.StoredProcedure);

        result = "Bnum";


        int rows = table.Rows.Count;
        DataView dv = table.DefaultView;

        dv.Sort = result + "" + " Desc";

        if (top >= dv.Count)
        {
            need = false;
            top = dv.Count;
        }
        for (int i = 0; i < top; i++)
        {
            
            coll += "{label: '" + dv[i][column].ToString() + "',data: '" + Convert.ToDecimal(dv[i][result].ToString()) + "'},";
        }
        if (need)
        {
            for (int i = top; i < dv.Count; i++)
            {
                other = other + Convert.ToDecimal(dv[i][result].ToString());
            }
            
            coll += "{label: '" + GetTran("000470", "其它") + "',data: '" + other + "'},";
        }
        coll = coll.Substring(0, coll.Length - 1);
        coll += "];";
        return coll;
    }
    public class Item
    {
        public Item(string text, decimal value)
        {
            this._text = text;
            this._value = value;
        }

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
        }

        private decimal _value;
        public decimal Value
        {
            get
            {
                return _value;
            }
        }
    }
}