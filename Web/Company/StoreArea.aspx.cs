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

public partial class Company_StoreArea : BLL.TranslationBase
{
    protected DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        this.lbl_message.Visible = false;

        this.lbl_Begin.Text = Session["Begin"].ToString();

        this.lbl_End.Text = Session["End"].ToString();

        if (!this.IsPostBack)
        {
            Bind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((Label)e.Row.FindControl("lbl_code")).Text = Convert.ToString(e.Row.RowIndex + 1);
        }
    }

    private void Bind()
    {
        if (Request.QueryString["Flag"].ToString() == "1")
        {
            SqlParameter[] param ={
									 new SqlParameter("@BeginDate",SqlDbType.DateTime),
									 new SqlParameter("@EndDate",SqlDbType.DateTime)
								 };
            param[0].Value = Session["Begin"].ToString();
            param[1].Value = Session["End"].ToString();
            dt = DAL.DBHelper.ExecuteDataTable("Store_info_getDatabyArea", param, CommandType.StoredProcedure);
        }
        else
        {

            SqlParameter[] param ={
									 new SqlParameter("@BeginDate",SqlDbType.DateTime),
									 new SqlParameter("@EndDate",SqlDbType.DateTime)
								 };
            param[0].Value = Convert.ToDateTime(Session["Begin"].ToString()).ToUniversalTime();
            param[1].Value = Convert.ToDateTime(Session["End"].ToString()).AddDays(1).ToUniversalTime();

            dt = DAL.DBHelper.ExecuteDataTable("Store_info_getDatabyCountry", param, CommandType.StoredProcedure);

        }

        if (dt.Rows.Count < 1)
        {
            this.lbl_message.Text = "没有相关信息!!";
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
}
