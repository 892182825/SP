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
using Model;
using BLL.CommonClass;
using BLL.other.Company;
using Model.Other;
using BLL;
using System.Data.SqlClient;
using ReportControl.Demo;
using Standard;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;


public partial class Company_MoneyReport : BLL.TranslationBase
{
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
    private void Translations()
    {
        this.TranControls(this.ddl_item,
                new string[][]{
                    new string []{"000535","汇款"},
                    new string []{"000391","周转货"},
                    new string []{"000538","报单款"}});
        this.TranControls(this.rbtn_type,
                new string[][]{
                    new string []{"000388","店铺"},
                    new string []{"000109","省份"}});
        this.TranControls(this.ddl_image,
                new string[][]{
                    new string []{"000529","饼图"},
                    new string []{"000531","柱形图"}});
        this.TranControls(this.Btn_Dsearch, new string[][] { new string[] { "000154", "按服务机构汇总" } });
        this.TranControls(this.Btn_Citysearch, new string[][] { new string[] { "000493", "省市汇总" } });
        this.TranControls(this.Button3, new string[][] { new string[] { "000298", "图形分析" } });
        this.TranControls(this.btn_view, new string[][] { new string[] { "000566", "显 示" } });

    }
    protected void Page_Load(object sender, EventArgs e)
    {

        Permissions.ComRedirect(Page, Permissions.redirUrl);
        // 权限设置
        Response.Cache.SetExpires(DateTime.Now);
        Permissions.CheckManagePermission(EnumCompanyPermission.CustomerStoreHuikuanTotal);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        // 在此处放置用户代码以初始化页面
        this.Page.RegisterStartupScript("", "<script lanagage='javascript'></script>"); Translations();
        if (!IsPostBack)
        {
            Session["language"] = LanguageBLL.GetDefaultLanguageTableName();
            Session["LanguegeSelect"] = LanguageBLL.GetDefaultlLanguageName();

            this.DatePicker1.Text = CommonDataBLL.GetDateBegin();
            this.DatePicker2.Text = CommonDataBLL.GetDateEnd();
            Session["Begin"] = this.DatePicker1.Text;
            Session["End"] = (DateTime.Parse(this.DatePicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59)).ToString();
            this.lbl_BeginDate.Text = CommonDataBLL.GetDateBegin();
            this.lbl_EndDate.Text = CommonDataBLL.GetDateEnd();
            btn_view_Click(null, null);
        }
        CommonDataBLL.setlanguage();
    }
    protected ArrayList ConstructData()
    {
        int top = 9;
        bool need = true;
        decimal other = 0;
        string column = "";
        string result = "";
        DataTable table;
        ArrayList coll = new ArrayList();

        if (this.rbtn_type.SelectedIndex == 0)
        {
            column = "remitnumber";
            if (this.ddl_item.SelectedValue == "-1")
            {
                table = CommonDataBLL.GetStoreTotal(DateTime.Parse(this.DatePicker1.Text).ToUniversalTime().ToString(), (DateTime.Parse(this.DatePicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59)).ToUniversalTime().ToString());

                result = "amt";
            }
            else
            {
                table = CommonDataBLL.getDataByStoreid(DateTime.Parse(this.DatePicker1.Text).ToUniversalTime().ToString(), (DateTime.Parse(this.DatePicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59)).ToUniversalTime().ToString());
                if (this.ddl_item.SelectedValue == "0")
                {
                    result = "Eyajin";
                }
                if (this.ddl_item.SelectedValue == "1")
                {
                    result = "Ejiameng";
                }
            }
        }
        else
        {
            column = "province";
            if (this.ddl_item.SelectedValue == "-1")
            {

                table = CommonDataBLL.GetStoreProvince(DateTime.Parse(this.DatePicker1.Text).ToUniversalTime().ToString(), DateTime.Parse(this.DatePicker2.Text).ToUniversalTime().ToString());
                result = "amt";
            }
            else
            {
                table = CommonDataBLL.getDataByCity(DateTime.Parse(this.DatePicker1.Text).ToUniversalTime().ToString(), (DateTime.Parse(this.DatePicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59)).ToUniversalTime().ToString());
                if (this.ddl_item.SelectedValue == "0")
                {
                    result = "Eyajin";
                }
                if (this.ddl_item.SelectedValue == "1")
                {
                    result = "Ejiameng";
                }
                column = "province";
            }
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
    protected void Btn_Dsearch_Click(object sender, System.EventArgs e)
    {
        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000465", "请输入要查询的日期区间！") + "');", true);
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
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000468", "日期格式不正确！") + "');", true);
                return;
            }
            Session["Begin"] = DateTime.Parse(this.DatePicker1.Text).ToUniversalTime().ToString();
            Session["End"] = DateTime.Parse(this.DatePicker2.Text).ToUniversalTime().ToString();
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "window.open('StoreReportDetail.aspx?Flag=store');", true);
        }
        btn_view_Click(null, null);

    }

    protected void Btn_Citysearch_Click(object sender, System.EventArgs e)
    {
        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000465", "请输入要查询的日期区间！") + "');", true);
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
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000468", "日期格式不正确！") + "');", true);
                return;
            }
            Session["Begin"] = DateTime.Parse(this.DatePicker1.Text).ToUniversalTime().ToString();
            Session["End"] = (DateTime.Parse(this.DatePicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59)).ToUniversalTime().ToString();
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "window.open('StoreReportDetail.aspx?Flag=province');", true);

        }
        btn_view_Click(null, null);
    }

    protected void Btn_image_Click(object sender, System.EventArgs e)
    {
        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000465", "请输入要查询的日期区间！") + "');", true);
        }
        else
        {
            try
            {
                DateTime d1 = Convert.ToDateTime(this.DatePicker1.Text);
                DateTime d2 = Convert.ToDateTime(this.DatePicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            catch
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000468", "日期格式不正确！") + "');", true);
                return;
            }
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "window.open('HuikuanGraph.aspx?Flag=1');", true);
        }
    }

    public void showChat()
    {
        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000465", "请输入要查询的日期区间！") + "');", true);
            return;
        }
        else
        {
            try
            {
                DateTime d1 = Convert.ToDateTime(this.DatePicker1.Text);
                DateTime d2 = Convert.ToDateTime(this.DatePicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            catch
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000468", "日期格式不正确！") + "');", true);
                return;
            }
        }
        //Chart1.DataSource = ConstructData();

        //if (this.ddl_image.SelectedValue == "1")
        //{

        //    Chart1.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(209, 237, 254);

        //    Chart1.Series["Series1"].ChartType = SeriesChartType.Pie;

        //    Chart1.Series["Series1"].ToolTip = "#VALX: #VAL";
        //    Chart1.Series["Series1"].Label = "#VALX:#PERCENT{P}";
        //    Chart1.Series["Series1"].LegendText = "#VALX";
        //    Chart1.Series["Series1"].PostBackValue = "#INDEX";
        //    Chart1.Series["Series1"].LegendPostBackValue = "#INDEX";
        //    Chart1.Series["Series1"].BorderColor = Color.DarkGray;
        //}
        //else
        //{
        //    if (this.ddl_image.SelectedValue == "2")
        //    {
        //        Chart1.Series["Series1"].ChartType = SeriesChartType.Column;

        //        Chart1.Series["Series1"].Label = "#VAL";
        //        Chart1.Series["Series1"].Points.DataBind(ConstructData(), "Text", "Value", "");

        //        Chart1.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(100, 209, 237, 254);

        //        Chart1.Legends[0].Enabled = false;
        //    }
        //}

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
    protected void btn_view_Click(object sender, System.EventArgs e)
    {
        showChat();
    }
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
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000465", "请输入要查询的日期区间！") + "');", true);
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
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "alert('" + GetTran("000468", "日期格式不正确！") + "');", true);
                return;
            }
            Session["Begin"] = DateTime.Parse(this.DatePicker1.Text).ToUniversalTime().ToString();
            Session["End"] = (DateTime.Parse(this.DatePicker2.Text).AddHours(23).AddMinutes(59).AddSeconds(59)).ToUniversalTime().ToString();
            System.Web.UI.ScriptManager.RegisterStartupScript(Page, GetType(), "success2", "window.open('StoreReportDetail.aspx?Flag=Country');", true);

        }
        btn_view_Click(null, null);
    }
}
