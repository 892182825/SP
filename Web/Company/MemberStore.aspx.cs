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

public partial class Company_MemberStore : BLL.TranslationBase 
{
    protected DataTable dt2;
    protected void Page_Load(object sender, EventArgs e)
    {
        Permissions.ComRedirect(Page, Permissions.redirUrl);
        // 在此处放置用户代码以初始化页面
        this.lbl_message2.Visible = false;
        this.lbl_begin2.Text = Session["Begin"].ToString();
        this.lbl_End2.Text = Session["End"].ToString();
        if (!this.IsPostBack)
        {
            Bind2();
        }
        Translations();
    }

    private void Translations()
    {
        this.TranControls(this.GridView1,
               new string[][]{
                    new string []{"000012","序号"},
                    new string []{"000150","店铺编号"},
                    new string []{"000039","店长姓名"},
                    new string []{"001474","期初"},
                    new string []{"001475","新增"},
                    new string []{"001476","期末"}
                });

      
    }

    private void Bind2()
    {
        SqlParameter[] param ={
									 new SqlParameter("@BeginDate",SqlDbType.DateTime),
									 new SqlParameter("@EndDate",SqlDbType.DateTime)
								 };
        param[0].Value = Session["Begin"].ToString();
        param[1].Value = Session["End"].ToString();
        dt2 = DAL.DBHelper.ExecuteDataTable("M_info_getDatabyStore", param, CommandType.StoredProcedure);
        if (dt2.Rows.Count < 1)
        {
            this.lbl_message2.Text = GetTran("000634", "没有相关信息!!");
            this.lbl_message2.Visible = true;
        }
        else
        {
            foreach (DataRow row in dt2.Rows)
            {
                row["Name"] = Encryption.Encryption.GetDecipherName(row["Name"].ToString());
               



            }


            this.GridView1.DataSource = dt2;
            this.GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((Label)e.Row.FindControl("lbl_code")).Text = Convert.ToString(e.Row.RowIndex + 1);
            e.Row.Attributes.Add("onmouseover", "bg=this.style.backgroundColor;this.style.backgroundColor='#FFFFCC'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=bg");
        }
    }
}
