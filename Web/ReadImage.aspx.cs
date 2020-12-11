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

public partial class ReadImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //在此处放置用户代码以初始化页面
        string ProID = Request.QueryString["ProductID"];
        string StrSql = "Select ProductImage,ImageType from Product where ProductID=@ProID";
        SqlParameter[] para =
        {
            new SqlParameter("@ProID",ProID)
        };
        SqlDataReader myReader = DAL.DBHelper.ExecuteReader(StrSql, para, CommandType.Text);
        try
        {
            if (myReader.Read())
            {
                Response.Clear();

                Response.ContentType = myReader["ImageType"].ToString();
                Response.BinaryWrite((byte[])myReader["ProductImage"]);
            }
        }
        catch (Exception esp)
        {

        }
        finally
        {
            myReader.Close();
            myReader.Dispose();
        }
        Response.End();
    }
}