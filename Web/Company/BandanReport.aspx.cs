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
using Model.Other;
using System.Data.SqlClient;
using BLL.CommonClass;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

public partial class Company_BandanReport : BLL.TranslationBase
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
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        Response.Cache.SetExpires(DateTime.Now);       
        //Right
        Permissions.CheckManagePermission(EnumCompanyPermission.ReportBaodanReport);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));
        if (!IsPostBack)
        {
            Translations_More();
            txtBeginTime.Text = CommonDataBLL.GetDateBegin();
            txtEndTime.Text = CommonDataBLL.GetDateEnd();

            Session["Begin"] = this.txtBeginTime.Text;
            Session["End"] = this.txtEndTime.Text;
            btn_view_Click(null, null);            
        }
       
    }

    /// <summary>
    /// Translation
    /// </summary>
    protected void Translations_More()
    {
        TranControls(btnCitySearch, new string[][] { new string[] { "000493", "按省市汇总" } });
        TranControls(btnProductSearch, new string[][] { new string[] { "000495", "按产品汇总" } });
        TranControls(btnCountrySearch, new string[][] { new string[] { "000000", "按按国家汇总" } });
        TranControls(btn_image, new string[][] { new string[] { "000298", "图形分析" } });
        TranControls(btn_view, new string[][] { new string[] { "000177", "显示" } });

        TranControls(rbtn_type, new string[][] 
                        { 
                                new string[] { "000109", "省份" }
                        }
                    );

        TranControls(ddl_type, new string[][] 
                        { 
                                new string[] { "000508", "首次报单" }, 
                                new string[] { "000513", "重复消费" }
                        }
                    );

        TranControls(ddl_item, new string[][] 
                        { 
                                new string[] { "000534", "小计" }, 
                                new string[] { "000517", "已支付" }, 
                                new string[] { "000521", "未支付" }
                        }
                    );

        TranControls(ddl_image, new string[][] 
                        { 
                                new string[] { "000529", "饼图" }, 
                                new string[] { "000531", "柱形图" }
                        }
                    );     
    }

    protected void btn_image_Click(object sender, System.EventArgs e)
    {
        if (this.txtBeginTime.Text.Length < 1 || this.txtEndTime.Text.Length < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000465","请输入要查询的日期区间!")));
            return;
        }
        else
        {
            try
            {
                DateTime d1 = Convert.ToDateTime(this.txtBeginTime.Text);
                DateTime d2 = Convert.ToDateTime(this.txtEndTime.Text);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000468", "日期格式不正确!")));
                return;
            }
            Session["Begin"] = this.txtBeginTime.Text;
            Session["End"] = this.txtEndTime.Text;

            Response.Write("<script lanagage='javascript'>window.open('SaleGraph.aspx?Flag=1')</script>");
        }
    }

    protected ArrayList ConstructData()
    {
        int top = 9;
        bool need = true;
        decimal other = 0;
        string column = "";
        string result = "";
        DataTable table = new DataTable();
        ArrayList coll = new ArrayList();
        SqlParameter[] parm=new SqlParameter[]
        {
		    new SqlParameter("@BeginDate",SqlDbType.DateTime),
		    new SqlParameter("@EndDate",SqlDbType.DateTime)							
		};
        parm[0].Value = Session["Begin"].ToString();
        parm[1].Value = Session["End"].ToString();

        if (this.rbtn_type.SelectedIndex == 0)
        {
            column = "city";
            table = DBHelper.ExecuteDataTable("MemberOrder_GetDataByArea", parm, CommandType.StoredProcedure);
            if (this.ddl_type.SelectedValue == "0")
            {
                switch (this.ddl_item.SelectedValue)
                {
                    case "-1":
                        result = "BAbaodan";
                        break;
                    case "0":
                        result = "BACheckOut";
                        break;
                    case "1":
                        result = "BANCheckOut";
                        break;
                }
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
     

    protected void btn_view_Click(object sender, System.EventArgs e)
    {
        //BindData();

        //if (this.ddl_image.SelectedValue == "1")
        //{
        //    int pointIndex = 1;

        //    if (pointIndex >= 0 && pointIndex < displayChart.Series["Series1"].Points.Count)
        //    {
        //        displayChart.Series["Series1"].Points[pointIndex].CustomProperties += "Exploded=true";
        //    }
        //}
    }

    protected void btnStoreSearch_Click(object sender, System.EventArgs e)
    {
        if (this.txtBeginTime.Text.Length < 1 || this.txtEndTime.Text.Length < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000465", "请输入要查询的日期区间!")));
        }
        else
        {
            try
            {
                DateTime d1 = Convert.ToDateTime(this.txtBeginTime.Text);
                DateTime d2 = Convert.ToDateTime(this.txtEndTime.Text);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000468", "日期格式不正确!")));
                return;
            }
            Session["Begin"] = this.txtBeginTime.Text;
            Session["End"] = this.txtEndTime.Text;

            Response.Write("<script lanagage='javascript'>window.open('BaodanSumReport.aspx?Flag=store')</script>");
        }
        btn_view_Click(null, null);
    }

    protected void btnCitySearch_Click(object sender, System.EventArgs e)
    {
        if (this.txtBeginTime.Text.Length < 1 || this.txtEndTime.Text.Length < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000465", "请输入要查询的日期区间!")));
        }
        else
        {
            try
            {
                DateTime d1 = Convert.ToDateTime(this.txtBeginTime.Text);
                DateTime d2 = Convert.ToDateTime(this.txtEndTime.Text);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000468", "日期格式不正确!")));
                return;
            }
            Session["Begin"] = this.txtBeginTime.Text;
            Session["End"] = this.txtEndTime.Text;

            Response.Write("<script lanagage='javascript'>window.open('BaodanSumReport.aspx?Flag=city')</script>");
        }
        btn_view_Click(null, null);
    }

    protected void btnProductSearch_Click(object sender, System.EventArgs e)
    {
        if (this.txtBeginTime.Text.Length < 1 || this.txtEndTime.Text.Length < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000465", "请输入要查询的日期区间!")));
        }
        else
        {
            try
            {
                DateTime d1 = Convert.ToDateTime(this.txtBeginTime.Text);
                DateTime d2 = Convert.ToDateTime(this.txtEndTime.Text);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000468", "日期格式不正确!")));
                return;
            }
            Session["Begin"] = this.txtBeginTime.Text;
            Session["End"] = this.txtEndTime.Text;

            Response.Write("<script lanagage='javascript'>window.open('BaodanProductReport.aspx?Flag=1')</script>");
        }
        btn_view_Click(null, null);
    }
    //protected void displayChart_Click(object sender, ImageMapEventArgs e)
    //{
    //    displayChart.DataSource = ConstructData();
    //    displayChart.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(209, 237, 254);

    //    displayChart.Series["Series1"].ToolTip = "#VALX: #VAL";
    //    displayChart.Series["Series1"].LegendToolTip = "#PERCENT";
    //    displayChart.Series["Series1"].PostBackValue = "#INDEX";
    //    displayChart.Series["Series1"].LegendPostBackValue = "#INDEX";
    //    displayChart.Series["Series1"].BorderColor = Color.DarkGray;
    //    displayChart.Series["Series1"].XValueMember = "Text";
    //    displayChart.Series["Series1"].YValueMembers = "Value";
    //    displayChart.DataBind();

    //    int pointIndex = int.Parse(e.PostBackValue);

    //    if (pointIndex >= 0 && pointIndex < displayChart.Series["Series1"].Points.Count)
    //    {
    //        displayChart.Series["Series1"].Points[pointIndex].CustomProperties += "Exploded=true";
    //    }
    //}
    /// <summary>
    /// 按按国家汇总
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCountrySearch_Click(object sender, EventArgs e)
    {
        if (this.txtBeginTime.Text.Length < 1 || this.txtEndTime.Text.Length < 1)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000465", "请输入要查询的日期区间!")));
        }
        else
        {
            try
            {
                DateTime d1 = Convert.ToDateTime(this.txtBeginTime.Text);
                DateTime d2 = Convert.ToDateTime(this.txtEndTime.Text);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "", Transforms.ReturnAlert(GetTran("000468", "日期格式不正确!")));
                return;
            }
            Session["Begin"] = this.txtBeginTime.Text;
            Session["End"] = this.txtEndTime.Text;

            Response.Write("<script lanagage='javascript'>window.open('BaodanSumReport.aspx?Flag=Country')</script>");
        }
        btn_view_Click(null, null);
    }
}
