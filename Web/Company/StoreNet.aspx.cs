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
using DAL;
using System.Data.SqlClient;
using BLL.other;

public partial class Company_StoreNet : BLL.TranslationBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
         Translations();
        if (!IsPostBack)
        {
            BindQS();
            BindData();
        }

        
    }


    private void Translations()
    {
        this.TranControls(this.Button1, new string[][] { new string[] { "000844", "显示" } });
        this.TranControls(this.GridView1, new string[][] { new string[] {"000037","服务机构编号"} ,
            new string[] { "000024", "会员编号" } ,
            new string[] { "000040", "服务机构名称" } ,
            new string[] { "001427", "服务机构级别" } 
        
        
        
        });


    }
    public void BindData()
    {
       
        if (TextBox1.Text.Trim() != "")
        {
            string number = DBHelper.ExecuteScalar("select Number from StoreInfo where StoreID=@StoreID", new SqlParameter("@StoreID", TextBox1.Text.Trim()), CommandType.Text) + "";

            if (number == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script type='text/javascript'>alert('" + GetTran("007568", "不存在此店铺！") + "')</script>");
                return;
            }

            DataTable dt = DBHelper.ExecuteDataTable("procDPNet", new SqlParameter[] { new SqlParameter("@bianhao", number), new SqlParameter("@qishu", DDLQs.SelectedValue) }, CommandType.StoredProcedure);
           
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Translations();

        }
    }

    public string GetStoreLevel(string level)
    {
        return DBHelper.ExecuteScalar("select levelstr from dbo.BSCO_Level where levelflag=1 and levelInt=@Level", new SqlParameter("@Level", level), CommandType.Text) + "";
    }

    public void BindQS()
    {
        DataTable dt = WTreeBLL.BindQS();

        DDLQs.DataSource = dt;
        DDLQs.DataTextField = "ExpectNum";
        DDLQs.DataValueField = "ExpectNum";
        DDLQs.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindData();
    }
}
