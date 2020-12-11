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

public partial class Company_MemberArea : BLL.TranslationBase 
{
    protected DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        // 在此处放置用户代码以初始化页面
        this.lbl_message.Visible = false;

        this.lbl_Begin.Text = Session["Begin"].ToString();

        this.lbl_End.Text = Session["End"].ToString();
        if (!this.IsPostBack)
        {
            Bind();

        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.GridView1,
               new string[][]{
                    new string []{"000012","序号"},
                    new string []{"000109","省份"},
                    new string []{"001474","期初"},
                    new string []{"001475","新增"},
                    new string []{"001476","期未"}
                });


    }

    private void Bind()
    {
        SqlParameter[] param ={
									new SqlParameter("@BeginDate",SqlDbType.DateTime),
									new SqlParameter("@EndDate",SqlDbType.DateTime)
								 };
        param[0].Value = Session["Begin"].ToString();
        param[1].Value = Session["End"].ToString();
        if (Request.QueryString["Flag"].ToString() == "1")
        {
            dt = DAL.DBHelper.ExecuteDataTable("M_info_getDatabyArea", param, CommandType.StoredProcedure);
        }
        else
        {
            dt = DAL.DBHelper.ExecuteDataTable("M_info_getDatabyCountry", param, CommandType.StoredProcedure);
        }

        if (dt.Rows.Count < 1)
        {
            this.lbl_message.Text = GetTran("000634", "没有相关信息!!");
            this.lbl_message.Visible = true;
        }
        else
        {
            if (Request.QueryString["Flag"].ToString() == "1")
            {
                this.GridView1.DataSource = dt;
                this.GridView1.DataBind();
                this.GridView2.Visible = false;
            }
            else
            {
                this.GridView2.DataSource = dt;
                this.GridView2.DataBind();
                this.GridView1.Visible = false;
            }
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
            ((Label)e.Row.FindControl("lbl_code")).Text = Convert.ToString(e.Row.RowIndex + 1);
        }
    }
}
