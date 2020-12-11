using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using BLL.CommonClass;

public partial class Company_MemberReport2 : BLL.TranslationBase 
{
    protected string msg = "";
    protected string msg1 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now); //设置缓存的到期时间
        Response.Cache.SetCacheability(HttpCacheability.NoCache); //清理缓存
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.ReportMemberReport);
        if (!IsPostBack)
        {
            this.DatePicker1.Text = CommonDataBLL.GetDateBegin();
            this.DatePicker2.Text = CommonDataBLL.GetDateEnd();
            btn_view_Click(null, null);
        }

        Translations();
    }
    /// <summary>
    /// 翻译
    /// </summary>
    private void Translations()
    {
        this.TranControls(this.rbtn_type,
               new string[][]{
                    new string []{"000109","省份"}
                });

        this.TranControls(this.ddl_image,
               new string[][]{
                    new string []{"000529","饼图"},
                    new string []{"000531","柱形图"}
                });

        this.Btn_Countrysearch.Text = GetTran("007575","按国家汇总");
        this.TranControls(this.btn_view, new string[][] { new string[] { "000566", "显 示" } });
        this.TranControls(this.Btn_Citysearch, new string[][] { new string[] { "001441", "按省份汇总" } });
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
    /// <summary>
    /// 从存储过程里获取数据并存储到ArrayList中去
    /// </summary>
    /// <returns></returns>
    private ArrayList ConstructData()
    {
        int top = 9;
        bool need = true;
        decimal other = 0;
        string column = "";
        string result = "";
        DataTable table = new DataTable();
        ArrayList coll = new ArrayList();

        SqlParameter[] param ={
									 new SqlParameter("@BeginDate",SqlDbType.DateTime),
									 new SqlParameter("@EndDate",SqlDbType.DateTime)
								 };
        param[0].Value = Convert.ToDateTime(Session["Begin"].ToString()).ToUniversalTime();
        param[1].Value = Convert.ToDateTime(Session["End"].ToString()).AddDays(1).ToUniversalTime();
        if (this.rbtn_type.SelectedIndex ==0)
        {
            column = "province";
            table = DAL.DBHelper.ExecuteDataTable("M_info_getDatabyArea", param, CommandType.StoredProcedure);

            result = "Bnum";

        }

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

            coll.Add(new Item(dv[i][column].ToString(), Convert.ToDecimal(dv[i][result].ToString())));
        }
        if (need)
        {
            for (int i = top; i < dv.Count; i++)
            {
                other = other + Convert.ToDecimal(dv[i][result].ToString());
            }
            coll.Add(new Item(GetTran("000470", "其它"), other));
        }
        return coll;
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
        showChat();
    }
    /// <summary>
    /// 图形分析
    /// 图形分析
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_graph_Click(object sender, EventArgs e)
    {
        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            Response.Write("<script language='javascript'>alert('" + GetTran("001377", "请输入要查询的日期区间") + "!')</script>");
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

            Response.Write("<script lanagage='javascript'>window.open('MemberGraph.aspx?Flag=1')</script>");
        }
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

        //Chart1.DataSource = ConstructData();

        if (this.ddl_image.SelectedValue == "1")
        {
            msg = "<script language='javascript'>var data = " + ConstructData1() + " $('#divpiechart').show();$('#divstackchart').hide();window.onload = yuan();</script>";
            //Chart1.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(209, 237, 254);

            //Chart1.Series["Series1"].ChartType = SeriesChartType.Pie;
            //Chart1.Series["Series1"].Label = "#VALX:#PERCENT{P}";
            //Chart1.Series["Series1"].LegendText = "#VALX";
            //Chart1.Series["Series1"].ToolTip = "#VALX: #VAL";
            //Chart1.Series["Series1"].PostBackValue = "#INDEX";
            //Chart1.Series["Series1"].LegendPostBackValue = "#INDEX";
            //Chart1.Series["Series1"].BorderColor = Color.DarkGray;
        }
        
            if (this.ddl_image.SelectedValue == "2")
            {

            msg1 = "<script language='javascript'>" + ConstructData2() + " $('#divstackchart').show();$('#divpiechart').hide();window.onload = zhu();</script>";
            //Chart1.Series["Series1"].ChartType = SeriesChartType.Column;

            //Chart1.Series["Series1"].Label = "#VAL";
            //Chart1.Series["Series1"].Points.DataBind(ConstructData(), "Text", "Value", "");
            //Chart1.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(100, 209, 237, 254);

            //Chart1.Legends[0].Enabled = false;
        }
        //Chart1.Series["Series1"].CustomProperties = "PieLabelStyle=Outside";
        //Chart1.Series["Series1"].XValueMember = "Text";
        //Chart1.Series["Series1"].YValueMembers = "Value";

        //Chart1.DataBind();

        //if (this.ddl_image.SelectedValue == "1")
        //{
        //    int pointIndex = 1;

        //    if (pointIndex >= 0 && pointIndex < Chart1.Series["Series1"].Points.Count)
        //    {
        //        Chart1.Series["Series1"].Points[pointIndex].CustomProperties += "Exploded=true";
        //    }
        //}
    }
    /// <summary>
    /// 图形的单机事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Chart1_Click(object sender, ImageMapEventArgs e)
    {
        //Chart1.DataSource = ConstructData();
        //Chart1.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(209, 237, 254);

        //Chart1.Series["Series1"].ToolTip = "#VALX: #VAL";
        //Chart1.Series["Series1"].Label = "#VALX:#PERCENT{P}";
        //Chart1.Series["Series1"].LegendText = "#VALX";
        //Chart1.Series["Series1"].PostBackValue = "#INDEX";
        //Chart1.Series["Series1"].LegendPostBackValue = "#INDEX";
        //Chart1.Series["Series1"].BorderColor = Color.DarkGray;
        //Chart1.Series["Series1"].XValueMember = "Text";
        //Chart1.Series["Series1"].YValueMembers = "Value";
        //Chart1.Series["Series1"].CustomProperties = "PieLabelStyle=Outside";
        //Chart1.DataBind();

        //int pointIndex = int.Parse(e.PostBackValue);

        //if (pointIndex >= 0 && pointIndex < Chart1.Series["Series1"].Points.Count)
        //{
        //    Chart1.Series["Series1"].Points[pointIndex].CustomProperties += "Exploded=true";
        //}

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
        showChat();
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

    private string ConstructData2()
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
}