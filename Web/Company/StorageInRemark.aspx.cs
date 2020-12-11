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
using System.Data.SqlClient;
using DAL;
using System.Xml.Linq;

public partial class Company_StorageInRemark : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //检查相应权限
       // Response.Cache.SetExpires(DateTime.Now);
        //			Permissions.CheckManagePermission (Standard.Config .ENUM_COMPANY_PERMISSION.Storage_StorageInAdmin );		

        Permissions.ComRedirect(Page, Permissions.redirUrl);
        if (Request.QueryString["ID"] == null)
        {
            Response.Write("错误参数");
            Response.End();
        }
        if (!Page.IsPostBack)
        {
            string sSQL = "SELECT Note FROM InventoryDoc WHERE DocID = @DocID";
            SqlParameter[] para ={
										 new SqlParameter("@DocID" ,SqlDbType .VarChar ,20) 
									 };
            para[0].Value = Request.QueryString["ID"].ToString();

            Response.Write(DBHelper.ExecuteScalar(sSQL, para, CommandType.Text).ToString());
        }
    }
}
