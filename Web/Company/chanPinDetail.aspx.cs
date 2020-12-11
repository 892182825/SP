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
using BLL.other.Company;
using Model.Other;
using BLL.CommonClass;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

public partial class Company_chanPinDetail : BLL.TranslationBase
{      
    protected string storeId;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Permissions.CheckManagePermission(EnumCompanyPermission.ReportStockReport2);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            //btn_view_Click(null, null);
        }
        //tb_Chart.Visible = false;
        Translations_More();
    }

    /// <summary>
    /// Translation
    /// </summary>
    protected void Translations_More()
    {
        TranControls(btnStoreInfo, new string[][] { new string[] { "000154 ", "按服务机构汇总" }});
        TranControls(btn_view, new string[][] { new string[] { "000177 ", "显示" } });
        TranControls(ddl_image, new string[][] { new string[] { "000199", "店铺库存饼图" }, new string[] { "000208", "店铺库存柱形图" } });

    }

    protected bool StoreIdIsExist()
    {
        if (this.txtStoreId.Text.Trim().Length < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000133","请输入要查询的店铺编号!")));
            return false;
        }

        else
        {
            storeId = txtStoreId.Text.Trim();
            //Judage the store whether exists beafore search the information about store by storeId
            int getCount = ReportBLL.StoreIdIsExistByStoreId(storeId);
            //When exist
            if (getCount > 0)
            {
                lblStoreId.Text = storeId;
                Session["Condition"] = storeId;
                tb_Chart.Visible = true;               
                return true;
            }

            //When no exist
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000142")));
                return false;
            }
        }
    }


    protected void btnStoreInfo_Click(object sender, System.EventArgs e)
    {
        if (StoreIdIsExist())
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>window.open('ProductStock1.aspx?Flag=Store')</script>");
            btn_view_Click(null, null);
        }
    }

    protected void btn_view_Click(object sender, System.EventArgs e)
    {        
        showChart();
    }

    //protected void BindData()
    //{
    //    if (StoreIdIsExist())
    //    {
    //        if (this.ddl_image.SelectedValue == "1")
    //        {
    //            displayChart.Series["Series1"].Points.DataBind(ReportFormsBLL.ConstructData_II(Session["condition"].ToString()), "Text", "Value", "LegendText=Text");
    //            displayChart.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(209, 237, 254);

    //            displayChart.Series["Series1"].ChartType = SeriesChartType.Pie;
    //            displayChart.Series["Series1"].Label = "#VALX:#PERCENT{P}";
    //            displayChart.Series["Series1"].ToolTip = "#VALX: #VAL";
    //            //displayChart.Series["Series1"].LegendToolTip = "#PERCENT";
    //            displayChart.Series["Series1"].PostBackValue = "#INDEX";
    //            displayChart.Series["Series1"].LegendPostBackValue = "#INDEX";
    //            displayChart.Series["Series1"].BorderColor = Color.DarkGray;

    //            //chart = new PieChart();
    //            //chart.ScriptUrl = "Scripts/pieChart.js";
    //            //chart.DataSource = this.ConstructData();
    //            //chart.ReportTitle = "店铺分布柱形图分析";
    //            //chart.DataTextField = "Text";
    //            //chart.DataValueField = "Value";
    //            //chart.Width = 800;
    //            //chart.Height = 400;
    //            //chart.Left = 0;
    //            //chart.Top = 72;
    //            //chart.DataBind();

    //            //this.Label1.Controls.Add(chart);
    //        }
    //        else
    //            if (this.ddl_image.SelectedValue == "2")
    //            {
    //                displayChart.Series["Series1"].ChartType = SeriesChartType.Column;

    //                displayChart.Series["Series1"].Label = "#VAL";
    //                displayChart.Series["Series1"].Points.DataBind(ReportFormsBLL.ConstructData_II(Session["condition"].ToString()), "Text", "Value", "");
    //                //chart = new ColumnChart();
    //                //chart.ScriptUrl = "Scripts/columnChart.js";
    //                //chart.DataSource = this.ConstructData();
    //                //chart.ReportTitle = "店铺分布柱形图分析";
    //                //chart.DataTextField = "Text";
    //                //chart.DataValueField = "Value";
    //                //chart.Width = 800;
    //                //chart.Height = 400;
    //                //chart.Left = 0;
    //                //chart.Top = 72;
    //                //chart.DataBind();
    //                displayChart.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(100, 209, 237, 254);
    //                //this.Label1.Controls.Add(chart);

    //                displayChart.Legends[0].Enabled = false;
    //            }
            
    //        displayChart.Series["Series1"].CustomProperties = "PieLabelStyle=Outside";
    //        displayChart.Series["Series1"].XValueMember = "Text";
    //        displayChart.Series["Series1"].YValueMembers = "Value";
    //        displayChart.DataBind();
           
    //    }
    //}

    protected void showChart()
    {
       // BindData();


        //if (this.ddl_image.SelectedValue == "1")
        //{
        //    int pointIndex = 1;

        //    if (pointIndex >= 0 && pointIndex < displayChart.Series["Series1"].Points.Count)
        //    {
        //        displayChart.Series["Series1"].Points[pointIndex].CustomProperties += "Exploded=true";
        //    }
        //}
    }

    protected void ddl_image_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_view_Click(null,null);
    }
    protected void displayChart_Click(object sender, ImageMapEventArgs e)
    {
        //displayChart.DataSource = ReportFormsBLL.ConstructData_II(Session["condition"].ToString());
        //displayChart.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(209, 237, 254);
        ////displayChart.Series["Series1"].ChartType = SeriesChartType.Pie;

        ////displayChart.Series["Series1"].Label = "#VAL";
        ////displayChart.Series["Series1"].Points.DataBind(ConstructData(), "Text", "Value", "");
        //displayChart.Series["Series1"].ToolTip = "#VALX: #VAL";
        ////displayChart.Series["Series1"].LegendToolTip = "#PERCENT";
        //displayChart.Series["Series1"].Label = "#VALX:#PERCENT{P}";
        //displayChart.Series["Series1"].LegendText = "#VALX";
        //displayChart.Series["Series1"].PostBackValue = "#INDEX";
        //displayChart.Series["Series1"].LegendPostBackValue = "#INDEX";
        //displayChart.Series["Series1"].BorderColor = Color.DarkGray;
        //displayChart.Series["Series1"].XValueMember = "Text";
        //displayChart.Series["Series1"].YValueMembers = "Value";
        //displayChart.Series["Series1"].CustomProperties = "PieLabelStyle=Outside";
        //displayChart.DataBind();

        //int pointIndex = int.Parse(e.PostBackValue);

        //if (pointIndex >= 0 && pointIndex < displayChart.Series["Series1"].Points.Count)
        //{
        //    displayChart.Series["Series1"].Points[pointIndex].CustomProperties += "Exploded=true";
        //}

    }
}
