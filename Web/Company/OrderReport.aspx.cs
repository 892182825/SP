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
using ReportControl.Demo;
using BLL.other.Company;
using Model.Other;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;


public partial class Company_OrderReport : BLL.TranslationBase
{
    public void Page_Load(object sender, System.EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        this.Btn_Dsearch.Click += new System.EventHandler(this.Btn_Dsearch_Click);
        this.Btn_Citysearch.Click += new System.EventHandler(this.Btn_Citysearch_Click);
        this.Btn_productsearch.Click += new System.EventHandler(this.Btn_productsearch_Click);
        this.btn_graph.Click += new System.EventHandler(this.btn_graph_Click);
        this.btn_view.Click += new System.EventHandler(this.btn_view_Click);
        this.Load += new System.EventHandler(this.Page_Load);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            Response.Cache.SetExpires(DateTime.Now);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Permissions.CheckManagePermission(EnumCompanyPermission.ReportOrderReport);
            Session["language"] = LanguageBLL.GetDefaultLanguageTableName();
            Session["LanguegeSelect"] = LanguageBLL.GetDefaultlLanguageName();

            DateTime dt = DateTime.Now;
            string t = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();

            this.DatePicker1.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            this.DatePicker2.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Session["Begin"] = this.DatePicker1.Text;
            Session["End"] = this.DatePicker2.Text;

            showChat();
        }

        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.rbtn_type, new string[][] { 
                                 new string[] { "000388", "店铺" },
                                 new string[] { "000109", "省份" }, 
                                 new string[] { "002186", "产品" } 
        
                         });
        this.TranControls(this.ddl_item, new string[][] { 
                                 new string[] { "002188", "订货" },
                                 new string[] { "000517", "已支付" },
                                 new string[] { "000521", "未支付" }

                         });
        this.TranControls(this.ddl_image, new string[][] { 
                                 new string[] { "000529", "饼图" },
                                 new string[] { "000531", "柱形图" }

                         });


        this.TranControls(this.Btn_Dsearch, new string[][] { 
                                 new string[] { "000154", "按服务机构汇总" },
                         });

        this.TranControls(this.Btn_Citysearch, new string[][] { 
                                 new string[] { "000493", "省市汇总" },
                         });
        this.TranControls(this.Btn_productsearch, new string[][] { 
                                 new string[] { "000495", "产品汇总" },
                         });
        this.TranControls(this.btn_graph, new string[][] { 
                                 new string[] { "000298", "图形分析" },
                         });
        this.TranControls(this.btn_view, new string[][] { 
                                 new string[] { "000566", "显 示" },
                         });

    }


    public void Btn_Dsearch_Click(object sender, System.EventArgs e)
    {
        Session["Begin"] = this.DatePicker1.Text;
        Session["End"] = this.DatePicker2.Text;

        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000465", "请输入要查询的日期区间！") + "!')</script>");
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
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000468", "日期格式不正确！") + "!')</script>");
                return;
            }
            Session["Begin"] = this.DatePicker1.Text;
            Session["End"] = this.DatePicker2.Text;

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script lanagage='javascript'>window.open('OrderSumReport.aspx?Flag=store')</script>");
        }
        showChat();

    }

    public void Btn_Citysearch_Click(object sender, System.EventArgs e)
    {
        Session["Begin"] = this.DatePicker1.Text;
        Session["End"] = this.DatePicker2.Text;

        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000465", "请输入要查询的日期区间！") + "!')</script>");
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
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000468", "日期格式不正确！") + "!')</script>");
                return;
            }
            Session["Begin"] = this.DatePicker1.Text;
            Session["End"] = this.DatePicker2.Text;

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script lanagage='javascript'>window.open('OrderSumReport.aspx?Flag=city')</script>");
        }
        showChat();
    }

    public void Btn_productsearch_Click(object sender, System.EventArgs e)
    {
        Session["Begin"] = this.DatePicker1.Text;
        Session["End"] = this.DatePicker2.Text;

        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000465", "请输入要查询的日期区间！") + "!')</script>");
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
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000468", "日期格式不正确！") + "!')</script>");
                return;
            }
            Session["Begin"] = this.DatePicker1.Text;
            Session["End"] = this.DatePicker2.Text;

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script lanagage='javascript'>window.open('OrderProductReport.aspx?Flag=1')</script>");
        }
        showChat();
    }

    public void btn_graph_Click(object sender, System.EventArgs e)
    {
        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000465", "请输入要查询的日期区间！") + "!')</script>");
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
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000468", "日期格式不正确！") + "!')</script>");
                return;
            }
            Session["Begin"] = this.DatePicker1.Text;
            Session["End"] = this.DatePicker2.Text;

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script lanagage='javascript'>window.open('OrderGraph.aspx?Flag=1')</script>");
        }
    }

    public void btn_view_Click(object sender, System.EventArgs e)
    {
        showChat();
    }

    public void showChat()
    {
        Translations();
        Session["Begin"] = this.DatePicker1.Text;
        Session["End"] = this.DatePicker2.Text;

        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000465", "请输入要查询的日期区间！") + "!')</script>");
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
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000468", "日期格式不正确！") + "!')</script>");
                return;
            }
            Session["Begin"] = this.DatePicker1.Text;
            Session["End"] = this.DatePicker2.Text;
        }

        //绑定
        ArrayList arraydate = ReportFormsBLL.ConstructData(rbtn_type.SelectedIndex, Session["Begin"].ToString(), Session["End"].ToString(), ddl_item.SelectedValue);

        //Chart1.DataSource = arraydate;

        //Chart1.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(209, 237, 254);

        //if (this.ddl_image.SelectedValue == "1")//饼图
        //{
        //    Chart1.Series["Series1"].ChartType = SeriesChartType.Pie;

        //    Chart1.Series["Series1"].BorderColor = Color.DarkGray;

        //    Chart1.Series["Series1"].Label = "#VALX:#PERCENT{P}";
        //    Chart1.Series["Series1"].LegendText = "#VALX";
        //    Chart1.Series["Series1"].PostBackValue = "#INDEX";
        //    Chart1.Series["Series1"].LegendPostBackValue = "#INDEX";
        //}
        //else if (this.ddl_image.SelectedValue == "2") //柱形图
        //{
        //    Chart1.Series["Series1"].ChartType = SeriesChartType.Column;

        //    Chart1.Series["Series1"].Label = "#VAL";
        //    Chart1.Series["Series1"].ToolTip = "#VALX  #PERCENT{P1}";

        //    //start
        //    Chart1.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(209, 237, 254);


        //    Chart1.Legends[0].Enabled = false;
        //}
        //Chart1.Series["Series1"].XValueMember = "Text";
        //Chart1.Series["Series1"].YValueMembers = "Value";
        //Chart1.DataBind();
        Translations();


        //if (this.ddl_image.SelectedValue == "1")
        //{
        //    int pointIndex = 1;

        //    if (pointIndex >= 0 && pointIndex < Chart1.Series["Series1"].Points.Count)
        //    {
        //        Chart1.Series["Series1"].Points[pointIndex].CustomProperties += "Exploded=true";
        //    }
        //}
    }

    protected void Chart1_Click(object sender, ImageMapEventArgs e)
    {
        //ArrayList arraydate = ReportFormsBLL.ConstructData(rbtn_type.SelectedIndex, Session["Begin"].ToString(), Session["End"].ToString(), ddl_item.SelectedValue);

        //Chart1.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(209, 237, 254);

        //Chart1.DataSource = arraydate;

        //Chart1.Series["Series1"].ToolTip = "#VALX: #VAL";
        //Chart1.Series["Series1"].Label = "#VALX:#PERCENT{P}";
        //Chart1.Series["Series1"].LegendText = "#VALX";
        //Chart1.Series["Series1"].PostBackValue = "#INDEX";
        //Chart1.Series["Series1"].LegendPostBackValue = "#INDEX";
        //Chart1.Series["Series1"].BorderColor = Color.DarkGray;
        //Chart1.Series["Series1"].XValueMember = "Text";
        //Chart1.Series["Series1"].YValueMembers = "Value";
        ////
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
        Session["Begin"] = this.DatePicker1.Text;
        Session["End"] = this.DatePicker2.Text;

        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000465", "请输入要查询的日期区间！") + "!')</script>");
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
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('" + GetTran("000468", "日期格式不正确！") + "!')</script>");
                return;
            }
            Session["Begin"] = this.DatePicker1.Text;
            Session["End"] = this.DatePicker2.Text;

            ClientScript.RegisterStartupScript(this.GetType(), "", "<script lanagage='javascript'>window.open('OrderSumReport.aspx?Flag=Country')</script>");
        }
        showChat();
    }
}
