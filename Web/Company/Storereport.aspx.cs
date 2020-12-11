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

using System.Data.SqlClient;
using ReportControl.Demo;
using Standard;
using BLL.CommonClass;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

public partial class Company_Storereport : BLL.TranslationBase 
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
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Permissions.CheckManagePermission(Model.Other.EnumCompanyPermission.ReportStorereport);

        AjaxPro.Utility.RegisterTypeForAjax(typeof(AjaxClass));

        // 在此处放置用户代码以初始化页面
        if (!IsPostBack)
        {
            this.DatePicker1.Text = CommonDataBLL.GetDateBegin().ToString();
            this.DatePicker2.Text = CommonDataBLL.GetDateEnd().ToString();
            this.Btn_Dsearch.Click += new System.EventHandler(this.Btn_Dsearch_Click);
            this.btn_view.Click += new System.EventHandler(this.btn_view_Click);
            showChat();
        }
        Translations();
    }
    /// <summary>
    /// 翻译
    /// </summary>
    private void Translations()
    {
        this.Btn_Dsearch.Text = GetTran("000154", "按服务机构汇总");
        this.Btn_Countrysearch.Text = GetTran("007575", "按国家汇总");
        //this.TranControls(this.ddl_image,
        //       new string[][]{
        //            new string []{"000529","饼图"},
        //            new string []{"000531","柱形图"}
        //        });

        this.TranControls(this.btn_view, new string[][] { new string[] { "000566", "显 示" } });
        
    }

    protected void Btn_Dsearch_Click(object sender, EventArgs e)
    {
        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001377", "请输入要查询的日期区间") + "!');</script>");
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
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001379", "日期格式不正确") + "!');</script>");
                return;
            }
            Session["Begin"] = this.DatePicker1.Text;
            Session["End"] = this.DatePicker2.Text;


            Response.Write("<script lanagage='javascript'>window.open('StoreArea.aspx?Flag=1')</script>");
        }
        showChat();
    }

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

      
    }
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

       

        table = DAL.DBHelper.ExecuteDataTable("Store_info_getDatabyArea", param, CommandType.StoredProcedure);
 //column = "city";
 //       result = "Bnum";



        /*
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
        }*/
        return coll;
    }
    
    /// <summary>
    /// 按按国家汇总
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_Countrysearch_Click(object sender, EventArgs e)
    {
        if (this.DatePicker1.Text.Length < 1 || this.DatePicker2.Text.Length < 1)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001377", "请输入要查询的日期区间") + "!');</script>");
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
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + GetTran("001379", "日期格式不正确") + "!');</script>");
                return;
            }
            Session["Begin"] = this.DatePicker1.Text;
            Session["End"] = this.DatePicker2.Text;


            Response.Write("<script lanagage='javascript'>window.open('StoreArea.aspx?Flag=Country')</script>");
        }
    }
}
